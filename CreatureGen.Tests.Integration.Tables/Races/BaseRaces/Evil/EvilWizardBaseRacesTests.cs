using CreatureGen.Alignments;
using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Tables;
using CreatureGen.Creatures;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Races.BaseRaces.Evil
{
    [TestFixture]
    public class EvilWizardBaseRacesTests : PercentileTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Evil, CharacterClassConstants.Wizard); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(1, 10, SizeConstants.BaseRaces.HighElf)]
        [TestCase(11, 11, SizeConstants.BaseRaces.WoodElf)]
        [TestCase(12, 26, SizeConstants.BaseRaces.HalfElf)]
        [TestCase(27, 27, SizeConstants.BaseRaces.LightfootHalfling)]
        [TestCase(28, 28, SizeConstants.BaseRaces.TallfellowHalfling)]
        [TestCase(29, 76, SizeConstants.BaseRaces.Human)]
        [TestCase(77, 77, SizeConstants.BaseRaces.Githyanki)]
        [TestCase(78, 78, SizeConstants.BaseRaces.DeathSlaad)]
        [TestCase(79, 79, SizeConstants.BaseRaces.YuanTiPureblood)]
        [TestCase(80, 80, SizeConstants.BaseRaces.YuanTiHalfblood)]
        [TestCase(81, 81, SizeConstants.BaseRaces.YuanTiAbomination)]
        [TestCase(82, 83, SizeConstants.BaseRaces.Hobgoblin)]
        [TestCase(84, 84, SizeConstants.BaseRaces.Tiefling)]
        [TestCase(85, 94, SizeConstants.BaseRaces.Drow)]
        [TestCase(95, 95, SizeConstants.BaseRaces.Gnoll)]
        [TestCase(96, 96, SizeConstants.BaseRaces.Bugbear)]
        [TestCase(97, 97, SizeConstants.BaseRaces.MindFlayer)]
        [TestCase(98, 99, SizeConstants.BaseRaces.OgreMage)]
        [TestCase(100, 100, SizeConstants.BaseRaces.Rakshasa)]
        public override void Percentile(int lower, int upper, string content)
        {
            base.Percentile(lower, upper, content);
        }
    }
}