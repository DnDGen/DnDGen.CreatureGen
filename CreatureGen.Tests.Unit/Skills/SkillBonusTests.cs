using CreatureGen.Skills;
using NUnit.Framework;

namespace CreatureGen.Tests.Unit.Skills
{
    [TestFixture]
    public class SkillBonusTests
    {
        private SkillBonus skillBonus;

        [SetUp]
        public void Setup()
        {
            skillBonus = new SkillBonus();
        }

        [Test]
        public void SkillBonusInitialized()
        {
            Assert.That(skillBonus.Condition, Is.Empty);
            Assert.That(skillBonus.IsConditional, Is.False);
            Assert.That(skillBonus.Value, Is.Zero);
        }

        [TestCase(null, false)]
        [TestCase("", false)]
        [TestCase("x", true)]
        [TestCase("a condition", true)]
        public void IsConditional(string condition, bool isConditional)
        {
            skillBonus.Condition = condition;
            Assert.That(skillBonus.IsConditional, Is.EqualTo(isConditional));
        }
    }
}
