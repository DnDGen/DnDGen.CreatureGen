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
            heights[CreatureConstants.AnimatedObject_Block_Stone_Colossal][GenderConstants.Agender] = GetGenderFromAverage(36 * 12);
            heights[CreatureConstants.AnimatedObject_Block_Stone_Colossal][CreatureConstants.AnimatedObject_Block_Stone_Colossal] = GetCreatureFromAverage(36 * 12);
            heights[CreatureConstants.AnimatedObject_Block_Stone_Gargantuan][GenderConstants.Agender] = GetGenderFromAverage(18 * 12);
            heights[CreatureConstants.AnimatedObject_Block_Stone_Gargantuan][CreatureConstants.AnimatedObject_Block_Stone_Gargantuan] = GetCreatureFromAverage(18 * 12);
            heights[CreatureConstants.AnimatedObject_Block_Stone_Huge][GenderConstants.Agender] = GetGenderFromAverage(18 * 12 / 2);
            heights[CreatureConstants.AnimatedObject_Block_Stone_Huge][CreatureConstants.AnimatedObject_Block_Stone_Huge] = GetCreatureFromAverage(18 * 12 / 2);
            heights[CreatureConstants.AnimatedObject_Block_Stone_Large][GenderConstants.Agender] = GetGenderFromAverage((9 * 12 + 6) / 2);
            heights[CreatureConstants.AnimatedObject_Block_Stone_Large][CreatureConstants.AnimatedObject_Block_Stone_Large] = GetCreatureFromAverage((9 * 12 + 6) / 2);
            heights[CreatureConstants.AnimatedObject_Block_Stone_Medium][GenderConstants.Agender] = "2*12+5";
            heights[CreatureConstants.AnimatedObject_Block_Stone_Medium][CreatureConstants.AnimatedObject_Block_Stone_Medium] = "1d10";
            heights[CreatureConstants.AnimatedObject_Block_Stone_Small][GenderConstants.Agender] = "1*12+4";
            heights[CreatureConstants.AnimatedObject_Block_Stone_Small][CreatureConstants.AnimatedObject_Block_Stone_Small] = "1d4";
            heights[CreatureConstants.AnimatedObject_Block_Stone_Tiny][GenderConstants.Agender] = GetGenderFromAverage(10);
            heights[CreatureConstants.AnimatedObject_Block_Stone_Tiny][CreatureConstants.AnimatedObject_Block_Stone_Tiny] = GetCreatureFromAverage(10);
            heights[CreatureConstants.AnimatedObject_Block_Wood_Colossal][GenderConstants.Agender] = GetGenderFromAverage(36 * 12);
            heights[CreatureConstants.AnimatedObject_Block_Wood_Colossal][CreatureConstants.AnimatedObject_Block_Wood_Colossal] = GetCreatureFromAverage(36 * 12);
            heights[CreatureConstants.AnimatedObject_Block_Wood_Gargantuan][GenderConstants.Agender] = GetGenderFromAverage(18 * 12);
            heights[CreatureConstants.AnimatedObject_Block_Wood_Gargantuan][CreatureConstants.AnimatedObject_Block_Wood_Gargantuan] = GetCreatureFromAverage(18 * 12);
            heights[CreatureConstants.AnimatedObject_Block_Wood_Huge][GenderConstants.Agender] = GetGenderFromAverage(18 * 12 / 2);
            heights[CreatureConstants.AnimatedObject_Block_Wood_Huge][CreatureConstants.AnimatedObject_Block_Wood_Huge] = GetCreatureFromAverage(18 * 12 / 2);
            heights[CreatureConstants.AnimatedObject_Block_Wood_Large][GenderConstants.Agender] = GetGenderFromAverage((9 * 12 + 6) / 2);
            heights[CreatureConstants.AnimatedObject_Block_Wood_Large][CreatureConstants.AnimatedObject_Block_Wood_Large] = GetCreatureFromAverage((9 * 12 + 6) / 2);
            heights[CreatureConstants.AnimatedObject_Block_Wood_Medium][GenderConstants.Agender] = "2*12+5";
            heights[CreatureConstants.AnimatedObject_Block_Wood_Medium][CreatureConstants.AnimatedObject_Block_Wood_Medium] = "1d10";
            heights[CreatureConstants.AnimatedObject_Block_Wood_Small][GenderConstants.Agender] = "1*12+4";
            heights[CreatureConstants.AnimatedObject_Block_Wood_Small][CreatureConstants.AnimatedObject_Block_Wood_Small] = "1d4";
            heights[CreatureConstants.AnimatedObject_Block_Wood_Tiny][GenderConstants.Agender] = GetGenderFromAverage(10);
            heights[CreatureConstants.AnimatedObject_Block_Wood_Tiny][CreatureConstants.AnimatedObject_Block_Wood_Tiny] = GetCreatureFromAverage(10);
            heights[CreatureConstants.AnimatedObject_Box_Colossal][GenderConstants.Agender] = GetGenderFromAverage(36 * 12);
            heights[CreatureConstants.AnimatedObject_Box_Colossal][CreatureConstants.AnimatedObject_Box_Colossal] = GetCreatureFromAverage(36 * 12);
            heights[CreatureConstants.AnimatedObject_Box_Gargantuan][GenderConstants.Agender] = GetGenderFromAverage(18 * 12);
            heights[CreatureConstants.AnimatedObject_Box_Gargantuan][CreatureConstants.AnimatedObject_Box_Gargantuan] = GetCreatureFromAverage(18 * 12);
            heights[CreatureConstants.AnimatedObject_Box_Huge][GenderConstants.Agender] = GetGenderFromAverage(18 * 12 / 2);
            heights[CreatureConstants.AnimatedObject_Box_Huge][CreatureConstants.AnimatedObject_Box_Huge] = GetCreatureFromAverage(18 * 12 / 2);
            heights[CreatureConstants.AnimatedObject_Box_Large][GenderConstants.Agender] = GetGenderFromAverage((9 * 12 + 6) / 2);
            heights[CreatureConstants.AnimatedObject_Box_Large][CreatureConstants.AnimatedObject_Box_Large] = GetCreatureFromAverage((9 * 12 + 6) / 2);
            heights[CreatureConstants.AnimatedObject_Box_Medium][GenderConstants.Agender] = "2*12+5";
            heights[CreatureConstants.AnimatedObject_Box_Medium][CreatureConstants.AnimatedObject_Box_Medium] = "1d10";
            heights[CreatureConstants.AnimatedObject_Box_Small][GenderConstants.Agender] = "1*12+4";
            heights[CreatureConstants.AnimatedObject_Box_Small][CreatureConstants.AnimatedObject_Box_Small] = "1d4";
            heights[CreatureConstants.AnimatedObject_Box_Tiny][GenderConstants.Agender] = GetGenderFromAverage(10);
            heights[CreatureConstants.AnimatedObject_Box_Tiny][CreatureConstants.AnimatedObject_Box_Tiny] = GetCreatureFromAverage(10);
            heights[CreatureConstants.AnimatedObject_Carpet_Colossal][GenderConstants.Agender] = GetGenderFromAverage(80 * 12);
            heights[CreatureConstants.AnimatedObject_Carpet_Colossal][CreatureConstants.AnimatedObject_Carpet_Colossal] = GetCreatureFromAverage(80 * 12);
            heights[CreatureConstants.AnimatedObject_Carpet_Gargantuan][GenderConstants.Agender] = GetGenderFromAverage(40 * 12);
            heights[CreatureConstants.AnimatedObject_Carpet_Gargantuan][CreatureConstants.AnimatedObject_Carpet_Gargantuan] = GetCreatureFromAverage(40 * 12);
            heights[CreatureConstants.AnimatedObject_Carpet_Huge][GenderConstants.Agender] = GetGenderFromAverage(20 * 12);
            heights[CreatureConstants.AnimatedObject_Carpet_Huge][CreatureConstants.AnimatedObject_Carpet_Huge] = GetCreatureFromAverage(20 * 12);
            heights[CreatureConstants.AnimatedObject_Carpet_Large][GenderConstants.Agender] = GetGenderFromAverage(10 * 12);
            heights[CreatureConstants.AnimatedObject_Carpet_Large][CreatureConstants.AnimatedObject_Carpet_Large] = GetCreatureFromAverage(10 * 12);
            heights[CreatureConstants.AnimatedObject_Carpet_Medium][GenderConstants.Agender] = GetGenderFromAverage(5 * 12);
            heights[CreatureConstants.AnimatedObject_Carpet_Medium][CreatureConstants.AnimatedObject_Carpet_Medium] = GetCreatureFromAverage(5 * 12);
            heights[CreatureConstants.AnimatedObject_Carpet_Small][GenderConstants.Agender] = GetGenderFromAverage(4 * 12);
            heights[CreatureConstants.AnimatedObject_Carpet_Small][CreatureConstants.AnimatedObject_Carpet_Small] = GetCreatureFromAverage(4 * 12);
            heights[CreatureConstants.AnimatedObject_Carpet_Tiny][GenderConstants.Agender] = GetGenderFromAverage(2 * 12 + 6);
            heights[CreatureConstants.AnimatedObject_Carpet_Tiny][CreatureConstants.AnimatedObject_Carpet_Tiny] = GetCreatureFromAverage(2 * 12 + 6);
            heights[CreatureConstants.AnimatedObject_Carriage_Colossal][GenderConstants.Agender] = GetGenderFromAverage(88 * 12);
            heights[CreatureConstants.AnimatedObject_Carriage_Colossal][CreatureConstants.AnimatedObject_Carriage_Colossal] = GetCreatureFromAverage(88 * 12);
            heights[CreatureConstants.AnimatedObject_Carriage_Gargantuan][GenderConstants.Agender] = GetGenderFromAverage(44 * 12);
            heights[CreatureConstants.AnimatedObject_Carriage_Gargantuan][CreatureConstants.AnimatedObject_Carriage_Gargantuan] = GetCreatureFromAverage(44 * 12);
            heights[CreatureConstants.AnimatedObject_Carriage_Huge][GenderConstants.Agender] = GetGenderFromAverage(22 * 12);
            heights[CreatureConstants.AnimatedObject_Carriage_Huge][CreatureConstants.AnimatedObject_Carriage_Huge] = GetCreatureFromAverage(22 * 12);
            heights[CreatureConstants.AnimatedObject_Carriage_Large][GenderConstants.Agender] = GetGenderFromAverage(11 * 12);
            heights[CreatureConstants.AnimatedObject_Carriage_Large][CreatureConstants.AnimatedObject_Carriage_Large] = GetCreatureFromAverage(11 * 12);
            heights[CreatureConstants.AnimatedObject_Carriage_Medium][GenderConstants.Agender] = GetGenderFromAverage(6 * 12);
            heights[CreatureConstants.AnimatedObject_Carriage_Medium][CreatureConstants.AnimatedObject_Carriage_Medium] = GetCreatureFromAverage(6 * 12);
            heights[CreatureConstants.AnimatedObject_Carriage_Small][GenderConstants.Agender] = GetGenderFromAverage(3 * 12);
            heights[CreatureConstants.AnimatedObject_Carriage_Small][CreatureConstants.AnimatedObject_Carriage_Small] = GetCreatureFromAverage(3 * 12);
            heights[CreatureConstants.AnimatedObject_Carriage_Tiny][GenderConstants.Agender] = GetGenderFromAverage(12);
            heights[CreatureConstants.AnimatedObject_Carriage_Tiny][CreatureConstants.AnimatedObject_Carriage_Tiny] = GetCreatureFromAverage(12);
            heights[CreatureConstants.AnimatedObject_Chain_Colossal][GenderConstants.Agender] = GetGenderFromAverage(160 * 12);
            heights[CreatureConstants.AnimatedObject_Chain_Colossal][CreatureConstants.AnimatedObject_Chain_Colossal] = GetCreatureFromAverage(160 * 12);
            heights[CreatureConstants.AnimatedObject_Chain_Gargantuan][GenderConstants.Agender] = GetGenderFromAverage(80 * 12);
            heights[CreatureConstants.AnimatedObject_Chain_Gargantuan][CreatureConstants.AnimatedObject_Chain_Gargantuan] = GetCreatureFromAverage(80 * 12);
            heights[CreatureConstants.AnimatedObject_Chain_Huge][GenderConstants.Agender] = GetGenderFromAverage(40 * 12);
            heights[CreatureConstants.AnimatedObject_Chain_Huge][CreatureConstants.AnimatedObject_Chain_Huge] = GetCreatureFromAverage(40 * 12);
            heights[CreatureConstants.AnimatedObject_Chain_Large][GenderConstants.Agender] = GetGenderFromAverage(20 * 12);
            heights[CreatureConstants.AnimatedObject_Chain_Large][CreatureConstants.AnimatedObject_Chain_Large] = GetCreatureFromAverage(20 * 12);
            heights[CreatureConstants.AnimatedObject_Chain_Medium][GenderConstants.Agender] = GetGenderFromAverage(10 * 12);
            heights[CreatureConstants.AnimatedObject_Chain_Medium][CreatureConstants.AnimatedObject_Chain_Medium] = GetCreatureFromAverage(10 * 12);
            heights[CreatureConstants.AnimatedObject_Chain_Small][GenderConstants.Agender] = GetGenderFromAverage(5 * 12);
            heights[CreatureConstants.AnimatedObject_Chain_Small][CreatureConstants.AnimatedObject_Chain_Small] = GetCreatureFromAverage(5 * 12);
            heights[CreatureConstants.AnimatedObject_Chain_Tiny][GenderConstants.Agender] = GetGenderFromAverage(2 * 12 + 6);
            heights[CreatureConstants.AnimatedObject_Chain_Tiny][CreatureConstants.AnimatedObject_Chain_Tiny] = GetCreatureFromAverage(2 * 12 + 6);
            heights[CreatureConstants.AnimatedObject_Chair_Colossal][GenderConstants.Agender] = "480";
            heights[CreatureConstants.AnimatedObject_Chair_Colossal][CreatureConstants.AnimatedObject_Chair_Colossal] = RollHelper.GetRollWithFewestDice(480, 544, 640);
            heights[CreatureConstants.AnimatedObject_Chair_Gargantuan][GenderConstants.Agender] = "240";
            heights[CreatureConstants.AnimatedObject_Chair_Gargantuan][CreatureConstants.AnimatedObject_Chair_Gargantuan] = RollHelper.GetRollWithFewestDice(240, 272, 320);
            heights[CreatureConstants.AnimatedObject_Chair_Huge][GenderConstants.Agender] = "120";
            heights[CreatureConstants.AnimatedObject_Chair_Huge][CreatureConstants.AnimatedObject_Chair_Huge] = RollHelper.GetRollWithFewestDice(120, 136, 160);
            heights[CreatureConstants.AnimatedObject_Chair_Large][GenderConstants.Agender] = "60";
            heights[CreatureConstants.AnimatedObject_Chair_Large][CreatureConstants.AnimatedObject_Chair_Large] = RollHelper.GetRollWithFewestDice(60, 68, 80);
            heights[CreatureConstants.AnimatedObject_Chair_Medium][GenderConstants.Agender] = "30";
            heights[CreatureConstants.AnimatedObject_Chair_Medium][CreatureConstants.AnimatedObject_Chair_Medium] = RollHelper.GetRollWithFewestDice(30, 34, 40);
            heights[CreatureConstants.AnimatedObject_Chair_Small][GenderConstants.Agender] = "15";
            heights[CreatureConstants.AnimatedObject_Chair_Small][CreatureConstants.AnimatedObject_Chair_Small] = RollHelper.GetRollWithFewestDice(15, 17, 20);
            heights[CreatureConstants.AnimatedObject_Chair_Tiny][GenderConstants.Agender] = "7";
            heights[CreatureConstants.AnimatedObject_Chair_Tiny][CreatureConstants.AnimatedObject_Chair_Tiny] = RollHelper.GetRollWithFewestDice(7, 8, 10);
            heights[CreatureConstants.AnimatedObject_Clothes_Colossal][GenderConstants.Agender] = "1080";
            heights[CreatureConstants.AnimatedObject_Clothes_Colossal][CreatureConstants.AnimatedObject_Clothes_Colossal] = "10d12";
            heights[CreatureConstants.AnimatedObject_Clothes_Gargantuan][GenderConstants.Agender] = "540";
            heights[CreatureConstants.AnimatedObject_Clothes_Gargantuan][CreatureConstants.AnimatedObject_Clothes_Gargantuan] = "6d10";
            heights[CreatureConstants.AnimatedObject_Clothes_Huge][GenderConstants.Agender] = "270";
            heights[CreatureConstants.AnimatedObject_Clothes_Huge][CreatureConstants.AnimatedObject_Clothes_Huge] = "3d10";
            heights[CreatureConstants.AnimatedObject_Clothes_Large][GenderConstants.Agender] = "15*12+4";
            heights[CreatureConstants.AnimatedObject_Clothes_Large][CreatureConstants.AnimatedObject_Clothes_Large] = "1d12";
            heights[CreatureConstants.AnimatedObject_Clothes_Medium][GenderConstants.Agender] = "4*12+5";
            heights[CreatureConstants.AnimatedObject_Clothes_Medium][CreatureConstants.AnimatedObject_Clothes_Medium] = "2d10";
            heights[CreatureConstants.AnimatedObject_Clothes_Small][GenderConstants.Agender] = "2*12+6";
            heights[CreatureConstants.AnimatedObject_Clothes_Small][CreatureConstants.AnimatedObject_Clothes_Small] = "2d4";
            heights[CreatureConstants.AnimatedObject_Clothes_Tiny][GenderConstants.Agender] = GetGenderFromAverage(18);
            heights[CreatureConstants.AnimatedObject_Clothes_Tiny][CreatureConstants.AnimatedObject_Clothes_Tiny] = GetCreatureFromAverage(18);
            heights[CreatureConstants.AnimatedObject_Ladder_Colossal][GenderConstants.Agender] = GetGenderFromAverage(1920);
            heights[CreatureConstants.AnimatedObject_Ladder_Colossal][CreatureConstants.AnimatedObject_Ladder_Colossal] = GetCreatureFromAverage(1920);
            heights[CreatureConstants.AnimatedObject_Ladder_Gargantuan][GenderConstants.Agender] = GetGenderFromAverage(960);
            heights[CreatureConstants.AnimatedObject_Ladder_Gargantuan][CreatureConstants.AnimatedObject_Ladder_Gargantuan] = GetCreatureFromAverage(960);
            heights[CreatureConstants.AnimatedObject_Ladder_Huge][GenderConstants.Agender] = GetGenderFromAverage(480);
            heights[CreatureConstants.AnimatedObject_Ladder_Huge][CreatureConstants.AnimatedObject_Ladder_Huge] = GetCreatureFromAverage(480);
            heights[CreatureConstants.AnimatedObject_Ladder_Large][GenderConstants.Agender] = GetGenderFromAverage(240);
            heights[CreatureConstants.AnimatedObject_Ladder_Large][CreatureConstants.AnimatedObject_Ladder_Large] = GetCreatureFromAverage(240);
            heights[CreatureConstants.AnimatedObject_Ladder_Medium][GenderConstants.Agender] = GetGenderFromAverage(120);
            heights[CreatureConstants.AnimatedObject_Ladder_Medium][CreatureConstants.AnimatedObject_Ladder_Medium] = GetCreatureFromAverage(120);
            heights[CreatureConstants.AnimatedObject_Ladder_Small][GenderConstants.Agender] = GetGenderFromAverage(60);
            heights[CreatureConstants.AnimatedObject_Ladder_Small][CreatureConstants.AnimatedObject_Ladder_Small] = GetCreatureFromAverage(60);
            heights[CreatureConstants.AnimatedObject_Ladder_Tiny][GenderConstants.Agender] = GetGenderFromAverage(30);
            heights[CreatureConstants.AnimatedObject_Ladder_Tiny][CreatureConstants.AnimatedObject_Ladder_Tiny] = GetCreatureFromAverage(30);
            heights[CreatureConstants.AnimatedObject_Rope_Colossal][GenderConstants.Agender] = GetGenderFromAverage(9600);
            heights[CreatureConstants.AnimatedObject_Rope_Colossal][CreatureConstants.AnimatedObject_Rope_Colossal] = GetCreatureFromAverage(9600);
            heights[CreatureConstants.AnimatedObject_Rope_Gargantuan][GenderConstants.Agender] = GetGenderFromAverage(4800);
            heights[CreatureConstants.AnimatedObject_Rope_Gargantuan][CreatureConstants.AnimatedObject_Rope_Gargantuan] = GetCreatureFromAverage(4800);
            heights[CreatureConstants.AnimatedObject_Rope_Huge][GenderConstants.Agender] = GetGenderFromAverage(2400);
            heights[CreatureConstants.AnimatedObject_Rope_Huge][CreatureConstants.AnimatedObject_Rope_Huge] = GetCreatureFromAverage(2400);
            heights[CreatureConstants.AnimatedObject_Rope_Large][GenderConstants.Agender] = GetGenderFromAverage(1200);
            heights[CreatureConstants.AnimatedObject_Rope_Large][CreatureConstants.AnimatedObject_Rope_Large] = GetCreatureFromAverage(1200);
            heights[CreatureConstants.AnimatedObject_Rope_Medium][GenderConstants.Agender] = GetGenderFromAverage(50 * 12);
            heights[CreatureConstants.AnimatedObject_Rope_Medium][CreatureConstants.AnimatedObject_Rope_Medium] = GetCreatureFromAverage(50 * 12);
            heights[CreatureConstants.AnimatedObject_Rope_Small][GenderConstants.Agender] = GetGenderFromAverage(25 * 12);
            heights[CreatureConstants.AnimatedObject_Rope_Small][CreatureConstants.AnimatedObject_Rope_Small] = GetCreatureFromAverage(25 * 12);
            heights[CreatureConstants.AnimatedObject_Rope_Tiny][GenderConstants.Agender] = GetGenderFromAverage(144);
            heights[CreatureConstants.AnimatedObject_Rope_Tiny][CreatureConstants.AnimatedObject_Rope_Tiny] = GetCreatureFromAverage(144);
            heights[CreatureConstants.AnimatedObject_Rug_Colossal][GenderConstants.Agender] = GetGenderFromAverage(80 * 12);
            heights[CreatureConstants.AnimatedObject_Rug_Colossal][CreatureConstants.AnimatedObject_Rug_Colossal] = GetCreatureFromAverage(80 * 12);
            heights[CreatureConstants.AnimatedObject_Rug_Gargantuan][GenderConstants.Agender] = GetGenderFromAverage(40 * 12);
            heights[CreatureConstants.AnimatedObject_Rug_Gargantuan][CreatureConstants.AnimatedObject_Rug_Gargantuan] = GetCreatureFromAverage(40 * 12);
            heights[CreatureConstants.AnimatedObject_Rug_Huge][GenderConstants.Agender] = GetGenderFromAverage(20 * 12);
            heights[CreatureConstants.AnimatedObject_Rug_Huge][CreatureConstants.AnimatedObject_Rug_Huge] = GetCreatureFromAverage(20 * 12);
            heights[CreatureConstants.AnimatedObject_Rug_Large][GenderConstants.Agender] = GetGenderFromAverage(10 * 12);
            heights[CreatureConstants.AnimatedObject_Rug_Large][CreatureConstants.AnimatedObject_Rug_Large] = GetCreatureFromAverage(10 * 12);
            heights[CreatureConstants.AnimatedObject_Rug_Medium][GenderConstants.Agender] = GetGenderFromAverage(5 * 12);
            heights[CreatureConstants.AnimatedObject_Rug_Medium][CreatureConstants.AnimatedObject_Rug_Medium] = GetCreatureFromAverage(5 * 12);
            heights[CreatureConstants.AnimatedObject_Rug_Small][GenderConstants.Agender] = GetGenderFromAverage(4 * 12);
            heights[CreatureConstants.AnimatedObject_Rug_Small][CreatureConstants.AnimatedObject_Rug_Small] = GetCreatureFromAverage(4 * 12);
            heights[CreatureConstants.AnimatedObject_Rug_Tiny][GenderConstants.Agender] = GetGenderFromAverage(2 * 12 + 6);
            heights[CreatureConstants.AnimatedObject_Rug_Tiny][CreatureConstants.AnimatedObject_Rug_Tiny] = GetCreatureFromAverage(2 * 12 + 6);
            heights[CreatureConstants.AnimatedObject_Sled_Colossal][GenderConstants.Agender] = "1792";
            heights[CreatureConstants.AnimatedObject_Sled_Colossal][CreatureConstants.AnimatedObject_Sled_Colossal] = "18d20";
            heights[CreatureConstants.AnimatedObject_Sled_Gargantuan][GenderConstants.Agender] = "896";
            heights[CreatureConstants.AnimatedObject_Sled_Gargantuan][CreatureConstants.AnimatedObject_Sled_Gargantuan] = "9d20";
            heights[CreatureConstants.AnimatedObject_Sled_Huge][GenderConstants.Agender] = "448";
            heights[CreatureConstants.AnimatedObject_Sled_Huge][CreatureConstants.AnimatedObject_Sled_Huge] = "8d12";
            heights[CreatureConstants.AnimatedObject_Sled_Large][GenderConstants.Agender] = "224";
            heights[CreatureConstants.AnimatedObject_Sled_Large][CreatureConstants.AnimatedObject_Sled_Large] = "6d8";
            heights[CreatureConstants.AnimatedObject_Sled_Medium][GenderConstants.Agender] = "112";
            heights[CreatureConstants.AnimatedObject_Sled_Medium][CreatureConstants.AnimatedObject_Sled_Medium] = "3d8";
            heights[CreatureConstants.AnimatedObject_Sled_Small][GenderConstants.Agender] = "66";
            heights[CreatureConstants.AnimatedObject_Sled_Small][CreatureConstants.AnimatedObject_Sled_Small] = "3d4";
            heights[CreatureConstants.AnimatedObject_Sled_Tiny][GenderConstants.Agender] = "33";
            heights[CreatureConstants.AnimatedObject_Sled_Tiny][CreatureConstants.AnimatedObject_Sled_Tiny] = "2d3";
            heights[CreatureConstants.AnimatedObject_Statue_Animal_Colossal][GenderConstants.Agender] = "60*12";
            heights[CreatureConstants.AnimatedObject_Statue_Animal_Colossal][CreatureConstants.AnimatedObject_Statue_Animal_Colossal] = RollHelper.GetRollWithFewestDice(60 * 12, 64 * 12, 128 * 12);
            heights[CreatureConstants.AnimatedObject_Statue_Animal_Gargantuan][GenderConstants.Agender] = "30*12";
            heights[CreatureConstants.AnimatedObject_Statue_Animal_Gargantuan][CreatureConstants.AnimatedObject_Statue_Animal_Gargantuan] = RollHelper.GetRollWithFewestDice(30 * 12, 32 * 12, 64 * 12 - 1);
            heights[CreatureConstants.AnimatedObject_Statue_Animal_Huge][GenderConstants.Agender] = "15*12";
            heights[CreatureConstants.AnimatedObject_Statue_Animal_Huge][CreatureConstants.AnimatedObject_Statue_Animal_Huge] = RollHelper.GetRollWithFewestDice(15 * 12, 16 * 12, 32 * 12 - 1);
            heights[CreatureConstants.AnimatedObject_Statue_Animal_Large][GenderConstants.Agender] = "5*12";
            heights[CreatureConstants.AnimatedObject_Statue_Animal_Large][CreatureConstants.AnimatedObject_Statue_Animal_Large] = RollHelper.GetRollWithFewestDice(5 * 12, 8 * 12, 16 * 12 - 1);
            heights[CreatureConstants.AnimatedObject_Statue_Animal_Medium][GenderConstants.Agender] = "2*12";
            heights[CreatureConstants.AnimatedObject_Statue_Animal_Medium][CreatureConstants.AnimatedObject_Statue_Animal_Medium] = RollHelper.GetRollWithFewestDice(2 * 12, 4 * 12, 8 * 12 - 1);
            heights[CreatureConstants.AnimatedObject_Statue_Animal_Small][GenderConstants.Agender] = "12";
            heights[CreatureConstants.AnimatedObject_Statue_Animal_Small][CreatureConstants.AnimatedObject_Statue_Animal_Small] = RollHelper.GetRollWithFewestDice(12, 2 * 12, 4 * 12 - 1);
            heights[CreatureConstants.AnimatedObject_Statue_Animal_Tiny][GenderConstants.Agender] = "11";
            heights[CreatureConstants.AnimatedObject_Statue_Animal_Tiny][CreatureConstants.AnimatedObject_Statue_Animal_Tiny] = RollHelper.GetRollWithFewestDice(11, 12, 23);
            heights[CreatureConstants.AnimatedObject_Statue_Humanoid_Colossal][GenderConstants.Agender] = "60*12";
            heights[CreatureConstants.AnimatedObject_Statue_Humanoid_Colossal][CreatureConstants.AnimatedObject_Statue_Humanoid_Colossal] = RollHelper.GetRollWithFewestDice(60 * 12, 64 * 12, 128 * 12);
            heights[CreatureConstants.AnimatedObject_Statue_Humanoid_Gargantuan][GenderConstants.Agender] = "30*12";
            heights[CreatureConstants.AnimatedObject_Statue_Humanoid_Gargantuan][CreatureConstants.AnimatedObject_Statue_Humanoid_Gargantuan] = RollHelper.GetRollWithFewestDice(30 * 12, 32 * 12, 64 * 12 - 1);
            heights[CreatureConstants.AnimatedObject_Statue_Humanoid_Huge][GenderConstants.Agender] = "15*12";
            heights[CreatureConstants.AnimatedObject_Statue_Humanoid_Huge][CreatureConstants.AnimatedObject_Statue_Humanoid_Huge] = RollHelper.GetRollWithFewestDice(15 * 12, 16 * 12, 32 * 12 - 1);
            heights[CreatureConstants.AnimatedObject_Statue_Humanoid_Large][GenderConstants.Agender] = "5*12";
            heights[CreatureConstants.AnimatedObject_Statue_Humanoid_Large][CreatureConstants.AnimatedObject_Statue_Humanoid_Large] = RollHelper.GetRollWithFewestDice(5 * 12, 8 * 12, 16 * 12 - 1);
            heights[CreatureConstants.AnimatedObject_Statue_Humanoid_Medium][GenderConstants.Agender] = "2*12";
            heights[CreatureConstants.AnimatedObject_Statue_Humanoid_Medium][CreatureConstants.AnimatedObject_Statue_Humanoid_Medium] = RollHelper.GetRollWithFewestDice(2 * 12, 4 * 12, 8 * 12 - 1);
            heights[CreatureConstants.AnimatedObject_Statue_Humanoid_Small][GenderConstants.Agender] = "12";
            heights[CreatureConstants.AnimatedObject_Statue_Humanoid_Small][CreatureConstants.AnimatedObject_Statue_Humanoid_Small] = RollHelper.GetRollWithFewestDice(12, 2 * 12, 4 * 12 - 1);
            heights[CreatureConstants.AnimatedObject_Statue_Humanoid_Tiny][GenderConstants.Agender] = "11";
            heights[CreatureConstants.AnimatedObject_Statue_Humanoid_Tiny][CreatureConstants.AnimatedObject_Statue_Humanoid_Tiny] = RollHelper.GetRollWithFewestDice(11, 12, 23);
            heights[CreatureConstants.AnimatedObject_Stool_Colossal][GenderConstants.Agender] = "272";
            heights[CreatureConstants.AnimatedObject_Stool_Colossal][CreatureConstants.AnimatedObject_Stool_Colossal] = "6d8";
            heights[CreatureConstants.AnimatedObject_Stool_Gargantuan][GenderConstants.Agender] = "136";
            heights[CreatureConstants.AnimatedObject_Stool_Gargantuan][CreatureConstants.AnimatedObject_Stool_Gargantuan] = "4d6";
            heights[CreatureConstants.AnimatedObject_Stool_Huge][GenderConstants.Agender] = "68";
            heights[CreatureConstants.AnimatedObject_Stool_Huge][CreatureConstants.AnimatedObject_Stool_Huge] = "3d4";
            heights[CreatureConstants.AnimatedObject_Stool_Large][GenderConstants.Agender] = "34";
            heights[CreatureConstants.AnimatedObject_Stool_Large][CreatureConstants.AnimatedObject_Stool_Large] = "2d3";
            heights[CreatureConstants.AnimatedObject_Stool_Medium][GenderConstants.Agender] = "17";
            heights[CreatureConstants.AnimatedObject_Stool_Medium][CreatureConstants.AnimatedObject_Stool_Medium] = "1d3";
            heights[CreatureConstants.AnimatedObject_Stool_Small][GenderConstants.Agender] = "8";
            heights[CreatureConstants.AnimatedObject_Stool_Small][CreatureConstants.AnimatedObject_Stool_Small] = "1d2";
            heights[CreatureConstants.AnimatedObject_Stool_Tiny][GenderConstants.Agender] = "4";
            heights[CreatureConstants.AnimatedObject_Stool_Tiny][CreatureConstants.AnimatedObject_Stool_Tiny] = "1";
            heights[CreatureConstants.AnimatedObject_Table_Colossal][GenderConstants.Agender] = "416";
            heights[CreatureConstants.AnimatedObject_Table_Colossal][CreatureConstants.AnimatedObject_Table_Colossal] = "8d12";
            heights[CreatureConstants.AnimatedObject_Table_Gargantuan][GenderConstants.Agender] = "208";
            heights[CreatureConstants.AnimatedObject_Table_Gargantuan][CreatureConstants.AnimatedObject_Table_Gargantuan] = "6d8";
            heights[CreatureConstants.AnimatedObject_Table_Huge][GenderConstants.Agender] = "104";
            heights[CreatureConstants.AnimatedObject_Table_Huge][CreatureConstants.AnimatedObject_Table_Huge] = "4d6";
            heights[CreatureConstants.AnimatedObject_Table_Large][GenderConstants.Agender] = "52";
            heights[CreatureConstants.AnimatedObject_Table_Large][CreatureConstants.AnimatedObject_Table_Large] = "3d4";
            heights[CreatureConstants.AnimatedObject_Table_Medium][GenderConstants.Agender] = "26";
            heights[CreatureConstants.AnimatedObject_Table_Medium][CreatureConstants.AnimatedObject_Table_Medium] = "2d3";
            heights[CreatureConstants.AnimatedObject_Table_Small][GenderConstants.Agender] = "13";
            heights[CreatureConstants.AnimatedObject_Table_Small][CreatureConstants.AnimatedObject_Table_Small] = "1d3";
            heights[CreatureConstants.AnimatedObject_Table_Tiny][GenderConstants.Agender] = "6";
            heights[CreatureConstants.AnimatedObject_Table_Tiny][CreatureConstants.AnimatedObject_Table_Tiny] = "1";
            heights[CreatureConstants.AnimatedObject_Tapestry_Colossal][GenderConstants.Agender] = GetGenderFromAverage(80 * 12);
            heights[CreatureConstants.AnimatedObject_Tapestry_Colossal][CreatureConstants.AnimatedObject_Tapestry_Colossal] = GetCreatureFromAverage(80 * 12);
            heights[CreatureConstants.AnimatedObject_Tapestry_Gargantuan][GenderConstants.Agender] = GetGenderFromAverage(40 * 12);
            heights[CreatureConstants.AnimatedObject_Tapestry_Gargantuan][CreatureConstants.AnimatedObject_Tapestry_Gargantuan] = GetCreatureFromAverage(40 * 12);
            heights[CreatureConstants.AnimatedObject_Tapestry_Huge][GenderConstants.Agender] = GetGenderFromAverage(20 * 12);
            heights[CreatureConstants.AnimatedObject_Tapestry_Huge][CreatureConstants.AnimatedObject_Tapestry_Huge] = GetCreatureFromAverage(20 * 12);
            heights[CreatureConstants.AnimatedObject_Tapestry_Large][GenderConstants.Agender] = GetGenderFromAverage(10 * 12);
            heights[CreatureConstants.AnimatedObject_Tapestry_Large][CreatureConstants.AnimatedObject_Tapestry_Large] = RollHelper.GetRollWithFewestDice(11, 11 * 12, 16 * 12);
            heights[CreatureConstants.AnimatedObject_Tapestry_Medium][GenderConstants.Agender] = GetGenderFromAverage(5 * 12);
            heights[CreatureConstants.AnimatedObject_Tapestry_Medium][CreatureConstants.AnimatedObject_Tapestry_Medium] = GetCreatureFromAverage(5 * 12);
            heights[CreatureConstants.AnimatedObject_Tapestry_Small][GenderConstants.Agender] = GetGenderFromAverage(4 * 12);
            heights[CreatureConstants.AnimatedObject_Tapestry_Small][CreatureConstants.AnimatedObject_Tapestry_Small] = GetCreatureFromAverage(4 * 12);
            heights[CreatureConstants.AnimatedObject_Tapestry_Tiny][GenderConstants.Agender] = GetGenderFromAverage(2 * 12 + 6);
            heights[CreatureConstants.AnimatedObject_Tapestry_Tiny][CreatureConstants.AnimatedObject_Tapestry_Tiny] = GetCreatureFromAverage(2 * 12 + 6);
            heights[CreatureConstants.AnimatedObject_Wagon_Colossal][GenderConstants.Agender] = GetGenderFromAverage(88 * 12);
            heights[CreatureConstants.AnimatedObject_Wagon_Colossal][CreatureConstants.AnimatedObject_Wagon_Colossal] = GetCreatureFromAverage(88 * 12);
            heights[CreatureConstants.AnimatedObject_Wagon_Gargantuan][GenderConstants.Agender] = GetGenderFromAverage(44 * 12);
            heights[CreatureConstants.AnimatedObject_Wagon_Gargantuan][CreatureConstants.AnimatedObject_Wagon_Gargantuan] = GetCreatureFromAverage(44 * 12);
            heights[CreatureConstants.AnimatedObject_Wagon_Huge][GenderConstants.Agender] = GetGenderFromAverage(22 * 12);
            heights[CreatureConstants.AnimatedObject_Wagon_Huge][CreatureConstants.AnimatedObject_Wagon_Huge] = GetCreatureFromAverage(22 * 12);
            heights[CreatureConstants.AnimatedObject_Wagon_Large][GenderConstants.Agender] = GetGenderFromAverage(11 * 12);
            heights[CreatureConstants.AnimatedObject_Wagon_Large][CreatureConstants.AnimatedObject_Wagon_Large] = GetCreatureFromAverage(11 * 12);
            heights[CreatureConstants.AnimatedObject_Wagon_Medium][GenderConstants.Agender] = GetGenderFromAverage(6 * 12);
            heights[CreatureConstants.AnimatedObject_Wagon_Medium][CreatureConstants.AnimatedObject_Wagon_Medium] = GetCreatureFromAverage(6 * 12);
            heights[CreatureConstants.AnimatedObject_Wagon_Small][GenderConstants.Agender] = GetGenderFromAverage(3 * 12);
            heights[CreatureConstants.AnimatedObject_Wagon_Small][CreatureConstants.AnimatedObject_Wagon_Small] = GetCreatureFromAverage(3 * 12);
            heights[CreatureConstants.AnimatedObject_Wagon_Tiny][GenderConstants.Agender] = GetGenderFromAverage(12);
            heights[CreatureConstants.AnimatedObject_Wagon_Tiny][CreatureConstants.AnimatedObject_Wagon_Tiny] = GetCreatureFromAverage(12);
            heights[CreatureConstants.Ankheg][GenderConstants.Female] = GetGenderFromAverage(10 * 12);
            heights[CreatureConstants.Ankheg][GenderConstants.Male] = GetGenderFromAverage(10 * 12);
            heights[CreatureConstants.Ankheg][CreatureConstants.Ankheg] = GetCreatureFromAverage(10 * 12);
            heights[CreatureConstants.Arrowhawk_Juvenile][GenderConstants.Female] = GetGenderFromAverage(5 * 12);
            heights[CreatureConstants.Arrowhawk_Juvenile][GenderConstants.Male] = GetGenderFromAverage(5 * 12);
            heights[CreatureConstants.Arrowhawk_Juvenile][CreatureConstants.Ankheg] = GetCreatureFromAverage(5 * 12);
            heights[CreatureConstants.Arrowhawk_Adult][GenderConstants.Female] = GetGenderFromAverage(10 * 12);
            heights[CreatureConstants.Arrowhawk_Adult][GenderConstants.Male] = GetGenderFromAverage(10 * 12);
            heights[CreatureConstants.Arrowhawk_Adult][CreatureConstants.Ankheg] = GetCreatureFromAverage(10 * 12);
            heights[CreatureConstants.Arrowhawk_Elder][GenderConstants.Female] = GetGenderFromAverage(20 * 12);
            heights[CreatureConstants.Arrowhawk_Elder][GenderConstants.Male] = GetGenderFromAverage(20 * 12);
            heights[CreatureConstants.Arrowhawk_Elder][CreatureConstants.Ankheg] = GetCreatureFromAverage(20 * 12);
            heights[CreatureConstants.Bear_Brown][GenderConstants.Female] = GetGenderFromAverage(9 * 12);
            heights[CreatureConstants.Bear_Brown][GenderConstants.Male] = GetGenderFromAverage(9 * 12);
            heights[CreatureConstants.Bear_Brown][CreatureConstants.Bear_Brown] = GetCreatureFromAverage(9 * 12);
            heights[CreatureConstants.Bugbear][GenderConstants.Female] = "5*12";
            heights[CreatureConstants.Bugbear][GenderConstants.Male] = "5*12";
            heights[CreatureConstants.Bugbear][CreatureConstants.Bugbear] = RollHelper.GetRollWithFewestDice(5 * 12, 6 * 12, 8 * 12);
            heights[CreatureConstants.Cat][GenderConstants.Female] = GetGenderFromAverage(17); //Small Animal
            heights[CreatureConstants.Cat][GenderConstants.Male] = GetGenderFromAverage(19);
            heights[CreatureConstants.Cat][CreatureConstants.Cat] = GetCreatureFromAverage(18);
            heights[CreatureConstants.Centaur][GenderConstants.Female] = "6*12";
            heights[CreatureConstants.Centaur][GenderConstants.Male] = "6*12";
            heights[CreatureConstants.Centaur][CreatureConstants.Bugbear] = RollHelper.GetRollWithFewestDice(6 * 12, 7 * 12, 9 * 12);
            heights[CreatureConstants.Criosphinx][GenderConstants.Male] = GetGenderFromAverage(120);
            heights[CreatureConstants.Criosphinx][CreatureConstants.Criosphinx] = GetCreatureFromAverage(120);
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
            heights[CreatureConstants.Ettin][GenderConstants.Female] = "12*12+2";
            heights[CreatureConstants.Ettin][GenderConstants.Male] = "13*12-2";
            heights[CreatureConstants.Ettin][CreatureConstants.Ettin] = "2d6";
            heights[CreatureConstants.Giant_Cloud][GenderConstants.Female] = "290"; //Huge
            heights[CreatureConstants.Giant_Cloud][GenderConstants.Male] = "270";
            heights[CreatureConstants.Giant_Cloud][CreatureConstants.Giant_Cloud] = "3d10";
            heights[CreatureConstants.Giant_Hill][GenderConstants.Female] = "15*12+4"; //Large
            heights[CreatureConstants.Giant_Hill][GenderConstants.Male] = "16*12";
            heights[CreatureConstants.Giant_Hill][CreatureConstants.Giant_Hill] = "1d12";
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
            heights[CreatureConstants.Hieracosphinx][GenderConstants.Male] = GetGenderFromAverage(10 * 12);
            heights[CreatureConstants.Hieracosphinx][CreatureConstants.Hieracosphinx] = GetCreatureFromAverage(10 * 12);
            heights[CreatureConstants.Hobgoblin][GenderConstants.Female] = "4*12+0";
            heights[CreatureConstants.Hobgoblin][GenderConstants.Male] = "4*12+2";
            heights[CreatureConstants.Hobgoblin][CreatureConstants.Hobgoblin] = "2d8";
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
            heights[CreatureConstants.Kobold][GenderConstants.Female] = "2*12+4";
            heights[CreatureConstants.Kobold][GenderConstants.Male] = "2*12+6";
            heights[CreatureConstants.Kobold][CreatureConstants.Kobold] = "2d4";
            heights[CreatureConstants.Lizardfolk][GenderConstants.Female] = "5*12";
            heights[CreatureConstants.Lizardfolk][GenderConstants.Male] = "5*12";
            heights[CreatureConstants.Lizardfolk][CreatureConstants.Lizardfolk] = RollHelper.GetRollWithFewestDice(5 * 12, 6 * 12, 7 * 12);
            heights[CreatureConstants.Locathah][GenderConstants.Female] = GetGenderFromAverage(5 * 12);
            heights[CreatureConstants.Locathah][GenderConstants.Male] = GetGenderFromAverage(5 * 12);
            heights[CreatureConstants.Locathah][CreatureConstants.Locathah] = GetCreatureFromAverage(5 * 12);
            heights[CreatureConstants.Merfolk][GenderConstants.Female] = "5*12+8";
            heights[CreatureConstants.Merfolk][GenderConstants.Male] = "5*12+10";
            heights[CreatureConstants.Merfolk][CreatureConstants.Merfolk] = "2d10";
            heights[CreatureConstants.Minotaur][GenderConstants.Female] = GetGenderFromAverage(7 * 12);
            heights[CreatureConstants.Minotaur][GenderConstants.Male] = GetGenderFromAverage(9 * 12);
            heights[CreatureConstants.Minotaur][CreatureConstants.Minotaur] = GetCreatureFromAverage(8 * 12);
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
            heights[CreatureConstants.Pixie][GenderConstants.Female] = "10";
            heights[CreatureConstants.Pixie][GenderConstants.Male] = "10";
            heights[CreatureConstants.Pixie][CreatureConstants.Pixie] = RollHelper.GetRollWithMostEvenDistribution(10, 12, 30);
            heights[CreatureConstants.Pixie_WithIrresistibleDance][GenderConstants.Female] = "10";
            heights[CreatureConstants.Pixie_WithIrresistibleDance][GenderConstants.Male] = "10";
            heights[CreatureConstants.Pixie_WithIrresistibleDance][CreatureConstants.Pixie_WithIrresistibleDance] = RollHelper.GetRollWithMostEvenDistribution(10, 12, 30);
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
            heights[CreatureConstants.Wolf][GenderConstants.Female] = "39"; //Medium Animal
            heights[CreatureConstants.Wolf][GenderConstants.Male] = "39";
            heights[CreatureConstants.Wolf][CreatureConstants.Wolf] = "2d12";

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
        [TestCase(CreatureConstants.AnimatedObject_Anvil_Colossal, GenderConstants.Agender, 36 * 12)]
        [TestCase(CreatureConstants.AnimatedObject_Anvil_Gargantuan, GenderConstants.Agender, 18 * 12)]
        [TestCase(CreatureConstants.AnimatedObject_Anvil_Huge, GenderConstants.Agender, 9 * 12)]
        [TestCase(CreatureConstants.AnimatedObject_Anvil_Large, GenderConstants.Agender, (9 * 12 + 6) / 2)]
        [TestCase(CreatureConstants.AnimatedObject_Anvil_Tiny, GenderConstants.Agender, 10)]
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

            Assert.That(baseHeight + multiplierMin, Is.EqualTo(average * 0.9).Within(1), "Min (-10%)");
            Assert.That(baseHeight + multiplierAvg, Is.EqualTo(average).Within(1), "Average");
            Assert.That(baseHeight + multiplierMax, Is.EqualTo(average * 1.1).Within(1), "Max (+10%)");
        }

        [TestCase(CreatureConstants.Angel_AstralDeva, GenderConstants.Male, 7 * 12, 7 * 12 + 6)]
        [TestCase(CreatureConstants.Angel_AstralDeva, GenderConstants.Female, 7 * 12, 7 * 12 + 6)]
        [TestCase(CreatureConstants.Angel_Planetar, GenderConstants.Male, 8 * 12, 9 * 12)]
        [TestCase(CreatureConstants.Angel_Planetar, GenderConstants.Female, 8 * 12, 9 * 12)]
        [TestCase(CreatureConstants.Angel_Solar, GenderConstants.Male, 9 * 12, 10 * 12)]
        [TestCase(CreatureConstants.Angel_Solar, GenderConstants.Female, 9 * 12, 10 * 12)]
        [TestCase(CreatureConstants.AnimatedObject_Chair_Medium, GenderConstants.Agender, 18 + 16, 40)]
        [TestCase(CreatureConstants.AnimatedObject_Sled_Medium, GenderConstants.Agender, 115, 135)]
        [TestCase(CreatureConstants.AnimatedObject_Stool_Medium, GenderConstants.Agender, 18, 20)]
        [TestCase(CreatureConstants.AnimatedObject_Statue_Animal_Colossal, GenderConstants.Agender, 64 * 12, 128 * 12)]
        [TestCase(CreatureConstants.AnimatedObject_Statue_Animal_Gargantuan, GenderConstants.Agender, 32 * 12, 64 * 12 - 1)]
        [TestCase(CreatureConstants.AnimatedObject_Statue_Animal_Huge, GenderConstants.Agender, 16 * 12, 32 * 12 - 1)]
        [TestCase(CreatureConstants.AnimatedObject_Statue_Animal_Large, GenderConstants.Agender, 8 * 12, 16 * 12 - 1)]
        [TestCase(CreatureConstants.AnimatedObject_Statue_Animal_Medium, GenderConstants.Agender, 4 * 12, 8 * 12 - 1)]
        [TestCase(CreatureConstants.AnimatedObject_Statue_Animal_Small, GenderConstants.Agender, 2 * 12, 4 * 12 - 1)]
        [TestCase(CreatureConstants.AnimatedObject_Statue_Animal_Tiny, GenderConstants.Agender, 12, 23)]
        [TestCase(CreatureConstants.AnimatedObject_Statue_Humanoid_Colossal, GenderConstants.Agender, 64 * 12, 128 * 12)]
        [TestCase(CreatureConstants.AnimatedObject_Statue_Humanoid_Gargantuan, GenderConstants.Agender, 32 * 12, 64 * 12 - 1)]
        [TestCase(CreatureConstants.AnimatedObject_Statue_Humanoid_Huge, GenderConstants.Agender, 16 * 12, 32 * 12 - 1)]
        [TestCase(CreatureConstants.AnimatedObject_Statue_Humanoid_Large, GenderConstants.Agender, 8 * 12, 16 * 12 - 1)]
        [TestCase(CreatureConstants.AnimatedObject_Statue_Humanoid_Medium, GenderConstants.Agender, 4 * 12, 8 * 12 - 1)]
        [TestCase(CreatureConstants.AnimatedObject_Statue_Humanoid_Small, GenderConstants.Agender, 2 * 12, 4 * 12 - 1)]
        [TestCase(CreatureConstants.AnimatedObject_Statue_Humanoid_Tiny, GenderConstants.Agender, 12, 23)]
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
        [TestCase(CreatureConstants.Ogre, GenderConstants.Male, 10 * 12 + 1, 10 * 12 + 10)]
        [TestCase(CreatureConstants.Ogre, GenderConstants.Female, 9 * 12 + 3, 10 * 12)]
        [TestCase(CreatureConstants.Pixie, GenderConstants.Male, 12, 30)]
        [TestCase(CreatureConstants.Pixie, GenderConstants.Female, 12, 30)]
        [TestCase(CreatureConstants.Wolf, GenderConstants.Male, 41, 63)]
        [TestCase(CreatureConstants.Wolf, GenderConstants.Female, 41, 63)]
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