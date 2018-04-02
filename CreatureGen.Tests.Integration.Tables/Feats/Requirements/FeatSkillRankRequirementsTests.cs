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
                return TableNameConstants.Set.TypeAndAmount.FeatSkillRankRequirements;
            }
        }

        [Test]
        public void CollectionNames()
        {
            var names = GetNames();

            AssertCollectionNames(names);
        }

        public static IEnumerable<string> GetNames()
        {
            var feats = FeatConstants.All();
            var metamagic = FeatConstants.Metamagic.All();
            var monster = FeatConstants.Monster.All();
            var craft = FeatConstants.MagicItemCreation.All();

            var names = feats.Union(metamagic).Union(monster).Union(craft);

            return names;
        }

        [Test]
        [TestCaseSource(typeof(AbilityRequirementsTestData), "TestCases")]
        public void AbilityRequirements(string name, Dictionary<string, int> typesAndAmounts)
        {
            AssertTypesAndAmounts(name, typesAndAmounts);
        }

        private class AbilityRequirementsTestData
        {
            public static IEnumerable TestCases
            {
                get
                {
                    var testCases = new Dictionary<string, Dictionary<string, int>>();
                    var feats = GetNames();

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
                            .SetName($"AbilityRequirements({testCase.Key}, [{string.Join("], [", requirements)}])");
                    }
                }
            }
        }
    }
}
