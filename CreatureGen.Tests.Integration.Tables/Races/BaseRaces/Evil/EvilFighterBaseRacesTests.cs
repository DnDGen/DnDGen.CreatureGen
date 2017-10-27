using CreatureGen.Alignments;
using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Tables;
using CreatureGen.Creatures;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Races.BaseRaces.Evil
{
    [TestFixture]
    public class EvilFighterBaseRacesTests : PercentileTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Evil, CharacterClassConstants.Fighter); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(1, 2, SizeConstants.BaseRaces.DeepDwarf)]
        [TestCase(3, 4, SizeConstants.BaseRaces.HillDwarf)]
        [TestCase(5, 5, SizeConstants.BaseRaces.HighElf)]
        [TestCase(6, 7, SizeConstants.BaseRaces.WoodElf)]
        [TestCase(8, 12, SizeConstants.BaseRaces.HalfElf)]
        [TestCase(13, 13, SizeConstants.BaseRaces.LightfootHalfling)]
        [TestCase(14, 14, SizeConstants.BaseRaces.DeepHalfling)]
        [TestCase(15, 23, SizeConstants.BaseRaces.HalfOrc)]
        [TestCase(24, 55, SizeConstants.BaseRaces.Human)]
        [TestCase(56, 56, SizeConstants.BaseRaces.CloudGiant)]
        [TestCase(57, 57, SizeConstants.BaseRaces.Lizardfolk)]
        [TestCase(58, 58, SizeConstants.BaseRaces.Goblin)]
        [TestCase(59, 70, SizeConstants.BaseRaces.Hobgoblin)]
        [TestCase(71, 72, SizeConstants.BaseRaces.Githyanki)]
        [TestCase(73, 73, SizeConstants.BaseRaces.DeathSlaad)]
        [TestCase(74, 74, SizeConstants.BaseRaces.YuanTiHalfblood)]
        [TestCase(75, 75, SizeConstants.BaseRaces.YuanTiAbomination)]
        [TestCase(76, 77, SizeConstants.BaseRaces.Gargoyle)]
        [TestCase(78, 78, SizeConstants.BaseRaces.Scorpionfolk)]
        [TestCase(79, 79, SizeConstants.BaseRaces.Grimlock)]
        [TestCase(80, 80, SizeConstants.BaseRaces.HillGiant)]
        [TestCase(81, 81, SizeConstants.BaseRaces.FireGiant)]
        [TestCase(82, 83, SizeConstants.BaseRaces.Troll)]
        [TestCase(84, 84, SizeConstants.BaseRaces.Kobold)]
        [TestCase(85, 89, SizeConstants.BaseRaces.Orc)]
        [TestCase(90, 91, SizeConstants.BaseRaces.Drow)]
        [TestCase(92, 92, SizeConstants.BaseRaces.DuergarDwarf)]
        [TestCase(93, 93, SizeConstants.BaseRaces.Derro)]
        [TestCase(94, 94, SizeConstants.BaseRaces.Gnoll)]
        [TestCase(95, 95, SizeConstants.BaseRaces.Troglodyte)]
        [TestCase(96, 96, SizeConstants.BaseRaces.Bugbear)]
        [TestCase(97, 97, SizeConstants.BaseRaces.Ogre)]
        [TestCase(98, 98, SizeConstants.BaseRaces.MindFlayer)]
        [TestCase(99, 99, SizeConstants.BaseRaces.OgreMage)]
        [TestCase(100, 100, SizeConstants.BaseRaces.FrostGiant)]
        public override void Percentile(int lower, int upper, string content)
        {
            base.Percentile(lower, upper, content);
        }
    }
}