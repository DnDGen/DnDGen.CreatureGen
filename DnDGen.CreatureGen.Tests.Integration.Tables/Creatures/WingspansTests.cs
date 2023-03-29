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

            Assert.That(typesAndRolls, Is.Not.Empty, name);
            Assert.That(typesAndRolls.Keys, Is.EqualTo(genders.Union(new[] { name })), name);

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
            //Source: https://forgottenrealms.fandom.com/wiki/Astral_Deva
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
            wingspans[CreatureConstants.Bear_Brown][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Bear_Brown][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Bear_Brown][CreatureConstants.Bear_Brown] = "0";
            wingspans[CreatureConstants.BeardedDevil_Barbazu][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.BeardedDevil_Barbazu][CreatureConstants.BeardedDevil_Barbazu] = "0";
            wingspans[CreatureConstants.Bebilith][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.Bebilith][CreatureConstants.Bebilith] = "0";
            wingspans[CreatureConstants.BoneDevil_Osyluth][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.BoneDevil_Osyluth][CreatureConstants.BoneDevil_Osyluth] = "0";
            //TODO: Double-check from here
            wingspans[CreatureConstants.Bugbear][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Bugbear][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Bugbear][CreatureConstants.Bugbear] = "0";
            wingspans[CreatureConstants.Cat][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Cat][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Cat][CreatureConstants.Cat] = "0";
            wingspans[CreatureConstants.Centaur][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Centaur][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Centaur][CreatureConstants.Centaur] = "0";
            wingspans[CreatureConstants.ChainDevil_Kyton][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.ChainDevil_Kyton][CreatureConstants.ChainDevil_Kyton] = "0";
            wingspans[CreatureConstants.Criosphinx][GenderConstants.Male] = GetBaseFromAverage(120);
            wingspans[CreatureConstants.Criosphinx][CreatureConstants.Criosphinx] = GetMultiplierFromAverage(120);
            wingspans[CreatureConstants.Dretch][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.Dretch][CreatureConstants.Dretch] = "0";
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
            wingspans[CreatureConstants.Erinyes][GenderConstants.Agender] = GetBaseFromAverage(6 * 12);
            wingspans[CreatureConstants.Erinyes][CreatureConstants.Erinyes] = GetMultiplierFromAverage(6 * 12);
            wingspans[CreatureConstants.Ettin][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Ettin][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Ettin][CreatureConstants.Ettin] = "0";
            //Source: https://forgottenrealms.fandom.com/wiki/Cloud_giant
            wingspans[CreatureConstants.Giant_Cloud][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Giant_Cloud][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Giant_Cloud][CreatureConstants.Giant_Cloud] = "0";
            wingspans[CreatureConstants.Giant_Hill][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Giant_Hill][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Giant_Hill][CreatureConstants.Giant_Hill] = "0";
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
            wingspans[CreatureConstants.GreenHag][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.GreenHag][CreatureConstants.GreenHag] = "0";
            wingspans[CreatureConstants.Grig][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Grig][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Grig][CreatureConstants.Grig] = "0";
            wingspans[CreatureConstants.Grig_WithFiddle][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Grig_WithFiddle][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Grig_WithFiddle][CreatureConstants.Grig_WithFiddle] = "0";
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
            wingspans[CreatureConstants.Hellcat_Bezekira][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.Hellcat_Bezekira][CreatureConstants.Hellcat_Bezekira] = "0";
            wingspans[CreatureConstants.Hezrou][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.Hezrou][CreatureConstants.Hezrou] = "0";
            //Source: https://www.d20srd.org/srd/monsters/sphinx.htm
            wingspans[CreatureConstants.Hieracosphinx][GenderConstants.Male] = GetBaseFromAverage(10 * 12);
            wingspans[CreatureConstants.Hieracosphinx][CreatureConstants.Hieracosphinx] = GetMultiplierFromAverage(10 * 12);
            wingspans[CreatureConstants.Hobgoblin][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Hobgoblin][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Hobgoblin][CreatureConstants.Hobgoblin] = "0";
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
            wingspans[CreatureConstants.Human][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Human][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Human][CreatureConstants.Human] = "0";
            wingspans[CreatureConstants.IceDevil_Gelugon][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.IceDevil_Gelugon][CreatureConstants.IceDevil_Gelugon] = "0";
            wingspans[CreatureConstants.Imp][GenderConstants.Agender] = GetBaseFromAverage(2 * 12);
            wingspans[CreatureConstants.Imp][CreatureConstants.Imp] = GetMultiplierFromAverage(2 * 12);
            wingspans[CreatureConstants.Kobold][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Kobold][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Kobold][CreatureConstants.Kobold] = "0";
            wingspans[CreatureConstants.Lemure][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.Lemure][CreatureConstants.Lemure] = "0";
            wingspans[CreatureConstants.Leonal][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Leonal][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Leonal][CreatureConstants.Leonal] = "0";
            wingspans[CreatureConstants.Lizardfolk][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Lizardfolk][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Lizardfolk][CreatureConstants.Lizardfolk] = "0";
            wingspans[CreatureConstants.Locathah][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Locathah][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Locathah][CreatureConstants.Locathah] = "0";
            wingspans[CreatureConstants.Marilith][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Marilith][CreatureConstants.Marilith] = "0";
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
            //Source: https://forgottenrealms.fandom.com/wiki/Nalfeshnee Wings are "greatly undersized", so we will say 1/3
            wingspans[CreatureConstants.Nalfeshnee][GenderConstants.Agender] = GetBaseFromRange(10 * 12 / 3, 20 * 12 / 3);
            wingspans[CreatureConstants.Nalfeshnee][CreatureConstants.Nalfeshnee] = GetMultiplierFromRange(10 * 12 / 3, 20 * 12 / 3);
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
            wingspans[CreatureConstants.Quasit][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.Quasit][CreatureConstants.Quasit] = "0";
            wingspans[CreatureConstants.Retriever][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.Retriever][CreatureConstants.Retriever] = "0";
            wingspans[CreatureConstants.Sahuagin][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Sahuagin][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Sahuagin][CreatureConstants.Locathah] = "0";
            wingspans[CreatureConstants.Sahuagin_Malenti][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Sahuagin_Malenti][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Sahuagin_Malenti][CreatureConstants.Locathah] = "0";
            wingspans[CreatureConstants.Sahuagin_Mutant][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Sahuagin_Mutant][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Sahuagin_Mutant][CreatureConstants.Locathah] = "0";
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
            //Source: https://forgottenrealms.fandom.com/wiki/Succubus - Large wings, so double height
            wingspans[CreatureConstants.Succubus][GenderConstants.Female] = GetBaseFromAverage(6 * 12 * 2);
            wingspans[CreatureConstants.Succubus][GenderConstants.Male] = GetBaseFromAverage(6 * 12 * 2);
            wingspans[CreatureConstants.Succubus][CreatureConstants.Succubus] = GetMultiplierFromAverage(6 * 12 * 2);
            wingspans[CreatureConstants.Tiefling][GenderConstants.Female] = "0";
            wingspans[CreatureConstants.Tiefling][GenderConstants.Male] = "0";
            wingspans[CreatureConstants.Tiefling][CreatureConstants.Tiefling] = "0";
            wingspans[CreatureConstants.Tojanida_Juvenile][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.Tojanida_Juvenile][CreatureConstants.Tojanida_Juvenile] = "0";
            wingspans[CreatureConstants.Tojanida_Adult][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.Tojanida_Adult][CreatureConstants.Tojanida_Adult] = "0";
            wingspans[CreatureConstants.Tojanida_Elder][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.Tojanida_Elder][CreatureConstants.Tojanida_Elder] = "0";
            //Source: https://forgottenrealms.fandom.com/wiki/Vrock - "broad" wings, so double height
            wingspans[CreatureConstants.Vrock][GenderConstants.Agender] = GetBaseFromAverage(8 * 12 * 2);
            wingspans[CreatureConstants.Vrock][CreatureConstants.Vrock] = GetMultiplierFromAverage(8 * 12 * 2);
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
            wingspans[CreatureConstants.Xorn_Minor][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.Xorn_Minor][CreatureConstants.Xorn_Minor] = "0";
            wingspans[CreatureConstants.Xorn_Average][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.Xorn_Average][CreatureConstants.Xorn_Average] = "0";
            wingspans[CreatureConstants.Xorn_Elder][GenderConstants.Agender] = "0";
            wingspans[CreatureConstants.Xorn_Elder][CreatureConstants.Xorn_Elder] = "0";

            return wingspans;
        }

        public static IEnumerable CreatureWingspansData => GetCreatureWingspans().Select(t => new TestCaseData(t.Key, t.Value));

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
            var heights = GetCreatureWingspans();

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
            var heights = GetCreatureWingspans();

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
        public void IfCreatureHasNoLength_HasHeight()
        {
            var heights = HeightsTests.GetCreatureHeights();
            var Wingspans = GetCreatureWingspans();
            var creatures = CreatureConstants.GetAll();

            foreach (var creature in creatures)
            {
                Assert.That(Wingspans[creature][creature], Is.Not.Empty);
                Assert.That(heights[creature][creature], Is.Not.Empty);

                if (Wingspans[creature][creature] == "0")
                    Assert.That(heights[creature][creature], Is.Not.EqualTo("0"), creature);
            }
        }
    }
}