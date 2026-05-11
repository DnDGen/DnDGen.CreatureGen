using DnDGen.CreatureGen.Abilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Defenses
{
    public class ArmorClass
    {
        public const int BaseArmorClass = 10;

        public Ability Dexterity { get; set; }
        public int SizeModifier { get; set; }
        public int MaxDexterityBonus { get; set; }

        public int DexterityBonus
        {
            get
            {
                if (Dexterity == null)
                    return 0;

                return Math.Min(MaxDexterityBonus, Dexterity.Modifier);
            }
        }

        public List<Bonus> ArmorBonuses { get; set; }
        public List<Bonus> ShieldBonuses { get; set; }
        public List<Bonus> DeflectionBonuses { get; set; }
        public List<Bonus> NaturalArmorBonuses { get; set; }
        public List<Bonus> DodgeBonuses { get; set; }

        public int ArmorBonus => ArmorBonuses.Where(b => !b.IsConditional).Select(b => b.Value).DefaultIfEmpty().Max();
        public int ShieldBonus => ShieldBonuses.Where(b => !b.IsConditional).Select(b => b.Value).DefaultIfEmpty().Max();
        public int DeflectionBonus => DeflectionBonuses.Where(b => !b.IsConditional).Select(b => b.Value).DefaultIfEmpty().Max();
        public int NaturalArmorBonus => NaturalArmorBonuses.Where(b => !b.IsConditional).Select(b => b.Value).DefaultIfEmpty().Max();
        public int DodgeBonus => DodgeBonuses.Where(b => !b.IsConditional).Sum(b => b.Value);

        public IEnumerable<Bonus> Bonuses => ArmorBonuses
            .Concat(ShieldBonuses)
            .Concat(DeflectionBonuses)
            .Concat(NaturalArmorBonuses)
            .Concat(DodgeBonuses);
        public bool IsConditional => Bonuses.Any(b => b.IsConditional);

        public int TotalBonus
        {
            get
            {
                var total = UnroundedTotal;
                return Math.Max(total, 1);
            }
        }

        private int UnroundedTotal
        {
            get
            {
                var total = BaseArmorClass;
                total += ArmorBonus;
                total += ShieldBonus;
                total += DeflectionBonus;
                total += NaturalArmorBonus;
                total += DodgeBonus;
                total += SizeModifier;
                total += DexterityBonus;

                return total;
            }
        }

        public int FlatFootedBonus
        {
            get
            {
                var total = UnroundedTotal;
                total -= DodgeBonus;
                total -= DexterityBonus;

                return Math.Max(total, 1);
            }
        }

        public int TouchBonus
        {
            get
            {
                var total = UnroundedTotal;
                total -= NaturalArmorBonus;
                total -= ArmorBonus;
                total -= ShieldBonus;

                return Math.Max(total, 1);
            }
        }

        public ArmorClass()
        {
            MaxDexterityBonus = int.MaxValue;
            ArmorBonuses = [];
            ShieldBonuses = [];
            DeflectionBonuses = [];
            NaturalArmorBonuses = [];
            DodgeBonuses = [];
        }

        public void AddBonus(string source, int value, string condition = "")
        {
            var bonus = new Bonus { Value = value, Condition = condition };
            var bonuses = GetBonuses(source);
            bonuses.Add(bonus);
        }

        private List<Bonus> GetBonuses(string source) => source switch
        {
            ArmorClassConstants.Armor => ArmorBonuses,
            ArmorClassConstants.Shield => ShieldBonuses,
            ArmorClassConstants.Deflection => DeflectionBonuses,
            ArmorClassConstants.Natural => NaturalArmorBonuses,
            ArmorClassConstants.Dodge => DodgeBonuses,
            _ => throw new ArgumentException("Invalid bonus source", nameof(source)),
        };

        public void RemoveAllBonuses(string source)
        {
            var bonuses = GetBonuses(source);
            bonuses.Clear();
        }
    }
}
