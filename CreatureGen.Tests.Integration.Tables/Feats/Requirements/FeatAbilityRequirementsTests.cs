using CreatureGen.Abilities;
using CreatureGen.Feats;
using CreatureGen.Tables;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CreatureGen.Tests.Integration.Tables.Feats.Requirements
{
    [TestFixture]
    public class FeatAbilityRequirementsTests : TypesAndAmountsTests
    {
        protected override string tableName
        {
            get
            {
                return TableNameConstants.Set.TypeAndAmount.FeatAbilityRequirements;
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

            return names;
        }

        [Test]
        [TestCaseSource(typeof(AbilityRequirementsTestData), "Feats")]
        [TestCaseSource(typeof(AbilityRequirementsTestData), "Metamagic")]
        [TestCaseSource(typeof(AbilityRequirementsTestData), "Monster")]
        [TestCaseSource(typeof(AbilityRequirementsTestData), "Craft")]
        public void AbilityRequirements(string name, Dictionary<string, int> typesAndAmounts)
        {
            AssertTypesAndAmounts(name, typesAndAmounts);
        }

        private class AbilityRequirementsTestData
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

                    testCases[FeatConstants.BullRush_Improved][AbilityConstants.Strength] = 13;
                    testCases[FeatConstants.Cleave][AbilityConstants.Strength] = 13;
                    testCases[FeatConstants.Cleave_Great][AbilityConstants.Strength] = 13;
                    testCases[FeatConstants.CombatExpertise][AbilityConstants.Intelligence] = 13;
                    testCases[FeatConstants.DeflectArrows][AbilityConstants.Dexterity] = 13;
                    testCases[FeatConstants.Disarm_Improved][AbilityConstants.Intelligence] = 13;
                    testCases[FeatConstants.Dodge][AbilityConstants.Dexterity] = 13;
                    testCases[FeatConstants.Feint_Improved][AbilityConstants.Intelligence] = 13;
                    testCases[FeatConstants.Grapple_Improved][AbilityConstants.Dexterity] = 13;
                    testCases[FeatConstants.Manyshot][AbilityConstants.Dexterity] = 17;
                    testCases[FeatConstants.Mobility][AbilityConstants.Dexterity] = 13;
                    testCases[FeatConstants.NaturalSpell][AbilityConstants.Wisdom] = 13;
                    testCases[FeatConstants.Overrun_Improved][AbilityConstants.Strength] = 13;
                    testCases[FeatConstants.PowerAttack][AbilityConstants.Strength] = 13;
                    testCases[FeatConstants.PreciseShot_Improved][AbilityConstants.Dexterity] = 19;
                    testCases[FeatConstants.RapidShot][AbilityConstants.Dexterity] = 13;
                    testCases[FeatConstants.ShotOnTheRun][AbilityConstants.Dexterity] = 13;
                    testCases[FeatConstants.SnatchArrows][AbilityConstants.Dexterity] = 15;
                    testCases[FeatConstants.SpringAttack][AbilityConstants.Dexterity] = 13;
                    testCases[FeatConstants.StunningFist][AbilityConstants.Dexterity] = 13;
                    testCases[FeatConstants.StunningFist][AbilityConstants.Wisdom] = 13;
                    testCases[FeatConstants.Sunder_Improved][AbilityConstants.Strength] = 13;
                    testCases[FeatConstants.Trip_Improved][AbilityConstants.Intelligence] = 13;
                    testCases[FeatConstants.TwoWeaponDefense][AbilityConstants.Dexterity] = 15;
                    testCases[FeatConstants.TwoWeaponFighting][AbilityConstants.Dexterity] = 15;
                    testCases[FeatConstants.TwoWeaponFighting_Greater][AbilityConstants.Dexterity] = 19;
                    testCases[FeatConstants.TwoWeaponFighting_Improved][AbilityConstants.Dexterity] = 17;
                    testCases[FeatConstants.WhirlwindAttack][AbilityConstants.Dexterity] = 13;
                    testCases[FeatConstants.WhirlwindAttack][AbilityConstants.Intelligence] = 13;

                    foreach (var testCase in testCases)
                    {
                        var requirements = testCase.Value.Select(kvp => $"{kvp.Key}:{kvp.Value}");
                        yield return new TestCaseData(testCase.Key, testCase.Value)
                            .SetName($"AbilityRequirements({testCase.Key}, [{string.Join("], [", requirements)}])");
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
                            .SetName($"AbilityRequirements({testCase.Key}, [{string.Join("], [", requirements)}])");
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

                    testCases[FeatConstants.Monster.AwesomeBlow][AbilityConstants.Strength] = 25;
                    testCases[FeatConstants.Monster.MultiweaponFighting][AbilityConstants.Dexterity] = 13;
                    testCases[FeatConstants.Monster.MultiweaponFighting_Greater][AbilityConstants.Dexterity] = 19;
                    testCases[FeatConstants.Monster.MultiweaponFighting_Improved][AbilityConstants.Dexterity] = 15;
                    testCases[FeatConstants.Monster.NaturalArmor_Improved][AbilityConstants.Constitution] = 13;

                    foreach (var testCase in testCases)
                    {
                        var requirements = testCase.Value.Select(kvp => $"{kvp.Key}:{kvp.Value}");
                        yield return new TestCaseData(testCase.Key, testCase.Value)
                            .SetName($"AbilityRequirements({testCase.Key}, [{string.Join("], [", requirements)}])");
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
                            .SetName($"AbilityRequirements({testCase.Key}, [{string.Join("], [", requirements)}])");
                    }
                }
            }
        }
    }
}
