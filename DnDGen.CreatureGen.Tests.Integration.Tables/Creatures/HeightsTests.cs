using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Tables;
using DnDGen.Infrastructure.Selectors.Collections;
using DnDGen.RollGen;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Integration.Tables.Creatures
{
    [TestFixture]
    public class HeightsTests : TypesAndAmountsTests
    {
        private ICollectionSelector collectionSelector;
        private Dice dice;

        protected override string tableName => TableNameConstants.TypeAndAmount.Heights;

        [SetUp]
        public void Setup()
        {
            collectionSelector = GetNewInstanceOf<ICollectionSelector>();
            dice = GetNewInstanceOf<Dice>();
        }

        [Test]
        public void HeightsNames()
        {
            var creatures = CreatureConstants.GetAll();
            AssertCollectionNames(creatures);
        }

        [TestCaseSource(nameof(CreatureHeightsData))]
        public void CreatureHeights(string name, Dictionary<string, string> typesAndRolls)
        {
            var genders = collectionSelector.SelectFrom(TableNameConstants.Collection.Genders, name);

            Assert.That(typesAndRolls, Is.Not.Empty, name);
            Assert.That(typesAndRolls.Keys, Is.EqualTo(genders.Union(new[] { name })), name);

            AssertTypesAndAmounts(name, typesAndRolls);
        }

        public static Dictionary<string, Dictionary<string, string>> GetCreatureHeights()
        {
            var creatures = CreatureConstants.GetAll();
            var heights = new Dictionary<string, Dictionary<string, string>>();

            foreach (var creature in creatures)
            {
                heights[creature] = new Dictionary<string, string>();
            }

            heights[CreatureConstants.Aasimar][GenderConstants.Female] = "5*12+0";
            heights[CreatureConstants.Aasimar][GenderConstants.Male] = "5*12+2";
            heights[CreatureConstants.Aasimar][CreatureConstants.Aasimar] = "2d8";
            heights[CreatureConstants.Aboleth][GenderConstants.Hermaphrodite] = GetGenderFromAverage(20 * 12);
            heights[CreatureConstants.Aboleth][CreatureConstants.Aboleth] = GetCreatureFromAverage(20 * 12);
            heights[CreatureConstants.Achaierai][GenderConstants.Female] = GetGenderFromAverage(15 * 12);
            heights[CreatureConstants.Achaierai][GenderConstants.Male] = GetGenderFromAverage(15 * 12);
            heights[CreatureConstants.Achaierai][CreatureConstants.Achaierai] = GetCreatureFromAverage(15 * 12);
            heights[CreatureConstants.Allip][GenderConstants.Female] = "4*12+5";
            heights[CreatureConstants.Allip][GenderConstants.Male] = "4*12+10";
            heights[CreatureConstants.Allip][CreatureConstants.Human] = "2d10";
            heights[CreatureConstants.Androsphinx][GenderConstants.Male] = GetGenderFromAverage(10 * 12);
            heights[CreatureConstants.Androsphinx][CreatureConstants.Androsphinx] = GetCreatureFromAverage(10 * 12);
            heights[CreatureConstants.Angel_AstralDeva][GenderConstants.Female] = GetGenderFromAverage(7 * 12 + 3);
            heights[CreatureConstants.Angel_AstralDeva][GenderConstants.Male] = GetGenderFromAverage(7 * 12 + 3);
            heights[CreatureConstants.Angel_AstralDeva][CreatureConstants.Angel_AstralDeva] = GetCreatureFromAverage(7 * 12 + 3);
            heights[CreatureConstants.Angel_Planetar][GenderConstants.Female] = GetGenderFromAverage(8 * 12 + 6);
            heights[CreatureConstants.Angel_Planetar][GenderConstants.Male] = GetGenderFromAverage(8 * 12 + 6);
            heights[CreatureConstants.Angel_Planetar][CreatureConstants.Angel_Planetar] = GetCreatureFromAverage(8 * 12 + 6);
            heights[CreatureConstants.Angel_Solar][GenderConstants.Female] = GetGenderFromAverage(9 * 12 + 6);
            heights[CreatureConstants.Angel_Solar][GenderConstants.Male] = GetGenderFromAverage(9 * 12 + 6);
            heights[CreatureConstants.Angel_Solar][CreatureConstants.Angel_Solar] = GetCreatureFromAverage(9 * 12 + 6);
            heights[CreatureConstants.AnimatedObject_Anvil_Colossal][GenderConstants.Agender] = GetGenderFromAverage(36 * 12);
            heights[CreatureConstants.AnimatedObject_Anvil_Colossal][CreatureConstants.AnimatedObject_Anvil_Colossal] = GetCreatureFromAverage(36 * 12);
            heights[CreatureConstants.AnimatedObject_Anvil_Gargantuan][GenderConstants.Agender] = GetGenderFromAverage(18 * 12);
            heights[CreatureConstants.AnimatedObject_Anvil_Gargantuan][CreatureConstants.AnimatedObject_Anvil_Gargantuan] = GetCreatureFromAverage(18 * 12);
            heights[CreatureConstants.AnimatedObject_Anvil_Huge][GenderConstants.Agender] = GetGenderFromAverage(18 * 12 / 2);
            heights[CreatureConstants.AnimatedObject_Anvil_Huge][CreatureConstants.AnimatedObject_Anvil_Huge] = GetCreatureFromAverage(18 * 12 / 2);
            heights[CreatureConstants.AnimatedObject_Anvil_Large][GenderConstants.Agender] = GetGenderFromAverage((9 * 12 + 6) / 2);
            heights[CreatureConstants.AnimatedObject_Anvil_Large][CreatureConstants.AnimatedObject_Anvil_Large] = GetCreatureFromAverage((9 * 12 + 6) / 2);
            heights[CreatureConstants.AnimatedObject_Anvil_Medium][GenderConstants.Agender] = "2*12+5";
            heights[CreatureConstants.AnimatedObject_Anvil_Medium][CreatureConstants.AnimatedObject_Anvil_Medium] = "1d10";
            heights[CreatureConstants.AnimatedObject_Anvil_Small][GenderConstants.Agender] = "1*12+4";
            heights[CreatureConstants.AnimatedObject_Anvil_Small][CreatureConstants.AnimatedObject_Anvil_Small] = "1d4";
            heights[CreatureConstants.AnimatedObject_Anvil_Tiny][GenderConstants.Agender] = GetGenderFromAverage(10);
            heights[CreatureConstants.AnimatedObject_Anvil_Tiny][CreatureConstants.AnimatedObject_Anvil_Tiny] = GetCreatureFromAverage(10);
            heights[CreatureConstants.Dwarf_Deep][GenderConstants.Female] = "3*12+7";
            heights[CreatureConstants.Dwarf_Deep][GenderConstants.Male] = "3*12+9";
            heights[CreatureConstants.Dwarf_Deep][CreatureConstants.Dwarf_Deep] = "2d4";
            heights[CreatureConstants.Dwarf_Duergar][GenderConstants.Female] = "3*12+7";
            heights[CreatureConstants.Dwarf_Duergar][GenderConstants.Male] = "3*12+9";
            heights[CreatureConstants.Dwarf_Duergar][CreatureConstants.Dwarf_Duergar] = "2d4";
            heights[CreatureConstants.Dwarf_Hill][GenderConstants.Female] = "3*12+7";
            heights[CreatureConstants.Dwarf_Hill][GenderConstants.Male] = "3*12+9";
            heights[CreatureConstants.Dwarf_Hill][CreatureConstants.Dwarf_Hill] = "2d4";
            heights[CreatureConstants.Dwarf_Mountain][GenderConstants.Female] = "3*12+7";
            heights[CreatureConstants.Dwarf_Mountain][GenderConstants.Male] = "3*12+9";
            heights[CreatureConstants.Dwarf_Mountain][CreatureConstants.Dwarf_Mountain] = "2d4";
            heights[CreatureConstants.Elf_Aquatic][GenderConstants.Female] = "4*12+5";
            heights[CreatureConstants.Elf_Aquatic][GenderConstants.Male] = "4*12+5";
            heights[CreatureConstants.Elf_Aquatic][CreatureConstants.Elf_Aquatic] = "2d6";
            heights[CreatureConstants.Elf_Drow][GenderConstants.Female] = "4*12+5";
            heights[CreatureConstants.Elf_Drow][GenderConstants.Male] = "4*12+5";
            heights[CreatureConstants.Elf_Drow][CreatureConstants.Elf_Drow] = "2d6";
            heights[CreatureConstants.Elf_Gray][GenderConstants.Female] = "4*12+5";
            heights[CreatureConstants.Elf_Gray][GenderConstants.Male] = "4*12+5";
            heights[CreatureConstants.Elf_Gray][CreatureConstants.Elf_Gray] = "2d6";
            heights[CreatureConstants.Elf_Half][GenderConstants.Female] = "4*12+5";
            heights[CreatureConstants.Elf_Half][GenderConstants.Male] = "4*12+7";
            heights[CreatureConstants.Elf_Half][CreatureConstants.Elf_Half] = "2d8";
            heights[CreatureConstants.Elf_High][GenderConstants.Female] = "4*12+5";
            heights[CreatureConstants.Elf_High][GenderConstants.Male] = "4*12+5";
            heights[CreatureConstants.Elf_High][CreatureConstants.Elf_High] = "2d6";
            heights[CreatureConstants.Elf_Wild][GenderConstants.Female] = "4*12+5";
            heights[CreatureConstants.Elf_Wild][GenderConstants.Male] = "4*12+5";
            heights[CreatureConstants.Elf_Wild][CreatureConstants.Elf_Wild] = "2d6";
            heights[CreatureConstants.Elf_Wood][GenderConstants.Female] = "4*12+5";
            heights[CreatureConstants.Elf_Wood][GenderConstants.Male] = "4*12+5";
            heights[CreatureConstants.Elf_Wood][CreatureConstants.Elf_Wood] = "2d6";
            heights[CreatureConstants.Gnome_Forest][GenderConstants.Female] = "2*12+10";
            heights[CreatureConstants.Gnome_Forest][GenderConstants.Male] = "3*12+0";
            heights[CreatureConstants.Gnome_Forest][CreatureConstants.Gnome_Forest] = "2d4";
            heights[CreatureConstants.Gnome_Rock][GenderConstants.Female] = "2*12+10";
            heights[CreatureConstants.Gnome_Rock][GenderConstants.Male] = "3*12+0";
            heights[CreatureConstants.Gnome_Rock][CreatureConstants.Gnome_Rock] = "2d4";
            heights[CreatureConstants.Gnome_Svirfneblin][GenderConstants.Female] = "2*12+10";
            heights[CreatureConstants.Gnome_Svirfneblin][GenderConstants.Male] = "3*12+0";
            heights[CreatureConstants.Gnome_Svirfneblin][CreatureConstants.Gnome_Svirfneblin] = "2d4";
            heights[CreatureConstants.Goblin][GenderConstants.Female] = "2*12+6";
            heights[CreatureConstants.Goblin][GenderConstants.Male] = "2*12+8";
            heights[CreatureConstants.Goblin][CreatureConstants.Goblin] = "2d4";
            heights[CreatureConstants.Halfling_Deep][GenderConstants.Female] = "2*12+6";
            heights[CreatureConstants.Halfling_Deep][GenderConstants.Male] = "2*12+8";
            heights[CreatureConstants.Halfling_Deep][CreatureConstants.Halfling_Deep] = "2d4";
            heights[CreatureConstants.Halfling_Lightfoot][GenderConstants.Female] = "2*12+6";
            heights[CreatureConstants.Halfling_Lightfoot][GenderConstants.Male] = "2*12+8";
            heights[CreatureConstants.Halfling_Lightfoot][CreatureConstants.Halfling_Lightfoot] = "2d4";
            heights[CreatureConstants.Halfling_Tallfellow][GenderConstants.Female] = "3*12+6";
            heights[CreatureConstants.Halfling_Tallfellow][GenderConstants.Male] = "3*12+8";
            heights[CreatureConstants.Halfling_Tallfellow][CreatureConstants.Halfling_Tallfellow] = "2d4";
            heights[CreatureConstants.Hobgoblin][GenderConstants.Female] = "4*12+0";
            heights[CreatureConstants.Hobgoblin][GenderConstants.Male] = "4*12+2";
            heights[CreatureConstants.Hobgoblin][CreatureConstants.Hobgoblin] = "2d8";
            heights[CreatureConstants.Human][GenderConstants.Female] = "4*12+5";
            heights[CreatureConstants.Human][GenderConstants.Male] = "4*12+10";
            heights[CreatureConstants.Human][CreatureConstants.Human] = "2d10";
            heights[CreatureConstants.Kobold][GenderConstants.Female] = "2*12+4";
            heights[CreatureConstants.Kobold][GenderConstants.Male] = "2*12+6";
            heights[CreatureConstants.Kobold][CreatureConstants.Kobold] = "2d4";
            heights[CreatureConstants.Merfolk][GenderConstants.Female] = "5*12+8";
            heights[CreatureConstants.Merfolk][GenderConstants.Male] = "5*12+10";
            heights[CreatureConstants.Merfolk][CreatureConstants.Merfolk] = "2d10";
            heights[CreatureConstants.Orc][GenderConstants.Female] = "5*12+1";
            heights[CreatureConstants.Orc][GenderConstants.Male] = "4*12+9";
            heights[CreatureConstants.Orc][CreatureConstants.Orc] = "2d12";
            heights[CreatureConstants.Orc_Half][GenderConstants.Female] = "4*12+5";
            heights[CreatureConstants.Orc_Half][GenderConstants.Male] = "4*12+10";
            heights[CreatureConstants.Orc_Half][CreatureConstants.Orc_Half] = "2d12";
            heights[CreatureConstants.Tiefling][GenderConstants.Female] = "4*12+5";
            heights[CreatureConstants.Tiefling][GenderConstants.Male] = "4*12+10";
            heights[CreatureConstants.Tiefling][CreatureConstants.Tiefling] = "2d10";

            return heights;
        }

        public static IEnumerable CreatureHeightsData => GetCreatureHeights().Select(t => new TestCaseData(t.Key, t.Value));

        private static string GetGenderFromAverage(int average)
        {
            var gender = average * 8 / 10;
            return gender.ToString();
        }

        private static string GetCreatureFromAverage(int average)
        {
            var baseQuantity = average * 8 / 10;
            var lower = average * 9 / 10;
            var upper = average * 11 / 10;
            var creature = RollHelper.GetRollWithFewestDice(baseQuantity, lower, upper);

            return creature;
        }

        [TestCase(CreatureConstants.Aboleth, GenderConstants.Hermaphrodite, 20 * 12)]
        [TestCase(CreatureConstants.Achaierai, GenderConstants.Male, 15 * 12)]
        [TestCase(CreatureConstants.Achaierai, GenderConstants.Female, 15 * 12)]
        [TestCase(CreatureConstants.Androsphinx, GenderConstants.Male, 10 * 12)]
        [TestCase(CreatureConstants.Angel_AstralDeva, GenderConstants.Male, 7 * 12 + 3)]
        [TestCase(CreatureConstants.Angel_AstralDeva, GenderConstants.Female, 7 * 12 + 3)]
        [TestCase(CreatureConstants.Angel_Planetar, GenderConstants.Male, 8 * 12 + 6)]
        [TestCase(CreatureConstants.Angel_Planetar, GenderConstants.Female, 8 * 12 + 6)]
        [TestCase(CreatureConstants.Angel_Solar, GenderConstants.Male, 9 * 12 + 6)]
        [TestCase(CreatureConstants.Angel_Solar, GenderConstants.Female, 9 * 12 + 6)]
        [TestCase(CreatureConstants.AnimatedObject_Anvil_Colossal, GenderConstants.Agender, 36 * 12)]
        [TestCase(CreatureConstants.AnimatedObject_Anvil_Gargantuan, GenderConstants.Agender, 18 * 12)]
        [TestCase(CreatureConstants.AnimatedObject_Anvil_Huge, GenderConstants.Agender, 9 * 12)]
        [TestCase(CreatureConstants.AnimatedObject_Anvil_Large, GenderConstants.Agender, (9 * 12 + 6) / 2)]
        [TestCase(CreatureConstants.AnimatedObject_Anvil_Tiny, GenderConstants.Agender, 10)]
        public void RollCalculationsAreAccurate(string creature, string gender, int average)
        {
            var heights = GetCreatureHeights();

            var baseHeight = dice.Roll(heights[creature][gender]).AsSum();
            var multiplierMin = dice.Roll(heights[creature][creature]).AsPotentialMinimum();
            var multiplierAvg = dice.Roll(heights[creature][creature]).AsPotentialAverage();
            var multiplierMax = dice.Roll(heights[creature][creature]).AsPotentialMaximum();

            Assert.That(baseHeight + multiplierMin, Is.EqualTo(average * 0.9).Within(1), "Min (-10%)");
            Assert.That(baseHeight + multiplierAvg, Is.EqualTo(average).Within(1), "Average");
            Assert.That(baseHeight + multiplierMax, Is.EqualTo(average * 1.1).Within(1), "Max (+10%)");
        }
    }
}