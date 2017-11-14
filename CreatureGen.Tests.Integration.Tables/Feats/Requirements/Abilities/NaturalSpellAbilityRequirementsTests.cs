using CreatureGen.Abilities;
using CreatureGen.Feats;
using CreatureGen.Tables;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Feats.Requirements.Abilities
{
    [TestFixture]
    public class NaturalSpellAbilityRequirementsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Adjustments.FEATAbilityRequirements, FeatConstants.NaturalSpell); }
        }

        [Test]
        public void CollectionNames()
        {
            var stats = new[] { AbilityConstants.Wisdom };
            AssertCollectionNames(stats);
        }

        [TestCase(AbilityConstants.Wisdom, 13)]
        public void AbilityRequirementForFeat(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
