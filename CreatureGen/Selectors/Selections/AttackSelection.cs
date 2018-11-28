namespace CreatureGen.Selectors.Selections
{
    internal class AttackSelection
    {
        public const char Divider = '@';

        public string Damage { get; set; }
        public string Name { get; set; }
        public bool IsPrimary { get; set; }
        public bool IsMelee { get; set; }
        public bool IsNatural { get; set; }
        public bool IsSpecial { get; set; }

        public AttackSelection()
        {
            Damage = string.Empty;
            Name = string.Empty;
        }
    }
}
