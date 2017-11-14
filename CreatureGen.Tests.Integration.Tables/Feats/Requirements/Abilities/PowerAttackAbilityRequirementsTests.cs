using CreatureGen.Abilities;
using CreatureGen.Feats;
using CreatureGen.Tables;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Feats.Requirements.Abilities
{
    [TestFixture]
    public class PowerAttackAbilityRequirementsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Adjustments.FEATAbilityRequirements, FeatConstants.PowerAttack); }
        }

        [Test]
        public void CollectionNames()
        {
            var stats = new[] { AbilityConstants.Strength };
            AssertCollectionNames(stats);
        }

        [TestCase(AbilityConstants.Strength, 13)]
        public void AbilityRequirementForFeat(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
