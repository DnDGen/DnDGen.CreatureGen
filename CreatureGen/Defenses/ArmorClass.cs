using CreatureGen.Abilities;
using System;

namespace CreatureGen.Defenses
{
    public class ArmorClass
    {
        public const int BaseArmorClass = 10;

        public Ability Dexterity { get; set; }
        public int ArmorBonus { get; set; }
        public int ShieldBonus { get; set; }
        public int DeflectionBonus { get; set; }
        public int NaturalArmorBonus { get; set; }
        public int SizeModifier { get; set; }
        public bool CircumstantialBonus { get; set; }

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
                total += SizeModifier;

                if (Dexterity != null)
                    total += Dexterity.Modifier;

                return total;
            }
        }

        public int FlatFootedBonus
        {
            get
            {
                var total = UnroundedTotal;

                if (Dexterity != null)
                    total -= Dexterity.Modifier;

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
    }
}
