using NUnit.Framework;
using System.Linq;

namespace CreatureGen.Tests.Integration.Tables
{
    public static class RollHelper
    {
        private static int[] StandardDice = new[] { 2, 3, 4, 6, 8, 10, 12, 20, 100 };

        public static string GetRoll(int baseHitDiceQuantity, int advancedLowerRange, int advancedUpperRange)
        {
            var lower = advancedLowerRange - baseHitDiceQuantity;
            var upper = advancedUpperRange - baseHitDiceQuantity;

            return GetRoll(lower, upper);
        }

        public static string GetRoll(int lower, int upper)
        {
            if (lower == upper)
                return lower.ToString();

            var range = upper - lower;

            var possibleDieRolls = Enumerable.Range(1, range)
                .Where(f => range % f == 0) //Get factors
                .ToDictionary(f => f, f => range / f + 1) //pair quantities with die
                .Where(r => StandardDice.Contains(r.Value)) //filter out non-standard die
                .ToDictionary(r => r.Key, r => r.Value);

            var singleRolls = possibleDieRolls.Where(r => r.Key <= r.Value).ToDictionary(r => r.Key, r => r.Value);

            if (singleRolls.Any())
            {
                var quantity = possibleDieRolls.Min(r => r.Key);
                var die = possibleDieRolls[quantity];
                var adjustment = lower - quantity;

                Assert.That(quantity + adjustment, Is.EqualTo(lower));
                Assert.That(quantity * die + adjustment, Is.EqualTo(upper));

                if (adjustment == 0)
                    return $"{quantity}d{die}";

                if (adjustment > 0)
                    return $"{quantity}d{die}+{adjustment}";

                return $"{quantity}d{die}{adjustment}";
            }

            var largestDie = StandardDice.Where(d => d <= range).Max();
            var largestQuantity = range / largestDie;
            var newLower = lower - largestQuantity;
            var newUpper = upper - largestDie * largestQuantity;

            var additionalRoll = GetRoll(newLower, newUpper);

            return $"{largestQuantity}d{largestDie}+{additionalRoll}";
        }
    }
}
