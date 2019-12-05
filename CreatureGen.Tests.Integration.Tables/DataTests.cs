using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CreatureGen.Tests.Integration.Tables
{
    [TestFixture]
    public abstract class DataTests : CollectionTests
    {
        protected void AssertSegmentedData(string name, IEnumerable<string> data)
        {
            AssertOrderedCollection(name, data.ToArray());
        }

        protected void AssertData(string name, IEnumerable<string[]> data, Func<string[], string> buildEntry)
        {
            var entries = data.Select(d => buildEntry(d)).ToArray();
            AssertDistinctCollection(name, entries);
        }
    }
}