using CreatureGen.Abilities;

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
                return BaseArmorClass + ArmorBonus + ShieldBonus + Dexterity.Bonus + DeflectionBonus + NaturalArmorBonus + SizeModifier;
            }
        }

        public int FlatFootedBonus
        {
            get
            {
                return TotalBonus - Dexterity.Bonus;
            }
        }

        public int TouchBonus
        {
            get
            {
                return TotalBonus - NaturalArmorBonus - ArmorBonus - ShieldBonus;
            }
        }
    }
}
