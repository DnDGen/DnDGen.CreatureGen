using DnDGen.CreatureGen.Attacks;
using NUnit.Framework;

namespace DnDGen.CreatureGen.Tests.Unit.Attacks
{
    [TestFixture]
    public class DamageTests
    {
        private Damage damage;

        [SetUp]
        public void Setup()
        {
            damage = new Damage();
        }

        [Test]
        public void DamageInitialized()
        {
            Assert.That(damage.Roll, Is.Empty);
            Assert.That(damage.Type, Is.Empty);
            Assert.That(damage.ToString(), Is.Empty);
        }

        [Test]
        public void ToString_WithRollAndType()
        {
            damage.Roll = "9266d90210";
            damage.Type = "emotional";

            Assert.That(damage.ToString(), Is.EqualTo("9266d90210 emotional"));
        }
    }
}
