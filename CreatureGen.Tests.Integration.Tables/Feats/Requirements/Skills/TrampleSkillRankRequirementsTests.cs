using CreatureGen.Tables;
using CreatureGen.Feats;
using CreatureGen.Skills;
using NUnit.Framework;
using System;

namespace CreatureGen.Tests.Integration.Tables.Feats.Requirements.Skills
{
    [TestFixture]
    public class TrampleSkillRankRequirementsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Adjustments.FEATSkillRankRequirements, FeatConstants.Trample); }
        }

        [Test]
        public void CollectionNames()
        {
            var skills = new[] { SkillConstants.Ride };
            AssertCollectionNames(skills);
        }

        [TestCase(SkillConstants.Ride, 1)]
        public override void Adjustment(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
