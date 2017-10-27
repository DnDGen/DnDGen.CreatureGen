using CreatureGen.Alignments;
using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Tables;
using CreatureGen.Creatures;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Races.BaseRaces.Neutral
{
    [TestFixture]
    public class NeutralMonkBaseRacesTests : PercentileTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Neutral, CharacterClassConstants.Monk); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(1, 2, SizeConstants.BaseRaces.HighElf)]
        [TestCase(3, 3, SizeConstants.BaseRaces.WoodElf)]
        [TestCase(4, 13, SizeConstants.BaseRaces.HalfElf)]
        [TestCase(14, 14, SizeConstants.BaseRaces.LightfootHalfling)]
        [TestCase(15, 15, SizeConstants.BaseRaces.DeepHalfling)]
        [TestCase(16, 25, SizeConstants.BaseRaces.HalfOrc)]
        [TestCase(26, 95, SizeConstants.BaseRaces.Human)]
        [TestCase(96, 97, SizeConstants.BaseRaces.Githzerai)]
        [TestCase(98, 98, SizeConstants.BaseRaces.Azer)]
        [TestCase(99, 99, SizeConstants.BaseRaces.Janni)]
        [TestCase(100, 100, SizeConstants.BaseRaces.StoneGiant)]
        public override void Percentile(int lower, int upper, string content)
        {
            base.Percentile(lower, upper, content);
        }
    }
}