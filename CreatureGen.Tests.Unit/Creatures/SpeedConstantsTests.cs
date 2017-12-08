using CreatureGen.Creatures;
using NUnit.Framework;

namespace CreatureGen.Tests.Unit.Creatures
{
    [TestFixture]
    public class SpeedConstantsTests
    {
        [TestCase(SpeedConstants.Fly, "Fly")]
        [TestCase(SpeedConstants.Burrow, "Burrow")]
        [TestCase(SpeedConstants.Climb, "Climb")]
        [TestCase(SpeedConstants.Walk, "Walk")]
        [TestCase(SpeedConstants.Swim, "Swim")]
        public void SpeedConstant(string constant, string value)
        {
            Assert.That(constant, Is.EqualTo(value));
        }
    }
}
