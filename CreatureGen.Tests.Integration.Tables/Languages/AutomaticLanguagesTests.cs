using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Tables;
using CreatureGen.Languages;
using CreatureGen.Creatures;
using NUnit.Framework;
using System.Linq;

namespace CreatureGen.Tests.Integration.Tables.Languages
{
    [TestFixture]
    public class AutomaticLanguagesTests : CollectionTests
    {
        protected override string tableName
        {
            get { return TableNameConstants.Set.Collection.AutomaticLanguages; }
        }

        [Test]
        public override void CollectionNames()
        {
            var baseRaceGroups = CollectionsMapper.Map(TableNameConstants.Set.Collection.BaseRaceGroups);
            var metaraceGroups = CollectionsMapper.Map(TableNameConstants.Set.Collection.MetaraceGroups);
            var classes = CollectionsMapper.Map(TableNameConstants.Set.Collection.ClassNameGroups);

            var names = classes[GroupConstants.All].Union(baseRaceGroups[GroupConstants.All]).Union(metaraceGroups[GroupConstants.All]);

            AssertCollectionNames(names);
        }

        [TestCase(CharacterClassConstants.Adept)]
        [TestCase(CharacterClassConstants.Aristocrat)]
        [TestCase(CharacterClassConstants.Barbarian)]
        [TestCase(CharacterClassConstants.Bard)]
        [TestCase(CharacterClassConstants.Cleric)]
        [TestCase(CharacterClassConstants.Commoner)]
        [TestCase(CharacterClassConstants.Druid, LanguageConstants.Special.Druidic)]
        [TestCase(CharacterClassConstants.Expert)]
        [TestCase(CharacterClassConstants.Fighter)]
        [TestCase(CharacterClassConstants.Monk)]
        [TestCase(CharacterClassConstants.Paladin)]
        [TestCase(CharacterClassConstants.Ranger)]
        [TestCase(CharacterClassConstants.Rogue)]
        [TestCase(CharacterClassConstants.Sorcerer)]
        [TestCase(CharacterClassConstants.Warrior)]
        [TestCase(CharacterClassConstants.Wizard)]
        [TestCase(SizeConstants.BaseRaces.Aasimar,
            LanguageConstants.Common,
            LanguageConstants.Celestial)]
        [TestCase(SizeConstants.BaseRaces.AquaticElf,
            LanguageConstants.Common,
            LanguageConstants.Elven)]
        [TestCase(SizeConstants.BaseRaces.Azer,
            LanguageConstants.Common,
            LanguageConstants.Ignan)]
        [TestCase(SizeConstants.BaseRaces.BlueSlaad, LanguageConstants.Special.Slaad)]
        [TestCase(SizeConstants.BaseRaces.Bugbear,
            LanguageConstants.Common,
            LanguageConstants.Goblin)]
        [TestCase(SizeConstants.BaseRaces.Centaur,
            LanguageConstants.Sylvan,
            LanguageConstants.Elven)]
        [TestCase(SizeConstants.BaseRaces.CloudGiant,
            LanguageConstants.Common,
            LanguageConstants.Giant)]
        [TestCase(SizeConstants.BaseRaces.DeathSlaad,
            LanguageConstants.Special.Slaad,
            LanguageConstants.Common)]
        [TestCase(SizeConstants.BaseRaces.DeepDwarf,
            LanguageConstants.Common,
            LanguageConstants.Dwarven)]
        [TestCase(SizeConstants.BaseRaces.DeepHalfling,
            LanguageConstants.Common,
            LanguageConstants.Halfling,
            LanguageConstants.Dwarven)]
        [TestCase(SizeConstants.BaseRaces.Derro,
            LanguageConstants.Common,
            LanguageConstants.Dwarven)]
        [TestCase(SizeConstants.BaseRaces.Doppelganger, LanguageConstants.Common)]
        [TestCase(SizeConstants.BaseRaces.Drow,
            LanguageConstants.Common,
            LanguageConstants.Elven,
            LanguageConstants.Undercommon)]
        [TestCase(SizeConstants.BaseRaces.DuergarDwarf,
            LanguageConstants.Common,
            LanguageConstants.Dwarven,
            LanguageConstants.Undercommon)]
        [TestCase(SizeConstants.BaseRaces.FireGiant,
            LanguageConstants.Common,
            LanguageConstants.Giant)]
        [TestCase(SizeConstants.BaseRaces.ForestGnome,
            LanguageConstants.Common,
            LanguageConstants.Elven,
            LanguageConstants.Gnome,
            LanguageConstants.Sylvan)]
        [TestCase(SizeConstants.BaseRaces.FrostGiant,
            LanguageConstants.Common,
            LanguageConstants.Giant)]
        [TestCase(SizeConstants.BaseRaces.Gargoyle, LanguageConstants.Common)]
        [TestCase(SizeConstants.BaseRaces.Githyanki, LanguageConstants.Special.Githyanki)]
        [TestCase(SizeConstants.BaseRaces.Githzerai, LanguageConstants.Special.Githzerai)]
        [TestCase(SizeConstants.BaseRaces.Gnoll, LanguageConstants.Gnoll)]
        [TestCase(SizeConstants.BaseRaces.Goblin,
            LanguageConstants.Common,
            LanguageConstants.Goblin)]
        [TestCase(SizeConstants.BaseRaces.GrayElf,
            LanguageConstants.Common,
            LanguageConstants.Elven)]
        [TestCase(SizeConstants.BaseRaces.GraySlaad,
            LanguageConstants.Special.Slaad,
            LanguageConstants.Common)]
        [TestCase(SizeConstants.BaseRaces.GreenSlaad,
            LanguageConstants.Special.Slaad,
            LanguageConstants.Common)]
        [TestCase(SizeConstants.BaseRaces.Grimlock,
            LanguageConstants.Special.Grimlock,
            LanguageConstants.Common)]
        [TestCase(SizeConstants.Metaraces.HalfCelestial, LanguageConstants.Celestial)]
        [TestCase(SizeConstants.Metaraces.HalfDragon, LanguageConstants.Draconic)]
        [TestCase(SizeConstants.BaseRaces.HalfElf,
            LanguageConstants.Common,
            LanguageConstants.Elven)]
        [TestCase(SizeConstants.Metaraces.HalfFiend, LanguageConstants.Infernal)]
        [TestCase(SizeConstants.BaseRaces.HalfOrc,
            LanguageConstants.Common,
            LanguageConstants.Orc)]
        [TestCase(SizeConstants.BaseRaces.Harpy, LanguageConstants.Common)]
        [TestCase(SizeConstants.BaseRaces.HighElf,
            LanguageConstants.Common,
            LanguageConstants.Elven)]
        [TestCase(SizeConstants.BaseRaces.HillDwarf,
            LanguageConstants.Common,
            LanguageConstants.Dwarven)]
        [TestCase(SizeConstants.BaseRaces.HillGiant, LanguageConstants.Giant)]
        [TestCase(SizeConstants.BaseRaces.Hobgoblin,
            LanguageConstants.Common,
            LanguageConstants.Goblin)]
        [TestCase(SizeConstants.BaseRaces.HoundArchon, LanguageConstants.Celestial)]
        [TestCase(SizeConstants.BaseRaces.Human, LanguageConstants.Common)]
        [TestCase(SizeConstants.BaseRaces.Janni, LanguageConstants.Common)]
        [TestCase(SizeConstants.BaseRaces.Kapoacinth, LanguageConstants.Common)]
        [TestCase(SizeConstants.BaseRaces.Kobold, LanguageConstants.Draconic)]
        [TestCase(SizeConstants.BaseRaces.KuoToa, LanguageConstants.Special.KuoToa)]
        [TestCase(SizeConstants.BaseRaces.LightfootHalfling,
            LanguageConstants.Common,
            LanguageConstants.Halfling)]
        [TestCase(SizeConstants.BaseRaces.Lizardfolk,
            LanguageConstants.Common,
            LanguageConstants.Draconic)]
        [TestCase(SizeConstants.BaseRaces.Locathah, LanguageConstants.Aquan)]
        [TestCase(SizeConstants.BaseRaces.Merfolk,
            LanguageConstants.Common,
            LanguageConstants.Aquan)]
        [TestCase(SizeConstants.BaseRaces.Merrow,
            LanguageConstants.Common,
            LanguageConstants.Giant)]
        [TestCase(SizeConstants.BaseRaces.MindFlayer,
            LanguageConstants.Common,
            LanguageConstants.Undercommon)]
        [TestCase(SizeConstants.BaseRaces.Minotaur,
            LanguageConstants.Common,
            LanguageConstants.Giant)]
        [TestCase(SizeConstants.BaseRaces.MountainDwarf,
            LanguageConstants.Common,
            LanguageConstants.Dwarven)]
        [TestCase(SizeConstants.BaseRaces.Ogre,
            LanguageConstants.Common,
            LanguageConstants.Giant)]
        [TestCase(SizeConstants.BaseRaces.OgreMage,
            LanguageConstants.Common,
            LanguageConstants.Giant)]
        [TestCase(SizeConstants.BaseRaces.Orc,
            LanguageConstants.Common,
            LanguageConstants.Orc)]
        [TestCase(SizeConstants.BaseRaces.Pixie,
            LanguageConstants.Common,
            LanguageConstants.Sylvan)]
        [TestCase(SizeConstants.BaseRaces.Rakshasa,
            LanguageConstants.Common,
            LanguageConstants.Infernal)]
        [TestCase(SizeConstants.BaseRaces.RedSlaad, LanguageConstants.Special.Slaad)]
        [TestCase(SizeConstants.BaseRaces.RockGnome,
            LanguageConstants.Common,
            LanguageConstants.Gnome)]
        [TestCase(SizeConstants.BaseRaces.Sahuagin,
            LanguageConstants.Common,
            LanguageConstants.Special.Sahuagin)]
        [TestCase(SizeConstants.BaseRaces.Satyr, LanguageConstants.Sylvan)]
        [TestCase(SizeConstants.BaseRaces.Scorpionfolk,
            LanguageConstants.Common,
            LanguageConstants.Terran)]
        [TestCase(SizeConstants.BaseRaces.Scrag, LanguageConstants.Giant)]
        [TestCase(SizeConstants.BaseRaces.StoneGiant, LanguageConstants.Giant)]
        [TestCase(SizeConstants.BaseRaces.StormGiant,
            LanguageConstants.Common,
            LanguageConstants.Giant)]
        [TestCase(SizeConstants.BaseRaces.Svirfneblin,
            LanguageConstants.Common,
            LanguageConstants.Gnome,
            LanguageConstants.Undercommon)]
        [TestCase(SizeConstants.BaseRaces.TallfellowHalfling,
            LanguageConstants.Common,
            LanguageConstants.Halfling)]
        [TestCase(SizeConstants.BaseRaces.Tiefling,
            LanguageConstants.Common,
            LanguageConstants.Infernal)]
        [TestCase(SizeConstants.BaseRaces.Troglodyte, LanguageConstants.Draconic)]
        [TestCase(SizeConstants.BaseRaces.Troll, LanguageConstants.Giant)]
        [TestCase(SizeConstants.BaseRaces.WildElf,
            LanguageConstants.Common,
            LanguageConstants.Elven)]
        [TestCase(SizeConstants.BaseRaces.WoodElf,
            LanguageConstants.Common,
            LanguageConstants.Elven)]
        [TestCase(SizeConstants.BaseRaces.YuanTiAbomination,
            LanguageConstants.Special.YuanTi,
            LanguageConstants.Common)]
        [TestCase(SizeConstants.BaseRaces.YuanTiHalfblood,
            LanguageConstants.Special.YuanTi,
            LanguageConstants.Common)]
        [TestCase(SizeConstants.BaseRaces.YuanTiPureblood,
            LanguageConstants.Special.YuanTi,
            LanguageConstants.Common)]
        [TestCase(SizeConstants.Metaraces.Ghost)]
        [TestCase(SizeConstants.Metaraces.Lich, LanguageConstants.Common)]
        [TestCase(SizeConstants.Metaraces.Mummy, LanguageConstants.Common)]
        [TestCase(SizeConstants.Metaraces.None)]
        [TestCase(SizeConstants.Metaraces.Vampire)]
        [TestCase(SizeConstants.Metaraces.Werebear)]
        [TestCase(SizeConstants.Metaraces.Wereboar)]
        [TestCase(SizeConstants.Metaraces.Wererat)]
        [TestCase(SizeConstants.Metaraces.Weretiger)]
        [TestCase(SizeConstants.Metaraces.Werewolf)]
        public void AutomaticLanguages(string name, params string[] languages)
        {
            base.DistinctCollection(name, languages);
        }
    }
}