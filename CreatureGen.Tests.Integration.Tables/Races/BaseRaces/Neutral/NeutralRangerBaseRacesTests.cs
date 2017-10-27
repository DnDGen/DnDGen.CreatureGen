using CreatureGen.Alignments;
using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Tables;
using CreatureGen.Creatures;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Races.BaseRaces.Neutral
{
    [TestFixture]
    public class NeutralRangerBaseRacesTests : PercentileTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Neutral, CharacterClassConstants.Ranger); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(1, 1, SizeConstants.BaseRaces.HillDwarf)]
        [TestCase(2, 6, SizeConstants.BaseRaces.HighElf)]
        [TestCase(7, 7, SizeConstants.BaseRaces.WildElf)]
        [TestCase(8, 34, SizeConstants.BaseRaces.WoodElf)]
        [TestCase(35, 35, SizeConstants.BaseRaces.GraySlaad)]
        [TestCase(36, 36, SizeConstants.BaseRaces.Azer)]
        [TestCase(37, 37, SizeConstants.BaseRaces.ForestGnome)]
        [TestCase(38, 38, SizeConstants.BaseRaces.RockGnome)]
        [TestCase(39, 55, SizeConstants.BaseRaces.HalfElf)]
        [TestCase(56, 56, SizeConstants.BaseRaces.LightfootHalfling)]
        [TestCase(57, 57, SizeConstants.BaseRaces.TallfellowHalfling)]
        [TestCase(58, 67, SizeConstants.BaseRaces.HalfOrc)]
        [TestCase(68, 96, SizeConstants.BaseRaces.Human)]
        [TestCase(97, 97, SizeConstants.BaseRaces.Janni)]
        [TestCase(98, 99, SizeConstants.BaseRaces.Lizardfolk)]
        [TestCase(100, 100, SizeConstants.BaseRaces.StoneGiant)]
        public override void Percentile(int lower, int upper, string content)
        {
            base.Percentile(lower, upper, content);
        }
    }
}