using CreatureGen.Abilities;
using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Generators.Languages;
using CreatureGen.Creatures;
using CreatureGen.Skills;
using EventGen;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace CreatureGen.Tests.Unit.Generators.Languages
{
    [TestFixture]
    public class LanguageGeneratorEventGenDecoratorTests
    {
        private ILanguageGenerator decorator;
        private Mock<ILanguageGenerator> mockInnerGenerator;
        private Mock<GenEventQueue> mockEventQueue;
        private CharacterClass characterClass;
        private Race race;
        private List<Skill> skills;
        private Dictionary<string, Ability> abilities;

        [SetUp]
        public void Setup()
        {
            mockInnerGenerator = new Mock<ILanguageGenerator>();
            mockEventQueue = new Mock<GenEventQueue>();
            decorator = new LanguageGeneratorEventGenDecorator(mockInnerGenerator.Object, mockEventQueue.Object);

            characterClass = new CharacterClass();
            race = new Race();
            skills = new List<Skill>();
            abilities = new Dictionary<string, Ability>();

            characterClass.Name = Guid.NewGuid().ToString();
            characterClass.Level = 9266;

            race.BaseRace = Guid.NewGuid().ToString();

            abilities[AbilityConstants.Intelligence] = new Ability(AbilityConstants.Intelligence);
            abilities[AbilityConstants.Intelligence].BaseValue = 90210;
        }

        [Test]
        public void ReturnInnerLanguages()
        {
            var languages = new[]
            {
                "klingon",
                "German",
            };

            mockInnerGenerator.Setup(g => g.GenerateWith(race, characterClass, abilities, skills)).Returns(languages);

            var generatedLanguages = decorator.GenerateWith(race, characterClass, abilities, skills);
            Assert.That(generatedLanguages, Is.EqualTo(languages));
        }

        [Test]
        public void LogEventsForLanguagesGeneration()
        {
            var languages = new[]
            {
                "klingon",
                "German",
            };

            mockInnerGenerator.Setup(g => g.GenerateWith(race, characterClass, abilities, skills)).Returns(languages);

            var generatedLanguages = decorator.GenerateWith(race, characterClass, abilities, skills);
            Assert.That(generatedLanguages, Is.EqualTo(languages));
            mockEventQueue.Verify(q => q.Enqueue(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(2));
            mockEventQueue.Verify(q => q.Enqueue("CharacterGen", $"Generating language for {characterClass.Name} {race.Summary}"), Times.Once);
            mockEventQueue.Verify(q => q.Enqueue("CharacterGen", $"Generated languages: klingon, German"), Times.Once);
        }
    }
}
