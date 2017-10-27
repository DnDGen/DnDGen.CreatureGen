using CreatureGen.Domain.Tables;
using CreatureGen.Creatures;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Combats
{
    [TestFixture]
    public class SizeModifiersTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get
            {
                return TableNameConstants.Set.Adjustments.SizeModifiers;
            }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                SizeConstants.Sizes.Colossal,
                SizeConstants.Sizes.Gargantuan,
                SizeConstants.Sizes.Huge,
                SizeConstants.Sizes.Large,
                SizeConstants.Sizes.Medium,
                SizeConstants.Sizes.Small,
                SizeConstants.Sizes.Tiny,
            };

            AssertCollectionNames(names);
        }

        [TestCase(SizeConstants.Sizes.Colossal, -8)]
        [TestCase(SizeConstants.Sizes.Gargantuan, -4)]
        [TestCase(SizeConstants.Sizes.Huge, -2)]
        [TestCase(SizeConstants.Sizes.Large, -1)]
        [TestCase(SizeConstants.Sizes.Medium, 0)]
        [TestCase(SizeConstants.Sizes.Small, 1)]
        [TestCase(SizeConstants.Sizes.Tiny, 2)]
        public void SizeModifier(string size, int adjustment)
        {
            base.Adjustment(size, adjustment);
        }
    }
}
