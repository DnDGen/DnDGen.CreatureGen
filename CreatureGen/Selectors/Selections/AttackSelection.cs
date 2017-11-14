namespace CreatureGen.Selectors.Selections
{
    internal class AttackSelection
    {
        public string Damage { get; set; }
        public string Name { get; set; }
        public bool IsPrimary { get; set; }
        public bool IsMelee { get; set; }

        public AttackSelection()
        {
            Damage = string.Empty;
            Name = string.Empty;
        }
    }
}
