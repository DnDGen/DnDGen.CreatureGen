using CreatureGen.Alignments;
using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Tables;
using CreatureGen.Creatures;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Races.BaseRaces.Good
{
    [TestFixture]
    public class GoodRangerBaseRacesTests : PercentileTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Good, CharacterClassConstants.Ranger); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(1, 5, SizeConstants.BaseRaces.HillDwarf)]
        [TestCase(6, 20, SizeConstants.BaseRaces.HighElf)]
        [TestCase(21, 21, SizeConstants.BaseRaces.WildElf)]
        [TestCase(22, 34, SizeConstants.BaseRaces.WoodElf)]
        [TestCase(35, 36, SizeConstants.BaseRaces.HoundArchon)]
        [TestCase(37, 41, SizeConstants.BaseRaces.ForestGnome)]
        [TestCase(42, 42, SizeConstants.BaseRaces.RockGnome)]
        [TestCase(43, 57, SizeConstants.BaseRaces.HalfElf)]
        [TestCase(58, 58, SizeConstants.BaseRaces.LightfootHalfling)]
        [TestCase(59, 59, SizeConstants.BaseRaces.TallfellowHalfling)]
        [TestCase(60, 64, SizeConstants.BaseRaces.HalfOrc)]
        [TestCase(65, 96, SizeConstants.BaseRaces.Human)]
        [TestCase(97, 97, SizeConstants.BaseRaces.StormGiant)]
        [TestCase(98, 98, SizeConstants.BaseRaces.CloudGiant)]
        [TestCase(99, 100, SizeConstants.BaseRaces.Centaur)]
        public override void Percentile(int lower, int upper, string content)
        {
            base.Percentile(lower, upper, content);
        }
    }
}