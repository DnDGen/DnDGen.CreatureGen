using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Defenses;
using NUnit.Framework;

namespace DnDGen.CreatureGen.Tests.Integration.Tables.Creatures.CreatureGroups
{
    [TestFixture]
    public class CreatureSaveGroupsTests : CreatureGroupsTableTests
    {
        [Test]
        public void CreatureGroupNames()
        {
            AssertCreatureGroupNamesAreComplete();
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
            CreatureConstants.Groups.Dwarf,
            CreatureConstants.Groups.Elemental_Earth,
            CreatureConstants.Groups.Elemental_Water,
            CreatureConstants.Groups.Elf,
            CreatureConstants.Groups.Gnome,
            CreatureConstants.Groups.Halfling,
            CreatureConstants.Groups.Orc,
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
            CreatureConstants.Groups.Elemental_Air,
            CreatureConstants.Groups.Elemental_Fire,
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
            CreatureConstants.Types.Undead)]
        public void CreatureSaveGroup(string save, params string[] group)
        {
            AssertDistinctCollection(save, group);
        }
    }
}
