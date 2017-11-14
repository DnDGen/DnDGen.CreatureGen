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
        public int Bonus { get; set; }

        public string DefaultRoll
        {
            get
            {
                if (HitDiceQuantity == 0 || HitDie == 0)
                {
                    return Bonus.ToString();
                }

                var roll = $"{HitDiceQuantity}d{HitDie}";

                if (Constitution != null)
                {
                    var product = Constitution.Bonus * HitDiceQuantity;

                    if (product > 0)
                        roll = $"{roll}+{product}";
                    else if (product < 0)
                        roll = $"{roll}{product}";
                }

                if (Bonus > 0)
                    roll = $"{roll}+{Bonus}";
                else if (Bonus < 0)
                    roll = $"{roll}{Bonus}";

                return roll;
            }
        }

        public void Roll(Dice dice)
        {
            var rolls = dice.Roll(HitDiceQuantity).d(HitDie).AsIndividualRolls();

            if (Constitution != null)
                rolls = rolls.Select(r => Math.Max(r + Constitution.Bonus, 1));

            Total = rolls.Sum() + Bonus;
        }

        public void RollDefault(Dice dice)
        {
            var averageTotal = dice.Roll(DefaultRoll).AsPotentialAverage();
            DefaultTotal = Convert.ToInt32(averageTotal);
        }
    }
}
