using CreatureGen.Abilities;
using CreatureGen.Alignments;
using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Generators.Magics;
using CreatureGen.Feats;
using CreatureGen.Items;
using CreatureGen.Magics;
using CreatureGen.Creatures;
using EventGen;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace CreatureGen.Tests.Unit.Generators.Magics
{
    [TestFixture]
    public class MagicGeneratorEventGenDecoratorTests
    {
        private IMagicGenerator decorator;
        private Mock<IMagicGenerator> mockInnerGenerator;
        private Mock<GenEventQueue> mockEventQueue;
        private Alignment alignment;
        private CharacterClass characterClass;
        private Race race;
        private Dictionary<string, Ability> stats;
        private List<Feat> feats;
        private Equipment equipment;

        [SetUp]
        public void Setup()
        {
            mockInnerGenerator = new Mock<IMagicGenerator>();
            mockEventQueue = new Mock<GenEventQueue>();
            decorator = new MagicGeneratorEventGenDecorator(mockInnerGenerator.Object, mockEventQueue.Object);

            alignment = new Alignment();
            characterClass = new CharacterClass();
            race = new Race();
            stats = new Dictionary<string, Ability>();
            feats = new List<Feat>();
            equipment = new Equipment();

            alignment.Goodness = Guid.NewGuid().ToString();
            alignment.Lawfulness = Guid.NewGuid().ToString();

            characterClass.Name = Guid.NewGuid().ToString();
            characterClass.Level = 9266;

            race.BaseRace = Guid.NewGuid().ToString();
        }

        [Test]
        public void ReturnInnerMagic()
        {
            var magic = new Magic();
            mockInnerGenerator.Setup(g => g.GenerateWith(alignment, characterClass, race, stats, feats, equipment)).Returns(magic);

            var generatedMagic = decorator.GenerateWith(alignment, characterClass, race, stats, feats, equipment);
            Assert.That(generatedMagic, Is.EqualTo(magic));
        }

        [Test]
        public void LogEventsForMagicGeneration()
        {
            var magic = new Magic();
            mockInnerGenerator.Setup(g => g.GenerateWith(alignment, characterClass, race, stats, feats, equipment)).Returns(magic);

            var generatedMagic = decorator.GenerateWith(alignment, characterClass, race, stats, feats, equipment);
            Assert.That(generatedMagic, Is.EqualTo(magic));
            mockEventQueue.Verify(q => q.Enqueue(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(2));
            mockEventQueue.Verify(q => q.Enqueue("CharacterGen", $"Generating magic for {alignment.Full} {characterClass.Summary} {race.Summary}"), Times.Once);
            mockEventQueue.Verify(q => q.Enqueue("CharacterGen", $"Generated magic"), Times.Once);
        }
    }
}
