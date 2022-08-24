using DnDGen.CreatureGen.Creatures;
using NUnit.Framework;

namespace DnDGen.CreatureGen.Tests.Unit.Creatures
{
    [TestFixture]
    public class CreaturePrototypeTests
    {
        private CreaturePrototype creature;

        [SetUp]
        public void Setup()
        {
            creature = new CreaturePrototype();
        }

        [Test]
        public void CreaturePrototypeInitialized()
        {
            Assert.That(creature.Abilities, Is.Empty);
            Assert.That(creature.Alignments, Is.Not.Null);
            Assert.That(creature.ChallengeRating, Is.Empty);
            Assert.That(creature.LevelAdjustment, Is.Null);
            Assert.That(creature.Name, Is.Empty);
            Assert.That(creature.Type, Is.Not.Null);
            Assert.That(creature.Type.Name, Is.Empty);
            Assert.That(creature.Type.SubTypes, Is.Empty);
            Assert.That(creature.CasterLevel, Is.Zero);
            Assert.That(creature.HitDiceQuantity, Is.Zero);
        }

        [TestCase(0, 0)]
        [TestCase(.01, 1)]
        [TestCase(.1, 1)]
        [TestCase(.25, 1)]
        [TestCase(.5, 1)]
        [TestCase(.6, 1)]
        [TestCase(.9266, 1)]
        [TestCase(1, 1)]
        [TestCase(1.5, 1)]
        [TestCase(2, 2)]
        [TestCase(2.5, 2)]
        [TestCase(2.999999999, 2)]
        [TestCase(9.266, 9)]
        [TestCase(92.66, 92)]
        [TestCase(926.6, 926)]
        [TestCase(9266, 9266)]
        public void GetRoundedHitDiceQuantity_ReturnsRoundedValue(double quantity, int roundedValue)
        {
            creature.HitDiceQuantity = quantity;

            var roundedQuantity = creature.GetRoundedHitDiceQuantity();
            Assert.That(roundedQuantity, Is.EqualTo(roundedValue));
        }
    }
}