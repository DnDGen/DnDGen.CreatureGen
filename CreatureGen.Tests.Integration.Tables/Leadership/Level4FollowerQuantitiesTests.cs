using CreatureGen.Domain.Tables;
using NUnit.Framework;
using System;
using System.Linq;

namespace CreatureGen.Tests.Integration.Tables.Leadership
{
    [TestFixture]
    public class Level4FollowerQuantitiesTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get
            {
                return string.Format(TableNameConstants.Formattable.Adjustments.LevelXFollowerQuantities, 4);
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
        [TestCase(17, 1)]
        [TestCase(18, 1)]
        [TestCase(19, 1)]
        [TestCase(20, 2)]
        [TestCase(21, 2)]
        [TestCase(22, 2)]
        [TestCase(23, 3)]
        [TestCase(24, 3)]
        [TestCase(25, 4)]
        public void Adjustment(Int32 leadershipScore, Int32 cohortLevel)
        {
            base.Adjustment(leadershipScore.ToString(), cohortLevel);
        }
    }
}
