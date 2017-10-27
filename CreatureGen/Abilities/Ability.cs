namespace CreatureGen.Abilities
{
    public class Ability
    {
        public string Name { get; private set; }
        public int BaseValue { get; set; }
        public int RacialAdjustment { get; set; }
        public int FullValue
        {
            get
            {
                return BaseValue + RacialAdjustment;
            }
        }

        public int Bonus
        {
            get
            {
                var even = FullValue - FullValue % 2;
                return (even - 10) / 2;
            }
        }

        public Ability(string name)
        {
            Name = name;
            BaseValue = 10;
        }
    }
}