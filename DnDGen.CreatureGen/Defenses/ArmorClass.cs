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

        private readonly Dictionary<string, IEnumerable<Bonus>> sourcesAndBonuses;

        public IEnumerable<Bonus> ArmorBonuses => sourcesAndBonuses[ArmorClassConstants.Armor];
        public IEnumerable<Bonus> ShieldBonuses => sourcesAndBonuses[ArmorClassConstants.Shield];
        public IEnumerable<Bonus> DeflectionBonuses => sourcesAndBonuses[ArmorClassConstants.Deflection];
        public IEnumerable<Bonus> NaturalArmorBonuses => sourcesAndBonuses[ArmorClassConstants.Natural];
        public IEnumerable<Bonus> DodgeBonuses => sourcesAndBonuses[ArmorClassConstants.Dodge];

        public int ArmorBonus => ArmorBonuses.Where(b => !b.IsConditional).Select(b => b.Value).DefaultIfEmpty().Max();
        public int ShieldBonus => ShieldBonuses.Where(b => !b.IsConditional).Select(b => b.Value).DefaultIfEmpty().Max();
        public int DeflectionBonus => DeflectionBonuses.Where(b => !b.IsConditional).Select(b => b.Value).DefaultIfEmpty().Max();
        public int NaturalArmorBonus => NaturalArmorBonuses.Where(b => !b.IsConditional).Select(b => b.Value).DefaultIfEmpty().Max();
        public int DodgeBonus => DodgeBonuses.Where(b => !b.IsConditional).Sum(b => b.Value);

        public IEnumerable<Bonus> Bonuses => sourcesAndBonuses.SelectMany(kvp => kvp.Value);
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

            sourcesAndBonuses = new Dictionary<string, IEnumerable<Bonus>>();
            sourcesAndBonuses[ArmorClassConstants.Armor] = Enumerable.Empty<Bonus>();
            sourcesAndBonuses[ArmorClassConstants.Shield] = Enumerable.Empty<Bonus>();
            sourcesAndBonuses[ArmorClassConstants.Dodge] = Enumerable.Empty<Bonus>();
            sourcesAndBonuses[ArmorClassConstants.Deflection] = Enumerable.Empty<Bonus>();
            sourcesAndBonuses[ArmorClassConstants.Natural] = Enumerable.Empty<Bonus>();
        }

        public void AddBonus(string source, int value, string condition = "")
        {
            var bonus = new Bonus { Value = value, Condition = condition };
            sourcesAndBonuses[source] = sourcesAndBonuses[source].Union(new[] { bonus });
        }

        public void RemoveBonus(string source)
        {
            if (sourcesAndBonuses.ContainsKey(source))
            {
                sourcesAndBonuses[source] = Enumerable.Empty<Bonus>();
            }
        }
    }
}
