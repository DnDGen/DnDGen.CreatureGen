using CreatureGen.Domain.Tables;
using CreatureGen.Feats;
using CreatureGen.Skills;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Feats.Requirements.Skills
{
    [TestFixture]
    public class AgileSkillRankRequirementsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Adjustments.FEATSkillRankRequirements, FeatConstants.Agile); }
        }

        [Test]
        public override void CollectionNames()
        {
            var skills = new[] { SkillConstants.Balance, SkillConstants.EscapeArtist };
            AssertCollectionNames(skills);
        }

        [TestCase(SkillConstants.Balance, 0)]
        [TestCase(SkillConstants.EscapeArtist, 0)]
        public override void Adjustment(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
