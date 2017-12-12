using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace CreatureGen.Tests.Integration.Tables
{
    [TestFixture]
    public abstract class TypesAndAmountsTests : CollectionTests
    {
        protected void AssertTypesAndAmounts(string name, Dictionary<string, int> typesAndAmounts)
        {
            var entries = typesAndAmounts.Select(kvp => $"{kvp.Key}/{kvp.Value}").ToArray();
            DistinctCollection(name, entries);
        }

        protected void AssertTypesAndAmounts(string name, Dictionary<string, string> typesAndAmounts)
        {
            var entries = typesAndAmounts.Select(kvp => $"{kvp.Key}/{kvp.Value}").ToArray();
            DistinctCollection(name, entries);
        }
    }
}