using DnDGen.CreatureGen.Creatures;
using NUnit.Framework;

namespace DnDGen.CreatureGen.Tests.Unit.Generators.Creatures
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
            Assert.That(measurement.Value, Is.Zero);
            Assert.That(measurement.Bonuses, Is.Empty);
        }

        [Test]
        public void AddBonus_Positive()
        {
            measurement.AddBonus(9266, "my condition");
            Assert.That(measurement.Bonuses, Has.Count.EqualTo(1));
            Assert.That(measurement.Bonuses[0].Value, Is.EqualTo(9266));
            Assert.That(measurement.Bonuses[0].Condition, Is.EqualTo("my condition"));
            Assert.That(measurement.Bonuses[0].IsConditional, Is.True);
        }

        [Test]
        public void AddBonus_Negative()
        {
            measurement.AddBonus(-90210, "my condition");
            Assert.That(measurement.Bonuses, Has.Count.EqualTo(1));
            Assert.That(measurement.Bonuses[0].Value, Is.EqualTo(-90210));
            Assert.That(measurement.Bonuses[0].Condition, Is.EqualTo("my condition"));
            Assert.That(measurement.Bonuses[0].IsConditional, Is.True);
        }

        [Test]
        public void AddBonus_WithExisting()
        {
            measurement.AddBonus(9266, "my condition");
            measurement.AddBonus(90210, "my other condition");

            Assert.That(measurement.Bonuses, Has.Count.EqualTo(2));
            Assert.That(measurement.Bonuses[0].Value, Is.EqualTo(9266));
            Assert.That(measurement.Bonuses[0].Condition, Is.EqualTo("my condition"));
            Assert.That(measurement.Bonuses[0].IsConditional, Is.True);
            Assert.That(measurement.Bonuses[1].Value, Is.EqualTo(90210));
            Assert.That(measurement.Bonuses[1].Condition, Is.EqualTo("my other condition"));
            Assert.That(measurement.Bonuses[1].IsConditional, Is.True);
        }

        [Test]
        public void ToString_ReturnsDescription()
        {
            measurement.Value = 9266;
            measurement.Description = "my description";

            Assert.That(measurement.ToString(), Is.EqualTo("9266 my unit (my description)"));
        }
    }
}
