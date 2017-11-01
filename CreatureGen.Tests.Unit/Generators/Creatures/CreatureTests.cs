using CreatureGen.Creatures;
using NUnit.Framework;

namespace CreatureGen.Tests.Unit.Generators.Creatures
{
    [TestFixture]
    public class CreatureTests
    {
        private Creature creature;

        [SetUp]
        public void Setup()
        {
            creature = new Creature();
        }

        [Test]
        public void CreatureInitialized()
        {
            Assert.That(creature.Abilities, Is.Empty);
            Assert.That(creature.AerialSpeed, Is.Not.Null);
            Assert.That(creature.AerialSpeed.Unit, Is.EqualTo("feet per round"));
            Assert.That(creature.Alignment, Is.Not.Null);
            Assert.That(creature.ArmorClass, Is.Not.Null);
            Assert.That(creature.Attacks, Is.Empty);
            Assert.That(creature.BaseAttackBonus, Is.EqualTo(0));
            Assert.That(creature.ChallengeRating, Is.Empty);
            Assert.That(creature.Feats, Is.Empty);
            Assert.That(creature.FullMeleeAttack, Is.Empty);
            Assert.That(creature.FullRangedAttack, Is.Empty);
            Assert.That(creature.GrappleBonus, Is.EqualTo(0));
            Assert.That(creature.HitPoints, Is.Not.Null);
            Assert.That(creature.InitiativeBonus, Is.EqualTo(0));
            Assert.That(creature.LandSpeed, Is.Not.Null);
            Assert.That(creature.LandSpeed.Unit, Is.EqualTo("feet per round"));
            Assert.That(creature.LevelAdjustment, Is.EqualTo(0));
            Assert.That(creature.MeleeAttack, Is.Null);
            Assert.That(creature.Name, Is.Empty);
            Assert.That(creature.RangedAttack, Is.Null);
            Assert.That(creature.Reach, Is.Not.Null);
            Assert.That(creature.Reach.Unit, Is.EqualTo("feet"));
            Assert.That(creature.Saves, Is.Not.Null);
            Assert.That(creature.Size, Is.Empty);
            Assert.That(creature.Skills, Is.Empty);
            Assert.That(creature.Space, Is.Not.Null);
            Assert.That(creature.Space.Unit, Is.EqualTo("feet"));
            Assert.That(creature.SpecialAttacks, Is.Empty);
            Assert.That(creature.SpecialQualities, Is.Empty);
            Assert.That(creature.Summary, Is.Empty);
            Assert.That(creature.SwimSpeed, Is.Not.Null);
            Assert.That(creature.SwimSpeed.Unit, Is.EqualTo("feet per round"));
            Assert.That(creature.Template, Is.Empty);
            Assert.That(creature.Type, Is.Not.Null);
        }
    }
}