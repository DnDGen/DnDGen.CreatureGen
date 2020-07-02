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

        private const int EquivalentBatchSize = 100;

        [SetUp]
        public void CollectionSetup()
        {
            collectionMapper = GetNewInstanceOf<CollectionMapper>();

            table = collectionMapper.Map(tableName);
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

        protected void AssertUniqueCollection(IEnumerable<string> collection)
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
            Assert.That(table, Contains.Key(name));
            AssertCollection(table[name], collection);
        }

        private void AssertCollection(IEnumerable<string> source, IEnumerable<string> expected)
        {
            //INFO: This is faster than Is.EquivalentTo() for large collections
            var sourceBatches = source
                .OrderBy(e => e)
                .Batch(EquivalentBatchSize)
                .ToArray();
            var expectedBatches = expected
                .OrderBy(e => e)
                .Batch(EquivalentBatchSize)
                .ToArray();

            for (var i = 0; i < expectedBatches.Length && i < sourceBatches.Length; i++)
            {
                Assert.That(sourceBatches[i], Is.EquivalentTo(expectedBatches[i]), $"Batch {i + 1} of {expectedBatches.Length}");
            }

            if (sourceBatches.Length == expectedBatches.Length)
            {
                Assert.That(source.Count(), Is.EqualTo(expected.Count()));
            }
            else if (sourceBatches.Length < expectedBatches.Length)
            {
                var i = sourceBatches.Length;
                var missingSet = Enumerable.Empty<string>();
                Assert.That(missingSet, Is.EquivalentTo(expectedBatches[i]), $"Batch {i + 1} of {expectedBatches.Length} (Missing)");
            }
            else if (sourceBatches.Length > expectedBatches.Length)
            {
                var i = expectedBatches.Length;
                var extraSet = Enumerable.Empty<string>();
                Assert.That(sourceBatches[i], Is.EquivalentTo(extraSet), $"Batch {i + 1} of {expectedBatches.Length} (Extra)");
            }
        }

        public void AssertOrderedCollection(string name, params string[] collection)
        {
            Assert.That(table.Keys, Contains.Item(name));
            AssertOrderedCollection(table[name], collection);
        }

        protected void AssertOrderedCollection(IEnumerable<string> actual, IEnumerable<string> expected, string metaKey = "")
        {
            PopulateIndices(actual);
            var expectedArray = expected.ToArray();
            var actualArray = actual.ToArray();

            Assert.That(actualArray, Has.Length.EqualTo(expectedArray.Length)
                .And.Length.EqualTo(indices.Count));

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
        }

        public virtual void AssertDistinctCollection(string name, params string[] collection)
        {
            AssertUniqueCollection(collection);
            AssertCollection(name, collection);
            AssertUniqueCollection(table[name]);
        }
    }
}