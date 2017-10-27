using CreatureGen.Domain.Tables;
using NUnit.Framework;
using System;
using System.Linq;

namespace CreatureGen.Tests.Integration.Tables.Leadership
{
    [TestFixture]
    public class Level1FollowerQuantitiesTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get
            {
                return string.Format(TableNameConstants.Formattable.Adjustments.LevelXFollowerQuantities, 1);
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
        [TestCase(10, 5)]
        [TestCase(11, 6)]
        [TestCase(12, 8)]
        [TestCase(13, 10)]
        [TestCase(14, 15)]
        [TestCase(15, 20)]
        [TestCase(16, 25)]
        [TestCase(17, 30)]
        [TestCase(18, 35)]
        [TestCase(19, 40)]
        [TestCase(20, 50)]
        [TestCase(21, 60)]
        [TestCase(22, 75)]
        [TestCase(23, 90)]
        [TestCase(24, 110)]
        [TestCase(25, 135)]
        public void Adjustment(Int32 leadershipScore, Int32 cohortLevel)
        {
            base.Adjustment(leadershipScore.ToString(), cohortLevel);
        }
    }
}
