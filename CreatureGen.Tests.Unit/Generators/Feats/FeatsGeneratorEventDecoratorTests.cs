using CreatureGen.Abilities;
using CreatureGen.Generators.Feats;
using CreatureGen.Skills;
using EventGen;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace CreatureGen.Tests.Unit.Generators.Feats
{
    [TestFixture]
    public class FeatsGeneratorEventDecoratorTests
    {
        private IFeatsGenerator decorator;
        private Mock<IFeatsGenerator> mockInnerGenerator;
        private Mock<GenEventQueue> mockEventQueue;
        private CharacterClass characterClass;
        private Race race;
        private Dictionary<string, Ability> stats;
        private List<Skill> skills;
        private BaseAttack baseAttack;

        [SetUp]
        public void Setup()
        {
            mockInnerGenerator = new Mock<IFeatsGenerator>();
            mockEventQueue = new Mock<GenEventQueue>();
            decorator = new FeatsGeneratorEventDecorator(mockInnerGenerator.Object, mockEventQueue.Object);

            characterClass = new CharacterClass();
            race = new Race();
            stats = new Dictionary<string, Ability>();
            skills = new List<Skill>();
            baseAttack = new BaseAttack();

            characterClass.Name = Guid.NewGuid().ToString();
            characterClass.Level = 9266;

            race.BaseRace = Guid.NewGuid().ToString();
        }

        [Test]
        public void ReturnInnerFeats()
        {
            var feats = new FeatCollections();
            mockInnerGenerator.Setup(g => g.GenerateWith(characterClass, race, stats, skills, baseAttack)).Returns(feats);

            var generatedFeats = decorator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            Assert.That(generatedFeats, Is.EqualTo(feats));
        }

        [Test]
        public void LogEventsForFeatsGeneration()
        {
            var feats = new FeatCollections();
            mockInnerGenerator.Setup(g => g.GenerateWith(characterClass, race, stats, skills, baseAttack)).Returns(feats);

            var generatedFeats = decorator.GenerateWith(characterClass, race, stats, skills, baseAttack);
            Assert.That(generatedFeats, Is.EqualTo(feats));
            mockEventQueue.Verify(q => q.Enqueue(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(2));
            mockEventQueue.Verify(q => q.Enqueue("CreatureGen", $"Generating feats for {characterClass.Summary} {race.Summary}"), Times.Once);
            mockEventQueue.Verify(q => q.Enqueue("CreatureGen", $"Generated feats"), Times.Once);
        }
    }
}
