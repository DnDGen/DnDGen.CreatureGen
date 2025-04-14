﻿using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Selectors.Selections;
using DnDGen.CreatureGen.Skills;
using DnDGen.CreatureGen.Tables;
using DnDGen.Infrastructure.Helpers;
using NUnit.Framework;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Integration.Tables.Skills
{
    [TestFixture]
    public class SkillDataTests : CollectionTests
    {
        protected override string tableName => TableNameConstants.Collection.SkillData;

        [Test]
        public void SkillDataNames()
        {
            var skillGroups = collectionMapper.Map(Config.Name, TableNameConstants.Collection.SkillGroups);
            //INFO: Getting the feat foci of skills includes foci for skills such as profession or craft
            var featFoci = collectionMapper.Map(Config.Name, TableNameConstants.Collection.FeatFoci);
            var otherSkills = new[]
            {
                SkillConstants.Climb + AbilityConstants.Dexterity,
                SkillConstants.Concentration + AbilityConstants.Charisma,
                SkillConstants.Craft + "2",
                SkillConstants.Craft + "3",
                SkillConstants.Craft + "4",
                SkillConstants.Craft + "5",
                SkillConstants.Jump + AbilityConstants.Dexterity,
                SkillConstants.Knowledge + "2",
                SkillConstants.Knowledge + "3",
                SkillConstants.Knowledge + "4",
                SkillConstants.Knowledge + "5",
                SkillConstants.Knowledge + GroupConstants.All,
                SkillConstants.Swim + AbilityConstants.Dexterity,
            };

            var names = skillGroups[GroupConstants.All]
                .Union(featFoci[GroupConstants.Skills])
                .Union(otherSkills);

            AssertCollectionNames(names);
        }

        [TestCase(SkillConstants.Appraise, AbilityConstants.Intelligence)]
        [TestCase(SkillConstants.Balance, AbilityConstants.Dexterity)]
        [TestCase(SkillConstants.Bluff, AbilityConstants.Charisma)]
        [TestCase(SkillConstants.Climb, AbilityConstants.Strength)]
        [TestCase(SkillConstants.Climb + AbilityConstants.Dexterity, AbilityConstants.Dexterity, SkillConstants.Climb)]
        [TestCase(SkillConstants.Concentration, AbilityConstants.Constitution)]
        [TestCase(SkillConstants.Concentration + AbilityConstants.Charisma, AbilityConstants.Charisma, SkillConstants.Concentration)]
        [TestCase(SkillConstants.Craft, AbilityConstants.Intelligence, SkillConstants.Craft, 1)]
        [TestCase(SkillConstants.Craft + "2", AbilityConstants.Intelligence, SkillConstants.Craft, 2)]
        [TestCase(SkillConstants.Craft + "3", AbilityConstants.Intelligence, SkillConstants.Craft, 3)]
        [TestCase(SkillConstants.Craft + "4", AbilityConstants.Intelligence, SkillConstants.Craft, 4)]
        [TestCase(SkillConstants.Craft + "5", AbilityConstants.Intelligence, SkillConstants.Craft, 5)]
        [TestCase(SkillConstants.DecipherScript, AbilityConstants.Intelligence)]
        [TestCase(SkillConstants.Diplomacy, AbilityConstants.Charisma)]
        [TestCase(SkillConstants.DisableDevice, AbilityConstants.Intelligence)]
        [TestCase(SkillConstants.Disguise, AbilityConstants.Charisma)]
        [TestCase(SkillConstants.EscapeArtist, AbilityConstants.Dexterity)]
        [TestCase(SkillConstants.Forgery, AbilityConstants.Intelligence)]
        [TestCase(SkillConstants.GatherInformation, AbilityConstants.Charisma)]
        [TestCase(SkillConstants.HandleAnimal, AbilityConstants.Charisma)]
        [TestCase(SkillConstants.Heal, AbilityConstants.Wisdom)]
        [TestCase(SkillConstants.Hide, AbilityConstants.Dexterity)]
        [TestCase(SkillConstants.Intimidate, AbilityConstants.Charisma)]
        [TestCase(SkillConstants.Jump, AbilityConstants.Strength)]
        [TestCase(SkillConstants.Jump + AbilityConstants.Dexterity, AbilityConstants.Dexterity, SkillConstants.Jump)]
        [TestCase(SkillConstants.Knowledge, AbilityConstants.Intelligence, SkillConstants.Knowledge, 1)]
        [TestCase(SkillConstants.Knowledge + "2", AbilityConstants.Intelligence, SkillConstants.Knowledge, 2)]
        [TestCase(SkillConstants.Knowledge + "3", AbilityConstants.Intelligence, SkillConstants.Knowledge, 3)]
        [TestCase(SkillConstants.Knowledge + "4", AbilityConstants.Intelligence, SkillConstants.Knowledge, 4)]
        [TestCase(SkillConstants.Knowledge + "5", AbilityConstants.Intelligence, SkillConstants.Knowledge, 5)]
        [TestCase(SkillConstants.Knowledge + GroupConstants.All, AbilityConstants.Intelligence, SkillConstants.Knowledge, SkillConstants.Foci.QuantityOfAll)]
        [TestCase(SkillConstants.Listen, AbilityConstants.Wisdom)]
        [TestCase(SkillConstants.MoveSilently, AbilityConstants.Dexterity)]
        [TestCase(SkillConstants.OpenLock, AbilityConstants.Dexterity)]
        [TestCase(SkillConstants.Perform, AbilityConstants.Charisma, SkillConstants.Perform, 1)]
        [TestCase(SkillConstants.Profession, AbilityConstants.Wisdom, SkillConstants.Profession, 1)]
        [TestCase(SkillConstants.Ride, AbilityConstants.Dexterity)]
        [TestCase(SkillConstants.Search, AbilityConstants.Intelligence)]
        [TestCase(SkillConstants.SenseMotive, AbilityConstants.Wisdom)]
        [TestCase(SkillConstants.SleightOfHand, AbilityConstants.Dexterity)]
        [TestCase(SkillConstants.Spellcraft, AbilityConstants.Intelligence)]
        [TestCase(SkillConstants.Spot, AbilityConstants.Wisdom)]
        [TestCase(SkillConstants.Survival, AbilityConstants.Wisdom)]
        [TestCase(SkillConstants.Swim, AbilityConstants.Strength)]
        [TestCase(SkillConstants.Swim + AbilityConstants.Dexterity, AbilityConstants.Dexterity, SkillConstants.Swim)]
        [TestCase(SkillConstants.Tumble, AbilityConstants.Dexterity)]
        [TestCase(SkillConstants.UseMagicDevice, AbilityConstants.Charisma)]
        [TestCase(SkillConstants.UseRope, AbilityConstants.Dexterity)]
        public void SkillSelectionData(string name, string baseAbility, string skillName = "", int randomFoci = 0)
        {
            if (string.IsNullOrEmpty(skillName))
                skillName = name;

            var data = DataHelper.Parse(new SkillDataSelection
            {
                SkillName = skillName,
                BaseAbilityName = baseAbility,
                RandomFociQuantity = randomFoci
            });

            AssertCollection(name, data);
        }

        [TestCase(SkillConstants.Craft, AbilityConstants.Intelligence, SkillConstants.Foci.Craft.Alchemy)]
        [TestCase(SkillConstants.Craft, AbilityConstants.Intelligence, SkillConstants.Foci.Craft.Armorsmithing)]
        [TestCase(SkillConstants.Craft, AbilityConstants.Intelligence, SkillConstants.Foci.Craft.Blacksmithing)]
        [TestCase(SkillConstants.Craft, AbilityConstants.Intelligence, SkillConstants.Foci.Craft.Bookbinding)]
        [TestCase(SkillConstants.Craft, AbilityConstants.Intelligence, SkillConstants.Foci.Craft.Bowmaking)]
        [TestCase(SkillConstants.Craft, AbilityConstants.Intelligence, SkillConstants.Foci.Craft.Brassmaking)]
        [TestCase(SkillConstants.Craft, AbilityConstants.Intelligence, SkillConstants.Foci.Craft.Brewing)]
        [TestCase(SkillConstants.Craft, AbilityConstants.Intelligence, SkillConstants.Foci.Craft.Candlemaking)]
        [TestCase(SkillConstants.Craft, AbilityConstants.Intelligence, SkillConstants.Foci.Craft.Cloth)]
        [TestCase(SkillConstants.Craft, AbilityConstants.Intelligence, SkillConstants.Foci.Craft.Coppersmithing)]
        [TestCase(SkillConstants.Craft, AbilityConstants.Intelligence, SkillConstants.Foci.Craft.Dyemaking)]
        [TestCase(SkillConstants.Craft, AbilityConstants.Intelligence, SkillConstants.Foci.Craft.Gemcutting)]
        [TestCase(SkillConstants.Craft, AbilityConstants.Intelligence, SkillConstants.Foci.Craft.Glass)]
        [TestCase(SkillConstants.Craft, AbilityConstants.Intelligence, SkillConstants.Foci.Craft.Goldsmithing)]
        [TestCase(SkillConstants.Craft, AbilityConstants.Intelligence, SkillConstants.Foci.Craft.Hatmaking)]
        [TestCase(SkillConstants.Craft, AbilityConstants.Intelligence, SkillConstants.Foci.Craft.Hornworking)]
        [TestCase(SkillConstants.Craft, AbilityConstants.Intelligence, SkillConstants.Foci.Craft.Jewelmaking)]
        [TestCase(SkillConstants.Craft, AbilityConstants.Intelligence, SkillConstants.Foci.Craft.Leather)]
        [TestCase(SkillConstants.Craft, AbilityConstants.Intelligence, SkillConstants.Foci.Craft.Locksmithing)]
        [TestCase(SkillConstants.Craft, AbilityConstants.Intelligence, SkillConstants.Foci.Craft.Mapmaking)]
        [TestCase(SkillConstants.Craft, AbilityConstants.Intelligence, SkillConstants.Foci.Craft.Milling)]
        [TestCase(SkillConstants.Craft, AbilityConstants.Intelligence, SkillConstants.Foci.Craft.Painting)]
        [TestCase(SkillConstants.Craft, AbilityConstants.Intelligence, SkillConstants.Foci.Craft.Parchmentmaking)]
        [TestCase(SkillConstants.Craft, AbilityConstants.Intelligence, SkillConstants.Foci.Craft.Pewtermaking)]
        [TestCase(SkillConstants.Craft, AbilityConstants.Intelligence, SkillConstants.Foci.Craft.Potterymaking)]
        [TestCase(SkillConstants.Craft, AbilityConstants.Intelligence, SkillConstants.Foci.Craft.Sculpting)]
        [TestCase(SkillConstants.Craft, AbilityConstants.Intelligence, SkillConstants.Foci.Craft.Shipmaking)]
        [TestCase(SkillConstants.Craft, AbilityConstants.Intelligence, SkillConstants.Foci.Craft.Shoemaking)]
        [TestCase(SkillConstants.Craft, AbilityConstants.Intelligence, SkillConstants.Foci.Craft.Silversmithing)]
        [TestCase(SkillConstants.Craft, AbilityConstants.Intelligence, SkillConstants.Foci.Craft.Skinning)]
        [TestCase(SkillConstants.Craft, AbilityConstants.Intelligence, SkillConstants.Foci.Craft.Soapmaking)]
        [TestCase(SkillConstants.Craft, AbilityConstants.Intelligence, SkillConstants.Foci.Craft.Stonemasonry)]
        [TestCase(SkillConstants.Craft, AbilityConstants.Intelligence, SkillConstants.Foci.Craft.Tanning)]
        [TestCase(SkillConstants.Craft, AbilityConstants.Intelligence, SkillConstants.Foci.Craft.Trapmaking)]
        [TestCase(SkillConstants.Craft, AbilityConstants.Intelligence, SkillConstants.Foci.Craft.Weaponsmithing)]
        [TestCase(SkillConstants.Craft, AbilityConstants.Intelligence, SkillConstants.Foci.Craft.Weaving)]
        [TestCase(SkillConstants.Craft, AbilityConstants.Intelligence, SkillConstants.Foci.Craft.Wheelmaking)]
        [TestCase(SkillConstants.Craft, AbilityConstants.Intelligence, SkillConstants.Foci.Craft.Winemaking)]
        [TestCase(SkillConstants.Craft, AbilityConstants.Intelligence, SkillConstants.Foci.Craft.Woodworking)]
        [TestCase(SkillConstants.Knowledge, AbilityConstants.Intelligence, SkillConstants.Foci.Knowledge.Arcana)]
        [TestCase(SkillConstants.Knowledge, AbilityConstants.Intelligence, SkillConstants.Foci.Knowledge.ArchitectureAndEngineering)]
        [TestCase(SkillConstants.Knowledge, AbilityConstants.Intelligence, SkillConstants.Foci.Knowledge.Dungeoneering)]
        [TestCase(SkillConstants.Knowledge, AbilityConstants.Intelligence, SkillConstants.Foci.Knowledge.Geography)]
        [TestCase(SkillConstants.Knowledge, AbilityConstants.Intelligence, SkillConstants.Foci.Knowledge.History)]
        [TestCase(SkillConstants.Knowledge, AbilityConstants.Intelligence, SkillConstants.Foci.Knowledge.Local)]
        [TestCase(SkillConstants.Knowledge, AbilityConstants.Intelligence, SkillConstants.Foci.Knowledge.Nature)]
        [TestCase(SkillConstants.Knowledge, AbilityConstants.Intelligence, SkillConstants.Foci.Knowledge.NobilityAndRoyalty)]
        [TestCase(SkillConstants.Knowledge, AbilityConstants.Intelligence, SkillConstants.Foci.Knowledge.Religion)]
        [TestCase(SkillConstants.Knowledge, AbilityConstants.Intelligence, SkillConstants.Foci.Knowledge.ThePlanes)]
        [TestCase(SkillConstants.Perform, AbilityConstants.Charisma, SkillConstants.Foci.Perform.Act)]
        [TestCase(SkillConstants.Perform, AbilityConstants.Charisma, SkillConstants.Foci.Perform.Comedy)]
        [TestCase(SkillConstants.Perform, AbilityConstants.Charisma, SkillConstants.Foci.Perform.Dance)]
        [TestCase(SkillConstants.Perform, AbilityConstants.Charisma, SkillConstants.Foci.Perform.KeyboardInstruments)]
        [TestCase(SkillConstants.Perform, AbilityConstants.Charisma, SkillConstants.Foci.Perform.Oratory)]
        [TestCase(SkillConstants.Perform, AbilityConstants.Charisma, SkillConstants.Foci.Perform.PercussionInstruments)]
        [TestCase(SkillConstants.Perform, AbilityConstants.Charisma, SkillConstants.Foci.Perform.Sing)]
        [TestCase(SkillConstants.Perform, AbilityConstants.Charisma, SkillConstants.Foci.Perform.StringInstruments)]
        [TestCase(SkillConstants.Perform, AbilityConstants.Charisma, SkillConstants.Foci.Perform.WindInstruments)]
        [TestCase(SkillConstants.Profession, AbilityConstants.Wisdom, SkillConstants.Foci.Profession.Adviser)]
        [TestCase(SkillConstants.Profession, AbilityConstants.Wisdom, SkillConstants.Foci.Profession.Alchemist)]
        [TestCase(SkillConstants.Profession, AbilityConstants.Wisdom, SkillConstants.Foci.Profession.AnimalGroomer)]
        [TestCase(SkillConstants.Profession, AbilityConstants.Wisdom, SkillConstants.Foci.Profession.AnimalTrainer)]
        [TestCase(SkillConstants.Profession, AbilityConstants.Wisdom, SkillConstants.Foci.Profession.Apothecary)]
        [TestCase(SkillConstants.Profession, AbilityConstants.Wisdom, SkillConstants.Foci.Profession.Appraiser)]
        [TestCase(SkillConstants.Profession, AbilityConstants.Wisdom, SkillConstants.Foci.Profession.Architect)]
        [TestCase(SkillConstants.Profession, AbilityConstants.Wisdom, SkillConstants.Foci.Profession.Armorer)]
        [TestCase(SkillConstants.Profession, AbilityConstants.Wisdom, SkillConstants.Foci.Profession.Barrister)]
        [TestCase(SkillConstants.Profession, AbilityConstants.Wisdom, SkillConstants.Foci.Profession.Blacksmith)]
        [TestCase(SkillConstants.Profession, AbilityConstants.Wisdom, SkillConstants.Foci.Profession.Bookbinder)]
        [TestCase(SkillConstants.Profession, AbilityConstants.Wisdom, SkillConstants.Foci.Profession.Bowyer)]
        [TestCase(SkillConstants.Profession, AbilityConstants.Wisdom, SkillConstants.Foci.Profession.Brazier)]
        [TestCase(SkillConstants.Profession, AbilityConstants.Wisdom, SkillConstants.Foci.Profession.Brewer)]
        [TestCase(SkillConstants.Profession, AbilityConstants.Wisdom, SkillConstants.Foci.Profession.Butler)]
        [TestCase(SkillConstants.Profession, AbilityConstants.Wisdom, SkillConstants.Foci.Profession.Carpenter)]
        [TestCase(SkillConstants.Profession, AbilityConstants.Wisdom, SkillConstants.Foci.Profession.Cartographer)]
        [TestCase(SkillConstants.Profession, AbilityConstants.Wisdom, SkillConstants.Foci.Profession.Cartwright)]
        [TestCase(SkillConstants.Profession, AbilityConstants.Wisdom, SkillConstants.Foci.Profession.Chandler)]
        [TestCase(SkillConstants.Profession, AbilityConstants.Wisdom, SkillConstants.Foci.Profession.CityGuide)]
        [TestCase(SkillConstants.Profession, AbilityConstants.Wisdom, SkillConstants.Foci.Profession.Clerk)]
        [TestCase(SkillConstants.Profession, AbilityConstants.Wisdom, SkillConstants.Foci.Profession.Cobbler)]
        [TestCase(SkillConstants.Profession, AbilityConstants.Wisdom, SkillConstants.Foci.Profession.Coffinmaker)]
        [TestCase(SkillConstants.Profession, AbilityConstants.Wisdom, SkillConstants.Foci.Profession.Coiffeur)]
        [TestCase(SkillConstants.Profession, AbilityConstants.Wisdom, SkillConstants.Foci.Profession.Cook)]
        [TestCase(SkillConstants.Profession, AbilityConstants.Wisdom, SkillConstants.Foci.Profession.Coppersmith)]
        [TestCase(SkillConstants.Profession, AbilityConstants.Wisdom, SkillConstants.Foci.Profession.Craftsman)]
        [TestCase(SkillConstants.Profession, AbilityConstants.Wisdom, SkillConstants.Foci.Profession.Dowser)]
        [TestCase(SkillConstants.Profession, AbilityConstants.Wisdom, SkillConstants.Foci.Profession.Dyer)]
        [TestCase(SkillConstants.Profession, AbilityConstants.Wisdom, SkillConstants.Foci.Profession.Embalmer)]
        [TestCase(SkillConstants.Profession, AbilityConstants.Wisdom, SkillConstants.Foci.Profession.Engineer)]
        [TestCase(SkillConstants.Profession, AbilityConstants.Wisdom, SkillConstants.Foci.Profession.Entertainer)]
        [TestCase(SkillConstants.Profession, AbilityConstants.Wisdom, SkillConstants.Foci.Profession.ExoticAnimalTrainer)]
        [TestCase(SkillConstants.Profession, AbilityConstants.Wisdom, SkillConstants.Foci.Profession.Farmer)]
        [TestCase(SkillConstants.Profession, AbilityConstants.Wisdom, SkillConstants.Foci.Profession.Fletcher)]
        [TestCase(SkillConstants.Profession, AbilityConstants.Wisdom, SkillConstants.Foci.Profession.Footman)]
        [TestCase(SkillConstants.Profession, AbilityConstants.Wisdom, SkillConstants.Foci.Profession.Gemcutter)]
        [TestCase(SkillConstants.Profession, AbilityConstants.Wisdom, SkillConstants.Foci.Profession.Goldsmith)]
        [TestCase(SkillConstants.Profession, AbilityConstants.Wisdom, SkillConstants.Foci.Profession.Governess)]
        [TestCase(SkillConstants.Profession, AbilityConstants.Wisdom, SkillConstants.Foci.Profession.Haberdasher)]
        [TestCase(SkillConstants.Profession, AbilityConstants.Wisdom, SkillConstants.Foci.Profession.Healer)]
        [TestCase(SkillConstants.Profession, AbilityConstants.Wisdom, SkillConstants.Foci.Profession.Horner)]
        [TestCase(SkillConstants.Profession, AbilityConstants.Wisdom, SkillConstants.Foci.Profession.Hunter)]
        [TestCase(SkillConstants.Profession, AbilityConstants.Wisdom, SkillConstants.Foci.Profession.Interpreter)]
        [TestCase(SkillConstants.Profession, AbilityConstants.Wisdom, SkillConstants.Foci.Profession.Jeweler)]
        [TestCase(SkillConstants.Profession, AbilityConstants.Wisdom, SkillConstants.Foci.Profession.Laborer)]
        [TestCase(SkillConstants.Profession, AbilityConstants.Wisdom, SkillConstants.Foci.Profession.Launderer)]
        [TestCase(SkillConstants.Profession, AbilityConstants.Wisdom, SkillConstants.Foci.Profession.Limner)]
        [TestCase(SkillConstants.Profession, AbilityConstants.Wisdom, SkillConstants.Foci.Profession.LocalCourier)]
        [TestCase(SkillConstants.Profession, AbilityConstants.Wisdom, SkillConstants.Foci.Profession.Locksmith)]
        [TestCase(SkillConstants.Profession, AbilityConstants.Wisdom, SkillConstants.Foci.Profession.Maid)]
        [TestCase(SkillConstants.Profession, AbilityConstants.Wisdom, SkillConstants.Foci.Profession.Masseuse)]
        [TestCase(SkillConstants.Profession, AbilityConstants.Wisdom, SkillConstants.Foci.Profession.Matchmaker)]
        [TestCase(SkillConstants.Profession, AbilityConstants.Wisdom, SkillConstants.Foci.Profession.Midwife)]
        [TestCase(SkillConstants.Profession, AbilityConstants.Wisdom, SkillConstants.Foci.Profession.Miller)]
        [TestCase(SkillConstants.Profession, AbilityConstants.Wisdom, SkillConstants.Foci.Profession.Miner)]
        [TestCase(SkillConstants.Profession, AbilityConstants.Wisdom, SkillConstants.Foci.Profession.Navigator)]
        [TestCase(SkillConstants.Profession, AbilityConstants.Wisdom, SkillConstants.Foci.Profession.Nursemaid)]
        [TestCase(SkillConstants.Profession, AbilityConstants.Wisdom, SkillConstants.Foci.Profession.OutOfTownCourier)]
        [TestCase(SkillConstants.Profession, AbilityConstants.Wisdom, SkillConstants.Foci.Profession.Painter)]
        [TestCase(SkillConstants.Profession, AbilityConstants.Wisdom, SkillConstants.Foci.Profession.Parchmentmaker)]
        [TestCase(SkillConstants.Profession, AbilityConstants.Wisdom, SkillConstants.Foci.Profession.Pewterer)]
        [TestCase(SkillConstants.Profession, AbilityConstants.Wisdom, SkillConstants.Foci.Profession.Polisher)]
        [TestCase(SkillConstants.Profession, AbilityConstants.Wisdom, SkillConstants.Foci.Profession.Porter)]
        [TestCase(SkillConstants.Profession, AbilityConstants.Wisdom, SkillConstants.Foci.Profession.Potter)]
        [TestCase(SkillConstants.Profession, AbilityConstants.Wisdom, SkillConstants.Foci.Profession.Sage)]
        [TestCase(SkillConstants.Profession, AbilityConstants.Wisdom, SkillConstants.Foci.Profession.SailorCrewmember)]
        [TestCase(SkillConstants.Profession, AbilityConstants.Wisdom, SkillConstants.Foci.Profession.SailorMate)]
        [TestCase(SkillConstants.Profession, AbilityConstants.Wisdom, SkillConstants.Foci.Profession.Scribe)]
        [TestCase(SkillConstants.Profession, AbilityConstants.Wisdom, SkillConstants.Foci.Profession.Sculptor)]
        [TestCase(SkillConstants.Profession, AbilityConstants.Wisdom, SkillConstants.Foci.Profession.Shepherd)]
        [TestCase(SkillConstants.Profession, AbilityConstants.Wisdom, SkillConstants.Foci.Profession.Shipwright)]
        [TestCase(SkillConstants.Profession, AbilityConstants.Wisdom, SkillConstants.Foci.Profession.Silversmith)]
        [TestCase(SkillConstants.Profession, AbilityConstants.Wisdom, SkillConstants.Foci.Profession.Skinner)]
        [TestCase(SkillConstants.Profession, AbilityConstants.Wisdom, SkillConstants.Foci.Profession.Soapmaker)]
        [TestCase(SkillConstants.Profession, AbilityConstants.Wisdom, SkillConstants.Foci.Profession.Soothsayer)]
        [TestCase(SkillConstants.Profession, AbilityConstants.Wisdom, SkillConstants.Foci.Profession.Tanner)]
        [TestCase(SkillConstants.Profession, AbilityConstants.Wisdom, SkillConstants.Foci.Profession.Teacher)]
        [TestCase(SkillConstants.Profession, AbilityConstants.Wisdom, SkillConstants.Foci.Profession.Teamster)]
        [TestCase(SkillConstants.Profession, AbilityConstants.Wisdom, SkillConstants.Foci.Profession.Trader)]
        [TestCase(SkillConstants.Profession, AbilityConstants.Wisdom, SkillConstants.Foci.Profession.Trapper)]
        [TestCase(SkillConstants.Profession, AbilityConstants.Wisdom, SkillConstants.Foci.Profession.Valet)]
        [TestCase(SkillConstants.Profession, AbilityConstants.Wisdom, SkillConstants.Foci.Profession.Vintner)]
        [TestCase(SkillConstants.Profession, AbilityConstants.Wisdom, SkillConstants.Foci.Profession.Weaponsmith)]
        [TestCase(SkillConstants.Profession, AbilityConstants.Wisdom, SkillConstants.Foci.Profession.Weaver)]
        [TestCase(SkillConstants.Profession, AbilityConstants.Wisdom, SkillConstants.Foci.Profession.Wheelwright)]
        [TestCase(SkillConstants.Profession, AbilityConstants.Wisdom, SkillConstants.Foci.Profession.WildernessGuide)]
        public void SkillSelectionData(string skillName, string baseAbility, string focus)
        {
            var name = SkillConstants.Build(skillName, focus);

            var data = DataHelper.Parse(new SkillDataSelection
            {
                SkillName = skillName,
                BaseAbilityName = baseAbility,
                Focus = focus
            });

            AssertCollection(name, data);
        }
    }
}