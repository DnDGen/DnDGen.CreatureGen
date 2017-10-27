using CreatureGen.Alignments;
using CreatureGen.Domain.Generators.Alignments;
using CreatureGen.Randomizers.Alignments;
using EventGen;
using Moq;
using NUnit.Framework;

namespace CreatureGen.Tests.Unit.Generators.Alignments
{
    [TestFixture]
    public class AlignmentGeneratorEventGenDecoratorTests
    {
        private IAlignmentGenerator decorator;
        private Mock<IAlignmentGenerator> mockInnerGenerator;
        private Mock<GenEventQueue> mockEventQueue;
        private Mock<IAlignmentRandomizer> mockAlignmentRandomizer;

        [SetUp]
        public void Setup()
        {
            mockInnerGenerator = new Mock<IAlignmentGenerator>();
            mockEventQueue = new Mock<GenEventQueue>();
            decorator = new AlignmentGeneratorEventGenDecorator(mockInnerGenerator.Object, mockEventQueue.Object);

            mockAlignmentRandomizer = new Mock<IAlignmentRandomizer>();
        }

        [Test]
        public void ReturnInnerAlignment()
        {
            var alignment = new Alignment();
            var prototype = new Alignment();
            mockInnerGenerator.Setup(g => g.GenerateWith(prototype)).Returns(alignment);

            var generatedAlignment = decorator.GenerateWith(prototype);
            Assert.That(generatedAlignment, Is.EqualTo(alignment));
        }

        [Test]
        public void LogEventsForAlignmentGeneration()
        {
            var alignment = new Alignment();
            alignment.Goodness = "goodness";
            alignment.Lawfulness = "lawfulness";

            var prototype = new Alignment();
            prototype.Goodness = "prototype goodness";
            prototype.Goodness = "prototype lawfulness";
            mockInnerGenerator.Setup(g => g.GenerateWith(prototype)).Returns(alignment);

            var generatedAlignment = decorator.GenerateWith(prototype);
            Assert.That(generatedAlignment, Is.EqualTo(alignment));
            mockEventQueue.Verify(q => q.Enqueue(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(2));
            mockEventQueue.Verify(q => q.Enqueue("CharacterGen", $"Generating alignment from prototype {prototype.Full}"), Times.Once);
            mockEventQueue.Verify(q => q.Enqueue("CharacterGen", $"Generated lawfulness goodness"), Times.Once);
        }

        [Test]
        public void ReturnInnerPrototype()
        {
            var prototype = new Alignment();
            prototype.Goodness = "prototype goodness";
            prototype.Goodness = "prototype lawfulness";
            mockInnerGenerator.Setup(g => g.GeneratePrototype(mockAlignmentRandomizer.Object)).Returns(prototype);

            var generatedPrototype = decorator.GeneratePrototype(mockAlignmentRandomizer.Object);
            Assert.That(generatedPrototype, Is.EqualTo(prototype));
        }

        [Test]
        public void DoNotLogEventsForAlignmentPrototypeGeneration()
        {
            var prototype = new Alignment();
            prototype.Goodness = "prototype goodness";
            prototype.Goodness = "prototype lawfulness";
            mockInnerGenerator.Setup(g => g.GeneratePrototype(mockAlignmentRandomizer.Object)).Returns(prototype);

            var generatedPrototype = decorator.GeneratePrototype(mockAlignmentRandomizer.Object);
            Assert.That(generatedPrototype, Is.EqualTo(prototype));
            mockEventQueue.Verify(q => q.Enqueue(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }
    }
}
