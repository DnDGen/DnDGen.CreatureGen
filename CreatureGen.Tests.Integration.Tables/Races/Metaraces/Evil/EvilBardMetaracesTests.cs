using CreatureGen.Alignments;
using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Tables;
using CreatureGen.Creatures;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Races.Metaraces.Evil
{
    [TestFixture]
    public class EvilBardMetaracesTests : PercentileTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSMetaraces, AlignmentConstants.Evil, CharacterClassConstants.Bard); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(1, 96, SizeConstants.Metaraces.None)]
        [TestCase(97, 97, SizeConstants.Metaraces.Ghost)]
        [TestCase(98, 98, SizeConstants.Metaraces.Vampire)]
        [TestCase(99, 99, SizeConstants.Metaraces.Lich)]
        [TestCase(100, 100, SizeConstants.Metaraces.Werewolf)]
        public override void Percentile(int lower, int upper, string content)
        {
            base.Percentile(lower, upper, content);
        }
    }
}