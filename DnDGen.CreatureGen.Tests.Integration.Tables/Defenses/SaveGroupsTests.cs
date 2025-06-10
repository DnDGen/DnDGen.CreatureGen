using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Defenses;
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

            groups[CreatureConstants.Aasimar] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Aboleth] = [SaveConstants.Will];
            groups[CreatureConstants.Achaierai] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Arrowhawk_Juvenile] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Arrowhawk_Adult] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Arrowhawk_Elder] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Elemental_Air_Elder] = [SaveConstants.Reflex];
            groups[CreatureConstants.Elemental_Air_Greater] = [SaveConstants.Reflex];
            groups[CreatureConstants.Elemental_Air_Huge] = [SaveConstants.Reflex];
            groups[CreatureConstants.Elemental_Air_Large] = [SaveConstants.Reflex];
            groups[CreatureConstants.Elemental_Air_Medium] = [SaveConstants.Reflex];
            groups[CreatureConstants.Elemental_Air_Small] = [SaveConstants.Reflex];
            groups[CreatureConstants.Elemental_Fire_Elder] = [SaveConstants.Reflex];
            groups[CreatureConstants.Elemental_Fire_Greater] = [SaveConstants.Reflex];
            groups[CreatureConstants.Elemental_Fire_Huge] = [SaveConstants.Reflex];
            groups[CreatureConstants.Elemental_Fire_Large] = [SaveConstants.Reflex];
            groups[CreatureConstants.Elemental_Fire_Medium] = [SaveConstants.Reflex];
            groups[CreatureConstants.Elemental_Fire_Small] = [SaveConstants.Reflex];
            groups[CreatureConstants.Tojanida_Juvenile] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Tojanida_Adult] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Tojanida_Elder] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];

            //TODO: Fill in settings based on existing groups

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

        [TestCaseSource(typeof(CreatureTestData), nameof(CreatureTestData.Creatures))]
        public void CreatureSaveGroupHasStrongSaves(string creature)
        {
            AssertDistinctCollection(creature, [.. saveGroups[creature]]);
        }

        [TestCaseSource(typeof(CreatureTestData), nameof(CreatureTestData.Templates))]
        public void TemplateSaveGroupHasStrongSaves(string template)
        {
            AssertDistinctCollection(template, [.. saveGroups[template]]);
        }
    }
}
