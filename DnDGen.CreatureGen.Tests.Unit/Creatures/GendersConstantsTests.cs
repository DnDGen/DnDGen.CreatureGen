using DnDGen.CreatureGen.Creatures;
using NUnit.Framework;

namespace DnDGen.CreatureGen.Tests.Unit.Creatures
{
    [TestFixture]
    internal class GendersConstantsTests
    {
        [TestCase(GenderConstants.Agender, "Agender")]
        [TestCase(GenderConstants.Female, "Female")]
        [TestCase(GenderConstants.Hermaphrodite, "Hermaphrodite")]
        [TestCase(GenderConstants.Male, "Male")]
        public void GenderConstant(string constant, string value)
        {
            Assert.That(constant, Is.EqualTo(value));
        }
    }
}
