using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Generators.Items;
using CreatureGen.Feats;
using CreatureGen.Items;
using CreatureGen.Creatures;
using EventGen;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace CreatureGen.Tests.Unit.Generators.Items
{
    [TestFixture]
    public class EquipmentGeneratorEventGenDecoratorTests
    {
        private IEquipmentGenerator decorator;
        private Mock<IEquipmentGenerator> mockInnerGenerator;
        private Mock<GenEventQueue> mockEventQueue;
        private CharacterClass characterClass;
        private Race race;
        private List<Feat> feats;

        [SetUp]
        public void Setup()
        {
            mockInnerGenerator = new Mock<IEquipmentGenerator>();
            mockEventQueue = new Mock<GenEventQueue>();
            decorator = new EquipmentGeneratorEventGenDecorator(mockInnerGenerator.Object, mockEventQueue.Object);

            characterClass = new CharacterClass();
            race = new Race();
            feats = new List<Feat>();

            characterClass.Name = Guid.NewGuid().ToString();
            characterClass.Level = 9266;

            race.BaseRace = Guid.NewGuid().ToString();
        }

        [Test]
        public void ReturnInnerEquipment()
        {
            var equipment = new Equipment();
            mockInnerGenerator.Setup(g => g.GenerateWith(feats, characterClass, race)).Returns(equipment);

            var generatedEquipment = decorator.GenerateWith(feats, characterClass, race);
            Assert.That(generatedEquipment, Is.EqualTo(equipment));
        }

        [Test]
        public void LogEventsForEquipmentGeneration()
        {
            var equipment = new Equipment();
            mockInnerGenerator.Setup(g => g.GenerateWith(feats, characterClass, race)).Returns(equipment);

            var generatedEquipment = decorator.GenerateWith(feats, characterClass, race);
            Assert.That(generatedEquipment, Is.EqualTo(equipment));
            mockEventQueue.Verify(q => q.Enqueue(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(2));
            mockEventQueue.Verify(q => q.Enqueue("CharacterGen", $"Generating equipment for {characterClass.Summary} {race.Summary}"), Times.Once);
            mockEventQueue.Verify(q => q.Enqueue("CharacterGen", $"Generated equipment"), Times.Once);
        }
    }
}
