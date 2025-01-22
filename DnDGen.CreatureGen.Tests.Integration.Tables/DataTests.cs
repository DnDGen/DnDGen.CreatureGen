using DnDGen.CreatureGen.Selectors.Helpers;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Integration.Tables
{
    [TestFixture]
    public abstract class DataTests : CollectionTests
    {
        protected DataHelper helper;

        protected void AssertSegmentedData(string name, IEnumerable<string> data)
        {
            AssertOrderedCollection(name, data.ToArray());
        }

        protected void AssertData(string name, IEnumerable<string[]> data)
        {
            var entries = data.Select(d => helper.BuildEntry(d)).ToArray();
            AssertData(name, entries);
        }

        private void AssertData(string name, IEnumerable<string> entries)
        {
            AssertUniqueCollection(entries, $"{name}: Expected Data Entries");
            AssertUniqueCollection(entries.Select(e => helper.BuildKey(name, e)), $"{name}: Expected Data Entry Keys");

            Assert.That(table, Contains.Key(name));
            var isValid = entries.All(helper.ValidateEntry) && table[name].All(helper.ValidateEntry);
            if (table[name].Count() != entries.Count() || !isValid)
            {
                Assert.That(table[name], Is.EquivalentTo(entries));
            }

            var actual = table[name]
                .Select(helper.ParseEntry)
                .OrderBy(d => helper.BuildKey(name, d))
                .ToArray();
            var expected = entries
                .Select(helper.ParseEntry)
                .OrderBy(d => helper.BuildKey(name, d))
                .ToArray();

            AssertUniqueCollection(table[name].Select(e => helper.BuildKey(name, e)), $"{name}: Actual Data Entry Keys");

            var actualKeys = actual.Select(d => helper.BuildKey(name, d));
            var expectedKeys = expected.Select(d => helper.BuildKey(name, d));

            Assert.That(actualKeys, Is.EqualTo(expectedKeys), "Attack Keys");

            for (var i = 0; i < actual.Length; i++)
            {
                var actualKey = helper.BuildKey(name, actual[i]);
                var expectedKey = helper.BuildKey(name, expected[i]);

                Assert.That(actualKey, Is.EqualTo(expectedKey), $"Index {i}");
                AssertOrderedCollection(actual[i], expected[i], actualKey);
            }

            AssertUniqueCollection(table[name], $"{name}: Actual Data Entries");
        }
    }
}