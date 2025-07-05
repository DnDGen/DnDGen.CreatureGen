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
            groups[CreatureConstants.Ape] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Ape_Dire] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Arrowhawk_Juvenile] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Arrowhawk_Adult] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Arrowhawk_Elder] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Athach] = [SaveConstants.Fortitude, SaveConstants.Will];
            groups[CreatureConstants.Baboon] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Badger] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Badger_Dire] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Bat] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Bat_Dire] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Bear_Black] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Bear_Brown] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Bear_Dire] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Bear_Polar] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Beholder] = [SaveConstants.Will];
            groups[CreatureConstants.Beholder_Gauth] = [SaveConstants.Will];
            groups[CreatureConstants.Belker] = [SaveConstants.Reflex];
            groups[CreatureConstants.Boar] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Boar_Dire] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Bugbear] = [SaveConstants.Reflex];
            groups[CreatureConstants.Dwarf_Deep] = [SaveConstants.Fortitude];
            groups[CreatureConstants.Dwarf_Duergar] = [SaveConstants.Fortitude];
            groups[CreatureConstants.Dwarf_Hill] = [SaveConstants.Fortitude];
            groups[CreatureConstants.Dwarf_Mountain] = [SaveConstants.Fortitude];
            groups[CreatureConstants.Elemental_Air_Elder] = [SaveConstants.Reflex];
            groups[CreatureConstants.Elemental_Air_Greater] = [SaveConstants.Reflex];
            groups[CreatureConstants.Elemental_Air_Huge] = [SaveConstants.Reflex];
            groups[CreatureConstants.Elemental_Air_Large] = [SaveConstants.Reflex];
            groups[CreatureConstants.Elemental_Air_Medium] = [SaveConstants.Reflex];
            groups[CreatureConstants.Elemental_Air_Small] = [SaveConstants.Reflex];
            groups[CreatureConstants.Elemental_Earth_Elder] = [SaveConstants.Fortitude];
            groups[CreatureConstants.Elemental_Earth_Greater] = [SaveConstants.Fortitude];
            groups[CreatureConstants.Elemental_Earth_Huge] = [SaveConstants.Fortitude];
            groups[CreatureConstants.Elemental_Earth_Large] = [SaveConstants.Fortitude];
            groups[CreatureConstants.Elemental_Earth_Medium] = [SaveConstants.Fortitude];
            groups[CreatureConstants.Elemental_Earth_Small] = [SaveConstants.Fortitude];
            groups[CreatureConstants.Elemental_Fire_Elder] = [SaveConstants.Reflex];
            groups[CreatureConstants.Elemental_Fire_Greater] = [SaveConstants.Reflex];
            groups[CreatureConstants.Elemental_Fire_Huge] = [SaveConstants.Reflex];
            groups[CreatureConstants.Elemental_Fire_Large] = [SaveConstants.Reflex];
            groups[CreatureConstants.Elemental_Fire_Medium] = [SaveConstants.Reflex];
            groups[CreatureConstants.Elemental_Fire_Small] = [SaveConstants.Reflex];
            groups[CreatureConstants.Elemental_Water_Elder] = [SaveConstants.Fortitude];
            groups[CreatureConstants.Elemental_Water_Greater] = [SaveConstants.Fortitude];
            groups[CreatureConstants.Elemental_Water_Huge] = [SaveConstants.Fortitude];
            groups[CreatureConstants.Elemental_Water_Large] = [SaveConstants.Fortitude];
            groups[CreatureConstants.Elemental_Water_Medium] = [SaveConstants.Fortitude];
            groups[CreatureConstants.Elemental_Water_Small] = [SaveConstants.Fortitude];
            groups[CreatureConstants.Elf_Aquatic] = [SaveConstants.Fortitude];
            groups[CreatureConstants.Elf_Drow] = [SaveConstants.Fortitude];
            groups[CreatureConstants.Elf_Gray] = [SaveConstants.Fortitude];
            groups[CreatureConstants.Elf_Half] = [SaveConstants.Fortitude];
            groups[CreatureConstants.Elf_High] = [SaveConstants.Fortitude];
            groups[CreatureConstants.Elf_Wild] = [SaveConstants.Fortitude];
            groups[CreatureConstants.Elf_Wood] = [SaveConstants.Fortitude];
            groups[CreatureConstants.Githyanki] = [SaveConstants.Fortitude];
            groups[CreatureConstants.Githzerai] = [SaveConstants.Fortitude];
            groups[CreatureConstants.Gnoll] = [SaveConstants.Fortitude];
            groups[CreatureConstants.Gnome_Forest] = [SaveConstants.Fortitude];
            groups[CreatureConstants.Gnome_Rock] = [SaveConstants.Fortitude];
            groups[CreatureConstants.Gnome_Svirfneblin] = [SaveConstants.Fortitude];
            groups[CreatureConstants.Goblin] = [SaveConstants.Fortitude];
            groups[CreatureConstants.Halfling_Deep] = [SaveConstants.Fortitude];
            groups[CreatureConstants.Halfling_Lightfoot] = [SaveConstants.Fortitude];
            groups[CreatureConstants.Halfling_Tallfellow] = [SaveConstants.Fortitude];
            groups[CreatureConstants.Hobgoblin] = [SaveConstants.Fortitude];
            groups[CreatureConstants.Human] = [SaveConstants.Reflex];
            groups[CreatureConstants.InvisibleStalker] = [SaveConstants.Reflex];
            groups[CreatureConstants.Kobold] = [SaveConstants.Fortitude];
            groups[CreatureConstants.Lion] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Lion_Dire] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Lizard] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Reflex];
            groups[CreatureConstants.Lizardfolk] = [SaveConstants.Reflex];
            groups[CreatureConstants.Locathah] = [SaveConstants.Fortitude];
            groups[CreatureConstants.Magmin] = [SaveConstants.Reflex];
            groups[CreatureConstants.Merfolk] = [SaveConstants.Fortitude];
            groups[CreatureConstants.Orc] = [SaveConstants.Fortitude];
            groups[CreatureConstants.Orc_Half] = [SaveConstants.Fortitude];
            groups[CreatureConstants.Rat] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Rat_Dire] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Shark_Dire] = [SaveConstants.Fortitude, SaveConstants.Will];
            groups[CreatureConstants.Shark_Huge] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Shark_Large] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Shark_Medium] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Thoqqua] = [SaveConstants.Fortitude];
            groups[CreatureConstants.Tiger] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Tiger_Dire] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Tojanida_Juvenile] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Tojanida_Adult] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Tojanida_Elder] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Troglodyte] = [SaveConstants.Fortitude];
            groups[CreatureConstants.Weasel] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Weasel_Dire] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Wolf] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Wolf_Dire] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Wolverine] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Wolverine_Dire] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];

            groups[CreatureConstants.Templates.Skeleton] = [SaveConstants.Will];
            groups[CreatureConstants.Templates.Zombie] = [SaveConstants.Will];

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
                    Assert.That(saveGroups[creature], Contains.Item(save), $"{creature} ({creatureType}) should have {save} save");
                }
            }
        }
    }
}
