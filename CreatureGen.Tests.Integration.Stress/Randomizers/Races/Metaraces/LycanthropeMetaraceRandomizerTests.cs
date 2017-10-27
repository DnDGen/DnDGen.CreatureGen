using CreatureGen.Creatures;
using CreatureGen.Randomizers.Races;
using NUnit.Framework;
using System.Collections.Generic;

namespace CreatureGen.Tests.Integration.Stress.Randomizers.Races.Metaraces
{
    [TestFixture]
    public class LycanthropeMetaraceRandomizerTests : ForcableMetaraceRandomizerTests
    {
        protected override IEnumerable<string> allowedMetaraces
        {
            get
            {
                return new[]
                {
                    SizeConstants.Metaraces.Werebear,
                    SizeConstants.Metaraces.Wereboar,
                    SizeConstants.Metaraces.Wererat,
                    SizeConstants.Metaraces.Weretiger,
                    SizeConstants.Metaraces.Werewolf,
                    SizeConstants.Metaraces.None
                };
            }
        }

        [SetUp]
        public void Setup()
        {
            forcableMetaraceRandomizer = GetNewInstanceOf<IForcableMetaraceRandomizer>(RaceRandomizerTypeConstants.Metarace.LycanthropeMeta);
        }

        [Test]
        public void StressLycanthropeMetarace()
        {
            stressor.Stress(GenerateAndAssertMetarace);
        }

        [Test]
        [Ignore("Because lycanthropes can only have a particular alignment, generating alignments that satisfy the randomizer takes over 200% of stress test time limit")]
        public void StressForcedLycanthropeMetarace()
        {
            stressor.Stress(GenerateAndAssertForcedMetarace);
        }
    }
}