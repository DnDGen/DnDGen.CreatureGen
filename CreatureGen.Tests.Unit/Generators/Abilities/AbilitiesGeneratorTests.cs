using CreatureGen.Generators.Abilities;
using CreatureGen.Selectors.Collections;
using CreatureGen.Selectors.Selections;
using CreatureGen.Tables;
using Moq;
using NUnit.Framework;
using RollGen;
using System.Collections.Generic;

namespace CreatureGen.Tests.Unit.Generators.Abilities
{
    [TestFixture]
    public class AbilitiesGeneratorTests
    {
        private Mock<ITypeAndAmountSelector> mockTypeAndAmountSelector;
        private Mock<Dice> mockDice;
        private IAbilitiesGenerator abilitiesGenerator;
        private List<TypeAndAmountSelection> abilitySelections;
        private Mock<PartialRoll> mockPartialTotal;

        [SetUp]
        public void Setup()
        {
            mockTypeAndAmountSelector = new Mock<ITypeAndAmountSelector>();
            mockDice = new Mock<Dice>();
            abilitiesGenerator = new AbilitiesGenerator(mockTypeAndAmountSelector.Object, mockDice.Object);

            abilitySelections = new List<TypeAndAmountSelection>();
            mockPartialTotal = new Mock<PartialRoll>();

            abilitySelections.Add(new TypeAndAmountSelection { Type = "ability", Amount = 0 });
            abilitySelections.Add(new TypeAndAmountSelection { Type = "other ability", Amount = 9266 });
            abilitySelections.Add(new TypeAndAmountSelection { Type = "last ability", Amount = -90210 });

            mockTypeAndAmountSelector.Setup(s => s.Select(TableNameConstants.Set.Collection.AbilityGroups, "creature name")).Returns(abilitySelections);

            var mockPartialDie = new Mock<PartialRoll>();
            mockDice.Setup(d => d.Roll(3)).Returns(mockPartialDie.Object);
            mockPartialDie.Setup(d => d.d(6)).Returns(mockPartialTotal.Object);

            mockPartialTotal.SetupSequence(d => d.AsSum()).Returns(42).Returns(600).Returns(1337);
        }

        [Test]
        public void GetAbilitiesFromSelections()
        {
            var abilities = abilitiesGenerator.GenerateFor("creature name");
            Assert.That(abilities["ability"].Name, Is.EqualTo("ability"));
            Assert.That(abilities["ability"].RacialAdjustment, Is.EqualTo(0));
            Assert.That(abilities["other ability"].Name, Is.EqualTo("other ability"));
            Assert.That(abilities["other ability"].RacialAdjustment, Is.EqualTo(9266));
            Assert.That(abilities["last ability"].Name, Is.EqualTo("last ability"));
            Assert.That(abilities["last ability"].RacialAdjustment, Is.EqualTo(-90210));
        }

        [Test]
        public void RollBaseValuesForAbilities()
        {
            var abilities = abilitiesGenerator.GenerateFor("creature name");
            Assert.That(abilities["ability"].Name, Is.EqualTo("ability"));
            Assert.That(abilities["ability"].BaseValue, Is.EqualTo(42));
            Assert.That(abilities["ability"].RacialAdjustment, Is.EqualTo(0));
            Assert.That(abilities["ability"].FullValue, Is.EqualTo(42));
            Assert.That(abilities["other ability"].Name, Is.EqualTo("other ability"));
            Assert.That(abilities["other ability"].BaseValue, Is.EqualTo(600));
            Assert.That(abilities["other ability"].RacialAdjustment, Is.EqualTo(9266));
            Assert.That(abilities["other ability"].FullValue, Is.EqualTo(9866));
            Assert.That(abilities["last ability"].Name, Is.EqualTo("last ability"));
            Assert.That(abilities["last ability"].BaseValue, Is.EqualTo(1337));
            Assert.That(abilities["last ability"].RacialAdjustment, Is.EqualTo(-90210));
            Assert.That(abilities["last ability"].FullValue, Is.EqualTo(1));
        }
    }
}