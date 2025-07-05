using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Defenses;
using DnDGen.CreatureGen.Selectors.Selections;
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
        protected override string tableName => TableNameConstants.Collection.SaveGroups;

        private Dictionary<string, IEnumerable<string>> saveGroups;
        private ICollectionDataSelector<CreatureDataSelection> creatureDataSelector;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            saveGroups = GetSaveGroups();
        }

        [SetUp]
        public void Setup()
        {
            creatureDataSelector = GetNewInstanceOf<ICollectionDataSelector<CreatureDataSelection>>();
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

        [TestCase(SaveConstants.Fortitude,
            CreatureConstants.Githyanki,
            CreatureConstants.Githzerai,
            CreatureConstants.Gnoll,
            CreatureConstants.Goblin,
            CreatureConstants.Hobgoblin,
            CreatureConstants.Kobold,
            CreatureConstants.Locathah,
            CreatureConstants.Merfolk,
            CreatureConstants.Thoqqua,
            CreatureConstants.Troglodyte,
            CreatureConstants.Dwarf_Deep,
            CreatureConstants.Dwarf_Duergar,
            CreatureConstants.Dwarf_Hill,
            CreatureConstants.Dwarf_Mountain,
            CreatureConstants.Elemental_Earth_Elder,
            CreatureConstants.Elemental_Earth_Greater,
            CreatureConstants.Elemental_Earth_Huge,
            CreatureConstants.Elemental_Earth_Large,
            CreatureConstants.Elemental_Earth_Medium,
            CreatureConstants.Elemental_Earth_Small,
            CreatureConstants.Elemental_Water_Elder,
            CreatureConstants.Elemental_Water_Greater,
            CreatureConstants.Elemental_Water_Huge,
            CreatureConstants.Elemental_Water_Large,
            CreatureConstants.Elemental_Water_Medium,
            CreatureConstants.Elemental_Water_Small,
            CreatureConstants.Elf_Aquatic,
            CreatureConstants.Elf_Drow,
            CreatureConstants.Elf_Gray,
            CreatureConstants.Elf_Half,
            CreatureConstants.Elf_High,
            CreatureConstants.Elf_Wild,
            CreatureConstants.Elf_Wood,
            CreatureConstants.Gnome_Forest,
            CreatureConstants.Gnome_Rock,
            CreatureConstants.Gnome_Svirfneblin,
            CreatureConstants.Halfling_Deep,
            CreatureConstants.Halfling_Lightfoot,
            CreatureConstants.Halfling_Tallfellow,
            CreatureConstants.Orc,
            CreatureConstants.Orc_Half,
            CreatureConstants.Types.Animal,
            CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Giant,
            CreatureConstants.Types.MagicalBeast,
            CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Plant,
            CreatureConstants.Types.Vermin)]
        [TestCase(SaveConstants.Reflex,
            CreatureConstants.Belker,
            CreatureConstants.Bugbear,
            CreatureConstants.Human,
            CreatureConstants.InvisibleStalker,
            CreatureConstants.Lizardfolk,
            CreatureConstants.Magmin,
            CreatureConstants.Elemental_Air_Elder,
            CreatureConstants.Elemental_Air_Greater,
            CreatureConstants.Elemental_Air_Huge,
            CreatureConstants.Elemental_Air_Large,
            CreatureConstants.Elemental_Air_Medium,
            CreatureConstants.Elemental_Air_Small,
            CreatureConstants.Elemental_Fire_Elder,
            CreatureConstants.Elemental_Fire_Greater,
            CreatureConstants.Elemental_Fire_Huge,
            CreatureConstants.Elemental_Fire_Large,
            CreatureConstants.Elemental_Fire_Medium,
            CreatureConstants.Elemental_Fire_Small,
            CreatureConstants.Types.Animal,
            CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Fey,
            CreatureConstants.Types.MagicalBeast,
            CreatureConstants.Types.MonstrousHumanoid,
            CreatureConstants.Types.Outsider)]
        [TestCase(SaveConstants.Will,
            CreatureConstants.Ape_Dire,
            CreatureConstants.Badger_Dire,
            CreatureConstants.Bat_Dire,
            CreatureConstants.Bear_Dire,
            CreatureConstants.Boar_Dire,
            CreatureConstants.Lion_Dire,
            CreatureConstants.Rat_Dire,
            CreatureConstants.Shark_Dire,
            CreatureConstants.Tiger_Dire,
            CreatureConstants.Weasel_Dire,
            CreatureConstants.Wolf_Dire,
            CreatureConstants.Wolverine_Dire,
            CreatureConstants.Types.Aberration,
            CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Fey,
            CreatureConstants.Types.MonstrousHumanoid,
            CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Undead,
            CreatureConstants.Templates.Skeleton,
            CreatureConstants.Templates.Zombie)]
        public void LEGACY_CreatureSaveGroup(string save, params string[] group)
        {
            var creatures = CreatureConstants.GetAll();
            var types = CreatureConstants.Types.GetAll();

            foreach (var creature in group.Intersect(creatures))
            {
                Assert.That(saveGroups, Contains.Key(creature), creature);
                Assert.That(saveGroups[creature], Contains.Item(save), $"{creature} should have {save} save");
            }

            var creatureData = creatureDataSelector.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureData);

            foreach (var creatureType in group.Intersect(types))
            {
                var typeCreatures = creatureData
                    .Where(d => d.Value.Single().Types.Contains(creatureType))
                    .Select(d => d.Key);

                foreach (var creature in typeCreatures)
                {
                    Assert.That(saveGroups, Contains.Key(creature), creature);
                    Assert.That(saveGroups[creature], Contains.Item(save), $"{creature} should have {save} save");
                }
            }
        }
    }
}
