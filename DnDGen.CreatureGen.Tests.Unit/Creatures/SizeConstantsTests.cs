using DnDGen.CreatureGen.Creatures;
using NUnit.Framework;

namespace DnDGen.CreatureGen.Tests.Unit.Creatures
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

        [Test]
        public void OrderedSizes()
        {
            var orderedSizes = SizeConstants.GetOrdered();
            Assert.That(orderedSizes[0], Is.EqualTo(SizeConstants.Fine));
            Assert.That(orderedSizes[1], Is.EqualTo(SizeConstants.Diminutive));
            Assert.That(orderedSizes[2], Is.EqualTo(SizeConstants.Tiny));
            Assert.That(orderedSizes[3], Is.EqualTo(SizeConstants.Small));
            Assert.That(orderedSizes[4], Is.EqualTo(SizeConstants.Medium));
            Assert.That(orderedSizes[5], Is.EqualTo(SizeConstants.Large));
            Assert.That(orderedSizes[6], Is.EqualTo(SizeConstants.Huge));
            Assert.That(orderedSizes[7], Is.EqualTo(SizeConstants.Gargantuan));
            Assert.That(orderedSizes[8], Is.EqualTo(SizeConstants.Colossal));
            Assert.That(orderedSizes.Length, Is.EqualTo(9));
        }
    }
}
