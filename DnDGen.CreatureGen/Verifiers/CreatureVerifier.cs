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
        private readonly IAdjustmentsSelector adjustmentSelector;

        public CreatureVerifier(
            JustInTimeFactory factory,
            ICreatureDataSelector creatureDataSelector,
            ICollectionSelector collectionsSelector,
            IAdjustmentsSelector adjustmentSelector)
        {
            this.factory = factory;
            this.creatureDataSelector = creatureDataSelector;
            this.collectionsSelector = collectionsSelector;
            this.adjustmentSelector = adjustmentSelector;
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
            var allHitDice = adjustmentSelector.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice);

            foreach (var otherTemplate in templates)
            {
                var templateApplicator = factory.Build<TemplateApplicator>(otherTemplate);
                var filteredBaseCreatures = baseCreatures;

                if (!string.IsNullOrEmpty(challengeRating))
                {
                    filteredBaseCreatures = filteredBaseCreatures.Where(c => CreatureInRange(templateApplicator, allData[c].ChallengeRating, challengeRating));

                    ////INFO: If the Max CR is null, then there is no upper limit.
                    ////This means that the CR will only increase, not decrease, so we can filter out any base creatures that have a CR higher than the filter
                    //var crRange = templateApplicator.GetChallengeRatingRange();
                    //if (string.IsNullOrEmpty(crRange.Upper))
                    //{
                    //    filteredBaseCreatures = filteredBaseCreatures.Where(c => !ChallengeRatingConstants.IsGreaterThan(allData[c].ChallengeRating, challengeRating));
                    //}
                    ////If there is a Max CR, we can check if the filter is greater than that maximum value.
                    //else if (ChallengeRatingConstants.IsGreaterThan(challengeRating, crRange.Upper) || ChallengeRatingConstants.IsGreaterThan(crRange.Lower, challengeRating))
                    //{
                    //    continue;
                    //}
                    ////If there is a Max CR, then the CR for any creature should decrease, so we can filter out any base creatures that have a CR lower than the filter
                    //else
                    //{
                    //    filteredBaseCreatures = filteredBaseCreatures.Where(c => !ChallengeRatingConstants.IsGreaterThan(challengeRating, allData[c].ChallengeRating));

                    //    var hitDiceRange = templateApplicator.GetHitDiceRange(challengeRating);
                    //    filteredBaseCreatures = filteredBaseCreatures.Where(c => allHitDice[c] > hitDiceRange.Lower && allHitDice[c] <= hitDiceRange.Upper);

                    //    //TODO: If this is Skeleton or Zombie, and the CR is low, then there might be a large number of FALSE results, which will take a long time
                    //    //Need to figure out another filter we can add here
                    //    //Filtering by type could be possible, but at that point we are doing the work of the template applicator, which feels weird
                    //    //Maybe make the creature types a public property, so we can reference it here?
                    //    //Then filter to creatures in those groups
                    //    //Filtering by hit points (any > 20 Skeleton not valid, > 10 for Zombie), but wouldn't eliminate many options, and again, is work of the template applicator
                    //    //Maybe return a hit point range given the CR filter, and remove creatures not in that range
                    //}
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

        private bool CreatureInRange(TemplateApplicator applicator, string creatureChallengeRating, string filterChallengeRating)
        {
            var crRange = applicator.GetChallengeRatingRange(creatureChallengeRating);
            return !ChallengeRatingConstants.IsGreaterThan(crRange.Lower, filterChallengeRating)
                && !ChallengeRatingConstants.IsGreaterThan(filterChallengeRating, crRange.Upper);
        }
    }
}