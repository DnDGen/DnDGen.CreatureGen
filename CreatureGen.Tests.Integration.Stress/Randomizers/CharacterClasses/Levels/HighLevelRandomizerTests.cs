using CreatureGen.Randomizers.CharacterClasses;
using Ninject;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Stress.Randomizers.CharacterClasses.Levels
{
    [TestFixture]
    public class HighLevelRandomizerTests : StressTests
    {
        [Inject, Named(LevelRandomizerTypeConstants.High)]
        public ILevelRandomizer HighLevelRandomizer { get; set; }

        [Test]
        public void StressHighLevel()
        {
            stressor.Stress(AssertLevel);
        }

        protected void AssertLevel()
        {
            var level = HighLevelRandomizer.Randomize();
            Assert.That(level, Is.InRange(11, 15));
        }
    }
}