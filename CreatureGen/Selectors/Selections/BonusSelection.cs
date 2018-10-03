namespace CreatureGen.Selectors.Selections
{
    internal class BonusSelection
    {
        public string Source { get; set; }
        public int Bonus { get; set; }
        public string Condition { get; set; }

        public BonusSelection()
        {
            Source = string.Empty;
            Condition = string.Empty;
        }
    }
}
