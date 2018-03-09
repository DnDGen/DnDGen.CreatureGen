using CreatureGen.Creatures;
using CreatureGen.Tables;
using NUnit.Framework;
using System;
using System.Linq;

namespace CreatureGen.Tests.Integration.Tables.Creatures
{
    [TestFixture]
    public class CreatureDataTests : DataTests
    {
        protected override string tableName
        {
            get
            {
                return TableNameConstants.Set.Collection.CreatureData;
            }
        }

        [Test]
        public void CollectionNames()
        {
            var names = CreatureConstants.All();
            AssertCollectionNames(names);
        }

        [TestCase(CreatureConstants.Aasimar, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.OneHalf, 1, true)]
        [TestCase(CreatureConstants.Aboleth, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Seven, 0, false)]
        [TestCase(CreatureConstants.Achaierai, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Five, null, false)]
        [TestCase(CreatureConstants.Allip, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Three, null, false)]
        [TestCase(CreatureConstants.Androsphinx, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Nine, 5, false)]
        [TestCase(CreatureConstants.Angel_AstralDeva, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Fourteen, 8, true)]
        [TestCase(CreatureConstants.Angel_Planetar, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Sixteen, null, true)]
        [TestCase(CreatureConstants.Angel_Solar, SizeConstants.Large, 10, 10, ChallengeRatingConstants.TwentyThree, null, true)]
        [TestCase(CreatureConstants.AnimatedObject_Colossal, SizeConstants.Colossal, 30, 20, ChallengeRatingConstants.Ten, null, false)]
        [TestCase(CreatureConstants.AnimatedObject_Gargantuan, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.Seven, null, false)]
        [TestCase(CreatureConstants.AnimatedObject_Huge, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Five, null, false)]
        [TestCase(CreatureConstants.AnimatedObject_Large, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Three, null, false)]
        [TestCase(CreatureConstants.AnimatedObject_Medium, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Two, null, false)]
        [TestCase(CreatureConstants.AnimatedObject_Small, SizeConstants.Small, 5, 5, ChallengeRatingConstants.One, null, false)]
        [TestCase(CreatureConstants.AnimatedObject_Tiny, SizeConstants.Tiny, 2.5, 0, ChallengeRatingConstants.OneHalf, null, false)]
        [TestCase(CreatureConstants.Ankheg, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Three, null, false)]
        [TestCase(CreatureConstants.Annis, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Six, 0, true)]
        [TestCase(CreatureConstants.Ant_Giant_Queen, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Two, null, false)]
        [TestCase(CreatureConstants.Ant_Giant_Soldier, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Two, null, false)]
        [TestCase(CreatureConstants.Ant_Giant_Worker, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.One, null, false)]
        [TestCase(CreatureConstants.Ape, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Two, null, false)]
        [TestCase(CreatureConstants.Ape_Dire, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Three, null, false)]
        [TestCase(CreatureConstants.Aranea, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Four, 4, true)]
        [TestCase(CreatureConstants.Arrowhawk_Adult, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Five, null, false)]
        [TestCase(CreatureConstants.Arrowhawk_Elder, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Eight, null, false)]
        [TestCase(CreatureConstants.Arrowhawk_Juvenile, SizeConstants.Small, 5, 5, ChallengeRatingConstants.Three, null, false)]
        [TestCase(CreatureConstants.AssassinVine, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Three, null, false)]
        [TestCase(CreatureConstants.Athach, SizeConstants.Huge, 15, 15, ChallengeRatingConstants.Eight, 5, true)]
        [TestCase(CreatureConstants.Avoral, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Nine, null, false)]
        [TestCase(CreatureConstants.Azer, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Two, 4, true)]
        [TestCase(CreatureConstants.Babau, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Six, null, false)]
        [TestCase(CreatureConstants.Baboon, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.OneHalf, null, false)]
        [TestCase(CreatureConstants.Badger, SizeConstants.Small, 5, 5, ChallengeRatingConstants.OneHalf, null, false)]
        [TestCase(CreatureConstants.Badger_Dire, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Two, null, false)]
        [TestCase(CreatureConstants.Balor, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Twenty, null, false)]
        [TestCase(CreatureConstants.BarbedDevil_Hamatula, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Eleven, null, false)]
        [TestCase(CreatureConstants.Barghest, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Four, null, false)]
        [TestCase(CreatureConstants.Barghest_Greater, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Five, null, false)]
        [TestCase(CreatureConstants.Basilisk, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Five, null, false)]
        [TestCase(CreatureConstants.Basilisk_AbyssalGreater, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Twelve, null, false)]
        [TestCase(CreatureConstants.Bat, SizeConstants.Diminutive, 1, 0, ChallengeRatingConstants.OneTenth, null, false)]
        [TestCase(CreatureConstants.Bat_Dire, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Two, null, false)]
        [TestCase(CreatureConstants.Bat_Swarm, SizeConstants.Diminutive, 10, 0, ChallengeRatingConstants.Two, null, false)]
        [TestCase(CreatureConstants.Bear_Black, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Two, null, false)]
        [TestCase(CreatureConstants.Bear_Brown, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Four, null, false)]
        [TestCase(CreatureConstants.Bear_Dire, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Seven, null, false)]
        [TestCase(CreatureConstants.Bear_Polar, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Four, null, false)]
        [TestCase(CreatureConstants.BeardedDevil_Barbazu, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Five, 6, true)]
        [TestCase(CreatureConstants.Bebilith, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Ten, null, false)]
        [TestCase(CreatureConstants.Bee_Giant, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.One, null, false)]
        [TestCase(CreatureConstants.Behir, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Eight, null, false)]
        [TestCase(CreatureConstants.Beholder, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Thirteen, null, false)]
        [TestCase(CreatureConstants.Beholder_Gauth, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Six, null, false)]
        [TestCase(CreatureConstants.Belker, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Six, null, false)]
        [TestCase(CreatureConstants.Bison, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Two, null, false)]
        [TestCase(CreatureConstants.BlackPudding, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Seven, null, false)]
        [TestCase(CreatureConstants.BlackPudding_Elder, SizeConstants.Gargantuan, 20, 20, ChallengeRatingConstants.Twelve, null, false)]
        [TestCase(CreatureConstants.BlinkDog, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Two, 2, false)]
        [TestCase(CreatureConstants.Boar, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Two, null, false)]
        [TestCase(CreatureConstants.Boar_Dire, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Four, null, false)]
        [TestCase(CreatureConstants.Bodak, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Eight, null, false)]
        [TestCase(CreatureConstants.BombardierBeetle_Giant, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Two, null, false)]
        [TestCase(CreatureConstants.BoneDevil_Osyluth, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Nine, null, false)]
        [TestCase(CreatureConstants.Bralani, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Six, 5, true)]
        [TestCase(CreatureConstants.Bugbear, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Two, 1, true)]
        [TestCase(CreatureConstants.Bulette, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Seven, null, false)]
        [TestCase(CreatureConstants.Camel_Bactrian, SizeConstants.Large, 10, 5, ChallengeRatingConstants.One, null, false)]
        [TestCase(CreatureConstants.Camel_Dromedary, SizeConstants.Large, 10, 5, ChallengeRatingConstants.One, null, false)]
        [TestCase(CreatureConstants.CarrionCrawler, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Four, null, false)]
        [TestCase(CreatureConstants.Cat, SizeConstants.Tiny, 2.5, 0, ChallengeRatingConstants.OneFourth, null, false)]
        [TestCase(CreatureConstants.Centaur, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Three, 2, true)]
        [TestCase(CreatureConstants.Centipede_Monstrous_Colossal, SizeConstants.Colossal, 30, 20, ChallengeRatingConstants.Nine, null, false)]
        [TestCase(CreatureConstants.Centipede_Monstrous_Gargantuan, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.Six, null, false)]
        [TestCase(CreatureConstants.Centipede_Monstrous_Huge, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Two, null, false)]
        [TestCase(CreatureConstants.Centipede_Monstrous_Large, SizeConstants.Large, 10, 5, ChallengeRatingConstants.One, null, false)]
        [TestCase(CreatureConstants.Centipede_Monstrous_Medium, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.OneHalf, null, false)]
        [TestCase(CreatureConstants.Centipede_Monstrous_Small, SizeConstants.Small, 5, 5, ChallengeRatingConstants.OneFourth, null, false)]
        [TestCase(CreatureConstants.Centipede_Monstrous_Tiny, SizeConstants.Tiny, 2.5, 0, ChallengeRatingConstants.OneEighth, null, false)]
        [TestCase(CreatureConstants.Centipede_Swarm, SizeConstants.Diminutive, 10, 0, ChallengeRatingConstants.Four, null, false)]
        [TestCase(CreatureConstants.ChainDevil_Kyton, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Six, 6, true)]
        [TestCase(CreatureConstants.ChaosBeast, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Seven, null, false)]
        [TestCase(CreatureConstants.Cheetah, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Two, null, false)]
        [TestCase(CreatureConstants.Chimera, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Seven, 2, false)]
        [TestCase(CreatureConstants.Choker, SizeConstants.Small, 5, 10, ChallengeRatingConstants.Two, null, false)]
        [TestCase(CreatureConstants.Chuul, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Seven, null, false)]
        [TestCase(CreatureConstants.Cloaker, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Five, null, false)]
        [TestCase(CreatureConstants.Cockatrice, SizeConstants.Small, 5, 5, ChallengeRatingConstants.Three, null, false)]
        [TestCase(CreatureConstants.Couatl, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Ten, 7, false)]
        [TestCase(CreatureConstants.Criosphinx, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Seven, 3, false)]
        [TestCase(CreatureConstants.Crocodile, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Two, null, false)]
        [TestCase(CreatureConstants.Crocodile_Giant, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Four, null, false)]
        [TestCase(CreatureConstants.Darkmantle, SizeConstants.Small, 5, 5, ChallengeRatingConstants.One, null, false)]
        [TestCase(CreatureConstants.Deinonychus, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Three, null, false)]
        [TestCase(CreatureConstants.Delver, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Nine, null, false)]
        [TestCase(CreatureConstants.Derro, SizeConstants.Small, 5, 5, ChallengeRatingConstants.Three, 0, true)]
        [TestCase(CreatureConstants.Derro_Sane, SizeConstants.Small, 5, 5, ChallengeRatingConstants.Three, 2, true)]
        [TestCase(CreatureConstants.Destrachan, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Eight, null, false)]
        [TestCase(CreatureConstants.Devourer, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Eleven, null, false)]
        [TestCase(CreatureConstants.Digester, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Six, null, false)]
        [TestCase(CreatureConstants.DisplacerBeast, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Four, 4, false)]
        [TestCase(CreatureConstants.DisplacerBeast_PackLord, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Twelve, null, false)]
        [TestCase(CreatureConstants.Djinni, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Five, 6, true)]
        [TestCase(CreatureConstants.Djinni_Noble, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Eight, 6, true)]
        [TestCase(CreatureConstants.Dog, SizeConstants.Small, 5, 5, ChallengeRatingConstants.OneThird, null, false)]
        [TestCase(CreatureConstants.Dog_Riding, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.One, null, false)]
        [TestCase(CreatureConstants.Donkey, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.OneSixth, null, false)]
        [TestCase(CreatureConstants.Doppelganger, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Three, 4, true)]
        [TestCase(CreatureConstants.Dragon_Black_Wyrmling, SizeConstants.Tiny, 2.5, 0, ChallengeRatingConstants.Three, 3, false)]
        [TestCase(CreatureConstants.Dragon_Black_VeryYoung, SizeConstants.Small, 5, 5, ChallengeRatingConstants.Four, 3, false)]
        [TestCase(CreatureConstants.Dragon_Black_Young, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Five, 3, false)]
        [TestCase(CreatureConstants.Dragon_Black_Juvenile, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Seven, 4, false)]
        [TestCase(CreatureConstants.Dragon_Black_YoungAdult, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Nine, null, false)]
        [TestCase(CreatureConstants.Dragon_Black_Adult, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Eleven, null, false)]
        [TestCase(CreatureConstants.Dragon_Black_MatureAdult, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Fourteen, null, false)]
        [TestCase(CreatureConstants.Dragon_Black_Old, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Sixteen, null, false)]
        [TestCase(CreatureConstants.Dragon_Black_VeryOld, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Eighteen, null, false)]
        [TestCase(CreatureConstants.Dragon_Black_Ancient, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Nineteen, null, false)]
        [TestCase(CreatureConstants.Dragon_Black_Wyrm, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.Twenty, null, false)]
        [TestCase(CreatureConstants.Dragon_Black_GreatWyrm, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.TwentyTwo, null, false)]
        [TestCase(CreatureConstants.Dragon_Blue_Wyrmling, SizeConstants.Small, 5, 5, ChallengeRatingConstants.Three, 4, false)]
        [TestCase(CreatureConstants.Dragon_Blue_VeryYoung, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Four, 4, false)]
        [TestCase(CreatureConstants.Dragon_Blue_Young, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Six, 5, false)]
        [TestCase(CreatureConstants.Dragon_Blue_Juvenile, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Eight, null, false)]
        [TestCase(CreatureConstants.Dragon_Blue_YoungAdult, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Eleven, null, false)]
        [TestCase(CreatureConstants.Dragon_Blue_Adult, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Fourteen, null, false)]
        [TestCase(CreatureConstants.Dragon_Blue_MatureAdult, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Sixteen, null, false)]
        [TestCase(CreatureConstants.Dragon_Blue_Old, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Eighteen, null, false)]
        [TestCase(CreatureConstants.Dragon_Blue_VeryOld, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Nineteen, null, false)]
        [TestCase(CreatureConstants.Dragon_Blue_Ancient, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.TwentyOne, null, false)]
        [TestCase(CreatureConstants.Dragon_Blue_Wyrm, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.TwentyThree, null, false)]
        [TestCase(CreatureConstants.Dragon_Blue_GreatWyrm, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.TwentyFive, null, false)]
        [TestCase(CreatureConstants.Dragon_Brass_Wyrmling, SizeConstants.Tiny, 2.5, 0, ChallengeRatingConstants.Three, 2, false)]
        [TestCase(CreatureConstants.Dragon_Brass_VeryYoung, SizeConstants.Small, 5, 5, ChallengeRatingConstants.Four, 3, false)]
        [TestCase(CreatureConstants.Dragon_Brass_Young, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Six, 4, false)]
        [TestCase(CreatureConstants.Dragon_Brass_Juvenile, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Eight, 4, false)]
        [TestCase(CreatureConstants.Dragon_Brass_YoungAdult, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Ten, null, false)]
        [TestCase(CreatureConstants.Dragon_Brass_Adult, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Twelve, null, false)]
        [TestCase(CreatureConstants.Dragon_Brass_MatureAdult, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Fifteen, null, false)]
        [TestCase(CreatureConstants.Dragon_Brass_Old, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Seventeen, null, false)]
        [TestCase(CreatureConstants.Dragon_Brass_VeryOld, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Nineteen, null, false)]
        [TestCase(CreatureConstants.Dragon_Brass_Ancient, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Twenty, null, false)]
        [TestCase(CreatureConstants.Dragon_Brass_Wyrm, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.TwentyOne, null, false)]
        [TestCase(CreatureConstants.Dragon_Brass_GreatWyrm, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.TwentyThree, null, false)]
        [TestCase(CreatureConstants.Dragon_Bronze_Wyrmling, SizeConstants.Small, 5, 5, ChallengeRatingConstants.Three, 4, false)]
        [TestCase(CreatureConstants.Dragon_Bronze_VeryYoung, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Five, 4, false)]
        [TestCase(CreatureConstants.Dragon_Bronze_Young, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Seven, 6, false)]
        [TestCase(CreatureConstants.Dragon_Bronze_Juvenile, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Nine, null, false)]
        [TestCase(CreatureConstants.Dragon_Bronze_YoungAdult, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Twelve, null, false)]
        [TestCase(CreatureConstants.Dragon_Bronze_Adult, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Fifteen, null, false)]
        [TestCase(CreatureConstants.Dragon_Bronze_MatureAdult, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Seventeen, null, false)]
        [TestCase(CreatureConstants.Dragon_Bronze_Old, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Nineteen, null, false)]
        [TestCase(CreatureConstants.Dragon_Bronze_VeryOld, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Twenty, null, false)]
        [TestCase(CreatureConstants.Dragon_Bronze_Ancient, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.TwentyTwo, null, false)]
        [TestCase(CreatureConstants.Dragon_Bronze_Wyrm, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.TwentyThree, null, false)]
        [TestCase(CreatureConstants.Dragon_Bronze_GreatWyrm, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.TwentyFive, null, false)]
        [TestCase(CreatureConstants.Dragon_Copper_Wyrmling, SizeConstants.Tiny, 2.5, 0, ChallengeRatingConstants.Three, 2, false)]
        [TestCase(CreatureConstants.Dragon_Copper_VeryYoung, SizeConstants.Small, 5, 5, ChallengeRatingConstants.Five, 3, false)]
        [TestCase(CreatureConstants.Dragon_Copper_Young, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Seven, 4, false)]
        [TestCase(CreatureConstants.Dragon_Copper_Juvenile, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Nine, 4, false)]
        [TestCase(CreatureConstants.Dragon_Copper_YoungAdult, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Eleven, null, false)]
        [TestCase(CreatureConstants.Dragon_Copper_Adult, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Fourteen, null, false)]
        [TestCase(CreatureConstants.Dragon_Copper_MatureAdult, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Sixteen, null, false)]
        [TestCase(CreatureConstants.Dragon_Copper_Old, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Nineteen, null, false)]
        [TestCase(CreatureConstants.Dragon_Copper_VeryOld, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Twenty, null, false)]
        [TestCase(CreatureConstants.Dragon_Copper_Ancient, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.TwentyTwo, null, false)]
        [TestCase(CreatureConstants.Dragon_Copper_Wyrm, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.TwentyThree, null, false)]
        [TestCase(CreatureConstants.Dragon_Copper_GreatWyrm, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.TwentyFive, null, false)]
        [TestCase(CreatureConstants.Dragon_Gold_Wyrmling, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Five, 4, false)]
        [TestCase(CreatureConstants.Dragon_Gold_VeryYoung, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Seven, 5, false)]
        [TestCase(CreatureConstants.Dragon_Gold_Young, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Nine, 6, false)]
        [TestCase(CreatureConstants.Dragon_Gold_Juvenile, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Eleven, null, false)]
        [TestCase(CreatureConstants.Dragon_Gold_YoungAdult, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Fourteen, null, false)]
        [TestCase(CreatureConstants.Dragon_Gold_Adult, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Sixteen, null, false)]
        [TestCase(CreatureConstants.Dragon_Gold_MatureAdult, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Nineteen, null, false)]
        [TestCase(CreatureConstants.Dragon_Gold_Old, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.TwentyOne, null, false)]
        [TestCase(CreatureConstants.Dragon_Gold_VeryOld, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.TwentyTwo, null, false)]
        [TestCase(CreatureConstants.Dragon_Gold_Ancient, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.TwentyFour, null, false)]
        [TestCase(CreatureConstants.Dragon_Gold_Wyrm, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.TwentyFive, null, false)]
        [TestCase(CreatureConstants.Dragon_Gold_GreatWyrm, SizeConstants.Colossal, 30, 20, ChallengeRatingConstants.TwentySeven, null, false)]
        [TestCase(CreatureConstants.Dragon_Green_Wyrmling, SizeConstants.Small, 5, 5, ChallengeRatingConstants.Three, 5, false)]
        [TestCase(CreatureConstants.Dragon_Green_VeryYoung, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Four, 5, false)]
        [TestCase(CreatureConstants.Dragon_Green_Young, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Five, 5, false)]
        [TestCase(CreatureConstants.Dragon_Green_Juvenile, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Eight, 6, false)]
        [TestCase(CreatureConstants.Dragon_Green_YoungAdult, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Eleven, null, false)]
        [TestCase(CreatureConstants.Dragon_Green_Adult, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Thirteen, null, false)]
        [TestCase(CreatureConstants.Dragon_Green_MatureAdult, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Sixteen, null, false)]
        [TestCase(CreatureConstants.Dragon_Green_Old, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Eighteen, null, false)]
        [TestCase(CreatureConstants.Dragon_Green_VeryOld, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Nineteen, null, false)]
        [TestCase(CreatureConstants.Dragon_Green_Ancient, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.TwentyOne, null, false)]
        [TestCase(CreatureConstants.Dragon_Green_Wyrm, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.TwentyTwo, null, false)]
        [TestCase(CreatureConstants.Dragon_Green_GreatWyrm, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.TwentyFour, null, false)]
        [TestCase(CreatureConstants.Dragon_Red_Wyrmling, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Four, 4, false)]
        [TestCase(CreatureConstants.Dragon_Red_VeryYoung, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Five, 5, false)]
        [TestCase(CreatureConstants.Dragon_Red_Young, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Seven, 6, false)]
        [TestCase(CreatureConstants.Dragon_Red_Juvenile, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Ten, null, false)]
        [TestCase(CreatureConstants.Dragon_Red_YoungAdult, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Thirteen, null, false)]
        [TestCase(CreatureConstants.Dragon_Red_Adult, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Fifteen, null, false)]
        [TestCase(CreatureConstants.Dragon_Red_MatureAdult, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Eighteen, null, false)]
        [TestCase(CreatureConstants.Dragon_Red_Old, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.Twenty, null, false)]
        [TestCase(CreatureConstants.Dragon_Red_VeryOld, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.TwentyOne, null, false)]
        [TestCase(CreatureConstants.Dragon_Red_Ancient, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.TwentyThree, null, false)]
        [TestCase(CreatureConstants.Dragon_Red_Wyrm, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.TwentyFour, null, false)]
        [TestCase(CreatureConstants.Dragon_Red_GreatWyrm, SizeConstants.Colossal, 30, 20, ChallengeRatingConstants.TwentySix, null, false)]
        [TestCase(CreatureConstants.Dragon_Silver_Wyrmling, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Four, 4, false)]
        [TestCase(CreatureConstants.Dragon_Silver_VeryYoung, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Five, 4, false)]
        [TestCase(CreatureConstants.Dragon_Silver_Young, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Seven, 5, false)]
        [TestCase(CreatureConstants.Dragon_Silver_Juvenile, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Ten, null, false)]
        [TestCase(CreatureConstants.Dragon_Silver_YoungAdult, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Thirteen, null, false)]
        [TestCase(CreatureConstants.Dragon_Silver_Adult, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Fifteen, null, false)]
        [TestCase(CreatureConstants.Dragon_Silver_MatureAdult, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Eighteen, null, false)]
        [TestCase(CreatureConstants.Dragon_Silver_Old, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.Twenty, null, false)]
        [TestCase(CreatureConstants.Dragon_Silver_VeryOld, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.TwentyOne, null, false)]
        [TestCase(CreatureConstants.Dragon_Silver_Ancient, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.TwentyThree, null, false)]
        [TestCase(CreatureConstants.Dragon_Silver_Wyrm, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.TwentyFour, null, false)]
        [TestCase(CreatureConstants.Dragon_Silver_GreatWyrm, SizeConstants.Colossal, 30, 20, ChallengeRatingConstants.TwentySix, null, false)]
        [TestCase(CreatureConstants.Dragon_White_Wyrmling, SizeConstants.Tiny, 2.5, 0, ChallengeRatingConstants.Two, 2, false)]
        [TestCase(CreatureConstants.Dragon_White_VeryYoung, SizeConstants.Small, 5, 5, ChallengeRatingConstants.Three, 3, false)]
        [TestCase(CreatureConstants.Dragon_White_Young, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Four, 3, false)]
        [TestCase(CreatureConstants.Dragon_White_Juvenile, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Six, 5, false)]
        [TestCase(CreatureConstants.Dragon_White_YoungAdult, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Eight, null, false)]
        [TestCase(CreatureConstants.Dragon_White_Adult, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Ten, null, false)]
        [TestCase(CreatureConstants.Dragon_White_MatureAdult, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Twelve, null, false)]
        [TestCase(CreatureConstants.Dragon_White_Old, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Fifteen, null, false)]
        [TestCase(CreatureConstants.Dragon_White_VeryOld, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Seventeen, null, false)]
        [TestCase(CreatureConstants.Dragon_White_Ancient, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Eighteen, null, false)]
        [TestCase(CreatureConstants.Dragon_White_Wyrm, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.Nineteen, null, false)]
        [TestCase(CreatureConstants.Dragon_White_GreatWyrm, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.TwentyOne, null, false)]
        [TestCase(CreatureConstants.DragonTurtle, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Nine, null, false)]
        [TestCase(CreatureConstants.Dragonne, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Seven, 4, false)]
        [TestCase(CreatureConstants.Dretch, SizeConstants.Small, 5, 5, ChallengeRatingConstants.Two, 2, true)]
        [TestCase(CreatureConstants.Drider, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Seven, 4, true)]
        [TestCase(CreatureConstants.Dryad, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Three, 0, true)]
        [TestCase(CreatureConstants.Dwarf_Deep, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.OneHalf, 0, true)]
        [TestCase(CreatureConstants.Dwarf_Duergar, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.One, 1, true)]
        [TestCase(CreatureConstants.Dwarf_Hill, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.OneHalf, 0, true)]
        [TestCase(CreatureConstants.Dwarf_Mountain, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.OneHalf, 0, true)]
        [TestCase(CreatureConstants.Eagle, SizeConstants.Small, 5, 5, ChallengeRatingConstants.OneHalf, null, false)]
        [TestCase(CreatureConstants.Eagle_Giant, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Three, 2, false)]
        [TestCase(CreatureConstants.Efreeti, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Eight, null, false)]
        [TestCase(CreatureConstants.Elasmosaurus, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Seven, null, false)]
        [TestCase(CreatureConstants.Elemental_Air_Elder, SizeConstants.Huge, 15, 15, ChallengeRatingConstants.Eleven, null, false)]
        [TestCase(CreatureConstants.Elemental_Air_Greater, SizeConstants.Huge, 15, 15, ChallengeRatingConstants.Nine, null, false)]
        [TestCase(CreatureConstants.Elemental_Air_Huge, SizeConstants.Huge, 15, 15, ChallengeRatingConstants.Seven, null, false)]
        [TestCase(CreatureConstants.Elemental_Air_Large, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Five, null, false)]
        [TestCase(CreatureConstants.Elemental_Air_Medium, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Three, null, false)]
        [TestCase(CreatureConstants.Elemental_Air_Small, SizeConstants.Small, 5, 5, ChallengeRatingConstants.One, null, false)]
        [TestCase(CreatureConstants.Elemental_Earth_Elder, SizeConstants.Huge, 15, 15, ChallengeRatingConstants.Eleven, null, false)]
        [TestCase(CreatureConstants.Elemental_Earth_Greater, SizeConstants.Huge, 15, 15, ChallengeRatingConstants.Nine, null, false)]
        [TestCase(CreatureConstants.Elemental_Earth_Huge, SizeConstants.Huge, 15, 15, ChallengeRatingConstants.Seven, null, false)]
        [TestCase(CreatureConstants.Elemental_Earth_Large, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Five, null, false)]
        [TestCase(CreatureConstants.Elemental_Earth_Medium, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Three, null, false)]
        [TestCase(CreatureConstants.Elemental_Earth_Small, SizeConstants.Small, 5, 5, ChallengeRatingConstants.One, null, false)]
        [TestCase(CreatureConstants.Elemental_Fire_Elder, SizeConstants.Huge, 15, 15, ChallengeRatingConstants.Eleven, null, false)]
        [TestCase(CreatureConstants.Elemental_Fire_Greater, SizeConstants.Huge, 15, 15, ChallengeRatingConstants.Nine, null, false)]
        [TestCase(CreatureConstants.Elemental_Fire_Huge, SizeConstants.Huge, 15, 15, ChallengeRatingConstants.Seven, null, false)]
        [TestCase(CreatureConstants.Elemental_Fire_Large, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Five, null, false)]
        [TestCase(CreatureConstants.Elemental_Fire_Medium, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Three, null, false)]
        [TestCase(CreatureConstants.Elemental_Fire_Small, SizeConstants.Small, 5, 5, ChallengeRatingConstants.One, null, false)]
        [TestCase(CreatureConstants.Elemental_Water_Elder, SizeConstants.Huge, 15, 15, ChallengeRatingConstants.Eleven, null, false)]
        [TestCase(CreatureConstants.Elemental_Water_Greater, SizeConstants.Huge, 15, 15, ChallengeRatingConstants.Nine, null, false)]
        [TestCase(CreatureConstants.Elemental_Water_Huge, SizeConstants.Huge, 15, 15, ChallengeRatingConstants.Seven, null, false)]
        [TestCase(CreatureConstants.Elemental_Water_Large, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Five, null, false)]
        [TestCase(CreatureConstants.Elemental_Water_Medium, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Three, null, false)]
        [TestCase(CreatureConstants.Elemental_Water_Small, SizeConstants.Small, 5, 5, ChallengeRatingConstants.One, null, false)]
        [TestCase(CreatureConstants.Elephant, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Seven, null, false)]
        [TestCase(CreatureConstants.Elf_Aquatic, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.OneHalf, 0, true)]
        [TestCase(CreatureConstants.Elf_Drow, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.One, 2, true)]
        [TestCase(CreatureConstants.Elf_Gray, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.OneHalf, 0, true)]
        [TestCase(CreatureConstants.Elf_Half, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.OneHalf, 0, true)]
        [TestCase(CreatureConstants.Elf_High, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.OneHalf, 0, true)]
        [TestCase(CreatureConstants.Elf_Wild, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.OneHalf, 0, true)]
        [TestCase(CreatureConstants.Elf_Wood, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.OneHalf, 0, true)]
        [TestCase(CreatureConstants.Erinyes, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Eight, 7, true)]
        [TestCase(CreatureConstants.EtherealFilcher, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Three, null, false)]
        [TestCase(CreatureConstants.EtherealMarauder, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Three, null, false)]
        [TestCase(CreatureConstants.Ettercap, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Three, 4, false)]
        [TestCase(CreatureConstants.Ettin, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Six, 5, true)]
        [TestCase(CreatureConstants.FireBeetle_Giant, SizeConstants.Small, 5, 5, ChallengeRatingConstants.OneThird, null, false)]
        [TestCase(CreatureConstants.FormianMyrmarch, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Ten, null, true)]
        [TestCase(CreatureConstants.FormianQueen, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Seventeen, null, true)]
        [TestCase(CreatureConstants.FormianTaskmaster, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Seven, null, true)]
        [TestCase(CreatureConstants.FormianWarrior, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Three, null, true)]
        [TestCase(CreatureConstants.FormianWorker, SizeConstants.Small, 5, 5, ChallengeRatingConstants.OneHalf, null, true)]
        [TestCase(CreatureConstants.FrostWorm, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Twelve, null, false)]
        [TestCase(CreatureConstants.Gargoyle, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Four, 5, true)]
        [TestCase(CreatureConstants.Gargoyle_Kapoacinth, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Four, 5, true)]
        [TestCase(CreatureConstants.GelatinousCube, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Three, null, false)]
        [TestCase(CreatureConstants.Ghaele, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Thirteen, null, true)]
        [TestCase(CreatureConstants.Ghoul, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.One, null, false)]
        [TestCase(CreatureConstants.Ghoul_Ghast, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Three, null, false)]
        [TestCase(CreatureConstants.Ghoul_Lacedon, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.One, null, false)]
        [TestCase(CreatureConstants.Giant_Cloud, SizeConstants.Huge, 15, 15, ChallengeRatingConstants.Eleven, 0, true)]
        [TestCase(CreatureConstants.Giant_Fire, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Ten, 4, true)]
        [TestCase(CreatureConstants.Giant_Frost, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Nine, 4, true)]
        [TestCase(CreatureConstants.Giant_Hill, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Seven, 4, true)]
        [TestCase(CreatureConstants.Giant_Stone, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Eight, 4, true)]
        [TestCase(CreatureConstants.Giant_Stone_Elder, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Nine, 6, true)]
        [TestCase(CreatureConstants.Giant_Storm, SizeConstants.Huge, 15, 15, ChallengeRatingConstants.Thirteen, 0, true)]
        [TestCase(CreatureConstants.GibberingMouther, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Five, null, false)]
        [TestCase(CreatureConstants.Girallon, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Six, null, false)]
        [TestCase(CreatureConstants.Githyanki, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.One, 2, true)]
        [TestCase(CreatureConstants.Githzerai, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.One, 2, true)]
        [TestCase(CreatureConstants.Glabrezu, SizeConstants.Huge, 15, 15, ChallengeRatingConstants.Thirteen, null, false)]
        [TestCase(CreatureConstants.Gnoll, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.One, 1, true)]
        [TestCase(CreatureConstants.Gnome_Forest, SizeConstants.Small, 5, 5, ChallengeRatingConstants.OneHalf, 0, true)]
        [TestCase(CreatureConstants.Gnome_Rock, SizeConstants.Small, 5, 5, ChallengeRatingConstants.OneHalf, 0, true)]
        [TestCase(CreatureConstants.Gnome_Svirfneblin, SizeConstants.Small, 5, 5, ChallengeRatingConstants.One, 3, true)]
        [TestCase(CreatureConstants.Goblin, SizeConstants.Small, 5, 5, ChallengeRatingConstants.OneThird, 0, true)]
        [TestCase(CreatureConstants.Golem_Clay, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Ten, null, false)]
        [TestCase(CreatureConstants.Golem_Flesh, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Seven, null, false)]
        [TestCase(CreatureConstants.Golem_Iron, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Thirteen, null, false)]
        [TestCase(CreatureConstants.Golem_Stone, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Eleven, null, false)]
        [TestCase(CreatureConstants.Golem_Stone_Greater, SizeConstants.Huge, 15, 15, ChallengeRatingConstants.Sixteen, null, false)]
        [TestCase(CreatureConstants.Gorgon, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Eight, null, false)]
        [TestCase(CreatureConstants.GrayOoze, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Four, null, false)]
        [TestCase(CreatureConstants.GrayRender, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Eight, 5, false)]
        [TestCase(CreatureConstants.GreenHag, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Five, 0, true)]
        [TestCase(CreatureConstants.Grick, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Three, null, false)]
        [TestCase(CreatureConstants.Grig, SizeConstants.Tiny, 2.5, 0, ChallengeRatingConstants.One, 3, true)]
        [TestCase(CreatureConstants.Gynosphinx, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Eight, 4, false)]
        [TestCase(CreatureConstants.Halfling_Deep, SizeConstants.Small, 5, 5, ChallengeRatingConstants.OneHalf, 0, true)]
        [TestCase(CreatureConstants.Halfling_Lightfoot, SizeConstants.Small, 5, 5, ChallengeRatingConstants.OneHalf, 0, true)]
        [TestCase(CreatureConstants.Halfling_Tallfellow, SizeConstants.Small, 5, 5, ChallengeRatingConstants.OneHalf, 0, true)]
        [TestCase(CreatureConstants.Hawk, SizeConstants.Tiny, 2.5, 0, ChallengeRatingConstants.OneThird, null, false)]
        [TestCase(CreatureConstants.Hellcat_Bezekira, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Seven, null, false)]
        [TestCase(CreatureConstants.Hellwasp_Swarm, SizeConstants.Diminutive, 10, 0, ChallengeRatingConstants.Eight, null, false)]
        [TestCase(CreatureConstants.Hezrou, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Eleven, 9, true)]
        [TestCase(CreatureConstants.Hieracosphinx, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Five, 3, false)]
        [TestCase(CreatureConstants.HornedDevil_Cornugon, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Sixteen, null, false)]
        [TestCase(CreatureConstants.Horse_Heavy, SizeConstants.Large, 10, 5, ChallengeRatingConstants.One, null, false)]
        [TestCase(CreatureConstants.Horse_Heavy_War, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Two, null, false)]
        [TestCase(CreatureConstants.Horse_Light, SizeConstants.Large, 10, 5, ChallengeRatingConstants.One, null, false)]
        [TestCase(CreatureConstants.Horse_Light_War, SizeConstants.Large, 10, 5, ChallengeRatingConstants.One, null, false)]
        [TestCase(CreatureConstants.Human, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.OneHalf, 0, true)]
        [TestCase(CreatureConstants.Hyena, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.One, null, false)]
        [TestCase(CreatureConstants.IceDevil_Gelugon, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Thirteen, null, false)]
        [TestCase(CreatureConstants.Janni, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Four, 5, true)]
        [TestCase(CreatureConstants.Lemure, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.One, null, false)]
        [TestCase(CreatureConstants.Leopard, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Two, null, false)]
        [TestCase(CreatureConstants.Lion, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Three, null, false)]
        [TestCase(CreatureConstants.Lion_Dire, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Five, null, false)]
        [TestCase(CreatureConstants.Lizard, SizeConstants.Tiny, 2.5, 0, ChallengeRatingConstants.OneSixth, null, false)]
        [TestCase(CreatureConstants.Lizard_Monitor, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Two, null, false)]
        [TestCase(CreatureConstants.Locust_Swarm, SizeConstants.Diminutive, 10, 0, ChallengeRatingConstants.Three, null, false)]
        [TestCase(CreatureConstants.MantaRay, SizeConstants.Large, 10, 5, ChallengeRatingConstants.One, null, false)]
        [TestCase(CreatureConstants.Marilith, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Seventeen, null, true)]
        [TestCase(CreatureConstants.Megaraptor, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Six, null, false)]
        [TestCase(CreatureConstants.Monkey, SizeConstants.Tiny, 2.5, 0, ChallengeRatingConstants.OneSixth, null, false)]
        [TestCase(CreatureConstants.Mule, SizeConstants.Large, 10, 5, ChallengeRatingConstants.One, null, false)]
        [TestCase(CreatureConstants.Nalfeshnee, SizeConstants.Huge, 15, 15, ChallengeRatingConstants.Fourteen, null, false)]
        [TestCase(CreatureConstants.Nixie, SizeConstants.Small, 5, 5, ChallengeRatingConstants.One, 3, true)]
        [TestCase(CreatureConstants.OchreJelly, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Five, null, false)]
        [TestCase(CreatureConstants.Octopus, SizeConstants.Small, 5, 5, ChallengeRatingConstants.One, null, false)]
        [TestCase(CreatureConstants.Octopus_Giant, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Eight, null, false)]
        [TestCase(CreatureConstants.Owl, SizeConstants.Tiny, 2.5, 0, ChallengeRatingConstants.OneFourth, null, false)]
        [TestCase(CreatureConstants.PitFiend, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Twenty, null, true)]
        [TestCase(CreatureConstants.Pixie, SizeConstants.Small, 5, 5, ChallengeRatingConstants.One, 3, true)]
        [TestCase(CreatureConstants.Pixie_WithIrresistableDance, SizeConstants.Small, 5, 5, ChallengeRatingConstants.One, 3, true)]
        [TestCase(CreatureConstants.Pony, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.OneFourth, null, false)]
        [TestCase(CreatureConstants.Pony_War, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.OneHalf, null, false)]
        [TestCase(CreatureConstants.Porpoise, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.OneHalf, null, false)]
        [TestCase(CreatureConstants.PrayingMantis_Giant, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Three, null, false)]
        [TestCase(CreatureConstants.Quasit, SizeConstants.Tiny, 2.5, 0, ChallengeRatingConstants.Two, null, false)]
        [TestCase(CreatureConstants.Rat, SizeConstants.Tiny, 2.5, 0, ChallengeRatingConstants.OneEighth, null, false)]
        [TestCase(CreatureConstants.Rat_Dire, SizeConstants.Small, 5, 5, ChallengeRatingConstants.OneThird, null, false)]
        [TestCase(CreatureConstants.Rat_Swarm, SizeConstants.Tiny, 10, 0, ChallengeRatingConstants.Two, null, false)]
        [TestCase(CreatureConstants.Raven, SizeConstants.Tiny, 2.5, 0, ChallengeRatingConstants.OneSixth, null, false)]
        [TestCase(CreatureConstants.Retriever, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Eleven, null, false)]
        [TestCase(CreatureConstants.Rhinoceras, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Four, null, false)]
        [TestCase(CreatureConstants.Scorpion_Monstrous_Colossal, SizeConstants.Colossal, 40, 30, ChallengeRatingConstants.Twelve, null, false)]
        [TestCase(CreatureConstants.Scorpion_Monstrous_Gargantuan, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.Ten, null, false)]
        [TestCase(CreatureConstants.Scorpion_Monstrous_Huge, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Seven, null, false)]
        [TestCase(CreatureConstants.Scorpion_Monstrous_Large, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Three, null, false)]
        [TestCase(CreatureConstants.Scorpion_Monstrous_Medium, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.One, null, false)]
        [TestCase(CreatureConstants.Scorpion_Monstrous_Small, SizeConstants.Small, 5, 5, ChallengeRatingConstants.OneHalf, null, false)]
        [TestCase(CreatureConstants.Scorpion_Monstrous_Tiny, SizeConstants.Tiny, 2.5, 0, ChallengeRatingConstants.OneFourth, null, false)]
        [TestCase(CreatureConstants.SeaHag, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Four, 0, true)]
        [TestCase(CreatureConstants.Shark_Dire, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Nine, null, false)]
        [TestCase(CreatureConstants.Shark_Huge, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Four, null, false)]
        [TestCase(CreatureConstants.Shark_Large, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Two, null, false)]
        [TestCase(CreatureConstants.Shark_Medium, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.One, null, false)]
        [TestCase(CreatureConstants.Shrieker, SizeConstants.Medium, 5, 0, ChallengeRatingConstants.One, null, false)]
        [TestCase(CreatureConstants.Slaad_Blue, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Eight, 6, true)]
        [TestCase(CreatureConstants.Slaad_Death, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Thirteen, null, true)]
        [TestCase(CreatureConstants.Slaad_Gray, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Ten, 6, true)]
        [TestCase(CreatureConstants.Slaad_Green, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Nine, 7, true)]
        [TestCase(CreatureConstants.Slaad_Red, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Seven, 6, true)]
        [TestCase(CreatureConstants.Snake_Constrictor, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Two, null, false)]
        [TestCase(CreatureConstants.Snake_Constrictor_Giant, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Five, null, false)]
        [TestCase(CreatureConstants.Snake_Viper_Huge, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Three, null, false)]
        [TestCase(CreatureConstants.Snake_Viper_Large, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Two, null, false)]
        [TestCase(CreatureConstants.Snake_Viper_Medium, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.One, null, false)]
        [TestCase(CreatureConstants.Snake_Viper_Small, SizeConstants.Small, 5, 5, ChallengeRatingConstants.OneHalf, null, false)]
        [TestCase(CreatureConstants.Snake_Viper_Tiny, SizeConstants.Tiny, 2.5, 0, ChallengeRatingConstants.OneThird, null, false)]
        [TestCase(CreatureConstants.Spider_Monstrous_Colossal, SizeConstants.Colossal, 40, 30, ChallengeRatingConstants.Eleven, null, false)]
        [TestCase(CreatureConstants.Spider_Monstrous_Gargantuan, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.Eight, null, false)]
        [TestCase(CreatureConstants.Spider_Monstrous_Huge, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Five, null, false)]
        [TestCase(CreatureConstants.Spider_Monstrous_Large, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Two, null, false)]
        [TestCase(CreatureConstants.Spider_Monstrous_Medium, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.One, null, false)]
        [TestCase(CreatureConstants.Spider_Monstrous_Small, SizeConstants.Small, 5, 5, ChallengeRatingConstants.OneHalf, null, false)]
        [TestCase(CreatureConstants.Spider_Monstrous_Tiny, SizeConstants.Tiny, 2.5, 0, ChallengeRatingConstants.OneFourth, null, false)]
        [TestCase(CreatureConstants.Spider_Swarm, SizeConstants.Diminutive, 10, 0, ChallengeRatingConstants.One, null, false)]
        [TestCase(CreatureConstants.Squid, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.One, null, false)]
        [TestCase(CreatureConstants.Squid_Giant, SizeConstants.Huge, 15, 15, ChallengeRatingConstants.Nine, null, false)]
        [TestCase(CreatureConstants.StagBeetle_Giant, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Four, null, false)]
        [TestCase(CreatureConstants.Succubus, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Seven, 6, true)]
        [TestCase(CreatureConstants.Tiefling, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.OneHalf, 1, true)]
        [TestCase(CreatureConstants.Tiger, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Four, null, false)]
        [TestCase(CreatureConstants.Tiger_Dire, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Eight, null, false)]
        [TestCase(CreatureConstants.Toad, SizeConstants.Diminutive, 1, 0, ChallengeRatingConstants.OneTenth, null, false)]
        [TestCase(CreatureConstants.Triceratops, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Nine, null, false)]
        [TestCase(CreatureConstants.Tyrannosaurus, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Eight, null, false)]
        [TestCase(CreatureConstants.VioletFungus, SizeConstants.Medium, 5, 10, ChallengeRatingConstants.Three, null, false)]
        [TestCase(CreatureConstants.Vrock, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Nine, 8, true)]
        [TestCase(CreatureConstants.Wasp_Giant, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Three, null, false)]
        [TestCase(CreatureConstants.Weasel, SizeConstants.Tiny, 2.5, 0, ChallengeRatingConstants.OneFourth, null, false)]
        [TestCase(CreatureConstants.Weasel_Dire, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Two, null, false)]
        [TestCase(CreatureConstants.Whale_Baleen, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.Six, null, false)]
        [TestCase(CreatureConstants.Whale_Cachalot, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.Seven, null, false)]
        [TestCase(CreatureConstants.Whale_Orca, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Five, null, false)]
        [TestCase(CreatureConstants.Wolf, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.One, null, false)]
        [TestCase(CreatureConstants.Wolf_Dire, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Three, null, false)]
        [TestCase(CreatureConstants.Wolverine, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Two, null, false)]
        [TestCase(CreatureConstants.Wolverine_Dire, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Four, null, false)]
        public void CreatureData(string creature, string size, double space, double reach, string challengeRating, int? levelAdjustment, bool canUseEquipment)
        {
            var collection = new string[6];
            collection[DataIndexConstants.CreatureData.ChallengeRating] = challengeRating;
            collection[DataIndexConstants.CreatureData.LevelAdjustment] = Convert.ToString(levelAdjustment);
            collection[DataIndexConstants.CreatureData.Reach] = reach.ToString();
            collection[DataIndexConstants.CreatureData.Size] = size;
            collection[DataIndexConstants.CreatureData.Space] = space.ToString();
            collection[DataIndexConstants.CreatureData.CanUseEquipment] = canUseEquipment.ToString();

            Data(creature, collection);
        }

