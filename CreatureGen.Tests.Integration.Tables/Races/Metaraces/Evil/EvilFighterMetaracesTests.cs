using CreatureGen.Alignments;
using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Tables;
using CreatureGen.Creatures;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Races.Metaraces.Evil
{
    [TestFixture]
    public class EvilFighterMetaracesTests : PercentileTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSMetaraces, AlignmentConstants.Evil, CharacterClassConstants.Fighter); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(1, 93, SizeConstants.Metaraces.None)]
        [TestCase(94, 94, SizeConstants.Metaraces.Mummy)]
        [TestCase(95, 95, SizeConstants.Metaraces.Ghost)]
        [TestCase(96, 96, SizeConstants.Metaraces.Vampire)]
        [TestCase(97, 97, SizeConstants.Metaraces.Wererat)]
        [TestCase(98, 98, SizeConstants.Metaraces.Werewolf)]
        [TestCase(99, 99, SizeConstants.Metaraces.HalfFiend)]
        [TestCase(100, 100, SizeConstants.Metaraces.HalfDragon)]
        public override void Percentile(int lower, int upper, string content)
        {
            base.Percentile(lower, upper, content);
        }
    }
}