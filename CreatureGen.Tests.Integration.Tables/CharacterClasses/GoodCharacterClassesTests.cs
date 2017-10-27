using CreatureGen.Alignments;
using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Tables;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.CharacterClasses
{
    [TestFixture]
    public class GoodCharacterClassesTests : PercentileTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCharacterClasses, AlignmentConstants.Good); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(1, 5, CharacterClassConstants.Barbarian)]
        [TestCase(6, 10, CharacterClassConstants.Bard)]
        [TestCase(11, 30, CharacterClassConstants.Cleric)]
        [TestCase(31, 35, CharacterClassConstants.Druid)]
        [TestCase(36, 45, CharacterClassConstants.Fighter)]
        [TestCase(46, 50, CharacterClassConstants.Monk)]
        [TestCase(51, 55, CharacterClassConstants.Paladin)]
        [TestCase(56, 65, CharacterClassConstants.Ranger)]
        [TestCase(66, 75, CharacterClassConstants.Rogue)]
        [TestCase(76, 80, CharacterClassConstants.Sorcerer)]
        [TestCase(81, 100, CharacterClassConstants.Wizard)]
        public override void Percentile(int lower, int upper, string content)
        {
            base.Percentile(lower, upper, content);
        }
    }
}