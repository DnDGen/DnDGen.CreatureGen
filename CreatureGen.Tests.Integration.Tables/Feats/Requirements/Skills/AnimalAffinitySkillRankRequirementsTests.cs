using CreatureGen.Domain.Tables;
using CreatureGen.Feats;
using CreatureGen.Skills;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Feats.Requirements.Skills
{
    [TestFixture]
    public class AnimalAffinitySkillRankRequirementsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Adjustments.FEATSkillRankRequirements, FeatConstants.AnimalAffinity); }
        }

        [Test]
        public override void CollectionNames()
        {
            var skills = new[] { SkillConstants.HandleAnimal, SkillConstants.Ride };
            AssertCollectionNames(skills);
        }

        [TestCase(SkillConstants.HandleAnimal, 0)]
        [TestCase(SkillConstants.Ride, 0)]
        public override void Adjustment(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
