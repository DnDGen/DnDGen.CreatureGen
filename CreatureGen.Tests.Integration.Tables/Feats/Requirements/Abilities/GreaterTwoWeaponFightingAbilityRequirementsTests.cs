using CreatureGen.Abilities;
using CreatureGen.Feats;
using CreatureGen.Tables;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Feats.Requirements.Abilities
{
    [TestFixture]
    public class GreaterTwoWeaponFightingAbilityRequirementsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Adjustments.FEATAbilityRequirements, FeatConstants.GreaterTwoWeaponFighting); }
        }

        [Test]
        public void CollectionNames()
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
