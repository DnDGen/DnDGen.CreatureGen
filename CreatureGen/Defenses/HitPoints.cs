using CreatureGen.Abilities;
using RollGen;
using System;
using System.Linq;

namespace CreatureGen.Defenses
{
    public class HitPoints
    {
        public int HitDie { get; set; }
        public int HitDiceQuantity { get; set; }
        public Ability Constitution { get; set; }
        public int Total { get; set; }
        public int DefaultTotal { get; set; }

        public string DefaultRoll
        {
            get
            {
                return $"{HitDiceQuantity}d{HitDie}+{Constitution.Bonus * HitDiceQuantity}";
            }
        }

        public void Roll(Dice dice)
        {
            var rolls = dice.Roll(HitDiceQuantity).d(HitDie).AsIndividualRolls();
            rolls = rolls.Select(r => Math.Max(r + Constitution.Bonus, 1));

            Total = rolls.Sum();
        }

        public void RollDefault(Dice dice)
        {
            var averageTotal = dice.Roll(DefaultRoll).AsPotentialAverage();
            DefaultTotal = Convert.ToInt32(averageTotal);
        }
    }
}
