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

        [TestCase(CreatureConstants.Human, 5 * 12 + 6)]
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

        [TestCase(CreatureConstants.AnimatedObject_Colossal, true)]
        [TestCase(CreatureConstants.AnimatedObject_Colossal_Flexible, false)]
        [TestCase(CreatureConstants.AnimatedObject_Colossal_MultipleLegs_Long, false)]
        [TestCase(CreatureConstants.AnimatedObject_Colossal_MultipleLegs_Long_Wooden, false)]
        [TestCase(CreatureConstants.AnimatedObject_Colossal_MultipleLegs_Tall, true)]
        [TestCase(CreatureConstants.AnimatedObject_Colossal_MultipleLegs_Tall_Wooden, true)]
        [TestCase(CreatureConstants.AnimatedObject_Colossal_Sheetlike, false)]
        [TestCase(CreatureConstants.AnimatedObject_Colossal_TwoLegs, true)]
        [TestCase(CreatureConstants.AnimatedObject_Colossal_TwoLegs_Wooden, true)]
        [TestCase(CreatureConstants.AnimatedObject_Colossal_Wheels_Wooden, false)]
        [TestCase(CreatureConstants.AnimatedObject_Colossal_Wooden, true)]
        [TestCase(CreatureConstants.AssassinVine, true)]
        [TestCase(CreatureConstants.Beholder, true)]
        [TestCase(CreatureConstants.Human, true)]
        [TestCase(CreatureConstants.LanternArchon, true)]
        [TestCase(CreatureConstants.Snake_Constrictor, false)]
        [TestCase(CreatureConstants.Wolf, false)]
        public void IsTall(string creature, bool expected)
        {
            var isTall = measurementHelper.IsTall(creature);
            Assert.That(isTall, Is.EqualTo(expected));
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
