using CreatureGen.Alignments;
using CreatureGen.CharacterClasses;
using CreatureGen.Generators.Races;
using CreatureGen.Creatures;
using CreatureGen.Randomizers.Races;
using EventGen;
using Moq;
using NUnit.Framework;
using System;

namespace CreatureGen.Tests.Unit.Generators.Races
{
    [TestFixture]
    public class RaceGeneratorEventGenDecoratorTests
    {
        private IRaceGenerator decorator;
        private Mock<IRaceGenerator> mockInnerGenerator;
        private Mock<GenEventQueue> mockEventQueue;
        private Alignment alignment;
        private CharacterClass characterClass;
        private RacePrototype racePrototype;
        private Mock<RaceRandomizer> mockBaseRaceRandomizer;
        private Mock<RaceRandomizer> mockMetaraceRandomizer;

        [SetUp]
        public void Setup()
        {
            mockInnerGenerator = new Mock<IRaceGenerator>();
            mockEventQueue = new Mock<GenEventQueue>();
            decorator = new RaceGeneratorEventGenDecorator(mockInnerGenerator.Object, mockEventQueue.Object);

            alignment = new Alignment();
            characterClass = new CharacterClass();
            racePrototype = new RacePrototype();
            mockBaseRaceRandomizer = new Mock<RaceRandomizer>();
            mockMetaraceRandomizer = new Mock<RaceRandomizer>();

            alignment.Goodness = Guid.NewGuid().ToString();
            alignment.Lawfulness = Guid.NewGuid().ToString();

            characterClass.Name = Guid.NewGuid().ToString();
            characterClass.Level = 9266;

            racePrototype.BaseRace = Guid.NewGuid().ToString();
            racePrototype.Metarace = Guid.NewGuid().ToString();
        }

        [Test]
        public void ReturnInnerRacePrototype()
        {
            var classPrototype = new CharacterClassPrototype();
            classPrototype.Name = Guid.NewGuid().ToString();
            classPrototype.Level = 90210;

            mockInnerGenerator.Setup(g => g.GeneratePrototype(alignment, classPrototype, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object)).Returns(racePrototype);

            var generatedPrototype = decorator.GeneratePrototype(alignment, classPrototype, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(generatedPrototype, Is.EqualTo(racePrototype));
        }

        [Test]
        public void DoNotLogEventsForRacePrototypeGeneration()
        {
            var classPrototype = new CharacterClassPrototype();
            classPrototype.Name = Guid.NewGuid().ToString();
            classPrototype.Level = 90210;

            mockInnerGenerator.Setup(g => g.GeneratePrototype(alignment, classPrototype, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object)).Returns(racePrototype);

            var generatedPrototype = decorator.GeneratePrototype(alignment, classPrototype, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(generatedPrototype, Is.EqualTo(racePrototype));
            mockEventQueue.Verify(q => q.Enqueue(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [Test]
        public void ReturnInnerRace()
        {
            var race = new Race();
            mockInnerGenerator.Setup(g => g.GenerateWith(alignment, characterClass, racePrototype)).Returns(race);

            var generatedRace = decorator.GenerateWith(alignment, characterClass, racePrototype);
            Assert.That(generatedRace, Is.EqualTo(race));
        }

        [Test]
        public void LogEventsForRaceGeneration()
        {
            var race = new Race();
            race.BaseRace = "base race";
            race.Metarace = "metarace";

            mockInnerGenerator.Setup(g => g.GenerateWith(alignment, characterClass, racePrototype)).Returns(race);

            var generatedRace = decorator.GenerateWith(alignment, characterClass, racePrototype);
            Assert.That(generatedRace, Is.EqualTo(race));
            mockEventQueue.Verify(q => q.Enqueue(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(2));
            mockEventQueue.Verify(q => q.Enqueue("CharacterGen", $"Generating race for {alignment.Full} {characterClass.Summary} from prototype {racePrototype.Summary}"), Times.Once);
            mockEventQueue.Verify(q => q.Enqueue("CharacterGen", $"Generated {race.Summary}"), Times.Once);
        }
    }
}
