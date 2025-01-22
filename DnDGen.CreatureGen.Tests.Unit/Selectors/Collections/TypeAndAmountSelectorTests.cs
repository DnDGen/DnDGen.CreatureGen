using DnDGen.CreatureGen.Selectors.Collections;
using DnDGen.CreatureGen.Selectors.Helpers;
using DnDGen.CreatureGen.Skills;
using DnDGen.Infrastructure.Selectors.Collections;
using DnDGen.RollGen;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Unit.Selectors.Collections
{
    [TestFixture]
    public class TypeAndAmountSelectorTests
    {
        private ITypeAndAmountSelector selector;
        private Mock<ICollectionSelector> mockCollectionSelector;
        private Mock<Dice> mockDice;

        [SetUp]
        public void Setup()
        {
            mockCollectionSelector = new Mock<ICollectionSelector>();
            mockDice = new Mock<Dice>();
            selector = new TypeAndAmountSelector(mockCollectionSelector.Object, mockDice.Object);
        }

        private void SetUpRoll(string roll, params int[] sums)
        {
            var mockPartialRoll = new Mock<PartialRoll>();
            mockDice.Setup(d => d.ContainsRoll(roll, false)).Returns(true);
            mockDice.Setup(d => d.Roll(roll)).Returns(mockPartialRoll.Object);

            var sequence = mockPartialRoll.SetupSequence(r => r.AsSum<int>());

            foreach (var sum in sums)
                sequence = sequence.Returns(sum);
        }

        [Test]
        public void SelectASingleTypeAndAmount()
        {
            var entries = new[]
            {
                TypeAndAmountHelper.Build("type", "9266"),
                TypeAndAmountHelper.Build("other type", "90210"),
            };

            mockCollectionSelector.Setup(s => s.SelectFrom(Config.Name, "table name", "name")).Returns(entries);

            SetUpRoll("9266", 9266);

            var typeAndAmount = selector.SelectOne("table name", "name");
            Assert.That(typeAndAmount.Type, Is.EqualTo("type"));
            Assert.That(typeAndAmount.Amount, Is.EqualTo(9266));
            Assert.That(typeAndAmount.RawAmount, Is.EqualTo("9266"));
        }

        [TestCase("skill", null)]
        [TestCase("skill", "")]
        [TestCase("skill", "focus")]
        public void SelectASingleSkillTypeAndAmount(string skill, string focus)
        {
            var skillString = SkillConstants.Build(skill, focus);

            var entries = new[]
            {
                TypeAndAmountHelper.Build(skillString, "9266"),
            };

            mockCollectionSelector.Setup(s => s.SelectFrom(Config.Name, "table name", "name")).Returns(entries);

            SetUpRoll("9266", 9266);

            var typeAndAmount = selector.SelectOne("table name", "name");
            Assert.That(typeAndAmount.Type, Is.EqualTo(skillString));
            Assert.That(typeAndAmount.Amount, Is.EqualTo(9266));
            Assert.That(typeAndAmount.RawAmount, Is.EqualTo("9266"));
        }

        [Test]
        public void SelectASingleTypeAndRandomAmount()
        {
            var entries = new[]
            {
                TypeAndAmountHelper.Build("type", "amount"),
                TypeAndAmountHelper.Build("other type", "other amount"),
            };

            mockCollectionSelector.Setup(s => s.SelectFrom(Config.Name, "table name", "name")).Returns(entries);

            SetUpRoll("amount", 42);

            var typeAndAmount = selector.SelectOne("table name", "name");
            Assert.That(typeAndAmount.Type, Is.EqualTo("type"));
            Assert.That(typeAndAmount.Amount, Is.EqualTo(42));
            Assert.That(typeAndAmount.RawAmount, Is.EqualTo("amount"));
        }

        [Test]
        public void SelectASingleTypeAndConsistentRandomAmount()
        {
            var entries = new[]
            {
                TypeAndAmountHelper.Build("type", "amount"),
                TypeAndAmountHelper.Build("other type", "other amount"),
            };

            mockCollectionSelector.Setup(s => s.SelectFrom(Config.Name, "table name", "name")).Returns(entries);

            SetUpRoll("amount", 42);

            var typeAndAmount = selector.SelectOne("table name", "name");
            Assert.That(typeAndAmount.Type, Is.EqualTo("type"));
            Assert.That(typeAndAmount.Amount, Is.EqualTo(42));
            Assert.That(typeAndAmount.RawAmount, Is.EqualTo("amount"));

            Assert.That(typeAndAmount.Type, Is.EqualTo("type"));
            Assert.That(typeAndAmount.Amount, Is.EqualTo(42));
            Assert.That(typeAndAmount.RawAmount, Is.EqualTo("amount"));
        }

        [Test]
        public void SelectMultipleTypesAndAmounts()
        {
            var entries = new[]
            {
                TypeAndAmountHelper.Build("type", "9266"),
                TypeAndAmountHelper.Build("other type", "90210"),
            };

            mockCollectionSelector.Setup(s => s.SelectFrom(Config.Name, "table name", "name")).Returns(entries);

            SetUpRoll("9266", 9266);
            SetUpRoll("90210", 90210);

            var typesAndAmounts = selector.Select("table name", "name");
            Assert.That(typesAndAmounts.Count, Is.EqualTo(2));

            var first = typesAndAmounts.First();
            var last = typesAndAmounts.Last();

            Assert.That(first.Type, Is.EqualTo("type"));
            Assert.That(first.Amount, Is.EqualTo(9266));
            Assert.That(first.RawAmount, Is.EqualTo("9266"));
            Assert.That(last.Type, Is.EqualTo("other type"));
            Assert.That(last.Amount, Is.EqualTo(90210));
            Assert.That(last.RawAmount, Is.EqualTo("90210"));
        }

        [Test]
        public void SelectMultipleTypesAndRandomAmounts()
        {
            var entries = new[]
            {
                TypeAndAmountHelper.Build("type", "amount"),
                TypeAndAmountHelper.Build("other type", "other amount"),
            };

            mockCollectionSelector.Setup(s => s.SelectFrom(Config.Name, "table name", "name")).Returns(entries);

            SetUpRoll("amount", 42);
            SetUpRoll("other amount", 600);

            var typesAndAmounts = selector.Select("table name", "name");
            Assert.That(typesAndAmounts.Count, Is.EqualTo(2));

            var first = typesAndAmounts.First();
            var last = typesAndAmounts.Last();

            Assert.That(first.Type, Is.EqualTo("type"));
            Assert.That(first.Amount, Is.EqualTo(42));
            Assert.That(first.RawAmount, Is.EqualTo("amount"));
            Assert.That(last.Type, Is.EqualTo("other type"));
            Assert.That(last.Amount, Is.EqualTo(600));
            Assert.That(last.RawAmount, Is.EqualTo("other amount"));
        }

        [Test]
        public void SelectMultipleTypesAndConsistentRandomAmounts()
        {
            var entries = new[]
            {
                TypeAndAmountHelper.Build("type", "amount"),
                TypeAndAmountHelper.Build("other type", "other amount"),
            };

            mockCollectionSelector.Setup(s => s.SelectFrom(Config.Name, "table name", "name")).Returns(entries);

            SetUpRoll("amount", 42);
            SetUpRoll("other amount", 600);

            var typesAndAmounts = selector.Select("table name", "name");
            Assert.That(typesAndAmounts.Count, Is.EqualTo(2));

            Assert.That(typesAndAmounts.First().Type, Is.EqualTo("type"));
            Assert.That(typesAndAmounts.First().Amount, Is.EqualTo(42));
            Assert.That(typesAndAmounts.First().RawAmount, Is.EqualTo("amount"));
            Assert.That(typesAndAmounts.Last().Type, Is.EqualTo("other type"));
            Assert.That(typesAndAmounts.Last().Amount, Is.EqualTo(600));
            Assert.That(typesAndAmounts.Last().RawAmount, Is.EqualTo("other amount"));

            Assert.That(typesAndAmounts.First().Type, Is.EqualTo("type"));
            Assert.That(typesAndAmounts.First().Amount, Is.EqualTo(42));
            Assert.That(typesAndAmounts.First().RawAmount, Is.EqualTo("amount"));
            Assert.That(typesAndAmounts.Last().Type, Is.EqualTo("other type"));
            Assert.That(typesAndAmounts.Last().Amount, Is.EqualTo(600));
            Assert.That(typesAndAmounts.Last().RawAmount, Is.EqualTo("other amount"));
        }

        [Test]
        public void SelectAllTypesAndAmounts()
        {
            var table = new Dictionary<string, IEnumerable<string>>();
            mockCollectionSelector.Setup(s => s.SelectAllFrom(Config.Name, "table name")).Returns(table);

            table["name"] = new[]
            {
                TypeAndAmountHelper.Build("type", "9266"),
                TypeAndAmountHelper.Build("other type", "90210"),
            };

            table["other name"] = new[]
            {
                TypeAndAmountHelper.Build("other type", "42"),
                TypeAndAmountHelper.Build("another type", "600"),
            };

            SetUpRoll("9266", 9266);
            SetUpRoll("90210", 90210);
            SetUpRoll("42", 42);
            SetUpRoll("600", 600);

            var typesAndAmounts = selector.SelectAll("table name");
            Assert.That(typesAndAmounts.Count, Is.EqualTo(2));

            Assert.That(typesAndAmounts["name"].First().Type, Is.EqualTo("type"));
            Assert.That(typesAndAmounts["name"].First().Amount, Is.EqualTo(9266));
            Assert.That(typesAndAmounts["name"].First().RawAmount, Is.EqualTo("9266"));
            Assert.That(typesAndAmounts["name"].Last().Type, Is.EqualTo("other type"));
            Assert.That(typesAndAmounts["name"].Last().Amount, Is.EqualTo(90210));
            Assert.That(typesAndAmounts["name"].Last().RawAmount, Is.EqualTo("90210"));
            Assert.That(typesAndAmounts["other name"].First().Type, Is.EqualTo("other type"));
            Assert.That(typesAndAmounts["other name"].First().Amount, Is.EqualTo(42));
            Assert.That(typesAndAmounts["other name"].First().RawAmount, Is.EqualTo("42"));
            Assert.That(typesAndAmounts["other name"].Last().Type, Is.EqualTo("another type"));
            Assert.That(typesAndAmounts["other name"].Last().Amount, Is.EqualTo(600));
            Assert.That(typesAndAmounts["other name"].Last().RawAmount, Is.EqualTo("600"));
        }

        [Test]
        public void SelectAllTypesAndRandomAmounts()
        {
            var table = new Dictionary<string, IEnumerable<string>>();
            mockCollectionSelector.Setup(s => s.SelectAllFrom(Config.Name, "table name")).Returns(table);

            table["name"] = new[]
            {
                TypeAndAmountHelper.Build("type", "amount"),
                TypeAndAmountHelper.Build("other type", "other amount"),
            };

            table["other name"] = new[]
            {
                TypeAndAmountHelper.Build("other type", "other amount"),
                TypeAndAmountHelper.Build("another type", "another amount"),
            };

            SetUpRoll("amount", 1337);
            SetUpRoll("other amount", 1234, 2345);
            SetUpRoll("another amount", 3456);

            var typesAndAmounts = selector.SelectAll("table name");
            Assert.That(typesAndAmounts.Count, Is.EqualTo(2));

            Assert.That(typesAndAmounts["name"].Count, Is.EqualTo(2));
            Assert.That(typesAndAmounts["name"].First().Type, Is.EqualTo("type"));
            Assert.That(typesAndAmounts["name"].First().Amount, Is.EqualTo(1337));
            Assert.That(typesAndAmounts["name"].First().RawAmount, Is.EqualTo("amount"));
            Assert.That(typesAndAmounts["name"].Last().Type, Is.EqualTo("other type"));
            Assert.That(typesAndAmounts["name"].Last().Amount, Is.EqualTo(1234));
            Assert.That(typesAndAmounts["name"].Last().RawAmount, Is.EqualTo("other amount"));
            Assert.That(typesAndAmounts["other name"].Count, Is.EqualTo(2));
            Assert.That(typesAndAmounts["other name"].First().Type, Is.EqualTo("other type"));
            Assert.That(typesAndAmounts["other name"].First().Amount, Is.EqualTo(2345));
            Assert.That(typesAndAmounts["other name"].First().RawAmount, Is.EqualTo("other amount"));
            Assert.That(typesAndAmounts["other name"].Last().Type, Is.EqualTo("another type"));
            Assert.That(typesAndAmounts["other name"].Last().Amount, Is.EqualTo(3456));
            Assert.That(typesAndAmounts["other name"].Last().RawAmount, Is.EqualTo("another amount"));
        }

        [Test]
        public void SelectAllTypesAndConsistentRandomAmounts()
        {
            var table = new Dictionary<string, IEnumerable<string>>();
            mockCollectionSelector.Setup(s => s.SelectAllFrom(Config.Name, "table name")).Returns(table);

            table["name"] = new[]
            {
                TypeAndAmountHelper.Build("type", "amount"),
                TypeAndAmountHelper.Build("other type", "other amount"),
            };

            table["other name"] = new[]
            {
                TypeAndAmountHelper.Build("other type", "other amount"),
                TypeAndAmountHelper.Build("another type", "another amount"),
            };

            SetUpRoll("amount", 1337);
            SetUpRoll("other amount", 1234, 2345);
            SetUpRoll("another amount", 3456);

            var typesAndAmounts = selector.SelectAll("table name");
            Assert.That(typesAndAmounts.Count, Is.EqualTo(2));

            Assert.That(typesAndAmounts["name"].Count, Is.EqualTo(2));
            Assert.That(typesAndAmounts["name"].First().Type, Is.EqualTo("type"));
            Assert.That(typesAndAmounts["name"].First().Amount, Is.EqualTo(1337));
            Assert.That(typesAndAmounts["name"].First().RawAmount, Is.EqualTo("amount"));
            Assert.That(typesAndAmounts["name"].Last().Type, Is.EqualTo("other type"));
            Assert.That(typesAndAmounts["name"].Last().Amount, Is.EqualTo(1234));
            Assert.That(typesAndAmounts["name"].Last().RawAmount, Is.EqualTo("other amount"));
            Assert.That(typesAndAmounts["other name"].Count, Is.EqualTo(2));
            Assert.That(typesAndAmounts["other name"].First().Type, Is.EqualTo("other type"));
            Assert.That(typesAndAmounts["other name"].First().Amount, Is.EqualTo(2345));
            Assert.That(typesAndAmounts["other name"].First().RawAmount, Is.EqualTo("other amount"));
            Assert.That(typesAndAmounts["other name"].Last().Type, Is.EqualTo("another type"));
            Assert.That(typesAndAmounts["other name"].Last().Amount, Is.EqualTo(3456));
            Assert.That(typesAndAmounts["other name"].Last().RawAmount, Is.EqualTo("another amount"));

            Assert.That(typesAndAmounts["name"].Count, Is.EqualTo(2));
            Assert.That(typesAndAmounts["name"].First().Type, Is.EqualTo("type"));
            Assert.That(typesAndAmounts["name"].First().Amount, Is.EqualTo(1337));
            Assert.That(typesAndAmounts["name"].First().RawAmount, Is.EqualTo("amount"));
            Assert.That(typesAndAmounts["name"].Last().Type, Is.EqualTo("other type"));
            Assert.That(typesAndAmounts["name"].Last().Amount, Is.EqualTo(1234));
            Assert.That(typesAndAmounts["name"].Last().RawAmount, Is.EqualTo("other amount"));
            Assert.That(typesAndAmounts["other name"].Count, Is.EqualTo(2));
            Assert.That(typesAndAmounts["other name"].First().Type, Is.EqualTo("other type"));
            Assert.That(typesAndAmounts["other name"].First().Amount, Is.EqualTo(2345));
            Assert.That(typesAndAmounts["other name"].First().RawAmount, Is.EqualTo("other amount"));
            Assert.That(typesAndAmounts["other name"].Last().Type, Is.EqualTo("another type"));
            Assert.That(typesAndAmounts["other name"].Last().Amount, Is.EqualTo(3456));
            Assert.That(typesAndAmounts["other name"].Last().RawAmount, Is.EqualTo("another amount"));
        }
    }
}
