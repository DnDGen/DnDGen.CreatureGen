using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables
{
    [TestFixture]
    public class RollHelperTests
    {
        [TestCase(1, 1, "1")]
        [TestCase(1, 2, "1d2")]
        [TestCase(1, 3, "1d3")]
        [TestCase(1, 5, "2d3-1")]
        [TestCase(1, 9, "1d8+1d2-1")]
        [TestCase(2, 4, "1d3+1")]
        [TestCase(2, 10, "1d8+1d2")]
        [TestCase(4, 12, "1d8+1d2+2")]
        [TestCase(5, 18, "1d12+1d3+3")]
        [TestCase(7, 30, "1d20+2d3+4")]
        [TestCase(7, 50, "2d20+1d6+4")]
        public void Roll(int lower, int upper, string expectedRoll)
        {
            var roll = RollHelper.GetRoll(lower, upper);
            Assert.That(roll, Is.EqualTo(expectedRoll));
        }

        [TestCase(1, 2, 2, "1")]
        [TestCase(6, 7, 9, "1d3")]
        [TestCase(6, 8, 10, "1d3+1")]
        [TestCase(6, 10, 18, "1d8+1d2+2")]
        [TestCase(9, 14, 27, "1d12+1d3+3")]
        [TestCase(9, 15, 27, "4d4+2")]
        [TestCase(12, 19, 36, "1d12+2d4+4")]
        [TestCase(15, 22, 45, "1d20+2d3+4")]
        public void Roll(int baseAmount, int lower, int upper, string expectedRoll)
        {
            var roll = RollHelper.GetRoll(baseAmount, lower, upper);
            Assert.That(roll, Is.EqualTo(expectedRoll));
        }
    }
}
