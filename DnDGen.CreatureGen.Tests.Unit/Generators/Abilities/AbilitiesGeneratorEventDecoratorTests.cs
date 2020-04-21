using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Generators.Abilities;
using DnDGen.CreatureGen.Items;
using DnDGen.EventGen;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace DnDGen.CreatureGen.Tests.Unit.Generators.Abilities
{
    [TestFixture]
    public class AbilitiesGeneratorEventDecoratorTests
    {
        private IAbilitiesGenerator decorator;
        private Mock<IAbilitiesGenerator> mockInnerGenerator;
        private Mock<GenEventQueue> mockEventQueue;

        [SetUp]
        public void Setup()
        {
            mockInnerGenerator = new Mock<IAbilitiesGenerator>();
            mockEventQueue = new Mock<GenEventQueue>();
            decorator = new AbilitiesGeneratorEventDecorator(mockInnerGenerator.Object, mockEventQueue.Object);
        }

        [Test]
        public void ReturnInnerAbilities()
        {
            var abilities = new Dictionary<string, Ability>();
            abilities["ability 1"] = new Ability("ability 1");
            abilities["ability 2"] = new Ability("ability 2");

            mockInnerGenerator.Setup(g => g.GenerateFor("creature name")).Returns(abilities);

            var generatedAbilities = decorator.GenerateFor("creature name");
            Assert.That(generatedAbilities, Is.EqualTo(abilities));
        }

        [Test]
        public void LogEventsForAbilitiesGeneration()
        {
            var abilities = new Dictionary<string, Ability>();
            abilities["ability"] = new Ability("ability");
            abilities["ability"].BaseScore = 9266;
            abilities["other ability"] = new Ability("other ability");
            abilities["other ability"].BaseScore = 90210;

            mockInnerGenerator.Setup(g => g.GenerateFor("creature name")).Returns(abilities);

            var generatedAbilities = decorator.GenerateFor("creature name");
            Assert.That(generatedAbilities, Is.EqualTo(abilities));
            mockEventQueue.Verify(q => q.Enqueue(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(2));
            mockEventQueue.Verify(q => q.Enqueue("CreatureGen", $"Generating abilities for creature name"), Times.Once);
            mockEventQueue.Verify(q => q.Enqueue("CreatureGen", $"Generated 2 abilities"), Times.Once);
        }

        [Test]
        public void ReturnInnerModifiedAbilities()
        {
            var abilities = new Dictionary<string, Ability>();
            abilities["ability 1"] = new Ability("ability 1");
            abilities["ability 2"] = new Ability("ability 2");

            var equipment = new Equipment();

            var modifiedAbilities = new Dictionary<string, Ability>();
            modifiedAbilities["ability 1"] = new Ability("ability 1");
            modifiedAbilities["ability 2"] = new Ability("ability 2");

            mockInnerGenerator.Setup(g => g.SetMaxBonuses(abilities, equipment)).Returns(modifiedAbilities);

            var updatedAbilities = decorator.SetMaxBonuses(abilities, equipment);
            Assert.That(updatedAbilities, Is.EqualTo(modifiedAbilities));
        }

        [Test]
        public void LogEventsForSettingMaxModifiers()
        {
            var abilities = new Dictionary<string, Ability>();
            abilities["ability"] = new Ability("ability");
            abilities["ability"].BaseScore = 9266;
            abilities["other ability"] = new Ability("other ability");
            abilities["other ability"].BaseScore = 90210;

            var equipment = new Equipment();

            var modifiedAbilities = new Dictionary<string, Ability>();
            modifiedAbilities["ability"] = new Ability("ability");
            modifiedAbilities["ability"].BaseScore = 9266;
            modifiedAbilities["other ability"] = new Ability("other ability");
            modifiedAbilities["other ability"].BaseScore = 90210;

            mockInnerGenerator.Setup(g => g.SetMaxBonuses(abilities, equipment)).Returns(modifiedAbilities);

            var updatedAbilities = decorator.SetMaxBonuses(abilities, equipment);
            Assert.That(updatedAbilities, Is.EqualTo(modifiedAbilities));
            mockEventQueue.Verify(q => q.Enqueue(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(2));
            mockEventQueue.Verify(q => q.Enqueue("CreatureGen", $"Setting max modifiers for abilities"), Times.Once);
            mockEventQueue.Verify(q => q.Enqueue("CreatureGen", $"Set max modifiers"), Times.Once);
        }
    }
}
