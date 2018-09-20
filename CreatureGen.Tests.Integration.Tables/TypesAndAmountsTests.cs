using CreatureGen.Selectors.Helpers;
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
            var stringAmounts = typesAndAmounts.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.ToString());
            AssertTypesAndAmounts(name, stringAmounts);
        }

        protected void AssertTypesAndAmounts(string name, Dictionary<string, string> typesAndAmounts)
        {
            var entries = typesAndAmounts.Select(kvp => TypeAndAmountHelper.BuildData(kvp.Key, kvp.Value)).ToArray();
            AssertDistinctCollection(name, entries);
        }
    }
}