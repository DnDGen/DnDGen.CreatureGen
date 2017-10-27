using CreatureGen.Alignments;
using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Tables;
using CreatureGen.Creatures;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Races.BaseRaces.Evil
{
    [TestFixture]
    public class EvilRogueBaseRacesTests : PercentileTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Evil, CharacterClassConstants.Rogue); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(1, 1, SizeConstants.BaseRaces.DeepDwarf)]
        [TestCase(2, 2, SizeConstants.BaseRaces.HighElf)]
        [TestCase(3, 3, SizeConstants.BaseRaces.WoodElf)]
        [TestCase(4, 18, SizeConstants.BaseRaces.HalfElf)]
        [TestCase(19, 38, SizeConstants.BaseRaces.LightfootHalfling)]
        [TestCase(39, 39, SizeConstants.BaseRaces.DeepHalfling)]
        [TestCase(40, 40, SizeConstants.BaseRaces.TallfellowHalfling)]
        [TestCase(41, 50, SizeConstants.BaseRaces.HalfOrc)]
        [TestCase(51, 73, SizeConstants.BaseRaces.Human)]
        [TestCase(74, 74, SizeConstants.BaseRaces.Scorpionfolk)]
        [TestCase(75, 75, SizeConstants.BaseRaces.Harpy)]
        [TestCase(76, 81, SizeConstants.BaseRaces.Goblin)]
        [TestCase(82, 83, SizeConstants.BaseRaces.DeathSlaad)]
        [TestCase(84, 84, SizeConstants.BaseRaces.YuanTiPureblood)]
        [TestCase(85, 85, SizeConstants.BaseRaces.YuanTiHalfblood)]
        [TestCase(86, 86, SizeConstants.BaseRaces.YuanTiAbomination)]
        [TestCase(87, 87, SizeConstants.BaseRaces.Gargoyle)]
        [TestCase(88, 88, SizeConstants.BaseRaces.Troll)]
        [TestCase(89, 89, SizeConstants.BaseRaces.Rakshasa)]
        [TestCase(90, 90, SizeConstants.BaseRaces.Grimlock)]
        [TestCase(91, 91, SizeConstants.BaseRaces.Hobgoblin)]
        [TestCase(92, 92, SizeConstants.BaseRaces.Kobold)]
        [TestCase(93, 94, SizeConstants.BaseRaces.Tiefling)]
        [TestCase(95, 98, SizeConstants.BaseRaces.Bugbear)]
        [TestCase(99, 99, SizeConstants.BaseRaces.MindFlayer)]
        [TestCase(100, 100, SizeConstants.BaseRaces.CloudGiant)]
        public override void Percentile(int lower, int upper, string content)
        {
            base.Percentile(lower, upper, content);
        }
    }
}