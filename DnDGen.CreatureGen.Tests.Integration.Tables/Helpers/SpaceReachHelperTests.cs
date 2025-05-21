using DnDGen.CreatureGen.Creatures;
using NUnit.Framework;

namespace DnDGen.CreatureGen.Tests.Integration.Tables.Helpers
{
    [TestFixture]
    public class SpaceReachHelperTests : IntegrationTests
    {
        private SpaceReachHelper spaceReachHelper;

        [SetUp]
        public void Setup()
        {
            spaceReachHelper = GetNewInstanceOf<SpaceReachHelper>();
        }

        [TestCase(SizeConstants.Fine, 0.5)]
        [TestCase(SizeConstants.Diminutive, 1)]
        [TestCase(SizeConstants.Tiny, 2.5)]
        [TestCase(SizeConstants.Small, 5)]
        [TestCase(SizeConstants.Medium, 5)]
        [TestCase(SizeConstants.Large, 10)]
        [TestCase(SizeConstants.Huge, 15)]
        [TestCase(SizeConstants.Gargantuan, 20)]
        [TestCase(SizeConstants.Colossal, 30)]
        public void GetDefaultSpace_ReturnsSpace(string size, double expected)
        {
            var space = spaceReachHelper.GetDefaultSpace(size);
            Assert.That(space, Is.EqualTo(expected));
        }

        [TestCase(SizeConstants.Fine, 0)]
        [TestCase(SizeConstants.Diminutive, 0)]
        [TestCase(SizeConstants.Tiny, 0)]
        [TestCase(SizeConstants.Small, 5)]
        [TestCase(SizeConstants.Medium, 5)]
        [TestCase(SizeConstants.Large, 10)]
        [TestCase(SizeConstants.Huge, 15)]
        [TestCase(SizeConstants.Gargantuan, 20)]
        [TestCase(SizeConstants.Colossal, 30)]
        public void GetDefaultReach_ReturnsReach_Tall(string size, double expected)
        {
            var reach = spaceReachHelper.GetDefaultReach(CreatureConstants.Human, size);
            Assert.That(reach, Is.EqualTo(expected));
        }

        [TestCase(SizeConstants.Fine, 0)]
        [TestCase(SizeConstants.Diminutive, 0)]
        [TestCase(SizeConstants.Tiny, 0)]
        [TestCase(SizeConstants.Small, 5)]
        [TestCase(SizeConstants.Medium, 5)]
        [TestCase(SizeConstants.Large, 5)]
        [TestCase(SizeConstants.Huge, 10)]
        [TestCase(SizeConstants.Gargantuan, 15)]
        [TestCase(SizeConstants.Colossal, 20)]
        public void GetDefaultReach_ReturnsReach_Long(string size, double expected)
        {
            var reach = spaceReachHelper.GetDefaultReach(CreatureConstants.Wolf, size);
            Assert.That(reach, Is.EqualTo(expected));
        }

        [TestCase(SizeConstants.Fine)]
        [TestCase(SizeConstants.Diminutive)]
        [TestCase(SizeConstants.Tiny)]
        [TestCase(SizeConstants.Small)]
        [TestCase(SizeConstants.Medium)]
        [TestCase(SizeConstants.Large)]
        [TestCase(SizeConstants.Huge)]
        [TestCase(SizeConstants.Gargantuan)]
        [TestCase(SizeConstants.Colossal)]
        [TestCase("my size")]
        public void GetAdvancedReach_SameSize(string size)
        {
            var advancedReach = spaceReachHelper.GetAdvancedReach("my creature", size, 926.6, size);
            Assert.That(advancedReach, Is.EqualTo(926.6));
        }

