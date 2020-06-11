using DnDGen.CreatureGen.Magics;
using NUnit.Framework;

namespace DnDGen.CreatureGen.Tests.Unit.Magics
{
    [TestFixture]
    public class SpellQuantityTests
    {
        private SpellQuantity spellQuantity;

        [SetUp]
        public void Setup()
        {
            spellQuantity = new SpellQuantity();
        }

        [Test]
        public void SpellQuantityInitialized()
        {
            Assert.That(spellQuantity.BonusSpells, Is.Zero);
            Assert.That(spellQuantity.HasDomainSpell, Is.False);
            Assert.That(spellQuantity.Level, Is.Zero);
            Assert.That(spellQuantity.Quantity, Is.Zero);
            Assert.That(spellQuantity.Source, Is.Empty);
            Assert.That(spellQuantity.TotalQuantity, Is.Zero);
        }

        [Test]
        public void TotalQuantity_NoQuantity_NoBonus_NoDomain()
        {
            spellQuantity.Quantity = 0;
            Assert.That(spellQuantity.TotalQuantity, Is.Zero);
        }

        [Test]
        public void TotalQuantity_NoQuantity_NoBonus_Domain()
        {
            spellQuantity.Quantity = 0;
            spellQuantity.HasDomainSpell = true;

            Assert.That(spellQuantity.TotalQuantity, Is.EqualTo(1));
        }

        [Test]
        public void TotalQuantity_NoQuantity_Bonus_NoDomain()
        {
            spellQuantity.Quantity = 0;
            spellQuantity.BonusSpells = 90210;

            Assert.That(spellQuantity.TotalQuantity, Is.EqualTo(90210));
        }

        [Test]
        public void TotalQuantity_NoQuantity_Bonus_Domain()
        {
            spellQuantity.Quantity = 0;
            spellQuantity.BonusSpells = 90210;
            spellQuantity.HasDomainSpell = true;

            Assert.That(spellQuantity.TotalQuantity, Is.EqualTo(90210 + 1));
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(10)]
        [TestCase(9266)]
        public void TotalQuantity_Quantity_NoBonus_NoDomain(int quantity)
        {
            spellQuantity.Quantity = quantity;
            Assert.That(spellQuantity.TotalQuantity, Is.EqualTo(quantity));
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(10)]
        [TestCase(9266)]
        public void TotalQuantity_Quantity_NoBonus_Domain(int quantity)
        {
            spellQuantity.Quantity = quantity;
            spellQuantity.HasDomainSpell = true;

            Assert.That(spellQuantity.TotalQuantity, Is.EqualTo(quantity + 1));
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(10)]
        [TestCase(9266)]
        public void TotalQuantity_Quantity_Bonus_NoDomain(int quantity)
        {
            spellQuantity.Quantity = quantity;
            spellQuantity.BonusSpells = 90210;

            Assert.That(spellQuantity.TotalQuantity, Is.EqualTo(quantity + 90210));
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(10)]
        [TestCase(9266)]
        public void TotalQuantity_Quantity_Bonus_Domain(int quantity)
        {
            spellQuantity.Quantity = quantity;
            spellQuantity.BonusSpells = 90210;
            spellQuantity.HasDomainSpell = true;

            Assert.That(spellQuantity.TotalQuantity, Is.EqualTo(quantity + 90210 + 1));
        }
    }
}
