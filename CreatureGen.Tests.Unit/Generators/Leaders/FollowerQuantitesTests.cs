using CreatureGen.Leaders;
using NUnit.Framework;

namespace CreatureGen.Tests.Unit.Generators.Leaders
{
    [TestFixture]
    public class FollowerQuantitesTests
    {
        private FollowerQuantities followerQuantities;

        [SetUp]
        public void Setup()
        {
            followerQuantities = new FollowerQuantities();
        }

        [Test]
        public void FollowerQuantitesAreinitialized()
        {
            Assert.That(followerQuantities.Level1, Is.EqualTo(0));
            Assert.That(followerQuantities.Level2, Is.EqualTo(0));
            Assert.That(followerQuantities.Level3, Is.EqualTo(0));
            Assert.That(followerQuantities.Level4, Is.EqualTo(0));
            Assert.That(followerQuantities.Level5, Is.EqualTo(0));
            Assert.That(followerQuantities.Level6, Is.EqualTo(0));
        }
    }
}