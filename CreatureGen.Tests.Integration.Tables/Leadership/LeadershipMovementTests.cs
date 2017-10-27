using CreatureGen.Domain.Tables;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Leadership
{
    [TestFixture]
    public class LeadershipMovementTests : PercentileTests
    {
        protected override string tableName
        {
            get
            {
                return TableNameConstants.Set.Percentile.LeadershipMovement;
            }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(1, 20, "Moves around a lot")]
        [TestCase(21, 90, EmptyContent)]
        [TestCase(91, 100, "Has a stronghold, base of operations, guildhouse, or the like")]
        public override void Percentile(int lower, int upper, string content)
        {
            base.Percentile(lower, upper, content);
        }
    }
}
