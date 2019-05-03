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
            AssertUniqueCollection(names);
            AssertCollection(table.Keys, names);
        }

        protected IEnumerable<string> GetCollection(string name)
        {
            return table[name];
        }

        private void AssertUniqueCollection(IEnumerable<string> collection)
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

        public void AssertCollection(string name, params string[] collection)
        {
            Assert.That(table.Keys, Contains.Item(name));
            AssertCollection(table[name], collection);
        }

        protected void AssertCollection(IEnumerable<string> actual, IEnumerable<string> expected, string message = "")
        {
            var actualCount = actual.Count();
            var expectedCount = expected.Count();

            //INFO: Is.Equivalent is not performant when more than 1K items in the collection
            if (actualCount < 1000 && expectedCount < 1000)
            {
                Assert.That(actual, Is.EquivalentTo(expected), message);
                Assert.That(expected, Is.EquivalentTo(actual), message);
            }
            else
            {
                var missing = expected.Except(actual);
                var extra = actual.Except(expected);

                Assert.That(missing, Is.Empty, $"Missing: {message}");
                Assert.That(extra, Is.Empty, $"Extra: {message}");
            }

            Assert.That(actualCount, Is.EqualTo(expectedCount), message);
        }

        public void AssertOrderedCollection(string name, params string[] collection)
        {
            Assert.That(table.Keys, Contains.Item(name));
            AssertOrderedCollection(table[name], collection);
        }

        private void AssertOrderedCollection(IEnumerable<string> actual, IEnumerable<string> expected)
        {
            PopulateIndices(actual);
            var expectedArray = expected.ToArray();
            var actualArray = actual.ToArray();

            Assert.That(actualArray.Length, Is.EqualTo(expectedArray.Length));

            foreach (var index in indices.Keys.OrderBy(k => k))
            {
                var expectedItem = expectedArray[index];
                var actualItem = actualArray[index];

                var message = string.Format("Index {0}", index);
                if (!string.IsNullOrEmpty(indices[index]))
                    message += string.Format(" ({0})", indices[index]);

                Assert.That(actualItem, Is.EqualTo(expectedItem), message);
            }
        }

        public virtual void AssertDistinctCollection(string name, params string[] collection)
        {
            AssertUniqueCollection(collection);
            AssertCollection(name, collection);
            AssertUniqueCollection(table[name]);
        }
    }
}