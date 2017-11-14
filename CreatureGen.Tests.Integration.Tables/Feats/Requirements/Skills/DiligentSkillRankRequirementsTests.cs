using CreatureGen.Tables;
using CreatureGen.Feats;
using CreatureGen.Skills;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Feats.Requirements.Skills
{
    [TestFixture]
    public class DiligentSkillRankRequirementsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Adjustments.FEATSkillRankRequirements, FeatConstants.Diligent); }
        }

        [Test]
        public void CollectionNames()
        {
            var skills = new[] { SkillConstants.Appraise, SkillConstants.DecipherScript };
            AssertCollectionNames(skills);
        }

        [TestCase(SkillConstants.Appraise, 0)]
        [TestCase(SkillConstants.DecipherScript, 0)]
        public void SkillRankRequirement(string skill, int ranks)
        {
            base.Adjustment(skill, ranks);
        }
    }
}
