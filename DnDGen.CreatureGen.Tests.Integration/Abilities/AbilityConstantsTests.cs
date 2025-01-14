using DnDGen.CreatureGen.Abilities;
using DnDGen.RollGen;
using NUnit.Framework;

namespace DnDGen.CreatureGen.Tests.Integration.Abilities
{
    [TestFixture]
    public class AbilityConstantsTests : IntegrationTests
    {
        private Dice dice;

        [SetUp]
        public void Setup()
        {
            dice = GetNewInstanceOf<Dice>();
        }

        [TestCase(AbilityConstants.RandomizerRolls.Best, 16, 18)]
        [TestCase(AbilityConstants.RandomizerRolls.BestOfFour, 3, 18)]
        [TestCase(AbilityConstants.RandomizerRolls.Default, 10, 11)]
        [TestCase(AbilityConstants.RandomizerRolls.Good, 12, 15)]
        [TestCase(AbilityConstants.RandomizerRolls.OnesAsSixes, 6, 18)]
        [TestCase(AbilityConstants.RandomizerRolls.Poor, 4, 9)]
        [TestCase(AbilityConstants.RandomizerRolls.Raw, 3, 18)]
        [TestCase(AbilityConstants.RandomizerRolls.Wild, 2, 20)]
        public void RandomizerRoll_FitsRange(string roll, int lower, int upper)
        {
            Assert.That(dice.Roll(roll).AsPotentialMinimum(), Is.EqualTo(lower));
            Assert.That(dice.Roll(roll).AsPotentialMaximum(), Is.EqualTo(upper));
        }
    }
}