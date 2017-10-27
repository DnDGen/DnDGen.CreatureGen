using CreatureGen.Alignments;
using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Tables;
using CreatureGen.Creatures;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Races.BaseRaces.Neutral
{
    [TestFixture]
    public class NeutralFighterBaseRacesTests : PercentileTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Neutral, CharacterClassConstants.Fighter); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(1, 10, SizeConstants.BaseRaces.DeepDwarf)]
        [TestCase(11, 27, SizeConstants.BaseRaces.HillDwarf)]
        [TestCase(28, 29, SizeConstants.BaseRaces.Azer)]
        [TestCase(30, 34, SizeConstants.BaseRaces.MountainDwarf)]
        [TestCase(35, 35, SizeConstants.BaseRaces.HighElf)]
        [TestCase(36, 41, SizeConstants.BaseRaces.WoodElf)]
        [TestCase(42, 46, SizeConstants.BaseRaces.HalfElf)]
        [TestCase(47, 47, SizeConstants.BaseRaces.LightfootHalfling)]
        [TestCase(48, 48, SizeConstants.BaseRaces.DeepHalfling)]
        [TestCase(49, 58, SizeConstants.BaseRaces.HalfOrc)]
        [TestCase(59, 93, SizeConstants.BaseRaces.Human)]
        [TestCase(94, 94, SizeConstants.BaseRaces.RedSlaad)]
        [TestCase(95, 95, SizeConstants.BaseRaces.BlueSlaad)]
        [TestCase(96, 96, SizeConstants.BaseRaces.GreenSlaad)]
        [TestCase(97, 97, SizeConstants.BaseRaces.GraySlaad)]
        [TestCase(98, 98, SizeConstants.BaseRaces.Lizardfolk)]
        [TestCase(99, 99, SizeConstants.BaseRaces.Doppelganger)]
        [TestCase(100, 100, SizeConstants.BaseRaces.Janni)]
        public override void Percentile(int lower, int upper, string content)
        {
            base.Percentile(lower, upper, content);
        }
    }
}