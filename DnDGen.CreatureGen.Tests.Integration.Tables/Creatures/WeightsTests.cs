using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Tables;
using DnDGen.Infrastructure.Selectors.Collections;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Integration.Tables.Creatures
{
    [TestFixture]
    public class WeightsTests : TypesAndAmountsTests
    {
        private ICollectionSelector collectionSelector;

        protected override string tableName => TableNameConstants.TypeAndAmount.Weights;

        [SetUp]
        public void Setup()
        {
            collectionSelector = GetNewInstanceOf<ICollectionSelector>();
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

        public static IEnumerable CreatureWeightsData
        {
            get
            {
                var weights = new Dictionary<string, Dictionary<string, string>>();
                var creatures = CreatureConstants.GetAll();

                foreach (var creature in creatures)
                {
                    weights[creature] = new Dictionary<string, string>();
                }

                weights[CreatureConstants.Aasimar][GenderConstants.Female] = "90";
                weights[CreatureConstants.Aasimar][GenderConstants.Male] = "110";
                weights[CreatureConstants.Aasimar][CreatureConstants.Aasimar] = "2d4"; //x5
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

                foreach (var testCase in weights)
                {
                    yield return new TestCaseData(testCase.Key, testCase.Value);
                }
            }
        }
    }
}