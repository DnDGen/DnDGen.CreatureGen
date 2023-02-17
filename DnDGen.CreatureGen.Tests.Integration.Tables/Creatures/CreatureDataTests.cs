using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Tables;
using DnDGen.Infrastructure.Selectors.Collections;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Integration.Tables.Creatures
{
    [TestFixture]
    public class CreatureDataTests : DataTests
    {
        private ICollectionSelector collectionSelector;

        protected override string tableName => TableNameConstants.Collection.CreatureData;

        protected override void PopulateIndices(IEnumerable<string> collection)
        {
            indices[DataIndexConstants.CreatureData.CanUseEquipment] = "Can Use Equipment";
            indices[DataIndexConstants.CreatureData.CasterLevel] = "Caster Level";
            indices[DataIndexConstants.CreatureData.ChallengeRating] = "Challenge Rating";
            indices[DataIndexConstants.CreatureData.LevelAdjustment] = "Level Adjustment";
            indices[DataIndexConstants.CreatureData.NaturalArmor] = "Natural Armor";
            indices[DataIndexConstants.CreatureData.NumberOfHands] = "Number Of Hands";
            indices[DataIndexConstants.CreatureData.Reach] = "Reach";
            indices[DataIndexConstants.CreatureData.Size] = "Size";
            indices[DataIndexConstants.CreatureData.Space] = "Space";
        }

        [SetUp]
        public void Setup()
        {
            collectionSelector = GetNewInstanceOf<ICollectionSelector>();
        }

        [Test]
        public void CreatureDataNames()
        {
            var names = CreatureConstants.GetAll();
            AssertCollectionNames(names);
        }

        [TestCase(CreatureConstants.Aasimar, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR1_2nd, 1, true, 1, 0, 2)]
        [TestCase(CreatureConstants.Aboleth, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.CR7, 0, false, 16, 7, 0)]
        [TestCase(CreatureConstants.Achaierai, SizeConstants.Large, 10, 10, ChallengeRatingConstants.CR5, null, false, 16, 10, 0)]
        [TestCase(CreatureConstants.Allip, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR3, null, false, 0, 0, 0)]
        [TestCase(CreatureConstants.Androsphinx, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR9, 5, false, 6, 13, 0)]
        [TestCase(CreatureConstants.Angel_AstralDeva, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR14, 8, true, 12, 15, 2)]
        [TestCase(CreatureConstants.Angel_Planetar, SizeConstants.Large, 10, 10, ChallengeRatingConstants.CR16, null, true, 17, 19, 2)]
        [TestCase(CreatureConstants.Angel_Solar, SizeConstants.Large, 10, 10, ChallengeRatingConstants.CR23, null, true, 20, 21, 2)]
        [TestCase(CreatureConstants.AnimatedObject_Colossal_MultipleLegs_Wooden, SizeConstants.Colossal, 30, 20, ChallengeRatingConstants.CR10, null, false, 0, 12, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Colossal_Sheetlike, SizeConstants.Colossal, 30, 20, ChallengeRatingConstants.CR10, null, false, 0, 12, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Colossal_TwoLegs, SizeConstants.Colossal, 30, 20, ChallengeRatingConstants.CR10, null, false, 0, 12, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Colossal_TwoLegs_Wooden, SizeConstants.Colossal, 30, 20, ChallengeRatingConstants.CR10, null, false, 0, 12, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Colossal_Wheels_Wooden, SizeConstants.Colossal, 30, 20, ChallengeRatingConstants.CR10, null, false, 0, 12, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Colossal_Wooden, SizeConstants.Colossal, 30, 20, ChallengeRatingConstants.CR10, null, false, 0, 12, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Gargantuan, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.CR7, null, false, 0, 8, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Gargantuan_Flexible, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.CR7, null, false, 0, 8, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Gargantuan_MultipleLegs, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.CR7, null, false, 0, 8, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Gargantuan_MultipleLegs_Wooden, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.CR7, null, false, 0, 8, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Gargantuan_Sheetlike, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.CR7, null, false, 0, 8, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Gargantuan_TwoLegs, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.CR7, null, false, 0, 8, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Gargantuan_TwoLegs_Wooden, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.CR7, null, false, 0, 8, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Gargantuan_Wheels_Wooden, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.CR7, null, false, 0, 8, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Gargantuan_Wooden, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.CR7, null, false, 0, 8, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Huge, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.CR5, null, false, 0, 6, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Huge_Flexible, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.CR5, null, false, 0, 6, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Huge_MultipleLegs, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.CR5, null, false, 0, 6, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Huge_MultipleLegs_Wooden, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.CR5, null, false, 0, 6, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Huge_Sheetlike, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.CR5, null, false, 0, 6, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Huge_TwoLegs, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.CR5, null, false, 0, 6, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Huge_TwoLegs_Wooden, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.CR5, null, false, 0, 6, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Huge_Wheels_Wooden, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.CR5, null, false, 0, 6, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Huge_Wooden, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.CR5, null, false, 0, 6, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Large, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR3, null, false, 0, 5, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Large_Flexible, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR3, null, false, 0, 5, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Large_MultipleLegs, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR3, null, false, 0, 5, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Large_MultipleLegs_Wooden, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR3, null, false, 0, 5, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Large_Sheetlike, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR3, null, false, 0, 5, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Large_TwoLegs, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR3, null, false, 0, 5, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Large_TwoLegs_Wooden, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR3, null, false, 0, 5, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Large_Wheels_Wooden, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR3, null, false, 0, 5, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Large_Wooden, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR3, null, false, 0, 5, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Medium, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR2, null, false, 0, 4, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Medium_Flexible, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR2, null, false, 0, 4, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Medium_MultipleLegs, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR2, null, false, 0, 4, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Medium_MultipleLegs_Wooden, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR2, null, false, 0, 4, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Medium_Sheetlike, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR2, null, false, 0, 4, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Medium_TwoLegs, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR2, null, false, 0, 4, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Medium_TwoLegs_Wooden, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR2, null, false, 0, 4, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Medium_Wheels_Wooden, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR2, null, false, 0, 4, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Medium_Wooden, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR2, null, false, 0, 4, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Small, SizeConstants.Small, 5, 5, ChallengeRatingConstants.CR1, null, false, 0, 2, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Small_Flexible, SizeConstants.Small, 5, 5, ChallengeRatingConstants.CR1, null, false, 0, 2, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Small_MultipleLegs, SizeConstants.Small, 5, 5, ChallengeRatingConstants.CR1, null, false, 0, 2, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Small_MultipleLegs_Wooden, SizeConstants.Small, 5, 5, ChallengeRatingConstants.CR1, null, false, 0, 2, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Small_Sheetlike, SizeConstants.Small, 5, 5, ChallengeRatingConstants.CR1, null, false, 0, 2, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Small_TwoLegs, SizeConstants.Small, 5, 5, ChallengeRatingConstants.CR1, null, false, 0, 2, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Small_TwoLegs_Wooden, SizeConstants.Small, 5, 5, ChallengeRatingConstants.CR1, null, false, 0, 2, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Small_Wheels_Wooden, SizeConstants.Small, 5, 5, ChallengeRatingConstants.CR1, null, false, 0, 2, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Small_Wooden, SizeConstants.Small, 5, 5, ChallengeRatingConstants.CR1, null, false, 0, 2, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Tiny, SizeConstants.Tiny, 2.5, 0, ChallengeRatingConstants.CR1_2nd, null, false, 0, 0, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Tiny_Flexible, SizeConstants.Tiny, 2.5, 0, ChallengeRatingConstants.CR1_2nd, null, false, 0, 0, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Tiny_MultipleLegs, SizeConstants.Tiny, 2.5, 0, ChallengeRatingConstants.CR1_2nd, null, false, 0, 0, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Tiny_MultipleLegs_Wooden, SizeConstants.Tiny, 2.5, 0, ChallengeRatingConstants.CR1_2nd, null, false, 0, 0, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Tiny_Sheetlike, SizeConstants.Tiny, 2.5, 0, ChallengeRatingConstants.CR1_2nd, null, false, 0, 0, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Tiny_TwoLegs, SizeConstants.Tiny, 2.5, 0, ChallengeRatingConstants.CR1_2nd, null, false, 0, 0, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Tiny_TwoLegs_Wooden, SizeConstants.Tiny, 2.5, 0, ChallengeRatingConstants.CR1_2nd, null, false, 0, 0, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Tiny_Wheels_Wooden, SizeConstants.Tiny, 2.5, 0, ChallengeRatingConstants.CR1_2nd, null, false, 0, 0, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Tiny_Wooden, SizeConstants.Tiny, 2.5, 0, ChallengeRatingConstants.CR1_2nd, null, false, 0, 0, 0)]
        [TestCase(CreatureConstants.Ankheg, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR3, null, false, 0, 9, 0)]
        [TestCase(CreatureConstants.Annis, SizeConstants.Large, 10, 10, ChallengeRatingConstants.CR6, 0, true, 8, 10, 2)]
        [TestCase(CreatureConstants.Ant_Giant_Queen, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR2, null, false, 0, 9, 0)]
        [TestCase(CreatureConstants.Ant_Giant_Soldier, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR2, null, false, 0, 7, 0)]
        [TestCase(CreatureConstants.Ant_Giant_Worker, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR1, null, false, 0, 7, 0)]
        [TestCase(CreatureConstants.Ape, SizeConstants.Large, 10, 10, ChallengeRatingConstants.CR2, null, false, 0, 3, 0)]
        [TestCase(CreatureConstants.Ape_Dire, SizeConstants.Large, 10, 10, ChallengeRatingConstants.CR3, null, false, 0, 4, 0)]
        [TestCase(CreatureConstants.Aranea, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR4, 4, true, 3, 1, 0)]
        [TestCase(CreatureConstants.Arrowhawk_Adult, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR5, null, false, 0, 6, 0)]
        [TestCase(CreatureConstants.Arrowhawk_Elder, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR8, null, false, 0, 8, 0)]
        [TestCase(CreatureConstants.Arrowhawk_Juvenile, SizeConstants.Small, 5, 5, ChallengeRatingConstants.CR3, null, false, 0, 4, 0)]
        [TestCase(CreatureConstants.AssassinVine, SizeConstants.Large, 10, 10, ChallengeRatingConstants.CR3, null, false, 4, 6, 0)]
        [TestCase(CreatureConstants.Athach, SizeConstants.Huge, 15, 15, ChallengeRatingConstants.CR8, 5, true, 0, 8, 3)]
        [TestCase(CreatureConstants.Avoral, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR9, null, true, 8, 8, 2)]
        [TestCase(CreatureConstants.Azer, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR2, 4, true, 0, 6, 2)]
        [TestCase(CreatureConstants.Babau, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR6, null, true, 7, 8, 0)]
        [TestCase(CreatureConstants.Baboon, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR1_2nd, null, false, 0, 1, 0)]
        [TestCase(CreatureConstants.Badger, SizeConstants.Small, 5, 5, ChallengeRatingConstants.CR1_2nd, null, false, 0, 1, 0)]
        [TestCase(CreatureConstants.Badger_Dire, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR2, null, false, 0, 3, 0)]
        [TestCase(CreatureConstants.Balor, SizeConstants.Large, 10, 10, ChallengeRatingConstants.CR20, null, true, 20, 19, 2)]
        [TestCase(CreatureConstants.BarbedDevil_Hamatula, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR11, null, true, 12, 13, 2)]
        [TestCase(CreatureConstants.Barghest, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR4, null, true, 0, 6, 2)]
        [TestCase(CreatureConstants.Barghest_Greater, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR5, null, true, 0, 9, 2)]
        [TestCase(CreatureConstants.Basilisk, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR5, null, false, 0, 7, 0)]
        [TestCase(CreatureConstants.Basilisk_Greater, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR12, null, false, 0, 9, 0)]
        [TestCase(CreatureConstants.Bat, SizeConstants.Diminutive, 1, 0, ChallengeRatingConstants.CR1_10th, null, false, 0, 0, 0)]
        [TestCase(CreatureConstants.Bat_Dire, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR2, null, false, 0, 5, 0)]
        [TestCase(CreatureConstants.Bat_Swarm, SizeConstants.Diminutive, 10, 0, ChallengeRatingConstants.CR2, null, false, 0, 0, 0)]
        [TestCase(CreatureConstants.Bear_Black, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR2, null, false, 0, 2, 0)]
        [TestCase(CreatureConstants.Bear_Brown, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR4, null, false, 0, 5, 0)]
        [TestCase(CreatureConstants.Bear_Dire, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR7, null, false, 0, 7, 0)]
        [TestCase(CreatureConstants.Bear_Polar, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR4, null, false, 0, 5, 0)]
        [TestCase(CreatureConstants.BeardedDevil_Barbazu, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR5, 6, true, 12, 7, 2)]
        [TestCase(CreatureConstants.Bebilith, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.CR10, null, false, 12, 13, 0)]
        [TestCase(CreatureConstants.Bee_Giant, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR1, null, false, 0, 2, 0)]
        [TestCase(CreatureConstants.Behir, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.CR8, null, false, 0, 11, 0)]
        [TestCase(CreatureConstants.Beholder, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR13, null, false, 13, 15, 0)]
        [TestCase(CreatureConstants.Beholder_Gauth, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR6, null, false, 8, 7, 0)]
        [TestCase(CreatureConstants.Belker, SizeConstants.Large, 10, 10, ChallengeRatingConstants.CR6, null, false, 0, 8, 0)]
        [TestCase(CreatureConstants.Bison, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR2, null, false, 0, 4, 0)]
        [TestCase(CreatureConstants.BlackPudding, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.CR7, null, false, 0, 0, 0)]
        [TestCase(CreatureConstants.BlackPudding_Elder, SizeConstants.Gargantuan, 20, 20, ChallengeRatingConstants.CR12, null, false, 0, 0, 0)]
        [TestCase(CreatureConstants.BlinkDog, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR2, 2, false, 8, 3, 0)]
        [TestCase(CreatureConstants.Boar, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR2, null, false, 0, 6, 0)]
        [TestCase(CreatureConstants.Boar_Dire, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR4, null, false, 0, 6, 0)]
        [TestCase(CreatureConstants.Bodak, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR8, null, false, 0, 8, 2)]
        [TestCase(CreatureConstants.BombardierBeetle_Giant, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR2, null, false, 0, 6, 0)]
        [TestCase(CreatureConstants.BoneDevil_Osyluth, SizeConstants.Large, 10, 10, ChallengeRatingConstants.CR9, null, true, 12, 11, 2)]
        [TestCase(CreatureConstants.Bralani, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR6, 5, true, 6, 6, 2)]
        [TestCase(CreatureConstants.Bugbear, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR2, 1, true, 0, 3, 2)]
        [TestCase(CreatureConstants.Bulette, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.CR7, null, false, 0, 12, 0)]
        [TestCase(CreatureConstants.Camel_Bactrian, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR1, null, false, 0, 1, 0)]
        [TestCase(CreatureConstants.Camel_Dromedary, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR1, null, false, 0, 1, 0)]
        [TestCase(CreatureConstants.CarrionCrawler, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR4, null, false, 0, 6, 0)]
        [TestCase(CreatureConstants.Cat, SizeConstants.Tiny, 2.5, 0, ChallengeRatingConstants.CR1_4th, null, false, 0, 0, 0)]
        [TestCase(CreatureConstants.Centaur, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR3, 2, true, 0, 3, 2)]
        [TestCase(CreatureConstants.Centipede_Monstrous_Colossal, SizeConstants.Colossal, 30, 20, ChallengeRatingConstants.CR9, null, false, 0, 16, 0)]
        [TestCase(CreatureConstants.Centipede_Monstrous_Gargantuan, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.CR6, null, false, 0, 10, 0)]
        [TestCase(CreatureConstants.Centipede_Monstrous_Huge, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.CR2, null, false, 0, 6, 0)]
        [TestCase(CreatureConstants.Centipede_Monstrous_Large, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR1, null, false, 0, 3, 0)]
        [TestCase(CreatureConstants.Centipede_Monstrous_Medium, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR1_2nd, null, false, 0, 2, 0)]
        [TestCase(CreatureConstants.Centipede_Monstrous_Small, SizeConstants.Small, 5, 5, ChallengeRatingConstants.CR1_4th, null, false, 0, 1, 0)]
        [TestCase(CreatureConstants.Centipede_Monstrous_Tiny, SizeConstants.Tiny, 2.5, 0, ChallengeRatingConstants.CR1_8th, null, false, 0, 0, 0)]
        [TestCase(CreatureConstants.Centipede_Swarm, SizeConstants.Diminutive, 10, 0, ChallengeRatingConstants.CR4, null, false, 0, 0, 0)]
        [TestCase(CreatureConstants.ChainDevil_Kyton, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR6, 6, true, 0, 8, 2)]
        [TestCase(CreatureConstants.ChaosBeast, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR7, null, false, 0, 5, 0)]
        [TestCase(CreatureConstants.Cheetah, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR2, null, false, 0, 1, 0)]
        [TestCase(CreatureConstants.Chimera_Black, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR7, 2, false, 0, 9, 0)]
        [TestCase(CreatureConstants.Chimera_Blue, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR7, 2, false, 0, 9, 0)]
        [TestCase(CreatureConstants.Chimera_Green, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR7, 2, false, 0, 9, 0)]
        [TestCase(CreatureConstants.Chimera_Red, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR7, 2, false, 0, 9, 0)]
        [TestCase(CreatureConstants.Chimera_White, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR7, 2, false, 0, 9, 0)]
        [TestCase(CreatureConstants.Choker, SizeConstants.Small, 5, 10, ChallengeRatingConstants.CR2, null, false, 0, 4, 0)]
        [TestCase(CreatureConstants.Chuul, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR7, null, false, 0, 10, 0)]
        [TestCase(CreatureConstants.Cloaker, SizeConstants.Large, 10, 10, ChallengeRatingConstants.CR5, null, false, 0, 7, 0)]
        [TestCase(CreatureConstants.Cockatrice, SizeConstants.Small, 5, 5, ChallengeRatingConstants.CR3, null, false, 0, 0, 0)]
        [TestCase(CreatureConstants.Couatl, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR10, 7, true, 9, 9, 0)]
        [TestCase(CreatureConstants.Criosphinx, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR7, 3, false, 0, 11, 0)]
        [TestCase(CreatureConstants.Crocodile, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR2, null, false, 0, 4, 0)]
        [TestCase(CreatureConstants.Crocodile_Giant, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.CR4, null, false, 0, 7, 0)]
        [TestCase(CreatureConstants.Cryohydra_5Heads, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.CR6, null, false, 0, 6, 0)]
        [TestCase(CreatureConstants.Cryohydra_6Heads, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.CR7, null, false, 0, 7, 0)]
        [TestCase(CreatureConstants.Cryohydra_7Heads, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.CR8, null, false, 0, 8, 0)]
        [TestCase(CreatureConstants.Cryohydra_8Heads, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.CR9, null, false, 0, 9, 0)]
        [TestCase(CreatureConstants.Cryohydra_9Heads, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.CR10, null, false, 0, 10, 0)]
        [TestCase(CreatureConstants.Cryohydra_10Heads, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.CR11, null, false, 0, 11, 0)]
        [TestCase(CreatureConstants.Cryohydra_11Heads, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.CR12, null, false, 0, 12, 0)]
        [TestCase(CreatureConstants.Cryohydra_12Heads, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.CR13, null, false, 0, 13, 0)]
        [TestCase(CreatureConstants.Darkmantle, SizeConstants.Small, 5, 5, ChallengeRatingConstants.CR1, null, false, 5, 6, 0)]
        [TestCase(CreatureConstants.Deinonychus, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR3, null, false, 0, 5, 0)]
        [TestCase(CreatureConstants.Delver, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.CR9, null, false, 15, 15, 0)]
        [TestCase(CreatureConstants.Derro, SizeConstants.Small, 5, 5, ChallengeRatingConstants.CR3, 0, true, 3, 2, 2)]
        [TestCase(CreatureConstants.Derro_Sane, SizeConstants.Small, 5, 5, ChallengeRatingConstants.CR3, 2, true, 3, 2, 2)]
        [TestCase(CreatureConstants.Destrachan, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR8, null, false, 0, 8, 0)]
        [TestCase(CreatureConstants.Devourer, SizeConstants.Large, 10, 10, ChallengeRatingConstants.CR11, null, false, 18, 15, 2)]
        [TestCase(CreatureConstants.Digester, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR6, null, false, 0, 5, 0)]
        [TestCase(CreatureConstants.DisplacerBeast, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR4, 4, false, 0, 5, 0)]
        [TestCase(CreatureConstants.DisplacerBeast_PackLord, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.CR12, null, false, 0, 8, 0)]
        [TestCase(CreatureConstants.Djinni, SizeConstants.Large, 10, 10, ChallengeRatingConstants.CR5, 6, true, 20, 3, 2)]
        [TestCase(CreatureConstants.Djinni_Noble, SizeConstants.Large, 10, 10, ChallengeRatingConstants.CR8, null, true, 20, 3, 2)]
        [TestCase(CreatureConstants.Dog, SizeConstants.Small, 5, 5, ChallengeRatingConstants.CR1_3rd, null, false, 0, 1, 0)]
        [TestCase(CreatureConstants.Dog_Riding, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR1, null, false, 0, 4, 0)]
        [TestCase(CreatureConstants.Donkey, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR1_6th, null, false, 0, 2, 0)]
        [TestCase(CreatureConstants.Doppelganger, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR3, 4, true, 18, 4, 2)]
        [TestCase(CreatureConstants.Dragon_Black_Wyrmling, SizeConstants.Tiny, 2.5, 0, ChallengeRatingConstants.CR3, 3, false, 0, 3, 0)]
        [TestCase(CreatureConstants.Dragon_Black_VeryYoung, SizeConstants.Small, 5, 5, ChallengeRatingConstants.CR4, 3, false, 0, 6, 0)]
        [TestCase(CreatureConstants.Dragon_Black_Young, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR5, 3, false, 0, 9, 0)]
        [TestCase(CreatureConstants.Dragon_Black_Juvenile, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR7, 4, false, 0, 12, 0)]
        [TestCase(CreatureConstants.Dragon_Black_YoungAdult, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR9, null, false, 1, 15, 0)]
        [TestCase(CreatureConstants.Dragon_Black_Adult, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR11, null, false, 3, 18, 0)]
        [TestCase(CreatureConstants.Dragon_Black_MatureAdult, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.CR14, null, false, 5, 21, 0)]
        [TestCase(CreatureConstants.Dragon_Black_Old, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.CR16, null, false, 7, 24, 0)]
        [TestCase(CreatureConstants.Dragon_Black_VeryOld, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.CR18, null, false, 9, 27, 0)]
        [TestCase(CreatureConstants.Dragon_Black_Ancient, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.CR19, null, false, 11, 30, 0)]
        [TestCase(CreatureConstants.Dragon_Black_Wyrm, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.CR20, null, false, 13, 33, 0)]
        [TestCase(CreatureConstants.Dragon_Black_GreatWyrm, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.CR22, null, false, 15, 36, 0)]
        [TestCase(CreatureConstants.Dragon_Blue_Wyrmling, SizeConstants.Small, 5, 5, ChallengeRatingConstants.CR3, 4, false, 0, 5, 0)]
        [TestCase(CreatureConstants.Dragon_Blue_VeryYoung, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR4, 4, false, 0, 8, 0)]
        [TestCase(CreatureConstants.Dragon_Blue_Young, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR6, 5, false, 0, 11, 0)]
        [TestCase(CreatureConstants.Dragon_Blue_Juvenile, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR8, null, false, 1, 14, 0)]
        [TestCase(CreatureConstants.Dragon_Blue_YoungAdult, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR11, null, false, 3, 17, 0)]
        [TestCase(CreatureConstants.Dragon_Blue_Adult, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.CR14, null, false, 5, 20, 0)]
        [TestCase(CreatureConstants.Dragon_Blue_MatureAdult, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.CR16, null, false, 7, 23, 0)]
        [TestCase(CreatureConstants.Dragon_Blue_Old, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.CR18, null, false, 9, 26, 0)]
        [TestCase(CreatureConstants.Dragon_Blue_VeryOld, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.CR19, null, false, 11, 29, 0)]
        [TestCase(CreatureConstants.Dragon_Blue_Ancient, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.CR21, null, false, 13, 32, 0)]
        [TestCase(CreatureConstants.Dragon_Blue_Wyrm, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.CR23, null, false, 15, 35, 0)]
        [TestCase(CreatureConstants.Dragon_Blue_GreatWyrm, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.CR25, null, false, 17, 38, 0)]
        [TestCase(CreatureConstants.Dragon_Brass_Wyrmling, SizeConstants.Tiny, 2.5, 0, ChallengeRatingConstants.CR3, 2, false, 0, 3, 0)]
        [TestCase(CreatureConstants.Dragon_Brass_VeryYoung, SizeConstants.Small, 5, 5, ChallengeRatingConstants.CR4, 3, false, 0, 6, 0)]
        [TestCase(CreatureConstants.Dragon_Brass_Young, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR6, 4, false, 1, 9, 0)]
        [TestCase(CreatureConstants.Dragon_Brass_Juvenile, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR8, 4, false, 3, 12, 0)]
        [TestCase(CreatureConstants.Dragon_Brass_YoungAdult, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR10, null, false, 5, 15, 0)]
        [TestCase(CreatureConstants.Dragon_Brass_Adult, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR12, null, false, 7, 18, 0)]
        [TestCase(CreatureConstants.Dragon_Brass_MatureAdult, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.CR15, null, false, 9, 21, 0)]
        [TestCase(CreatureConstants.Dragon_Brass_Old, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.CR17, null, false, 11, 24, 0)]
        [TestCase(CreatureConstants.Dragon_Brass_VeryOld, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.CR19, null, false, 13, 27, 0)]
        [TestCase(CreatureConstants.Dragon_Brass_Ancient, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.CR20, null, false, 15, 30, 0)]
        [TestCase(CreatureConstants.Dragon_Brass_Wyrm, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.CR21, null, false, 17, 33, 0)]
        [TestCase(CreatureConstants.Dragon_Brass_GreatWyrm, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.CR23, null, false, 19, 36, 0)]
        [TestCase(CreatureConstants.Dragon_Bronze_Wyrmling, SizeConstants.Small, 5, 5, ChallengeRatingConstants.CR3, 4, false, 0, 5, 0)]
        [TestCase(CreatureConstants.Dragon_Bronze_VeryYoung, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR5, 4, false, 0, 8, 0)]
        [TestCase(CreatureConstants.Dragon_Bronze_Young, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR7, 6, true, 1, 11, 0)]
        [TestCase(CreatureConstants.Dragon_Bronze_Juvenile, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR9, null, true, 3, 14, 0)]
        [TestCase(CreatureConstants.Dragon_Bronze_YoungAdult, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR12, null, true, 5, 17, 0)]
        [TestCase(CreatureConstants.Dragon_Bronze_Adult, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.CR15, null, true, 7, 20, 0)]
        [TestCase(CreatureConstants.Dragon_Bronze_MatureAdult, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.CR17, null, true, 9, 23, 0)]
        [TestCase(CreatureConstants.Dragon_Bronze_Old, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.CR19, null, true, 11, 26, 0)]
        [TestCase(CreatureConstants.Dragon_Bronze_VeryOld, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.CR20, null, true, 13, 29, 0)]
        [TestCase(CreatureConstants.Dragon_Bronze_Ancient, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.CR22, null, true, 15, 32, 0)]
        [TestCase(CreatureConstants.Dragon_Bronze_Wyrm, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.CR23, null, true, 17, 35, 0)]
        [TestCase(CreatureConstants.Dragon_Bronze_GreatWyrm, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.CR25, null, true, 19, 38, 0)]
        [TestCase(CreatureConstants.Dragon_Copper_Wyrmling, SizeConstants.Tiny, 2.5, 0, ChallengeRatingConstants.CR3, 2, false, 0, 4, 0)]
        [TestCase(CreatureConstants.Dragon_Copper_VeryYoung, SizeConstants.Small, 5, 5, ChallengeRatingConstants.CR5, 3, false, 0, 7, 0)]
        [TestCase(CreatureConstants.Dragon_Copper_Young, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR7, 4, false, 1, 10, 0)]
        [TestCase(CreatureConstants.Dragon_Copper_Juvenile, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR9, 4, false, 3, 13, 0)]
        [TestCase(CreatureConstants.Dragon_Copper_YoungAdult, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR11, null, false, 5, 16, 0)]
        [TestCase(CreatureConstants.Dragon_Copper_Adult, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR14, null, false, 7, 19, 0)]
        [TestCase(CreatureConstants.Dragon_Copper_MatureAdult, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.CR16, null, false, 9, 22, 0)]
        [TestCase(CreatureConstants.Dragon_Copper_Old, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.CR19, null, false, 11, 25, 0)]
        [TestCase(CreatureConstants.Dragon_Copper_VeryOld, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.CR20, null, false, 13, 28, 0)]
        [TestCase(CreatureConstants.Dragon_Copper_Ancient, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.CR22, null, false, 15, 31, 0)]
        [TestCase(CreatureConstants.Dragon_Copper_Wyrm, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.CR23, null, false, 17, 34, 0)]
        [TestCase(CreatureConstants.Dragon_Copper_GreatWyrm, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.CR25, null, false, 19, 37, 0)]
        [TestCase(CreatureConstants.Dragon_Gold_Wyrmling, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR5, 4, true, 0, 7, 0)]
        [TestCase(CreatureConstants.Dragon_Gold_VeryYoung, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR7, 5, true, 0, 10, 0)]
        [TestCase(CreatureConstants.Dragon_Gold_Young, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR9, 6, true, 1, 13, 0)]
        [TestCase(CreatureConstants.Dragon_Gold_Juvenile, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR11, null, true, 3, 16, 0)]
        [TestCase(CreatureConstants.Dragon_Gold_YoungAdult, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.CR14, null, true, 5, 19, 0)]
        [TestCase(CreatureConstants.Dragon_Gold_Adult, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.CR16, null, true, 7, 22, 0)]
        [TestCase(CreatureConstants.Dragon_Gold_MatureAdult, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.CR19, null, true, 9, 25, 0)]
        [TestCase(CreatureConstants.Dragon_Gold_Old, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.CR21, null, true, 11, 28, 0)]
        [TestCase(CreatureConstants.Dragon_Gold_VeryOld, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.CR22, null, true, 13, 31, 0)]
        [TestCase(CreatureConstants.Dragon_Gold_Ancient, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.CR24, null, true, 15, 34, 0)]
        [TestCase(CreatureConstants.Dragon_Gold_Wyrm, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.CR25, null, true, 17, 37, 0)]
        [TestCase(CreatureConstants.Dragon_Gold_GreatWyrm, SizeConstants.Colossal, 30, 20, ChallengeRatingConstants.CR27, null, true, 19, 40, 0)]
        [TestCase(CreatureConstants.Dragon_Green_Wyrmling, SizeConstants.Small, 5, 5, ChallengeRatingConstants.CR3, 5, false, 0, 4, 0)]
        [TestCase(CreatureConstants.Dragon_Green_VeryYoung, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR4, 5, false, 0, 7, 0)]
        [TestCase(CreatureConstants.Dragon_Green_Young, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR5, 5, false, 0, 10, 0)]
        [TestCase(CreatureConstants.Dragon_Green_Juvenile, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR8, 6, false, 1, 13, 0)]
        [TestCase(CreatureConstants.Dragon_Green_YoungAdult, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR11, null, false, 3, 16, 0)]
        [TestCase(CreatureConstants.Dragon_Green_Adult, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.CR13, null, false, 5, 19, 0)]
        [TestCase(CreatureConstants.Dragon_Green_MatureAdult, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.CR16, null, false, 7, 22, 0)]
        [TestCase(CreatureConstants.Dragon_Green_Old, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.CR18, null, false, 9, 25, 0)]
        [TestCase(CreatureConstants.Dragon_Green_VeryOld, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.CR19, null, false, 11, 28, 0)]
        [TestCase(CreatureConstants.Dragon_Green_Ancient, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.CR21, null, false, 13, 31, 0)]
        [TestCase(CreatureConstants.Dragon_Green_Wyrm, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.CR22, null, false, 15, 34, 0)]
        [TestCase(CreatureConstants.Dragon_Green_GreatWyrm, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.CR24, null, false, 17, 37, 0)]
        [TestCase(CreatureConstants.Dragon_Red_Wyrmling, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR4, 4, false, 0, 6, 0)]
        [TestCase(CreatureConstants.Dragon_Red_VeryYoung, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR5, 5, false, 0, 9, 0)]
        [TestCase(CreatureConstants.Dragon_Red_Young, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR7, 6, false, 1, 12, 0)]
        [TestCase(CreatureConstants.Dragon_Red_Juvenile, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR10, null, false, 3, 15, 0)]
        [TestCase(CreatureConstants.Dragon_Red_YoungAdult, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.CR13, null, false, 5, 18, 0)]
        [TestCase(CreatureConstants.Dragon_Red_Adult, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.CR15, null, false, 7, 21, 0)]
        [TestCase(CreatureConstants.Dragon_Red_MatureAdult, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.CR18, null, false, 9, 24, 0)]
        [TestCase(CreatureConstants.Dragon_Red_Old, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.CR20, null, false, 11, 27, 0)]
        [TestCase(CreatureConstants.Dragon_Red_VeryOld, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.CR21, null, false, 13, 30, 0)]
        [TestCase(CreatureConstants.Dragon_Red_Ancient, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.CR23, null, false, 15, 33, 0)]
        [TestCase(CreatureConstants.Dragon_Red_Wyrm, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.CR24, null, false, 17, 36, 0)]
        [TestCase(CreatureConstants.Dragon_Red_GreatWyrm, SizeConstants.Colossal, 30, 20, ChallengeRatingConstants.CR26, null, false, 19, 39, 0)]
        [TestCase(CreatureConstants.Dragon_Silver_Wyrmling, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR4, 4, true, 0, 6, 0)]
        [TestCase(CreatureConstants.Dragon_Silver_VeryYoung, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR5, 4, true, 0, 9, 0)]
        [TestCase(CreatureConstants.Dragon_Silver_Young, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR7, 5, true, 1, 12, 0)]
        [TestCase(CreatureConstants.Dragon_Silver_Juvenile, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR10, null, true, 3, 15, 0)]
        [TestCase(CreatureConstants.Dragon_Silver_YoungAdult, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.CR13, null, true, 5, 18, 0)]
        [TestCase(CreatureConstants.Dragon_Silver_Adult, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.CR15, null, true, 7, 21, 0)]
        [TestCase(CreatureConstants.Dragon_Silver_MatureAdult, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.CR18, null, true, 9, 24, 0)]
        [TestCase(CreatureConstants.Dragon_Silver_Old, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.CR20, null, true, 11, 27, 0)]
        [TestCase(CreatureConstants.Dragon_Silver_VeryOld, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.CR21, null, true, 13, 30, 0)]
        [TestCase(CreatureConstants.Dragon_Silver_Ancient, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.CR23, null, true, 15, 33, 0)]
        [TestCase(CreatureConstants.Dragon_Silver_Wyrm, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.CR24, null, true, 17, 36, 0)]
        [TestCase(CreatureConstants.Dragon_Silver_GreatWyrm, SizeConstants.Colossal, 30, 20, ChallengeRatingConstants.CR26, null, true, 19, 39, 0)]
        [TestCase(CreatureConstants.Dragon_White_Wyrmling, SizeConstants.Tiny, 2.5, 0, ChallengeRatingConstants.CR2, 2, false, 0, 2, 0)]
        [TestCase(CreatureConstants.Dragon_White_VeryYoung, SizeConstants.Small, 5, 5, ChallengeRatingConstants.CR3, 3, false, 0, 5, 0)]
        [TestCase(CreatureConstants.Dragon_White_Young, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR4, 3, false, 0, 8, 0)]
        [TestCase(CreatureConstants.Dragon_White_Juvenile, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR6, 5, false, 0, 11, 0)]
        [TestCase(CreatureConstants.Dragon_White_YoungAdult, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR8, null, false, 0, 14, 0)]
        [TestCase(CreatureConstants.Dragon_White_Adult, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR10, null, false, 1, 17, 0)]
        [TestCase(CreatureConstants.Dragon_White_MatureAdult, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.CR12, null, false, 3, 20, 0)]
        [TestCase(CreatureConstants.Dragon_White_Old, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.CR15, null, false, 5, 23, 0)]
        [TestCase(CreatureConstants.Dragon_White_VeryOld, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.CR17, null, false, 7, 26, 0)]
        [TestCase(CreatureConstants.Dragon_White_Ancient, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.CR18, null, false, 9, 29, 0)]
        [TestCase(CreatureConstants.Dragon_White_Wyrm, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.CR19, null, false, 11, 32, 0)]
        [TestCase(CreatureConstants.Dragon_White_GreatWyrm, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.CR21, null, false, 13, 35, 0)]
        [TestCase(CreatureConstants.DragonTurtle, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.CR9, null, false, 0, 17, 0)]
        [TestCase(CreatureConstants.Dragonne, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR7, 4, false, 0, 7, 0)]
        [TestCase(CreatureConstants.Dretch, SizeConstants.Small, 5, 5, ChallengeRatingConstants.CR2, 2, true, 2, 5, 2)]
        [TestCase(CreatureConstants.Drider, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR7, 4, true, 6, 6, 2)]
        [TestCase(CreatureConstants.Dryad, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR3, 0, true, 6, 3, 2)]
        [TestCase(CreatureConstants.Dwarf_Deep, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR1_2nd, 0, true, 0, 0, 2)]
        [TestCase(CreatureConstants.Dwarf_Duergar, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR1, 1, true, 0, 0, 2)]
        [TestCase(CreatureConstants.Dwarf_Hill, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR1_2nd, 0, true, 0, 0, 2)]
        [TestCase(CreatureConstants.Dwarf_Mountain, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR1_2nd, 0, true, 0, 0, 2)]
        [TestCase(CreatureConstants.Eagle, SizeConstants.Small, 5, 5, ChallengeRatingConstants.CR1_2nd, null, false, 0, 1, 0)]
        [TestCase(CreatureConstants.Eagle_Giant, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR3, 2, false, 0, 3, 0)]
        [TestCase(CreatureConstants.Efreeti, SizeConstants.Large, 10, 10, ChallengeRatingConstants.CR8, null, true, 12, 6, 2)]
        [TestCase(CreatureConstants.Elasmosaurus, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.CR7, null, false, 0, 3, 0)]
        [TestCase(CreatureConstants.Elemental_Air_Elder, SizeConstants.Huge, 15, 15, ChallengeRatingConstants.CR11, null, false, 0, 8, 0)]
        [TestCase(CreatureConstants.Elemental_Air_Greater, SizeConstants.Huge, 15, 15, ChallengeRatingConstants.CR9, null, false, 0, 8, 0)]
        [TestCase(CreatureConstants.Elemental_Air_Huge, SizeConstants.Huge, 15, 15, ChallengeRatingConstants.CR7, null, false, 0, 4, 0)]
        [TestCase(CreatureConstants.Elemental_Air_Large, SizeConstants.Large, 10, 10, ChallengeRatingConstants.CR5, null, false, 0, 4, 0)]
        [TestCase(CreatureConstants.Elemental_Air_Medium, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR3, null, false, 0, 3, 0)]
        [TestCase(CreatureConstants.Elemental_Air_Small, SizeConstants.Small, 5, 5, ChallengeRatingConstants.CR1, null, false, 0, 3, 0)]
        [TestCase(CreatureConstants.Elemental_Earth_Elder, SizeConstants.Huge, 15, 15, ChallengeRatingConstants.CR11, null, false, 0, 15, 0)]
        [TestCase(CreatureConstants.Elemental_Earth_Greater, SizeConstants.Huge, 15, 15, ChallengeRatingConstants.CR9, null, false, 0, 13, 0)]
        [TestCase(CreatureConstants.Elemental_Earth_Huge, SizeConstants.Huge, 15, 15, ChallengeRatingConstants.CR7, null, false, 0, 11, 0)]
        [TestCase(CreatureConstants.Elemental_Earth_Large, SizeConstants.Large, 10, 10, ChallengeRatingConstants.CR5, null, false, 0, 10, 0)]
        [TestCase(CreatureConstants.Elemental_Earth_Medium, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR3, null, false, 0, 9, 0)]
        [TestCase(CreatureConstants.Elemental_Earth_Small, SizeConstants.Small, 5, 5, ChallengeRatingConstants.CR1, null, false, 0, 7, 0)]
        [TestCase(CreatureConstants.Elemental_Fire_Elder, SizeConstants.Huge, 15, 15, ChallengeRatingConstants.CR11, null, false, 0, 8, 0)]
        [TestCase(CreatureConstants.Elemental_Fire_Greater, SizeConstants.Huge, 15, 15, ChallengeRatingConstants.CR9, null, false, 0, 8, 0)]
        [TestCase(CreatureConstants.Elemental_Fire_Huge, SizeConstants.Huge, 15, 15, ChallengeRatingConstants.CR7, null, false, 0, 4, 0)]
        [TestCase(CreatureConstants.Elemental_Fire_Large, SizeConstants.Large, 10, 10, ChallengeRatingConstants.CR5, null, false, 0, 4, 0)]
        [TestCase(CreatureConstants.Elemental_Fire_Medium, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR3, null, false, 0, 3, 0)]
        [TestCase(CreatureConstants.Elemental_Fire_Small, SizeConstants.Small, 5, 5, ChallengeRatingConstants.CR1, null, false, 0, 3, 0)]
        [TestCase(CreatureConstants.Elemental_Water_Elder, SizeConstants.Huge, 15, 15, ChallengeRatingConstants.CR11, null, false, 0, 9, 0)]
        [TestCase(CreatureConstants.Elemental_Water_Greater, SizeConstants.Huge, 15, 15, ChallengeRatingConstants.CR9, null, false, 0, 9, 0)]
        [TestCase(CreatureConstants.Elemental_Water_Huge, SizeConstants.Huge, 15, 15, ChallengeRatingConstants.CR7, null, false, 0, 9, 0)]
        [TestCase(CreatureConstants.Elemental_Water_Large, SizeConstants.Large, 10, 10, ChallengeRatingConstants.CR5, null, false, 0, 9, 0)]
        [TestCase(CreatureConstants.Elemental_Water_Medium, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR3, null, false, 0, 8, 0)]
        [TestCase(CreatureConstants.Elemental_Water_Small, SizeConstants.Small, 5, 5, ChallengeRatingConstants.CR1, null, false, 0, 6, 0)]
        [TestCase(CreatureConstants.Elephant, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.CR7, null, false, 0, 7, 0)]
        [TestCase(CreatureConstants.Elf_Aquatic, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR1_2nd, 0, true, 0, 0, 2)]
        [TestCase(CreatureConstants.Elf_Drow, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR1, 2, true, 1, 0, 2)]
        [TestCase(CreatureConstants.Elf_Gray, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR1_2nd, 0, true, 0, 0, 2)]
        [TestCase(CreatureConstants.Elf_Half, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR1_2nd, 0, true, 0, 0, 2)]
        [TestCase(CreatureConstants.Elf_High, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR1_2nd, 0, true, 0, 0, 2)]
        [TestCase(CreatureConstants.Elf_Wild, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR1_2nd, 0, true, 0, 0, 2)]
        [TestCase(CreatureConstants.Elf_Wood, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR1_2nd, 0, true, 0, 0, 2)]
        [TestCase(CreatureConstants.Erinyes, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR8, 7, true, 12, 8, 2)]
        [TestCase(CreatureConstants.EtherealFilcher, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR3, null, false, 5, 3, 4)]
        [TestCase(CreatureConstants.EtherealMarauder, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR3, null, false, 15, 3, 0)]
        [TestCase(CreatureConstants.Ettercap, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR3, 4, false, 0, 1, 0)]
        [TestCase(CreatureConstants.Ettin, SizeConstants.Large, 10, 10, ChallengeRatingConstants.CR6, 5, true, 0, 7, 2)]
        [TestCase(CreatureConstants.FireBeetle_Giant, SizeConstants.Small, 5, 5, ChallengeRatingConstants.CR1_3rd, null, false, 0, 5, 0)]
        [TestCase(CreatureConstants.FormianMyrmarch, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR10, null, true, 12, 15, 2)]
        [TestCase(CreatureConstants.FormianQueen, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR17, null, true, 17, 14, 2)]
        [TestCase(CreatureConstants.FormianTaskmaster, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR7, null, true, 10, 6, 2)]
        [TestCase(CreatureConstants.FormianWarrior, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR3, null, true, 0, 5, 2)]
        [TestCase(CreatureConstants.FormianWorker, SizeConstants.Small, 5, 5, ChallengeRatingConstants.CR1_2nd, null, true, 0, 4, 2)]
        [TestCase(CreatureConstants.FrostWorm, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.CR12, null, false, 0, 10, 0)]
        [TestCase(CreatureConstants.Gargoyle, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR4, 5, true, 0, 4, 2)]
        [TestCase(CreatureConstants.Gargoyle_Kapoacinth, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR4, 5, true, 0, 4, 2)]
        [TestCase(CreatureConstants.GelatinousCube, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR3, null, false, 0, 0, 0)]
        [TestCase(CreatureConstants.Ghaele, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR13, null, true, 12, 14, 2)]
        [TestCase(CreatureConstants.Ghoul, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR1, null, false, 0, 2, 2)]
        [TestCase(CreatureConstants.Ghoul_Ghast, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR3, null, false, 0, 4, 2)]
        [TestCase(CreatureConstants.Ghoul_Lacedon, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR1, null, false, 0, 2, 2)]
        [TestCase(CreatureConstants.Giant_Cloud, SizeConstants.Huge, 15, 15, ChallengeRatingConstants.CR11, 0, true, 15, 12, 2)]
        [TestCase(CreatureConstants.Giant_Fire, SizeConstants.Large, 10, 10, ChallengeRatingConstants.CR10, 4, true, 0, 8, 2)]
        [TestCase(CreatureConstants.Giant_Frost, SizeConstants.Large, 10, 10, ChallengeRatingConstants.CR9, 4, true, 0, 9, 2)]
        [TestCase(CreatureConstants.Giant_Hill, SizeConstants.Large, 10, 10, ChallengeRatingConstants.CR7, 4, true, 0, 9, 2)]
        [TestCase(CreatureConstants.Giant_Stone, SizeConstants.Large, 10, 10, ChallengeRatingConstants.CR8, 4, true, 0, 11, 2)]
        [TestCase(CreatureConstants.Giant_Stone_Elder, SizeConstants.Large, 10, 10, ChallengeRatingConstants.CR9, 6, true, 10, 11, 2)]
        [TestCase(CreatureConstants.Giant_Storm, SizeConstants.Huge, 15, 15, ChallengeRatingConstants.CR13, 0, true, 20, 12, 2)]
        [TestCase(CreatureConstants.GibberingMouther, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR5, null, false, 0, 8, 0)]
        [TestCase(CreatureConstants.Girallon, SizeConstants.Large, 10, 10, ChallengeRatingConstants.CR6, null, false, 0, 4, 4)]
        [TestCase(CreatureConstants.Githyanki, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR1, 2, true, 1, 0, 2)]
        [TestCase(CreatureConstants.Githzerai, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR1, 2, true, 1, 0, 2)]
        [TestCase(CreatureConstants.Glabrezu, SizeConstants.Huge, 15, 15, ChallengeRatingConstants.CR13, null, true, 14, 19, 2)]
        [TestCase(CreatureConstants.Gnoll, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR1, 1, true, 0, 1, 2)]
        [TestCase(CreatureConstants.Gnome_Forest, SizeConstants.Small, 5, 5, ChallengeRatingConstants.CR1_2nd, 0, true, 0, 0, 2)]
        [TestCase(CreatureConstants.Gnome_Rock, SizeConstants.Small, 5, 5, ChallengeRatingConstants.CR1_2nd, 0, true, 0, 0, 2)]
        [TestCase(CreatureConstants.Gnome_Svirfneblin, SizeConstants.Small, 5, 5, ChallengeRatingConstants.CR1, 3, true, 1, 0, 2)]
        [TestCase(CreatureConstants.Goblin, SizeConstants.Small, 5, 5, ChallengeRatingConstants.CR1_3rd, 0, true, 0, 0, 2)]
        [TestCase(CreatureConstants.Golem_Clay, SizeConstants.Large, 10, 10, ChallengeRatingConstants.CR10, null, false, 0, 14, 2)]
        [TestCase(CreatureConstants.Golem_Flesh, SizeConstants.Large, 10, 10, ChallengeRatingConstants.CR7, null, false, 0, 10, 2)]
        [TestCase(CreatureConstants.Golem_Iron, SizeConstants.Large, 10, 10, ChallengeRatingConstants.CR13, null, false, 0, 22, 2)]
        [TestCase(CreatureConstants.Golem_Stone, SizeConstants.Large, 10, 10, ChallengeRatingConstants.CR11, null, false, 0, 18, 2)]
        [TestCase(CreatureConstants.Golem_Stone_Greater, SizeConstants.Huge, 15, 15, ChallengeRatingConstants.CR16, null, false, 0, 21, 2)]
        [TestCase(CreatureConstants.Gorgon, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR8, null, false, 0, 11, 0)]
        [TestCase(CreatureConstants.GrayOoze, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR4, null, false, 0, 0, 0)]
        [TestCase(CreatureConstants.GrayRender, SizeConstants.Large, 10, 10, ChallengeRatingConstants.CR8, 5, false, 0, 10, 2)]
        [TestCase(CreatureConstants.GreenHag, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR5, 0, true, 9, 11, 2)]
        [TestCase(CreatureConstants.Grick, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR3, null, false, 0, 4, 0)]
        [TestCase(CreatureConstants.Grig, SizeConstants.Tiny, 2.5, 0, ChallengeRatingConstants.CR1, 3, true, 9, 2, 2)]
        [TestCase(CreatureConstants.Grig_WithFiddle, SizeConstants.Tiny, 2.5, 0, ChallengeRatingConstants.CR1, 3, true, 9, 2, 2)]
        [TestCase(CreatureConstants.Griffon, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR4, 3, false, 0, 6, 0)]
        [TestCase(CreatureConstants.Grimlock, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR1, 2, true, 0, 4, 2)]
        [TestCase(CreatureConstants.Gynosphinx, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR8, 4, false, 14, 11, 0)]
        [TestCase(CreatureConstants.Halfling_Deep, SizeConstants.Small, 5, 5, ChallengeRatingConstants.CR1_2nd, 0, true, 0, 0, 2)]
        [TestCase(CreatureConstants.Halfling_Lightfoot, SizeConstants.Small, 5, 5, ChallengeRatingConstants.CR1_2nd, 0, true, 0, 0, 2)]
        [TestCase(CreatureConstants.Halfling_Tallfellow, SizeConstants.Small, 5, 5, ChallengeRatingConstants.CR1_2nd, 0, true, 0, 0, 2)]
        [TestCase(CreatureConstants.Harpy, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR4, 3, true, 0, 1, 2)]
        [TestCase(CreatureConstants.Hawk, SizeConstants.Tiny, 2.5, 0, ChallengeRatingConstants.CR1_3rd, null, false, 0, 2, 0)]
        [TestCase(CreatureConstants.Hellcat_Bezekira, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR7, null, false, 0, 7, 0)]
        [TestCase(CreatureConstants.Hellwasp_Swarm, SizeConstants.Diminutive, 10, 0, ChallengeRatingConstants.CR8, null, false, 0, 0, 0)]
        [TestCase(CreatureConstants.HellHound, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR3, 3, false, 0, 5, 0)]
        [TestCase(CreatureConstants.HellHound_NessianWarhound, SizeConstants.Large, 10, 10, ChallengeRatingConstants.CR9, 4, false, 0, 7, 0)]
        [TestCase(CreatureConstants.Hezrou, SizeConstants.Large, 10, 10, ChallengeRatingConstants.CR11, 9, true, 13, 14, 2)]
        [TestCase(CreatureConstants.Hieracosphinx, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR5, 3, false, 0, 8, 0)]
        [TestCase(CreatureConstants.Hippogriff, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR2, null, false, 0, 4, 0)]
        [TestCase(CreatureConstants.Hobgoblin, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR1_2nd, 1, true, 0, 0, 2)]
        [TestCase(CreatureConstants.Homunculus, SizeConstants.Tiny, 2.5, 0, ChallengeRatingConstants.CR1, null, false, 0, 0, 0)]
        [TestCase(CreatureConstants.HornedDevil_Cornugon, SizeConstants.Large, 10, 10, ChallengeRatingConstants.CR16, null, true, 15, 19, 2)]
        [TestCase(CreatureConstants.Horse_Heavy, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR1, null, false, 0, 3, 0)]
        [TestCase(CreatureConstants.Horse_Heavy_War, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR2, null, false, 0, 4, 0)]
        [TestCase(CreatureConstants.Horse_Light, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR1, null, false, 0, 3, 0)]
        [TestCase(CreatureConstants.Horse_Light_War, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR1, null, false, 0, 4, 0)]
        [TestCase(CreatureConstants.HoundArchon, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR4, 5, true, 6, 0, 2)]
        [TestCase(CreatureConstants.Howler, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR3, 3, false, 0, 5, 0)]
        [TestCase(CreatureConstants.Human, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR1_2nd, 0, true, 0, 0, 2)]
        [TestCase(CreatureConstants.Hydra_5Heads, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.CR4, null, false, 0, 6, 0)]
        [TestCase(CreatureConstants.Hydra_6Heads, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.CR5, null, false, 0, 7, 0)]
        [TestCase(CreatureConstants.Hydra_7Heads, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.CR6, null, false, 0, 8, 0)]
        [TestCase(CreatureConstants.Hydra_8Heads, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.CR7, null, false, 0, 9, 0)]
        [TestCase(CreatureConstants.Hydra_9Heads, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.CR8, null, false, 0, 10, 0)]
        [TestCase(CreatureConstants.Hydra_10Heads, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.CR9, null, false, 0, 11, 0)]
        [TestCase(CreatureConstants.Hydra_11Heads, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.CR10, null, false, 0, 12, 0)]
        [TestCase(CreatureConstants.Hydra_12Heads, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.CR11, null, false, 0, 13, 0)]
        [TestCase(CreatureConstants.Hyena, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR1, null, false, 0, 2, 0)]
        [TestCase(CreatureConstants.IceDevil_Gelugon, SizeConstants.Large, 10, 10, ChallengeRatingConstants.CR13, null, true, 13, 18, 2)]
        [TestCase(CreatureConstants.Imp, SizeConstants.Tiny, 2.5, 0, ChallengeRatingConstants.CR2, null, false, 6, 5, 0)]
        [TestCase(CreatureConstants.InvisibleStalker, SizeConstants.Large, 10, 10, ChallengeRatingConstants.CR7, null, false, 0, 4, 0)]
        [TestCase(CreatureConstants.Janni, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR4, 5, true, 12, 1, 2)]
        [TestCase(CreatureConstants.Kobold, SizeConstants.Small, 5, 5, ChallengeRatingConstants.CR1_4th, 0, true, 0, 1, 2)]
        [TestCase(CreatureConstants.Kolyarut, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR12, null, true, 13, 10, 2)]
        [TestCase(CreatureConstants.Kraken, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.CR12, null, false, 9, 14, 0)]
        [TestCase(CreatureConstants.Krenshar, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR1, 2, false, 3, 3, 0)]
        [TestCase(CreatureConstants.KuoToa, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR2, 3, true, 0, 6, 2)]
        [TestCase(CreatureConstants.Lamia, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR6, 4, true, 9, 7, 2)]
        [TestCase(CreatureConstants.Lammasu, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR8, 5, false, 7, 10, 0)]
        [TestCase(CreatureConstants.LanternArchon, SizeConstants.Small, 5, 5, ChallengeRatingConstants.CR2, null, false, 3, 4, 0)]
        [TestCase(CreatureConstants.Lemure, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR1, null, false, 0, 4, 0)]
        [TestCase(CreatureConstants.Leonal, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR12, null, false, 10, 14, 2)]
        [TestCase(CreatureConstants.Leopard, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR2, null, false, 0, 1, 0)]
        [TestCase(CreatureConstants.Lillend, SizeConstants.Large, 10, 10, ChallengeRatingConstants.CR7, 6, true, 10, 5, 2)]
        [TestCase(CreatureConstants.Lion, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR3, null, false, 0, 3, 0)]
        [TestCase(CreatureConstants.Lion_Dire, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR5, null, false, 0, 4, 0)]
        [TestCase(CreatureConstants.Lizard, SizeConstants.Tiny, 2.5, 0, ChallengeRatingConstants.CR1_6th, null, false, 0, 0, 0)]
        [TestCase(CreatureConstants.Lizard_Monitor, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR2, null, false, 0, 3, 0)]
        [TestCase(CreatureConstants.Lizardfolk, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR1, 1, true, 0, 5, 2)]
        [TestCase(CreatureConstants.Locathah, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR1_2nd, 1, true, 0, 3, 2)]
        [TestCase(CreatureConstants.Locust_Swarm, SizeConstants.Diminutive, 10, 0, ChallengeRatingConstants.CR3, null, false, 0, 0, 0)]
        [TestCase(CreatureConstants.Magmin, SizeConstants.Small, 5, 5, ChallengeRatingConstants.CR3, null, false, 0, 6, 0)]
        [TestCase(CreatureConstants.MantaRay, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR1, null, false, 0, 3, 0)]
        [TestCase(CreatureConstants.Manticore, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR5, 3, false, 0, 6, 0)]
        [TestCase(CreatureConstants.Marilith, SizeConstants.Large, 10, 10, ChallengeRatingConstants.CR17, null, true, 16, 16, 6)]
        [TestCase(CreatureConstants.Marut, SizeConstants.Large, 10, 10, ChallengeRatingConstants.CR15, null, false, 14, 16, 2)]
        [TestCase(CreatureConstants.Medusa, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR7, 0, true, 0, 3, 2)]
        [TestCase(CreatureConstants.Megaraptor, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR6, null, false, 0, 6, 0)]
        [TestCase(CreatureConstants.Mephit_Air, SizeConstants.Small, 5, 5, ChallengeRatingConstants.CR3, 3, true, 3, 3, 2)]
        [TestCase(CreatureConstants.Mephit_Dust, SizeConstants.Small, 5, 5, ChallengeRatingConstants.CR3, 3, true, 3, 3, 2)]
        [TestCase(CreatureConstants.Mephit_Earth, SizeConstants.Small, 5, 5, ChallengeRatingConstants.CR3, 3, true, 6, 6, 2)]
        [TestCase(CreatureConstants.Mephit_Fire, SizeConstants.Small, 5, 5, ChallengeRatingConstants.CR3, 3, true, 3, 4, 2)]
        [TestCase(CreatureConstants.Mephit_Ice, SizeConstants.Small, 5, 5, ChallengeRatingConstants.CR3, 3, true, 3, 4, 2)]
        [TestCase(CreatureConstants.Mephit_Magma, SizeConstants.Small, 5, 5, ChallengeRatingConstants.CR3, 3, true, 6, 4, 2)]
        [TestCase(CreatureConstants.Mephit_Ooze, SizeConstants.Small, 5, 5, ChallengeRatingConstants.CR3, 3, true, 3, 5, 2)]
        [TestCase(CreatureConstants.Mephit_Salt, SizeConstants.Small, 5, 5, ChallengeRatingConstants.CR3, 3, true, 3, 6, 2)]
        [TestCase(CreatureConstants.Mephit_Steam, SizeConstants.Small, 5, 5, ChallengeRatingConstants.CR3, 3, true, 3, 4, 2)]
        [TestCase(CreatureConstants.Mephit_Water, SizeConstants.Small, 5, 5, ChallengeRatingConstants.CR3, 3, true, 3, 5, 2)]
        [TestCase(CreatureConstants.Merfolk, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR1_2nd, 1, true, 0, 0, 2)]
        [TestCase(CreatureConstants.Mimic, SizeConstants.Large, 10, 10, ChallengeRatingConstants.CR4, null, false, 0, 5, 0)]
        [TestCase(CreatureConstants.MindFlayer, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR8, 7, true, 8, 3, 2)]
        [TestCase(CreatureConstants.Minotaur, SizeConstants.Large, 10, 10, ChallengeRatingConstants.CR4, 2, true, 0, 5, 2)]
        [TestCase(CreatureConstants.Mohrg, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR8, null, false, 0, 9, 0)]
        [TestCase(CreatureConstants.Monkey, SizeConstants.Tiny, 2.5, 0, ChallengeRatingConstants.CR1_6th, null, false, 0, 0, 0)]
        [TestCase(CreatureConstants.Mule, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR1, null, false, 0, 3, 0)]
        [TestCase(CreatureConstants.Mummy, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR5, 0, true, 0, 10, 2)]
        [TestCase(CreatureConstants.Naga_Dark, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR8, null, false, 7, 3, 0)]
        [TestCase(CreatureConstants.Naga_Guardian, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR10, null, false, 9, 7, 0)]
        [TestCase(CreatureConstants.Naga_Spirit, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR9, null, false, 7, 6, 0)]
        [TestCase(CreatureConstants.Naga_Water, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR7, null, false, 7, 5, 0)]
        [TestCase(CreatureConstants.Nalfeshnee, SizeConstants.Huge, 15, 15, ChallengeRatingConstants.CR14, null, true, 12, 18, 2)]
        [TestCase(CreatureConstants.NightHag, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR9, null, true, 8, 11, 2)]
        [TestCase(CreatureConstants.Nightcrawler, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.CR18, null, false, 25, 29, 0)]
        [TestCase(CreatureConstants.Nightmare, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR5, 4, false, 20, 13, 0)]
        [TestCase(CreatureConstants.Nightmare_Cauchemar, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.CR11, 4, false, 20, 16, 0)]
        [TestCase(CreatureConstants.Nightwalker, SizeConstants.Huge, 15, 15, ChallengeRatingConstants.CR16, null, false, 21, 22, 2)]
        [TestCase(CreatureConstants.Nightwing, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.CR14, null, false, 17, 18, 0)]
        [TestCase(CreatureConstants.Nixie, SizeConstants.Small, 5, 5, ChallengeRatingConstants.CR1, 3, true, 12, 0, 2)]
        [TestCase(CreatureConstants.Nymph, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR7, 7, true, 7, 0, 2)]
        [TestCase(CreatureConstants.OchreJelly, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR5, null, false, 0, 0, 0)]
        [TestCase(CreatureConstants.Octopus, SizeConstants.Small, 5, 5, ChallengeRatingConstants.CR1, null, false, 0, 2, 0)]
        [TestCase(CreatureConstants.Octopus_Giant, SizeConstants.Large, 10, 10, ChallengeRatingConstants.CR8, null, false, 0, 7, 0)]
        [TestCase(CreatureConstants.Ogre, SizeConstants.Large, 10, 10, ChallengeRatingConstants.CR3, 2, true, 0, 5, 2)]
        [TestCase(CreatureConstants.Ogre_Merrow, SizeConstants.Large, 10, 10, ChallengeRatingConstants.CR3, 2, true, 0, 5, 2)]
        [TestCase(CreatureConstants.OgreMage, SizeConstants.Large, 10, 10, ChallengeRatingConstants.CR8, 7, true, 9, 5, 2)]
        [TestCase(CreatureConstants.Orc, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR1_2nd, 0, true, 0, 0, 2)]
        [TestCase(CreatureConstants.Orc_Half, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR1_2nd, 0, true, 0, 0, 2)]
        [TestCase(CreatureConstants.Otyugh, SizeConstants.Large, 10, 10, ChallengeRatingConstants.CR4, null, false, 0, 8, 0)]
        [TestCase(CreatureConstants.Owl, SizeConstants.Tiny, 2.5, 0, ChallengeRatingConstants.CR1_4th, null, false, 0, 2, 0)]
        [TestCase(CreatureConstants.Owl_Giant, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR3, 2, false, 0, 3, 0)]
        [TestCase(CreatureConstants.Owlbear, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR4, null, false, 0, 5, 0)]
        [TestCase(CreatureConstants.Pegasus, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR3, 2, false, 5, 3, 0)]
        [TestCase(CreatureConstants.PhantomFungus, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR3, null, false, 12, 4, 0)]
        [TestCase(CreatureConstants.PhaseSpider, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR5, null, false, 15, 3, 0)]
        [TestCase(CreatureConstants.Phasm, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR7, null, true, 0, 5, 0)]
        [TestCase(CreatureConstants.PitFiend, SizeConstants.Large, 10, 10, ChallengeRatingConstants.CR20, null, false, 18, 23, 2)]
        [TestCase(CreatureConstants.Pixie, SizeConstants.Small, 5, 5, ChallengeRatingConstants.CR4, 4, true, 8, 1, 2)]
        [TestCase(CreatureConstants.Pixie_WithIrresistibleDance, SizeConstants.Small, 5, 5, ChallengeRatingConstants.CR5, 6, true, 8, 1, 2)]
        [TestCase(CreatureConstants.Pony, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR1_4th, null, false, 0, 2, 0)]
        [TestCase(CreatureConstants.Pony_War, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR1_2nd, null, false, 0, 2, 0)]
        [TestCase(CreatureConstants.Porpoise, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR1_2nd, null, false, 0, 2, 0)]
        [TestCase(CreatureConstants.PrayingMantis_Giant, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR3, null, false, 0, 6, 0)]
        [TestCase(CreatureConstants.Pseudodragon, SizeConstants.Tiny, 2.5, 0, ChallengeRatingConstants.CR1, 3, false, 0, 4, 0)]
        [TestCase(CreatureConstants.PurpleWorm, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.CR12, null, false, 0, 15, 0)]
        [TestCase(CreatureConstants.Pyrohydra_5Heads, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.CR6, null, false, 0, 6, 0)]
        [TestCase(CreatureConstants.Pyrohydra_6Heads, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.CR7, null, false, 0, 7, 0)]
        [TestCase(CreatureConstants.Pyrohydra_7Heads, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.CR8, null, false, 0, 8, 0)]
        [TestCase(CreatureConstants.Pyrohydra_8Heads, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.CR9, null, false, 0, 9, 0)]
        [TestCase(CreatureConstants.Pyrohydra_9Heads, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.CR10, null, false, 0, 10, 0)]
        [TestCase(CreatureConstants.Pyrohydra_10Heads, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.CR11, null, false, 0, 11, 0)]
        [TestCase(CreatureConstants.Pyrohydra_11Heads, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.CR12, null, false, 0, 12, 0)]
        [TestCase(CreatureConstants.Pyrohydra_12Heads, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.CR13, null, false, 0, 13, 0)]
        [TestCase(CreatureConstants.Quasit, SizeConstants.Tiny, 2.5, 0, ChallengeRatingConstants.CR2, null, false, 6, 3, 2)]
        [TestCase(CreatureConstants.Rakshasa, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR10, 7, true, 7, 9, 2)]
        [TestCase(CreatureConstants.Rast, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR5, null, false, 0, 4, 0)]
        [TestCase(CreatureConstants.Rat, SizeConstants.Tiny, 2.5, 0, ChallengeRatingConstants.CR1_8th, null, false, 0, 0, 0)]
        [TestCase(CreatureConstants.Rat_Dire, SizeConstants.Small, 5, 5, ChallengeRatingConstants.CR1_3rd, null, false, 0, 1, 0)]
        [TestCase(CreatureConstants.Rat_Swarm, SizeConstants.Tiny, 10, 0, ChallengeRatingConstants.CR2, null, false, 0, 0, 0)]
        [TestCase(CreatureConstants.Raven, SizeConstants.Tiny, 2.5, 0, ChallengeRatingConstants.CR1_6th, null, false, 0, 0, 0)]
        [TestCase(CreatureConstants.Ravid, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR5, null, false, 20, 15, 1)]
        [TestCase(CreatureConstants.RazorBoar, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR10, null, false, 0, 17, 0)]
        [TestCase(CreatureConstants.Remorhaz, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.CR7, null, false, 0, 11, 0)]
        [TestCase(CreatureConstants.Retriever, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.CR11, null, false, 0, 10, 0)]
        [TestCase(CreatureConstants.Rhinoceras, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR4, null, false, 0, 7, 0)]
        [TestCase(CreatureConstants.Roc, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.CR9, null, false, 0, 9, 0)]
        [TestCase(CreatureConstants.Roper, SizeConstants.Large, 10, 10, ChallengeRatingConstants.CR12, null, false, 0, 14, 0)]
        [TestCase(CreatureConstants.RustMonster, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR3, null, false, 0, 5, 0)]
        [TestCase(CreatureConstants.Sahuagin, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR2, 2, true, 0, 5, 2)]
        [TestCase(CreatureConstants.Sahuagin_Mutant, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR2, 3, true, 0, 5, 4)]
        [TestCase(CreatureConstants.Sahuagin_Malenti, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR2, 2, true, 0, 5, 2)]
        [TestCase(CreatureConstants.Salamander_Average, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR6, 5, true, 0, 7, 2)]
        [TestCase(CreatureConstants.Salamander_Flamebrother, SizeConstants.Small, 5, 5, ChallengeRatingConstants.CR3, 4, true, 0, 7, 2)]
        [TestCase(CreatureConstants.Salamander_Noble, SizeConstants.Large, 10, 10, ChallengeRatingConstants.CR10, null, true, 15, 8, 2)]
        [TestCase(CreatureConstants.Satyr, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR2, 2, true, 0, 4, 2)]
        [TestCase(CreatureConstants.Satyr_WithPipes, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR4, 2, true, 10, 4, 2)]
        [TestCase(CreatureConstants.Scorpion_Monstrous_Colossal, SizeConstants.Colossal, 40, 30, ChallengeRatingConstants.CR12, null, false, 0, 25, 0)]
        [TestCase(CreatureConstants.Scorpion_Monstrous_Gargantuan, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.CR10, null, false, 0, 18, 0)]
        [TestCase(CreatureConstants.Scorpion_Monstrous_Huge, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.CR7, null, false, 0, 12, 0)]
        [TestCase(CreatureConstants.Scorpion_Monstrous_Large, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR3, null, false, 0, 7, 0)]
        [TestCase(CreatureConstants.Scorpion_Monstrous_Medium, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR1, null, false, 0, 4, 0)]
        [TestCase(CreatureConstants.Scorpion_Monstrous_Small, SizeConstants.Small, 5, 5, ChallengeRatingConstants.CR1_2nd, null, false, 0, 3, 0)]
        [TestCase(CreatureConstants.Scorpion_Monstrous_Tiny, SizeConstants.Tiny, 2.5, 0, ChallengeRatingConstants.CR1_4th, null, false, 0, 2, 0)]
        [TestCase(CreatureConstants.Scorpionfolk, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR7, 4, true, 10, 6, 2)]
        [TestCase(CreatureConstants.SeaCat, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR4, null, false, 0, 8, 0)]
        [TestCase(CreatureConstants.SeaHag, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR4, 0, true, 0, 3, 2)]
        [TestCase(CreatureConstants.Shadow, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR3, null, false, 0, 0, 0)]
        [TestCase(CreatureConstants.Shadow_Greater, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR8, null, false, 0, 0, 0)]
        [TestCase(CreatureConstants.ShadowMastiff, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR5, 3, false, 0, 3, 0)]
        [TestCase(CreatureConstants.ShamblingMound, SizeConstants.Large, 10, 10, ChallengeRatingConstants.CR6, 6, false, 0, 11, 0)]
        [TestCase(CreatureConstants.Shark_Dire, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.CR9, null, false, 0, 7, 0)]
        [TestCase(CreatureConstants.Shark_Huge, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.CR4, null, false, 0, 5, 0)]
        [TestCase(CreatureConstants.Shark_Large, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR2, null, false, 0, 4, 0)]
        [TestCase(CreatureConstants.Shark_Medium, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR1, null, false, 0, 3, 0)]
        [TestCase(CreatureConstants.ShieldGuardian, SizeConstants.Large, 10, 10, ChallengeRatingConstants.CR8, null, false, 0, 15, 2)]
        [TestCase(CreatureConstants.ShockerLizard, SizeConstants.Small, 5, 5, ChallengeRatingConstants.CR2, null, false, 0, 3, 0)]
        [TestCase(CreatureConstants.Shrieker, SizeConstants.Medium, 5, 0, ChallengeRatingConstants.CR1, null, false, 0, 3, 0)]
        [TestCase(CreatureConstants.Skum, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR2, 3, true, 0, 2, 2)]
        [TestCase(CreatureConstants.Slaad_Blue, SizeConstants.Large, 10, 10, ChallengeRatingConstants.CR8, 6, true, 8, 9, 2)]
        [TestCase(CreatureConstants.Slaad_Death, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR13, null, true, 15, 12, 2)]
        [TestCase(CreatureConstants.Slaad_Gray, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR10, 6, true, 10, 11, 2)]
        [TestCase(CreatureConstants.Slaad_Green, SizeConstants.Large, 10, 10, ChallengeRatingConstants.CR9, 7, true, 9, 13, 2)]
        [TestCase(CreatureConstants.Slaad_Red, SizeConstants.Large, 10, 10, ChallengeRatingConstants.CR7, 6, true, 0, 8, 2)]
        [TestCase(CreatureConstants.Snake_Constrictor, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR2, null, false, 0, 2, 0)]
        [TestCase(CreatureConstants.Snake_Constrictor_Giant, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.CR5, null, false, 0, 4, 0)]
        [TestCase(CreatureConstants.Snake_Viper_Huge, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.CR3, null, false, 0, 3, 0)]
        [TestCase(CreatureConstants.Snake_Viper_Large, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR2, null, false, 0, 3, 0)]
        [TestCase(CreatureConstants.Snake_Viper_Medium, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR1, null, false, 0, 3, 0)]
        [TestCase(CreatureConstants.Snake_Viper_Small, SizeConstants.Small, 5, 5, ChallengeRatingConstants.CR1_2nd, null, false, 0, 3, 0)]
        [TestCase(CreatureConstants.Snake_Viper_Tiny, SizeConstants.Tiny, 2.5, 0, ChallengeRatingConstants.CR1_3rd, null, false, 0, 2, 0)]
        [TestCase(CreatureConstants.Spectre, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR7, null, false, 0, 0, 0)]
        [TestCase(CreatureConstants.Spider_Monstrous_Hunter_Colossal, SizeConstants.Colossal, 40, 30, ChallengeRatingConstants.CR11, null, false, 0, 18, 0)]
        [TestCase(CreatureConstants.Spider_Monstrous_Hunter_Gargantuan, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.CR8, null, false, 0, 10, 0)]
        [TestCase(CreatureConstants.Spider_Monstrous_Hunter_Huge, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.CR5, null, false, 0, 5, 0)]
        [TestCase(CreatureConstants.Spider_Monstrous_Hunter_Large, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR2, null, false, 0, 2, 0)]
        [TestCase(CreatureConstants.Spider_Monstrous_Hunter_Medium, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR1, null, false, 0, 1, 0)]
        [TestCase(CreatureConstants.Spider_Monstrous_Hunter_Small, SizeConstants.Small, 5, 5, ChallengeRatingConstants.CR1_2nd, null, false, 0, 0, 0)]
        [TestCase(CreatureConstants.Spider_Monstrous_Hunter_Tiny, SizeConstants.Tiny, 2.5, 0, ChallengeRatingConstants.CR1_4th, null, false, 0, 0, 0)]
        [TestCase(CreatureConstants.Spider_Monstrous_WebSpinner_Colossal, SizeConstants.Colossal, 40, 30, ChallengeRatingConstants.CR11, null, false, 0, 18, 0)]
        [TestCase(CreatureConstants.Spider_Monstrous_WebSpinner_Gargantuan, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.CR8, null, false, 0, 10, 0)]
        [TestCase(CreatureConstants.Spider_Monstrous_WebSpinner_Huge, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.CR5, null, false, 0, 5, 0)]
        [TestCase(CreatureConstants.Spider_Monstrous_WebSpinner_Large, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR2, null, false, 0, 2, 0)]
        [TestCase(CreatureConstants.Spider_Monstrous_WebSpinner_Medium, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR1, null, false, 0, 1, 0)]
        [TestCase(CreatureConstants.Spider_Monstrous_WebSpinner_Small, SizeConstants.Small, 5, 5, ChallengeRatingConstants.CR1_2nd, null, false, 0, 0, 0)]
        [TestCase(CreatureConstants.Spider_Monstrous_WebSpinner_Tiny, SizeConstants.Tiny, 2.5, 0, ChallengeRatingConstants.CR1_4th, null, false, 0, 0, 0)]
        [TestCase(CreatureConstants.SpiderEater, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR5, null, false, 12, 4, 0)]
        [TestCase(CreatureConstants.Spider_Swarm, SizeConstants.Diminutive, 10, 0, ChallengeRatingConstants.CR1, null, false, 0, 0, 0)]
        [TestCase(CreatureConstants.Squid, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR1, null, false, 0, 3, 0)]
        [TestCase(CreatureConstants.Squid_Giant, SizeConstants.Huge, 15, 15, ChallengeRatingConstants.CR9, null, false, 0, 6, 0)]
        [TestCase(CreatureConstants.StagBeetle_Giant, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR4, null, false, 0, 10, 0)]
        [TestCase(CreatureConstants.Stirge, SizeConstants.Tiny, 2.5, 0, ChallengeRatingConstants.CR1_2nd, null, false, 0, 0, 0)]
        [TestCase(CreatureConstants.Succubus, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR7, 6, true, 12, 9, 2)]
        [TestCase(CreatureConstants.Tarrasque, SizeConstants.Colossal, 30, 20, ChallengeRatingConstants.CR20, null, false, 0, 30, 2)]
        [TestCase(CreatureConstants.Tendriculos, SizeConstants.Huge, 15, 15, ChallengeRatingConstants.CR6, null, false, 0, 9, 0)]
        [TestCase(CreatureConstants.Thoqqua, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR2, null, false, 0, 7, 0)]
        [TestCase(CreatureConstants.Tiefling, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR1_2nd, 1, true, 1, 0, 2)]
        [TestCase(CreatureConstants.Tiger, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR4, null, false, 0, 3, 0)]
        [TestCase(CreatureConstants.Tiger_Dire, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR8, null, false, 0, 6, 0)]
        [TestCase(CreatureConstants.Titan, SizeConstants.Huge, 15, 15, ChallengeRatingConstants.CR21, null, true, 20, 19, 2)]
        [TestCase(CreatureConstants.Toad, SizeConstants.Diminutive, 1, 0, ChallengeRatingConstants.CR1_10th, null, false, 0, 0, 0)]
        [TestCase(CreatureConstants.Tojanida_Adult, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR5, null, false, 0, 12, 0)]
        [TestCase(CreatureConstants.Tojanida_Elder, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR9, null, false, 0, 14, 0)]
        [TestCase(CreatureConstants.Tojanida_Juvenile, SizeConstants.Small, 5, 5, ChallengeRatingConstants.CR3, null, false, 0, 10, 0)]
        [TestCase(CreatureConstants.Treant, SizeConstants.Huge, 15, 15, ChallengeRatingConstants.CR8, 5, false, 12, 13, 2)]
        [TestCase(CreatureConstants.Triceratops, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.CR9, null, false, 0, 11, 0)]
        [TestCase(CreatureConstants.Triton, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR2, 2, true, 7, 6, 2)]
        [TestCase(CreatureConstants.Troglodyte, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR1, 2, true, 0, 6, 2)]
        [TestCase(CreatureConstants.Troll, SizeConstants.Large, 10, 10, ChallengeRatingConstants.CR5, 5, true, 0, 5, 2)]
        [TestCase(CreatureConstants.Troll_Scrag, SizeConstants.Large, 10, 10, ChallengeRatingConstants.CR5, 5, true, 0, 5, 2)]
        [TestCase(CreatureConstants.TrumpetArchon, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR14, 8, true, 12, 14, 2)]
        [TestCase(CreatureConstants.Tyrannosaurus, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.CR8, null, false, 0, 5, 0)]
        [TestCase(CreatureConstants.UmberHulk, SizeConstants.Large, 10, 10, ChallengeRatingConstants.CR7, null, false, 8, 8, 0)]
        [TestCase(CreatureConstants.UmberHulk_TrulyHorrid, SizeConstants.Huge, 15, 15, ChallengeRatingConstants.CR14, null, false, 8, 14, 0)]
        [TestCase(CreatureConstants.Unicorn, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR3, 4, false, 5, 6, 0)]
        [TestCase(CreatureConstants.VampireSpawn, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR4, null, false, 6, 3, 2)]
        [TestCase(CreatureConstants.Vargouille, SizeConstants.Small, 5, 5, ChallengeRatingConstants.CR2, null, false, 0, 0, 0)]
        [TestCase(CreatureConstants.VioletFungus, SizeConstants.Medium, 5, 10, ChallengeRatingConstants.CR3, null, false, 0, 4, 0)]
        [TestCase(CreatureConstants.Vrock, SizeConstants.Large, 10, 10, ChallengeRatingConstants.CR9, 8, true, 12, 11, 2)]
        [TestCase(CreatureConstants.Wasp_Giant, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR3, null, false, 0, 4, 0)]
        [TestCase(CreatureConstants.Weasel, SizeConstants.Tiny, 2.5, 0, ChallengeRatingConstants.CR1_4th, null, false, 0, 0, 0)]
        [TestCase(CreatureConstants.Weasel_Dire, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR2, null, false, 0, 2, 0)]
        [TestCase(CreatureConstants.Whale_Baleen, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.CR6, null, false, 0, 9, 0)]
        [TestCase(CreatureConstants.Whale_Cachalot, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.CR7, null, false, 0, 9, 0)]
        [TestCase(CreatureConstants.Whale_Orca, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.CR5, null, false, 0, 6, 0)]
        [TestCase(CreatureConstants.Wight, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR3, null, false, 0, 4, 2)]
        [TestCase(CreatureConstants.WillOWisp, SizeConstants.Small, 5, 5, ChallengeRatingConstants.CR6, null, false, 0, 0, 0)]
        [TestCase(CreatureConstants.WinterWolf, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR5, 3, false, 0, 5, 0)]
        [TestCase(CreatureConstants.Wolf, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR1, null, false, 0, 2, 0)]
        [TestCase(CreatureConstants.Wolf_Dire, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR3, null, false, 0, 3, 0)]
        [TestCase(CreatureConstants.Wolverine, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR2, null, false, 0, 2, 0)]
        [TestCase(CreatureConstants.Wolverine_Dire, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR4, null, false, 0, 4, 0)]
        [TestCase(CreatureConstants.Worg, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR2, 1, false, 0, 2, 0)]
        [TestCase(CreatureConstants.Wraith, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR5, null, false, 0, 0, 0)]
        [TestCase(CreatureConstants.Wraith_Dread, SizeConstants.Large, 10, 10, ChallengeRatingConstants.CR11, null, false, 0, 0, 0)]
        [TestCase(CreatureConstants.Wyvern, SizeConstants.Large, 10, 5, ChallengeRatingConstants.CR6, null, false, 0, 8, 0)]
        [TestCase(CreatureConstants.Xill, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR6, 4, true, 0, 7, 4)]
        [TestCase(CreatureConstants.Xorn_Average, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR6, null, false, 0, 14, 0)]
        [TestCase(CreatureConstants.Xorn_Elder, SizeConstants.Large, 10, 10, ChallengeRatingConstants.CR8, null, false, 0, 16, 0)]
        [TestCase(CreatureConstants.Xorn_Minor, SizeConstants.Small, 5, 5, ChallengeRatingConstants.CR3, null, false, 0, 12, 0)]
        [TestCase(CreatureConstants.YethHound, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR3, 3, false, 0, 8, 0)]
        [TestCase(CreatureConstants.Yrthak, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.CR9, null, false, 0, 8, 0)]
        [TestCase(CreatureConstants.YuanTi_Abomination, SizeConstants.Large, 10, 10, ChallengeRatingConstants.CR7, 7, true, 10, 10, 2)]
        [TestCase(CreatureConstants.YuanTi_Halfblood_SnakeHead, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR5, 5, true, 4, 4, 2)]
        [TestCase(CreatureConstants.YuanTi_Halfblood_SnakeArms, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR5, 5, true, 4, 4, 0)]
        [TestCase(CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR5, 5, true, 4, 4, 2)]
        [TestCase(CreatureConstants.YuanTi_Halfblood_SnakeTail, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR5, 5, true, 4, 4, 2)]
        [TestCase(CreatureConstants.YuanTi_Pureblood, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.CR3, 2, true, 4, 1, 2)]
        [TestCase(CreatureConstants.Zelekhut, SizeConstants.Large, 10, 10, ChallengeRatingConstants.CR9, 7, true, 8, 10, 2)]
        public void CreatureData(
            string creature,
            string size,
            double space,
            double reach,
            string challengeRating,
            int? levelAdjustment,
            bool canUseEquipment,
            int casterLevel,
            int naturalArmor,
            int numberOfHands)
        {
            var collection = DataIndexConstants.CreatureData.InitializeData();
            collection[DataIndexConstants.CreatureData.ChallengeRating] = challengeRating;
            collection[DataIndexConstants.CreatureData.LevelAdjustment] = Convert.ToString(levelAdjustment);
            collection[DataIndexConstants.CreatureData.Reach] = reach.ToString();
            collection[DataIndexConstants.CreatureData.Size] = size;
            collection[DataIndexConstants.CreatureData.Space] = space.ToString();
            collection[DataIndexConstants.CreatureData.CanUseEquipment] = canUseEquipment.ToString();
            collection[DataIndexConstants.CreatureData.CasterLevel] = casterLevel.ToString();
            collection[DataIndexConstants.CreatureData.NaturalArmor] = naturalArmor.ToString();
            collection[DataIndexConstants.CreatureData.NumberOfHands] = numberOfHands.ToString();

            AssertSegmentedData(creature, collection);

            //Number of Entries
            var emptyData = DataIndexConstants.CreatureData.InitializeData();
            var data = table[creature].ToArray();
            Assert.That(data, Has.Length.EqualTo(emptyData.Length));

            //Valid Challenge Ratings
            var challengeRatings = ChallengeRatingConstants.GetOrdered();

            Assert.That(data.Length - 1, Is.AtLeast(DataIndexConstants.CreatureData.ChallengeRating), creature);
            Assert.That(challengeRating, Is.Not.Empty, creature);
            Assert.That(data[DataIndexConstants.CreatureData.ChallengeRating], Is.Not.Empty, creature);
            Assert.That(challengeRatings, Contains.Item(challengeRating).And.Contains(data[DataIndexConstants.CreatureData.ChallengeRating]), creature);
            Assert.That(challengeRating, Is.Not.EqualTo(ChallengeRatingConstants.CR0), creature);
            Assert.That(data[DataIndexConstants.CreatureData.ChallengeRating], Is.Not.EqualTo(ChallengeRatingConstants.CR0), creature);

            //Valid Sizes
            var sizes = new[]
            {
                SizeConstants.Colossal,
                SizeConstants.Gargantuan,
                SizeConstants.Huge,
                SizeConstants.Large,
                SizeConstants.Medium,
                SizeConstants.Small,
                SizeConstants.Tiny,
                SizeConstants.Diminutive,
            };

            Assert.That(data.Length - 1, Is.AtLeast(DataIndexConstants.CreatureData.Size), creature);
            Assert.That(size, Is.Not.Empty, creature);
            Assert.That(data[DataIndexConstants.CreatureData.Size], Is.Not.Empty, creature);
            Assert.That(sizes, Contains.Item(size).And.Contains(data[DataIndexConstants.CreatureData.Size]), creature);

            //Valid Reach
            Assert.That(data.Length - 1, Is.AtLeast(DataIndexConstants.CreatureData.Reach), creature);
            Assert.That(double.TryParse(data[DataIndexConstants.CreatureData.Reach], out var actualReach), Is.True, creature);
            Assert.That(reach, Is.Not.Negative, creature);
            Assert.That(actualReach, Is.Not.Negative, creature);

            //Valid Space
            Assert.That(data.Length - 1, Is.AtLeast(DataIndexConstants.CreatureData.Space), creature);
            Assert.That(double.TryParse(data[DataIndexConstants.CreatureData.Space], out var actualSpace), Is.True, creature);
            Assert.That(space, Is.Positive, creature);
            Assert.That(actualSpace, Is.Positive, creature);

            //Valid Level Adjustment
            var characters = CreatureConstants.GetAllCharacters();

            Assert.That(data.Length - 1, Is.AtLeast(DataIndexConstants.CreatureData.LevelAdjustment), creature);

            if (!string.IsNullOrEmpty(data[DataIndexConstants.CreatureData.LevelAdjustment]))
            {
                Assert.That(int.TryParse(data[DataIndexConstants.CreatureData.LevelAdjustment], out var actualLevelAdjustment), Is.True, creature);
                Assert.That(levelAdjustment, Is.Not.Negative, creature);
                Assert.That(actualLevelAdjustment, Is.Not.Negative, creature);
                Assert.That(characters, Contains.Item(creature));
            }
            else
            {
                Assert.That(levelAdjustment, Is.Null, creature);
                Assert.That(data[DataIndexConstants.CreatureData.LevelAdjustment], Is.Empty, creature);
                Assert.That(characters, Does.Not.Contains(creature), creature);
            }

            //Valid Can Use Equipment
            Assert.That(data.Length - 1, Is.AtLeast(DataIndexConstants.CreatureData.CanUseEquipment), creature);
            Assert.That(canUseEquipment, Is.True.Or.False, creature);
            Assert.That(data[DataIndexConstants.CreatureData.CanUseEquipment], Is.EqualTo(bool.TrueString).Or.EqualTo(bool.FalseString), creature);

            //Valid Caster Level
            Assert.That(data.Length - 1, Is.AtLeast(DataIndexConstants.CreatureData.CasterLevel), creature);
            Assert.That(int.TryParse(data[DataIndexConstants.CreatureData.CasterLevel], out var actualCasterLevel), Is.True, creature);
            Assert.That(casterLevel, Is.Not.Negative, creature);
            Assert.That(actualCasterLevel, Is.Not.Negative, creature);

            //Valid Natural Armor
            Assert.That(data.Length - 1, Is.AtLeast(DataIndexConstants.CreatureData.NaturalArmor), creature);
            Assert.That(int.TryParse(data[DataIndexConstants.CreatureData.NaturalArmor], out var actualNaturalArmor), Is.True, creature);
            Assert.That(naturalArmor, Is.Not.Negative, creature);
            Assert.That(actualNaturalArmor, Is.Not.Negative, creature);

            //Valid Number Of Hands
            Assert.That(data.Length - 1, Is.AtLeast(DataIndexConstants.CreatureData.NumberOfHands), creature);
            Assert.That(int.TryParse(data[DataIndexConstants.CreatureData.NumberOfHands], out var actualNumberOfHands), Is.True, creature);
            Assert.That(numberOfHands, Is.Not.Negative, creature);
            Assert.That(actualNumberOfHands, Is.Not.Negative, creature);
        }

        [TestCase(CreatureConstants.Types.Animal, false)]
        [TestCase(CreatureConstants.Types.Fey, true)]
        [TestCase(CreatureConstants.Types.Giant, true)]
        [TestCase(CreatureConstants.Types.Humanoid, true)]
        [TestCase(CreatureConstants.Types.MonstrousHumanoid, true)]
        [TestCase(CreatureConstants.Types.Ooze, false)]
        [TestCase(CreatureConstants.Types.Vermin, false)]
        public void CreaturesOfTypeCanUseEquipment(string creatureType, bool useEquipment)
        {
            var creatures = collectionSelector.Explode(TableNameConstants.Collection.CreatureGroups, creatureType);
            var templates = CreatureConstants.Templates.GetAll();

            foreach (var creature in creatures.Except(templates))
            {
                Assert.That(table, Contains.Key(creature));

                var data = table[creature].ToArray();
                Assert.That(data.Length - 1, Is.AtLeast(DataIndexConstants.CreatureData.CanUseEquipment), creature);
                Assert.That(data[DataIndexConstants.CreatureData.CanUseEquipment], Is.EqualTo(useEquipment.ToString()), creature);
            }
        }
    }
}
