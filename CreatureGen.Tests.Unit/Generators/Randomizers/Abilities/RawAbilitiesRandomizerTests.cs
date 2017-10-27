using CreatureGen.Abilities;
using CreatureGen.Domain.Generators.Randomizers.Abilities;
using CreatureGen.Randomizers.Abilities;
using Moq;
using NUnit.Framework;
using RollGen;

namespace CreatureGen.Tests.Unit.Generators.Randomizers.Abilities
{
    [TestFixture]
    public class RawAbilitiesRandomizerTests
    {
        private IAbilitiesRandomizer randomizer;
        private Mock<Dice> mockDice;

        [SetUp]
        public void Setup()
        {
            mockDice = new Mock<Dice>();
            var generator = new ConfigurableIterationGenerator(2);
            randomizer = new RawAbilitiesRandomizer(mockDice.Object, generator);

            mockDice.Setup(d => d.Roll(It.IsAny<int>()).d(It.IsAny<int>()).AsSum()).Returns(1);
        }

        [Test]
        public void RawAbilitiesCalls3d6PerStat()
        {
            var stats = randomizer.Randomize();
            mockDice.Verify(d => d.Roll(3).d(6).AsSum(), Times.Exactly(stats.Count));
        }

        [Test]
        public void RawAbilitiesReturnsUnmodified3d6PerStat()
        {
            mockDice.Setup(d => d.Roll(3).d(6).AsSum()).Returns(12);
            var stats = randomizer.Randomize();

            foreach (var stat in stats.Values)
                Assert.That(stat.BaseValue, Is.EqualTo(12));
        }

        [Test]
        public void RolledAbilitiesAreAlwaysAllowed()
        {
            mockDice.SetupSequence(d => d.Roll(3).d(6).AsSum())
                .Returns(9266).Returns(-42).Returns(int.MaxValue)
                .Returns(int.MinValue).Returns(0).Returns(1337);

            var stats = randomizer.Randomize();

            Assert.That(stats[AbilityConstants.Charisma].BaseValue, Is.EqualTo(9266));
            Assert.That(stats[AbilityConstants.Constitution].BaseValue, Is.EqualTo(-42));
            Assert.That(stats[AbilityConstants.Dexterity].BaseValue, Is.EqualTo(int.MaxValue));
            Assert.That(stats[AbilityConstants.Intelligence].BaseValue, Is.EqualTo(int.MinValue));
            Assert.That(stats[AbilityConstants.Strength].BaseValue, Is.EqualTo(0));
            Assert.That(stats[AbilityConstants.Wisdom].BaseValue, Is.EqualTo(1337));
        }
    }
}