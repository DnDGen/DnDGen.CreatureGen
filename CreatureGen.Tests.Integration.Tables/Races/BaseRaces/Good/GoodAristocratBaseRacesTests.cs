using CreatureGen.Alignments;
using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Tables;
using CreatureGen.Creatures;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Races.BaseRaces.Good
{
    [TestFixture]
    public class GoodAristocratBaseRacesTests : PercentileTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Good, CharacterClassConstants.Aristocrat); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(1, 1, SizeConstants.BaseRaces.Aasimar)]
        [TestCase(2, 6, SizeConstants.BaseRaces.HillDwarf)]
        [TestCase(7, 11, SizeConstants.BaseRaces.GrayElf)]
        [TestCase(12, 36, SizeConstants.BaseRaces.HighElf)]
        [TestCase(37, 37, SizeConstants.BaseRaces.WildElf)]
        [TestCase(38, 38, SizeConstants.BaseRaces.WoodElf)]
        [TestCase(39, 39, SizeConstants.BaseRaces.ForestGnome)]
        [TestCase(40, 44, SizeConstants.BaseRaces.RockGnome)]
        [TestCase(45, 53, SizeConstants.BaseRaces.HalfElf)]
        [TestCase(54, 54, SizeConstants.BaseRaces.LightfootHalfling)]
        [TestCase(55, 55, SizeConstants.BaseRaces.DeepHalfling)]
        [TestCase(56, 57, SizeConstants.BaseRaces.TallfellowHalfling)]
        [TestCase(58, 100, SizeConstants.BaseRaces.Human)]
        public override void Percentile(int lower, int upper, string content)
        {
            base.Percentile(lower, upper, content);
        }
    }
}