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
    public class LengthsTests : TypesAndAmountsTests
    {
        private ICollectionSelector collectionSelector;
        private Dice dice;

        protected override string tableName => TableNameConstants.TypeAndAmount.Lengths;

        [SetUp]
        public void Setup()
        {
            collectionSelector = GetNewInstanceOf<ICollectionSelector>();
            dice = GetNewInstanceOf<Dice>();
        }

        [Test]
        public void LengthsNames()
        {
            var creatures = CreatureConstants.GetAll();
            AssertCollectionNames(creatures);
        }

        [TestCaseSource(nameof(CreatureLengthsData))]
        public void CreatureLengths(string name, Dictionary<string, string> typesAndRolls)
        {
            var genders = collectionSelector.SelectFrom(TableNameConstants.Collection.Genders, name);

            Assert.That(typesAndRolls, Is.Not.Empty, name);
            Assert.That(typesAndRolls.Keys, Is.EqualTo(genders.Union(new[] { name })), name);

            AssertTypesAndAmounts(name, typesAndRolls);
        }

        public static Dictionary<string, Dictionary<string, string>> GetCreatureLengths()
        {
            var creatures = CreatureConstants.GetAll();
            var lengths = new Dictionary<string, Dictionary<string, string>>();

            foreach (var creature in creatures)
            {
                lengths[creature] = new Dictionary<string, string>();
            }

            lengths[CreatureConstants.Aasimar][GenderConstants.Female] = "0";
            lengths[CreatureConstants.Aasimar][GenderConstants.Male] = "0";
            lengths[CreatureConstants.Aasimar][CreatureConstants.Aasimar] = "0";
            //Source: https://forgottenrealms.fandom.com/wiki/Aboleth
            lengths[CreatureConstants.Aboleth][GenderConstants.Hermaphrodite] = GetBaseFromAverage(20 * 12);
            lengths[CreatureConstants.Aboleth][CreatureConstants.Aboleth] = GetMultiplierFromAverage(20 * 12);
            //Source: https://www.d20srd.org/srd/monsters/achaierai.htm
            lengths[CreatureConstants.Achaierai][GenderConstants.Female] = "0";
            lengths[CreatureConstants.Achaierai][GenderConstants.Male] = "0";
            lengths[CreatureConstants.Achaierai][CreatureConstants.Achaierai] = "0";
            //Basing off humans
            lengths[CreatureConstants.Allip][GenderConstants.Female] = "0";
            lengths[CreatureConstants.Allip][GenderConstants.Male] = "0";
            lengths[CreatureConstants.Allip][CreatureConstants.Allip] = "0";
            //Source: https://www.d20srd.org/srd/monsters/sphinx.htm
            lengths[CreatureConstants.Androsphinx][GenderConstants.Male] = GetBaseFromAverage(10 * 12);
            lengths[CreatureConstants.Androsphinx][CreatureConstants.Androsphinx] = GetMultiplierFromAverage(10 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Astral_Deva
            lengths[CreatureConstants.Angel_AstralDeva][GenderConstants.Female] = "0";
            lengths[CreatureConstants.Angel_AstralDeva][GenderConstants.Male] = "0";
            lengths[CreatureConstants.Angel_AstralDeva][CreatureConstants.Angel_AstralDeva] = "0";
            //Source: https://forgottenrealms.fandom.com/wiki/Planetar
            lengths[CreatureConstants.Angel_Planetar][GenderConstants.Female] = "0";
            lengths[CreatureConstants.Angel_Planetar][GenderConstants.Male] = "0";
            lengths[CreatureConstants.Angel_Planetar][CreatureConstants.Angel_Planetar] = "0";
            //Source: https://forgottenrealms.fandom.com/wiki/Solar
            lengths[CreatureConstants.Angel_Solar][GenderConstants.Female] = "0";
            lengths[CreatureConstants.Angel_Solar][GenderConstants.Male] = "0";
            lengths[CreatureConstants.Angel_Solar][CreatureConstants.Angel_Solar] = "0";
            //Source: https://www.d20srd.org/srd/combat/movementPositionAndDistance.htm
            lengths[CreatureConstants.AnimatedObject_Colossal][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.AnimatedObject_Colossal][CreatureConstants.AnimatedObject_Colossal] = "0";
            lengths[CreatureConstants.AnimatedObject_Colossal_Flexible][GenderConstants.Agender] = GetBaseFromRange(64 * 12, 128 * 12);
            lengths[CreatureConstants.AnimatedObject_Colossal_Flexible][CreatureConstants.AnimatedObject_Colossal_Flexible] = GetMultiplierFromRange(64 * 12, 128 * 12);
            lengths[CreatureConstants.AnimatedObject_Colossal_MultipleLegs][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.AnimatedObject_Colossal_MultipleLegs][CreatureConstants.AnimatedObject_Colossal_MultipleLegs] = "0";
            lengths[CreatureConstants.AnimatedObject_Colossal_MultipleLegs_Wooden][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.AnimatedObject_Colossal_MultipleLegs_Wooden][CreatureConstants.AnimatedObject_Colossal_MultipleLegs_Wooden] = "0";
            lengths[CreatureConstants.AnimatedObject_Colossal_Sheetlike][GenderConstants.Agender] = GetBaseFromRange(64 * 12, 128 * 12);
            lengths[CreatureConstants.AnimatedObject_Colossal_Sheetlike][CreatureConstants.AnimatedObject_Colossal_Sheetlike] = GetMultiplierFromRange(64 * 12, 128 * 12);
            lengths[CreatureConstants.AnimatedObject_Colossal_TwoLegs][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.AnimatedObject_Colossal_TwoLegs][CreatureConstants.AnimatedObject_Colossal_TwoLegs] = "0";
            lengths[CreatureConstants.AnimatedObject_Colossal_TwoLegs_Wooden][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.AnimatedObject_Colossal_TwoLegs_Wooden][CreatureConstants.AnimatedObject_Colossal_TwoLegs_Wooden] = "0";
            lengths[CreatureConstants.AnimatedObject_Colossal_Wheels_Wooden][GenderConstants.Agender] = GetBaseFromRange(64 * 12, 128 * 12);
            lengths[CreatureConstants.AnimatedObject_Colossal_Wheels_Wooden][CreatureConstants.AnimatedObject_Colossal_Wheels_Wooden] = GetMultiplierFromRange(64 * 12, 128 * 12);
            lengths[CreatureConstants.AnimatedObject_Colossal_Wooden][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.AnimatedObject_Colossal_Wooden][CreatureConstants.AnimatedObject_Colossal_Wooden] = "0";
            lengths[CreatureConstants.AnimatedObject_Gargantuan][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.AnimatedObject_Gargantuan][CreatureConstants.AnimatedObject_Gargantuan] = "0";
            lengths[CreatureConstants.AnimatedObject_Gargantuan_Flexible][GenderConstants.Agender] = GetBaseFromRange(32 * 12, 64 * 12);
            lengths[CreatureConstants.AnimatedObject_Gargantuan_Flexible][CreatureConstants.AnimatedObject_Gargantuan_Flexible] = GetMultiplierFromRange(32 * 12, 64 * 12);
            lengths[CreatureConstants.AnimatedObject_Gargantuan_MultipleLegs][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.AnimatedObject_Gargantuan_MultipleLegs][CreatureConstants.AnimatedObject_Gargantuan_MultipleLegs] = "0";
            lengths[CreatureConstants.AnimatedObject_Gargantuan_MultipleLegs_Wooden][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.AnimatedObject_Gargantuan_MultipleLegs_Wooden][CreatureConstants.AnimatedObject_Gargantuan_MultipleLegs_Wooden] = "0";
            lengths[CreatureConstants.AnimatedObject_Gargantuan_Sheetlike][GenderConstants.Agender] = GetBaseFromRange(32 * 12, 64 * 12);
            lengths[CreatureConstants.AnimatedObject_Gargantuan_Sheetlike][CreatureConstants.AnimatedObject_Gargantuan_Sheetlike] = GetMultiplierFromRange(32 * 12, 64 * 12);
            lengths[CreatureConstants.AnimatedObject_Gargantuan_TwoLegs][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.AnimatedObject_Gargantuan_TwoLegs][CreatureConstants.AnimatedObject_Gargantuan_TwoLegs] = "0";
            lengths[CreatureConstants.AnimatedObject_Gargantuan_TwoLegs_Wooden][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.AnimatedObject_Gargantuan_TwoLegs_Wooden][CreatureConstants.AnimatedObject_Gargantuan_TwoLegs_Wooden] = "0";
            lengths[CreatureConstants.AnimatedObject_Gargantuan_Wheels_Wooden][GenderConstants.Agender] = GetBaseFromRange(32 * 12, 64 * 12);
            lengths[CreatureConstants.AnimatedObject_Gargantuan_Wheels_Wooden][CreatureConstants.AnimatedObject_Gargantuan_Wheels_Wooden] = GetMultiplierFromRange(32 * 12, 64 * 12);
            lengths[CreatureConstants.AnimatedObject_Gargantuan_Wooden][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.AnimatedObject_Gargantuan_Wooden][CreatureConstants.AnimatedObject_Gargantuan_Wooden] = "0";
            lengths[CreatureConstants.AnimatedObject_Huge][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.AnimatedObject_Huge][CreatureConstants.AnimatedObject_Huge] = "0";
            lengths[CreatureConstants.AnimatedObject_Huge_Flexible][GenderConstants.Agender] = GetBaseFromRange(16 * 12, 32 * 12);
            lengths[CreatureConstants.AnimatedObject_Huge_Flexible][CreatureConstants.AnimatedObject_Huge_Flexible] = GetMultiplierFromRange(16 * 12, 32 * 12);
            lengths[CreatureConstants.AnimatedObject_Huge_MultipleLegs][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.AnimatedObject_Huge_MultipleLegs][CreatureConstants.AnimatedObject_Huge_MultipleLegs] = "0";
            lengths[CreatureConstants.AnimatedObject_Huge_MultipleLegs_Wooden][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.AnimatedObject_Huge_MultipleLegs_Wooden][CreatureConstants.AnimatedObject_Huge_MultipleLegs_Wooden] = "0";
            lengths[CreatureConstants.AnimatedObject_Huge_Sheetlike][GenderConstants.Agender] = GetBaseFromRange(16 * 12, 32 * 12);
            lengths[CreatureConstants.AnimatedObject_Huge_Sheetlike][CreatureConstants.AnimatedObject_Huge_Sheetlike] = GetMultiplierFromRange(16 * 12, 32 * 12);
            lengths[CreatureConstants.AnimatedObject_Huge_TwoLegs][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.AnimatedObject_Huge_TwoLegs][CreatureConstants.AnimatedObject_Huge_TwoLegs] = "0";
            lengths[CreatureConstants.AnimatedObject_Huge_TwoLegs_Wooden][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.AnimatedObject_Huge_TwoLegs_Wooden][CreatureConstants.AnimatedObject_Huge_TwoLegs_Wooden] = "0";
            lengths[CreatureConstants.AnimatedObject_Huge_Wheels_Wooden][GenderConstants.Agender] = GetBaseFromRange(16 * 12, 32 * 12);
            lengths[CreatureConstants.AnimatedObject_Huge_Wheels_Wooden][CreatureConstants.AnimatedObject_Huge_Wheels_Wooden] = GetMultiplierFromRange(16 * 12, 32 * 12);
            lengths[CreatureConstants.AnimatedObject_Huge_Wooden][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.AnimatedObject_Huge_Wooden][CreatureConstants.AnimatedObject_Huge_Wooden] = "0";
            lengths[CreatureConstants.AnimatedObject_Large][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.AnimatedObject_Large][CreatureConstants.AnimatedObject_Large] = "0";
            lengths[CreatureConstants.AnimatedObject_Large_Flexible][GenderConstants.Agender] = GetBaseFromRange(8 * 12, 16 * 12);
            lengths[CreatureConstants.AnimatedObject_Large_Flexible][CreatureConstants.AnimatedObject_Large_Flexible] = GetMultiplierFromRange(8 * 12, 16 * 12);
            lengths[CreatureConstants.AnimatedObject_Large_MultipleLegs][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.AnimatedObject_Large_MultipleLegs][CreatureConstants.AnimatedObject_Large_MultipleLegs] = "0";
            lengths[CreatureConstants.AnimatedObject_Large_MultipleLegs_Wooden][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.AnimatedObject_Large_MultipleLegs_Wooden][CreatureConstants.AnimatedObject_Large_MultipleLegs_Wooden] = "0";
            lengths[CreatureConstants.AnimatedObject_Large_Sheetlike][GenderConstants.Agender] = GetBaseFromRange(8 * 12, 16 * 12);
            lengths[CreatureConstants.AnimatedObject_Large_Sheetlike][CreatureConstants.AnimatedObject_Large_Sheetlike] = GetMultiplierFromRange(8 * 12, 16 * 12);
            lengths[CreatureConstants.AnimatedObject_Large_TwoLegs][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.AnimatedObject_Large_TwoLegs][CreatureConstants.AnimatedObject_Large_TwoLegs] = "0";
            lengths[CreatureConstants.AnimatedObject_Large_TwoLegs_Wooden][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.AnimatedObject_Large_TwoLegs_Wooden][CreatureConstants.AnimatedObject_Large_TwoLegs_Wooden] = "0";
            lengths[CreatureConstants.AnimatedObject_Large_Wheels_Wooden][GenderConstants.Agender] = GetBaseFromRange(8 * 12, 16 * 12);
            lengths[CreatureConstants.AnimatedObject_Large_Wheels_Wooden][CreatureConstants.AnimatedObject_Large_Wheels_Wooden] = GetMultiplierFromRange(8 * 12, 16 * 12);
            lengths[CreatureConstants.AnimatedObject_Large_Wooden][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.AnimatedObject_Large_Wooden][CreatureConstants.AnimatedObject_Large_Wooden] = "0";
            lengths[CreatureConstants.AnimatedObject_Medium][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.AnimatedObject_Medium][CreatureConstants.AnimatedObject_Medium] = "0";
            lengths[CreatureConstants.AnimatedObject_Medium_Flexible][GenderConstants.Agender] = GetBaseFromRange(4 * 12, 8 * 12);
            lengths[CreatureConstants.AnimatedObject_Medium_Flexible][CreatureConstants.AnimatedObject_Medium_Flexible] = GetMultiplierFromRange(4 * 12, 8 * 12);
            lengths[CreatureConstants.AnimatedObject_Medium_MultipleLegs][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.AnimatedObject_Medium_MultipleLegs][CreatureConstants.AnimatedObject_Medium_MultipleLegs] = "0";
            lengths[CreatureConstants.AnimatedObject_Medium_MultipleLegs_Wooden][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.AnimatedObject_Medium_MultipleLegs_Wooden][CreatureConstants.AnimatedObject_Medium_MultipleLegs_Wooden] = "0";
            lengths[CreatureConstants.AnimatedObject_Medium_Sheetlike][GenderConstants.Agender] = GetBaseFromRange(4 * 12, 8 * 12);
            lengths[CreatureConstants.AnimatedObject_Medium_Sheetlike][CreatureConstants.AnimatedObject_Medium_Sheetlike] = GetMultiplierFromRange(4 * 12, 8 * 12);
            lengths[CreatureConstants.AnimatedObject_Medium_TwoLegs][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.AnimatedObject_Medium_TwoLegs][CreatureConstants.AnimatedObject_Medium_TwoLegs] = "0";
            lengths[CreatureConstants.AnimatedObject_Medium_TwoLegs_Wooden][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.AnimatedObject_Medium_TwoLegs_Wooden][CreatureConstants.AnimatedObject_Medium_TwoLegs_Wooden] = "0";
            lengths[CreatureConstants.AnimatedObject_Medium_Wheels_Wooden][GenderConstants.Agender] = GetBaseFromRange(4 * 12, 8 * 12);
            lengths[CreatureConstants.AnimatedObject_Medium_Wheels_Wooden][CreatureConstants.AnimatedObject_Medium_Wheels_Wooden] = GetMultiplierFromRange(4 * 12, 8 * 12);
            lengths[CreatureConstants.AnimatedObject_Medium_Wooden][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.AnimatedObject_Medium_Wooden][CreatureConstants.AnimatedObject_Medium_Wooden] = "0";
            lengths[CreatureConstants.AnimatedObject_Small][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.AnimatedObject_Small][CreatureConstants.AnimatedObject_Small] = "0";
            lengths[CreatureConstants.AnimatedObject_Small_Flexible][GenderConstants.Agender] = GetBaseFromRange(2 * 12, 4 * 12);
            lengths[CreatureConstants.AnimatedObject_Small_Flexible][CreatureConstants.AnimatedObject_Small_Flexible] = GetMultiplierFromRange(2 * 12, 4 * 12);
            lengths[CreatureConstants.AnimatedObject_Small_MultipleLegs][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.AnimatedObject_Small_MultipleLegs][CreatureConstants.AnimatedObject_Small_MultipleLegs] = "0";
            lengths[CreatureConstants.AnimatedObject_Small_MultipleLegs_Wooden][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.AnimatedObject_Small_MultipleLegs_Wooden][CreatureConstants.AnimatedObject_Small_MultipleLegs_Wooden] = "0";
            lengths[CreatureConstants.AnimatedObject_Small_Sheetlike][GenderConstants.Agender] = GetBaseFromRange(2 * 12, 4 * 12);
            lengths[CreatureConstants.AnimatedObject_Small_Sheetlike][CreatureConstants.AnimatedObject_Small_Sheetlike] = GetMultiplierFromRange(2 * 12, 4 * 12);
            lengths[CreatureConstants.AnimatedObject_Small_TwoLegs][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.AnimatedObject_Small_TwoLegs][CreatureConstants.AnimatedObject_Small_TwoLegs] = "0";
            lengths[CreatureConstants.AnimatedObject_Small_TwoLegs_Wooden][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.AnimatedObject_Small_TwoLegs_Wooden][CreatureConstants.AnimatedObject_Small_TwoLegs_Wooden] = "0";
            lengths[CreatureConstants.AnimatedObject_Small_Wheels_Wooden][GenderConstants.Agender] = GetBaseFromRange(2 * 12, 4 * 12);
            lengths[CreatureConstants.AnimatedObject_Small_Wheels_Wooden][CreatureConstants.AnimatedObject_Small_Wheels_Wooden] = GetMultiplierFromRange(2 * 12, 4 * 12);
            lengths[CreatureConstants.AnimatedObject_Small_Wooden][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.AnimatedObject_Small_Wooden][CreatureConstants.AnimatedObject_Small_Wooden] = "0";
            lengths[CreatureConstants.AnimatedObject_Tiny][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.AnimatedObject_Tiny][CreatureConstants.AnimatedObject_Tiny] = "0";
            lengths[CreatureConstants.AnimatedObject_Tiny_Flexible][GenderConstants.Agender] = GetBaseFromRange(12, 24);
            lengths[CreatureConstants.AnimatedObject_Tiny_Flexible][CreatureConstants.AnimatedObject_Tiny_Flexible] = GetMultiplierFromRange(12, 24);
            lengths[CreatureConstants.AnimatedObject_Tiny_MultipleLegs][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.AnimatedObject_Tiny_MultipleLegs][CreatureConstants.AnimatedObject_Tiny_MultipleLegs] = "0";
            lengths[CreatureConstants.AnimatedObject_Tiny_MultipleLegs_Wooden][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.AnimatedObject_Tiny_MultipleLegs_Wooden][CreatureConstants.AnimatedObject_Tiny_MultipleLegs_Wooden] = "0";
            lengths[CreatureConstants.AnimatedObject_Tiny_Sheetlike][GenderConstants.Agender] = GetBaseFromRange(12, 24);
            lengths[CreatureConstants.AnimatedObject_Tiny_Sheetlike][CreatureConstants.AnimatedObject_Tiny_Sheetlike] = GetMultiplierFromRange(12, 24);
            lengths[CreatureConstants.AnimatedObject_Tiny_TwoLegs][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.AnimatedObject_Tiny_TwoLegs][CreatureConstants.AnimatedObject_Tiny_TwoLegs] = "0";
            lengths[CreatureConstants.AnimatedObject_Tiny_TwoLegs_Wooden][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.AnimatedObject_Tiny_TwoLegs_Wooden][CreatureConstants.AnimatedObject_Tiny_TwoLegs_Wooden] = "0";
            lengths[CreatureConstants.AnimatedObject_Tiny_Wheels_Wooden][GenderConstants.Agender] = GetBaseFromRange(12, 24);
            lengths[CreatureConstants.AnimatedObject_Tiny_Wheels_Wooden][CreatureConstants.AnimatedObject_Tiny_Wheels_Wooden] = GetMultiplierFromRange(12, 24);
            lengths[CreatureConstants.AnimatedObject_Tiny_Wooden][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.AnimatedObject_Tiny_Wooden][CreatureConstants.AnimatedObject_Tiny_Wooden] = "0";
            //Source: https://www.d20srd.org/srd/monsters/ankheg.htm
            lengths[CreatureConstants.Ankheg][GenderConstants.Female] = GetBaseFromAverage(10 * 12);
            lengths[CreatureConstants.Ankheg][GenderConstants.Male] = GetBaseFromAverage(10 * 12);
            lengths[CreatureConstants.Ankheg][CreatureConstants.Ankheg] = GetMultiplierFromAverage(10 * 12);
            //Source: https://www.d20srd.org/srd/monsters/hag.htm#annis
            lengths[CreatureConstants.Annis][GenderConstants.Female] = "0";
            lengths[CreatureConstants.Annis][CreatureConstants.Annis] = "0";
            //Source: (Male) https://www.d20srd.org/srd/monsters/ape.htm, (Female) https://nationalzoo.si.edu/animals/western-lowland-gorilla
            lengths[CreatureConstants.Ape][GenderConstants.Female] = "0";
            lengths[CreatureConstants.Ape][GenderConstants.Male] = "0";
            lengths[CreatureConstants.Ape][CreatureConstants.Ape] = "0";
            //Multiplying the female up
            lengths[CreatureConstants.Ape_Dire][GenderConstants.Female] = "0";
            lengths[CreatureConstants.Ape_Dire][GenderConstants.Male] = "0";
            lengths[CreatureConstants.Ape_Dire][CreatureConstants.Ape_Dire] = "0";
            //INFO: Based on Half-Elf, since could be Human, Half-Elf, or Drow
            lengths[CreatureConstants.Aranea][GenderConstants.Female] = "0";
            lengths[CreatureConstants.Aranea][GenderConstants.Male] = "0";
            lengths[CreatureConstants.Aranea][CreatureConstants.Aranea] = "0";
            //Source: https://www.d20srd.org/srd/monsters/arrowhawk.htm
            lengths[CreatureConstants.Arrowhawk_Juvenile][GenderConstants.Female] = GetBaseFromAverage(5 * 12);
            lengths[CreatureConstants.Arrowhawk_Juvenile][GenderConstants.Male] = GetBaseFromAverage(5 * 12);
            lengths[CreatureConstants.Arrowhawk_Juvenile][CreatureConstants.Arrowhawk_Juvenile] = GetMultiplierFromAverage(5 * 12);
            lengths[CreatureConstants.Arrowhawk_Adult][GenderConstants.Female] = GetBaseFromAverage(10 * 12);
            lengths[CreatureConstants.Arrowhawk_Adult][GenderConstants.Male] = GetBaseFromAverage(10 * 12);
            lengths[CreatureConstants.Arrowhawk_Adult][CreatureConstants.Arrowhawk_Adult] = GetMultiplierFromAverage(10 * 12);
            lengths[CreatureConstants.Arrowhawk_Elder][GenderConstants.Female] = GetBaseFromAverage(20 * 12);
            lengths[CreatureConstants.Arrowhawk_Elder][GenderConstants.Male] = GetBaseFromAverage(20 * 12);
            lengths[CreatureConstants.Arrowhawk_Elder][CreatureConstants.Arrowhawk_Elder] = GetMultiplierFromAverage(20 * 12);
            //Source: https://www.d20srd.org/srd/monsters/athach.htm
            lengths[CreatureConstants.Athach][GenderConstants.Female] = "0";
            lengths[CreatureConstants.Athach][GenderConstants.Male] = "0";
            lengths[CreatureConstants.Athach][CreatureConstants.Athach] = "0";
            //Source: https://forgottenrealms.fandom.com/wiki/Avoral
            lengths[CreatureConstants.Avoral][GenderConstants.Female] = "0";
            lengths[CreatureConstants.Avoral][GenderConstants.Male] = "0";
            lengths[CreatureConstants.Avoral][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.Avoral][CreatureConstants.Avoral] = "0";
            //Source: https://forgottenrealms.fandom.com/wiki/Azer
            lengths[CreatureConstants.Azer][GenderConstants.Male] = "0";
            lengths[CreatureConstants.Azer][GenderConstants.Female] = "0";
            lengths[CreatureConstants.Azer][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.Azer][CreatureConstants.Azer] = "0";
            lengths[CreatureConstants.Babau][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.Babau][CreatureConstants.Babau] = "0";
            //Source: https://www.d20srd.org/srd/monsters/baboon.htm
            lengths[CreatureConstants.Baboon][GenderConstants.Female] = GetBaseFromRange(24, 48);
            lengths[CreatureConstants.Baboon][GenderConstants.Male] = GetBaseFromRange(24, 48);
            lengths[CreatureConstants.Baboon][CreatureConstants.Baboon] = GetMultiplierFromRange(24, 48);
            //Source: https://www.d20srd.org/srd/monsters/badger.htm
            lengths[CreatureConstants.Badger][GenderConstants.Female] = GetBaseFromRange(24, 36);
            lengths[CreatureConstants.Badger][GenderConstants.Male] = GetBaseFromRange(24, 36);
            lengths[CreatureConstants.Badger][CreatureConstants.Badger] = GetMultiplierFromRange(24, 36);
            //Source: https://forgottenrealms.fandom.com/wiki/Dire_badger
            lengths[CreatureConstants.Badger_Dire][GenderConstants.Female] = GetBaseFromRange(4 * 12, 7 * 12 + 8);
            lengths[CreatureConstants.Badger_Dire][GenderConstants.Male] = GetBaseFromRange(4 * 12, 7 * 12 + 8);
            lengths[CreatureConstants.Badger_Dire][CreatureConstants.Badger_Dire] = GetMultiplierFromRange(4 * 12, 7 * 12 + 8);
            lengths[CreatureConstants.Balor][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.Balor][CreatureConstants.Balor] = "0";
            lengths[CreatureConstants.BarbedDevil_Hamatula][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.BarbedDevil_Hamatula][CreatureConstants.BarbedDevil_Hamatula] = "0";
            //Source: https://forgottenrealms.fandom.com/wiki/Barghest
            lengths[CreatureConstants.Barghest][GenderConstants.Agender] = GetBaseFromAverage(6 * 12);
            lengths[CreatureConstants.Barghest][CreatureConstants.Barghest] = GetMultiplierFromAverage(6 * 12);
            lengths[CreatureConstants.Barghest_Greater][GenderConstants.Agender] = GetBaseFromAverage(6 * 12);
            lengths[CreatureConstants.Barghest_Greater][CreatureConstants.Barghest_Greater] = GetMultiplierFromAverage(6 * 12);
            //Source: https://www.dimensions.com/element/grizzly-bear
            lengths[CreatureConstants.Bear_Brown][GenderConstants.Female] = GetBaseFromRange(5 * 12 + 6, 6 * 12 + 6);
            lengths[CreatureConstants.Bear_Brown][GenderConstants.Male] = GetBaseFromRange(7 * 12, 8 * 12);
            lengths[CreatureConstants.Bear_Brown][CreatureConstants.Bear_Brown] = GetMultiplierFromRange(7 * 12, 8 * 12);
            lengths[CreatureConstants.BeardedDevil_Barbazu][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.BeardedDevil_Barbazu][CreatureConstants.BeardedDevil_Barbazu] = "0";
            //Source: https://forgottenrealms.fandom.com/wiki/Bebilith
            lengths[CreatureConstants.Bebilith][GenderConstants.Agender] = GetBaseFromAverage(14 * 12);
            lengths[CreatureConstants.Bebilith][CreatureConstants.Bebilith] = GetMultiplierFromAverage(14 * 12);
            lengths[CreatureConstants.BoneDevil_Osyluth][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.BoneDevil_Osyluth][CreatureConstants.BoneDevil_Osyluth] = "0";
            //TODO: Double-check from here
            lengths[CreatureConstants.Bugbear][GenderConstants.Female] = "0";
            lengths[CreatureConstants.Bugbear][GenderConstants.Male] = "0";
            lengths[CreatureConstants.Bugbear][CreatureConstants.Bugbear] = "0";
            //Source: https://www.dimensions.com/element/american-shorthair-cat
            lengths[CreatureConstants.Cat][GenderConstants.Female] = GetBaseFromRange(12, 15);
            lengths[CreatureConstants.Cat][GenderConstants.Male] = GetBaseFromRange(12, 15);
            lengths[CreatureConstants.Cat][CreatureConstants.Cat] = GetMultiplierFromRange(12, 15);
            //INFO: Using standard horse length
            lengths[CreatureConstants.Centaur][GenderConstants.Female] = GetBaseFromAverage(8 * 12);
            lengths[CreatureConstants.Centaur][GenderConstants.Male] = GetBaseFromAverage(8 * 12);
            lengths[CreatureConstants.Centaur][CreatureConstants.Centaur] = GetMultiplierFromAverage(8 * 12);
            lengths[CreatureConstants.ChainDevil_Kyton][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.ChainDevil_Kyton][CreatureConstants.ChainDevil_Kyton] = "0";
            //Source: https://www.d20srd.org/srd/monsters/sphinx.htm
            lengths[CreatureConstants.Criosphinx][GenderConstants.Male] = GetBaseFromAverage(10 * 12);
            lengths[CreatureConstants.Criosphinx][CreatureConstants.Androsphinx] = GetMultiplierFromAverage(10 * 12);
            lengths[CreatureConstants.Dretch][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.Dretch][CreatureConstants.Dretch] = "0";
            lengths[CreatureConstants.Dwarf_Deep][GenderConstants.Female] = "0";
            lengths[CreatureConstants.Dwarf_Deep][GenderConstants.Male] = "0";
            lengths[CreatureConstants.Dwarf_Deep][CreatureConstants.Dwarf_Deep] = "0";
            lengths[CreatureConstants.Dwarf_Duergar][GenderConstants.Female] = "0";
            lengths[CreatureConstants.Dwarf_Duergar][GenderConstants.Male] = "0";
            lengths[CreatureConstants.Dwarf_Duergar][CreatureConstants.Dwarf_Duergar] = "0";
            lengths[CreatureConstants.Dwarf_Hill][GenderConstants.Female] = "0";
            lengths[CreatureConstants.Dwarf_Hill][GenderConstants.Male] = "0";
            lengths[CreatureConstants.Dwarf_Hill][CreatureConstants.Dwarf_Hill] = "0";
            lengths[CreatureConstants.Dwarf_Mountain][GenderConstants.Female] = "0";
            lengths[CreatureConstants.Dwarf_Mountain][GenderConstants.Male] = "0";
            lengths[CreatureConstants.Dwarf_Mountain][CreatureConstants.Dwarf_Mountain] = "0";
            lengths[CreatureConstants.Elemental_Air_Small][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.Elemental_Air_Small][CreatureConstants.Elemental_Air_Small] = "0";
            lengths[CreatureConstants.Elemental_Air_Medium][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.Elemental_Air_Medium][CreatureConstants.Elemental_Air_Medium] = "0";
            lengths[CreatureConstants.Elemental_Air_Large][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.Elemental_Air_Large][CreatureConstants.Elemental_Air_Large] = "0";
            lengths[CreatureConstants.Elemental_Air_Huge][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.Elemental_Air_Huge][CreatureConstants.Elemental_Air_Huge] = "0";
            lengths[CreatureConstants.Elemental_Air_Greater][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.Elemental_Air_Greater][CreatureConstants.Elemental_Air_Greater] = "0";
            lengths[CreatureConstants.Elemental_Air_Elder][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.Elemental_Air_Elder][CreatureConstants.Elemental_Air_Elder] = "0";
            lengths[CreatureConstants.Elemental_Earth_Small][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.Elemental_Earth_Small][CreatureConstants.Elemental_Earth_Small] = "0";
            lengths[CreatureConstants.Elemental_Earth_Medium][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.Elemental_Earth_Medium][CreatureConstants.Elemental_Earth_Medium] = "0";
            lengths[CreatureConstants.Elemental_Earth_Large][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.Elemental_Earth_Large][CreatureConstants.Elemental_Earth_Large] = "0";
            lengths[CreatureConstants.Elemental_Earth_Huge][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.Elemental_Earth_Huge][CreatureConstants.Elemental_Earth_Huge] = "0";
            lengths[CreatureConstants.Elemental_Earth_Greater][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.Elemental_Earth_Greater][CreatureConstants.Elemental_Earth_Greater] = "0";
            lengths[CreatureConstants.Elemental_Earth_Elder][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.Elemental_Earth_Elder][CreatureConstants.Elemental_Earth_Elder] = "0";
            lengths[CreatureConstants.Elemental_Fire_Small][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.Elemental_Fire_Small][CreatureConstants.Elemental_Fire_Small] = "0";
            lengths[CreatureConstants.Elemental_Fire_Medium][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.Elemental_Fire_Medium][CreatureConstants.Elemental_Fire_Medium] = "0";
            lengths[CreatureConstants.Elemental_Fire_Large][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.Elemental_Fire_Large][CreatureConstants.Elemental_Fire_Large] = "0";
            lengths[CreatureConstants.Elemental_Fire_Huge][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.Elemental_Fire_Huge][CreatureConstants.Elemental_Fire_Huge] = "0";
            lengths[CreatureConstants.Elemental_Fire_Greater][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.Elemental_Fire_Greater][CreatureConstants.Elemental_Fire_Greater] = "0";
            lengths[CreatureConstants.Elemental_Fire_Elder][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.Elemental_Fire_Elder][CreatureConstants.Elemental_Fire_Elder] = "0";
            lengths[CreatureConstants.Elemental_Water_Small][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.Elemental_Water_Small][CreatureConstants.Elemental_Water_Small] = "0";
            lengths[CreatureConstants.Elemental_Water_Medium][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.Elemental_Water_Medium][CreatureConstants.Elemental_Water_Medium] = "0";
            lengths[CreatureConstants.Elemental_Water_Large][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.Elemental_Water_Large][CreatureConstants.Elemental_Water_Large] = "0";
            lengths[CreatureConstants.Elemental_Water_Huge][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.Elemental_Water_Huge][CreatureConstants.Elemental_Water_Huge] = "0";
            lengths[CreatureConstants.Elemental_Water_Greater][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.Elemental_Water_Greater][CreatureConstants.Elemental_Water_Greater] = "0";
            lengths[CreatureConstants.Elemental_Water_Elder][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.Elemental_Water_Elder][CreatureConstants.Elemental_Water_Elder] = "0";
            lengths[CreatureConstants.Elf_Aquatic][GenderConstants.Female] = "0";
            lengths[CreatureConstants.Elf_Aquatic][GenderConstants.Male] = "0";
            lengths[CreatureConstants.Elf_Aquatic][CreatureConstants.Elf_Aquatic] = "0";
            lengths[CreatureConstants.Elf_Drow][GenderConstants.Female] = "0";
            lengths[CreatureConstants.Elf_Drow][GenderConstants.Male] = "0";
            lengths[CreatureConstants.Elf_Drow][CreatureConstants.Elf_Drow] = "0";
            lengths[CreatureConstants.Elf_Gray][GenderConstants.Female] = "0";
            lengths[CreatureConstants.Elf_Gray][GenderConstants.Male] = "0";
            lengths[CreatureConstants.Elf_Gray][CreatureConstants.Elf_Gray] = "0";
            lengths[CreatureConstants.Elf_Half][GenderConstants.Female] = "0";
            lengths[CreatureConstants.Elf_Half][GenderConstants.Male] = "0";
            lengths[CreatureConstants.Elf_Half][CreatureConstants.Elf_Half] = "0";
            lengths[CreatureConstants.Elf_High][GenderConstants.Female] = "0";
            lengths[CreatureConstants.Elf_High][GenderConstants.Male] = "0";
            lengths[CreatureConstants.Elf_High][CreatureConstants.Elf_High] = "0";
            lengths[CreatureConstants.Elf_Wild][GenderConstants.Female] = "0";
            lengths[CreatureConstants.Elf_Wild][GenderConstants.Male] = "0";
            lengths[CreatureConstants.Elf_Wild][CreatureConstants.Elf_Wild] = "0";
            lengths[CreatureConstants.Elf_Wood][GenderConstants.Female] = "0";
            lengths[CreatureConstants.Elf_Wood][GenderConstants.Male] = "0";
            lengths[CreatureConstants.Elf_Wood][CreatureConstants.Elf_Wood] = "0";
            lengths[CreatureConstants.Erinyes][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.Erinyes][CreatureConstants.Erinyes] = "0";
            lengths[CreatureConstants.Ettin][GenderConstants.Female] = "0";
            lengths[CreatureConstants.Ettin][GenderConstants.Male] = "0";
            lengths[CreatureConstants.Ettin][CreatureConstants.Ettin] = "0";
            //Source: https://forgottenrealms.fandom.com/wiki/Cloud_giant
            lengths[CreatureConstants.Giant_Cloud][GenderConstants.Female] = "0";
            lengths[CreatureConstants.Giant_Cloud][GenderConstants.Male] = "0";
            lengths[CreatureConstants.Giant_Cloud][CreatureConstants.Giant_Cloud] = "0";
            lengths[CreatureConstants.Giant_Hill][GenderConstants.Female] = "0";
            lengths[CreatureConstants.Giant_Hill][GenderConstants.Male] = "0";
            lengths[CreatureConstants.Giant_Hill][CreatureConstants.Giant_Hill] = "0";
            lengths[CreatureConstants.Glabrezu][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.Glabrezu][CreatureConstants.Glabrezu] = "0";
            lengths[CreatureConstants.Gnoll][GenderConstants.Female] = "0";
            lengths[CreatureConstants.Gnoll][GenderConstants.Male] = "0";
            lengths[CreatureConstants.Gnoll][CreatureConstants.Gnoll] = "0";
            lengths[CreatureConstants.Gnome_Forest][GenderConstants.Female] = "0";
            lengths[CreatureConstants.Gnome_Forest][GenderConstants.Male] = "0";
            lengths[CreatureConstants.Gnome_Forest][CreatureConstants.Gnome_Forest] = "0";
            lengths[CreatureConstants.Gnome_Rock][GenderConstants.Female] = "0";
            lengths[CreatureConstants.Gnome_Rock][GenderConstants.Male] = "0";
            lengths[CreatureConstants.Gnome_Rock][CreatureConstants.Gnome_Rock] = "0";
            lengths[CreatureConstants.Gnome_Svirfneblin][GenderConstants.Female] = "0";
            lengths[CreatureConstants.Gnome_Svirfneblin][GenderConstants.Male] = "0";
            lengths[CreatureConstants.Gnome_Svirfneblin][CreatureConstants.Gnome_Svirfneblin] = "0";
            lengths[CreatureConstants.Goblin][GenderConstants.Female] = "0";
            lengths[CreatureConstants.Goblin][GenderConstants.Male] = "0";
            lengths[CreatureConstants.Goblin][CreatureConstants.Goblin] = "0";
            lengths[CreatureConstants.GreenHag][GenderConstants.Female] = "0";
            lengths[CreatureConstants.GreenHag][CreatureConstants.GreenHag] = "0";
            lengths[CreatureConstants.Grig][GenderConstants.Female] = "0";
            lengths[CreatureConstants.Grig][GenderConstants.Male] = "0";
            lengths[CreatureConstants.Grig][CreatureConstants.Grig] = "0";
            lengths[CreatureConstants.Grig_WithFiddle][GenderConstants.Female] = "0";
            lengths[CreatureConstants.Grig_WithFiddle][GenderConstants.Male] = "0";
            lengths[CreatureConstants.Grig_WithFiddle][CreatureConstants.Grig_WithFiddle] = "0";
            //Source: https://www.d20srd.org/srd/monsters/sphinx.htm
            lengths[CreatureConstants.Gynosphinx][GenderConstants.Female] = GetBaseFromAverage(10 * 12);
            lengths[CreatureConstants.Gynosphinx][CreatureConstants.Gynosphinx] = GetMultiplierFromAverage(10 * 12);
            lengths[CreatureConstants.Halfling_Deep][GenderConstants.Female] = "0";
            lengths[CreatureConstants.Halfling_Deep][GenderConstants.Male] = "0";
            lengths[CreatureConstants.Halfling_Deep][CreatureConstants.Halfling_Deep] = "0";
            lengths[CreatureConstants.Halfling_Lightfoot][GenderConstants.Female] = "0";
            lengths[CreatureConstants.Halfling_Lightfoot][GenderConstants.Male] = "0";
            lengths[CreatureConstants.Halfling_Lightfoot][CreatureConstants.Halfling_Lightfoot] = "0";
            lengths[CreatureConstants.Halfling_Tallfellow][GenderConstants.Female] = "0";
            lengths[CreatureConstants.Halfling_Tallfellow][GenderConstants.Male] = "0";
            lengths[CreatureConstants.Halfling_Tallfellow][CreatureConstants.Halfling_Tallfellow] = "0";
            //Source: https://forgottenrealms.fandom.com/wiki/Hellcat
            lengths[CreatureConstants.Hellcat_Bezekira][GenderConstants.Agender] = GetBaseFromRange(6 * 12, 9 * 12);
            lengths[CreatureConstants.Hellcat_Bezekira][CreatureConstants.Hellcat_Bezekira] = GetMultiplierFromRange(6 * 12, 9 * 12);
            //Source: https://www.d20srd.org/srd/monsters/demon.htm#hezrou
            lengths[CreatureConstants.Hezrou][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.Hezrou][CreatureConstants.Hezrou] = "0";
            //Source: https://www.d20srd.org/srd/monsters/sphinx.htm
            lengths[CreatureConstants.Hieracosphinx][GenderConstants.Male] = GetBaseFromAverage(10 * 12);
            lengths[CreatureConstants.Hieracosphinx][CreatureConstants.Hieracosphinx] = GetMultiplierFromAverage(10 * 12);
            lengths[CreatureConstants.Hobgoblin][GenderConstants.Female] = "0";
            lengths[CreatureConstants.Hobgoblin][GenderConstants.Male] = "0";
            lengths[CreatureConstants.Hobgoblin][CreatureConstants.Hobgoblin] = "0";
            lengths[CreatureConstants.HornedDevil_Cornugon][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.HornedDevil_Cornugon][CreatureConstants.HornedDevil_Cornugon] = "0";
            lengths[CreatureConstants.Horse_Heavy][GenderConstants.Female] = GetBaseFromAverage(8 * 12);
            lengths[CreatureConstants.Horse_Heavy][GenderConstants.Male] = GetBaseFromAverage(8 * 12);
            lengths[CreatureConstants.Horse_Heavy][CreatureConstants.Horse_Heavy] = GetMultiplierFromAverage(8 * 12);
            lengths[CreatureConstants.Horse_Light][GenderConstants.Female] = GetBaseFromAverage(8 * 12);
            lengths[CreatureConstants.Horse_Light][GenderConstants.Male] = GetBaseFromAverage(8 * 12);
            lengths[CreatureConstants.Horse_Light][CreatureConstants.Horse_Light] = GetMultiplierFromAverage(8 * 121);
            lengths[CreatureConstants.Horse_Heavy_War][GenderConstants.Female] = GetBaseFromAverage(8 * 12);
            lengths[CreatureConstants.Horse_Heavy_War][GenderConstants.Male] = GetBaseFromAverage(8 * 12);
            lengths[CreatureConstants.Horse_Heavy_War][CreatureConstants.Horse_Heavy_War] = GetMultiplierFromAverage(8 * 12);
            lengths[CreatureConstants.Horse_Light_War][GenderConstants.Female] = GetBaseFromAverage(8 * 12);
            lengths[CreatureConstants.Horse_Light_War][GenderConstants.Male] = GetBaseFromAverage(8 * 12);
            lengths[CreatureConstants.Horse_Light_War][CreatureConstants.Horse_Light_War] = GetMultiplierFromAverage(8 * 121);
            lengths[CreatureConstants.Human][GenderConstants.Female] = "0";
            lengths[CreatureConstants.Human][GenderConstants.Male] = "0";
            lengths[CreatureConstants.Human][CreatureConstants.Human] = "0";
            lengths[CreatureConstants.IceDevil_Gelugon][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.IceDevil_Gelugon][CreatureConstants.IceDevil_Gelugon] = "0";
            lengths[CreatureConstants.Imp][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.Imp][CreatureConstants.Imp] = "0";
            lengths[CreatureConstants.Kobold][GenderConstants.Female] = "0";
            lengths[CreatureConstants.Kobold][GenderConstants.Male] = "0";
            lengths[CreatureConstants.Kobold][CreatureConstants.Kobold] = "0";
            lengths[CreatureConstants.Lemure][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.Lemure][CreatureConstants.Lemure] = "0";
            lengths[CreatureConstants.Leonal][GenderConstants.Female] = "0";
            lengths[CreatureConstants.Leonal][GenderConstants.Male] = "0";
            lengths[CreatureConstants.Leonal][CreatureConstants.Leonal] = "0";
            lengths[CreatureConstants.Lizardfolk][GenderConstants.Female] = "0";
            lengths[CreatureConstants.Lizardfolk][GenderConstants.Male] = "0";
            lengths[CreatureConstants.Lizardfolk][CreatureConstants.Lizardfolk] = "0";
            lengths[CreatureConstants.Locathah][GenderConstants.Female] = "0";
            lengths[CreatureConstants.Locathah][GenderConstants.Male] = "0";
            lengths[CreatureConstants.Locathah][CreatureConstants.Locathah] = "0";
            //Source: https://forgottenrealms.fandom.com/wiki/Marilith
            lengths[CreatureConstants.Marilith][GenderConstants.Female] = GetBaseFromAverage(20 * 12);
            lengths[CreatureConstants.Marilith][CreatureConstants.Marilith] = GetMultiplierFromAverage(20 * 12);
            lengths[CreatureConstants.Mephit_Air][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.Mephit_Air][GenderConstants.Female] = "0";
            lengths[CreatureConstants.Mephit_Air][GenderConstants.Male] = "0";
            lengths[CreatureConstants.Mephit_Air][CreatureConstants.Mephit_Air] = "0";
            lengths[CreatureConstants.Mephit_Dust][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.Mephit_Dust][GenderConstants.Female] = "0";
            lengths[CreatureConstants.Mephit_Dust][GenderConstants.Male] = "0";
            lengths[CreatureConstants.Mephit_Dust][CreatureConstants.Mephit_Dust] = "0";
            lengths[CreatureConstants.Mephit_Earth][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.Mephit_Earth][GenderConstants.Female] = "0";
            lengths[CreatureConstants.Mephit_Earth][GenderConstants.Male] = "0";
            lengths[CreatureConstants.Mephit_Earth][CreatureConstants.Mephit_Earth] = "0";
            lengths[CreatureConstants.Mephit_Fire][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.Mephit_Fire][GenderConstants.Female] = "0";
            lengths[CreatureConstants.Mephit_Fire][GenderConstants.Male] = "0";
            lengths[CreatureConstants.Mephit_Fire][CreatureConstants.Mephit_Fire] = "0";
            lengths[CreatureConstants.Mephit_Ice][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.Mephit_Ice][GenderConstants.Female] = "0";
            lengths[CreatureConstants.Mephit_Ice][GenderConstants.Male] = "0";
            lengths[CreatureConstants.Mephit_Ice][CreatureConstants.Mephit_Ice] = "0";
            lengths[CreatureConstants.Mephit_Magma][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.Mephit_Magma][GenderConstants.Female] = "0";
            lengths[CreatureConstants.Mephit_Magma][GenderConstants.Male] = "0";
            lengths[CreatureConstants.Mephit_Magma][CreatureConstants.Mephit_Magma] = "0";
            lengths[CreatureConstants.Mephit_Ooze][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.Mephit_Ooze][GenderConstants.Female] = "0";
            lengths[CreatureConstants.Mephit_Ooze][GenderConstants.Male] = "0";
            lengths[CreatureConstants.Mephit_Ooze][CreatureConstants.Mephit_Ooze] = "0";
            lengths[CreatureConstants.Mephit_Salt][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.Mephit_Salt][GenderConstants.Female] = "0";
            lengths[CreatureConstants.Mephit_Salt][GenderConstants.Male] = "0";
            lengths[CreatureConstants.Mephit_Salt][CreatureConstants.Mephit_Salt] = "0";
            lengths[CreatureConstants.Mephit_Steam][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.Mephit_Steam][GenderConstants.Female] = "0";
            lengths[CreatureConstants.Mephit_Steam][GenderConstants.Male] = "0";
            lengths[CreatureConstants.Mephit_Steam][CreatureConstants.Mephit_Steam] = "0";
            lengths[CreatureConstants.Mephit_Water][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.Mephit_Water][GenderConstants.Female] = "0";
            lengths[CreatureConstants.Mephit_Water][GenderConstants.Male] = "0";
            lengths[CreatureConstants.Mephit_Water][CreatureConstants.Mephit_Water] = "0";
            lengths[CreatureConstants.Merfolk][GenderConstants.Female] = GetBaseFromAverage(8 * 12);
            lengths[CreatureConstants.Merfolk][GenderConstants.Male] = GetBaseFromAverage(8 * 12);
            lengths[CreatureConstants.Merfolk][CreatureConstants.Merfolk] = GetMultiplierFromAverage(8 * 12);
            lengths[CreatureConstants.Minotaur][GenderConstants.Female] = "0";
            lengths[CreatureConstants.Minotaur][GenderConstants.Male] = "0";
            lengths[CreatureConstants.Minotaur][CreatureConstants.Minotaur] = "0";
            lengths[CreatureConstants.Nalfeshnee][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.Nalfeshnee][CreatureConstants.Nalfeshnee] = "0";
            lengths[CreatureConstants.Ogre][GenderConstants.Female] = "0";
            lengths[CreatureConstants.Ogre][GenderConstants.Male] = "0";
            lengths[CreatureConstants.Ogre][CreatureConstants.Ogre] = "0";
            lengths[CreatureConstants.Ogre_Merrow][GenderConstants.Female] = "0";
            lengths[CreatureConstants.Ogre_Merrow][GenderConstants.Male] = "0";
            lengths[CreatureConstants.Ogre_Merrow][CreatureConstants.Ogre] = "0";
            lengths[CreatureConstants.OgreMage][GenderConstants.Female] = "0";
            lengths[CreatureConstants.OgreMage][GenderConstants.Male] = "0";
            lengths[CreatureConstants.OgreMage][CreatureConstants.OgreMage] = "0";
            lengths[CreatureConstants.Orc][GenderConstants.Female] = "0";
            lengths[CreatureConstants.Orc][GenderConstants.Male] = "0";
            lengths[CreatureConstants.Orc][CreatureConstants.Orc] = "0";
            lengths[CreatureConstants.Orc_Half][GenderConstants.Female] = "0";
            lengths[CreatureConstants.Orc_Half][GenderConstants.Male] = "0";
            lengths[CreatureConstants.Orc_Half][CreatureConstants.Orc_Half] = "0";
            lengths[CreatureConstants.PitFiend][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.PitFiend][CreatureConstants.PitFiend] = "0";
            lengths[CreatureConstants.Pixie][GenderConstants.Female] = "0";
            lengths[CreatureConstants.Pixie][GenderConstants.Male] = "0";
            lengths[CreatureConstants.Pixie][CreatureConstants.Pixie] = "0";
            lengths[CreatureConstants.Pixie_WithIrresistibleDance][GenderConstants.Female] = "0";
            lengths[CreatureConstants.Pixie_WithIrresistibleDance][GenderConstants.Male] = "0";
            lengths[CreatureConstants.Pixie_WithIrresistibleDance][CreatureConstants.Pixie_WithIrresistibleDance] = "0";
            lengths[CreatureConstants.Quasit][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.Quasit][CreatureConstants.Quasit] = "0";
            //Source: https://forgottenrealms.fandom.com/wiki/Retriever
            lengths[CreatureConstants.Retriever][GenderConstants.Agender] = GetBaseFromAverage(14 * 12);
            lengths[CreatureConstants.Retriever][CreatureConstants.Retriever] = GetMultiplierFromAverage(14 * 12);
            lengths[CreatureConstants.Sahuagin][GenderConstants.Female] = "0";
            lengths[CreatureConstants.Sahuagin][GenderConstants.Male] = "0";
            lengths[CreatureConstants.Sahuagin][CreatureConstants.Locathah] = "0";
            lengths[CreatureConstants.Sahuagin_Malenti][GenderConstants.Female] = "0";
            lengths[CreatureConstants.Sahuagin_Malenti][GenderConstants.Male] = "0";
            lengths[CreatureConstants.Sahuagin_Malenti][CreatureConstants.Locathah] = "0";
            lengths[CreatureConstants.Sahuagin_Mutant][GenderConstants.Female] = "0";
            lengths[CreatureConstants.Sahuagin_Mutant][GenderConstants.Male] = "0";
            lengths[CreatureConstants.Sahuagin_Mutant][CreatureConstants.Locathah] = "0";
            //Source: ??? - Numbers are most likely length
            lengths[CreatureConstants.Salamander_Flamebrother][GenderConstants.Agender] = GetBaseFromRange(24, 48);
            lengths[CreatureConstants.Salamander_Flamebrother][CreatureConstants.Salamander_Flamebrother] = GetMultiplierFromRange(24, 48);
            lengths[CreatureConstants.Salamander_Average][GenderConstants.Agender] = GetBaseFromRange(48, 8 * 12);
            lengths[CreatureConstants.Salamander_Average][CreatureConstants.Salamander_Average] = GetMultiplierFromRange(48, 8 * 12);
            lengths[CreatureConstants.Salamander_Noble][GenderConstants.Agender] = GetBaseFromRange(8 * 12, 16 * 12);
            lengths[CreatureConstants.Salamander_Noble][CreatureConstants.Salamander_Noble] = GetMultiplierFromRange(8 * 12, 16 * 12);
            lengths[CreatureConstants.Satyr][GenderConstants.Male] = "0";
            lengths[CreatureConstants.Satyr][CreatureConstants.Satyr] = "0";
            lengths[CreatureConstants.Satyr_WithPipes][GenderConstants.Male] = "0";
            lengths[CreatureConstants.Satyr_WithPipes][CreatureConstants.Satyr] = "0";
            lengths[CreatureConstants.SeaHag][GenderConstants.Female] = "0";
            lengths[CreatureConstants.SeaHag][CreatureConstants.SeaHag] = "0";
            lengths[CreatureConstants.Succubus][GenderConstants.Female] = "0";
            lengths[CreatureConstants.Succubus][GenderConstants.Male] = "0";
            lengths[CreatureConstants.Succubus][CreatureConstants.Succubus] = "0";
            lengths[CreatureConstants.Tiefling][GenderConstants.Female] = "0";
            lengths[CreatureConstants.Tiefling][GenderConstants.Male] = "0";
            lengths[CreatureConstants.Tiefling][CreatureConstants.Tiefling] = "0";
            //Source: https://forgottenrealms.fandom.com/wiki/Tojanida
            lengths[CreatureConstants.Tojanida_Juvenile][GenderConstants.Agender] = GetBaseFromAverage(3 * 12);
            lengths[CreatureConstants.Tojanida_Juvenile][CreatureConstants.Tojanida_Juvenile] = GetMultiplierFromAverage(3 * 12);
            lengths[CreatureConstants.Tojanida_Adult][GenderConstants.Agender] = GetBaseFromAverage(6 * 12);
            lengths[CreatureConstants.Tojanida_Adult][CreatureConstants.Tojanida_Adult] = GetMultiplierFromAverage(6 * 12);
            lengths[CreatureConstants.Tojanida_Elder][GenderConstants.Agender] = GetBaseFromAverage(9 * 12);
            lengths[CreatureConstants.Tojanida_Elder][CreatureConstants.Tojanida_Elder] = GetMultiplierFromAverage(9 * 12);
            lengths[CreatureConstants.Vrock][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.Vrock][CreatureConstants.Vrock] = "0";
            //Source: https://www.d20srd.org/srd/monsters/whale.htm
            lengths[CreatureConstants.Whale_Baleen][GenderConstants.Female] = GetBaseFromRange(30 * 12, 60 * 12);
            lengths[CreatureConstants.Whale_Baleen][GenderConstants.Male] = GetBaseFromRange(30 * 12, 60 * 12);
            lengths[CreatureConstants.Whale_Baleen][CreatureConstants.Whale_Baleen] = GetMultiplierFromRange(30 * 12, 60 * 12);
            //Source: https://www.d20srd.org/srd/monsters/whale.htm
            lengths[CreatureConstants.Whale_Cachalot][GenderConstants.Female] = GetBaseFromAverage(60 * 12);
            lengths[CreatureConstants.Whale_Cachalot][GenderConstants.Male] = GetBaseFromAverage(60 * 12);
            lengths[CreatureConstants.Whale_Cachalot][CreatureConstants.Whale_Cachalot] = GetMultiplierFromAverage(60 * 12);
            //Source: https://www.d20srd.org/srd/monsters/whale.htm
            lengths[CreatureConstants.Whale_Orca][GenderConstants.Female] = GetBaseFromAverage(30 * 12);
            lengths[CreatureConstants.Whale_Orca][GenderConstants.Male] = GetBaseFromAverage(30 * 12);
            lengths[CreatureConstants.Whale_Orca][CreatureConstants.Whale_Orca] = GetMultiplierFromAverage(30 * 12);
            //Source: https://www.dimensions.com/element/gray-wolf
            lengths[CreatureConstants.Wolf][GenderConstants.Female] = GetBaseFromRange(40, 72);
            lengths[CreatureConstants.Wolf][GenderConstants.Male] = GetBaseFromRange(40, 72);
            lengths[CreatureConstants.Wolf][CreatureConstants.Wolf] = GetBaseFromRange(40, 72);
            //Source: https://forgottenrealms.fandom.com/wiki/Xorn
            lengths[CreatureConstants.Xorn_Minor][GenderConstants.Agender] = GetBaseFromAverage(3 * 12);
            lengths[CreatureConstants.Xorn_Minor][CreatureConstants.Xorn_Minor] = GetMultiplierFromAverage(3 * 12);
            lengths[CreatureConstants.Xorn_Average][GenderConstants.Agender] = GetBaseFromAverage(5 * 12);
            lengths[CreatureConstants.Xorn_Average][CreatureConstants.Xorn_Average] = GetMultiplierFromAverage(5 * 12);
            lengths[CreatureConstants.Xorn_Elder][GenderConstants.Agender] = GetBaseFromAverage(8 * 12);
            lengths[CreatureConstants.Xorn_Elder][CreatureConstants.Xorn_Elder] = GetMultiplierFromAverage(8 * 12);

            return lengths;
        }

        public static IEnumerable CreatureLengthsData => GetCreatureLengths().Select(t => new TestCaseData(t.Key, t.Value));

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
            var heights = GetCreatureLengths();

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
        [TestCase(CreatureConstants.Bear_Brown, GenderConstants.Male, 7 * 12, 8 * 12)]
        [TestCase(CreatureConstants.Bear_Brown, GenderConstants.Female, 5 * 12 + 6, 6 * 12 + 6)]
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
            var heights = GetCreatureLengths();

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
            var lengths = GetCreatureLengths();
            var creatures = CreatureConstants.GetAll();

            foreach (var creature in creatures)
            {
                Assert.That(lengths[creature][creature], Is.Not.Empty);
                Assert.That(heights[creature][creature], Is.Not.Empty);

                if (lengths[creature][creature] == "0")
                    Assert.That(heights[creature][creature], Is.Not.EqualTo("0"), creature);
            }
        }
    }
}