﻿using CreatureGen.Abilities;
using CreatureGen.Domain.Tables;
using CreatureGen.Feats;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Feats.Requirements.Abilities
{
    [TestFixture]
    public class WhirlwindAttackAbilityRequirementsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Adjustments.FEATAbilityRequirements, FeatConstants.WhirlwindAttack); }
        }

        [Test]
        public override void CollectionNames()
        {
            var stats = new[] { AbilityConstants.Dexterity, AbilityConstants.Intelligence };
            AssertCollectionNames(stats);
        }

        [TestCase(AbilityConstants.Dexterity, 13)]
        [TestCase(AbilityConstants.Intelligence, 13)]
        public void AbilityRequirementForFeat(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
