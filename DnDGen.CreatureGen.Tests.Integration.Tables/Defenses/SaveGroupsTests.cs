using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Defenses;
using DnDGen.CreatureGen.Tables;
using DnDGen.CreatureGen.Tests.Integration.TestData;
using DnDGen.Infrastructure.Selectors.Collections;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Integration.Tables.Defenses
{
    [TestFixture]
    public class SaveGroupsTests : CollectionTests
    {
        private ICollectionSelector collectionSelector;

        protected override string tableName => TableNameConstants.Collection.SaveGroups;

        [SetUp]
        public void Setup()
        {
            collectionSelector = GetNewInstanceOf<ICollectionSelector>();
        }

        [Test]
        public void SaveGroupsNames()
        {
            var creatures = CreatureConstants.GetAll();
            var templates = CreatureConstants.Templates.GetAll();

            var names = creatures.Union(templates);
            AssertCollectionNames(names);
        }

        [TestCaseSource(typeof(CreatureTestData), nameof(CreatureTestData.Creatures))]
        public void CreatureSaveGroupHasStrongSaves(string creature)
        {
            var fortitude = collectionSelector.Explode(Config.Name, TableNameConstants.Collection.CreatureGroups, SaveConstants.Fortitude);
            var reflex = collectionSelector.Explode(Config.Name, TableNameConstants.Collection.CreatureGroups, SaveConstants.Reflex);
            var will = collectionSelector.Explode(Config.Name, TableNameConstants.Collection.CreatureGroups, SaveConstants.Will);

            var strongSaves = new List<string>();
            if (fortitude.Contains(creature))
                strongSaves.Add(SaveConstants.Fortitude);
            if (reflex.Contains(creature))
                strongSaves.Add(SaveConstants.Reflex);
            if (will.Contains(creature))
                strongSaves.Add(SaveConstants.Will);

            var saveGroup = GetCollection(creature);
            Assert.That(saveGroup, Is.EquivalentTo(strongSaves));
        }

        [TestCaseSource(typeof(CreatureTestData), nameof(CreatureTestData.Templates))]
        public void TemplateSaveGroupHasStrongSaves(string template)
        {
            var fortitude = collectionSelector.Explode(Config.Name, TableNameConstants.Collection.CreatureGroups, SaveConstants.Fortitude);
            var reflex = collectionSelector.Explode(Config.Name, TableNameConstants.Collection.CreatureGroups, SaveConstants.Reflex);
            var will = collectionSelector.Explode(Config.Name, TableNameConstants.Collection.CreatureGroups, SaveConstants.Will);

            var strongSaves = new List<string>();
            if (fortitude.Contains(template))
                strongSaves.Add(SaveConstants.Fortitude);
            if (reflex.Contains(template))
                strongSaves.Add(SaveConstants.Reflex);
            if (will.Contains(template))
                strongSaves.Add(SaveConstants.Will);

            var saveGroup = GetCollection(template);
            Assert.That(saveGroup, Is.EquivalentTo(strongSaves));
        }
    }
}
