using DnDGen.CreatureGen.Selectors.Collections;
using DnDGen.CreatureGen.Tables;
using DnDGen.Infrastructure.Selectors.Collections;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace DnDGen.CreatureGen.Tests.Unit.Selectors.Collections
{
    [TestFixture]
    public class CreatureDataSelectorTests
    {
        private const string Creature = "creature";

        private ICreatureDataSelector creatureDataSelector;
        private Mock<ICollectionSelector> mockCollectionSelector;
        private string[] data;

        [SetUp]
        public void SelectFor_Setup()
        {
            mockCollectionSelector = new Mock<ICollectionSelector>();
            creatureDataSelector = new CreatureDataSelector(mockCollectionSelector.Object);

            data = DataIndexConstants.CreatureData.InitializeData();
            data[DataIndexConstants.CreatureData.ChallengeRating] = "challenge rating";
            data[DataIndexConstants.CreatureData.LevelAdjustment] = "9266";
            data[DataIndexConstants.CreatureData.Reach] = "90.210";
            data[DataIndexConstants.CreatureData.Size] = "size";
            data[DataIndexConstants.CreatureData.Space] = "4.2";
            data[DataIndexConstants.CreatureData.CanUseEquipment] = bool.TrueString;
            data[DataIndexConstants.CreatureData.CasterLevel] = "600";
            data[DataIndexConstants.CreatureData.NaturalArmor] = "1337";
            data[DataIndexConstants.CreatureData.NumberOfHands] = "1336";

            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureData, Creature))
                .Returns(data);
        }

        [Test]
        public void SelectFor_SelectChallengeRating()
        {
            var selection = creatureDataSelector.SelectFor(Creature);
            Assert.That(selection.ChallengeRating, Is.EqualTo("challenge rating"));
        }

        [Test]
        public void SelectFor_SelectCreatureDataWithLevelAdjustment()
        {
            var selection = creatureDataSelector.SelectFor(Creature);
            Assert.That(selection.LevelAdjustment, Is.EqualTo(9266));
        }

        [Test]
        public void SelectFor_SelectCreatureDataWithLevelAdjustmentOfZero()
        {
            data[DataIndexConstants.CreatureData.LevelAdjustment] = "0";

            var selection = creatureDataSelector.SelectFor(Creature);
            Assert.That(selection.LevelAdjustment, Is.EqualTo(0));
        }

        [Test]
        public void SelectFor_SelectCreatureDataWithoutLevelAdjustment()
        {
            data[DataIndexConstants.CreatureData.LevelAdjustment] = string.Empty;

            var selection = creatureDataSelector.SelectFor(Creature);
            Assert.That(selection.LevelAdjustment, Is.Null);
        }

        [Test]
        public void SelectFor_SelectReach()
        {
            var selection = creatureDataSelector.SelectFor(Creature);
            Assert.That(selection.Reach, Is.EqualTo(90.210));
        }

        [Test]
        public void SelectFor_SelectSpace()
        {
            var selection = creatureDataSelector.SelectFor(Creature);
            Assert.That(selection.Space, Is.EqualTo(4.2));
        }

        [Test]
        public void SelectFor_SelectCasterLevel()
        {
            var selection = creatureDataSelector.SelectFor(Creature);
            Assert.That(selection.CasterLevel, Is.EqualTo(600));
        }

        [Test]
        public void SelectFor_SelectCreatureDataCanUseEquipment()
        {
            data[DataIndexConstants.CreatureData.CanUseEquipment] = bool.TrueString;

            var selection = creatureDataSelector.SelectFor(Creature);
            Assert.That(selection.CanUseEquipment, Is.True);
        }

        [Test]
        public void SelectFor_SelectCreatureDataCannotUseEquipment()
        {
            data[DataIndexConstants.CreatureData.CanUseEquipment] = bool.FalseString;

            var selection = creatureDataSelector.SelectFor(Creature);
            Assert.That(selection.CanUseEquipment, Is.False);
        }

        [Test]
        public void SelectFor_SelectNaturalArmor()
        {
            var selection = creatureDataSelector.SelectFor(Creature);
            Assert.That(selection.NaturalArmor, Is.EqualTo(1337));
        }

        [Test]
        public void SelectFor_SelectNumberOfHands()
        {
            var selection = creatureDataSelector.SelectFor(Creature);
            Assert.That(selection.NumberOfHands, Is.EqualTo(1336));
        }

        [Test]
        public void SelectAll_ReturnsAll()
        {
            var data = new Dictionary<string, IEnumerable<string>>();
            var creatureData = DataIndexConstants.CreatureData.InitializeData();
            creatureData[DataIndexConstants.CreatureData.ChallengeRating] = "challenge rating";
            creatureData[DataIndexConstants.CreatureData.LevelAdjustment] = "9266";
            creatureData[DataIndexConstants.CreatureData.Reach] = "90.210";
            creatureData[DataIndexConstants.CreatureData.Size] = "size";
            creatureData[DataIndexConstants.CreatureData.Space] = "4.2";
            creatureData[DataIndexConstants.CreatureData.CanUseEquipment] = bool.TrueString;
            creatureData[DataIndexConstants.CreatureData.CasterLevel] = "600";
            creatureData[DataIndexConstants.CreatureData.NaturalArmor] = "1337";
            creatureData[DataIndexConstants.CreatureData.NumberOfHands] = "1336";

            var otherCreatureData = DataIndexConstants.CreatureData.InitializeData();
            otherCreatureData[DataIndexConstants.CreatureData.ChallengeRating] = "other challenge rating";
            otherCreatureData[DataIndexConstants.CreatureData.LevelAdjustment] = string.Empty;
            otherCreatureData[DataIndexConstants.CreatureData.Reach] = "78.3";
            otherCreatureData[DataIndexConstants.CreatureData.Size] = "other size";
            otherCreatureData[DataIndexConstants.CreatureData.Space] = "82.45";
            otherCreatureData[DataIndexConstants.CreatureData.CanUseEquipment] = bool.FalseString;
            otherCreatureData[DataIndexConstants.CreatureData.CasterLevel] = "0";
            otherCreatureData[DataIndexConstants.CreatureData.NaturalArmor] = "0";
            otherCreatureData[DataIndexConstants.CreatureData.NumberOfHands] = "0";

            data[Creature] = creatureData;
            data["my other creature"] = otherCreatureData;

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureData))
                .Returns(data);

            var allData = creatureDataSelector.SelectAll();
            Assert.That(allData, Has.Count.EqualTo(2)
                .And.ContainKey(Creature)
                .And.ContainKey("my other creature"));
            Assert.That(allData[Creature].CanUseEquipment, Is.True);
            Assert.That(allData[Creature].CasterLevel, Is.EqualTo(600));
            Assert.That(allData[Creature].ChallengeRating, Is.EqualTo("challenge rating"));
            Assert.That(allData[Creature].LevelAdjustment, Is.EqualTo(9266));
            Assert.That(allData[Creature].NaturalArmor, Is.EqualTo(1337));
            Assert.That(allData[Creature].NumberOfHands, Is.EqualTo(1336));
            Assert.That(allData[Creature].Reach, Is.EqualTo(90.210));
            Assert.That(allData[Creature].Size, Is.EqualTo("size"));
            Assert.That(allData[Creature].Space, Is.EqualTo(4.2));
            Assert.That(allData["my other creature"].CanUseEquipment, Is.False);
            Assert.That(allData["my other creature"].CasterLevel, Is.Zero);
            Assert.That(allData["my other creature"].ChallengeRating, Is.EqualTo("other challenge rating"));
            Assert.That(allData["my other creature"].LevelAdjustment, Is.Null);
            Assert.That(allData["my other creature"].NaturalArmor, Is.Zero);
            Assert.That(allData["my other creature"].NumberOfHands, Is.Zero);
            Assert.That(allData["my other creature"].Reach, Is.EqualTo(78.3));
            Assert.That(allData["my other creature"].Size, Is.EqualTo("other size"));
            Assert.That(allData["my other creature"].Space, Is.EqualTo(82.45));
        }
    }
}
