using CreatureGen.Domain.Generators.Randomizers.Abilities;
using CreatureGen.Randomizers.Abilities;
using Moq;
using NUnit.Framework;
using RollGen;
using System.Linq;

namespace CreatureGen.Tests.Unit.Generators.Randomizers.Abilities
{
    [TestFixture]
    public class OnesAsSixesAbilitiesRandomizerTests
    {
        private IAbilitiesRandomizer randomizer;
        private Mock<Dice> mockDice;

        [SetUp]
        public void Setup()
        {
            mockDice = new Mock<Dice>();
            var generator = new ConfigurableIterationGenerator(2);
            randomizer = new OnesAsSixesAbilitiesRandomizer(mockDice.Object, generator);

            mockDice.Setup(d => d.Roll(3).d(6).AsIndividualRolls()).Returns(new[] { 2, 3, 4 });
        }

        [Test]
        public void OnesAsSixesAbilitiesCalls3d6OncePerAbility()
        {
            var abilities = randomizer.Randomize();
            mockDice.Verify(d => d.Roll(3).d(6).AsIndividualRolls(), Times.Exactly(abilities.Count));
        }

        [Test]
        public void OnesAsSixesTreatsOnesAsSixes()
        {
            mockDice.Setup(d => d.Roll(3).d(6).AsIndividualRolls()).Returns(new[] { 2, 3, 1 });

            var abilities = randomizer.Randomize();
            var ability = abilities.Values.First();
            Assert.That(ability.BaseValue, Is.EqualTo(11));
        }
    }
}