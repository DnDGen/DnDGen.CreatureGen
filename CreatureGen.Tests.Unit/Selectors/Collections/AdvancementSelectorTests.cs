using CreatureGen.Selectors.Collections;
using CreatureGen.Selectors.Selections;
using CreatureGen.Tables;
using DnDGen.Core.Selectors.Collections;
using DnDGen.Core.Selectors.Percentiles;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace CreatureGen.Tests.Unit.Selectors.Collections
{
    [TestFixture]
    public class AdvancementSelectorTests
    {
        private IAdvancementSelector advancementSelector;
        private Mock<ITypeAndAmountSelector> mockTypeAndAmountSelector;
        private Mock<IPercentileSelector> mockPercentileSelector;
        private Mock<ICollectionSelector> mockCollectionSelector;
        private List<TypeAndAmountSelection> typesAndAmounts;

        [SetUp]
        public void Setup()
        {
            mockTypeAndAmountSelector = new Mock<ITypeAndAmountSelector>();
            mockPercentileSelector = new Mock<IPercentileSelector>();
            mockCollectionSelector = new Mock<ICollectionSelector>();
            advancementSelector = new AdvancementSelector(mockTypeAndAmountSelector.Object, mockPercentileSelector.Object, mockCollectionSelector.Object);

            typesAndAmounts = new List<TypeAndAmountSelection>();

            mockTypeAndAmountSelector.Setup(s => s.Select(TableNameConstants.Set.Collection.Advancements, "creature")).Returns(typesAndAmounts);
            mockCollectionSelector.Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<TypeAndAmountSelection>>()))
                .Returns((IEnumerable<TypeAndAmountSelection> c) => c.First());
        }

        [Test]
        public void SelectAdvancement()
        {
            typesAndAmounts.Add(new TypeAndAmountSelection
            {
                Type = "advanced size,92.66,902.1",
                Amount = 42
            });

            var advancement = advancementSelector.SelectRandomFor("creature");
            Assert.That(advancement.AdditionalHitDice, Is.EqualTo(42));
            Assert.That(advancement.Reach, Is.EqualTo(902.1));
            Assert.That(advancement.Size, Is.EqualTo("advanced size"));
            Assert.That(advancement.Space, Is.EqualTo(92.66));
        }

        [Test]
        public void SelectFromMultipleAdvancements()
        {
            typesAndAmounts.Add(new TypeAndAmountSelection
            {
                Type = "advanced size,92.66,902.1",
                Amount = 42
            });

            typesAndAmounts.Add(new TypeAndAmountSelection
            {
                Type = "other advanced size,600,1.337",
                Amount = 1234
            });

            typesAndAmounts.Add(new TypeAndAmountSelection
            {
                Type = "wrong advanced size,666,666",
                Amount = 666
            });

            mockCollectionSelector.Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<TypeAndAmountSelection>>()))
                .Returns((IEnumerable<TypeAndAmountSelection> c) => c.ElementAt(1));

            var advancement = advancementSelector.SelectRandomFor("creature");
            Assert.That(advancement.AdditionalHitDice, Is.EqualTo(1234));
            Assert.That(advancement.Reach, Is.EqualTo(1.337));
            Assert.That(advancement.Size, Is.EqualTo("other advanced size"));
            Assert.That(advancement.Space, Is.EqualTo(600));
        }

        [Test]
        public void SelectNoAdvancements()
        {
            Assert.That(() => advancementSelector.SelectRandomFor("creature"), Throws.Exception);
        }

        [Test]
        public void IsRandomlyAdvanced()
        {
            typesAndAmounts.Add(new TypeAndAmountSelection
            {
                Type = "advanced size,92.66,902.1",
                Amount = 42
            });

            mockPercentileSelector.Setup(s => s.SelectFrom(.1)).Returns(true);

            var isAdvanced = advancementSelector.IsAdvanced("creature");
            Assert.That(isAdvanced, Is.True);
        }

        [Test]
        public void IsRandomlyNotAdvanced()
        {
            typesAndAmounts.Add(new TypeAndAmountSelection
            {
                Type = "advanced size,92.66,902.1",
                Amount = 42
            });

            mockPercentileSelector.Setup(s => s.SelectFrom(.1)).Returns(false);

            var isAdvanced = advancementSelector.IsAdvanced("creature");
            Assert.That(isAdvanced, Is.False);
        }

        [Test]
        public void IsNotAdvancedIfNoAdvancements()
        {
            mockPercentileSelector.Setup(s => s.SelectFrom(.1)).Returns(true);

            var isAdvanced = advancementSelector.IsAdvanced("creature");
            Assert.That(isAdvanced, Is.False);
        }
    }
}
