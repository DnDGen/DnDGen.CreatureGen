using CreatureGen.Domain.Tables;
using CreatureGen.Creatures;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Races.BaseRaces.Ages
{
    [TestFixture]
    public class TieflingAgesTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Adjustments.RACEAges, SizeConstants.BaseRaces.Tiefling); }
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

        [TestCase(SizeConstants.Ages.Adulthood, 15)]
        [TestCase(SizeConstants.Ages.MiddleAge, 45)]
        [TestCase(SizeConstants.Ages.Old, 68)]
        [TestCase(SizeConstants.Ages.Venerable, 90)]
        public void RacialAges(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}