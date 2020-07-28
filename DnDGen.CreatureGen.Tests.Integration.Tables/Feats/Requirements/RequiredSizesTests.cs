using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Feats;
using DnDGen.CreatureGen.Selectors.Helpers;
using DnDGen.CreatureGen.Tables;
using DnDGen.CreatureGen.Tests.Integration.Tables.Feats.Data;
using DnDGen.TreasureGen.Items;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;

namespace DnDGen.CreatureGen.Tests.Integration.Tables.Feats.Requirements
{
    [TestFixture]
    public class RequiredSizesTests : CollectionTests
    {
        protected override string tableName => TableNameConstants.Collection.RequiredSizes;

        [Test]
        public void RequiredSizesNames()
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

        [TestCaseSource(typeof(RequiredSizesTestData), "Feats")]
        [TestCaseSource(typeof(RequiredSizesTestData), "Metamagic")]
        [TestCaseSource(typeof(RequiredSizesTestData), "Monster")]
        [TestCaseSource(typeof(RequiredSizesTestData), "Craft")]
        [TestCaseSource(typeof(RequiredSizesTestData), "SpecialQualities")]
        public void RequiredSizes(string name, params string[] sizes)
        {
            AssertCollection(name, sizes);
        }

        public class RequiredSizesTestData
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

                    testCases[FeatConstants.Monster.AwesomeBlow] = new[] { SizeConstants.Large, SizeConstants.Huge, SizeConstants.Gargantuan, SizeConstants.Colossal };
                    testCases[FeatConstants.Monster.Snatch] = new[] { SizeConstants.Huge, SizeConstants.Gargantuan, SizeConstants.Colossal };

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

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Giant_Cloud, FeatConstants.SpecialQualities.OversizedWeapon, SizeConstants.Gargantuan, 0.ToString())] = new[] { SizeConstants.Huge };

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Titan, FeatConstants.SpecialQualities.OversizedWeapon, SizeConstants.Gargantuan, 0.ToString())] = new[] { SizeConstants.Huge };
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Titan, FeatConstants.SpecialQualities.OversizedWeapon, SizeConstants.Colossal, 0.ToString())] = new[] { SizeConstants.Gargantuan };

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Subtypes.Swarm, FeatConstants.SpecialQualities.HalfDamage, AttributeConstants.DamageTypes.Piercing, 0.ToString())] = new[] { SizeConstants.Tiny };
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Subtypes.Swarm, FeatConstants.SpecialQualities.HalfDamage, AttributeConstants.DamageTypes.Slashing, 0.ToString())] = new[] { SizeConstants.Tiny };
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Subtypes.Swarm, FeatConstants.SpecialQualities.Immunity, "Weapon damage", 0.ToString())] = new[] { SizeConstants.Diminutive, SizeConstants.Fine };
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Subtypes.Swarm, FeatConstants.SpecialQualities.Vulnerability, "High winds", 0.ToString())] = new[] { SizeConstants.Diminutive, SizeConstants.Fine };

                    foreach (var testCase in testCases)
                    {
                        yield return new TestCaseData(testCase.Key, testCase.Value);
                    }
                }
            }
        }
    }
}
