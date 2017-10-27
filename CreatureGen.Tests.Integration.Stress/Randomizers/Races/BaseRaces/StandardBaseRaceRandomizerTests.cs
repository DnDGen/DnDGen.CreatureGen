using CreatureGen.Creatures;
using CreatureGen.Randomizers.Races;
using NUnit.Framework;
using System.Collections.Generic;

namespace CreatureGen.Tests.Integration.Stress.Randomizers.Races.BaseRaces
{
    [TestFixture]
    public class StandardBaseRaceRandomizerTests : BaseRaceRandomizerTests
    {
        protected override IEnumerable<string> allowedBaseRaces
        {
            get
            {
                return new[]
                {
                    SizeConstants.BaseRaces.RockGnome,
                    SizeConstants.BaseRaces.HalfElf,
                    SizeConstants.BaseRaces.HalfOrc,
                    SizeConstants.BaseRaces.HighElf,
                    SizeConstants.BaseRaces.HillDwarf,
                    SizeConstants.BaseRaces.Human,
                    SizeConstants.BaseRaces.LightfootHalfling
                };
            }
        }

        [SetUp]
        public void Setup()
        {
            BaseRaceRandomizer = GetNewInstanceOf<RaceRandomizer>(RaceRandomizerTypeConstants.BaseRace.StandardBase);
        }

        [Test]
        public void StressStandardBaseRace()
        {
            stressor.Stress(AssertBaseRace);
        }
    }
}