using DnDGen.Core.Mappers.Collections;
using Ninject;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace CreatureGen.Tests.Integration.Tables
{
    [TestFixture]
    public abstract class CollectionTests : TableTests
    {
        [Inject]
        internal CollectionMapper CollectionMapper { get; set; }

        protected Dictionary<string, IEnumerable<string>> table;
        protected Dictionary<int, string> indices;

        [SetUp]
        public void CollectionSetup()
        {
            table = CollectionMapper.Map(tableName);
            indices = new Dictionary<int, string>();
        }

        protected void AssertCollectionNames(IEnumerable<string> names)
        {
            AssertUnique(names);
            AssertCollection(table.Keys, names);
        }

        protected IEnumerable<string> GetCollection(string name)
        {
            return table[name];
        }

        private void AssertUnique(IEnumerable<string> collection)
        {
            var duplicateItems = collection.Where(s => collection.Count(c => c == s) > 1);
            var duplicates = string.Join(", ", duplicateItems.Distinct());

            Assert.That(collection, Is.Unique, $"Collection is not distinct: {duplicates}");
        }

        protected virtual void PopulateIndices(IEnumerable<string> collection)
        {
            for (var i = 0; i < collection.Count(); i++)
                indices[i] = string.Empty;
        }

        public virtual void Collection(string name, params string[] collection)
        {
            Assert.That(table.Keys, Contains.Item(name), tableName);

            if (table[name].Count() == 1 && collection.Count() == 1)
                Assert.That(table[name].Single(), Is.EqualTo(collection.Single()));

            AssertCollection(table[name], collection);
        }

        protected void AssertCollection(IEnumerable<string> actual, IEnumerable<string> expected, string message = "")
        {
            AssertMissingItems(actual, expected, message);
            AssertExtraItems(actual, expected, message);
            Assert.That(actual, Is.EquivalentTo(expected), message);
            Assert.That(expected, Is.EquivalentTo(actual), message);
            Assert.That(actual.Count(), Is.EqualTo(expected.Count()), message);
        }

        protected void AssertMissingItems(IEnumerable<string> actual, IEnumerable<string> expected, string message = "")
        {
            var missingItems = expected.Except(actual);
            Assert.That(missingItems, Is.Empty, $"{message}: {missingItems.Count()} of {expected.Count()} missing");
        }

        protected void AssertExtraItems(IEnumerable<string> actual, IEnumerable<string> expected, string message = "")
        {
            var extras = actual.Except(expected);
            Assert.That(extras, Is.Empty, $"{message}: {extras.Count()} extra");
        }

        public virtual void OrderedCollection(string name, params string[] collection)
        {
            AssertOrdered(table[name], collection);
        }

        private void AssertOrdered(IEnumerable<string> actual, IEnumerable<string> expected)
        {
            PopulateIndices(actual);
            var expectedArray = expected.ToArray();
            var actualArray = actual.ToArray();

            foreach (var index in indices.Keys.OrderBy(k => k))
            {
                var expectedItem = expectedArray[index];

                var message = string.Format("Index {0}", index);
                if (!string.IsNullOrEmpty(indices[index]))
                    message += string.Format(" ({0})", indices[index]);

                Assert.That(actualArray.Length - 1, Is.AtLeast(index), message);

                var actualItem = actualArray[index];

                Assert.That(actualItem, Is.EqualTo(expectedItem), message);
            }

            Assert.That(actual.Count(), Is.EqualTo(expected.Count()));
        }

        public virtual void DistinctCollection(string name, params string[] collection)
        {
            AssertUnique(collection);
            Collection(name, collection);
            AssertUnique(table[name]);
        }
    }
}