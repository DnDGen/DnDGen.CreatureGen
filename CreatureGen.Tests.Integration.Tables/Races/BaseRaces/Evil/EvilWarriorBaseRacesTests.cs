using CreatureGen.Alignments;
using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Tables;
using CreatureGen.Creatures;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Races.BaseRaces.Evil
{
    [TestFixture]
    public class EvilWarriorBaseRacesTests : PercentileTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Evil, CharacterClassConstants.Warrior); }
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
        [TestCase(24, 100, SizeConstants.BaseRaces.Human)]
        public override void Percentile(int lower, int upper, string content)
        {
            base.Percentile(lower, upper, content);
        }
    }
}