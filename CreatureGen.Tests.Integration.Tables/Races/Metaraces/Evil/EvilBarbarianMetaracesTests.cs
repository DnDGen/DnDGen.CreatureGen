using CreatureGen.Alignments;
using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Tables;
using CreatureGen.Creatures;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Races.Metaraces.Evil
{
    [TestFixture]
    public class EvilBarbarianMetaracesTests : PercentileTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSMetaraces, AlignmentConstants.Evil, CharacterClassConstants.Barbarian); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(1, 92, SizeConstants.Metaraces.None)]
        [TestCase(93, 93, SizeConstants.Metaraces.Ghost)]
        [TestCase(94, 94, SizeConstants.Metaraces.Vampire)]
        [TestCase(95, 96, SizeConstants.Metaraces.Werewolf)]
        [TestCase(97, 98, SizeConstants.Metaraces.HalfFiend)]
        [TestCase(99, 100, SizeConstants.Metaraces.HalfDragon)]
        public override void Percentile(int lower, int upper, string content)
        {
            base.Percentile(lower, upper, content);
        }
    }
}