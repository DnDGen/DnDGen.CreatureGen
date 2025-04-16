using DnDGen.CreatureGen.Selectors.Selections;
using NUnit.Framework;

namespace DnDGen.CreatureGen.Tests.Unit.Selectors.Selections
{
    [TestFixture]
    public class TypeAndAmountDataSelectionTests
    {
        private TypeAndAmountDataSelection selection;

        [SetUp]
        public void Setup()
        {
            selection = new TypeAndAmountDataSelection();
        }

        [Test]
        public void TypeAndAmountDataSelectionInitialized()
        {
            Assert.That(selection.Amount, Is.Zero);
            Assert.That(selection.Type, Is.Empty);
            Assert.That(selection.RawAmount, Is.Empty);
        }

        [Test]
        public void TypeAndAmountDivider()
        {
            Assert.That(TypeAndAmountDataSelection.Divider, Is.EqualTo('@'));
        }
    }
}
