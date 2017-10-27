using CreatureGen.Alignments;
using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Tables;
using CreatureGen.Creatures;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Races.BaseRaces.Evil
{
    [TestFixture]
    public class EvilAdeptBaseRacesTests : PercentileTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Evil, CharacterClassConstants.Adept); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(1, 2, SizeConstants.BaseRaces.DeepDwarf)]
        [TestCase(3, 3, SizeConstants.BaseRaces.HillDwarf)]
        [TestCase(4, 4, SizeConstants.BaseRaces.HighElf)]
        [TestCase(5, 5, SizeConstants.BaseRaces.WildElf)]
        [TestCase(6, 8, SizeConstants.BaseRaces.WoodElf)]
        [TestCase(9, 18, SizeConstants.BaseRaces.HalfElf)]
        [TestCase(19, 20, SizeConstants.BaseRaces.LightfootHalfling)]
        [TestCase(21, 21, SizeConstants.BaseRaces.DeepHalfling)]
        [TestCase(22, 22, SizeConstants.BaseRaces.TallfellowHalfling)]
        [TestCase(23, 25, SizeConstants.BaseRaces.HalfOrc)]
        [TestCase(26, 99, SizeConstants.BaseRaces.Human)]
        [TestCase(100, 100, SizeConstants.BaseRaces.Tiefling)]
        public override void Percentile(int lower, int upper, string content)
        {
            base.Percentile(lower, upper, content);
        }
    }
}
