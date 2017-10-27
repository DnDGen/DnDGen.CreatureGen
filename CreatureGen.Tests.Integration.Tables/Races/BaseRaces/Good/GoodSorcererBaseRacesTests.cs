using CreatureGen.Alignments;
using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Tables;
using CreatureGen.Creatures;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Races.BaseRaces.Good
{
    [TestFixture]
    public class GoodSorcererBaseRacesTests : PercentileTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Good, CharacterClassConstants.Sorcerer); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(1, 2, SizeConstants.BaseRaces.Aasimar)]
        [TestCase(3, 3, SizeConstants.BaseRaces.DeepDwarf)]
        [TestCase(4, 5, SizeConstants.BaseRaces.HillDwarf)]
        [TestCase(6, 6, SizeConstants.BaseRaces.MountainDwarf)]
        [TestCase(7, 8, SizeConstants.BaseRaces.GrayElf)]
        [TestCase(9, 11, SizeConstants.BaseRaces.HighElf)]
        [TestCase(12, 35, SizeConstants.BaseRaces.WildElf)]
        [TestCase(36, 36, SizeConstants.BaseRaces.HoundArchon)]
        [TestCase(37, 37, SizeConstants.BaseRaces.WoodElf)]
        [TestCase(38, 38, SizeConstants.BaseRaces.ForestGnome)]
        [TestCase(39, 40, SizeConstants.BaseRaces.RockGnome)]
        [TestCase(41, 45, SizeConstants.BaseRaces.HalfElf)]
        [TestCase(46, 54, SizeConstants.BaseRaces.LightfootHalfling)]
        [TestCase(55, 55, SizeConstants.BaseRaces.DeepHalfling)]
        [TestCase(56, 56, SizeConstants.BaseRaces.TallfellowHalfling)]
        [TestCase(57, 58, SizeConstants.BaseRaces.HalfOrc)]
        [TestCase(59, 95, SizeConstants.BaseRaces.Human)]
        [TestCase(96, 97, SizeConstants.BaseRaces.Pixie)]
        [TestCase(98, 98, SizeConstants.BaseRaces.StormGiant)]
        [TestCase(99, 99, SizeConstants.BaseRaces.Svirfneblin)]
        [TestCase(100, 100, SizeConstants.BaseRaces.CloudGiant)]
        public override void Percentile(int lower, int upper, string content)
        {
            base.Percentile(lower, upper, content);
        }
    }
}