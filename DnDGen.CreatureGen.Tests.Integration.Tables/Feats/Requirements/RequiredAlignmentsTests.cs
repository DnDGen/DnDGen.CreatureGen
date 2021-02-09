using DnDGen.CreatureGen.Alignments;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Feats;
using DnDGen.CreatureGen.Magics;
using DnDGen.CreatureGen.Selectors.Helpers;
using DnDGen.CreatureGen.Tables;
using DnDGen.CreatureGen.Tests.Integration.Tables.Feats.Data;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;

namespace DnDGen.CreatureGen.Tests.Integration.Tables.Feats.Requirements
{
    [TestFixture]
    public class RequiredAlignmentsTests : CollectionTests
    {
        protected override string tableName => TableNameConstants.Collection.RequiredAlignments;

        [Test]
        public void RequiredAlignmentsNames()
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
            var specialQualities = SpecialQualityTestData.GetRequirementKeys();

            var names = new List<string>();
            names.AddRange(feats);
            names.AddRange(metamagic);
            names.AddRange(monster);
            names.AddRange(craft);
            names.AddRange(specialQualities);

            return names;
        }

        [TestCaseSource(typeof(RequiredAlignmentsTestData), nameof(RequiredAlignmentsTestData.Feats))]
        [TestCaseSource(typeof(RequiredAlignmentsTestData), nameof(RequiredAlignmentsTestData.Metamagic))]
        [TestCaseSource(typeof(RequiredAlignmentsTestData), nameof(RequiredAlignmentsTestData.Monster))]
        [TestCaseSource(typeof(RequiredAlignmentsTestData), nameof(RequiredAlignmentsTestData.Craft))]
        [TestCaseSource(typeof(RequiredAlignmentsTestData), nameof(RequiredAlignmentsTestData.SpecialQualities))]
        public void RequiredAlignments(string name, params string[] alignments)
        {
            AssertCollection(name, alignments);
        }

        public class RequiredAlignmentsTestData
        {
            public static IEnumerable Feats
            {
                get
                {
                    var testCases = new Dictionary<string, string[]>();
                    var feats = FeatConstants.All();

                    foreach (var feat in feats)
                    {
                        testCases[feat] = new string[0];
                    }

                    foreach (var testCase in testCases)
                    {
                        yield return new TestCaseData(testCase.Key, testCase.Value);
                    }
                }
            }

            public static IEnumerable Metamagic
            {
                get
                {
                    var testCases = new Dictionary<string, string[]>();
                    var feats = FeatConstants.Metamagic.All();

                    foreach (var feat in feats)
                    {
                        testCases[feat] = new string[0];
                    }

                    foreach (var testCase in testCases)
                    {
                        yield return new TestCaseData(testCase.Key, testCase.Value);
                    }
                }
            }

            public static IEnumerable Monster
            {
                get
                {
                    var testCases = new Dictionary<string, string[]>();
                    var feats = FeatConstants.Monster.All();

                    foreach (var feat in feats)
                    {
                        testCases[feat] = new string[0];
                    }

                    foreach (var testCase in testCases)
                    {
                        yield return new TestCaseData(testCase.Key, testCase.Value);
                    }
                }
            }

            public static IEnumerable Craft
            {
                get
                {
                    var testCases = new Dictionary<string, string[]>();
                    var feats = FeatConstants.MagicItemCreation.All();

                    foreach (var feat in feats)
                    {
                        testCases[feat] = new string[0];
                    }

                    foreach (var testCase in testCases)
                    {
                        yield return new TestCaseData(testCase.Key, testCase.Value);
                    }
                }
            }

            public static IEnumerable SpecialQualities
            {
                get
                {
                    var testCases = new Dictionary<string, string[]>();
                    var helper = new SpecialQualityHelper();
                    var keys = SpecialQualityTestData.GetRequirementKeys();

                    foreach (var key in keys)
                    {
                        testCases[key] = new string[0];
                    }

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Titan, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.BestowCurse, 0.ToString())] = new[] { AlignmentConstants.ChaoticEvil };
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Titan, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.CrushingHand, 0.ToString())] = new[] { AlignmentConstants.ChaoticEvil };
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Titan, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Daylight, 0.ToString())] = new[] { AlignmentConstants.ChaoticGood, AlignmentConstants.ChaoticNeutral };
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Titan, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DeeperDarkness, 0.ToString())] = new[] { AlignmentConstants.ChaoticEvil };
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Titan, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.HolySmite, 0.ToString())] = new[] { AlignmentConstants.ChaoticGood, AlignmentConstants.ChaoticNeutral };
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Titan, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.RemoveCurse, 0.ToString())] = new[] { AlignmentConstants.ChaoticGood, AlignmentConstants.ChaoticNeutral };
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Titan, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Restoration_Greater, 0.ToString())] = new[] { AlignmentConstants.ChaoticGood, AlignmentConstants.ChaoticNeutral };
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Titan, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.UnholyBlight, 0.ToString())] = new[] { AlignmentConstants.ChaoticEvil };

                    foreach (var testCase in testCases)
                    {
                        yield return new TestCaseData(testCase.Key, testCase.Value);
                    }
                }
            }
        }
    }
}
