using CreatureGen.Alignments;
using CreatureGen.Generators.Alignments;
using EventGen;
using Moq;
using NUnit.Framework;

namespace CreatureGen.Tests.Unit.Generators.Alignments
{
    [TestFixture]
    public class AlignmentGeneratorEventDecoratorTests
    {
        private IAlignmentGenerator decorator;
        private Mock<IAlignmentGenerator> mockInnerGenerator;
        private Mock<GenEventQueue> mockEventQueue;

        [SetUp]
        public void Setup()
        {
            mockInnerGenerator = new Mock<IAlignmentGenerator>();
            mockEventQueue = new Mock<GenEventQueue>();
            decorator = new AlignmentGeneratorEventDecorator(mockInnerGenerator.Object, mockEventQueue.Object);
        }

        [Test]
        public void ReturnInnerAlignment()
        {
            var alignment = new Alignment();
            alignment.Goodness = "goodness";
            alignment.Lawfulness = "lawfulness";

            mockInnerGenerator.Setup(g => g.Generate("creature name")).Returns(alignment);

            var generatedAlignment = decorator.Generate("creature name");
            Assert.That(generatedAlignment, Is.EqualTo(alignment));
        }

        [Test]
        public void LogEventsForAlignmentGeneration()
        {
            var alignment = new Alignment();
            alignment.Goodness = "goodness";
            alignment.Lawfulness = "lawfulness";

            mockInnerGenerator.Setup(g => g.Generate("creature name")).Returns(alignment);

            var generatedAlignment = decorator.Generate("creature name");
            Assert.That(generatedAlignment, Is.EqualTo(alignment));
            mockEventQueue.Verify(q => q.Enqueue(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(2));
            mockEventQueue.Verify(q => q.Enqueue("CreatureGen", $"Generating alignment for creature name"), Times.Once);
            mockEventQueue.Verify(q => q.Enqueue("CreatureGen", $"Generated lawfulness goodness"), Times.Once);
        }

        [Test]
        public void LogEventsForNoAlignmentGeneration()
        {
            var alignment = new Alignment();
            mockInnerGenerator.Setup(g => g.Generate("creature name")).Returns(alignment);

            var generatedAlignment = decorator.Generate("creature name");
            Assert.That(generatedAlignment, Is.EqualTo(alignment));
            mockEventQueue.Verify(q => q.Enqueue(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(2));
            mockEventQueue.Verify(q => q.Enqueue("CreatureGen", $"Generating alignment for creature name"), Times.Once);
            mockEventQueue.Verify(q => q.Enqueue("CreatureGen", $"Generated no alignment"), Times.Once);
        }
    }
}
