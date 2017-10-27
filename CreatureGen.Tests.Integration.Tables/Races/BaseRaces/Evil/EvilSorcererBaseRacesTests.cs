using CreatureGen.Alignments;
using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Tables;
using CreatureGen.Creatures;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Races.BaseRaces.Evil
{
    [TestFixture]
    public class EvilSorcererBaseRacesTests : PercentileTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Evil, CharacterClassConstants.Sorcerer); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(1, 1, SizeConstants.BaseRaces.WildElf)]
        [TestCase(2, 16, SizeConstants.BaseRaces.HalfElf)]
        [TestCase(17, 21, SizeConstants.BaseRaces.LightfootHalfling)]
        [TestCase(22, 22, SizeConstants.BaseRaces.DeepHalfling)]
        [TestCase(23, 23, SizeConstants.BaseRaces.TallfellowHalfling)]
        [TestCase(24, 28, SizeConstants.BaseRaces.HalfOrc)]
        [TestCase(29, 69, SizeConstants.BaseRaces.Human)]
        [TestCase(70, 71, SizeConstants.BaseRaces.Rakshasa)]
        [TestCase(72, 72, SizeConstants.BaseRaces.FrostGiant)]
        [TestCase(73, 73, SizeConstants.BaseRaces.Lizardfolk)]
        [TestCase(74, 74, SizeConstants.BaseRaces.Goblin)]
        [TestCase(75, 75, SizeConstants.BaseRaces.Hobgoblin)]
        [TestCase(76, 83, SizeConstants.BaseRaces.Kobold)]
        [TestCase(84, 84, SizeConstants.BaseRaces.DeathSlaad)]
        [TestCase(85, 85, SizeConstants.BaseRaces.YuanTiPureblood)]
        [TestCase(86, 86, SizeConstants.BaseRaces.YuanTiHalfblood)]
        [TestCase(87, 87, SizeConstants.BaseRaces.YuanTiAbomination)]
        [TestCase(88, 88, SizeConstants.BaseRaces.Scorpionfolk)]
        [TestCase(89, 89, SizeConstants.BaseRaces.Harpy)]
        [TestCase(90, 90, SizeConstants.BaseRaces.CloudGiant)]
        [TestCase(91, 91, SizeConstants.BaseRaces.Gnoll)]
        [TestCase(92, 94, SizeConstants.BaseRaces.Troglodyte)]
        [TestCase(95, 95, SizeConstants.BaseRaces.Bugbear)]
        [TestCase(96, 96, SizeConstants.BaseRaces.Ogre)]
        [TestCase(97, 97, SizeConstants.BaseRaces.Minotaur)]
        [TestCase(98, 98, SizeConstants.BaseRaces.MindFlayer)]
        [TestCase(99, 99, SizeConstants.BaseRaces.OgreMage)]
        [TestCase(100, 100, SizeConstants.BaseRaces.FireGiant)]
        public override void Percentile(int lower, int upper, string content)
        {
            base.Percentile(lower, upper, content);
        }
    }
}