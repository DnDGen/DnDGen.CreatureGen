using CreatureGen.Domain.Tables;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Leadership
{
    [TestFixture]
    public class ReputationTests : PercentileTests
    {
        protected override string tableName
        {
            get
            {
                return TableNameConstants.Set.Percentile.Reputation;
            }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(1, 5, "Cruelty")]
        [TestCase(6, 15, "Aloofness")]
        [TestCase(16, 25, "Failure")]
        [TestCase(26, 75, EmptyContent)]
        [TestCase(76, 85, "Special power")]
        [TestCase(86, 95, "Fairness and generosity")]
        [TestCase(96, 100, "Great renown")]
        public override void Percentile(int lower, int upper, string content)
        {
            base.Percentile(lower, upper, content);
        }
    }
}