        [TestCase(SizeConstants.Fine, 0, SizeConstants.Diminutive, 0)]
        [TestCase(SizeConstants.Fine, 0, SizeConstants.Tiny, 0)]
        [TestCase(SizeConstants.Fine, 0, SizeConstants.Small, 5)]
        [TestCase(SizeConstants.Fine, 0, SizeConstants.Medium, 5)]
        [TestCase(SizeConstants.Fine, 0, SizeConstants.Large, 10)]
        [TestCase(SizeConstants.Fine, 0, SizeConstants.Huge, 15)]
        [TestCase(SizeConstants.Fine, 0, SizeConstants.Gargantuan, 20)]
        [TestCase(SizeConstants.Fine, 0, SizeConstants.Colossal, 30)]
        [TestCase(SizeConstants.Diminutive, 0, SizeConstants.Tiny, 0)]
        [TestCase(SizeConstants.Diminutive, 0, SizeConstants.Small, 5)]
        [TestCase(SizeConstants.Diminutive, 0, SizeConstants.Medium, 5)]
        [TestCase(SizeConstants.Diminutive, 0, SizeConstants.Large, 10)]
        [TestCase(SizeConstants.Diminutive, 0, SizeConstants.Huge, 15)]
        [TestCase(SizeConstants.Diminutive, 0, SizeConstants.Gargantuan, 20)]
        [TestCase(SizeConstants.Diminutive, 0, SizeConstants.Colossal, 30)]
        [TestCase(SizeConstants.Tiny, 0, SizeConstants.Small, 5)]
        [TestCase(SizeConstants.Tiny, 0, SizeConstants.Medium, 5)]
        [TestCase(SizeConstants.Tiny, 0, SizeConstants.Large, 10)]
        [TestCase(SizeConstants.Tiny, 0, SizeConstants.Huge, 15)]
        [TestCase(SizeConstants.Tiny, 0, SizeConstants.Gargantuan, 20)]
        [TestCase(SizeConstants.Tiny, 0, SizeConstants.Colossal, 30)]
        [TestCase(SizeConstants.Small, 5, SizeConstants.Medium, 5)]
        [TestCase(SizeConstants.Small, 5, SizeConstants.Large, 10)]
        [TestCase(SizeConstants.Small, 5, SizeConstants.Huge, 15)]
        [TestCase(SizeConstants.Small, 5, SizeConstants.Gargantuan, 20)]
        [TestCase(SizeConstants.Small, 5, SizeConstants.Colossal, 30)]
        [TestCase(SizeConstants.Medium, 5, SizeConstants.Large, 10)]
        [TestCase(SizeConstants.Medium, 5, SizeConstants.Huge, 15)]
        [TestCase(SizeConstants.Medium, 5, SizeConstants.Gargantuan, 20)]
        [TestCase(SizeConstants.Medium, 5, SizeConstants.Colossal, 30)]
        [TestCase(SizeConstants.Large, 10, SizeConstants.Huge, 15)]
        [TestCase(SizeConstants.Large, 10, SizeConstants.Gargantuan, 20)]
        [TestCase(SizeConstants.Large, 10, SizeConstants.Colossal, 30)]
        [TestCase(SizeConstants.Huge, 15, SizeConstants.Gargantuan, 20)]
        [TestCase(SizeConstants.Huge, 15, SizeConstants.Colossal, 30)]
        [TestCase(SizeConstants.Gargantuan, 20, SizeConstants.Colossal, 30)]
        public void GetAdvancedReach_DefaultIncrease_Tall(string size, double originalReach, string advancedSize, double expectedReach)
        {
            var advancedReach = spaceReachHelper.GetAdvancedReach(CreatureConstants.Human, size, originalReach, advancedSize);
            Assert.That(advancedReach, Is.EqualTo(expectedReach));
        }

