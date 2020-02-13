using DnDGen.CreatureGen.Abilities;
using DnDGen.RollGen;
using System;
using System.Linq;

namespace DnDGen.CreatureGen.Defenses
{
    public class HitPoints
    {
        public int HitDie { get; set; }
        public double HitDiceQuantity { get; set; }
        public Ability Constitution { get; set; }
        public int Total { get; set; }
        public int DefaultTotal { get; set; }
        public int Bonus { get; set; }

        public int RoundedHitDiceQuantity
        {
            get
            {
                if (HitDiceQuantity == 0)
                    return 0;

                if (HitDiceQuantity < 1)
                    return 1;

                var rawRoundedHitDiceQuantity = Math.Floor(HitDiceQuantity);
                var roundedHitDiceQuantity = Convert.ToInt32(rawRoundedHitDiceQuantity);

                return roundedHitDiceQuantity;
            }
        }

        public string DefaultRoll
        {
            get
            {
                if (HitDiceQuantity == 0 || HitDie == 0)
                {
                    return Bonus.ToString();
                }

                var roll = $"{RoundedHitDiceQuantity}d{HitDie}";

                if (HitDiceQuantity < 1)
                {
                    roll += $"/{divisor}";
                }

                roll = AppendBonus(roll, totalConstitutionBonus);
                roll = AppendBonus(roll, Bonus);

                return roll;
            }
        }

        private int divisor
        {
            get
            {
                var rawDenominator = 1 / HitDiceQuantity;
                var divisor = Convert.ToInt32(rawDenominator);

                return divisor;
            }
        }

        private int totalConstitutionBonus
        {
            get
            {
                return Constitution.Modifier * RoundedHitDiceQuantity;
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
            var rolls = dice.Roll(RoundedHitDiceQuantity).d(HitDie).AsIndividualRolls();

            if (HitDiceQuantity < 1)
                rolls = rolls.Select(r => r / divisor);

            rolls = rolls.Select(r => Math.Max(r + Constitution.Modifier, 1));

            Total = rolls.Sum() + Bonus;
        }

        public void RollDefault(Dice dice)
        {
            var averageTotal = dice.Roll(DefaultRoll).AsPotentialAverage();
            var floor = Math.Floor(averageTotal);
            DefaultTotal = Convert.ToInt32(floor);
        }
    }
}
