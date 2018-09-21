using CreatureGen.Selectors.Helpers;
using CreatureGen.Skills;
using NUnit.Framework;

namespace CreatureGen.Tests.Unit.Selectors.Helpers
{
    [TestFixture]
    public class TypeAndAmountHelperTests
    {
        [Test]
        public void BuildTypeAndAmountData()
        {
            var data = TypeAndAmountHelper.Build("type", "amount");
            Assert.That(data, Is.EqualTo("type@amount"));
        }

        [TestCase("skill", null)]
        [TestCase("skill", "")]
        [TestCase("skill", "focus")]
        public void BuildSkillTypeAndAmountData(string skill, string focus)
        {
            var skillString = SkillConstants.Build(skill, focus);

            var typeAndAmount = TypeAndAmountHelper.Build(skillString, "9266");
            Assert.That(typeAndAmount, Is.EqualTo(skillString + "@9266"));
        }

        [Test]
        public void ParseTypeAndAmountData()
        {
            var data = TypeAndAmountHelper.Parse("type@amount");
            Assert.That(data[0], Is.EqualTo("type"));
            Assert.That(data[1], Is.EqualTo("amount"));
            Assert.That(data.Length, Is.EqualTo(2));
        }

        [TestCase("skill", null)]
        [TestCase("skill", "")]
        [TestCase("skill", "focus")]
        public void ParseSkillTypeAndAmountData(string skill, string focus)
        {
            var skillString = SkillConstants.Build(skill, focus);

            var typeAndAmount = TypeAndAmountHelper.Parse(skillString + "@9266");
            Assert.That(typeAndAmount[0], Is.EqualTo(skillString));
            Assert.That(typeAndAmount[1], Is.EqualTo("9266"));
            Assert.That(typeAndAmount.Length, Is.EqualTo(2));
        }
    }
}
