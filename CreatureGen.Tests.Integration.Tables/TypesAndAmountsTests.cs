using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CreatureGen.Tests.Integration.Tables
{
    [TestFixture]
    public abstract class TypesAndAmountsTests : CollectionTests
    {
        protected void AssertTypesAndAmounts(string name, IEnumerable<Tuple<string, int>> typesAndAmounts)
        {
            var entries = typesAndAmounts.Select(t => $"{t.Item1}/{t.Item2}").ToArray();
            DistinctCollection(name, entries);
        }
    }
}