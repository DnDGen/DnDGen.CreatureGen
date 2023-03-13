using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Selectors.Collections;
using DnDGen.CreatureGen.Selectors.Helpers;
using DnDGen.CreatureGen.Tables;
using DnDGen.RollGen;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Integration.Tables.Creatures
{
    [TestFixture]
    public class AdvancementsTests : TypesAndAmountsTests
    {
        private Dice dice;
        private ICreatureDataSelector creatureDataSelector;

        protected override string tableName => TableNameConstants.TypeAndAmount.Advancements;

        [SetUp]
        public void Setup()
        {
            dice = GetNewInstanceOf<Dice>();
            creatureDataSelector = GetNewInstanceOf<ICreatureDataSelector>();
        }

        [Test]
        public void AdvancementsNames()
        {
            var creatures = CreatureConstants.GetAll();
            var creatureTypes = CreatureConstants.Types.GetAll();

            var names = creatures.Union(creatureTypes);

            AssertCollectionNames(names);
        }

        [TestCaseSource(nameof(AdvancementsTestData))]
        public void Advancement(string creature, Dictionary<string, string[]> rollAndData)
        {
            var typesAndAmounts = new Dictionary<string, string>();

            foreach (var kvp in rollAndData)
            {
                var roll = kvp.Key;
                var data = kvp.Value;

                var type = string.Join(",", data);

                typesAndAmounts[type] = roll;
            }

            AssertTypesAndAmounts(creature, typesAndAmounts);

            //Hit Dice Only Increases
            if (!table[creature].Any())
                Assert.Pass($"No advancements to test for {creature}");

            var rolls = table[creature].Select(a => TypeAndAmountHelper.Parse(a)[1]);

            foreach (var roll in rolls)
            {
                var minimum = dice.Roll(roll).AsPotentialMinimum();
                Assert.That(minimum, Is.Positive);

                var maximum = dice.Roll(roll).AsPotentialMaximum();
                var range = Enumerable.Range(minimum, maximum - minimum + 1);

                foreach (var otherRoll in rolls.Except(new[] { roll }))
                {
                    var otherMinimum = dice.Roll(otherRoll).AsPotentialMinimum();
                    var otherMaximum = dice.Roll(otherRoll).AsPotentialMaximum();
                    var otherRange = Enumerable.Range(otherMinimum, otherMaximum - otherMinimum + 1);

                    Assert.That(range.Intersect(otherRange), Is.Empty, $"{roll} vs {otherRoll}");
                }
            }

            //Size Only Increases, Are Distinct, Minimum Increases
            var sizes = table[creature].Select(a => TypeAndAmountHelper.Parse(a)[0].Split(',')[0]);
            Assert.That(sizes, Is.Unique);

            var creatureData = creatureDataSelector.SelectFor(creature);

            var orderedSizes = SizeConstants.GetOrdered();
            var originalSizeIndex = Array.IndexOf(orderedSizes, creatureData.Size);

            foreach (var size in sizes)
            {
                var sizeIndex = Array.IndexOf(orderedSizes, size);
                Assert.That(sizeIndex, Is.AtLeast(originalSizeIndex), $"{size} >= {creatureData.Size}");
            }

            foreach (var advancement in table[creature])
            {
                var size = TypeAndAmountHelper.Parse(advancement)[0].Split(',')[0];
                var sizeIndex = Array.IndexOf(orderedSizes, size);

                var roll = TypeAndAmountHelper.Parse(advancement)[1];
                var minimum = dice.Roll(roll).AsPotentialMinimum();

                foreach (var otherAdvancement in table[creature].Except(new[] { advancement }))
                {
                    var otherSize = TypeAndAmountHelper.Parse(otherAdvancement)[0].Split(',')[0];
                    var otherSizeIndex = Array.IndexOf(orderedSizes, otherSize);

                    var otherRoll = TypeAndAmountHelper.Parse(otherAdvancement)[1];
                    var otherMinimum = dice.Roll(otherRoll).AsPotentialMinimum();

                    if (minimum < otherMinimum)
                        Assert.That(sizeIndex, Is.LessThan(otherSizeIndex), $"{size} < {otherSize}");
                    else
                        Assert.That(sizeIndex, Is.GreaterThan(otherSizeIndex), $"{size} > {otherSize}");
                }
            }
        }

        private const string None = "NONE";

        public static IEnumerable AdvancementsTestData
        {
            get
            {
                var testCases = new Dictionary<string, Dictionary<string, string[]>>();
                var creatures = CreatureConstants.GetAll();

                foreach (var creature in creatures)
                {
                    testCases[creature] = new Dictionary<string, string[]>();
                }

                testCases[CreatureConstants.Aasimar][None] = new string[0];
                testCases[CreatureConstants.Aboleth][RollHelper.GetRollWithMostEvenDistribution(8, 9, 16, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.Aboleth][RollHelper.GetRollWithMostEvenDistribution(8, 17, 24, true)] = GetData(SizeConstants.Gargantuan, 20, 15);
                testCases[CreatureConstants.Achaierai][RollHelper.GetRollWithMostEvenDistribution(6, 7, 12, true)] = GetData(SizeConstants.Large, 10, 10);
                testCases[CreatureConstants.Achaierai][RollHelper.GetRollWithMostEvenDistribution(6, 13, 18, true)] = GetData(SizeConstants.Huge, 15, 15);
                testCases[CreatureConstants.Allip][RollHelper.GetRollWithMostEvenDistribution(4, 5, 12, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.Androsphinx][RollHelper.GetRollWithMostEvenDistribution(12, 13, 18, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.Androsphinx][RollHelper.GetRollWithMostEvenDistribution(12, 19, 36, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.Angel_AstralDeva][RollHelper.GetRollWithMostEvenDistribution(12, 13, 18, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.Angel_AstralDeva][RollHelper.GetRollWithMostEvenDistribution(12, 19, 36, true)] = GetData(SizeConstants.Large, 10, 10);
                testCases[CreatureConstants.Angel_Planetar][RollHelper.GetRollWithMostEvenDistribution(14, 15, 21, true)] = GetData(SizeConstants.Large, 10, 10);
                testCases[CreatureConstants.Angel_Planetar][RollHelper.GetRollWithMostEvenDistribution(14, 22, 42, true)] = GetData(SizeConstants.Huge, 15, 15);
                testCases[CreatureConstants.Angel_Solar][RollHelper.GetRollWithMostEvenDistribution(22, 23, 33, true)] = GetData(SizeConstants.Large, 10, 10);
                testCases[CreatureConstants.Angel_Solar][RollHelper.GetRollWithMostEvenDistribution(22, 34, 66, true)] = GetData(SizeConstants.Huge, 15, 15);
                testCases[CreatureConstants.AnimatedObject_Colossal][None] = new string[0];
                testCases[CreatureConstants.AnimatedObject_Colossal_Flexible][None] = new string[0];
                testCases[CreatureConstants.AnimatedObject_Colossal_MultipleLegs][None] = new string[0];
                testCases[CreatureConstants.AnimatedObject_Colossal_MultipleLegs_Wooden][None] = new string[0];
                testCases[CreatureConstants.AnimatedObject_Colossal_Sheetlike][None] = new string[0];
                testCases[CreatureConstants.AnimatedObject_Colossal_TwoLegs][None] = new string[0];
                testCases[CreatureConstants.AnimatedObject_Colossal_TwoLegs_Wooden][None] = new string[0];
                testCases[CreatureConstants.AnimatedObject_Colossal_Wheels_Wooden][None] = new string[0];
                testCases[CreatureConstants.AnimatedObject_Colossal_Wooden][None] = new string[0];
                testCases[CreatureConstants.AnimatedObject_Gargantuan][None] = new string[0];
                testCases[CreatureConstants.AnimatedObject_Gargantuan_Flexible][None] = new string[0];
                testCases[CreatureConstants.AnimatedObject_Gargantuan_MultipleLegs][None] = new string[0];
                testCases[CreatureConstants.AnimatedObject_Gargantuan_MultipleLegs_Wooden][None] = new string[0];
                testCases[CreatureConstants.AnimatedObject_Gargantuan_Sheetlike][None] = new string[0];
                testCases[CreatureConstants.AnimatedObject_Gargantuan_TwoLegs][None] = new string[0];
                testCases[CreatureConstants.AnimatedObject_Gargantuan_TwoLegs_Wooden][None] = new string[0];
                testCases[CreatureConstants.AnimatedObject_Gargantuan_Wheels_Wooden][None] = new string[0];
                testCases[CreatureConstants.AnimatedObject_Gargantuan_Wooden][None] = new string[0];
                testCases[CreatureConstants.AnimatedObject_Huge][None] = new string[0];
                testCases[CreatureConstants.AnimatedObject_Huge_Flexible][None] = new string[0];
                testCases[CreatureConstants.AnimatedObject_Huge_MultipleLegs][None] = new string[0];
                testCases[CreatureConstants.AnimatedObject_Huge_MultipleLegs_Wooden][None] = new string[0];
                testCases[CreatureConstants.AnimatedObject_Huge_Sheetlike][None] = new string[0];
                testCases[CreatureConstants.AnimatedObject_Huge_TwoLegs][None] = new string[0];
                testCases[CreatureConstants.AnimatedObject_Huge_TwoLegs_Wooden][None] = new string[0];
                testCases[CreatureConstants.AnimatedObject_Huge_Wheels_Wooden][None] = new string[0];
                testCases[CreatureConstants.AnimatedObject_Huge_Wooden][None] = new string[0];
                testCases[CreatureConstants.AnimatedObject_Large][None] = new string[0];
                testCases[CreatureConstants.AnimatedObject_Large_Flexible][None] = new string[0];
                testCases[CreatureConstants.AnimatedObject_Large_MultipleLegs][None] = new string[0];
                testCases[CreatureConstants.AnimatedObject_Large_MultipleLegs_Wooden][None] = new string[0];
                testCases[CreatureConstants.AnimatedObject_Large_Sheetlike][None] = new string[0];
                testCases[CreatureConstants.AnimatedObject_Large_TwoLegs][None] = new string[0];
                testCases[CreatureConstants.AnimatedObject_Large_TwoLegs_Wooden][None] = new string[0];
                testCases[CreatureConstants.AnimatedObject_Large_Wheels_Wooden][None] = new string[0];
                testCases[CreatureConstants.AnimatedObject_Large_Wooden][None] = new string[0];
                testCases[CreatureConstants.AnimatedObject_Medium][None] = new string[0];
                testCases[CreatureConstants.AnimatedObject_Medium_Flexible][None] = new string[0];
                testCases[CreatureConstants.AnimatedObject_Medium_MultipleLegs][None] = new string[0];
                testCases[CreatureConstants.AnimatedObject_Medium_MultipleLegs_Wooden][None] = new string[0];
                testCases[CreatureConstants.AnimatedObject_Medium_Sheetlike][None] = new string[0];
                testCases[CreatureConstants.AnimatedObject_Medium_TwoLegs][None] = new string[0];
                testCases[CreatureConstants.AnimatedObject_Medium_TwoLegs_Wooden][None] = new string[0];
                testCases[CreatureConstants.AnimatedObject_Medium_Wheels_Wooden][None] = new string[0];
                testCases[CreatureConstants.AnimatedObject_Medium_Wooden][None] = new string[0];
                testCases[CreatureConstants.AnimatedObject_Small][None] = new string[0];
                testCases[CreatureConstants.AnimatedObject_Small_Flexible][None] = new string[0];
                testCases[CreatureConstants.AnimatedObject_Small_MultipleLegs][None] = new string[0];
                testCases[CreatureConstants.AnimatedObject_Small_MultipleLegs_Wooden][None] = new string[0];
                testCases[CreatureConstants.AnimatedObject_Small_Sheetlike][None] = new string[0];
                testCases[CreatureConstants.AnimatedObject_Small_TwoLegs][None] = new string[0];
                testCases[CreatureConstants.AnimatedObject_Small_TwoLegs_Wooden][None] = new string[0];
                testCases[CreatureConstants.AnimatedObject_Small_Wheels_Wooden][None] = new string[0];
                testCases[CreatureConstants.AnimatedObject_Small_Wooden][None] = new string[0];
                testCases[CreatureConstants.AnimatedObject_Tiny][None] = new string[0];
                testCases[CreatureConstants.AnimatedObject_Tiny_Flexible][None] = new string[0];
                testCases[CreatureConstants.AnimatedObject_Tiny_MultipleLegs][None] = new string[0];
                testCases[CreatureConstants.AnimatedObject_Tiny_MultipleLegs_Wooden][None] = new string[0];
                testCases[CreatureConstants.AnimatedObject_Tiny_Sheetlike][None] = new string[0];
                testCases[CreatureConstants.AnimatedObject_Tiny_TwoLegs][None] = new string[0];
                testCases[CreatureConstants.AnimatedObject_Tiny_TwoLegs_Wooden][None] = new string[0];
                testCases[CreatureConstants.AnimatedObject_Tiny_Wheels_Wooden][None] = new string[0];
                testCases[CreatureConstants.AnimatedObject_Tiny_Wooden][None] = new string[0];
                testCases[CreatureConstants.Ankheg][RollHelper.GetRollWithMostEvenDistribution(3, 4, 4, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.Ankheg][RollHelper.GetRollWithMostEvenDistribution(3, 5, 9, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.Annis][None] = new string[0];
                testCases[CreatureConstants.Ant_Giant_Queen][RollHelper.GetRollWithMostEvenDistribution(4, 5, 6, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.Ant_Giant_Queen][RollHelper.GetRollWithMostEvenDistribution(4, 7, 8, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.Ant_Giant_Soldier][RollHelper.GetRollWithMostEvenDistribution(2, 3, 4, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.Ant_Giant_Soldier][RollHelper.GetRollWithMostEvenDistribution(2, 5, 6, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.Ant_Giant_Worker][RollHelper.GetRollWithMostEvenDistribution(2, 3, 4, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.Ant_Giant_Worker][RollHelper.GetRollWithMostEvenDistribution(2, 5, 6, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.Ape][RollHelper.GetRollWithMostEvenDistribution(4, 5, 8, true)] = GetData(SizeConstants.Large, 10, 10);
                testCases[CreatureConstants.Ape_Dire][RollHelper.GetRollWithMostEvenDistribution(5, 6, 15, true)] = GetData(SizeConstants.Large, 10, 10);
                testCases[CreatureConstants.Aranea][None] = new string[0];
                testCases[CreatureConstants.Arrowhawk_Adult][RollHelper.GetRollWithMostEvenDistribution(7, 8, 14, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.Arrowhawk_Elder][RollHelper.GetRollWithMostEvenDistribution(15, 16, 24, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.Arrowhawk_Juvenile][RollHelper.GetRollWithMostEvenDistribution(3, 4, 6, true)] = GetData(SizeConstants.Small, 5, 5);
                testCases[CreatureConstants.AssassinVine][RollHelper.GetRollWithMostEvenDistribution(4, 5, 16, true)] = GetData(SizeConstants.Huge, 15, 15);
                testCases[CreatureConstants.AssassinVine][RollHelper.GetRollWithMostEvenDistribution(4, 17, 32, true)] = GetData(SizeConstants.Gargantuan, 20, 20);
                testCases[CreatureConstants.AssassinVine][RollHelper.GetRollWithMostEvenDistribution(4, 33, 100, true)] = GetData(SizeConstants.Colossal, 30, 30);
                testCases[CreatureConstants.Athach][RollHelper.GetRollWithMostEvenDistribution(14, 15, 28, true)] = GetData(SizeConstants.Huge, 15, 15);
                testCases[CreatureConstants.Avoral][RollHelper.GetRollWithMostEvenDistribution(7, 8, 14, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.Avoral][RollHelper.GetRollWithMostEvenDistribution(7, 15, 21, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.Azer][None] = new string[0];
                testCases[CreatureConstants.Babau][RollHelper.GetRollWithMostEvenDistribution(7, 8, 14, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.Babau][RollHelper.GetRollWithMostEvenDistribution(7, 15, 21, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.Baboon][RollHelper.GetRollWithMostEvenDistribution(1, 2, 3, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.Badger][RollHelper.GetRollWithMostEvenDistribution(1, 2, 2, true)] = GetData(SizeConstants.Small, 5, 5);
                testCases[CreatureConstants.Badger_Dire][RollHelper.GetRollWithMostEvenDistribution(3, 4, 9, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.Balor][RollHelper.GetRollWithMostEvenDistribution(20, 21, 30, true)] = GetData(SizeConstants.Large, 10, 10);
                testCases[CreatureConstants.Balor][RollHelper.GetRollWithMostEvenDistribution(20, 31, 60, true)] = GetData(SizeConstants.Huge, 15, 15);
                testCases[CreatureConstants.BarbedDevil_Hamatula][RollHelper.GetRollWithMostEvenDistribution(12, 13, 24, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.BarbedDevil_Hamatula][RollHelper.GetRollWithMostEvenDistribution(12, 25, 36, true)] = GetData(SizeConstants.Large, 10, 10);
                testCases[CreatureConstants.Barghest][RollHelper.GetRollWithMostEvenDistribution(6, 7, 8, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.Barghest_Greater][RollHelper.GetRollWithMostEvenDistribution(9, 10, 18, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.Basilisk][RollHelper.GetRollWithMostEvenDistribution(6, 7, 10, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.Basilisk][RollHelper.GetRollWithMostEvenDistribution(6, 11, 18, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.Basilisk_Greater][None] = new string[0];
                testCases[CreatureConstants.Bat][None] = new string[0];
                testCases[CreatureConstants.Bat_Dire][RollHelper.GetRollWithMostEvenDistribution(4, 5, 12, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.Bat_Swarm][None] = new string[0];
                testCases[CreatureConstants.Bear_Black][RollHelper.GetRollWithMostEvenDistribution(3, 4, 5, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.Bear_Brown][RollHelper.GetRollWithMostEvenDistribution(6, 7, 10, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.Bear_Polar][RollHelper.GetRollWithMostEvenDistribution(8, 9, 12, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.Bear_Dire][RollHelper.GetRollWithMostEvenDistribution(12, 13, 16, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.Bear_Dire][RollHelper.GetRollWithMostEvenDistribution(12, 17, 36, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.BeardedDevil_Barbazu][RollHelper.GetRollWithMostEvenDistribution(6, 7, 9, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.BeardedDevil_Barbazu][RollHelper.GetRollWithMostEvenDistribution(6, 10, 18, true)] = GetData(SizeConstants.Large, 10, 10);
                testCases[CreatureConstants.Bebilith][RollHelper.GetRollWithMostEvenDistribution(12, 13, 18, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.Bebilith][RollHelper.GetRollWithMostEvenDistribution(12, 19, 36, true)] = GetData(SizeConstants.Gargantuan, 20, 15);
                testCases[CreatureConstants.Bee_Giant][RollHelper.GetRollWithMostEvenDistribution(3, 4, 6, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.Bee_Giant][RollHelper.GetRollWithMostEvenDistribution(3, 7, 9, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.Behir][RollHelper.GetRollWithMostEvenDistribution(9, 10, 13, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.Behir][RollHelper.GetRollWithMostEvenDistribution(9, 14, 27, true)] = GetData(SizeConstants.Gargantuan, 20, 15);
                testCases[CreatureConstants.Beholder][RollHelper.GetRollWithMostEvenDistribution(11, 12, 16, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.Beholder][RollHelper.GetRollWithMostEvenDistribution(11, 17, 33, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.Beholder_Gauth][RollHelper.GetRollWithMostEvenDistribution(6, 7, 12, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.Beholder_Gauth][RollHelper.GetRollWithMostEvenDistribution(6, 13, 18, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.Belker][RollHelper.GetRollWithMostEvenDistribution(7, 8, 10, true)] = GetData(SizeConstants.Large, 10, 10);
                testCases[CreatureConstants.Belker][RollHelper.GetRollWithMostEvenDistribution(7, 11, 21, true)] = GetData(SizeConstants.Huge, 15, 15);
                testCases[CreatureConstants.Bison][RollHelper.GetRollWithMostEvenDistribution(5, 6, 7, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.BlackPudding][RollHelper.GetRollWithMostEvenDistribution(10, 11, 15, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.BlackPudding_Elder][None] = new string[0];
                testCases[CreatureConstants.BlinkDog][RollHelper.GetRollWithMostEvenDistribution(4, 5, 7, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.BlinkDog][RollHelper.GetRollWithMostEvenDistribution(4, 8, 12, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.Boar][RollHelper.GetRollWithMostEvenDistribution(3, 4, 5, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.Boar_Dire][RollHelper.GetRollWithMostEvenDistribution(7, 8, 16, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.Boar_Dire][RollHelper.GetRollWithMostEvenDistribution(7, 17, 21, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.Bodak][RollHelper.GetRollWithMostEvenDistribution(9, 10, 13, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.Bodak][RollHelper.GetRollWithMostEvenDistribution(9, 14, 27, true)] = GetData(SizeConstants.Large, 10, 10);
                testCases[CreatureConstants.BombardierBeetle_Giant][RollHelper.GetRollWithMostEvenDistribution(2, 3, 4, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.BombardierBeetle_Giant][RollHelper.GetRollWithMostEvenDistribution(2, 5, 6, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.BoneDevil_Osyluth][RollHelper.GetRollWithMostEvenDistribution(10, 11, 20, true)] = GetData(SizeConstants.Large, 10, 10);
                testCases[CreatureConstants.BoneDevil_Osyluth][RollHelper.GetRollWithMostEvenDistribution(10, 21, 30, true)] = GetData(SizeConstants.Huge, 15, 15);
                testCases[CreatureConstants.Bralani][RollHelper.GetRollWithMostEvenDistribution(6, 7, 12, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.Bralani][RollHelper.GetRollWithMostEvenDistribution(6, 13, 18, true)] = GetData(SizeConstants.Large, 10, 10);
                testCases[CreatureConstants.Bugbear][None] = new string[0];
                testCases[CreatureConstants.Bulette][RollHelper.GetRollWithMostEvenDistribution(9, 10, 16, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.Bulette][RollHelper.GetRollWithMostEvenDistribution(9, 17, 27, true)] = GetData(SizeConstants.Gargantuan, 20, 15);
                testCases[CreatureConstants.Camel_Bactrian][None] = new string[0];
                testCases[CreatureConstants.Camel_Dromedary][None] = new string[0];
                testCases[CreatureConstants.CarrionCrawler][RollHelper.GetRollWithMostEvenDistribution(3, 4, 6, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.CarrionCrawler][RollHelper.GetRollWithMostEvenDistribution(3, 7, 9, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.Cat][None] = new string[0];
                testCases[CreatureConstants.Centaur][None] = new string[0];
                testCases[CreatureConstants.Centipede_Monstrous_Colossal][RollHelper.GetRollWithMostEvenDistribution(24, 25, 48, true)] = GetData(SizeConstants.Colossal, 30, 20);
                testCases[CreatureConstants.Centipede_Monstrous_Gargantuan][RollHelper.GetRollWithMostEvenDistribution(12, 13, 23, true)] = GetData(SizeConstants.Gargantuan, 20, 15);
                testCases[CreatureConstants.Centipede_Monstrous_Huge][RollHelper.GetRollWithMostEvenDistribution(6, 7, 11, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.Centipede_Monstrous_Large][RollHelper.GetRollWithMostEvenDistribution(3, 4, 5, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.Centipede_Monstrous_Medium][None] = new string[0];
                testCases[CreatureConstants.Centipede_Monstrous_Small][None] = new string[0];
                testCases[CreatureConstants.Centipede_Monstrous_Tiny][None] = new string[0];
                testCases[CreatureConstants.Centipede_Swarm][None] = new string[0];
                testCases[CreatureConstants.ChainDevil_Kyton][RollHelper.GetRollWithMostEvenDistribution(8, 9, 16, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.ChaosBeast][RollHelper.GetRollWithMostEvenDistribution(8, 9, 12, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.ChaosBeast][RollHelper.GetRollWithMostEvenDistribution(8, 13, 24, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.Cheetah][RollHelper.GetRollWithMostEvenDistribution(3, 4, 5, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.Chimera_Black][RollHelper.GetRollWithMostEvenDistribution(9, 10, 13, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.Chimera_Black][RollHelper.GetRollWithMostEvenDistribution(9, 14, 27, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.Chimera_Blue][RollHelper.GetRollWithMostEvenDistribution(9, 10, 13, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.Chimera_Blue][RollHelper.GetRollWithMostEvenDistribution(9, 14, 27, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.Chimera_Green][RollHelper.GetRollWithMostEvenDistribution(9, 10, 13, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.Chimera_Green][RollHelper.GetRollWithMostEvenDistribution(9, 14, 27, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.Chimera_Red][RollHelper.GetRollWithMostEvenDistribution(9, 10, 13, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.Chimera_Red][RollHelper.GetRollWithMostEvenDistribution(9, 14, 27, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.Chimera_White][RollHelper.GetRollWithMostEvenDistribution(9, 10, 13, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.Chimera_White][RollHelper.GetRollWithMostEvenDistribution(9, 14, 27, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.Choker][RollHelper.GetRollWithMostEvenDistribution(3, 4, 6, true)] = GetData(SizeConstants.Small, 5, 10);
                testCases[CreatureConstants.Choker][RollHelper.GetRollWithMostEvenDistribution(3, 7, 12, true)] = GetData(SizeConstants.Medium, 5, 10);
                testCases[CreatureConstants.Chuul][RollHelper.GetRollWithMostEvenDistribution(11, 12, 16, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.Chuul][RollHelper.GetRollWithMostEvenDistribution(11, 17, 33, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.Cloaker][RollHelper.GetRollWithMostEvenDistribution(6, 7, 9, true)] = GetData(SizeConstants.Large, 10, 10);
                testCases[CreatureConstants.Cloaker][RollHelper.GetRollWithMostEvenDistribution(6, 10, 18, true)] = GetData(SizeConstants.Huge, 15, 15);
                testCases[CreatureConstants.Cockatrice][RollHelper.GetRollWithMostEvenDistribution(5, 6, 8, true)] = GetData(SizeConstants.Small, 5, 5);
                testCases[CreatureConstants.Cockatrice][RollHelper.GetRollWithMostEvenDistribution(5, 9, 15, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.Couatl][RollHelper.GetRollWithMostEvenDistribution(9, 10, 13, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.Couatl][RollHelper.GetRollWithMostEvenDistribution(9, 14, 27, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.Criosphinx][RollHelper.GetRollWithMostEvenDistribution(10, 11, 15, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.Criosphinx][RollHelper.GetRollWithMostEvenDistribution(10, 16, 30, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.Crocodile][RollHelper.GetRollWithMostEvenDistribution(3, 4, 5, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.Crocodile_Giant][RollHelper.GetRollWithMostEvenDistribution(7, 8, 14, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.Cryohydra_10Heads][None] = new string[0];
                testCases[CreatureConstants.Cryohydra_11Heads][None] = new string[0];
                testCases[CreatureConstants.Cryohydra_12Heads][None] = new string[0];
                testCases[CreatureConstants.Cryohydra_5Heads][None] = new string[0];
                testCases[CreatureConstants.Cryohydra_6Heads][None] = new string[0];
                testCases[CreatureConstants.Cryohydra_7Heads][None] = new string[0];
                testCases[CreatureConstants.Cryohydra_8Heads][None] = new string[0];
                testCases[CreatureConstants.Cryohydra_9Heads][None] = new string[0];
                testCases[CreatureConstants.Darkmantle][RollHelper.GetRollWithMostEvenDistribution(1, 2, 3, true)] = GetData(SizeConstants.Small, 5, 5);
                testCases[CreatureConstants.Deinonychus][RollHelper.GetRollWithMostEvenDistribution(4, 5, 8, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.Delver][RollHelper.GetRollWithMostEvenDistribution(15, 16, 30, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.Delver][RollHelper.GetRollWithMostEvenDistribution(15, 31, 45, true)] = GetData(SizeConstants.Gargantuan, 20, 15);
                testCases[CreatureConstants.Derro][None] = new string[0];
                testCases[CreatureConstants.Derro_Sane][None] = new string[0];
                testCases[CreatureConstants.Destrachan][RollHelper.GetRollWithMostEvenDistribution(8, 9, 16, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.Destrachan][RollHelper.GetRollWithMostEvenDistribution(8, 17, 24, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.Devourer][RollHelper.GetRollWithMostEvenDistribution(12, 13, 24, true)] = GetData(SizeConstants.Large, 10, 10);
                testCases[CreatureConstants.Devourer][RollHelper.GetRollWithMostEvenDistribution(12, 25, 36, true)] = GetData(SizeConstants.Huge, 15, 15);
                testCases[CreatureConstants.Digester][RollHelper.GetRollWithMostEvenDistribution(8, 9, 12, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.Digester][RollHelper.GetRollWithMostEvenDistribution(8, 13, 24, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.DisplacerBeast][RollHelper.GetRollWithMostEvenDistribution(6, 7, 9, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.DisplacerBeast][RollHelper.GetRollWithMostEvenDistribution(6, 10, 18, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.DisplacerBeast_PackLord][None] = new string[0];
                testCases[CreatureConstants.Djinni][RollHelper.GetRollWithMostEvenDistribution(7, 8, 10, true)] = GetData(SizeConstants.Large, 10, 10);
                testCases[CreatureConstants.Djinni][RollHelper.GetRollWithMostEvenDistribution(7, 11, 21, true)] = GetData(SizeConstants.Huge, 15, 15);
                testCases[CreatureConstants.Djinni_Noble][RollHelper.GetRollWithMostEvenDistribution(10, 11, 15, true)] = GetData(SizeConstants.Large, 10, 10);
                testCases[CreatureConstants.Djinni_Noble][RollHelper.GetRollWithMostEvenDistribution(10, 16, 30, true)] = GetData(SizeConstants.Huge, 15, 15);
                testCases[CreatureConstants.Dog][None] = new string[0];
                testCases[CreatureConstants.Dog_Riding][None] = new string[0];
                testCases[CreatureConstants.Donkey][None] = new string[0];
                testCases[CreatureConstants.Doppelganger][None] = new string[0];
                testCases[CreatureConstants.Dragon_Black_Wyrmling][RollHelper.GetRollWithMostEvenDistribution(4, 5, 6, true)] = GetData(SizeConstants.Tiny, 2.5, 0);
                testCases[CreatureConstants.Dragon_Black_VeryYoung][RollHelper.GetRollWithMostEvenDistribution(7, 8, 9, true)] = GetData(SizeConstants.Small, 5, 5);
                testCases[CreatureConstants.Dragon_Black_Young][RollHelper.GetRollWithMostEvenDistribution(10, 11, 12, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.Dragon_Black_Juvenile][RollHelper.GetRollWithMostEvenDistribution(13, 14, 15, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.Dragon_Black_YoungAdult][RollHelper.GetRollWithMostEvenDistribution(16, 17, 18, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.Dragon_Black_Adult][RollHelper.GetRollWithMostEvenDistribution(19, 20, 21, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.Dragon_Black_MatureAdult][RollHelper.GetRollWithMostEvenDistribution(22, 23, 24, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.Dragon_Black_Old][RollHelper.GetRollWithMostEvenDistribution(25, 26, 27, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.Dragon_Black_VeryOld][RollHelper.GetRollWithMostEvenDistribution(28, 29, 30, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.Dragon_Black_Ancient][RollHelper.GetRollWithMostEvenDistribution(31, 32, 33, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.Dragon_Black_Wyrm][RollHelper.GetRollWithMostEvenDistribution(34, 35, 36, true)] = GetData(SizeConstants.Gargantuan, 20, 15);
                testCases[CreatureConstants.Dragon_Black_GreatWyrm][RollHelper.GetRollWithMostEvenDistribution(37, 38, 100, true)] = GetData(SizeConstants.Gargantuan, 20, 15);
                testCases[CreatureConstants.Dragon_Blue_Wyrmling][RollHelper.GetRollWithMostEvenDistribution(6, 7, 8, true)] = GetData(SizeConstants.Small, 5, 5);
                testCases[CreatureConstants.Dragon_Blue_VeryYoung][RollHelper.GetRollWithMostEvenDistribution(9, 10, 11, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.Dragon_Blue_Young][RollHelper.GetRollWithMostEvenDistribution(12, 13, 14, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.Dragon_Blue_Juvenile][RollHelper.GetRollWithMostEvenDistribution(15, 16, 17, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.Dragon_Blue_YoungAdult][RollHelper.GetRollWithMostEvenDistribution(18, 19, 20, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.Dragon_Blue_Adult][RollHelper.GetRollWithMostEvenDistribution(21, 22, 23, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.Dragon_Blue_MatureAdult][RollHelper.GetRollWithMostEvenDistribution(24, 25, 26, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.Dragon_Blue_Old][RollHelper.GetRollWithMostEvenDistribution(27, 28, 29, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.Dragon_Blue_VeryOld][RollHelper.GetRollWithMostEvenDistribution(30, 31, 32, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.Dragon_Blue_Ancient][RollHelper.GetRollWithMostEvenDistribution(33, 34, 35, true)] = GetData(SizeConstants.Gargantuan, 20, 15);
                testCases[CreatureConstants.Dragon_Blue_Wyrm][RollHelper.GetRollWithMostEvenDistribution(36, 37, 38, true)] = GetData(SizeConstants.Gargantuan, 20, 15);
                testCases[CreatureConstants.Dragon_Blue_GreatWyrm][RollHelper.GetRollWithMostEvenDistribution(39, 40, 100, true)] = GetData(SizeConstants.Gargantuan, 20, 15);
                testCases[CreatureConstants.Dragon_Green_Wyrmling][RollHelper.GetRollWithMostEvenDistribution(5, 6, 7, true)] = GetData(SizeConstants.Small, 5, 5);
                testCases[CreatureConstants.Dragon_Green_VeryYoung][RollHelper.GetRollWithMostEvenDistribution(8, 9, 10, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.Dragon_Green_Young][RollHelper.GetRollWithMostEvenDistribution(11, 12, 13, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.Dragon_Green_Juvenile][RollHelper.GetRollWithMostEvenDistribution(14, 15, 16, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.Dragon_Green_YoungAdult][RollHelper.GetRollWithMostEvenDistribution(17, 18, 19, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.Dragon_Green_Adult][RollHelper.GetRollWithMostEvenDistribution(20, 21, 22, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.Dragon_Green_MatureAdult][RollHelper.GetRollWithMostEvenDistribution(23, 24, 25, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.Dragon_Green_Old][RollHelper.GetRollWithMostEvenDistribution(26, 27, 28, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.Dragon_Green_VeryOld][RollHelper.GetRollWithMostEvenDistribution(29, 30, 31, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.Dragon_Green_Ancient][RollHelper.GetRollWithMostEvenDistribution(32, 33, 34, true)] = GetData(SizeConstants.Gargantuan, 20, 15);
                testCases[CreatureConstants.Dragon_Green_Wyrm][RollHelper.GetRollWithMostEvenDistribution(35, 36, 37, true)] = GetData(SizeConstants.Gargantuan, 20, 15);
                testCases[CreatureConstants.Dragon_Green_GreatWyrm][RollHelper.GetRollWithMostEvenDistribution(38, 39, 100, true)] = GetData(SizeConstants.Gargantuan, 20, 15);
                testCases[CreatureConstants.Dragon_Red_Wyrmling][RollHelper.GetRollWithMostEvenDistribution(7, 8, 9, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.Dragon_Red_VeryYoung][RollHelper.GetRollWithMostEvenDistribution(10, 11, 12, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.Dragon_Red_Young][RollHelper.GetRollWithMostEvenDistribution(13, 14, 15, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.Dragon_Red_Juvenile][RollHelper.GetRollWithMostEvenDistribution(16, 17, 18, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.Dragon_Red_YoungAdult][RollHelper.GetRollWithMostEvenDistribution(19, 20, 21, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.Dragon_Red_Adult][RollHelper.GetRollWithMostEvenDistribution(22, 23, 24, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.Dragon_Red_MatureAdult][RollHelper.GetRollWithMostEvenDistribution(25, 26, 27, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.Dragon_Red_Old][RollHelper.GetRollWithMostEvenDistribution(28, 29, 30, true)] = GetData(SizeConstants.Gargantuan, 20, 15);
                testCases[CreatureConstants.Dragon_Red_VeryOld][RollHelper.GetRollWithMostEvenDistribution(31, 32, 33, true)] = GetData(SizeConstants.Gargantuan, 20, 15);
                testCases[CreatureConstants.Dragon_Red_Ancient][RollHelper.GetRollWithMostEvenDistribution(34, 35, 36, true)] = GetData(SizeConstants.Gargantuan, 20, 15);
                testCases[CreatureConstants.Dragon_Red_Wyrm][RollHelper.GetRollWithMostEvenDistribution(37, 38, 39, true)] = GetData(SizeConstants.Gargantuan, 20, 15);
                testCases[CreatureConstants.Dragon_Red_GreatWyrm][RollHelper.GetRollWithMostEvenDistribution(40, 41, 100, true)] = GetData(SizeConstants.Colossal, 30, 20);
                testCases[CreatureConstants.Dragon_White_Wyrmling][RollHelper.GetRollWithMostEvenDistribution(3, 4, 5, true)] = GetData(SizeConstants.Tiny, 2.5, 0);
                testCases[CreatureConstants.Dragon_White_VeryYoung][RollHelper.GetRollWithMostEvenDistribution(6, 7, 8, true)] = GetData(SizeConstants.Small, 5, 5);
                testCases[CreatureConstants.Dragon_White_Young][RollHelper.GetRollWithMostEvenDistribution(9, 10, 11, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.Dragon_White_Juvenile][RollHelper.GetRollWithMostEvenDistribution(12, 13, 14, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.Dragon_White_YoungAdult][RollHelper.GetRollWithMostEvenDistribution(15, 16, 17, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.Dragon_White_Adult][RollHelper.GetRollWithMostEvenDistribution(18, 19, 20, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.Dragon_White_MatureAdult][RollHelper.GetRollWithMostEvenDistribution(21, 22, 23, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.Dragon_White_Old][RollHelper.GetRollWithMostEvenDistribution(24, 25, 26, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.Dragon_White_VeryOld][RollHelper.GetRollWithMostEvenDistribution(27, 28, 29, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.Dragon_White_Ancient][RollHelper.GetRollWithMostEvenDistribution(30, 31, 32, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.Dragon_White_Wyrm][RollHelper.GetRollWithMostEvenDistribution(33, 34, 35, true)] = GetData(SizeConstants.Gargantuan, 20, 15);
                testCases[CreatureConstants.Dragon_White_GreatWyrm][RollHelper.GetRollWithMostEvenDistribution(36, 37, 100, true)] = GetData(SizeConstants.Gargantuan, 20, 15);
                testCases[CreatureConstants.Dragon_Brass_Wyrmling][RollHelper.GetRollWithMostEvenDistribution(4, 5, 6, true)] = GetData(SizeConstants.Tiny, 2.5, 0);
                testCases[CreatureConstants.Dragon_Brass_VeryYoung][RollHelper.GetRollWithMostEvenDistribution(7, 8, 9, true)] = GetData(SizeConstants.Small, 5, 5);
                testCases[CreatureConstants.Dragon_Brass_Young][RollHelper.GetRollWithMostEvenDistribution(10, 11, 12, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.Dragon_Brass_Juvenile][RollHelper.GetRollWithMostEvenDistribution(13, 14, 15, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.Dragon_Brass_YoungAdult][RollHelper.GetRollWithMostEvenDistribution(16, 17, 18, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.Dragon_Brass_Adult][RollHelper.GetRollWithMostEvenDistribution(19, 20, 21, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.Dragon_Brass_MatureAdult][RollHelper.GetRollWithMostEvenDistribution(22, 23, 24, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.Dragon_Brass_Old][RollHelper.GetRollWithMostEvenDistribution(25, 26, 27, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.Dragon_Brass_VeryOld][RollHelper.GetRollWithMostEvenDistribution(28, 29, 30, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.Dragon_Brass_Ancient][RollHelper.GetRollWithMostEvenDistribution(31, 32, 33, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.Dragon_Brass_Wyrm][RollHelper.GetRollWithMostEvenDistribution(34, 35, 36, true)] = GetData(SizeConstants.Gargantuan, 20, 15);
                testCases[CreatureConstants.Dragon_Brass_GreatWyrm][RollHelper.GetRollWithMostEvenDistribution(37, 38, 100, true)] = GetData(SizeConstants.Gargantuan, 20, 15);
                testCases[CreatureConstants.Dragon_Bronze_Wyrmling][RollHelper.GetRollWithMostEvenDistribution(6, 7, 8, true)] = GetData(SizeConstants.Small, 5, 5);
                testCases[CreatureConstants.Dragon_Bronze_VeryYoung][RollHelper.GetRollWithMostEvenDistribution(9, 10, 11, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.Dragon_Bronze_Young][RollHelper.GetRollWithMostEvenDistribution(12, 13, 14, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.Dragon_Bronze_Juvenile][RollHelper.GetRollWithMostEvenDistribution(15, 16, 17, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.Dragon_Bronze_YoungAdult][RollHelper.GetRollWithMostEvenDistribution(18, 19, 20, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.Dragon_Bronze_Adult][RollHelper.GetRollWithMostEvenDistribution(21, 22, 23, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.Dragon_Bronze_MatureAdult][RollHelper.GetRollWithMostEvenDistribution(24, 25, 26, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.Dragon_Bronze_Old][RollHelper.GetRollWithMostEvenDistribution(27, 28, 29, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.Dragon_Bronze_VeryOld][RollHelper.GetRollWithMostEvenDistribution(30, 31, 32, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.Dragon_Bronze_Ancient][RollHelper.GetRollWithMostEvenDistribution(33, 34, 35, true)] = GetData(SizeConstants.Gargantuan, 20, 15);
                testCases[CreatureConstants.Dragon_Bronze_Wyrm][RollHelper.GetRollWithMostEvenDistribution(36, 37, 38, true)] = GetData(SizeConstants.Gargantuan, 20, 15);
                testCases[CreatureConstants.Dragon_Bronze_GreatWyrm][RollHelper.GetRollWithMostEvenDistribution(39, 40, 100, true)] = GetData(SizeConstants.Gargantuan, 20, 15);
                testCases[CreatureConstants.Dragon_Copper_Wyrmling][RollHelper.GetRollWithMostEvenDistribution(5, 6, 7, true)] = GetData(SizeConstants.Small, 5, 5);
                testCases[CreatureConstants.Dragon_Copper_VeryYoung][RollHelper.GetRollWithMostEvenDistribution(8, 9, 10, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.Dragon_Copper_Young][RollHelper.GetRollWithMostEvenDistribution(11, 12, 13, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.Dragon_Copper_Juvenile][RollHelper.GetRollWithMostEvenDistribution(14, 15, 16, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.Dragon_Copper_YoungAdult][RollHelper.GetRollWithMostEvenDistribution(17, 18, 19, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.Dragon_Copper_Adult][RollHelper.GetRollWithMostEvenDistribution(20, 21, 22, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.Dragon_Copper_MatureAdult][RollHelper.GetRollWithMostEvenDistribution(23, 24, 25, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.Dragon_Copper_Old][RollHelper.GetRollWithMostEvenDistribution(26, 27, 28, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.Dragon_Copper_VeryOld][RollHelper.GetRollWithMostEvenDistribution(29, 30, 31, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.Dragon_Copper_Ancient][RollHelper.GetRollWithMostEvenDistribution(32, 33, 34, true)] = GetData(SizeConstants.Gargantuan, 20, 15);
                testCases[CreatureConstants.Dragon_Copper_Wyrm][RollHelper.GetRollWithMostEvenDistribution(35, 36, 37, true)] = GetData(SizeConstants.Gargantuan, 20, 15);
                testCases[CreatureConstants.Dragon_Copper_GreatWyrm][RollHelper.GetRollWithMostEvenDistribution(38, 39, 100, true)] = GetData(SizeConstants.Gargantuan, 20, 15);
                testCases[CreatureConstants.Dragon_Gold_Wyrmling][RollHelper.GetRollWithMostEvenDistribution(8, 9, 10, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.Dragon_Gold_VeryYoung][RollHelper.GetRollWithMostEvenDistribution(11, 12, 13, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.Dragon_Gold_Young][RollHelper.GetRollWithMostEvenDistribution(14, 15, 16, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.Dragon_Gold_Juvenile][RollHelper.GetRollWithMostEvenDistribution(17, 18, 19, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.Dragon_Gold_YoungAdult][RollHelper.GetRollWithMostEvenDistribution(20, 21, 22, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.Dragon_Gold_Adult][RollHelper.GetRollWithMostEvenDistribution(23, 24, 25, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.Dragon_Gold_MatureAdult][RollHelper.GetRollWithMostEvenDistribution(26, 27, 28, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.Dragon_Gold_Old][RollHelper.GetRollWithMostEvenDistribution(29, 30, 31, true)] = GetData(SizeConstants.Gargantuan, 20, 15);
                testCases[CreatureConstants.Dragon_Gold_VeryOld][RollHelper.GetRollWithMostEvenDistribution(32, 33, 34, true)] = GetData(SizeConstants.Gargantuan, 20, 15);
                testCases[CreatureConstants.Dragon_Gold_Ancient][RollHelper.GetRollWithMostEvenDistribution(35, 36, 37, true)] = GetData(SizeConstants.Gargantuan, 20, 15);
                testCases[CreatureConstants.Dragon_Gold_Wyrm][RollHelper.GetRollWithMostEvenDistribution(38, 39, 40, true)] = GetData(SizeConstants.Colossal, 30, 20);
                testCases[CreatureConstants.Dragon_Gold_GreatWyrm][RollHelper.GetRollWithMostEvenDistribution(41, 42, 100, true)] = GetData(SizeConstants.Colossal, 30, 20);
                testCases[CreatureConstants.Dragon_Silver_Wyrmling][RollHelper.GetRollWithMostEvenDistribution(7, 8, 9, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.Dragon_Silver_VeryYoung][RollHelper.GetRollWithMostEvenDistribution(10, 11, 12, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.Dragon_Silver_Young][RollHelper.GetRollWithMostEvenDistribution(13, 14, 15, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.Dragon_Silver_Juvenile][RollHelper.GetRollWithMostEvenDistribution(16, 17, 18, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.Dragon_Silver_YoungAdult][RollHelper.GetRollWithMostEvenDistribution(19, 20, 21, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.Dragon_Silver_Adult][RollHelper.GetRollWithMostEvenDistribution(22, 23, 24, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.Dragon_Silver_MatureAdult][RollHelper.GetRollWithMostEvenDistribution(25, 26, 27, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.Dragon_Silver_Old][RollHelper.GetRollWithMostEvenDistribution(28, 29, 30, true)] = GetData(SizeConstants.Gargantuan, 20, 15);
                testCases[CreatureConstants.Dragon_Silver_VeryOld][RollHelper.GetRollWithMostEvenDistribution(31, 32, 33, true)] = GetData(SizeConstants.Gargantuan, 20, 15);
                testCases[CreatureConstants.Dragon_Silver_Ancient][RollHelper.GetRollWithMostEvenDistribution(34, 35, 36, true)] = GetData(SizeConstants.Gargantuan, 20, 15);
                testCases[CreatureConstants.Dragon_Silver_Wyrm][RollHelper.GetRollWithMostEvenDistribution(37, 38, 39, true)] = GetData(SizeConstants.Gargantuan, 20, 15);
                testCases[CreatureConstants.Dragon_Silver_GreatWyrm][RollHelper.GetRollWithMostEvenDistribution(40, 41, 100, true)] = GetData(SizeConstants.Colossal, 30, 20);
                testCases[CreatureConstants.DragonTurtle][RollHelper.GetRollWithMostEvenDistribution(12, 13, 24, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.DragonTurtle][RollHelper.GetRollWithMostEvenDistribution(12, 25, 36, true)] = GetData(SizeConstants.Gargantuan, 20, 15);
                testCases[CreatureConstants.Dragonne][RollHelper.GetRollWithMostEvenDistribution(9, 10, 12, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.Dragonne][RollHelper.GetRollWithMostEvenDistribution(9, 13, 27, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.Dretch][RollHelper.GetRollWithMostEvenDistribution(2, 3, 6, true)] = GetData(SizeConstants.Small, 5, 5);
                testCases[CreatureConstants.Drider][None] = new string[0];
                testCases[CreatureConstants.Dryad][None] = new string[0];
                testCases[CreatureConstants.Dwarf_Deep][None] = new string[0];
                testCases[CreatureConstants.Dwarf_Duergar][None] = new string[0];
                testCases[CreatureConstants.Dwarf_Hill][None] = new string[0];
                testCases[CreatureConstants.Dwarf_Mountain][None] = new string[0];
                testCases[CreatureConstants.Eagle][RollHelper.GetRollWithMostEvenDistribution(1, 2, 3, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.Eagle_Giant][RollHelper.GetRollWithMostEvenDistribution(4, 5, 8, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.Eagle_Giant][RollHelper.GetRollWithMostEvenDistribution(4, 9, 12, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.Efreeti][RollHelper.GetRollWithMostEvenDistribution(10, 11, 15, true)] = GetData(SizeConstants.Large, 10, 10);
                testCases[CreatureConstants.Efreeti][RollHelper.GetRollWithMostEvenDistribution(10, 16, 30, true)] = GetData(SizeConstants.Huge, 15, 15);
                testCases[CreatureConstants.Elasmosaurus][RollHelper.GetRollWithMostEvenDistribution(10, 11, 20, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.Elasmosaurus][RollHelper.GetRollWithMostEvenDistribution(10, 21, 30, true)] = GetData(SizeConstants.Gargantuan, 20, 15);
                testCases[CreatureConstants.Elemental_Air_Elder][RollHelper.GetRollWithMostEvenDistribution(24, 25, 48, true)] = GetData(SizeConstants.Huge, 15, 15);
                testCases[CreatureConstants.Elemental_Air_Greater][RollHelper.GetRollWithMostEvenDistribution(21, 22, 23, true)] = GetData(SizeConstants.Huge, 15, 15);
                testCases[CreatureConstants.Elemental_Air_Huge][RollHelper.GetRollWithMostEvenDistribution(16, 17, 20, true)] = GetData(SizeConstants.Huge, 15, 15);
                testCases[CreatureConstants.Elemental_Air_Large][RollHelper.GetRollWithMostEvenDistribution(8, 9, 15, true)] = GetData(SizeConstants.Large, 10, 10);
                testCases[CreatureConstants.Elemental_Air_Medium][RollHelper.GetRollWithMostEvenDistribution(4, 5, 7, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.Elemental_Air_Small][RollHelper.GetRollWithMostEvenDistribution(2, 3, 3, true)] = GetData(SizeConstants.Small, 5, 5);
                testCases[CreatureConstants.Elemental_Earth_Elder][RollHelper.GetRollWithMostEvenDistribution(24, 25, 48, true)] = GetData(SizeConstants.Huge, 15, 15);
                testCases[CreatureConstants.Elemental_Earth_Greater][RollHelper.GetRollWithMostEvenDistribution(21, 22, 23, true)] = GetData(SizeConstants.Huge, 15, 15);
                testCases[CreatureConstants.Elemental_Earth_Huge][RollHelper.GetRollWithMostEvenDistribution(16, 17, 20, true)] = GetData(SizeConstants.Huge, 15, 15);
                testCases[CreatureConstants.Elemental_Earth_Large][RollHelper.GetRollWithMostEvenDistribution(8, 9, 15, true)] = GetData(SizeConstants.Large, 10, 10);
                testCases[CreatureConstants.Elemental_Earth_Medium][RollHelper.GetRollWithMostEvenDistribution(4, 5, 7, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.Elemental_Earth_Small][RollHelper.GetRollWithMostEvenDistribution(2, 3, 3, true)] = GetData(SizeConstants.Small, 5, 5);
                testCases[CreatureConstants.Elemental_Fire_Elder][RollHelper.GetRollWithMostEvenDistribution(24, 25, 48, true)] = GetData(SizeConstants.Huge, 15, 15);
                testCases[CreatureConstants.Elemental_Fire_Greater][RollHelper.GetRollWithMostEvenDistribution(21, 22, 23, true)] = GetData(SizeConstants.Huge, 15, 15);
                testCases[CreatureConstants.Elemental_Fire_Huge][RollHelper.GetRollWithMostEvenDistribution(16, 17, 20, true)] = GetData(SizeConstants.Huge, 15, 15);
                testCases[CreatureConstants.Elemental_Fire_Large][RollHelper.GetRollWithMostEvenDistribution(8, 9, 15, true)] = GetData(SizeConstants.Large, 10, 10);
                testCases[CreatureConstants.Elemental_Fire_Medium][RollHelper.GetRollWithMostEvenDistribution(4, 5, 7, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.Elemental_Fire_Small][RollHelper.GetRollWithMostEvenDistribution(2, 3, 3, true)] = GetData(SizeConstants.Small, 5, 5);
                testCases[CreatureConstants.Elemental_Water_Elder][RollHelper.GetRollWithMostEvenDistribution(24, 25, 48, true)] = GetData(SizeConstants.Huge, 15, 15);
                testCases[CreatureConstants.Elemental_Water_Greater][RollHelper.GetRollWithMostEvenDistribution(21, 22, 23, true)] = GetData(SizeConstants.Huge, 15, 15);
                testCases[CreatureConstants.Elemental_Water_Huge][RollHelper.GetRollWithMostEvenDistribution(16, 17, 20, true)] = GetData(SizeConstants.Huge, 15, 15);
                testCases[CreatureConstants.Elemental_Water_Large][RollHelper.GetRollWithMostEvenDistribution(8, 9, 15, true)] = GetData(SizeConstants.Large, 10, 10);
                testCases[CreatureConstants.Elemental_Water_Medium][RollHelper.GetRollWithMostEvenDistribution(4, 5, 7, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.Elemental_Water_Small][RollHelper.GetRollWithMostEvenDistribution(2, 3, 3, true)] = GetData(SizeConstants.Small, 5, 5);
                testCases[CreatureConstants.Elephant][RollHelper.GetRollWithMostEvenDistribution(11, 12, 22, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.Elf_Aquatic][None] = new string[0];
                testCases[CreatureConstants.Elf_Drow][None] = new string[0];
                testCases[CreatureConstants.Elf_Gray][None] = new string[0];
                testCases[CreatureConstants.Elf_Half][None] = new string[0];
                testCases[CreatureConstants.Elf_High][None] = new string[0];
                testCases[CreatureConstants.Elf_Wild][None] = new string[0];
                testCases[CreatureConstants.Elf_Wood][None] = new string[0];
                testCases[CreatureConstants.Erinyes][RollHelper.GetRollWithMostEvenDistribution(9, 10, 18, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.EtherealFilcher][RollHelper.GetRollWithMostEvenDistribution(5, 6, 7, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.EtherealFilcher][RollHelper.GetRollWithMostEvenDistribution(5, 8, 15, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.EtherealMarauder][RollHelper.GetRollWithMostEvenDistribution(2, 3, 4, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.EtherealMarauder][RollHelper.GetRollWithMostEvenDistribution(2, 5, 6, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.Ettercap][RollHelper.GetRollWithMostEvenDistribution(5, 6, 7, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.Ettercap][RollHelper.GetRollWithMostEvenDistribution(5, 8, 15, true)] = GetData(SizeConstants.Large, 10, 10);
                testCases[CreatureConstants.Ettin][None] = new string[0];
                testCases[CreatureConstants.FireBeetle_Giant][RollHelper.GetRollWithMostEvenDistribution(1, 2, 3, true)] = GetData(SizeConstants.Small, 5, 5);
                testCases[CreatureConstants.FormianMyrmarch][RollHelper.GetRollWithMostEvenDistribution(12, 13, 18, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.FormianMyrmarch][RollHelper.GetRollWithMostEvenDistribution(12, 19, 24, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.FormianQueen][RollHelper.GetRollWithMostEvenDistribution(20, 21, 30, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.FormianQueen][RollHelper.GetRollWithMostEvenDistribution(20, 31, 40, true)] = GetData(SizeConstants.Gargantuan, 20, 15);
                testCases[CreatureConstants.FormianTaskmaster][RollHelper.GetRollWithMostEvenDistribution(6, 7, 9, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.FormianTaskmaster][RollHelper.GetRollWithMostEvenDistribution(6, 10, 12, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.FormianWarrior][RollHelper.GetRollWithMostEvenDistribution(4, 5, 8, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.FormianWarrior][RollHelper.GetRollWithMostEvenDistribution(4, 9, 12, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.FormianWorker][RollHelper.GetRollWithMostEvenDistribution(1, 2, 3, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.FrostWorm][RollHelper.GetRollWithMostEvenDistribution(14, 15, 21, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.FrostWorm][RollHelper.GetRollWithMostEvenDistribution(14, 22, 42, true)] = GetData(SizeConstants.Gargantuan, 20, 15);
                testCases[CreatureConstants.Gargoyle][RollHelper.GetRollWithMostEvenDistribution(4, 5, 6, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.Gargoyle][RollHelper.GetRollWithMostEvenDistribution(4, 7, 12, true)] = GetData(SizeConstants.Large, 10, 10);
                testCases[CreatureConstants.Gargoyle_Kapoacinth][RollHelper.GetRollWithMostEvenDistribution(4, 5, 6, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.Gargoyle_Kapoacinth][RollHelper.GetRollWithMostEvenDistribution(4, 7, 12, true)] = GetData(SizeConstants.Large, 10, 10);
                testCases[CreatureConstants.GelatinousCube][RollHelper.GetRollWithMostEvenDistribution(4, 5, 12, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.GelatinousCube][RollHelper.GetRollWithMostEvenDistribution(4, 13, 24, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.Ghaele][RollHelper.GetRollWithMostEvenDistribution(10, 11, 15, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.Ghaele][RollHelper.GetRollWithMostEvenDistribution(10, 16, 30, true)] = GetData(SizeConstants.Large, 10, 10);
                testCases[CreatureConstants.Ghoul][RollHelper.GetRollWithMostEvenDistribution(2, 3, 3, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.Ghoul_Ghast][RollHelper.GetRollWithMostEvenDistribution(4, 5, 8, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.Ghoul_Lacedon][RollHelper.GetRollWithMostEvenDistribution(2, 3, 3, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.Giant_Cloud][None] = new string[0];
                testCases[CreatureConstants.Giant_Fire][None] = new string[0];
                testCases[CreatureConstants.Giant_Frost][None] = new string[0];
                testCases[CreatureConstants.Giant_Hill][None] = new string[0];
                testCases[CreatureConstants.Giant_Stone][None] = new string[0];
                testCases[CreatureConstants.Giant_Stone_Elder][None] = new string[0];
                testCases[CreatureConstants.Giant_Storm][None] = new string[0];
                testCases[CreatureConstants.GibberingMouther][RollHelper.GetRollWithMostEvenDistribution(4, 5, 12, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.Girallon][RollHelper.GetRollWithMostEvenDistribution(7, 8, 10, true)] = GetData(SizeConstants.Large, 10, 10);
                testCases[CreatureConstants.Girallon][RollHelper.GetRollWithMostEvenDistribution(7, 11, 21, true)] = GetData(SizeConstants.Huge, 15, 15);
                testCases[CreatureConstants.Githyanki][None] = new string[0];
                testCases[CreatureConstants.Githzerai][None] = new string[0];
                testCases[CreatureConstants.Glabrezu][RollHelper.GetRollWithMostEvenDistribution(12, 13, 18, true)] = GetData(SizeConstants.Huge, 15, 15);
                testCases[CreatureConstants.Glabrezu][RollHelper.GetRollWithMostEvenDistribution(12, 19, 36, true)] = GetData(SizeConstants.Gargantuan, 20, 20);
                testCases[CreatureConstants.Gnoll][None] = new string[0];
                testCases[CreatureConstants.Gnome_Forest][None] = new string[0];
                testCases[CreatureConstants.Gnome_Rock][None] = new string[0];
                testCases[CreatureConstants.Gnome_Svirfneblin][None] = new string[0];
                testCases[CreatureConstants.Goblin][None] = new string[0];
                testCases[CreatureConstants.Golem_Clay][RollHelper.GetRollWithMostEvenDistribution(11, 12, 18, true)] = GetData(SizeConstants.Large, 10, 10);
                testCases[CreatureConstants.Golem_Clay][RollHelper.GetRollWithMostEvenDistribution(11, 19, 33, true)] = GetData(SizeConstants.Huge, 15, 15);
                testCases[CreatureConstants.Golem_Flesh][RollHelper.GetRollWithMostEvenDistribution(9, 10, 18, true)] = GetData(SizeConstants.Large, 10, 10);
                testCases[CreatureConstants.Golem_Flesh][RollHelper.GetRollWithMostEvenDistribution(9, 19, 27, true)] = GetData(SizeConstants.Huge, 15, 15);
                testCases[CreatureConstants.Golem_Iron][RollHelper.GetRollWithMostEvenDistribution(18, 19, 24, true)] = GetData(SizeConstants.Large, 10, 10);
                testCases[CreatureConstants.Golem_Iron][RollHelper.GetRollWithMostEvenDistribution(18, 25, 54, true)] = GetData(SizeConstants.Huge, 15, 15);
                testCases[CreatureConstants.Golem_Stone][RollHelper.GetRollWithMostEvenDistribution(14, 15, 21, true)] = GetData(SizeConstants.Large, 10, 10);
                testCases[CreatureConstants.Golem_Stone][RollHelper.GetRollWithMostEvenDistribution(14, 22, 42, true)] = GetData(SizeConstants.Huge, 15, 15);
                testCases[CreatureConstants.Golem_Stone_Greater][None] = new string[0];
                testCases[CreatureConstants.Gorgon][RollHelper.GetRollWithMostEvenDistribution(8, 9, 15, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.Gorgon][RollHelper.GetRollWithMostEvenDistribution(8, 16, 24, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.GrayOoze][RollHelper.GetRollWithMostEvenDistribution(3, 4, 6, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.GrayOoze][RollHelper.GetRollWithMostEvenDistribution(3, 7, 9, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.GrayRender][RollHelper.GetRollWithMostEvenDistribution(10, 11, 15, true)] = GetData(SizeConstants.Large, 10, 10);
                testCases[CreatureConstants.GrayRender][RollHelper.GetRollWithMostEvenDistribution(10, 16, 30, true)] = GetData(SizeConstants.Huge, 15, 15);
                testCases[CreatureConstants.GreenHag][None] = new string[0];
                testCases[CreatureConstants.Grick][RollHelper.GetRollWithMostEvenDistribution(2, 3, 4, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.Grick][RollHelper.GetRollWithMostEvenDistribution(2, 5, 6, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.Griffon][RollHelper.GetRollWithMostEvenDistribution(7, 8, 10, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.Griffon][RollHelper.GetRollWithMostEvenDistribution(7, 11, 21, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.Grig][RollHelper.GetRollWithMostEvenDistribution(0, 1, 3, true)] = GetData(SizeConstants.Tiny, 2.5, 0);
                testCases[CreatureConstants.Grig_WithFiddle][RollHelper.GetRollWithMostEvenDistribution(0, 1, 3, true)] = GetData(SizeConstants.Tiny, 2.5, 0);
                testCases[CreatureConstants.Grimlock][None] = new string[0];
                testCases[CreatureConstants.Gynosphinx][RollHelper.GetRollWithMostEvenDistribution(8, 9, 12, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.Gynosphinx][RollHelper.GetRollWithMostEvenDistribution(8, 13, 24, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.Halfling_Deep][None] = new string[0];
                testCases[CreatureConstants.Halfling_Lightfoot][None] = new string[0];
                testCases[CreatureConstants.Halfling_Tallfellow][None] = new string[0];
                testCases[CreatureConstants.Harpy][None] = new string[0];
                testCases[CreatureConstants.Hawk][None] = new string[0];
                testCases[CreatureConstants.HellHound][RollHelper.GetRollWithMostEvenDistribution(4, 5, 8, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.HellHound][RollHelper.GetRollWithMostEvenDistribution(4, 9, 12, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.HellHound_NessianWarhound][RollHelper.GetRollWithMostEvenDistribution(12, 13, 17, true)] = GetData(SizeConstants.Large, 10, 10);
                testCases[CreatureConstants.HellHound_NessianWarhound][RollHelper.GetRollWithMostEvenDistribution(12, 18, 24, true)] = GetData(SizeConstants.Huge, 15, 15);
                testCases[CreatureConstants.Hellcat_Bezekira][RollHelper.GetRollWithMostEvenDistribution(8, 9, 10, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.Hellcat_Bezekira][RollHelper.GetRollWithMostEvenDistribution(8, 11, 24, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.Hellwasp_Swarm][None] = new string[0];
                testCases[CreatureConstants.Hezrou][RollHelper.GetRollWithMostEvenDistribution(10, 11, 15, true)] = GetData(SizeConstants.Large, 10, 10);
                testCases[CreatureConstants.Hezrou][RollHelper.GetRollWithMostEvenDistribution(10, 16, 30, true)] = GetData(SizeConstants.Huge, 15, 15);
                testCases[CreatureConstants.Hieracosphinx][RollHelper.GetRollWithMostEvenDistribution(9, 10, 14, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.Hieracosphinx][RollHelper.GetRollWithMostEvenDistribution(9, 15, 27, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.Hippogriff][RollHelper.GetRollWithMostEvenDistribution(3, 4, 6, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.Hippogriff][RollHelper.GetRollWithMostEvenDistribution(3, 7, 9, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.Hobgoblin][None] = new string[0];
                testCases[CreatureConstants.Homunculus][RollHelper.GetRollWithMostEvenDistribution(2, 3, 6, true)] = GetData(SizeConstants.Tiny, 2.5, 0);
                testCases[CreatureConstants.HornedDevil_Cornugon][RollHelper.GetRollWithMostEvenDistribution(15, 16, 20, true)] = GetData(SizeConstants.Large, 10, 10);
                testCases[CreatureConstants.HornedDevil_Cornugon][RollHelper.GetRollWithMostEvenDistribution(15, 21, 45, true)] = GetData(SizeConstants.Huge, 15, 15);
                testCases[CreatureConstants.Horse_Heavy][None] = new string[0];
                testCases[CreatureConstants.Horse_Heavy_War][None] = new string[0];
                testCases[CreatureConstants.Horse_Light][None] = new string[0];
                testCases[CreatureConstants.Horse_Light_War][None] = new string[0];
                testCases[CreatureConstants.HoundArchon][RollHelper.GetRollWithMostEvenDistribution(6, 7, 9, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.HoundArchon][RollHelper.GetRollWithMostEvenDistribution(6, 10, 18, true)] = GetData(SizeConstants.Large, 10, 10);
                testCases[CreatureConstants.Howler][RollHelper.GetRollWithMostEvenDistribution(6, 7, 9, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.Howler][RollHelper.GetRollWithMostEvenDistribution(6, 10, 18, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.Human][None] = new string[0];
                testCases[CreatureConstants.Hydra_10Heads][None] = new string[0];
                testCases[CreatureConstants.Hydra_11Heads][None] = new string[0];
                testCases[CreatureConstants.Hydra_12Heads][None] = new string[0];
                testCases[CreatureConstants.Hydra_5Heads][None] = new string[0];
                testCases[CreatureConstants.Hydra_6Heads][None] = new string[0];
                testCases[CreatureConstants.Hydra_7Heads][None] = new string[0];
                testCases[CreatureConstants.Hydra_8Heads][None] = new string[0];
                testCases[CreatureConstants.Hydra_9Heads][None] = new string[0];
                testCases[CreatureConstants.Hyena][RollHelper.GetRollWithMostEvenDistribution(2, 3, 3, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.Hyena][RollHelper.GetRollWithMostEvenDistribution(2, 4, 5, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.IceDevil_Gelugon][RollHelper.GetRollWithMostEvenDistribution(14, 15, 28, true)] = GetData(SizeConstants.Large, 10, 10);
                testCases[CreatureConstants.IceDevil_Gelugon][RollHelper.GetRollWithMostEvenDistribution(14, 29, 42, true)] = GetData(SizeConstants.Huge, 15, 15);
                testCases[CreatureConstants.Imp][RollHelper.GetRollWithMostEvenDistribution(3, 4, 6, true)] = GetData(SizeConstants.Tiny, 2.5, 0);
                testCases[CreatureConstants.InvisibleStalker][RollHelper.GetRollWithMostEvenDistribution(8, 9, 12, true)] = GetData(SizeConstants.Large, 10, 10);
                testCases[CreatureConstants.InvisibleStalker][RollHelper.GetRollWithMostEvenDistribution(8, 13, 24, true)] = GetData(SizeConstants.Huge, 15, 15);
                testCases[CreatureConstants.Janni][RollHelper.GetRollWithMostEvenDistribution(6, 7, 9, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.Janni][RollHelper.GetRollWithMostEvenDistribution(6, 10, 18, true)] = GetData(SizeConstants.Large, 10, 10);
                testCases[CreatureConstants.Kobold][None] = new string[0];
                testCases[CreatureConstants.Kolyarut][RollHelper.GetRollWithMostEvenDistribution(13, 14, 22, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.Kolyarut][RollHelper.GetRollWithMostEvenDistribution(13, 23, 39, true)] = GetData(SizeConstants.Large, 10, 10);
                testCases[CreatureConstants.Kraken][RollHelper.GetRollWithMostEvenDistribution(20, 21, 32, true)] = GetData(SizeConstants.Gargantuan, 20, 15);
                testCases[CreatureConstants.Kraken][RollHelper.GetRollWithMostEvenDistribution(20, 33, 60, true)] = GetData(SizeConstants.Colossal, 30, 20);
                testCases[CreatureConstants.Krenshar][RollHelper.GetRollWithMostEvenDistribution(2, 3, 4, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.Krenshar][RollHelper.GetRollWithMostEvenDistribution(2, 5, 8, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.KuoToa][None] = new string[0];
                testCases[CreatureConstants.Lamia][RollHelper.GetRollWithMostEvenDistribution(9, 10, 13, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.Lamia][RollHelper.GetRollWithMostEvenDistribution(9, 14, 27, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.Lammasu][RollHelper.GetRollWithMostEvenDistribution(7, 8, 10, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.Lammasu][RollHelper.GetRollWithMostEvenDistribution(7, 11, 21, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.LanternArchon][RollHelper.GetRollWithMostEvenDistribution(1, 2, 4, true)] = GetData(SizeConstants.Small, 5, 5);
                testCases[CreatureConstants.Lemure][RollHelper.GetRollWithMostEvenDistribution(2, 3, 6, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.Leonal][RollHelper.GetRollWithMostEvenDistribution(12, 13, 18, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.Leonal][RollHelper.GetRollWithMostEvenDistribution(12, 19, 36, true)] = GetData(SizeConstants.Large, 10, 10);
                testCases[CreatureConstants.Leopard][RollHelper.GetRollWithMostEvenDistribution(3, 4, 5, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.Lillend][RollHelper.GetRollWithMostEvenDistribution(7, 8, 10, true)] = GetData(SizeConstants.Large, 10, 10);
                testCases[CreatureConstants.Lillend][RollHelper.GetRollWithMostEvenDistribution(7, 11, 21, true)] = GetData(SizeConstants.Huge, 15, 15);
                testCases[CreatureConstants.Lion][RollHelper.GetRollWithMostEvenDistribution(5, 6, 8, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.Lion_Dire][RollHelper.GetRollWithMostEvenDistribution(8, 9, 16, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.Lion_Dire][RollHelper.GetRollWithMostEvenDistribution(8, 17, 24, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.Lizard][None] = new string[0];
                testCases[CreatureConstants.Lizard_Monitor][RollHelper.GetRollWithMostEvenDistribution(3, 4, 5, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.Lizardfolk][None] = new string[0];
                testCases[CreatureConstants.Locathah][None] = new string[0];
                testCases[CreatureConstants.Locust_Swarm][None] = new string[0];
                testCases[CreatureConstants.Magmin][RollHelper.GetRollWithMostEvenDistribution(2, 3, 4, true)] = GetData(SizeConstants.Small, 5, 5);
                testCases[CreatureConstants.Magmin][RollHelper.GetRollWithMostEvenDistribution(2, 5, 6, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.MantaRay][RollHelper.GetRollWithMostEvenDistribution(4, 5, 6, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.Manticore][RollHelper.GetRollWithMostEvenDistribution(6, 7, 16, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.Manticore][RollHelper.GetRollWithMostEvenDistribution(6, 17, 18, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.Marilith][RollHelper.GetRollWithMostEvenDistribution(16, 17, 20, true)] = GetData(SizeConstants.Large, 10, 10);
                testCases[CreatureConstants.Marilith][RollHelper.GetRollWithMostEvenDistribution(16, 21, 48, true)] = GetData(SizeConstants.Huge, 15, 15);
                testCases[CreatureConstants.Marut][RollHelper.GetRollWithMostEvenDistribution(15, 16, 28, true)] = GetData(SizeConstants.Large, 10, 10);
                testCases[CreatureConstants.Marut][RollHelper.GetRollWithMostEvenDistribution(15, 29, 45, true)] = GetData(SizeConstants.Huge, 15, 15);
                testCases[CreatureConstants.Medusa][None] = new string[0];
                testCases[CreatureConstants.Megaraptor][RollHelper.GetRollWithMostEvenDistribution(8, 9, 16, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.Megaraptor][RollHelper.GetRollWithMostEvenDistribution(8, 17, 24, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.Mephit_Air][RollHelper.GetRollWithMostEvenDistribution(3, 4, 6, true)] = GetData(SizeConstants.Small, 5, 5);
                testCases[CreatureConstants.Mephit_Air][RollHelper.GetRollWithMostEvenDistribution(3, 7, 9, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.Mephit_Dust][RollHelper.GetRollWithMostEvenDistribution(3, 4, 6, true)] = GetData(SizeConstants.Small, 5, 5);
                testCases[CreatureConstants.Mephit_Dust][RollHelper.GetRollWithMostEvenDistribution(3, 7, 9, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.Mephit_Earth][RollHelper.GetRollWithMostEvenDistribution(3, 4, 6, true)] = GetData(SizeConstants.Small, 5, 5);
                testCases[CreatureConstants.Mephit_Earth][RollHelper.GetRollWithMostEvenDistribution(3, 7, 9, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.Mephit_Fire][RollHelper.GetRollWithMostEvenDistribution(3, 4, 6, true)] = GetData(SizeConstants.Small, 5, 5);
                testCases[CreatureConstants.Mephit_Fire][RollHelper.GetRollWithMostEvenDistribution(3, 7, 9, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.Mephit_Ice][RollHelper.GetRollWithMostEvenDistribution(3, 4, 6, true)] = GetData(SizeConstants.Small, 5, 5);
                testCases[CreatureConstants.Mephit_Ice][RollHelper.GetRollWithMostEvenDistribution(3, 7, 9, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.Mephit_Magma][RollHelper.GetRollWithMostEvenDistribution(3, 4, 6, true)] = GetData(SizeConstants.Small, 5, 5);
                testCases[CreatureConstants.Mephit_Magma][RollHelper.GetRollWithMostEvenDistribution(3, 7, 9, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.Mephit_Ooze][RollHelper.GetRollWithMostEvenDistribution(3, 4, 6, true)] = GetData(SizeConstants.Small, 5, 5);
                testCases[CreatureConstants.Mephit_Ooze][RollHelper.GetRollWithMostEvenDistribution(3, 7, 9, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.Mephit_Salt][RollHelper.GetRollWithMostEvenDistribution(3, 4, 6, true)] = GetData(SizeConstants.Small, 5, 5);
                testCases[CreatureConstants.Mephit_Salt][RollHelper.GetRollWithMostEvenDistribution(3, 7, 9, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.Mephit_Steam][RollHelper.GetRollWithMostEvenDistribution(3, 4, 6, true)] = GetData(SizeConstants.Small, 5, 5);
                testCases[CreatureConstants.Mephit_Steam][RollHelper.GetRollWithMostEvenDistribution(3, 7, 9, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.Mephit_Water][RollHelper.GetRollWithMostEvenDistribution(3, 4, 6, true)] = GetData(SizeConstants.Small, 5, 5);
                testCases[CreatureConstants.Mephit_Water][RollHelper.GetRollWithMostEvenDistribution(3, 7, 9, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.Merfolk][None] = new string[0];
                testCases[CreatureConstants.Mimic][RollHelper.GetRollWithMostEvenDistribution(7, 8, 10, true)] = GetData(SizeConstants.Large, 10, 10);
                testCases[CreatureConstants.Mimic][RollHelper.GetRollWithMostEvenDistribution(7, 11, 21, true)] = GetData(SizeConstants.Huge, 15, 15);
                testCases[CreatureConstants.MindFlayer][None] = new string[0];
                testCases[CreatureConstants.Minotaur][None] = new string[0];
                testCases[CreatureConstants.Mohrg][RollHelper.GetRollWithMostEvenDistribution(14, 15, 21, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.Mohrg][RollHelper.GetRollWithMostEvenDistribution(14, 22, 28, true)] = GetData(SizeConstants.Large, 10, 10);
                testCases[CreatureConstants.Monkey][RollHelper.GetRollWithMostEvenDistribution(1, 2, 3, true)] = GetData(SizeConstants.Small, 5, 5);
                testCases[CreatureConstants.Mule][None] = new string[0];
                testCases[CreatureConstants.Mummy][RollHelper.GetRollWithMostEvenDistribution(8, 9, 16, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.Mummy][RollHelper.GetRollWithMostEvenDistribution(8, 17, 24, true)] = GetData(SizeConstants.Large, 10, 10);
                testCases[CreatureConstants.Naga_Dark][RollHelper.GetRollWithMostEvenDistribution(9, 10, 13, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.Naga_Dark][RollHelper.GetRollWithMostEvenDistribution(9, 14, 27, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.Naga_Guardian][RollHelper.GetRollWithMostEvenDistribution(11, 12, 16, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.Naga_Guardian][RollHelper.GetRollWithMostEvenDistribution(11, 17, 33, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.Naga_Spirit][RollHelper.GetRollWithMostEvenDistribution(9, 10, 13, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.Naga_Spirit][RollHelper.GetRollWithMostEvenDistribution(9, 14, 27, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.Naga_Water][RollHelper.GetRollWithMostEvenDistribution(7, 8, 10, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.Naga_Water][RollHelper.GetRollWithMostEvenDistribution(7, 11, 21, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.Nalfeshnee][RollHelper.GetRollWithMostEvenDistribution(14, 15, 20, true)] = GetData(SizeConstants.Huge, 15, 15);
                testCases[CreatureConstants.Nalfeshnee][RollHelper.GetRollWithMostEvenDistribution(14, 21, 42, true)] = GetData(SizeConstants.Gargantuan, 20, 20);
                testCases[CreatureConstants.NightHag][RollHelper.GetRollWithMostEvenDistribution(8, 9, 16, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.Nightcrawler][RollHelper.GetRollWithMostEvenDistribution(25, 26, 50, true)] = GetData(SizeConstants.Colossal, 30, 20);
                testCases[CreatureConstants.Nightmare][RollHelper.GetRollWithMostEvenDistribution(6, 7, 10, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.Nightmare][RollHelper.GetRollWithMostEvenDistribution(6, 11, 18, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.Nightmare_Cauchemar][None] = new string[0];
                testCases[CreatureConstants.Nightwalker][RollHelper.GetRollWithMostEvenDistribution(21, 22, 31, true)] = GetData(SizeConstants.Huge, 15, 15);
                testCases[CreatureConstants.Nightwalker][RollHelper.GetRollWithMostEvenDistribution(21, 32, 42, true)] = GetData(SizeConstants.Gargantuan, 20, 20);
                testCases[CreatureConstants.Nightwing][RollHelper.GetRollWithMostEvenDistribution(17, 18, 25, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.Nightwing][RollHelper.GetRollWithMostEvenDistribution(17, 26, 34, true)] = GetData(SizeConstants.Gargantuan, 20, 15);
                testCases[CreatureConstants.Nixie][RollHelper.GetRollWithMostEvenDistribution(1, 2, 3, true)] = GetData(SizeConstants.Small, 5, 5);
                testCases[CreatureConstants.Nymph][RollHelper.GetRollWithMostEvenDistribution(6, 7, 12, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.OchreJelly][RollHelper.GetRollWithMostEvenDistribution(6, 7, 9, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.OchreJelly][RollHelper.GetRollWithMostEvenDistribution(6, 10, 18, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.Octopus][RollHelper.GetRollWithMostEvenDistribution(2, 3, 6, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.Octopus_Giant][RollHelper.GetRollWithMostEvenDistribution(8, 9, 12, true)] = GetData(SizeConstants.Large, 10, 10);
                testCases[CreatureConstants.Octopus_Giant][RollHelper.GetRollWithMostEvenDistribution(8, 13, 24, true)] = GetData(SizeConstants.Huge, 15, 15);
                testCases[CreatureConstants.Ogre][None] = new string[0];
                testCases[CreatureConstants.Ogre_Merrow][None] = new string[0];
                testCases[CreatureConstants.OgreMage][None] = new string[0];
                testCases[CreatureConstants.Orc][None] = new string[0];
                testCases[CreatureConstants.Orc_Half][None] = new string[0];
                testCases[CreatureConstants.Otyugh][RollHelper.GetRollWithMostEvenDistribution(6, 7, 8, true)] = GetData(SizeConstants.Large, 10, 10);
                testCases[CreatureConstants.Otyugh][RollHelper.GetRollWithMostEvenDistribution(6, 9, 18, true)] = GetData(SizeConstants.Huge, 15, 15);
                testCases[CreatureConstants.Owl][RollHelper.GetRollWithMostEvenDistribution(1, 2, 2, true)] = GetData(SizeConstants.Small, 5, 5);
                testCases[CreatureConstants.Owl_Giant][RollHelper.GetRollWithMostEvenDistribution(4, 5, 8, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.Owl_Giant][RollHelper.GetRollWithMostEvenDistribution(4, 9, 12, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.Owlbear][RollHelper.GetRollWithMostEvenDistribution(5, 6, 8, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.Owlbear][RollHelper.GetRollWithMostEvenDistribution(5, 9, 15, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.Pegasus][RollHelper.GetRollWithMostEvenDistribution(4, 5, 8, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.PhantomFungus][RollHelper.GetRollWithMostEvenDistribution(2, 3, 4, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.PhantomFungus][RollHelper.GetRollWithMostEvenDistribution(2, 5, 6, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.PhaseSpider][RollHelper.GetRollWithMostEvenDistribution(5, 6, 8, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.PhaseSpider][RollHelper.GetRollWithMostEvenDistribution(5, 9, 15, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.Phasm][RollHelper.GetRollWithMostEvenDistribution(15, 16, 21, true)] = GetData(SizeConstants.Huge, 15, 15);
                testCases[CreatureConstants.Phasm][RollHelper.GetRollWithMostEvenDistribution(15, 22, 45, true)] = GetData(SizeConstants.Gargantuan, 20, 20);
                testCases[CreatureConstants.PitFiend][RollHelper.GetRollWithMostEvenDistribution(18, 19, 36, true)] = GetData(SizeConstants.Large, 10, 10);
                testCases[CreatureConstants.PitFiend][RollHelper.GetRollWithMostEvenDistribution(18, 37, 54, true)] = GetData(SizeConstants.Huge, 15, 15);
                testCases[CreatureConstants.Pixie][RollHelper.GetRollWithMostEvenDistribution(1, 2, 3, true)] = GetData(SizeConstants.Small, 5, 5);
                testCases[CreatureConstants.Pixie_WithIrresistibleDance][RollHelper.GetRollWithMostEvenDistribution(1, 2, 3, true)] = GetData(SizeConstants.Small, 5, 5);
                testCases[CreatureConstants.Pony][None] = new string[0];
                testCases[CreatureConstants.Pony_War][None] = new string[0];
                testCases[CreatureConstants.Porpoise][RollHelper.GetRollWithMostEvenDistribution(2, 3, 4, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.Porpoise][RollHelper.GetRollWithMostEvenDistribution(2, 3, 4, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.PrayingMantis_Giant][RollHelper.GetRollWithMostEvenDistribution(4, 5, 8, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.PrayingMantis_Giant][RollHelper.GetRollWithMostEvenDistribution(4, 9, 12, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.Pseudodragon][RollHelper.GetRollWithMostEvenDistribution(2, 3, 4, true)] = GetData(SizeConstants.Tiny, 2.5, 0);
                testCases[CreatureConstants.PurpleWorm][RollHelper.GetRollWithMostEvenDistribution(16, 17, 32, true)] = GetData(SizeConstants.Gargantuan, 20, 15);
                testCases[CreatureConstants.PurpleWorm][RollHelper.GetRollWithMostEvenDistribution(16, 33, 48, true)] = GetData(SizeConstants.Colossal, 30, 20);
                testCases[CreatureConstants.Pyrohydra_10Heads][None] = new string[0];
                testCases[CreatureConstants.Pyrohydra_11Heads][None] = new string[0];
                testCases[CreatureConstants.Pyrohydra_12Heads][None] = new string[0];
                testCases[CreatureConstants.Pyrohydra_5Heads][None] = new string[0];
                testCases[CreatureConstants.Pyrohydra_6Heads][None] = new string[0];
                testCases[CreatureConstants.Pyrohydra_7Heads][None] = new string[0];
                testCases[CreatureConstants.Pyrohydra_8Heads][None] = new string[0];
                testCases[CreatureConstants.Pyrohydra_9Heads][None] = new string[0];
                testCases[CreatureConstants.Quasit][RollHelper.GetRollWithMostEvenDistribution(3, 4, 6, true)] = GetData(SizeConstants.Tiny, 2.5, 0);
                testCases[CreatureConstants.Rakshasa][None] = new string[0];
                testCases[CreatureConstants.Rast][RollHelper.GetRollWithMostEvenDistribution(4, 5, 6, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.Rast][RollHelper.GetRollWithMostEvenDistribution(4, 7, 12, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.Rat][None] = new string[0];
                testCases[CreatureConstants.Rat_Dire][RollHelper.GetRollWithMostEvenDistribution(1, 2, 3, true)] = GetData(SizeConstants.Small, 5, 5);
                testCases[CreatureConstants.Rat_Dire][RollHelper.GetRollWithMostEvenDistribution(1, 4, 6, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.Rat_Swarm][None] = new string[0];
                testCases[CreatureConstants.Raven][None] = new string[0];
                testCases[CreatureConstants.Ravid][RollHelper.GetRollWithMostEvenDistribution(3, 4, 4, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.Ravid][RollHelper.GetRollWithMostEvenDistribution(3, 5, 9, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.RazorBoar][RollHelper.GetRollWithMostEvenDistribution(15, 16, 30, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.RazorBoar][RollHelper.GetRollWithMostEvenDistribution(15, 31, 45, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.Remorhaz][RollHelper.GetRollWithMostEvenDistribution(7, 8, 14, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.Remorhaz][RollHelper.GetRollWithMostEvenDistribution(7, 15, 21, true)] = GetData(SizeConstants.Gargantuan, 20, 15);
                testCases[CreatureConstants.Retriever][RollHelper.GetRollWithMostEvenDistribution(10, 11, 15, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.Retriever][RollHelper.GetRollWithMostEvenDistribution(10, 16, 30, true)] = GetData(SizeConstants.Gargantuan, 20, 15);
                testCases[CreatureConstants.Rhinoceras][RollHelper.GetRollWithMostEvenDistribution(8, 9, 12, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.Rhinoceras][RollHelper.GetRollWithMostEvenDistribution(8, 13, 24, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.Roc][RollHelper.GetRollWithMostEvenDistribution(18, 19, 32, true)] = GetData(SizeConstants.Gargantuan, 20, 15);
                testCases[CreatureConstants.Roc][RollHelper.GetRollWithMostEvenDistribution(18, 33, 54, true)] = GetData(SizeConstants.Colossal, 30, 20);
                testCases[CreatureConstants.Roper][RollHelper.GetRollWithMostEvenDistribution(10, 11, 15, true)] = GetData(SizeConstants.Large, 10, 10);
                testCases[CreatureConstants.Roper][RollHelper.GetRollWithMostEvenDistribution(10, 16, 30, true)] = GetData(SizeConstants.Huge, 15, 15);
                testCases[CreatureConstants.RustMonster][RollHelper.GetRollWithMostEvenDistribution(5, 6, 8, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.RustMonster][RollHelper.GetRollWithMostEvenDistribution(5, 9, 15, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.Sahuagin][RollHelper.GetRollWithMostEvenDistribution(2, 3, 5, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.Sahuagin][RollHelper.GetRollWithMostEvenDistribution(2, 6, 10, true)] = GetData(SizeConstants.Large, 10, 10);
                testCases[CreatureConstants.Sahuagin_Malenti][RollHelper.GetRollWithMostEvenDistribution(2, 3, 5, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.Sahuagin_Malenti][RollHelper.GetRollWithMostEvenDistribution(2, 6, 10, true)] = GetData(SizeConstants.Large, 10, 10);
                testCases[CreatureConstants.Sahuagin_Mutant][RollHelper.GetRollWithMostEvenDistribution(2, 3, 5, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.Sahuagin_Mutant][RollHelper.GetRollWithMostEvenDistribution(2, 6, 10, true)] = GetData(SizeConstants.Large, 10, 10);
                testCases[CreatureConstants.Salamander_Average][RollHelper.GetRollWithMostEvenDistribution(9, 10, 14, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.Salamander_Flamebrother][RollHelper.GetRollWithMostEvenDistribution(4, 5, 6, true)] = GetData(SizeConstants.Small, 5, 5);
                testCases[CreatureConstants.Salamander_Noble][RollHelper.GetRollWithMostEvenDistribution(15, 16, 21, true)] = GetData(SizeConstants.Large, 10, 10);
                testCases[CreatureConstants.Salamander_Noble][RollHelper.GetRollWithMostEvenDistribution(15, 22, 45, true)] = GetData(SizeConstants.Huge, 15, 15);
                testCases[CreatureConstants.Satyr][RollHelper.GetRollWithMostEvenDistribution(5, 6, 10, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.Satyr_WithPipes][RollHelper.GetRollWithMostEvenDistribution(5, 6, 10, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.Scorpion_Monstrous_Colossal][RollHelper.GetRollWithMostEvenDistribution(40, 41, 60, true)] = GetData(SizeConstants.Colossal, 40, 30);
                testCases[CreatureConstants.Scorpion_Monstrous_Gargantuan][RollHelper.GetRollWithMostEvenDistribution(20, 21, 39, true)] = GetData(SizeConstants.Gargantuan, 20, 15);
                testCases[CreatureConstants.Scorpion_Monstrous_Huge][RollHelper.GetRollWithMostEvenDistribution(10, 11, 19, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.Scorpion_Monstrous_Large][RollHelper.GetRollWithMostEvenDistribution(5, 6, 9, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.Scorpion_Monstrous_Medium][RollHelper.GetRollWithMostEvenDistribution(2, 3, 4, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.Scorpion_Monstrous_Small][None] = new string[0];
                testCases[CreatureConstants.Scorpion_Monstrous_Tiny][None] = new string[0];
                testCases[CreatureConstants.Scorpionfolk][None] = new string[0];
                testCases[CreatureConstants.SeaCat][RollHelper.GetRollWithMostEvenDistribution(6, 7, 9, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.SeaCat][RollHelper.GetRollWithMostEvenDistribution(6, 10, 18, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.SeaHag][None] = new string[0];
                testCases[CreatureConstants.Shadow][RollHelper.GetRollWithMostEvenDistribution(3, 4, 9, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.Shadow_Greater][None] = new string[0];
                testCases[CreatureConstants.ShadowMastiff][RollHelper.GetRollWithMostEvenDistribution(4, 5, 6, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.ShadowMastiff][RollHelper.GetRollWithMostEvenDistribution(4, 7, 12, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.ShamblingMound][RollHelper.GetRollWithMostEvenDistribution(8, 9, 12, true)] = GetData(SizeConstants.Large, 10, 10);
                testCases[CreatureConstants.ShamblingMound][RollHelper.GetRollWithMostEvenDistribution(8, 13, 24, true)] = GetData(SizeConstants.Huge, 15, 15);
                testCases[CreatureConstants.Shark_Dire][RollHelper.GetRollWithMostEvenDistribution(18, 19, 32, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.Shark_Dire][RollHelper.GetRollWithMostEvenDistribution(18, 33, 54, true)] = GetData(SizeConstants.Gargantuan, 20, 15);
                testCases[CreatureConstants.Shark_Huge][RollHelper.GetRollWithMostEvenDistribution(10, 11, 17, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.Shark_Large][RollHelper.GetRollWithMostEvenDistribution(7, 8, 9, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.Shark_Medium][RollHelper.GetRollWithMostEvenDistribution(3, 4, 6, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.ShieldGuardian][RollHelper.GetRollWithMostEvenDistribution(15, 16, 24, true)] = GetData(SizeConstants.Large, 10, 10);
                testCases[CreatureConstants.ShieldGuardian][RollHelper.GetRollWithMostEvenDistribution(15, 25, 45, true)] = GetData(SizeConstants.Huge, 15, 15);
                testCases[CreatureConstants.ShockerLizard][RollHelper.GetRollWithMostEvenDistribution(2, 3, 4, true)] = GetData(SizeConstants.Small, 5, 5);
                testCases[CreatureConstants.ShockerLizard][RollHelper.GetRollWithMostEvenDistribution(2, 5, 6, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.Shrieker][RollHelper.GetRollWithMostEvenDistribution(2, 3, 3, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.Skum][RollHelper.GetRollWithMostEvenDistribution(2, 3, 4, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.Skum][RollHelper.GetRollWithMostEvenDistribution(2, 5, 6, true)] = GetData(SizeConstants.Large, 10, 10);
                testCases[CreatureConstants.Slaad_Blue][RollHelper.GetRollWithMostEvenDistribution(8, 9, 12, true)] = GetData(SizeConstants.Large, 10, 10);
                testCases[CreatureConstants.Slaad_Blue][RollHelper.GetRollWithMostEvenDistribution(8, 13, 24, true)] = GetData(SizeConstants.Huge, 15, 15);
                testCases[CreatureConstants.Slaad_Death][RollHelper.GetRollWithMostEvenDistribution(15, 16, 22, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.Slaad_Death][RollHelper.GetRollWithMostEvenDistribution(15, 23, 45, true)] = GetData(SizeConstants.Large, 10, 10);
                testCases[CreatureConstants.Slaad_Gray][RollHelper.GetRollWithMostEvenDistribution(10, 11, 15, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.Slaad_Gray][RollHelper.GetRollWithMostEvenDistribution(10, 16, 30, true)] = GetData(SizeConstants.Large, 10, 10);
                testCases[CreatureConstants.Slaad_Green][RollHelper.GetRollWithMostEvenDistribution(9, 10, 15, true)] = GetData(SizeConstants.Large, 10, 10);
                testCases[CreatureConstants.Slaad_Green][RollHelper.GetRollWithMostEvenDistribution(9, 16, 27, true)] = GetData(SizeConstants.Huge, 15, 15);
                testCases[CreatureConstants.Slaad_Red][RollHelper.GetRollWithMostEvenDistribution(7, 8, 10, true)] = GetData(SizeConstants.Large, 10, 10);
                testCases[CreatureConstants.Slaad_Red][RollHelper.GetRollWithMostEvenDistribution(7, 11, 21, true)] = GetData(SizeConstants.Huge, 15, 15);
                testCases[CreatureConstants.Snake_Constrictor][RollHelper.GetRollWithMostEvenDistribution(3, 4, 5, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.Snake_Constrictor][RollHelper.GetRollWithMostEvenDistribution(3, 6, 10, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.Snake_Constrictor_Giant][RollHelper.GetRollWithMostEvenDistribution(11, 12, 16, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.Snake_Constrictor_Giant][RollHelper.GetRollWithMostEvenDistribution(11, 17, 33, true)] = GetData(SizeConstants.Gargantuan, 20, 15);
                testCases[CreatureConstants.Snake_Viper_Huge][RollHelper.GetRollWithMostEvenDistribution(6, 7, 18, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.Snake_Viper_Large][None] = new string[0];
                testCases[CreatureConstants.Snake_Viper_Medium][None] = new string[0];
                testCases[CreatureConstants.Snake_Viper_Small][None] = new string[0];
                testCases[CreatureConstants.Snake_Viper_Tiny][None] = new string[0];
                testCases[CreatureConstants.Spectre][RollHelper.GetRollWithMostEvenDistribution(7, 8, 14, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Colossal][RollHelper.GetRollWithMostEvenDistribution(32, 33, 60, true)] = GetData(SizeConstants.Colossal, 40, 30);
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Gargantuan][RollHelper.GetRollWithMostEvenDistribution(16, 17, 31, true)] = GetData(SizeConstants.Gargantuan, 20, 15);
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Huge][RollHelper.GetRollWithMostEvenDistribution(8, 9, 15, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Large][RollHelper.GetRollWithMostEvenDistribution(4, 5, 7, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Medium][RollHelper.GetRollWithMostEvenDistribution(2, 3, 3, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Small][None] = new string[0];
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Tiny][None] = new string[0];
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Colossal][RollHelper.GetRollWithMostEvenDistribution(32, 33, 60, true)] = GetData(SizeConstants.Colossal, 40, 30);
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Gargantuan][RollHelper.GetRollWithMostEvenDistribution(16, 17, 31, true)] = GetData(SizeConstants.Gargantuan, 20, 15);
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Huge][RollHelper.GetRollWithMostEvenDistribution(8, 9, 15, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Large][RollHelper.GetRollWithMostEvenDistribution(4, 5, 7, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Medium][RollHelper.GetRollWithMostEvenDistribution(2, 3, 3, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Small][None] = new string[0];
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Tiny][None] = new string[0];
                testCases[CreatureConstants.Spider_Swarm][None] = new string[0];
                testCases[CreatureConstants.SpiderEater][RollHelper.GetRollWithMostEvenDistribution(4, 5, 12, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.Squid][RollHelper.GetRollWithMostEvenDistribution(3, 4, 6, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.Squid][RollHelper.GetRollWithMostEvenDistribution(3, 7, 11, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.Squid_Giant][RollHelper.GetRollWithMostEvenDistribution(12, 13, 18, true)] = GetData(SizeConstants.Huge, 15, 15);
                testCases[CreatureConstants.Squid_Giant][RollHelper.GetRollWithMostEvenDistribution(12, 19, 36, true)] = GetData(SizeConstants.Gargantuan, 20, 20);
                testCases[CreatureConstants.StagBeetle_Giant][RollHelper.GetRollWithMostEvenDistribution(7, 8, 10, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.StagBeetle_Giant][RollHelper.GetRollWithMostEvenDistribution(7, 11, 21, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.Stirge][None] = new string[0];
                testCases[CreatureConstants.Succubus][RollHelper.GetRollWithMostEvenDistribution(6, 7, 12, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.Tarrasque][RollHelper.GetRollWithMostEvenDistribution(48, 49, 100, true)] = GetData(SizeConstants.Colossal, 30, 20);
                testCases[CreatureConstants.Tendriculos][RollHelper.GetRollWithMostEvenDistribution(9, 10, 16, true)] = GetData(SizeConstants.Huge, 15, 15);
                testCases[CreatureConstants.Tendriculos][RollHelper.GetRollWithMostEvenDistribution(9, 17, 27, true)] = GetData(SizeConstants.Gargantuan, 20, 20);
                testCases[CreatureConstants.Thoqqua][RollHelper.GetRollWithMostEvenDistribution(3, 4, 9, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.Tiefling][None] = new string[0];
                testCases[CreatureConstants.Tiger][RollHelper.GetRollWithMostEvenDistribution(6, 7, 12, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.Tiger][RollHelper.GetRollWithMostEvenDistribution(6, 13, 18, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.Tiger_Dire][RollHelper.GetRollWithMostEvenDistribution(16, 17, 32, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.Tiger_Dire][RollHelper.GetRollWithMostEvenDistribution(16, 33, 48, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.Titan][RollHelper.GetRollWithMostEvenDistribution(20, 21, 30, true)] = GetData(SizeConstants.Huge, 15, 15);
                testCases[CreatureConstants.Titan][RollHelper.GetRollWithMostEvenDistribution(20, 31, 60, true)] = GetData(SizeConstants.Gargantuan, 20, 20);
                testCases[CreatureConstants.Toad][None] = new string[0];
                testCases[CreatureConstants.Tojanida_Adult][RollHelper.GetRollWithMostEvenDistribution(7, 8, 14, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.Tojanida_Elder][RollHelper.GetRollWithMostEvenDistribution(15, 16, 24, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.Tojanida_Juvenile][RollHelper.GetRollWithMostEvenDistribution(3, 4, 6, true)] = GetData(SizeConstants.Small, 5, 5);
                testCases[CreatureConstants.Treant][RollHelper.GetRollWithMostEvenDistribution(7, 8, 16, true)] = GetData(SizeConstants.Huge, 15, 15);
                testCases[CreatureConstants.Treant][RollHelper.GetRollWithMostEvenDistribution(7, 17, 21, true)] = GetData(SizeConstants.Gargantuan, 20, 20);
                testCases[CreatureConstants.Triceratops][RollHelper.GetRollWithMostEvenDistribution(16, 17, 32, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.Triceratops][RollHelper.GetRollWithMostEvenDistribution(16, 33, 48, true)] = GetData(SizeConstants.Gargantuan, 20, 15);
                testCases[CreatureConstants.Triton][RollHelper.GetRollWithMostEvenDistribution(3, 4, 9, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.Troglodyte][None] = new string[0];
                testCases[CreatureConstants.Troll][None] = new string[0];
                testCases[CreatureConstants.Troll_Scrag][None] = new string[0];
                testCases[CreatureConstants.TrumpetArchon][RollHelper.GetRollWithMostEvenDistribution(12, 13, 18, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.TrumpetArchon][RollHelper.GetRollWithMostEvenDistribution(12, 19, 36, true)] = GetData(SizeConstants.Large, 10, 10);
                testCases[CreatureConstants.Tyrannosaurus][RollHelper.GetRollWithMostEvenDistribution(18, 19, 36, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.Tyrannosaurus][RollHelper.GetRollWithMostEvenDistribution(18, 37, 54, true)] = GetData(SizeConstants.Gargantuan, 20, 15);
                testCases[CreatureConstants.Unicorn][RollHelper.GetRollWithMostEvenDistribution(4, 5, 8, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.UmberHulk][RollHelper.GetRollWithMostEvenDistribution(8, 9, 12, true)] = GetData(SizeConstants.Large, 10, 10);
                testCases[CreatureConstants.UmberHulk][RollHelper.GetRollWithMostEvenDistribution(8, 13, 24, true)] = GetData(SizeConstants.Huge, 15, 15);
                testCases[CreatureConstants.UmberHulk_TrulyHorrid][None] = new string[0];
                testCases[CreatureConstants.VampireSpawn][None] = new string[0];
                testCases[CreatureConstants.Vargouille][RollHelper.GetRollWithMostEvenDistribution(1, 2, 3, true)] = GetData(SizeConstants.Small, 5, 5);
                testCases[CreatureConstants.VioletFungus][RollHelper.GetRollWithMostEvenDistribution(2, 3, 6, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.Vrock][RollHelper.GetRollWithMostEvenDistribution(10, 11, 14, true)] = GetData(SizeConstants.Large, 10, 10);
                testCases[CreatureConstants.Vrock][RollHelper.GetRollWithMostEvenDistribution(10, 15, 30, true)] = GetData(SizeConstants.Huge, 15, 15);
                testCases[CreatureConstants.Wasp_Giant][RollHelper.GetRollWithMostEvenDistribution(5, 6, 8, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.Wasp_Giant][RollHelper.GetRollWithMostEvenDistribution(5, 9, 15, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.Weasel][None] = new string[0];
                testCases[CreatureConstants.Weasel_Dire][RollHelper.GetRollWithMostEvenDistribution(3, 4, 6, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.Weasel_Dire][RollHelper.GetRollWithMostEvenDistribution(3, 7, 9, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.Whale_Baleen][RollHelper.GetRollWithMostEvenDistribution(12, 13, 18, true)] = GetData(SizeConstants.Gargantuan, 20, 15);
                testCases[CreatureConstants.Whale_Baleen][RollHelper.GetRollWithMostEvenDistribution(12, 19, 36, true)] = GetData(SizeConstants.Colossal, 30, 20);
                testCases[CreatureConstants.Whale_Cachalot][RollHelper.GetRollWithMostEvenDistribution(12, 13, 18, true)] = GetData(SizeConstants.Gargantuan, 20, 15);
                testCases[CreatureConstants.Whale_Cachalot][RollHelper.GetRollWithMostEvenDistribution(12, 19, 36, true)] = GetData(SizeConstants.Colossal, 30, 20);
                testCases[CreatureConstants.Whale_Orca][RollHelper.GetRollWithMostEvenDistribution(9, 10, 13, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.Whale_Orca][RollHelper.GetRollWithMostEvenDistribution(9, 14, 27, true)] = GetData(SizeConstants.Gargantuan, 20, 15);
                testCases[CreatureConstants.Wight][RollHelper.GetRollWithMostEvenDistribution(4, 5, 8, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.WillOWisp][RollHelper.GetRollWithMostEvenDistribution(9, 10, 18, true)] = GetData(SizeConstants.Small, 5, 5);
                testCases[CreatureConstants.WinterWolf][RollHelper.GetRollWithMostEvenDistribution(6, 7, 9, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.WinterWolf][RollHelper.GetRollWithMostEvenDistribution(6, 10, 18, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.Wolf][RollHelper.GetRollWithMostEvenDistribution(2, 3, 3, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.Wolf][RollHelper.GetRollWithMostEvenDistribution(2, 4, 6, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.Wolf_Dire][RollHelper.GetRollWithMostEvenDistribution(6, 7, 18, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.Wolverine][RollHelper.GetRollWithMostEvenDistribution(3, 4, 5, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.Wolverine_Dire][RollHelper.GetRollWithMostEvenDistribution(5, 6, 15, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.Worg][RollHelper.GetRollWithMostEvenDistribution(4, 5, 6, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.Worg][RollHelper.GetRollWithMostEvenDistribution(4, 7, 12, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.Wraith][RollHelper.GetRollWithMostEvenDistribution(5, 6, 10, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.Wraith_Dread][RollHelper.GetRollWithMostEvenDistribution(16, 17, 32, true)] = GetData(SizeConstants.Large, 10, 10);
                testCases[CreatureConstants.Wyvern][RollHelper.GetRollWithMostEvenDistribution(7, 8, 10, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.Wyvern][RollHelper.GetRollWithMostEvenDistribution(7, 11, 21, true)] = GetData(SizeConstants.Gargantuan, 20, 15);
                testCases[CreatureConstants.Xill][RollHelper.GetRollWithMostEvenDistribution(5, 6, 8, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.Xill][RollHelper.GetRollWithMostEvenDistribution(5, 9, 15, true)] = GetData(SizeConstants.Large, 10, 10);
                testCases[CreatureConstants.Xorn_Average][RollHelper.GetRollWithMostEvenDistribution(7, 8, 14, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.Xorn_Elder][RollHelper.GetRollWithMostEvenDistribution(15, 16, 21, true)] = GetData(SizeConstants.Large, 10, 10);
                testCases[CreatureConstants.Xorn_Elder][RollHelper.GetRollWithMostEvenDistribution(15, 22, 45, true)] = GetData(SizeConstants.Huge, 15, 15);
                testCases[CreatureConstants.Xorn_Minor][RollHelper.GetRollWithMostEvenDistribution(3, 4, 6, true)] = GetData(SizeConstants.Small, 5, 5);
                testCases[CreatureConstants.YethHound][RollHelper.GetRollWithMostEvenDistribution(3, 4, 6, true)] = GetData(SizeConstants.Medium, 5, 5);
                testCases[CreatureConstants.YethHound][RollHelper.GetRollWithMostEvenDistribution(3, 7, 9, true)] = GetData(SizeConstants.Large, 10, 5);
                testCases[CreatureConstants.Yrthak][RollHelper.GetRollWithMostEvenDistribution(12, 13, 16, true)] = GetData(SizeConstants.Huge, 15, 10);
                testCases[CreatureConstants.Yrthak][RollHelper.GetRollWithMostEvenDistribution(12, 17, 36, true)] = GetData(SizeConstants.Gargantuan, 20, 15);
                testCases[CreatureConstants.YuanTi_Abomination][None] = new string[0];
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeArms][None] = new string[0];
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeHead][None] = new string[0];
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeTail][None] = new string[0];
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs][None] = new string[0];
                testCases[CreatureConstants.YuanTi_Pureblood][None] = new string[0];
                testCases[CreatureConstants.Zelekhut][RollHelper.GetRollWithMostEvenDistribution(8, 9, 16, true)] = GetData(SizeConstants.Large, 10, 10);
                testCases[CreatureConstants.Zelekhut][RollHelper.GetRollWithMostEvenDistribution(8, 17, 24, true)] = GetData(SizeConstants.Huge, 15, 15);

                foreach (var testCase in testCases)
                {
                    if (testCase.Value.Any() && testCase.Value.First().Key == None)
                    {
                        testCase.Value.Clear();
                    }

                    yield return new TestCaseData(testCase.Key, testCase.Value);
                }
            }
        }

        private static string[] GetData(string advancedSize, double space, double reach)
        {
            var data = new string[3];
            data[DataIndexConstants.AdvancementSelectionData.Reach] = reach.ToString();
            data[DataIndexConstants.AdvancementSelectionData.Size] = advancedSize;
            data[DataIndexConstants.AdvancementSelectionData.Space] = space.ToString();

            return data;
        }

        [TestCase(CreatureConstants.Types.Aberration, 4)]
        [TestCase(CreatureConstants.Types.Animal, 3)]
        [TestCase(CreatureConstants.Types.Construct, 4)]
        [TestCase(CreatureConstants.Types.Dragon, 2)]
        [TestCase(CreatureConstants.Types.Elemental, 4)]
        [TestCase(CreatureConstants.Types.Fey, 4)]
        [TestCase(CreatureConstants.Types.Giant, 4)]
        [TestCase(CreatureConstants.Types.Humanoid, 4)]
        [TestCase(CreatureConstants.Types.MagicalBeast, 3)]
        [TestCase(CreatureConstants.Types.MonstrousHumanoid, 3)]
        [TestCase(CreatureConstants.Types.Ooze, 4)]
        [TestCase(CreatureConstants.Types.Outsider, 2)]
        [TestCase(CreatureConstants.Types.Plant, 4)]
        [TestCase(CreatureConstants.Types.Undead, 4)]
        [TestCase(CreatureConstants.Types.Vermin, 4)]
        public void AdvancementChallengeRatingDivisor(string creatureType, int divisor)
        {
            var typesAndAmounts = new Dictionary<string, int>();
            typesAndAmounts[creatureType] = divisor;

            AssertTypesAndAmounts(creatureType, typesAndAmounts);
        }
    }
}
