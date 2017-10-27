using CreatureGen.Creatures;
using CreatureGen.Randomizers.Races;
using NUnit.Framework;
using System.Collections.Generic;

namespace CreatureGen.Tests.Integration.Stress.Randomizers.Races.Metaraces
{
    [TestFixture]
    public abstract class ForcableMetaraceRandomizerTests : StressTests
    {
        protected IForcableMetaraceRandomizer forcableMetaraceRandomizer
        {
            get { return MetaraceRandomizer as IForcableMetaraceRandomizer; }
            set { MetaraceRandomizer = value; }
        }

        protected abstract IEnumerable<string> allowedMetaraces { get; }

        protected void GenerateAndAssertMetarace()
        {
            var metarace = GenerateMetarace();
            Assert.That(allowedMetaraces, Contains.Item(metarace));
        }

        private string GenerateMetarace()
        {
            var prototype = GetCharacterPrototype();
            return MetaraceRandomizer.Randomize(prototype.Alignment, prototype.CharacterClass);
        }

        protected void GenerateAndAssertForcedMetarace()
        {
            forcableMetaraceRandomizer.ForceMetarace = true;

            var metarace = GenerateMetarace();
            Assert.That(allowedMetaraces, Contains.Item(metarace));
            Assert.That(metarace, Is.Not.EqualTo(SizeConstants.Metaraces.None));
        }
    }
}