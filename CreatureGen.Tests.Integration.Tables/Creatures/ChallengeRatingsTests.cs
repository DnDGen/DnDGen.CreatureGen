using CreatureGen.Creatures;
using CreatureGen.Tables;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Creatures
{
    [TestFixture]
    public class ChallengeRatingsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get
            {
                return TableNameConstants.Set.Adjustments.ChallengeRatings;
            }
        }

        [Test]
        public void CollectionNames()
        {
            var creatures = CreatureConstants.All();
            AssertCollectionNames(creatures);
        }

        [TestCase(CreatureConstants.Aasimar, 0)]
        [TestCase(CreatureConstants.Elf_Aquatic, 0)]
        [TestCase(CreatureConstants.Azer, 2)]
        [TestCase(CreatureConstants.Slaad_Blue, 8)]
        [TestCase(CreatureConstants.Bugbear, 2)]
        [TestCase(CreatureConstants.Centaur, 3)]
        [TestCase(CreatureConstants.Giant_Cloud, 11)]
        [TestCase(CreatureConstants.Slaad_Death, 13)]
        [TestCase(CreatureConstants.Dwarf_Deep, 0)]
        [TestCase(CreatureConstants.Halfling_Deep, 0)]
        [TestCase(CreatureConstants.Derro, 3)]
        [TestCase(CreatureConstants.Doppelganger, 3)]
        [TestCase(CreatureConstants.Elf_Drow, 0)]
        [TestCase(CreatureConstants.Dwarf_Duergar, 0)]
        [TestCase(CreatureConstants.Giant_Fire, 10)]
        [TestCase(CreatureConstants.Gnome_Forest, 0)]
        [TestCase(CreatureConstants.Giant_Frost, 9)]
        [TestCase(CreatureConstants.Gargoyle, 4)]
        [TestCase(CreatureConstants.Githyanki, 0)]
        [TestCase(CreatureConstants.Githzerai, 0)]
        [TestCase(CreatureConstants.Gnoll, 1)]
        [TestCase(CreatureConstants.Goblin, 0)]
        [TestCase(CreatureConstants.Elf_Gray, 0)]
        [TestCase(CreatureConstants.Slaad_Gray, 10)]
        [TestCase(CreatureConstants.Slaad_Green, 9)]
        [TestCase(CreatureConstants.Grimlock, 1)]
        [TestCase(CreatureConstants.Elf_Half, 0)]
        [TestCase(CreatureConstants.Orc_Half, 0)]
        [TestCase(CreatureConstants.Harpy, 4)]
        [TestCase(CreatureConstants.Elf_High, 0)]
        [TestCase(CreatureConstants.Dwarf_Hill, 0)]
        [TestCase(CreatureConstants.Giant_Hill, 7)]
        [TestCase(CreatureConstants.Hobgoblin, 0)]
        [TestCase(CreatureConstants.HoundArchon, 5)]
        [TestCase(CreatureConstants.Human, 0)]
        [TestCase(CreatureConstants.Janni, 4)]
        [TestCase(CreatureConstants.Gargoyle_Kapoacinth, 4)]
        [TestCase(CreatureConstants.Kobold, 0)]
        [TestCase(CreatureConstants.KuoToa, 2)]
        [TestCase(CreatureConstants.Halfling_Lightfoot, 0)]
        [TestCase(CreatureConstants.Lizardfolk, 1)]
        [TestCase(CreatureConstants.Locathah, 0)]
        [TestCase(CreatureConstants.Merfolk, 0)]
        [TestCase(CreatureConstants.Ogre_Merrow, 3)]
        [TestCase(CreatureConstants.MindFlayer, 8)]
        [TestCase(CreatureConstants.Minotaur, 4)]
        [TestCase(CreatureConstants.Dwarf_Mountain, 0)]
        [TestCase(CreatureConstants.Ogre, 3)]
        [TestCase(CreatureConstants.OgreMage, 8)]
        [TestCase(CreatureConstants.Orc, 0)]
        [TestCase(CreatureConstants.Pixie, 4)]
        [TestCase(CreatureConstants.Rakshasa, 10)]
        [TestCase(CreatureConstants.Slaad_Red, 7)]
        [TestCase(CreatureConstants.Gnome_Rock, 0)]
        [TestCase(CreatureConstants.Sahuagin, 2)]
        [TestCase(CreatureConstants.Satyr, 2)]
        [TestCase(CreatureConstants.Scorpionfolk, 7)]
        [TestCase(CreatureConstants.Troll_Scrag, 5)]
        [TestCase(CreatureConstants.Giant_Stone, 8)]
        [TestCase(CreatureConstants.Giant_Stone_Elder, 9)]
        [TestCase(CreatureConstants.Giant_Storm, 13)]
        [TestCase(CreatureConstants.Gnome_Svirfneblin, 0)]
        [TestCase(CreatureConstants.Halfling_Tallfellow, 0)]
        [TestCase(CreatureConstants.Tiefling, 0)]
        [TestCase(CreatureConstants.Troglodyte, 1)]
        [TestCase(CreatureConstants.Troll, 5)]
        [TestCase(CreatureConstants.Elf_Wild, 0)]
        [TestCase(CreatureConstants.Elf_Wood, 0)]
        [TestCase(CreatureConstants.YuanTi_Abomination, 7)]
        [TestCase(CreatureConstants.YuanTi_Halfblood, 5)]
        [TestCase(CreatureConstants.YuanTi_Pureblood, 3)]
        public void ChallengeRating(string name, int challengeRating)
        {
            base.Adjustment(name, challengeRating);
        }
    }
}
