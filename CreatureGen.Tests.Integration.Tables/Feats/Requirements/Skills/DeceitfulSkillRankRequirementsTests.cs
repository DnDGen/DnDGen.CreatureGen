using CreatureGen.Domain.Tables;
using CreatureGen.Feats;
using CreatureGen.Skills;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Feats.Requirements.Skills
{
    [TestFixture]
    public class DeceitfulSkillRankRequirementsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Adjustments.FEATSkillRankRequirements, FeatConstants.Deceitful); }
        }

        [Test]
        public override void CollectionNames()
        {
            var skills = new[] { SkillConstants.Disguise, SkillConstants.Forgery };
            AssertCollectionNames(skills);
        }

        [TestCase(SkillConstants.Disguise, 0)]
        [TestCase(SkillConstants.Forgery, 0)]
        public override void Adjustment(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
