using CreatureGen.Abilities;
using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Generators.Skills;
using CreatureGen.Creatures;
using CreatureGen.Skills;
using EventGen;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace CreatureGen.Tests.Unit.Generators.Skills
{
    [TestFixture]
    public class SkillsGeneratorEventGenDecoratorTests
    {
        private ISkillsGenerator decorator;
        private Mock<ISkillsGenerator> mockInnerGenerator;
        private Mock<GenEventQueue> mockEventQueue;
        private CharacterClass characterClass;
        private Race race;
        private Dictionary<string, Ability> stats;

        [SetUp]
        public void Setup()
        {
            mockInnerGenerator = new Mock<ISkillsGenerator>();
            mockEventQueue = new Mock<GenEventQueue>();
            decorator = new SkillsGeneratorEventGenDecorator(mockInnerGenerator.Object, mockEventQueue.Object);

            characterClass = new CharacterClass();
            race = new Race();
            stats = new Dictionary<string, Ability>();

            characterClass.Name = Guid.NewGuid().ToString();
            characterClass.Level = 9266;

            race.BaseRace = Guid.NewGuid().ToString();
            stats["stat"] = new Ability("stat");
        }

        [Test]
        public void ReturnInnerSkills()
        {
            var skills = new[]
            {
                new Skill("skill 1", stats["stat"], 9266),
                new Skill("skill 2", stats["stat"], 90210),
            };

            mockInnerGenerator.Setup(g => g.GenerateWith(characterClass, race, stats)).Returns(skills);

            var generatedSkills = decorator.GenerateWith(characterClass, race, stats);
            Assert.That(generatedSkills, Is.EqualTo(skills));
        }

        [Test]
        public void LogEventsForSkillsGeneration()
        {
            var skills = new[]
            {
                new Skill("skill 1", stats["stat"], 9266),
                new Skill("skill 2", stats["stat"], 90210),
            };

            mockInnerGenerator.Setup(g => g.GenerateWith(characterClass, race, stats)).Returns(skills);

            var generatedSkills = decorator.GenerateWith(characterClass, race, stats);
            Assert.That(generatedSkills, Is.EqualTo(skills));
            mockEventQueue.Verify(q => q.Enqueue(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(2));
            mockEventQueue.Verify(q => q.Enqueue("CharacterGen", $"Generating skills for {characterClass.Summary} {race.Summary}"), Times.Once);
            mockEventQueue.Verify(q => q.Enqueue("CharacterGen", $"Generated skills: [skill 1, skill 2]"), Times.Once);
        }
    }
}
