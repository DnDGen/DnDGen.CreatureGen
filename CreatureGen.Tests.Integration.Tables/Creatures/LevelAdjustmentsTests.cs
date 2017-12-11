using CreatureGen.Creatures;
using CreatureGen.Tables;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Creatures
{
    [TestFixture]
    public class LevelAdjustmentsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get { return TableNameConstants.Set.Adjustments.LevelAdjustments; }
        }

        [Test]
        public void CollectionNames()
        {
            var names = CreatureConstants.All();
            AssertCollectionNames(names);
        }

        [TestCase(CreatureConstants.Aasimar, 1)]
        [TestCase(CreatureConstants.Elf_Aquatic, 0)]
        [TestCase(CreatureConstants.Azer, 4)]
        [TestCase(CreatureConstants.Slaad_Blue, 6)]
        [TestCase(CreatureConstants.Bugbear, 1)]
        [TestCase(CreatureConstants.Centaur, 2)]
        [TestCase(CreatureConstants.Giant_Cloud, 4)]
        [TestCase(CreatureConstants.Slaad_Death, 6)]
        [TestCase(CreatureConstants.Dwarf_Deep, 0)]
        [TestCase(CreatureConstants.Halfling_Deep, 0)]
        [TestCase(CreatureConstants.Derro, 0)]
        [TestCase(CreatureConstants.Doppelganger, 4)]
        [TestCase(CreatureConstants.Elf_Drow, 2)]
        [TestCase(CreatureConstants.Dwarf_Duergar, 1)]
        [TestCase(CreatureConstants.Giant_Fire, 4)]
        [TestCase(CreatureConstants.Gnome_Forest, 0)]
        [TestCase(CreatureConstants.Giant_Frost, 4)]
        [TestCase(CreatureConstants.Gargoyle, 5)]
        [TestCase(CreatureConstants.Githyanki, 2)]
        [TestCase(CreatureConstants.Githzerai, 2)]
        [TestCase(CreatureConstants.Gnoll, 1)]
        [TestCase(CreatureConstants.Goblin, 0)]
        [TestCase(CreatureConstants.Elf_Gray, 0)]
        [TestCase(CreatureConstants.Slaad_Gray, 6)]
        [TestCase(CreatureConstants.Slaad_Green, 6)]
        [TestCase(CreatureConstants.Grimlock, 2)]
        [TestCase(CreatureConstants.Elf_Half, 0)]
        [TestCase(CreatureConstants.Orc_Half, 0)]
        [TestCase(CreatureConstants.Harpy, 3)]
        [TestCase(CreatureConstants.Elf_High, 0)]
        [TestCase(CreatureConstants.Dwarf_Hill, 0)]
        [TestCase(CreatureConstants.Giant_Hill, 4)]
        [TestCase(CreatureConstants.Hobgoblin, 1)]
        [TestCase(CreatureConstants.HoundArchon, 5)]
        [TestCase(CreatureConstants.Human, 0)]
        [TestCase(CreatureConstants.Janni, 5)]
        [TestCase(CreatureConstants.Gargoyle_Kapoacinth, 5)]
        [TestCase(CreatureConstants.Kobold, 0)]
        [TestCase(CreatureConstants.KuoToa, 3)]
        [TestCase(CreatureConstants.Halfling_Lightfoot, 0)]
        [TestCase(CreatureConstants.Lizardfolk, 1)]
        [TestCase(CreatureConstants.Locathah, 1)]
        [TestCase(CreatureConstants.Merfolk, 1)]
        [TestCase(CreatureConstants.Ogre_Merrow, 2)]
        [TestCase(CreatureConstants.MindFlayer, 7)]
        [TestCase(CreatureConstants.Minotaur, 2)]
        [TestCase(CreatureConstants.Dwarf_Mountain, 0)]
        [TestCase(CreatureConstants.Ogre, 2)]
        [TestCase(CreatureConstants.OgreMage, 7)]
        [TestCase(CreatureConstants.Orc, 0)]
        [TestCase(CreatureConstants.Pixie, 4)]
        [TestCase(CreatureConstants.Rakshasa, 7)]
        [TestCase(CreatureConstants.Slaad_Red, 6)]
        [TestCase(CreatureConstants.Gnome_Rock, 0)]
        [TestCase(CreatureConstants.Sahuagin, 2)]
        [TestCase(CreatureConstants.Satyr, 2)]
        [TestCase(CreatureConstants.Scorpionfolk, 4)]
        [TestCase(CreatureConstants.Troll_Scrag, 5)]
        [TestCase(CreatureConstants.Giant_Stone, 4)]
        [TestCase(CreatureConstants.Giant_Storm, 4)]
        [TestCase(CreatureConstants.Gnome_Svirfneblin, 3)]
        [TestCase(CreatureConstants.Halfling_Tallfellow, 0)]
        [TestCase(CreatureConstants.Tiefling, 1)]
        [TestCase(CreatureConstants.Troglodyte, 2)]
        [TestCase(CreatureConstants.Troll, 5)]
        [TestCase(CreatureConstants.Elf_Wild, 0)]
        [TestCase(CreatureConstants.Elf_Wood, 0)]
        [TestCase(CreatureConstants.YuanTi_Abomination, 7)]
        [TestCase(CreatureConstants.YuanTi_Halfblood, 5)]
        [TestCase(CreatureConstants.YuanTi_Pureblood, 2)]
        public void LevelAdjustment(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}