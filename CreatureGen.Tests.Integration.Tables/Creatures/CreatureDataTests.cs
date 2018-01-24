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

        [TestCase(CreatureConstants.Aasimar, ChallengeRatingConstants.OneHalf, 1, 5, SizeConstants.Medium, 5)]
        [TestCase(CreatureConstants.Aboleth, ChallengeRatingConstants.Seven, null, 10, SizeConstants.Huge, 15)]
        [TestCase(CreatureConstants.Aboleth_Mage, ChallengeRatingConstants.Seventeen, null, 10, SizeConstants.Huge, 15)]
        [TestCase(CreatureConstants.Achaierai, ChallengeRatingConstants.Five, null, 10, SizeConstants.Large, 10)]
        [TestCase(CreatureConstants.Arrowhawk_Adult, ChallengeRatingConstants.Five, null, 5, SizeConstants.Medium, 5)]
        [TestCase(CreatureConstants.Arrowhawk_Elder, ChallengeRatingConstants.Eight, null, 5, SizeConstants.Large, 10)]
        [TestCase(CreatureConstants.Arrowhawk_Juvenile, ChallengeRatingConstants.Three, null, 5, SizeConstants.Small, 5)]
        [TestCase(CreatureConstants.Tiefling, ChallengeRatingConstants.OneHalf, 1, 5, SizeConstants.Medium, 5)]
        public void CreatureData(string creature, string challengeRating, int? levelAdjustment, int reach, string size, int space)
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
        public void AllCreaturesHaveCorrectNumberOfEntries()
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
