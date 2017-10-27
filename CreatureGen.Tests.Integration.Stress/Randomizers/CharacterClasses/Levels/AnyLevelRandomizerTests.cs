using CreatureGen.Randomizers.CharacterClasses;
using Ninject;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Stress.Randomizers.CharacterClasses.Levels
{
    [TestFixture]
    public class AnyLevelRandomizerTests : StressTests
    {
        [Inject, Named(LevelRandomizerTypeConstants.Any)]
        public ILevelRandomizer AnyLevelRandomizer { get; set; }

        [Test]
        public void StressAnyLevel()
        {
            stressor.Stress(AssertLevel);
        }

        protected void AssertLevel()
        {
            var level = LevelRandomizer.Randomize();
            Assert.That(level, Is.InRange(1, 20));
        }
    }
}