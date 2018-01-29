namespace CreatureGen.Selectors.Selections
{
    internal class CreatureDataSelection
    {
        public string Size { get; set; }
        public string ChallengeRating { get; set; }
        public double Space { get; set; }
        public double Reach { get; set; }
        public int? LevelAdjustment { get; set; }

        public CreatureDataSelection()
        {
            Size = string.Empty;
            ChallengeRating = string.Empty;
        }
    }
}
