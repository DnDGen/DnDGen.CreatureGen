using CreatureGen.Creatures;
using CreatureGen.Tables;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Races.BaseRaces
{
    [TestFixture]
    public class CreatureGroupsTests : CollectionTests
    {
        protected override string tableName
        {
            get { return TableNameConstants.Set.Collection.CreatureGroups; }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                GroupConstants.All,
                GroupConstants.Aquatic,
            };

            AssertCollectionNames(names);
        }

        [TestCase(GroupConstants.Aquatic,
            CreatureConstants.Elf_Aquatic,
            CreatureConstants.Gargoyle_Kapoacinth,
            CreatureConstants.KuoToa,
            CreatureConstants.Locathah,
            CreatureConstants.Merfolk,
            CreatureConstants.Ogre_Merrow,
            CreatureConstants.Sahuagin,
            CreatureConstants.Troll_Scrag)]
        public void Creature(string name, params string[] collection)
        {
            base.DistinctCollection(name, collection);
        }
    }
}