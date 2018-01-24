using CreatureGen.Selectors.Collections;
using CreatureGen.Tables;
using DnDGen.Core.Selectors.Collections;
using Moq;
using NUnit.Framework;

namespace CreatureGen.Tests.Unit.Selectors.Collections
{
    [TestFixture]
    public class CreatureDataSelectorTests
    {
        private ICreatureDataSelector creatureDataSelector;
        private Mock<ICollectionSelector> mockCollectionSelector;

        [SetUp]
        public void Setup()
        {
            mockCollectionSelector = new Mock<ICollectionSelector>();
            creatureDataSelector = new CreatureDataSelector(mockCollectionSelector.Object);
        }

        [Test]
        public void SelectCreatureDataWithLevelAdjustment()
        {
            var data = new[]
            {
                "challenge rating",
                "9266",
                "90210",
                "size",
                "42"
            };

            mockCollectionSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.CreatureData, "creature"))
                .Returns(data);

            var selection = creatureDataSelector.SelectFor("creature");
            Assert.That(selection.ChallengeRating, Is.EqualTo("challenge rating"));
            Assert.That(selection.LevelAdjustment, Is.EqualTo(9266));
            Assert.That(selection.Reach, Is.EqualTo(90210));
            Assert.That(selection.Size, Is.EqualTo("size"));
            Assert.That(selection.Space, Is.EqualTo(42));
        }

        [Test]
        public void SelectCreatureDataWithLevelAdjustmentOfZero()
        {
            var data = new[]
            {
                "challenge rating",
                "0",
                "90210",
                "size",
                "42"
            };

            mockCollectionSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.CreatureData, "creature"))
                .Returns(data);

            var selection = creatureDataSelector.SelectFor("creature");
            Assert.That(selection.ChallengeRating, Is.EqualTo("challenge rating"));
            Assert.That(selection.LevelAdjustment, Is.EqualTo(0));
            Assert.That(selection.Reach, Is.EqualTo(90210));
            Assert.That(selection.Size, Is.EqualTo("size"));
            Assert.That(selection.Space, Is.EqualTo(42));
        }

        [Test]
        public void SelectCreatureDataWithoutLevelAdjustment()
        {
            var data = new[]
            {
                "challenge rating",
                string.Empty,
                "90210",
                "size",
                "42"
            };

            mockCollectionSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.CreatureData, "creature"))
                .Returns(data);

            var selection = creatureDataSelector.SelectFor("creature");
            Assert.That(selection.ChallengeRating, Is.EqualTo("challenge rating"));
            Assert.That(selection.LevelAdjustment, Is.Null);
            Assert.That(selection.Reach, Is.EqualTo(90210));
            Assert.That(selection.Size, Is.EqualTo("size"));
            Assert.That(selection.Space, Is.EqualTo(42));
        }
    }
}
