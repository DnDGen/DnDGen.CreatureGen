using NUnit.Framework;
using System.Collections.Generic;

namespace DnDGen.CreatureGen.Tests.Integration.Tables
{
    [TestFixture]
    public abstract class AdjustmentsTests : TypesAndAmountsTests
    {
        public virtual void AssertAdjustment(string name, double adjustment)
        {
            var typesAndAmounts = new Dictionary<string, string> { [string.Empty] = adjustment.ToString() };
            AssertTypesAndAmounts(name, typesAndAmounts);
        }
    }
}