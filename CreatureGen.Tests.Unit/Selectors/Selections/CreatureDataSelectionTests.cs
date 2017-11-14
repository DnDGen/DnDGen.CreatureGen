using CreatureGen.Selectors.Selections;
using NUnit.Framework;

namespace CreatureGen.Tests.Unit.Selectors.Selections
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
            Assert.That(selection.Reach, Is.EqualTo(0));
            Assert.That(selection.Size, Is.Empty);
            Assert.That(selection.Space, Is.EqualTo(0));
        }
    }
}
