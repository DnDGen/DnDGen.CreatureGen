using CreatureGen.Alignments;
using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Tables;
using CreatureGen.Creatures;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Races.BaseRaces.Evil
{
    [TestFixture]
    public class EvilBarbarianBaseRacesTests : PercentileTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Evil, CharacterClassConstants.Barbarian); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(1, 1, SizeConstants.BaseRaces.WildElf)]
        [TestCase(2, 3, SizeConstants.BaseRaces.WoodElf)]
        [TestCase(4, 4, SizeConstants.BaseRaces.HalfElf)]
        [TestCase(5, 5, SizeConstants.BaseRaces.LightfootHalfling)]
        [TestCase(6, 6, SizeConstants.BaseRaces.DeepHalfling)]
        [TestCase(7, 29, SizeConstants.BaseRaces.HalfOrc)]
        [TestCase(30, 42, SizeConstants.BaseRaces.Human)]
        [TestCase(43, 44, SizeConstants.BaseRaces.HillGiant)]
        [TestCase(45, 49, SizeConstants.BaseRaces.Lizardfolk)]
        [TestCase(50, 50, SizeConstants.BaseRaces.Goblin)]
        [TestCase(51, 51, SizeConstants.BaseRaces.Hobgoblin)]
        [TestCase(52, 52, SizeConstants.BaseRaces.Kobold)]
        [TestCase(53, 74, SizeConstants.BaseRaces.Orc)]
        [TestCase(75, 75, SizeConstants.BaseRaces.DeathSlaad)]
        [TestCase(76, 76, SizeConstants.BaseRaces.YuanTiHalfblood)]
        [TestCase(77, 77, SizeConstants.BaseRaces.YuanTiAbomination)]
        [TestCase(78, 78, SizeConstants.BaseRaces.Gargoyle)]
        [TestCase(79, 79, SizeConstants.BaseRaces.Troll)]
        [TestCase(80, 80, SizeConstants.BaseRaces.CloudGiant)]
        [TestCase(81, 82, SizeConstants.BaseRaces.Grimlock)]
        [TestCase(83, 83, SizeConstants.BaseRaces.Tiefling)]
        [TestCase(84, 88, SizeConstants.BaseRaces.Gnoll)]
        [TestCase(89, 89, SizeConstants.BaseRaces.Troglodyte)]
        [TestCase(90, 91, SizeConstants.BaseRaces.Bugbear)]
        [TestCase(92, 95, SizeConstants.BaseRaces.Ogre)]
        [TestCase(96, 99, SizeConstants.BaseRaces.Minotaur)]
        [TestCase(100, 100, SizeConstants.BaseRaces.FrostGiant)]
        public override void Percentile(int lower, int upper, string content)
        {
            base.Percentile(lower, upper, content);
        }
    }
}