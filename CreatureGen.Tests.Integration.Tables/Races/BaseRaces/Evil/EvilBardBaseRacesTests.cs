using CreatureGen.Alignments;
using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Tables;
using CreatureGen.Creatures;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Races.BaseRaces.Evil
{
    [TestFixture]
    public class EvilBardBaseRacesTests : PercentileTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Evil, CharacterClassConstants.Bard); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(1, 1, SizeConstants.BaseRaces.HighElf)]
        [TestCase(2, 2, SizeConstants.BaseRaces.WoodElf)]
        [TestCase(3, 17, SizeConstants.BaseRaces.HalfElf)]
        [TestCase(18, 18, SizeConstants.BaseRaces.LightfootHalfling)]
        [TestCase(19, 19, SizeConstants.BaseRaces.DeepHalfling)]
        [TestCase(20, 20, SizeConstants.BaseRaces.TallfellowHalfling)]
        [TestCase(21, 22, SizeConstants.BaseRaces.HalfOrc)]
        [TestCase(23, 92, SizeConstants.BaseRaces.Human)]
        [TestCase(93, 93, SizeConstants.BaseRaces.DeathSlaad)]
        [TestCase(94, 94, SizeConstants.BaseRaces.YuanTiPureblood)]
        [TestCase(95, 95, SizeConstants.BaseRaces.YuanTiHalfblood)]
        [TestCase(96, 96, SizeConstants.BaseRaces.YuanTiAbomination)]
        [TestCase(97, 97, SizeConstants.BaseRaces.Harpy)]
        [TestCase(98, 98, SizeConstants.BaseRaces.Goblin)]
        [TestCase(99, 99, SizeConstants.BaseRaces.Tiefling)]
        [TestCase(100, 100, SizeConstants.BaseRaces.CloudGiant)]
        public override void Percentile(int lower, int upper, string content)
        {
            base.Percentile(lower, upper, content);
        }
    }
}