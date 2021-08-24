using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Selectors.Collections;
using DnDGen.CreatureGen.Tables;
using DnDGen.CreatureGen.Templates;
using DnDGen.Infrastructure.Generators;
using DnDGen.Infrastructure.Selectors.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Verifiers
{
    internal class CreatureVerifier : ICreatureVerifier
    {
        private readonly JustInTimeFactory factory;
        private readonly ICreatureDataSelector creatureDataSelector;
        private readonly ICollectionSelector collectionsSelector;

        public CreatureVerifier(JustInTimeFactory factory, ICreatureDataSelector creatureDataSelector, ICollectionSelector collectionsSelector)
        {
            this.factory = factory;
            this.creatureDataSelector = creatureDataSelector;
            this.collectionsSelector = collectionsSelector;
        }

        public bool CanBeCharacter(string creatureName)
        {
            var creatureData = creatureDataSelector.SelectFor(creatureName);
            return creatureData.LevelAdjustment.HasValue;
        }

        public bool VerifyCompatibility(bool asCharacter, string creature = null, string template = null, string type = null, string challengeRating = null)
        {
            IEnumerable<string> baseCreatures = new[] { creature };
            if (string.IsNullOrEmpty(creature))
            {
                baseCreatures = collectionsSelector.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.All);
            }

            if (asCharacter)
            {
                var characters = collectionsSelector.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Characters);
                baseCreatures = baseCreatures.Intersect(characters);
            }

            var creatures = baseCreatures;
            if (!string.IsNullOrEmpty(template))
            {
                var applicator = factory.Build<TemplateApplicator>(template);

                if (!string.IsNullOrEmpty(challengeRating))
                {
                    //INFO: Skeleton and Zombie are the only templates that might decrease a challenge rating
                    //If it IS a Skeleton or Zombie, we can do other filtering based on knowing their max potential CR
                    if (template == CreatureConstants.Templates.Skeleton && ChallengeRatingConstants.IsGreaterThan(challengeRating, ChallengeRatingConstants.CR8))
                    {
                        return false;
                    }
                    else if (template == CreatureConstants.Templates.Zombie && ChallengeRatingConstants.IsGreaterThan(challengeRating, ChallengeRatingConstants.CR6))
                    {
                        return false;
                    }
                }

                creatures = creatures.Where(c => applicator.IsCompatible(c, type, challengeRating));

                return creatures.Any();
            }

            if (!string.IsNullOrEmpty(type))
            {
                var ofType = collectionsSelector.Explode(TableNameConstants.Collection.CreatureGroups, type);
                creatures = ofType.Intersect(creatures);
            }

            if (!string.IsNullOrEmpty(challengeRating))
            {
                var ofChallengeRating = collectionsSelector.Explode(TableNameConstants.Collection.CreatureGroups, challengeRating);
                creatures = ofChallengeRating.Intersect(creatures);
            }

            if (creatures.Any())
                return true;

            var templates = collectionsSelector.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Templates);
            var allData = creatureDataSelector.SelectAll();

            foreach (var otherTemplate in templates)
            {
                var templateApplicator = factory.Build<TemplateApplicator>(otherTemplate);
                var filteredBaseCreatures = baseCreatures;

                if (!string.IsNullOrEmpty(challengeRating))
                {
                    //INFO: Skeleton and Zombie are the only templates that might decrease a challenge rating
                    //So, as long as this is not one of those, we can filter out any creatures that have a HIGHER CR than the filter
                    //If it IS a Skeleton or Zombie, we can do other filtering based on knowing their max potential CR
                    if (otherTemplate != CreatureConstants.Templates.Skeleton && otherTemplate != CreatureConstants.Templates.Zombie)
                    {
                        filteredBaseCreatures = filteredBaseCreatures.Where(c => !ChallengeRatingConstants.IsGreaterThan(allData[c].ChallengeRating, challengeRating));
                    }
                    else if (otherTemplate == CreatureConstants.Templates.Skeleton && ChallengeRatingConstants.IsGreaterThan(challengeRating, ChallengeRatingConstants.CR8))
                    {
                        continue;
                    }
                    else if (otherTemplate == CreatureConstants.Templates.Zombie && ChallengeRatingConstants.IsGreaterThan(challengeRating, ChallengeRatingConstants.CR6))
                    {
                        continue;
                    }
                }

                if (!string.IsNullOrEmpty(type))
                {
                    //INFO: Unless this type is added by a template, it must already exist on the base creature
                    //So first, we check to see if the template could return this type for a human and a rat
                    //If not, then we can filter the base creatures down to ones that already have this type
                    var templateTypes = templateApplicator.GetPotentialTypes(CreatureConstants.Human)
                        .Except(new[] { CreatureConstants.Types.Humanoid, CreatureConstants.Types.Subtypes.Human });
                    if (templateApplicator.IsCompatible(CreatureConstants.Rat))
                    {
                        var ratTypes = templateApplicator.GetPotentialTypes(CreatureConstants.Rat)
                            .Except(new[] { CreatureConstants.Types.Vermin });
                        templateTypes = templateTypes.Union(ratTypes);
                    }

                    if (!templateTypes.Contains(type))
                    {
                        var ofType = collectionsSelector.Explode(TableNameConstants.Collection.CreatureGroups, type);
                        filteredBaseCreatures = filteredBaseCreatures.Intersect(ofType);
                    }
                }

                var templateCreatures = filteredBaseCreatures.Where(c => templateApplicator.IsCompatible(c, type, challengeRating));
                if (templateCreatures.Any())
                    return true;
            }

            return false;
        }
    }
}