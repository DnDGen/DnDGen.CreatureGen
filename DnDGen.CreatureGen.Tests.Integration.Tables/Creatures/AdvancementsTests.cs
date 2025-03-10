using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Selectors.Collections;
using DnDGen.CreatureGen.Selectors.Selections;
using DnDGen.CreatureGen.Tables;
using DnDGen.CreatureGen.Tests.Integration.Tables.Helpers;
using DnDGen.CreatureGen.Tests.Integration.TestData;
using DnDGen.Infrastructure.Selectors.Collections;
using DnDGen.RollGen;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Integration.Tables.Creatures
{
    [TestFixture]
    public class AdvancementsTests : CollectionTests
    {
        private Dice dice;
        private ICreatureDataSelector creatureDataSelector;
        private Dictionary<string, string[]> advancements;
        private ICollectionTypeAndAmountSelector collectionTypeAndAmountSelector;
        private ICollectionSelector collectionSelector;
        private Dictionary<string, int> typeDivisors;
        private SpaceReachHelper spaceReachHelper;

        protected override string tableName => TableNameConstants.TypeAndAmount.Advancements;

        [OneTimeSetUp]
        public void OnetimeSetup()
        {
            advancements = GetAdvancementsTestData();
            typeDivisors = new Dictionary<string, int>
            {
                [CreatureConstants.Types.Aberration] = 4,
                [CreatureConstants.Types.Animal] = 3,
                [CreatureConstants.Types.Construct] = 4,
                [CreatureConstants.Types.Dragon] = 2,
                [CreatureConstants.Types.Elemental] = 4,
                [CreatureConstants.Types.Fey] = 4,
                [CreatureConstants.Types.Giant] = 4,
                [CreatureConstants.Types.Humanoid] = 4,
                [CreatureConstants.Types.MagicalBeast] = 3,
                [CreatureConstants.Types.MonstrousHumanoid] = 3,
                [CreatureConstants.Types.Ooze] = 4,
                [CreatureConstants.Types.Outsider] = 2,
                [CreatureConstants.Types.Plant] = 4,
                [CreatureConstants.Types.Undead] = 4,
                [CreatureConstants.Types.Vermin] = 4,
            };

            spaceReachHelper = GetNewInstanceOf<SpaceReachHelper>();
        }

        [SetUp]
        public void Setup()
        {
            dice = GetNewInstanceOf<Dice>();
            creatureDataSelector = GetNewInstanceOf<ICreatureDataSelector>();
            collectionTypeAndAmountSelector = GetNewInstanceOf<ICollectionTypeAndAmountSelector>();
            collectionSelector = GetNewInstanceOf<ICollectionSelector>();
        }

        [Test]
        public void AdvancementsNames()
        {
            var creatures = CreatureConstants.GetAll();
            var creatureTypes = CreatureConstants.Types.GetAll();

            var names = creatures.Union(creatureTypes);

            AssertCollectionNames(names);
        }

        [TestCaseSource(typeof(CreatureTestData), nameof(CreatureTestData.Creatures))]
        public void Advancement(string creature)
        {
            Assert.That(advancements, Contains.Key(creature));

            AssertHitDieOnlyIncreases(creature);
            AssertSizeOnlyIncreases(creature);
            AssertCollection(creature, advancements[creature]);
        }

        private void AssertHitDieOnlyIncreases(string creature)
        {
            if (!advancements[creature].Any())
                return;

            var rolls = advancements[creature]
                .Select(Infrastructure.Helpers.DataHelper.Parse<AdvancementDataSelection>)
                .Select(s => s.AdditionalHitDiceRoll);

            foreach (var roll in rolls)
            {
                var minimum = dice.Roll(roll).AsPotentialMinimum();
                Assert.That(minimum, Is.Positive);

                var maximum = dice.Roll(roll).AsPotentialMaximum();

                foreach (var otherRoll in rolls.Except([roll]))
                {
                    var otherMinimum = dice.Roll(otherRoll).AsPotentialMinimum();
                    var otherMaximum = dice.Roll(otherRoll).AsPotentialMaximum();

                    Assert.That(otherMaximum, Is.Not.EqualTo(minimum).And.Not.EqualTo(maximum), $"{roll} vs {otherRoll}");
                    Assert.That(otherMinimum, Is.Not.EqualTo(minimum).And.Not.EqualTo(maximum), $"{roll} vs {otherRoll}");

                    if (otherMinimum < maximum)
                        Assert.That(otherMaximum, Is.LessThan(minimum), $"{roll} vs {otherRoll}");
                    else if (otherMaximum > minimum)
                        Assert.That(otherMinimum, Is.GreaterThan(maximum), $"{roll} vs {otherRoll}");
                }
            }
        }

        private void AssertSizeOnlyIncreases(string creature)
        {
            if (!advancements[creature].Any())
                return;

            var sizes = advancements[creature]
                .Select(Infrastructure.Helpers.DataHelper.Parse<AdvancementDataSelection>)
                .Select(s => s.Size);
            Assert.That(sizes, Is.Unique);

            var creatureData = creatureDataSelector.SelectFor(creature);
            var orderedSizes = SizeConstants.GetOrdered();
            var originalSizeIndex = Array.IndexOf(orderedSizes, creatureData.Size);

            foreach (var size in sizes)
            {
                var sizeIndex = Array.IndexOf(orderedSizes, size);
                Assert.That(sizeIndex, Is.AtLeast(originalSizeIndex), $"{size} >= {creatureData.Size}");
            }

            foreach (var advancementData in advancements[creature])
            {
                var advancement = Infrastructure.Helpers.DataHelper.Parse<AdvancementDataSelection>(advancementData);
                var size = advancement.Size;
                var sizeIndex = Array.IndexOf(orderedSizes, size);

                var roll = advancement.AdditionalHitDiceRoll;
                var minimum = dice.Roll(roll).AsPotentialMinimum();

                foreach (var otherAdvancementData in advancements[creature].Except([advancementData]))
                {
                    var otherAdvancement = Infrastructure.Helpers.DataHelper.Parse<AdvancementDataSelection>(otherAdvancementData);
                    var otherSize = otherAdvancement.Size;
                    var otherSizeIndex = Array.IndexOf(orderedSizes, otherSize);

                    var otherRoll = otherAdvancement.AdditionalHitDiceRoll;
                    var otherMinimum = dice.Roll(otherRoll).AsPotentialMinimum();

                    if (minimum < otherMinimum)
                        Assert.That(sizeIndex, Is.LessThan(otherSizeIndex), $"{size} < {otherSize}");
                    else
                        Assert.That(sizeIndex, Is.GreaterThan(otherSizeIndex), $"{size} > {otherSize}");
                }
            }
        }

        private Dictionary<string, string[]> GetAdvancementsTestData()
        {
            var testCases = new Dictionary<string, string[]>();
            var creatures = CreatureConstants.GetAll();

            foreach (var creature in creatures)
            {
                testCases[creature] = [];
            }

            testCases[CreatureConstants.Aboleth] =
            [
                GetData(CreatureConstants.Aboleth, SizeConstants.Huge, 9, 16),
                GetData(CreatureConstants.Aboleth, SizeConstants.Gargantuan, 17, 24),
            ];
            testCases[CreatureConstants.Achaierai] =
            [
                GetData(CreatureConstants.Achaierai, SizeConstants.Large, 7, 12),
                GetData(CreatureConstants.Achaierai, SizeConstants.Huge, 13, 18),
            ];
            testCases[CreatureConstants.Allip] =
            [
                GetData(CreatureConstants.Allip, SizeConstants.Medium, 5, 12),
            ];
            testCases[CreatureConstants.Androsphinx] =
            [
                GetData(CreatureConstants.Androsphinx, SizeConstants.Large, 13, 18),
                GetData(CreatureConstants.Androsphinx, SizeConstants.Huge, 19, 36),
            ];
            testCases[CreatureConstants.Angel_AstralDeva] =
            [
                GetData(CreatureConstants.Angel_AstralDeva, SizeConstants.Medium, 13, 18),
                GetData(CreatureConstants.Angel_AstralDeva, SizeConstants.Large, 19, 36),
            ];
            testCases[CreatureConstants.Angel_Planetar] =
            [
                GetData(CreatureConstants.Angel_Planetar, SizeConstants.Large, 15, 21),
                GetData(CreatureConstants.Angel_Planetar, SizeConstants.Huge, 22, 42),
            ];
            testCases[CreatureConstants.Angel_Solar] =
            [
                GetData(CreatureConstants.Angel_Solar, SizeConstants.Large, 23, 33),
                GetData(CreatureConstants.Angel_Solar, SizeConstants.Huge, 34, 66),
            ];
            testCases[CreatureConstants.Ankheg] =
            [
                GetData(CreatureConstants.Ankheg, SizeConstants.Large, 4, 4),
                GetData(CreatureConstants.Ankheg, SizeConstants.Huge, 5, 9),
            ];
            testCases[CreatureConstants.Ant_Giant_Queen] =
            [
                GetData(CreatureConstants.Ant_Giant_Queen, SizeConstants.Large, 5, 6),
                GetData(CreatureConstants.Ant_Giant_Queen, SizeConstants.Huge, 7, 8),
            ];
            testCases[CreatureConstants.Ant_Giant_Soldier] =
            [
                GetData(CreatureConstants.Ant_Giant_Soldier, SizeConstants.Medium, 3, 4),
                GetData(CreatureConstants.Ant_Giant_Soldier, SizeConstants.Large, 5, 6),
            ];
            testCases[CreatureConstants.Ant_Giant_Worker] =
            [
                GetData(CreatureConstants.Ant_Giant_Worker, SizeConstants.Medium, 3, 4),
                GetData(CreatureConstants.Ant_Giant_Worker, SizeConstants.Large, 5, 6),
            ];
            testCases[CreatureConstants.Ape] =
            [
                GetData(CreatureConstants.Ape, SizeConstants.Large, 5, 8),
            ];
            testCases[CreatureConstants.Ape_Dire] =
            [
                GetData(CreatureConstants.Ape_Dire, SizeConstants.Large, 6, 15),
            ];
            testCases[CreatureConstants.Arrowhawk_Adult] =
            [
                GetData(CreatureConstants.Arrowhawk_Adult, SizeConstants.Medium, 8, 14),
            ];
            testCases[CreatureConstants.Arrowhawk_Elder] =
            [
                GetData(CreatureConstants.Arrowhawk_Elder, SizeConstants.Large, 16, 24),
            ];
            testCases[CreatureConstants.Arrowhawk_Juvenile] =
            [
                GetData(CreatureConstants.Arrowhawk_Juvenile, SizeConstants.Small, 4, 6),
            ];
            testCases[CreatureConstants.AssassinVine] =
            [
                GetData(CreatureConstants.AssassinVine, SizeConstants.Huge, 5, 16),
                GetData(CreatureConstants.AssassinVine, SizeConstants.Gargantuan,  17, 32),
                GetData(CreatureConstants.AssassinVine, SizeConstants.Colossal, 33, 100),
            ];
            testCases[CreatureConstants.Athach] =
            [
                GetData(CreatureConstants.Athach, SizeConstants.Huge, 15, 28),
            ];
            testCases[CreatureConstants.Avoral] =
            [
                GetData(CreatureConstants.Avoral, SizeConstants.Medium, 8, 14),
                GetData(CreatureConstants.Avoral, SizeConstants.Large, 15, 21),
            ];
            testCases[CreatureConstants.Babau] =
            [
                GetData(CreatureConstants.Babau, SizeConstants.Large, 8, 14),
                GetData(CreatureConstants.Babau, SizeConstants.Huge, 15, 21),
            ];
            testCases[CreatureConstants.Baboon] =
            [
                GetData(CreatureConstants.Baboon, SizeConstants.Medium, 2, 3),
            ];
            testCases[CreatureConstants.Badger] =
            [
                GetData(CreatureConstants.Badger, SizeConstants.Small, 2, 2),
            ];
            testCases[CreatureConstants.Badger_Dire] =
            [
                GetData(CreatureConstants.Badger_Dire, SizeConstants.Large, 4, 9),
            ];
            testCases[CreatureConstants.Balor] =
            [
                GetData(CreatureConstants.Balor, SizeConstants.Large, 21, 30),
                GetData(CreatureConstants.Balor, SizeConstants.Huge, 31, 60),
            ];
            testCases[CreatureConstants.BarbedDevil_Hamatula] =
            [
                GetData(CreatureConstants.BarbedDevil_Hamatula, SizeConstants.Medium, 13, 24),
                GetData(CreatureConstants.BarbedDevil_Hamatula, SizeConstants.Large, 25, 36),
            ];
            testCases[CreatureConstants.Barghest] =
            [
                GetData(CreatureConstants.Barghest, SizeConstants.Medium, 7, 8),
            ];
            testCases[CreatureConstants.Barghest_Greater] =
            [
                GetData(CreatureConstants.Barghest_Greater, SizeConstants.Large, 10, 18),
            ];
            testCases[CreatureConstants.Basilisk] =
            [
                GetData(CreatureConstants.Basilisk, SizeConstants.Medium, 7, 10),
                GetData(CreatureConstants.Basilisk, SizeConstants.Large, 11, 18),
            ];
            testCases[CreatureConstants.Bat_Dire] =
            [
                GetData(CreatureConstants.Bat_Dire, SizeConstants.Large, 5, 12),
            ];
            testCases[CreatureConstants.Bear_Black] =
            [
                GetData(CreatureConstants.Bear_Black, SizeConstants.Medium, 4, 5),
            ];
            testCases[CreatureConstants.Bear_Brown] =
            [
                GetData(CreatureConstants.Bear_Brown, SizeConstants.Large, 7, 10),
            ];
            testCases[CreatureConstants.Bear_Polar] =
            [
                GetData(CreatureConstants.Bear_Polar, SizeConstants.Large, 9, 12),
            ];
            testCases[CreatureConstants.Bear_Dire] =
            [
                GetData(CreatureConstants.Bear_Dire, SizeConstants.Large, 13, 16),
                GetData(CreatureConstants.Bear_Dire, SizeConstants.Huge, 17, 36),
            ];
            testCases[CreatureConstants.BeardedDevil_Barbazu] =
            [
                GetData(CreatureConstants.BeardedDevil_Barbazu, SizeConstants.Medium, 7, 9),
                GetData(CreatureConstants.BeardedDevil_Barbazu, SizeConstants.Large, 10, 18),
            ];
            testCases[CreatureConstants.Bebilith] =
            [
                GetData(CreatureConstants.Bebilith, SizeConstants.Huge, 13, 18),
                GetData(CreatureConstants.Bebilith, SizeConstants.Gargantuan, 19, 36),
            ];
            testCases[CreatureConstants.Bee_Giant] =
            [
                GetData(CreatureConstants.Bee_Giant, SizeConstants.Medium, 4, 6),
                GetData(CreatureConstants.Bee_Giant, SizeConstants.Large, 7, 9),
            ];
            testCases[CreatureConstants.Behir] =
            [
                GetData(CreatureConstants.Behir, SizeConstants.Huge, 10, 13),
                GetData(CreatureConstants.Behir, SizeConstants.Gargantuan, 14, 27),
            ];
            testCases[CreatureConstants.Beholder] =
            [
                GetData(CreatureConstants.Beholder, SizeConstants.Large, 12, 16),
                GetData(CreatureConstants.Beholder, SizeConstants.Huge, 17, 33),
            ];
            testCases[CreatureConstants.Beholder_Gauth] =
            [
                GetData(CreatureConstants.Beholder_Gauth, SizeConstants.Medium, 7, 12),
                GetData(CreatureConstants.Beholder_Gauth, SizeConstants.Large, 13, 18),
            ];
            testCases[CreatureConstants.Belker] =
            [
                GetData(CreatureConstants.Belker, SizeConstants.Large, 8, 10),
                GetData(CreatureConstants.Belker, SizeConstants.Huge, 11, 21),
            ];
            testCases[CreatureConstants.Bison] =
            [
                GetData(CreatureConstants.Bison, SizeConstants.Large, 6, 7),
            ];
            testCases[CreatureConstants.BlackPudding] =
            [
                GetData(CreatureConstants.BlackPudding, SizeConstants.Huge, 11, 15),
            ];
            testCases[CreatureConstants.BlinkDog] =
            [
                GetData(CreatureConstants.BlinkDog, SizeConstants.Medium, 5, 7),
                GetData(CreatureConstants.BlinkDog, SizeConstants.Large, 8, 12),
            ];
            testCases[CreatureConstants.Boar] =
            [
                GetData(CreatureConstants.Boar, SizeConstants.Medium, 4, 5),
            ];
            testCases[CreatureConstants.Boar_Dire] =
            [
                GetData(CreatureConstants.Boar_Dire, SizeConstants.Large, 8, 16),
                GetData(CreatureConstants.Boar_Dire, SizeConstants.Huge, 17, 21),
            ];
            testCases[CreatureConstants.Bodak] =
            [
                GetData(CreatureConstants.Bodak, SizeConstants.Medium, 10, 13),
                GetData(CreatureConstants.Bodak, SizeConstants.Large, 14, 27),
            ];
            testCases[CreatureConstants.BombardierBeetle_Giant] =
            [
                GetData(CreatureConstants.BombardierBeetle_Giant, SizeConstants.Medium, 3, 4),
                GetData(CreatureConstants.BombardierBeetle_Giant, SizeConstants.Large, 5, 6),
            ];
            testCases[CreatureConstants.BoneDevil_Osyluth] =
            [
                GetData(CreatureConstants.BoneDevil_Osyluth, SizeConstants.Large, 11, 20),
                GetData(CreatureConstants.BoneDevil_Osyluth, SizeConstants.Huge, 21, 30),
            ];
            testCases[CreatureConstants.Bralani] =
            [
                GetData(CreatureConstants.Bralani, SizeConstants.Medium, 7, 12),
                GetData(CreatureConstants.Bralani, SizeConstants.Large, 13, 18),
            ];
            testCases[CreatureConstants.Bulette] =
            [
                GetData(CreatureConstants.Bulette, SizeConstants.Huge, 10, 16),
                GetData(CreatureConstants.Bulette, SizeConstants.Gargantuan, 17, 27),
            ];
            testCases[CreatureConstants.CarrionCrawler] =
            [
                GetData(CreatureConstants.CarrionCrawler, SizeConstants.Large, 4, 6),
                GetData(CreatureConstants.CarrionCrawler, SizeConstants.Huge, 7, 9),
            ];
            testCases[CreatureConstants.Centipede_Monstrous_Colossal] =
            [
                GetData(CreatureConstants.Centipede_Monstrous_Colossal, SizeConstants.Colossal, 25, 48),
            ];
            testCases[CreatureConstants.Centipede_Monstrous_Gargantuan] =
            [
                GetData(CreatureConstants.Centipede_Monstrous_Gargantuan, SizeConstants.Gargantuan, 13, 23),
            ];
            testCases[CreatureConstants.Centipede_Monstrous_Huge] =
            [
                GetData(CreatureConstants.Centipede_Monstrous_Huge, SizeConstants.Huge, 7, 11),
            ];
            testCases[CreatureConstants.Centipede_Monstrous_Large] =
            [
                GetData(CreatureConstants.Centipede_Monstrous_Large, SizeConstants.Large, 4, 5),
            ];
            testCases[CreatureConstants.ChainDevil_Kyton] =
            [
                GetData(CreatureConstants.ChainDevil_Kyton, SizeConstants.Medium, 9, 16),
            ];
            testCases[CreatureConstants.ChaosBeast] =
            [
                GetData(CreatureConstants.ChaosBeast, SizeConstants.Medium, 9, 12),
                GetData(CreatureConstants.ChaosBeast, SizeConstants.Large, 13, 24),
            ];
            testCases[CreatureConstants.Cheetah] =
            [
                GetData(CreatureConstants.Cheetah, SizeConstants.Medium, 4, 5),
            ];
            testCases[CreatureConstants.Chimera_Black] =
            [
                GetData(CreatureConstants.Chimera_Black, SizeConstants.Large, 10, 13),
                GetData(CreatureConstants.Chimera_Black, SizeConstants.Huge, 14, 27),
            ];
            testCases[CreatureConstants.Chimera_Blue] =
            [
                GetData(CreatureConstants.Chimera_Blue, SizeConstants.Large, 10, 13),
                GetData(CreatureConstants.Chimera_Blue, SizeConstants.Huge, 14, 27),
            ];
            testCases[CreatureConstants.Chimera_Green] =
            [
                GetData(CreatureConstants.Chimera_Green, SizeConstants.Large, 10, 13),
                GetData(CreatureConstants.Chimera_Green, SizeConstants.Huge, 14, 27),
            ];
            testCases[CreatureConstants.Chimera_Red] =
            [
                GetData(CreatureConstants.Chimera_Red, SizeConstants.Large, 10, 13),
                GetData(CreatureConstants.Chimera_Red, SizeConstants.Huge, 14, 27),
            ];
            testCases[CreatureConstants.Chimera_White] =
            [
                GetData(CreatureConstants.Chimera_White, SizeConstants.Large, 10, 13),
                GetData(CreatureConstants.Chimera_White, SizeConstants.Huge, 14, 27),
            ];
            testCases[CreatureConstants.Choker] =
            [
                GetData(CreatureConstants.Choker, SizeConstants.Small, 4, 6),
                GetData(CreatureConstants.Choker, SizeConstants.Medium, 7, 12),
            ];
            testCases[CreatureConstants.Chuul] =
            [
                GetData(CreatureConstants.Chuul, SizeConstants.Large, 12, 16),
                GetData(CreatureConstants.Chuul, SizeConstants.Huge, 17, 33),
            ];
            testCases[CreatureConstants.Cloaker] =
            [
                GetData(CreatureConstants.Cloaker, SizeConstants.Large, 7, 9),
                GetData(CreatureConstants.Cloaker, SizeConstants.Huge, 10, 18),
            ];
            testCases[CreatureConstants.Cockatrice] =
            [
                GetData(CreatureConstants.Cockatrice, SizeConstants.Small, 6, 8),
                GetData(CreatureConstants.Cockatrice, SizeConstants.Medium, 9, 15),
            ];
            testCases[CreatureConstants.Couatl] =
            [
                GetData(CreatureConstants.Couatl, SizeConstants.Large, 10, 13),
                GetData(CreatureConstants.Couatl, SizeConstants.Huge, 14, 27),
            ];
            testCases[CreatureConstants.Criosphinx] =
            [
                GetData(CreatureConstants.Criosphinx, SizeConstants.Large, 11, 15),
                GetData(CreatureConstants.Criosphinx, SizeConstants.Huge, 16, 30),
            ];
            testCases[CreatureConstants.Crocodile] =
            [
                GetData(CreatureConstants.Crocodile, SizeConstants.Medium, 4, 5),
            ];
            testCases[CreatureConstants.Crocodile_Giant] =
            [
                GetData(CreatureConstants.Crocodile_Giant, SizeConstants.Huge, 8, 14),
            ];
            testCases[CreatureConstants.Darkmantle] =
            [
                GetData(CreatureConstants.Darkmantle, SizeConstants.Small, 2, 3),
            ];
            testCases[CreatureConstants.Deinonychus] =
            [
                GetData(CreatureConstants.Deinonychus, SizeConstants.Huge, 5, 8),
            ];
            testCases[CreatureConstants.Delver] =
            [
                GetData(CreatureConstants.Delver, SizeConstants.Huge, 16, 30),
                GetData(CreatureConstants.Delver, SizeConstants.Gargantuan, 31, 45),
            ];
            testCases[CreatureConstants.Destrachan] =
            [
                GetData(CreatureConstants.Destrachan, SizeConstants.Large, 9, 16),
                GetData(CreatureConstants.Destrachan, SizeConstants.Huge, 17, 24),
            ];
            testCases[CreatureConstants.Devourer] =
            [
                GetData(CreatureConstants.Devourer, SizeConstants.Large, 13, 24),
                GetData(CreatureConstants.Devourer, SizeConstants.Huge, 25, 36),
            ];
            testCases[CreatureConstants.Digester] =
            [
                GetData(CreatureConstants.Digester, SizeConstants.Medium, 9, 12),
                GetData(CreatureConstants.Digester, SizeConstants.Large, 13, 24),
            ];
            testCases[CreatureConstants.DisplacerBeast] =
            [
                GetData(CreatureConstants.DisplacerBeast, SizeConstants.Large, 7, 9),
                GetData(CreatureConstants.DisplacerBeast, SizeConstants.Huge, 10, 18),
            ];
            testCases[CreatureConstants.Djinni] =
            [
                GetData(CreatureConstants.Djinni, SizeConstants.Large, 8, 10),
                GetData(CreatureConstants.Djinni, SizeConstants.Huge, 11, 21),
            ];
            testCases[CreatureConstants.Djinni_Noble] =
            [
                GetData(CreatureConstants.Djinni_Noble, SizeConstants.Large, 11, 15),
                GetData(CreatureConstants.Djinni_Noble, SizeConstants.Huge, 16, 30),
            ];
            testCases[CreatureConstants.Dragon_Black_Wyrmling] = [GetData(CreatureConstants.Dragon_Black_Wyrmling, SizeConstants.Tiny, 5, 6)];
            testCases[CreatureConstants.Dragon_Black_VeryYoung] = [GetData(CreatureConstants.Dragon_Black_VeryYoung, SizeConstants.Small, 8, 9)];
            testCases[CreatureConstants.Dragon_Black_Young] = [GetData(CreatureConstants.Dragon_Black_Young, SizeConstants.Medium, 11, 12)];
            testCases[CreatureConstants.Dragon_Black_Juvenile] = [GetData(CreatureConstants.Dragon_Black_Juvenile, SizeConstants.Medium, 14, 15)];
            testCases[CreatureConstants.Dragon_Black_YoungAdult] = [GetData(CreatureConstants.Dragon_Black_YoungAdult, SizeConstants.Large, 17, 18)];
            testCases[CreatureConstants.Dragon_Black_Adult] = [GetData(CreatureConstants.Dragon_Black_Adult, SizeConstants.Large, 20, 21)];
            testCases[CreatureConstants.Dragon_Black_MatureAdult] = [GetData(CreatureConstants.Dragon_Black_MatureAdult, SizeConstants.Huge, 23, 24)];
            testCases[CreatureConstants.Dragon_Black_Old] = [GetData(CreatureConstants.Dragon_Black_Old, SizeConstants.Huge, 25, 27)];
            testCases[CreatureConstants.Dragon_Black_VeryOld] = [GetData(CreatureConstants.Dragon_Black_VeryOld, SizeConstants.Huge, 29, 30)];
            testCases[CreatureConstants.Dragon_Black_Ancient] = [GetData(CreatureConstants.Dragon_Black_Ancient, SizeConstants.Huge, 32, 33)];
            testCases[CreatureConstants.Dragon_Black_Wyrm] = [GetData(CreatureConstants.Dragon_Black_Wyrm, SizeConstants.Gargantuan, 35, 36)];
            testCases[CreatureConstants.Dragon_Black_GreatWyrm] = [GetData(CreatureConstants.Dragon_Black_GreatWyrm, SizeConstants.Gargantuan, 38, 100)];

            testCases[CreatureConstants.Dragon_Blue_Wyrmling] = [GetData(CreatureConstants.Dragon_Blue_Wyrmling, SizeConstants.Small, 7, 8)];
            testCases[CreatureConstants.Dragon_Blue_VeryYoung] = [GetData(CreatureConstants.Dragon_Blue_VeryYoung, SizeConstants.Medium, 10, 11)];
            testCases[CreatureConstants.Dragon_Blue_Young] = [GetData(CreatureConstants.Dragon_Blue_Young, SizeConstants.Medium, 13, 14)];
            testCases[CreatureConstants.Dragon_Blue_Juvenile] = [GetData(CreatureConstants.Dragon_Blue_Juvenile, SizeConstants.Large, 16, 17)];
            testCases[CreatureConstants.Dragon_Blue_YoungAdult] = [GetData(CreatureConstants.Dragon_Blue_YoungAdult, SizeConstants.Large, 19, 20)];
            testCases[CreatureConstants.Dragon_Blue_Adult] = [GetData(CreatureConstants.Dragon_Blue_Adult, SizeConstants.Huge, 22, 23)];
            testCases[CreatureConstants.Dragon_Blue_MatureAdult] = [GetData(CreatureConstants.Dragon_Blue_MatureAdult, SizeConstants.Huge, 25, 26)];
            testCases[CreatureConstants.Dragon_Blue_Old] = [GetData(CreatureConstants.Dragon_Blue_Old, SizeConstants.Huge, 28, 29)];
            testCases[CreatureConstants.Dragon_Blue_VeryOld] = [GetData(CreatureConstants.Dragon_Blue_VeryOld, SizeConstants.Huge, 31, 32)];
            testCases[CreatureConstants.Dragon_Blue_Ancient] = [GetData(CreatureConstants.Dragon_Blue_Ancient, SizeConstants.Gargantuan, 34, 35)];
            testCases[CreatureConstants.Dragon_Blue_Wyrm] = [GetData(CreatureConstants.Dragon_Blue_Wyrm, SizeConstants.Gargantuan, 37, 38)];
            testCases[CreatureConstants.Dragon_Blue_GreatWyrm] = [GetData(CreatureConstants.Dragon_Blue_GreatWyrm, SizeConstants.Gargantuan, 40, 100)];

            testCases[CreatureConstants.Dragon_Green_Wyrmling] = [GetData(CreatureConstants.Dragon_Green_Wyrmling, SizeConstants.Small, 6, 7)];
            testCases[CreatureConstants.Dragon_Green_VeryYoung] = [GetData(CreatureConstants.Dragon_Green_VeryYoung, SizeConstants.Medium, 9, 10)];
            testCases[CreatureConstants.Dragon_Green_Young] = [GetData(CreatureConstants.Dragon_Green_Young, SizeConstants.Medium, 12, 13)];
            testCases[CreatureConstants.Dragon_Green_Juvenile] = [GetData(CreatureConstants.Dragon_Green_Juvenile, SizeConstants.Large, 15, 16)];
            testCases[CreatureConstants.Dragon_Green_YoungAdult] = [GetData(CreatureConstants.Dragon_Green_YoungAdult, SizeConstants.Large, 18, 19)];
            testCases[CreatureConstants.Dragon_Green_Adult] = [GetData(CreatureConstants.Dragon_Green_Adult, SizeConstants.Huge, 21, 22)];
            testCases[CreatureConstants.Dragon_Green_MatureAdult] = [GetData(CreatureConstants.Dragon_Green_MatureAdult, SizeConstants.Huge, 24, 25)];
            testCases[CreatureConstants.Dragon_Green_Old] = [GetData(CreatureConstants.Dragon_Green_Old, SizeConstants.Huge, 27, 28)];
            testCases[CreatureConstants.Dragon_Green_VeryOld] = [GetData(CreatureConstants.Dragon_Green_VeryOld, SizeConstants.Huge, 30, 31)];
            testCases[CreatureConstants.Dragon_Green_Ancient] = [GetData(CreatureConstants.Dragon_Green_Ancient, SizeConstants.Gargantuan, 33, 34)];
            testCases[CreatureConstants.Dragon_Green_Wyrm] = [GetData(CreatureConstants.Dragon_Green_Wyrm, SizeConstants.Gargantuan, 36, 37)];
            testCases[CreatureConstants.Dragon_Green_GreatWyrm] = [GetData(CreatureConstants.Dragon_Green_GreatWyrm, SizeConstants.Gargantuan, 39, 100)];

            testCases[CreatureConstants.Dragon_Red_Wyrmling] = [GetData(CreatureConstants.Dragon_Red_Wyrmling, SizeConstants.Medium, 8, 9)];
            testCases[CreatureConstants.Dragon_Red_VeryYoung] = [GetData(CreatureConstants.Dragon_Red_VeryYoung, SizeConstants.Large, 11, 12)];
            testCases[CreatureConstants.Dragon_Red_Young] = [GetData(CreatureConstants.Dragon_Red_Young, SizeConstants.Large, 14, 15)];
            testCases[CreatureConstants.Dragon_Red_Juvenile] = [GetData(CreatureConstants.Dragon_Red_Juvenile, SizeConstants.Large, 17, 18)];
            testCases[CreatureConstants.Dragon_Red_YoungAdult] = [GetData(CreatureConstants.Dragon_Red_YoungAdult, SizeConstants.Huge, 20, 21)];
            testCases[CreatureConstants.Dragon_Red_Adult] = [GetData(CreatureConstants.Dragon_Red_Adult, SizeConstants.Huge, 23, 24)];
            testCases[CreatureConstants.Dragon_Red_MatureAdult] = [GetData(CreatureConstants.Dragon_Red_MatureAdult, SizeConstants.Huge, 26, 27)];
            testCases[CreatureConstants.Dragon_Red_Old] = [GetData(CreatureConstants.Dragon_Red_Old, SizeConstants.Gargantuan, 29, 30)];
            testCases[CreatureConstants.Dragon_Red_VeryOld] = [GetData(CreatureConstants.Dragon_Red_VeryOld, SizeConstants.Gargantuan, 32, 33)];
            testCases[CreatureConstants.Dragon_Red_Ancient] = [GetData(CreatureConstants.Dragon_Red_Ancient, SizeConstants.Gargantuan, 35, 36)];
            testCases[CreatureConstants.Dragon_Red_Wyrm] = [GetData(CreatureConstants.Dragon_Red_Wyrm, SizeConstants.Gargantuan, 38, 39)];
            testCases[CreatureConstants.Dragon_Red_GreatWyrm] = [GetData(CreatureConstants.Dragon_Red_GreatWyrm, SizeConstants.Colossal, 41, 100)];

            testCases[CreatureConstants.Dragon_White_Wyrmling] = [GetData(CreatureConstants.Dragon_White_Wyrmling, SizeConstants.Tiny, 4, 5)];
            testCases[CreatureConstants.Dragon_White_VeryYoung] = [GetData(CreatureConstants.Dragon_White_VeryYoung, SizeConstants.Small, 7, 8)];
            testCases[CreatureConstants.Dragon_White_Young] = [GetData(CreatureConstants.Dragon_White_Young, SizeConstants.Medium, 10, 11)];
            testCases[CreatureConstants.Dragon_White_Juvenile] = [GetData(CreatureConstants.Dragon_White_Juvenile, SizeConstants.Medium, 13, 14)];
            testCases[CreatureConstants.Dragon_White_YoungAdult] = [GetData(CreatureConstants.Dragon_White_YoungAdult, SizeConstants.Large, 16, 17)];
            testCases[CreatureConstants.Dragon_White_Adult] = [GetData(CreatureConstants.Dragon_White_Adult, SizeConstants.Large, 19, 20)];
            testCases[CreatureConstants.Dragon_White_MatureAdult] = [GetData(CreatureConstants.Dragon_White_MatureAdult, SizeConstants.Huge, 22, 23)];
            testCases[CreatureConstants.Dragon_White_Old] = [GetData(CreatureConstants.Dragon_White_Old, SizeConstants.Huge, 25, 26)];
            testCases[CreatureConstants.Dragon_White_VeryOld] = [GetData(CreatureConstants.Dragon_White_VeryOld, SizeConstants.Huge, 28, 29)];
            testCases[CreatureConstants.Dragon_White_Ancient] = [GetData(CreatureConstants.Dragon_White_Ancient, SizeConstants.Huge, 31, 32)];
            testCases[CreatureConstants.Dragon_White_Wyrm] = [GetData(CreatureConstants.Dragon_White_Wyrm, SizeConstants.Gargantuan, 34, 35)];
            testCases[CreatureConstants.Dragon_White_GreatWyrm] = [GetData(CreatureConstants.Dragon_White_GreatWyrm, SizeConstants.Gargantuan, 37, 100)];

            testCases[CreatureConstants.Dragon_Brass_Wyrmling] = [GetData(CreatureConstants.Dragon_Brass_Wyrmling, SizeConstants.Tiny, 5, 6)];
            testCases[CreatureConstants.Dragon_Brass_VeryYoung] = [GetData(CreatureConstants.Dragon_Brass_VeryYoung, SizeConstants.Small, 8, 9)];
            testCases[CreatureConstants.Dragon_Brass_Young] = [GetData(CreatureConstants.Dragon_Brass_Young, SizeConstants.Medium, 11, 12)];
            testCases[CreatureConstants.Dragon_Brass_Juvenile] = [GetData(CreatureConstants.Dragon_Brass_Juvenile, SizeConstants.Medium, 14, 15)];
            testCases[CreatureConstants.Dragon_Brass_YoungAdult] = [GetData(CreatureConstants.Dragon_Brass_YoungAdult, SizeConstants.Large, 17, 18)];
            testCases[CreatureConstants.Dragon_Brass_Adult] = [GetData(CreatureConstants.Dragon_Brass_Adult, SizeConstants.Large, 20, 21)];
            testCases[CreatureConstants.Dragon_Brass_MatureAdult] = [GetData(CreatureConstants.Dragon_Brass_MatureAdult, SizeConstants.Huge, 23, 24)];
            testCases[CreatureConstants.Dragon_Brass_Old] = [GetData(CreatureConstants.Dragon_Brass_Old, SizeConstants.Huge, 26, 27)];
            testCases[CreatureConstants.Dragon_Brass_VeryOld] = [GetData(CreatureConstants.Dragon_Brass_VeryOld, SizeConstants.Huge, 29, 30)];
            testCases[CreatureConstants.Dragon_Brass_Ancient] = [GetData(CreatureConstants.Dragon_Brass_Ancient, SizeConstants.Huge, 32, 33)];
            testCases[CreatureConstants.Dragon_Brass_Wyrm] = [GetData(CreatureConstants.Dragon_Brass_Wyrm, SizeConstants.Gargantuan, 35, 36)];
            testCases[CreatureConstants.Dragon_Brass_GreatWyrm] = [GetData(CreatureConstants.Dragon_Brass_GreatWyrm, SizeConstants.Gargantuan, 38, 100)];

            testCases[CreatureConstants.Dragon_Bronze_Wyrmling] = [GetData(CreatureConstants.Dragon_Bronze_Wyrmling, SizeConstants.Small, 7, 8)];
            testCases[CreatureConstants.Dragon_Bronze_VeryYoung] = [GetData(CreatureConstants.Dragon_Bronze_VeryYoung, SizeConstants.Medium, 10, 11)];
            testCases[CreatureConstants.Dragon_Bronze_Young] = [GetData(CreatureConstants.Dragon_Bronze_Young, SizeConstants.Medium, 13, 14)];
            testCases[CreatureConstants.Dragon_Bronze_Juvenile] = [GetData(CreatureConstants.Dragon_Bronze_Juvenile, SizeConstants.Large, 16, 17)];
            testCases[CreatureConstants.Dragon_Bronze_YoungAdult] = [GetData(CreatureConstants.Dragon_Bronze_YoungAdult, SizeConstants.Large, 19, 20)];
            testCases[CreatureConstants.Dragon_Bronze_Adult] = [GetData(CreatureConstants.Dragon_Bronze_Adult, SizeConstants.Huge, 22, 23)];
            testCases[CreatureConstants.Dragon_Bronze_MatureAdult] = [GetData(CreatureConstants.Dragon_Bronze_MatureAdult, SizeConstants.Huge, 25, 26)];
            testCases[CreatureConstants.Dragon_Bronze_Old] = [GetData(CreatureConstants.Dragon_Bronze_Old, SizeConstants.Huge, 28, 29)];
            testCases[CreatureConstants.Dragon_Bronze_VeryOld] = [GetData(CreatureConstants.Dragon_Bronze_VeryOld, SizeConstants.Huge, 31, 32)];
            testCases[CreatureConstants.Dragon_Bronze_Ancient] = [GetData(CreatureConstants.Dragon_Bronze_Ancient, SizeConstants.Gargantuan, 34, 35)];
            testCases[CreatureConstants.Dragon_Bronze_Wyrm] = [GetData(CreatureConstants.Dragon_Bronze_Wyrm, SizeConstants.Gargantuan, 37, 38)];
            testCases[CreatureConstants.Dragon_Bronze_GreatWyrm] = [GetData(CreatureConstants.Dragon_Bronze_GreatWyrm, SizeConstants.Gargantuan, 40, 100)];


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
            testCases[CreatureConstants.Erinyes][RollHelper.GetRollWithMostEvenDistribution(9, 10, 18, true)] = GetData(SizeConstants.Medium, 5, 5);
            testCases[CreatureConstants.EtherealFilcher][RollHelper.GetRollWithMostEvenDistribution(5, 6, 7, true)] = GetData(SizeConstants.Medium, 5, 5);
            testCases[CreatureConstants.EtherealFilcher][RollHelper.GetRollWithMostEvenDistribution(5, 8, 15, true)] = GetData(SizeConstants.Large, 10, 5);
            testCases[CreatureConstants.EtherealMarauder][RollHelper.GetRollWithMostEvenDistribution(2, 3, 4, true)] = GetData(SizeConstants.Medium, 5, 5);
            testCases[CreatureConstants.EtherealMarauder][RollHelper.GetRollWithMostEvenDistribution(2, 5, 6, true)] = GetData(SizeConstants.Large, 10, 5);
            testCases[CreatureConstants.Ettercap][RollHelper.GetRollWithMostEvenDistribution(5, 6, 7, true)] = GetData(SizeConstants.Medium, 5, 5);
            testCases[CreatureConstants.Ettercap][RollHelper.GetRollWithMostEvenDistribution(5, 8, 15, true)] = GetData(SizeConstants.Large, 10, 10);
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
            testCases[CreatureConstants.GibberingMouther][RollHelper.GetRollWithMostEvenDistribution(4, 5, 12, true)] = GetData(SizeConstants.Large, 10, 5);
            testCases[CreatureConstants.Girallon][RollHelper.GetRollWithMostEvenDistribution(7, 8, 10, true)] = GetData(SizeConstants.Large, 10, 10);
            testCases[CreatureConstants.Girallon][RollHelper.GetRollWithMostEvenDistribution(7, 11, 21, true)] = GetData(SizeConstants.Huge, 15, 15);
            testCases[CreatureConstants.Glabrezu][RollHelper.GetRollWithMostEvenDistribution(12, 13, 18, true)] = GetData(SizeConstants.Huge, 15, 15);
            testCases[CreatureConstants.Glabrezu][RollHelper.GetRollWithMostEvenDistribution(12, 19, 36, true)] = GetData(SizeConstants.Gargantuan, 20, 20);
            testCases[CreatureConstants.Golem_Clay][RollHelper.GetRollWithMostEvenDistribution(11, 12, 18, true)] = GetData(SizeConstants.Large, 10, 10);
            testCases[CreatureConstants.Golem_Clay][RollHelper.GetRollWithMostEvenDistribution(11, 19, 33, true)] = GetData(SizeConstants.Huge, 15, 15);
            testCases[CreatureConstants.Golem_Flesh][RollHelper.GetRollWithMostEvenDistribution(9, 10, 18, true)] = GetData(SizeConstants.Large, 10, 10);
            testCases[CreatureConstants.Golem_Flesh][RollHelper.GetRollWithMostEvenDistribution(9, 19, 27, true)] = GetData(SizeConstants.Huge, 15, 15);
            testCases[CreatureConstants.Golem_Iron][RollHelper.GetRollWithMostEvenDistribution(18, 19, 24, true)] = GetData(SizeConstants.Large, 10, 10);
            testCases[CreatureConstants.Golem_Iron][RollHelper.GetRollWithMostEvenDistribution(18, 25, 54, true)] = GetData(SizeConstants.Huge, 15, 15);
            testCases[CreatureConstants.Golem_Stone][RollHelper.GetRollWithMostEvenDistribution(14, 15, 21, true)] = GetData(SizeConstants.Large, 10, 10);
            testCases[CreatureConstants.Golem_Stone][RollHelper.GetRollWithMostEvenDistribution(14, 22, 42, true)] = GetData(SizeConstants.Huge, 15, 15);
            testCases[CreatureConstants.Gorgon][RollHelper.GetRollWithMostEvenDistribution(8, 9, 15, true)] = GetData(SizeConstants.Large, 10, 5);
            testCases[CreatureConstants.Gorgon][RollHelper.GetRollWithMostEvenDistribution(8, 16, 24, true)] = GetData(SizeConstants.Huge, 15, 10);
            testCases[CreatureConstants.GrayOoze][RollHelper.GetRollWithMostEvenDistribution(3, 4, 6, true)] = GetData(SizeConstants.Medium, 5, 5);
            testCases[CreatureConstants.GrayOoze][RollHelper.GetRollWithMostEvenDistribution(3, 7, 9, true)] = GetData(SizeConstants.Large, 10, 5);
            testCases[CreatureConstants.GrayRender][RollHelper.GetRollWithMostEvenDistribution(10, 11, 15, true)] = GetData(SizeConstants.Large, 10, 10);
            testCases[CreatureConstants.GrayRender][RollHelper.GetRollWithMostEvenDistribution(10, 16, 30, true)] = GetData(SizeConstants.Huge, 15, 15);
            testCases[CreatureConstants.Grick][RollHelper.GetRollWithMostEvenDistribution(2, 3, 4, true)] = GetData(SizeConstants.Medium, 5, 5);
            testCases[CreatureConstants.Grick][RollHelper.GetRollWithMostEvenDistribution(2, 5, 6, true)] = GetData(SizeConstants.Large, 10, 5);
            testCases[CreatureConstants.Griffon][RollHelper.GetRollWithMostEvenDistribution(7, 8, 10, true)] = GetData(SizeConstants.Large, 10, 5);
            testCases[CreatureConstants.Griffon][RollHelper.GetRollWithMostEvenDistribution(7, 11, 21, true)] = GetData(SizeConstants.Huge, 15, 10);
            testCases[CreatureConstants.Grig][RollHelper.GetRollWithMostEvenDistribution(0, 1, 3, true)] = GetData(SizeConstants.Tiny, 2.5, 0);
            testCases[CreatureConstants.Grig_WithFiddle][RollHelper.GetRollWithMostEvenDistribution(0, 1, 3, true)] = GetData(SizeConstants.Tiny, 2.5, 0);
            testCases[CreatureConstants.Gynosphinx][RollHelper.GetRollWithMostEvenDistribution(8, 9, 12, true)] = GetData(SizeConstants.Large, 10, 5);
            testCases[CreatureConstants.Gynosphinx][RollHelper.GetRollWithMostEvenDistribution(8, 13, 24, true)] = GetData(SizeConstants.Huge, 15, 10);
            testCases[CreatureConstants.HellHound][RollHelper.GetRollWithMostEvenDistribution(4, 5, 8, true)] = GetData(SizeConstants.Medium, 5, 5);
            testCases[CreatureConstants.HellHound][RollHelper.GetRollWithMostEvenDistribution(4, 9, 12, true)] = GetData(SizeConstants.Large, 10, 5);
            testCases[CreatureConstants.HellHound_NessianWarhound][RollHelper.GetRollWithMostEvenDistribution(12, 13, 17, true)] = GetData(SizeConstants.Large, 10, 10);
            testCases[CreatureConstants.HellHound_NessianWarhound][RollHelper.GetRollWithMostEvenDistribution(12, 18, 24, true)] = GetData(SizeConstants.Huge, 15, 15);
            testCases[CreatureConstants.Hellcat_Bezekira][RollHelper.GetRollWithMostEvenDistribution(8, 9, 10, true)] = GetData(SizeConstants.Large, 10, 5);
            testCases[CreatureConstants.Hellcat_Bezekira][RollHelper.GetRollWithMostEvenDistribution(8, 11, 24, true)] = GetData(SizeConstants.Huge, 15, 10);
            testCases[CreatureConstants.Hezrou][RollHelper.GetRollWithMostEvenDistribution(10, 11, 15, true)] = GetData(SizeConstants.Large, 10, 10);
            testCases[CreatureConstants.Hezrou][RollHelper.GetRollWithMostEvenDistribution(10, 16, 30, true)] = GetData(SizeConstants.Huge, 15, 15);
            testCases[CreatureConstants.Hieracosphinx][RollHelper.GetRollWithMostEvenDistribution(9, 10, 14, true)] = GetData(SizeConstants.Large, 10, 5);
            testCases[CreatureConstants.Hieracosphinx][RollHelper.GetRollWithMostEvenDistribution(9, 15, 27, true)] = GetData(SizeConstants.Huge, 15, 10);
            testCases[CreatureConstants.Hippogriff][RollHelper.GetRollWithMostEvenDistribution(3, 4, 6, true)] = GetData(SizeConstants.Large, 10, 5);
            testCases[CreatureConstants.Hippogriff][RollHelper.GetRollWithMostEvenDistribution(3, 7, 9, true)] = GetData(SizeConstants.Huge, 15, 10);
            testCases[CreatureConstants.Homunculus][RollHelper.GetRollWithMostEvenDistribution(2, 3, 6, true)] = GetData(SizeConstants.Tiny, 2.5, 0);
            testCases[CreatureConstants.HornedDevil_Cornugon][RollHelper.GetRollWithMostEvenDistribution(15, 16, 20, true)] = GetData(SizeConstants.Large, 10, 10);
            testCases[CreatureConstants.HornedDevil_Cornugon][RollHelper.GetRollWithMostEvenDistribution(15, 21, 45, true)] = GetData(SizeConstants.Huge, 15, 15);
            testCases[CreatureConstants.HoundArchon][RollHelper.GetRollWithMostEvenDistribution(6, 7, 9, true)] = GetData(SizeConstants.Medium, 5, 5);
            testCases[CreatureConstants.HoundArchon][RollHelper.GetRollWithMostEvenDistribution(6, 10, 18, true)] = GetData(SizeConstants.Large, 10, 10);
            testCases[CreatureConstants.Howler][RollHelper.GetRollWithMostEvenDistribution(6, 7, 9, true)] = GetData(SizeConstants.Large, 10, 5);
            testCases[CreatureConstants.Howler][RollHelper.GetRollWithMostEvenDistribution(6, 10, 18, true)] = GetData(SizeConstants.Huge, 15, 10);
            testCases[CreatureConstants.Hyena][RollHelper.GetRollWithMostEvenDistribution(2, 3, 3, true)] = GetData(SizeConstants.Medium, 5, 5);
            testCases[CreatureConstants.Hyena][RollHelper.GetRollWithMostEvenDistribution(2, 4, 5, true)] = GetData(SizeConstants.Large, 10, 5);
            testCases[CreatureConstants.IceDevil_Gelugon][RollHelper.GetRollWithMostEvenDistribution(14, 15, 28, true)] = GetData(SizeConstants.Large, 10, 10);
            testCases[CreatureConstants.IceDevil_Gelugon][RollHelper.GetRollWithMostEvenDistribution(14, 29, 42, true)] = GetData(SizeConstants.Huge, 15, 15);
            testCases[CreatureConstants.Imp][RollHelper.GetRollWithMostEvenDistribution(3, 4, 6, true)] = GetData(SizeConstants.Tiny, 2.5, 0);
            testCases[CreatureConstants.InvisibleStalker][RollHelper.GetRollWithMostEvenDistribution(8, 9, 12, true)] = GetData(SizeConstants.Large, 10, 10);
            testCases[CreatureConstants.InvisibleStalker][RollHelper.GetRollWithMostEvenDistribution(8, 13, 24, true)] = GetData(SizeConstants.Huge, 15, 15);
            testCases[CreatureConstants.Janni][RollHelper.GetRollWithMostEvenDistribution(6, 7, 9, true)] = GetData(SizeConstants.Medium, 5, 5);
            testCases[CreatureConstants.Janni][RollHelper.GetRollWithMostEvenDistribution(6, 10, 18, true)] = GetData(SizeConstants.Large, 10, 10);
            testCases[CreatureConstants.Kolyarut][RollHelper.GetRollWithMostEvenDistribution(13, 14, 22, true)] = GetData(SizeConstants.Medium, 5, 5);
            testCases[CreatureConstants.Kolyarut][RollHelper.GetRollWithMostEvenDistribution(13, 23, 39, true)] = GetData(SizeConstants.Large, 10, 10);
            testCases[CreatureConstants.Kraken][RollHelper.GetRollWithMostEvenDistribution(20, 21, 32, true)] = GetData(SizeConstants.Gargantuan, 20, 15);
            testCases[CreatureConstants.Kraken][RollHelper.GetRollWithMostEvenDistribution(20, 33, 60, true)] = GetData(SizeConstants.Colossal, 30, 20);
            testCases[CreatureConstants.Krenshar][RollHelper.GetRollWithMostEvenDistribution(2, 3, 4, true)] = GetData(SizeConstants.Medium, 5, 5);
            testCases[CreatureConstants.Krenshar][RollHelper.GetRollWithMostEvenDistribution(2, 5, 8, true)] = GetData(SizeConstants.Large, 10, 5);
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
            testCases[CreatureConstants.Lizard_Monitor][RollHelper.GetRollWithMostEvenDistribution(3, 4, 5, true)] = GetData(SizeConstants.Medium, 5, 5);
            testCases[CreatureConstants.Magmin][RollHelper.GetRollWithMostEvenDistribution(2, 3, 4, true)] = GetData(SizeConstants.Small, 5, 5);
            testCases[CreatureConstants.Magmin][RollHelper.GetRollWithMostEvenDistribution(2, 5, 6, true)] = GetData(SizeConstants.Medium, 5, 5);
            testCases[CreatureConstants.MantaRay][RollHelper.GetRollWithMostEvenDistribution(4, 5, 6, true)] = GetData(SizeConstants.Large, 10, 5);
            testCases[CreatureConstants.Manticore][RollHelper.GetRollWithMostEvenDistribution(6, 7, 16, true)] = GetData(SizeConstants.Large, 10, 5);
            testCases[CreatureConstants.Manticore][RollHelper.GetRollWithMostEvenDistribution(6, 17, 18, true)] = GetData(SizeConstants.Huge, 15, 10);
            testCases[CreatureConstants.Marilith][RollHelper.GetRollWithMostEvenDistribution(16, 17, 20, true)] = GetData(SizeConstants.Large, 10, 10);
            testCases[CreatureConstants.Marilith][RollHelper.GetRollWithMostEvenDistribution(16, 21, 48, true)] = GetData(SizeConstants.Huge, 15, 15);
            testCases[CreatureConstants.Marut][RollHelper.GetRollWithMostEvenDistribution(15, 16, 28, true)] = GetData(SizeConstants.Large, 10, 10);
            testCases[CreatureConstants.Marut][RollHelper.GetRollWithMostEvenDistribution(15, 29, 45, true)] = GetData(SizeConstants.Huge, 15, 15);
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
            testCases[CreatureConstants.Mimic][RollHelper.GetRollWithMostEvenDistribution(7, 8, 10, true)] = GetData(SizeConstants.Large, 10, 10);
            testCases[CreatureConstants.Mimic][RollHelper.GetRollWithMostEvenDistribution(7, 11, 21, true)] = GetData(SizeConstants.Huge, 15, 15);
            testCases[CreatureConstants.Mohrg][RollHelper.GetRollWithMostEvenDistribution(14, 15, 21, true)] = GetData(SizeConstants.Medium, 5, 5);
            testCases[CreatureConstants.Mohrg][RollHelper.GetRollWithMostEvenDistribution(14, 22, 28, true)] = GetData(SizeConstants.Large, 10, 10);
            testCases[CreatureConstants.Monkey][RollHelper.GetRollWithMostEvenDistribution(1, 2, 3, true)] = GetData(SizeConstants.Small, 5, 5);
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
            testCases[CreatureConstants.Porpoise][RollHelper.GetRollWithMostEvenDistribution(2, 3, 4, true)] = GetData(SizeConstants.Medium, 5, 5);
            testCases[CreatureConstants.Porpoise][RollHelper.GetRollWithMostEvenDistribution(2, 3, 4, true)] = GetData(SizeConstants.Medium, 5, 5);
            testCases[CreatureConstants.PrayingMantis_Giant][RollHelper.GetRollWithMostEvenDistribution(4, 5, 8, true)] = GetData(SizeConstants.Large, 10, 5);
            testCases[CreatureConstants.PrayingMantis_Giant][RollHelper.GetRollWithMostEvenDistribution(4, 9, 12, true)] = GetData(SizeConstants.Huge, 15, 10);
            testCases[CreatureConstants.Pseudodragon][RollHelper.GetRollWithMostEvenDistribution(2, 3, 4, true)] = GetData(SizeConstants.Tiny, 2.5, 0);
            testCases[CreatureConstants.PurpleWorm][RollHelper.GetRollWithMostEvenDistribution(16, 17, 32, true)] = GetData(SizeConstants.Gargantuan, 20, 15);
            testCases[CreatureConstants.PurpleWorm][RollHelper.GetRollWithMostEvenDistribution(16, 33, 48, true)] = GetData(SizeConstants.Colossal, 30, 20);
            testCases[CreatureConstants.Quasit][RollHelper.GetRollWithMostEvenDistribution(3, 4, 6, true)] = GetData(SizeConstants.Tiny, 2.5, 0);
            testCases[CreatureConstants.Rast][RollHelper.GetRollWithMostEvenDistribution(4, 5, 6, true)] = GetData(SizeConstants.Medium, 5, 5);
            testCases[CreatureConstants.Rast][RollHelper.GetRollWithMostEvenDistribution(4, 7, 12, true)] = GetData(SizeConstants.Large, 10, 5);
            testCases[CreatureConstants.Rat_Dire][RollHelper.GetRollWithMostEvenDistribution(1, 2, 3, true)] = GetData(SizeConstants.Small, 5, 5);
            testCases[CreatureConstants.Rat_Dire][RollHelper.GetRollWithMostEvenDistribution(1, 4, 6, true)] = GetData(SizeConstants.Medium, 5, 5);
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
            testCases[CreatureConstants.SeaCat][RollHelper.GetRollWithMostEvenDistribution(6, 7, 9, true)] = GetData(SizeConstants.Large, 10, 5);
            testCases[CreatureConstants.SeaCat][RollHelper.GetRollWithMostEvenDistribution(6, 10, 18, true)] = GetData(SizeConstants.Huge, 15, 10);
            testCases[CreatureConstants.Shadow][RollHelper.GetRollWithMostEvenDistribution(3, 4, 9, true)] = GetData(SizeConstants.Medium, 5, 5);
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
            testCases[CreatureConstants.Spectre][RollHelper.GetRollWithMostEvenDistribution(7, 8, 14, true)] = GetData(SizeConstants.Medium, 5, 5);
            testCases[CreatureConstants.Spider_Monstrous_Hunter_Colossal][RollHelper.GetRollWithMostEvenDistribution(32, 33, 60, true)] = GetData(SizeConstants.Colossal, 40, 30);
            testCases[CreatureConstants.Spider_Monstrous_Hunter_Gargantuan][RollHelper.GetRollWithMostEvenDistribution(16, 17, 31, true)] = GetData(SizeConstants.Gargantuan, 20, 15);
            testCases[CreatureConstants.Spider_Monstrous_Hunter_Huge][RollHelper.GetRollWithMostEvenDistribution(8, 9, 15, true)] = GetData(SizeConstants.Huge, 15, 10);
            testCases[CreatureConstants.Spider_Monstrous_Hunter_Large][RollHelper.GetRollWithMostEvenDistribution(4, 5, 7, true)] = GetData(SizeConstants.Large, 10, 5);
            testCases[CreatureConstants.Spider_Monstrous_Hunter_Medium][RollHelper.GetRollWithMostEvenDistribution(2, 3, 3, true)] = GetData(SizeConstants.Medium, 5, 5);
            testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Colossal][RollHelper.GetRollWithMostEvenDistribution(32, 33, 60, true)] = GetData(SizeConstants.Colossal, 40, 30);
            testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Gargantuan][RollHelper.GetRollWithMostEvenDistribution(16, 17, 31, true)] = GetData(SizeConstants.Gargantuan, 20, 15);
            testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Huge][RollHelper.GetRollWithMostEvenDistribution(8, 9, 15, true)] = GetData(SizeConstants.Huge, 15, 10);
            testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Large][RollHelper.GetRollWithMostEvenDistribution(4, 5, 7, true)] = GetData(SizeConstants.Large, 10, 5);
            testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Medium][RollHelper.GetRollWithMostEvenDistribution(2, 3, 3, true)] = GetData(SizeConstants.Medium, 5, 5);
            testCases[CreatureConstants.SpiderEater][RollHelper.GetRollWithMostEvenDistribution(4, 5, 12, true)] = GetData(SizeConstants.Huge, 15, 10);
            testCases[CreatureConstants.Squid][RollHelper.GetRollWithMostEvenDistribution(3, 4, 6, true)] = GetData(SizeConstants.Medium, 5, 5);
            testCases[CreatureConstants.Squid][RollHelper.GetRollWithMostEvenDistribution(3, 7, 11, true)] = GetData(SizeConstants.Large, 10, 5);
            testCases[CreatureConstants.Squid_Giant][RollHelper.GetRollWithMostEvenDistribution(12, 13, 18, true)] = GetData(SizeConstants.Huge, 15, 15);
            testCases[CreatureConstants.Squid_Giant][RollHelper.GetRollWithMostEvenDistribution(12, 19, 36, true)] = GetData(SizeConstants.Gargantuan, 20, 20);
            testCases[CreatureConstants.StagBeetle_Giant][RollHelper.GetRollWithMostEvenDistribution(7, 8, 10, true)] = GetData(SizeConstants.Large, 10, 5);
            testCases[CreatureConstants.StagBeetle_Giant][RollHelper.GetRollWithMostEvenDistribution(7, 11, 21, true)] = GetData(SizeConstants.Huge, 15, 10);
            testCases[CreatureConstants.Succubus][RollHelper.GetRollWithMostEvenDistribution(6, 7, 12, true)] = GetData(SizeConstants.Medium, 5, 5);
            testCases[CreatureConstants.Tarrasque][RollHelper.GetRollWithMostEvenDistribution(48, 49, 100, true)] = GetData(SizeConstants.Colossal, 30, 20);
            testCases[CreatureConstants.Tendriculos][RollHelper.GetRollWithMostEvenDistribution(9, 10, 16, true)] = GetData(SizeConstants.Huge, 15, 15);
            testCases[CreatureConstants.Tendriculos][RollHelper.GetRollWithMostEvenDistribution(9, 17, 27, true)] = GetData(SizeConstants.Gargantuan, 20, 20);
            testCases[CreatureConstants.Thoqqua][RollHelper.GetRollWithMostEvenDistribution(3, 4, 9, true)] = GetData(SizeConstants.Large, 10, 5);
            testCases[CreatureConstants.Tiger][RollHelper.GetRollWithMostEvenDistribution(6, 7, 12, true)] = GetData(SizeConstants.Large, 10, 5);
            testCases[CreatureConstants.Tiger][RollHelper.GetRollWithMostEvenDistribution(6, 13, 18, true)] = GetData(SizeConstants.Huge, 15, 10);
            testCases[CreatureConstants.Tiger_Dire][RollHelper.GetRollWithMostEvenDistribution(16, 17, 32, true)] = GetData(SizeConstants.Large, 10, 5);
            testCases[CreatureConstants.Tiger_Dire][RollHelper.GetRollWithMostEvenDistribution(16, 33, 48, true)] = GetData(SizeConstants.Huge, 15, 10);
            testCases[CreatureConstants.Titan][RollHelper.GetRollWithMostEvenDistribution(20, 21, 30, true)] = GetData(SizeConstants.Huge, 15, 15);
            testCases[CreatureConstants.Titan][RollHelper.GetRollWithMostEvenDistribution(20, 31, 60, true)] = GetData(SizeConstants.Gargantuan, 20, 20);
            testCases[CreatureConstants.Tojanida_Adult][RollHelper.GetRollWithMostEvenDistribution(7, 8, 14, true)] = GetData(SizeConstants.Medium, 5, 5);
            testCases[CreatureConstants.Tojanida_Elder][RollHelper.GetRollWithMostEvenDistribution(15, 16, 24, true)] = GetData(SizeConstants.Large, 10, 5);
            testCases[CreatureConstants.Tojanida_Juvenile][RollHelper.GetRollWithMostEvenDistribution(3, 4, 6, true)] = GetData(SizeConstants.Small, 5, 5);
            testCases[CreatureConstants.Treant][RollHelper.GetRollWithMostEvenDistribution(7, 8, 16, true)] = GetData(SizeConstants.Huge, 15, 15);
            testCases[CreatureConstants.Treant][RollHelper.GetRollWithMostEvenDistribution(7, 17, 21, true)] = GetData(SizeConstants.Gargantuan, 20, 20);
            testCases[CreatureConstants.Triceratops][RollHelper.GetRollWithMostEvenDistribution(16, 17, 32, true)] = GetData(SizeConstants.Huge, 15, 10);
            testCases[CreatureConstants.Triceratops][RollHelper.GetRollWithMostEvenDistribution(16, 33, 48, true)] = GetData(SizeConstants.Gargantuan, 20, 15);
            testCases[CreatureConstants.Triton][RollHelper.GetRollWithMostEvenDistribution(3, 4, 9, true)] = GetData(SizeConstants.Medium, 5, 5);
            testCases[CreatureConstants.TrumpetArchon][RollHelper.GetRollWithMostEvenDistribution(12, 13, 18, true)] = GetData(SizeConstants.Medium, 5, 5);
            testCases[CreatureConstants.TrumpetArchon][RollHelper.GetRollWithMostEvenDistribution(12, 19, 36, true)] = GetData(SizeConstants.Large, 10, 10);
            testCases[CreatureConstants.Tyrannosaurus][RollHelper.GetRollWithMostEvenDistribution(18, 19, 36, true)] = GetData(SizeConstants.Huge, 15, 10);
            testCases[CreatureConstants.Tyrannosaurus][RollHelper.GetRollWithMostEvenDistribution(18, 37, 54, true)] = GetData(SizeConstants.Gargantuan, 20, 15);
            testCases[CreatureConstants.Unicorn][RollHelper.GetRollWithMostEvenDistribution(4, 5, 8, true)] = GetData(SizeConstants.Large, 10, 5);
            testCases[CreatureConstants.UmberHulk][RollHelper.GetRollWithMostEvenDistribution(8, 9, 12, true)] = GetData(SizeConstants.Large, 10, 10);
            testCases[CreatureConstants.UmberHulk][RollHelper.GetRollWithMostEvenDistribution(8, 13, 24, true)] = GetData(SizeConstants.Huge, 15, 15);
            testCases[CreatureConstants.Vargouille][RollHelper.GetRollWithMostEvenDistribution(1, 2, 3, true)] = GetData(SizeConstants.Small, 5, 5);
            testCases[CreatureConstants.VioletFungus][RollHelper.GetRollWithMostEvenDistribution(2, 3, 6, true)] = GetData(SizeConstants.Medium, 5, 5);
            testCases[CreatureConstants.Vrock][RollHelper.GetRollWithMostEvenDistribution(10, 11, 14, true)] = GetData(SizeConstants.Large, 10, 10);
            testCases[CreatureConstants.Vrock][RollHelper.GetRollWithMostEvenDistribution(10, 15, 30, true)] = GetData(SizeConstants.Huge, 15, 15);
            testCases[CreatureConstants.Wasp_Giant][RollHelper.GetRollWithMostEvenDistribution(5, 6, 8, true)] = GetData(SizeConstants.Large, 10, 5);
            testCases[CreatureConstants.Wasp_Giant][RollHelper.GetRollWithMostEvenDistribution(5, 9, 15, true)] = GetData(SizeConstants.Huge, 15, 10);
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
            testCases[CreatureConstants.Zelekhut][RollHelper.GetRollWithMostEvenDistribution(8, 9, 16, true)] = GetData(SizeConstants.Large, 10, 10);
            testCases[CreatureConstants.Zelekhut][RollHelper.GetRollWithMostEvenDistribution(8, 17, 24, true)] = GetData(SizeConstants.Huge, 15, 15);

            return testCases;
        }

        private string GetData(string creature, string advancedSize, int lowerHitDice, int upperHitDice)
        {
            var creatureData = creatureDataSelector.SelectFor(creature);
            var creatureHitDice = collectionTypeAndAmountSelector.SelectOneFrom(Config.Name, TableNameConstants.TypeAndAmount.HitDice, creature);

            var selection = new AdvancementDataSelection
            {
                Reach = spaceReachHelper.GetReach(creature, advancedSize),
                Space = spaceReachHelper.GetSpace(advancedSize),
                Size = advancedSize,
                AdditionalHitDiceRoll = RollHelper.GetRollWithMostEvenDistribution(creatureHitDice.Amount, lowerHitDice, upperHitDice, true),
                StrengthAdjustment = GetStrengthAdjustment(creatureData.Size, advancedSize),
                ConstitutionAdjustment = GetConstitutionAdjustment(creatureData.Size, advancedSize),
                DexterityAdjustment = GetDexterityAdjustment(creatureData.Size, advancedSize),
                NaturalArmorAdjustment = GetNaturalArmorAdjustment(creatureData.Size, advancedSize),
                ChallengeRatingDivisor = GetChallengeRatingDivisor(creature),
                AdjustedChallengeRating = GetAdjustedChallengeRating(creatureData.ChallengeRating, creatureData.Size, advancedSize),
            };

            return Infrastructure.Helpers.DataHelper.Parse(selection);
        }

        private string GetAdjustedChallengeRating(string cr, string originalSize, string advancedSize)
        {
            var sizes = SizeConstants.GetOrdered();
            var originalSizeIndex = Array.IndexOf(sizes, originalSize);
            var advancedIndex = Array.IndexOf(sizes, advancedSize);
            var largeIndex = Array.IndexOf(sizes, SizeConstants.Large);

            if (advancedIndex < largeIndex || originalSize == advancedSize)
            {
                return cr;
            }

            var increase = advancedIndex - Math.Max(largeIndex - 1, originalSizeIndex);
            return ChallengeRatingConstants.IncreaseChallengeRating(cr, increase);
        }

        private int GetChallengeRatingDivisor(string creature)
        {
            var types = collectionSelector.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureTypes, creature);
            var creatureType = types.First();
            return typeDivisors[creatureType];
        }

        private int GetConstitutionAdjustment(string originalSize, string advancedSize)
        {
            var constitutionAdjustment = 0;
            var currentSize = originalSize;

            while (currentSize != advancedSize)
            {
                switch (currentSize)
                {
                    case SizeConstants.Fine: currentSize = SizeConstants.Diminutive; break;
                    case SizeConstants.Diminutive: currentSize = SizeConstants.Tiny; break;
                    case SizeConstants.Tiny: currentSize = SizeConstants.Small; break;
                    case SizeConstants.Small: currentSize = SizeConstants.Medium; constitutionAdjustment += 2; break;
                    case SizeConstants.Medium: currentSize = SizeConstants.Large; constitutionAdjustment += 4; break;
                    case SizeConstants.Large: currentSize = SizeConstants.Huge; constitutionAdjustment += 4; break;
                    case SizeConstants.Huge: currentSize = SizeConstants.Gargantuan; constitutionAdjustment += 4; break;
                    case SizeConstants.Gargantuan: currentSize = SizeConstants.Colossal; constitutionAdjustment += 4; break;
                    case SizeConstants.Colossal:
                    default: throw new ArgumentException($"{currentSize} is not a valid size that can be advanced");
                }
            }

            return constitutionAdjustment;
        }

        private int GetDexterityAdjustment(string originalSize, string advancedSize)
        {
            var dexterityAdjustment = 0;
            var currentSize = originalSize;

            while (currentSize != advancedSize)
            {
                switch (currentSize)
                {
                    case SizeConstants.Fine: currentSize = SizeConstants.Diminutive; dexterityAdjustment -= 2; break;
                    case SizeConstants.Diminutive: currentSize = SizeConstants.Tiny; dexterityAdjustment -= 2; break;
                    case SizeConstants.Tiny: currentSize = SizeConstants.Small; dexterityAdjustment -= 2; break;
                    case SizeConstants.Small: currentSize = SizeConstants.Medium; dexterityAdjustment -= 2; break;
                    case SizeConstants.Medium: currentSize = SizeConstants.Large; dexterityAdjustment -= 2; break;
                    case SizeConstants.Large: currentSize = SizeConstants.Huge; dexterityAdjustment -= 2; break;
                    case SizeConstants.Huge: currentSize = SizeConstants.Gargantuan; break;
                    case SizeConstants.Gargantuan: currentSize = SizeConstants.Colossal; break;
                    case SizeConstants.Colossal:
                    default: throw new ArgumentException($"{currentSize} is not a valid size that can be advanced");
                }
            }

            return dexterityAdjustment;
        }

        private int GetStrengthAdjustment(string originalSize, string advancedSize)
        {
            var strengthAdjustment = 0;
            var currentSize = originalSize;

            while (currentSize != advancedSize)
            {
                switch (currentSize)
                {
                    case SizeConstants.Fine: currentSize = SizeConstants.Diminutive; break;
                    case SizeConstants.Diminutive: currentSize = SizeConstants.Tiny; strengthAdjustment += 2; break;
                    case SizeConstants.Tiny: currentSize = SizeConstants.Small; strengthAdjustment += 4; break;
                    case SizeConstants.Small: currentSize = SizeConstants.Medium; strengthAdjustment += 4; break;
                    case SizeConstants.Medium: currentSize = SizeConstants.Large; strengthAdjustment += 8; break;
                    case SizeConstants.Large: currentSize = SizeConstants.Huge; strengthAdjustment += 8; break;
                    case SizeConstants.Huge: currentSize = SizeConstants.Gargantuan; strengthAdjustment += 8; break;
                    case SizeConstants.Gargantuan: currentSize = SizeConstants.Colossal; strengthAdjustment += 8; break;
                    case SizeConstants.Colossal:
                    default: throw new ArgumentException($"{currentSize} is not a valid size that can be advanced");
                }
            }

            return strengthAdjustment;
        }

        private int GetNaturalArmorAdjustment(string originalSize, string advancedSize)
        {
            var naturalArmorAdjustment = 0;
            var currentSize = originalSize;

            while (currentSize != advancedSize)
            {
                switch (currentSize)
                {
                    case SizeConstants.Fine: currentSize = SizeConstants.Diminutive; break;
                    case SizeConstants.Diminutive: currentSize = SizeConstants.Tiny; break;
                    case SizeConstants.Tiny: currentSize = SizeConstants.Small; break;
                    case SizeConstants.Small: currentSize = SizeConstants.Medium; break;
                    case SizeConstants.Medium: currentSize = SizeConstants.Large; naturalArmorAdjustment += 2; break;
                    case SizeConstants.Large: currentSize = SizeConstants.Huge; naturalArmorAdjustment += 3; break;
                    case SizeConstants.Huge: currentSize = SizeConstants.Gargantuan; naturalArmorAdjustment += 4; break;
                    case SizeConstants.Gargantuan: currentSize = SizeConstants.Colossal; naturalArmorAdjustment += 5; break;
                    case SizeConstants.Colossal:
                    default: throw new ArgumentException($"{currentSize} is not a valid size that can be advanced");
                }
            }

            return naturalArmorAdjustment;
        }
    }
}
