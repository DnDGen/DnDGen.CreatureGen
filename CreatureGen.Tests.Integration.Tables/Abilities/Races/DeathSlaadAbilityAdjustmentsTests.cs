using CreatureGen.Abilities;
using CreatureGen.Domain.Tables;
using CreatureGen.Creatures;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Abilities.Races
{
    [TestFixture]
    public class DeathSlaadAbilityAdjustmentsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Adjustments.RACEAbilityAdjustments, SizeConstants.BaseRaces.DeathSlaad); }
        }

        [Test]
        public override void CollectionNames()
        {
            var abilityGroups = CollectionsMapper.Map(TableNameConstants.Set.Collection.AbilityGroups);
            AssertCollectionNames(abilityGroups[GroupConstants.All]);
        }

        [TestCase(AbilityConstants.Charisma, 8)]
        [TestCase(AbilityConstants.Constitution, 10)]
        [TestCase(AbilityConstants.Dexterity, 12)]
        [TestCase(AbilityConstants.Intelligence, 8)]
        [TestCase(AbilityConstants.Strength, 10)]
        [TestCase(AbilityConstants.Wisdom, 8)]
        public void RacialAbilityAdjustment(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
