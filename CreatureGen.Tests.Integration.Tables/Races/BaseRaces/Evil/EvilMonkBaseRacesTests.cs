using CreatureGen.Alignments;
using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Tables;
using CreatureGen.Creatures;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Races.BaseRaces.Evil
{
    [TestFixture]
    public class EvilMonkBaseRacesTests : PercentileTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Evil, CharacterClassConstants.Monk); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(1, 10, SizeConstants.BaseRaces.HalfElf)]
        [TestCase(11, 20, SizeConstants.BaseRaces.HalfOrc)]
        [TestCase(21, 92, SizeConstants.BaseRaces.Human)]
        [TestCase(93, 93, SizeConstants.BaseRaces.Scorpionfolk)]
        [TestCase(94, 96, SizeConstants.BaseRaces.Hobgoblin)]
        [TestCase(97, 97, SizeConstants.BaseRaces.Tiefling)]
        [TestCase(98, 99, SizeConstants.BaseRaces.OgreMage)]
        [TestCase(100, 100, SizeConstants.BaseRaces.FireGiant)]
        public override void Percentile(int lower, int upper, string content)
        {
            base.Percentile(lower, upper, content);
        }
    }
}