using CreatureGen.Selectors.Selections;
using NUnit.Framework;

namespace CreatureGen.Tests.Unit.Selectors.Selections
{
    [TestFixture]
    public class AttackSelectionTests
    {
        private AttackSelection selection;

        [SetUp]
        public void Setup()
        {
            selection = new AttackSelection();
        }

        [Test]
        public void AttackSelectionIsInitialized()
        {
            Assert.That(selection.Damage, Is.Empty);
            Assert.That(selection.IsMelee, Is.False);
            Assert.That(selection.IsPrimary, Is.False);
            Assert.That(selection.Name, Is.Empty);
        }
    }
}
