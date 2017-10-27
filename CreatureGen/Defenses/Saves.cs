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

        public int Fortitude
        {
            get
            {
                var bonus = RacialFortitudeBonus;
                if (Constitution != null)
                    bonus += Constitution.Bonus;

                return bonus;
            }
        }

        public int Reflex
        {
            get
            {
                var bonus = RacialReflexBonus;
                if (Dexterity != null)
                    bonus += Dexterity.Bonus;

                return bonus;
            }
        }

        public int Will
        {
            get
            {
                var bonus = RacialWillBonus;
                if (Wisdom != null)
                    bonus += Wisdom.Bonus;

                return bonus;
            }
        }
    }
}
