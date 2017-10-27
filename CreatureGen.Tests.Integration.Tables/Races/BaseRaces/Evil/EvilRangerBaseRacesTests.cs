using CreatureGen.Alignments;
using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Tables;
using CreatureGen.Creatures;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Races.BaseRaces.Evil
{
    [TestFixture]
    public class EvilRangerBaseRacesTests : PercentileTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Evil, CharacterClassConstants.Ranger); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(1, 1, SizeConstants.BaseRaces.HighElf)]
        [TestCase(2, 11, SizeConstants.BaseRaces.WoodElf)]
        [TestCase(12, 28, SizeConstants.BaseRaces.HalfElf)]
        [TestCase(29, 29, SizeConstants.BaseRaces.LightfootHalfling)]
        [TestCase(30, 30, SizeConstants.BaseRaces.TallfellowHalfling)]
        [TestCase(31, 39, SizeConstants.BaseRaces.HalfOrc)]
        [TestCase(40, 72, SizeConstants.BaseRaces.Human)]
        [TestCase(73, 74, SizeConstants.BaseRaces.Lizardfolk)]
        [TestCase(75, 75, SizeConstants.BaseRaces.Hobgoblin)]
        [TestCase(76, 84, SizeConstants.BaseRaces.Gnoll)]
        [TestCase(85, 85, SizeConstants.BaseRaces.DeathSlaad)]
        [TestCase(86, 87, SizeConstants.BaseRaces.YuanTiPureblood)]
        [TestCase(88, 89, SizeConstants.BaseRaces.YuanTiHalfblood)]
        [TestCase(90, 90, SizeConstants.BaseRaces.YuanTiAbomination)]
        [TestCase(91, 91, SizeConstants.BaseRaces.Gargoyle)]
        [TestCase(92, 92, SizeConstants.BaseRaces.FrostGiant)]
        [TestCase(93, 93, SizeConstants.BaseRaces.FireGiant)]
        [TestCase(94, 94, SizeConstants.BaseRaces.CloudGiant)]
        [TestCase(95, 95, SizeConstants.BaseRaces.Troll)]
        [TestCase(96, 96, SizeConstants.BaseRaces.Troglodyte)]
        [TestCase(97, 97, SizeConstants.BaseRaces.Bugbear)]
        [TestCase(98, 98, SizeConstants.BaseRaces.Ogre)]
        [TestCase(99, 100, SizeConstants.BaseRaces.Scorpionfolk)]
        public override void Percentile(int lower, int upper, string content)
        {
            base.Percentile(lower, upper, content);
        }
    }
}