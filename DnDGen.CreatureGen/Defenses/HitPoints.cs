using DnDGen.CreatureGen.Abilities;
using DnDGen.RollGen;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Defenses
{
    public class HitPoints
    {
        public List<HitDice> HitDice { get; set; }
        public Ability Constitution { get; set; }
        public int Total { get; set; }
        public int DefaultTotal { get; set; }
        public int Bonus { get; set; }

        public double HitDiceQuantity => HitDice.Sum(hd => hd.Quantity);
        public int RoundedHitDiceQuantity => HitDice.Sum(hd => hd.RoundedQuantity);

        public string DefaultRoll
        {
            get
            {
                var rolls = HitDice.Select(hd => hd.DefaultRoll).Where(hd => hd != 0.ToString());
                if (!rolls.Any())
                {
                    return Bonus.ToString();
                }

                var roll = string.Join("+", rolls);
                roll = AppendBonus(roll, totalConstitutionBonus);
                roll = AppendBonus(roll, Bonus);

                return roll;
            }
        }

        private int totalConstitutionBonus => Constitution.Modifier * RoundedHitDiceQuantity;

        public IEnumerable<(string Condition, int Bonus)> ConditionalBonuses
        {
            get
            {
                var conditionalBonuses = Constitution.Bonuses.Where(b => b.IsConditional);

                if (!conditionalBonuses.Any())
                    return [];

                return conditionalBonuses.Select(b => (b.Condition, b.Value / 2 * RoundedHitDiceQuantity));
            }
        }

        public HitPoints()
        {
            HitDice = new List<HitDice>();
        }

        private string AppendBonus(string roll, int bonus)
        {
            if (bonus > 0)
                return $"{roll}+{bonus}";

            if (bonus < 0)
                return $"{roll}{bonus}";

            return roll;
        }

        public void RollTotal(Dice dice)
        {
            Total = 0;

            foreach (var hitDice in HitDice)
            {
                var rolls = dice.Roll(hitDice.RoundedQuantity).d(hitDice.HitDie).AsIndividualRolls();

                rolls = rolls.Select(r => Math.Max(1, r / hitDice.Divisor));
                rolls = rolls.Select(r => Math.Max(r + Constitution.Modifier, 1));

                Total += rolls.Sum();
            }

            Total += Bonus;
        }

        public void RollDefaultTotal(Dice dice)
        {
            DefaultTotal = 0;

            foreach (var hitDice in HitDice)
            {
                var average = dice.Roll(hitDice.RoundedQuantity).d(hitDice.HitDie).AsPotentialAverage();
                average /= hitDice.Divisor;
                average = Math.Max(hitDice.RoundedQuantity, average);
                average += Constitution.Modifier * hitDice.RoundedQuantity;
                average = Math.Max(hitDice.RoundedQuantity, average);

                DefaultTotal += Convert.ToInt32(Math.Floor(average));
            }

            DefaultTotal += Bonus;
        }
    }
}
