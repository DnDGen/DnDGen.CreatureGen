using CreatureGen.Domain.Generators.Randomizers.Abilities;
using CreatureGen.Randomizers.Abilities;
using Moq;
using NUnit.Framework;
using RollGen;
using System.Linq;

namespace CreatureGen.Tests.Unit.Generators.Randomizers.Abilities
{
    [TestFixture]
    public class AverageAbilitiesRandomizerTests
    {
        private const int min = 10;
        private const int max = 12;
        private const int middle = (max + min) / 2;

        private IAbilitiesRandomizer randomizer;
        private Mock<Dice> mockDice;

        [SetUp]
        public void Setup()
        {
            var generator = new ConfigurableIterationGenerator(2);
            mockDice = new Mock<Dice>();
            mockDice.SetupSequence(d => d.Roll(3).d(6).AsSum())
                .Returns(min).Returns(max).Returns(middle)
                .Returns(min - 1).Returns(max + 1).Returns(middle);

            randomizer = new AverageAbilitiesRandomizer(mockDice.Object, generator);
        }

        [Test]
        public void AverageCalls3d6PerStat()
        {
            var stats = randomizer.Randomize();
            mockDice.Verify(d => d.Roll(3).d(6).AsSum(), Times.Exactly(stats.Count));
        }

        [Test]
        public void AllowIfStatAverageIsInRange()
        {
            var stats = randomizer.Randomize();
            var average = stats.Values.Average(s => s.BaseValue);
            Assert.That(average, Is.InRange(min, max));
        }

        [Test]
        public void RerollIfStatAverageIsGreaterThanTwelve()
        {
            mockDice.SetupSequence(d => d.Roll(3).d(6).AsSum())
                .Returns(min).Returns(max).Returns(middle)
                .Returns(min - 1).Returns(max + 1).Returns(18) //invalid average
                .Returns(min).Returns(max).Returns(middle)
                .Returns(min - 1).Returns(max + 1).Returns(middle); //valid average

            var stats = randomizer.Randomize();
            mockDice.Verify(d => d.Roll(3).d(6).AsSum(), Times.Exactly(stats.Count * 2));
        }

        [Test]
        public void RerollIfStatAverageIsLessThanTen()
        {
            mockDice.SetupSequence(d => d.Roll(3).d(6).AsSum())
                .Returns(min).Returns(max).Returns(middle)
                .Returns(min - 1).Returns(max + 1).Returns(3) //invalid average
                .Returns(min).Returns(max).Returns(middle)
                .Returns(min - 1).Returns(max + 1).Returns(middle); //valid average

            var stats = randomizer.Randomize();
            mockDice.Verify(d => d.Roll(3).d(6).AsSum(), Times.Exactly(stats.Count * 2));
        }

        [Test]
        public void DefaultValueIs10()
        {
            mockDice.Setup(d => d.Roll(3).d(6).AsSum()).Returns(9);

            var stats = randomizer.Randomize();

            foreach (var stat in stats.Values)
                Assert.That(stat.BaseValue, Is.EqualTo(10));
        }
    }
}