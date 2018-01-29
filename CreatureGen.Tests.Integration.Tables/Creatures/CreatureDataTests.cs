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
        [TestCase(CreatureConstants.Aboleth, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Seven, null)]
        [TestCase(CreatureConstants.Aboleth_Mage, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Seventeen, null)]
        [TestCase(CreatureConstants.Achaierai, SizeConstants.Large, 10, 10, ChallengeRatingConstants.Five, null)]
        [TestCase(CreatureConstants.Androsphinx, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Nine, 5)]
        [TestCase(CreatureConstants.AnimatedObject_Colossal, SizeConstants.Colossal, 30, 20, ChallengeRatingConstants.Ten, null)]
        [TestCase(CreatureConstants.AnimatedObject_Gargantuan, SizeConstants.Gargantuan, 20, 15, ChallengeRatingConstants.Seven, null)]
        [TestCase(CreatureConstants.AnimatedObject_Huge, SizeConstants.Huge, 15, 10, ChallengeRatingConstants.Five, null)]
        [TestCase(CreatureConstants.AnimatedObject_Large, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Three, null)]
        [TestCase(CreatureConstants.AnimatedObject_Medium, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Two, null)]
        [TestCase(CreatureConstants.AnimatedObject_Small, SizeConstants.Small, 5, 5, ChallengeRatingConstants.One, null)]
        [TestCase(CreatureConstants.AnimatedObject_Tiny, SizeConstants.Tiny, 2.5, 0, ChallengeRatingConstants.OneHalf, null)]
        [TestCase(CreatureConstants.Arrowhawk_Adult, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.Five, null)]
        [TestCase(CreatureConstants.Arrowhawk_Elder, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Eight, null)]
        [TestCase(CreatureConstants.Arrowhawk_Juvenile, SizeConstants.Small, 5, 5, ChallengeRatingConstants.Three, null)]
        [TestCase(CreatureConstants.Criosphinx, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Seven, 3)]
        [TestCase(CreatureConstants.Gynosphinx, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Eight, 4)]
        [TestCase(CreatureConstants.Hieracosphinx, SizeConstants.Large, 10, 5, ChallengeRatingConstants.Five, 3)]
        [TestCase(CreatureConstants.Tiefling, SizeConstants.Medium, 5, 5, ChallengeRatingConstants.OneHalf, 1)]
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
                var junk = 0;

                Assert.That(data.Length, Is.EqualTo(5), creature);
                Assert.That(data[DataIndexConstants.CreatureData.ChallengeRating], Is.Not.Empty);
                Assert.That(new[] { data[DataIndexConstants.CreatureData.Size] }, Is.SubsetOf(sizes), creature);
                Assert.That(int.TryParse(data[DataIndexConstants.CreatureData.Reach], out junk), Is.True, creature);
                Assert.That(int.TryParse(data[DataIndexConstants.CreatureData.Space], out junk), Is.True, creature);

                if (!string.IsNullOrEmpty(data[DataIndexConstants.CreatureData.LevelAdjustment]))
                    Assert.That(int.TryParse(data[DataIndexConstants.CreatureData.LevelAdjustment], out junk), Is.True, creature);
                else
                    Assert.That(data[DataIndexConstants.CreatureData.LevelAdjustment], Is.Empty);
            }
        }
    }
}
