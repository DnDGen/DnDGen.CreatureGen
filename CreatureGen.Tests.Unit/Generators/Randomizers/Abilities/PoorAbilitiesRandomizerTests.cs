using CreatureGen.Domain.Generators.Randomizers.Abilities;
using CreatureGen.Randomizers.Abilities;
using DnDGen.Core.Generators;
using Moq;
using NUnit.Framework;
using RollGen;
using System.Linq;

namespace CreatureGen.Tests.Unit.Generators.Randomizers.Abilities
{
    [TestFixture]
    public class PoorAbilitiesRandomizerTests
    {
        private const int min = 3;
        private const int max = 9;
        private const int middle = (max + min) / 2;

        private IAbilitiesRandomizer randomizer;
        private Mock<Dice> mockDice;
        private Generator generator;

        [SetUp]
        public void Setup()
        {
            generator = new ConfigurableIterationGenerator(2);
            mockDice = new Mock<Dice>();
            mockDice.SetupSequence(d => d.Roll(3).d(6).AsSum())
                .Returns(min).Returns(max).Returns(middle)
                .Returns(min - 1).Returns(max + 1).Returns(middle);

            randomizer = new PoorAbilitiesRandomizer(mockDice.Object, generator);
        }

        [Test]
        public void PoorCalls3d6PerStat()
        {
            var stats = randomizer.Randomize();
            mockDice.Verify(d => d.Roll(3).d(6).AsSum(), Times.Exactly(stats.Count));
        }

        [Test]
        public void AllowIfStatAverageIsLessThanTen()
        {
            var stats = randomizer.Randomize();
            var average = stats.Values.Average(s => s.BaseValue);
            Assert.That(average, Is.InRange(min, max));
        }

        [Test]
        public void RerollIfStatAverageIsNotLessThanTen()
        {
            mockDice.SetupSequence(d => d.Roll(3).d(6).AsSum())
                .Returns(min).Returns(max).Returns(middle)
                .Returns(min - 1).Returns(max + 1).Returns(9000) //invalid average
                .Returns(min).Returns(max).Returns(middle)
                .Returns(min - 1).Returns(max + 1).Returns(middle); //valid average

            var stats = randomizer.Randomize();
            mockDice.Verify(d => d.Roll(3).d(6).AsSum(), Times.Exactly(stats.Count * 2));
        }

        [Test]
        public void DefaultValueIs9()
        {
            mockDice.Setup(d => d.Roll(3).d(6).AsSum()).Returns(19);

            var stats = randomizer.Randomize();

            foreach (var stat in stats.Values)
                Assert.That(stat.BaseValue, Is.EqualTo(9));
        }
    }
}