        [Test]
        public void AllCreaturesHaveCorrectNumberOfEntries()
        {
            var wrongEntries = table.Where(kvp => kvp.Value.Count() != 6);
            var wrongCreatures = wrongEntries.Select(kvp => kvp.Key);
            var message = $"{wrongCreatures.Count()} of {table.Count} have incorrect number of entries";
            Assert.That(wrongCreatures, Is.Empty, message);
        }

        [Test]
        public void AllCreaturesHaveCorrectChallengeRatings()
        {
            var challengeRatings = new[]
            {
                ChallengeRatingConstants.Eight,
                ChallengeRatingConstants.Eighteen,
                ChallengeRatingConstants.Eleven,
                ChallengeRatingConstants.Fifteen,
                ChallengeRatingConstants.Five,
                ChallengeRatingConstants.Four,
                ChallengeRatingConstants.Fourteen,
                ChallengeRatingConstants.Nine,
                ChallengeRatingConstants.Nineteen,
                ChallengeRatingConstants.One,
                ChallengeRatingConstants.OneEighth,
                ChallengeRatingConstants.OneFourth,
                ChallengeRatingConstants.OneHalf,
                ChallengeRatingConstants.OneSixth,
                ChallengeRatingConstants.OneTenth,
                ChallengeRatingConstants.OneThird,
                ChallengeRatingConstants.Seven,
                ChallengeRatingConstants.Seventeen,
                ChallengeRatingConstants.Six,
                ChallengeRatingConstants.Sixteen,
                ChallengeRatingConstants.Ten,
                ChallengeRatingConstants.Thirteen,
                ChallengeRatingConstants.Thirty,
                ChallengeRatingConstants.Three,
                ChallengeRatingConstants.Twelve,
                ChallengeRatingConstants.Twenty,
                ChallengeRatingConstants.TwentyEight,
                ChallengeRatingConstants.TwentyFive,
                ChallengeRatingConstants.TwentyFour,
                ChallengeRatingConstants.TwentyNine,
                ChallengeRatingConstants.TwentyOne,
                ChallengeRatingConstants.TwentySeven,
                ChallengeRatingConstants.TwentySix,
                ChallengeRatingConstants.TwentyThree,
                ChallengeRatingConstants.TwentyTwo,
                ChallengeRatingConstants.Two,
                ChallengeRatingConstants.Zero,
            };

            foreach (var kvp in table)
            {
                var creature = kvp.Key;
                var data = kvp.Value.ToArray();

                Assert.That(data[DataIndexConstants.CreatureData.ChallengeRating], Is.Not.Empty, creature);
                Assert.That(new[] { data[DataIndexConstants.CreatureData.ChallengeRating] }, Is.SubsetOf(challengeRatings), creature);
                Assert.That(data[DataIndexConstants.CreatureData.ChallengeRating], Is.Not.EqualTo(ChallengeRatingConstants.Zero), creature);
            }
        }

