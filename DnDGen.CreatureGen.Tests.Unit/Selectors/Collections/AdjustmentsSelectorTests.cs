using DnDGen.CreatureGen.Selectors.Collections;
using DnDGen.Infrastructure.Selectors.Collections;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Unit.Selectors.Collections
{
    [TestFixture]
    public class AdjustmentsSelectorTests
    {
        private const string TableName = "table name";

        private IAdjustmentsSelector adjustmentsSelector;
        private Mock<ICollectionSelector> mockCollectionsSelector;
        private Dictionary<string, IEnumerable<string>> collections;

        [SetUp]
        public void Setup()
        {
            mockCollectionsSelector = new Mock<ICollectionSelector>();
            adjustmentsSelector = new AdjustmentsSelector(mockCollectionsSelector.Object);
            collections = new Dictionary<string, IEnumerable<string>>();

            mockCollectionsSelector.Setup(m => m.SelectAllFrom(TableName)).Returns(collections);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableName, It.IsAny<string>()))
                .Returns((string table, string name) => collections[name]);
        }

        [Test]
        public void SelectAllAdjustments()
        {
            collections["first"] = new[] { "9266" };
            collections["second"] = new[] { "42" };

            var adjustments = adjustmentsSelector.SelectAllFrom(TableName);
            Assert.That(adjustments["first"], Is.EqualTo(9266));
            Assert.That(adjustments["second"], Is.EqualTo(42));
        }

        [Test]
        public void ThrowExceptionIfAnyEmptyCollections()
        {
            collections["first"] = Enumerable.Empty<string>();
            collections["second"] = new[] { "42" };

            Assert.That(() => adjustmentsSelector.SelectAllFrom(TableName), Throws.InstanceOf<InvalidOperationException>());
        }

        [Test]
        public void SelectAdjustmentAsInt()
        {
            collections["first"] = new[] { "9266" };
            collections["second"] = new[] { "42" };

            var adjustment = adjustmentsSelector.SelectFrom<int>(TableName, "second");
            Assert.That(adjustment, Is.EqualTo(42));
        }

        [Test]
        public void SelectAdjustmentAsDouble()
        {
            collections["first"] = new[] { "92.66" };
            collections["second"] = new[] { "4.2" };

            var adjustment = adjustmentsSelector.SelectFrom<double>(TableName, "second");
            Assert.That(adjustment, Is.EqualTo(4.2));
        }

        [Test]
        public void SelectAdjustmentThrowsExceptionIfCollectionIsEmpty()
        {
            collections["first"] = Enumerable.Empty<string>();
            collections["second"] = new[] { "42" };

            Assert.That(() => adjustmentsSelector.SelectFrom<int>(TableName, "first"), Throws.InstanceOf<InvalidOperationException>());
        }

        [Test]
        public void SelectAdjustmentThrowsExceptionIfNameNotThere()
        {
            collections["first"] = new[] { "9266" };

            Assert.That(() => adjustmentsSelector.SelectFrom<int>(TableName, "second"), Throws.Exception);
        }
    }
}