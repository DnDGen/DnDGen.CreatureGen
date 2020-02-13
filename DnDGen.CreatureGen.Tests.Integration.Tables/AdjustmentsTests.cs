using NUnit.Framework;
using System;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Integration.Tables
{
    [TestFixture]
    public abstract class AdjustmentsTests : CollectionTests
    {
        protected const string TrueString = "True";
        protected const string FalseString = "False";

        public virtual void AssertAdjustment(string name, double adjustment)
        {
            Assert.That(table.Keys, Contains.Item(name), tableName);

            var actualAdjustment = GetAdjustment(name);
            Assert.That(actualAdjustment, Is.EqualTo(adjustment));
        }

        protected double GetAdjustment(string name)
        {
            Assert.That(table.Keys, Contains.Item(name), tableName);

            var adjustment = table[name].Single();
            return Convert.ToDouble(adjustment);
        }
    }
}