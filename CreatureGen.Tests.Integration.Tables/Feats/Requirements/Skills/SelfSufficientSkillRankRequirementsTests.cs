using CreatureGen.Tables;
using CreatureGen.Feats;
using CreatureGen.Skills;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Feats.Requirements.Skills
{
    [TestFixture]
    public class SelfSufficientSkillRankRequirementsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Adjustments.FEATSkillRankRequirements, FeatConstants.SelfSufficient); }
        }

        [Test]
        public override void CollectionNames()
        {
            var skills = new[] { SkillConstants.Heal, SkillConstants.Survival };
            AssertCollectionNames(skills);
        }

        [TestCase(SkillConstants.Heal, 0)]
        [TestCase(SkillConstants.Survival, 0)]
        public override void Adjustment(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
