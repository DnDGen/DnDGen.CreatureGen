using CreatureGen.Domain.Tables;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Leadership
{
    [TestFixture]
    public class KilledFollowersTests : BooleanPercentileTests
    {
        protected override string tableName
        {
            get
            {
                return TableNameConstants.Set.TrueOrFalse.KilledFollowers;
            }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(1, 20, true)]
        [TestCase(21, 100, false)]
        public override void BooleanPercentile(int lower, int upper, bool isTrue)
        {
            base.BooleanPercentile(lower, upper, isTrue);
        }
    }
}
