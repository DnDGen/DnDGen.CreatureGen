using CreatureGen.Alignments;
using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Tables;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.CharacterClasses
{
    [TestFixture]
    public class EvilCharacterClassesTests : PercentileTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCharacterClasses, AlignmentConstants.Evil); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(1, 10, CharacterClassConstants.Barbarian)]
        [TestCase(11, 15, CharacterClassConstants.Bard)]
        [TestCase(16, 35, CharacterClassConstants.Cleric)]
        [TestCase(36, 40, CharacterClassConstants.Druid)]
        [TestCase(41, 50, CharacterClassConstants.Fighter)]
        [TestCase(51, 55, CharacterClassConstants.Monk)]
        [TestCase(56, 60, CharacterClassConstants.Ranger)]
        [TestCase(61, 80, CharacterClassConstants.Rogue)]
        [TestCase(81, 85, CharacterClassConstants.Sorcerer)]
        [TestCase(86, 100, CharacterClassConstants.Wizard)]
        public override void Percentile(int lower, int upper, string content)
        {
            base.Percentile(lower, upper, content);
        }
    }
}