using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Tables;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Integration.Tables.Creatures.CreatureGroups
{
    [TestFixture]
    public abstract class CreatureGroupsTableTests : CollectionTests
    {
        protected override string tableName => TableNameConstants.Collection.CreatureGroups;

        protected void AssertCreatureGroupNamesAreComplete()
        {
            var types = CreatureConstants.Types.GetAll();
            var subtypes = CreatureConstants.Types.Subtypes.GetAll();
            var templates = CreatureConstants.Templates.GetAll();

            var entries = new[]
            {
                GroupConstants.GoodBaseAttack,
                GroupConstants.AverageBaseAttack,
                GroupConstants.PoorBaseAttack,
                GroupConstants.All,
                GroupConstants.Characters,
                GroupConstants.HasSkeleton,
            };

            var names = entries
                .Union(types)
                .Union(subtypes)
                .Union(templates.Select(t => t + bool.FalseString))
                .Union(templates.Select(t => t + bool.TrueString));

            AssertCollectionNames(names);
        }

        protected void AssertCreatureGroup(string name, IEnumerable<string> entries)
        {
            AssertDistinctCollection(name, [.. entries]);
        }
    }
}
