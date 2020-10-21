using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Tables;
using DnDGen.CreatureGen.Tests.Integration.TestData;
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

        [TestCase(CreatureConstants.Aasimar, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.OneHalf, 1, true, 1, 0, 2)]
        [TestCase(CreatureConstants.Aboleth, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Seven, 0, false, 16, 7, 0)]
        [TestCase(CreatureConstants.Achaierai, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Five, null, false, 16, 10, 0)]
        [TestCase(CreatureConstants.Allip, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Three, null, false, 0, 0, 0)]
        [TestCase(CreatureConstants.Androsphinx, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Nine, 5, false, 6, 13, 0)]
        [TestCase(CreatureConstants.Angel_AstralDeva, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Fourteen, 8, true, 12, 15, 2)]
        [TestCase(CreatureConstants.Angel_Planetar, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Sixteen, null, true, 17, 19, 2)]
        [TestCase(CreatureConstants.Angel_Solar, SizeConstants.Large, 10, 10, ChallengeRatingConstants.TwentyThree, null, true, 20, 21, 2)]
        [TestCase(CreatureConstants.AnimatedObject_Anvil_Colossal, SizeConstants.Colossal, 30, 20, ChallengeRatingConstants.Ten, null, false, 0, 12, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Anvil_Gargantuan, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.Seven, null, false, 0, 8, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Anvil_Huge, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Five, null, false, 0, 6, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Anvil_Large, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Three, null, false, 0, 5, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Anvil_Medium, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Two, null, false, 0, 4, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Anvil_Small, SizeConstants.Small, 5, 5, ChallengeRatingConstants.One, null, false, 0, 2, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Anvil_Tiny, SizeConstants.Tiny, 2.5, 0, ChallengeRatingConstants.OneHalf, null, false, 0, 0, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Block_Stone_Colossal, SizeConstants.Colossal, 30, 20, ChallengeRatingConstants.Ten, null, false, 0, 12, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Block_Stone_Gargantuan, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.Seven, null, false, 0, 8, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Block_Stone_Huge, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Five, null, false, 0, 6, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Block_Stone_Large, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Three, null, false, 0, 5, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Block_Stone_Medium, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Two, null, false, 0, 4, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Block_Stone_Small, SizeConstants.Small, 5, 5, ChallengeRatingConstants.One, null, false, 0, 2, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Block_Stone_Tiny, SizeConstants.Tiny, 2.5, 0, ChallengeRatingConstants.OneHalf, null, false, 0, 0, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Block_Wood_Colossal, SizeConstants.Colossal, 30, 20, ChallengeRatingConstants.Ten, null, false, 0, 12, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Block_Wood_Gargantuan, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.Seven, null, false, 0, 8, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Block_Wood_Huge, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Five, null, false, 0, 6, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Block_Wood_Large, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Three, null, false, 0, 5, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Block_Wood_Medium, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Two, null, false, 0, 4, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Block_Wood_Small, SizeConstants.Small, 5, 5, ChallengeRatingConstants.One, null, false, 0, 2, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Block_Wood_Tiny, SizeConstants.Tiny, 2.5, 0, ChallengeRatingConstants.OneHalf, null, false, 0, 0, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Box_Colossal, SizeConstants.Colossal, 30, 20, ChallengeRatingConstants.Ten, null, false, 0, 12, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Box_Gargantuan, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.Seven, null, false, 0, 8, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Box_Huge, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Five, null, false, 0, 6, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Box_Large, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Three, null, false, 0, 5, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Box_Medium, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Two, null, false, 0, 4, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Box_Small, SizeConstants.Small, 5, 5, ChallengeRatingConstants.One, null, false, 0, 2, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Box_Tiny, SizeConstants.Tiny, 2.5, 0, ChallengeRatingConstants.OneHalf, null, false, 0, 0, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Carpet_Colossal, SizeConstants.Colossal, 30, 20, ChallengeRatingConstants.Ten, null, false, 0, 12, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Carpet_Gargantuan, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.Seven, null, false, 0, 8, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Carpet_Huge, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Five, null, false, 0, 6, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Carpet_Large, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Three, null, false, 0, 5, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Carpet_Medium, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Two, null, false, 0, 4, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Carpet_Small, SizeConstants.Small, 5, 5, ChallengeRatingConstants.One, null, false, 0, 2, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Carpet_Tiny, SizeConstants.Tiny, 2.5, 0, ChallengeRatingConstants.OneHalf, null, false, 0, 0, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Carriage_Colossal, SizeConstants.Colossal, 30, 20, ChallengeRatingConstants.Ten, null, false, 0, 12, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Carriage_Gargantuan, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.Seven, null, false, 0, 8, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Carriage_Huge, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Five, null, false, 0, 6, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Carriage_Large, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Three, null, false, 0, 5, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Carriage_Medium, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Two, null, false, 0, 4, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Carriage_Small, SizeConstants.Small, 5, 5, ChallengeRatingConstants.One, null, false, 0, 2, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Carriage_Tiny, SizeConstants.Tiny, 2.5, 0, ChallengeRatingConstants.OneHalf, null, false, 0, 0, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Chain_Colossal, SizeConstants.Colossal, 30, 20, ChallengeRatingConstants.Ten, null, false, 0, 12, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Chain_Gargantuan, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.Seven, null, false, 0, 8, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Chain_Huge, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Five, null, false, 0, 6, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Chain_Large, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Three, null, false, 0, 5, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Chain_Medium, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Two, null, false, 0, 4, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Chain_Small, SizeConstants.Small, 5, 5, ChallengeRatingConstants.One, null, false, 0, 2, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Chain_Tiny, SizeConstants.Tiny, 2.5, 0, ChallengeRatingConstants.OneHalf, null, false, 0, 0, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Chair_Colossal, SizeConstants.Colossal, 30, 20, ChallengeRatingConstants.Ten, null, false, 0, 12, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Chair_Gargantuan, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.Seven, null, false, 0, 8, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Chair_Huge, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Five, null, false, 0, 6, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Chair_Large, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Three, null, false, 0, 5, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Chair_Medium, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Two, null, false, 0, 4, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Chair_Small, SizeConstants.Small, 5, 5, ChallengeRatingConstants.One, null, false, 0, 2, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Chair_Tiny, SizeConstants.Tiny, 2.5, 0, ChallengeRatingConstants.OneHalf, null, false, 0, 0, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Clothes_Colossal, SizeConstants.Colossal, 30, 20, ChallengeRatingConstants.Ten, null, false, 0, 12, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Clothes_Gargantuan, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.Seven, null, false, 0, 8, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Clothes_Huge, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Five, null, false, 0, 6, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Clothes_Large, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Three, null, false, 0, 5, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Clothes_Medium, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Two, null, false, 0, 4, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Clothes_Small, SizeConstants.Small, 5, 5, ChallengeRatingConstants.One, null, false, 0, 2, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Clothes_Tiny, SizeConstants.Tiny, 2.5, 0, ChallengeRatingConstants.OneHalf, null, false, 0, 0, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Ladder_Colossal, SizeConstants.Colossal, 30, 20, ChallengeRatingConstants.Ten, null, false, 0, 12, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Ladder_Gargantuan, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.Seven, null, false, 0, 8, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Ladder_Huge, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Five, null, false, 0, 6, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Ladder_Large, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Three, null, false, 0, 5, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Ladder_Medium, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Two, null, false, 0, 4, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Ladder_Small, SizeConstants.Small, 5, 5, ChallengeRatingConstants.One, null, false, 0, 2, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Ladder_Tiny, SizeConstants.Tiny, 2.5, 0, ChallengeRatingConstants.OneHalf, null, false, 0, 0, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Rope_Colossal, SizeConstants.Colossal, 30, 20, ChallengeRatingConstants.Ten, null, false, 0, 12, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Rope_Gargantuan, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.Seven, null, false, 0, 8, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Rope_Huge, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Five, null, false, 0, 6, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Rope_Large, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Three, null, false, 0, 5, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Rope_Medium, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Two, null, false, 0, 4, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Rope_Small, SizeConstants.Small, 5, 5, ChallengeRatingConstants.One, null, false, 0, 2, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Rope_Tiny, SizeConstants.Tiny, 2.5, 0, ChallengeRatingConstants.OneHalf, null, false, 0, 0, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Rug_Colossal, SizeConstants.Colossal, 30, 20, ChallengeRatingConstants.Ten, null, false, 0, 12, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Rug_Gargantuan, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.Seven, null, false, 0, 8, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Rug_Huge, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Five, null, false, 0, 6, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Rug_Large, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Three, null, false, 0, 5, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Rug_Medium, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Two, null, false, 0, 4, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Rug_Small, SizeConstants.Small, 5, 5, ChallengeRatingConstants.One, null, false, 0, 2, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Rug_Tiny, SizeConstants.Tiny, 2.5, 0, ChallengeRatingConstants.OneHalf, null, false, 0, 0, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Sled_Colossal, SizeConstants.Colossal, 30, 20, ChallengeRatingConstants.Ten, null, false, 0, 12, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Sled_Gargantuan, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.Seven, null, false, 0, 8, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Sled_Huge, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Five, null, false, 0, 6, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Sled_Large, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Three, null, false, 0, 5, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Sled_Medium, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Two, null, false, 0, 4, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Sled_Small, SizeConstants.Small, 5, 5, ChallengeRatingConstants.One, null, false, 0, 2, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Sled_Tiny, SizeConstants.Tiny, 2.5, 0, ChallengeRatingConstants.OneHalf, null, false, 0, 0, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Statue_Animal_Colossal, SizeConstants.Colossal, 30, 20, ChallengeRatingConstants.Ten, null, false, 0, 12, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Statue_Animal_Gargantuan, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.Seven, null, false, 0, 8, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Statue_Animal_Huge, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Five, null, false, 0, 6, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Statue_Animal_Large, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Three, null, false, 0, 5, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Statue_Animal_Medium, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Two, null, false, 0, 4, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Statue_Animal_Small, SizeConstants.Small, 5, 5, ChallengeRatingConstants.One, null, false, 0, 2, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Statue_Animal_Tiny, SizeConstants.Tiny, 2.5, 0, ChallengeRatingConstants.OneHalf, null, false, 0, 0, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Statue_Humanoid_Colossal, SizeConstants.Colossal, 30, 20, ChallengeRatingConstants.Ten, null, false, 0, 12, 2)]
        [TestCase(CreatureConstants.AnimatedObject_Statue_Humanoid_Gargantuan, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.Seven, null, false, 0, 8, 2)]
        [TestCase(CreatureConstants.AnimatedObject_Statue_Humanoid_Huge, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Five, null, false, 0, 6, 2)]
        [TestCase(CreatureConstants.AnimatedObject_Statue_Humanoid_Large, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Three, null, false, 0, 5, 2)]
        [TestCase(CreatureConstants.AnimatedObject_Statue_Humanoid_Medium, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Two, null, false, 0, 4, 2)]
        [TestCase(CreatureConstants.AnimatedObject_Statue_Humanoid_Small, SizeConstants.Small, 5, 5, ChallengeRatingConstants.One, null, false, 0, 2, 2)]
        [TestCase(CreatureConstants.AnimatedObject_Statue_Humanoid_Tiny, SizeConstants.Tiny, 2.5, 0, ChallengeRatingConstants.OneHalf, null, false, 0, 0, 2)]
        [TestCase(CreatureConstants.AnimatedObject_Stool_Colossal, SizeConstants.Colossal, 30, 20, ChallengeRatingConstants.Ten, null, false, 0, 12, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Stool_Gargantuan, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.Seven, null, false, 0, 8, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Stool_Huge, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Five, null, false, 0, 6, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Stool_Large, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Three, null, false, 0, 5, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Stool_Medium, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Two, null, false, 0, 4, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Stool_Small, SizeConstants.Small, 5, 5, ChallengeRatingConstants.One, null, false, 0, 2, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Stool_Tiny, SizeConstants.Tiny, 2.5, 0, ChallengeRatingConstants.OneHalf, null, false, 0, 0, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Table_Colossal, SizeConstants.Colossal, 30, 20, ChallengeRatingConstants.Ten, null, false, 0, 12, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Table_Gargantuan, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.Seven, null, false, 0, 8, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Table_Huge, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Five, null, false, 0, 6, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Table_Large, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Three, null, false, 0, 5, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Table_Medium, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Two, null, false, 0, 4, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Table_Small, SizeConstants.Small, 5, 5, ChallengeRatingConstants.One, null, false, 0, 2, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Table_Tiny, SizeConstants.Tiny, 2.5, 0, ChallengeRatingConstants.OneHalf, null, false, 0, 0, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Tapestry_Colossal, SizeConstants.Colossal, 30, 20, ChallengeRatingConstants.Ten, null, false, 0, 12, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Tapestry_Gargantuan, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.Seven, null, false, 0, 8, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Tapestry_Huge, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Five, null, false, 0, 6, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Tapestry_Large, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Three, null, false, 0, 5, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Tapestry_Medium, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Two, null, false, 0, 4, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Tapestry_Small, SizeConstants.Small, 5, 5, ChallengeRatingConstants.One, null, false, 0, 2, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Tapestry_Tiny, SizeConstants.Tiny, 2.5, 0, ChallengeRatingConstants.OneHalf, null, false, 0, 0, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Wagon_Colossal, SizeConstants.Colossal, 30, 20, ChallengeRatingConstants.Ten, null, false, 0, 12, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Wagon_Gargantuan, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.Seven, null, false, 0, 8, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Wagon_Huge, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Five, null, false, 0, 6, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Wagon_Large, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Three, null, false, 0, 5, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Wagon_Medium, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Two, null, false, 0, 4, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Wagon_Small, SizeConstants.Small, 5, 5, ChallengeRatingConstants.One, null, false, 0, 2, 0)]
        [TestCase(CreatureConstants.AnimatedObject_Wagon_Tiny, SizeConstants.Tiny, 2.5, 0, ChallengeRatingConstants.OneHalf, null, false, 0, 0, 0)]
        [TestCase(CreatureConstants.Ankheg, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Three, null, false, 0, 9, 0)]
        [TestCase(CreatureConstants.Annis, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Six, 0, true, 8, 10, 2)]
        [TestCase(CreatureConstants.Ant_Giant_Queen, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Two, null, false, 0, 9, 0)]
        [TestCase(CreatureConstants.Ant_Giant_Soldier, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Two, null, false, 0, 7, 0)]
        [TestCase(CreatureConstants.Ant_Giant_Worker, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.One, null, false, 0, 7, 0)]
        [TestCase(CreatureConstants.Ape, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Two, null, false, 0, 3, 0)]
        [TestCase(CreatureConstants.Ape_Dire, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Three, null, false, 0, 4, 0)]
        [TestCase(CreatureConstants.Aranea, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Four, 4, true, 3, 1, 0)]
        [TestCase(CreatureConstants.Arrowhawk_Adult, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Five, null, false, 0, 6, 0)]
        [TestCase(CreatureConstants.Arrowhawk_Elder, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Eight, null, false, 0, 8, 0)]
        [TestCase(CreatureConstants.Arrowhawk_Juvenile, SizeConstants.Small, 5, 5, ChallengeRatingConstants.Three, null, false, 0, 4, 0)]
        [TestCase(CreatureConstants.AssassinVine, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Three, null, false, 4, 6, 0)]
        [TestCase(CreatureConstants.Athach, SizeConstants.Huge, 15, 15, ChallengeRatingConstants.Eight, 5, true, 0, 8, 3)]
        [TestCase(CreatureConstants.Avoral, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Nine, null, true, 8, 8, 2)]
        [TestCase(CreatureConstants.Azer, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Two, 4, true, 0, 6, 2)]
        [TestCase(CreatureConstants.Babau, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Six, null, true, 7, 8, 0)]
        [TestCase(CreatureConstants.Baboon, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.OneHalf, null, false, 0, 1, 0)]
        [TestCase(CreatureConstants.Badger, SizeConstants.Small, 5, 5, ChallengeRatingConstants.OneHalf, null, false, 0, 1, 0)]
        [TestCase(CreatureConstants.Badger_Dire, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Two, null, false, 0, 3, 0)]
        [TestCase(CreatureConstants.Balor, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Twenty, null, true, 20, 19, 2)]
        [TestCase(CreatureConstants.BarbedDevil_Hamatula, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Eleven, null, true, 12, 13, 2)]
        [TestCase(CreatureConstants.Barghest, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Four, null, true, 0, 6, 2)]
        [TestCase(CreatureConstants.Barghest_Greater, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Five, null, true, 0, 9, 2)]
        [TestCase(CreatureConstants.Basilisk, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Five, null, false, 0, 7, 0)]
        [TestCase(CreatureConstants.Basilisk_Greater, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Twelve, null, false, 0, 9, 0)]
        [TestCase(CreatureConstants.Bat, SizeConstants.Diminutive, 1, 0, ChallengeRatingConstants.OneTenth, null, false, 0, 0, 0)]
        [TestCase(CreatureConstants.Bat_Dire, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Two, null, false, 0, 5, 0)]
        [TestCase(CreatureConstants.Bat_Swarm, SizeConstants.Diminutive, 10, 0, ChallengeRatingConstants.Two, null, false, 0, 0, 0)]
        [TestCase(CreatureConstants.Bear_Black, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Two, null, false, 0, 2, 0)]
        [TestCase(CreatureConstants.Bear_Brown, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Four, null, false, 0, 5, 0)]
        [TestCase(CreatureConstants.Bear_Dire, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Seven, null, false, 0, 7, 0)]
        [TestCase(CreatureConstants.Bear_Polar, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Four, null, false, 0, 5, 0)]
        [TestCase(CreatureConstants.BeardedDevil_Barbazu, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Five, 6, true, 12, 7, 2)]
        [TestCase(CreatureConstants.Bebilith, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Ten, null, false, 12, 13, 0)]
        [TestCase(CreatureConstants.Bee_Giant, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.One, null, false, 0, 2, 0)]
        [TestCase(CreatureConstants.Behir, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Eight, null, false, 0, 11, 0)]
        [TestCase(CreatureConstants.Beholder, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Thirteen, null, false, 13, 15, 0)]
        [TestCase(CreatureConstants.Beholder_Gauth, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Six, null, false, 8, 7, 0)]
        [TestCase(CreatureConstants.Belker, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Six, null, false, 0, 8, 0)]
        [TestCase(CreatureConstants.Bison, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Two, null, false, 0, 4, 0)]
        [TestCase(CreatureConstants.BlackPudding, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Seven, null, false, 0, 0, 0)]
        [TestCase(CreatureConstants.BlackPudding_Elder, SizeConstants.Gargantuan, 20, 20, ChallengeRatingConstants.Twelve, null, false, 0, 0, 0)]
        [TestCase(CreatureConstants.BlinkDog, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Two, 2, false, 8, 3, 0)]
        [TestCase(CreatureConstants.Boar, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Two, null, false, 0, 6, 0)]
        [TestCase(CreatureConstants.Boar_Dire, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Four, null, false, 0, 6, 0)]
        [TestCase(CreatureConstants.Bodak, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Eight, null, false, 0, 8, 2)]
        [TestCase(CreatureConstants.BombardierBeetle_Giant, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Two, null, false, 0, 6, 0)]
        [TestCase(CreatureConstants.BoneDevil_Osyluth, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Nine, null, true, 12, 11, 2)]
        [TestCase(CreatureConstants.Bralani, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Six, 5, true, 6, 6, 2)]
        [TestCase(CreatureConstants.Bugbear, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Two, 1, true, 0, 3, 2)]
        [TestCase(CreatureConstants.Bulette, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Seven, null, false, 0, 12, 0)]
        [TestCase(CreatureConstants.Camel_Bactrian, SizeConstants.Large, 10, 5, ChallengeRatingConstants.One, null, false, 0, 1, 0)]
        [TestCase(CreatureConstants.Camel_Dromedary, SizeConstants.Large, 10, 5, ChallengeRatingConstants.One, null, false, 0, 1, 0)]
        [TestCase(CreatureConstants.CarrionCrawler, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Four, null, false, 0, 6, 0)]
        [TestCase(CreatureConstants.Cat, SizeConstants.Tiny, 2.5, 0, ChallengeRatingConstants.OneFourth, null, false, 0, 0, 0)]
        [TestCase(CreatureConstants.Centaur, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Three, 2, true, 0, 3, 2)]
        [TestCase(CreatureConstants.Centipede_Monstrous_Colossal, SizeConstants.Colossal, 30, 20, ChallengeRatingConstants.Nine, null, false, 0, 16, 0)]
        [TestCase(CreatureConstants.Centipede_Monstrous_Gargantuan, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.Six, null, false, 0, 10, 0)]
        [TestCase(CreatureConstants.Centipede_Monstrous_Huge, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Two, null, false, 0, 6, 0)]
        [TestCase(CreatureConstants.Centipede_Monstrous_Large, SizeConstants.Large, 10, 5, ChallengeRatingConstants.One, null, false, 0, 3, 0)]
        [TestCase(CreatureConstants.Centipede_Monstrous_Medium, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.OneHalf, null, false, 0, 2, 0)]
        [TestCase(CreatureConstants.Centipede_Monstrous_Small, SizeConstants.Small, 5, 5, ChallengeRatingConstants.OneFourth, null, false, 0, 1, 0)]
        [TestCase(CreatureConstants.Centipede_Monstrous_Tiny, SizeConstants.Tiny, 2.5, 0, ChallengeRatingConstants.OneEighth, null, false, 0, 0, 0)]
        [TestCase(CreatureConstants.Centipede_Swarm, SizeConstants.Diminutive, 10, 0, ChallengeRatingConstants.Four, null, false, 0, 0, 0)]
        [TestCase(CreatureConstants.ChainDevil_Kyton, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Six, 6, true, 0, 8, 2)]
        [TestCase(CreatureConstants.ChaosBeast, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Seven, null, false, 0, 5, 0)]
        [TestCase(CreatureConstants.Cheetah, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Two, null, false, 0, 1, 0)]
        [TestCase(CreatureConstants.Chimera_Black, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Seven, 2, false, 0, 9, 0)]
        [TestCase(CreatureConstants.Chimera_Blue, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Seven, 2, false, 0, 9, 0)]
        [TestCase(CreatureConstants.Chimera_Green, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Seven, 2, false, 0, 9, 0)]
        [TestCase(CreatureConstants.Chimera_Red, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Seven, 2, false, 0, 9, 0)]
        [TestCase(CreatureConstants.Chimera_White, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Seven, 2, false, 0, 9, 0)]
        [TestCase(CreatureConstants.Choker, SizeConstants.Small, 5, 10, ChallengeRatingConstants.Two, null, false, 0, 4, 0)]
        [TestCase(CreatureConstants.Chuul, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Seven, null, false, 0, 10, 0)]
        [TestCase(CreatureConstants.Cloaker, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Five, null, false, 0, 7, 0)]
        [TestCase(CreatureConstants.Cockatrice, SizeConstants.Small, 5, 5, ChallengeRatingConstants.Three, null, false, 0, 0, 0)]
        [TestCase(CreatureConstants.Couatl, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Ten, 7, true, 9, 9, 0)]
        [TestCase(CreatureConstants.Criosphinx, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Seven, 3, false, 0, 11, 0)]
        [TestCase(CreatureConstants.Crocodile, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Two, null, false, 0, 4, 0)]
        [TestCase(CreatureConstants.Crocodile_Giant, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Four, null, false, 0, 7, 0)]
        [TestCase(CreatureConstants.Cryohydra_5Heads, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Six, null, false, 0, 6, 0)]
        [TestCase(CreatureConstants.Cryohydra_6Heads, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Seven, null, false, 0, 7, 0)]
        [TestCase(CreatureConstants.Cryohydra_7Heads, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Eight, null, false, 0, 8, 0)]
        [TestCase(CreatureConstants.Cryohydra_8Heads, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Nine, null, false, 0, 9, 0)]
        [TestCase(CreatureConstants.Cryohydra_9Heads, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Ten, null, false, 0, 10, 0)]
        [TestCase(CreatureConstants.Cryohydra_10Heads, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Eleven, null, false, 0, 11, 0)]
        [TestCase(CreatureConstants.Cryohydra_11Heads, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Twelve, null, false, 0, 12, 0)]
        [TestCase(CreatureConstants.Cryohydra_12Heads, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Thirteen, null, false, 0, 13, 0)]
        [TestCase(CreatureConstants.Darkmantle, SizeConstants.Small, 5, 5, ChallengeRatingConstants.One, null, false, 5, 6, 0)]
        [TestCase(CreatureConstants.Deinonychus, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Three, null, false, 0, 5, 0)]
        [TestCase(CreatureConstants.Delver, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Nine, null, false, 15, 15, 0)]
        [TestCase(CreatureConstants.Derro, SizeConstants.Small, 5, 5, ChallengeRatingConstants.Three, 0, true, 3, 2, 2)]
        [TestCase(CreatureConstants.Derro_Sane, SizeConstants.Small, 5, 5, ChallengeRatingConstants.Three, 2, true, 3, 2, 2)]
        [TestCase(CreatureConstants.Destrachan, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Eight, null, false, 0, 8, 0)]
        [TestCase(CreatureConstants.Devourer, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Eleven, null, false, 18, 15, 2)]
        [TestCase(CreatureConstants.Digester, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Six, null, false, 0, 5, 0)]
        [TestCase(CreatureConstants.DisplacerBeast, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Four, 4, false, 0, 5, 0)]
        [TestCase(CreatureConstants.DisplacerBeast_PackLord, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Twelve, null, false, 0, 8, 0)]
        [TestCase(CreatureConstants.Djinni, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Five, 6, true, 20, 3, 2)]
        [TestCase(CreatureConstants.Djinni_Noble, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Eight, 6, true, 20, 3, 2)]
        [TestCase(CreatureConstants.Dog, SizeConstants.Small, 5, 5, ChallengeRatingConstants.OneThird, null, false, 0, 1, 0)]
        [TestCase(CreatureConstants.Dog_Riding, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.One, null, false, 0, 4, 0)]
        [TestCase(CreatureConstants.Donkey, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.OneSixth, null, false, 0, 2, 0)]
        [TestCase(CreatureConstants.Doppelganger, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Three, 4, true, 18, 4, 2)]
        [TestCase(CreatureConstants.Dragon_Black_Wyrmling, SizeConstants.Tiny, 2.5, 0, ChallengeRatingConstants.Three, 3, false, 0, 3, 0)]
        [TestCase(CreatureConstants.Dragon_Black_VeryYoung, SizeConstants.Small, 5, 5, ChallengeRatingConstants.Four, 3, false, 0, 6, 0)]
        [TestCase(CreatureConstants.Dragon_Black_Young, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Five, 3, false, 0, 9, 0)]
        [TestCase(CreatureConstants.Dragon_Black_Juvenile, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Seven, 4, false, 0, 12, 0)]
        [TestCase(CreatureConstants.Dragon_Black_YoungAdult, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Nine, null, false, 1, 15, 0)]
        [TestCase(CreatureConstants.Dragon_Black_Adult, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Eleven, null, false, 3, 18, 0)]
        [TestCase(CreatureConstants.Dragon_Black_MatureAdult, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Fourteen, null, false, 5, 21, 0)]
        [TestCase(CreatureConstants.Dragon_Black_Old, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Sixteen, null, false, 7, 24, 0)]
        [TestCase(CreatureConstants.Dragon_Black_VeryOld, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Eighteen, null, false, 9, 27, 0)]
        [TestCase(CreatureConstants.Dragon_Black_Ancient, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Nineteen, null, false, 11, 30, 0)]
        [TestCase(CreatureConstants.Dragon_Black_Wyrm, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.Twenty, null, false, 13, 33, 0)]
        [TestCase(CreatureConstants.Dragon_Black_GreatWyrm, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.TwentyTwo, null, false, 15, 36, 0)]
        [TestCase(CreatureConstants.Dragon_Blue_Wyrmling, SizeConstants.Small, 5, 5, ChallengeRatingConstants.Three, 4, false, 0, 5, 0)]
        [TestCase(CreatureConstants.Dragon_Blue_VeryYoung, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Four, 4, false, 0, 8, 0)]
        [TestCase(CreatureConstants.Dragon_Blue_Young, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Six, 5, false, 0, 11, 0)]
        [TestCase(CreatureConstants.Dragon_Blue_Juvenile, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Eight, null, false, 1, 14, 0)]
        [TestCase(CreatureConstants.Dragon_Blue_YoungAdult, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Eleven, null, false, 3, 17, 0)]
        [TestCase(CreatureConstants.Dragon_Blue_Adult, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Fourteen, null, false, 5, 20, 0)]
        [TestCase(CreatureConstants.Dragon_Blue_MatureAdult, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Sixteen, null, false, 7, 23, 0)]
        [TestCase(CreatureConstants.Dragon_Blue_Old, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Eighteen, null, false, 9, 26, 0)]
        [TestCase(CreatureConstants.Dragon_Blue_VeryOld, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Nineteen, null, false, 11, 29, 0)]
        [TestCase(CreatureConstants.Dragon_Blue_Ancient, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.TwentyOne, null, false, 13, 32, 0)]
        [TestCase(CreatureConstants.Dragon_Blue_Wyrm, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.TwentyThree, null, false, 15, 35, 0)]
        [TestCase(CreatureConstants.Dragon_Blue_GreatWyrm, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.TwentyFive, null, false, 17, 38, 0)]
        [TestCase(CreatureConstants.Dragon_Brass_Wyrmling, SizeConstants.Tiny, 2.5, 0, ChallengeRatingConstants.Three, 2, false, 0, 3, 0)]
        [TestCase(CreatureConstants.Dragon_Brass_VeryYoung, SizeConstants.Small, 5, 5, ChallengeRatingConstants.Four, 3, false, 0, 6, 0)]
        [TestCase(CreatureConstants.Dragon_Brass_Young, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Six, 4, false, 1, 9, 0)]
        [TestCase(CreatureConstants.Dragon_Brass_Juvenile, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Eight, 4, false, 3, 12, 0)]
        [TestCase(CreatureConstants.Dragon_Brass_YoungAdult, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Ten, null, false, 5, 15, 0)]
        [TestCase(CreatureConstants.Dragon_Brass_Adult, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Twelve, null, false, 7, 18, 0)]
        [TestCase(CreatureConstants.Dragon_Brass_MatureAdult, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Fifteen, null, false, 9, 21, 0)]
        [TestCase(CreatureConstants.Dragon_Brass_Old, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Seventeen, null, false, 11, 24, 0)]
        [TestCase(CreatureConstants.Dragon_Brass_VeryOld, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Nineteen, null, false, 13, 27, 0)]
        [TestCase(CreatureConstants.Dragon_Brass_Ancient, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Twenty, null, false, 15, 30, 0)]
        [TestCase(CreatureConstants.Dragon_Brass_Wyrm, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.TwentyOne, null, false, 17, 33, 0)]
        [TestCase(CreatureConstants.Dragon_Brass_GreatWyrm, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.TwentyThree, null, false, 19, 36, 0)]
        [TestCase(CreatureConstants.Dragon_Bronze_Wyrmling, SizeConstants.Small, 5, 5, ChallengeRatingConstants.Three, 4, false, 0, 5, 0)]
        [TestCase(CreatureConstants.Dragon_Bronze_VeryYoung, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Five, 4, false, 0, 8, 0)]
        [TestCase(CreatureConstants.Dragon_Bronze_Young, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Seven, 6, true, 1, 11, 0)]
        [TestCase(CreatureConstants.Dragon_Bronze_Juvenile, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Nine, null, true, 3, 14, 0)]
        [TestCase(CreatureConstants.Dragon_Bronze_YoungAdult, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Twelve, null, true, 5, 17, 0)]
        [TestCase(CreatureConstants.Dragon_Bronze_Adult, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Fifteen, null, true, 7, 20, 0)]
        [TestCase(CreatureConstants.Dragon_Bronze_MatureAdult, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Seventeen, null, true, 9, 23, 0)]
        [TestCase(CreatureConstants.Dragon_Bronze_Old, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Nineteen, null, true, 11, 26, 0)]
        [TestCase(CreatureConstants.Dragon_Bronze_VeryOld, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Twenty, null, true, 13, 29, 0)]
        [TestCase(CreatureConstants.Dragon_Bronze_Ancient, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.TwentyTwo, null, true, 15, 32, 0)]
        [TestCase(CreatureConstants.Dragon_Bronze_Wyrm, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.TwentyThree, null, true, 17, 35, 0)]
        [TestCase(CreatureConstants.Dragon_Bronze_GreatWyrm, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.TwentyFive, null, true, 19, 38, 0)]
        [TestCase(CreatureConstants.Dragon_Copper_Wyrmling, SizeConstants.Tiny, 2.5, 0, ChallengeRatingConstants.Three, 2, false, 0, 4, 0)]
        [TestCase(CreatureConstants.Dragon_Copper_VeryYoung, SizeConstants.Small, 5, 5, ChallengeRatingConstants.Five, 3, false, 0, 7, 0)]
        [TestCase(CreatureConstants.Dragon_Copper_Young, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Seven, 4, false, 1, 10, 0)]
        [TestCase(CreatureConstants.Dragon_Copper_Juvenile, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Nine, 4, false, 3, 13, 0)]
        [TestCase(CreatureConstants.Dragon_Copper_YoungAdult, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Eleven, null, false, 5, 16, 0)]
        [TestCase(CreatureConstants.Dragon_Copper_Adult, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Fourteen, null, false, 7, 19, 0)]
        [TestCase(CreatureConstants.Dragon_Copper_MatureAdult, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Sixteen, null, false, 9, 22, 0)]
        [TestCase(CreatureConstants.Dragon_Copper_Old, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Nineteen, null, false, 11, 25, 0)]
        [TestCase(CreatureConstants.Dragon_Copper_VeryOld, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Twenty, null, false, 13, 28, 0)]
        [TestCase(CreatureConstants.Dragon_Copper_Ancient, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.TwentyTwo, null, false, 15, 31, 0)]
        [TestCase(CreatureConstants.Dragon_Copper_Wyrm, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.TwentyThree, null, false, 17, 34, 0)]
        [TestCase(CreatureConstants.Dragon_Copper_GreatWyrm, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.TwentyFive, null, false, 19, 37, 0)]
        [TestCase(CreatureConstants.Dragon_Gold_Wyrmling, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Five, 4, true, 0, 7, 0)]
        [TestCase(CreatureConstants.Dragon_Gold_VeryYoung, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Seven, 5, true, 0, 10, 0)]
        [TestCase(CreatureConstants.Dragon_Gold_Young, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Nine, 6, true, 1, 13, 0)]
        [TestCase(CreatureConstants.Dragon_Gold_Juvenile, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Eleven, null, true, 3, 16, 0)]
        [TestCase(CreatureConstants.Dragon_Gold_YoungAdult, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Fourteen, null, true, 5, 19, 0)]
        [TestCase(CreatureConstants.Dragon_Gold_Adult, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Sixteen, null, true, 7, 22, 0)]
        [TestCase(CreatureConstants.Dragon_Gold_MatureAdult, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Nineteen, null, true, 9, 25, 0)]
        [TestCase(CreatureConstants.Dragon_Gold_Old, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.TwentyOne, null, true, 11, 28, 0)]
        [TestCase(CreatureConstants.Dragon_Gold_VeryOld, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.TwentyTwo, null, true, 13, 31, 0)]
        [TestCase(CreatureConstants.Dragon_Gold_Ancient, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.TwentyFour, null, true, 15, 34, 0)]
        [TestCase(CreatureConstants.Dragon_Gold_Wyrm, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.TwentyFive, null, true, 17, 37, 0)]
        [TestCase(CreatureConstants.Dragon_Gold_GreatWyrm, SizeConstants.Colossal, 30, 20, ChallengeRatingConstants.TwentySeven, null, true, 19, 40, 0)]
        [TestCase(CreatureConstants.Dragon_Green_Wyrmling, SizeConstants.Small, 5, 5, ChallengeRatingConstants.Three, 5, false, 0, 4, 0)]
        [TestCase(CreatureConstants.Dragon_Green_VeryYoung, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Four, 5, false, 0, 7, 0)]
        [TestCase(CreatureConstants.Dragon_Green_Young, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Five, 5, false, 0, 10, 0)]
        [TestCase(CreatureConstants.Dragon_Green_Juvenile, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Eight, 6, false, 1, 13, 0)]
        [TestCase(CreatureConstants.Dragon_Green_YoungAdult, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Eleven, null, false, 3, 16, 0)]
        [TestCase(CreatureConstants.Dragon_Green_Adult, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Thirteen, null, false, 5, 19, 0)]
        [TestCase(CreatureConstants.Dragon_Green_MatureAdult, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Sixteen, null, false, 7, 22, 0)]
        [TestCase(CreatureConstants.Dragon_Green_Old, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Eighteen, null, false, 9, 25, 0)]
        [TestCase(CreatureConstants.Dragon_Green_VeryOld, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Nineteen, null, false, 11, 28, 0)]
        [TestCase(CreatureConstants.Dragon_Green_Ancient, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.TwentyOne, null, false, 13, 31, 0)]
        [TestCase(CreatureConstants.Dragon_Green_Wyrm, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.TwentyTwo, null, false, 15, 34, 0)]
        [TestCase(CreatureConstants.Dragon_Green_GreatWyrm, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.TwentyFour, null, false, 17, 37, 0)]
        [TestCase(CreatureConstants.Dragon_Red_Wyrmling, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Four, 4, false, 0, 6, 0)]
        [TestCase(CreatureConstants.Dragon_Red_VeryYoung, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Five, 5, false, 0, 9, 0)]
        [TestCase(CreatureConstants.Dragon_Red_Young, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Seven, 6, false, 1, 12, 0)]
        [TestCase(CreatureConstants.Dragon_Red_Juvenile, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Ten, null, false, 3, 15, 0)]
        [TestCase(CreatureConstants.Dragon_Red_YoungAdult, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Thirteen, null, false, 5, 18, 0)]
        [TestCase(CreatureConstants.Dragon_Red_Adult, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Fifteen, null, false, 7, 21, 0)]
        [TestCase(CreatureConstants.Dragon_Red_MatureAdult, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Eighteen, null, false, 9, 24, 0)]
        [TestCase(CreatureConstants.Dragon_Red_Old, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.Twenty, null, false, 11, 27, 0)]
        [TestCase(CreatureConstants.Dragon_Red_VeryOld, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.TwentyOne, null, false, 13, 30, 0)]
        [TestCase(CreatureConstants.Dragon_Red_Ancient, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.TwentyThree, null, false, 15, 33, 0)]
        [TestCase(CreatureConstants.Dragon_Red_Wyrm, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.TwentyFour, null, false, 17, 36, 0)]
        [TestCase(CreatureConstants.Dragon_Red_GreatWyrm, SizeConstants.Colossal, 30, 20, ChallengeRatingConstants.TwentySix, null, false, 19, 39, 0)]
        [TestCase(CreatureConstants.Dragon_Silver_Wyrmling, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Four, 4, true, 0, 6, 0)]
        [TestCase(CreatureConstants.Dragon_Silver_VeryYoung, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Five, 4, true, 0, 9, 0)]
        [TestCase(CreatureConstants.Dragon_Silver_Young, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Seven, 5, true, 1, 12, 0)]
        [TestCase(CreatureConstants.Dragon_Silver_Juvenile, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Ten, null, true, 3, 15, 0)]
        [TestCase(CreatureConstants.Dragon_Silver_YoungAdult, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Thirteen, null, true, 5, 18, 0)]
        [TestCase(CreatureConstants.Dragon_Silver_Adult, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Fifteen, null, true, 7, 21, 0)]
        [TestCase(CreatureConstants.Dragon_Silver_MatureAdult, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Eighteen, null, true, 9, 24, 0)]
        [TestCase(CreatureConstants.Dragon_Silver_Old, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.Twenty, null, true, 11, 27, 0)]
        [TestCase(CreatureConstants.Dragon_Silver_VeryOld, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.TwentyOne, null, true, 13, 30, 0)]
        [TestCase(CreatureConstants.Dragon_Silver_Ancient, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.TwentyThree, null, true, 15, 33, 0)]
        [TestCase(CreatureConstants.Dragon_Silver_Wyrm, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.TwentyFour, null, true, 17, 36, 0)]
        [TestCase(CreatureConstants.Dragon_Silver_GreatWyrm, SizeConstants.Colossal, 30, 20, ChallengeRatingConstants.TwentySix, null, true, 19, 39, 0)]
        [TestCase(CreatureConstants.Dragon_White_Wyrmling, SizeConstants.Tiny, 2.5, 0, ChallengeRatingConstants.Two, 2, false, 0, 2, 0)]
        [TestCase(CreatureConstants.Dragon_White_VeryYoung, SizeConstants.Small, 5, 5, ChallengeRatingConstants.Three, 3, false, 0, 5, 0)]
        [TestCase(CreatureConstants.Dragon_White_Young, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Four, 3, false, 0, 8, 0)]
        [TestCase(CreatureConstants.Dragon_White_Juvenile, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Six, 5, false, 0, 11, 0)]
        [TestCase(CreatureConstants.Dragon_White_YoungAdult, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Eight, null, false, 0, 14, 0)]
        [TestCase(CreatureConstants.Dragon_White_Adult, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Ten, null, false, 1, 17, 0)]
        [TestCase(CreatureConstants.Dragon_White_MatureAdult, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Twelve, null, false, 3, 20, 0)]
        [TestCase(CreatureConstants.Dragon_White_Old, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Fifteen, null, false, 5, 23, 0)]
        [TestCase(CreatureConstants.Dragon_White_VeryOld, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Seventeen, null, false, 7, 26, 0)]
        [TestCase(CreatureConstants.Dragon_White_Ancient, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Eighteen, null, false, 9, 29, 0)]
        [TestCase(CreatureConstants.Dragon_White_Wyrm, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.Nineteen, null, false, 11, 32, 0)]
        [TestCase(CreatureConstants.Dragon_White_GreatWyrm, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.TwentyOne, null, false, 13, 35, 0)]
        [TestCase(CreatureConstants.DragonTurtle, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Nine, null, false, 0, 17, 0)]
        [TestCase(CreatureConstants.Dragonne, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Seven, 4, false, 0, 7, 0)]
        [TestCase(CreatureConstants.Dretch, SizeConstants.Small, 5, 5, ChallengeRatingConstants.Two, 2, true, 2, 5, 2)]
        [TestCase(CreatureConstants.Drider, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Seven, 4, true, 6, 6, 2)]
        [TestCase(CreatureConstants.Dryad, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Three, 0, true, 6, 3, 2)]
        [TestCase(CreatureConstants.Dwarf_Deep, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.OneHalf, 0, true, 0, 0, 2)]
        [TestCase(CreatureConstants.Dwarf_Duergar, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.One, 1, true, 0, 0, 2)]
        [TestCase(CreatureConstants.Dwarf_Hill, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.OneHalf, 0, true, 0, 0, 2)]
        [TestCase(CreatureConstants.Dwarf_Mountain, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.OneHalf, 0, true, 0, 0, 2)]
        [TestCase(CreatureConstants.Eagle, SizeConstants.Small, 5, 5, ChallengeRatingConstants.OneHalf, null, false, 0, 1, 0)]
        [TestCase(CreatureConstants.Eagle_Giant, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Three, 2, false, 0, 3, 0)]
        [TestCase(CreatureConstants.Efreeti, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Eight, null, true, 12, 6, 2)]
        [TestCase(CreatureConstants.Elasmosaurus, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Seven, null, false, 0, 3, 0)]
        [TestCase(CreatureConstants.Elemental_Air_Elder, SizeConstants.Huge, 15, 15, ChallengeRatingConstants.Eleven, null, false, 0, 8, 0)]
        [TestCase(CreatureConstants.Elemental_Air_Greater, SizeConstants.Huge, 15, 15, ChallengeRatingConstants.Nine, null, false, 0, 8, 0)]
        [TestCase(CreatureConstants.Elemental_Air_Huge, SizeConstants.Huge, 15, 15, ChallengeRatingConstants.Seven, null, false, 0, 4, 0)]
        [TestCase(CreatureConstants.Elemental_Air_Large, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Five, null, false, 0, 4, 0)]
        [TestCase(CreatureConstants.Elemental_Air_Medium, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Three, null, false, 0, 3, 0)]
        [TestCase(CreatureConstants.Elemental_Air_Small, SizeConstants.Small, 5, 5, ChallengeRatingConstants.One, null, false, 0, 3, 0)]
        [TestCase(CreatureConstants.Elemental_Earth_Elder, SizeConstants.Huge, 15, 15, ChallengeRatingConstants.Eleven, null, false, 0, 15, 0)]
        [TestCase(CreatureConstants.Elemental_Earth_Greater, SizeConstants.Huge, 15, 15, ChallengeRatingConstants.Nine, null, false, 0, 13, 0)]
        [TestCase(CreatureConstants.Elemental_Earth_Huge, SizeConstants.Huge, 15, 15, ChallengeRatingConstants.Seven, null, false, 0, 11, 0)]
        [TestCase(CreatureConstants.Elemental_Earth_Large, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Five, null, false, 0, 10, 0)]
        [TestCase(CreatureConstants.Elemental_Earth_Medium, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Three, null, false, 0, 9, 0)]
        [TestCase(CreatureConstants.Elemental_Earth_Small, SizeConstants.Small, 5, 5, ChallengeRatingConstants.One, null, false, 0, 7, 0)]
        [TestCase(CreatureConstants.Elemental_Fire_Elder, SizeConstants.Huge, 15, 15, ChallengeRatingConstants.Eleven, null, false, 0, 8, 0)]
        [TestCase(CreatureConstants.Elemental_Fire_Greater, SizeConstants.Huge, 15, 15, ChallengeRatingConstants.Nine, null, false, 0, 8, 0)]
        [TestCase(CreatureConstants.Elemental_Fire_Huge, SizeConstants.Huge, 15, 15, ChallengeRatingConstants.Seven, null, false, 0, 4, 0)]
        [TestCase(CreatureConstants.Elemental_Fire_Large, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Five, null, false, 0, 4, 0)]
        [TestCase(CreatureConstants.Elemental_Fire_Medium, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Three, null, false, 0, 3, 0)]
        [TestCase(CreatureConstants.Elemental_Fire_Small, SizeConstants.Small, 5, 5, ChallengeRatingConstants.One, null, false, 0, 3, 0)]
        [TestCase(CreatureConstants.Elemental_Water_Elder, SizeConstants.Huge, 15, 15, ChallengeRatingConstants.Eleven, null, false, 0, 9, 0)]
        [TestCase(CreatureConstants.Elemental_Water_Greater, SizeConstants.Huge, 15, 15, ChallengeRatingConstants.Nine, null, false, 0, 9, 0)]
        [TestCase(CreatureConstants.Elemental_Water_Huge, SizeConstants.Huge, 15, 15, ChallengeRatingConstants.Seven, null, false, 0, 9, 0)]
        [TestCase(CreatureConstants.Elemental_Water_Large, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Five, null, false, 0, 9, 0)]
        [TestCase(CreatureConstants.Elemental_Water_Medium, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Three, null, false, 0, 8, 0)]
        [TestCase(CreatureConstants.Elemental_Water_Small, SizeConstants.Small, 5, 5, ChallengeRatingConstants.One, null, false, 0, 6, 0)]
        [TestCase(CreatureConstants.Elephant, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Seven, null, false, 0, 7, 0)]
        [TestCase(CreatureConstants.Elf_Aquatic, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.OneHalf, 0, true, 0, 0, 2)]
        [TestCase(CreatureConstants.Elf_Drow, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.One, 2, true, 1, 0, 2)]
        [TestCase(CreatureConstants.Elf_Gray, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.OneHalf, 0, true, 0, 0, 2)]
        [TestCase(CreatureConstants.Elf_Half, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.OneHalf, 0, true, 0, 0, 2)]
        [TestCase(CreatureConstants.Elf_High, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.OneHalf, 0, true, 0, 0, 2)]
        [TestCase(CreatureConstants.Elf_Wild, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.OneHalf, 0, true, 0, 0, 2)]
        [TestCase(CreatureConstants.Elf_Wood, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.OneHalf, 0, true, 0, 0, 2)]
        [TestCase(CreatureConstants.Erinyes, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Eight, 7, true, 12, 8, 2)]
        [TestCase(CreatureConstants.EtherealFilcher, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Three, null, false, 5, 3, 4)]
        [TestCase(CreatureConstants.EtherealMarauder, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Three, null, false, 15, 3, 0)]
        [TestCase(CreatureConstants.Ettercap, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Three, 4, false, 0, 1, 0)]
        [TestCase(CreatureConstants.Ettin, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Six, 5, true, 0, 7, 2)]
        [TestCase(CreatureConstants.FireBeetle_Giant, SizeConstants.Small, 5, 5, ChallengeRatingConstants.OneThird, null, false, 0, 5, 0)]
        [TestCase(CreatureConstants.FormianMyrmarch, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Ten, null, true, 12, 15, 2)]
        [TestCase(CreatureConstants.FormianQueen, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Seventeen, null, true, 17, 14, 2)]
        [TestCase(CreatureConstants.FormianTaskmaster, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Seven, null, true, 10, 6, 2)]
        [TestCase(CreatureConstants.FormianWarrior, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Three, null, true, 0, 5, 2)]
        [TestCase(CreatureConstants.FormianWorker, SizeConstants.Small, 5, 5, ChallengeRatingConstants.OneHalf, null, true, 0, 4, 2)]
        [TestCase(CreatureConstants.FrostWorm, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Twelve, null, false, 0, 10, 0)]
        [TestCase(CreatureConstants.Gargoyle, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Four, 5, true, 0, 4, 2)]
        [TestCase(CreatureConstants.Gargoyle_Kapoacinth, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Four, 5, true, 0, 4, 2)]
        [TestCase(CreatureConstants.GelatinousCube, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Three, null, false, 0, 0, 0)]
        [TestCase(CreatureConstants.Ghaele, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Thirteen, null, true, 12, 14, 2)]
        [TestCase(CreatureConstants.Ghoul, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.One, null, false, 0, 2, 2)]
        [TestCase(CreatureConstants.Ghoul_Ghast, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Three, null, false, 0, 4, 2)]
        [TestCase(CreatureConstants.Ghoul_Lacedon, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.One, null, false, 0, 2, 2)]
        [TestCase(CreatureConstants.Giant_Cloud, SizeConstants.Huge, 15, 15, ChallengeRatingConstants.Eleven, 0, true, 15, 12, 2)]
        [TestCase(CreatureConstants.Giant_Fire, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Ten, 4, true, 0, 8, 2)]
        [TestCase(CreatureConstants.Giant_Frost, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Nine, 4, true, 0, 9, 2)]
        [TestCase(CreatureConstants.Giant_Hill, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Seven, 4, true, 0, 9, 2)]
        [TestCase(CreatureConstants.Giant_Stone, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Eight, 4, true, 0, 11, 2)]
        [TestCase(CreatureConstants.Giant_Stone_Elder, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Nine, 6, true, 10, 11, 2)]
        [TestCase(CreatureConstants.Giant_Storm, SizeConstants.Huge, 15, 15, ChallengeRatingConstants.Thirteen, 0, true, 20, 12, 2)]
        [TestCase(CreatureConstants.GibberingMouther, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Five, null, false, 0, 8, 0)]
        [TestCase(CreatureConstants.Girallon, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Six, null, false, 0, 4, 4)]
        [TestCase(CreatureConstants.Githyanki, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.One, 2, true, 1, 0, 2)]
        [TestCase(CreatureConstants.Githzerai, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.One, 2, true, 1, 0, 2)]
        [TestCase(CreatureConstants.Glabrezu, SizeConstants.Huge, 15, 15, ChallengeRatingConstants.Thirteen, null, true, 14, 19, 2)]
        [TestCase(CreatureConstants.Gnoll, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.One, 1, true, 0, 1, 2)]
        [TestCase(CreatureConstants.Gnome_Forest, SizeConstants.Small, 5, 5, ChallengeRatingConstants.OneHalf, 0, true, 0, 0, 2)]
        [TestCase(CreatureConstants.Gnome_Rock, SizeConstants.Small, 5, 5, ChallengeRatingConstants.OneHalf, 0, true, 0, 0, 2)]
        [TestCase(CreatureConstants.Gnome_Svirfneblin, SizeConstants.Small, 5, 5, ChallengeRatingConstants.One, 3, true, 1, 0, 2)]
        [TestCase(CreatureConstants.Goblin, SizeConstants.Small, 5, 5, ChallengeRatingConstants.OneThird, 0, true, 0, 0, 2)]
        [TestCase(CreatureConstants.Golem_Clay, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Ten, null, false, 0, 14, 2)]
        [TestCase(CreatureConstants.Golem_Flesh, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Seven, null, false, 0, 10, 2)]
        [TestCase(CreatureConstants.Golem_Iron, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Thirteen, null, false, 0, 22, 2)]
        [TestCase(CreatureConstants.Golem_Stone, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Eleven, null, false, 0, 18, 2)]
        [TestCase(CreatureConstants.Golem_Stone_Greater, SizeConstants.Huge, 15, 15, ChallengeRatingConstants.Sixteen, null, false, 0, 21, 2)]
        [TestCase(CreatureConstants.Gorgon, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Eight, null, false, 0, 11, 0)]
        [TestCase(CreatureConstants.GrayOoze, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Four, null, false, 0, 0, 0)]
        [TestCase(CreatureConstants.GrayRender, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Eight, 5, false, 0, 10, 2)]
        [TestCase(CreatureConstants.GreenHag, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Five, 0, true, 9, 11, 2)]
        [TestCase(CreatureConstants.Grick, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Three, null, false, 0, 4, 0)]
        [TestCase(CreatureConstants.Grig, SizeConstants.Tiny, 2.5, 0, ChallengeRatingConstants.One, 3, true, 9, 2, 2)]
        [TestCase(CreatureConstants.Grig_WithFiddle, SizeConstants.Tiny, 2.5, 0, ChallengeRatingConstants.One, 3, true, 9, 2, 2)]
        [TestCase(CreatureConstants.Griffon, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Four, 3, false, 0, 6, 0)]
        [TestCase(CreatureConstants.Grimlock, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.One, 2, true, 0, 4, 2)]
        [TestCase(CreatureConstants.Gynosphinx, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Eight, 4, false, 14, 11, 0)]
        [TestCase(CreatureConstants.Halfling_Deep, SizeConstants.Small, 5, 5, ChallengeRatingConstants.OneHalf, 0, true, 0, 0, 2)]
        [TestCase(CreatureConstants.Halfling_Lightfoot, SizeConstants.Small, 5, 5, ChallengeRatingConstants.OneHalf, 0, true, 0, 0, 2)]
        [TestCase(CreatureConstants.Halfling_Tallfellow, SizeConstants.Small, 5, 5, ChallengeRatingConstants.OneHalf, 0, true, 0, 0, 2)]
        [TestCase(CreatureConstants.Harpy, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Four, 3, true, 0, 1, 2)]
        [TestCase(CreatureConstants.Hawk, SizeConstants.Tiny, 2.5, 0, ChallengeRatingConstants.OneThird, null, false, 0, 2, 0)]
        [TestCase(CreatureConstants.Hellcat_Bezekira, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Seven, null, false, 0, 7, 0)]
        [TestCase(CreatureConstants.Hellwasp_Swarm, SizeConstants.Diminutive, 10, 0, ChallengeRatingConstants.Eight, null, false, 0, 0, 0)]
        [TestCase(CreatureConstants.HellHound, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Three, 3, false, 0, 5, 0)]
        [TestCase(CreatureConstants.HellHound_NessianWarhound, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Nine, 4, false, 0, 7, 0)]
        [TestCase(CreatureConstants.Hezrou, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Eleven, 9, true, 13, 14, 2)]
        [TestCase(CreatureConstants.Hieracosphinx, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Five, 3, false, 0, 8, 0)]
        [TestCase(CreatureConstants.Hippogriff, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Two, null, false, 0, 4, 0)]
        [TestCase(CreatureConstants.Hobgoblin, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.OneHalf, 1, true, 0, 0, 2)]
        [TestCase(CreatureConstants.Homunculus, SizeConstants.Tiny, 2.5, 0, ChallengeRatingConstants.One, null, false, 0, 0, 0)]
        [TestCase(CreatureConstants.HornedDevil_Cornugon, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Sixteen, null, true, 15, 19, 2)]
        [TestCase(CreatureConstants.Horse_Heavy, SizeConstants.Large, 10, 5, ChallengeRatingConstants.One, null, false, 0, 3, 0)]
        [TestCase(CreatureConstants.Horse_Heavy_War, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Two, null, false, 0, 4, 0)]
        [TestCase(CreatureConstants.Horse_Light, SizeConstants.Large, 10, 5, ChallengeRatingConstants.One, null, false, 0, 3, 0)]
        [TestCase(CreatureConstants.Horse_Light_War, SizeConstants.Large, 10, 5, ChallengeRatingConstants.One, null, false, 0, 4, 0)]
        [TestCase(CreatureConstants.HoundArchon, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Four, 5, true, 6, 0, 2)]
        [TestCase(CreatureConstants.Howler, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Three, 3, false, 0, 5, 0)]
        [TestCase(CreatureConstants.Human, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.OneHalf, 0, true, 0, 0, 2)]
        [TestCase(CreatureConstants.Hydra_5Heads, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Four, null, false, 0, 6, 0)]
        [TestCase(CreatureConstants.Hydra_6Heads, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Five, null, false, 0, 7, 0)]
        [TestCase(CreatureConstants.Hydra_7Heads, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Six, null, false, 0, 8, 0)]
        [TestCase(CreatureConstants.Hydra_8Heads, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Seven, null, false, 0, 9, 0)]
        [TestCase(CreatureConstants.Hydra_9Heads, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Eight, null, false, 0, 10, 0)]
        [TestCase(CreatureConstants.Hydra_10Heads, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Nine, null, false, 0, 11, 0)]
        [TestCase(CreatureConstants.Hydra_11Heads, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Ten, null, false, 0, 12, 0)]
        [TestCase(CreatureConstants.Hydra_12Heads, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Eleven, null, false, 0, 13, 0)]
        [TestCase(CreatureConstants.Hyena, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.One, null, false, 0, 2, 0)]
        [TestCase(CreatureConstants.IceDevil_Gelugon, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Thirteen, null, true, 13, 18, 2)]
        [TestCase(CreatureConstants.Imp, SizeConstants.Tiny, 2.5, 0, ChallengeRatingConstants.Two, null, false, 6, 5, 0)]
        [TestCase(CreatureConstants.InvisibleStalker, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Seven, null, false, 0, 4, 0)]
        [TestCase(CreatureConstants.Janni, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Four, 5, true, 12, 1, 2)]
        [TestCase(CreatureConstants.Kobold, SizeConstants.Small, 5, 5, ChallengeRatingConstants.OneFourth, 0, true, 0, 1, 2)]
        [TestCase(CreatureConstants.Kolyarut, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Twelve, null, true, 13, 10, 2)]
        [TestCase(CreatureConstants.Kraken, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.Twelve, null, false, 9, 14, 0)]
        [TestCase(CreatureConstants.Krenshar, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.One, 2, false, 3, 3, 0)]
        [TestCase(CreatureConstants.KuoToa, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Two, 3, true, 0, 6, 2)]
        [TestCase(CreatureConstants.Lamia, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Six, 4, true, 9, 7, 2)]
        [TestCase(CreatureConstants.Lammasu, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Eight, 5, false, 7, 10, 0)]
        [TestCase(CreatureConstants.LanternArchon, SizeConstants.Small, 5, 5, ChallengeRatingConstants.Two, null, false, 3, 4, 0)]
        [TestCase(CreatureConstants.Lemure, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.One, null, false, 0, 4, 0)]
        [TestCase(CreatureConstants.Leonal, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Twelve, null, false, 10, 14, 2)]
        [TestCase(CreatureConstants.Leopard, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Two, null, false, 0, 1, 0)]
        [TestCase(CreatureConstants.Lillend, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Seven, 6, true, 10, 5, 2)]
        [TestCase(CreatureConstants.Lion, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Three, null, false, 0, 3, 0)]
        [TestCase(CreatureConstants.Lion_Dire, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Five, null, false, 0, 4, 0)]
        [TestCase(CreatureConstants.Lizard, SizeConstants.Tiny, 2.5, 0, ChallengeRatingConstants.OneSixth, null, false, 0, 0, 0)]
        [TestCase(CreatureConstants.Lizard_Monitor, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Two, null, false, 0, 3, 0)]
        [TestCase(CreatureConstants.Lizardfolk, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.One, 1, true, 0, 5, 2)]
        [TestCase(CreatureConstants.Locathah, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.OneHalf, 1, true, 0, 3, 2)]
        [TestCase(CreatureConstants.Locust_Swarm, SizeConstants.Diminutive, 10, 0, ChallengeRatingConstants.Three, null, false, 0, 0, 0)]
        [TestCase(CreatureConstants.Magmin, SizeConstants.Small, 5, 5, ChallengeRatingConstants.Three, null, false, 0, 6, 0)]
        [TestCase(CreatureConstants.MantaRay, SizeConstants.Large, 10, 5, ChallengeRatingConstants.One, null, false, 0, 3, 0)]
        [TestCase(CreatureConstants.Manticore, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Five, 3, false, 0, 6, 0)]
        [TestCase(CreatureConstants.Marilith, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Seventeen, null, true, 16, 16, 6)]
        [TestCase(CreatureConstants.Marut, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Fifteen, null, false, 14, 16, 2)]
        [TestCase(CreatureConstants.Medusa, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Seven, 0, true, 0, 3, 2)]
        [TestCase(CreatureConstants.Megaraptor, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Six, null, false, 0, 6, 0)]
        [TestCase(CreatureConstants.Mephit_Air, SizeConstants.Small, 5, 5, ChallengeRatingConstants.Three, 3, true, 3, 3, 2)]
        [TestCase(CreatureConstants.Mephit_Dust, SizeConstants.Small, 5, 5, ChallengeRatingConstants.Three, 3, true, 3, 3, 2)]
        [TestCase(CreatureConstants.Mephit_Earth, SizeConstants.Small, 5, 5, ChallengeRatingConstants.Three, 3, true, 6, 6, 2)]
        [TestCase(CreatureConstants.Mephit_Fire, SizeConstants.Small, 5, 5, ChallengeRatingConstants.Three, 3, true, 3, 4, 2)]
        [TestCase(CreatureConstants.Mephit_Ice, SizeConstants.Small, 5, 5, ChallengeRatingConstants.Three, 3, true, 3, 4, 2)]
        [TestCase(CreatureConstants.Mephit_Magma, SizeConstants.Small, 5, 5, ChallengeRatingConstants.Three, 3, true, 6, 4, 2)]
        [TestCase(CreatureConstants.Mephit_Ooze, SizeConstants.Small, 5, 5, ChallengeRatingConstants.Three, 3, true, 3, 5, 2)]
        [TestCase(CreatureConstants.Mephit_Salt, SizeConstants.Small, 5, 5, ChallengeRatingConstants.Three, 3, true, 3, 6, 2)]
        [TestCase(CreatureConstants.Mephit_Steam, SizeConstants.Small, 5, 5, ChallengeRatingConstants.Three, 3, true, 3, 4, 2)]
        [TestCase(CreatureConstants.Mephit_Water, SizeConstants.Small, 5, 5, ChallengeRatingConstants.Three, 3, true, 3, 5, 2)]
        [TestCase(CreatureConstants.Merfolk, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.OneHalf, 1, true, 0, 0, 2)]
        [TestCase(CreatureConstants.Mimic, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Four, null, false, 0, 5, 0)]
        [TestCase(CreatureConstants.MindFlayer, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Eight, 7, true, 8, 3, 2)]
        [TestCase(CreatureConstants.Minotaur, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Four, 2, true, 0, 5, 2)]
        [TestCase(CreatureConstants.Mohrg, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Eight, null, false, 0, 9, 0)]
        [TestCase(CreatureConstants.Monkey, SizeConstants.Tiny, 2.5, 0, ChallengeRatingConstants.OneSixth, null, false, 0, 0, 0)]
        [TestCase(CreatureConstants.Mule, SizeConstants.Large, 10, 5, ChallengeRatingConstants.One, null, false, 0, 3, 0)]
        [TestCase(CreatureConstants.Mummy, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Five, 0, true, 0, 10, 2)]
        [TestCase(CreatureConstants.Naga_Dark, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Eight, null, false, 7, 3, 0)]
        [TestCase(CreatureConstants.Naga_Guardian, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Ten, null, false, 9, 7, 0)]
        [TestCase(CreatureConstants.Naga_Spirit, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Nine, null, false, 7, 6, 0)]
        [TestCase(CreatureConstants.Naga_Water, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Seven, null, false, 7, 5, 0)]
        [TestCase(CreatureConstants.Nalfeshnee, SizeConstants.Huge, 15, 15, ChallengeRatingConstants.Fourteen, null, true, 12, 18, 2)]
        [TestCase(CreatureConstants.NightHag, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Nine, null, true, 8, 11, 2)]
        [TestCase(CreatureConstants.Nightcrawler, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.Eighteen, null, false, 25, 29, 0)]
        [TestCase(CreatureConstants.Nightmare, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Five, 4, false, 20, 13, 0)]
        [TestCase(CreatureConstants.Nightmare_Cauchemar, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Eleven, 4, false, 20, 16, 0)]
        [TestCase(CreatureConstants.Nightwalker, SizeConstants.Huge, 15, 15, ChallengeRatingConstants.Sixteen, null, false, 21, 22, 2)]
        [TestCase(CreatureConstants.Nightwing, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Fourteen, null, false, 17, 18, 0)]
        [TestCase(CreatureConstants.Nixie, SizeConstants.Small, 5, 5, ChallengeRatingConstants.One, 3, true, 12, 0, 2)]
        [TestCase(CreatureConstants.Nymph, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Seven, 7, true, 7, 0, 2)]
        [TestCase(CreatureConstants.OchreJelly, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Five, null, false, 0, 0, 0)]
        [TestCase(CreatureConstants.Octopus, SizeConstants.Small, 5, 5, ChallengeRatingConstants.One, null, false, 0, 2, 0)]
        [TestCase(CreatureConstants.Octopus_Giant, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Eight, null, false, 0, 7, 0)]
        [TestCase(CreatureConstants.Ogre, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Three, 2, true, 0, 5, 2)]
        [TestCase(CreatureConstants.Ogre_Merrow, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Three, 2, true, 0, 5, 2)]
        [TestCase(CreatureConstants.OgreMage, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Eight, 7, true, 9, 5, 2)]
        [TestCase(CreatureConstants.Orc, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.OneHalf, 0, true, 0, 0, 2)]
        [TestCase(CreatureConstants.Orc_Half, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.OneHalf, 0, true, 0, 0, 2)]
        [TestCase(CreatureConstants.Otyugh, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Four, null, false, 0, 8, 0)]
        [TestCase(CreatureConstants.Owl, SizeConstants.Tiny, 2.5, 0, ChallengeRatingConstants.OneFourth, null, false, 0, 2, 0)]
        [TestCase(CreatureConstants.Owl_Giant, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Three, 2, false, 0, 3, 0)]
        [TestCase(CreatureConstants.Owlbear, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Four, null, false, 0, 5, 0)]
        [TestCase(CreatureConstants.Pegasus, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Three, 2, false, 5, 3, 0)]
        [TestCase(CreatureConstants.PhantomFungus, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Three, null, false, 12, 4, 0)]
        [TestCase(CreatureConstants.PhaseSpider, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Five, null, false, 15, 3, 0)]
        [TestCase(CreatureConstants.Phasm, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Seven, null, true, 0, 5, 0)]
        [TestCase(CreatureConstants.PitFiend, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Twenty, null, false, 18, 23, 2)]
        [TestCase(CreatureConstants.Pixie, SizeConstants.Small, 5, 5, ChallengeRatingConstants.Four, 4, true, 8, 1, 2)]
        [TestCase(CreatureConstants.Pixie_WithIrresistibleDance, SizeConstants.Small, 5, 5, ChallengeRatingConstants.Five, 6, true, 8, 1, 2)]
        [TestCase(CreatureConstants.Pony, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.OneFourth, null, false, 0, 2, 0)]
        [TestCase(CreatureConstants.Pony_War, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.OneHalf, null, false, 0, 2, 0)]
        [TestCase(CreatureConstants.Porpoise, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.OneHalf, null, false, 0, 2, 0)]
        [TestCase(CreatureConstants.PrayingMantis_Giant, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Three, null, false, 0, 6, 0)]
        [TestCase(CreatureConstants.Pseudodragon, SizeConstants.Tiny, 2.5, 0, ChallengeRatingConstants.One, 3, false, 0, 4, 0)]
        [TestCase(CreatureConstants.PurpleWorm, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.Twelve, null, false, 0, 15, 0)]
        [TestCase(CreatureConstants.Pyrohydra_5Heads, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Six, null, false, 0, 6, 0)]
        [TestCase(CreatureConstants.Pyrohydra_6Heads, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Seven, null, false, 0, 7, 0)]
        [TestCase(CreatureConstants.Pyrohydra_7Heads, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Eight, null, false, 0, 8, 0)]
        [TestCase(CreatureConstants.Pyrohydra_8Heads, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Nine, null, false, 0, 9, 0)]
        [TestCase(CreatureConstants.Pyrohydra_9Heads, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Ten, null, false, 0, 10, 0)]
        [TestCase(CreatureConstants.Pyrohydra_10Heads, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Eleven, null, false, 0, 11, 0)]
        [TestCase(CreatureConstants.Pyrohydra_11Heads, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Twelve, null, false, 0, 12, 0)]
        [TestCase(CreatureConstants.Pyrohydra_12Heads, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Thirteen, null, false, 0, 13, 0)]
        [TestCase(CreatureConstants.Quasit, SizeConstants.Tiny, 2.5, 0, ChallengeRatingConstants.Two, null, false, 6, 3, 2)]
        [TestCase(CreatureConstants.Rakshasa, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Ten, 7, true, 7, 9, 2)]
        [TestCase(CreatureConstants.Rast, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Five, null, false, 0, 4, 0)]
        [TestCase(CreatureConstants.Rat, SizeConstants.Tiny, 2.5, 0, ChallengeRatingConstants.OneEighth, null, false, 0, 0, 0)]
        [TestCase(CreatureConstants.Rat_Dire, SizeConstants.Small, 5, 5, ChallengeRatingConstants.OneThird, null, false, 0, 1, 0)]
        [TestCase(CreatureConstants.Rat_Swarm, SizeConstants.Tiny, 10, 0, ChallengeRatingConstants.Two, null, false, 0, 0, 0)]
        [TestCase(CreatureConstants.Raven, SizeConstants.Tiny, 2.5, 0, ChallengeRatingConstants.OneSixth, null, false, 0, 0, 0)]
        [TestCase(CreatureConstants.Ravid, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Five, null, false, 20, 15, 1)]
        [TestCase(CreatureConstants.RazorBoar, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Ten, null, false, 0, 17, 0)]
        [TestCase(CreatureConstants.Remorhaz, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Seven, null, false, 0, 11, 0)]
        [TestCase(CreatureConstants.Retriever, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Eleven, null, false, 0, 10, 0)]
        [TestCase(CreatureConstants.Rhinoceras, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Four, null, false, 0, 7, 0)]
        [TestCase(CreatureConstants.Roc, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.Nine, null, false, 0, 9, 0)]
        [TestCase(CreatureConstants.Roper, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Twelve, null, false, 0, 14, 0)]
        [TestCase(CreatureConstants.RustMonster, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Three, null, false, 0, 5, 0)]
        [TestCase(CreatureConstants.Sahuagin, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Two, 2, true, 0, 5, 2)]
        [TestCase(CreatureConstants.Sahuagin_Mutant, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Two, 3, true, 0, 5, 4)]
        [TestCase(CreatureConstants.Sahuagin_Malenti, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Two, 2, true, 0, 5, 2)]
        [TestCase(CreatureConstants.Salamander_Average, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Six, 5, true, 0, 7, 2)]
        [TestCase(CreatureConstants.Salamander_Flamebrother, SizeConstants.Small, 5, 5, ChallengeRatingConstants.Three, 4, true, 0, 7, 2)]
        [TestCase(CreatureConstants.Salamander_Noble, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Ten, null, true, 15, 8, 2)]
        [TestCase(CreatureConstants.Satyr, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Two, 2, true, 0, 4, 2)]
        [TestCase(CreatureConstants.Satyr_WithPipes, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Four, 2, true, 10, 4, 2)]
        [TestCase(CreatureConstants.Scorpion_Monstrous_Colossal, SizeConstants.Colossal, 40, 30, ChallengeRatingConstants.Twelve, null, false, 0, 25, 0)]
        [TestCase(CreatureConstants.Scorpion_Monstrous_Gargantuan, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.Ten, null, false, 0, 18, 0)]
        [TestCase(CreatureConstants.Scorpion_Monstrous_Huge, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Seven, null, false, 0, 12, 0)]
        [TestCase(CreatureConstants.Scorpion_Monstrous_Large, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Three, null, false, 0, 7, 0)]
        [TestCase(CreatureConstants.Scorpion_Monstrous_Medium, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.One, null, false, 0, 4, 0)]
        [TestCase(CreatureConstants.Scorpion_Monstrous_Small, SizeConstants.Small, 5, 5, ChallengeRatingConstants.OneHalf, null, false, 0, 3, 0)]
        [TestCase(CreatureConstants.Scorpion_Monstrous_Tiny, SizeConstants.Tiny, 2.5, 0, ChallengeRatingConstants.OneFourth, null, false, 0, 2, 0)]
        [TestCase(CreatureConstants.Scorpionfolk, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Seven, 4, true, 10, 6, 2)]
        [TestCase(CreatureConstants.SeaCat, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Four, null, false, 0, 8, 0)]
        [TestCase(CreatureConstants.SeaHag, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Four, 0, true, 0, 3, 2)]
        [TestCase(CreatureConstants.Shadow, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Three, null, false, 0, 0, 0)]
        [TestCase(CreatureConstants.Shadow_Greater, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Eight, null, false, 0, 0, 0)]
        [TestCase(CreatureConstants.ShadowMastiff, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Five, 3, false, 0, 3, 0)]
        [TestCase(CreatureConstants.ShamblingMound, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Six, 6, false, 0, 11, 0)]
        [TestCase(CreatureConstants.Shark_Dire, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Nine, null, false, 0, 7, 0)]
        [TestCase(CreatureConstants.Shark_Huge, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Four, null, false, 0, 5, 0)]
        [TestCase(CreatureConstants.Shark_Large, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Two, null, false, 0, 4, 0)]
        [TestCase(CreatureConstants.Shark_Medium, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.One, null, false, 0, 3, 0)]
        [TestCase(CreatureConstants.ShieldGuardian, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Eight, null, false, 0, 15, 2)]
        [TestCase(CreatureConstants.ShockerLizard, SizeConstants.Small, 5, 5, ChallengeRatingConstants.Two, null, false, 0, 3, 0)]
        [TestCase(CreatureConstants.Shrieker, SizeConstants.Medium, 5, 0, ChallengeRatingConstants.One, null, false, 0, 3, 0)]
        [TestCase(CreatureConstants.Skum, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Two, 3, true, 0, 2, 2)]
        [TestCase(CreatureConstants.Slaad_Blue, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Eight, 6, true, 8, 9, 2)]
        [TestCase(CreatureConstants.Slaad_Death, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Thirteen, null, true, 15, 12, 2)]
        [TestCase(CreatureConstants.Slaad_Gray, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Ten, 6, true, 10, 11, 2)]
        [TestCase(CreatureConstants.Slaad_Green, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Nine, 7, true, 9, 13, 2)]
        [TestCase(CreatureConstants.Slaad_Red, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Seven, 6, true, 0, 8, 2)]
        [TestCase(CreatureConstants.Snake_Constrictor, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Two, null, false, 0, 2, 0)]
        [TestCase(CreatureConstants.Snake_Constrictor_Giant, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Five, null, false, 0, 4, 0)]
        [TestCase(CreatureConstants.Snake_Viper_Huge, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Three, null, false, 0, 3, 0)]
        [TestCase(CreatureConstants.Snake_Viper_Large, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Two, null, false, 0, 3, 0)]
        [TestCase(CreatureConstants.Snake_Viper_Medium, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.One, null, false, 0, 3, 0)]
        [TestCase(CreatureConstants.Snake_Viper_Small, SizeConstants.Small, 5, 5, ChallengeRatingConstants.OneHalf, null, false, 0, 3, 0)]
        [TestCase(CreatureConstants.Snake_Viper_Tiny, SizeConstants.Tiny, 2.5, 0, ChallengeRatingConstants.OneThird, null, false, 0, 2, 0)]
        [TestCase(CreatureConstants.Spectre, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Seven, null, false, 0, 0, 0)]
        [TestCase(CreatureConstants.Spider_Monstrous_Hunter_Colossal, SizeConstants.Colossal, 40, 30, ChallengeRatingConstants.Eleven, null, false, 0, 18, 0)]
        [TestCase(CreatureConstants.Spider_Monstrous_Hunter_Gargantuan, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.Eight, null, false, 0, 10, 0)]
        [TestCase(CreatureConstants.Spider_Monstrous_Hunter_Huge, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Five, null, false, 0, 5, 0)]
        [TestCase(CreatureConstants.Spider_Monstrous_Hunter_Large, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Two, null, false, 0, 2, 0)]
        [TestCase(CreatureConstants.Spider_Monstrous_Hunter_Medium, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.One, null, false, 0, 1, 0)]
        [TestCase(CreatureConstants.Spider_Monstrous_Hunter_Small, SizeConstants.Small, 5, 5, ChallengeRatingConstants.OneHalf, null, false, 0, 0, 0)]
        [TestCase(CreatureConstants.Spider_Monstrous_Hunter_Tiny, SizeConstants.Tiny, 2.5, 0, ChallengeRatingConstants.OneFourth, null, false, 0, 0, 0)]
        [TestCase(CreatureConstants.Spider_Monstrous_WebSpinner_Colossal, SizeConstants.Colossal, 40, 30, ChallengeRatingConstants.Eleven, null, false, 0, 18, 0)]
        [TestCase(CreatureConstants.Spider_Monstrous_WebSpinner_Gargantuan, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.Eight, null, false, 0, 10, 0)]
        [TestCase(CreatureConstants.Spider_Monstrous_WebSpinner_Huge, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Five, null, false, 0, 5, 0)]
        [TestCase(CreatureConstants.Spider_Monstrous_WebSpinner_Large, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Two, null, false, 0, 2, 0)]
        [TestCase(CreatureConstants.Spider_Monstrous_WebSpinner_Medium, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.One, null, false, 0, 1, 0)]
        [TestCase(CreatureConstants.Spider_Monstrous_WebSpinner_Small, SizeConstants.Small, 5, 5, ChallengeRatingConstants.OneHalf, null, false, 0, 0, 0)]
        [TestCase(CreatureConstants.Spider_Monstrous_WebSpinner_Tiny, SizeConstants.Tiny, 2.5, 0, ChallengeRatingConstants.OneFourth, null, false, 0, 0, 0)]
        [TestCase(CreatureConstants.SpiderEater, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Five, null, false, 12, 4, 0)]
        [TestCase(CreatureConstants.Spider_Swarm, SizeConstants.Diminutive, 10, 0, ChallengeRatingConstants.One, null, false, 0, 0, 0)]
        [TestCase(CreatureConstants.Squid, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.One, null, false, 0, 3, 0)]
        [TestCase(CreatureConstants.Squid_Giant, SizeConstants.Huge, 15, 15, ChallengeRatingConstants.Nine, null, false, 0, 6, 0)]
        [TestCase(CreatureConstants.StagBeetle_Giant, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Four, null, false, 0, 10, 0)]
        [TestCase(CreatureConstants.Stirge, SizeConstants.Tiny, 2.5, 0, ChallengeRatingConstants.OneHalf, null, false, 0, 0, 0)]
        [TestCase(CreatureConstants.Succubus, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Seven, 6, true, 12, 9, 2)]
        [TestCase(CreatureConstants.Tarrasque, SizeConstants.Colossal, 30, 20, ChallengeRatingConstants.Twenty, null, false, 0, 30, 2)]
        [TestCase(CreatureConstants.Tendriculos, SizeConstants.Huge, 15, 15, ChallengeRatingConstants.Six, null, false, 0, 9, 0)]
        [TestCase(CreatureConstants.Thoqqua, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Two, null, false, 0, 7, 0)]
        [TestCase(CreatureConstants.Tiefling, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.OneHalf, 1, true, 1, 0, 2)]
        [TestCase(CreatureConstants.Tiger, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Four, null, false, 0, 3, 0)]
        [TestCase(CreatureConstants.Tiger_Dire, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Eight, null, false, 0, 6, 0)]
        [TestCase(CreatureConstants.Titan, SizeConstants.Huge, 15, 15, ChallengeRatingConstants.TwentyOne, null, true, 20, 19, 2)]
        [TestCase(CreatureConstants.Toad, SizeConstants.Diminutive, 1, 0, ChallengeRatingConstants.OneTenth, null, false, 0, 0, 0)]
        [TestCase(CreatureConstants.Tojanida_Adult, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Five, null, false, 0, 12, 0)]
        [TestCase(CreatureConstants.Tojanida_Elder, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Nine, null, false, 0, 14, 0)]
        [TestCase(CreatureConstants.Tojanida_Juvenile, SizeConstants.Small, 5, 5, ChallengeRatingConstants.Three, null, false, 0, 10, 0)]
        [TestCase(CreatureConstants.Treant, SizeConstants.Huge, 15, 15, ChallengeRatingConstants.Eight, 5, false, 12, 13, 2)]
        [TestCase(CreatureConstants.Triceratops, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Nine, null, false, 0, 11, 0)]
        [TestCase(CreatureConstants.Triton, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Two, 2, true, 7, 6, 2)]
        [TestCase(CreatureConstants.Troglodyte, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.One, 2, true, 0, 6, 2)]
        [TestCase(CreatureConstants.Troll, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Five, 5, true, 0, 5, 2)]
        [TestCase(CreatureConstants.Troll_Scrag, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Five, 5, true, 0, 5, 2)]
        [TestCase(CreatureConstants.TrumpetArchon, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Fourteen, 8, true, 12, 14, 2)]
        [TestCase(CreatureConstants.Tyrannosaurus, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Eight, null, false, 0, 5, 0)]
        [TestCase(CreatureConstants.UmberHulk, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Seven, null, false, 8, 8, 0)]
        [TestCase(CreatureConstants.UmberHulk_TrulyHorrid, SizeConstants.Huge, 15, 15, ChallengeRatingConstants.Fourteen, null, false, 8, 14, 0)]
        [TestCase(CreatureConstants.Unicorn, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Three, 4, false, 5, 6, 0)]
        [TestCase(CreatureConstants.VampireSpawn, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Four, null, false, 6, 3, 2)]
        [TestCase(CreatureConstants.Vargouille, SizeConstants.Small, 5, 5, ChallengeRatingConstants.Two, null, false, 0, 0, 0)]
        [TestCase(CreatureConstants.VioletFungus, SizeConstants.Medium, 5, 10, ChallengeRatingConstants.Three, null, false, 0, 4, 0)]
        [TestCase(CreatureConstants.Vrock, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Nine, 8, true, 12, 11, 2)]
        [TestCase(CreatureConstants.Wasp_Giant, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Three, null, false, 0, 4, 0)]
        [TestCase(CreatureConstants.Weasel, SizeConstants.Tiny, 2.5, 0, ChallengeRatingConstants.OneFourth, null, false, 0, 0, 0)]
        [TestCase(CreatureConstants.Weasel_Dire, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Two, null, false, 0, 2, 0)]
        [TestCase(CreatureConstants.Whale_Baleen, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.Six, null, false, 0, 9, 0)]
        [TestCase(CreatureConstants.Whale_Cachalot, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.Seven, null, false, 0, 9, 0)]
        [TestCase(CreatureConstants.Whale_Orca, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Five, null, false, 0, 6, 0)]
        [TestCase(CreatureConstants.Wight, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Three, null, false, 0, 4, 2)]
        [TestCase(CreatureConstants.WillOWisp, SizeConstants.Small, 5, 5, ChallengeRatingConstants.Six, null, false, 0, 0, 0)]
        [TestCase(CreatureConstants.WinterWolf, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Five, 3, false, 0, 5, 0)]
        [TestCase(CreatureConstants.Wolf, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.One, null, false, 0, 2, 0)]
        [TestCase(CreatureConstants.Wolf_Dire, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Three, null, false, 0, 3, 0)]
        [TestCase(CreatureConstants.Wolverine, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Two, null, false, 0, 2, 0)]
        [TestCase(CreatureConstants.Wolverine_Dire, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Four, null, false, 0, 4, 0)]
        [TestCase(CreatureConstants.Worg, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Two, 1, false, 0, 2, 0)]
        [TestCase(CreatureConstants.Wraith, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Five, null, false, 0, 0, 0)]
        [TestCase(CreatureConstants.Wraith_Dread, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Eleven, null, false, 0, 0, 0)]
        [TestCase(CreatureConstants.Wyvern, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Six, null, false, 0, 8, 0)]
        [TestCase(CreatureConstants.Xill, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Six, 4, true, 0, 7, 4)]
        [TestCase(CreatureConstants.Xorn_Average, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Six, null, false, 0, 14, 0)]
        [TestCase(CreatureConstants.Xorn_Elder, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Eight, null, false, 0, 16, 0)]
        [TestCase(CreatureConstants.Xorn_Minor, SizeConstants.Small, 5, 5, ChallengeRatingConstants.Three, null, false, 0, 12, 0)]
        [TestCase(CreatureConstants.YethHound, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Three, 3, false, 0, 8, 0)]
        [TestCase(CreatureConstants.Yrthak, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Nine, null, false, 0, 8, 0)]
        [TestCase(CreatureConstants.YuanTi_Abomination, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Seven, 7, true, 10, 10, 2)]
        [TestCase(CreatureConstants.YuanTi_Halfblood_SnakeHead, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Five, 5, true, 4, 4, 2)]
        [TestCase(CreatureConstants.YuanTi_Halfblood_SnakeArms, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Five, 5, true, 4, 4, 0)]
        [TestCase(CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Five, 5, true, 4, 4, 2)]
        [TestCase(CreatureConstants.YuanTi_Halfblood_SnakeTail, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Five, 5, true, 4, 4, 2)]
        [TestCase(CreatureConstants.YuanTi_Pureblood, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Three, 2, true, 4, 1, 2)]
        [TestCase(CreatureConstants.Zelekhut, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Nine, 7, true, 8, 10, 2)]
        public void CreatureData(string creature, string size, double space, double reach, string challengeRating, int? levelAdjustment, bool canUseEquipment, int casterLevel, int naturalArmor, int numberOfHands)
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
        }

        [TestCaseSource(typeof(CreatureTestData), nameof(CreatureTestData.Creatures))]
        public void AllCreaturesHaveCorrectNumberOfEntries(string creature)
        {
            var data = DataIndexConstants.CreatureData.InitializeData();
            Assert.That(table[creature].Count(), Is.EqualTo(data.Length));
        }

        [TestCaseSource(typeof(CreatureTestData), nameof(CreatureTestData.Creatures))]

        public void AllCreaturesHaveCorrectChallengeRatings(string creature)
        {
            var challengeRatings = ChallengeRatingConstants.GetOrdered();
            var data = table[creature].ToArray();

            Assert.That(data.Length - 1, Is.AtLeast(DataIndexConstants.CreatureData.ChallengeRating), creature);
            Assert.That(data[DataIndexConstants.CreatureData.ChallengeRating], Is.Not.Empty, creature);
            Assert.That(new[] { data[DataIndexConstants.CreatureData.ChallengeRating] }, Is.SubsetOf(challengeRatings), creature);
            Assert.That(data[DataIndexConstants.CreatureData.ChallengeRating], Is.Not.EqualTo(ChallengeRatingConstants.Zero), creature);
        }

        [TestCaseSource(typeof(CreatureTestData), nameof(CreatureTestData.Creatures))]
        public void AllCreaturesHaveCorrectSizes(string creature)
        {
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

            var data = table[creature].ToArray();

            Assert.That(data.Length - 1, Is.AtLeast(DataIndexConstants.CreatureData.Size), creature);
            Assert.That(data[DataIndexConstants.CreatureData.Size], Is.Not.Empty, creature);
            Assert.That(new[] { data[DataIndexConstants.CreatureData.Size] }, Is.SubsetOf(sizes), creature);
        }

        [TestCaseSource(typeof(CreatureTestData), nameof(CreatureTestData.Creatures))]
        public void AllCreaturesHaveCorrectReach(string creature)
        {
            var data = table[creature].ToArray();

            Assert.That(data.Length - 1, Is.AtLeast(DataIndexConstants.CreatureData.Reach), creature);
            Assert.That(double.TryParse(data[DataIndexConstants.CreatureData.Reach], out var reach), Is.True, creature);
            Assert.That(reach, Is.Not.Negative, creature);
        }

        [TestCaseSource(typeof(CreatureTestData), nameof(CreatureTestData.Creatures))]
        public void AllCreaturesHaveCorrectSpace(string creature)
        {
            var data = table[creature].ToArray();

            Assert.That(data.Length - 1, Is.AtLeast(DataIndexConstants.CreatureData.Space), creature);
            Assert.That(double.TryParse(data[DataIndexConstants.CreatureData.Space], out var space), Is.True, creature);
            Assert.That(space, Is.Positive, creature);
        }

        [TestCaseSource(typeof(CreatureTestData), nameof(CreatureTestData.Creatures))]
        public void AllCreaturesHaveCorrectLevelAdjustment(string creature)
        {
            var data = table[creature].ToArray();

            Assert.That(data.Length - 1, Is.AtLeast(DataIndexConstants.CreatureData.LevelAdjustment), creature);

            if (!string.IsNullOrEmpty(data[DataIndexConstants.CreatureData.LevelAdjustment]))
            {
                Assert.That(int.TryParse(data[DataIndexConstants.CreatureData.LevelAdjustment], out var levelAdjustment), Is.True, creature);
                Assert.That(levelAdjustment, Is.Not.Negative, creature);
            }
            else
            {
                Assert.That(data[DataIndexConstants.CreatureData.LevelAdjustment], Is.Empty, creature);
            }
        }

        [TestCaseSource(typeof(CreatureTestData), nameof(CreatureTestData.Creatures))]
        public void AllCreaturesHaveCorrectCanUseEquipment(string creature)
        {
            var data = table[creature].ToArray();

            Assert.That(data.Length - 1, Is.AtLeast(DataIndexConstants.CreatureData.CanUseEquipment), creature);
            Assert.That(data[DataIndexConstants.CreatureData.CanUseEquipment], Is.EqualTo(bool.TrueString).Or.EqualTo(bool.FalseString), creature);
        }

        [TestCaseSource(typeof(CreatureTestData), nameof(CreatureTestData.Creatures))]
        public void CreaturesOfTypeCanUseEquipment(string creature)
        {
            var equipmentTypes = new[]
            {
                CreatureConstants.Types.Fey,
                CreatureConstants.Types.Giant,
                CreatureConstants.Types.Humanoid,
                CreatureConstants.Types.MonstrousHumanoid,
            };
            var types = collectionSelector.SelectFrom(TableNameConstants.Collection.CreatureTypes, creature);

            if (equipmentTypes.Contains(types.First()))
            {
                var data = table[creature].ToArray();

                Assert.That(data.Length - 1, Is.AtLeast(DataIndexConstants.CreatureData.CanUseEquipment), creature);
                Assert.That(data[DataIndexConstants.CreatureData.CanUseEquipment], Is.EqualTo(bool.TrueString), creature);
            }
        }

        [TestCaseSource(typeof(CreatureTestData), nameof(CreatureTestData.Creatures))]
        public void CreaturesOfTypeCannotUseEquipment(string creature)
        {
            var noEquipmentTypes = new[]
            {
                CreatureConstants.Types.Animal,
                CreatureConstants.Types.Ooze,
                CreatureConstants.Types.Vermin,
            };
            var types = collectionSelector.SelectFrom(TableNameConstants.Collection.CreatureTypes, creature);

            if (noEquipmentTypes.Contains(types.First()))
            {
                var data = table[creature].ToArray();

                Assert.That(data.Length - 1, Is.AtLeast(DataIndexConstants.CreatureData.CanUseEquipment), creature);
                Assert.That(data[DataIndexConstants.CreatureData.CanUseEquipment], Is.EqualTo(bool.FalseString), creature);
            }
        }

        [TestCaseSource(typeof(CreatureTestData), nameof(CreatureTestData.Creatures))]
        public void AllCreaturesHaveCorrectCasterLevel(string creature)
        {
            var data = table[creature].ToArray();

            Assert.That(data.Length - 1, Is.AtLeast(DataIndexConstants.CreatureData.CasterLevel), creature);
            Assert.That(int.TryParse(data[DataIndexConstants.CreatureData.CasterLevel], out var casterLevel), Is.True, creature);
            Assert.That(casterLevel, Is.Not.Negative, creature);
        }

        [TestCaseSource(typeof(CreatureTestData), nameof(CreatureTestData.Creatures))]
        public void AllCreaturesHaveCorrectNaturalArmor(string creature)
        {
            var data = table[creature].ToArray();

            Assert.That(data.Length - 1, Is.AtLeast(DataIndexConstants.CreatureData.NaturalArmor), creature);
            Assert.That(int.TryParse(data[DataIndexConstants.CreatureData.NaturalArmor], out var naturalArmor), Is.True, creature);
            Assert.That(naturalArmor, Is.Not.Negative, creature);
        }

        [TestCaseSource(typeof(CreatureTestData), nameof(CreatureTestData.Creatures))]
        public void AllCreaturesHaveCorrectNumberOfHands(string creature)
        {
            var data = table[creature].ToArray();

            Assert.That(data.Length - 1, Is.AtLeast(DataIndexConstants.CreatureData.NumberOfHands), creature);
            Assert.That(int.TryParse(data[DataIndexConstants.CreatureData.NumberOfHands], out var numberOfHands), Is.True, creature);
            Assert.That(numberOfHands, Is.Not.Negative, creature);
        }
    }
}
