using CreatureGen.Alignments;
using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Tables;
using CreatureGen.Creatures;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Races.BaseRaces.Neutral
{
    [TestFixture]
    public class NeutralWizardBaseRacesTests : PercentileTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Neutral, CharacterClassConstants.Wizard); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(1, 1, SizeConstants.BaseRaces.GrayElf)]
        [TestCase(2, 26, SizeConstants.BaseRaces.HighElf)]
        [TestCase(27, 28, SizeConstants.BaseRaces.WoodElf)]
        [TestCase(29, 29, SizeConstants.BaseRaces.RockGnome)]
        [TestCase(30, 44, SizeConstants.BaseRaces.HalfElf)]
        [TestCase(45, 47, SizeConstants.BaseRaces.LightfootHalfling)]
        [TestCase(48, 49, SizeConstants.BaseRaces.TallfellowHalfling)]
        [TestCase(50, 50, SizeConstants.BaseRaces.HalfOrc)]
        [TestCase(51, 95, SizeConstants.BaseRaces.Human)]
        [TestCase(96, 96, SizeConstants.BaseRaces.GraySlaad)]
        [TestCase(97, 97, SizeConstants.BaseRaces.Azer)]
        [TestCase(98, 98, SizeConstants.BaseRaces.Satyr)]
        [TestCase(99, 99, SizeConstants.BaseRaces.Doppelganger)]
        [TestCase(100, 100, SizeConstants.BaseRaces.Janni)]
        public override void Percentile(int lower, int upper, string content)
        {
            base.Percentile(lower, upper, content);
        }
    }
}