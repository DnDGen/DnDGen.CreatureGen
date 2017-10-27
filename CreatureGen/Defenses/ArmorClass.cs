using CreatureGen.Abilities;

namespace CreatureGen.Defenses
{
    public class ArmorClass
    {
        public Ability Dexterity { get; set; }
        public int DeflectionBonus { get; set; }
        public int NaturalArmorBonus { get; set; }

        public int TotalBonus
        {
            get
            {
                return 10 + Dexterity.Bonus + DeflectionBonus + NaturalArmorBonus;
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
                return TotalBonus - NaturalArmorBonus;
            }
        }
    }
}
