using CreatureGen.Creatures;
using CreatureGen.Randomizers.Races;
using NUnit.Framework;
using System.Collections.Generic;

namespace CreatureGen.Tests.Integration.Stress.Randomizers.Races.BaseRaces
{
    [TestFixture]
    public class NonMonsterBaseRaceRandomizerTests : BaseRaceRandomizerTests
    {
        protected override IEnumerable<string> allowedBaseRaces
        {
            get
            {
                return new[]
                {
                    SizeConstants.BaseRaces.Aasimar,
                    SizeConstants.BaseRaces.DeepDwarf,
                    SizeConstants.BaseRaces.DeepHalfling,
                    SizeConstants.BaseRaces.Drow,
                    SizeConstants.BaseRaces.DuergarDwarf,
                    SizeConstants.BaseRaces.ForestGnome,
                    SizeConstants.BaseRaces.GrayElf,
                    SizeConstants.BaseRaces.HalfElf,
                    SizeConstants.BaseRaces.HalfOrc,
                    SizeConstants.BaseRaces.HighElf,
                    SizeConstants.BaseRaces.HillDwarf,
                    SizeConstants.BaseRaces.Human,
                    SizeConstants.BaseRaces.LightfootHalfling,
                    SizeConstants.BaseRaces.MountainDwarf,
                    SizeConstants.BaseRaces.RockGnome,
                    SizeConstants.BaseRaces.Svirfneblin,
                    SizeConstants.BaseRaces.TallfellowHalfling,
                    SizeConstants.BaseRaces.Tiefling,
                    SizeConstants.BaseRaces.WildElf,
                    SizeConstants.BaseRaces.WoodElf,
                };
            }
        }

        [SetUp]
        public void Setup()
        {
            BaseRaceRandomizer = GetNewInstanceOf<RaceRandomizer>(RaceRandomizerTypeConstants.BaseRace.NonMonsterBase);
        }

        [Test]
        public void StressNonMonsterBaseRace()
        {
            stressor.Stress(AssertBaseRace);
        }
    }
}