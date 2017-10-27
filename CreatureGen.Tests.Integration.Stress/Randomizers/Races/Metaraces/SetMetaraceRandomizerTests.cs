using CreatureGen.Randomizers.Races;
using Ninject;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Stress.Randomizers.Races.Metaraces
{
    [TestFixture]
    public class SetMetaraceRandomizerTests : StressTests
    {
        [Inject]
        public ISetMetaraceRandomizer SetMetaraceRandomizer { get; set; }

        [SetUp]
        public void Setup()
        {
            var forcableMetaraceRandomizer = MetaraceRandomizer as IForcableMetaraceRandomizer;
            forcableMetaraceRandomizer.ForceMetarace = true;
        }

        [TearDown]
        public void TearDown()
        {
            MetaraceRandomizer = GetNewInstanceOf<RaceRandomizer>(RaceRandomizerTypeConstants.Metarace.AnyMeta);
        }

        [Test]
        public void StressSetMetarace()
        {
            stressor.Stress(AssertMetarace);
        }

        protected void AssertMetarace()
        {
            var prototype = GetCharacterPrototype();
            SetMetaraceRandomizer.SetMetarace = MetaraceRandomizer.Randomize(prototype.Alignment, prototype.CharacterClass);

            var metarace = SetMetaraceRandomizer.Randomize(prototype.Alignment, prototype.CharacterClass);
            Assert.That(metarace, Is.EqualTo(SetMetaraceRandomizer.SetMetarace));
        }
    }
}