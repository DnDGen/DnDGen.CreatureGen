﻿using CreatureGen.Abilities;
using CreatureGen.Domain.Tables;
using CreatureGen.Creatures;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Abilities.Races
{
    [TestFixture]
    public class StormGiantAbilityAdjustmentsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Adjustments.RACEAbilityAdjustments, SizeConstants.BaseRaces.StormGiant); }
        }

        [Test]
        public override void CollectionNames()
        {
            var abilityGroups = CollectionsMapper.Map(TableNameConstants.Set.Collection.AbilityGroups);
            AssertCollectionNames(abilityGroups[GroupConstants.All]);
        }

        [TestCase(AbilityConstants.Charisma, 4)]
        [TestCase(AbilityConstants.Constitution, 12)]
        [TestCase(AbilityConstants.Dexterity, 4)]
        [TestCase(AbilityConstants.Intelligence, 6)]
        [TestCase(AbilityConstants.Strength, 28)]
        [TestCase(AbilityConstants.Wisdom, 4)]
        public void RacialAbilityAdjustment(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
