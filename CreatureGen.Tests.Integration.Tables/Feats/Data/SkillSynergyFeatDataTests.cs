using CreatureGen.Feats;
using CreatureGen.Skills;
using CreatureGen.Tables;
using NUnit.Framework;
using System.Collections.Generic;

namespace CreatureGen.Tests.Integration.Tables.Feats.Data
{
    [TestFixture]
    public class SkillSynergyFeatDataTests : DataTests
    {
        protected override string tableName
        {
            get { return TableNameConstants.Collection.SkillSynergyFeatData; }
        }

        protected override void PopulateIndices(IEnumerable<string> collection)
        {
            indices[DataIndexConstants.SkillSynergyFeatData.FeatNameIndex] = "Feat Name";
            indices[DataIndexConstants.SkillSynergyFeatData.FocusTypeIndex] = "Focus Type";
            indices[DataIndexConstants.SkillSynergyFeatData.PowerIndex] = "Power";
        }

        [Test]
        public void CollectionNames()
        {
            var names = new[]
            {
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Bluff, null, SkillConstants.Diplomacy, null),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Bluff, null, SkillConstants.Disguise, null),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Bluff, null, SkillConstants.Intimidate, null),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Bluff, null, SkillConstants.SleightOfHand, null),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Craft, null, SkillConstants.Appraise, null),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.DecipherScript, null, SkillConstants.UseMagicDevice, null),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.EscapeArtist, null, SkillConstants.UseRope, null),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.HandleAnimal, null, SkillConstants.Diplomacy, null),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.HandleAnimal, null, SkillConstants.Ride, null),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Jump, null, SkillConstants.Tumble, null),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Arcana, SkillConstants.Spellcraft, null),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.ArchitectureAndEngineering, SkillConstants.Search, null),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Dungeoneering, SkillConstants.Survival, null),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Geography, SkillConstants.Survival, null),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.History, SkillConstants.Knowledge, "bardic"),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Local, SkillConstants.GatherInformation, null),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Nature, SkillConstants.Survival, null),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.NobilityAndRoyalty, SkillConstants.Diplomacy, null),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.ThePlanes, SkillConstants.Survival, null),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Search, null, SkillConstants.Survival, null),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.SenseMotive, null, SkillConstants.Diplomacy, null),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Spellcraft, null, SkillConstants.UseMagicDevice, null),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Survival, null, SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Nature),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Tumble, null, SkillConstants.Balance, null),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Tumble, null, SkillConstants.Jump, null),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.UseMagicDevice, null, SkillConstants.Spellcraft, null),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.UseRope, null, SkillConstants.Climb, null),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.UseRope, null, SkillConstants.EscapeArtist, null),
            };

            AssertCollectionNames(names);
        }

        [TestCase(SkillConstants.Appraise, null, SkillConstants.Profession, SkillConstants.Foci.Profession.Appraiser, null)]
        [TestCase(SkillConstants.Appraise, null, SkillConstants.Profession, SkillConstants.Foci.Profession.Trader, null)]
        [TestCase(SkillConstants.Bluff, null, SkillConstants.Diplomacy, null, null)]
        [TestCase(SkillConstants.Bluff, null, SkillConstants.Disguise, null, "acting")]
        [TestCase(SkillConstants.Bluff, null, SkillConstants.Intimidate, null, null)]
        [TestCase(SkillConstants.Bluff, null, SkillConstants.Profession, SkillConstants.Foci.Profession.Dowser, null)]
        [TestCase(SkillConstants.Bluff, null, SkillConstants.Profession, SkillConstants.Foci.Profession.Soothsayer, null)]
        [TestCase(SkillConstants.Bluff, null, SkillConstants.SleightOfHand, null, null)]
        [TestCase(SkillConstants.Craft, null, SkillConstants.Appraise, null, "related to items made with your Craft skill")]
        [TestCase(SkillConstants.Craft, SkillConstants.Foci.Craft.Alchemy, SkillConstants.Profession, SkillConstants.Foci.Profession.Alchemist, null)]
        [TestCase(SkillConstants.Craft, SkillConstants.Foci.Craft.Alchemy, SkillConstants.Profession, SkillConstants.Foci.Profession.Craftsman, null)]
        [TestCase(SkillConstants.Craft, SkillConstants.Foci.Craft.Alchemy, SkillConstants.Profession, SkillConstants.Foci.Profession.Embalmer, null)]
        [TestCase(SkillConstants.Craft, SkillConstants.Foci.Craft.Armorsmithing, SkillConstants.Profession, SkillConstants.Foci.Profession.Armorer, null)]
        [TestCase(SkillConstants.Craft, SkillConstants.Foci.Craft.Armorsmithing, SkillConstants.Profession, SkillConstants.Foci.Profession.Craftsman, null)]
        [TestCase(SkillConstants.Craft, SkillConstants.Foci.Craft.Blacksmithing, SkillConstants.Profession, SkillConstants.Foci.Profession.Blacksmith, null)]
        [TestCase(SkillConstants.Craft, SkillConstants.Foci.Craft.Blacksmithing, SkillConstants.Profession, SkillConstants.Foci.Profession.Craftsman, null)]
        [TestCase(SkillConstants.Craft, SkillConstants.Foci.Craft.Bookbinding, SkillConstants.Profession, SkillConstants.Foci.Profession.Bookbinder, null)]
        [TestCase(SkillConstants.Craft, SkillConstants.Foci.Craft.Bookbinding, SkillConstants.Profession, SkillConstants.Foci.Profession.Craftsman, null)]
        [TestCase(SkillConstants.Craft, SkillConstants.Foci.Craft.Bowmaking, SkillConstants.Profession, SkillConstants.Foci.Profession.Bowyer, null)]
        [TestCase(SkillConstants.Craft, SkillConstants.Foci.Craft.Bowmaking, SkillConstants.Profession, SkillConstants.Foci.Profession.Craftsman, null)]
        [TestCase(SkillConstants.Craft, SkillConstants.Foci.Craft.Bowmaking, SkillConstants.Profession, SkillConstants.Foci.Profession.Fletcher, null)]
        [TestCase(SkillConstants.Craft, SkillConstants.Foci.Craft.Brassmaking, SkillConstants.Profession, SkillConstants.Foci.Profession.Brazier, null)]
        [TestCase(SkillConstants.Craft, SkillConstants.Foci.Craft.Brassmaking, SkillConstants.Profession, SkillConstants.Foci.Profession.Craftsman, null)]
        [TestCase(SkillConstants.Craft, SkillConstants.Foci.Craft.Brewing, SkillConstants.Profession, SkillConstants.Foci.Profession.Brewer, null)]
        [TestCase(SkillConstants.Craft, SkillConstants.Foci.Craft.Brewing, SkillConstants.Profession, SkillConstants.Foci.Profession.Craftsman, null)]
        [TestCase(SkillConstants.Craft, SkillConstants.Foci.Craft.Candlemaking, SkillConstants.Profession, SkillConstants.Foci.Profession.Chandler, null)]
        [TestCase(SkillConstants.Craft, SkillConstants.Foci.Craft.Candlemaking, SkillConstants.Profession, SkillConstants.Foci.Profession.Craftsman, null)]
        [TestCase(SkillConstants.DecipherScript, null, SkillConstants.UseMagicDevice, null, "with scrolls")]
        [TestCase(SkillConstants.EscapeArtist, null, SkillConstants.UseRope, null, "binding someone")]
        [TestCase(SkillConstants.HandleAnimal, null, SkillConstants.Diplomacy, null, "wild empathy")]
        [TestCase(SkillConstants.HandleAnimal, null, SkillConstants.Ride, null, null)]
        [TestCase(SkillConstants.Jump, null, SkillConstants.Tumble, null, null)]
        [TestCase(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Arcana, SkillConstants.Spellcraft, null, null)]
        [TestCase(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.ArchitectureAndEngineering, SkillConstants.Search, null, "find secret doors or compartments")]
        [TestCase(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Dungeoneering, SkillConstants.Survival, null, "underground")]
        [TestCase(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Geography, SkillConstants.Survival, null, "keep from getting lost or avoid natural hazards")]
        [TestCase(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.History, SkillConstants.Knowledge, "bardic", null)]
        [TestCase(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Local, SkillConstants.GatherInformation, null, null)]
        [TestCase(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Nature, SkillConstants.Survival, null, "in aboveground natural environments")]
        [TestCase(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.NobilityAndRoyalty, SkillConstants.Diplomacy, null, null)]
        [TestCase(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.ThePlanes, SkillConstants.Survival, null, "on other planes")]
        [TestCase(SkillConstants.Search, null, SkillConstants.Survival, null, "following tracks")]
        [TestCase(SkillConstants.SenseMotive, null, SkillConstants.Diplomacy, null, null)]
        [TestCase(SkillConstants.Spellcraft, null, SkillConstants.UseMagicDevice, null, "with scrolls")]
        [TestCase(SkillConstants.Survival, null, SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Nature, null)]
        [TestCase(SkillConstants.Tumble, null, SkillConstants.Balance, null, null)]
        [TestCase(SkillConstants.Tumble, null, SkillConstants.Jump, null, null)]
        [TestCase(SkillConstants.UseMagicDevice, null, SkillConstants.Spellcraft, null, "decipher scrolls")]
        [TestCase(SkillConstants.UseRope, null, SkillConstants.Climb, null, "with rope")]
        [TestCase(SkillConstants.UseRope, null, SkillConstants.EscapeArtist, null, "escaping rope bonds")]
        public void SkillSynergyFeatData(string sourceSkill, string sourceFocus, string targetSkillName, string targetSkillFocus, string condition)
        {
            var focus = targetSkillName;

            if (!string.IsNullOrEmpty(targetSkillFocus))
                focus += $"/{targetSkillFocus}";

            if (!string.IsNullOrEmpty(condition))
                focus += $": {condition}";

            var data = DataIndexConstants.SkillSynergyFeatData.InitializeData();

            data[DataIndexConstants.SkillSynergyFeatData.FeatNameIndex] = FeatConstants.SpecialQualities.SkillBonus;
            data[DataIndexConstants.SkillSynergyFeatData.FocusTypeIndex] = focus;
            data[DataIndexConstants.SkillSynergyFeatData.PowerIndex] = 2.ToString();

            var key = DataIndexConstants.SkillSynergyFeatData.BuildDataKey(sourceSkill, sourceFocus, targetSkillName, targetSkillFocus);

            Data(key, data);
        }

        [TestCase(SkillConstants.Craft, SkillConstants.Foci.Craft.Cloth,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Craftsman)]
        [TestCase(SkillConstants.Craft, SkillConstants.Foci.Craft.Coppersmithing,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Coppersmith,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Craftsman)]
        [TestCase(SkillConstants.Craft, SkillConstants.Foci.Craft.Dyemaking,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Craftsman,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Dyer)]
        [TestCase(SkillConstants.Craft, SkillConstants.Foci.Craft.Gemcutting,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Craftsman,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Gemcutter)]
        [TestCase(SkillConstants.Craft, SkillConstants.Foci.Craft.Glass,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Craftsman)]
        [TestCase(SkillConstants.Craft, SkillConstants.Foci.Craft.Goldsmithing,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Craftsman,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Goldsmith)]
        [TestCase(SkillConstants.Craft, SkillConstants.Foci.Craft.Hatmaking,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Craftsman,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Haberdasher)]
        [TestCase(SkillConstants.Craft, SkillConstants.Foci.Craft.Hornworking,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Craftsman,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Horner)]
        [TestCase(SkillConstants.Craft, SkillConstants.Foci.Craft.Jewelmaking,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Craftsman,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Jeweler)]
        [TestCase(SkillConstants.Craft, SkillConstants.Foci.Craft.Leather,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Craftsman)]
        [TestCase(SkillConstants.Craft, SkillConstants.Foci.Craft.Locksmithing,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Craftsman,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Locksmith)]
        [TestCase(SkillConstants.Craft, SkillConstants.Foci.Craft.Mapmaking,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Cartographer,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Craftsman)]
        [TestCase(SkillConstants.Craft, SkillConstants.Foci.Craft.Milling,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Craftsman,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Miller)]
        [TestCase(SkillConstants.Craft, SkillConstants.Foci.Craft.Painting,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Craftsman,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Limner,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Painter)]
        [TestCase(SkillConstants.Craft, SkillConstants.Foci.Craft.Parchmentmaking,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Craftsman,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Parchmentmaker)]
        [TestCase(SkillConstants.Craft, SkillConstants.Foci.Craft.Pewtermaking,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Craftsman,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Pewterer)]
        [TestCase(SkillConstants.Craft, SkillConstants.Foci.Craft.Potterymaking,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Craftsman,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Potter)]
        [TestCase(SkillConstants.Craft, SkillConstants.Foci.Craft.Sculpting,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Craftsman,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Sculptor)]
        [TestCase(SkillConstants.Craft, SkillConstants.Foci.Craft.Shipmaking,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Craftsman,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Shipwright)]
        [TestCase(SkillConstants.Craft, SkillConstants.Foci.Craft.Shoemaking,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Cobbler,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Craftsman)]
        [TestCase(SkillConstants.Craft, SkillConstants.Foci.Craft.Silversmithing,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Craftsman,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Silversmith)]
        [TestCase(SkillConstants.Craft, SkillConstants.Foci.Craft.Skinning,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Craftsman,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Skinner)]
        [TestCase(SkillConstants.Craft, SkillConstants.Foci.Craft.Soapmaking,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Craftsman,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Soapmaker)]
        [TestCase(SkillConstants.Craft, SkillConstants.Foci.Craft.Stonemasonry,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Craftsman)]
        [TestCase(SkillConstants.Craft, SkillConstants.Foci.Craft.Tanning,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Craftsman,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Tanner)]
        [TestCase(SkillConstants.Craft, SkillConstants.Foci.Craft.Trapmaking,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Craftsman)]
        [TestCase(SkillConstants.Craft, SkillConstants.Foci.Craft.Weaponsmithing,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Craftsman,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Weaponsmith)]
        [TestCase(SkillConstants.Craft, SkillConstants.Foci.Craft.Weaving,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Craftsman,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Weaver)]
        [TestCase(SkillConstants.Craft, SkillConstants.Foci.Craft.Wheelmaking,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Craftsman,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Wheelwright)]
        [TestCase(SkillConstants.Craft, SkillConstants.Foci.Craft.Winemaking,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Craftsman,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Vintner)]
        [TestCase(SkillConstants.Craft, SkillConstants.Foci.Craft.Woodworking,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Carpenter,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Cartwright,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Coffinmaker,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Craftsman)]
        [TestCase(SkillConstants.Diplomacy,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Barrister,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Butler,
            SkillConstants.Profession, SkillConstants.Foci.Profession.CityGuide,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Footman,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Governess,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Matchmaker,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Valet)]
        [TestCase(SkillConstants.HandleAnimal,
            SkillConstants.Profession, SkillConstants.Foci.Profession.AnimalGroomer,
            SkillConstants.Profession, SkillConstants.Foci.Profession.AnimalTrainer,
            SkillConstants.Profession, SkillConstants.Foci.Profession.ExoticAnimalTrainer,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Shepherd,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Teamster,
            SkillConstants.Ride)]
        [TestCase(SkillConstants.Heal,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Healer,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Masseuse,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Midwife)]
        [TestCase(SkillConstants.Intimidate,
            SkillConstants.Profession, SkillConstants.Foci.Profession.SailorMate)]
        [TestCase(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Arcana,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Adviser,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Sage,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Teacher,
            SkillConstants.Spellcraft)]
        [TestCase(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.ArchitectureAndEngineering,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Adviser,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Architect,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Engineer,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Sage,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Teacher,
            SkillConstants.Search)]
        [TestCase(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Dungeoneering,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Adviser,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Miner,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Sage,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Teacher,
            SkillConstants.Survival)]
        [TestCase(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Geography,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Adviser,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Miner,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Sage,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Teacher,
            SkillConstants.Survival)]
        [TestCase(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.History,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Adviser,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Sage,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Teacher)]
        [TestCase(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Local,
            SkillConstants.GatherInformation,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Adviser,
            SkillConstants.Profession, SkillConstants.Foci.Profession.CityGuide,
            SkillConstants.Profession, SkillConstants.Foci.Profession.LocalCourier,
            SkillConstants.Profession, SkillConstants.Foci.Profession.OutOfTownCourier,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Sage,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Teacher,
            SkillConstants.Profession, SkillConstants.Foci.Profession.WildernessGuide)]
        [TestCase(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Nature,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Adviser,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Apothecary,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Farmer,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Miner,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Sage,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Teacher,
            SkillConstants.Profession, SkillConstants.Foci.Profession.WildernessGuide,
            SkillConstants.Survival)]
        [TestCase(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.NobilityAndRoyalty,
            SkillConstants.Diplomacy,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Adviser,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Butler,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Footman,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Governess,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Maid,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Sage,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Teacher)]
        [TestCase(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Religion,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Adviser,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Sage,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Teacher)]
        [TestCase(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.ThePlanes,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Adviser,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Sage,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Teacher,
            SkillConstants.Survival)]
        [TestCase(SkillConstants.Listen)]
        [TestCase(SkillConstants.MoveSilently)]
        [TestCase(SkillConstants.OpenLock)]
        [TestCase(SkillConstants.Perform, SkillConstants.Foci.Perform.Act,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Entertainer)]
        [TestCase(SkillConstants.Perform, SkillConstants.Foci.Perform.Comedy,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Entertainer)]
        [TestCase(SkillConstants.Perform, SkillConstants.Foci.Perform.Dance,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Entertainer)]
        [TestCase(SkillConstants.Perform, SkillConstants.Foci.Perform.KeyboardInstruments,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Entertainer)]
        [TestCase(SkillConstants.Perform, SkillConstants.Foci.Perform.Oratory,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Entertainer)]
        [TestCase(SkillConstants.Perform, SkillConstants.Foci.Perform.PercussionInstruments,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Entertainer)]
        [TestCase(SkillConstants.Perform, SkillConstants.Foci.Perform.Sing,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Entertainer)]
        [TestCase(SkillConstants.Perform, SkillConstants.Foci.Perform.StringInstruments,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Entertainer)]
        [TestCase(SkillConstants.Perform, SkillConstants.Foci.Perform.WindInstruments,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Entertainer)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Adviser,
            SkillConstants.Diplomacy,
            SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Arcana,
            SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.ArchitectureAndEngineering,
            SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Dungeoneering,
            SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Geography,
            SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.History,
            SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Local,
            SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Nature,
            SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.NobilityAndRoyalty,
            SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Religion,
            SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.ThePlanes)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Alchemist,
            SkillConstants.Craft, SkillConstants.Foci.Craft.Alchemy)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.AnimalGroomer,
            SkillConstants.HandleAnimal)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.AnimalTrainer,
            SkillConstants.HandleAnimal)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Apothecary,
            SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Nature)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Appraiser,
            SkillConstants.Appraise)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Architect,
            SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.ArchitectureAndEngineering)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Armorer,
            SkillConstants.Craft, SkillConstants.Foci.Craft.Armorsmithing)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Barrister,
            SkillConstants.Diplomacy)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Blacksmith,
            SkillConstants.Craft, SkillConstants.Foci.Craft.Blacksmithing)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Bookbinder,
            SkillConstants.Craft, SkillConstants.Foci.Craft.Bookbinding)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Bowyer,
            SkillConstants.Craft, SkillConstants.Foci.Craft.Bowmaking)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Brazier,
            SkillConstants.Craft, SkillConstants.Foci.Craft.Brassmaking)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Brewer,
            SkillConstants.Craft, SkillConstants.Foci.Craft.Brewing)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Butler,
            SkillConstants.Diplomacy,
            SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.NobilityAndRoyalty)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Carpenter,
            SkillConstants.Craft, SkillConstants.Foci.Craft.Woodworking)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Cartographer,
            SkillConstants.Craft, SkillConstants.Foci.Craft.Mapmaking)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Cartwright,
            SkillConstants.Craft, SkillConstants.Foci.Craft.Woodworking)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Chandler,
            SkillConstants.Craft, SkillConstants.Foci.Craft.Candlemaking)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.CityGuide,
            SkillConstants.Diplomacy,
            SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Local)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Clerk)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Cobbler,
            SkillConstants.Craft, SkillConstants.Foci.Craft.Shoemaking)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Coffinmaker,
            SkillConstants.Craft, SkillConstants.Foci.Craft.Woodworking)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Coiffeur)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Cook)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Coppersmith,
            SkillConstants.Craft, SkillConstants.Foci.Craft.Coppersmithing)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Dowser,
            SkillConstants.Bluff,
            SkillConstants.Survival)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Dyer,
            SkillConstants.Craft, SkillConstants.Foci.Craft.Dyemaking)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Embalmer,
            SkillConstants.Craft, SkillConstants.Foci.Craft.Alchemy)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Engineer,
            SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.ArchitectureAndEngineering)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Entertainer,
            SkillConstants.Perform, SkillConstants.Foci.Perform.Act,
            SkillConstants.Perform, SkillConstants.Foci.Perform.Comedy,
            SkillConstants.Perform, SkillConstants.Foci.Perform.Dance,
            SkillConstants.Perform, SkillConstants.Foci.Perform.KeyboardInstruments,
            SkillConstants.Perform, SkillConstants.Foci.Perform.Oratory,
            SkillConstants.Perform, SkillConstants.Foci.Perform.PercussionInstruments,
            SkillConstants.Perform, SkillConstants.Foci.Perform.Sing,
            SkillConstants.Perform, SkillConstants.Foci.Perform.StringInstruments,
            SkillConstants.Perform, SkillConstants.Foci.Perform.WindInstruments)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.ExoticAnimalTrainer,
            SkillConstants.HandleAnimal)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Farmer,
            SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Nature)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Fletcher,
            SkillConstants.Craft, SkillConstants.Foci.Craft.Bowmaking)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Footman,
            SkillConstants.Diplomacy,
            SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.NobilityAndRoyalty)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Gemcutter,
            SkillConstants.Craft, SkillConstants.Foci.Craft.Gemcutting)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Goldsmith,
            SkillConstants.Craft, SkillConstants.Foci.Craft.Goldsmithing)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Governess,
            SkillConstants.Diplomacy,
            SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.NobilityAndRoyalty)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Haberdasher,
            SkillConstants.Craft, SkillConstants.Foci.Craft.Hatmaking)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Healer,
            SkillConstants.Heal)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Horner,
            SkillConstants.Craft, SkillConstants.Foci.Craft.Hornworking)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Hunter,
            SkillConstants.Survival)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Interpreter)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Jeweler,
            SkillConstants.Craft, SkillConstants.Foci.Craft.Jewelmaking)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Laborer)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Launderer)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Limner,
            SkillConstants.Craft, SkillConstants.Foci.Craft.Painting)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.LocalCourier,
            SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Local)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Locksmith,
            SkillConstants.Craft, SkillConstants.Foci.Craft.Locksmithing)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Maid,
            SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.NobilityAndRoyalty)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Masseuse,
            SkillConstants.Heal)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Matchmaker,
            SkillConstants.Diplomacy,
            SkillConstants.SenseMotive)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Midwife,
            SkillConstants.Heal)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Miller,
            SkillConstants.Craft, SkillConstants.Foci.Craft.Milling)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Miner,
            SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Geography,
            SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Nature,
            SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Dungeoneering)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Navigator,
            SkillConstants.Survival)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Nursemaid)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.OutOfTownCourier,
            SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Local,
            SkillConstants.Ride)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Painter,
            SkillConstants.Craft, SkillConstants.Foci.Craft.Painting)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Parchmentmaker,
            SkillConstants.Craft, SkillConstants.Foci.Craft.Parchmentmaking)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Pewterer,
            SkillConstants.Craft, SkillConstants.Foci.Craft.Pewtermaking)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Polisher)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Porter)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Potter,
            SkillConstants.Craft, SkillConstants.Foci.Craft.Potterymaking)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Sage,
            SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Arcana,
            SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.ArchitectureAndEngineering,
            SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Dungeoneering,
            SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Geography,
            SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.History,
            SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Local,
            SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Nature,
            SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.NobilityAndRoyalty,
            SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Religion,
            SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.ThePlanes)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.SailorCrewmember,
            SkillConstants.Swim)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.SailorMate,
            SkillConstants.Intimidate,
            SkillConstants.Swim)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Scribe)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Sculptor,
            SkillConstants.Craft, SkillConstants.Foci.Craft.Sculpting)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Shepherd,
            SkillConstants.HandleAnimal)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Shipwright,
            SkillConstants.Craft, SkillConstants.Foci.Craft.Shipmaking)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Silversmith,
            SkillConstants.Craft, SkillConstants.Foci.Craft.Silversmithing)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Skinner,
            SkillConstants.Craft, SkillConstants.Foci.Craft.Skinning)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Soapmaker,
            SkillConstants.Craft, SkillConstants.Foci.Craft.Soapmaking)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Soothsayer,
            SkillConstants.Bluff)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Tanner,
            SkillConstants.Craft, SkillConstants.Foci.Craft.Tanning)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Teacher,
            SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Arcana,
            SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.ArchitectureAndEngineering,
            SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Dungeoneering,
            SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Geography,
            SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.History,
            SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Local,
            SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Nature,
            SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.NobilityAndRoyalty,
            SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Religion,
            SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.ThePlanes)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Teamster,
            SkillConstants.HandleAnimal,
            SkillConstants.Ride)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Trader,
            SkillConstants.Appraise,
            SkillConstants.SenseMotive)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Valet,
            SkillConstants.Diplomacy)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Vintner,
            SkillConstants.Craft, SkillConstants.Foci.Craft.Winemaking)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Weaponsmith,
            SkillConstants.Craft, SkillConstants.Foci.Craft.Weaponsmithing)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Weaver,
            SkillConstants.Craft, SkillConstants.Foci.Craft.Weaving)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Wheelwright,
            SkillConstants.Craft, SkillConstants.Foci.Craft.Wheelmaking)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.WildernessGuide,
            SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Local,
            SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Nature)]
        [TestCase(SkillConstants.Ride,
            SkillConstants.Profession, SkillConstants.Foci.Profession.OutOfTownCourier,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Teamster)]
        [TestCase(SkillConstants.Search,
            SkillConstants.Survival)]
        [TestCase(SkillConstants.SenseMotive,
            SkillConstants.Diplomacy,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Matchmaker,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Trader)]
        [TestCase(SkillConstants.SleightOfHand)]
        [TestCase(SkillConstants.Spellcraft,
            SkillConstants.UseMagicDevice)]
        [TestCase(SkillConstants.Spot)]
        [TestCase(SkillConstants.Survival,
            SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Nature,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Dowser,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Hunter,
            SkillConstants.Profession, SkillConstants.Foci.Profession.Navigator)]
        [TestCase(SkillConstants.Swim,
            SkillConstants.Profession, SkillConstants.Foci.Profession.SailorCrewmember,
            SkillConstants.Profession, SkillConstants.Foci.Profession.SailorMate)]
        [TestCase(SkillConstants.Tumble,
            SkillConstants.Balance,
            SkillConstants.Jump)]
        [TestCase(SkillConstants.UseMagicDevice,
            SkillConstants.Spellcraft)]
        [TestCase(SkillConstants.UseRope,
            SkillConstants.Climb,
            SkillConstants.EscapeArtist)]
        public void SynergySkills(string skill, params string[] synergies)
        {
            base.AssertDistinctCollection(skill, synergies);
        }

        [Test]
        public void CraftsmanSkillSynergy()
        {
            var skills = new[]
            {
                SkillConstants.Craft, SkillConstants.Foci.Craft.Alchemy,
                SkillConstants.Craft, SkillConstants.Foci.Craft.Armorsmithing,
                SkillConstants.Craft, SkillConstants.Foci.Craft.Blacksmithing,
                SkillConstants.Craft, SkillConstants.Foci.Craft.Bookbinding,
                SkillConstants.Craft, SkillConstants.Foci.Craft.Bowmaking,
                SkillConstants.Craft, SkillConstants.Foci.Craft.Brassmaking,
                SkillConstants.Craft, SkillConstants.Foci.Craft.Brewing,
                SkillConstants.Craft, SkillConstants.Foci.Craft.Candlemaking,
                SkillConstants.Craft, SkillConstants.Foci.Craft.Cloth,
                SkillConstants.Craft, SkillConstants.Foci.Craft.Coppersmithing,
                SkillConstants.Craft, SkillConstants.Foci.Craft.Dyemaking,
                SkillConstants.Craft, SkillConstants.Foci.Craft.Gemcutting,
                SkillConstants.Craft, SkillConstants.Foci.Craft.Glass,
                SkillConstants.Craft, SkillConstants.Foci.Craft.Goldsmithing,
                SkillConstants.Craft, SkillConstants.Foci.Craft.Hatmaking,
                SkillConstants.Craft, SkillConstants.Foci.Craft.Hornworking,
                SkillConstants.Craft, SkillConstants.Foci.Craft.Jewelmaking,
                SkillConstants.Craft, SkillConstants.Foci.Craft.Leather,
                SkillConstants.Craft, SkillConstants.Foci.Craft.Locksmithing,
                SkillConstants.Craft, SkillConstants.Foci.Craft.Mapmaking,
                SkillConstants.Craft, SkillConstants.Foci.Craft.Milling,
                SkillConstants.Craft, SkillConstants.Foci.Craft.Painting,
                SkillConstants.Craft, SkillConstants.Foci.Craft.Parchmentmaking,
                SkillConstants.Craft, SkillConstants.Foci.Craft.Pewtermaking,
                SkillConstants.Craft, SkillConstants.Foci.Craft.Potterymaking,
                SkillConstants.Craft, SkillConstants.Foci.Craft.Sculpting,
                SkillConstants.Craft, SkillConstants.Foci.Craft.Shipmaking,
                SkillConstants.Craft, SkillConstants.Foci.Craft.Shoemaking,
                SkillConstants.Craft, SkillConstants.Foci.Craft.Silversmithing,
                SkillConstants.Craft, SkillConstants.Foci.Craft.Skinning,
                SkillConstants.Craft, SkillConstants.Foci.Craft.Soapmaking,
                SkillConstants.Craft, SkillConstants.Foci.Craft.Stonemasonry,
                SkillConstants.Craft, SkillConstants.Foci.Craft.Tanning,
                SkillConstants.Craft, SkillConstants.Foci.Craft.Trapmaking,
                SkillConstants.Craft, SkillConstants.Foci.Craft.Weaponsmithing,
                SkillConstants.Craft, SkillConstants.Foci.Craft.Weaving,
                SkillConstants.Craft, SkillConstants.Foci.Craft.Wheelmaking,
                SkillConstants.Craft, SkillConstants.Foci.Craft.Winemaking,
                SkillConstants.Craft, SkillConstants.Foci.Craft.Woodworking,
            };

            AssertDistinctCollection(SkillConstants.Profession, SkillConstants.Foci.Profession.Craftsman, skills);
        }
    }
}