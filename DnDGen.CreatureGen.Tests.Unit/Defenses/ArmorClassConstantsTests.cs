using DnDGen.CreatureGen.Defenses;
using NUnit.Framework;

namespace DnDGen.CreatureGen.Tests.Unit.Defenses
{
    [TestFixture]
    public class ArmorClassConstantsTests
    {
        [TestCase(ArmorClassConstants.Armor, "Armor")]
        [TestCase(ArmorClassConstants.Deflection, "Deflection")]
        [TestCase(ArmorClassConstants.Dodge, "Dodge")]
        [TestCase(ArmorClassConstants.Natural, "Natural")]
        [TestCase(ArmorClassConstants.Shield, "Shield")]
        public void ArmorClassConstant(string constant, string value)
        {
            Assert.That(constant, Is.EqualTo(value));
        }
    }
}
