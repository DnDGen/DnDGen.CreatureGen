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

        private static readonly Dictionary<string, Dictionary<string, string>> heights = HeightsTests.GetCreatureHeights();
        private static readonly Dictionary<string, Dictionary<string, string>> lengths = LengthsTests.GetCreatureLengths();

        [SetUp]
        public void Setup()
        {
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
            Assert.That(typesAndRolls.Keys, Is.EquivalentTo(genders.Union(new[] { name })).And.Not.Empty, $"TEST DATA: {name}");

            foreach (var roll in typesAndRolls.Values)
            {
                var isValid = dice.Roll(roll).IsValid();
                Assert.That(isValid, Is.True, roll);
            }

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

            //Source: http://people.wku.edu/charles.plemons/ad&d/races/height.html 5d10 = Mountain Dwarf, so 2d6
            weights[CreatureConstants.Aasimar][GenderConstants.Female] = "90";
            weights[CreatureConstants.Aasimar][GenderConstants.Male] = "140";
            weights[CreatureConstants.Aasimar][CreatureConstants.Aasimar] = "2d6";
            //Source: https://forgottenrealms.fandom.com/wiki/Aboleth
            weights[CreatureConstants.Aboleth][GenderConstants.Hermaphrodite] = GetBaseFromAverage(CreatureConstants.Aboleth, 6500);
            weights[CreatureConstants.Aboleth][CreatureConstants.Aboleth] = GetMultiplierFromAverage(CreatureConstants.Aboleth, 6500);
            //Source: https://www.d20srd.org/srd/monsters/achaierai.htm
            weights[CreatureConstants.Achaierai][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Achaierai, 750);
            weights[CreatureConstants.Achaierai][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Achaierai, 750);
            weights[CreatureConstants.Achaierai][CreatureConstants.Achaierai] = GetMultiplierFromAverage(CreatureConstants.Achaierai, 750);
            //Incorporeal, so weight is 0
            weights[CreatureConstants.Allip][GenderConstants.Female] = "0";
            weights[CreatureConstants.Allip][GenderConstants.Male] = "0";
            weights[CreatureConstants.Allip][CreatureConstants.Allip] = "0";
            //Source: https://www.mojobob.com/roleplay/monstrousmanual/s/sphinx.html
            weights[CreatureConstants.Androsphinx][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Androsphinx, 800);
            weights[CreatureConstants.Androsphinx][CreatureConstants.Androsphinx] = GetMultiplierFromAverage(CreatureConstants.Androsphinx, 800);
            //Source: https://forgottenrealms.fandom.com/wiki/Astral_Deva
            weights[CreatureConstants.Angel_AstralDeva][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Angel_AstralDeva, 250);
            weights[CreatureConstants.Angel_AstralDeva][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Angel_AstralDeva, 250);
            weights[CreatureConstants.Angel_AstralDeva][CreatureConstants.Angel_AstralDeva] = GetMultiplierFromAverage(CreatureConstants.Angel_AstralDeva, 250);
            //Source: https://forgottenrealms.fandom.com/wiki/Planetar
            weights[CreatureConstants.Angel_Planetar][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Angel_Planetar, 500);
            weights[CreatureConstants.Angel_Planetar][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Angel_Planetar, 500);
            weights[CreatureConstants.Angel_Planetar][CreatureConstants.Angel_Planetar] = GetMultiplierFromAverage(CreatureConstants.Angel_Planetar, 500);
            //Source: https://forgottenrealms.fandom.com/wiki/Solar
            weights[CreatureConstants.Angel_Solar][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Angel_Solar, 500);
            weights[CreatureConstants.Angel_Solar][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Angel_Solar, 500);
            weights[CreatureConstants.Angel_Solar][CreatureConstants.Angel_Solar] = GetMultiplierFromAverage(CreatureConstants.Angel_Solar, 500);
            //Source: https://www.d20srd.org/srd/combat/movementPositionAndDistance.htm
            weights[CreatureConstants.AnimatedObject_Colossal][GenderConstants.Agender] =
                GetBaseFromRange(CreatureConstants.AnimatedObject_Colossal, 125 * 2000, 1000 * 2000);
            weights[CreatureConstants.AnimatedObject_Colossal][CreatureConstants.AnimatedObject_Colossal] =
                GetMultiplierFromRange(CreatureConstants.AnimatedObject_Colossal, 125 * 2000, 1000 * 2000);
            weights[CreatureConstants.AnimatedObject_Colossal_Flexible][GenderConstants.Agender] =
                GetBaseFromRange(CreatureConstants.AnimatedObject_Colossal_Flexible, 125 * 2000, 1000 * 2000);
            weights[CreatureConstants.AnimatedObject_Colossal_Flexible][CreatureConstants.AnimatedObject_Colossal_Flexible] =
                GetMultiplierFromRange(CreatureConstants.AnimatedObject_Colossal_Flexible, 125 * 2000, 1000 * 2000);
            weights[CreatureConstants.AnimatedObject_Colossal_MultipleLegs][GenderConstants.Agender] =
                GetBaseFromRange(CreatureConstants.AnimatedObject_Colossal_MultipleLegs, 125 * 2000, 1000 * 2000);
            weights[CreatureConstants.AnimatedObject_Colossal_MultipleLegs][CreatureConstants.AnimatedObject_Colossal_MultipleLegs] =
                GetMultiplierFromRange(CreatureConstants.AnimatedObject_Colossal_MultipleLegs, 125 * 2000, 1000 * 2000);
            weights[CreatureConstants.AnimatedObject_Colossal_MultipleLegs_Wooden][GenderConstants.Agender] =
                GetBaseFromRange(CreatureConstants.AnimatedObject_Colossal_MultipleLegs_Wooden, 125 * 2000, 1000 * 2000);
            weights[CreatureConstants.AnimatedObject_Colossal_MultipleLegs_Wooden][CreatureConstants.AnimatedObject_Colossal_MultipleLegs_Wooden] =
                GetMultiplierFromRange(CreatureConstants.AnimatedObject_Colossal_MultipleLegs_Wooden, 125 * 2000, 1000 * 2000);
            weights[CreatureConstants.AnimatedObject_Colossal_Sheetlike][GenderConstants.Agender] =
                GetBaseFromRange(CreatureConstants.AnimatedObject_Colossal_Sheetlike, 125 * 2000, 1000 * 2000);
            weights[CreatureConstants.AnimatedObject_Colossal_Sheetlike][CreatureConstants.AnimatedObject_Colossal_Sheetlike] =
                GetMultiplierFromRange(CreatureConstants.AnimatedObject_Colossal_Sheetlike, 125 * 2000, 1000 * 2000);
            weights[CreatureConstants.AnimatedObject_Colossal_TwoLegs][GenderConstants.Agender] =
                GetBaseFromRange(CreatureConstants.AnimatedObject_Colossal_TwoLegs, 125 * 2000, 1000 * 2000);
            weights[CreatureConstants.AnimatedObject_Colossal_TwoLegs][CreatureConstants.AnimatedObject_Colossal_TwoLegs] =
                GetMultiplierFromRange(CreatureConstants.AnimatedObject_Colossal_TwoLegs, 125 * 2000, 1000 * 2000);
            weights[CreatureConstants.AnimatedObject_Colossal_TwoLegs_Wooden][GenderConstants.Agender] =
                GetBaseFromRange(CreatureConstants.AnimatedObject_Colossal_TwoLegs_Wooden, 125 * 2000, 1000 * 2000);
            weights[CreatureConstants.AnimatedObject_Colossal_TwoLegs_Wooden][CreatureConstants.AnimatedObject_Colossal_TwoLegs_Wooden] =
                GetMultiplierFromRange(CreatureConstants.AnimatedObject_Colossal_TwoLegs_Wooden, 125 * 2000, 1000 * 2000);
            weights[CreatureConstants.AnimatedObject_Colossal_Wheels_Wooden][GenderConstants.Agender] =
                GetBaseFromRange(CreatureConstants.AnimatedObject_Colossal_Wheels_Wooden, 125 * 2000, 1000 * 2000);
            weights[CreatureConstants.AnimatedObject_Colossal_Wheels_Wooden][CreatureConstants.AnimatedObject_Colossal_Wheels_Wooden] =
                GetMultiplierFromRange(CreatureConstants.AnimatedObject_Colossal_Wheels_Wooden, 125 * 2000, 1000 * 2000);
            weights[CreatureConstants.AnimatedObject_Colossal_Wooden][GenderConstants.Agender] =
                GetBaseFromRange(CreatureConstants.AnimatedObject_Colossal_Wooden, 125 * 2000, 1000 * 2000);
            weights[CreatureConstants.AnimatedObject_Colossal_Wooden][CreatureConstants.AnimatedObject_Colossal_Wooden] =
                GetMultiplierFromRange(CreatureConstants.AnimatedObject_Colossal_Wooden, 125 * 2000, 1000 * 2000);
            weights[CreatureConstants.AnimatedObject_Gargantuan][GenderConstants.Agender] =
                GetBaseFromRange(CreatureConstants.AnimatedObject_Gargantuan, 16 * 2000, 125 * 2000);
            weights[CreatureConstants.AnimatedObject_Gargantuan][CreatureConstants.AnimatedObject_Gargantuan] =
                GetMultiplierFromRange(CreatureConstants.AnimatedObject_Gargantuan, 16 * 2000, 125 * 2000);
            weights[CreatureConstants.AnimatedObject_Gargantuan_Flexible][GenderConstants.Agender] =
                GetBaseFromRange(CreatureConstants.AnimatedObject_Gargantuan_Flexible, 16 * 2000, 125 * 2000);
            weights[CreatureConstants.AnimatedObject_Gargantuan_Flexible][CreatureConstants.AnimatedObject_Gargantuan_Flexible] =
                GetMultiplierFromRange(CreatureConstants.AnimatedObject_Gargantuan_Flexible, 16 * 2000, 125 * 2000);
            weights[CreatureConstants.AnimatedObject_Gargantuan_MultipleLegs][GenderConstants.Agender] =
                GetBaseFromRange(CreatureConstants.AnimatedObject_Gargantuan_MultipleLegs, 16 * 2000, 125 * 2000);
            weights[CreatureConstants.AnimatedObject_Gargantuan_MultipleLegs][CreatureConstants.AnimatedObject_Gargantuan_MultipleLegs] =
                GetMultiplierFromRange(CreatureConstants.AnimatedObject_Gargantuan_MultipleLegs, 16 * 2000, 125 * 2000);
            weights[CreatureConstants.AnimatedObject_Gargantuan_MultipleLegs_Wooden][GenderConstants.Agender] =
                GetBaseFromRange(CreatureConstants.AnimatedObject_Gargantuan_MultipleLegs_Wooden, 16 * 2000, 125 * 2000);
            weights[CreatureConstants.AnimatedObject_Gargantuan_MultipleLegs_Wooden][CreatureConstants.AnimatedObject_Gargantuan_MultipleLegs_Wooden] =
                GetMultiplierFromRange(CreatureConstants.AnimatedObject_Gargantuan_MultipleLegs_Wooden, 16 * 2000, 125 * 2000);
            weights[CreatureConstants.AnimatedObject_Gargantuan_Sheetlike][GenderConstants.Agender] =
                GetBaseFromRange(CreatureConstants.AnimatedObject_Gargantuan_Sheetlike, 16 * 2000, 125 * 2000);
            weights[CreatureConstants.AnimatedObject_Gargantuan_Sheetlike][CreatureConstants.AnimatedObject_Gargantuan_Sheetlike] =
                GetMultiplierFromRange(CreatureConstants.AnimatedObject_Gargantuan_Sheetlike, 16 * 2000, 125 * 2000);
            weights[CreatureConstants.AnimatedObject_Gargantuan_TwoLegs][GenderConstants.Agender] =
                GetBaseFromRange(CreatureConstants.AnimatedObject_Gargantuan_TwoLegs, 16 * 2000, 125 * 2000);
            weights[CreatureConstants.AnimatedObject_Gargantuan_TwoLegs][CreatureConstants.AnimatedObject_Gargantuan_TwoLegs] =
                GetMultiplierFromRange(CreatureConstants.AnimatedObject_Gargantuan_TwoLegs, 16 * 2000, 125 * 2000);
            weights[CreatureConstants.AnimatedObject_Gargantuan_TwoLegs_Wooden][GenderConstants.Agender] =
                GetBaseFromRange(CreatureConstants.AnimatedObject_Gargantuan_TwoLegs_Wooden, 16 * 2000, 125 * 2000);
            weights[CreatureConstants.AnimatedObject_Gargantuan_TwoLegs_Wooden][CreatureConstants.AnimatedObject_Gargantuan_TwoLegs_Wooden] =
                GetMultiplierFromRange(CreatureConstants.AnimatedObject_Gargantuan_TwoLegs_Wooden, 16 * 2000, 125 * 2000);
            weights[CreatureConstants.AnimatedObject_Gargantuan_Wheels_Wooden][GenderConstants.Agender] =
                GetBaseFromRange(CreatureConstants.AnimatedObject_Gargantuan_Wheels_Wooden, 16 * 2000, 125 * 2000);
            weights[CreatureConstants.AnimatedObject_Gargantuan_Wheels_Wooden][CreatureConstants.AnimatedObject_Gargantuan_Wheels_Wooden] =
                GetMultiplierFromRange(CreatureConstants.AnimatedObject_Gargantuan_Wheels_Wooden, 16 * 2000, 125 * 2000);
            weights[CreatureConstants.AnimatedObject_Gargantuan_Wooden][GenderConstants.Agender] =
                GetBaseFromRange(CreatureConstants.AnimatedObject_Gargantuan_Wooden, 16 * 2000, 125 * 2000);
            weights[CreatureConstants.AnimatedObject_Gargantuan_Wooden][CreatureConstants.AnimatedObject_Gargantuan_Wooden] =
                GetMultiplierFromRange(CreatureConstants.AnimatedObject_Gargantuan_Wooden, 16 * 2000, 125 * 2000);
            weights[CreatureConstants.AnimatedObject_Huge][GenderConstants.Agender] =
                GetBaseFromRange(CreatureConstants.AnimatedObject_Huge, 2 * 2000, 16 * 2000);
            weights[CreatureConstants.AnimatedObject_Huge][CreatureConstants.AnimatedObject_Huge] =
                GetMultiplierFromRange(CreatureConstants.AnimatedObject_Huge, 2 * 2000, 16 * 2000);
            weights[CreatureConstants.AnimatedObject_Huge_Flexible][GenderConstants.Agender] =
                GetBaseFromRange(CreatureConstants.AnimatedObject_Huge_Flexible, 2 * 2000, 16 * 2000);
            weights[CreatureConstants.AnimatedObject_Huge_Flexible][CreatureConstants.AnimatedObject_Huge_Flexible] =
                GetMultiplierFromRange(CreatureConstants.AnimatedObject_Huge_Flexible, 2 * 2000, 16 * 2000);
            weights[CreatureConstants.AnimatedObject_Huge_MultipleLegs][GenderConstants.Agender] =
                GetBaseFromRange(CreatureConstants.AnimatedObject_Huge_MultipleLegs, 2 * 2000, 16 * 2000);
            weights[CreatureConstants.AnimatedObject_Huge_MultipleLegs][CreatureConstants.AnimatedObject_Huge_MultipleLegs] =
                GetMultiplierFromRange(CreatureConstants.AnimatedObject_Huge_MultipleLegs, 2 * 2000, 16 * 2000);
            weights[CreatureConstants.AnimatedObject_Huge_MultipleLegs_Wooden][GenderConstants.Agender] =
                GetBaseFromRange(CreatureConstants.AnimatedObject_Huge_MultipleLegs_Wooden, 2 * 2000, 16 * 2000);
            weights[CreatureConstants.AnimatedObject_Huge_MultipleLegs_Wooden][CreatureConstants.AnimatedObject_Huge_MultipleLegs_Wooden] =
                GetMultiplierFromRange(CreatureConstants.AnimatedObject_Huge_MultipleLegs_Wooden, 2 * 2000, 16 * 2000);
            weights[CreatureConstants.AnimatedObject_Huge_Sheetlike][GenderConstants.Agender] =
                GetBaseFromRange(CreatureConstants.AnimatedObject_Huge_Sheetlike, 2 * 2000, 16 * 2000);
            weights[CreatureConstants.AnimatedObject_Huge_Sheetlike][CreatureConstants.AnimatedObject_Huge_Sheetlike] =
                GetMultiplierFromRange(CreatureConstants.AnimatedObject_Huge_Sheetlike, 2 * 2000, 16 * 2000);
            weights[CreatureConstants.AnimatedObject_Huge_TwoLegs][GenderConstants.Agender] =
                GetBaseFromRange(CreatureConstants.AnimatedObject_Huge_TwoLegs, 2 * 2000, 16 * 2000);
            weights[CreatureConstants.AnimatedObject_Huge_TwoLegs][CreatureConstants.AnimatedObject_Huge_TwoLegs] =
                GetMultiplierFromRange(CreatureConstants.AnimatedObject_Huge_TwoLegs, 2 * 2000, 16 * 2000);
            weights[CreatureConstants.AnimatedObject_Huge_TwoLegs_Wooden][GenderConstants.Agender] =
                GetBaseFromRange(CreatureConstants.AnimatedObject_Huge_TwoLegs_Wooden, 2 * 2000, 16 * 2000);
            weights[CreatureConstants.AnimatedObject_Huge_TwoLegs_Wooden][CreatureConstants.AnimatedObject_Huge_TwoLegs_Wooden] =
                GetMultiplierFromRange(CreatureConstants.AnimatedObject_Huge_TwoLegs_Wooden, 2 * 2000, 16 * 2000);
            weights[CreatureConstants.AnimatedObject_Huge_Wheels_Wooden][GenderConstants.Agender] =
                GetBaseFromRange(CreatureConstants.AnimatedObject_Huge_Wheels_Wooden, 2 * 2000, 16 * 2000);
            weights[CreatureConstants.AnimatedObject_Huge_Wheels_Wooden][CreatureConstants.AnimatedObject_Huge_Wheels_Wooden] =
                GetMultiplierFromRange(CreatureConstants.AnimatedObject_Huge_Wheels_Wooden, 2 * 2000, 16 * 2000);
            weights[CreatureConstants.AnimatedObject_Huge_Wooden][GenderConstants.Agender] =
                GetBaseFromRange(CreatureConstants.AnimatedObject_Huge_Wooden, 2 * 2000, 16 * 2000);
            weights[CreatureConstants.AnimatedObject_Huge_Wooden][CreatureConstants.AnimatedObject_Huge_Wooden] =
                GetMultiplierFromRange(CreatureConstants.AnimatedObject_Huge_Wooden, 2 * 2000, 16 * 2000);
            weights[CreatureConstants.AnimatedObject_Large][GenderConstants.Agender] =
                GetBaseFromRange(CreatureConstants.AnimatedObject_Large, 500, 2 * 2000);
            weights[CreatureConstants.AnimatedObject_Large][CreatureConstants.AnimatedObject_Large] =
                GetMultiplierFromRange(CreatureConstants.AnimatedObject_Large, 500, 2 * 2000);
            weights[CreatureConstants.AnimatedObject_Large_Flexible][GenderConstants.Agender] =
                GetBaseFromRange(CreatureConstants.AnimatedObject_Large_Flexible, 500, 2 * 2000);
            weights[CreatureConstants.AnimatedObject_Large_Flexible][CreatureConstants.AnimatedObject_Large_Flexible] =
                GetMultiplierFromRange(CreatureConstants.AnimatedObject_Large_Flexible, 500, 2 * 2000);
            weights[CreatureConstants.AnimatedObject_Large_MultipleLegs][GenderConstants.Agender] =
                GetBaseFromRange(CreatureConstants.AnimatedObject_Large_MultipleLegs, 500, 2 * 2000);
            weights[CreatureConstants.AnimatedObject_Large_MultipleLegs][CreatureConstants.AnimatedObject_Large_MultipleLegs] =
                GetMultiplierFromRange(CreatureConstants.AnimatedObject_Large_MultipleLegs, 500, 2 * 2000);
            weights[CreatureConstants.AnimatedObject_Large_MultipleLegs_Wooden][GenderConstants.Agender] =
                GetBaseFromRange(CreatureConstants.AnimatedObject_Large_MultipleLegs_Wooden, 500, 2 * 2000);
            weights[CreatureConstants.AnimatedObject_Large_MultipleLegs_Wooden][CreatureConstants.AnimatedObject_Large_MultipleLegs_Wooden] =
                GetMultiplierFromRange(CreatureConstants.AnimatedObject_Large_MultipleLegs_Wooden, 500, 2 * 2000);
            weights[CreatureConstants.AnimatedObject_Large_Sheetlike][GenderConstants.Agender] =
                GetBaseFromRange(CreatureConstants.AnimatedObject_Large_Sheetlike, 500, 2 * 2000);
            weights[CreatureConstants.AnimatedObject_Large_Sheetlike][CreatureConstants.AnimatedObject_Large_Sheetlike] =
                GetMultiplierFromRange(CreatureConstants.AnimatedObject_Large_Sheetlike, 500, 2 * 2000);
            weights[CreatureConstants.AnimatedObject_Large_TwoLegs][GenderConstants.Agender] =
                GetBaseFromRange(CreatureConstants.AnimatedObject_Large_TwoLegs, 500, 2 * 2000);
            weights[CreatureConstants.AnimatedObject_Large_TwoLegs][CreatureConstants.AnimatedObject_Large_TwoLegs] =
                GetMultiplierFromRange(CreatureConstants.AnimatedObject_Large_TwoLegs, 500, 2 * 2000);
            weights[CreatureConstants.AnimatedObject_Large_TwoLegs_Wooden][GenderConstants.Agender] =
                GetBaseFromRange(CreatureConstants.AnimatedObject_Large_TwoLegs_Wooden, 500, 2 * 2000);
            weights[CreatureConstants.AnimatedObject_Large_TwoLegs_Wooden][CreatureConstants.AnimatedObject_Large_TwoLegs_Wooden] =
                GetMultiplierFromRange(CreatureConstants.AnimatedObject_Large_TwoLegs_Wooden, 500, 2 * 2000);
            weights[CreatureConstants.AnimatedObject_Large_Wheels_Wooden][GenderConstants.Agender] =
                GetBaseFromRange(CreatureConstants.AnimatedObject_Large_Wheels_Wooden, 500, 2 * 2000);
            weights[CreatureConstants.AnimatedObject_Large_Wheels_Wooden][CreatureConstants.AnimatedObject_Large_Wheels_Wooden] =
                GetMultiplierFromRange(CreatureConstants.AnimatedObject_Large_Wheels_Wooden, 500, 2 * 2000);
            weights[CreatureConstants.AnimatedObject_Large_Wooden][GenderConstants.Agender] =
                GetBaseFromRange(CreatureConstants.AnimatedObject_Large_Wooden, 500, 2 * 2000);
            weights[CreatureConstants.AnimatedObject_Large_Wooden][CreatureConstants.AnimatedObject_Large_Wooden] =
                GetMultiplierFromRange(CreatureConstants.AnimatedObject_Large_Wooden, 500, 2 * 2000);
            weights[CreatureConstants.AnimatedObject_Medium][GenderConstants.Agender] =
                GetBaseFromRange(CreatureConstants.AnimatedObject_Medium, 60, 500);
            weights[CreatureConstants.AnimatedObject_Medium][CreatureConstants.AnimatedObject_Medium] =
                GetMultiplierFromRange(CreatureConstants.AnimatedObject_Medium, 60, 500);
            weights[CreatureConstants.AnimatedObject_Medium_Flexible][GenderConstants.Agender] =
                GetBaseFromRange(CreatureConstants.AnimatedObject_Medium_Flexible, 60, 500);
            weights[CreatureConstants.AnimatedObject_Medium_Flexible][CreatureConstants.AnimatedObject_Medium_Flexible] =
                GetMultiplierFromRange(CreatureConstants.AnimatedObject_Medium_Flexible, 60, 500);
            weights[CreatureConstants.AnimatedObject_Medium_MultipleLegs][GenderConstants.Agender] =
                GetBaseFromRange(CreatureConstants.AnimatedObject_Medium_MultipleLegs, 60, 500);
            weights[CreatureConstants.AnimatedObject_Medium_MultipleLegs][CreatureConstants.AnimatedObject_Medium_MultipleLegs] =
                GetMultiplierFromRange(CreatureConstants.AnimatedObject_Medium_MultipleLegs, 60, 500);
            weights[CreatureConstants.AnimatedObject_Medium_MultipleLegs_Wooden][GenderConstants.Agender] =
                GetBaseFromRange(CreatureConstants.AnimatedObject_Medium_MultipleLegs_Wooden, 60, 500);
            weights[CreatureConstants.AnimatedObject_Medium_MultipleLegs_Wooden][CreatureConstants.AnimatedObject_Medium_MultipleLegs_Wooden] =
                GetMultiplierFromRange(CreatureConstants.AnimatedObject_Medium_MultipleLegs_Wooden, 60, 500);
            weights[CreatureConstants.AnimatedObject_Medium_Sheetlike][GenderConstants.Agender] =
                GetBaseFromRange(CreatureConstants.AnimatedObject_Medium_Sheetlike, 60, 500);
            weights[CreatureConstants.AnimatedObject_Medium_Sheetlike][CreatureConstants.AnimatedObject_Medium_Sheetlike] =
                GetMultiplierFromRange(CreatureConstants.AnimatedObject_Medium_Sheetlike, 60, 500);
            weights[CreatureConstants.AnimatedObject_Medium_TwoLegs][GenderConstants.Agender] =
                GetBaseFromRange(CreatureConstants.AnimatedObject_Medium_TwoLegs, 60, 500);
            weights[CreatureConstants.AnimatedObject_Medium_TwoLegs][CreatureConstants.AnimatedObject_Medium_TwoLegs] =
                GetMultiplierFromRange(CreatureConstants.AnimatedObject_Medium_TwoLegs, 60, 500);
            weights[CreatureConstants.AnimatedObject_Medium_TwoLegs_Wooden][GenderConstants.Agender] =
                GetBaseFromRange(CreatureConstants.AnimatedObject_Medium_TwoLegs_Wooden, 60, 500);
            weights[CreatureConstants.AnimatedObject_Medium_TwoLegs_Wooden][CreatureConstants.AnimatedObject_Medium_TwoLegs_Wooden] =
                GetMultiplierFromRange(CreatureConstants.AnimatedObject_Medium_TwoLegs_Wooden, 60, 500);
            weights[CreatureConstants.AnimatedObject_Medium_Wheels_Wooden][GenderConstants.Agender] =
                GetBaseFromRange(CreatureConstants.AnimatedObject_Medium_Wheels_Wooden, 60, 500);
            weights[CreatureConstants.AnimatedObject_Medium_Wheels_Wooden][CreatureConstants.AnimatedObject_Medium_Wheels_Wooden] =
                GetMultiplierFromRange(CreatureConstants.AnimatedObject_Medium_Wheels_Wooden, 60, 500);
            weights[CreatureConstants.AnimatedObject_Medium_Wooden][GenderConstants.Agender] =
                GetBaseFromRange(CreatureConstants.AnimatedObject_Medium_Wooden, 60, 500);
            weights[CreatureConstants.AnimatedObject_Medium_Wooden][CreatureConstants.AnimatedObject_Medium_Wooden] =
                GetMultiplierFromRange(CreatureConstants.AnimatedObject_Medium_Wooden, 60, 500);
            weights[CreatureConstants.AnimatedObject_Small][GenderConstants.Agender] =
                GetBaseFromRange(CreatureConstants.AnimatedObject_Small, 8, 60);
            weights[CreatureConstants.AnimatedObject_Small][CreatureConstants.AnimatedObject_Small] =
                GetMultiplierFromRange(CreatureConstants.AnimatedObject_Small, 8, 60);
            weights[CreatureConstants.AnimatedObject_Small_Flexible][GenderConstants.Agender] =
                GetBaseFromRange(CreatureConstants.AnimatedObject_Small_Flexible, 8, 60);
            weights[CreatureConstants.AnimatedObject_Small_Flexible][CreatureConstants.AnimatedObject_Small_Flexible] =
                GetMultiplierFromRange(CreatureConstants.AnimatedObject_Small_Flexible, 8, 60);
            weights[CreatureConstants.AnimatedObject_Small_MultipleLegs][GenderConstants.Agender] =
                GetBaseFromRange(CreatureConstants.AnimatedObject_Small_MultipleLegs, 8, 60);
            weights[CreatureConstants.AnimatedObject_Small_MultipleLegs][CreatureConstants.AnimatedObject_Small_MultipleLegs] =
                GetMultiplierFromRange(CreatureConstants.AnimatedObject_Small_MultipleLegs, 8, 60);
            weights[CreatureConstants.AnimatedObject_Small_MultipleLegs_Wooden][GenderConstants.Agender] =
                GetBaseFromRange(CreatureConstants.AnimatedObject_Small_MultipleLegs_Wooden, 8, 60);
            weights[CreatureConstants.AnimatedObject_Small_MultipleLegs_Wooden][CreatureConstants.AnimatedObject_Small_MultipleLegs_Wooden] =
                GetMultiplierFromRange(CreatureConstants.AnimatedObject_Small_MultipleLegs_Wooden, 8, 60);
            weights[CreatureConstants.AnimatedObject_Small_Sheetlike][GenderConstants.Agender] =
                GetBaseFromRange(CreatureConstants.AnimatedObject_Small_Sheetlike, 8, 60);
            weights[CreatureConstants.AnimatedObject_Small_Sheetlike][CreatureConstants.AnimatedObject_Small_Sheetlike] =
                GetMultiplierFromRange(CreatureConstants.AnimatedObject_Small_Sheetlike, 8, 60);
            weights[CreatureConstants.AnimatedObject_Small_TwoLegs][GenderConstants.Agender] =
                GetBaseFromRange(CreatureConstants.AnimatedObject_Small_TwoLegs, 8, 60);
            weights[CreatureConstants.AnimatedObject_Small_TwoLegs][CreatureConstants.AnimatedObject_Small_TwoLegs] =
                GetMultiplierFromRange(CreatureConstants.AnimatedObject_Small_TwoLegs, 8, 60);
            weights[CreatureConstants.AnimatedObject_Small_TwoLegs_Wooden][GenderConstants.Agender] =
                GetBaseFromRange(CreatureConstants.AnimatedObject_Small_TwoLegs_Wooden, 8, 60);
            weights[CreatureConstants.AnimatedObject_Small_TwoLegs_Wooden][CreatureConstants.AnimatedObject_Small_TwoLegs_Wooden] =
                GetMultiplierFromRange(CreatureConstants.AnimatedObject_Small_TwoLegs_Wooden, 8, 60);
            weights[CreatureConstants.AnimatedObject_Small_Wheels_Wooden][GenderConstants.Agender] =
                GetBaseFromRange(CreatureConstants.AnimatedObject_Small_Wheels_Wooden, 8, 60);
            weights[CreatureConstants.AnimatedObject_Small_Wheels_Wooden][CreatureConstants.AnimatedObject_Small_Wheels_Wooden] =
                GetMultiplierFromRange(CreatureConstants.AnimatedObject_Small_Wheels_Wooden, 8, 60);
            weights[CreatureConstants.AnimatedObject_Small_Wooden][GenderConstants.Agender] =
                GetBaseFromRange(CreatureConstants.AnimatedObject_Small_Wooden, 8, 60);
            weights[CreatureConstants.AnimatedObject_Small_Wooden][CreatureConstants.AnimatedObject_Small_Wooden] =
                GetMultiplierFromRange(CreatureConstants.AnimatedObject_Small_Wooden, 8, 60);
            weights[CreatureConstants.AnimatedObject_Tiny][GenderConstants.Agender] =
                GetBaseFromRange(CreatureConstants.AnimatedObject_Tiny, 1, 8);
            weights[CreatureConstants.AnimatedObject_Tiny][CreatureConstants.AnimatedObject_Tiny] =
                GetMultiplierFromRange(CreatureConstants.AnimatedObject_Tiny, 1, 8);
            weights[CreatureConstants.AnimatedObject_Tiny_Flexible][GenderConstants.Agender] =
                GetBaseFromRange(CreatureConstants.AnimatedObject_Tiny_Flexible, 1, 8);
            weights[CreatureConstants.AnimatedObject_Tiny_Flexible][CreatureConstants.AnimatedObject_Tiny_Flexible] =
                GetMultiplierFromRange(CreatureConstants.AnimatedObject_Tiny_Flexible, 1, 8);
            weights[CreatureConstants.AnimatedObject_Tiny_MultipleLegs][GenderConstants.Agender] =
                GetBaseFromRange(CreatureConstants.AnimatedObject_Tiny_MultipleLegs, 1, 8);
            weights[CreatureConstants.AnimatedObject_Tiny_MultipleLegs][CreatureConstants.AnimatedObject_Tiny_MultipleLegs] =
                GetMultiplierFromRange(CreatureConstants.AnimatedObject_Tiny_MultipleLegs, 1, 8);
            weights[CreatureConstants.AnimatedObject_Tiny_MultipleLegs_Wooden][GenderConstants.Agender] =
                GetBaseFromRange(CreatureConstants.AnimatedObject_Tiny_MultipleLegs_Wooden, 1, 8);
            weights[CreatureConstants.AnimatedObject_Tiny_MultipleLegs_Wooden][CreatureConstants.AnimatedObject_Tiny_MultipleLegs_Wooden] =
                GetMultiplierFromRange(CreatureConstants.AnimatedObject_Tiny_MultipleLegs_Wooden, 1, 8);
            weights[CreatureConstants.AnimatedObject_Tiny_Sheetlike][GenderConstants.Agender] =
                GetBaseFromRange(CreatureConstants.AnimatedObject_Tiny_Sheetlike, 1, 8);
            weights[CreatureConstants.AnimatedObject_Tiny_Sheetlike][CreatureConstants.AnimatedObject_Tiny_Sheetlike] =
                GetMultiplierFromRange(CreatureConstants.AnimatedObject_Tiny_Sheetlike, 1, 8);
            weights[CreatureConstants.AnimatedObject_Tiny_TwoLegs][GenderConstants.Agender] =
                GetBaseFromRange(CreatureConstants.AnimatedObject_Tiny_TwoLegs, 1, 8);
            weights[CreatureConstants.AnimatedObject_Tiny_TwoLegs][CreatureConstants.AnimatedObject_Tiny_TwoLegs] =
                GetMultiplierFromRange(CreatureConstants.AnimatedObject_Tiny_TwoLegs, 1, 8);
            weights[CreatureConstants.AnimatedObject_Tiny_TwoLegs_Wooden][GenderConstants.Agender] =
                GetBaseFromRange(CreatureConstants.AnimatedObject_Tiny_TwoLegs_Wooden, 1, 8);
            weights[CreatureConstants.AnimatedObject_Tiny_TwoLegs_Wooden][CreatureConstants.AnimatedObject_Tiny_TwoLegs_Wooden] =
                GetMultiplierFromRange(CreatureConstants.AnimatedObject_Tiny_TwoLegs_Wooden, 1, 8);
            weights[CreatureConstants.AnimatedObject_Tiny_Wheels_Wooden][GenderConstants.Agender] =
                GetBaseFromRange(CreatureConstants.AnimatedObject_Tiny_Wheels_Wooden, 1, 8);
            weights[CreatureConstants.AnimatedObject_Tiny_Wheels_Wooden][CreatureConstants.AnimatedObject_Tiny_Wheels_Wooden] =
                GetMultiplierFromRange(CreatureConstants.AnimatedObject_Tiny_Wheels_Wooden, 1, 8);
            weights[CreatureConstants.AnimatedObject_Tiny_Wooden][GenderConstants.Agender] =
                GetBaseFromRange(CreatureConstants.AnimatedObject_Tiny_Wooden, 1, 8);
            weights[CreatureConstants.AnimatedObject_Tiny_Wooden][CreatureConstants.AnimatedObject_Tiny_Wooden] =
                GetMultiplierFromRange(CreatureConstants.AnimatedObject_Tiny_Wooden, 1, 8);
            //Source: https://www.d20srd.org/srd/monsters/ankheg.htm
            weights[CreatureConstants.Ankheg][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Ankheg, 800);
            weights[CreatureConstants.Ankheg][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Ankheg, 800);
            weights[CreatureConstants.Ankheg][CreatureConstants.Ankheg] = GetMultiplierFromAverage(CreatureConstants.Ankheg, 800);
            //Source: https://www.d20srd.org/srd/monsters/hag.htm#annis
            weights[CreatureConstants.Annis][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Annis, 325);
            weights[CreatureConstants.Annis][CreatureConstants.Annis] = GetMultiplierFromAverage(CreatureConstants.Annis, 325);
            //Source: https://www.dimensions.com/element/eastern-lowland-gorilla-gorilla-beringei-graueri
            //https://www.d20srd.org/srd/monsters/ape.htm (male)
            weights[CreatureConstants.Ape][GenderConstants.Female] = GetBaseFromRange(CreatureConstants.Ape, 220, 460);
            weights[CreatureConstants.Ape][GenderConstants.Male] = GetBaseFromRange(CreatureConstants.Ape, 300, 400);
            weights[CreatureConstants.Ape][CreatureConstants.Ape] = GetMultiplierFromRange(CreatureConstants.Ape, 220, 460);
            //Source: https://www.d20srd.org/srd/monsters/direApe.htm
            weights[CreatureConstants.Ape_Dire][GenderConstants.Female] = GetBaseFromRange(CreatureConstants.Ape, 800, 1200);
            weights[CreatureConstants.Ape_Dire][GenderConstants.Male] = GetBaseFromRange(CreatureConstants.Ape, 800, 1200);
            weights[CreatureConstants.Ape_Dire][CreatureConstants.Ape_Dire] = GetMultiplierFromRange(CreatureConstants.Ape, 800, 1200);
            //INFO: Based on Half-Elf, since could be Human, Half-Elf, or Drow
            weights[CreatureConstants.Aranea][GenderConstants.Female] = "4*12+5";
            weights[CreatureConstants.Aranea][GenderConstants.Male] = "4*12+7";
            weights[CreatureConstants.Aranea][CreatureConstants.Aranea] = "2d8";
            //Source: https://www.d20srd.org/srd/monsters/arrowhawk.htm
            weights[CreatureConstants.Arrowhawk_Juvenile][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Arrowhawk_Juvenile, 20);
            weights[CreatureConstants.Arrowhawk_Juvenile][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Arrowhawk_Juvenile, 20);
            weights[CreatureConstants.Arrowhawk_Juvenile][CreatureConstants.Arrowhawk_Juvenile] = GetMultiplierFromAverage(CreatureConstants.Arrowhawk_Juvenile, 20);
            weights[CreatureConstants.Arrowhawk_Adult][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Arrowhawk_Adult, 100);
            weights[CreatureConstants.Arrowhawk_Adult][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Arrowhawk_Adult, 100);
            weights[CreatureConstants.Arrowhawk_Adult][CreatureConstants.Arrowhawk_Adult] = GetMultiplierFromAverage(CreatureConstants.Arrowhawk_Adult, 100);
            weights[CreatureConstants.Arrowhawk_Elder][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Arrowhawk_Elder, 800);
            weights[CreatureConstants.Arrowhawk_Elder][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Arrowhawk_Elder, 800);
            weights[CreatureConstants.Arrowhawk_Elder][CreatureConstants.Arrowhawk_Elder] = GetMultiplierFromAverage(CreatureConstants.Arrowhawk_Elder, 800);
            //Source: https://www.epicpath.org/index.php/Assassin_Vine
            weights[CreatureConstants.AssassinVine][GenderConstants.Agender] = GetBaseFromAverage(CreatureConstants.AssassinVine, 50);
            weights[CreatureConstants.AssassinVine][CreatureConstants.AssassinVine] = GetMultiplierFromAverage(CreatureConstants.AssassinVine, 50);
            //Source: https://www.d20srd.org/srd/monsters/athach.htm
            weights[CreatureConstants.Athach][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Athach, 4500);
            weights[CreatureConstants.Athach][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Athach, 4500);
            weights[CreatureConstants.Athach][CreatureConstants.Athach] = GetMultiplierFromAverage(CreatureConstants.Athach, 4500);
            //Source: https://forgottenrealms.fandom.com/wiki/Avoral
            weights[CreatureConstants.Avoral][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Avoral, 120);
            weights[CreatureConstants.Avoral][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Avoral, 120);
            weights[CreatureConstants.Avoral][GenderConstants.Agender] = GetBaseFromAverage(CreatureConstants.Avoral, 120);
            weights[CreatureConstants.Avoral][CreatureConstants.Avoral] = GetMultiplierFromAverage(CreatureConstants.Avoral, 120);
            //Source: https://www.worldanvil.com/w/faerun-tatortotzke/a/azer-article
            weights[CreatureConstants.Azer][GenderConstants.Female] = GetBaseFromRange(CreatureConstants.Azer, 160, 220);
            weights[CreatureConstants.Azer][GenderConstants.Male] = GetBaseFromRange(CreatureConstants.Azer, 160, 220);
            weights[CreatureConstants.Azer][GenderConstants.Agender] = GetBaseFromRange(CreatureConstants.Azer, 160, 220);
            weights[CreatureConstants.Azer][CreatureConstants.Azer] = GetMultiplierFromRange(CreatureConstants.Azer, 160, 220);
            //Source: https://forgottenrealms.fandom.com/wiki/Babau
            weights[CreatureConstants.Babau][GenderConstants.Agender] = GetBaseFromAverage(CreatureConstants.Babau, 140);
            weights[CreatureConstants.Babau][CreatureConstants.Babau] = GetMultiplierFromAverage(CreatureConstants.Babau, 140);
            //Source: https://www.dimensions.com/element/mandrill-mandrillus-sphinx
            //https://www.d20srd.org/srd/monsters/baboon.htm (male)
            weights[CreatureConstants.Baboon][GenderConstants.Female] = GetBaseFromRange(CreatureConstants.Baboon, 26, 82);
            weights[CreatureConstants.Baboon][GenderConstants.Male] = GetBaseFromUpTo(CreatureConstants.Baboon, 90);
            weights[CreatureConstants.Baboon][CreatureConstants.Baboon] = GetMultiplierFromUpTo(CreatureConstants.Baboon, 90);
            //Source: https://www.d20srd.org/srd/monsters/badger.htm
            weights[CreatureConstants.Badger][GenderConstants.Female] = GetBaseFromRange(CreatureConstants.Badger, 25, 35);
            weights[CreatureConstants.Badger][GenderConstants.Male] = GetBaseFromRange(CreatureConstants.Badger, 25, 35);
            weights[CreatureConstants.Badger][CreatureConstants.Badger] = GetMultiplierFromRange(CreatureConstants.Badger, 25, 35);
            //Source: https://www.d20srd.org/srd/monsters/direBadger.htm
            weights[CreatureConstants.Badger_Dire][GenderConstants.Female] = GetBaseFromUpTo(CreatureConstants.Badger_Dire, 500);
            weights[CreatureConstants.Badger_Dire][GenderConstants.Male] = GetBaseFromUpTo(CreatureConstants.Badger_Dire, 500);
            weights[CreatureConstants.Badger_Dire][CreatureConstants.Badger_Dire] = GetMultiplierFromUpTo(CreatureConstants.Badger_Dire, 500);
            //Source: https://www.d20srd.org/srd/monsters/demon.htm#balor
            weights[CreatureConstants.Balor][GenderConstants.Agender] = GetBaseFromAverage(CreatureConstants.Balor, 4500);
            weights[CreatureConstants.Balor][CreatureConstants.Balor] = GetMultiplierFromAverage(CreatureConstants.Balor, 4500);
            //Source: https://www.d20srd.org/srd/monsters/devil.htm#barbedDevilHamatula
            weights[CreatureConstants.BarbedDevil_Hamatula][GenderConstants.Agender] = GetBaseFromAverage(CreatureConstants.BarbedDevil_Hamatula, 300);
            weights[CreatureConstants.BarbedDevil_Hamatula][CreatureConstants.BarbedDevil_Hamatula] = GetMultiplierFromAverage(CreatureConstants.BarbedDevil_Hamatula, 300);
            //Source: https://www.d20srd.org/srd/monsters/barghest.htm
            weights[CreatureConstants.Barghest][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Barghest, 180);
            weights[CreatureConstants.Barghest][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Barghest, 180);
            weights[CreatureConstants.Barghest][CreatureConstants.Barghest] = GetMultiplierFromAverage(CreatureConstants.Barghest, 180);
            weights[CreatureConstants.Barghest_Greater][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Barghest_Greater, 400);
            weights[CreatureConstants.Barghest_Greater][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Barghest_Greater, 400);
            weights[CreatureConstants.Barghest_Greater][CreatureConstants.Barghest_Greater] = GetMultiplierFromAverage(CreatureConstants.Barghest_Greater, 400);
            //Source: https://forgottenrealms.fandom.com/wiki/Basilisk
            weights[CreatureConstants.Basilisk][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Basilisk, 300);
            weights[CreatureConstants.Basilisk][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Basilisk, 300);
            weights[CreatureConstants.Basilisk][CreatureConstants.Basilisk] = GetMultiplierFromAverage(CreatureConstants.Basilisk, 300);
            //Scaling up. Since 1 size category bigger, x8
            weights[CreatureConstants.Basilisk_Greater][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Basilisk_Greater, 300 * 8);
            weights[CreatureConstants.Basilisk_Greater][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Basilisk_Greater, 300 * 8);
            weights[CreatureConstants.Basilisk_Greater][CreatureConstants.Basilisk_Greater] = GetMultiplierFromAverage(CreatureConstants.Basilisk_Greater, 300 * 8);
            //Source: https://www.dimensions.com/element/little-brown-bat-myotis-lucifugus
            weights[CreatureConstants.Bat][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Bat, 1);
            weights[CreatureConstants.Bat][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Bat, 1);
            weights[CreatureConstants.Bat][CreatureConstants.Bat] = GetMultiplierFromAverage(CreatureConstants.Bat, 1);
            //Source: https://www.d20srd.org/srd/monsters/direBat.htm
            weights[CreatureConstants.Bat_Dire][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Bat_Dire, 200);
            weights[CreatureConstants.Bat_Dire][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Bat_Dire, 200);
            weights[CreatureConstants.Bat_Dire][CreatureConstants.Bat_Dire] = GetMultiplierFromAverage(CreatureConstants.Bat_Dire, 200);
            //Source: https://www.d20srd.org/srd/monsters/swarm.htm Bats are Diminutive, so (.18-.46)x5000
            weights[CreatureConstants.Bat_Swarm][GenderConstants.Agender] = GetBaseFromRange(CreatureConstants.Bat_Swarm, 900, 2300);
            weights[CreatureConstants.Bat_Swarm][CreatureConstants.Bat_Swarm] = GetMultiplierFromRange(CreatureConstants.Bat_Swarm, 900, 2300);
            //Source: https://www.dimensions.com/element/american-black-bear
            weights[CreatureConstants.Bear_Black][GenderConstants.Female] = GetBaseFromRange(CreatureConstants.Bear_Black, 200, 450);
            weights[CreatureConstants.Bear_Black][GenderConstants.Male] = GetBaseFromRange(CreatureConstants.Bear_Black, 375, 600);
            weights[CreatureConstants.Bear_Black][CreatureConstants.Bear_Black] = GetMultiplierFromRange(CreatureConstants.Bear_Black, 375, 600);
            //Source: https://www.d20srd.org/srd/monsters/bearBrown.htm
            weights[CreatureConstants.Bear_Brown][GenderConstants.Female] = GetBaseFromAtLeast(CreatureConstants.Bear_Brown, 1800);
            weights[CreatureConstants.Bear_Brown][GenderConstants.Male] = GetBaseFromAtLeast(CreatureConstants.Bear_Brown, 1800);
            weights[CreatureConstants.Bear_Brown][CreatureConstants.Bear_Brown] = GetMultiplierFromAtLeast(CreatureConstants.Bear_Brown, 1800);
            //Source: https://forgottenrealms.fandom.com/wiki/Dire_bear
            weights[CreatureConstants.Bear_Dire][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Bear_Dire, 8000);
            weights[CreatureConstants.Bear_Dire][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Bear_Dire, 8000);
            weights[CreatureConstants.Bear_Dire][CreatureConstants.Bear_Dire] = GetMultiplierFromAverage(CreatureConstants.Bear_Dire, 8000);
            //Source: https://www.dimensions.com/element/polar-bears
            weights[CreatureConstants.Bear_Polar][GenderConstants.Female] = GetBaseFromRange(CreatureConstants.Bear_Polar, 330, 650);
            weights[CreatureConstants.Bear_Polar][GenderConstants.Male] = GetBaseFromRange(CreatureConstants.Bear_Polar, 775, 1500);
            weights[CreatureConstants.Bear_Polar][CreatureConstants.Bear_Polar] = GetMultiplierFromRange(CreatureConstants.Bear_Polar, 775, 1500);
            //Source: https://forgottenrealms.fandom.com/wiki/Barbazu
            weights[CreatureConstants.BeardedDevil_Barbazu][GenderConstants.Agender] = GetBaseFromAverage(CreatureConstants.BeardedDevil_Barbazu, 225);
            weights[CreatureConstants.BeardedDevil_Barbazu][CreatureConstants.BeardedDevil_Barbazu] = GetMultiplierFromAverage(CreatureConstants.BeardedDevil_Barbazu, 225);
            //Source: https://forgottenrealms.fandom.com/wiki/Bebilith
            weights[CreatureConstants.Bebilith][GenderConstants.Agender] = GetBaseFromAtLeast(CreatureConstants.Bebilith, 4000);
            weights[CreatureConstants.Bebilith][CreatureConstants.Bebilith] = GetMultiplierFromAtLeast(CreatureConstants.Bebilith, 4000);
            //Source: https://forgottenrealms.fandom.com/wiki/Behir
            weights[CreatureConstants.Behir][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Behir, 4000);
            weights[CreatureConstants.Behir][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Behir, 4000);
            weights[CreatureConstants.Behir][CreatureConstants.Behir] = GetMultiplierFromAverage(CreatureConstants.Behir, 4000);
            //Source: http://thecampaign20xx.blogspot.com/2015/07/dungeons-dragons-guide-to-beholder.html
            weights[CreatureConstants.Beholder][GenderConstants.Agender] = GetBaseFromAverage(CreatureConstants.Beholder, 4500);
            weights[CreatureConstants.Beholder][CreatureConstants.Beholder] = GetMultiplierFromAverage(CreatureConstants.Beholder, 4500);
            //Scaling down from Beholder. /2 length, so /8 weight
            weights[CreatureConstants.Beholder_Gauth][GenderConstants.Agender] = GetBaseFromAverage(CreatureConstants.Beholder_Gauth, 4500 / 8);
            weights[CreatureConstants.Beholder_Gauth][CreatureConstants.Beholder_Gauth] = GetMultiplierFromAverage(CreatureConstants.Beholder_Gauth, 4500 / 8);
            //Copying from Large air elemental
            weights[CreatureConstants.Belker][GenderConstants.Agender] = GetBaseFromAverage(CreatureConstants.Belker, 4);
            weights[CreatureConstants.Belker][CreatureConstants.Belker] = GetMultiplierFromAverage(CreatureConstants.Belker, 4);
            //Source: https://www.d20srd.org/srd/monsters/bison.htm
            weights[CreatureConstants.Bison][GenderConstants.Female] = GetBaseFromRange(CreatureConstants.Bison, 1800, 2400);
            weights[CreatureConstants.Bison][GenderConstants.Male] = GetBaseFromRange(CreatureConstants.Bison, 1800, 2400);
            weights[CreatureConstants.Bison][CreatureConstants.Bison] = GetMultiplierFromRange(CreatureConstants.Bison, 1800, 2400);
            //Source: https://forgottenrealms.fandom.com/wiki/Black_pudding
            weights[CreatureConstants.BlackPudding][GenderConstants.Agender] = GetBaseFromAverage(CreatureConstants.BlackPudding, 18_000);
            weights[CreatureConstants.BlackPudding][CreatureConstants.BlackPudding] = GetMultiplierFromAverage(CreatureConstants.BlackPudding, 18_000);
            //Elder is a size category up, so x8 weight
            weights[CreatureConstants.BlackPudding_Elder][GenderConstants.Agender] = GetBaseFromAverage(CreatureConstants.BlackPudding_Elder, 18_000 * 8);
            weights[CreatureConstants.BlackPudding_Elder][CreatureConstants.BlackPudding_Elder] = GetMultiplierFromAverage(CreatureConstants.BlackPudding_Elder, 18_000 * 8);
            //Source: https://www.d20pfsrd.com/bestiary/monster-listings/magical-beasts/blink-dog
            weights[CreatureConstants.BlinkDog][GenderConstants.Female] = GetBaseFromAtLeast(CreatureConstants.BlinkDog, 180);
            weights[CreatureConstants.BlinkDog][GenderConstants.Male] = GetBaseFromAtLeast(CreatureConstants.BlinkDog, 180);
            weights[CreatureConstants.BlinkDog][CreatureConstants.BlinkDog] = GetMultiplierFromAtLeast(CreatureConstants.BlinkDog, 180);
            //Source: https://www.dimensions.com/element/wild-boar
            weights[CreatureConstants.Boar][GenderConstants.Female] = GetBaseFromRange(CreatureConstants.Boar, 150, 220);
            weights[CreatureConstants.Boar][GenderConstants.Male] = GetBaseFromRange(CreatureConstants.Boar, 150, 220);
            weights[CreatureConstants.Boar][CreatureConstants.Boar] = GetMultiplierFromRange(CreatureConstants.Boar, 150, 220);
            //Source: https://forgottenrealms.fandom.com/wiki/Dire_boar
            weights[CreatureConstants.Boar_Dire][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Boar_Dire, 1200);
            weights[CreatureConstants.Boar_Dire][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Boar_Dire, 1200);
            weights[CreatureConstants.Boar_Dire][CreatureConstants.Boar_Dire] = GetMultiplierFromAverage(CreatureConstants.Boar_Dire, 1200);
            //INfo: Basing off of humans
            weights[CreatureConstants.Bodak][GenderConstants.Female] = "85";
            weights[CreatureConstants.Bodak][GenderConstants.Male] = "120";
            weights[CreatureConstants.Bodak][CreatureConstants.Bodak] = "2d4";
            //Source: https://forgottenrealms.fandom.com/wiki/Osyluth
            weights[CreatureConstants.BoneDevil_Osyluth][GenderConstants.Agender] = GetBaseFromAverage(CreatureConstants.BoneDevil_Osyluth, 500);
            weights[CreatureConstants.BoneDevil_Osyluth][CreatureConstants.BoneDevil_Osyluth] = GetMultiplierFromAverage(CreatureConstants.BoneDevil_Osyluth, 500);
            //Source: http://people.wku.edu/charles.plemons/ad&d/races/height.html //6d10 = Human 2d4
            weights[CreatureConstants.Bugbear][GenderConstants.Female] = "180";
            weights[CreatureConstants.Bugbear][GenderConstants.Male] = "210";
            weights[CreatureConstants.Bugbear][CreatureConstants.Bugbear] = "2d4";
            //Source: http://gurpswiki.wikidot.com/m:bulette
            weights[CreatureConstants.Bulette][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Bulette, 1200);
            weights[CreatureConstants.Bulette][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Bulette, 1200);
            weights[CreatureConstants.Bulette][CreatureConstants.Bulette] = GetMultiplierFromAverage(CreatureConstants.Bulette, 1200);
            //Source: https://www.dimensions.com/element/bactrian-camel
            weights[CreatureConstants.Camel_Bactrian][GenderConstants.Female] = GetBaseFromRange(CreatureConstants.Camel_Bactrian, 990, 1100);
            weights[CreatureConstants.Camel_Bactrian][GenderConstants.Male] = GetBaseFromRange(CreatureConstants.Camel_Bactrian, 990, 1100);
            weights[CreatureConstants.Camel_Bactrian][CreatureConstants.Camel_Bactrian] = GetMultiplierFromRange(CreatureConstants.Camel_Bactrian, 990, 1100);
            //Source: https://www.dimensions.com/element/dromedary-camel
            weights[CreatureConstants.Camel_Dromedary][GenderConstants.Female] = GetBaseFromRange(CreatureConstants.Camel_Dromedary, 880, 1320);
            weights[CreatureConstants.Camel_Dromedary][GenderConstants.Male] = GetBaseFromRange(CreatureConstants.Camel_Dromedary, 880, 1320);
            weights[CreatureConstants.Camel_Dromedary][CreatureConstants.Camel_Dromedary] = GetMultiplierFromRange(CreatureConstants.Camel_Dromedary, 880, 1320);
            //Source: https://forgottenrealms.fandom.com/wiki/Carrion_crawler
            weights[CreatureConstants.CarrionCrawler][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.CarrionCrawler, 500);
            weights[CreatureConstants.CarrionCrawler][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.CarrionCrawler, 500);
            weights[CreatureConstants.CarrionCrawler][CreatureConstants.CarrionCrawler] = GetMultiplierFromAverage(CreatureConstants.CarrionCrawler, 500);
            //Source: https://www.dimensions.com/element/american-shorthair-cat
            weights[CreatureConstants.Cat][GenderConstants.Female] = GetBaseFromRange(CreatureConstants.Cat, 10, 15);
            weights[CreatureConstants.Cat][GenderConstants.Male] = GetBaseFromRange(CreatureConstants.Cat, 10, 15);
            weights[CreatureConstants.Cat][CreatureConstants.Cat] = GetMultiplierFromRange(CreatureConstants.Cat, 10, 15);
            //Source: http://people.wku.edu/charles.plemons/ad&d/races/height.html //6d20 = double 6d10 = double 2d4 = 2d8
            weights[CreatureConstants.Centaur][GenderConstants.Female] = "960";
            weights[CreatureConstants.Centaur][GenderConstants.Male] = "1000";
            weights[CreatureConstants.Centaur][CreatureConstants.Centaur] = "2d8";
            //Source: https://www.d20srd.org/srd/monsters/swarm.htm Centipedes are Diminutive, so x5000
            //https://alexaanswers.amazon.com/question/3zw2aivt2nsallUz6Ylyut [1-1.5 grams]x5000 = [11, 17]
            weights[CreatureConstants.Centipede_Swarm][GenderConstants.Agender] = GetBaseFromRange(CreatureConstants.Centipede_Swarm, 11, 17);
            weights[CreatureConstants.Centipede_Swarm][CreatureConstants.Centipede_Swarm] = GetMultiplierFromRange(CreatureConstants.Centipede_Swarm, 11, 17);
            //Source: https://www.d20srd.org/srd/monsters/devil.htm#chainDevilKyton
            weights[CreatureConstants.ChainDevil_Kyton][GenderConstants.Agender] = GetBaseFromAverage(CreatureConstants.ChainDevil_Kyton, 300);
            weights[CreatureConstants.ChainDevil_Kyton][CreatureConstants.ChainDevil_Kyton] = GetMultiplierFromAverage(CreatureConstants.ChainDevil_Kyton, 300);
            //Source: https://forgottenrealms.fandom.com/wiki/Chaos_beast
            weights[CreatureConstants.ChaosBeast][GenderConstants.Agender] = GetBaseFromAverage(CreatureConstants.ChaosBeast, 200);
            weights[CreatureConstants.ChaosBeast][CreatureConstants.ChaosBeast] = GetMultiplierFromAverage(CreatureConstants.ChaosBeast, 200);
            //Source: https://www.d20srd.org/srd/monsters/cheetah.htm
            weights[CreatureConstants.Cheetah][GenderConstants.Female] = GetBaseFromRange(CreatureConstants.Cheetah, 110, 130);
            weights[CreatureConstants.Cheetah][GenderConstants.Male] = GetBaseFromRange(CreatureConstants.Cheetah, 110, 130);
            weights[CreatureConstants.Cheetah][CreatureConstants.Cheetah] = GetMultiplierFromRange(CreatureConstants.Cheetah, 110, 130);
            //Source: https://forgottenrealms.fandom.com/wiki/Chimera
            weights[CreatureConstants.Chimera_Black][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Chimera_Black, 4000);
            weights[CreatureConstants.Chimera_Black][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Chimera_Black, 4000);
            weights[CreatureConstants.Chimera_Black][CreatureConstants.Chimera_Black] = GetMultiplierFromAverage(CreatureConstants.Chimera_Black, 4000);
            weights[CreatureConstants.Chimera_Blue][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Chimera_Blue, 4000);
            weights[CreatureConstants.Chimera_Blue][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Chimera_Blue, 4000);
            weights[CreatureConstants.Chimera_Blue][CreatureConstants.Chimera_Blue] = GetMultiplierFromAverage(CreatureConstants.Chimera_Blue, 4000);
            weights[CreatureConstants.Chimera_Green][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Chimera_Green, 4000);
            weights[CreatureConstants.Chimera_Green][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Chimera_Green, 4000);
            weights[CreatureConstants.Chimera_Green][CreatureConstants.Chimera_Green] = GetMultiplierFromAverage(CreatureConstants.Chimera_Green, 4000);
            weights[CreatureConstants.Chimera_Red][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Chimera_Red, 4000);
            weights[CreatureConstants.Chimera_Red][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Chimera_Red, 4000);
            weights[CreatureConstants.Chimera_Red][CreatureConstants.Chimera_Red] = GetMultiplierFromAverage(CreatureConstants.Chimera_Red, 4000);
            weights[CreatureConstants.Chimera_White][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Chimera_White, 4000);
            weights[CreatureConstants.Chimera_White][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Chimera_White, 4000);
            weights[CreatureConstants.Chimera_White][CreatureConstants.Chimera_White] = GetMultiplierFromAverage(CreatureConstants.Chimera_White, 4000);
            //Source: https://forgottenrealms.fandom.com/wiki/Choker
            weights[CreatureConstants.Choker][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Choker, 35);
            weights[CreatureConstants.Choker][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Choker, 35);
            weights[CreatureConstants.Choker][CreatureConstants.Choker] = GetMultiplierFromAverage(CreatureConstants.Choker, 35);
            //Source: https://forgottenrealms.fandom.com/wiki/Chuul
            weights[CreatureConstants.Chuul][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Chuul, 650);
            weights[CreatureConstants.Chuul][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Chuul, 650);
            weights[CreatureConstants.Chuul][CreatureConstants.Chuul] = GetMultiplierFromAverage(CreatureConstants.Chuul, 650);
            //Source: https://forgottenrealms.fandom.com/wiki/Cloaker
            weights[CreatureConstants.Cloaker][GenderConstants.Agender] = GetBaseFromAverage(CreatureConstants.Cloaker, 100);
            weights[CreatureConstants.Cloaker][CreatureConstants.Cloaker] = GetMultiplierFromAverage(CreatureConstants.Cloaker, 100);
            //Source: https://forgottenrealms.fandom.com/wiki/Cockatrice
            weights[CreatureConstants.Cockatrice][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Cockatrice, 25);
            weights[CreatureConstants.Cockatrice][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Cockatrice, 25);
            weights[CreatureConstants.Cockatrice][CreatureConstants.Cockatrice] = GetMultiplierFromAverage(CreatureConstants.Cockatrice, 25);
            //Source: https://forgottenrealms.fandom.com/wiki/Couatl
            weights[CreatureConstants.Couatl][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Couatl, 1800);
            weights[CreatureConstants.Couatl][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Couatl, 1800);
            weights[CreatureConstants.Couatl][CreatureConstants.Couatl] = GetMultiplierFromAverage(CreatureConstants.Couatl, 1800);
            //Source: https://www.d20srd.org/srd/monsters/sphinx.htm
            weights[CreatureConstants.Criosphinx][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Criosphinx, 800);
            weights[CreatureConstants.Criosphinx][CreatureConstants.Criosphinx] = GetMultiplierFromAverage(CreatureConstants.Criosphinx, 800);
            //Source: https://www.dimensions.com/element/nile-crocodile-crocodylus-niloticus
            weights[CreatureConstants.Crocodile][GenderConstants.Female] = GetBaseFromRange(CreatureConstants.Crocodile, 496, 1102);
            weights[CreatureConstants.Crocodile][GenderConstants.Male] = GetBaseFromRange(CreatureConstants.Crocodile, 496, 1102);
            weights[CreatureConstants.Crocodile][CreatureConstants.Crocodile] = GetMultiplierFromRange(CreatureConstants.Crocodile, 496, 1102);
            //Source: https://www.dimensions.com/element/saltwater-crocodile-crocodylus-porosus
            weights[CreatureConstants.Crocodile_Giant][GenderConstants.Female] = GetBaseFromRange(CreatureConstants.Crocodile_Giant, 180, 220);
            weights[CreatureConstants.Crocodile_Giant][GenderConstants.Male] = GetBaseFromRange(CreatureConstants.Crocodile_Giant, 880, 2200);
            weights[CreatureConstants.Crocodile_Giant][CreatureConstants.Crocodile_Giant] = GetMultiplierFromRange(CreatureConstants.Crocodile_Giant, 880, 2200);
            //Source: https://forgottenrealms.fandom.com/wiki/Darkmantle
            weights[CreatureConstants.Darkmantle][GenderConstants.Hermaphrodite] = GetBaseFromAverage(CreatureConstants.Darkmantle, 30);
            weights[CreatureConstants.Darkmantle][CreatureConstants.Darkmantle] = GetMultiplierFromAverage(CreatureConstants.Darkmantle, 30);
            //Source: https://www.dimensions.com/element/deinonychus-deinonychus-antirrhopus
            weights[CreatureConstants.Deinonychus][GenderConstants.Female] = GetBaseFromRange(CreatureConstants.Deinonychus, 160, 220);
            weights[CreatureConstants.Deinonychus][GenderConstants.Male] = GetBaseFromRange(CreatureConstants.Deinonychus, 160, 220);
            weights[CreatureConstants.Deinonychus][CreatureConstants.Deinonychus] = GetMultiplierFromRange(CreatureConstants.Deinonychus, 160, 220);
            //Source: https://dungeonsdragons.fandom.com/wiki/Delver
            weights[CreatureConstants.Delver][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Delver, 6000);
            weights[CreatureConstants.Delver][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Delver, 6000);
            weights[CreatureConstants.Delver][CreatureConstants.Delver] = GetMultiplierFromAverage(CreatureConstants.Delver, 6000);
            //Source: https://monster.fandom.com/wiki/Derro
            weights[CreatureConstants.Derro][GenderConstants.Female] = GetBaseFromUpTo(CreatureConstants.Derro, 40);
            weights[CreatureConstants.Derro][GenderConstants.Male] = GetBaseFromUpTo(CreatureConstants.Derro, 40);
            weights[CreatureConstants.Derro][CreatureConstants.Derro] = GetMultiplierFromUpTo(CreatureConstants.Derro, 40);
            weights[CreatureConstants.Derro_Sane][GenderConstants.Female] = GetBaseFromUpTo(CreatureConstants.Derro_Sane, 40);
            weights[CreatureConstants.Derro_Sane][GenderConstants.Male] = GetBaseFromUpTo(CreatureConstants.Derro_Sane, 40);
            weights[CreatureConstants.Derro_Sane][CreatureConstants.Derro_Sane] = GetMultiplierFromUpTo(CreatureConstants.Derro_Sane, 40);
            //Source: https://forgottenrealms.fandom.com/wiki/Destrachan
            weights[CreatureConstants.Destrachan][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Delver, 4000);
            weights[CreatureConstants.Destrachan][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Delver, 4000);
            weights[CreatureConstants.Destrachan][CreatureConstants.Destrachan] = GetMultiplierFromAverage(CreatureConstants.Delver, 4000);
            //Source: https://www.d20srd.org/srd/monsters/devourer.htm
            weights[CreatureConstants.Devourer][GenderConstants.Agender] = GetBaseFromAverage(CreatureConstants.Delver, 500);
            weights[CreatureConstants.Devourer][CreatureConstants.Devourer] = GetMultiplierFromAverage(CreatureConstants.Devourer, 500);
            //Source: https://forgottenrealms.fandom.com/wiki/Digester
            weights[CreatureConstants.Digester][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Digester, 350);
            weights[CreatureConstants.Digester][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Digester, 350);
            weights[CreatureConstants.Digester][CreatureConstants.Digester] = GetMultiplierFromAverage(CreatureConstants.Digester, 350);
            //Source: https://forgottenrealms.fandom.com/wiki/Displacer_beast
            weights[CreatureConstants.DisplacerBeast][GenderConstants.Female] = GetBaseFromRange(CreatureConstants.DisplacerBeast, 450, 450 * 2);
            weights[CreatureConstants.DisplacerBeast][GenderConstants.Male] = GetBaseFromRange(CreatureConstants.DisplacerBeast, 500, 500 * 2);
            weights[CreatureConstants.DisplacerBeast][CreatureConstants.DisplacerBeast] = GetMultiplierFromRange(CreatureConstants.DisplacerBeast, 500, 500 * 2);
            //Source: scale up from normal, x8
            weights[CreatureConstants.DisplacerBeast_PackLord][GenderConstants.Female] = GetBaseFromRange(CreatureConstants.DisplacerBeast_PackLord, 450 * 8, 450 * 16);
            weights[CreatureConstants.DisplacerBeast_PackLord][GenderConstants.Male] = GetBaseFromRange(CreatureConstants.DisplacerBeast_PackLord, 500 * 8, 500 * 16);
            weights[CreatureConstants.DisplacerBeast_PackLord][CreatureConstants.DisplacerBeast_PackLord] =
                GetMultiplierFromRange(CreatureConstants.DisplacerBeast_PackLord, 500 * 8, 500 * 16);
            //Source: https://forgottenrealms.fandom.com/wiki/Djinni
            weights[CreatureConstants.Djinni][GenderConstants.Agender] = GetBaseFromAverage(CreatureConstants.Djinni, 1000);
            weights[CreatureConstants.Djinni][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Djinni, 1000);
            weights[CreatureConstants.Djinni][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Djinni, 1000);
            weights[CreatureConstants.Djinni][CreatureConstants.Djinni] = GetMultiplierFromAverage(CreatureConstants.Djinni, 1000);
            //Source: https://forgottenrealms.fandom.com/wiki/Noble_djinni height increase by x1.14, so weight increase by x1.5
            weights[CreatureConstants.Djinni_Noble][GenderConstants.Agender] = GetBaseFromAverage(CreatureConstants.Djinni_Noble, 1500);
            weights[CreatureConstants.Djinni_Noble][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Djinni_Noble, 1500);
            weights[CreatureConstants.Djinni_Noble][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Djinni_Noble, 1500);
            weights[CreatureConstants.Djinni_Noble][CreatureConstants.Djinni_Noble] = GetMultiplierFromAverage(CreatureConstants.Djinni_Noble, 1500);
            //Source: https://www.d20srd.org/srd/monsters/dog.htm
            weights[CreatureConstants.Dog][GenderConstants.Female] = GetBaseFromRange(CreatureConstants.Dog, 20, 50);
            weights[CreatureConstants.Dog][GenderConstants.Male] = GetBaseFromRange(CreatureConstants.Dog, 20, 50);
            weights[CreatureConstants.Dog][CreatureConstants.Dog] = GetMultiplierFromRange(CreatureConstants.Dog, 20, 50);
            //Source: https://www.dimensions.com/element/saint-bernard-dog M:140-180,F:120-140
            //https://www.dimensions.com/element/siberian-husky 35-65
            //https://www.dimensions.com/element/dogs-collie M:45-65,F:40-55
            weights[CreatureConstants.Dog_Riding][GenderConstants.Female] = GetBaseFromRange(CreatureConstants.Dog_Riding, 35, 140);
            weights[CreatureConstants.Dog_Riding][GenderConstants.Male] = GetBaseFromRange(CreatureConstants.Dog_Riding, 35, 180);
            weights[CreatureConstants.Dog_Riding][CreatureConstants.Dog_Riding] = GetMultiplierFromRange(CreatureConstants.Dog_Riding, 35, 180);
            //Source: https://www.dimensions.com/element/donkey-equus-africanus-asinus
            weights[CreatureConstants.Donkey][GenderConstants.Female] = GetBaseFromRange(CreatureConstants.Donkey, 400, 500);
            weights[CreatureConstants.Donkey][GenderConstants.Male] = GetBaseFromRange(CreatureConstants.Donkey, 400, 500);
            weights[CreatureConstants.Donkey][CreatureConstants.Donkey] = GetMultiplierFromRange(CreatureConstants.Donkey, 400, 500);
            //Source: https://forgottenrealms.fandom.com/wiki/Doppelganger
            weights[CreatureConstants.Doppelganger][GenderConstants.Agender] = GetBaseFromAverage(CreatureConstants.Doppelganger, 150);
            weights[CreatureConstants.Doppelganger][CreatureConstants.Doppelganger] = GetMultiplierFromAverage(CreatureConstants.Doppelganger, 150);
            //Source: Draconomicon
            weights[CreatureConstants.Dragon_Black_Wyrmling][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_Black_Wyrmling, 5);
            weights[CreatureConstants.Dragon_Black_Wyrmling][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_Black_Wyrmling, 5);
            weights[CreatureConstants.Dragon_Black_Wyrmling][CreatureConstants.Dragon_Black_Wyrmling] =
                GetMultiplierFromAverage(CreatureConstants.Dragon_Black_Wyrmling, 5);
            weights[CreatureConstants.Dragon_Black_VeryYoung][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_Black_VeryYoung, 40);
            weights[CreatureConstants.Dragon_Black_VeryYoung][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_Black_VeryYoung, 40);
            weights[CreatureConstants.Dragon_Black_VeryYoung][CreatureConstants.Dragon_Black_VeryYoung] =
                GetMultiplierFromAverage(CreatureConstants.Dragon_Black_VeryYoung, 40);
            weights[CreatureConstants.Dragon_Black_Young][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_Black_Young, 320);
            weights[CreatureConstants.Dragon_Black_Young][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_Black_Young, 320);
            weights[CreatureConstants.Dragon_Black_Young][CreatureConstants.Dragon_Black_Young] =
                GetMultiplierFromAverage(CreatureConstants.Dragon_Black_Young, 320);
            weights[CreatureConstants.Dragon_Black_Juvenile][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_Black_Juvenile, 320);
            weights[CreatureConstants.Dragon_Black_Juvenile][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_Black_Juvenile, 320);
            weights[CreatureConstants.Dragon_Black_Juvenile][CreatureConstants.Dragon_Black_Juvenile] =
                GetMultiplierFromAverage(CreatureConstants.Dragon_Black_Juvenile, 320);
            weights[CreatureConstants.Dragon_Black_YoungAdult][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_Black_YoungAdult, 2500);
            weights[CreatureConstants.Dragon_Black_YoungAdult][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_Black_YoungAdult, 2500);
            weights[CreatureConstants.Dragon_Black_YoungAdult][CreatureConstants.Dragon_Black_YoungAdult] =
                GetMultiplierFromAverage(CreatureConstants.Dragon_Black_YoungAdult, 2500);
            weights[CreatureConstants.Dragon_Black_Adult][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_Black_Adult, 2500);
            weights[CreatureConstants.Dragon_Black_Adult][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_Black_Adult, 2500);
            weights[CreatureConstants.Dragon_Black_Adult][CreatureConstants.Dragon_Black_Adult] =
                GetMultiplierFromAverage(CreatureConstants.Dragon_Black_Adult, 2500);
            weights[CreatureConstants.Dragon_Black_MatureAdult][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_Black_MatureAdult, 20_000);
            weights[CreatureConstants.Dragon_Black_MatureAdult][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_Black_MatureAdult, 20_000);
            weights[CreatureConstants.Dragon_Black_MatureAdult][CreatureConstants.Dragon_Black_MatureAdult] =
                GetMultiplierFromAverage(CreatureConstants.Dragon_Black_MatureAdult, 20_000);
            weights[CreatureConstants.Dragon_Black_Old][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_Black_Old, 20_000);
            weights[CreatureConstants.Dragon_Black_Old][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_Black_Old, 20_000);
            weights[CreatureConstants.Dragon_Black_Old][CreatureConstants.Dragon_Black_Old] =
                GetMultiplierFromAverage(CreatureConstants.Dragon_Black_Old, 20_000);
            weights[CreatureConstants.Dragon_Black_VeryOld][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_Black_VeryOld, 20_000);
            weights[CreatureConstants.Dragon_Black_VeryOld][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_Black_VeryOld, 20_000);
            weights[CreatureConstants.Dragon_Black_VeryOld][CreatureConstants.Dragon_Black_VeryOld] =
                GetMultiplierFromAverage(CreatureConstants.Dragon_Black_VeryOld, 20_000);
            weights[CreatureConstants.Dragon_Black_Ancient][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_Black_Ancient, 20_000);
            weights[CreatureConstants.Dragon_Black_Ancient][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_Black_Ancient, 20_000);
            weights[CreatureConstants.Dragon_Black_Ancient][CreatureConstants.Dragon_Black_Ancient] =
                GetMultiplierFromAverage(CreatureConstants.Dragon_Black_Ancient, 20_000);
            weights[CreatureConstants.Dragon_Black_Wyrm][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_Black_Wyrm, 160_000);
            weights[CreatureConstants.Dragon_Black_Wyrm][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_Black_Wyrm, 160_000);
            weights[CreatureConstants.Dragon_Black_Wyrm][CreatureConstants.Dragon_Black_Wyrm] =
                GetMultiplierFromAverage(CreatureConstants.Dragon_Black_Wyrm, 160_000);
            weights[CreatureConstants.Dragon_Black_GreatWyrm][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_Black_GreatWyrm, 160_000);
            weights[CreatureConstants.Dragon_Black_GreatWyrm][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_Black_GreatWyrm, 160_000);
            weights[CreatureConstants.Dragon_Black_GreatWyrm][CreatureConstants.Dragon_Black_GreatWyrm] =
                GetMultiplierFromAverage(CreatureConstants.Dragon_Black_GreatWyrm, 160_000);
            weights[CreatureConstants.Dragon_Blue_Wyrmling][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_Blue_Wyrmling, 40);
            weights[CreatureConstants.Dragon_Blue_Wyrmling][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_Blue_Wyrmling, 40);
            weights[CreatureConstants.Dragon_Blue_Wyrmling][CreatureConstants.Dragon_Blue_Wyrmling] = GetMultiplierFromAverage(CreatureConstants.Dragon_Blue_Wyrmling, 40);
            weights[CreatureConstants.Dragon_Blue_VeryYoung][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_Blue_VeryYoung, 320);
            weights[CreatureConstants.Dragon_Blue_VeryYoung][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_Blue_VeryYoung, 320);
            weights[CreatureConstants.Dragon_Blue_VeryYoung][CreatureConstants.Dragon_Blue_VeryYoung] =
                GetMultiplierFromAverage(CreatureConstants.Dragon_Blue_VeryYoung, 320);
            weights[CreatureConstants.Dragon_Blue_Young][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_Blue_Young, 320);
            weights[CreatureConstants.Dragon_Blue_Young][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_Blue_Young, 320);
            weights[CreatureConstants.Dragon_Blue_Young][CreatureConstants.Dragon_Blue_Young] = GetMultiplierFromAverage(CreatureConstants.Dragon_Blue_Young, 320);
            weights[CreatureConstants.Dragon_Blue_Juvenile][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_Blue_Juvenile, 2500);
            weights[CreatureConstants.Dragon_Blue_Juvenile][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_Blue_Juvenile, 2500);
            weights[CreatureConstants.Dragon_Blue_Juvenile][CreatureConstants.Dragon_Blue_Juvenile] =
                GetMultiplierFromAverage(CreatureConstants.Dragon_Blue_Juvenile, 2500);
            weights[CreatureConstants.Dragon_Blue_YoungAdult][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_Blue_YoungAdult, 2500);
            weights[CreatureConstants.Dragon_Blue_YoungAdult][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_Blue_YoungAdult, 2500);
            weights[CreatureConstants.Dragon_Blue_YoungAdult][CreatureConstants.Dragon_Blue_YoungAdult] =
                GetMultiplierFromAverage(CreatureConstants.Dragon_Blue_YoungAdult, 2500);
            weights[CreatureConstants.Dragon_Blue_Adult][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_Blue_Adult, 20_000);
            weights[CreatureConstants.Dragon_Blue_Adult][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_Blue_Adult, 20_000);
            weights[CreatureConstants.Dragon_Blue_Adult][CreatureConstants.Dragon_Blue_Adult] = GetMultiplierFromAverage(CreatureConstants.Dragon_Blue_Adult, 20_000);
            weights[CreatureConstants.Dragon_Blue_MatureAdult][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_Blue_MatureAdult, 20_000);
            weights[CreatureConstants.Dragon_Blue_MatureAdult][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_Blue_MatureAdult, 20_000);
            weights[CreatureConstants.Dragon_Blue_MatureAdult][CreatureConstants.Dragon_Blue_MatureAdult] =
                GetMultiplierFromAverage(CreatureConstants.Dragon_Blue_MatureAdult, 20_000);
            weights[CreatureConstants.Dragon_Blue_Old][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_Blue_Old, 20_000);
            weights[CreatureConstants.Dragon_Blue_Old][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_Blue_Old, 20_000);
            weights[CreatureConstants.Dragon_Blue_Old][CreatureConstants.Dragon_Blue_Old] = GetMultiplierFromAverage(CreatureConstants.Dragon_Blue_Old, 20_000);
            weights[CreatureConstants.Dragon_Blue_VeryOld][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_Blue_VeryOld, 20_000);
            weights[CreatureConstants.Dragon_Blue_VeryOld][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_Blue_VeryOld, 20_000);
            weights[CreatureConstants.Dragon_Blue_VeryOld][CreatureConstants.Dragon_Blue_VeryOld] =
                GetMultiplierFromAverage(CreatureConstants.Dragon_Blue_VeryOld, 20_000);
            weights[CreatureConstants.Dragon_Blue_Ancient][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_Blue_Ancient, 160_000);
            weights[CreatureConstants.Dragon_Blue_Ancient][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_Blue_Ancient, 160_000);
            weights[CreatureConstants.Dragon_Blue_Ancient][CreatureConstants.Dragon_Blue_Ancient] =
                GetMultiplierFromAverage(CreatureConstants.Dragon_Blue_Ancient, 160_000);
            weights[CreatureConstants.Dragon_Blue_Wyrm][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_Blue_Wyrm, 160_000);
            weights[CreatureConstants.Dragon_Blue_Wyrm][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_Blue_Wyrm, 160_000);
            weights[CreatureConstants.Dragon_Blue_Wyrm][CreatureConstants.Dragon_Blue_Wyrm] = GetMultiplierFromAverage(CreatureConstants.Dragon_Blue_Wyrm, 160_000);
            weights[CreatureConstants.Dragon_Blue_GreatWyrm][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_Blue_GreatWyrm, 160_000);
            weights[CreatureConstants.Dragon_Blue_GreatWyrm][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_Blue_GreatWyrm, 160_000);
            weights[CreatureConstants.Dragon_Blue_GreatWyrm][CreatureConstants.Dragon_Blue_GreatWyrm] =
                GetMultiplierFromAverage(CreatureConstants.Dragon_Blue_GreatWyrm, 160_000);
            weights[CreatureConstants.Dragon_Brass_Wyrmling][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_Brass_Wyrmling, 5);
            weights[CreatureConstants.Dragon_Brass_Wyrmling][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_Brass_Wyrmling, 5);
            weights[CreatureConstants.Dragon_Brass_Wyrmling][CreatureConstants.Dragon_Brass_Wyrmling] =
                GetMultiplierFromAverage(CreatureConstants.Dragon_Brass_Wyrmling, 5);
            weights[CreatureConstants.Dragon_Brass_VeryYoung][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_Brass_VeryYoung, 40);
            weights[CreatureConstants.Dragon_Brass_VeryYoung][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_Brass_VeryYoung, 40);
            weights[CreatureConstants.Dragon_Brass_VeryYoung][CreatureConstants.Dragon_Brass_VeryYoung] =
                GetMultiplierFromAverage(CreatureConstants.Dragon_Brass_VeryYoung, 40);
            weights[CreatureConstants.Dragon_Brass_Young][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_Brass_Young, 320);
            weights[CreatureConstants.Dragon_Brass_Young][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_Brass_Young, 320);
            weights[CreatureConstants.Dragon_Brass_Young][CreatureConstants.Dragon_Brass_Young] = GetMultiplierFromAverage(CreatureConstants.Dragon_Brass_Young, 320);
            weights[CreatureConstants.Dragon_Brass_Juvenile][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_Brass_Juvenile, 320);
            weights[CreatureConstants.Dragon_Brass_Juvenile][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_Brass_Juvenile, 320);
            weights[CreatureConstants.Dragon_Brass_Juvenile][CreatureConstants.Dragon_Brass_Juvenile] =
                GetMultiplierFromAverage(CreatureConstants.Dragon_Brass_Juvenile, 320);
            weights[CreatureConstants.Dragon_Brass_YoungAdult][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_Brass_YoungAdult, 2500);
            weights[CreatureConstants.Dragon_Brass_YoungAdult][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_Brass_YoungAdult, 2500);
            weights[CreatureConstants.Dragon_Brass_YoungAdult][CreatureConstants.Dragon_Brass_YoungAdult] =
                GetMultiplierFromAverage(CreatureConstants.Dragon_Brass_YoungAdult, 2500);
            weights[CreatureConstants.Dragon_Brass_Adult][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_Brass_Adult, 2500);
            weights[CreatureConstants.Dragon_Brass_Adult][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_Brass_Adult, 2500);
            weights[CreatureConstants.Dragon_Brass_Adult][CreatureConstants.Dragon_Brass_Adult] = GetMultiplierFromAverage(CreatureConstants.Dragon_Brass_Adult, 2500);
            weights[CreatureConstants.Dragon_Brass_MatureAdult][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_Brass_MatureAdult, 20_000);
            weights[CreatureConstants.Dragon_Brass_MatureAdult][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_Brass_MatureAdult, 20_000);
            weights[CreatureConstants.Dragon_Brass_MatureAdult][CreatureConstants.Dragon_Brass_MatureAdult] =
                GetMultiplierFromAverage(CreatureConstants.Dragon_Brass_MatureAdult, 20_000);
            weights[CreatureConstants.Dragon_Brass_Old][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_Brass_Old, 20_000);
            weights[CreatureConstants.Dragon_Brass_Old][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_Brass_Old, 20_000);
            weights[CreatureConstants.Dragon_Brass_Old][CreatureConstants.Dragon_Brass_Old] = GetMultiplierFromAverage(CreatureConstants.Dragon_Brass_Old, 20_000);
            weights[CreatureConstants.Dragon_Brass_VeryOld][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_Brass_VeryOld, 20_000);
            weights[CreatureConstants.Dragon_Brass_VeryOld][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_Brass_VeryOld, 20_000);
            weights[CreatureConstants.Dragon_Brass_VeryOld][CreatureConstants.Dragon_Brass_VeryOld] =
                GetMultiplierFromAverage(CreatureConstants.Dragon_Brass_VeryOld, 20_000);
            weights[CreatureConstants.Dragon_Brass_Ancient][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_Brass_Ancient, 20_000);
            weights[CreatureConstants.Dragon_Brass_Ancient][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_Brass_Ancient, 20_000);
            weights[CreatureConstants.Dragon_Brass_Ancient][CreatureConstants.Dragon_Brass_Ancient] =
                GetMultiplierFromAverage(CreatureConstants.Dragon_Brass_Ancient, 20_000);
            weights[CreatureConstants.Dragon_Brass_Wyrm][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_Brass_Wyrm, 160_000);
            weights[CreatureConstants.Dragon_Brass_Wyrm][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_Brass_Wyrm, 160_000);
            weights[CreatureConstants.Dragon_Brass_Wyrm][CreatureConstants.Dragon_Brass_Wyrm] = GetMultiplierFromAverage(CreatureConstants.Dragon_Brass_Wyrm, 160_000);
            weights[CreatureConstants.Dragon_Brass_GreatWyrm][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_Brass_GreatWyrm, 160_000);
            weights[CreatureConstants.Dragon_Brass_GreatWyrm][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_Brass_GreatWyrm, 160_000);
            weights[CreatureConstants.Dragon_Brass_GreatWyrm][CreatureConstants.Dragon_Brass_GreatWyrm] =
                GetMultiplierFromAverage(CreatureConstants.Dragon_Brass_GreatWyrm, 160_000);
            weights[CreatureConstants.Dragon_Bronze_Wyrmling][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_Bronze_Wyrmling, 40);
            weights[CreatureConstants.Dragon_Bronze_Wyrmling][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_Bronze_Wyrmling, 40);
            weights[CreatureConstants.Dragon_Bronze_Wyrmling][CreatureConstants.Dragon_Bronze_Wyrmling] =
                GetMultiplierFromAverage(CreatureConstants.Dragon_Bronze_Wyrmling, 40);
            weights[CreatureConstants.Dragon_Bronze_VeryYoung][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_Bronze_VeryYoung, 320);
            weights[CreatureConstants.Dragon_Bronze_VeryYoung][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_Bronze_VeryYoung, 320);
            weights[CreatureConstants.Dragon_Bronze_VeryYoung][CreatureConstants.Dragon_Bronze_VeryYoung] =
                GetMultiplierFromAverage(CreatureConstants.Dragon_Bronze_VeryYoung, 320);
            weights[CreatureConstants.Dragon_Bronze_Young][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_Bronze_Young, 320);
            weights[CreatureConstants.Dragon_Bronze_Young][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_Bronze_Young, 320);
            weights[CreatureConstants.Dragon_Bronze_Young][CreatureConstants.Dragon_Bronze_Young] = GetMultiplierFromAverage(CreatureConstants.Dragon_Bronze_Young, 320);
            weights[CreatureConstants.Dragon_Bronze_Juvenile][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_Bronze_Juvenile, 2500);
            weights[CreatureConstants.Dragon_Bronze_Juvenile][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_Bronze_Juvenile, 2500);
            weights[CreatureConstants.Dragon_Bronze_Juvenile][CreatureConstants.Dragon_Bronze_Juvenile] =
                GetMultiplierFromAverage(CreatureConstants.Dragon_Bronze_Juvenile, 2500);
            weights[CreatureConstants.Dragon_Bronze_YoungAdult][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_Bronze_YoungAdult, 2500);
            weights[CreatureConstants.Dragon_Bronze_YoungAdult][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_Bronze_YoungAdult, 2500);
            weights[CreatureConstants.Dragon_Bronze_YoungAdult][CreatureConstants.Dragon_Bronze_YoungAdult] =
                GetMultiplierFromAverage(CreatureConstants.Dragon_Bronze_YoungAdult, 2500);
            weights[CreatureConstants.Dragon_Bronze_Adult][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_Bronze_Adult, 20_000);
            weights[CreatureConstants.Dragon_Bronze_Adult][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_Bronze_Adult, 20_000);
            weights[CreatureConstants.Dragon_Bronze_Adult][CreatureConstants.Dragon_Bronze_Adult] =
                GetMultiplierFromAverage(CreatureConstants.Dragon_Bronze_Adult, 20_000);
            weights[CreatureConstants.Dragon_Bronze_MatureAdult][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_Bronze_MatureAdult, 20_000);
            weights[CreatureConstants.Dragon_Bronze_MatureAdult][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_Bronze_MatureAdult, 20_000);
            weights[CreatureConstants.Dragon_Bronze_MatureAdult][CreatureConstants.Dragon_Bronze_MatureAdult] =
                GetMultiplierFromAverage(CreatureConstants.Dragon_Bronze_MatureAdult, 20_000);
            weights[CreatureConstants.Dragon_Bronze_Old][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_Bronze_Old, 20_000);
            weights[CreatureConstants.Dragon_Bronze_Old][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_Bronze_Old, 20_000);
            weights[CreatureConstants.Dragon_Bronze_Old][CreatureConstants.Dragon_Bronze_Old] = GetMultiplierFromAverage(CreatureConstants.Dragon_Bronze_Old, 20_000);
            weights[CreatureConstants.Dragon_Bronze_VeryOld][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_Bronze_VeryOld, 20_000);
            weights[CreatureConstants.Dragon_Bronze_VeryOld][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_Bronze_VeryOld, 20_000);
            weights[CreatureConstants.Dragon_Bronze_VeryOld][CreatureConstants.Dragon_Bronze_VeryOld] =
                GetMultiplierFromAverage(CreatureConstants.Dragon_Bronze_VeryOld, 20_000);
            weights[CreatureConstants.Dragon_Bronze_Ancient][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_Bronze_Ancient, 160_000);
            weights[CreatureConstants.Dragon_Bronze_Ancient][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_Bronze_Ancient, 160_000);
            weights[CreatureConstants.Dragon_Bronze_Ancient][CreatureConstants.Dragon_Bronze_Ancient] =
                GetMultiplierFromAverage(CreatureConstants.Dragon_Bronze_Ancient, 160_000);
            weights[CreatureConstants.Dragon_Bronze_Wyrm][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_Bronze_Wyrm, 160_000);
            weights[CreatureConstants.Dragon_Bronze_Wyrm][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_Bronze_Wyrm, 160_000);
            weights[CreatureConstants.Dragon_Bronze_Wyrm][CreatureConstants.Dragon_Bronze_Wyrm] = GetMultiplierFromAverage(CreatureConstants.Dragon_Bronze_Wyrm, 160_000);
            weights[CreatureConstants.Dragon_Bronze_GreatWyrm][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_Bronze_GreatWyrm, 160_000);
            weights[CreatureConstants.Dragon_Bronze_GreatWyrm][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_Bronze_GreatWyrm, 160_000);
            weights[CreatureConstants.Dragon_Bronze_GreatWyrm][CreatureConstants.Dragon_Bronze_GreatWyrm] =
                GetMultiplierFromAverage(CreatureConstants.Dragon_Bronze_GreatWyrm, 160_000);
            weights[CreatureConstants.Dragon_Copper_Wyrmling][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_Copper_Wyrmling, 5);
            weights[CreatureConstants.Dragon_Copper_Wyrmling][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_Copper_Wyrmling, 5);
            weights[CreatureConstants.Dragon_Copper_Wyrmling][CreatureConstants.Dragon_Copper_Wyrmling] =
                GetMultiplierFromAverage(CreatureConstants.Dragon_Copper_Wyrmling, 5);
            weights[CreatureConstants.Dragon_Copper_VeryYoung][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_Copper_VeryYoung, 40);
            weights[CreatureConstants.Dragon_Copper_VeryYoung][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_Copper_VeryYoung, 40);
            weights[CreatureConstants.Dragon_Copper_VeryYoung][CreatureConstants.Dragon_Copper_VeryYoung] =
                GetMultiplierFromAverage(CreatureConstants.Dragon_Copper_VeryYoung, 40);
            weights[CreatureConstants.Dragon_Copper_Young][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_Copper_Young, 320);
            weights[CreatureConstants.Dragon_Copper_Young][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_Copper_Young, 320);
            weights[CreatureConstants.Dragon_Copper_Young][CreatureConstants.Dragon_Copper_Young] = GetMultiplierFromAverage(CreatureConstants.Dragon_Copper_Young, 320);
            weights[CreatureConstants.Dragon_Copper_Juvenile][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_Copper_Juvenile, 320);
            weights[CreatureConstants.Dragon_Copper_Juvenile][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_Copper_Juvenile, 320);
            weights[CreatureConstants.Dragon_Copper_Juvenile][CreatureConstants.Dragon_Copper_Juvenile] =
                GetMultiplierFromAverage(CreatureConstants.Dragon_Copper_Juvenile, 320);
            weights[CreatureConstants.Dragon_Copper_YoungAdult][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_Copper_YoungAdult, 2500);
            weights[CreatureConstants.Dragon_Copper_YoungAdult][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_Copper_YoungAdult, 2500);
            weights[CreatureConstants.Dragon_Copper_YoungAdult][CreatureConstants.Dragon_Copper_YoungAdult] =
                GetMultiplierFromAverage(CreatureConstants.Dragon_Copper_YoungAdult, 2500);
            weights[CreatureConstants.Dragon_Copper_Adult][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_Copper_Adult, 2500);
            weights[CreatureConstants.Dragon_Copper_Adult][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_Copper_Adult, 2500);
            weights[CreatureConstants.Dragon_Copper_Adult][CreatureConstants.Dragon_Copper_Adult] = GetMultiplierFromAverage(CreatureConstants.Dragon_Copper_Adult, 2500);
            weights[CreatureConstants.Dragon_Copper_MatureAdult][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_Copper_MatureAdult, 20_000);
            weights[CreatureConstants.Dragon_Copper_MatureAdult][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_Copper_MatureAdult, 20_000);
            weights[CreatureConstants.Dragon_Copper_MatureAdult][CreatureConstants.Dragon_Copper_MatureAdult] =
                GetMultiplierFromAverage(CreatureConstants.Dragon_Copper_MatureAdult, 20_000);
            weights[CreatureConstants.Dragon_Copper_Old][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_Copper_Old, 20_000);
            weights[CreatureConstants.Dragon_Copper_Old][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_Copper_Old, 20_000);
            weights[CreatureConstants.Dragon_Copper_Old][CreatureConstants.Dragon_Copper_Old] = GetMultiplierFromAverage(CreatureConstants.Dragon_Copper_Old, 20_000);
            weights[CreatureConstants.Dragon_Copper_VeryOld][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_Copper_VeryOld, 20_000);
            weights[CreatureConstants.Dragon_Copper_VeryOld][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_Copper_VeryOld, 20_000);
            weights[CreatureConstants.Dragon_Copper_VeryOld][CreatureConstants.Dragon_Copper_VeryOld] =
                GetMultiplierFromAverage(CreatureConstants.Dragon_Copper_VeryOld, 20_000);
            weights[CreatureConstants.Dragon_Copper_Ancient][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_Copper_Ancient, 20_000);
            weights[CreatureConstants.Dragon_Copper_Ancient][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_Copper_Ancient, 20_000);
            weights[CreatureConstants.Dragon_Copper_Ancient][CreatureConstants.Dragon_Copper_Ancient] =
                GetMultiplierFromAverage(CreatureConstants.Dragon_Copper_Ancient, 20_000);
            weights[CreatureConstants.Dragon_Copper_Wyrm][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_Copper_Wyrm, 160_000);
            weights[CreatureConstants.Dragon_Copper_Wyrm][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_Copper_Wyrm, 160_000);
            weights[CreatureConstants.Dragon_Copper_Wyrm][CreatureConstants.Dragon_Copper_Wyrm] = GetMultiplierFromAverage(CreatureConstants.Dragon_Copper_Wyrm, 160_000);
            weights[CreatureConstants.Dragon_Copper_GreatWyrm][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_Copper_GreatWyrm, 160_000);
            weights[CreatureConstants.Dragon_Copper_GreatWyrm][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_Copper_GreatWyrm, 160_000);
            weights[CreatureConstants.Dragon_Copper_GreatWyrm][CreatureConstants.Dragon_Copper_GreatWyrm] =
                GetMultiplierFromAverage(CreatureConstants.Dragon_Copper_GreatWyrm, 160_000);
            weights[CreatureConstants.Dragon_Gold_Wyrmling][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_Gold_Wyrmling, 320);
            weights[CreatureConstants.Dragon_Gold_Wyrmling][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_Gold_Wyrmling, 320);
            weights[CreatureConstants.Dragon_Gold_Wyrmling][CreatureConstants.Dragon_Gold_Wyrmling] =
                GetMultiplierFromAverage(CreatureConstants.Dragon_Gold_Wyrmling, 320);
            weights[CreatureConstants.Dragon_Gold_VeryYoung][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_Gold_VeryYoung, 2500);
            weights[CreatureConstants.Dragon_Gold_VeryYoung][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_Gold_VeryYoung, 2500);
            weights[CreatureConstants.Dragon_Gold_VeryYoung][CreatureConstants.Dragon_Gold_VeryYoung] =
                GetMultiplierFromAverage(CreatureConstants.Dragon_Gold_VeryYoung, 2500);
            weights[CreatureConstants.Dragon_Gold_Young][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_Gold_Young, 2500);
            weights[CreatureConstants.Dragon_Gold_Young][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_Gold_Young, 2500);
            weights[CreatureConstants.Dragon_Gold_Young][CreatureConstants.Dragon_Gold_Young] = GetMultiplierFromAverage(CreatureConstants.Dragon_Gold_Young, 2500);
            weights[CreatureConstants.Dragon_Gold_Juvenile][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_Gold_Juvenile, 2500);
            weights[CreatureConstants.Dragon_Gold_Juvenile][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_Gold_Juvenile, 2500);
            weights[CreatureConstants.Dragon_Gold_Juvenile][CreatureConstants.Dragon_Gold_Juvenile] =
                GetMultiplierFromAverage(CreatureConstants.Dragon_Gold_Juvenile, 2500);
            weights[CreatureConstants.Dragon_Gold_YoungAdult][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_Gold_YoungAdult, 20_000);
            weights[CreatureConstants.Dragon_Gold_YoungAdult][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_Gold_YoungAdult, 20_000);
            weights[CreatureConstants.Dragon_Gold_YoungAdult][CreatureConstants.Dragon_Gold_YoungAdult] =
                GetMultiplierFromAverage(CreatureConstants.Dragon_Gold_YoungAdult, 20_000);
            weights[CreatureConstants.Dragon_Gold_Adult][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_Gold_Adult, 20_000);
            weights[CreatureConstants.Dragon_Gold_Adult][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_Gold_Adult, 20_000);
            weights[CreatureConstants.Dragon_Gold_Adult][CreatureConstants.Dragon_Gold_Adult] = GetMultiplierFromAverage(CreatureConstants.Dragon_Gold_Adult, 20_000);
            weights[CreatureConstants.Dragon_Gold_MatureAdult][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_Gold_MatureAdult, 20_000);
            weights[CreatureConstants.Dragon_Gold_MatureAdult][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_Gold_MatureAdult, 20_000);
            weights[CreatureConstants.Dragon_Gold_MatureAdult][CreatureConstants.Dragon_Gold_MatureAdult] =
                GetMultiplierFromAverage(CreatureConstants.Dragon_Gold_MatureAdult, 20_000);
            weights[CreatureConstants.Dragon_Gold_Old][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_Gold_Old, 160_000);
            weights[CreatureConstants.Dragon_Gold_Old][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_Gold_Old, 160_000);
            weights[CreatureConstants.Dragon_Gold_Old][CreatureConstants.Dragon_Gold_Old] = GetMultiplierFromAverage(CreatureConstants.Dragon_Gold_Old, 160_000);
            weights[CreatureConstants.Dragon_Gold_VeryOld][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_Gold_VeryOld, 160_000);
            weights[CreatureConstants.Dragon_Gold_VeryOld][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_Gold_VeryOld, 160_000);
            weights[CreatureConstants.Dragon_Gold_VeryOld][CreatureConstants.Dragon_Gold_VeryOld] =
                GetMultiplierFromAverage(CreatureConstants.Dragon_Gold_VeryOld, 160_000);
            weights[CreatureConstants.Dragon_Gold_Ancient][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_Gold_Ancient, 160_000);
            weights[CreatureConstants.Dragon_Gold_Ancient][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_Gold_Ancient, 160_000);
            weights[CreatureConstants.Dragon_Gold_Ancient][CreatureConstants.Dragon_Gold_Ancient] =
                GetMultiplierFromAverage(CreatureConstants.Dragon_Gold_Ancient, 160_000);
            weights[CreatureConstants.Dragon_Gold_Wyrm][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_Gold_Wyrm, 1_280_000);
            weights[CreatureConstants.Dragon_Gold_Wyrm][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_Gold_Wyrm, 1_280_000);
            weights[CreatureConstants.Dragon_Gold_Wyrm][CreatureConstants.Dragon_Gold_Wyrm] = GetMultiplierFromAverage(CreatureConstants.Dragon_Gold_Wyrm, 1_280_000);
            weights[CreatureConstants.Dragon_Gold_GreatWyrm][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_Gold_GreatWyrm, 1_280_000);
            weights[CreatureConstants.Dragon_Gold_GreatWyrm][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_Gold_GreatWyrm, 1_280_000);
            weights[CreatureConstants.Dragon_Gold_GreatWyrm][CreatureConstants.Dragon_Gold_GreatWyrm] =
                GetMultiplierFromAverage(CreatureConstants.Dragon_Gold_GreatWyrm, 1_280_000);
            weights[CreatureConstants.Dragon_Green_Wyrmling][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_Green_Wyrmling, 40);
            weights[CreatureConstants.Dragon_Green_Wyrmling][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_Green_Wyrmling, 40);
            weights[CreatureConstants.Dragon_Green_Wyrmling][CreatureConstants.Dragon_Green_Wyrmling] =
                GetMultiplierFromAverage(CreatureConstants.Dragon_Green_Wyrmling, 40);
            weights[CreatureConstants.Dragon_Green_VeryYoung][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_Green_VeryYoung, 320);
            weights[CreatureConstants.Dragon_Green_VeryYoung][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_Green_VeryYoung, 320);
            weights[CreatureConstants.Dragon_Green_VeryYoung][CreatureConstants.Dragon_Green_VeryYoung] =
                GetMultiplierFromAverage(CreatureConstants.Dragon_Green_VeryYoung, 320);
            weights[CreatureConstants.Dragon_Green_Young][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_Green_Young, 320);
            weights[CreatureConstants.Dragon_Green_Young][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_Green_Young, 320);
            weights[CreatureConstants.Dragon_Green_Young][CreatureConstants.Dragon_Green_Young] = GetMultiplierFromAverage(CreatureConstants.Dragon_Green_Young, 320);
            weights[CreatureConstants.Dragon_Green_Juvenile][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_Green_Juvenile, 2500);
            weights[CreatureConstants.Dragon_Green_Juvenile][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_Green_Juvenile, 2500);
            weights[CreatureConstants.Dragon_Green_Juvenile][CreatureConstants.Dragon_Green_Juvenile] =
                GetMultiplierFromAverage(CreatureConstants.Dragon_Green_Juvenile, 2500);
            weights[CreatureConstants.Dragon_Green_YoungAdult][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_Green_YoungAdult, 2500);
            weights[CreatureConstants.Dragon_Green_YoungAdult][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_Green_YoungAdult, 2500);
            weights[CreatureConstants.Dragon_Green_YoungAdult][CreatureConstants.Dragon_Green_YoungAdult] =
                GetMultiplierFromAverage(CreatureConstants.Dragon_Green_YoungAdult, 2500);
            weights[CreatureConstants.Dragon_Green_Adult][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_Green_Adult, 20_000);
            weights[CreatureConstants.Dragon_Green_Adult][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_Green_Adult, 20_000);
            weights[CreatureConstants.Dragon_Green_Adult][CreatureConstants.Dragon_Green_Adult] = GetMultiplierFromAverage(CreatureConstants.Dragon_Green_Adult, 20_000);
            weights[CreatureConstants.Dragon_Green_MatureAdult][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_Green_MatureAdult, 20_000);
            weights[CreatureConstants.Dragon_Green_MatureAdult][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_Green_MatureAdult, 20_000);
            weights[CreatureConstants.Dragon_Green_MatureAdult][CreatureConstants.Dragon_Green_MatureAdult] =
                GetMultiplierFromAverage(CreatureConstants.Dragon_Green_MatureAdult, 20_000);
            weights[CreatureConstants.Dragon_Green_Old][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_Green_Old, 20_000);
            weights[CreatureConstants.Dragon_Green_Old][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_Green_Old, 20_000);
            weights[CreatureConstants.Dragon_Green_Old][CreatureConstants.Dragon_Green_Old] = GetMultiplierFromAverage(CreatureConstants.Dragon_Green_Old, 20_000);
            weights[CreatureConstants.Dragon_Green_VeryOld][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_Green_VeryOld, 20_000);
            weights[CreatureConstants.Dragon_Green_VeryOld][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_Green_VeryOld, 20_000);
            weights[CreatureConstants.Dragon_Green_VeryOld][CreatureConstants.Dragon_Green_VeryOld] =
                GetMultiplierFromAverage(CreatureConstants.Dragon_Green_VeryOld, 20_000);
            weights[CreatureConstants.Dragon_Green_Ancient][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_Green_Ancient, 160_000);
            weights[CreatureConstants.Dragon_Green_Ancient][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_Green_Ancient, 160_000);
            weights[CreatureConstants.Dragon_Green_Ancient][CreatureConstants.Dragon_Green_Ancient] =
                GetMultiplierFromAverage(CreatureConstants.Dragon_Green_Ancient, 160_000);
            weights[CreatureConstants.Dragon_Green_Wyrm][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_Green_Wyrm, 160_000);
            weights[CreatureConstants.Dragon_Green_Wyrm][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_Green_Wyrm, 160_000);
            weights[CreatureConstants.Dragon_Green_Wyrm][CreatureConstants.Dragon_Green_Wyrm] = GetMultiplierFromAverage(CreatureConstants.Dragon_Green_Wyrm, 160_000);
            weights[CreatureConstants.Dragon_Green_GreatWyrm][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_Green_GreatWyrm, 160_000);
            weights[CreatureConstants.Dragon_Green_GreatWyrm][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_Green_GreatWyrm, 160_000);
            weights[CreatureConstants.Dragon_Green_GreatWyrm][CreatureConstants.Dragon_Green_GreatWyrm] =
                GetMultiplierFromAverage(CreatureConstants.Dragon_Green_GreatWyrm, 160_000);
            weights[CreatureConstants.Dragon_Red_Wyrmling][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_Red_Wyrmling, 320);
            weights[CreatureConstants.Dragon_Red_Wyrmling][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_Red_Wyrmling, 320);
            weights[CreatureConstants.Dragon_Red_Wyrmling][CreatureConstants.Dragon_Red_Wyrmling] = GetMultiplierFromAverage(CreatureConstants.Dragon_Red_Wyrmling, 320);
            weights[CreatureConstants.Dragon_Red_VeryYoung][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_Red_VeryYoung, 2500);
            weights[CreatureConstants.Dragon_Red_VeryYoung][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_Red_VeryYoung, 2500);
            weights[CreatureConstants.Dragon_Red_VeryYoung][CreatureConstants.Dragon_Red_VeryYoung] =
                GetMultiplierFromAverage(CreatureConstants.Dragon_Red_VeryYoung, 2500);
            weights[CreatureConstants.Dragon_Red_Young][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_Red_Young, 2500);
            weights[CreatureConstants.Dragon_Red_Young][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_Red_Young, 2500);
            weights[CreatureConstants.Dragon_Red_Young][CreatureConstants.Dragon_Red_Young] = GetMultiplierFromAverage(CreatureConstants.Dragon_Red_Young, 2500);
            weights[CreatureConstants.Dragon_Red_Juvenile][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_Red_Juvenile, 2500);
            weights[CreatureConstants.Dragon_Red_Juvenile][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_Red_Juvenile, 2500);
            weights[CreatureConstants.Dragon_Red_Juvenile][CreatureConstants.Dragon_Red_Juvenile] = GetMultiplierFromAverage(CreatureConstants.Dragon_Red_Juvenile, 2500);
            weights[CreatureConstants.Dragon_Red_YoungAdult][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_Red_YoungAdult, 20_000);
            weights[CreatureConstants.Dragon_Red_YoungAdult][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_Red_YoungAdult, 20_000);
            weights[CreatureConstants.Dragon_Red_YoungAdult][CreatureConstants.Dragon_Red_YoungAdult] =
                GetMultiplierFromAverage(CreatureConstants.Dragon_Red_YoungAdult, 20_000);
            weights[CreatureConstants.Dragon_Red_Adult][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_Red_Adult, 20_000);
            weights[CreatureConstants.Dragon_Red_Adult][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_Red_Adult, 20_000);
            weights[CreatureConstants.Dragon_Red_Adult][CreatureConstants.Dragon_Red_Adult] = GetMultiplierFromAverage(CreatureConstants.Dragon_Red_Adult, 20_000);
            weights[CreatureConstants.Dragon_Red_MatureAdult][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_Red_MatureAdult, 20_000);
            weights[CreatureConstants.Dragon_Red_MatureAdult][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_Red_MatureAdult, 20_000);
            weights[CreatureConstants.Dragon_Red_MatureAdult][CreatureConstants.Dragon_Red_MatureAdult] =
                GetMultiplierFromAverage(CreatureConstants.Dragon_Red_MatureAdult, 20_000);
            weights[CreatureConstants.Dragon_Red_Old][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_Red_Old, 160_000);
            weights[CreatureConstants.Dragon_Red_Old][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_Red_Old, 160_000);
            weights[CreatureConstants.Dragon_Red_Old][CreatureConstants.Dragon_Red_Old] = GetMultiplierFromAverage(CreatureConstants.Dragon_Red_Old, 160_000);
            weights[CreatureConstants.Dragon_Red_VeryOld][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_Red_VeryOld, 160_000);
            weights[CreatureConstants.Dragon_Red_VeryOld][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_Red_VeryOld, 160_000);
            weights[CreatureConstants.Dragon_Red_VeryOld][CreatureConstants.Dragon_Red_VeryOld] = GetMultiplierFromAverage(CreatureConstants.Dragon_Red_VeryOld, 160_000);
            weights[CreatureConstants.Dragon_Red_Ancient][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_Red_Ancient, 160_000);
            weights[CreatureConstants.Dragon_Red_Ancient][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_Red_Ancient, 160_000);
            weights[CreatureConstants.Dragon_Red_Ancient][CreatureConstants.Dragon_Red_Ancient] = GetMultiplierFromAverage(CreatureConstants.Dragon_Red_Ancient, 160_000);
            weights[CreatureConstants.Dragon_Red_Wyrm][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_Red_Wyrm, 160_000);
            weights[CreatureConstants.Dragon_Red_Wyrm][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_Red_Wyrm, 160_000);
            weights[CreatureConstants.Dragon_Red_Wyrm][CreatureConstants.Dragon_Red_Wyrm] = GetMultiplierFromAverage(CreatureConstants.Dragon_Red_Wyrm, 160_000);
            weights[CreatureConstants.Dragon_Red_GreatWyrm][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_Red_GreatWyrm, 1_280_000);
            weights[CreatureConstants.Dragon_Red_GreatWyrm][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_Red_GreatWyrm, 1_280_000);
            weights[CreatureConstants.Dragon_Red_GreatWyrm][CreatureConstants.Dragon_Red_GreatWyrm] =
                GetMultiplierFromAverage(CreatureConstants.Dragon_Red_GreatWyrm, 1_280_000);
            weights[CreatureConstants.Dragon_Silver_Wyrmling][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_Silver_Wyrmling, 40);
            weights[CreatureConstants.Dragon_Silver_Wyrmling][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_Silver_Wyrmling, 40);
            weights[CreatureConstants.Dragon_Silver_Wyrmling][CreatureConstants.Dragon_Silver_Wyrmling] =
                GetMultiplierFromAverage(CreatureConstants.Dragon_Silver_Wyrmling, 40);
            weights[CreatureConstants.Dragon_Silver_VeryYoung][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_Silver_VeryYoung, 320);
            weights[CreatureConstants.Dragon_Silver_VeryYoung][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_Silver_VeryYoung, 320);
            weights[CreatureConstants.Dragon_Silver_VeryYoung][CreatureConstants.Dragon_Silver_VeryYoung] =
                GetMultiplierFromAverage(CreatureConstants.Dragon_Silver_VeryYoung, 320);
            weights[CreatureConstants.Dragon_Silver_Young][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_Silver_Young, 2500);
            weights[CreatureConstants.Dragon_Silver_Young][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_Silver_Young, 2500);
            weights[CreatureConstants.Dragon_Silver_Young][CreatureConstants.Dragon_Silver_Young] = GetMultiplierFromAverage(CreatureConstants.Dragon_Silver_Young, 2500);
            weights[CreatureConstants.Dragon_Silver_Juvenile][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_Silver_Juvenile, 2500);
            weights[CreatureConstants.Dragon_Silver_Juvenile][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_Silver_Juvenile, 2500);
            weights[CreatureConstants.Dragon_Silver_Juvenile][CreatureConstants.Dragon_Silver_Juvenile] =
                GetMultiplierFromAverage(CreatureConstants.Dragon_Silver_Juvenile, 2500);
            weights[CreatureConstants.Dragon_Silver_YoungAdult][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_Silver_YoungAdult, 2500);
            weights[CreatureConstants.Dragon_Silver_YoungAdult][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_Silver_YoungAdult, 2500);
            weights[CreatureConstants.Dragon_Silver_YoungAdult][CreatureConstants.Dragon_Silver_YoungAdult] =
                GetMultiplierFromAverage(CreatureConstants.Dragon_Silver_YoungAdult, 2500);
            weights[CreatureConstants.Dragon_Silver_Adult][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_Silver_Adult, 20_000);
            weights[CreatureConstants.Dragon_Silver_Adult][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_Silver_Adult, 20_000);
            weights[CreatureConstants.Dragon_Silver_Adult][CreatureConstants.Dragon_Silver_Adult] =
                GetMultiplierFromAverage(CreatureConstants.Dragon_Silver_Adult, 20_000);
            weights[CreatureConstants.Dragon_Silver_MatureAdult][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_Silver_MatureAdult, 20_000);
            weights[CreatureConstants.Dragon_Silver_MatureAdult][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_Silver_MatureAdult, 20_000);
            weights[CreatureConstants.Dragon_Silver_MatureAdult][CreatureConstants.Dragon_Silver_MatureAdult] =
                GetMultiplierFromAverage(CreatureConstants.Dragon_Silver_MatureAdult, 20_000);
            weights[CreatureConstants.Dragon_Silver_Old][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_Silver_Old, 20_000);
            weights[CreatureConstants.Dragon_Silver_Old][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_Silver_Old, 20_000);
            weights[CreatureConstants.Dragon_Silver_Old][CreatureConstants.Dragon_Silver_Old] = GetMultiplierFromAverage(CreatureConstants.Dragon_Silver_Old, 20_000);
            weights[CreatureConstants.Dragon_Silver_VeryOld][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_Silver_VeryOld, 20_000);
            weights[CreatureConstants.Dragon_Silver_VeryOld][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_Silver_VeryOld, 20_000);
            weights[CreatureConstants.Dragon_Silver_VeryOld][CreatureConstants.Dragon_Silver_VeryOld] =
                GetMultiplierFromAverage(CreatureConstants.Dragon_Silver_VeryOld, 20_000);
            weights[CreatureConstants.Dragon_Silver_Ancient][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_Silver_Ancient, 160_000);
            weights[CreatureConstants.Dragon_Silver_Ancient][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_Silver_Ancient, 160_000);
            weights[CreatureConstants.Dragon_Silver_Ancient][CreatureConstants.Dragon_Silver_Ancient] =
                GetMultiplierFromAverage(CreatureConstants.Dragon_Silver_Ancient, 160_000);
            weights[CreatureConstants.Dragon_Silver_Wyrm][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_Silver_Wyrm, 160_000);
            weights[CreatureConstants.Dragon_Silver_Wyrm][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_Silver_Wyrm, 160_000);
            weights[CreatureConstants.Dragon_Silver_Wyrm][CreatureConstants.Dragon_Silver_Wyrm] = GetMultiplierFromAverage(CreatureConstants.Dragon_Silver_Wyrm, 160_000);
            weights[CreatureConstants.Dragon_Silver_GreatWyrm][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_Silver_GreatWyrm, 1_280_000);
            weights[CreatureConstants.Dragon_Silver_GreatWyrm][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_Silver_GreatWyrm, 1_280_000);
            weights[CreatureConstants.Dragon_Silver_GreatWyrm][CreatureConstants.Dragon_Silver_GreatWyrm] =
                GetMultiplierFromAverage(CreatureConstants.Dragon_Silver_GreatWyrm, 1_280_000);
            weights[CreatureConstants.Dragon_White_Wyrmling][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_White_Wyrmling, 5);
            weights[CreatureConstants.Dragon_White_Wyrmling][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_White_Wyrmling, 5);
            weights[CreatureConstants.Dragon_White_Wyrmling][CreatureConstants.Dragon_White_Wyrmling] =
                GetMultiplierFromAverage(CreatureConstants.Dragon_White_Wyrmling, 5);
            weights[CreatureConstants.Dragon_White_VeryYoung][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_White_VeryYoung, 40);
            weights[CreatureConstants.Dragon_White_VeryYoung][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_White_VeryYoung, 40);
            weights[CreatureConstants.Dragon_White_VeryYoung][CreatureConstants.Dragon_White_VeryYoung] =
                GetMultiplierFromAverage(CreatureConstants.Dragon_White_VeryYoung, 40);
            weights[CreatureConstants.Dragon_White_Young][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_White_Young, 320);
            weights[CreatureConstants.Dragon_White_Young][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_White_Young, 320);
            weights[CreatureConstants.Dragon_White_Young][CreatureConstants.Dragon_White_Young] = GetMultiplierFromAverage(CreatureConstants.Dragon_White_Young, 320);
            weights[CreatureConstants.Dragon_White_Juvenile][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_White_Juvenile, 320);
            weights[CreatureConstants.Dragon_White_Juvenile][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_White_Juvenile, 320);
            weights[CreatureConstants.Dragon_White_Juvenile][CreatureConstants.Dragon_White_Juvenile] =
                GetMultiplierFromAverage(CreatureConstants.Dragon_White_Juvenile, 320);
            weights[CreatureConstants.Dragon_White_YoungAdult][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_White_YoungAdult, 2500);
            weights[CreatureConstants.Dragon_White_YoungAdult][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_White_YoungAdult, 2500);
            weights[CreatureConstants.Dragon_White_YoungAdult][CreatureConstants.Dragon_White_YoungAdult] =
                GetMultiplierFromAverage(CreatureConstants.Dragon_White_YoungAdult, 2500);
            weights[CreatureConstants.Dragon_White_Adult][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_White_Adult, 2500);
            weights[CreatureConstants.Dragon_White_Adult][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_White_Adult, 2500);
            weights[CreatureConstants.Dragon_White_Adult][CreatureConstants.Dragon_White_Adult] = GetMultiplierFromAverage(CreatureConstants.Dragon_White_Adult, 2500);
            weights[CreatureConstants.Dragon_White_MatureAdult][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_White_MatureAdult, 20_000);
            weights[CreatureConstants.Dragon_White_MatureAdult][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_White_MatureAdult, 20_000);
            weights[CreatureConstants.Dragon_White_MatureAdult][CreatureConstants.Dragon_White_MatureAdult] =
                GetMultiplierFromAverage(CreatureConstants.Dragon_White_MatureAdult, 20_000);
            weights[CreatureConstants.Dragon_White_Old][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_White_Old, 20_000);
            weights[CreatureConstants.Dragon_White_Old][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_White_Old, 20_000);
            weights[CreatureConstants.Dragon_White_Old][CreatureConstants.Dragon_White_Old] = GetMultiplierFromAverage(CreatureConstants.Dragon_White_Old, 20_000);
            weights[CreatureConstants.Dragon_White_VeryOld][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_White_VeryOld, 20_000);
            weights[CreatureConstants.Dragon_White_VeryOld][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_White_VeryOld, 20_000);
            weights[CreatureConstants.Dragon_White_VeryOld][CreatureConstants.Dragon_White_VeryOld] =
                GetMultiplierFromAverage(CreatureConstants.Dragon_White_VeryOld, 20_000);
            weights[CreatureConstants.Dragon_White_Ancient][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_White_Ancient, 20_000);
            weights[CreatureConstants.Dragon_White_Ancient][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_White_Ancient, 20_000);
            weights[CreatureConstants.Dragon_White_Ancient][CreatureConstants.Dragon_White_Ancient] =
                GetMultiplierFromAverage(CreatureConstants.Dragon_White_Ancient, 20_000);
            weights[CreatureConstants.Dragon_White_Wyrm][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_White_Wyrm, 160_000);
            weights[CreatureConstants.Dragon_White_Wyrm][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_White_Wyrm, 160_000);
            weights[CreatureConstants.Dragon_White_Wyrm][CreatureConstants.Dragon_White_Wyrm] = GetMultiplierFromAverage(CreatureConstants.Dragon_White_Wyrm, 160_000);
            weights[CreatureConstants.Dragon_White_GreatWyrm][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragon_White_GreatWyrm, 160_000);
            weights[CreatureConstants.Dragon_White_GreatWyrm][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragon_White_GreatWyrm, 160_000);
            weights[CreatureConstants.Dragon_White_GreatWyrm][CreatureConstants.Dragon_White_GreatWyrm] =
                GetMultiplierFromAverage(CreatureConstants.Dragon_White_GreatWyrm, 160_000);
            //Source: https://forgottenrealms.fandom.com/wiki/Dragon_turtle
            weights[CreatureConstants.DragonTurtle][GenderConstants.Female] = GetBaseFromRange(CreatureConstants.DragonTurtle, 8000, 32_000);
            weights[CreatureConstants.DragonTurtle][GenderConstants.Male] = GetBaseFromRange(CreatureConstants.DragonTurtle, 8000, 32_000);
            weights[CreatureConstants.DragonTurtle][CreatureConstants.DragonTurtle] = GetMultiplierFromRange(CreatureConstants.DragonTurtle, 8000, 32_000);
            //Source: https://forgottenrealms.fandom.com/wiki/Dragonne
            weights[CreatureConstants.Dragonne][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Dragonne, 700);
            weights[CreatureConstants.Dragonne][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Dragonne, 700);
            weights[CreatureConstants.Dragonne][CreatureConstants.Dragonne] = GetMultiplierFromAverage(CreatureConstants.Dragonne, 700);
            //Source: https://forgottenrealms.fandom.com/wiki/Dretch
            weights[CreatureConstants.Dretch][GenderConstants.Agender] = GetBaseFromAverage(CreatureConstants.Dretch, 60);
            weights[CreatureConstants.Dretch][CreatureConstants.Dretch] = GetMultiplierFromAverage(CreatureConstants.Dretch, 60);
            //Source: https://www.worldanvil.com/w/faerun-tatortotzke/a/dryad-species
            weights[CreatureConstants.Dryad][GenderConstants.Agender] = GetBaseFromAverage(CreatureConstants.Dryad, 150);
            weights[CreatureConstants.Dryad][CreatureConstants.Dryad] = GetMultiplierFromAverage(CreatureConstants.Dryad, 150);
            //Source: https://www.d20srd.org/srd/description.htm#vitalStatistics
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
            //Source: https://www.d20srd.org/srd/monsters/elemental.htm
            weights[CreatureConstants.Elemental_Air_Small][GenderConstants.Agender] = GetBaseFromAverage(CreatureConstants.Elemental_Air_Small, 1);
            weights[CreatureConstants.Elemental_Air_Small][CreatureConstants.Elemental_Air_Small] = GetMultiplierFromAverage(CreatureConstants.Elemental_Air_Small, 1);
            weights[CreatureConstants.Elemental_Air_Medium][GenderConstants.Agender] = GetBaseFromAverage(CreatureConstants.Elemental_Air_Medium, 2);
            weights[CreatureConstants.Elemental_Air_Medium][CreatureConstants.Elemental_Air_Small] = GetMultiplierFromAverage(CreatureConstants.Elemental_Air_Medium, 2);
            weights[CreatureConstants.Elemental_Air_Large][GenderConstants.Agender] = GetBaseFromAverage(CreatureConstants.Elemental_Air_Large, 4);
            weights[CreatureConstants.Elemental_Air_Large][CreatureConstants.Elemental_Air_Small] = GetMultiplierFromAverage(CreatureConstants.Elemental_Air_Large, 4);
            weights[CreatureConstants.Elemental_Air_Huge][GenderConstants.Agender] = GetBaseFromAverage(CreatureConstants.Elemental_Air_Huge, 8);
            weights[CreatureConstants.Elemental_Air_Huge][CreatureConstants.Elemental_Air_Small] = GetMultiplierFromAverage(CreatureConstants.Elemental_Air_Huge, 8);
            weights[CreatureConstants.Elemental_Air_Greater][GenderConstants.Agender] = GetBaseFromAverage(CreatureConstants.Elemental_Air_Greater, 10);
            weights[CreatureConstants.Elemental_Air_Greater][CreatureConstants.Elemental_Air_Small] = GetMultiplierFromAverage(CreatureConstants.Elemental_Air_Greater, 10);
            weights[CreatureConstants.Elemental_Air_Elder][GenderConstants.Agender] = GetBaseFromAverage(CreatureConstants.Elemental_Air_Elder, 12);
            weights[CreatureConstants.Elemental_Air_Elder][CreatureConstants.Elemental_Air_Small] = GetMultiplierFromAverage(CreatureConstants.Elemental_Air_Elder, 12);
            weights[CreatureConstants.Elemental_Earth_Small][GenderConstants.Agender] = GetBaseFromAverage(CreatureConstants.Elemental_Earth_Small, 80);
            weights[CreatureConstants.Elemental_Earth_Small][CreatureConstants.Elemental_Air_Small] = GetMultiplierFromAverage(CreatureConstants.Elemental_Earth_Small, 80);
            weights[CreatureConstants.Elemental_Earth_Medium][GenderConstants.Agender] = GetBaseFromAverage(CreatureConstants.Elemental_Earth_Medium, 750);
            weights[CreatureConstants.Elemental_Earth_Medium][CreatureConstants.Elemental_Air_Small] = GetMultiplierFromAverage(CreatureConstants.Elemental_Earth_Medium, 750);
            weights[CreatureConstants.Elemental_Earth_Large][GenderConstants.Agender] = GetBaseFromAverage(CreatureConstants.Elemental_Earth_Large, 6000);
            weights[CreatureConstants.Elemental_Earth_Large][CreatureConstants.Elemental_Air_Small] = GetMultiplierFromAverage(CreatureConstants.Elemental_Earth_Large, 6000);
            weights[CreatureConstants.Elemental_Earth_Huge][GenderConstants.Agender] = GetBaseFromAverage(CreatureConstants.Elemental_Earth_Huge, 48_000);
            weights[CreatureConstants.Elemental_Earth_Huge][CreatureConstants.Elemental_Air_Small] = GetMultiplierFromAverage(CreatureConstants.Elemental_Earth_Huge, 48_000);
            weights[CreatureConstants.Elemental_Earth_Greater][GenderConstants.Agender] = GetBaseFromAverage(CreatureConstants.Elemental_Earth_Greater, 54_000);
            weights[CreatureConstants.Elemental_Earth_Greater][CreatureConstants.Elemental_Air_Small] = GetMultiplierFromAverage(CreatureConstants.Elemental_Earth_Greater, 54_000);
            weights[CreatureConstants.Elemental_Earth_Elder][GenderConstants.Agender] = GetBaseFromAverage(CreatureConstants.Elemental_Earth_Elder, 60_000);
            weights[CreatureConstants.Elemental_Earth_Elder][CreatureConstants.Elemental_Air_Small] = GetMultiplierFromAverage(CreatureConstants.Elemental_Earth_Elder, 60_000);
            weights[CreatureConstants.Elemental_Fire_Small][GenderConstants.Agender] = GetBaseFromAverage(CreatureConstants.Elemental_Fire_Small, 1);
            weights[CreatureConstants.Elemental_Fire_Small][CreatureConstants.Elemental_Air_Small] = GetMultiplierFromAverage(CreatureConstants.Elemental_Fire_Small, 1);
            weights[CreatureConstants.Elemental_Fire_Medium][GenderConstants.Agender] = GetBaseFromAverage(CreatureConstants.Elemental_Fire_Medium, 2);
            weights[CreatureConstants.Elemental_Fire_Medium][CreatureConstants.Elemental_Air_Small] = GetMultiplierFromAverage(CreatureConstants.Elemental_Fire_Medium, 2);
            weights[CreatureConstants.Elemental_Fire_Large][GenderConstants.Agender] = GetBaseFromAverage(CreatureConstants.Elemental_Fire_Large, 4);
            weights[CreatureConstants.Elemental_Fire_Large][CreatureConstants.Elemental_Air_Small] = GetMultiplierFromAverage(CreatureConstants.Elemental_Fire_Large, 4);
            weights[CreatureConstants.Elemental_Fire_Huge][GenderConstants.Agender] = GetBaseFromAverage(CreatureConstants.Elemental_Fire_Huge, 8);
            weights[CreatureConstants.Elemental_Fire_Huge][CreatureConstants.Elemental_Air_Small] = GetMultiplierFromAverage(CreatureConstants.Elemental_Fire_Huge, 8);
            weights[CreatureConstants.Elemental_Fire_Greater][GenderConstants.Agender] = GetBaseFromAverage(CreatureConstants.Elemental_Fire_Greater, 10);
            weights[CreatureConstants.Elemental_Fire_Greater][CreatureConstants.Elemental_Air_Small] = GetMultiplierFromAverage(CreatureConstants.Elemental_Fire_Greater, 10);
            weights[CreatureConstants.Elemental_Fire_Elder][GenderConstants.Agender] = GetBaseFromAverage(CreatureConstants.Elemental_Fire_Elder, 12);
            weights[CreatureConstants.Elemental_Fire_Elder][CreatureConstants.Elemental_Air_Small] = GetMultiplierFromAverage(CreatureConstants.Elemental_Fire_Elder, 12);
            weights[CreatureConstants.Elemental_Water_Small][GenderConstants.Agender] = GetBaseFromAverage(CreatureConstants.Elemental_Water_Small, 34);
            weights[CreatureConstants.Elemental_Water_Small][CreatureConstants.Elemental_Air_Small] = GetMultiplierFromAverage(CreatureConstants.Elemental_Water_Small, 34);
            weights[CreatureConstants.Elemental_Water_Medium][GenderConstants.Agender] = GetBaseFromAverage(CreatureConstants.Elemental_Water_Medium, 280);
            weights[CreatureConstants.Elemental_Water_Medium][CreatureConstants.Elemental_Air_Small] = GetMultiplierFromAverage(CreatureConstants.Elemental_Water_Medium, 280);
            weights[CreatureConstants.Elemental_Water_Large][GenderConstants.Agender] = GetBaseFromAverage(CreatureConstants.Elemental_Water_Large, 2250);
            weights[CreatureConstants.Elemental_Water_Large][CreatureConstants.Elemental_Air_Small] = GetMultiplierFromAverage(CreatureConstants.Elemental_Water_Large, 2250);
            weights[CreatureConstants.Elemental_Water_Huge][GenderConstants.Agender] = GetBaseFromAverage(CreatureConstants.Elemental_Water_Huge, 18_000);
            weights[CreatureConstants.Elemental_Water_Huge][CreatureConstants.Elemental_Air_Small] = GetMultiplierFromAverage(CreatureConstants.Elemental_Water_Huge, 18_000);
            weights[CreatureConstants.Elemental_Water_Greater][GenderConstants.Agender] = GetBaseFromAverage(CreatureConstants.Elemental_Water_Greater, 21_000);
            weights[CreatureConstants.Elemental_Water_Greater][CreatureConstants.Elemental_Air_Small] = GetMultiplierFromAverage(CreatureConstants.Elemental_Water_Greater, 21_000);
            weights[CreatureConstants.Elemental_Water_Elder][GenderConstants.Agender] = GetBaseFromAverage(CreatureConstants.Elemental_Water_Elder, 24_000);
            weights[CreatureConstants.Elemental_Water_Elder][CreatureConstants.Elemental_Air_Small] = GetMultiplierFromAverage(CreatureConstants.Elemental_Water_Elder, 24_000);
            //Source: https://www.d20srd.org/srd/description.htm#vitalStatistics
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
            //Source: https://www.d20srd.org/srd/monsters/devil.htm#erinyes
            weights[CreatureConstants.Erinyes][GenderConstants.Agender] = GetBaseFromAverage(CreatureConstants.Erinyes, 150);
            weights[CreatureConstants.Erinyes][CreatureConstants.Erinyes] = GetMultiplierFromAverage(CreatureConstants.Erinyes, 150);
            //Source: https://forgottenrealms.fandom.com/wiki/Ettin
            weights[CreatureConstants.Ettin][GenderConstants.Female] = GetBaseFromRange(CreatureConstants.Ettin, 930, 5200);
            weights[CreatureConstants.Ettin][GenderConstants.Male] = GetBaseFromRange(CreatureConstants.Ettin, 930, 5200);
            weights[CreatureConstants.Ettin][CreatureConstants.Ettin] = GetMultiplierFromRange(CreatureConstants.Ettin, 930, 5200);
            //Source: https://forgottenrealms.fandom.com/wiki/Cloud_giant
            weights[CreatureConstants.Giant_Cloud][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Giant_Cloud, 11_500);
            weights[CreatureConstants.Giant_Cloud][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Giant_Cloud, 11_500);
            weights[CreatureConstants.Giant_Cloud][CreatureConstants.Giant_Cloud] = GetMultiplierFromAverage(CreatureConstants.Giant_Cloud, 11_500);
            //Source: https://forgottenrealms.fandom.com/wiki/Hill_giant
            weights[CreatureConstants.Giant_Hill][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Giant_Hill, 4500);
            weights[CreatureConstants.Giant_Hill][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Giant_Hill, 4500);
            weights[CreatureConstants.Giant_Hill][CreatureConstants.Giant_Hill] = GetMultiplierFromAverage(CreatureConstants.Giant_Hill, 4500);
            //Source: http://people.wku.edu/charles.plemons/ad&d/races/height.html 3d10 = High Elf, so 1d6
            weights[CreatureConstants.Githyanki][GenderConstants.Female] = "90";
            weights[CreatureConstants.Githyanki][GenderConstants.Male] = "120";
            weights[CreatureConstants.Githyanki][CreatureConstants.Githyanki] = "1d6";
            //Source: http://people.wku.edu/charles.plemons/ad&d/races/height.html
            weights[CreatureConstants.Githzerai][GenderConstants.Female] = "90";
            weights[CreatureConstants.Githzerai][GenderConstants.Male] = "120";
            weights[CreatureConstants.Githzerai][CreatureConstants.Githzerai] = "1d6";
            //Source: https://forgottenrealms.fandom.com/wiki/Glabrezu
            weights[CreatureConstants.Glabrezu][GenderConstants.Agender] = GetBaseFromAverage(CreatureConstants.Glabrezu, 5500);
            weights[CreatureConstants.Glabrezu][CreatureConstants.Glabrezu] = GetMultiplierFromAverage(CreatureConstants.Glabrezu, 5500);
            //Source: http://people.wku.edu/charles.plemons/ad&d/races/height.html 4d10 = Hill Dwarf, so 2d6
            weights[CreatureConstants.Gnoll][GenderConstants.Female] = "160";
            weights[CreatureConstants.Gnoll][GenderConstants.Male] = "180";
            weights[CreatureConstants.Gnoll][CreatureConstants.Bugbear] = "2d6";
            //Source: https://www.d20srd.org/srd/description.htm#vitalStatistics
            weights[CreatureConstants.Gnome_Forest][GenderConstants.Female] = "35";
            weights[CreatureConstants.Gnome_Forest][GenderConstants.Male] = "40";
            weights[CreatureConstants.Gnome_Forest][CreatureConstants.Gnome_Forest] = "1"; //x1
            weights[CreatureConstants.Gnome_Rock][GenderConstants.Female] = "35";
            weights[CreatureConstants.Gnome_Rock][GenderConstants.Male] = "40";
            weights[CreatureConstants.Gnome_Rock][CreatureConstants.Gnome_Rock] = "1"; //x1
            weights[CreatureConstants.Gnome_Svirfneblin][GenderConstants.Female] = "1";
            weights[CreatureConstants.Gnome_Svirfneblin][GenderConstants.Male] = "1";
            weights[CreatureConstants.Gnome_Svirfneblin][CreatureConstants.Gnome_Svirfneblin] = "1"; //x1
            //Source: http://people.wku.edu/charles.plemons/ad&d/races/height.html 5d4 = Gnome, so 1
            weights[CreatureConstants.Goblin][GenderConstants.Female] = "68";
            weights[CreatureConstants.Goblin][GenderConstants.Male] = "72";
            weights[CreatureConstants.Goblin][CreatureConstants.Goblin] = "1"; //x1
            weights[CreatureConstants.GreenHag][GenderConstants.Female] = "85";
            weights[CreatureConstants.GreenHag][CreatureConstants.GreenHag] = "2d4";
            weights[CreatureConstants.Grig][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Grig, 1); //Tiny
            weights[CreatureConstants.Grig][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Grig, 1);
            weights[CreatureConstants.Grig][CreatureConstants.Grig] = GetMultiplierFromAverage(CreatureConstants.Grig, 1);
            weights[CreatureConstants.Grig_WithFiddle][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Grig_WithFiddle, 1); //Tiny
            weights[CreatureConstants.Grig_WithFiddle][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Grig_WithFiddle, 1);
            weights[CreatureConstants.Grig_WithFiddle][CreatureConstants.Grig_WithFiddle] = GetMultiplierFromAverage(CreatureConstants.Grig_WithFiddle, 1);
            weights[CreatureConstants.Halfling_Deep][GenderConstants.Female] = "25";
            weights[CreatureConstants.Halfling_Deep][GenderConstants.Male] = "30";
            weights[CreatureConstants.Halfling_Deep][CreatureConstants.Halfling_Deep] = "1"; //x1
            weights[CreatureConstants.Halfling_Lightfoot][GenderConstants.Female] = "25";
            weights[CreatureConstants.Halfling_Lightfoot][GenderConstants.Male] = "30";
            weights[CreatureConstants.Halfling_Lightfoot][CreatureConstants.Halfling_Lightfoot] = "1"; //x1
            weights[CreatureConstants.Halfling_Tallfellow][GenderConstants.Female] = "25";
            weights[CreatureConstants.Halfling_Tallfellow][GenderConstants.Male] = "30";
            weights[CreatureConstants.Halfling_Tallfellow][CreatureConstants.Halfling_Tallfellow] = "1"; //x1
            weights[CreatureConstants.Hellcat_Bezekira][GenderConstants.Agender] = GetBaseFromAverage(CreatureConstants.Hellcat_Bezekira, 900);
            weights[CreatureConstants.Hellcat_Bezekira][CreatureConstants.Hellcat_Bezekira] = GetMultiplierFromAverage(CreatureConstants.Hellcat_Bezekira, 900);
            weights[CreatureConstants.Hezrou][GenderConstants.Agender] = GetBaseFromAverage(CreatureConstants.Hezrou, 750);
            weights[CreatureConstants.Hezrou][CreatureConstants.Hezrou] = GetMultiplierFromAverage(CreatureConstants.Hezrou, 750);
            weights[CreatureConstants.Hieracosphinx][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Hieracosphinx, 800);
            weights[CreatureConstants.Hieracosphinx][CreatureConstants.Hieracosphinx] = GetMultiplierFromAverage(CreatureConstants.Hieracosphinx, 800);
            weights[CreatureConstants.Hobgoblin][GenderConstants.Female] = "145";
            weights[CreatureConstants.Hobgoblin][GenderConstants.Male] = "165";
            weights[CreatureConstants.Hobgoblin][CreatureConstants.Hobgoblin] = "2d4"; //x5
            weights[CreatureConstants.HornedDevil_Cornugon][GenderConstants.Agender] = GetBaseFromAverage(CreatureConstants.HornedDevil_Cornugon, 600);
            weights[CreatureConstants.HornedDevil_Cornugon][CreatureConstants.HornedDevil_Cornugon] = GetMultiplierFromAverage(CreatureConstants.HornedDevil_Cornugon, 600);
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
            weights[CreatureConstants.IceDevil_Gelugon][GenderConstants.Agender] = GetBaseFromAverage(CreatureConstants.IceDevil_Gelugon, 700);
            weights[CreatureConstants.IceDevil_Gelugon][CreatureConstants.IceDevil_Gelugon] = GetMultiplierFromAverage(CreatureConstants.IceDevil_Gelugon, 700);
            weights[CreatureConstants.Imp][GenderConstants.Agender] = GetBaseFromAverage(CreatureConstants.Imp, 8);
            weights[CreatureConstants.Imp][CreatureConstants.Imp] = GetMultiplierFromAverage(CreatureConstants.Imp, 8);
            weights[CreatureConstants.Kobold][GenderConstants.Female] = "20";
            weights[CreatureConstants.Kobold][GenderConstants.Male] = "25";
            weights[CreatureConstants.Kobold][CreatureConstants.Kobold] = "1"; //x1
            weights[CreatureConstants.Lemure][GenderConstants.Agender] = GetBaseFromAverage(CreatureConstants.Lemure, 100);
            weights[CreatureConstants.Lemure][CreatureConstants.Lemure] = GetMultiplierFromAverage(CreatureConstants.Lemure, 100);
            weights[CreatureConstants.Leonal][GenderConstants.Female] = "130";
            weights[CreatureConstants.Leonal][GenderConstants.Male] = "130";
            weights[CreatureConstants.Leonal][CreatureConstants.Leonal] = "2d4";
            weights[CreatureConstants.Lizardfolk][GenderConstants.Female] = "150";
            weights[CreatureConstants.Lizardfolk][GenderConstants.Male] = "150";
            weights[CreatureConstants.Lizardfolk][CreatureConstants.Lizardfolk] = RollHelper.GetRollWithFewestDice(150, 200, 250);
            weights[CreatureConstants.Locathah][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Locathah, 175);
            weights[CreatureConstants.Locathah][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Locathah, 175);
            weights[CreatureConstants.Locathah][CreatureConstants.Locathah] = GetMultiplierFromAverage(CreatureConstants.Locathah, 175);
            weights[CreatureConstants.Marilith][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Marilith, 2 * 2000);
            weights[CreatureConstants.Marilith][CreatureConstants.Marilith] = GetMultiplierFromAverage(CreatureConstants.Marilith, 2 * 2000);
            weights[CreatureConstants.Mephit_Air][GenderConstants.Agender] = GetBaseFromAverage(CreatureConstants.Mephit_Air, 1);
            weights[CreatureConstants.Mephit_Air][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Mephit_Air, 1);
            weights[CreatureConstants.Mephit_Air][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Mephit_Air, 1);
            weights[CreatureConstants.Mephit_Air][CreatureConstants.Mephit_Air] = GetMultiplierFromAverage(CreatureConstants.Mephit_Air, 1);
            weights[CreatureConstants.Mephit_Dust][GenderConstants.Agender] = GetBaseFromAverage(CreatureConstants.Mephit_Dust, 2);
            weights[CreatureConstants.Mephit_Dust][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Mephit_Dust, 2);
            weights[CreatureConstants.Mephit_Dust][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Mephit_Dust, 2);
            weights[CreatureConstants.Mephit_Dust][CreatureConstants.Mephit_Dust] = GetMultiplierFromAverage(CreatureConstants.Mephit_Dust, 2);
            weights[CreatureConstants.Mephit_Earth][GenderConstants.Agender] = GetBaseFromAverage(CreatureConstants.Mephit_Earth, 80);
            weights[CreatureConstants.Mephit_Earth][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Mephit_Earth, 80);
            weights[CreatureConstants.Mephit_Earth][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Mephit_Earth, 80);
            weights[CreatureConstants.Mephit_Earth][CreatureConstants.Mephit_Earth] = GetMultiplierFromAverage(CreatureConstants.Mephit_Earth, 80);
            weights[CreatureConstants.Mephit_Fire][GenderConstants.Agender] = GetBaseFromAverage(CreatureConstants.Mephit_Fire, 1);
            weights[CreatureConstants.Mephit_Fire][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Mephit_Fire, 1);
            weights[CreatureConstants.Mephit_Fire][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Mephit_Fire, 1);
            weights[CreatureConstants.Mephit_Fire][CreatureConstants.Mephit_Fire] = GetMultiplierFromAverage(CreatureConstants.Mephit_Fire, 1);
            weights[CreatureConstants.Mephit_Ice][GenderConstants.Agender] = GetBaseFromAverage(CreatureConstants.Mephit_Ice, 30);
            weights[CreatureConstants.Mephit_Ice][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Mephit_Ice, 30);
            weights[CreatureConstants.Mephit_Ice][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Mephit_Ice, 30);
            weights[CreatureConstants.Mephit_Ice][CreatureConstants.Mephit_Ice] = GetMultiplierFromAverage(CreatureConstants.Mephit_Ice, 30);
            weights[CreatureConstants.Mephit_Magma][GenderConstants.Agender] = GetBaseFromAverage(CreatureConstants.Mephit_Magma, 60);
            weights[CreatureConstants.Mephit_Magma][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Mephit_Magma, 60);
            weights[CreatureConstants.Mephit_Magma][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Mephit_Magma, 60);
            weights[CreatureConstants.Mephit_Magma][CreatureConstants.Mephit_Magma] = GetMultiplierFromAverage(CreatureConstants.Mephit_Magma, 60);
            weights[CreatureConstants.Mephit_Ooze][GenderConstants.Agender] = GetBaseFromAverage(CreatureConstants.Mephit_Ooze, 30);
            weights[CreatureConstants.Mephit_Ooze][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Mephit_Ooze, 30);
            weights[CreatureConstants.Mephit_Ooze][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Mephit_Ooze, 30);
            weights[CreatureConstants.Mephit_Ooze][CreatureConstants.Mephit_Ooze] = GetMultiplierFromAverage(CreatureConstants.Mephit_Ooze, 30);
            weights[CreatureConstants.Mephit_Salt][GenderConstants.Agender] = GetBaseFromAverage(CreatureConstants.Mephit_Salt, 80);
            weights[CreatureConstants.Mephit_Salt][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Mephit_Salt, 80);
            weights[CreatureConstants.Mephit_Salt][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Mephit_Salt, 80);
            weights[CreatureConstants.Mephit_Salt][CreatureConstants.Mephit_Salt] = GetMultiplierFromAverage(CreatureConstants.Mephit_Salt, 80);
            weights[CreatureConstants.Mephit_Steam][GenderConstants.Agender] = GetBaseFromAverage(CreatureConstants.Mephit_Steam, 2);
            weights[CreatureConstants.Mephit_Steam][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Mephit_Steam, 2);
            weights[CreatureConstants.Mephit_Steam][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Mephit_Steam, 2);
            weights[CreatureConstants.Mephit_Steam][CreatureConstants.Mephit_Steam] = GetMultiplierFromAverage(CreatureConstants.Mephit_Steam, 2);
            weights[CreatureConstants.Mephit_Water][GenderConstants.Agender] = GetBaseFromAverage(CreatureConstants.Mephit_Water, 30);
            weights[CreatureConstants.Mephit_Water][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Mephit_Water, 30);
            weights[CreatureConstants.Mephit_Water][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Mephit_Water, 30);
            weights[CreatureConstants.Mephit_Water][CreatureConstants.Mephit_Water] = GetMultiplierFromAverage(CreatureConstants.Mephit_Water, 30);
            weights[CreatureConstants.Merfolk][GenderConstants.Female] = "135";
            weights[CreatureConstants.Merfolk][GenderConstants.Male] = "145";
            weights[CreatureConstants.Merfolk][CreatureConstants.Merfolk] = "2d4"; //x5
            weights[CreatureConstants.Minotaur][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Minotaur, 700);
            weights[CreatureConstants.Minotaur][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Minotaur, 700);
            weights[CreatureConstants.Minotaur][CreatureConstants.Locathah] = GetMultiplierFromAverage(CreatureConstants.Minotaur, 700);
            weights[CreatureConstants.Nalfeshnee][GenderConstants.Agender] = GetBaseFromAverage(CreatureConstants.Nalfeshnee, 8000);
            weights[CreatureConstants.Nalfeshnee][CreatureConstants.Nalfeshnee] = GetMultiplierFromAverage(CreatureConstants.Nalfeshnee, 8000);
            weights[CreatureConstants.Ogre][GenderConstants.Female] = "554";
            weights[CreatureConstants.Ogre][GenderConstants.Male] = "599";
            weights[CreatureConstants.Ogre][CreatureConstants.Ogre] = "1d10";
            weights[CreatureConstants.Ogre_Merrow][GenderConstants.Female] = "554";
            weights[CreatureConstants.Ogre_Merrow][GenderConstants.Male] = "599";
            weights[CreatureConstants.Ogre_Merrow][CreatureConstants.Ogre_Merrow] = "1d10";
            weights[CreatureConstants.OgreMage][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.OgreMage, 700);
            weights[CreatureConstants.OgreMage][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.OgreMage, 700);
            weights[CreatureConstants.OgreMage][CreatureConstants.OgreMage] = GetMultiplierFromAverage(CreatureConstants.OgreMage, 700);
            weights[CreatureConstants.Orc][GenderConstants.Female] = "120";
            weights[CreatureConstants.Orc][GenderConstants.Male] = "160";
            weights[CreatureConstants.Orc][CreatureConstants.Orc_Half] = "2d6"; //x7
            weights[CreatureConstants.Orc_Half][GenderConstants.Female] = "110";
            weights[CreatureConstants.Orc_Half][GenderConstants.Male] = "150";
            weights[CreatureConstants.Orc_Half][CreatureConstants.Orc_Half] = "2d6"; //x7
            weights[CreatureConstants.PitFiend][GenderConstants.Agender] = GetBaseFromAverage(CreatureConstants.PitFiend, 800);
            weights[CreatureConstants.PitFiend][CreatureConstants.PitFiend] = GetMultiplierFromAverage(CreatureConstants.PitFiend, 800);
            weights[CreatureConstants.Pixie][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Pixie, 30);
            weights[CreatureConstants.Pixie][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Pixie, 30);
            weights[CreatureConstants.Pixie][CreatureConstants.Pixie] = GetMultiplierFromAverage(CreatureConstants.Pixie, 30);
            weights[CreatureConstants.Pixie_WithIrresistibleDance][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Pixie_WithIrresistibleDance, 30);
            weights[CreatureConstants.Pixie_WithIrresistibleDance][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Pixie_WithIrresistibleDance, 30);
            weights[CreatureConstants.Pixie_WithIrresistibleDance][CreatureConstants.Pixie_WithIrresistibleDance] = GetMultiplierFromAverage(CreatureConstants.Pixie_WithIrresistibleDance, 30);
            weights[CreatureConstants.Quasit][GenderConstants.Agender] = GetBaseFromAverage(CreatureConstants.Quasit, 8);
            weights[CreatureConstants.Quasit][CreatureConstants.Quasit] = GetMultiplierFromAverage(CreatureConstants.Quasit, 8);
            weights[CreatureConstants.Retriever][GenderConstants.Agender] = GetBaseFromAverage(CreatureConstants.Retriever, 6500);
            weights[CreatureConstants.Retriever][CreatureConstants.Retriever] = GetMultiplierFromAverage(CreatureConstants.Retriever, 6500);
            weights[CreatureConstants.Salamander_Flamebrother][GenderConstants.Agender] = "5";
            weights[CreatureConstants.Salamander_Flamebrother][CreatureConstants.Salamander_Flamebrother] = RollHelper.GetRollWithMostEvenDistribution(5, 8, 60);
            weights[CreatureConstants.Salamander_Average][GenderConstants.Agender] = "50";
            weights[CreatureConstants.Salamander_Average][CreatureConstants.Salamander_Average] = RollHelper.GetRollWithMostEvenDistribution(50, 60, 500);
            weights[CreatureConstants.Salamander_Noble][GenderConstants.Agender] = "400";
            weights[CreatureConstants.Salamander_Noble][CreatureConstants.Salamander_Noble] = RollHelper.GetRollWithMostEvenDistribution(400, 500, 4000);
            weights[CreatureConstants.SeaHag][GenderConstants.Female] = "85";
            weights[CreatureConstants.SeaHag][CreatureConstants.SeaHag] = "2d4";
            weights[CreatureConstants.Succubus][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Succubus, 125);
            weights[CreatureConstants.Succubus][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Succubus, 125);
            weights[CreatureConstants.Succubus][CreatureConstants.Succubus] = GetMultiplierFromAverage(CreatureConstants.Succubus, 125);
            weights[CreatureConstants.Tiefling][GenderConstants.Female] = "85";
            weights[CreatureConstants.Tiefling][GenderConstants.Male] = "120";
            weights[CreatureConstants.Tiefling][CreatureConstants.Tiefling] = "2d4"; //x5
            weights[CreatureConstants.Tojanida_Juvenile][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Tojanida_Juvenile, 60);
            weights[CreatureConstants.Tojanida_Juvenile][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Tojanida_Juvenile, 60);
            weights[CreatureConstants.Tojanida_Juvenile][CreatureConstants.Tojanida_Juvenile] = GetMultiplierFromAverage(CreatureConstants.Tojanida_Juvenile, 60);
            weights[CreatureConstants.Tojanida_Adult][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Tojanida_Adult, 220);
            weights[CreatureConstants.Tojanida_Adult][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Tojanida_Adult, 220);
            weights[CreatureConstants.Tojanida_Adult][CreatureConstants.Tojanida_Adult] = GetMultiplierFromAverage(CreatureConstants.Tojanida_Adult, 220);
            weights[CreatureConstants.Tojanida_Elder][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Tojanida_Elder, 500);
            weights[CreatureConstants.Tojanida_Elder][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Tojanida_Elder, 500);
            weights[CreatureConstants.Tojanida_Elder][CreatureConstants.Tojanida_Elder] = GetMultiplierFromAverage(CreatureConstants.Tojanida_Elder, 500);
            weights[CreatureConstants.Vrock][GenderConstants.Agender] = GetBaseFromAverage(CreatureConstants.Vrock, 500);
            weights[CreatureConstants.Vrock][CreatureConstants.Vrock] = GetMultiplierFromAverage(CreatureConstants.Vrock, 500);
            weights[CreatureConstants.Whale_Baleen][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Whale_Baleen, 44 * 2000);
            weights[CreatureConstants.Whale_Baleen][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Whale_Baleen, 44 * 2000);
            weights[CreatureConstants.Whale_Baleen][CreatureConstants.Whale_Baleen] = GetMultiplierFromAverage(CreatureConstants.Whale_Baleen, 44 * 2000);
            weights[CreatureConstants.Whale_Cachalot][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Whale_Cachalot, 15 * 2000);
            weights[CreatureConstants.Whale_Cachalot][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Whale_Cachalot, 45 * 2000);
            weights[CreatureConstants.Whale_Cachalot][CreatureConstants.Whale_Cachalot] = GetMultiplierFromAverage(CreatureConstants.Whale_Cachalot, 30 * 2000);
            weights[CreatureConstants.Whale_Orca][GenderConstants.Female] = GetBaseFromAverage(CreatureConstants.Whale_Orca, 4 * 2000);
            weights[CreatureConstants.Whale_Orca][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Whale_Orca, 6 * 2000);
            weights[CreatureConstants.Whale_Orca][CreatureConstants.Whale_Orca] = GetMultiplierFromAverage(CreatureConstants.Whale_Orca, 5 * 2000);
            weights[CreatureConstants.Wolf][GenderConstants.Female] = "39"; //Medium Animal
            weights[CreatureConstants.Wolf][GenderConstants.Male] = "39";
            weights[CreatureConstants.Wolf][CreatureConstants.Wolf] = "2d12";
            weights[CreatureConstants.Xorn_Minor][GenderConstants.Agender] = GetBaseFromAverage(CreatureConstants.Xorn_Minor, 120);
            weights[CreatureConstants.Xorn_Minor][CreatureConstants.Xorn_Minor] = GetMultiplierFromAverage(CreatureConstants.Xorn_Minor, 120);
            weights[CreatureConstants.Xorn_Average][GenderConstants.Agender] = GetBaseFromAverage(CreatureConstants.Xorn_Average, 600);
            weights[CreatureConstants.Xorn_Average][CreatureConstants.Xorn_Average] = GetMultiplierFromAverage(CreatureConstants.Xorn_Average, 600);
            weights[CreatureConstants.Xorn_Elder][GenderConstants.Agender] = GetBaseFromAverage(CreatureConstants.Xorn_Elder, 9000);
            weights[CreatureConstants.Xorn_Elder][CreatureConstants.Xorn_Elder] = GetMultiplierFromAverage(CreatureConstants.Xorn_Elder, 9000);

            return weights;
        }

        public static IEnumerable CreatureWeightsData => GetCreatureWeights().Select(t => new TestCaseData(t.Key, t.Value));

        private static string GetBaseFromAverage(string creature, int average) => GetBaseFromRange(creature, average * 9 / 10, average * 11 / 10);
        private static string GetBaseFromUpTo(string creature, int upTo) => GetBaseFromRange(creature, upTo * 9 / 11, upTo);
        private static string GetBaseFromAtLeast(string creature, int atLeast) => GetBaseFromRange(creature, atLeast, atLeast * 11 / 9);

        private static string GetMultiplierFromAverage(string creature, int average) => GetMultiplierFromRange(creature, average * 9 / 10, average * 11 / 10);
        private static string GetMultiplierFromUpTo(string creature, int upTo) => GetMultiplierFromRange(creature, upTo * 9 / 11, upTo);
        private static string GetMultiplierFromAtLeast(string creature, int atLeast) => GetMultiplierFromRange(creature, atLeast, atLeast * 11 / 9);

        private static string GetBaseFromRange(string creature, int lower, int upper)
        {
            var roll = GetTheoreticalWeightRoll(creature, lower, upper);
            if (string.IsNullOrEmpty(roll))
                return string.Empty;
            else if (roll.Contains("NOT A VALID RANGE"))
                return roll;

            var sections = roll.Split('+');
            if (sections.Length == 1)
                return "0";

            return sections[1];
        }

        private static string GetTheoreticalWeightRoll(string creature, int lower, int upper)
        {
            if (!heights.ContainsKey(creature) || !heights[creature].ContainsKey(creature)
                || !lengths.ContainsKey(creature) || !lengths[creature].ContainsKey(creature))
            {
                return string.Empty;
            }

            var sectionsH = heights[creature][creature].Split('d');
            var qH = Convert.ToInt32(sectionsH[0]);
            var dH = 1;
            if (sectionsH.Length > 1)
                dH = Convert.ToInt32(sectionsH[1]);

            var sectionsL = lengths[creature][creature].Split('d');
            var qL = Convert.ToInt32(sectionsL[0]);
            var dL = 1;
            if (sectionsL.Length > 1)
                dL = Convert.ToInt32(sectionsL[1]);

            var minMultiplier = qH;
            var maxMultiplier = qH * dH;
            if (qL * dL > qH * dH)
            {
                minMultiplier = qL;
                maxMultiplier = qL * dL;
            }

            var range = upper / maxMultiplier - lower / minMultiplier + 1;
            var adjustedUpper = range + lower - 1;
            if (adjustedUpper < lower)
                return $"[{lower},{adjustedUpper}] IS NOT A VALID RANGE";

            var roll = RollHelper.GetRollWithFewestDice(lower, adjustedUpper);
            return roll;
        }

        private static string GetMultiplierFromRange(string creature, int lower, int upper)
        {
            var roll = GetTheoreticalWeightRoll(creature, lower, upper);
            var sections = roll.Split('+');
            return sections[0];
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
        [TestCase(CreatureConstants.Baboon, GenderConstants.Male, 90)]
        [TestCase(CreatureConstants.Badger_Dire, GenderConstants.Male, 500)]
        [TestCase(CreatureConstants.Badger_Dire, GenderConstants.Female, 500)]
        [TestCase(CreatureConstants.Barghest, GenderConstants.Agender, 180)]
        [TestCase(CreatureConstants.Bear_Brown, GenderConstants.Male, 1800)]
        [TestCase(CreatureConstants.Bear_Brown, GenderConstants.Female, 1800)]
        //https://forgottenrealms.fandom.com/wiki/Centaur
        [TestCase(CreatureConstants.Centaur, GenderConstants.Male, 2100)]
        [TestCase(CreatureConstants.Centaur, GenderConstants.Female, 2100)]
        [TestCase(CreatureConstants.Giant_Cloud, GenderConstants.Male, 5000)]
        [TestCase(CreatureConstants.Giant_Cloud, GenderConstants.Female, 5000)]
        [TestCase(CreatureConstants.Giant_Hill, GenderConstants.Male, 1100)]
        [TestCase(CreatureConstants.Giant_Hill, GenderConstants.Female, 1100)]
        //https://forgottenrealms.fandom.com/wiki/Goblin
        [TestCase(CreatureConstants.Goblin, GenderConstants.Male, 60)]
        [TestCase(CreatureConstants.Goblin, GenderConstants.Female, 60)]
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

            Assert.That(heights, Contains.Key(creature));
            Assert.That(heights[creature], Contains.Key(creature).And.ContainKey(gender));

            var heightMultiplierMin = dice.Roll(heights[creature][creature]).AsPotentialMinimum();
            var heightMultiplierAvg = dice.Roll(heights[creature][creature]).AsPotentialAverage();
            var heightMultiplierMax = dice.Roll(heights[creature][creature]).AsPotentialMaximum();

            var weights = GetCreatureWeights();

            Assert.That(weights, Contains.Key(creature));
            Assert.That(weights[creature], Contains.Key(creature).And.ContainKey(gender));

            var baseWeight = dice.Roll(weights[creature][gender]).AsSum();
            var weightMultiplierMin = dice.Roll(weights[creature][creature]).AsPotentialMinimum();
            var weightMultiplierAvg = dice.Roll(weights[creature][creature]).AsPotentialAverage();
            var weightMultiplierMax = dice.Roll(weights[creature][creature]).AsPotentialMaximum();
            var sigma = Math.Max(1, average * 0.05);

            var theoreticalRoll = RollHelper.GetRollWithFewestDice(average * 9 / 10, average * 11 / 10);

            Assert.That(baseWeight + heightMultiplierMin * weightMultiplierMin, Is.Positive.And.EqualTo(average * 0.9).Within(sigma), $"Min (90%); Theoretical: {theoreticalRoll}");
            Assert.That(baseWeight + heightMultiplierAvg * weightMultiplierAvg, Is.Positive.And.EqualTo(average).Within(sigma), $"Average; Theoretical: {theoreticalRoll}");
            Assert.That(baseWeight + heightMultiplierMax * weightMultiplierMax, Is.Positive.And.EqualTo(average * 1.1).Within(sigma), $"Max (110%); Theoretical: {theoreticalRoll}");
        }

        //https://forgottenrealms.fandom.com/wiki/Aasimar
        [TestCase(CreatureConstants.Aasimar, GenderConstants.Male, 124, 280)]
        [TestCase(CreatureConstants.Aasimar, GenderConstants.Female, 89, 245)]
        [TestCase(CreatureConstants.AnimatedObject_Tiny, GenderConstants.Agender, 1, 8)]
        [TestCase(CreatureConstants.AnimatedObject_Tiny_Flexible, GenderConstants.Agender, 1, 8)]
        [TestCase(CreatureConstants.AnimatedObject_Small, GenderConstants.Agender, 8, 60)]
        [TestCase(CreatureConstants.AnimatedObject_Small_Flexible, GenderConstants.Agender, 8, 60)]
        [TestCase(CreatureConstants.AnimatedObject_Medium, GenderConstants.Agender, 60, 500)]
        [TestCase(CreatureConstants.AnimatedObject_Medium_Flexible, GenderConstants.Agender, 60, 500)]
        [TestCase(CreatureConstants.AnimatedObject_Large, GenderConstants.Agender, 500, 2 * 2000)]
        [TestCase(CreatureConstants.AnimatedObject_Large_Flexible, GenderConstants.Agender, 500, 2 * 2000)]
        [TestCase(CreatureConstants.AnimatedObject_Huge, GenderConstants.Agender, 2 * 2000, 16 * 2000)]
        [TestCase(CreatureConstants.AnimatedObject_Huge_Flexible, GenderConstants.Agender, 2 * 2000, 16 * 2000)]
        [TestCase(CreatureConstants.AnimatedObject_Gargantuan, GenderConstants.Agender, 16 * 2000, 125 * 2000)]
        [TestCase(CreatureConstants.AnimatedObject_Gargantuan_Flexible, GenderConstants.Agender, 16 * 2000, 125 * 2000)]
        [TestCase(CreatureConstants.AnimatedObject_Colossal, GenderConstants.Agender, 125 * 2000, 1000 * 2000)]
        [TestCase(CreatureConstants.AnimatedObject_Colossal_Flexible, GenderConstants.Agender, 125 * 2000, 1000 * 2000)]
        [TestCase(CreatureConstants.Ape, GenderConstants.Male, 300, 400)]
        [TestCase(CreatureConstants.Ape, GenderConstants.Female, 220, 460)]
        [TestCase(CreatureConstants.Ape_Dire, GenderConstants.Male, 800, 1200)]
        [TestCase(CreatureConstants.Ape_Dire, GenderConstants.Female, 800, 1200)]
        [TestCase(CreatureConstants.Azer, GenderConstants.Agender, 180, 220)]
        [TestCase(CreatureConstants.Baboon, GenderConstants.Female, 22, 82)]
        [TestCase(CreatureConstants.Badger, GenderConstants.Male, 25, 35)]
        [TestCase(CreatureConstants.Badger, GenderConstants.Female, 25, 35)]
        [TestCase(CreatureConstants.Balor, GenderConstants.Agender, 4000, 4900)]
        [TestCase(CreatureConstants.Bear_Black, GenderConstants.Male, 375, 600)]
        [TestCase(CreatureConstants.Bear_Black, GenderConstants.Female, 200, 450)]
        [TestCase(CreatureConstants.Bear_Polar, GenderConstants.Male, 775, 1500)]
        [TestCase(CreatureConstants.Bear_Polar, GenderConstants.Female, 330, 650)]
        //https://forgottenrealms.fandom.com/wiki/Bugbear
        [TestCase(CreatureConstants.Bugbear, GenderConstants.Male, 250, 350)]
        [TestCase(CreatureConstants.Bugbear, GenderConstants.Female, 250, 350)]
        [TestCase(CreatureConstants.Crocodile_Giant, GenderConstants.Male, 880, 2200)]
        [TestCase(CreatureConstants.Crocodile_Giant, GenderConstants.Female, 180, 220)]
        [TestCase(CreatureConstants.DisplacerBeast, GenderConstants.Male, 500, 1000)]
        [TestCase(CreatureConstants.DisplacerBeast, GenderConstants.Female, 450, 900)]
        [TestCase(CreatureConstants.DisplacerBeast_PackLord, GenderConstants.Male, 4000, 8000)]
        [TestCase(CreatureConstants.DisplacerBeast_PackLord, GenderConstants.Female, 3600, 7200)]
        [TestCase(CreatureConstants.Dog_Riding, GenderConstants.Male, 35, 180)]
        [TestCase(CreatureConstants.Dog_Riding, GenderConstants.Female, 35, 140)]
        [TestCase(CreatureConstants.Ettin, GenderConstants.Male, 930, 5200)]
        [TestCase(CreatureConstants.Ettin, GenderConstants.Female, 930, 5200)]
        //https://forgottenrealms.fandom.com/wiki/Githyanki
        [TestCase(CreatureConstants.Githyanki, GenderConstants.Male, 124, 280)]
        [TestCase(CreatureConstants.Githyanki, GenderConstants.Female, 89, 245)]
        //https://forgottenrealms.fandom.com/wiki/Githzerai
        [TestCase(CreatureConstants.Githzerai, GenderConstants.Male, 92, 196)]
        [TestCase(CreatureConstants.Githzerai, GenderConstants.Female, 92, 196)]
        //https://forgottenrealms.fandom.com/wiki/Gnoll
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

            Assert.That(heights, Contains.Key(creature));
            Assert.That(heights[creature], Contains.Key(creature).And.ContainKey(gender));

            var heightMultiplierMin = dice.Roll(heights[creature][creature]).AsPotentialMinimum();
            var heightMultiplierMax = dice.Roll(heights[creature][creature]).AsPotentialMaximum();

            var weights = GetCreatureWeights();

            Assert.That(weights, Contains.Key(creature));
            Assert.That(weights[creature], Contains.Key(creature).And.ContainKey(gender));

            var baseWeight = dice.Roll(weights[creature][gender]).AsSum();
            var weightMultiplierMin = dice.Roll(weights[creature][creature]).AsPotentialMinimum();
            var weightMultiplierMax = dice.Roll(weights[creature][creature]).AsPotentialMaximum();

            var theoreticalRoll = RollHelper.GetRollWithFewestDice(min, max);

            Assert.That(baseWeight + heightMultiplierMin * weightMultiplierMin, Is.Positive.And.EqualTo(min), $"Min; Theoretical: {theoreticalRoll}; Height: {heights[creature][creature]}");
            Assert.That(baseWeight + heightMultiplierMax * weightMultiplierMax, Is.Positive.And.EqualTo(max), $"Max; Theoretical: {theoreticalRoll}; Height: {heights[creature][creature]}");
        }
    }
}