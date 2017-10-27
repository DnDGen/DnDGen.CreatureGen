using NUnit.Framework;
using System;

namespace CreatureGen.Tests.Unit.Common.Items
{
    [TestFixture]
    public class ProficiencyConstantsTests
    {
        public void Constant(String constant, String value)
        {
            Assert.That(constant, Is.EqualTo(value));
        }
    }
}