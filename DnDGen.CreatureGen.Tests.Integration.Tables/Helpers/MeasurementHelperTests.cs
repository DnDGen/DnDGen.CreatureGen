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

        [TestCase(CreatureConstants.AssassinVine, 20 * 12)]
        [TestCase(CreatureConstants.Bear_Black, 2 * 12 + 9)]
        [TestCase(CreatureConstants.Bear_Polar, 3 * 12 + 6)]
        [TestCase(CreatureConstants.Hippogriff, 0)]
        [TestCase(CreatureConstants.Human, 5 * 12 + 4)]
        [TestCase(CreatureConstants.Wolf, 29.5)]
        public void GetAverageHeight(string creature, double expected)
        {
            var height = measurementHelper.GetAverageHeight(creature);
            Assert.That(height, Is.EqualTo(expected));
        }

        [TestCase(CreatureConstants.AssassinVine, GenderConstants.Agender, 20 * 12)]
        [TestCase(CreatureConstants.Bear_Black, GenderConstants.Male, 3 * 12 + 2)]
        [TestCase(CreatureConstants.Bear_Black, GenderConstants.Female, 2 * 12 + 9)]
        [TestCase(CreatureConstants.Bear_Polar, GenderConstants.Male, 4 * 12 + 5)]
        [TestCase(CreatureConstants.Bear_Polar, GenderConstants.Female, 3 * 12 + 6)]
        [TestCase(CreatureConstants.Hippogriff, GenderConstants.Male, 0)]
        [TestCase(CreatureConstants.Hippogriff, GenderConstants.Female, 0)]
        [TestCase(CreatureConstants.Human, GenderConstants.Male, 5 * 12 + 9)]
        [TestCase(CreatureConstants.Human, GenderConstants.Female, 5 * 12 + 4)]
        [TestCase(CreatureConstants.Wolf, GenderConstants.Male, 29.5)]
        [TestCase(CreatureConstants.Wolf, GenderConstants.Female, 29.5)]
        public void GetAverageHeight_WithGender(string creature, string gender, double expected)
        {
            var height = measurementHelper.GetAverageHeight(creature, gender);
            Assert.That(height, Is.EqualTo(expected));
        }

        [TestCase(CreatureConstants.AssassinVine, 3.5)]
        [TestCase(CreatureConstants.Bear_Black, 4 * 12 + 6.5)]
        [TestCase(CreatureConstants.Bear_Polar, 8 * 12 + 6)]
        [TestCase(CreatureConstants.Hippogriff, 8 * 12 + 11.5)]
        [TestCase(CreatureConstants.Human, 0)]
        [TestCase(CreatureConstants.Wolf, 56)]
        public void GetAverageLength(string creature, double expected)
        {
            var length = measurementHelper.GetAverageLength(creature);
            Assert.That(length, Is.EqualTo(expected));
        }

        [TestCase(CreatureConstants.AssassinVine, GenderConstants.Agender, 3.5)]
        [TestCase(CreatureConstants.Bear_Black, GenderConstants.Male, 4 * 12 + 6.5)]
        [TestCase(CreatureConstants.Bear_Black, GenderConstants.Female, 4 * 12 + 6.5)]
        [TestCase(CreatureConstants.Bear_Polar, GenderConstants.Male, 9 * 12 + 2)]
        [TestCase(CreatureConstants.Bear_Polar, GenderConstants.Female, 8 * 12 + 6)]
        [TestCase(CreatureConstants.Hippogriff, GenderConstants.Male, 8 * 12 + 11.5)]
        [TestCase(CreatureConstants.Hippogriff, GenderConstants.Female, 8 * 12 + 11.5)]
        [TestCase(CreatureConstants.Human, GenderConstants.Male, 0)]
        [TestCase(CreatureConstants.Human, GenderConstants.Female, 0)]
        [TestCase(CreatureConstants.Wolf, GenderConstants.Male, 56)]
        [TestCase(CreatureConstants.Wolf, GenderConstants.Female, 56)]
        public void GetAverageLength_WithGender(string creature, string gender, double expected)
        {
            var length = measurementHelper.GetAverageLength(creature, gender);
            Assert.That(length, Is.EqualTo(expected));
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
        [TestCase(CreatureConstants.Nightwing, false)]
        [TestCase(CreatureConstants.ShamblingMound, true)]
        [TestCase(CreatureConstants.Snake_Constrictor, false)]
        [TestCase(CreatureConstants.Wolf, false)]
        public void IsTall(string creature, bool expected)
        {
            var isTall = measurementHelper.IsTall(creature);
            Assert.That(isTall, Is.EqualTo(expected));
        }

        [TestCase(CreatureConstants.Human, 85)]
        [TestCase(CreatureConstants.Wolf, 48.75)]
        public void GetAverageWeight(string creature, double expected)
        {
            var weight = measurementHelper.GetAverageWeight(creature);
            Assert.That(weight, Is.EqualTo(expected));
        }

        [TestCase(CreatureConstants.Human, GenderConstants.Male, 120)]
        [TestCase(CreatureConstants.Human, GenderConstants.Female, 85)]
        [TestCase(CreatureConstants.Wolf, GenderConstants.Male, 48.75)]
        [TestCase(CreatureConstants.Wolf, GenderConstants.Female, 48.75)]
        public void GetAverageWeight_WithGender(string creature, string gender, double expected)
        {
            var weight = measurementHelper.GetAverageWeight(creature, gender);
            Assert.That(weight, Is.EqualTo(expected));
        }
    }
}
