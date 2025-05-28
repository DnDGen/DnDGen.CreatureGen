using DnDGen.CreatureGen.Creatures;
using NUnit.Framework;

namespace DnDGen.CreatureGen.Tests.Integration.Tables.Helpers
{
    [TestFixture]
    public class MeasurementHelperTests : IntegrationTests
    {
        private MeasurementHelper measurementHelper;

        [SetUp]
        public void Setup()
        {
            measurementHelper = GetNewInstanceOf<MeasurementHelper>();
        }

        [Test]
        public void GetAverageHeight(string creature, double expected)
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void GetAverageHeight_WithGender(string creature, string gender, double expected)
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void GetAverageLength(string creature, double expected)
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void GetAverageLength_WithGender(string creature, string gender, double expected)
        {
            Assert.Fail("not yet written");
        }

        [TestCase(CreatureConstants.Human, true)]
        [TestCase(CreatureConstants.Wolf, false)]
        [TestCase(CreatureConstants.Beholder, true)]
        [TestCase(CreatureConstants.LanternArchon, true)]
        [TestCase(CreatureConstants.Snake_Constrictor, false)]
        [TestCase(CreatureConstants.AnimatedObject_Colossal_MultipleLegs_Long, false)]
        [TestCase(CreatureConstants.AnimatedObject_Colossal_MultipleLegs_Tall, true)]
        public void IsTall(string creature, bool expected)
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void GetAverageWeight(string creature, double expected)
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void GetAverageWeight_WithGender(string creature, string gender, double expected)
        {
            Assert.Fail("not yet written");
        }
    }
}