        [TestCase(SizeConstants.Fine, 0, SizeConstants.Diminutive, 0)]
        [TestCase(SizeConstants.Fine, 0, SizeConstants.Tiny, 0)]
        [TestCase(SizeConstants.Fine, 0, SizeConstants.Small, 5)]
        [TestCase(SizeConstants.Fine, 0, SizeConstants.Medium, 5)]
        [TestCase(SizeConstants.Fine, 0, SizeConstants.Large, 5)]
        [TestCase(SizeConstants.Fine, 0, SizeConstants.Huge, 10)]
        [TestCase(SizeConstants.Fine, 0, SizeConstants.Gargantuan, 15)]
        [TestCase(SizeConstants.Fine, 0, SizeConstants.Colossal, 20)]
        [TestCase(SizeConstants.Diminutive, 0, SizeConstants.Tiny, 0)]
        [TestCase(SizeConstants.Diminutive, 0, SizeConstants.Small, 5)]
        [TestCase(SizeConstants.Diminutive, 0, SizeConstants.Medium, 5)]
        [TestCase(SizeConstants.Diminutive, 0, SizeConstants.Large, 5)]
        [TestCase(SizeConstants.Diminutive, 0, SizeConstants.Huge, 10)]
        [TestCase(SizeConstants.Diminutive, 0, SizeConstants.Gargantuan, 15)]
        [TestCase(SizeConstants.Diminutive, 0, SizeConstants.Colossal, 20)]
        [TestCase(SizeConstants.Tiny, 0, SizeConstants.Small, 5)]
        [TestCase(SizeConstants.Tiny, 0, SizeConstants.Medium, 5)]
        [TestCase(SizeConstants.Tiny, 0, SizeConstants.Large, 5)]
        [TestCase(SizeConstants.Tiny, 0, SizeConstants.Huge, 10)]
        [TestCase(SizeConstants.Tiny, 0, SizeConstants.Gargantuan, 15)]
        [TestCase(SizeConstants.Tiny, 0, SizeConstants.Colossal, 20)]
        [TestCase(SizeConstants.Small, 5, SizeConstants.Medium, 5)]
        [TestCase(SizeConstants.Small, 5, SizeConstants.Large, 5)]
        [TestCase(SizeConstants.Small, 5, SizeConstants.Huge, 10)]
        [TestCase(SizeConstants.Small, 5, SizeConstants.Gargantuan, 15)]
        [TestCase(SizeConstants.Small, 5, SizeConstants.Colossal, 20)]
        [TestCase(SizeConstants.Medium, 5, SizeConstants.Large, 5)]
        [TestCase(SizeConstants.Medium, 5, SizeConstants.Huge, 10)]
        [TestCase(SizeConstants.Medium, 5, SizeConstants.Gargantuan, 15)]
        [TestCase(SizeConstants.Medium, 5, SizeConstants.Colossal, 20)]
        [TestCase(SizeConstants.Large, 5, SizeConstants.Huge, 10)]
        [TestCase(SizeConstants.Large, 5, SizeConstants.Gargantuan, 15)]
        [TestCase(SizeConstants.Large, 5, SizeConstants.Colossal, 20)]
        [TestCase(SizeConstants.Huge, 10, SizeConstants.Gargantuan, 15)]
        [TestCase(SizeConstants.Huge, 10, SizeConstants.Colossal, 20)]
        [TestCase(SizeConstants.Gargantuan, 15, SizeConstants.Colossal, 20)]
        public void GetAdvancedReach_DefaultIncrease_Long(string size, double originalReach, string advancedSize, double expectedReach)
        {
            var advancedReach = spaceReachHelper.GetAdvancedReach(CreatureConstants.Wolf, size, originalReach, advancedSize);
            Assert.That(advancedReach, Is.EqualTo(expectedReach));
        }

        [TestCase(SizeConstants.Large, 20)]
        [TestCase(SizeConstants.Huge, 30)]
        [TestCase(SizeConstants.Gargantuan, 40)]
        [TestCase(SizeConstants.Colossal, 60)]
        public void GetAdvancedReach_CustomIncrease_HigherThanNormal_Tall(string advancedSize, double expectedReach)
        {
            var advancedReach = spaceReachHelper.GetAdvancedReach(CreatureConstants.Human, SizeConstants.Medium, 10, advancedSize);
            Assert.That(advancedReach, Is.EqualTo(expectedReach));
        }

        [TestCase(SizeConstants.Large, 10)]
        [TestCase(SizeConstants.Huge, 20)]
        [TestCase(SizeConstants.Gargantuan, 30)]
        [TestCase(SizeConstants.Colossal, 40)]
        public void GetAdvancedReach_CustomIncrease_HigherThanNormal_Long(string advancedSize, double expectedReach)
        {
            var advancedReach = spaceReachHelper.GetAdvancedReach(CreatureConstants.Wolf, SizeConstants.Medium, 10, advancedSize);
            Assert.That(advancedReach, Is.EqualTo(expectedReach));
        }

