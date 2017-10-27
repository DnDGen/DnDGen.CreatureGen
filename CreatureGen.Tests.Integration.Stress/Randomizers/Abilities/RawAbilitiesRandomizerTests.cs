using CreatureGen.Abilities;
using CreatureGen.Randomizers.Abilities;
using Ninject;
using NUnit.Framework;
using System.Linq;

namespace CreatureGen.Tests.Integration.Stress.Randomizers.Abilities
{
    [TestFixture]
    public class RawAbilitiesRandomizerTests : StressTests
    {
        [Inject, Named(AbilitiesRandomizerTypeConstants.Raw)]
        public IAbilitiesRandomizer RawAbilitiesRandomizer { get; set; }

        [Test]
        public void StressRawAbilities()
        {
            stressor.Stress(AssertAbilities);
        }

        protected void AssertAbilities()
        {
            var stats = RawAbilitiesRandomizer.Randomize();

            Assert.That(stats.Count, Is.EqualTo(6));
            Assert.That(stats.Keys, Contains.Item(AbilityConstants.Charisma));
            Assert.That(stats.Keys, Contains.Item(AbilityConstants.Constitution));
            Assert.That(stats.Keys, Contains.Item(AbilityConstants.Dexterity));
            Assert.That(stats.Keys, Contains.Item(AbilityConstants.Intelligence));
            Assert.That(stats.Keys, Contains.Item(AbilityConstants.Strength));
            Assert.That(stats.Keys, Contains.Item(AbilityConstants.Wisdom));
            Assert.That(stats[AbilityConstants.Charisma].BaseValue, Is.InRange(3, 18));
            Assert.That(stats[AbilityConstants.Constitution].BaseValue, Is.InRange(3, 18));
            Assert.That(stats[AbilityConstants.Dexterity].BaseValue, Is.InRange(3, 18));
            Assert.That(stats[AbilityConstants.Intelligence].BaseValue, Is.InRange(3, 18));
            Assert.That(stats[AbilityConstants.Strength].BaseValue, Is.InRange(3, 18));
            Assert.That(stats[AbilityConstants.Wisdom].BaseValue, Is.InRange(3, 18));
        }

        [Test]
        public void NonDefaultRawAbilitiesOccur()
        {
            var stats = stressor.GenerateOrFail(RawAbilitiesRandomizer.Randomize, ss => ss.Values.Any(s => s.BaseValue != 10));
            var allAbilitiesAreDefault = stats.Values.All(s => s.BaseValue == 10);
            Assert.That(allAbilitiesAreDefault, Is.False);
        }
    }
}