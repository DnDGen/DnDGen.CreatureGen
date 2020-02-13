using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Generators.Creatures;
using DnDGen.EventGen;
using Moq;
using NUnit.Framework;
using System;

namespace DnDGen.CreatureGen.Tests.Unit.Generators.Creatures
{
    [TestFixture]
    public class CreatureGeneratorEventDecoratorTests
    {
        private ICreatureGenerator decorator;
        private Mock<ICreatureGenerator> mockInnerGenerator;
        private Mock<GenEventQueue> mockEventQueue;

        [SetUp]
        public void Setup()
        {
            mockInnerGenerator = new Mock<ICreatureGenerator>();
            mockEventQueue = new Mock<GenEventQueue>();
            decorator = new CreatureGeneratorEventDecorator(mockInnerGenerator.Object, mockEventQueue.Object);
        }

        [Test]
        public void ReturnInnerCreature()
        {
            var creature = new Creature();
            creature.Name = Guid.NewGuid().ToString();
            creature.Template = Guid.NewGuid().ToString();

            mockInnerGenerator.Setup(g => g.Generate("creature name", "template name")).Returns(creature);

            var generatedCharacter = decorator.Generate("creature name", "template name");
            Assert.That(generatedCharacter, Is.EqualTo(creature));
        }

        [Test]
        public void LogEventsForCreatureGeneration()
        {
            var creature = new Creature();
            creature.Name = Guid.NewGuid().ToString();
            creature.Template = Guid.NewGuid().ToString();

            mockInnerGenerator.Setup(g => g.Generate("creature name", "template name")).Returns(creature);

            var generatedCharacter = decorator.Generate("creature name", "template name");
            Assert.That(generatedCharacter, Is.EqualTo(creature));
            mockEventQueue.Verify(q => q.Enqueue(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(2));
            mockEventQueue.Verify(q => q.Enqueue("CreatureGen", $"Generating template name creature name"), Times.Once);
            mockEventQueue.Verify(q => q.Enqueue("CreatureGen", $"Generated {creature.Summary}"), Times.Once);
        }

        [Test]
        public void LogEventsForCreatureGenerationWithNoTemplate()
        {
            var creature = new Creature();
            creature.Name = Guid.NewGuid().ToString();

            mockInnerGenerator.Setup(g => g.Generate("creature name", string.Empty)).Returns(creature);

            var generatedCharacter = decorator.Generate("creature name", string.Empty);
            Assert.That(generatedCharacter, Is.EqualTo(creature));
            mockEventQueue.Verify(q => q.Enqueue(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(2));
            mockEventQueue.Verify(q => q.Enqueue("CreatureGen", $"Generating creature name"), Times.Once);
            mockEventQueue.Verify(q => q.Enqueue("CreatureGen", $"Generated {creature.Summary}"), Times.Once);
        }
    }
}
