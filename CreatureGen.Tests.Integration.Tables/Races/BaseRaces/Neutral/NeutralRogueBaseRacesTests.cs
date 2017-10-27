using CreatureGen.Alignments;
using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Tables;
using CreatureGen.Creatures;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Races.BaseRaces.Neutral
{
    [TestFixture]
    public class NeutralRogueBaseRacesTests : PercentileTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Neutral, CharacterClassConstants.Rogue); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(1, 1, SizeConstants.BaseRaces.DeepDwarf)]
        [TestCase(2, 4, SizeConstants.BaseRaces.HillDwarf)]
        [TestCase(5, 8, SizeConstants.BaseRaces.HighElf)]
        [TestCase(9, 9, SizeConstants.BaseRaces.WoodElf)]
        [TestCase(10, 10, SizeConstants.BaseRaces.RockGnome)]
        [TestCase(11, 25, SizeConstants.BaseRaces.HalfElf)]
        [TestCase(26, 46, SizeConstants.BaseRaces.LightfootHalfling)]
        [TestCase(47, 47, SizeConstants.BaseRaces.RedSlaad)]
        [TestCase(48, 48, SizeConstants.BaseRaces.BlueSlaad)]
        [TestCase(49, 49, SizeConstants.BaseRaces.GreenSlaad)]
        [TestCase(50, 50, SizeConstants.BaseRaces.GraySlaad)]
        [TestCase(51, 51, SizeConstants.BaseRaces.Azer)]
        [TestCase(52, 52, SizeConstants.BaseRaces.Satyr)]
        [TestCase(53, 53, SizeConstants.BaseRaces.StoneGiant)]
        [TestCase(54, 58, SizeConstants.BaseRaces.DeepHalfling)]
        [TestCase(59, 63, SizeConstants.BaseRaces.TallfellowHalfling)]
        [TestCase(64, 72, SizeConstants.BaseRaces.HalfOrc)]
        [TestCase(73, 73, SizeConstants.BaseRaces.Githzerai)]
        [TestCase(74, 97, SizeConstants.BaseRaces.Human)]
        [TestCase(98, 98, SizeConstants.BaseRaces.Doppelganger)]
        [TestCase(99, 100, SizeConstants.BaseRaces.Janni)]
        public override void Percentile(int lower, int upper, string content)
        {
            base.Percentile(lower, upper, content);
        }
    }
}