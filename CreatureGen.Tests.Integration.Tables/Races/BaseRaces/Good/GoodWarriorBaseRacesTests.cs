using CreatureGen.Alignments;
using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Tables;
using CreatureGen.Creatures;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Races.BaseRaces.Good
{
    [TestFixture]
    public class GoodWarriorBaseRacesTests : PercentileTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Good, CharacterClassConstants.Warrior); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(1, 3, SizeConstants.BaseRaces.DeepDwarf)]
        [TestCase(4, 33, SizeConstants.BaseRaces.HillDwarf)]
        [TestCase(34, 41, SizeConstants.BaseRaces.MountainDwarf)]
        [TestCase(42, 42, SizeConstants.BaseRaces.GrayElf)]
        [TestCase(43, 47, SizeConstants.BaseRaces.HighElf)]
        [TestCase(48, 48, SizeConstants.BaseRaces.RockGnome)]
        [TestCase(49, 50, SizeConstants.BaseRaces.HalfElf)]
        [TestCase(51, 51, SizeConstants.BaseRaces.LightfootHalfling)]
        [TestCase(52, 52, SizeConstants.BaseRaces.DeepHalfling)]
        [TestCase(53, 57, SizeConstants.BaseRaces.HalfOrc)]
        [TestCase(58, 100, SizeConstants.BaseRaces.Human)]
        public override void Percentile(int lower, int upper, string content)
        {
            base.Percentile(lower, upper, content);
        }
    }
}