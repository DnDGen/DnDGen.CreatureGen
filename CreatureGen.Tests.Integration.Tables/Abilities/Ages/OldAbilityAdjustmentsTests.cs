using CreatureGen.Abilities;
using CreatureGen.Domain.Tables;
using CreatureGen.Creatures;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Abilities.Ages
{
    [TestFixture]
    public class OldAbilityAdjustmentsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get
            {
                return string.Format(TableNameConstants.Formattable.Adjustments.AGEAbilityAdjustments, SizeConstants.Ages.Old);
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

        [TestCase(AbilityConstants.Charisma, 2)]
        [TestCase(AbilityConstants.Constitution, -3)]
        [TestCase(AbilityConstants.Dexterity, -3)]
        [TestCase(AbilityConstants.Intelligence, 2)]
        [TestCase(AbilityConstants.Strength, -3)]
        [TestCase(AbilityConstants.Wisdom, 2)]
        public override void Adjustment(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
