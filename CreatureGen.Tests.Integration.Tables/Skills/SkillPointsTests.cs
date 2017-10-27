using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Tables;
using CreatureGen.Creatures;
using NUnit.Framework;
using System.Linq;

namespace CreatureGen.Tests.Integration.Tables.Skills
{
    [TestFixture]
    public class SkillPointsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get { return TableNameConstants.Set.Adjustments.SkillPoints; }
        }

        [Test]
        public override void CollectionNames()
        {
            var classGroups = CollectionsMapper.Map(TableNameConstants.Set.Collection.ClassNameGroups);
            var baseRaceGroups = CollectionsMapper.Map(TableNameConstants.Set.Collection.BaseRaceGroups);

            var names = classGroups[GroupConstants.All].Union(baseRaceGroups[GroupConstants.Monsters]);

            AssertCollectionNames(names);
        }

        [TestCase(CharacterClassConstants.Adept, 2)]
        [TestCase(CharacterClassConstants.Aristocrat, 4)]
        [TestCase(CharacterClassConstants.Barbarian, 4)]
        [TestCase(CharacterClassConstants.Bard, 6)]
        [TestCase(CharacterClassConstants.Cleric, 2)]
        [TestCase(CharacterClassConstants.Commoner, 2)]
        [TestCase(CharacterClassConstants.Druid, 4)]
        [TestCase(CharacterClassConstants.Expert, 6)]
        [TestCase(CharacterClassConstants.Fighter, 2)]
        [TestCase(CharacterClassConstants.Monk, 4)]
        [TestCase(CharacterClassConstants.Paladin, 2)]
        [TestCase(CharacterClassConstants.Ranger, 6)]
        [TestCase(CharacterClassConstants.Rogue, 8)]
        [TestCase(CharacterClassConstants.Sorcerer, 2)]
        [TestCase(CharacterClassConstants.Warrior, 2)]
        [TestCase(CharacterClassConstants.Wizard, 2)]
        [TestCase(SizeConstants.BaseRaces.AquaticElf, 0)]
        [TestCase(SizeConstants.BaseRaces.Azer, 8)]
        [TestCase(SizeConstants.BaseRaces.BlueSlaad, 2)]
        [TestCase(SizeConstants.BaseRaces.Bugbear, 2)]
        [TestCase(SizeConstants.BaseRaces.Centaur, 2)]
        [TestCase(SizeConstants.BaseRaces.CloudGiant, 2)]
        [TestCase(SizeConstants.BaseRaces.DeathSlaad, 2)]
        [TestCase(SizeConstants.BaseRaces.Derro, 2)]
        [TestCase(SizeConstants.BaseRaces.Doppelganger, 2)]
        [TestCase(SizeConstants.BaseRaces.FireGiant, 2)]
        [TestCase(SizeConstants.BaseRaces.FrostGiant, 2)]
        [TestCase(SizeConstants.BaseRaces.Gargoyle, 2)]
        [TestCase(SizeConstants.BaseRaces.Githyanki, 0)]
        [TestCase(SizeConstants.BaseRaces.Githzerai, 0)]
        [TestCase(SizeConstants.BaseRaces.Gnoll, 2)]
        [TestCase(SizeConstants.BaseRaces.Goblin, 0)]
        [TestCase(SizeConstants.BaseRaces.GraySlaad, 2)]
        [TestCase(SizeConstants.BaseRaces.GreenSlaad, 2)]
        [TestCase(SizeConstants.BaseRaces.Grimlock, 2)]
        [TestCase(SizeConstants.BaseRaces.Harpy, 2)]
        [TestCase(SizeConstants.BaseRaces.HillGiant, 2)]
        [TestCase(SizeConstants.BaseRaces.Hobgoblin, 0)]
        [TestCase(SizeConstants.BaseRaces.HoundArchon, 8)]
        [TestCase(SizeConstants.BaseRaces.Janni, 8)]
        [TestCase(SizeConstants.BaseRaces.Kapoacinth, 2)]
        [TestCase(SizeConstants.BaseRaces.Kobold, 0)]
        [TestCase(SizeConstants.BaseRaces.KuoToa, 2)]
        [TestCase(SizeConstants.BaseRaces.Lizardfolk, 2)]
        [TestCase(SizeConstants.BaseRaces.Locathah, 0)]
        [TestCase(SizeConstants.BaseRaces.Merfolk, 0)]
        [TestCase(SizeConstants.BaseRaces.Merrow, 2)]
        [TestCase(SizeConstants.BaseRaces.MindFlayer, 2)]
        [TestCase(SizeConstants.BaseRaces.Minotaur, 2)]
        [TestCase(SizeConstants.BaseRaces.Ogre, 2)]
        [TestCase(SizeConstants.BaseRaces.OgreMage, 2)]
        [TestCase(SizeConstants.BaseRaces.Orc, 0)]
        [TestCase(SizeConstants.BaseRaces.Pixie, 2)]
        [TestCase(SizeConstants.BaseRaces.Rakshasa, 8)]
        [TestCase(SizeConstants.BaseRaces.RedSlaad, 2)]
        [TestCase(SizeConstants.BaseRaces.Sahuagin, 2)]
        [TestCase(SizeConstants.BaseRaces.Satyr, 6)]
        [TestCase(SizeConstants.BaseRaces.Scorpionfolk, 2)]
        [TestCase(SizeConstants.BaseRaces.Scrag, 2)]
        [TestCase(SizeConstants.BaseRaces.StoneGiant, 2)]
        [TestCase(SizeConstants.BaseRaces.StormGiant, 2)]
        [TestCase(SizeConstants.BaseRaces.Troglodyte, 2)]
        [TestCase(SizeConstants.BaseRaces.Troll, 2)]
        [TestCase(SizeConstants.BaseRaces.YuanTiAbomination, 2)]
        [TestCase(SizeConstants.BaseRaces.YuanTiHalfblood, 2)]
        [TestCase(SizeConstants.BaseRaces.YuanTiPureblood, 2)]
        public void SkillPoints(string name, int points)
        {
            base.Adjustment(name, points);
        }
    }
}