using CreatureGen.Domain.Tables;
using CreatureGen.Creatures;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Races.BaseRaces.Ages
{
    [TestFixture]
    public class GithyankiAgesTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Adjustments.RACEAges, SizeConstants.BaseRaces.Githyanki); }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                SizeConstants.Ages.Adulthood,
                SizeConstants.Ages.MiddleAge,
                SizeConstants.Ages.Old,
                SizeConstants.Ages.Venerable
            };

            AssertCollectionNames(names);
        }

        [TestCase(SizeConstants.Ages.Adulthood, 20)]
        [TestCase(SizeConstants.Ages.MiddleAge, 50)]
        [TestCase(SizeConstants.Ages.Old, 75)]
        [TestCase(SizeConstants.Ages.Venerable, 100)]
        public void GithyankiAges(string name, int age)
        {
            base.Adjustment(name, age);
        }
    }
}