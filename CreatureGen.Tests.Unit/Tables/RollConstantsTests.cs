using CreatureGen.Tables;
using NUnit.Framework;
using System.Linq;

namespace CreatureGen.Tests.Unit.Tables
{
    [TestFixture]
    public class RollConstantsTests
    {
        [TestCase(RollConstants.Roll1, 1, 1)]
        [TestCase(RollConstants.Roll1To2, 1, 2)]
        [TestCase(RollConstants.Roll1To3, 1, 3)]
        [TestCase(RollConstants.Roll1To4, 1, 4)]
        [TestCase(RollConstants.Roll1To5, 1, 5)]
        [TestCase(RollConstants.Roll1To6, 1, 6)]
        [TestCase(RollConstants.Roll1To7, 1, 7)]
        [TestCase(RollConstants.Roll1To8, 1, 8)]
        [TestCase(RollConstants.Roll1To9, 1, 9)]
        [TestCase(RollConstants.Roll1To24, 1, 24)]
        [TestCase(RollConstants.Roll4To6, 4, 6)]
        [TestCase(RollConstants.Roll5To12, 5, 12)]
        [TestCase(RollConstants.Roll5To16, 5, 16)]
        [TestCase(RollConstants.Roll6To18, 6, 18)]
        [TestCase(RollConstants.Roll6To20, 6, 20)]
        [TestCase(RollConstants.Roll7To12, 7, 12)]
        [TestCase(RollConstants.Roll7To24, 7, 24)]
        [TestCase(RollConstants.Roll9To16, 9, 16)]
        public void AmountConstant(string constant, int lower, int upper)
        {
            var bestRoll = GetBestRollFor(lower, upper);
            Assert.That(constant, Is.EqualTo(bestRoll));
        }

        private string GetBestRollFor(int lower, int upper)
        {
            if (lower == upper)
                return lower.ToString();

            var standardDie = new[] { 2, 3, 4, 6, 8, 10, 12, 20, 100 };
            var range = upper - lower;

            var possibleDieRolls = Enumerable.Range(1, range)
                .Where(f => range % f == 0) //Get factors
                .ToDictionary(f => f, f => range / f + 1) //pair quantities with die
                .Where(r => standardDie.Contains(r.Value)) //filter out non-standard die
                .ToDictionary(r => r.Key, r => r.Value);

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
    }
}
