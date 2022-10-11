using DnDGen.CreatureGen.Selectors.Selections;
using NUnit.Framework;

namespace DnDGen.CreatureGen.Tests.Unit.Selectors.Selections
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
            Assert.That(selection.Amount, Is.Zero);
            Assert.That(selection.Type, Is.Empty);
            Assert.That(selection.RawAmount, Is.Empty);
        }

        [Test]
        public void TypeAndAmountDivider()
        {
            Assert.That(TypeAndAmountSelection.Divider, Is.EqualTo('@'));
        }
    }
}
