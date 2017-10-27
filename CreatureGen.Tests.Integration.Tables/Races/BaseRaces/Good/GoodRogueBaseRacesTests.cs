using CreatureGen.Alignments;
using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Tables;
using CreatureGen.Creatures;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Races.BaseRaces.Good
{
    [TestFixture]
    public class GoodRogueBaseRacesTests : PercentileTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Good, CharacterClassConstants.Rogue); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(1, 5, SizeConstants.BaseRaces.HillDwarf)]
        [TestCase(6, 6, SizeConstants.BaseRaces.MountainDwarf)]
        [TestCase(7, 19, SizeConstants.BaseRaces.HighElf)]
        [TestCase(20, 20, SizeConstants.BaseRaces.ForestGnome)]
        [TestCase(21, 25, SizeConstants.BaseRaces.RockGnome)]
        [TestCase(26, 35, SizeConstants.BaseRaces.HalfElf)]
        [TestCase(36, 58, SizeConstants.BaseRaces.LightfootHalfling)]
        [TestCase(59, 59, SizeConstants.BaseRaces.Pixie)]
        [TestCase(60, 60, SizeConstants.BaseRaces.StormGiant)]
        [TestCase(61, 66, SizeConstants.BaseRaces.DeepHalfling)]
        [TestCase(67, 72, SizeConstants.BaseRaces.TallfellowHalfling)]
        [TestCase(73, 77, SizeConstants.BaseRaces.HalfOrc)]
        [TestCase(78, 97, SizeConstants.BaseRaces.Human)]
        [TestCase(98, 98, SizeConstants.BaseRaces.CloudGiant)]
        [TestCase(99, 99, SizeConstants.BaseRaces.Svirfneblin)]
        [TestCase(100, 100, SizeConstants.BaseRaces.Centaur)]
        public override void Percentile(int lower, int upper, string content)
        {
            base.Percentile(lower, upper, content);
        }
    }
}