using CreatureGen.Domain.Tables;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Leadership
{
    [TestFixture]
    public class KilledCohortTests : BooleanPercentileTests
    {
        protected override string tableName
        {
            get
            {
                return TableNameConstants.Set.TrueOrFalse.KilledCohort;
            }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(1, 10, true)]
        [TestCase(11, 100, false)]
        public override void BooleanPercentile(int lower, int upper, bool isTrue)
        {
            base.BooleanPercentile(lower, upper, isTrue);
        }
    }
}
