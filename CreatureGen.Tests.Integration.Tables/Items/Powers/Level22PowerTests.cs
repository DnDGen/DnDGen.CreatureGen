using CreatureGen.Domain.Tables;
using NUnit.Framework;
using TreasureGen.Items;

namespace CreatureGen.Tests.Integration.Tables.Items.Powers
{
    [TestFixture]
    public class Level22PowerTests : PercentileTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Percentile.LevelXPower, 22); }
        }

        [TestCase(1, 35, PowerConstants.Medium)]
        [TestCase(36, 100, PowerConstants.Major)]
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
