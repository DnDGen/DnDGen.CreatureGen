using CreatureGen.Creatures;
using CreatureGen.Randomizers.Races;
using NUnit.Framework;
using System.Collections.Generic;

namespace CreatureGen.Tests.Integration.Stress.Randomizers.Races.Metaraces
{
    [TestFixture]
    public class UndeadMetaraceRandomizerTests : ForcableMetaraceRandomizerTests
    {
        protected override IEnumerable<string> allowedMetaraces
        {
            get
            {
                return new[]
                {
                    SizeConstants.Metaraces.Ghost,
                    SizeConstants.Metaraces.Lich,
                    SizeConstants.Metaraces.Mummy,
                    SizeConstants.Metaraces.None,
                    SizeConstants.Metaraces.Vampire,
                };
            }
        }

        [SetUp]
        public void Setup()
        {
            forcableMetaraceRandomizer = GetNewInstanceOf<IForcableMetaraceRandomizer>(RaceRandomizerTypeConstants.Metarace.UndeadMeta);
        }

        [Test]
        public void StressUndeadMetarace()
        {
            stressor.Stress(GenerateAndAssertMetarace);
        }

        [Test]
        public void StressForcedUndeadMetarace()
        {
            stressor.Stress(GenerateAndAssertForcedMetarace);
        }
    }
}
