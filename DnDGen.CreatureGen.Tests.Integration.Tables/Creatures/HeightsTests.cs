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
            heights[CreatureConstants.Angel_AstralDeva][GenderConstants.Female] = "82";
            heights[CreatureConstants.Angel_AstralDeva][GenderConstants.Male] = "82";
            heights[CreatureConstants.Angel_AstralDeva][CreatureConstants.Angel_AstralDeva] = RollHelper.GetRollWithFewestDice(82, 7 * 12, 7 * 12 + 6);
            heights[CreatureConstants.Angel_Planetar][GenderConstants.Female] = "92";
            heights[CreatureConstants.Angel_Planetar][GenderConstants.Male] = "92";
            heights[CreatureConstants.Angel_Planetar][CreatureConstants.Angel_Planetar] = RollHelper.GetRollWithFewestDice(92, 8 * 12, 9 * 12);
            heights[CreatureConstants.Angel_Solar][GenderConstants.Female] = "104";
            heights[CreatureConstants.Angel_Solar][GenderConstants.Male] = "104";
            heights[CreatureConstants.Angel_Solar][CreatureConstants.Angel_Solar] = RollHelper.GetRollWithFewestDice(104, 9 * 12, 10 * 12);
            heights[CreatureConstants.Ankheg][GenderConstants.Female] = GetGenderFromAverage(10 * 12);
            heights[CreatureConstants.Ankheg][GenderConstants.Male] = GetGenderFromAverage(10 * 12);
            heights[CreatureConstants.Ankheg][CreatureConstants.Ankheg] = GetCreatureFromAverage(10 * 12);
            heights[CreatureConstants.Annis][GenderConstants.Female] = GetGenderFromAverage(8 * 12);
            heights[CreatureConstants.Annis][CreatureConstants.Annis] = GetCreatureFromAverage(8 * 12);
            heights[CreatureConstants.Ape][GenderConstants.Female] = "60";
            heights[CreatureConstants.Ape][GenderConstants.Male] = "60";
            heights[CreatureConstants.Ape][CreatureConstants.Ape] = RollHelper.GetRollWithFewestDice(5 * 12, 5 * 12 + 6, 6 * 12);
            heights[CreatureConstants.Ape_Dire][GenderConstants.Female] = GetGenderFromAverage(9 * 12);
            heights[CreatureConstants.Ape_Dire][GenderConstants.Male] = GetGenderFromAverage(9 * 12);
            heights[CreatureConstants.Ape_Dire][CreatureConstants.Ape_Dire] = GetCreatureFromAverage(9 * 12);
            //INFO: Based on Half-Elf, since could be Human, Half-Elf, or Drow
            heights[CreatureConstants.Aranea][GenderConstants.Female] = "4*12+5";
            heights[CreatureConstants.Aranea][GenderConstants.Male] = "4*12+7";
            heights[CreatureConstants.Aranea][CreatureConstants.Aranea] = "2d8";
            heights[CreatureConstants.Arrowhawk_Juvenile][GenderConstants.Female] = GetGenderFromAverage(5 * 12);
            heights[CreatureConstants.Arrowhawk_Juvenile][GenderConstants.Male] = GetGenderFromAverage(5 * 12);
            heights[CreatureConstants.Arrowhawk_Juvenile][CreatureConstants.Arrowhawk_Juvenile] = GetCreatureFromAverage(5 * 12);
            heights[CreatureConstants.Arrowhawk_Adult][GenderConstants.Female] = GetGenderFromAverage(10 * 12);
            heights[CreatureConstants.Arrowhawk_Adult][GenderConstants.Male] = GetGenderFromAverage(10 * 12);
            heights[CreatureConstants.Arrowhawk_Adult][CreatureConstants.Arrowhawk_Adult] = GetCreatureFromAverage(10 * 12);
            heights[CreatureConstants.Arrowhawk_Elder][GenderConstants.Female] = GetGenderFromAverage(20 * 12);
            heights[CreatureConstants.Arrowhawk_Elder][GenderConstants.Male] = GetGenderFromAverage(20 * 12);
            heights[CreatureConstants.Arrowhawk_Elder][CreatureConstants.Arrowhawk_Elder] = GetCreatureFromAverage(20 * 12);
            heights[CreatureConstants.Athach][GenderConstants.Female] = GetGenderFromAverage(18 * 12);
            heights[CreatureConstants.Athach][GenderConstants.Male] = GetGenderFromAverage(18 * 12);
            heights[CreatureConstants.Athach][CreatureConstants.Athach] = GetCreatureFromAverage(18 * 12);
            heights[CreatureConstants.Avoral][GenderConstants.Female] = "6*12";
            heights[CreatureConstants.Avoral][GenderConstants.Male] = "6*12";
            heights[CreatureConstants.Avoral][GenderConstants.Agender] = "6*12";
            heights[CreatureConstants.Avoral][CreatureConstants.Avoral] = RollHelper.GetRollWithFewestDice(6 * 12, 6 * 12 + 6, 7 * 12);
            heights[CreatureConstants.Azer][GenderConstants.Male] = "4*12";
            heights[CreatureConstants.Azer][GenderConstants.Female] = "4*12";
            heights[CreatureConstants.Azer][GenderConstants.Agender] = "4*12";
            heights[CreatureConstants.Azer][CreatureConstants.Azer] = RollHelper.GetRollWithFewestDice(4 * 12, 4 * 12 + 5, 4 * 12 + 9);
            heights[CreatureConstants.Babau][GenderConstants.Agender] = "5*12";
            heights[CreatureConstants.Babau][CreatureConstants.Babau] = RollHelper.GetRollWithFewestDice(5 * 12, 6 * 12, 7 * 12);
            heights[CreatureConstants.Baboon][GenderConstants.Female] = "21";
            heights[CreatureConstants.Baboon][GenderConstants.Male] = "25";
            heights[CreatureConstants.Baboon][CreatureConstants.Baboon] = "1d6";
            heights[CreatureConstants.Badger][GenderConstants.Female] = "20";
            heights[CreatureConstants.Badger][GenderConstants.Male] = "20";
            heights[CreatureConstants.Badger][CreatureConstants.Badger] = RollHelper.GetRollWithFewestDice(20, 24, 36);
            heights[CreatureConstants.Badger_Dire][GenderConstants.Female] = "60";
            heights[CreatureConstants.Badger_Dire][GenderConstants.Male] = "60";
            heights[CreatureConstants.Badger_Dire][CreatureConstants.Badger_Dire] = RollHelper.GetRollWithFewestDice(60, 5 * 12, 7 * 12);
            heights[CreatureConstants.Balor][GenderConstants.Agender] = GetGenderFromAverage(12 * 12);
            heights[CreatureConstants.Balor][CreatureConstants.Balor] = GetCreatureFromAverage(12 * 12);
            heights[CreatureConstants.BarbedDevil_Hamatula][GenderConstants.Agender] = GetGenderFromAverage(7 * 12);
            heights[CreatureConstants.BarbedDevil_Hamatula][CreatureConstants.BarbedDevil_Hamatula] = GetCreatureFromAverage(7 * 12);
            heights[CreatureConstants.Barghest][GenderConstants.Agender] = GetGenderFromAverage(6 * 12);
            heights[CreatureConstants.Barghest][CreatureConstants.Barghest] = GetCreatureFromAverage(6 * 12);
            heights[CreatureConstants.Barghest_Greater][GenderConstants.Agender] = GetGenderFromAverage(6 * 12);
            heights[CreatureConstants.Barghest_Greater][CreatureConstants.Barghest_Greater] = GetCreatureFromAverage(6 * 12);
            heights[CreatureConstants.Bear_Brown][GenderConstants.Female] = GetGenderFromAverage(9 * 12);
            heights[CreatureConstants.Bear_Brown][GenderConstants.Male] = GetGenderFromAverage(9 * 12);
            heights[CreatureConstants.Bear_Brown][CreatureConstants.Bear_Brown] = GetCreatureFromAverage(9 * 12);
            heights[CreatureConstants.BeardedDevil_Barbazu][GenderConstants.Agender] = GetGenderFromAverage(6 * 12);
            heights[CreatureConstants.BeardedDevil_Barbazu][CreatureConstants.BeardedDevil_Barbazu] = GetCreatureFromAverage(6 * 12);
            heights[CreatureConstants.Bebilith][GenderConstants.Agender] = GetGenderFromAverage(14 * 12);
            heights[CreatureConstants.Bebilith][CreatureConstants.Bebilith] = GetCreatureFromAverage(14 * 12);
            heights[CreatureConstants.BoneDevil_Osyluth][GenderConstants.Agender] = GetGenderFromAverage(9 * 12);
            heights[CreatureConstants.BoneDevil_Osyluth][CreatureConstants.BoneDevil_Osyluth] = GetCreatureFromAverage(9 * 12);
            heights[CreatureConstants.Bugbear][GenderConstants.Female] = "5*12";
            heights[CreatureConstants.Bugbear][GenderConstants.Male] = "5*12";
            heights[CreatureConstants.Bugbear][CreatureConstants.Bugbear] = RollHelper.GetRollWithFewestDice(5 * 12, 6 * 12, 8 * 12);
            heights[CreatureConstants.Cat][GenderConstants.Female] = GetGenderFromAverage(17); //Small Animal
            heights[CreatureConstants.Cat][GenderConstants.Male] = GetGenderFromAverage(19);
            heights[CreatureConstants.Cat][CreatureConstants.Cat] = GetCreatureFromAverage(18);
            heights[CreatureConstants.Centaur][GenderConstants.Female] = "6*12";
            heights[CreatureConstants.Centaur][GenderConstants.Male] = "6*12";
            heights[CreatureConstants.Centaur][CreatureConstants.Centaur] = RollHelper.GetRollWithFewestDice(6 * 12, 7 * 12, 9 * 12);
            heights[CreatureConstants.ChainDevil_Kyton][GenderConstants.Agender] = GetGenderFromAverage(6 * 12);
            heights[CreatureConstants.ChainDevil_Kyton][CreatureConstants.ChainDevil_Kyton] = GetCreatureFromAverage(6 * 12);
            heights[CreatureConstants.Criosphinx][GenderConstants.Male] = GetGenderFromAverage(120);
            heights[CreatureConstants.Criosphinx][CreatureConstants.Criosphinx] = GetCreatureFromAverage(120);
            heights[CreatureConstants.Dretch][GenderConstants.Agender] = GetGenderFromAverage(4 * 12);
            heights[CreatureConstants.Dretch][CreatureConstants.Dretch] = GetCreatureFromAverage(4 * 12);
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
            heights[CreatureConstants.Elemental_Air_Small][GenderConstants.Agender] = GetGenderFromAverage(4 * 12);
            heights[CreatureConstants.Elemental_Air_Small][CreatureConstants.Elemental_Air_Small] = GetCreatureFromAverage(4 * 12);
            heights[CreatureConstants.Elemental_Air_Medium][GenderConstants.Agender] = GetGenderFromAverage(8 * 12);
            heights[CreatureConstants.Elemental_Air_Medium][CreatureConstants.Elemental_Air_Small] = GetCreatureFromAverage(8 * 12);
            heights[CreatureConstants.Elemental_Air_Large][GenderConstants.Agender] = GetGenderFromAverage(16 * 12);
            heights[CreatureConstants.Elemental_Air_Large][CreatureConstants.Elemental_Air_Small] = GetCreatureFromAverage(16 * 12);
            heights[CreatureConstants.Elemental_Air_Huge][GenderConstants.Agender] = GetGenderFromAverage(32 * 12);
            heights[CreatureConstants.Elemental_Air_Huge][CreatureConstants.Elemental_Air_Small] = GetCreatureFromAverage(32 * 12);
            heights[CreatureConstants.Elemental_Air_Greater][GenderConstants.Agender] = GetGenderFromAverage(36 * 12);
            heights[CreatureConstants.Elemental_Air_Greater][CreatureConstants.Elemental_Air_Small] = GetCreatureFromAverage(36 * 12);
            heights[CreatureConstants.Elemental_Air_Elder][GenderConstants.Agender] = GetGenderFromAverage(40 * 12);
            heights[CreatureConstants.Elemental_Air_Elder][CreatureConstants.Elemental_Air_Small] = GetCreatureFromAverage(40 * 12);
            heights[CreatureConstants.Elemental_Earth_Small][GenderConstants.Agender] = GetGenderFromAverage(4 * 12);
            heights[CreatureConstants.Elemental_Earth_Small][CreatureConstants.Elemental_Air_Small] = GetCreatureFromAverage(4 * 12);
            heights[CreatureConstants.Elemental_Earth_Medium][GenderConstants.Agender] = GetGenderFromAverage(8 * 12);
            heights[CreatureConstants.Elemental_Earth_Medium][CreatureConstants.Elemental_Air_Small] = GetCreatureFromAverage(8 * 12);
            heights[CreatureConstants.Elemental_Earth_Large][GenderConstants.Agender] = GetGenderFromAverage(16 * 12);
            heights[CreatureConstants.Elemental_Earth_Large][CreatureConstants.Elemental_Air_Small] = GetCreatureFromAverage(16 * 12);
            heights[CreatureConstants.Elemental_Earth_Huge][GenderConstants.Agender] = GetGenderFromAverage(32 * 12);
            heights[CreatureConstants.Elemental_Earth_Huge][CreatureConstants.Elemental_Air_Small] = GetCreatureFromAverage(32 * 12);
            heights[CreatureConstants.Elemental_Earth_Greater][GenderConstants.Agender] = GetGenderFromAverage(36 * 12);
            heights[CreatureConstants.Elemental_Earth_Greater][CreatureConstants.Elemental_Air_Small] = GetCreatureFromAverage(36 * 12);
            heights[CreatureConstants.Elemental_Earth_Elder][GenderConstants.Agender] = GetGenderFromAverage(40 * 12);
            heights[CreatureConstants.Elemental_Earth_Elder][CreatureConstants.Elemental_Air_Small] = GetCreatureFromAverage(40 * 12);
            heights[CreatureConstants.Elemental_Fire_Small][GenderConstants.Agender] = GetGenderFromAverage(4 * 12);
            heights[CreatureConstants.Elemental_Fire_Small][CreatureConstants.Elemental_Air_Small] = GetCreatureFromAverage(4 * 12);
            heights[CreatureConstants.Elemental_Fire_Medium][GenderConstants.Agender] = GetGenderFromAverage(8 * 12);
            heights[CreatureConstants.Elemental_Fire_Medium][CreatureConstants.Elemental_Air_Small] = GetCreatureFromAverage(8 * 12);
            heights[CreatureConstants.Elemental_Fire_Large][GenderConstants.Agender] = GetGenderFromAverage(16 * 12);
            heights[CreatureConstants.Elemental_Fire_Large][CreatureConstants.Elemental_Air_Small] = GetCreatureFromAverage(16 * 12);
            heights[CreatureConstants.Elemental_Fire_Huge][GenderConstants.Agender] = GetGenderFromAverage(32 * 12);
            heights[CreatureConstants.Elemental_Fire_Huge][CreatureConstants.Elemental_Air_Small] = GetCreatureFromAverage(32 * 12);
            heights[CreatureConstants.Elemental_Fire_Greater][GenderConstants.Agender] = GetGenderFromAverage(36 * 12);
            heights[CreatureConstants.Elemental_Fire_Greater][CreatureConstants.Elemental_Air_Small] = GetCreatureFromAverage(36 * 12);
            heights[CreatureConstants.Elemental_Fire_Elder][GenderConstants.Agender] = GetGenderFromAverage(40 * 12);
            heights[CreatureConstants.Elemental_Fire_Elder][CreatureConstants.Elemental_Air_Small] = GetCreatureFromAverage(40 * 12);
            heights[CreatureConstants.Elemental_Water_Small][GenderConstants.Agender] = GetGenderFromAverage(4 * 12);
            heights[CreatureConstants.Elemental_Water_Small][CreatureConstants.Elemental_Air_Small] = GetCreatureFromAverage(4 * 12);
            heights[CreatureConstants.Elemental_Water_Medium][GenderConstants.Agender] = GetGenderFromAverage(8 * 12);
            heights[CreatureConstants.Elemental_Water_Medium][CreatureConstants.Elemental_Air_Small] = GetCreatureFromAverage(8 * 12);
            heights[CreatureConstants.Elemental_Water_Large][GenderConstants.Agender] = GetGenderFromAverage(16 * 12);
            heights[CreatureConstants.Elemental_Water_Large][CreatureConstants.Elemental_Air_Small] = GetCreatureFromAverage(16 * 12);
            heights[CreatureConstants.Elemental_Water_Huge][GenderConstants.Agender] = GetGenderFromAverage(32 * 12);
            heights[CreatureConstants.Elemental_Water_Huge][CreatureConstants.Elemental_Air_Small] = GetCreatureFromAverage(32 * 12);
            heights[CreatureConstants.Elemental_Water_Greater][GenderConstants.Agender] = GetGenderFromAverage(36 * 12);
            heights[CreatureConstants.Elemental_Water_Greater][CreatureConstants.Elemental_Air_Small] = GetCreatureFromAverage(36 * 12);
            heights[CreatureConstants.Elemental_Water_Elder][GenderConstants.Agender] = GetGenderFromAverage(40 * 12);
            heights[CreatureConstants.Elemental_Water_Elder][CreatureConstants.Elemental_Air_Small] = GetCreatureFromAverage(40 * 12);
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
            heights[CreatureConstants.Erinyes][GenderConstants.Agender] = GetGenderFromAverage(6 * 12);
            heights[CreatureConstants.Erinyes][CreatureConstants.Erinyes] = GetCreatureFromAverage(6 * 12);
            heights[CreatureConstants.Ettin][GenderConstants.Female] = "12*12+2";
            heights[CreatureConstants.Ettin][GenderConstants.Male] = "13*12-2";
            heights[CreatureConstants.Ettin][CreatureConstants.Ettin] = "2d6";
            heights[CreatureConstants.Giant_Cloud][GenderConstants.Female] = "290"; //Huge
            heights[CreatureConstants.Giant_Cloud][GenderConstants.Male] = "270";
            heights[CreatureConstants.Giant_Cloud][CreatureConstants.Giant_Cloud] = "3d10";
            heights[CreatureConstants.Giant_Hill][GenderConstants.Female] = "15*12+4"; //Large
            heights[CreatureConstants.Giant_Hill][GenderConstants.Male] = "16*12";
            heights[CreatureConstants.Giant_Hill][CreatureConstants.Giant_Hill] = "1d12";
            heights[CreatureConstants.Glabrezu][GenderConstants.Agender] = "8*12";
            heights[CreatureConstants.Glabrezu][CreatureConstants.Glabrezu] = RollHelper.GetRollWithFewestDice(8 * 12, 9 * 12, 15 * 12);
            heights[CreatureConstants.Gnoll][GenderConstants.Female] = "6*12";
            heights[CreatureConstants.Gnoll][GenderConstants.Male] = "6*12";
            heights[CreatureConstants.Gnoll][CreatureConstants.Gnoll] = RollHelper.GetRollWithFewestDice(6 * 12, 7 * 12, 7 * 12 + 6);
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
            heights[CreatureConstants.GreenHag][GenderConstants.Female] = "4*12+5";
            heights[CreatureConstants.GreenHag][CreatureConstants.GreenHag] = "2d10";
            heights[CreatureConstants.Grig][GenderConstants.Female] = GetGenderFromAverage(18); //Tiny
            heights[CreatureConstants.Grig][GenderConstants.Male] = GetGenderFromAverage(18);
            heights[CreatureConstants.Grig][CreatureConstants.Grig] = GetCreatureFromAverage(18);
            heights[CreatureConstants.Grig_WithFiddle][GenderConstants.Female] = GetGenderFromAverage(18); //Tiny
            heights[CreatureConstants.Grig_WithFiddle][GenderConstants.Male] = GetGenderFromAverage(18);
            heights[CreatureConstants.Grig_WithFiddle][CreatureConstants.Grig_WithFiddle] = GetCreatureFromAverage(18);
            heights[CreatureConstants.Halfling_Deep][GenderConstants.Female] = "2*12+6";
            heights[CreatureConstants.Halfling_Deep][GenderConstants.Male] = "2*12+8";
            heights[CreatureConstants.Halfling_Deep][CreatureConstants.Halfling_Deep] = "2d4";
            heights[CreatureConstants.Halfling_Lightfoot][GenderConstants.Female] = "2*12+6"; //Small
            heights[CreatureConstants.Halfling_Lightfoot][GenderConstants.Male] = "2*12+8";
            heights[CreatureConstants.Halfling_Lightfoot][CreatureConstants.Halfling_Lightfoot] = "2d4";
            heights[CreatureConstants.Halfling_Tallfellow][GenderConstants.Female] = "3*12+6";
            heights[CreatureConstants.Halfling_Tallfellow][GenderConstants.Male] = "3*12+8";
            heights[CreatureConstants.Halfling_Tallfellow][CreatureConstants.Halfling_Tallfellow] = "2d4";
            heights[CreatureConstants.Hellcat_Bezekira][GenderConstants.Agender] = GetGenderFromAverage(9 * 12);
            heights[CreatureConstants.Hellcat_Bezekira][CreatureConstants.Hellcat_Bezekira] = GetCreatureFromAverage(9 * 12);
            heights[CreatureConstants.Hezrou][GenderConstants.Agender] = GetGenderFromAverage(8 * 12);
            heights[CreatureConstants.Hezrou][CreatureConstants.Hezrou] = GetCreatureFromAverage(8 * 12);
            heights[CreatureConstants.Hieracosphinx][GenderConstants.Male] = GetGenderFromAverage(10 * 12);
            heights[CreatureConstants.Hieracosphinx][CreatureConstants.Hieracosphinx] = GetCreatureFromAverage(10 * 12);
            heights[CreatureConstants.Hobgoblin][GenderConstants.Female] = "4*12+0";
            heights[CreatureConstants.Hobgoblin][GenderConstants.Male] = "4*12+2";
            heights[CreatureConstants.Hobgoblin][CreatureConstants.Hobgoblin] = "2d8";
            heights[CreatureConstants.HornedDevil_Cornugon][GenderConstants.Agender] = GetGenderFromAverage(9 * 12);
            heights[CreatureConstants.HornedDevil_Cornugon][CreatureConstants.HornedDevil_Cornugon] = GetCreatureFromAverage(9 * 12);
            heights[CreatureConstants.Horse_Heavy][GenderConstants.Female] = GetGenderFromAverage(6 * 12);
            heights[CreatureConstants.Horse_Heavy][GenderConstants.Male] = GetGenderFromAverage(6 * 12);
            heights[CreatureConstants.Horse_Heavy][CreatureConstants.Horse_Heavy] = GetCreatureFromAverage(6 * 12);
            heights[CreatureConstants.Horse_Light][GenderConstants.Female] = "56";
            heights[CreatureConstants.Horse_Light][GenderConstants.Male] = "56";
            heights[CreatureConstants.Horse_Light][CreatureConstants.Horse_Light] = "1d4";
            heights[CreatureConstants.Horse_Heavy_War][GenderConstants.Female] = GetGenderFromAverage(6 * 12);
            heights[CreatureConstants.Horse_Heavy_War][GenderConstants.Male] = GetGenderFromAverage(6 * 12);
            heights[CreatureConstants.Horse_Heavy_War][CreatureConstants.Horse_Heavy_War] = GetCreatureFromAverage(6 * 12);
            heights[CreatureConstants.Horse_Light_War][GenderConstants.Female] = "56";
            heights[CreatureConstants.Horse_Light_War][GenderConstants.Male] = "56";
            heights[CreatureConstants.Horse_Light_War][CreatureConstants.Horse_Light_War] = "1d4";
            heights[CreatureConstants.Human][GenderConstants.Female] = "4*12+5"; //Medium
            heights[CreatureConstants.Human][GenderConstants.Male] = "4*12+10";
            heights[CreatureConstants.Human][CreatureConstants.Human] = "2d10";
            heights[CreatureConstants.IceDevil_Gelugon][GenderConstants.Agender] = GetGenderFromAverage(12 * 12);
            heights[CreatureConstants.IceDevil_Gelugon][CreatureConstants.IceDevil_Gelugon] = GetCreatureFromAverage(12 * 12);
            heights[CreatureConstants.Imp][GenderConstants.Agender] = GetGenderFromAverage(2 * 12);
            heights[CreatureConstants.Imp][CreatureConstants.Imp] = GetCreatureFromAverage(2 * 12);
            heights[CreatureConstants.Kobold][GenderConstants.Female] = "2*12+4";
            heights[CreatureConstants.Kobold][GenderConstants.Male] = "2*12+6";
            heights[CreatureConstants.Kobold][CreatureConstants.Kobold] = "2d4";
            heights[CreatureConstants.Lemure][GenderConstants.Agender] = GetGenderFromAverage(5 * 12);
            heights[CreatureConstants.Lemure][CreatureConstants.Lemure] = GetCreatureFromAverage(5 * 12);
            heights[CreatureConstants.Leonal][GenderConstants.Female] = "5*12+7";
            heights[CreatureConstants.Leonal][GenderConstants.Male] = "5*12+7";
            heights[CreatureConstants.Leonal][CreatureConstants.Leonal] = "2d4";
            heights[CreatureConstants.Lizardfolk][GenderConstants.Female] = "5*12";
            heights[CreatureConstants.Lizardfolk][GenderConstants.Male] = "5*12";
            heights[CreatureConstants.Lizardfolk][CreatureConstants.Lizardfolk] = RollHelper.GetRollWithFewestDice(5 * 12, 6 * 12, 7 * 12);
            heights[CreatureConstants.Locathah][GenderConstants.Female] = GetGenderFromAverage(5 * 12);
            heights[CreatureConstants.Locathah][GenderConstants.Male] = GetGenderFromAverage(5 * 12);
            heights[CreatureConstants.Locathah][CreatureConstants.Locathah] = GetCreatureFromAverage(5 * 12);
            heights[CreatureConstants.Marilith][GenderConstants.Female] = GetGenderFromAverage(20 * 12);
            heights[CreatureConstants.Marilith][CreatureConstants.Marilith] = GetCreatureFromAverage(20 * 12);
            heights[CreatureConstants.Mephit_Air][GenderConstants.Agender] = GetGenderFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Air][GenderConstants.Female] = GetGenderFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Air][GenderConstants.Male] = GetGenderFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Air][CreatureConstants.Mephit_Air] = GetCreatureFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Dust][GenderConstants.Agender] = GetGenderFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Dust][GenderConstants.Female] = GetGenderFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Dust][GenderConstants.Male] = GetGenderFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Dust][CreatureConstants.Mephit_Air] = GetCreatureFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Earth][GenderConstants.Agender] = GetGenderFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Earth][GenderConstants.Female] = GetGenderFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Earth][GenderConstants.Male] = GetGenderFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Earth][CreatureConstants.Mephit_Air] = GetCreatureFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Fire][GenderConstants.Agender] = GetGenderFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Fire][GenderConstants.Female] = GetGenderFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Fire][GenderConstants.Male] = GetGenderFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Fire][CreatureConstants.Mephit_Air] = GetCreatureFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Ice][GenderConstants.Agender] = GetGenderFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Ice][GenderConstants.Female] = GetGenderFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Ice][GenderConstants.Male] = GetGenderFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Ice][CreatureConstants.Mephit_Air] = GetCreatureFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Magma][GenderConstants.Agender] = GetGenderFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Magma][GenderConstants.Female] = GetGenderFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Magma][GenderConstants.Male] = GetGenderFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Magma][CreatureConstants.Mephit_Air] = GetCreatureFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Ooze][GenderConstants.Agender] = GetGenderFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Ooze][GenderConstants.Female] = GetGenderFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Ooze][GenderConstants.Male] = GetGenderFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Ooze][CreatureConstants.Mephit_Air] = GetCreatureFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Salt][GenderConstants.Agender] = GetGenderFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Salt][GenderConstants.Female] = GetGenderFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Salt][GenderConstants.Male] = GetGenderFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Salt][CreatureConstants.Mephit_Air] = GetCreatureFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Steam][GenderConstants.Agender] = GetGenderFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Steam][GenderConstants.Female] = GetGenderFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Steam][GenderConstants.Male] = GetGenderFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Steam][CreatureConstants.Mephit_Air] = GetCreatureFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Water][GenderConstants.Agender] = GetGenderFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Water][GenderConstants.Female] = GetGenderFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Water][GenderConstants.Male] = GetGenderFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Water][CreatureConstants.Mephit_Air] = GetCreatureFromAverage(4 * 12);
            heights[CreatureConstants.Merfolk][GenderConstants.Female] = "5*12+8";
            heights[CreatureConstants.Merfolk][GenderConstants.Male] = "5*12+10";
            heights[CreatureConstants.Merfolk][CreatureConstants.Merfolk] = "2d10";
            heights[CreatureConstants.Minotaur][GenderConstants.Female] = GetGenderFromAverage(7 * 12);
            heights[CreatureConstants.Minotaur][GenderConstants.Male] = GetGenderFromAverage(9 * 12);
            heights[CreatureConstants.Minotaur][CreatureConstants.Minotaur] = GetCreatureFromAverage(8 * 12);
            heights[CreatureConstants.Nalfeshnee][GenderConstants.Agender] = "9*12";
            heights[CreatureConstants.Nalfeshnee][CreatureConstants.Nalfeshnee] = RollHelper.GetRollWithFewestDice(9 * 12, 10 * 12, 20 * 12);
            heights[CreatureConstants.Ogre][GenderConstants.Female] = "110";
            heights[CreatureConstants.Ogre][GenderConstants.Male] = "120";
            heights[CreatureConstants.Ogre][CreatureConstants.Ogre] = "1d10";
            heights[CreatureConstants.Ogre_Merrow][GenderConstants.Female] = "110";
            heights[CreatureConstants.Ogre_Merrow][GenderConstants.Male] = "120";
            heights[CreatureConstants.Ogre_Merrow][CreatureConstants.Ogre] = "1d10";
            heights[CreatureConstants.OgreMage][GenderConstants.Female] = GetGenderFromAverage(10 * 12);
            heights[CreatureConstants.OgreMage][GenderConstants.Male] = GetGenderFromAverage(10 * 12);
            heights[CreatureConstants.OgreMage][CreatureConstants.OgreMage] = GetCreatureFromAverage(10 * 12);
            heights[CreatureConstants.Orc][GenderConstants.Female] = "5*12+1";
            heights[CreatureConstants.Orc][GenderConstants.Male] = "4*12+9";
            heights[CreatureConstants.Orc][CreatureConstants.Orc] = "2d12";
            heights[CreatureConstants.Orc_Half][GenderConstants.Female] = "4*12+5";
            heights[CreatureConstants.Orc_Half][GenderConstants.Male] = "4*12+10";
            heights[CreatureConstants.Orc_Half][CreatureConstants.Orc_Half] = "2d12";
            heights[CreatureConstants.PitFiend][GenderConstants.Agender] = GetGenderFromAverage(12 * 12);
            heights[CreatureConstants.PitFiend][CreatureConstants.PitFiend] = GetCreatureFromAverage(12 * 12);
            heights[CreatureConstants.Pixie][GenderConstants.Female] = "10";
            heights[CreatureConstants.Pixie][GenderConstants.Male] = "10";
            heights[CreatureConstants.Pixie][CreatureConstants.Pixie] = RollHelper.GetRollWithMostEvenDistribution(10, 12, 30);
            heights[CreatureConstants.Pixie_WithIrresistibleDance][GenderConstants.Female] = "10";
            heights[CreatureConstants.Pixie_WithIrresistibleDance][GenderConstants.Male] = "10";
            heights[CreatureConstants.Pixie_WithIrresistibleDance][CreatureConstants.Pixie_WithIrresistibleDance] = RollHelper.GetRollWithMostEvenDistribution(10, 12, 30);
            heights[CreatureConstants.Quasit][GenderConstants.Agender] = "10";
            heights[CreatureConstants.Quasit][CreatureConstants.Quasit] = RollHelper.GetRollWithFewestDice(10, 1 * 12, 2 * 12);
            heights[CreatureConstants.Retriever][GenderConstants.Agender] = GetGenderFromAverage(14 * 12);
            heights[CreatureConstants.Retriever][CreatureConstants.Retriever] = GetCreatureFromAverage(14 * 12);
            heights[CreatureConstants.Salamander_Flamebrother][GenderConstants.Agender] = "20";
            heights[CreatureConstants.Salamander_Flamebrother][CreatureConstants.Salamander_Flamebrother] = RollHelper.GetRollWithMostEvenDistribution(20, 24, 48);
            heights[CreatureConstants.Salamander_Average][GenderConstants.Agender] = "40";
            heights[CreatureConstants.Salamander_Average][CreatureConstants.Salamander_Average] = RollHelper.GetRollWithMostEvenDistribution(40, 48, 8 * 12);
            heights[CreatureConstants.Salamander_Noble][GenderConstants.Agender] = "7*12";
            heights[CreatureConstants.Salamander_Noble][CreatureConstants.Salamander_Noble] = RollHelper.GetRollWithMostEvenDistribution(20, 8 * 12, 16 * 12);
            heights[CreatureConstants.SeaHag][GenderConstants.Female] = "4*12+5";
            heights[CreatureConstants.SeaHag][CreatureConstants.GreenHag] = "2d10";
            heights[CreatureConstants.Succubus][GenderConstants.Female] = GetGenderFromAverage(6 * 12);
            heights[CreatureConstants.Succubus][GenderConstants.Male] = GetGenderFromAverage(6 * 12);
            heights[CreatureConstants.Succubus][CreatureConstants.Succubus] = GetCreatureFromAverage(6 * 12);
            heights[CreatureConstants.Tiefling][GenderConstants.Female] = "4*12+5";
            heights[CreatureConstants.Tiefling][GenderConstants.Male] = "4*12+10";
            heights[CreatureConstants.Tiefling][CreatureConstants.Tiefling] = "2d10";
            heights[CreatureConstants.Tojanida_Juvenile][GenderConstants.Female] = GetGenderFromAverage(3 * 12);
            heights[CreatureConstants.Tojanida_Juvenile][GenderConstants.Male] = GetGenderFromAverage(3 * 12);
            heights[CreatureConstants.Tojanida_Juvenile][CreatureConstants.Tojanida_Juvenile] = GetCreatureFromAverage(3 * 12);
            heights[CreatureConstants.Tojanida_Adult][GenderConstants.Female] = GetGenderFromAverage(6 * 12);
            heights[CreatureConstants.Tojanida_Adult][GenderConstants.Male] = GetGenderFromAverage(6 * 12);
            heights[CreatureConstants.Tojanida_Adult][CreatureConstants.Tojanida_Adult] = GetCreatureFromAverage(6 * 12);
            heights[CreatureConstants.Tojanida_Elder][GenderConstants.Female] = GetGenderFromAverage(9 * 12);
            heights[CreatureConstants.Tojanida_Elder][GenderConstants.Male] = GetGenderFromAverage(9 * 12);
            heights[CreatureConstants.Tojanida_Elder][CreatureConstants.Tojanida_Elder] = GetCreatureFromAverage(9 * 12);
            heights[CreatureConstants.Vrock][GenderConstants.Agender] = GetGenderFromAverage(8 * 12);
            heights[CreatureConstants.Vrock][CreatureConstants.Vrock] = GetCreatureFromAverage(8 * 12);
            heights[CreatureConstants.Whale_Baleen][GenderConstants.Female] = "30*12";
            heights[CreatureConstants.Whale_Baleen][GenderConstants.Male] = "30*12";
            heights[CreatureConstants.Whale_Baleen][CreatureConstants.Whale_Baleen] = RollHelper.GetRollWithMostEvenDistribution(30 * 12, 30 * 12, 60 * 12);
            heights[CreatureConstants.Whale_Cachalot][GenderConstants.Female] = GetGenderFromAverage(60 * 12);
            heights[CreatureConstants.Whale_Cachalot][GenderConstants.Male] = GetGenderFromAverage(60 * 12);
            heights[CreatureConstants.Whale_Cachalot][CreatureConstants.Whale_Cachalot] = GetCreatureFromAverage(60 * 12);
            heights[CreatureConstants.Whale_Orca][GenderConstants.Female] = GetGenderFromAverage(30 * 12);
            heights[CreatureConstants.Whale_Orca][GenderConstants.Male] = GetGenderFromAverage(30 * 12);
            heights[CreatureConstants.Whale_Orca][CreatureConstants.Whale_Cachalot] = GetCreatureFromAverage(30 * 12);
            heights[CreatureConstants.Wolf][GenderConstants.Female] = "39"; //Medium Animal
            heights[CreatureConstants.Wolf][GenderConstants.Male] = "39";
            heights[CreatureConstants.Wolf][CreatureConstants.Wolf] = "2d12";
            heights[CreatureConstants.Xorn_Minor][GenderConstants.Agender] = GetGenderFromAverage(3 * 12);
            heights[CreatureConstants.Xorn_Minor][CreatureConstants.Xorn_Minor] = GetCreatureFromAverage(3 * 12);
            heights[CreatureConstants.Xorn_Average][GenderConstants.Agender] = GetGenderFromAverage(5 * 12);
            heights[CreatureConstants.Xorn_Average][CreatureConstants.Xorn_Average] = GetCreatureFromAverage(5 * 12);
            heights[CreatureConstants.Xorn_Elder][GenderConstants.Agender] = GetGenderFromAverage(8 * 12);
            heights[CreatureConstants.Xorn_Elder][CreatureConstants.Xorn_Elder] = GetCreatureFromAverage(8 * 12);

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
        [TestCase(CreatureConstants.Balor, GenderConstants.Agender, 12 * 12)]
        [TestCase(CreatureConstants.Baboon, GenderConstants.Female, 24)]
        [TestCase(CreatureConstants.Baboon, GenderConstants.Male, 28)]
        [TestCase(CreatureConstants.Horse_Heavy, GenderConstants.Male, 6 * 12)]
        [TestCase(CreatureConstants.Horse_Heavy, GenderConstants.Female, 6 * 12)]
        [TestCase(CreatureConstants.Locathah, GenderConstants.Male, 5 * 12)]
        [TestCase(CreatureConstants.Locathah, GenderConstants.Female, 5 * 12)]
        [TestCase(CreatureConstants.Minotaur, GenderConstants.Male, 9 * 12)]
        [TestCase(CreatureConstants.Minotaur, GenderConstants.Female, 7 * 12)]
        public void RollCalculationsAreAccurate(string creature, string gender, int average)
        {
            var heights = GetCreatureHeights();

            var baseHeight = dice.Roll(heights[creature][gender]).AsSum();
            var multiplierMin = dice.Roll(heights[creature][creature]).AsPotentialMinimum();
            var multiplierAvg = dice.Roll(heights[creature][creature]).AsPotentialAverage();
            var multiplierMax = dice.Roll(heights[creature][creature]).AsPotentialMaximum();
            var theoreticalRoll = RollHelper.GetRollWithFewestDice(average * 9 / 10, average * 11 / 10);

            Assert.That(baseHeight + multiplierMin, Is.EqualTo(average * 0.9).Within(1), $"Min (-10%); Theoretical: {theoreticalRoll}");
            Assert.That(baseHeight + multiplierAvg, Is.EqualTo(average).Within(1), $"Average; Theoretical: {theoreticalRoll}");
            Assert.That(baseHeight + multiplierMax, Is.EqualTo(average * 1.1).Within(1), $"Max (+10%); Theoretical: {theoreticalRoll}");
        }

        [TestCase(CreatureConstants.Angel_AstralDeva, GenderConstants.Male, 7 * 12, 7 * 12 + 6)]
        [TestCase(CreatureConstants.Angel_AstralDeva, GenderConstants.Female, 7 * 12, 7 * 12 + 6)]
        [TestCase(CreatureConstants.Angel_Planetar, GenderConstants.Male, 8 * 12, 9 * 12)]
        [TestCase(CreatureConstants.Angel_Planetar, GenderConstants.Female, 8 * 12, 9 * 12)]
        [TestCase(CreatureConstants.Angel_Solar, GenderConstants.Male, 9 * 12, 10 * 12)]
        [TestCase(CreatureConstants.Angel_Solar, GenderConstants.Female, 9 * 12, 10 * 12)]
        //Heights: https://www.d20srd.org/srd/combat/movementPositionAndDistance.htm
        [TestCase(CreatureConstants.AnimatedObject_Tiny, GenderConstants.Agender, 12, 24)]
        [TestCase(CreatureConstants.AnimatedObject_Small, GenderConstants.Agender, 24, 48)]
        [TestCase(CreatureConstants.AnimatedObject_Medium, GenderConstants.Agender, 48, 96)]
        [TestCase(CreatureConstants.AnimatedObject_Large, GenderConstants.Agender, 8 * 12, 16 * 12)]
        [TestCase(CreatureConstants.AnimatedObject_Huge, GenderConstants.Agender, 16 * 12, 32 * 12)]
        [TestCase(CreatureConstants.AnimatedObject_Gargantuan, GenderConstants.Agender, 32 * 12, 64 * 12)]
        [TestCase(CreatureConstants.AnimatedObject_Colossal, GenderConstants.Agender, 64 * 12, 128 * 12)]
        [TestCase(CreatureConstants.Ape, GenderConstants.Male, 5 * 12 + 6, 6 * 12)]
        [TestCase(CreatureConstants.Ape, GenderConstants.Female, 5 * 12 + 6, 6 * 12)]
        [TestCase(CreatureConstants.Azer, GenderConstants.Agender, 4 * 12 + 5, 4 * 12 + 9)]
        [TestCase(CreatureConstants.Babau, GenderConstants.Agender, 6 * 12, 7 * 12)]
        [TestCase(CreatureConstants.Badger, GenderConstants.Male, 24, 36)]
        [TestCase(CreatureConstants.Badger, GenderConstants.Female, 24, 36)]
        [TestCase(CreatureConstants.Badger_Dire, GenderConstants.Male, 5 * 12, 7 * 12)]
        [TestCase(CreatureConstants.Badger_Dire, GenderConstants.Female, 5 * 12, 7 * 12)]
        [TestCase(CreatureConstants.Bugbear, GenderConstants.Male, 6 * 12, 8 * 12)]
        [TestCase(CreatureConstants.Bugbear, GenderConstants.Female, 6 * 12, 8 * 12)]
        [TestCase(CreatureConstants.Centaur, GenderConstants.Male, 7 * 12, 9 * 12)]
        [TestCase(CreatureConstants.Centaur, GenderConstants.Female, 7 * 12, 9 * 12)]
        [TestCase(CreatureConstants.Ettin, GenderConstants.Male, 13 * 12, 13 * 12 + 10)]
        [TestCase(CreatureConstants.Ettin, GenderConstants.Female, 12 * 12 + 4, 13 * 12 + 2)]
        [TestCase(CreatureConstants.Giant_Cloud, GenderConstants.Male, 24 * 12 + 4, 26 * 12 + 8)]
        [TestCase(CreatureConstants.Giant_Cloud, GenderConstants.Female, 22 * 12 + 8, 25 * 12)]
        [TestCase(CreatureConstants.Giant_Hill, GenderConstants.Male, 16 * 12 + 1, 17 * 12)]
        [TestCase(CreatureConstants.Giant_Hill, GenderConstants.Female, 15 * 12 + 5, 16 * 12 + 4)]
        [TestCase(CreatureConstants.Gnoll, GenderConstants.Male, 7 * 12, 7 * 12 + 6)]
        [TestCase(CreatureConstants.Gnoll, GenderConstants.Female, 7 * 12, 7 * 12 + 6)]
        [TestCase(CreatureConstants.Horse_Heavy, GenderConstants.Male, 64, 72)]
        [TestCase(CreatureConstants.Horse_Heavy, GenderConstants.Female, 64, 72)]
        [TestCase(CreatureConstants.Horse_Light, GenderConstants.Male, 57, 61)]
        [TestCase(CreatureConstants.Horse_Light, GenderConstants.Female, 57, 61)]
        [TestCase(CreatureConstants.Nalfeshnee, GenderConstants.Agender, 10 * 12, 20 * 12)]
        [TestCase(CreatureConstants.Ogre, GenderConstants.Male, 10 * 12 + 1, 10 * 12 + 10)]
        [TestCase(CreatureConstants.Ogre, GenderConstants.Female, 9 * 12 + 3, 10 * 12)]
        [TestCase(CreatureConstants.Pixie, GenderConstants.Male, 12, 30)]
        [TestCase(CreatureConstants.Pixie, GenderConstants.Female, 12, 30)]
        [TestCase(CreatureConstants.Quasit, GenderConstants.Agender, 1 * 12, 2 * 12)]
        [TestCase(CreatureConstants.Salamander_Noble, GenderConstants.Agender, 8 * 12, 16 * 12)]
        [TestCase(CreatureConstants.Salamander_Average, GenderConstants.Agender, 4 * 12, 8 * 12)]
        [TestCase(CreatureConstants.Salamander_Flamebrother, GenderConstants.Agender, 2 * 12, 4 * 12)]
        [TestCase(CreatureConstants.Wolf, GenderConstants.Male, 41, 63)]
        [TestCase(CreatureConstants.Wolf, GenderConstants.Female, 41, 63)]
        [TestCase(CreatureConstants.Whale_Baleen, GenderConstants.Male, 30 * 12, 60 * 12)]
        [TestCase(CreatureConstants.Whale_Baleen, GenderConstants.Female, 30 * 12, 60 * 12)]
        public void RollCalculationsAreAccurate(string creature, string gender, int min, int max)
        {
            var heights = GetCreatureHeights();

            var baseHeight = dice.Roll(heights[creature][gender]).AsSum();
            var multiplierMin = dice.Roll(heights[creature][creature]).AsPotentialMinimum();
            var multiplierAvg = dice.Roll(heights[creature][creature]).AsPotentialAverage();
            var multiplierMax = dice.Roll(heights[creature][creature]).AsPotentialMaximum();
            var theoreticalRoll = RollHelper.GetRollWithFewestDice(min, max);

            Assert.That(baseHeight + multiplierMin, Is.EqualTo(min), $"Min; Theoretical: {theoreticalRoll}");
            Assert.That(baseHeight + multiplierAvg, Is.EqualTo((min + max) / 2).Within(1), $"Average; Theoretical: {theoreticalRoll}");
            Assert.That(baseHeight + multiplierMax, Is.EqualTo(max), $"Max; Theoretical: {theoreticalRoll}");
        }
    }
}