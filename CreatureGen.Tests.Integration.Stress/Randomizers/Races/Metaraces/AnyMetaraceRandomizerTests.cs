using CreatureGen.Creatures;
using CreatureGen.Randomizers.Races;
using NUnit.Framework;
using System.Collections.Generic;

namespace CreatureGen.Tests.Integration.Stress.Randomizers.Races.Metaraces
{
    [TestFixture]
    public class AnyMetaraceRandomizerTests : ForcableMetaraceRandomizerTests
    {
        protected override IEnumerable<string> allowedMetaraces
        {
            get
            {
                return new[] {
                    SizeConstants.Metaraces.Ghost,
                    SizeConstants.Metaraces.HalfCelestial,
                    SizeConstants.Metaraces.HalfDragon,
                    SizeConstants.Metaraces.HalfFiend,
                    SizeConstants.Metaraces.Lich,
                    SizeConstants.Metaraces.Mummy,
                    SizeConstants.Metaraces.None,
                    SizeConstants.Metaraces.Vampire,
                    SizeConstants.Metaraces.Werebear,
                    SizeConstants.Metaraces.Wereboar,
                    SizeConstants.Metaraces.Wererat,
                    SizeConstants.Metaraces.Weretiger,
                    SizeConstants.Metaraces.Werewolf,
                };
            }
        }

        [SetUp]
        public void Setup()
        {
            forcableMetaraceRandomizer = GetNewInstanceOf<IForcableMetaraceRandomizer>(RaceRandomizerTypeConstants.Metarace.AnyMeta);
        }

        [Test]
        public void StressAnyMetarace()
        {
            stressor.Stress(GenerateAndAssertMetarace);
        }

        [Test]
        public void StressForcedAnyMetarace()
        {
            stressor.Stress(GenerateAndAssertForcedMetarace);
        }
    }
}