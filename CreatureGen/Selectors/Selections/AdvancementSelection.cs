namespace CreatureGen.Selectors.Selections
{
    internal class AdvancementSelection
    {
        public int AdditionalHitDice { get; set; }
        public string Size { get; set; }
        public double Space { get; set; }
        public double Reach { get; set; }
        public int StrengthAdjustment { get; set; }
        public int DexterityAdjustment { get; set; }
        public int ConstitutionAdjustment { get; set; }
        public int NaturalArmorAdjustment { get; set; }
        public string AdjustedChallengeRating { get; set; }
        public int CasterLevelAdjustment { get; set; }

        public AdvancementSelection()
        {
            Size = string.Empty;
            AdjustedChallengeRating = string.Empty;
        }
    }
}
