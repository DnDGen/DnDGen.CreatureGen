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
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Appraise, null, SkillConstants.Profession, SkillConstants.Foci.Profession.Appraiser),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Appraise, null, SkillConstants.Profession, SkillConstants.Foci.Profession.Trader),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Bluff, null, SkillConstants.Diplomacy, null),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Bluff, null, SkillConstants.Disguise, null),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Bluff, null, SkillConstants.Intimidate, null),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Bluff, null, SkillConstants.Profession, SkillConstants.Foci.Profession.Dowser),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Bluff, null, SkillConstants.Profession, SkillConstants.Foci.Profession.Soothsayer),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Bluff, null, SkillConstants.SleightOfHand, null),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Craft, null, SkillConstants.Appraise, null),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Craft, null, SkillConstants.Profession, SkillConstants.Foci.Profession.Craftsman),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Craft, SkillConstants.Foci.Craft.Alchemy, SkillConstants.Profession, SkillConstants.Foci.Profession.Alchemist),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Craft, SkillConstants.Foci.Craft.Alchemy, SkillConstants.Profession, SkillConstants.Foci.Profession.Embalmer),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Craft, SkillConstants.Foci.Craft.Armorsmithing, SkillConstants.Profession, SkillConstants.Foci.Profession.Armorer),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Craft, SkillConstants.Foci.Craft.Blacksmithing, SkillConstants.Profession, SkillConstants.Foci.Profession.Blacksmith),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Craft, SkillConstants.Foci.Craft.Bookbinding, SkillConstants.Profession, SkillConstants.Foci.Profession.Bookbinder),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Craft, SkillConstants.Foci.Craft.Bowmaking, SkillConstants.Profession, SkillConstants.Foci.Profession.Bowyer),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Craft, SkillConstants.Foci.Craft.Bowmaking, SkillConstants.Profession, SkillConstants.Foci.Profession.Fletcher),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Craft, SkillConstants.Foci.Craft.Brassmaking, SkillConstants.Profession, SkillConstants.Foci.Profession.Brazier),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Craft, SkillConstants.Foci.Craft.Brewing, SkillConstants.Profession, SkillConstants.Foci.Profession.Brewer),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Craft, SkillConstants.Foci.Craft.Candlemaking, SkillConstants.Profession, SkillConstants.Foci.Profession.Chandler),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Craft, SkillConstants.Foci.Craft.Coppersmithing, SkillConstants.Profession, SkillConstants.Foci.Profession.Coppersmith),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Craft, SkillConstants.Foci.Craft.Dyemaking, SkillConstants.Profession, SkillConstants.Foci.Profession.Dyer),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Craft, SkillConstants.Foci.Craft.Gemcutting, SkillConstants.Profession, SkillConstants.Foci.Profession.Gemcutter),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Craft, SkillConstants.Foci.Craft.Goldsmithing, SkillConstants.Profession, SkillConstants.Foci.Profession.Goldsmith),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Craft, SkillConstants.Foci.Craft.Hatmaking, SkillConstants.Profession, SkillConstants.Foci.Profession.Haberdasher),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Craft, SkillConstants.Foci.Craft.Hornworking, SkillConstants.Profession, SkillConstants.Foci.Profession.Horner),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Craft, SkillConstants.Foci.Craft.Jewelmaking, SkillConstants.Profession, SkillConstants.Foci.Profession.Jeweler),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Craft, SkillConstants.Foci.Craft.Locksmithing, SkillConstants.Profession, SkillConstants.Foci.Profession.Locksmith),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Craft, SkillConstants.Foci.Craft.Mapmaking, SkillConstants.Profession, SkillConstants.Foci.Profession.Cartographer),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Craft, SkillConstants.Foci.Craft.Milling, SkillConstants.Profession, SkillConstants.Foci.Profession.Miller),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Craft, SkillConstants.Foci.Craft.Painting, SkillConstants.Profession, SkillConstants.Foci.Profession.Limner),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Craft, SkillConstants.Foci.Craft.Painting, SkillConstants.Profession, SkillConstants.Foci.Profession.Painter),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Craft, SkillConstants.Foci.Craft.Parchmentmaking, SkillConstants.Profession, SkillConstants.Foci.Profession.Parchmentmaker),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Craft, SkillConstants.Foci.Craft.Pewtermaking, SkillConstants.Profession, SkillConstants.Foci.Profession.Pewterer),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Craft, SkillConstants.Foci.Craft.Potterymaking, SkillConstants.Profession, SkillConstants.Foci.Profession.Potter),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Craft, SkillConstants.Foci.Craft.Sculpting, SkillConstants.Profession, SkillConstants.Foci.Profession.Sculptor),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Craft, SkillConstants.Foci.Craft.Shipmaking, SkillConstants.Profession, SkillConstants.Foci.Profession.Shipwright),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Craft, SkillConstants.Foci.Craft.Shoemaking, SkillConstants.Profession, SkillConstants.Foci.Profession.Cobbler),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Craft, SkillConstants.Foci.Craft.Silversmithing, SkillConstants.Profession, SkillConstants.Foci.Profession.Silversmith),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Craft, SkillConstants.Foci.Craft.Skinning, SkillConstants.Profession, SkillConstants.Foci.Profession.Skinner),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Craft, SkillConstants.Foci.Craft.Soapmaking, SkillConstants.Profession, SkillConstants.Foci.Profession.Soapmaker),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Craft, SkillConstants.Foci.Craft.Tanning, SkillConstants.Profession, SkillConstants.Foci.Profession.Tanner),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Craft, SkillConstants.Foci.Craft.Trapmaking, SkillConstants.Profession, SkillConstants.Foci.Profession.Hunter),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Craft, SkillConstants.Foci.Craft.Trapmaking, SkillConstants.Profession, SkillConstants.Foci.Profession.Trapper),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Craft, SkillConstants.Foci.Craft.Trapmaking, SkillConstants.Search, null),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Craft, SkillConstants.Foci.Craft.Weaponsmithing, SkillConstants.Profession, SkillConstants.Foci.Profession.Weaponsmith),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Craft, SkillConstants.Foci.Craft.Weaving, SkillConstants.Profession, SkillConstants.Foci.Profession.Weaver),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Craft, SkillConstants.Foci.Craft.Wheelmaking, SkillConstants.Profession, SkillConstants.Foci.Profession.Wheelwright),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Craft, SkillConstants.Foci.Craft.Winemaking, SkillConstants.Profession, SkillConstants.Foci.Profession.Vintner),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Craft, SkillConstants.Foci.Craft.Woodworking, SkillConstants.Profession, SkillConstants.Foci.Profession.Carpenter),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Craft, SkillConstants.Foci.Craft.Woodworking, SkillConstants.Profession, SkillConstants.Foci.Profession.Cartwright),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Craft, SkillConstants.Foci.Craft.Woodworking, SkillConstants.Profession, SkillConstants.Foci.Profession.Coffinmaker),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.DecipherScript, null, SkillConstants.UseMagicDevice, null),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Diplomacy, null, SkillConstants.Profession, SkillConstants.Foci.Profession.Barrister),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Diplomacy, null, SkillConstants.Profession, SkillConstants.Foci.Profession.Butler),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Diplomacy, null, SkillConstants.Profession, SkillConstants.Foci.Profession.CityGuide),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Diplomacy, null, SkillConstants.Profession, SkillConstants.Foci.Profession.Footman),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Diplomacy, null, SkillConstants.Profession, SkillConstants.Foci.Profession.Governess),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Diplomacy, null, SkillConstants.Profession, SkillConstants.Foci.Profession.Matchmaker),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Diplomacy, null, SkillConstants.Profession, SkillConstants.Foci.Profession.Valet),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.EscapeArtist, null, SkillConstants.UseRope, null),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.HandleAnimal, null, SkillConstants.Diplomacy, null),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.HandleAnimal, null, SkillConstants.Profession, SkillConstants.Foci.Profession.AnimalGroomer),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.HandleAnimal, null, SkillConstants.Profession, SkillConstants.Foci.Profession.AnimalTrainer),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.HandleAnimal, null, SkillConstants.Profession, SkillConstants.Foci.Profession.ExoticAnimalTrainer),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.HandleAnimal, null, SkillConstants.Profession, SkillConstants.Foci.Profession.Shepherd),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.HandleAnimal, null, SkillConstants.Profession, SkillConstants.Foci.Profession.Teamster),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.HandleAnimal, null, SkillConstants.Ride, null),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Heal, null, SkillConstants.Profession, SkillConstants.Foci.Profession.Healer),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Heal, null, SkillConstants.Profession, SkillConstants.Foci.Profession.Masseuse),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Heal, null, SkillConstants.Profession, SkillConstants.Foci.Profession.Midwife),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Intimidate, null, SkillConstants.Profession, SkillConstants.Foci.Profession.SailorMate),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Jump, null, SkillConstants.Tumble, null),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Knowledge, null, SkillConstants.Profession, SkillConstants.Foci.Profession.Adviser),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Knowledge, null, SkillConstants.Profession, SkillConstants.Foci.Profession.Sage),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Knowledge, null, SkillConstants.Profession, SkillConstants.Foci.Profession.Teacher),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Arcana, SkillConstants.Spellcraft, null),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.ArchitectureAndEngineering, SkillConstants.Profession, SkillConstants.Foci.Profession.Architect),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.ArchitectureAndEngineering, SkillConstants.Profession, SkillConstants.Foci.Profession.Engineer),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.ArchitectureAndEngineering, SkillConstants.Search, null),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Dungeoneering, SkillConstants.Profession, SkillConstants.Foci.Profession.Miner),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Dungeoneering, SkillConstants.Survival, null),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Geography, SkillConstants.Profession, SkillConstants.Foci.Profession.Miner),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Geography, SkillConstants.Survival, null),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.History, SkillConstants.Knowledge, "bardic"),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Local, SkillConstants.GatherInformation, null),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Local, SkillConstants.Profession, SkillConstants.Foci.Profession.CityGuide),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Local, SkillConstants.Profession, SkillConstants.Foci.Profession.LocalCourier),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Local, SkillConstants.Profession, SkillConstants.Foci.Profession.OutOfTownCourier),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Local, SkillConstants.Profession, SkillConstants.Foci.Profession.WildernessGuide),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Nature, SkillConstants.Profession, SkillConstants.Foci.Profession.Apothecary),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Nature, SkillConstants.Profession, SkillConstants.Foci.Profession.Farmer),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Nature, SkillConstants.Profession, SkillConstants.Foci.Profession.Hunter),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Nature, SkillConstants.Profession, SkillConstants.Foci.Profession.Miner),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Nature, SkillConstants.Profession, SkillConstants.Foci.Profession.Trapper),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Nature, SkillConstants.Profession, SkillConstants.Foci.Profession.WildernessGuide),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Nature, SkillConstants.Survival, null),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.NobilityAndRoyalty, SkillConstants.Diplomacy, null),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.NobilityAndRoyalty, SkillConstants.Profession, SkillConstants.Foci.Profession.Butler),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.NobilityAndRoyalty, SkillConstants.Profession, SkillConstants.Foci.Profession.Footman),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.NobilityAndRoyalty, SkillConstants.Profession, SkillConstants.Foci.Profession.Governess),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.NobilityAndRoyalty, SkillConstants.Profession, SkillConstants.Foci.Profession.Maid),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.ThePlanes, SkillConstants.Survival, null),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Perform, null, SkillConstants.Profession, SkillConstants.Foci.Profession.Entertainer),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Profession, SkillConstants.Foci.Profession.Adviser, SkillConstants.Diplomacy, null),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Profession, SkillConstants.Foci.Profession.Adviser, SkillConstants.Knowledge, null),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Profession, SkillConstants.Foci.Profession.Alchemist, SkillConstants.Craft, SkillConstants.Foci.Craft.Alchemy),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Profession, SkillConstants.Foci.Profession.AnimalGroomer, SkillConstants.HandleAnimal, null),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Profession, SkillConstants.Foci.Profession.AnimalTrainer, SkillConstants.HandleAnimal, null),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Profession, SkillConstants.Foci.Profession.Apothecary, SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Nature),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Profession, SkillConstants.Foci.Profession.Appraiser, SkillConstants.Appraise, null),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Profession, SkillConstants.Foci.Profession.Architect, SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.ArchitectureAndEngineering),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Profession, SkillConstants.Foci.Profession.Armorer, SkillConstants.Craft, SkillConstants.Foci.Craft.Armorsmithing),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Profession, SkillConstants.Foci.Profession.Barrister, SkillConstants.Diplomacy, null),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Profession, SkillConstants.Foci.Profession.Blacksmith, SkillConstants.Craft, SkillConstants.Foci.Craft.Blacksmithing),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Profession, SkillConstants.Foci.Profession.Bookbinder, SkillConstants.Craft, SkillConstants.Foci.Craft.Bookbinding),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Profession, SkillConstants.Foci.Profession.Bowyer, SkillConstants.Craft, SkillConstants.Foci.Craft.Bowmaking),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Profession, SkillConstants.Foci.Profession.Brazier, SkillConstants.Craft, SkillConstants.Foci.Craft.Brassmaking),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Profession, SkillConstants.Foci.Profession.Brewer, SkillConstants.Craft, SkillConstants.Foci.Craft.Brewing),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Profession, SkillConstants.Foci.Profession.Butler, SkillConstants.Diplomacy, null),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Profession, SkillConstants.Foci.Profession.Butler, SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.NobilityAndRoyalty),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Profession, SkillConstants.Foci.Profession.Carpenter, SkillConstants.Craft, SkillConstants.Foci.Craft.Woodworking),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Profession, SkillConstants.Foci.Profession.Cartographer, SkillConstants.Craft, SkillConstants.Foci.Craft.Mapmaking),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Profession, SkillConstants.Foci.Profession.Cartwright, SkillConstants.Craft, SkillConstants.Foci.Craft.Woodworking),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Profession, SkillConstants.Foci.Profession.Chandler, SkillConstants.Craft, SkillConstants.Foci.Craft.Candlemaking),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Profession, SkillConstants.Foci.Profession.CityGuide, SkillConstants.Diplomacy, null),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Profession, SkillConstants.Foci.Profession.CityGuide, SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Local),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Profession, SkillConstants.Foci.Profession.Cobbler, SkillConstants.Craft, SkillConstants.Foci.Craft.Shoemaking),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Profession, SkillConstants.Foci.Profession.Coffinmaker, SkillConstants.Craft, SkillConstants.Foci.Craft.Woodworking),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Profession, SkillConstants.Foci.Profession.Coppersmith, SkillConstants.Craft, SkillConstants.Foci.Craft.Coppersmithing),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Profession, SkillConstants.Foci.Profession.Craftsman, SkillConstants.Craft, null),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Profession, SkillConstants.Foci.Profession.Dowser, SkillConstants.Bluff, null),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Profession, SkillConstants.Foci.Profession.Dowser, SkillConstants.Survival, null),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Profession, SkillConstants.Foci.Profession.Dyer, SkillConstants.Craft, SkillConstants.Foci.Craft.Dyemaking),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Profession, SkillConstants.Foci.Profession.Embalmer, SkillConstants.Craft, SkillConstants.Foci.Craft.Alchemy),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Profession, SkillConstants.Foci.Profession.Engineer, SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.ArchitectureAndEngineering),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Profession, SkillConstants.Foci.Profession.Entertainer, SkillConstants.Perform, null),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Profession, SkillConstants.Foci.Profession.ExoticAnimalTrainer, SkillConstants.HandleAnimal, null),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Profession, SkillConstants.Foci.Profession.Farmer, SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Nature),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Profession, SkillConstants.Foci.Profession.Fletcher, SkillConstants.Craft, SkillConstants.Foci.Craft.Bowmaking),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Profession, SkillConstants.Foci.Profession.Footman, SkillConstants.Diplomacy, null),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Profession, SkillConstants.Foci.Profession.Footman, SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.NobilityAndRoyalty),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Profession, SkillConstants.Foci.Profession.Gemcutter, SkillConstants.Craft, SkillConstants.Foci.Craft.Gemcutting),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Profession, SkillConstants.Foci.Profession.Goldsmith, SkillConstants.Craft, SkillConstants.Foci.Craft.Goldsmithing),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Profession, SkillConstants.Foci.Profession.Governess, SkillConstants.Diplomacy, null),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Profession, SkillConstants.Foci.Profession.Governess, SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.NobilityAndRoyalty),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Profession, SkillConstants.Foci.Profession.Haberdasher, SkillConstants.Craft, SkillConstants.Foci.Craft.Hatmaking),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Profession, SkillConstants.Foci.Profession.Healer, SkillConstants.Heal, null),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Profession, SkillConstants.Foci.Profession.Horner, SkillConstants.Craft, SkillConstants.Foci.Craft.Hornworking),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Profession, SkillConstants.Foci.Profession.Hunter, SkillConstants.Craft, SkillConstants.Foci.Craft.Trapmaking),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Profession, SkillConstants.Foci.Profession.Hunter, SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Nature),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Profession, SkillConstants.Foci.Profession.Hunter, SkillConstants.Survival, null),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Profession, SkillConstants.Foci.Profession.Jeweler, SkillConstants.Craft, SkillConstants.Foci.Craft.Jewelmaking),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Profession, SkillConstants.Foci.Profession.Limner, SkillConstants.Craft, SkillConstants.Foci.Craft.Painting),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Profession, SkillConstants.Foci.Profession.LocalCourier, SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Local),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Profession, SkillConstants.Foci.Profession.Locksmith, SkillConstants.Craft, SkillConstants.Foci.Craft.Locksmithing),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Profession, SkillConstants.Foci.Profession.Maid, SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.NobilityAndRoyalty),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Profession, SkillConstants.Foci.Profession.Masseuse, SkillConstants.Heal, null),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Profession, SkillConstants.Foci.Profession.Matchmaker, SkillConstants.Diplomacy, null),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Profession, SkillConstants.Foci.Profession.Matchmaker, SkillConstants.SenseMotive, null),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Profession, SkillConstants.Foci.Profession.Midwife, SkillConstants.Heal, null),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Profession, SkillConstants.Foci.Profession.Miller, SkillConstants.Craft, SkillConstants.Foci.Craft.Milling),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Profession, SkillConstants.Foci.Profession.Miner, SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Geography),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Profession, SkillConstants.Foci.Profession.Miner, SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Nature),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Profession, SkillConstants.Foci.Profession.Miner, SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Dungeoneering),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Profession, SkillConstants.Foci.Profession.Navigator, SkillConstants.Survival, null),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Profession, SkillConstants.Foci.Profession.OutOfTownCourier, SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Local),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Profession, SkillConstants.Foci.Profession.OutOfTownCourier, SkillConstants.Ride, null),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Profession, SkillConstants.Foci.Profession.Painter, SkillConstants.Craft, SkillConstants.Foci.Craft.Painting),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Profession, SkillConstants.Foci.Profession.Parchmentmaker, SkillConstants.Craft, SkillConstants.Foci.Craft.Parchmentmaking),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Profession, SkillConstants.Foci.Profession.Pewterer, SkillConstants.Craft, SkillConstants.Foci.Craft.Pewtermaking),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Profession, SkillConstants.Foci.Profession.Potter, SkillConstants.Craft, SkillConstants.Foci.Craft.Potterymaking),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Profession, SkillConstants.Foci.Profession.Sage, SkillConstants.Knowledge, null),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Profession, SkillConstants.Foci.Profession.SailorCrewmember, SkillConstants.Swim, null),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Profession, SkillConstants.Foci.Profession.SailorMate, SkillConstants.Intimidate, null),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Profession, SkillConstants.Foci.Profession.SailorMate, SkillConstants.Swim, null),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Profession, SkillConstants.Foci.Profession.Sculptor, SkillConstants.Craft, SkillConstants.Foci.Craft.Sculpting),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Profession, SkillConstants.Foci.Profession.Shepherd, SkillConstants.HandleAnimal, null),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Profession, SkillConstants.Foci.Profession.Shipwright, SkillConstants.Craft, SkillConstants.Foci.Craft.Shipmaking),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Profession, SkillConstants.Foci.Profession.Silversmith, SkillConstants.Craft, SkillConstants.Foci.Craft.Silversmithing),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Profession, SkillConstants.Foci.Profession.Skinner, SkillConstants.Craft, SkillConstants.Foci.Craft.Skinning),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Profession, SkillConstants.Foci.Profession.Soapmaker, SkillConstants.Craft, SkillConstants.Foci.Craft.Soapmaking),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Profession, SkillConstants.Foci.Profession.Soothsayer, SkillConstants.Bluff, null),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Profession, SkillConstants.Foci.Profession.Tanner, SkillConstants.Craft, SkillConstants.Foci.Craft.Tanning),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Profession, SkillConstants.Foci.Profession.Teacher, SkillConstants.Knowledge, null),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Profession, SkillConstants.Foci.Profession.Teamster, SkillConstants.HandleAnimal, null),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Profession, SkillConstants.Foci.Profession.Teamster, SkillConstants.Ride, null),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Profession, SkillConstants.Foci.Profession.Trader, SkillConstants.Appraise, null),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Profession, SkillConstants.Foci.Profession.Trader, SkillConstants.SenseMotive, null),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Profession, SkillConstants.Foci.Profession.Trapper, SkillConstants.Craft, SkillConstants.Foci.Craft.Trapmaking),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Profession, SkillConstants.Foci.Profession.Trapper, SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Nature),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Profession, SkillConstants.Foci.Profession.Trapper, SkillConstants.Survival, null),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Profession, SkillConstants.Foci.Profession.Valet, SkillConstants.Diplomacy, null),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Profession, SkillConstants.Foci.Profession.Vintner, SkillConstants.Craft, SkillConstants.Foci.Craft.Winemaking),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Profession, SkillConstants.Foci.Profession.Weaponsmith, SkillConstants.Craft, SkillConstants.Foci.Craft.Weaponsmithing),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Profession, SkillConstants.Foci.Profession.Weaver, SkillConstants.Craft, SkillConstants.Foci.Craft.Weaving),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Profession, SkillConstants.Foci.Profession.Wheelwright, SkillConstants.Craft, SkillConstants.Foci.Craft.Wheelmaking),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Profession, SkillConstants.Foci.Profession.WildernessGuide, SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Local),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Profession, SkillConstants.Foci.Profession.WildernessGuide, SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Nature),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Ride, null, SkillConstants.Profession, SkillConstants.Foci.Profession.OutOfTownCourier),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Ride, null, SkillConstants.Profession, SkillConstants.Foci.Profession.Teamster),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Search, null, SkillConstants.Survival, null),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.SenseMotive, null, SkillConstants.Diplomacy, null),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.SenseMotive, null, SkillConstants.Profession, SkillConstants.Foci.Profession.Matchmaker),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.SenseMotive, null, SkillConstants.Profession, SkillConstants.Foci.Profession.Trader),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Spellcraft, null, SkillConstants.UseMagicDevice, null),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Survival, null, SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Nature),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Survival, null, SkillConstants.Profession, SkillConstants.Foci.Profession.Dowser),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Survival, null, SkillConstants.Profession, SkillConstants.Foci.Profession.Hunter),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Survival, null, SkillConstants.Profession, SkillConstants.Foci.Profession.Navigator),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Survival, null, SkillConstants.Profession, SkillConstants.Foci.Profession.Trapper),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Swim, null, SkillConstants.Profession, SkillConstants.Foci.Profession.SailorCrewmember),
                DataIndexConstants.SkillSynergyFeatData.BuildDataKey(SkillConstants.Swim, null, SkillConstants.Profession, SkillConstants.Foci.Profession.SailorMate),
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
        [TestCase(SkillConstants.Craft, null, SkillConstants.Profession, SkillConstants.Foci.Profession.Craftsman, null)]
        [TestCase(SkillConstants.Craft, SkillConstants.Foci.Craft.Alchemy, SkillConstants.Profession, SkillConstants.Foci.Profession.Alchemist, null)]
        [TestCase(SkillConstants.Craft, SkillConstants.Foci.Craft.Alchemy, SkillConstants.Profession, SkillConstants.Foci.Profession.Embalmer, null)]
        [TestCase(SkillConstants.Craft, SkillConstants.Foci.Craft.Armorsmithing, SkillConstants.Profession, SkillConstants.Foci.Profession.Armorer, null)]
        [TestCase(SkillConstants.Craft, SkillConstants.Foci.Craft.Blacksmithing, SkillConstants.Profession, SkillConstants.Foci.Profession.Blacksmith, null)]
        [TestCase(SkillConstants.Craft, SkillConstants.Foci.Craft.Bookbinding, SkillConstants.Profession, SkillConstants.Foci.Profession.Bookbinder, null)]
        [TestCase(SkillConstants.Craft, SkillConstants.Foci.Craft.Bowmaking, SkillConstants.Profession, SkillConstants.Foci.Profession.Bowyer, null)]
        [TestCase(SkillConstants.Craft, SkillConstants.Foci.Craft.Bowmaking, SkillConstants.Profession, SkillConstants.Foci.Profession.Fletcher, null)]
        [TestCase(SkillConstants.Craft, SkillConstants.Foci.Craft.Brassmaking, SkillConstants.Profession, SkillConstants.Foci.Profession.Brazier, null)]
        [TestCase(SkillConstants.Craft, SkillConstants.Foci.Craft.Brewing, SkillConstants.Profession, SkillConstants.Foci.Profession.Brewer, null)]
        [TestCase(SkillConstants.Craft, SkillConstants.Foci.Craft.Candlemaking, SkillConstants.Profession, SkillConstants.Foci.Profession.Chandler, null)]
        [TestCase(SkillConstants.Craft, SkillConstants.Foci.Craft.Coppersmithing, SkillConstants.Profession, SkillConstants.Foci.Profession.Coppersmith, null)]
        [TestCase(SkillConstants.Craft, SkillConstants.Foci.Craft.Dyemaking, SkillConstants.Profession, SkillConstants.Foci.Profession.Dyer, null)]
        [TestCase(SkillConstants.Craft, SkillConstants.Foci.Craft.Gemcutting, SkillConstants.Profession, SkillConstants.Foci.Profession.Gemcutter, null)]
        [TestCase(SkillConstants.Craft, SkillConstants.Foci.Craft.Goldsmithing, SkillConstants.Profession, SkillConstants.Foci.Profession.Goldsmith, null)]
        [TestCase(SkillConstants.Craft, SkillConstants.Foci.Craft.Hatmaking, SkillConstants.Profession, SkillConstants.Foci.Profession.Haberdasher, null)]
        [TestCase(SkillConstants.Craft, SkillConstants.Foci.Craft.Hornworking, SkillConstants.Profession, SkillConstants.Foci.Profession.Horner, null)]
        [TestCase(SkillConstants.Craft, SkillConstants.Foci.Craft.Jewelmaking, SkillConstants.Profession, SkillConstants.Foci.Profession.Jeweler, null)]
        [TestCase(SkillConstants.Craft, SkillConstants.Foci.Craft.Locksmithing, SkillConstants.Profession, SkillConstants.Foci.Profession.Locksmith, null)]
        [TestCase(SkillConstants.Craft, SkillConstants.Foci.Craft.Mapmaking, SkillConstants.Profession, SkillConstants.Foci.Profession.Cartographer, null)]
        [TestCase(SkillConstants.Craft, SkillConstants.Foci.Craft.Milling, SkillConstants.Profession, SkillConstants.Foci.Profession.Miller, null)]
        [TestCase(SkillConstants.Craft, SkillConstants.Foci.Craft.Painting, SkillConstants.Profession, SkillConstants.Foci.Profession.Limner, null)]
        [TestCase(SkillConstants.Craft, SkillConstants.Foci.Craft.Painting, SkillConstants.Profession, SkillConstants.Foci.Profession.Painter, null)]
        [TestCase(SkillConstants.Craft, SkillConstants.Foci.Craft.Parchmentmaking, SkillConstants.Profession, SkillConstants.Foci.Profession.Parchmentmaker, null)]
        [TestCase(SkillConstants.Craft, SkillConstants.Foci.Craft.Pewtermaking, SkillConstants.Profession, SkillConstants.Foci.Profession.Pewterer, null)]
        [TestCase(SkillConstants.Craft, SkillConstants.Foci.Craft.Potterymaking, SkillConstants.Profession, SkillConstants.Foci.Profession.Potter, null)]
        [TestCase(SkillConstants.Craft, SkillConstants.Foci.Craft.Sculpting, SkillConstants.Profession, SkillConstants.Foci.Profession.Sculptor, null)]
        [TestCase(SkillConstants.Craft, SkillConstants.Foci.Craft.Shipmaking, SkillConstants.Profession, SkillConstants.Foci.Profession.Shipwright, null)]
        [TestCase(SkillConstants.Craft, SkillConstants.Foci.Craft.Shoemaking, SkillConstants.Profession, SkillConstants.Foci.Profession.Cobbler, null)]
        [TestCase(SkillConstants.Craft, SkillConstants.Foci.Craft.Silversmithing, SkillConstants.Profession, SkillConstants.Foci.Profession.Silversmith, null)]
        [TestCase(SkillConstants.Craft, SkillConstants.Foci.Craft.Skinning, SkillConstants.Profession, SkillConstants.Foci.Profession.Skinner, null)]
        [TestCase(SkillConstants.Craft, SkillConstants.Foci.Craft.Soapmaking, SkillConstants.Profession, SkillConstants.Foci.Profession.Soapmaker, null)]
        [TestCase(SkillConstants.Craft, SkillConstants.Foci.Craft.Tanning, SkillConstants.Profession, SkillConstants.Foci.Profession.Tanner, null)]
        [TestCase(SkillConstants.Craft, SkillConstants.Foci.Craft.Trapmaking, SkillConstants.Profession, SkillConstants.Foci.Profession.Hunter, null)]
        [TestCase(SkillConstants.Craft, SkillConstants.Foci.Craft.Trapmaking, SkillConstants.Profession, SkillConstants.Foci.Profession.Trapper, null)]
        [TestCase(SkillConstants.Craft, SkillConstants.Foci.Craft.Trapmaking, SkillConstants.Search, null, "finding traps")]
        [TestCase(SkillConstants.Craft, SkillConstants.Foci.Craft.Weaponsmithing, SkillConstants.Profession, SkillConstants.Foci.Profession.Weaponsmith, null)]
        [TestCase(SkillConstants.Craft, SkillConstants.Foci.Craft.Weaving, SkillConstants.Profession, SkillConstants.Foci.Profession.Weaver, null)]
        [TestCase(SkillConstants.Craft, SkillConstants.Foci.Craft.Wheelmaking, SkillConstants.Profession, SkillConstants.Foci.Profession.Wheelwright, null)]
        [TestCase(SkillConstants.Craft, SkillConstants.Foci.Craft.Winemaking, SkillConstants.Profession, SkillConstants.Foci.Profession.Vintner, null)]
        [TestCase(SkillConstants.Craft, SkillConstants.Foci.Craft.Woodworking, SkillConstants.Profession, SkillConstants.Foci.Profession.Carpenter, null)]
        [TestCase(SkillConstants.Craft, SkillConstants.Foci.Craft.Woodworking, SkillConstants.Profession, SkillConstants.Foci.Profession.Cartwright, null)]
        [TestCase(SkillConstants.Craft, SkillConstants.Foci.Craft.Woodworking, SkillConstants.Profession, SkillConstants.Foci.Profession.Coffinmaker, null)]
        [TestCase(SkillConstants.DecipherScript, null, SkillConstants.UseMagicDevice, null, "with scrolls")]
        [TestCase(SkillConstants.Diplomacy, null, SkillConstants.Profession, SkillConstants.Foci.Profession.Barrister, null)]
        [TestCase(SkillConstants.Diplomacy, null, SkillConstants.Profession, SkillConstants.Foci.Profession.Butler, null)]
        [TestCase(SkillConstants.Diplomacy, null, SkillConstants.Profession, SkillConstants.Foci.Profession.CityGuide, null)]
        [TestCase(SkillConstants.Diplomacy, null, SkillConstants.Profession, SkillConstants.Foci.Profession.Footman, null)]
        [TestCase(SkillConstants.Diplomacy, null, SkillConstants.Profession, SkillConstants.Foci.Profession.Governess, null)]
        [TestCase(SkillConstants.Diplomacy, null, SkillConstants.Profession, SkillConstants.Foci.Profession.Matchmaker, null)]
        [TestCase(SkillConstants.Diplomacy, null, SkillConstants.Profession, SkillConstants.Foci.Profession.Valet, null)]
        [TestCase(SkillConstants.EscapeArtist, null, SkillConstants.UseRope, null, "binding someone")]
        [TestCase(SkillConstants.HandleAnimal, null, SkillConstants.Diplomacy, null, "wild empathy")]
        [TestCase(SkillConstants.HandleAnimal, null, SkillConstants.Profession, SkillConstants.Foci.Profession.AnimalGroomer, null)]
        [TestCase(SkillConstants.HandleAnimal, null, SkillConstants.Profession, SkillConstants.Foci.Profession.AnimalTrainer, null)]
        [TestCase(SkillConstants.HandleAnimal, null, SkillConstants.Profession, SkillConstants.Foci.Profession.ExoticAnimalTrainer, null)]
        [TestCase(SkillConstants.HandleAnimal, null, SkillConstants.Profession, SkillConstants.Foci.Profession.Shepherd, null)]
        [TestCase(SkillConstants.HandleAnimal, null, SkillConstants.Profession, SkillConstants.Foci.Profession.Teamster, null)]
        [TestCase(SkillConstants.HandleAnimal, null, SkillConstants.Ride, null, null)]
        [TestCase(SkillConstants.Heal, null, SkillConstants.Profession, SkillConstants.Foci.Profession.Healer, null)]
        [TestCase(SkillConstants.Heal, null, SkillConstants.Profession, SkillConstants.Foci.Profession.Masseuse, null)]
        [TestCase(SkillConstants.Heal, null, SkillConstants.Profession, SkillConstants.Foci.Profession.Midwife, null)]
        [TestCase(SkillConstants.Intimidate, null, SkillConstants.Profession, SkillConstants.Foci.Profession.SailorMate, null)]
        [TestCase(SkillConstants.Jump, null, SkillConstants.Tumble, null, null)]
        [TestCase(SkillConstants.Knowledge, null, SkillConstants.Profession, SkillConstants.Foci.Profession.Adviser, null)]
        [TestCase(SkillConstants.Knowledge, null, SkillConstants.Profession, SkillConstants.Foci.Profession.Sage, null)]
        [TestCase(SkillConstants.Knowledge, null, SkillConstants.Profession, SkillConstants.Foci.Profession.Teacher, null)]
        [TestCase(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Arcana, SkillConstants.Spellcraft, null, null)]
        [TestCase(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.ArchitectureAndEngineering, SkillConstants.Profession, SkillConstants.Foci.Profession.Architect, null)]
        [TestCase(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.ArchitectureAndEngineering, SkillConstants.Profession, SkillConstants.Foci.Profession.Engineer, null)]
        [TestCase(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.ArchitectureAndEngineering, SkillConstants.Search, null, "find secret doors or compartments")]
        [TestCase(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Dungeoneering, SkillConstants.Profession, SkillConstants.Foci.Profession.Miner, null)]
        [TestCase(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Dungeoneering, SkillConstants.Survival, null, "underground")]
        [TestCase(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Geography, SkillConstants.Profession, SkillConstants.Foci.Profession.Miner, null)]
        [TestCase(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Geography, SkillConstants.Survival, null, "keep from getting lost or avoid natural hazards")]
        [TestCase(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.History, SkillConstants.Knowledge, "bardic", null)]
        [TestCase(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Local, SkillConstants.GatherInformation, null, null)]
        [TestCase(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Local, SkillConstants.Profession, SkillConstants.Foci.Profession.CityGuide, null)]
        [TestCase(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Local, SkillConstants.Profession, SkillConstants.Foci.Profession.LocalCourier, null)]
        [TestCase(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Local, SkillConstants.Profession, SkillConstants.Foci.Profession.OutOfTownCourier, null)]
        [TestCase(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Local, SkillConstants.Profession, SkillConstants.Foci.Profession.WildernessGuide, null)]
        [TestCase(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Nature, SkillConstants.Profession, SkillConstants.Foci.Profession.Apothecary, null)]
        [TestCase(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Nature, SkillConstants.Profession, SkillConstants.Foci.Profession.Farmer, null)]
        [TestCase(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Nature, SkillConstants.Profession, SkillConstants.Foci.Profession.Hunter, null)]
        [TestCase(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Nature, SkillConstants.Profession, SkillConstants.Foci.Profession.Miner, null)]
        [TestCase(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Nature, SkillConstants.Profession, SkillConstants.Foci.Profession.Trapper, null)]
        [TestCase(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Nature, SkillConstants.Profession, SkillConstants.Foci.Profession.WildernessGuide, null)]
        [TestCase(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Nature, SkillConstants.Survival, null, "in aboveground natural environments")]
        [TestCase(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.NobilityAndRoyalty, SkillConstants.Diplomacy, null, null)]
        [TestCase(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.NobilityAndRoyalty, SkillConstants.Profession, SkillConstants.Foci.Profession.Butler, null)]
        [TestCase(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.NobilityAndRoyalty, SkillConstants.Profession, SkillConstants.Foci.Profession.Footman, null)]
        [TestCase(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.NobilityAndRoyalty, SkillConstants.Profession, SkillConstants.Foci.Profession.Governess, null)]
        [TestCase(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.NobilityAndRoyalty, SkillConstants.Profession, SkillConstants.Foci.Profession.Maid, null)]
        [TestCase(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.ThePlanes, SkillConstants.Survival, null, "on other planes")]
        [TestCase(SkillConstants.Perform, null, SkillConstants.Profession, SkillConstants.Foci.Profession.Entertainer, null)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Adviser, SkillConstants.Diplomacy, null, null)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Adviser, SkillConstants.Knowledge, null, null)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Alchemist, SkillConstants.Craft, SkillConstants.Foci.Craft.Alchemy, null)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.AnimalGroomer, SkillConstants.HandleAnimal, null, null)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.AnimalTrainer, SkillConstants.HandleAnimal, null, null)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Apothecary, SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Nature, null)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Appraiser, SkillConstants.Appraise, null, null)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Architect, SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.ArchitectureAndEngineering, null)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Armorer, SkillConstants.Craft, SkillConstants.Foci.Craft.Armorsmithing, null)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Barrister, SkillConstants.Diplomacy, null, null)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Blacksmith, SkillConstants.Craft, SkillConstants.Foci.Craft.Blacksmithing, null)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Bookbinder, SkillConstants.Craft, SkillConstants.Foci.Craft.Bookbinding, null)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Bowyer, SkillConstants.Craft, SkillConstants.Foci.Craft.Bowmaking, null)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Brazier, SkillConstants.Craft, SkillConstants.Foci.Craft.Brassmaking, null)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Brewer, SkillConstants.Craft, SkillConstants.Foci.Craft.Brewing, null)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Butler, SkillConstants.Diplomacy, null, null)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Butler, SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.NobilityAndRoyalty, null)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Carpenter, SkillConstants.Craft, SkillConstants.Foci.Craft.Woodworking, null)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Cartographer, SkillConstants.Craft, SkillConstants.Foci.Craft.Mapmaking, null)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Cartwright, SkillConstants.Craft, SkillConstants.Foci.Craft.Woodworking, null)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Chandler, SkillConstants.Craft, SkillConstants.Foci.Craft.Candlemaking, null)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.CityGuide, SkillConstants.Diplomacy, null, null)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.CityGuide, SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Local, null)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Cobbler, SkillConstants.Craft, SkillConstants.Foci.Craft.Shoemaking, null)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Coffinmaker, SkillConstants.Craft, SkillConstants.Foci.Craft.Woodworking, null)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Coppersmith, SkillConstants.Craft, SkillConstants.Foci.Craft.Coppersmithing, null)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Craftsman, SkillConstants.Craft, null, null)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Dowser, SkillConstants.Bluff, null, null)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Dowser, SkillConstants.Survival, null, null)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Dyer, SkillConstants.Craft, SkillConstants.Foci.Craft.Dyemaking, null)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Embalmer, SkillConstants.Craft, SkillConstants.Foci.Craft.Alchemy, null)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Engineer, SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.ArchitectureAndEngineering, null)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Entertainer, SkillConstants.Perform, null, null)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.ExoticAnimalTrainer, SkillConstants.HandleAnimal, null, null)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Farmer, SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Nature, null)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Fletcher, SkillConstants.Craft, SkillConstants.Foci.Craft.Bowmaking, null)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Footman, SkillConstants.Diplomacy, null, null)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Footman, SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.NobilityAndRoyalty, null)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Gemcutter, SkillConstants.Craft, SkillConstants.Foci.Craft.Gemcutting, null)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Goldsmith, SkillConstants.Craft, SkillConstants.Foci.Craft.Goldsmithing, null)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Governess, SkillConstants.Diplomacy, null, null)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Governess, SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.NobilityAndRoyalty, null)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Haberdasher, SkillConstants.Craft, SkillConstants.Foci.Craft.Hatmaking, null)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Healer, SkillConstants.Heal, null, null)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Horner, SkillConstants.Craft, SkillConstants.Foci.Craft.Hornworking, null)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Hunter, SkillConstants.Craft, SkillConstants.Foci.Craft.Trapmaking, null)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Hunter, SkillConstants.Survival, null, null)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Jeweler, SkillConstants.Craft, SkillConstants.Foci.Craft.Jewelmaking, null)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Limner, SkillConstants.Craft, SkillConstants.Foci.Craft.Painting, null)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.LocalCourier, SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Local, null)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Locksmith, SkillConstants.Craft, SkillConstants.Foci.Craft.Locksmithing, null)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Maid, SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.NobilityAndRoyalty, null)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Masseuse, SkillConstants.Heal, null, null)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Matchmaker, SkillConstants.Diplomacy, null, null)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Matchmaker, SkillConstants.SenseMotive, null, null)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Midwife, SkillConstants.Heal, null, null)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Miller, SkillConstants.Craft, SkillConstants.Foci.Craft.Milling, null)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Miner, SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Geography, null)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Miner, SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Nature, null)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Miner, SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Dungeoneering, null)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Navigator, SkillConstants.Survival, null, null)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.OutOfTownCourier, SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Local, null)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.OutOfTownCourier, SkillConstants.Ride, null, null)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Painter, SkillConstants.Craft, SkillConstants.Foci.Craft.Painting, null)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Parchmentmaker, SkillConstants.Craft, SkillConstants.Foci.Craft.Parchmentmaking, null)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Pewterer, SkillConstants.Craft, SkillConstants.Foci.Craft.Pewtermaking, null)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Potter, SkillConstants.Craft, SkillConstants.Foci.Craft.Potterymaking, null)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Sage, SkillConstants.Knowledge, null, null)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.SailorCrewmember, SkillConstants.Swim, null, null)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.SailorMate, SkillConstants.Intimidate, null, null)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.SailorMate, SkillConstants.Swim, null, null)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Sculptor, SkillConstants.Craft, SkillConstants.Foci.Craft.Sculpting, null)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Shepherd, SkillConstants.HandleAnimal, null, null)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Shipwright, SkillConstants.Craft, SkillConstants.Foci.Craft.Shipmaking, null)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Silversmith, SkillConstants.Craft, SkillConstants.Foci.Craft.Silversmithing, null)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Skinner, SkillConstants.Craft, SkillConstants.Foci.Craft.Skinning, null)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Soapmaker, SkillConstants.Craft, SkillConstants.Foci.Craft.Soapmaking, null)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Soothsayer, SkillConstants.Bluff, null, null)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Tanner, SkillConstants.Craft, SkillConstants.Foci.Craft.Tanning, null)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Teacher, SkillConstants.Knowledge, null, null)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Teamster, SkillConstants.HandleAnimal, null, null)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Teamster, SkillConstants.Ride, null, null)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Trader, SkillConstants.Appraise, null, null)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Trader, SkillConstants.SenseMotive, null, null)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Trapper, SkillConstants.Craft, SkillConstants.Foci.Craft.Trapmaking, null)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Trapper, SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Nature, null)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Trapper, SkillConstants.Survival, null, null)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Valet, SkillConstants.Diplomacy, null, null)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Vintner, SkillConstants.Craft, SkillConstants.Foci.Craft.Winemaking, null)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Weaponsmith, SkillConstants.Craft, SkillConstants.Foci.Craft.Weaponsmithing, null)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Weaver, SkillConstants.Craft, SkillConstants.Foci.Craft.Weaving, null)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.Wheelwright, SkillConstants.Craft, SkillConstants.Foci.Craft.Wheelmaking, null)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.WildernessGuide, SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Local, null)]
        [TestCase(SkillConstants.Profession, SkillConstants.Foci.Profession.WildernessGuide, SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Nature, null)]
        [TestCase(SkillConstants.Ride, null, SkillConstants.Profession, SkillConstants.Foci.Profession.OutOfTownCourier, null)]
        [TestCase(SkillConstants.Ride, null, SkillConstants.Profession, SkillConstants.Foci.Profession.Teamster, null)]
        [TestCase(SkillConstants.Search, null, SkillConstants.Survival, null, "following tracks")]
        [TestCase(SkillConstants.SenseMotive, null, SkillConstants.Diplomacy, null, null)]
        [TestCase(SkillConstants.SenseMotive, null, SkillConstants.Profession, SkillConstants.Foci.Profession.Matchmaker, null)]
        [TestCase(SkillConstants.SenseMotive, null, SkillConstants.Profession, SkillConstants.Foci.Profession.Trader, null)]
        [TestCase(SkillConstants.Spellcraft, null, SkillConstants.UseMagicDevice, null, "with scrolls")]
        [TestCase(SkillConstants.Survival, null, SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Nature, null)]
        [TestCase(SkillConstants.Survival, null, SkillConstants.Profession, SkillConstants.Foci.Profession.Dowser, null)]
        [TestCase(SkillConstants.Survival, null, SkillConstants.Profession, SkillConstants.Foci.Profession.Hunter, null)]
        [TestCase(SkillConstants.Survival, null, SkillConstants.Profession, SkillConstants.Foci.Profession.Navigator, null)]
        [TestCase(SkillConstants.Survival, null, SkillConstants.Profession, SkillConstants.Foci.Profession.Trapper, null)]
        [TestCase(SkillConstants.Swim, null, SkillConstants.Profession, SkillConstants.Foci.Profession.SailorCrewmember, null)]
        [TestCase(SkillConstants.Swim, null, SkillConstants.Profession, SkillConstants.Foci.Profession.SailorMate, null)]
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
    }
}