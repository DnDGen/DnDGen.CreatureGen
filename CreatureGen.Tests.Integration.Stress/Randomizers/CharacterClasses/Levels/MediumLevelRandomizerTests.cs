using CreatureGen.Randomizers.CharacterClasses;
using Ninject;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Stress.Randomizers.CharacterClasses.Levels
{
    [TestFixture]
    public class MediumLevelRandomizerTests : StressTests
    {
        [Inject, Named(LevelRandomizerTypeConstants.Medium)]
        public ILevelRandomizer MediumLevelRandomizer { get; set; }

        [Test]
        public void StressMediumLevel()
        {
            stressor.Stress(AssertLevel);
        }

        protected void AssertLevel()
        {
            var level = MediumLevelRandomizer.Randomize();
            Assert.That(level, Is.InRange(6, 10));
        }
    }
}