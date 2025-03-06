using DnDGen.CreatureGen.Creatures;
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
        public int ChallengeRatingDivisor { get; set; }
        public int CasterLevelAdjustment { get; set; }
        public int MaxHitDice { get; private set; }

        public override Func<string[], AdvancementDataSelection> MapTo => Map;
        public override Func<AdvancementDataSelection, string[]> MapFrom => Map;

        public override int SectionCount => 10;

        public static AdvancementDataSelection Map(string[] splitData)
        {
            var selection = new AdvancementDataSelection
            {
                AdditionalHitDiceRoll = splitData[DataIndexConstants.AdvancementSelectionData.AdditionalHitDiceRoll],
                Size = splitData[DataIndexConstants.AdvancementSelectionData.Size],
                Space = Convert.ToDouble(splitData[DataIndexConstants.AdvancementSelectionData.Space]),
                Reach = Convert.ToDouble(splitData[DataIndexConstants.AdvancementSelectionData.Reach]),
                StrengthAdjustment = Convert.ToInt32(splitData[DataIndexConstants.AdvancementSelectionData.StrengthAdjustment]),
                ConstitutionAdjustment = Convert.ToInt32(splitData[DataIndexConstants.AdvancementSelectionData.ConstitutionAdjustment]),
                DexterityAdjustment = Convert.ToInt32(splitData[DataIndexConstants.AdvancementSelectionData.DexterityAdjustment]),
                NaturalArmorAdjustment = Convert.ToInt32(splitData[DataIndexConstants.AdvancementSelectionData.NaturalArmorAdjustment]),
                ChallengeRatingDivisor = Convert.ToInt32(splitData[DataIndexConstants.AdvancementSelectionData.ChallengeRatingDivisor]),
                AdjustedChallengeRating = splitData[DataIndexConstants.AdvancementSelectionData.AdjustedChallengeRating],
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
            data[DataIndexConstants.AdvancementSelectionData.AdjustedChallengeRating] = selection.AdjustedChallengeRating;

            return data;
        }

        public AdvancementDataSelection()
        {
            Size = string.Empty;
            AdjustedChallengeRating = string.Empty;
            AdditionalHitDiceRoll = string.Empty;
            ChallengeRatingDivisor = 1;
            MaxHitDice = int.MaxValue;
        }

        public bool AdvancementIsValid(Dice dice, int max)
        {
            MaxHitDice = max;
            return dice.Roll(AdditionalHitDiceRoll).AsPotentialMinimum() <= MaxHitDice;
        }

        public void SetAdditionalProperties(Dice dice)
        {
            AdditionalHitDice = dice.Roll(AdditionalHitDiceRoll).AsSum();
            if (AdditionalHitDice > MaxHitDice)
                AdditionalHitDice = MaxHitDice;

            var advancementAmount = AdditionalHitDice / ChallengeRatingDivisor;
            AdjustedChallengeRating = ChallengeRatingConstants.IncreaseChallengeRating(AdjustedChallengeRating, advancementAmount);
        }
    }
}
