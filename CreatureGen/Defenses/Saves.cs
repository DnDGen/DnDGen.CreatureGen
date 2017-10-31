using CreatureGen.Abilities;

namespace CreatureGen.Defenses
{
    public class Saves
    {
        public Ability Constitution { get; set; }
        public Ability Dexterity { get; set; }
        public Ability Wisdom { get; set; }
        public int RacialFortitudeBonus { get; set; }
        public int RacialReflexBonus { get; set; }
        public int RacialWillBonus { get; set; }
        public int FeatFortitudeBonus { get; set; }
        public int FeatReflexBonus { get; set; }
        public int FeatWillBonus { get; set; }

        public int Fortitude
        {
            get
            {
                return ComputeTotal(Constitution, RacialFortitudeBonus, FeatFortitudeBonus);
            }
        }

        public int Reflex
        {
            get
            {
                return ComputeTotal(Dexterity, RacialReflexBonus, FeatReflexBonus);
            }
        }

        private int ComputeTotal(Ability ability, int racialBonus, int featBonus)
        {
            var bonus = 0;
            bonus += racialBonus;
            bonus += featBonus;

            if (ability != null)
                bonus += ability.Bonus;

            return bonus;
        }

        public int Will
        {
            get
            {
                return ComputeTotal(Wisdom, RacialWillBonus, FeatWillBonus);
            }
        }
    }
}
