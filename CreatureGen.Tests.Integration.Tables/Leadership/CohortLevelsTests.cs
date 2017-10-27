using CreatureGen.Domain.Tables;
using NUnit.Framework;
using System;
using System.Linq;

namespace CreatureGen.Tests.Integration.Tables.Leadership
{
    [TestFixture]
    public class CohortLevelsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get
            {
                return TableNameConstants.Set.Adjustments.CohortLevels;
            }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = Enumerable.Range(1, 25).Select(i => i.ToString());
            AssertCollectionNames(names);
        }

        [TestCase(1, 0)]
        [TestCase(2, 1)]
        [TestCase(3, 2)]
        [TestCase(4, 3)]
        [TestCase(5, 3)]
        [TestCase(6, 4)]
        [TestCase(7, 5)]
        [TestCase(8, 5)]
        [TestCase(9, 6)]
        [TestCase(10, 7)]
        [TestCase(11, 7)]
        [TestCase(12, 8)]
        [TestCase(13, 9)]
        [TestCase(14, 10)]
        [TestCase(15, 10)]
        [TestCase(16, 11)]
        [TestCase(17, 12)]
        [TestCase(18, 12)]
        [TestCase(19, 13)]
        [TestCase(20, 14)]
        [TestCase(21, 15)]
        [TestCase(22, 15)]
        [TestCase(23, 16)]
        [TestCase(24, 17)]
        [TestCase(25, 17)]
        public void Adjustment(Int32 leadershipScore, Int32 cohortLevel)
        {
            base.Adjustment(leadershipScore.ToString(), cohortLevel);
        }
    }
}
