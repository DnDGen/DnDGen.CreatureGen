using CreatureGen.Abilities;
using CreatureGen.CharacterClasses;
using CreatureGen.Combats;
using CreatureGen.Generators.Defenses;
using CreatureGen.Feats;
using CreatureGen.Items;
using CreatureGen.Creatures;
using EventGen;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace CreatureGen.Tests.Unit.Generators.Combats
{
    [TestFixture]
    public class CombatGeneratorEventGenDecoratorTests
    {
        private ICombatGenerator decorator;
        private Mock<ICombatGenerator> mockInnerGenerator;
        private Mock<GenEventQueue> mockEventQueue;
        private CharacterClass characterClass;
        private Race race;
        private Dictionary<string, Ability> stats;
        private List<Feat> feats;
        private Equipment equipment;

        [SetUp]
        public void Setup()
        {
            mockInnerGenerator = new Mock<ICombatGenerator>();
            mockEventQueue = new Mock<GenEventQueue>();
            decorator = new CombatGeneratorEventGenDecorator(mockInnerGenerator.Object, mockEventQueue.Object);

            characterClass = new CharacterClass();
            race = new Race();
            stats = new Dictionary<string, Ability>();
            feats = new List<Feat>();
            equipment = new Equipment();

            characterClass.Name = Guid.NewGuid().ToString();
            characterClass.Level = 9266;

            race.BaseRace = Guid.NewGuid().ToString();
        }

        [Test]
        public void ReturnInnerCombat()
        {
            var baseAttack = new BaseAttack();
            var combat = new Combat();
            mockInnerGenerator.Setup(g => g.GenerateWith(baseAttack, characterClass, race, feats, stats, equipment)).Returns(combat);

            var generatedCombat = decorator.GenerateWith(baseAttack, characterClass, race, feats, stats, equipment);
            Assert.That(generatedCombat, Is.EqualTo(combat));
        }

        [Test]
        public void LogEventsForCombatGeneration()
        {
            var baseAttack = new BaseAttack();
            var combat = new Combat();
            mockInnerGenerator.Setup(g => g.GenerateWith(baseAttack, characterClass, race, feats, stats, equipment)).Returns(combat);

            var generatedCombat = decorator.GenerateWith(baseAttack, characterClass, race, feats, stats, equipment);
            Assert.That(generatedCombat, Is.EqualTo(combat));
            mockEventQueue.Verify(q => q.Enqueue(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(2));
            mockEventQueue.Verify(q => q.Enqueue("CharacterGen", $"Generating combat statistics for {characterClass.Summary} {race.Summary}"), Times.Once);
            mockEventQueue.Verify(q => q.Enqueue("CharacterGen", $"Generated combat statistics"), Times.Once);
        }

        [Test]
        public void ReturnInnerBaseAttack()
        {
            var baseAttack = new BaseAttack();
            mockInnerGenerator.Setup(g => g.GenerateBaseAttackWith(characterClass, race, stats)).Returns(baseAttack);

            var generatedBaseAttack = decorator.GenerateBaseAttackWith(characterClass, race, stats);
            Assert.That(generatedBaseAttack, Is.EqualTo(baseAttack));
        }

        [Test]
        public void LogEventsForBaseAttackGeneration()
        {
            var baseAttack = new BaseAttack();
            mockInnerGenerator.Setup(g => g.GenerateBaseAttackWith(characterClass, race, stats)).Returns(baseAttack);

            var generatedBaseAttack = decorator.GenerateBaseAttackWith(characterClass, race, stats);
            Assert.That(generatedBaseAttack, Is.EqualTo(baseAttack));
            mockEventQueue.Verify(q => q.Enqueue(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(2));
            mockEventQueue.Verify(q => q.Enqueue("CharacterGen", $"Generating base attack for {characterClass.Summary} {race.Summary}"), Times.Once);
            mockEventQueue.Verify(q => q.Enqueue("CharacterGen", $"Generated base attack"), Times.Once);
        }
    }
}
