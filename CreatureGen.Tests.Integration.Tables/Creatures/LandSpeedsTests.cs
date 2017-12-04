using CreatureGen.Creatures;
using CreatureGen.Tables;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Races.BaseRaces
{
    [TestFixture]
    public class LandSpeedsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get { return TableNameConstants.Set.Adjustments.LandSpeeds; }
        }

        [Test]
        public void CollectionNames()
        {
            var names = CreatureConstants.All();
            AssertCollectionNames(names);
        }

        [TestCase(CreatureConstants.Aasimar, 30)]
        [TestCase(CreatureConstants.Elf_Aquatic, 30)]
        [TestCase(CreatureConstants.Azer, 30)]
        [TestCase(CreatureConstants.Slaad_Blue, 30)]
        [TestCase(CreatureConstants.Bugbear, 30)]
        [TestCase(CreatureConstants.Centaur, 50)]
        [TestCase(CreatureConstants.Giant_Cloud, 50)]
        [TestCase(CreatureConstants.Slaad_Death, 30)]
        [TestCase(CreatureConstants.Dwarf_Deep, 20)]
        [TestCase(CreatureConstants.Halfling_Deep, 20)]
        [TestCase(CreatureConstants.Derro, 20)]
        [TestCase(CreatureConstants.Doppelganger, 30)]
        [TestCase(CreatureConstants.Elf_Drow, 30)]
        [TestCase(CreatureConstants.Dwarf_Duergar, 20)]
        [TestCase(CreatureConstants.Giant_Fire, 40)]
        [TestCase(CreatureConstants.Gnome_Forest, 20)]
        [TestCase(CreatureConstants.Giant_Frost, 40)]
        [TestCase(CreatureConstants.Gargoyle, 40)]
        [TestCase(CreatureConstants.Githyanki, 30)]
        [TestCase(CreatureConstants.Githzerai, 30)]
        [TestCase(CreatureConstants.Gnoll, 30)]
        [TestCase(CreatureConstants.Goblin, 30)]
        [TestCase(CreatureConstants.Elf_Gray, 30)]
        [TestCase(CreatureConstants.Slaad_Gray, 30)]
        [TestCase(CreatureConstants.Slaad_Green, 30)]
        [TestCase(CreatureConstants.Grimlock, 30)]
        [TestCase(CreatureConstants.Elf_Half, 30)]
        [TestCase(CreatureConstants.Orc_Half, 30)]
        [TestCase(CreatureConstants.Harpy, 20)]
        [TestCase(CreatureConstants.Elf_High, 30)]
        [TestCase(CreatureConstants.Dwarf_Hill, 20)]
        [TestCase(CreatureConstants.Giant_Hill, 40)]
        [TestCase(CreatureConstants.Hobgoblin, 30)]
        [TestCase(CreatureConstants.HoundArchon, 40)]
        [TestCase(CreatureConstants.Human, 30)]
        [TestCase(CreatureConstants.Janni, 30)]
        [TestCase(CreatureConstants.Gargoyle_Kapoacinth, 40)]
        [TestCase(CreatureConstants.Kobold, 30)]
        [TestCase(CreatureConstants.KuoToa, 20)]
        [TestCase(CreatureConstants.Halfling_Lightfoot, 20)]
        [TestCase(CreatureConstants.Lizardfolk, 30)]
        [TestCase(CreatureConstants.Locathah, 10)]
        [TestCase(CreatureConstants.Merfolk, 5)]
        [TestCase(CreatureConstants.Ogre_Merrow, 30)]
        [TestCase(CreatureConstants.MindFlayer, 30)]
        [TestCase(CreatureConstants.Minotaur, 30)]
        [TestCase(CreatureConstants.Dwarf_Mountain, 20)]
        [TestCase(CreatureConstants.Ogre, 40)]
        [TestCase(CreatureConstants.OgreMage, 50)]
        [TestCase(CreatureConstants.Orc, 30)]
        [TestCase(CreatureConstants.Pixie, 20)]
        [TestCase(CreatureConstants.Rakshasa, 40)]
        [TestCase(CreatureConstants.Slaad_Red, 30)]
        [TestCase(CreatureConstants.Gnome_Rock, 20)]
        [TestCase(CreatureConstants.Sahuagin, 30)]
        [TestCase(CreatureConstants.Satyr, 40)]
        [TestCase(CreatureConstants.Scorpionfolk, 40)]
        [TestCase(CreatureConstants.Troll_Scrag, 20)]
        [TestCase(CreatureConstants.Giant_Stone, 40)]
        [TestCase(CreatureConstants.Giant_Storm, 50)]
        [TestCase(CreatureConstants.Gnome_Svirfneblin, 20)]
        [TestCase(CreatureConstants.Halfling_Tallfellow, 20)]
        [TestCase(CreatureConstants.Tiefling, 30)]
        [TestCase(CreatureConstants.Troglodyte, 30)]
        [TestCase(CreatureConstants.Troll, 30)]
        [TestCase(CreatureConstants.Elf_Wild, 30)]
        [TestCase(CreatureConstants.Elf_Wood, 30)]
        [TestCase(CreatureConstants.YuanTi_Abomination, 30)]
        [TestCase(CreatureConstants.YuanTi_Halfblood, 30)]
        [TestCase(CreatureConstants.YuanTi_Pureblood, 30)]
        public void LandSpeed(string name, int speed)
        {
            base.Adjustment(name, speed);
        }
    }
}