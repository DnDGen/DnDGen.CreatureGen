using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Tables;
using DnDGen.RollGen;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;

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
                testCases[CreatureConstants.Ankheg][AgeConstants.Categories.Adulthood] = GetAdulthoodRoll(20, 2);
                testCases[CreatureConstants.Ankheg][AgeConstants.Categories.MiddleAge] = GetMiddleAgeRoll(20);
                testCases[CreatureConstants.Ankheg][AgeConstants.Categories.Old] = GetOldRoll(20);
                testCases[CreatureConstants.Ankheg][AgeConstants.Categories.Venerable] = GetVenerableRoll(20);
                testCases[CreatureConstants.Ankheg][AgeConstants.Categories.Maximum] = GetMaximumRoll(20);
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
                testCases[CreatureConstants.Athach][AgeConstants.Categories.Adulthood] = GetRoll(36, 79);
                testCases[CreatureConstants.Athach][AgeConstants.Categories.MiddleAge] = GetRoll(80, 119);
                testCases[CreatureConstants.Athach][AgeConstants.Categories.Old] = GetRoll(120, 159);
                testCases[CreatureConstants.Athach][AgeConstants.Categories.Venerable] = GetRoll(160, 200);
                testCases[CreatureConstants.Athach][AgeConstants.Categories.Maximum] = "160+2d20";
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
                testCases[CreatureConstants.Bralani][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(90, 224, true);
                testCases[CreatureConstants.Bralani][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(225, 337, true);
                testCases[CreatureConstants.Bralani][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(338, 449, true);
                testCases[CreatureConstants.Bralani][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(450, 650, true);
                testCases[CreatureConstants.Bralani][AgeConstants.Categories.Maximum] = "450+2d100";
                //Source: http://people.wku.edu/charles.plemons/ad&d/races/age.html
                testCases[CreatureConstants.Bugbear][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(10, 32, true);
                testCases[CreatureConstants.Bugbear][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(33, 43, true);
                testCases[CreatureConstants.Bugbear][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(44, 64, true);
                testCases[CreatureConstants.Bugbear][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(65, 85, true);
                testCases[CreatureConstants.Bugbear][AgeConstants.Categories.Maximum] = "65+2d10";
                //Source: unknown
                testCases[CreatureConstants.Bulette][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(1, 2, true);
                testCases[CreatureConstants.Bulette][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(3, 3, true);
                testCases[CreatureConstants.Bulette][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(4, 4, true);
                testCases[CreatureConstants.Bulette][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(5, 5, true);
                testCases[CreatureConstants.Bulette][AgeConstants.Categories.Maximum] = RollHelper.GetRollWithFewestDice(5, 5);
                //Source: https://www.dimensions.com/element/bactrian-camel (maximum)
                //https://www.pbs.org/wnet/nature/blog/camel-fact-sheet/ (adulthood)
                testCases[CreatureConstants.Camel_Bactrian][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(7, 18, true);
                testCases[CreatureConstants.Camel_Bactrian][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(19, 19, true);
                testCases[CreatureConstants.Camel_Bactrian][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(20, 40, true);
                testCases[CreatureConstants.Camel_Bactrian][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(41, 50, true);
                testCases[CreatureConstants.Camel_Bactrian][AgeConstants.Categories.Maximum] = "40+1d10";
                //Source: https://www.dimensions.com/element/dromedary-camel (maximum)
                //https://www.pbs.org/wnet/nature/blog/camel-fact-sheet/ (adulthood)
                testCases[CreatureConstants.Camel_Dromedary][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(7, 19, true);
                testCases[CreatureConstants.Camel_Dromedary][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(20, 39, true);
                testCases[CreatureConstants.Camel_Dromedary][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(30, 39, true);
                testCases[CreatureConstants.Camel_Dromedary][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(40, 50, true);
                testCases[CreatureConstants.Camel_Dromedary][AgeConstants.Categories.Maximum] = "40+1d10";
                //Source: https://www.worldanvil.com/w/faerun-tatortotzke/a/carrion-crawler-species
                testCases[CreatureConstants.CarrionCrawler][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(4, 9, true);
                testCases[CreatureConstants.CarrionCrawler][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(10, 14, true);
                testCases[CreatureConstants.CarrionCrawler][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(15, 19, true);
                testCases[CreatureConstants.CarrionCrawler][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(20, 30, true);
                testCases[CreatureConstants.CarrionCrawler][AgeConstants.Categories.Maximum] = "20+1d10";
                //Source: https://www.dimensions.com/element/american-shorthair-cat (maximum)
                //https://www.purina.com/cats/cat-breeds/american-shorthair (adulthood)
                testCases[CreatureConstants.Cat][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(3, 7, true);
                testCases[CreatureConstants.Cat][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(8, 11, true);
                testCases[CreatureConstants.Cat][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(12, 14, true);
                testCases[CreatureConstants.Cat][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(15, 20, true);
                testCases[CreatureConstants.Cat][AgeConstants.Categories.Maximum] = "14+1d6";
                //Source: http://people.wku.edu/charles.plemons/ad&d/races/age.html
                testCases[CreatureConstants.Centaur][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(18, 36, true);
                testCases[CreatureConstants.Centaur][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(37, 49, true);
                testCases[CreatureConstants.Centaur][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(50, 74, true);
                testCases[CreatureConstants.Centaur][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(75, 115, true);
                testCases[CreatureConstants.Centaur][AgeConstants.Categories.Maximum] = "75+2d20";
                //Source: https://www.dimensions.com/element/tiger-centipede-scolopendra-polymorpha
                testCases[CreatureConstants.Centipede_Monstrous_Colossal][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(0, 1, true);
                testCases[CreatureConstants.Centipede_Monstrous_Colossal][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(2, 2, true);
                testCases[CreatureConstants.Centipede_Monstrous_Colossal][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(3, 3, true);
                testCases[CreatureConstants.Centipede_Monstrous_Colossal][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(4, 6, true);
                testCases[CreatureConstants.Centipede_Monstrous_Colossal][AgeConstants.Categories.Maximum] = "4+1d2";
                testCases[CreatureConstants.Centipede_Monstrous_Gargantuan][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(0, 1, true);
                testCases[CreatureConstants.Centipede_Monstrous_Gargantuan][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(2, 2, true);
                testCases[CreatureConstants.Centipede_Monstrous_Gargantuan][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(3, 3, true);
                testCases[CreatureConstants.Centipede_Monstrous_Gargantuan][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(4, 6, true);
                testCases[CreatureConstants.Centipede_Monstrous_Gargantuan][AgeConstants.Categories.Maximum] = "4+1d2";
                testCases[CreatureConstants.Centipede_Monstrous_Huge][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(0, 1, true);
                testCases[CreatureConstants.Centipede_Monstrous_Huge][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(2, 2, true);
                testCases[CreatureConstants.Centipede_Monstrous_Huge][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(3, 3, true);
                testCases[CreatureConstants.Centipede_Monstrous_Huge][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(4, 6, true);
                testCases[CreatureConstants.Centipede_Monstrous_Huge][AgeConstants.Categories.Maximum] = "4+1d2";
                testCases[CreatureConstants.Centipede_Monstrous_Large][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(0, 1, true);
                testCases[CreatureConstants.Centipede_Monstrous_Large][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(2, 2, true);
                testCases[CreatureConstants.Centipede_Monstrous_Large][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(3, 3, true);
                testCases[CreatureConstants.Centipede_Monstrous_Large][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(4, 6, true);
                testCases[CreatureConstants.Centipede_Monstrous_Large][AgeConstants.Categories.Maximum] = "4+1d2";
                testCases[CreatureConstants.Centipede_Monstrous_Medium][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(0, 1, true);
                testCases[CreatureConstants.Centipede_Monstrous_Medium][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(2, 2, true);
                testCases[CreatureConstants.Centipede_Monstrous_Medium][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(3, 3, true);
                testCases[CreatureConstants.Centipede_Monstrous_Medium][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(4, 6, true);
                testCases[CreatureConstants.Centipede_Monstrous_Medium][AgeConstants.Categories.Maximum] = "4+1d2";
                testCases[CreatureConstants.Centipede_Monstrous_Small][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(0, 1, true);
                testCases[CreatureConstants.Centipede_Monstrous_Small][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(2, 2, true);
                testCases[CreatureConstants.Centipede_Monstrous_Small][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(3, 3, true);
                testCases[CreatureConstants.Centipede_Monstrous_Small][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(4, 6, true);
                testCases[CreatureConstants.Centipede_Monstrous_Small][AgeConstants.Categories.Maximum] = "4+1d2";
                testCases[CreatureConstants.Centipede_Monstrous_Tiny][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(0, 1, true);
                testCases[CreatureConstants.Centipede_Monstrous_Tiny][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(2, 2, true);
                testCases[CreatureConstants.Centipede_Monstrous_Tiny][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(3, 3, true);
                testCases[CreatureConstants.Centipede_Monstrous_Tiny][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(4, 6, true);
                testCases[CreatureConstants.Centipede_Monstrous_Tiny][AgeConstants.Categories.Maximum] = "4+1d2";
                testCases[CreatureConstants.Centipede_Swarm][AgeConstants.Categories.Swarm] = RollHelper.GetRollWithMostEvenDistribution(0, 8, true);
                testCases[CreatureConstants.Centipede_Swarm][AgeConstants.Categories.Maximum] = RollHelper.GetRollWithFewestDice(0, 8);
                testCases[CreatureConstants.ChainDevil_Kyton][AgeConstants.Categories.Adulthood] = outsiderAgeRoll;
                testCases[CreatureConstants.ChainDevil_Kyton][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                //Source unknown
                testCases[CreatureConstants.ChaosBeast][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(0, 100, true);
                testCases[CreatureConstants.ChaosBeast][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                //Source: https://www.dimensions.com/element/cheetahs (maximum)
                //https://ielc.libguides.com/sdzg/factsheets/cheetah/reproduction (adulthood)
                testCases[CreatureConstants.Cheetah][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(3, 4, true);
                testCases[CreatureConstants.Cheetah][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(5, 7, true);
                testCases[CreatureConstants.Cheetah][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(8, 9, true);
                testCases[CreatureConstants.Cheetah][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(10, 12, true);
                testCases[CreatureConstants.Cheetah][AgeConstants.Categories.Maximum] = "10+1d2";
                //Source: https://www.worldanvil.com/w/faerun-tatortotzke/a/chimera-species
                testCases[CreatureConstants.Chimera_Black][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(12, 29, true);
                testCases[CreatureConstants.Chimera_Black][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(30, 44, true);
                testCases[CreatureConstants.Chimera_Black][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(45, 59, true);
                testCases[CreatureConstants.Chimera_Black][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(60, 90, true);
                testCases[CreatureConstants.Chimera_Black][AgeConstants.Categories.Maximum] = "60+3d10";
                testCases[CreatureConstants.Chimera_Blue][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(12, 29, true);
                testCases[CreatureConstants.Chimera_Blue][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(30, 44, true);
                testCases[CreatureConstants.Chimera_Blue][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(45, 59, true);
                testCases[CreatureConstants.Chimera_Blue][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(60, 90, true);
                testCases[CreatureConstants.Chimera_Blue][AgeConstants.Categories.Maximum] = "60+3d10";
                testCases[CreatureConstants.Chimera_Green][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(12, 29, true);
                testCases[CreatureConstants.Chimera_Green][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(30, 44, true);
                testCases[CreatureConstants.Chimera_Green][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(45, 59, true);
                testCases[CreatureConstants.Chimera_Green][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(60, 90, true);
                testCases[CreatureConstants.Chimera_Green][AgeConstants.Categories.Maximum] = "60+3d10";
                testCases[CreatureConstants.Chimera_Red][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(12, 29, true);
                testCases[CreatureConstants.Chimera_Red][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(30, 44, true);
                testCases[CreatureConstants.Chimera_Red][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(45, 59, true);
                testCases[CreatureConstants.Chimera_Red][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(60, 90, true);
                testCases[CreatureConstants.Chimera_Red][AgeConstants.Categories.Maximum] = "60+3d10";
                testCases[CreatureConstants.Chimera_White][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(12, 29, true);
                testCases[CreatureConstants.Chimera_White][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(30, 44, true);
                testCases[CreatureConstants.Chimera_White][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(45, 59, true);
                testCases[CreatureConstants.Chimera_White][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(60, 90, true);
                testCases[CreatureConstants.Chimera_White][AgeConstants.Categories.Maximum] = "60+3d10";
                //Source: https://forgottenrealms.fandom.com/wiki/Choker (adulthood)
                //Can find adult age, but nothing about maximum age. So, making it up
                testCases[CreatureConstants.Choker][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(3, 20, true);
                testCases[CreatureConstants.Choker][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(21, 30, true);
                testCases[CreatureConstants.Choker][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(31, 40, true);
                testCases[CreatureConstants.Choker][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(41, 50, true);
                testCases[CreatureConstants.Choker][AgeConstants.Categories.Maximum] = "40+1d10";
                //Source: unknown
                //INFO: Found a thing talking about maximum lifespan, but nothing else. Making up the rest
                testCases[CreatureConstants.Chuul][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(10, 49, true);
                testCases[CreatureConstants.Chuul][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(50, 79, true);
                testCases[CreatureConstants.Chuul][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(80, 99, true);
                testCases[CreatureConstants.Chuul][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(100, 110, true);
                testCases[CreatureConstants.Chuul][AgeConstants.Categories.Maximum] = "100+1d10";
                //Source: https://www.worldanvil.com/w/faerun-tatortotzke/a/cloaker-species
                testCases[CreatureConstants.Cloaker][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(20, 49, true);
                testCases[CreatureConstants.Cloaker][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(50, 74, true);
                testCases[CreatureConstants.Cloaker][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(75, 99, true);
                testCases[CreatureConstants.Cloaker][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(100, 150, true);
                testCases[CreatureConstants.Cloaker][AgeConstants.Categories.Maximum] = "100+5d10";
                //Source: making it up, roughly basing on geese: https://www.dimensions.com/element/canada-goose-branta-canadensis
                testCases[CreatureConstants.Cockatrice][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(2, 5, true);
                testCases[CreatureConstants.Cockatrice][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(6, 8, true);
                testCases[CreatureConstants.Cockatrice][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(9, 11, true);
                testCases[CreatureConstants.Cockatrice][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(12, 24, true);
                testCases[CreatureConstants.Cockatrice][AgeConstants.Categories.Maximum] = "12+2d6";
                //Source: https://dumpstatadventures.com/blog/deep-dive-the-couatl (adulthood)
                testCases[CreatureConstants.Couatl][AgeConstants.Categories.Adulthood] = $"{tenKRoll}+35";
                testCases[CreatureConstants.Couatl][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                //Source: https://pathfinder.d20srd.org/bestiary3/sphinx.html (adulthood)
                //https://www.belloflostsouls.net/2021/12/dd-monster-spotlight-androsphinx.html (maximum)
                testCases[CreatureConstants.Criosphinx][AgeConstants.Categories.Adulthood] = $"{tenKRoll}+3";
                testCases[CreatureConstants.Criosphinx][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                //Source: https://www.dimensions.com/element/nile-crocodile-crocodylus-niloticus (maximum)
                //https://en.wikipedia.org/wiki/Nile_crocodile (adulthood)
                testCases[CreatureConstants.Crocodile][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(12, 24, true);
                testCases[CreatureConstants.Crocodile][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(25, 37, true);
                testCases[CreatureConstants.Crocodile][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(38, 49, true);
                testCases[CreatureConstants.Crocodile][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(50, 80, true);
                testCases[CreatureConstants.Crocodile][AgeConstants.Categories.Maximum] = "50+3d10";
                //Source: https://www.dimensions.com/element/saltwater-crocodile-crocodylus-porosus
                //https://en.wikipedia.org/wiki/Saltwater_crocodile (adulthood, same as nile crocodiles)
                testCases[CreatureConstants.Crocodile_Giant][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(12, 49, true);
                testCases[CreatureConstants.Crocodile_Giant][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(50, 69, true);
                testCases[CreatureConstants.Crocodile_Giant][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(70, 100, true);
                testCases[CreatureConstants.Crocodile_Giant][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(101, 120, true);
                testCases[CreatureConstants.Crocodile_Giant][AgeConstants.Categories.Maximum] = "100+2d10";
                //Source: https://www.reddit.com/r/DnDBehindTheScreen/comments/63zx1m/hydra/
                testCases[CreatureConstants.Cryohydra_5Heads][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(25, 25 * 20, true);
                testCases[CreatureConstants.Cryohydra_5Heads][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Cryohydra_6Heads][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(501, 1000, true);
                testCases[CreatureConstants.Cryohydra_6Heads][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Cryohydra_7Heads][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(1001, 2000, true);
                testCases[CreatureConstants.Cryohydra_7Heads][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Cryohydra_8Heads][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(2001, 4000, true);
                testCases[CreatureConstants.Cryohydra_8Heads][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Cryohydra_9Heads][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(4001, 6000, true);
                testCases[CreatureConstants.Cryohydra_9Heads][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Cryohydra_10Heads][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(6001, 8000, true);
                testCases[CreatureConstants.Cryohydra_10Heads][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Cryohydra_11Heads][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(8001, 10_000, true);
                testCases[CreatureConstants.Cryohydra_11Heads][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Cryohydra_12Heads][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(10_000, 12_000, true);
                testCases[CreatureConstants.Cryohydra_12Heads][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                //Source: https://forgottenrealms.fandom.com/wiki/Darkmantle
                testCases[CreatureConstants.Darkmantle][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(0, 2, true);
                testCases[CreatureConstants.Darkmantle][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(3, 4, true);
                testCases[CreatureConstants.Darkmantle][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(5, 5, true);
                testCases[CreatureConstants.Darkmantle][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(6, 9, true);
                testCases[CreatureConstants.Darkmantle][AgeConstants.Categories.Maximum] = "6+1d3";
                //Source: https://jurassicworld-evolution.fandom.com/wiki/Deinonychus
                testCases[CreatureConstants.Deinonychus][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(16, 38, true);
                testCases[CreatureConstants.Deinonychus][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(39, 58, true);
                testCases[CreatureConstants.Deinonychus][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(59, 77, true);
                testCases[CreatureConstants.Deinonychus][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(78, 118, true);
                testCases[CreatureConstants.Deinonychus][AgeConstants.Categories.Maximum] = "78+2d20";
                //INFO: Can't find anything on lifespan. So, making it up with max age of 50
                testCases[CreatureConstants.Delver][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(10, 24, true);
                testCases[CreatureConstants.Delver][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(25, 37, true);
                testCases[CreatureConstants.Delver][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(38, 49, true);
                testCases[CreatureConstants.Delver][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(50, 75, true);
                testCases[CreatureConstants.Delver][AgeConstants.Categories.Maximum] = RollHelper.GetRollWithFewestDice(50, 75);
                //Source (5e): https://www.5esrd.com/database/race/derro/
                testCases[CreatureConstants.Derro][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(15, 39, true);
                testCases[CreatureConstants.Derro][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(38, 56, true);
                testCases[CreatureConstants.Derro][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(57, 74, true);
                testCases[CreatureConstants.Derro][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(75, 115, true);
                testCases[CreatureConstants.Derro][AgeConstants.Categories.Maximum] = "75+2d20";
                testCases[CreatureConstants.Derro_Sane][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(15, 39, true);
                testCases[CreatureConstants.Derro_Sane][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(38, 56, true);
                testCases[CreatureConstants.Derro_Sane][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(57, 74, true);
                testCases[CreatureConstants.Derro_Sane][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(75, 115, true);
                testCases[CreatureConstants.Derro_Sane][AgeConstants.Categories.Maximum] = "75+2d20";
                //INFO: Can't find anything on lifespan. So, making it up with max age of 50
                testCases[CreatureConstants.Destrachan][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(10, 24, true);
                testCases[CreatureConstants.Destrachan][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(25, 37, true);
                testCases[CreatureConstants.Destrachan][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(38, 49, true);
                testCases[CreatureConstants.Destrachan][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(50, 75, true);
                testCases[CreatureConstants.Destrachan][AgeConstants.Categories.Maximum] = RollHelper.GetRollWithFewestDice(50, 75);
                testCases[CreatureConstants.Devourer][AgeConstants.Categories.Undead] = undeadAgeRoll;
                testCases[CreatureConstants.Devourer][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                //INFO: Can't find anything on lifespan. So, making it up with max age of 50
                testCases[CreatureConstants.Digester][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(10, 24, true);
                testCases[CreatureConstants.Digester][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(25, 37, true);
                testCases[CreatureConstants.Digester][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(38, 49, true);
                testCases[CreatureConstants.Digester][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(50, 75, true);
                testCases[CreatureConstants.Digester][AgeConstants.Categories.Maximum] = RollHelper.GetRollWithFewestDice(50, 75);
                //Source: https://forgottenrealms.fandom.com/wiki/Displacer_beast
                testCases[CreatureConstants.DisplacerBeast][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(1, 49, true);
                testCases[CreatureConstants.DisplacerBeast][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(50, 74, true);
                testCases[CreatureConstants.DisplacerBeast][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(75, 99, true);
                testCases[CreatureConstants.DisplacerBeast][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(100, 150, true);
                testCases[CreatureConstants.DisplacerBeast][AgeConstants.Categories.Maximum] = "100+5d10";
                testCases[CreatureConstants.DisplacerBeast_PackLord][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(1, 49, true);
                testCases[CreatureConstants.DisplacerBeast_PackLord][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(50, 69, true);
                testCases[CreatureConstants.DisplacerBeast_PackLord][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(70, 99, true);
                testCases[CreatureConstants.DisplacerBeast_PackLord][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(100, 150, true);
                testCases[CreatureConstants.DisplacerBeast_PackLord][AgeConstants.Categories.Maximum] = "100+5d10";
                testCases[CreatureConstants.Djinni][AgeConstants.Categories.Adulthood] = outsiderAgeRoll;
                testCases[CreatureConstants.Djinni][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Djinni_Noble][AgeConstants.Categories.Adulthood] = outsiderAgeRoll;
                testCases[CreatureConstants.Djinni_Noble][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                //Source: https://www.dimensions.com/element/eastern-coyote W:10-15,C:->20
                testCases[CreatureConstants.Dog][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(1, 7, true);
                testCases[CreatureConstants.Dog][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(8, 9, true);
                testCases[CreatureConstants.Dog][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(10, 15, true);
                testCases[CreatureConstants.Dog][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(16, 20, true);
                testCases[CreatureConstants.Dog][AgeConstants.Categories.Maximum] = "14+2d3";
                //Source: https://www.dimensions.com/element/saint-bernard-dog 8-10
                //https://www.dimensions.com/element/siberian-husky 12-14
                //https://www.dimensions.com/element/dogs-collie 14-16
                testCases[CreatureConstants.Dog_Riding][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(1, 3, true);
                testCases[CreatureConstants.Dog_Riding][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(4, 5, true);
                testCases[CreatureConstants.Dog_Riding][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(6, 7, true);
                testCases[CreatureConstants.Dog_Riding][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(8, 16, true);
                testCases[CreatureConstants.Dog_Riding][AgeConstants.Categories.Maximum] = "8+2d4";
                //Source: https://www.dimensions.com/element/donkey-equus-africanus-asinus (maximum)
                //https://www.thedonkeysanctuary.org.uk/research/taxonomy/term/153 (adulthood)
                testCases[CreatureConstants.Donkey][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(3, 12, true);
                testCases[CreatureConstants.Donkey][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(13, 18, true);
                testCases[CreatureConstants.Donkey][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(19, 24, true);
                testCases[CreatureConstants.Donkey][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(25, 40, true);
                testCases[CreatureConstants.Donkey][AgeConstants.Categories.Maximum] = "24+2d8";
                //Source: https://www.worldanvil.com/w/faerun-tatortotzke/a/doppelganger-species
                testCases[CreatureConstants.Doppelganger][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(18, 44, true);
                testCases[CreatureConstants.Doppelganger][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(45, 67, true);
                testCases[CreatureConstants.Doppelganger][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(68, 89, true);
                testCases[CreatureConstants.Doppelganger][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(90, 135, true);
                testCases[CreatureConstants.Doppelganger][AgeConstants.Categories.Maximum] = RollHelper.GetRollWithFewestDice(90, 135);
                //Source: https://www.d20srd.org/srd/monsters/dragonTrue.htm
                //Maximum from the Draconomicon
                testCases[CreatureConstants.Dragon_Black_Wyrmling][AgeConstants.Categories.Dragon.Wyrmling] = RollHelper.GetRollWithMostEvenDistribution(0, 5, true);
                testCases[CreatureConstants.Dragon_Black_Wyrmling][AgeConstants.Categories.Maximum] = "6";
                testCases[CreatureConstants.Dragon_Black_VeryYoung][AgeConstants.Categories.Dragon.VeryYoung] = RollHelper.GetRollWithMostEvenDistribution(6, 15, true);
                testCases[CreatureConstants.Dragon_Black_VeryYoung][AgeConstants.Categories.Maximum] = "16";
                testCases[CreatureConstants.Dragon_Black_Young][AgeConstants.Categories.Dragon.Young] = RollHelper.GetRollWithMostEvenDistribution(16, 25, true);
                testCases[CreatureConstants.Dragon_Black_Young][AgeConstants.Categories.Maximum] = "26";
                testCases[CreatureConstants.Dragon_Black_Juvenile][AgeConstants.Categories.Dragon.Juvenile] = RollHelper.GetRollWithMostEvenDistribution(26, 50, true);
                testCases[CreatureConstants.Dragon_Black_Juvenile][AgeConstants.Categories.Maximum] = "51";
                testCases[CreatureConstants.Dragon_Black_YoungAdult][AgeConstants.Categories.Dragon.YoungAdult] = RollHelper.GetRollWithMostEvenDistribution(51, 100, true);
                testCases[CreatureConstants.Dragon_Black_YoungAdult][AgeConstants.Categories.Maximum] = "101";
                testCases[CreatureConstants.Dragon_Black_Adult][AgeConstants.Categories.Dragon.Adult] = RollHelper.GetRollWithMostEvenDistribution(101, 200, true);
                testCases[CreatureConstants.Dragon_Black_Adult][AgeConstants.Categories.Maximum] = "201";
                testCases[CreatureConstants.Dragon_Black_MatureAdult][AgeConstants.Categories.Dragon.MatureAdult] = RollHelper.GetRollWithMostEvenDistribution(201, 400, true);
                testCases[CreatureConstants.Dragon_Black_MatureAdult][AgeConstants.Categories.Maximum] = "401";
                testCases[CreatureConstants.Dragon_Black_Old][AgeConstants.Categories.Dragon.Old] = RollHelper.GetRollWithMostEvenDistribution(401, 600, true);
                testCases[CreatureConstants.Dragon_Black_Old][AgeConstants.Categories.Maximum] = "601";
                testCases[CreatureConstants.Dragon_Black_VeryOld][AgeConstants.Categories.Dragon.VeryOld] = RollHelper.GetRollWithMostEvenDistribution(601, 800, true);
                testCases[CreatureConstants.Dragon_Black_VeryOld][AgeConstants.Categories.Maximum] = "801";
                testCases[CreatureConstants.Dragon_Black_Ancient][AgeConstants.Categories.Dragon.Ancient] = RollHelper.GetRollWithMostEvenDistribution(801, 1000, true);
                testCases[CreatureConstants.Dragon_Black_Ancient][AgeConstants.Categories.Maximum] = "1001";
                testCases[CreatureConstants.Dragon_Black_Wyrm][AgeConstants.Categories.Dragon.Wyrm] = RollHelper.GetRollWithMostEvenDistribution(1001, 1200, true);
                testCases[CreatureConstants.Dragon_Black_Wyrm][AgeConstants.Categories.Maximum] = "1201";
                testCases[CreatureConstants.Dragon_Black_GreatWyrm][AgeConstants.Categories.Dragon.GreatWyrm] = RollHelper.GetRollWithMostEvenDistribution(1201, 2200, true);
                testCases[CreatureConstants.Dragon_Black_GreatWyrm][AgeConstants.Categories.Maximum] = "2201";
                testCases[CreatureConstants.Dragon_Blue_Wyrmling][AgeConstants.Categories.Dragon.Wyrmling] = RollHelper.GetRollWithMostEvenDistribution(0, 5, true);
                testCases[CreatureConstants.Dragon_Blue_Wyrmling][AgeConstants.Categories.Maximum] = "6";
                testCases[CreatureConstants.Dragon_Blue_VeryYoung][AgeConstants.Categories.Dragon.VeryYoung] = RollHelper.GetRollWithMostEvenDistribution(6, 15, true);
                testCases[CreatureConstants.Dragon_Blue_VeryYoung][AgeConstants.Categories.Maximum] = "16";
                testCases[CreatureConstants.Dragon_Blue_Young][AgeConstants.Categories.Dragon.Young] = RollHelper.GetRollWithMostEvenDistribution(16, 25, true);
                testCases[CreatureConstants.Dragon_Blue_Young][AgeConstants.Categories.Maximum] = "26";
                testCases[CreatureConstants.Dragon_Blue_Juvenile][AgeConstants.Categories.Dragon.Juvenile] = RollHelper.GetRollWithMostEvenDistribution(26, 50, true);
                testCases[CreatureConstants.Dragon_Blue_Juvenile][AgeConstants.Categories.Maximum] = "51";
                testCases[CreatureConstants.Dragon_Blue_YoungAdult][AgeConstants.Categories.Dragon.YoungAdult] = RollHelper.GetRollWithMostEvenDistribution(51, 100, true);
                testCases[CreatureConstants.Dragon_Blue_YoungAdult][AgeConstants.Categories.Maximum] = "101";
                testCases[CreatureConstants.Dragon_Blue_Adult][AgeConstants.Categories.Dragon.Adult] = RollHelper.GetRollWithMostEvenDistribution(101, 200, true);
                testCases[CreatureConstants.Dragon_Blue_Adult][AgeConstants.Categories.Maximum] = "201";
                testCases[CreatureConstants.Dragon_Blue_MatureAdult][AgeConstants.Categories.Dragon.MatureAdult] = RollHelper.GetRollWithMostEvenDistribution(201, 400, true);
                testCases[CreatureConstants.Dragon_Blue_MatureAdult][AgeConstants.Categories.Maximum] = "401";
                testCases[CreatureConstants.Dragon_Blue_Old][AgeConstants.Categories.Dragon.Old] = RollHelper.GetRollWithMostEvenDistribution(401, 600, true);
                testCases[CreatureConstants.Dragon_Blue_Old][AgeConstants.Categories.Maximum] = "601";
                testCases[CreatureConstants.Dragon_Blue_VeryOld][AgeConstants.Categories.Dragon.VeryOld] = RollHelper.GetRollWithMostEvenDistribution(601, 800, true);
                testCases[CreatureConstants.Dragon_Blue_VeryOld][AgeConstants.Categories.Maximum] = "801";
                testCases[CreatureConstants.Dragon_Blue_Ancient][AgeConstants.Categories.Dragon.Ancient] = RollHelper.GetRollWithMostEvenDistribution(801, 1000, true);
                testCases[CreatureConstants.Dragon_Blue_Ancient][AgeConstants.Categories.Maximum] = "1001";
                testCases[CreatureConstants.Dragon_Blue_Wyrm][AgeConstants.Categories.Dragon.Wyrm] = RollHelper.GetRollWithMostEvenDistribution(1001, 1200, true);
                testCases[CreatureConstants.Dragon_Blue_Wyrm][AgeConstants.Categories.Maximum] = "1201";
                testCases[CreatureConstants.Dragon_Blue_GreatWyrm][AgeConstants.Categories.Dragon.GreatWyrm] = RollHelper.GetRollWithMostEvenDistribution(1201, 2300, true);
                testCases[CreatureConstants.Dragon_Blue_GreatWyrm][AgeConstants.Categories.Maximum] = "2301";
                testCases[CreatureConstants.Dragon_Brass_Wyrmling][AgeConstants.Categories.Dragon.Wyrmling] = RollHelper.GetRollWithMostEvenDistribution(0, 5, true);
                testCases[CreatureConstants.Dragon_Brass_Wyrmling][AgeConstants.Categories.Maximum] = "6";
                testCases[CreatureConstants.Dragon_Brass_VeryYoung][AgeConstants.Categories.Dragon.VeryYoung] = RollHelper.GetRollWithMostEvenDistribution(6, 15, true);
                testCases[CreatureConstants.Dragon_Brass_VeryYoung][AgeConstants.Categories.Maximum] = "16";
                testCases[CreatureConstants.Dragon_Brass_Young][AgeConstants.Categories.Dragon.Young] = RollHelper.GetRollWithMostEvenDistribution(16, 25, true);
                testCases[CreatureConstants.Dragon_Brass_Young][AgeConstants.Categories.Maximum] = "26";
                testCases[CreatureConstants.Dragon_Brass_Juvenile][AgeConstants.Categories.Dragon.Juvenile] = RollHelper.GetRollWithMostEvenDistribution(26, 50, true);
                testCases[CreatureConstants.Dragon_Brass_Juvenile][AgeConstants.Categories.Maximum] = "51";
                testCases[CreatureConstants.Dragon_Brass_YoungAdult][AgeConstants.Categories.Dragon.YoungAdult] = RollHelper.GetRollWithMostEvenDistribution(51, 100, true);
                testCases[CreatureConstants.Dragon_Brass_YoungAdult][AgeConstants.Categories.Maximum] = "101";
                testCases[CreatureConstants.Dragon_Brass_Adult][AgeConstants.Categories.Dragon.Adult] = RollHelper.GetRollWithMostEvenDistribution(101, 200, true);
                testCases[CreatureConstants.Dragon_Brass_Adult][AgeConstants.Categories.Maximum] = "201";
                testCases[CreatureConstants.Dragon_Brass_MatureAdult][AgeConstants.Categories.Dragon.MatureAdult] = RollHelper.GetRollWithMostEvenDistribution(201, 400, true);
                testCases[CreatureConstants.Dragon_Brass_MatureAdult][AgeConstants.Categories.Maximum] = "401";
                testCases[CreatureConstants.Dragon_Brass_Old][AgeConstants.Categories.Dragon.Old] = RollHelper.GetRollWithMostEvenDistribution(401, 600, true);
                testCases[CreatureConstants.Dragon_Brass_Old][AgeConstants.Categories.Maximum] = "601";
                testCases[CreatureConstants.Dragon_Brass_VeryOld][AgeConstants.Categories.Dragon.VeryOld] = RollHelper.GetRollWithMostEvenDistribution(601, 800, true);
                testCases[CreatureConstants.Dragon_Brass_VeryOld][AgeConstants.Categories.Maximum] = "801";
                testCases[CreatureConstants.Dragon_Brass_Ancient][AgeConstants.Categories.Dragon.Ancient] = RollHelper.GetRollWithMostEvenDistribution(801, 1000, true);
                testCases[CreatureConstants.Dragon_Brass_Ancient][AgeConstants.Categories.Maximum] = "1001";
                testCases[CreatureConstants.Dragon_Brass_Wyrm][AgeConstants.Categories.Dragon.Wyrm] = RollHelper.GetRollWithMostEvenDistribution(1001, 1200, true);
                testCases[CreatureConstants.Dragon_Brass_Wyrm][AgeConstants.Categories.Maximum] = "1201";
                testCases[CreatureConstants.Dragon_Brass_GreatWyrm][AgeConstants.Categories.Dragon.GreatWyrm] = RollHelper.GetRollWithMostEvenDistribution(1201, 3200, true);
                testCases[CreatureConstants.Dragon_Brass_GreatWyrm][AgeConstants.Categories.Maximum] = "3201";
                testCases[CreatureConstants.Dragon_Bronze_Wyrmling][AgeConstants.Categories.Dragon.Wyrmling] = RollHelper.GetRollWithMostEvenDistribution(0, 5, true);
                testCases[CreatureConstants.Dragon_Bronze_Wyrmling][AgeConstants.Categories.Maximum] = "6";
                testCases[CreatureConstants.Dragon_Bronze_VeryYoung][AgeConstants.Categories.Dragon.VeryYoung] = RollHelper.GetRollWithMostEvenDistribution(6, 15, true);
                testCases[CreatureConstants.Dragon_Bronze_VeryYoung][AgeConstants.Categories.Maximum] = "16";
                testCases[CreatureConstants.Dragon_Bronze_Young][AgeConstants.Categories.Dragon.Young] = RollHelper.GetRollWithMostEvenDistribution(16, 25, true);
                testCases[CreatureConstants.Dragon_Bronze_Young][AgeConstants.Categories.Maximum] = "26";
                testCases[CreatureConstants.Dragon_Bronze_Juvenile][AgeConstants.Categories.Dragon.Juvenile] = RollHelper.GetRollWithMostEvenDistribution(26, 50, true);
                testCases[CreatureConstants.Dragon_Bronze_Juvenile][AgeConstants.Categories.Maximum] = "51";
                testCases[CreatureConstants.Dragon_Bronze_YoungAdult][AgeConstants.Categories.Dragon.YoungAdult] = RollHelper.GetRollWithMostEvenDistribution(51, 100, true);
                testCases[CreatureConstants.Dragon_Bronze_YoungAdult][AgeConstants.Categories.Maximum] = "101";
                testCases[CreatureConstants.Dragon_Bronze_Adult][AgeConstants.Categories.Dragon.Adult] = RollHelper.GetRollWithMostEvenDistribution(101, 200, true);
                testCases[CreatureConstants.Dragon_Bronze_Adult][AgeConstants.Categories.Maximum] = "201";
                testCases[CreatureConstants.Dragon_Bronze_MatureAdult][AgeConstants.Categories.Dragon.MatureAdult] = RollHelper.GetRollWithMostEvenDistribution(201, 400, true);
                testCases[CreatureConstants.Dragon_Bronze_MatureAdult][AgeConstants.Categories.Maximum] = "401";
                testCases[CreatureConstants.Dragon_Bronze_Old][AgeConstants.Categories.Dragon.Old] = RollHelper.GetRollWithMostEvenDistribution(401, 600, true);
                testCases[CreatureConstants.Dragon_Bronze_Old][AgeConstants.Categories.Maximum] = "601";
                testCases[CreatureConstants.Dragon_Bronze_VeryOld][AgeConstants.Categories.Dragon.VeryOld] = RollHelper.GetRollWithMostEvenDistribution(601, 800, true);
                testCases[CreatureConstants.Dragon_Bronze_VeryOld][AgeConstants.Categories.Maximum] = "801";
                testCases[CreatureConstants.Dragon_Bronze_Ancient][AgeConstants.Categories.Dragon.Ancient] = RollHelper.GetRollWithMostEvenDistribution(801, 1000, true);
                testCases[CreatureConstants.Dragon_Bronze_Ancient][AgeConstants.Categories.Maximum] = "1001";
                testCases[CreatureConstants.Dragon_Bronze_Wyrm][AgeConstants.Categories.Dragon.Wyrm] = RollHelper.GetRollWithMostEvenDistribution(1001, 1200, true);
                testCases[CreatureConstants.Dragon_Bronze_Wyrm][AgeConstants.Categories.Maximum] = "1201";
                testCases[CreatureConstants.Dragon_Bronze_GreatWyrm][AgeConstants.Categories.Dragon.GreatWyrm] = RollHelper.GetRollWithMostEvenDistribution(1201, 3800, true);
                testCases[CreatureConstants.Dragon_Bronze_GreatWyrm][AgeConstants.Categories.Maximum] = "3801";
                testCases[CreatureConstants.Dragon_Copper_Wyrmling][AgeConstants.Categories.Dragon.Wyrmling] = RollHelper.GetRollWithMostEvenDistribution(0, 5, true);
                testCases[CreatureConstants.Dragon_Copper_Wyrmling][AgeConstants.Categories.Maximum] = "6";
                testCases[CreatureConstants.Dragon_Copper_VeryYoung][AgeConstants.Categories.Dragon.VeryYoung] = RollHelper.GetRollWithMostEvenDistribution(6, 15, true);
                testCases[CreatureConstants.Dragon_Copper_VeryYoung][AgeConstants.Categories.Maximum] = "16";
                testCases[CreatureConstants.Dragon_Copper_Young][AgeConstants.Categories.Dragon.Young] = RollHelper.GetRollWithMostEvenDistribution(16, 25, true);
                testCases[CreatureConstants.Dragon_Copper_Young][AgeConstants.Categories.Maximum] = "26";
                testCases[CreatureConstants.Dragon_Copper_Juvenile][AgeConstants.Categories.Dragon.Juvenile] = RollHelper.GetRollWithMostEvenDistribution(26, 50, true);
                testCases[CreatureConstants.Dragon_Copper_Juvenile][AgeConstants.Categories.Maximum] = "51";
                testCases[CreatureConstants.Dragon_Copper_YoungAdult][AgeConstants.Categories.Dragon.YoungAdult] = RollHelper.GetRollWithMostEvenDistribution(51, 100, true);
                testCases[CreatureConstants.Dragon_Copper_YoungAdult][AgeConstants.Categories.Maximum] = "101";
                testCases[CreatureConstants.Dragon_Copper_Adult][AgeConstants.Categories.Dragon.Adult] = RollHelper.GetRollWithMostEvenDistribution(101, 200, true);
                testCases[CreatureConstants.Dragon_Copper_Adult][AgeConstants.Categories.Maximum] = "201";
                testCases[CreatureConstants.Dragon_Copper_MatureAdult][AgeConstants.Categories.Dragon.MatureAdult] = RollHelper.GetRollWithMostEvenDistribution(201, 400, true);
                testCases[CreatureConstants.Dragon_Copper_MatureAdult][AgeConstants.Categories.Maximum] = "401";
                testCases[CreatureConstants.Dragon_Copper_Old][AgeConstants.Categories.Dragon.Old] = RollHelper.GetRollWithMostEvenDistribution(401, 600, true);
                testCases[CreatureConstants.Dragon_Copper_Old][AgeConstants.Categories.Maximum] = "601";
                testCases[CreatureConstants.Dragon_Copper_VeryOld][AgeConstants.Categories.Dragon.VeryOld] = RollHelper.GetRollWithMostEvenDistribution(601, 800, true);
                testCases[CreatureConstants.Dragon_Copper_VeryOld][AgeConstants.Categories.Maximum] = "801";
                testCases[CreatureConstants.Dragon_Copper_Ancient][AgeConstants.Categories.Dragon.Ancient] = RollHelper.GetRollWithMostEvenDistribution(801, 1000, true);
                testCases[CreatureConstants.Dragon_Copper_Ancient][AgeConstants.Categories.Maximum] = "1001";
                testCases[CreatureConstants.Dragon_Copper_Wyrm][AgeConstants.Categories.Dragon.Wyrm] = RollHelper.GetRollWithMostEvenDistribution(1001, 1200, true);
                testCases[CreatureConstants.Dragon_Copper_Wyrm][AgeConstants.Categories.Maximum] = "1201";
                testCases[CreatureConstants.Dragon_Copper_GreatWyrm][AgeConstants.Categories.Dragon.GreatWyrm] = RollHelper.GetRollWithMostEvenDistribution(1201, 3400, true);
                testCases[CreatureConstants.Dragon_Copper_GreatWyrm][AgeConstants.Categories.Maximum] = "3401";
                testCases[CreatureConstants.Dragon_Green_Wyrmling][AgeConstants.Categories.Dragon.Wyrmling] = RollHelper.GetRollWithMostEvenDistribution(0, 5, true);
                testCases[CreatureConstants.Dragon_Green_Wyrmling][AgeConstants.Categories.Maximum] = "6";
                testCases[CreatureConstants.Dragon_Green_VeryYoung][AgeConstants.Categories.Dragon.VeryYoung] = RollHelper.GetRollWithMostEvenDistribution(6, 15, true);
                testCases[CreatureConstants.Dragon_Green_VeryYoung][AgeConstants.Categories.Maximum] = "16";
                testCases[CreatureConstants.Dragon_Green_Young][AgeConstants.Categories.Dragon.Young] = RollHelper.GetRollWithMostEvenDistribution(16, 25, true);
                testCases[CreatureConstants.Dragon_Green_Young][AgeConstants.Categories.Maximum] = "26";
                testCases[CreatureConstants.Dragon_Green_Juvenile][AgeConstants.Categories.Dragon.Juvenile] = RollHelper.GetRollWithMostEvenDistribution(26, 50, true);
                testCases[CreatureConstants.Dragon_Green_Juvenile][AgeConstants.Categories.Maximum] = "51";
                testCases[CreatureConstants.Dragon_Green_YoungAdult][AgeConstants.Categories.Dragon.YoungAdult] = RollHelper.GetRollWithMostEvenDistribution(51, 100, true);
                testCases[CreatureConstants.Dragon_Green_YoungAdult][AgeConstants.Categories.Maximum] = "101";
                testCases[CreatureConstants.Dragon_Green_Adult][AgeConstants.Categories.Dragon.Adult] = RollHelper.GetRollWithMostEvenDistribution(101, 200, true);
                testCases[CreatureConstants.Dragon_Green_Adult][AgeConstants.Categories.Maximum] = "201";
                testCases[CreatureConstants.Dragon_Green_MatureAdult][AgeConstants.Categories.Dragon.MatureAdult] = RollHelper.GetRollWithMostEvenDistribution(201, 400, true);
                testCases[CreatureConstants.Dragon_Green_MatureAdult][AgeConstants.Categories.Maximum] = "401";
                testCases[CreatureConstants.Dragon_Green_Old][AgeConstants.Categories.Dragon.Old] = RollHelper.GetRollWithMostEvenDistribution(401, 600, true);
                testCases[CreatureConstants.Dragon_Green_Old][AgeConstants.Categories.Maximum] = "601";
                testCases[CreatureConstants.Dragon_Green_VeryOld][AgeConstants.Categories.Dragon.VeryOld] = RollHelper.GetRollWithMostEvenDistribution(601, 800, true);
                testCases[CreatureConstants.Dragon_Green_VeryOld][AgeConstants.Categories.Maximum] = "801";
                testCases[CreatureConstants.Dragon_Green_Ancient][AgeConstants.Categories.Dragon.Ancient] = RollHelper.GetRollWithMostEvenDistribution(801, 1000, true);
                testCases[CreatureConstants.Dragon_Green_Ancient][AgeConstants.Categories.Maximum] = "1001";
                testCases[CreatureConstants.Dragon_Green_Wyrm][AgeConstants.Categories.Dragon.Wyrm] = RollHelper.GetRollWithMostEvenDistribution(1001, 1200, true);
                testCases[CreatureConstants.Dragon_Green_Wyrm][AgeConstants.Categories.Maximum] = "1201";
                testCases[CreatureConstants.Dragon_Green_GreatWyrm][AgeConstants.Categories.Dragon.GreatWyrm] = RollHelper.GetRollWithMostEvenDistribution(1201, 2300, true);
                testCases[CreatureConstants.Dragon_Green_GreatWyrm][AgeConstants.Categories.Maximum] = "2301";
                testCases[CreatureConstants.Dragon_Gold_Wyrmling][AgeConstants.Categories.Dragon.Wyrmling] = RollHelper.GetRollWithMostEvenDistribution(0, 5, true);
                testCases[CreatureConstants.Dragon_Gold_Wyrmling][AgeConstants.Categories.Maximum] = "6";
                testCases[CreatureConstants.Dragon_Gold_VeryYoung][AgeConstants.Categories.Dragon.VeryYoung] = RollHelper.GetRollWithMostEvenDistribution(6, 15, true);
                testCases[CreatureConstants.Dragon_Gold_VeryYoung][AgeConstants.Categories.Maximum] = "16";
                testCases[CreatureConstants.Dragon_Gold_Young][AgeConstants.Categories.Dragon.Young] = RollHelper.GetRollWithMostEvenDistribution(16, 25, true);
                testCases[CreatureConstants.Dragon_Gold_Young][AgeConstants.Categories.Maximum] = "26";
                testCases[CreatureConstants.Dragon_Gold_Juvenile][AgeConstants.Categories.Dragon.Juvenile] = RollHelper.GetRollWithMostEvenDistribution(26, 50, true);
                testCases[CreatureConstants.Dragon_Gold_Juvenile][AgeConstants.Categories.Maximum] = "51";
                testCases[CreatureConstants.Dragon_Gold_YoungAdult][AgeConstants.Categories.Dragon.YoungAdult] = RollHelper.GetRollWithMostEvenDistribution(51, 100, true);
                testCases[CreatureConstants.Dragon_Gold_YoungAdult][AgeConstants.Categories.Maximum] = "101";
                testCases[CreatureConstants.Dragon_Gold_Adult][AgeConstants.Categories.Dragon.Adult] = RollHelper.GetRollWithMostEvenDistribution(101, 200, true);
                testCases[CreatureConstants.Dragon_Gold_Adult][AgeConstants.Categories.Maximum] = "201";
                testCases[CreatureConstants.Dragon_Gold_MatureAdult][AgeConstants.Categories.Dragon.MatureAdult] = RollHelper.GetRollWithMostEvenDistribution(201, 400, true);
                testCases[CreatureConstants.Dragon_Gold_MatureAdult][AgeConstants.Categories.Maximum] = "401";
                testCases[CreatureConstants.Dragon_Gold_Old][AgeConstants.Categories.Dragon.Old] = RollHelper.GetRollWithMostEvenDistribution(401, 600, true);
                testCases[CreatureConstants.Dragon_Gold_Old][AgeConstants.Categories.Maximum] = "601";
                testCases[CreatureConstants.Dragon_Gold_VeryOld][AgeConstants.Categories.Dragon.VeryOld] = RollHelper.GetRollWithMostEvenDistribution(601, 800, true);
                testCases[CreatureConstants.Dragon_Gold_VeryOld][AgeConstants.Categories.Maximum] = "801";
                testCases[CreatureConstants.Dragon_Gold_Ancient][AgeConstants.Categories.Dragon.Ancient] = RollHelper.GetRollWithMostEvenDistribution(801, 1000, true);
                testCases[CreatureConstants.Dragon_Gold_Ancient][AgeConstants.Categories.Maximum] = "1001";
                testCases[CreatureConstants.Dragon_Gold_Wyrm][AgeConstants.Categories.Dragon.Wyrm] = RollHelper.GetRollWithMostEvenDistribution(1001, 1200, true);
                testCases[CreatureConstants.Dragon_Gold_Wyrm][AgeConstants.Categories.Maximum] = "1201";
                testCases[CreatureConstants.Dragon_Gold_GreatWyrm][AgeConstants.Categories.Dragon.GreatWyrm] = RollHelper.GetRollWithMostEvenDistribution(1201, 4400, true);
                testCases[CreatureConstants.Dragon_Gold_GreatWyrm][AgeConstants.Categories.Maximum] = "4401";
                testCases[CreatureConstants.Dragon_Red_Wyrmling][AgeConstants.Categories.Dragon.Wyrmling] = RollHelper.GetRollWithMostEvenDistribution(0, 5, true);
                testCases[CreatureConstants.Dragon_Red_Wyrmling][AgeConstants.Categories.Maximum] = "6";
                testCases[CreatureConstants.Dragon_Red_VeryYoung][AgeConstants.Categories.Dragon.VeryYoung] = RollHelper.GetRollWithMostEvenDistribution(6, 15, true);
                testCases[CreatureConstants.Dragon_Red_VeryYoung][AgeConstants.Categories.Maximum] = "16";
                testCases[CreatureConstants.Dragon_Red_Young][AgeConstants.Categories.Dragon.Young] = RollHelper.GetRollWithMostEvenDistribution(16, 25, true);
                testCases[CreatureConstants.Dragon_Red_Young][AgeConstants.Categories.Maximum] = "26";
                testCases[CreatureConstants.Dragon_Red_Juvenile][AgeConstants.Categories.Dragon.Juvenile] = RollHelper.GetRollWithMostEvenDistribution(26, 50, true);
                testCases[CreatureConstants.Dragon_Red_Juvenile][AgeConstants.Categories.Maximum] = "51";
                testCases[CreatureConstants.Dragon_Red_YoungAdult][AgeConstants.Categories.Dragon.YoungAdult] = RollHelper.GetRollWithMostEvenDistribution(51, 100, true);
                testCases[CreatureConstants.Dragon_Red_YoungAdult][AgeConstants.Categories.Maximum] = "101";
                testCases[CreatureConstants.Dragon_Red_Adult][AgeConstants.Categories.Dragon.Adult] = RollHelper.GetRollWithMostEvenDistribution(101, 200, true);
                testCases[CreatureConstants.Dragon_Red_Adult][AgeConstants.Categories.Maximum] = "201";
                testCases[CreatureConstants.Dragon_Red_MatureAdult][AgeConstants.Categories.Dragon.MatureAdult] = RollHelper.GetRollWithMostEvenDistribution(201, 400, true);
                testCases[CreatureConstants.Dragon_Red_MatureAdult][AgeConstants.Categories.Maximum] = "401";
                testCases[CreatureConstants.Dragon_Red_Old][AgeConstants.Categories.Dragon.Old] = RollHelper.GetRollWithMostEvenDistribution(401, 600, true);
                testCases[CreatureConstants.Dragon_Red_Old][AgeConstants.Categories.Maximum] = "601";
                testCases[CreatureConstants.Dragon_Red_VeryOld][AgeConstants.Categories.Dragon.VeryOld] = RollHelper.GetRollWithMostEvenDistribution(601, 800, true);
                testCases[CreatureConstants.Dragon_Red_VeryOld][AgeConstants.Categories.Maximum] = "801";
                testCases[CreatureConstants.Dragon_Red_Ancient][AgeConstants.Categories.Dragon.Ancient] = RollHelper.GetRollWithMostEvenDistribution(801, 1000, true);
                testCases[CreatureConstants.Dragon_Red_Ancient][AgeConstants.Categories.Maximum] = "1001";
                testCases[CreatureConstants.Dragon_Red_Wyrm][AgeConstants.Categories.Dragon.Wyrm] = RollHelper.GetRollWithMostEvenDistribution(1001, 1200, true);
                testCases[CreatureConstants.Dragon_Red_Wyrm][AgeConstants.Categories.Maximum] = "1201";
                testCases[CreatureConstants.Dragon_Red_GreatWyrm][AgeConstants.Categories.Dragon.GreatWyrm] = RollHelper.GetRollWithMostEvenDistribution(1201, 2500, true);
                testCases[CreatureConstants.Dragon_Red_GreatWyrm][AgeConstants.Categories.Maximum] = "2501";
                testCases[CreatureConstants.Dragon_Silver_Wyrmling][AgeConstants.Categories.Dragon.Wyrmling] = RollHelper.GetRollWithMostEvenDistribution(0, 5, true);
                testCases[CreatureConstants.Dragon_Silver_Wyrmling][AgeConstants.Categories.Maximum] = "6";
                testCases[CreatureConstants.Dragon_Silver_VeryYoung][AgeConstants.Categories.Dragon.VeryYoung] = RollHelper.GetRollWithMostEvenDistribution(6, 15, true);
                testCases[CreatureConstants.Dragon_Silver_VeryYoung][AgeConstants.Categories.Maximum] = "16";
                testCases[CreatureConstants.Dragon_Silver_Young][AgeConstants.Categories.Dragon.Young] = RollHelper.GetRollWithMostEvenDistribution(16, 25, true);
                testCases[CreatureConstants.Dragon_Silver_Young][AgeConstants.Categories.Maximum] = "26";
                testCases[CreatureConstants.Dragon_Silver_Juvenile][AgeConstants.Categories.Dragon.Juvenile] = RollHelper.GetRollWithMostEvenDistribution(26, 50, true);
                testCases[CreatureConstants.Dragon_Silver_Juvenile][AgeConstants.Categories.Maximum] = "51";
                testCases[CreatureConstants.Dragon_Silver_YoungAdult][AgeConstants.Categories.Dragon.YoungAdult] = RollHelper.GetRollWithMostEvenDistribution(51, 100, true);
                testCases[CreatureConstants.Dragon_Silver_YoungAdult][AgeConstants.Categories.Maximum] = "101";
                testCases[CreatureConstants.Dragon_Silver_Adult][AgeConstants.Categories.Dragon.Adult] = RollHelper.GetRollWithMostEvenDistribution(101, 200, true);
                testCases[CreatureConstants.Dragon_Silver_Adult][AgeConstants.Categories.Maximum] = "201";
                testCases[CreatureConstants.Dragon_Silver_MatureAdult][AgeConstants.Categories.Dragon.MatureAdult] = RollHelper.GetRollWithMostEvenDistribution(201, 400, true);
                testCases[CreatureConstants.Dragon_Silver_MatureAdult][AgeConstants.Categories.Maximum] = "401";
                testCases[CreatureConstants.Dragon_Silver_Old][AgeConstants.Categories.Dragon.Old] = RollHelper.GetRollWithMostEvenDistribution(401, 600, true);
                testCases[CreatureConstants.Dragon_Silver_Old][AgeConstants.Categories.Maximum] = "601";
                testCases[CreatureConstants.Dragon_Silver_VeryOld][AgeConstants.Categories.Dragon.VeryOld] = RollHelper.GetRollWithMostEvenDistribution(601, 800, true);
                testCases[CreatureConstants.Dragon_Silver_VeryOld][AgeConstants.Categories.Maximum] = "801";
                testCases[CreatureConstants.Dragon_Silver_Ancient][AgeConstants.Categories.Dragon.Ancient] = RollHelper.GetRollWithMostEvenDistribution(801, 1000, true);
                testCases[CreatureConstants.Dragon_Silver_Ancient][AgeConstants.Categories.Maximum] = "1001";
                testCases[CreatureConstants.Dragon_Silver_Wyrm][AgeConstants.Categories.Dragon.Wyrm] = RollHelper.GetRollWithMostEvenDistribution(1001, 1200, true);
                testCases[CreatureConstants.Dragon_Silver_Wyrm][AgeConstants.Categories.Maximum] = "1201";
                testCases[CreatureConstants.Dragon_Silver_GreatWyrm][AgeConstants.Categories.Dragon.GreatWyrm] = RollHelper.GetRollWithMostEvenDistribution(1201, 4200, true);
                testCases[CreatureConstants.Dragon_Silver_GreatWyrm][AgeConstants.Categories.Maximum] = "4201";
                testCases[CreatureConstants.Dragon_White_Wyrmling][AgeConstants.Categories.Dragon.Wyrmling] = RollHelper.GetRollWithMostEvenDistribution(0, 5, true);
                testCases[CreatureConstants.Dragon_White_Wyrmling][AgeConstants.Categories.Maximum] = "6";
                testCases[CreatureConstants.Dragon_White_VeryYoung][AgeConstants.Categories.Dragon.VeryYoung] = RollHelper.GetRollWithMostEvenDistribution(6, 15, true);
                testCases[CreatureConstants.Dragon_White_VeryYoung][AgeConstants.Categories.Maximum] = "16";
                testCases[CreatureConstants.Dragon_White_Young][AgeConstants.Categories.Dragon.Young] = RollHelper.GetRollWithMostEvenDistribution(16, 25, true);
                testCases[CreatureConstants.Dragon_White_Young][AgeConstants.Categories.Maximum] = "26";
                testCases[CreatureConstants.Dragon_White_Juvenile][AgeConstants.Categories.Dragon.Juvenile] = RollHelper.GetRollWithMostEvenDistribution(26, 50, true);
                testCases[CreatureConstants.Dragon_White_Juvenile][AgeConstants.Categories.Maximum] = "51";
                testCases[CreatureConstants.Dragon_White_YoungAdult][AgeConstants.Categories.Dragon.YoungAdult] = RollHelper.GetRollWithMostEvenDistribution(51, 100, true);
                testCases[CreatureConstants.Dragon_White_YoungAdult][AgeConstants.Categories.Maximum] = "101";
                testCases[CreatureConstants.Dragon_White_Adult][AgeConstants.Categories.Dragon.Adult] = RollHelper.GetRollWithMostEvenDistribution(101, 200, true);
                testCases[CreatureConstants.Dragon_White_Adult][AgeConstants.Categories.Maximum] = "201";
                testCases[CreatureConstants.Dragon_White_MatureAdult][AgeConstants.Categories.Dragon.MatureAdult] = RollHelper.GetRollWithMostEvenDistribution(201, 400, true);
                testCases[CreatureConstants.Dragon_White_MatureAdult][AgeConstants.Categories.Maximum] = "401";
                testCases[CreatureConstants.Dragon_White_Old][AgeConstants.Categories.Dragon.Old] = RollHelper.GetRollWithMostEvenDistribution(401, 600, true);
                testCases[CreatureConstants.Dragon_White_Old][AgeConstants.Categories.Maximum] = "601";
                testCases[CreatureConstants.Dragon_White_VeryOld][AgeConstants.Categories.Dragon.VeryOld] = RollHelper.GetRollWithMostEvenDistribution(601, 800, true);
                testCases[CreatureConstants.Dragon_White_VeryOld][AgeConstants.Categories.Maximum] = "801";
                testCases[CreatureConstants.Dragon_White_Ancient][AgeConstants.Categories.Dragon.Ancient] = RollHelper.GetRollWithMostEvenDistribution(801, 1000, true);
                testCases[CreatureConstants.Dragon_White_Ancient][AgeConstants.Categories.Maximum] = "1001";
                testCases[CreatureConstants.Dragon_White_Wyrm][AgeConstants.Categories.Dragon.Wyrm] = RollHelper.GetRollWithMostEvenDistribution(1001, 1200, true);
                testCases[CreatureConstants.Dragon_White_Wyrm][AgeConstants.Categories.Maximum] = "1201";
                testCases[CreatureConstants.Dragon_White_GreatWyrm][AgeConstants.Categories.Dragon.GreatWyrm] = RollHelper.GetRollWithMostEvenDistribution(1201, 2100, true);
                testCases[CreatureConstants.Dragon_White_GreatWyrm][AgeConstants.Categories.Maximum] = "2101";
                //Source: https://www.worldanvil.com/w/faerun-tatortotzke/a/dragon-turtle-article
                testCases[CreatureConstants.DragonTurtle][AgeConstants.Categories.Adulthood] = tenKRoll;
                testCases[CreatureConstants.DragonTurtle][AgeConstants.Categories.Maximum] = "10000+10d100";
                //INFO: Can't find anything on aging, other than 150 years been exceptionally/magically old. So, making it up
                testCases[CreatureConstants.Dragonne][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(18, 44, true);
                testCases[CreatureConstants.Dragonne][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(45, 67, true);
                testCases[CreatureConstants.Dragonne][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(68, 89, true);
                testCases[CreatureConstants.Dragonne][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(90, 135, true);
                testCases[CreatureConstants.Dragonne][AgeConstants.Categories.Maximum] = RollHelper.GetRollWithFewestDice(90, 135);
                testCases[CreatureConstants.Dretch][AgeConstants.Categories.Adulthood] = outsiderAgeRoll;
                testCases[CreatureConstants.Dretch][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                //Source: https://www.worldanvil.com/w/faerun-tatortotzke/a/drider-species
                testCases[CreatureConstants.Drider][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(150, 374, true);
                testCases[CreatureConstants.Drider][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(375, 562, true);
                testCases[CreatureConstants.Drider][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(563, 749, true);
                testCases[CreatureConstants.Drider][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(750, 1125, true);
                testCases[CreatureConstants.Drider][AgeConstants.Categories.Maximum] = RollHelper.GetRollWithFewestDice(750, 1125);
                //Soruce: https://www.worldanvil.com/w/faerun-tatortotzke/a/dryad-species
                testCases[CreatureConstants.Dryad][AgeConstants.Categories.Adulthood] = feyAgeRoll;
                testCases[CreatureConstants.Dryad][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                //Source: https://www.d20srd.org/srd/description.htm#vitalStatistics
                testCases[CreatureConstants.Dwarf_Deep][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(45, 139, true);
                testCases[CreatureConstants.Dwarf_Deep][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(140, 186, true);
                testCases[CreatureConstants.Dwarf_Deep][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(187, 279, true);
                testCases[CreatureConstants.Dwarf_Deep][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(280, 480, true);
                testCases[CreatureConstants.Dwarf_Deep][AgeConstants.Categories.Maximum] = "280+2d100";
                testCases[CreatureConstants.Dwarf_Duergar][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(40, 149, true);
                testCases[CreatureConstants.Dwarf_Duergar][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(150, 199, true);
                testCases[CreatureConstants.Dwarf_Duergar][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(200, 299, true);
                testCases[CreatureConstants.Dwarf_Duergar][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(300, 500, true);
                testCases[CreatureConstants.Dwarf_Duergar][AgeConstants.Categories.Maximum] = "300+2d100";
                testCases[CreatureConstants.Dwarf_Hill][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(40, 124, true);
                testCases[CreatureConstants.Dwarf_Hill][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(125, 187, true);
                testCases[CreatureConstants.Dwarf_Hill][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(188, 249, true);
                testCases[CreatureConstants.Dwarf_Hill][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(250, 450, true);
                testCases[CreatureConstants.Dwarf_Hill][AgeConstants.Categories.Maximum] = "250+2d100";
                testCases[CreatureConstants.Dwarf_Mountain][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(40, 124, true);
                testCases[CreatureConstants.Dwarf_Mountain][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(125, 187, true);
                testCases[CreatureConstants.Dwarf_Mountain][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(188, 249, true);
                testCases[CreatureConstants.Dwarf_Mountain][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(250, 450, true);
                testCases[CreatureConstants.Dwarf_Mountain][AgeConstants.Categories.Maximum] = "250+2d100";
                //Source: https://www.dimensions.com/element/bald-eagle-haliaeetus-leucocephalus (maximum)
                //http://www.swbemc.org/plummage.html (adulthood)
                testCases[CreatureConstants.Eagle][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(5, 9, true);
                testCases[CreatureConstants.Eagle][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(10, 14, true);
                testCases[CreatureConstants.Eagle][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(15, 19, true);
                testCases[CreatureConstants.Eagle][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(20, 30, true);
                testCases[CreatureConstants.Eagle][AgeConstants.Categories.Maximum] = "20+1d10";
                testCases[CreatureConstants.Eagle_Giant][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(5, 9, true);
                testCases[CreatureConstants.Eagle_Giant][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(10, 14, true);
                testCases[CreatureConstants.Eagle_Giant][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(15, 19, true);
                testCases[CreatureConstants.Eagle_Giant][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(20, 30, true);
                testCases[CreatureConstants.Eagle_Giant][AgeConstants.Categories.Maximum] = "20+1d10";
                testCases[CreatureConstants.Efreeti][AgeConstants.Categories.Adulthood] = outsiderAgeRoll;
                testCases[CreatureConstants.Efreeti][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                //Source: https://jurassicworld-evolution.fandom.com/wiki/Elasmosaurus
                testCases[CreatureConstants.Elasmosaurus][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(16, 39, true);
                testCases[CreatureConstants.Elasmosaurus][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(40, 59, true);
                testCases[CreatureConstants.Elasmosaurus][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(60, 79, true);
                testCases[CreatureConstants.Elasmosaurus][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(80, 120, true);
                testCases[CreatureConstants.Elasmosaurus][AgeConstants.Categories.Maximum] = "80+2d20";
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
                testCases[CreatureConstants.Elephant][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(10, 29, true);
                testCases[CreatureConstants.Elephant][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(30, 44, true);
                testCases[CreatureConstants.Elephant][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(45, 59, true);
                testCases[CreatureConstants.Elephant][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(60, 75, true);
                testCases[CreatureConstants.Elephant][AgeConstants.Categories.Maximum] = "59+2d8";
                //Source: https://www.d20srd.org/srd/description.htm#vitalStatistics
                testCases[CreatureConstants.Elf_Aquatic][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(110, 174, true);
                testCases[CreatureConstants.Elf_Aquatic][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(175, 262, true);
                testCases[CreatureConstants.Elf_Aquatic][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(263, 349, true);
                testCases[CreatureConstants.Elf_Aquatic][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(350, 750, true);
                testCases[CreatureConstants.Elf_Aquatic][AgeConstants.Categories.Maximum] = "350+4d100";
                testCases[CreatureConstants.Elf_Drow][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(110, 174, true);
                testCases[CreatureConstants.Elf_Drow][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(175, 262, true);
                testCases[CreatureConstants.Elf_Drow][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(263, 349, true);
                testCases[CreatureConstants.Elf_Drow][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(350, 750, true);
                testCases[CreatureConstants.Elf_Drow][AgeConstants.Categories.Maximum] = "350+4d100";
                testCases[CreatureConstants.Elf_Gray][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(110, 174, true);
                testCases[CreatureConstants.Elf_Gray][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(175, 262, true);
                testCases[CreatureConstants.Elf_Gray][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(263, 349, true);
                testCases[CreatureConstants.Elf_Gray][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(350, 750, true);
                testCases[CreatureConstants.Elf_Gray][AgeConstants.Categories.Maximum] = "350+4d100";
                testCases[CreatureConstants.Elf_Half][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(20, 61, true);
                testCases[CreatureConstants.Elf_Half][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(62, 92, true);
                testCases[CreatureConstants.Elf_Half][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(93, 124, true);
                testCases[CreatureConstants.Elf_Half][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(125, 185, true);
                testCases[CreatureConstants.Elf_Half][AgeConstants.Categories.Maximum] = "125+3d20";
                testCases[CreatureConstants.Elf_High][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(110, 174, true);
                testCases[CreatureConstants.Elf_High][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(175, 262, true);
                testCases[CreatureConstants.Elf_High][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(263, 349, true);
                testCases[CreatureConstants.Elf_High][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(350, 750, true);
                testCases[CreatureConstants.Elf_High][AgeConstants.Categories.Maximum] = "350+4d100";
                testCases[CreatureConstants.Elf_Wild][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(110, 174, true);
                testCases[CreatureConstants.Elf_Wild][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(175, 262, true);
                testCases[CreatureConstants.Elf_Wild][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(263, 349, true);
                testCases[CreatureConstants.Elf_Wild][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(350, 750, true);
                testCases[CreatureConstants.Elf_Wild][AgeConstants.Categories.Maximum] = "350+4d100";
                testCases[CreatureConstants.Elf_Wood][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(110, 174, true);
                testCases[CreatureConstants.Elf_Wood][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(175, 262, true);
                testCases[CreatureConstants.Elf_Wood][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(263, 349, true);
                testCases[CreatureConstants.Elf_Wood][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(350, 750, true);
                testCases[CreatureConstants.Elf_Wood][AgeConstants.Categories.Maximum] = "350+4d100";
                testCases[CreatureConstants.Erinyes][AgeConstants.Categories.Adulthood] = outsiderAgeRoll;
                testCases[CreatureConstants.Erinyes][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                //INFO: Can't find anything on lifespan. So, making it up
                testCases[CreatureConstants.EtherealFilcher][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(10, 24, true);
                testCases[CreatureConstants.EtherealFilcher][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(25, 37, true);
                testCases[CreatureConstants.EtherealFilcher][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(38, 49, true);
                testCases[CreatureConstants.EtherealFilcher][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(50, 75, true);
                testCases[CreatureConstants.EtherealFilcher][AgeConstants.Categories.Maximum] = RollHelper.GetRollWithFewestDice(50, 75);
                //INFO: Can't find anything on lifespan. So, making it up
                testCases[CreatureConstants.EtherealMarauder][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(10, 24, true);
                testCases[CreatureConstants.EtherealMarauder][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(25, 37, true);
                testCases[CreatureConstants.EtherealMarauder][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(38, 49, true);
                testCases[CreatureConstants.EtherealMarauder][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(50, 75, true);
                testCases[CreatureConstants.EtherealMarauder][AgeConstants.Categories.Maximum] = RollHelper.GetRollWithFewestDice(50, 75);
                //INFO: https://syrikdarkenedskies.obsidianportal.com/wikis/ettercap-race
                testCases[CreatureConstants.Ettercap][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(6, 13, true);
                testCases[CreatureConstants.Ettercap][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(14, 21, true);
                testCases[CreatureConstants.Ettercap][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(22, 31, true);
                testCases[CreatureConstants.Ettercap][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(32, 38, true);
                testCases[CreatureConstants.Ettercap][AgeConstants.Categories.Maximum] = "32+1d6";
                //Source: https://forgottenrealms.fandom.com/wiki/Ettin
                testCases[CreatureConstants.Ettin][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(1, 37, true);
                testCases[CreatureConstants.Ettin][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(38, 56, true);
                testCases[CreatureConstants.Ettin][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(57, 74, true);
                testCases[CreatureConstants.Ettin][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(75, 115, true);
                testCases[CreatureConstants.Ettin][AgeConstants.Categories.Maximum] = "75+2d20";
                //Source: https://beetleidentifications.com/fire-beetle/
                testCases[CreatureConstants.FireBeetle_Giant][AgeConstants.Categories.Adulthood] = "0";
                testCases[CreatureConstants.FireBeetle_Giant][AgeConstants.Categories.MiddleAge] = "0";
                testCases[CreatureConstants.FireBeetle_Giant][AgeConstants.Categories.Old] = "0";
                testCases[CreatureConstants.FireBeetle_Giant][AgeConstants.Categories.Venerable] = "1d2-1";
                testCases[CreatureConstants.FireBeetle_Giant][AgeConstants.Categories.Maximum] = "1d2-1";
                //CAn't find anything, so making it up. 50 years for max. Letting Queens live 10x longer (like ants)
                testCases[CreatureConstants.FormianWorker][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(10, 24, true);
                testCases[CreatureConstants.FormianWorker][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(25, 37, true);
                testCases[CreatureConstants.FormianWorker][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(38, 49, true);
                testCases[CreatureConstants.FormianWorker][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(50, 75, true);
                testCases[CreatureConstants.FormianWorker][AgeConstants.Categories.Maximum] = RollHelper.GetRollWithFewestDice(50, 75);
                testCases[CreatureConstants.FormianWarrior][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(10, 24, true);
                testCases[CreatureConstants.FormianWarrior][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(25, 37, true);
                testCases[CreatureConstants.FormianWarrior][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(38, 49, true);
                testCases[CreatureConstants.FormianWarrior][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(50, 75, true);
                testCases[CreatureConstants.FormianWarrior][AgeConstants.Categories.Maximum] = RollHelper.GetRollWithFewestDice(50, 75);
                testCases[CreatureConstants.FormianTaskmaster][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(10, 24, true);
                testCases[CreatureConstants.FormianTaskmaster][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(25, 37, true);
                testCases[CreatureConstants.FormianTaskmaster][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(38, 49, true);
                testCases[CreatureConstants.FormianTaskmaster][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(50, 75, true);
                testCases[CreatureConstants.FormianTaskmaster][AgeConstants.Categories.Maximum] = RollHelper.GetRollWithFewestDice(50, 75);
                testCases[CreatureConstants.FormianMyrmarch][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(10, 24, true);
                testCases[CreatureConstants.FormianMyrmarch][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(25, 37, true);
                testCases[CreatureConstants.FormianMyrmarch][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(38, 49, true);
                testCases[CreatureConstants.FormianMyrmarch][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(50, 75, true);
                testCases[CreatureConstants.FormianMyrmarch][AgeConstants.Categories.Maximum] = RollHelper.GetRollWithFewestDice(50, 75);
                testCases[CreatureConstants.FormianQueen][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(100, 249, true);
                testCases[CreatureConstants.FormianQueen][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(250, 374, true);
                testCases[CreatureConstants.FormianQueen][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(375, 499, true);
                testCases[CreatureConstants.FormianQueen][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(500, 750, true);
                testCases[CreatureConstants.FormianQueen][AgeConstants.Categories.Maximum] = RollHelper.GetRollWithFewestDice(500, 750);
                //Source: https://forgottenrealms.fandom.com/wiki/Frost_worm#Ecology
                //Only know when they reach maturity (3-5 years), nothing else, so making it up
                testCases[CreatureConstants.FrostWorm][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(3, 12, true);
                testCases[CreatureConstants.FrostWorm][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(13, 18, true);
                testCases[CreatureConstants.FrostWorm][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(19, 24, true);
                testCases[CreatureConstants.FrostWorm][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(25, 38, true);
                testCases[CreatureConstants.FrostWorm][AgeConstants.Categories.Maximum] = RollHelper.GetRollWithFewestDice(25, 38);
                //Source: https://forgottenrealms.fandom.com/wiki/Gargoyle
                testCases[CreatureConstants.Gargoyle][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(10, 24, true);
                testCases[CreatureConstants.Gargoyle][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(25, 37, true);
                testCases[CreatureConstants.Gargoyle][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(38, 49, true);
                testCases[CreatureConstants.Gargoyle][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(50, 75, true);
                testCases[CreatureConstants.Gargoyle][AgeConstants.Categories.Maximum] = RollHelper.GetRollWithFewestDice(50, 75);
                testCases[CreatureConstants.Gargoyle_Kapoacinth][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(10, 24, true);
                testCases[CreatureConstants.Gargoyle_Kapoacinth][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(25, 37, true);
                testCases[CreatureConstants.Gargoyle_Kapoacinth][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(38, 49, true);
                testCases[CreatureConstants.Gargoyle_Kapoacinth][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(50, 75, true);
                testCases[CreatureConstants.Gargoyle_Kapoacinth][AgeConstants.Categories.Maximum] = RollHelper.GetRollWithFewestDice(50, 75);
                //Source: https://www.worldanvil.com/w/faerun-tatortotzke/a/gelatinous-cube-species
                testCases[CreatureConstants.GelatinousCube][AgeConstants.Categories.Adulthood] = oneKRoll;
                testCases[CreatureConstants.GelatinousCube][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                //Source: https://forgottenrealms.fandom.com/wiki/Ghaele
                testCases[CreatureConstants.Ghaele][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(80, 199, true);
                testCases[CreatureConstants.Ghaele][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(200, 299, true);
                testCases[CreatureConstants.Ghaele][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(300, 399, true);
                testCases[CreatureConstants.Ghaele][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(400, 800, true);
                testCases[CreatureConstants.Ghaele][AgeConstants.Categories.Maximum] = "400+4d100";
                testCases[CreatureConstants.Ghoul][AgeConstants.Categories.Undead] = undeadAgeRoll;
                testCases[CreatureConstants.Ghoul][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Ghoul_Ghast][AgeConstants.Categories.Undead] = undeadAgeRoll;
                testCases[CreatureConstants.Ghoul_Ghast][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Ghoul_Lacedon][AgeConstants.Categories.Undead] = undeadAgeRoll;
                testCases[CreatureConstants.Ghoul_Lacedon][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                //Source: https://forgottenrealms.fandom.com/wiki/Cloud_giant
                testCases[CreatureConstants.Giant_Cloud][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(80, 199, true);
                testCases[CreatureConstants.Giant_Cloud][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(200, 299, true);
                testCases[CreatureConstants.Giant_Cloud][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(300, 399, true);
                testCases[CreatureConstants.Giant_Cloud][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(400, 600, true);
                testCases[CreatureConstants.Giant_Cloud][AgeConstants.Categories.Maximum] = "400+2d100";
                //Source: https://forgottenrealms.fandom.com/wiki/Fire_giant
                testCases[CreatureConstants.Giant_Fire][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(70, 174, true);
                testCases[CreatureConstants.Giant_Fire][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(175, 262, true);
                testCases[CreatureConstants.Giant_Fire][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(263, 349, true);
                testCases[CreatureConstants.Giant_Fire][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(350, 550, true);
                testCases[CreatureConstants.Giant_Fire][AgeConstants.Categories.Maximum] = "350+2d100";
                //Source: https://forgottenrealms.fandom.com/wiki/Frost_giant
                testCases[CreatureConstants.Giant_Frost][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(50, 124, true);
                testCases[CreatureConstants.Giant_Frost][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(125, 187, true);
                testCases[CreatureConstants.Giant_Frost][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(188, 249, true);
                testCases[CreatureConstants.Giant_Frost][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(250, 350, true);
                testCases[CreatureConstants.Giant_Frost][AgeConstants.Categories.Maximum] = "250+1d100";
                //Source: https://forgottenrealms.fandom.com/wiki/Hill_giant
                testCases[CreatureConstants.Giant_Hill][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(40, 99, true);
                testCases[CreatureConstants.Giant_Hill][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(100, 149, true);
                testCases[CreatureConstants.Giant_Hill][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(150, 199, true);
                testCases[CreatureConstants.Giant_Hill][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(200, 300, true);
                testCases[CreatureConstants.Giant_Hill][AgeConstants.Categories.Maximum] = "200+1d100";
                //Source: https://forgottenrealms.fandom.com/wiki/Stone_giant
                testCases[CreatureConstants.Giant_Stone][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(160, 399, true);
                testCases[CreatureConstants.Giant_Stone][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(400, 599, true);
                testCases[CreatureConstants.Giant_Stone][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(600, 799, true);
                testCases[CreatureConstants.Giant_Stone][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(800, 1200, true);
                testCases[CreatureConstants.Giant_Stone][AgeConstants.Categories.Maximum] = "800+4d100";
                testCases[CreatureConstants.Giant_Stone_Elder][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(160, 399, true);
                testCases[CreatureConstants.Giant_Stone_Elder][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(400, 599, true);
                testCases[CreatureConstants.Giant_Stone_Elder][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(600, 799, true);
                testCases[CreatureConstants.Giant_Stone_Elder][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(800, 1200, true);
                testCases[CreatureConstants.Giant_Stone_Elder][AgeConstants.Categories.Maximum] = "800+4d100";
                //Source: https://forgottenrealms.fandom.com/wiki/Storm_giant
                testCases[CreatureConstants.Giant_Storm][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(120, 299, true);
                testCases[CreatureConstants.Giant_Storm][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(300, 449, true);
                testCases[CreatureConstants.Giant_Storm][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(450, 599, true);
                testCases[CreatureConstants.Giant_Storm][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(600, 900, true);
                testCases[CreatureConstants.Giant_Storm][AgeConstants.Categories.Maximum] = "600+3d100";
                //Source: https://www.worldanvil.com/w/faerun-tatortotzke/a/gibbering-mouther-species
                testCases[CreatureConstants.GibberingMouther][AgeConstants.Categories.Adulthood] = oneKRoll;
                testCases[CreatureConstants.GibberingMouther][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                //TODO: Pick up from here
                testCases[CreatureConstants.Girallon][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(12, 19, true);
                testCases[CreatureConstants.Girallon][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(20, 24, true);
                testCases[CreatureConstants.Girallon][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(25, 29, true);
                testCases[CreatureConstants.Girallon][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(30, 35, true);
                testCases[CreatureConstants.Girallon][AgeConstants.Categories.Maximum] = "29+1d6";
                //Source: http://people.wku.edu/charles.plemons/ad&d/races/age.html
                testCases[CreatureConstants.Githyanki][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(30, 124, true);
                testCases[CreatureConstants.Githyanki][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(125, 166, true);
                testCases[CreatureConstants.Githyanki][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(167, 249, true);
                testCases[CreatureConstants.Githyanki][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(250, 350, true);
                testCases[CreatureConstants.Githyanki][AgeConstants.Categories.Maximum] = "250+1d100";
                //Source: http://people.wku.edu/charles.plemons/ad&d/races/age.html
                testCases[CreatureConstants.Githzerai][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(30, 124, true);
                testCases[CreatureConstants.Githzerai][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(125, 166, true);
                testCases[CreatureConstants.Githzerai][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(167, 249, true);
                testCases[CreatureConstants.Githzerai][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(250, 350, true);
                testCases[CreatureConstants.Githzerai][AgeConstants.Categories.Maximum] = "250+1d100";
                testCases[CreatureConstants.Glabrezu][AgeConstants.Categories.Adulthood] = outsiderAgeRoll;
                testCases[CreatureConstants.Glabrezu][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                //Source: http://people.wku.edu/charles.plemons/ad&d/races/age.html
                testCases[CreatureConstants.Gnoll][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(7, 15, true);
                testCases[CreatureConstants.Gnoll][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(16, 21, true);
                testCases[CreatureConstants.Gnoll][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(22, 32, true);
                testCases[CreatureConstants.Gnoll][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(33, 37, true);
                testCases[CreatureConstants.Gnoll][AgeConstants.Categories.Maximum] = "33+1d4";
                //Source: https://www.d20srd.org/srd/description.htm#vitalStatistics
                testCases[CreatureConstants.Gnome_Forest][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(40, 99, true);
                testCases[CreatureConstants.Gnome_Forest][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(100, 149, true);
                testCases[CreatureConstants.Gnome_Forest][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(150, 199, true);
                testCases[CreatureConstants.Gnome_Forest][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(200, 500, true);
                testCases[CreatureConstants.Gnome_Forest][AgeConstants.Categories.Maximum] = "200+3d100";
                testCases[CreatureConstants.Gnome_Rock][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(40, 99, true);
                testCases[CreatureConstants.Gnome_Rock][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(100, 149, true);
                testCases[CreatureConstants.Gnome_Rock][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(150, 199, true);
                testCases[CreatureConstants.Gnome_Rock][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(200, 500, true);
                testCases[CreatureConstants.Gnome_Rock][AgeConstants.Categories.Maximum] = "200+3d100";
                //Source: above, plus https://www.d20srd.org/srd/monsters/gnome.htm
                testCases[CreatureConstants.Gnome_Svirfneblin][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(40, 99, true);
                testCases[CreatureConstants.Gnome_Svirfneblin][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(100, 149, true);
                testCases[CreatureConstants.Gnome_Svirfneblin][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(150, 199, true);
                testCases[CreatureConstants.Gnome_Svirfneblin][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(200, 300, true);
                testCases[CreatureConstants.Gnome_Svirfneblin][AgeConstants.Categories.Maximum] = "200+5d20";
                //Source: http://people.wku.edu/charles.plemons/ad&d/races/age.html
                testCases[CreatureConstants.Goblin][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(12, 19, true);
                testCases[CreatureConstants.Goblin][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(20, 29, true);
                testCases[CreatureConstants.Goblin][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(30, 39, true);
                testCases[CreatureConstants.Goblin][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(40, 60, true);
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
                testCases[CreatureConstants.Gorgon][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(5, 34, true);
                testCases[CreatureConstants.Gorgon][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(35, 49, true);
                testCases[CreatureConstants.Gorgon][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(50, 59, true);
                testCases[CreatureConstants.Gorgon][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(60, 75, true);
                testCases[CreatureConstants.Gorgon][AgeConstants.Categories.Maximum] = "59+2d8";
                testCases[CreatureConstants.GrayOoze][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(1, 100, true);
                testCases[CreatureConstants.GrayOoze][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.GrayRender][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(1, 49, true);
                testCases[CreatureConstants.GrayRender][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(50, 74, true);
                testCases[CreatureConstants.GrayRender][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(75, 99, true);
                testCases[CreatureConstants.GrayRender][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(100, 300, true);
                testCases[CreatureConstants.GrayRender][AgeConstants.Categories.Maximum] = "100+2d100";
                testCases[CreatureConstants.GreenHag][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(40, 1000, true);
                testCases[CreatureConstants.GreenHag][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Grick][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(1, 49, true);
                testCases[CreatureConstants.Grick][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(50, 74, true);
                testCases[CreatureConstants.Grick][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(75, 89, true);
                testCases[CreatureConstants.Grick][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(90, 100, true);
                testCases[CreatureConstants.Grick][AgeConstants.Categories.Maximum] = "90+1d10";
                testCases[CreatureConstants.Griffon][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(1, 25, true);
                testCases[CreatureConstants.Griffon][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(26, 44, true);
                testCases[CreatureConstants.Griffon][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(45, 59, true);
                testCases[CreatureConstants.Griffon][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(60, 70, true);
                testCases[CreatureConstants.Griffon][AgeConstants.Categories.Maximum] = "60+1d10";
                //Source: http://people.wku.edu/charles.plemons/ad&d/races/age.html - from Pixie
                testCases[CreatureConstants.Grig][AgeConstants.Categories.Adulthood] = "100";
                testCases[CreatureConstants.Grig][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(100, 132, true);
                testCases[CreatureConstants.Grig][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(133, 199, true);
                testCases[CreatureConstants.Grig][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(200, 400, true);
                testCases[CreatureConstants.Grig][AgeConstants.Categories.Maximum] = "200+2d100";
                testCases[CreatureConstants.Grig_WithFiddle][AgeConstants.Categories.Adulthood] = "100";
                testCases[CreatureConstants.Grig_WithFiddle][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(100, 132, true);
                testCases[CreatureConstants.Grig_WithFiddle][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(133, 199, true);
                testCases[CreatureConstants.Grig_WithFiddle][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(200, 400, true);
                testCases[CreatureConstants.Grig_WithFiddle][AgeConstants.Categories.Maximum] = "200+2d100";
                testCases[CreatureConstants.Grimlock][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(15, 24, true);
                testCases[CreatureConstants.Grimlock][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(25, 32, true);
                testCases[CreatureConstants.Grimlock][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(33, 49, true);
                testCases[CreatureConstants.Grimlock][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(50, 60, true);
                testCases[CreatureConstants.Grimlock][AgeConstants.Categories.Maximum] = "50+1d10";
                //Source: https://pathfinder.d20srd.org/bestiary3/sphinx.html (adulthood)
                //https://www.belloflostsouls.net/2021/12/dd-monster-spotlight-androsphinx.html (maximum)
                testCases[CreatureConstants.Gynosphinx][AgeConstants.Categories.Adulthood] = $"{tenKRoll}+3";
                testCases[CreatureConstants.Gynosphinx][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                //Source: https://www.d20srd.org/srd/description.htm#vitalStatistics
                testCases[CreatureConstants.Halfling_Deep][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(20, 49, true);
                testCases[CreatureConstants.Halfling_Deep][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(50, 74, true);
                testCases[CreatureConstants.Halfling_Deep][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(75, 99, true);
                testCases[CreatureConstants.Halfling_Deep][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(100, 200, true);
                testCases[CreatureConstants.Halfling_Deep][AgeConstants.Categories.Maximum] = "100+5d20";
                testCases[CreatureConstants.Halfling_Lightfoot][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(20, 49, true);
                testCases[CreatureConstants.Halfling_Lightfoot][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(50, 74, true);
                testCases[CreatureConstants.Halfling_Lightfoot][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(75, 99, true);
                testCases[CreatureConstants.Halfling_Lightfoot][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(100, 200, true);
                testCases[CreatureConstants.Halfling_Lightfoot][AgeConstants.Categories.Maximum] = "100+5d20";
                testCases[CreatureConstants.Halfling_Tallfellow][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(20, 49, true);
                testCases[CreatureConstants.Halfling_Tallfellow][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(50, 74, true);
                testCases[CreatureConstants.Halfling_Tallfellow][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(75, 99, true);
                testCases[CreatureConstants.Halfling_Tallfellow][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(100, 200, true);
                testCases[CreatureConstants.Halfling_Tallfellow][AgeConstants.Categories.Maximum] = "100+5d20";
                testCases[CreatureConstants.Harpy][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(2, 24, true);
                testCases[CreatureConstants.Harpy][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(25, 34, true);
                testCases[CreatureConstants.Harpy][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(35, 42, true);
                testCases[CreatureConstants.Harpy][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(43, 50, true);
                testCases[CreatureConstants.Harpy][AgeConstants.Categories.Maximum] = "42+1d8";
                testCases[CreatureConstants.Hawk][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(3, 7, true);
                testCases[CreatureConstants.Hawk][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(8, 9, true);
                testCases[CreatureConstants.Hawk][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(10, 11, true);
                testCases[CreatureConstants.Hawk][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(12, 20, true);
                testCases[CreatureConstants.Hawk][AgeConstants.Categories.Maximum] = "12+1d8";
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
                testCases[CreatureConstants.Hippogriff][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(15, 34, true);
                testCases[CreatureConstants.Hippogriff][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(35, 52, true);
                testCases[CreatureConstants.Hippogriff][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(53, 69, true);
                testCases[CreatureConstants.Hippogriff][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(70, 190, true);
                testCases[CreatureConstants.Hippogriff][AgeConstants.Categories.Maximum] = "70+6d20";
                //Source: http://people.wku.edu/charles.plemons/ad&d/races/age.html
                testCases[CreatureConstants.Hobgoblin][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(14, 24, true);
                testCases[CreatureConstants.Hobgoblin][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(25, 32, true);
                testCases[CreatureConstants.Hobgoblin][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(33, 49, true);
                testCases[CreatureConstants.Hobgoblin][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(50, 70, true);
                testCases[CreatureConstants.Hobgoblin][AgeConstants.Categories.Maximum] = "50+1d20";
                testCases[CreatureConstants.Homunculus][AgeConstants.Categories.Construct] = "1d100";
                testCases[CreatureConstants.Homunculus][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.HornedDevil_Cornugon][AgeConstants.Categories.Adulthood] = outsiderAgeRoll;
                testCases[CreatureConstants.HornedDevil_Cornugon][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Horse_Heavy][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(5, 19, true);
                testCases[CreatureConstants.Horse_Heavy][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(20, 24, true);
                testCases[CreatureConstants.Horse_Heavy][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(25, 29, true);
                testCases[CreatureConstants.Horse_Heavy][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(30, 35, true);
                testCases[CreatureConstants.Horse_Heavy][AgeConstants.Categories.Maximum] = "30+1d6";
                testCases[CreatureConstants.Horse_Light][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(5, 19, true);
                testCases[CreatureConstants.Horse_Light][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(20, 24, true);
                testCases[CreatureConstants.Horse_Light][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(25, 29, true);
                testCases[CreatureConstants.Horse_Light][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(30, 35, true);
                testCases[CreatureConstants.Horse_Light][AgeConstants.Categories.Maximum] = "30+1d6";
                testCases[CreatureConstants.Horse_Heavy_War][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(5, 19, true);
                testCases[CreatureConstants.Horse_Heavy_War][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(20, 24, true);
                testCases[CreatureConstants.Horse_Heavy_War][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(25, 29, true);
                testCases[CreatureConstants.Horse_Heavy_War][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(30, 35, true);
                testCases[CreatureConstants.Horse_Heavy_War][AgeConstants.Categories.Maximum] = "30+1d6";
                testCases[CreatureConstants.Horse_Light_War][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(5, 19, true);
                testCases[CreatureConstants.Horse_Light_War][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(20, 24, true);
                testCases[CreatureConstants.Horse_Light_War][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(25, 29, true);
                testCases[CreatureConstants.Horse_Light_War][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(30, 35, true);
                testCases[CreatureConstants.Horse_Light_War][AgeConstants.Categories.Maximum] = "30+1d6";
                testCases[CreatureConstants.HoundArchon][AgeConstants.Categories.Adulthood] = outsiderAgeRoll;
                testCases[CreatureConstants.HoundArchon][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                //INFO: Can't find anything. So, making it up
                testCases[CreatureConstants.Howler][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(3, 20, true);
                testCases[CreatureConstants.Howler][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(21, 30, true);
                testCases[CreatureConstants.Howler][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(31, 40, true);
                testCases[CreatureConstants.Howler][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(41, 50, true);
                testCases[CreatureConstants.Howler][AgeConstants.Categories.Maximum] = "40+1d10";
                //Source https://www.d20srd.org/srd/description.htm#vitalStatistics
                testCases[CreatureConstants.Human][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(15, 34, true);
                testCases[CreatureConstants.Human][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(35, 52, true);
                testCases[CreatureConstants.Human][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(53, 69, true);
                testCases[CreatureConstants.Human][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(70, 110, true);
                testCases[CreatureConstants.Human][AgeConstants.Categories.Maximum] = "70+2d20";
                //Source: https://www.reddit.com/r/DnDBehindTheScreen/comments/63zx1m/hydra/
                testCases[CreatureConstants.Hydra_5Heads][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(25, 25 * 20, true);
                testCases[CreatureConstants.Hydra_5Heads][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Hydra_6Heads][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(501, 1000, true);
                testCases[CreatureConstants.Hydra_6Heads][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Hydra_7Heads][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(1001, 2000, true);
                testCases[CreatureConstants.Hydra_7Heads][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Hydra_8Heads][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(2001, 4000, true);
                testCases[CreatureConstants.Hydra_8Heads][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Hydra_9Heads][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(4001, 6000, true);
                testCases[CreatureConstants.Hydra_9Heads][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Hydra_10Heads][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(6001, 8000, true);
                testCases[CreatureConstants.Hydra_10Heads][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Hydra_11Heads][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(8001, 10_000, true);
                testCases[CreatureConstants.Hydra_11Heads][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Hydra_12Heads][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(10_000, 12_000, true);
                testCases[CreatureConstants.Hydra_12Heads][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Hyena][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(3, 10, true);
                testCases[CreatureConstants.Hyena][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(11, 15, true);
                testCases[CreatureConstants.Hyena][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(16, 19, true);
                testCases[CreatureConstants.Hyena][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(20, 40, true);
                testCases[CreatureConstants.Hyena][AgeConstants.Categories.Maximum] = "20+2d10";
                testCases[CreatureConstants.IceDevil_Gelugon][AgeConstants.Categories.Adulthood] = outsiderAgeRoll;
                testCases[CreatureConstants.IceDevil_Gelugon][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Imp][AgeConstants.Categories.Adulthood] = outsiderAgeRoll;
                testCases[CreatureConstants.Imp][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.InvisibleStalker][AgeConstants.Categories.Adulthood] = outsiderAgeRoll;
                testCases[CreatureConstants.InvisibleStalker][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Janni][AgeConstants.Categories.Adulthood] = outsiderAgeRoll;
                testCases[CreatureConstants.Janni][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                //Source: http://people.wku.edu/charles.plemons/ad&d/races/age.html
                testCases[CreatureConstants.Kobold][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(12, 47, true);
                testCases[CreatureConstants.Kobold][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(48, 61, true);
                testCases[CreatureConstants.Kobold][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(62, 94, true);
                testCases[CreatureConstants.Kobold][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(95, 135, true);
                testCases[CreatureConstants.Kobold][AgeConstants.Categories.Maximum] = "95+2d20";
                testCases[CreatureConstants.Kolyarut][AgeConstants.Categories.Construct] = outsiderAgeRoll;
                testCases[CreatureConstants.Kolyarut][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Kraken][AgeConstants.Categories.Adulthood] = "1000d100";
                testCases[CreatureConstants.Kraken][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Krenshar][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(4, 10, true);
                testCases[CreatureConstants.Krenshar][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(11, 12, true);
                testCases[CreatureConstants.Krenshar][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(13, 14, true);
                testCases[CreatureConstants.Krenshar][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(15, 20, true);
                testCases[CreatureConstants.Krenshar][AgeConstants.Categories.Maximum] = "14+1d6";
                //Source: https://www.worldanvil.com/w/verum-arcadum/a/kuo-toa-species
                testCases[CreatureConstants.KuoToa][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(6, 199, true);
                testCases[CreatureConstants.KuoToa][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(200, 449, true);
                testCases[CreatureConstants.KuoToa][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(450, 699, true);
                testCases[CreatureConstants.KuoToa][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(700, 1000, true);
                testCases[CreatureConstants.KuoToa][AgeConstants.Categories.Maximum] = "700+3d100";
                testCases[CreatureConstants.Lamia][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(20, 59, true);
                testCases[CreatureConstants.Lamia][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(60, 99, true);
                testCases[CreatureConstants.Lamia][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(100, 134, true);
                testCases[CreatureConstants.Lamia][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(135, 165, true);
                testCases[CreatureConstants.Lamia][AgeConstants.Categories.Maximum] = "135+3d10";
                //HACK: Cn't find anything about ages. So, making it up
                testCases[CreatureConstants.Lammasu][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(20, 79, true);
                testCases[CreatureConstants.Lammasu][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(80, 119, true);
                testCases[CreatureConstants.Lammasu][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(120, 159, true);
                testCases[CreatureConstants.Lammasu][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(160, 200, true);
                testCases[CreatureConstants.Lammasu][AgeConstants.Categories.Maximum] = "160+2d20";
                testCases[CreatureConstants.LanternArchon][AgeConstants.Categories.Adulthood] = outsiderAgeRoll;
                testCases[CreatureConstants.LanternArchon][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Lemure][AgeConstants.Categories.Adulthood] = outsiderAgeRoll;
                testCases[CreatureConstants.Lemure][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Leonal][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(15, 34, true);
                testCases[CreatureConstants.Leonal][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(35, 52, true);
                testCases[CreatureConstants.Leonal][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(53, 69, true);
                testCases[CreatureConstants.Leonal][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(70, 110, true);
                testCases[CreatureConstants.Leonal][AgeConstants.Categories.Maximum] = "70+2d20";
                testCases[CreatureConstants.Leopard][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(2, 7, true);
                testCases[CreatureConstants.Leopard][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(8, 9, true);
                testCases[CreatureConstants.Leopard][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(10, 11, true);
                testCases[CreatureConstants.Leopard][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(12, 24, true);
                testCases[CreatureConstants.Leopard][AgeConstants.Categories.Maximum] = "12+2d6";
                testCases[CreatureConstants.Lillend][AgeConstants.Categories.Adulthood] = oneHundredKRoll;
                testCases[CreatureConstants.Lillend][AgeConstants.Categories.Maximum] = $"{oneMRoll}+50000";
                testCases[CreatureConstants.Lion][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(4, 7, true);
                testCases[CreatureConstants.Lion][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(8, 9, true);
                testCases[CreatureConstants.Lion][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(10, 11, true);
                testCases[CreatureConstants.Lion][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(12, 16, true);
                testCases[CreatureConstants.Lion][AgeConstants.Categories.Maximum] = "12+1d4";
                testCases[CreatureConstants.Lion_Dire][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(4, 7, true);
                testCases[CreatureConstants.Lion_Dire][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(8, 9, true);
                testCases[CreatureConstants.Lion_Dire][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(10, 11, true);
                testCases[CreatureConstants.Lion_Dire][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(12, 16, true);
                testCases[CreatureConstants.Lion_Dire][AgeConstants.Categories.Maximum] = "12+1d4";
                testCases[CreatureConstants.Lizard][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(2, 9, true);
                testCases[CreatureConstants.Lizard][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(10, 12, true);
                testCases[CreatureConstants.Lizard][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(13, 14, true);
                testCases[CreatureConstants.Lizard][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(15, 30, true);
                testCases[CreatureConstants.Lizard][AgeConstants.Categories.Maximum] = "14+2d8";
                //Source: http://people.wku.edu/charles.plemons/ad&d/races/age.html
                testCases[CreatureConstants.Lizardfolk][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(15, 54, true);
                testCases[CreatureConstants.Lizardfolk][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(55, 72, true);
                testCases[CreatureConstants.Lizardfolk][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(73, 109, true);
                testCases[CreatureConstants.Lizardfolk][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(110, 130, true);
                testCases[CreatureConstants.Lizardfolk][AgeConstants.Categories.Maximum] = "110+2d10";
                testCases[CreatureConstants.Lizard_Monitor][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(5, 11, true);
                testCases[CreatureConstants.Lizard_Monitor][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(12, 14, true);
                testCases[CreatureConstants.Lizard_Monitor][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(15, 17, true);
                testCases[CreatureConstants.Lizard_Monitor][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(18, 22, true);
                testCases[CreatureConstants.Lizard_Monitor][AgeConstants.Categories.Maximum] = "18+1d4";
                //Source: http://people.wku.edu/charles.plemons/ad&d/races/age.html
                testCases[CreatureConstants.Locathah][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(15, 24, true);
                testCases[CreatureConstants.Locathah][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(25, 32, true);
                testCases[CreatureConstants.Locathah][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(33, 49, true);
                testCases[CreatureConstants.Locathah][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(50, 74, true);
                testCases[CreatureConstants.Locathah][AgeConstants.Categories.Maximum] = "50+2d12";
                testCases[CreatureConstants.Locust_Swarm][AgeConstants.Categories.Swarm] = RollHelper.GetRollWithMostEvenDistribution(0, 1, true);
                testCases[CreatureConstants.Locust_Swarm][AgeConstants.Categories.Maximum] = RollHelper.GetRollWithFewestDice(0, 1);
                testCases[CreatureConstants.Magmin][AgeConstants.Categories.Adulthood] = outsiderAgeRoll;
                testCases[CreatureConstants.Magmin][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.MantaRay][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(10, 29, true);
                testCases[CreatureConstants.MantaRay][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(30, 39, true);
                testCases[CreatureConstants.MantaRay][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(40, 44, true);
                testCases[CreatureConstants.MantaRay][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(45, 50, true);
                testCases[CreatureConstants.MantaRay][AgeConstants.Categories.Maximum] = "44+1d6";
                testCases[CreatureConstants.Manticore][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(5, 11, true);
                testCases[CreatureConstants.Manticore][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(12, 14, true);
                testCases[CreatureConstants.Manticore][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(15, 17, true);
                testCases[CreatureConstants.Manticore][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(18, 22, true);
                testCases[CreatureConstants.Manticore][AgeConstants.Categories.Maximum] = "18+1d4";
                testCases[CreatureConstants.Marilith][AgeConstants.Categories.Adulthood] = outsiderAgeRoll;
                testCases[CreatureConstants.Marilith][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Marut][AgeConstants.Categories.Construct] = outsiderAgeRoll;
                testCases[CreatureConstants.Marut][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Medusa][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(5, 49, true);
                testCases[CreatureConstants.Medusa][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(50, 75, true);
                testCases[CreatureConstants.Medusa][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(76, 99, true);
                testCases[CreatureConstants.Medusa][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(100, 500, true);
                testCases[CreatureConstants.Medusa][AgeConstants.Categories.Maximum] = "100+4d100";
                testCases[CreatureConstants.Megaraptor][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(2, 9, true);
                testCases[CreatureConstants.Megaraptor][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(10, 12, true);
                testCases[CreatureConstants.Megaraptor][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(13, 14, true);
                testCases[CreatureConstants.Megaraptor][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(15, 20, true);
                testCases[CreatureConstants.Megaraptor][AgeConstants.Categories.Maximum] = "14+1d6";
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
                testCases[CreatureConstants.Merfolk][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(15, 44, true);
                testCases[CreatureConstants.Merfolk][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(45, 59, true);
                testCases[CreatureConstants.Merfolk][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(60, 89, true);
                testCases[CreatureConstants.Merfolk][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(90, 130, true);
                testCases[CreatureConstants.Merfolk][AgeConstants.Categories.Maximum] = "90+2d20";
                testCases[CreatureConstants.Mimic][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(2, 34, true);
                testCases[CreatureConstants.Mimic][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(35, 52, true);
                testCases[CreatureConstants.Mimic][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(53, 69, true);
                testCases[CreatureConstants.Mimic][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(70, 100, true);
                testCases[CreatureConstants.Mimic][AgeConstants.Categories.Maximum] = "70+3d10";
                testCases[CreatureConstants.MindFlayer][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(10, 34, true);
                testCases[CreatureConstants.MindFlayer][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(35, 52, true);
                testCases[CreatureConstants.MindFlayer][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(53, 109, true);
                testCases[CreatureConstants.MindFlayer][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(110, 140, true);
                testCases[CreatureConstants.MindFlayer][AgeConstants.Categories.Maximum] = "110+3d10";
                //Source: http://people.wku.edu/charles.plemons/ad&d/races/age.html
                testCases[CreatureConstants.Minotaur][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(12, 74, true);
                testCases[CreatureConstants.Minotaur][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(75, 99, true);
                testCases[CreatureConstants.Minotaur][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(100, 149, true);
                testCases[CreatureConstants.Minotaur][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(150, 250, true);
                testCases[CreatureConstants.Minotaur][AgeConstants.Categories.Maximum] = "150+1d100";
                testCases[CreatureConstants.Mohrg][AgeConstants.Categories.Undead] = undeadAgeRoll;
                testCases[CreatureConstants.Mohrg][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Monkey][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(5, 10, true);
                testCases[CreatureConstants.Monkey][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(11, 12, true);
                testCases[CreatureConstants.Monkey][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(13, 14, true);
                testCases[CreatureConstants.Monkey][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(15, 50, true);
                testCases[CreatureConstants.Monkey][AgeConstants.Categories.Maximum] = "14+3d12";
                testCases[CreatureConstants.Mule][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(6, 14, true);
                testCases[CreatureConstants.Mule][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(15, 20, true);
                testCases[CreatureConstants.Mule][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(21, 29, true);
                testCases[CreatureConstants.Mule][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(30, 50, true);
                testCases[CreatureConstants.Mule][AgeConstants.Categories.Maximum] = "30+2d10";
                testCases[CreatureConstants.Mummy][AgeConstants.Categories.Undead] = undeadAgeRoll;
                testCases[CreatureConstants.Mummy][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.Naga_Dark][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(20, 499, true);
                testCases[CreatureConstants.Naga_Dark][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(500, 699, true);
                testCases[CreatureConstants.Naga_Dark][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(700, 899, true);
                testCases[CreatureConstants.Naga_Dark][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(900, 1100, true);
                testCases[CreatureConstants.Naga_Dark][AgeConstants.Categories.Maximum] = "900+2d100";
                testCases[CreatureConstants.Naga_Guardian][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(20, 499, true);
                testCases[CreatureConstants.Naga_Guardian][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(500, 699, true);
                testCases[CreatureConstants.Naga_Guardian][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(700, 899, true);
                testCases[CreatureConstants.Naga_Guardian][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(900, 1100, true);
                testCases[CreatureConstants.Naga_Guardian][AgeConstants.Categories.Maximum] = "900+2d100";
                testCases[CreatureConstants.Naga_Spirit][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(20, 499, true);
                testCases[CreatureConstants.Naga_Spirit][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(500, 699, true);
                testCases[CreatureConstants.Naga_Spirit][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(700, 899, true);
                testCases[CreatureConstants.Naga_Spirit][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(900, 1100, true);
                testCases[CreatureConstants.Naga_Spirit][AgeConstants.Categories.Maximum] = "900+2d100";
                testCases[CreatureConstants.Naga_Water][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(20, 499, true);
                testCases[CreatureConstants.Naga_Water][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(500, 699, true);
                testCases[CreatureConstants.Naga_Water][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(700, 899, true);
                testCases[CreatureConstants.Naga_Water][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(900, 1100, true);
                testCases[CreatureConstants.Naga_Water][AgeConstants.Categories.Maximum] = "900+2d100";
                testCases[CreatureConstants.Nalfeshnee][AgeConstants.Categories.Adulthood] = outsiderAgeRoll;
                testCases[CreatureConstants.Nalfeshnee][AgeConstants.Categories.Maximum] = AgeConstants.Ageless.ToString();
                testCases[CreatureConstants.NightHag][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(15, 1000, true);
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
                testCases[CreatureConstants.Ogre][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(90, 110, true);
                testCases[CreatureConstants.Ogre][AgeConstants.Categories.Maximum] = "90+2d20";
                testCases[CreatureConstants.Ogre_Merrow][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(15, 44, true);
                testCases[CreatureConstants.Ogre_Merrow][AgeConstants.Categories.MiddleAge] = RollHelper.GetRollWithMostEvenDistribution(45, 59, true);
                testCases[CreatureConstants.Ogre_Merrow][AgeConstants.Categories.Old] = RollHelper.GetRollWithMostEvenDistribution(60, 89, true);
                testCases[CreatureConstants.Ogre_Merrow][AgeConstants.Categories.Venerable] = RollHelper.GetRollWithMostEvenDistribution(90, 110, true);
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
                testCases[CreatureConstants.SeaHag][AgeConstants.Categories.Adulthood] = RollHelper.GetRollWithMostEvenDistribution(40, 800, true);
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

        private static string GetAdulthoodRoll(int venerable, int adulthood = -1)
        {
            if (adulthood == -1)
                adulthood = venerable / 5;

            var middleAge = venerable / 2;
            return GetRoll(adulthood, middleAge - 1);
        }

        private static string GetMiddleAgeRoll(int venerable, int old = -1)
        {
            if (old == -1)
                old = venerable * 3 / 4;

            var middleAge = venerable / 2;
            return GetRoll(middleAge, old - 1);
        }

        private static string GetOldRoll(int venerable, int old = -1)
        {
            if (old == -1)
                old = venerable * 3 / 4;

            return GetRoll(old, venerable - 1);
        }

        private static string GetVenerableRoll(int venerable, int max = -1)
        {
            if (max == -1)
                max = venerable * 3 / 2;

            return GetRoll(venerable, max);
        }

        private static string GetRoll(int lower, int upper) => RollHelper.GetRollWithMostEvenDistribution(lower, upper, true);

        private static string GetMaximumRoll(int venerable, int max = -1)
        {
            if (max == -1)
                max = venerable * 3 / 2;

            return RollHelper.GetRollWithFewestDice(venerable + 1, max);
        }
    }
}