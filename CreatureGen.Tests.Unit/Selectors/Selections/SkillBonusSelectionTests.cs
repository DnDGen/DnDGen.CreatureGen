using CreatureGen.Selectors.Selections;
using NUnit.Framework;

namespace CreatureGen.Tests.Unit.Selectors.Selections
{
    [TestFixture]
    public class SkillBonusSelectionTests
    {
        private SkillBonusSelection selection;

        [SetUp]
        public void Setup()
        {
            selection = new SkillBonusSelection();
        }

        [Test]
        public void SkillBonusSelectionInitialized()
        {
            Assert.That(selection.Bonus, Is.Zero);
            Assert.That(selection.Condition, Is.Empty);
            Assert.That(selection.Skill, Is.Empty);
        }
    }
}
