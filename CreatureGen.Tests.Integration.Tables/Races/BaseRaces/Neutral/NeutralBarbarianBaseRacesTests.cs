using CreatureGen.Alignments;
using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Tables;
using CreatureGen.Creatures;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Races.BaseRaces.Neutral
{
    [TestFixture]
    public class NeutralBarbarianBaseRacesTests : PercentileTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Neutral, CharacterClassConstants.Barbarian); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(1, 1, SizeConstants.BaseRaces.DeepDwarf)]
        [TestCase(2, 2, SizeConstants.BaseRaces.HillDwarf)]
        [TestCase(3, 13, SizeConstants.BaseRaces.WildElf)]
        [TestCase(14, 14, SizeConstants.BaseRaces.WoodElf)]
        [TestCase(15, 16, SizeConstants.BaseRaces.HalfElf)]
        [TestCase(17, 18, SizeConstants.BaseRaces.LightfootHalfling)]
        [TestCase(19, 19, SizeConstants.BaseRaces.DeepHalfling)]
        [TestCase(20, 58, SizeConstants.BaseRaces.HalfOrc)]
        [TestCase(59, 87, SizeConstants.BaseRaces.Human)]
        [TestCase(88, 88, SizeConstants.BaseRaces.Janni)]
        [TestCase(89, 95, SizeConstants.BaseRaces.Lizardfolk)]
        [TestCase(96, 96, SizeConstants.BaseRaces.RedSlaad)]
        [TestCase(97, 97, SizeConstants.BaseRaces.BlueSlaad)]
        [TestCase(98, 98, SizeConstants.BaseRaces.GreenSlaad)]
        [TestCase(99, 99, SizeConstants.BaseRaces.GraySlaad)]
        [TestCase(100, 100, SizeConstants.BaseRaces.StoneGiant)]
        public override void Percentile(int lower, int upper, string content)
        {
            base.Percentile(lower, upper, content);
        }
    }
}