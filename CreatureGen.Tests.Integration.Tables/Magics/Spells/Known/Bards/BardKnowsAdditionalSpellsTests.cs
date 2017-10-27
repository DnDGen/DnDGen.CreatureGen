using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Tables;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Magics.Spells.Known.Bards
{
    [TestFixture]
    public class BardKnowsAdditionalSpellsTests : BooleanPercentileTests
    {
        protected override string tableName
        {
            get
            {
                return string.Format(TableNameConstants.Formattable.TrueOrFalse.CLASSKnowsAdditionalSpells, CharacterClassConstants.Bard);
            }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(1, 100, false)]
        public override void BooleanPercentile(int lower, int upper, bool isTrue)
        {
            base.BooleanPercentile(lower, upper, isTrue);
        }
    }
}
