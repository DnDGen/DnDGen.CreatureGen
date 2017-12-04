using CreatureGen.Creatures;
using CreatureGen.Tables;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Races.BaseRaces
{
    [TestFixture]
    public class SwimSpeedsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get { return TableNameConstants.Set.Adjustments.SwimSpeeds; }
        }

        [Test]
        public void CollectionNames()
        {
            var names = CreatureConstants.All();
            AssertCollectionNames(names);
        }

        [TestCase(CreatureConstants.Aasimar, 0)]
        [TestCase(CreatureConstants.Elf_Aquatic, 40)]
        [TestCase(CreatureConstants.Azer, 0)]
        [TestCase(CreatureConstants.Slaad_Blue, 0)]
        [TestCase(CreatureConstants.Bugbear, 0)]
        [TestCase(CreatureConstants.Centaur, 0)]
        [TestCase(CreatureConstants.Giant_Cloud, 0)]
        [TestCase(CreatureConstants.Slaad_Death, 0)]
        [TestCase(CreatureConstants.Dwarf_Deep, 0)]
        [TestCase(CreatureConstants.Halfling_Deep, 0)]
        [TestCase(CreatureConstants.Derro, 0)]
        [TestCase(CreatureConstants.Doppelganger, 0)]
        [TestCase(CreatureConstants.Elf_Drow, 0)]
        [TestCase(CreatureConstants.Dwarf_Duergar, 0)]
        [TestCase(CreatureConstants.Giant_Fire, 0)]
        [TestCase(CreatureConstants.Gnome_Forest, 0)]
        [TestCase(CreatureConstants.Giant_Frost, 0)]
        [TestCase(CreatureConstants.Gargoyle, 0)]
        [TestCase(CreatureConstants.Githyanki, 0)]
        [TestCase(CreatureConstants.Githzerai, 0)]
        [TestCase(CreatureConstants.Gnoll, 0)]
        [TestCase(CreatureConstants.Goblin, 0)]
        [TestCase(CreatureConstants.Elf_Gray, 0)]
        [TestCase(CreatureConstants.Slaad_Gray, 0)]
        [TestCase(CreatureConstants.Slaad_Green, 0)]
        [TestCase(CreatureConstants.Grimlock, 0)]
        [TestCase(CreatureConstants.Elf_Half, 0)]
        [TestCase(CreatureConstants.Orc_Half, 0)]
        [TestCase(CreatureConstants.Harpy, 0)]
        [TestCase(CreatureConstants.Elf_High, 0)]
        [TestCase(CreatureConstants.Dwarf_Hill, 0)]
        [TestCase(CreatureConstants.Giant_Hill, 0)]
        [TestCase(CreatureConstants.Hobgoblin, 0)]
        [TestCase(CreatureConstants.HoundArchon, 0)]
        [TestCase(CreatureConstants.Human, 0)]
        [TestCase(CreatureConstants.Janni, 0)]
        [TestCase(CreatureConstants.Gargoyle_Kapoacinth, 60)]
        [TestCase(CreatureConstants.Kobold, 0)]
        [TestCase(CreatureConstants.KuoToa, 50)]
        [TestCase(CreatureConstants.Halfling_Lightfoot, 0)]
        [TestCase(CreatureConstants.Lizardfolk, 0)]
        [TestCase(CreatureConstants.Locathah, 60)]
        [TestCase(CreatureConstants.Merfolk, 50)]
        [TestCase(CreatureConstants.Ogre_Merrow, 40)]
        [TestCase(CreatureConstants.MindFlayer, 0)]
        [TestCase(CreatureConstants.Minotaur, 0)]
        [TestCase(CreatureConstants.Dwarf_Mountain, 0)]
        [TestCase(CreatureConstants.Ogre, 0)]
        [TestCase(CreatureConstants.OgreMage, 0)]
        [TestCase(CreatureConstants.Orc, 0)]
        [TestCase(CreatureConstants.Pixie, 0)]
        [TestCase(CreatureConstants.Rakshasa, 0)]
        [TestCase(CreatureConstants.Slaad_Red, 0)]
        [TestCase(CreatureConstants.Gnome_Rock, 0)]
        [TestCase(CreatureConstants.Sahuagin, 60)]
        [TestCase(CreatureConstants.Satyr, 0)]
        [TestCase(CreatureConstants.Scorpionfolk, 0)]
        [TestCase(CreatureConstants.Troll_Scrag, 40)]
        [TestCase(CreatureConstants.Giant_Stone, 0)]
        [TestCase(CreatureConstants.Giant_Storm, 40)]
        [TestCase(CreatureConstants.Gnome_Svirfneblin, 0)]
        [TestCase(CreatureConstants.Halfling_Tallfellow, 0)]
        [TestCase(CreatureConstants.Tiefling, 0)]
        [TestCase(CreatureConstants.Troglodyte, 0)]
        [TestCase(CreatureConstants.Troll, 0)]
        [TestCase(CreatureConstants.Elf_Wild, 0)]
        [TestCase(CreatureConstants.Elf_Wood, 0)]
        [TestCase(CreatureConstants.YuanTi_Abomination, 20)]
        [TestCase(CreatureConstants.YuanTi_Halfblood, 0)]
        [TestCase(CreatureConstants.YuanTi_Pureblood, 0)]
        public void SwimSpeed(string name, int speed)
        {
            base.Adjustment(name, speed);
        }
    }
}