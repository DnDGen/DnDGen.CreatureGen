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

        [TestCase(CreatureConstants.Aasimar, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.OneHalf, 1)]
        [TestCase(CreatureConstants.Aboleth, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Seven, 0)]
        [TestCase(CreatureConstants.Achaierai, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Five, null)]
        [TestCase(CreatureConstants.Allip, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Three, null)]
        [TestCase(CreatureConstants.Androsphinx, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Nine, 5)]
        [TestCase(CreatureConstants.Angel_AstralDeva, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Fourteen, 8)]
        [TestCase(CreatureConstants.Angel_Planetar, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Sixteen, null)]
        [TestCase(CreatureConstants.Angel_Solar, SizeConstants.Large, 10, 10, ChallengeRatingConstants.TwentyThree, null)]
        [TestCase(CreatureConstants.AnimatedObject_Colossal, SizeConstants.Colossal, 30, 20, ChallengeRatingConstants.Ten, null)]
        [TestCase(CreatureConstants.AnimatedObject_Gargantuan, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.Seven, null)]
        [TestCase(CreatureConstants.AnimatedObject_Huge, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Five, null)]
        [TestCase(CreatureConstants.AnimatedObject_Large, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Three, null)]
        [TestCase(CreatureConstants.AnimatedObject_Medium, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Two, null)]
        [TestCase(CreatureConstants.AnimatedObject_Small, SizeConstants.Small, 5, 5, ChallengeRatingConstants.One, null)]
        [TestCase(CreatureConstants.AnimatedObject_Tiny, SizeConstants.Tiny, 2.5, 0, ChallengeRatingConstants.OneHalf, null)]
        [TestCase(CreatureConstants.Ankheg, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Three, null)]
        [TestCase(CreatureConstants.Annis, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Six, 0)]
        [TestCase(CreatureConstants.Ape, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Two, null)]
        [TestCase(CreatureConstants.Ape_Dire, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Three, null)]
        [TestCase(CreatureConstants.Aranea, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Four, 4)]
        [TestCase(CreatureConstants.Arrowhawk_Adult, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Five, null)]
        [TestCase(CreatureConstants.Arrowhawk_Elder, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Eight, null)]
        [TestCase(CreatureConstants.Arrowhawk_Juvenile, SizeConstants.Small, 5, 5, ChallengeRatingConstants.Three, null)]
        [TestCase(CreatureConstants.AssassinVine, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Three, null)]
        [TestCase(CreatureConstants.Athach, SizeConstants.Huge, 15, 15, ChallengeRatingConstants.Eight, 5)]
        [TestCase(CreatureConstants.Avoral, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Nine, null)]
        [TestCase(CreatureConstants.Azer, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Two, 4)]
        [TestCase(CreatureConstants.Babau, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Six, null)]
        [TestCase(CreatureConstants.Baboon, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.OneHalf, null)]
        [TestCase(CreatureConstants.Badger, SizeConstants.Small, 5, 5, ChallengeRatingConstants.OneHalf, null)]
        [TestCase(CreatureConstants.Badger_Dire, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Two, null)]
        [TestCase(CreatureConstants.Balor, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Twenty, null)]
        [TestCase(CreatureConstants.BarbedDevil_Hamatula, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Eleven, null)]
        [TestCase(CreatureConstants.Barghest, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Four, null)]
        [TestCase(CreatureConstants.Barghest_Greater, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Five, null)]
        [TestCase(CreatureConstants.Basilisk, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Five, null)]
        [TestCase(CreatureConstants.Basilisk_AbyssalGreater, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Twelve, null)]
        [TestCase(CreatureConstants.Bat, SizeConstants.Diminutive, 1, 0, ChallengeRatingConstants.OneTenth, null)]
        [TestCase(CreatureConstants.Bat_Dire, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Two, null)]
        [TestCase(CreatureConstants.Bat_Swarm, SizeConstants.Diminutive, 10, 0, ChallengeRatingConstants.Two, null)]
        [TestCase(CreatureConstants.Bear_Black, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Two, null)]
        [TestCase(CreatureConstants.Bear_Brown, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Four, null)]
        [TestCase(CreatureConstants.Bear_Dire, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Seven, null)]
        [TestCase(CreatureConstants.Bear_Polar, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Four, null)]
        [TestCase(CreatureConstants.BeardedDevil_Barbazu, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Five, 6)]
        [TestCase(CreatureConstants.Bebilith, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Ten, null)]
        [TestCase(CreatureConstants.Behir, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Eight, null)]
        [TestCase(CreatureConstants.Beholder, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Thirteen, null)]
        [TestCase(CreatureConstants.Beholder_Gauth, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Six, null)]
        [TestCase(CreatureConstants.Belker, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Six, null)]
        [TestCase(CreatureConstants.Bison, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Two, null)]
        [TestCase(CreatureConstants.BlackPudding, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Seven, null)]
        [TestCase(CreatureConstants.BlackPudding_Elder, SizeConstants.Gargantuan, 20, 20, ChallengeRatingConstants.Twelve, null)]
        [TestCase(CreatureConstants.BlinkDog, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Two, 2)]
        [TestCase(CreatureConstants.Boar, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Two, null)]
        [TestCase(CreatureConstants.Boar_Dire, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Four, null)]
        [TestCase(CreatureConstants.Bodak, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Eight, null)]
        [TestCase(CreatureConstants.BoneDevil_Osyluth, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Nine, null)]
        [TestCase(CreatureConstants.Bralani, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Six, 5)]
        [TestCase(CreatureConstants.Bugbear, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Two, 1)]
        [TestCase(CreatureConstants.Bulette, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Seven, null)]
        [TestCase(CreatureConstants.Camel_Bactrian, SizeConstants.Large, 10, 5, ChallengeRatingConstants.One, null)]
        [TestCase(CreatureConstants.Camel_Dromedary, SizeConstants.Large, 10, 5, ChallengeRatingConstants.One, null)]
        [TestCase(CreatureConstants.CarrionCrawler, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Four, null)]
        [TestCase(CreatureConstants.Cat, SizeConstants.Tiny, 2.5, 0, ChallengeRatingConstants.OneFourth, null)]
        [TestCase(CreatureConstants.Centaur, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Three, 2)]
        [TestCase(CreatureConstants.Centipede_Swarm, SizeConstants.Diminutive, 10, 0, ChallengeRatingConstants.Four, null)]
        [TestCase(CreatureConstants.ChainDevil_Kyton, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Six, 6)]
        [TestCase(CreatureConstants.ChaosBeast, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Seven, null)]
        [TestCase(CreatureConstants.Cheetah, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Two, null)]
        [TestCase(CreatureConstants.Chimera, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Seven, 2)]
        [TestCase(CreatureConstants.Choker, SizeConstants.Small, 5, 10, ChallengeRatingConstants.Two, null)]
        [TestCase(CreatureConstants.Criosphinx, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Seven, 3)]
        [TestCase(CreatureConstants.Dretch, SizeConstants.Small, 5, 5, ChallengeRatingConstants.Two, 2)]
        [TestCase(CreatureConstants.Erinyes, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Eight, 7)]
        [TestCase(CreatureConstants.GelatinousCube, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Three, null)]
        [TestCase(CreatureConstants.Glabrezu, SizeConstants.Huge, 15, 15, ChallengeRatingConstants.Thirteen, null)]
        [TestCase(CreatureConstants.GrayOoze, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Four, null)]
        [TestCase(CreatureConstants.GreenHag, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Five, 0)]
        [TestCase(CreatureConstants.Gynosphinx, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Eight, 4)]
        [TestCase(CreatureConstants.Hellcat_Bezekira, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Seven, null)]
        [TestCase(CreatureConstants.Hellwasp_Swarm, SizeConstants.Diminutive, 10, 0, ChallengeRatingConstants.Eight, null)]
        [TestCase(CreatureConstants.Hezrou, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Eleven, 9)]
        [TestCase(CreatureConstants.Hieracosphinx, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Five, 3)]
        [TestCase(CreatureConstants.HornedDevil_Cornugon, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Sixteen, null)]
        [TestCase(CreatureConstants.IceDevil_Gelugon, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Thirteen, null)]
        [TestCase(CreatureConstants.Lemure, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.One, null)]
        [TestCase(CreatureConstants.Lion, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Three, null)]
        [TestCase(CreatureConstants.Lion_Dire, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Five, null)]
        [TestCase(CreatureConstants.Locust_Swarm, SizeConstants.Diminutive, 10, 0, ChallengeRatingConstants.Three, null)]
        [TestCase(CreatureConstants.Marilith, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Seventeen, null)]
        [TestCase(CreatureConstants.Nalfeshnee, SizeConstants.Huge, 15, 15, ChallengeRatingConstants.Fourteen, null)]
        [TestCase(CreatureConstants.OchreJelly, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Five, null)]
        [TestCase(CreatureConstants.PitFiend, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Twenty, null)]
        [TestCase(CreatureConstants.Quasit, SizeConstants.Tiny, 2.5, 0, ChallengeRatingConstants.Two, null)]
        [TestCase(CreatureConstants.Rat, SizeConstants.Tiny, 2.5, 0, ChallengeRatingConstants.OneEighth, null)]
        [TestCase(CreatureConstants.Rat_Dire, SizeConstants.Small, 5, 5, ChallengeRatingConstants.OneThird, null)]
        [TestCase(CreatureConstants.Rat_Swarm, SizeConstants.Tiny, 10, 0, ChallengeRatingConstants.Two, null)]
        [TestCase(CreatureConstants.Retriever, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Eleven, null)]
        [TestCase(CreatureConstants.SeaHag, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Four, 0)]
        [TestCase(CreatureConstants.Shark_Dire, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Nine, null)]
        [TestCase(CreatureConstants.Shark_Huge, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Four, null)]
        [TestCase(CreatureConstants.Shark_Large, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Two, null)]
        [TestCase(CreatureConstants.Shark_Medium, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.One, null)]
        [TestCase(CreatureConstants.Slaad_Blue, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Eight, 6)]
        [TestCase(CreatureConstants.Slaad_Death, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Thirteen, null)]
        [TestCase(CreatureConstants.Slaad_Gray, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Ten, 6)]
        [TestCase(CreatureConstants.Slaad_Green, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Nine, 7)]
        [TestCase(CreatureConstants.Slaad_Red, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Seven, 6)]
        [TestCase(CreatureConstants.Snake_Constrictor, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Two, null)]
        [TestCase(CreatureConstants.Snake_Constrictor_Giant, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Five, null)]
        [TestCase(CreatureConstants.Snake_Viper_Huge, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Three, null)]
        [TestCase(CreatureConstants.Snake_Viper_Large, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Two, null)]
        [TestCase(CreatureConstants.Snake_Viper_Medium, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.One, null)]
        [TestCase(CreatureConstants.Snake_Viper_Small, SizeConstants.Small, 5, 5, ChallengeRatingConstants.OneHalf, null)]
        [TestCase(CreatureConstants.Snake_Viper_Tiny, SizeConstants.Tiny, 2.5, 0, ChallengeRatingConstants.OneThird, null)]
        [TestCase(CreatureConstants.Spider_Swarm, SizeConstants.Diminutive, 10, 0, ChallengeRatingConstants.One, null)]
        [TestCase(CreatureConstants.Succubus, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Seven, 6)]
        [TestCase(CreatureConstants.Tiefling, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.OneHalf, 1)]
        [TestCase(CreatureConstants.Tiger, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Four, null)]
        [TestCase(CreatureConstants.Tiger_Dire, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Eight, null)]
        [TestCase(CreatureConstants.Vrock, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Nine, 8)]
        [TestCase(CreatureConstants.Weasel, SizeConstants.Tiny, 2.5, 0, ChallengeRatingConstants.OneFourth, null)]
        [TestCase(CreatureConstants.Weasel_Dire, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Two, null)]
        [TestCase(CreatureConstants.Whale_Baleen, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.Six, null)]
        [TestCase(CreatureConstants.Whale_Cachalot, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.Seven, null)]
        [TestCase(CreatureConstants.Whale_Orca, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Five, null)]
        [TestCase(CreatureConstants.Wolf, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.One, null)]
        [TestCase(CreatureConstants.Wolf_Dire, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Three, null)]
        [TestCase(CreatureConstants.Wolverine, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Two, null)]
        [TestCase(CreatureConstants.Wolverine_Dire, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Four, null)]
        public void CreatureData(string creature, string size, double space, double reach, string challengeRating, int? levelAdjustment)
        {
            var collection = new string[5];
            collection[DataIndexConstants.CreatureData.ChallengeRating] = challengeRating;
            collection[DataIndexConstants.CreatureData.LevelAdjustment] = Convert.ToString(levelAdjustment);
            collection[DataIndexConstants.CreatureData.Reach] = reach.ToString();
            collection[DataIndexConstants.CreatureData.Size] = size;
            collection[DataIndexConstants.CreatureData.Space] = space.ToString();

            Data(creature, collection);
        }

        [Test]
        public void AllCreaturesHaveCorrectEntries()
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
            };

            var wrongEntries = table.Where(kvp => kvp.Value.Count() != 5);
            var wrongCreatures = wrongEntries.Select(kvp => kvp.Key);
            var message = $"{wrongCreatures.Count()} of {table.Count} incorrect";
            Assert.That(wrongCreatures, Is.Empty, message);

            foreach (var kvp in table)
            {
                var creature = kvp.Key;
                var data = kvp.Value.ToArray();
                var doubleJunk = 0d;
                var intJunk = 0;

                Assert.That(data.Length, Is.EqualTo(5), creature);
                Assert.That(data[DataIndexConstants.CreatureData.ChallengeRating], Is.Not.Empty, creature);
                Assert.That(new[] { data[DataIndexConstants.CreatureData.Size] }, Is.SubsetOf(sizes), creature);
                Assert.That(double.TryParse(data[DataIndexConstants.CreatureData.Reach], out doubleJunk), Is.True, creature);
                Assert.That(doubleJunk, Is.Not.Negative, creature);
                Assert.That(double.TryParse(data[DataIndexConstants.CreatureData.Space], out doubleJunk), Is.True, creature);
                Assert.That(doubleJunk, Is.Not.Negative, creature);

                if (!string.IsNullOrEmpty(data[DataIndexConstants.CreatureData.LevelAdjustment]))
                {
                    Assert.That(int.TryParse(data[DataIndexConstants.CreatureData.LevelAdjustment], out intJunk), Is.True, creature);
                    Assert.That(intJunk, Is.Not.Negative, creature);
                }
                else
                {
                    Assert.That(data[DataIndexConstants.CreatureData.LevelAdjustment], Is.Empty, creature);
                }
            }
        }
    }
}
