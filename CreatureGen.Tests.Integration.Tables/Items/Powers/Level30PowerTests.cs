using CreatureGen.Domain.Tables;
using NUnit.Framework;
using TreasureGen.Items;

namespace CreatureGen.Tests.Integration.Tables.Items.Powers
{
    [TestFixture]
    public class Level30PowerTests : PercentileTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Percentile.LevelXPower, 30); }
        }

        [TestCase(1, 6, PowerConstants.Medium)]
        [TestCase(7, 100, PowerConstants.Major)]
        public override void Percentile(int lower, int upper, string content)
        {
            base.Percentile(lower, upper, content);
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }
    }
}
