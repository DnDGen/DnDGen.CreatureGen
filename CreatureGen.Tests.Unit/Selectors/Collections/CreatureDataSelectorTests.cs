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
        private string[] data;
        private const string Creature = "creature";

        [SetUp]
        public void Setup()
        {
            mockCollectionSelector = new Mock<ICollectionSelector>();
            creatureDataSelector = new CreatureDataSelector(mockCollectionSelector.Object);

            data = new string[6];
            data[DataIndexConstants.CreatureData.ChallengeRating] = "challenge rating";
            data[DataIndexConstants.CreatureData.LevelAdjustment] = "9266";
            data[DataIndexConstants.CreatureData.Reach] = "90.210";
            data[DataIndexConstants.CreatureData.Size] = "size";
            data[DataIndexConstants.CreatureData.Space] = "4.2";
            data[DataIndexConstants.CreatureData.CanUseEquipment] = bool.TrueString;

            mockCollectionSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.CreatureData, Creature))
                .Returns(data);
        }

        [Test]
        public void SelectCreatureDataWithLevelAdjustment()
        {
            var selection = creatureDataSelector.SelectFor(Creature);
            Assert.That(selection.ChallengeRating, Is.EqualTo("challenge rating"));
            Assert.That(selection.LevelAdjustment, Is.EqualTo(9266));
            Assert.That(selection.Reach, Is.EqualTo(90.210));
            Assert.That(selection.Size, Is.EqualTo("size"));
            Assert.That(selection.Space, Is.EqualTo(4.2));
            Assert.That(selection.CanUseEquipment, Is.True);
        }

        [Test]
        public void SelectCreatureDataWithLevelAdjustmentOfZero()
        {
            data[DataIndexConstants.CreatureData.LevelAdjustment] = "0";

            var selection = creatureDataSelector.SelectFor(Creature);
            Assert.That(selection.ChallengeRating, Is.EqualTo("challenge rating"));
            Assert.That(selection.LevelAdjustment, Is.EqualTo(0));
            Assert.That(selection.Reach, Is.EqualTo(90.210));
            Assert.That(selection.Size, Is.EqualTo("size"));
            Assert.That(selection.Space, Is.EqualTo(4.2));
            Assert.That(selection.CanUseEquipment, Is.True);
        }

        [Test]
        public void SelectCreatureDataWithoutLevelAdjustment()
        {
            data[DataIndexConstants.CreatureData.LevelAdjustment] = string.Empty;

            var selection = creatureDataSelector.SelectFor(Creature);
            Assert.That(selection.ChallengeRating, Is.EqualTo("challenge rating"));
            Assert.That(selection.LevelAdjustment, Is.Null);
            Assert.That(selection.Reach, Is.EqualTo(90.210));
            Assert.That(selection.Size, Is.EqualTo("size"));
            Assert.That(selection.Space, Is.EqualTo(4.2));
            Assert.That(selection.CanUseEquipment, Is.True);
        }

        [Test]
        public void SelectCreatureDataCanUseEquipment()
        {
            data[DataIndexConstants.CreatureData.CanUseEquipment] = bool.TrueString;

            var selection = creatureDataSelector.SelectFor(Creature);
            Assert.That(selection.ChallengeRating, Is.EqualTo("challenge rating"));
            Assert.That(selection.LevelAdjustment, Is.EqualTo(9266));
            Assert.That(selection.Reach, Is.EqualTo(90.210));
            Assert.That(selection.Size, Is.EqualTo("size"));
            Assert.That(selection.Space, Is.EqualTo(4.2));
            Assert.That(selection.CanUseEquipment, Is.True);
        }

        [Test]
        public void SelectCreatureDataCannotUseEquipment()
        {
            data[DataIndexConstants.CreatureData.CanUseEquipment] = bool.FalseString;

            var selection = creatureDataSelector.SelectFor(Creature);
            Assert.That(selection.ChallengeRating, Is.EqualTo("challenge rating"));
            Assert.That(selection.LevelAdjustment, Is.EqualTo(9266));
            Assert.That(selection.Reach, Is.EqualTo(90.210));
            Assert.That(selection.Size, Is.EqualTo("size"));
            Assert.That(selection.Space, Is.EqualTo(4.2));
            Assert.That(selection.CanUseEquipment, Is.False);
        }
    }
}
