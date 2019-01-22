using CreatureGen.Creatures;
using CreatureGen.Feats;
using CreatureGen.Selectors.Helpers;
using CreatureGen.Tables;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CreatureGen.Tests.Integration.Tables.Feats.Requirements
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

            var specialQualityData = CollectionMapper.Map(TableNameConstants.Collection.SpecialQualityData);
            var specialQualities = specialQualityData
                .SelectMany(kvp => kvp.Value.Select(v => kvp.Key + v))
                .Select(q => SpecialQualityHelper.ParseData(q))
                .Select(q => q[DataIndexConstants.SpecialQualityData.FeatNameIndex] + q[DataIndexConstants.SpecialQualityData.FocusIndex]);

            var names = feats.Union(metamagic).Union(monster).Union(craft).Union(specialQualities);

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

                    foreach (var testCase in testCases)
                    {
                        yield return new TestCaseData(testCase.Key, testCase.Value);
                    }
                }
            }
        }
    }
}
