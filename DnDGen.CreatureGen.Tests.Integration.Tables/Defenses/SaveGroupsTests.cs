using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Tables;
using DnDGen.CreatureGen.Tests.Integration.TestData;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Integration.Tables.Defenses
{
    [TestFixture]
    public class SaveGroupsTests : CollectionTests
    {
        protected override string tableName => TableNameConstants.Collection.SaveGroups;

        private Dictionary<string, IEnumerable<string>> saveGroups;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            saveGroups = GetSaveGroups();
        }

        private Dictionary<string, IEnumerable<string>> GetSaveGroups()
        {
            var groups = new Dictionary<string, IEnumerable<string>>();
            var creatures = CreatureConstants.GetAll();
            var templates = CreatureConstants.Templates.GetAll();

            foreach (var creature in creatures)
                groups[creature] = [];

            foreach (var template in templates)
                groups[template] = [];

            //TODO: Fill in settings based on eisting groups

            return groups;
        }

        [Test]
        public void SaveGroupsNames()
        {
            var creatures = CreatureConstants.GetAll();
            var templates = CreatureConstants.Templates.GetAll();

            var names = creatures.Union(templates);
            Assert.That(saveGroups.Keys, Is.EquivalentTo(names));
            AssertCollectionNames(names);
        }

        //TODO: Redo as individual test cases
        [TestCaseSource(typeof(CreatureTestData), nameof(CreatureTestData.Creatures))]
        public void CreatureSaveGroupHasStrongSaves(string creature)
        {
            AssertDistinctCollection(creature, [.. saveGroups[creature]]);
        }

        //TODO: Redo as individual test cases
        [TestCaseSource(typeof(CreatureTestData), nameof(CreatureTestData.Templates))]
        public void TemplateSaveGroupHasStrongSaves(string template)
        {
            AssertDistinctCollection(template, [.. saveGroups[template]]);
        }
    }
}
