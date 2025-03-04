using DnDGen.CreatureGen.Tables;
using DnDGen.Infrastructure.Models;
using DnDGen.RollGen;
using System;

namespace DnDGen.CreatureGen.Selectors.Selections
{
    internal class AdvancementDataSelection : DataSelection<AdvancementDataSelection>
    {
        public string AdditionalHitDiceRoll { get; set; }
        public int AdditionalHitDice { get; private set; }
        public string Size { get; set; }
        public double Space { get; set; }
        public double Reach { get; set; }
        public int StrengthAdjustment { get; set; }
        public int DexterityAdjustment { get; set; }
        public int ConstitutionAdjustment { get; set; }
        public int NaturalArmorAdjustment { get; set; }
        public string AdjustedChallengeRating { get; set; }
        public int ChallengeRatingAdjustment { get; set; }
        public int ChallengeRatingDivisor { get; set; }
        public int CasterLevelAdjustment { get; set; }

        public override Func<string[], AdvancementDataSelection> MapTo => Map;
        public override Func<AdvancementDataSelection, string[]> MapFrom => Map;

        public override int SectionCount => 10;

        private int maxHitDice;

        public static AdvancementDataSelection Map(string[] splitData)
        {
            var selection = new AdvancementDataSelection
            {
                AdditionalHitDiceRoll = splitData[DataIndexConstants.AdvancementSelectionData.AdditionalHitDiceRoll],
                Size = splitData[DataIndexConstants.AdvancementSelectionData.Size],
                Space = Convert.ToInt32(splitData[DataIndexConstants.AdvancementSelectionData.Space]),
                Reach = Convert.ToInt32(splitData[DataIndexConstants.AdvancementSelectionData.Reach]),
                StrengthAdjustment = Convert.ToInt32(splitData[DataIndexConstants.AdvancementSelectionData.StrengthAdjustment]),
                ConstitutionAdjustment = Convert.ToInt32(splitData[DataIndexConstants.AdvancementSelectionData.ConstitutionAdjustment]),
                DexterityAdjustment = Convert.ToInt32(splitData[DataIndexConstants.AdvancementSelectionData.DexterityAdjustment]),
                NaturalArmorAdjustment = Convert.ToInt32(splitData[DataIndexConstants.AdvancementSelectionData.NaturalArmorAdjustment]),
                ChallengeRatingDivisor = Convert.ToInt32(splitData[DataIndexConstants.AdvancementSelectionData.ChallengeRatingDivisor]),
                ChallengeRatingAdjustment = Convert.ToInt32(splitData[DataIndexConstants.AdvancementSelectionData.ChallengeRatingDivisor]),
            };

            return selection;
        }

        public static string[] Map(AdvancementDataSelection selection)
        {
            var data = new string[selection.SectionCount];
            data[DataIndexConstants.AdvancementSelectionData.Size] = selection.Size;
            data[DataIndexConstants.AdvancementSelectionData.Space] = selection.Space.ToString();
            data[DataIndexConstants.AdvancementSelectionData.Reach] = selection.Reach.ToString();
            data[DataIndexConstants.AdvancementSelectionData.AdditionalHitDiceRoll] = selection.AdditionalHitDiceRoll;
            data[DataIndexConstants.AdvancementSelectionData.StrengthAdjustment] = selection.StrengthAdjustment.ToString();
            data[DataIndexConstants.AdvancementSelectionData.ConstitutionAdjustment] = selection.ConstitutionAdjustment.ToString();
            data[DataIndexConstants.AdvancementSelectionData.DexterityAdjustment] = selection.DexterityAdjustment.ToString();
            data[DataIndexConstants.AdvancementSelectionData.NaturalArmorAdjustment] = selection.NaturalArmorAdjustment.ToString();
            data[DataIndexConstants.AdvancementSelectionData.ChallengeRatingDivisor] = selection.ChallengeRatingDivisor.ToString();
            data[DataIndexConstants.AdvancementSelectionData.ChallengeRatingAdjustment] = selection.ChallengeRatingAdjustment.ToString();

            return data;
        }

        public AdvancementDataSelection()
        {
            Size = string.Empty;
            AdjustedChallengeRating = string.Empty;
        }

        public bool AdvancementIsValid(Dice dice, int max)
        {
            maxHitDice = max;
            return dice.Roll(AdditionalHitDiceRoll).AsPotentialMinimum() <= maxHitDice;
        }

        public void SetAdditionalProperties(Dice dice)
        {
            AdditionalHitDice = dice.Roll(AdditionalHitDiceRoll).AsSum();
            if (AdditionalHitDice > maxHitDice)
                AdditionalHitDice = maxHitDice;

            AdjustedChallengeRating = AdjustChallengeRating();
        }

        private string AdjustChallengeRating()
        {
            var sizeAdjustedChallengeRating = AdjustChallengeRating(size, advancedSize, originalChallengeRating);
            var hitDieAdjustedChallengeRating = AdjustChallengeRating(sizeAdjustedChallengeRating, additionalHitDice, creatureType);

            return hitDieAdjustedChallengeRating;
        }

        private string AdjustChallengeRating(string originalSize, string advancedSize, string originalChallengeRating)
        {
            var sizes = SizeConstants.GetOrdered();
            var originalSizeIndex = Array.IndexOf(sizes, originalSize);
            var advancedIndex = Array.IndexOf(sizes, advancedSize);
            var largeIndex = Array.IndexOf(sizes, SizeConstants.Large);

            if (advancedIndex < largeIndex || originalSize == advancedSize)
            {
                return originalChallengeRating;
            }

            var increase = advancedIndex - Math.Max(largeIndex - 1, originalSizeIndex);
            var adjustedChallengeRating = ChallengeRatingConstants.IncreaseChallengeRating(originalChallengeRating, increase);

            return adjustedChallengeRating;
        }

        private string AdjustChallengeRating(string originalChallengeRating, int additionalHitDice, string creatureType)
        {
            var creatureTypeDivisor = typeAndAmountSelector.SelectOne(TableNameConstants.TypeAndAmount.Advancements, creatureType);
            var divisor = creatureTypeDivisor.Amount;
            var advancementAmount = additionalHitDice / divisor;

            return ChallengeRatingConstants.IncreaseChallengeRating(originalChallengeRating, advancementAmount);
        }
    }
}
