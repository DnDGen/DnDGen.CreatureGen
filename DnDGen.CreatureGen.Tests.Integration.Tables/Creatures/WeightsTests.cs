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
            weights[CreatureConstants.Ankheg][GenderConstants.Female] = GetGenderFromAverage(800);
            weights[CreatureConstants.Ankheg][GenderConstants.Male] = GetGenderFromAverage(800);
            weights[CreatureConstants.Ankheg][CreatureConstants.Ankheg] = GetCreatureFromAverage(800, 10 * 12);
            weights[CreatureConstants.Annis][GenderConstants.Female] = GetGenderFromAverage(325);
            weights[CreatureConstants.Annis][CreatureConstants.Annis] = GetCreatureFromAverage(325, 8 * 12);
            weights[CreatureConstants.Ape][GenderConstants.Female] = "299";
            weights[CreatureConstants.Ape][GenderConstants.Male] = "299";
            weights[CreatureConstants.Ape][CreatureConstants.Ape] = RollHelper.GetRollWithFewestDice(299, 300, 400);
            weights[CreatureConstants.Ape_Dire][GenderConstants.Female] = "796";
            weights[CreatureConstants.Ape_Dire][GenderConstants.Male] = "796";
            weights[CreatureConstants.Ape_Dire][CreatureConstants.Ape_Dire] = RollHelper.GetRollWithFewestDice(796, 800, 1200);
            //INFO: Based on Half-Elf, since could be Human, Half-Elf, or Drow
            weights[CreatureConstants.Aranea][GenderConstants.Female] = "4*12+5";
            weights[CreatureConstants.Aranea][GenderConstants.Male] = "4*12+7";
            weights[CreatureConstants.Aranea][CreatureConstants.Aranea] = "2d8";
            weights[CreatureConstants.Arrowhawk_Juvenile][GenderConstants.Female] = GetGenderFromAverage(20);
            weights[CreatureConstants.Arrowhawk_Juvenile][GenderConstants.Male] = GetGenderFromAverage(20);
            weights[CreatureConstants.Arrowhawk_Juvenile][CreatureConstants.Arrowhawk_Juvenile] = GetCreatureFromAverage(20, 5 * 12);
            weights[CreatureConstants.Arrowhawk_Adult][GenderConstants.Female] = GetGenderFromAverage(100);
            weights[CreatureConstants.Arrowhawk_Adult][GenderConstants.Male] = GetGenderFromAverage(100);
            weights[CreatureConstants.Arrowhawk_Adult][CreatureConstants.Arrowhawk_Adult] = GetCreatureFromAverage(100, 10 * 12);
            weights[CreatureConstants.Arrowhawk_Elder][GenderConstants.Female] = GetGenderFromAverage(800);
            weights[CreatureConstants.Arrowhawk_Elder][GenderConstants.Male] = GetGenderFromAverage(800);
            weights[CreatureConstants.Arrowhawk_Elder][CreatureConstants.Arrowhawk_Elder] = GetCreatureFromAverage(800, 20 * 12);
            weights[CreatureConstants.Athach][GenderConstants.Female] = GetGenderFromAverage(4500);
            weights[CreatureConstants.Athach][GenderConstants.Male] = GetGenderFromAverage(4500);
            weights[CreatureConstants.Athach][CreatureConstants.Athach] = GetCreatureFromAverage(4500, 18 * 12);
            weights[CreatureConstants.Avoral][GenderConstants.Female] = GetGenderFromAverage(120);
            weights[CreatureConstants.Avoral][GenderConstants.Male] = GetGenderFromAverage(120);
            weights[CreatureConstants.Avoral][GenderConstants.Agender] = GetGenderFromAverage(120);
            weights[CreatureConstants.Avoral][CreatureConstants.Avoral] = GetCreatureFromAverage(120, 6 * 12 + 9);
            weights[CreatureConstants.Azer][GenderConstants.Female] = GetGenderFromAverage(200);
            weights[CreatureConstants.Azer][GenderConstants.Male] = GetGenderFromAverage(200);
            weights[CreatureConstants.Azer][GenderConstants.Agender] = GetGenderFromAverage(200);
            weights[CreatureConstants.Azer][CreatureConstants.Azer] = GetCreatureFromAverage(200, 4 * 12 + 7);
            weights[CreatureConstants.Babau][GenderConstants.Agender] = GetGenderFromAverage(140);
            weights[CreatureConstants.Babau][CreatureConstants.Babau] = GetCreatureFromAverage(140, 6 * 12 + 6);
            weights[CreatureConstants.Baboon][GenderConstants.Female] = GetGenderFromAverage(32);
            weights[CreatureConstants.Baboon][GenderConstants.Male] = GetGenderFromAverage(24);
            weights[CreatureConstants.Baboon][CreatureConstants.Baboon] = RollHelper.GetRollWithFewestDice(22, 82);
            weights[CreatureConstants.Badger][GenderConstants.Female] = "25";
            weights[CreatureConstants.Badger][GenderConstants.Male] = "25";
            weights[CreatureConstants.Badger][CreatureConstants.Badger] = RollHelper.GetRollWithFewestDice(25, 35);
            weights[CreatureConstants.Badger_Dire][GenderConstants.Female] = GetGenderFromAverage(500);
            weights[CreatureConstants.Badger_Dire][GenderConstants.Male] = GetGenderFromAverage(500);
            weights[CreatureConstants.Badger_Dire][CreatureConstants.Badger_Dire] = GetCreatureFromAverage(500, 6 * 12);
            weights[CreatureConstants.Balor][GenderConstants.Agender] = GetGenderFromAverage(4500);
            weights[CreatureConstants.Balor][CreatureConstants.Balor] = GetCreatureFromAverage(4500, 12 * 12);
            weights[CreatureConstants.BarbedDevil_Hamatula][GenderConstants.Agender] = GetGenderFromAverage(300);
            weights[CreatureConstants.BarbedDevil_Hamatula][CreatureConstants.BarbedDevil_Hamatula] = GetCreatureFromAverage(300, 7 * 12);
            weights[CreatureConstants.Barghest][GenderConstants.Agender] = GetGenderFromAverage(180);
            weights[CreatureConstants.Barghest][CreatureConstants.Barghest] = GetCreatureFromAverage(180, 6 * 12);
            weights[CreatureConstants.Barghest_Greater][GenderConstants.Agender] = GetGenderFromAverage(180);
            weights[CreatureConstants.Barghest_Greater][CreatureConstants.Barghest_Greater] = GetCreatureFromAverage(180, 6 * 12);
            weights[CreatureConstants.Bear_Brown][GenderConstants.Female] = GetGenderFromAverage(1800);
            weights[CreatureConstants.Bear_Brown][GenderConstants.Male] = GetGenderFromAverage(1800);
            weights[CreatureConstants.Bear_Brown][CreatureConstants.Bear_Brown] = GetCreatureFromAverage(1800, 9 * 12);
            weights[CreatureConstants.BeardedDevil_Barbazu][GenderConstants.Agender] = GetGenderFromAverage(225);
            weights[CreatureConstants.BeardedDevil_Barbazu][CreatureConstants.BeardedDevil_Barbazu] = GetCreatureFromAverage(225, 6 * 12);
            weights[CreatureConstants.Bebilith][GenderConstants.Agender] = GetGenderFromAverage(4500);
            weights[CreatureConstants.Bebilith][CreatureConstants.Bebilith] = GetCreatureFromAverage(4500, 14 * 12);
            weights[CreatureConstants.BoneDevil_Osyluth][GenderConstants.Agender] = GetGenderFromAverage(500);
            weights[CreatureConstants.BoneDevil_Osyluth][CreatureConstants.BoneDevil_Osyluth] = GetCreatureFromAverage(500, 9 * 12);
            weights[CreatureConstants.Bugbear][GenderConstants.Female] = "200";
            weights[CreatureConstants.Bugbear][GenderConstants.Male] = "200";
            weights[CreatureConstants.Bugbear][CreatureConstants.Bugbear] = RollHelper.GetRollWithFewestDice(200, 250, 350);
            weights[CreatureConstants.Cat][GenderConstants.Female] = GetGenderFromAverage(9); //Small Animal
            weights[CreatureConstants.Cat][GenderConstants.Male] = GetGenderFromAverage(11);
            weights[CreatureConstants.Cat][CreatureConstants.Cat] = GetCreatureFromAverage(10, 18);
            weights[CreatureConstants.Centaur][GenderConstants.Female] = GetGenderFromAverage(2100);
            weights[CreatureConstants.Centaur][GenderConstants.Male] = GetGenderFromAverage(2100);
            weights[CreatureConstants.Centaur][CreatureConstants.Centaur] = GetCreatureFromAverage(2100, 8 * 12);
            weights[CreatureConstants.ChainDevil_Kyton][GenderConstants.Agender] = GetGenderFromAverage(300);
            weights[CreatureConstants.ChainDevil_Kyton][CreatureConstants.ChainDevil_Kyton] = GetCreatureFromAverage(300, 6 * 12);
            weights[CreatureConstants.Criosphinx][GenderConstants.Male] = GetGenderFromAverage(800);
            weights[CreatureConstants.Criosphinx][CreatureConstants.Criosphinx] = GetCreatureFromAverage(800, 120);
            weights[CreatureConstants.Dretch][GenderConstants.Agender] = GetGenderFromAverage(60);
            weights[CreatureConstants.Dretch][CreatureConstants.Dretch] = GetCreatureFromAverage(60, 4 * 12);
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
            weights[CreatureConstants.Elemental_Air_Small][GenderConstants.Agender] = GetGenderFromAverage(1);
            weights[CreatureConstants.Elemental_Air_Small][CreatureConstants.Elemental_Air_Small] = GetCreatureFromAverage(1, 4 * 12);
            weights[CreatureConstants.Elemental_Air_Medium][GenderConstants.Agender] = GetGenderFromAverage(2);
            weights[CreatureConstants.Elemental_Air_Medium][CreatureConstants.Elemental_Air_Small] = GetCreatureFromAverage(2, 8 * 12);
            weights[CreatureConstants.Elemental_Air_Large][GenderConstants.Agender] = GetGenderFromAverage(4);
            weights[CreatureConstants.Elemental_Air_Large][CreatureConstants.Elemental_Air_Small] = GetCreatureFromAverage(4, 16 * 12);
            weights[CreatureConstants.Elemental_Air_Huge][GenderConstants.Agender] = GetGenderFromAverage(8);
            weights[CreatureConstants.Elemental_Air_Huge][CreatureConstants.Elemental_Air_Small] = GetCreatureFromAverage(8, 32 * 12);
            weights[CreatureConstants.Elemental_Air_Greater][GenderConstants.Agender] = GetGenderFromAverage(10);
            weights[CreatureConstants.Elemental_Air_Greater][CreatureConstants.Elemental_Air_Small] = GetCreatureFromAverage(10, 36 * 12);
            weights[CreatureConstants.Elemental_Air_Elder][GenderConstants.Agender] = GetGenderFromAverage(12);
            weights[CreatureConstants.Elemental_Air_Elder][CreatureConstants.Elemental_Air_Small] = GetCreatureFromAverage(12, 40 * 12);
            weights[CreatureConstants.Elemental_Earth_Small][GenderConstants.Agender] = GetGenderFromAverage(80);
            weights[CreatureConstants.Elemental_Earth_Small][CreatureConstants.Elemental_Air_Small] = GetCreatureFromAverage(80, 4 * 12);
            weights[CreatureConstants.Elemental_Earth_Medium][GenderConstants.Agender] = GetGenderFromAverage(750);
            weights[CreatureConstants.Elemental_Earth_Medium][CreatureConstants.Elemental_Air_Small] = GetCreatureFromAverage(750, 8 * 12);
            weights[CreatureConstants.Elemental_Earth_Large][GenderConstants.Agender] = GetGenderFromAverage(6000);
            weights[CreatureConstants.Elemental_Earth_Large][CreatureConstants.Elemental_Air_Small] = GetCreatureFromAverage(6000, 16 * 12);
            weights[CreatureConstants.Elemental_Earth_Huge][GenderConstants.Agender] = GetGenderFromAverage(48_000);
            weights[CreatureConstants.Elemental_Earth_Huge][CreatureConstants.Elemental_Air_Small] = GetCreatureFromAverage(48_000, 32 * 12);
            weights[CreatureConstants.Elemental_Earth_Greater][GenderConstants.Agender] = GetGenderFromAverage(54_000);
            weights[CreatureConstants.Elemental_Earth_Greater][CreatureConstants.Elemental_Air_Small] = GetCreatureFromAverage(54_000, 36 * 12);
            weights[CreatureConstants.Elemental_Earth_Elder][GenderConstants.Agender] = GetGenderFromAverage(60_000);
            weights[CreatureConstants.Elemental_Earth_Elder][CreatureConstants.Elemental_Air_Small] = GetCreatureFromAverage(60_000, 40 * 12);
            weights[CreatureConstants.Elemental_Fire_Small][GenderConstants.Agender] = GetGenderFromAverage(1);
            weights[CreatureConstants.Elemental_Fire_Small][CreatureConstants.Elemental_Air_Small] = GetCreatureFromAverage(1, 4 * 12);
            weights[CreatureConstants.Elemental_Fire_Medium][GenderConstants.Agender] = GetGenderFromAverage(2);
            weights[CreatureConstants.Elemental_Fire_Medium][CreatureConstants.Elemental_Air_Small] = GetCreatureFromAverage(2, 8 * 12);
            weights[CreatureConstants.Elemental_Fire_Large][GenderConstants.Agender] = GetGenderFromAverage(4);
            weights[CreatureConstants.Elemental_Fire_Large][CreatureConstants.Elemental_Air_Small] = GetCreatureFromAverage(4, 16 * 12);
            weights[CreatureConstants.Elemental_Fire_Huge][GenderConstants.Agender] = GetGenderFromAverage(8);
            weights[CreatureConstants.Elemental_Fire_Huge][CreatureConstants.Elemental_Air_Small] = GetCreatureFromAverage(8, 32 * 12);
            weights[CreatureConstants.Elemental_Fire_Greater][GenderConstants.Agender] = GetGenderFromAverage(10);
            weights[CreatureConstants.Elemental_Fire_Greater][CreatureConstants.Elemental_Air_Small] = GetCreatureFromAverage(10, 36 * 12);
            weights[CreatureConstants.Elemental_Fire_Elder][GenderConstants.Agender] = GetGenderFromAverage(12);
            weights[CreatureConstants.Elemental_Fire_Elder][CreatureConstants.Elemental_Air_Small] = GetCreatureFromAverage(12, 40 * 12);
            weights[CreatureConstants.Elemental_Water_Small][GenderConstants.Agender] = GetGenderFromAverage(34);
            weights[CreatureConstants.Elemental_Water_Small][CreatureConstants.Elemental_Air_Small] = GetCreatureFromAverage(34, 4 * 12);
            weights[CreatureConstants.Elemental_Water_Medium][GenderConstants.Agender] = GetGenderFromAverage(280);
            weights[CreatureConstants.Elemental_Water_Medium][CreatureConstants.Elemental_Air_Small] = GetCreatureFromAverage(280, 8 * 12);
            weights[CreatureConstants.Elemental_Water_Large][GenderConstants.Agender] = GetGenderFromAverage(2250);
            weights[CreatureConstants.Elemental_Water_Large][CreatureConstants.Elemental_Air_Small] = GetCreatureFromAverage(2250, 16 * 12);
            weights[CreatureConstants.Elemental_Water_Huge][GenderConstants.Agender] = GetGenderFromAverage(18_000);
            weights[CreatureConstants.Elemental_Water_Huge][CreatureConstants.Elemental_Air_Small] = GetCreatureFromAverage(18_000, 32 * 12);
            weights[CreatureConstants.Elemental_Water_Greater][GenderConstants.Agender] = GetGenderFromAverage(21_000);
            weights[CreatureConstants.Elemental_Water_Greater][CreatureConstants.Elemental_Air_Small] = GetCreatureFromAverage(21_000, 36 * 12);
            weights[CreatureConstants.Elemental_Water_Elder][GenderConstants.Agender] = GetGenderFromAverage(24_000);
            weights[CreatureConstants.Elemental_Water_Elder][CreatureConstants.Elemental_Air_Small] = GetCreatureFromAverage(24_000, 40 * 12);
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
            weights[CreatureConstants.Erinyes][GenderConstants.Agender] = GetGenderFromAverage(150);
            weights[CreatureConstants.Erinyes][CreatureConstants.Erinyes] = GetCreatureFromAverage(150, 6 * 12);
            weights[CreatureConstants.Ettin][GenderConstants.Female] = "912";
            weights[CreatureConstants.Ettin][GenderConstants.Male] = "912";
            weights[CreatureConstants.Ettin][CreatureConstants.Ettin] = "18d20";
            weights[CreatureConstants.Giant_Cloud][GenderConstants.Female] = "290"; //Huge
            weights[CreatureConstants.Giant_Cloud][GenderConstants.Male] = "270";
            weights[CreatureConstants.Giant_Cloud][CreatureConstants.Giant_Cloud] = "3d10";
            weights[CreatureConstants.Giant_Hill][GenderConstants.Female] = "290"; //Huge
            weights[CreatureConstants.Giant_Hill][GenderConstants.Male] = "270";
            weights[CreatureConstants.Giant_Hill][CreatureConstants.Giant_Cloud] = "3d10";
            weights[CreatureConstants.Glabrezu][GenderConstants.Agender] = GetGenderFromAverage(5500);
            weights[CreatureConstants.Glabrezu][CreatureConstants.Glabrezu] = GetCreatureFromAverage(5500, 12 * 12);
            weights[CreatureConstants.Gnoll][GenderConstants.Female] = "250";
            weights[CreatureConstants.Gnoll][GenderConstants.Male] = "250";
            weights[CreatureConstants.Gnoll][CreatureConstants.Bugbear] = RollHelper.GetRollWithFewestDice(250, 280, 320);
            weights[CreatureConstants.Gnome_Forest][GenderConstants.Female] = "35";
            weights[CreatureConstants.Gnome_Forest][GenderConstants.Male] = "40";
            weights[CreatureConstants.Gnome_Forest][CreatureConstants.Gnome_Forest] = "1"; //x1
            weights[CreatureConstants.Gnome_Rock][GenderConstants.Female] = "35";
            weights[CreatureConstants.Gnome_Rock][GenderConstants.Male] = "40";
            weights[CreatureConstants.Gnome_Rock][CreatureConstants.Gnome_Rock] = "1"; //x1
            weights[CreatureConstants.Gnome_Svirfneblin][GenderConstants.Female] = "1";
            weights[CreatureConstants.Gnome_Svirfneblin][GenderConstants.Male] = "1";
            weights[CreatureConstants.Gnome_Svirfneblin][CreatureConstants.Gnome_Svirfneblin] = "1"; //x1
            weights[CreatureConstants.Goblin][GenderConstants.Female] = "25";
            weights[CreatureConstants.Goblin][GenderConstants.Male] = "30";
            weights[CreatureConstants.Goblin][CreatureConstants.Goblin] = "1"; //x1
            weights[CreatureConstants.GreenHag][GenderConstants.Female] = "85";
            weights[CreatureConstants.GreenHag][CreatureConstants.GreenHag] = "2d4";
            weights[CreatureConstants.Grig][GenderConstants.Female] = GetGenderFromAverage(1); //Tiny
            weights[CreatureConstants.Grig][GenderConstants.Male] = GetGenderFromAverage(1);
            weights[CreatureConstants.Grig][CreatureConstants.Grig] = GetCreatureFromAverage(1, 18);
            weights[CreatureConstants.Grig_WithFiddle][GenderConstants.Female] = GetGenderFromAverage(1); //Tiny
            weights[CreatureConstants.Grig_WithFiddle][GenderConstants.Male] = GetGenderFromAverage(1);
            weights[CreatureConstants.Grig_WithFiddle][CreatureConstants.Grig_WithFiddle] = GetCreatureFromAverage(1, 18);
            weights[CreatureConstants.Halfling_Deep][GenderConstants.Female] = "25";
            weights[CreatureConstants.Halfling_Deep][GenderConstants.Male] = "30";
            weights[CreatureConstants.Halfling_Deep][CreatureConstants.Halfling_Deep] = "1"; //x1
            weights[CreatureConstants.Halfling_Lightfoot][GenderConstants.Female] = "25";
            weights[CreatureConstants.Halfling_Lightfoot][GenderConstants.Male] = "30";
            weights[CreatureConstants.Halfling_Lightfoot][CreatureConstants.Halfling_Lightfoot] = "1"; //x1
            weights[CreatureConstants.Halfling_Tallfellow][GenderConstants.Female] = "25";
            weights[CreatureConstants.Halfling_Tallfellow][GenderConstants.Male] = "30";
            weights[CreatureConstants.Halfling_Tallfellow][CreatureConstants.Halfling_Tallfellow] = "1"; //x1
            weights[CreatureConstants.Hellcat_Bezekira][GenderConstants.Agender] = GetGenderFromAverage(900);
            weights[CreatureConstants.Hellcat_Bezekira][CreatureConstants.Hellcat_Bezekira] = GetCreatureFromAverage(900, 9 * 12);
            weights[CreatureConstants.Hezrou][GenderConstants.Agender] = GetGenderFromAverage(750);
            weights[CreatureConstants.Hezrou][CreatureConstants.Hezrou] = GetCreatureFromAverage(750, 8 * 12);
            weights[CreatureConstants.Hieracosphinx][GenderConstants.Male] = GetGenderFromAverage(800);
            weights[CreatureConstants.Hieracosphinx][CreatureConstants.Hieracosphinx] = GetCreatureFromAverage(800, 10 * 12);
            weights[CreatureConstants.Hobgoblin][GenderConstants.Female] = "145";
            weights[CreatureConstants.Hobgoblin][GenderConstants.Male] = "165";
            weights[CreatureConstants.Hobgoblin][CreatureConstants.Hobgoblin] = "2d4"; //x5
            weights[CreatureConstants.HornedDevil_Cornugon][GenderConstants.Agender] = GetGenderFromAverage(600);
            weights[CreatureConstants.HornedDevil_Cornugon][CreatureConstants.HornedDevil_Cornugon] = GetCreatureFromAverage(600, 9 * 12);
            weights[CreatureConstants.Horse_Heavy][GenderConstants.Female] = "1695";
            weights[CreatureConstants.Horse_Heavy][GenderConstants.Male] = "1695";
            weights[CreatureConstants.Horse_Heavy][CreatureConstants.Horse_Heavy] = RollHelper.GetRollWithFewestDice(1695, 1700, 2200);
            weights[CreatureConstants.Horse_Light][GenderConstants.Female] = "798";
            weights[CreatureConstants.Horse_Light][GenderConstants.Male] = "798";
            weights[CreatureConstants.Horse_Light][CreatureConstants.Horse_Light] = RollHelper.GetRollWithFewestDice(798, 800, 1000);
            weights[CreatureConstants.Horse_Heavy_War][GenderConstants.Female] = "1695";
            weights[CreatureConstants.Horse_Heavy_War][GenderConstants.Male] = "1695";
            weights[CreatureConstants.Horse_Heavy_War][CreatureConstants.Horse_Heavy] = RollHelper.GetRollWithFewestDice(1695, 1700, 2200);
            weights[CreatureConstants.Horse_Light_War][GenderConstants.Female] = "798";
            weights[CreatureConstants.Horse_Light_War][GenderConstants.Male] = "798";
            weights[CreatureConstants.Horse_Light_War][CreatureConstants.Horse_Light] = RollHelper.GetRollWithFewestDice(798, 800, 1000);
            weights[CreatureConstants.Human][GenderConstants.Female] = "85";
            weights[CreatureConstants.Human][GenderConstants.Male] = "120";
            weights[CreatureConstants.Human][CreatureConstants.Human] = "2d4"; //x5
            weights[CreatureConstants.IceDevil_Gelugon][GenderConstants.Agender] = GetGenderFromAverage(700);
            weights[CreatureConstants.IceDevil_Gelugon][CreatureConstants.IceDevil_Gelugon] = GetCreatureFromAverage(700, 12 * 12);
            weights[CreatureConstants.Imp][GenderConstants.Agender] = GetGenderFromAverage(8);
            weights[CreatureConstants.Imp][CreatureConstants.Imp] = GetCreatureFromAverage(8, 2 * 12);
            weights[CreatureConstants.Kobold][GenderConstants.Female] = "20";
            weights[CreatureConstants.Kobold][GenderConstants.Male] = "25";
            weights[CreatureConstants.Kobold][CreatureConstants.Kobold] = "1"; //x1
            weights[CreatureConstants.Lemure][GenderConstants.Agender] = GetGenderFromAverage(100);
            weights[CreatureConstants.Lemure][CreatureConstants.Lemure] = GetCreatureFromAverage(100, 5 * 12);
            weights[CreatureConstants.Leonal][GenderConstants.Female] = "130";
            weights[CreatureConstants.Leonal][GenderConstants.Male] = "130";
            weights[CreatureConstants.Leonal][CreatureConstants.Leonal] = "2d4";
            weights[CreatureConstants.Lizardfolk][GenderConstants.Female] = "150";
            weights[CreatureConstants.Lizardfolk][GenderConstants.Male] = "150";
            weights[CreatureConstants.Lizardfolk][CreatureConstants.Lizardfolk] = RollHelper.GetRollWithFewestDice(150, 200, 250);
            weights[CreatureConstants.Locathah][GenderConstants.Female] = GetGenderFromAverage(175);
            weights[CreatureConstants.Locathah][GenderConstants.Male] = GetGenderFromAverage(175);
            weights[CreatureConstants.Locathah][CreatureConstants.Locathah] = GetCreatureFromAverage(175, 5 * 12);
            weights[CreatureConstants.Marilith][GenderConstants.Female] = GetGenderFromAverage(2 * 2000);
            weights[CreatureConstants.Marilith][CreatureConstants.Marilith] = GetCreatureFromAverage(2 * 2000, 20 * 12);
            weights[CreatureConstants.Mephit_Air][GenderConstants.Agender] = GetGenderFromAverage(1);
            weights[CreatureConstants.Mephit_Air][GenderConstants.Female] = GetGenderFromAverage(1);
            weights[CreatureConstants.Mephit_Air][GenderConstants.Male] = GetGenderFromAverage(1);
            weights[CreatureConstants.Mephit_Air][CreatureConstants.Mephit_Air] = GetCreatureFromAverage(1, 4 * 12);
            weights[CreatureConstants.Mephit_Dust][GenderConstants.Agender] = GetGenderFromAverage(2);
            weights[CreatureConstants.Mephit_Dust][GenderConstants.Female] = GetGenderFromAverage(2);
            weights[CreatureConstants.Mephit_Dust][GenderConstants.Male] = GetGenderFromAverage(2);
            weights[CreatureConstants.Mephit_Dust][CreatureConstants.Mephit_Dust] = GetCreatureFromAverage(2, 4 * 12);
            weights[CreatureConstants.Mephit_Earth][GenderConstants.Agender] = GetGenderFromAverage(80);
            weights[CreatureConstants.Mephit_Earth][GenderConstants.Female] = GetGenderFromAverage(80);
            weights[CreatureConstants.Mephit_Earth][GenderConstants.Male] = GetGenderFromAverage(80);
            weights[CreatureConstants.Mephit_Earth][CreatureConstants.Mephit_Earth] = GetCreatureFromAverage(80, 4 * 12);
            weights[CreatureConstants.Mephit_Fire][GenderConstants.Agender] = GetGenderFromAverage(1);
            weights[CreatureConstants.Mephit_Fire][GenderConstants.Female] = GetGenderFromAverage(1);
            weights[CreatureConstants.Mephit_Fire][GenderConstants.Male] = GetGenderFromAverage(1);
            weights[CreatureConstants.Mephit_Fire][CreatureConstants.Mephit_Fire] = GetCreatureFromAverage(1, 4 * 12);
            weights[CreatureConstants.Mephit_Ice][GenderConstants.Agender] = GetGenderFromAverage(30);
            weights[CreatureConstants.Mephit_Ice][GenderConstants.Female] = GetGenderFromAverage(30);
            weights[CreatureConstants.Mephit_Ice][GenderConstants.Male] = GetGenderFromAverage(30);
            weights[CreatureConstants.Mephit_Ice][CreatureConstants.Mephit_Ice] = GetCreatureFromAverage(30, 4 * 12);
            weights[CreatureConstants.Mephit_Magma][GenderConstants.Agender] = GetGenderFromAverage(60);
            weights[CreatureConstants.Mephit_Magma][GenderConstants.Female] = GetGenderFromAverage(60);
            weights[CreatureConstants.Mephit_Magma][GenderConstants.Male] = GetGenderFromAverage(60);
            weights[CreatureConstants.Mephit_Magma][CreatureConstants.Mephit_Magma] = GetCreatureFromAverage(60, 4 * 12);
            weights[CreatureConstants.Mephit_Ooze][GenderConstants.Agender] = GetGenderFromAverage(30);
            weights[CreatureConstants.Mephit_Ooze][GenderConstants.Female] = GetGenderFromAverage(30);
            weights[CreatureConstants.Mephit_Ooze][GenderConstants.Male] = GetGenderFromAverage(30);
            weights[CreatureConstants.Mephit_Ooze][CreatureConstants.Mephit_Ooze] = GetCreatureFromAverage(30, 4 * 12);
            weights[CreatureConstants.Mephit_Salt][GenderConstants.Agender] = GetGenderFromAverage(80);
            weights[CreatureConstants.Mephit_Salt][GenderConstants.Female] = GetGenderFromAverage(80);
            weights[CreatureConstants.Mephit_Salt][GenderConstants.Male] = GetGenderFromAverage(80);
            weights[CreatureConstants.Mephit_Salt][CreatureConstants.Mephit_Salt] = GetCreatureFromAverage(80, 4 * 12);
            weights[CreatureConstants.Mephit_Steam][GenderConstants.Agender] = GetGenderFromAverage(2);
            weights[CreatureConstants.Mephit_Steam][GenderConstants.Female] = GetGenderFromAverage(2);
            weights[CreatureConstants.Mephit_Steam][GenderConstants.Male] = GetGenderFromAverage(2);
            weights[CreatureConstants.Mephit_Steam][CreatureConstants.Mephit_Steam] = GetCreatureFromAverage(2, 4 * 12);
            weights[CreatureConstants.Mephit_Water][GenderConstants.Agender] = GetGenderFromAverage(30);
            weights[CreatureConstants.Mephit_Water][GenderConstants.Female] = GetGenderFromAverage(30);
            weights[CreatureConstants.Mephit_Water][GenderConstants.Male] = GetGenderFromAverage(30);
            weights[CreatureConstants.Mephit_Water][CreatureConstants.Mephit_Water] = GetCreatureFromAverage(30, 4 * 12);
            weights[CreatureConstants.Merfolk][GenderConstants.Female] = "135";
            weights[CreatureConstants.Merfolk][GenderConstants.Male] = "145";
            weights[CreatureConstants.Merfolk][CreatureConstants.Merfolk] = "2d4"; //x5
            weights[CreatureConstants.Minotaur][GenderConstants.Female] = GetGenderFromAverage(700);
            weights[CreatureConstants.Minotaur][GenderConstants.Male] = GetGenderFromAverage(700);
            weights[CreatureConstants.Minotaur][CreatureConstants.Locathah] = GetCreatureFromAverage(700, 8 * 12);
            weights[CreatureConstants.Nalfeshnee][GenderConstants.Agender] = GetGenderFromAverage(8000);
            weights[CreatureConstants.Nalfeshnee][CreatureConstants.Nalfeshnee] = GetCreatureFromAverage(8000, 15 * 12);
            weights[CreatureConstants.Ogre][GenderConstants.Female] = "554";
            weights[CreatureConstants.Ogre][GenderConstants.Male] = "599";
            weights[CreatureConstants.Ogre][CreatureConstants.Ogre] = "1d10";
            weights[CreatureConstants.Ogre_Merrow][GenderConstants.Female] = "554";
            weights[CreatureConstants.Ogre_Merrow][GenderConstants.Male] = "599";
            weights[CreatureConstants.Ogre_Merrow][CreatureConstants.Ogre_Merrow] = "1d10";
            weights[CreatureConstants.OgreMage][GenderConstants.Female] = GetGenderFromAverage(700);
            weights[CreatureConstants.OgreMage][GenderConstants.Male] = GetGenderFromAverage(700);
            weights[CreatureConstants.OgreMage][CreatureConstants.OgreMage] = GetCreatureFromAverage(700, 10 * 12);
            weights[CreatureConstants.Orc][GenderConstants.Female] = "120";
            weights[CreatureConstants.Orc][GenderConstants.Male] = "160";
            weights[CreatureConstants.Orc][CreatureConstants.Orc_Half] = "2d6"; //x7
            weights[CreatureConstants.Orc_Half][GenderConstants.Female] = "110";
            weights[CreatureConstants.Orc_Half][GenderConstants.Male] = "150";
            weights[CreatureConstants.Orc_Half][CreatureConstants.Orc_Half] = "2d6"; //x7
            weights[CreatureConstants.PitFiend][GenderConstants.Agender] = GetGenderFromAverage(800);
            weights[CreatureConstants.PitFiend][CreatureConstants.PitFiend] = GetCreatureFromAverage(800, 12 * 12);
            weights[CreatureConstants.Pixie][GenderConstants.Female] = GetGenderFromAverage(30);
            weights[CreatureConstants.Pixie][GenderConstants.Male] = GetGenderFromAverage(30);
            weights[CreatureConstants.Pixie][CreatureConstants.Pixie] = GetCreatureFromAverage(30, 21);
            weights[CreatureConstants.Pixie_WithIrresistibleDance][GenderConstants.Female] = GetGenderFromAverage(30);
            weights[CreatureConstants.Pixie_WithIrresistibleDance][GenderConstants.Male] = GetGenderFromAverage(30);
            weights[CreatureConstants.Pixie_WithIrresistibleDance][CreatureConstants.Pixie_WithIrresistibleDance] = GetCreatureFromAverage(30, 21);
            weights[CreatureConstants.Quasit][GenderConstants.Agender] = GetGenderFromAverage(8);
            weights[CreatureConstants.Quasit][CreatureConstants.Quasit] = GetCreatureFromAverage(8, 18);
            weights[CreatureConstants.Retriever][GenderConstants.Agender] = GetGenderFromAverage(6500);
            weights[CreatureConstants.Retriever][CreatureConstants.Retriever] = GetCreatureFromAverage(6500, 14 * 12);
            weights[CreatureConstants.Salamander_Flamebrother][GenderConstants.Agender] = "5";
            weights[CreatureConstants.Salamander_Flamebrother][CreatureConstants.Salamander_Flamebrother] = RollHelper.GetRollWithMostEvenDistribution(5, 8, 60);
            weights[CreatureConstants.Salamander_Average][GenderConstants.Agender] = "50";
            weights[CreatureConstants.Salamander_Average][CreatureConstants.Salamander_Average] = RollHelper.GetRollWithMostEvenDistribution(50, 60, 500);
            weights[CreatureConstants.Salamander_Noble][GenderConstants.Agender] = "400";
            weights[CreatureConstants.Salamander_Noble][CreatureConstants.Salamander_Noble] = RollHelper.GetRollWithMostEvenDistribution(400, 500, 4000);
            weights[CreatureConstants.SeaHag][GenderConstants.Female] = "85";
            weights[CreatureConstants.SeaHag][CreatureConstants.SeaHag] = "2d4";
            weights[CreatureConstants.Succubus][GenderConstants.Female] = GetGenderFromAverage(125);
            weights[CreatureConstants.Succubus][GenderConstants.Male] = GetGenderFromAverage(125);
            weights[CreatureConstants.Succubus][CreatureConstants.Succubus] = GetCreatureFromAverage(125, 6 * 12);
            weights[CreatureConstants.Tiefling][GenderConstants.Female] = "85";
            weights[CreatureConstants.Tiefling][GenderConstants.Male] = "120";
            weights[CreatureConstants.Tiefling][CreatureConstants.Tiefling] = "2d4"; //x5
            weights[CreatureConstants.Tojanida_Juvenile][GenderConstants.Female] = GetGenderFromAverage(60);
            weights[CreatureConstants.Tojanida_Juvenile][GenderConstants.Male] = GetGenderFromAverage(60);
            weights[CreatureConstants.Tojanida_Juvenile][CreatureConstants.Tojanida_Juvenile] = GetCreatureFromAverage(60, 3 * 12);
            weights[CreatureConstants.Tojanida_Adult][GenderConstants.Female] = GetGenderFromAverage(220);
            weights[CreatureConstants.Tojanida_Adult][GenderConstants.Male] = GetGenderFromAverage(220);
            weights[CreatureConstants.Tojanida_Adult][CreatureConstants.Tojanida_Adult] = GetCreatureFromAverage(220, 6 * 12);
            weights[CreatureConstants.Tojanida_Elder][GenderConstants.Female] = GetGenderFromAverage(500);
            weights[CreatureConstants.Tojanida_Elder][GenderConstants.Male] = GetGenderFromAverage(500);
            weights[CreatureConstants.Tojanida_Elder][CreatureConstants.Tojanida_Elder] = GetCreatureFromAverage(500, 9 * 12);
            weights[CreatureConstants.Vrock][GenderConstants.Agender] = GetGenderFromAverage(500);
            weights[CreatureConstants.Vrock][CreatureConstants.Vrock] = GetCreatureFromAverage(500, 8 * 12);
            weights[CreatureConstants.Whale_Baleen][GenderConstants.Female] = GetGenderFromAverage(44 * 2000);
            weights[CreatureConstants.Whale_Baleen][GenderConstants.Male] = GetGenderFromAverage(44 * 2000);
            weights[CreatureConstants.Whale_Baleen][CreatureConstants.Whale_Baleen] = GetCreatureFromAverage(44 * 2000, 45 * 12);
            weights[CreatureConstants.Whale_Cachalot][GenderConstants.Female] = GetGenderFromAverage(15 * 2000);
            weights[CreatureConstants.Whale_Cachalot][GenderConstants.Male] = GetGenderFromAverage(45 * 2000);
            weights[CreatureConstants.Whale_Cachalot][CreatureConstants.Whale_Cachalot] = GetCreatureFromAverage(30 * 2000, 60 * 12);
            weights[CreatureConstants.Whale_Orca][GenderConstants.Female] = GetGenderFromAverage(4 * 2000);
            weights[CreatureConstants.Whale_Orca][GenderConstants.Male] = GetGenderFromAverage(6 * 2000);
            weights[CreatureConstants.Whale_Orca][CreatureConstants.Whale_Orca] = GetCreatureFromAverage(5 * 2000, 30 * 12);
            weights[CreatureConstants.Wolf][GenderConstants.Female] = "39"; //Medium Animal
            weights[CreatureConstants.Wolf][GenderConstants.Male] = "39";
            weights[CreatureConstants.Wolf][CreatureConstants.Wolf] = "2d12";
            weights[CreatureConstants.Xorn_Minor][GenderConstants.Agender] = GetGenderFromAverage(120);
            weights[CreatureConstants.Xorn_Minor][CreatureConstants.Xorn_Minor] = GetCreatureFromAverage(120, 3 * 12);
            weights[CreatureConstants.Xorn_Average][GenderConstants.Agender] = GetGenderFromAverage(600);
            weights[CreatureConstants.Xorn_Average][CreatureConstants.Xorn_Average] = GetCreatureFromAverage(600, 5 * 12);
            weights[CreatureConstants.Xorn_Elder][GenderConstants.Agender] = GetGenderFromAverage(9000);
            weights[CreatureConstants.Xorn_Elder][CreatureConstants.Xorn_Elder] = GetCreatureFromAverage(9000, 8 * 12);

            return weights;
        }

        public static IEnumerable CreatureWeightsData => GetCreatureWeights().Select(t => new TestCaseData(t.Key, t.Value));

        private static string GetGenderFromAverage(double average)
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
        [TestCase(CreatureConstants.Avoral, GenderConstants.Agender, 120)]
        [TestCase(CreatureConstants.Babau, GenderConstants.Agender, 140)]
        [TestCase(CreatureConstants.Badger_Dire, GenderConstants.Male, 500)]
        [TestCase(CreatureConstants.Badger_Dire, GenderConstants.Female, 500)]
        [TestCase(CreatureConstants.Barghest, GenderConstants.Agender, 180)]
        [TestCase(CreatureConstants.Bear_Brown, GenderConstants.Male, 1800)]
        [TestCase(CreatureConstants.Bear_Brown, GenderConstants.Female, 1800)]
        [TestCase(CreatureConstants.Centaur, GenderConstants.Male, 2100)]
        [TestCase(CreatureConstants.Centaur, GenderConstants.Female, 2100)]
        [TestCase(CreatureConstants.Giant_Cloud, GenderConstants.Male, 5000)]
        [TestCase(CreatureConstants.Giant_Cloud, GenderConstants.Female, 5000)]
        [TestCase(CreatureConstants.Giant_Hill, GenderConstants.Male, 1100)]
        [TestCase(CreatureConstants.Giant_Hill, GenderConstants.Female, 1100)]
        [TestCase(CreatureConstants.Grig, GenderConstants.Male, 1)]
        [TestCase(CreatureConstants.Grig, GenderConstants.Female, 1)]
        [TestCase(CreatureConstants.Hieracosphinx, GenderConstants.Male, 800)]
        [TestCase(CreatureConstants.Locathah, GenderConstants.Male, 175)]
        [TestCase(CreatureConstants.Locathah, GenderConstants.Female, 175)]
        [TestCase(CreatureConstants.Minotaur, GenderConstants.Male, 700)]
        [TestCase(CreatureConstants.Minotaur, GenderConstants.Female, 700)]
        [TestCase(CreatureConstants.Whale_Baleen, GenderConstants.Male, 44 * 2000)]
        [TestCase(CreatureConstants.Whale_Baleen, GenderConstants.Female, 44 * 2000)]
        [TestCase(CreatureConstants.Whale_Cachalot, GenderConstants.Male, 45 * 2000)]
        [TestCase(CreatureConstants.Whale_Cachalot, GenderConstants.Female, 15 * 2000)]
        [TestCase(CreatureConstants.Whale_Orca, GenderConstants.Male, 6 * 2000)]
        [TestCase(CreatureConstants.Whale_Orca, GenderConstants.Female, 4 * 2000)]
        [TestCase(CreatureConstants.Xorn_Minor, GenderConstants.Agender, 120)]
        [TestCase(CreatureConstants.Xorn_Average, GenderConstants.Agender, 600)]
        [TestCase(CreatureConstants.Xorn_Elder, GenderConstants.Agender, 9000)]
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

            var theoreticalRoll = RollHelper.GetRollWithFewestDice(average * 9 / 10, average * 11 / 10);

            Assert.That(baseWeight, Is.Positive.And.EqualTo(average * 0.8).Within(sigma), $"Base Weight (80%); Theoretical: {theoreticalRoll}");
            Assert.That(heightMultiplierMin * weightMultiplierMin, Is.Positive.And.EqualTo(average * 0.1).Within(sigma), $"Min (10%); Theoretical: {theoreticalRoll}; H:{heightMultiplierMin}, W:{weightMultiplierMin}");
            Assert.That(heightMultiplierAvg * weightMultiplierAvg, Is.Positive.And.EqualTo(average * 0.2).Within(sigma), $"Average (20%); Theoretical: {theoreticalRoll}; H:{heightMultiplierAvg}, W:{weightMultiplierAvg}");
            Assert.That(heightMultiplierMax * weightMultiplierMax, Is.Positive.And.EqualTo(average * 0.3).Within(sigma), $"Max (30%); Theoretical: {theoreticalRoll}; H:{heightMultiplierMax}, W:{weightMultiplierMax}");
            Assert.That(baseWeight + heightMultiplierMin * weightMultiplierMin, Is.Positive.And.EqualTo(average * 0.9).Within(sigma), $"Min (90%); Theoretical: {theoreticalRoll}");
            Assert.That(baseWeight + heightMultiplierAvg * weightMultiplierAvg, Is.Positive.And.EqualTo(average).Within(sigma), $"Average; Theoretical: {theoreticalRoll}");
            Assert.That(baseWeight + heightMultiplierMax * weightMultiplierMax, Is.Positive.And.EqualTo(average * 1.1).Within(sigma), $"Max (110%); Theoretical: {theoreticalRoll}");
        }

        //Weights: https://www.d20srd.org/srd/combat/movementPositionAndDistance.htm
        [TestCase(CreatureConstants.AnimatedObject_Tiny, GenderConstants.Agender, 1, 8)]
        [TestCase(CreatureConstants.AnimatedObject_Small, GenderConstants.Agender, 8, 60)]
        [TestCase(CreatureConstants.AnimatedObject_Medium, GenderConstants.Agender, 60, 500)]
        [TestCase(CreatureConstants.AnimatedObject_Large, GenderConstants.Agender, 500, 2 * 2000)]
        [TestCase(CreatureConstants.AnimatedObject_Huge, GenderConstants.Agender, 2 * 2000, 16 * 2000)]
        [TestCase(CreatureConstants.AnimatedObject_Gargantuan, GenderConstants.Agender, 16 * 2000, 125 * 2000)]
        [TestCase(CreatureConstants.AnimatedObject_Colossal, GenderConstants.Agender, 125 * 2000, 1000 * 2000)]
        [TestCase(CreatureConstants.Ape, GenderConstants.Male, 300, 400)]
        [TestCase(CreatureConstants.Ape, GenderConstants.Female, 300, 400)]
        [TestCase(CreatureConstants.Ape_Dire, GenderConstants.Male, 800, 1200)]
        [TestCase(CreatureConstants.Ape_Dire, GenderConstants.Female, 800, 1200)]
        [TestCase(CreatureConstants.Azer, GenderConstants.Agender, 180, 220)]
        [TestCase(CreatureConstants.Baboon, GenderConstants.Male, 22, 82)]
        [TestCase(CreatureConstants.Baboon, GenderConstants.Female, 22, 42)]
        [TestCase(CreatureConstants.Badger, GenderConstants.Male, 25, 35)]
        [TestCase(CreatureConstants.Badger, GenderConstants.Female, 25, 35)]
        [TestCase(CreatureConstants.Balor, GenderConstants.Agender, 4000, 4900)]
        [TestCase(CreatureConstants.Bugbear, GenderConstants.Male, 250, 350)]
        [TestCase(CreatureConstants.Bugbear, GenderConstants.Female, 250, 350)]
        [TestCase(CreatureConstants.Ettin, GenderConstants.Male, 930, 5200)]
        [TestCase(CreatureConstants.Ettin, GenderConstants.Female, 930, 5200)]
        [TestCase(CreatureConstants.Gnoll, GenderConstants.Male, 280, 320)]
        [TestCase(CreatureConstants.Gnoll, GenderConstants.Female, 280, 320)]
        [TestCase(CreatureConstants.Lizardfolk, GenderConstants.Male, 200, 250)]
        [TestCase(CreatureConstants.Lizardfolk, GenderConstants.Female, 200, 250)]
        [TestCase(CreatureConstants.Ogre, GenderConstants.Male, 600, 690)]
        [TestCase(CreatureConstants.Ogre, GenderConstants.Female, 555, 645)]
        [TestCase(CreatureConstants.Salamander_Noble, GenderConstants.Agender, 500, 4000)]
        [TestCase(CreatureConstants.Salamander_Average, GenderConstants.Agender, 60, 500)]
        [TestCase(CreatureConstants.Salamander_Flamebrother, GenderConstants.Agender, 8, 60)]
        [TestCase(CreatureConstants.Wolf, GenderConstants.Male, 55, 85)]
        [TestCase(CreatureConstants.Wolf, GenderConstants.Female, 55, 85)]
        public void RollCalculationsAreAccurate(string creature, string gender, int min, int max)
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

            var theoreticalRoll = RollHelper.GetRollWithFewestDice(min, max);

            Assert.That(baseWeight + heightMultiplierMin * weightMultiplierMin, Is.Positive.And.EqualTo(min), $"Min; Theoretical: {theoreticalRoll}; Height: {heights[creature][creature]}");
            Assert.That(baseWeight + heightMultiplierAvg * weightMultiplierAvg, Is.Positive.And.EqualTo((min + max) / 2).Within(1), $"Average; Theoretical: {theoreticalRoll}; Height: {heights[creature][creature]}");
            Assert.That(baseWeight + heightMultiplierMax * weightMultiplierMax, Is.Positive.And.EqualTo(max), $"Max; Theoretical: {theoreticalRoll}; Height: {heights[creature][creature]}");
        }
    }
}