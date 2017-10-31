using CreatureGen.Abilities;
using CreatureGen.Creatures;
using CreatureGen.Generators.Skills;
using CreatureGen.Skills;
using EventGen;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace CreatureGen.Tests.Unit.Generators.Skills
{
    [TestFixture]
    public class SkillsGeneratorEventDecoratorTests
    {
        private ISkillsGenerator decorator;
        private Mock<ISkillsGenerator> mockInnerGenerator;
        private Mock<GenEventQueue> mockEventQueue;
        private Creature creature;
        private Dictionary<string, Ability> abilities;

        [SetUp]
        public void Setup()
        {
            mockInnerGenerator = new Mock<ISkillsGenerator>();
            mockEventQueue = new Mock<GenEventQueue>();
            decorator = new SkillsGeneratorEventGenDecorator(mockInnerGenerator.Object, mockEventQueue.Object);

            creature = new Creature();
            abilities = new Dictionary<string, Ability>();

            creature.Name = Guid.NewGuid().ToString();
            creature.HitPoints.HitDiceQuantity = 9266;

            abilities["stat"] = new Ability("stat");
        }

        [Test]
        public void ReturnInnerSkills()
        {
            var skills = new[]
            {
                new Skill("skill 1", abilities["stat"], 9266),
                new Skill("skill 2", abilities["stat"], 90210),
            };

            mockInnerGenerator.Setup(g => g.GenerateFor(creature)).Returns(skills);

            var generatedSkills = decorator.GenerateFor(creature);
            Assert.That(generatedSkills, Is.EqualTo(skills));
        }

        [Test]
        public void LogEventsForSkillsGeneration()
        {
            var skills = new[]
            {
                new Skill("skill 1", abilities["stat"], 9266),
                new Skill("skill 2", abilities["stat"], 90210),
            };

            mockInnerGenerator.Setup(g => g.GenerateFor(creature)).Returns(skills);

            var generatedSkills = decorator.GenerateFor(creature);
            Assert.That(generatedSkills, Is.EqualTo(skills));
            mockEventQueue.Verify(q => q.Enqueue(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(2));
            mockEventQueue.Verify(q => q.Enqueue("CreatureGen", $"Generating skills for {creature.Summary}"), Times.Once);
            mockEventQueue.Verify(q => q.Enqueue("CreatureGen", $"Generated skills: [skill 1, skill 2]"), Times.Once);
        }
    }
}
