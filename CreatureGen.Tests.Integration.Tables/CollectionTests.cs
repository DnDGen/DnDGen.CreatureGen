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
            Assert.That(table.Keys, Is.EquivalentTo(names));
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
            Assert.That(table[name], Is.EquivalentTo(collection));
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