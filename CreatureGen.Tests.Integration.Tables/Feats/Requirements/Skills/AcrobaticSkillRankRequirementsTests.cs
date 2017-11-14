using CreatureGen.Feats;
using CreatureGen.Skills;
using CreatureGen.Tables;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Feats.Requirements.Skills
{
    [TestFixture]
    public class AcrobaticSkillRankRequirementsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Adjustments.FEATSkillRankRequirements, FeatConstants.Acrobatic); }
        }

        [Test]
        public void CollectionNames()
        {
            var skills = new[] { SkillConstants.Jump, SkillConstants.Tumble };
            AssertCollectionNames(skills);
        }

        [TestCase(SkillConstants.Jump, 0)]
        [TestCase(SkillConstants.Tumble, 0)]
        public override void Adjustment(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
