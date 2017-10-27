using NUnit.Framework;
using System;
using System.Linq;

namespace CreatureGen.Tests.Integration.Tables
{
    [TestFixture]
    public abstract class AdjustmentsTests : CollectionTests
    {
        protected const string TrueString = "True";
        protected const string FalseString = "False";

        public virtual void Adjustment(string name, int adjustment)
        {
            Assert.That(table.Keys, Contains.Item(name), tableName);

            var actualAdjustment = Convert.ToInt32(table[name].Single());
            Assert.That(actualAdjustment, Is.EqualTo(adjustment));
        }
    }
}