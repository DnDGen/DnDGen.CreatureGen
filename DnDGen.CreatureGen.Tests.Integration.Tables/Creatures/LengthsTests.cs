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
            Assert.That(typesAndRolls.Keys, Is.EquivalentTo(genders.Union(new[] { name })).And.Not.Empty, $"TEST DATA: {name}");

            foreach (var roll in typesAndRolls.Values)
            {
                var isValid = dice.Roll(roll).IsValid();
                Assert.That(isValid, Is.True, roll);
            }

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
            lengths[CreatureConstants.Angel_AstralDeva][GenderConstants.Female] = "0";
            lengths[CreatureConstants.Angel_AstralDeva][GenderConstants.Male] = "0";
            lengths[CreatureConstants.Angel_AstralDeva][CreatureConstants.Angel_AstralDeva] = "0";
            lengths[CreatureConstants.Angel_Planetar][GenderConstants.Female] = "0";
            lengths[CreatureConstants.Angel_Planetar][GenderConstants.Male] = "0";
            lengths[CreatureConstants.Angel_Planetar][CreatureConstants.Angel_Planetar] = "0";
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
            //Source: https://www.d20srd.org/srd/monsters/giantAnt.htm
            lengths[CreatureConstants.Ant_Giant_Worker][GenderConstants.Male] = GetBaseFromAverage(6 * 12);
            lengths[CreatureConstants.Ant_Giant_Worker][CreatureConstants.Ant_Giant_Worker] = GetMultiplierFromAverage(6 * 12);
            lengths[CreatureConstants.Ant_Giant_Soldier][GenderConstants.Male] = GetBaseFromAverage(6 * 12);
            lengths[CreatureConstants.Ant_Giant_Soldier][CreatureConstants.Ant_Giant_Soldier] = GetMultiplierFromAverage(6 * 12);
            lengths[CreatureConstants.Ant_Giant_Queen][GenderConstants.Female] = GetBaseFromAverage(9 * 12);
            lengths[CreatureConstants.Ant_Giant_Queen][CreatureConstants.Ant_Giant_Queen] = GetMultiplierFromAverage(9 * 12);
            //Source: https://www.dimensions.com/element/eastern-lowland-gorilla-gorilla-beringei-graueri
            lengths[CreatureConstants.Ape][GenderConstants.Female] = GetBaseFromRange(37, 42);
            lengths[CreatureConstants.Ape][GenderConstants.Male] = GetBaseFromRange(37, 42);
            lengths[CreatureConstants.Ape][CreatureConstants.Ape] = GetMultiplierFromRange(37, 42);
            //Multiplying up from normal ape, where Dire is about 1.5x the dimensions of the normal
            lengths[CreatureConstants.Ape_Dire][GenderConstants.Female] = GetBaseFromRange(55, 63);
            lengths[CreatureConstants.Ape_Dire][GenderConstants.Male] = GetBaseFromRange(55, 63);
            lengths[CreatureConstants.Ape_Dire][CreatureConstants.Ape_Dire] = GetMultiplierFromRange(55, 63);
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
            //Source: https://forgottenrealms.fandom.com/wiki/Assassin_vine
            lengths[CreatureConstants.AssassinVine][GenderConstants.Agender] = GetBaseFromAverage(20 * 12);
            lengths[CreatureConstants.AssassinVine][CreatureConstants.AssassinVine] = GetMultiplierFromAverage(20 * 12);
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
            //Source: https://www.dimensions.com/element/mandrill-mandrillus-sphinx
            //https://www.d20srd.org/srd/monsters/baboon.htm (male)
            lengths[CreatureConstants.Baboon][GenderConstants.Female] = GetBaseFromRange(21, 38);
            lengths[CreatureConstants.Baboon][GenderConstants.Male] = GetBaseFromRange(2 * 12, 4 * 12);
            lengths[CreatureConstants.Baboon][CreatureConstants.Baboon] = GetMultiplierFromRange(2 * 12, 4 * 12);
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
            //Source: https://forgottenrealms.fandom.com/wiki/Basilisk
            lengths[CreatureConstants.Basilisk][GenderConstants.Female] = GetBaseFromRange(11 * 12, 13 * 12);
            lengths[CreatureConstants.Basilisk][GenderConstants.Male] = GetBaseFromRange(11 * 12, 13 * 12);
            lengths[CreatureConstants.Basilisk][CreatureConstants.Basilisk] = GetMultiplierFromRange(11 * 12, 13 * 12);
            //Scaling up. Since 1 size category bigger, just doubling everything
            lengths[CreatureConstants.Basilisk_Greater][GenderConstants.Female] = GetBaseFromRange(22 * 12, 26 * 12);
            lengths[CreatureConstants.Basilisk_Greater][GenderConstants.Male] = GetBaseFromRange(22 * 12, 26 * 12);
            lengths[CreatureConstants.Basilisk_Greater][CreatureConstants.Basilisk_Greater] = GetMultiplierFromRange(22 * 12, 26 * 12);
            //Source: https://www.dimensions.com/element/little-brown-bat-myotis-lucifugus
            lengths[CreatureConstants.Bat][GenderConstants.Female] = GetBaseFromRange(3, 4);
            lengths[CreatureConstants.Bat][GenderConstants.Male] = GetBaseFromRange(3, 4);
            lengths[CreatureConstants.Bat][CreatureConstants.Bat] = GetMultiplierFromRange(3, 4);
            //Scaled up from bat, x15 based on wingspan
            lengths[CreatureConstants.Bat_Dire][GenderConstants.Female] = GetBaseFromRange(45, 60);
            lengths[CreatureConstants.Bat_Dire][GenderConstants.Male] = GetBaseFromRange(45, 60);
            lengths[CreatureConstants.Bat_Dire][CreatureConstants.Bat_Dire] = GetMultiplierFromRange(45, 60);
            //Source: https://www.d20srd.org/srd/monsters/swarm.htm
            lengths[CreatureConstants.Bat_Swarm][GenderConstants.Agender] = GetBaseFromAverage(10 * 12);
            lengths[CreatureConstants.Bat_Swarm][CreatureConstants.Bat_Swarm] = GetMultiplierFromAverage(10 * 12);
            //Source: https://www.dimensions.com/element/american-black-bear
            lengths[CreatureConstants.Bear_Black][GenderConstants.Female] = GetBaseFromUpTo(5 * 12);
            lengths[CreatureConstants.Bear_Black][GenderConstants.Male] = GetBaseFromUpTo(5 * 12);
            lengths[CreatureConstants.Bear_Black][CreatureConstants.Bear_Black] = GetMultiplierFromUpTo(5 * 12);
            //Source: https://www.dimensions.com/element/grizzly-bear
            lengths[CreatureConstants.Bear_Brown][GenderConstants.Female] = GetBaseFromRange(5 * 12 + 6, 6 * 12 + 6);
            lengths[CreatureConstants.Bear_Brown][GenderConstants.Male] = GetBaseFromRange(7 * 12, 8 * 12);
            lengths[CreatureConstants.Bear_Brown][CreatureConstants.Bear_Brown] = GetMultiplierFromRange(7 * 12, 8 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Dire_bear
            lengths[CreatureConstants.Bear_Dire][GenderConstants.Female] = GetBaseFromAverage(12 * 12);
            lengths[CreatureConstants.Bear_Dire][GenderConstants.Male] = GetBaseFromAverage(12 * 12);
            lengths[CreatureConstants.Bear_Dire][CreatureConstants.Bear_Dire] = GetMultiplierFromAverage(12 * 12);
            //Source: https://www.dimensions.com/element/polar-bears
            lengths[CreatureConstants.Bear_Polar][GenderConstants.Female] = GetBaseFromRange(7 * 12 + 10, 9 * 12 + 2);
            lengths[CreatureConstants.Bear_Polar][GenderConstants.Male] = GetBaseFromRange(8 * 12 + 6, 9 * 12 + 10);
            lengths[CreatureConstants.Bear_Polar][CreatureConstants.Bear_Polar] = GetMultiplierFromRange(8 * 12 + 6, 9 * 12 + 10);
            lengths[CreatureConstants.BeardedDevil_Barbazu][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.BeardedDevil_Barbazu][CreatureConstants.BeardedDevil_Barbazu] = "0";
            //Source: https://forgottenrealms.fandom.com/wiki/Bebilith
            lengths[CreatureConstants.Bebilith][GenderConstants.Agender] = GetBaseFromAverage(14 * 12);
            lengths[CreatureConstants.Bebilith][CreatureConstants.Bebilith] = GetMultiplierFromAverage(14 * 12);
            //Source: https://www.d20srd.org/srd/monsters/giantBee.htm
            lengths[CreatureConstants.Bee_Giant][GenderConstants.Male] = GetBaseFromAverage(5 * 12);
            lengths[CreatureConstants.Bee_Giant][CreatureConstants.Bee_Giant] = GetMultiplierFromAverage(5 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Behir
            lengths[CreatureConstants.Behir][GenderConstants.Female] = GetBaseFromAverage(40 * 12);
            lengths[CreatureConstants.Behir][GenderConstants.Male] = GetBaseFromAverage(40 * 12);
            lengths[CreatureConstants.Behir][CreatureConstants.Behir] = GetMultiplierFromAverage(40 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Beholder
            lengths[CreatureConstants.Beholder][GenderConstants.Agender] = GetBaseFromAverage(8 * 12);
            lengths[CreatureConstants.Beholder][CreatureConstants.Beholder] = GetMultiplierFromAverage(8 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Gauth
            lengths[CreatureConstants.Beholder_Gauth][GenderConstants.Agender] = GetBaseFromRange(4 * 12, 6 * 12);
            lengths[CreatureConstants.Beholder_Gauth][CreatureConstants.Beholder_Gauth] = GetMultiplierFromRange(4 * 12, 6 * 12);
            lengths[CreatureConstants.Belker][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.Belker][CreatureConstants.Belker] = "0";
            //Source: https://www.dimensions.com/element/american-bison-bison-bison
            lengths[CreatureConstants.Bison][GenderConstants.Female] = GetBaseFromRange(86, 114);
            lengths[CreatureConstants.Bison][GenderConstants.Male] = GetBaseFromRange(86, 114);
            lengths[CreatureConstants.Bison][CreatureConstants.Bison] = GetMultiplierFromRange(86, 114);
            //Source: https://forgottenrealms.fandom.com/wiki/Black_pudding
            lengths[CreatureConstants.BlackPudding][GenderConstants.Agender] = GetBaseFromAverage(15 * 12);
            lengths[CreatureConstants.BlackPudding][CreatureConstants.BlackPudding] = GetMultiplierFromAverage(15 * 12);
            //Elder is a size category up, so double dimensions
            lengths[CreatureConstants.BlackPudding_Elder][GenderConstants.Agender] = GetBaseFromAverage(20 * 12);
            lengths[CreatureConstants.BlackPudding_Elder][CreatureConstants.BlackPudding_Elder] = GetMultiplierFromAverage(20 * 12);
            //Source: https://www.dimensions.com/search?query=dog
            lengths[CreatureConstants.BlinkDog][GenderConstants.Female] = GetBaseFromRange(28, 47);
            lengths[CreatureConstants.BlinkDog][GenderConstants.Male] = GetBaseFromRange(28, 47);
            lengths[CreatureConstants.BlinkDog][CreatureConstants.Dog_Riding] = GetMultiplierFromRange(28, 47);
            //Source: https://www.dimensions.com/element/wild-boar
            lengths[CreatureConstants.Boar][GenderConstants.Female] = GetBaseFromRange(5 * 12, 6 * 12);
            lengths[CreatureConstants.Boar][GenderConstants.Male] = GetBaseFromRange(5 * 12, 6 * 12);
            lengths[CreatureConstants.Boar][CreatureConstants.Boar] = GetMultiplierFromRange(5 * 12, 6 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Dire_boar
            lengths[CreatureConstants.Boar_Dire][GenderConstants.Female] = GetBaseFromAverage(12 * 12);
            lengths[CreatureConstants.Boar_Dire][GenderConstants.Male] = GetBaseFromAverage(12 * 12);
            lengths[CreatureConstants.Boar_Dire][CreatureConstants.Boar] = GetMultiplierFromAverage(12 * 12);
            lengths[CreatureConstants.Bodak][GenderConstants.Female] = "0";
            lengths[CreatureConstants.Bodak][GenderConstants.Male] = "0";
            lengths[CreatureConstants.Bodak][CreatureConstants.Bodak] = "0";
            //Source: https://www.d20srd.org/srd/monsters/giantBombardierBeetle.htm
            lengths[CreatureConstants.BombardierBeetle_Giant][GenderConstants.Female] = GetBaseFromAverage(6 * 12);
            lengths[CreatureConstants.BombardierBeetle_Giant][GenderConstants.Male] = GetBaseFromAverage(6 * 12);
            lengths[CreatureConstants.BombardierBeetle_Giant][CreatureConstants.BombardierBeetle_Giant] = GetMultiplierFromAverage(6 * 12);
            lengths[CreatureConstants.BoneDevil_Osyluth][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.BoneDevil_Osyluth][CreatureConstants.BoneDevil_Osyluth] = "0";
            lengths[CreatureConstants.Bralani][GenderConstants.Female] = "0";
            lengths[CreatureConstants.Bralani][GenderConstants.Male] = "0";
            lengths[CreatureConstants.Bralani][CreatureConstants.Bralani] = "0";
            lengths[CreatureConstants.Bugbear][GenderConstants.Female] = "0";
            lengths[CreatureConstants.Bugbear][GenderConstants.Male] = "0";
            lengths[CreatureConstants.Bugbear][CreatureConstants.Bugbear] = "0";
            //Source: https://forgottenrealms.fandom.com/wiki/Bulette
            lengths[CreatureConstants.Bulette][GenderConstants.Female] = GetBaseFromAverage(12 * 12);
            lengths[CreatureConstants.Bulette][GenderConstants.Male] = GetBaseFromAverage(12 * 12);
            lengths[CreatureConstants.Bulette][CreatureConstants.Bulette] = GetMultiplierFromAverage(12 * 12);
            //Source: https://www.dimensions.com/element/bactrian-camel
            lengths[CreatureConstants.Camel_Bactrian][GenderConstants.Female] = GetBaseFromRange(89, 138);
            lengths[CreatureConstants.Camel_Bactrian][GenderConstants.Male] = GetBaseFromRange(89, 138);
            lengths[CreatureConstants.Camel_Bactrian][CreatureConstants.Camel_Bactrian] = GetMultiplierFromRange(89, 138);
            //Source: https://www.dimensions.com/element/dromedary-camel
            lengths[CreatureConstants.Camel_Dromedary][GenderConstants.Female] = GetBaseFromRange(86, 134);
            lengths[CreatureConstants.Camel_Dromedary][GenderConstants.Male] = GetBaseFromRange(86, 134);
            lengths[CreatureConstants.Camel_Dromedary][CreatureConstants.Camel_Dromedary] = GetMultiplierFromRange(86, 134);
            //Source: https://forgottenrealms.fandom.com/wiki/Carrion_crawler
            lengths[CreatureConstants.CarrionCrawler][GenderConstants.Female] = GetBaseFromRange(9 * 12, 10 * 12);
            lengths[CreatureConstants.CarrionCrawler][GenderConstants.Male] = GetBaseFromRange(9 * 12, 10 * 12);
            lengths[CreatureConstants.CarrionCrawler][CreatureConstants.CarrionCrawler] = GetMultiplierFromRange(9 * 12, 10 * 12);
            //Source: https://www.dimensions.com/element/american-shorthair-cat
            lengths[CreatureConstants.Cat][GenderConstants.Female] = GetBaseFromRange(12, 15);
            lengths[CreatureConstants.Cat][GenderConstants.Male] = GetBaseFromRange(12, 15);
            lengths[CreatureConstants.Cat][CreatureConstants.Cat] = GetMultiplierFromRange(12, 15);
            //INFO: Using standard horse length
            lengths[CreatureConstants.Centaur][GenderConstants.Female] = GetBaseFromAverage(8 * 12);
            lengths[CreatureConstants.Centaur][GenderConstants.Male] = GetBaseFromAverage(8 * 12);
            lengths[CreatureConstants.Centaur][CreatureConstants.Centaur] = GetMultiplierFromAverage(8 * 12);
            //Source: https://www.d20srd.org/srd/monsters/swarm.htm
            lengths[CreatureConstants.Centipede_Swarm][GenderConstants.Agender] = GetBaseFromAverage(10 * 12);
            lengths[CreatureConstants.Centipede_Swarm][CreatureConstants.Centipede_Swarm] = GetMultiplierFromAverage(10 * 12);
            lengths[CreatureConstants.ChainDevil_Kyton][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.ChainDevil_Kyton][CreatureConstants.ChainDevil_Kyton] = "0";
            //Source: https://forgottenrealms.fandom.com/wiki/Chaos_beast
            lengths[CreatureConstants.ChaosBeast][GenderConstants.Agender] = GetBaseFromRange(3 * 12, 9 * 12);
            lengths[CreatureConstants.ChaosBeast][CreatureConstants.ChaosBeast] = GetMultiplierFromRange(3 * 12, 9 * 12);
            //Source: https://www.d20srd.org/srd/monsters/cheetah.htm
            lengths[CreatureConstants.Cheetah][GenderConstants.Female] = GetBaseFromRange(3 * 12, 5 * 12);
            lengths[CreatureConstants.Cheetah][GenderConstants.Male] = GetBaseFromRange(3 * 12, 5 * 12);
            lengths[CreatureConstants.Cheetah][CreatureConstants.Cheetah] = GetMultiplierFromRange(3 * 12, 5 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Chimera
            lengths[CreatureConstants.Chimera_Black][GenderConstants.Female] = GetBaseFromAverage(10 * 12);
            lengths[CreatureConstants.Chimera_Black][GenderConstants.Male] = GetBaseFromAverage(10 * 12);
            lengths[CreatureConstants.Chimera_Black][CreatureConstants.Chimera_Black] = GetMultiplierFromAverage(10 * 12);
            lengths[CreatureConstants.Chimera_Blue][GenderConstants.Female] = GetBaseFromAverage(10 * 12);
            lengths[CreatureConstants.Chimera_Blue][GenderConstants.Male] = GetBaseFromAverage(10 * 12);
            lengths[CreatureConstants.Chimera_Blue][CreatureConstants.Chimera_Blue] = GetMultiplierFromAverage(10 * 12);
            lengths[CreatureConstants.Chimera_Green][GenderConstants.Female] = GetBaseFromAverage(10 * 12);
            lengths[CreatureConstants.Chimera_Green][GenderConstants.Male] = GetBaseFromAverage(10 * 12);
            lengths[CreatureConstants.Chimera_Green][CreatureConstants.Chimera_Green] = GetMultiplierFromAverage(10 * 12);
            lengths[CreatureConstants.Chimera_Red][GenderConstants.Female] = GetBaseFromAverage(10 * 12);
            lengths[CreatureConstants.Chimera_Red][GenderConstants.Male] = GetBaseFromAverage(10 * 12);
            lengths[CreatureConstants.Chimera_Red][CreatureConstants.Chimera_Red] = GetMultiplierFromAverage(10 * 12);
            lengths[CreatureConstants.Chimera_White][GenderConstants.Female] = GetBaseFromAverage(10 * 12);
            lengths[CreatureConstants.Chimera_White][GenderConstants.Male] = GetBaseFromAverage(10 * 12);
            lengths[CreatureConstants.Chimera_White][CreatureConstants.Chimera_White] = GetMultiplierFromAverage(10 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Choker
            lengths[CreatureConstants.Choker][GenderConstants.Female] = "0";
            lengths[CreatureConstants.Choker][GenderConstants.Male] = "0";
            lengths[CreatureConstants.Choker][CreatureConstants.Choker] = "0";
            //Source: https://forgottenrealms.fandom.com/wiki/Chuul
            lengths[CreatureConstants.Chuul][GenderConstants.Female] = GetBaseFromAverage(8 * 12);
            lengths[CreatureConstants.Chuul][GenderConstants.Male] = GetBaseFromAverage(8 * 12);
            lengths[CreatureConstants.Chuul][CreatureConstants.Chuul] = GetMultiplierFromAverage(8 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Cloaker and https://www.mojobob.com/roleplay/monstrousmanual/c/cloaker.html
            lengths[CreatureConstants.Cloaker][GenderConstants.Agender] = GetBaseFromAverage(8 * 12);
            lengths[CreatureConstants.Cloaker][CreatureConstants.Cloaker] = GetMultiplierFromAverage(8 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Cockatrice
            lengths[CreatureConstants.Cockatrice][GenderConstants.Female] = "0";
            lengths[CreatureConstants.Cockatrice][GenderConstants.Male] = "0";
            lengths[CreatureConstants.Cockatrice][CreatureConstants.Cockatrice] = "0";
            //Source: https://forgottenrealms.fandom.com/wiki/Couatl
            lengths[CreatureConstants.Couatl][GenderConstants.Female] = GetBaseFromAverage(12 * 12);
            lengths[CreatureConstants.Couatl][GenderConstants.Male] = GetBaseFromAverage(12 * 12);
            lengths[CreatureConstants.Couatl][CreatureConstants.Couatl] = GetBaseFromAverage(12 * 12);
            //Source: https://www.d20srd.org/srd/monsters/sphinx.htm
            lengths[CreatureConstants.Criosphinx][GenderConstants.Male] = GetBaseFromAverage(10 * 12);
            lengths[CreatureConstants.Criosphinx][CreatureConstants.Criosphinx] = GetMultiplierFromAverage(10 * 12);
            //Source: https://www.d20srd.org/srd/monsters/crocodile.htm
            lengths[CreatureConstants.Crocodile][GenderConstants.Female] = GetBaseFromRange(11 * 12, 12 * 12);
            lengths[CreatureConstants.Crocodile][GenderConstants.Male] = GetBaseFromRange(11 * 12, 12 * 12);
            lengths[CreatureConstants.Crocodile][CreatureConstants.Crocodile] = GetMultiplierFromRange(11 * 12, 12 * 12);
            //Source: https://www.d20srd.org/srd/monsters/crocodileGiant.htm
            lengths[CreatureConstants.Crocodile_Giant][GenderConstants.Female] = GetBaseFromAtLeast(20 * 12);
            lengths[CreatureConstants.Crocodile_Giant][GenderConstants.Male] = GetBaseFromAtLeast(20 * 12);
            lengths[CreatureConstants.Crocodile_Giant][CreatureConstants.Crocodile_Giant] = GetMultiplierFromAtLeast(20 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Hydra
            lengths[CreatureConstants.Cryohydra_5Heads][GenderConstants.Female] = GetBaseFromAverage(20 * 12);
            lengths[CreatureConstants.Cryohydra_5Heads][GenderConstants.Male] = GetBaseFromAverage(20 * 12);
            lengths[CreatureConstants.Cryohydra_5Heads][CreatureConstants.Cryohydra_5Heads] = GetMultiplierFromAverage(20 * 12);
            lengths[CreatureConstants.Cryohydra_6Heads][GenderConstants.Female] = GetBaseFromAverage(20 * 12);
            lengths[CreatureConstants.Cryohydra_6Heads][GenderConstants.Male] = GetBaseFromAverage(20 * 12);
            lengths[CreatureConstants.Cryohydra_6Heads][CreatureConstants.Cryohydra_6Heads] = GetMultiplierFromAverage(20 * 12);
            lengths[CreatureConstants.Cryohydra_7Heads][GenderConstants.Female] = GetBaseFromAverage(20 * 12);
            lengths[CreatureConstants.Cryohydra_7Heads][GenderConstants.Male] = GetBaseFromAverage(20 * 12);
            lengths[CreatureConstants.Cryohydra_7Heads][CreatureConstants.Cryohydra_7Heads] = GetMultiplierFromAverage(20 * 12);
            lengths[CreatureConstants.Cryohydra_8Heads][GenderConstants.Female] = GetBaseFromAverage(20 * 12);
            lengths[CreatureConstants.Cryohydra_8Heads][GenderConstants.Male] = GetBaseFromAverage(20 * 12);
            lengths[CreatureConstants.Cryohydra_8Heads][CreatureConstants.Cryohydra_8Heads] = GetMultiplierFromAverage(20 * 12);
            lengths[CreatureConstants.Cryohydra_9Heads][GenderConstants.Female] = GetBaseFromAverage(20 * 12);
            lengths[CreatureConstants.Cryohydra_9Heads][GenderConstants.Male] = GetBaseFromAverage(20 * 12);
            lengths[CreatureConstants.Cryohydra_9Heads][CreatureConstants.Cryohydra_9Heads] = GetMultiplierFromAverage(20 * 12);
            lengths[CreatureConstants.Cryohydra_10Heads][GenderConstants.Female] = GetBaseFromAverage(20 * 12);
            lengths[CreatureConstants.Cryohydra_10Heads][GenderConstants.Male] = GetBaseFromAverage(20 * 12);
            lengths[CreatureConstants.Cryohydra_10Heads][CreatureConstants.Cryohydra_10Heads] = GetMultiplierFromAverage(20 * 12);
            lengths[CreatureConstants.Cryohydra_11Heads][GenderConstants.Female] = GetBaseFromAverage(20 * 12);
            lengths[CreatureConstants.Cryohydra_11Heads][GenderConstants.Male] = GetBaseFromAverage(20 * 12);
            lengths[CreatureConstants.Cryohydra_11Heads][CreatureConstants.Cryohydra_11Heads] = GetMultiplierFromAverage(20 * 12);
            lengths[CreatureConstants.Cryohydra_12Heads][GenderConstants.Female] = GetBaseFromAverage(20 * 12);
            lengths[CreatureConstants.Cryohydra_12Heads][GenderConstants.Male] = GetBaseFromAverage(20 * 12);
            lengths[CreatureConstants.Cryohydra_12Heads][CreatureConstants.Cryohydra_5Heads] = GetMultiplierFromAverage(20 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Darkmantle
            lengths[CreatureConstants.Darkmantle][GenderConstants.Hermaphrodite] = GetBaseFromAverage(4 * 12);
            lengths[CreatureConstants.Darkmantle][CreatureConstants.Darkmantle] = GetMultiplierFromAverage(4 * 12);
            //Source: https://www.dimensions.com/element/deinonychus-deinonychus-antirrhopus
            lengths[CreatureConstants.Deinonychus][GenderConstants.Female] = GetBaseFromRange(9 * 12, 16 * 12);
            lengths[CreatureConstants.Deinonychus][GenderConstants.Male] = GetBaseFromRange(9 * 12, 16 * 12);
            lengths[CreatureConstants.Deinonychus][CreatureConstants.Deinonychus] = GetMultiplierFromRange(9 * 12, 16 * 12);
            //Source: https://dungeonsdragons.fandom.com/wiki/Delver
            lengths[CreatureConstants.Delver][GenderConstants.Female] = GetBaseFromAverage(15 * 12);
            lengths[CreatureConstants.Delver][GenderConstants.Male] = GetBaseFromAverage(15 * 12);
            lengths[CreatureConstants.Delver][CreatureConstants.Delver] = GetMultiplierFromAverage(15 * 12);
            lengths[CreatureConstants.Derro][GenderConstants.Female] = "0";
            lengths[CreatureConstants.Derro][GenderConstants.Male] = "0";
            lengths[CreatureConstants.Derro][CreatureConstants.Derro] = "0";
            lengths[CreatureConstants.Derro_Sane][GenderConstants.Female] = "0";
            lengths[CreatureConstants.Derro_Sane][GenderConstants.Male] = "0";
            lengths[CreatureConstants.Derro_Sane][CreatureConstants.Derro_Sane] = "0";
            //Source: https://forgottenrealms.fandom.com/wiki/Destrachan
            lengths[CreatureConstants.Destrachan][GenderConstants.Female] = GetBaseFromAverage(10 * 12);
            lengths[CreatureConstants.Destrachan][GenderConstants.Male] = GetBaseFromAverage(10 * 12);
            lengths[CreatureConstants.Destrachan][CreatureConstants.Destrachan] = GetMultiplierFromAverage(10 * 12);
            lengths[CreatureConstants.Devourer][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.Devourer][CreatureConstants.Devourer] = "0";
            //Source: https://forgottenrealms.fandom.com/wiki/Digester
            lengths[CreatureConstants.Digester][GenderConstants.Female] = GetBaseFromAverage(7 * 12);
            lengths[CreatureConstants.Digester][GenderConstants.Male] = GetBaseFromAverage(7 * 12);
            lengths[CreatureConstants.Digester][CreatureConstants.Digester] = GetMultiplierFromAverage(7 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Displacer_beast
            lengths[CreatureConstants.DisplacerBeast][GenderConstants.Female] = GetBaseFromRange(8 * 12, 9 * 12);
            lengths[CreatureConstants.DisplacerBeast][GenderConstants.Male] = GetBaseFromRange(9 * 12, 12 * 12);
            lengths[CreatureConstants.DisplacerBeast][CreatureConstants.DisplacerBeast] = GetMultiplierFromRange(9 * 12, 12 * 12);
            lengths[CreatureConstants.DisplacerBeast_PackLord][GenderConstants.Female] = GetBaseFromAverage(20 * 12);
            lengths[CreatureConstants.DisplacerBeast_PackLord][GenderConstants.Male] = GetBaseFromAverage(20 * 12);
            lengths[CreatureConstants.DisplacerBeast_PackLord][CreatureConstants.DisplacerBeast_PackLord] = GetMultiplierFromAverage(20 * 12);
            lengths[CreatureConstants.Djinni][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.Djinni][GenderConstants.Female] = "0";
            lengths[CreatureConstants.Djinni][GenderConstants.Male] = "0";
            lengths[CreatureConstants.Djinni][CreatureConstants.Djinni] = "0";
            lengths[CreatureConstants.Djinni_Noble][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.Djinni_Noble][GenderConstants.Female] = "0";
            lengths[CreatureConstants.Djinni_Noble][GenderConstants.Male] = "0";
            lengths[CreatureConstants.Djinni_Noble][CreatureConstants.Djinni_Noble] = "0";
            //Source: https://www.dimensions.com/search?query=dog average of various dogs in the 20-50 pound weight range, including coyote
            lengths[CreatureConstants.Dog][GenderConstants.Female] = GetBaseFromRange(22, 39);
            lengths[CreatureConstants.Dog][GenderConstants.Male] = GetBaseFromRange(22, 39);
            lengths[CreatureConstants.Dog][CreatureConstants.Dog] = GetMultiplierFromRange(22, 39);
            //Source: https://www.dimensions.com/element/saint-bernard-dog M:43-47,F:40-44
            //https://www.dimensions.com/element/siberian-husky 30-35
            //https://www.dimensions.com/element/dogs-collie M:31-34,F:29-32
            lengths[CreatureConstants.Dog_Riding][GenderConstants.Female] = GetBaseFromRange(29, 44);
            lengths[CreatureConstants.Dog_Riding][GenderConstants.Male] = GetBaseFromRange(32, 47);
            lengths[CreatureConstants.Dog_Riding][CreatureConstants.Dog_Riding] = GetMultiplierFromRange(32, 47);
            //Source: https://www.dimensions.com/element/donkey-equus-africanus-asinus
            lengths[CreatureConstants.Donkey][GenderConstants.Female] = GetBaseFromRange(57, 76);
            lengths[CreatureConstants.Donkey][GenderConstants.Male] = GetBaseFromRange(57, 76);
            lengths[CreatureConstants.Donkey][CreatureConstants.Donkey] = GetMultiplierFromRange(57, 76);
            lengths[CreatureConstants.Doppelganger][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.Doppelganger][CreatureConstants.Doppelganger] = "0";
            //Source: Draconomicon
            lengths[CreatureConstants.Dragon_Black_Wyrmling][GenderConstants.Female] = GetBaseFromAverage(4 * 12);
            lengths[CreatureConstants.Dragon_Black_Wyrmling][GenderConstants.Male] = GetBaseFromAverage(4 * 12);
            lengths[CreatureConstants.Dragon_Black_Wyrmling][CreatureConstants.Dragon_Black_Wyrmling] = GetMultiplierFromAverage(4 * 12);
            lengths[CreatureConstants.Dragon_Black_VeryYoung][GenderConstants.Female] = GetBaseFromAverage(8 * 12);
            lengths[CreatureConstants.Dragon_Black_VeryYoung][GenderConstants.Male] = GetBaseFromAverage(8 * 12);
            lengths[CreatureConstants.Dragon_Black_VeryYoung][CreatureConstants.Dragon_Black_VeryYoung] = GetMultiplierFromAverage(8 * 12);
            lengths[CreatureConstants.Dragon_Black_Young][GenderConstants.Female] = GetBaseFromAverage(16 * 12);
            lengths[CreatureConstants.Dragon_Black_Young][GenderConstants.Male] = GetBaseFromAverage(16 * 12);
            lengths[CreatureConstants.Dragon_Black_Young][CreatureConstants.Dragon_Black_Young] = GetMultiplierFromAverage(16 * 12);
            lengths[CreatureConstants.Dragon_Black_Juvenile][GenderConstants.Female] = GetBaseFromAverage(16 * 12);
            lengths[CreatureConstants.Dragon_Black_Juvenile][GenderConstants.Male] = GetBaseFromAverage(16 * 12);
            lengths[CreatureConstants.Dragon_Black_Juvenile][CreatureConstants.Dragon_Black_Juvenile] = GetMultiplierFromAverage(16 * 12);
            lengths[CreatureConstants.Dragon_Black_YoungAdult][GenderConstants.Female] = GetBaseFromAverage(31 * 12);
            lengths[CreatureConstants.Dragon_Black_YoungAdult][GenderConstants.Male] = GetBaseFromAverage(31 * 12);
            lengths[CreatureConstants.Dragon_Black_YoungAdult][CreatureConstants.Dragon_Black_YoungAdult] = GetMultiplierFromAverage(31 * 12);
            lengths[CreatureConstants.Dragon_Black_Adult][GenderConstants.Female] = GetBaseFromAverage(31 * 12);
            lengths[CreatureConstants.Dragon_Black_Adult][GenderConstants.Male] = GetBaseFromAverage(31 * 12);
            lengths[CreatureConstants.Dragon_Black_Adult][CreatureConstants.Dragon_Black_Adult] = GetMultiplierFromAverage(31 * 12);
            lengths[CreatureConstants.Dragon_Black_MatureAdult][GenderConstants.Female] = GetBaseFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_Black_MatureAdult][GenderConstants.Male] = GetBaseFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_Black_MatureAdult][CreatureConstants.Dragon_Black_MatureAdult] = GetMultiplierFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_Black_Old][GenderConstants.Female] = GetBaseFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_Black_Old][GenderConstants.Male] = GetBaseFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_Black_Old][CreatureConstants.Dragon_Black_Old] = GetMultiplierFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_Black_VeryOld][GenderConstants.Female] = GetBaseFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_Black_VeryOld][GenderConstants.Male] = GetBaseFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_Black_VeryOld][CreatureConstants.Dragon_Black_VeryOld] = GetMultiplierFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_Black_Ancient][GenderConstants.Female] = GetBaseFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_Black_Ancient][GenderConstants.Male] = GetBaseFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_Black_Ancient][CreatureConstants.Dragon_Black_Ancient] = GetMultiplierFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_Black_Wyrm][GenderConstants.Female] = GetBaseFromAverage(85 * 12);
            lengths[CreatureConstants.Dragon_Black_Wyrm][GenderConstants.Male] = GetBaseFromAverage(85 * 12);
            lengths[CreatureConstants.Dragon_Black_Wyrm][CreatureConstants.Dragon_Black_Wyrm] = GetMultiplierFromAverage(85 * 12);
            lengths[CreatureConstants.Dragon_Black_GreatWyrm][GenderConstants.Female] = GetBaseFromAverage(85 * 12);
            lengths[CreatureConstants.Dragon_Black_GreatWyrm][GenderConstants.Male] = GetBaseFromAverage(85 * 12);
            lengths[CreatureConstants.Dragon_Black_GreatWyrm][CreatureConstants.Dragon_Black_GreatWyrm] = GetMultiplierFromAverage(85 * 12);
            lengths[CreatureConstants.Dragon_Blue_Wyrmling][GenderConstants.Female] = GetBaseFromAverage(8 * 12);
            lengths[CreatureConstants.Dragon_Blue_Wyrmling][GenderConstants.Male] = GetBaseFromAverage(8 * 12);
            lengths[CreatureConstants.Dragon_Blue_Wyrmling][CreatureConstants.Dragon_Blue_Wyrmling] = GetMultiplierFromAverage(8 * 12);
            lengths[CreatureConstants.Dragon_Blue_VeryYoung][GenderConstants.Female] = GetBaseFromAverage(16 * 12);
            lengths[CreatureConstants.Dragon_Blue_VeryYoung][GenderConstants.Male] = GetBaseFromAverage(16 * 12);
            lengths[CreatureConstants.Dragon_Blue_VeryYoung][CreatureConstants.Dragon_Blue_VeryYoung] = GetMultiplierFromAverage(16 * 12);
            lengths[CreatureConstants.Dragon_Blue_Young][GenderConstants.Female] = GetBaseFromAverage(16 * 12);
            lengths[CreatureConstants.Dragon_Blue_Young][GenderConstants.Male] = GetBaseFromAverage(16 * 12);
            lengths[CreatureConstants.Dragon_Blue_Young][CreatureConstants.Dragon_Blue_Young] = GetMultiplierFromAverage(16 * 12);
            lengths[CreatureConstants.Dragon_Blue_Juvenile][GenderConstants.Female] = GetBaseFromAverage(31 * 12);
            lengths[CreatureConstants.Dragon_Blue_Juvenile][GenderConstants.Male] = GetBaseFromAverage(31 * 12);
            lengths[CreatureConstants.Dragon_Blue_Juvenile][CreatureConstants.Dragon_Blue_Juvenile] = GetMultiplierFromAverage(31 * 12);
            lengths[CreatureConstants.Dragon_Blue_YoungAdult][GenderConstants.Female] = GetBaseFromAverage(31 * 12);
            lengths[CreatureConstants.Dragon_Blue_YoungAdult][GenderConstants.Male] = GetBaseFromAverage(31 * 12);
            lengths[CreatureConstants.Dragon_Blue_YoungAdult][CreatureConstants.Dragon_Blue_YoungAdult] = GetMultiplierFromAverage(31 * 12);
            lengths[CreatureConstants.Dragon_Blue_Adult][GenderConstants.Female] = GetBaseFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_Blue_Adult][GenderConstants.Male] = GetBaseFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_Blue_Adult][CreatureConstants.Dragon_Blue_Adult] = GetMultiplierFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_Blue_MatureAdult][GenderConstants.Female] = GetBaseFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_Blue_MatureAdult][GenderConstants.Male] = GetBaseFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_Blue_MatureAdult][CreatureConstants.Dragon_Blue_MatureAdult] = GetMultiplierFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_Blue_Old][GenderConstants.Female] = GetBaseFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_Blue_Old][GenderConstants.Male] = GetBaseFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_Blue_Old][CreatureConstants.Dragon_Blue_Old] = GetMultiplierFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_Blue_VeryOld][GenderConstants.Female] = GetBaseFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_Blue_VeryOld][GenderConstants.Male] = GetBaseFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_Blue_VeryOld][CreatureConstants.Dragon_Blue_VeryOld] = GetMultiplierFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_Blue_Ancient][GenderConstants.Female] = GetBaseFromAverage(85 * 12);
            lengths[CreatureConstants.Dragon_Blue_Ancient][GenderConstants.Male] = GetBaseFromAverage(85 * 12);
            lengths[CreatureConstants.Dragon_Blue_Ancient][CreatureConstants.Dragon_Blue_Ancient] = GetMultiplierFromAverage(85 * 12);
            lengths[CreatureConstants.Dragon_Blue_Wyrm][GenderConstants.Female] = GetBaseFromAverage(85 * 12);
            lengths[CreatureConstants.Dragon_Blue_Wyrm][GenderConstants.Male] = GetBaseFromAverage(85 * 12);
            lengths[CreatureConstants.Dragon_Blue_Wyrm][CreatureConstants.Dragon_Blue_Wyrm] = GetMultiplierFromAverage(85 * 12);
            lengths[CreatureConstants.Dragon_Blue_GreatWyrm][GenderConstants.Female] = GetBaseFromAverage(85 * 12);
            lengths[CreatureConstants.Dragon_Blue_GreatWyrm][GenderConstants.Male] = GetBaseFromAverage(85 * 12);
            lengths[CreatureConstants.Dragon_Blue_GreatWyrm][CreatureConstants.Dragon_Blue_GreatWyrm] = GetMultiplierFromAverage(85 * 12);
            lengths[CreatureConstants.Dragon_Brass_Wyrmling][GenderConstants.Female] = GetBaseFromAverage(4 * 12);
            lengths[CreatureConstants.Dragon_Brass_Wyrmling][GenderConstants.Male] = GetBaseFromAverage(4 * 12);
            lengths[CreatureConstants.Dragon_Brass_Wyrmling][CreatureConstants.Dragon_Brass_Wyrmling] = GetMultiplierFromAverage(4 * 12);
            lengths[CreatureConstants.Dragon_Brass_VeryYoung][GenderConstants.Female] = GetBaseFromAverage(8 * 12);
            lengths[CreatureConstants.Dragon_Brass_VeryYoung][GenderConstants.Male] = GetBaseFromAverage(8 * 12);
            lengths[CreatureConstants.Dragon_Brass_VeryYoung][CreatureConstants.Dragon_Brass_VeryYoung] = GetMultiplierFromAverage(8 * 12);
            lengths[CreatureConstants.Dragon_Brass_Young][GenderConstants.Female] = GetBaseFromAverage(16 * 12);
            lengths[CreatureConstants.Dragon_Brass_Young][GenderConstants.Male] = GetBaseFromAverage(16 * 12);
            lengths[CreatureConstants.Dragon_Brass_Young][CreatureConstants.Dragon_Brass_Young] = GetMultiplierFromAverage(16 * 12);
            lengths[CreatureConstants.Dragon_Brass_Juvenile][GenderConstants.Female] = GetBaseFromAverage(16 * 12);
            lengths[CreatureConstants.Dragon_Brass_Juvenile][GenderConstants.Male] = GetBaseFromAverage(16 * 12);
            lengths[CreatureConstants.Dragon_Brass_Juvenile][CreatureConstants.Dragon_Brass_Juvenile] = GetMultiplierFromAverage(16 * 12);
            lengths[CreatureConstants.Dragon_Brass_YoungAdult][GenderConstants.Female] = GetBaseFromAverage(31 * 12);
            lengths[CreatureConstants.Dragon_Brass_YoungAdult][GenderConstants.Male] = GetBaseFromAverage(31 * 12);
            lengths[CreatureConstants.Dragon_Brass_YoungAdult][CreatureConstants.Dragon_Brass_YoungAdult] = GetMultiplierFromAverage(31 * 12);
            lengths[CreatureConstants.Dragon_Brass_Adult][GenderConstants.Female] = GetBaseFromAverage(31 * 12);
            lengths[CreatureConstants.Dragon_Brass_Adult][GenderConstants.Male] = GetBaseFromAverage(31 * 12);
            lengths[CreatureConstants.Dragon_Brass_Adult][CreatureConstants.Dragon_Brass_Adult] = GetMultiplierFromAverage(31 * 12);
            lengths[CreatureConstants.Dragon_Brass_MatureAdult][GenderConstants.Female] = GetBaseFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_Brass_MatureAdult][GenderConstants.Male] = GetBaseFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_Brass_MatureAdult][CreatureConstants.Dragon_Brass_MatureAdult] = GetMultiplierFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_Brass_Old][GenderConstants.Female] = GetBaseFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_Brass_Old][GenderConstants.Male] = GetBaseFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_Brass_Old][CreatureConstants.Dragon_Brass_Old] = GetMultiplierFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_Brass_VeryOld][GenderConstants.Female] = GetBaseFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_Brass_VeryOld][GenderConstants.Male] = GetBaseFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_Brass_VeryOld][CreatureConstants.Dragon_Brass_VeryOld] = GetMultiplierFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_Brass_Ancient][GenderConstants.Female] = GetBaseFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_Brass_Ancient][GenderConstants.Male] = GetBaseFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_Brass_Ancient][CreatureConstants.Dragon_Brass_Ancient] = GetMultiplierFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_Brass_Wyrm][GenderConstants.Female] = GetBaseFromAverage(85 * 12);
            lengths[CreatureConstants.Dragon_Brass_Wyrm][GenderConstants.Male] = GetBaseFromAverage(85 * 12);
            lengths[CreatureConstants.Dragon_Brass_Wyrm][CreatureConstants.Dragon_Brass_Wyrm] = GetMultiplierFromAverage(85 * 12);
            lengths[CreatureConstants.Dragon_Brass_GreatWyrm][GenderConstants.Female] = GetBaseFromAverage(85 * 12);
            lengths[CreatureConstants.Dragon_Brass_GreatWyrm][GenderConstants.Male] = GetBaseFromAverage(85 * 12);
            lengths[CreatureConstants.Dragon_Brass_GreatWyrm][CreatureConstants.Dragon_Brass_GreatWyrm] = GetMultiplierFromAverage(85 * 12);
            lengths[CreatureConstants.Dragon_Bronze_Wyrmling][GenderConstants.Female] = GetBaseFromAverage(8 * 12);
            lengths[CreatureConstants.Dragon_Bronze_Wyrmling][GenderConstants.Male] = GetBaseFromAverage(8 * 12);
            lengths[CreatureConstants.Dragon_Bronze_Wyrmling][CreatureConstants.Dragon_Bronze_Wyrmling] = GetMultiplierFromAverage(8 * 12);
            lengths[CreatureConstants.Dragon_Bronze_VeryYoung][GenderConstants.Female] = GetBaseFromAverage(16 * 12);
            lengths[CreatureConstants.Dragon_Bronze_VeryYoung][GenderConstants.Male] = GetBaseFromAverage(16 * 12);
            lengths[CreatureConstants.Dragon_Bronze_VeryYoung][CreatureConstants.Dragon_Bronze_VeryYoung] = GetMultiplierFromAverage(16 * 12);
            lengths[CreatureConstants.Dragon_Bronze_Young][GenderConstants.Female] = GetBaseFromAverage(16 * 12);
            lengths[CreatureConstants.Dragon_Bronze_Young][GenderConstants.Male] = GetBaseFromAverage(16 * 12);
            lengths[CreatureConstants.Dragon_Bronze_Young][CreatureConstants.Dragon_Bronze_Young] = GetMultiplierFromAverage(16 * 12);
            lengths[CreatureConstants.Dragon_Bronze_Juvenile][GenderConstants.Female] = GetBaseFromAverage(31 * 12);
            lengths[CreatureConstants.Dragon_Bronze_Juvenile][GenderConstants.Male] = GetBaseFromAverage(31 * 12);
            lengths[CreatureConstants.Dragon_Bronze_Juvenile][CreatureConstants.Dragon_Bronze_Juvenile] = GetMultiplierFromAverage(31 * 12);
            lengths[CreatureConstants.Dragon_Bronze_YoungAdult][GenderConstants.Female] = GetBaseFromAverage(31 * 12);
            lengths[CreatureConstants.Dragon_Bronze_YoungAdult][GenderConstants.Male] = GetBaseFromAverage(31 * 12);
            lengths[CreatureConstants.Dragon_Bronze_YoungAdult][CreatureConstants.Dragon_Bronze_YoungAdult] = GetMultiplierFromAverage(31 * 12);
            lengths[CreatureConstants.Dragon_Bronze_Adult][GenderConstants.Female] = GetBaseFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_Bronze_Adult][GenderConstants.Male] = GetBaseFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_Bronze_Adult][CreatureConstants.Dragon_Bronze_Adult] = GetMultiplierFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_Bronze_MatureAdult][GenderConstants.Female] = GetBaseFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_Bronze_MatureAdult][GenderConstants.Male] = GetBaseFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_Bronze_MatureAdult][CreatureConstants.Dragon_Bronze_MatureAdult] = GetMultiplierFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_Bronze_Old][GenderConstants.Female] = GetBaseFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_Bronze_Old][GenderConstants.Male] = GetBaseFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_Bronze_Old][CreatureConstants.Dragon_Bronze_Old] = GetMultiplierFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_Bronze_VeryOld][GenderConstants.Female] = GetBaseFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_Bronze_VeryOld][GenderConstants.Male] = GetBaseFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_Bronze_VeryOld][CreatureConstants.Dragon_Bronze_VeryOld] = GetMultiplierFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_Bronze_Ancient][GenderConstants.Female] = GetBaseFromAverage(85 * 12);
            lengths[CreatureConstants.Dragon_Bronze_Ancient][GenderConstants.Male] = GetBaseFromAverage(85 * 12);
            lengths[CreatureConstants.Dragon_Bronze_Ancient][CreatureConstants.Dragon_Bronze_Ancient] = GetMultiplierFromAverage(85 * 12);
            lengths[CreatureConstants.Dragon_Bronze_Wyrm][GenderConstants.Female] = GetBaseFromAverage(85 * 12);
            lengths[CreatureConstants.Dragon_Bronze_Wyrm][GenderConstants.Male] = GetBaseFromAverage(85 * 12);
            lengths[CreatureConstants.Dragon_Bronze_Wyrm][CreatureConstants.Dragon_Bronze_Wyrm] = GetMultiplierFromAverage(85 * 12);
            lengths[CreatureConstants.Dragon_Bronze_GreatWyrm][GenderConstants.Female] = GetBaseFromAverage(85 * 12);
            lengths[CreatureConstants.Dragon_Bronze_GreatWyrm][GenderConstants.Male] = GetBaseFromAverage(85 * 12);
            lengths[CreatureConstants.Dragon_Bronze_GreatWyrm][CreatureConstants.Dragon_Bronze_GreatWyrm] = GetMultiplierFromAverage(85 * 12);
            lengths[CreatureConstants.Dragon_Copper_Wyrmling][GenderConstants.Female] = GetBaseFromAverage(4 * 12);
            lengths[CreatureConstants.Dragon_Copper_Wyrmling][GenderConstants.Male] = GetBaseFromAverage(4 * 12);
            lengths[CreatureConstants.Dragon_Copper_Wyrmling][CreatureConstants.Dragon_Copper_Wyrmling] = GetMultiplierFromAverage(4 * 12);
            lengths[CreatureConstants.Dragon_Copper_VeryYoung][GenderConstants.Female] = GetBaseFromAverage(8 * 12);
            lengths[CreatureConstants.Dragon_Copper_VeryYoung][GenderConstants.Male] = GetBaseFromAverage(8 * 12);
            lengths[CreatureConstants.Dragon_Copper_VeryYoung][CreatureConstants.Dragon_Copper_VeryYoung] = GetMultiplierFromAverage(8 * 12);
            lengths[CreatureConstants.Dragon_Copper_Young][GenderConstants.Female] = GetBaseFromAverage(16 * 12);
            lengths[CreatureConstants.Dragon_Copper_Young][GenderConstants.Male] = GetBaseFromAverage(16 * 12);
            lengths[CreatureConstants.Dragon_Copper_Young][CreatureConstants.Dragon_Copper_Young] = GetMultiplierFromAverage(16 * 12);
            lengths[CreatureConstants.Dragon_Copper_Juvenile][GenderConstants.Female] = GetBaseFromAverage(16 * 12);
            lengths[CreatureConstants.Dragon_Copper_Juvenile][GenderConstants.Male] = GetBaseFromAverage(16 * 12);
            lengths[CreatureConstants.Dragon_Copper_Juvenile][CreatureConstants.Dragon_Copper_Juvenile] = GetMultiplierFromAverage(16 * 12);
            lengths[CreatureConstants.Dragon_Copper_YoungAdult][GenderConstants.Female] = GetBaseFromAverage(31 * 12);
            lengths[CreatureConstants.Dragon_Copper_YoungAdult][GenderConstants.Male] = GetBaseFromAverage(31 * 12);
            lengths[CreatureConstants.Dragon_Copper_YoungAdult][CreatureConstants.Dragon_Copper_YoungAdult] = GetMultiplierFromAverage(31 * 12);
            lengths[CreatureConstants.Dragon_Copper_Adult][GenderConstants.Female] = GetBaseFromAverage(31 * 12);
            lengths[CreatureConstants.Dragon_Copper_Adult][GenderConstants.Male] = GetBaseFromAverage(31 * 12);
            lengths[CreatureConstants.Dragon_Copper_Adult][CreatureConstants.Dragon_Copper_Adult] = GetMultiplierFromAverage(31 * 12);
            lengths[CreatureConstants.Dragon_Copper_MatureAdult][GenderConstants.Female] = GetBaseFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_Copper_MatureAdult][GenderConstants.Male] = GetBaseFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_Copper_MatureAdult][CreatureConstants.Dragon_Copper_MatureAdult] = GetMultiplierFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_Copper_Old][GenderConstants.Female] = GetBaseFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_Copper_Old][GenderConstants.Male] = GetBaseFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_Copper_Old][CreatureConstants.Dragon_Copper_Old] = GetMultiplierFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_Copper_VeryOld][GenderConstants.Female] = GetBaseFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_Copper_VeryOld][GenderConstants.Male] = GetBaseFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_Copper_VeryOld][CreatureConstants.Dragon_Copper_VeryOld] = GetMultiplierFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_Copper_Ancient][GenderConstants.Female] = GetBaseFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_Copper_Ancient][GenderConstants.Male] = GetBaseFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_Copper_Ancient][CreatureConstants.Dragon_Copper_Ancient] = GetMultiplierFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_Copper_Wyrm][GenderConstants.Female] = GetBaseFromAverage(85 * 12);
            lengths[CreatureConstants.Dragon_Copper_Wyrm][GenderConstants.Male] = GetBaseFromAverage(85 * 12);
            lengths[CreatureConstants.Dragon_Copper_Wyrm][CreatureConstants.Dragon_Copper_Wyrm] = GetMultiplierFromAverage(85 * 12);
            lengths[CreatureConstants.Dragon_Copper_GreatWyrm][GenderConstants.Female] = GetBaseFromAverage(85 * 12);
            lengths[CreatureConstants.Dragon_Copper_GreatWyrm][GenderConstants.Male] = GetBaseFromAverage(85 * 12);
            lengths[CreatureConstants.Dragon_Copper_GreatWyrm][CreatureConstants.Dragon_Copper_GreatWyrm] = GetMultiplierFromAverage(85 * 12);
            lengths[CreatureConstants.Dragon_Gold_Wyrmling][GenderConstants.Female] = GetBaseFromAverage(16 * 12);
            lengths[CreatureConstants.Dragon_Gold_Wyrmling][GenderConstants.Male] = GetBaseFromAverage(16 * 12);
            lengths[CreatureConstants.Dragon_Gold_Wyrmling][CreatureConstants.Dragon_Gold_Wyrmling] = GetMultiplierFromAverage(16 * 12);
            lengths[CreatureConstants.Dragon_Gold_VeryYoung][GenderConstants.Female] = GetBaseFromAverage(31 * 12);
            lengths[CreatureConstants.Dragon_Gold_VeryYoung][GenderConstants.Male] = GetBaseFromAverage(31 * 12);
            lengths[CreatureConstants.Dragon_Gold_VeryYoung][CreatureConstants.Dragon_Gold_VeryYoung] = GetMultiplierFromAverage(31 * 12);
            lengths[CreatureConstants.Dragon_Gold_Young][GenderConstants.Female] = GetBaseFromAverage(31 * 12);
            lengths[CreatureConstants.Dragon_Gold_Young][GenderConstants.Male] = GetBaseFromAverage(31 * 12);
            lengths[CreatureConstants.Dragon_Gold_Young][CreatureConstants.Dragon_Gold_Young] = GetMultiplierFromAverage(31 * 12);
            lengths[CreatureConstants.Dragon_Gold_Juvenile][GenderConstants.Female] = GetBaseFromAverage(31 * 12);
            lengths[CreatureConstants.Dragon_Gold_Juvenile][GenderConstants.Male] = GetBaseFromAverage(31 * 12);
            lengths[CreatureConstants.Dragon_Gold_Juvenile][CreatureConstants.Dragon_Gold_Juvenile] = GetMultiplierFromAverage(31 * 12);
            lengths[CreatureConstants.Dragon_Gold_YoungAdult][GenderConstants.Female] = GetBaseFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_Gold_YoungAdult][GenderConstants.Male] = GetBaseFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_Gold_YoungAdult][CreatureConstants.Dragon_Gold_YoungAdult] = GetMultiplierFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_Gold_Adult][GenderConstants.Female] = GetBaseFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_Gold_Adult][GenderConstants.Male] = GetBaseFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_Gold_Adult][CreatureConstants.Dragon_Gold_Adult] = GetMultiplierFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_Gold_MatureAdult][GenderConstants.Female] = GetBaseFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_Gold_MatureAdult][GenderConstants.Male] = GetBaseFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_Gold_MatureAdult][CreatureConstants.Dragon_Gold_MatureAdult] = GetMultiplierFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_Gold_Old][GenderConstants.Female] = GetBaseFromAverage(85 * 12);
            lengths[CreatureConstants.Dragon_Gold_Old][GenderConstants.Male] = GetBaseFromAverage(85 * 12);
            lengths[CreatureConstants.Dragon_Gold_Old][CreatureConstants.Dragon_Gold_Old] = GetMultiplierFromAverage(85 * 12);
            lengths[CreatureConstants.Dragon_Gold_VeryOld][GenderConstants.Female] = GetBaseFromAverage(85 * 12);
            lengths[CreatureConstants.Dragon_Gold_VeryOld][GenderConstants.Male] = GetBaseFromAverage(85 * 12);
            lengths[CreatureConstants.Dragon_Gold_VeryOld][CreatureConstants.Dragon_Gold_VeryOld] = GetMultiplierFromAverage(85 * 12);
            lengths[CreatureConstants.Dragon_Gold_Ancient][GenderConstants.Female] = GetBaseFromAverage(85 * 12);
            lengths[CreatureConstants.Dragon_Gold_Ancient][GenderConstants.Male] = GetBaseFromAverage(85 * 12);
            lengths[CreatureConstants.Dragon_Gold_Ancient][CreatureConstants.Dragon_Gold_Ancient] = GetMultiplierFromAverage(85 * 12);
            lengths[CreatureConstants.Dragon_Gold_Wyrm][GenderConstants.Female] = GetBaseFromAverage(120 * 12);
            lengths[CreatureConstants.Dragon_Gold_Wyrm][GenderConstants.Male] = GetBaseFromAverage(120 * 12);
            lengths[CreatureConstants.Dragon_Gold_Wyrm][CreatureConstants.Dragon_Gold_Wyrm] = GetMultiplierFromAverage(120 * 12);
            lengths[CreatureConstants.Dragon_Gold_GreatWyrm][GenderConstants.Female] = GetBaseFromAverage(120 * 12);
            lengths[CreatureConstants.Dragon_Gold_GreatWyrm][GenderConstants.Male] = GetBaseFromAverage(120 * 12);
            lengths[CreatureConstants.Dragon_Gold_GreatWyrm][CreatureConstants.Dragon_Gold_GreatWyrm] = GetMultiplierFromAverage(120 * 12);
            lengths[CreatureConstants.Dragon_Green_Wyrmling][GenderConstants.Female] = GetBaseFromAverage(8 * 12);
            lengths[CreatureConstants.Dragon_Green_Wyrmling][GenderConstants.Male] = GetBaseFromAverage(8 * 12);
            lengths[CreatureConstants.Dragon_Green_Wyrmling][CreatureConstants.Dragon_Green_Wyrmling] = GetMultiplierFromAverage(8 * 12);
            lengths[CreatureConstants.Dragon_Green_VeryYoung][GenderConstants.Female] = GetBaseFromAverage(16 * 12);
            lengths[CreatureConstants.Dragon_Green_VeryYoung][GenderConstants.Male] = GetBaseFromAverage(16 * 12);
            lengths[CreatureConstants.Dragon_Green_VeryYoung][CreatureConstants.Dragon_Green_VeryYoung] = GetMultiplierFromAverage(16 * 12);
            lengths[CreatureConstants.Dragon_Green_Young][GenderConstants.Female] = GetBaseFromAverage(16 * 12);
            lengths[CreatureConstants.Dragon_Green_Young][GenderConstants.Male] = GetBaseFromAverage(16 * 12);
            lengths[CreatureConstants.Dragon_Green_Young][CreatureConstants.Dragon_Green_Young] = GetMultiplierFromAverage(16 * 12);
            lengths[CreatureConstants.Dragon_Green_Juvenile][GenderConstants.Female] = GetBaseFromAverage(31 * 12);
            lengths[CreatureConstants.Dragon_Green_Juvenile][GenderConstants.Male] = GetBaseFromAverage(31 * 12);
            lengths[CreatureConstants.Dragon_Green_Juvenile][CreatureConstants.Dragon_Green_Juvenile] = GetMultiplierFromAverage(31 * 12);
            lengths[CreatureConstants.Dragon_Green_YoungAdult][GenderConstants.Female] = GetBaseFromAverage(31 * 12);
            lengths[CreatureConstants.Dragon_Green_YoungAdult][GenderConstants.Male] = GetBaseFromAverage(31 * 12);
            lengths[CreatureConstants.Dragon_Green_YoungAdult][CreatureConstants.Dragon_Green_YoungAdult] = GetMultiplierFromAverage(31 * 12);
            lengths[CreatureConstants.Dragon_Green_Adult][GenderConstants.Female] = GetBaseFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_Green_Adult][GenderConstants.Male] = GetBaseFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_Green_Adult][CreatureConstants.Dragon_Green_Adult] = GetMultiplierFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_Green_MatureAdult][GenderConstants.Female] = GetBaseFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_Green_MatureAdult][GenderConstants.Male] = GetBaseFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_Green_MatureAdult][CreatureConstants.Dragon_Green_MatureAdult] = GetMultiplierFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_Green_Old][GenderConstants.Female] = GetBaseFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_Green_Old][GenderConstants.Male] = GetBaseFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_Green_Old][CreatureConstants.Dragon_Green_Old] = GetMultiplierFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_Green_VeryOld][GenderConstants.Female] = GetBaseFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_Green_VeryOld][GenderConstants.Male] = GetBaseFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_Green_VeryOld][CreatureConstants.Dragon_Green_VeryOld] = GetMultiplierFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_Green_Ancient][GenderConstants.Female] = GetBaseFromAverage(85 * 12);
            lengths[CreatureConstants.Dragon_Green_Ancient][GenderConstants.Male] = GetBaseFromAverage(85 * 12);
            lengths[CreatureConstants.Dragon_Green_Ancient][CreatureConstants.Dragon_Green_Ancient] = GetMultiplierFromAverage(85 * 12);
            lengths[CreatureConstants.Dragon_Green_Wyrm][GenderConstants.Female] = GetBaseFromAverage(85 * 12);
            lengths[CreatureConstants.Dragon_Green_Wyrm][GenderConstants.Male] = GetBaseFromAverage(85 * 12);
            lengths[CreatureConstants.Dragon_Green_Wyrm][CreatureConstants.Dragon_Green_Wyrm] = GetMultiplierFromAverage(85 * 12);
            lengths[CreatureConstants.Dragon_Green_GreatWyrm][GenderConstants.Female] = GetBaseFromAverage(85 * 12);
            lengths[CreatureConstants.Dragon_Green_GreatWyrm][GenderConstants.Male] = GetBaseFromAverage(85 * 12);
            lengths[CreatureConstants.Dragon_Green_GreatWyrm][CreatureConstants.Dragon_Green_GreatWyrm] = GetMultiplierFromAverage(85 * 12);
            lengths[CreatureConstants.Dragon_Red_Wyrmling][GenderConstants.Female] = GetBaseFromAverage(16 * 12);
            lengths[CreatureConstants.Dragon_Red_Wyrmling][GenderConstants.Male] = GetBaseFromAverage(16 * 12);
            lengths[CreatureConstants.Dragon_Red_Wyrmling][CreatureConstants.Dragon_Red_Wyrmling] = GetMultiplierFromAverage(16 * 12);
            lengths[CreatureConstants.Dragon_Red_VeryYoung][GenderConstants.Female] = GetBaseFromAverage(31 * 12);
            lengths[CreatureConstants.Dragon_Red_VeryYoung][GenderConstants.Male] = GetBaseFromAverage(31 * 12);
            lengths[CreatureConstants.Dragon_Red_VeryYoung][CreatureConstants.Dragon_Red_VeryYoung] = GetMultiplierFromAverage(31 * 12);
            lengths[CreatureConstants.Dragon_Red_Young][GenderConstants.Female] = GetBaseFromAverage(31 * 12);
            lengths[CreatureConstants.Dragon_Red_Young][GenderConstants.Male] = GetBaseFromAverage(31 * 12);
            lengths[CreatureConstants.Dragon_Red_Young][CreatureConstants.Dragon_Red_Young] = GetMultiplierFromAverage(31 * 12);
            lengths[CreatureConstants.Dragon_Red_Juvenile][GenderConstants.Female] = GetBaseFromAverage(31 * 12);
            lengths[CreatureConstants.Dragon_Red_Juvenile][GenderConstants.Male] = GetBaseFromAverage(31 * 12);
            lengths[CreatureConstants.Dragon_Red_Juvenile][CreatureConstants.Dragon_Red_Juvenile] = GetMultiplierFromAverage(31 * 12);
            lengths[CreatureConstants.Dragon_Red_YoungAdult][GenderConstants.Female] = GetBaseFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_Red_YoungAdult][GenderConstants.Male] = GetBaseFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_Red_YoungAdult][CreatureConstants.Dragon_Red_YoungAdult] = GetMultiplierFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_Red_Adult][GenderConstants.Female] = GetBaseFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_Red_Adult][GenderConstants.Male] = GetBaseFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_Red_Adult][CreatureConstants.Dragon_Red_Adult] = GetMultiplierFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_Red_MatureAdult][GenderConstants.Female] = GetBaseFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_Red_MatureAdult][GenderConstants.Male] = GetBaseFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_Red_MatureAdult][CreatureConstants.Dragon_Red_MatureAdult] = GetMultiplierFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_Red_Old][GenderConstants.Female] = GetBaseFromAverage(85 * 12);
            lengths[CreatureConstants.Dragon_Red_Old][GenderConstants.Male] = GetBaseFromAverage(85 * 12);
            lengths[CreatureConstants.Dragon_Red_Old][CreatureConstants.Dragon_Red_Old] = GetMultiplierFromAverage(85 * 12);
            lengths[CreatureConstants.Dragon_Red_VeryOld][GenderConstants.Female] = GetBaseFromAverage(85 * 12);
            lengths[CreatureConstants.Dragon_Red_VeryOld][GenderConstants.Male] = GetBaseFromAverage(85 * 12);
            lengths[CreatureConstants.Dragon_Red_VeryOld][CreatureConstants.Dragon_Red_VeryOld] = GetMultiplierFromAverage(85 * 12);
            lengths[CreatureConstants.Dragon_Red_Ancient][GenderConstants.Female] = GetBaseFromAverage(85 * 12);
            lengths[CreatureConstants.Dragon_Red_Ancient][GenderConstants.Male] = GetBaseFromAverage(85 * 12);
            lengths[CreatureConstants.Dragon_Red_Ancient][CreatureConstants.Dragon_Red_Ancient] = GetMultiplierFromAverage(85 * 12);
            lengths[CreatureConstants.Dragon_Red_Wyrm][GenderConstants.Female] = GetBaseFromAverage(85 * 12);
            lengths[CreatureConstants.Dragon_Red_Wyrm][GenderConstants.Male] = GetBaseFromAverage(85 * 12);
            lengths[CreatureConstants.Dragon_Red_Wyrm][CreatureConstants.Dragon_Red_Wyrm] = GetMultiplierFromAverage(85 * 12);
            lengths[CreatureConstants.Dragon_Red_GreatWyrm][GenderConstants.Female] = GetBaseFromAverage(120 * 12);
            lengths[CreatureConstants.Dragon_Red_GreatWyrm][GenderConstants.Male] = GetBaseFromAverage(120 * 12);
            lengths[CreatureConstants.Dragon_Red_GreatWyrm][CreatureConstants.Dragon_Red_GreatWyrm] = GetMultiplierFromAverage(120 * 12);
            lengths[CreatureConstants.Dragon_Silver_Wyrmling][GenderConstants.Female] = GetBaseFromAverage(8 * 12);
            lengths[CreatureConstants.Dragon_Silver_Wyrmling][GenderConstants.Male] = GetBaseFromAverage(8 * 12);
            lengths[CreatureConstants.Dragon_Silver_Wyrmling][CreatureConstants.Dragon_Silver_Wyrmling] = GetMultiplierFromAverage(8 * 12);
            lengths[CreatureConstants.Dragon_Silver_VeryYoung][GenderConstants.Female] = GetBaseFromAverage(16 * 12);
            lengths[CreatureConstants.Dragon_Silver_VeryYoung][GenderConstants.Male] = GetBaseFromAverage(16 * 12);
            lengths[CreatureConstants.Dragon_Silver_VeryYoung][CreatureConstants.Dragon_Silver_VeryYoung] = GetMultiplierFromAverage(16 * 12);
            lengths[CreatureConstants.Dragon_Silver_Young][GenderConstants.Female] = GetBaseFromAverage(16 * 12);
            lengths[CreatureConstants.Dragon_Silver_Young][GenderConstants.Male] = GetBaseFromAverage(16 * 12);
            lengths[CreatureConstants.Dragon_Silver_Young][CreatureConstants.Dragon_Silver_Young] = GetMultiplierFromAverage(16 * 12);
            lengths[CreatureConstants.Dragon_Silver_Juvenile][GenderConstants.Female] = GetBaseFromAverage(31 * 12);
            lengths[CreatureConstants.Dragon_Silver_Juvenile][GenderConstants.Male] = GetBaseFromAverage(31 * 12);
            lengths[CreatureConstants.Dragon_Silver_Juvenile][CreatureConstants.Dragon_Silver_Juvenile] = GetMultiplierFromAverage(31 * 12);
            lengths[CreatureConstants.Dragon_Silver_YoungAdult][GenderConstants.Female] = GetBaseFromAverage(31 * 12);
            lengths[CreatureConstants.Dragon_Silver_YoungAdult][GenderConstants.Male] = GetBaseFromAverage(31 * 12);
            lengths[CreatureConstants.Dragon_Silver_YoungAdult][CreatureConstants.Dragon_Silver_YoungAdult] = GetMultiplierFromAverage(31 * 12);
            lengths[CreatureConstants.Dragon_Silver_Adult][GenderConstants.Female] = GetBaseFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_Silver_Adult][GenderConstants.Male] = GetBaseFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_Silver_Adult][CreatureConstants.Dragon_Silver_Adult] = GetMultiplierFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_Silver_MatureAdult][GenderConstants.Female] = GetBaseFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_Silver_MatureAdult][GenderConstants.Male] = GetBaseFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_Silver_MatureAdult][CreatureConstants.Dragon_Silver_MatureAdult] = GetMultiplierFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_Silver_Old][GenderConstants.Female] = GetBaseFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_Silver_Old][GenderConstants.Male] = GetBaseFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_Silver_Old][CreatureConstants.Dragon_Silver_Old] = GetMultiplierFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_Silver_VeryOld][GenderConstants.Female] = GetBaseFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_Silver_VeryOld][GenderConstants.Male] = GetBaseFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_Silver_VeryOld][CreatureConstants.Dragon_Silver_VeryOld] = GetMultiplierFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_Silver_Ancient][GenderConstants.Female] = GetBaseFromAverage(85 * 12);
            lengths[CreatureConstants.Dragon_Silver_Ancient][GenderConstants.Male] = GetBaseFromAverage(85 * 12);
            lengths[CreatureConstants.Dragon_Silver_Ancient][CreatureConstants.Dragon_Silver_Ancient] = GetMultiplierFromAverage(85 * 12);
            lengths[CreatureConstants.Dragon_Silver_Wyrm][GenderConstants.Female] = GetBaseFromAverage(85 * 12);
            lengths[CreatureConstants.Dragon_Silver_Wyrm][GenderConstants.Male] = GetBaseFromAverage(85 * 12);
            lengths[CreatureConstants.Dragon_Silver_Wyrm][CreatureConstants.Dragon_Silver_Wyrm] = GetMultiplierFromAverage(85 * 12);
            lengths[CreatureConstants.Dragon_Silver_GreatWyrm][GenderConstants.Female] = GetBaseFromAverage(120 * 12);
            lengths[CreatureConstants.Dragon_Silver_GreatWyrm][GenderConstants.Male] = GetBaseFromAverage(120 * 12);
            lengths[CreatureConstants.Dragon_Silver_GreatWyrm][CreatureConstants.Dragon_Silver_GreatWyrm] = GetMultiplierFromAverage(120 * 12);
            lengths[CreatureConstants.Dragon_White_Wyrmling][GenderConstants.Female] = GetBaseFromAverage(4 * 12);
            lengths[CreatureConstants.Dragon_White_Wyrmling][GenderConstants.Male] = GetBaseFromAverage(4 * 12);
            lengths[CreatureConstants.Dragon_White_Wyrmling][CreatureConstants.Dragon_White_Wyrmling] = GetMultiplierFromAverage(4 * 12);
            lengths[CreatureConstants.Dragon_White_VeryYoung][GenderConstants.Female] = GetBaseFromAverage(8 * 12);
            lengths[CreatureConstants.Dragon_White_VeryYoung][GenderConstants.Male] = GetBaseFromAverage(8 * 12);
            lengths[CreatureConstants.Dragon_White_VeryYoung][CreatureConstants.Dragon_White_VeryYoung] = GetMultiplierFromAverage(8 * 12);
            lengths[CreatureConstants.Dragon_White_Young][GenderConstants.Female] = GetBaseFromAverage(16 * 12);
            lengths[CreatureConstants.Dragon_White_Young][GenderConstants.Male] = GetBaseFromAverage(16 * 12);
            lengths[CreatureConstants.Dragon_White_Young][CreatureConstants.Dragon_White_Young] = GetMultiplierFromAverage(16 * 12);
            lengths[CreatureConstants.Dragon_White_Juvenile][GenderConstants.Female] = GetBaseFromAverage(16 * 12);
            lengths[CreatureConstants.Dragon_White_Juvenile][GenderConstants.Male] = GetBaseFromAverage(16 * 12);
            lengths[CreatureConstants.Dragon_White_Juvenile][CreatureConstants.Dragon_White_Juvenile] = GetMultiplierFromAverage(16 * 12);
            lengths[CreatureConstants.Dragon_White_YoungAdult][GenderConstants.Female] = GetBaseFromAverage(31 * 12);
            lengths[CreatureConstants.Dragon_White_YoungAdult][GenderConstants.Male] = GetBaseFromAverage(31 * 12);
            lengths[CreatureConstants.Dragon_White_YoungAdult][CreatureConstants.Dragon_White_YoungAdult] = GetMultiplierFromAverage(31 * 12);
            lengths[CreatureConstants.Dragon_White_Adult][GenderConstants.Female] = GetBaseFromAverage(31 * 12);
            lengths[CreatureConstants.Dragon_White_Adult][GenderConstants.Male] = GetBaseFromAverage(31 * 12);
            lengths[CreatureConstants.Dragon_White_Adult][CreatureConstants.Dragon_White_Adult] = GetMultiplierFromAverage(31 * 12);
            lengths[CreatureConstants.Dragon_White_MatureAdult][GenderConstants.Female] = GetBaseFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_White_MatureAdult][GenderConstants.Male] = GetBaseFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_White_MatureAdult][CreatureConstants.Dragon_White_MatureAdult] = GetMultiplierFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_White_Old][GenderConstants.Female] = GetBaseFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_White_Old][GenderConstants.Male] = GetBaseFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_White_Old][CreatureConstants.Dragon_White_Old] = GetMultiplierFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_White_VeryOld][GenderConstants.Female] = GetBaseFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_White_VeryOld][GenderConstants.Male] = GetBaseFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_White_VeryOld][CreatureConstants.Dragon_White_VeryOld] = GetMultiplierFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_White_Ancient][GenderConstants.Female] = GetBaseFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_White_Ancient][GenderConstants.Male] = GetBaseFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_White_Ancient][CreatureConstants.Dragon_White_Ancient] = GetMultiplierFromAverage(55 * 12);
            lengths[CreatureConstants.Dragon_White_Wyrm][GenderConstants.Female] = GetBaseFromAverage(85 * 12);
            lengths[CreatureConstants.Dragon_White_Wyrm][GenderConstants.Male] = GetBaseFromAverage(85 * 12);
            lengths[CreatureConstants.Dragon_White_Wyrm][CreatureConstants.Dragon_White_Wyrm] = GetMultiplierFromAverage(85 * 12);
            lengths[CreatureConstants.Dragon_White_GreatWyrm][GenderConstants.Female] = GetBaseFromAverage(85 * 12);
            lengths[CreatureConstants.Dragon_White_GreatWyrm][GenderConstants.Male] = GetBaseFromAverage(85 * 12);
            lengths[CreatureConstants.Dragon_White_GreatWyrm][CreatureConstants.Dragon_White_GreatWyrm] = GetMultiplierFromAverage(85 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Dragon_turtle
            lengths[CreatureConstants.DragonTurtle][GenderConstants.Female] = GetBaseFromRange(20 * 12, 30 * 12);
            lengths[CreatureConstants.DragonTurtle][GenderConstants.Male] = GetBaseFromRange(20 * 12, 30 * 12);
            lengths[CreatureConstants.DragonTurtle][CreatureConstants.DragonTurtle] = GetMultiplierFromRange(20 * 12, 30 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Dragonne
            lengths[CreatureConstants.Dragonne][GenderConstants.Female] = GetBaseFromAverage(12 * 12);
            lengths[CreatureConstants.Dragonne][GenderConstants.Male] = GetBaseFromAverage(12 * 12);
            lengths[CreatureConstants.Dragonne][CreatureConstants.Dragonne] = GetMultiplierFromAverage(12 * 12);
            lengths[CreatureConstants.Dretch][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.Dretch][GenderConstants.Female] = "0";
            lengths[CreatureConstants.Dretch][GenderConstants.Male] = "0";
            lengths[CreatureConstants.Dretch][CreatureConstants.Dretch] = "0";
            //Source: https://www.worldanvil.com/w/faerun-tatortotzke/a/drider-species
            lengths[CreatureConstants.Drider][GenderConstants.Agender] = GetBaseFromRange(7 * 12, 9 * 12);
            lengths[CreatureConstants.Drider][CreatureConstants.Drider] = GetMultiplierFromRange(7 * 12, 9 * 12);
            lengths[CreatureConstants.Dryad][GenderConstants.Female] = "0";
            lengths[CreatureConstants.Dryad][CreatureConstants.Dryad] = "0";
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
            //Source: https://www.d20srd.org/srd/monsters/eagle.htm
            lengths[CreatureConstants.Eagle][GenderConstants.Female] = GetBaseFromAverage(3 * 12);
            lengths[CreatureConstants.Eagle][GenderConstants.Male] = GetBaseFromAverage(3 * 12);
            lengths[CreatureConstants.Eagle][CreatureConstants.Eagle] = GetMultiplierFromAverage(3 * 12);
            //Source: https://www.d20srd.org/srd/monsters/eagleGiant.htm
            //https://www.dimensions.com/element/bald-eagle-haliaeetus-leucocephalus scale up: 3*12*10*12/[17,24] = [254,180]
            lengths[CreatureConstants.Eagle_Giant][GenderConstants.Female] = GetBaseFromRange(180, 254);
            lengths[CreatureConstants.Eagle_Giant][GenderConstants.Male] = GetBaseFromRange(180, 254);
            lengths[CreatureConstants.Eagle_Giant][CreatureConstants.Eagle_Giant] = GetMultiplierFromRange(180, 254);
            lengths[CreatureConstants.Efreeti][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.Efreeti][GenderConstants.Female] = "0";
            lengths[CreatureConstants.Efreeti][GenderConstants.Male] = "0";
            lengths[CreatureConstants.Efreeti][CreatureConstants.Efreeti] = "0";
            //Source: https://jurassicworld-evolution.fandom.com/wiki/Elasmosaurus
            lengths[CreatureConstants.Elasmosaurus][GenderConstants.Female] = GetBaseFromAverage(394);
            lengths[CreatureConstants.Elasmosaurus][GenderConstants.Male] = GetBaseFromAverage(394);
            lengths[CreatureConstants.Elasmosaurus][CreatureConstants.Elasmosaurus] = GetMultiplierFromAverage(394);
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
            //Source: https://www.dimensions.com/element/african-bush-elephant-loxodonta-africana
            lengths[CreatureConstants.Elephant][GenderConstants.Female] = GetBaseFromRange(10 * 12, 16 * 12 + 6);
            lengths[CreatureConstants.Elephant][GenderConstants.Male] = GetBaseFromRange(10 * 12, 16 * 12 + 6);
            lengths[CreatureConstants.Elephant][CreatureConstants.Elephant] = GetMultiplierFromRange(10 * 12, 16 * 12 + 6);
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
            lengths[CreatureConstants.Erinyes][GenderConstants.Female] = "0";
            lengths[CreatureConstants.Erinyes][GenderConstants.Male] = "0";
            lengths[CreatureConstants.Erinyes][CreatureConstants.Erinyes] = "0";
            lengths[CreatureConstants.EtherealFilcher][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.EtherealFilcher][CreatureConstants.EtherealFilcher] = "0";
            //Source: https://www.d20srd.org/srd/monsters/etherealMarauder.htm
            lengths[CreatureConstants.EtherealMarauder][GenderConstants.Female] = GetBaseFromAverage(7 * 12);
            lengths[CreatureConstants.EtherealMarauder][GenderConstants.Male] = GetBaseFromAverage(7 * 12);
            lengths[CreatureConstants.EtherealMarauder][CreatureConstants.EtherealMarauder] = GetMultiplierFromAverage(7 * 12);
            //Source: https://syrikdarkenedskies.obsidianportal.com/wikis/ettercap-race
            lengths[CreatureConstants.Ettercap][GenderConstants.Female] = "0";
            lengths[CreatureConstants.Ettercap][GenderConstants.Male] = "0";
            lengths[CreatureConstants.Ettercap][CreatureConstants.Ettin] = "0";
            lengths[CreatureConstants.Ettin][GenderConstants.Female] = "0";
            lengths[CreatureConstants.Ettin][GenderConstants.Male] = "0";
            lengths[CreatureConstants.Ettin][CreatureConstants.Ettin] = "0";
            //Source: https://www.d20srd.org/srd/monsters/giantFireBeetle.htm
            lengths[CreatureConstants.FireBeetle_Giant][GenderConstants.Male] = GetBaseFromAverage(2 * 12);
            lengths[CreatureConstants.FireBeetle_Giant][GenderConstants.Female] = GetBaseFromAverage(2 * 12);
            lengths[CreatureConstants.FireBeetle_Giant][CreatureConstants.FireBeetle_Giant] = GetMultiplierFromAverage(2 * 12);
            //Source: https://www.d20srd.org/srd/monsters/formian.htm
            lengths[CreatureConstants.FormianWorker][GenderConstants.Male] = GetBaseFromAverage(3 * 12);
            lengths[CreatureConstants.FormianWorker][CreatureConstants.FormianWorker] = GetMultiplierFromAverage(3 * 12);
            lengths[CreatureConstants.FormianWarrior][GenderConstants.Male] = GetBaseFromAverage(5 * 12);
            lengths[CreatureConstants.FormianWarrior][CreatureConstants.FormianWarrior] = GetMultiplierFromAverage(5 * 12);
            lengths[CreatureConstants.FormianTaskmaster][GenderConstants.Male] = GetBaseFromAverage(5 * 12);
            lengths[CreatureConstants.FormianTaskmaster][CreatureConstants.FormianTaskmaster] = GetMultiplierFromAverage(5 * 12);
            lengths[CreatureConstants.FormianMyrmarch][GenderConstants.Male] = GetBaseFromAverage(7 * 12);
            lengths[CreatureConstants.FormianMyrmarch][CreatureConstants.FormianMyrmarch] = GetMultiplierFromAverage(7 * 12);
            lengths[CreatureConstants.FormianQueen][GenderConstants.Female] = GetBaseFromAverage(10 * 12);
            lengths[CreatureConstants.FormianQueen][CreatureConstants.FormianQueen] = GetMultiplierFromAverage(10 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Frost_worm
            lengths[CreatureConstants.FrostWorm][GenderConstants.Female] = GetBaseFromAverage(40 * 12);
            lengths[CreatureConstants.FrostWorm][GenderConstants.Male] = GetBaseFromAverage(40 * 12);
            lengths[CreatureConstants.FrostWorm][CreatureConstants.FrostWorm] = GetMultiplierFromAverage(40 * 12);
            lengths[CreatureConstants.Gargoyle][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.Gargoyle][CreatureConstants.Gargoyle] = "0";
            lengths[CreatureConstants.Gargoyle_Kapoacinth][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.Gargoyle_Kapoacinth][CreatureConstants.Gargoyle_Kapoacinth] = "0";
            //Source: https://www.worldanvil.com/w/faerun-tatortotzke/a/gelatinous-cube-species
            lengths[CreatureConstants.GelatinousCube][GenderConstants.Agender] = GetBaseFromAverage(10 * 12);
            lengths[CreatureConstants.GelatinousCube][CreatureConstants.GelatinousCube] = GetMultiplierFromAverage(10 * 12);
            lengths[CreatureConstants.Ghaele][GenderConstants.Female] = "0";
            lengths[CreatureConstants.Ghaele][GenderConstants.Male] = "0";
            lengths[CreatureConstants.Ghaele][CreatureConstants.Ghaele] = "0";
            lengths[CreatureConstants.Ghoul][GenderConstants.Female] = "0";
            lengths[CreatureConstants.Ghoul][GenderConstants.Male] = "0";
            lengths[CreatureConstants.Ghoul][CreatureConstants.Ghoul] = "0";
            lengths[CreatureConstants.Ghoul_Ghast][GenderConstants.Female] = "0";
            lengths[CreatureConstants.Ghoul_Ghast][GenderConstants.Male] = "0";
            lengths[CreatureConstants.Ghoul_Ghast][CreatureConstants.Ghoul_Ghast] = "0";
            lengths[CreatureConstants.Ghoul_Lacedon][GenderConstants.Female] = "0";
            lengths[CreatureConstants.Ghoul_Lacedon][GenderConstants.Male] = "0";
            lengths[CreatureConstants.Ghoul_Lacedon][CreatureConstants.Ghoul_Lacedon] = "0";
            //Source: https://forgottenrealms.fandom.com/wiki/Cloud_giant
            lengths[CreatureConstants.Giant_Cloud][GenderConstants.Female] = "0";
            lengths[CreatureConstants.Giant_Cloud][GenderConstants.Male] = "0";
            lengths[CreatureConstants.Giant_Cloud][CreatureConstants.Giant_Cloud] = "0";
            lengths[CreatureConstants.Giant_Fire][GenderConstants.Female] = "0";
            lengths[CreatureConstants.Giant_Fire][GenderConstants.Male] = "0";
            lengths[CreatureConstants.Giant_Fire][CreatureConstants.Giant_Fire] = "0";
            lengths[CreatureConstants.Giant_Frost][GenderConstants.Female] = "0";
            lengths[CreatureConstants.Giant_Frost][GenderConstants.Male] = "0";
            lengths[CreatureConstants.Giant_Frost][CreatureConstants.Giant_Frost] = "0";
            lengths[CreatureConstants.Giant_Hill][GenderConstants.Female] = "0";
            lengths[CreatureConstants.Giant_Hill][GenderConstants.Male] = "0";
            lengths[CreatureConstants.Giant_Hill][CreatureConstants.Giant_Hill] = "0";
            lengths[CreatureConstants.GibberingMouther][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.GibberingMouther][CreatureConstants.GibberingMouther] = "0";
            lengths[CreatureConstants.Girallon][GenderConstants.Female] = "0";
            lengths[CreatureConstants.Girallon][GenderConstants.Male] = "0";
            lengths[CreatureConstants.Girallon][CreatureConstants.Girallon] = "0";
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
            lengths[CreatureConstants.Golem_Clay][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.Golem_Clay][CreatureConstants.Golem_Clay] = "0";
            lengths[CreatureConstants.Golem_Flesh][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.Golem_Flesh][CreatureConstants.Golem_Flesh] = "0";
            lengths[CreatureConstants.Golem_Iron][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.Golem_Iron][CreatureConstants.Golem_Iron] = "0";
            lengths[CreatureConstants.Golem_Stone][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.Golem_Stone][CreatureConstants.Golem_Stone] = "0";
            lengths[CreatureConstants.Golem_Stone_Greater][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.Golem_Stone_Greater][CreatureConstants.Golem_Stone_Greater] = "0";
            //Source: https://www.d20srd.org/srd/monsters/gorgon.htm
            lengths[CreatureConstants.Gorgon][GenderConstants.Female] = GetBaseFromAverage(8 * 12);
            lengths[CreatureConstants.Gorgon][GenderConstants.Male] = GetBaseFromAverage(8 * 12);
            lengths[CreatureConstants.Gorgon][CreatureConstants.Gorgon] = GetMultiplierFromAverage(8 * 12);
            //Source: https://www.d20srd.org/srd/monsters/ooze.htm#grayOoze
            lengths[CreatureConstants.GrayOoze][GenderConstants.Agender] = GetBaseFromUpTo(10 * 12);
            lengths[CreatureConstants.GrayOoze][CreatureConstants.GrayOoze] = GetMultiplierFromUpTo(10 * 12);
            lengths[CreatureConstants.GrayRender][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.GrayRender][CreatureConstants.GrayRender] = "0";
            lengths[CreatureConstants.GreenHag][GenderConstants.Female] = "0";
            lengths[CreatureConstants.GreenHag][CreatureConstants.GreenHag] = "0";
            //Source: https://www.d20srd.org/srd/monsters/grick.htm
            lengths[CreatureConstants.Grick][GenderConstants.Female] = GetBaseFromAverage(8 * 12);
            lengths[CreatureConstants.Grick][GenderConstants.Male] = GetBaseFromAverage(8 * 12);
            lengths[CreatureConstants.Grick][CreatureConstants.Grick] = GetMultiplierFromAverage(8 * 12);
            //Source: https://www.d20srd.org/srd/monsters/griffon.htm
            lengths[CreatureConstants.Griffon][GenderConstants.Female] = GetBaseFromUpTo(8 * 12);
            lengths[CreatureConstants.Griffon][GenderConstants.Male] = GetBaseFromUpTo(8 * 12);
            lengths[CreatureConstants.Griffon][CreatureConstants.Griffon] = GetMultiplierFromUpTo(8 * 12);
            lengths[CreatureConstants.Grig][GenderConstants.Female] = "0";
            lengths[CreatureConstants.Grig][GenderConstants.Male] = "0";
            lengths[CreatureConstants.Grig][CreatureConstants.Grig] = "0";
            lengths[CreatureConstants.Grig_WithFiddle][GenderConstants.Female] = "0";
            lengths[CreatureConstants.Grig_WithFiddle][GenderConstants.Male] = "0";
            lengths[CreatureConstants.Grig_WithFiddle][CreatureConstants.Grig_WithFiddle] = "0";
            lengths[CreatureConstants.Grimlock][GenderConstants.Female] = "0";
            lengths[CreatureConstants.Grimlock][GenderConstants.Male] = "0";
            lengths[CreatureConstants.Grimlock][CreatureConstants.Grimlock] = "0";
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
            lengths[CreatureConstants.Harpy][GenderConstants.Female] = "0";
            lengths[CreatureConstants.Harpy][GenderConstants.Male] = "0";
            lengths[CreatureConstants.Harpy][CreatureConstants.Harpy] = "0";
            //Source: https://www.d20srd.org/srd/monsters/hawk.htm
            lengths[CreatureConstants.Hawk][GenderConstants.Female] = GetBaseFromRange(12, 24);
            lengths[CreatureConstants.Hawk][GenderConstants.Male] = GetBaseFromRange(12, 24);
            lengths[CreatureConstants.Hawk][CreatureConstants.Hawk] = GetMultiplierFromRange(12, 24);
            //Source: https://forgottenrealms.fandom.com/wiki/Hell_hound
            //Scaling down from Nessian: 8*12*[24,54]/[64,72] = [36,72]
            lengths[CreatureConstants.HellHound][GenderConstants.Female] = GetBaseFromRange(36, 72);
            lengths[CreatureConstants.HellHound][GenderConstants.Male] = GetBaseFromRange(36, 72);
            lengths[CreatureConstants.HellHound][CreatureConstants.HellHound] = GetMultiplierFromRange(36, 72);
            lengths[CreatureConstants.HellHound_NessianWarhound][GenderConstants.Female] = GetBaseFromAverage(8 * 12);
            lengths[CreatureConstants.HellHound_NessianWarhound][GenderConstants.Male] = GetBaseFromAverage(8 * 12);
            lengths[CreatureConstants.HellHound_NessianWarhound][CreatureConstants.HellHound_NessianWarhound] = GetMultiplierFromAverage(8 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Hellcat
            lengths[CreatureConstants.Hellcat_Bezekira][GenderConstants.Female] = GetBaseFromRange(6 * 12, 9 * 12);
            lengths[CreatureConstants.Hellcat_Bezekira][GenderConstants.Male] = GetBaseFromRange(6 * 12, 9 * 12);
            lengths[CreatureConstants.Hellcat_Bezekira][CreatureConstants.Hellcat_Bezekira] = GetMultiplierFromRange(6 * 12, 9 * 12);
            //Source: https://www.d20srd.org/srd/monsters/swarm.htm
            lengths[CreatureConstants.Hellwasp_Swarm][GenderConstants.Agender] = GetBaseFromAverage(10 * 12);
            lengths[CreatureConstants.Hellwasp_Swarm][CreatureConstants.Hellwasp_Swarm] = GetMultiplierFromAverage(10 * 12);
            //Source: https://www.d20srd.org/srd/monsters/demon.htm#hezrou
            lengths[CreatureConstants.Hezrou][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.Hezrou][CreatureConstants.Hezrou] = "0";
            //Source: https://www.d20srd.org/srd/monsters/sphinx.htm
            lengths[CreatureConstants.Hieracosphinx][GenderConstants.Male] = GetBaseFromAverage(10 * 12);
            lengths[CreatureConstants.Hieracosphinx][CreatureConstants.Hieracosphinx] = GetMultiplierFromAverage(10 * 12);
            //Source: https://www.d20srd.org/srd/monsters/hippogriff.htm
            lengths[CreatureConstants.Hippogriff][GenderConstants.Female] = GetBaseFromAverage(9 * 12);
            lengths[CreatureConstants.Hippogriff][GenderConstants.Male] = GetBaseFromAverage(9 * 12);
            lengths[CreatureConstants.Hippogriff][CreatureConstants.Hippogriff] = GetMultiplierFromAverage(9 * 12);
            lengths[CreatureConstants.Hobgoblin][GenderConstants.Female] = "0";
            lengths[CreatureConstants.Hobgoblin][GenderConstants.Male] = "0";
            lengths[CreatureConstants.Hobgoblin][CreatureConstants.Hobgoblin] = "0";
            //Source: https://forgottenrealms.fandom.com/wiki/Homunculus
            //https://www.dimensions.com/element/eastern-gray-squirrel
            lengths[CreatureConstants.Homunculus][GenderConstants.Agender] = GetBaseFromRange(8, 11);
            lengths[CreatureConstants.Homunculus][CreatureConstants.Homunculus] = GetMultiplierFromRange(8, 11);
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
            lengths[CreatureConstants.Horse_Light_War][CreatureConstants.Horse_Light_War] = GetMultiplierFromAverage(8 * 12);
            lengths[CreatureConstants.HoundArchon][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.HoundArchon][CreatureConstants.HoundArchon] = "0";
            //Source: https://forgottenrealms.fandom.com/wiki/Howler
            lengths[CreatureConstants.Howler][GenderConstants.Agender] = GetBaseFromAverage(8 * 12);
            lengths[CreatureConstants.Howler][CreatureConstants.Howler] = GetMultiplierFromAverage(8 * 12);
            lengths[CreatureConstants.Human][GenderConstants.Female] = "0";
            lengths[CreatureConstants.Human][GenderConstants.Male] = "0";
            lengths[CreatureConstants.Human][CreatureConstants.Human] = "0";
            //Source: https://forgottenrealms.fandom.com/wiki/Hydra
            lengths[CreatureConstants.Hydra_5Heads][GenderConstants.Female] = GetBaseFromAverage(20 * 12);
            lengths[CreatureConstants.Hydra_5Heads][GenderConstants.Male] = GetBaseFromAverage(20 * 12);
            lengths[CreatureConstants.Hydra_5Heads][CreatureConstants.Hydra_5Heads] = GetMultiplierFromAverage(20 * 12);
            lengths[CreatureConstants.Hydra_6Heads][GenderConstants.Female] = GetBaseFromAverage(20 * 12);
            lengths[CreatureConstants.Hydra_6Heads][GenderConstants.Male] = GetBaseFromAverage(20 * 12);
            lengths[CreatureConstants.Hydra_6Heads][CreatureConstants.Hydra_6Heads] = GetMultiplierFromAverage(20 * 12);
            lengths[CreatureConstants.Hydra_7Heads][GenderConstants.Female] = GetBaseFromAverage(20 * 12);
            lengths[CreatureConstants.Hydra_7Heads][GenderConstants.Male] = GetBaseFromAverage(20 * 12);
            lengths[CreatureConstants.Hydra_7Heads][CreatureConstants.Hydra_7Heads] = GetMultiplierFromAverage(20 * 12);
            lengths[CreatureConstants.Hydra_8Heads][GenderConstants.Female] = GetBaseFromAverage(20 * 12);
            lengths[CreatureConstants.Hydra_8Heads][GenderConstants.Male] = GetBaseFromAverage(20 * 12);
            lengths[CreatureConstants.Hydra_8Heads][CreatureConstants.Hydra_8Heads] = GetMultiplierFromAverage(20 * 12);
            lengths[CreatureConstants.Hydra_9Heads][GenderConstants.Female] = GetBaseFromAverage(20 * 12);
            lengths[CreatureConstants.Hydra_9Heads][GenderConstants.Male] = GetBaseFromAverage(20 * 12);
            lengths[CreatureConstants.Hydra_9Heads][CreatureConstants.Hydra_9Heads] = GetMultiplierFromAverage(20 * 12);
            lengths[CreatureConstants.Hydra_10Heads][GenderConstants.Female] = GetBaseFromAverage(20 * 12);
            lengths[CreatureConstants.Hydra_10Heads][GenderConstants.Male] = GetBaseFromAverage(20 * 12);
            lengths[CreatureConstants.Hydra_10Heads][CreatureConstants.Hydra_10Heads] = GetMultiplierFromAverage(20 * 12);
            lengths[CreatureConstants.Hydra_11Heads][GenderConstants.Female] = GetBaseFromAverage(20 * 12);
            lengths[CreatureConstants.Hydra_11Heads][GenderConstants.Male] = GetBaseFromAverage(20 * 12);
            lengths[CreatureConstants.Hydra_11Heads][CreatureConstants.Hydra_11Heads] = GetMultiplierFromAverage(20 * 12);
            lengths[CreatureConstants.Hydra_12Heads][GenderConstants.Female] = GetBaseFromAverage(20 * 12);
            lengths[CreatureConstants.Hydra_12Heads][GenderConstants.Male] = GetBaseFromAverage(20 * 12);
            lengths[CreatureConstants.Hydra_12Heads][CreatureConstants.Hydra_12Heads] = GetMultiplierFromAverage(20 * 12);
            //Source: https://www.d20srd.org/srd/monsters/hyena.htm
            lengths[CreatureConstants.Hyena][GenderConstants.Female] = GetBaseFromAverage(36);
            lengths[CreatureConstants.Hyena][GenderConstants.Male] = GetBaseFromAverage(36);
            lengths[CreatureConstants.Hyena][CreatureConstants.Hyena] = GetMultiplierFromAverage(36);
            lengths[CreatureConstants.IceDevil_Gelugon][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.IceDevil_Gelugon][CreatureConstants.IceDevil_Gelugon] = "0";
            lengths[CreatureConstants.Imp][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.Imp][GenderConstants.Female] = "0";
            lengths[CreatureConstants.Imp][GenderConstants.Male] = "0";
            lengths[CreatureConstants.Imp][CreatureConstants.Imp] = "0";
            //Source: https://www.d20srd.org/srd/monsters/invisibleStalker.htm
            //https://www.d20srd.org/srd/combat/movementPositionAndDistance.htm using Large, since actual form is unknown
            lengths[CreatureConstants.InvisibleStalker][GenderConstants.Agender] = GetBaseFromRange(8 * 12, 16 * 12);
            lengths[CreatureConstants.InvisibleStalker][CreatureConstants.InvisibleStalker] = GetMultiplierFromRange(8 * 12, 16 * 12);
            lengths[CreatureConstants.Kobold][GenderConstants.Female] = "0";
            lengths[CreatureConstants.Kobold][GenderConstants.Male] = "0";
            lengths[CreatureConstants.Kobold][CreatureConstants.Kobold] = "0";
            lengths[CreatureConstants.Kolyarut][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.Kolyarut][CreatureConstants.Kolyarut] = "0";
            //Source: https://forgottenrealms.fandom.com/wiki/Kraken
            lengths[CreatureConstants.Kraken][GenderConstants.Female] = GetBaseFromRange(60 * 12, 90 * 12);
            lengths[CreatureConstants.Kraken][GenderConstants.Male] = GetBaseFromRange(60 * 12, 90 * 12);
            lengths[CreatureConstants.Kraken][CreatureConstants.Kraken] = GetMultiplierFromRange(60 * 12, 90 * 12);
            //Source: https://www.d20srd.org/srd/monsters/krenshar.htm
            lengths[CreatureConstants.Krenshar][GenderConstants.Female] = GetBaseFromRange(4 * 12, 5 * 12);
            lengths[CreatureConstants.Krenshar][GenderConstants.Male] = GetBaseFromRange(4 * 12, 5 * 12);
            lengths[CreatureConstants.Krenshar][CreatureConstants.Krenshar] = GetMultiplierFromRange(4 * 12, 5 * 12);
            lengths[CreatureConstants.KuoToa][GenderConstants.Female] = "0";
            lengths[CreatureConstants.KuoToa][GenderConstants.Male] = "0";
            lengths[CreatureConstants.KuoToa][CreatureConstants.KuoToa] = "0";
            //Source: https://forgottenrealms.fandom.com/wiki/Lamia
            lengths[CreatureConstants.Lamia][GenderConstants.Female] = GetBaseFromAverage(8 * 12);
            lengths[CreatureConstants.Lamia][GenderConstants.Male] = GetBaseFromAverage(8 * 12);
            lengths[CreatureConstants.Lamia][CreatureConstants.Lamia] = GetMultiplierFromAverage(8 * 12);
            //Source: https://www.d20srd.org/srd/monsters/lammasu.htm
            lengths[CreatureConstants.Lammasu][GenderConstants.Female] = GetBaseFromAverage(8 * 12);
            lengths[CreatureConstants.Lammasu][GenderConstants.Male] = GetBaseFromAverage(8 * 12);
            lengths[CreatureConstants.Lammasu][CreatureConstants.Lammasu] = GetMultiplierFromAverage(8 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Lantern_archon
            lengths[CreatureConstants.LanternArchon][GenderConstants.Agender] = GetBaseFromRange(12, 36);
            lengths[CreatureConstants.LanternArchon][CreatureConstants.LanternArchon] = GetMultiplierFromRange(12, 36);
            lengths[CreatureConstants.Lemure][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.Lemure][CreatureConstants.Lemure] = "0";
            lengths[CreatureConstants.Leonal][GenderConstants.Female] = "0";
            lengths[CreatureConstants.Leonal][GenderConstants.Male] = "0";
            lengths[CreatureConstants.Leonal][CreatureConstants.Leonal] = "0";
            //Source: https://www.d20srd.org/srd/monsters/leopard.htm
            lengths[CreatureConstants.Leopard][GenderConstants.Female] = GetBaseFromAverage(4 * 12);
            lengths[CreatureConstants.Leopard][GenderConstants.Male] = GetBaseFromAverage(4 * 12);
            lengths[CreatureConstants.Leopard][CreatureConstants.Leopard] = GetMultiplierFromAverage(4 * 12);
            //Source: https://www.dimensions.com/element/african-lion
            lengths[CreatureConstants.Lion][GenderConstants.Female] = GetBaseFromAverage(4 * 12 + 6);
            lengths[CreatureConstants.Lion][GenderConstants.Male] = GetBaseFromRange(4 * 12 + 6, 6 * 12 + 6);
            lengths[CreatureConstants.Lion][CreatureConstants.Lion] = GetMultiplierFromRange(4 * 12 + 6, 6 * 12 + 6);
            //Source: https://forgottenrealms.fandom.com/wiki/Dire_lion
            lengths[CreatureConstants.Lion_Dire][GenderConstants.Female] = GetBaseFromUpTo(15 * 12);
            lengths[CreatureConstants.Lion_Dire][GenderConstants.Male] = GetBaseFromUpTo(15 * 12);
            lengths[CreatureConstants.Lion_Dire][CreatureConstants.Lion_Dire] = GetMultiplierFromUpTo(15 * 12);
            lengths[CreatureConstants.Lizardfolk][GenderConstants.Female] = "0";
            lengths[CreatureConstants.Lizardfolk][GenderConstants.Male] = "0";
            lengths[CreatureConstants.Lizardfolk][CreatureConstants.Lizardfolk] = "0";
            lengths[CreatureConstants.Locathah][GenderConstants.Female] = "0";
            lengths[CreatureConstants.Locathah][GenderConstants.Male] = "0";
            lengths[CreatureConstants.Locathah][CreatureConstants.Locathah] = "0";
            //Source: https://www.d20srd.org/srd/monsters/swarm.htm
            lengths[CreatureConstants.Locust_Swarm][GenderConstants.Agender] = GetBaseFromAverage(10 * 12);
            lengths[CreatureConstants.Locust_Swarm][CreatureConstants.Locust_Swarm] = GetMultiplierFromAverage(10 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Marilith
            lengths[CreatureConstants.Marilith][GenderConstants.Female] = GetBaseFromAverage(20 * 12);
            lengths[CreatureConstants.Marilith][CreatureConstants.Marilith] = GetMultiplierFromAverage(20 * 12);
            lengths[CreatureConstants.Marut][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.Marut][CreatureConstants.Marut] = "0";
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
            //Source: https://forgottenrealms.fandom.com/wiki/Merfolk
            lengths[CreatureConstants.Merfolk][GenderConstants.Female] = GetBaseFromAverage(8 * 12);
            lengths[CreatureConstants.Merfolk][GenderConstants.Male] = GetBaseFromAverage(8 * 12);
            lengths[CreatureConstants.Merfolk][CreatureConstants.Merfolk] = GetMultiplierFromAverage(8 * 12);
            lengths[CreatureConstants.Minotaur][GenderConstants.Female] = "0";
            lengths[CreatureConstants.Minotaur][GenderConstants.Male] = "0";
            lengths[CreatureConstants.Minotaur][CreatureConstants.Minotaur] = "0";
            //Source: https://forgottenrealms.fandom.com/wiki/Dark_naga
            lengths[CreatureConstants.Naga_Dark][GenderConstants.Hermaphrodite] = GetBaseFromAverage(12 * 12);
            lengths[CreatureConstants.Naga_Dark][CreatureConstants.Naga_Dark] = GetMultiplierFromAverage(12 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Guardian_naga
            lengths[CreatureConstants.Naga_Guardian][GenderConstants.Hermaphrodite] = GetBaseFromRange(10 * 12, 20 * 12);
            lengths[CreatureConstants.Naga_Guardian][CreatureConstants.Naga_Guardian] = GetMultiplierFromRange(10 * 12, 20 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Spirit_naga
            lengths[CreatureConstants.Naga_Spirit][GenderConstants.Hermaphrodite] = GetBaseFromAverage(15 * 12);
            lengths[CreatureConstants.Naga_Spirit][CreatureConstants.Naga_Spirit] = GetMultiplierFromAverage(15 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Water_naga
            lengths[CreatureConstants.Naga_Water][GenderConstants.Hermaphrodite] = GetBaseFromRange(10 * 12, 20 * 12);
            lengths[CreatureConstants.Naga_Water][CreatureConstants.Naga_Water] = GetMultiplierFromRange(10 * 12, 20 * 12);
            lengths[CreatureConstants.Nalfeshnee][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.Nalfeshnee][CreatureConstants.Nalfeshnee] = "0";
            //Source: https://www.d20srd.org/srd/monsters/octopusGiant.htm
            lengths[CreatureConstants.Octopus_Giant][GenderConstants.Female] = GetBaseFromAtLeast(10 * 12);
            lengths[CreatureConstants.Octopus_Giant][GenderConstants.Male] = GetBaseFromAtLeast(10 * 12);
            lengths[CreatureConstants.Octopus_Giant][CreatureConstants.Octopus_Giant] = GetMultiplierFromAtLeast(10 * 12);
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
            //Source: https://www.d20srd.org/srd/monsters/owlGiant.htm
            //https://www.dimensions.com/element/great-horned-owl-bubo-virginianus scale up: [17,25]*9*12/[9,14] = [204,193]
            lengths[CreatureConstants.Owl_Giant][GenderConstants.Female] = GetBaseFromRange(193, 204);
            lengths[CreatureConstants.Owl_Giant][GenderConstants.Male] = GetBaseFromRange(193, 204);
            lengths[CreatureConstants.Owl_Giant][CreatureConstants.Owl_Giant] = GetMultiplierFromRange(193, 204);
            lengths[CreatureConstants.PitFiend][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.PitFiend][CreatureConstants.PitFiend] = "0";
            lengths[CreatureConstants.Pixie][GenderConstants.Female] = "0";
            lengths[CreatureConstants.Pixie][GenderConstants.Male] = "0";
            lengths[CreatureConstants.Pixie][CreatureConstants.Pixie] = "0";
            lengths[CreatureConstants.Pixie_WithIrresistibleDance][GenderConstants.Female] = "0";
            lengths[CreatureConstants.Pixie_WithIrresistibleDance][GenderConstants.Male] = "0";
            lengths[CreatureConstants.Pixie_WithIrresistibleDance][CreatureConstants.Pixie_WithIrresistibleDance] = "0";
            lengths[CreatureConstants.PrayingMantis_Giant][GenderConstants.Female] = "0";
            lengths[CreatureConstants.PrayingMantis_Giant][GenderConstants.Male] = "0";
            lengths[CreatureConstants.PrayingMantis_Giant][CreatureConstants.PrayingMantis_Giant] = "0";
            //Source: https://forgottenrealms.fandom.com/wiki/Hydra
            lengths[CreatureConstants.Pyrohydra_5Heads][GenderConstants.Female] = GetBaseFromAverage(20 * 12);
            lengths[CreatureConstants.Pyrohydra_5Heads][GenderConstants.Male] = GetBaseFromAverage(20 * 12);
            lengths[CreatureConstants.Pyrohydra_5Heads][CreatureConstants.Pyrohydra_5Heads] = GetMultiplierFromAverage(20 * 12);
            lengths[CreatureConstants.Pyrohydra_6Heads][GenderConstants.Female] = GetBaseFromAverage(20 * 12);
            lengths[CreatureConstants.Pyrohydra_6Heads][GenderConstants.Male] = GetBaseFromAverage(20 * 12);
            lengths[CreatureConstants.Pyrohydra_6Heads][CreatureConstants.Pyrohydra_6Heads] = GetMultiplierFromAverage(20 * 12);
            lengths[CreatureConstants.Pyrohydra_7Heads][GenderConstants.Female] = GetBaseFromAverage(20 * 12);
            lengths[CreatureConstants.Pyrohydra_7Heads][GenderConstants.Male] = GetBaseFromAverage(20 * 12);
            lengths[CreatureConstants.Pyrohydra_7Heads][CreatureConstants.Pyrohydra_7Heads] = GetMultiplierFromAverage(20 * 12);
            lengths[CreatureConstants.Pyrohydra_8Heads][GenderConstants.Female] = GetBaseFromAverage(20 * 12);
            lengths[CreatureConstants.Pyrohydra_8Heads][GenderConstants.Male] = GetBaseFromAverage(20 * 12);
            lengths[CreatureConstants.Pyrohydra_8Heads][CreatureConstants.Pyrohydra_8Heads] = GetMultiplierFromAverage(20 * 12);
            lengths[CreatureConstants.Pyrohydra_9Heads][GenderConstants.Female] = GetBaseFromAverage(20 * 12);
            lengths[CreatureConstants.Pyrohydra_9Heads][GenderConstants.Male] = GetBaseFromAverage(20 * 12);
            lengths[CreatureConstants.Pyrohydra_9Heads][CreatureConstants.Pyrohydra_9Heads] = GetMultiplierFromAverage(20 * 12);
            lengths[CreatureConstants.Pyrohydra_10Heads][GenderConstants.Female] = GetBaseFromAverage(20 * 12);
            lengths[CreatureConstants.Pyrohydra_10Heads][GenderConstants.Male] = GetBaseFromAverage(20 * 12);
            lengths[CreatureConstants.Pyrohydra_10Heads][CreatureConstants.Pyrohydra_10Heads] = GetMultiplierFromAverage(20 * 12);
            lengths[CreatureConstants.Pyrohydra_11Heads][GenderConstants.Female] = GetBaseFromAverage(20 * 12);
            lengths[CreatureConstants.Pyrohydra_11Heads][GenderConstants.Male] = GetBaseFromAverage(20 * 12);
            lengths[CreatureConstants.Pyrohydra_11Heads][CreatureConstants.Pyrohydra_11Heads] = GetMultiplierFromAverage(20 * 12);
            lengths[CreatureConstants.Pyrohydra_12Heads][GenderConstants.Female] = GetBaseFromAverage(20 * 12);
            lengths[CreatureConstants.Pyrohydra_12Heads][GenderConstants.Male] = GetBaseFromAverage(20 * 12);
            lengths[CreatureConstants.Pyrohydra_12Heads][CreatureConstants.Pyrohydra_12Heads] = GetMultiplierFromAverage(20 * 12);
            lengths[CreatureConstants.Quasit][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.Quasit][CreatureConstants.Quasit] = "0";
            //Source: https://www.dimensions.com/element/common-rat
            lengths[CreatureConstants.Rat][GenderConstants.Female] = GetBaseFromRange(6, 10);
            lengths[CreatureConstants.Rat][GenderConstants.Male] = GetBaseFromRange(6, 10);
            lengths[CreatureConstants.Rat][CreatureConstants.Rat] = GetMultiplierFromRange(6, 10);
            //Source: https://forgottenrealms.fandom.com/wiki/Giant_rat
            lengths[CreatureConstants.Rat_Dire][GenderConstants.Female] = GetBaseFromAverage(4 * 12);
            lengths[CreatureConstants.Rat_Dire][GenderConstants.Male] = GetBaseFromAverage(4 * 12);
            lengths[CreatureConstants.Rat_Dire][CreatureConstants.Rat_Dire] = GetMultiplierFromAverage(4 * 12);
            //Source: https://www.d20srd.org/srd/monsters/swarm.htm
            lengths[CreatureConstants.Rat_Swarm][GenderConstants.Agender] = GetBaseFromAverage(10 * 12);
            lengths[CreatureConstants.Rat_Swarm][CreatureConstants.Rat_Swarm] = GetMultiplierFromAverage(10 * 12);
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
            //Source: https://www.worldanvil.com/w/faerun-tatortotzke/a/salamander-article (average)
            //Scaling down by half for flamebrother, Scaling up x2 for noble.
            lengths[CreatureConstants.Salamander_Flamebrother][GenderConstants.Agender] = GetBaseFromAverage(20 * 12 / 2);
            lengths[CreatureConstants.Salamander_Flamebrother][CreatureConstants.Salamander_Flamebrother] = GetMultiplierFromAverage(20 * 12 / 2);
            lengths[CreatureConstants.Salamander_Average][GenderConstants.Agender] = GetBaseFromAverage(20 * 12);
            lengths[CreatureConstants.Salamander_Average][CreatureConstants.Salamander_Average] = GetMultiplierFromAverage(20 * 12);
            lengths[CreatureConstants.Salamander_Noble][GenderConstants.Agender] = GetBaseFromAverage(20 * 12 * 2);
            lengths[CreatureConstants.Salamander_Noble][CreatureConstants.Salamander_Noble] = GetMultiplierFromAverage(20 * 12 * 2);
            lengths[CreatureConstants.Satyr][GenderConstants.Male] = "0";
            lengths[CreatureConstants.Satyr][CreatureConstants.Satyr] = "0";
            lengths[CreatureConstants.Satyr_WithPipes][GenderConstants.Male] = "0";
            lengths[CreatureConstants.Satyr_WithPipes][CreatureConstants.Satyr] = "0";
            lengths[CreatureConstants.SeaHag][GenderConstants.Female] = "0";
            lengths[CreatureConstants.SeaHag][CreatureConstants.SeaHag] = "0";
            //Source: https://www.d20srd.org/srd/monsters/shadow.htm
            lengths[CreatureConstants.Shadow][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.Shadow][CreatureConstants.Shadow] = "0";
            lengths[CreatureConstants.Shadow_Greater][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.Shadow_Greater][CreatureConstants.Shadow_Greater] = "0";
            //Source: https://www.dimensions.com/element/blacktip-shark-carcharhinus-limbatus
            lengths[CreatureConstants.Shark_Medium][GenderConstants.Female] = GetBaseFromRange(59, 8 * 12);
            lengths[CreatureConstants.Shark_Medium][GenderConstants.Male] = GetBaseFromRange(59, 8 * 12);
            lengths[CreatureConstants.Shark_Medium][CreatureConstants.Shark_Medium] = GetMultiplierFromRange(59, 8 * 12);
            //Source: https://www.dimensions.com/element/thresher-shark
            lengths[CreatureConstants.Shark_Large][GenderConstants.Female] = GetBaseFromRange(10 * 12 + 6, 20 * 12);
            lengths[CreatureConstants.Shark_Large][GenderConstants.Male] = GetBaseFromRange(10 * 12 + 6, 20 * 12);
            lengths[CreatureConstants.Shark_Large][CreatureConstants.Shark_Large] = GetMultiplierFromRange(10 * 12 + 6, 20 * 12);
            //Source: https://www.dimensions.com/element/great-white-shark
            lengths[CreatureConstants.Shark_Huge][GenderConstants.Female] = GetBaseFromRange(15 * 12, 21 * 12);
            lengths[CreatureConstants.Shark_Huge][GenderConstants.Male] = GetBaseFromRange(11 * 12, 13 * 12);
            lengths[CreatureConstants.Shark_Huge][CreatureConstants.Shark_Huge] = GetMultiplierFromRange(15 * 12, 21 * 12);
            //Source: https://www.d20srd.org/srd/monsters/direShark.htm
            lengths[CreatureConstants.Shark_Dire][GenderConstants.Female] = GetBaseFromAverage(25 * 12);
            lengths[CreatureConstants.Shark_Dire][GenderConstants.Male] = GetBaseFromAverage(25 * 12);
            lengths[CreatureConstants.Shark_Dire][CreatureConstants.Shark_Dire] = GetMultiplierFromAverage(25 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Blue_slaad
            lengths[CreatureConstants.Slaad_Blue][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.Slaad_Blue][CreatureConstants.Slaad_Blue] = "0";
            //Source: https://forgottenrealms.fandom.com/wiki/Red_slaad
            lengths[CreatureConstants.Slaad_Red][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.Slaad_Red][CreatureConstants.Slaad_Red] = "0";
            //Source: https://forgottenrealms.fandom.com/wiki/Green_slaad
            lengths[CreatureConstants.Slaad_Green][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.Slaad_Green][CreatureConstants.Slaad_Green] = "0";
            //Source: https://forgottenrealms.fandom.com/wiki/Gray_slaad
            lengths[CreatureConstants.Slaad_Gray][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.Slaad_Gray][CreatureConstants.Slaad_Gray] = "0";
            //Source: https://forgottenrealms.fandom.com/wiki/Gray_slaad
            lengths[CreatureConstants.Slaad_Death][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.Slaad_Death][CreatureConstants.Slaad_Death] = "0";
            //Source: https://www.dimensions.com/element/green-tree-python-morelia-viridis
            lengths[CreatureConstants.Snake_Constrictor][GenderConstants.Female] = GetBaseFromRange(60, 78);
            lengths[CreatureConstants.Snake_Constrictor][GenderConstants.Male] = GetBaseFromRange(60, 78);
            lengths[CreatureConstants.Snake_Constrictor][CreatureConstants.Snake_Constrictor] = GetMultiplierFromRange(60, 78);
            //Source: https://www.dimensions.com/element/burmese-python-python-bivittatus
            lengths[CreatureConstants.Snake_Constrictor_Giant][GenderConstants.Female] = GetBaseFromRange(8 * 12, 26 * 12);
            lengths[CreatureConstants.Snake_Constrictor_Giant][GenderConstants.Male] = GetBaseFromRange(8 * 12, 26 * 12);
            lengths[CreatureConstants.Snake_Constrictor_Giant][CreatureConstants.Snake_Constrictor_Giant] = GetMultiplierFromRange(8 * 12, 26 * 12);
            //Source: https://www.d20srd.org/srd/monsters/swarm.htm
            lengths[CreatureConstants.Spider_Swarm][GenderConstants.Agender] = GetBaseFromAverage(10 * 12);
            lengths[CreatureConstants.Spider_Swarm][CreatureConstants.Spider_Swarm] = GetMultiplierFromAverage(10 * 12);
            //Source: https://www.d20srd.org/srd/monsters/squidGiant.htm
            lengths[CreatureConstants.Squid_Giant][GenderConstants.Female] = GetBaseFromUpTo(20 * 20);
            lengths[CreatureConstants.Squid_Giant][GenderConstants.Male] = GetBaseFromUpTo(20 * 20);
            lengths[CreatureConstants.Squid_Giant][CreatureConstants.Squid_Giant] = GetMultiplierFromUpTo(20 * 20);
            //Source: https://www.d20srd.org/srd/monsters/giantStagBeetle.htm
            lengths[CreatureConstants.StagBeetle_Giant][GenderConstants.Female] = GetBaseFromAverage(10 * 12);
            lengths[CreatureConstants.StagBeetle_Giant][GenderConstants.Male] = GetBaseFromAverage(10 * 12);
            lengths[CreatureConstants.StagBeetle_Giant][CreatureConstants.StagBeetle_Giant] = GetMultiplierFromAverage(10 * 12);
            lengths[CreatureConstants.Succubus][GenderConstants.Female] = "0";
            lengths[CreatureConstants.Succubus][GenderConstants.Male] = "0";
            lengths[CreatureConstants.Succubus][CreatureConstants.Succubus] = "0";
            lengths[CreatureConstants.Tiefling][GenderConstants.Female] = "0";
            lengths[CreatureConstants.Tiefling][GenderConstants.Male] = "0";
            lengths[CreatureConstants.Tiefling][CreatureConstants.Tiefling] = "0";
            //Source: https://www.dimensions.com/element/bengal-tiger
            lengths[CreatureConstants.Tiger][GenderConstants.Female] = GetBaseFromRange(5 * 12 + 3, 6 * 12 + 5);
            lengths[CreatureConstants.Tiger][GenderConstants.Male] = GetBaseFromRange(5 * 12 + 3, 6 * 12 + 5);
            lengths[CreatureConstants.Tiger][CreatureConstants.Tiger] = GetMultiplierFromRange(5 * 12 + 3, 6 * 12 + 5);
            //Source: https://forgottenrealms.fandom.com/wiki/Dire_tiger
            lengths[CreatureConstants.Tiger_Dire][GenderConstants.Female] = GetBaseFromAverage(12 * 12);
            lengths[CreatureConstants.Tiger_Dire][GenderConstants.Male] = GetBaseFromAverage(12 * 12);
            lengths[CreatureConstants.Tiger_Dire][CreatureConstants.Tiger_Dire] = GetMultiplierFromAverage(12 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Tojanida
            lengths[CreatureConstants.Tojanida_Juvenile][GenderConstants.Agender] = GetBaseFromAverage(3 * 12);
            lengths[CreatureConstants.Tojanida_Juvenile][CreatureConstants.Tojanida_Juvenile] = GetMultiplierFromAverage(3 * 12);
            lengths[CreatureConstants.Tojanida_Adult][GenderConstants.Agender] = GetBaseFromAverage(6 * 12);
            lengths[CreatureConstants.Tojanida_Adult][CreatureConstants.Tojanida_Adult] = GetMultiplierFromAverage(6 * 12);
            lengths[CreatureConstants.Tojanida_Elder][GenderConstants.Agender] = GetBaseFromAverage(9 * 12);
            lengths[CreatureConstants.Tojanida_Elder][CreatureConstants.Tojanida_Elder] = GetMultiplierFromAverage(9 * 12);
            lengths[CreatureConstants.Vrock][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.Vrock][CreatureConstants.Vrock] = "0";
            //Source: https://forgottenrealms.fandom.com/wiki/Giant_wasp
            lengths[CreatureConstants.Wasp_Giant][GenderConstants.Male] = GetBaseFromAverage(5 * 12);
            lengths[CreatureConstants.Wasp_Giant][CreatureConstants.Wasp_Giant] = GetMultiplierFromAverage(5 * 12);
            //Source: https://www.dimensions.com/element/least-weasel-mustela-nivalis
            lengths[CreatureConstants.Weasel][GenderConstants.Female] = GetBaseFromRange(6, 10);
            lengths[CreatureConstants.Weasel][GenderConstants.Male] = GetBaseFromRange(6, 10);
            lengths[CreatureConstants.Weasel][CreatureConstants.Weasel] = GetMultiplierFromRange(6, 10);
            //Source: https://www.d20srd.org/srd/monsters/direWeasel.htm
            lengths[CreatureConstants.Weasel_Dire][GenderConstants.Female] = GetBaseFromUpTo(10 * 12);
            lengths[CreatureConstants.Weasel_Dire][GenderConstants.Male] = GetBaseFromUpTo(10 * 12);
            lengths[CreatureConstants.Weasel_Dire][CreatureConstants.Weasel_Dire] = GetMultiplierFromUpTo(10 * 12);
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
            //Source: https://forgottenrealms.fandom.com/wiki/Dire_wolf
            lengths[CreatureConstants.Wolf_Dire][GenderConstants.Female] = GetBaseFromAverage(9 * 12);
            lengths[CreatureConstants.Wolf_Dire][GenderConstants.Male] = GetBaseFromAverage(9 * 12);
            lengths[CreatureConstants.Wolf_Dire][CreatureConstants.Wolf_Dire] = GetMultiplierFromAverage(9 * 12);
            //Source: https://www.dimensions.com/element/wolverine-gulo-gulo
            lengths[CreatureConstants.Wolverine][GenderConstants.Female] = GetBaseFromRange(25, 41);
            lengths[CreatureConstants.Wolverine][GenderConstants.Male] = GetBaseFromRange(25, 41);
            lengths[CreatureConstants.Wolverine][CreatureConstants.Wolverine] = GetMultiplierFromRange(25, 41);
            //Source: https://www.d20srd.org/srd/monsters/direWolverine.htm
            lengths[CreatureConstants.Wolverine_Dire][GenderConstants.Female] = GetBaseFromAverage(12 * 12);
            lengths[CreatureConstants.Wolverine_Dire][GenderConstants.Male] = GetBaseFromAverage(12 * 12);
            lengths[CreatureConstants.Wolverine_Dire][CreatureConstants.Wolverine_Dire] = GetMultiplierFromAverage(12 * 12);
            lengths[CreatureConstants.Wraith][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.Wraith][CreatureConstants.Wraith] = "0";
            lengths[CreatureConstants.Wraith_Dread][GenderConstants.Agender] = "0";
            lengths[CreatureConstants.Wraith_Dread][CreatureConstants.Wraith_Dread] = "0";
            //Source: https://forgottenrealms.fandom.com/wiki/Xorn
            lengths[CreatureConstants.Xorn_Minor][GenderConstants.Agender] = GetBaseFromAverage(3 * 12);
            lengths[CreatureConstants.Xorn_Minor][CreatureConstants.Xorn_Minor] = GetMultiplierFromAverage(3 * 12);
            lengths[CreatureConstants.Xorn_Average][GenderConstants.Agender] = GetBaseFromAverage(5 * 12);
            lengths[CreatureConstants.Xorn_Average][CreatureConstants.Xorn_Average] = GetMultiplierFromAverage(5 * 12);
            lengths[CreatureConstants.Xorn_Elder][GenderConstants.Agender] = GetBaseFromAverage(8 * 12);
            lengths[CreatureConstants.Xorn_Elder][CreatureConstants.Xorn_Elder] = GetMultiplierFromAverage(8 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Zelekhut - using Centaur
            lengths[CreatureConstants.Zelekhut][GenderConstants.Agender] = GetBaseFromAverage(8 * 12);
            lengths[CreatureConstants.Zelekhut][CreatureConstants.Zelekhut] = GetMultiplierFromAverage(8 * 12);

            return lengths;
        }

        public static IEnumerable CreatureLengthsData => GetCreatureLengths().Select(t => new TestCaseData(t.Key, t.Value));

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
        [TestCase(CreatureConstants.Lion, GenderConstants.Female, 4 * 12 + 6)]
        [TestCase(CreatureConstants.Locathah, GenderConstants.Male, 5 * 12)]
        [TestCase(CreatureConstants.Locathah, GenderConstants.Female, 5 * 12)]
        [TestCase(CreatureConstants.Minotaur, GenderConstants.Male, 9 * 12)]
        [TestCase(CreatureConstants.Minotaur, GenderConstants.Female, 7 * 12)]
        public void RollCalculationsAreAccurate_FromAverage(string creature, string gender, int average)
        {
            var lengths = GetCreatureLengths();

            Assert.That(lengths, Contains.Key(creature));
            Assert.That(lengths[creature], Contains.Key(creature).And.ContainKey(gender));

            var baseLength = dice.Roll(lengths[creature][gender]).AsSum();
            var multiplierMin = dice.Roll(lengths[creature][creature]).AsPotentialMinimum();
            var multiplierAvg = dice.Roll(lengths[creature][creature]).AsPotentialAverage();
            var multiplierMax = dice.Roll(lengths[creature][creature]).AsPotentialMaximum();
            var theoreticalRoll = RollHelper.GetRollWithFewestDice(average * 9 / 10, average * 11 / 10);

            Assert.That(baseLength + multiplierMin, Is.EqualTo(average * 0.9).Within(1), $"Min (-10%); Theoretical: {theoreticalRoll}");
            Assert.That(baseLength + multiplierAvg, Is.EqualTo(average).Within(1), $"Average; Theoretical: {theoreticalRoll}");
            Assert.That(baseLength + multiplierMax, Is.EqualTo(average * 1.1).Within(1), $"Max (+10%); Theoretical: {theoreticalRoll}");
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
        [TestCase(CreatureConstants.Baboon, GenderConstants.Male, 24, 48)]
        [TestCase(CreatureConstants.Baboon, GenderConstants.Female, 21, 38)]
        [TestCase(CreatureConstants.Badger, GenderConstants.Male, 24, 36)]
        [TestCase(CreatureConstants.Badger, GenderConstants.Female, 24, 36)]
        [TestCase(CreatureConstants.Badger_Dire, GenderConstants.Male, 5 * 12, 7 * 12)]
        [TestCase(CreatureConstants.Badger_Dire, GenderConstants.Female, 5 * 12, 7 * 12)]
        [TestCase(CreatureConstants.Bear_Black, GenderConstants.Male, 5 * 12 + 2, 6 * 12 + 8)]
        [TestCase(CreatureConstants.Bear_Black, GenderConstants.Female, 4 * 12, 5 * 12 + 6)]
        [TestCase(CreatureConstants.Bear_Brown, GenderConstants.Male, 7 * 12, 8 * 12)]
        [TestCase(CreatureConstants.Bear_Brown, GenderConstants.Female, 5 * 12 + 6, 6 * 12 + 6)]
        [TestCase(CreatureConstants.Bear_Polar, GenderConstants.Male, 8 * 12 + 6, 9 * 12 + 10)]
        [TestCase(CreatureConstants.Bear_Polar, GenderConstants.Female, 7 * 12 + 10, 9 * 12 + 2)]
        [TestCase(CreatureConstants.Bugbear, GenderConstants.Male, 6 * 12, 8 * 12)]
        [TestCase(CreatureConstants.Bugbear, GenderConstants.Female, 6 * 12, 8 * 12)]
        [TestCase(CreatureConstants.Centaur, GenderConstants.Male, 7 * 12, 9 * 12)]
        [TestCase(CreatureConstants.Centaur, GenderConstants.Female, 7 * 12, 9 * 12)]
        [TestCase(CreatureConstants.Crocodile_Giant, GenderConstants.Male, 14 * 12, 23 * 12)]
        [TestCase(CreatureConstants.Crocodile_Giant, GenderConstants.Female, 7 * 12 + 6, 11 * 12)]
        [TestCase(CreatureConstants.DisplacerBeast, GenderConstants.Male, 9 * 12, 12 * 12)]
        [TestCase(CreatureConstants.DisplacerBeast, GenderConstants.Female, 8 * 12, 9 * 12)]
        [TestCase(CreatureConstants.Dog_Riding, GenderConstants.Male, 32, 47)]
        [TestCase(CreatureConstants.Dog_Riding, GenderConstants.Female, 29, 44)]
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
        [TestCase(CreatureConstants.Lion, GenderConstants.Male, 4 * 12 + 6, 6 * 12 + 6)]
        [TestCase(CreatureConstants.Nalfeshnee, GenderConstants.Agender, 10 * 12, 20 * 12)]
        [TestCase(CreatureConstants.Ogre, GenderConstants.Male, 10 * 12 + 1, 10 * 12 + 10)]
        [TestCase(CreatureConstants.Ogre, GenderConstants.Female, 9 * 12 + 3, 10 * 12)]
        [TestCase(CreatureConstants.Pixie, GenderConstants.Male, 12, 30)]
        [TestCase(CreatureConstants.Pixie, GenderConstants.Female, 12, 30)]
        [TestCase(CreatureConstants.Quasit, GenderConstants.Agender, 1 * 12, 2 * 12)]
        [TestCase(CreatureConstants.Salamander_Noble, GenderConstants.Agender, 8 * 12, 16 * 12)]
        [TestCase(CreatureConstants.Salamander_Average, GenderConstants.Agender, 4 * 12, 8 * 12)]
        [TestCase(CreatureConstants.Salamander_Flamebrother, GenderConstants.Agender, 2 * 12, 4 * 12)]
        [TestCase(CreatureConstants.Shark_Huge, GenderConstants.Female, 15 * 12, 21 * 12)]
        [TestCase(CreatureConstants.Shark_Huge, GenderConstants.Male, 11 * 12, 13 * 12)]
        [TestCase(CreatureConstants.Wolf, GenderConstants.Male, 41, 63)]
        [TestCase(CreatureConstants.Wolf, GenderConstants.Female, 41, 63)]
        [TestCase(CreatureConstants.Whale_Baleen, GenderConstants.Male, 30 * 12, 60 * 12)]
        [TestCase(CreatureConstants.Whale_Baleen, GenderConstants.Female, 30 * 12, 60 * 12)]
        public void RollCalculationsAreAccurate_FromRange(string creature, string gender, int min, int max)
        {
            var lengths = GetCreatureLengths();

            Assert.That(lengths, Contains.Key(creature));
            Assert.That(lengths[creature], Contains.Key(creature).And.ContainKey(gender));

            var baseLength = dice.Roll(lengths[creature][gender]).AsSum();
            var multiplierMin = dice.Roll(lengths[creature][creature]).AsPotentialMinimum();
            var multiplierMax = dice.Roll(lengths[creature][creature]).AsPotentialMaximum();
            var theoreticalRoll = RollHelper.GetRollWithFewestDice(min, max);

            Assert.That(baseLength + multiplierMin, Is.EqualTo(min), $"Min; Theoretical: {theoreticalRoll}");
            Assert.That(baseLength + multiplierMax, Is.EqualTo(max), $"Max; Theoretical: {theoreticalRoll}");
        }

        [Test]
        public void IfCreatureHasNoLength_HasHeight()
        {
            var heights = HeightsTests.GetCreatureHeights();
            var lengths = GetCreatureLengths();
            var creatures = CreatureConstants.GetAll();

            foreach (var creature in creatures)
            {
                Assert.That(lengths, Contains.Key(creature), "Lengths");
                Assert.That(lengths[creature], Contains.Key(creature), $"Lengths[{creature}]");
                Assert.That(heights, Contains.Key(creature), "Heights");
                Assert.That(heights[creature], Contains.Key(creature), $"Heights[{creature}]");

                Assert.That(lengths[creature][creature], Is.Not.Empty, $"Lengths[{creature}][{creature}]");
                Assert.That(heights[creature][creature], Is.Not.Empty, $"Heights[{creature}][{creature}]");

                if (lengths[creature][creature] == "0")
                    Assert.That(heights[creature][creature], Is.Not.EqualTo("0"), creature);
            }
        }
    }
}