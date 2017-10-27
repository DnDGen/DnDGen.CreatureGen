using CreatureGen.Alignments;
using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Tables;
using CreatureGen.Creatures;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Races.BaseRaces.Good
{
    [TestFixture]
    public class GoodDruidBaseRacesTests : PercentileTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Good, CharacterClassConstants.Druid); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(1, 1, SizeConstants.BaseRaces.GrayElf)]
        [TestCase(2, 11, SizeConstants.BaseRaces.HighElf)]
        [TestCase(12, 21, SizeConstants.BaseRaces.WildElf)]
        [TestCase(22, 31, SizeConstants.BaseRaces.WoodElf)]
        [TestCase(32, 36, SizeConstants.BaseRaces.ForestGnome)]
        [TestCase(37, 37, SizeConstants.BaseRaces.RockGnome)]
        [TestCase(38, 46, SizeConstants.BaseRaces.HalfElf)]
        [TestCase(47, 47, SizeConstants.BaseRaces.LightfootHalfling)]
        [TestCase(48, 48, SizeConstants.BaseRaces.TallfellowHalfling)]
        [TestCase(49, 49, SizeConstants.BaseRaces.HalfOrc)]
        [TestCase(50, 97, SizeConstants.BaseRaces.Human)]
        [TestCase(98, 98, SizeConstants.BaseRaces.Pixie)]
        [TestCase(99, 99, SizeConstants.BaseRaces.CloudGiant)]
        [TestCase(100, 100, SizeConstants.BaseRaces.Centaur)]
        public override void Percentile(int lower, int upper, string content)
        {
            base.Percentile(lower, upper, content);
        }
    }
}