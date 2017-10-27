using CreatureGen.Alignments;
using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Tables;
using CreatureGen.Creatures;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Races.BaseRaces.Neutral
{
    [TestFixture]
    public class NeutralAdeptBaseRacesTests : PercentileTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Neutral, CharacterClassConstants.Adept); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(1, 15, SizeConstants.BaseRaces.DeepDwarf)]
        [TestCase(16, 25, SizeConstants.BaseRaces.HillDwarf)]
        [TestCase(26, 26, SizeConstants.BaseRaces.MountainDwarf)]
        [TestCase(27, 27, SizeConstants.BaseRaces.HighElf)]
        [TestCase(28, 28, SizeConstants.BaseRaces.WildElf)]
        [TestCase(29, 38, SizeConstants.BaseRaces.WoodElf)]
        [TestCase(39, 39, SizeConstants.BaseRaces.RockGnome)]
        [TestCase(40, 48, SizeConstants.BaseRaces.HalfElf)]
        [TestCase(49, 58, SizeConstants.BaseRaces.LightfootHalfling)]
        [TestCase(59, 59, SizeConstants.BaseRaces.DeepHalfling)]
        [TestCase(60, 60, SizeConstants.BaseRaces.TallfellowHalfling)]
        [TestCase(61, 62, SizeConstants.BaseRaces.HalfOrc)]
        [TestCase(63, 100, SizeConstants.BaseRaces.Human)]
        public override void Percentile(int lower, int upper, string content)
        {
            base.Percentile(lower, upper, content);
        }
    }
}
