using CreatureGen.Alignments;
using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Tables;
using CreatureGen.Creatures;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Races.BaseRaces.Neutral
{
    [TestFixture]
    public class NeutralSorcererBaseRacesTests : PercentileTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Neutral, CharacterClassConstants.Sorcerer); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(1, 1, SizeConstants.BaseRaces.HillDwarf)]
        [TestCase(2, 2, SizeConstants.BaseRaces.HighElf)]
        [TestCase(3, 12, SizeConstants.BaseRaces.WildElf)]
        [TestCase(13, 15, SizeConstants.BaseRaces.WoodElf)]
        [TestCase(16, 16, SizeConstants.BaseRaces.RockGnome)]
        [TestCase(17, 31, SizeConstants.BaseRaces.HalfElf)]
        [TestCase(32, 41, SizeConstants.BaseRaces.LightfootHalfling)]
        [TestCase(42, 42, SizeConstants.BaseRaces.DeepHalfling)]
        [TestCase(43, 43, SizeConstants.BaseRaces.TallfellowHalfling)]
        [TestCase(44, 48, SizeConstants.BaseRaces.HalfOrc)]
        [TestCase(49, 90, SizeConstants.BaseRaces.Human)]
        [TestCase(91, 91, SizeConstants.BaseRaces.Githzerai)]
        [TestCase(92, 92, SizeConstants.BaseRaces.GreenSlaad)]
        [TestCase(93, 94, SizeConstants.BaseRaces.GraySlaad)]
        [TestCase(95, 95, SizeConstants.BaseRaces.Satyr)]
        [TestCase(96, 96, SizeConstants.BaseRaces.Janni)]
        [TestCase(97, 98, SizeConstants.BaseRaces.Lizardfolk)]
        [TestCase(99, 99, SizeConstants.BaseRaces.Doppelganger)]
        [TestCase(100, 100, SizeConstants.BaseRaces.StoneGiant)]
        public override void Percentile(int lower, int upper, string content)
        {
            base.Percentile(lower, upper, content);
        }
    }
}