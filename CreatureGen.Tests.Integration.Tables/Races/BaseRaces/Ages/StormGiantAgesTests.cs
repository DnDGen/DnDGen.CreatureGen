using CreatureGen.Domain.Tables;
using CreatureGen.Creatures;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Races.BaseRaces.Ages
{
    [TestFixture]
    public class StormGiantAgesTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Adjustments.RACEAges, SizeConstants.BaseRaces.StormGiant); }
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

        [TestCase(SizeConstants.Ages.Adulthood, 60)]
        [TestCase(SizeConstants.Ages.MiddleAge, 180)]
        [TestCase(SizeConstants.Ages.Old, 390)]
        [TestCase(SizeConstants.Ages.Venerable, 450)]
        public void RacialAges(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}