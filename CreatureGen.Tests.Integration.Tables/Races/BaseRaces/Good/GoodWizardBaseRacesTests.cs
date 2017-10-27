using CreatureGen.Alignments;
using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Tables;
using CreatureGen.Creatures;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Races.BaseRaces.Good
{
    [TestFixture]
    public class GoodWizardBaseRacesTests : PercentileTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Good, CharacterClassConstants.Wizard); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(1, 1, SizeConstants.BaseRaces.Aasimar)]
        [TestCase(2, 2, SizeConstants.BaseRaces.HillDwarf)]
        [TestCase(3, 7, SizeConstants.BaseRaces.GrayElf)]
        [TestCase(8, 39, SizeConstants.BaseRaces.HighElf)]
        [TestCase(40, 40, SizeConstants.BaseRaces.Pixie)]
        [TestCase(41, 41, SizeConstants.BaseRaces.StormGiant)]
        [TestCase(42, 42, SizeConstants.BaseRaces.WoodElf)]
        [TestCase(43, 43, SizeConstants.BaseRaces.ForestGnome)]
        [TestCase(44, 48, SizeConstants.BaseRaces.RockGnome)]
        [TestCase(49, 58, SizeConstants.BaseRaces.HalfElf)]
        [TestCase(59, 63, SizeConstants.BaseRaces.LightfootHalfling)]
        [TestCase(64, 64, SizeConstants.BaseRaces.DeepHalfling)]
        [TestCase(65, 67, SizeConstants.BaseRaces.TallfellowHalfling)]
        [TestCase(68, 68, SizeConstants.BaseRaces.HalfOrc)]
        [TestCase(69, 99, SizeConstants.BaseRaces.Human)]
        [TestCase(100, 100, SizeConstants.BaseRaces.Svirfneblin)]
        public override void Percentile(int lower, int upper, string content)
        {
            base.Percentile(lower, upper, content);
        }
    }
}