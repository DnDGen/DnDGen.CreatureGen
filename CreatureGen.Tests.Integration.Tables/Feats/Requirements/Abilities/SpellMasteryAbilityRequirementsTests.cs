using CreatureGen.Abilities;
using CreatureGen.Feats;
using CreatureGen.Tables;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Feats.Requirements.Abilities
{
    [TestFixture]
    public class SpellMasteryAbilityRequirementsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Adjustments.FEATAbilityRequirements, FeatConstants.SpellMastery); }
        }

        [Test]
        public void CollectionNames()
        {
            var stats = new[] { AbilityConstants.Intelligence };
            AssertCollectionNames(stats);
        }

        [TestCase(AbilityConstants.Intelligence, 12)]
        public void AbilityRequirementForFeat(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
