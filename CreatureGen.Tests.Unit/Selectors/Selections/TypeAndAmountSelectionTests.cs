using CreatureGen.Selectors.Selections;
using NUnit.Framework;

namespace CreatureGen.Tests.Unit.Selectors.Selections
{
    [TestFixture]
    public class TypeAndAmountSelectionTests
    {
        private TypeAndAmountSelection selection;

        [SetUp]
        public void Setup()
        {
            selection = new TypeAndAmountSelection();
        }

        [Test]
        public void TypeAndAmountSelectionInitialized()
        {
            Assert.That(selection.Amount, Is.EqualTo(0));
            Assert.That(selection.Type, Is.Empty);
        }

        [Test]
        public void TypeAndAmountDivider()
        {
            Assert.That(TypeAndAmountSelection.Divider, Is.EqualTo('@'));
        }
    }
}
