using CreatureGen.Feats;
using NUnit.Framework;

namespace CreatureGen.Tests.Unit.Feats
{
    [TestFixture]
    public class FrequencyTests
    {
        private Frequency frequency;

        [SetUp]
        public void Setup()
        {
            frequency = new Frequency();
        }

        [Test]
        public void FrequencyInitialized()
        {
            Assert.That(frequency.Quantity, Is.Zero);
            Assert.That(frequency.TimePeriod, Is.Empty);
        }
    }
}