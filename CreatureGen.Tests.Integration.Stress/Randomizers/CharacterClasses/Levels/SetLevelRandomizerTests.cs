using CreatureGen.Randomizers.CharacterClasses;
using Ninject;
using NUnit.Framework;
using System;

namespace CreatureGen.Tests.Integration.Stress.Randomizers.CharacterClasses.Levels
{
    [TestFixture]
    public class SetLevelRandomizerTests : StressTests
    {
        [Inject]
        public ISetLevelRandomizer SetLevelRandomizer { get; set; }
        [Inject]
        public Random Random { get; set; }

        [Test]
        public void StressSetLevel()
        {
            stressor.Stress(AssertLevel);
        }

        protected void AssertLevel()
        {
            SetLevelRandomizer.SetLevel = Random.Next();
            var level = SetLevelRandomizer.Randomize();
            Assert.That(level, Is.EqualTo(SetLevelRandomizer.SetLevel));
        }
    }
}