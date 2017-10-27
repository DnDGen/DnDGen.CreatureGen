using CreatureGen.Domain.Tables;
using NUnit.Framework;
using TreasureGen.Items;

namespace CreatureGen.Tests.Integration.Tables.Items.Powers
{
    [TestFixture]
    public class Level14PowerTests : PercentileTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Percentile.LevelXPower, 14); }
        }

        [TestCase(1, 19, PowerConstants.Mundane)]
        [TestCase(20, 58, PowerConstants.Minor)]
        [TestCase(59, 92, PowerConstants.Medium)]
        [TestCase(93, 100, PowerConstants.Major)]
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
