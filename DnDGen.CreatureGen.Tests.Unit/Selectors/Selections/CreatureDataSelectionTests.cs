using DnDGen.CreatureGen.Selectors.Selections;
using NUnit.Framework;

namespace DnDGen.CreatureGen.Tests.Unit.Selectors.Selections
{
    [TestFixture]
    public class CreatureDataSelectionTests
    {
        private CreatureDataSelection selection;

        [SetUp]
        public void Setup()
        {
            selection = new CreatureDataSelection();
        }

        [Test]
        public void CreatureDataSelectionInitialized()
        {
            Assert.That(selection.ChallengeRating, Is.Empty);
            Assert.That(selection.NumberOfHands, Is.Zero);
            Assert.That(selection.NaturalArmor, Is.Zero);
            Assert.That(selection.Reach, Is.Zero);
            Assert.That(selection.Size, Is.Empty);
            Assert.That(selection.Space, Is.Zero);
        }
    }
}
