using CreatureGen.Abilities;

namespace CreatureGen.Defenses
{
    public class Saves
    {
        public Ability FortitudeAbility { get; set; }
        public Ability ReflexAbility { get; set; }
        public Ability WillAbility { get; set; }
        public int RacialFortitudeBonus { get; set; }
        public int RacialReflexBonus { get; set; }
        public int RacialWillBonus { get; set; }
        public int FeatFortitudeBonus { get; set; }
        public int FeatReflexBonus { get; set; }
        public int FeatWillBonus { get; set; }
        public bool CircumstantialBonus { get; set; }

        public int Fortitude
        {
            get
            {
                return ComputeTotal(FortitudeAbility, RacialFortitudeBonus, FeatFortitudeBonus);
            }
        }

        public int Reflex
        {
            get
            {
                return ComputeTotal(ReflexAbility, RacialReflexBonus, FeatReflexBonus);
            }
        }

        private int ComputeTotal(Ability ability, int racialBonus, int featBonus)
        {
            var bonus = 0;
            bonus += racialBonus;
            bonus += featBonus;

            if (ability != null)
                bonus += ability.Modifier;

            return bonus;
        }

        public int Will
        {
            get
            {
                return ComputeTotal(WillAbility, RacialWillBonus, FeatWillBonus);
            }
        }
    }
}
