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

            //Source: http://people.wku.edu/charles.plemons/ad&d/races/height.html
            weights[CreatureConstants.Aasimar][GenderConstants.Female] = "90";
            weights[CreatureConstants.Aasimar][GenderConstants.Male] = "140";
            weights[CreatureConstants.Aasimar][CreatureConstants.Aasimar] = "5d10"; //x5
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
            heights[CreatureConstants.ChaosBeast][GenderConstants.Agender] = GetBaseFromRange(5 * 12, 7 * 12);
            heights[CreatureConstants.ChaosBeast][CreatureConstants.ChaosBeast] = GetMultiplierFromRange(5 * 12, 7 * 12);
            //Source: https://www.dimensions.com/element/cheetahs
            heights[CreatureConstants.Cheetah][GenderConstants.Female] = GetBaseFromRange(28, 35);
            heights[CreatureConstants.Cheetah][GenderConstants.Male] = GetBaseFromRange(28, 35);
            heights[CreatureConstants.Cheetah][CreatureConstants.Cheetah] = GetMultiplierFromRange(28, 35);
            //Source: https://forgottenrealms.fandom.com/wiki/Chimera
            heights[CreatureConstants.Chimera_Black][GenderConstants.Female] = GetBaseFromAverage(5 * 12);
            heights[CreatureConstants.Chimera_Black][GenderConstants.Male] = GetBaseFromAverage(5 * 12);
            heights[CreatureConstants.Chimera_Black][CreatureConstants.Chimera_Black] = GetMultiplierFromAverage(5 * 12);
            heights[CreatureConstants.Chimera_Blue][GenderConstants.Female] = GetBaseFromAverage(5 * 12);
            heights[CreatureConstants.Chimera_Blue][GenderConstants.Male] = GetBaseFromAverage(5 * 12);
            heights[CreatureConstants.Chimera_Blue][CreatureConstants.Chimera_Blue] = GetMultiplierFromAverage(5 * 12);
            heights[CreatureConstants.Chimera_Green][GenderConstants.Female] = GetBaseFromAverage(5 * 12);
            heights[CreatureConstants.Chimera_Green][GenderConstants.Male] = GetBaseFromAverage(5 * 12);
            heights[CreatureConstants.Chimera_Green][CreatureConstants.Chimera_Green] = GetMultiplierFromAverage(5 * 12);
            heights[CreatureConstants.Chimera_Red][GenderConstants.Female] = GetBaseFromAverage(5 * 12);
            heights[CreatureConstants.Chimera_Red][GenderConstants.Male] = GetBaseFromAverage(5 * 12);
            heights[CreatureConstants.Chimera_Red][CreatureConstants.Chimera_Red] = GetMultiplierFromAverage(5 * 12);
            heights[CreatureConstants.Chimera_White][GenderConstants.Female] = GetBaseFromAverage(5 * 12);
            heights[CreatureConstants.Chimera_White][GenderConstants.Male] = GetBaseFromAverage(5 * 12);
            heights[CreatureConstants.Chimera_White][CreatureConstants.Chimera_White] = GetMultiplierFromAverage(5 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Choker
            heights[CreatureConstants.Choker][GenderConstants.Female] = GetBaseFromAverage(3 * 12 + 6);
            heights[CreatureConstants.Choker][GenderConstants.Male] = GetBaseFromAverage(3 * 12 + 6);
            heights[CreatureConstants.Choker][CreatureConstants.Choker] = GetMultiplierFromAverage(3 * 12 + 6);
            //Source: https://forgottenrealms.fandom.com/wiki/Chuul
            heights[CreatureConstants.Chuul][GenderConstants.Female] = "0";
            heights[CreatureConstants.Chuul][GenderConstants.Male] = "0";
            heights[CreatureConstants.Chuul][CreatureConstants.Chuul] = "0";
            //Source: https://forgottenrealms.fandom.com/wiki/Cloaker and https://www.mojobob.com/roleplay/monstrousmanual/c/cloaker.html
            heights[CreatureConstants.Cloaker][GenderConstants.Agender] = "0";
            heights[CreatureConstants.Cloaker][CreatureConstants.Cloaker] = "0";
            //Source: https://forgottenrealms.fandom.com/wiki/Cockatrice
            heights[CreatureConstants.Cockatrice][GenderConstants.Female] = GetBaseFromAverage(3 * 12);
            heights[CreatureConstants.Cockatrice][GenderConstants.Male] = GetBaseFromAverage(3 * 12);
            heights[CreatureConstants.Cockatrice][CreatureConstants.Cockatrice] = GetMultiplierFromAverage(3 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Couatl
            heights[CreatureConstants.Couatl][GenderConstants.Female] = "0";
            heights[CreatureConstants.Couatl][GenderConstants.Male] = "0";
            heights[CreatureConstants.Couatl][CreatureConstants.Couatl] = "0";
            //Source: https://www.d20srd.org/srd/monsters/sphinx.htm
            weights[CreatureConstants.Criosphinx][GenderConstants.Male] = GetBaseFromAverage(CreatureConstants.Criosphinx, 800);
            weights[CreatureConstants.Criosphinx][CreatureConstants.Criosphinx] = GetMultiplierFromAverage(CreatureConstants.Criosphinx, 800);
            weights[CreatureConstants.Dretch][GenderConstants.Agender] = GetBaseFromAverage(CreatureConstants.Dretch, 60);
            weights[CreatureConstants.Dretch][CreatureConstants.Dretch] = GetMultiplierFromAverage(CreatureConstants.Dretch, 60);
            //Source: https://www.worldanvil.com/w/faerun-tatortotzke/a/dryad-species
            weights[CreatureConstants.Dryad][GenderConstants.Agender] = GetBaseFromAverage(CreatureConstants.Dryad, 150);
            weights[CreatureConstants.Dryad][CreatureConstants.Dryad] = GetMultiplierFromAverage(CreatureConstants.Dryad, 150);
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
            weights[CreatureConstants.Erinyes][GenderConstants.Agender] = GetBaseFromAverage(CreatureConstants.Erinyes, 150);
            weights[CreatureConstants.Erinyes][CreatureConstants.Erinyes] = GetMultiplierFromAverage(CreatureConstants.Erinyes, 150);
            weights[CreatureConstants.Ettin][GenderConstants.Female] = "912";
            weights[CreatureConstants.Ettin][GenderConstants.Male] = "912";
            weights[CreatureConstants.Ettin][CreatureConstants.Ettin] = "18d20";
            weights[CreatureConstants.Giant_Cloud][GenderConstants.Female] = "290"; //Huge
            weights[CreatureConstants.Giant_Cloud][GenderConstants.Male] = "270";
            weights[CreatureConstants.Giant_Cloud][CreatureConstants.Giant_Cloud] = "3d10";
            weights[CreatureConstants.Giant_Hill][GenderConstants.Female] = "290"; //Huge
            weights[CreatureConstants.Giant_Hill][GenderConstants.Male] = "270";
            weights[CreatureConstants.Giant_Hill][CreatureConstants.Giant_Cloud] = "3d10";
            weights[CreatureConstants.Glabrezu][GenderConstants.Agender] = GetBaseFromAverage(CreatureConstants.Glabrezu, 5500);
            weights[CreatureConstants.Glabrezu][CreatureConstants.Glabrezu] = GetMultiplierFromAverage(CreatureConstants.Glabrezu, 5500);
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