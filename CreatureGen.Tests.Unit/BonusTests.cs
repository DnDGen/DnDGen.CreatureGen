using NUnit.Framework;

namespace CreatureGen.Tests.Unit
{
    [TestFixture]
    public class BonusTests
    {
        private Bonus bonus;

        [SetUp]
        public void Setup()
        {
            bonus = new Bonus();
        }

        [Test]
        public void BonusInitialized()
        {
            Assert.That(bonus.Condition, Is.Empty);
            Assert.That(bonus.IsConditional, Is.False);
            Assert.That(bonus.Value, Is.Zero);
        }

        [TestCase(null, false)]
        [TestCase("", false)]
        [TestCase("x", true)]
        [TestCase("a condition", true)]
        public void IsConditional(string condition, bool isConditional)
        {
            bonus.Condition = condition;
            Assert.That(bonus.IsConditional, Is.EqualTo(isConditional));
        }
    }
}
