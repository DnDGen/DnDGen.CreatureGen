using CreatureGen.Domain.Tables;
using NUnit.Framework;
using System;

namespace CreatureGen.Tests.Integration.Tables.Leadership
{
    [TestFixture]
    public class LeadershipModifiersTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get
            {
                return TableNameConstants.Set.Adjustments.LeadershipModifiers;
            }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                String.Empty,
                "Cruelty",
                "Aloofness",
                "Failure",
                "Special power",
                "Fairness and generosity",
                "Great renown",
                "Moves around a lot",
                "Has a stronghold, base of operations, guildhouse, or the like"
            };

            AssertCollectionNames(names);
        }

        [TestCase("", 0)]
        [TestCase("Cruelty", -2)]
        [TestCase("Aloofness", -1)]
        [TestCase("Failure", -1)]
        [TestCase("Special power", 1)]
        [TestCase("Fairness and generosity", 1)]
        [TestCase("Great renown", 2)]
        [TestCase("Moves around a lot", -1)]
        [TestCase("Has a stronghold, base of operations, guildhouse, or the like", 2)]
        public void LeadershipModifier(string name, int adjustment)
        {
            Adjustment(name, adjustment);
        }
    }
}
