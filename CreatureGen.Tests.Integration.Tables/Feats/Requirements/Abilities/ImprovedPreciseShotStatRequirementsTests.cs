﻿using CreatureGen.Abilities;
using CreatureGen.Domain.Tables;
using CreatureGen.Feats;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Feats.Requirements.Abilities
{
    [TestFixture]
    public class ImprovedPreciseShotStatRequirementsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Adjustments.FEATAbilityRequirements, FeatConstants.ImprovedPreciseShot); }
        }

        [Test]
        public override void CollectionNames()
        {
            var stats = new[] { AbilityConstants.Dexterity };
            AssertCollectionNames(stats);
        }

        [TestCase(AbilityConstants.Dexterity, 19)]
        public void AbilityRequirementForFeat(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
