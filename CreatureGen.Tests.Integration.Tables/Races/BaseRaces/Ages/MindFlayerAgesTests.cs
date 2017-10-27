﻿using CreatureGen.Domain.Tables;
using CreatureGen.Creatures;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Races.BaseRaces.Ages
{
    [TestFixture]
    public class MindFlayerAgesTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Adjustments.RACEAges, SizeConstants.BaseRaces.MindFlayer); }
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

        [TestCase(SizeConstants.Ages.Adulthood, 18)]
        [TestCase(SizeConstants.Ages.MiddleAge, 50)]
        [TestCase(SizeConstants.Ages.Old, 80)]
        [TestCase(SizeConstants.Ages.Venerable, 115)]
        public override void Adjustment(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}