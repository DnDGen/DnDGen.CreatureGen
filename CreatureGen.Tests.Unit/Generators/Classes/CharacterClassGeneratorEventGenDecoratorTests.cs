using CreatureGen.Alignments;
using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Generators.Classes;
using CreatureGen.Creatures;
using CreatureGen.Randomizers.CharacterClasses;
using EventGen;
using Moq;
using NUnit.Framework;
using System.Linq;

namespace CreatureGen.Tests.Unit.Generators.Classes
{
    [TestFixture]
    public class CharacterClassGeneratorEventGenDecoratorTests
    {
        private ICharacterClassGenerator decorator;
        private Mock<ICharacterClassGenerator> mockInnerGenerator;
        private Mock<GenEventQueue> mockEventQueue;
        private Alignment alignment;
        private CharacterClassPrototype classPrototype;
        private Mock<IClassNameRandomizer> mockClassNameRandomizer;
        private Mock<ILevelRandomizer> mockLevelRandomizer;

        [SetUp]
        public void Setup()
        {
            mockInnerGenerator = new Mock<ICharacterClassGenerator>();
            mockEventQueue = new Mock<GenEventQueue>();
            decorator = new CharacterClassGeneratorEventGenDecorator(mockInnerGenerator.Object, mockEventQueue.Object);

            alignment = new Alignment();
            classPrototype = new CharacterClassPrototype();
            mockClassNameRandomizer = new Mock<IClassNameRandomizer>();
            mockLevelRandomizer = new Mock<ILevelRandomizer>();

            alignment.Goodness = "goodness";
            alignment.Lawfulness = "lawfulness";
            classPrototype.Level = 9266;
            classPrototype.Name = "class name";
        }

        [Test]
        public void ReturnInnerClassPrototype()
        {
            mockInnerGenerator.Setup(g => g.GeneratePrototype(alignment, mockClassNameRandomizer.Object, mockLevelRandomizer.Object)).Returns(classPrototype);

            var generatedPrototype = decorator.GeneratePrototype(alignment, mockClassNameRandomizer.Object, mockLevelRandomizer.Object);
            Assert.That(generatedPrototype, Is.EqualTo(classPrototype));
        }

        [Test]
        public void DoNotLogEventsForClassPrototypeGeneration()
        {
            mockInnerGenerator.Setup(g => g.GeneratePrototype(alignment, mockClassNameRandomizer.Object, mockLevelRandomizer.Object)).Returns(classPrototype);

            var generatedPrototype = decorator.GeneratePrototype(alignment, mockClassNameRandomizer.Object, mockLevelRandomizer.Object);
            Assert.That(generatedPrototype, Is.EqualTo(classPrototype));
            mockEventQueue.Verify(q => q.Enqueue(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [Test]
        public void ReturnInnerClass()
        {
            var characterClass = new CharacterClass();
            mockInnerGenerator.Setup(g => g.GenerateWith(alignment, classPrototype)).Returns(characterClass);

            var generatedClass = decorator.GenerateWith(alignment, classPrototype);
            Assert.That(generatedClass, Is.EqualTo(characterClass));
        }

        [Test]
        public void LogEventsForClassGeneration()
        {
            var characterClass = new CharacterClass();
            characterClass.Name = "class name";
            characterClass.Level = 9266;

            mockInnerGenerator.Setup(g => g.GenerateWith(alignment, classPrototype)).Returns(characterClass);

            var generatedClass = decorator.GenerateWith(alignment, classPrototype);
            Assert.That(generatedClass, Is.EqualTo(characterClass));
            mockEventQueue.Verify(q => q.Enqueue(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(2));
            mockEventQueue.Verify(q => q.Enqueue("CharacterGen", $"Generating class for {alignment.Full} from prototype {classPrototype.Summary}"), Times.Once);
            mockEventQueue.Verify(q => q.Enqueue("CharacterGen", $"Generated {characterClass.Summary}"), Times.Once);
        }

        [Test]
        public void ReturnInnerRegeneratedSpecialistFields()
        {
            var characterClass = new CharacterClass();
            var race = new Race();
            var fields = new[]
            {
                "field 1",
                "field 2"
            };

            mockInnerGenerator.Setup(g => g.RegenerateSpecialistFields(alignment, characterClass, race)).Returns(fields);

            var generatedFields = decorator.RegenerateSpecialistFields(alignment, characterClass, race);
            Assert.That(generatedFields, Is.EqualTo(fields));
        }

        [Test]
        public void LogEventsForRegenerationOfSpecialistFields()
        {
            var characterClass = new CharacterClass { Name = "class name", Level = 9266 };
            var race = new Race { BaseRace = "base race" };
            var fields = new[]
            {
                "field 1",
                "field 2"
            };

            mockInnerGenerator.Setup(g => g.RegenerateSpecialistFields(alignment, characterClass, race)).Returns(fields);

            var generatedFields = decorator.RegenerateSpecialistFields(alignment, characterClass, race);
            Assert.That(generatedFields, Is.EqualTo(fields));
            mockEventQueue.Verify(q => q.Enqueue(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(2));
            mockEventQueue.Verify(q => q.Enqueue("CharacterGen", $"Regenerating specialist fields for {alignment.Full} {characterClass.Summary} {race.Summary}"), Times.Once);
            mockEventQueue.Verify(q => q.Enqueue("CharacterGen", $"Regenerated specialist fields: {string.Join(", ", fields)}"), Times.Once);
        }

        [Test]
        public void LogEventsForRegenerationOfNoSpecialistFields()
        {
            var characterClass = new CharacterClass { Name = "class name", Level = 9266 };
            var race = new Race { BaseRace = "base race" };
            var fields = Enumerable.Empty<string>();

            mockInnerGenerator.Setup(g => g.RegenerateSpecialistFields(alignment, characterClass, race)).Returns(fields);

            var generatedFields = decorator.RegenerateSpecialistFields(alignment, characterClass, race);
            Assert.That(generatedFields, Is.EqualTo(fields));
            mockEventQueue.Verify(q => q.Enqueue(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(2));
            mockEventQueue.Verify(q => q.Enqueue("CharacterGen", $"Regenerating specialist fields for {alignment.Full} {characterClass.Summary} {race.Summary}"), Times.Once);
            mockEventQueue.Verify(q => q.Enqueue("CharacterGen", $"Regenerated no specialist fields"), Times.Once);
        }
    }
}
