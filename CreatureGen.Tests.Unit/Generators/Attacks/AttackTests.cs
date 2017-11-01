using CreatureGen.Attacks;
using NUnit.Framework;

namespace CreatureGen.Tests.Unit.Generators.Attacks
{
    [TestFixture]
    public class AttackTests
    {
        private Attack attack;

        [SetUp]
        public void Setup()
        {
            attack = new Attack();
        }

        [Test]
        public void AttackInitialized()
        {
            Assert.That(attack.Damage, Is.Empty);
            Assert.That(attack.IsMelee, Is.False);
            Assert.That(attack.IsNatural, Is.False);
            Assert.That(attack.IsPrimary, Is.False);
            Assert.That(attack.IsSpecial, Is.False);
            Assert.That(attack.Name, Is.Empty);
            Assert.That(attack.TotalAttackBonus, Is.EqualTo(0));
        }
    }
}