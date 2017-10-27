using CreatureGen.Domain.Selectors.Collections;
using CreatureGen.Domain.Selectors.Selections;
using EventGen;
using Moq;
using NUnit.Framework;

namespace CreatureGen.Tests.Unit.Selectors.Collections
{
    [TestFixture]
    public class FeatsSelectorEventGenDecoratorTests
    {
        private IFeatsSelector decorator;
        private Mock<IFeatsSelector> mockInnerSelector;
        private Mock<GenEventQueue> mockEventQueue;

        [SetUp]
        public void Setup()
        {
            mockInnerSelector = new Mock<IFeatsSelector>();
            mockEventQueue = new Mock<GenEventQueue>();
            decorator = new FeatsSelectorEventGenDecorator(mockInnerSelector.Object, mockEventQueue.Object);
        }

        [Test]
        public void ReturnInnerAdditionalFeats()
        {
            var featSelections = new[]
            {
                new AdditionalFeatSelection(),
                new AdditionalFeatSelection(),
            };

            mockInnerSelector.Setup(s => s.SelectAdditional()).Returns(featSelections);

            var generatedFeatSelections = decorator.SelectAdditional();
            Assert.That(generatedFeatSelections, Is.EqualTo(featSelections));
        }

        [Test]
        public void LogEventsForAdditionalFeatsGeneration()
        {
            var featSelections = new[]
            {
                new AdditionalFeatSelection(),
                new AdditionalFeatSelection(),
            };

            mockInnerSelector.Setup(s => s.SelectAdditional()).Returns(featSelections);

            var generatedFeatSelections = decorator.SelectAdditional();
            Assert.That(generatedFeatSelections, Is.EqualTo(featSelections));
            mockEventQueue.Verify(q => q.Enqueue(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(2));
            mockEventQueue.Verify(q => q.Enqueue("CharacterGen", $"Selecting additional feat data"), Times.Once);
            mockEventQueue.Verify(q => q.Enqueue("CharacterGen", $"Selected additional feat data"), Times.Once);
        }

        [Test]
        public void ReturnInnerClassFeats()
        {
            var featSelections = new[]
            {
                new CharacterClassFeatSelection(),
                new CharacterClassFeatSelection(),
            };

            mockInnerSelector.Setup(s => s.SelectClass("class name")).Returns(featSelections);

            var generatedFeatSelections = decorator.SelectClass("class name");
            Assert.That(generatedFeatSelections, Is.EqualTo(featSelections));
        }

        [Test]
        public void LogEventsForClassFeatsGeneration()
        {
            var featSelections = new[]
            {
                new CharacterClassFeatSelection(),
                new CharacterClassFeatSelection(),
            };

            mockInnerSelector.Setup(s => s.SelectClass("class name")).Returns(featSelections);

            var generatedFeatSelections = decorator.SelectClass("class name");
            Assert.That(generatedFeatSelections, Is.EqualTo(featSelections));
            mockEventQueue.Verify(q => q.Enqueue(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(2));
            mockEventQueue.Verify(q => q.Enqueue("CharacterGen", $"Selecting class feat data for class name"), Times.Once);
            mockEventQueue.Verify(q => q.Enqueue("CharacterGen", $"Selected class feat data for class name"), Times.Once);
        }

        [Test]
        public void ReturnInnerRacialFeats()
        {
            var featSelections = new[]
            {
                new RacialFeatSelection(),
                new RacialFeatSelection(),
            };

            mockInnerSelector.Setup(s => s.SelectRacial("race")).Returns(featSelections);

            var generatedFeatSelections = decorator.SelectRacial("race");
            Assert.That(generatedFeatSelections, Is.EqualTo(featSelections));
        }

        [Test]
        public void LogEventsForRacialFeatsGeneration()
        {
            var featSelections = new[]
            {
                new RacialFeatSelection(),
                new RacialFeatSelection(),
            };

            mockInnerSelector.Setup(s => s.SelectRacial("race")).Returns(featSelections);

            var generatedFeatSelections = decorator.SelectRacial("race");
            Assert.That(generatedFeatSelections, Is.EqualTo(featSelections));
            mockEventQueue.Verify(q => q.Enqueue(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(2));
            mockEventQueue.Verify(q => q.Enqueue("CharacterGen", $"Selecting racial feat data for race"), Times.Once);
            mockEventQueue.Verify(q => q.Enqueue("CharacterGen", $"Selected racial feat data for race"), Times.Once);
        }
    }
}
