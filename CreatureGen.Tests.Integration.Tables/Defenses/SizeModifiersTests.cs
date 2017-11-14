using CreatureGen.Creatures;
using CreatureGen.Tables;
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
        public void CollectionNames()
        {
            var names = new[]
            {
                SizeConstants.Colossal,
                SizeConstants.Gargantuan,
                SizeConstants.Huge,
                SizeConstants.Large,
                SizeConstants.Medium,
                SizeConstants.Small,
                SizeConstants.Tiny,
            };

            AssertCollectionNames(names);
        }

        [TestCase(SizeConstants.Colossal, -8)]
        [TestCase(SizeConstants.Gargantuan, -4)]
        [TestCase(SizeConstants.Huge, -2)]
        [TestCase(SizeConstants.Large, -1)]
        [TestCase(SizeConstants.Medium, 0)]
        [TestCase(SizeConstants.Small, 1)]
        [TestCase(SizeConstants.Tiny, 2)]
        public void SizeModifier(string size, int adjustment)
        {
            base.Adjustment(size, adjustment);
        }
    }
}
