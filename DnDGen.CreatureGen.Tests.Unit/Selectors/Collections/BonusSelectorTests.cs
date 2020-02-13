using DnDGen.CreatureGen.Selectors.Collections;
using DnDGen.CreatureGen.Selectors.Selections;
using Moq;
using NUnit.Framework;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Unit.Selectors.Collections
{
    [TestFixture]
    public class BonusSelectorTests
    {
        private IBonusSelector bonusSelector;
        private Mock<ITypeAndAmountSelector> mockTypeAndAmountSelector;

        [SetUp]
        public void Setup()
        {
            mockTypeAndAmountSelector = new Mock<ITypeAndAmountSelector>();
            bonusSelector = new BonusSelector(mockTypeAndAmountSelector.Object);
        }

        [Test]
        public void SelectNoBonuses()
        {
            var bonuses = bonusSelector.SelectFor("table name", "source");
            Assert.That(bonuses, Is.Empty);
        }

        [Test]
        public void SelectBonus()
        {
            var typesAndAmounts = new[]
            {
                new TypeAndAmountSelection { Type = "type", Amount = 9266 },
            };

            mockTypeAndAmountSelector.Setup(s => s.Select("table name", "source")).Returns(typesAndAmounts);

            var bonuses = bonusSelector.SelectFor("table name", "source");
            Assert.That(bonuses, Is.Not.Empty);

            var bonus = bonuses.Single();
            Assert.That(bonus.Bonus, Is.EqualTo(9266));
            Assert.That(bonus.Condition, Is.Empty);
            Assert.That(bonus.Target, Is.EqualTo("type"));
        }

        [Test]
        public void SelectBonuses()
        {
            var typesAndAmounts = new[]
            {
                new TypeAndAmountSelection { Type = "type", Amount = 9266 },
                new TypeAndAmountSelection { Type = "other type", Amount = 90210 },
            };

            mockTypeAndAmountSelector.Setup(s => s.Select("table name", "source")).Returns(typesAndAmounts);

            var bonuses = bonusSelector.SelectFor("table name", "source");
            Assert.That(bonuses, Is.Not.Empty);

            var bonus = bonuses.First();
            Assert.That(bonus.Bonus, Is.EqualTo(9266));
            Assert.That(bonus.Condition, Is.Empty);
            Assert.That(bonus.Target, Is.EqualTo("type"));

            bonus = bonuses.Last();
            Assert.That(bonus.Bonus, Is.EqualTo(90210));
            Assert.That(bonus.Condition, Is.Empty);
            Assert.That(bonus.Target, Is.EqualTo("other type"));
        }

        [TestCase("condition")]
        [TestCase("my condition")]
        [TestCase("my condition, with commas")]
        public void SelectBonusWithCondition(string condition)
        {
            var typesAndAmounts = new[]
            {
                new TypeAndAmountSelection { Type = $"type{BonusSelection.Divider}{condition}", Amount = 9266 },
            };

            mockTypeAndAmountSelector.Setup(s => s.Select("table name", "source")).Returns(typesAndAmounts);

            var bonuses = bonusSelector.SelectFor("table name", "source");
            Assert.That(bonuses, Is.Not.Empty);

            var bonus = bonuses.Single();
            Assert.That(bonus.Bonus, Is.EqualTo(9266));
            Assert.That(bonus.Condition, Is.EqualTo(condition));
            Assert.That(bonus.Target, Is.EqualTo("type"));
        }
    }
}
