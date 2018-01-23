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
                roll = AppendBonus(roll, Constitution.Modifier * HitDiceQuantity);
                roll = AppendBonus(roll, Bonus);

                return roll;
            }
        }

        private string AppendBonus(string roll, int bonus)
        {
            if (bonus > 0)
                return $"{roll}+{bonus}";

            if (bonus < 0)
                return $"{roll}{bonus}";

            return roll;
        }

        public void Roll(Dice dice)
        {
            var rolls = dice.Roll(HitDiceQuantity).d(HitDie).AsIndividualRolls();
            rolls = rolls.Select(r => Math.Max(r + Constitution.Modifier, 1));

            Total = rolls.Sum() + Bonus;
        }

        public void RollDefault(Dice dice)
        {
            var averageTotal = dice.Roll(DefaultRoll).AsPotentialAverage();
            DefaultTotal = Convert.ToInt32(averageTotal);
        }
    }
}
