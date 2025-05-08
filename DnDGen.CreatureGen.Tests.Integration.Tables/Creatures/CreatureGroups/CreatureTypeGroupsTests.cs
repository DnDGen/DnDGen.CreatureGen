using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Tables;
using DnDGen.CreatureGen.Tests.Integration.TestData;
using NUnit.Framework;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Integration.Tables.Creatures.CreatureGroups
{
    [TestFixture]
    public class CreatureTypeGroupsTests : CreatureGroupsTableTests
    {
        [Test]
        public void CreatureGroupNames()
        {
            AssertCreatureGroupNamesAreComplete();
        }

        [TestCaseSource(typeof(CreatureTestData), nameof(CreatureTestData.Creatures))]
        public void CreatureTypeAndSubtypesMatchesCreatureGroupTypeAndSubtypes(string creature)
        {
            var types = collectionMapper.Map(Config.Name, TableNameConstants.Collection.CreatureTypes);
            Assert.That(types.Keys, Contains.Item(creature));
            Assert.That(types[creature], Is.Not.Empty);

            foreach (var type in types[creature])
            {
                Assert.That(table.Keys, Contains.Item(type), "Table keys");
                Assert.That(table[type], Contains.Item(creature), type);
            }

            var wrongTypes = CreatureConstants.Types.GetAll()
                .Union(CreatureConstants.Types.Subtypes.GetAll())
                .Except(types[creature]);

            foreach (var type in wrongTypes)
            {
                Assert.That(table.Keys, Contains.Item(type), "Table keys");
                Assert.That(table[type], Does.Not.Contain(creature), type);
            }
        }
    }
}