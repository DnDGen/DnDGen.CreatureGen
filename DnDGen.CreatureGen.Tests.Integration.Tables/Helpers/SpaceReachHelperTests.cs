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
        public void GetSpace_ReturnsSpace(string size, double expected)
        {
            var space = spaceReachHelper.GetSpace(size);
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
        public void GetReach_ReturnsReach_Tall(string size, double expected)
        {
            var reach = spaceReachHelper.GetReach(CreatureConstants.Human, size);
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
        public void GetReach_ReturnsReach_Long(string size, double expected)
        {
            var reach = spaceReachHelper.GetReach(CreatureConstants.Wolf, size);
            Assert.That(reach, Is.EqualTo(expected));
        }
    }
}
