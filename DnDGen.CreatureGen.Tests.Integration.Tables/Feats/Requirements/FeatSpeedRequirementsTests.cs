using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Feats;
using DnDGen.CreatureGen.Tables;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Integration.Tables.Feats.Requirements
{
    [TestFixture]
    public class FeatSpeedRequirementsTests : TypesAndAmountsTests
    {
        protected override string tableName => TableNameConstants.TypeAndAmount.FeatSpeedRequirements;

        [Test]
        public void FeatSpeedRequirementsNames()
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

            return names;
        }

        [TestCaseSource(typeof(SpeedRequirementsTestData), nameof(SpeedRequirementsTestData.Feats))]
        [TestCaseSource(typeof(SpeedRequirementsTestData), nameof(SpeedRequirementsTestData.Metamagic))]
        [TestCaseSource(typeof(SpeedRequirementsTestData), nameof(SpeedRequirementsTestData.Monster))]
        [TestCaseSource(typeof(SpeedRequirementsTestData), nameof(SpeedRequirementsTestData.Craft))]
        public void SpeedRequirements(string name, Dictionary<string, int> typesAndAmounts)
        {
            AssertTypesAndAmounts(name, typesAndAmounts);
        }

        [Test]
        public void NoSpeedRequirements()
        {
            var names = GetNames();
            var monsters = SpeedRequirementsTestData.GetMonsterSpeedRequirementNames();

            var emptyRequirements = names.Except(monsters);

            foreach (var requirement in emptyRequirements)
            {
                var empty = new Dictionary<string, int>();
                AssertTypesAndAmounts(requirement, empty);
            }
        }

        public class SpeedRequirementsTestData
        {
            public static IEnumerable Feats
            {
                get
                {
                    var testCases = new Dictionary<string, Dictionary<string, int>>();

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
                    var testCases = new Dictionary<string, Dictionary<string, int>>();

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
                    var testCases = new Dictionary<string, Dictionary<string, int>>();
                    var feats = GetMonsterSpeedRequirementNames();

                    foreach (var feat in feats)
                    {
                        testCases[feat] = new Dictionary<string, int>();
                    }

                    testCases[FeatConstants.Monster.FlybyAttack][SpeedConstants.Fly] = 1;
                    testCases[FeatConstants.Monster.FlybyAttack_Improved][SpeedConstants.Fly] = 1;
                    testCases[FeatConstants.Monster.Hover][SpeedConstants.Fly] = 1;
                    testCases[FeatConstants.Monster.Wingover][SpeedConstants.Fly] = 1;

                    foreach (var testCase in testCases)
                    {
                        yield return new TestCaseData(testCase.Key, testCase.Value);
                    }
                }
            }

            public static IEnumerable<string> GetMonsterSpeedRequirementNames()
            {
                return new[]
                {
                    FeatConstants.Monster.FlybyAttack,
                    FeatConstants.Monster.FlybyAttack_Improved,
                    FeatConstants.Monster.Hover,
                    FeatConstants.Monster.Wingover,
                };
            }

            public static IEnumerable Craft
            {
                get
                {
                    var testCases = new Dictionary<string, Dictionary<string, int>>();

                    foreach (var testCase in testCases)
                    {
                        yield return new TestCaseData(testCase.Key, testCase.Value);
                    }
                }
            }
        }
    }
}
