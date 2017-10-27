using CreatureGen.Creatures;
using NUnit.Framework;

namespace CreatureGen.Tests.Unit.Generators.Races
{
    [TestFixture]
    public class MeasurementTests
    {
        private Measurement measurement;

        [SetUp]
        public void Setup()
        {
            measurement = new Measurement("my unit");
        }

        [Test]
        public void MeasurementInitialized()
        {
            Assert.That(measurement.Description, Is.Empty);
            Assert.That(measurement.Unit, Is.EqualTo("my unit"));
            Assert.That(measurement.Value, Is.EqualTo(0));
        }
    }
}
