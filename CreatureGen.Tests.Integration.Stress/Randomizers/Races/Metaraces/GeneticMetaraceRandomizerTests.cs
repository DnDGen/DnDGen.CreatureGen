using CreatureGen.Creatures;
using CreatureGen.Randomizers.Alignments;
using CreatureGen.Randomizers.Races;
using NUnit.Framework;
using System.Collections.Generic;

namespace CreatureGen.Tests.Integration.Stress.Randomizers.Races.Metaraces
{
    [TestFixture]
    public class GeneticMetaraceRandomizerTests : ForcableMetaraceRandomizerTests
    {
        protected override IEnumerable<string> allowedMetaraces
        {
            get
            {
                return new[]
                {
                    SizeConstants.Metaraces.HalfDragon,
                    SizeConstants.Metaraces.HalfFiend,
                    SizeConstants.Metaraces.HalfCelestial,
                    SizeConstants.Metaraces.None
                };
            }
        }

        [SetUp]
        public void Setup()
        {
            forcableMetaraceRandomizer = GetNewInstanceOf<IForcableMetaraceRandomizer>(RaceRandomizerTypeConstants.Metarace.GeneticMeta);
            AlignmentRandomizer = GetNewInstanceOf<IAlignmentRandomizer>(AlignmentRandomizerTypeConstants.NonNeutral);
            //INFO: We are using the non-neutral alignment, as the current genetic metaraces (Half-dragon, Half-celestial,
            //and Half-fiend) all cannot be neutral.  This makes the process faster for testing
        }

        [TearDown]
        public void TearDown()
        {
            AlignmentRandomizer = GetNewInstanceOf<IAlignmentRandomizer>(AlignmentRandomizerTypeConstants.Any);
        }

        [Test]
        public void StressGeneticMetarace()
        {
            stressor.Stress(GenerateAndAssertMetarace);
        }

        [Test]
        public void StressForcedGeneticMetarace()
        {
            stressor.Stress(GenerateAndAssertForcedMetarace);
        }
    }
}