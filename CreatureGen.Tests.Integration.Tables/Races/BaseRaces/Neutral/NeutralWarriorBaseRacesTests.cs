using CreatureGen.Alignments;
using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Tables;
using CreatureGen.Creatures;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Races.BaseRaces.Neutral
{
    [TestFixture]
    public class NeutralWarriorBaseRacesTests : PercentileTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Neutral, CharacterClassConstants.Warrior); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(1, 10, SizeConstants.BaseRaces.DeepDwarf)]
        [TestCase(11, 29, SizeConstants.BaseRaces.HillDwarf)]
        [TestCase(30, 34, SizeConstants.BaseRaces.MountainDwarf)]
        [TestCase(35, 35, SizeConstants.BaseRaces.HighElf)]
        [TestCase(36, 41, SizeConstants.BaseRaces.WoodElf)]
        [TestCase(42, 46, SizeConstants.BaseRaces.HalfElf)]
        [TestCase(47, 47, SizeConstants.BaseRaces.LightfootHalfling)]
        [TestCase(48, 48, SizeConstants.BaseRaces.DeepHalfling)]
        [TestCase(49, 58, SizeConstants.BaseRaces.HalfOrc)]
        [TestCase(59, 100, SizeConstants.BaseRaces.Human)]
        public override void Percentile(int lower, int upper, string content)
        {
            base.Percentile(lower, upper, content);
        }
    }
}