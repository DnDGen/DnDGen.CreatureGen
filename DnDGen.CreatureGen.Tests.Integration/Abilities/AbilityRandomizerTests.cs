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

        [TestCase(AbilityConstants.RandomizerRolls.Average, 10, 13)]
        [TestCase(AbilityConstants.RandomizerRolls.BestOfFour, 3, 18)]
        [TestCase(AbilityConstants.RandomizerRolls.Default, 10, 11)]
        [TestCase(AbilityConstants.RandomizerRolls.Good, 13, 16)]
        [TestCase(AbilityConstants.RandomizerRolls.Heroic, 15, 18)]
        [TestCase(AbilityConstants.RandomizerRolls.OnesAsSixes, 6, 18)]
        [TestCase(AbilityConstants.RandomizerRolls.Poor, 3, 9)]
        [TestCase(AbilityConstants.RandomizerRolls.Raw, 3, 18)]
        [TestCase(AbilityConstants.RandomizerRolls.Wild, 2, 20)]
        public void AbililityRandomizerRangeIsCorrect(string roll, int expectedMin, int expectedMax)
        {
            var randomizer = new AbilityRandomizer(roll);
            var (min, max) = randomizer.GetRange(dice, string.Empty);
            Assert.That(min, Is.EqualTo(expectedMin));
            Assert.That(max, Is.EqualTo(expectedMax));
        }
    }
}
