namespace CreatureGen.Selectors.Selections
{
    internal class CreatureDataSelection
    {
        public string Size { get; set; }
        public string ChallengeRating { get; set; }
        public int Space { get; set; }
        public int Reach { get; set; }

        public CreatureDataSelection()
        {
            Size = string.Empty;
            ChallengeRating = string.Empty;
        }
    }
}
