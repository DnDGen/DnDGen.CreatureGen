using DnDGen.Infrastructure.Mappers.Collections;
using MoreLinq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Integration.Tables
{
    [TestFixture]
    public abstract class CollectionTests : TableTests
    {
        protected CollectionMapper collectionMapper;

        protected Dictionary<string, IEnumerable<string>> table;
        protected Dictionary<int, string> indices;

        [SetUp]
        public void CollectionSetup()
        {
            collectionMapper = GetNewInstanceOf<CollectionMapper>();

            table = collectionMapper.Map(Config.Name, tableName);
            indices = [];
        }

        protected void AssertCollectionNames(IEnumerable<string> names)
        {
            AssertUniqueCollection(names);
            AssertCollection(table.Keys, names, "Table Keys");
        }

        protected IEnumerable<string> GetCollection(string name)
        {
            return table[name];
        }

        protected void AssertUniqueCollection(IEnumerable<string> collection)
        {
            Assert.That(collection, Is.Unique);
        }

        protected virtual void PopulateIndices(IEnumerable<string> collection)
        {
            indices = [];

            for (var i = 0; i < collection.Count(); i++)
                indices[i] = string.Empty;
        }

        public void AssertCollection(string name, params string[] collection)
        {
            Assert.That(table, Contains.Key(name));
            AssertCollection(table[name], collection, name);
        }

        private void AssertCollection(IEnumerable<string> source, IEnumerable<string> expected, string message)
        {
            //If descriptions are longer than this, there is a chance that the test failure message will be truncated
            //When the test failure message is truncated, we might not be able to assess what to fix
            if (expected.Sum(e => e.Length) > 150 * 10)
            {
                Assert.That(source.OrderBy(s => s), Is.EquivalentTo(expected.OrderBy(e => e)), message);
                return;
            }

            AssertOrderedCollection(source.OrderBy(s => s), expected.OrderBy(e => e), message);
        }

        public void AssertWeightedCollection(string name, params string[] collection)
        {
            Assert.That(table, Contains.Key(name));
            AssertWeightedCollection(table[name], collection, name);
        }

        private void AssertWeightedCollection(IEnumerable<string> source, IEnumerable<string> expected, string message)
        {
            var distinctSource = source.Distinct();
            var distinctExpected = expected.Distinct();
            AssertCollection(distinctSource, distinctExpected, $"{message}: Distinct appearances");

            foreach (var sourceItem in distinctSource)
            {
                var sourceCount = source.Count(s => s == sourceItem);
                var expectedCount = expected.Count(s => s == sourceItem);
                Assert.That(sourceCount, Is.EqualTo(expectedCount), $"{message}: {sourceItem}");
            }
        }

        public void AssertOrderedCollection(string name, params string[] collection)
        {
            Assert.That(table.Keys, Contains.Item(name));
            AssertOrderedCollection(table[name], collection);
        }

        protected void AssertOrderedCollection(IEnumerable<string> actual, IEnumerable<string> expected, string metaKey = "")
        {
            var expectedArray = expected.ToArray();
            var actualArray = actual.ToArray();

            var shorter = actualArray.Length < expectedArray.Length ? actual : expected;
            PopulateIndices(shorter);

            foreach (var index in indices.Keys.OrderBy(k => k))
            {
                var expectedItem = expectedArray[index];
                var actualItem = actualArray[index];

                var message = $"Index {index}";

                if (!string.IsNullOrEmpty(metaKey))
                    message = $"Key {metaKey}: {message}";

                if (!string.IsNullOrEmpty(indices[index]))
                    message += $" ({indices[index]})";

                Assert.That(actualItem, Is.EqualTo(expectedItem), message);
            }

            Assert.That(expectedArray.Except(actualArray), Is.Empty, $"Extra: Key {metaKey}");
            Assert.That(actualArray.Except(expectedArray), Is.Empty, $"Missing: Key {metaKey}");
        }

        public virtual void AssertDistinctCollection(string name, params string[] collection)
        {
            AssertUniqueCollection(collection);
            AssertCollection(name, collection);
            AssertUniqueCollection(table[name]);
        }
    }
}