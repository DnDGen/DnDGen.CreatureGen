using CreatureGen.Abilities;
using CreatureGen.Tables;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Abilities
{
    [TestFixture]
    public class AbilityGroupsTests : CollectionTests
    {
        protected override string tableName
        {
            get
            {
                return TableNameConstants.Set.Collection.AbilityGroups;
            }
        }

        public override void CollectionNames()
        {
            var names = new[]
            {
                GroupConstants.All
            };

            AssertCollectionNames(names);
        }

        [TestCase(GroupConstants.All,
            AbilityConstants.Charisma,
            AbilityConstants.Constitution,
            AbilityConstants.Dexterity,
            AbilityConstants.Intelligence,
            AbilityConstants.Strength,
            AbilityConstants.Wisdom)]
        public void AbilityGroup(string name, params string[] abilities)
        {
            base.Collection(name, abilities);
        }
    }
}
