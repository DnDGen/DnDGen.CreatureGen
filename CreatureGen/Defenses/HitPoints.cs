using CreatureGen.Abilities;

namespace CreatureGen.Defenses
{
    public class HitPoints
    {
        public int HitDie { get; set; }
        public int HitDiceQuantity { get; set; }
        public Ability Constitution { get; set; }
        public int Total { get; set; }
        public int DefaultTotal { get; set; }

        public string Roll
        {
            get
            {
                return $"{HitDiceQuantity}d{HitDie}+{Constitution.Bonus * HitDiceQuantity}";
            }
        }
    }
}
