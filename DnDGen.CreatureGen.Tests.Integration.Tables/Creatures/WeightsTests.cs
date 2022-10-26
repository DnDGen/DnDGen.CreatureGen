using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Tables;
using DnDGen.Infrastructure.Selectors.Collections;
using DnDGen.RollGen;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Integration.Tables.Creatures
{
    [TestFixture]
    public class WeightsTests : TypesAndAmountsTests
    {
        private ICollectionSelector collectionSelector;
        private Dice dice;

        protected override string tableName => TableNameConstants.TypeAndAmount.Weights;

        [SetUp]
        public void Setup()
        {
            collectionSelector = GetNewInstanceOf<ICollectionSelector>();
            collectionSelector = GetNewInstanceOf<ICollectionSelector>();
            dice = GetNewInstanceOf<Dice>();
        }

        [Test]
        public void WeightsNames()
        {
            var creatures = CreatureConstants.GetAll();
            AssertCollectionNames(creatures);
        }

        [TestCaseSource(nameof(CreatureWeightsData))]
        public void CreatureWeights(string name, Dictionary<string, string> typesAndRolls)
        {
            var genders = collectionSelector.SelectFrom(TableNameConstants.Collection.Genders, name);

            Assert.That(typesAndRolls, Is.Not.Empty, name);
            Assert.That(typesAndRolls.Keys, Is.EqualTo(genders.Union(new[] { name })), name);

            AssertTypesAndAmounts(name, typesAndRolls);
        }

        public static Dictionary<string, Dictionary<string, string>> GetCreatureWeights()
        {
            var creatures = CreatureConstants.GetAll();
            var weights = new Dictionary<string, Dictionary<string, string>>();

            foreach (var creature in creatures)
            {
                weights[creature] = new Dictionary<string, string>();
            }

            //INFO: The weight modifier is multiplied by the height modifier
            weights[CreatureConstants.Aasimar][GenderConstants.Female] = "90";
            weights[CreatureConstants.Aasimar][GenderConstants.Male] = "110";
            weights[CreatureConstants.Aasimar][CreatureConstants.Aasimar] = "2d4"; //x5
            weights[CreatureConstants.Aboleth][GenderConstants.Hermaphrodite] = GetGenderFromAverage(6500);
            weights[CreatureConstants.Aboleth][CreatureConstants.Aboleth] = GetCreatureFromAverage(6500, 20 * 12);
            weights[CreatureConstants.Achaierai][GenderConstants.Female] = GetGenderFromAverage(750);
            weights[CreatureConstants.Achaierai][GenderConstants.Male] = GetGenderFromAverage(750);
            weights[CreatureConstants.Achaierai][CreatureConstants.Achaierai] = GetCreatureFromAverage(750, 15 * 12);
            weights[CreatureConstants.Allip][GenderConstants.Female] = "85";
            weights[CreatureConstants.Allip][GenderConstants.Male] = "120";
            weights[CreatureConstants.Allip][CreatureConstants.Human] = "2d4"; //x5
            weights[CreatureConstants.Androsphinx][GenderConstants.Male] = GetGenderFromAverage(800);
            weights[CreatureConstants.Androsphinx][CreatureConstants.Androsphinx] = GetCreatureFromAverage(800, 10 * 12);
            weights[CreatureConstants.Angel_AstralDeva][GenderConstants.Female] = GetGenderFromAverage(250);
            weights[CreatureConstants.Angel_AstralDeva][GenderConstants.Male] = GetGenderFromAverage(250);
            weights[CreatureConstants.Angel_AstralDeva][CreatureConstants.Angel_AstralDeva] = GetCreatureFromAverage(250, 7 * 12 + 3);
            weights[CreatureConstants.Angel_Planetar][GenderConstants.Female] = GetGenderFromAverage(500);
            weights[CreatureConstants.Angel_Planetar][GenderConstants.Male] = GetGenderFromAverage(500);
            weights[CreatureConstants.Angel_Planetar][CreatureConstants.Angel_Planetar] = GetCreatureFromAverage(500, 8 * 12 + 6);
            weights[CreatureConstants.Angel_Solar][GenderConstants.Female] = GetGenderFromAverage(500);
            weights[CreatureConstants.Angel_Solar][GenderConstants.Male] = GetGenderFromAverage(500);
            weights[CreatureConstants.Angel_Solar][CreatureConstants.Angel_Solar] = GetCreatureFromAverage(500, 9 * 12 + 6);
            weights[CreatureConstants.AnimatedObject_Anvil_Colossal][GenderConstants.Agender] = GetGenderFromAverage(2400);
            weights[CreatureConstants.AnimatedObject_Anvil_Colossal][CreatureConstants.AnimatedObject_Anvil_Colossal] = GetCreatureFromAverage(2400, 36 * 12);
            weights[CreatureConstants.AnimatedObject_Anvil_Gargantuan][GenderConstants.Agender] = GetGenderFromAverage(1200);
            weights[CreatureConstants.AnimatedObject_Anvil_Gargantuan][CreatureConstants.AnimatedObject_Anvil_Gargantuan] = GetCreatureFromAverage(1200, 18 * 12);
            weights[CreatureConstants.AnimatedObject_Anvil_Huge][GenderConstants.Agender] = GetGenderFromAverage(600);
            weights[CreatureConstants.AnimatedObject_Anvil_Huge][CreatureConstants.AnimatedObject_Anvil_Huge] = GetCreatureFromAverage(600, 18 * 12 / 2);
            weights[CreatureConstants.AnimatedObject_Anvil_Large][GenderConstants.Agender] = GetGenderFromAverage(300);
            weights[CreatureConstants.AnimatedObject_Anvil_Large][CreatureConstants.AnimatedObject_Anvil_Large] = GetCreatureFromAverage(300, (9 * 12 + 6) / 2);
            weights[CreatureConstants.AnimatedObject_Anvil_Medium][GenderConstants.Agender] = GetGenderFromAverage(150);
            weights[CreatureConstants.AnimatedObject_Anvil_Medium][CreatureConstants.AnimatedObject_Anvil_Medium] = GetCreatureFromAverage(150, 3 * 12);
            weights[CreatureConstants.AnimatedObject_Anvil_Small][GenderConstants.Agender] = GetGenderFromAverage(75);
            weights[CreatureConstants.AnimatedObject_Anvil_Small][CreatureConstants.AnimatedObject_Anvil_Small] = GetCreatureFromAverage(75, 18);
            weights[CreatureConstants.AnimatedObject_Anvil_Tiny][GenderConstants.Agender] = GetGenderFromAverage(37);
            weights[CreatureConstants.AnimatedObject_Anvil_Tiny][CreatureConstants.AnimatedObject_Anvil_Tiny] = GetCreatureFromAverage(37, 10);
            weights[CreatureConstants.Dwarf_Deep][GenderConstants.Female] = "100";
            weights[CreatureConstants.Dwarf_Deep][GenderConstants.Male] = "130";
            weights[CreatureConstants.Dwarf_Deep][CreatureConstants.Dwarf_Deep] = "2d6"; //x7
            weights[CreatureConstants.Dwarf_Duergar][GenderConstants.Female] = "100";
            weights[CreatureConstants.Dwarf_Duergar][GenderConstants.Male] = "130";
            weights[CreatureConstants.Dwarf_Duergar][CreatureConstants.Dwarf_Duergar] = "2d6"; //x7
            weights[CreatureConstants.Dwarf_Hill][GenderConstants.Female] = "100";
            weights[CreatureConstants.Dwarf_Hill][GenderConstants.Male] = "130";
            weights[CreatureConstants.Dwarf_Hill][CreatureConstants.Dwarf_Hill] = "2d6"; //x7
            weights[CreatureConstants.Dwarf_Mountain][GenderConstants.Female] = "100";
            weights[CreatureConstants.Dwarf_Mountain][GenderConstants.Male] = "130";
            weights[CreatureConstants.Dwarf_Mountain][CreatureConstants.Dwarf_Mountain] = "2d6"; //x7
            weights[CreatureConstants.Elf_Aquatic][GenderConstants.Female] = "80";
            weights[CreatureConstants.Elf_Aquatic][GenderConstants.Male] = "85";
            weights[CreatureConstants.Elf_Aquatic][CreatureConstants.Elf_Aquatic] = "1d6"; //x3
            weights[CreatureConstants.Elf_Drow][GenderConstants.Female] = "80";
            weights[CreatureConstants.Elf_Drow][GenderConstants.Male] = "85";
            weights[CreatureConstants.Elf_Drow][CreatureConstants.Elf_Drow] = "1d6"; //x3
            weights[CreatureConstants.Elf_Gray][GenderConstants.Female] = "80";
            weights[CreatureConstants.Elf_Gray][GenderConstants.Male] = "85";
            weights[CreatureConstants.Elf_Gray][CreatureConstants.Elf_Gray] = "1d6"; //x3
            weights[CreatureConstants.Elf_Half][GenderConstants.Female] = "80";
            weights[CreatureConstants.Elf_Half][GenderConstants.Male] = "100";
            weights[CreatureConstants.Elf_Half][CreatureConstants.Elf_Half] = "2d4"; //x5
            weights[CreatureConstants.Elf_High][GenderConstants.Female] = "80";
            weights[CreatureConstants.Elf_High][GenderConstants.Male] = "85";
            weights[CreatureConstants.Elf_High][CreatureConstants.Elf_High] = "1d6"; //x3
            weights[CreatureConstants.Elf_Wild][GenderConstants.Female] = "80";
            weights[CreatureConstants.Elf_Wild][GenderConstants.Male] = "85";
            weights[CreatureConstants.Elf_Wild][CreatureConstants.Elf_Wild] = "1d6"; //x3
            weights[CreatureConstants.Elf_Wood][GenderConstants.Female] = "80";
            weights[CreatureConstants.Elf_Wood][GenderConstants.Male] = "85";
            weights[CreatureConstants.Elf_Wood][CreatureConstants.Elf_Wood] = "1d6"; //x3
            weights[CreatureConstants.Gnome_Forest][GenderConstants.Female] = "35";
            weights[CreatureConstants.Gnome_Forest][GenderConstants.Male] = "40";
            weights[CreatureConstants.Gnome_Forest][CreatureConstants.Gnome_Forest] = "1"; //x1
            weights[CreatureConstants.Gnome_Rock][GenderConstants.Female] = "35";
            weights[CreatureConstants.Gnome_Rock][GenderConstants.Male] = "40";
            weights[CreatureConstants.Gnome_Rock][CreatureConstants.Gnome_Rock] = "1"; //x1
            weights[CreatureConstants.Gnome_Svirfneblin][GenderConstants.Female] = "35";
            weights[CreatureConstants.Gnome_Svirfneblin][GenderConstants.Male] = "40";
            weights[CreatureConstants.Gnome_Svirfneblin][CreatureConstants.Gnome_Svirfneblin] = "1"; //x1
            weights[CreatureConstants.Goblin][GenderConstants.Female] = "25";
            weights[CreatureConstants.Goblin][GenderConstants.Male] = "30";
            weights[CreatureConstants.Goblin][CreatureConstants.Goblin] = "1"; //x1
            weights[CreatureConstants.Halfling_Deep][GenderConstants.Female] = "25";
            weights[CreatureConstants.Halfling_Deep][GenderConstants.Male] = "30";
            weights[CreatureConstants.Halfling_Deep][CreatureConstants.Halfling_Deep] = "1"; //x1
            weights[CreatureConstants.Halfling_Lightfoot][GenderConstants.Female] = "25";
            weights[CreatureConstants.Halfling_Lightfoot][GenderConstants.Male] = "30";
            weights[CreatureConstants.Halfling_Lightfoot][CreatureConstants.Halfling_Lightfoot] = "1"; //x1
            weights[CreatureConstants.Halfling_Tallfellow][GenderConstants.Female] = "25";
            weights[CreatureConstants.Halfling_Tallfellow][GenderConstants.Male] = "30";
            weights[CreatureConstants.Halfling_Tallfellow][CreatureConstants.Halfling_Tallfellow] = "1"; //x1
            weights[CreatureConstants.Hobgoblin][GenderConstants.Female] = "145";
            weights[CreatureConstants.Hobgoblin][GenderConstants.Male] = "165";
            weights[CreatureConstants.Hobgoblin][CreatureConstants.Hobgoblin] = "2d4"; //x5
            weights[CreatureConstants.Human][GenderConstants.Female] = "85";
            weights[CreatureConstants.Human][GenderConstants.Male] = "120";
            weights[CreatureConstants.Human][CreatureConstants.Human] = "2d4"; //x5
            weights[CreatureConstants.Kobold][GenderConstants.Female] = "20";
            weights[CreatureConstants.Kobold][GenderConstants.Male] = "25";
            weights[CreatureConstants.Kobold][CreatureConstants.Kobold] = "1"; //x1
            weights[CreatureConstants.Merfolk][GenderConstants.Female] = "135";
            weights[CreatureConstants.Merfolk][GenderConstants.Male] = "145";
            weights[CreatureConstants.Merfolk][CreatureConstants.Merfolk] = "2d4"; //x5
            weights[CreatureConstants.Orc][GenderConstants.Female] = "120";
            weights[CreatureConstants.Orc][GenderConstants.Male] = "160";
            weights[CreatureConstants.Orc][CreatureConstants.Orc_Half] = "2d6"; //x7
            weights[CreatureConstants.Orc_Half][GenderConstants.Female] = "110";
            weights[CreatureConstants.Orc_Half][GenderConstants.Male] = "150";
            weights[CreatureConstants.Orc_Half][CreatureConstants.Orc_Half] = "2d6"; //x7
            weights[CreatureConstants.Tiefling][GenderConstants.Female] = "85";
            weights[CreatureConstants.Tiefling][GenderConstants.Male] = "120";
            weights[CreatureConstants.Tiefling][CreatureConstants.Tiefling] = "2d4"; //x5

            return weights;
        }

        public static IEnumerable CreatureWeightsData => GetCreatureWeights().Select(t => new TestCaseData(t.Key, t.Value));

        private static string GetGenderFromAverage(int average)
        {
            var gender = average * 8 / 10;
            return gender.ToString();
        }

        private static string GetCreatureFromAverage(int weightAverage, int heightAverage)
        {
            var lowerMultiplier = heightAverage / 10;
            var upperMultiplier = heightAverage * 3 / 10;

            var lower = weightAverage / 10 / lowerMultiplier;
            var upper = weightAverage * 3 / 10 / upperMultiplier;

            var creature = RollHelper.GetRollWithFewestDice(lower, upper);

            return creature;
        }

        [TestCase(CreatureConstants.Aboleth, GenderConstants.Hermaphrodite, 6500)]
        [TestCase(CreatureConstants.Achaierai, GenderConstants.Male, 750)]
        [TestCase(CreatureConstants.Achaierai, GenderConstants.Female, 750)]
        [TestCase(CreatureConstants.Androsphinx, GenderConstants.Male, 800)]
        [TestCase(CreatureConstants.Angel_AstralDeva, GenderConstants.Male, 250)]
        [TestCase(CreatureConstants.Angel_AstralDeva, GenderConstants.Female, 250)]
        [TestCase(CreatureConstants.Angel_Planetar, GenderConstants.Male, 500)]
        [TestCase(CreatureConstants.Angel_Planetar, GenderConstants.Female, 500)]
        [TestCase(CreatureConstants.Angel_Solar, GenderConstants.Male, 500)]
        [TestCase(CreatureConstants.Angel_Solar, GenderConstants.Female, 500)]
        [TestCase(CreatureConstants.AnimatedObject_Anvil_Colossal, GenderConstants.Agender, 2400)]
        [TestCase(CreatureConstants.AnimatedObject_Anvil_Gargantuan, GenderConstants.Agender, 1200)]
        [TestCase(CreatureConstants.AnimatedObject_Anvil_Huge, GenderConstants.Agender, 600)]
        [TestCase(CreatureConstants.AnimatedObject_Anvil_Large, GenderConstants.Agender, 300)]
        [TestCase(CreatureConstants.AnimatedObject_Anvil_Medium, GenderConstants.Agender, 150)]
        [TestCase(CreatureConstants.AnimatedObject_Anvil_Small, GenderConstants.Agender, 75)]
        [TestCase(CreatureConstants.AnimatedObject_Anvil_Tiny, GenderConstants.Agender, 37)]
        public void RollCalculationsAreAccurate(string creature, string gender, int average)
        {
            var heights = HeightsTests.GetCreatureHeights();

            var heightMultiplierMin = dice.Roll(heights[creature][creature]).AsPotentialMinimum();
            var heightMultiplierAvg = dice.Roll(heights[creature][creature]).AsPotentialAverage();
            var heightMultiplierMax = dice.Roll(heights[creature][creature]).AsPotentialMaximum();

            var weights = GetCreatureWeights();

            var baseWeight = dice.Roll(weights[creature][gender]).AsSum();
            var weightMultiplierMin = dice.Roll(weights[creature][creature]).AsPotentialMinimum();
            var weightMultiplierAvg = dice.Roll(weights[creature][creature]).AsPotentialAverage();
            var weightMultiplierMax = dice.Roll(weights[creature][creature]).AsPotentialMaximum();
            var sigma = Math.Max(1, average * 0.05);

            Assert.That(baseWeight, Is.EqualTo(average * 0.8).Within(sigma), "Base Weight (80%)");
            Assert.That(heightMultiplierMin * weightMultiplierMin, Is.EqualTo(average * 0.1).Within(sigma), $"Min (10%). H:{heightMultiplierMin}, W:{weightMultiplierMin}");
            Assert.That(heightMultiplierAvg * weightMultiplierAvg, Is.EqualTo(average * 0.2).Within(sigma), $"Average (20%). H:{heightMultiplierAvg}, W:{weightMultiplierAvg}");
            Assert.That(heightMultiplierMax * weightMultiplierMax, Is.EqualTo(average * 0.3).Within(sigma), $"Max (30%). H:{heightMultiplierMax}, W:{weightMultiplierMax}");
            Assert.That(baseWeight + heightMultiplierMin * weightMultiplierMin, Is.EqualTo(average * 0.9).Within(sigma), "Min (-10%)");
            Assert.That(baseWeight + heightMultiplierAvg * weightMultiplierAvg, Is.EqualTo(average).Within(sigma), "Average");
            Assert.That(baseWeight + heightMultiplierMax * weightMultiplierMax, Is.EqualTo(average * 1.1).Within(sigma), "Max (+10%)");
        }
    }
}