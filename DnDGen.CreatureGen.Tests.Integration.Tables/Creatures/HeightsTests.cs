﻿using DnDGen.CreatureGen.Creatures;
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
            Assert.That(typesAndRolls.Keys, Is.EquivalentTo(genders.Union(new[] { name })).And.Not.Empty, $"TEST DATA: {name}");

            foreach (var roll in typesAndRolls.Values)
            {
                var isValid = dice.Roll(roll).IsValid();
                Assert.That(isValid, Is.True, roll);
            }

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

            //Source: http://people.wku.edu/charles.plemons/ad&d/races/height.html
            heights[CreatureConstants.Aasimar][GenderConstants.Female] = "60";
            heights[CreatureConstants.Aasimar][GenderConstants.Male] = "61";
            heights[CreatureConstants.Aasimar][CreatureConstants.Aasimar] = "2d10";
            //Source: https://forgottenrealms.fandom.com/wiki/Aboleth
            heights[CreatureConstants.Aboleth][GenderConstants.Hermaphrodite] = "0";
            heights[CreatureConstants.Aboleth][CreatureConstants.Aboleth] = "0";
            //Source: https://www.d20srd.org/srd/monsters/achaierai.htm
            heights[CreatureConstants.Achaierai][GenderConstants.Female] = GetBaseFromAverage(15 * 12);
            heights[CreatureConstants.Achaierai][GenderConstants.Male] = GetBaseFromAverage(15 * 12);
            heights[CreatureConstants.Achaierai][CreatureConstants.Achaierai] = GetMultiplierFromAverage(15 * 12);
            //Basing off humans
            heights[CreatureConstants.Allip][GenderConstants.Female] = "4*12+5";
            heights[CreatureConstants.Allip][GenderConstants.Male] = "4*12+10";
            heights[CreatureConstants.Allip][CreatureConstants.Allip] = "2d10";
            //Source: https://www.mojobob.com/roleplay/monstrousmanual/s/sphinx.html
            heights[CreatureConstants.Androsphinx][GenderConstants.Male] = GetBaseFromAverage(8 * 12);
            heights[CreatureConstants.Androsphinx][CreatureConstants.Androsphinx] = GetMultiplierFromAverage(8 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Astral_deva
            heights[CreatureConstants.Angel_AstralDeva][GenderConstants.Female] = GetBaseFromRange(7 * 12, 7 * 12 + 6);
            heights[CreatureConstants.Angel_AstralDeva][GenderConstants.Male] = GetBaseFromRange(7 * 12, 7 * 12 + 6);
            heights[CreatureConstants.Angel_AstralDeva][CreatureConstants.Angel_AstralDeva] = GetMultiplierFromRange(7 * 12, 7 * 12 + 6);
            //Source: https://forgottenrealms.fandom.com/wiki/Planetar
            heights[CreatureConstants.Angel_Planetar][GenderConstants.Female] = GetBaseFromRange(8 * 12, 9 * 12);
            heights[CreatureConstants.Angel_Planetar][GenderConstants.Male] = GetBaseFromRange(8 * 12, 9 * 12);
            heights[CreatureConstants.Angel_Planetar][CreatureConstants.Angel_Planetar] = GetMultiplierFromRange(8 * 12, 9 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Solar
            heights[CreatureConstants.Angel_Solar][GenderConstants.Female] = GetBaseFromRange(9 * 12, 10 * 12);
            heights[CreatureConstants.Angel_Solar][GenderConstants.Male] = GetBaseFromRange(9 * 12, 10 * 12);
            heights[CreatureConstants.Angel_Solar][CreatureConstants.Angel_Solar] = GetMultiplierFromRange(9 * 12, 10 * 12);
            //Source: https://www.d20srd.org/srd/combat/movementPositionAndDistance.htm
            heights[CreatureConstants.AnimatedObject_Colossal][GenderConstants.Agender] = GetBaseFromRange(64 * 12, 128 * 12);
            heights[CreatureConstants.AnimatedObject_Colossal][CreatureConstants.AnimatedObject_Colossal] = GetMultiplierFromRange(64 * 12, 128 * 12);
            heights[CreatureConstants.AnimatedObject_Colossal_Flexible][GenderConstants.Agender] = "0";
            heights[CreatureConstants.AnimatedObject_Colossal_Flexible][CreatureConstants.AnimatedObject_Colossal_Flexible] = "0";
            heights[CreatureConstants.AnimatedObject_Colossal_MultipleLegs][GenderConstants.Agender] = GetBaseFromRange(64 * 12, 128 * 12);
            heights[CreatureConstants.AnimatedObject_Colossal_MultipleLegs][CreatureConstants.AnimatedObject_Colossal_MultipleLegs] = GetMultiplierFromRange(64 * 12, 128 * 12);
            heights[CreatureConstants.AnimatedObject_Colossal_MultipleLegs_Wooden][GenderConstants.Agender] = GetBaseFromRange(64 * 12, 128 * 12);
            heights[CreatureConstants.AnimatedObject_Colossal_MultipleLegs_Wooden][CreatureConstants.AnimatedObject_Colossal_MultipleLegs_Wooden] = GetMultiplierFromRange(64 * 12, 128 * 12);
            heights[CreatureConstants.AnimatedObject_Colossal_Sheetlike][GenderConstants.Agender] = "0";
            heights[CreatureConstants.AnimatedObject_Colossal_Sheetlike][CreatureConstants.AnimatedObject_Colossal_Sheetlike] = "0";
            heights[CreatureConstants.AnimatedObject_Colossal_TwoLegs][GenderConstants.Agender] = GetBaseFromRange(64 * 12, 128 * 12);
            heights[CreatureConstants.AnimatedObject_Colossal_TwoLegs][CreatureConstants.AnimatedObject_Colossal_TwoLegs] = GetMultiplierFromRange(64 * 12, 128 * 12);
            heights[CreatureConstants.AnimatedObject_Colossal_TwoLegs_Wooden][GenderConstants.Agender] = GetBaseFromRange(64 * 12, 128 * 12);
            heights[CreatureConstants.AnimatedObject_Colossal_TwoLegs_Wooden][CreatureConstants.AnimatedObject_Colossal_TwoLegs_Wooden] = GetMultiplierFromRange(64 * 12, 128 * 12);
            heights[CreatureConstants.AnimatedObject_Colossal_Wheels_Wooden][GenderConstants.Agender] = GetBaseFromRange(64 * 12, 128 * 12);
            heights[CreatureConstants.AnimatedObject_Colossal_Wheels_Wooden][CreatureConstants.AnimatedObject_Colossal_Wheels_Wooden] = GetMultiplierFromRange(64 * 12, 128 * 12);
            heights[CreatureConstants.AnimatedObject_Colossal_Wooden][GenderConstants.Agender] = GetBaseFromRange(64 * 12, 128 * 12);
            heights[CreatureConstants.AnimatedObject_Colossal_Wooden][CreatureConstants.AnimatedObject_Colossal_Wooden] = GetMultiplierFromRange(64 * 12, 128 * 12);
            heights[CreatureConstants.AnimatedObject_Gargantuan][GenderConstants.Agender] = GetBaseFromRange(32 * 12, 64 * 12);
            heights[CreatureConstants.AnimatedObject_Gargantuan][CreatureConstants.AnimatedObject_Gargantuan] = GetMultiplierFromRange(32 * 12, 64 * 12);
            heights[CreatureConstants.AnimatedObject_Gargantuan_Flexible][GenderConstants.Agender] = "0";
            heights[CreatureConstants.AnimatedObject_Gargantuan_Flexible][CreatureConstants.AnimatedObject_Gargantuan_Flexible] = "0";
            heights[CreatureConstants.AnimatedObject_Gargantuan_MultipleLegs][GenderConstants.Agender] = GetBaseFromRange(32 * 12, 64 * 12);
            heights[CreatureConstants.AnimatedObject_Gargantuan_MultipleLegs][CreatureConstants.AnimatedObject_Gargantuan_MultipleLegs] = GetMultiplierFromRange(32 * 12, 64 * 12);
            heights[CreatureConstants.AnimatedObject_Gargantuan_MultipleLegs_Wooden][GenderConstants.Agender] = GetBaseFromRange(32 * 12, 64 * 12);
            heights[CreatureConstants.AnimatedObject_Gargantuan_MultipleLegs_Wooden][CreatureConstants.AnimatedObject_Gargantuan_MultipleLegs_Wooden] = GetMultiplierFromRange(32 * 12, 64 * 12);
            heights[CreatureConstants.AnimatedObject_Gargantuan_Sheetlike][GenderConstants.Agender] = "0";
            heights[CreatureConstants.AnimatedObject_Gargantuan_Sheetlike][CreatureConstants.AnimatedObject_Gargantuan_Sheetlike] = "0";
            heights[CreatureConstants.AnimatedObject_Gargantuan_TwoLegs][GenderConstants.Agender] = GetBaseFromRange(32 * 12, 64 * 12);
            heights[CreatureConstants.AnimatedObject_Gargantuan_TwoLegs][CreatureConstants.AnimatedObject_Gargantuan_TwoLegs] = GetMultiplierFromRange(32 * 12, 64 * 12);
            heights[CreatureConstants.AnimatedObject_Gargantuan_TwoLegs_Wooden][GenderConstants.Agender] = GetBaseFromRange(32 * 12, 64 * 12);
            heights[CreatureConstants.AnimatedObject_Gargantuan_TwoLegs_Wooden][CreatureConstants.AnimatedObject_Gargantuan_TwoLegs_Wooden] = GetMultiplierFromRange(32 * 12, 64 * 12);
            heights[CreatureConstants.AnimatedObject_Gargantuan_Wheels_Wooden][GenderConstants.Agender] = GetBaseFromRange(32 * 12, 64 * 12);
            heights[CreatureConstants.AnimatedObject_Gargantuan_Wheels_Wooden][CreatureConstants.AnimatedObject_Gargantuan_Wheels_Wooden] = GetMultiplierFromRange(32 * 12, 64 * 12);
            heights[CreatureConstants.AnimatedObject_Gargantuan_Wooden][GenderConstants.Agender] = GetBaseFromRange(32 * 12, 64 * 12);
            heights[CreatureConstants.AnimatedObject_Gargantuan_Wooden][CreatureConstants.AnimatedObject_Gargantuan_Wooden] = GetMultiplierFromRange(32 * 12, 64 * 12);
            heights[CreatureConstants.AnimatedObject_Huge][GenderConstants.Agender] = GetBaseFromRange(16 * 12, 32 * 12);
            heights[CreatureConstants.AnimatedObject_Huge][CreatureConstants.AnimatedObject_Huge] = GetMultiplierFromRange(16 * 12, 32 * 12);
            heights[CreatureConstants.AnimatedObject_Huge_Flexible][GenderConstants.Agender] = "0";
            heights[CreatureConstants.AnimatedObject_Huge_Flexible][CreatureConstants.AnimatedObject_Huge_Flexible] = "0";
            heights[CreatureConstants.AnimatedObject_Huge_MultipleLegs][GenderConstants.Agender] = GetBaseFromRange(16 * 12, 32 * 12);
            heights[CreatureConstants.AnimatedObject_Huge_MultipleLegs][CreatureConstants.AnimatedObject_Huge_MultipleLegs] = GetMultiplierFromRange(16 * 12, 32 * 12);
            heights[CreatureConstants.AnimatedObject_Huge_MultipleLegs_Wooden][GenderConstants.Agender] = GetBaseFromRange(16 * 12, 32 * 12);
            heights[CreatureConstants.AnimatedObject_Huge_MultipleLegs_Wooden][CreatureConstants.AnimatedObject_Huge_MultipleLegs_Wooden] = GetMultiplierFromRange(16 * 12, 32 * 12);
            heights[CreatureConstants.AnimatedObject_Huge_Sheetlike][GenderConstants.Agender] = "0";
            heights[CreatureConstants.AnimatedObject_Huge_Sheetlike][CreatureConstants.AnimatedObject_Huge_Sheetlike] = "0";
            heights[CreatureConstants.AnimatedObject_Huge_TwoLegs][GenderConstants.Agender] = GetBaseFromRange(16 * 12, 32 * 12);
            heights[CreatureConstants.AnimatedObject_Huge_TwoLegs][CreatureConstants.AnimatedObject_Huge_TwoLegs] = GetMultiplierFromRange(16 * 12, 32 * 12);
            heights[CreatureConstants.AnimatedObject_Huge_TwoLegs_Wooden][GenderConstants.Agender] = GetBaseFromRange(16 * 12, 32 * 12);
            heights[CreatureConstants.AnimatedObject_Huge_TwoLegs_Wooden][CreatureConstants.AnimatedObject_Huge_TwoLegs_Wooden] = GetMultiplierFromRange(16 * 12, 32 * 12);
            heights[CreatureConstants.AnimatedObject_Huge_Wheels_Wooden][GenderConstants.Agender] = GetBaseFromRange(16 * 12, 32 * 12);
            heights[CreatureConstants.AnimatedObject_Huge_Wheels_Wooden][CreatureConstants.AnimatedObject_Huge_Wheels_Wooden] = GetMultiplierFromRange(16 * 12, 32 * 12);
            heights[CreatureConstants.AnimatedObject_Huge_Wooden][GenderConstants.Agender] = GetBaseFromRange(16 * 12, 32 * 12);
            heights[CreatureConstants.AnimatedObject_Huge_Wooden][CreatureConstants.AnimatedObject_Huge_Wooden] = GetMultiplierFromRange(16 * 12, 32 * 12);
            heights[CreatureConstants.AnimatedObject_Large][GenderConstants.Agender] = GetBaseFromRange(8 * 12, 16 * 12);
            heights[CreatureConstants.AnimatedObject_Large][CreatureConstants.AnimatedObject_Large] = GetMultiplierFromRange(8 * 12, 16 * 12);
            heights[CreatureConstants.AnimatedObject_Large_Flexible][GenderConstants.Agender] = "0";
            heights[CreatureConstants.AnimatedObject_Large_Flexible][CreatureConstants.AnimatedObject_Large_Flexible] = "0";
            heights[CreatureConstants.AnimatedObject_Large_MultipleLegs][GenderConstants.Agender] = GetBaseFromRange(8 * 12, 16 * 12);
            heights[CreatureConstants.AnimatedObject_Large_MultipleLegs][CreatureConstants.AnimatedObject_Large_MultipleLegs] = GetMultiplierFromRange(8 * 12, 16 * 12);
            heights[CreatureConstants.AnimatedObject_Large_MultipleLegs_Wooden][GenderConstants.Agender] = GetBaseFromRange(8 * 12, 16 * 12);
            heights[CreatureConstants.AnimatedObject_Large_MultipleLegs_Wooden][CreatureConstants.AnimatedObject_Large_MultipleLegs_Wooden] = GetMultiplierFromRange(8 * 12, 16 * 12);
            heights[CreatureConstants.AnimatedObject_Large_Sheetlike][GenderConstants.Agender] = "0";
            heights[CreatureConstants.AnimatedObject_Large_Sheetlike][CreatureConstants.AnimatedObject_Large_Sheetlike] = "0";
            heights[CreatureConstants.AnimatedObject_Large_TwoLegs][GenderConstants.Agender] = GetBaseFromRange(8 * 12, 16 * 12);
            heights[CreatureConstants.AnimatedObject_Large_TwoLegs][CreatureConstants.AnimatedObject_Large_TwoLegs] = GetMultiplierFromRange(8 * 12, 16 * 12);
            heights[CreatureConstants.AnimatedObject_Large_TwoLegs_Wooden][GenderConstants.Agender] = GetBaseFromRange(8 * 12, 16 * 12);
            heights[CreatureConstants.AnimatedObject_Large_TwoLegs_Wooden][CreatureConstants.AnimatedObject_Large_TwoLegs_Wooden] = GetMultiplierFromRange(8 * 12, 16 * 12);
            heights[CreatureConstants.AnimatedObject_Large_Wheels_Wooden][GenderConstants.Agender] = GetBaseFromRange(8 * 12, 16 * 12);
            heights[CreatureConstants.AnimatedObject_Large_Wheels_Wooden][CreatureConstants.AnimatedObject_Large_Wheels_Wooden] = GetMultiplierFromRange(8 * 12, 16 * 12);
            heights[CreatureConstants.AnimatedObject_Large_Wooden][GenderConstants.Agender] = GetBaseFromRange(8 * 12, 16 * 12);
            heights[CreatureConstants.AnimatedObject_Large_Wooden][CreatureConstants.AnimatedObject_Large_Wooden] = GetMultiplierFromRange(8 * 12, 16 * 12);
            heights[CreatureConstants.AnimatedObject_Medium][GenderConstants.Agender] = GetBaseFromRange(4 * 12, 8 * 12);
            heights[CreatureConstants.AnimatedObject_Medium][CreatureConstants.AnimatedObject_Medium] = GetMultiplierFromRange(4 * 12, 8 * 12);
            heights[CreatureConstants.AnimatedObject_Medium_Flexible][GenderConstants.Agender] = "0";
            heights[CreatureConstants.AnimatedObject_Medium_Flexible][CreatureConstants.AnimatedObject_Medium_Flexible] = "0";
            heights[CreatureConstants.AnimatedObject_Medium_MultipleLegs][GenderConstants.Agender] = GetBaseFromRange(4 * 12, 8 * 12);
            heights[CreatureConstants.AnimatedObject_Medium_MultipleLegs][CreatureConstants.AnimatedObject_Medium_MultipleLegs] = GetMultiplierFromRange(4 * 12, 8 * 12);
            heights[CreatureConstants.AnimatedObject_Medium_MultipleLegs_Wooden][GenderConstants.Agender] = GetBaseFromRange(4 * 12, 8 * 12);
            heights[CreatureConstants.AnimatedObject_Medium_MultipleLegs_Wooden][CreatureConstants.AnimatedObject_Medium_MultipleLegs_Wooden] = GetMultiplierFromRange(4 * 12, 8 * 12);
            heights[CreatureConstants.AnimatedObject_Medium_Sheetlike][GenderConstants.Agender] = "0";
            heights[CreatureConstants.AnimatedObject_Medium_Sheetlike][CreatureConstants.AnimatedObject_Medium_Sheetlike] = "0";
            heights[CreatureConstants.AnimatedObject_Medium_TwoLegs][GenderConstants.Agender] = GetBaseFromRange(4 * 12, 8 * 12);
            heights[CreatureConstants.AnimatedObject_Medium_TwoLegs][CreatureConstants.AnimatedObject_Medium_TwoLegs] = GetMultiplierFromRange(4 * 12, 8 * 12);
            heights[CreatureConstants.AnimatedObject_Medium_TwoLegs_Wooden][GenderConstants.Agender] = GetBaseFromRange(4 * 12, 8 * 12);
            heights[CreatureConstants.AnimatedObject_Medium_TwoLegs_Wooden][CreatureConstants.AnimatedObject_Medium_TwoLegs_Wooden] = GetMultiplierFromRange(4 * 12, 8 * 12);
            heights[CreatureConstants.AnimatedObject_Medium_Wheels_Wooden][GenderConstants.Agender] = GetBaseFromRange(4 * 12, 8 * 12);
            heights[CreatureConstants.AnimatedObject_Medium_Wheels_Wooden][CreatureConstants.AnimatedObject_Medium_Wheels_Wooden] = GetMultiplierFromRange(4 * 12, 8 * 12);
            heights[CreatureConstants.AnimatedObject_Medium_Wooden][GenderConstants.Agender] = GetBaseFromRange(4 * 12, 8 * 12);
            heights[CreatureConstants.AnimatedObject_Medium_Wooden][CreatureConstants.AnimatedObject_Medium_Wooden] = GetMultiplierFromRange(4 * 12, 8 * 12);
            heights[CreatureConstants.AnimatedObject_Small][GenderConstants.Agender] = GetBaseFromRange(2 * 12, 4 * 12);
            heights[CreatureConstants.AnimatedObject_Small][CreatureConstants.AnimatedObject_Small] = GetMultiplierFromRange(2 * 12, 4 * 12);
            heights[CreatureConstants.AnimatedObject_Small_Flexible][GenderConstants.Agender] = "0";
            heights[CreatureConstants.AnimatedObject_Small_Flexible][CreatureConstants.AnimatedObject_Small_Flexible] = "0";
            heights[CreatureConstants.AnimatedObject_Small_MultipleLegs][GenderConstants.Agender] = GetBaseFromRange(2 * 12, 4 * 12);
            heights[CreatureConstants.AnimatedObject_Small_MultipleLegs][CreatureConstants.AnimatedObject_Small_MultipleLegs] = GetMultiplierFromRange(2 * 12, 4 * 12);
            heights[CreatureConstants.AnimatedObject_Small_MultipleLegs_Wooden][GenderConstants.Agender] = GetBaseFromRange(2 * 12, 4 * 12);
            heights[CreatureConstants.AnimatedObject_Small_MultipleLegs_Wooden][CreatureConstants.AnimatedObject_Small_MultipleLegs_Wooden] = GetMultiplierFromRange(2 * 12, 4 * 12);
            heights[CreatureConstants.AnimatedObject_Small_Sheetlike][GenderConstants.Agender] = "0";
            heights[CreatureConstants.AnimatedObject_Small_Sheetlike][CreatureConstants.AnimatedObject_Small_Sheetlike] = "0";
            heights[CreatureConstants.AnimatedObject_Small_TwoLegs][GenderConstants.Agender] = GetBaseFromRange(2 * 12, 4 * 12);
            heights[CreatureConstants.AnimatedObject_Small_TwoLegs][CreatureConstants.AnimatedObject_Small_TwoLegs] = GetMultiplierFromRange(2 * 12, 4 * 12);
            heights[CreatureConstants.AnimatedObject_Small_TwoLegs_Wooden][GenderConstants.Agender] = GetBaseFromRange(2 * 12, 4 * 12);
            heights[CreatureConstants.AnimatedObject_Small_TwoLegs_Wooden][CreatureConstants.AnimatedObject_Small_TwoLegs_Wooden] = GetMultiplierFromRange(2 * 12, 4 * 12);
            heights[CreatureConstants.AnimatedObject_Small_Wheels_Wooden][GenderConstants.Agender] = GetBaseFromRange(2 * 12, 4 * 12);
            heights[CreatureConstants.AnimatedObject_Small_Wheels_Wooden][CreatureConstants.AnimatedObject_Small_Wheels_Wooden] = GetMultiplierFromRange(2 * 12, 4 * 12);
            heights[CreatureConstants.AnimatedObject_Small_Wooden][GenderConstants.Agender] = GetBaseFromRange(2 * 12, 4 * 12);
            heights[CreatureConstants.AnimatedObject_Small_Wooden][CreatureConstants.AnimatedObject_Small_Wooden] = GetMultiplierFromRange(2 * 12, 4 * 12);
            heights[CreatureConstants.AnimatedObject_Tiny][GenderConstants.Agender] = GetBaseFromRange(12, 24);
            heights[CreatureConstants.AnimatedObject_Tiny][CreatureConstants.AnimatedObject_Tiny] = GetMultiplierFromRange(12, 24);
            heights[CreatureConstants.AnimatedObject_Tiny_Flexible][GenderConstants.Agender] = "0";
            heights[CreatureConstants.AnimatedObject_Tiny_Flexible][CreatureConstants.AnimatedObject_Tiny_Flexible] = "0";
            heights[CreatureConstants.AnimatedObject_Tiny_MultipleLegs][GenderConstants.Agender] = GetBaseFromRange(12, 24);
            heights[CreatureConstants.AnimatedObject_Tiny_MultipleLegs][CreatureConstants.AnimatedObject_Tiny_MultipleLegs] = GetMultiplierFromRange(12, 24);
            heights[CreatureConstants.AnimatedObject_Tiny_MultipleLegs_Wooden][GenderConstants.Agender] = GetBaseFromRange(12, 24);
            heights[CreatureConstants.AnimatedObject_Tiny_MultipleLegs_Wooden][CreatureConstants.AnimatedObject_Tiny_MultipleLegs_Wooden] = GetMultiplierFromRange(12, 24);
            heights[CreatureConstants.AnimatedObject_Tiny_Sheetlike][GenderConstants.Agender] = "0";
            heights[CreatureConstants.AnimatedObject_Tiny_Sheetlike][CreatureConstants.AnimatedObject_Tiny_Sheetlike] = "0";
            heights[CreatureConstants.AnimatedObject_Tiny_TwoLegs][GenderConstants.Agender] = GetBaseFromRange(12, 24);
            heights[CreatureConstants.AnimatedObject_Tiny_TwoLegs][CreatureConstants.AnimatedObject_Tiny_TwoLegs] = GetMultiplierFromRange(12, 24);
            heights[CreatureConstants.AnimatedObject_Tiny_TwoLegs_Wooden][GenderConstants.Agender] = GetBaseFromRange(12, 24);
            heights[CreatureConstants.AnimatedObject_Tiny_TwoLegs_Wooden][CreatureConstants.AnimatedObject_Tiny_TwoLegs_Wooden] = GetMultiplierFromRange(12, 24);
            heights[CreatureConstants.AnimatedObject_Tiny_Wheels_Wooden][GenderConstants.Agender] = GetBaseFromRange(12, 24);
            heights[CreatureConstants.AnimatedObject_Tiny_Wheels_Wooden][CreatureConstants.AnimatedObject_Tiny_Wheels_Wooden] = GetMultiplierFromRange(12, 24);
            heights[CreatureConstants.AnimatedObject_Tiny_Wooden][GenderConstants.Agender] = GetBaseFromRange(12, 24);
            heights[CreatureConstants.AnimatedObject_Tiny_Wooden][CreatureConstants.AnimatedObject_Tiny_Wooden] = GetMultiplierFromRange(12, 24);
            //Source: https://www.d20srd.org/srd/monsters/ankheg.htm
            heights[CreatureConstants.Ankheg][GenderConstants.Female] = "0";
            heights[CreatureConstants.Ankheg][GenderConstants.Male] = "0";
            heights[CreatureConstants.Ankheg][CreatureConstants.Ankheg] = "0";
            //Source: https://www.d20srd.org/srd/monsters/hag.htm#annis
            heights[CreatureConstants.Annis][GenderConstants.Female] = GetBaseFromAverage(8 * 12);
            heights[CreatureConstants.Annis][CreatureConstants.Annis] = GetMultiplierFromAverage(8 * 12);
            //Source: https://www.d20srd.org/srd/monsters/giantAnt.htm
            //https://www.dimensions.com/element/black-garden-ant-lasius-niger - scale up, [.035,.05]*6*12/[.14,.2] = [18,18]
            heights[CreatureConstants.Ant_Giant_Worker][GenderConstants.Male] = GetBaseFromAverage(18);
            heights[CreatureConstants.Ant_Giant_Worker][CreatureConstants.Ant_Giant_Worker] = GetMultiplierFromAverage(18);
            heights[CreatureConstants.Ant_Giant_Soldier][GenderConstants.Male] = GetBaseFromAverage(18);
            heights[CreatureConstants.Ant_Giant_Soldier][CreatureConstants.Ant_Giant_Soldier] = GetMultiplierFromAverage(18);
            //https://www.dimensions.com/element/black-garden-ant-lasius-niger - scale up, [.035,.05]*9*12/[.31,.35] = [12,16]
            heights[CreatureConstants.Ant_Giant_Queen][GenderConstants.Female] = GetBaseFromRange(12, 16);
            heights[CreatureConstants.Ant_Giant_Queen][CreatureConstants.Ant_Giant_Queen] = GetMultiplierFromRange(12, 16);
            //Source: https://www.dimensions.com/element/eastern-lowland-gorilla-gorilla-beringei-graueri
            //https://www.d20srd.org/srd/monsters/ape.htm (male)
            heights[CreatureConstants.Ape][GenderConstants.Female] = GetBaseFromRange(63, 72);
            heights[CreatureConstants.Ape][GenderConstants.Male] = GetBaseFromRange(5 * 12 + 6, 6 * 12);
            heights[CreatureConstants.Ape][CreatureConstants.Ape] = GetMultiplierFromRange(5 * 12 + 6, 6 * 12);
            //Source: https://www.d20srd.org/srd/monsters/direApe.htm
            heights[CreatureConstants.Ape_Dire][GenderConstants.Female] = GetBaseFromAverage(9 * 12);
            heights[CreatureConstants.Ape_Dire][GenderConstants.Male] = GetBaseFromAverage(9 * 12);
            heights[CreatureConstants.Ape_Dire][CreatureConstants.Ape_Dire] = GetMultiplierFromAverage(9 * 12);
            //INFO: Based on Half-Elf, since could be Human, Half-Elf, or Drow
            heights[CreatureConstants.Aranea][GenderConstants.Female] = "4*12+5";
            heights[CreatureConstants.Aranea][GenderConstants.Male] = "4*12+7";
            heights[CreatureConstants.Aranea][CreatureConstants.Aranea] = "2d8";
            //Source: https://www.d20srd.org/srd/monsters/arrowhawk.htm
            heights[CreatureConstants.Arrowhawk_Juvenile][GenderConstants.Female] = "0";
            heights[CreatureConstants.Arrowhawk_Juvenile][GenderConstants.Male] = "0";
            heights[CreatureConstants.Arrowhawk_Juvenile][CreatureConstants.Arrowhawk_Juvenile] = "0";
            heights[CreatureConstants.Arrowhawk_Adult][GenderConstants.Female] = "0";
            heights[CreatureConstants.Arrowhawk_Adult][GenderConstants.Male] = "0";
            heights[CreatureConstants.Arrowhawk_Adult][CreatureConstants.Arrowhawk_Adult] = "0";
            heights[CreatureConstants.Arrowhawk_Elder][GenderConstants.Female] = "0";
            heights[CreatureConstants.Arrowhawk_Elder][GenderConstants.Male] = "0";
            heights[CreatureConstants.Arrowhawk_Elder][CreatureConstants.Arrowhawk_Elder] = "0";
            //Source: https://forgottenrealms.fandom.com/wiki/Assassin_vine
            heights[CreatureConstants.AssassinVine][GenderConstants.Agender] = "0";
            heights[CreatureConstants.AssassinVine][CreatureConstants.AssassinVine] = "0";
            //Source: https://www.d20srd.org/srd/monsters/athach.htm
            heights[CreatureConstants.Athach][GenderConstants.Female] = GetBaseFromAverage(18 * 12);
            heights[CreatureConstants.Athach][GenderConstants.Male] = GetBaseFromAverage(18 * 12);
            heights[CreatureConstants.Athach][CreatureConstants.Athach] = GetMultiplierFromAverage(18 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Avoral
            heights[CreatureConstants.Avoral][GenderConstants.Female] = GetBaseFromRange(6 * 12 + 6, 7 * 12);
            heights[CreatureConstants.Avoral][GenderConstants.Male] = GetBaseFromRange(6 * 12 + 6, 7 * 12);
            heights[CreatureConstants.Avoral][GenderConstants.Agender] = GetBaseFromRange(6 * 12 + 6, 7 * 12);
            heights[CreatureConstants.Avoral][CreatureConstants.Avoral] = GetMultiplierFromRange(6 * 12 + 6, 7 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Azer
            heights[CreatureConstants.Azer][GenderConstants.Male] = GetBaseFromAverage(5 * 12);
            heights[CreatureConstants.Azer][GenderConstants.Female] = GetBaseFromAverage(5 * 12);
            heights[CreatureConstants.Azer][GenderConstants.Agender] = GetBaseFromAverage(5 * 12);
            heights[CreatureConstants.Azer][CreatureConstants.Azer] = GetMultiplierFromAverage(5 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Babau
            heights[CreatureConstants.Babau][GenderConstants.Agender] = GetBaseFromRange(6 * 12, 7 * 12);
            heights[CreatureConstants.Babau][CreatureConstants.Babau] = GetMultiplierFromRange(6 * 12, 7 * 12);
            //Source: https://www.dimensions.com/element/mandrill-mandrillus-sphinx
            heights[CreatureConstants.Baboon][GenderConstants.Female] = GetBaseFromRange(20, 36);
            heights[CreatureConstants.Baboon][GenderConstants.Male] = GetBaseFromRange(20, 36);
            heights[CreatureConstants.Baboon][CreatureConstants.Baboon] = GetMultiplierFromRange(20, 36);
            //Source: https://www.dimensions.com/element/honey-badger-mellivora-capensis
            heights[CreatureConstants.Badger][GenderConstants.Female] = GetBaseFromRange(11, 16);
            heights[CreatureConstants.Badger][GenderConstants.Male] = GetBaseFromRange(11, 16);
            heights[CreatureConstants.Badger][CreatureConstants.Badger] = GetMultiplierFromRange(11, 16);
            //Multiplying up from normal badger. Length is about x2 from normal low, 2.5x for high
            heights[CreatureConstants.Badger_Dire][GenderConstants.Female] = GetBaseFromRange(22, 40);
            heights[CreatureConstants.Badger_Dire][GenderConstants.Male] = GetBaseFromRange(22, 40);
            heights[CreatureConstants.Badger_Dire][CreatureConstants.Badger_Dire] = GetMultiplierFromRange(22, 40);
            //Source: https://forgottenrealms.fandom.com/wiki/Balor
            heights[CreatureConstants.Balor][GenderConstants.Agender] = GetBaseFromAverage(12 * 12);
            heights[CreatureConstants.Balor][CreatureConstants.Balor] = GetMultiplierFromAverage(12 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Hamatula
            heights[CreatureConstants.BarbedDevil_Hamatula][GenderConstants.Agender] = GetBaseFromAverage(7 * 12);
            heights[CreatureConstants.BarbedDevil_Hamatula][CreatureConstants.BarbedDevil_Hamatula] = GetMultiplierFromAverage(7 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Barghest
            heights[CreatureConstants.Barghest][GenderConstants.Female] = GetBaseFromRange(5 * 12, 7 * 12);
            heights[CreatureConstants.Barghest][GenderConstants.Male] = GetBaseFromRange(5 * 12, 7 * 12);
            heights[CreatureConstants.Barghest][CreatureConstants.Barghest] = GetMultiplierFromRange(5 * 12, 7 * 12);
            heights[CreatureConstants.Barghest_Greater][GenderConstants.Female] = GetBaseFromRange(7 * 12, 9 * 12);
            heights[CreatureConstants.Barghest_Greater][GenderConstants.Male] = GetBaseFromRange(7 * 12, 9 * 12);
            heights[CreatureConstants.Barghest_Greater][CreatureConstants.Barghest_Greater] = GetMultiplierFromRange(7 * 12, 9 * 12);
            heights[CreatureConstants.Basilisk][GenderConstants.Female] = "0";
            heights[CreatureConstants.Basilisk][GenderConstants.Male] = "0";
            heights[CreatureConstants.Basilisk][CreatureConstants.Basilisk] = "0";
            heights[CreatureConstants.Basilisk_Greater][GenderConstants.Female] = "0";
            heights[CreatureConstants.Basilisk_Greater][GenderConstants.Male] = "0";
            heights[CreatureConstants.Basilisk_Greater][CreatureConstants.Basilisk_Greater] = "0";
            //Source: https://www.dimensions.com/element/little-brown-bat-myotis-lucifugus hanging height
            heights[CreatureConstants.Bat][GenderConstants.Female] = GetBaseFromRange(4, 5);
            heights[CreatureConstants.Bat][GenderConstants.Male] = GetBaseFromRange(4, 5);
            heights[CreatureConstants.Bat][CreatureConstants.Bat] = GetMultiplierFromRange(4, 5);
            //Scaled up from bat, x15 based on wingspan
            heights[CreatureConstants.Bat_Dire][GenderConstants.Female] = GetBaseFromRange(60, 75);
            heights[CreatureConstants.Bat_Dire][GenderConstants.Male] = GetBaseFromRange(60, 75);
            heights[CreatureConstants.Bat_Dire][CreatureConstants.Bat_Dire] = GetMultiplierFromRange(60, 75);
            //Source: https://www.d20srd.org/srd/monsters/swarm.htm
            heights[CreatureConstants.Bat_Swarm][GenderConstants.Agender] = GetBaseFromAverage(10 * 12);
            heights[CreatureConstants.Bat_Swarm][CreatureConstants.Bat_Swarm] = GetMultiplierFromAverage(10 * 12);
            //Source: https://www.dimensions.com/element/american-black-bear shoulder height
            heights[CreatureConstants.Bear_Black][GenderConstants.Female] = GetBaseFromRange(2 * 12 + 6, 3 * 12 + 1);
            heights[CreatureConstants.Bear_Black][GenderConstants.Male] = GetBaseFromRange(2 * 12 + 11, 3 * 12 + 5);
            heights[CreatureConstants.Bear_Black][CreatureConstants.Bear_Black] = GetMultiplierFromRange(2 * 12 + 11, 3 * 12 + 5);
            //Source: https://www.dimensions.com/element/grizzly-bear shoulder height
            heights[CreatureConstants.Bear_Brown][GenderConstants.Female] = GetBaseFromRange(3 * 12, 3 * 12 + 8);
            heights[CreatureConstants.Bear_Brown][GenderConstants.Male] = GetBaseFromRange(3 * 12 + 6, 4 * 12 + 6);
            heights[CreatureConstants.Bear_Brown][CreatureConstants.Bear_Brown] = GetMultiplierFromRange(3 * 12 + 6, 4 * 12 + 6);
            //Source: Scaled up from grizzly, x1.5
            heights[CreatureConstants.Bear_Dire][GenderConstants.Female] = GetBaseFromRange(63, 81);
            heights[CreatureConstants.Bear_Dire][GenderConstants.Male] = GetBaseFromRange(63, 81);
            heights[CreatureConstants.Bear_Dire][CreatureConstants.Bear_Dire] = GetMultiplierFromRange(63, 81);
            //Source: https://www.dimensions.com/element/polar-bears shoulder height
            heights[CreatureConstants.Bear_Polar][GenderConstants.Female] = GetBaseFromRange(2 * 12 + 8, 3 * 12 + 11);
            heights[CreatureConstants.Bear_Polar][GenderConstants.Male] = GetBaseFromRange(3 * 12 + 7, 5 * 12 + 3);
            heights[CreatureConstants.Bear_Polar][CreatureConstants.Bear_Polar] = GetMultiplierFromRange(3 * 12 + 7, 5 * 12 + 3);
            //Source: https://forgottenrealms.fandom.com/wiki/Barbazu
            heights[CreatureConstants.BeardedDevil_Barbazu][GenderConstants.Agender] = GetBaseFromAverage(6 * 12);
            heights[CreatureConstants.BeardedDevil_Barbazu][CreatureConstants.BeardedDevil_Barbazu] = GetMultiplierFromAverage(6 * 12);
            heights[CreatureConstants.Bebilith][GenderConstants.Agender] = "0";
            heights[CreatureConstants.Bebilith][CreatureConstants.Bebilith] = "0";
            //Source: https://www.d20srd.org/srd/monsters/giantBee.htm
            //https://www.dimensions.com/element/western-honey-bee-apis-mellifera scale up, [.12,.2]*5*12/[.39,.59] = [18,21]
            heights[CreatureConstants.Bee_Giant][GenderConstants.Male] = GetBaseFromRange(18, 21);
            heights[CreatureConstants.Bee_Giant][CreatureConstants.Bee_Giant] = GetMultiplierFromRange(18, 21);
            //Source: https://forgottenrealms.fandom.com/wiki/Behir
            heights[CreatureConstants.Behir][GenderConstants.Female] = "0";
            heights[CreatureConstants.Behir][GenderConstants.Male] = "0";
            heights[CreatureConstants.Behir][CreatureConstants.Behir] = "0";
            //Source: https://forgottenrealms.fandom.com/wiki/Beholder
            heights[CreatureConstants.Beholder][GenderConstants.Agender] = GetBaseFromAverage(8 * 12);
            heights[CreatureConstants.Beholder][CreatureConstants.Beholder] = GetMultiplierFromAverage(8 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Gauth
            heights[CreatureConstants.Beholder_Gauth][GenderConstants.Agender] = GetBaseFromRange(4 * 12, 6 * 12);
            heights[CreatureConstants.Beholder_Gauth][CreatureConstants.Beholder_Gauth] = GetMultiplierFromRange(4 * 12, 6 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Belker
            heights[CreatureConstants.Belker][GenderConstants.Agender] = GetBaseFromRange(7 * 12, 9 * 12);
            heights[CreatureConstants.Belker][CreatureConstants.Belker] = GetMultiplierFromRange(7 * 12, 9 * 12);
            //Source: https://www.dimensions.com/element/american-bison-bison-bison, withers height
            heights[CreatureConstants.Bison][GenderConstants.Female] = GetBaseFromRange(60, 78);
            heights[CreatureConstants.Bison][GenderConstants.Male] = GetBaseFromRange(60, 78);
            heights[CreatureConstants.Bison][CreatureConstants.Bison] = GetMultiplierFromRange(60, 78);
            //Source: https://forgottenrealms.fandom.com/wiki/Black_pudding
            heights[CreatureConstants.BlackPudding][GenderConstants.Agender] = GetBaseFromAverage(2 * 12);
            heights[CreatureConstants.BlackPudding][CreatureConstants.BlackPudding] = GetMultiplierFromAverage(2 * 12);
            //Elder is a size category up, so double dimensions
            heights[CreatureConstants.BlackPudding_Elder][GenderConstants.Agender] = GetBaseFromAverage(4 * 12);
            heights[CreatureConstants.BlackPudding_Elder][CreatureConstants.BlackPudding_Elder] = GetMultiplierFromAverage(4 * 12);
            //Source: https://www.d20pfsrd.com/bestiary/monster-listings/magical-beasts/blink-dog
            heights[CreatureConstants.BlinkDog][GenderConstants.Female] = GetBaseFromAverage(3 * 12);
            heights[CreatureConstants.BlinkDog][GenderConstants.Male] = GetBaseFromAverage(3 * 12);
            heights[CreatureConstants.BlinkDog][CreatureConstants.BlinkDog] = GetMultiplierFromAverage(3 * 12);
            //Source: https://www.dimensions.com/element/wild-boar
            heights[CreatureConstants.Boar][GenderConstants.Female] = GetBaseFromRange(2 * 12 + 6, 3 * 12);
            heights[CreatureConstants.Boar][GenderConstants.Male] = GetBaseFromRange(2 * 12 + 6, 3 * 12);
            heights[CreatureConstants.Boar][CreatureConstants.Boar] = GetMultiplierFromRange(2 * 12 + 6, 3 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Dire_boar
            heights[CreatureConstants.Boar_Dire][GenderConstants.Female] = GetBaseFromAverage(6 * 12);
            heights[CreatureConstants.Boar_Dire][GenderConstants.Male] = GetBaseFromAverage(6 * 12);
            heights[CreatureConstants.Boar_Dire][CreatureConstants.Boar_Dire] = GetMultiplierFromAverage(6 * 12);
            //INfo: Basing off of humans
            heights[CreatureConstants.Bodak][GenderConstants.Female] = "4*12+5";
            heights[CreatureConstants.Bodak][GenderConstants.Male] = "4*12+10";
            heights[CreatureConstants.Bodak][CreatureConstants.Bodak] = "2d10";
            //Source: https://web.stanford.edu/~cbross/bombbeetle.html
            heights[CreatureConstants.BombardierBeetle_Giant][GenderConstants.Female] = "0";
            heights[CreatureConstants.BombardierBeetle_Giant][GenderConstants.Male] = "0";
            heights[CreatureConstants.BombardierBeetle_Giant][CreatureConstants.BombardierBeetle_Giant] = "0";
            //Source: https://forgottenrealms.fandom.com/wiki/Osyluth
            heights[CreatureConstants.BoneDevil_Osyluth][GenderConstants.Agender] = GetBaseFromRange(9 * 12, 9 * 12 + 6);
            heights[CreatureConstants.BoneDevil_Osyluth][CreatureConstants.BoneDevil_Osyluth] = GetMultiplierFromRange(9 * 12, 9 * 12 + 6);
            //Source: https://forgottenrealms.fandom.com/wiki/Bralani
            heights[CreatureConstants.Bralani][GenderConstants.Female] = GetBaseFromAverage(5 * 12);
            heights[CreatureConstants.Bralani][GenderConstants.Male] = GetBaseFromAverage(5 * 12);
            heights[CreatureConstants.Bralani][CreatureConstants.Bralani] = GetMultiplierFromAverage(5 * 12);
            //Source: http://people.wku.edu/charles.plemons/ad&d/races/height.html
            heights[CreatureConstants.Bugbear][GenderConstants.Female] = "68";
            heights[CreatureConstants.Bugbear][GenderConstants.Male] = "72";
            heights[CreatureConstants.Bugbear][CreatureConstants.Bugbear] = "2d10";
            //Source: https://forgottenrealms.fandom.com/wiki/Bulette
            heights[CreatureConstants.Bulette][GenderConstants.Female] = GetBaseFromAverage(9 * 12);
            heights[CreatureConstants.Bulette][GenderConstants.Male] = GetBaseFromAverage(9 * 12);
            heights[CreatureConstants.Bulette][CreatureConstants.Bulette] = GetMultiplierFromAverage(9 * 12);
            //Source: https://www.dimensions.com/element/bactrian-camel
            heights[CreatureConstants.Camel_Bactrian][GenderConstants.Female] = GetBaseFromRange(62, 71);
            heights[CreatureConstants.Camel_Bactrian][GenderConstants.Male] = GetBaseFromRange(62, 71);
            heights[CreatureConstants.Camel_Bactrian][CreatureConstants.Camel_Bactrian] = GetMultiplierFromRange(62, 71);
            //Source: https://www.dimensions.com/element/dromedary-camel
            heights[CreatureConstants.Camel_Dromedary][GenderConstants.Female] = GetBaseFromRange(71, 78);
            heights[CreatureConstants.Camel_Dromedary][GenderConstants.Male] = GetBaseFromRange(71, 78);
            heights[CreatureConstants.Camel_Dromedary][CreatureConstants.Camel_Dromedary] = GetMultiplierFromRange(71, 78);
            //Source: https://forgottenrealms.fandom.com/wiki/Carrion_crawler
            heights[CreatureConstants.CarrionCrawler][GenderConstants.Female] = "0";
            heights[CreatureConstants.CarrionCrawler][GenderConstants.Male] = "0";
            heights[CreatureConstants.CarrionCrawler][CreatureConstants.CarrionCrawler] = "0";
            //Source: https://www.dimensions.com/element/american-shorthair-cat
            heights[CreatureConstants.Cat][GenderConstants.Female] = GetBaseFromRange(8, 10);
            heights[CreatureConstants.Cat][GenderConstants.Male] = GetBaseFromRange(8, 10);
            heights[CreatureConstants.Cat][CreatureConstants.Cat] = GetMultiplierFromRange(8, 10);
            //Source: http://people.wku.edu/charles.plemons/ad&d/races/height.html
            heights[CreatureConstants.Centaur][GenderConstants.Female] = "80";
            heights[CreatureConstants.Centaur][GenderConstants.Male] = "84";
            heights[CreatureConstants.Centaur][CreatureConstants.Centaur] = "3d12";
            //Source: https://www.d20srd.org/srd/monsters/swarm.htm
            heights[CreatureConstants.Centipede_Swarm][GenderConstants.Agender] = "0";
            heights[CreatureConstants.Centipede_Swarm][CreatureConstants.Centipede_Swarm] = "0";
            //Source: https://www.d20srd.org/srd/monsters/devil.htm#chainDevilKyton
            heights[CreatureConstants.ChainDevil_Kyton][GenderConstants.Agender] = GetBaseFromAverage(6 * 12);
            heights[CreatureConstants.ChainDevil_Kyton][GenderConstants.Female] = GetBaseFromAverage(6 * 12);
            heights[CreatureConstants.ChainDevil_Kyton][GenderConstants.Male] = GetBaseFromAverage(6 * 12);
            heights[CreatureConstants.ChainDevil_Kyton][CreatureConstants.ChainDevil_Kyton] = GetMultiplierFromAverage(6 * 12);
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
            //Source: https://www.mojobob.com/roleplay/monstrousmanual/s/sphinx.html
            heights[CreatureConstants.Criosphinx][GenderConstants.Male] = GetBaseFromAverage(7 * 12 + 6);
            heights[CreatureConstants.Criosphinx][CreatureConstants.Criosphinx] = GetMultiplierFromAverage(7 * 12 + 6);
            //Source: https://www.dimensions.com/element/nile-crocodile-crocodylus-niloticus
            heights[CreatureConstants.Crocodile][GenderConstants.Female] = GetBaseFromRange(12, 19);
            heights[CreatureConstants.Crocodile][GenderConstants.Male] = GetBaseFromRange(12, 19);
            heights[CreatureConstants.Crocodile][CreatureConstants.Crocodile] = GetMultiplierFromRange(12, 19);
            //Source: https://www.dimensions.com/element/saltwater-crocodile-crocodylus-porosus
            heights[CreatureConstants.Crocodile_Giant][GenderConstants.Female] = GetBaseFromRange(10, 30);
            heights[CreatureConstants.Crocodile_Giant][GenderConstants.Male] = GetBaseFromRange(10, 30);
            heights[CreatureConstants.Crocodile_Giant][CreatureConstants.Crocodile_Giant] = GetMultiplierFromRange(10, 30);
            //Source: https://forgottenrealms.fandom.com/wiki/Hydra half of length for height
            heights[CreatureConstants.Cryohydra_5Heads][GenderConstants.Female] = GetBaseFromAverage(20 * 12 / 2);
            heights[CreatureConstants.Cryohydra_5Heads][GenderConstants.Male] = GetBaseFromAverage(20 * 12 / 2);
            heights[CreatureConstants.Cryohydra_5Heads][CreatureConstants.Cryohydra_5Heads] = GetMultiplierFromAverage(20 * 12 / 2);
            heights[CreatureConstants.Cryohydra_6Heads][GenderConstants.Female] = GetBaseFromAverage(20 * 12 / 2);
            heights[CreatureConstants.Cryohydra_6Heads][GenderConstants.Male] = GetBaseFromAverage(20 * 12 / 2);
            heights[CreatureConstants.Cryohydra_6Heads][CreatureConstants.Cryohydra_6Heads] = GetMultiplierFromAverage(20 * 12 / 2);
            heights[CreatureConstants.Cryohydra_7Heads][GenderConstants.Female] = GetBaseFromAverage(20 * 12 / 2);
            heights[CreatureConstants.Cryohydra_7Heads][GenderConstants.Male] = GetBaseFromAverage(20 * 12 / 2);
            heights[CreatureConstants.Cryohydra_7Heads][CreatureConstants.Cryohydra_7Heads] = GetMultiplierFromAverage(20 * 12 / 2);
            heights[CreatureConstants.Cryohydra_8Heads][GenderConstants.Female] = GetBaseFromAverage(20 * 12 / 2);
            heights[CreatureConstants.Cryohydra_8Heads][GenderConstants.Male] = GetBaseFromAverage(20 * 12 / 2);
            heights[CreatureConstants.Cryohydra_8Heads][CreatureConstants.Cryohydra_8Heads] = GetMultiplierFromAverage(20 * 12 / 2);
            heights[CreatureConstants.Cryohydra_9Heads][GenderConstants.Female] = GetBaseFromAverage(20 * 12 / 2);
            heights[CreatureConstants.Cryohydra_9Heads][GenderConstants.Male] = GetBaseFromAverage(20 * 12 / 2);
            heights[CreatureConstants.Cryohydra_9Heads][CreatureConstants.Cryohydra_9Heads] = GetMultiplierFromAverage(20 * 12 / 2);
            heights[CreatureConstants.Cryohydra_10Heads][GenderConstants.Female] = GetBaseFromAverage(20 * 12 / 2);
            heights[CreatureConstants.Cryohydra_10Heads][GenderConstants.Male] = GetBaseFromAverage(20 * 12 / 2);
            heights[CreatureConstants.Cryohydra_10Heads][CreatureConstants.Cryohydra_10Heads] = GetMultiplierFromAverage(20 * 12 / 2);
            heights[CreatureConstants.Cryohydra_11Heads][GenderConstants.Female] = GetBaseFromAverage(20 * 12 / 2);
            heights[CreatureConstants.Cryohydra_11Heads][GenderConstants.Male] = GetBaseFromAverage(20 * 12 / 2);
            heights[CreatureConstants.Cryohydra_11Heads][CreatureConstants.Cryohydra_11Heads] = GetMultiplierFromAverage(20 * 12 / 2);
            heights[CreatureConstants.Cryohydra_12Heads][GenderConstants.Female] = GetBaseFromAverage(20 * 12 / 2);
            heights[CreatureConstants.Cryohydra_12Heads][GenderConstants.Male] = GetBaseFromAverage(20 * 12 / 2);
            heights[CreatureConstants.Cryohydra_12Heads][CreatureConstants.Cryohydra_5Heads] = GetMultiplierFromAverage(20 * 12 / 2);
            //Source: https://forgottenrealms.fandom.com/wiki/Darkmantle
            heights[CreatureConstants.Darkmantle][GenderConstants.Hermaphrodite] = "0";
            heights[CreatureConstants.Darkmantle][CreatureConstants.Darkmantle] = "0";
            //Source: https://www.dimensions.com/element/deinonychus-deinonychus-antirrhopus
            heights[CreatureConstants.Deinonychus][GenderConstants.Female] = GetBaseFromRange(34, 57);
            heights[CreatureConstants.Deinonychus][GenderConstants.Male] = GetBaseFromRange(34, 57);
            heights[CreatureConstants.Deinonychus][CreatureConstants.Deinonychus] = GetMultiplierFromRange(34, 57);
            //Source: https://dungeonsdragons.fandom.com/wiki/Delver
            heights[CreatureConstants.Delver][GenderConstants.Female] = GetBaseFromAverage(12 * 12);
            heights[CreatureConstants.Delver][GenderConstants.Male] = GetBaseFromAverage(12 * 12);
            heights[CreatureConstants.Delver][CreatureConstants.Delver] = GetMultiplierFromAverage(12 * 12);
            //Source: https://monster.fandom.com/wiki/Derro
            heights[CreatureConstants.Derro][GenderConstants.Female] = GetBaseFromRange(3 * 12, 4 * 12);
            heights[CreatureConstants.Derro][GenderConstants.Male] = GetBaseFromRange(3 * 12, 4 * 12);
            heights[CreatureConstants.Derro][CreatureConstants.Derro] = GetMultiplierFromRange(3 * 12, 4 * 12);
            heights[CreatureConstants.Derro_Sane][GenderConstants.Female] = GetBaseFromRange(3 * 12, 4 * 12);
            heights[CreatureConstants.Derro_Sane][GenderConstants.Male] = GetBaseFromRange(3 * 12, 4 * 12);
            heights[CreatureConstants.Derro_Sane][CreatureConstants.Derro_Sane] = GetMultiplierFromRange(3 * 12, 4 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Destrachan
            heights[CreatureConstants.Destrachan][GenderConstants.Female] = "0";
            heights[CreatureConstants.Destrachan][GenderConstants.Male] = "0";
            heights[CreatureConstants.Destrachan][CreatureConstants.Destrachan] = "0";
            //Source: https://www.d20srd.org/srd/monsters/devourer.htm
            heights[CreatureConstants.Devourer][GenderConstants.Agender] = GetBaseFromAverage(9 * 12);
            heights[CreatureConstants.Devourer][CreatureConstants.Devourer] = GetMultiplierFromAverage(9 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Digester
            heights[CreatureConstants.Digester][GenderConstants.Female] = GetBaseFromAverage(5 * 12);
            heights[CreatureConstants.Digester][GenderConstants.Male] = GetBaseFromAverage(5 * 12);
            heights[CreatureConstants.Digester][CreatureConstants.Digester] = GetMultiplierFromAverage(5 * 12);
            //Scaled down from Pack Lord, x2 based on length
            heights[CreatureConstants.DisplacerBeast][GenderConstants.Female] = GetBaseFromAverage(5 * 12);
            heights[CreatureConstants.DisplacerBeast][GenderConstants.Male] = GetBaseFromAverage(5 * 12);
            heights[CreatureConstants.DisplacerBeast][CreatureConstants.DisplacerBeast] = GetMultiplierFromAverage(5 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Displacer_beast
            heights[CreatureConstants.DisplacerBeast_PackLord][GenderConstants.Female] = GetBaseFromAverage(10 * 12);
            heights[CreatureConstants.DisplacerBeast_PackLord][GenderConstants.Male] = GetBaseFromAverage(10 * 12);
            heights[CreatureConstants.DisplacerBeast_PackLord][CreatureConstants.DisplacerBeast_PackLord] = GetMultiplierFromAverage(10 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Djinni
            heights[CreatureConstants.Djinni][GenderConstants.Agender] = GetBaseFromAverage(10 * 12 + 6);
            heights[CreatureConstants.Djinni][GenderConstants.Female] = GetBaseFromAverage(10 * 12 + 6);
            heights[CreatureConstants.Djinni][GenderConstants.Male] = GetBaseFromAverage(10 * 12 + 6);
            heights[CreatureConstants.Djinni][CreatureConstants.Djinni] = GetMultiplierFromAverage(10 * 12 + 6);
            //Source: https://forgottenrealms.fandom.com/wiki/Noble_djinni
            heights[CreatureConstants.Djinni_Noble][GenderConstants.Agender] = GetBaseFromAverage(12 * 12);
            heights[CreatureConstants.Djinni_Noble][GenderConstants.Female] = GetBaseFromAverage(12 * 12);
            heights[CreatureConstants.Djinni_Noble][GenderConstants.Male] = GetBaseFromAverage(12 * 12);
            heights[CreatureConstants.Djinni_Noble][CreatureConstants.Djinni_Noble] = GetMultiplierFromAverage(12 * 12);
            //Source: https://www.dimensions.com/search?query=dog average of various dogs in the 20-50 pound weight range, including coyote
            heights[CreatureConstants.Dog][GenderConstants.Female] = GetBaseFromRange(16, 25);
            heights[CreatureConstants.Dog][GenderConstants.Male] = GetBaseFromRange(16, 25);
            heights[CreatureConstants.Dog][CreatureConstants.Dog] = GetMultiplierFromRange(16, 25);
            //Source: https://www.dimensions.com/element/saint-bernard-dog M:28-30,F:26-28
            //https://www.dimensions.com/element/siberian-husky 20-24
            //https://www.dimensions.com/element/dogs-collie M:22-24,F:20-22
            heights[CreatureConstants.Dog_Riding][GenderConstants.Female] = GetBaseFromRange(20, 28);
            heights[CreatureConstants.Dog_Riding][GenderConstants.Male] = GetBaseFromRange(22, 30);
            heights[CreatureConstants.Dog_Riding][CreatureConstants.Dog_Riding] = GetMultiplierFromRange(22, 30);
            //Source: https://www.dimensions.com/element/donkey-equus-africanus-asinus
            heights[CreatureConstants.Donkey][GenderConstants.Female] = GetBaseFromRange(36, 48);
            heights[CreatureConstants.Donkey][GenderConstants.Male] = GetBaseFromRange(36, 48);
            heights[CreatureConstants.Donkey][CreatureConstants.Donkey] = GetMultiplierFromRange(36, 48);
            //Source: https://forgottenrealms.fandom.com/wiki/Doppelganger
            heights[CreatureConstants.Doppelganger][GenderConstants.Agender] = GetBaseFromAverage(5 * 12);
            heights[CreatureConstants.Doppelganger][CreatureConstants.Doppelganger] = GetMultiplierFromAverage(5 * 12);
            //Source: Draconomicon
            heights[CreatureConstants.Dragon_Black_Wyrmling][GenderConstants.Female] = GetBaseFromAverage(12);
            heights[CreatureConstants.Dragon_Black_Wyrmling][GenderConstants.Male] = GetBaseFromAverage(12);
            heights[CreatureConstants.Dragon_Black_Wyrmling][CreatureConstants.Dragon_Black_Wyrmling] = GetMultiplierFromAverage(12);
            heights[CreatureConstants.Dragon_Black_VeryYoung][GenderConstants.Female] = GetBaseFromAverage(24);
            heights[CreatureConstants.Dragon_Black_VeryYoung][GenderConstants.Male] = GetBaseFromAverage(24);
            heights[CreatureConstants.Dragon_Black_VeryYoung][CreatureConstants.Dragon_Black_VeryYoung] = GetMultiplierFromAverage(24);
            heights[CreatureConstants.Dragon_Black_Young][GenderConstants.Female] = GetBaseFromAverage(4 * 12);
            heights[CreatureConstants.Dragon_Black_Young][GenderConstants.Male] = GetBaseFromAverage(4 * 12);
            heights[CreatureConstants.Dragon_Black_Young][CreatureConstants.Dragon_Black_Young] = GetMultiplierFromAverage(4 * 12);
            heights[CreatureConstants.Dragon_Black_Juvenile][GenderConstants.Female] = GetBaseFromAverage(4 * 12);
            heights[CreatureConstants.Dragon_Black_Juvenile][GenderConstants.Male] = GetBaseFromAverage(4 * 12);
            heights[CreatureConstants.Dragon_Black_Juvenile][CreatureConstants.Dragon_Black_Juvenile] = GetMultiplierFromAverage(4 * 12);
            heights[CreatureConstants.Dragon_Black_YoungAdult][GenderConstants.Female] = GetBaseFromAverage(7 * 12);
            heights[CreatureConstants.Dragon_Black_YoungAdult][GenderConstants.Male] = GetBaseFromAverage(7 * 12);
            heights[CreatureConstants.Dragon_Black_YoungAdult][CreatureConstants.Dragon_Black_YoungAdult] = GetMultiplierFromAverage(7 * 12);
            heights[CreatureConstants.Dragon_Black_Adult][GenderConstants.Female] = GetBaseFromAverage(7 * 12);
            heights[CreatureConstants.Dragon_Black_Adult][GenderConstants.Male] = GetBaseFromAverage(7 * 12);
            heights[CreatureConstants.Dragon_Black_Adult][CreatureConstants.Dragon_Black_Adult] = GetMultiplierFromAverage(7 * 12);
            heights[CreatureConstants.Dragon_Black_MatureAdult][GenderConstants.Female] = GetBaseFromAverage(12 * 12);
            heights[CreatureConstants.Dragon_Black_MatureAdult][GenderConstants.Male] = GetBaseFromAverage(12 * 12);
            heights[CreatureConstants.Dragon_Black_MatureAdult][CreatureConstants.Dragon_Black_MatureAdult] = GetMultiplierFromAverage(12 * 12);
            heights[CreatureConstants.Dragon_Black_Old][GenderConstants.Female] = GetBaseFromAverage(12 * 12);
            heights[CreatureConstants.Dragon_Black_Old][GenderConstants.Male] = GetBaseFromAverage(12 * 12);
            heights[CreatureConstants.Dragon_Black_Old][CreatureConstants.Dragon_Black_Old] = GetMultiplierFromAverage(12 * 12);
            heights[CreatureConstants.Dragon_Black_VeryOld][GenderConstants.Female] = GetBaseFromAverage(12 * 12);
            heights[CreatureConstants.Dragon_Black_VeryOld][GenderConstants.Male] = GetBaseFromAverage(12 * 12);
            heights[CreatureConstants.Dragon_Black_VeryOld][CreatureConstants.Dragon_Black_VeryOld] = GetMultiplierFromAverage(12 * 12);
            heights[CreatureConstants.Dragon_Black_Ancient][GenderConstants.Female] = GetBaseFromAverage(12 * 12);
            heights[CreatureConstants.Dragon_Black_Ancient][GenderConstants.Male] = GetBaseFromAverage(12 * 12);
            heights[CreatureConstants.Dragon_Black_Ancient][CreatureConstants.Dragon_Black_Ancient] = GetMultiplierFromAverage(12 * 12);
            heights[CreatureConstants.Dragon_Black_Wyrm][GenderConstants.Female] = GetBaseFromAverage(16 * 12);
            heights[CreatureConstants.Dragon_Black_Wyrm][GenderConstants.Male] = GetBaseFromAverage(16 * 12);
            heights[CreatureConstants.Dragon_Black_Wyrm][CreatureConstants.Dragon_Black_Wyrm] = GetMultiplierFromAverage(16 * 12);
            heights[CreatureConstants.Dragon_Black_GreatWyrm][GenderConstants.Female] = GetBaseFromAverage(16 * 12);
            heights[CreatureConstants.Dragon_Black_GreatWyrm][GenderConstants.Male] = GetBaseFromAverage(16 * 12);
            heights[CreatureConstants.Dragon_Black_GreatWyrm][CreatureConstants.Dragon_Black_GreatWyrm] = GetMultiplierFromAverage(16 * 12);
            heights[CreatureConstants.Dragon_Blue_Wyrmling][GenderConstants.Female] = GetBaseFromAverage(24);
            heights[CreatureConstants.Dragon_Blue_Wyrmling][GenderConstants.Male] = GetBaseFromAverage(24);
            heights[CreatureConstants.Dragon_Blue_Wyrmling][CreatureConstants.Dragon_Blue_Wyrmling] = GetMultiplierFromAverage(24);
            heights[CreatureConstants.Dragon_Blue_VeryYoung][GenderConstants.Female] = GetBaseFromAverage(4 * 12);
            heights[CreatureConstants.Dragon_Blue_VeryYoung][GenderConstants.Male] = GetBaseFromAverage(4 * 12);
            heights[CreatureConstants.Dragon_Blue_VeryYoung][CreatureConstants.Dragon_Blue_VeryYoung] = GetMultiplierFromAverage(4 * 12);
            heights[CreatureConstants.Dragon_Blue_Young][GenderConstants.Female] = GetBaseFromAverage(4 * 12);
            heights[CreatureConstants.Dragon_Blue_Young][GenderConstants.Male] = GetBaseFromAverage(4 * 12);
            heights[CreatureConstants.Dragon_Blue_Young][CreatureConstants.Dragon_Blue_Young] = GetMultiplierFromAverage(4 * 12);
            heights[CreatureConstants.Dragon_Blue_Juvenile][GenderConstants.Female] = GetBaseFromAverage(7 * 12);
            heights[CreatureConstants.Dragon_Blue_Juvenile][GenderConstants.Male] = GetBaseFromAverage(7 * 12);
            heights[CreatureConstants.Dragon_Blue_Juvenile][CreatureConstants.Dragon_Blue_Juvenile] = GetMultiplierFromAverage(7 * 12);
            heights[CreatureConstants.Dragon_Blue_YoungAdult][GenderConstants.Female] = GetBaseFromAverage(7 * 12);
            heights[CreatureConstants.Dragon_Blue_YoungAdult][GenderConstants.Male] = GetBaseFromAverage(7 * 12);
            heights[CreatureConstants.Dragon_Blue_YoungAdult][CreatureConstants.Dragon_Blue_YoungAdult] = GetMultiplierFromAverage(7 * 12);
            heights[CreatureConstants.Dragon_Blue_Adult][GenderConstants.Female] = GetBaseFromAverage(12 * 12);
            heights[CreatureConstants.Dragon_Blue_Adult][GenderConstants.Male] = GetBaseFromAverage(12 * 12);
            heights[CreatureConstants.Dragon_Blue_Adult][CreatureConstants.Dragon_Blue_Adult] = GetMultiplierFromAverage(12 * 12);
            heights[CreatureConstants.Dragon_Blue_MatureAdult][GenderConstants.Female] = GetBaseFromAverage(12 * 12);
            heights[CreatureConstants.Dragon_Blue_MatureAdult][GenderConstants.Male] = GetBaseFromAverage(12 * 12);
            heights[CreatureConstants.Dragon_Blue_MatureAdult][CreatureConstants.Dragon_Blue_MatureAdult] = GetMultiplierFromAverage(12 * 12);
            heights[CreatureConstants.Dragon_Blue_Old][GenderConstants.Female] = GetBaseFromAverage(12 * 12);
            heights[CreatureConstants.Dragon_Blue_Old][GenderConstants.Male] = GetBaseFromAverage(12 * 12);
            heights[CreatureConstants.Dragon_Blue_Old][CreatureConstants.Dragon_Blue_Old] = GetMultiplierFromAverage(12 * 12);
            heights[CreatureConstants.Dragon_Blue_VeryOld][GenderConstants.Female] = GetBaseFromAverage(12 * 12);
            heights[CreatureConstants.Dragon_Blue_VeryOld][GenderConstants.Male] = GetBaseFromAverage(12 * 12);
            heights[CreatureConstants.Dragon_Blue_VeryOld][CreatureConstants.Dragon_Blue_VeryOld] = GetMultiplierFromAverage(12 * 12);
            heights[CreatureConstants.Dragon_Blue_Ancient][GenderConstants.Female] = GetBaseFromAverage(16 * 12);
            heights[CreatureConstants.Dragon_Blue_Ancient][GenderConstants.Male] = GetBaseFromAverage(16 * 12);
            heights[CreatureConstants.Dragon_Blue_Ancient][CreatureConstants.Dragon_Blue_Ancient] = GetMultiplierFromAverage(16 * 12);
            heights[CreatureConstants.Dragon_Blue_Wyrm][GenderConstants.Female] = GetBaseFromAverage(16 * 12);
            heights[CreatureConstants.Dragon_Blue_Wyrm][GenderConstants.Male] = GetBaseFromAverage(16 * 12);
            heights[CreatureConstants.Dragon_Blue_Wyrm][CreatureConstants.Dragon_Blue_Wyrm] = GetMultiplierFromAverage(16 * 12);
            heights[CreatureConstants.Dragon_Blue_GreatWyrm][GenderConstants.Female] = GetBaseFromAverage(16 * 12);
            heights[CreatureConstants.Dragon_Blue_GreatWyrm][GenderConstants.Male] = GetBaseFromAverage(16 * 12);
            heights[CreatureConstants.Dragon_Blue_GreatWyrm][CreatureConstants.Dragon_Blue_GreatWyrm] = GetMultiplierFromAverage(16 * 12);
            heights[CreatureConstants.Dragon_Brass_Wyrmling][GenderConstants.Female] = GetBaseFromAverage(12);
            heights[CreatureConstants.Dragon_Brass_Wyrmling][GenderConstants.Male] = GetBaseFromAverage(12);
            heights[CreatureConstants.Dragon_Brass_Wyrmling][CreatureConstants.Dragon_Brass_Wyrmling] = GetMultiplierFromAverage(12);
            heights[CreatureConstants.Dragon_Brass_VeryYoung][GenderConstants.Female] = GetBaseFromAverage(24);
            heights[CreatureConstants.Dragon_Brass_VeryYoung][GenderConstants.Male] = GetBaseFromAverage(24);
            heights[CreatureConstants.Dragon_Brass_VeryYoung][CreatureConstants.Dragon_Brass_VeryYoung] = GetMultiplierFromAverage(24);
            heights[CreatureConstants.Dragon_Brass_Young][GenderConstants.Female] = GetBaseFromAverage(4 * 12);
            heights[CreatureConstants.Dragon_Brass_Young][GenderConstants.Male] = GetBaseFromAverage(4 * 12);
            heights[CreatureConstants.Dragon_Brass_Young][CreatureConstants.Dragon_Brass_Young] = GetMultiplierFromAverage(4 * 12);
            heights[CreatureConstants.Dragon_Brass_Juvenile][GenderConstants.Female] = GetBaseFromAverage(4 * 12);
            heights[CreatureConstants.Dragon_Brass_Juvenile][GenderConstants.Male] = GetBaseFromAverage(4 * 12);
            heights[CreatureConstants.Dragon_Brass_Juvenile][CreatureConstants.Dragon_Brass_Juvenile] = GetMultiplierFromAverage(4 * 12);
            heights[CreatureConstants.Dragon_Brass_YoungAdult][GenderConstants.Female] = GetBaseFromAverage(7 * 12);
            heights[CreatureConstants.Dragon_Brass_YoungAdult][GenderConstants.Male] = GetBaseFromAverage(7 * 12);
            heights[CreatureConstants.Dragon_Brass_YoungAdult][CreatureConstants.Dragon_Brass_YoungAdult] = GetMultiplierFromAverage(7 * 12);
            heights[CreatureConstants.Dragon_Brass_Adult][GenderConstants.Female] = GetBaseFromAverage(7 * 12);
            heights[CreatureConstants.Dragon_Brass_Adult][GenderConstants.Male] = GetBaseFromAverage(7 * 12);
            heights[CreatureConstants.Dragon_Brass_Adult][CreatureConstants.Dragon_Brass_Adult] = GetMultiplierFromAverage(7 * 12);
            heights[CreatureConstants.Dragon_Brass_MatureAdult][GenderConstants.Female] = GetBaseFromAverage(12 * 12);
            heights[CreatureConstants.Dragon_Brass_MatureAdult][GenderConstants.Male] = GetBaseFromAverage(12 * 12);
            heights[CreatureConstants.Dragon_Brass_MatureAdult][CreatureConstants.Dragon_Brass_MatureAdult] = GetMultiplierFromAverage(12 * 12);
            heights[CreatureConstants.Dragon_Brass_Old][GenderConstants.Female] = GetBaseFromAverage(12 * 12);
            heights[CreatureConstants.Dragon_Brass_Old][GenderConstants.Male] = GetBaseFromAverage(12 * 12);
            heights[CreatureConstants.Dragon_Brass_Old][CreatureConstants.Dragon_Brass_Old] = GetMultiplierFromAverage(12 * 12);
            heights[CreatureConstants.Dragon_Brass_VeryOld][GenderConstants.Female] = GetBaseFromAverage(12 * 12);
            heights[CreatureConstants.Dragon_Brass_VeryOld][GenderConstants.Male] = GetBaseFromAverage(12 * 12);
            heights[CreatureConstants.Dragon_Brass_VeryOld][CreatureConstants.Dragon_Brass_VeryOld] = GetMultiplierFromAverage(12 * 12);
            heights[CreatureConstants.Dragon_Brass_Ancient][GenderConstants.Female] = GetBaseFromAverage(12 * 12);
            heights[CreatureConstants.Dragon_Brass_Ancient][GenderConstants.Male] = GetBaseFromAverage(12 * 12);
            heights[CreatureConstants.Dragon_Brass_Ancient][CreatureConstants.Dragon_Brass_Ancient] = GetMultiplierFromAverage(12 * 12);
            heights[CreatureConstants.Dragon_Brass_Wyrm][GenderConstants.Female] = GetBaseFromAverage(16 * 12);
            heights[CreatureConstants.Dragon_Brass_Wyrm][GenderConstants.Male] = GetBaseFromAverage(16 * 12);
            heights[CreatureConstants.Dragon_Brass_Wyrm][CreatureConstants.Dragon_Brass_Wyrm] = GetMultiplierFromAverage(16 * 12);
            heights[CreatureConstants.Dragon_Brass_GreatWyrm][GenderConstants.Female] = GetBaseFromAverage(16 * 12);
            heights[CreatureConstants.Dragon_Brass_GreatWyrm][GenderConstants.Male] = GetBaseFromAverage(16 * 12);
            heights[CreatureConstants.Dragon_Brass_GreatWyrm][CreatureConstants.Dragon_Brass_GreatWyrm] = GetMultiplierFromAverage(16 * 12);
            heights[CreatureConstants.Dragon_Bronze_Wyrmling][GenderConstants.Female] = GetBaseFromAverage(24);
            heights[CreatureConstants.Dragon_Bronze_Wyrmling][GenderConstants.Male] = GetBaseFromAverage(24);
            heights[CreatureConstants.Dragon_Bronze_Wyrmling][CreatureConstants.Dragon_Bronze_Wyrmling] = GetMultiplierFromAverage(24);
            heights[CreatureConstants.Dragon_Bronze_VeryYoung][GenderConstants.Female] = GetBaseFromAverage(4 * 12);
            heights[CreatureConstants.Dragon_Bronze_VeryYoung][GenderConstants.Male] = GetBaseFromAverage(4 * 12);
            heights[CreatureConstants.Dragon_Bronze_VeryYoung][CreatureConstants.Dragon_Bronze_VeryYoung] = GetMultiplierFromAverage(4 * 12);
            heights[CreatureConstants.Dragon_Bronze_Young][GenderConstants.Female] = GetBaseFromAverage(4 * 12);
            heights[CreatureConstants.Dragon_Bronze_Young][GenderConstants.Male] = GetBaseFromAverage(4 * 12);
            heights[CreatureConstants.Dragon_Bronze_Young][CreatureConstants.Dragon_Bronze_Young] = GetMultiplierFromAverage(4 * 12);
            heights[CreatureConstants.Dragon_Bronze_Juvenile][GenderConstants.Female] = GetBaseFromAverage(7 * 12);
            heights[CreatureConstants.Dragon_Bronze_Juvenile][GenderConstants.Male] = GetBaseFromAverage(7 * 12);
            heights[CreatureConstants.Dragon_Bronze_Juvenile][CreatureConstants.Dragon_Bronze_Juvenile] = GetMultiplierFromAverage(7 * 12);
            heights[CreatureConstants.Dragon_Bronze_YoungAdult][GenderConstants.Female] = GetBaseFromAverage(7 * 12);
            heights[CreatureConstants.Dragon_Bronze_YoungAdult][GenderConstants.Male] = GetBaseFromAverage(7 * 12);
            heights[CreatureConstants.Dragon_Bronze_YoungAdult][CreatureConstants.Dragon_Bronze_YoungAdult] = GetMultiplierFromAverage(7 * 12);
            heights[CreatureConstants.Dragon_Bronze_Adult][GenderConstants.Female] = GetBaseFromAverage(12 * 12);
            heights[CreatureConstants.Dragon_Bronze_Adult][GenderConstants.Male] = GetBaseFromAverage(12 * 12);
            heights[CreatureConstants.Dragon_Bronze_Adult][CreatureConstants.Dragon_Bronze_Adult] = GetMultiplierFromAverage(12 * 12);
            heights[CreatureConstants.Dragon_Bronze_MatureAdult][GenderConstants.Female] = GetBaseFromAverage(12 * 12);
            heights[CreatureConstants.Dragon_Bronze_MatureAdult][GenderConstants.Male] = GetBaseFromAverage(12 * 12);
            heights[CreatureConstants.Dragon_Bronze_MatureAdult][CreatureConstants.Dragon_Bronze_MatureAdult] = GetMultiplierFromAverage(12 * 12);
            heights[CreatureConstants.Dragon_Bronze_Old][GenderConstants.Female] = GetBaseFromAverage(12 * 12);
            heights[CreatureConstants.Dragon_Bronze_Old][GenderConstants.Male] = GetBaseFromAverage(12 * 12);
            heights[CreatureConstants.Dragon_Bronze_Old][CreatureConstants.Dragon_Bronze_Old] = GetMultiplierFromAverage(12 * 12);
            heights[CreatureConstants.Dragon_Bronze_VeryOld][GenderConstants.Female] = GetBaseFromAverage(12 * 12);
            heights[CreatureConstants.Dragon_Bronze_VeryOld][GenderConstants.Male] = GetBaseFromAverage(12 * 12);
            heights[CreatureConstants.Dragon_Bronze_VeryOld][CreatureConstants.Dragon_Bronze_VeryOld] = GetMultiplierFromAverage(12 * 12);
            heights[CreatureConstants.Dragon_Bronze_Ancient][GenderConstants.Female] = GetBaseFromAverage(16 * 12);
            heights[CreatureConstants.Dragon_Bronze_Ancient][GenderConstants.Male] = GetBaseFromAverage(16 * 12);
            heights[CreatureConstants.Dragon_Bronze_Ancient][CreatureConstants.Dragon_Bronze_Ancient] = GetMultiplierFromAverage(16 * 12);
            heights[CreatureConstants.Dragon_Bronze_Wyrm][GenderConstants.Female] = GetBaseFromAverage(16 * 12);
            heights[CreatureConstants.Dragon_Bronze_Wyrm][GenderConstants.Male] = GetBaseFromAverage(16 * 12);
            heights[CreatureConstants.Dragon_Bronze_Wyrm][CreatureConstants.Dragon_Bronze_Wyrm] = GetMultiplierFromAverage(16 * 12);
            heights[CreatureConstants.Dragon_Bronze_GreatWyrm][GenderConstants.Female] = GetBaseFromAverage(16 * 12);
            heights[CreatureConstants.Dragon_Bronze_GreatWyrm][GenderConstants.Male] = GetBaseFromAverage(16 * 12);
            heights[CreatureConstants.Dragon_Bronze_GreatWyrm][CreatureConstants.Dragon_Bronze_GreatWyrm] = GetMultiplierFromAverage(16 * 12);
            heights[CreatureConstants.Dragon_Copper_Wyrmling][GenderConstants.Female] = GetBaseFromAverage(12);
            heights[CreatureConstants.Dragon_Copper_Wyrmling][GenderConstants.Male] = GetBaseFromAverage(12);
            heights[CreatureConstants.Dragon_Copper_Wyrmling][CreatureConstants.Dragon_Copper_Wyrmling] = GetMultiplierFromAverage(12);
            heights[CreatureConstants.Dragon_Copper_VeryYoung][GenderConstants.Female] = GetBaseFromAverage(24);
            heights[CreatureConstants.Dragon_Copper_VeryYoung][GenderConstants.Male] = GetBaseFromAverage(24);
            heights[CreatureConstants.Dragon_Copper_VeryYoung][CreatureConstants.Dragon_Copper_VeryYoung] = GetMultiplierFromAverage(24);
            heights[CreatureConstants.Dragon_Copper_Young][GenderConstants.Female] = GetBaseFromAverage(4 * 12);
            heights[CreatureConstants.Dragon_Copper_Young][GenderConstants.Male] = GetBaseFromAverage(4 * 12);
            heights[CreatureConstants.Dragon_Copper_Young][CreatureConstants.Dragon_Copper_Young] = GetMultiplierFromAverage(4 * 12);
            heights[CreatureConstants.Dragon_Copper_Juvenile][GenderConstants.Female] = GetBaseFromAverage(4 * 12);
            heights[CreatureConstants.Dragon_Copper_Juvenile][GenderConstants.Male] = GetBaseFromAverage(4 * 12);
            heights[CreatureConstants.Dragon_Copper_Juvenile][CreatureConstants.Dragon_Copper_Juvenile] = GetMultiplierFromAverage(4 * 12);
            heights[CreatureConstants.Dragon_Copper_YoungAdult][GenderConstants.Female] = GetBaseFromAverage(7 * 12);
            heights[CreatureConstants.Dragon_Copper_YoungAdult][GenderConstants.Male] = GetBaseFromAverage(7 * 12);
            heights[CreatureConstants.Dragon_Copper_YoungAdult][CreatureConstants.Dragon_Copper_YoungAdult] = GetMultiplierFromAverage(7 * 12);
            heights[CreatureConstants.Dragon_Copper_Adult][GenderConstants.Female] = GetBaseFromAverage(7 * 12);
            heights[CreatureConstants.Dragon_Copper_Adult][GenderConstants.Male] = GetBaseFromAverage(7 * 12);
            heights[CreatureConstants.Dragon_Copper_Adult][CreatureConstants.Dragon_Copper_Adult] = GetMultiplierFromAverage(7 * 12);
            heights[CreatureConstants.Dragon_Copper_MatureAdult][GenderConstants.Female] = GetBaseFromAverage(12 * 12);
            heights[CreatureConstants.Dragon_Copper_MatureAdult][GenderConstants.Male] = GetBaseFromAverage(12 * 12);
            heights[CreatureConstants.Dragon_Copper_MatureAdult][CreatureConstants.Dragon_Copper_MatureAdult] = GetMultiplierFromAverage(12 * 12);
            heights[CreatureConstants.Dragon_Copper_Old][GenderConstants.Female] = GetBaseFromAverage(12 * 12);
            heights[CreatureConstants.Dragon_Copper_Old][GenderConstants.Male] = GetBaseFromAverage(12 * 12);
            heights[CreatureConstants.Dragon_Copper_Old][CreatureConstants.Dragon_Copper_Old] = GetMultiplierFromAverage(12 * 12);
            heights[CreatureConstants.Dragon_Copper_VeryOld][GenderConstants.Female] = GetBaseFromAverage(12 * 12);
            heights[CreatureConstants.Dragon_Copper_VeryOld][GenderConstants.Male] = GetBaseFromAverage(12 * 12);
            heights[CreatureConstants.Dragon_Copper_VeryOld][CreatureConstants.Dragon_Copper_VeryOld] = GetMultiplierFromAverage(12 * 12);
            heights[CreatureConstants.Dragon_Copper_Ancient][GenderConstants.Female] = GetBaseFromAverage(12 * 12);
            heights[CreatureConstants.Dragon_Copper_Ancient][GenderConstants.Male] = GetBaseFromAverage(12 * 12);
            heights[CreatureConstants.Dragon_Copper_Ancient][CreatureConstants.Dragon_Copper_Ancient] = GetMultiplierFromAverage(12 * 12);
            heights[CreatureConstants.Dragon_Copper_Wyrm][GenderConstants.Female] = GetBaseFromAverage(16 * 12);
            heights[CreatureConstants.Dragon_Copper_Wyrm][GenderConstants.Male] = GetBaseFromAverage(16 * 12);
            heights[CreatureConstants.Dragon_Copper_Wyrm][CreatureConstants.Dragon_Copper_Wyrm] = GetMultiplierFromAverage(16 * 12);
            heights[CreatureConstants.Dragon_Copper_GreatWyrm][GenderConstants.Female] = GetBaseFromAverage(16 * 12);
            heights[CreatureConstants.Dragon_Copper_GreatWyrm][GenderConstants.Male] = GetBaseFromAverage(16 * 12);
            heights[CreatureConstants.Dragon_Copper_GreatWyrm][CreatureConstants.Dragon_Copper_GreatWyrm] = GetMultiplierFromAverage(16 * 12);
            heights[CreatureConstants.Dragon_Gold_Wyrmling][GenderConstants.Female] = GetBaseFromAverage(4 * 12);
            heights[CreatureConstants.Dragon_Gold_Wyrmling][GenderConstants.Male] = GetBaseFromAverage(4 * 12);
            heights[CreatureConstants.Dragon_Gold_Wyrmling][CreatureConstants.Dragon_Gold_Wyrmling] = GetMultiplierFromAverage(4 * 12);
            heights[CreatureConstants.Dragon_Gold_VeryYoung][GenderConstants.Female] = GetBaseFromAverage(7 * 12);
            heights[CreatureConstants.Dragon_Gold_VeryYoung][GenderConstants.Male] = GetBaseFromAverage(7 * 12);
            heights[CreatureConstants.Dragon_Gold_VeryYoung][CreatureConstants.Dragon_Gold_VeryYoung] = GetMultiplierFromAverage(7 * 12);
            heights[CreatureConstants.Dragon_Gold_Young][GenderConstants.Female] = GetBaseFromAverage(7 * 12);
            heights[CreatureConstants.Dragon_Gold_Young][GenderConstants.Male] = GetBaseFromAverage(7 * 12);
            heights[CreatureConstants.Dragon_Gold_Young][CreatureConstants.Dragon_Gold_Young] = GetMultiplierFromAverage(7 * 12);
            heights[CreatureConstants.Dragon_Gold_Juvenile][GenderConstants.Female] = GetBaseFromAverage(7 * 12);
            heights[CreatureConstants.Dragon_Gold_Juvenile][GenderConstants.Male] = GetBaseFromAverage(7 * 12);
            heights[CreatureConstants.Dragon_Gold_Juvenile][CreatureConstants.Dragon_Gold_Juvenile] = GetMultiplierFromAverage(7 * 12);
            heights[CreatureConstants.Dragon_Gold_YoungAdult][GenderConstants.Female] = GetBaseFromAverage(12 * 12);
            heights[CreatureConstants.Dragon_Gold_YoungAdult][GenderConstants.Male] = GetBaseFromAverage(12 * 12);
            heights[CreatureConstants.Dragon_Gold_YoungAdult][CreatureConstants.Dragon_Gold_YoungAdult] = GetMultiplierFromAverage(12 * 12);
            heights[CreatureConstants.Dragon_Gold_Adult][GenderConstants.Female] = GetBaseFromAverage(12 * 12);
            heights[CreatureConstants.Dragon_Gold_Adult][GenderConstants.Male] = GetBaseFromAverage(12 * 12);
            heights[CreatureConstants.Dragon_Gold_Adult][CreatureConstants.Dragon_Gold_Adult] = GetMultiplierFromAverage(12 * 12);
            heights[CreatureConstants.Dragon_Gold_MatureAdult][GenderConstants.Female] = GetBaseFromAverage(12 * 12);
            heights[CreatureConstants.Dragon_Gold_MatureAdult][GenderConstants.Male] = GetBaseFromAverage(12 * 12);
            heights[CreatureConstants.Dragon_Gold_MatureAdult][CreatureConstants.Dragon_Gold_MatureAdult] = GetMultiplierFromAverage(12 * 12);
            heights[CreatureConstants.Dragon_Gold_Old][GenderConstants.Female] = GetBaseFromAverage(16 * 12);
            heights[CreatureConstants.Dragon_Gold_Old][GenderConstants.Male] = GetBaseFromAverage(16 * 12);
            heights[CreatureConstants.Dragon_Gold_Old][CreatureConstants.Dragon_Gold_Old] = GetMultiplierFromAverage(16 * 12);
            heights[CreatureConstants.Dragon_Gold_VeryOld][GenderConstants.Female] = GetBaseFromAverage(16 * 12);
            heights[CreatureConstants.Dragon_Gold_VeryOld][GenderConstants.Male] = GetBaseFromAverage(16 * 12);
            heights[CreatureConstants.Dragon_Gold_VeryOld][CreatureConstants.Dragon_Gold_VeryOld] = GetMultiplierFromAverage(16 * 12);
            heights[CreatureConstants.Dragon_Gold_Ancient][GenderConstants.Female] = GetBaseFromAverage(16 * 12);
            heights[CreatureConstants.Dragon_Gold_Ancient][GenderConstants.Male] = GetBaseFromAverage(16 * 12);
            heights[CreatureConstants.Dragon_Gold_Ancient][CreatureConstants.Dragon_Gold_Ancient] = GetMultiplierFromAverage(16 * 12);
            heights[CreatureConstants.Dragon_Gold_Wyrm][GenderConstants.Female] = GetBaseFromAverage(22 * 12);
            heights[CreatureConstants.Dragon_Gold_Wyrm][GenderConstants.Male] = GetBaseFromAverage(22 * 12);
            heights[CreatureConstants.Dragon_Gold_Wyrm][CreatureConstants.Dragon_Gold_Wyrm] = GetMultiplierFromAverage(22 * 12);
            heights[CreatureConstants.Dragon_Gold_GreatWyrm][GenderConstants.Female] = GetBaseFromAverage(22 * 12);
            heights[CreatureConstants.Dragon_Gold_GreatWyrm][GenderConstants.Male] = GetBaseFromAverage(22 * 12);
            heights[CreatureConstants.Dragon_Gold_GreatWyrm][CreatureConstants.Dragon_Gold_GreatWyrm] = GetMultiplierFromAverage(22 * 12);
            heights[CreatureConstants.Dragon_Green_Wyrmling][GenderConstants.Female] = GetBaseFromAverage(30);
            heights[CreatureConstants.Dragon_Green_Wyrmling][GenderConstants.Male] = GetBaseFromAverage(30);
            heights[CreatureConstants.Dragon_Green_Wyrmling][CreatureConstants.Dragon_Green_Wyrmling] = GetMultiplierFromAverage(30);
            heights[CreatureConstants.Dragon_Green_VeryYoung][GenderConstants.Female] = GetBaseFromAverage(5 * 12);
            heights[CreatureConstants.Dragon_Green_VeryYoung][GenderConstants.Male] = GetBaseFromAverage(5 * 12);
            heights[CreatureConstants.Dragon_Green_VeryYoung][CreatureConstants.Dragon_Green_VeryYoung] = GetMultiplierFromAverage(5 * 12);
            heights[CreatureConstants.Dragon_Green_Young][GenderConstants.Female] = GetBaseFromAverage(5 * 12);
            heights[CreatureConstants.Dragon_Green_Young][GenderConstants.Male] = GetBaseFromAverage(5 * 12);
            heights[CreatureConstants.Dragon_Green_Young][CreatureConstants.Dragon_Green_Young] = GetMultiplierFromAverage(5 * 12);
            heights[CreatureConstants.Dragon_Green_Juvenile][GenderConstants.Female] = GetBaseFromAverage(9 * 12);
            heights[CreatureConstants.Dragon_Green_Juvenile][GenderConstants.Male] = GetBaseFromAverage(9 * 12);
            heights[CreatureConstants.Dragon_Green_Juvenile][CreatureConstants.Dragon_Green_Juvenile] = GetMultiplierFromAverage(9 * 12);
            heights[CreatureConstants.Dragon_Green_YoungAdult][GenderConstants.Female] = GetBaseFromAverage(9 * 12);
            heights[CreatureConstants.Dragon_Green_YoungAdult][GenderConstants.Male] = GetBaseFromAverage(9 * 12);
            heights[CreatureConstants.Dragon_Green_YoungAdult][CreatureConstants.Dragon_Green_YoungAdult] = GetMultiplierFromAverage(9 * 12);
            heights[CreatureConstants.Dragon_Green_Adult][GenderConstants.Female] = GetBaseFromAverage(15 * 12);
            heights[CreatureConstants.Dragon_Green_Adult][GenderConstants.Male] = GetBaseFromAverage(15 * 12);
            heights[CreatureConstants.Dragon_Green_Adult][CreatureConstants.Dragon_Green_Adult] = GetMultiplierFromAverage(15 * 12);
            heights[CreatureConstants.Dragon_Green_MatureAdult][GenderConstants.Female] = GetBaseFromAverage(15 * 12);
            heights[CreatureConstants.Dragon_Green_MatureAdult][GenderConstants.Male] = GetBaseFromAverage(15 * 12);
            heights[CreatureConstants.Dragon_Green_MatureAdult][CreatureConstants.Dragon_Green_MatureAdult] = GetMultiplierFromAverage(15 * 12);
            heights[CreatureConstants.Dragon_Green_Old][GenderConstants.Female] = GetBaseFromAverage(15 * 12);
            heights[CreatureConstants.Dragon_Green_Old][GenderConstants.Male] = GetBaseFromAverage(15 * 12);
            heights[CreatureConstants.Dragon_Green_Old][CreatureConstants.Dragon_Green_Old] = GetMultiplierFromAverage(15 * 12);
            heights[CreatureConstants.Dragon_Green_VeryOld][GenderConstants.Female] = GetBaseFromAverage(15 * 12);
            heights[CreatureConstants.Dragon_Green_VeryOld][GenderConstants.Male] = GetBaseFromAverage(15 * 12);
            heights[CreatureConstants.Dragon_Green_VeryOld][CreatureConstants.Dragon_Green_VeryOld] = GetMultiplierFromAverage(15 * 12);
            heights[CreatureConstants.Dragon_Green_Ancient][GenderConstants.Female] = GetBaseFromAverage(20 * 12);
            heights[CreatureConstants.Dragon_Green_Ancient][GenderConstants.Male] = GetBaseFromAverage(20 * 12);
            heights[CreatureConstants.Dragon_Green_Ancient][CreatureConstants.Dragon_Green_Ancient] = GetMultiplierFromAverage(20 * 12);
            heights[CreatureConstants.Dragon_Green_Wyrm][GenderConstants.Female] = GetBaseFromAverage(20 * 12);
            heights[CreatureConstants.Dragon_Green_Wyrm][GenderConstants.Male] = GetBaseFromAverage(20 * 12);
            heights[CreatureConstants.Dragon_Green_Wyrm][CreatureConstants.Dragon_Green_Wyrm] = GetMultiplierFromAverage(20 * 12);
            heights[CreatureConstants.Dragon_Green_GreatWyrm][GenderConstants.Female] = GetBaseFromAverage(20 * 12);
            heights[CreatureConstants.Dragon_Green_GreatWyrm][GenderConstants.Male] = GetBaseFromAverage(20 * 12);
            heights[CreatureConstants.Dragon_Green_GreatWyrm][CreatureConstants.Dragon_Green_GreatWyrm] = GetMultiplierFromAverage(20 * 12);
            heights[CreatureConstants.Dragon_Red_Wyrmling][GenderConstants.Female] = GetBaseFromAverage(4 * 12);
            heights[CreatureConstants.Dragon_Red_Wyrmling][GenderConstants.Male] = GetBaseFromAverage(4 * 12);
            heights[CreatureConstants.Dragon_Red_Wyrmling][CreatureConstants.Dragon_Red_Wyrmling] = GetMultiplierFromAverage(4 * 12);
            heights[CreatureConstants.Dragon_Red_VeryYoung][GenderConstants.Female] = GetBaseFromAverage(7 * 12);
            heights[CreatureConstants.Dragon_Red_VeryYoung][GenderConstants.Male] = GetBaseFromAverage(7 * 12);
            heights[CreatureConstants.Dragon_Red_VeryYoung][CreatureConstants.Dragon_Red_VeryYoung] = GetMultiplierFromAverage(7 * 12);
            heights[CreatureConstants.Dragon_Red_Young][GenderConstants.Female] = GetBaseFromAverage(7 * 12);
            heights[CreatureConstants.Dragon_Red_Young][GenderConstants.Male] = GetBaseFromAverage(7 * 12);
            heights[CreatureConstants.Dragon_Red_Young][CreatureConstants.Dragon_Red_Young] = GetMultiplierFromAverage(7 * 12);
            heights[CreatureConstants.Dragon_Red_Juvenile][GenderConstants.Female] = GetBaseFromAverage(7 * 12);
            heights[CreatureConstants.Dragon_Red_Juvenile][GenderConstants.Male] = GetBaseFromAverage(7 * 12);
            heights[CreatureConstants.Dragon_Red_Juvenile][CreatureConstants.Dragon_Red_Juvenile] = GetMultiplierFromAverage(7 * 12);
            heights[CreatureConstants.Dragon_Red_YoungAdult][GenderConstants.Female] = GetBaseFromAverage(12 * 12);
            heights[CreatureConstants.Dragon_Red_YoungAdult][GenderConstants.Male] = GetBaseFromAverage(12 * 12);
            heights[CreatureConstants.Dragon_Red_YoungAdult][CreatureConstants.Dragon_Red_YoungAdult] = GetMultiplierFromAverage(12 * 12);
            heights[CreatureConstants.Dragon_Red_Adult][GenderConstants.Female] = GetBaseFromAverage(12 * 12);
            heights[CreatureConstants.Dragon_Red_Adult][GenderConstants.Male] = GetBaseFromAverage(12 * 12);
            heights[CreatureConstants.Dragon_Red_Adult][CreatureConstants.Dragon_Red_Adult] = GetMultiplierFromAverage(12 * 12);
            heights[CreatureConstants.Dragon_Red_MatureAdult][GenderConstants.Female] = GetBaseFromAverage(12 * 12);
            heights[CreatureConstants.Dragon_Red_MatureAdult][GenderConstants.Male] = GetBaseFromAverage(12 * 12);
            heights[CreatureConstants.Dragon_Red_MatureAdult][CreatureConstants.Dragon_Red_MatureAdult] = GetMultiplierFromAverage(12 * 12);
            heights[CreatureConstants.Dragon_Red_Old][GenderConstants.Female] = GetBaseFromAverage(16 * 12);
            heights[CreatureConstants.Dragon_Red_Old][GenderConstants.Male] = GetBaseFromAverage(16 * 12);
            heights[CreatureConstants.Dragon_Red_Old][CreatureConstants.Dragon_Red_Old] = GetMultiplierFromAverage(16 * 12);
            heights[CreatureConstants.Dragon_Red_VeryOld][GenderConstants.Female] = GetBaseFromAverage(16 * 12);
            heights[CreatureConstants.Dragon_Red_VeryOld][GenderConstants.Male] = GetBaseFromAverage(16 * 12);
            heights[CreatureConstants.Dragon_Red_VeryOld][CreatureConstants.Dragon_Red_VeryOld] = GetMultiplierFromAverage(16 * 12);
            heights[CreatureConstants.Dragon_Red_Ancient][GenderConstants.Female] = GetBaseFromAverage(16 * 12);
            heights[CreatureConstants.Dragon_Red_Ancient][GenderConstants.Male] = GetBaseFromAverage(16 * 12);
            heights[CreatureConstants.Dragon_Red_Ancient][CreatureConstants.Dragon_Red_Ancient] = GetMultiplierFromAverage(16 * 12);
            heights[CreatureConstants.Dragon_Red_Wyrm][GenderConstants.Female] = GetBaseFromAverage(16 * 12);
            heights[CreatureConstants.Dragon_Red_Wyrm][GenderConstants.Male] = GetBaseFromAverage(16 * 12);
            heights[CreatureConstants.Dragon_Red_Wyrm][CreatureConstants.Dragon_Red_Wyrm] = GetMultiplierFromAverage(16 * 12);
            heights[CreatureConstants.Dragon_Red_GreatWyrm][GenderConstants.Female] = GetBaseFromAverage(22 * 12);
            heights[CreatureConstants.Dragon_Red_GreatWyrm][GenderConstants.Male] = GetBaseFromAverage(22 * 12);
            heights[CreatureConstants.Dragon_Red_GreatWyrm][CreatureConstants.Dragon_Red_GreatWyrm] = GetMultiplierFromAverage(22 * 12);
            heights[CreatureConstants.Dragon_Silver_Wyrmling][GenderConstants.Female] = GetBaseFromAverage(2 * 12);
            heights[CreatureConstants.Dragon_Silver_Wyrmling][GenderConstants.Male] = GetBaseFromAverage(2 * 12);
            heights[CreatureConstants.Dragon_Silver_Wyrmling][CreatureConstants.Dragon_Silver_Wyrmling] = GetMultiplierFromAverage(2 * 12);
            heights[CreatureConstants.Dragon_Silver_VeryYoung][GenderConstants.Female] = GetBaseFromAverage(4 * 12);
            heights[CreatureConstants.Dragon_Silver_VeryYoung][GenderConstants.Male] = GetBaseFromAverage(4 * 12);
            heights[CreatureConstants.Dragon_Silver_VeryYoung][CreatureConstants.Dragon_Silver_VeryYoung] = GetMultiplierFromAverage(4 * 12);
            heights[CreatureConstants.Dragon_Silver_Young][GenderConstants.Female] = GetBaseFromAverage(7 * 12);
            heights[CreatureConstants.Dragon_Silver_Young][GenderConstants.Male] = GetBaseFromAverage(7 * 12);
            heights[CreatureConstants.Dragon_Silver_Young][CreatureConstants.Dragon_Silver_Young] = GetMultiplierFromAverage(7 * 12);
            heights[CreatureConstants.Dragon_Silver_Juvenile][GenderConstants.Female] = GetBaseFromAverage(7 * 12);
            heights[CreatureConstants.Dragon_Silver_Juvenile][GenderConstants.Male] = GetBaseFromAverage(7 * 12);
            heights[CreatureConstants.Dragon_Silver_Juvenile][CreatureConstants.Dragon_Silver_Juvenile] = GetMultiplierFromAverage(7 * 12);
            heights[CreatureConstants.Dragon_Silver_YoungAdult][GenderConstants.Female] = GetBaseFromAverage(7 * 12);
            heights[CreatureConstants.Dragon_Silver_YoungAdult][GenderConstants.Male] = GetBaseFromAverage(7 * 12);
            heights[CreatureConstants.Dragon_Silver_YoungAdult][CreatureConstants.Dragon_Silver_YoungAdult] = GetMultiplierFromAverage(7 * 12);
            heights[CreatureConstants.Dragon_Silver_Adult][GenderConstants.Female] = GetBaseFromAverage(12 * 12);
            heights[CreatureConstants.Dragon_Silver_Adult][GenderConstants.Male] = GetBaseFromAverage(12 * 12);
            heights[CreatureConstants.Dragon_Silver_Adult][CreatureConstants.Dragon_Silver_Adult] = GetMultiplierFromAverage(12 * 12);
            heights[CreatureConstants.Dragon_Silver_MatureAdult][GenderConstants.Female] = GetBaseFromAverage(12 * 12);
            heights[CreatureConstants.Dragon_Silver_MatureAdult][GenderConstants.Male] = GetBaseFromAverage(12 * 12);
            heights[CreatureConstants.Dragon_Silver_MatureAdult][CreatureConstants.Dragon_Silver_MatureAdult] = GetMultiplierFromAverage(12 * 12);
            heights[CreatureConstants.Dragon_Silver_Old][GenderConstants.Female] = GetBaseFromAverage(12 * 12);
            heights[CreatureConstants.Dragon_Silver_Old][GenderConstants.Male] = GetBaseFromAverage(12 * 12);
            heights[CreatureConstants.Dragon_Silver_Old][CreatureConstants.Dragon_Silver_Old] = GetMultiplierFromAverage(12 * 12);
            heights[CreatureConstants.Dragon_Silver_VeryOld][GenderConstants.Female] = GetBaseFromAverage(12 * 12);
            heights[CreatureConstants.Dragon_Silver_VeryOld][GenderConstants.Male] = GetBaseFromAverage(12 * 12);
            heights[CreatureConstants.Dragon_Silver_VeryOld][CreatureConstants.Dragon_Silver_VeryOld] = GetMultiplierFromAverage(12 * 12);
            heights[CreatureConstants.Dragon_Silver_Ancient][GenderConstants.Female] = GetBaseFromAverage(16 * 12);
            heights[CreatureConstants.Dragon_Silver_Ancient][GenderConstants.Male] = GetBaseFromAverage(16 * 12);
            heights[CreatureConstants.Dragon_Silver_Ancient][CreatureConstants.Dragon_Silver_Ancient] = GetMultiplierFromAverage(16 * 12);
            heights[CreatureConstants.Dragon_Silver_Wyrm][GenderConstants.Female] = GetBaseFromAverage(16 * 12);
            heights[CreatureConstants.Dragon_Silver_Wyrm][GenderConstants.Male] = GetBaseFromAverage(16 * 12);
            heights[CreatureConstants.Dragon_Silver_Wyrm][CreatureConstants.Dragon_Silver_Wyrm] = GetMultiplierFromAverage(16 * 12);
            heights[CreatureConstants.Dragon_Silver_GreatWyrm][GenderConstants.Female] = GetBaseFromAverage(22 * 12);
            heights[CreatureConstants.Dragon_Silver_GreatWyrm][GenderConstants.Male] = GetBaseFromAverage(22 * 12);
            heights[CreatureConstants.Dragon_Silver_GreatWyrm][CreatureConstants.Dragon_Silver_GreatWyrm] = GetMultiplierFromAverage(22 * 12);
            heights[CreatureConstants.Dragon_White_Wyrmling][GenderConstants.Female] = GetBaseFromAverage(12);
            heights[CreatureConstants.Dragon_White_Wyrmling][GenderConstants.Male] = GetBaseFromAverage(12);
            heights[CreatureConstants.Dragon_White_Wyrmling][CreatureConstants.Dragon_White_Wyrmling] = GetMultiplierFromAverage(12);
            heights[CreatureConstants.Dragon_White_VeryYoung][GenderConstants.Female] = GetBaseFromAverage(24);
            heights[CreatureConstants.Dragon_White_VeryYoung][GenderConstants.Male] = GetBaseFromAverage(24);
            heights[CreatureConstants.Dragon_White_VeryYoung][CreatureConstants.Dragon_White_VeryYoung] = GetMultiplierFromAverage(24);
            heights[CreatureConstants.Dragon_White_Young][GenderConstants.Female] = GetBaseFromAverage(4 * 12);
            heights[CreatureConstants.Dragon_White_Young][GenderConstants.Male] = GetBaseFromAverage(4 * 12);
            heights[CreatureConstants.Dragon_White_Young][CreatureConstants.Dragon_White_Young] = GetMultiplierFromAverage(4 * 12);
            heights[CreatureConstants.Dragon_White_Juvenile][GenderConstants.Female] = GetBaseFromAverage(4 * 12);
            heights[CreatureConstants.Dragon_White_Juvenile][GenderConstants.Male] = GetBaseFromAverage(4 * 12);
            heights[CreatureConstants.Dragon_White_Juvenile][CreatureConstants.Dragon_White_Juvenile] = GetMultiplierFromAverage(4 * 12);
            heights[CreatureConstants.Dragon_White_YoungAdult][GenderConstants.Female] = GetBaseFromAverage(7 * 12);
            heights[CreatureConstants.Dragon_White_YoungAdult][GenderConstants.Male] = GetBaseFromAverage(7 * 12);
            heights[CreatureConstants.Dragon_White_YoungAdult][CreatureConstants.Dragon_White_YoungAdult] = GetMultiplierFromAverage(7 * 12);
            heights[CreatureConstants.Dragon_White_Adult][GenderConstants.Female] = GetBaseFromAverage(7 * 12);
            heights[CreatureConstants.Dragon_White_Adult][GenderConstants.Male] = GetBaseFromAverage(7 * 12);
            heights[CreatureConstants.Dragon_White_Adult][CreatureConstants.Dragon_White_Adult] = GetMultiplierFromAverage(7 * 12);
            heights[CreatureConstants.Dragon_White_MatureAdult][GenderConstants.Female] = GetBaseFromAverage(12 * 12);
            heights[CreatureConstants.Dragon_White_MatureAdult][GenderConstants.Male] = GetBaseFromAverage(12 * 12);
            heights[CreatureConstants.Dragon_White_MatureAdult][CreatureConstants.Dragon_White_MatureAdult] = GetMultiplierFromAverage(12 * 12);
            heights[CreatureConstants.Dragon_White_Old][GenderConstants.Female] = GetBaseFromAverage(12 * 12);
            heights[CreatureConstants.Dragon_White_Old][GenderConstants.Male] = GetBaseFromAverage(12 * 12);
            heights[CreatureConstants.Dragon_White_Old][CreatureConstants.Dragon_White_Old] = GetMultiplierFromAverage(12 * 12);
            heights[CreatureConstants.Dragon_White_VeryOld][GenderConstants.Female] = GetBaseFromAverage(12 * 12);
            heights[CreatureConstants.Dragon_White_VeryOld][GenderConstants.Male] = GetBaseFromAverage(12 * 12);
            heights[CreatureConstants.Dragon_White_VeryOld][CreatureConstants.Dragon_White_VeryOld] = GetMultiplierFromAverage(12 * 12);
            heights[CreatureConstants.Dragon_White_Ancient][GenderConstants.Female] = GetBaseFromAverage(12 * 12);
            heights[CreatureConstants.Dragon_White_Ancient][GenderConstants.Male] = GetBaseFromAverage(12 * 12);
            heights[CreatureConstants.Dragon_White_Ancient][CreatureConstants.Dragon_White_Ancient] = GetMultiplierFromAverage(12 * 12);
            heights[CreatureConstants.Dragon_White_Wyrm][GenderConstants.Female] = GetBaseFromAverage(16 * 12);
            heights[CreatureConstants.Dragon_White_Wyrm][GenderConstants.Male] = GetBaseFromAverage(16 * 12);
            heights[CreatureConstants.Dragon_White_Wyrm][CreatureConstants.Dragon_White_Wyrm] = GetMultiplierFromAverage(16 * 12);
            heights[CreatureConstants.Dragon_White_GreatWyrm][GenderConstants.Female] = GetBaseFromAverage(16 * 12);
            heights[CreatureConstants.Dragon_White_GreatWyrm][GenderConstants.Male] = GetBaseFromAverage(16 * 12);
            heights[CreatureConstants.Dragon_White_GreatWyrm][CreatureConstants.Dragon_White_GreatWyrm] = GetMultiplierFromAverage(16 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Dragon_turtle
            heights[CreatureConstants.DragonTurtle][GenderConstants.Female] = "0";
            heights[CreatureConstants.DragonTurtle][GenderConstants.Male] = "0";
            heights[CreatureConstants.DragonTurtle][CreatureConstants.DragonTurtle] = "0";
            //Source: https://forgottenrealms.fandom.com/wiki/Dragonne
            heights[CreatureConstants.Dragonne][GenderConstants.Female] = GetBaseFromAverage(5 * 12);
            heights[CreatureConstants.Dragonne][GenderConstants.Male] = GetBaseFromAverage(5 * 12);
            heights[CreatureConstants.Dragonne][CreatureConstants.Dragonne] = GetMultiplierFromAverage(5 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Dretch
            heights[CreatureConstants.Dretch][GenderConstants.Agender] = GetBaseFromAverage(4 * 12);
            heights[CreatureConstants.Dretch][GenderConstants.Female] = GetBaseFromAverage(4 * 12);
            heights[CreatureConstants.Dretch][GenderConstants.Male] = GetBaseFromAverage(4 * 12);
            heights[CreatureConstants.Dretch][CreatureConstants.Dretch] = GetMultiplierFromAverage(4 * 12);
            //Source: https://www.worldanvil.com/w/faerun-tatortotzke/a/drider-species
            //https://www.5esrd.com/database/race/drider/ (for height)
            heights[CreatureConstants.Drider][GenderConstants.Agender] = GetBaseFromAverage(7 * 12);
            heights[CreatureConstants.Drider][CreatureConstants.Drider] = GetMultiplierFromAverage(7 * 12);
            //Source: https://www.worldanvil.com/w/faerun-tatortotzke/a/dryad-species
            heights[CreatureConstants.Dryad][GenderConstants.Female] = GetBaseFromAverage(5 * 12);
            heights[CreatureConstants.Dryad][CreatureConstants.Dryad] = GetMultiplierFromAverage(5 * 12);
            //Source: https://www.d20srd.org/srd/description.htm#vitalStatistics
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
            //Source: https://www.dimensions.com/element/bald-eagle-haliaeetus-leucocephalus
            heights[CreatureConstants.Eagle][GenderConstants.Female] = GetBaseFromRange(17, 24);
            heights[CreatureConstants.Eagle][GenderConstants.Male] = GetBaseFromRange(17, 24);
            heights[CreatureConstants.Eagle][CreatureConstants.Eagle] = GetMultiplierFromRange(17, 24);
            //Source: https://www.d20srd.org/srd/monsters/eagleGiant.htm
            heights[CreatureConstants.Eagle_Giant][GenderConstants.Female] = GetBaseFromAverage(10 * 12);
            heights[CreatureConstants.Eagle_Giant][GenderConstants.Male] = GetBaseFromAverage(10 * 12);
            heights[CreatureConstants.Eagle_Giant][CreatureConstants.Eagle_Giant] = GetMultiplierFromAverage(10 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Efreeti
            heights[CreatureConstants.Efreeti][GenderConstants.Agender] = GetBaseFromAverage(12 * 12);
            heights[CreatureConstants.Efreeti][GenderConstants.Female] = GetBaseFromAverage(12 * 12);
            heights[CreatureConstants.Efreeti][GenderConstants.Male] = GetBaseFromAverage(12 * 12);
            heights[CreatureConstants.Efreeti][CreatureConstants.Efreeti] = GetMultiplierFromAverage(12 * 12);
            //Source: https://jurassicworld-evolution.fandom.com/wiki/Elasmosaurus
            heights[CreatureConstants.Elasmosaurus][GenderConstants.Female] = GetBaseFromAverage(40);
            heights[CreatureConstants.Elasmosaurus][GenderConstants.Male] = GetBaseFromAverage(40);
            heights[CreatureConstants.Elasmosaurus][CreatureConstants.Elasmosaurus] = GetMultiplierFromAverage(40);
            //Source: https://www.d20srd.org/srd/monsters/elemental.htm
            heights[CreatureConstants.Elemental_Air_Small][GenderConstants.Agender] = GetBaseFromAverage(4 * 12);
            heights[CreatureConstants.Elemental_Air_Small][CreatureConstants.Elemental_Air_Small] = GetMultiplierFromAverage(4 * 12);
            heights[CreatureConstants.Elemental_Air_Medium][GenderConstants.Agender] = GetBaseFromAverage(8 * 12);
            heights[CreatureConstants.Elemental_Air_Medium][CreatureConstants.Elemental_Air_Medium] = GetMultiplierFromAverage(8 * 12);
            heights[CreatureConstants.Elemental_Air_Large][GenderConstants.Agender] = GetBaseFromAverage(16 * 12);
            heights[CreatureConstants.Elemental_Air_Large][CreatureConstants.Elemental_Air_Large] = GetMultiplierFromAverage(16 * 12);
            heights[CreatureConstants.Elemental_Air_Huge][GenderConstants.Agender] = GetBaseFromAverage(32 * 12);
            heights[CreatureConstants.Elemental_Air_Huge][CreatureConstants.Elemental_Air_Huge] = GetMultiplierFromAverage(32 * 12);
            heights[CreatureConstants.Elemental_Air_Greater][GenderConstants.Agender] = GetBaseFromAverage(36 * 12);
            heights[CreatureConstants.Elemental_Air_Greater][CreatureConstants.Elemental_Air_Greater] = GetMultiplierFromAverage(36 * 12);
            heights[CreatureConstants.Elemental_Air_Elder][GenderConstants.Agender] = GetBaseFromAverage(40 * 12);
            heights[CreatureConstants.Elemental_Air_Elder][CreatureConstants.Elemental_Air_Elder] = GetMultiplierFromAverage(40 * 12);
            heights[CreatureConstants.Elemental_Earth_Small][GenderConstants.Agender] = GetBaseFromAverage(4 * 12);
            heights[CreatureConstants.Elemental_Earth_Small][CreatureConstants.Elemental_Earth_Small] = GetMultiplierFromAverage(4 * 12);
            heights[CreatureConstants.Elemental_Earth_Medium][GenderConstants.Agender] = GetBaseFromAverage(8 * 12);
            heights[CreatureConstants.Elemental_Earth_Medium][CreatureConstants.Elemental_Earth_Medium] = GetMultiplierFromAverage(8 * 12);
            heights[CreatureConstants.Elemental_Earth_Large][GenderConstants.Agender] = GetBaseFromAverage(16 * 12);
            heights[CreatureConstants.Elemental_Earth_Large][CreatureConstants.Elemental_Earth_Large] = GetMultiplierFromAverage(16 * 12);
            heights[CreatureConstants.Elemental_Earth_Huge][GenderConstants.Agender] = GetBaseFromAverage(32 * 12);
            heights[CreatureConstants.Elemental_Earth_Huge][CreatureConstants.Elemental_Earth_Huge] = GetMultiplierFromAverage(32 * 12);
            heights[CreatureConstants.Elemental_Earth_Greater][GenderConstants.Agender] = GetBaseFromAverage(36 * 12);
            heights[CreatureConstants.Elemental_Earth_Greater][CreatureConstants.Elemental_Earth_Greater] = GetMultiplierFromAverage(36 * 12);
            heights[CreatureConstants.Elemental_Earth_Elder][GenderConstants.Agender] = GetBaseFromAverage(40 * 12);
            heights[CreatureConstants.Elemental_Earth_Elder][CreatureConstants.Elemental_Earth_Elder] = GetMultiplierFromAverage(40 * 12);
            heights[CreatureConstants.Elemental_Fire_Small][GenderConstants.Agender] = GetBaseFromAverage(4 * 12);
            heights[CreatureConstants.Elemental_Fire_Small][CreatureConstants.Elemental_Fire_Small] = GetMultiplierFromAverage(4 * 12);
            heights[CreatureConstants.Elemental_Fire_Medium][GenderConstants.Agender] = GetBaseFromAverage(8 * 12);
            heights[CreatureConstants.Elemental_Fire_Medium][CreatureConstants.Elemental_Fire_Medium] = GetMultiplierFromAverage(8 * 12);
            heights[CreatureConstants.Elemental_Fire_Large][GenderConstants.Agender] = GetBaseFromAverage(16 * 12);
            heights[CreatureConstants.Elemental_Fire_Large][CreatureConstants.Elemental_Fire_Large] = GetMultiplierFromAverage(16 * 12);
            heights[CreatureConstants.Elemental_Fire_Huge][GenderConstants.Agender] = GetBaseFromAverage(32 * 12);
            heights[CreatureConstants.Elemental_Fire_Huge][CreatureConstants.Elemental_Fire_Huge] = GetMultiplierFromAverage(32 * 12);
            heights[CreatureConstants.Elemental_Fire_Greater][GenderConstants.Agender] = GetBaseFromAverage(36 * 12);
            heights[CreatureConstants.Elemental_Fire_Greater][CreatureConstants.Elemental_Fire_Greater] = GetMultiplierFromAverage(36 * 12);
            heights[CreatureConstants.Elemental_Fire_Elder][GenderConstants.Agender] = GetBaseFromAverage(40 * 12);
            heights[CreatureConstants.Elemental_Fire_Elder][CreatureConstants.Elemental_Fire_Elder] = GetMultiplierFromAverage(40 * 12);
            heights[CreatureConstants.Elemental_Water_Small][GenderConstants.Agender] = GetBaseFromAverage(4 * 12);
            heights[CreatureConstants.Elemental_Water_Small][CreatureConstants.Elemental_Water_Small] = GetMultiplierFromAverage(4 * 12);
            heights[CreatureConstants.Elemental_Water_Medium][GenderConstants.Agender] = GetBaseFromAverage(8 * 12);
            heights[CreatureConstants.Elemental_Water_Medium][CreatureConstants.Elemental_Water_Medium] = GetMultiplierFromAverage(8 * 12);
            heights[CreatureConstants.Elemental_Water_Large][GenderConstants.Agender] = GetBaseFromAverage(16 * 12);
            heights[CreatureConstants.Elemental_Water_Large][CreatureConstants.Elemental_Water_Large] = GetMultiplierFromAverage(16 * 12);
            heights[CreatureConstants.Elemental_Water_Huge][GenderConstants.Agender] = GetBaseFromAverage(32 * 12);
            heights[CreatureConstants.Elemental_Water_Huge][CreatureConstants.Elemental_Water_Huge] = GetMultiplierFromAverage(32 * 12);
            heights[CreatureConstants.Elemental_Water_Greater][GenderConstants.Agender] = GetBaseFromAverage(36 * 12);
            heights[CreatureConstants.Elemental_Water_Greater][CreatureConstants.Elemental_Water_Greater] = GetMultiplierFromAverage(36 * 12);
            heights[CreatureConstants.Elemental_Water_Elder][GenderConstants.Agender] = GetBaseFromAverage(40 * 12);
            heights[CreatureConstants.Elemental_Water_Elder][CreatureConstants.Elemental_Water_Elder] = GetMultiplierFromAverage(40 * 12);
            //Source: https://www.dimensions.com/element/african-bush-elephant-loxodonta-africana
            heights[CreatureConstants.Elephant][GenderConstants.Female] = GetBaseFromRange(8 * 12 + 6, 13 * 12);
            heights[CreatureConstants.Elephant][GenderConstants.Male] = GetBaseFromRange(8 * 12 + 6, 13 * 12);
            heights[CreatureConstants.Elephant][CreatureConstants.Elephant] = GetMultiplierFromRange(8 * 12 + 6, 13 * 12);
            //Source: https://www.d20srd.org/srd/description.htm#vitalStatistics
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
            //Source: https://www.d20srd.org/srd/monsters/devil.htm#erinyes
            heights[CreatureConstants.Erinyes][GenderConstants.Female] = GetBaseFromAverage(6 * 12);
            heights[CreatureConstants.Erinyes][GenderConstants.Male] = GetBaseFromAverage(6 * 12);
            heights[CreatureConstants.Erinyes][CreatureConstants.Erinyes] = GetMultiplierFromAverage(6 * 12);
            //Source: https://aminoapps.com/c/officialdd/page/item/ethereal-filcher/5B63_a5vi5Ia7NM1djj0V1QYVoxpXweGW1z
            heights[CreatureConstants.EtherealFilcher][GenderConstants.Agender] = GetBaseFromAverage(4 * 12 + 6);
            heights[CreatureConstants.EtherealFilcher][CreatureConstants.EtherealFilcher] = GetMultiplierFromAverage(4 * 12 + 6);
            //Source: https://www.d20srd.org/srd/monsters/etherealMarauder.htm
            heights[CreatureConstants.EtherealMarauder][GenderConstants.Female] = GetBaseFromAverage(4 * 12);
            heights[CreatureConstants.EtherealMarauder][GenderConstants.Male] = GetBaseFromAverage(4 * 12);
            heights[CreatureConstants.EtherealMarauder][CreatureConstants.EtherealMarauder] = GetMultiplierFromAverage(4 * 12);
            //Source: https://syrikdarkenedskies.obsidianportal.com/wikis/ettercap-race
            heights[CreatureConstants.Ettercap][GenderConstants.Female] = "5*12+7";
            heights[CreatureConstants.Ettercap][GenderConstants.Male] = "5*12+2";
            heights[CreatureConstants.Ettercap][CreatureConstants.Ettercap] = "2d10";
            //Source: https://forgottenrealms.fandom.com/wiki/Ettin
            heights[CreatureConstants.Ettin][GenderConstants.Female] = "12*12+2";
            heights[CreatureConstants.Ettin][GenderConstants.Male] = "12*12+10";
            heights[CreatureConstants.Ettin][CreatureConstants.Ettin] = "2d6";
            heights[CreatureConstants.FireBeetle_Giant][GenderConstants.Female] = "0";
            heights[CreatureConstants.FireBeetle_Giant][GenderConstants.Male] = "0";
            heights[CreatureConstants.FireBeetle_Giant][CreatureConstants.Ettin] = "0";
            //Source: https://www.d20srd.org/srd/monsters/formian.htm
            heights[CreatureConstants.FormianWorker][GenderConstants.Male] = GetBaseFromAverage(2 * 12 + 6);
            heights[CreatureConstants.FormianWorker][CreatureConstants.FormianWorker] = GetMultiplierFromAverage(2 * 12 + 6);
            heights[CreatureConstants.FormianWarrior][GenderConstants.Male] = GetBaseFromAverage(4 * 12 + 6);
            heights[CreatureConstants.FormianWarrior][CreatureConstants.FormianWarrior] = GetMultiplierFromAverage(4 * 12 + 6);
            heights[CreatureConstants.FormianTaskmaster][GenderConstants.Male] = GetBaseFromAverage(4 * 12 + 6);
            heights[CreatureConstants.FormianTaskmaster][CreatureConstants.FormianTaskmaster] = GetMultiplierFromAverage(4 * 12 + 6);
            heights[CreatureConstants.FormianMyrmarch][GenderConstants.Male] = GetBaseFromAverage(5 * 12 + 6);
            heights[CreatureConstants.FormianMyrmarch][CreatureConstants.FormianMyrmarch] = GetMultiplierFromAverage(5 * 12 + 6);
            heights[CreatureConstants.FormianQueen][GenderConstants.Female] = GetBaseFromAverage(4 * 12);
            heights[CreatureConstants.FormianQueen][CreatureConstants.FormianQueen] = GetMultiplierFromAverage(4 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Frost_worm
            heights[CreatureConstants.FrostWorm][GenderConstants.Female] = GetBaseFromAverage(5 * 12);
            heights[CreatureConstants.FrostWorm][GenderConstants.Male] = GetBaseFromAverage(5 * 12);
            heights[CreatureConstants.FrostWorm][CreatureConstants.FrostWorm] = GetMultiplierFromAverage(5 * 12);
            //Source: https://dungeonsdragons.fandom.com/wiki/Gargoyle
            heights[CreatureConstants.Gargoyle][GenderConstants.Agender] = "5*12";
            heights[CreatureConstants.Gargoyle][CreatureConstants.Gargoyle] = "2d10";
            heights[CreatureConstants.Gargoyle_Kapoacinth][GenderConstants.Agender] = "5*12";
            heights[CreatureConstants.Gargoyle_Kapoacinth][CreatureConstants.Gargoyle_Kapoacinth] = "2d10";
            //Source: https://www.worldanvil.com/w/faerun-tatortotzke/a/gelatinous-cube-species
            heights[CreatureConstants.GelatinousCube][GenderConstants.Agender] = GetBaseFromAverage(10 * 12);
            heights[CreatureConstants.GelatinousCube][CreatureConstants.GelatinousCube] = GetMultiplierFromAverage(10 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Ghaele
            heights[CreatureConstants.Ghaele][GenderConstants.Female] = GetBaseFromRange(4 * 12 + 7, 6 * 12 + 7);
            heights[CreatureConstants.Ghaele][GenderConstants.Male] = GetBaseFromRange(5 * 12 + 2, 7 * 12);
            heights[CreatureConstants.Ghaele][CreatureConstants.Ghaele] = GetMultiplierFromRange(5 * 12 + 2, 7 * 12);
            //Source: https://www.dandwiki.com/wiki/Ghoul_(5e_Race)
            heights[CreatureConstants.Ghoul][GenderConstants.Female] = "4*12";
            heights[CreatureConstants.Ghoul][GenderConstants.Male] = "4*12";
            heights[CreatureConstants.Ghoul][CreatureConstants.Ghoul] = "2d12";
            heights[CreatureConstants.Ghoul_Ghast][GenderConstants.Female] = "4*12";
            heights[CreatureConstants.Ghoul_Ghast][GenderConstants.Male] = "4*12";
            heights[CreatureConstants.Ghoul_Ghast][CreatureConstants.Ghoul_Ghast] = "2d12";
            heights[CreatureConstants.Ghoul_Lacedon][GenderConstants.Female] = "4*12";
            heights[CreatureConstants.Ghoul_Lacedon][GenderConstants.Male] = "4*12";
            heights[CreatureConstants.Ghoul_Lacedon][CreatureConstants.Ghoul_Lacedon] = "2d12";
            //Source: https://forgottenrealms.fandom.com/wiki/Cloud_giant
            heights[CreatureConstants.Giant_Cloud][GenderConstants.Female] = GetBaseFromRange(22 * 12 + 8, 25 * 12);
            heights[CreatureConstants.Giant_Cloud][GenderConstants.Male] = GetBaseFromRange(24 * 12 + 4, 26 * 12 + 8);
            heights[CreatureConstants.Giant_Cloud][CreatureConstants.Giant_Cloud] = GetMultiplierFromRange(22 * 12 + 8, 25 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Fire_giant
            heights[CreatureConstants.Giant_Fire][GenderConstants.Female] = GetBaseFromRange(17 * 12 + 5, 19 * 12);
            heights[CreatureConstants.Giant_Fire][GenderConstants.Male] = GetBaseFromRange(18 * 12 + 2, 19 * 12 + 8);
            heights[CreatureConstants.Giant_Fire][CreatureConstants.Giant_Fire] = GetMultiplierFromRange(18 * 12 + 2, 19 * 12 + 8);
            //Source: https://forgottenrealms.fandom.com/wiki/Frost_giant
            heights[CreatureConstants.Giant_Frost][GenderConstants.Female] = GetBaseFromRange(20 * 12 + 1, 22 * 12 + 4);
            heights[CreatureConstants.Giant_Frost][GenderConstants.Male] = GetBaseFromRange(21 * 12 + 3, 23 * 12 + 6);
            heights[CreatureConstants.Giant_Frost][CreatureConstants.Giant_Frost] = GetMultiplierFromRange(21 * 12 + 3, 23 * 12 + 6);
            //Source: https://forgottenrealms.fandom.com/wiki/Hill_giant
            heights[CreatureConstants.Giant_Hill][GenderConstants.Female] = GetBaseFromRange(15 * 12 + 5, 16 * 12 + 4);
            heights[CreatureConstants.Giant_Hill][GenderConstants.Male] = GetBaseFromRange(16 * 12 + 1, 17 * 12);
            heights[CreatureConstants.Giant_Hill][CreatureConstants.Giant_Hill] = GetMultiplierFromRange(15 * 12 + 5, 16 * 12 + 4);
            //Source: https://www.worldanvil.com/w/faerun-tatortotzke/a/gibbering-mouther-species
            heights[CreatureConstants.GibberingMouther][GenderConstants.Agender] = GetBaseFromRange(3 * 12, 7 * 12);
            heights[CreatureConstants.GibberingMouther][CreatureConstants.GibberingMouther] = GetMultiplierFromRange(3 * 12, 7 * 12);
            //Source: https://www.d20srd.org/srd/monsters/girallon.htm
            heights[CreatureConstants.Girallon][GenderConstants.Female] = GetBaseFromAverage(8 * 12);
            heights[CreatureConstants.Girallon][GenderConstants.Male] = GetBaseFromAverage(8 * 12);
            heights[CreatureConstants.Girallon][CreatureConstants.Girallon] = GetMultiplierFromAverage(8 * 12);
            //Source: http://people.wku.edu/charles.plemons/ad&d/races/height.html
            heights[CreatureConstants.Githyanki][GenderConstants.Female] = "60";
            heights[CreatureConstants.Githyanki][GenderConstants.Male] = "62";
            heights[CreatureConstants.Githyanki][CreatureConstants.Githyanki] = "2d10";
            //Source: http://people.wku.edu/charles.plemons/ad&d/races/height.html
            heights[CreatureConstants.Githzerai][GenderConstants.Female] = "60";
            heights[CreatureConstants.Githzerai][GenderConstants.Male] = "62";
            heights[CreatureConstants.Githzerai][CreatureConstants.Githzerai] = "2d10";
            //Source: https://forgottenrealms.fandom.com/wiki/Glabrezu
            heights[CreatureConstants.Glabrezu][GenderConstants.Agender] = GetBaseFromRange(9 * 12, 15 * 12);
            heights[CreatureConstants.Glabrezu][CreatureConstants.Glabrezu] = GetMultiplierFromRange(9 * 12, 15 * 12);
            //Source: http://people.wku.edu/charles.plemons/ad&d/races/height.html
            heights[CreatureConstants.Gnoll][GenderConstants.Female] = "80";
            heights[CreatureConstants.Gnoll][GenderConstants.Male] = "84";
            heights[CreatureConstants.Gnoll][CreatureConstants.Gnoll] = "1d12";
            //Source: https://www.d20srd.org/srd/description.htm#vitalStatistics
            heights[CreatureConstants.Gnome_Forest][GenderConstants.Female] = "2*12+10";
            heights[CreatureConstants.Gnome_Forest][GenderConstants.Male] = "3*12+0";
            heights[CreatureConstants.Gnome_Forest][CreatureConstants.Gnome_Forest] = "2d4";
            heights[CreatureConstants.Gnome_Rock][GenderConstants.Female] = "2*12+10";
            heights[CreatureConstants.Gnome_Rock][GenderConstants.Male] = "3*12+0";
            heights[CreatureConstants.Gnome_Rock][CreatureConstants.Gnome_Rock] = "2d4";
            heights[CreatureConstants.Gnome_Svirfneblin][GenderConstants.Female] = "2*12+10";
            heights[CreatureConstants.Gnome_Svirfneblin][GenderConstants.Male] = "3*12+0";
            heights[CreatureConstants.Gnome_Svirfneblin][CreatureConstants.Gnome_Svirfneblin] = "2d4";
            //Source: http://people.wku.edu/charles.plemons/ad&d/races/height.html
            heights[CreatureConstants.Goblin][GenderConstants.Female] = "41";
            heights[CreatureConstants.Goblin][GenderConstants.Male] = "43";
            heights[CreatureConstants.Goblin][CreatureConstants.Goblin] = "1d10";
            //Source: https://pathfinderwiki.com/wiki/Clay_golem
            heights[CreatureConstants.Golem_Clay][GenderConstants.Agender] = GetBaseFromAverage(8 * 12);
            heights[CreatureConstants.Golem_Clay][CreatureConstants.Golem_Clay] = GetMultiplierFromAverage(8 * 12);
            //Source: https://www.d20srd.org/srd/monsters/golem.htm
            heights[CreatureConstants.Golem_Flesh][GenderConstants.Agender] = GetBaseFromAverage(8 * 12);
            heights[CreatureConstants.Golem_Flesh][CreatureConstants.Golem_Flesh] = GetMultiplierFromAverage(8 * 12);
            //Source: https://www.d20srd.org/srd/monsters/hag.htm#greenHag
            heights[CreatureConstants.GreenHag][GenderConstants.Female] = "4*12+5";
            heights[CreatureConstants.GreenHag][CreatureConstants.GreenHag] = "2d10";
            //Source: https://www.d20srd.org/srd/monsters/sprite.htm#grig
            heights[CreatureConstants.Grig][GenderConstants.Female] = GetBaseFromAverage(18);
            heights[CreatureConstants.Grig][GenderConstants.Male] = GetBaseFromAverage(18);
            heights[CreatureConstants.Grig][CreatureConstants.Grig] = GetMultiplierFromAverage(18);
            heights[CreatureConstants.Grig_WithFiddle][GenderConstants.Female] = GetBaseFromAverage(18);
            heights[CreatureConstants.Grig_WithFiddle][GenderConstants.Male] = GetBaseFromAverage(18);
            heights[CreatureConstants.Grig_WithFiddle][CreatureConstants.Grig_WithFiddle] = GetMultiplierFromAverage(18);
            //Source: https://www.mojobob.com/roleplay/monstrousmanual/s/sphinx.html
            heights[CreatureConstants.Gynosphinx][GenderConstants.Female] = GetBaseFromAverage(7 * 12);
            heights[CreatureConstants.Gynosphinx][CreatureConstants.Gynosphinx] = GetMultiplierFromAverage(7 * 12);
            //Source: https://www.d20srd.org/srd/description.htm#vitalStatistics
            heights[CreatureConstants.Halfling_Deep][GenderConstants.Female] = "2*12+6";
            heights[CreatureConstants.Halfling_Deep][GenderConstants.Male] = "2*12+8";
            heights[CreatureConstants.Halfling_Deep][CreatureConstants.Halfling_Deep] = "2d4";
            heights[CreatureConstants.Halfling_Lightfoot][GenderConstants.Female] = "2*12+6";
            heights[CreatureConstants.Halfling_Lightfoot][GenderConstants.Male] = "2*12+8";
            heights[CreatureConstants.Halfling_Lightfoot][CreatureConstants.Halfling_Lightfoot] = "2d4";
            heights[CreatureConstants.Halfling_Tallfellow][GenderConstants.Female] = "3*12+6";
            heights[CreatureConstants.Halfling_Tallfellow][GenderConstants.Male] = "3*12+8";
            heights[CreatureConstants.Halfling_Tallfellow][CreatureConstants.Halfling_Tallfellow] = "2d4";
            heights[CreatureConstants.Hellcat_Bezekira][GenderConstants.Agender] = "0";
            heights[CreatureConstants.Hellcat_Bezekira][CreatureConstants.Hellcat_Bezekira] = "0";
            //Source: https://www.d20srd.org/srd/monsters/swarm.htm
            heights[CreatureConstants.Hellwasp_Swarm][GenderConstants.Agender] = GetBaseFromAverage(10 * 12);
            heights[CreatureConstants.Hellwasp_Swarm][CreatureConstants.Hellwasp_Swarm] = GetMultiplierFromAverage(10 * 12);
            //Source: https://www.d20srd.org/srd/monsters/demon.htm#hezrou
            heights[CreatureConstants.Hezrou][GenderConstants.Agender] = GetBaseFromAverage(8 * 12);
            heights[CreatureConstants.Hezrou][CreatureConstants.Hezrou] = GetMultiplierFromAverage(8 * 12);
            //Source: https://www.mojobob.com/roleplay/monstrousmanual/s/sphinx.html
            heights[CreatureConstants.Hieracosphinx][GenderConstants.Male] = GetBaseFromAverage(7 * 12);
            heights[CreatureConstants.Hieracosphinx][CreatureConstants.Hieracosphinx] = GetMultiplierFromAverage(7 * 12);
            //Source: http://people.wku.edu/charles.plemons/ad&d/races/height.html
            heights[CreatureConstants.Hobgoblin][GenderConstants.Female] = "68";
            heights[CreatureConstants.Hobgoblin][GenderConstants.Male] = "72";
            heights[CreatureConstants.Hobgoblin][CreatureConstants.Hobgoblin] = "1d8";
            //Source: https://www.d20srd.org/srd/monsters/devil.htm#hornedDevilCornugon
            heights[CreatureConstants.HornedDevil_Cornugon][GenderConstants.Agender] = GetBaseFromAverage(9 * 12);
            heights[CreatureConstants.HornedDevil_Cornugon][CreatureConstants.HornedDevil_Cornugon] = GetMultiplierFromAverage(9 * 12);
            //Source: https://www.dimensions.com/element/clydesdale-horse
            heights[CreatureConstants.Horse_Heavy][GenderConstants.Female] = GetBaseFromRange(64, 72);
            heights[CreatureConstants.Horse_Heavy][GenderConstants.Male] = GetBaseFromRange(64, 72);
            heights[CreatureConstants.Horse_Heavy][CreatureConstants.Horse_Heavy] = GetMultiplierFromRange(64, 72);
            //Source: https://www.dimensions.com/element/arabian-horse
            heights[CreatureConstants.Horse_Light][GenderConstants.Female] = GetBaseFromRange(57, 61);
            heights[CreatureConstants.Horse_Light][GenderConstants.Male] = GetBaseFromRange(57, 61);
            heights[CreatureConstants.Horse_Light][CreatureConstants.Horse_Light] = GetMultiplierFromRange(57, 61);
            //Source: https://www.dimensions.com/element/clydesdale-horse
            heights[CreatureConstants.Horse_Heavy_War][GenderConstants.Female] = GetBaseFromRange(64, 72);
            heights[CreatureConstants.Horse_Heavy_War][GenderConstants.Male] = GetBaseFromRange(64, 72);
            heights[CreatureConstants.Horse_Heavy_War][CreatureConstants.Horse_Heavy_War] = GetMultiplierFromRange(64, 72);
            //Source: https://www.dimensions.com/element/arabian-horse
            heights[CreatureConstants.Horse_Light_War][GenderConstants.Female] = GetBaseFromRange(57, 61);
            heights[CreatureConstants.Horse_Light_War][GenderConstants.Male] = GetBaseFromRange(57, 61);
            heights[CreatureConstants.Horse_Light_War][CreatureConstants.Horse_Light_War] = GetMultiplierFromRange(57, 61);
            //Source: https://www.d20srd.org/srd/description.htm#vitalStatistics
            heights[CreatureConstants.Human][GenderConstants.Female] = "4*12+5";
            heights[CreatureConstants.Human][GenderConstants.Male] = "4*12+10";
            heights[CreatureConstants.Human][CreatureConstants.Human] = "2d10";
            //Source: https://forgottenrealms.fandom.com/wiki/Hydra half of length for height
            heights[CreatureConstants.Hydra_5Heads][GenderConstants.Female] = GetBaseFromAverage(20 * 12 / 2);
            heights[CreatureConstants.Hydra_5Heads][GenderConstants.Male] = GetBaseFromAverage(20 * 12 / 2);
            heights[CreatureConstants.Hydra_5Heads][CreatureConstants.Hydra_5Heads] = GetMultiplierFromAverage(20 * 12 / 2);
            heights[CreatureConstants.Hydra_6Heads][GenderConstants.Female] = GetBaseFromAverage(20 * 12 / 2);
            heights[CreatureConstants.Hydra_6Heads][GenderConstants.Male] = GetBaseFromAverage(20 * 12 / 2);
            heights[CreatureConstants.Hydra_6Heads][CreatureConstants.Hydra_6Heads] = GetMultiplierFromAverage(20 * 12 / 2);
            heights[CreatureConstants.Hydra_7Heads][GenderConstants.Female] = GetBaseFromAverage(20 * 12 / 2);
            heights[CreatureConstants.Hydra_7Heads][GenderConstants.Male] = GetBaseFromAverage(20 * 12 / 2);
            heights[CreatureConstants.Hydra_7Heads][CreatureConstants.Hydra_7Heads] = GetMultiplierFromAverage(20 * 12 / 2);
            heights[CreatureConstants.Hydra_8Heads][GenderConstants.Female] = GetBaseFromAverage(20 * 12 / 2);
            heights[CreatureConstants.Hydra_8Heads][GenderConstants.Male] = GetBaseFromAverage(20 * 12 / 2);
            heights[CreatureConstants.Hydra_8Heads][CreatureConstants.Hydra_8Heads] = GetMultiplierFromAverage(20 * 12 / 2);
            heights[CreatureConstants.Hydra_9Heads][GenderConstants.Female] = GetBaseFromAverage(20 * 12 / 2);
            heights[CreatureConstants.Hydra_9Heads][GenderConstants.Male] = GetBaseFromAverage(20 * 12 / 2);
            heights[CreatureConstants.Hydra_9Heads][CreatureConstants.Hydra_9Heads] = GetMultiplierFromAverage(20 * 12 / 2);
            heights[CreatureConstants.Hydra_10Heads][GenderConstants.Female] = GetBaseFromAverage(20 * 12 / 2);
            heights[CreatureConstants.Hydra_10Heads][GenderConstants.Male] = GetBaseFromAverage(20 * 12 / 2);
            heights[CreatureConstants.Hydra_10Heads][CreatureConstants.Hydra_10Heads] = GetMultiplierFromAverage(20 * 12 / 2);
            heights[CreatureConstants.Hydra_11Heads][GenderConstants.Female] = GetBaseFromAverage(20 * 12 / 2);
            heights[CreatureConstants.Hydra_11Heads][GenderConstants.Male] = GetBaseFromAverage(20 * 12 / 2);
            heights[CreatureConstants.Hydra_11Heads][CreatureConstants.Hydra_11Heads] = GetMultiplierFromAverage(20 * 12 / 2);
            heights[CreatureConstants.Hydra_12Heads][GenderConstants.Female] = GetBaseFromAverage(20 * 12 / 2);
            heights[CreatureConstants.Hydra_12Heads][GenderConstants.Male] = GetBaseFromAverage(20 * 12 / 2);
            heights[CreatureConstants.Hydra_12Heads][CreatureConstants.Hydra_12Heads] = GetMultiplierFromAverage(20 * 12 / 2);
            //Source: https://forgottenrealms.fandom.com/wiki/Gelugon
            heights[CreatureConstants.IceDevil_Gelugon][GenderConstants.Agender] = GetBaseFromRange(10 * 12 + 6, 12 * 12);
            heights[CreatureConstants.IceDevil_Gelugon][CreatureConstants.IceDevil_Gelugon] = GetMultiplierFromRange(10 * 12 + 6, 12 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Imp
            heights[CreatureConstants.Imp][GenderConstants.Agender] = GetBaseFromAverage(2 * 12);
            heights[CreatureConstants.Imp][CreatureConstants.Imp] = GetMultiplierFromAverage(2 * 12);
            //Source: http://people.wku.edu/charles.plemons/ad&d/races/height.html
            heights[CreatureConstants.Kobold][GenderConstants.Female] = "30";
            heights[CreatureConstants.Kobold][GenderConstants.Male] = "32";
            heights[CreatureConstants.Kobold][CreatureConstants.Kobold] = "3d4";
            //Source: https://forgottenrealms.fandom.com/wiki/Lemure
            heights[CreatureConstants.Lemure][GenderConstants.Agender] = GetBaseFromAverage(5 * 12);
            heights[CreatureConstants.Lemure][CreatureConstants.Lemure] = GetMultiplierFromAverage(5 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Leonal
            heights[CreatureConstants.Leonal][GenderConstants.Female] = GetBaseFromAverage(6 * 12);
            heights[CreatureConstants.Leonal][GenderConstants.Male] = GetBaseFromAverage(6 * 12);
            heights[CreatureConstants.Leonal][CreatureConstants.Leonal] = GetMultiplierFromAverage(6 * 12);
            //Source: https://www.dimensions.com/element/african-lion
            heights[CreatureConstants.Lion][GenderConstants.Female] = GetBaseFromAverage(2 * 12 + 10);
            heights[CreatureConstants.Lion][GenderConstants.Male] = GetBaseFromAverage(3 * 12 + 4);
            heights[CreatureConstants.Lion][CreatureConstants.Lion] = GetMultiplierFromAverage(3 * 12 + 4);
            //Scaling up from lion, x3 based on length
            heights[CreatureConstants.Lion_Dire][GenderConstants.Female] = GetBaseFromAverage(120);
            heights[CreatureConstants.Lion_Dire][GenderConstants.Male] = GetBaseFromAverage(120);
            heights[CreatureConstants.Lion_Dire][CreatureConstants.Lion_Dire] = GetMultiplierFromAverage(120);
            //Source: http://people.wku.edu/charles.plemons/ad&d/races/height.html
            heights[CreatureConstants.Lizardfolk][GenderConstants.Female] = "60";
            heights[CreatureConstants.Lizardfolk][GenderConstants.Male] = "60";
            heights[CreatureConstants.Lizardfolk][CreatureConstants.Lizardfolk] = "2d12";
            //Source: http://people.wku.edu/charles.plemons/ad&d/races/height.html
            heights[CreatureConstants.Locathah][GenderConstants.Female] = "60";
            heights[CreatureConstants.Locathah][GenderConstants.Male] = "60";
            heights[CreatureConstants.Locathah][CreatureConstants.Locathah] = "1d12";
            //Source: https://www.d20srd.org/srd/monsters/swarm.htm
            heights[CreatureConstants.Locust_Swarm][GenderConstants.Agender] = GetBaseFromAverage(10 * 12);
            heights[CreatureConstants.Locust_Swarm][CreatureConstants.Locust_Swarm] = GetMultiplierFromAverage(10 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Marilith
            heights[CreatureConstants.Marilith][GenderConstants.Female] = GetBaseFromRange(7 * 12, 9 * 12);
            heights[CreatureConstants.Marilith][CreatureConstants.Marilith] = GetMultiplierFromRange(7 * 12, 9 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Mephit
            heights[CreatureConstants.Mephit_Air][GenderConstants.Agender] = GetBaseFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Air][GenderConstants.Female] = GetBaseFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Air][GenderConstants.Male] = GetBaseFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Air][CreatureConstants.Mephit_Air] = GetMultiplierFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Dust][GenderConstants.Agender] = GetBaseFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Dust][GenderConstants.Female] = GetBaseFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Dust][GenderConstants.Male] = GetBaseFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Dust][CreatureConstants.Mephit_Dust] = GetMultiplierFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Earth][GenderConstants.Agender] = GetBaseFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Earth][GenderConstants.Female] = GetBaseFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Earth][GenderConstants.Male] = GetBaseFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Earth][CreatureConstants.Mephit_Earth] = GetMultiplierFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Fire][GenderConstants.Agender] = GetBaseFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Fire][GenderConstants.Female] = GetBaseFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Fire][GenderConstants.Male] = GetBaseFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Fire][CreatureConstants.Mephit_Fire] = GetMultiplierFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Ice][GenderConstants.Agender] = GetBaseFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Ice][GenderConstants.Female] = GetBaseFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Ice][GenderConstants.Male] = GetBaseFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Ice][CreatureConstants.Mephit_Ice] = GetMultiplierFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Magma][GenderConstants.Agender] = GetBaseFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Magma][GenderConstants.Female] = GetBaseFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Magma][GenderConstants.Male] = GetBaseFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Magma][CreatureConstants.Mephit_Magma] = GetMultiplierFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Ooze][GenderConstants.Agender] = GetBaseFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Ooze][GenderConstants.Female] = GetBaseFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Ooze][GenderConstants.Male] = GetBaseFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Ooze][CreatureConstants.Mephit_Ooze] = GetMultiplierFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Salt][GenderConstants.Agender] = GetBaseFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Salt][GenderConstants.Female] = GetBaseFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Salt][GenderConstants.Male] = GetBaseFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Salt][CreatureConstants.Mephit_Salt] = GetMultiplierFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Steam][GenderConstants.Agender] = GetBaseFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Steam][GenderConstants.Female] = GetBaseFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Steam][GenderConstants.Male] = GetBaseFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Steam][CreatureConstants.Mephit_Steam] = GetMultiplierFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Water][GenderConstants.Agender] = GetBaseFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Water][GenderConstants.Female] = GetBaseFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Water][GenderConstants.Male] = GetBaseFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Water][CreatureConstants.Mephit_Water] = GetMultiplierFromAverage(4 * 12);
            //Source: http://people.wku.edu/charles.plemons/ad&d/races/height.html
            heights[CreatureConstants.Merfolk][GenderConstants.Female] = "54";
            heights[CreatureConstants.Merfolk][GenderConstants.Male] = "60";
            heights[CreatureConstants.Merfolk][CreatureConstants.Merfolk] = "1d12";
            //Source: http://people.wku.edu/charles.plemons/ad&d/races/height.html
            heights[CreatureConstants.Minotaur][GenderConstants.Female] = "84";
            heights[CreatureConstants.Minotaur][GenderConstants.Male] = "80";
            heights[CreatureConstants.Minotaur][CreatureConstants.Minotaur] = "2d6";
            heights[CreatureConstants.Naga_Dark][GenderConstants.Hermaphrodite] = "0";
            heights[CreatureConstants.Naga_Dark][CreatureConstants.Naga_Dark] = "0";
            heights[CreatureConstants.Naga_Guardian][GenderConstants.Hermaphrodite] = "0";
            heights[CreatureConstants.Naga_Guardian][CreatureConstants.Naga_Guardian] = "0";
            heights[CreatureConstants.Naga_Spirit][GenderConstants.Hermaphrodite] = "0";
            heights[CreatureConstants.Naga_Spirit][CreatureConstants.Naga_Spirit] = "0";
            heights[CreatureConstants.Naga_Water][GenderConstants.Hermaphrodite] = "0";
            heights[CreatureConstants.Naga_Water][CreatureConstants.Naga_Water] = "0";
            //Source: https://forgottenrealms.fandom.com/wiki/Nalfeshnee
            heights[CreatureConstants.Nalfeshnee][GenderConstants.Agender] = GetBaseFromRange(10 * 12, 20 * 12);
            heights[CreatureConstants.Nalfeshnee][CreatureConstants.Nalfeshnee] = GetMultiplierFromRange(10 * 12, 20 * 12);
            //Source: https://www.dimensions.com/element/common-octopus-octopus-vulgaris (mantle length)
            heights[CreatureConstants.Octopus_Giant][GenderConstants.Female] = GetBaseFromRange(20, 24);
            heights[CreatureConstants.Octopus_Giant][GenderConstants.Male] = GetBaseFromRange(20, 24);
            heights[CreatureConstants.Octopus_Giant][CreatureConstants.Octopus_Giant] = GetMultiplierFromRange(20, 24);
            //Source: http://people.wku.edu/charles.plemons/ad&d/races/height.html
            heights[CreatureConstants.Ogre][GenderConstants.Female] = "93";
            heights[CreatureConstants.Ogre][GenderConstants.Male] = "96";
            heights[CreatureConstants.Ogre][CreatureConstants.Ogre] = "2d12";
            heights[CreatureConstants.Ogre_Merrow][GenderConstants.Female] = "93";
            heights[CreatureConstants.Ogre_Merrow][GenderConstants.Male] = "96";
            heights[CreatureConstants.Ogre_Merrow][CreatureConstants.Ogre] = "2d12";
            //Source: http://people.wku.edu/charles.plemons/ad&d/races/height.html
            heights[CreatureConstants.OgreMage][GenderConstants.Female] = "96";
            heights[CreatureConstants.OgreMage][GenderConstants.Male] = "114";
            heights[CreatureConstants.OgreMage][CreatureConstants.OgreMage] = "2d6";
            //Source: http://people.wku.edu/charles.plemons/ad&d/races/height.html
            heights[CreatureConstants.Orc][GenderConstants.Female] = "56";
            heights[CreatureConstants.Orc][GenderConstants.Male] = "58";
            heights[CreatureConstants.Orc][CreatureConstants.Orc] = "1d12";
            //Source: https://www.d20srd.org/srd/description.htm#vitalStatistics
            heights[CreatureConstants.Orc_Half][GenderConstants.Female] = "4*12+5";
            heights[CreatureConstants.Orc_Half][GenderConstants.Male] = "4*12+10";
            heights[CreatureConstants.Orc_Half][CreatureConstants.Orc_Half] = "2d12";
            //Source: https://www.d20srd.org/srd/monsters/owlGiant.htm
            heights[CreatureConstants.Owl_Giant][GenderConstants.Female] = GetBaseFromAverage(9 * 12);
            heights[CreatureConstants.Owl_Giant][GenderConstants.Male] = GetBaseFromAverage(9 * 12);
            heights[CreatureConstants.Owl_Giant][CreatureConstants.Owl_Giant] = GetMultiplierFromAverage(9 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Pit_fiend
            heights[CreatureConstants.PitFiend][GenderConstants.Agender] = GetBaseFromAverage(12 * 12);
            heights[CreatureConstants.PitFiend][CreatureConstants.PitFiend] = GetMultiplierFromAverage(12 * 12);
            //Source: http://people.wku.edu/charles.plemons/ad&d/races/height.html
            heights[CreatureConstants.Pixie][GenderConstants.Female] = "23";
            heights[CreatureConstants.Pixie][GenderConstants.Male] = "24";
            heights[CreatureConstants.Pixie][CreatureConstants.Pixie] = "3d6";
            heights[CreatureConstants.Pixie_WithIrresistibleDance][GenderConstants.Female] = "23";
            heights[CreatureConstants.Pixie_WithIrresistibleDance][GenderConstants.Male] = "24";
            heights[CreatureConstants.Pixie_WithIrresistibleDance][CreatureConstants.Pixie_WithIrresistibleDance] = "3d6";
            //Source: https://forgottenrealms.fandom.com/wiki/Giant_praying_mantis
            heights[CreatureConstants.PrayingMantis_Giant][GenderConstants.Female] = GetBaseFromRange(2 * 12, 5 * 12);
            heights[CreatureConstants.PrayingMantis_Giant][GenderConstants.Male] = GetBaseFromRange(2 * 12, 5 * 12);
            heights[CreatureConstants.PrayingMantis_Giant][CreatureConstants.PrayingMantis_Giant] = GetMultiplierFromRange(2 * 12, 5 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Hydra half of length for height
            heights[CreatureConstants.Pyrohydra_5Heads][GenderConstants.Female] = GetBaseFromAverage(20 * 12 / 2);
            heights[CreatureConstants.Pyrohydra_5Heads][GenderConstants.Male] = GetBaseFromAverage(20 * 12 / 2);
            heights[CreatureConstants.Pyrohydra_5Heads][CreatureConstants.Pyrohydra_5Heads] = GetMultiplierFromAverage(20 * 12 / 2);
            heights[CreatureConstants.Pyrohydra_6Heads][GenderConstants.Female] = GetBaseFromAverage(20 * 12 / 2);
            heights[CreatureConstants.Pyrohydra_6Heads][GenderConstants.Male] = GetBaseFromAverage(20 * 12 / 2);
            heights[CreatureConstants.Pyrohydra_6Heads][CreatureConstants.Pyrohydra_6Heads] = GetMultiplierFromAverage(20 * 12 / 2);
            heights[CreatureConstants.Pyrohydra_7Heads][GenderConstants.Female] = GetBaseFromAverage(20 * 12 / 2);
            heights[CreatureConstants.Pyrohydra_7Heads][GenderConstants.Male] = GetBaseFromAverage(20 * 12 / 2);
            heights[CreatureConstants.Pyrohydra_7Heads][CreatureConstants.Pyrohydra_7Heads] = GetMultiplierFromAverage(20 * 12 / 2);
            heights[CreatureConstants.Pyrohydra_8Heads][GenderConstants.Female] = GetBaseFromAverage(20 * 12 / 2);
            heights[CreatureConstants.Pyrohydra_8Heads][GenderConstants.Male] = GetBaseFromAverage(20 * 12 / 2);
            heights[CreatureConstants.Pyrohydra_8Heads][CreatureConstants.Pyrohydra_8Heads] = GetMultiplierFromAverage(20 * 12 / 2);
            heights[CreatureConstants.Pyrohydra_9Heads][GenderConstants.Female] = GetBaseFromAverage(20 * 12 / 2);
            heights[CreatureConstants.Pyrohydra_9Heads][GenderConstants.Male] = GetBaseFromAverage(20 * 12 / 2);
            heights[CreatureConstants.Pyrohydra_9Heads][CreatureConstants.Pyrohydra_9Heads] = GetMultiplierFromAverage(20 * 12 / 2);
            heights[CreatureConstants.Pyrohydra_10Heads][GenderConstants.Female] = GetBaseFromAverage(20 * 12 / 2);
            heights[CreatureConstants.Pyrohydra_10Heads][GenderConstants.Male] = GetBaseFromAverage(20 * 12 / 2);
            heights[CreatureConstants.Pyrohydra_10Heads][CreatureConstants.Pyrohydra_10Heads] = GetMultiplierFromAverage(20 * 12 / 2);
            heights[CreatureConstants.Pyrohydra_11Heads][GenderConstants.Female] = GetBaseFromAverage(20 * 12 / 2);
            heights[CreatureConstants.Pyrohydra_11Heads][GenderConstants.Male] = GetBaseFromAverage(20 * 12 / 2);
            heights[CreatureConstants.Pyrohydra_11Heads][CreatureConstants.Pyrohydra_11Heads] = GetMultiplierFromAverage(20 * 12 / 2);
            heights[CreatureConstants.Pyrohydra_12Heads][GenderConstants.Female] = GetBaseFromAverage(20 * 12 / 2);
            heights[CreatureConstants.Pyrohydra_12Heads][GenderConstants.Male] = GetBaseFromAverage(20 * 12 / 2);
            heights[CreatureConstants.Pyrohydra_12Heads][CreatureConstants.Pyrohydra_12Heads] = GetMultiplierFromAverage(20 * 12 / 2);
            //Source: https://forgottenrealms.fandom.com/wiki/Quasit
            heights[CreatureConstants.Quasit][GenderConstants.Agender] = GetBaseFromRange(12, 24);
            heights[CreatureConstants.Quasit][CreatureConstants.Quasit] = GetMultiplierFromRange(12, 24);
            //Source: https://www.dimensions.com/element/common-rat
            heights[CreatureConstants.Rat][GenderConstants.Female] = GetBaseFromRange(2, 4);
            heights[CreatureConstants.Rat][GenderConstants.Male] = GetBaseFromRange(2, 4);
            heights[CreatureConstants.Rat][CreatureConstants.Rat] = GetMultiplierFromRange(2, 4);
            //Scaled up from Rat, x6 based on length
            heights[CreatureConstants.Rat_Dire][GenderConstants.Female] = GetBaseFromRange(12, 24);
            heights[CreatureConstants.Rat_Dire][GenderConstants.Male] = GetBaseFromRange(12, 24);
            heights[CreatureConstants.Rat_Dire][CreatureConstants.Rat_Dire] = GetMultiplierFromRange(12, 24);
            //Source: https://www.d20srd.org/srd/monsters/swarm.htm
            heights[CreatureConstants.Rat_Swarm][GenderConstants.Agender] = "0";
            heights[CreatureConstants.Rat_Swarm][CreatureConstants.Rat_Swarm] = "0";
            //Source: https://forgottenrealms.fandom.com/wiki/Retriever
            heights[CreatureConstants.Retriever][GenderConstants.Agender] = GetBaseFromAverage(12 * 12);
            heights[CreatureConstants.Retriever][CreatureConstants.Retriever] = GetMultiplierFromAverage(12 * 12);
            //Source: http://people.wku.edu/charles.plemons/ad&d/races/height.html
            heights[CreatureConstants.Sahuagin][GenderConstants.Female] = "50";
            heights[CreatureConstants.Sahuagin][GenderConstants.Male] = "50";
            heights[CreatureConstants.Sahuagin][CreatureConstants.Locathah] = "1d8";
            heights[CreatureConstants.Sahuagin_Malenti][GenderConstants.Female] = "50";
            heights[CreatureConstants.Sahuagin_Malenti][GenderConstants.Male] = "50";
            heights[CreatureConstants.Sahuagin_Malenti][CreatureConstants.Locathah] = "1d8";
            heights[CreatureConstants.Sahuagin_Mutant][GenderConstants.Female] = "50";
            heights[CreatureConstants.Sahuagin_Mutant][GenderConstants.Male] = "50";
            heights[CreatureConstants.Sahuagin_Mutant][CreatureConstants.Locathah] = "1d8";
            //Source: https://www.worldanvil.com/w/faerun-tatortotzke/a/salamander-article (average)
            //Scaling down by half for flamebrother, Scaling up x2 for noble. Assuming height is half of length
            heights[CreatureConstants.Salamander_Flamebrother][GenderConstants.Agender] = GetBaseFromAverage(20 * 12 / 4);
            heights[CreatureConstants.Salamander_Flamebrother][CreatureConstants.Salamander_Flamebrother] = GetMultiplierFromAverage(20 * 12 / 4);
            heights[CreatureConstants.Salamander_Average][GenderConstants.Agender] = GetBaseFromAverage(20 * 12 / 2);
            heights[CreatureConstants.Salamander_Average][CreatureConstants.Salamander_Average] = GetMultiplierFromAverage(20 * 12 / 2);
            heights[CreatureConstants.Salamander_Noble][GenderConstants.Agender] = GetBaseFromAverage(20 * 12);
            heights[CreatureConstants.Salamander_Noble][CreatureConstants.Salamander_Noble] = GetMultiplierFromAverage(20 * 12);
            //Source: https://www.d20srd.org/srd/monsters/satyr.htm - copy from Half-Elf
            heights[CreatureConstants.Satyr][GenderConstants.Male] = "4*12+7";
            heights[CreatureConstants.Satyr][CreatureConstants.Satyr] = "2d8";
            heights[CreatureConstants.Satyr_WithPipes][GenderConstants.Male] = "4*12+7";
            heights[CreatureConstants.Satyr_WithPipes][CreatureConstants.Satyr_WithPipes] = "2d8";
            //Source: https://www.d20srd.org/srd/monsters/hag.htm - copy from Human
            heights[CreatureConstants.SeaHag][GenderConstants.Female] = "4*12+5";
            heights[CreatureConstants.SeaHag][CreatureConstants.SeaHag] = "2d10";
            //Source: https://www.dimensions.com/element/blacktip-shark-carcharhinus-limbatus
            heights[CreatureConstants.Shark_Medium][GenderConstants.Female] = "0";
            heights[CreatureConstants.Shark_Medium][GenderConstants.Male] = "0";
            heights[CreatureConstants.Shark_Medium][CreatureConstants.Shark_Medium] = "0";
            //Source: https://www.dimensions.com/element/thresher-shark
            heights[CreatureConstants.Shark_Large][GenderConstants.Female] = "0";
            heights[CreatureConstants.Shark_Large][GenderConstants.Male] = "0";
            heights[CreatureConstants.Shark_Large][CreatureConstants.Shark_Large] = "0";
            //Source: https://www.dimensions.com/element/great-white-shark
            heights[CreatureConstants.Shark_Huge][GenderConstants.Female] = "0";
            heights[CreatureConstants.Shark_Huge][GenderConstants.Male] = "0";
            heights[CreatureConstants.Shark_Huge][CreatureConstants.Shark_Huge] = "0";
            heights[CreatureConstants.Shark_Dire][GenderConstants.Female] = "0";
            heights[CreatureConstants.Shark_Dire][GenderConstants.Male] = "0";
            heights[CreatureConstants.Shark_Dire][CreatureConstants.Shark_Dire] = "0";
            //Source: https://forgottenrealms.fandom.com/wiki/Blue_slaad
            heights[CreatureConstants.Slaad_Blue][GenderConstants.Agender] = GetBaseFromAverage(10 * 12);
            heights[CreatureConstants.Slaad_Blue][CreatureConstants.Slaad_Blue] = GetMultiplierFromAverage(10 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Red_slaad
            heights[CreatureConstants.Slaad_Red][GenderConstants.Agender] = GetBaseFromAverage(8 * 12);
            heights[CreatureConstants.Slaad_Red][CreatureConstants.Slaad_Red] = GetMultiplierFromAverage(8 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Green_slaad
            heights[CreatureConstants.Slaad_Green][GenderConstants.Agender] = GetBaseFromAverage(7 * 12);
            heights[CreatureConstants.Slaad_Green][CreatureConstants.Slaad_Green] = GetMultiplierFromAverage(7 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Gray_slaad
            heights[CreatureConstants.Slaad_Gray][GenderConstants.Agender] = GetBaseFromAverage(6 * 12);
            heights[CreatureConstants.Slaad_Gray][CreatureConstants.Slaad_Gray] = GetMultiplierFromAverage(6 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Gray_slaad
            heights[CreatureConstants.Slaad_Death][GenderConstants.Agender] = GetBaseFromAverage(6 * 12);
            heights[CreatureConstants.Slaad_Death][CreatureConstants.Slaad_Death] = GetMultiplierFromAverage(6 * 12);
            //Source: https://www.dimensions.com/element/green-tree-python-morelia-viridis
            heights[CreatureConstants.Snake_Constrictor][GenderConstants.Female] = GetBaseFromRange(1, 2);
            heights[CreatureConstants.Snake_Constrictor][GenderConstants.Male] = GetBaseFromRange(1, 2);
            heights[CreatureConstants.Snake_Constrictor][CreatureConstants.Snake_Constrictor] = GetMultiplierFromRange(1, 2);
            //Source: https://www.dimensions.com/element/burmese-python-python-bivittatus
            heights[CreatureConstants.Snake_Constrictor_Giant][GenderConstants.Female] = GetBaseFromRange(3, 9);
            heights[CreatureConstants.Snake_Constrictor_Giant][GenderConstants.Male] = GetBaseFromRange(3, 9);
            heights[CreatureConstants.Snake_Constrictor_Giant][CreatureConstants.Snake_Constrictor_Giant] = GetMultiplierFromRange(3, 9);
            //Source: https://www.d20srd.org/srd/monsters/swarm.htm
            heights[CreatureConstants.Spider_Swarm][GenderConstants.Agender] = "0";
            heights[CreatureConstants.Spider_Swarm][CreatureConstants.Spider_Swarm] = "0";
            heights[CreatureConstants.Squid_Giant][GenderConstants.Female] = "0";
            heights[CreatureConstants.Squid_Giant][GenderConstants.Male] = "0";
            heights[CreatureConstants.Squid_Giant][CreatureConstants.Squid_Giant] = "0";
            //Source: https://www.d20srd.org/srd/monsters/giantStagBeetle.htm
            //https://www.dimensions.com/element/hercules-beetle-dynastes-hercules scale up: [.47,1.42]*10*12/[2.36,7.09] = [24,24]
            heights[CreatureConstants.StagBeetle_Giant][GenderConstants.Female] = GetBaseFromAverage(24);
            heights[CreatureConstants.StagBeetle_Giant][GenderConstants.Male] = GetBaseFromAverage(24);
            heights[CreatureConstants.StagBeetle_Giant][CreatureConstants.StagBeetle_Giant] = GetMultiplierFromAverage(24);
            //Source: https://forgottenrealms.fandom.com/wiki/Succubus
            heights[CreatureConstants.Succubus][GenderConstants.Female] = GetBaseFromAverage(6 * 12);
            heights[CreatureConstants.Succubus][GenderConstants.Male] = GetBaseFromAverage(6 * 12);
            heights[CreatureConstants.Succubus][CreatureConstants.Succubus] = GetMultiplierFromAverage(6 * 12);
            //Source: http://people.wku.edu/charles.plemons/ad&d/races/height.html
            heights[CreatureConstants.Tiefling][GenderConstants.Female] = "57";
            heights[CreatureConstants.Tiefling][GenderConstants.Male] = "59";
            heights[CreatureConstants.Tiefling][CreatureConstants.Tiefling] = "2d10";
            //Source: https://www.dimensions.com/element/bengal-tiger
            heights[CreatureConstants.Tiger][GenderConstants.Female] = GetBaseFromRange(34, 45);
            heights[CreatureConstants.Tiger][GenderConstants.Male] = GetBaseFromRange(34, 45);
            heights[CreatureConstants.Tiger][CreatureConstants.Tiger] = GetMultiplierFromRange(34, 45);
            //Scaled up from Tiger, x2 bsed on length
            heights[CreatureConstants.Tiger_Dire][GenderConstants.Female] = GetBaseFromRange(34 * 2, 45 * 2);
            heights[CreatureConstants.Tiger_Dire][GenderConstants.Male] = GetBaseFromRange(34 * 2, 45 * 2);
            heights[CreatureConstants.Tiger_Dire][CreatureConstants.Tiger_Dire] = GetMultiplierFromRange(34 * 2, 45 * 2);
            heights[CreatureConstants.Tojanida_Juvenile][GenderConstants.Agender] = "0";
            heights[CreatureConstants.Tojanida_Juvenile][CreatureConstants.Tojanida_Juvenile] = "0";
            heights[CreatureConstants.Tojanida_Adult][GenderConstants.Agender] = "0";
            heights[CreatureConstants.Tojanida_Adult][CreatureConstants.Tojanida_Adult] = "0";
            heights[CreatureConstants.Tojanida_Elder][GenderConstants.Agender] = "0";
            heights[CreatureConstants.Tojanida_Elder][CreatureConstants.Tojanida_Elder] = "0";
            //Source: https://forgottenrealms.fandom.com/wiki/Vrock
            heights[CreatureConstants.Vrock][GenderConstants.Agender] = GetBaseFromAverage(8 * 12);
            heights[CreatureConstants.Vrock][CreatureConstants.Vrock] = GetMultiplierFromAverage(8 * 12);
            //Source: https://www.dimensions.com/element/red-paper-wasp-polistes-carolina
            //https://forgottenrealms.fandom.com/wiki/Giant_wasp scale up: [.23,.33]*5*12/[.94,1.26] = [14,16]
            heights[CreatureConstants.Wasp_Giant][GenderConstants.Male] = GetBaseFromRange(14, 16);
            heights[CreatureConstants.Wasp_Giant][CreatureConstants.Wasp_Giant] = GetMultiplierFromRange(14, 16);
            //Source: https://www.dimensions.com/element/least-weasel-mustela-nivalis
            heights[CreatureConstants.Weasel][GenderConstants.Female] = GetBaseFromRange(2, 3);
            heights[CreatureConstants.Weasel][GenderConstants.Male] = GetBaseFromRange(2, 3);
            heights[CreatureConstants.Weasel][CreatureConstants.Weasel] = GetMultiplierFromRange(2, 3);
            //Scaled up from weasel, x15 based on length
            heights[CreatureConstants.Weasel_Dire][GenderConstants.Female] = GetBaseFromRange(30, 45);
            heights[CreatureConstants.Weasel_Dire][GenderConstants.Male] = GetBaseFromRange(30, 45);
            heights[CreatureConstants.Weasel_Dire][CreatureConstants.Weasel_Dire] = GetMultiplierFromRange(30, 45);
            //Source: https://www.dimensions.com/element/humpback-whale-megaptera-novaeangliae
            heights[CreatureConstants.Whale_Baleen][GenderConstants.Female] = GetBaseFromRange(8 * 12, 9 * 12 + 8);
            heights[CreatureConstants.Whale_Baleen][GenderConstants.Male] = GetBaseFromRange(8 * 12, 9 * 12 + 8);
            heights[CreatureConstants.Whale_Baleen][CreatureConstants.Whale_Baleen] = GetMultiplierFromRange(8 * 12, 9 * 12 + 8);
            //Source: https://www.dimensions.com/element/sperm-whale-physeter-macrocephalus
            heights[CreatureConstants.Whale_Cachalot][GenderConstants.Female] = GetBaseFromRange(6 * 12 + 9, 11 * 12);
            heights[CreatureConstants.Whale_Cachalot][GenderConstants.Male] = GetBaseFromRange(6 * 12 + 9, 11 * 12);
            heights[CreatureConstants.Whale_Cachalot][CreatureConstants.Whale_Cachalot] = GetMultiplierFromRange(6 * 12 + 9, 11 * 12);
            //Source: https://www.dimensions.com/element/orca-killer-whale-orcinus-orca
            heights[CreatureConstants.Whale_Orca][GenderConstants.Female] = GetBaseFromRange(5 * 12 + 3, 7 * 12 + 6);
            heights[CreatureConstants.Whale_Orca][GenderConstants.Male] = GetBaseFromRange(5 * 12 + 3, 7 * 12 + 6);
            heights[CreatureConstants.Whale_Orca][CreatureConstants.Whale_Orca] = GetMultiplierFromRange(5 * 12 + 3, 7 * 12 + 6);
            //Source: https://www.dimensions.com/element/gray-wolf
            heights[CreatureConstants.Wolf][GenderConstants.Female] = GetBaseFromRange(26, 33);
            heights[CreatureConstants.Wolf][GenderConstants.Male] = GetBaseFromRange(26, 33);
            heights[CreatureConstants.Wolf][CreatureConstants.Wolf] = GetMultiplierFromRange(26, 33);
            //Scaled up from Wolf, x2 based on length
            heights[CreatureConstants.Wolf_Dire][GenderConstants.Female] = GetBaseFromRange(26 * 2, 33 * 2);
            heights[CreatureConstants.Wolf_Dire][GenderConstants.Male] = GetBaseFromRange(26 * 2, 33 * 2);
            heights[CreatureConstants.Wolf_Dire][CreatureConstants.Wolf_Dire] = GetMultiplierFromRange(26 * 2, 33 * 2);
            //Source: https://www.dimensions.com/element/wolverine-gulo-gulo
            heights[CreatureConstants.Wolverine][GenderConstants.Female] = GetBaseFromRange(14, 21);
            heights[CreatureConstants.Wolverine][GenderConstants.Male] = GetBaseFromRange(14, 21);
            heights[CreatureConstants.Wolverine][CreatureConstants.Wolverine] = GetMultiplierFromRange(14, 21);
            //Scaled up from Wolverine, x4 based on length
            heights[CreatureConstants.Wolverine_Dire][GenderConstants.Female] = GetBaseFromRange(14 * 4, 21 * 4);
            heights[CreatureConstants.Wolverine_Dire][GenderConstants.Male] = GetBaseFromRange(14 * 4, 21 * 4);
            heights[CreatureConstants.Wolverine_Dire][CreatureConstants.Wolverine_Dire] = GetMultiplierFromRange(14 * 4, 21 * 4);
            //Source: https://www.d20srd.org/srd/monsters/wraith.htm W:Human, DW:Ogre
            heights[CreatureConstants.Wraith][GenderConstants.Agender] = "4*12+10";
            heights[CreatureConstants.Wraith][CreatureConstants.Wraith] = "2d10";
            heights[CreatureConstants.Wraith_Dread][GenderConstants.Agender] = "96";
            heights[CreatureConstants.Wraith_Dread][CreatureConstants.Wraith_Dread] = "2d12";
            //Source: https://forgottenrealms.fandom.com/wiki/Xorn
            heights[CreatureConstants.Xorn_Minor][GenderConstants.Agender] = GetBaseFromAverage(3 * 12);
            heights[CreatureConstants.Xorn_Minor][CreatureConstants.Xorn_Minor] = GetMultiplierFromAverage(3 * 12);
            heights[CreatureConstants.Xorn_Average][GenderConstants.Agender] = GetBaseFromAverage(5 * 12);
            heights[CreatureConstants.Xorn_Average][CreatureConstants.Xorn_Average] = GetMultiplierFromAverage(5 * 12);
            heights[CreatureConstants.Xorn_Elder][GenderConstants.Agender] = GetBaseFromAverage(8 * 12);
            heights[CreatureConstants.Xorn_Elder][CreatureConstants.Xorn_Elder] = GetMultiplierFromAverage(8 * 12);

            return heights;
        }

        public static IEnumerable CreatureHeightsData => GetCreatureHeights().Select(t => new TestCaseData(t.Key, t.Value));

        private static string GetBaseFromAverage(int average) => GetBaseFromRange(average * 9 / 10, average * 11 / 10);
        private static string GetBaseFromUpTo(int upTo) => GetBaseFromRange(upTo * 9 / 11, upTo);
        private static string GetBaseFromAtLeast(int atLeast) => GetBaseFromRange(atLeast, atLeast * 11 / 9);

        private static string GetMultiplierFromAverage(int average) => GetMultiplierFromRange(average * 9 / 10, average * 11 / 10);
        private static string GetMultiplierFromUpTo(int upTo) => GetMultiplierFromRange(upTo * 9 / 11, upTo);
        private static string GetMultiplierFromAtLeast(int atLeast) => GetMultiplierFromRange(atLeast, atLeast * 11 / 9);

        private static string GetBaseFromRange(int lower, int upper)
        {
            var roll = RollHelper.GetRollWithFewestDice(lower, upper);
            var sections = roll.Split('+');
            if (sections.Length == 1)
                return "0";

            return sections[1];
        }

        private static string GetMultiplierFromRange(int lower, int upper)
        {
            var roll = RollHelper.GetRollWithFewestDice(lower, upper);
            var sections = roll.Split('+');
            return sections[0];
        }

        [TestCase(CreatureConstants.Aboleth, GenderConstants.Hermaphrodite, 20 * 12)]
        [TestCase(CreatureConstants.Achaierai, GenderConstants.Male, 15 * 12)]
        [TestCase(CreatureConstants.Achaierai, GenderConstants.Female, 15 * 12)]
        [TestCase(CreatureConstants.Ape_Dire, GenderConstants.Male, 9 * 12)]
        [TestCase(CreatureConstants.Ape_Dire, GenderConstants.Female, 6 * 12 + 9)]
        [TestCase(CreatureConstants.Androsphinx, GenderConstants.Male, 10 * 12)]
        [TestCase(CreatureConstants.Balor, GenderConstants.Agender, 12 * 12)]
        [TestCase(CreatureConstants.Baboon, GenderConstants.Female, 24)]
        [TestCase(CreatureConstants.Baboon, GenderConstants.Male, 28)]
        [TestCase(CreatureConstants.Horse_Heavy, GenderConstants.Male, 6 * 12)]
        [TestCase(CreatureConstants.Horse_Heavy, GenderConstants.Female, 6 * 12)]
        [TestCase(CreatureConstants.Lion, GenderConstants.Male, 3 * 12 + 4)]
        [TestCase(CreatureConstants.Lion, GenderConstants.Female, 2 * 12 + 10)]
        [TestCase(CreatureConstants.Locathah, GenderConstants.Male, 5 * 12)]
        [TestCase(CreatureConstants.Locathah, GenderConstants.Female, 5 * 12)]
        //https://forgottenrealms.fandom.com/wiki/Minotaur
        [TestCase(CreatureConstants.Minotaur, GenderConstants.Male, 9 * 12)]
        [TestCase(CreatureConstants.Minotaur, GenderConstants.Female, 7 * 12)]
        public void RollCalculationsAreAccurate_FromAverage(string creature, string gender, int average)
        {
            var heights = GetCreatureHeights();

            Assert.That(heights, Contains.Key(creature));
            Assert.That(heights[creature], Contains.Key(creature).And.ContainKey(gender));

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
        [TestCase(CreatureConstants.AnimatedObject_Tiny, GenderConstants.Agender, 12, 24)]
        [TestCase(CreatureConstants.AnimatedObject_Small, GenderConstants.Agender, 24, 48)]
        [TestCase(CreatureConstants.AnimatedObject_Medium, GenderConstants.Agender, 48, 96)]
        [TestCase(CreatureConstants.AnimatedObject_Large, GenderConstants.Agender, 8 * 12, 16 * 12)]
        [TestCase(CreatureConstants.AnimatedObject_Huge, GenderConstants.Agender, 16 * 12, 32 * 12)]
        [TestCase(CreatureConstants.AnimatedObject_Gargantuan, GenderConstants.Agender, 32 * 12, 64 * 12)]
        [TestCase(CreatureConstants.AnimatedObject_Colossal, GenderConstants.Agender, 64 * 12, 128 * 12)]
        [TestCase(CreatureConstants.Ape, GenderConstants.Male, 5 * 12 + 6, 6 * 12)]
        [TestCase(CreatureConstants.Ape, GenderConstants.Female, 4 * 12, 4 * 12 + 6)]
        [TestCase(CreatureConstants.Azer, GenderConstants.Agender, 4 * 12 + 5, 4 * 12 + 9)]
        [TestCase(CreatureConstants.Babau, GenderConstants.Agender, 6 * 12, 7 * 12)]
        [TestCase(CreatureConstants.Badger, GenderConstants.Male, 24, 36)]
        [TestCase(CreatureConstants.Badger, GenderConstants.Female, 24, 36)]
        [TestCase(CreatureConstants.Badger_Dire, GenderConstants.Male, 5 * 12, 7 * 12)]
        [TestCase(CreatureConstants.Badger_Dire, GenderConstants.Female, 5 * 12, 7 * 12)]
        [TestCase(CreatureConstants.Bear_Black, GenderConstants.Male, 2 * 12 + 11, 3 * 12 + 5)]
        [TestCase(CreatureConstants.Bear_Black, GenderConstants.Female, 2 * 12 + 6, 3 * 12 + 1)]
        [TestCase(CreatureConstants.Bear_Brown, GenderConstants.Male, 3 * 12 + 6, 4 * 12 + 6)]
        [TestCase(CreatureConstants.Bear_Brown, GenderConstants.Female, 3 * 12, 3 * 12 + 8)]
        [TestCase(CreatureConstants.Bear_Polar, GenderConstants.Male, 3 * 12 + 7, 5 * 12 + 3)]
        [TestCase(CreatureConstants.Bear_Polar, GenderConstants.Female, 2 * 12 + 8, 3 * 12 + 11)]
        [TestCase(CreatureConstants.Bugbear, GenderConstants.Male, 6 * 12, 8 * 12)]
        [TestCase(CreatureConstants.Bugbear, GenderConstants.Female, 6 * 12, 8 * 12)]
        [TestCase(CreatureConstants.Centaur, GenderConstants.Male, 7 * 12, 9 * 12)]
        [TestCase(CreatureConstants.Centaur, GenderConstants.Female, 7 * 12, 9 * 12)]
        [TestCase(CreatureConstants.Dog_Riding, GenderConstants.Male, 22, 30)]
        [TestCase(CreatureConstants.Dog_Riding, GenderConstants.Female, 20, 28)]
        [TestCase(CreatureConstants.Ettin, GenderConstants.Male, 13 * 12, 13 * 12 + 10)]
        [TestCase(CreatureConstants.Ettin, GenderConstants.Female, 12 * 12 + 4, 13 * 12 + 2)]
        [TestCase(CreatureConstants.Ghaele, GenderConstants.Male, 5 * 12 + 2, 7 * 12)]
        [TestCase(CreatureConstants.Ghaele, GenderConstants.Female, 4 * 12 + 7, 6 * 12 + 7)]
        [TestCase(CreatureConstants.Giant_Cloud, GenderConstants.Male, 24 * 12 + 4, 26 * 12 + 8)]
        [TestCase(CreatureConstants.Giant_Cloud, GenderConstants.Female, 22 * 12 + 8, 25 * 12)]
        [TestCase(CreatureConstants.Giant_Fire, GenderConstants.Male, 18 * 12 + 2, 19 * 12 + 8)]
        [TestCase(CreatureConstants.Giant_Fire, GenderConstants.Female, 17 * 12 + 5, 19 * 12)]
        [TestCase(CreatureConstants.Giant_Frost, GenderConstants.Male, 21 * 12 + 3, 23 * 12 + 6)]
        [TestCase(CreatureConstants.Giant_Frost, GenderConstants.Female, 20 * 12 + 1, 22 * 12 + 4)]
        [TestCase(CreatureConstants.Giant_Hill, GenderConstants.Male, 16 * 12 + 1, 17 * 12)]
        [TestCase(CreatureConstants.Giant_Hill, GenderConstants.Female, 15 * 12 + 5, 16 * 12 + 4)]
        [TestCase(CreatureConstants.Gnoll, GenderConstants.Male, 7 * 12, 7 * 12 + 6)]
        [TestCase(CreatureConstants.Gnoll, GenderConstants.Female, 7 * 12, 7 * 12 + 6)]
        [TestCase(CreatureConstants.Horse_Heavy, GenderConstants.Male, 64, 72)]
        [TestCase(CreatureConstants.Horse_Heavy, GenderConstants.Female, 64, 72)]
        [TestCase(CreatureConstants.Horse_Light, GenderConstants.Male, 57, 61)]
        [TestCase(CreatureConstants.Horse_Light, GenderConstants.Female, 57, 61)]
        [TestCase(CreatureConstants.Lizardfolk, GenderConstants.Male, 6 * 12, 7 * 12)]
        [TestCase(CreatureConstants.Lizardfolk, GenderConstants.Female, 6 * 12, 7 * 12)]
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
        public void RollCalculationsAreAccurate_FromRange(string creature, string gender, int min, int max)
        {
            var heights = GetCreatureHeights();

            Assert.That(heights, Contains.Key(creature));
            Assert.That(heights[creature], Contains.Key(creature).And.ContainKey(gender));

            var baseHeight = dice.Roll(heights[creature][gender]).AsSum();
            var multiplierMin = dice.Roll(heights[creature][creature]).AsPotentialMinimum();
            var multiplierMax = dice.Roll(heights[creature][creature]).AsPotentialMaximum();
            var theoreticalRoll = RollHelper.GetRollWithFewestDice(min, max);

            Assert.That(baseHeight + multiplierMin, Is.EqualTo(min), $"Min; Theoretical: {theoreticalRoll}");
            Assert.That(baseHeight + multiplierMax, Is.EqualTo(max), $"Max; Theoretical: {theoreticalRoll}");
        }

        [Test]
        public void IfCreatureHasNoHeight_HasLength()
        {
            var heights = GetCreatureHeights();
            var lengths = LengthsTests.GetCreatureLengths();
            var creatures = CreatureConstants.GetAll();

            foreach (var creature in creatures)
            {
                Assert.That(heights, Contains.Key(creature), "Heights");
                Assert.That(heights[creature], Contains.Key(creature), $"Heights[{creature}]");
                Assert.That(lengths, Contains.Key(creature), "Lengths");
                Assert.That(lengths[creature], Contains.Key(creature), $"Lengths[{creature}]");

                Assert.That(heights[creature][creature], Is.Not.Empty, $"Heights[{creature}][{creature}]");
                Assert.That(lengths[creature][creature], Is.Not.Empty, $"Lengths[{creature}][{creature}]");

                if (heights[creature][creature] == "0")
                    Assert.That(lengths[creature][creature], Is.Not.EqualTo("0"), creature);
            }
        }
    }
}