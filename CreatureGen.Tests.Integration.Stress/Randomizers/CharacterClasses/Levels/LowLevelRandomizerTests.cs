using CreatureGen.Randomizers.CharacterClasses;
using Ninject;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Stress.Randomizers.CharacterClasses.Levels
{
    [TestFixture]
    public class LowLevelRandomizerTests : StressTests
    {
        [Inject, Named(LevelRandomizerTypeConstants.Low)]
        public ILevelRandomizer LowLevelRandomizer { get; set; }

        [Test]
        public void StressLowLevel()
        {
            stressor.Stress(AssertLevel);
        }

        protected void AssertLevel()
        {
            var level = LowLevelRandomizer.Randomize();
            Assert.That(level, Is.InRange(1, 5));
        }
    }
}