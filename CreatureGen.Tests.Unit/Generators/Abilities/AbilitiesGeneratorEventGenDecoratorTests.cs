using CreatureGen.Abilities;
using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Generators.Abilities;
using CreatureGen.Creatures;
using CreatureGen.Randomizers.Abilities;
using EventGen;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace CreatureGen.Tests.Unit.Generators.Abilities
{
    [TestFixture]
    public class AbilitiesGeneratorEventGenDecoratorTests
    {
        private IAbilitiesGenerator decorator;
        private Mock<IAbilitiesGenerator> mockInnerGenerator;
        private Mock<GenEventQueue> mockEventQueue;
        private Mock<IAbilitiesRandomizer> mockAbilitiesRandomizer;
        private CharacterClass characterClass;
        private Race race;

        [SetUp]
        public void Setup()
        {
            mockInnerGenerator = new Mock<IAbilitiesGenerator>();
            mockEventQueue = new Mock<GenEventQueue>();
            decorator = new AbilitiesGeneratorEventGenDecorator(mockInnerGenerator.Object, mockEventQueue.Object);

            mockAbilitiesRandomizer = new Mock<IAbilitiesRandomizer>();

            characterClass = new CharacterClass();
            race = new Race();

            characterClass.Name = Guid.NewGuid().ToString();
            characterClass.Level = 9266;

            race.BaseRace = Guid.NewGuid().ToString();
        }

        [Test]
        public void ReturnInnerAbilities()
        {
            var abilities = new Dictionary<string, Ability>();
            abilities["ability 1"] = new Ability("ability 1");
            abilities["ability 2"] = new Ability("ability 2");

            mockInnerGenerator.Setup(g => g.GenerateWith(mockAbilitiesRandomizer.Object, characterClass, race)).Returns(abilities);

            var generatedAbilities = decorator.GenerateWith(mockAbilitiesRandomizer.Object, characterClass, race);
            Assert.That(generatedAbilities, Is.EqualTo(abilities));
        }

        [Test]
        public void LogEventsForAbilitiesGeneration()
        {
            var abilities = new Dictionary<string, Ability>();
            abilities["ability"] = new Ability("ability");
            abilities["ability"].BaseValue = 9266;
            abilities["other ability"] = new Ability("other ability");
            abilities["other ability"].BaseValue = 90210;

            mockInnerGenerator.Setup(g => g.GenerateWith(mockAbilitiesRandomizer.Object, characterClass, race)).Returns(abilities);

            var generatedAbilities = decorator.GenerateWith(mockAbilitiesRandomizer.Object, characterClass, race);
            Assert.That(generatedAbilities, Is.EqualTo(abilities));
            mockEventQueue.Verify(q => q.Enqueue(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(2));
            mockEventQueue.Verify(q => q.Enqueue("CharacterGen", $"Generating abilities for {characterClass.Summary} {race.Summary}"), Times.Once);
            mockEventQueue.Verify(q => q.Enqueue("CharacterGen", $"Generated abilities: [ability 9266, other ability 90210]"), Times.Once);
        }
    }
}
