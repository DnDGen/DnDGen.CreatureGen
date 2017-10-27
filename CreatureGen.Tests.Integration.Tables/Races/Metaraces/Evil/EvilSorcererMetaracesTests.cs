using CreatureGen.Alignments;
using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Tables;
using CreatureGen.Creatures;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Races.Metaraces.Evil
{
    [TestFixture]
    public class EvilSorcererMetaracesTests : PercentileTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSMetaraces, AlignmentConstants.Evil, CharacterClassConstants.Sorcerer); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(1, 90, SizeConstants.Metaraces.None)]
        [TestCase(91, 91, SizeConstants.Metaraces.Mummy)]
        [TestCase(92, 92, SizeConstants.Metaraces.Ghost)]
        [TestCase(93, 93, SizeConstants.Metaraces.Vampire)]
        [TestCase(94, 95, SizeConstants.Metaraces.Lich)]
        [TestCase(96, 96, SizeConstants.Metaraces.Wererat)]
        [TestCase(97, 97, SizeConstants.Metaraces.Werewolf)]
        [TestCase(98, 98, SizeConstants.Metaraces.HalfFiend)]
        [TestCase(99, 100, SizeConstants.Metaraces.HalfDragon)]
        public override void Percentile(int lower, int upper, string content)
        {
            base.Percentile(lower, upper, content);
        }
    }
}