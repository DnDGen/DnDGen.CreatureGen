using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Generators.Creatures;
using DnDGen.CreatureGen.Selectors.Collections;
using DnDGen.CreatureGen.Tables;
using DnDGen.Infrastructure.Selectors.Collections;
using DnDGen.RollGen;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;

namespace DnDGen.CreatureGen.Tests.Integration.Tables.Creatures
{
    [TestFixture]
    public class AgeRollsTests : TypesAndAmountsTests
    {
        private ICollectionSelector collectionSelector;
        private ITypeAndAmountSelector typesAndAmountsSelector;

        protected override string tableName => TableNameConstants.TypeAndAmount.AgeRolls;

        [SetUp]
        public void Setup()
        {
            collectionSelector = GetNewInstanceOf<ICollectionSelector>();
            typesAndAmountsSelector = GetNewInstanceOf<ITypeAndAmountSelector>();
        }

        [Test]
        public void AgeRollsNames()
        {
            var creatures = CreatureConstants.GetAll();
            AssertCollectionNames(creatures);
        }

        [TestCaseSource(nameof(CreatureAgeRollsData))]
        public void CreatureAgeRolls(string name, Dictionary<string, string> typesAndRolls)
        {
            Assert.That(typesAndRolls, Is.Not.Empty
                .And.Count.AtLeast(2)
                .And.ContainKey(AgeConstants.Categories.Maximum), name);
            AssertTypesAndAmounts(name, typesAndRolls);
        }

        public static IEnumerable CreatureAgeRollsData
        {
            get
            {
                var testCases = new Dictionary<string, Dictionary<string, string>>();
                var creatures = CreatureConstants.GetAll();

                foreach (var creature in creatures)
                {
                    testCases[creature] = new Dictionary<string, string>();
                }

                testCases[CreatureConstants.Dwarf_Deep][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(40, 124);
                testCases[CreatureConstants.Dwarf_Deep][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(125, 187);
                testCases[CreatureConstants.Dwarf_Deep][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(188, 249);
                testCases[CreatureConstants.Dwarf_Deep][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(250, 450);
                testCases[CreatureConstants.Dwarf_Deep][AgeConstants.Categories.Maximum] = "250+2d100";
                testCases[CreatureConstants.Dwarf_Duergar][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(40, 124);
                testCases[CreatureConstants.Dwarf_Duergar][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(125, 187);
                testCases[CreatureConstants.Dwarf_Duergar][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(188, 249);
                testCases[CreatureConstants.Dwarf_Duergar][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(250, 450);
                testCases[CreatureConstants.Dwarf_Duergar][AgeConstants.Categories.Maximum] = "250+2d100";
                testCases[CreatureConstants.Dwarf_Hill][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(40, 124);
                testCases[CreatureConstants.Dwarf_Hill][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(125, 187);
                testCases[CreatureConstants.Dwarf_Hill][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(188, 249);
                testCases[CreatureConstants.Dwarf_Hill][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(250, 450);
                testCases[CreatureConstants.Dwarf_Hill][AgeConstants.Categories.Maximum] = "250+2d100";
                testCases[CreatureConstants.Dwarf_Mountain][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(40, 124);
                testCases[CreatureConstants.Dwarf_Mountain][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(125, 187);
                testCases[CreatureConstants.Dwarf_Mountain][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(188, 249);
                testCases[CreatureConstants.Dwarf_Mountain][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(250, 450);
                testCases[CreatureConstants.Dwarf_Mountain][AgeConstants.Categories.Maximum] = "250+2d100";
                testCases[CreatureConstants.Human][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(15, 34);
                testCases[CreatureConstants.Human][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(35, 52);
                testCases[CreatureConstants.Human][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(53, 69);
                testCases[CreatureConstants.Human][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(70, 110);
                testCases[CreatureConstants.Human][AgeConstants.Categories.Maximum] = "70+2d20";

                foreach (var testCase in testCases)
                {
                    yield return new TestCaseData(testCase.Key, testCase.Value);
                }
            }
        }
    }
}