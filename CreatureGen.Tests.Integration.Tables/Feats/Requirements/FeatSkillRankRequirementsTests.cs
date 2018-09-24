using CreatureGen.Feats;
using CreatureGen.Skills;
using CreatureGen.Tables;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CreatureGen.Tests.Integration.Tables.Feats.Requirements
{
    [TestFixture]
    public class FeatSkillRankRequirementsTests : TypesAndAmountsTests
    {
        protected override string tableName
        {
            get
            {
                return TableNameConstants.TypeAndAmount.FeatSkillRankRequirements;
            }
        }

        [Test]
        public void CollectionNames()
        {
            var names = GetNames();

            AssertCollectionNames(names);
        }

        private IEnumerable<string> GetNames()
        {
            var feats = FeatConstants.All();
            var metamagic = FeatConstants.Metamagic.All();
            var monster = FeatConstants.Monster.All();
            var craft = FeatConstants.MagicItemCreation.All();

            var names = feats.Union(metamagic).Union(monster).Union(craft);

            var skillSynergyData = CollectionMapper.Map(TableNameConstants.Collection.SkillSynergyFeatData);

            return names.Union(skillSynergyData.Keys);
        }

        [TestCaseSource(typeof(SkillRankRequirementsTestData), "Feats")]
        [TestCaseSource(typeof(SkillRankRequirementsTestData), "Metamagic")]
        [TestCaseSource(typeof(SkillRankRequirementsTestData), "Monster")]
        [TestCaseSource(typeof(SkillRankRequirementsTestData), "Craft")]
        [TestCaseSource(typeof(SkillRankRequirementsTestData), "SkillSynergy")]
        public void SkillRankRequirements(string name, Dictionary<string, int> typesAndAmounts)
        {
            AssertTypesAndAmounts(name, typesAndAmounts);
        }

        public class SkillRankRequirementsTestData
        {
            public static IEnumerable Feats
            {
                get
                {
                    var testCases = new Dictionary<string, Dictionary<string, int>>();
                    var feats = FeatConstants.All();

                    foreach (var feat in feats)
                    {
                        testCases[feat] = new Dictionary<string, int>();
                    }

                    testCases[FeatConstants.MountedArchery][SkillConstants.Ride] = 1;
                    testCases[FeatConstants.MountedCombat][SkillConstants.Ride] = 1;
                    testCases[FeatConstants.RideByAttack][SkillConstants.Ride] = 1;
                    testCases[FeatConstants.SpiritedCharge][SkillConstants.Ride] = 1;
                    testCases[FeatConstants.Trample][SkillConstants.Ride] = 1;

                    foreach (var testCase in testCases)
                    {
                        var requirements = testCase.Value.Select(kvp => $"{kvp.Key}:{kvp.Value}");
                        yield return new TestCaseData(testCase.Key, testCase.Value)
                            .SetName($"SkillRankRequirements({testCase.Key}, [{string.Join("], [", requirements)}])");
                    }
                }
            }

            public static IEnumerable Metamagic
            {
                get
                {
                    var testCases = new Dictionary<string, Dictionary<string, int>>();
                    var feats = FeatConstants.Metamagic.All();

                    foreach (var feat in feats)
                    {
                        testCases[feat] = new Dictionary<string, int>();
                    }

                    foreach (var testCase in testCases)
                    {
                        var requirements = testCase.Value.Select(kvp => $"{kvp.Key}:{kvp.Value}");
                        yield return new TestCaseData(testCase.Key, testCase.Value)
                            .SetName($"SkillRankRequirements({testCase.Key}, [{string.Join("], [", requirements)}])");
                    }
                }
            }

            public static IEnumerable Monster
            {
                get
                {
                    var testCases = new Dictionary<string, Dictionary<string, int>>();
                    var feats = FeatConstants.Monster.All();

                    foreach (var feat in feats)
                    {
                        testCases[feat] = new Dictionary<string, int>();
                    }

                    foreach (var testCase in testCases)
                    {
                        var requirements = testCase.Value.Select(kvp => $"{kvp.Key}:{kvp.Value}");
                        yield return new TestCaseData(testCase.Key, testCase.Value)
                            .SetName($"SkillRankRequirements({testCase.Key}, [{string.Join("], [", requirements)}])");
                    }
                }
            }

            public static IEnumerable Craft
            {
                get
                {
                    var testCases = new Dictionary<string, Dictionary<string, int>>();
                    var feats = FeatConstants.MagicItemCreation.All();

                    foreach (var feat in feats)
                    {
                        testCases[feat] = new Dictionary<string, int>();
                    }

                    foreach (var testCase in testCases)
                    {
                        var requirements = testCase.Value.Select(kvp => $"{kvp.Key}:{kvp.Value}");
                        yield return new TestCaseData(testCase.Key, testCase.Value)
                            .SetName($"SkillRankRequirements({testCase.Key}, [{string.Join("], [", requirements)}])");
                    }
                }
            }

            public static IEnumerable SkillSynergy
            {
                get
                {
                    var testCases = new Dictionary<string, Dictionary<string, int>>();

                    var keys = new[]
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

                    foreach (var key in keys)
                    {
                        testCases[key] = new Dictionary<string, int>();

                        var sections = key.Split(':');
                        testCases[key][sections[0]] = 5;

                        if (!sections[1].Contains("bardic"))
                            testCases[key][sections[1]] = 0;
                    }

                    foreach (var testCase in testCases)
                    {
                        var requirements = testCase.Value.Select(kvp => $"{kvp.Key}:{kvp.Value}");
                        yield return new TestCaseData(testCase.Key, testCase.Value)
                            .SetName($"SkillRankRequirements({testCase.Key}, [{string.Join("], [", requirements)}])");
                    }
                }
            }
        }
    }
}
