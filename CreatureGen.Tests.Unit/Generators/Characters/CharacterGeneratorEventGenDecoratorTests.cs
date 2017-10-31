using CreatureGen.Creatures;
using CreatureGen.Generators.Creatures;
using CreatureGen.Randomizers.Abilities;
using CreatureGen.Randomizers.Alignments;
using CreatureGen.Randomizers.CharacterClasses;
using CreatureGen.Randomizers.Races;
using EventGen;
using Moq;
using NUnit.Framework;
using System;

namespace CreatureGen.Tests.Unit.Generators.Characters
{
    [TestFixture]
    public class CharacterGeneratorEventGenDecoratorTests
    {
        private ICharacterGenerator decorator;
        private Mock<ICharacterGenerator> mockInnerGenerator;
        private Mock<GenEventQueue> mockEventQueue;
        private Mock<IAlignmentRandomizer> mockAlignmentRandomizer;
        private Mock<IClassNameRandomizer> mockClassNameRandomizer;
        private Mock<ILevelRandomizer> mockLevelRandomizer;
        private Mock<RaceRandomizer> mockBaseRaceRandomizer;
        private Mock<RaceRandomizer> mockMetaraceRandomizer;
        private Mock<IAbilitiesRandomizer> mockAbilitiesRandomizer;

        [SetUp]
        public void Setup()
        {
            mockInnerGenerator = new Mock<ICharacterGenerator>();
            mockEventQueue = new Mock<GenEventQueue>();
            decorator = new CharacterGeneratorEventGenDecorator(mockInnerGenerator.Object, mockEventQueue.Object);

            mockAlignmentRandomizer = new Mock<IAlignmentRandomizer>();
            mockClassNameRandomizer = new Mock<IClassNameRandomizer>();
            mockLevelRandomizer = new Mock<ILevelRandomizer>();
            mockAbilitiesRandomizer = new Mock<IAbilitiesRandomizer>();
            mockBaseRaceRandomizer = new Mock<RaceRandomizer>();
            mockMetaraceRandomizer = new Mock<RaceRandomizer>();
        }

        [Test]
        public void ReturnInnerCharacter()
        {
            var character = new Character();
            mockInnerGenerator.Setup(g => g.GenerateWith(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object, mockLevelRandomizer.Object, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object, mockAbilitiesRandomizer.Object))
                .Returns(character);

            var generatedCharacter = decorator.GenerateWith(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object, mockLevelRandomizer.Object, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object, mockAbilitiesRandomizer.Object);
            Assert.That(generatedCharacter, Is.EqualTo(character));
        }

        [Test]
        public void LogEventsForCharacterGeneration()
        {
            var character = new Character();
            character.Alignment.Goodness = Guid.NewGuid().ToString();
            character.Alignment.Lawfulness = Guid.NewGuid().ToString();
            character.Class.Name = Guid.NewGuid().ToString();
            character.Class.Level = 9266;
            character.Race.BaseRace = Guid.NewGuid().ToString();
            character.Race.Metarace = Guid.NewGuid().ToString();

            mockInnerGenerator.Setup(g => g.GenerateWith(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object, mockLevelRandomizer.Object, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object, mockAbilitiesRandomizer.Object))
                .Returns(character);

            var generatedCharacter = decorator.GenerateWith(mockAlignmentRandomizer.Object, mockClassNameRandomizer.Object, mockLevelRandomizer.Object, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object, mockAbilitiesRandomizer.Object);
            Assert.That(generatedCharacter, Is.EqualTo(character));
            mockEventQueue.Verify(q => q.Enqueue(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(2));
            mockEventQueue.Verify(q => q.Enqueue("CharacterGen", "Generating character"), Times.Once);
            mockEventQueue.Verify(q => q.Enqueue("CharacterGen", $"Generated {character.Summary}"), Times.Once);
        }
    }
}
