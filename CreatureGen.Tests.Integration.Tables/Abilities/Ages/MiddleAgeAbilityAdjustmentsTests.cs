using CreatureGen.Abilities;
using CreatureGen.Domain.Tables;
using CreatureGen.Creatures;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Abilities.Ages
{
    [TestFixture]
    public class MiddleAgeAbilityAdjustmentsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get
            {
                return string.Format(TableNameConstants.Formattable.Adjustments.AGEAbilityAdjustments, SizeConstants.Ages.MiddleAge);
            }
        }

        public override void CollectionNames()
        {
            var names = new[]
            {
                AbilityConstants.Charisma,
                AbilityConstants.Constitution,
                AbilityConstants.Dexterity,
                AbilityConstants.Intelligence,
                AbilityConstants.Strength,
                AbilityConstants.Wisdom
            };

            AssertCollectionNames(names);
        }

        [TestCase(AbilityConstants.Charisma, 1)]
        [TestCase(AbilityConstants.Constitution, -1)]
        [TestCase(AbilityConstants.Dexterity, -1)]
        [TestCase(AbilityConstants.Intelligence, 1)]
        [TestCase(AbilityConstants.Strength, -1)]
        [TestCase(AbilityConstants.Wisdom, 1)]
        public override void Adjustment(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
