using CreatureGen.Domain.Tables;
using NUnit.Framework;
using System;
using System.Linq;

namespace CreatureGen.Tests.Integration.Tables.Leadership
{
    [TestFixture]
    public class Level6FollowerQuantitiesTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get
            {
                return string.Format(TableNameConstants.Formattable.Adjustments.LevelXFollowerQuantities, 6);
            }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = Enumerable.Range(1, 25).Select(i => i.ToString());
            AssertCollectionNames(names);
        }

        [TestCase(1, 0)]
        [TestCase(2, 0)]
        [TestCase(3, 0)]
        [TestCase(4, 0)]
        [TestCase(5, 0)]
        [TestCase(6, 0)]
        [TestCase(7, 0)]
        [TestCase(8, 0)]
        [TestCase(9, 0)]
        [TestCase(10, 0)]
        [TestCase(11, 0)]
        [TestCase(12, 0)]
        [TestCase(13, 0)]
        [TestCase(14, 0)]
        [TestCase(15, 0)]
        [TestCase(16, 0)]
        [TestCase(17, 0)]
        [TestCase(18, 0)]
        [TestCase(19, 0)]
        [TestCase(20, 0)]
        [TestCase(21, 1)]
        [TestCase(22, 1)]
        [TestCase(23, 1)]
        [TestCase(24, 1)]
        [TestCase(25, 2)]
        public void Adjustment(Int32 leadershipScore, Int32 cohortLevel)
        {
            base.Adjustment(leadershipScore.ToString(), cohortLevel);
        }
    }
}
