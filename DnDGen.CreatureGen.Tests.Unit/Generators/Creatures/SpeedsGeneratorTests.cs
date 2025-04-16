using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Generators.Creatures;
using DnDGen.CreatureGen.Tables;
using DnDGen.Infrastructure.Models;
using DnDGen.Infrastructure.Selectors.Collections;
using Moq;
using NUnit.Framework;

namespace DnDGen.CreatureGen.Tests.Unit.Generators.Creatures
{
    [TestFixture]
    public class SpeedsGeneratorTests
    {
        private ISpeedsGenerator speedsGenerator;
        private Mock<ICollectionTypeAndAmountSelector> mockTypeAndAmountSelector;
        private Mock<ICollectionSelector> mockCollectionSelector;

        [SetUp]
        public void Setup()
        {
            mockTypeAndAmountSelector = new Mock<ICollectionTypeAndAmountSelector>();
            mockCollectionSelector = new Mock<ICollectionSelector>();
            speedsGenerator = new SpeedsGenerator(mockTypeAndAmountSelector.Object, mockCollectionSelector.Object);
        }

        [Test]
        public void GenerateCreatureSpeeds()
        {
            var speedSelections = new[]
            {
                new TypeAndAmountDataSelection { Type = "on foot", AmountAsDouble = 1234 },
                new TypeAndAmountDataSelection { Type = "in a car", AmountAsDouble = 2345 },
            };

            mockTypeAndAmountSelector.Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.Speeds, "creature")).Returns(speedSelections);

            var speeds = speedsGenerator.Generate("creature");
            Assert.That(speeds["on foot"].Unit, Is.EqualTo("feet per round"));
            Assert.That(speeds["on foot"].Value, Is.EqualTo(1234));
            Assert.That(speeds["on foot"].Description, Is.Empty);
            Assert.That(speeds["in a car"].Unit, Is.EqualTo("feet per round"));
            Assert.That(speeds["in a car"].Value, Is.EqualTo(2345));
            Assert.That(speeds["in a car"].Description, Is.Empty);
            Assert.That(speeds.Count, Is.EqualTo(2));
        }

        [Test]
        public void GenerateCreatureAerialSpeedAndDescription()
        {
            var speedSelections = new[]
            {
                new TypeAndAmountDataSelection { Type = "on foot", AmountAsDouble = 1234 },
                new TypeAndAmountDataSelection { Type = SpeedConstants.Fly, AmountAsDouble = 2345 },
            };

            mockTypeAndAmountSelector.Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.Speeds, "creature")).Returns(speedSelections);
            mockCollectionSelector.Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AerialManeuverability, "creature")).Returns(["maneuverability"]);

            var speeds = speedsGenerator.Generate("creature");
            Assert.That(speeds["on foot"].Unit, Is.EqualTo("feet per round"));
            Assert.That(speeds["on foot"].Value, Is.EqualTo(1234));
            Assert.That(speeds["on foot"].Description, Is.Empty);
            Assert.That(speeds[SpeedConstants.Fly].Unit, Is.EqualTo("feet per round"));
            Assert.That(speeds[SpeedConstants.Fly].Value, Is.EqualTo(2345));
            Assert.That(speeds[SpeedConstants.Fly].Description, Is.EqualTo("maneuverability"));
            Assert.That(speeds.Count, Is.EqualTo(2));
        }
    }
}
