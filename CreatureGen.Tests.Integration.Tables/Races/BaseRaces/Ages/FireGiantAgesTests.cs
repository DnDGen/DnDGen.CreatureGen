﻿using CreatureGen.Domain.Tables;
using CreatureGen.Creatures;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Races.BaseRaces.Ages
{
    [TestFixture]
    public class FireGiantAgesTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Adjustments.RACEAges, SizeConstants.BaseRaces.FireGiant); }
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

        [TestCase(SizeConstants.Ages.Adulthood, 35)]
        [TestCase(SizeConstants.Ages.MiddleAge, 105)]
        [TestCase(SizeConstants.Ages.Old, 228)]
        [TestCase(SizeConstants.Ages.Venerable, 263)]
        public override void Adjustment(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}