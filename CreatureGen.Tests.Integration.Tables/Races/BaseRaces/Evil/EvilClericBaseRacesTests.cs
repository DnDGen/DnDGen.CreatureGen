using CreatureGen.Alignments;
using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Tables;
using CreatureGen.Creatures;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Races.BaseRaces.Evil
{
    [TestFixture]
    public class EvilClericBaseRacesTests : PercentileTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Evil, CharacterClassConstants.Cleric); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(1, 2, SizeConstants.BaseRaces.DeepDwarf)]
        [TestCase(3, 3, SizeConstants.BaseRaces.HillDwarf)]
        [TestCase(4, 4, SizeConstants.BaseRaces.HighElf)]
        [TestCase(5, 5, SizeConstants.BaseRaces.WildElf)]
        [TestCase(6, 8, SizeConstants.BaseRaces.WoodElf)]
        [TestCase(9, 18, SizeConstants.BaseRaces.HalfElf)]
        [TestCase(19, 20, SizeConstants.BaseRaces.LightfootHalfling)]
        [TestCase(21, 21, SizeConstants.BaseRaces.DeepHalfling)]
        [TestCase(22, 22, SizeConstants.BaseRaces.TallfellowHalfling)]
        [TestCase(23, 25, SizeConstants.BaseRaces.HalfOrc)]
        [TestCase(26, 58, SizeConstants.BaseRaces.Human)]
        [TestCase(59, 59, SizeConstants.BaseRaces.FrostGiant)]
        [TestCase(60, 60, SizeConstants.BaseRaces.FireGiant)]
        [TestCase(61, 67, SizeConstants.BaseRaces.Lizardfolk)]
        [TestCase(68, 68, SizeConstants.BaseRaces.Goblin)]
        [TestCase(69, 69, SizeConstants.BaseRaces.Hobgoblin)]
        [TestCase(70, 70, SizeConstants.BaseRaces.Kobold)]
        [TestCase(71, 71, SizeConstants.BaseRaces.Orc)]
        [TestCase(72, 72, SizeConstants.BaseRaces.Tiefling)]
        [TestCase(73, 75, SizeConstants.BaseRaces.Drow)]
        [TestCase(76, 76, SizeConstants.BaseRaces.DuergarDwarf)]
        [TestCase(77, 78, SizeConstants.BaseRaces.Gnoll)]
        [TestCase(79, 79, SizeConstants.BaseRaces.Scorpionfolk)]
        [TestCase(80, 87, SizeConstants.BaseRaces.Troglodyte)]
        [TestCase(88, 88, SizeConstants.BaseRaces.DeathSlaad)]
        [TestCase(89, 90, SizeConstants.BaseRaces.YuanTiAbomination)]
        [TestCase(91, 91, SizeConstants.BaseRaces.YuanTiHalfblood)]
        [TestCase(92, 92, SizeConstants.BaseRaces.Rakshasa)]
        [TestCase(93, 93, SizeConstants.BaseRaces.Harpy)]
        [TestCase(94, 95, SizeConstants.BaseRaces.Bugbear)]
        [TestCase(96, 96, SizeConstants.BaseRaces.Ogre)]
        [TestCase(97, 97, SizeConstants.BaseRaces.Minotaur)]
        [TestCase(98, 98, SizeConstants.BaseRaces.MindFlayer)]
        [TestCase(99, 99, SizeConstants.BaseRaces.OgreMage)]
        [TestCase(100, 100, SizeConstants.BaseRaces.CloudGiant)]
        public override void Percentile(int lower, int upper, string content)
        {
            base.Percentile(lower, upper, content);
        }
    }
}