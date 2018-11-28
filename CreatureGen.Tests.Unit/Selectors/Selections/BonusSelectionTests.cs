using CreatureGen.Selectors.Selections;
using NUnit.Framework;

namespace CreatureGen.Tests.Unit.Selectors.Selections
{
    [TestFixture]
    public class BonusSelectionTests
    {
        private BonusSelection selection;

        [SetUp]
        public void Setup()
        {
            selection = new BonusSelection();
        }

        [Test]
        public void BonusSelectionInitialized()
        {
            Assert.That(selection.Bonus, Is.Zero);
            Assert.That(selection.Condition, Is.Empty);
            Assert.That(selection.Target, Is.Empty);
        }

        [Test]
        public void BonusDivider()
        {
            Assert.That(BonusSelection.Divider, Is.EqualTo('$'));
        }
    }
}
