using CreatureGen.Domain.Tables;
using CreatureGen.Feats;
using CreatureGen.Skills;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Feats.Requirements.Skills
{
    [TestFixture]
    public class InvestigatorSkillRankRequirementsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Adjustments.FEATSkillRankRequirements, FeatConstants.Investigator); }
        }

        [Test]
        public override void CollectionNames()
        {
            var skills = new[] { SkillConstants.GatherInformation, SkillConstants.Search };
            AssertCollectionNames(skills);
        }

        [TestCase(SkillConstants.GatherInformation, 0)]
        [TestCase(SkillConstants.Search, 0)]
        public override void Adjustment(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
