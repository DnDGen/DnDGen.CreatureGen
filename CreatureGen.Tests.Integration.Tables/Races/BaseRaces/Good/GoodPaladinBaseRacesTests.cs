using CreatureGen.Alignments;
using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Tables;
using CreatureGen.Creatures;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Races.BaseRaces.Good
{
    [TestFixture]
    public class GoodPaladinBaseRacesTests : PercentileTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Good, CharacterClassConstants.Paladin); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(1, 10, SizeConstants.BaseRaces.Aasimar)]
        [TestCase(11, 20, SizeConstants.BaseRaces.HillDwarf)]
        [TestCase(21, 21, SizeConstants.BaseRaces.MountainDwarf)]
        [TestCase(22, 22, SizeConstants.BaseRaces.RockGnome)]
        [TestCase(23, 27, SizeConstants.BaseRaces.HalfElf)]
        [TestCase(28, 28, SizeConstants.BaseRaces.LightfootHalfling)]
        [TestCase(29, 29, SizeConstants.BaseRaces.DeepHalfling)]
        [TestCase(30, 30, SizeConstants.BaseRaces.HalfOrc)]
        [TestCase(31, 99, SizeConstants.BaseRaces.Human)]
        [TestCase(100, 100, SizeConstants.BaseRaces.HoundArchon)]
        public override void Percentile(int lower, int upper, string content)
        {
            base.Percentile(lower, upper, content);
        }
    }
}