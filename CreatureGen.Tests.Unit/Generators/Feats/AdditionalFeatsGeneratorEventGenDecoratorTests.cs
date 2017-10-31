using CreatureGen.Abilities;
using CreatureGen.CharacterClasses;
using CreatureGen.Combats;
using CreatureGen.Generators.Feats;
using CreatureGen.Feats;
using CreatureGen.Creatures;
using CreatureGen.Skills;
using EventGen;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace CreatureGen.Tests.Unit.Generators.Feats
{
    [TestFixture]
    public class AdditionalFeatsGeneratorEventGenDecoratorTests
    {
        private IAdditionalFeatsGenerator decorator;
        private Mock<IAdditionalFeatsGenerator> mockInnerGenerator;
        private Mock<GenEventQueue> mockEventQueue;
        private CharacterClass characterClass;
        private Race race;
        private Dictionary<string, Ability> stats;
        private List<Skill> skills;
        private BaseAttack baseAttack;
        private List<Feat> preselectedFeats;

        [SetUp]
        public void Setup()
        {
            mockInnerGenerator = new Mock<IAdditionalFeatsGenerator>();
            mockEventQueue = new Mock<GenEventQueue>();
            decorator = new AdditionalFeatsGeneratorEventGenDecorator(mockInnerGenerator.Object, mockEventQueue.Object);

            characterClass = new CharacterClass();
            race = new Race();
            stats = new Dictionary<string, Ability>();
            skills = new List<Skill>();
            baseAttack = new BaseAttack();
            preselectedFeats = new List<Feat>();

            characterClass.Name = Guid.NewGuid().ToString();
            characterClass.Level = 9266;

            race.BaseRace = Guid.NewGuid().ToString();
        }

        [Test]
        public void ReturnInnerFeats()
        {
            var feats = new[]
            {
                new Feat(),
                new Feat(),
            };

            mockInnerGenerator.Setup(g => g.GenerateWith(characterClass, race, stats, skills, baseAttack, preselectedFeats)).Returns(feats);

            var generatedFeats = decorator.GenerateWith(characterClass, race, stats, skills, baseAttack, preselectedFeats);
            Assert.That(generatedFeats, Is.EqualTo(feats));
        }

        [Test]
        public void LogEventsForFeatsGeneration()
        {
            var feats = new[]
            {
                new Feat { Name = "feat" },
                new Feat { Name = "other feat" },
            };

            mockInnerGenerator.Setup(g => g.GenerateWith(characterClass, race, stats, skills, baseAttack, preselectedFeats)).Returns(feats);

            var generatedFeats = decorator.GenerateWith(characterClass, race, stats, skills, baseAttack, preselectedFeats);
            Assert.That(generatedFeats, Is.EqualTo(feats));
            mockEventQueue.Verify(q => q.Enqueue(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(2));
            mockEventQueue.Verify(q => q.Enqueue("CharacterGen", $"Generating additional feats for {characterClass.Summary} {race.Summary}"), Times.Once);
            mockEventQueue.Verify(q => q.Enqueue("CharacterGen", $"Generated additional feats: [feat, other feat]"), Times.Once);
        }
    }
}
