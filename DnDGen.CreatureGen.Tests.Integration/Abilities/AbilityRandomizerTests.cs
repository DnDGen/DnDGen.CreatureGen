using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Generators.Abilities;
using DnDGen.RollGen;
using NUnit.Framework;

namespace DnDGen.CreatureGen.Tests.Integration.Abilities
{
    internal class AbilityRandomizerTests : IntegrationTests
    {
        private Dice dice;

        [SetUp]
        public void Setup()
        {
            dice = GetNewInstanceOf<Dice>();
        }

        [Test]
        public void DefaultAbilityRandomizerIsValid()
        {
            var randomizer = new AbilityRandomizer();
            var valid = randomizer.Validate(dice);
            Assert.That(valid, Is.True);
        }

        [TestCase(AbilityConstants.RandomizerRolls.Average)]
        [TestCase(AbilityConstants.RandomizerRolls.BestOfFour)]
        [TestCase(AbilityConstants.RandomizerRolls.Default)]
        [TestCase(AbilityConstants.RandomizerRolls.Good)]
        [TestCase(AbilityConstants.RandomizerRolls.Heroic)]
        [TestCase(AbilityConstants.RandomizerRolls.OnesAsSixes)]
        [TestCase(AbilityConstants.RandomizerRolls.Poor)]
        [TestCase(AbilityConstants.RandomizerRolls.Raw)]
        [TestCase(AbilityConstants.RandomizerRolls.Wild)]
        public void AbililityRandomizerRollConstantIsValid(string roll)
        {
            var randomizer = new AbilityRandomizer(roll);
            var valid = randomizer.Validate(dice);
            Assert.That(valid, Is.True);
        }
    }
}
