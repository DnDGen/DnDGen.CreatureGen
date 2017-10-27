using CreatureGen.Domain.Tables;
using CreatureGen.Feats;
using CreatureGen.Skills;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Feats.Requirements.Skills
{
    [TestFixture]
    public class NegotiatorSkillRankRequirementsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Adjustments.FEATSkillRankRequirements, FeatConstants.Negotiator); }
        }

        [Test]
        public override void CollectionNames()
        {
            var skills = new[] { SkillConstants.Diplomacy, SkillConstants.SenseMotive };
            AssertCollectionNames(skills);
        }

        [TestCase(SkillConstants.Diplomacy, 0)]
        [TestCase(SkillConstants.SenseMotive, 0)]
        public override void Adjustment(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
