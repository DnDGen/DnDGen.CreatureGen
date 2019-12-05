using CreatureGen.Selectors.Helpers;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace CreatureGen.Tests.Integration.Tables
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
            AssertUniqueCollection(entries);

            Assert.That(table, Contains.Key(name));
            if (table[name].Count() != entries.Count())
            {
                Assert.That(table[name], Is.EquivalentTo(entries));
                return;
            }

            var actual = table[name]
                .Select(helper.ParseEntry)
                .OrderBy(d => helper.BuildKey(name, d))
                .ToArray();
            var expected = entries
                .Select(helper.ParseEntry)
                .OrderBy(d => helper.BuildKey(name, d))
                .ToArray();

            for (var i = 0; i < actual.Length; i++)
            {
                AssertOrderedCollection(actual[i], expected[i], i);
            }

            AssertUniqueCollection(table[name]);
        }
    }
}