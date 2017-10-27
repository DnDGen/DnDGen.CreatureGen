using CreatureGen.Domain.Tables;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Abilities
{
    [TestFixture]
    public class IncreaseFirstPriorityAbilityTests : BooleanPercentileTests
    {
        protected override string tableName
        {
            get { return TableNameConstants.Set.TrueOrFalse.IncreaseFirstPriorityAbility; }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(1, 50, true)]
        [TestCase(51, 100, false)]
        public override void BooleanPercentile(int lower, int upper, bool isTrue)
        {
            base.BooleanPercentile(lower, upper, isTrue);
        }
    }
}