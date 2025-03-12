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

            testCases[CreatureConstants.Dragon_Copper_Wyrmling] = [GetData(CreatureConstants.Dragon_Copper_Wyrmling, SizeConstants.Small, 6, 7)];
            testCases[CreatureConstants.Dragon_Copper_VeryYoung] = [GetData(CreatureConstants.Dragon_Copper_VeryYoung, SizeConstants.Medium, 9, 10)];
            testCases[CreatureConstants.Dragon_Copper_Young] = [GetData(CreatureConstants.Dragon_Copper_Young, SizeConstants.Medium, 12, 13)];
            testCases[CreatureConstants.Dragon_Copper_Juvenile] = [GetData(CreatureConstants.Dragon_Copper_Juvenile, SizeConstants.Large, 15, 16)];
            testCases[CreatureConstants.Dragon_Copper_YoungAdult] = [GetData(CreatureConstants.Dragon_Copper_YoungAdult, SizeConstants.Large, 18, 19)];
            testCases[CreatureConstants.Dragon_Copper_Adult] = [GetData(CreatureConstants.Dragon_Copper_Adult, SizeConstants.Huge, 21, 22)];
            testCases[CreatureConstants.Dragon_Copper_MatureAdult] = [GetData(CreatureConstants.Dragon_Copper_MatureAdult, SizeConstants.Huge, 24, 25)];
            testCases[CreatureConstants.Dragon_Copper_Old] = [GetData(CreatureConstants.Dragon_Copper_Old, SizeConstants.Huge, 27, 28)];
            testCases[CreatureConstants.Dragon_Copper_VeryOld] = [GetData(CreatureConstants.Dragon_Copper_VeryOld, SizeConstants.Huge, 30, 31)];
            testCases[CreatureConstants.Dragon_Copper_Ancient] = [GetData(CreatureConstants.Dragon_Copper_Ancient, SizeConstants.Gargantuan, 33, 34)];
            testCases[CreatureConstants.Dragon_Copper_Wyrm] = [GetData(CreatureConstants.Dragon_Copper_Wyrm, SizeConstants.Gargantuan, 36, 37)];
            testCases[CreatureConstants.Dragon_Copper_GreatWyrm] = [GetData(CreatureConstants.Dragon_Copper_GreatWyrm, SizeConstants.Gargantuan, 39, 100)];

            testCases[CreatureConstants.Dragon_Gold_Wyrmling] = [GetData(CreatureConstants.Dragon_Gold_Wyrmling, SizeConstants.Medium, 9, 10)];
            testCases[CreatureConstants.Dragon_Gold_VeryYoung] = [GetData(CreatureConstants.Dragon_Gold_VeryYoung, SizeConstants.Large, 12, 13)];
            testCases[CreatureConstants.Dragon_Gold_Young] = [GetData(CreatureConstants.Dragon_Gold_Young, SizeConstants.Large, 15, 16)];
            testCases[CreatureConstants.Dragon_Gold_Juvenile] = [GetData(CreatureConstants.Dragon_Gold_Juvenile, SizeConstants.Large, 18, 19)];
            testCases[CreatureConstants.Dragon_Gold_YoungAdult] = [GetData(CreatureConstants.Dragon_Gold_YoungAdult, SizeConstants.Huge, 21, 22)];
            testCases[CreatureConstants.Dragon_Gold_Adult] = [GetData(CreatureConstants.Dragon_Gold_Adult, SizeConstants.Huge, 24, 25)];
            testCases[CreatureConstants.Dragon_Gold_MatureAdult] = [GetData(CreatureConstants.Dragon_Gold_MatureAdult, SizeConstants.Huge, 27, 28)];
            testCases[CreatureConstants.Dragon_Gold_Old] = [GetData(CreatureConstants.Dragon_Gold_Old, SizeConstants.Gargantuan, 30, 31)];
            testCases[CreatureConstants.Dragon_Gold_VeryOld] = [GetData(CreatureConstants.Dragon_Gold_VeryOld, SizeConstants.Gargantuan, 33, 34)];
            testCases[CreatureConstants.Dragon_Gold_Ancient] = [GetData(CreatureConstants.Dragon_Gold_Ancient, SizeConstants.Gargantuan, 36, 37)];
            testCases[CreatureConstants.Dragon_Gold_Wyrm] = [GetData(CreatureConstants.Dragon_Gold_Wyrm, SizeConstants.Gargantuan, 39, 40)];
            testCases[CreatureConstants.Dragon_Gold_GreatWyrm] = [GetData(CreatureConstants.Dragon_Gold_GreatWyrm, SizeConstants.Colossal, 42, 100)];

            testCases[CreatureConstants.Dragon_Silver_Wyrmling] = [GetData(CreatureConstants.Dragon_Silver_Wyrmling, SizeConstants.Medium, 8, 9)];
            testCases[CreatureConstants.Dragon_Silver_VeryYoung] = [GetData(CreatureConstants.Dragon_Silver_VeryYoung, SizeConstants.Large, 11, 12)];
            testCases[CreatureConstants.Dragon_Silver_Young] = [GetData(CreatureConstants.Dragon_Silver_Young, SizeConstants.Large, 14, 15)];
            testCases[CreatureConstants.Dragon_Silver_Juvenile] = [GetData(CreatureConstants.Dragon_Silver_Juvenile, SizeConstants.Large, 17, 18)];
            testCases[CreatureConstants.Dragon_Silver_YoungAdult] = [GetData(CreatureConstants.Dragon_Silver_YoungAdult, SizeConstants.Huge, 20, 21)];
            testCases[CreatureConstants.Dragon_Silver_Adult] = [GetData(CreatureConstants.Dragon_Silver_Adult, SizeConstants.Huge, 23, 24)];
            testCases[CreatureConstants.Dragon_Silver_MatureAdult] = [GetData(CreatureConstants.Dragon_Silver_MatureAdult, SizeConstants.Huge, 26, 27)];
            testCases[CreatureConstants.Dragon_Silver_Old] = [GetData(CreatureConstants.Dragon_Silver_Old, SizeConstants.Gargantuan, 29, 30)];
            testCases[CreatureConstants.Dragon_Silver_VeryOld] = [GetData(CreatureConstants.Dragon_Silver_VeryOld, SizeConstants.Gargantuan, 32, 33)];
            testCases[CreatureConstants.Dragon_Silver_Ancient] = [GetData(CreatureConstants.Dragon_Silver_Ancient, SizeConstants.Gargantuan, 35, 36)];
            testCases[CreatureConstants.Dragon_Silver_Wyrm] = [GetData(CreatureConstants.Dragon_Silver_Wyrm, SizeConstants.Gargantuan, 38, 39)];
            testCases[CreatureConstants.Dragon_Silver_GreatWyrm] = [GetData(CreatureConstants.Dragon_Silver_GreatWyrm, SizeConstants.Colossal, 41, 100)];

            testCases[CreatureConstants.DragonTurtle] =
            [
                GetData(CreatureConstants.DragonTurtle, SizeConstants.Huge, 13, 24),
                GetData(CreatureConstants.DragonTurtle, SizeConstants.Gargantuan, 25, 36),
            ];
            testCases[CreatureConstants.Dragonne] =
            [
                GetData(CreatureConstants.Dragonne, SizeConstants.Large, 10, 12),
                GetData(CreatureConstants.Dragonne, SizeConstants.Huge, 13, 27),
            ];
            testCases[CreatureConstants.Dretch] = [GetData(CreatureConstants.Dretch, SizeConstants.Small, 3, 6)];
            testCases[CreatureConstants.Eagle] = [GetData(CreatureConstants.Eagle, SizeConstants.Medium, 2, 3)];
            testCases[CreatureConstants.Eagle_Giant] =
            [
                GetData(CreatureConstants.Eagle_Giant, SizeConstants.Large, 5, 8),
                GetData(CreatureConstants.Eagle_Giant, SizeConstants.Huge, 9, 12),
            ];
            testCases[CreatureConstants.Efreeti] =
            [
                GetData(CreatureConstants.Efreeti, SizeConstants.Large, 11, 15),
                GetData(CreatureConstants.Efreeti, SizeConstants.Huge, 16, 30),
            ];
            testCases[CreatureConstants.Elasmosaurus] =
            [
                GetData(CreatureConstants.Elasmosaurus, SizeConstants.Huge, 11, 20),
                GetData(CreatureConstants.Elasmosaurus, SizeConstants.Gargantuan, 21, 30),
            ];
            testCases[CreatureConstants.Elemental_Air_Elder] = [GetData(CreatureConstants.Elemental_Air_Elder, SizeConstants.Huge, 25, 48)];
            testCases[CreatureConstants.Elemental_Air_Greater] = [GetData(CreatureConstants.Elemental_Air_Greater, SizeConstants.Huge, 22, 23)];
            testCases[CreatureConstants.Elemental_Air_Huge] = [GetData(CreatureConstants.Elemental_Air_Huge, SizeConstants.Huge, 17, 20)];
            testCases[CreatureConstants.Elemental_Air_Large] = [GetData(CreatureConstants.Elemental_Air_Large, SizeConstants.Large, 9, 15)];
            testCases[CreatureConstants.Elemental_Air_Medium] = [GetData(CreatureConstants.Elemental_Air_Medium, SizeConstants.Medium, 5, 7)];
            testCases[CreatureConstants.Elemental_Air_Small] = [GetData(CreatureConstants.Elemental_Air_Small, SizeConstants.Small, 3, 3)];
            testCases[CreatureConstants.Elemental_Earth_Elder] = [GetData(CreatureConstants.Elemental_Earth_Elder, SizeConstants.Huge, 25, 48)];
            testCases[CreatureConstants.Elemental_Earth_Greater] = [GetData(CreatureConstants.Elemental_Earth_Greater, SizeConstants.Huge, 22, 23)];
            testCases[CreatureConstants.Elemental_Earth_Huge] = [GetData(CreatureConstants.Elemental_Earth_Huge, SizeConstants.Huge, 17, 20)];
            testCases[CreatureConstants.Elemental_Earth_Large] = [GetData(CreatureConstants.Elemental_Earth_Large, SizeConstants.Large, 9, 15)];
            testCases[CreatureConstants.Elemental_Earth_Medium] = [GetData(CreatureConstants.Elemental_Earth_Medium, SizeConstants.Medium, 5, 7)];
            testCases[CreatureConstants.Elemental_Earth_Small] = [GetData(CreatureConstants.Elemental_Earth_Small, SizeConstants.Small, 3, 3)];
            testCases[CreatureConstants.Elemental_Fire_Elder] = [GetData(CreatureConstants.Elemental_Fire_Elder, SizeConstants.Huge, 25, 48)];
            testCases[CreatureConstants.Elemental_Fire_Greater] = [GetData(CreatureConstants.Elemental_Fire_Greater, SizeConstants.Huge, 22, 23)];
            testCases[CreatureConstants.Elemental_Fire_Huge] = [GetData(CreatureConstants.Elemental_Fire_Huge, SizeConstants.Huge, 17, 20)];
            testCases[CreatureConstants.Elemental_Fire_Large] = [GetData(CreatureConstants.Elemental_Fire_Large, SizeConstants.Large, 9, 15)];
            testCases[CreatureConstants.Elemental_Fire_Medium] = [GetData(CreatureConstants.Elemental_Fire_Medium, SizeConstants.Medium, 5, 7)];
            testCases[CreatureConstants.Elemental_Fire_Small] = [GetData(CreatureConstants.Elemental_Fire_Small, SizeConstants.Small, 3, 3)];
            testCases[CreatureConstants.Elemental_Water_Elder] = [GetData(CreatureConstants.Elemental_Water_Elder, SizeConstants.Huge, 25, 48)];
            testCases[CreatureConstants.Elemental_Water_Greater] = [GetData(CreatureConstants.Elemental_Water_Greater, SizeConstants.Huge, 22, 23)];
            testCases[CreatureConstants.Elemental_Water_Huge] = [GetData(CreatureConstants.Elemental_Water_Huge, SizeConstants.Huge, 17, 20)];
            testCases[CreatureConstants.Elemental_Water_Large] = [GetData(CreatureConstants.Elemental_Water_Large, SizeConstants.Large, 9, 15)];
            testCases[CreatureConstants.Elemental_Water_Medium] = [GetData(CreatureConstants.Elemental_Water_Medium, SizeConstants.Medium, 5, 7)];
            testCases[CreatureConstants.Elemental_Water_Small] = [GetData(CreatureConstants.Elemental_Water_Small, SizeConstants.Small, 3, 3)];
            testCases[CreatureConstants.Elephant] = [GetData(CreatureConstants.Elephant, SizeConstants.Huge, 12, 22)];
            testCases[CreatureConstants.Erinyes] = [GetData(CreatureConstants.Erinyes, SizeConstants.Medium, 10, 18)];
            testCases[CreatureConstants.EtherealFilcher] =
            [
                GetData(CreatureConstants.EtherealFilcher, SizeConstants.Medium, 6, 7),
                GetData(CreatureConstants.EtherealFilcher, SizeConstants.Large, 8, 15),
            ];
            testCases[CreatureConstants.EtherealMarauder] =
            [
                GetData(CreatureConstants.EtherealMarauder, SizeConstants.Medium, 3, 4),
                GetData(CreatureConstants.EtherealMarauder, SizeConstants.Large, 5, 6),
            ];
            testCases[CreatureConstants.Ettercap] =
            [
                GetData(CreatureConstants.Ettercap, SizeConstants.Medium, 6, 7),
                GetData(CreatureConstants.Ettercap, SizeConstants.Large, 8, 15),
            ];
            testCases[CreatureConstants.FireBeetle_Giant] = [GetData(CreatureConstants.FireBeetle_Giant, SizeConstants.Small, 2, 3)];
            testCases[CreatureConstants.FormianMyrmarch] =
            [
                GetData(CreatureConstants.FormianMyrmarch, SizeConstants.Large, 13, 18),
                GetData(CreatureConstants.FormianMyrmarch, SizeConstants.Huge, 19, 24),
            ];
            testCases[CreatureConstants.FormianQueen] =
            [
                GetData(CreatureConstants.FormianQueen, SizeConstants.Huge, 21, 30),
                GetData(CreatureConstants.FormianQueen, SizeConstants.Gargantuan, 31, 40),
            ];
            testCases[CreatureConstants.FormianTaskmaster] =
            [
                GetData(CreatureConstants.FormianTaskmaster, SizeConstants.Medium, 7, 9),
                GetData(CreatureConstants.FormianTaskmaster, SizeConstants.Large, 10, 12),
            ];
            testCases[CreatureConstants.FormianWarrior] =
            [
                GetData(CreatureConstants.FormianWarrior, SizeConstants.Medium, 5, 8),
                GetData(CreatureConstants.FormianWarrior, SizeConstants.Large, 9, 12),
            ];
            testCases[CreatureConstants.FormianWorker] = [GetData(CreatureConstants.FormianWorker, SizeConstants.Medium, 2, 3)];
            testCases[CreatureConstants.FrostWorm] =
            [
                GetData(CreatureConstants.FrostWorm, SizeConstants.Huge, 15, 21),
                GetData(CreatureConstants.FrostWorm, SizeConstants.Gargantuan, 22, 42),
            ];
            testCases[CreatureConstants.Gargoyle] =
            [
                GetData(CreatureConstants.Gargoyle, SizeConstants.Medium, 5, 6),
                GetData(CreatureConstants.Gargoyle, SizeConstants.Large, 7, 12),
            ];
            testCases[CreatureConstants.Gargoyle_Kapoacinth] =
            [
                GetData(CreatureConstants.Gargoyle_Kapoacinth, SizeConstants.Medium, 5, 6),
                GetData(CreatureConstants.Gargoyle_Kapoacinth, SizeConstants.Large, 7, 12),
            ];
            testCases[CreatureConstants.Gargoyle_Kapoacinth] =
            [
                GetData(CreatureConstants.Gargoyle_Kapoacinth, SizeConstants.Medium, 5, 6),
                GetData(CreatureConstants.Gargoyle_Kapoacinth, SizeConstants.Large, 7, 12),
            ];
            testCases[CreatureConstants.GelatinousCube] =
            [
                GetData(CreatureConstants.GelatinousCube, SizeConstants.Large, 5, 12),
                GetData(CreatureConstants.GelatinousCube, SizeConstants.Huge, 13, 24),
            ];
            testCases[CreatureConstants.Ghaele] =
            [
                GetData(CreatureConstants.Ghaele, SizeConstants.Medium, 11, 15),
                GetData(CreatureConstants.Ghaele, SizeConstants.Large, 16, 30),
            ];
            testCases[CreatureConstants.Ghoul] = [GetData(CreatureConstants.Ghoul, SizeConstants.Medium, 3, 3)];
            testCases[CreatureConstants.Ghoul_Ghast] = [GetData(CreatureConstants.Ghoul_Ghast, SizeConstants.Medium, 5, 8)];
            testCases[CreatureConstants.Ghoul_Lacedon] = [GetData(CreatureConstants.Ghoul_Lacedon, SizeConstants.Medium, 3, 3)];
            testCases[CreatureConstants.GibberingMouther] = [GetData(CreatureConstants.GibberingMouther, SizeConstants.Large, 5, 12)];
            testCases[CreatureConstants.Girallon] =
            [
                GetData(CreatureConstants.Girallon, SizeConstants.Large, 8, 10),
                GetData(CreatureConstants.Girallon, SizeConstants.Huge, 11, 21),
            ];
            testCases[CreatureConstants.Glabrezu] =
            [
                GetData(CreatureConstants.Glabrezu, SizeConstants.Huge, 13, 18),
                GetData(CreatureConstants.Glabrezu, SizeConstants.Gargantuan, 19, 36),
            ];
            testCases[CreatureConstants.Golem_Clay] =
            [
                GetData(CreatureConstants.Golem_Clay, SizeConstants.Large, 12, 18),
                GetData(CreatureConstants.Golem_Clay, SizeConstants.Huge, 19, 33),
            ];
            testCases[CreatureConstants.Golem_Flesh] =
            [
                GetData(CreatureConstants.Golem_Flesh, SizeConstants.Large, 10, 18),
                GetData(CreatureConstants.Golem_Flesh, SizeConstants.Huge, 19, 27),
            ];
            testCases[CreatureConstants.Golem_Iron] =
            [
                GetData(CreatureConstants.Golem_Iron, SizeConstants.Large, 19, 24),
                GetData(CreatureConstants.Golem_Iron, SizeConstants.Huge, 25, 54),
            ];
            testCases[CreatureConstants.Golem_Stone] =
            [
                GetData(CreatureConstants.Golem_Stone, SizeConstants.Large, 15, 21),
                GetData(CreatureConstants.Golem_Stone, SizeConstants.Huge, 22, 42),
            ];
            testCases[CreatureConstants.Gorgon] =
            [
                GetData(CreatureConstants.Gorgon, SizeConstants.Large, 9, 15),
                GetData(CreatureConstants.Gorgon, SizeConstants.Huge, 16, 24),
            ];
            testCases[CreatureConstants.GrayOoze] =
            [
                GetData(CreatureConstants.GrayOoze, SizeConstants.Medium, 4, 6),
                GetData(CreatureConstants.GrayOoze, SizeConstants.Large, 7, 9),
            ];
            testCases[CreatureConstants.GrayRender] =
            [
                GetData(CreatureConstants.GrayRender, SizeConstants.Large, 11, 15),
                GetData(CreatureConstants.GrayRender, SizeConstants.Huge, 16, 30),
            ];
            testCases[CreatureConstants.Grick] =
            [
                GetData(CreatureConstants.Grick, SizeConstants.Medium, 3, 4),
                GetData(CreatureConstants.Grick, SizeConstants.Large, 5, 6),
            ];
            testCases[CreatureConstants.Griffon] =
            [
                GetData(CreatureConstants.Griffon, SizeConstants.Large, 8, 10),
                GetData(CreatureConstants.Griffon, SizeConstants.Huge, 11, 21),
            ];
            testCases[CreatureConstants.Grig] = [GetData(CreatureConstants.Grig, SizeConstants.Tiny, 1, 3)];
            testCases[CreatureConstants.Grig_WithFiddle] = [GetData(CreatureConstants.Grig_WithFiddle, SizeConstants.Tiny, 1, 3)];
            testCases[CreatureConstants.Gynosphinx] =
            [
                GetData(CreatureConstants.Gynosphinx, SizeConstants.Large, 9, 12),
                GetData(CreatureConstants.Gynosphinx, SizeConstants.Huge, 13, 24),
            ];

            testCases[CreatureConstants.HellHound][GetData(5, 8, SizeConstants.Medium, 5, 5);
            testCases[CreatureConstants.HellHound][GetData(9, 12, SizeConstants.Large, 10, 5);
            testCases[CreatureConstants.HellHound_NessianWarhound][GetData(13, 17, SizeConstants.Large, 10, 10);
            testCases[CreatureConstants.HellHound_NessianWarhound][GetData(18, 24, SizeConstants.Huge, 15, 15);
            testCases[CreatureConstants.Hellcat_Bezekira][GetData(9, 10, SizeConstants.Large, 10, 5);
            testCases[CreatureConstants.Hellcat_Bezekira][GetData(11, 24, SizeConstants.Huge, 15, 10);
            testCases[CreatureConstants.Hezrou][GetData(11, 15, SizeConstants.Large, 10, 10);
            testCases[CreatureConstants.Hezrou][GetData(16, 30, SizeConstants.Huge, 15, 15);
            testCases[CreatureConstants.Hieracosphinx][GetData(10, 14, SizeConstants.Large, 10, 5);
            testCases[CreatureConstants.Hieracosphinx][GetData(15, 27, SizeConstants.Huge, 15, 10);
            testCases[CreatureConstants.Hippogriff][GetData(4, 6, SizeConstants.Large, 10, 5);
            testCases[CreatureConstants.Hippogriff][GetData(7, 9, SizeConstants.Huge, 15, 10);
            testCases[CreatureConstants.Homunculus][GetData(3, 6, SizeConstants.Tiny, 2.5, 0);
            testCases[CreatureConstants.HornedDevil_Cornugon][GetData(16, 20, SizeConstants.Large, 10, 10);
            testCases[CreatureConstants.HornedDevil_Cornugon][GetData(21, 45, SizeConstants.Huge, 15, 15);
            testCases[CreatureConstants.HoundArchon][GetData(7, 9, SizeConstants.Medium, 5, 5);
            testCases[CreatureConstants.HoundArchon][GetData(10, 18, SizeConstants.Large, 10, 10);
            testCases[CreatureConstants.Howler][GetData(7, 9, SizeConstants.Large, 10, 5);
            testCases[CreatureConstants.Howler][GetData(10, 18, SizeConstants.Huge, 15, 10);
            testCases[CreatureConstants.Hyena][GetData(3, 3, SizeConstants.Medium, 5, 5);
            testCases[CreatureConstants.Hyena][GetData(4, 5, SizeConstants.Large, 10, 5);
            testCases[CreatureConstants.IceDevil_Gelugon][GetData(15, 28, SizeConstants.Large, 10, 10);
            testCases[CreatureConstants.IceDevil_Gelugon][GetData(29, 42, SizeConstants.Huge, 15, 15);
            testCases[CreatureConstants.Imp][GetData(4, 6, SizeConstants.Tiny, 2.5, 0);
            testCases[CreatureConstants.InvisibleStalker][GetData(9, 12, SizeConstants.Large, 10, 10);
            testCases[CreatureConstants.InvisibleStalker][GetData(13, 24, SizeConstants.Huge, 15, 15);
            testCases[CreatureConstants.Janni][GetData(7, 9, SizeConstants.Medium, 5, 5);
            testCases[CreatureConstants.Janni][GetData(10, 18, SizeConstants.Large, 10, 10);
            testCases[CreatureConstants.Kolyarut][GetData(14, 22, SizeConstants.Medium, 5, 5);
            testCases[CreatureConstants.Kolyarut][GetData(23, 39, SizeConstants.Large, 10, 10);
            testCases[CreatureConstants.Kraken][GetData(21, 32, SizeConstants.Gargantuan, 20, 15);
            testCases[CreatureConstants.Kraken][GetData(33, 60, SizeConstants.Colossal, 30, 20);
            testCases[CreatureConstants.Krenshar][GetData(3, 4, SizeConstants.Medium, 5, 5);
            testCases[CreatureConstants.Krenshar][GetData(5, 8, SizeConstants.Large, 10, 5);
            testCases[CreatureConstants.Lamia][GetData(10, 13, SizeConstants.Large, 10, 5);
            testCases[CreatureConstants.Lamia][GetData(14, 27, SizeConstants.Huge, 15, 10);
            testCases[CreatureConstants.Lammasu][GetData(8, 10, SizeConstants.Large, 10, 5);
            testCases[CreatureConstants.Lammasu][GetData(11, 21, SizeConstants.Huge, 15, 10);
            testCases[CreatureConstants.LanternArchon][GetData(2, 4, SizeConstants.Small, 5, 5);
            testCases[CreatureConstants.Lemure][GetData(3, 6, SizeConstants.Medium, 5, 5);
            testCases[CreatureConstants.Leonal][GetData(13, 18, SizeConstants.Medium, 5, 5);
            testCases[CreatureConstants.Leonal][GetData(19, 36, SizeConstants.Large, 10, 10);
            testCases[CreatureConstants.Leopard][GetData(4, 5, SizeConstants.Medium, 5, 5);
            testCases[CreatureConstants.Lillend][GetData(8, 10, SizeConstants.Large, 10, 10);
            testCases[CreatureConstants.Lillend][GetData(11, 21, SizeConstants.Huge, 15, 15);
            testCases[CreatureConstants.Lion][GetData(6, 8, SizeConstants.Large, 10, 5);
            testCases[CreatureConstants.Lion_Dire][GetData(9, 16, SizeConstants.Large, 10, 5);
            testCases[CreatureConstants.Lion_Dire][GetData(17, 24, SizeConstants.Huge, 15, 10);
            testCases[CreatureConstants.Lizard_Monitor][GetData(4, 5, SizeConstants.Medium, 5, 5);
            testCases[CreatureConstants.Magmin][GetData(3, 4, SizeConstants.Small, 5, 5);
            testCases[CreatureConstants.Magmin][GetData(5, 6, SizeConstants.Medium, 5, 5);
            testCases[CreatureConstants.MantaRay][GetData(5, 6, SizeConstants.Large, 10, 5);
            testCases[CreatureConstants.Manticore][GetData(7, 16, SizeConstants.Large, 10, 5);
            testCases[CreatureConstants.Manticore][GetData(17, 18, SizeConstants.Huge, 15, 10);
            testCases[CreatureConstants.Marilith][GetData(17, 20, SizeConstants.Large, 10, 10);
            testCases[CreatureConstants.Marilith][GetData(21, 48, SizeConstants.Huge, 15, 15);
            testCases[CreatureConstants.Marut][GetData(16, 28, SizeConstants.Large, 10, 10);
            testCases[CreatureConstants.Marut][GetData(29, 45, SizeConstants.Huge, 15, 15);
            testCases[CreatureConstants.Megaraptor][GetData(9, 16, SizeConstants.Large, 10, 5);
            testCases[CreatureConstants.Megaraptor][GetData(17, 24, SizeConstants.Huge, 15, 10);
            testCases[CreatureConstants.Mephit_Air][GetData(4, 6, SizeConstants.Small, 5, 5);
            testCases[CreatureConstants.Mephit_Air][GetData(7, 9, SizeConstants.Medium, 5, 5);
            testCases[CreatureConstants.Mephit_Dust][GetData(4, 6, SizeConstants.Small, 5, 5);
            testCases[CreatureConstants.Mephit_Dust][GetData(7, 9, SizeConstants.Medium, 5, 5);
            testCases[CreatureConstants.Mephit_Earth][GetData(4, 6, SizeConstants.Small, 5, 5);
            testCases[CreatureConstants.Mephit_Earth][GetData(7, 9, SizeConstants.Medium, 5, 5);
            testCases[CreatureConstants.Mephit_Fire][GetData(4, 6, SizeConstants.Small, 5, 5);
            testCases[CreatureConstants.Mephit_Fire][GetData(7, 9, SizeConstants.Medium, 5, 5);
            testCases[CreatureConstants.Mephit_Ice][GetData(4, 6, SizeConstants.Small, 5, 5);
            testCases[CreatureConstants.Mephit_Ice][GetData(7, 9, SizeConstants.Medium, 5, 5);
            testCases[CreatureConstants.Mephit_Magma][GetData(4, 6, SizeConstants.Small, 5, 5);
            testCases[CreatureConstants.Mephit_Magma][GetData(7, 9, SizeConstants.Medium, 5, 5);
            testCases[CreatureConstants.Mephit_Ooze][GetData(4, 6, SizeConstants.Small, 5, 5);
            testCases[CreatureConstants.Mephit_Ooze][GetData(7, 9, SizeConstants.Medium, 5, 5);
            testCases[CreatureConstants.Mephit_Salt][GetData(4, 6, SizeConstants.Small, 5, 5);
            testCases[CreatureConstants.Mephit_Salt][GetData(7, 9, SizeConstants.Medium, 5, 5);
            testCases[CreatureConstants.Mephit_Steam][GetData(4, 6, SizeConstants.Small, 5, 5);
            testCases[CreatureConstants.Mephit_Steam][GetData(7, 9, SizeConstants.Medium, 5, 5);
            testCases[CreatureConstants.Mephit_Water][GetData(4, 6, SizeConstants.Small, 5, 5);
            testCases[CreatureConstants.Mephit_Water][GetData(7, 9, SizeConstants.Medium, 5, 5);
            testCases[CreatureConstants.Mimic][GetData(8, 10, SizeConstants.Large, 10, 10);
            testCases[CreatureConstants.Mimic][GetData(11, 21, SizeConstants.Huge, 15, 15);
            testCases[CreatureConstants.Mohrg][GetData(15, 21, SizeConstants.Medium, 5, 5);
            testCases[CreatureConstants.Mohrg][GetData(22, 28, SizeConstants.Large, 10, 10);
            testCases[CreatureConstants.Monkey][GetData(2, 3, SizeConstants.Small, 5, 5);
            testCases[CreatureConstants.Mummy][GetData(9, 16, SizeConstants.Medium, 5, 5);
            testCases[CreatureConstants.Mummy][GetData(17, 24, SizeConstants.Large, 10, 10);
            testCases[CreatureConstants.Naga_Dark][GetData(10, 13, SizeConstants.Large, 10, 5);
            testCases[CreatureConstants.Naga_Dark][GetData(14, 27, SizeConstants.Huge, 15, 10);
            testCases[CreatureConstants.Naga_Guardian][GetData(12, 16, SizeConstants.Large, 10, 5);
            testCases[CreatureConstants.Naga_Guardian][GetData(17, 33, SizeConstants.Huge, 15, 10);
            testCases[CreatureConstants.Naga_Spirit][GetData(10, 13, SizeConstants.Large, 10, 5);
            testCases[CreatureConstants.Naga_Spirit][GetData(14, 27, SizeConstants.Huge, 15, 10);
            testCases[CreatureConstants.Naga_Water][GetData(8, 10, SizeConstants.Large, 10, 5);
            testCases[CreatureConstants.Naga_Water][GetData(11, 21, SizeConstants.Huge, 15, 10);
            testCases[CreatureConstants.Nalfeshnee][GetData(15, 20, SizeConstants.Huge, 15, 15);
            testCases[CreatureConstants.Nalfeshnee][GetData(21, 42, SizeConstants.Gargantuan, 20, 20);
            testCases[CreatureConstants.NightHag][GetData(9, 16, SizeConstants.Medium, 5, 5);
            testCases[CreatureConstants.Nightcrawler][GetData(26, 50, SizeConstants.Colossal, 30, 20);
            testCases[CreatureConstants.Nightmare][GetData(7, 10, SizeConstants.Large, 10, 5);
            testCases[CreatureConstants.Nightmare][GetData(11, 18, SizeConstants.Huge, 15, 10);
            testCases[CreatureConstants.Nightwalker][GetData(22, 31, SizeConstants.Huge, 15, 15);
            testCases[CreatureConstants.Nightwalker][GetData(32, 42, SizeConstants.Gargantuan, 20, 20);
            testCases[CreatureConstants.Nightwing][GetData(18, 25, SizeConstants.Huge, 15, 10);
            testCases[CreatureConstants.Nightwing][GetData(26, 34, SizeConstants.Gargantuan, 20, 15);
            testCases[CreatureConstants.Nixie][GetData(2, 3, SizeConstants.Small, 5, 5);
            testCases[CreatureConstants.Nymph][GetData(7, 12, SizeConstants.Medium, 5, 5);
            testCases[CreatureConstants.OchreJelly][GetData(7, 9, SizeConstants.Large, 10, 5);
            testCases[CreatureConstants.OchreJelly][GetData(10, 18, SizeConstants.Huge, 15, 10);
            testCases[CreatureConstants.Octopus][GetData(3, 6, SizeConstants.Medium, 5, 5);
            testCases[CreatureConstants.Octopus_Giant][GetData(9, 12, SizeConstants.Large, 10, 10);
            testCases[CreatureConstants.Octopus_Giant][GetData(13, 24, SizeConstants.Huge, 15, 15);
            testCases[CreatureConstants.Otyugh][GetData(7, 8, SizeConstants.Large, 10, 10);
            testCases[CreatureConstants.Otyugh][GetData(9, 18, SizeConstants.Huge, 15, 15);
            testCases[CreatureConstants.Owl][GetData(2, 2, SizeConstants.Small, 5, 5);
            testCases[CreatureConstants.Owl_Giant][GetData(5, 8, SizeConstants.Large, 10, 5);
            testCases[CreatureConstants.Owl_Giant][GetData(9, 12, SizeConstants.Huge, 15, 10);
            testCases[CreatureConstants.Owlbear][GetData(6, 8, SizeConstants.Large, 10, 5);
            testCases[CreatureConstants.Owlbear][GetData(9, 15, SizeConstants.Huge, 15, 10);
            testCases[CreatureConstants.Pegasus][GetData(5, 8, SizeConstants.Large, 10, 5);
            testCases[CreatureConstants.PhantomFungus][GetData(3, 4, SizeConstants.Medium, 5, 5);
            testCases[CreatureConstants.PhantomFungus][GetData(5, 6, SizeConstants.Large, 10, 5);
            testCases[CreatureConstants.PhaseSpider][GetData(6, 8, SizeConstants.Large, 10, 5);
            testCases[CreatureConstants.PhaseSpider][GetData(9, 15, SizeConstants.Huge, 15, 10);
            testCases[CreatureConstants.Phasm][GetData(16, 21, SizeConstants.Huge, 15, 15);
            testCases[CreatureConstants.Phasm][GetData(22, 45, SizeConstants.Gargantuan, 20, 20);
            testCases[CreatureConstants.PitFiend][GetData(19, 36, SizeConstants.Large, 10, 10);
            testCases[CreatureConstants.PitFiend][GetData(37, 54, SizeConstants.Huge, 15, 15);
            testCases[CreatureConstants.Pixie][GetData(2, 3, SizeConstants.Small, 5, 5);
            testCases[CreatureConstants.Pixie_WithIrresistibleDance][GetData(2, 3, SizeConstants.Small, 5, 5);
            testCases[CreatureConstants.Porpoise][GetData(3, 4, SizeConstants.Medium, 5, 5);
            testCases[CreatureConstants.Porpoise][GetData(3, 4, SizeConstants.Medium, 5, 5);
            testCases[CreatureConstants.PrayingMantis_Giant][GetData(5, 8, SizeConstants.Large, 10, 5);
            testCases[CreatureConstants.PrayingMantis_Giant][GetData(9, 12, SizeConstants.Huge, 15, 10);
            testCases[CreatureConstants.Pseudodragon][GetData(3, 4, SizeConstants.Tiny, 2.5, 0);
            testCases[CreatureConstants.PurpleWorm][GetData(17, 32, SizeConstants.Gargantuan, 20, 15);
            testCases[CreatureConstants.PurpleWorm][GetData(33, 48, SizeConstants.Colossal, 30, 20);
            testCases[CreatureConstants.Quasit][GetData(4, 6, SizeConstants.Tiny, 2.5, 0);
            testCases[CreatureConstants.Rast][GetData(5, 6, SizeConstants.Medium, 5, 5);
            testCases[CreatureConstants.Rast][GetData(7, 12, SizeConstants.Large, 10, 5);
            testCases[CreatureConstants.Rat_Dire][GetData(2, 3, SizeConstants.Small, 5, 5);
            testCases[CreatureConstants.Rat_Dire][GetData(4, 6, SizeConstants.Medium, 5, 5);
            testCases[CreatureConstants.Ravid][GetData(4, 4, SizeConstants.Medium, 5, 5);
            testCases[CreatureConstants.Ravid][GetData(5, 9, SizeConstants.Large, 10, 5);
            testCases[CreatureConstants.RazorBoar][GetData(16, 30, SizeConstants.Large, 10, 5);
            testCases[CreatureConstants.RazorBoar][GetData(31, 45, SizeConstants.Huge, 15, 10);
            testCases[CreatureConstants.Remorhaz][GetData(8, 14, SizeConstants.Huge, 15, 10);
            testCases[CreatureConstants.Remorhaz][GetData(15, 21, SizeConstants.Gargantuan, 20, 15);
            testCases[CreatureConstants.Retriever][GetData(11, 15, SizeConstants.Huge, 15, 10);
            testCases[CreatureConstants.Retriever][GetData(16, 30, SizeConstants.Gargantuan, 20, 15);
            testCases[CreatureConstants.Rhinoceras][GetData(9, 12, SizeConstants.Large, 10, 5);
            testCases[CreatureConstants.Rhinoceras][GetData(13, 24, SizeConstants.Huge, 15, 10);
            testCases[CreatureConstants.Roc][GetData(19, 32, SizeConstants.Gargantuan, 20, 15);
            testCases[CreatureConstants.Roc][GetData(33, 54, SizeConstants.Colossal, 30, 20);
            testCases[CreatureConstants.Roper][GetData(11, 15, SizeConstants.Large, 10, 10);
            testCases[CreatureConstants.Roper][GetData(16, 30, SizeConstants.Huge, 15, 15);
            testCases[CreatureConstants.RustMonster][GetData(6, 8, SizeConstants.Medium, 5, 5);
            testCases[CreatureConstants.RustMonster][GetData(9, 15, SizeConstants.Large, 10, 5);
            testCases[CreatureConstants.Sahuagin][GetData(3, 5, SizeConstants.Medium, 5, 5);
            testCases[CreatureConstants.Sahuagin][GetData(6, 10, SizeConstants.Large, 10, 10);
            testCases[CreatureConstants.Sahuagin_Malenti][GetData(3, 5, SizeConstants.Medium, 5, 5);
            testCases[CreatureConstants.Sahuagin_Malenti][GetData(6, 10, SizeConstants.Large, 10, 10);
            testCases[CreatureConstants.Sahuagin_Mutant][GetData(3, 5, SizeConstants.Medium, 5, 5);
            testCases[CreatureConstants.Sahuagin_Mutant][GetData(6, 10, SizeConstants.Large, 10, 10);
            testCases[CreatureConstants.Salamander_Average][GetData(10, 14, SizeConstants.Medium, 5, 5);
            testCases[CreatureConstants.Salamander_Flamebrother][GetData(5, 6, SizeConstants.Small, 5, 5);
            testCases[CreatureConstants.Salamander_Noble][GetData(16, 21, SizeConstants.Large, 10, 10);
            testCases[CreatureConstants.Salamander_Noble][GetData(22, 45, SizeConstants.Huge, 15, 15);
            testCases[CreatureConstants.Satyr][GetData(6, 10, SizeConstants.Medium, 5, 5);
            testCases[CreatureConstants.Satyr_WithPipes][GetData(6, 10, SizeConstants.Medium, 5, 5);
            testCases[CreatureConstants.Scorpion_Monstrous_Colossal][GetData(41, 60, SizeConstants.Colossal, 40, 30);
            testCases[CreatureConstants.Scorpion_Monstrous_Gargantuan][GetData(21, 39, SizeConstants.Gargantuan, 20, 15);
            testCases[CreatureConstants.Scorpion_Monstrous_Huge][GetData(11, 19, SizeConstants.Huge, 15, 10);
            testCases[CreatureConstants.Scorpion_Monstrous_Large][GetData(6, 9, SizeConstants.Large, 10, 5);
            testCases[CreatureConstants.Scorpion_Monstrous_Medium][GetData(3, 4, SizeConstants.Medium, 5, 5);
            testCases[CreatureConstants.SeaCat][GetData(7, 9, SizeConstants.Large, 10, 5);
            testCases[CreatureConstants.SeaCat][GetData(10, 18, SizeConstants.Huge, 15, 10);
            testCases[CreatureConstants.Shadow][GetData(4, 9, SizeConstants.Medium, 5, 5);
            testCases[CreatureConstants.ShadowMastiff][GetData(5, 6, SizeConstants.Medium, 5, 5);
            testCases[CreatureConstants.ShadowMastiff][GetData(7, 12, SizeConstants.Large, 10, 5);
            testCases[CreatureConstants.ShamblingMound][GetData(9, 12, SizeConstants.Large, 10, 10);
            testCases[CreatureConstants.ShamblingMound][GetData(13, 24, SizeConstants.Huge, 15, 15);
            testCases[CreatureConstants.Shark_Dire][GetData(19, 32, SizeConstants.Huge, 15, 10);
            testCases[CreatureConstants.Shark_Dire][GetData(33, 54, SizeConstants.Gargantuan, 20, 15);
            testCases[CreatureConstants.Shark_Huge][GetData(11, 17, SizeConstants.Huge, 15, 10);
            testCases[CreatureConstants.Shark_Large][GetData(8, 9, SizeConstants.Large, 10, 5);
            testCases[CreatureConstants.Shark_Medium][GetData(4, 6, SizeConstants.Medium, 5, 5);
            testCases[CreatureConstants.ShieldGuardian][GetData(16, 24, SizeConstants.Large, 10, 10);
            testCases[CreatureConstants.ShieldGuardian][GetData(25, 45, SizeConstants.Huge, 15, 15);
            testCases[CreatureConstants.ShockerLizard][GetData(3, 4, SizeConstants.Small, 5, 5);
            testCases[CreatureConstants.ShockerLizard][GetData(5, 6, SizeConstants.Medium, 5, 5);
            testCases[CreatureConstants.Shrieker][GetData(3, 3, SizeConstants.Medium, 5, 5);
            testCases[CreatureConstants.Skum][GetData(3, 4, SizeConstants.Medium, 5, 5);
            testCases[CreatureConstants.Skum][GetData(5, 6, SizeConstants.Large, 10, 10);
            testCases[CreatureConstants.Slaad_Blue][GetData(9, 12, SizeConstants.Large, 10, 10);
            testCases[CreatureConstants.Slaad_Blue][GetData(13, 24, SizeConstants.Huge, 15, 15);
            testCases[CreatureConstants.Slaad_Death][GetData(16, 22, SizeConstants.Medium, 5, 5);
            testCases[CreatureConstants.Slaad_Death][GetData(23, 45, SizeConstants.Large, 10, 10);
            testCases[CreatureConstants.Slaad_Gray][GetData(11, 15, SizeConstants.Medium, 5, 5);
            testCases[CreatureConstants.Slaad_Gray][GetData(16, 30, SizeConstants.Large, 10, 10);
            testCases[CreatureConstants.Slaad_Green][GetData(10, 15, SizeConstants.Large, 10, 10);
            testCases[CreatureConstants.Slaad_Green][GetData(16, 27, SizeConstants.Huge, 15, 15);
            testCases[CreatureConstants.Slaad_Red][GetData(8, 10, SizeConstants.Large, 10, 10);
            testCases[CreatureConstants.Slaad_Red][GetData(11, 21, SizeConstants.Huge, 15, 15);
            testCases[CreatureConstants.Snake_Constrictor][GetData(4, 5, SizeConstants.Medium, 5, 5);
            testCases[CreatureConstants.Snake_Constrictor][GetData(6, 10, SizeConstants.Large, 10, 5);
            testCases[CreatureConstants.Snake_Constrictor_Giant][GetData(12, 16, SizeConstants.Huge, 15, 10);
            testCases[CreatureConstants.Snake_Constrictor_Giant][GetData(17, 33, SizeConstants.Gargantuan, 20, 15);
            testCases[CreatureConstants.Snake_Viper_Huge][GetData(7, 18, SizeConstants.Huge, 15, 10);
            testCases[CreatureConstants.Spectre][GetData(8, 14, SizeConstants.Medium, 5, 5);
            testCases[CreatureConstants.Spider_Monstrous_Hunter_Colossal][GetData(33, 60, SizeConstants.Colossal, 40, 30);
            testCases[CreatureConstants.Spider_Monstrous_Hunter_Gargantuan][GetData(17, 31, SizeConstants.Gargantuan, 20, 15);
            testCases[CreatureConstants.Spider_Monstrous_Hunter_Huge][GetData(9, 15, SizeConstants.Huge, 15, 10);
            testCases[CreatureConstants.Spider_Monstrous_Hunter_Large][GetData(5, 7, SizeConstants.Large, 10, 5);
            testCases[CreatureConstants.Spider_Monstrous_Hunter_Medium][GetData(3, 3, SizeConstants.Medium, 5, 5);
            testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Colossal][GetData(33, 60, SizeConstants.Colossal, 40, 30);
            testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Gargantuan][GetData(17, 31, SizeConstants.Gargantuan, 20, 15);
            testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Huge][GetData(9, 15, SizeConstants.Huge, 15, 10);
            testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Large][GetData(5, 7, SizeConstants.Large, 10, 5);
            testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Medium][GetData(3, 3, SizeConstants.Medium, 5, 5);
            testCases[CreatureConstants.SpiderEater][GetData(5, 12, SizeConstants.Huge, 15, 10);
            testCases[CreatureConstants.Squid][GetData(4, 6, SizeConstants.Medium, 5, 5);
            testCases[CreatureConstants.Squid][GetData(7, 11, SizeConstants.Large, 10, 5);
            testCases[CreatureConstants.Squid_Giant][GetData(13, 18, SizeConstants.Huge, 15, 15);
            testCases[CreatureConstants.Squid_Giant][GetData(19, 36, SizeConstants.Gargantuan, 20, 20);
            testCases[CreatureConstants.StagBeetle_Giant][GetData(8, 10, SizeConstants.Large, 10, 5);
            testCases[CreatureConstants.StagBeetle_Giant][GetData(11, 21, SizeConstants.Huge, 15, 10);
            testCases[CreatureConstants.Succubus][GetData(7, 12, SizeConstants.Medium, 5, 5);
            testCases[CreatureConstants.Tarrasque][GetData(49, 100, SizeConstants.Colossal, 30, 20);
            testCases[CreatureConstants.Tendriculos][GetData(10, 16, SizeConstants.Huge, 15, 15);
            testCases[CreatureConstants.Tendriculos][GetData(17, 27, SizeConstants.Gargantuan, 20, 20);
            testCases[CreatureConstants.Thoqqua][GetData(4, 9, SizeConstants.Large, 10, 5);
            testCases[CreatureConstants.Tiger][GetData(7, 12, SizeConstants.Large, 10, 5);
            testCases[CreatureConstants.Tiger][GetData(13, 18, SizeConstants.Huge, 15, 10);
            testCases[CreatureConstants.Tiger_Dire][GetData(17, 32, SizeConstants.Large, 10, 5);
            testCases[CreatureConstants.Tiger_Dire][GetData(33, 48, SizeConstants.Huge, 15, 10);
            testCases[CreatureConstants.Titan][GetData(21, 30, SizeConstants.Huge, 15, 15);
            testCases[CreatureConstants.Titan][GetData(31, 60, SizeConstants.Gargantuan, 20, 20);
            testCases[CreatureConstants.Tojanida_Adult][GetData(8, 14, SizeConstants.Medium, 5, 5);
            testCases[CreatureConstants.Tojanida_Elder][GetData(16, 24, SizeConstants.Large, 10, 5);
            testCases[CreatureConstants.Tojanida_Juvenile][GetData(4, 6, SizeConstants.Small, 5, 5);
            testCases[CreatureConstants.Treant][GetData(8, 16, SizeConstants.Huge, 15, 15);
            testCases[CreatureConstants.Treant][GetData(17, 21, SizeConstants.Gargantuan, 20, 20);
            testCases[CreatureConstants.Triceratops][GetData(17, 32, SizeConstants.Huge, 15, 10);
            testCases[CreatureConstants.Triceratops][GetData(33, 48, SizeConstants.Gargantuan, 20, 15);
            testCases[CreatureConstants.Triton][GetData(4, 9, SizeConstants.Medium, 5, 5);
            testCases[CreatureConstants.TrumpetArchon][GetData(13, 18, SizeConstants.Medium, 5, 5);
            testCases[CreatureConstants.TrumpetArchon][GetData(19, 36, SizeConstants.Large, 10, 10);
            testCases[CreatureConstants.Tyrannosaurus][GetData(19, 36, SizeConstants.Huge, 15, 10);
            testCases[CreatureConstants.Tyrannosaurus][GetData(37, 54, SizeConstants.Gargantuan, 20, 15);
            testCases[CreatureConstants.Unicorn][GetData(5, 8, SizeConstants.Large, 10, 5);
            testCases[CreatureConstants.UmberHulk][GetData(9, 12, SizeConstants.Large, 10, 10);
            testCases[CreatureConstants.UmberHulk][GetData(13, 24, SizeConstants.Huge, 15, 15);
            testCases[CreatureConstants.Vargouille][GetData(2, 3, SizeConstants.Small, 5, 5);
            testCases[CreatureConstants.VioletFungus][GetData(3, 6, SizeConstants.Medium, 5, 5);
            testCases[CreatureConstants.Vrock][GetData(11, 14, SizeConstants.Large, 10, 10);
            testCases[CreatureConstants.Vrock][GetData(15, 30, SizeConstants.Huge, 15, 15);
            testCases[CreatureConstants.Wasp_Giant][GetData(6, 8, SizeConstants.Large, 10, 5);
            testCases[CreatureConstants.Wasp_Giant][GetData(9, 15, SizeConstants.Huge, 15, 10);
            testCases[CreatureConstants.Weasel_Dire][GetData(4, 6, SizeConstants.Medium, 5, 5);
            testCases[CreatureConstants.Weasel_Dire][GetData(7, 9, SizeConstants.Large, 10, 5);
            testCases[CreatureConstants.Whale_Baleen][GetData(13, 18, SizeConstants.Gargantuan, 20, 15);
            testCases[CreatureConstants.Whale_Baleen][GetData(19, 36, SizeConstants.Colossal, 30, 20);
            testCases[CreatureConstants.Whale_Cachalot][GetData(13, 18, SizeConstants.Gargantuan, 20, 15);
            testCases[CreatureConstants.Whale_Cachalot][GetData(19, 36, SizeConstants.Colossal, 30, 20);
            testCases[CreatureConstants.Whale_Orca][GetData(10, 13, SizeConstants.Huge, 15, 10);
            testCases[CreatureConstants.Whale_Orca][GetData(14, 27, SizeConstants.Gargantuan, 20, 15);
            testCases[CreatureConstants.Wight][GetData(5, 8, SizeConstants.Medium, 5, 5);
            testCases[CreatureConstants.WillOWisp][GetData(10, 18, SizeConstants.Small, 5, 5);
            testCases[CreatureConstants.WinterWolf][GetData(7, 9, SizeConstants.Large, 10, 5);
            testCases[CreatureConstants.WinterWolf][GetData(10, 18, SizeConstants.Huge, 15, 10);
            testCases[CreatureConstants.Wolf][GetData(3, 3, SizeConstants.Medium, 5, 5);
            testCases[CreatureConstants.Wolf][GetData(4, 6, SizeConstants.Large, 10, 5);
            testCases[CreatureConstants.Wolf_Dire][GetData(7, 18, SizeConstants.Large, 10, 5);
            testCases[CreatureConstants.Wolverine][GetData(4, 5, SizeConstants.Large, 10, 5);
            testCases[CreatureConstants.Wolverine_Dire][GetData(6, 15, SizeConstants.Large, 10, 5);
            testCases[CreatureConstants.Worg][GetData(5, 6, SizeConstants.Medium, 5, 5);
            testCases[CreatureConstants.Worg][GetData(7, 12, SizeConstants.Large, 10, 5);
            testCases[CreatureConstants.Wraith][GetData(6, 10, SizeConstants.Medium, 5, 5);
            testCases[CreatureConstants.Wraith_Dread][GetData(17, 32, SizeConstants.Large, 10, 10);
            testCases[CreatureConstants.Wyvern][GetData(8, 10, SizeConstants.Huge, 15, 10);
            testCases[CreatureConstants.Wyvern][GetData(11, 21, SizeConstants.Gargantuan, 20, 15);
            testCases[CreatureConstants.Xill][GetData(6, 8, SizeConstants.Medium, 5, 5);
            testCases[CreatureConstants.Xill][GetData(9, 15, SizeConstants.Large, 10, 10);
            testCases[CreatureConstants.Xorn_Average][GetData(8, 14, SizeConstants.Medium, 5, 5);
            testCases[CreatureConstants.Xorn_Elder][GetData(16, 21, SizeConstants.Large, 10, 10);
            testCases[CreatureConstants.Xorn_Elder][GetData(22, 45, SizeConstants.Huge, 15, 15);
            testCases[CreatureConstants.Xorn_Minor][GetData(4, 6, SizeConstants.Small, 5, 5);
            testCases[CreatureConstants.YethHound][GetData(4, 6, SizeConstants.Medium, 5, 5);
            testCases[CreatureConstants.YethHound][GetData(7, 9, SizeConstants.Large, 10, 5);
            testCases[CreatureConstants.Yrthak][GetData(13, 16, SizeConstants.Huge, 15, 10);
            testCases[CreatureConstants.Yrthak][GetData(17, 36, SizeConstants.Gargantuan, 20, 15);
            testCases[CreatureConstants.Zelekhut][GetData(9, 16, SizeConstants.Large, 10, 10);
            testCases[CreatureConstants.Zelekhut][GetData(17, 24, SizeConstants.Huge, 15, 15);

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
