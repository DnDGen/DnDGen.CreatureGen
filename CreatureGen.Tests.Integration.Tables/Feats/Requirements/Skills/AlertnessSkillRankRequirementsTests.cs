using CreatureGen.Feats;
using CreatureGen.Skills;
using CreatureGen.Tables;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Feats.Requirements.Skills
{
    [TestFixture]
    public class AlertnessSkillRankRequirementsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Adjustments.FEATSkillRankRequirements, FeatConstants.Alertness); }
        }

        [Test]
        public void CollectionNames()
        {
            var skills = new[] { SkillConstants.Listen, SkillConstants.Spot };
            AssertCollectionNames(skills);
        }

        [TestCase(SkillConstants.Listen, 0)]
        [TestCase(SkillConstants.Spot, 0)]
        public override void Adjustment(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
