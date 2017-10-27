using CreatureGen.Alignments;
using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Tables;
using CreatureGen.Creatures;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Races.BaseRaces.Neutral
{
    [TestFixture]
    public class NeutralBardBaseRacesTests : PercentileTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Neutral, CharacterClassConstants.Bard); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(1, 1, SizeConstants.BaseRaces.DeepDwarf)]
        [TestCase(2, 3, SizeConstants.BaseRaces.HillDwarf)]
        [TestCase(4, 5, SizeConstants.BaseRaces.GrayElf)]
        [TestCase(6, 15, SizeConstants.BaseRaces.HighElf)]
        [TestCase(16, 16, SizeConstants.BaseRaces.WildElf)]
        [TestCase(17, 21, SizeConstants.BaseRaces.WoodElf)]
        [TestCase(22, 23, SizeConstants.BaseRaces.RockGnome)]
        [TestCase(24, 33, SizeConstants.BaseRaces.HalfElf)]
        [TestCase(34, 36, SizeConstants.BaseRaces.LightfootHalfling)]
        [TestCase(37, 37, SizeConstants.BaseRaces.DeepHalfling)]
        [TestCase(38, 38, SizeConstants.BaseRaces.TallfellowHalfling)]
        [TestCase(39, 40, SizeConstants.BaseRaces.HalfOrc)]
        [TestCase(41, 96, SizeConstants.BaseRaces.Human)]
        [TestCase(97, 97, SizeConstants.BaseRaces.GraySlaad)]
        [TestCase(98, 98, SizeConstants.BaseRaces.Janni)]
        [TestCase(99, 100, SizeConstants.BaseRaces.Satyr)]
        public override void Percentile(int lower, int upper, string content)
        {
            base.Percentile(lower, upper, content);
        }
    }
}