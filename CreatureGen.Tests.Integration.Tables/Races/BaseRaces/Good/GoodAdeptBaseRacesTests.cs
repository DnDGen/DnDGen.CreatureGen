using CreatureGen.Alignments;
using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Tables;
using CreatureGen.Creatures;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Races.BaseRaces.Good
{
    [TestFixture]
    public class GoodAdeptBaseRacesTests : PercentileTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Good, CharacterClassConstants.Adept); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(1, 1, SizeConstants.BaseRaces.Aasimar)]
        [TestCase(2, 2, SizeConstants.BaseRaces.DeepDwarf)]
        [TestCase(3, 21, SizeConstants.BaseRaces.HillDwarf)]
        [TestCase(22, 24, SizeConstants.BaseRaces.MountainDwarf)]
        [TestCase(25, 25, SizeConstants.BaseRaces.GrayElf)]
        [TestCase(26, 35, SizeConstants.BaseRaces.HighElf)]
        [TestCase(36, 40, SizeConstants.BaseRaces.WildElf)]
        [TestCase(41, 41, SizeConstants.BaseRaces.WoodElf)]
        [TestCase(42, 42, SizeConstants.BaseRaces.ForestGnome)]
        [TestCase(43, 51, SizeConstants.BaseRaces.RockGnome)]
        [TestCase(52, 56, SizeConstants.BaseRaces.HalfElf)]
        [TestCase(57, 66, SizeConstants.BaseRaces.LightfootHalfling)]
        [TestCase(67, 67, SizeConstants.BaseRaces.DeepHalfling)]
        [TestCase(68, 69, SizeConstants.BaseRaces.TallfellowHalfling)]
        [TestCase(70, 70, SizeConstants.BaseRaces.HalfOrc)]
        [TestCase(71, 100, SizeConstants.BaseRaces.Human)]
        public override void Percentile(int lower, int upper, string content)
        {
            base.Percentile(lower, upper, content);
        }
    }
}
