using CreatureGen.Alignments;
using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Tables;
using CreatureGen.Creatures;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Races.BaseRaces.Neutral
{
    [TestFixture]
    public class NeutralDruidBaseRacesTests : PercentileTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Neutral, CharacterClassConstants.Druid); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(1, 1, SizeConstants.BaseRaces.GrayElf)]
        [TestCase(2, 6, SizeConstants.BaseRaces.HighElf)]
        [TestCase(7, 11, SizeConstants.BaseRaces.WildElf)]
        [TestCase(12, 30, SizeConstants.BaseRaces.WoodElf)]
        [TestCase(31, 31, SizeConstants.BaseRaces.Azer)]
        [TestCase(32, 32, SizeConstants.BaseRaces.ForestGnome)]
        [TestCase(33, 37, SizeConstants.BaseRaces.HalfElf)]
        [TestCase(38, 38, SizeConstants.BaseRaces.LightfootHalfling)]
        [TestCase(39, 39, SizeConstants.BaseRaces.TallfellowHalfling)]
        [TestCase(40, 40, SizeConstants.BaseRaces.HalfOrc)]
        [TestCase(41, 87, SizeConstants.BaseRaces.Human)]
        [TestCase(88, 88, SizeConstants.BaseRaces.GraySlaad)]
        [TestCase(89, 89, SizeConstants.BaseRaces.Satyr)]
        [TestCase(90, 99, SizeConstants.BaseRaces.Lizardfolk)]
        [TestCase(100, 100, SizeConstants.BaseRaces.Janni)]
        public override void Percentile(int lower, int upper, string content)
        {
            base.Percentile(lower, upper, content);
        }
    }
}