using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Tables;
using NUnit.Framework;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Integration.Tables.Creatures
{
    [TestFixture]
    public class TemplateGroupsTests : CollectionTests
    {
        protected override string tableName => TableNameConstants.Collection.TemplateGroups;

        [Test]
        public void TemplateGroupNames()
        {
            var entries = new[]
            {
                GroupConstants.All,
            };

            AssertCollectionNames(entries);
        }

        [Test]
        public void AllGroup()
        {
            var allTemplates = CreatureConstants.Templates.GetAll().Except([CreatureConstants.Templates.None]);
            AssertDistinctCollection(GroupConstants.All, [.. allTemplates]);
        }

        [Test]
        public void NoCircularSubgroups()
        {
            foreach (var kvp in table)
            {
                AssertGroupDoesNotContain(kvp.Key, kvp.Key);
            }
        }

        private void AssertGroupDoesNotContain(string name, string forbiddenEntry)
        {
            var group = table[name];

            //INFO: A group is allowed to contain itself as an immediate child
            //Example is the Orc subtype group containing the Orc creature
            if (name != forbiddenEntry)
                Assert.That(group, Does.Not.Contain(forbiddenEntry), name);

            //INFO: Remove the name from the group, or we get infinite recursion traversing itself as a subgroup
            var subgroupNames = group.Intersect(table.Keys).Except([name]);

            foreach (var subgroupName in subgroupNames)
            {
                AssertGroupDoesNotContain(subgroupName, forbiddenEntry);
                AssertGroupDoesNotContain(subgroupName, subgroupName);
            }
        }
    }
}
