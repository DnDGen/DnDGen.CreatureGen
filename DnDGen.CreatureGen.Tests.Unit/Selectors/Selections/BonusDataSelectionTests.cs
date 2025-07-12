using DnDGen.CreatureGen.Selectors.Selections;
using DnDGen.CreatureGen.Tables;
using NUnit.Framework;

namespace DnDGen.CreatureGen.Tests.Unit.Selectors.Selections
{
    [TestFixture]
    public class BonusDataSelectionTests
    {
        private BonusDataSelection selection;

        [SetUp]
        public void Setup()
        {
            selection = new BonusDataSelection();
        }

        [Test]
        public void BonusDataSelectionIsInitialized()
        {
            Assert.That(selection.Bonus, Is.Zero);
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
            data[DataIndexConstants.BonusData.BonusIndex] = "9266";
            data[DataIndexConstants.BonusData.TargetIndex] = "my target";
            data[DataIndexConstants.BonusData.ConditionIndex] = "my condition";

            var newSelection = BonusDataSelection.Map(data);
            Assert.That(newSelection, Is.Not.Null);
            Assert.That(newSelection.Bonus, Is.EqualTo(9266));
            Assert.That(newSelection.Target, Is.EqualTo("my target"));
            Assert.That(newSelection.Condition, Is.EqualTo("my condition"));
        }

        [TestCase("")]
        [TestCase(null)]
        public void Map_FromString_ReturnsSelection_NoCondition(string condition)
        {
            var data = new string[selection.SectionCount];
            data[DataIndexConstants.BonusData.BonusIndex] = "9266";
            data[DataIndexConstants.BonusData.TargetIndex] = "my target";
            data[DataIndexConstants.BonusData.ConditionIndex] = condition;

            var newSelection = BonusDataSelection.Map(data);
            Assert.That(newSelection, Is.Not.Null);
            Assert.That(newSelection.Bonus, Is.EqualTo(9266));
            Assert.That(newSelection.Target, Is.EqualTo("my target"));
            Assert.That(newSelection.Condition, Is.Empty);
        }

        [Test]
        public void Map_FromSelection_ReturnsString()
        {
            var selection = new BonusDataSelection
            {
                Bonus = 9266,
                Target = "my target",
                Condition = "my condition",
            };

            var rawData = BonusDataSelection.Map(selection);
            Assert.That(rawData.Length, Is.EqualTo(selection.SectionCount));
            Assert.That(rawData[DataIndexConstants.BonusData.BonusIndex], Is.EqualTo("9266"));
            Assert.That(rawData[DataIndexConstants.BonusData.TargetIndex], Is.EqualTo("my target"));
            Assert.That(rawData[DataIndexConstants.BonusData.ConditionIndex], Is.EqualTo("my condition"));
        }

        [TestCase("")]
        [TestCase(null)]
        public void Map_FromSelection_ReturnsString_NoCondition(string condition)
        {
            var selection = new BonusDataSelection
            {
                Bonus = 9266,
                Target = "my target",
                Condition = condition,
            };

            var rawData = BonusDataSelection.Map(selection);
            Assert.That(rawData.Length, Is.EqualTo(selection.SectionCount));
            Assert.That(rawData[DataIndexConstants.BonusData.BonusIndex], Is.EqualTo("9266"));
            Assert.That(rawData[DataIndexConstants.BonusData.TargetIndex], Is.EqualTo("my target"));
            Assert.That(rawData[DataIndexConstants.BonusData.ConditionIndex], Is.Empty);
        }

        [Test]
        public void MapTo_ReturnsSelection()
        {
            var data = new string[selection.SectionCount];
            data[DataIndexConstants.BonusData.BonusIndex] = "9266";
            data[DataIndexConstants.BonusData.TargetIndex] = "my target";
            data[DataIndexConstants.BonusData.ConditionIndex] = "my condition";

            var newSelection = selection.MapTo(data);
            Assert.That(newSelection, Is.Not.Null);
            Assert.That(newSelection.Bonus, Is.EqualTo(9266));
            Assert.That(newSelection.Target, Is.EqualTo("my target"));
            Assert.That(newSelection.Condition, Is.EqualTo("my condition"));
        }

        [TestCase("")]
        [TestCase(null)]
        public void MapTo_ReturnsSelection_NoCondition(string condition)
        {
            var data = new string[selection.SectionCount];
            data[DataIndexConstants.BonusData.BonusIndex] = "9266";
            data[DataIndexConstants.BonusData.TargetIndex] = "my target";
            data[DataIndexConstants.BonusData.ConditionIndex] = condition;

            var newSelection = selection.MapTo(data);
            Assert.That(newSelection, Is.Not.Null);
            Assert.That(newSelection.Bonus, Is.EqualTo(9266));
            Assert.That(newSelection.Target, Is.EqualTo("my target"));
            Assert.That(newSelection.Condition, Is.Empty);
        }

        [Test]
        public void MapFrom_ReturnsString()
        {
            var selection = new BonusDataSelection
            {
                Bonus = 9266,
                Target = "my target",
                Condition = "my condition",
            };

            var rawData = selection.MapFrom(selection);
            Assert.That(rawData.Length, Is.EqualTo(selection.SectionCount));
            Assert.That(rawData[DataIndexConstants.BonusData.BonusIndex], Is.EqualTo("9266"));
            Assert.That(rawData[DataIndexConstants.BonusData.TargetIndex], Is.EqualTo("my target"));
            Assert.That(rawData[DataIndexConstants.BonusData.ConditionIndex], Is.EqualTo("my condition"));
        }

        [TestCase("")]
        [TestCase(null)]
        public void MapFrom_ReturnsString_NoCondition(string condition)
        {
            var selection = new BonusDataSelection
            {
                Bonus = 9266,
                Target = "my target",
                Condition = condition,
            };

            var rawData = selection.MapFrom(selection);
            Assert.That(rawData.Length, Is.EqualTo(selection.SectionCount));
            Assert.That(rawData[DataIndexConstants.BonusData.BonusIndex], Is.EqualTo("9266"));
            Assert.That(rawData[DataIndexConstants.BonusData.TargetIndex], Is.EqualTo("my target"));
            Assert.That(rawData[DataIndexConstants.BonusData.ConditionIndex], Is.Empty);
        }
    }
}
