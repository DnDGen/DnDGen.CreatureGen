namespace CreatureGen.Selectors.Selections
{
    internal class AdvancementSelection
    {
        public int AdditionalHitDice { get; set; }
        public string Size { get; set; }
        public double Space { get; set; }
        public double Reach { get; set; }

        public AdvancementSelection()
        {
            Size = string.Empty;
        }
    }
}
