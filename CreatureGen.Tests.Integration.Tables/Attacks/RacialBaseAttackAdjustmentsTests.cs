using CreatureGen.Tables;
using CreatureGen.Creatures;
using NUnit.Framework;
using System.Linq;

namespace CreatureGen.Tests.Integration.Tables.Combats
{
    [TestFixture]
    public class RacialBaseAttackAdjustmentsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get { return TableNameConstants.Set.Adjustments.RacialBaseAttackAdjustments; }
        }

        [Test]
        public override void CollectionNames()
        {
            var baseRaceGroups = CollectionsMapper.Map(TableNameConstants.Set.Collection.BaseRaceGroups);
            var metaraceGroups = CollectionsMapper.Map(TableNameConstants.Set.Collection.MetaraceGroups);

            var names = metaraceGroups[GroupConstants.All].Union(baseRaceGroups[GroupConstants.All]);
            AssertCollectionNames(names);
        }

        [TestCase(CreatureConstants.Aasimar, 0)]
        [TestCase(CreatureConstants.Elf_Aquatic, 0)]
        [TestCase(CreatureConstants.Azer, 2)]
        [TestCase(CreatureConstants.Slaad_Blue, 8)]
        [TestCase(CreatureConstants.Bugbear, 2)]
        [TestCase(CreatureConstants.Centaur, 4)]
        [TestCase(CreatureConstants.Giant_Cloud, 12)]
        [TestCase(CreatureConstants.Slaad_Death, 15)]
        [TestCase(CreatureConstants.Dwarf_Deep, 0)]
        [TestCase(CreatureConstants.Halfling_Deep, 0)]
        [TestCase(CreatureConstants.Doppelganger, 4)]
        [TestCase(CreatureConstants.Derro, 3)]
        [TestCase(CreatureConstants.Elf_Drow, 0)]
        [TestCase(CreatureConstants.Duergar, 0)]
        [TestCase(CreatureConstants.Giant_Fire, 11)]
        [TestCase(CreatureConstants.Gnome_Forest, 0)]
        [TestCase(CreatureConstants.Giant_Frost, 10)]
        [TestCase(CreatureConstants.Gargoyle, 4)]
        [TestCase(CreatureConstants.Githyanki, 0)]
        [TestCase(CreatureConstants.Githzerai, 0)]
        [TestCase(CreatureConstants.Gnoll, 1)]
        [TestCase(CreatureConstants.Goblin, 0)]
        [TestCase(CreatureConstants.Elf_Gray, 0)]
        [TestCase(CreatureConstants.Slaad_Gray, 10)]
        [TestCase(CreatureConstants.Slaad_Green, 9)]
        [TestCase(CreatureConstants.Grimlock, 2)]
        [TestCase(CreatureConstants.Elf_Half, 0)]
        [TestCase(CreatureConstants.Orc_Half, 0)]
        [TestCase(CreatureConstants.Harpy, 7)]
        [TestCase(CreatureConstants.Elf_High, 0)]
        [TestCase(CreatureConstants.Dwarf_Hill, 0)]
        [TestCase(CreatureConstants.Giant_Hill, 9)]
        [TestCase(CreatureConstants.Hobgoblin, 0)]
        [TestCase(CreatureConstants.HoundArchon, 6)]
        [TestCase(CreatureConstants.Human, 0)]
        [TestCase(CreatureConstants.Janni, 6)]
        [TestCase(CreatureConstants.Gargoyle_Kapoacinth, 4)]
        [TestCase(CreatureConstants.Kobold, 0)]
        [TestCase(CreatureConstants.KuoToa, 0)]
        [TestCase(CreatureConstants.Halfling_Lightfoot, 0)]
        [TestCase(CreatureConstants.Lizardfolk, 1)]
        [TestCase(CreatureConstants.Locathah, 0)]
        [TestCase(CreatureConstants.Merfolk, 0)]
        [TestCase(CreatureConstants.Ogre_Merrow, 3)]
        [TestCase(CreatureConstants.MindFlayer, 6)]
        [TestCase(CreatureConstants.Minotaur, 6)]
        [TestCase(CreatureConstants.Dwarf_Mountain, 0)]
        [TestCase(CreatureConstants.Ogre, 3)]
        [TestCase(CreatureConstants.OgreMage, 3)]
        [TestCase(CreatureConstants.Orc, 0)]
        [TestCase(CreatureConstants.Pixie, 0)]
        [TestCase(CreatureConstants.Rakshasa, 7)]
        [TestCase(CreatureConstants.Slaad_Red, 7)]
        [TestCase(CreatureConstants.Gnome_Rock, 0)]
        [TestCase(CreatureConstants.Sahuagin, 2)]
        [TestCase(CreatureConstants.Satyr, 2)]
        [TestCase(CreatureConstants.Scorpionfolk, 12)]
        [TestCase(CreatureConstants.Troll_Scrag, 4)]
        [TestCase(CreatureConstants.Giant_Stone, 10)]
        [TestCase(CreatureConstants.Giant_Storm, 14)]
        [TestCase(CreatureConstants.Svirfneblin, 0)]
        [TestCase(CreatureConstants.Halfling_Tallfellow, 0)]
        [TestCase(CreatureConstants.Tiefling, 0)]
        [TestCase(CreatureConstants.Troglodyte, 1)]
        [TestCase(CreatureConstants.Troll, 4)]
        [TestCase(CreatureConstants.Elf_Wild, 0)]
        [TestCase(CreatureConstants.Elf_Wood, 0)]
        [TestCase(CreatureConstants.YuanTi_Abomination, 9)]
        [TestCase(CreatureConstants.YuanTi_Halfblood, 7)]
        [TestCase(CreatureConstants.YuanTi_Pureblood, 4)]
        [TestCase(CreatureConstants.Templates.Ghost, 0)]
        [TestCase(CreatureConstants.Templates.HalfCelestial, 0)]
        [TestCase(CreatureConstants.Templates.HalfDragon, 0)]
        [TestCase(CreatureConstants.Templates.HalfFiend, 0)]
        [TestCase(CreatureConstants.Templates.Lich, 0)]
        [TestCase(CreatureConstants.Templates.Mummy, 4)]
        [TestCase(CreatureConstants.Templates.None, 0)]
        [TestCase(CreatureConstants.Templates.Vampire, 0)]
        [TestCase(CreatureConstants.Templates.Werebear, 5)]
        [TestCase(CreatureConstants.Templates.Wereboar, 3)]
        [TestCase(CreatureConstants.Templates.Wererat, 1)]
        [TestCase(CreatureConstants.Templates.Weretiger, 5)]
        [TestCase(CreatureConstants.Templates.Werewolf, 2)]
        public void RacialBaseAttackAdjustment(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}