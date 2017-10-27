using CreatureGen.Alignments;
using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Tables;
using CreatureGen.Creatures;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Races.BaseRaces.Good
{
    [TestFixture]
    public class GoodBarbarianBaseRacesTests : PercentileTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Good, CharacterClassConstants.Barbarian); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(1, 2, SizeConstants.BaseRaces.HillDwarf)]
        [TestCase(3, 32, SizeConstants.BaseRaces.WildElf)]
        [TestCase(33, 34, SizeConstants.BaseRaces.WoodElf)]
        [TestCase(35, 35, SizeConstants.BaseRaces.HalfElf)]
        [TestCase(36, 36, SizeConstants.BaseRaces.LightfootHalfling)]
        [TestCase(37, 61, SizeConstants.BaseRaces.HalfOrc)]
        [TestCase(62, 97, SizeConstants.BaseRaces.Human)]
        [TestCase(98, 98, SizeConstants.BaseRaces.StormGiant)]
        [TestCase(99, 99, SizeConstants.BaseRaces.CloudGiant)]
        [TestCase(100, 100, SizeConstants.BaseRaces.Centaur)]
        public override void Percentile(int lower, int upper, string content)
        {
            base.Percentile(lower, upper, content);
        }
    }
}