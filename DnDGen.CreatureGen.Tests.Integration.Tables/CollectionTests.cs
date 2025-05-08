using DnDGen.Infrastructure.Mappers.Collections;
using DnDGen.Infrastructure.Selectors.Collections;
using MoreLinq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Integration.Tables
{
    [TestFixture]
    public abstract class CollectionTests : TableTests
    {
        protected CollectionMapper collectionMapper;
        protected ICollectionSelector collectionSelector;

        protected Dictionary<string, IEnumerable<string>> table;
        protected Dictionary<int, string> indices;

        [SetUp]
        public void CollectionSetup()
        {
            collectionMapper = GetNewInstanceOf<CollectionMapper>();
            collectionSelector = GetNewInstanceOf<ICollectionSelector>();

            table = collectionMapper.Map(Config.Name, tableName);
            indices = [];
        }

        protected void AssertCollectionNames(IEnumerable<string> names)
        {
            AssertUniqueCollection(names, "Collection Names");
            AssertCollection(table.Keys, names, "Table Keys");
        }

        protected IEnumerable<string> GetCollection(string name)
        {
            return table[name];
        }

        protected void AssertUniqueCollection(IEnumerable<string> collection, string message)
        {
            Assert.That(collection, Is.Unique, message);
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

        private void AssertCollection(IEnumerable<string> source, IEnumerable<string> expected, string message = "")
        {
            Assert.That(source.OrderBy(s => s), Is.EquivalentTo(expected.OrderBy(e => e)), message);
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

            Assert.That(expectedArray.Except(actualArray), Is.Empty, $"Add to XML: Key {metaKey}");
            Assert.That(actualArray.Except(expectedArray), Is.Empty, $"Remove from XML: Key {metaKey}");
        }

        public virtual void AssertDistinctCollection(string name, params string[] collection)
        {
            Assert.That(table, Contains.Key(name), name);
            AssertDistinctCollection(table[name], collection, name);
        }

        public virtual void AssertDistinctCollection(IEnumerable<string> actual, IEnumerable<string> expected, string message = "")
        {
            AssertUniqueCollection(expected, $"{message}: Expected");
            AssertCollection(actual, expected, message);
            AssertUniqueCollection(actual, $"{message}: Actual");
        }

        [Obsolete("Just don't use this, make a proper flat collection")]
        protected IEnumerable<string> ExplodeCollection(string tableName, string collectionName) => ExplodeRecursive(tableName, collectionName);

        private List<string> ExplodeRecursive(string tableName, string collectionName)
        {
            var rootCollection = collectionSelector.SelectFrom(Config.Name, tableName, collectionName);
            var explodedCollection = new List<string>();

            foreach (var entry in rootCollection)
            {
                if (collectionSelector.IsCollection(Config.Name, tableName, entry) && entry != collectionName)
                {
                    var subCollection = ExplodeRecursive(tableName, entry);

                    explodedCollection.AddRange(subCollection);
                }
                else
                {
                    explodedCollection.Add(entry);
                }
            }

            return explodedCollection;
        }
    }
}