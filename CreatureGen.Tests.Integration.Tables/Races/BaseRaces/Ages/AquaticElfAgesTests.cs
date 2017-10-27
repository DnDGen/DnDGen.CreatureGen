﻿using CreatureGen.Domain.Tables;
using CreatureGen.Creatures;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Races.BaseRaces.Ages
{
    [TestFixture]
    public class AquaticElfAgesTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Adjustments.RACEAges, SizeConstants.BaseRaces.AquaticElf); }
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

        [TestCase(SizeConstants.Ages.Adulthood, 110)]
        [TestCase(SizeConstants.Ages.MiddleAge, 175)]
        [TestCase(SizeConstants.Ages.Old, 263)]
        [TestCase(SizeConstants.Ages.Venerable, 350)]
        public void Age(string ageDescription, int age)
        {
            base.Adjustment(ageDescription, age);
        }
    }
}