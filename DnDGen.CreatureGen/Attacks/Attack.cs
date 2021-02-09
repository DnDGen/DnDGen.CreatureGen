using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Feats;
using DnDGen.TreasureGen.Items;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace DnDGen.CreatureGen.Attacks
{
    public class Attack
    {
        public string Name { get; set; }
        public int BaseAttackBonus { get; set; }
        public int SizeModifier { get; set; }
        public Ability BaseAbility { get; set; }
        public bool IsMelee { get; set; }
        public List<Damage> Damages { get; set; }
        public string DamageEffect { get; set; }
        public int DamageBonus { get; set; }
        public bool IsPrimary { get; set; }
        public bool IsSpecial { get; set; }
        public bool IsNatural { get; set; }
        public Frequency Frequency { get; set; }
        public SaveDieCheck Save { get; set; }
        public string AttackType { get; set; }
        public List<int> AttackBonuses { get; set; }
        public int MaxNumberOfAttacks { get; set; }

        public int TotalAttackBonus
        {
            get
            {
                var abilityBonus = 0;
                if (BaseAbility != null)
                    abilityBonus = BaseAbility.Modifier;

                return BaseAttackBonus + abilityBonus + SizeModifier + AttackBonuses.Sum();
            }
        }

        public int[] FullAttackBonuses
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
                while (BaseAttackBonus - decrement > 0 && bonuses.Count < MaxNumberOfAttacks);

                return bonuses.ToArray();
            }
        }

        public string DamageDescription
        {
            get
            {
                if (!Damages.Any())
                    return DamageEffect;

                var damage = string.Join(" + ", Damages.Select(d => d.Description));
                if (DamageBonus > 0)
                {
                    var regex = new Regex(Regex.Escape(Damages[0].Roll));
                    damage = regex.Replace(damage, $"{Damages[0].Roll}+{DamageBonus}", 1);
                }
                else if (DamageBonus < 0)
                {
                    var regex = new Regex(Regex.Escape(Damages[0].Roll));
                    damage = regex.Replace(damage, $"{Damages[0].Roll}{DamageBonus}", 1);
                }

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
            Damages = new List<Damage>();
            DamageEffect = string.Empty;
            AttackBonuses = new List<int>();
            MaxNumberOfAttacks = 1;
            Frequency = new Frequency();
            AttackType = string.Empty;
        }
    }
}
