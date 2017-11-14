namespace CreatureGen.Attacks
{
    public class Attack
    {
        public string Name { get; set; }
        public int TotalAttackBonus { get; set; }
        public bool IsMelee { get; set; }
        public string Damage { get; set; }
        public bool IsPrimary { get; set; }
        public bool IsSpecial { get; set; }
        public bool IsNatural { get; set; }

        public Attack()
        {
            Name = string.Empty;
            Damage = string.Empty;
        }
    }
}
