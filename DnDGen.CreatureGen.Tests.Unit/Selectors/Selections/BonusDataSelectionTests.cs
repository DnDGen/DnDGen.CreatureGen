using DnDGen.CreatureGen.Selectors.Selections;
using DnDGen.CreatureGen.Tables;
using DnDGen.RollGen;
using Moq;
using NUnit.Framework;

namespace DnDGen.CreatureGen.Tests.Unit.Selectors.Selections
{
    [TestFixture]
    public class BonusDataSelectionTests
    {
        private BonusDataSelection selection;
        private Mock<Dice> mockDice;

        [SetUp]
        public void Setup()
        {
            selection = new BonusDataSelection();
            mockDice = new Mock<Dice>();
        }

        [Test]
        public void BonusDataSelectionIsInitialized()
        {
            Assert.That(selection.Bonus, Is.Zero);
            Assert.That(selection.BonusRoll, Is.Empty);
            Assert.That(selection.Target, Is.Empty);
            Assert.That(selection.Condition, Is.Empty);
        }

        [Test]
        public void SectionCountIs3()
        {
            Assert.That(selection.SectionCount, Is.EqualTo(3));
        }

        [Test]
        public void Map_FromString_ReturnsSelection()
        {
            var data = new string[selection.SectionCount];
            data[DataIndexConstants.BonusData.BonusRollIndex] = "9266d90210";
            data[DataIndexConstants.BonusData.TargetIndex] = "my target";
            data[DataIndexConstants.BonusData.ConditionIndex] = "my condition";

            var newSelection = BonusDataSelection.Map(data);
            Assert.That(newSelection, Is.Not.Null);
            Assert.That(newSelection.BonusRoll, Is.EqualTo("9266d90210"));
            Assert.That(newSelection.Target, Is.EqualTo("my target"));
            Assert.That(newSelection.Condition, Is.EqualTo("my condition"));
        }

        [Test]
        public void Map_FromSelection_ReturnsString()
        {
            var selection = new BonusDataSelection
            {
                BonusRoll = "9266d90210",
                Target = "my target",
                Condition = "my condition",
            };

            var rawData = BonusDataSelection.Map(selection);
            Assert.That(rawData.Length, Is.EqualTo(selection.SectionCount));
            Assert.That(rawData[DataIndexConstants.BonusData.BonusRollIndex], Is.EqualTo("9266d90210"));
            Assert.That(rawData[DataIndexConstants.BonusData.TargetIndex], Is.EqualTo("my target"));
            Assert.That(rawData[DataIndexConstants.BonusData.ConditionIndex], Is.EqualTo("my condition"));
        }

        [Test]
        public void MapTo_ReturnsSelection()
        {
            var data = new string[selection.SectionCount];
            data[DataIndexConstants.BonusData.BonusRollIndex] = "9266d90210";
            data[DataIndexConstants.BonusData.TargetIndex] = "my target";
            data[DataIndexConstants.BonusData.ConditionIndex] = "my condition";

            var newSelection = selection.MapTo(data);
            Assert.That(newSelection, Is.Not.Null);
            Assert.That(newSelection.BonusRoll, Is.EqualTo("9266d90210"));
            Assert.That(newSelection.Target, Is.EqualTo("my target"));
            Assert.That(newSelection.Condition, Is.EqualTo("my condition"));
        }

        [Test]
        public void MapFrom_ReturnsString()
        {
            var selection = new BonusDataSelection
            {
                BonusRoll = "9266d90210",
                Target = "my target",
                Condition = "my condition",
            };

            var rawData = selection.MapFrom(selection);
            Assert.That(rawData.Length, Is.EqualTo(selection.SectionCount));
            Assert.That(rawData[DataIndexConstants.BonusData.BonusRollIndex], Is.EqualTo("9266d90210"));
            Assert.That(rawData[DataIndexConstants.BonusData.TargetIndex], Is.EqualTo("my target"));
            Assert.That(rawData[DataIndexConstants.BonusData.ConditionIndex], Is.EqualTo("my condition"));
        }

        [Test]
        public void SetAdditionalProperties_SetsBonus()
        {
            selection.BonusRoll = "roll 9266";
            mockDice.Setup(d => d.Roll("roll 9266").AsSum<int>()).Returns(9266);

            selection.SetAdditionalProperties(mockDice.Object);
            Assert.That(selection.Bonus, Is.EqualTo(9266));
        }
    }
}