        [TestCase(SizeConstants.Large, 5)]
        [TestCase(SizeConstants.Huge, 7.5)]
        [TestCase(SizeConstants.Gargantuan, 10)]
        [TestCase(SizeConstants.Colossal, 15)]
        public void GetAdvancedReach_CustomIncrease_LowerThanNormal_Tall(string advancedSize, double expectedReach)
        {
            var advancedReach = spaceReachHelper.GetAdvancedReach(CreatureConstants.Human, SizeConstants.Medium, 2.5, advancedSize);
            Assert.That(advancedReach, Is.EqualTo(expectedReach));
        }

        [TestCase(SizeConstants.Large, 2.5)]
        [TestCase(SizeConstants.Huge, 5)]
        [TestCase(SizeConstants.Gargantuan, 7.5)]
        [TestCase(SizeConstants.Colossal, 10)]
        public void GetAdvancedReach_CustomIncrease_LowerThanNormal_Long(string advancedSize, double expectedReach)
        {
            var advancedReach = spaceReachHelper.GetAdvancedReach(CreatureConstants.Wolf, SizeConstants.Medium, 2.5, advancedSize);
            Assert.That(advancedReach, Is.EqualTo(expectedReach));
        }

        [Test]
        public void GetAdvancedReach_CustomIncrease_None_Tall()
        {
            var advancedReach = spaceReachHelper.GetAdvancedReach(CreatureConstants.Human, SizeConstants.Medium, 0, SizeConstants.Colossal);
            Assert.That(advancedReach, Is.Zero);
        }

        [Test]
        public void GetAdvancedReach_CustomIncrease_None_Long()
        {
            var advancedReach = spaceReachHelper.GetAdvancedReach(CreatureConstants.Wolf, SizeConstants.Medium, 0, SizeConstants.Colossal);
            Assert.That(advancedReach, Is.Zero);
        }

        [TestCase(SizeConstants.Fine)]
        [TestCase(SizeConstants.Diminutive)]
        [TestCase(SizeConstants.Tiny)]
        [TestCase(SizeConstants.Small)]
        [TestCase(SizeConstants.Medium)]
        [TestCase(SizeConstants.Large)]
        [TestCase(SizeConstants.Huge)]
        [TestCase(SizeConstants.Gargantuan)]
        [TestCase(SizeConstants.Colossal)]
        public void GetAdvancedSpace_SameSize(string size)
        {
            var advancedReach = spaceReachHelper.GetAdvancedSpace(size, 926.6, size);
            Assert.That(advancedReach, Is.EqualTo(926.6));
        }

