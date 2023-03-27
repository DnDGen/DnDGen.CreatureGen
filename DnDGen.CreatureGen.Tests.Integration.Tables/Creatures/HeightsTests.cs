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
            //Source: https://www.d20srd.org/srd/monsters/sphinx.htm
            heights[CreatureConstants.Androsphinx][GenderConstants.Male] = "0";
            heights[CreatureConstants.Androsphinx][CreatureConstants.Androsphinx] = "0";
            //Source: https://forgottenrealms.fandom.com/wiki/Astral_Deva
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
            //Source: (Male) https://www.d20srd.org/srd/monsters/ape.htm, (Female) https://nationalzoo.si.edu/animals/western-lowland-gorilla
            heights[CreatureConstants.Ape][GenderConstants.Female] = GetBaseFromRange(4 * 12, 4 * 12 + 6);
            heights[CreatureConstants.Ape][GenderConstants.Male] = GetBaseFromRange(5 * 12 + 6, 6 * 12);
            heights[CreatureConstants.Ape][CreatureConstants.Ape] = GetMultiplierFromRange(5 * 12 + 6, 6 * 12);
            //Multiplying the female up
            heights[CreatureConstants.Ape_Dire][GenderConstants.Female] = GetBaseFromAverage(6 * 12 + 9);
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
            //TODO: Double-check from here
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
            heights[CreatureConstants.Balor][GenderConstants.Agender] = GetBaseFromAverage(12 * 12);
            heights[CreatureConstants.Balor][CreatureConstants.Balor] = GetMultiplierFromAverage(12 * 12);
            heights[CreatureConstants.BarbedDevil_Hamatula][GenderConstants.Agender] = GetBaseFromAverage(7 * 12);
            heights[CreatureConstants.BarbedDevil_Hamatula][CreatureConstants.BarbedDevil_Hamatula] = GetMultiplierFromAverage(7 * 12);
            heights[CreatureConstants.Barghest][GenderConstants.Agender] = GetBaseFromAverage(6 * 12);
            heights[CreatureConstants.Barghest][CreatureConstants.Barghest] = GetMultiplierFromAverage(6 * 12);
            heights[CreatureConstants.Barghest_Greater][GenderConstants.Agender] = GetBaseFromAverage(6 * 12);
            heights[CreatureConstants.Barghest_Greater][CreatureConstants.Barghest_Greater] = GetMultiplierFromAverage(6 * 12);
            heights[CreatureConstants.Bear_Brown][GenderConstants.Female] = GetBaseFromAverage(9 * 12);
            heights[CreatureConstants.Bear_Brown][GenderConstants.Male] = GetBaseFromAverage(9 * 12);
            heights[CreatureConstants.Bear_Brown][CreatureConstants.Bear_Brown] = GetMultiplierFromAverage(9 * 12);
            heights[CreatureConstants.BeardedDevil_Barbazu][GenderConstants.Agender] = GetBaseFromAverage(6 * 12);
            heights[CreatureConstants.BeardedDevil_Barbazu][CreatureConstants.BeardedDevil_Barbazu] = GetMultiplierFromAverage(6 * 12);
            heights[CreatureConstants.Bebilith][GenderConstants.Agender] = GetBaseFromAverage(14 * 12);
            heights[CreatureConstants.Bebilith][CreatureConstants.Bebilith] = GetMultiplierFromAverage(14 * 12);
            heights[CreatureConstants.BoneDevil_Osyluth][GenderConstants.Agender] = GetBaseFromAverage(9 * 12);
            heights[CreatureConstants.BoneDevil_Osyluth][CreatureConstants.BoneDevil_Osyluth] = GetMultiplierFromAverage(9 * 12);
            heights[CreatureConstants.Bugbear][GenderConstants.Female] = "5*12";
            heights[CreatureConstants.Bugbear][GenderConstants.Male] = "5*12";
            heights[CreatureConstants.Bugbear][CreatureConstants.Bugbear] = RollHelper.GetRollWithFewestDice(5 * 12, 6 * 12, 8 * 12);
            heights[CreatureConstants.Cat][GenderConstants.Female] = GetBaseFromAverage(17); //Small Animal
            heights[CreatureConstants.Cat][GenderConstants.Male] = GetBaseFromAverage(19);
            heights[CreatureConstants.Cat][CreatureConstants.Cat] = GetMultiplierFromAverage(18);
            heights[CreatureConstants.Centaur][GenderConstants.Female] = "6*12";
            heights[CreatureConstants.Centaur][GenderConstants.Male] = "6*12";
            heights[CreatureConstants.Centaur][CreatureConstants.Centaur] = RollHelper.GetRollWithFewestDice(6 * 12, 7 * 12, 9 * 12);
            heights[CreatureConstants.ChainDevil_Kyton][GenderConstants.Agender] = GetBaseFromAverage(6 * 12);
            heights[CreatureConstants.ChainDevil_Kyton][CreatureConstants.ChainDevil_Kyton] = GetMultiplierFromAverage(6 * 12);
            heights[CreatureConstants.Criosphinx][GenderConstants.Male] = GetBaseFromAverage(120);
            heights[CreatureConstants.Criosphinx][CreatureConstants.Criosphinx] = GetMultiplierFromAverage(120);
            heights[CreatureConstants.Dretch][GenderConstants.Agender] = GetBaseFromAverage(4 * 12);
            heights[CreatureConstants.Dretch][CreatureConstants.Dretch] = GetMultiplierFromAverage(4 * 12);
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
            heights[CreatureConstants.Elemental_Air_Small][GenderConstants.Agender] = GetBaseFromAverage(4 * 12);
            heights[CreatureConstants.Elemental_Air_Small][CreatureConstants.Elemental_Air_Small] = GetMultiplierFromAverage(4 * 12);
            heights[CreatureConstants.Elemental_Air_Medium][GenderConstants.Agender] = GetBaseFromAverage(8 * 12);
            heights[CreatureConstants.Elemental_Air_Medium][CreatureConstants.Elemental_Air_Small] = GetMultiplierFromAverage(8 * 12);
            heights[CreatureConstants.Elemental_Air_Large][GenderConstants.Agender] = GetBaseFromAverage(16 * 12);
            heights[CreatureConstants.Elemental_Air_Large][CreatureConstants.Elemental_Air_Small] = GetMultiplierFromAverage(16 * 12);
            heights[CreatureConstants.Elemental_Air_Huge][GenderConstants.Agender] = GetBaseFromAverage(32 * 12);
            heights[CreatureConstants.Elemental_Air_Huge][CreatureConstants.Elemental_Air_Small] = GetMultiplierFromAverage(32 * 12);
            heights[CreatureConstants.Elemental_Air_Greater][GenderConstants.Agender] = GetBaseFromAverage(36 * 12);
            heights[CreatureConstants.Elemental_Air_Greater][CreatureConstants.Elemental_Air_Small] = GetMultiplierFromAverage(36 * 12);
            heights[CreatureConstants.Elemental_Air_Elder][GenderConstants.Agender] = GetBaseFromAverage(40 * 12);
            heights[CreatureConstants.Elemental_Air_Elder][CreatureConstants.Elemental_Air_Small] = GetMultiplierFromAverage(40 * 12);
            heights[CreatureConstants.Elemental_Earth_Small][GenderConstants.Agender] = GetBaseFromAverage(4 * 12);
            heights[CreatureConstants.Elemental_Earth_Small][CreatureConstants.Elemental_Air_Small] = GetMultiplierFromAverage(4 * 12);
            heights[CreatureConstants.Elemental_Earth_Medium][GenderConstants.Agender] = GetBaseFromAverage(8 * 12);
            heights[CreatureConstants.Elemental_Earth_Medium][CreatureConstants.Elemental_Air_Small] = GetMultiplierFromAverage(8 * 12);
            heights[CreatureConstants.Elemental_Earth_Large][GenderConstants.Agender] = GetBaseFromAverage(16 * 12);
            heights[CreatureConstants.Elemental_Earth_Large][CreatureConstants.Elemental_Air_Small] = GetMultiplierFromAverage(16 * 12);
            heights[CreatureConstants.Elemental_Earth_Huge][GenderConstants.Agender] = GetBaseFromAverage(32 * 12);
            heights[CreatureConstants.Elemental_Earth_Huge][CreatureConstants.Elemental_Air_Small] = GetMultiplierFromAverage(32 * 12);
            heights[CreatureConstants.Elemental_Earth_Greater][GenderConstants.Agender] = GetBaseFromAverage(36 * 12);
            heights[CreatureConstants.Elemental_Earth_Greater][CreatureConstants.Elemental_Air_Small] = GetMultiplierFromAverage(36 * 12);
            heights[CreatureConstants.Elemental_Earth_Elder][GenderConstants.Agender] = GetBaseFromAverage(40 * 12);
            heights[CreatureConstants.Elemental_Earth_Elder][CreatureConstants.Elemental_Air_Small] = GetMultiplierFromAverage(40 * 12);
            heights[CreatureConstants.Elemental_Fire_Small][GenderConstants.Agender] = GetBaseFromAverage(4 * 12);
            heights[CreatureConstants.Elemental_Fire_Small][CreatureConstants.Elemental_Air_Small] = GetMultiplierFromAverage(4 * 12);
            heights[CreatureConstants.Elemental_Fire_Medium][GenderConstants.Agender] = GetBaseFromAverage(8 * 12);
            heights[CreatureConstants.Elemental_Fire_Medium][CreatureConstants.Elemental_Air_Small] = GetMultiplierFromAverage(8 * 12);
            heights[CreatureConstants.Elemental_Fire_Large][GenderConstants.Agender] = GetBaseFromAverage(16 * 12);
            heights[CreatureConstants.Elemental_Fire_Large][CreatureConstants.Elemental_Air_Small] = GetMultiplierFromAverage(16 * 12);
            heights[CreatureConstants.Elemental_Fire_Huge][GenderConstants.Agender] = GetBaseFromAverage(32 * 12);
            heights[CreatureConstants.Elemental_Fire_Huge][CreatureConstants.Elemental_Air_Small] = GetMultiplierFromAverage(32 * 12);
            heights[CreatureConstants.Elemental_Fire_Greater][GenderConstants.Agender] = GetBaseFromAverage(36 * 12);
            heights[CreatureConstants.Elemental_Fire_Greater][CreatureConstants.Elemental_Air_Small] = GetMultiplierFromAverage(36 * 12);
            heights[CreatureConstants.Elemental_Fire_Elder][GenderConstants.Agender] = GetBaseFromAverage(40 * 12);
            heights[CreatureConstants.Elemental_Fire_Elder][CreatureConstants.Elemental_Air_Small] = GetMultiplierFromAverage(40 * 12);
            heights[CreatureConstants.Elemental_Water_Small][GenderConstants.Agender] = GetBaseFromAverage(4 * 12);
            heights[CreatureConstants.Elemental_Water_Small][CreatureConstants.Elemental_Air_Small] = GetMultiplierFromAverage(4 * 12);
            heights[CreatureConstants.Elemental_Water_Medium][GenderConstants.Agender] = GetBaseFromAverage(8 * 12);
            heights[CreatureConstants.Elemental_Water_Medium][CreatureConstants.Elemental_Air_Small] = GetMultiplierFromAverage(8 * 12);
            heights[CreatureConstants.Elemental_Water_Large][GenderConstants.Agender] = GetBaseFromAverage(16 * 12);
            heights[CreatureConstants.Elemental_Water_Large][CreatureConstants.Elemental_Air_Small] = GetMultiplierFromAverage(16 * 12);
            heights[CreatureConstants.Elemental_Water_Huge][GenderConstants.Agender] = GetBaseFromAverage(32 * 12);
            heights[CreatureConstants.Elemental_Water_Huge][CreatureConstants.Elemental_Air_Small] = GetMultiplierFromAverage(32 * 12);
            heights[CreatureConstants.Elemental_Water_Greater][GenderConstants.Agender] = GetBaseFromAverage(36 * 12);
            heights[CreatureConstants.Elemental_Water_Greater][CreatureConstants.Elemental_Air_Small] = GetMultiplierFromAverage(36 * 12);
            heights[CreatureConstants.Elemental_Water_Elder][GenderConstants.Agender] = GetBaseFromAverage(40 * 12);
            heights[CreatureConstants.Elemental_Water_Elder][CreatureConstants.Elemental_Air_Small] = GetMultiplierFromAverage(40 * 12);
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
            heights[CreatureConstants.Erinyes][GenderConstants.Agender] = GetBaseFromAverage(6 * 12);
            heights[CreatureConstants.Erinyes][CreatureConstants.Erinyes] = GetMultiplierFromAverage(6 * 12);
            heights[CreatureConstants.Ettin][GenderConstants.Female] = "12*12+2";
            heights[CreatureConstants.Ettin][GenderConstants.Male] = "13*12-2";
            heights[CreatureConstants.Ettin][CreatureConstants.Ettin] = "2d6";
            //Source: https://forgottenrealms.fandom.com/wiki/Cloud_giant
            heights[CreatureConstants.Giant_Cloud][GenderConstants.Female] = GetBaseFromRange(22 * 12 + 8, 25 * 12); //Huge
            heights[CreatureConstants.Giant_Cloud][GenderConstants.Male] = GetBaseFromRange(24 * 12 + 4, 26 * 12 + 8);
            heights[CreatureConstants.Giant_Cloud][CreatureConstants.Giant_Cloud] = GetMultiplierFromRange(22 * 12 + 8, 25 * 12);
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
            heights[CreatureConstants.Grig][GenderConstants.Female] = GetBaseFromAverage(18);
            heights[CreatureConstants.Grig][GenderConstants.Male] = GetBaseFromAverage(18);
            heights[CreatureConstants.Grig][CreatureConstants.Grig] = GetMultiplierFromAverage(18);
            heights[CreatureConstants.Grig_WithFiddle][GenderConstants.Female] = GetBaseFromAverage(18);
            heights[CreatureConstants.Grig_WithFiddle][GenderConstants.Male] = GetBaseFromAverage(18);
            heights[CreatureConstants.Grig_WithFiddle][CreatureConstants.Grig_WithFiddle] = GetMultiplierFromAverage(18);
            heights[CreatureConstants.Halfling_Deep][GenderConstants.Female] = "2*12+6";
            heights[CreatureConstants.Halfling_Deep][GenderConstants.Male] = "2*12+8";
            heights[CreatureConstants.Halfling_Deep][CreatureConstants.Halfling_Deep] = "2d4";
            heights[CreatureConstants.Halfling_Lightfoot][GenderConstants.Female] = "2*12+6";
            heights[CreatureConstants.Halfling_Lightfoot][GenderConstants.Male] = "2*12+8";
            heights[CreatureConstants.Halfling_Lightfoot][CreatureConstants.Halfling_Lightfoot] = "2d4";
            heights[CreatureConstants.Halfling_Tallfellow][GenderConstants.Female] = "3*12+6";
            heights[CreatureConstants.Halfling_Tallfellow][GenderConstants.Male] = "3*12+8";
            heights[CreatureConstants.Halfling_Tallfellow][CreatureConstants.Halfling_Tallfellow] = "2d4";
            heights[CreatureConstants.Hellcat_Bezekira][GenderConstants.Agender] = GetBaseFromAverage(9 * 12);
            heights[CreatureConstants.Hellcat_Bezekira][CreatureConstants.Hellcat_Bezekira] = GetMultiplierFromAverage(9 * 12);
            heights[CreatureConstants.Hezrou][GenderConstants.Agender] = GetBaseFromAverage(8 * 12);
            heights[CreatureConstants.Hezrou][CreatureConstants.Hezrou] = GetMultiplierFromAverage(8 * 12);
            heights[CreatureConstants.Hieracosphinx][GenderConstants.Male] = GetBaseFromAverage(10 * 12);
            heights[CreatureConstants.Hieracosphinx][CreatureConstants.Hieracosphinx] = GetMultiplierFromAverage(10 * 12);
            heights[CreatureConstants.Hobgoblin][GenderConstants.Female] = "4*12+0";
            heights[CreatureConstants.Hobgoblin][GenderConstants.Male] = "4*12+2";
            heights[CreatureConstants.Hobgoblin][CreatureConstants.Hobgoblin] = "2d8";
            heights[CreatureConstants.HornedDevil_Cornugon][GenderConstants.Agender] = GetBaseFromAverage(9 * 12);
            heights[CreatureConstants.HornedDevil_Cornugon][CreatureConstants.HornedDevil_Cornugon] = GetMultiplierFromAverage(9 * 12);
            heights[CreatureConstants.Horse_Heavy][GenderConstants.Female] = GetBaseFromRange(64, 72);
            heights[CreatureConstants.Horse_Heavy][GenderConstants.Male] = GetBaseFromRange(64, 72);
            heights[CreatureConstants.Horse_Heavy][CreatureConstants.Horse_Heavy] = GetMultiplierFromRange(64, 72);
            heights[CreatureConstants.Horse_Light][GenderConstants.Female] = GetBaseFromRange(57, 61);
            heights[CreatureConstants.Horse_Light][GenderConstants.Male] = GetBaseFromRange(57, 61);
            heights[CreatureConstants.Horse_Light][CreatureConstants.Horse_Light] = GetMultiplierFromRange(57, 61);
            heights[CreatureConstants.Horse_Heavy_War][GenderConstants.Female] = GetBaseFromRange(64, 72);
            heights[CreatureConstants.Horse_Heavy_War][GenderConstants.Male] = GetBaseFromRange(64, 72);
            heights[CreatureConstants.Horse_Heavy_War][CreatureConstants.Horse_Heavy_War] = GetMultiplierFromRange(64, 72);
            heights[CreatureConstants.Horse_Light_War][GenderConstants.Female] = GetBaseFromRange(57, 61);
            heights[CreatureConstants.Horse_Light_War][GenderConstants.Male] = GetBaseFromRange(57, 61);
            heights[CreatureConstants.Horse_Light_War][CreatureConstants.Horse_Light_War] = GetMultiplierFromRange(57, 61);
            heights[CreatureConstants.Human][GenderConstants.Female] = "4*12+5";
            heights[CreatureConstants.Human][GenderConstants.Male] = "4*12+10";
            heights[CreatureConstants.Human][CreatureConstants.Human] = "2d10";
            heights[CreatureConstants.IceDevil_Gelugon][GenderConstants.Agender] = GetBaseFromAverage(12 * 12);
            heights[CreatureConstants.IceDevil_Gelugon][CreatureConstants.IceDevil_Gelugon] = GetMultiplierFromAverage(12 * 12);
            heights[CreatureConstants.Imp][GenderConstants.Agender] = GetBaseFromAverage(2 * 12);
            heights[CreatureConstants.Imp][CreatureConstants.Imp] = GetMultiplierFromAverage(2 * 12);
            heights[CreatureConstants.Kobold][GenderConstants.Female] = "2*12+4";
            heights[CreatureConstants.Kobold][GenderConstants.Male] = "2*12+6";
            heights[CreatureConstants.Kobold][CreatureConstants.Kobold] = "2d4";
            heights[CreatureConstants.Lemure][GenderConstants.Agender] = GetBaseFromAverage(5 * 12);
            heights[CreatureConstants.Lemure][CreatureConstants.Lemure] = GetMultiplierFromAverage(5 * 12);
            heights[CreatureConstants.Leonal][GenderConstants.Female] = "5*12+7";
            heights[CreatureConstants.Leonal][GenderConstants.Male] = "5*12+7";
            heights[CreatureConstants.Leonal][CreatureConstants.Leonal] = "2d4";
            heights[CreatureConstants.Lizardfolk][GenderConstants.Female] = "5*12";
            heights[CreatureConstants.Lizardfolk][GenderConstants.Male] = "5*12";
            heights[CreatureConstants.Lizardfolk][CreatureConstants.Lizardfolk] = RollHelper.GetRollWithFewestDice(5 * 12, 6 * 12, 7 * 12);
            heights[CreatureConstants.Locathah][GenderConstants.Female] = GetBaseFromAverage(5 * 12);
            heights[CreatureConstants.Locathah][GenderConstants.Male] = GetBaseFromAverage(5 * 12);
            heights[CreatureConstants.Locathah][CreatureConstants.Locathah] = GetMultiplierFromAverage(5 * 12);
            heights[CreatureConstants.Marilith][GenderConstants.Female] = GetBaseFromAverage(20 * 12);
            heights[CreatureConstants.Marilith][CreatureConstants.Marilith] = GetMultiplierFromAverage(20 * 12);
            heights[CreatureConstants.Mephit_Air][GenderConstants.Agender] = GetBaseFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Air][GenderConstants.Female] = GetBaseFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Air][GenderConstants.Male] = GetBaseFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Air][CreatureConstants.Mephit_Air] = GetMultiplierFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Dust][GenderConstants.Agender] = GetBaseFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Dust][GenderConstants.Female] = GetBaseFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Dust][GenderConstants.Male] = GetBaseFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Dust][CreatureConstants.Mephit_Air] = GetMultiplierFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Earth][GenderConstants.Agender] = GetBaseFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Earth][GenderConstants.Female] = GetBaseFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Earth][GenderConstants.Male] = GetBaseFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Earth][CreatureConstants.Mephit_Air] = GetMultiplierFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Fire][GenderConstants.Agender] = GetBaseFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Fire][GenderConstants.Female] = GetBaseFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Fire][GenderConstants.Male] = GetBaseFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Fire][CreatureConstants.Mephit_Air] = GetMultiplierFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Ice][GenderConstants.Agender] = GetBaseFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Ice][GenderConstants.Female] = GetBaseFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Ice][GenderConstants.Male] = GetBaseFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Ice][CreatureConstants.Mephit_Air] = GetMultiplierFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Magma][GenderConstants.Agender] = GetBaseFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Magma][GenderConstants.Female] = GetBaseFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Magma][GenderConstants.Male] = GetBaseFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Magma][CreatureConstants.Mephit_Air] = GetMultiplierFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Ooze][GenderConstants.Agender] = GetBaseFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Ooze][GenderConstants.Female] = GetBaseFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Ooze][GenderConstants.Male] = GetBaseFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Ooze][CreatureConstants.Mephit_Air] = GetMultiplierFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Salt][GenderConstants.Agender] = GetBaseFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Salt][GenderConstants.Female] = GetBaseFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Salt][GenderConstants.Male] = GetBaseFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Salt][CreatureConstants.Mephit_Air] = GetMultiplierFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Steam][GenderConstants.Agender] = GetBaseFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Steam][GenderConstants.Female] = GetBaseFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Steam][GenderConstants.Male] = GetBaseFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Steam][CreatureConstants.Mephit_Air] = GetMultiplierFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Water][GenderConstants.Agender] = GetBaseFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Water][GenderConstants.Female] = GetBaseFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Water][GenderConstants.Male] = GetBaseFromAverage(4 * 12);
            heights[CreatureConstants.Mephit_Water][CreatureConstants.Mephit_Air] = GetMultiplierFromAverage(4 * 12);
            heights[CreatureConstants.Merfolk][GenderConstants.Female] = "5*12+8";
            heights[CreatureConstants.Merfolk][GenderConstants.Male] = "5*12+10";
            heights[CreatureConstants.Merfolk][CreatureConstants.Merfolk] = "2d10";
            heights[CreatureConstants.Minotaur][GenderConstants.Female] = GetBaseFromAverage(7 * 12);
            heights[CreatureConstants.Minotaur][GenderConstants.Male] = GetBaseFromAverage(9 * 12);
            heights[CreatureConstants.Minotaur][CreatureConstants.Minotaur] = GetMultiplierFromAverage(8 * 12);
            heights[CreatureConstants.Nalfeshnee][GenderConstants.Agender] = "9*12";
            heights[CreatureConstants.Nalfeshnee][CreatureConstants.Nalfeshnee] = RollHelper.GetRollWithFewestDice(9 * 12, 10 * 12, 20 * 12);
            heights[CreatureConstants.Ogre][GenderConstants.Female] = "110";
            heights[CreatureConstants.Ogre][GenderConstants.Male] = "120";
            heights[CreatureConstants.Ogre][CreatureConstants.Ogre] = "1d10";
            heights[CreatureConstants.Ogre_Merrow][GenderConstants.Female] = "110";
            heights[CreatureConstants.Ogre_Merrow][GenderConstants.Male] = "120";
            heights[CreatureConstants.Ogre_Merrow][CreatureConstants.Ogre] = "1d10";
            heights[CreatureConstants.OgreMage][GenderConstants.Female] = GetBaseFromAverage(10 * 12);
            heights[CreatureConstants.OgreMage][GenderConstants.Male] = GetBaseFromAverage(10 * 12);
            heights[CreatureConstants.OgreMage][CreatureConstants.OgreMage] = GetMultiplierFromAverage(10 * 12);
            heights[CreatureConstants.Orc][GenderConstants.Female] = "5*12+1";
            heights[CreatureConstants.Orc][GenderConstants.Male] = "4*12+9";
            heights[CreatureConstants.Orc][CreatureConstants.Orc] = "2d12";
            heights[CreatureConstants.Orc_Half][GenderConstants.Female] = "4*12+5";
            heights[CreatureConstants.Orc_Half][GenderConstants.Male] = "4*12+10";
            heights[CreatureConstants.Orc_Half][CreatureConstants.Orc_Half] = "2d12";
            heights[CreatureConstants.PitFiend][GenderConstants.Agender] = GetBaseFromAverage(12 * 12);
            heights[CreatureConstants.PitFiend][CreatureConstants.PitFiend] = GetMultiplierFromAverage(12 * 12);
            heights[CreatureConstants.Pixie][GenderConstants.Female] = "10";
            heights[CreatureConstants.Pixie][GenderConstants.Male] = "10";
            heights[CreatureConstants.Pixie][CreatureConstants.Pixie] = RollHelper.GetRollWithFewestDice(10, 12, 30);
            heights[CreatureConstants.Pixie_WithIrresistibleDance][GenderConstants.Female] = "10";
            heights[CreatureConstants.Pixie_WithIrresistibleDance][GenderConstants.Male] = "10";
            heights[CreatureConstants.Pixie_WithIrresistibleDance][CreatureConstants.Pixie_WithIrresistibleDance] = RollHelper.GetRollWithFewestDice(10, 12, 30);
            heights[CreatureConstants.Quasit][GenderConstants.Agender] = "10";
            heights[CreatureConstants.Quasit][CreatureConstants.Quasit] = RollHelper.GetRollWithFewestDice(10, 1 * 12, 2 * 12);
            heights[CreatureConstants.Retriever][GenderConstants.Agender] = GetBaseFromAverage(14 * 12);
            heights[CreatureConstants.Retriever][CreatureConstants.Retriever] = GetMultiplierFromAverage(14 * 12);
            heights[CreatureConstants.Salamander_Flamebrother][GenderConstants.Agender] = "20";
            heights[CreatureConstants.Salamander_Flamebrother][CreatureConstants.Salamander_Flamebrother] = RollHelper.GetRollWithFewestDice(20, 24, 48);
            heights[CreatureConstants.Salamander_Average][GenderConstants.Agender] = "40";
            heights[CreatureConstants.Salamander_Average][CreatureConstants.Salamander_Average] = RollHelper.GetRollWithFewestDice(40, 48, 8 * 12);
            heights[CreatureConstants.Salamander_Noble][GenderConstants.Agender] = "7*12";
            heights[CreatureConstants.Salamander_Noble][CreatureConstants.Salamander_Noble] = RollHelper.GetRollWithFewestDice(20, 8 * 12, 16 * 12);
            heights[CreatureConstants.SeaHag][GenderConstants.Female] = "4*12+5";
            heights[CreatureConstants.SeaHag][CreatureConstants.GreenHag] = "2d10";
            heights[CreatureConstants.Succubus][GenderConstants.Female] = GetBaseFromAverage(6 * 12);
            heights[CreatureConstants.Succubus][GenderConstants.Male] = GetBaseFromAverage(6 * 12);
            heights[CreatureConstants.Succubus][CreatureConstants.Succubus] = GetMultiplierFromAverage(6 * 12);
            heights[CreatureConstants.Tiefling][GenderConstants.Female] = "4*12+5";
            heights[CreatureConstants.Tiefling][GenderConstants.Male] = "4*12+10";
            heights[CreatureConstants.Tiefling][CreatureConstants.Tiefling] = "2d10";
            heights[CreatureConstants.Tojanida_Juvenile][GenderConstants.Female] = GetBaseFromAverage(3 * 12);
            heights[CreatureConstants.Tojanida_Juvenile][GenderConstants.Male] = GetBaseFromAverage(3 * 12);
            heights[CreatureConstants.Tojanida_Juvenile][CreatureConstants.Tojanida_Juvenile] = GetMultiplierFromAverage(3 * 12);
            heights[CreatureConstants.Tojanida_Adult][GenderConstants.Female] = GetBaseFromAverage(6 * 12);
            heights[CreatureConstants.Tojanida_Adult][GenderConstants.Male] = GetBaseFromAverage(6 * 12);
            heights[CreatureConstants.Tojanida_Adult][CreatureConstants.Tojanida_Adult] = GetMultiplierFromAverage(6 * 12);
            heights[CreatureConstants.Tojanida_Elder][GenderConstants.Female] = GetBaseFromAverage(9 * 12);
            heights[CreatureConstants.Tojanida_Elder][GenderConstants.Male] = GetBaseFromAverage(9 * 12);
            heights[CreatureConstants.Tojanida_Elder][CreatureConstants.Tojanida_Elder] = GetMultiplierFromAverage(9 * 12);
            heights[CreatureConstants.Vrock][GenderConstants.Agender] = GetBaseFromAverage(8 * 12);
            heights[CreatureConstants.Vrock][CreatureConstants.Vrock] = GetMultiplierFromAverage(8 * 12);
            heights[CreatureConstants.Whale_Baleen][GenderConstants.Female] = "30*12";
            heights[CreatureConstants.Whale_Baleen][GenderConstants.Male] = "30*12";
            heights[CreatureConstants.Whale_Baleen][CreatureConstants.Whale_Baleen] = RollHelper.GetRollWithFewestDice(30 * 12, 30 * 12, 60 * 12);
            heights[CreatureConstants.Whale_Cachalot][GenderConstants.Female] = GetBaseFromAverage(60 * 12);
            heights[CreatureConstants.Whale_Cachalot][GenderConstants.Male] = GetBaseFromAverage(60 * 12);
            heights[CreatureConstants.Whale_Cachalot][CreatureConstants.Whale_Cachalot] = GetMultiplierFromAverage(60 * 12);
            heights[CreatureConstants.Whale_Orca][GenderConstants.Female] = GetBaseFromAverage(30 * 12);
            heights[CreatureConstants.Whale_Orca][GenderConstants.Male] = GetBaseFromAverage(30 * 12);
            heights[CreatureConstants.Whale_Orca][CreatureConstants.Whale_Cachalot] = GetMultiplierFromAverage(30 * 12);
            heights[CreatureConstants.Wolf][GenderConstants.Female] = "39"; //Medium Animal
            heights[CreatureConstants.Wolf][GenderConstants.Male] = "39";
            heights[CreatureConstants.Wolf][CreatureConstants.Wolf] = "2d12";
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

        private static string GetMultiplierFromAverage(int average) => GetMultiplierFromRange(average * 9 / 10, average * 11 / 10);

        private static string GetBaseFromRange(int lower, int upper)
        {
            var roll = RollHelper.GetRollWithFewestDice(lower, upper);
            var sections = roll.Split('+');
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
        [TestCase(CreatureConstants.Locathah, GenderConstants.Male, 5 * 12)]
        [TestCase(CreatureConstants.Locathah, GenderConstants.Female, 5 * 12)]
        [TestCase(CreatureConstants.Minotaur, GenderConstants.Male, 9 * 12)]
        [TestCase(CreatureConstants.Minotaur, GenderConstants.Female, 7 * 12)]
        public void RollCalculationsAreAccurate_FromAverage(string creature, string gender, int average)
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
        public void RollCalculationsAreAccurate_FromRange(string creature, string gender, int min, int max)
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

        [Test]
        public void IfCreatureHasNoHeight_HasLength()
        {
            var heights = GetCreatureHeights();
            var lengths = LengthsTests.GetCreatureLengths();
            var creatures = CreatureConstants.GetAll();

            foreach (var creature in creatures)
            {
                Assert.That(heights[creature][creature], Is.Not.Empty);
                Assert.That(lengths[creature][creature], Is.Not.Empty);

                if (heights[creature][creature] == "0")
                    Assert.That(lengths[creature][creature], Is.Not.EqualTo("0"), creature);
            }
        }
    }
}