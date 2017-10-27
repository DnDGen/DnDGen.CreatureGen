using CreatureGen.Domain.Tables;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Races
{
    [TestFixture]
    public class MaleTests : BooleanPercentileTests
    {
        protected override string tableName
        {
            get { return TableNameConstants.Set.TrueOrFalse.Male; }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(1, 50, true)]
        [TestCase(51, 100, false)]
        public void IsMalePercentile(int lower, int upper, bool isTrue)
        {
            base.BooleanPercentile(lower, upper, isTrue);
        }
    }
}
