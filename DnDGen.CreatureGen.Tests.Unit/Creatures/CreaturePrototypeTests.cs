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
    }
}