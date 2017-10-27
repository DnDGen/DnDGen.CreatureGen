using CreatureGen.Alignments;
using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Tables;
using CreatureGen.Creatures;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Races.Metaraces.Good
{
    [TestFixture]
    public class GoodClericMetaracesTests : PercentileTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSMetaraces, AlignmentConstants.Good, CharacterClassConstants.Cleric); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(1, 95, SizeConstants.Metaraces.None)]
        [TestCase(96, 96, SizeConstants.Metaraces.Ghost)]
        [TestCase(97, 98, SizeConstants.Metaraces.HalfCelestial)]
        [TestCase(99, 99, SizeConstants.Metaraces.HalfDragon)]
        [TestCase(100, 100, SizeConstants.Metaraces.Werebear)]
        public override void Percentile(int lower, int upper, string content)
        {
            base.Percentile(lower, upper, content);
        }
    }
}