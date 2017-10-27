using CreatureGen.Alignments;
using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Tables;
using CreatureGen.Creatures;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Races.BaseRaces.Good
{
    [TestFixture]
    public class GoodMonkBaseRacesTests : PercentileTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Good, CharacterClassConstants.Monk); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(1, 2, SizeConstants.BaseRaces.Aasimar)]
        [TestCase(3, 3, SizeConstants.BaseRaces.HillDwarf)]
        [TestCase(4, 13, SizeConstants.BaseRaces.HighElf)]
        [TestCase(14, 18, SizeConstants.BaseRaces.HalfElf)]
        [TestCase(19, 19, SizeConstants.BaseRaces.LightfootHalfling)]
        [TestCase(20, 20, SizeConstants.BaseRaces.DeepHalfling)]
        [TestCase(21, 25, SizeConstants.BaseRaces.HalfOrc)]
        [TestCase(26, 98, SizeConstants.BaseRaces.Human)]
        [TestCase(99, 99, SizeConstants.BaseRaces.HoundArchon)]
        [TestCase(100, 100, SizeConstants.BaseRaces.Centaur)]
        public override void Percentile(int lower, int upper, string content)
        {
            base.Percentile(lower, upper, content);
        }
    }
}