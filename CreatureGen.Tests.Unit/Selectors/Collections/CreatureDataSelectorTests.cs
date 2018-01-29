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
            var data = new string[5];
            data[DataIndexConstants.CreatureData.ChallengeRating] = "challenge rating";
            data[DataIndexConstants.CreatureData.LevelAdjustment] = "9266";
            data[DataIndexConstants.CreatureData.Reach] = "90.210";
            data[DataIndexConstants.CreatureData.Size] = "size";
            data[DataIndexConstants.CreatureData.Space] = "4.2";

            mockCollectionSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.CreatureData, "creature"))
                .Returns(data);

            var selection = creatureDataSelector.SelectFor("creature");
            Assert.That(selection.ChallengeRating, Is.EqualTo("challenge rating"));
            Assert.That(selection.LevelAdjustment, Is.EqualTo(9266));
            Assert.That(selection.Reach, Is.EqualTo(90.210));
            Assert.That(selection.Size, Is.EqualTo("size"));
            Assert.That(selection.Space, Is.EqualTo(4.2));
        }

        [Test]
        public void SelectCreatureDataWithLevelAdjustmentOfZero()
        {
            var data = new string[5];
            data[DataIndexConstants.CreatureData.ChallengeRating] = "challenge rating";
            data[DataIndexConstants.CreatureData.LevelAdjustment] = "0";
            data[DataIndexConstants.CreatureData.Reach] = "90.210";
            data[DataIndexConstants.CreatureData.Size] = "size";
            data[DataIndexConstants.CreatureData.Space] = "4.2";

            mockCollectionSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.CreatureData, "creature"))
                .Returns(data);

            var selection = creatureDataSelector.SelectFor("creature");
            Assert.That(selection.ChallengeRating, Is.EqualTo("challenge rating"));
            Assert.That(selection.LevelAdjustment, Is.EqualTo(0));
            Assert.That(selection.Reach, Is.EqualTo(90.210));
            Assert.That(selection.Size, Is.EqualTo("size"));
            Assert.That(selection.Space, Is.EqualTo(4.2));
        }

        [Test]
        public void SelectCreatureDataWithoutLevelAdjustment()
        {
            var data = new string[5];
            data[DataIndexConstants.CreatureData.ChallengeRating] = "challenge rating";
            data[DataIndexConstants.CreatureData.LevelAdjustment] = string.Empty;
            data[DataIndexConstants.CreatureData.Reach] = "90.210";
            data[DataIndexConstants.CreatureData.Size] = "size";
            data[DataIndexConstants.CreatureData.Space] = "4.2";

            mockCollectionSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.CreatureData, "creature"))
                .Returns(data);

            var selection = creatureDataSelector.SelectFor("creature");
            Assert.That(selection.ChallengeRating, Is.EqualTo("challenge rating"));
            Assert.That(selection.LevelAdjustment, Is.Null);
            Assert.That(selection.Reach, Is.EqualTo(90.210));
            Assert.That(selection.Size, Is.EqualTo("size"));
            Assert.That(selection.Space, Is.EqualTo(4.2));
        }
    }
}
