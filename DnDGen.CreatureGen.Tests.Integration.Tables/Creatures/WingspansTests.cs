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
    public class WingspansTests : TypesAndAmountsTests
    {
        private ICollectionSelector collectionSelector;
        private Dice dice;

        protected override string tableName => TableNameConstants.TypeAndAmount.Wingspans;

        [SetUp]
        public void Setup()
        {
            collectionSelector = GetNewInstanceOf<ICollectionSelector>();
            dice = GetNewInstanceOf<Dice>();
        }

        [Test]
        public void WingspansNames()
        {
            var creatures = CreatureConstants.GetAll();
            AssertCollectionNames(creatures);
        }

        [TestCaseSource(nameof(CreatureWingspansData))]
        public void CreatureWingspans(string name, Dictionary<string, string> typesAndRolls)
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

        public static Dictionary<string, Dictionary<string, string>> GetCreatureWingspans()
        {
            var creatures = CreatureConstants.GetAll();
            var wingspans = new Dictionary<string, Dictionary<string, string>>();

            foreach (var creature in creatures)
            {
                wingspans[creature] = new Dictionary<string, string>();
            }

            //INFO: When wingspan is not readily available, assuming it is equal to height/length
            wingspans[CreatureConstants.Aasimar][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Aasimar][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Aasimar][CreatureConstants.Aasimar] = "0";
            wingspans[CreatureConstants.Aboleth][GenderConstants.Hermaphrodite] = "0";
            wingspans[CreatureConstants.Aboleth][CreatureConstants.Aboleth] = "0";
            wingspans[CreatureConstants.Achaierai][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Achaierai][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Achaierai][CreatureConstants.Achaierai] = "0";
            wingspans[CreatureConstants.Allip][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Allip][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Allip][CreatureConstants.Allip] = "0";
            wingspans[CreatureConstants.Androsphinx][GenderConstants.Male] = GetBaseFromAverage(10 * 12);
            wingspans[CreatureConstants.Androsphinx][CreatureConstants.Androsphinx] = GetMultiplierFromAverage(10 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Astral_deva
            wingspans[CreatureConstants.Angel_AstralDeva][GenderConstants.Female] = GetBaseFromRange(7 * 12, 7 * 12 + 6);
            wingspans[CreatureConstants.Angel_AstralDeva][GenderConstants.Male] = GetBaseFromRange(7 * 12, 7 * 12 + 6);
            wingspans[CreatureConstants.Angel_AstralDeva][CreatureConstants.Angel_AstralDeva] = GetMultiplierFromRange(7 * 12, 7 * 12 + 6);
            //Source: https://forgottenrealms.fandom.com/wiki/Planetar
            wingspans[CreatureConstants.Angel_Planetar][GenderConstants.Female] = GetBaseFromRange(8 * 12, 9 * 12);
            wingspans[CreatureConstants.Angel_Planetar][GenderConstants.Male] = GetBaseFromRange(8 * 12, 9 * 12);
            wingspans[CreatureConstants.Angel_Planetar][CreatureConstants.Angel_Planetar] = GetMultiplierFromRange(8 * 12, 9 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Solar
            wingspans[CreatureConstants.Angel_Solar][GenderConstants.Female] = GetBaseFromRange(9 * 12, 10 * 12);
            wingspans[CreatureConstants.Angel_Solar][GenderConstants.Male] = GetBaseFromRange(9 * 12, 10 * 12);
            wingspans[CreatureConstants.Angel_Solar][CreatureConstants.Angel_Solar] = GetMultiplierFromRange(9 * 12, 10 * 12);
            //Source: https://www.d20srd.org/srd/combat/movementPositionAndDistance.htm
            wingspans[CreatureConstants.AnimatedObject_Colossal][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.AnimatedObject_Colossal][CreatureConstants.AnimatedObject_Colossal] = "0";
            wingspans[CreatureConstants.AnimatedObject_Colossal_Flexible][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.AnimatedObject_Colossal_Flexible][CreatureConstants.AnimatedObject_Colossal_Flexible] = "0";
            wingspans[CreatureConstants.AnimatedObject_Colossal_MultipleLegs][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.AnimatedObject_Colossal_MultipleLegs][CreatureConstants.AnimatedObject_Colossal_MultipleLegs] = "0";
            wingspans[CreatureConstants.AnimatedObject_Colossal_MultipleLegs_Wooden][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.AnimatedObject_Colossal_MultipleLegs_Wooden][CreatureConstants.AnimatedObject_Colossal_MultipleLegs_Wooden] = "0";
            wingspans[CreatureConstants.AnimatedObject_Colossal_Sheetlike][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.AnimatedObject_Colossal_Sheetlike][CreatureConstants.AnimatedObject_Colossal_Sheetlike] = "0";
            wingspans[CreatureConstants.AnimatedObject_Colossal_TwoLegs][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.AnimatedObject_Colossal_TwoLegs][CreatureConstants.AnimatedObject_Colossal_TwoLegs] = "0";
            wingspans[CreatureConstants.AnimatedObject_Colossal_TwoLegs_Wooden][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.AnimatedObject_Colossal_TwoLegs_Wooden][CreatureConstants.AnimatedObject_Colossal_TwoLegs_Wooden] = "0";
            wingspans[CreatureConstants.AnimatedObject_Colossal_Wheels_Wooden][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.AnimatedObject_Colossal_Wheels_Wooden][CreatureConstants.AnimatedObject_Colossal_Wheels_Wooden] = "0";
            wingspans[CreatureConstants.AnimatedObject_Colossal_Wooden][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.AnimatedObject_Colossal_Wooden][CreatureConstants.AnimatedObject_Colossal_Wooden] = "0";
            wingspans[CreatureConstants.AnimatedObject_Gargantuan][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.AnimatedObject_Gargantuan][CreatureConstants.AnimatedObject_Gargantuan] = "0";
            wingspans[CreatureConstants.AnimatedObject_Gargantuan_Flexible][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.AnimatedObject_Gargantuan_Flexible][CreatureConstants.AnimatedObject_Gargantuan_Flexible] = "0";
            wingspans[CreatureConstants.AnimatedObject_Gargantuan_MultipleLegs][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.AnimatedObject_Gargantuan_MultipleLegs][CreatureConstants.AnimatedObject_Gargantuan_MultipleLegs] = "0";
            wingspans[CreatureConstants.AnimatedObject_Gargantuan_MultipleLegs_Wooden][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.AnimatedObject_Gargantuan_MultipleLegs_Wooden][CreatureConstants.AnimatedObject_Gargantuan_MultipleLegs_Wooden] = "0";
            wingspans[CreatureConstants.AnimatedObject_Gargantuan_Sheetlike][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.AnimatedObject_Gargantuan_Sheetlike][CreatureConstants.AnimatedObject_Gargantuan_Sheetlike] = "0";
            wingspans[CreatureConstants.AnimatedObject_Gargantuan_TwoLegs][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.AnimatedObject_Gargantuan_TwoLegs][CreatureConstants.AnimatedObject_Gargantuan_TwoLegs] = "0";
            wingspans[CreatureConstants.AnimatedObject_Gargantuan_TwoLegs_Wooden][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.AnimatedObject_Gargantuan_TwoLegs_Wooden][CreatureConstants.AnimatedObject_Gargantuan_TwoLegs_Wooden] = "0";
            wingspans[CreatureConstants.AnimatedObject_Gargantuan_Wheels_Wooden][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.AnimatedObject_Gargantuan_Wheels_Wooden][CreatureConstants.AnimatedObject_Gargantuan_Wheels_Wooden] = "0";
            wingspans[CreatureConstants.AnimatedObject_Gargantuan_Wooden][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.AnimatedObject_Gargantuan_Wooden][CreatureConstants.AnimatedObject_Gargantuan_Wooden] = "0";
            wingspans[CreatureConstants.AnimatedObject_Huge][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.AnimatedObject_Huge][CreatureConstants.AnimatedObject_Huge] = "0";
            wingspans[CreatureConstants.AnimatedObject_Huge_Flexible][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.AnimatedObject_Huge_Flexible][CreatureConstants.AnimatedObject_Huge_Flexible] = "0";
            wingspans[CreatureConstants.AnimatedObject_Huge_MultipleLegs][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.AnimatedObject_Huge_MultipleLegs][CreatureConstants.AnimatedObject_Huge_MultipleLegs] = "0";
            wingspans[CreatureConstants.AnimatedObject_Huge_MultipleLegs_Wooden][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.AnimatedObject_Huge_MultipleLegs_Wooden][CreatureConstants.AnimatedObject_Huge_MultipleLegs_Wooden] = "0";
            wingspans[CreatureConstants.AnimatedObject_Huge_Sheetlike][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.AnimatedObject_Huge_Sheetlike][CreatureConstants.AnimatedObject_Huge_Sheetlike] = "0";
            wingspans[CreatureConstants.AnimatedObject_Huge_TwoLegs][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.AnimatedObject_Huge_TwoLegs][CreatureConstants.AnimatedObject_Huge_TwoLegs] = "0";
            wingspans[CreatureConstants.AnimatedObject_Huge_TwoLegs_Wooden][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.AnimatedObject_Huge_TwoLegs_Wooden][CreatureConstants.AnimatedObject_Huge_TwoLegs_Wooden] = "0";
            wingspans[CreatureConstants.AnimatedObject_Huge_Wheels_Wooden][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.AnimatedObject_Huge_Wheels_Wooden][CreatureConstants.AnimatedObject_Huge_Wheels_Wooden] = "0";
            wingspans[CreatureConstants.AnimatedObject_Huge_Wooden][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.AnimatedObject_Huge_Wooden][CreatureConstants.AnimatedObject_Huge_Wooden] = "0";
            wingspans[CreatureConstants.AnimatedObject_Large][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.AnimatedObject_Large][CreatureConstants.AnimatedObject_Large] = "0";
            wingspans[CreatureConstants.AnimatedObject_Large_Flexible][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.AnimatedObject_Large_Flexible][CreatureConstants.AnimatedObject_Large_Flexible] = "0";
            wingspans[CreatureConstants.AnimatedObject_Large_MultipleLegs][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.AnimatedObject_Large_MultipleLegs][CreatureConstants.AnimatedObject_Large_MultipleLegs] = "0";
            wingspans[CreatureConstants.AnimatedObject_Large_MultipleLegs_Wooden][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.AnimatedObject_Large_MultipleLegs_Wooden][CreatureConstants.AnimatedObject_Large_MultipleLegs_Wooden] = "0";
            wingspans[CreatureConstants.AnimatedObject_Large_Sheetlike][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.AnimatedObject_Large_Sheetlike][CreatureConstants.AnimatedObject_Large_Sheetlike] = "0";
            wingspans[CreatureConstants.AnimatedObject_Large_TwoLegs][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.AnimatedObject_Large_TwoLegs][CreatureConstants.AnimatedObject_Large_TwoLegs] = "0";
            wingspans[CreatureConstants.AnimatedObject_Large_TwoLegs_Wooden][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.AnimatedObject_Large_TwoLegs_Wooden][CreatureConstants.AnimatedObject_Large_TwoLegs_Wooden] = "0";
            wingspans[CreatureConstants.AnimatedObject_Large_Wheels_Wooden][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.AnimatedObject_Large_Wheels_Wooden][CreatureConstants.AnimatedObject_Large_Wheels_Wooden] = "0";
            wingspans[CreatureConstants.AnimatedObject_Large_Wooden][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.AnimatedObject_Large_Wooden][CreatureConstants.AnimatedObject_Large_Wooden] = "0";
            wingspans[CreatureConstants.AnimatedObject_Medium][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.AnimatedObject_Medium][CreatureConstants.AnimatedObject_Medium] = "0";
            wingspans[CreatureConstants.AnimatedObject_Medium_Flexible][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.AnimatedObject_Medium_Flexible][CreatureConstants.AnimatedObject_Medium_Flexible] = "0";
            wingspans[CreatureConstants.AnimatedObject_Medium_MultipleLegs][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.AnimatedObject_Medium_MultipleLegs][CreatureConstants.AnimatedObject_Medium_MultipleLegs] = "0";
            wingspans[CreatureConstants.AnimatedObject_Medium_MultipleLegs_Wooden][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.AnimatedObject_Medium_MultipleLegs_Wooden][CreatureConstants.AnimatedObject_Medium_MultipleLegs_Wooden] = "0";
            wingspans[CreatureConstants.AnimatedObject_Medium_Sheetlike][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.AnimatedObject_Medium_Sheetlike][CreatureConstants.AnimatedObject_Medium_Sheetlike] = "0";
            wingspans[CreatureConstants.AnimatedObject_Medium_TwoLegs][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.AnimatedObject_Medium_TwoLegs][CreatureConstants.AnimatedObject_Medium_TwoLegs] = "0";
            wingspans[CreatureConstants.AnimatedObject_Medium_TwoLegs_Wooden][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.AnimatedObject_Medium_TwoLegs_Wooden][CreatureConstants.AnimatedObject_Medium_TwoLegs_Wooden] = "0";
            wingspans[CreatureConstants.AnimatedObject_Medium_Wheels_Wooden][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.AnimatedObject_Medium_Wheels_Wooden][CreatureConstants.AnimatedObject_Medium_Wheels_Wooden] = "0";
            wingspans[CreatureConstants.AnimatedObject_Medium_Wooden][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.AnimatedObject_Medium_Wooden][CreatureConstants.AnimatedObject_Medium_Wooden] = "0";
            wingspans[CreatureConstants.AnimatedObject_Small][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.AnimatedObject_Small][CreatureConstants.AnimatedObject_Small] = "0";
            wingspans[CreatureConstants.AnimatedObject_Small_Flexible][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.AnimatedObject_Small_Flexible][CreatureConstants.AnimatedObject_Small_Flexible] = "0";
            wingspans[CreatureConstants.AnimatedObject_Small_MultipleLegs][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.AnimatedObject_Small_MultipleLegs][CreatureConstants.AnimatedObject_Small_MultipleLegs] = "0";
            wingspans[CreatureConstants.AnimatedObject_Small_MultipleLegs_Wooden][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.AnimatedObject_Small_MultipleLegs_Wooden][CreatureConstants.AnimatedObject_Small_MultipleLegs_Wooden] = "0";
            wingspans[CreatureConstants.AnimatedObject_Small_Sheetlike][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.AnimatedObject_Small_Sheetlike][CreatureConstants.AnimatedObject_Small_Sheetlike] = "0";
            wingspans[CreatureConstants.AnimatedObject_Small_TwoLegs][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.AnimatedObject_Small_TwoLegs][CreatureConstants.AnimatedObject_Small_TwoLegs] = "0";
            wingspans[CreatureConstants.AnimatedObject_Small_TwoLegs_Wooden][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.AnimatedObject_Small_TwoLegs_Wooden][CreatureConstants.AnimatedObject_Small_TwoLegs_Wooden] = "0";
            wingspans[CreatureConstants.AnimatedObject_Small_Wheels_Wooden][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.AnimatedObject_Small_Wheels_Wooden][CreatureConstants.AnimatedObject_Small_Wheels_Wooden] = "0";
            wingspans[CreatureConstants.AnimatedObject_Small_Wooden][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.AnimatedObject_Small_Wooden][CreatureConstants.AnimatedObject_Small_Wooden] = "0";
            wingspans[CreatureConstants.AnimatedObject_Tiny][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.AnimatedObject_Tiny][CreatureConstants.AnimatedObject_Tiny] = "0";
            wingspans[CreatureConstants.AnimatedObject_Tiny_Flexible][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.AnimatedObject_Tiny_Flexible][CreatureConstants.AnimatedObject_Tiny_Flexible] = "0";
            wingspans[CreatureConstants.AnimatedObject_Tiny_MultipleLegs][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.AnimatedObject_Tiny_MultipleLegs][CreatureConstants.AnimatedObject_Tiny_MultipleLegs] = "0";
            wingspans[CreatureConstants.AnimatedObject_Tiny_MultipleLegs_Wooden][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.AnimatedObject_Tiny_MultipleLegs_Wooden][CreatureConstants.AnimatedObject_Tiny_MultipleLegs_Wooden] = "0";
            wingspans[CreatureConstants.AnimatedObject_Tiny_Sheetlike][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.AnimatedObject_Tiny_Sheetlike][CreatureConstants.AnimatedObject_Tiny_Sheetlike] = "0";
            wingspans[CreatureConstants.AnimatedObject_Tiny_TwoLegs][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.AnimatedObject_Tiny_TwoLegs][CreatureConstants.AnimatedObject_Tiny_TwoLegs] = "0";
            wingspans[CreatureConstants.AnimatedObject_Tiny_TwoLegs_Wooden][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.AnimatedObject_Tiny_TwoLegs_Wooden][CreatureConstants.AnimatedObject_Tiny_TwoLegs_Wooden] = "0";
            wingspans[CreatureConstants.AnimatedObject_Tiny_Wheels_Wooden][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.AnimatedObject_Tiny_Wheels_Wooden][CreatureConstants.AnimatedObject_Tiny_Wheels_Wooden] = "0";
            wingspans[CreatureConstants.AnimatedObject_Tiny_Wooden][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.AnimatedObject_Tiny_Wooden][CreatureConstants.AnimatedObject_Tiny_Wooden] = "0";
            //Source: https://www.d20srd.org/srd/monsters/ankheg.htm
            wingspans[CreatureConstants.Ankheg][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Ankheg][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Ankheg][CreatureConstants.Ankheg] = "0";
            wingspans[CreatureConstants.Ant_Giant_Worker][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Ant_Giant_Worker][CreatureConstants.Ant_Giant_Worker] = "0";
            wingspans[CreatureConstants.Ant_Giant_Soldier][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Ant_Giant_Soldier][CreatureConstants.Ant_Giant_Soldier] = "0";
            wingspans[CreatureConstants.Ant_Giant_Queen][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Ant_Giant_Queen][CreatureConstants.Ant_Giant_Queen] = "0";
            //Source: https://www.d20srd.org/srd/monsters/hag.htm#annis
            wingspans[CreatureConstants.Annis][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Annis][CreatureConstants.Annis] = "0";
            wingspans[CreatureConstants.Ape][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Ape][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Ape][CreatureConstants.Ape] = "0";
            wingspans[CreatureConstants.Ape_Dire][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Ape_Dire][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Ape_Dire][CreatureConstants.Ape_Dire] = "0";
            //INFO: Based on Half-Elf, since could be Human, Half-Elf, or Drow
            wingspans[CreatureConstants.Aranea][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Aranea][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Aranea][CreatureConstants.Aranea] = "0";
            //Source: https://www.d20srd.org/srd/monsters/arrowhawk.htm
            wingspans[CreatureConstants.Arrowhawk_Juvenile][GenderConstants.Female] = GetBaseFromAverage(7 * 12);
            wingspans[CreatureConstants.Arrowhawk_Juvenile][GenderConstants.Male] = GetBaseFromAverage(7 * 12);
            wingspans[CreatureConstants.Arrowhawk_Juvenile][CreatureConstants.Arrowhawk_Juvenile] = GetMultiplierFromAverage(7 * 12);
            wingspans[CreatureConstants.Arrowhawk_Adult][GenderConstants.Female] = GetBaseFromAverage(15 * 12);
            wingspans[CreatureConstants.Arrowhawk_Adult][GenderConstants.Male] = GetBaseFromAverage(15 * 12);
            wingspans[CreatureConstants.Arrowhawk_Adult][CreatureConstants.Arrowhawk_Adult] = GetMultiplierFromAverage(15 * 12);
            wingspans[CreatureConstants.Arrowhawk_Elder][GenderConstants.Female] = GetBaseFromAverage(30 * 12);
            wingspans[CreatureConstants.Arrowhawk_Elder][GenderConstants.Male] = GetBaseFromAverage(30 * 12);
            wingspans[CreatureConstants.Arrowhawk_Elder][CreatureConstants.Arrowhawk_Elder] = GetMultiplierFromAverage(30 * 12);
            wingspans[CreatureConstants.AssassinVine][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.AssassinVine][CreatureConstants.AssassinVine] = "0";
            //Source: https://www.d20srd.org/srd/monsters/athach.htm
            wingspans[CreatureConstants.Athach][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Athach][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Athach][CreatureConstants.Athach] = "0";
            //Source: https://forgottenrealms.fandom.com/wiki/Avoral
            wingspans[CreatureConstants.Avoral][GenderConstants.Female] = GetBaseFromAverage(20 * 12);
            wingspans[CreatureConstants.Avoral][GenderConstants.Male] = GetBaseFromAverage(20 * 12);
            wingspans[CreatureConstants.Avoral][GenderConstants.Agender] = GetBaseFromAverage(20 * 12);
            wingspans[CreatureConstants.Avoral][CreatureConstants.Avoral] = GetMultiplierFromAverage(20 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Azer
            wingspans[CreatureConstants.Azer][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Azer][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Azer][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.Azer][CreatureConstants.Azer] = "0";
            wingspans[CreatureConstants.Babau][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.Babau][CreatureConstants.Babau] = "0";
            wingspans[CreatureConstants.Baboon][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Baboon][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Baboon][CreatureConstants.Baboon] = "0";
            wingspans[CreatureConstants.Badger][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Badger][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Badger][CreatureConstants.Badger] = "0";
            wingspans[CreatureConstants.Badger_Dire][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Badger_Dire][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Badger_Dire][CreatureConstants.Badger_Dire] = "0";
            wingspans[CreatureConstants.Balor][GenderConstants.Agender] = GetBaseFromAverage(12 * 12);
            wingspans[CreatureConstants.Balor][CreatureConstants.Balor] = GetMultiplierFromAverage(12 * 12);
            wingspans[CreatureConstants.BarbedDevil_Hamatula][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.BarbedDevil_Hamatula][CreatureConstants.BarbedDevil_Hamatula] = "0";
            wingspans[CreatureConstants.Barghest][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.Barghest][CreatureConstants.Barghest] = "0";
            wingspans[CreatureConstants.Barghest_Greater][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.Barghest_Greater][CreatureConstants.Barghest_Greater] = "0";
            wingspans[CreatureConstants.Basilisk][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Basilisk][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Basilisk][CreatureConstants.Basilisk] = "0";
            wingspans[CreatureConstants.Basilisk_Greater][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Basilisk_Greater][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Basilisk_Greater][CreatureConstants.Basilisk_Greater] = "0";
            //Source: https://www.dimensions.com/element/little-brown-bat-myotis-lucifugus
            wingspans[CreatureConstants.Bat][GenderConstants.Female] = GetBaseFromRange(9, 11);
            wingspans[CreatureConstants.Bat][GenderConstants.Male] = GetBaseFromRange(9, 11);
            wingspans[CreatureConstants.Bat][CreatureConstants.Bat] = GetMultiplierFromRange(9, 11);
            wingspans[CreatureConstants.Bat_Dire][GenderConstants.Female] = GetBaseFromAverage(15 * 12);
            wingspans[CreatureConstants.Bat_Dire][GenderConstants.Male] = GetBaseFromAverage(15 * 12);
            wingspans[CreatureConstants.Bat_Dire][CreatureConstants.Bat_Dire] = GetMultiplierFromAverage(15 * 12);
            wingspans[CreatureConstants.Bat_Swarm][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.Bat_Swarm][CreatureConstants.Bat_Swarm] = "0";
            wingspans[CreatureConstants.Bear_Black][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Bear_Black][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Bear_Black][CreatureConstants.Bear_Black] = "0";
            wingspans[CreatureConstants.Bear_Brown][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Bear_Brown][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Bear_Brown][CreatureConstants.Bear_Brown] = "0";
            wingspans[CreatureConstants.Bear_Dire][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Bear_Dire][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Bear_Dire][CreatureConstants.Bear_Dire] = "0";
            wingspans[CreatureConstants.Bear_Polar][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Bear_Polar][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Bear_Polar][CreatureConstants.Bear_Polar] = "0";
            wingspans[CreatureConstants.BeardedDevil_Barbazu][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.BeardedDevil_Barbazu][CreatureConstants.BeardedDevil_Barbazu] = "0";
            wingspans[CreatureConstants.Bebilith][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.Bebilith][CreatureConstants.Bebilith] = "0";
            //Source: https://www.d20srd.org/srd/monsters/giantBee.htm
            //https://www.dimensions.com/element/western-honey-bee-apis-mellifera scale up, [.71,.79]*5*12/[.39,.59] = [109,80] = [80,109]
            wingspans[CreatureConstants.Bee_Giant][GenderConstants.Male] = GetBaseFromRange(80, 109);
            wingspans[CreatureConstants.Bee_Giant][CreatureConstants.Bee_Giant] = GetMultiplierFromRange(80, 109);
            //Source: https://forgottenrealms.fandom.com/wiki/Behir
            wingspans[CreatureConstants.Behir][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Behir][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Behir][CreatureConstants.Behir] = "0";
            wingspans[CreatureConstants.Beholder][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.Beholder][CreatureConstants.Beholder] = "0";
            wingspans[CreatureConstants.Beholder_Gauth][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.Beholder_Gauth][CreatureConstants.Beholder_Gauth] = "0";
            //Source: https://forgottenrealms.fandom.com/wiki/Belker - Large wings, so double height
            wingspans[CreatureConstants.Belker][GenderConstants.Agender] = GetBaseFromRange(14 * 12, 18 * 12);
            wingspans[CreatureConstants.Belker][CreatureConstants.Belker] = GetMultiplierFromRange(14 * 12, 18 * 12);
            wingspans[CreatureConstants.Bison][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Bison][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Bison][CreatureConstants.Bison] = "0";
            wingspans[CreatureConstants.BlackPudding][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.BlackPudding][CreatureConstants.BlackPudding] = "0";
            wingspans[CreatureConstants.BlackPudding_Elder][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.BlackPudding_Elder][CreatureConstants.BlackPudding_Elder] = "0";
            wingspans[CreatureConstants.BlinkDog][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.BlinkDog][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.BlinkDog][CreatureConstants.BlinkDog] = "0";
            wingspans[CreatureConstants.Boar][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Boar][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Boar][CreatureConstants.Boar] = "0";
            wingspans[CreatureConstants.Boar_Dire][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Boar_Dire][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Boar_Dire][CreatureConstants.Boar_Dire] = "0";
            wingspans[CreatureConstants.Bodak][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Bodak][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Bodak][CreatureConstants.Bodak] = "0";
            wingspans[CreatureConstants.BombardierBeetle_Giant][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.BombardierBeetle_Giant][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.BombardierBeetle_Giant][CreatureConstants.BombardierBeetle_Giant] = "0";
            wingspans[CreatureConstants.BoneDevil_Osyluth][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.BoneDevil_Osyluth][CreatureConstants.BoneDevil_Osyluth] = "0";
            wingspans[CreatureConstants.Bralani][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Bralani][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Bralani][CreatureConstants.Bralani] = "0";
            wingspans[CreatureConstants.Bugbear][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Bugbear][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Bugbear][CreatureConstants.Bugbear] = "0";
            wingspans[CreatureConstants.Bulette][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Bulette][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Bulette][CreatureConstants.Bulette] = "0";
            wingspans[CreatureConstants.Camel_Dromedary][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Camel_Dromedary][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Camel_Dromedary][CreatureConstants.Camel_Dromedary] = "0";
            wingspans[CreatureConstants.Camel_Bactrian][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Camel_Bactrian][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Camel_Bactrian][CreatureConstants.Camel_Bactrian] = "0";
            wingspans[CreatureConstants.CarrionCrawler][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.CarrionCrawler][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.CarrionCrawler][CreatureConstants.CarrionCrawler] = "0";
            wingspans[CreatureConstants.Cat][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Cat][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Cat][CreatureConstants.Cat] = "0";
            wingspans[CreatureConstants.Centaur][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Centaur][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Centaur][CreatureConstants.Centaur] = "0";
            wingspans[CreatureConstants.Centipede_Swarm][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.Centipede_Swarm][CreatureConstants.Centipede_Swarm] = "0";
            wingspans[CreatureConstants.ChainDevil_Kyton][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.ChainDevil_Kyton][CreatureConstants.ChainDevil_Kyton] = "0";
            wingspans[CreatureConstants.ChaosBeast][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.ChaosBeast][CreatureConstants.ChaosBeast] = "0";
            wingspans[CreatureConstants.Cheetah][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Cheetah][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Cheetah][CreatureConstants.Cheetah] = "0";
            //Source: https://forgottenrealms.fandom.com/wiki/Chimera - basing wingspan off of Large (Color) Dragon, since Chimeras are Large and weight is comparable
            wingspans[CreatureConstants.Chimera_Black][GenderConstants.Female] = GetBaseFromAverage(36 * 12);
            wingspans[CreatureConstants.Chimera_Black][GenderConstants.Male] = GetBaseFromAverage(36 * 12);
            wingspans[CreatureConstants.Chimera_Black][CreatureConstants.Chimera_Black] = GetMultiplierFromAverage(36 * 12);
            wingspans[CreatureConstants.Chimera_Blue][GenderConstants.Female] = GetBaseFromAverage(36 * 12);
            wingspans[CreatureConstants.Chimera_Blue][GenderConstants.Male] = GetBaseFromAverage(36 * 12);
            wingspans[CreatureConstants.Chimera_Blue][CreatureConstants.Chimera_Blue] = GetMultiplierFromAverage(36 * 12);
            wingspans[CreatureConstants.Chimera_Green][GenderConstants.Female] = GetBaseFromAverage(36 * 12);
            wingspans[CreatureConstants.Chimera_Green][GenderConstants.Male] = GetBaseFromAverage(36 * 12);
            wingspans[CreatureConstants.Chimera_Green][CreatureConstants.Chimera_Green] = GetMultiplierFromAverage(36 * 12);
            wingspans[CreatureConstants.Chimera_Red][GenderConstants.Female] = GetBaseFromAverage(45 * 12);
            wingspans[CreatureConstants.Chimera_Red][GenderConstants.Male] = GetBaseFromAverage(45 * 12);
            wingspans[CreatureConstants.Chimera_Red][CreatureConstants.Chimera_Red] = GetMultiplierFromAverage(45 * 12);
            wingspans[CreatureConstants.Chimera_White][GenderConstants.Female] = GetBaseFromAverage(32 * 12);
            wingspans[CreatureConstants.Chimera_White][GenderConstants.Male] = GetBaseFromAverage(32 * 12);
            wingspans[CreatureConstants.Chimera_White][CreatureConstants.Chimera_White] = GetMultiplierFromAverage(32 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Choker
            wingspans[CreatureConstants.Choker][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Choker][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Choker][CreatureConstants.Choker] = "0";
            //Source: https://forgottenrealms.fandom.com/wiki/Cloaker and https://www.mojobob.com/roleplay/monstrousmanual/c/cloaker.html
            wingspans[CreatureConstants.Cloaker][GenderConstants.Agender] = GetBaseFromAverage(8 * 12);
            wingspans[CreatureConstants.Cloaker][CreatureConstants.Cloaker] = GetMultiplierFromAverage(8 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Cockatrice - basing wingspan off of height
            wingspans[CreatureConstants.Cockatrice][GenderConstants.Female] = GetBaseFromAverage(3 * 12);
            wingspans[CreatureConstants.Cockatrice][GenderConstants.Male] = GetBaseFromAverage(3 * 12);
            wingspans[CreatureConstants.Cockatrice][CreatureConstants.Cockatrice] = GetMultiplierFromAverage(3 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Couatl
            wingspans[CreatureConstants.Couatl][GenderConstants.Female] = GetBaseFromAverage(15 * 12);
            wingspans[CreatureConstants.Couatl][GenderConstants.Male] = GetBaseFromAverage(15 * 12);
            wingspans[CreatureConstants.Couatl][CreatureConstants.Couatl] = GetBaseFromAverage(15 * 12);
            wingspans[CreatureConstants.Criosphinx][GenderConstants.Male] = GetBaseFromAverage(120);
            wingspans[CreatureConstants.Criosphinx][CreatureConstants.Criosphinx] = GetMultiplierFromAverage(120);
            wingspans[CreatureConstants.Crocodile][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Crocodile][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Crocodile][CreatureConstants.Crocodile] = "0";
            wingspans[CreatureConstants.Crocodile_Giant][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Crocodile_Giant][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Crocodile_Giant][CreatureConstants.Crocodile_Giant] = "0";
            wingspans[CreatureConstants.Cryohydra_5Heads][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Cryohydra_5Heads][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Cryohydra_5Heads][CreatureConstants.Cryohydra_5Heads] = "0";
            wingspans[CreatureConstants.Cryohydra_6Heads][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Cryohydra_6Heads][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Cryohydra_6Heads][CreatureConstants.Cryohydra_6Heads] = "0";
            wingspans[CreatureConstants.Cryohydra_7Heads][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Cryohydra_7Heads][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Cryohydra_7Heads][CreatureConstants.Cryohydra_7Heads] = "0";
            wingspans[CreatureConstants.Cryohydra_8Heads][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Cryohydra_8Heads][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Cryohydra_8Heads][CreatureConstants.Cryohydra_8Heads] = "0";
            wingspans[CreatureConstants.Cryohydra_9Heads][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Cryohydra_9Heads][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Cryohydra_9Heads][CreatureConstants.Cryohydra_9Heads] = "0";
            wingspans[CreatureConstants.Cryohydra_10Heads][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Cryohydra_10Heads][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Cryohydra_10Heads][CreatureConstants.Cryohydra_10Heads] = "0";
            wingspans[CreatureConstants.Cryohydra_11Heads][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Cryohydra_11Heads][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Cryohydra_11Heads][CreatureConstants.Cryohydra_11Heads] = "0";
            wingspans[CreatureConstants.Cryohydra_12Heads][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Cryohydra_12Heads][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Cryohydra_12Heads][CreatureConstants.Cryohydra_12Heads] = "0";
            //Source: https://forgottenrealms.fandom.com/wiki/Darkmantle
            wingspans[CreatureConstants.Darkmantle][GenderConstants.Hermaphrodite] = "0";
            wingspans[CreatureConstants.Darkmantle][CreatureConstants.Darkmantle] = "0";
            wingspans[CreatureConstants.Deinonychus][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Deinonychus][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Deinonychus][CreatureConstants.Deinonychus] = "0";
            wingspans[CreatureConstants.Delver][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Delver][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Delver][CreatureConstants.Delver] = "0";
            wingspans[CreatureConstants.Derro][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Derro][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Derro][CreatureConstants.Derro] = "0";
            wingspans[CreatureConstants.Derro_Sane][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Derro_Sane][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Derro_Sane][CreatureConstants.Derro_Sane] = "0";
            wingspans[CreatureConstants.Destrachan][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Destrachan][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Destrachan][CreatureConstants.Destrachan] = "0";
            wingspans[CreatureConstants.Devourer][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.Devourer][CreatureConstants.Devourer] = "0";
            wingspans[CreatureConstants.Digester][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Digester][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Digester][CreatureConstants.Digester] = "0";
            wingspans[CreatureConstants.Djinni][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.Djinni][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Djinni][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Djinni][CreatureConstants.Djinni] = "0";
            wingspans[CreatureConstants.Djinni_Noble][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.Djinni_Noble][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Djinni_Noble][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Djinni_Noble][CreatureConstants.Djinni_Noble] = "0";
            wingspans[CreatureConstants.Dog][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Dog][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Dog][CreatureConstants.Dog] = "0";
            wingspans[CreatureConstants.Dog_Riding][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Dog_Riding][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Dog_Riding][CreatureConstants.Dog_Riding] = "0";
            wingspans[CreatureConstants.Donkey][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Donkey][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Donkey][CreatureConstants.Donkey] = "0";
            wingspans[CreatureConstants.Doppelganger][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.Doppelganger][CreatureConstants.Doppelganger] = "0";
            //Source: Draconomicon
            wingspans[CreatureConstants.Dragon_Black_Wyrmling][GenderConstants.Female] = GetBaseFromAverage(8 * 12);
            wingspans[CreatureConstants.Dragon_Black_Wyrmling][GenderConstants.Male] = GetBaseFromAverage(8 * 12);
            wingspans[CreatureConstants.Dragon_Black_Wyrmling][CreatureConstants.Dragon_Black_Wyrmling] = GetMultiplierFromAverage(8 * 12);
            wingspans[CreatureConstants.Dragon_Black_VeryYoung][GenderConstants.Female] = GetBaseFromAverage(16 * 12);
            wingspans[CreatureConstants.Dragon_Black_VeryYoung][GenderConstants.Male] = GetBaseFromAverage(16 * 12);
            wingspans[CreatureConstants.Dragon_Black_VeryYoung][CreatureConstants.Dragon_Black_VeryYoung] = GetMultiplierFromAverage(16 * 12);
            wingspans[CreatureConstants.Dragon_Black_Young][GenderConstants.Female] = GetBaseFromAverage(24 * 12);
            wingspans[CreatureConstants.Dragon_Black_Young][GenderConstants.Male] = GetBaseFromAverage(24 * 12);
            wingspans[CreatureConstants.Dragon_Black_Young][CreatureConstants.Dragon_Black_Young] = GetMultiplierFromAverage(24 * 12);
            wingspans[CreatureConstants.Dragon_Black_Juvenile][GenderConstants.Female] = GetBaseFromAverage(24 * 12);
            wingspans[CreatureConstants.Dragon_Black_Juvenile][GenderConstants.Male] = GetBaseFromAverage(24 * 12);
            wingspans[CreatureConstants.Dragon_Black_Juvenile][CreatureConstants.Dragon_Black_Juvenile] = GetMultiplierFromAverage(24 * 12);
            wingspans[CreatureConstants.Dragon_Black_YoungAdult][GenderConstants.Female] = GetBaseFromAverage(36 * 12);
            wingspans[CreatureConstants.Dragon_Black_YoungAdult][GenderConstants.Male] = GetBaseFromAverage(36 * 12);
            wingspans[CreatureConstants.Dragon_Black_YoungAdult][CreatureConstants.Dragon_Black_YoungAdult] = GetMultiplierFromAverage(36 * 12);
            wingspans[CreatureConstants.Dragon_Black_Adult][GenderConstants.Female] = GetBaseFromAverage(36 * 12);
            wingspans[CreatureConstants.Dragon_Black_Adult][GenderConstants.Male] = GetBaseFromAverage(36 * 12);
            wingspans[CreatureConstants.Dragon_Black_Adult][CreatureConstants.Dragon_Black_Adult] = GetMultiplierFromAverage(36 * 12);
            wingspans[CreatureConstants.Dragon_Black_MatureAdult][GenderConstants.Female] = GetBaseFromAverage(60 * 12);
            wingspans[CreatureConstants.Dragon_Black_MatureAdult][GenderConstants.Male] = GetBaseFromAverage(60 * 12);
            wingspans[CreatureConstants.Dragon_Black_MatureAdult][CreatureConstants.Dragon_Black_MatureAdult] = GetMultiplierFromAverage(60 * 12);
            wingspans[CreatureConstants.Dragon_Black_Old][GenderConstants.Female] = GetBaseFromAverage(60 * 12);
            wingspans[CreatureConstants.Dragon_Black_Old][GenderConstants.Male] = GetBaseFromAverage(60 * 12);
            wingspans[CreatureConstants.Dragon_Black_Old][CreatureConstants.Dragon_Black_Old] = GetMultiplierFromAverage(60 * 12);
            wingspans[CreatureConstants.Dragon_Black_VeryOld][GenderConstants.Female] = GetBaseFromAverage(60 * 12);
            wingspans[CreatureConstants.Dragon_Black_VeryOld][GenderConstants.Male] = GetBaseFromAverage(60 * 12);
            wingspans[CreatureConstants.Dragon_Black_VeryOld][CreatureConstants.Dragon_Black_VeryOld] = GetMultiplierFromAverage(60 * 12);
            wingspans[CreatureConstants.Dragon_Black_Ancient][GenderConstants.Female] = GetBaseFromAverage(60 * 12);
            wingspans[CreatureConstants.Dragon_Black_Ancient][GenderConstants.Male] = GetBaseFromAverage(60 * 12);
            wingspans[CreatureConstants.Dragon_Black_Ancient][CreatureConstants.Dragon_Black_Ancient] = GetMultiplierFromAverage(60 * 12);
            wingspans[CreatureConstants.Dragon_Black_Wyrm][GenderConstants.Female] = GetBaseFromAverage(80 * 12);
            wingspans[CreatureConstants.Dragon_Black_Wyrm][GenderConstants.Male] = GetBaseFromAverage(80 * 12);
            wingspans[CreatureConstants.Dragon_Black_Wyrm][CreatureConstants.Dragon_Black_Wyrm] = GetMultiplierFromAverage(80 * 12);
            wingspans[CreatureConstants.Dragon_Black_GreatWyrm][GenderConstants.Female] = GetBaseFromAverage(80 * 12);
            wingspans[CreatureConstants.Dragon_Black_GreatWyrm][GenderConstants.Male] = GetBaseFromAverage(80 * 12);
            wingspans[CreatureConstants.Dragon_Black_GreatWyrm][CreatureConstants.Dragon_Black_GreatWyrm] = GetMultiplierFromAverage(80 * 12);
            wingspans[CreatureConstants.Dragon_Blue_Wyrmling][GenderConstants.Female] = GetBaseFromAverage(16 * 12);
            wingspans[CreatureConstants.Dragon_Blue_Wyrmling][GenderConstants.Male] = GetBaseFromAverage(16 * 12);
            wingspans[CreatureConstants.Dragon_Blue_Wyrmling][CreatureConstants.Dragon_Blue_Wyrmling] = GetMultiplierFromAverage(16 * 12);
            wingspans[CreatureConstants.Dragon_Blue_VeryYoung][GenderConstants.Female] = GetBaseFromAverage(24 * 12);
            wingspans[CreatureConstants.Dragon_Blue_VeryYoung][GenderConstants.Male] = GetBaseFromAverage(24 * 12);
            wingspans[CreatureConstants.Dragon_Blue_VeryYoung][CreatureConstants.Dragon_Blue_VeryYoung] = GetMultiplierFromAverage(24 * 12);
            wingspans[CreatureConstants.Dragon_Blue_Young][GenderConstants.Female] = GetBaseFromAverage(24 * 12);
            wingspans[CreatureConstants.Dragon_Blue_Young][GenderConstants.Male] = GetBaseFromAverage(24 * 12);
            wingspans[CreatureConstants.Dragon_Blue_Young][CreatureConstants.Dragon_Blue_Young] = GetMultiplierFromAverage(24 * 12);
            wingspans[CreatureConstants.Dragon_Blue_Juvenile][GenderConstants.Female] = GetBaseFromAverage(36 * 12);
            wingspans[CreatureConstants.Dragon_Blue_Juvenile][GenderConstants.Male] = GetBaseFromAverage(36 * 12);
            wingspans[CreatureConstants.Dragon_Blue_Juvenile][CreatureConstants.Dragon_Blue_Juvenile] = GetMultiplierFromAverage(36 * 12);
            wingspans[CreatureConstants.Dragon_Blue_YoungAdult][GenderConstants.Female] = GetBaseFromAverage(36 * 12);
            wingspans[CreatureConstants.Dragon_Blue_YoungAdult][GenderConstants.Male] = GetBaseFromAverage(36 * 12);
            wingspans[CreatureConstants.Dragon_Blue_YoungAdult][CreatureConstants.Dragon_Blue_YoungAdult] = GetMultiplierFromAverage(36 * 12);
            wingspans[CreatureConstants.Dragon_Blue_Adult][GenderConstants.Female] = GetBaseFromAverage(60 * 12);
            wingspans[CreatureConstants.Dragon_Blue_Adult][GenderConstants.Male] = GetBaseFromAverage(60 * 12);
            wingspans[CreatureConstants.Dragon_Blue_Adult][CreatureConstants.Dragon_Blue_Adult] = GetMultiplierFromAverage(60 * 12);
            wingspans[CreatureConstants.Dragon_Blue_MatureAdult][GenderConstants.Female] = GetBaseFromAverage(60 * 12);
            wingspans[CreatureConstants.Dragon_Blue_MatureAdult][GenderConstants.Male] = GetBaseFromAverage(60 * 12);
            wingspans[CreatureConstants.Dragon_Blue_MatureAdult][CreatureConstants.Dragon_Blue_MatureAdult] = GetMultiplierFromAverage(60 * 12);
            wingspans[CreatureConstants.Dragon_Blue_Old][GenderConstants.Female] = GetBaseFromAverage(60 * 12);
            wingspans[CreatureConstants.Dragon_Blue_Old][GenderConstants.Male] = GetBaseFromAverage(60 * 12);
            wingspans[CreatureConstants.Dragon_Blue_Old][CreatureConstants.Dragon_Blue_Old] = GetMultiplierFromAverage(60 * 12);
            wingspans[CreatureConstants.Dragon_Blue_VeryOld][GenderConstants.Female] = GetBaseFromAverage(60 * 12);
            wingspans[CreatureConstants.Dragon_Blue_VeryOld][GenderConstants.Male] = GetBaseFromAverage(60 * 12);
            wingspans[CreatureConstants.Dragon_Blue_VeryOld][CreatureConstants.Dragon_Blue_VeryOld] = GetMultiplierFromAverage(60 * 12);
            wingspans[CreatureConstants.Dragon_Blue_Ancient][GenderConstants.Female] = GetBaseFromAverage(80 * 12);
            wingspans[CreatureConstants.Dragon_Blue_Ancient][GenderConstants.Male] = GetBaseFromAverage(80 * 12);
            wingspans[CreatureConstants.Dragon_Blue_Ancient][CreatureConstants.Dragon_Blue_Ancient] = GetMultiplierFromAverage(80 * 12);
            wingspans[CreatureConstants.Dragon_Blue_Wyrm][GenderConstants.Female] = GetBaseFromAverage(80 * 12);
            wingspans[CreatureConstants.Dragon_Blue_Wyrm][GenderConstants.Male] = GetBaseFromAverage(80 * 12);
            wingspans[CreatureConstants.Dragon_Blue_Wyrm][CreatureConstants.Dragon_Blue_Wyrm] = GetMultiplierFromAverage(80 * 12);
            wingspans[CreatureConstants.Dragon_Blue_GreatWyrm][GenderConstants.Female] = GetBaseFromAverage(80 * 12);
            wingspans[CreatureConstants.Dragon_Blue_GreatWyrm][GenderConstants.Male] = GetBaseFromAverage(80 * 12);
            wingspans[CreatureConstants.Dragon_Blue_GreatWyrm][CreatureConstants.Dragon_Blue_GreatWyrm] = GetMultiplierFromAverage(80 * 12);
            wingspans[CreatureConstants.Dragon_Brass_Wyrmling][GenderConstants.Female] = GetBaseFromAverage(6 * 12);
            wingspans[CreatureConstants.Dragon_Brass_Wyrmling][GenderConstants.Male] = GetBaseFromAverage(6 * 12);
            wingspans[CreatureConstants.Dragon_Brass_Wyrmling][CreatureConstants.Dragon_Brass_Wyrmling] = GetMultiplierFromAverage(6 * 12);
            wingspans[CreatureConstants.Dragon_Brass_VeryYoung][GenderConstants.Female] = GetBaseFromAverage(12 * 12);
            wingspans[CreatureConstants.Dragon_Brass_VeryYoung][GenderConstants.Male] = GetBaseFromAverage(12 * 12);
            wingspans[CreatureConstants.Dragon_Brass_VeryYoung][CreatureConstants.Dragon_Brass_VeryYoung] = GetMultiplierFromAverage(12 * 12);
            wingspans[CreatureConstants.Dragon_Brass_Young][GenderConstants.Female] = GetBaseFromAverage(18 * 12);
            wingspans[CreatureConstants.Dragon_Brass_Young][GenderConstants.Male] = GetBaseFromAverage(18 * 12);
            wingspans[CreatureConstants.Dragon_Brass_Young][CreatureConstants.Dragon_Brass_Young] = GetMultiplierFromAverage(18 * 12);
            wingspans[CreatureConstants.Dragon_Brass_Juvenile][GenderConstants.Female] = GetBaseFromAverage(18 * 12);
            wingspans[CreatureConstants.Dragon_Brass_Juvenile][GenderConstants.Male] = GetBaseFromAverage(18 * 12);
            wingspans[CreatureConstants.Dragon_Brass_Juvenile][CreatureConstants.Dragon_Brass_Juvenile] = GetMultiplierFromAverage(18 * 12);
            wingspans[CreatureConstants.Dragon_Brass_YoungAdult][GenderConstants.Female] = GetBaseFromAverage(27 * 12);
            wingspans[CreatureConstants.Dragon_Brass_YoungAdult][GenderConstants.Male] = GetBaseFromAverage(27 * 12);
            wingspans[CreatureConstants.Dragon_Brass_YoungAdult][CreatureConstants.Dragon_Brass_YoungAdult] = GetMultiplierFromAverage(27 * 12);
            wingspans[CreatureConstants.Dragon_Brass_Adult][GenderConstants.Female] = GetBaseFromAverage(27 * 12);
            wingspans[CreatureConstants.Dragon_Brass_Adult][GenderConstants.Male] = GetBaseFromAverage(27 * 12);
            wingspans[CreatureConstants.Dragon_Brass_Adult][CreatureConstants.Dragon_Brass_Adult] = GetMultiplierFromAverage(27 * 12);
            wingspans[CreatureConstants.Dragon_Brass_MatureAdult][GenderConstants.Female] = GetBaseFromAverage(45 * 12);
            wingspans[CreatureConstants.Dragon_Brass_MatureAdult][GenderConstants.Male] = GetBaseFromAverage(45 * 12);
            wingspans[CreatureConstants.Dragon_Brass_MatureAdult][CreatureConstants.Dragon_Brass_MatureAdult] = GetMultiplierFromAverage(45 * 12);
            wingspans[CreatureConstants.Dragon_Brass_Old][GenderConstants.Female] = GetBaseFromAverage(45 * 12);
            wingspans[CreatureConstants.Dragon_Brass_Old][GenderConstants.Male] = GetBaseFromAverage(45 * 12);
            wingspans[CreatureConstants.Dragon_Brass_Old][CreatureConstants.Dragon_Brass_Old] = GetMultiplierFromAverage(45 * 12);
            wingspans[CreatureConstants.Dragon_Brass_VeryOld][GenderConstants.Female] = GetBaseFromAverage(45 * 12);
            wingspans[CreatureConstants.Dragon_Brass_VeryOld][GenderConstants.Male] = GetBaseFromAverage(45 * 12);
            wingspans[CreatureConstants.Dragon_Brass_VeryOld][CreatureConstants.Dragon_Brass_VeryOld] = GetMultiplierFromAverage(45 * 12);
            wingspans[CreatureConstants.Dragon_Brass_Ancient][GenderConstants.Female] = GetBaseFromAverage(45 * 12);
            wingspans[CreatureConstants.Dragon_Brass_Ancient][GenderConstants.Male] = GetBaseFromAverage(45 * 12);
            wingspans[CreatureConstants.Dragon_Brass_Ancient][CreatureConstants.Dragon_Brass_Ancient] = GetMultiplierFromAverage(45 * 12);
            wingspans[CreatureConstants.Dragon_Brass_Wyrm][GenderConstants.Female] = GetBaseFromAverage(60 * 12);
            wingspans[CreatureConstants.Dragon_Brass_Wyrm][GenderConstants.Male] = GetBaseFromAverage(60 * 12);
            wingspans[CreatureConstants.Dragon_Brass_Wyrm][CreatureConstants.Dragon_Brass_Wyrm] = GetMultiplierFromAverage(60 * 12);
            wingspans[CreatureConstants.Dragon_Brass_GreatWyrm][GenderConstants.Female] = GetBaseFromAverage(60 * 12);
            wingspans[CreatureConstants.Dragon_Brass_GreatWyrm][GenderConstants.Male] = GetBaseFromAverage(60 * 12);
            wingspans[CreatureConstants.Dragon_Brass_GreatWyrm][CreatureConstants.Dragon_Brass_GreatWyrm] = GetMultiplierFromAverage(60 * 12);
            wingspans[CreatureConstants.Dragon_Bronze_Wyrmling][GenderConstants.Female] = GetBaseFromAverage(16 * 12);
            wingspans[CreatureConstants.Dragon_Bronze_Wyrmling][GenderConstants.Male] = GetBaseFromAverage(16 * 12);
            wingspans[CreatureConstants.Dragon_Bronze_Wyrmling][CreatureConstants.Dragon_Bronze_Wyrmling] = GetMultiplierFromAverage(16 * 12);
            wingspans[CreatureConstants.Dragon_Bronze_VeryYoung][GenderConstants.Female] = GetBaseFromAverage(24 * 12);
            wingspans[CreatureConstants.Dragon_Bronze_VeryYoung][GenderConstants.Male] = GetBaseFromAverage(24 * 12);
            wingspans[CreatureConstants.Dragon_Bronze_VeryYoung][CreatureConstants.Dragon_Bronze_VeryYoung] = GetMultiplierFromAverage(24 * 12);
            wingspans[CreatureConstants.Dragon_Bronze_Young][GenderConstants.Female] = GetBaseFromAverage(24 * 12);
            wingspans[CreatureConstants.Dragon_Bronze_Young][GenderConstants.Male] = GetBaseFromAverage(24 * 12);
            wingspans[CreatureConstants.Dragon_Bronze_Young][CreatureConstants.Dragon_Bronze_Young] = GetMultiplierFromAverage(24 * 12);
            wingspans[CreatureConstants.Dragon_Bronze_Juvenile][GenderConstants.Female] = GetBaseFromAverage(36 * 12);
            wingspans[CreatureConstants.Dragon_Bronze_Juvenile][GenderConstants.Male] = GetBaseFromAverage(36 * 12);
            wingspans[CreatureConstants.Dragon_Bronze_Juvenile][CreatureConstants.Dragon_Bronze_Juvenile] = GetMultiplierFromAverage(36 * 12);
            wingspans[CreatureConstants.Dragon_Bronze_YoungAdult][GenderConstants.Female] = GetBaseFromAverage(36 * 12);
            wingspans[CreatureConstants.Dragon_Bronze_YoungAdult][GenderConstants.Male] = GetBaseFromAverage(36 * 12);
            wingspans[CreatureConstants.Dragon_Bronze_YoungAdult][CreatureConstants.Dragon_Bronze_YoungAdult] = GetMultiplierFromAverage(36 * 12);
            wingspans[CreatureConstants.Dragon_Bronze_Adult][GenderConstants.Female] = GetBaseFromAverage(60 * 12);
            wingspans[CreatureConstants.Dragon_Bronze_Adult][GenderConstants.Male] = GetBaseFromAverage(60 * 12);
            wingspans[CreatureConstants.Dragon_Bronze_Adult][CreatureConstants.Dragon_Bronze_Adult] = GetMultiplierFromAverage(60 * 12);
            wingspans[CreatureConstants.Dragon_Bronze_MatureAdult][GenderConstants.Female] = GetBaseFromAverage(60 * 12);
            wingspans[CreatureConstants.Dragon_Bronze_MatureAdult][GenderConstants.Male] = GetBaseFromAverage(60 * 12);
            wingspans[CreatureConstants.Dragon_Bronze_MatureAdult][CreatureConstants.Dragon_Bronze_MatureAdult] = GetMultiplierFromAverage(60 * 12);
            wingspans[CreatureConstants.Dragon_Bronze_Old][GenderConstants.Female] = GetBaseFromAverage(60 * 12);
            wingspans[CreatureConstants.Dragon_Bronze_Old][GenderConstants.Male] = GetBaseFromAverage(60 * 12);
            wingspans[CreatureConstants.Dragon_Bronze_Old][CreatureConstants.Dragon_Bronze_Old] = GetMultiplierFromAverage(60 * 12);
            wingspans[CreatureConstants.Dragon_Bronze_VeryOld][GenderConstants.Female] = GetBaseFromAverage(60 * 12);
            wingspans[CreatureConstants.Dragon_Bronze_VeryOld][GenderConstants.Male] = GetBaseFromAverage(60 * 12);
            wingspans[CreatureConstants.Dragon_Bronze_VeryOld][CreatureConstants.Dragon_Bronze_VeryOld] = GetMultiplierFromAverage(60 * 12);
            wingspans[CreatureConstants.Dragon_Bronze_Ancient][GenderConstants.Female] = GetBaseFromAverage(80 * 12);
            wingspans[CreatureConstants.Dragon_Bronze_Ancient][GenderConstants.Male] = GetBaseFromAverage(80 * 12);
            wingspans[CreatureConstants.Dragon_Bronze_Ancient][CreatureConstants.Dragon_Bronze_Ancient] = GetMultiplierFromAverage(80 * 12);
            wingspans[CreatureConstants.Dragon_Bronze_Wyrm][GenderConstants.Female] = GetBaseFromAverage(80 * 12);
            wingspans[CreatureConstants.Dragon_Bronze_Wyrm][GenderConstants.Male] = GetBaseFromAverage(80 * 12);
            wingspans[CreatureConstants.Dragon_Bronze_Wyrm][CreatureConstants.Dragon_Bronze_Wyrm] = GetMultiplierFromAverage(80 * 12);
            wingspans[CreatureConstants.Dragon_Bronze_GreatWyrm][GenderConstants.Female] = GetBaseFromAverage(80 * 12);
            wingspans[CreatureConstants.Dragon_Bronze_GreatWyrm][GenderConstants.Male] = GetBaseFromAverage(80 * 12);
            wingspans[CreatureConstants.Dragon_Bronze_GreatWyrm][CreatureConstants.Dragon_Bronze_GreatWyrm] = GetMultiplierFromAverage(80 * 12);
            wingspans[CreatureConstants.Dragon_Copper_Wyrmling][GenderConstants.Female] = GetBaseFromAverage(8 * 12);
            wingspans[CreatureConstants.Dragon_Copper_Wyrmling][GenderConstants.Male] = GetBaseFromAverage(8 * 12);
            wingspans[CreatureConstants.Dragon_Copper_Wyrmling][CreatureConstants.Dragon_Copper_Wyrmling] = GetMultiplierFromAverage(8 * 12);
            wingspans[CreatureConstants.Dragon_Copper_VeryYoung][GenderConstants.Female] = GetBaseFromAverage(16 * 12);
            wingspans[CreatureConstants.Dragon_Copper_VeryYoung][GenderConstants.Male] = GetBaseFromAverage(16 * 12);
            wingspans[CreatureConstants.Dragon_Copper_VeryYoung][CreatureConstants.Dragon_Copper_VeryYoung] = GetMultiplierFromAverage(16 * 12);
            wingspans[CreatureConstants.Dragon_Copper_Young][GenderConstants.Female] = GetBaseFromAverage(24 * 12);
            wingspans[CreatureConstants.Dragon_Copper_Young][GenderConstants.Male] = GetBaseFromAverage(24 * 12);
            wingspans[CreatureConstants.Dragon_Copper_Young][CreatureConstants.Dragon_Copper_Young] = GetMultiplierFromAverage(24 * 12);
            wingspans[CreatureConstants.Dragon_Copper_Juvenile][GenderConstants.Female] = GetBaseFromAverage(24 * 12);
            wingspans[CreatureConstants.Dragon_Copper_Juvenile][GenderConstants.Male] = GetBaseFromAverage(24 * 12);
            wingspans[CreatureConstants.Dragon_Copper_Juvenile][CreatureConstants.Dragon_Copper_Juvenile] = GetMultiplierFromAverage(24 * 12);
            wingspans[CreatureConstants.Dragon_Copper_YoungAdult][GenderConstants.Female] = GetBaseFromAverage(36 * 12);
            wingspans[CreatureConstants.Dragon_Copper_YoungAdult][GenderConstants.Male] = GetBaseFromAverage(36 * 12);
            wingspans[CreatureConstants.Dragon_Copper_YoungAdult][CreatureConstants.Dragon_Copper_YoungAdult] = GetMultiplierFromAverage(36 * 12);
            wingspans[CreatureConstants.Dragon_Copper_Adult][GenderConstants.Female] = GetBaseFromAverage(36 * 12);
            wingspans[CreatureConstants.Dragon_Copper_Adult][GenderConstants.Male] = GetBaseFromAverage(36 * 12);
            wingspans[CreatureConstants.Dragon_Copper_Adult][CreatureConstants.Dragon_Copper_Adult] = GetMultiplierFromAverage(36 * 12);
            wingspans[CreatureConstants.Dragon_Copper_MatureAdult][GenderConstants.Female] = GetBaseFromAverage(60 * 12);
            wingspans[CreatureConstants.Dragon_Copper_MatureAdult][GenderConstants.Male] = GetBaseFromAverage(60 * 12);
            wingspans[CreatureConstants.Dragon_Copper_MatureAdult][CreatureConstants.Dragon_Copper_MatureAdult] = GetMultiplierFromAverage(60 * 12);
            wingspans[CreatureConstants.Dragon_Copper_Old][GenderConstants.Female] = GetBaseFromAverage(60 * 12);
            wingspans[CreatureConstants.Dragon_Copper_Old][GenderConstants.Male] = GetBaseFromAverage(60 * 12);
            wingspans[CreatureConstants.Dragon_Copper_Old][CreatureConstants.Dragon_Copper_Old] = GetMultiplierFromAverage(60 * 12);
            wingspans[CreatureConstants.Dragon_Copper_VeryOld][GenderConstants.Female] = GetBaseFromAverage(60 * 12);
            wingspans[CreatureConstants.Dragon_Copper_VeryOld][GenderConstants.Male] = GetBaseFromAverage(60 * 12);
            wingspans[CreatureConstants.Dragon_Copper_VeryOld][CreatureConstants.Dragon_Copper_VeryOld] = GetMultiplierFromAverage(60 * 12);
            wingspans[CreatureConstants.Dragon_Copper_Ancient][GenderConstants.Female] = GetBaseFromAverage(60 * 12);
            wingspans[CreatureConstants.Dragon_Copper_Ancient][GenderConstants.Male] = GetBaseFromAverage(60 * 12);
            wingspans[CreatureConstants.Dragon_Copper_Ancient][CreatureConstants.Dragon_Copper_Ancient] = GetMultiplierFromAverage(60 * 12);
            wingspans[CreatureConstants.Dragon_Copper_Wyrm][GenderConstants.Female] = GetBaseFromAverage(80 * 12);
            wingspans[CreatureConstants.Dragon_Copper_Wyrm][GenderConstants.Male] = GetBaseFromAverage(80 * 12);
            wingspans[CreatureConstants.Dragon_Copper_Wyrm][CreatureConstants.Dragon_Copper_Wyrm] = GetMultiplierFromAverage(80 * 12);
            wingspans[CreatureConstants.Dragon_Copper_GreatWyrm][GenderConstants.Female] = GetBaseFromAverage(80 * 12);
            wingspans[CreatureConstants.Dragon_Copper_GreatWyrm][GenderConstants.Male] = GetBaseFromAverage(80 * 12);
            wingspans[CreatureConstants.Dragon_Copper_GreatWyrm][CreatureConstants.Dragon_Copper_GreatWyrm] = GetMultiplierFromAverage(80 * 12);
            wingspans[CreatureConstants.Dragon_Gold_Wyrmling][GenderConstants.Female] = GetBaseFromAverage(27 * 12);
            wingspans[CreatureConstants.Dragon_Gold_Wyrmling][GenderConstants.Male] = GetBaseFromAverage(27 * 12);
            wingspans[CreatureConstants.Dragon_Gold_Wyrmling][CreatureConstants.Dragon_Gold_Wyrmling] = GetMultiplierFromAverage(27 * 12);
            wingspans[CreatureConstants.Dragon_Gold_VeryYoung][GenderConstants.Female] = GetBaseFromAverage(40 * 12);
            wingspans[CreatureConstants.Dragon_Gold_VeryYoung][GenderConstants.Male] = GetBaseFromAverage(40 * 12);
            wingspans[CreatureConstants.Dragon_Gold_VeryYoung][CreatureConstants.Dragon_Gold_VeryYoung] = GetMultiplierFromAverage(40 * 12);
            wingspans[CreatureConstants.Dragon_Gold_Young][GenderConstants.Female] = GetBaseFromAverage(40 * 12);
            wingspans[CreatureConstants.Dragon_Gold_Young][GenderConstants.Male] = GetBaseFromAverage(40 * 12);
            wingspans[CreatureConstants.Dragon_Gold_Young][CreatureConstants.Dragon_Gold_Young] = GetMultiplierFromAverage(40 * 12);
            wingspans[CreatureConstants.Dragon_Gold_Juvenile][GenderConstants.Female] = GetBaseFromAverage(40 * 12);
            wingspans[CreatureConstants.Dragon_Gold_Juvenile][GenderConstants.Male] = GetBaseFromAverage(40 * 12);
            wingspans[CreatureConstants.Dragon_Gold_Juvenile][CreatureConstants.Dragon_Gold_Juvenile] = GetMultiplierFromAverage(40 * 12);
            wingspans[CreatureConstants.Dragon_Gold_YoungAdult][GenderConstants.Female] = GetBaseFromAverage(68 * 12);
            wingspans[CreatureConstants.Dragon_Gold_YoungAdult][GenderConstants.Male] = GetBaseFromAverage(68 * 12);
            wingspans[CreatureConstants.Dragon_Gold_YoungAdult][CreatureConstants.Dragon_Gold_YoungAdult] = GetMultiplierFromAverage(68 * 12);
            wingspans[CreatureConstants.Dragon_Gold_Adult][GenderConstants.Female] = GetBaseFromAverage(68 * 12);
            wingspans[CreatureConstants.Dragon_Gold_Adult][GenderConstants.Male] = GetBaseFromAverage(68 * 12);
            wingspans[CreatureConstants.Dragon_Gold_Adult][CreatureConstants.Dragon_Gold_Adult] = GetMultiplierFromAverage(68 * 12);
            wingspans[CreatureConstants.Dragon_Gold_MatureAdult][GenderConstants.Female] = GetBaseFromAverage(68 * 12);
            wingspans[CreatureConstants.Dragon_Gold_MatureAdult][GenderConstants.Male] = GetBaseFromAverage(68 * 12);
            wingspans[CreatureConstants.Dragon_Gold_MatureAdult][CreatureConstants.Dragon_Gold_MatureAdult] = GetMultiplierFromAverage(68 * 12);
            wingspans[CreatureConstants.Dragon_Gold_Old][GenderConstants.Female] = GetBaseFromAverage(90 * 12);
            wingspans[CreatureConstants.Dragon_Gold_Old][GenderConstants.Male] = GetBaseFromAverage(90 * 12);
            wingspans[CreatureConstants.Dragon_Gold_Old][CreatureConstants.Dragon_Gold_Old] = GetMultiplierFromAverage(90 * 12);
            wingspans[CreatureConstants.Dragon_Gold_VeryOld][GenderConstants.Female] = GetBaseFromAverage(90 * 12);
            wingspans[CreatureConstants.Dragon_Gold_VeryOld][GenderConstants.Male] = GetBaseFromAverage(90 * 12);
            wingspans[CreatureConstants.Dragon_Gold_VeryOld][CreatureConstants.Dragon_Gold_VeryOld] = GetMultiplierFromAverage(90 * 12);
            wingspans[CreatureConstants.Dragon_Gold_Ancient][GenderConstants.Female] = GetBaseFromAverage(90 * 12);
            wingspans[CreatureConstants.Dragon_Gold_Ancient][GenderConstants.Male] = GetBaseFromAverage(90 * 12);
            wingspans[CreatureConstants.Dragon_Gold_Ancient][CreatureConstants.Dragon_Gold_Ancient] = GetMultiplierFromAverage(90 * 12);
            wingspans[CreatureConstants.Dragon_Gold_Wyrm][GenderConstants.Female] = GetBaseFromAverage(135 * 12);
            wingspans[CreatureConstants.Dragon_Gold_Wyrm][GenderConstants.Male] = GetBaseFromAverage(135 * 12);
            wingspans[CreatureConstants.Dragon_Gold_Wyrm][CreatureConstants.Dragon_Gold_Wyrm] = GetMultiplierFromAverage(135 * 12);
            wingspans[CreatureConstants.Dragon_Gold_GreatWyrm][GenderConstants.Female] = GetBaseFromAverage(135 * 12);
            wingspans[CreatureConstants.Dragon_Gold_GreatWyrm][GenderConstants.Male] = GetBaseFromAverage(135 * 12);
            wingspans[CreatureConstants.Dragon_Gold_GreatWyrm][CreatureConstants.Dragon_Gold_GreatWyrm] = GetMultiplierFromAverage(135 * 12);
            wingspans[CreatureConstants.Dragon_Green_Wyrmling][GenderConstants.Female] = GetBaseFromAverage(16 * 12);
            wingspans[CreatureConstants.Dragon_Green_Wyrmling][GenderConstants.Male] = GetBaseFromAverage(16 * 12);
            wingspans[CreatureConstants.Dragon_Green_Wyrmling][CreatureConstants.Dragon_Green_Wyrmling] = GetMultiplierFromAverage(16 * 12);
            wingspans[CreatureConstants.Dragon_Green_VeryYoung][GenderConstants.Female] = GetBaseFromAverage(24 * 12);
            wingspans[CreatureConstants.Dragon_Green_VeryYoung][GenderConstants.Male] = GetBaseFromAverage(24 * 12);
            wingspans[CreatureConstants.Dragon_Green_VeryYoung][CreatureConstants.Dragon_Green_VeryYoung] = GetMultiplierFromAverage(24 * 12);
            wingspans[CreatureConstants.Dragon_Green_Young][GenderConstants.Female] = GetBaseFromAverage(24 * 12);
            wingspans[CreatureConstants.Dragon_Green_Young][GenderConstants.Male] = GetBaseFromAverage(24 * 12);
            wingspans[CreatureConstants.Dragon_Green_Young][CreatureConstants.Dragon_Green_Young] = GetMultiplierFromAverage(24 * 12);
            wingspans[CreatureConstants.Dragon_Green_Juvenile][GenderConstants.Female] = GetBaseFromAverage(36 * 12);
            wingspans[CreatureConstants.Dragon_Green_Juvenile][GenderConstants.Male] = GetBaseFromAverage(36 * 12);
            wingspans[CreatureConstants.Dragon_Green_Juvenile][CreatureConstants.Dragon_Green_Juvenile] = GetMultiplierFromAverage(36 * 12);
            wingspans[CreatureConstants.Dragon_Green_YoungAdult][GenderConstants.Female] = GetBaseFromAverage(36 * 12);
            wingspans[CreatureConstants.Dragon_Green_YoungAdult][GenderConstants.Male] = GetBaseFromAverage(36 * 12);
            wingspans[CreatureConstants.Dragon_Green_YoungAdult][CreatureConstants.Dragon_Green_YoungAdult] = GetMultiplierFromAverage(36 * 12);
            wingspans[CreatureConstants.Dragon_Green_Adult][GenderConstants.Female] = GetBaseFromAverage(60 * 12);
            wingspans[CreatureConstants.Dragon_Green_Adult][GenderConstants.Male] = GetBaseFromAverage(60 * 12);
            wingspans[CreatureConstants.Dragon_Green_Adult][CreatureConstants.Dragon_Green_Adult] = GetMultiplierFromAverage(60 * 12);
            wingspans[CreatureConstants.Dragon_Green_MatureAdult][GenderConstants.Female] = GetBaseFromAverage(60 * 12);
            wingspans[CreatureConstants.Dragon_Green_MatureAdult][GenderConstants.Male] = GetBaseFromAverage(60 * 12);
            wingspans[CreatureConstants.Dragon_Green_MatureAdult][CreatureConstants.Dragon_Green_MatureAdult] = GetMultiplierFromAverage(60 * 12);
            wingspans[CreatureConstants.Dragon_Green_Old][GenderConstants.Female] = GetBaseFromAverage(60 * 12);
            wingspans[CreatureConstants.Dragon_Green_Old][GenderConstants.Male] = GetBaseFromAverage(60 * 12);
            wingspans[CreatureConstants.Dragon_Green_Old][CreatureConstants.Dragon_Green_Old] = GetMultiplierFromAverage(60 * 12);
            wingspans[CreatureConstants.Dragon_Green_VeryOld][GenderConstants.Female] = GetBaseFromAverage(60 * 12);
            wingspans[CreatureConstants.Dragon_Green_VeryOld][GenderConstants.Male] = GetBaseFromAverage(60 * 12);
            wingspans[CreatureConstants.Dragon_Green_VeryOld][CreatureConstants.Dragon_Green_VeryOld] = GetMultiplierFromAverage(60 * 12);
            wingspans[CreatureConstants.Dragon_Green_Ancient][GenderConstants.Female] = GetBaseFromAverage(80 * 12);
            wingspans[CreatureConstants.Dragon_Green_Ancient][GenderConstants.Male] = GetBaseFromAverage(80 * 12);
            wingspans[CreatureConstants.Dragon_Green_Ancient][CreatureConstants.Dragon_Green_Ancient] = GetMultiplierFromAverage(80 * 12);
            wingspans[CreatureConstants.Dragon_Green_Wyrm][GenderConstants.Female] = GetBaseFromAverage(80 * 12);
            wingspans[CreatureConstants.Dragon_Green_Wyrm][GenderConstants.Male] = GetBaseFromAverage(80 * 12);
            wingspans[CreatureConstants.Dragon_Green_Wyrm][CreatureConstants.Dragon_Green_Wyrm] = GetMultiplierFromAverage(80 * 12);
            wingspans[CreatureConstants.Dragon_Green_GreatWyrm][GenderConstants.Female] = GetBaseFromAverage(80 * 12);
            wingspans[CreatureConstants.Dragon_Green_GreatWyrm][GenderConstants.Male] = GetBaseFromAverage(80 * 12);
            wingspans[CreatureConstants.Dragon_Green_GreatWyrm][CreatureConstants.Dragon_Green_GreatWyrm] = GetMultiplierFromAverage(80 * 12);
            wingspans[CreatureConstants.Dragon_Red_Wyrmling][GenderConstants.Female] = GetBaseFromAverage(30 * 12);
            wingspans[CreatureConstants.Dragon_Red_Wyrmling][GenderConstants.Male] = GetBaseFromAverage(30 * 12);
            wingspans[CreatureConstants.Dragon_Red_Wyrmling][CreatureConstants.Dragon_Red_Wyrmling] = GetMultiplierFromAverage(30 * 12);
            wingspans[CreatureConstants.Dragon_Red_VeryYoung][GenderConstants.Female] = GetBaseFromAverage(45 * 12);
            wingspans[CreatureConstants.Dragon_Red_VeryYoung][GenderConstants.Male] = GetBaseFromAverage(45 * 12);
            wingspans[CreatureConstants.Dragon_Red_VeryYoung][CreatureConstants.Dragon_Red_VeryYoung] = GetMultiplierFromAverage(45 * 12);
            wingspans[CreatureConstants.Dragon_Red_Young][GenderConstants.Female] = GetBaseFromAverage(45 * 12);
            wingspans[CreatureConstants.Dragon_Red_Young][GenderConstants.Male] = GetBaseFromAverage(45 * 12);
            wingspans[CreatureConstants.Dragon_Red_Young][CreatureConstants.Dragon_Red_Young] = GetMultiplierFromAverage(45 * 12);
            wingspans[CreatureConstants.Dragon_Red_Juvenile][GenderConstants.Female] = GetBaseFromAverage(45 * 12);
            wingspans[CreatureConstants.Dragon_Red_Juvenile][GenderConstants.Male] = GetBaseFromAverage(45 * 12);
            wingspans[CreatureConstants.Dragon_Red_Juvenile][CreatureConstants.Dragon_Red_Juvenile] = GetMultiplierFromAverage(45 * 12);
            wingspans[CreatureConstants.Dragon_Red_YoungAdult][GenderConstants.Female] = GetBaseFromAverage(75 * 12);
            wingspans[CreatureConstants.Dragon_Red_YoungAdult][GenderConstants.Male] = GetBaseFromAverage(75 * 12);
            wingspans[CreatureConstants.Dragon_Red_YoungAdult][CreatureConstants.Dragon_Red_YoungAdult] = GetMultiplierFromAverage(75 * 12);
            wingspans[CreatureConstants.Dragon_Red_Adult][GenderConstants.Female] = GetBaseFromAverage(75 * 12);
            wingspans[CreatureConstants.Dragon_Red_Adult][GenderConstants.Male] = GetBaseFromAverage(75 * 12);
            wingspans[CreatureConstants.Dragon_Red_Adult][CreatureConstants.Dragon_Red_Adult] = GetMultiplierFromAverage(75 * 12);
            wingspans[CreatureConstants.Dragon_Red_MatureAdult][GenderConstants.Female] = GetBaseFromAverage(75 * 12);
            wingspans[CreatureConstants.Dragon_Red_MatureAdult][GenderConstants.Male] = GetBaseFromAverage(75 * 12);
            wingspans[CreatureConstants.Dragon_Red_MatureAdult][CreatureConstants.Dragon_Red_MatureAdult] = GetMultiplierFromAverage(75 * 12);
            wingspans[CreatureConstants.Dragon_Red_Old][GenderConstants.Female] = GetBaseFromAverage(100 * 12);
            wingspans[CreatureConstants.Dragon_Red_Old][GenderConstants.Male] = GetBaseFromAverage(100 * 12);
            wingspans[CreatureConstants.Dragon_Red_Old][CreatureConstants.Dragon_Red_Old] = GetMultiplierFromAverage(100 * 12);
            wingspans[CreatureConstants.Dragon_Red_VeryOld][GenderConstants.Female] = GetBaseFromAverage(100 * 12);
            wingspans[CreatureConstants.Dragon_Red_VeryOld][GenderConstants.Male] = GetBaseFromAverage(100 * 12);
            wingspans[CreatureConstants.Dragon_Red_VeryOld][CreatureConstants.Dragon_Red_VeryOld] = GetMultiplierFromAverage(100 * 12);
            wingspans[CreatureConstants.Dragon_Red_Ancient][GenderConstants.Female] = GetBaseFromAverage(100 * 12);
            wingspans[CreatureConstants.Dragon_Red_Ancient][GenderConstants.Male] = GetBaseFromAverage(100 * 12);
            wingspans[CreatureConstants.Dragon_Red_Ancient][CreatureConstants.Dragon_Red_Ancient] = GetMultiplierFromAverage(100 * 12);
            wingspans[CreatureConstants.Dragon_Red_Wyrm][GenderConstants.Female] = GetBaseFromAverage(100 * 12);
            wingspans[CreatureConstants.Dragon_Red_Wyrm][GenderConstants.Male] = GetBaseFromAverage(100 * 12);
            wingspans[CreatureConstants.Dragon_Red_Wyrm][CreatureConstants.Dragon_Red_Wyrm] = GetMultiplierFromAverage(100 * 12);
            wingspans[CreatureConstants.Dragon_Red_GreatWyrm][GenderConstants.Female] = GetBaseFromAverage(150 * 12);
            wingspans[CreatureConstants.Dragon_Red_GreatWyrm][GenderConstants.Male] = GetBaseFromAverage(150 * 12);
            wingspans[CreatureConstants.Dragon_Red_GreatWyrm][CreatureConstants.Dragon_Red_GreatWyrm] = GetMultiplierFromAverage(150 * 12);
            wingspans[CreatureConstants.Dragon_Silver_Wyrmling][GenderConstants.Female] = GetBaseFromAverage(20 * 12);
            wingspans[CreatureConstants.Dragon_Silver_Wyrmling][GenderConstants.Male] = GetBaseFromAverage(20 * 12);
            wingspans[CreatureConstants.Dragon_Silver_Wyrmling][CreatureConstants.Dragon_Silver_Wyrmling] = GetMultiplierFromAverage(20 * 12);
            wingspans[CreatureConstants.Dragon_Silver_VeryYoung][GenderConstants.Female] = GetBaseFromAverage(30 * 12);
            wingspans[CreatureConstants.Dragon_Silver_VeryYoung][GenderConstants.Male] = GetBaseFromAverage(30 * 12);
            wingspans[CreatureConstants.Dragon_Silver_VeryYoung][CreatureConstants.Dragon_Silver_VeryYoung] = GetMultiplierFromAverage(30 * 12);
            wingspans[CreatureConstants.Dragon_Silver_Young][GenderConstants.Female] = GetBaseFromAverage(30 * 12);
            wingspans[CreatureConstants.Dragon_Silver_Young][GenderConstants.Male] = GetBaseFromAverage(30 * 12);
            wingspans[CreatureConstants.Dragon_Silver_Young][CreatureConstants.Dragon_Silver_Young] = GetMultiplierFromAverage(30 * 12);
            wingspans[CreatureConstants.Dragon_Silver_Juvenile][GenderConstants.Female] = GetBaseFromAverage(45 * 12);
            wingspans[CreatureConstants.Dragon_Silver_Juvenile][GenderConstants.Male] = GetBaseFromAverage(45 * 12);
            wingspans[CreatureConstants.Dragon_Silver_Juvenile][CreatureConstants.Dragon_Silver_Juvenile] = GetMultiplierFromAverage(45 * 12);
            wingspans[CreatureConstants.Dragon_Silver_YoungAdult][GenderConstants.Female] = GetBaseFromAverage(45 * 12);
            wingspans[CreatureConstants.Dragon_Silver_YoungAdult][GenderConstants.Male] = GetBaseFromAverage(45 * 12);
            wingspans[CreatureConstants.Dragon_Silver_YoungAdult][CreatureConstants.Dragon_Silver_YoungAdult] = GetMultiplierFromAverage(45 * 12);
            wingspans[CreatureConstants.Dragon_Silver_Adult][GenderConstants.Female] = GetBaseFromAverage(75 * 12);
            wingspans[CreatureConstants.Dragon_Silver_Adult][GenderConstants.Male] = GetBaseFromAverage(75 * 12);
            wingspans[CreatureConstants.Dragon_Silver_Adult][CreatureConstants.Dragon_Silver_Adult] = GetMultiplierFromAverage(75 * 12);
            wingspans[CreatureConstants.Dragon_Silver_MatureAdult][GenderConstants.Female] = GetBaseFromAverage(75 * 12);
            wingspans[CreatureConstants.Dragon_Silver_MatureAdult][GenderConstants.Male] = GetBaseFromAverage(75 * 12);
            wingspans[CreatureConstants.Dragon_Silver_MatureAdult][CreatureConstants.Dragon_Silver_MatureAdult] = GetMultiplierFromAverage(75 * 12);
            wingspans[CreatureConstants.Dragon_Silver_Old][GenderConstants.Female] = GetBaseFromAverage(75 * 12);
            wingspans[CreatureConstants.Dragon_Silver_Old][GenderConstants.Male] = GetBaseFromAverage(75 * 12);
            wingspans[CreatureConstants.Dragon_Silver_Old][CreatureConstants.Dragon_Silver_Old] = GetMultiplierFromAverage(75 * 12);
            wingspans[CreatureConstants.Dragon_Silver_VeryOld][GenderConstants.Female] = GetBaseFromAverage(75 * 12);
            wingspans[CreatureConstants.Dragon_Silver_VeryOld][GenderConstants.Male] = GetBaseFromAverage(75 * 12);
            wingspans[CreatureConstants.Dragon_Silver_VeryOld][CreatureConstants.Dragon_Silver_VeryOld] = GetMultiplierFromAverage(75 * 12);
            wingspans[CreatureConstants.Dragon_Silver_Ancient][GenderConstants.Female] = GetBaseFromAverage(100 * 12);
            wingspans[CreatureConstants.Dragon_Silver_Ancient][GenderConstants.Male] = GetBaseFromAverage(100 * 12);
            wingspans[CreatureConstants.Dragon_Silver_Ancient][CreatureConstants.Dragon_Silver_Ancient] = GetMultiplierFromAverage(100 * 12);
            wingspans[CreatureConstants.Dragon_Silver_Wyrm][GenderConstants.Female] = GetBaseFromAverage(100 * 12);
            wingspans[CreatureConstants.Dragon_Silver_Wyrm][GenderConstants.Male] = GetBaseFromAverage(100 * 12);
            wingspans[CreatureConstants.Dragon_Silver_Wyrm][CreatureConstants.Dragon_Silver_Wyrm] = GetMultiplierFromAverage(100 * 12);
            wingspans[CreatureConstants.Dragon_Silver_GreatWyrm][GenderConstants.Female] = GetBaseFromAverage(150 * 12);
            wingspans[CreatureConstants.Dragon_Silver_GreatWyrm][GenderConstants.Male] = GetBaseFromAverage(150 * 12);
            wingspans[CreatureConstants.Dragon_Silver_GreatWyrm][CreatureConstants.Dragon_Silver_GreatWyrm] = GetMultiplierFromAverage(150 * 12);
            wingspans[CreatureConstants.Dragon_White_Wyrmling][GenderConstants.Female] = GetBaseFromAverage(7 * 12);
            wingspans[CreatureConstants.Dragon_White_Wyrmling][GenderConstants.Male] = GetBaseFromAverage(7 * 12);
            wingspans[CreatureConstants.Dragon_White_Wyrmling][CreatureConstants.Dragon_White_Wyrmling] = GetMultiplierFromAverage(7 * 12);
            wingspans[CreatureConstants.Dragon_White_VeryYoung][GenderConstants.Female] = GetBaseFromAverage(14 * 12);
            wingspans[CreatureConstants.Dragon_White_VeryYoung][GenderConstants.Male] = GetBaseFromAverage(14 * 12);
            wingspans[CreatureConstants.Dragon_White_VeryYoung][CreatureConstants.Dragon_White_VeryYoung] = GetMultiplierFromAverage(14 * 12);
            wingspans[CreatureConstants.Dragon_White_Young][GenderConstants.Female] = GetBaseFromAverage(21 * 12);
            wingspans[CreatureConstants.Dragon_White_Young][GenderConstants.Male] = GetBaseFromAverage(21 * 12);
            wingspans[CreatureConstants.Dragon_White_Young][CreatureConstants.Dragon_White_Young] = GetMultiplierFromAverage(21 * 12);
            wingspans[CreatureConstants.Dragon_White_Juvenile][GenderConstants.Female] = GetBaseFromAverage(21 * 12);
            wingspans[CreatureConstants.Dragon_White_Juvenile][GenderConstants.Male] = GetBaseFromAverage(21 * 12);
            wingspans[CreatureConstants.Dragon_White_Juvenile][CreatureConstants.Dragon_White_Juvenile] = GetMultiplierFromAverage(21 * 12);
            wingspans[CreatureConstants.Dragon_White_YoungAdult][GenderConstants.Female] = GetBaseFromAverage(32 * 12);
            wingspans[CreatureConstants.Dragon_White_YoungAdult][GenderConstants.Male] = GetBaseFromAverage(32 * 12);
            wingspans[CreatureConstants.Dragon_White_YoungAdult][CreatureConstants.Dragon_White_YoungAdult] = GetMultiplierFromAverage(32 * 12);
            wingspans[CreatureConstants.Dragon_White_Adult][GenderConstants.Female] = GetBaseFromAverage(32 * 12);
            wingspans[CreatureConstants.Dragon_White_Adult][GenderConstants.Male] = GetBaseFromAverage(32 * 12);
            wingspans[CreatureConstants.Dragon_White_Adult][CreatureConstants.Dragon_White_Adult] = GetMultiplierFromAverage(32 * 12);
            wingspans[CreatureConstants.Dragon_White_MatureAdult][GenderConstants.Female] = GetBaseFromAverage(55 * 12);
            wingspans[CreatureConstants.Dragon_White_MatureAdult][GenderConstants.Male] = GetBaseFromAverage(55 * 12);
            wingspans[CreatureConstants.Dragon_White_MatureAdult][CreatureConstants.Dragon_White_MatureAdult] = GetMultiplierFromAverage(55 * 12);
            wingspans[CreatureConstants.Dragon_White_Old][GenderConstants.Female] = GetBaseFromAverage(55 * 12);
            wingspans[CreatureConstants.Dragon_White_Old][GenderConstants.Male] = GetBaseFromAverage(55 * 12);
            wingspans[CreatureConstants.Dragon_White_Old][CreatureConstants.Dragon_White_Old] = GetMultiplierFromAverage(55 * 12);
            wingspans[CreatureConstants.Dragon_White_VeryOld][GenderConstants.Female] = GetBaseFromAverage(55 * 12);
            wingspans[CreatureConstants.Dragon_White_VeryOld][GenderConstants.Male] = GetBaseFromAverage(55 * 12);
            wingspans[CreatureConstants.Dragon_White_VeryOld][CreatureConstants.Dragon_White_VeryOld] = GetMultiplierFromAverage(55 * 12);
            wingspans[CreatureConstants.Dragon_White_Ancient][GenderConstants.Female] = GetBaseFromAverage(55 * 12);
            wingspans[CreatureConstants.Dragon_White_Ancient][GenderConstants.Male] = GetBaseFromAverage(55 * 12);
            wingspans[CreatureConstants.Dragon_White_Ancient][CreatureConstants.Dragon_White_Ancient] = GetMultiplierFromAverage(55 * 12);
            wingspans[CreatureConstants.Dragon_White_Wyrm][GenderConstants.Female] = GetBaseFromAverage(72 * 12);
            wingspans[CreatureConstants.Dragon_White_Wyrm][GenderConstants.Male] = GetBaseFromAverage(72 * 12);
            wingspans[CreatureConstants.Dragon_White_Wyrm][CreatureConstants.Dragon_White_Wyrm] = GetMultiplierFromAverage(72 * 12);
            wingspans[CreatureConstants.Dragon_White_GreatWyrm][GenderConstants.Female] = GetBaseFromAverage(72 * 12);
            wingspans[CreatureConstants.Dragon_White_GreatWyrm][GenderConstants.Male] = GetBaseFromAverage(72 * 12);
            wingspans[CreatureConstants.Dragon_White_GreatWyrm][CreatureConstants.Dragon_White_GreatWyrm] = GetMultiplierFromAverage(72 * 12);
            wingspans[CreatureConstants.DragonTurtle][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.DragonTurtle][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.DragonTurtle][CreatureConstants.DragonTurtle] = "0";
            //Source: https://forgottenrealms.fandom.com/wiki/Dragonne "smaller version of wings of Brass Dragons". Dragonne are large, so use medium brass
            wingspans[CreatureConstants.Dragonne][GenderConstants.Female] = GetBaseFromAverage(18 * 12);
            wingspans[CreatureConstants.Dragonne][GenderConstants.Male] = GetBaseFromAverage(18 * 12);
            wingspans[CreatureConstants.Dragonne][CreatureConstants.Dragonne] = GetMultiplierFromAverage(18 * 12);
            wingspans[CreatureConstants.Dretch][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.Dretch][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Dretch][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Dretch][CreatureConstants.Dretch] = "0";
            wingspans[CreatureConstants.Drider][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.Drider][CreatureConstants.Drider] = "0";
            wingspans[CreatureConstants.Dryad][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Dryad][CreatureConstants.Dryad] = "0";
            wingspans[CreatureConstants.Dwarf_Deep][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Dwarf_Deep][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Dwarf_Deep][CreatureConstants.Dwarf_Deep] = "0";
            wingspans[CreatureConstants.Dwarf_Duergar][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Dwarf_Duergar][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Dwarf_Duergar][CreatureConstants.Dwarf_Duergar] = "0";
            wingspans[CreatureConstants.Dwarf_Hill][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Dwarf_Hill][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Dwarf_Hill][CreatureConstants.Dwarf_Hill] = "0";
            wingspans[CreatureConstants.Dwarf_Mountain][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Dwarf_Mountain][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Dwarf_Mountain][CreatureConstants.Dwarf_Mountain] = "0";
            //Source: https://www.d20srd.org/srd/monsters/eagle.htm
            wingspans[CreatureConstants.Eagle][GenderConstants.Female] = GetBaseFromAverage(7 * 12);
            wingspans[CreatureConstants.Eagle][GenderConstants.Male] = GetBaseFromAverage(7 * 12);
            wingspans[CreatureConstants.Eagle][CreatureConstants.Eagle] = GetMultiplierFromAverage(7 * 12);
            //Source: https://www.d20srd.org/srd/monsters/eagleGiant.htm
            wingspans[CreatureConstants.Eagle_Giant][GenderConstants.Female] = GetBaseFromUpTo(20 * 12);
            wingspans[CreatureConstants.Eagle_Giant][GenderConstants.Male] = GetBaseFromUpTo(20 * 12);
            wingspans[CreatureConstants.Eagle_Giant][CreatureConstants.Eagle_Giant] = GetMultiplierFromUpTo(20 * 12);
            wingspans[CreatureConstants.Efreeti][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.Efreeti][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Efreeti][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Efreeti][CreatureConstants.Efreeti] = "0";
            wingspans[CreatureConstants.Elasmosaurus][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Elasmosaurus][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Elasmosaurus][CreatureConstants.Elasmosaurus] = "0";
            wingspans[CreatureConstants.Elemental_Air_Small][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.Elemental_Air_Small][CreatureConstants.Elemental_Air_Small] = "0";
            wingspans[CreatureConstants.Elemental_Air_Medium][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.Elemental_Air_Medium][CreatureConstants.Elemental_Air_Medium] = "0";
            wingspans[CreatureConstants.Elemental_Air_Large][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.Elemental_Air_Large][CreatureConstants.Elemental_Air_Large] = "0";
            wingspans[CreatureConstants.Elemental_Air_Huge][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.Elemental_Air_Huge][CreatureConstants.Elemental_Air_Huge] = "0";
            wingspans[CreatureConstants.Elemental_Air_Greater][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.Elemental_Air_Greater][CreatureConstants.Elemental_Air_Greater] = "0";
            wingspans[CreatureConstants.Elemental_Air_Elder][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.Elemental_Air_Elder][CreatureConstants.Elemental_Air_Elder] = "0";
            wingspans[CreatureConstants.Elemental_Earth_Small][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.Elemental_Earth_Small][CreatureConstants.Elemental_Earth_Small] = "0";
            wingspans[CreatureConstants.Elemental_Earth_Medium][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.Elemental_Earth_Medium][CreatureConstants.Elemental_Earth_Medium] = "0";
            wingspans[CreatureConstants.Elemental_Earth_Large][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.Elemental_Earth_Large][CreatureConstants.Elemental_Earth_Large] = "0";
            wingspans[CreatureConstants.Elemental_Earth_Huge][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.Elemental_Earth_Huge][CreatureConstants.Elemental_Earth_Huge] = "0";
            wingspans[CreatureConstants.Elemental_Earth_Greater][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.Elemental_Earth_Greater][CreatureConstants.Elemental_Earth_Greater] = "0";
            wingspans[CreatureConstants.Elemental_Earth_Elder][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.Elemental_Earth_Elder][CreatureConstants.Elemental_Earth_Elder] = "0";
            wingspans[CreatureConstants.Elemental_Fire_Small][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.Elemental_Fire_Small][CreatureConstants.Elemental_Fire_Small] = "0";
            wingspans[CreatureConstants.Elemental_Fire_Medium][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.Elemental_Fire_Medium][CreatureConstants.Elemental_Fire_Medium] = "0";
            wingspans[CreatureConstants.Elemental_Fire_Large][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.Elemental_Fire_Large][CreatureConstants.Elemental_Fire_Large] = "0";
            wingspans[CreatureConstants.Elemental_Fire_Huge][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.Elemental_Fire_Huge][CreatureConstants.Elemental_Fire_Huge] = "0";
            wingspans[CreatureConstants.Elemental_Fire_Greater][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.Elemental_Fire_Greater][CreatureConstants.Elemental_Fire_Greater] = "0";
            wingspans[CreatureConstants.Elemental_Fire_Elder][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.Elemental_Fire_Elder][CreatureConstants.Elemental_Fire_Elder] = "0";
            wingspans[CreatureConstants.Elemental_Water_Small][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.Elemental_Water_Small][CreatureConstants.Elemental_Water_Small] = "0";
            wingspans[CreatureConstants.Elemental_Water_Medium][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.Elemental_Water_Medium][CreatureConstants.Elemental_Water_Medium] = "0";
            wingspans[CreatureConstants.Elemental_Water_Large][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.Elemental_Water_Large][CreatureConstants.Elemental_Water_Large] = "0";
            wingspans[CreatureConstants.Elemental_Water_Huge][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.Elemental_Water_Huge][CreatureConstants.Elemental_Water_Huge] = "0";
            wingspans[CreatureConstants.Elemental_Water_Greater][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.Elemental_Water_Greater][CreatureConstants.Elemental_Water_Greater] = "0";
            wingspans[CreatureConstants.Elemental_Water_Elder][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.Elemental_Water_Elder][CreatureConstants.Elemental_Water_Elder] = "0";
            wingspans[CreatureConstants.Elephant][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Elephant][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Elephant][CreatureConstants.Elephant] = "0";
            wingspans[CreatureConstants.Elf_Aquatic][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Elf_Aquatic][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Elf_Aquatic][CreatureConstants.Elf_Aquatic] = "0";
            wingspans[CreatureConstants.Elf_Drow][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Elf_Drow][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Elf_Drow][CreatureConstants.Elf_Drow] = "0";
            wingspans[CreatureConstants.Elf_Gray][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Elf_Gray][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Elf_Gray][CreatureConstants.Elf_Gray] = "0";
            wingspans[CreatureConstants.Elf_Half][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Elf_Half][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Elf_Half][CreatureConstants.Elf_Half] = "0";
            wingspans[CreatureConstants.Elf_High][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Elf_High][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Elf_High][CreatureConstants.Elf_High] = "0";
            wingspans[CreatureConstants.Elf_Wild][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Elf_Wild][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Elf_Wild][CreatureConstants.Elf_Wild] = "0";
            wingspans[CreatureConstants.Elf_Wood][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Elf_Wood][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Elf_Wood][CreatureConstants.Elf_Wood] = "0";
            //Source: https://www.d20srd.org/srd/monsters/devil.htm#erinyes
            wingspans[CreatureConstants.Erinyes][GenderConstants.Female] = GetBaseFromAverage(6 * 12);
            wingspans[CreatureConstants.Erinyes][GenderConstants.Male] = GetBaseFromAverage(6 * 12);
            wingspans[CreatureConstants.Erinyes][CreatureConstants.Erinyes] = GetMultiplierFromAverage(6 * 12);
            wingspans[CreatureConstants.EtherealFilcher][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.EtherealFilcher][CreatureConstants.EtherealFilcher] = "0";
            wingspans[CreatureConstants.EtherealMarauder][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.EtherealMarauder][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.EtherealMarauder][CreatureConstants.EtherealMarauder] = "0";
            //Source: https://syrikdarkenedskies.obsidianportal.com/wikis/ettercap-race
            wingspans[CreatureConstants.Ettercap][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Ettercap][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Ettercap][CreatureConstants.Ettin] = "0";
            wingspans[CreatureConstants.Ettin][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Ettin][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Ettin][CreatureConstants.Ettin] = "0";
            wingspans[CreatureConstants.FireBeetle_Giant][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.FireBeetle_Giant][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.FireBeetle_Giant][CreatureConstants.FireBeetle_Giant] = "0";
            wingspans[CreatureConstants.FormianWorker][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.FormianWorker][CreatureConstants.FormianWorker] = "0";
            wingspans[CreatureConstants.FormianWarrior][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.FormianWarrior][CreatureConstants.FormianWarrior] = "0";
            wingspans[CreatureConstants.FormianTaskmaster][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.FormianTaskmaster][CreatureConstants.FormianTaskmaster] = "0";
            wingspans[CreatureConstants.FormianMyrmarch][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.FormianMyrmarch][CreatureConstants.FormianMyrmarch] = "0";
            wingspans[CreatureConstants.FormianQueen][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.FormianQueen][CreatureConstants.FormianQueen] = "0";
            wingspans[CreatureConstants.FrostWorm][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.FrostWorm][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.FrostWorm][CreatureConstants.FrostWorm] = "0";
            //Using height
            wingspans[CreatureConstants.Gargoyle][GenderConstants.Agender] = "5*12";
            wingspans[CreatureConstants.Gargoyle][CreatureConstants.Gargoyle] = "2d10";
            wingspans[CreatureConstants.Gargoyle_Kapoacinth][GenderConstants.Agender] = "5*12";
            wingspans[CreatureConstants.Gargoyle_Kapoacinth][CreatureConstants.Gargoyle_Kapoacinth] = "2d10";
            wingspans[CreatureConstants.GelatinousCube][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.GelatinousCube][CreatureConstants.GelatinousCube] = "0";
            wingspans[CreatureConstants.Ghaele][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Ghaele][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Ghaele][CreatureConstants.Ghaele] = "0";
            wingspans[CreatureConstants.Ghoul][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Ghoul][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Ghoul][CreatureConstants.Ghoul] = "0";
            wingspans[CreatureConstants.Ghoul_Ghast][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Ghoul_Ghast][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Ghoul_Ghast][CreatureConstants.Ghoul_Ghast] = "0";
            wingspans[CreatureConstants.Ghoul_Lacedon][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Ghoul_Lacedon][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Ghoul_Lacedon][CreatureConstants.Ghoul_Lacedon] = "0";
            //Source: https://forgottenrealms.fandom.com/wiki/Cloud_giant
            wingspans[CreatureConstants.Giant_Cloud][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Giant_Cloud][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Giant_Cloud][CreatureConstants.Giant_Cloud] = "0";
            wingspans[CreatureConstants.Giant_Fire][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Giant_Fire][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Giant_Fire][CreatureConstants.Giant_Fire] = "0";
            wingspans[CreatureConstants.Giant_Frost][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Giant_Frost][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Giant_Frost][CreatureConstants.Giant_Frost] = "0";
            wingspans[CreatureConstants.Giant_Hill][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Giant_Hill][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Giant_Hill][CreatureConstants.Giant_Hill] = "0";
            wingspans[CreatureConstants.GibberingMouther][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.GibberingMouther][CreatureConstants.GibberingMouther] = "0";
            wingspans[CreatureConstants.Girallon][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Girallon][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Girallon][CreatureConstants.Girallon] = "0";
            wingspans[CreatureConstants.Glabrezu][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.Glabrezu][CreatureConstants.Glabrezu] = "0";
            wingspans[CreatureConstants.Gnoll][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Gnoll][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Gnoll][CreatureConstants.Gnoll] = "0";
            wingspans[CreatureConstants.Gnome_Forest][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Gnome_Forest][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Gnome_Forest][CreatureConstants.Gnome_Forest] = "0";
            wingspans[CreatureConstants.Gnome_Rock][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Gnome_Rock][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Gnome_Rock][CreatureConstants.Gnome_Rock] = "0";
            wingspans[CreatureConstants.Gnome_Svirfneblin][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Gnome_Svirfneblin][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Gnome_Svirfneblin][CreatureConstants.Gnome_Svirfneblin] = "0";
            wingspans[CreatureConstants.Goblin][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Goblin][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Goblin][CreatureConstants.Goblin] = "0";
            wingspans[CreatureConstants.Golem_Clay][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.Golem_Clay][CreatureConstants.Golem_Clay] = "0";
            wingspans[CreatureConstants.Golem_Flesh][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.Golem_Flesh][CreatureConstants.Golem_Flesh] = "0";
            wingspans[CreatureConstants.Golem_Iron][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.Golem_Iron][CreatureConstants.Golem_Iron] = "0";
            wingspans[CreatureConstants.Golem_Stone][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.Golem_Stone][CreatureConstants.Golem_Stone] = "0";
            wingspans[CreatureConstants.Golem_Stone_Greater][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.Golem_Stone_Greater][CreatureConstants.Golem_Stone_Greater] = "0";
            wingspans[CreatureConstants.Gorgon][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Gorgon][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Gorgon][CreatureConstants.Gorgon] = "0";
            wingspans[CreatureConstants.GrayOoze][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.GrayOoze][CreatureConstants.GrayOoze] = "0";
            wingspans[CreatureConstants.GrayRender][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.GrayRender][CreatureConstants.GrayRender] = "0";
            wingspans[CreatureConstants.GreenHag][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.GreenHag][CreatureConstants.GreenHag] = "0";
            //Source: https://www.d20srd.org/srd/monsters/grick.htm
            wingspans[CreatureConstants.Grick][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Grick][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Grick][CreatureConstants.Grick] = "0";
            //Source: https://www.d20srd.org/srd/monsters/griffon.htm
            wingspans[CreatureConstants.Griffon][GenderConstants.Female] = GetBaseFromAtLeast(25 * 12);
            wingspans[CreatureConstants.Griffon][GenderConstants.Male] = GetBaseFromAtLeast(25 * 12);
            wingspans[CreatureConstants.Griffon][CreatureConstants.Griffon] = GetMultiplierFromAtLeast(25 * 12);
            wingspans[CreatureConstants.Grig][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Grig][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Grig][CreatureConstants.Grig] = "0";
            wingspans[CreatureConstants.Grig_WithFiddle][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Grig_WithFiddle][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Grig_WithFiddle][CreatureConstants.Grig_WithFiddle] = "0";
            wingspans[CreatureConstants.Grimlock][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Grimlock][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Grimlock][CreatureConstants.Grimlock] = "0";
            //Source: https://www.d20srd.org/srd/monsters/sphinx.htm
            wingspans[CreatureConstants.Gynosphinx][GenderConstants.Female] = GetBaseFromAverage(10 * 12);
            wingspans[CreatureConstants.Gynosphinx][CreatureConstants.Gynosphinx] = GetMultiplierFromAverage(10 * 12);
            wingspans[CreatureConstants.Halfling_Deep][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Halfling_Deep][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Halfling_Deep][CreatureConstants.Halfling_Deep] = "0";
            wingspans[CreatureConstants.Halfling_Lightfoot][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Halfling_Lightfoot][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Halfling_Lightfoot][CreatureConstants.Halfling_Lightfoot] = "0";
            wingspans[CreatureConstants.Halfling_Tallfellow][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Halfling_Tallfellow][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Halfling_Tallfellow][CreatureConstants.Halfling_Tallfellow] = "0";
            //Source: https://www.5esrd.com/database/race/harpy/ Assuming same as height
            wingspans[CreatureConstants.Harpy][GenderConstants.Female] = "4*12+5";
            wingspans[CreatureConstants.Harpy][GenderConstants.Male] = "4*12+10";
            wingspans[CreatureConstants.Harpy][CreatureConstants.Harpy] = "2d10";
            //Source: https://www.d20srd.org/srd/monsters/hawk.htm
            wingspans[CreatureConstants.Hawk][GenderConstants.Female] = GetBaseFromUpTo(6 * 12);
            wingspans[CreatureConstants.Hawk][GenderConstants.Male] = GetBaseFromUpTo(6 * 12);
            wingspans[CreatureConstants.Hawk][CreatureConstants.Hawk] = GetMultiplierFromUpTo(6 * 12);
            wingspans[CreatureConstants.HellHound][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.HellHound][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.HellHound][CreatureConstants.HellHound] = "0";
            wingspans[CreatureConstants.HellHound_NessianWarhound][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.HellHound_NessianWarhound][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.HellHound_NessianWarhound][CreatureConstants.HellHound_NessianWarhound] = "0";
            wingspans[CreatureConstants.Hellcat_Bezekira][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Hellcat_Bezekira][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Hellcat_Bezekira][CreatureConstants.Hellcat_Bezekira] = "0";
            wingspans[CreatureConstants.Hellwasp_Swarm][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.Hellwasp_Swarm][CreatureConstants.Hellwasp_Swarm] = "0";
            wingspans[CreatureConstants.Hezrou][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.Hezrou][CreatureConstants.Hezrou] = "0";
            //Source: https://www.d20srd.org/srd/monsters/sphinx.htm
            wingspans[CreatureConstants.Hieracosphinx][GenderConstants.Male] = GetBaseFromAverage(10 * 12);
            wingspans[CreatureConstants.Hieracosphinx][CreatureConstants.Hieracosphinx] = GetMultiplierFromAverage(10 * 12);
            //Source: https://www.d20srd.org/srd/monsters/hippogriff.htm
            wingspans[CreatureConstants.Hippogriff][GenderConstants.Female] = GetBaseFromAverage(20 * 12);
            wingspans[CreatureConstants.Hippogriff][GenderConstants.Male] = GetBaseFromAverage(20 * 12);
            wingspans[CreatureConstants.Hippogriff][CreatureConstants.Hippogriff] = GetMultiplierFromAverage(20 * 12);
            wingspans[CreatureConstants.Hobgoblin][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Hobgoblin][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Hobgoblin][CreatureConstants.Hobgoblin] = "0";
            //Source: https://forgottenrealms.fandom.com/wiki/Homunculus
            //https://www.dimensions.com/element/eastern-gray-squirrel
            wingspans[CreatureConstants.Homunculus][GenderConstants.Agender] = GetBaseFromRange(8, 11);
            wingspans[CreatureConstants.Homunculus][CreatureConstants.Homunculus] = GetMultiplierFromRange(8, 11);
            //Source: https://forgottenrealms.fandom.com/wiki/Cornugon Wings are massive, so we will say triple their height
            wingspans[CreatureConstants.HornedDevil_Cornugon][GenderConstants.Agender] = GetBaseFromAverage(3 * 9 * 12);
            wingspans[CreatureConstants.HornedDevil_Cornugon][CreatureConstants.HornedDevil_Cornugon] = GetMultiplierFromAverage(3 * 9 * 12);
            wingspans[CreatureConstants.Horse_Heavy][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Horse_Heavy][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Horse_Heavy][CreatureConstants.Horse_Heavy] = "0";
            wingspans[CreatureConstants.Horse_Light][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Horse_Light][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Horse_Light][CreatureConstants.Horse_Light] = "0";
            wingspans[CreatureConstants.Horse_Heavy_War][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Horse_Heavy_War][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Horse_Heavy_War][CreatureConstants.Horse_Heavy_War] = "0";
            wingspans[CreatureConstants.Horse_Light_War][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Horse_Light_War][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Horse_Light_War][CreatureConstants.Horse_Light_War] = "0";
            wingspans[CreatureConstants.HoundArchon][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.HoundArchon][CreatureConstants.HoundArchon] = "0";
            wingspans[CreatureConstants.Howler][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.Howler][CreatureConstants.Howler] = "0";
            wingspans[CreatureConstants.Human][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Human][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Human][CreatureConstants.Human] = "0";
            wingspans[CreatureConstants.Hydra_5Heads][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Hydra_5Heads][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Hydra_5Heads][CreatureConstants.Hydra_5Heads] = "0";
            wingspans[CreatureConstants.Hydra_6Heads][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Hydra_6Heads][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Hydra_6Heads][CreatureConstants.Hydra_6Heads] = "0";
            wingspans[CreatureConstants.Hydra_7Heads][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Hydra_7Heads][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Hydra_7Heads][CreatureConstants.Hydra_7Heads] = "0";
            wingspans[CreatureConstants.Hydra_8Heads][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Hydra_8Heads][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Hydra_8Heads][CreatureConstants.Hydra_8Heads] = "0";
            wingspans[CreatureConstants.Hydra_9Heads][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Hydra_9Heads][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Hydra_9Heads][CreatureConstants.Hydra_9Heads] = "0";
            wingspans[CreatureConstants.Hydra_10Heads][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Hydra_10Heads][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Hydra_10Heads][CreatureConstants.Hydra_10Heads] = "0";
            wingspans[CreatureConstants.Hydra_11Heads][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Hydra_11Heads][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Hydra_11Heads][CreatureConstants.Hydra_11Heads] = "0";
            wingspans[CreatureConstants.Hydra_12Heads][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Hydra_12Heads][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Hydra_12Heads][CreatureConstants.Hydra_12Heads] = "0";
            wingspans[CreatureConstants.Hyena][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Hyena][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Hyena][CreatureConstants.Hyena] = "0";
            wingspans[CreatureConstants.IceDevil_Gelugon][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.IceDevil_Gelugon][CreatureConstants.IceDevil_Gelugon] = "0";
            wingspans[CreatureConstants.Imp][GenderConstants.Agender] = GetBaseFromAverage(2 * 12);
            wingspans[CreatureConstants.Imp][GenderConstants.Female] = GetBaseFromAverage(2 * 12);
            wingspans[CreatureConstants.Imp][GenderConstants.Male] = GetBaseFromAverage(2 * 12);
            wingspans[CreatureConstants.Imp][CreatureConstants.Imp] = GetMultiplierFromAverage(2 * 12);
            wingspans[CreatureConstants.InvisibleStalker][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.InvisibleStalker][CreatureConstants.InvisibleStalker] = "0";
            wingspans[CreatureConstants.Kobold][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Kobold][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Kobold][CreatureConstants.Kobold] = "0";
            wingspans[CreatureConstants.Kolyarut][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.Kolyarut][CreatureConstants.Kolyarut] = "0";
            wingspans[CreatureConstants.Kraken][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Kraken][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Kraken][CreatureConstants.Kraken] = "0";
            //Source: https://www.d20srd.org/srd/monsters/krenshar.htm
            wingspans[CreatureConstants.Krenshar][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Krenshar][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Krenshar][CreatureConstants.Krenshar] = "0";
            wingspans[CreatureConstants.KuoToa][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.KuoToa][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.KuoToa][CreatureConstants.KuoToa] = "0";
            wingspans[CreatureConstants.Lamia][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Lamia][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Lamia][CreatureConstants.Lamia] = "0";
            //Source: https://forgottenrealms.fandom.com/wiki/Lammasu
            //https://www.d20srd.org/srd/monsters/eagleGiant.htm
            wingspans[CreatureConstants.Lammasu][GenderConstants.Female] = GetBaseFromUpTo(20 * 12);
            wingspans[CreatureConstants.Lammasu][GenderConstants.Male] = GetBaseFromUpTo(20 * 12);
            wingspans[CreatureConstants.Lammasu][CreatureConstants.Lammasu] = GetMultiplierFromUpTo(20 * 12);
            wingspans[CreatureConstants.LanternArchon][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.LanternArchon][CreatureConstants.LanternArchon] = "0";
            wingspans[CreatureConstants.Lemure][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.Lemure][CreatureConstants.Lemure] = "0";
            wingspans[CreatureConstants.Leonal][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Leonal][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Leonal][CreatureConstants.Leonal] = "0";
            wingspans[CreatureConstants.Leopard][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Leopard][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Leopard][CreatureConstants.Leopard] = "0";
            wingspans[CreatureConstants.Lion][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Lion][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Lion][CreatureConstants.Lion] = "0";
            wingspans[CreatureConstants.Lion_Dire][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Lion_Dire][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Lion_Dire][CreatureConstants.Lion_Dire] = "0";
            wingspans[CreatureConstants.Lizardfolk][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Lizardfolk][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Lizardfolk][CreatureConstants.Lizardfolk] = "0";
            wingspans[CreatureConstants.Locathah][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Locathah][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Locathah][CreatureConstants.Locathah] = "0";
            //Source: https://www.d20srd.org/srd/monsters/swarm.htm
            wingspans[CreatureConstants.Locust_Swarm][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.Locust_Swarm][CreatureConstants.Locust_Swarm] = "0";
            wingspans[CreatureConstants.Marilith][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Marilith][CreatureConstants.Marilith] = "0";
            wingspans[CreatureConstants.Marut][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.Marut][CreatureConstants.Marut] = "0";
            //Source: https://forgottenrealms.fandom.com/wiki/Mephit
            wingspans[CreatureConstants.Mephit_Air][GenderConstants.Agender] = GetBaseFromAverage(4 * 12);
            wingspans[CreatureConstants.Mephit_Air][GenderConstants.Female] = GetBaseFromAverage(4 * 12);
            wingspans[CreatureConstants.Mephit_Air][GenderConstants.Male] = GetBaseFromAverage(4 * 12);
            wingspans[CreatureConstants.Mephit_Air][CreatureConstants.Mephit_Air] = GetMultiplierFromAverage(4 * 12);
            wingspans[CreatureConstants.Mephit_Dust][GenderConstants.Agender] = GetBaseFromAverage(4 * 12);
            wingspans[CreatureConstants.Mephit_Dust][GenderConstants.Female] = GetBaseFromAverage(4 * 12);
            wingspans[CreatureConstants.Mephit_Dust][GenderConstants.Male] = GetBaseFromAverage(4 * 12);
            wingspans[CreatureConstants.Mephit_Dust][CreatureConstants.Mephit_Dust] = GetMultiplierFromAverage(4 * 12);
            wingspans[CreatureConstants.Mephit_Earth][GenderConstants.Agender] = GetBaseFromAverage(4 * 12);
            wingspans[CreatureConstants.Mephit_Earth][GenderConstants.Female] = GetBaseFromAverage(4 * 12);
            wingspans[CreatureConstants.Mephit_Earth][GenderConstants.Male] = GetBaseFromAverage(4 * 12);
            wingspans[CreatureConstants.Mephit_Earth][CreatureConstants.Mephit_Earth] = GetMultiplierFromAverage(4 * 12);
            wingspans[CreatureConstants.Mephit_Fire][GenderConstants.Agender] = GetBaseFromAverage(4 * 12);
            wingspans[CreatureConstants.Mephit_Fire][GenderConstants.Female] = GetBaseFromAverage(4 * 12);
            wingspans[CreatureConstants.Mephit_Fire][GenderConstants.Male] = GetBaseFromAverage(4 * 12);
            wingspans[CreatureConstants.Mephit_Fire][CreatureConstants.Mephit_Fire] = GetMultiplierFromAverage(4 * 12);
            wingspans[CreatureConstants.Mephit_Ice][GenderConstants.Agender] = GetBaseFromAverage(4 * 12);
            wingspans[CreatureConstants.Mephit_Ice][GenderConstants.Female] = GetBaseFromAverage(4 * 12);
            wingspans[CreatureConstants.Mephit_Ice][GenderConstants.Male] = GetBaseFromAverage(4 * 12);
            wingspans[CreatureConstants.Mephit_Ice][CreatureConstants.Mephit_Ice] = GetMultiplierFromAverage(4 * 12);
            wingspans[CreatureConstants.Mephit_Magma][GenderConstants.Agender] = GetBaseFromAverage(4 * 12);
            wingspans[CreatureConstants.Mephit_Magma][GenderConstants.Female] = GetBaseFromAverage(4 * 12);
            wingspans[CreatureConstants.Mephit_Magma][GenderConstants.Male] = GetBaseFromAverage(4 * 12);
            wingspans[CreatureConstants.Mephit_Magma][CreatureConstants.Mephit_Magma] = GetMultiplierFromAverage(4 * 12);
            wingspans[CreatureConstants.Mephit_Ooze][GenderConstants.Agender] = GetBaseFromAverage(4 * 12);
            wingspans[CreatureConstants.Mephit_Ooze][GenderConstants.Female] = GetBaseFromAverage(4 * 12);
            wingspans[CreatureConstants.Mephit_Ooze][GenderConstants.Male] = GetBaseFromAverage(4 * 12);
            wingspans[CreatureConstants.Mephit_Ooze][CreatureConstants.Mephit_Ooze] = GetMultiplierFromAverage(4 * 12);
            wingspans[CreatureConstants.Mephit_Salt][GenderConstants.Agender] = GetBaseFromAverage(4 * 12);
            wingspans[CreatureConstants.Mephit_Salt][GenderConstants.Female] = GetBaseFromAverage(4 * 12);
            wingspans[CreatureConstants.Mephit_Salt][GenderConstants.Male] = GetBaseFromAverage(4 * 12);
            wingspans[CreatureConstants.Mephit_Salt][CreatureConstants.Mephit_Salt] = GetMultiplierFromAverage(4 * 12);
            wingspans[CreatureConstants.Mephit_Steam][GenderConstants.Agender] = GetBaseFromAverage(4 * 12);
            wingspans[CreatureConstants.Mephit_Steam][GenderConstants.Female] = GetBaseFromAverage(4 * 12);
            wingspans[CreatureConstants.Mephit_Steam][GenderConstants.Male] = GetBaseFromAverage(4 * 12);
            wingspans[CreatureConstants.Mephit_Steam][CreatureConstants.Mephit_Steam] = GetMultiplierFromAverage(4 * 12);
            wingspans[CreatureConstants.Mephit_Water][GenderConstants.Agender] = GetBaseFromAverage(4 * 12);
            wingspans[CreatureConstants.Mephit_Water][GenderConstants.Female] = GetBaseFromAverage(4 * 12);
            wingspans[CreatureConstants.Mephit_Water][GenderConstants.Male] = GetBaseFromAverage(4 * 12);
            wingspans[CreatureConstants.Mephit_Water][CreatureConstants.Mephit_Water] = GetMultiplierFromAverage(4 * 12);
            wingspans[CreatureConstants.Merfolk][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Merfolk][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Merfolk][CreatureConstants.Merfolk] = "0";
            wingspans[CreatureConstants.Minotaur][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Minotaur][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Minotaur][CreatureConstants.Minotaur] = "0";
            wingspans[CreatureConstants.Naga_Dark][GenderConstants.Hermaphrodite] = "0";
            wingspans[CreatureConstants.Naga_Dark][CreatureConstants.Naga_Dark] = "0";
            wingspans[CreatureConstants.Naga_Guardian][GenderConstants.Hermaphrodite] = "0";
            wingspans[CreatureConstants.Naga_Guardian][CreatureConstants.Naga_Guardian] = "0";
            wingspans[CreatureConstants.Naga_Spirit][GenderConstants.Hermaphrodite] = "0";
            wingspans[CreatureConstants.Naga_Spirit][CreatureConstants.Naga_Spirit] = "0";
            wingspans[CreatureConstants.Naga_Water][GenderConstants.Hermaphrodite] = "0";
            wingspans[CreatureConstants.Naga_Water][CreatureConstants.Naga_Water] = "0";
            //Source: https://forgottenrealms.fandom.com/wiki/Nalfeshnee Wings are "greatly undersized", so we will say 1/3
            wingspans[CreatureConstants.Nalfeshnee][GenderConstants.Agender] = GetBaseFromRange(10 * 12 / 3, 20 * 12 / 3);
            wingspans[CreatureConstants.Nalfeshnee][CreatureConstants.Nalfeshnee] = GetMultiplierFromRange(10 * 12 / 3, 20 * 12 / 3);
            wingspans[CreatureConstants.Octopus_Giant][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Octopus_Giant][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Octopus_Giant][CreatureConstants.Octopus_Giant] = "0";
            wingspans[CreatureConstants.Ogre][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Ogre][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Ogre][CreatureConstants.Ogre] = "0";
            wingspans[CreatureConstants.Ogre_Merrow][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Ogre_Merrow][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Ogre_Merrow][CreatureConstants.Ogre] = "0";
            wingspans[CreatureConstants.OgreMage][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.OgreMage][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.OgreMage][CreatureConstants.OgreMage] = "0";
            wingspans[CreatureConstants.Orc][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Orc][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Orc][CreatureConstants.Orc] = "0";
            wingspans[CreatureConstants.Orc_Half][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Orc_Half][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Orc_Half][CreatureConstants.Orc_Half] = "0";
            //Source: https://www.d20srd.org/srd/monsters/owlGiant.htm
            wingspans[CreatureConstants.Owl_Giant][GenderConstants.Female] = GetBaseFromUpTo(20 * 12);
            wingspans[CreatureConstants.Owl_Giant][GenderConstants.Male] = GetBaseFromUpTo(20 * 12);
            wingspans[CreatureConstants.Owl_Giant][CreatureConstants.Owl_Giant] = GetMultiplierFromUpTo(20 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Pit_fiend Pair of "massive" wings, so triple height
            wingspans[CreatureConstants.PitFiend][GenderConstants.Agender] = GetBaseFromAverage(12 * 12 * 3);
            wingspans[CreatureConstants.PitFiend][CreatureConstants.PitFiend] = GetMultiplierFromAverage(12 * 12 * 3);
            //Source: http://people.wku.edu/charles.plemons/ad&d/races/height.html - using height as wingspan
            wingspans[CreatureConstants.Pixie][GenderConstants.Female] = "23";
            wingspans[CreatureConstants.Pixie][GenderConstants.Male] = "24";
            wingspans[CreatureConstants.Pixie][CreatureConstants.Pixie] = "3d6";
            wingspans[CreatureConstants.Pixie_WithIrresistibleDance][GenderConstants.Female] = "23";
            wingspans[CreatureConstants.Pixie_WithIrresistibleDance][GenderConstants.Male] = "24";
            wingspans[CreatureConstants.Pixie_WithIrresistibleDance][CreatureConstants.Pixie_WithIrresistibleDance] = "3d6";
            //Source: http://www.biokids.umich.edu/critters/Tenodera_aridifolia/
            //https://forgottenrealms.fandom.com/wiki/Giant_praying_mantis scale up: [1,2]*[2*12,5*12]/[2,5] = [12,24]
            wingspans[CreatureConstants.PrayingMantis_Giant][GenderConstants.Female] = GetBaseFromRange(12, 24);
            wingspans[CreatureConstants.PrayingMantis_Giant][GenderConstants.Male] = GetBaseFromRange(12, 24);
            wingspans[CreatureConstants.PrayingMantis_Giant][CreatureConstants.PrayingMantis_Giant] = GetMultiplierFromRange(12, 24);
            wingspans[CreatureConstants.Pyrohydra_5Heads][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Pyrohydra_5Heads][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Pyrohydra_5Heads][CreatureConstants.Pyrohydra_5Heads] = "0";
            wingspans[CreatureConstants.Pyrohydra_6Heads][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Pyrohydra_6Heads][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Pyrohydra_6Heads][CreatureConstants.Pyrohydra_6Heads] = "0";
            wingspans[CreatureConstants.Pyrohydra_7Heads][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Pyrohydra_7Heads][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Pyrohydra_7Heads][CreatureConstants.Pyrohydra_7Heads] = "0";
            wingspans[CreatureConstants.Pyrohydra_8Heads][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Pyrohydra_8Heads][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Pyrohydra_8Heads][CreatureConstants.Pyrohydra_8Heads] = "0";
            wingspans[CreatureConstants.Pyrohydra_9Heads][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Pyrohydra_9Heads][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Pyrohydra_9Heads][CreatureConstants.Pyrohydra_9Heads] = "0";
            wingspans[CreatureConstants.Pyrohydra_10Heads][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Pyrohydra_10Heads][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Pyrohydra_10Heads][CreatureConstants.Pyrohydra_10Heads] = "0";
            wingspans[CreatureConstants.Pyrohydra_11Heads][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Pyrohydra_11Heads][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Pyrohydra_11Heads][CreatureConstants.Pyrohydra_11Heads] = "0";
            wingspans[CreatureConstants.Pyrohydra_12Heads][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Pyrohydra_12Heads][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Pyrohydra_12Heads][CreatureConstants.Pyrohydra_12Heads] = "0";
            wingspans[CreatureConstants.Quasit][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.Quasit][CreatureConstants.Quasit] = "0";
            wingspans[CreatureConstants.Rat][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Rat][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Rat][CreatureConstants.Rat] = "0";
            wingspans[CreatureConstants.Rat_Dire][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Rat_Dire][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Rat_Dire][CreatureConstants.Rat_Dire] = "0";
            wingspans[CreatureConstants.Rat_Swarm][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.Rat_Swarm][CreatureConstants.Rat_Swarm] = "0";
            wingspans[CreatureConstants.Retriever][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.Retriever][CreatureConstants.Retriever] = "0";
            wingspans[CreatureConstants.Sahuagin][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Sahuagin][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Sahuagin][CreatureConstants.Sahuagin] = "0";
            wingspans[CreatureConstants.Sahuagin_Malenti][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Sahuagin_Malenti][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Sahuagin_Malenti][CreatureConstants.Sahuagin_Malenti] = "0";
            wingspans[CreatureConstants.Sahuagin_Mutant][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Sahuagin_Mutant][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Sahuagin_Mutant][CreatureConstants.Sahuagin_Mutant] = "0";
            wingspans[CreatureConstants.Salamander_Flamebrother][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.Salamander_Flamebrother][CreatureConstants.Salamander_Flamebrother] = "0";
            wingspans[CreatureConstants.Salamander_Average][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.Salamander_Average][CreatureConstants.Salamander_Average] = "0";
            wingspans[CreatureConstants.Salamander_Noble][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.Salamander_Noble][CreatureConstants.Salamander_Noble] = "0";
            wingspans[CreatureConstants.Satyr][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Satyr][CreatureConstants.Satyr] = "0";
            wingspans[CreatureConstants.Satyr_WithPipes][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Satyr_WithPipes][CreatureConstants.Satyr] = "0";
            wingspans[CreatureConstants.SeaHag][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.SeaHag][CreatureConstants.SeaHag] = "0";
            //Source: https://www.d20srd.org/srd/monsters/shadow.htm
            wingspans[CreatureConstants.Shadow][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.Shadow][CreatureConstants.Shadow] = "0";
            wingspans[CreatureConstants.Shadow_Greater][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.Shadow_Greater][CreatureConstants.Shadow_Greater] = "0";
            wingspans[CreatureConstants.Shark_Medium][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Shark_Medium][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Shark_Medium][CreatureConstants.Shark_Medium] = "0";
            wingspans[CreatureConstants.Shark_Large][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Shark_Large][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Shark_Large][CreatureConstants.Shark_Large] = "0";
            wingspans[CreatureConstants.Shark_Huge][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Shark_Huge][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Shark_Huge][CreatureConstants.Shark_Huge] = "0";
            wingspans[CreatureConstants.Shark_Dire][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Shark_Dire][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Shark_Dire][CreatureConstants.Shark_Dire] = "0";
            //Source: https://forgottenrealms.fandom.com/wiki/Blue_slaad
            wingspans[CreatureConstants.Slaad_Blue][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.Slaad_Blue][CreatureConstants.Slaad_Blue] = "0";
            //Source: https://forgottenrealms.fandom.com/wiki/Red_slaad
            wingspans[CreatureConstants.Slaad_Red][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.Slaad_Red][CreatureConstants.Slaad_Red] = "0";
            //Source: https://forgottenrealms.fandom.com/wiki/Green_slaad
            wingspans[CreatureConstants.Slaad_Green][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.Slaad_Green][CreatureConstants.Slaad_Green] = "0";
            //Source: https://forgottenrealms.fandom.com/wiki/Gray_slaad
            wingspans[CreatureConstants.Slaad_Gray][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.Slaad_Gray][CreatureConstants.Slaad_Gray] = "0";
            //Source: https://forgottenrealms.fandom.com/wiki/Gray_slaad
            wingspans[CreatureConstants.Slaad_Death][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.Slaad_Death][CreatureConstants.Slaad_Death] = "0";
            wingspans[CreatureConstants.Snake_Constrictor][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Snake_Constrictor][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Snake_Constrictor][CreatureConstants.Snake_Constrictor] = "0";
            wingspans[CreatureConstants.Snake_Constrictor_Giant][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Snake_Constrictor_Giant][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Snake_Constrictor_Giant][CreatureConstants.Snake_Constrictor_Giant] = "0";
            //Source: https://www.d20srd.org/srd/monsters/swarm.htm
            wingspans[CreatureConstants.Spider_Swarm][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.Spider_Swarm][CreatureConstants.Spider_Swarm] = "0";
            wingspans[CreatureConstants.Squid_Giant][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Squid_Giant][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Squid_Giant][CreatureConstants.Squid_Giant] = "0";
            wingspans[CreatureConstants.StagBeetle_Giant][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.StagBeetle_Giant][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.StagBeetle_Giant][CreatureConstants.StagBeetle_Giant] = "0";
            //Source: https://forgottenrealms.fandom.com/wiki/Succubus - Large wings, so double height
            wingspans[CreatureConstants.Succubus][GenderConstants.Female] = GetBaseFromAverage(6 * 12 * 2);
            wingspans[CreatureConstants.Succubus][GenderConstants.Male] = GetBaseFromAverage(6 * 12 * 2);
            wingspans[CreatureConstants.Succubus][CreatureConstants.Succubus] = GetMultiplierFromAverage(6 * 12 * 2);
            wingspans[CreatureConstants.Tiefling][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Tiefling][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Tiefling][CreatureConstants.Tiefling] = "0";
            wingspans[CreatureConstants.Tiger][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Tiger][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Tiger][CreatureConstants.Tiger] = "0";
            wingspans[CreatureConstants.Tiger_Dire][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Tiger_Dire][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Tiger_Dire][CreatureConstants.Tiger_Dire] = "0";
            wingspans[CreatureConstants.Tojanida_Juvenile][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.Tojanida_Juvenile][CreatureConstants.Tojanida_Juvenile] = "0";
            wingspans[CreatureConstants.Tojanida_Adult][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.Tojanida_Adult][CreatureConstants.Tojanida_Adult] = "0";
            wingspans[CreatureConstants.Tojanida_Elder][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.Tojanida_Elder][CreatureConstants.Tojanida_Elder] = "0";
            //Source: https://forgottenrealms.fandom.com/wiki/Vrock - "broad" wings, so double height
            wingspans[CreatureConstants.Vrock][GenderConstants.Agender] = GetBaseFromAverage(8 * 12 * 2);
            wingspans[CreatureConstants.Vrock][CreatureConstants.Vrock] = GetMultiplierFromAverage(8 * 12 * 2);
            //Source: https://www.dimensions.com/element/red-paper-wasp-polistes-carolina
            //https://forgottenrealms.fandom.com/wiki/Giant_wasp scale up: [.59,.98]*5*12/[.94,1.26] = [38,47]
            wingspans[CreatureConstants.Wasp_Giant][GenderConstants.Male] = GetBaseFromRange(38, 47);
            wingspans[CreatureConstants.Wasp_Giant][CreatureConstants.Wasp_Giant] = GetMultiplierFromRange(38, 47);
            wingspans[CreatureConstants.Weasel][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Weasel][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Weasel][CreatureConstants.Weasel] = "0";
            wingspans[CreatureConstants.Weasel_Dire][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Weasel_Dire][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Weasel_Dire][CreatureConstants.Weasel_Dire] = "0";
            wingspans[CreatureConstants.Whale_Baleen][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Whale_Baleen][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Whale_Baleen][CreatureConstants.Whale_Baleen] = "0";
            wingspans[CreatureConstants.Whale_Cachalot][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Whale_Cachalot][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Whale_Cachalot][CreatureConstants.Whale_Cachalot] = "0";
            wingspans[CreatureConstants.Whale_Orca][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Whale_Orca][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Whale_Orca][CreatureConstants.Whale_Orca] = "0";
            wingspans[CreatureConstants.Wolf][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Wolf][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Wolf][CreatureConstants.Wolf] = "0";
            wingspans[CreatureConstants.Wolf_Dire][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Wolf_Dire][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Wolf_Dire][CreatureConstants.Wolf_Dire] = "0";
            wingspans[CreatureConstants.Wolverine][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Wolverine][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Wolverine][CreatureConstants.Wolverine] = "0";
            wingspans[CreatureConstants.Wolverine_Dire][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Wolverine_Dire][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Wolverine_Dire][CreatureConstants.Wolverine_Dire] = "0";
            wingspans[CreatureConstants.Wraith][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.Wraith][CreatureConstants.Wraith] = "0";
            wingspans[CreatureConstants.Wraith_Dread][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.Wraith_Dread][CreatureConstants.Wraith_Dread] = "0";
            wingspans[CreatureConstants.Xorn_Minor][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.Xorn_Minor][CreatureConstants.Xorn_Minor] = "0";
            wingspans[CreatureConstants.Xorn_Average][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.Xorn_Average][CreatureConstants.Xorn_Average] = "0";
            wingspans[CreatureConstants.Xorn_Elder][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.Xorn_Elder][CreatureConstants.Xorn_Elder] = "0";
            //Source: https://forgottenrealms.fandom.com/wiki/Zelekhut using Griffon stats
            wingspans[CreatureConstants.Zelekhut][GenderConstants.Agender] = GetBaseFromAtLeast(25 * 12);
            wingspans[CreatureConstants.Zelekhut][CreatureConstants.Zelekhut] = GetMultiplierFromAtLeast(25 * 12);

            return wingspans;
        }

        public static IEnumerable CreatureWingspansData => GetCreatureWingspans().Select(t => new TestCaseData(t.Key, t.Value));

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
            var wingspans = GetCreatureWingspans();

            Assert.That(wingspans, Contains.Key(creature));
            Assert.That(wingspans[creature], Contains.Key(creature).And.ContainKey(gender));

            var baseWingspan = dice.Roll(wingspans[creature][gender]).AsSum();
            var multiplierMin = dice.Roll(wingspans[creature][creature]).AsPotentialMinimum();
            var multiplierAvg = dice.Roll(wingspans[creature][creature]).AsPotentialAverage();
            var multiplierMax = dice.Roll(wingspans[creature][creature]).AsPotentialMaximum();
            var theoreticalRoll = RollHelper.GetRollWithFewestDice(average * 9 / 10, average * 11 / 10);

            Assert.That(baseWingspan + multiplierMin, Is.EqualTo(average * 0.9).Within(1), $"Min (-10%); Theoretical: {theoreticalRoll}");
            Assert.That(baseWingspan + multiplierAvg, Is.EqualTo(average).Within(1), $"Average; Theoretical: {theoreticalRoll}");
            Assert.That(baseWingspan + multiplierMax, Is.EqualTo(average * 1.1).Within(1), $"Max (+10%); Theoretical: {theoreticalRoll}");
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
            var wingspans = GetCreatureWingspans();

            Assert.That(wingspans, Contains.Key(creature));
            Assert.That(wingspans[creature], Contains.Key(creature).And.ContainKey(gender));

            var baseWingspan = dice.Roll(wingspans[creature][gender]).AsSum();
            var multiplierMin = dice.Roll(wingspans[creature][creature]).AsPotentialMinimum();
            var multiplierMax = dice.Roll(wingspans[creature][creature]).AsPotentialMaximum();
            var theoreticalRoll = RollHelper.GetRollWithFewestDice(min, max);

            Assert.That(baseWingspan + multiplierMin, Is.EqualTo(min), $"Min; Theoretical: {theoreticalRoll}");
            Assert.That(baseWingspan + multiplierMax, Is.EqualTo(max), $"Max; Theoretical: {theoreticalRoll}");
        }
    }
}