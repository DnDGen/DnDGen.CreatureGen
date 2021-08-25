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
                    var templateChallengeRatings = applicator.GetChallengeRatings();
                    if (templateChallengeRatings != null)
                    {
                        if (!templateChallengeRatings.Contains(challengeRating))
                        {
                            return false;
                        }
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

                    //INFO: If the Max CR is null, then there is no upper limit.
                    //This means that the CR will only increase, not decrease, so we can filter out any base creatures that have a CR higher than the filter
                    var templateChallengeRatings = templateApplicator.GetChallengeRatings();
                    if (templateChallengeRatings == null)
                    {
                        filteredBaseCreatures = filteredBaseCreatures.Where(c => !ChallengeRatingConstants.IsGreaterThan(allData[c].ChallengeRating, challengeRating));
                    }
                    //If there is a Max CR, we can check if the filter is greater than that maximum value.
                    else if (!templateChallengeRatings.Contains(challengeRating))
                    {
                        continue;
                    }
                    //If there is a Max CR, then the CR for any creature should decrease, so we can filter out any base creatures that have a CR lower than the filter
                    else
                    {
                        filteredBaseCreatures = filteredBaseCreatures.Where(c => !ChallengeRatingConstants.IsGreaterThan(challengeRating, allData[c].ChallengeRating));

                        //We can also filter based on the hit dice range that would grant the desired challenge rating filter
                        var hitDiceRange = templateApplicator.GetHitDiceRange(challengeRating);
                        filteredBaseCreatures = filteredBaseCreatures.Where(c => allHitDice[c] > hitDiceRange.Lower && allHitDice[c] <= hitDiceRange.Upper);
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

        private bool CreatureInRange(TemplateApplicator applicator, string creatureChallengeRating, string filterChallengeRating)
        {
            var templateChallengeRatings = applicator.GetChallengeRatings(creatureChallengeRating);
            var crInRange = templateChallengeRatings.Contains(filterChallengeRating);

            return crInRange;
        }
    }
}