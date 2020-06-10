using DnDGen.CreatureGen.Magics;
using NUnit.Framework;

namespace DnDGen.CreatureGen.Tests.Unit.Magics
{
    [TestFixture]
    public class MagicTests
    {
        private Magic magic;

        [SetUp]
        public void Setup()
        {
            magic = new Magic();
        }

        [Test]
        public void MagicInitialized()
        {
            Assert.That(magic.SpellsPerDay, Is.Empty);
            Assert.That(magic.ArcaneSpellFailure, Is.Zero);
            Assert.That(magic.KnownSpells, Is.Empty);
            Assert.That(magic.PreparedSpells, Is.Empty);
            Assert.That(magic.Caster, Is.Empty);
            Assert.That(magic.CasterLevel, Is.Zero);
            Assert.That(magic.Domains, Is.Empty);
        }
    }
}