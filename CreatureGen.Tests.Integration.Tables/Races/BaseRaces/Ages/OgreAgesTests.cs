﻿using CreatureGen.Domain.Tables;
using CreatureGen.Creatures;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Races.BaseRaces.Ages
{
    [TestFixture]
    public class OgreAgesTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Adjustments.RACEAges, SizeConstants.BaseRaces.Ogre); }
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

        [TestCase(SizeConstants.Ages.Adulthood, 30)]
        [TestCase(SizeConstants.Ages.MiddleAge, 60)]
        [TestCase(SizeConstants.Ages.Old, 80)]
        [TestCase(SizeConstants.Ages.Venerable, 120)]
        public void Age(string ageDescription, int age)
        {
            base.Adjustment(ageDescription, age);
        }
    }
}