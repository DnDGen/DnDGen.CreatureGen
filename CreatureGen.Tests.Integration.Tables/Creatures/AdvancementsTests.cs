using CreatureGen.Creatures;
using CreatureGen.Tables;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CreatureGen.Tests.Integration.Tables.Creatures
{
    [TestFixture]
    public class AdvancementsTests : TypesAndAmountsTests
    {
        protected override string tableName => TableNameConstants.TypeAndAmount.Advancements;

        [Test]
        public void CollectionNames()
        {
            var creatures = CreatureConstants.All();
            var creatureTypes = CreatureConstants.Types.All();

            var names = creatures.Union(creatureTypes);

            AssertCollectionNames(names);
        }

        [TestCaseSource(typeof(AdvancementsTestData), "TestCases")]
        public void Advancement(string creature, Dictionary<string, string[]> rollAndData)
        {
            var typesAndAmounts = new Dictionary<string, string>();

            foreach (var kvp in rollAndData)
            {
                var roll = kvp.Key;
                var data = kvp.Value;

                var type = string.Join(",", data);

                typesAndAmounts[type] = roll;
            }

            AssertTypesAndAmounts(creature, typesAndAmounts);
        }

        public class AdvancementsTestData
        {
            public static IEnumerable TestCases
            {
                get
                {
                    var testCases = new Dictionary<string, Dictionary<string, string[]>>();
                    var creatures = CreatureConstants.All();

                    foreach (var creature in creatures)
                    {
                        testCases[creature] = new Dictionary<string, string[]>();
                    }

                    foreach (var testCase in testCases)
                    {
                        var advancements = testCase.Value.Select(kvp => $"{kvp.Key}:{kvp.Value[DataIndexConstants.AdvancementSelectionData.Size]}");
                        yield return new TestCaseData(testCase.Key, testCase.Value)
                            .SetName($"Advancement({testCase.Key}, [{string.Join("], [", advancements)}])");
                    }
                }
            }

            private static string[] GetData(
                string size,
                string challengeRating,
                double space,
                double reach,
                int strength,
                int dexterity,
                int constitution,
                int casterLevel,
                int naturalArmor)
            {
                var data = new string[9];
                data[DataIndexConstants.AdvancementSelectionData.CasterLevel] = casterLevel.ToString();
                data[DataIndexConstants.AdvancementSelectionData.ChallengeRating] = challengeRating;
                data[DataIndexConstants.AdvancementSelectionData.ConstitutionAdjustment] = constitution.ToString();
                data[DataIndexConstants.AdvancementSelectionData.DexterityAdjustment] = dexterity.ToString();
                data[DataIndexConstants.AdvancementSelectionData.NaturalArmorAdjustment] = naturalArmor.ToString();
                data[DataIndexConstants.AdvancementSelectionData.Reach] = reach.ToString();
                data[DataIndexConstants.AdvancementSelectionData.Size] = size;
                data[DataIndexConstants.AdvancementSelectionData.Space] = space.ToString();
                data[DataIndexConstants.AdvancementSelectionData.StrengthAdjustment] = strength.ToString();

                return data;
            }
        }

        [TestCase(CreatureConstants.Types.Aberration, 4)]
        [TestCase(CreatureConstants.Types.Animal, 3)]
        [TestCase(CreatureConstants.Types.Construct, 4)]
        [TestCase(CreatureConstants.Types.Dragon, 2)]
        [TestCase(CreatureConstants.Types.Elemental, 4)]
        [TestCase(CreatureConstants.Types.Fey, 4)]
        [TestCase(CreatureConstants.Types.Giant, 4)]
        [TestCase(CreatureConstants.Types.Humanoid, 4)]
        [TestCase(CreatureConstants.Types.MagicalBeast, 3)]
        [TestCase(CreatureConstants.Types.MonstrousHumanoid, 3)]
        [TestCase(CreatureConstants.Types.Ooze, 4)]
        [TestCase(CreatureConstants.Types.Outsider, 2)]
        [TestCase(CreatureConstants.Types.Plant, 4)]
        [TestCase(CreatureConstants.Types.Undead, 4)]
        [TestCase(CreatureConstants.Types.Vermin, 4)]
        public void AdvancementChallengeRatingDivisor(string creatureType, int divisor)
        {
            var typesAndAmounts = new Dictionary<string, int>();
            typesAndAmounts[creatureType] = divisor;

            AssertTypesAndAmounts(creatureType, typesAndAmounts);
        }
    }
}
