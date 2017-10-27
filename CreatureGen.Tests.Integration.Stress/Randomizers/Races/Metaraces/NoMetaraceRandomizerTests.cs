using CreatureGen.Creatures;
using CreatureGen.Randomizers.Races;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Stress.Randomizers.Races.Metaraces
{
    [TestFixture]
    public class NoMetaraceRandomizerTests : StressTests
    {
        [SetUp]
        public void Setup()
        {
            MetaraceRandomizer = GetNewInstanceOf<RaceRandomizer>(RaceRandomizerTypeConstants.Metarace.NoMeta);
        }

        [Test]
        public void StressNoMetarace()
        {
            stressor.Stress(AssertMetarace);
        }

        protected void AssertMetarace()
        {
            var prototype = GetCharacterPrototype();

            var metarace = MetaraceRandomizer.Randomize(prototype.Alignment, prototype.CharacterClass);
            Assert.That(metarace, Is.EqualTo(SizeConstants.Metaraces.None));
        }
    }
}