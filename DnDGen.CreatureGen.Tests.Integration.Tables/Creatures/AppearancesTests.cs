using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Tables;
using DnDGen.CreatureGen.Tests.Integration.TestData;
using DnDGen.Infrastructure.Selectors.Collections;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Integration.Tables.Creatures
{
    [TestFixture]
    internal class AppearancesTests : CollectionTests
    {
        protected override string tableName => TableNameConstants.Collection.Appearances;

        private Dictionary<string, IEnumerable<string>> creatureAppearances;
        private ICollectionSelector collectionSelector;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            collectionSelector = GetNewInstanceOf<ICollectionSelector>();
            creatureAppearances = GetCreatureAppearances();
        }

        [Test]
        public void AppearancesNames()
        {
            var creatures = CreatureConstants.GetAll();
            AssertCollectionNames(creatures);
        }

        [TestCaseSource(typeof(CreatureTestData), nameof(CreatureTestData.Creatures))]
        public void Appearances(string creature)
        {
            Assert.Fail("Need to fill out test cases");

            Assert.That(creatureAppearances, Contains.Key(creature));
            Assert.That(creatureAppearances[creature], Is.Not.Empty);

            AssertCollection(creature, creatureAppearances[creature].ToArray());
        }


        private Dictionary<string, IEnumerable<string>> GetCreatureAppearances()
        {
            var creatures = CreatureConstants.GetAll();
            var appearances = new Dictionary<string, IEnumerable<string>>();

            foreach (var creature in creatures)
            {
                appearances[creature] = new string[0];
            }

            //Source: https://forgottenrealms.fandom.com/wiki/Aasimar
            var common = new[] { "Pupil-less pale white eyes and silver hair" };
            var uncommon = new[] { };
            appearances[CreatureConstants.Aasimar] = collectionSelector.CreateWeighted(common: common, uncommon: uncommon);
            //Source: https://forgottenrealms.fandom.com/wiki/Aboleth
            appearances[CreatureConstants.Aboleth][GenderConstants.Hermaphrodite] = "0";
            appearances[CreatureConstants.Aboleth][CreatureConstants.Aboleth] = "0";
            //Source: https://www.d20srd.org/srd/monsters/achaierai.htm
            appearances[CreatureConstants.Achaierai][GenderConstants.Female] = GetBaseFromAverage(15 * 12);
            appearances[CreatureConstants.Achaierai][GenderConstants.Male] = GetBaseFromAverage(15 * 12);
            appearances[CreatureConstants.Achaierai][CreatureConstants.Achaierai] = GetMultiplierFromAverage(15 * 12);
            //Basing off humans
            appearances[CreatureConstants.Allip][GenderConstants.Female] = "4*12+5";
            appearances[CreatureConstants.Allip][GenderConstants.Male] = "4*12+10";
            appearances[CreatureConstants.Allip][CreatureConstants.Allip] = "2d10";
            //Source: https://www.mojobob.com/roleplay/monstrousmanual/s/sphinx.html
            appearances[CreatureConstants.Androsphinx][GenderConstants.Male] = GetBaseFromAverage(8 * 12);
            appearances[CreatureConstants.Androsphinx][CreatureConstants.Androsphinx] = GetMultiplierFromAverage(8 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Astral_deva
            appearances[CreatureConstants.Angel_AstralDeva][GenderConstants.Female] = GetBaseFromRange(7 * 12, 7 * 12 + 6);
            appearances[CreatureConstants.Angel_AstralDeva][GenderConstants.Male] = GetBaseFromRange(7 * 12, 7 * 12 + 6);
            appearances[CreatureConstants.Angel_AstralDeva][CreatureConstants.Angel_AstralDeva] = GetMultiplierFromRange(7 * 12, 7 * 12 + 6);
            //Source: https://forgottenrealms.fandom.com/wiki/Planetar
            appearances[CreatureConstants.Angel_Planetar][GenderConstants.Female] = GetBaseFromRange(8 * 12, 9 * 12);
            appearances[CreatureConstants.Angel_Planetar][GenderConstants.Male] = GetBaseFromRange(8 * 12, 9 * 12);
            appearances[CreatureConstants.Angel_Planetar][CreatureConstants.Angel_Planetar] = GetMultiplierFromRange(8 * 12, 9 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Solar
            appearances[CreatureConstants.Angel_Solar][GenderConstants.Female] = GetBaseFromRange(9 * 12, 10 * 12);
            appearances[CreatureConstants.Angel_Solar][GenderConstants.Male] = GetBaseFromRange(9 * 12, 10 * 12);
            appearances[CreatureConstants.Angel_Solar][CreatureConstants.Angel_Solar] = GetMultiplierFromRange(9 * 12, 10 * 12);
            //Source: https://www.d20srd.org/srd/combat/movementPositionAndDistance.htm
            appearances[CreatureConstants.AnimatedObject_Colossal][GenderConstants.Agender] = GetBaseFromRange(64 * 12, 128 * 12);
            appearances[CreatureConstants.AnimatedObject_Colossal][CreatureConstants.AnimatedObject_Colossal] = GetMultiplierFromRange(64 * 12, 128 * 12);
            appearances[CreatureConstants.AnimatedObject_Colossal_Flexible][GenderConstants.Agender] = "0";
            appearances[CreatureConstants.AnimatedObject_Colossal_Flexible][CreatureConstants.AnimatedObject_Colossal_Flexible] = "0";
            appearances[CreatureConstants.AnimatedObject_Colossal_MultipleLegs][GenderConstants.Agender] = GetBaseFromRange(64 * 12, 128 * 12);
            appearances[CreatureConstants.AnimatedObject_Colossal_MultipleLegs][CreatureConstants.AnimatedObject_Colossal_MultipleLegs] = GetMultiplierFromRange(64 * 12, 128 * 12);
            appearances[CreatureConstants.AnimatedObject_Colossal_MultipleLegs_Wooden][GenderConstants.Agender] = GetBaseFromRange(64 * 12, 128 * 12);
            appearances[CreatureConstants.AnimatedObject_Colossal_MultipleLegs_Wooden][CreatureConstants.AnimatedObject_Colossal_MultipleLegs_Wooden] = GetMultiplierFromRange(64 * 12, 128 * 12);
            appearances[CreatureConstants.AnimatedObject_Colossal_Sheetlike][GenderConstants.Agender] = "0";
            appearances[CreatureConstants.AnimatedObject_Colossal_Sheetlike][CreatureConstants.AnimatedObject_Colossal_Sheetlike] = "0";
            appearances[CreatureConstants.AnimatedObject_Colossal_TwoLegs][GenderConstants.Agender] = GetBaseFromRange(64 * 12, 128 * 12);
            appearances[CreatureConstants.AnimatedObject_Colossal_TwoLegs][CreatureConstants.AnimatedObject_Colossal_TwoLegs] = GetMultiplierFromRange(64 * 12, 128 * 12);
            appearances[CreatureConstants.AnimatedObject_Colossal_TwoLegs_Wooden][GenderConstants.Agender] = GetBaseFromRange(64 * 12, 128 * 12);
            appearances[CreatureConstants.AnimatedObject_Colossal_TwoLegs_Wooden][CreatureConstants.AnimatedObject_Colossal_TwoLegs_Wooden] = GetMultiplierFromRange(64 * 12, 128 * 12);
            appearances[CreatureConstants.AnimatedObject_Colossal_Wheels_Wooden][GenderConstants.Agender] = GetBaseFromRange(64 * 12, 128 * 12);
            appearances[CreatureConstants.AnimatedObject_Colossal_Wheels_Wooden][CreatureConstants.AnimatedObject_Colossal_Wheels_Wooden] = GetMultiplierFromRange(64 * 12, 128 * 12);
            appearances[CreatureConstants.AnimatedObject_Colossal_Wooden][GenderConstants.Agender] = GetBaseFromRange(64 * 12, 128 * 12);
            appearances[CreatureConstants.AnimatedObject_Colossal_Wooden][CreatureConstants.AnimatedObject_Colossal_Wooden] = GetMultiplierFromRange(64 * 12, 128 * 12);
            appearances[CreatureConstants.AnimatedObject_Gargantuan][GenderConstants.Agender] = GetBaseFromRange(32 * 12, 64 * 12);
            appearances[CreatureConstants.AnimatedObject_Gargantuan][CreatureConstants.AnimatedObject_Gargantuan] = GetMultiplierFromRange(32 * 12, 64 * 12);
            appearances[CreatureConstants.AnimatedObject_Gargantuan_Flexible][GenderConstants.Agender] = "0";
            appearances[CreatureConstants.AnimatedObject_Gargantuan_Flexible][CreatureConstants.AnimatedObject_Gargantuan_Flexible] = "0";
            appearances[CreatureConstants.AnimatedObject_Gargantuan_MultipleLegs][GenderConstants.Agender] = GetBaseFromRange(32 * 12, 64 * 12);
            appearances[CreatureConstants.AnimatedObject_Gargantuan_MultipleLegs][CreatureConstants.AnimatedObject_Gargantuan_MultipleLegs] = GetMultiplierFromRange(32 * 12, 64 * 12);
            appearances[CreatureConstants.AnimatedObject_Gargantuan_MultipleLegs_Wooden][GenderConstants.Agender] = GetBaseFromRange(32 * 12, 64 * 12);
            appearances[CreatureConstants.AnimatedObject_Gargantuan_MultipleLegs_Wooden][CreatureConstants.AnimatedObject_Gargantuan_MultipleLegs_Wooden] = GetMultiplierFromRange(32 * 12, 64 * 12);
            appearances[CreatureConstants.AnimatedObject_Gargantuan_Sheetlike][GenderConstants.Agender] = "0";
            appearances[CreatureConstants.AnimatedObject_Gargantuan_Sheetlike][CreatureConstants.AnimatedObject_Gargantuan_Sheetlike] = "0";
            appearances[CreatureConstants.AnimatedObject_Gargantuan_TwoLegs][GenderConstants.Agender] = GetBaseFromRange(32 * 12, 64 * 12);
            appearances[CreatureConstants.AnimatedObject_Gargantuan_TwoLegs][CreatureConstants.AnimatedObject_Gargantuan_TwoLegs] = GetMultiplierFromRange(32 * 12, 64 * 12);
            appearances[CreatureConstants.AnimatedObject_Gargantuan_TwoLegs_Wooden][GenderConstants.Agender] = GetBaseFromRange(32 * 12, 64 * 12);
            appearances[CreatureConstants.AnimatedObject_Gargantuan_TwoLegs_Wooden][CreatureConstants.AnimatedObject_Gargantuan_TwoLegs_Wooden] = GetMultiplierFromRange(32 * 12, 64 * 12);
            appearances[CreatureConstants.AnimatedObject_Gargantuan_Wheels_Wooden][GenderConstants.Agender] = GetBaseFromRange(32 * 12, 64 * 12);
            appearances[CreatureConstants.AnimatedObject_Gargantuan_Wheels_Wooden][CreatureConstants.AnimatedObject_Gargantuan_Wheels_Wooden] = GetMultiplierFromRange(32 * 12, 64 * 12);
            appearances[CreatureConstants.AnimatedObject_Gargantuan_Wooden][GenderConstants.Agender] = GetBaseFromRange(32 * 12, 64 * 12);
            appearances[CreatureConstants.AnimatedObject_Gargantuan_Wooden][CreatureConstants.AnimatedObject_Gargantuan_Wooden] = GetMultiplierFromRange(32 * 12, 64 * 12);
            appearances[CreatureConstants.AnimatedObject_Huge][GenderConstants.Agender] = GetBaseFromRange(16 * 12, 32 * 12);
            appearances[CreatureConstants.AnimatedObject_Huge][CreatureConstants.AnimatedObject_Huge] = GetMultiplierFromRange(16 * 12, 32 * 12);
            appearances[CreatureConstants.AnimatedObject_Huge_Flexible][GenderConstants.Agender] = "0";
            appearances[CreatureConstants.AnimatedObject_Huge_Flexible][CreatureConstants.AnimatedObject_Huge_Flexible] = "0";
            appearances[CreatureConstants.AnimatedObject_Huge_MultipleLegs][GenderConstants.Agender] = GetBaseFromRange(16 * 12, 32 * 12);
            appearances[CreatureConstants.AnimatedObject_Huge_MultipleLegs][CreatureConstants.AnimatedObject_Huge_MultipleLegs] = GetMultiplierFromRange(16 * 12, 32 * 12);
            appearances[CreatureConstants.AnimatedObject_Huge_MultipleLegs_Wooden][GenderConstants.Agender] = GetBaseFromRange(16 * 12, 32 * 12);
            appearances[CreatureConstants.AnimatedObject_Huge_MultipleLegs_Wooden][CreatureConstants.AnimatedObject_Huge_MultipleLegs_Wooden] = GetMultiplierFromRange(16 * 12, 32 * 12);
            appearances[CreatureConstants.AnimatedObject_Huge_Sheetlike][GenderConstants.Agender] = "0";
            appearances[CreatureConstants.AnimatedObject_Huge_Sheetlike][CreatureConstants.AnimatedObject_Huge_Sheetlike] = "0";
            appearances[CreatureConstants.AnimatedObject_Huge_TwoLegs][GenderConstants.Agender] = GetBaseFromRange(16 * 12, 32 * 12);
            appearances[CreatureConstants.AnimatedObject_Huge_TwoLegs][CreatureConstants.AnimatedObject_Huge_TwoLegs] = GetMultiplierFromRange(16 * 12, 32 * 12);
            appearances[CreatureConstants.AnimatedObject_Huge_TwoLegs_Wooden][GenderConstants.Agender] = GetBaseFromRange(16 * 12, 32 * 12);
            appearances[CreatureConstants.AnimatedObject_Huge_TwoLegs_Wooden][CreatureConstants.AnimatedObject_Huge_TwoLegs_Wooden] = GetMultiplierFromRange(16 * 12, 32 * 12);
            appearances[CreatureConstants.AnimatedObject_Huge_Wheels_Wooden][GenderConstants.Agender] = GetBaseFromRange(16 * 12, 32 * 12);
            appearances[CreatureConstants.AnimatedObject_Huge_Wheels_Wooden][CreatureConstants.AnimatedObject_Huge_Wheels_Wooden] = GetMultiplierFromRange(16 * 12, 32 * 12);
            appearances[CreatureConstants.AnimatedObject_Huge_Wooden][GenderConstants.Agender] = GetBaseFromRange(16 * 12, 32 * 12);
            appearances[CreatureConstants.AnimatedObject_Huge_Wooden][CreatureConstants.AnimatedObject_Huge_Wooden] = GetMultiplierFromRange(16 * 12, 32 * 12);
            appearances[CreatureConstants.AnimatedObject_Large][GenderConstants.Agender] = GetBaseFromRange(8 * 12, 16 * 12);
            appearances[CreatureConstants.AnimatedObject_Large][CreatureConstants.AnimatedObject_Large] = GetMultiplierFromRange(8 * 12, 16 * 12);
            appearances[CreatureConstants.AnimatedObject_Large_Flexible][GenderConstants.Agender] = "0";
            appearances[CreatureConstants.AnimatedObject_Large_Flexible][CreatureConstants.AnimatedObject_Large_Flexible] = "0";
            appearances[CreatureConstants.AnimatedObject_Large_MultipleLegs][GenderConstants.Agender] = GetBaseFromRange(8 * 12, 16 * 12);
            appearances[CreatureConstants.AnimatedObject_Large_MultipleLegs][CreatureConstants.AnimatedObject_Large_MultipleLegs] = GetMultiplierFromRange(8 * 12, 16 * 12);
            appearances[CreatureConstants.AnimatedObject_Large_MultipleLegs_Wooden][GenderConstants.Agender] = GetBaseFromRange(8 * 12, 16 * 12);
            appearances[CreatureConstants.AnimatedObject_Large_MultipleLegs_Wooden][CreatureConstants.AnimatedObject_Large_MultipleLegs_Wooden] = GetMultiplierFromRange(8 * 12, 16 * 12);
            appearances[CreatureConstants.AnimatedObject_Large_Sheetlike][GenderConstants.Agender] = "0";
            appearances[CreatureConstants.AnimatedObject_Large_Sheetlike][CreatureConstants.AnimatedObject_Large_Sheetlike] = "0";
            appearances[CreatureConstants.AnimatedObject_Large_TwoLegs][GenderConstants.Agender] = GetBaseFromRange(8 * 12, 16 * 12);
            appearances[CreatureConstants.AnimatedObject_Large_TwoLegs][CreatureConstants.AnimatedObject_Large_TwoLegs] = GetMultiplierFromRange(8 * 12, 16 * 12);
            appearances[CreatureConstants.AnimatedObject_Large_TwoLegs_Wooden][GenderConstants.Agender] = GetBaseFromRange(8 * 12, 16 * 12);
            appearances[CreatureConstants.AnimatedObject_Large_TwoLegs_Wooden][CreatureConstants.AnimatedObject_Large_TwoLegs_Wooden] = GetMultiplierFromRange(8 * 12, 16 * 12);
            appearances[CreatureConstants.AnimatedObject_Large_Wheels_Wooden][GenderConstants.Agender] = GetBaseFromRange(8 * 12, 16 * 12);
            appearances[CreatureConstants.AnimatedObject_Large_Wheels_Wooden][CreatureConstants.AnimatedObject_Large_Wheels_Wooden] = GetMultiplierFromRange(8 * 12, 16 * 12);
            appearances[CreatureConstants.AnimatedObject_Large_Wooden][GenderConstants.Agender] = GetBaseFromRange(8 * 12, 16 * 12);
            appearances[CreatureConstants.AnimatedObject_Large_Wooden][CreatureConstants.AnimatedObject_Large_Wooden] = GetMultiplierFromRange(8 * 12, 16 * 12);
            appearances[CreatureConstants.AnimatedObject_Medium][GenderConstants.Agender] = GetBaseFromRange(4 * 12, 8 * 12);
            appearances[CreatureConstants.AnimatedObject_Medium][CreatureConstants.AnimatedObject_Medium] = GetMultiplierFromRange(4 * 12, 8 * 12);
            appearances[CreatureConstants.AnimatedObject_Medium_Flexible][GenderConstants.Agender] = "0";
            appearances[CreatureConstants.AnimatedObject_Medium_Flexible][CreatureConstants.AnimatedObject_Medium_Flexible] = "0";
            appearances[CreatureConstants.AnimatedObject_Medium_MultipleLegs][GenderConstants.Agender] = GetBaseFromRange(4 * 12, 8 * 12);
            appearances[CreatureConstants.AnimatedObject_Medium_MultipleLegs][CreatureConstants.AnimatedObject_Medium_MultipleLegs] = GetMultiplierFromRange(4 * 12, 8 * 12);
            appearances[CreatureConstants.AnimatedObject_Medium_MultipleLegs_Wooden][GenderConstants.Agender] = GetBaseFromRange(4 * 12, 8 * 12);
            appearances[CreatureConstants.AnimatedObject_Medium_MultipleLegs_Wooden][CreatureConstants.AnimatedObject_Medium_MultipleLegs_Wooden] = GetMultiplierFromRange(4 * 12, 8 * 12);
            appearances[CreatureConstants.AnimatedObject_Medium_Sheetlike][GenderConstants.Agender] = "0";
            appearances[CreatureConstants.AnimatedObject_Medium_Sheetlike][CreatureConstants.AnimatedObject_Medium_Sheetlike] = "0";
            appearances[CreatureConstants.AnimatedObject_Medium_TwoLegs][GenderConstants.Agender] = GetBaseFromRange(4 * 12, 8 * 12);
            appearances[CreatureConstants.AnimatedObject_Medium_TwoLegs][CreatureConstants.AnimatedObject_Medium_TwoLegs] = GetMultiplierFromRange(4 * 12, 8 * 12);
            appearances[CreatureConstants.AnimatedObject_Medium_TwoLegs_Wooden][GenderConstants.Agender] = GetBaseFromRange(4 * 12, 8 * 12);
            appearances[CreatureConstants.AnimatedObject_Medium_TwoLegs_Wooden][CreatureConstants.AnimatedObject_Medium_TwoLegs_Wooden] = GetMultiplierFromRange(4 * 12, 8 * 12);
            appearances[CreatureConstants.AnimatedObject_Medium_Wheels_Wooden][GenderConstants.Agender] = GetBaseFromRange(4 * 12, 8 * 12);
            appearances[CreatureConstants.AnimatedObject_Medium_Wheels_Wooden][CreatureConstants.AnimatedObject_Medium_Wheels_Wooden] = GetMultiplierFromRange(4 * 12, 8 * 12);
            appearances[CreatureConstants.AnimatedObject_Medium_Wooden][GenderConstants.Agender] = GetBaseFromRange(4 * 12, 8 * 12);
            appearances[CreatureConstants.AnimatedObject_Medium_Wooden][CreatureConstants.AnimatedObject_Medium_Wooden] = GetMultiplierFromRange(4 * 12, 8 * 12);
            appearances[CreatureConstants.AnimatedObject_Small][GenderConstants.Agender] = GetBaseFromRange(2 * 12, 4 * 12);
            appearances[CreatureConstants.AnimatedObject_Small][CreatureConstants.AnimatedObject_Small] = GetMultiplierFromRange(2 * 12, 4 * 12);
            appearances[CreatureConstants.AnimatedObject_Small_Flexible][GenderConstants.Agender] = "0";
            appearances[CreatureConstants.AnimatedObject_Small_Flexible][CreatureConstants.AnimatedObject_Small_Flexible] = "0";
            appearances[CreatureConstants.AnimatedObject_Small_MultipleLegs][GenderConstants.Agender] = GetBaseFromRange(2 * 12, 4 * 12);
            appearances[CreatureConstants.AnimatedObject_Small_MultipleLegs][CreatureConstants.AnimatedObject_Small_MultipleLegs] = GetMultiplierFromRange(2 * 12, 4 * 12);
            appearances[CreatureConstants.AnimatedObject_Small_MultipleLegs_Wooden][GenderConstants.Agender] = GetBaseFromRange(2 * 12, 4 * 12);
            appearances[CreatureConstants.AnimatedObject_Small_MultipleLegs_Wooden][CreatureConstants.AnimatedObject_Small_MultipleLegs_Wooden] = GetMultiplierFromRange(2 * 12, 4 * 12);
            appearances[CreatureConstants.AnimatedObject_Small_Sheetlike][GenderConstants.Agender] = "0";
            appearances[CreatureConstants.AnimatedObject_Small_Sheetlike][CreatureConstants.AnimatedObject_Small_Sheetlike] = "0";
            appearances[CreatureConstants.AnimatedObject_Small_TwoLegs][GenderConstants.Agender] = GetBaseFromRange(2 * 12, 4 * 12);
            appearances[CreatureConstants.AnimatedObject_Small_TwoLegs][CreatureConstants.AnimatedObject_Small_TwoLegs] = GetMultiplierFromRange(2 * 12, 4 * 12);
            appearances[CreatureConstants.AnimatedObject_Small_TwoLegs_Wooden][GenderConstants.Agender] = GetBaseFromRange(2 * 12, 4 * 12);
            appearances[CreatureConstants.AnimatedObject_Small_TwoLegs_Wooden][CreatureConstants.AnimatedObject_Small_TwoLegs_Wooden] = GetMultiplierFromRange(2 * 12, 4 * 12);
            appearances[CreatureConstants.AnimatedObject_Small_Wheels_Wooden][GenderConstants.Agender] = GetBaseFromRange(2 * 12, 4 * 12);
            appearances[CreatureConstants.AnimatedObject_Small_Wheels_Wooden][CreatureConstants.AnimatedObject_Small_Wheels_Wooden] = GetMultiplierFromRange(2 * 12, 4 * 12);
            appearances[CreatureConstants.AnimatedObject_Small_Wooden][GenderConstants.Agender] = GetBaseFromRange(2 * 12, 4 * 12);
            appearances[CreatureConstants.AnimatedObject_Small_Wooden][CreatureConstants.AnimatedObject_Small_Wooden] = GetMultiplierFromRange(2 * 12, 4 * 12);
            appearances[CreatureConstants.AnimatedObject_Tiny][GenderConstants.Agender] = GetBaseFromRange(12, 24);
            appearances[CreatureConstants.AnimatedObject_Tiny][CreatureConstants.AnimatedObject_Tiny] = GetMultiplierFromRange(12, 24);
            appearances[CreatureConstants.AnimatedObject_Tiny_Flexible][GenderConstants.Agender] = "0";
            appearances[CreatureConstants.AnimatedObject_Tiny_Flexible][CreatureConstants.AnimatedObject_Tiny_Flexible] = "0";
            appearances[CreatureConstants.AnimatedObject_Tiny_MultipleLegs][GenderConstants.Agender] = GetBaseFromRange(12, 24);
            appearances[CreatureConstants.AnimatedObject_Tiny_MultipleLegs][CreatureConstants.AnimatedObject_Tiny_MultipleLegs] = GetMultiplierFromRange(12, 24);
            appearances[CreatureConstants.AnimatedObject_Tiny_MultipleLegs_Wooden][GenderConstants.Agender] = GetBaseFromRange(12, 24);
            appearances[CreatureConstants.AnimatedObject_Tiny_MultipleLegs_Wooden][CreatureConstants.AnimatedObject_Tiny_MultipleLegs_Wooden] = GetMultiplierFromRange(12, 24);
            appearances[CreatureConstants.AnimatedObject_Tiny_Sheetlike][GenderConstants.Agender] = "0";
            appearances[CreatureConstants.AnimatedObject_Tiny_Sheetlike][CreatureConstants.AnimatedObject_Tiny_Sheetlike] = "0";
            appearances[CreatureConstants.AnimatedObject_Tiny_TwoLegs][GenderConstants.Agender] = GetBaseFromRange(12, 24);
            appearances[CreatureConstants.AnimatedObject_Tiny_TwoLegs][CreatureConstants.AnimatedObject_Tiny_TwoLegs] = GetMultiplierFromRange(12, 24);
            appearances[CreatureConstants.AnimatedObject_Tiny_TwoLegs_Wooden][GenderConstants.Agender] = GetBaseFromRange(12, 24);
            appearances[CreatureConstants.AnimatedObject_Tiny_TwoLegs_Wooden][CreatureConstants.AnimatedObject_Tiny_TwoLegs_Wooden] = GetMultiplierFromRange(12, 24);
            appearances[CreatureConstants.AnimatedObject_Tiny_Wheels_Wooden][GenderConstants.Agender] = GetBaseFromRange(12, 24);
            appearances[CreatureConstants.AnimatedObject_Tiny_Wheels_Wooden][CreatureConstants.AnimatedObject_Tiny_Wheels_Wooden] = GetMultiplierFromRange(12, 24);
            appearances[CreatureConstants.AnimatedObject_Tiny_Wooden][GenderConstants.Agender] = GetBaseFromRange(12, 24);
            appearances[CreatureConstants.AnimatedObject_Tiny_Wooden][CreatureConstants.AnimatedObject_Tiny_Wooden] = GetMultiplierFromRange(12, 24);
            //Source: https://www.d20srd.org/srd/monsters/ankheg.htm
            appearances[CreatureConstants.Ankheg][GenderConstants.Female] = "0";
            appearances[CreatureConstants.Ankheg][GenderConstants.Male] = "0";
            appearances[CreatureConstants.Ankheg][CreatureConstants.Ankheg] = "0";
            //Source: https://www.d20srd.org/srd/monsters/hag.htm#annis
            appearances[CreatureConstants.Annis][GenderConstants.Female] = GetBaseFromAverage(8 * 12);
            appearances[CreatureConstants.Annis][CreatureConstants.Annis] = GetMultiplierFromAverage(8 * 12);
            //Source: https://www.d20srd.org/srd/monsters/giantAnt.htm
            //https://www.dimensions.com/element/black-garden-ant-lasius-niger - scale up, [.035,.05]*6*12/[.14,.2] = [18,18]
            appearances[CreatureConstants.Ant_Giant_Worker][GenderConstants.Male] = GetBaseFromAverage(18);
            appearances[CreatureConstants.Ant_Giant_Worker][CreatureConstants.Ant_Giant_Worker] = GetMultiplierFromAverage(18);
            appearances[CreatureConstants.Ant_Giant_Soldier][GenderConstants.Male] = GetBaseFromAverage(18);
            appearances[CreatureConstants.Ant_Giant_Soldier][CreatureConstants.Ant_Giant_Soldier] = GetMultiplierFromAverage(18);
            //https://www.dimensions.com/element/black-garden-ant-lasius-niger - scale up, [.035,.05]*9*12/[.31,.35] = [12,16]
            appearances[CreatureConstants.Ant_Giant_Queen][GenderConstants.Female] = GetBaseFromRange(12, 16);
            appearances[CreatureConstants.Ant_Giant_Queen][CreatureConstants.Ant_Giant_Queen] = GetMultiplierFromRange(12, 16);
            //Source: https://www.dimensions.com/element/eastern-lowland-gorilla-gorilla-beringei-graueri (using for female)
            //https://www.d20srd.org/srd/monsters/ape.htm (male)
            //Adjusting female max -3" to match range
            appearances[CreatureConstants.Ape][GenderConstants.Female] = GetBaseFromRange(63, 72 - 3);
            appearances[CreatureConstants.Ape][GenderConstants.Male] = GetBaseFromRange(5 * 12 + 6, 6 * 12);
            appearances[CreatureConstants.Ape][CreatureConstants.Ape] = GetMultiplierFromRange(5 * 12 + 6, 6 * 12);
            //Source: https://www.d20srd.org/srd/monsters/direApe.htm
            appearances[CreatureConstants.Ape_Dire][GenderConstants.Female] = GetBaseFromAverage(9 * 12);
            appearances[CreatureConstants.Ape_Dire][GenderConstants.Male] = GetBaseFromAverage(9 * 12);
            appearances[CreatureConstants.Ape_Dire][CreatureConstants.Ape_Dire] = GetMultiplierFromAverage(9 * 12);
            //INFO: Based on Half-Elf, since could be Human, Half-Elf, or Drow
            appearances[CreatureConstants.Aranea][GenderConstants.Female] = "4*12+5";
            appearances[CreatureConstants.Aranea][GenderConstants.Male] = "4*12+7";
            appearances[CreatureConstants.Aranea][CreatureConstants.Aranea] = "2d8";
            //Source: https://www.d20srd.org/srd/monsters/arrowhawk.htm
            appearances[CreatureConstants.Arrowhawk_Juvenile][GenderConstants.Female] = "0";
            appearances[CreatureConstants.Arrowhawk_Juvenile][GenderConstants.Male] = "0";
            appearances[CreatureConstants.Arrowhawk_Juvenile][CreatureConstants.Arrowhawk_Juvenile] = "0";
            appearances[CreatureConstants.Arrowhawk_Adult][GenderConstants.Female] = "0";
            appearances[CreatureConstants.Arrowhawk_Adult][GenderConstants.Male] = "0";
            appearances[CreatureConstants.Arrowhawk_Adult][CreatureConstants.Arrowhawk_Adult] = "0";
            appearances[CreatureConstants.Arrowhawk_Elder][GenderConstants.Female] = "0";
            appearances[CreatureConstants.Arrowhawk_Elder][GenderConstants.Male] = "0";
            appearances[CreatureConstants.Arrowhawk_Elder][CreatureConstants.Arrowhawk_Elder] = "0";
            //Source: https://forgottenrealms.fandom.com/wiki/Assassin_vine
            appearances[CreatureConstants.AssassinVine][GenderConstants.Agender] = "0";
            appearances[CreatureConstants.AssassinVine][CreatureConstants.AssassinVine] = "0";
            //Source: https://www.d20srd.org/srd/monsters/athach.htm
            appearances[CreatureConstants.Athach][GenderConstants.Female] = GetBaseFromAverage(18 * 12);
            appearances[CreatureConstants.Athach][GenderConstants.Male] = GetBaseFromAverage(18 * 12);
            appearances[CreatureConstants.Athach][CreatureConstants.Athach] = GetMultiplierFromAverage(18 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Avoral
            appearances[CreatureConstants.Avoral][GenderConstants.Female] = GetBaseFromRange(6 * 12 + 6, 7 * 12);
            appearances[CreatureConstants.Avoral][GenderConstants.Male] = GetBaseFromRange(6 * 12 + 6, 7 * 12);
            appearances[CreatureConstants.Avoral][GenderConstants.Agender] = GetBaseFromRange(6 * 12 + 6, 7 * 12);
            appearances[CreatureConstants.Avoral][CreatureConstants.Avoral] = GetMultiplierFromRange(6 * 12 + 6, 7 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Azer
            appearances[CreatureConstants.Azer][GenderConstants.Male] = GetBaseFromAverage(5 * 12);
            appearances[CreatureConstants.Azer][GenderConstants.Female] = GetBaseFromAverage(5 * 12);
            appearances[CreatureConstants.Azer][GenderConstants.Agender] = GetBaseFromAverage(5 * 12);
            appearances[CreatureConstants.Azer][CreatureConstants.Azer] = GetMultiplierFromAverage(5 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Babau
            appearances[CreatureConstants.Babau][GenderConstants.Agender] = GetBaseFromRange(6 * 12, 7 * 12);
            appearances[CreatureConstants.Babau][CreatureConstants.Babau] = GetMultiplierFromRange(6 * 12, 7 * 12);
            //Source: https://www.dimensions.com/element/mandrill-mandrillus-sphinx
            appearances[CreatureConstants.Baboon][GenderConstants.Female] = GetBaseFromRange(20, 36);
            appearances[CreatureConstants.Baboon][GenderConstants.Male] = GetBaseFromRange(20, 36);
            appearances[CreatureConstants.Baboon][CreatureConstants.Baboon] = GetMultiplierFromRange(20, 36);
            //Source: https://www.dimensions.com/element/honey-badger-mellivora-capensis
            appearances[CreatureConstants.Badger][GenderConstants.Female] = GetBaseFromRange(11, 16);
            appearances[CreatureConstants.Badger][GenderConstants.Male] = GetBaseFromRange(11, 16);
            appearances[CreatureConstants.Badger][CreatureConstants.Badger] = GetMultiplierFromRange(11, 16);
            //Multiplying up from normal badger. Length is about x2 from normal low, 2.5x for high
            appearances[CreatureConstants.Badger_Dire][GenderConstants.Female] = GetBaseFromRange(22, 40);
            appearances[CreatureConstants.Badger_Dire][GenderConstants.Male] = GetBaseFromRange(22, 40);
            appearances[CreatureConstants.Badger_Dire][CreatureConstants.Badger_Dire] = GetMultiplierFromRange(22, 40);
            //Source: https://forgottenrealms.fandom.com/wiki/Balor
            appearances[CreatureConstants.Balor][GenderConstants.Agender] = GetBaseFromAverage(12 * 12);
            appearances[CreatureConstants.Balor][CreatureConstants.Balor] = GetMultiplierFromAverage(12 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Hamatula
            appearances[CreatureConstants.BarbedDevil_Hamatula][GenderConstants.Agender] = GetBaseFromAverage(7 * 12);
            appearances[CreatureConstants.BarbedDevil_Hamatula][CreatureConstants.BarbedDevil_Hamatula] = GetMultiplierFromAverage(7 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Barghest
            appearances[CreatureConstants.Barghest][GenderConstants.Female] = GetBaseFromRange(5 * 12, 7 * 12);
            appearances[CreatureConstants.Barghest][GenderConstants.Male] = GetBaseFromRange(5 * 12, 7 * 12);
            appearances[CreatureConstants.Barghest][CreatureConstants.Barghest] = GetMultiplierFromRange(5 * 12, 7 * 12);
            appearances[CreatureConstants.Barghest_Greater][GenderConstants.Female] = GetBaseFromRange(7 * 12, 9 * 12);
            appearances[CreatureConstants.Barghest_Greater][GenderConstants.Male] = GetBaseFromRange(7 * 12, 9 * 12);
            appearances[CreatureConstants.Barghest_Greater][CreatureConstants.Barghest_Greater] = GetMultiplierFromRange(7 * 12, 9 * 12);
            appearances[CreatureConstants.Basilisk][GenderConstants.Female] = "0";
            appearances[CreatureConstants.Basilisk][GenderConstants.Male] = "0";
            appearances[CreatureConstants.Basilisk][CreatureConstants.Basilisk] = "0";
            appearances[CreatureConstants.Basilisk_Greater][GenderConstants.Female] = "0";
            appearances[CreatureConstants.Basilisk_Greater][GenderConstants.Male] = "0";
            appearances[CreatureConstants.Basilisk_Greater][CreatureConstants.Basilisk_Greater] = "0";
            //Source: https://www.dimensions.com/element/little-brown-bat-myotis-lucifugus hanging height
            appearances[CreatureConstants.Bat][GenderConstants.Female] = GetBaseFromRange(4, 5);
            appearances[CreatureConstants.Bat][GenderConstants.Male] = GetBaseFromRange(4, 5);
            appearances[CreatureConstants.Bat][CreatureConstants.Bat] = GetMultiplierFromRange(4, 5);
            //Scaled up from bat, x15 based on wingspan
            appearances[CreatureConstants.Bat_Dire][GenderConstants.Female] = GetBaseFromRange(60, 75);
            appearances[CreatureConstants.Bat_Dire][GenderConstants.Male] = GetBaseFromRange(60, 75);
            appearances[CreatureConstants.Bat_Dire][CreatureConstants.Bat_Dire] = GetMultiplierFromRange(60, 75);
            //Source: https://www.d20srd.org/srd/monsters/swarm.htm
            appearances[CreatureConstants.Bat_Swarm][GenderConstants.Agender] = GetBaseFromAverage(10 * 12);
            appearances[CreatureConstants.Bat_Swarm][CreatureConstants.Bat_Swarm] = GetMultiplierFromAverage(10 * 12);
            //Source: https://www.dimensions.com/element/american-black-bear shoulder height
            //Adjusting upper female -1" to match range
            appearances[CreatureConstants.Bear_Black][GenderConstants.Female] = GetBaseFromRange(2 * 12 + 6, 3 * 12 + 1 - 1);
            appearances[CreatureConstants.Bear_Black][GenderConstants.Male] = GetBaseFromRange(2 * 12 + 11, 3 * 12 + 5);
            appearances[CreatureConstants.Bear_Black][CreatureConstants.Bear_Black] = GetMultiplierFromRange(2 * 12 + 11, 3 * 12 + 5);
            //Source: https://www.dimensions.com/element/grizzly-bear shoulder height
            //Adjusting upper female +4" to match range
            appearances[CreatureConstants.Bear_Brown][GenderConstants.Female] = GetBaseFromRange(3 * 12, 3 * 12 + 8 + 4);
            appearances[CreatureConstants.Bear_Brown][GenderConstants.Male] = GetBaseFromRange(3 * 12 + 6, 4 * 12 + 6);
            appearances[CreatureConstants.Bear_Brown][CreatureConstants.Bear_Brown] = GetMultiplierFromRange(3 * 12 + 6, 4 * 12 + 6);
            //Source: Scaled up from grizzly, x1.5
            appearances[CreatureConstants.Bear_Dire][GenderConstants.Female] = GetBaseFromRange(63, 81);
            appearances[CreatureConstants.Bear_Dire][GenderConstants.Male] = GetBaseFromRange(63, 81);
            appearances[CreatureConstants.Bear_Dire][CreatureConstants.Bear_Dire] = GetMultiplierFromRange(63, 81);
            //Source: https://www.dimensions.com/element/polar-bears shoulder height
            //Adjusting female max +5" to match range
            appearances[CreatureConstants.Bear_Polar][GenderConstants.Female] = GetBaseFromRange(2 * 12 + 8, 3 * 12 + 11 + 5);
            appearances[CreatureConstants.Bear_Polar][GenderConstants.Male] = GetBaseFromRange(3 * 12 + 7, 5 * 12 + 3);
            appearances[CreatureConstants.Bear_Polar][CreatureConstants.Bear_Polar] = GetMultiplierFromRange(3 * 12 + 7, 5 * 12 + 3);
            //Source: https://forgottenrealms.fandom.com/wiki/Barbazu
            appearances[CreatureConstants.BeardedDevil_Barbazu][GenderConstants.Agender] = GetBaseFromAverage(6 * 12);
            appearances[CreatureConstants.BeardedDevil_Barbazu][CreatureConstants.BeardedDevil_Barbazu] = GetMultiplierFromAverage(6 * 12);
            appearances[CreatureConstants.Bebilith][GenderConstants.Agender] = "0";
            appearances[CreatureConstants.Bebilith][CreatureConstants.Bebilith] = "0";
            //Source: https://www.d20srd.org/srd/monsters/giantBee.htm
            //https://www.dimensions.com/element/western-honey-bee-apis-mellifera scale up, [.12,.2]*5*12/[.39,.59] = [18,21]
            appearances[CreatureConstants.Bee_Giant][GenderConstants.Male] = GetBaseFromRange(18, 21);
            appearances[CreatureConstants.Bee_Giant][CreatureConstants.Bee_Giant] = GetMultiplierFromRange(18, 21);
            //Source: https://forgottenrealms.fandom.com/wiki/Behir
            appearances[CreatureConstants.Behir][GenderConstants.Female] = "0";
            appearances[CreatureConstants.Behir][GenderConstants.Male] = "0";
            appearances[CreatureConstants.Behir][CreatureConstants.Behir] = "0";
            //Source: https://forgottenrealms.fandom.com/wiki/Beholder
            appearances[CreatureConstants.Beholder][GenderConstants.Agender] = GetBaseFromAverage(8 * 12);
            appearances[CreatureConstants.Beholder][CreatureConstants.Beholder] = GetMultiplierFromAverage(8 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Gauth
            appearances[CreatureConstants.Beholder_Gauth][GenderConstants.Agender] = GetBaseFromRange(4 * 12, 6 * 12);
            appearances[CreatureConstants.Beholder_Gauth][CreatureConstants.Beholder_Gauth] = GetMultiplierFromRange(4 * 12, 6 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Belker
            appearances[CreatureConstants.Belker][GenderConstants.Agender] = GetBaseFromRange(7 * 12, 9 * 12);
            appearances[CreatureConstants.Belker][CreatureConstants.Belker] = GetMultiplierFromRange(7 * 12, 9 * 12);
            //Source: https://www.dimensions.com/element/american-bison-bison-bison, withers height
            appearances[CreatureConstants.Bison][GenderConstants.Female] = GetBaseFromRange(60, 78);
            appearances[CreatureConstants.Bison][GenderConstants.Male] = GetBaseFromRange(60, 78);
            appearances[CreatureConstants.Bison][CreatureConstants.Bison] = GetMultiplierFromRange(60, 78);
            //Source: https://forgottenrealms.fandom.com/wiki/Black_pudding
            appearances[CreatureConstants.BlackPudding][GenderConstants.Agender] = GetBaseFromAverage(2 * 12);
            appearances[CreatureConstants.BlackPudding][CreatureConstants.BlackPudding] = GetMultiplierFromAverage(2 * 12);
            //Elder is a size category up, so double dimensions
            appearances[CreatureConstants.BlackPudding_Elder][GenderConstants.Agender] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.BlackPudding_Elder][CreatureConstants.BlackPudding_Elder] = GetMultiplierFromAverage(4 * 12);
            //Source: https://www.d20pfsrd.com/bestiary/monster-listings/magical-beasts/blink-dog
            appearances[CreatureConstants.BlinkDog][GenderConstants.Female] = GetBaseFromAverage(3 * 12);
            appearances[CreatureConstants.BlinkDog][GenderConstants.Male] = GetBaseFromAverage(3 * 12);
            appearances[CreatureConstants.BlinkDog][CreatureConstants.BlinkDog] = GetMultiplierFromAverage(3 * 12);
            //Source: https://www.dimensions.com/element/wild-boar
            appearances[CreatureConstants.Boar][GenderConstants.Female] = GetBaseFromRange(2 * 12 + 6, 3 * 12);
            appearances[CreatureConstants.Boar][GenderConstants.Male] = GetBaseFromRange(2 * 12 + 6, 3 * 12);
            appearances[CreatureConstants.Boar][CreatureConstants.Boar] = GetMultiplierFromRange(2 * 12 + 6, 3 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Dire_boar
            appearances[CreatureConstants.Boar_Dire][GenderConstants.Female] = GetBaseFromAverage(6 * 12);
            appearances[CreatureConstants.Boar_Dire][GenderConstants.Male] = GetBaseFromAverage(6 * 12);
            appearances[CreatureConstants.Boar_Dire][CreatureConstants.Boar_Dire] = GetMultiplierFromAverage(6 * 12);
            //INFO: Basing off of humans
            appearances[CreatureConstants.Bodak][GenderConstants.Female] = "4*12+5";
            appearances[CreatureConstants.Bodak][GenderConstants.Male] = "4*12+10";
            appearances[CreatureConstants.Bodak][CreatureConstants.Bodak] = "2d10";
            //Source: https://web.stanford.edu/~cbross/bombbeetle.html
            appearances[CreatureConstants.BombardierBeetle_Giant][GenderConstants.Female] = "0";
            appearances[CreatureConstants.BombardierBeetle_Giant][GenderConstants.Male] = "0";
            appearances[CreatureConstants.BombardierBeetle_Giant][CreatureConstants.BombardierBeetle_Giant] = "0";
            //Source: https://forgottenrealms.fandom.com/wiki/Osyluth
            appearances[CreatureConstants.BoneDevil_Osyluth][GenderConstants.Agender] = GetBaseFromRange(9 * 12, 9 * 12 + 6);
            appearances[CreatureConstants.BoneDevil_Osyluth][CreatureConstants.BoneDevil_Osyluth] = GetMultiplierFromRange(9 * 12, 9 * 12 + 6);
            //Source: https://forgottenrealms.fandom.com/wiki/Bralani
            appearances[CreatureConstants.Bralani][GenderConstants.Female] = GetBaseFromAverage(5 * 12);
            appearances[CreatureConstants.Bralani][GenderConstants.Male] = GetBaseFromAverage(5 * 12);
            appearances[CreatureConstants.Bralani][CreatureConstants.Bralani] = GetMultiplierFromAverage(5 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Bugbear
            appearances[CreatureConstants.Bugbear][GenderConstants.Female] = GetBaseFromRange(6 * 12, 8 * 12);
            appearances[CreatureConstants.Bugbear][GenderConstants.Male] = GetBaseFromRange(6 * 12, 8 * 12);
            appearances[CreatureConstants.Bugbear][CreatureConstants.Bugbear] = GetMultiplierFromRange(6 * 12, 8 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Bulette
            appearances[CreatureConstants.Bulette][GenderConstants.Female] = GetBaseFromAverage(9 * 12);
            appearances[CreatureConstants.Bulette][GenderConstants.Male] = GetBaseFromAverage(9 * 12);
            appearances[CreatureConstants.Bulette][CreatureConstants.Bulette] = GetMultiplierFromAverage(9 * 12);
            //Source: https://www.dimensions.com/element/bactrian-camel
            appearances[CreatureConstants.Camel_Bactrian][GenderConstants.Female] = GetBaseFromRange(62, 71);
            appearances[CreatureConstants.Camel_Bactrian][GenderConstants.Male] = GetBaseFromRange(62, 71);
            appearances[CreatureConstants.Camel_Bactrian][CreatureConstants.Camel_Bactrian] = GetMultiplierFromRange(62, 71);
            //Source: https://www.dimensions.com/element/dromedary-camel
            appearances[CreatureConstants.Camel_Dromedary][GenderConstants.Female] = GetBaseFromRange(71, 78);
            appearances[CreatureConstants.Camel_Dromedary][GenderConstants.Male] = GetBaseFromRange(71, 78);
            appearances[CreatureConstants.Camel_Dromedary][CreatureConstants.Camel_Dromedary] = GetMultiplierFromRange(71, 78);
            //Source: https://forgottenrealms.fandom.com/wiki/Carrion_crawler
            appearances[CreatureConstants.CarrionCrawler][GenderConstants.Female] = "0";
            appearances[CreatureConstants.CarrionCrawler][GenderConstants.Male] = "0";
            appearances[CreatureConstants.CarrionCrawler][CreatureConstants.CarrionCrawler] = "0";
            //Source: https://www.dimensions.com/element/american-shorthair-cat
            appearances[CreatureConstants.Cat][GenderConstants.Female] = GetBaseFromRange(8, 10);
            appearances[CreatureConstants.Cat][GenderConstants.Male] = GetBaseFromRange(8, 10);
            appearances[CreatureConstants.Cat][CreatureConstants.Cat] = GetMultiplierFromRange(8, 10);
            //Source: https://forgottenrealms.fandom.com/wiki/Centaur
            appearances[CreatureConstants.Centaur][GenderConstants.Female] = GetBaseFromRange(7 * 12, 9 * 12);
            appearances[CreatureConstants.Centaur][GenderConstants.Male] = GetBaseFromRange(7 * 12, 9 * 12);
            appearances[CreatureConstants.Centaur][CreatureConstants.Centaur] = GetMultiplierFromRange(7 * 12, 9 * 12);
            //Source: https://www.realmshelps.net/monsters/block/Monstrous_Centipede,_Tiny
            appearances[CreatureConstants.Centipede_Monstrous_Tiny][GenderConstants.Female] = GetBaseFromRange(1, 2);
            appearances[CreatureConstants.Centipede_Monstrous_Tiny][GenderConstants.Male] = GetBaseFromRange(1, 2);
            appearances[CreatureConstants.Centipede_Monstrous_Tiny][CreatureConstants.Centipede_Monstrous_Tiny] = GetMultiplierFromRange(1, 2);
            //Source: https://www.realmshelps.net/monsters/block/Monstrous_Centipede,_Small
            appearances[CreatureConstants.Centipede_Monstrous_Small][GenderConstants.Female] = GetBaseFromAverage(3);
            appearances[CreatureConstants.Centipede_Monstrous_Small][GenderConstants.Male] = GetBaseFromAverage(3);
            appearances[CreatureConstants.Centipede_Monstrous_Small][CreatureConstants.Centipede_Monstrous_Small] = GetMultiplierFromAverage(3);
            //Source: https://www.realmshelps.net/monsters/block/Monstrous_Centipede,_Medium
            appearances[CreatureConstants.Centipede_Monstrous_Medium][GenderConstants.Female] = GetBaseFromAverage(6);
            appearances[CreatureConstants.Centipede_Monstrous_Medium][GenderConstants.Male] = GetBaseFromAverage(6);
            appearances[CreatureConstants.Centipede_Monstrous_Medium][CreatureConstants.Centipede_Monstrous_Medium] = GetMultiplierFromAverage(6);
            //Source: https://www.realmshelps.net/monsters/block/Monstrous_Centipede,_Large
            appearances[CreatureConstants.Centipede_Monstrous_Large][GenderConstants.Female] = GetBaseFromAverage(12);
            appearances[CreatureConstants.Centipede_Monstrous_Large][GenderConstants.Male] = GetBaseFromAverage(12);
            appearances[CreatureConstants.Centipede_Monstrous_Large][CreatureConstants.Centipede_Monstrous_Large] = GetMultiplierFromAverage(12);
            //Source: https://www.realmshelps.net/monsters/block/Monstrous_Centipede,_Huge
            appearances[CreatureConstants.Centipede_Monstrous_Huge][GenderConstants.Female] = GetBaseFromAverage(2 * 12);
            appearances[CreatureConstants.Centipede_Monstrous_Huge][GenderConstants.Male] = GetBaseFromAverage(2 * 12);
            appearances[CreatureConstants.Centipede_Monstrous_Huge][CreatureConstants.Centipede_Monstrous_Huge] = GetMultiplierFromAverage(2 * 12);
            //Source: https://www.realmshelps.net/monsters/block/Monstrous_Centipede,_Gargantuan
            appearances[CreatureConstants.Centipede_Monstrous_Gargantuan][GenderConstants.Female] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.Centipede_Monstrous_Gargantuan][GenderConstants.Male] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.Centipede_Monstrous_Gargantuan][CreatureConstants.Centipede_Monstrous_Gargantuan] = GetMultiplierFromAverage(4 * 12);
            //Source: https://www.realmshelps.net/monsters/block/Monstrous_Centipede,_Colossal
            appearances[CreatureConstants.Centipede_Monstrous_Colossal][GenderConstants.Female] = GetBaseFromAverage(8 * 12);
            appearances[CreatureConstants.Centipede_Monstrous_Colossal][GenderConstants.Male] = GetBaseFromAverage(8 * 12);
            appearances[CreatureConstants.Centipede_Monstrous_Colossal][CreatureConstants.Centipede_Monstrous_Colossal] = GetMultiplierFromAverage(8 * 12);
            //Source: https://www.d20srd.org/srd/monsters/swarm.htm
            appearances[CreatureConstants.Centipede_Swarm][GenderConstants.Agender] = "0";
            appearances[CreatureConstants.Centipede_Swarm][CreatureConstants.Centipede_Swarm] = "0";
            //Source: https://www.d20srd.org/srd/monsters/devil.htm#chainDevilKyton
            appearances[CreatureConstants.ChainDevil_Kyton][GenderConstants.Agender] = GetBaseFromAverage(6 * 12);
            appearances[CreatureConstants.ChainDevil_Kyton][GenderConstants.Female] = GetBaseFromAverage(6 * 12);
            appearances[CreatureConstants.ChainDevil_Kyton][GenderConstants.Male] = GetBaseFromAverage(6 * 12);
            appearances[CreatureConstants.ChainDevil_Kyton][CreatureConstants.ChainDevil_Kyton] = GetMultiplierFromAverage(6 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Chaos_beast
            appearances[CreatureConstants.ChaosBeast][GenderConstants.Agender] = GetBaseFromRange(5 * 12, 7 * 12);
            appearances[CreatureConstants.ChaosBeast][CreatureConstants.ChaosBeast] = GetMultiplierFromRange(5 * 12, 7 * 12);
            //Source: https://www.dimensions.com/element/cheetahs
            appearances[CreatureConstants.Cheetah][GenderConstants.Female] = GetBaseFromRange(28, 35);
            appearances[CreatureConstants.Cheetah][GenderConstants.Male] = GetBaseFromRange(28, 35);
            appearances[CreatureConstants.Cheetah][CreatureConstants.Cheetah] = GetMultiplierFromRange(28, 35);
            //Source: https://forgottenrealms.fandom.com/wiki/Chimera
            appearances[CreatureConstants.Chimera_Black][GenderConstants.Female] = GetBaseFromAverage(5 * 12);
            appearances[CreatureConstants.Chimera_Black][GenderConstants.Male] = GetBaseFromAverage(5 * 12);
            appearances[CreatureConstants.Chimera_Black][CreatureConstants.Chimera_Black] = GetMultiplierFromAverage(5 * 12);
            appearances[CreatureConstants.Chimera_Blue][GenderConstants.Female] = GetBaseFromAverage(5 * 12);
            appearances[CreatureConstants.Chimera_Blue][GenderConstants.Male] = GetBaseFromAverage(5 * 12);
            appearances[CreatureConstants.Chimera_Blue][CreatureConstants.Chimera_Blue] = GetMultiplierFromAverage(5 * 12);
            appearances[CreatureConstants.Chimera_Green][GenderConstants.Female] = GetBaseFromAverage(5 * 12);
            appearances[CreatureConstants.Chimera_Green][GenderConstants.Male] = GetBaseFromAverage(5 * 12);
            appearances[CreatureConstants.Chimera_Green][CreatureConstants.Chimera_Green] = GetMultiplierFromAverage(5 * 12);
            appearances[CreatureConstants.Chimera_Red][GenderConstants.Female] = GetBaseFromAverage(5 * 12);
            appearances[CreatureConstants.Chimera_Red][GenderConstants.Male] = GetBaseFromAverage(5 * 12);
            appearances[CreatureConstants.Chimera_Red][CreatureConstants.Chimera_Red] = GetMultiplierFromAverage(5 * 12);
            appearances[CreatureConstants.Chimera_White][GenderConstants.Female] = GetBaseFromAverage(5 * 12);
            appearances[CreatureConstants.Chimera_White][GenderConstants.Male] = GetBaseFromAverage(5 * 12);
            appearances[CreatureConstants.Chimera_White][CreatureConstants.Chimera_White] = GetMultiplierFromAverage(5 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Choker
            appearances[CreatureConstants.Choker][GenderConstants.Female] = GetBaseFromAverage(3 * 12 + 6);
            appearances[CreatureConstants.Choker][GenderConstants.Male] = GetBaseFromAverage(3 * 12 + 6);
            appearances[CreatureConstants.Choker][CreatureConstants.Choker] = GetMultiplierFromAverage(3 * 12 + 6);
            //Source: https://forgottenrealms.fandom.com/wiki/Chuul
            appearances[CreatureConstants.Chuul][GenderConstants.Female] = "0";
            appearances[CreatureConstants.Chuul][GenderConstants.Male] = "0";
            appearances[CreatureConstants.Chuul][CreatureConstants.Chuul] = "0";
            //Source: https://forgottenrealms.fandom.com/wiki/Cloaker and https://www.mojobob.com/roleplay/monstrousmanual/c/cloaker.html
            appearances[CreatureConstants.Cloaker][GenderConstants.Agender] = "0";
            appearances[CreatureConstants.Cloaker][CreatureConstants.Cloaker] = "0";
            //Source: https://forgottenrealms.fandom.com/wiki/Cockatrice
            appearances[CreatureConstants.Cockatrice][GenderConstants.Female] = GetBaseFromAverage(3 * 12);
            appearances[CreatureConstants.Cockatrice][GenderConstants.Male] = GetBaseFromAverage(3 * 12);
            appearances[CreatureConstants.Cockatrice][CreatureConstants.Cockatrice] = GetMultiplierFromAverage(3 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Couatl
            appearances[CreatureConstants.Couatl][GenderConstants.Female] = "0";
            appearances[CreatureConstants.Couatl][GenderConstants.Male] = "0";
            appearances[CreatureConstants.Couatl][CreatureConstants.Couatl] = "0";
            //Source: https://www.mojobob.com/roleplay/monstrousmanual/s/sphinx.html
            appearances[CreatureConstants.Criosphinx][GenderConstants.Male] = GetBaseFromAverage(7 * 12 + 6);
            appearances[CreatureConstants.Criosphinx][CreatureConstants.Criosphinx] = GetMultiplierFromAverage(7 * 12 + 6);
            //Source: https://www.dimensions.com/element/nile-crocodile-crocodylus-niloticus
            appearances[CreatureConstants.Crocodile][GenderConstants.Female] = GetBaseFromRange(12, 19);
            appearances[CreatureConstants.Crocodile][GenderConstants.Male] = GetBaseFromRange(12, 19);
            appearances[CreatureConstants.Crocodile][CreatureConstants.Crocodile] = GetMultiplierFromRange(12, 19);
            //Source: https://www.dimensions.com/element/saltwater-crocodile-crocodylus-porosus
            appearances[CreatureConstants.Crocodile_Giant][GenderConstants.Female] = GetBaseFromRange(10, 30);
            appearances[CreatureConstants.Crocodile_Giant][GenderConstants.Male] = GetBaseFromRange(10, 30);
            appearances[CreatureConstants.Crocodile_Giant][CreatureConstants.Crocodile_Giant] = GetMultiplierFromRange(10, 30);
            //Source: https://forgottenrealms.fandom.com/wiki/Hydra half of length for height
            appearances[CreatureConstants.Cryohydra_5Heads][GenderConstants.Female] = GetBaseFromAverage(20 * 12 / 2);
            appearances[CreatureConstants.Cryohydra_5Heads][GenderConstants.Male] = GetBaseFromAverage(20 * 12 / 2);
            appearances[CreatureConstants.Cryohydra_5Heads][CreatureConstants.Cryohydra_5Heads] = GetMultiplierFromAverage(20 * 12 / 2);
            appearances[CreatureConstants.Cryohydra_6Heads][GenderConstants.Female] = GetBaseFromAverage(20 * 12 / 2);
            appearances[CreatureConstants.Cryohydra_6Heads][GenderConstants.Male] = GetBaseFromAverage(20 * 12 / 2);
            appearances[CreatureConstants.Cryohydra_6Heads][CreatureConstants.Cryohydra_6Heads] = GetMultiplierFromAverage(20 * 12 / 2);
            appearances[CreatureConstants.Cryohydra_7Heads][GenderConstants.Female] = GetBaseFromAverage(20 * 12 / 2);
            appearances[CreatureConstants.Cryohydra_7Heads][GenderConstants.Male] = GetBaseFromAverage(20 * 12 / 2);
            appearances[CreatureConstants.Cryohydra_7Heads][CreatureConstants.Cryohydra_7Heads] = GetMultiplierFromAverage(20 * 12 / 2);
            appearances[CreatureConstants.Cryohydra_8Heads][GenderConstants.Female] = GetBaseFromAverage(20 * 12 / 2);
            appearances[CreatureConstants.Cryohydra_8Heads][GenderConstants.Male] = GetBaseFromAverage(20 * 12 / 2);
            appearances[CreatureConstants.Cryohydra_8Heads][CreatureConstants.Cryohydra_8Heads] = GetMultiplierFromAverage(20 * 12 / 2);
            appearances[CreatureConstants.Cryohydra_9Heads][GenderConstants.Female] = GetBaseFromAverage(20 * 12 / 2);
            appearances[CreatureConstants.Cryohydra_9Heads][GenderConstants.Male] = GetBaseFromAverage(20 * 12 / 2);
            appearances[CreatureConstants.Cryohydra_9Heads][CreatureConstants.Cryohydra_9Heads] = GetMultiplierFromAverage(20 * 12 / 2);
            appearances[CreatureConstants.Cryohydra_10Heads][GenderConstants.Female] = GetBaseFromAverage(20 * 12 / 2);
            appearances[CreatureConstants.Cryohydra_10Heads][GenderConstants.Male] = GetBaseFromAverage(20 * 12 / 2);
            appearances[CreatureConstants.Cryohydra_10Heads][CreatureConstants.Cryohydra_10Heads] = GetMultiplierFromAverage(20 * 12 / 2);
            appearances[CreatureConstants.Cryohydra_11Heads][GenderConstants.Female] = GetBaseFromAverage(20 * 12 / 2);
            appearances[CreatureConstants.Cryohydra_11Heads][GenderConstants.Male] = GetBaseFromAverage(20 * 12 / 2);
            appearances[CreatureConstants.Cryohydra_11Heads][CreatureConstants.Cryohydra_11Heads] = GetMultiplierFromAverage(20 * 12 / 2);
            appearances[CreatureConstants.Cryohydra_12Heads][GenderConstants.Female] = GetBaseFromAverage(20 * 12 / 2);
            appearances[CreatureConstants.Cryohydra_12Heads][GenderConstants.Male] = GetBaseFromAverage(20 * 12 / 2);
            appearances[CreatureConstants.Cryohydra_12Heads][CreatureConstants.Cryohydra_12Heads] = GetMultiplierFromAverage(20 * 12 / 2);
            //Source: https://forgottenrealms.fandom.com/wiki/Darkmantle
            appearances[CreatureConstants.Darkmantle][GenderConstants.Hermaphrodite] = "0";
            appearances[CreatureConstants.Darkmantle][CreatureConstants.Darkmantle] = "0";
            //Source: https://www.dimensions.com/element/deinonychus-deinonychus-antirrhopus
            appearances[CreatureConstants.Deinonychus][GenderConstants.Female] = GetBaseFromRange(34, 57);
            appearances[CreatureConstants.Deinonychus][GenderConstants.Male] = GetBaseFromRange(34, 57);
            appearances[CreatureConstants.Deinonychus][CreatureConstants.Deinonychus] = GetMultiplierFromRange(34, 57);
            //Source: https://dungeonsdragons.fandom.com/wiki/Delver
            appearances[CreatureConstants.Delver][GenderConstants.Female] = GetBaseFromAverage(12 * 12);
            appearances[CreatureConstants.Delver][GenderConstants.Male] = GetBaseFromAverage(12 * 12);
            appearances[CreatureConstants.Delver][CreatureConstants.Delver] = GetMultiplierFromAverage(12 * 12);
            //Source: https://monster.fandom.com/wiki/Derro
            appearances[CreatureConstants.Derro][GenderConstants.Female] = GetBaseFromRange(3 * 12, 4 * 12);
            appearances[CreatureConstants.Derro][GenderConstants.Male] = GetBaseFromRange(3 * 12, 4 * 12);
            appearances[CreatureConstants.Derro][CreatureConstants.Derro] = GetMultiplierFromRange(3 * 12, 4 * 12);
            appearances[CreatureConstants.Derro_Sane][GenderConstants.Female] = GetBaseFromRange(3 * 12, 4 * 12);
            appearances[CreatureConstants.Derro_Sane][GenderConstants.Male] = GetBaseFromRange(3 * 12, 4 * 12);
            appearances[CreatureConstants.Derro_Sane][CreatureConstants.Derro_Sane] = GetMultiplierFromRange(3 * 12, 4 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Destrachan
            appearances[CreatureConstants.Destrachan][GenderConstants.Female] = "0";
            appearances[CreatureConstants.Destrachan][GenderConstants.Male] = "0";
            appearances[CreatureConstants.Destrachan][CreatureConstants.Destrachan] = "0";
            //Source: https://www.d20srd.org/srd/monsters/devourer.htm
            appearances[CreatureConstants.Devourer][GenderConstants.Agender] = GetBaseFromAverage(9 * 12);
            appearances[CreatureConstants.Devourer][CreatureConstants.Devourer] = GetMultiplierFromAverage(9 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Digester
            appearances[CreatureConstants.Digester][GenderConstants.Female] = GetBaseFromAverage(5 * 12);
            appearances[CreatureConstants.Digester][GenderConstants.Male] = GetBaseFromAverage(5 * 12);
            appearances[CreatureConstants.Digester][CreatureConstants.Digester] = GetMultiplierFromAverage(5 * 12);
            //Scaled down from Pack Lord, x2 based on length
            appearances[CreatureConstants.DisplacerBeast][GenderConstants.Female] = GetBaseFromAverage(5 * 12);
            appearances[CreatureConstants.DisplacerBeast][GenderConstants.Male] = GetBaseFromAverage(5 * 12);
            appearances[CreatureConstants.DisplacerBeast][CreatureConstants.DisplacerBeast] = GetMultiplierFromAverage(5 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Displacer_beast
            appearances[CreatureConstants.DisplacerBeast_PackLord][GenderConstants.Female] = GetBaseFromAverage(10 * 12);
            appearances[CreatureConstants.DisplacerBeast_PackLord][GenderConstants.Male] = GetBaseFromAverage(10 * 12);
            appearances[CreatureConstants.DisplacerBeast_PackLord][CreatureConstants.DisplacerBeast_PackLord] = GetMultiplierFromAverage(10 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Djinni
            appearances[CreatureConstants.Djinni][GenderConstants.Agender] = GetBaseFromAverage(10 * 12 + 6);
            appearances[CreatureConstants.Djinni][GenderConstants.Female] = GetBaseFromAverage(10 * 12 + 6);
            appearances[CreatureConstants.Djinni][GenderConstants.Male] = GetBaseFromAverage(10 * 12 + 6);
            appearances[CreatureConstants.Djinni][CreatureConstants.Djinni] = GetMultiplierFromAverage(10 * 12 + 6);
            //Source: https://forgottenrealms.fandom.com/wiki/Noble_djinni
            appearances[CreatureConstants.Djinni_Noble][GenderConstants.Agender] = GetBaseFromAverage(12 * 12);
            appearances[CreatureConstants.Djinni_Noble][GenderConstants.Female] = GetBaseFromAverage(12 * 12);
            appearances[CreatureConstants.Djinni_Noble][GenderConstants.Male] = GetBaseFromAverage(12 * 12);
            appearances[CreatureConstants.Djinni_Noble][CreatureConstants.Djinni_Noble] = GetMultiplierFromAverage(12 * 12);
            //Source: https://www.dimensions.com/search?query=dog average of various dogs in the 20-50 pound weight range, including coyote
            appearances[CreatureConstants.Dog][GenderConstants.Female] = GetBaseFromRange(16, 25);
            appearances[CreatureConstants.Dog][GenderConstants.Male] = GetBaseFromRange(16, 25);
            appearances[CreatureConstants.Dog][CreatureConstants.Dog] = GetMultiplierFromRange(16, 25);
            //Source: https://www.dimensions.com/element/saint-bernard-dog M:28-30,F:26-28
            //https://www.dimensions.com/element/siberian-husky 20-24
            //https://www.dimensions.com/element/dogs-collie M:22-24,F:20-22
            appearances[CreatureConstants.Dog_Riding][GenderConstants.Female] = GetBaseFromRange(20, 28);
            appearances[CreatureConstants.Dog_Riding][GenderConstants.Male] = GetBaseFromRange(22, 30);
            appearances[CreatureConstants.Dog_Riding][CreatureConstants.Dog_Riding] = GetMultiplierFromRange(22, 30);
            //Source: https://www.dimensions.com/element/donkey-equus-africanus-asinus
            appearances[CreatureConstants.Donkey][GenderConstants.Female] = GetBaseFromRange(36, 48);
            appearances[CreatureConstants.Donkey][GenderConstants.Male] = GetBaseFromRange(36, 48);
            appearances[CreatureConstants.Donkey][CreatureConstants.Donkey] = GetMultiplierFromRange(36, 48);
            //Source: https://forgottenrealms.fandom.com/wiki/Doppelganger
            appearances[CreatureConstants.Doppelganger][GenderConstants.Agender] = GetBaseFromAverage(5 * 12);
            appearances[CreatureConstants.Doppelganger][CreatureConstants.Doppelganger] = GetMultiplierFromAverage(5 * 12);
            //Source: Draconomicon
            appearances[CreatureConstants.Dragon_Black_Wyrmling][GenderConstants.Female] = GetBaseFromAverage(12);
            appearances[CreatureConstants.Dragon_Black_Wyrmling][GenderConstants.Male] = GetBaseFromAverage(12);
            appearances[CreatureConstants.Dragon_Black_Wyrmling][CreatureConstants.Dragon_Black_Wyrmling] = GetMultiplierFromAverage(12);
            appearances[CreatureConstants.Dragon_Black_VeryYoung][GenderConstants.Female] = GetBaseFromAverage(24);
            appearances[CreatureConstants.Dragon_Black_VeryYoung][GenderConstants.Male] = GetBaseFromAverage(24);
            appearances[CreatureConstants.Dragon_Black_VeryYoung][CreatureConstants.Dragon_Black_VeryYoung] = GetMultiplierFromAverage(24);
            appearances[CreatureConstants.Dragon_Black_Young][GenderConstants.Female] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.Dragon_Black_Young][GenderConstants.Male] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.Dragon_Black_Young][CreatureConstants.Dragon_Black_Young] = GetMultiplierFromAverage(4 * 12);
            appearances[CreatureConstants.Dragon_Black_Juvenile][GenderConstants.Female] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.Dragon_Black_Juvenile][GenderConstants.Male] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.Dragon_Black_Juvenile][CreatureConstants.Dragon_Black_Juvenile] = GetMultiplierFromAverage(4 * 12);
            appearances[CreatureConstants.Dragon_Black_YoungAdult][GenderConstants.Female] = GetBaseFromAverage(7 * 12);
            appearances[CreatureConstants.Dragon_Black_YoungAdult][GenderConstants.Male] = GetBaseFromAverage(7 * 12);
            appearances[CreatureConstants.Dragon_Black_YoungAdult][CreatureConstants.Dragon_Black_YoungAdult] = GetMultiplierFromAverage(7 * 12);
            appearances[CreatureConstants.Dragon_Black_Adult][GenderConstants.Female] = GetBaseFromAverage(7 * 12);
            appearances[CreatureConstants.Dragon_Black_Adult][GenderConstants.Male] = GetBaseFromAverage(7 * 12);
            appearances[CreatureConstants.Dragon_Black_Adult][CreatureConstants.Dragon_Black_Adult] = GetMultiplierFromAverage(7 * 12);
            appearances[CreatureConstants.Dragon_Black_MatureAdult][GenderConstants.Female] = GetBaseFromAverage(12 * 12);
            appearances[CreatureConstants.Dragon_Black_MatureAdult][GenderConstants.Male] = GetBaseFromAverage(12 * 12);
            appearances[CreatureConstants.Dragon_Black_MatureAdult][CreatureConstants.Dragon_Black_MatureAdult] = GetMultiplierFromAverage(12 * 12);
            appearances[CreatureConstants.Dragon_Black_Old][GenderConstants.Female] = GetBaseFromAverage(12 * 12);
            appearances[CreatureConstants.Dragon_Black_Old][GenderConstants.Male] = GetBaseFromAverage(12 * 12);
            appearances[CreatureConstants.Dragon_Black_Old][CreatureConstants.Dragon_Black_Old] = GetMultiplierFromAverage(12 * 12);
            appearances[CreatureConstants.Dragon_Black_VeryOld][GenderConstants.Female] = GetBaseFromAverage(12 * 12);
            appearances[CreatureConstants.Dragon_Black_VeryOld][GenderConstants.Male] = GetBaseFromAverage(12 * 12);
            appearances[CreatureConstants.Dragon_Black_VeryOld][CreatureConstants.Dragon_Black_VeryOld] = GetMultiplierFromAverage(12 * 12);
            appearances[CreatureConstants.Dragon_Black_Ancient][GenderConstants.Female] = GetBaseFromAverage(12 * 12);
            appearances[CreatureConstants.Dragon_Black_Ancient][GenderConstants.Male] = GetBaseFromAverage(12 * 12);
            appearances[CreatureConstants.Dragon_Black_Ancient][CreatureConstants.Dragon_Black_Ancient] = GetMultiplierFromAverage(12 * 12);
            appearances[CreatureConstants.Dragon_Black_Wyrm][GenderConstants.Female] = GetBaseFromAverage(16 * 12);
            appearances[CreatureConstants.Dragon_Black_Wyrm][GenderConstants.Male] = GetBaseFromAverage(16 * 12);
            appearances[CreatureConstants.Dragon_Black_Wyrm][CreatureConstants.Dragon_Black_Wyrm] = GetMultiplierFromAverage(16 * 12);
            appearances[CreatureConstants.Dragon_Black_GreatWyrm][GenderConstants.Female] = GetBaseFromAverage(16 * 12);
            appearances[CreatureConstants.Dragon_Black_GreatWyrm][GenderConstants.Male] = GetBaseFromAverage(16 * 12);
            appearances[CreatureConstants.Dragon_Black_GreatWyrm][CreatureConstants.Dragon_Black_GreatWyrm] = GetMultiplierFromAverage(16 * 12);
            appearances[CreatureConstants.Dragon_Blue_Wyrmling][GenderConstants.Female] = GetBaseFromAverage(24);
            appearances[CreatureConstants.Dragon_Blue_Wyrmling][GenderConstants.Male] = GetBaseFromAverage(24);
            appearances[CreatureConstants.Dragon_Blue_Wyrmling][CreatureConstants.Dragon_Blue_Wyrmling] = GetMultiplierFromAverage(24);
            appearances[CreatureConstants.Dragon_Blue_VeryYoung][GenderConstants.Female] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.Dragon_Blue_VeryYoung][GenderConstants.Male] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.Dragon_Blue_VeryYoung][CreatureConstants.Dragon_Blue_VeryYoung] = GetMultiplierFromAverage(4 * 12);
            appearances[CreatureConstants.Dragon_Blue_Young][GenderConstants.Female] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.Dragon_Blue_Young][GenderConstants.Male] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.Dragon_Blue_Young][CreatureConstants.Dragon_Blue_Young] = GetMultiplierFromAverage(4 * 12);
            appearances[CreatureConstants.Dragon_Blue_Juvenile][GenderConstants.Female] = GetBaseFromAverage(7 * 12);
            appearances[CreatureConstants.Dragon_Blue_Juvenile][GenderConstants.Male] = GetBaseFromAverage(7 * 12);
            appearances[CreatureConstants.Dragon_Blue_Juvenile][CreatureConstants.Dragon_Blue_Juvenile] = GetMultiplierFromAverage(7 * 12);
            appearances[CreatureConstants.Dragon_Blue_YoungAdult][GenderConstants.Female] = GetBaseFromAverage(7 * 12);
            appearances[CreatureConstants.Dragon_Blue_YoungAdult][GenderConstants.Male] = GetBaseFromAverage(7 * 12);
            appearances[CreatureConstants.Dragon_Blue_YoungAdult][CreatureConstants.Dragon_Blue_YoungAdult] = GetMultiplierFromAverage(7 * 12);
            appearances[CreatureConstants.Dragon_Blue_Adult][GenderConstants.Female] = GetBaseFromAverage(12 * 12);
            appearances[CreatureConstants.Dragon_Blue_Adult][GenderConstants.Male] = GetBaseFromAverage(12 * 12);
            appearances[CreatureConstants.Dragon_Blue_Adult][CreatureConstants.Dragon_Blue_Adult] = GetMultiplierFromAverage(12 * 12);
            appearances[CreatureConstants.Dragon_Blue_MatureAdult][GenderConstants.Female] = GetBaseFromAverage(12 * 12);
            appearances[CreatureConstants.Dragon_Blue_MatureAdult][GenderConstants.Male] = GetBaseFromAverage(12 * 12);
            appearances[CreatureConstants.Dragon_Blue_MatureAdult][CreatureConstants.Dragon_Blue_MatureAdult] = GetMultiplierFromAverage(12 * 12);
            appearances[CreatureConstants.Dragon_Blue_Old][GenderConstants.Female] = GetBaseFromAverage(12 * 12);
            appearances[CreatureConstants.Dragon_Blue_Old][GenderConstants.Male] = GetBaseFromAverage(12 * 12);
            appearances[CreatureConstants.Dragon_Blue_Old][CreatureConstants.Dragon_Blue_Old] = GetMultiplierFromAverage(12 * 12);
            appearances[CreatureConstants.Dragon_Blue_VeryOld][GenderConstants.Female] = GetBaseFromAverage(12 * 12);
            appearances[CreatureConstants.Dragon_Blue_VeryOld][GenderConstants.Male] = GetBaseFromAverage(12 * 12);
            appearances[CreatureConstants.Dragon_Blue_VeryOld][CreatureConstants.Dragon_Blue_VeryOld] = GetMultiplierFromAverage(12 * 12);
            appearances[CreatureConstants.Dragon_Blue_Ancient][GenderConstants.Female] = GetBaseFromAverage(16 * 12);
            appearances[CreatureConstants.Dragon_Blue_Ancient][GenderConstants.Male] = GetBaseFromAverage(16 * 12);
            appearances[CreatureConstants.Dragon_Blue_Ancient][CreatureConstants.Dragon_Blue_Ancient] = GetMultiplierFromAverage(16 * 12);
            appearances[CreatureConstants.Dragon_Blue_Wyrm][GenderConstants.Female] = GetBaseFromAverage(16 * 12);
            appearances[CreatureConstants.Dragon_Blue_Wyrm][GenderConstants.Male] = GetBaseFromAverage(16 * 12);
            appearances[CreatureConstants.Dragon_Blue_Wyrm][CreatureConstants.Dragon_Blue_Wyrm] = GetMultiplierFromAverage(16 * 12);
            appearances[CreatureConstants.Dragon_Blue_GreatWyrm][GenderConstants.Female] = GetBaseFromAverage(16 * 12);
            appearances[CreatureConstants.Dragon_Blue_GreatWyrm][GenderConstants.Male] = GetBaseFromAverage(16 * 12);
            appearances[CreatureConstants.Dragon_Blue_GreatWyrm][CreatureConstants.Dragon_Blue_GreatWyrm] = GetMultiplierFromAverage(16 * 12);
            appearances[CreatureConstants.Dragon_Brass_Wyrmling][GenderConstants.Female] = GetBaseFromAverage(12);
            appearances[CreatureConstants.Dragon_Brass_Wyrmling][GenderConstants.Male] = GetBaseFromAverage(12);
            appearances[CreatureConstants.Dragon_Brass_Wyrmling][CreatureConstants.Dragon_Brass_Wyrmling] = GetMultiplierFromAverage(12);
            appearances[CreatureConstants.Dragon_Brass_VeryYoung][GenderConstants.Female] = GetBaseFromAverage(24);
            appearances[CreatureConstants.Dragon_Brass_VeryYoung][GenderConstants.Male] = GetBaseFromAverage(24);
            appearances[CreatureConstants.Dragon_Brass_VeryYoung][CreatureConstants.Dragon_Brass_VeryYoung] = GetMultiplierFromAverage(24);
            appearances[CreatureConstants.Dragon_Brass_Young][GenderConstants.Female] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.Dragon_Brass_Young][GenderConstants.Male] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.Dragon_Brass_Young][CreatureConstants.Dragon_Brass_Young] = GetMultiplierFromAverage(4 * 12);
            appearances[CreatureConstants.Dragon_Brass_Juvenile][GenderConstants.Female] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.Dragon_Brass_Juvenile][GenderConstants.Male] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.Dragon_Brass_Juvenile][CreatureConstants.Dragon_Brass_Juvenile] = GetMultiplierFromAverage(4 * 12);
            appearances[CreatureConstants.Dragon_Brass_YoungAdult][GenderConstants.Female] = GetBaseFromAverage(7 * 12);
            appearances[CreatureConstants.Dragon_Brass_YoungAdult][GenderConstants.Male] = GetBaseFromAverage(7 * 12);
            appearances[CreatureConstants.Dragon_Brass_YoungAdult][CreatureConstants.Dragon_Brass_YoungAdult] = GetMultiplierFromAverage(7 * 12);
            appearances[CreatureConstants.Dragon_Brass_Adult][GenderConstants.Female] = GetBaseFromAverage(7 * 12);
            appearances[CreatureConstants.Dragon_Brass_Adult][GenderConstants.Male] = GetBaseFromAverage(7 * 12);
            appearances[CreatureConstants.Dragon_Brass_Adult][CreatureConstants.Dragon_Brass_Adult] = GetMultiplierFromAverage(7 * 12);
            appearances[CreatureConstants.Dragon_Brass_MatureAdult][GenderConstants.Female] = GetBaseFromAverage(12 * 12);
            appearances[CreatureConstants.Dragon_Brass_MatureAdult][GenderConstants.Male] = GetBaseFromAverage(12 * 12);
            appearances[CreatureConstants.Dragon_Brass_MatureAdult][CreatureConstants.Dragon_Brass_MatureAdult] = GetMultiplierFromAverage(12 * 12);
            appearances[CreatureConstants.Dragon_Brass_Old][GenderConstants.Female] = GetBaseFromAverage(12 * 12);
            appearances[CreatureConstants.Dragon_Brass_Old][GenderConstants.Male] = GetBaseFromAverage(12 * 12);
            appearances[CreatureConstants.Dragon_Brass_Old][CreatureConstants.Dragon_Brass_Old] = GetMultiplierFromAverage(12 * 12);
            appearances[CreatureConstants.Dragon_Brass_VeryOld][GenderConstants.Female] = GetBaseFromAverage(12 * 12);
            appearances[CreatureConstants.Dragon_Brass_VeryOld][GenderConstants.Male] = GetBaseFromAverage(12 * 12);
            appearances[CreatureConstants.Dragon_Brass_VeryOld][CreatureConstants.Dragon_Brass_VeryOld] = GetMultiplierFromAverage(12 * 12);
            appearances[CreatureConstants.Dragon_Brass_Ancient][GenderConstants.Female] = GetBaseFromAverage(12 * 12);
            appearances[CreatureConstants.Dragon_Brass_Ancient][GenderConstants.Male] = GetBaseFromAverage(12 * 12);
            appearances[CreatureConstants.Dragon_Brass_Ancient][CreatureConstants.Dragon_Brass_Ancient] = GetMultiplierFromAverage(12 * 12);
            appearances[CreatureConstants.Dragon_Brass_Wyrm][GenderConstants.Female] = GetBaseFromAverage(16 * 12);
            appearances[CreatureConstants.Dragon_Brass_Wyrm][GenderConstants.Male] = GetBaseFromAverage(16 * 12);
            appearances[CreatureConstants.Dragon_Brass_Wyrm][CreatureConstants.Dragon_Brass_Wyrm] = GetMultiplierFromAverage(16 * 12);
            appearances[CreatureConstants.Dragon_Brass_GreatWyrm][GenderConstants.Female] = GetBaseFromAverage(16 * 12);
            appearances[CreatureConstants.Dragon_Brass_GreatWyrm][GenderConstants.Male] = GetBaseFromAverage(16 * 12);
            appearances[CreatureConstants.Dragon_Brass_GreatWyrm][CreatureConstants.Dragon_Brass_GreatWyrm] = GetMultiplierFromAverage(16 * 12);
            appearances[CreatureConstants.Dragon_Bronze_Wyrmling][GenderConstants.Female] = GetBaseFromAverage(24);
            appearances[CreatureConstants.Dragon_Bronze_Wyrmling][GenderConstants.Male] = GetBaseFromAverage(24);
            appearances[CreatureConstants.Dragon_Bronze_Wyrmling][CreatureConstants.Dragon_Bronze_Wyrmling] = GetMultiplierFromAverage(24);
            appearances[CreatureConstants.Dragon_Bronze_VeryYoung][GenderConstants.Female] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.Dragon_Bronze_VeryYoung][GenderConstants.Male] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.Dragon_Bronze_VeryYoung][CreatureConstants.Dragon_Bronze_VeryYoung] = GetMultiplierFromAverage(4 * 12);
            appearances[CreatureConstants.Dragon_Bronze_Young][GenderConstants.Female] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.Dragon_Bronze_Young][GenderConstants.Male] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.Dragon_Bronze_Young][CreatureConstants.Dragon_Bronze_Young] = GetMultiplierFromAverage(4 * 12);
            appearances[CreatureConstants.Dragon_Bronze_Juvenile][GenderConstants.Female] = GetBaseFromAverage(7 * 12);
            appearances[CreatureConstants.Dragon_Bronze_Juvenile][GenderConstants.Male] = GetBaseFromAverage(7 * 12);
            appearances[CreatureConstants.Dragon_Bronze_Juvenile][CreatureConstants.Dragon_Bronze_Juvenile] = GetMultiplierFromAverage(7 * 12);
            appearances[CreatureConstants.Dragon_Bronze_YoungAdult][GenderConstants.Female] = GetBaseFromAverage(7 * 12);
            appearances[CreatureConstants.Dragon_Bronze_YoungAdult][GenderConstants.Male] = GetBaseFromAverage(7 * 12);
            appearances[CreatureConstants.Dragon_Bronze_YoungAdult][CreatureConstants.Dragon_Bronze_YoungAdult] = GetMultiplierFromAverage(7 * 12);
            appearances[CreatureConstants.Dragon_Bronze_Adult][GenderConstants.Female] = GetBaseFromAverage(12 * 12);
            appearances[CreatureConstants.Dragon_Bronze_Adult][GenderConstants.Male] = GetBaseFromAverage(12 * 12);
            appearances[CreatureConstants.Dragon_Bronze_Adult][CreatureConstants.Dragon_Bronze_Adult] = GetMultiplierFromAverage(12 * 12);
            appearances[CreatureConstants.Dragon_Bronze_MatureAdult][GenderConstants.Female] = GetBaseFromAverage(12 * 12);
            appearances[CreatureConstants.Dragon_Bronze_MatureAdult][GenderConstants.Male] = GetBaseFromAverage(12 * 12);
            appearances[CreatureConstants.Dragon_Bronze_MatureAdult][CreatureConstants.Dragon_Bronze_MatureAdult] = GetMultiplierFromAverage(12 * 12);
            appearances[CreatureConstants.Dragon_Bronze_Old][GenderConstants.Female] = GetBaseFromAverage(12 * 12);
            appearances[CreatureConstants.Dragon_Bronze_Old][GenderConstants.Male] = GetBaseFromAverage(12 * 12);
            appearances[CreatureConstants.Dragon_Bronze_Old][CreatureConstants.Dragon_Bronze_Old] = GetMultiplierFromAverage(12 * 12);
            appearances[CreatureConstants.Dragon_Bronze_VeryOld][GenderConstants.Female] = GetBaseFromAverage(12 * 12);
            appearances[CreatureConstants.Dragon_Bronze_VeryOld][GenderConstants.Male] = GetBaseFromAverage(12 * 12);
            appearances[CreatureConstants.Dragon_Bronze_VeryOld][CreatureConstants.Dragon_Bronze_VeryOld] = GetMultiplierFromAverage(12 * 12);
            appearances[CreatureConstants.Dragon_Bronze_Ancient][GenderConstants.Female] = GetBaseFromAverage(16 * 12);
            appearances[CreatureConstants.Dragon_Bronze_Ancient][GenderConstants.Male] = GetBaseFromAverage(16 * 12);
            appearances[CreatureConstants.Dragon_Bronze_Ancient][CreatureConstants.Dragon_Bronze_Ancient] = GetMultiplierFromAverage(16 * 12);
            appearances[CreatureConstants.Dragon_Bronze_Wyrm][GenderConstants.Female] = GetBaseFromAverage(16 * 12);
            appearances[CreatureConstants.Dragon_Bronze_Wyrm][GenderConstants.Male] = GetBaseFromAverage(16 * 12);
            appearances[CreatureConstants.Dragon_Bronze_Wyrm][CreatureConstants.Dragon_Bronze_Wyrm] = GetMultiplierFromAverage(16 * 12);
            appearances[CreatureConstants.Dragon_Bronze_GreatWyrm][GenderConstants.Female] = GetBaseFromAverage(16 * 12);
            appearances[CreatureConstants.Dragon_Bronze_GreatWyrm][GenderConstants.Male] = GetBaseFromAverage(16 * 12);
            appearances[CreatureConstants.Dragon_Bronze_GreatWyrm][CreatureConstants.Dragon_Bronze_GreatWyrm] = GetMultiplierFromAverage(16 * 12);
            appearances[CreatureConstants.Dragon_Copper_Wyrmling][GenderConstants.Female] = GetBaseFromAverage(12);
            appearances[CreatureConstants.Dragon_Copper_Wyrmling][GenderConstants.Male] = GetBaseFromAverage(12);
            appearances[CreatureConstants.Dragon_Copper_Wyrmling][CreatureConstants.Dragon_Copper_Wyrmling] = GetMultiplierFromAverage(12);
            appearances[CreatureConstants.Dragon_Copper_VeryYoung][GenderConstants.Female] = GetBaseFromAverage(24);
            appearances[CreatureConstants.Dragon_Copper_VeryYoung][GenderConstants.Male] = GetBaseFromAverage(24);
            appearances[CreatureConstants.Dragon_Copper_VeryYoung][CreatureConstants.Dragon_Copper_VeryYoung] = GetMultiplierFromAverage(24);
            appearances[CreatureConstants.Dragon_Copper_Young][GenderConstants.Female] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.Dragon_Copper_Young][GenderConstants.Male] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.Dragon_Copper_Young][CreatureConstants.Dragon_Copper_Young] = GetMultiplierFromAverage(4 * 12);
            appearances[CreatureConstants.Dragon_Copper_Juvenile][GenderConstants.Female] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.Dragon_Copper_Juvenile][GenderConstants.Male] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.Dragon_Copper_Juvenile][CreatureConstants.Dragon_Copper_Juvenile] = GetMultiplierFromAverage(4 * 12);
            appearances[CreatureConstants.Dragon_Copper_YoungAdult][GenderConstants.Female] = GetBaseFromAverage(7 * 12);
            appearances[CreatureConstants.Dragon_Copper_YoungAdult][GenderConstants.Male] = GetBaseFromAverage(7 * 12);
            appearances[CreatureConstants.Dragon_Copper_YoungAdult][CreatureConstants.Dragon_Copper_YoungAdult] = GetMultiplierFromAverage(7 * 12);
            appearances[CreatureConstants.Dragon_Copper_Adult][GenderConstants.Female] = GetBaseFromAverage(7 * 12);
            appearances[CreatureConstants.Dragon_Copper_Adult][GenderConstants.Male] = GetBaseFromAverage(7 * 12);
            appearances[CreatureConstants.Dragon_Copper_Adult][CreatureConstants.Dragon_Copper_Adult] = GetMultiplierFromAverage(7 * 12);
            appearances[CreatureConstants.Dragon_Copper_MatureAdult][GenderConstants.Female] = GetBaseFromAverage(12 * 12);
            appearances[CreatureConstants.Dragon_Copper_MatureAdult][GenderConstants.Male] = GetBaseFromAverage(12 * 12);
            appearances[CreatureConstants.Dragon_Copper_MatureAdult][CreatureConstants.Dragon_Copper_MatureAdult] = GetMultiplierFromAverage(12 * 12);
            appearances[CreatureConstants.Dragon_Copper_Old][GenderConstants.Female] = GetBaseFromAverage(12 * 12);
            appearances[CreatureConstants.Dragon_Copper_Old][GenderConstants.Male] = GetBaseFromAverage(12 * 12);
            appearances[CreatureConstants.Dragon_Copper_Old][CreatureConstants.Dragon_Copper_Old] = GetMultiplierFromAverage(12 * 12);
            appearances[CreatureConstants.Dragon_Copper_VeryOld][GenderConstants.Female] = GetBaseFromAverage(12 * 12);
            appearances[CreatureConstants.Dragon_Copper_VeryOld][GenderConstants.Male] = GetBaseFromAverage(12 * 12);
            appearances[CreatureConstants.Dragon_Copper_VeryOld][CreatureConstants.Dragon_Copper_VeryOld] = GetMultiplierFromAverage(12 * 12);
            appearances[CreatureConstants.Dragon_Copper_Ancient][GenderConstants.Female] = GetBaseFromAverage(12 * 12);
            appearances[CreatureConstants.Dragon_Copper_Ancient][GenderConstants.Male] = GetBaseFromAverage(12 * 12);
            appearances[CreatureConstants.Dragon_Copper_Ancient][CreatureConstants.Dragon_Copper_Ancient] = GetMultiplierFromAverage(12 * 12);
            appearances[CreatureConstants.Dragon_Copper_Wyrm][GenderConstants.Female] = GetBaseFromAverage(16 * 12);
            appearances[CreatureConstants.Dragon_Copper_Wyrm][GenderConstants.Male] = GetBaseFromAverage(16 * 12);
            appearances[CreatureConstants.Dragon_Copper_Wyrm][CreatureConstants.Dragon_Copper_Wyrm] = GetMultiplierFromAverage(16 * 12);
            appearances[CreatureConstants.Dragon_Copper_GreatWyrm][GenderConstants.Female] = GetBaseFromAverage(16 * 12);
            appearances[CreatureConstants.Dragon_Copper_GreatWyrm][GenderConstants.Male] = GetBaseFromAverage(16 * 12);
            appearances[CreatureConstants.Dragon_Copper_GreatWyrm][CreatureConstants.Dragon_Copper_GreatWyrm] = GetMultiplierFromAverage(16 * 12);
            appearances[CreatureConstants.Dragon_Gold_Wyrmling][GenderConstants.Female] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.Dragon_Gold_Wyrmling][GenderConstants.Male] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.Dragon_Gold_Wyrmling][CreatureConstants.Dragon_Gold_Wyrmling] = GetMultiplierFromAverage(4 * 12);
            appearances[CreatureConstants.Dragon_Gold_VeryYoung][GenderConstants.Female] = GetBaseFromAverage(7 * 12);
            appearances[CreatureConstants.Dragon_Gold_VeryYoung][GenderConstants.Male] = GetBaseFromAverage(7 * 12);
            appearances[CreatureConstants.Dragon_Gold_VeryYoung][CreatureConstants.Dragon_Gold_VeryYoung] = GetMultiplierFromAverage(7 * 12);
            appearances[CreatureConstants.Dragon_Gold_Young][GenderConstants.Female] = GetBaseFromAverage(7 * 12);
            appearances[CreatureConstants.Dragon_Gold_Young][GenderConstants.Male] = GetBaseFromAverage(7 * 12);
            appearances[CreatureConstants.Dragon_Gold_Young][CreatureConstants.Dragon_Gold_Young] = GetMultiplierFromAverage(7 * 12);
            appearances[CreatureConstants.Dragon_Gold_Juvenile][GenderConstants.Female] = GetBaseFromAverage(7 * 12);
            appearances[CreatureConstants.Dragon_Gold_Juvenile][GenderConstants.Male] = GetBaseFromAverage(7 * 12);
            appearances[CreatureConstants.Dragon_Gold_Juvenile][CreatureConstants.Dragon_Gold_Juvenile] = GetMultiplierFromAverage(7 * 12);
            appearances[CreatureConstants.Dragon_Gold_YoungAdult][GenderConstants.Female] = GetBaseFromAverage(12 * 12);
            appearances[CreatureConstants.Dragon_Gold_YoungAdult][GenderConstants.Male] = GetBaseFromAverage(12 * 12);
            appearances[CreatureConstants.Dragon_Gold_YoungAdult][CreatureConstants.Dragon_Gold_YoungAdult] = GetMultiplierFromAverage(12 * 12);
            appearances[CreatureConstants.Dragon_Gold_Adult][GenderConstants.Female] = GetBaseFromAverage(12 * 12);
            appearances[CreatureConstants.Dragon_Gold_Adult][GenderConstants.Male] = GetBaseFromAverage(12 * 12);
            appearances[CreatureConstants.Dragon_Gold_Adult][CreatureConstants.Dragon_Gold_Adult] = GetMultiplierFromAverage(12 * 12);
            appearances[CreatureConstants.Dragon_Gold_MatureAdult][GenderConstants.Female] = GetBaseFromAverage(12 * 12);
            appearances[CreatureConstants.Dragon_Gold_MatureAdult][GenderConstants.Male] = GetBaseFromAverage(12 * 12);
            appearances[CreatureConstants.Dragon_Gold_MatureAdult][CreatureConstants.Dragon_Gold_MatureAdult] = GetMultiplierFromAverage(12 * 12);
            appearances[CreatureConstants.Dragon_Gold_Old][GenderConstants.Female] = GetBaseFromAverage(16 * 12);
            appearances[CreatureConstants.Dragon_Gold_Old][GenderConstants.Male] = GetBaseFromAverage(16 * 12);
            appearances[CreatureConstants.Dragon_Gold_Old][CreatureConstants.Dragon_Gold_Old] = GetMultiplierFromAverage(16 * 12);
            appearances[CreatureConstants.Dragon_Gold_VeryOld][GenderConstants.Female] = GetBaseFromAverage(16 * 12);
            appearances[CreatureConstants.Dragon_Gold_VeryOld][GenderConstants.Male] = GetBaseFromAverage(16 * 12);
            appearances[CreatureConstants.Dragon_Gold_VeryOld][CreatureConstants.Dragon_Gold_VeryOld] = GetMultiplierFromAverage(16 * 12);
            appearances[CreatureConstants.Dragon_Gold_Ancient][GenderConstants.Female] = GetBaseFromAverage(16 * 12);
            appearances[CreatureConstants.Dragon_Gold_Ancient][GenderConstants.Male] = GetBaseFromAverage(16 * 12);
            appearances[CreatureConstants.Dragon_Gold_Ancient][CreatureConstants.Dragon_Gold_Ancient] = GetMultiplierFromAverage(16 * 12);
            appearances[CreatureConstants.Dragon_Gold_Wyrm][GenderConstants.Female] = GetBaseFromAverage(22 * 12);
            appearances[CreatureConstants.Dragon_Gold_Wyrm][GenderConstants.Male] = GetBaseFromAverage(22 * 12);
            appearances[CreatureConstants.Dragon_Gold_Wyrm][CreatureConstants.Dragon_Gold_Wyrm] = GetMultiplierFromAverage(22 * 12);
            appearances[CreatureConstants.Dragon_Gold_GreatWyrm][GenderConstants.Female] = GetBaseFromAverage(22 * 12);
            appearances[CreatureConstants.Dragon_Gold_GreatWyrm][GenderConstants.Male] = GetBaseFromAverage(22 * 12);
            appearances[CreatureConstants.Dragon_Gold_GreatWyrm][CreatureConstants.Dragon_Gold_GreatWyrm] = GetMultiplierFromAverage(22 * 12);
            appearances[CreatureConstants.Dragon_Green_Wyrmling][GenderConstants.Female] = GetBaseFromAverage(30);
            appearances[CreatureConstants.Dragon_Green_Wyrmling][GenderConstants.Male] = GetBaseFromAverage(30);
            appearances[CreatureConstants.Dragon_Green_Wyrmling][CreatureConstants.Dragon_Green_Wyrmling] = GetMultiplierFromAverage(30);
            appearances[CreatureConstants.Dragon_Green_VeryYoung][GenderConstants.Female] = GetBaseFromAverage(5 * 12);
            appearances[CreatureConstants.Dragon_Green_VeryYoung][GenderConstants.Male] = GetBaseFromAverage(5 * 12);
            appearances[CreatureConstants.Dragon_Green_VeryYoung][CreatureConstants.Dragon_Green_VeryYoung] = GetMultiplierFromAverage(5 * 12);
            appearances[CreatureConstants.Dragon_Green_Young][GenderConstants.Female] = GetBaseFromAverage(5 * 12);
            appearances[CreatureConstants.Dragon_Green_Young][GenderConstants.Male] = GetBaseFromAverage(5 * 12);
            appearances[CreatureConstants.Dragon_Green_Young][CreatureConstants.Dragon_Green_Young] = GetMultiplierFromAverage(5 * 12);
            appearances[CreatureConstants.Dragon_Green_Juvenile][GenderConstants.Female] = GetBaseFromAverage(9 * 12);
            appearances[CreatureConstants.Dragon_Green_Juvenile][GenderConstants.Male] = GetBaseFromAverage(9 * 12);
            appearances[CreatureConstants.Dragon_Green_Juvenile][CreatureConstants.Dragon_Green_Juvenile] = GetMultiplierFromAverage(9 * 12);
            appearances[CreatureConstants.Dragon_Green_YoungAdult][GenderConstants.Female] = GetBaseFromAverage(9 * 12);
            appearances[CreatureConstants.Dragon_Green_YoungAdult][GenderConstants.Male] = GetBaseFromAverage(9 * 12);
            appearances[CreatureConstants.Dragon_Green_YoungAdult][CreatureConstants.Dragon_Green_YoungAdult] = GetMultiplierFromAverage(9 * 12);
            appearances[CreatureConstants.Dragon_Green_Adult][GenderConstants.Female] = GetBaseFromAverage(15 * 12);
            appearances[CreatureConstants.Dragon_Green_Adult][GenderConstants.Male] = GetBaseFromAverage(15 * 12);
            appearances[CreatureConstants.Dragon_Green_Adult][CreatureConstants.Dragon_Green_Adult] = GetMultiplierFromAverage(15 * 12);
            appearances[CreatureConstants.Dragon_Green_MatureAdult][GenderConstants.Female] = GetBaseFromAverage(15 * 12);
            appearances[CreatureConstants.Dragon_Green_MatureAdult][GenderConstants.Male] = GetBaseFromAverage(15 * 12);
            appearances[CreatureConstants.Dragon_Green_MatureAdult][CreatureConstants.Dragon_Green_MatureAdult] = GetMultiplierFromAverage(15 * 12);
            appearances[CreatureConstants.Dragon_Green_Old][GenderConstants.Female] = GetBaseFromAverage(15 * 12);
            appearances[CreatureConstants.Dragon_Green_Old][GenderConstants.Male] = GetBaseFromAverage(15 * 12);
            appearances[CreatureConstants.Dragon_Green_Old][CreatureConstants.Dragon_Green_Old] = GetMultiplierFromAverage(15 * 12);
            appearances[CreatureConstants.Dragon_Green_VeryOld][GenderConstants.Female] = GetBaseFromAverage(15 * 12);
            appearances[CreatureConstants.Dragon_Green_VeryOld][GenderConstants.Male] = GetBaseFromAverage(15 * 12);
            appearances[CreatureConstants.Dragon_Green_VeryOld][CreatureConstants.Dragon_Green_VeryOld] = GetMultiplierFromAverage(15 * 12);
            appearances[CreatureConstants.Dragon_Green_Ancient][GenderConstants.Female] = GetBaseFromAverage(20 * 12);
            appearances[CreatureConstants.Dragon_Green_Ancient][GenderConstants.Male] = GetBaseFromAverage(20 * 12);
            appearances[CreatureConstants.Dragon_Green_Ancient][CreatureConstants.Dragon_Green_Ancient] = GetMultiplierFromAverage(20 * 12);
            appearances[CreatureConstants.Dragon_Green_Wyrm][GenderConstants.Female] = GetBaseFromAverage(20 * 12);
            appearances[CreatureConstants.Dragon_Green_Wyrm][GenderConstants.Male] = GetBaseFromAverage(20 * 12);
            appearances[CreatureConstants.Dragon_Green_Wyrm][CreatureConstants.Dragon_Green_Wyrm] = GetMultiplierFromAverage(20 * 12);
            appearances[CreatureConstants.Dragon_Green_GreatWyrm][GenderConstants.Female] = GetBaseFromAverage(20 * 12);
            appearances[CreatureConstants.Dragon_Green_GreatWyrm][GenderConstants.Male] = GetBaseFromAverage(20 * 12);
            appearances[CreatureConstants.Dragon_Green_GreatWyrm][CreatureConstants.Dragon_Green_GreatWyrm] = GetMultiplierFromAverage(20 * 12);
            appearances[CreatureConstants.Dragon_Red_Wyrmling][GenderConstants.Female] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.Dragon_Red_Wyrmling][GenderConstants.Male] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.Dragon_Red_Wyrmling][CreatureConstants.Dragon_Red_Wyrmling] = GetMultiplierFromAverage(4 * 12);
            appearances[CreatureConstants.Dragon_Red_VeryYoung][GenderConstants.Female] = GetBaseFromAverage(7 * 12);
            appearances[CreatureConstants.Dragon_Red_VeryYoung][GenderConstants.Male] = GetBaseFromAverage(7 * 12);
            appearances[CreatureConstants.Dragon_Red_VeryYoung][CreatureConstants.Dragon_Red_VeryYoung] = GetMultiplierFromAverage(7 * 12);
            appearances[CreatureConstants.Dragon_Red_Young][GenderConstants.Female] = GetBaseFromAverage(7 * 12);
            appearances[CreatureConstants.Dragon_Red_Young][GenderConstants.Male] = GetBaseFromAverage(7 * 12);
            appearances[CreatureConstants.Dragon_Red_Young][CreatureConstants.Dragon_Red_Young] = GetMultiplierFromAverage(7 * 12);
            appearances[CreatureConstants.Dragon_Red_Juvenile][GenderConstants.Female] = GetBaseFromAverage(7 * 12);
            appearances[CreatureConstants.Dragon_Red_Juvenile][GenderConstants.Male] = GetBaseFromAverage(7 * 12);
            appearances[CreatureConstants.Dragon_Red_Juvenile][CreatureConstants.Dragon_Red_Juvenile] = GetMultiplierFromAverage(7 * 12);
            appearances[CreatureConstants.Dragon_Red_YoungAdult][GenderConstants.Female] = GetBaseFromAverage(12 * 12);
            appearances[CreatureConstants.Dragon_Red_YoungAdult][GenderConstants.Male] = GetBaseFromAverage(12 * 12);
            appearances[CreatureConstants.Dragon_Red_YoungAdult][CreatureConstants.Dragon_Red_YoungAdult] = GetMultiplierFromAverage(12 * 12);
            appearances[CreatureConstants.Dragon_Red_Adult][GenderConstants.Female] = GetBaseFromAverage(12 * 12);
            appearances[CreatureConstants.Dragon_Red_Adult][GenderConstants.Male] = GetBaseFromAverage(12 * 12);
            appearances[CreatureConstants.Dragon_Red_Adult][CreatureConstants.Dragon_Red_Adult] = GetMultiplierFromAverage(12 * 12);
            appearances[CreatureConstants.Dragon_Red_MatureAdult][GenderConstants.Female] = GetBaseFromAverage(12 * 12);
            appearances[CreatureConstants.Dragon_Red_MatureAdult][GenderConstants.Male] = GetBaseFromAverage(12 * 12);
            appearances[CreatureConstants.Dragon_Red_MatureAdult][CreatureConstants.Dragon_Red_MatureAdult] = GetMultiplierFromAverage(12 * 12);
            appearances[CreatureConstants.Dragon_Red_Old][GenderConstants.Female] = GetBaseFromAverage(16 * 12);
            appearances[CreatureConstants.Dragon_Red_Old][GenderConstants.Male] = GetBaseFromAverage(16 * 12);
            appearances[CreatureConstants.Dragon_Red_Old][CreatureConstants.Dragon_Red_Old] = GetMultiplierFromAverage(16 * 12);
            appearances[CreatureConstants.Dragon_Red_VeryOld][GenderConstants.Female] = GetBaseFromAverage(16 * 12);
            appearances[CreatureConstants.Dragon_Red_VeryOld][GenderConstants.Male] = GetBaseFromAverage(16 * 12);
            appearances[CreatureConstants.Dragon_Red_VeryOld][CreatureConstants.Dragon_Red_VeryOld] = GetMultiplierFromAverage(16 * 12);
            appearances[CreatureConstants.Dragon_Red_Ancient][GenderConstants.Female] = GetBaseFromAverage(16 * 12);
            appearances[CreatureConstants.Dragon_Red_Ancient][GenderConstants.Male] = GetBaseFromAverage(16 * 12);
            appearances[CreatureConstants.Dragon_Red_Ancient][CreatureConstants.Dragon_Red_Ancient] = GetMultiplierFromAverage(16 * 12);
            appearances[CreatureConstants.Dragon_Red_Wyrm][GenderConstants.Female] = GetBaseFromAverage(16 * 12);
            appearances[CreatureConstants.Dragon_Red_Wyrm][GenderConstants.Male] = GetBaseFromAverage(16 * 12);
            appearances[CreatureConstants.Dragon_Red_Wyrm][CreatureConstants.Dragon_Red_Wyrm] = GetMultiplierFromAverage(16 * 12);
            appearances[CreatureConstants.Dragon_Red_GreatWyrm][GenderConstants.Female] = GetBaseFromAverage(22 * 12);
            appearances[CreatureConstants.Dragon_Red_GreatWyrm][GenderConstants.Male] = GetBaseFromAverage(22 * 12);
            appearances[CreatureConstants.Dragon_Red_GreatWyrm][CreatureConstants.Dragon_Red_GreatWyrm] = GetMultiplierFromAverage(22 * 12);
            appearances[CreatureConstants.Dragon_Silver_Wyrmling][GenderConstants.Female] = GetBaseFromAverage(2 * 12);
            appearances[CreatureConstants.Dragon_Silver_Wyrmling][GenderConstants.Male] = GetBaseFromAverage(2 * 12);
            appearances[CreatureConstants.Dragon_Silver_Wyrmling][CreatureConstants.Dragon_Silver_Wyrmling] = GetMultiplierFromAverage(2 * 12);
            appearances[CreatureConstants.Dragon_Silver_VeryYoung][GenderConstants.Female] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.Dragon_Silver_VeryYoung][GenderConstants.Male] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.Dragon_Silver_VeryYoung][CreatureConstants.Dragon_Silver_VeryYoung] = GetMultiplierFromAverage(4 * 12);
            appearances[CreatureConstants.Dragon_Silver_Young][GenderConstants.Female] = GetBaseFromAverage(7 * 12);
            appearances[CreatureConstants.Dragon_Silver_Young][GenderConstants.Male] = GetBaseFromAverage(7 * 12);
            appearances[CreatureConstants.Dragon_Silver_Young][CreatureConstants.Dragon_Silver_Young] = GetMultiplierFromAverage(7 * 12);
            appearances[CreatureConstants.Dragon_Silver_Juvenile][GenderConstants.Female] = GetBaseFromAverage(7 * 12);
            appearances[CreatureConstants.Dragon_Silver_Juvenile][GenderConstants.Male] = GetBaseFromAverage(7 * 12);
            appearances[CreatureConstants.Dragon_Silver_Juvenile][CreatureConstants.Dragon_Silver_Juvenile] = GetMultiplierFromAverage(7 * 12);
            appearances[CreatureConstants.Dragon_Silver_YoungAdult][GenderConstants.Female] = GetBaseFromAverage(7 * 12);
            appearances[CreatureConstants.Dragon_Silver_YoungAdult][GenderConstants.Male] = GetBaseFromAverage(7 * 12);
            appearances[CreatureConstants.Dragon_Silver_YoungAdult][CreatureConstants.Dragon_Silver_YoungAdult] = GetMultiplierFromAverage(7 * 12);
            appearances[CreatureConstants.Dragon_Silver_Adult][GenderConstants.Female] = GetBaseFromAverage(12 * 12);
            appearances[CreatureConstants.Dragon_Silver_Adult][GenderConstants.Male] = GetBaseFromAverage(12 * 12);
            appearances[CreatureConstants.Dragon_Silver_Adult][CreatureConstants.Dragon_Silver_Adult] = GetMultiplierFromAverage(12 * 12);
            appearances[CreatureConstants.Dragon_Silver_MatureAdult][GenderConstants.Female] = GetBaseFromAverage(12 * 12);
            appearances[CreatureConstants.Dragon_Silver_MatureAdult][GenderConstants.Male] = GetBaseFromAverage(12 * 12);
            appearances[CreatureConstants.Dragon_Silver_MatureAdult][CreatureConstants.Dragon_Silver_MatureAdult] = GetMultiplierFromAverage(12 * 12);
            appearances[CreatureConstants.Dragon_Silver_Old][GenderConstants.Female] = GetBaseFromAverage(12 * 12);
            appearances[CreatureConstants.Dragon_Silver_Old][GenderConstants.Male] = GetBaseFromAverage(12 * 12);
            appearances[CreatureConstants.Dragon_Silver_Old][CreatureConstants.Dragon_Silver_Old] = GetMultiplierFromAverage(12 * 12);
            appearances[CreatureConstants.Dragon_Silver_VeryOld][GenderConstants.Female] = GetBaseFromAverage(12 * 12);
            appearances[CreatureConstants.Dragon_Silver_VeryOld][GenderConstants.Male] = GetBaseFromAverage(12 * 12);
            appearances[CreatureConstants.Dragon_Silver_VeryOld][CreatureConstants.Dragon_Silver_VeryOld] = GetMultiplierFromAverage(12 * 12);
            appearances[CreatureConstants.Dragon_Silver_Ancient][GenderConstants.Female] = GetBaseFromAverage(16 * 12);
            appearances[CreatureConstants.Dragon_Silver_Ancient][GenderConstants.Male] = GetBaseFromAverage(16 * 12);
            appearances[CreatureConstants.Dragon_Silver_Ancient][CreatureConstants.Dragon_Silver_Ancient] = GetMultiplierFromAverage(16 * 12);
            appearances[CreatureConstants.Dragon_Silver_Wyrm][GenderConstants.Female] = GetBaseFromAverage(16 * 12);
            appearances[CreatureConstants.Dragon_Silver_Wyrm][GenderConstants.Male] = GetBaseFromAverage(16 * 12);
            appearances[CreatureConstants.Dragon_Silver_Wyrm][CreatureConstants.Dragon_Silver_Wyrm] = GetMultiplierFromAverage(16 * 12);
            appearances[CreatureConstants.Dragon_Silver_GreatWyrm][GenderConstants.Female] = GetBaseFromAverage(22 * 12);
            appearances[CreatureConstants.Dragon_Silver_GreatWyrm][GenderConstants.Male] = GetBaseFromAverage(22 * 12);
            appearances[CreatureConstants.Dragon_Silver_GreatWyrm][CreatureConstants.Dragon_Silver_GreatWyrm] = GetMultiplierFromAverage(22 * 12);
            appearances[CreatureConstants.Dragon_White_Wyrmling][GenderConstants.Female] = GetBaseFromAverage(12);
            appearances[CreatureConstants.Dragon_White_Wyrmling][GenderConstants.Male] = GetBaseFromAverage(12);
            appearances[CreatureConstants.Dragon_White_Wyrmling][CreatureConstants.Dragon_White_Wyrmling] = GetMultiplierFromAverage(12);
            appearances[CreatureConstants.Dragon_White_VeryYoung][GenderConstants.Female] = GetBaseFromAverage(24);
            appearances[CreatureConstants.Dragon_White_VeryYoung][GenderConstants.Male] = GetBaseFromAverage(24);
            appearances[CreatureConstants.Dragon_White_VeryYoung][CreatureConstants.Dragon_White_VeryYoung] = GetMultiplierFromAverage(24);
            appearances[CreatureConstants.Dragon_White_Young][GenderConstants.Female] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.Dragon_White_Young][GenderConstants.Male] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.Dragon_White_Young][CreatureConstants.Dragon_White_Young] = GetMultiplierFromAverage(4 * 12);
            appearances[CreatureConstants.Dragon_White_Juvenile][GenderConstants.Female] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.Dragon_White_Juvenile][GenderConstants.Male] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.Dragon_White_Juvenile][CreatureConstants.Dragon_White_Juvenile] = GetMultiplierFromAverage(4 * 12);
            appearances[CreatureConstants.Dragon_White_YoungAdult][GenderConstants.Female] = GetBaseFromAverage(7 * 12);
            appearances[CreatureConstants.Dragon_White_YoungAdult][GenderConstants.Male] = GetBaseFromAverage(7 * 12);
            appearances[CreatureConstants.Dragon_White_YoungAdult][CreatureConstants.Dragon_White_YoungAdult] = GetMultiplierFromAverage(7 * 12);
            appearances[CreatureConstants.Dragon_White_Adult][GenderConstants.Female] = GetBaseFromAverage(7 * 12);
            appearances[CreatureConstants.Dragon_White_Adult][GenderConstants.Male] = GetBaseFromAverage(7 * 12);
            appearances[CreatureConstants.Dragon_White_Adult][CreatureConstants.Dragon_White_Adult] = GetMultiplierFromAverage(7 * 12);
            appearances[CreatureConstants.Dragon_White_MatureAdult][GenderConstants.Female] = GetBaseFromAverage(12 * 12);
            appearances[CreatureConstants.Dragon_White_MatureAdult][GenderConstants.Male] = GetBaseFromAverage(12 * 12);
            appearances[CreatureConstants.Dragon_White_MatureAdult][CreatureConstants.Dragon_White_MatureAdult] = GetMultiplierFromAverage(12 * 12);
            appearances[CreatureConstants.Dragon_White_Old][GenderConstants.Female] = GetBaseFromAverage(12 * 12);
            appearances[CreatureConstants.Dragon_White_Old][GenderConstants.Male] = GetBaseFromAverage(12 * 12);
            appearances[CreatureConstants.Dragon_White_Old][CreatureConstants.Dragon_White_Old] = GetMultiplierFromAverage(12 * 12);
            appearances[CreatureConstants.Dragon_White_VeryOld][GenderConstants.Female] = GetBaseFromAverage(12 * 12);
            appearances[CreatureConstants.Dragon_White_VeryOld][GenderConstants.Male] = GetBaseFromAverage(12 * 12);
            appearances[CreatureConstants.Dragon_White_VeryOld][CreatureConstants.Dragon_White_VeryOld] = GetMultiplierFromAverage(12 * 12);
            appearances[CreatureConstants.Dragon_White_Ancient][GenderConstants.Female] = GetBaseFromAverage(12 * 12);
            appearances[CreatureConstants.Dragon_White_Ancient][GenderConstants.Male] = GetBaseFromAverage(12 * 12);
            appearances[CreatureConstants.Dragon_White_Ancient][CreatureConstants.Dragon_White_Ancient] = GetMultiplierFromAverage(12 * 12);
            appearances[CreatureConstants.Dragon_White_Wyrm][GenderConstants.Female] = GetBaseFromAverage(16 * 12);
            appearances[CreatureConstants.Dragon_White_Wyrm][GenderConstants.Male] = GetBaseFromAverage(16 * 12);
            appearances[CreatureConstants.Dragon_White_Wyrm][CreatureConstants.Dragon_White_Wyrm] = GetMultiplierFromAverage(16 * 12);
            appearances[CreatureConstants.Dragon_White_GreatWyrm][GenderConstants.Female] = GetBaseFromAverage(16 * 12);
            appearances[CreatureConstants.Dragon_White_GreatWyrm][GenderConstants.Male] = GetBaseFromAverage(16 * 12);
            appearances[CreatureConstants.Dragon_White_GreatWyrm][CreatureConstants.Dragon_White_GreatWyrm] = GetMultiplierFromAverage(16 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Dragon_turtle
            appearances[CreatureConstants.DragonTurtle][GenderConstants.Female] = "0";
            appearances[CreatureConstants.DragonTurtle][GenderConstants.Male] = "0";
            appearances[CreatureConstants.DragonTurtle][CreatureConstants.DragonTurtle] = "0";
            //Source: https://forgottenrealms.fandom.com/wiki/Dragonne
            appearances[CreatureConstants.Dragonne][GenderConstants.Female] = GetBaseFromAverage(5 * 12);
            appearances[CreatureConstants.Dragonne][GenderConstants.Male] = GetBaseFromAverage(5 * 12);
            appearances[CreatureConstants.Dragonne][CreatureConstants.Dragonne] = GetMultiplierFromAverage(5 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Dretch
            appearances[CreatureConstants.Dretch][GenderConstants.Agender] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.Dretch][GenderConstants.Female] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.Dretch][GenderConstants.Male] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.Dretch][CreatureConstants.Dretch] = GetMultiplierFromAverage(4 * 12);
            //Source: https://www.worldanvil.com/w/faerun-tatortotzke/a/drider-species
            //https://www.5esrd.com/database/race/drider/ (for height)
            appearances[CreatureConstants.Drider][GenderConstants.Agender] = GetBaseFromAverage(7 * 12);
            appearances[CreatureConstants.Drider][CreatureConstants.Drider] = GetMultiplierFromAverage(7 * 12);
            //Source: https://www.worldanvil.com/w/faerun-tatortotzke/a/dryad-species
            appearances[CreatureConstants.Dryad][GenderConstants.Female] = GetBaseFromAverage(5 * 12);
            appearances[CreatureConstants.Dryad][CreatureConstants.Dryad] = GetMultiplierFromAverage(5 * 12);
            //Source: https://www.d20srd.org/srd/description.htm#vitalStatistics
            appearances[CreatureConstants.Dwarf_Deep][GenderConstants.Female] = "3*12+7";
            appearances[CreatureConstants.Dwarf_Deep][GenderConstants.Male] = "3*12+9";
            appearances[CreatureConstants.Dwarf_Deep][CreatureConstants.Dwarf_Deep] = "2d4";
            appearances[CreatureConstants.Dwarf_Duergar][GenderConstants.Female] = "3*12+7";
            appearances[CreatureConstants.Dwarf_Duergar][GenderConstants.Male] = "3*12+9";
            appearances[CreatureConstants.Dwarf_Duergar][CreatureConstants.Dwarf_Duergar] = "2d4";
            appearances[CreatureConstants.Dwarf_Hill][GenderConstants.Female] = "3*12+7";
            appearances[CreatureConstants.Dwarf_Hill][GenderConstants.Male] = "3*12+9";
            appearances[CreatureConstants.Dwarf_Hill][CreatureConstants.Dwarf_Hill] = "2d4";
            appearances[CreatureConstants.Dwarf_Mountain][GenderConstants.Female] = "3*12+7";
            appearances[CreatureConstants.Dwarf_Mountain][GenderConstants.Male] = "3*12+9";
            appearances[CreatureConstants.Dwarf_Mountain][CreatureConstants.Dwarf_Mountain] = "2d4";
            //Source: https://www.dimensions.com/element/bald-eagle-haliaeetus-leucocephalus
            appearances[CreatureConstants.Eagle][GenderConstants.Female] = GetBaseFromRange(17, 24);
            appearances[CreatureConstants.Eagle][GenderConstants.Male] = GetBaseFromRange(17, 24);
            appearances[CreatureConstants.Eagle][CreatureConstants.Eagle] = GetMultiplierFromRange(17, 24);
            //Source: https://www.d20srd.org/srd/monsters/eagleGiant.htm
            appearances[CreatureConstants.Eagle_Giant][GenderConstants.Female] = GetBaseFromAverage(10 * 12);
            appearances[CreatureConstants.Eagle_Giant][GenderConstants.Male] = GetBaseFromAverage(10 * 12);
            appearances[CreatureConstants.Eagle_Giant][CreatureConstants.Eagle_Giant] = GetMultiplierFromAverage(10 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Efreeti
            appearances[CreatureConstants.Efreeti][GenderConstants.Agender] = GetBaseFromAverage(12 * 12);
            appearances[CreatureConstants.Efreeti][GenderConstants.Female] = GetBaseFromAverage(12 * 12);
            appearances[CreatureConstants.Efreeti][GenderConstants.Male] = GetBaseFromAverage(12 * 12);
            appearances[CreatureConstants.Efreeti][CreatureConstants.Efreeti] = GetMultiplierFromAverage(12 * 12);
            //Source: https://jurassicworld-evolution.fandom.com/wiki/Elasmosaurus
            appearances[CreatureConstants.Elasmosaurus][GenderConstants.Female] = GetBaseFromAverage(40);
            appearances[CreatureConstants.Elasmosaurus][GenderConstants.Male] = GetBaseFromAverage(40);
            appearances[CreatureConstants.Elasmosaurus][CreatureConstants.Elasmosaurus] = GetMultiplierFromAverage(40);
            //Source: https://www.d20srd.org/srd/monsters/elemental.htm
            appearances[CreatureConstants.Elemental_Air_Small][GenderConstants.Agender] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.Elemental_Air_Small][CreatureConstants.Elemental_Air_Small] = GetMultiplierFromAverage(4 * 12);
            appearances[CreatureConstants.Elemental_Air_Medium][GenderConstants.Agender] = GetBaseFromAverage(8 * 12);
            appearances[CreatureConstants.Elemental_Air_Medium][CreatureConstants.Elemental_Air_Medium] = GetMultiplierFromAverage(8 * 12);
            appearances[CreatureConstants.Elemental_Air_Large][GenderConstants.Agender] = GetBaseFromAverage(16 * 12);
            appearances[CreatureConstants.Elemental_Air_Large][CreatureConstants.Elemental_Air_Large] = GetMultiplierFromAverage(16 * 12);
            appearances[CreatureConstants.Elemental_Air_Huge][GenderConstants.Agender] = GetBaseFromAverage(32 * 12);
            appearances[CreatureConstants.Elemental_Air_Huge][CreatureConstants.Elemental_Air_Huge] = GetMultiplierFromAverage(32 * 12);
            appearances[CreatureConstants.Elemental_Air_Greater][GenderConstants.Agender] = GetBaseFromAverage(36 * 12);
            appearances[CreatureConstants.Elemental_Air_Greater][CreatureConstants.Elemental_Air_Greater] = GetMultiplierFromAverage(36 * 12);
            appearances[CreatureConstants.Elemental_Air_Elder][GenderConstants.Agender] = GetBaseFromAverage(40 * 12);
            appearances[CreatureConstants.Elemental_Air_Elder][CreatureConstants.Elemental_Air_Elder] = GetMultiplierFromAverage(40 * 12);
            appearances[CreatureConstants.Elemental_Earth_Small][GenderConstants.Agender] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.Elemental_Earth_Small][CreatureConstants.Elemental_Earth_Small] = GetMultiplierFromAverage(4 * 12);
            appearances[CreatureConstants.Elemental_Earth_Medium][GenderConstants.Agender] = GetBaseFromAverage(8 * 12);
            appearances[CreatureConstants.Elemental_Earth_Medium][CreatureConstants.Elemental_Earth_Medium] = GetMultiplierFromAverage(8 * 12);
            appearances[CreatureConstants.Elemental_Earth_Large][GenderConstants.Agender] = GetBaseFromAverage(16 * 12);
            appearances[CreatureConstants.Elemental_Earth_Large][CreatureConstants.Elemental_Earth_Large] = GetMultiplierFromAverage(16 * 12);
            appearances[CreatureConstants.Elemental_Earth_Huge][GenderConstants.Agender] = GetBaseFromAverage(32 * 12);
            appearances[CreatureConstants.Elemental_Earth_Huge][CreatureConstants.Elemental_Earth_Huge] = GetMultiplierFromAverage(32 * 12);
            appearances[CreatureConstants.Elemental_Earth_Greater][GenderConstants.Agender] = GetBaseFromAverage(36 * 12);
            appearances[CreatureConstants.Elemental_Earth_Greater][CreatureConstants.Elemental_Earth_Greater] = GetMultiplierFromAverage(36 * 12);
            appearances[CreatureConstants.Elemental_Earth_Elder][GenderConstants.Agender] = GetBaseFromAverage(40 * 12);
            appearances[CreatureConstants.Elemental_Earth_Elder][CreatureConstants.Elemental_Earth_Elder] = GetMultiplierFromAverage(40 * 12);
            appearances[CreatureConstants.Elemental_Fire_Small][GenderConstants.Agender] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.Elemental_Fire_Small][CreatureConstants.Elemental_Fire_Small] = GetMultiplierFromAverage(4 * 12);
            appearances[CreatureConstants.Elemental_Fire_Medium][GenderConstants.Agender] = GetBaseFromAverage(8 * 12);
            appearances[CreatureConstants.Elemental_Fire_Medium][CreatureConstants.Elemental_Fire_Medium] = GetMultiplierFromAverage(8 * 12);
            appearances[CreatureConstants.Elemental_Fire_Large][GenderConstants.Agender] = GetBaseFromAverage(16 * 12);
            appearances[CreatureConstants.Elemental_Fire_Large][CreatureConstants.Elemental_Fire_Large] = GetMultiplierFromAverage(16 * 12);
            appearances[CreatureConstants.Elemental_Fire_Huge][GenderConstants.Agender] = GetBaseFromAverage(32 * 12);
            appearances[CreatureConstants.Elemental_Fire_Huge][CreatureConstants.Elemental_Fire_Huge] = GetMultiplierFromAverage(32 * 12);
            appearances[CreatureConstants.Elemental_Fire_Greater][GenderConstants.Agender] = GetBaseFromAverage(36 * 12);
            appearances[CreatureConstants.Elemental_Fire_Greater][CreatureConstants.Elemental_Fire_Greater] = GetMultiplierFromAverage(36 * 12);
            appearances[CreatureConstants.Elemental_Fire_Elder][GenderConstants.Agender] = GetBaseFromAverage(40 * 12);
            appearances[CreatureConstants.Elemental_Fire_Elder][CreatureConstants.Elemental_Fire_Elder] = GetMultiplierFromAverage(40 * 12);
            appearances[CreatureConstants.Elemental_Water_Small][GenderConstants.Agender] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.Elemental_Water_Small][CreatureConstants.Elemental_Water_Small] = GetMultiplierFromAverage(4 * 12);
            appearances[CreatureConstants.Elemental_Water_Medium][GenderConstants.Agender] = GetBaseFromAverage(8 * 12);
            appearances[CreatureConstants.Elemental_Water_Medium][CreatureConstants.Elemental_Water_Medium] = GetMultiplierFromAverage(8 * 12);
            appearances[CreatureConstants.Elemental_Water_Large][GenderConstants.Agender] = GetBaseFromAverage(16 * 12);
            appearances[CreatureConstants.Elemental_Water_Large][CreatureConstants.Elemental_Water_Large] = GetMultiplierFromAverage(16 * 12);
            appearances[CreatureConstants.Elemental_Water_Huge][GenderConstants.Agender] = GetBaseFromAverage(32 * 12);
            appearances[CreatureConstants.Elemental_Water_Huge][CreatureConstants.Elemental_Water_Huge] = GetMultiplierFromAverage(32 * 12);
            appearances[CreatureConstants.Elemental_Water_Greater][GenderConstants.Agender] = GetBaseFromAverage(36 * 12);
            appearances[CreatureConstants.Elemental_Water_Greater][CreatureConstants.Elemental_Water_Greater] = GetMultiplierFromAverage(36 * 12);
            appearances[CreatureConstants.Elemental_Water_Elder][GenderConstants.Agender] = GetBaseFromAverage(40 * 12);
            appearances[CreatureConstants.Elemental_Water_Elder][CreatureConstants.Elemental_Water_Elder] = GetMultiplierFromAverage(40 * 12);
            //Source: https://www.dimensions.com/element/african-bush-elephant-loxodonta-africana
            appearances[CreatureConstants.Elephant][GenderConstants.Female] = GetBaseFromRange(8 * 12 + 6, 13 * 12);
            appearances[CreatureConstants.Elephant][GenderConstants.Male] = GetBaseFromRange(8 * 12 + 6, 13 * 12);
            appearances[CreatureConstants.Elephant][CreatureConstants.Elephant] = GetMultiplierFromRange(8 * 12 + 6, 13 * 12);
            //Source: https://www.d20srd.org/srd/description.htm#vitalStatistics
            appearances[CreatureConstants.Elf_Aquatic][GenderConstants.Female] = "4*12+5";
            appearances[CreatureConstants.Elf_Aquatic][GenderConstants.Male] = "4*12+5";
            appearances[CreatureConstants.Elf_Aquatic][CreatureConstants.Elf_Aquatic] = "2d6";
            appearances[CreatureConstants.Elf_Drow][GenderConstants.Female] = "4*12+5";
            appearances[CreatureConstants.Elf_Drow][GenderConstants.Male] = "4*12+5";
            appearances[CreatureConstants.Elf_Drow][CreatureConstants.Elf_Drow] = "2d6";
            appearances[CreatureConstants.Elf_Gray][GenderConstants.Female] = "4*12+5";
            appearances[CreatureConstants.Elf_Gray][GenderConstants.Male] = "4*12+5";
            appearances[CreatureConstants.Elf_Gray][CreatureConstants.Elf_Gray] = "2d6";
            appearances[CreatureConstants.Elf_Half][GenderConstants.Female] = "4*12+5";
            appearances[CreatureConstants.Elf_Half][GenderConstants.Male] = "4*12+7";
            appearances[CreatureConstants.Elf_Half][CreatureConstants.Elf_Half] = "2d8";
            appearances[CreatureConstants.Elf_High][GenderConstants.Female] = "4*12+5";
            appearances[CreatureConstants.Elf_High][GenderConstants.Male] = "4*12+5";
            appearances[CreatureConstants.Elf_High][CreatureConstants.Elf_High] = "2d6";
            appearances[CreatureConstants.Elf_Wild][GenderConstants.Female] = "4*12+5";
            appearances[CreatureConstants.Elf_Wild][GenderConstants.Male] = "4*12+5";
            appearances[CreatureConstants.Elf_Wild][CreatureConstants.Elf_Wild] = "2d6";
            appearances[CreatureConstants.Elf_Wood][GenderConstants.Female] = "4*12+5";
            appearances[CreatureConstants.Elf_Wood][GenderConstants.Male] = "4*12+5";
            appearances[CreatureConstants.Elf_Wood][CreatureConstants.Elf_Wood] = "2d6";
            //Source: https://www.d20srd.org/srd/monsters/devil.htm#erinyes
            appearances[CreatureConstants.Erinyes][GenderConstants.Female] = GetBaseFromAverage(6 * 12);
            appearances[CreatureConstants.Erinyes][GenderConstants.Male] = GetBaseFromAverage(6 * 12);
            appearances[CreatureConstants.Erinyes][CreatureConstants.Erinyes] = GetMultiplierFromAverage(6 * 12);
            //Source: https://aminoapps.com/c/officialdd/page/item/ethereal-filcher/5B63_a5vi5Ia7NM1djj0V1QYVoxpXweGW1z
            appearances[CreatureConstants.EtherealFilcher][GenderConstants.Agender] = GetBaseFromAverage(4 * 12 + 6);
            appearances[CreatureConstants.EtherealFilcher][CreatureConstants.EtherealFilcher] = GetMultiplierFromAverage(4 * 12 + 6);
            //Source: https://www.d20srd.org/srd/monsters/etherealMarauder.htm
            appearances[CreatureConstants.EtherealMarauder][GenderConstants.Female] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.EtherealMarauder][GenderConstants.Male] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.EtherealMarauder][CreatureConstants.EtherealMarauder] = GetMultiplierFromAverage(4 * 12);
            //Source: https://syrikdarkenedskies.obsidianportal.com/wikis/ettercap-race
            appearances[CreatureConstants.Ettercap][GenderConstants.Female] = "5*12+7";
            appearances[CreatureConstants.Ettercap][GenderConstants.Male] = "5*12+2";
            appearances[CreatureConstants.Ettercap][CreatureConstants.Ettercap] = "2d10";
            //Source: https://forgottenrealms.fandom.com/wiki/Ettin
            appearances[CreatureConstants.Ettin][GenderConstants.Female] = "12*12+2";
            appearances[CreatureConstants.Ettin][GenderConstants.Male] = "12*12+10";
            appearances[CreatureConstants.Ettin][CreatureConstants.Ettin] = "2d6";
            appearances[CreatureConstants.FireBeetle_Giant][GenderConstants.Female] = "0";
            appearances[CreatureConstants.FireBeetle_Giant][GenderConstants.Male] = "0";
            appearances[CreatureConstants.FireBeetle_Giant][CreatureConstants.FireBeetle_Giant] = "0";
            //Source: https://www.d20srd.org/srd/monsters/formian.htm
            appearances[CreatureConstants.FormianWorker][GenderConstants.Male] = GetBaseFromAverage(2 * 12 + 6);
            appearances[CreatureConstants.FormianWorker][CreatureConstants.FormianWorker] = GetMultiplierFromAverage(2 * 12 + 6);
            appearances[CreatureConstants.FormianWarrior][GenderConstants.Male] = GetBaseFromAverage(4 * 12 + 6);
            appearances[CreatureConstants.FormianWarrior][CreatureConstants.FormianWarrior] = GetMultiplierFromAverage(4 * 12 + 6);
            appearances[CreatureConstants.FormianTaskmaster][GenderConstants.Male] = GetBaseFromAverage(4 * 12 + 6);
            appearances[CreatureConstants.FormianTaskmaster][CreatureConstants.FormianTaskmaster] = GetMultiplierFromAverage(4 * 12 + 6);
            appearances[CreatureConstants.FormianMyrmarch][GenderConstants.Male] = GetBaseFromAverage(5 * 12 + 6);
            appearances[CreatureConstants.FormianMyrmarch][CreatureConstants.FormianMyrmarch] = GetMultiplierFromAverage(5 * 12 + 6);
            appearances[CreatureConstants.FormianQueen][GenderConstants.Female] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.FormianQueen][CreatureConstants.FormianQueen] = GetMultiplierFromAverage(4 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Frost_worm
            appearances[CreatureConstants.FrostWorm][GenderConstants.Female] = GetBaseFromAverage(5 * 12);
            appearances[CreatureConstants.FrostWorm][GenderConstants.Male] = GetBaseFromAverage(5 * 12);
            appearances[CreatureConstants.FrostWorm][CreatureConstants.FrostWorm] = GetMultiplierFromAverage(5 * 12);
            //Source: https://dungeonsdragons.fandom.com/wiki/Gargoyle
            appearances[CreatureConstants.Gargoyle][GenderConstants.Agender] = "5*12";
            appearances[CreatureConstants.Gargoyle][CreatureConstants.Gargoyle] = "2d10";
            appearances[CreatureConstants.Gargoyle_Kapoacinth][GenderConstants.Agender] = "5*12";
            appearances[CreatureConstants.Gargoyle_Kapoacinth][CreatureConstants.Gargoyle_Kapoacinth] = "2d10";
            //Source: https://www.worldanvil.com/w/faerun-tatortotzke/a/gelatinous-cube-species
            appearances[CreatureConstants.GelatinousCube][GenderConstants.Agender] = GetBaseFromAverage(10 * 12);
            appearances[CreatureConstants.GelatinousCube][CreatureConstants.GelatinousCube] = GetMultiplierFromAverage(10 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Ghaele
            //Adjusting female -2" to match range
            appearances[CreatureConstants.Ghaele][GenderConstants.Female] = GetBaseFromRange(4 * 12 + 7, 6 * 12 + 7 - 2);
            appearances[CreatureConstants.Ghaele][GenderConstants.Male] = GetBaseFromRange(5 * 12 + 2, 7 * 12);
            appearances[CreatureConstants.Ghaele][CreatureConstants.Ghaele] = GetMultiplierFromRange(5 * 12 + 2, 7 * 12);
            //Source: https://www.dandwiki.com/wiki/Ghoul_(5e_Race)
            appearances[CreatureConstants.Ghoul][GenderConstants.Female] = "4*12";
            appearances[CreatureConstants.Ghoul][GenderConstants.Male] = "4*12";
            appearances[CreatureConstants.Ghoul][CreatureConstants.Ghoul] = "2d12";
            appearances[CreatureConstants.Ghoul_Ghast][GenderConstants.Female] = "4*12";
            appearances[CreatureConstants.Ghoul_Ghast][GenderConstants.Male] = "4*12";
            appearances[CreatureConstants.Ghoul_Ghast][CreatureConstants.Ghoul_Ghast] = "2d12";
            appearances[CreatureConstants.Ghoul_Lacedon][GenderConstants.Female] = "4*12";
            appearances[CreatureConstants.Ghoul_Lacedon][GenderConstants.Male] = "4*12";
            appearances[CreatureConstants.Ghoul_Lacedon][CreatureConstants.Ghoul_Lacedon] = "2d12";
            //Source: https://forgottenrealms.fandom.com/wiki/Cloud_giant
            appearances[CreatureConstants.Giant_Cloud][GenderConstants.Female] = GetBaseFromRange(22 * 12 + 8, 25 * 12);
            appearances[CreatureConstants.Giant_Cloud][GenderConstants.Male] = GetBaseFromRange(24 * 12 + 4, 26 * 12 + 8);
            appearances[CreatureConstants.Giant_Cloud][CreatureConstants.Giant_Cloud] = GetMultiplierFromRange(22 * 12 + 8, 25 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Fire_giant
            //Adjusting female max -1" to match range
            appearances[CreatureConstants.Giant_Fire][GenderConstants.Female] = GetBaseFromRange(17 * 12 + 5, 19 * 12 - 1);
            appearances[CreatureConstants.Giant_Fire][GenderConstants.Male] = GetBaseFromRange(18 * 12 + 2, 19 * 12 + 8);
            appearances[CreatureConstants.Giant_Fire][CreatureConstants.Giant_Fire] = GetMultiplierFromRange(18 * 12 + 2, 19 * 12 + 8);
            //Source: https://forgottenrealms.fandom.com/wiki/Frost_giant
            appearances[CreatureConstants.Giant_Frost][GenderConstants.Female] = GetBaseFromRange(20 * 12 + 1, 22 * 12 + 4);
            appearances[CreatureConstants.Giant_Frost][GenderConstants.Male] = GetBaseFromRange(21 * 12 + 3, 23 * 12 + 6);
            appearances[CreatureConstants.Giant_Frost][CreatureConstants.Giant_Frost] = GetMultiplierFromRange(21 * 12 + 3, 23 * 12 + 6);
            //Source: https://forgottenrealms.fandom.com/wiki/Hill_giant
            appearances[CreatureConstants.Giant_Hill][GenderConstants.Female] = GetBaseFromRange(15 * 12 + 5, 16 * 12 + 4);
            appearances[CreatureConstants.Giant_Hill][GenderConstants.Male] = GetBaseFromRange(16 * 12 + 1, 17 * 12);
            appearances[CreatureConstants.Giant_Hill][CreatureConstants.Giant_Hill] = GetMultiplierFromRange(15 * 12 + 5, 16 * 12 + 4);
            //Source: https://forgottenrealms.fandom.com/wiki/Stone_giant
            //Adjusting female max -1" to match range
            appearances[CreatureConstants.Giant_Stone][GenderConstants.Female] = GetBaseFromRange(17 * 12 + 5, 19 * 12 - 1);
            appearances[CreatureConstants.Giant_Stone][GenderConstants.Male] = GetBaseFromRange(18 * 12 + 2, 19 * 12 + 8);
            appearances[CreatureConstants.Giant_Stone][CreatureConstants.Giant_Stone] = GetMultiplierFromRange(18 * 12 + 2, 19 * 12 + 8);
            appearances[CreatureConstants.Giant_Stone_Elder][GenderConstants.Female] = GetBaseFromRange(17 * 12 + 5, 19 * 12 - 1);
            appearances[CreatureConstants.Giant_Stone_Elder][GenderConstants.Male] = GetBaseFromRange(18 * 12 + 2, 19 * 12 + 8);
            appearances[CreatureConstants.Giant_Stone_Elder][CreatureConstants.Giant_Stone_Elder] = GetMultiplierFromRange(18 * 12 + 2, 19 * 12 + 8);
            //Source: https://forgottenrealms.fandom.com/wiki/Storm_giant
            appearances[CreatureConstants.Giant_Storm][GenderConstants.Female] = GetBaseFromRange(23 * 12 + 8, 26 * 12 + 8);
            appearances[CreatureConstants.Giant_Storm][GenderConstants.Male] = GetBaseFromRange(26 * 12 + 4, 29 * 12 + 4);
            appearances[CreatureConstants.Giant_Storm][CreatureConstants.Giant_Storm] = GetMultiplierFromRange(26 * 12 + 4, 29 * 12 + 4);
            //Source: https://www.worldanvil.com/w/faerun-tatortotzke/a/gibbering-mouther-species
            appearances[CreatureConstants.GibberingMouther][GenderConstants.Agender] = GetBaseFromRange(3 * 12, 7 * 12);
            appearances[CreatureConstants.GibberingMouther][CreatureConstants.GibberingMouther] = GetMultiplierFromRange(3 * 12, 7 * 12);
            //Source: https://www.d20srd.org/srd/monsters/girallon.htm
            appearances[CreatureConstants.Girallon][GenderConstants.Female] = GetBaseFromAverage(8 * 12);
            appearances[CreatureConstants.Girallon][GenderConstants.Male] = GetBaseFromAverage(8 * 12);
            appearances[CreatureConstants.Girallon][CreatureConstants.Girallon] = GetMultiplierFromAverage(8 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Githyanki
            appearances[CreatureConstants.Githyanki][GenderConstants.Female] = GetBaseFromRange(5 * 12 + 4, 6 * 12 + 10);
            appearances[CreatureConstants.Githyanki][GenderConstants.Male] = GetBaseFromRange(5 * 12 + 5, 6 * 12 + 11);
            appearances[CreatureConstants.Githyanki][CreatureConstants.Githyanki] = GetMultiplierFromRange(5 * 12 + 5, 6 * 12 + 11);
            //Source: https://forgottenrealms.fandom.com/wiki/Githzerai
            appearances[CreatureConstants.Githzerai][GenderConstants.Female] = GetBaseFromRange(5 * 12 + 1, 7 * 12);
            appearances[CreatureConstants.Githzerai][GenderConstants.Male] = GetBaseFromRange(5 * 12 + 1, 7 * 12);
            appearances[CreatureConstants.Githzerai][CreatureConstants.Githzerai] = GetMultiplierFromRange(5 * 12 + 1, 7 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Glabrezu
            appearances[CreatureConstants.Glabrezu][GenderConstants.Agender] = GetBaseFromRange(9 * 12, 15 * 12);
            appearances[CreatureConstants.Glabrezu][CreatureConstants.Glabrezu] = GetMultiplierFromRange(9 * 12, 15 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Gnoll
            appearances[CreatureConstants.Gnoll][GenderConstants.Female] = GetBaseFromRange(7 * 12, 7 * 12 + 6);
            appearances[CreatureConstants.Gnoll][GenderConstants.Male] = GetBaseFromRange(7 * 12, 7 * 12 + 6);
            appearances[CreatureConstants.Gnoll][CreatureConstants.Gnoll] = GetMultiplierFromRange(7 * 12, 7 * 12 + 6);
            //Source: https://www.d20srd.org/srd/description.htm#vitalStatistics
            appearances[CreatureConstants.Gnome_Forest][GenderConstants.Female] = "2*12+10";
            appearances[CreatureConstants.Gnome_Forest][GenderConstants.Male] = "3*12+0";
            appearances[CreatureConstants.Gnome_Forest][CreatureConstants.Gnome_Forest] = "2d4";
            appearances[CreatureConstants.Gnome_Rock][GenderConstants.Female] = "2*12+10";
            appearances[CreatureConstants.Gnome_Rock][GenderConstants.Male] = "3*12+0";
            appearances[CreatureConstants.Gnome_Rock][CreatureConstants.Gnome_Rock] = "2d4";
            appearances[CreatureConstants.Gnome_Svirfneblin][GenderConstants.Female] = "2*12+10";
            appearances[CreatureConstants.Gnome_Svirfneblin][GenderConstants.Male] = "3*12+0";
            appearances[CreatureConstants.Gnome_Svirfneblin][CreatureConstants.Gnome_Svirfneblin] = "2d4";
            //Source: https://forgottenrealms.fandom.com/wiki/Goblin
            appearances[CreatureConstants.Goblin][GenderConstants.Female] = GetBaseFromRange(3 * 12 + 4, 3 * 12 + 8);
            appearances[CreatureConstants.Goblin][GenderConstants.Male] = GetBaseFromRange(3 * 12 + 4, 3 * 12 + 8);
            appearances[CreatureConstants.Goblin][CreatureConstants.Goblin] = GetMultiplierFromRange(3 * 12 + 4, 3 * 12 + 8);
            //Source: https://pathfinderwiki.com/wiki/Clay_golem
            appearances[CreatureConstants.Golem_Clay][GenderConstants.Agender] = GetBaseFromAverage(8 * 12);
            appearances[CreatureConstants.Golem_Clay][CreatureConstants.Golem_Clay] = GetMultiplierFromAverage(8 * 12);
            //Source: https://www.d20srd.org/srd/monsters/golem.htm
            appearances[CreatureConstants.Golem_Flesh][GenderConstants.Agender] = GetBaseFromAverage(8 * 12);
            appearances[CreatureConstants.Golem_Flesh][CreatureConstants.Golem_Flesh] = GetMultiplierFromAverage(8 * 12);
            //Source: https://www.d20srd.org/srd/monsters/golem.htm
            appearances[CreatureConstants.Golem_Iron][GenderConstants.Agender] = GetBaseFromAverage(12 * 12);
            appearances[CreatureConstants.Golem_Iron][CreatureConstants.Golem_Iron] = GetMultiplierFromAverage(12 * 12);
            //Source: https://www.d20srd.org/srd/monsters/golem.htm
            appearances[CreatureConstants.Golem_Stone][GenderConstants.Agender] = GetBaseFromAverage(9 * 12);
            appearances[CreatureConstants.Golem_Stone][CreatureConstants.Golem_Stone] = GetMultiplierFromAverage(9 * 12);
            //Source: https://www.d20srd.org/srd/monsters/golem.htm
            appearances[CreatureConstants.Golem_Stone_Greater][GenderConstants.Agender] = GetBaseFromAverage(18 * 12);
            appearances[CreatureConstants.Golem_Stone_Greater][CreatureConstants.Golem_Stone_Greater] = GetMultiplierFromAverage(18 * 12);
            //Source: https://www.d20srd.org/srd/monsters/gorgon.htm
            appearances[CreatureConstants.Gorgon][GenderConstants.Female] = GetBaseFromAverage(6 * 12);
            appearances[CreatureConstants.Gorgon][GenderConstants.Male] = GetBaseFromAverage(6 * 12);
            appearances[CreatureConstants.Gorgon][CreatureConstants.Gorgon] = GetMultiplierFromAverage(6 * 12);
            //Source: https://www.d20srd.org/srd/monsters/ooze.htm#grayOoze
            appearances[CreatureConstants.GrayOoze][GenderConstants.Agender] = GetBaseFromAverage(6);
            appearances[CreatureConstants.GrayOoze][CreatureConstants.GrayOoze] = GetMultiplierFromAverage(6);
            //Source: https://www.d20srd.org/srd/monsters/grayRender.htm
            appearances[CreatureConstants.GrayRender][GenderConstants.Agender] = GetBaseFromAverage(9 * 12);
            appearances[CreatureConstants.GrayRender][CreatureConstants.GrayRender] = GetMultiplierFromAverage(9 * 12);
            //Source: https://www.d20srd.org/srd/monsters/hag.htm#greenHag Copy from Human
            appearances[CreatureConstants.GreenHag][GenderConstants.Female] = "4*12+5";
            appearances[CreatureConstants.GreenHag][CreatureConstants.GreenHag] = "2d10";
            //Source: https://www.d20srd.org/srd/monsters/grick.htm
            appearances[CreatureConstants.Grick][GenderConstants.Female] = "0";
            appearances[CreatureConstants.Grick][GenderConstants.Male] = "0";
            appearances[CreatureConstants.Grick][CreatureConstants.Grick] = "0";
            //Source: https://forgottenrealms.fandom.com/wiki/Griffon
            appearances[CreatureConstants.Griffon][GenderConstants.Female] = GetBaseFromAverage(5 * 12);
            appearances[CreatureConstants.Griffon][GenderConstants.Male] = GetBaseFromAverage(5 * 12);
            appearances[CreatureConstants.Griffon][CreatureConstants.Griffon] = GetMultiplierFromAverage(5 * 12);
            //Source: https://www.d20srd.org/srd/monsters/sprite.htm#grig
            appearances[CreatureConstants.Grig][GenderConstants.Female] = GetBaseFromAverage(18);
            appearances[CreatureConstants.Grig][GenderConstants.Male] = GetBaseFromAverage(18);
            appearances[CreatureConstants.Grig][CreatureConstants.Grig] = GetMultiplierFromAverage(18);
            appearances[CreatureConstants.Grig_WithFiddle][GenderConstants.Female] = GetBaseFromAverage(18);
            appearances[CreatureConstants.Grig_WithFiddle][GenderConstants.Male] = GetBaseFromAverage(18);
            appearances[CreatureConstants.Grig_WithFiddle][CreatureConstants.Grig_WithFiddle] = GetMultiplierFromAverage(18);
            //Source: https://forgottenrealms.fandom.com/wiki/Grimlock
            appearances[CreatureConstants.Grimlock][GenderConstants.Female] = GetBaseFromRange(5 * 12, 5 * 12 + 6);
            appearances[CreatureConstants.Grimlock][GenderConstants.Male] = GetBaseFromRange(5 * 12, 5 * 12 + 6);
            appearances[CreatureConstants.Grimlock][CreatureConstants.Grimlock] = GetMultiplierFromRange(5 * 12, 5 * 12 + 6);
            //Source: https://www.mojobob.com/roleplay/monstrousmanual/s/sphinx.html
            appearances[CreatureConstants.Gynosphinx][GenderConstants.Female] = GetBaseFromAverage(7 * 12);
            appearances[CreatureConstants.Gynosphinx][CreatureConstants.Gynosphinx] = GetMultiplierFromAverage(7 * 12);
            //Source: https://www.d20srd.org/srd/description.htm#vitalStatistics
            appearances[CreatureConstants.Halfling_Deep][GenderConstants.Female] = "2*12+6";
            appearances[CreatureConstants.Halfling_Deep][GenderConstants.Male] = "2*12+8";
            appearances[CreatureConstants.Halfling_Deep][CreatureConstants.Halfling_Deep] = "2d4";
            appearances[CreatureConstants.Halfling_Lightfoot][GenderConstants.Female] = "2*12+6";
            appearances[CreatureConstants.Halfling_Lightfoot][GenderConstants.Male] = "2*12+8";
            appearances[CreatureConstants.Halfling_Lightfoot][CreatureConstants.Halfling_Lightfoot] = "2d4";
            appearances[CreatureConstants.Halfling_Tallfellow][GenderConstants.Female] = "3*12+6";
            appearances[CreatureConstants.Halfling_Tallfellow][GenderConstants.Male] = "3*12+8";
            appearances[CreatureConstants.Halfling_Tallfellow][CreatureConstants.Halfling_Tallfellow] = "2d4";
            //Source: https://www.5esrd.com/database/race/harpy/
            appearances[CreatureConstants.Harpy][GenderConstants.Female] = "4*12+5";
            appearances[CreatureConstants.Harpy][GenderConstants.Male] = "4*12+10";
            appearances[CreatureConstants.Harpy][CreatureConstants.Harpy] = "2d10";
            //Source: https://www.dimensions.com/element/osprey-pandion-haliaetus
            appearances[CreatureConstants.Hawk][GenderConstants.Female] = GetBaseFromRange(11, 16);
            appearances[CreatureConstants.Hawk][GenderConstants.Male] = GetBaseFromRange(11, 16);
            appearances[CreatureConstants.Hawk][CreatureConstants.Hawk] = GetMultiplierFromRange(11, 16);
            appearances[CreatureConstants.Hellcat_Bezekira][GenderConstants.Female] = "0";
            appearances[CreatureConstants.Hellcat_Bezekira][GenderConstants.Male] = "0";
            appearances[CreatureConstants.Hellcat_Bezekira][CreatureConstants.Hellcat_Bezekira] = "0";
            //Source: https://forgottenrealms.fandom.com/wiki/Hell_hound
            appearances[CreatureConstants.HellHound][GenderConstants.Female] = GetBaseFromRange(24, 48 + 6);
            appearances[CreatureConstants.HellHound][GenderConstants.Male] = GetBaseFromRange(24, 48 + 6);
            appearances[CreatureConstants.HellHound][CreatureConstants.HellHound] = GetMultiplierFromRange(24, 48 + 6);
            appearances[CreatureConstants.HellHound_NessianWarhound][GenderConstants.Female] = GetBaseFromRange(64, 72);
            appearances[CreatureConstants.HellHound_NessianWarhound][GenderConstants.Male] = GetBaseFromRange(64, 72);
            appearances[CreatureConstants.HellHound_NessianWarhound][CreatureConstants.HellHound_NessianWarhound] = GetMultiplierFromRange(64, 72);
            //Source: https://www.d20srd.org/srd/monsters/swarm.htm
            appearances[CreatureConstants.Hellwasp_Swarm][GenderConstants.Agender] = GetBaseFromAverage(10 * 12);
            appearances[CreatureConstants.Hellwasp_Swarm][CreatureConstants.Hellwasp_Swarm] = GetMultiplierFromAverage(10 * 12);
            //Source: https://www.d20srd.org/srd/monsters/demon.htm#hezrou
            appearances[CreatureConstants.Hezrou][GenderConstants.Agender] = GetBaseFromAverage(8 * 12);
            appearances[CreatureConstants.Hezrou][CreatureConstants.Hezrou] = GetMultiplierFromAverage(8 * 12);
            //Source: https://www.mojobob.com/roleplay/monstrousmanual/s/sphinx.html
            appearances[CreatureConstants.Hieracosphinx][GenderConstants.Male] = GetBaseFromAverage(7 * 12);
            appearances[CreatureConstants.Hieracosphinx][CreatureConstants.Hieracosphinx] = GetMultiplierFromAverage(7 * 12);
            appearances[CreatureConstants.Hippogriff][GenderConstants.Female] = "0";
            appearances[CreatureConstants.Hippogriff][GenderConstants.Male] = "0";
            appearances[CreatureConstants.Hippogriff][CreatureConstants.Hippogriff] = "0";
            //Source: https://forgottenrealms.fandom.com/wiki/Hobgoblin
            appearances[CreatureConstants.Hobgoblin][GenderConstants.Female] = GetBaseFromRange(5 * 12, 6 * 12);
            appearances[CreatureConstants.Hobgoblin][GenderConstants.Male] = GetBaseFromRange(5 * 12, 6 * 12);
            appearances[CreatureConstants.Hobgoblin][CreatureConstants.Hobgoblin] = GetMultiplierFromRange(5 * 12, 6 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Homunculus
            //https://www.dimensions.com/element/eastern-gray-squirrel
            appearances[CreatureConstants.Homunculus][GenderConstants.Agender] = GetBaseFromRange(4, 6);
            appearances[CreatureConstants.Homunculus][CreatureConstants.Homunculus] = GetMultiplierFromRange(4, 6);
            //Source: https://www.d20srd.org/srd/monsters/devil.htm#hornedDevilCornugon
            appearances[CreatureConstants.HornedDevil_Cornugon][GenderConstants.Agender] = GetBaseFromAverage(9 * 12);
            appearances[CreatureConstants.HornedDevil_Cornugon][CreatureConstants.HornedDevil_Cornugon] = GetMultiplierFromAverage(9 * 12);
            //Source: https://www.dimensions.com/element/clydesdale-horse
            appearances[CreatureConstants.Horse_Heavy][GenderConstants.Female] = GetBaseFromRange(64, 72);
            appearances[CreatureConstants.Horse_Heavy][GenderConstants.Male] = GetBaseFromRange(64, 72);
            appearances[CreatureConstants.Horse_Heavy][CreatureConstants.Horse_Heavy] = GetMultiplierFromRange(64, 72);
            //Source: https://www.dimensions.com/element/arabian-horse
            appearances[CreatureConstants.Horse_Light][GenderConstants.Female] = GetBaseFromRange(57, 61);
            appearances[CreatureConstants.Horse_Light][GenderConstants.Male] = GetBaseFromRange(57, 61);
            appearances[CreatureConstants.Horse_Light][CreatureConstants.Horse_Light] = GetMultiplierFromRange(57, 61);
            //Source: https://www.dimensions.com/element/clydesdale-horse
            appearances[CreatureConstants.Horse_Heavy_War][GenderConstants.Female] = GetBaseFromRange(64, 72);
            appearances[CreatureConstants.Horse_Heavy_War][GenderConstants.Male] = GetBaseFromRange(64, 72);
            appearances[CreatureConstants.Horse_Heavy_War][CreatureConstants.Horse_Heavy_War] = GetMultiplierFromRange(64, 72);
            //Source: https://www.dimensions.com/element/arabian-horse
            appearances[CreatureConstants.Horse_Light_War][GenderConstants.Female] = GetBaseFromRange(57, 61);
            appearances[CreatureConstants.Horse_Light_War][GenderConstants.Male] = GetBaseFromRange(57, 61);
            appearances[CreatureConstants.Horse_Light_War][CreatureConstants.Horse_Light_War] = GetMultiplierFromRange(57, 61);
            //Source: https://forgottenrealms.fandom.com/wiki/Hound_archon
            appearances[CreatureConstants.HoundArchon][GenderConstants.Agender] = GetBaseFromAverage(6 * 12);
            appearances[CreatureConstants.HoundArchon][CreatureConstants.HoundArchon] = GetMultiplierFromAverage(6 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Howler
            appearances[CreatureConstants.Howler][GenderConstants.Agender] = GetBaseFromAverage(8 * 12);
            appearances[CreatureConstants.Howler][CreatureConstants.Howler] = GetMultiplierFromAverage(8 * 12);
            //Source: https://www.d20srd.org/srd/description.htm#vitalStatistics
            appearances[CreatureConstants.Human][GenderConstants.Female] = "4*12+5";
            appearances[CreatureConstants.Human][GenderConstants.Male] = "4*12+10";
            appearances[CreatureConstants.Human][CreatureConstants.Human] = "2d10";
            //Source: https://forgottenrealms.fandom.com/wiki/Hydra half of length for height
            appearances[CreatureConstants.Hydra_5Heads][GenderConstants.Female] = GetBaseFromAverage(20 * 12 / 2);
            appearances[CreatureConstants.Hydra_5Heads][GenderConstants.Male] = GetBaseFromAverage(20 * 12 / 2);
            appearances[CreatureConstants.Hydra_5Heads][CreatureConstants.Hydra_5Heads] = GetMultiplierFromAverage(20 * 12 / 2);
            appearances[CreatureConstants.Hydra_6Heads][GenderConstants.Female] = GetBaseFromAverage(20 * 12 / 2);
            appearances[CreatureConstants.Hydra_6Heads][GenderConstants.Male] = GetBaseFromAverage(20 * 12 / 2);
            appearances[CreatureConstants.Hydra_6Heads][CreatureConstants.Hydra_6Heads] = GetMultiplierFromAverage(20 * 12 / 2);
            appearances[CreatureConstants.Hydra_7Heads][GenderConstants.Female] = GetBaseFromAverage(20 * 12 / 2);
            appearances[CreatureConstants.Hydra_7Heads][GenderConstants.Male] = GetBaseFromAverage(20 * 12 / 2);
            appearances[CreatureConstants.Hydra_7Heads][CreatureConstants.Hydra_7Heads] = GetMultiplierFromAverage(20 * 12 / 2);
            appearances[CreatureConstants.Hydra_8Heads][GenderConstants.Female] = GetBaseFromAverage(20 * 12 / 2);
            appearances[CreatureConstants.Hydra_8Heads][GenderConstants.Male] = GetBaseFromAverage(20 * 12 / 2);
            appearances[CreatureConstants.Hydra_8Heads][CreatureConstants.Hydra_8Heads] = GetMultiplierFromAverage(20 * 12 / 2);
            appearances[CreatureConstants.Hydra_9Heads][GenderConstants.Female] = GetBaseFromAverage(20 * 12 / 2);
            appearances[CreatureConstants.Hydra_9Heads][GenderConstants.Male] = GetBaseFromAverage(20 * 12 / 2);
            appearances[CreatureConstants.Hydra_9Heads][CreatureConstants.Hydra_9Heads] = GetMultiplierFromAverage(20 * 12 / 2);
            appearances[CreatureConstants.Hydra_10Heads][GenderConstants.Female] = GetBaseFromAverage(20 * 12 / 2);
            appearances[CreatureConstants.Hydra_10Heads][GenderConstants.Male] = GetBaseFromAverage(20 * 12 / 2);
            appearances[CreatureConstants.Hydra_10Heads][CreatureConstants.Hydra_10Heads] = GetMultiplierFromAverage(20 * 12 / 2);
            appearances[CreatureConstants.Hydra_11Heads][GenderConstants.Female] = GetBaseFromAverage(20 * 12 / 2);
            appearances[CreatureConstants.Hydra_11Heads][GenderConstants.Male] = GetBaseFromAverage(20 * 12 / 2);
            appearances[CreatureConstants.Hydra_11Heads][CreatureConstants.Hydra_11Heads] = GetMultiplierFromAverage(20 * 12 / 2);
            appearances[CreatureConstants.Hydra_12Heads][GenderConstants.Female] = GetBaseFromAverage(20 * 12 / 2);
            appearances[CreatureConstants.Hydra_12Heads][GenderConstants.Male] = GetBaseFromAverage(20 * 12 / 2);
            appearances[CreatureConstants.Hydra_12Heads][CreatureConstants.Hydra_12Heads] = GetMultiplierFromAverage(20 * 12 / 2);
            //Source: https://www.dimensions.com/element/striped-hyena-hyaena-hyaena
            appearances[CreatureConstants.Hyena][GenderConstants.Female] = GetBaseFromRange(25, 30);
            appearances[CreatureConstants.Hyena][GenderConstants.Male] = GetBaseFromRange(25, 30);
            appearances[CreatureConstants.Hyena][CreatureConstants.Hyena] = GetMultiplierFromRange(25, 30);
            //Source: https://forgottenrealms.fandom.com/wiki/Gelugon
            appearances[CreatureConstants.IceDevil_Gelugon][GenderConstants.Agender] = GetBaseFromRange(10 * 12 + 6, 12 * 12);
            appearances[CreatureConstants.IceDevil_Gelugon][CreatureConstants.IceDevil_Gelugon] = GetMultiplierFromRange(10 * 12 + 6, 12 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Imp
            appearances[CreatureConstants.Imp][GenderConstants.Agender] = GetBaseFromAverage(2 * 12);
            appearances[CreatureConstants.Imp][GenderConstants.Female] = GetBaseFromAverage(2 * 12);
            appearances[CreatureConstants.Imp][GenderConstants.Male] = GetBaseFromAverage(2 * 12);
            appearances[CreatureConstants.Imp][CreatureConstants.Imp] = GetMultiplierFromAverage(2 * 12);
            //Source: https://www.d20srd.org/srd/monsters/invisibleStalker.htm
            //https://www.d20srd.org/srd/combat/movementPositionAndDistance.htm using generic Large, since actual form is unknown
            appearances[CreatureConstants.InvisibleStalker][GenderConstants.Agender] = GetBaseFromRange(8 * 12, 16 * 12);
            appearances[CreatureConstants.InvisibleStalker][CreatureConstants.InvisibleStalker] = GetMultiplierFromRange(8 * 12, 16 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Janni
            appearances[CreatureConstants.Janni][GenderConstants.Agender] = GetBaseFromRange(6 * 12, 7 * 12);
            appearances[CreatureConstants.Janni][GenderConstants.Female] = GetBaseFromRange(6 * 12, 7 * 12);
            appearances[CreatureConstants.Janni][GenderConstants.Male] = GetBaseFromRange(6 * 12, 7 * 12);
            appearances[CreatureConstants.Janni][CreatureConstants.Janni] = GetMultiplierFromRange(6 * 12, 7 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Kobold
            appearances[CreatureConstants.Kobold][GenderConstants.Female] = GetBaseFromRange(2 * 12, 2 * 12 + 6);
            appearances[CreatureConstants.Kobold][GenderConstants.Male] = GetBaseFromRange(2 * 12, 2 * 12 + 6);
            appearances[CreatureConstants.Kobold][CreatureConstants.Kobold] = GetMultiplierFromRange(2 * 12, 2 * 12 + 6);
            //Source: https://pathfinderwiki.com/wiki/Kolyarut
            //Can't find definitive height, but "size of a tall humanoid", so using human + some
            appearances[CreatureConstants.Kolyarut][GenderConstants.Agender] = "5*12";
            appearances[CreatureConstants.Kolyarut][CreatureConstants.Kolyarut] = "2d12";
            //Source: https://forgottenrealms.fandom.com/wiki/Kraken
            appearances[CreatureConstants.Kraken][GenderConstants.Female] = GetBaseFromAverage(30 * 12);
            appearances[CreatureConstants.Kraken][GenderConstants.Male] = GetBaseFromAverage(30 * 12);
            appearances[CreatureConstants.Kraken][CreatureConstants.Kraken] = GetMultiplierFromAverage(30 * 12);
            //Source: https://www.d20srd.org/srd/monsters/krenshar.htm
            appearances[CreatureConstants.Krenshar][GenderConstants.Female] = "0";
            appearances[CreatureConstants.Krenshar][GenderConstants.Male] = "0";
            appearances[CreatureConstants.Krenshar][CreatureConstants.Krenshar] = "0";
            //Source: https://forgottenrealms.fandom.com/wiki/Kuo-toa
            appearances[CreatureConstants.KuoToa][GenderConstants.Female] = GetBaseFromAverage(5 * 12);
            appearances[CreatureConstants.KuoToa][GenderConstants.Male] = GetBaseFromAverage(5 * 12);
            appearances[CreatureConstants.KuoToa][CreatureConstants.KuoToa] = GetMultiplierFromAverage(5 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Lamia
            appearances[CreatureConstants.Lamia][GenderConstants.Female] = GetBaseFromAverage(6 * 12);
            appearances[CreatureConstants.Lamia][GenderConstants.Male] = GetBaseFromAverage(6 * 12);
            appearances[CreatureConstants.Lamia][CreatureConstants.Lamia] = GetMultiplierFromAverage(6 * 12);
            //Source: https://www.d20srd.org/srd/monsters/lammasu.htm
            //https://www.dimensions.com/element/african-lion scale up from lion: [44,50]*8*12/[54,78] = [78,62]
            appearances[CreatureConstants.Lammasu][GenderConstants.Female] = GetBaseFromRange(62, 78);
            appearances[CreatureConstants.Lammasu][GenderConstants.Male] = GetBaseFromRange(62, 78);
            appearances[CreatureConstants.Lammasu][CreatureConstants.Lammasu] = GetMultiplierFromRange(62, 78);
            //Source: https://forgottenrealms.fandom.com/wiki/Lantern_archon
            appearances[CreatureConstants.LanternArchon][GenderConstants.Agender] = GetBaseFromRange(12, 36);
            appearances[CreatureConstants.LanternArchon][CreatureConstants.LanternArchon] = GetMultiplierFromRange(12, 36);
            //Source: https://forgottenrealms.fandom.com/wiki/Lemure
            appearances[CreatureConstants.Lemure][GenderConstants.Agender] = GetBaseFromAverage(5 * 12);
            appearances[CreatureConstants.Lemure][CreatureConstants.Lemure] = GetMultiplierFromAverage(5 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Leonal
            appearances[CreatureConstants.Leonal][GenderConstants.Female] = GetBaseFromAverage(6 * 12);
            appearances[CreatureConstants.Leonal][GenderConstants.Male] = GetBaseFromAverage(6 * 12);
            appearances[CreatureConstants.Leonal][CreatureConstants.Leonal] = GetMultiplierFromAverage(6 * 12);
            //Source: https://www.dimensions.com/element/cougar
            appearances[CreatureConstants.Leopard][GenderConstants.Female] = GetBaseFromRange(21, 28);
            appearances[CreatureConstants.Leopard][GenderConstants.Male] = GetBaseFromRange(21, 28);
            appearances[CreatureConstants.Leopard][CreatureConstants.Leopard] = GetMultiplierFromRange(21, 28);
            //Using Human
            appearances[CreatureConstants.Lillend][GenderConstants.Female] = "4*12+5";
            appearances[CreatureConstants.Lillend][GenderConstants.Male] = "4*12+10";
            appearances[CreatureConstants.Lillend][CreatureConstants.Lillend] = "2d10";
            //Source: https://www.dimensions.com/element/african-lion
            appearances[CreatureConstants.Lion][GenderConstants.Female] = GetBaseFromAverage(2 * 12 + 10, 3 * 12 + 4);
            appearances[CreatureConstants.Lion][GenderConstants.Male] = GetBaseFromAverage(3 * 12 + 4);
            appearances[CreatureConstants.Lion][CreatureConstants.Lion] = GetMultiplierFromAverage(3 * 12 + 4);
            //Scaling up from lion, x3 based on length. Since length doesn't differ for male/female, only use male
            appearances[CreatureConstants.Lion_Dire][GenderConstants.Female] = GetBaseFromAverage((3 * 12 + 4) * 3);
            appearances[CreatureConstants.Lion_Dire][GenderConstants.Male] = GetBaseFromAverage((3 * 12 + 4) * 3);
            appearances[CreatureConstants.Lion_Dire][CreatureConstants.Lion_Dire] = GetMultiplierFromAverage((3 * 12 + 4) * 3);
            //Source: https://www.dimensions.com/element/green-iguana-iguana-iguana
            appearances[CreatureConstants.Lizard][GenderConstants.Female] = GetBaseFromRange(1, 2);
            appearances[CreatureConstants.Lizard][GenderConstants.Male] = GetBaseFromRange(1, 2);
            appearances[CreatureConstants.Lizard][CreatureConstants.Lizard] = GetMultiplierFromRange(1, 2);
            //Source: https://www.dimensions.com/element/savannah-monitor-varanus-exanthematicus
            appearances[CreatureConstants.Lizard_Monitor][GenderConstants.Female] = GetBaseFromRange(7, 8);
            appearances[CreatureConstants.Lizard_Monitor][GenderConstants.Male] = GetBaseFromRange(7, 8);
            appearances[CreatureConstants.Lizard_Monitor][CreatureConstants.Lizard_Monitor] = GetMultiplierFromRange(7, 8);
            //Source: https://forgottenrealms.fandom.com/wiki/Lizardfolk
            appearances[CreatureConstants.Lizardfolk][GenderConstants.Female] = GetBaseFromRange(6 * 12, 7 * 12);
            appearances[CreatureConstants.Lizardfolk][GenderConstants.Male] = GetBaseFromRange(6 * 12, 7 * 12);
            appearances[CreatureConstants.Lizardfolk][CreatureConstants.Lizardfolk] = GetMultiplierFromRange(6 * 12, 7 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Locathah
            appearances[CreatureConstants.Locathah][GenderConstants.Female] = GetBaseFromAverage(5 * 12);
            appearances[CreatureConstants.Locathah][GenderConstants.Male] = GetBaseFromAverage(5 * 12);
            appearances[CreatureConstants.Locathah][CreatureConstants.Locathah] = GetMultiplierFromAverage(5 * 12);
            //Source: https://www.d20srd.org/srd/monsters/swarm.htm
            appearances[CreatureConstants.Locust_Swarm][GenderConstants.Agender] = GetBaseFromAverage(10 * 12);
            appearances[CreatureConstants.Locust_Swarm][CreatureConstants.Locust_Swarm] = GetMultiplierFromAverage(10 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Magmin
            appearances[CreatureConstants.Magmin][GenderConstants.Agender] = GetBaseFromRange(3 * 12, 4 * 12);
            appearances[CreatureConstants.Magmin][CreatureConstants.Magmin] = GetMultiplierFromRange(3 * 12, 4 * 12);
            //Source: https://www.dimensions.com/element/reef-manta-ray-mobula-alfredi
            appearances[CreatureConstants.MantaRay][GenderConstants.Female] = "0";
            appearances[CreatureConstants.MantaRay][GenderConstants.Male] = "0";
            appearances[CreatureConstants.MantaRay][CreatureConstants.MantaRay] = "0";
            //Source: https://forgottenrealms.fandom.com/wiki/Manticore Using Lion for height
            appearances[CreatureConstants.Manticore][GenderConstants.Female] = GetBaseFromAverage(2 * 12 + 10, 3 * 12 + 4);
            appearances[CreatureConstants.Manticore][GenderConstants.Male] = GetBaseFromAverage(3 * 12 + 4);
            appearances[CreatureConstants.Manticore][CreatureConstants.Manticore] = GetMultiplierFromAverage(3 * 12 + 4);
            //Source: https://forgottenrealms.fandom.com/wiki/Marilith
            appearances[CreatureConstants.Marilith][GenderConstants.Female] = GetBaseFromRange(7 * 12, 9 * 12);
            appearances[CreatureConstants.Marilith][CreatureConstants.Marilith] = GetMultiplierFromRange(7 * 12, 9 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Marut
            appearances[CreatureConstants.Marut][GenderConstants.Agender] = GetBaseFromAverage(12 * 12);
            appearances[CreatureConstants.Marut][CreatureConstants.Marut] = GetMultiplierFromAverage(12 * 12);
            //Source: https://www.d20srd.org/srd/monsters/medusa.htm
            appearances[CreatureConstants.Medusa][GenderConstants.Female] = GetBaseFromRange(5 * 12, 6 * 12);
            appearances[CreatureConstants.Medusa][GenderConstants.Male] = GetBaseFromRange(5 * 12, 6 * 12);
            appearances[CreatureConstants.Medusa][CreatureConstants.Medusa] = GetMultiplierFromRange(5 * 12, 6 * 12);
            //Source: https://jurassicworld-evolution.fandom.com/wiki/Velociraptor
            appearances[CreatureConstants.Megaraptor][GenderConstants.Female] = GetBaseFromAverage(67);
            appearances[CreatureConstants.Megaraptor][GenderConstants.Male] = GetBaseFromAverage(67);
            appearances[CreatureConstants.Megaraptor][CreatureConstants.Megaraptor] = GetMultiplierFromAverage(67);
            //Source: https://forgottenrealms.fandom.com/wiki/Mephit
            appearances[CreatureConstants.Mephit_Air][GenderConstants.Agender] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.Mephit_Air][GenderConstants.Female] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.Mephit_Air][GenderConstants.Male] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.Mephit_Air][CreatureConstants.Mephit_Air] = GetMultiplierFromAverage(4 * 12);
            appearances[CreatureConstants.Mephit_Dust][GenderConstants.Agender] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.Mephit_Dust][GenderConstants.Female] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.Mephit_Dust][GenderConstants.Male] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.Mephit_Dust][CreatureConstants.Mephit_Dust] = GetMultiplierFromAverage(4 * 12);
            appearances[CreatureConstants.Mephit_Earth][GenderConstants.Agender] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.Mephit_Earth][GenderConstants.Female] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.Mephit_Earth][GenderConstants.Male] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.Mephit_Earth][CreatureConstants.Mephit_Earth] = GetMultiplierFromAverage(4 * 12);
            appearances[CreatureConstants.Mephit_Fire][GenderConstants.Agender] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.Mephit_Fire][GenderConstants.Female] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.Mephit_Fire][GenderConstants.Male] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.Mephit_Fire][CreatureConstants.Mephit_Fire] = GetMultiplierFromAverage(4 * 12);
            appearances[CreatureConstants.Mephit_Ice][GenderConstants.Agender] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.Mephit_Ice][GenderConstants.Female] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.Mephit_Ice][GenderConstants.Male] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.Mephit_Ice][CreatureConstants.Mephit_Ice] = GetMultiplierFromAverage(4 * 12);
            appearances[CreatureConstants.Mephit_Magma][GenderConstants.Agender] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.Mephit_Magma][GenderConstants.Female] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.Mephit_Magma][GenderConstants.Male] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.Mephit_Magma][CreatureConstants.Mephit_Magma] = GetMultiplierFromAverage(4 * 12);
            appearances[CreatureConstants.Mephit_Ooze][GenderConstants.Agender] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.Mephit_Ooze][GenderConstants.Female] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.Mephit_Ooze][GenderConstants.Male] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.Mephit_Ooze][CreatureConstants.Mephit_Ooze] = GetMultiplierFromAverage(4 * 12);
            appearances[CreatureConstants.Mephit_Salt][GenderConstants.Agender] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.Mephit_Salt][GenderConstants.Female] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.Mephit_Salt][GenderConstants.Male] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.Mephit_Salt][CreatureConstants.Mephit_Salt] = GetMultiplierFromAverage(4 * 12);
            appearances[CreatureConstants.Mephit_Steam][GenderConstants.Agender] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.Mephit_Steam][GenderConstants.Female] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.Mephit_Steam][GenderConstants.Male] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.Mephit_Steam][CreatureConstants.Mephit_Steam] = GetMultiplierFromAverage(4 * 12);
            appearances[CreatureConstants.Mephit_Water][GenderConstants.Agender] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.Mephit_Water][GenderConstants.Female] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.Mephit_Water][GenderConstants.Male] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.Mephit_Water][CreatureConstants.Mephit_Water] = GetMultiplierFromAverage(4 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Merfolk
            appearances[CreatureConstants.Merfolk][GenderConstants.Female] = "0";
            appearances[CreatureConstants.Merfolk][GenderConstants.Male] = "0";
            appearances[CreatureConstants.Merfolk][CreatureConstants.Merfolk] = "0";
            //Source: https://www.d20srd.org/srd/monsters/mimic.htm
            appearances[CreatureConstants.Mimic][GenderConstants.Agender] = GetBaseFromRange(3 * 12, 10 * 12);
            appearances[CreatureConstants.Mimic][CreatureConstants.Mimic] = GetMultiplierFromRange(3 * 12, 10 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Mind_flayer
            appearances[CreatureConstants.MindFlayer][GenderConstants.Agender] = GetBaseFromAverage(6 * 12);
            appearances[CreatureConstants.MindFlayer][CreatureConstants.MindFlayer] = GetMultiplierFromAverage(6 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Minotaur
            appearances[CreatureConstants.Minotaur][GenderConstants.Female] = GetBaseFromAverage(7 * 12, 9 * 12);
            appearances[CreatureConstants.Minotaur][GenderConstants.Male] = GetBaseFromAverage(9 * 12);
            appearances[CreatureConstants.Minotaur][CreatureConstants.Minotaur] = GetMultiplierFromAverage(9 * 12);
            //Source: https://www.d20srd.org/srd/monsters/mohrg.htm
            appearances[CreatureConstants.Mohrg][GenderConstants.Agender] = GetBaseFromRange(5 * 12, 6 * 12);
            appearances[CreatureConstants.Mohrg][CreatureConstants.Mohrg] = GetMultiplierFromRange(5 * 12, 6 * 12);
            //Source: https://www.dimensions.com/element/tufted-capuchin-sapajus-apella
            appearances[CreatureConstants.Monkey][GenderConstants.Female] = GetBaseFromRange(10, 16);
            appearances[CreatureConstants.Monkey][GenderConstants.Male] = GetBaseFromRange(10, 16);
            appearances[CreatureConstants.Monkey][CreatureConstants.Monkey] = GetMultiplierFromRange(10, 16);
            //Source: https://www.dimensions.com/element/mule-equus-asinus-x-equus-caballus
            appearances[CreatureConstants.Mule][GenderConstants.Female] = GetBaseFromRange(56, 68);
            appearances[CreatureConstants.Mule][GenderConstants.Male] = GetBaseFromRange(56, 68);
            appearances[CreatureConstants.Mule][CreatureConstants.Mule] = GetMultiplierFromRange(56, 68);
            //Source: https://www.d20srd.org/srd/monsters/mummy.htm
            appearances[CreatureConstants.Mummy][GenderConstants.Female] = GetBaseFromRange(5 * 12, 6 * 12);
            appearances[CreatureConstants.Mummy][GenderConstants.Male] = GetBaseFromRange(5 * 12, 6 * 12);
            appearances[CreatureConstants.Mummy][CreatureConstants.Mummy] = GetMultiplierFromRange(5 * 12, 6 * 12);
            appearances[CreatureConstants.Naga_Dark][GenderConstants.Hermaphrodite] = "0";
            appearances[CreatureConstants.Naga_Dark][CreatureConstants.Naga_Dark] = "0";
            appearances[CreatureConstants.Naga_Guardian][GenderConstants.Hermaphrodite] = "0";
            appearances[CreatureConstants.Naga_Guardian][CreatureConstants.Naga_Guardian] = "0";
            appearances[CreatureConstants.Naga_Spirit][GenderConstants.Hermaphrodite] = "0";
            appearances[CreatureConstants.Naga_Spirit][CreatureConstants.Naga_Spirit] = "0";
            appearances[CreatureConstants.Naga_Water][GenderConstants.Hermaphrodite] = "0";
            appearances[CreatureConstants.Naga_Water][CreatureConstants.Naga_Water] = "0";
            //Source: https://forgottenrealms.fandom.com/wiki/Nalfeshnee
            appearances[CreatureConstants.Nalfeshnee][GenderConstants.Agender] = GetBaseFromRange(10 * 12, 20 * 12);
            appearances[CreatureConstants.Nalfeshnee][CreatureConstants.Nalfeshnee] = GetMultiplierFromRange(10 * 12, 20 * 12);
            //Source: https://www.d20srd.org/srd/monsters/nightHag.htm Copy from Human
            appearances[CreatureConstants.NightHag][GenderConstants.Female] = "4*12+5";
            appearances[CreatureConstants.NightHag][CreatureConstants.NightHag] = "2d10";
            //Source: https://www.d20srd.org/srd/monsters/nightshade.htm
            appearances[CreatureConstants.Nightcrawler][GenderConstants.Agender] = GetBaseFromAverage(7 * 12);
            appearances[CreatureConstants.Nightcrawler][CreatureConstants.Nightcrawler] = GetMultiplierFromAverage(7 * 12);
            //Source: https://www.d20srd.org/srd/monsters/nightmare.htm
            appearances[CreatureConstants.Nightmare][GenderConstants.Agender] = GetBaseFromRange(57, 61);
            appearances[CreatureConstants.Nightmare][CreatureConstants.Nightmare] = GetMultiplierFromRange(57, 61);
            //Scale up x2
            appearances[CreatureConstants.Nightmare_Cauchemar][GenderConstants.Agender] = GetBaseFromRange(57 * 2, 61 * 2);
            appearances[CreatureConstants.Nightmare_Cauchemar][CreatureConstants.Nightmare_Cauchemar] = GetMultiplierFromRange(57 * 2, 61 * 2);
            //Source: https://www.d20srd.org/srd/monsters/nightshade.htm
            appearances[CreatureConstants.Nightwalker][GenderConstants.Agender] = GetBaseFromAverage(20 * 12);
            appearances[CreatureConstants.Nightwalker][CreatureConstants.Nightwalker] = GetMultiplierFromAverage(20 * 12);
            //Source: https://www.d20srd.org/srd/monsters/nightshade.htm
            //https://www.dimensions.com/element/giant-golden-crowned-flying-fox-acerodon-jubatus scaled up: [18,22]*40*12/[59,67] = [146,158]
            appearances[CreatureConstants.Nightwing][GenderConstants.Agender] = GetBaseFromRange(146, 158);
            appearances[CreatureConstants.Nightwing][CreatureConstants.Nightwing] = GetMultiplierFromRange(146, 158);
            //Source: https://www.d20srd.org/srd/monsters/sprite.htm#nixie
            appearances[CreatureConstants.Nixie][GenderConstants.Female] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.Nixie][GenderConstants.Male] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.Nixie][CreatureConstants.Nixie] = GetMultiplierFromAverage(4 * 12);
            //Source: https://www.d20srd.org/srd/monsters/nymph.htm Copy from High Elf
            appearances[CreatureConstants.Nymph][GenderConstants.Female] = "4*12+5";
            appearances[CreatureConstants.Nymph][CreatureConstants.Nymph] = "2d6";
            //Source: https://www.d20srd.org/srd/monsters/ooze.htm#ochreJelly
            appearances[CreatureConstants.OchreJelly][GenderConstants.Agender] = GetBaseFromAverage(6);
            appearances[CreatureConstants.OchreJelly][CreatureConstants.OchreJelly] = GetMultiplierFromAverage(6);
            //Source: https://www.dimensions.com/element/common-octopus-octopus-vulgaris (mantle length)
            appearances[CreatureConstants.Octopus][GenderConstants.Female] = GetBaseFromRange(6, 10);
            appearances[CreatureConstants.Octopus][GenderConstants.Male] = GetBaseFromRange(6, 10);
            appearances[CreatureConstants.Octopus][CreatureConstants.Octopus] = GetMultiplierFromRange(6, 10);
            //Source: https://www.dimensions.com/element/giant-pacific-octopus-enteroctopus-dofleini (mantle length)
            appearances[CreatureConstants.Octopus_Giant][GenderConstants.Female] = GetBaseFromRange(20, 24);
            appearances[CreatureConstants.Octopus_Giant][GenderConstants.Male] = GetBaseFromRange(20, 24);
            appearances[CreatureConstants.Octopus_Giant][CreatureConstants.Octopus_Giant] = GetMultiplierFromRange(20, 24);
            //Source: https://forgottenrealms.fandom.com/wiki/Ogre
            appearances[CreatureConstants.Ogre][GenderConstants.Female] = GetBaseFromRange(9 * 12 + 3, 10 * 12);
            appearances[CreatureConstants.Ogre][GenderConstants.Male] = GetBaseFromRange(10 * 12 + 1, 10 * 12 + 10);
            appearances[CreatureConstants.Ogre][CreatureConstants.Ogre] = GetMultiplierFromRange(10 * 12 + 1, 10 * 12 + 10);
            appearances[CreatureConstants.Ogre_Merrow][GenderConstants.Female] = GetBaseFromRange(9 * 12 + 3, 10 * 12);
            appearances[CreatureConstants.Ogre_Merrow][GenderConstants.Male] = GetBaseFromRange(10 * 12 + 1, 10 * 12 + 10);
            appearances[CreatureConstants.Ogre_Merrow][CreatureConstants.Ogre_Merrow] = GetMultiplierFromRange(10 * 12 + 1, 10 * 12 + 10);
            //Source: https://forgottenrealms.fandom.com/wiki/Oni_mage
            appearances[CreatureConstants.OgreMage][GenderConstants.Female] = GetBaseFromAverage(10 * 12);
            appearances[CreatureConstants.OgreMage][GenderConstants.Male] = GetBaseFromAverage(10 * 12);
            appearances[CreatureConstants.OgreMage][CreatureConstants.OgreMage] = GetMultiplierFromAverage(10 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Orc
            appearances[CreatureConstants.Orc][GenderConstants.Female] = GetBaseFromAtLeast(6 * 12);
            appearances[CreatureConstants.Orc][GenderConstants.Male] = GetBaseFromAtLeast(6 * 12);
            appearances[CreatureConstants.Orc][CreatureConstants.Orc] = GetMultiplierFromAtLeast(6 * 12);
            //Source: https://www.d20srd.org/srd/description.htm#vitalStatistics
            appearances[CreatureConstants.Orc_Half][GenderConstants.Female] = "4*12+5";
            appearances[CreatureConstants.Orc_Half][GenderConstants.Male] = "4*12+10";
            appearances[CreatureConstants.Orc_Half][CreatureConstants.Orc_Half] = "2d12";
            //Source: https://criticalrole.fandom.com/wiki/Otyugh
            appearances[CreatureConstants.Otyugh][GenderConstants.Hermaphrodite] = GetBaseFromAverage(15 * 12);
            appearances[CreatureConstants.Otyugh][CreatureConstants.Otyugh] = GetMultiplierFromAverage(15 * 12);
            //Source: https://www.dimensions.com/element/great-horned-owl-bubo-virginianus
            appearances[CreatureConstants.Owl][GenderConstants.Female] = GetBaseFromRange(9, 14);
            appearances[CreatureConstants.Owl][GenderConstants.Male] = GetBaseFromRange(9, 14);
            appearances[CreatureConstants.Owl][CreatureConstants.Owl] = GetMultiplierFromRange(9, 14);
            //Source: https://www.d20srd.org/srd/monsters/owlGiant.htm 
            appearances[CreatureConstants.Owl_Giant][GenderConstants.Female] = GetBaseFromAverage(9 * 12);
            appearances[CreatureConstants.Owl_Giant][GenderConstants.Male] = GetBaseFromAverage(9 * 12);
            appearances[CreatureConstants.Owl_Giant][CreatureConstants.Owl_Giant] = GetMultiplierFromAverage(9 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Owlbear
            //https://www.dimensions.com/element/polar-bears polar bears are similar length, so using for height
            //Adjusting female max +5" to match range
            appearances[CreatureConstants.Owlbear][GenderConstants.Female] = GetBaseFromRange(2 * 12 + 8, 3 * 12 + 11 + 5);
            appearances[CreatureConstants.Owlbear][GenderConstants.Male] = GetBaseFromRange(3 * 12 + 7, 5 * 12 + 3);
            appearances[CreatureConstants.Owlbear][CreatureConstants.Owlbear] = GetMultiplierFromRange(3 * 12 + 7, 5 * 12 + 3);
            //Source: https://www.d20srd.org/srd/monsters/pegasus.htm
            appearances[CreatureConstants.Pegasus][GenderConstants.Female] = GetBaseFromAverage(6 * 12);
            appearances[CreatureConstants.Pegasus][GenderConstants.Male] = GetBaseFromAverage(6 * 12);
            appearances[CreatureConstants.Pegasus][CreatureConstants.Pegasus] = GetMultiplierFromAverage(6 * 12);
            //Source: https://www.d20pfsrd.com/bestiary/monster-listings/plants/fungus-phantom/
            appearances[CreatureConstants.PhantomFungus][GenderConstants.Agender] = GetBaseFromAverage(6 * 12);
            appearances[CreatureConstants.PhantomFungus][CreatureConstants.PhantomFungus] = GetMultiplierFromAverage(6 * 12);
            //Source: https://www.d20srd.org/srd/monsters/phaseSpider.htm
            //Source: https://www.realmshelps.net/monsters/block/Monstrous_Spider,_Medium
            //Source: https://www.realmshelps.net/monsters/block/Monstrous_Spider,_Large - going between, so 12 inches
            appearances[CreatureConstants.PhaseSpider][GenderConstants.Female] = GetBaseFromAverage(12);
            appearances[CreatureConstants.PhaseSpider][GenderConstants.Male] = GetBaseFromAverage(12);
            appearances[CreatureConstants.PhaseSpider][CreatureConstants.PhaseSpider] = GetMultiplierFromAverage(12);
            //Source: https://www.d20srd.org/srd/monsters/phasm.htm
            appearances[CreatureConstants.Phasm][GenderConstants.Agender] = GetBaseFromAverage(2 * 12);
            appearances[CreatureConstants.Phasm][CreatureConstants.Phasm] = GetMultiplierFromAverage(2 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Pit_fiend
            appearances[CreatureConstants.PitFiend][GenderConstants.Agender] = GetBaseFromAverage(12 * 12);
            appearances[CreatureConstants.PitFiend][CreatureConstants.PitFiend] = GetMultiplierFromAverage(12 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Pixie
            appearances[CreatureConstants.Pixie][GenderConstants.Female] = GetBaseFromRange(12, 30);
            appearances[CreatureConstants.Pixie][GenderConstants.Male] = GetBaseFromRange(12, 30);
            appearances[CreatureConstants.Pixie][CreatureConstants.Pixie] = GetMultiplierFromRange(12, 30);
            appearances[CreatureConstants.Pixie_WithIrresistibleDance][GenderConstants.Female] = GetBaseFromRange(12, 30);
            appearances[CreatureConstants.Pixie_WithIrresistibleDance][GenderConstants.Male] = GetBaseFromRange(12, 30);
            appearances[CreatureConstants.Pixie_WithIrresistibleDance][CreatureConstants.Pixie_WithIrresistibleDance] = GetMultiplierFromRange(12, 30);
            //Source: https://www.dimensions.com/element/shetland-pony
            appearances[CreatureConstants.Pony][GenderConstants.Female] = GetBaseFromRange(28, 44);
            appearances[CreatureConstants.Pony][GenderConstants.Male] = GetBaseFromRange(28, 44);
            appearances[CreatureConstants.Pony][CreatureConstants.Pony] = GetMultiplierFromRange(28, 44);
            appearances[CreatureConstants.Pony_War][GenderConstants.Female] = GetBaseFromRange(28, 44);
            appearances[CreatureConstants.Pony_War][GenderConstants.Male] = GetBaseFromRange(28, 44);
            appearances[CreatureConstants.Pony_War][CreatureConstants.Pony_War] = GetMultiplierFromRange(28, 44);
            //Source: https://www.dimensions.com/element/harbour-porpoise-phocoena-phocoena
            appearances[CreatureConstants.Porpoise][GenderConstants.Female] = GetBaseFromRange(14, 16);
            appearances[CreatureConstants.Porpoise][GenderConstants.Male] = GetBaseFromRange(14, 16);
            appearances[CreatureConstants.Porpoise][CreatureConstants.Porpoise] = GetMultiplierFromRange(14, 16);
            //Source: https://forgottenrealms.fandom.com/wiki/Giant_praying_mantis
            appearances[CreatureConstants.PrayingMantis_Giant][GenderConstants.Female] = GetBaseFromRange(2 * 12, 5 * 12);
            appearances[CreatureConstants.PrayingMantis_Giant][GenderConstants.Male] = GetBaseFromRange(2 * 12, 5 * 12);
            appearances[CreatureConstants.PrayingMantis_Giant][CreatureConstants.PrayingMantis_Giant] = GetMultiplierFromRange(2 * 12, 5 * 12);
            //Source: https://www.d20srd.org/srd/monsters/pseudodragon.htm
            //Scale from Red Dragon Wyrmling: 48*3/16 = 9
            appearances[CreatureConstants.Pseudodragon][GenderConstants.Female] = GetBaseFromAverage(9);
            appearances[CreatureConstants.Pseudodragon][GenderConstants.Male] = GetBaseFromAverage(9);
            appearances[CreatureConstants.Pseudodragon][CreatureConstants.Pseudodragon] = GetMultiplierFromAverage(9);
            //Source: https://www.d20srd.org/srd/monsters/purpleWorm.htm
            appearances[CreatureConstants.PurpleWorm][GenderConstants.Female] = GetBaseFromAverage(5 * 12);
            appearances[CreatureConstants.PurpleWorm][GenderConstants.Male] = GetBaseFromAverage(5 * 12);
            appearances[CreatureConstants.PurpleWorm][CreatureConstants.PurpleWorm] = GetMultiplierFromAverage(5 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Hydra half of length for height
            appearances[CreatureConstants.Pyrohydra_5Heads][GenderConstants.Female] = GetBaseFromAverage(20 * 12 / 2);
            appearances[CreatureConstants.Pyrohydra_5Heads][GenderConstants.Male] = GetBaseFromAverage(20 * 12 / 2);
            appearances[CreatureConstants.Pyrohydra_5Heads][CreatureConstants.Pyrohydra_5Heads] = GetMultiplierFromAverage(20 * 12 / 2);
            appearances[CreatureConstants.Pyrohydra_6Heads][GenderConstants.Female] = GetBaseFromAverage(20 * 12 / 2);
            appearances[CreatureConstants.Pyrohydra_6Heads][GenderConstants.Male] = GetBaseFromAverage(20 * 12 / 2);
            appearances[CreatureConstants.Pyrohydra_6Heads][CreatureConstants.Pyrohydra_6Heads] = GetMultiplierFromAverage(20 * 12 / 2);
            appearances[CreatureConstants.Pyrohydra_7Heads][GenderConstants.Female] = GetBaseFromAverage(20 * 12 / 2);
            appearances[CreatureConstants.Pyrohydra_7Heads][GenderConstants.Male] = GetBaseFromAverage(20 * 12 / 2);
            appearances[CreatureConstants.Pyrohydra_7Heads][CreatureConstants.Pyrohydra_7Heads] = GetMultiplierFromAverage(20 * 12 / 2);
            appearances[CreatureConstants.Pyrohydra_8Heads][GenderConstants.Female] = GetBaseFromAverage(20 * 12 / 2);
            appearances[CreatureConstants.Pyrohydra_8Heads][GenderConstants.Male] = GetBaseFromAverage(20 * 12 / 2);
            appearances[CreatureConstants.Pyrohydra_8Heads][CreatureConstants.Pyrohydra_8Heads] = GetMultiplierFromAverage(20 * 12 / 2);
            appearances[CreatureConstants.Pyrohydra_9Heads][GenderConstants.Female] = GetBaseFromAverage(20 * 12 / 2);
            appearances[CreatureConstants.Pyrohydra_9Heads][GenderConstants.Male] = GetBaseFromAverage(20 * 12 / 2);
            appearances[CreatureConstants.Pyrohydra_9Heads][CreatureConstants.Pyrohydra_9Heads] = GetMultiplierFromAverage(20 * 12 / 2);
            appearances[CreatureConstants.Pyrohydra_10Heads][GenderConstants.Female] = GetBaseFromAverage(20 * 12 / 2);
            appearances[CreatureConstants.Pyrohydra_10Heads][GenderConstants.Male] = GetBaseFromAverage(20 * 12 / 2);
            appearances[CreatureConstants.Pyrohydra_10Heads][CreatureConstants.Pyrohydra_10Heads] = GetMultiplierFromAverage(20 * 12 / 2);
            appearances[CreatureConstants.Pyrohydra_11Heads][GenderConstants.Female] = GetBaseFromAverage(20 * 12 / 2);
            appearances[CreatureConstants.Pyrohydra_11Heads][GenderConstants.Male] = GetBaseFromAverage(20 * 12 / 2);
            appearances[CreatureConstants.Pyrohydra_11Heads][CreatureConstants.Pyrohydra_11Heads] = GetMultiplierFromAverage(20 * 12 / 2);
            appearances[CreatureConstants.Pyrohydra_12Heads][GenderConstants.Female] = GetBaseFromAverage(20 * 12 / 2);
            appearances[CreatureConstants.Pyrohydra_12Heads][GenderConstants.Male] = GetBaseFromAverage(20 * 12 / 2);
            appearances[CreatureConstants.Pyrohydra_12Heads][CreatureConstants.Pyrohydra_12Heads] = GetMultiplierFromAverage(20 * 12 / 2);
            //Source: https://forgottenrealms.fandom.com/wiki/Quasit
            appearances[CreatureConstants.Quasit][GenderConstants.Agender] = GetBaseFromRange(12, 24);
            appearances[CreatureConstants.Quasit][CreatureConstants.Quasit] = GetMultiplierFromRange(12, 24);
            //Source: https://www.d20srd.org/srd/monsters/rakshasa.htm Copy from Human
            appearances[CreatureConstants.Rakshasa][GenderConstants.Female] = "4*12+5";
            appearances[CreatureConstants.Rakshasa][GenderConstants.Male] = "4*12+10";
            appearances[CreatureConstants.Rakshasa][CreatureConstants.Rakshasa] = "2d10";
            //Source: https://www.d20srd.org/srd/monsters/rast.htm - body size of "large dog", head almost as large as body, so Riding Dog height x2
            appearances[CreatureConstants.Rast][GenderConstants.Agender] = GetBaseFromRange(22 * 2, 30 * 2);
            appearances[CreatureConstants.Rast][CreatureConstants.Rast] = GetMultiplierFromRange(22 * 2, 30 * 2);
            //Source: https://www.dimensions.com/element/common-rat
            appearances[CreatureConstants.Rat][GenderConstants.Female] = GetBaseFromRange(2, 4);
            appearances[CreatureConstants.Rat][GenderConstants.Male] = GetBaseFromRange(2, 4);
            appearances[CreatureConstants.Rat][CreatureConstants.Rat] = GetMultiplierFromRange(2, 4);
            //Scaled up from Rat, x6 based on length
            appearances[CreatureConstants.Rat_Dire][GenderConstants.Female] = GetBaseFromRange(12, 24);
            appearances[CreatureConstants.Rat_Dire][GenderConstants.Male] = GetBaseFromRange(12, 24);
            appearances[CreatureConstants.Rat_Dire][CreatureConstants.Rat_Dire] = GetMultiplierFromRange(12, 24);
            //Source: https://www.d20srd.org/srd/monsters/swarm.htm
            appearances[CreatureConstants.Rat_Swarm][GenderConstants.Agender] = "0";
            appearances[CreatureConstants.Rat_Swarm][CreatureConstants.Rat_Swarm] = "0";
            //Source: https://www.dimensions.com/element/common-raven-corvus-corax
            appearances[CreatureConstants.Raven][GenderConstants.Female] = GetBaseFromRange(12, 16);
            appearances[CreatureConstants.Raven][GenderConstants.Male] = GetBaseFromRange(12, 16);
            appearances[CreatureConstants.Raven][CreatureConstants.Raven] = GetMultiplierFromRange(12, 16);
            //Source: https://www.d20srd.org/srd/monsters/ravid.htm
            appearances[CreatureConstants.Ravid][GenderConstants.Agender] = "0";
            appearances[CreatureConstants.Ravid][CreatureConstants.Ravid] = "0";
            //Source: https://www.d20srd.org/srd/monsters/razorBoar.htm Copying from Dire Boar
            appearances[CreatureConstants.RazorBoar][GenderConstants.Female] = GetBaseFromAverage(6 * 12);
            appearances[CreatureConstants.RazorBoar][GenderConstants.Male] = GetBaseFromAverage(6 * 12);
            appearances[CreatureConstants.RazorBoar][CreatureConstants.RazorBoar] = GetMultiplierFromAverage(6 * 12);
            //Source: https://www.d20srd.org/srd/monsters/remorhaz.htm
            appearances[CreatureConstants.Remorhaz][GenderConstants.Female] = GetBaseFromAverage(5 * 12);
            appearances[CreatureConstants.Remorhaz][GenderConstants.Male] = GetBaseFromAverage(5 * 12);
            appearances[CreatureConstants.Remorhaz][CreatureConstants.Remorhaz] = GetMultiplierFromAverage(5 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Retriever
            appearances[CreatureConstants.Retriever][GenderConstants.Agender] = GetBaseFromAverage(12 * 12);
            appearances[CreatureConstants.Retriever][CreatureConstants.Retriever] = GetMultiplierFromAverage(12 * 12);
            //Source: https://www.d20srd.org/srd/monsters/rhinoceros.htm
            appearances[CreatureConstants.Rhinoceras][GenderConstants.Female] = GetBaseFromRange(3 * 12, 6 * 12);
            appearances[CreatureConstants.Rhinoceras][GenderConstants.Male] = GetBaseFromRange(3 * 12, 6 * 12);
            appearances[CreatureConstants.Rhinoceras][CreatureConstants.Rhinoceras] = GetMultiplierFromRange(3 * 12, 6 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Roc
            appearances[CreatureConstants.Roc][GenderConstants.Female] = "0";
            appearances[CreatureConstants.Roc][GenderConstants.Male] = "0";
            appearances[CreatureConstants.Roc][CreatureConstants.Roc] = "0";
            //Source: https://www.d20srd.org/srd/monsters/roper.htm
            appearances[CreatureConstants.Roper][GenderConstants.Hermaphrodite] = GetBaseFromAverage(9 * 12);
            appearances[CreatureConstants.Roper][CreatureConstants.Roper] = GetMultiplierFromAverage(9 * 12);
            //Source: https://www.d20srd.org/srd/monsters/rustMonster.htm
            appearances[CreatureConstants.RustMonster][GenderConstants.Female] = GetBaseFromAverage(3 * 12);
            appearances[CreatureConstants.RustMonster][GenderConstants.Male] = GetBaseFromAverage(3 * 12);
            appearances[CreatureConstants.RustMonster][CreatureConstants.RustMonster] = GetMultiplierFromAverage(3 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Sahuagin
            appearances[CreatureConstants.Sahuagin][GenderConstants.Female] = GetBaseFromRange(6 * 12, 9 * 12);
            appearances[CreatureConstants.Sahuagin][GenderConstants.Male] = GetBaseFromRange(6 * 12, 9 * 12);
            appearances[CreatureConstants.Sahuagin][CreatureConstants.Sahuagin] = GetMultiplierFromRange(6 * 12, 9 * 12);
            appearances[CreatureConstants.Sahuagin_Malenti][GenderConstants.Female] = GetBaseFromRange(6 * 12, 9 * 12);
            appearances[CreatureConstants.Sahuagin_Malenti][GenderConstants.Male] = GetBaseFromRange(6 * 12, 9 * 12);
            appearances[CreatureConstants.Sahuagin_Malenti][CreatureConstants.Sahuagin_Malenti] = GetMultiplierFromRange(6 * 12, 9 * 12);
            appearances[CreatureConstants.Sahuagin_Mutant][GenderConstants.Female] = GetBaseFromRange(6 * 12, 9 * 12);
            appearances[CreatureConstants.Sahuagin_Mutant][GenderConstants.Male] = GetBaseFromRange(6 * 12, 9 * 12);
            appearances[CreatureConstants.Sahuagin_Mutant][CreatureConstants.Sahuagin_Mutant] = GetMultiplierFromRange(6 * 12, 9 * 12);
            //Source: https://www.worldanvil.com/w/faerun-tatortotzke/a/salamander-article (average)
            //Scaling down by half for flamebrother, Scaling up x2 for noble. Assuming height is half of length
            appearances[CreatureConstants.Salamander_Flamebrother][GenderConstants.Agender] = GetBaseFromAverage(20 * 12 / 4);
            appearances[CreatureConstants.Salamander_Flamebrother][CreatureConstants.Salamander_Flamebrother] = GetMultiplierFromAverage(20 * 12 / 4);
            appearances[CreatureConstants.Salamander_Average][GenderConstants.Agender] = GetBaseFromAverage(20 * 12 / 2);
            appearances[CreatureConstants.Salamander_Average][CreatureConstants.Salamander_Average] = GetMultiplierFromAverage(20 * 12 / 2);
            appearances[CreatureConstants.Salamander_Noble][GenderConstants.Agender] = GetBaseFromAverage(20 * 12);
            appearances[CreatureConstants.Salamander_Noble][CreatureConstants.Salamander_Noble] = GetMultiplierFromAverage(20 * 12);
            //Source: https://www.d20srd.org/srd/monsters/satyr.htm - copy from Half-Elf
            appearances[CreatureConstants.Satyr][GenderConstants.Male] = "4*12+7";
            appearances[CreatureConstants.Satyr][CreatureConstants.Satyr] = "2d8";
            appearances[CreatureConstants.Satyr_WithPipes][GenderConstants.Male] = "4*12+7";
            appearances[CreatureConstants.Satyr_WithPipes][CreatureConstants.Satyr_WithPipes] = "2d8";
            //Source: https://www.realmshelps.net/monsters/block/Monstrous_Scorpion,_Tiny
            appearances[CreatureConstants.Scorpion_Monstrous_Tiny][GenderConstants.Female] = GetBaseFromRange(1, 2);
            appearances[CreatureConstants.Scorpion_Monstrous_Tiny][GenderConstants.Male] = GetBaseFromRange(1, 2);
            appearances[CreatureConstants.Scorpion_Monstrous_Tiny][CreatureConstants.Scorpion_Monstrous_Tiny] = GetMultiplierFromRange(1, 2);
            //Source: https://www.realmshelps.net/monsters/block/Monstrous_Scorpion,_Small
            appearances[CreatureConstants.Scorpion_Monstrous_Small][GenderConstants.Female] = GetBaseFromAverage(3);
            appearances[CreatureConstants.Scorpion_Monstrous_Small][GenderConstants.Male] = GetBaseFromAverage(3);
            appearances[CreatureConstants.Scorpion_Monstrous_Small][CreatureConstants.Scorpion_Monstrous_Small] = GetMultiplierFromAverage(3);
            //Source: https://www.realmshelps.net/monsters/block/Monstrous_Scorpion,_Medium
            appearances[CreatureConstants.Scorpion_Monstrous_Medium][GenderConstants.Female] = GetBaseFromAverage(6);
            appearances[CreatureConstants.Scorpion_Monstrous_Medium][GenderConstants.Male] = GetBaseFromAverage(6);
            appearances[CreatureConstants.Scorpion_Monstrous_Medium][CreatureConstants.Scorpion_Monstrous_Medium] = GetMultiplierFromAverage(6);
            //Source: https://www.realmshelps.net/monsters/block/Monstrous_Scorpion,_Large
            appearances[CreatureConstants.Scorpion_Monstrous_Large][GenderConstants.Female] = GetBaseFromAverage(18);
            appearances[CreatureConstants.Scorpion_Monstrous_Large][GenderConstants.Male] = GetBaseFromAverage(18);
            appearances[CreatureConstants.Scorpion_Monstrous_Large][CreatureConstants.Scorpion_Monstrous_Large] = GetMultiplierFromAverage(18);
            //Source: https://www.realmshelps.net/monsters/block/Monstrous_Scorpion,_Huge
            appearances[CreatureConstants.Scorpion_Monstrous_Huge][GenderConstants.Female] = GetBaseFromAverage(2 * 12 + 6);
            appearances[CreatureConstants.Scorpion_Monstrous_Huge][GenderConstants.Male] = GetBaseFromAverage(2 * 12 + 6);
            appearances[CreatureConstants.Scorpion_Monstrous_Huge][CreatureConstants.Scorpion_Monstrous_Huge] = GetMultiplierFromAverage(2 * 12 + 6);
            //Source: https://www.realmshelps.net/monsters/block/Monstrous_Scorpion,_Gargantuan
            appearances[CreatureConstants.Scorpion_Monstrous_Gargantuan][GenderConstants.Female] = GetBaseFromAverage(5 * 12);
            appearances[CreatureConstants.Scorpion_Monstrous_Gargantuan][GenderConstants.Male] = GetBaseFromAverage(5 * 12);
            appearances[CreatureConstants.Scorpion_Monstrous_Gargantuan][CreatureConstants.Scorpion_Monstrous_Gargantuan] = GetMultiplierFromAverage(5 * 12);
            //Source: https://www.realmshelps.net/monsters/block/Monstrous_Scorpion,_Colossal
            appearances[CreatureConstants.Scorpion_Monstrous_Colossal][GenderConstants.Female] = GetBaseFromAverage(10 * 12);
            appearances[CreatureConstants.Scorpion_Monstrous_Colossal][GenderConstants.Male] = GetBaseFromAverage(10 * 12);
            appearances[CreatureConstants.Scorpion_Monstrous_Colossal][CreatureConstants.Scorpion_Monstrous_Colossal] = GetMultiplierFromAverage(10 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Tlincalli
            appearances[CreatureConstants.Scorpionfolk][GenderConstants.Female] = GetBaseFromAverage(6 * 12);
            appearances[CreatureConstants.Scorpionfolk][GenderConstants.Male] = GetBaseFromAverage(6 * 12);
            appearances[CreatureConstants.Scorpionfolk][CreatureConstants.Scorpionfolk] = GetMultiplierFromAverage(6 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Sea_cat
            appearances[CreatureConstants.SeaCat][GenderConstants.Female] = "0";
            appearances[CreatureConstants.SeaCat][GenderConstants.Male] = "0";
            appearances[CreatureConstants.SeaCat][CreatureConstants.SeaCat] = "0";
            //Source: https://www.d20srd.org/srd/monsters/hag.htm - copy from Human
            appearances[CreatureConstants.SeaHag][GenderConstants.Female] = "4*12+5";
            appearances[CreatureConstants.SeaHag][CreatureConstants.SeaHag] = "2d10";
            //Source: https://www.d20srd.org/srd/monsters/shadow.htm
            appearances[CreatureConstants.Shadow][GenderConstants.Agender] = GetBaseFromRange(5 * 12, 6 * 12);
            appearances[CreatureConstants.Shadow][CreatureConstants.Shadow] = GetMultiplierFromRange(5 * 12, 6 * 12);
            appearances[CreatureConstants.Shadow_Greater][GenderConstants.Agender] = GetBaseFromRange(5 * 12, 6 * 12);
            appearances[CreatureConstants.Shadow_Greater][CreatureConstants.Shadow_Greater] = GetMultiplierFromRange(5 * 12, 6 * 12);
            //Source: https://www.d20srd.org/srd/monsters/shadowMastiff.htm
            appearances[CreatureConstants.ShadowMastiff][GenderConstants.Female] = GetBaseFromAverage(2 * 12);
            appearances[CreatureConstants.ShadowMastiff][GenderConstants.Male] = GetBaseFromAverage(2 * 12);
            appearances[CreatureConstants.ShadowMastiff][CreatureConstants.ShadowMastiff] = GetMultiplierFromAverage(2 * 12);
            //Source: https://www.d20srd.org/srd/monsters/shamblingMound.htm
            appearances[CreatureConstants.ShamblingMound][GenderConstants.Agender] = GetBaseFromAverage(6 * 12);
            appearances[CreatureConstants.ShamblingMound][CreatureConstants.ShamblingMound] = GetMultiplierFromAverage(6 * 12);
            //Source: https://www.dimensions.com/element/blacktip-shark-carcharhinus-limbatus
            appearances[CreatureConstants.Shark_Medium][GenderConstants.Female] = "0";
            appearances[CreatureConstants.Shark_Medium][GenderConstants.Male] = "0";
            appearances[CreatureConstants.Shark_Medium][CreatureConstants.Shark_Medium] = "0";
            //Source: https://www.dimensions.com/element/thresher-shark
            appearances[CreatureConstants.Shark_Large][GenderConstants.Female] = "0";
            appearances[CreatureConstants.Shark_Large][GenderConstants.Male] = "0";
            appearances[CreatureConstants.Shark_Large][CreatureConstants.Shark_Large] = "0";
            //Source: https://www.dimensions.com/element/great-white-shark
            appearances[CreatureConstants.Shark_Huge][GenderConstants.Female] = "0";
            appearances[CreatureConstants.Shark_Huge][GenderConstants.Male] = "0";
            appearances[CreatureConstants.Shark_Huge][CreatureConstants.Shark_Huge] = "0";
            appearances[CreatureConstants.Shark_Dire][GenderConstants.Female] = "0";
            appearances[CreatureConstants.Shark_Dire][GenderConstants.Male] = "0";
            appearances[CreatureConstants.Shark_Dire][CreatureConstants.Shark_Dire] = "0";
            //Source: https://www.d20srd.org/srd/monsters/shieldGuardian.htm
            appearances[CreatureConstants.ShieldGuardian][GenderConstants.Agender] = GetBaseFromAverage(9 * 12);
            appearances[CreatureConstants.ShieldGuardian][CreatureConstants.ShieldGuardian] = GetMultiplierFromAverage(9 * 12);
            //Source: https://www.d20srd.org/srd/monsters/shockerLizard.htm
            appearances[CreatureConstants.ShockerLizard][GenderConstants.Female] = GetBaseFromAverage(12);
            appearances[CreatureConstants.ShockerLizard][GenderConstants.Male] = GetBaseFromAverage(12);
            appearances[CreatureConstants.ShockerLizard][CreatureConstants.ShockerLizard] = GetMultiplierFromAverage(12);
            //Source: https://www.worldanvil.com/w/faerun-tatortotzke/a/shrieker-species
            appearances[CreatureConstants.Shrieker][GenderConstants.Agender] = GetBaseFromAverage(3 * 12);
            appearances[CreatureConstants.Shrieker][CreatureConstants.Shrieker] = GetMultiplierFromAverage(3 * 12);
            //Source: https://www.d20srd.org/srd/monsters/skum.htm Copying from Human
            appearances[CreatureConstants.Skum][GenderConstants.Female] = "4*12+5";
            appearances[CreatureConstants.Skum][GenderConstants.Male] = "4*12+10";
            appearances[CreatureConstants.Skum][CreatureConstants.Skum] = "2d10";
            //Source: https://forgottenrealms.fandom.com/wiki/Blue_slaad
            appearances[CreatureConstants.Slaad_Blue][GenderConstants.Agender] = GetBaseFromAverage(10 * 12);
            appearances[CreatureConstants.Slaad_Blue][CreatureConstants.Slaad_Blue] = GetMultiplierFromAverage(10 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Red_slaad
            appearances[CreatureConstants.Slaad_Red][GenderConstants.Agender] = GetBaseFromAverage(8 * 12);
            appearances[CreatureConstants.Slaad_Red][CreatureConstants.Slaad_Red] = GetMultiplierFromAverage(8 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Green_slaad
            appearances[CreatureConstants.Slaad_Green][GenderConstants.Agender] = GetBaseFromAverage(7 * 12);
            appearances[CreatureConstants.Slaad_Green][CreatureConstants.Slaad_Green] = GetMultiplierFromAverage(7 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Gray_slaad
            appearances[CreatureConstants.Slaad_Gray][GenderConstants.Agender] = GetBaseFromAverage(6 * 12);
            appearances[CreatureConstants.Slaad_Gray][CreatureConstants.Slaad_Gray] = GetMultiplierFromAverage(6 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Gray_slaad
            appearances[CreatureConstants.Slaad_Death][GenderConstants.Agender] = GetBaseFromAverage(6 * 12);
            appearances[CreatureConstants.Slaad_Death][CreatureConstants.Slaad_Death] = GetMultiplierFromAverage(6 * 12);
            //Source: https://www.dimensions.com/element/green-tree-python-morelia-viridis
            appearances[CreatureConstants.Snake_Constrictor][GenderConstants.Female] = GetBaseFromRange(1, 2);
            appearances[CreatureConstants.Snake_Constrictor][GenderConstants.Male] = GetBaseFromRange(1, 2);
            appearances[CreatureConstants.Snake_Constrictor][CreatureConstants.Snake_Constrictor] = GetMultiplierFromRange(1, 2);
            //Source: https://www.dimensions.com/element/burmese-python-python-bivittatus
            appearances[CreatureConstants.Snake_Constrictor_Giant][GenderConstants.Female] = GetBaseFromRange(3, 9);
            appearances[CreatureConstants.Snake_Constrictor_Giant][GenderConstants.Male] = GetBaseFromRange(3, 9);
            appearances[CreatureConstants.Snake_Constrictor_Giant][CreatureConstants.Snake_Constrictor_Giant] = GetMultiplierFromRange(3, 9);
            //Source: https://www.dimensions.com/element/ribbon-snake-thamnophis-saurita 
            appearances[CreatureConstants.Snake_Viper_Tiny][GenderConstants.Female] = GetBaseFromAverage(1);
            appearances[CreatureConstants.Snake_Viper_Tiny][GenderConstants.Male] = GetBaseFromAverage(1);
            appearances[CreatureConstants.Snake_Viper_Tiny][CreatureConstants.Snake_Viper_Tiny] = GetMultiplierFromAverage(1);
            //Source: https://www.dimensions.com/element/copperhead-agkistrodon-contortrix 
            appearances[CreatureConstants.Snake_Viper_Small][GenderConstants.Female] = GetBaseFromRange(1, 2);
            appearances[CreatureConstants.Snake_Viper_Small][GenderConstants.Male] = GetBaseFromRange(1, 2);
            appearances[CreatureConstants.Snake_Viper_Small][CreatureConstants.Snake_Viper_Small] = GetMultiplierFromRange(1, 2);
            //Source: https://www.dimensions.com/element/western-diamondback-rattlesnake-crotalus-atrox 
            appearances[CreatureConstants.Snake_Viper_Medium][GenderConstants.Female] = GetBaseFromRange(1, 3);
            appearances[CreatureConstants.Snake_Viper_Medium][GenderConstants.Male] = GetBaseFromRange(1, 3);
            appearances[CreatureConstants.Snake_Viper_Medium][CreatureConstants.Snake_Viper_Medium] = GetMultiplierFromRange(1, 3);
            //Source: https://www.dimensions.com/element/black-mamba-dendroaspis-polylepis 
            appearances[CreatureConstants.Snake_Viper_Large][GenderConstants.Female] = GetBaseFromRange(2, 4);
            appearances[CreatureConstants.Snake_Viper_Large][GenderConstants.Male] = GetBaseFromRange(2, 4);
            appearances[CreatureConstants.Snake_Viper_Large][CreatureConstants.Snake_Viper_Large] = GetMultiplierFromRange(2, 4);
            //Source: https://www.dimensions.com/element/king-cobra-ophiophagus-hannah 
            appearances[CreatureConstants.Snake_Viper_Huge][GenderConstants.Female] = GetBaseFromRange(3, 6);
            appearances[CreatureConstants.Snake_Viper_Huge][GenderConstants.Male] = GetBaseFromRange(3, 6);
            appearances[CreatureConstants.Snake_Viper_Huge][CreatureConstants.Snake_Viper_Huge] = GetMultiplierFromRange(3, 6);
            //Source: https://www.d20srd.org/srd/monsters/spectre.htm Copying from Human
            appearances[CreatureConstants.Spectre][GenderConstants.Female] = "4*12+5";
            appearances[CreatureConstants.Spectre][GenderConstants.Male] = "4*12+10";
            appearances[CreatureConstants.Spectre][CreatureConstants.Spectre] = "2d10";
            //Source: https://www.realmshelps.net/monsters/block/Monstrous_Spider,_Tiny
            appearances[CreatureConstants.Spider_Monstrous_Hunter_Tiny][GenderConstants.Female] = GetBaseFromAverage(2);
            appearances[CreatureConstants.Spider_Monstrous_Hunter_Tiny][GenderConstants.Male] = GetBaseFromAverage(2);
            appearances[CreatureConstants.Spider_Monstrous_Hunter_Tiny][CreatureConstants.Spider_Monstrous_Hunter_Tiny] = GetMultiplierFromAverage(2);
            //Source: https://www.realmshelps.net/monsters/block/Monstrous_Spider,_Small
            appearances[CreatureConstants.Spider_Monstrous_Hunter_Small][GenderConstants.Female] = GetBaseFromAverage(3);
            appearances[CreatureConstants.Spider_Monstrous_Hunter_Small][GenderConstants.Male] = GetBaseFromAverage(3);
            appearances[CreatureConstants.Spider_Monstrous_Hunter_Small][CreatureConstants.Spider_Monstrous_Hunter_Small] = GetMultiplierFromAverage(3);
            //Source: https://www.realmshelps.net/monsters/block/Monstrous_Spider,_Medium
            appearances[CreatureConstants.Spider_Monstrous_Hunter_Medium][GenderConstants.Female] = GetBaseFromAverage(6);
            appearances[CreatureConstants.Spider_Monstrous_Hunter_Medium][GenderConstants.Male] = GetBaseFromAverage(6);
            appearances[CreatureConstants.Spider_Monstrous_Hunter_Medium][CreatureConstants.Spider_Monstrous_Hunter_Medium] = GetMultiplierFromAverage(6);
            //Source: https://www.realmshelps.net/monsters/block/Monstrous_Spider,_Large
            appearances[CreatureConstants.Spider_Monstrous_Hunter_Large][GenderConstants.Female] = GetBaseFromAverage(18);
            appearances[CreatureConstants.Spider_Monstrous_Hunter_Large][GenderConstants.Male] = GetBaseFromAverage(18);
            appearances[CreatureConstants.Spider_Monstrous_Hunter_Large][CreatureConstants.Spider_Monstrous_Hunter_Large] = GetMultiplierFromAverage(18);
            //Source: https://www.realmshelps.net/monsters/block/Monstrous_Spider,_Huge
            appearances[CreatureConstants.Spider_Monstrous_Hunter_Huge][GenderConstants.Female] = GetBaseFromAverage(2 * 12 + 6);
            appearances[CreatureConstants.Spider_Monstrous_Hunter_Huge][GenderConstants.Male] = GetBaseFromAverage(2 * 12 + 6);
            appearances[CreatureConstants.Spider_Monstrous_Hunter_Huge][CreatureConstants.Spider_Monstrous_Hunter_Huge] = GetMultiplierFromAverage(2 * 12 + 6);
            //Source: https://www.realmshelps.net/monsters/block/Monstrous_Spider,_Gargantuan
            appearances[CreatureConstants.Spider_Monstrous_Hunter_Gargantuan][GenderConstants.Female] = GetBaseFromAverage(5 * 12);
            appearances[CreatureConstants.Spider_Monstrous_Hunter_Gargantuan][GenderConstants.Male] = GetBaseFromAverage(5 * 12);
            appearances[CreatureConstants.Spider_Monstrous_Hunter_Gargantuan][CreatureConstants.Spider_Monstrous_Hunter_Gargantuan] = GetMultiplierFromAverage(5 * 12);
            //Source: https://www.realmshelps.net/monsters/block/Monstrous_Spider,_Colossal
            appearances[CreatureConstants.Spider_Monstrous_Hunter_Colossal][GenderConstants.Female] = GetBaseFromAverage(10 * 12);
            appearances[CreatureConstants.Spider_Monstrous_Hunter_Colossal][GenderConstants.Male] = GetBaseFromAverage(10 * 12);
            appearances[CreatureConstants.Spider_Monstrous_Hunter_Colossal][CreatureConstants.Spider_Monstrous_Hunter_Colossal] = GetMultiplierFromAverage(10 * 12);
            //Source: https://www.realmshelps.net/monsters/block/Monstrous_Spider,_Tiny
            appearances[CreatureConstants.Spider_Monstrous_WebSpinner_Tiny][GenderConstants.Female] = GetBaseFromAverage(2);
            appearances[CreatureConstants.Spider_Monstrous_WebSpinner_Tiny][GenderConstants.Male] = GetBaseFromAverage(2);
            appearances[CreatureConstants.Spider_Monstrous_WebSpinner_Tiny][CreatureConstants.Spider_Monstrous_WebSpinner_Tiny] = GetMultiplierFromAverage(2);
            //Source: https://www.realmshelps.net/monsters/block/Monstrous_Spider,_Small
            appearances[CreatureConstants.Spider_Monstrous_WebSpinner_Small][GenderConstants.Female] = GetBaseFromAverage(3);
            appearances[CreatureConstants.Spider_Monstrous_WebSpinner_Small][GenderConstants.Male] = GetBaseFromAverage(3);
            appearances[CreatureConstants.Spider_Monstrous_WebSpinner_Small][CreatureConstants.Spider_Monstrous_WebSpinner_Small] = GetMultiplierFromAverage(3);
            //Source: https://www.realmshelps.net/monsters/block/Monstrous_Spider,_Medium
            appearances[CreatureConstants.Spider_Monstrous_WebSpinner_Medium][GenderConstants.Female] = GetBaseFromAverage(6);
            appearances[CreatureConstants.Spider_Monstrous_WebSpinner_Medium][GenderConstants.Male] = GetBaseFromAverage(6);
            appearances[CreatureConstants.Spider_Monstrous_WebSpinner_Medium][CreatureConstants.Spider_Monstrous_WebSpinner_Medium] = GetMultiplierFromAverage(6);
            //Source: https://www.realmshelps.net/monsters/block/Monstrous_Spider,_Large
            appearances[CreatureConstants.Spider_Monstrous_WebSpinner_Large][GenderConstants.Female] = GetBaseFromAverage(18);
            appearances[CreatureConstants.Spider_Monstrous_WebSpinner_Large][GenderConstants.Male] = GetBaseFromAverage(18);
            appearances[CreatureConstants.Spider_Monstrous_WebSpinner_Large][CreatureConstants.Spider_Monstrous_WebSpinner_Large] = GetMultiplierFromAverage(18);
            //Source: https://www.realmshelps.net/monsters/block/Monstrous_Spider,_Huge
            appearances[CreatureConstants.Spider_Monstrous_WebSpinner_Huge][GenderConstants.Female] = GetBaseFromAverage(2 * 12 + 6);
            appearances[CreatureConstants.Spider_Monstrous_WebSpinner_Huge][GenderConstants.Male] = GetBaseFromAverage(2 * 12 + 6);
            appearances[CreatureConstants.Spider_Monstrous_WebSpinner_Huge][CreatureConstants.Spider_Monstrous_WebSpinner_Huge] = GetMultiplierFromAverage(2 * 12 + 6);
            //Source: https://www.realmshelps.net/monsters/block/Monstrous_Spider,_Gargantuan
            appearances[CreatureConstants.Spider_Monstrous_WebSpinner_Gargantuan][GenderConstants.Female] = GetBaseFromAverage(5 * 12);
            appearances[CreatureConstants.Spider_Monstrous_WebSpinner_Gargantuan][GenderConstants.Male] = GetBaseFromAverage(5 * 12);
            appearances[CreatureConstants.Spider_Monstrous_WebSpinner_Gargantuan][CreatureConstants.Spider_Monstrous_WebSpinner_Gargantuan] = GetMultiplierFromAverage(5 * 12);
            //Source: https://www.realmshelps.net/monsters/block/Monstrous_Spider,_Colossal
            appearances[CreatureConstants.Spider_Monstrous_WebSpinner_Colossal][GenderConstants.Female] = GetBaseFromAverage(10 * 12);
            appearances[CreatureConstants.Spider_Monstrous_WebSpinner_Colossal][GenderConstants.Male] = GetBaseFromAverage(10 * 12);
            appearances[CreatureConstants.Spider_Monstrous_WebSpinner_Colossal][CreatureConstants.Spider_Monstrous_WebSpinner_Colossal] = GetMultiplierFromAverage(10 * 12);
            //Source: https://www.d20srd.org/srd/monsters/swarm.htm
            appearances[CreatureConstants.Spider_Swarm][GenderConstants.Agender] = "0";
            appearances[CreatureConstants.Spider_Swarm][CreatureConstants.Spider_Swarm] = "0";
            //Source: https://www.d20srd.org/srd/monsters/spiderEater.htm
            appearances[CreatureConstants.SpiderEater][GenderConstants.Female] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.SpiderEater][GenderConstants.Male] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.SpiderEater][CreatureConstants.SpiderEater] = GetMultiplierFromAverage(4 * 12);
            //Source: https://www.dimensions.com/element/humboldt-squid-dosidicus-gigas (mantle length)
            appearances[CreatureConstants.Squid][GenderConstants.Female] = GetBaseFromRange(29, 79);
            appearances[CreatureConstants.Squid][GenderConstants.Male] = GetBaseFromRange(29, 79);
            appearances[CreatureConstants.Squid][CreatureConstants.Squid] = GetMultiplierFromRange(29, 79);
            appearances[CreatureConstants.Squid_Giant][GenderConstants.Female] = "0";
            appearances[CreatureConstants.Squid_Giant][GenderConstants.Male] = "0";
            appearances[CreatureConstants.Squid_Giant][CreatureConstants.Squid_Giant] = "0";
            //Source: https://www.d20srd.org/srd/monsters/giantStagBeetle.htm
            //https://www.dimensions.com/element/hercules-beetle-dynastes-hercules scale up: [.47,1.42]*10*12/[2.36,7.09] = [24,24]
            appearances[CreatureConstants.StagBeetle_Giant][GenderConstants.Female] = GetBaseFromAverage(24);
            appearances[CreatureConstants.StagBeetle_Giant][GenderConstants.Male] = GetBaseFromAverage(24);
            appearances[CreatureConstants.StagBeetle_Giant][CreatureConstants.StagBeetle_Giant] = GetMultiplierFromAverage(24);
            appearances[CreatureConstants.Stirge][GenderConstants.Female] = "0";
            appearances[CreatureConstants.Stirge][GenderConstants.Male] = "0";
            appearances[CreatureConstants.Stirge][CreatureConstants.Stirge] = "0";
            //Source: https://forgottenrealms.fandom.com/wiki/Succubus
            appearances[CreatureConstants.Succubus][GenderConstants.Female] = GetBaseFromAverage(6 * 12);
            appearances[CreatureConstants.Succubus][GenderConstants.Male] = GetBaseFromAverage(6 * 12);
            appearances[CreatureConstants.Succubus][CreatureConstants.Succubus] = GetMultiplierFromAverage(6 * 12);
            //Source: https://www.d20srd.org/srd/monsters/tarrasque.htm
            appearances[CreatureConstants.Tarrasque][GenderConstants.Agender] = GetBaseFromAverage(50 * 12);
            appearances[CreatureConstants.Tarrasque][CreatureConstants.Tarrasque] = GetMultiplierFromAverage(50 * 12);
            //Source: https://www.d20srd.org/srd/monsters/tendriculos.htm
            appearances[CreatureConstants.Tendriculos][GenderConstants.Agender] = GetBaseFromAverage(15 * 12);
            appearances[CreatureConstants.Tendriculos][CreatureConstants.Tendriculos] = GetMultiplierFromAverage(15 * 12);
            //Source: https://www.d20srd.org/srd/monsters/thoqqua.htm
            appearances[CreatureConstants.Thoqqua][GenderConstants.Agender] = GetBaseFromAverage(12);
            appearances[CreatureConstants.Thoqqua][CreatureConstants.Thoqqua] = GetMultiplierFromAverage(12);
            //Source: https://forgottenrealms.fandom.com/wiki/Tiefling
            appearances[CreatureConstants.Tiefling][GenderConstants.Female] = GetBaseFromRange(4 * 12 + 11, 6 * 12);
            appearances[CreatureConstants.Tiefling][GenderConstants.Male] = GetBaseFromRange(4 * 12 + 11, 6 * 12);
            appearances[CreatureConstants.Tiefling][CreatureConstants.Tiefling] = GetMultiplierFromRange(4 * 12 + 11, 6 * 12);
            //Source: https://www.dimensions.com/element/bengal-tiger
            appearances[CreatureConstants.Tiger][GenderConstants.Female] = GetBaseFromRange(34, 45);
            appearances[CreatureConstants.Tiger][GenderConstants.Male] = GetBaseFromRange(34, 45);
            appearances[CreatureConstants.Tiger][CreatureConstants.Tiger] = GetMultiplierFromRange(34, 45);
            //Scaled up from Tiger, x2 based on length
            appearances[CreatureConstants.Tiger_Dire][GenderConstants.Female] = GetBaseFromRange(34 * 2, 45 * 2);
            appearances[CreatureConstants.Tiger_Dire][GenderConstants.Male] = GetBaseFromRange(34 * 2, 45 * 2);
            appearances[CreatureConstants.Tiger_Dire][CreatureConstants.Tiger_Dire] = GetMultiplierFromRange(34 * 2, 45 * 2);
            //Source: https://www.d20srd.org/srd/monsters/titan.htm
            appearances[CreatureConstants.Titan][GenderConstants.Female] = GetBaseFromAverage(25 * 12);
            appearances[CreatureConstants.Titan][GenderConstants.Male] = GetBaseFromAverage(25 * 12);
            appearances[CreatureConstants.Titan][CreatureConstants.Titan] = GetMultiplierFromAverage(25 * 12);
            //Source: https://www.dimensions.com/element/common-toad-bufo-bufo
            appearances[CreatureConstants.Toad][GenderConstants.Female] = GetBaseFromRange(2, 3);
            appearances[CreatureConstants.Toad][GenderConstants.Male] = GetBaseFromRange(2, 3);
            appearances[CreatureConstants.Toad][CreatureConstants.Toad] = GetMultiplierFromRange(2, 3);
            appearances[CreatureConstants.Tojanida_Juvenile][GenderConstants.Agender] = "0";
            appearances[CreatureConstants.Tojanida_Juvenile][CreatureConstants.Tojanida_Juvenile] = "0";
            appearances[CreatureConstants.Tojanida_Adult][GenderConstants.Agender] = "0";
            appearances[CreatureConstants.Tojanida_Adult][CreatureConstants.Tojanida_Adult] = "0";
            appearances[CreatureConstants.Tojanida_Elder][GenderConstants.Agender] = "0";
            appearances[CreatureConstants.Tojanida_Elder][CreatureConstants.Tojanida_Elder] = "0";
            //Source: https://www.d20srd.org/srd/monsters/treant.htm
            appearances[CreatureConstants.Treant][GenderConstants.Female] = GetBaseFromAverage(30 * 12);
            appearances[CreatureConstants.Treant][GenderConstants.Male] = GetBaseFromAverage(30 * 12);
            appearances[CreatureConstants.Treant][CreatureConstants.Treant] = GetMultiplierFromAverage(30 * 12);
            //Source: https://jurassicworld-evolution.fandom.com/wiki/Triceratops
            appearances[CreatureConstants.Triceratops][GenderConstants.Female] = GetBaseFromAverage(157);
            appearances[CreatureConstants.Triceratops][GenderConstants.Male] = GetBaseFromAverage(157);
            appearances[CreatureConstants.Triceratops][CreatureConstants.Triceratops] = GetMultiplierFromAverage(157);
            //Source: https://www.d20srd.org/srd/monsters/triton.htm Copying from Human
            appearances[CreatureConstants.Triton][GenderConstants.Female] = "4*12+5";
            appearances[CreatureConstants.Triton][GenderConstants.Male] = "4*12+10";
            appearances[CreatureConstants.Triton][CreatureConstants.Triton] = "2d10";
            //Source: https://forgottenrealms.fandom.com/wiki/Troglodyte
            appearances[CreatureConstants.Troglodyte][GenderConstants.Female] = GetBaseFromRange(5 * 12, 6 * 12);
            appearances[CreatureConstants.Troglodyte][GenderConstants.Male] = GetBaseFromRange(5 * 12, 6 * 12);
            appearances[CreatureConstants.Troglodyte][CreatureConstants.Troglodyte] = GetMultiplierFromRange(5 * 12, 6 * 12);
            //Source: https://www.d20srd.org/srd/monsters/troll.htm Female "slightly larger than males", so 110%
            appearances[CreatureConstants.Troll][GenderConstants.Male] = GetBaseFromAverage(9 * 12);
            appearances[CreatureConstants.Troll][GenderConstants.Female] = GetBaseFromAverage(119, 9 * 12);
            appearances[CreatureConstants.Troll][CreatureConstants.Troll] = GetMultiplierFromAverage(9 * 12);
            appearances[CreatureConstants.Troll_Scrag][GenderConstants.Male] = GetBaseFromAverage(9 * 12);
            appearances[CreatureConstants.Troll_Scrag][GenderConstants.Female] = GetBaseFromAverage(119, 9 * 12);
            appearances[CreatureConstants.Troll_Scrag][CreatureConstants.Troll_Scrag] = GetMultiplierFromAverage(9 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Trumpet_archon
            appearances[CreatureConstants.TrumpetArchon][GenderConstants.Female] = GetBaseFromRange(6 * 12 + 1, 7 * 12 + 4);
            appearances[CreatureConstants.TrumpetArchon][GenderConstants.Male] = GetBaseFromRange(6 * 12 + 6, 7 * 12 + 9);
            appearances[CreatureConstants.TrumpetArchon][CreatureConstants.TrumpetArchon] = GetMultiplierFromRange(6 * 12 + 6, 7 * 12 + 9);
            //Source: https://jurassicworld-evolution.fandom.com/wiki/Tyrannosaurus
            appearances[CreatureConstants.Tyrannosaurus][GenderConstants.Male] = GetBaseFromAverage(232);
            appearances[CreatureConstants.Tyrannosaurus][GenderConstants.Female] = GetBaseFromAverage(232);
            appearances[CreatureConstants.Tyrannosaurus][CreatureConstants.Tyrannosaurus] = GetMultiplierFromAverage(232);
            //Source: https://forgottenrealms.fandom.com/wiki/Umber_hulk
            appearances[CreatureConstants.UmberHulk][GenderConstants.Female] = GetBaseFromRange(4 * 12 + 6, 5 * 12);
            appearances[CreatureConstants.UmberHulk][GenderConstants.Male] = GetBaseFromRange(4 * 12 + 6, 5 * 12);
            appearances[CreatureConstants.UmberHulk][CreatureConstants.UmberHulk] = GetMultiplierFromRange(4 * 12 + 6, 5 * 12);
            //Scale up based on length, X2
            appearances[CreatureConstants.UmberHulk_TrulyHorrid][GenderConstants.Female] = GetBaseFromRange(9 * 12, 10 * 12);
            appearances[CreatureConstants.UmberHulk_TrulyHorrid][GenderConstants.Male] = GetBaseFromRange(9 * 12, 10 * 12);
            appearances[CreatureConstants.UmberHulk_TrulyHorrid][CreatureConstants.UmberHulk_TrulyHorrid] = GetMultiplierFromRange(9 * 12, 10 * 12);
            //Source: https://www.d20srd.org/srd/monsters/unicorn.htm Females "slightly smaller", so 90%
            appearances[CreatureConstants.Unicorn][GenderConstants.Female] = GetBaseFromAverage(54, 5 * 12);
            appearances[CreatureConstants.Unicorn][GenderConstants.Male] = GetBaseFromAverage(5 * 12);
            appearances[CreatureConstants.Unicorn][CreatureConstants.Unicorn] = GetMultiplierFromAverage(5 * 12);
            //Source: https://www.d20srd.org/srd/monsters/vampire.htm#vampireSpawn Copying from Human
            appearances[CreatureConstants.VampireSpawn][GenderConstants.Female] = "4*12+5";
            appearances[CreatureConstants.VampireSpawn][GenderConstants.Male] = "4*12+10";
            appearances[CreatureConstants.VampireSpawn][CreatureConstants.VampireSpawn] = "2d10";
            //Source: https://www.d20srd.org/srd/monsters/vargouille.htm
            appearances[CreatureConstants.Vargouille][GenderConstants.Agender] = GetBaseFromAverage(18);
            appearances[CreatureConstants.Vargouille][CreatureConstants.Vargouille] = GetMultiplierFromAverage(18);
            //Source: https://www.worldanvil.com/w/faerun-tatortotzke/a/violet-fungus-species
            appearances[CreatureConstants.VioletFungus][GenderConstants.Agender] = GetBaseFromRange(4 * 12, 7 * 12);
            appearances[CreatureConstants.VioletFungus][CreatureConstants.VioletFungus] = GetMultiplierFromRange(4 * 12, 7 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Vrock
            appearances[CreatureConstants.Vrock][GenderConstants.Agender] = GetBaseFromAverage(8 * 12);
            appearances[CreatureConstants.Vrock][CreatureConstants.Vrock] = GetMultiplierFromAverage(8 * 12);
            //Source: https://www.dimensions.com/element/red-paper-wasp-polistes-carolina
            //https://forgottenrealms.fandom.com/wiki/Giant_wasp scale up: [.23,.33]*5*12/[.94,1.26] = [14,16]
            appearances[CreatureConstants.Wasp_Giant][GenderConstants.Male] = GetBaseFromRange(14, 16);
            appearances[CreatureConstants.Wasp_Giant][CreatureConstants.Wasp_Giant] = GetMultiplierFromRange(14, 16);
            //Source: https://www.dimensions.com/element/least-weasel-mustela-nivalis
            appearances[CreatureConstants.Weasel][GenderConstants.Female] = GetBaseFromRange(2, 3);
            appearances[CreatureConstants.Weasel][GenderConstants.Male] = GetBaseFromRange(2, 3);
            appearances[CreatureConstants.Weasel][CreatureConstants.Weasel] = GetMultiplierFromRange(2, 3);
            //Scaled up from weasel, x15 based on length
            appearances[CreatureConstants.Weasel_Dire][GenderConstants.Female] = GetBaseFromRange(30, 45);
            appearances[CreatureConstants.Weasel_Dire][GenderConstants.Male] = GetBaseFromRange(30, 45);
            appearances[CreatureConstants.Weasel_Dire][CreatureConstants.Weasel_Dire] = GetMultiplierFromRange(30, 45);
            //Source: https://www.dimensions.com/element/humpback-whale-megaptera-novaeangliae
            appearances[CreatureConstants.Whale_Baleen][GenderConstants.Female] = GetBaseFromRange(8 * 12, 9 * 12 + 8);
            appearances[CreatureConstants.Whale_Baleen][GenderConstants.Male] = GetBaseFromRange(8 * 12, 9 * 12 + 8);
            appearances[CreatureConstants.Whale_Baleen][CreatureConstants.Whale_Baleen] = GetMultiplierFromRange(8 * 12, 9 * 12 + 8);
            //Source: https://www.dimensions.com/element/sperm-whale-physeter-macrocephalus
            appearances[CreatureConstants.Whale_Cachalot][GenderConstants.Female] = GetBaseFromRange(6 * 12 + 9, 11 * 12);
            appearances[CreatureConstants.Whale_Cachalot][GenderConstants.Male] = GetBaseFromRange(6 * 12 + 9, 11 * 12);
            appearances[CreatureConstants.Whale_Cachalot][CreatureConstants.Whale_Cachalot] = GetMultiplierFromRange(6 * 12 + 9, 11 * 12);
            //Source: https://www.dimensions.com/element/orca-killer-whale-orcinus-orca
            appearances[CreatureConstants.Whale_Orca][GenderConstants.Female] = GetBaseFromRange(5 * 12 + 3, 7 * 12 + 6);
            appearances[CreatureConstants.Whale_Orca][GenderConstants.Male] = GetBaseFromRange(5 * 12 + 3, 7 * 12 + 6);
            appearances[CreatureConstants.Whale_Orca][CreatureConstants.Whale_Orca] = GetMultiplierFromRange(5 * 12 + 3, 7 * 12 + 6);
            //Source: https://www.d20srd.org/srd/monsters/wight.htm Copy from Human
            appearances[CreatureConstants.Wight][GenderConstants.Female] = "4*12+5";
            appearances[CreatureConstants.Wight][GenderConstants.Male] = "4*12+10";
            appearances[CreatureConstants.Wight][CreatureConstants.Wight] = "2d10";
            //Source: https://www.d20srd.org/srd/monsters/willOWisp.htm
            appearances[CreatureConstants.WillOWisp][GenderConstants.Agender] = GetBaseFromAverage(12);
            appearances[CreatureConstants.WillOWisp][CreatureConstants.WillOWisp] = GetMultiplierFromAverage(12);
            //Source: https://www.d20srd.org/srd/monsters/winterWolf.htm
            appearances[CreatureConstants.WinterWolf][GenderConstants.Female] = GetBaseFromAverage(4 * 12 + 6);
            appearances[CreatureConstants.WinterWolf][GenderConstants.Male] = GetBaseFromAverage(4 * 12 + 6);
            appearances[CreatureConstants.WinterWolf][CreatureConstants.WinterWolf] = GetMultiplierFromAverage(4 * 12 + 6);
            //Source: https://www.dimensions.com/element/gray-wolf
            appearances[CreatureConstants.Wolf][GenderConstants.Female] = GetBaseFromRange(26, 33);
            appearances[CreatureConstants.Wolf][GenderConstants.Male] = GetBaseFromRange(26, 33);
            appearances[CreatureConstants.Wolf][CreatureConstants.Wolf] = GetMultiplierFromRange(26, 33);
            //Scaled up from Wolf, x2 based on length
            appearances[CreatureConstants.Wolf_Dire][GenderConstants.Female] = GetBaseFromRange(26 * 2, 33 * 2);
            appearances[CreatureConstants.Wolf_Dire][GenderConstants.Male] = GetBaseFromRange(26 * 2, 33 * 2);
            appearances[CreatureConstants.Wolf_Dire][CreatureConstants.Wolf_Dire] = GetMultiplierFromRange(26 * 2, 33 * 2);
            //Source: https://www.dimensions.com/element/wolverine-gulo-gulo
            appearances[CreatureConstants.Wolverine][GenderConstants.Female] = GetBaseFromRange(14, 21);
            appearances[CreatureConstants.Wolverine][GenderConstants.Male] = GetBaseFromRange(14, 21);
            appearances[CreatureConstants.Wolverine][CreatureConstants.Wolverine] = GetMultiplierFromRange(14, 21);
            //Scaled up from Wolverine, x4 based on length
            appearances[CreatureConstants.Wolverine_Dire][GenderConstants.Female] = GetBaseFromRange(14 * 4, 21 * 4);
            appearances[CreatureConstants.Wolverine_Dire][GenderConstants.Male] = GetBaseFromRange(14 * 4, 21 * 4);
            appearances[CreatureConstants.Wolverine_Dire][CreatureConstants.Wolverine_Dire] = GetMultiplierFromRange(14 * 4, 21 * 4);
            //Source: https://www.d20srd.org/srd/monsters/worg.htm
            appearances[CreatureConstants.Worg][GenderConstants.Female] = GetBaseFromAverage(3 * 12);
            appearances[CreatureConstants.Worg][GenderConstants.Male] = GetBaseFromAverage(3 * 12);
            appearances[CreatureConstants.Worg][CreatureConstants.Worg] = GetMultiplierFromAverage(3 * 12);
            //Source: https://www.d20srd.org/srd/monsters/wraith.htm W:Human, DW:Ogre
            appearances[CreatureConstants.Wraith][GenderConstants.Agender] = "4*12+10";
            appearances[CreatureConstants.Wraith][CreatureConstants.Wraith] = "2d10";
            appearances[CreatureConstants.Wraith_Dread][GenderConstants.Agender] = "96";
            appearances[CreatureConstants.Wraith_Dread][CreatureConstants.Wraith_Dread] = "2d12";
            //Source: https://forgottenrealms.fandom.com/wiki/Wyvern
            appearances[CreatureConstants.Wyvern][GenderConstants.Female] = "0";
            appearances[CreatureConstants.Wyvern][GenderConstants.Male] = "0";
            appearances[CreatureConstants.Wyvern][CreatureConstants.Wyvern] = "0";
            //Source: https://www.d20srd.org/srd/monsters/xill.htm
            appearances[CreatureConstants.Xill][GenderConstants.Agender] = GetBaseFromRange(4 * 12, 5 * 12);
            appearances[CreatureConstants.Xill][CreatureConstants.Xill] = GetMultiplierFromRange(4 * 12, 5 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Xorn
            appearances[CreatureConstants.Xorn_Minor][GenderConstants.Agender] = GetBaseFromAverage(3 * 12);
            appearances[CreatureConstants.Xorn_Minor][CreatureConstants.Xorn_Minor] = GetMultiplierFromAverage(3 * 12);
            appearances[CreatureConstants.Xorn_Average][GenderConstants.Agender] = GetBaseFromAverage(5 * 12);
            appearances[CreatureConstants.Xorn_Average][CreatureConstants.Xorn_Average] = GetMultiplierFromAverage(5 * 12);
            appearances[CreatureConstants.Xorn_Elder][GenderConstants.Agender] = GetBaseFromAverage(8 * 12);
            appearances[CreatureConstants.Xorn_Elder][CreatureConstants.Xorn_Elder] = GetMultiplierFromAverage(8 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Yeth_hound
            appearances[CreatureConstants.YethHound][GenderConstants.Agender] = GetBaseFromRange(4 * 12, 5 * 12);
            appearances[CreatureConstants.YethHound][CreatureConstants.YethHound] = GetMultiplierFromRange(4 * 12, 5 * 12);
            //Source: https://www.d20srd.org/srd/monsters/yrthak.htm
            appearances[CreatureConstants.Yrthak][GenderConstants.Agender] = "0";
            appearances[CreatureConstants.Yrthak][CreatureConstants.Yrthak] = "0";
            //Source: https://forgottenrealms.fandom.com/wiki/Yuan-ti_pureblood
            appearances[CreatureConstants.YuanTi_Pureblood][GenderConstants.Female] = GetBaseFromRange(4 * 12 + 7, 6 * 12 + 6);
            appearances[CreatureConstants.YuanTi_Pureblood][GenderConstants.Male] = GetBaseFromRange(4 * 12 + 7, 6 * 12 + 6);
            appearances[CreatureConstants.YuanTi_Pureblood][CreatureConstants.YuanTi_Pureblood] = GetMultiplierFromRange(4 * 12 + 7, 6 * 12 + 6);
            //Source: https://forgottenrealms.fandom.com/wiki/Yuan-ti_malison
            appearances[CreatureConstants.YuanTi_Halfblood_SnakeArms][GenderConstants.Female] = GetBaseFromRange(4 * 12 + 7, 6 * 12 + 6);
            appearances[CreatureConstants.YuanTi_Halfblood_SnakeArms][GenderConstants.Male] = GetBaseFromRange(4 * 12 + 7, 6 * 12 + 6);
            appearances[CreatureConstants.YuanTi_Halfblood_SnakeArms][CreatureConstants.YuanTi_Halfblood_SnakeArms] = GetMultiplierFromRange(4 * 12 + 7, 6 * 12 + 6);
            appearances[CreatureConstants.YuanTi_Halfblood_SnakeHead][GenderConstants.Female] = GetBaseFromRange(4 * 12 + 7, 6 * 12 + 6);
            appearances[CreatureConstants.YuanTi_Halfblood_SnakeHead][GenderConstants.Male] = GetBaseFromRange(4 * 12 + 7, 6 * 12 + 6);
            appearances[CreatureConstants.YuanTi_Halfblood_SnakeHead][CreatureConstants.YuanTi_Halfblood_SnakeHead] = GetMultiplierFromRange(4 * 12 + 7, 6 * 12 + 6);
            appearances[CreatureConstants.YuanTi_Halfblood_SnakeTail][GenderConstants.Female] = GetBaseFromRange(4 * 12 + 7, 6 * 12 + 6);
            appearances[CreatureConstants.YuanTi_Halfblood_SnakeTail][GenderConstants.Male] = GetBaseFromRange(4 * 12 + 7, 6 * 12 + 6);
            appearances[CreatureConstants.YuanTi_Halfblood_SnakeTail][CreatureConstants.YuanTi_Halfblood_SnakeTail] = GetMultiplierFromRange(4 * 12 + 7, 6 * 12 + 6);
            appearances[CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs][GenderConstants.Female] = GetBaseFromRange(4 * 12 + 7, 6 * 12 + 6);
            appearances[CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs][GenderConstants.Male] = GetBaseFromRange(4 * 12 + 7, 6 * 12 + 6);
            appearances[CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs][CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs] =
                GetMultiplierFromRange(4 * 12 + 7, 6 * 12 + 6);
            //Source: https://forgottenrealms.fandom.com/wiki/Yuan-ti_abomination (assuming height based on other yuan-ti)
            appearances[CreatureConstants.YuanTi_Abomination][GenderConstants.Female] = GetBaseFromRange(4 * 12 + 7, 6 * 12 + 6);
            appearances[CreatureConstants.YuanTi_Abomination][GenderConstants.Male] = GetBaseFromRange(4 * 12 + 7, 6 * 12 + 6);
            appearances[CreatureConstants.YuanTi_Abomination][CreatureConstants.YuanTi_Abomination] = GetMultiplierFromRange(4 * 12 + 7, 6 * 12 + 6);
            //Source: https://forgottenrealms.fandom.com/wiki/Zelekhut using centaur stats
            appearances[CreatureConstants.Zelekhut][GenderConstants.Agender] = GetBaseFromRange(7 * 12, 9 * 12);
            appearances[CreatureConstants.Zelekhut][CreatureConstants.Zelekhut] = GetMultiplierFromRange(7 * 12, 9 * 12);

            return appearances;
        }
    }
}
