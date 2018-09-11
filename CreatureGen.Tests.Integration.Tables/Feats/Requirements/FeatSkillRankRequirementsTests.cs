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

                    foreach (var key in keys)
                    {
                        testCases[key] = new Dictionary<string, int>();

                        var sections = key.Split(':');
                        testCases[key][sections[0]] = 5;
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
