using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Defenses;
using DnDGen.CreatureGen.Tables;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Integration.Tables.Creatures.TemplateGroups
{
    [TestFixture]
    public abstract class TemplateGroupsTableTests : CollectionTests
    {
        protected override string tableName => TableNameConstants.Collection.TemplateGroups;

        protected void AssertTemplateGroupNamesAreComplete()
        {
            var types = CreatureConstants.Types.GetAll();
            var subtypes = CreatureConstants.Types.Subtypes.GetAll();

            var entries = new[]
            {
                CreatureConstants.Groups.HalfDragon,
                CreatureConstants.Groups.Lycanthrope,
                SaveConstants.Fortitude,
                SaveConstants.Reflex,
                SaveConstants.Will,
                SaveConstants.Fortitude + GroupConstants.TREE,
                SaveConstants.Reflex + GroupConstants.TREE,
                SaveConstants.Will + GroupConstants.TREE,
                GroupConstants.GoodBaseAttack,
                GroupConstants.AverageBaseAttack,
                GroupConstants.PoorBaseAttack,
                GroupConstants.GoodBaseAttack + GroupConstants.TREE,
                GroupConstants.AverageBaseAttack + GroupConstants.TREE,
                GroupConstants.PoorBaseAttack + GroupConstants.TREE,
                GroupConstants.All,
                GroupConstants.All + GroupConstants.TREE,
            };

            var names = entries
                .Union(types)
                .Union(types.Select(t => t + GroupConstants.TREE))
                .Union(subtypes)
                .Union(subtypes.Select(st => st + GroupConstants.TREE));

            AssertCollectionNames(names);
        }

        protected void AssertTemplateGroup(string name, IEnumerable<string> entries)
        {
            AssertDistinctCollection(name + GroupConstants.TREE, [.. entries]);

            var exploded = ExplodeCollection(tableName, name + GroupConstants.TREE).Distinct();
            AssertDistinctCollection(name, [.. exploded]);
        }
    }
}
