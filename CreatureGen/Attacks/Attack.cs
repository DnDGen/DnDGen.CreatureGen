using CreatureGen.Abilities;
using CreatureGen.Feats;
using System.Collections.Generic;

namespace CreatureGen.Attacks
{
    public class Attack
    {
        public string Name { get; set; }
        public int BaseAttackBonus { get; set; }
        public int SizeModifier { get; set; }
        public Ability BaseAbility { get; set; }
        public bool IsMelee { get; set; }
        public string DamageRoll { get; set; }
        public string DamageEffect { get; set; }
        public int DamageBonus { get; set; }
        public bool IsPrimary { get; set; }
        public bool IsSpecial { get; set; }
        public bool IsNatural { get; set; }
        public int SecondaryAttackPenalty { get; set; }
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

                return BaseAttackBonus + abilityBonus + SizeModifier + SecondaryAttackPenalty;
            }
        }

        public int[] AttackBonuses
        {
            get
            {
                var bonuses = new List<int>();
                var decrement = 0;

                do
                {
                    bonuses.Add(TotalAttackBonus - decrement);
                    decrement += 5;
                }
                while (BaseAttackBonus - decrement > 0 && IsPrimary && !IsNatural);

                return bonuses.ToArray();
            }
        }

        public string Damage
        {
            get
            {
                var damage = DamageRoll;
                if (DamageBonus > 0)
                    damage += $"+{DamageBonus}";
                else if (DamageBonus < 0)
                    damage += $"{DamageBonus}";

                if (!string.IsNullOrEmpty(DamageEffect))
                {
                    damage += $" plus {DamageEffect}";
                }

                return damage;
            }
        }

        public Attack()
        {
            Name = string.Empty;
            DamageRoll = string.Empty;
        }
    }
}
