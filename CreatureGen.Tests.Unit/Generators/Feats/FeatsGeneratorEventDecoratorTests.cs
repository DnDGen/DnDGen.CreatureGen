using CreatureGen.Abilities;
using CreatureGen.Creatures;
using CreatureGen.Feats;
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
        private Dictionary<string, Ability> abilities;
        private List<Skill> skills;
        private List<Feat> preselectedFeats;
        private Creature creature;

        [SetUp]
        public void Setup()
        {
            mockInnerGenerator = new Mock<IFeatsGenerator>();
            mockEventQueue = new Mock<GenEventQueue>();
            decorator = new FeatsGeneratorEventDecorator(mockInnerGenerator.Object, mockEventQueue.Object);

            abilities = new Dictionary<string, Ability>();
            skills = new List<Skill>();
            preselectedFeats = new List<Feat>();
            creature = new Creature();

            creature.Name = Guid.NewGuid().ToString();
            creature.Template = Guid.NewGuid().ToString();
        }

        [Test]
        public void ReturnInnerFeats()
        {
            var feats = new[]
            {
                new Feat(),
                new Feat(),
            };

            mockInnerGenerator.Setup(g => g.GenerateFeats(creature)).Returns(feats);

            var generatedFeats = decorator.GenerateFeats(creature);
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

            mockInnerGenerator.Setup(g => g.GenerateFeats(creature)).Returns(feats);

            var generatedFeats = decorator.GenerateFeats(creature);
            Assert.That(generatedFeats, Is.EqualTo(feats));
            mockEventQueue.Verify(q => q.Enqueue(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(2));
            mockEventQueue.Verify(q => q.Enqueue("CreatureGen", $"Generating feats for {creature.Summary}"), Times.Once);
            mockEventQueue.Verify(q => q.Enqueue("CreatureGen", $"Generated 2 feats"), Times.Once);
        }

        [Test]
        public void ReturnInnerSpecialQualities()
        {
            var feats = new[]
            {
                new Feat(),
                new Feat(),
            };

            mockInnerGenerator.Setup(g => g.GenerateSpecialQualities(race, skills, abilities)).Returns(feats);

            var generatedFeats = decorator.GenerateSpecialQualities(race, skills, abilities);
            Assert.That(generatedFeats, Is.EqualTo(feats));
        }

        [Test]
        public void LogEventsForSpecialQualitiesGeneration()
        {
            var feats = new[]
            {
                new Feat { Name = "feat" },
                new Feat { Name = "other feat" },
            };

            mockInnerGenerator.Setup(g => g.GenerateSpecialQualities(race, skills, abilities)).Returns(feats);

            var generatedFeats = decorator.GenerateSpecialQualities(race, skills, abilities);
            Assert.That(generatedFeats, Is.EqualTo(feats));
            mockEventQueue.Verify(q => q.Enqueue(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(2));
            mockEventQueue.Verify(q => q.Enqueue("CreatureGen", $"Generating racial feats for {race.Summary}"), Times.Once);
            mockEventQueue.Verify(q => q.Enqueue("CreatureGen", $"Generated racial feats: [feat, other feat]"), Times.Once);
        }
    }
}
