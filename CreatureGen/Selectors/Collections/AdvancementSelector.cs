using CreatureGen.Creatures;
using CreatureGen.Selectors.Selections;
using CreatureGen.Tables;
using DnDGen.Core.Selectors.Collections;
using DnDGen.Core.Selectors.Percentiles;
using System;
using System.Linq;

namespace CreatureGen.Selectors.Collections
{
    internal class AdvancementSelector : IAdvancementSelector
    {
        private readonly ITypeAndAmountSelector typeAndAmountSelector;
        private readonly IPercentileSelector percentileSelector;
        private readonly ICollectionSelector collectionSelector;

        public AdvancementSelector(ITypeAndAmountSelector typeAndAmountSelector, IPercentileSelector percentileSelector, ICollectionSelector collectionSelector)
        {
            this.typeAndAmountSelector = typeAndAmountSelector;
            this.percentileSelector = percentileSelector;
            this.collectionSelector = collectionSelector;
        }

        public bool IsAdvanced(string creature)
        {
            var typesAndAmounts = typeAndAmountSelector.Select(TableNameConstants.TypeAndAmount.Advancements, creature);

            return percentileSelector.SelectFrom(.1) && typesAndAmounts.Any();
        }

        public AdvancementSelection SelectRandomFor(string creature, CreatureType creatureType)
        {
            var typesAndAmounts = typeAndAmountSelector.Select(TableNameConstants.TypeAndAmount.Advancements, creature);
            var randomTypeAndAmount = collectionSelector.SelectRandomFrom(typesAndAmounts);
            var selection = GetAdvancementSelection(creature, creatureType, randomTypeAndAmount);

            return selection;
        }

        private AdvancementSelection GetAdvancementSelection(string creatureName, CreatureType creatureType, TypeAndAmountSelection typeAndAmount)
        {
            var selection = new AdvancementSelection();
            selection.AdditionalHitDice = typeAndAmount.Amount;

            var sections = typeAndAmount.Type.Split(',');
            selection.CasterLevelAdjustment = Convert.ToInt32(sections[DataIndexConstants.AdvancementSelectionData.CasterLevel]);
            selection.AdjustedChallengeRating = sections[DataIndexConstants.AdvancementSelectionData.ChallengeRating];
            selection.ConstitutionAdjustment = Convert.ToInt32(sections[DataIndexConstants.AdvancementSelectionData.ConstitutionAdjustment]);
            selection.DexterityAdjustment = Convert.ToInt32(sections[DataIndexConstants.AdvancementSelectionData.DexterityAdjustment]);
            selection.NaturalArmorAdjustment = Convert.ToInt32(sections[DataIndexConstants.AdvancementSelectionData.NaturalArmorAdjustment]);
            selection.Reach = Convert.ToDouble(sections[DataIndexConstants.AdvancementSelectionData.Reach]);
            selection.Size = sections[DataIndexConstants.AdvancementSelectionData.Size];
            selection.Space = Convert.ToDouble(sections[DataIndexConstants.AdvancementSelectionData.Space]);
            selection.StrengthAdjustment = Convert.ToInt32(sections[DataIndexConstants.AdvancementSelectionData.StrengthAdjustment]);

            if (IsBarghest(creatureName))
            {
                selection.StrengthAdjustment = selection.AdditionalHitDice;
                selection.ConstitutionAdjustment = selection.AdditionalHitDice;
                selection.NaturalArmorAdjustment = selection.AdditionalHitDice;
                selection.CasterLevelAdjustment = selection.AdditionalHitDice;
            }

            selection.AdjustedChallengeRating = AdjustChallengeRating(selection.AdjustedChallengeRating, selection.AdditionalHitDice, creatureType.Name);

            return selection;
        }

        private string AdjustChallengeRating(string challengeRating, int additionalHitDice, string creatureType)
        {
            var challengeRatings = ChallengeRatingConstants.GetOrdered();
            var index = Array.IndexOf(challengeRatings, challengeRating);
            var oneIndex = Array.IndexOf(challengeRatings, ChallengeRatingConstants.One);

            var creatureTypeDivisor = typeAndAmountSelector.SelectOne(TableNameConstants.TypeAndAmount.Advancements, creatureType);
            var divisor = creatureTypeDivisor.Amount;
            var advancementAmount = additionalHitDice / divisor;

            if (index + advancementAmount < oneIndex)
                return challengeRatings[index + advancementAmount];

            advancementAmount -= oneIndex - index;

            return Convert.ToString(1 + advancementAmount);
        }

        private bool IsBarghest(string creatureName)
        {
            return creatureName == CreatureConstants.Barghest || creatureName == CreatureConstants.Barghest_Greater;
        }
    }
}