        [TestCase(SizeConstants.Fine, 0.5, SizeConstants.Diminutive, 1)]
        [TestCase(SizeConstants.Fine, 0.5, SizeConstants.Tiny, 2.5)]
        [TestCase(SizeConstants.Fine, 0.5, SizeConstants.Small, 5)]
        [TestCase(SizeConstants.Fine, 0.5, SizeConstants.Medium, 5)]
        [TestCase(SizeConstants.Fine, 0.5, SizeConstants.Large, 10)]
        [TestCase(SizeConstants.Fine, 0.5, SizeConstants.Huge, 15)]
        [TestCase(SizeConstants.Fine, 0.5, SizeConstants.Gargantuan, 20)]
        [TestCase(SizeConstants.Fine, 0.5, SizeConstants.Colossal, 30)]
        [TestCase(SizeConstants.Diminutive, 1, SizeConstants.Tiny, 2.5)]
        [TestCase(SizeConstants.Diminutive, 1, SizeConstants.Small, 5)]
        [TestCase(SizeConstants.Diminutive, 1, SizeConstants.Medium, 5)]
        [TestCase(SizeConstants.Diminutive, 1, SizeConstants.Large, 10)]
        [TestCase(SizeConstants.Diminutive, 1, SizeConstants.Huge, 15)]
        [TestCase(SizeConstants.Diminutive, 1, SizeConstants.Gargantuan, 20)]
        [TestCase(SizeConstants.Diminutive, 1, SizeConstants.Colossal, 30)]
        [TestCase(SizeConstants.Tiny, 2.5, SizeConstants.Small, 5)]
        [TestCase(SizeConstants.Tiny, 2.5, SizeConstants.Medium, 5)]
        [TestCase(SizeConstants.Tiny, 2.5, SizeConstants.Large, 10)]
        [TestCase(SizeConstants.Tiny, 2.5, SizeConstants.Huge, 15)]
        [TestCase(SizeConstants.Tiny, 2.5, SizeConstants.Gargantuan, 20)]
        [TestCase(SizeConstants.Tiny, 2.5, SizeConstants.Colossal, 30)]
        [TestCase(SizeConstants.Small, 5, SizeConstants.Medium, 5)]
        [TestCase(SizeConstants.Small, 5, SizeConstants.Large, 10)]
        [TestCase(SizeConstants.Small, 5, SizeConstants.Huge, 15)]
        [TestCase(SizeConstants.Small, 5, SizeConstants.Gargantuan, 20)]
        [TestCase(SizeConstants.Small, 5, SizeConstants.Colossal, 30)]
        [TestCase(SizeConstants.Medium, 5, SizeConstants.Large, 10)]
        [TestCase(SizeConstants.Medium, 5, SizeConstants.Huge, 15)]
        [TestCase(SizeConstants.Medium, 5, SizeConstants.Gargantuan, 20)]
        [TestCase(SizeConstants.Medium, 5, SizeConstants.Colossal, 30)]
        [TestCase(SizeConstants.Large, 10, SizeConstants.Huge, 15)]
        [TestCase(SizeConstants.Large, 10, SizeConstants.Gargantuan, 20)]
        [TestCase(SizeConstants.Large, 10, SizeConstants.Colossal, 30)]
        [TestCase(SizeConstants.Huge, 15, SizeConstants.Gargantuan, 20)]
        [TestCase(SizeConstants.Huge, 15, SizeConstants.Colossal, 30)]
        [TestCase(SizeConstants.Gargantuan, 20, SizeConstants.Colossal, 30)]
        public void GetAdvancedSpace_DefaultIncrease(string size, double originalSpace, string advancedSize, double expectedSpace)
        {
            var advancedReach = spaceReachHelper.GetAdvancedSpace(size, originalSpace, advancedSize);
            Assert.That(advancedReach, Is.EqualTo(expectedSpace));
        }

        [TestCase(SizeConstants.Large, 20)]
        [TestCase(SizeConstants.Huge, 30)]
        [TestCase(SizeConstants.Gargantuan, 40)]
        [TestCase(SizeConstants.Colossal, 60)]
        public void GetAdvancedSpace_CustomIncrease_HigherThanNormal(string advancedSize, double expectedSpace)
        {
            var advancedReach = spaceReachHelper.GetAdvancedSpace(SizeConstants.Medium, 10, advancedSize);
            Assert.That(advancedReach, Is.EqualTo(expectedSpace));
        }

        [TestCase(SizeConstants.Large, 5)]
        [TestCase(SizeConstants.Huge, 7.5)]
        [TestCase(SizeConstants.Gargantuan, 10)]
        [TestCase(SizeConstants.Colossal, 15)]
        public void GetAdvancedSpace_CustomIncrease_LowerThanNormal(string advancedSize, double expectedSpace)
        {
            var advancedReach = spaceReachHelper.GetAdvancedSpace(SizeConstants.Medium, 2.5, advancedSize);
            Assert.That(advancedReach, Is.EqualTo(expectedSpace));
        }

        [Test]
        public void GetAdvancedSpace_CustomIncrease_None()
        {
            var advancedReach = spaceReachHelper.GetAdvancedSpace(SizeConstants.Medium, 0, SizeConstants.Colossal);
            Assert.That(advancedReach, Is.Zero);
        }
    }
}
