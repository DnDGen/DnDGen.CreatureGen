using CreatureGen.Abilities;
using CreatureGen.Feats;

namespace CreatureGen.Attacks
{
    public class Attack
    {
        public string Name { get; set; }
        public int BaseAttackBonus { get; set; }
        public int SizeModifierForAttackBonus { get; set; }
        public Ability BaseAbility { get; set; }
        public bool IsMelee { get; set; }
        public string Damage { get; set; }
        public bool IsPrimary { get; set; }
        public bool IsSpecial { get; set; }
        public bool IsNatural { get; set; }
        public int SecondaryAttackModifiers { get; set; }
        public Frequency Frequency { get; set; }
        public SaveDieCheck Save { get; set; }
        public string AttackType { get; set; }

        public int TotalAttackBonus
        {
            get
            {
                var abilityBonus = 0;
                if (BaseAbility != null)
                    abilityBonus = BaseAbility.Modifier;

                return BaseAttackBonus + abilityBonus + SizeModifierForAttackBonus + SecondaryAttackModifiers;
            }
        }

        public Attack()
        {
            Name = string.Empty;
            Damage = string.Empty;
        }
    }
}