        [Test]
        public void AllCreaturesHaveCorrectSizes()
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

            foreach (var kvp in table)
            {
                var creature = kvp.Key;
                var data = kvp.Value.ToArray();

                Assert.That(data[DataIndexConstants.CreatureData.Size], Is.Not.Empty, creature);
                Assert.That(new[] { data[DataIndexConstants.CreatureData.Size] }, Is.SubsetOf(sizes), creature);
            }
        }

        [Test]
        public void AllCreaturesHaveCorrectReach()
        {
            foreach (var kvp in table)
            {
                var creature = kvp.Key;
                var data = kvp.Value.ToArray();
                var reach = 0d;

                Assert.That(double.TryParse(data[DataIndexConstants.CreatureData.Reach], out reach), Is.True, creature);
                Assert.That(reach, Is.Not.Negative, creature);
            }
        }

        [Test]
        public void AllCreaturesHaveCorrectSpace()
        {
            foreach (var kvp in table)
            {
                var creature = kvp.Key;
                var data = kvp.Value.ToArray();
                var space = 0d;

                Assert.That(double.TryParse(data[DataIndexConstants.CreatureData.Space], out space), Is.True, creature);
                Assert.That(space, Is.Positive, creature);
            }
        }

        [Test]
        public void AllCreaturesHaveCorrectLevelAdjustment()
        {
            foreach (var kvp in table)
            {
                var creature = kvp.Key;
                var data = kvp.Value.ToArray();
                var levelAdjustment = 0;

                if (!string.IsNullOrEmpty(data[DataIndexConstants.CreatureData.LevelAdjustment]))
                {
                    Assert.That(int.TryParse(data[DataIndexConstants.CreatureData.LevelAdjustment], out levelAdjustment), Is.True, creature);
                    Assert.That(levelAdjustment, Is.Not.Negative, creature);
                }
                else
                {
                    Assert.That(data[DataIndexConstants.CreatureData.LevelAdjustment], Is.Empty, creature);
                }
            }
        }

        [Test]
        public void AllCreaturesHaveCorrectCanUseEquipment()
        {
            foreach (var kvp in table)
            {
                var creature = kvp.Key;
                var data = kvp.Value.ToArray();

                Assert.That(data[DataIndexConstants.CreatureData.CanUseEquipment], Is.EqualTo(bool.TrueString).Or.EqualTo(bool.FalseString));
            }
        }
    }
}
