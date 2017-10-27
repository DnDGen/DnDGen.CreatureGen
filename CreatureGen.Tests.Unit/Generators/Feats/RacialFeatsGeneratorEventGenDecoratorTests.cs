using CreatureGen.Abilities;
using CreatureGen.Domain.Generators.Feats;
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
    public class RacialFeatsGeneratorEventGenDecoratorTests
    {
        private IRacialFeatsGenerator decorator;
        private Mock<IRacialFeatsGenerator> mockInnerGenerator;
        private Mock<GenEventQueue> mockEventQueue;
        private Race race;
        private Dictionary<string, Ability> stats;
        private List<Skill> skills;

        [SetUp]
        public void Setup()
        {
            mockInnerGenerator = new Mock<IRacialFeatsGenerator>();
            mockEventQueue = new Mock<GenEventQueue>();
            decorator = new RacialFeatsGeneratorEventGenDecorator(mockInnerGenerator.Object, mockEventQueue.Object);

            race = new Race();
            stats = new Dictionary<string, Ability>();
            skills = new List<Skill>();

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

            mockInnerGenerator.Setup(g => g.GenerateWith(race, skills, stats)).Returns(feats);

            var generatedFeats = decorator.GenerateWith(race, skills, stats);
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

            mockInnerGenerator.Setup(g => g.GenerateWith(race, skills, stats)).Returns(feats);

            var generatedFeats = decorator.GenerateWith(race, skills, stats);
            Assert.That(generatedFeats, Is.EqualTo(feats));
            mockEventQueue.Verify(q => q.Enqueue(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(2));
            mockEventQueue.Verify(q => q.Enqueue("CharacterGen", $"Generating racial feats for {race.Summary}"), Times.Once);
            mockEventQueue.Verify(q => q.Enqueue("CharacterGen", $"Generated racial feats: [feat, other feat]"), Times.Once);
        }
    }
}
