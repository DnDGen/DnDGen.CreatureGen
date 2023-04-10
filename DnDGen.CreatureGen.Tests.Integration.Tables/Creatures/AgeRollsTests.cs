using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Tables;
using DnDGen.RollGen;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Integration.Tables.Creatures
{
    [TestFixture]
    public class AgeRollsTests : TypesAndAmountsTests
    {
        protected override string tableName => TableNameConstants.TypeAndAmount.AgeRolls;

        private Dice dice;

        [SetUp]
        public void Setup()
        {
            dice = GetNewInstanceOf<Dice>();
        }

        [Test]
        public void AgeRollsNames()
        {
            var creatures = CreatureConstants.GetAll();
            AssertCollectionNames(creatures);
        }

        [TestCaseSource(nameof(CreatureAgeRollsData))]
        public void CreatureAgeRolls(string name, Dictionary<string, string> typesAndRolls)
        {
            Assert.That(typesAndRolls, Is.Not.Empty
                .And.Count.AtLeast(2)
                .And.ContainKey(AgeConstants.Categories.Maximum), name);

            foreach (var roll in typesAndRolls.Values)
            {
                var isValid = dice.Roll(roll).IsValid();
                Assert.That(isValid, Is.True, roll);
            }

            AssertTypesAndAmounts(name, typesAndRolls);
        }

        public static IEnumerable CreatureAgeRollsData
        {
            get
            {
                var oneKRoll = GetRoll(1, 1_000);
                var constructAgeRoll = oneKRoll;
                var tenKRoll = GetRoll(1, 10_000);
                var undeadAgeRoll = tenKRoll;
                var oneHundredKRoll = GetRoll(1, 100_000);
                var feyAgeRoll = oneHundredKRoll;
                var oneMRoll = GetRoll(1, 1_000_000);
                var outsiderAgeRoll = oneMRoll;

                var testCases = new Dictionary<string, Dictionary<string, string>>();
                var creatures = CreatureConstants.GetAll();

                foreach (var creature in creatures)
                {
                    testCases[creature] = new Dictionary<string, string>();
                }

                /*INFO: For animal ages, and ages I create, here is my method:
                 1. Get the start of the Venerable age category. If not found, assume 50 or 100 (whichever seems more reasonable)
                   A. If lifespan is described by a range, use that
                   B. If lifespan described as "over", then use as start of Venerable
                   C. If lifespan described as "average", then use as 1.25x of Venerable
                   D. If lifespan described as "up to", then use that as 1.5x of Venerable
                 2. Maximum is +50%
                 2. Figure out when "adulthood" starts. If not found, assume at 20% of venerable (for 50, it would be 10; 100 = 20)
                 3. Middle Age starts at halfway to venerable (50 = 25, 100 = 50)
                 4. If a "wild" versus "captivity" age is specified, captivity = venerable, wild = old. Otherwise, Old = 75% of venerable

                 Example: Human
                   
                    Adulthood = [15, 34];
                    MiddleAge = [35, 52];
                    Old = [53, 69];
                    Venerable = [70, 110];
                    Maximum = "70+2d20";

                    1. Venerable starts at 70
                    2. Maximum = 70*1.5 = 105 ~= 3d12 (close enough to 2d20 for making something up)
                    2. Adulthood is 70*.2 = 14 (close enough to 15 for making something up)
                    3. Middle Age = 70*.5 = 35
                    4. Old = 70*.75 = 52.5 (round up to 53)

                For swarms, let the age be 0 to double max
                */

                //Source: http://people.wku.edu/charles.plemons/ad&d/races/age.html
                testCases[CreatureConstants.Aasimar][AgeConstants.Categories.Adulthood] = GetRoll(16, 61);
                testCases[CreatureConstants.Aasimar][AgeConstants.Categories.MiddleAge] = GetRoll(62, 82);
                testCases[CreatureConstants.Aasimar][AgeConstants.Categories.Old] = GetRoll(83, 124);
                testCases[CreatureConstants.Aasimar][AgeConstants.Categories.Venerable] = GetRoll(125, 165);
                testCases[CreatureConstants.Aasimar][AgeConstants.Categories.Maximum] = "125+2d20";
                //Source: https://forgottenrealms.fandom.com/wiki/Aboleth
                testCases[CreatureConstants.Aboleth][AgeConstants.Categories.Adulthood] = $"{oneMRoll}+10";
                testCases[CreatureConstants.Aboleth][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                //Source: https://forgottenrealms.fandom.com/wiki/Achaierai#Ecology - "extremely long lived", so x2.5 from Venerable
                testCases[CreatureConstants.Achaierai][AgeConstants.Categories.Adulthood] = GetAdulthoodRoll(30, 3);
                testCases[CreatureConstants.Achaierai][AgeConstants.Categories.MiddleAge] = GetMiddleAgeRoll(30);
                testCases[CreatureConstants.Achaierai][AgeConstants.Categories.Old] = GetOldRoll(30);
                testCases[CreatureConstants.Achaierai][AgeConstants.Categories.Venerable] = GetVenerableRoll(30, 30 * 5 / 2);
                testCases[CreatureConstants.Achaierai][AgeConstants.Categories.Maximum] = GetMaximumRoll(30, 30 * 5 / 2);
                testCases[CreatureConstants.Allip][AgeConstants.Categories.Undead] = undeadAgeRoll;
                testCases[CreatureConstants.Allip][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                //Source: https://pathfinder.d20srd.org/bestiary3/sphinx.html (adulthood)
                //https://www.belloflostsouls.net/2021/12/dd-monster-spotlight-androsphinx.html (maximum)
                testCases[CreatureConstants.Androsphinx][AgeConstants.Categories.Adulthood] = $"{tenKRoll}+3";
                testCases[CreatureConstants.Androsphinx][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Angel_AstralDeva][AgeConstants.Categories.Adulthood] = outsiderAgeRoll;
                testCases[CreatureConstants.Angel_AstralDeva][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Angel_Planetar][AgeConstants.Categories.Adulthood] = outsiderAgeRoll;
                testCases[CreatureConstants.Angel_Planetar][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Angel_Solar][AgeConstants.Categories.Adulthood] = outsiderAgeRoll;
                testCases[CreatureConstants.Angel_Solar][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.AnimatedObject_Colossal][AgeConstants.Categories.Construct] = constructAgeRoll;
                testCases[CreatureConstants.AnimatedObject_Colossal][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.AnimatedObject_Colossal_Flexible][AgeConstants.Categories.Construct] = constructAgeRoll;
                testCases[CreatureConstants.AnimatedObject_Colossal_Flexible][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.AnimatedObject_Colossal_MultipleLegs][AgeConstants.Categories.Construct] = constructAgeRoll;
                testCases[CreatureConstants.AnimatedObject_Colossal_MultipleLegs][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.AnimatedObject_Colossal_MultipleLegs_Wooden][AgeConstants.Categories.Construct] = constructAgeRoll;
                testCases[CreatureConstants.AnimatedObject_Colossal_MultipleLegs_Wooden][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.AnimatedObject_Colossal_Sheetlike][AgeConstants.Categories.Construct] = constructAgeRoll;
                testCases[CreatureConstants.AnimatedObject_Colossal_Sheetlike][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.AnimatedObject_Colossal_TwoLegs][AgeConstants.Categories.Construct] = constructAgeRoll;
                testCases[CreatureConstants.AnimatedObject_Colossal_TwoLegs][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.AnimatedObject_Colossal_TwoLegs_Wooden][AgeConstants.Categories.Construct] = constructAgeRoll;
                testCases[CreatureConstants.AnimatedObject_Colossal_TwoLegs_Wooden][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.AnimatedObject_Colossal_Wheels_Wooden][AgeConstants.Categories.Construct] = constructAgeRoll;
                testCases[CreatureConstants.AnimatedObject_Colossal_Wheels_Wooden][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.AnimatedObject_Colossal_Wooden][AgeConstants.Categories.Construct] = constructAgeRoll;
                testCases[CreatureConstants.AnimatedObject_Colossal_Wooden][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.AnimatedObject_Gargantuan][AgeConstants.Categories.Construct] = constructAgeRoll;
                testCases[CreatureConstants.AnimatedObject_Gargantuan][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.AnimatedObject_Gargantuan_Flexible][AgeConstants.Categories.Construct] = constructAgeRoll;
                testCases[CreatureConstants.AnimatedObject_Gargantuan_Flexible][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.AnimatedObject_Gargantuan_MultipleLegs][AgeConstants.Categories.Construct] = constructAgeRoll;
                testCases[CreatureConstants.AnimatedObject_Gargantuan_MultipleLegs][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.AnimatedObject_Gargantuan_MultipleLegs_Wooden][AgeConstants.Categories.Construct] = constructAgeRoll;
                testCases[CreatureConstants.AnimatedObject_Gargantuan_MultipleLegs_Wooden][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.AnimatedObject_Gargantuan_Sheetlike][AgeConstants.Categories.Construct] = constructAgeRoll;
                testCases[CreatureConstants.AnimatedObject_Gargantuan_Sheetlike][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.AnimatedObject_Gargantuan_TwoLegs][AgeConstants.Categories.Construct] = constructAgeRoll;
                testCases[CreatureConstants.AnimatedObject_Gargantuan_TwoLegs][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.AnimatedObject_Gargantuan_TwoLegs_Wooden][AgeConstants.Categories.Construct] = constructAgeRoll;
                testCases[CreatureConstants.AnimatedObject_Gargantuan_TwoLegs_Wooden][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.AnimatedObject_Gargantuan_Wheels_Wooden][AgeConstants.Categories.Construct] = constructAgeRoll;
                testCases[CreatureConstants.AnimatedObject_Gargantuan_Wheels_Wooden][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.AnimatedObject_Gargantuan_Wooden][AgeConstants.Categories.Construct] = constructAgeRoll;
                testCases[CreatureConstants.AnimatedObject_Gargantuan_Wooden][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.AnimatedObject_Huge][AgeConstants.Categories.Construct] = constructAgeRoll;
                testCases[CreatureConstants.AnimatedObject_Huge][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.AnimatedObject_Huge_Flexible][AgeConstants.Categories.Construct] = constructAgeRoll;
                testCases[CreatureConstants.AnimatedObject_Huge_Flexible][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.AnimatedObject_Huge_MultipleLegs][AgeConstants.Categories.Construct] = constructAgeRoll;
                testCases[CreatureConstants.AnimatedObject_Huge_MultipleLegs][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.AnimatedObject_Huge_MultipleLegs_Wooden][AgeConstants.Categories.Construct] = constructAgeRoll;
                testCases[CreatureConstants.AnimatedObject_Huge_MultipleLegs_Wooden][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.AnimatedObject_Huge_Sheetlike][AgeConstants.Categories.Construct] = constructAgeRoll;
                testCases[CreatureConstants.AnimatedObject_Huge_Sheetlike][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.AnimatedObject_Huge_TwoLegs][AgeConstants.Categories.Construct] = constructAgeRoll;
                testCases[CreatureConstants.AnimatedObject_Huge_TwoLegs][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.AnimatedObject_Huge_TwoLegs_Wooden][AgeConstants.Categories.Construct] = constructAgeRoll;
                testCases[CreatureConstants.AnimatedObject_Huge_TwoLegs_Wooden][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.AnimatedObject_Huge_Wheels_Wooden][AgeConstants.Categories.Construct] = constructAgeRoll;
                testCases[CreatureConstants.AnimatedObject_Huge_Wheels_Wooden][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.AnimatedObject_Huge_Wooden][AgeConstants.Categories.Construct] = constructAgeRoll;
                testCases[CreatureConstants.AnimatedObject_Huge_Wooden][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.AnimatedObject_Large][AgeConstants.Categories.Construct] = constructAgeRoll;
                testCases[CreatureConstants.AnimatedObject_Large][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.AnimatedObject_Large_Flexible][AgeConstants.Categories.Construct] = constructAgeRoll;
                testCases[CreatureConstants.AnimatedObject_Large_Flexible][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.AnimatedObject_Large_MultipleLegs][AgeConstants.Categories.Construct] = constructAgeRoll;
                testCases[CreatureConstants.AnimatedObject_Large_MultipleLegs][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.AnimatedObject_Large_MultipleLegs_Wooden][AgeConstants.Categories.Construct] = constructAgeRoll;
                testCases[CreatureConstants.AnimatedObject_Large_MultipleLegs_Wooden][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.AnimatedObject_Large_Sheetlike][AgeConstants.Categories.Construct] = constructAgeRoll;
                testCases[CreatureConstants.AnimatedObject_Large_Sheetlike][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.AnimatedObject_Large_TwoLegs][AgeConstants.Categories.Construct] = constructAgeRoll;
                testCases[CreatureConstants.AnimatedObject_Large_TwoLegs][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.AnimatedObject_Large_TwoLegs_Wooden][AgeConstants.Categories.Construct] = constructAgeRoll;
                testCases[CreatureConstants.AnimatedObject_Large_TwoLegs_Wooden][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.AnimatedObject_Large_Wheels_Wooden][AgeConstants.Categories.Construct] = constructAgeRoll;
                testCases[CreatureConstants.AnimatedObject_Large_Wheels_Wooden][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.AnimatedObject_Large_Wooden][AgeConstants.Categories.Construct] = constructAgeRoll;
                testCases[CreatureConstants.AnimatedObject_Large_Wooden][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.AnimatedObject_Medium][AgeConstants.Categories.Construct] = constructAgeRoll;
                testCases[CreatureConstants.AnimatedObject_Medium][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.AnimatedObject_Medium_Flexible][AgeConstants.Categories.Construct] = constructAgeRoll;
                testCases[CreatureConstants.AnimatedObject_Medium_Flexible][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.AnimatedObject_Medium_MultipleLegs][AgeConstants.Categories.Construct] = constructAgeRoll;
                testCases[CreatureConstants.AnimatedObject_Medium_MultipleLegs][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.AnimatedObject_Medium_MultipleLegs_Wooden][AgeConstants.Categories.Construct] = constructAgeRoll;
                testCases[CreatureConstants.AnimatedObject_Medium_MultipleLegs_Wooden][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.AnimatedObject_Medium_Sheetlike][AgeConstants.Categories.Construct] = constructAgeRoll;
                testCases[CreatureConstants.AnimatedObject_Medium_Sheetlike][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.AnimatedObject_Medium_TwoLegs][AgeConstants.Categories.Construct] = constructAgeRoll;
                testCases[CreatureConstants.AnimatedObject_Medium_TwoLegs][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.AnimatedObject_Medium_TwoLegs_Wooden][AgeConstants.Categories.Construct] = constructAgeRoll;
                testCases[CreatureConstants.AnimatedObject_Medium_TwoLegs_Wooden][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.AnimatedObject_Medium_Wheels_Wooden][AgeConstants.Categories.Construct] = constructAgeRoll;
                testCases[CreatureConstants.AnimatedObject_Medium_Wheels_Wooden][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.AnimatedObject_Medium_Wooden][AgeConstants.Categories.Construct] = constructAgeRoll;
                testCases[CreatureConstants.AnimatedObject_Medium_Wooden][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.AnimatedObject_Small][AgeConstants.Categories.Construct] = constructAgeRoll;
                testCases[CreatureConstants.AnimatedObject_Small][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.AnimatedObject_Small_Flexible][AgeConstants.Categories.Construct] = constructAgeRoll;
                testCases[CreatureConstants.AnimatedObject_Small_Flexible][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.AnimatedObject_Small_MultipleLegs][AgeConstants.Categories.Construct] = constructAgeRoll;
                testCases[CreatureConstants.AnimatedObject_Small_MultipleLegs][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.AnimatedObject_Small_MultipleLegs_Wooden][AgeConstants.Categories.Construct] = constructAgeRoll;
                testCases[CreatureConstants.AnimatedObject_Small_MultipleLegs_Wooden][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.AnimatedObject_Small_Sheetlike][AgeConstants.Categories.Construct] = constructAgeRoll;
                testCases[CreatureConstants.AnimatedObject_Small_Sheetlike][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.AnimatedObject_Small_TwoLegs][AgeConstants.Categories.Construct] = constructAgeRoll;
                testCases[CreatureConstants.AnimatedObject_Small_TwoLegs][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.AnimatedObject_Small_TwoLegs_Wooden][AgeConstants.Categories.Construct] = constructAgeRoll;
                testCases[CreatureConstants.AnimatedObject_Small_TwoLegs_Wooden][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.AnimatedObject_Small_Wheels_Wooden][AgeConstants.Categories.Construct] = constructAgeRoll;
                testCases[CreatureConstants.AnimatedObject_Small_Wheels_Wooden][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.AnimatedObject_Small_Wooden][AgeConstants.Categories.Construct] = constructAgeRoll;
                testCases[CreatureConstants.AnimatedObject_Small_Wooden][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.AnimatedObject_Tiny][AgeConstants.Categories.Construct] = constructAgeRoll;
                testCases[CreatureConstants.AnimatedObject_Tiny][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.AnimatedObject_Tiny_Flexible][AgeConstants.Categories.Construct] = constructAgeRoll;
                testCases[CreatureConstants.AnimatedObject_Tiny_Flexible][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.AnimatedObject_Tiny_MultipleLegs][AgeConstants.Categories.Construct] = constructAgeRoll;
                testCases[CreatureConstants.AnimatedObject_Tiny_MultipleLegs][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.AnimatedObject_Tiny_MultipleLegs_Wooden][AgeConstants.Categories.Construct] = constructAgeRoll;
                testCases[CreatureConstants.AnimatedObject_Tiny_MultipleLegs_Wooden][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.AnimatedObject_Tiny_Sheetlike][AgeConstants.Categories.Construct] = constructAgeRoll;
                testCases[CreatureConstants.AnimatedObject_Tiny_Sheetlike][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.AnimatedObject_Tiny_TwoLegs][AgeConstants.Categories.Construct] = constructAgeRoll;
                testCases[CreatureConstants.AnimatedObject_Tiny_TwoLegs][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.AnimatedObject_Tiny_TwoLegs_Wooden][AgeConstants.Categories.Construct] = constructAgeRoll;
                testCases[CreatureConstants.AnimatedObject_Tiny_TwoLegs_Wooden][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.AnimatedObject_Tiny_Wheels_Wooden][AgeConstants.Categories.Construct] = constructAgeRoll;
                testCases[CreatureConstants.AnimatedObject_Tiny_Wheels_Wooden][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.AnimatedObject_Tiny_Wooden][AgeConstants.Categories.Construct] = constructAgeRoll;
                testCases[CreatureConstants.AnimatedObject_Tiny_Wooden][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                //Source: https://www.worldanvil.com/w/faerun-tatortotzke/a/ankheg-species (maximum)
                //https://forgottenrealms.fandom.com/wiki/Ankheg#Life_cycle (adulthood)
                testCases[CreatureConstants.Ankheg][AgeConstants.Categories.Adulthood] = GetAdulthoodRollFromAverage(20, 2);
                testCases[CreatureConstants.Ankheg][AgeConstants.Categories.MiddleAge] = GetMiddleAgeRollFromAverage(20);
                testCases[CreatureConstants.Ankheg][AgeConstants.Categories.Old] = GetOldRollFromAverage(20);
                testCases[CreatureConstants.Ankheg][AgeConstants.Categories.Venerable] = GetVenerableRollFromAverage(20);
                testCases[CreatureConstants.Ankheg][AgeConstants.Categories.Maximum] = GetMaximumRollFromAverage(20);
                //Source: https://forgottenrealms.fandom.com/wiki/Annis#Ecology
                //https://forgottenrealms.fandom.com/wiki/Hag#Reproduction
                testCases[CreatureConstants.Annis][AgeConstants.Categories.Adulthood] = GetRoll(50, 500);
                testCases[CreatureConstants.Annis][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                //Source: https://www.dimensions.com/element/black-garden-ant-lasius-niger
                testCases[CreatureConstants.Ant_Giant_Queen][AgeConstants.Categories.Adulthood] = GetAdulthoodRoll(10, 0);
                testCases[CreatureConstants.Ant_Giant_Queen][AgeConstants.Categories.MiddleAge] = GetMiddleAgeRoll(10);
                testCases[CreatureConstants.Ant_Giant_Queen][AgeConstants.Categories.Old] = GetOldRoll(10);
                testCases[CreatureConstants.Ant_Giant_Queen][AgeConstants.Categories.Venerable] = GetVenerableRoll(10, 15);
                testCases[CreatureConstants.Ant_Giant_Queen][AgeConstants.Categories.Maximum] = GetMaximumRoll(10, 15);
                testCases[CreatureConstants.Ant_Giant_Soldier][AgeConstants.Categories.Adulthood] = "0";
                testCases[CreatureConstants.Ant_Giant_Soldier][AgeConstants.Categories.MiddleAge] = "0";
                testCases[CreatureConstants.Ant_Giant_Soldier][AgeConstants.Categories.Old] = "0";
                testCases[CreatureConstants.Ant_Giant_Soldier][AgeConstants.Categories.Venerable] = "1d2";
                testCases[CreatureConstants.Ant_Giant_Soldier][AgeConstants.Categories.Maximum] = "1d2";
                testCases[CreatureConstants.Ant_Giant_Worker][AgeConstants.Categories.Adulthood] = "0";
                testCases[CreatureConstants.Ant_Giant_Worker][AgeConstants.Categories.MiddleAge] = "0";
                testCases[CreatureConstants.Ant_Giant_Worker][AgeConstants.Categories.Old] = "0";
                testCases[CreatureConstants.Ant_Giant_Worker][AgeConstants.Categories.Venerable] = "1d2";
                testCases[CreatureConstants.Ant_Giant_Worker][AgeConstants.Categories.Maximum] = "1d2";
                //Source: https://www.dimensions.com/element/eastern-lowland-gorilla-gorilla-beringei-graueri (maximum)
                //https://www.silverbackgorillatours.com/eastern-lowland-and-grauers-gorillas (adulthood)
                testCases[CreatureConstants.Ape][AgeConstants.Categories.Adulthood] = GetAdulthoodRoll(40, 10);
                testCases[CreatureConstants.Ape][AgeConstants.Categories.MiddleAge] = GetMiddleAgeRoll(40, 30);
                testCases[CreatureConstants.Ape][AgeConstants.Categories.Old] = GetOldRoll(40, 30);
                testCases[CreatureConstants.Ape][AgeConstants.Categories.Venerable] = GetVenerableRoll(40, 60);
                testCases[CreatureConstants.Ape][AgeConstants.Categories.Maximum] = GetMaximumRoll(40, 60);
                testCases[CreatureConstants.Ape_Dire][AgeConstants.Categories.Adulthood] = GetAdulthoodRoll(40, 10);
                testCases[CreatureConstants.Ape_Dire][AgeConstants.Categories.MiddleAge] = GetMiddleAgeRoll(40, 30);
                testCases[CreatureConstants.Ape_Dire][AgeConstants.Categories.Old] = GetOldRoll(40, 30);
                testCases[CreatureConstants.Ape_Dire][AgeConstants.Categories.Venerable] = GetVenerableRoll(40, 60);
                testCases[CreatureConstants.Ape_Dire][AgeConstants.Categories.Maximum] = GetMaximumRoll(40, 60);
                //INFO: Based on Half-Elf, since could be Human, Half-Elf, or Drow
                testCases[CreatureConstants.Aranea][AgeConstants.Categories.Adulthood] = GetRoll(20, 61);
                testCases[CreatureConstants.Aranea][AgeConstants.Categories.MiddleAge] = GetRoll(62, 92);
                testCases[CreatureConstants.Aranea][AgeConstants.Categories.Old] = GetRoll(93, 124);
                testCases[CreatureConstants.Aranea][AgeConstants.Categories.Venerable] = GetRoll(125, 185);
                testCases[CreatureConstants.Aranea][AgeConstants.Categories.Maximum] = "125+3d20";
                //Source: https://forgottenrealms.fandom.com/wiki/Arrowhawk
                testCases[CreatureConstants.Arrowhawk_Adult][AgeConstants.Categories.Arrowhawk.Adult] = GetRoll(11, 40);
                testCases[CreatureConstants.Arrowhawk_Adult][AgeConstants.Categories.Maximum] = "41";
                testCases[CreatureConstants.Arrowhawk_Elder][AgeConstants.Categories.Arrowhawk.Elder] = GetRoll(41, 75);
                testCases[CreatureConstants.Arrowhawk_Elder][AgeConstants.Categories.Maximum] = "75";
                testCases[CreatureConstants.Arrowhawk_Juvenile][AgeConstants.Categories.Arrowhawk.Juvenile] = GetRoll(1, 10);
                testCases[CreatureConstants.Arrowhawk_Juvenile][AgeConstants.Categories.Maximum] = "11";
                //Can't find lifespan, so making up, V:10 (kinda like pitcher plant)
                testCases[CreatureConstants.AssassinVine][AgeConstants.Categories.Adulthood] = GetAdulthoodRoll(10);
                testCases[CreatureConstants.AssassinVine][AgeConstants.Categories.MiddleAge] = GetMiddleAgeRoll(10);
                testCases[CreatureConstants.AssassinVine][AgeConstants.Categories.Old] = GetOldRoll(10);
                testCases[CreatureConstants.AssassinVine][AgeConstants.Categories.Venerable] = GetVenerableRoll(10);
                testCases[CreatureConstants.AssassinVine][AgeConstants.Categories.Maximum] = GetMaximumRoll(10);
                //INFO: Basing off of Hill Giants, as I can't find anything about lifespan or aging
                testCases[CreatureConstants.Athach][AgeConstants.Categories.Adulthood] = GetAdulthoodRollFromAverage(200);
                testCases[CreatureConstants.Athach][AgeConstants.Categories.MiddleAge] = GetMiddleAgeRollFromAverage(200);
                testCases[CreatureConstants.Athach][AgeConstants.Categories.Old] = GetOldRollFromAverage(200);
                testCases[CreatureConstants.Athach][AgeConstants.Categories.Venerable] = GetVenerableRollFromAverage(200);
                testCases[CreatureConstants.Athach][AgeConstants.Categories.Maximum] = GetMaximumRollFromAverage(200);
                //INFO: Making up, Max age of 100
                testCases[CreatureConstants.Avoral][AgeConstants.Categories.Adulthood] = GetAdulthoodRoll(100);
                testCases[CreatureConstants.Avoral][AgeConstants.Categories.MiddleAge] = GetMiddleAgeRoll(100);
                testCases[CreatureConstants.Avoral][AgeConstants.Categories.Old] = GetOldRoll(100);
                testCases[CreatureConstants.Avoral][AgeConstants.Categories.Venerable] = GetVenerableRoll(100);
                testCases[CreatureConstants.Avoral][AgeConstants.Categories.Maximum] = GetMaximumRoll(100);
                //INFO: 5e Source: https://www.5esrd.com/database/race/azer/
                //says "born adults, lifespan "many centuries", so I will say max age of 500
                testCases[CreatureConstants.Azer][AgeConstants.Categories.Adulthood] = GetAdulthoodRoll(500, 0);
                testCases[CreatureConstants.Azer][AgeConstants.Categories.MiddleAge] = GetMiddleAgeRoll(500);
                testCases[CreatureConstants.Azer][AgeConstants.Categories.Old] = GetOldRoll(500);
                testCases[CreatureConstants.Azer][AgeConstants.Categories.Venerable] = GetVenerableRoll(500);
                testCases[CreatureConstants.Azer][AgeConstants.Categories.Maximum] = GetMaximumRoll(500);
                testCases[CreatureConstants.Babau][AgeConstants.Categories.Adulthood] = outsiderAgeRoll;
                testCases[CreatureConstants.Babau][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                //Source: https://www.dimensions.com/element/mandrill-mandrillus-sphinx (maximum)
                //https://animaldiversity.org/accounts/Mandrillus_sphinx/ (adulthood)
                testCases[CreatureConstants.Baboon][AgeConstants.Categories.Adulthood] = GetAdulthoodRoll(29, 6);
                testCases[CreatureConstants.Baboon][AgeConstants.Categories.MiddleAge] = GetMiddleAgeRoll(29);
                testCases[CreatureConstants.Baboon][AgeConstants.Categories.Old] = GetOldRoll(29);
                testCases[CreatureConstants.Baboon][AgeConstants.Categories.Venerable] = GetVenerableRoll(29, 40);
                testCases[CreatureConstants.Baboon][AgeConstants.Categories.Maximum] = GetMaximumRoll(29, 40);
                //Source: https://www.dimensions.com/element/honey-badger-mellivora-capensis (maximum)
                //https://animaldiversity.org/accounts/Mellivora_capensis/ (adulthood)
                testCases[CreatureConstants.Badger][AgeConstants.Categories.Adulthood] = GetAdulthoodRoll(7, 2);
                testCases[CreatureConstants.Badger][AgeConstants.Categories.MiddleAge] = GetMiddleAgeRoll(7);
                testCases[CreatureConstants.Badger][AgeConstants.Categories.Old] = GetOldRoll(7);
                testCases[CreatureConstants.Badger][AgeConstants.Categories.Venerable] = GetVenerableRoll(7, 26);
                testCases[CreatureConstants.Badger][AgeConstants.Categories.Maximum] = GetMaximumRoll(7, 26);
                testCases[CreatureConstants.Badger_Dire][AgeConstants.Categories.Adulthood] = GetAdulthoodRoll(7, 2);
                testCases[CreatureConstants.Badger_Dire][AgeConstants.Categories.MiddleAge] = GetMiddleAgeRoll(7);
                testCases[CreatureConstants.Badger_Dire][AgeConstants.Categories.Old] = GetOldRoll(7);
                testCases[CreatureConstants.Badger_Dire][AgeConstants.Categories.Venerable] = GetVenerableRoll(7, 26);
                testCases[CreatureConstants.Badger_Dire][AgeConstants.Categories.Maximum] = GetMaximumRoll(7, 26);
                testCases[CreatureConstants.Balor][AgeConstants.Categories.Adulthood] = outsiderAgeRoll;
                testCases[CreatureConstants.Balor][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.BarbedDevil_Hamatula][AgeConstants.Categories.Adulthood] = outsiderAgeRoll;
                testCases[CreatureConstants.BarbedDevil_Hamatula][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                //HACK: Can't find any information about Barghest aging. Since eating is how they grow, assume they won't die of old age
                testCases[CreatureConstants.Barghest][AgeConstants.Categories.Adulthood] = GetRoll(0, 100);
                testCases[CreatureConstants.Barghest][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Barghest_Greater][AgeConstants.Categories.Adulthood] = GetRoll(10, 100);
                testCases[CreatureConstants.Barghest_Greater][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                //Source: https://www.reddit.com/r/DnDBehindTheScreen/comments/35gjqf/basilisk/
                testCases[CreatureConstants.Basilisk][AgeConstants.Categories.Adulthood] = GetAdulthoodRoll(60, 3);
                testCases[CreatureConstants.Basilisk][AgeConstants.Categories.MiddleAge] = GetMiddleAgeRoll(60);
                testCases[CreatureConstants.Basilisk][AgeConstants.Categories.Old] = GetOldRoll(60);
                testCases[CreatureConstants.Basilisk][AgeConstants.Categories.Venerable] = GetVenerableRoll(60);
                testCases[CreatureConstants.Basilisk][AgeConstants.Categories.Maximum] = GetMaximumRoll(60);
                testCases[CreatureConstants.Basilisk_Greater][AgeConstants.Categories.Adulthood] = GetAdulthoodRoll(60, 3);
                testCases[CreatureConstants.Basilisk_Greater][AgeConstants.Categories.MiddleAge] = GetMiddleAgeRoll(60);
                testCases[CreatureConstants.Basilisk_Greater][AgeConstants.Categories.Old] = GetOldRoll(60);
                testCases[CreatureConstants.Basilisk_Greater][AgeConstants.Categories.Venerable] = GetVenerableRoll(60);
                testCases[CreatureConstants.Basilisk_Greater][AgeConstants.Categories.Maximum] = GetMaximumRoll(60);
                //Souce: https://www.dimensions.com/element/little-brown-bat-myotis-lucifugus (maximum)
                //https://www.vancouverwildlife.com/bats/bat-facts/ (adulthood)
                testCases[CreatureConstants.Bat][AgeConstants.Categories.Adulthood] = GetAdulthoodRoll(7, 0);
                testCases[CreatureConstants.Bat][AgeConstants.Categories.MiddleAge] = GetMiddleAgeRoll(7);
                testCases[CreatureConstants.Bat][AgeConstants.Categories.Old] = GetOldRoll(7);
                testCases[CreatureConstants.Bat][AgeConstants.Categories.Venerable] = GetVenerableRoll(7, 34);
                testCases[CreatureConstants.Bat][AgeConstants.Categories.Maximum] = GetMaximumRoll(7, 34);
                testCases[CreatureConstants.Bat_Dire][AgeConstants.Categories.Adulthood] = GetAdulthoodRoll(7, 0);
                testCases[CreatureConstants.Bat_Dire][AgeConstants.Categories.MiddleAge] = GetMiddleAgeRoll(7);
                testCases[CreatureConstants.Bat_Dire][AgeConstants.Categories.Old] = GetOldRoll(7);
                testCases[CreatureConstants.Bat_Dire][AgeConstants.Categories.Venerable] = GetVenerableRoll(7, 34);
                testCases[CreatureConstants.Bat_Dire][AgeConstants.Categories.Maximum] = GetMaximumRoll(7, 34);
                testCases[CreatureConstants.Bat_Swarm][AgeConstants.Categories.Swarm] = GetRoll(0, 14);
                testCases[CreatureConstants.Bat_Swarm][AgeConstants.Categories.Maximum] = GetMaximumRoll(0, 14);
                //Source: https://www.dimensions.com/element/grizzly-bear (maximum)
                //https://www.nwf.org/Educational-Resources/Wildlife-Guide/Mammals/Grizzly-Bear (adulthood)
                testCases[CreatureConstants.Bear_Brown][AgeConstants.Categories.Adulthood] = GetAdulthoodRoll(26, 5);
                testCases[CreatureConstants.Bear_Brown][AgeConstants.Categories.MiddleAge] = GetMiddleAgeRoll(26, 20);
                testCases[CreatureConstants.Bear_Brown][AgeConstants.Categories.Old] = GetOldRoll(25, 20);
                testCases[CreatureConstants.Bear_Brown][AgeConstants.Categories.Venerable] = GetVenerableRoll(26, 45);
                testCases[CreatureConstants.Bear_Brown][AgeConstants.Categories.Maximum] = GetMaximumRoll(26, 45);
                //Source: https://www.dimensions.com/element/american-black-bear (maximum)
                //https://www.mdwfp.com/wildlife-hunting/black-bear-program/mississippi-black-bear-ecology/reproduction/ (adulthood)
                testCases[CreatureConstants.Bear_Black][AgeConstants.Categories.Adulthood] = GetAdulthoodRoll(26, 3);
                testCases[CreatureConstants.Bear_Black][AgeConstants.Categories.MiddleAge] = GetMiddleAgeRoll(26, 20);
                testCases[CreatureConstants.Bear_Black][AgeConstants.Categories.Old] = GetOldRoll(25, 20);
                testCases[CreatureConstants.Bear_Black][AgeConstants.Categories.Venerable] = GetVenerableRoll(26, 45);
                testCases[CreatureConstants.Bear_Black][AgeConstants.Categories.Maximum] = GetMaximumRoll(26, 45);
                //Basing off of brown bear
                testCases[CreatureConstants.Bear_Dire][AgeConstants.Categories.Adulthood] = GetAdulthoodRoll(26, 5);
                testCases[CreatureConstants.Bear_Dire][AgeConstants.Categories.MiddleAge] = GetMiddleAgeRoll(26, 20);
                testCases[CreatureConstants.Bear_Dire][AgeConstants.Categories.Old] = GetOldRoll(25, 20);
                testCases[CreatureConstants.Bear_Dire][AgeConstants.Categories.Venerable] = GetVenerableRoll(26, 45);
                testCases[CreatureConstants.Bear_Dire][AgeConstants.Categories.Maximum] = GetMaximumRoll(26, 45);
                //Source: https://www.dimensions.com/element/polar-bears (maximum)
                //https://www.marinemammalcenter.org/animal-care/learn-about-marine-mammals/polar-bears (adulthood)
                testCases[CreatureConstants.Bear_Polar][AgeConstants.Categories.Adulthood] = GetAdulthoodRoll(31, 6);
                testCases[CreatureConstants.Bear_Polar][AgeConstants.Categories.MiddleAge] = GetMiddleAgeRoll(31, 20);
                testCases[CreatureConstants.Bear_Polar][AgeConstants.Categories.Old] = GetOldRoll(30, 20);
                testCases[CreatureConstants.Bear_Polar][AgeConstants.Categories.Venerable] = GetVenerableRoll(31, 45);
                testCases[CreatureConstants.Bear_Polar][AgeConstants.Categories.Maximum] = GetMaximumRoll(31, 45);
                testCases[CreatureConstants.BeardedDevil_Barbazu][AgeConstants.Categories.Adulthood] = outsiderAgeRoll;
                testCases[CreatureConstants.BeardedDevil_Barbazu][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Bebilith][AgeConstants.Categories.Adulthood] = outsiderAgeRoll;
                testCases[CreatureConstants.Bebilith][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                //Source: https://www.dimensions.com/element/western-honey-bee-apis-mellifera
                testCases[CreatureConstants.Bee_Giant][AgeConstants.Categories.Adulthood] = "0";
                testCases[CreatureConstants.Bee_Giant][AgeConstants.Categories.MiddleAge] = "0";
                testCases[CreatureConstants.Bee_Giant][AgeConstants.Categories.Old] = "0";
                testCases[CreatureConstants.Bee_Giant][AgeConstants.Categories.Venerable] = "0";
                testCases[CreatureConstants.Bee_Giant][AgeConstants.Categories.Maximum] = "0";
                //Source: https://forgottenrealms.fandom.com/wiki/Behir
                testCases[CreatureConstants.Behir][AgeConstants.Categories.Adulthood] = GetAdulthoodRoll(50);
                testCases[CreatureConstants.Behir][AgeConstants.Categories.MiddleAge] = GetMiddleAgeRoll(50);
                testCases[CreatureConstants.Behir][AgeConstants.Categories.Old] = GetOldRoll(50);
                testCases[CreatureConstants.Behir][AgeConstants.Categories.Venerable] = GetVenerableRoll(50, 60);
                testCases[CreatureConstants.Behir][AgeConstants.Categories.Maximum] = GetMaximumRoll(50, 60);
                //Source: https://forgottenrealms.fandom.com/wiki/Beholder#Life_Cycle
                testCases[CreatureConstants.Beholder][AgeConstants.Categories.Adulthood] = GetRoll(2, 89);
                testCases[CreatureConstants.Beholder][AgeConstants.Categories.MiddleAge] = GetRoll(90, 104);
                testCases[CreatureConstants.Beholder][AgeConstants.Categories.Old] = GetRoll(105, 119);
                testCases[CreatureConstants.Beholder][AgeConstants.Categories.Venerable] = GetVenerableRoll(120, 150);
                testCases[CreatureConstants.Beholder][AgeConstants.Categories.Maximum] = GetMaximumRoll(120, 150);
                testCases[CreatureConstants.Beholder_Gauth][AgeConstants.Categories.Adulthood] = GetRoll(2, 89);
                testCases[CreatureConstants.Beholder_Gauth][AgeConstants.Categories.MiddleAge] = GetRoll(90, 104);
                testCases[CreatureConstants.Beholder_Gauth][AgeConstants.Categories.Old] = GetRoll(105, 119);
                testCases[CreatureConstants.Beholder_Gauth][AgeConstants.Categories.Venerable] = GetVenerableRoll(120, 150);
                testCases[CreatureConstants.Beholder_Gauth][AgeConstants.Categories.Maximum] = GetMaximumRoll(120, 150);
                testCases[CreatureConstants.Belker][AgeConstants.Categories.Adulthood] = undeadAgeRoll;
                testCases[CreatureConstants.Belker][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                //Source: https://www.dimensions.com/element/american-bison-bison-bison (maximum)
                //https://ielc.libguides.com/sdzg/factsheets/americanbison/reproduction (adulthood)
                testCases[CreatureConstants.Bison][AgeConstants.Categories.Adulthood] = GetAdulthoodRoll(20, 2);
                testCases[CreatureConstants.Bison][AgeConstants.Categories.MiddleAge] = GetMiddleAgeRoll(20, 12);
                testCases[CreatureConstants.Bison][AgeConstants.Categories.Old] = GetRoll(12, 19);
                testCases[CreatureConstants.Bison][AgeConstants.Categories.Venerable] = GetVenerableRoll(20, 40);
                testCases[CreatureConstants.Bison][AgeConstants.Categories.Maximum] = GetMaximumRoll(20, 40);
                //Can't find original source, so counting as "making up"
                testCases[CreatureConstants.BlackPudding][AgeConstants.Categories.Adulthood] = GetRoll(0, 100);
                testCases[CreatureConstants.BlackPudding][AgeConstants.Categories.Maximum] = "101";
                testCases[CreatureConstants.BlackPudding_Elder][AgeConstants.Categories.BlackPudding.Elder] = GetRoll(101, 1_000);
                testCases[CreatureConstants.BlackPudding_Elder][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                //Source: https://www.reddit.com/r/DnDBehindTheScreen/comments/d7w6ar/blink_dogs/
                testCases[CreatureConstants.BlinkDog][AgeConstants.Categories.Adulthood] = GetAdulthoodRoll(20, 1);
                testCases[CreatureConstants.BlinkDog][AgeConstants.Categories.MiddleAge] = GetMiddleAgeRoll(20);
                testCases[CreatureConstants.BlinkDog][AgeConstants.Categories.Old] = GetOldRoll(20);
                testCases[CreatureConstants.BlinkDog][AgeConstants.Categories.Venerable] = GetVenerableRoll(20);
                testCases[CreatureConstants.BlinkDog][AgeConstants.Categories.Maximum] = GetMaximumRoll(20);
                //Source: https://www.dimensions.com/element/wild-boar (maximum)
                //https://en.wikipedia.org/wiki/Wild_boar#Social_behaviour_and_life_cycle (adulthood)
                testCases[CreatureConstants.Boar][AgeConstants.Categories.Adulthood] = GetAdulthoodRoll(15, 2);
                testCases[CreatureConstants.Boar][AgeConstants.Categories.MiddleAge] = GetMiddleAgeRoll(15, 10);
                testCases[CreatureConstants.Boar][AgeConstants.Categories.Old] = GetRoll(10, 14);
                testCases[CreatureConstants.Boar][AgeConstants.Categories.Venerable] = GetVenerableRoll(15, 20);
                testCases[CreatureConstants.Boar][AgeConstants.Categories.Maximum] = GetMaximumRoll(15, 20);
                testCases[CreatureConstants.Boar_Dire][AgeConstants.Categories.Adulthood] = GetAdulthoodRoll(15, 2);
                testCases[CreatureConstants.Boar_Dire][AgeConstants.Categories.MiddleAge] = GetMiddleAgeRoll(15, 10);
                testCases[CreatureConstants.Boar_Dire][AgeConstants.Categories.Old] = GetRoll(10, 14);
                testCases[CreatureConstants.Boar_Dire][AgeConstants.Categories.Venerable] = GetVenerableRoll(15, 20);
                testCases[CreatureConstants.Boar_Dire][AgeConstants.Categories.Maximum] = GetMaximumRoll(15, 20);
                testCases[CreatureConstants.Bodak][AgeConstants.Categories.Undead] = undeadAgeRoll;
                testCases[CreatureConstants.Bodak][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                //Source: https://web.stanford.edu/~cbross/bombbeetle.html
                testCases[CreatureConstants.BombardierBeetle_Giant][AgeConstants.Categories.Adulthood] = "0";
                testCases[CreatureConstants.BombardierBeetle_Giant][AgeConstants.Categories.MiddleAge] = "0";
                testCases[CreatureConstants.BombardierBeetle_Giant][AgeConstants.Categories.Old] = "1";
                testCases[CreatureConstants.BombardierBeetle_Giant][AgeConstants.Categories.Venerable] = "1d2+1";
                testCases[CreatureConstants.BombardierBeetle_Giant][AgeConstants.Categories.Maximum] = "1+1d2";
                testCases[CreatureConstants.BoneDevil_Osyluth][AgeConstants.Categories.Adulthood] = outsiderAgeRoll;
                testCases[CreatureConstants.BoneDevil_Osyluth][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                //Source: https://forgottenrealms.fandom.com/wiki/Bralani
                testCases[CreatureConstants.Bralani][AgeConstants.Categories.Adulthood] = GetAdulthoodRoll(450);
                testCases[CreatureConstants.Bralani][AgeConstants.Categories.MiddleAge] = GetMiddleAgeRoll(450);
                testCases[CreatureConstants.Bralani][AgeConstants.Categories.Old] = GetOldRoll(450);
                testCases[CreatureConstants.Bralani][AgeConstants.Categories.Venerable] = GetVenerableRoll(450, 650);
                testCases[CreatureConstants.Bralani][AgeConstants.Categories.Maximum] = "450+2d100";
                //Source: http://people.wku.edu/charles.plemons/ad&d/races/age.html
                testCases[CreatureConstants.Bugbear][AgeConstants.Categories.Adulthood] = GetRoll(10, 32);
                testCases[CreatureConstants.Bugbear][AgeConstants.Categories.MiddleAge] = GetRoll(33, 43);
                testCases[CreatureConstants.Bugbear][AgeConstants.Categories.Old] = GetRoll(44, 64);
                testCases[CreatureConstants.Bugbear][AgeConstants.Categories.Venerable] = GetRoll(65, 85);
                testCases[CreatureConstants.Bugbear][AgeConstants.Categories.Maximum] = "65+2d10";
                //Source: https://www.worldanvil.com/w/Drakaise-Battalion/a/bulettes-article
                testCases[CreatureConstants.Bulette][AgeConstants.Categories.Adulthood] = GetRoll(0, 1);
                testCases[CreatureConstants.Bulette][AgeConstants.Categories.MiddleAge] = "2";
                testCases[CreatureConstants.Bulette][AgeConstants.Categories.Old] = GetRoll(2, 4);
                testCases[CreatureConstants.Bulette][AgeConstants.Categories.Venerable] = "5";
                testCases[CreatureConstants.Bulette][AgeConstants.Categories.Maximum] = "5";
                //Source: https://www.dimensions.com/element/bactrian-camel (maximum)
                //https://www.pbs.org/wnet/nature/blog/camel-fact-sheet/ (adulthood)
                testCases[CreatureConstants.Camel_Bactrian][AgeConstants.Categories.Adulthood] = GetAdulthoodRoll(41, 7);
                testCases[CreatureConstants.Camel_Bactrian][AgeConstants.Categories.MiddleAge] = GetMiddleAgeRoll(41, 20);
                testCases[CreatureConstants.Camel_Bactrian][AgeConstants.Categories.Old] = GetRoll(20, 40);
                testCases[CreatureConstants.Camel_Bactrian][AgeConstants.Categories.Venerable] = GetVenerableRoll(41, 50);
                testCases[CreatureConstants.Camel_Bactrian][AgeConstants.Categories.Maximum] = "40+1d10";
                //Source: https://www.dimensions.com/element/dromedary-camel (maximum)
                //https://www.pbs.org/wnet/nature/blog/camel-fact-sheet/ (adulthood)
                testCases[CreatureConstants.Camel_Dromedary][AgeConstants.Categories.Adulthood] = GetAdulthoodRoll(40, 7);
                testCases[CreatureConstants.Camel_Dromedary][AgeConstants.Categories.MiddleAge] = GetMiddleAgeRoll(40);
                testCases[CreatureConstants.Camel_Dromedary][AgeConstants.Categories.Old] = GetOldRoll(40);
                testCases[CreatureConstants.Camel_Dromedary][AgeConstants.Categories.Venerable] = GetVenerableRoll(40, 50);
                testCases[CreatureConstants.Camel_Dromedary][AgeConstants.Categories.Maximum] = "40+1d10";
                //Source: https://www.worldanvil.com/w/faerun-tatortotzke/a/carrion-crawler-species
                testCases[CreatureConstants.CarrionCrawler][AgeConstants.Categories.Adulthood] = GetAdulthoodRollFromAverage(20);
                testCases[CreatureConstants.CarrionCrawler][AgeConstants.Categories.MiddleAge] = GetMiddleAgeRollFromAverage(20);
                testCases[CreatureConstants.CarrionCrawler][AgeConstants.Categories.Old] = GetOldRollFromAverage(20);
                testCases[CreatureConstants.CarrionCrawler][AgeConstants.Categories.Venerable] = GetVenerableRollFromAverage(20);
                testCases[CreatureConstants.CarrionCrawler][AgeConstants.Categories.Maximum] = GetMaximumRollFromAverage(20);
                //Source: https://www.dimensions.com/element/american-shorthair-cat (maximum)
                //https://www.purina.com/cats/cat-breeds/american-shorthair (adulthood)
                testCases[CreatureConstants.Cat][AgeConstants.Categories.Adulthood] = GetAdulthoodRoll(15, 3);
                testCases[CreatureConstants.Cat][AgeConstants.Categories.MiddleAge] = GetMiddleAgeRoll(15);
                testCases[CreatureConstants.Cat][AgeConstants.Categories.Old] = GetOldRoll(15);
                testCases[CreatureConstants.Cat][AgeConstants.Categories.Venerable] = GetVenerableRoll(15, 20);
                testCases[CreatureConstants.Cat][AgeConstants.Categories.Maximum] = GetMaximumRoll(15, 20);
                //Source: http://people.wku.edu/charles.plemons/ad&d/races/age.html
                testCases[CreatureConstants.Centaur][AgeConstants.Categories.Adulthood] = GetRoll(18, 36);
                testCases[CreatureConstants.Centaur][AgeConstants.Categories.MiddleAge] = GetRoll(37, 49);
                testCases[CreatureConstants.Centaur][AgeConstants.Categories.Old] = GetRoll(50, 74);
                testCases[CreatureConstants.Centaur][AgeConstants.Categories.Venerable] = GetRoll(75, 115);
                testCases[CreatureConstants.Centaur][AgeConstants.Categories.Maximum] = "75+2d20";
                //Source: https://www.dimensions.com/element/tiger-centipede-scolopendra-polymorpha
                testCases[CreatureConstants.Centipede_Monstrous_Colossal][AgeConstants.Categories.Adulthood] = GetRoll(0, 1);
                testCases[CreatureConstants.Centipede_Monstrous_Colossal][AgeConstants.Categories.MiddleAge] = "2";
                testCases[CreatureConstants.Centipede_Monstrous_Colossal][AgeConstants.Categories.Old] = "3";
                testCases[CreatureConstants.Centipede_Monstrous_Colossal][AgeConstants.Categories.Venerable] = GetRoll(4, 6);
                testCases[CreatureConstants.Centipede_Monstrous_Colossal][AgeConstants.Categories.Maximum] = "4+1d2";
                testCases[CreatureConstants.Centipede_Monstrous_Gargantuan][AgeConstants.Categories.Adulthood] = GetRoll(0, 1);
                testCases[CreatureConstants.Centipede_Monstrous_Gargantuan][AgeConstants.Categories.MiddleAge] = "2";
                testCases[CreatureConstants.Centipede_Monstrous_Gargantuan][AgeConstants.Categories.Old] = "3";
                testCases[CreatureConstants.Centipede_Monstrous_Gargantuan][AgeConstants.Categories.Venerable] = GetRoll(4, 6);
                testCases[CreatureConstants.Centipede_Monstrous_Gargantuan][AgeConstants.Categories.Maximum] = "4+1d2";
                testCases[CreatureConstants.Centipede_Monstrous_Huge][AgeConstants.Categories.Adulthood] = GetRoll(0, 1);
                testCases[CreatureConstants.Centipede_Monstrous_Huge][AgeConstants.Categories.MiddleAge] = "2";
                testCases[CreatureConstants.Centipede_Monstrous_Huge][AgeConstants.Categories.Old] = "3";
                testCases[CreatureConstants.Centipede_Monstrous_Huge][AgeConstants.Categories.Venerable] = GetRoll(4, 6);
                testCases[CreatureConstants.Centipede_Monstrous_Huge][AgeConstants.Categories.Maximum] = "4+1d2";
                testCases[CreatureConstants.Centipede_Monstrous_Large][AgeConstants.Categories.Adulthood] = GetRoll(0, 1);
                testCases[CreatureConstants.Centipede_Monstrous_Large][AgeConstants.Categories.MiddleAge] = "2";
                testCases[CreatureConstants.Centipede_Monstrous_Large][AgeConstants.Categories.Old] = "3";
                testCases[CreatureConstants.Centipede_Monstrous_Large][AgeConstants.Categories.Venerable] = GetRoll(4, 6);
                testCases[CreatureConstants.Centipede_Monstrous_Large][AgeConstants.Categories.Maximum] = "4+1d2";
                testCases[CreatureConstants.Centipede_Monstrous_Medium][AgeConstants.Categories.Adulthood] = GetRoll(0, 1);
                testCases[CreatureConstants.Centipede_Monstrous_Medium][AgeConstants.Categories.MiddleAge] = "2";
                testCases[CreatureConstants.Centipede_Monstrous_Medium][AgeConstants.Categories.Old] = "3";
                testCases[CreatureConstants.Centipede_Monstrous_Medium][AgeConstants.Categories.Venerable] = GetRoll(4, 6);
                testCases[CreatureConstants.Centipede_Monstrous_Medium][AgeConstants.Categories.Maximum] = "4+1d2";
                testCases[CreatureConstants.Centipede_Monstrous_Small][AgeConstants.Categories.Adulthood] = GetRoll(0, 1);
                testCases[CreatureConstants.Centipede_Monstrous_Small][AgeConstants.Categories.MiddleAge] = "2";
                testCases[CreatureConstants.Centipede_Monstrous_Small][AgeConstants.Categories.Old] = "3";
                testCases[CreatureConstants.Centipede_Monstrous_Small][AgeConstants.Categories.Venerable] = GetRoll(4, 6);
                testCases[CreatureConstants.Centipede_Monstrous_Small][AgeConstants.Categories.Maximum] = "4+1d2";
                testCases[CreatureConstants.Centipede_Monstrous_Tiny][AgeConstants.Categories.Adulthood] = GetRoll(0, 1);
                testCases[CreatureConstants.Centipede_Monstrous_Tiny][AgeConstants.Categories.MiddleAge] = "2";
                testCases[CreatureConstants.Centipede_Monstrous_Tiny][AgeConstants.Categories.Old] = "3";
                testCases[CreatureConstants.Centipede_Monstrous_Tiny][AgeConstants.Categories.Venerable] = GetRoll(4, 6);
                testCases[CreatureConstants.Centipede_Monstrous_Tiny][AgeConstants.Categories.Maximum] = "4+1d2";
                testCases[CreatureConstants.Centipede_Swarm][AgeConstants.Categories.Swarm] = GetRoll(0, 8);
                testCases[CreatureConstants.Centipede_Swarm][AgeConstants.Categories.Maximum] = GetMaximumRoll(0, 8);
                testCases[CreatureConstants.ChainDevil_Kyton][AgeConstants.Categories.Adulthood] = outsiderAgeRoll;
                testCases[CreatureConstants.ChainDevil_Kyton][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                //Source unknown
                testCases[CreatureConstants.ChaosBeast][AgeConstants.Categories.Adulthood] = GetRoll(0, 100);
                testCases[CreatureConstants.ChaosBeast][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                //Source: https://www.dimensions.com/element/cheetahs (maximum)
                //https://ielc.libguides.com/sdzg/factsheets/cheetah/reproduction (adulthood)
                testCases[CreatureConstants.Cheetah][AgeConstants.Categories.Adulthood] = GetAdulthoodRoll(10, 3);
                testCases[CreatureConstants.Cheetah][AgeConstants.Categories.MiddleAge] = GetMiddleAgeRoll(10);
                testCases[CreatureConstants.Cheetah][AgeConstants.Categories.Old] = GetOldRoll(10);
                testCases[CreatureConstants.Cheetah][AgeConstants.Categories.Venerable] = GetVenerableRoll(10, 12);
                testCases[CreatureConstants.Cheetah][AgeConstants.Categories.Maximum] = "10+1d2";
                //Source: https://www.worldanvil.com/w/faerun-tatortotzke/a/chimera-species
                testCases[CreatureConstants.Chimera_Black][AgeConstants.Categories.Adulthood] = GetAdulthoodRollFromAverage(60);
                testCases[CreatureConstants.Chimera_Black][AgeConstants.Categories.MiddleAge] = GetMiddleAgeRollFromAverage(60);
                testCases[CreatureConstants.Chimera_Black][AgeConstants.Categories.Old] = GetOldRollFromAverage(60);
                testCases[CreatureConstants.Chimera_Black][AgeConstants.Categories.Venerable] = GetVenerableRollFromAverage(60);
                testCases[CreatureConstants.Chimera_Black][AgeConstants.Categories.Maximum] = GetMaximumRollFromAverage(60);
                testCases[CreatureConstants.Chimera_Blue][AgeConstants.Categories.Adulthood] = GetAdulthoodRollFromAverage(60);
                testCases[CreatureConstants.Chimera_Blue][AgeConstants.Categories.MiddleAge] = GetMiddleAgeRollFromAverage(60);
                testCases[CreatureConstants.Chimera_Blue][AgeConstants.Categories.Old] = GetOldRollFromAverage(60);
                testCases[CreatureConstants.Chimera_Blue][AgeConstants.Categories.Venerable] = GetVenerableRollFromAverage(60);
                testCases[CreatureConstants.Chimera_Blue][AgeConstants.Categories.Maximum] = GetMaximumRollFromAverage(60);
                testCases[CreatureConstants.Chimera_Green][AgeConstants.Categories.Adulthood] = GetAdulthoodRollFromAverage(60);
                testCases[CreatureConstants.Chimera_Green][AgeConstants.Categories.MiddleAge] = GetMiddleAgeRollFromAverage(60);
                testCases[CreatureConstants.Chimera_Green][AgeConstants.Categories.Old] = GetOldRollFromAverage(60);
                testCases[CreatureConstants.Chimera_Green][AgeConstants.Categories.Venerable] = GetVenerableRollFromAverage(60);
                testCases[CreatureConstants.Chimera_Green][AgeConstants.Categories.Maximum] = GetMaximumRollFromAverage(60);
                testCases[CreatureConstants.Chimera_Red][AgeConstants.Categories.Adulthood] = GetAdulthoodRollFromAverage(60);
                testCases[CreatureConstants.Chimera_Red][AgeConstants.Categories.MiddleAge] = GetMiddleAgeRollFromAverage(60);
                testCases[CreatureConstants.Chimera_Red][AgeConstants.Categories.Old] = GetOldRollFromAverage(60);
                testCases[CreatureConstants.Chimera_Red][AgeConstants.Categories.Venerable] = GetVenerableRollFromAverage(60);
                testCases[CreatureConstants.Chimera_Red][AgeConstants.Categories.Maximum] = GetMaximumRollFromAverage(60);
                testCases[CreatureConstants.Chimera_White][AgeConstants.Categories.Adulthood] = GetAdulthoodRollFromAverage(60);
                testCases[CreatureConstants.Chimera_White][AgeConstants.Categories.MiddleAge] = GetMiddleAgeRollFromAverage(60);
                testCases[CreatureConstants.Chimera_White][AgeConstants.Categories.Old] = GetOldRollFromAverage(60);
                testCases[CreatureConstants.Chimera_White][AgeConstants.Categories.Venerable] = GetVenerableRollFromAverage(60);
                testCases[CreatureConstants.Chimera_White][AgeConstants.Categories.Maximum] = GetMaximumRollFromAverage(60);
                //Source: https://forgottenrealms.fandom.com/wiki/Choker (adulthood)
                //Can find adult age, but nothing about maximum age. So, making it up as V = 3 * 5
                testCases[CreatureConstants.Choker][AgeConstants.Categories.Adulthood] = GetAdulthoodRoll(15, 3);
                testCases[CreatureConstants.Choker][AgeConstants.Categories.MiddleAge] = GetMiddleAgeRoll(15);
                testCases[CreatureConstants.Choker][AgeConstants.Categories.Old] = GetOldRoll(15);
                testCases[CreatureConstants.Choker][AgeConstants.Categories.Venerable] = GetVenerableRoll(15);
                testCases[CreatureConstants.Choker][AgeConstants.Categories.Maximum] = GetMaximumRoll(15);
                //Source: unknown
                //INFO: Found a thing talking about maximum lifespan, but nothing else. Making up the rest
                testCases[CreatureConstants.Chuul][AgeConstants.Categories.Adulthood] = GetAdulthoodRoll(100);
                testCases[CreatureConstants.Chuul][AgeConstants.Categories.MiddleAge] = GetMiddleAgeRoll(100);
                testCases[CreatureConstants.Chuul][AgeConstants.Categories.Old] = GetOldRoll(100);
                testCases[CreatureConstants.Chuul][AgeConstants.Categories.Venerable] = GetVenerableRoll(100);
                testCases[CreatureConstants.Chuul][AgeConstants.Categories.Maximum] = GetMaximumRoll(100);
                //Source: https://www.worldanvil.com/w/faerun-tatortotzke/a/cloaker-species
                testCases[CreatureConstants.Cloaker][AgeConstants.Categories.Adulthood] = GetAdulthoodRollFromAverage(100);
                testCases[CreatureConstants.Cloaker][AgeConstants.Categories.MiddleAge] = GetMiddleAgeRollFromAverage(100);
                testCases[CreatureConstants.Cloaker][AgeConstants.Categories.Old] = GetOldRollFromAverage(100);
                testCases[CreatureConstants.Cloaker][AgeConstants.Categories.Venerable] = GetVenerableRollFromAverage(100);
                testCases[CreatureConstants.Cloaker][AgeConstants.Categories.Maximum] = GetMaximumRollFromAverage(100);
                //Source: https://seananmcguire.com/fgcockatrice.php
                testCases[CreatureConstants.Cockatrice][AgeConstants.Categories.Adulthood] = GetAdulthoodRoll(8);
                testCases[CreatureConstants.Cockatrice][AgeConstants.Categories.MiddleAge] = GetMiddleAgeRoll(8, 5);
                testCases[CreatureConstants.Cockatrice][AgeConstants.Categories.Old] = GetRoll(5, 7);
                testCases[CreatureConstants.Cockatrice][AgeConstants.Categories.Venerable] = GetRoll(8, 10);
                testCases[CreatureConstants.Cockatrice][AgeConstants.Categories.Maximum] = GetMaximumRoll(8, 10);
                //Source: https://dumpstatadventures.com/blog/deep-dive-the-couatl (adulthood)
                testCases[CreatureConstants.Couatl][AgeConstants.Categories.Adulthood] = $"{tenKRoll}+35";
                testCases[CreatureConstants.Couatl][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                //Source: https://pathfinder.d20srd.org/bestiary3/sphinx.html (adulthood)
                //https://www.belloflostsouls.net/2021/12/dd-monster-spotlight-androsphinx.html (maximum)
                testCases[CreatureConstants.Criosphinx][AgeConstants.Categories.Adulthood] = $"{tenKRoll}+3";
                testCases[CreatureConstants.Criosphinx][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                //Source: https://www.dimensions.com/element/nile-crocodile-crocodylus-niloticus (maximum)
                //https://en.wikipedia.org/wiki/Nile_crocodile (adulthood)
                testCases[CreatureConstants.Crocodile][AgeConstants.Categories.Adulthood] = GetAdulthoodRoll(50, 12);
                testCases[CreatureConstants.Crocodile][AgeConstants.Categories.MiddleAge] = GetMiddleAgeRoll(50);
                testCases[CreatureConstants.Crocodile][AgeConstants.Categories.Old] = GetOldRoll(50);
                testCases[CreatureConstants.Crocodile][AgeConstants.Categories.Venerable] = GetVenerableRoll(50, 80);
                testCases[CreatureConstants.Crocodile][AgeConstants.Categories.Maximum] = "50+3d10";
                //Source: https://www.dimensions.com/element/saltwater-crocodile-crocodylus-porosus
                //https://en.wikipedia.org/wiki/Saltwater_crocodile (adulthood, same as nile crocodiles)
                testCases[CreatureConstants.Crocodile_Giant][AgeConstants.Categories.Adulthood] = GetAdulthoodRoll(101, 12);
                testCases[CreatureConstants.Crocodile_Giant][AgeConstants.Categories.MiddleAge] = GetMiddleAgeRoll(101);
                testCases[CreatureConstants.Crocodile_Giant][AgeConstants.Categories.Old] = GetRoll(70, 100);
                testCases[CreatureConstants.Crocodile_Giant][AgeConstants.Categories.Venerable] = GetRoll(101, 120);
                testCases[CreatureConstants.Crocodile_Giant][AgeConstants.Categories.Maximum] = "100+2d10";
                //Source: https://www.reddit.com/r/DnDBehindTheScreen/comments/63zx1m/hydra/
                testCases[CreatureConstants.Cryohydra_5Heads][AgeConstants.Categories.Adulthood] = GetRoll(25, 25 * 20);
                testCases[CreatureConstants.Cryohydra_5Heads][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Cryohydra_6Heads][AgeConstants.Categories.Adulthood] = GetRoll(501, 1000);
                testCases[CreatureConstants.Cryohydra_6Heads][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Cryohydra_7Heads][AgeConstants.Categories.Adulthood] = GetRoll(1001, 2000);
                testCases[CreatureConstants.Cryohydra_7Heads][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Cryohydra_8Heads][AgeConstants.Categories.Adulthood] = GetRoll(2001, 4000);
                testCases[CreatureConstants.Cryohydra_8Heads][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Cryohydra_9Heads][AgeConstants.Categories.Adulthood] = GetRoll(4001, 6000);
                testCases[CreatureConstants.Cryohydra_9Heads][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Cryohydra_10Heads][AgeConstants.Categories.Adulthood] = GetRoll(6001, 8000);
                testCases[CreatureConstants.Cryohydra_10Heads][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Cryohydra_11Heads][AgeConstants.Categories.Adulthood] = GetRoll(8001, 10_000);
                testCases[CreatureConstants.Cryohydra_11Heads][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Cryohydra_12Heads][AgeConstants.Categories.Adulthood] = GetRoll(10_000, 12_000);
                testCases[CreatureConstants.Cryohydra_12Heads][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                //Source: https://forgottenrealms.fandom.com/wiki/Darkmantle
                testCases[CreatureConstants.Darkmantle][AgeConstants.Categories.Adulthood] = GetAdulthoodRollFromAverage(6, 0);
                testCases[CreatureConstants.Darkmantle][AgeConstants.Categories.MiddleAge] = GetMiddleAgeRollFromAverage(6);
                testCases[CreatureConstants.Darkmantle][AgeConstants.Categories.Old] = GetOldRollFromAverage(6);
                testCases[CreatureConstants.Darkmantle][AgeConstants.Categories.Venerable] = GetVenerableRollFromAverage(6);
                testCases[CreatureConstants.Darkmantle][AgeConstants.Categories.Maximum] = GetMaximumRollFromAverage(6);
                //Source: https://jurassicworld-evolution.fandom.com/wiki/Deinonychus
                testCases[CreatureConstants.Deinonychus][AgeConstants.Categories.Adulthood] = GetAdulthoodRoll(78);
                testCases[CreatureConstants.Deinonychus][AgeConstants.Categories.MiddleAge] = GetMiddleAgeRoll(78);
                testCases[CreatureConstants.Deinonychus][AgeConstants.Categories.Old] = GetOldRoll(78);
                testCases[CreatureConstants.Deinonychus][AgeConstants.Categories.Venerable] = GetVenerableRoll(78);
                testCases[CreatureConstants.Deinonychus][AgeConstants.Categories.Maximum] = GetMaximumRoll(78);
                //INFO: Can't find anything on lifespan. So, making it up with max age of 50
                testCases[CreatureConstants.Delver][AgeConstants.Categories.Adulthood] = GetAdulthoodRoll(50);
                testCases[CreatureConstants.Delver][AgeConstants.Categories.MiddleAge] = GetMiddleAgeRoll(50);
                testCases[CreatureConstants.Delver][AgeConstants.Categories.Old] = GetOldRoll(50);
                testCases[CreatureConstants.Delver][AgeConstants.Categories.Venerable] = GetVenerableRoll(50);
                testCases[CreatureConstants.Delver][AgeConstants.Categories.Maximum] = GetMaximumRoll(50);
                //Source (5e): https://www.5esrd.com/database/race/derro/
                testCases[CreatureConstants.Derro][AgeConstants.Categories.Adulthood] = GetAdulthoodRollFromAverage(75, 15);
                testCases[CreatureConstants.Derro][AgeConstants.Categories.MiddleAge] = GetMiddleAgeRollFromAverage(75);
                testCases[CreatureConstants.Derro][AgeConstants.Categories.Old] = GetOldRollFromAverage(75);
                testCases[CreatureConstants.Derro][AgeConstants.Categories.Venerable] = GetVenerableRollFromAverage(75);
                testCases[CreatureConstants.Derro][AgeConstants.Categories.Maximum] = GetMaximumRollFromAverage(75);
                testCases[CreatureConstants.Derro_Sane][AgeConstants.Categories.Adulthood] = GetAdulthoodRollFromAverage(75, 15);
                testCases[CreatureConstants.Derro_Sane][AgeConstants.Categories.MiddleAge] = GetMiddleAgeRollFromAverage(75);
                testCases[CreatureConstants.Derro_Sane][AgeConstants.Categories.Old] = GetOldRollFromAverage(75);
                testCases[CreatureConstants.Derro_Sane][AgeConstants.Categories.Venerable] = GetVenerableRollFromAverage(75);
                testCases[CreatureConstants.Derro_Sane][AgeConstants.Categories.Maximum] = GetMaximumRollFromAverage(75);
                //INFO: Can't find anything on lifespan. So, making it up with max age of 50
                testCases[CreatureConstants.Destrachan][AgeConstants.Categories.Adulthood] = GetAdulthoodRoll(50);
                testCases[CreatureConstants.Destrachan][AgeConstants.Categories.MiddleAge] = GetMiddleAgeRoll(50);
                testCases[CreatureConstants.Destrachan][AgeConstants.Categories.Old] = GetOldRoll(50);
                testCases[CreatureConstants.Destrachan][AgeConstants.Categories.Venerable] = GetVenerableRoll(50);
                testCases[CreatureConstants.Destrachan][AgeConstants.Categories.Maximum] = GetMaximumRoll(50);
                testCases[CreatureConstants.Devourer][AgeConstants.Categories.Undead] = undeadAgeRoll;
                testCases[CreatureConstants.Devourer][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                //INFO: Can't find anything on lifespan. So, making it up with max age of 50
                testCases[CreatureConstants.Digester][AgeConstants.Categories.Adulthood] = GetAdulthoodRoll(50);
                testCases[CreatureConstants.Digester][AgeConstants.Categories.MiddleAge] = GetMiddleAgeRoll(50);
                testCases[CreatureConstants.Digester][AgeConstants.Categories.Old] = GetOldRoll(50);
                testCases[CreatureConstants.Digester][AgeConstants.Categories.Venerable] = GetVenerableRoll(50);
                testCases[CreatureConstants.Digester][AgeConstants.Categories.Maximum] = GetMaximumRoll(50);
                //Source: https://forgottenrealms.fandom.com/wiki/Displacer_beast
                testCases[CreatureConstants.DisplacerBeast][AgeConstants.Categories.Adulthood] = GetAdulthoodRollFromAverage(100, 1);
                testCases[CreatureConstants.DisplacerBeast][AgeConstants.Categories.MiddleAge] = GetMiddleAgeRollFromAverage(100);
                testCases[CreatureConstants.DisplacerBeast][AgeConstants.Categories.Old] = GetOldRollFromAverage(100);
                testCases[CreatureConstants.DisplacerBeast][AgeConstants.Categories.Venerable] = GetVenerableRollFromAverage(100);
                testCases[CreatureConstants.DisplacerBeast][AgeConstants.Categories.Maximum] = GetMaximumRollFromAverage(100);
                testCases[CreatureConstants.DisplacerBeast_PackLord][AgeConstants.Categories.Adulthood] = GetAdulthoodRollFromAverage(100, 1);
                testCases[CreatureConstants.DisplacerBeast_PackLord][AgeConstants.Categories.MiddleAge] = GetMiddleAgeRollFromAverage(100);
                testCases[CreatureConstants.DisplacerBeast_PackLord][AgeConstants.Categories.Old] = GetOldRollFromAverage(100);
                testCases[CreatureConstants.DisplacerBeast_PackLord][AgeConstants.Categories.Venerable] = GetVenerableRollFromAverage(100);
                testCases[CreatureConstants.DisplacerBeast_PackLord][AgeConstants.Categories.Maximum] = GetMaximumRollFromAverage(100);
                testCases[CreatureConstants.Djinni][AgeConstants.Categories.Adulthood] = outsiderAgeRoll;
                testCases[CreatureConstants.Djinni][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Djinni_Noble][AgeConstants.Categories.Adulthood] = outsiderAgeRoll;
                testCases[CreatureConstants.Djinni_Noble][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                //Source: https://www.dimensions.com/element/eastern-coyote W:10-15,C:->20
                //https://animaldiversity.org/accounts/Canis_latrans/ (adulthood)
                testCases[CreatureConstants.Dog][AgeConstants.Categories.Adulthood] = GetAdulthoodRoll(16, 1);
                testCases[CreatureConstants.Dog][AgeConstants.Categories.MiddleAge] = GetMiddleAgeRoll(16, 10);
                testCases[CreatureConstants.Dog][AgeConstants.Categories.Old] = GetRoll(10, 15);
                testCases[CreatureConstants.Dog][AgeConstants.Categories.Venerable] = GetRoll(16, 20);
                testCases[CreatureConstants.Dog][AgeConstants.Categories.Maximum] = GetMaximumRoll(16, 20);
                //Source: https://www.dimensions.com/element/saint-bernard-dog 8-10
                //https://www.dimensions.com/element/siberian-husky 12-14
                //https://www.dimensions.com/element/dogs-collie 14-16
                testCases[CreatureConstants.Dog_Riding][AgeConstants.Categories.Adulthood] = GetAdulthoodRoll(8, 1);
                testCases[CreatureConstants.Dog_Riding][AgeConstants.Categories.MiddleAge] = GetMiddleAgeRoll(8);
                testCases[CreatureConstants.Dog_Riding][AgeConstants.Categories.Old] = GetOldRoll(8);
                testCases[CreatureConstants.Dog_Riding][AgeConstants.Categories.Venerable] = GetRoll(8, 16);
                testCases[CreatureConstants.Dog_Riding][AgeConstants.Categories.Maximum] = GetMaximumRoll(8, 16);
                //Source: https://www.dimensions.com/element/donkey-equus-africanus-asinus (maximum)
                //https://www.thedonkeysanctuary.org.uk/research/taxonomy/term/153 (adulthood)
                testCases[CreatureConstants.Donkey][AgeConstants.Categories.Adulthood] = GetAdulthoodRoll(25, 3);
                testCases[CreatureConstants.Donkey][AgeConstants.Categories.MiddleAge] = GetMiddleAgeRoll(25);
                testCases[CreatureConstants.Donkey][AgeConstants.Categories.Old] = GetOldRoll(25);
                testCases[CreatureConstants.Donkey][AgeConstants.Categories.Venerable] = GetRoll(25, 40);
                testCases[CreatureConstants.Donkey][AgeConstants.Categories.Maximum] = GetMaximumRoll(25, 40);
                //Source: https://www.worldanvil.com/w/faerun-tatortotzke/a/doppelganger-species
                testCases[CreatureConstants.Doppelganger][AgeConstants.Categories.Adulthood] = GetAdulthoodRollFromAverage(90);
                testCases[CreatureConstants.Doppelganger][AgeConstants.Categories.MiddleAge] = GetMiddleAgeRollFromAverage(90);
                testCases[CreatureConstants.Doppelganger][AgeConstants.Categories.Old] = GetOldRollFromAverage(90);
                testCases[CreatureConstants.Doppelganger][AgeConstants.Categories.Venerable] = GetVenerableRollFromAverage(90);
                testCases[CreatureConstants.Doppelganger][AgeConstants.Categories.Maximum] = GetMaximumRollFromAverage(90);
                //Source: https://www.d20srd.org/srd/monsters/dragonTrue.htm
                //Maximum from the Draconomicon
                testCases[CreatureConstants.Dragon_Black_Wyrmling][AgeConstants.Categories.Dragon.Wyrmling] = GetRoll(0, 5);
                testCases[CreatureConstants.Dragon_Black_Wyrmling][AgeConstants.Categories.Maximum] = "6";
                testCases[CreatureConstants.Dragon_Black_VeryYoung][AgeConstants.Categories.Dragon.VeryYoung] = GetRoll(6, 15);
                testCases[CreatureConstants.Dragon_Black_VeryYoung][AgeConstants.Categories.Maximum] = "16";
                testCases[CreatureConstants.Dragon_Black_Young][AgeConstants.Categories.Dragon.Young] = GetRoll(16, 25);
                testCases[CreatureConstants.Dragon_Black_Young][AgeConstants.Categories.Maximum] = "26";
                testCases[CreatureConstants.Dragon_Black_Juvenile][AgeConstants.Categories.Dragon.Juvenile] = GetRoll(26, 50);
                testCases[CreatureConstants.Dragon_Black_Juvenile][AgeConstants.Categories.Maximum] = "51";
                testCases[CreatureConstants.Dragon_Black_YoungAdult][AgeConstants.Categories.Dragon.YoungAdult] = GetRoll(51, 100);
                testCases[CreatureConstants.Dragon_Black_YoungAdult][AgeConstants.Categories.Maximum] = "101";
                testCases[CreatureConstants.Dragon_Black_Adult][AgeConstants.Categories.Dragon.Adult] = GetRoll(101, 200);
                testCases[CreatureConstants.Dragon_Black_Adult][AgeConstants.Categories.Maximum] = "201";
                testCases[CreatureConstants.Dragon_Black_MatureAdult][AgeConstants.Categories.Dragon.MatureAdult] = GetRoll(201, 400);
                testCases[CreatureConstants.Dragon_Black_MatureAdult][AgeConstants.Categories.Maximum] = "401";
                testCases[CreatureConstants.Dragon_Black_Old][AgeConstants.Categories.Dragon.Old] = GetRoll(401, 600);
                testCases[CreatureConstants.Dragon_Black_Old][AgeConstants.Categories.Maximum] = "601";
                testCases[CreatureConstants.Dragon_Black_VeryOld][AgeConstants.Categories.Dragon.VeryOld] = GetRoll(601, 800);
                testCases[CreatureConstants.Dragon_Black_VeryOld][AgeConstants.Categories.Maximum] = "801";
                testCases[CreatureConstants.Dragon_Black_Ancient][AgeConstants.Categories.Dragon.Ancient] = GetRoll(801, 1000);
                testCases[CreatureConstants.Dragon_Black_Ancient][AgeConstants.Categories.Maximum] = "1001";
                testCases[CreatureConstants.Dragon_Black_Wyrm][AgeConstants.Categories.Dragon.Wyrm] = GetRoll(1001, 1200);
                testCases[CreatureConstants.Dragon_Black_Wyrm][AgeConstants.Categories.Maximum] = "1201";
                testCases[CreatureConstants.Dragon_Black_GreatWyrm][AgeConstants.Categories.Dragon.GreatWyrm] = GetRoll(1201, 2200);
                testCases[CreatureConstants.Dragon_Black_GreatWyrm][AgeConstants.Categories.Maximum] = "2201";
                testCases[CreatureConstants.Dragon_Blue_Wyrmling][AgeConstants.Categories.Dragon.Wyrmling] = GetRoll(0, 5);
                testCases[CreatureConstants.Dragon_Blue_Wyrmling][AgeConstants.Categories.Maximum] = "6";
                testCases[CreatureConstants.Dragon_Blue_VeryYoung][AgeConstants.Categories.Dragon.VeryYoung] = GetRoll(6, 15);
                testCases[CreatureConstants.Dragon_Blue_VeryYoung][AgeConstants.Categories.Maximum] = "16";
                testCases[CreatureConstants.Dragon_Blue_Young][AgeConstants.Categories.Dragon.Young] = GetRoll(16, 25);
                testCases[CreatureConstants.Dragon_Blue_Young][AgeConstants.Categories.Maximum] = "26";
                testCases[CreatureConstants.Dragon_Blue_Juvenile][AgeConstants.Categories.Dragon.Juvenile] = GetRoll(26, 50);
                testCases[CreatureConstants.Dragon_Blue_Juvenile][AgeConstants.Categories.Maximum] = "51";
                testCases[CreatureConstants.Dragon_Blue_YoungAdult][AgeConstants.Categories.Dragon.YoungAdult] = GetRoll(51, 100);
                testCases[CreatureConstants.Dragon_Blue_YoungAdult][AgeConstants.Categories.Maximum] = "101";
                testCases[CreatureConstants.Dragon_Blue_Adult][AgeConstants.Categories.Dragon.Adult] = GetRoll(101, 200);
                testCases[CreatureConstants.Dragon_Blue_Adult][AgeConstants.Categories.Maximum] = "201";
                testCases[CreatureConstants.Dragon_Blue_MatureAdult][AgeConstants.Categories.Dragon.MatureAdult] = GetRoll(201, 400);
                testCases[CreatureConstants.Dragon_Blue_MatureAdult][AgeConstants.Categories.Maximum] = "401";
                testCases[CreatureConstants.Dragon_Blue_Old][AgeConstants.Categories.Dragon.Old] = GetRoll(401, 600);
                testCases[CreatureConstants.Dragon_Blue_Old][AgeConstants.Categories.Maximum] = "601";
                testCases[CreatureConstants.Dragon_Blue_VeryOld][AgeConstants.Categories.Dragon.VeryOld] = GetRoll(601, 800);
                testCases[CreatureConstants.Dragon_Blue_VeryOld][AgeConstants.Categories.Maximum] = "801";
                testCases[CreatureConstants.Dragon_Blue_Ancient][AgeConstants.Categories.Dragon.Ancient] = GetRoll(801, 1000);
                testCases[CreatureConstants.Dragon_Blue_Ancient][AgeConstants.Categories.Maximum] = "1001";
                testCases[CreatureConstants.Dragon_Blue_Wyrm][AgeConstants.Categories.Dragon.Wyrm] = GetRoll(1001, 1200);
                testCases[CreatureConstants.Dragon_Blue_Wyrm][AgeConstants.Categories.Maximum] = "1201";
                testCases[CreatureConstants.Dragon_Blue_GreatWyrm][AgeConstants.Categories.Dragon.GreatWyrm] = GetRoll(1201, 2300);
                testCases[CreatureConstants.Dragon_Blue_GreatWyrm][AgeConstants.Categories.Maximum] = "2301";
                testCases[CreatureConstants.Dragon_Brass_Wyrmling][AgeConstants.Categories.Dragon.Wyrmling] = GetRoll(0, 5);
                testCases[CreatureConstants.Dragon_Brass_Wyrmling][AgeConstants.Categories.Maximum] = "6";
                testCases[CreatureConstants.Dragon_Brass_VeryYoung][AgeConstants.Categories.Dragon.VeryYoung] = GetRoll(6, 15);
                testCases[CreatureConstants.Dragon_Brass_VeryYoung][AgeConstants.Categories.Maximum] = "16";
                testCases[CreatureConstants.Dragon_Brass_Young][AgeConstants.Categories.Dragon.Young] = GetRoll(16, 25);
                testCases[CreatureConstants.Dragon_Brass_Young][AgeConstants.Categories.Maximum] = "26";
                testCases[CreatureConstants.Dragon_Brass_Juvenile][AgeConstants.Categories.Dragon.Juvenile] = GetRoll(26, 50);
                testCases[CreatureConstants.Dragon_Brass_Juvenile][AgeConstants.Categories.Maximum] = "51";
                testCases[CreatureConstants.Dragon_Brass_YoungAdult][AgeConstants.Categories.Dragon.YoungAdult] = GetRoll(51, 100);
                testCases[CreatureConstants.Dragon_Brass_YoungAdult][AgeConstants.Categories.Maximum] = "101";
                testCases[CreatureConstants.Dragon_Brass_Adult][AgeConstants.Categories.Dragon.Adult] = GetRoll(101, 200);
                testCases[CreatureConstants.Dragon_Brass_Adult][AgeConstants.Categories.Maximum] = "201";
                testCases[CreatureConstants.Dragon_Brass_MatureAdult][AgeConstants.Categories.Dragon.MatureAdult] = GetRoll(201, 400);
                testCases[CreatureConstants.Dragon_Brass_MatureAdult][AgeConstants.Categories.Maximum] = "401";
                testCases[CreatureConstants.Dragon_Brass_Old][AgeConstants.Categories.Dragon.Old] = GetRoll(401, 600);
                testCases[CreatureConstants.Dragon_Brass_Old][AgeConstants.Categories.Maximum] = "601";
                testCases[CreatureConstants.Dragon_Brass_VeryOld][AgeConstants.Categories.Dragon.VeryOld] = GetRoll(601, 800);
                testCases[CreatureConstants.Dragon_Brass_VeryOld][AgeConstants.Categories.Maximum] = "801";
                testCases[CreatureConstants.Dragon_Brass_Ancient][AgeConstants.Categories.Dragon.Ancient] = GetRoll(801, 1000);
                testCases[CreatureConstants.Dragon_Brass_Ancient][AgeConstants.Categories.Maximum] = "1001";
                testCases[CreatureConstants.Dragon_Brass_Wyrm][AgeConstants.Categories.Dragon.Wyrm] = GetRoll(1001, 1200);
                testCases[CreatureConstants.Dragon_Brass_Wyrm][AgeConstants.Categories.Maximum] = "1201";
                testCases[CreatureConstants.Dragon_Brass_GreatWyrm][AgeConstants.Categories.Dragon.GreatWyrm] = GetRoll(1201, 3200);
                testCases[CreatureConstants.Dragon_Brass_GreatWyrm][AgeConstants.Categories.Maximum] = "3201";
                testCases[CreatureConstants.Dragon_Bronze_Wyrmling][AgeConstants.Categories.Dragon.Wyrmling] = GetRoll(0, 5);
                testCases[CreatureConstants.Dragon_Bronze_Wyrmling][AgeConstants.Categories.Maximum] = "6";
                testCases[CreatureConstants.Dragon_Bronze_VeryYoung][AgeConstants.Categories.Dragon.VeryYoung] = GetRoll(6, 15);
                testCases[CreatureConstants.Dragon_Bronze_VeryYoung][AgeConstants.Categories.Maximum] = "16";
                testCases[CreatureConstants.Dragon_Bronze_Young][AgeConstants.Categories.Dragon.Young] = GetRoll(16, 25);
                testCases[CreatureConstants.Dragon_Bronze_Young][AgeConstants.Categories.Maximum] = "26";
                testCases[CreatureConstants.Dragon_Bronze_Juvenile][AgeConstants.Categories.Dragon.Juvenile] = GetRoll(26, 50);
                testCases[CreatureConstants.Dragon_Bronze_Juvenile][AgeConstants.Categories.Maximum] = "51";
                testCases[CreatureConstants.Dragon_Bronze_YoungAdult][AgeConstants.Categories.Dragon.YoungAdult] = GetRoll(51, 100);
                testCases[CreatureConstants.Dragon_Bronze_YoungAdult][AgeConstants.Categories.Maximum] = "101";
                testCases[CreatureConstants.Dragon_Bronze_Adult][AgeConstants.Categories.Dragon.Adult] = GetRoll(101, 200);
                testCases[CreatureConstants.Dragon_Bronze_Adult][AgeConstants.Categories.Maximum] = "201";
                testCases[CreatureConstants.Dragon_Bronze_MatureAdult][AgeConstants.Categories.Dragon.MatureAdult] = GetRoll(201, 400);
                testCases[CreatureConstants.Dragon_Bronze_MatureAdult][AgeConstants.Categories.Maximum] = "401";
                testCases[CreatureConstants.Dragon_Bronze_Old][AgeConstants.Categories.Dragon.Old] = GetRoll(401, 600);
                testCases[CreatureConstants.Dragon_Bronze_Old][AgeConstants.Categories.Maximum] = "601";
                testCases[CreatureConstants.Dragon_Bronze_VeryOld][AgeConstants.Categories.Dragon.VeryOld] = GetRoll(601, 800);
                testCases[CreatureConstants.Dragon_Bronze_VeryOld][AgeConstants.Categories.Maximum] = "801";
                testCases[CreatureConstants.Dragon_Bronze_Ancient][AgeConstants.Categories.Dragon.Ancient] = GetRoll(801, 1000);
                testCases[CreatureConstants.Dragon_Bronze_Ancient][AgeConstants.Categories.Maximum] = "1001";
                testCases[CreatureConstants.Dragon_Bronze_Wyrm][AgeConstants.Categories.Dragon.Wyrm] = GetRoll(1001, 1200);
                testCases[CreatureConstants.Dragon_Bronze_Wyrm][AgeConstants.Categories.Maximum] = "1201";
                testCases[CreatureConstants.Dragon_Bronze_GreatWyrm][AgeConstants.Categories.Dragon.GreatWyrm] = GetRoll(1201, 3800);
                testCases[CreatureConstants.Dragon_Bronze_GreatWyrm][AgeConstants.Categories.Maximum] = "3801";
                testCases[CreatureConstants.Dragon_Copper_Wyrmling][AgeConstants.Categories.Dragon.Wyrmling] = GetRoll(0, 5);
                testCases[CreatureConstants.Dragon_Copper_Wyrmling][AgeConstants.Categories.Maximum] = "6";
                testCases[CreatureConstants.Dragon_Copper_VeryYoung][AgeConstants.Categories.Dragon.VeryYoung] = GetRoll(6, 15);
                testCases[CreatureConstants.Dragon_Copper_VeryYoung][AgeConstants.Categories.Maximum] = "16";
                testCases[CreatureConstants.Dragon_Copper_Young][AgeConstants.Categories.Dragon.Young] = GetRoll(16, 25);
                testCases[CreatureConstants.Dragon_Copper_Young][AgeConstants.Categories.Maximum] = "26";
                testCases[CreatureConstants.Dragon_Copper_Juvenile][AgeConstants.Categories.Dragon.Juvenile] = GetRoll(26, 50);
                testCases[CreatureConstants.Dragon_Copper_Juvenile][AgeConstants.Categories.Maximum] = "51";
                testCases[CreatureConstants.Dragon_Copper_YoungAdult][AgeConstants.Categories.Dragon.YoungAdult] = GetRoll(51, 100);
                testCases[CreatureConstants.Dragon_Copper_YoungAdult][AgeConstants.Categories.Maximum] = "101";
                testCases[CreatureConstants.Dragon_Copper_Adult][AgeConstants.Categories.Dragon.Adult] = GetRoll(101, 200);
                testCases[CreatureConstants.Dragon_Copper_Adult][AgeConstants.Categories.Maximum] = "201";
                testCases[CreatureConstants.Dragon_Copper_MatureAdult][AgeConstants.Categories.Dragon.MatureAdult] = GetRoll(201, 400);
                testCases[CreatureConstants.Dragon_Copper_MatureAdult][AgeConstants.Categories.Maximum] = "401";
                testCases[CreatureConstants.Dragon_Copper_Old][AgeConstants.Categories.Dragon.Old] = GetRoll(401, 600);
                testCases[CreatureConstants.Dragon_Copper_Old][AgeConstants.Categories.Maximum] = "601";
                testCases[CreatureConstants.Dragon_Copper_VeryOld][AgeConstants.Categories.Dragon.VeryOld] = GetRoll(601, 800);
                testCases[CreatureConstants.Dragon_Copper_VeryOld][AgeConstants.Categories.Maximum] = "801";
                testCases[CreatureConstants.Dragon_Copper_Ancient][AgeConstants.Categories.Dragon.Ancient] = GetRoll(801, 1000);
                testCases[CreatureConstants.Dragon_Copper_Ancient][AgeConstants.Categories.Maximum] = "1001";
                testCases[CreatureConstants.Dragon_Copper_Wyrm][AgeConstants.Categories.Dragon.Wyrm] = GetRoll(1001, 1200);
                testCases[CreatureConstants.Dragon_Copper_Wyrm][AgeConstants.Categories.Maximum] = "1201";
                testCases[CreatureConstants.Dragon_Copper_GreatWyrm][AgeConstants.Categories.Dragon.GreatWyrm] = GetRoll(1201, 3400);
                testCases[CreatureConstants.Dragon_Copper_GreatWyrm][AgeConstants.Categories.Maximum] = "3401";
                testCases[CreatureConstants.Dragon_Green_Wyrmling][AgeConstants.Categories.Dragon.Wyrmling] = GetRoll(0, 5);
                testCases[CreatureConstants.Dragon_Green_Wyrmling][AgeConstants.Categories.Maximum] = "6";
                testCases[CreatureConstants.Dragon_Green_VeryYoung][AgeConstants.Categories.Dragon.VeryYoung] = GetRoll(6, 15);
                testCases[CreatureConstants.Dragon_Green_VeryYoung][AgeConstants.Categories.Maximum] = "16";
                testCases[CreatureConstants.Dragon_Green_Young][AgeConstants.Categories.Dragon.Young] = GetRoll(16, 25);
                testCases[CreatureConstants.Dragon_Green_Young][AgeConstants.Categories.Maximum] = "26";
                testCases[CreatureConstants.Dragon_Green_Juvenile][AgeConstants.Categories.Dragon.Juvenile] = GetRoll(26, 50);
                testCases[CreatureConstants.Dragon_Green_Juvenile][AgeConstants.Categories.Maximum] = "51";
                testCases[CreatureConstants.Dragon_Green_YoungAdult][AgeConstants.Categories.Dragon.YoungAdult] = GetRoll(51, 100);
                testCases[CreatureConstants.Dragon_Green_YoungAdult][AgeConstants.Categories.Maximum] = "101";
                testCases[CreatureConstants.Dragon_Green_Adult][AgeConstants.Categories.Dragon.Adult] = GetRoll(101, 200);
                testCases[CreatureConstants.Dragon_Green_Adult][AgeConstants.Categories.Maximum] = "201";
                testCases[CreatureConstants.Dragon_Green_MatureAdult][AgeConstants.Categories.Dragon.MatureAdult] = GetRoll(201, 400);
                testCases[CreatureConstants.Dragon_Green_MatureAdult][AgeConstants.Categories.Maximum] = "401";
                testCases[CreatureConstants.Dragon_Green_Old][AgeConstants.Categories.Dragon.Old] = GetRoll(401, 600);
                testCases[CreatureConstants.Dragon_Green_Old][AgeConstants.Categories.Maximum] = "601";
                testCases[CreatureConstants.Dragon_Green_VeryOld][AgeConstants.Categories.Dragon.VeryOld] = GetRoll(601, 800);
                testCases[CreatureConstants.Dragon_Green_VeryOld][AgeConstants.Categories.Maximum] = "801";
                testCases[CreatureConstants.Dragon_Green_Ancient][AgeConstants.Categories.Dragon.Ancient] = GetRoll(801, 1000);
                testCases[CreatureConstants.Dragon_Green_Ancient][AgeConstants.Categories.Maximum] = "1001";
                testCases[CreatureConstants.Dragon_Green_Wyrm][AgeConstants.Categories.Dragon.Wyrm] = GetRoll(1001, 1200);
                testCases[CreatureConstants.Dragon_Green_Wyrm][AgeConstants.Categories.Maximum] = "1201";
                testCases[CreatureConstants.Dragon_Green_GreatWyrm][AgeConstants.Categories.Dragon.GreatWyrm] = GetRoll(1201, 2300);
                testCases[CreatureConstants.Dragon_Green_GreatWyrm][AgeConstants.Categories.Maximum] = "2301";
                testCases[CreatureConstants.Dragon_Gold_Wyrmling][AgeConstants.Categories.Dragon.Wyrmling] = GetRoll(0, 5);
                testCases[CreatureConstants.Dragon_Gold_Wyrmling][AgeConstants.Categories.Maximum] = "6";
                testCases[CreatureConstants.Dragon_Gold_VeryYoung][AgeConstants.Categories.Dragon.VeryYoung] = GetRoll(6, 15);
                testCases[CreatureConstants.Dragon_Gold_VeryYoung][AgeConstants.Categories.Maximum] = "16";
                testCases[CreatureConstants.Dragon_Gold_Young][AgeConstants.Categories.Dragon.Young] = GetRoll(16, 25);
                testCases[CreatureConstants.Dragon_Gold_Young][AgeConstants.Categories.Maximum] = "26";
                testCases[CreatureConstants.Dragon_Gold_Juvenile][AgeConstants.Categories.Dragon.Juvenile] = GetRoll(26, 50);
                testCases[CreatureConstants.Dragon_Gold_Juvenile][AgeConstants.Categories.Maximum] = "51";
                testCases[CreatureConstants.Dragon_Gold_YoungAdult][AgeConstants.Categories.Dragon.YoungAdult] = GetRoll(51, 100);
                testCases[CreatureConstants.Dragon_Gold_YoungAdult][AgeConstants.Categories.Maximum] = "101";
                testCases[CreatureConstants.Dragon_Gold_Adult][AgeConstants.Categories.Dragon.Adult] = GetRoll(101, 200);
                testCases[CreatureConstants.Dragon_Gold_Adult][AgeConstants.Categories.Maximum] = "201";
                testCases[CreatureConstants.Dragon_Gold_MatureAdult][AgeConstants.Categories.Dragon.MatureAdult] = GetRoll(201, 400);
                testCases[CreatureConstants.Dragon_Gold_MatureAdult][AgeConstants.Categories.Maximum] = "401";
                testCases[CreatureConstants.Dragon_Gold_Old][AgeConstants.Categories.Dragon.Old] = GetRoll(401, 600);
                testCases[CreatureConstants.Dragon_Gold_Old][AgeConstants.Categories.Maximum] = "601";
                testCases[CreatureConstants.Dragon_Gold_VeryOld][AgeConstants.Categories.Dragon.VeryOld] = GetRoll(601, 800);
                testCases[CreatureConstants.Dragon_Gold_VeryOld][AgeConstants.Categories.Maximum] = "801";
                testCases[CreatureConstants.Dragon_Gold_Ancient][AgeConstants.Categories.Dragon.Ancient] = GetRoll(801, 1000);
                testCases[CreatureConstants.Dragon_Gold_Ancient][AgeConstants.Categories.Maximum] = "1001";
                testCases[CreatureConstants.Dragon_Gold_Wyrm][AgeConstants.Categories.Dragon.Wyrm] = GetRoll(1001, 1200);
                testCases[CreatureConstants.Dragon_Gold_Wyrm][AgeConstants.Categories.Maximum] = "1201";
                testCases[CreatureConstants.Dragon_Gold_GreatWyrm][AgeConstants.Categories.Dragon.GreatWyrm] = GetRoll(1201, 4400);
                testCases[CreatureConstants.Dragon_Gold_GreatWyrm][AgeConstants.Categories.Maximum] = "4401";
                testCases[CreatureConstants.Dragon_Red_Wyrmling][AgeConstants.Categories.Dragon.Wyrmling] = GetRoll(0, 5);
                testCases[CreatureConstants.Dragon_Red_Wyrmling][AgeConstants.Categories.Maximum] = "6";
                testCases[CreatureConstants.Dragon_Red_VeryYoung][AgeConstants.Categories.Dragon.VeryYoung] = GetRoll(6, 15);
                testCases[CreatureConstants.Dragon_Red_VeryYoung][AgeConstants.Categories.Maximum] = "16";
                testCases[CreatureConstants.Dragon_Red_Young][AgeConstants.Categories.Dragon.Young] = GetRoll(16, 25);
                testCases[CreatureConstants.Dragon_Red_Young][AgeConstants.Categories.Maximum] = "26";
                testCases[CreatureConstants.Dragon_Red_Juvenile][AgeConstants.Categories.Dragon.Juvenile] = GetRoll(26, 50);
                testCases[CreatureConstants.Dragon_Red_Juvenile][AgeConstants.Categories.Maximum] = "51";
                testCases[CreatureConstants.Dragon_Red_YoungAdult][AgeConstants.Categories.Dragon.YoungAdult] = GetRoll(51, 100);
                testCases[CreatureConstants.Dragon_Red_YoungAdult][AgeConstants.Categories.Maximum] = "101";
                testCases[CreatureConstants.Dragon_Red_Adult][AgeConstants.Categories.Dragon.Adult] = GetRoll(101, 200);
                testCases[CreatureConstants.Dragon_Red_Adult][AgeConstants.Categories.Maximum] = "201";
                testCases[CreatureConstants.Dragon_Red_MatureAdult][AgeConstants.Categories.Dragon.MatureAdult] = GetRoll(201, 400);
                testCases[CreatureConstants.Dragon_Red_MatureAdult][AgeConstants.Categories.Maximum] = "401";
                testCases[CreatureConstants.Dragon_Red_Old][AgeConstants.Categories.Dragon.Old] = GetRoll(401, 600);
                testCases[CreatureConstants.Dragon_Red_Old][AgeConstants.Categories.Maximum] = "601";
                testCases[CreatureConstants.Dragon_Red_VeryOld][AgeConstants.Categories.Dragon.VeryOld] = GetRoll(601, 800);
                testCases[CreatureConstants.Dragon_Red_VeryOld][AgeConstants.Categories.Maximum] = "801";
                testCases[CreatureConstants.Dragon_Red_Ancient][AgeConstants.Categories.Dragon.Ancient] = GetRoll(801, 1000);
                testCases[CreatureConstants.Dragon_Red_Ancient][AgeConstants.Categories.Maximum] = "1001";
                testCases[CreatureConstants.Dragon_Red_Wyrm][AgeConstants.Categories.Dragon.Wyrm] = GetRoll(1001, 1200);
                testCases[CreatureConstants.Dragon_Red_Wyrm][AgeConstants.Categories.Maximum] = "1201";
                testCases[CreatureConstants.Dragon_Red_GreatWyrm][AgeConstants.Categories.Dragon.GreatWyrm] = GetRoll(1201, 2500);
                testCases[CreatureConstants.Dragon_Red_GreatWyrm][AgeConstants.Categories.Maximum] = "2501";
                testCases[CreatureConstants.Dragon_Silver_Wyrmling][AgeConstants.Categories.Dragon.Wyrmling] = GetRoll(0, 5);
                testCases[CreatureConstants.Dragon_Silver_Wyrmling][AgeConstants.Categories.Maximum] = "6";
                testCases[CreatureConstants.Dragon_Silver_VeryYoung][AgeConstants.Categories.Dragon.VeryYoung] = GetRoll(6, 15);
                testCases[CreatureConstants.Dragon_Silver_VeryYoung][AgeConstants.Categories.Maximum] = "16";
                testCases[CreatureConstants.Dragon_Silver_Young][AgeConstants.Categories.Dragon.Young] = GetRoll(16, 25);
                testCases[CreatureConstants.Dragon_Silver_Young][AgeConstants.Categories.Maximum] = "26";
                testCases[CreatureConstants.Dragon_Silver_Juvenile][AgeConstants.Categories.Dragon.Juvenile] = GetRoll(26, 50);
                testCases[CreatureConstants.Dragon_Silver_Juvenile][AgeConstants.Categories.Maximum] = "51";
                testCases[CreatureConstants.Dragon_Silver_YoungAdult][AgeConstants.Categories.Dragon.YoungAdult] = GetRoll(51, 100);
                testCases[CreatureConstants.Dragon_Silver_YoungAdult][AgeConstants.Categories.Maximum] = "101";
                testCases[CreatureConstants.Dragon_Silver_Adult][AgeConstants.Categories.Dragon.Adult] = GetRoll(101, 200);
                testCases[CreatureConstants.Dragon_Silver_Adult][AgeConstants.Categories.Maximum] = "201";
                testCases[CreatureConstants.Dragon_Silver_MatureAdult][AgeConstants.Categories.Dragon.MatureAdult] = GetRoll(201, 400);
                testCases[CreatureConstants.Dragon_Silver_MatureAdult][AgeConstants.Categories.Maximum] = "401";
                testCases[CreatureConstants.Dragon_Silver_Old][AgeConstants.Categories.Dragon.Old] = GetRoll(401, 600);
                testCases[CreatureConstants.Dragon_Silver_Old][AgeConstants.Categories.Maximum] = "601";
                testCases[CreatureConstants.Dragon_Silver_VeryOld][AgeConstants.Categories.Dragon.VeryOld] = GetRoll(601, 800);
                testCases[CreatureConstants.Dragon_Silver_VeryOld][AgeConstants.Categories.Maximum] = "801";
                testCases[CreatureConstants.Dragon_Silver_Ancient][AgeConstants.Categories.Dragon.Ancient] = GetRoll(801, 1000);
                testCases[CreatureConstants.Dragon_Silver_Ancient][AgeConstants.Categories.Maximum] = "1001";
                testCases[CreatureConstants.Dragon_Silver_Wyrm][AgeConstants.Categories.Dragon.Wyrm] = GetRoll(1001, 1200);
                testCases[CreatureConstants.Dragon_Silver_Wyrm][AgeConstants.Categories.Maximum] = "1201";
                testCases[CreatureConstants.Dragon_Silver_GreatWyrm][AgeConstants.Categories.Dragon.GreatWyrm] = GetRoll(1201, 4200);
                testCases[CreatureConstants.Dragon_Silver_GreatWyrm][AgeConstants.Categories.Maximum] = "4201";
                testCases[CreatureConstants.Dragon_White_Wyrmling][AgeConstants.Categories.Dragon.Wyrmling] = GetRoll(0, 5);
                testCases[CreatureConstants.Dragon_White_Wyrmling][AgeConstants.Categories.Maximum] = "6";
                testCases[CreatureConstants.Dragon_White_VeryYoung][AgeConstants.Categories.Dragon.VeryYoung] = GetRoll(6, 15);
                testCases[CreatureConstants.Dragon_White_VeryYoung][AgeConstants.Categories.Maximum] = "16";
                testCases[CreatureConstants.Dragon_White_Young][AgeConstants.Categories.Dragon.Young] = GetRoll(16, 25);
                testCases[CreatureConstants.Dragon_White_Young][AgeConstants.Categories.Maximum] = "26";
                testCases[CreatureConstants.Dragon_White_Juvenile][AgeConstants.Categories.Dragon.Juvenile] = GetRoll(26, 50);
                testCases[CreatureConstants.Dragon_White_Juvenile][AgeConstants.Categories.Maximum] = "51";
                testCases[CreatureConstants.Dragon_White_YoungAdult][AgeConstants.Categories.Dragon.YoungAdult] = GetRoll(51, 100);
                testCases[CreatureConstants.Dragon_White_YoungAdult][AgeConstants.Categories.Maximum] = "101";
                testCases[CreatureConstants.Dragon_White_Adult][AgeConstants.Categories.Dragon.Adult] = GetRoll(101, 200);
                testCases[CreatureConstants.Dragon_White_Adult][AgeConstants.Categories.Maximum] = "201";
                testCases[CreatureConstants.Dragon_White_MatureAdult][AgeConstants.Categories.Dragon.MatureAdult] = GetRoll(201, 400);
                testCases[CreatureConstants.Dragon_White_MatureAdult][AgeConstants.Categories.Maximum] = "401";
                testCases[CreatureConstants.Dragon_White_Old][AgeConstants.Categories.Dragon.Old] = GetRoll(401, 600);
                testCases[CreatureConstants.Dragon_White_Old][AgeConstants.Categories.Maximum] = "601";
                testCases[CreatureConstants.Dragon_White_VeryOld][AgeConstants.Categories.Dragon.VeryOld] = GetRoll(601, 800);
                testCases[CreatureConstants.Dragon_White_VeryOld][AgeConstants.Categories.Maximum] = "801";
                testCases[CreatureConstants.Dragon_White_Ancient][AgeConstants.Categories.Dragon.Ancient] = GetRoll(801, 1000);
                testCases[CreatureConstants.Dragon_White_Ancient][AgeConstants.Categories.Maximum] = "1001";
                testCases[CreatureConstants.Dragon_White_Wyrm][AgeConstants.Categories.Dragon.Wyrm] = GetRoll(1001, 1200);
                testCases[CreatureConstants.Dragon_White_Wyrm][AgeConstants.Categories.Maximum] = "1201";
                testCases[CreatureConstants.Dragon_White_GreatWyrm][AgeConstants.Categories.Dragon.GreatWyrm] = GetRoll(1201, 2100);
                testCases[CreatureConstants.Dragon_White_GreatWyrm][AgeConstants.Categories.Maximum] = "2101";
                //Source: https://www.worldanvil.com/w/faerun-tatortotzke/a/dragon-turtle-article
                testCases[CreatureConstants.DragonTurtle][AgeConstants.Categories.Adulthood] = tenKRoll;
                testCases[CreatureConstants.DragonTurtle][AgeConstants.Categories.Maximum] = "10000+10d100";
                //Source: https://forgottenrealms.fandom.com/wiki/Dragonne
                //Nothing on aging other than 150 years been exceptionally/magically old. So, will use 150 as "up to"
                testCases[CreatureConstants.Dragonne][AgeConstants.Categories.Adulthood] = GetAdulthoodRollFromUpTo(150);
                testCases[CreatureConstants.Dragonne][AgeConstants.Categories.MiddleAge] = GetMiddleAgeRollFromUpTo(150);
                testCases[CreatureConstants.Dragonne][AgeConstants.Categories.Old] = GetOldRollFromUpTo(150);
                testCases[CreatureConstants.Dragonne][AgeConstants.Categories.Venerable] = GetVenerableRollFromUpTo(150);
                testCases[CreatureConstants.Dragonne][AgeConstants.Categories.Maximum] = GetMaximumRollFromUpTo(150);
                testCases[CreatureConstants.Dretch][AgeConstants.Categories.Adulthood] = outsiderAgeRoll;
                testCases[CreatureConstants.Dretch][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                //Source: https://www.worldanvil.com/w/faerun-tatortotzke/a/drider-species
                testCases[CreatureConstants.Drider][AgeConstants.Categories.Adulthood] = GetAdulthoodRollFromAverage(750);
                testCases[CreatureConstants.Drider][AgeConstants.Categories.MiddleAge] = GetMiddleAgeRollFromAverage(750);
                testCases[CreatureConstants.Drider][AgeConstants.Categories.Old] = GetOldRollFromAverage(750);
                testCases[CreatureConstants.Drider][AgeConstants.Categories.Venerable] = GetVenerableRollFromAverage(750);
                testCases[CreatureConstants.Drider][AgeConstants.Categories.Maximum] = GetMaximumRollFromAverage(750);
                //Soruce: https://www.worldanvil.com/w/faerun-tatortotzke/a/dryad-species
                testCases[CreatureConstants.Dryad][AgeConstants.Categories.Adulthood] = feyAgeRoll;
                testCases[CreatureConstants.Dryad][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                //Source: https://www.d20srd.org/srd/description.htm#vitalStatistics
                testCases[CreatureConstants.Dwarf_Deep][AgeConstants.Categories.Adulthood] = GetRoll(40, 124);
                testCases[CreatureConstants.Dwarf_Deep][AgeConstants.Categories.MiddleAge] = GetRoll(125, 187);
                testCases[CreatureConstants.Dwarf_Deep][AgeConstants.Categories.Old] = GetRoll(188, 249);
                testCases[CreatureConstants.Dwarf_Deep][AgeConstants.Categories.Venerable] = GetRoll(250, 450);
                testCases[CreatureConstants.Dwarf_Deep][AgeConstants.Categories.Maximum] = "250+2d100";
                testCases[CreatureConstants.Dwarf_Duergar][AgeConstants.Categories.Adulthood] = GetRoll(40, 124);
                testCases[CreatureConstants.Dwarf_Duergar][AgeConstants.Categories.MiddleAge] = GetRoll(125, 187);
                testCases[CreatureConstants.Dwarf_Duergar][AgeConstants.Categories.Old] = GetRoll(188, 249);
                testCases[CreatureConstants.Dwarf_Duergar][AgeConstants.Categories.Venerable] = GetRoll(250, 450);
                testCases[CreatureConstants.Dwarf_Duergar][AgeConstants.Categories.Maximum] = "250+2d100";
                testCases[CreatureConstants.Dwarf_Hill][AgeConstants.Categories.Adulthood] = GetRoll(40, 124);
                testCases[CreatureConstants.Dwarf_Hill][AgeConstants.Categories.MiddleAge] = GetRoll(125, 187);
                testCases[CreatureConstants.Dwarf_Hill][AgeConstants.Categories.Old] = GetRoll(188, 249);
                testCases[CreatureConstants.Dwarf_Hill][AgeConstants.Categories.Venerable] = GetRoll(250, 450);
                testCases[CreatureConstants.Dwarf_Hill][AgeConstants.Categories.Maximum] = "250+2d100";
                testCases[CreatureConstants.Dwarf_Mountain][AgeConstants.Categories.Adulthood] = GetRoll(40, 124);
                testCases[CreatureConstants.Dwarf_Mountain][AgeConstants.Categories.MiddleAge] = GetRoll(125, 187);
                testCases[CreatureConstants.Dwarf_Mountain][AgeConstants.Categories.Old] = GetRoll(188, 249);
                testCases[CreatureConstants.Dwarf_Mountain][AgeConstants.Categories.Venerable] = GetRoll(250, 450);
                testCases[CreatureConstants.Dwarf_Mountain][AgeConstants.Categories.Maximum] = "250+2d100";
                //Source: https://www.dimensions.com/element/bald-eagle-haliaeetus-leucocephalus (maximum)
                //http://www.swbemc.org/plummage.html (adulthood)
                testCases[CreatureConstants.Eagle][AgeConstants.Categories.Adulthood] = GetAdulthoodRoll(20, 5);
                testCases[CreatureConstants.Eagle][AgeConstants.Categories.MiddleAge] = GetMiddleAgeRoll(20);
                testCases[CreatureConstants.Eagle][AgeConstants.Categories.Old] = GetOldRoll(20);
                testCases[CreatureConstants.Eagle][AgeConstants.Categories.Venerable] = GetRoll(20, 30);
                testCases[CreatureConstants.Eagle][AgeConstants.Categories.Maximum] = GetMaximumRoll(20, 30);
                testCases[CreatureConstants.Eagle_Giant][AgeConstants.Categories.Adulthood] = GetAdulthoodRoll(20, 5);
                testCases[CreatureConstants.Eagle_Giant][AgeConstants.Categories.MiddleAge] = GetMiddleAgeRoll(20);
                testCases[CreatureConstants.Eagle_Giant][AgeConstants.Categories.Old] = GetOldRoll(20);
                testCases[CreatureConstants.Eagle_Giant][AgeConstants.Categories.Venerable] = GetRoll(20, 30);
                testCases[CreatureConstants.Eagle_Giant][AgeConstants.Categories.Maximum] = GetMaximumRoll(20, 30);
                testCases[CreatureConstants.Efreeti][AgeConstants.Categories.Adulthood] = outsiderAgeRoll;
                testCases[CreatureConstants.Efreeti][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                //Source: https://jurassicworld-evolution.fandom.com/wiki/Elasmosaurus
                testCases[CreatureConstants.Elasmosaurus][AgeConstants.Categories.Adulthood] = GetAdulthoodRollFromAverage(80);
                testCases[CreatureConstants.Elasmosaurus][AgeConstants.Categories.MiddleAge] = GetMiddleAgeRollFromAverage(80);
                testCases[CreatureConstants.Elasmosaurus][AgeConstants.Categories.Old] = GetOldRollFromAverage(80);
                testCases[CreatureConstants.Elasmosaurus][AgeConstants.Categories.Venerable] = GetVenerableRollFromAverage(80);
                testCases[CreatureConstants.Elasmosaurus][AgeConstants.Categories.Maximum] = GetMaximumRollFromAverage(80);
                testCases[CreatureConstants.Elemental_Air_Small][AgeConstants.Categories.Adulthood] = outsiderAgeRoll;
                testCases[CreatureConstants.Elemental_Air_Small][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Elemental_Air_Medium][AgeConstants.Categories.Adulthood] = outsiderAgeRoll;
                testCases[CreatureConstants.Elemental_Air_Medium][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Elemental_Air_Large][AgeConstants.Categories.Adulthood] = outsiderAgeRoll;
                testCases[CreatureConstants.Elemental_Air_Large][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Elemental_Air_Huge][AgeConstants.Categories.Adulthood] = outsiderAgeRoll;
                testCases[CreatureConstants.Elemental_Air_Huge][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Elemental_Air_Greater][AgeConstants.Categories.Adulthood] = outsiderAgeRoll;
                testCases[CreatureConstants.Elemental_Air_Greater][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Elemental_Air_Elder][AgeConstants.Categories.Adulthood] = outsiderAgeRoll;
                testCases[CreatureConstants.Elemental_Air_Elder][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Elemental_Earth_Small][AgeConstants.Categories.Adulthood] = outsiderAgeRoll;
                testCases[CreatureConstants.Elemental_Earth_Small][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Elemental_Earth_Medium][AgeConstants.Categories.Adulthood] = outsiderAgeRoll;
                testCases[CreatureConstants.Elemental_Earth_Medium][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Elemental_Earth_Large][AgeConstants.Categories.Adulthood] = outsiderAgeRoll;
                testCases[CreatureConstants.Elemental_Earth_Large][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Elemental_Earth_Huge][AgeConstants.Categories.Adulthood] = outsiderAgeRoll;
                testCases[CreatureConstants.Elemental_Earth_Huge][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Elemental_Earth_Greater][AgeConstants.Categories.Adulthood] = outsiderAgeRoll;
                testCases[CreatureConstants.Elemental_Earth_Greater][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Elemental_Earth_Elder][AgeConstants.Categories.Adulthood] = outsiderAgeRoll;
                testCases[CreatureConstants.Elemental_Earth_Elder][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Elemental_Fire_Small][AgeConstants.Categories.Adulthood] = outsiderAgeRoll;
                testCases[CreatureConstants.Elemental_Fire_Small][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Elemental_Fire_Medium][AgeConstants.Categories.Adulthood] = outsiderAgeRoll;
                testCases[CreatureConstants.Elemental_Fire_Medium][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Elemental_Fire_Large][AgeConstants.Categories.Adulthood] = outsiderAgeRoll;
                testCases[CreatureConstants.Elemental_Fire_Large][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Elemental_Fire_Huge][AgeConstants.Categories.Adulthood] = outsiderAgeRoll;
                testCases[CreatureConstants.Elemental_Fire_Huge][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Elemental_Fire_Greater][AgeConstants.Categories.Adulthood] = outsiderAgeRoll;
                testCases[CreatureConstants.Elemental_Fire_Greater][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Elemental_Fire_Elder][AgeConstants.Categories.Adulthood] = outsiderAgeRoll;
                testCases[CreatureConstants.Elemental_Fire_Elder][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Elemental_Water_Small][AgeConstants.Categories.Adulthood] = outsiderAgeRoll;
                testCases[CreatureConstants.Elemental_Water_Small][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Elemental_Water_Medium][AgeConstants.Categories.Adulthood] = outsiderAgeRoll;
                testCases[CreatureConstants.Elemental_Water_Medium][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Elemental_Water_Large][AgeConstants.Categories.Adulthood] = outsiderAgeRoll;
                testCases[CreatureConstants.Elemental_Water_Large][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Elemental_Water_Huge][AgeConstants.Categories.Adulthood] = outsiderAgeRoll;
                testCases[CreatureConstants.Elemental_Water_Huge][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Elemental_Water_Greater][AgeConstants.Categories.Adulthood] = outsiderAgeRoll;
                testCases[CreatureConstants.Elemental_Water_Greater][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Elemental_Water_Elder][AgeConstants.Categories.Adulthood] = outsiderAgeRoll;
                testCases[CreatureConstants.Elemental_Water_Elder][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                //Source: https://www.dimensions.com/element/african-bush-elephant-loxodonta-africana (maximum)
                //https://royalsocietypublishing.org/doi/10.1098/rstb.1953.0001 (adulthood)
                testCases[CreatureConstants.Elephant][AgeConstants.Categories.Adulthood] = GetAdulthoodRoll(60, 10);
                testCases[CreatureConstants.Elephant][AgeConstants.Categories.MiddleAge] = GetMiddleAgeRoll(60);
                testCases[CreatureConstants.Elephant][AgeConstants.Categories.Old] = GetOldRoll(60);
                testCases[CreatureConstants.Elephant][AgeConstants.Categories.Venerable] = GetRoll(60, 75);
                testCases[CreatureConstants.Elephant][AgeConstants.Categories.Maximum] = GetMaximumRoll(60, 75);
                //Source: https://www.d20srd.org/srd/description.htm#vitalStatistics
                testCases[CreatureConstants.Elf_Aquatic][AgeConstants.Categories.Adulthood] = GetRoll(110, 174);
                testCases[CreatureConstants.Elf_Aquatic][AgeConstants.Categories.MiddleAge] = GetRoll(175, 262);
                testCases[CreatureConstants.Elf_Aquatic][AgeConstants.Categories.Old] = GetRoll(263, 349);
                testCases[CreatureConstants.Elf_Aquatic][AgeConstants.Categories.Venerable] = GetRoll(350, 750);
                testCases[CreatureConstants.Elf_Aquatic][AgeConstants.Categories.Maximum] = "350+4d100";
                testCases[CreatureConstants.Elf_Drow][AgeConstants.Categories.Adulthood] = GetRoll(110, 174);
                testCases[CreatureConstants.Elf_Drow][AgeConstants.Categories.MiddleAge] = GetRoll(175, 262);
                testCases[CreatureConstants.Elf_Drow][AgeConstants.Categories.Old] = GetRoll(263, 349);
                testCases[CreatureConstants.Elf_Drow][AgeConstants.Categories.Venerable] = GetRoll(350, 750);
                testCases[CreatureConstants.Elf_Drow][AgeConstants.Categories.Maximum] = "350+4d100";
                testCases[CreatureConstants.Elf_Gray][AgeConstants.Categories.Adulthood] = GetRoll(110, 174);
                testCases[CreatureConstants.Elf_Gray][AgeConstants.Categories.MiddleAge] = GetRoll(175, 262);
                testCases[CreatureConstants.Elf_Gray][AgeConstants.Categories.Old] = GetRoll(263, 349);
                testCases[CreatureConstants.Elf_Gray][AgeConstants.Categories.Venerable] = GetRoll(350, 750);
                testCases[CreatureConstants.Elf_Gray][AgeConstants.Categories.Maximum] = "350+4d100";
                testCases[CreatureConstants.Elf_Half][AgeConstants.Categories.Adulthood] = GetRoll(20, 61);
                testCases[CreatureConstants.Elf_Half][AgeConstants.Categories.MiddleAge] = GetRoll(62, 92);
                testCases[CreatureConstants.Elf_Half][AgeConstants.Categories.Old] = GetRoll(93, 124);
                testCases[CreatureConstants.Elf_Half][AgeConstants.Categories.Venerable] = GetRoll(125, 185);
                testCases[CreatureConstants.Elf_Half][AgeConstants.Categories.Maximum] = "125+3d20";
                testCases[CreatureConstants.Elf_High][AgeConstants.Categories.Adulthood] = GetRoll(110, 174);
                testCases[CreatureConstants.Elf_High][AgeConstants.Categories.MiddleAge] = GetRoll(175, 262);
                testCases[CreatureConstants.Elf_High][AgeConstants.Categories.Old] = GetRoll(263, 349);
                testCases[CreatureConstants.Elf_High][AgeConstants.Categories.Venerable] = GetRoll(350, 750);
                testCases[CreatureConstants.Elf_High][AgeConstants.Categories.Maximum] = "350+4d100";
                testCases[CreatureConstants.Elf_Wild][AgeConstants.Categories.Adulthood] = GetRoll(110, 174);
                testCases[CreatureConstants.Elf_Wild][AgeConstants.Categories.MiddleAge] = GetRoll(175, 262);
                testCases[CreatureConstants.Elf_Wild][AgeConstants.Categories.Old] = GetRoll(263, 349);
                testCases[CreatureConstants.Elf_Wild][AgeConstants.Categories.Venerable] = GetRoll(350, 750);
                testCases[CreatureConstants.Elf_Wild][AgeConstants.Categories.Maximum] = "350+4d100";
                testCases[CreatureConstants.Elf_Wood][AgeConstants.Categories.Adulthood] = GetRoll(110, 174);
                testCases[CreatureConstants.Elf_Wood][AgeConstants.Categories.MiddleAge] = GetRoll(175, 262);
                testCases[CreatureConstants.Elf_Wood][AgeConstants.Categories.Old] = GetRoll(263, 349);
                testCases[CreatureConstants.Elf_Wood][AgeConstants.Categories.Venerable] = GetRoll(350, 750);
                testCases[CreatureConstants.Elf_Wood][AgeConstants.Categories.Maximum] = "350+4d100";
                testCases[CreatureConstants.Erinyes][AgeConstants.Categories.Adulthood] = outsiderAgeRoll;
                testCases[CreatureConstants.Erinyes][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                //INFO: Can't find anything on lifespan. So, making it up, V:50
                testCases[CreatureConstants.EtherealFilcher][AgeConstants.Categories.Adulthood] = GetAdulthoodRoll(50);
                testCases[CreatureConstants.EtherealFilcher][AgeConstants.Categories.MiddleAge] = GetMiddleAgeRoll(50);
                testCases[CreatureConstants.EtherealFilcher][AgeConstants.Categories.Old] = GetOldRoll(50);
                testCases[CreatureConstants.EtherealFilcher][AgeConstants.Categories.Venerable] = GetVenerableRoll(50);
                testCases[CreatureConstants.EtherealFilcher][AgeConstants.Categories.Maximum] = GetMaximumRoll(50);
                //INFO: Can't find anything on lifespan. So, making it up, V:50
                testCases[CreatureConstants.EtherealMarauder][AgeConstants.Categories.Adulthood] = GetAdulthoodRoll(50);
                testCases[CreatureConstants.EtherealMarauder][AgeConstants.Categories.MiddleAge] = GetMiddleAgeRoll(50);
                testCases[CreatureConstants.EtherealMarauder][AgeConstants.Categories.Old] = GetOldRoll(50);
                testCases[CreatureConstants.EtherealMarauder][AgeConstants.Categories.Venerable] = GetVenerableRoll(50);
                testCases[CreatureConstants.EtherealMarauder][AgeConstants.Categories.Maximum] = GetMaximumRoll(50);
                //INFO: https://syrikdarkenedskies.obsidianportal.com/wikis/ettercap-race
                testCases[CreatureConstants.Ettercap][AgeConstants.Categories.Adulthood] = GetRoll(6, 13);
                testCases[CreatureConstants.Ettercap][AgeConstants.Categories.MiddleAge] = GetRoll(14, 21);
                testCases[CreatureConstants.Ettercap][AgeConstants.Categories.Old] = GetRoll(22, 31);
                testCases[CreatureConstants.Ettercap][AgeConstants.Categories.Venerable] = GetRoll(32, 38);
                testCases[CreatureConstants.Ettercap][AgeConstants.Categories.Maximum] = "32+1d6";
                //Source: https://forgottenrealms.fandom.com/wiki/Ettin
                testCases[CreatureConstants.Ettin][AgeConstants.Categories.Adulthood] = GetAdulthoodRollFromAverage(75, 1);
                testCases[CreatureConstants.Ettin][AgeConstants.Categories.MiddleAge] = GetMiddleAgeRollFromAverage(75);
                testCases[CreatureConstants.Ettin][AgeConstants.Categories.Old] = GetOldRollFromAverage(75);
                testCases[CreatureConstants.Ettin][AgeConstants.Categories.Venerable] = GetVenerableRollFromAverage(75);
                testCases[CreatureConstants.Ettin][AgeConstants.Categories.Maximum] = GetMaximumRollFromAverage(75);
                //Source: https://beetleidentifications.com/fire-beetle/
                testCases[CreatureConstants.FireBeetle_Giant][AgeConstants.Categories.Adulthood] = "0";
                testCases[CreatureConstants.FireBeetle_Giant][AgeConstants.Categories.MiddleAge] = "0";
                testCases[CreatureConstants.FireBeetle_Giant][AgeConstants.Categories.Old] = "0";
                testCases[CreatureConstants.FireBeetle_Giant][AgeConstants.Categories.Venerable] = "1d2-1";
                testCases[CreatureConstants.FireBeetle_Giant][AgeConstants.Categories.Maximum] = "1d2-1";
                //Source: https://aminoapps.com/c/officialdd/page/item/formian/42Wg_oXrtvIQXY13WR6egrakpqw1GVmQZp
                //Letting Queens live 10x longer (like ants)
                testCases[CreatureConstants.FormianWorker][AgeConstants.Categories.Adulthood] = GetAdulthoodRollFromAverage(50, 3);
                testCases[CreatureConstants.FormianWorker][AgeConstants.Categories.MiddleAge] = GetMiddleAgeRollFromAverage(50);
                testCases[CreatureConstants.FormianWorker][AgeConstants.Categories.Old] = GetOldRollFromAverage(50);
                testCases[CreatureConstants.FormianWorker][AgeConstants.Categories.Venerable] = GetVenerableRollFromAverage(50);
                testCases[CreatureConstants.FormianWorker][AgeConstants.Categories.Maximum] = GetMaximumRollFromAverage(50);
                testCases[CreatureConstants.FormianWarrior][AgeConstants.Categories.Adulthood] = GetAdulthoodRollFromAverage(50, 3);
                testCases[CreatureConstants.FormianWarrior][AgeConstants.Categories.MiddleAge] = GetMiddleAgeRollFromAverage(50);
                testCases[CreatureConstants.FormianWarrior][AgeConstants.Categories.Old] = GetOldRollFromAverage(50);
                testCases[CreatureConstants.FormianWarrior][AgeConstants.Categories.Venerable] = GetVenerableRollFromAverage(50);
                testCases[CreatureConstants.FormianWarrior][AgeConstants.Categories.Maximum] = GetMaximumRollFromAverage(50);
                testCases[CreatureConstants.FormianTaskmaster][AgeConstants.Categories.Adulthood] = GetAdulthoodRollFromAverage(50, 3);
                testCases[CreatureConstants.FormianTaskmaster][AgeConstants.Categories.MiddleAge] = GetMiddleAgeRollFromAverage(50);
                testCases[CreatureConstants.FormianTaskmaster][AgeConstants.Categories.Old] = GetOldRollFromAverage(50);
                testCases[CreatureConstants.FormianTaskmaster][AgeConstants.Categories.Venerable] = GetVenerableRollFromAverage(50);
                testCases[CreatureConstants.FormianTaskmaster][AgeConstants.Categories.Maximum] = GetMaximumRollFromAverage(50);
                testCases[CreatureConstants.FormianMyrmarch][AgeConstants.Categories.Adulthood] = GetAdulthoodRollFromAverage(50, 3);
                testCases[CreatureConstants.FormianMyrmarch][AgeConstants.Categories.MiddleAge] = GetMiddleAgeRollFromAverage(50);
                testCases[CreatureConstants.FormianMyrmarch][AgeConstants.Categories.Old] = GetOldRollFromAverage(50);
                testCases[CreatureConstants.FormianMyrmarch][AgeConstants.Categories.Venerable] = GetVenerableRollFromAverage(50);
                testCases[CreatureConstants.FormianMyrmarch][AgeConstants.Categories.Maximum] = GetMaximumRollFromAverage(50);
                testCases[CreatureConstants.FormianQueen][AgeConstants.Categories.Adulthood] = GetAdulthoodRollFromAverage(500, 3);
                testCases[CreatureConstants.FormianQueen][AgeConstants.Categories.MiddleAge] = GetMiddleAgeRollFromAverage(500);
                testCases[CreatureConstants.FormianQueen][AgeConstants.Categories.Old] = GetOldRollFromAverage(500);
                testCases[CreatureConstants.FormianQueen][AgeConstants.Categories.Venerable] = GetVenerableRollFromAverage(500);
                testCases[CreatureConstants.FormianQueen][AgeConstants.Categories.Maximum] = GetMaximumRollFromAverage(500);
                //Source: https://forgottenrealms.fandom.com/wiki/Frost_worm#Ecology
                //Only know when they reach maturity (3-5 years), nothing else, so making it up, V:5*5
                testCases[CreatureConstants.FrostWorm][AgeConstants.Categories.Adulthood] = GetAdulthoodRoll(25, 3);
                testCases[CreatureConstants.FrostWorm][AgeConstants.Categories.MiddleAge] = GetMiddleAgeRoll(25);
                testCases[CreatureConstants.FrostWorm][AgeConstants.Categories.Old] = GetOldRoll(25);
                testCases[CreatureConstants.FrostWorm][AgeConstants.Categories.Venerable] = GetVenerableRoll(25);
                testCases[CreatureConstants.FrostWorm][AgeConstants.Categories.Maximum] = GetMaximumRoll(25);
                //Source: https://forgottenrealms.fandom.com/wiki/Gargoyle
                testCases[CreatureConstants.Gargoyle][AgeConstants.Categories.Adulthood] = GetAdulthoodRollFromAverage(50);
                testCases[CreatureConstants.Gargoyle][AgeConstants.Categories.MiddleAge] = GetMiddleAgeRollFromAverage(50);
                testCases[CreatureConstants.Gargoyle][AgeConstants.Categories.Old] = GetOldRollFromAverage(50);
                testCases[CreatureConstants.Gargoyle][AgeConstants.Categories.Venerable] = GetVenerableRollFromAverage(50);
                testCases[CreatureConstants.Gargoyle][AgeConstants.Categories.Maximum] = GetMaximumRollFromAverage(50);
                testCases[CreatureConstants.Gargoyle_Kapoacinth][AgeConstants.Categories.Adulthood] = GetAdulthoodRollFromAverage(50);
                testCases[CreatureConstants.Gargoyle_Kapoacinth][AgeConstants.Categories.MiddleAge] = GetMiddleAgeRollFromAverage(50);
                testCases[CreatureConstants.Gargoyle_Kapoacinth][AgeConstants.Categories.Old] = GetOldRollFromAverage(50);
                testCases[CreatureConstants.Gargoyle_Kapoacinth][AgeConstants.Categories.Venerable] = GetVenerableRollFromAverage(50);
                testCases[CreatureConstants.Gargoyle_Kapoacinth][AgeConstants.Categories.Maximum] = GetMaximumRollFromAverage(50);
                //Source: https://www.worldanvil.com/w/faerun-tatortotzke/a/gelatinous-cube-species
                testCases[CreatureConstants.GelatinousCube][AgeConstants.Categories.Adulthood] = oneKRoll;
                testCases[CreatureConstants.GelatinousCube][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                //Source: https://forgottenrealms.fandom.com/wiki/Ghaele
                testCases[CreatureConstants.Ghaele][AgeConstants.Categories.Adulthood] = GetAdulthoodRoll(400);
                testCases[CreatureConstants.Ghaele][AgeConstants.Categories.MiddleAge] = GetMiddleAgeRoll(400);
                testCases[CreatureConstants.Ghaele][AgeConstants.Categories.Old] = GetOldRoll(400);
                testCases[CreatureConstants.Ghaele][AgeConstants.Categories.Venerable] = GetRoll(400, 800);
                testCases[CreatureConstants.Ghaele][AgeConstants.Categories.Maximum] = "400+4d100";
                testCases[CreatureConstants.Ghoul][AgeConstants.Categories.Undead] = undeadAgeRoll;
                testCases[CreatureConstants.Ghoul][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Ghoul_Ghast][AgeConstants.Categories.Undead] = undeadAgeRoll;
                testCases[CreatureConstants.Ghoul_Ghast][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Ghoul_Lacedon][AgeConstants.Categories.Undead] = undeadAgeRoll;
                testCases[CreatureConstants.Ghoul_Lacedon][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                //Source: https://forgottenrealms.fandom.com/wiki/Cloud_giant
                testCases[CreatureConstants.Giant_Cloud][AgeConstants.Categories.Adulthood] = GetAdulthoodRollFromUpTo(400);
                testCases[CreatureConstants.Giant_Cloud][AgeConstants.Categories.MiddleAge] = GetMiddleAgeRollFromUpTo(400);
                testCases[CreatureConstants.Giant_Cloud][AgeConstants.Categories.Old] = GetOldRollFromUpTo(400);
                testCases[CreatureConstants.Giant_Cloud][AgeConstants.Categories.Venerable] = GetVenerableRollFromUpTo(400);
                testCases[CreatureConstants.Giant_Cloud][AgeConstants.Categories.Maximum] = GetMaximumRollFromUpTo(400);
                //Source: https://forgottenrealms.fandom.com/wiki/Fire_giant
                testCases[CreatureConstants.Giant_Fire][AgeConstants.Categories.Adulthood] = GetAdulthoodRollFromAverage(350);
                testCases[CreatureConstants.Giant_Fire][AgeConstants.Categories.MiddleAge] = GetMiddleAgeRollFromAverage(350);
                testCases[CreatureConstants.Giant_Fire][AgeConstants.Categories.Old] = GetOldRollFromAverage(350);
                testCases[CreatureConstants.Giant_Fire][AgeConstants.Categories.Venerable] = GetVenerableRollFromAverage(350);
                testCases[CreatureConstants.Giant_Fire][AgeConstants.Categories.Maximum] = GetMaximumRollFromAverage(350);
                //Source: https://forgottenrealms.fandom.com/wiki/Frost_giant
                testCases[CreatureConstants.Giant_Frost][AgeConstants.Categories.Adulthood] = GetAdulthoodRollFromAverage(250);
                testCases[CreatureConstants.Giant_Frost][AgeConstants.Categories.MiddleAge] = GetMiddleAgeRollFromAverage(250);
                testCases[CreatureConstants.Giant_Frost][AgeConstants.Categories.Old] = GetOldRollFromAverage(250);
                testCases[CreatureConstants.Giant_Frost][AgeConstants.Categories.Venerable] = GetVenerableRollFromAverage(250);
                testCases[CreatureConstants.Giant_Frost][AgeConstants.Categories.Maximum] = GetMaximumRollFromAverage(250);
                //Source: https://forgottenrealms.fandom.com/wiki/Hill_giant
                testCases[CreatureConstants.Giant_Hill][AgeConstants.Categories.Adulthood] = GetAdulthoodRollFromAverage(200);
                testCases[CreatureConstants.Giant_Hill][AgeConstants.Categories.MiddleAge] = GetMiddleAgeRollFromAverage(200);
                testCases[CreatureConstants.Giant_Hill][AgeConstants.Categories.Old] = GetOldRollFromAverage(200);
                testCases[CreatureConstants.Giant_Hill][AgeConstants.Categories.Venerable] = GetVenerableRollFromAverage(200);
                testCases[CreatureConstants.Giant_Hill][AgeConstants.Categories.Maximum] = GetMaximumRollFromAverage(200);
                //Source: https://forgottenrealms.fandom.com/wiki/Stone_giant
                testCases[CreatureConstants.Giant_Stone][AgeConstants.Categories.Adulthood] = GetAdulthoodRollFromUpTo(800);
                testCases[CreatureConstants.Giant_Stone][AgeConstants.Categories.MiddleAge] = GetMiddleAgeRollFromUpTo(800);
                testCases[CreatureConstants.Giant_Stone][AgeConstants.Categories.Old] = GetOldRollFromUpTo(800);
                testCases[CreatureConstants.Giant_Stone][AgeConstants.Categories.Venerable] = GetVenerableRollFromUpTo(800);
                testCases[CreatureConstants.Giant_Stone][AgeConstants.Categories.Maximum] = GetMaximumRollFromUpTo(800);
                testCases[CreatureConstants.Giant_Stone_Elder][AgeConstants.Categories.Adulthood] = GetAdulthoodRollFromUpTo(800);
                testCases[CreatureConstants.Giant_Stone_Elder][AgeConstants.Categories.MiddleAge] = GetMiddleAgeRollFromUpTo(800);
                testCases[CreatureConstants.Giant_Stone_Elder][AgeConstants.Categories.Old] = GetOldRollFromUpTo(800);
                testCases[CreatureConstants.Giant_Stone_Elder][AgeConstants.Categories.Venerable] = GetVenerableRollFromUpTo(800);
                testCases[CreatureConstants.Giant_Stone_Elder][AgeConstants.Categories.Maximum] = GetMaximumRollFromUpTo(800);
                //Source: https://forgottenrealms.fandom.com/wiki/Storm_giant
                testCases[CreatureConstants.Giant_Storm][AgeConstants.Categories.Adulthood] = GetAdulthoodRollFromAverage(600);
                testCases[CreatureConstants.Giant_Storm][AgeConstants.Categories.MiddleAge] = GetMiddleAgeRollFromAverage(600);
                testCases[CreatureConstants.Giant_Storm][AgeConstants.Categories.Old] = GetOldRollFromAverage(600);
                testCases[CreatureConstants.Giant_Storm][AgeConstants.Categories.Venerable] = GetVenerableRollFromAverage(600);
                testCases[CreatureConstants.Giant_Storm][AgeConstants.Categories.Maximum] = GetMaximumRollFromAverage(600);
                //Source: https://www.worldanvil.com/w/faerun-tatortotzke/a/gibbering-mouther-species
                testCases[CreatureConstants.GibberingMouther][AgeConstants.Categories.Adulthood] = oneKRoll;
                testCases[CreatureConstants.GibberingMouther][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                //Source: https://www.reddit.com/r/DnDBehindTheScreen/comments/grdahi/girallon/
                testCases[CreatureConstants.Girallon][AgeConstants.Categories.Adulthood] = GetAdulthoodRollFromAverage(35, 12);
                testCases[CreatureConstants.Girallon][AgeConstants.Categories.MiddleAge] = GetMiddleAgeRollFromAverage(35);
                testCases[CreatureConstants.Girallon][AgeConstants.Categories.Old] = GetOldRollFromAverage(35);
                testCases[CreatureConstants.Girallon][AgeConstants.Categories.Venerable] = GetVenerableRollFromAverage(35);
                testCases[CreatureConstants.Girallon][AgeConstants.Categories.Maximum] = GetMaximumRollFromAverage(35);
                //Source: http://people.wku.edu/charles.plemons/ad&d/races/age.html
                testCases[CreatureConstants.Githyanki][AgeConstants.Categories.Adulthood] = GetRoll(30, 124);
                testCases[CreatureConstants.Githyanki][AgeConstants.Categories.MiddleAge] = GetRoll(125, 166);
                testCases[CreatureConstants.Githyanki][AgeConstants.Categories.Old] = GetRoll(167, 249);
                testCases[CreatureConstants.Githyanki][AgeConstants.Categories.Venerable] = GetRoll(250, 350);
                testCases[CreatureConstants.Githyanki][AgeConstants.Categories.Maximum] = "250+1d100";
                //Source: http://people.wku.edu/charles.plemons/ad&d/races/age.html
                testCases[CreatureConstants.Githzerai][AgeConstants.Categories.Adulthood] = GetRoll(30, 124);
                testCases[CreatureConstants.Githzerai][AgeConstants.Categories.MiddleAge] = GetRoll(125, 166);
                testCases[CreatureConstants.Githzerai][AgeConstants.Categories.Old] = GetRoll(167, 249);
                testCases[CreatureConstants.Githzerai][AgeConstants.Categories.Venerable] = GetRoll(250, 350);
                testCases[CreatureConstants.Githzerai][AgeConstants.Categories.Maximum] = "250+1d100";
                testCases[CreatureConstants.Glabrezu][AgeConstants.Categories.Adulthood] = outsiderAgeRoll;
                testCases[CreatureConstants.Glabrezu][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                //Source: http://people.wku.edu/charles.plemons/ad&d/races/age.html
                testCases[CreatureConstants.Gnoll][AgeConstants.Categories.Adulthood] = GetRoll(7, 15);
                testCases[CreatureConstants.Gnoll][AgeConstants.Categories.MiddleAge] = GetRoll(16, 21);
                testCases[CreatureConstants.Gnoll][AgeConstants.Categories.Old] = GetRoll(22, 32);
                testCases[CreatureConstants.Gnoll][AgeConstants.Categories.Venerable] = GetRoll(33, 37);
                testCases[CreatureConstants.Gnoll][AgeConstants.Categories.Maximum] = "33+1d4";
                //Source: https://www.d20srd.org/srd/description.htm#vitalStatistics
                testCases[CreatureConstants.Gnome_Forest][AgeConstants.Categories.Adulthood] = GetRoll(40, 99);
                testCases[CreatureConstants.Gnome_Forest][AgeConstants.Categories.MiddleAge] = GetRoll(100, 149);
                testCases[CreatureConstants.Gnome_Forest][AgeConstants.Categories.Old] = GetRoll(150, 199);
                testCases[CreatureConstants.Gnome_Forest][AgeConstants.Categories.Venerable] = GetRoll(200, 500);
                testCases[CreatureConstants.Gnome_Forest][AgeConstants.Categories.Maximum] = "200+3d100";
                testCases[CreatureConstants.Gnome_Rock][AgeConstants.Categories.Adulthood] = GetRoll(40, 99);
                testCases[CreatureConstants.Gnome_Rock][AgeConstants.Categories.MiddleAge] = GetRoll(100, 149);
                testCases[CreatureConstants.Gnome_Rock][AgeConstants.Categories.Old] = GetRoll(150, 199);
                testCases[CreatureConstants.Gnome_Rock][AgeConstants.Categories.Venerable] = GetRoll(200, 500);
                testCases[CreatureConstants.Gnome_Rock][AgeConstants.Categories.Maximum] = "200+3d100";
                //Source: above, plus https://www.d20srd.org/srd/monsters/gnome.htm
                testCases[CreatureConstants.Gnome_Svirfneblin][AgeConstants.Categories.Adulthood] = GetRoll(40, 99);
                testCases[CreatureConstants.Gnome_Svirfneblin][AgeConstants.Categories.MiddleAge] = GetRoll(100, 149);
                testCases[CreatureConstants.Gnome_Svirfneblin][AgeConstants.Categories.Old] = GetRoll(150, 199);
                testCases[CreatureConstants.Gnome_Svirfneblin][AgeConstants.Categories.Venerable] = GetRoll(200, 300);
                testCases[CreatureConstants.Gnome_Svirfneblin][AgeConstants.Categories.Maximum] = "200+1d100";
                //Source: http://people.wku.edu/charles.plemons/ad&d/races/age.html
                testCases[CreatureConstants.Goblin][AgeConstants.Categories.Adulthood] = GetRoll(12, 19);
                testCases[CreatureConstants.Goblin][AgeConstants.Categories.MiddleAge] = GetRoll(20, 29);
                testCases[CreatureConstants.Goblin][AgeConstants.Categories.Old] = GetRoll(30, 39);
                testCases[CreatureConstants.Goblin][AgeConstants.Categories.Venerable] = GetRoll(40, 60);
                testCases[CreatureConstants.Goblin][AgeConstants.Categories.Maximum] = "40+1d20";
                testCases[CreatureConstants.Golem_Clay][AgeConstants.Categories.Construct] = constructAgeRoll;
                testCases[CreatureConstants.Golem_Clay][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Golem_Flesh][AgeConstants.Categories.Construct] = constructAgeRoll;
                testCases[CreatureConstants.Golem_Flesh][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Golem_Iron][AgeConstants.Categories.Construct] = constructAgeRoll;
                testCases[CreatureConstants.Golem_Iron][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Golem_Stone][AgeConstants.Categories.Construct] = constructAgeRoll;
                testCases[CreatureConstants.Golem_Stone][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Golem_Stone_Greater][AgeConstants.Categories.Construct] = constructAgeRoll;
                testCases[CreatureConstants.Golem_Stone_Greater][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                //Source: https://www.worldanvil.com/w/hijr-sudaj/a/gorgon-species
                testCases[CreatureConstants.Gorgon][AgeConstants.Categories.Adulthood] = GetAdulthoodRoll(60, 5);
                testCases[CreatureConstants.Gorgon][AgeConstants.Categories.MiddleAge] = GetMiddleAgeRoll(60, 50);
                testCases[CreatureConstants.Gorgon][AgeConstants.Categories.Old] = GetRoll(50, 59);
                testCases[CreatureConstants.Gorgon][AgeConstants.Categories.Venerable] = GetRoll(60, 75);
                testCases[CreatureConstants.Gorgon][AgeConstants.Categories.Maximum] = GetMaximumRoll(60, 75);
                testCases[CreatureConstants.GrayOoze][AgeConstants.Categories.Adulthood] = GetRoll(1, 100);
                testCases[CreatureConstants.GrayOoze][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                //Source: https://thecampaign20xx.blogspot.com/2020/03/dungeons-dragons-guide-to-gray-render.html "several hundred" as 300
                testCases[CreatureConstants.GrayRender][AgeConstants.Categories.Adulthood] = GetAdulthoodRollFromAverage(300, 1);
                testCases[CreatureConstants.GrayRender][AgeConstants.Categories.MiddleAge] = GetMiddleAgeRollFromAverage(300);
                testCases[CreatureConstants.GrayRender][AgeConstants.Categories.Old] = GetOldRollFromAverage(300);
                testCases[CreatureConstants.GrayRender][AgeConstants.Categories.Venerable] = GetVenerableRollFromAverage(300);
                testCases[CreatureConstants.GrayRender][AgeConstants.Categories.Maximum] = GetMaximumRollFromAverage(300);
                testCases[CreatureConstants.GreenHag][AgeConstants.Categories.Adulthood] = GetRoll(50, 500);
                testCases[CreatureConstants.GreenHag][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                //Source: https://www.worldanvil.com/w/faerun-tatortotzke/a/grick-species
                testCases[CreatureConstants.Grick][AgeConstants.Categories.Adulthood] = GetAdulthoodRollFromAverage(10);
                testCases[CreatureConstants.Grick][AgeConstants.Categories.MiddleAge] = GetMiddleAgeRollFromAverage(10);
                testCases[CreatureConstants.Grick][AgeConstants.Categories.Old] = GetOldRollFromAverage(10);
                testCases[CreatureConstants.Grick][AgeConstants.Categories.Venerable] = GetVenerableRollFromAverage(10);
                testCases[CreatureConstants.Grick][AgeConstants.Categories.Maximum] = GetMaximumRollFromAverage(10);
                //Source: https://www.reddit.com/r/DnDBehindTheScreen/comments/ebfc1h/griffon/
                testCases[CreatureConstants.Griffon][AgeConstants.Categories.Adulthood] = GetAdulthoodRoll(25, 0);
                testCases[CreatureConstants.Griffon][AgeConstants.Categories.MiddleAge] = GetMiddleAgeRoll(25);
                testCases[CreatureConstants.Griffon][AgeConstants.Categories.Old] = GetOldRoll(25);
                testCases[CreatureConstants.Griffon][AgeConstants.Categories.Venerable] = GetRoll(25, 70);
                testCases[CreatureConstants.Griffon][AgeConstants.Categories.Maximum] = GetMaximumRoll(25, 70);
                //Source: http://people.wku.edu/charles.plemons/ad&d/races/age.html - from Pixie
                testCases[CreatureConstants.Grig][AgeConstants.Categories.Adulthood] = "100";
                testCases[CreatureConstants.Grig][AgeConstants.Categories.MiddleAge] = GetRoll(100, 132);
                testCases[CreatureConstants.Grig][AgeConstants.Categories.Old] = GetRoll(133, 199);
                testCases[CreatureConstants.Grig][AgeConstants.Categories.Venerable] = GetRoll(200, 400);
                testCases[CreatureConstants.Grig][AgeConstants.Categories.Maximum] = "200+2d100";
                testCases[CreatureConstants.Grig_WithFiddle][AgeConstants.Categories.Adulthood] = "100";
                testCases[CreatureConstants.Grig_WithFiddle][AgeConstants.Categories.MiddleAge] = GetRoll(100, 132);
                testCases[CreatureConstants.Grig_WithFiddle][AgeConstants.Categories.Old] = GetRoll(133, 199);
                testCases[CreatureConstants.Grig_WithFiddle][AgeConstants.Categories.Venerable] = GetRoll(200, 400);
                testCases[CreatureConstants.Grig_WithFiddle][AgeConstants.Categories.Maximum] = "200+2d100";
                //Source: https://www.worldanvil.com/w/faerun-tatortotzke/a/grimlock-species
                testCases[CreatureConstants.Grimlock][AgeConstants.Categories.Adulthood] = GetAdulthoodRollFromAverage(60);
                testCases[CreatureConstants.Grimlock][AgeConstants.Categories.MiddleAge] = GetMiddleAgeRollFromAverage(60);
                testCases[CreatureConstants.Grimlock][AgeConstants.Categories.Old] = GetOldRollFromAverage(60);
                testCases[CreatureConstants.Grimlock][AgeConstants.Categories.Venerable] = GetVenerableRollFromAverage(60);
                testCases[CreatureConstants.Grimlock][AgeConstants.Categories.Maximum] = GetMaximumRollFromAverage(60);
                //Source: https://pathfinder.d20srd.org/bestiary3/sphinx.html (adulthood)
                //https://www.belloflostsouls.net/2021/12/dd-monster-spotlight-androsphinx.html (maximum)
                testCases[CreatureConstants.Gynosphinx][AgeConstants.Categories.Adulthood] = $"{tenKRoll}+3";
                testCases[CreatureConstants.Gynosphinx][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                //Source: https://www.d20srd.org/srd/description.htm#vitalStatistics
                testCases[CreatureConstants.Halfling_Deep][AgeConstants.Categories.Adulthood] = GetRoll(20, 49);
                testCases[CreatureConstants.Halfling_Deep][AgeConstants.Categories.MiddleAge] = GetRoll(50, 74);
                testCases[CreatureConstants.Halfling_Deep][AgeConstants.Categories.Old] = GetRoll(75, 99);
                testCases[CreatureConstants.Halfling_Deep][AgeConstants.Categories.Venerable] = GetRoll(100, 200);
                testCases[CreatureConstants.Halfling_Deep][AgeConstants.Categories.Maximum] = "100+5d20";
                testCases[CreatureConstants.Halfling_Lightfoot][AgeConstants.Categories.Adulthood] = GetRoll(20, 49);
                testCases[CreatureConstants.Halfling_Lightfoot][AgeConstants.Categories.MiddleAge] = GetRoll(50, 74);
                testCases[CreatureConstants.Halfling_Lightfoot][AgeConstants.Categories.Old] = GetRoll(75, 99);
                testCases[CreatureConstants.Halfling_Lightfoot][AgeConstants.Categories.Venerable] = GetRoll(100, 200);
                testCases[CreatureConstants.Halfling_Lightfoot][AgeConstants.Categories.Maximum] = "100+5d20";
                testCases[CreatureConstants.Halfling_Tallfellow][AgeConstants.Categories.Adulthood] = GetRoll(20, 49);
                testCases[CreatureConstants.Halfling_Tallfellow][AgeConstants.Categories.MiddleAge] = GetRoll(50, 74);
                testCases[CreatureConstants.Halfling_Tallfellow][AgeConstants.Categories.Old] = GetRoll(75, 99);
                testCases[CreatureConstants.Halfling_Tallfellow][AgeConstants.Categories.Venerable] = GetRoll(100, 200);
                testCases[CreatureConstants.Halfling_Tallfellow][AgeConstants.Categories.Maximum] = "100+5d20";
                //Source: https://forgottenrealms.fandom.com/wiki/Harpy
                testCases[CreatureConstants.Harpy][AgeConstants.Categories.Adulthood] = GetAdulthoodRoll(25, 2);
                testCases[CreatureConstants.Harpy][AgeConstants.Categories.MiddleAge] = GetMiddleAgeRoll(25);
                testCases[CreatureConstants.Harpy][AgeConstants.Categories.Old] = GetOldRoll(25);
                testCases[CreatureConstants.Harpy][AgeConstants.Categories.Venerable] = GetRoll(25, 50);
                testCases[CreatureConstants.Harpy][AgeConstants.Categories.Maximum] = GetMaximumRoll(25, 50);
                //Source: https://www.dimensions.com/element/osprey-pandion-haliaetus (maximum)
                //https://www.rspb.org.uk/birds-and-wildlife/wildlife-guides/bird-a-z/osprey/nesting-and-breeding-habits/ (adulthood)
                testCases[CreatureConstants.Hawk][AgeConstants.Categories.Adulthood] = GetAdulthoodRoll(25, 3);
                testCases[CreatureConstants.Hawk][AgeConstants.Categories.MiddleAge] = GetMiddleAgeRoll(25);
                testCases[CreatureConstants.Hawk][AgeConstants.Categories.Old] = GetOldRoll(25);
                testCases[CreatureConstants.Hawk][AgeConstants.Categories.Venerable] = GetRoll(25, 32);
                testCases[CreatureConstants.Hawk][AgeConstants.Categories.Maximum] = GetMaximumRoll(25, 32);
                testCases[CreatureConstants.HellHound][AgeConstants.Categories.Adulthood] = $"{outsiderAgeRoll}+2";
                testCases[CreatureConstants.HellHound][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.HellHound_NessianWarhound][AgeConstants.Categories.Adulthood] = $"{outsiderAgeRoll}+2";
                testCases[CreatureConstants.HellHound_NessianWarhound][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Hellcat_Bezekira][AgeConstants.Categories.Adulthood] = outsiderAgeRoll;
                testCases[CreatureConstants.Hellcat_Bezekira][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Hellwasp_Swarm][AgeConstants.Categories.Swarm] = outsiderAgeRoll;
                testCases[CreatureConstants.Hellwasp_Swarm][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Hezrou][AgeConstants.Categories.Adulthood] = outsiderAgeRoll;
                testCases[CreatureConstants.Hezrou][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                //Source: https://pathfinder.d20srd.org/bestiary3/sphinx.html (adulthood)
                //https://www.belloflostsouls.net/2021/12/dd-monster-spotlight-androsphinx.html (maximum)
                testCases[CreatureConstants.Hieracosphinx][AgeConstants.Categories.Adulthood] = $"{tenKRoll}+3";
                testCases[CreatureConstants.Hieracosphinx][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                //Source: https://www.worldanvil.com/w/faerun-tatortotzke/a/hippogriff-species (maximum)
                //https://forgottenrealms.fandom.com/wiki/Hippogriff#Ecology (adulthood)
                testCases[CreatureConstants.Hippogriff][AgeConstants.Categories.Adulthood] = GetAdulthoodRoll(25, 1);
                testCases[CreatureConstants.Hippogriff][AgeConstants.Categories.MiddleAge] = GetMiddleAgeRoll(25);
                testCases[CreatureConstants.Hippogriff][AgeConstants.Categories.Old] = GetOldRoll(25);
                testCases[CreatureConstants.Hippogriff][AgeConstants.Categories.Venerable] = GetRoll(25, 30);
                testCases[CreatureConstants.Hippogriff][AgeConstants.Categories.Maximum] = GetMaximumRoll(25, 30);
                //Source: http://people.wku.edu/charles.plemons/ad&d/races/age.html
                testCases[CreatureConstants.Hobgoblin][AgeConstants.Categories.Adulthood] = GetRoll(14, 24);
                testCases[CreatureConstants.Hobgoblin][AgeConstants.Categories.MiddleAge] = GetRoll(25, 32);
                testCases[CreatureConstants.Hobgoblin][AgeConstants.Categories.Old] = GetRoll(33, 49);
                testCases[CreatureConstants.Hobgoblin][AgeConstants.Categories.Venerable] = GetRoll(50, 70);
                testCases[CreatureConstants.Hobgoblin][AgeConstants.Categories.Maximum] = "50+1d20";
                testCases[CreatureConstants.Homunculus][AgeConstants.Categories.Construct] = "1d100";
                testCases[CreatureConstants.Homunculus][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.HornedDevil_Cornugon][AgeConstants.Categories.Adulthood] = outsiderAgeRoll;
                testCases[CreatureConstants.HornedDevil_Cornugon][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                //Source: https://www.dimensions.com/element/clydesdale-horse (maximum)
                //https://seaworld.org/animals/all-about/clydesdale/reproduction/ (adulthood)
                testCases[CreatureConstants.Horse_Heavy][AgeConstants.Categories.Adulthood] = GetAdulthoodRoll(20, 3);
                testCases[CreatureConstants.Horse_Heavy][AgeConstants.Categories.MiddleAge] = GetMiddleAgeRoll(20);
                testCases[CreatureConstants.Horse_Heavy][AgeConstants.Categories.Old] = GetOldRoll(20);
                testCases[CreatureConstants.Horse_Heavy][AgeConstants.Categories.Venerable] = GetRoll(20, 25);
                testCases[CreatureConstants.Horse_Heavy][AgeConstants.Categories.Maximum] = GetMaximumRoll(20, 25);
                //Source: https://www.dimensions.com/element/arabian-horse (maximum)
                //https://horserookie.com/arabian-horse-lifespan-and-facts/ (adulthood)
                testCases[CreatureConstants.Horse_Light][AgeConstants.Categories.Adulthood] = GetAdulthoodRoll(25, 4);
                testCases[CreatureConstants.Horse_Light][AgeConstants.Categories.MiddleAge] = GetMiddleAgeRoll(25);
                testCases[CreatureConstants.Horse_Light][AgeConstants.Categories.Old] = GetOldRoll(25);
                testCases[CreatureConstants.Horse_Light][AgeConstants.Categories.Venerable] = GetRoll(25, 30);
                testCases[CreatureConstants.Horse_Light][AgeConstants.Categories.Maximum] = GetMaximumRoll(25, 30);
                //Source: https://www.dimensions.com/element/clydesdale-horse (maximum)
                //https://seaworld.org/animals/all-about/clydesdale/reproduction/ (adulthood)
                testCases[CreatureConstants.Horse_Heavy_War][AgeConstants.Categories.Adulthood] = GetAdulthoodRoll(20, 3);
                testCases[CreatureConstants.Horse_Heavy_War][AgeConstants.Categories.MiddleAge] = GetMiddleAgeRoll(20);
                testCases[CreatureConstants.Horse_Heavy_War][AgeConstants.Categories.Old] = GetOldRoll(20);
                testCases[CreatureConstants.Horse_Heavy_War][AgeConstants.Categories.Venerable] = GetRoll(20, 25);
                testCases[CreatureConstants.Horse_Heavy_War][AgeConstants.Categories.Maximum] = GetMaximumRoll(20, 25);
                //Source: https://www.dimensions.com/element/arabian-horse (maximum)
                //https://horserookie.com/arabian-horse-lifespan-and-facts/ (adulthood)
                testCases[CreatureConstants.Horse_Light_War][AgeConstants.Categories.Adulthood] = GetAdulthoodRoll(25, 4);
                testCases[CreatureConstants.Horse_Light_War][AgeConstants.Categories.MiddleAge] = GetMiddleAgeRoll(25);
                testCases[CreatureConstants.Horse_Light_War][AgeConstants.Categories.Old] = GetOldRoll(25);
                testCases[CreatureConstants.Horse_Light_War][AgeConstants.Categories.Venerable] = GetRoll(25, 30);
                testCases[CreatureConstants.Horse_Light_War][AgeConstants.Categories.Maximum] = GetMaximumRoll(25, 30);
                testCases[CreatureConstants.HoundArchon][AgeConstants.Categories.Adulthood] = outsiderAgeRoll;
                testCases[CreatureConstants.HoundArchon][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                //INFO: Can't find anything. So, making it up, V:50
                testCases[CreatureConstants.Howler][AgeConstants.Categories.Adulthood] = GetAdulthoodRoll(50);
                testCases[CreatureConstants.Howler][AgeConstants.Categories.MiddleAge] = GetMiddleAgeRoll(50);
                testCases[CreatureConstants.Howler][AgeConstants.Categories.Old] = GetOldRoll(50);
                testCases[CreatureConstants.Howler][AgeConstants.Categories.Venerable] = GetVenerableRoll(50);
                testCases[CreatureConstants.Howler][AgeConstants.Categories.Maximum] = GetMaximumRoll(50);
                //Source https://www.d20srd.org/srd/description.htm#vitalStatistics
                testCases[CreatureConstants.Human][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(15, 34, true);
                testCases[CreatureConstants.Human][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(35, 52, true);
                testCases[CreatureConstants.Human][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(53, 69, true);
                testCases[CreatureConstants.Human][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(70, 110, true);
                testCases[CreatureConstants.Human][AgeConstants.Categories.Maximum] = "70+2d20";
                //Source: https://www.reddit.com/r/DnDBehindTheScreen/comments/63zx1m/hydra/
                testCases[CreatureConstants.Hydra_5Heads][AgeConstants.Categories.Adulthood] = GetRoll(25, 25 * 20);
                testCases[CreatureConstants.Hydra_5Heads][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Hydra_6Heads][AgeConstants.Categories.Adulthood] = GetRoll(501, 1000);
                testCases[CreatureConstants.Hydra_6Heads][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Hydra_7Heads][AgeConstants.Categories.Adulthood] = GetRoll(1001, 2000);
                testCases[CreatureConstants.Hydra_7Heads][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Hydra_8Heads][AgeConstants.Categories.Adulthood] = GetRoll(2001, 4000);
                testCases[CreatureConstants.Hydra_8Heads][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Hydra_9Heads][AgeConstants.Categories.Adulthood] = GetRoll(4001, 6000);
                testCases[CreatureConstants.Hydra_9Heads][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Hydra_10Heads][AgeConstants.Categories.Adulthood] = GetRoll(6001, 8000);
                testCases[CreatureConstants.Hydra_10Heads][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Hydra_11Heads][AgeConstants.Categories.Adulthood] = GetRoll(8001, 10_000);
                testCases[CreatureConstants.Hydra_11Heads][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Hydra_12Heads][AgeConstants.Categories.Adulthood] = GetRoll(10_000, 12_000);
                testCases[CreatureConstants.Hydra_12Heads][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                //Source: https://www.dimensions.com/element/spotted-hyena-crocuta-crocuta (maximum)
                //https://ielc.libguides.com/sdzg/factsheets/spottedhyena (adulthood)
                testCases[CreatureConstants.Hyena][AgeConstants.Categories.Adulthood] = GetAdulthoodRoll(12, 2);
                testCases[CreatureConstants.Hyena][AgeConstants.Categories.MiddleAge] = GetMiddleAgeRoll(12);
                testCases[CreatureConstants.Hyena][AgeConstants.Categories.Old] = GetOldRoll(12);
                testCases[CreatureConstants.Hyena][AgeConstants.Categories.Venerable] = GetVenerableRoll(12, 25);
                testCases[CreatureConstants.Hyena][AgeConstants.Categories.Maximum] = GetMaximumRoll(12, 25);
                testCases[CreatureConstants.IceDevil_Gelugon][AgeConstants.Categories.Adulthood] = outsiderAgeRoll;
                testCases[CreatureConstants.IceDevil_Gelugon][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Imp][AgeConstants.Categories.Adulthood] = outsiderAgeRoll;
                testCases[CreatureConstants.Imp][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.InvisibleStalker][AgeConstants.Categories.Adulthood] = outsiderAgeRoll;
                testCases[CreatureConstants.InvisibleStalker][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Janni][AgeConstants.Categories.Adulthood] = outsiderAgeRoll;
                testCases[CreatureConstants.Janni][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                //Source: http://people.wku.edu/charles.plemons/ad&d/races/age.html
                testCases[CreatureConstants.Kobold][AgeConstants.Categories.Adulthood] = GetRoll(12, 47);
                testCases[CreatureConstants.Kobold][AgeConstants.Categories.MiddleAge] = GetRoll(48, 61);
                testCases[CreatureConstants.Kobold][AgeConstants.Categories.Old] = GetRoll(62, 94);
                testCases[CreatureConstants.Kobold][AgeConstants.Categories.Venerable] = GetRoll(95, 135);
                testCases[CreatureConstants.Kobold][AgeConstants.Categories.Maximum] = "95+2d20";
                testCases[CreatureConstants.Kolyarut][AgeConstants.Categories.Construct] = outsiderAgeRoll;
                testCases[CreatureConstants.Kolyarut][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Kraken][AgeConstants.Categories.Adulthood] = oneHundredKRoll;
                testCases[CreatureConstants.Kraken][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                //Source: https://www.reddit.com/r/DnDBehindTheScreen/comments/fds0yy/ecology_of_the_krenshar/
                testCases[CreatureConstants.Krenshar][AgeConstants.Categories.Adulthood] = GetRoll(4, 9);
                testCases[CreatureConstants.Krenshar][AgeConstants.Categories.MiddleAge] = GetRoll(10, 12);
                testCases[CreatureConstants.Krenshar][AgeConstants.Categories.Old] = GetRoll(13, 15);
                testCases[CreatureConstants.Krenshar][AgeConstants.Categories.Venerable] = GetRoll(16, 20);
                testCases[CreatureConstants.Krenshar][AgeConstants.Categories.Maximum] = GetMaximumRoll(16, 20);
                //Source: https://www.worldanvil.com/w/verum-arcadum/a/kuo-toa-species
                testCases[CreatureConstants.KuoToa][AgeConstants.Categories.Adulthood] = GetRoll(15, 199);
                testCases[CreatureConstants.KuoToa][AgeConstants.Categories.MiddleAge] = GetRoll(200, 449);
                testCases[CreatureConstants.KuoToa][AgeConstants.Categories.Old] = GetRoll(450, 699);
                testCases[CreatureConstants.KuoToa][AgeConstants.Categories.Venerable] = GetRoll(700, 1000);
                testCases[CreatureConstants.KuoToa][AgeConstants.Categories.Maximum] = "700+3d100";
                //Source: https://www.5esrd.com/database/race/lamia/
                testCases[CreatureConstants.Lamia][AgeConstants.Categories.Adulthood] = GetAdulthoodRoll(150, 20);
                testCases[CreatureConstants.Lamia][AgeConstants.Categories.MiddleAge] = GetMiddleAgeRoll(150);
                testCases[CreatureConstants.Lamia][AgeConstants.Categories.Old] = GetOldRoll(150);
                testCases[CreatureConstants.Lamia][AgeConstants.Categories.Venerable] = GetVenerableRoll(150);
                testCases[CreatureConstants.Lamia][AgeConstants.Categories.Maximum] = GetMaximumRoll(150);
                //HACK: Can't find anything about ages. So, making it up, V:150
                testCases[CreatureConstants.Lammasu][AgeConstants.Categories.Adulthood] = GetAdulthoodRoll(150);
                testCases[CreatureConstants.Lammasu][AgeConstants.Categories.MiddleAge] = GetMiddleAgeRoll(150);
                testCases[CreatureConstants.Lammasu][AgeConstants.Categories.Old] = GetOldRoll(150);
                testCases[CreatureConstants.Lammasu][AgeConstants.Categories.Venerable] = GetVenerableRoll(150);
                testCases[CreatureConstants.Lammasu][AgeConstants.Categories.Maximum] = GetMaximumRoll(150);
                testCases[CreatureConstants.LanternArchon][AgeConstants.Categories.Adulthood] = outsiderAgeRoll;
                testCases[CreatureConstants.LanternArchon][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Lemure][AgeConstants.Categories.Adulthood] = outsiderAgeRoll;
                testCases[CreatureConstants.Lemure][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Leonal][AgeConstants.Categories.Adulthood] = outsiderAgeRoll;
                testCases[CreatureConstants.Leonal][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                //Source: https://www.dimensions.com/element/clouded-leopard O:11-15,V:17-20
                //https://nationalzoo.si.edu/animals/clouded-leopard (adulthood)
                testCases[CreatureConstants.Leopard][AgeConstants.Categories.Adulthood] = GetAdulthoodRoll(16, 2);
                testCases[CreatureConstants.Leopard][AgeConstants.Categories.MiddleAge] = GetMiddleAgeRoll(16, 10);
                testCases[CreatureConstants.Leopard][AgeConstants.Categories.Old] = GetRoll(10, 15);
                testCases[CreatureConstants.Leopard][AgeConstants.Categories.Venerable] = GetRoll(16, 20);
                testCases[CreatureConstants.Leopard][AgeConstants.Categories.Maximum] = GetMaximumRoll(16, 20);
                testCases[CreatureConstants.Lillend][AgeConstants.Categories.Adulthood] = outsiderAgeRoll;
                testCases[CreatureConstants.Lillend][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                //Source: https://www.dimensions.com/element/african-lion (maximum)
                //https://denverzoo.org/animals/african-lion/ (adulthood)
                testCases[CreatureConstants.Lion][AgeConstants.Categories.Adulthood] = GetAdulthoodRoll(16, 3);
                testCases[CreatureConstants.Lion][AgeConstants.Categories.MiddleAge] = GetMiddleAgeRoll(16, 10);
                testCases[CreatureConstants.Lion][AgeConstants.Categories.Old] = GetRoll(10, 15);
                testCases[CreatureConstants.Lion][AgeConstants.Categories.Venerable] = GetRoll(16, 30);
                testCases[CreatureConstants.Lion][AgeConstants.Categories.Maximum] = GetMaximumRoll(16, 30);
                testCases[CreatureConstants.Lion_Dire][AgeConstants.Categories.Adulthood] = GetAdulthoodRoll(16, 3);
                testCases[CreatureConstants.Lion_Dire][AgeConstants.Categories.MiddleAge] = GetMiddleAgeRoll(16, 10);
                testCases[CreatureConstants.Lion_Dire][AgeConstants.Categories.Old] = GetRoll(10, 15);
                testCases[CreatureConstants.Lion_Dire][AgeConstants.Categories.Venerable] = GetRoll(16, 30);
                testCases[CreatureConstants.Lion_Dire][AgeConstants.Categories.Maximum] = GetMaximumRoll(16, 30);
                //Source: https://www.dimensions.com/element/green-iguana-iguana-iguana (maximum)
                //https://biobubblepets.com/what-to-expect-when-your-iguana-reaches-sexual-maturity/ (adulthood)
                testCases[CreatureConstants.Lizard][AgeConstants.Categories.Adulthood] = GetAdulthoodRoll(12, 2);
                testCases[CreatureConstants.Lizard][AgeConstants.Categories.MiddleAge] = GetMiddleAgeRoll(12);
                testCases[CreatureConstants.Lizard][AgeConstants.Categories.Old] = GetOldRoll(12);
                testCases[CreatureConstants.Lizard][AgeConstants.Categories.Venerable] = GetRoll(12, 25);
                testCases[CreatureConstants.Lizard][AgeConstants.Categories.Maximum] = GetMaximumRoll(12, 25);
                //Source: http://people.wku.edu/charles.plemons/ad&d/races/age.html
                testCases[CreatureConstants.Lizardfolk][AgeConstants.Categories.Adulthood] = GetRoll(15, 54);
                testCases[CreatureConstants.Lizardfolk][AgeConstants.Categories.MiddleAge] = GetRoll(55, 72);
                testCases[CreatureConstants.Lizardfolk][AgeConstants.Categories.Old] = GetRoll(73, 109);
                testCases[CreatureConstants.Lizardfolk][AgeConstants.Categories.Venerable] = GetRoll(110, 130);
                testCases[CreatureConstants.Lizardfolk][AgeConstants.Categories.Maximum] = "110+2d10";
                //Source: https://www.dimensions.com/element/savannah-monitor-varanus-exanthematicus (maximum)
                //https://www.animalspot.net/savannah-monitor.html (adulthood)
                testCases[CreatureConstants.Lizard_Monitor][AgeConstants.Categories.Adulthood] = GetAdulthoodRoll(8, 2);
                testCases[CreatureConstants.Lizard_Monitor][AgeConstants.Categories.MiddleAge] = GetMiddleAgeRoll(8);
                testCases[CreatureConstants.Lizard_Monitor][AgeConstants.Categories.Old] = GetOldRoll(8);
                testCases[CreatureConstants.Lizard_Monitor][AgeConstants.Categories.Venerable] = GetRoll(8, 20);
                testCases[CreatureConstants.Lizard_Monitor][AgeConstants.Categories.Maximum] = GetMaximumRoll(8, 20);
                //Source: http://people.wku.edu/charles.plemons/ad&d/races/age.html
                testCases[CreatureConstants.Locathah][AgeConstants.Categories.Adulthood] = GetRoll(15, 24);
                testCases[CreatureConstants.Locathah][AgeConstants.Categories.MiddleAge] = GetRoll(25, 32);
                testCases[CreatureConstants.Locathah][AgeConstants.Categories.Old] = GetRoll(33, 49);
                testCases[CreatureConstants.Locathah][AgeConstants.Categories.Venerable] = GetRoll(50, 74);
                testCases[CreatureConstants.Locathah][AgeConstants.Categories.Maximum] = "50+2d12";
                testCases[CreatureConstants.Locust_Swarm][AgeConstants.Categories.Swarm] = RollHelper.GetRollWithMostEvenDistribution(0, 1, true);
                testCases[CreatureConstants.Locust_Swarm][AgeConstants.Categories.Maximum] = RollHelper.GetRollWithFewestDice(0, 1);
                testCases[CreatureConstants.Magmin][AgeConstants.Categories.Adulthood] = outsiderAgeRoll;
                testCases[CreatureConstants.Magmin][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                //Source: https://www.dimensions.com/element/reef-manta-ray-mobula-alfredi (maximum)
                //https://saveourseas.com/worldofsharks/species/reef-manta-ray (adulthood)
                testCases[CreatureConstants.MantaRay][AgeConstants.Categories.Adulthood] = GetAdulthoodRoll(40, 8);
                testCases[CreatureConstants.MantaRay][AgeConstants.Categories.MiddleAge] = GetMiddleAgeRoll(40);
                testCases[CreatureConstants.MantaRay][AgeConstants.Categories.Old] = GetOldRoll(40);
                testCases[CreatureConstants.MantaRay][AgeConstants.Categories.Venerable] = GetRoll(40, 50);
                testCases[CreatureConstants.MantaRay][AgeConstants.Categories.Maximum] = GetMaximumRoll(40, 50);
                //Source: https://dumpstatadventures.com/blog/deep-dive-the-manticore (adulthood)
                //https://www.tumblr.com/encyclopediafantasytome/100850072650/creature-manticore (maximum)
                testCases[CreatureConstants.Manticore][AgeConstants.Categories.Adulthood] = GetAdulthoodRoll(20, 5);
                testCases[CreatureConstants.Manticore][AgeConstants.Categories.MiddleAge] = GetMiddleAgeRoll(20);
                testCases[CreatureConstants.Manticore][AgeConstants.Categories.Old] = GetOldRoll(20);
                testCases[CreatureConstants.Manticore][AgeConstants.Categories.Venerable] = GetVenerableRoll(20);
                testCases[CreatureConstants.Manticore][AgeConstants.Categories.Maximum] = GetMaximumRoll(20);
                testCases[CreatureConstants.Marilith][AgeConstants.Categories.Adulthood] = outsiderAgeRoll;
                testCases[CreatureConstants.Marilith][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Marut][AgeConstants.Categories.Construct] = outsiderAgeRoll;
                testCases[CreatureConstants.Marut][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                //Source: https://forgottenrealms.fandom.com/wiki/Medusa (adulthood) Same as humans
                //https://www.worldanvil.com/w/faerun-tatortotzke/a/medusa-species (maximum) immortal, so using 1K for adulthood max
                testCases[CreatureConstants.Medusa][AgeConstants.Categories.Adulthood] = GetRoll(15, 1000);
                testCases[CreatureConstants.Medusa][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                //Source: https://jurassicworld-evolution.fandom.com/wiki/Velociraptor
                //No specific "megaraptor", but how Jurassic Park treats Velocirapter is more like a Megaraptor/Utahraptor than an actual velociraptor
                testCases[CreatureConstants.Megaraptor][AgeConstants.Categories.Adulthood] = GetAdulthoodRollFromAverage(70);
                testCases[CreatureConstants.Megaraptor][AgeConstants.Categories.MiddleAge] = GetMiddleAgeRollFromAverage(70);
                testCases[CreatureConstants.Megaraptor][AgeConstants.Categories.Old] = GetOldRollFromAverage(70);
                testCases[CreatureConstants.Megaraptor][AgeConstants.Categories.Venerable] = GetVenerableRollFromAverage(70);
                testCases[CreatureConstants.Megaraptor][AgeConstants.Categories.Maximum] = GetMaximumRollFromAverage(70);
                testCases[CreatureConstants.Mephit_Air][AgeConstants.Categories.Adulthood] = outsiderAgeRoll;
                testCases[CreatureConstants.Mephit_Air][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Mephit_Dust][AgeConstants.Categories.Adulthood] = outsiderAgeRoll;
                testCases[CreatureConstants.Mephit_Dust][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Mephit_Earth][AgeConstants.Categories.Adulthood] = outsiderAgeRoll;
                testCases[CreatureConstants.Mephit_Earth][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Mephit_Fire][AgeConstants.Categories.Adulthood] = outsiderAgeRoll;
                testCases[CreatureConstants.Mephit_Fire][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Mephit_Ice][AgeConstants.Categories.Adulthood] = outsiderAgeRoll;
                testCases[CreatureConstants.Mephit_Ice][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Mephit_Magma][AgeConstants.Categories.Adulthood] = outsiderAgeRoll;
                testCases[CreatureConstants.Mephit_Magma][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Mephit_Ooze][AgeConstants.Categories.Adulthood] = outsiderAgeRoll;
                testCases[CreatureConstants.Mephit_Ooze][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Mephit_Salt][AgeConstants.Categories.Adulthood] = outsiderAgeRoll;
                testCases[CreatureConstants.Mephit_Salt][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Mephit_Steam][AgeConstants.Categories.Adulthood] = outsiderAgeRoll;
                testCases[CreatureConstants.Mephit_Steam][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Mephit_Water][AgeConstants.Categories.Adulthood] = outsiderAgeRoll;
                testCases[CreatureConstants.Mephit_Water][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                //Source: http://people.wku.edu/charles.plemons/ad&d/races/age.html
                testCases[CreatureConstants.Merfolk][AgeConstants.Categories.Adulthood] = GetRoll(15, 44);
                testCases[CreatureConstants.Merfolk][AgeConstants.Categories.MiddleAge] = GetRoll(45, 59);
                testCases[CreatureConstants.Merfolk][AgeConstants.Categories.Old] = GetRoll(60, 89);
                testCases[CreatureConstants.Merfolk][AgeConstants.Categories.Venerable] = GetRoll(90, 130);
                testCases[CreatureConstants.Merfolk][AgeConstants.Categories.Maximum] = "90+2d20";
                //Source: https://forgottenrealms.fandom.com/wiki/Mimic (adulthood)
                //Can't find reliable source on max age, so will use V:100
                testCases[CreatureConstants.Mimic][AgeConstants.Categories.Adulthood] = GetAdulthoodRoll(100, 2);
                testCases[CreatureConstants.Mimic][AgeConstants.Categories.MiddleAge] = GetMiddleAgeRoll(100);
                testCases[CreatureConstants.Mimic][AgeConstants.Categories.Old] = GetOldRoll(100);
                testCases[CreatureConstants.Mimic][AgeConstants.Categories.Venerable] = GetVenerableRoll(100);
                testCases[CreatureConstants.Mimic][AgeConstants.Categories.Maximum] = GetMaximumRoll(100);
                //Source: https://forgottenrealms.fandom.com/wiki/Mind_flayer
                testCases[CreatureConstants.MindFlayer][AgeConstants.Categories.Adulthood] = GetAdulthoodRollFromAverage(125, 10);
                testCases[CreatureConstants.MindFlayer][AgeConstants.Categories.MiddleAge] = GetMiddleAgeRollFromAverage(125);
                testCases[CreatureConstants.MindFlayer][AgeConstants.Categories.Old] = GetOldRollFromAverage(125);
                testCases[CreatureConstants.MindFlayer][AgeConstants.Categories.Venerable] = GetVenerableRollFromAverage(125);
                testCases[CreatureConstants.MindFlayer][AgeConstants.Categories.Maximum] = GetMaximumRollFromAverage(125);
                //Source: http://people.wku.edu/charles.plemons/ad&d/races/age.html
                testCases[CreatureConstants.Minotaur][AgeConstants.Categories.Adulthood] = GetRoll(12, 74);
                testCases[CreatureConstants.Minotaur][AgeConstants.Categories.MiddleAge] = GetRoll(75, 99);
                testCases[CreatureConstants.Minotaur][AgeConstants.Categories.Old] = GetRoll(100, 149);
                testCases[CreatureConstants.Minotaur][AgeConstants.Categories.Venerable] = GetRoll(150, 250);
                testCases[CreatureConstants.Minotaur][AgeConstants.Categories.Maximum] = "150+1d100";
                testCases[CreatureConstants.Mohrg][AgeConstants.Categories.Undead] = undeadAgeRoll;
                testCases[CreatureConstants.Mohrg][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                //Source: https://www.dimensions.com/element/tufted-capuchin-sapajus-apella (maximum)
                //https://en.wikipedia.org/wiki/Capuchin_monkey (adulthood)
                testCases[CreatureConstants.Monkey][AgeConstants.Categories.Adulthood] = GetAdulthoodRoll(26, 6);
                testCases[CreatureConstants.Monkey][AgeConstants.Categories.MiddleAge] = GetMiddleAgeRoll(26, 15);
                testCases[CreatureConstants.Monkey][AgeConstants.Categories.Old] = GetRoll(15, 25);
                testCases[CreatureConstants.Monkey][AgeConstants.Categories.Venerable] = GetRoll(26, 50);
                testCases[CreatureConstants.Monkey][AgeConstants.Categories.Maximum] = GetMaximumRoll(26, 50);
                //Source: https://www.dimensions.com/element/mule-equus-asinus-x-equus-caballus
                //https://pdf4pro.com/cdn/maturity-in-horses-and-mules-alberta-37ea7b.pdf (adulthood, same as arabian horse)
                testCases[CreatureConstants.Mule][AgeConstants.Categories.Adulthood] = GetAdulthoodRoll(30, 4);
                testCases[CreatureConstants.Mule][AgeConstants.Categories.MiddleAge] = GetMiddleAgeRoll(30);
                testCases[CreatureConstants.Mule][AgeConstants.Categories.Old] = GetOldRoll(30);
                testCases[CreatureConstants.Mule][AgeConstants.Categories.Venerable] = GetRoll(30, 40);
                testCases[CreatureConstants.Mule][AgeConstants.Categories.Maximum] = GetMaximumRoll(30, 40);
                testCases[CreatureConstants.Mummy][AgeConstants.Categories.Undead] = undeadAgeRoll;
                testCases[CreatureConstants.Mummy][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                //Source: https://www.worldanvil.com/w/faerun-tatortotzke/a/naga-species
                testCases[CreatureConstants.Naga_Dark][AgeConstants.Categories.Adulthood] = $"{tenKRoll}+20";
                testCases[CreatureConstants.Naga_Dark][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Naga_Guardian][AgeConstants.Categories.Adulthood] = $"{tenKRoll}+20";
                testCases[CreatureConstants.Naga_Guardian][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Naga_Spirit][AgeConstants.Categories.Adulthood] = $"{tenKRoll}+20";
                testCases[CreatureConstants.Naga_Spirit][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Naga_Water][AgeConstants.Categories.Adulthood] = $"{tenKRoll}+20";
                testCases[CreatureConstants.Naga_Water][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Nalfeshnee][AgeConstants.Categories.Adulthood] = outsiderAgeRoll;
                testCases[CreatureConstants.Nalfeshnee][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.NightHag][AgeConstants.Categories.Adulthood] = $"{outsiderAgeRoll}+50";
                testCases[CreatureConstants.NightHag][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Nightcrawler][AgeConstants.Categories.Undead] = undeadAgeRoll;
                testCases[CreatureConstants.Nightcrawler][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Nightmare][AgeConstants.Categories.Adulthood] = outsiderAgeRoll;
                testCases[CreatureConstants.Nightmare][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Nightmare_Cauchemar][AgeConstants.Categories.Adulthood] = outsiderAgeRoll;
                testCases[CreatureConstants.Nightmare_Cauchemar][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Nightwalker][AgeConstants.Categories.Undead] = undeadAgeRoll;
                testCases[CreatureConstants.Nightwalker][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Nightwing][AgeConstants.Categories.Undead] = undeadAgeRoll;
                testCases[CreatureConstants.Nightwing][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                //Source: http://people.wku.edu/charles.plemons/ad&d/races/age.html - from Pixie
                testCases[CreatureConstants.Nixie][AgeConstants.Categories.Adulthood] = "100";
                testCases[CreatureConstants.Nixie][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(100, 132, true);
                testCases[CreatureConstants.Nixie][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(133, 199, true);
                testCases[CreatureConstants.Nixie][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(200, 400, true);
                testCases[CreatureConstants.Nixie][AgeConstants.Categories.Maximum] = "200+2d100";
                testCases[CreatureConstants.Nymph][AgeConstants.Categories.Adulthood] = $"{feyAgeRoll}+20";
                testCases[CreatureConstants.Nymph][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                //TODO: From here
                testCases[CreatureConstants.OchreJelly][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(1, 100, true);
                testCases[CreatureConstants.OchreJelly][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Octopus][AgeConstants.Categories.Adulthood] = "0";
                testCases[CreatureConstants.Octopus][AgeConstants.Categories.MiddleAge] = "1";
                testCases[CreatureConstants.Octopus][AgeConstants.Categories.Old] = "2";
                testCases[CreatureConstants.Octopus][AgeConstants.Categories.Venerable] = "1d3+2";
                testCases[CreatureConstants.Octopus][AgeConstants.Categories.Maximum] = "2+1d3";
                testCases[CreatureConstants.Octopus_Giant][AgeConstants.Categories.Adulthood] = "0";
                testCases[CreatureConstants.Octopus_Giant][AgeConstants.Categories.MiddleAge] = "1";
                testCases[CreatureConstants.Octopus_Giant][AgeConstants.Categories.Old] = "2";
                testCases[CreatureConstants.Octopus_Giant][AgeConstants.Categories.Venerable] = "1d3+2";
                testCases[CreatureConstants.Octopus_Giant][AgeConstants.Categories.Maximum] = "2+1d3";
                //Source: http://people.wku.edu/charles.plemons/ad&d/races/age.html
                testCases[CreatureConstants.Ogre][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(15, 44, true);
                testCases[CreatureConstants.Ogre][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(45, 59, true);
                testCases[CreatureConstants.Ogre][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(60, 89, true);
                testCases[CreatureConstants.Ogre][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(90, 130, true);
                testCases[CreatureConstants.Ogre][AgeConstants.Categories.Maximum] = "90+2d20";
                testCases[CreatureConstants.Ogre_Merrow][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(15, 44, true);
                testCases[CreatureConstants.Ogre_Merrow][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(45, 59, true);
                testCases[CreatureConstants.Ogre_Merrow][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(60, 89, true);
                testCases[CreatureConstants.Ogre_Merrow][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(90, 130, true);
                testCases[CreatureConstants.Ogre_Merrow][AgeConstants.Categories.Maximum] = "90+2d20";
                //Source: http://people.wku.edu/charles.plemons/ad&d/races/age.html
                testCases[CreatureConstants.OgreMage][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(20, 81, true);
                testCases[CreatureConstants.OgreMage][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(82, 115, true);
                testCases[CreatureConstants.OgreMage][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(116, 174, true);
                testCases[CreatureConstants.OgreMage][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(175, 215, true);
                testCases[CreatureConstants.OgreMage][AgeConstants.Categories.Maximum] = "175+2d20";
                //Source: http://people.wku.edu/charles.plemons/ad&d/races/age.html
                testCases[CreatureConstants.Orc][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(10, 16, true);
                testCases[CreatureConstants.Orc][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(17, 22, true);
                testCases[CreatureConstants.Orc][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(23, 34, true);
                testCases[CreatureConstants.Orc][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(35, 45, true);
                testCases[CreatureConstants.Orc][AgeConstants.Categories.Maximum] = "35+1d10";
                //Source: https://www.d20srd.org/srd/description.htm#vitalStatistics
                testCases[CreatureConstants.Orc_Half][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(14, 29, true);
                testCases[CreatureConstants.Orc_Half][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(30, 44, true);
                testCases[CreatureConstants.Orc_Half][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(45, 59, true);
                testCases[CreatureConstants.Orc_Half][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(60, 80, true);
                testCases[CreatureConstants.Orc_Half][AgeConstants.Categories.Maximum] = "60+2d10";
                testCases[CreatureConstants.Otyugh][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(1, 100, true);
                testCases[CreatureConstants.Otyugh][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Owl][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(0, 2, true);
                testCases[CreatureConstants.Owl][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(3, 3, true);
                testCases[CreatureConstants.Owl][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(4, 4, true);
                testCases[CreatureConstants.Owl][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(5, 12, true);
                testCases[CreatureConstants.Owl][AgeConstants.Categories.Maximum] = "4+1d8";
                testCases[CreatureConstants.Owl_Giant][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(0, 2, true);
                testCases[CreatureConstants.Owl_Giant][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(3, 3, true);
                testCases[CreatureConstants.Owl_Giant][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(4, 4, true);
                testCases[CreatureConstants.Owl_Giant][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(5, 12, true);
                testCases[CreatureConstants.Owl_Giant][AgeConstants.Categories.Maximum] = "4+1d8";
                testCases[CreatureConstants.Owlbear][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(4, 11, true);
                testCases[CreatureConstants.Owlbear][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(12, 14, true);
                testCases[CreatureConstants.Owlbear][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(15, 17, true);
                testCases[CreatureConstants.Owlbear][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(18, 22, true);
                testCases[CreatureConstants.Owlbear][AgeConstants.Categories.Maximum] = "18+1d4";
                testCases[CreatureConstants.Pegasus][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(8, 25, true);
                testCases[CreatureConstants.Pegasus][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(26, 30, true);
                testCases[CreatureConstants.Pegasus][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(31, 35, true);
                testCases[CreatureConstants.Pegasus][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(36, 44, true);
                testCases[CreatureConstants.Pegasus][AgeConstants.Categories.Maximum] = "36+1d8";
                //INFO: Can't find anything on lifespan. So, making it up
                testCases[CreatureConstants.PhantomFungus][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(5, 34, true);
                testCases[CreatureConstants.PhantomFungus][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(35, 39, true);
                testCases[CreatureConstants.PhantomFungus][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(40, 44, true);
                testCases[CreatureConstants.PhantomFungus][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(45, 55, true);
                testCases[CreatureConstants.PhantomFungus][AgeConstants.Categories.Maximum] = "45+1d10";
                //INFO: Can't find anything on lifespan. So, making it up
                testCases[CreatureConstants.PhaseSpider][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(5, 34, true);
                testCases[CreatureConstants.PhaseSpider][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(35, 39, true);
                testCases[CreatureConstants.PhaseSpider][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(40, 44, true);
                testCases[CreatureConstants.PhaseSpider][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(45, 55, true);
                testCases[CreatureConstants.PhaseSpider][AgeConstants.Categories.Maximum] = "45+1d10";
                //INFO: Can't find anything on lifespan. So, making it up
                testCases[CreatureConstants.Phasm][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(5, 34, true);
                testCases[CreatureConstants.Phasm][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(35, 39, true);
                testCases[CreatureConstants.Phasm][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(40, 44, true);
                testCases[CreatureConstants.Phasm][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(45, 55, true);
                testCases[CreatureConstants.Phasm][AgeConstants.Categories.Maximum] = "45+1d10";
                testCases[CreatureConstants.PitFiend][AgeConstants.Categories.Adulthood] = outsiderAgeRoll;
                testCases[CreatureConstants.PitFiend][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                //Source: http://people.wku.edu/charles.plemons/ad&d/races/age.html
                testCases[CreatureConstants.Pixie][AgeConstants.Categories.Adulthood] = "100";
                testCases[CreatureConstants.Pixie][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(100, 132, true);
                testCases[CreatureConstants.Pixie][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(133, 199, true);
                testCases[CreatureConstants.Pixie][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(200, 400, true);
                testCases[CreatureConstants.Pixie][AgeConstants.Categories.Maximum] = "200+2d100";
                testCases[CreatureConstants.Pixie_WithIrresistibleDance][AgeConstants.Categories.Adulthood] = "100";
                testCases[CreatureConstants.Pixie_WithIrresistibleDance][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(100, 132, true);
                testCases[CreatureConstants.Pixie_WithIrresistibleDance][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(133, 199, true);
                testCases[CreatureConstants.Pixie_WithIrresistibleDance][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(200, 400, true);
                testCases[CreatureConstants.Pixie_WithIrresistibleDance][AgeConstants.Categories.Maximum] = "200+2d100";
                testCases[CreatureConstants.Pony][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(5, 9, true);
                testCases[CreatureConstants.Pony][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(10, 14, true);
                testCases[CreatureConstants.Pony][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(15, 24, true);
                testCases[CreatureConstants.Pony][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(25, 40, true);
                testCases[CreatureConstants.Pony][AgeConstants.Categories.Maximum] = "24+2d8";
                testCases[CreatureConstants.Pony_War][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(5, 9, true);
                testCases[CreatureConstants.Pony_War][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(10, 14, true);
                testCases[CreatureConstants.Pony_War][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(15, 24, true);
                testCases[CreatureConstants.Pony_War][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(25, 40, true);
                testCases[CreatureConstants.Pony_War][AgeConstants.Categories.Maximum] = "24+2d8";
                testCases[CreatureConstants.Porpoise][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(3, 5, true);
                testCases[CreatureConstants.Porpoise][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(6, 6, true);
                testCases[CreatureConstants.Porpoise][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(7, 7, true);
                testCases[CreatureConstants.Porpoise][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(8, 10, true);
                testCases[CreatureConstants.Porpoise][AgeConstants.Categories.Maximum] = "7+1d3";
                testCases[CreatureConstants.PrayingMantis_Giant][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(0, 0, true);
                testCases[CreatureConstants.PrayingMantis_Giant][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(0, 0, true);
                testCases[CreatureConstants.PrayingMantis_Giant][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(0, 0, true);
                testCases[CreatureConstants.PrayingMantis_Giant][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(0, 1, true);
                testCases[CreatureConstants.PrayingMantis_Giant][AgeConstants.Categories.Maximum] = "1d2-1";
                testCases[CreatureConstants.Pseudodragon][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(2, 6, true);
                testCases[CreatureConstants.Pseudodragon][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(7, 8, true);
                testCases[CreatureConstants.Pseudodragon][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(9, 9, true);
                testCases[CreatureConstants.Pseudodragon][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(10, 15, true);
                testCases[CreatureConstants.Pseudodragon][AgeConstants.Categories.Maximum] = "9+1d6";
                testCases[CreatureConstants.PurpleWorm][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(30, 99, true);
                testCases[CreatureConstants.PurpleWorm][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(100, 119, true);
                testCases[CreatureConstants.PurpleWorm][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(120, 134, true);
                testCases[CreatureConstants.PurpleWorm][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(135, 165, true);
                testCases[CreatureConstants.PurpleWorm][AgeConstants.Categories.Maximum] = "135+3d10";
                //Source: https://www.reddit.com/r/DnDBehindTheScreen/comments/63zx1m/hydra/
                testCases[CreatureConstants.Pyrohydra_5Heads][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(25, 25 * 20, true);
                testCases[CreatureConstants.Pyrohydra_5Heads][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Pyrohydra_6Heads][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(501, 1000, true);
                testCases[CreatureConstants.Pyrohydra_6Heads][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Pyrohydra_7Heads][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(1001, 2000, true);
                testCases[CreatureConstants.Pyrohydra_7Heads][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Pyrohydra_8Heads][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(2001, 4000, true);
                testCases[CreatureConstants.Pyrohydra_8Heads][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Pyrohydra_9Heads][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(4001, 6000, true);
                testCases[CreatureConstants.Pyrohydra_9Heads][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Pyrohydra_10Heads][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(6001, 8000, true);
                testCases[CreatureConstants.Pyrohydra_10Heads][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Pyrohydra_11Heads][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(8001, 10_000, true);
                testCases[CreatureConstants.Pyrohydra_11Heads][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Pyrohydra_12Heads][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(10_000, 12_000, true);
                testCases[CreatureConstants.Pyrohydra_12Heads][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Quasit][AgeConstants.Categories.Adulthood] = outsiderAgeRoll;
                testCases[CreatureConstants.Quasit][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Rakshasa][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(1, 1_000, true);
                testCases[CreatureConstants.Rakshasa][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                //INFO: Can't find anything on lifespan. So, making it up
                testCases[CreatureConstants.Rast][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(5, 34, true);
                testCases[CreatureConstants.Rast][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(35, 39, true);
                testCases[CreatureConstants.Rast][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(40, 44, true);
                testCases[CreatureConstants.Rast][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(45, 55, true);
                testCases[CreatureConstants.Rast][AgeConstants.Categories.Maximum] = "45+1d10";
                testCases[CreatureConstants.Rat][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(0, 0, true);
                testCases[CreatureConstants.Rat][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(1, 1, true);
                testCases[CreatureConstants.Rat][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(2, 2, true);
                testCases[CreatureConstants.Rat][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(3, 4, true);
                testCases[CreatureConstants.Rat][AgeConstants.Categories.Maximum] = "2+1d2";
                testCases[CreatureConstants.Rat_Dire][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(0, 0, true);
                testCases[CreatureConstants.Rat_Dire][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(1, 1, true);
                testCases[CreatureConstants.Rat_Dire][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(2, 2, true);
                testCases[CreatureConstants.Rat_Dire][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(3, 4, true);
                testCases[CreatureConstants.Rat_Dire][AgeConstants.Categories.Maximum] = "2+1d2";
                testCases[CreatureConstants.Rat_Swarm][AgeConstants.Categories.Swarm] = RollHelper.GetRollWithMostEvenDistribution(0, 6, true);
                testCases[CreatureConstants.Rat_Swarm][AgeConstants.Categories.Maximum] = RollHelper.GetRollWithFewestDice(0, 6);
                testCases[CreatureConstants.Raven][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(1, 5, true);
                testCases[CreatureConstants.Raven][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(6, 7, true);
                testCases[CreatureConstants.Raven][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(8, 9, true);
                testCases[CreatureConstants.Raven][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(10, 15, true);
                testCases[CreatureConstants.Raven][AgeConstants.Categories.Maximum] = "9+1d6";
                testCases[CreatureConstants.Ravid][AgeConstants.Categories.Adulthood] = outsiderAgeRoll;
                testCases[CreatureConstants.Ravid][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.RazorBoar][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(2, 5, true);
                testCases[CreatureConstants.RazorBoar][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(6, 7, true);
                testCases[CreatureConstants.RazorBoar][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(8, 9, true);
                testCases[CreatureConstants.RazorBoar][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(10, 14, true);
                testCases[CreatureConstants.RazorBoar][AgeConstants.Categories.Maximum] = "10+1d4";
                testCases[CreatureConstants.Remorhaz][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(0, 15, true);
                testCases[CreatureConstants.Remorhaz][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(16, 20, true);
                testCases[CreatureConstants.Remorhaz][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(21, 26, true);
                testCases[CreatureConstants.Remorhaz][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(27, 33, true);
                testCases[CreatureConstants.Remorhaz][AgeConstants.Categories.Maximum] = "27+1d6";
                testCases[CreatureConstants.Retriever][AgeConstants.Categories.Adulthood] = outsiderAgeRoll;
                testCases[CreatureConstants.Retriever][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Rhinoceras][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(3, 19, true);
                testCases[CreatureConstants.Rhinoceras][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(20, 25, true);
                testCases[CreatureConstants.Rhinoceras][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(26, 34, true);
                testCases[CreatureConstants.Rhinoceras][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(35, 50, true);
                testCases[CreatureConstants.Rhinoceras][AgeConstants.Categories.Maximum] = "34+2d8";
                testCases[CreatureConstants.Roc][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(20, 99, true);
                testCases[CreatureConstants.Roc][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(100, 149, true);
                testCases[CreatureConstants.Roc][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(150, 199, true);
                testCases[CreatureConstants.Roc][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(200, 400, true);
                testCases[CreatureConstants.Roc][AgeConstants.Categories.Maximum] = "200+2d100";
                //INFO: Can't find anything on lifespan. So, making it up
                testCases[CreatureConstants.Roper][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(5, 34, true);
                testCases[CreatureConstants.Roper][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(35, 39, true);
                testCases[CreatureConstants.Roper][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(40, 44, true);
                testCases[CreatureConstants.Roper][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(45, 55, true);
                testCases[CreatureConstants.Roper][AgeConstants.Categories.Maximum] = "45+1d10";
                //INFO: Could find the adult age, but as for max, only that they live for "decades"
                testCases[CreatureConstants.RustMonster][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(2, 34, true);
                testCases[CreatureConstants.RustMonster][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(35, 39, true);
                testCases[CreatureConstants.RustMonster][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(40, 44, true);
                testCases[CreatureConstants.RustMonster][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(45, 55, true);
                testCases[CreatureConstants.RustMonster][AgeConstants.Categories.Maximum] = "45+1d10";
                //Source: http://people.wku.edu/charles.plemons/ad&d/races/age.html
                testCases[CreatureConstants.Sahuagin][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(20, 74, true);
                testCases[CreatureConstants.Sahuagin][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(75, 99, true);
                testCases[CreatureConstants.Sahuagin][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(100, 149, true);
                testCases[CreatureConstants.Sahuagin][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(150, 190, true);
                testCases[CreatureConstants.Sahuagin][AgeConstants.Categories.Maximum] = "150+2d20";
                testCases[CreatureConstants.Sahuagin_Malenti][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(20, 74, true);
                testCases[CreatureConstants.Sahuagin_Malenti][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(75, 99, true);
                testCases[CreatureConstants.Sahuagin_Malenti][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(100, 149, true);
                testCases[CreatureConstants.Sahuagin_Malenti][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(150, 190, true);
                testCases[CreatureConstants.Sahuagin_Malenti][AgeConstants.Categories.Maximum] = "150+2d20";
                testCases[CreatureConstants.Sahuagin_Mutant][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(20, 74, true);
                testCases[CreatureConstants.Sahuagin_Mutant][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(75, 99, true);
                testCases[CreatureConstants.Sahuagin_Mutant][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(100, 149, true);
                testCases[CreatureConstants.Sahuagin_Mutant][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(150, 190, true);
                testCases[CreatureConstants.Sahuagin_Mutant][AgeConstants.Categories.Maximum] = "150+2d20";
                testCases[CreatureConstants.Salamander_Flamebrother][AgeConstants.Categories.Salamander.Flamebrother] = RollHelper.GetRollWithMostEvenDistribution(10, 99, true);
                testCases[CreatureConstants.Salamander_Flamebrother][AgeConstants.Categories.Maximum] = "100";
                testCases[CreatureConstants.Salamander_Average][AgeConstants.Categories.Salamander.Average] = RollHelper.GetRollWithMostEvenDistribution(100, 999, true);
                testCases[CreatureConstants.Salamander_Average][AgeConstants.Categories.Maximum] = "1000";
                testCases[CreatureConstants.Salamander_Noble][AgeConstants.Categories.Salamander.Noble] = RollHelper.GetRollWithMostEvenDistribution(1000, 10_000, true);
                testCases[CreatureConstants.Salamander_Noble][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                //Source: http://people.wku.edu/charles.plemons/ad&d/races/age.html
                testCases[CreatureConstants.Satyr][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(20, 49, true);
                testCases[CreatureConstants.Satyr][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(50, 66, true);
                testCases[CreatureConstants.Satyr][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(67, 99, true);
                testCases[CreatureConstants.Satyr][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(100, 200, true);
                testCases[CreatureConstants.Satyr][AgeConstants.Categories.Maximum] = "100+1d100";
                testCases[CreatureConstants.Satyr_WithPipes][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(20, 49, true);
                testCases[CreatureConstants.Satyr_WithPipes][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(50, 66, true);
                testCases[CreatureConstants.Satyr_WithPipes][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(67, 99, true);
                testCases[CreatureConstants.Satyr_WithPipes][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(100, 200, true);
                testCases[CreatureConstants.Satyr_WithPipes][AgeConstants.Categories.Maximum] = "100+1d100";
                testCases[CreatureConstants.Scorpion_Monstrous_Colossal][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(0, 2, true);
                testCases[CreatureConstants.Scorpion_Monstrous_Colossal][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(3, 3, true);
                testCases[CreatureConstants.Scorpion_Monstrous_Colossal][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(4, 4, true);
                testCases[CreatureConstants.Scorpion_Monstrous_Colossal][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(5, 15, true);
                testCases[CreatureConstants.Scorpion_Monstrous_Colossal][AgeConstants.Categories.Maximum] = "5+1d10";
                testCases[CreatureConstants.Scorpion_Monstrous_Gargantuan][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(0, 2, true);
                testCases[CreatureConstants.Scorpion_Monstrous_Gargantuan][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(3, 3, true);
                testCases[CreatureConstants.Scorpion_Monstrous_Gargantuan][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(4, 4, true);
                testCases[CreatureConstants.Scorpion_Monstrous_Gargantuan][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(5, 15, true);
                testCases[CreatureConstants.Scorpion_Monstrous_Gargantuan][AgeConstants.Categories.Maximum] = "5+1d10";
                testCases[CreatureConstants.Scorpion_Monstrous_Huge][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(0, 2, true);
                testCases[CreatureConstants.Scorpion_Monstrous_Huge][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(3, 3, true);
                testCases[CreatureConstants.Scorpion_Monstrous_Huge][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(4, 4, true);
                testCases[CreatureConstants.Scorpion_Monstrous_Huge][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(5, 15, true);
                testCases[CreatureConstants.Scorpion_Monstrous_Huge][AgeConstants.Categories.Maximum] = "5+1d10";
                testCases[CreatureConstants.Scorpion_Monstrous_Large][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(0, 2, true);
                testCases[CreatureConstants.Scorpion_Monstrous_Large][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(3, 3, true);
                testCases[CreatureConstants.Scorpion_Monstrous_Large][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(4, 4, true);
                testCases[CreatureConstants.Scorpion_Monstrous_Large][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(5, 15, true);
                testCases[CreatureConstants.Scorpion_Monstrous_Large][AgeConstants.Categories.Maximum] = "5+1d10";
                testCases[CreatureConstants.Scorpion_Monstrous_Medium][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(0, 2, true);
                testCases[CreatureConstants.Scorpion_Monstrous_Medium][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(3, 3, true);
                testCases[CreatureConstants.Scorpion_Monstrous_Medium][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(4, 4, true);
                testCases[CreatureConstants.Scorpion_Monstrous_Medium][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(5, 15, true);
                testCases[CreatureConstants.Scorpion_Monstrous_Medium][AgeConstants.Categories.Maximum] = "5+1d10";
                testCases[CreatureConstants.Scorpion_Monstrous_Small][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(0, 2, true);
                testCases[CreatureConstants.Scorpion_Monstrous_Small][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(3, 3, true);
                testCases[CreatureConstants.Scorpion_Monstrous_Small][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(4, 4, true);
                testCases[CreatureConstants.Scorpion_Monstrous_Small][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(5, 15, true);
                testCases[CreatureConstants.Scorpion_Monstrous_Small][AgeConstants.Categories.Maximum] = "5+1d10";
                testCases[CreatureConstants.Scorpion_Monstrous_Tiny][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(0, 2, true);
                testCases[CreatureConstants.Scorpion_Monstrous_Tiny][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(3, 3, true);
                testCases[CreatureConstants.Scorpion_Monstrous_Tiny][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(4, 4, true);
                testCases[CreatureConstants.Scorpion_Monstrous_Tiny][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(5, 15, true);
                testCases[CreatureConstants.Scorpion_Monstrous_Tiny][AgeConstants.Categories.Maximum] = "5+1d10";
                //INFO: Can't find info on aging, so copying from Centaur
                testCases[CreatureConstants.Scorpionfolk][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(18, 36, true);
                testCases[CreatureConstants.Scorpionfolk][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(37, 49, true);
                testCases[CreatureConstants.Scorpionfolk][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(50, 74, true);
                testCases[CreatureConstants.Scorpionfolk][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(75, 115, true);
                testCases[CreatureConstants.Scorpionfolk][AgeConstants.Categories.Maximum] = "75+2d20";
                //Source: https://aminoapps.com/c/officialdd/page/item/half-sea-cat/o3Y5_0JQioIQe2k54E2KKPY4nbbDMn84bGb
                testCases[CreatureConstants.SeaCat][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(20, 119, true);
                testCases[CreatureConstants.SeaCat][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(120, 199, true);
                testCases[CreatureConstants.SeaCat][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(200, 269, true);
                testCases[CreatureConstants.SeaCat][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(270, 330, true);
                testCases[CreatureConstants.SeaCat][AgeConstants.Categories.Maximum] = "270+3d20";
                testCases[CreatureConstants.SeaHag][AgeConstants.Categories.Adulthood] = GetRoll(50, 500);
                testCases[CreatureConstants.SeaHag][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Shadow][AgeConstants.Categories.Undead] = undeadAgeRoll;
                testCases[CreatureConstants.Shadow][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Shadow_Greater][AgeConstants.Categories.Undead] = undeadAgeRoll;
                testCases[CreatureConstants.Shadow_Greater][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.ShadowMastiff][AgeConstants.Categories.Adulthood] = outsiderAgeRoll;
                testCases[CreatureConstants.ShadowMastiff][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                //INFO: Can't find anything on lifespan. So, making it up
                testCases[CreatureConstants.ShamblingMound][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(20, 49, true);
                testCases[CreatureConstants.ShamblingMound][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(50, 69, true);
                testCases[CreatureConstants.ShamblingMound][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(70, 89, true);
                testCases[CreatureConstants.ShamblingMound][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(90, 110, true);
                testCases[CreatureConstants.ShamblingMound][AgeConstants.Categories.Maximum] = "90+2d10";
                testCases[CreatureConstants.Shark_Medium][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(14, 17, true);
                testCases[CreatureConstants.Shark_Medium][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(18, 18, true);
                testCases[CreatureConstants.Shark_Medium][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(19, 19, true);
                testCases[CreatureConstants.Shark_Medium][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(20, 30, true);
                testCases[CreatureConstants.Shark_Medium][AgeConstants.Categories.Maximum] = "20+1d10";
                testCases[CreatureConstants.Shark_Large][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(14, 17, true);
                testCases[CreatureConstants.Shark_Large][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(18, 18, true);
                testCases[CreatureConstants.Shark_Large][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(19, 19, true);
                testCases[CreatureConstants.Shark_Large][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(20, 30, true);
                testCases[CreatureConstants.Shark_Large][AgeConstants.Categories.Maximum] = "20+1d10";
                testCases[CreatureConstants.Shark_Huge][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(14, 17, true);
                testCases[CreatureConstants.Shark_Huge][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(18, 18, true);
                testCases[CreatureConstants.Shark_Huge][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(19, 19, true);
                testCases[CreatureConstants.Shark_Huge][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(20, 30, true);
                testCases[CreatureConstants.Shark_Huge][AgeConstants.Categories.Maximum] = "20+1d10";
                testCases[CreatureConstants.Shark_Dire][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(14, 17, true);
                testCases[CreatureConstants.Shark_Dire][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(18, 18, true);
                testCases[CreatureConstants.Shark_Dire][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(19, 19, true);
                testCases[CreatureConstants.Shark_Dire][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(20, 30, true);
                testCases[CreatureConstants.Shark_Dire][AgeConstants.Categories.Maximum] = "20+1d10";
                testCases[CreatureConstants.ShieldGuardian][AgeConstants.Categories.Construct] = constructAgeRoll;
                testCases[CreatureConstants.ShieldGuardian][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                //INFO: Can't find anything on lifespan. So, making it up
                testCases[CreatureConstants.ShockerLizard][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(5, 34, true);
                testCases[CreatureConstants.ShockerLizard][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(35, 39, true);
                testCases[CreatureConstants.ShockerLizard][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(40, 44, true);
                testCases[CreatureConstants.ShockerLizard][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(45, 55, true);
                testCases[CreatureConstants.ShockerLizard][AgeConstants.Categories.Maximum] = "45+1d10";
                //Source: (Max Age) https://www.worldanvil.com/w/faerun-tatortotzke/a/shrieker-species
                testCases[CreatureConstants.Shrieker][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(10, 499, true);
                testCases[CreatureConstants.Shrieker][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(500, 699, true);
                testCases[CreatureConstants.Shrieker][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(700, 899, true);
                testCases[CreatureConstants.Shrieker][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(900, 1100, true);
                testCases[CreatureConstants.Shrieker][AgeConstants.Categories.Maximum] = "900+2d100";
                //Source: https://www.mojobob.com/roleplay/monstrousmanual/s/skum.html
                testCases[CreatureConstants.Skum][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(3, 14, true);
                testCases[CreatureConstants.Skum][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(15, 20, true);
                testCases[CreatureConstants.Skum][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(21, 26, true);
                testCases[CreatureConstants.Skum][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(27, 33, true);
                testCases[CreatureConstants.Skum][AgeConstants.Categories.Maximum] = "27+1d6";
                testCases[CreatureConstants.Slaad_Blue][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(2, 1_000, true);
                testCases[CreatureConstants.Slaad_Blue][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Slaad_Red][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(2, 1_000, true);
                testCases[CreatureConstants.Slaad_Red][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Slaad_Green][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(2, 100, true);
                testCases[CreatureConstants.Slaad_Green][AgeConstants.Categories.Maximum] = "101";
                testCases[CreatureConstants.Slaad_Gray][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(101, 1_000, true);
                testCases[CreatureConstants.Slaad_Gray][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Slaad_Death][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(101, 1_000, true);
                testCases[CreatureConstants.Slaad_Death][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Snake_Constrictor][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(3, 9, true);
                testCases[CreatureConstants.Snake_Constrictor][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(10, 14, true);
                testCases[CreatureConstants.Snake_Constrictor][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(15, 19, true);
                testCases[CreatureConstants.Snake_Constrictor][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(20, 30, true);
                testCases[CreatureConstants.Snake_Constrictor][AgeConstants.Categories.Maximum] = "20+1d10";
                testCases[CreatureConstants.Snake_Constrictor_Giant][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(3, 9, true);
                testCases[CreatureConstants.Snake_Constrictor_Giant][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(10, 14, true);
                testCases[CreatureConstants.Snake_Constrictor_Giant][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(15, 19, true);
                testCases[CreatureConstants.Snake_Constrictor_Giant][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(20, 30, true);
                testCases[CreatureConstants.Snake_Constrictor_Giant][AgeConstants.Categories.Maximum] = "20+1d10";
                testCases[CreatureConstants.Snake_Viper_Tiny][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(2, 5, true);
                testCases[CreatureConstants.Snake_Viper_Tiny][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(6, 7, true);
                testCases[CreatureConstants.Snake_Viper_Tiny][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(8, 9, true);
                testCases[CreatureConstants.Snake_Viper_Tiny][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(10, 25, true);
                testCases[CreatureConstants.Snake_Viper_Tiny][AgeConstants.Categories.Maximum] = "9+2d8";
                testCases[CreatureConstants.Snake_Viper_Small][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(2, 5, true);
                testCases[CreatureConstants.Snake_Viper_Small][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(6, 7, true);
                testCases[CreatureConstants.Snake_Viper_Small][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(8, 9, true);
                testCases[CreatureConstants.Snake_Viper_Small][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(10, 25, true);
                testCases[CreatureConstants.Snake_Viper_Small][AgeConstants.Categories.Maximum] = "9+2d8";
                testCases[CreatureConstants.Snake_Viper_Medium][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(2, 5, true);
                testCases[CreatureConstants.Snake_Viper_Medium][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(6, 7, true);
                testCases[CreatureConstants.Snake_Viper_Medium][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(8, 9, true);
                testCases[CreatureConstants.Snake_Viper_Medium][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(10, 25, true);
                testCases[CreatureConstants.Snake_Viper_Medium][AgeConstants.Categories.Maximum] = "9+2d8";
                testCases[CreatureConstants.Snake_Viper_Large][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(2, 5, true);
                testCases[CreatureConstants.Snake_Viper_Large][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(6, 7, true);
                testCases[CreatureConstants.Snake_Viper_Large][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(8, 9, true);
                testCases[CreatureConstants.Snake_Viper_Large][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(10, 25, true);
                testCases[CreatureConstants.Snake_Viper_Large][AgeConstants.Categories.Maximum] = "9+2d8";
                testCases[CreatureConstants.Snake_Viper_Huge][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(2, 5, true);
                testCases[CreatureConstants.Snake_Viper_Huge][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(6, 7, true);
                testCases[CreatureConstants.Snake_Viper_Huge][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(8, 9, true);
                testCases[CreatureConstants.Snake_Viper_Huge][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(10, 25, true);
                testCases[CreatureConstants.Snake_Viper_Huge][AgeConstants.Categories.Maximum] = "9+2d8";
                testCases[CreatureConstants.Spectre][AgeConstants.Categories.Undead] = undeadAgeRoll;
                testCases[CreatureConstants.Spectre][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Colossal][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(0, 1, true);
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Colossal][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(2, 2, true);
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Colossal][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(2, 2, true);
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Colossal][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(2, 3, true);
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Colossal][AgeConstants.Categories.Maximum] = "1d2+1";
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Gargantuan][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(0, 1, true);
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Gargantuan][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(2, 2, true);
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Gargantuan][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(2, 2, true);
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Gargantuan][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(2, 3, true);
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Gargantuan][AgeConstants.Categories.Maximum] = "1d2+1";
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Huge][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(0, 1, true);
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Huge][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(2, 2, true);
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Huge][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(2, 2, true);
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Huge][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(2, 3, true);
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Huge][AgeConstants.Categories.Maximum] = "1d2+1";
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Large][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(0, 1, true);
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Large][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(2, 2, true);
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Large][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(2, 2, true);
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Large][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(2, 3, true);
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Large][AgeConstants.Categories.Maximum] = "1d2+1";
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Medium][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(0, 1, true);
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Medium][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(2, 2, true);
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Medium][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(2, 2, true);
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Medium][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(2, 3, true);
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Medium][AgeConstants.Categories.Maximum] = "1d2+1";
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Small][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(0, 1, true);
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Small][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(2, 2, true);
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Small][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(2, 2, true);
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Small][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(2, 3, true);
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Small][AgeConstants.Categories.Maximum] = "1d2+1";
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Tiny][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(0, 1, true);
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Tiny][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(2, 2, true);
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Tiny][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(2, 2, true);
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Tiny][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(2, 3, true);
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Tiny][AgeConstants.Categories.Maximum] = "1d2+1";
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Colossal][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(0, 1, true);
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Colossal][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(2, 2, true);
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Colossal][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(2, 2, true);
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Colossal][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(2, 3, true);
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Colossal][AgeConstants.Categories.Maximum] = "1d2+1";
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Gargantuan][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(0, 1, true);
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Gargantuan][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(2, 2, true);
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Gargantuan][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(2, 2, true);
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Gargantuan][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(2, 3, true);
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Gargantuan][AgeConstants.Categories.Maximum] = "1d2+1";
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Huge][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(0, 1, true);
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Huge][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(2, 2, true);
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Huge][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(2, 2, true);
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Huge][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(2, 3, true);
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Huge][AgeConstants.Categories.Maximum] = "1d2+1";
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Large][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(0, 1, true);
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Large][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(2, 2, true);
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Large][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(2, 2, true);
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Large][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(2, 3, true);
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Large][AgeConstants.Categories.Maximum] = "1d2+1";
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Medium][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(0, 1, true);
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Medium][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(2, 2, true);
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Medium][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(2, 2, true);
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Medium][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(2, 3, true);
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Medium][AgeConstants.Categories.Maximum] = "1d2+1";
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Small][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(0, 1, true);
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Small][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(2, 2, true);
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Small][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(2, 2, true);
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Small][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(2, 3, true);
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Small][AgeConstants.Categories.Maximum] = "1d2+1";
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Tiny][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(0, 1, true);
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Tiny][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(2, 2, true);
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Tiny][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(2, 2, true);
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Tiny][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(2, 3, true);
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Tiny][AgeConstants.Categories.Maximum] = "1d2+1";
                testCases[CreatureConstants.Spider_Swarm][AgeConstants.Categories.Swarm] = RollHelper.GetRollWithMostEvenDistribution(0, 4, true);
                testCases[CreatureConstants.Spider_Swarm][AgeConstants.Categories.Maximum] = RollHelper.GetRollWithFewestDice(0, 4);
                //INFO: Can't find anything on lifespan. So, making it up
                testCases[CreatureConstants.SpiderEater][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(5, 34, true);
                testCases[CreatureConstants.SpiderEater][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(35, 39, true);
                testCases[CreatureConstants.SpiderEater][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(40, 44, true);
                testCases[CreatureConstants.SpiderEater][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(45, 55, true);
                testCases[CreatureConstants.SpiderEater][AgeConstants.Categories.Maximum] = "45+1d10";
                testCases[CreatureConstants.Squid][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(0, 0, true);
                testCases[CreatureConstants.Squid][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(0, 0, true);
                testCases[CreatureConstants.Squid][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(0, 0, true);
                testCases[CreatureConstants.Squid][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(1, 3, true);
                testCases[CreatureConstants.Squid][AgeConstants.Categories.Maximum] = "1d3";
                testCases[CreatureConstants.Squid_Giant][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(0, 0, true);
                testCases[CreatureConstants.Squid_Giant][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(0, 0, true);
                testCases[CreatureConstants.Squid_Giant][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(0, 0, true);
                testCases[CreatureConstants.Squid_Giant][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(1, 3, true);
                testCases[CreatureConstants.Squid_Giant][AgeConstants.Categories.Maximum] = "1d3";
                testCases[CreatureConstants.StagBeetle_Giant][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(0, 0, true);
                testCases[CreatureConstants.StagBeetle_Giant][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(0, 0, true);
                testCases[CreatureConstants.StagBeetle_Giant][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(0, 0, true);
                testCases[CreatureConstants.StagBeetle_Giant][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(1, 2, true);
                testCases[CreatureConstants.StagBeetle_Giant][AgeConstants.Categories.Maximum] = "1d2";
                //Source: (Max Age) https://www.worldanvil.com/w/faerun-tatortotzke/a/stirge-species
                testCases[CreatureConstants.Stirge][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(1, 5, true);
                testCases[CreatureConstants.Stirge][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(6, 7, true);
                testCases[CreatureConstants.Stirge][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(8, 9, true);
                testCases[CreatureConstants.Stirge][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(10, 20, true);
                testCases[CreatureConstants.Stirge][AgeConstants.Categories.Maximum] = "10+1d10";
                testCases[CreatureConstants.Succubus][AgeConstants.Categories.Adulthood] = outsiderAgeRoll;
                testCases[CreatureConstants.Succubus][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Tarrasque][AgeConstants.Categories.Adulthood] = $"{oneMRoll}+1000000";
                testCases[CreatureConstants.Tarrasque][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                //INFO: Can't find anything on lifespan. So, making it up
                testCases[CreatureConstants.Tendriculos][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(5, 34, true);
                testCases[CreatureConstants.Tendriculos][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(35, 39, true);
                testCases[CreatureConstants.Tendriculos][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(40, 44, true);
                testCases[CreatureConstants.Tendriculos][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(45, 55, true);
                testCases[CreatureConstants.Tendriculos][AgeConstants.Categories.Maximum] = "45+1d10";
                //INFO: Can't find anything on lifespan. So, making it up
                testCases[CreatureConstants.Thoqqua][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(5, 34, true);
                testCases[CreatureConstants.Thoqqua][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(35, 39, true);
                testCases[CreatureConstants.Thoqqua][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(40, 44, true);
                testCases[CreatureConstants.Thoqqua][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(45, 55, true);
                testCases[CreatureConstants.Thoqqua][AgeConstants.Categories.Maximum] = "45+1d10";
                //Source: http://people.wku.edu/charles.plemons/ad&d/races/age.html
                testCases[CreatureConstants.Tiefling][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(17, 49, true);
                testCases[CreatureConstants.Tiefling][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(50, 66, true);
                testCases[CreatureConstants.Tiefling][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(67, 99, true);
                testCases[CreatureConstants.Tiefling][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(100, 200, true);
                testCases[CreatureConstants.Tiefling][AgeConstants.Categories.Maximum] = "100+1d100";
                testCases[CreatureConstants.Tiger][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(2, 5, true);
                testCases[CreatureConstants.Tiger][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(6, 6, true);
                testCases[CreatureConstants.Tiger][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(7, 7, true);
                testCases[CreatureConstants.Tiger][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(8, 10, true);
                testCases[CreatureConstants.Tiger][AgeConstants.Categories.Maximum] = "7+1d3";
                testCases[CreatureConstants.Tiger_Dire][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(2, 5, true);
                testCases[CreatureConstants.Tiger_Dire][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(6, 6, true);
                testCases[CreatureConstants.Tiger_Dire][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(7, 7, true);
                testCases[CreatureConstants.Tiger_Dire][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(8, 10, true);
                testCases[CreatureConstants.Tiger_Dire][AgeConstants.Categories.Maximum] = "7+1d3";
                testCases[CreatureConstants.Titan][AgeConstants.Categories.Adulthood] = outsiderAgeRoll;
                testCases[CreatureConstants.Titan][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Toad][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(2, 5, true);
                testCases[CreatureConstants.Toad][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(6, 7, true);
                testCases[CreatureConstants.Toad][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(8, 9, true);
                testCases[CreatureConstants.Toad][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(10, 12, true);
                testCases[CreatureConstants.Toad][AgeConstants.Categories.Maximum] = "10+1d2";
                testCases[CreatureConstants.Tojanida_Adult][AgeConstants.Categories.Tojanida.Adult] = RollHelper.GetRollWithMostEvenDistribution(26, 80, true);
                testCases[CreatureConstants.Tojanida_Adult][AgeConstants.Categories.Maximum] = "81";
                testCases[CreatureConstants.Tojanida_Elder][AgeConstants.Categories.Tojanida.Elder] = RollHelper.GetRollWithMostEvenDistribution(81, 150, true);
                testCases[CreatureConstants.Tojanida_Elder][AgeConstants.Categories.Maximum] = "150";
                testCases[CreatureConstants.Tojanida_Juvenile][AgeConstants.Categories.Tojanida.Juvenile] = RollHelper.GetRollWithMostEvenDistribution(1, 25, true);
                testCases[CreatureConstants.Tojanida_Juvenile][AgeConstants.Categories.Maximum] = "26";
                //INFO: Sort of making it up
                testCases[CreatureConstants.Treant][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(100, 399, true);
                testCases[CreatureConstants.Treant][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(400, 699, true);
                testCases[CreatureConstants.Treant][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(700, 999, true);
                testCases[CreatureConstants.Treant][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(1000, 10_000, true);
                testCases[CreatureConstants.Treant][AgeConstants.Categories.Maximum] = "1000+100d100";
                //Source: https://jurassic-park-ecology.fandom.com/wiki/Triceratops
                testCases[CreatureConstants.Triceratops][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(6, 19, true);
                testCases[CreatureConstants.Triceratops][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(20, 27, true);
                testCases[CreatureConstants.Triceratops][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(28, 35, true);
                testCases[CreatureConstants.Triceratops][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(36, 44, true);
                testCases[CreatureConstants.Triceratops][AgeConstants.Categories.Maximum] = "36+2d4";
                //Source: http://dnd5e.wikidot.com/triton
                testCases[CreatureConstants.Triton][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(15, 99, true);
                testCases[CreatureConstants.Triton][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(100, 139, true);
                testCases[CreatureConstants.Triton][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(140, 179, true);
                testCases[CreatureConstants.Triton][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(180, 220, true);
                testCases[CreatureConstants.Triton][AgeConstants.Categories.Maximum] = "180+2d20";
                //Source: https://legacy.aonprd.com/monsterCodex/troglodytes.html
                testCases[CreatureConstants.Troglodyte][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(10, 29, true);
                testCases[CreatureConstants.Troglodyte][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(30, 39, true);
                testCases[CreatureConstants.Troglodyte][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(40, 49, true);
                testCases[CreatureConstants.Troglodyte][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(50, 60, true);
                testCases[CreatureConstants.Troglodyte][AgeConstants.Categories.Maximum] = "50+1d10";
                testCases[CreatureConstants.Troll][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(10, 49, true);
                testCases[CreatureConstants.Troll][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(50, 64, true);
                testCases[CreatureConstants.Troll][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(65, 89, true);
                testCases[CreatureConstants.Troll][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(90, 110, true);
                testCases[CreatureConstants.Troll][AgeConstants.Categories.Maximum] = "90+2d10";
                testCases[CreatureConstants.Troll_Scrag][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(10, 49, true);
                testCases[CreatureConstants.Troll_Scrag][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(50, 64, true);
                testCases[CreatureConstants.Troll_Scrag][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(65, 89, true);
                testCases[CreatureConstants.Troll_Scrag][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(90, 110, true);
                testCases[CreatureConstants.Troll_Scrag][AgeConstants.Categories.Maximum] = "90+2d10";
                testCases[CreatureConstants.TrumpetArchon][AgeConstants.Categories.Adulthood] = outsiderAgeRoll;
                testCases[CreatureConstants.TrumpetArchon][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                //Source: https://jurassic-park-ecology.fandom.com/wiki/Tyrannosaurus_rex
                testCases[CreatureConstants.Tyrannosaurus][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(6, 23, true);
                testCases[CreatureConstants.Tyrannosaurus][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(24, 31, true);
                testCases[CreatureConstants.Tyrannosaurus][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(32, 39, true);
                testCases[CreatureConstants.Tyrannosaurus][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(40, 44, true);
                testCases[CreatureConstants.Tyrannosaurus][AgeConstants.Categories.Maximum] = "40+1d4";
                //Source: https://forgottenrealms.fandom.com/wiki/Umber_hulk#Life_cycle
                testCases[CreatureConstants.UmberHulk][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(2, 34, true);
                testCases[CreatureConstants.UmberHulk][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(35, 39, true);
                testCases[CreatureConstants.UmberHulk][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(40, 44, true);
                testCases[CreatureConstants.UmberHulk][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(45, 85, true);
                testCases[CreatureConstants.UmberHulk][AgeConstants.Categories.Maximum] = "45+2d20";
                //Source: https://forgottenrealms.fandom.com/wiki/Umber_hulk#Life_cycle
                testCases[CreatureConstants.UmberHulk_TrulyHorrid][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(2, 34, true);
                testCases[CreatureConstants.UmberHulk_TrulyHorrid][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(35, 39, true);
                testCases[CreatureConstants.UmberHulk_TrulyHorrid][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(40, 44, true);
                testCases[CreatureConstants.UmberHulk_TrulyHorrid][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(45, 85, true);
                testCases[CreatureConstants.UmberHulk_TrulyHorrid][AgeConstants.Categories.Maximum] = "45+2d20";
                //Source: https://adnd2e.fandom.com/wiki/Unicorn
                testCases[CreatureConstants.Unicorn][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(5, 1100, true);
                testCases[CreatureConstants.Unicorn][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(1000, 1100, true);
                testCases[CreatureConstants.Unicorn][AgeConstants.Categories.Maximum] = "1000";
                testCases[CreatureConstants.VampireSpawn][AgeConstants.Categories.Undead] = undeadAgeRoll;
                testCases[CreatureConstants.VampireSpawn][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Vargouille][AgeConstants.Categories.Adulthood] = outsiderAgeRoll;
                testCases[CreatureConstants.Vargouille][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                //Source: (Max Age) https://www.worldanvil.com/w/faerun-tatortotzke/a/violet-fungus-species
                testCases[CreatureConstants.VioletFungus][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(10, 499, true);
                testCases[CreatureConstants.VioletFungus][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(500, 699, true);
                testCases[CreatureConstants.VioletFungus][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(700, 899, true);
                testCases[CreatureConstants.VioletFungus][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(900, 1100, true);
                testCases[CreatureConstants.VioletFungus][AgeConstants.Categories.Maximum] = "900+2d100";
                testCases[CreatureConstants.Vrock][AgeConstants.Categories.Adulthood] = outsiderAgeRoll;
                testCases[CreatureConstants.Vrock][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Wasp_Giant][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(0, 0, true);
                testCases[CreatureConstants.Wasp_Giant][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(0, 0, true);
                testCases[CreatureConstants.Wasp_Giant][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(0, 0, true);
                testCases[CreatureConstants.Wasp_Giant][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(0, 1, true);
                testCases[CreatureConstants.Wasp_Giant][AgeConstants.Categories.Maximum] = "1d2-1";
                testCases[CreatureConstants.Weasel][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(1, 4, true);
                testCases[CreatureConstants.Weasel][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(5, 5, true);
                testCases[CreatureConstants.Weasel][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(6, 6, true);
                testCases[CreatureConstants.Weasel][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(7, 10, true);
                testCases[CreatureConstants.Weasel][AgeConstants.Categories.Maximum] = "6+1d4";
                testCases[CreatureConstants.Weasel_Dire][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(1, 4, true);
                testCases[CreatureConstants.Weasel_Dire][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(5, 5, true);
                testCases[CreatureConstants.Weasel_Dire][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(6, 6, true);
                testCases[CreatureConstants.Weasel_Dire][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(7, 10, true);
                testCases[CreatureConstants.Weasel_Dire][AgeConstants.Categories.Maximum] = "6+1d4";
                testCases[CreatureConstants.Whale_Baleen][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(5, 25, true);
                testCases[CreatureConstants.Whale_Baleen][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(36, 40, true);
                testCases[CreatureConstants.Whale_Baleen][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(41, 44, true);
                testCases[CreatureConstants.Whale_Baleen][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(45, 50, true);
                testCases[CreatureConstants.Whale_Baleen][AgeConstants.Categories.Maximum] = "44+1d6";
                testCases[CreatureConstants.Whale_Cachalot][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(14, 50, true);
                testCases[CreatureConstants.Whale_Cachalot][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(51, 60, true);
                testCases[CreatureConstants.Whale_Cachalot][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(61, 64, true);
                testCases[CreatureConstants.Whale_Cachalot][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(65, 75, true);
                testCases[CreatureConstants.Whale_Cachalot][AgeConstants.Categories.Maximum] = "65+1d10";
                testCases[CreatureConstants.Whale_Orca][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(15, 30, true);
                testCases[CreatureConstants.Whale_Orca][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(31, 35, true);
                testCases[CreatureConstants.Whale_Orca][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(36, 39, true);
                testCases[CreatureConstants.Whale_Orca][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(40, 70, true);
                testCases[CreatureConstants.Whale_Orca][AgeConstants.Categories.Maximum] = "40+3d10";
                testCases[CreatureConstants.Wight][AgeConstants.Categories.Undead] = undeadAgeRoll;
                testCases[CreatureConstants.Wight][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.WillOWisp][AgeConstants.Categories.Adulthood] = tenKRoll;
                testCases[CreatureConstants.WillOWisp][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                //Source: https://pathfinderwiki.com/wiki/Winter_wolf
                testCases[CreatureConstants.WinterWolf][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(3, 25, true);
                testCases[CreatureConstants.WinterWolf][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(26, 35, true);
                testCases[CreatureConstants.WinterWolf][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(36, 44, true);
                testCases[CreatureConstants.WinterWolf][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(45, 55, true);
                testCases[CreatureConstants.WinterWolf][AgeConstants.Categories.Maximum] = "45+1d10";
                testCases[CreatureConstants.Wolf][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(3, 5, true);
                testCases[CreatureConstants.Wolf][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(6, 8, true);
                testCases[CreatureConstants.Wolf][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(9, 11, true);
                testCases[CreatureConstants.Wolf][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(12, 14, true);
                testCases[CreatureConstants.Wolf][AgeConstants.Categories.Maximum] = "12+1d2";
                testCases[CreatureConstants.Wolf_Dire][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(3, 5, true);
                testCases[CreatureConstants.Wolf_Dire][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(6, 8, true);
                testCases[CreatureConstants.Wolf_Dire][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(9, 11, true);
                testCases[CreatureConstants.Wolf_Dire][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(12, 14, true);
                testCases[CreatureConstants.Wolf_Dire][AgeConstants.Categories.Maximum] = "12+1d2";
                testCases[CreatureConstants.Wolverine][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(1, 5, true);
                testCases[CreatureConstants.Wolverine][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(6, 6, true);
                testCases[CreatureConstants.Wolverine][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(7, 7, true);
                testCases[CreatureConstants.Wolverine][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(8, 10, true);
                testCases[CreatureConstants.Wolverine][AgeConstants.Categories.Maximum] = "7+1d3";
                testCases[CreatureConstants.Wolverine_Dire][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(1, 5, true);
                testCases[CreatureConstants.Wolverine_Dire][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(6, 6, true);
                testCases[CreatureConstants.Wolverine_Dire][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(7, 7, true);
                testCases[CreatureConstants.Wolverine_Dire][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(8, 10, true);
                testCases[CreatureConstants.Wolverine_Dire][AgeConstants.Categories.Maximum] = "7+1d3";
                //Source: https://www.reddit.com/r/dndnext/comments/3vxt0g/playable_worgs/
                testCases[CreatureConstants.Worg][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(4, 20, true);
                testCases[CreatureConstants.Worg][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(21, 27, true);
                testCases[CreatureConstants.Worg][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(28, 34, true);
                testCases[CreatureConstants.Worg][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(35, 55, true);
                testCases[CreatureConstants.Worg][AgeConstants.Categories.Maximum] = "35+2d10";
                testCases[CreatureConstants.Wraith][AgeConstants.Categories.Undead] = RollHelper.GetRollWithMostEvenDistribution(1, 1000, true);
                testCases[CreatureConstants.Wraith][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Wraith_Dread][AgeConstants.Categories.Undead] = $"{undeadAgeRoll}+1000";
                testCases[CreatureConstants.Wraith_Dread][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                //Source: (Max) https://www.worldanvil.com/w/faerun-tatortotzke/a/wyvern-article
                testCases[CreatureConstants.Wyvern][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(10, 74, true);
                testCases[CreatureConstants.Wyvern][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(75, 99, true);
                testCases[CreatureConstants.Wyvern][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(100, 134, true);
                testCases[CreatureConstants.Wyvern][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(135, 165, true);
                testCases[CreatureConstants.Wyvern][AgeConstants.Categories.Maximum] = "135+3d10";
                testCases[CreatureConstants.Xill][AgeConstants.Categories.Adulthood] = outsiderAgeRoll;
                testCases[CreatureConstants.Xill][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Xorn_Minor][AgeConstants.Categories.Xorn.Minor] = RollHelper.GetRollWithMostEvenDistribution(1, 333, true);
                testCases[CreatureConstants.Xorn_Minor][AgeConstants.Categories.Maximum] = "334";
                testCases[CreatureConstants.Xorn_Average][AgeConstants.Categories.Xorn.Average] = RollHelper.GetRollWithMostEvenDistribution(334, 666, true);
                testCases[CreatureConstants.Xorn_Average][AgeConstants.Categories.Maximum] = "667";
                testCases[CreatureConstants.Xorn_Elder][AgeConstants.Categories.Xorn.Elder] = RollHelper.GetRollWithMostEvenDistribution(667, 1000, true);
                testCases[CreatureConstants.Xorn_Elder][AgeConstants.Categories.Maximum] = "900+1d100";
                testCases[CreatureConstants.YethHound][AgeConstants.Categories.Adulthood] = outsiderAgeRoll;
                testCases[CreatureConstants.YethHound][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                //INFO: Can't find anything on lifespan. So, making it up
                testCases[CreatureConstants.Yrthak][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(5, 34, true);
                testCases[CreatureConstants.Yrthak][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(35, 39, true);
                testCases[CreatureConstants.Yrthak][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(40, 44, true);
                testCases[CreatureConstants.Yrthak][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(45, 55, true);
                testCases[CreatureConstants.Yrthak][AgeConstants.Categories.Maximum] = "45+1d10";
                //Source: https://forgottenrealms.fandom.com/wiki/Yuan-ti_pureblood
                testCases[CreatureConstants.YuanTi_Pureblood][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(12, 39, true);
                testCases[CreatureConstants.YuanTi_Pureblood][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(40, 59, true);
                testCases[CreatureConstants.YuanTi_Pureblood][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(60, 79, true);
                testCases[CreatureConstants.YuanTi_Pureblood][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(80, 120, true);
                testCases[CreatureConstants.YuanTi_Pureblood][AgeConstants.Categories.Maximum] = "80+2d20";
                //Source: https://forgottenrealms.fandom.com/wiki/Yuan-ti_malison
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeArms][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(12, 39, true);
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeArms][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(40, 59, true);
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeArms][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(60, 79, true);
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeArms][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(80, 120, true);
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeArms][AgeConstants.Categories.Maximum] = "80+2d20";
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeHead][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(12, 39, true);
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeHead][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(40, 59, true);
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeHead][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(60, 79, true);
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeHead][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(80, 120, true);
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeHead][AgeConstants.Categories.Maximum] = "80+2d20";
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeTail][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(12, 39, true);
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeTail][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(40, 59, true);
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeTail][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(60, 79, true);
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeTail][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(80, 120, true);
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeTail][AgeConstants.Categories.Maximum] = "80+2d20";
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(12, 39, true);
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(40, 59, true);
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(60, 79, true);
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(80, 120, true);
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs][AgeConstants.Categories.Maximum] = "80+2d20";
                testCases[CreatureConstants.YuanTi_Abomination][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(12, 39, true);
                testCases[CreatureConstants.YuanTi_Abomination][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(40, 59, true);
                testCases[CreatureConstants.YuanTi_Abomination][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(60, 79, true);
                testCases[CreatureConstants.YuanTi_Abomination][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(80, 120, true);
                testCases[CreatureConstants.YuanTi_Abomination][AgeConstants.Categories.Maximum] = "80+2d20";
                testCases[CreatureConstants.Zelekhut][AgeConstants.Categories.Construct] = outsiderAgeRoll;
                testCases[CreatureConstants.Zelekhut][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();

                foreach (var testCase in testCases)
                {
                    yield return new TestCaseData(testCase.Key, testCase.Value);
                }
            }
        }

        private static string GetAdulthoodRollFromAverage(int lifespan, int adulthood = -1) => GetAdulthoodRoll(lifespan * 4 / 5, adulthood);
        private static string GetAdulthoodRollFromUpTo(int lifespan, int adulthood = -1) => GetAdulthoodRoll(lifespan * 2 / 3, adulthood);

        private static string GetAdulthoodRoll(int venerable, int adulthood = -1)
        {
            if (adulthood == -1)
                adulthood = venerable / 5;

            var middleAge = venerable / 2;
            return GetRoll(adulthood, middleAge - 1);
        }

        private static string GetMiddleAgeRollFromAverage(int lifespan) => GetMiddleAgeRoll(lifespan * 4 / 5);
        private static string GetMiddleAgeRollFromUpTo(int lifespan) => GetMiddleAgeRoll(lifespan * 2 / 3);

        private static string GetMiddleAgeRoll(int venerable, int old = -1)
        {
            if (old == -1)
                old = venerable * 3 / 4;

            var middleAge = venerable / 2;

            if (old - 1 < middleAge)
                return (old - 1).ToString();

            return GetRoll(middleAge, old - 1);
        }

        private static string GetOldRollFromAverage(int lifespan) => GetOldRoll(lifespan * 4 / 5);
        private static string GetOldRollFromUpTo(int lifespan) => GetOldRoll(lifespan * 2 / 3);

        private static string GetOldRoll(int venerable, int old = -1)
        {
            if (old == -1)
                old = venerable * 3 / 4;

            return GetRoll(old, venerable - 1);
        }

        private static string GetVenerableRollFromAverage(int lifespan) => GetVenerableRoll(lifespan * 4 / 5, lifespan * 6 / 5);
        private static string GetVenerableRollFromUpTo(int lifespan) => GetVenerableRoll(lifespan * 2 / 3, lifespan);

        private static string GetVenerableRoll(int venerable, int max = -1)
        {
            if (max == -1)
                max = venerable * 3 / 2;

            return GetRoll(venerable, max);
        }

        private static string GetRoll(int lower, int upper) => RollHelper.GetRollWithMostEvenDistribution(lower, upper, true);

        private static string GetMaximumRollFromAverage(int lifespan) => GetMaximumRoll(lifespan * 4 / 5, lifespan * 6 / 5);
        private static string GetMaximumRollFromUpTo(int lifespan) => GetMaximumRoll(lifespan * 2 / 3, lifespan);

        private static string GetMaximumRoll(int venerable, int max = -1)
        {
            if (max == -1)
                max = venerable * 3 / 2;

            var min = GetMinForMaximumRoll(venerable, max);
            return RollHelper.GetRollWithFewestDice(min, max);
        }

        private static int GetMinForMaximumRoll(int venerable, int max)
        {
            //We allow for non-strict ranges, since for elves (as an example), age range could be 350-750, written as 350+4d100, so actual range is 354-750
            //Therefore, we should allow for some variability in the age range
            //Ignoring d2, since normal roll assessment would produce that anyways, so it wouldn't be a shortcut
            var dice = new[] { 100, 20, 12, 10, 8, 6, 4, 3 };
            var range = max - venerable + 1;

            foreach (var die in dice.Where(d => d <= range))
            {
                if (range % die == 0)
                    return venerable;

                var q = range / die;

                if (venerable + die * q == max)
                    return venerable + q;
            }

            return venerable;
        }

        [TestCase(9, 16, "1d8+8")]
        [TestCase(35, 45, "1d10+35")]
        [TestCase(40, 60, "1d20+40")]
        [TestCase(40, 70, "3d10+40")]
        [TestCase(40, 80, "2d20+40")]
        [TestCase(40, 90, "5d10+40")]
        [TestCase(40, 100, "3d20+40")]
        [TestCase(50, 70, "1d20+50")]
        [TestCase(50, 74, "2d12+50")]
        [TestCase(60, 80, "1d20+60")]
        [TestCase(65, 85, "1d20+65")]
        [TestCase(70, 110, "2d20+70")]
        [TestCase(80, 120, "2d20+80")]
        [TestCase(90, 130, "2d20+90")]
        [TestCase(95, 135, "2d20+95")]
        [TestCase(100, 200, "1d100+100")]
        [TestCase(101, 200, "1d100+100")]
        [TestCase(110, 130, "1d20+110")]
        [TestCase(125, 165, "2d20+125")]
        [TestCase(125, 185, "3d20+125")]
        [TestCase(150, 190, "2d20+150")]
        [TestCase(150, 250, "1d100+150")]
        [TestCase(175, 215, "2d20+175")]
        [TestCase(200, 300, "1d100+200")]
        [TestCase(200, 400, "2d100+200")]
        [TestCase(200, 500, "3d100+200")]
        [TestCase(250, 350, "1d100+250")]
        [TestCase(250, 450, "2d100+250")]
        [TestCase(350, 750, "4d100+350")]
        [TestCase(400, 800, "4d100+400")]
        [TestCase(450, 650, "2d100+450")]
        [TestCase(700, 1000, "3d100+700")]
        public void GetMaximumRoll_HasCorrectVariation(int venerable, int max, string expectedRoll)
        {
            var maxRoll = GetMaximumRoll(venerable, max);
            Assert.That(dice.IsValid(maxRoll), Is.True);
            Assert.That(dice.Roll(maxRoll).AsPotentialMaximum(), Is.EqualTo(max));
            Assert.That(dice.Roll(maxRoll).AsPotentialMinimum(), Is.AtLeast(venerable));
            Assert.That(maxRoll, Is.EqualTo(expectedRoll));
        }
    }
}