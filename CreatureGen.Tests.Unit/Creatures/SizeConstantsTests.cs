using CreatureGen.Creatures;
using NUnit.Framework;

namespace CreatureGen.Tests.Unit.Creatures
{
    [TestFixture]
    public class SizeConstantsTests
    {
        [TestCase(SizeConstants.Colossal, "Colossal")]
        [TestCase(SizeConstants.Gargantuan, "Gargantuan")]
        [TestCase(SizeConstants.Huge, "Huge")]
        [TestCase(SizeConstants.Large, "Large")]
        [TestCase(SizeConstants.Medium, "Medium")]
        [TestCase(SizeConstants.Small, "Small")]
        [TestCase(SizeConstants.Tiny, "Tiny")]
        [TestCase(SizeConstants.Diminutive, "Diminutive")]
        [TestCase(SizeConstants.Fine, "Fine")]
        public void SizeConstant(string constant, string value)
        {
            Assert.That(constant, Is.EqualTo(value));
        }
    }
}
