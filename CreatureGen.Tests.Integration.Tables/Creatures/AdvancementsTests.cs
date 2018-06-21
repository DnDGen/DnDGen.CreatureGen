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

                    testCases[CreatureConstants.Aboleth][RollConstants.Roll1To8] = GetData(SizeConstants.Huge, ChallengeRatingConstants.Seven, 15, 10, 0, 0, 0, 0);
                    testCases[CreatureConstants.Aboleth][RollConstants.Roll9To16] = GetData(SizeConstants.Gargantuan, ChallengeRatingConstants.Eight, 20, 15, 8, 0, 4, 4);
                    testCases[CreatureConstants.Achaierai][RollConstants.Roll1To6] = GetData(SizeConstants.Large, ChallengeRatingConstants.Five, 10, 10, 0, 0, 0, 0);
                    testCases[CreatureConstants.Achaierai][RollConstants.Roll7To12] = GetData(SizeConstants.Huge, ChallengeRatingConstants.Six, 15, 10, 8, -2, 4, 3);
                    testCases[CreatureConstants.Allip][RollConstants.Roll1To8] = GetData(SizeConstants.Medium, ChallengeRatingConstants.Three, 5, 5, 0, 0, 0, 0);
                    testCases[CreatureConstants.Androsphinx][RollConstants.Roll1To6] = GetData(SizeConstants.Large, ChallengeRatingConstants.Nine, 10, 5, 0, 0, 0, 0);
                    testCases[CreatureConstants.Androsphinx][RollConstants.Roll7To24] = GetData(SizeConstants.Huge, ChallengeRatingConstants.Ten, 15, 10, 8, -2, 4, 3);
                    testCases[CreatureConstants.Angel_AstralDeva][RollConstants.Roll1To6] = GetData(SizeConstants.Medium, ChallengeRatingConstants.Fourteen, 5, 5, 0, 0, 0, 0);
                    testCases[CreatureConstants.Angel_AstralDeva][RollConstants.Roll7To24] = GetData(SizeConstants.Large, ChallengeRatingConstants.Fifteen, 10, 5, 8, -2, 4, 2);
                    testCases[CreatureConstants.Angel_Planetar][RollConstants.Roll1To7] = GetData(SizeConstants.Large, ChallengeRatingConstants.Sixteen, 10, 10, 0, 0, 0, 0);
                    testCases[CreatureConstants.Angel_Planetar][RollConstants.Roll8To28] = GetData(SizeConstants.Huge, ChallengeRatingConstants.Seventeen, 15, 15, 8, -2, 4, 3);
                    testCases[CreatureConstants.Angel_Solar][RollConstants.Roll1To11] = GetData(SizeConstants.Large, ChallengeRatingConstants.TwentyThree, 10, 10, 0, 0, 0, 0);
                    testCases[CreatureConstants.Angel_Solar][RollConstants.Roll12To44] = GetData(SizeConstants.Huge, ChallengeRatingConstants.TwentyFour, 15, 15, 8, -2, 4, 3);
                    testCases[CreatureConstants.Arrowhawk_Adult][RollConstants.Roll1To7] = GetData(SizeConstants.Small, ChallengeRatingConstants.Five, 5, 5, 0, 0, 0, 0);
                    testCases[CreatureConstants.Arrowhawk_Elder][RollConstants.Roll1To9] = GetData(SizeConstants.Large, ChallengeRatingConstants.Eight, 10, 5, 0, 0, 0, 0);
                    testCases[CreatureConstants.Arrowhawk_Juvenile][RollConstants.Roll1To3] = GetData(SizeConstants.Medium, ChallengeRatingConstants.Three, 5, 5, 0, 0, 0, 0);
                    testCases[CreatureConstants.Basilisk][RollConstants.Roll1To4] = GetData(SizeConstants.Medium, ChallengeRatingConstants.Five, 5, 5, 0, 0, 0, 0);
                    testCases[CreatureConstants.Basilisk][RollConstants.Roll5To12] = GetData(SizeConstants.Large, ChallengeRatingConstants.Six, 10, 5, 8, -2, 4, 2);
                    testCases[CreatureConstants.Criosphinx][RollConstants.Roll1To5] = GetData(SizeConstants.Large, ChallengeRatingConstants.Seven, 10, 5, 0, 0, 0, 0);
                    testCases[CreatureConstants.Criosphinx][RollConstants.Roll6To20] = GetData(SizeConstants.Huge, ChallengeRatingConstants.Eight, 15, 10, 8, -2, 4, 3);
                    testCases[CreatureConstants.Elemental_Air_Elder][RollConstants.Roll1To24] = GetData(SizeConstants.Huge, ChallengeRatingConstants.Eleven, 15, 15, 0, 0, 0, 0);
                    testCases[CreatureConstants.Elemental_Air_Greater][RollConstants.Roll1To2] = GetData(SizeConstants.Huge, ChallengeRatingConstants.Nine, 15, 15, 0, 0, 0, 0);
                    testCases[CreatureConstants.Elemental_Air_Huge][RollConstants.Roll1To4] = GetData(SizeConstants.Huge, ChallengeRatingConstants.Seven, 15, 15, 0, 0, 0, 0);
                    testCases[CreatureConstants.Elemental_Air_Large][RollConstants.Roll1To7] = GetData(SizeConstants.Large, ChallengeRatingConstants.Five, 10, 10, 0, 0, 0, 0);
                    testCases[CreatureConstants.Elemental_Air_Medium][RollConstants.Roll1To3] = GetData(SizeConstants.Medium, ChallengeRatingConstants.Three, 5, 5, 0, 0, 0, 0);
                    testCases[CreatureConstants.Elemental_Air_Small][RollConstants.Roll1] = GetData(SizeConstants.Small, ChallengeRatingConstants.One, 5, 5, 0, 0, 0, 0);
                    testCases[CreatureConstants.Elemental_Earth_Elder][RollConstants.Roll1To24] = GetData(SizeConstants.Huge, ChallengeRatingConstants.Eleven, 15, 15, 0, 0, 0, 0);
                    testCases[CreatureConstants.Elemental_Earth_Greater][RollConstants.Roll1To2] = GetData(SizeConstants.Huge, ChallengeRatingConstants.Nine, 15, 15, 0, 0, 0, 0);
                    testCases[CreatureConstants.Elemental_Earth_Huge][RollConstants.Roll1To4] = GetData(SizeConstants.Huge, ChallengeRatingConstants.Seven, 15, 15, 0, 0, 0, 0);
                    testCases[CreatureConstants.Elemental_Earth_Large][RollConstants.Roll1To7] = GetData(SizeConstants.Large, ChallengeRatingConstants.Five, 10, 10, 0, 0, 0, 0);
                    testCases[CreatureConstants.Elemental_Earth_Medium][RollConstants.Roll1To3] = GetData(SizeConstants.Medium, ChallengeRatingConstants.Three, 5, 5, 0, 0, 0, 0);
                    testCases[CreatureConstants.Elemental_Earth_Small][RollConstants.Roll1] = GetData(SizeConstants.Small, ChallengeRatingConstants.One, 5, 5, 0, 0, 0, 0);
                    testCases[CreatureConstants.Elemental_Fire_Elder][RollConstants.Roll1To24] = GetData(SizeConstants.Huge, ChallengeRatingConstants.Eleven, 15, 15, 0, 0, 0, 0);
                    testCases[CreatureConstants.Elemental_Fire_Greater][RollConstants.Roll1To2] = GetData(SizeConstants.Huge, ChallengeRatingConstants.Nine, 15, 15, 0, 0, 0, 0);
                    testCases[CreatureConstants.Elemental_Fire_Huge][RollConstants.Roll1To4] = GetData(SizeConstants.Huge, ChallengeRatingConstants.Seven, 15, 15, 0, 0, 0, 0);
                    testCases[CreatureConstants.Elemental_Fire_Large][RollConstants.Roll1To7] = GetData(SizeConstants.Large, ChallengeRatingConstants.Five, 10, 10, 0, 0, 0, 0);
                    testCases[CreatureConstants.Elemental_Fire_Medium][RollConstants.Roll1To3] = GetData(SizeConstants.Medium, ChallengeRatingConstants.Three, 5, 5, 0, 0, 0, 0);
                    testCases[CreatureConstants.Elemental_Fire_Small][RollConstants.Roll1] = GetData(SizeConstants.Small, ChallengeRatingConstants.One, 5, 5, 0, 0, 0, 0);
                    testCases[CreatureConstants.Elemental_Water_Elder][RollConstants.Roll1To24] = GetData(SizeConstants.Huge, ChallengeRatingConstants.Eleven, 15, 15, 0, 0, 0, 0);
                    testCases[CreatureConstants.Elemental_Water_Greater][RollConstants.Roll1To2] = GetData(SizeConstants.Huge, ChallengeRatingConstants.Nine, 15, 15, 0, 0, 0, 0);
                    testCases[CreatureConstants.Elemental_Water_Huge][RollConstants.Roll1To4] = GetData(SizeConstants.Huge, ChallengeRatingConstants.Seven, 15, 15, 0, 0, 0, 0);
                    testCases[CreatureConstants.Elemental_Water_Large][RollConstants.Roll1To7] = GetData(SizeConstants.Large, ChallengeRatingConstants.Five, 10, 10, 0, 0, 0, 0);
                    testCases[CreatureConstants.Elemental_Water_Medium][RollConstants.Roll1To3] = GetData(SizeConstants.Medium, ChallengeRatingConstants.Three, 5, 5, 0, 0, 0, 0);
                    testCases[CreatureConstants.Elemental_Water_Small][RollConstants.Roll1] = GetData(SizeConstants.Small, ChallengeRatingConstants.One, 5, 5, 0, 0, 0, 0);
                    testCases[CreatureConstants.Gynosphinx][RollConstants.Roll1To4] = GetData(SizeConstants.Large, ChallengeRatingConstants.Eight, 10, 5, 0, 0, 0, 0);
                    testCases[CreatureConstants.Gynosphinx][RollConstants.Roll5To16] = GetData(SizeConstants.Huge, ChallengeRatingConstants.Nine, 15, 10, 8, -2, 4, 3);
                    testCases[CreatureConstants.Hieracosphinx][RollConstants.Roll1To5] = GetData(SizeConstants.Large, ChallengeRatingConstants.Five, 10, 5, 0, 0, 0, 0);
                    testCases[CreatureConstants.Hieracosphinx][RollConstants.Roll6To18] = GetData(SizeConstants.Huge, ChallengeRatingConstants.Six, 15, 10, 8, -2, 4, 3);
                    testCases[CreatureConstants.Mephit_Air][RollConstants.Roll1To3] = GetData(SizeConstants.Small, ChallengeRatingConstants.Three, 5, 5, 0, 0, 0, 0);
                    testCases[CreatureConstants.Mephit_Air][RollConstants.Roll4To6] = GetData(SizeConstants.Medium, ChallengeRatingConstants.Three, 5, 5, 4, -2, 4, 0);
                    testCases[CreatureConstants.Mephit_Dust][RollConstants.Roll1To3] = GetData(SizeConstants.Small, ChallengeRatingConstants.Three, 5, 5, 0, 0, 0, 0);
                    testCases[CreatureConstants.Mephit_Dust][RollConstants.Roll4To6] = GetData(SizeConstants.Medium, ChallengeRatingConstants.Three, 5, 5, 4, -2, 4, 0);
                    testCases[CreatureConstants.Mephit_Earth][RollConstants.Roll1To3] = GetData(SizeConstants.Small, ChallengeRatingConstants.Three, 5, 5, 0, 0, 0, 0);
                    testCases[CreatureConstants.Mephit_Earth][RollConstants.Roll4To6] = GetData(SizeConstants.Medium, ChallengeRatingConstants.Three, 5, 5, 4, -2, 4, 0);
                    testCases[CreatureConstants.Mephit_Fire][RollConstants.Roll1To3] = GetData(SizeConstants.Small, ChallengeRatingConstants.Three, 5, 5, 0, 0, 0, 0);
                    testCases[CreatureConstants.Mephit_Fire][RollConstants.Roll4To6] = GetData(SizeConstants.Medium, ChallengeRatingConstants.Three, 5, 5, 4, -2, 4, 0);
                    testCases[CreatureConstants.Mephit_Ice][RollConstants.Roll1To3] = GetData(SizeConstants.Small, ChallengeRatingConstants.Three, 5, 5, 0, 0, 0, 0);
                    testCases[CreatureConstants.Mephit_Ice][RollConstants.Roll4To6] = GetData(SizeConstants.Medium, ChallengeRatingConstants.Three, 5, 5, 4, -2, 4, 0);
                    testCases[CreatureConstants.Mephit_Magma][RollConstants.Roll1To3] = GetData(SizeConstants.Small, ChallengeRatingConstants.Three, 5, 5, 0, 0, 0, 0);
                    testCases[CreatureConstants.Mephit_Magma][RollConstants.Roll4To6] = GetData(SizeConstants.Medium, ChallengeRatingConstants.Three, 5, 5, 4, -2, 4, 0);
                    testCases[CreatureConstants.Mephit_Ooze][RollConstants.Roll1To3] = GetData(SizeConstants.Small, ChallengeRatingConstants.Three, 5, 5, 0, 0, 0, 0);
                    testCases[CreatureConstants.Mephit_Ooze][RollConstants.Roll4To6] = GetData(SizeConstants.Medium, ChallengeRatingConstants.Three, 5, 5, 4, -2, 4, 0);
                    testCases[CreatureConstants.Mephit_Salt][RollConstants.Roll1To3] = GetData(SizeConstants.Small, ChallengeRatingConstants.Three, 5, 5, 0, 0, 0, 0);
                    testCases[CreatureConstants.Mephit_Salt][RollConstants.Roll4To6] = GetData(SizeConstants.Medium, ChallengeRatingConstants.Three, 5, 5, 4, -2, 4, 0);
                    testCases[CreatureConstants.Mephit_Steam][RollConstants.Roll1To3] = GetData(SizeConstants.Small, ChallengeRatingConstants.Three, 5, 5, 0, 0, 0, 0);
                    testCases[CreatureConstants.Mephit_Steam][RollConstants.Roll4To6] = GetData(SizeConstants.Medium, ChallengeRatingConstants.Three, 5, 5, 4, -2, 4, 0);
                    testCases[CreatureConstants.Mephit_Water][RollConstants.Roll1To3] = GetData(SizeConstants.Small, ChallengeRatingConstants.Three, 5, 5, 0, 0, 0, 0);
                    testCases[CreatureConstants.Mephit_Water][RollConstants.Roll4To6] = GetData(SizeConstants.Medium, ChallengeRatingConstants.Three, 5, 5, 4, -2, 4, 0);
                    testCases[CreatureConstants.Tojanida_Adult][RollConstants.Roll1To7] = GetData(SizeConstants.Small, ChallengeRatingConstants.Five, 5, 5, 0, 0, 0, 0);
                    testCases[CreatureConstants.Tojanida_Elder][RollConstants.Roll1To9] = GetData(SizeConstants.Large, ChallengeRatingConstants.Eight, 10, 5, 0, 0, 0, 0);
                    testCases[CreatureConstants.Tojanida_Juvenile][RollConstants.Roll1To3] = GetData(SizeConstants.Medium, ChallengeRatingConstants.Three, 5, 5, 0, 0, 0, 0);

                    foreach (var testCase in testCases)
                    {
                        var advancements = testCase.Value.Select(kvp => $"{kvp.Key}:{kvp.Value[DataIndexConstants.AdvancementSelectionData.Size]}");
                        yield return new TestCaseData(testCase.Key, testCase.Value)
                            .SetName($"Advancement({testCase.Key}, [{string.Join("], [", advancements)}])");
                    }
                }
            }

            private static string[] GetData(
                string originalSize,
                string size,
                string challengeRating,
                double space,
                double reach)
            {


                var data = new string[9];
                //INFO: Only thing that adjusts caster level is the Barghest, and that gets set special in the selector
                data[DataIndexConstants.AdvancementSelectionData.CasterLevel] = 0.ToString();
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
