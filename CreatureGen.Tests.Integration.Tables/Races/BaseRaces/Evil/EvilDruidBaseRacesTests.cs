using CreatureGen.Alignments;
using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Tables;
using CreatureGen.Creatures;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Races.BaseRaces.Evil
{
    [TestFixture]
    public class EvilDruidBaseRacesTests : PercentileTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Evil, CharacterClassConstants.Druid); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(1, 2, SizeConstants.BaseRaces.WoodElf)]
        [TestCase(3, 3, SizeConstants.BaseRaces.HalfElf)]
        [TestCase(4, 4, SizeConstants.BaseRaces.TallfellowHalfling)]
        [TestCase(5, 6, SizeConstants.BaseRaces.HalfOrc)]
        [TestCase(7, 56, SizeConstants.BaseRaces.Human)]
        [TestCase(57, 71, SizeConstants.BaseRaces.Lizardfolk)]
        [TestCase(72, 72, SizeConstants.BaseRaces.Goblin)]
        [TestCase(73, 73, SizeConstants.BaseRaces.Hobgoblin)]
        [TestCase(74, 74, SizeConstants.BaseRaces.Kobold)]
        [TestCase(75, 75, SizeConstants.BaseRaces.Orc)]
        [TestCase(76, 99, SizeConstants.BaseRaces.Gnoll)]
        [TestCase(100, 100, SizeConstants.BaseRaces.CloudGiant)]
        public override void Percentile(int lower, int upper, string content)
        {
            base.Percentile(lower, upper, content);
        }
    }
}