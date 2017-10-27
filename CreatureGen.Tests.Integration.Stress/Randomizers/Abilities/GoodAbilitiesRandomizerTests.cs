using CreatureGen.Abilities;
using CreatureGen.Randomizers.Abilities;
using Ninject;
using NUnit.Framework;
using System.Linq;

namespace CreatureGen.Tests.Integration.Stress.Randomizers.Abilities
{
    [TestFixture]
    public class GoodAbilitiesRandomizerTests : StressTests
    {
        [Inject, Named(AbilitiesRandomizerTypeConstants.Good)]
        public IAbilitiesRandomizer GoodAbilitiesRandomizer { get; set; }

        [Test]
        public void StressGoodAbilities()
        {
            stressor.Stress(AssertAbilities);
        }

        protected void AssertAbilities()
        {
            var stats = GoodAbilitiesRandomizer.Randomize();

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

            var average = stats.Values.Average(s => s.BaseValue);
            Assert.That(average, Is.InRange(13, 15));
        }

        [Test]
        public void NonDefaultGoodAbilitiesOccur()
        {
            var stats = stressor.GenerateOrFail(GoodAbilitiesRandomizer.Randomize, ss => ss.Values.Any(s => s.BaseValue != 13));
            var allAbilitiesAreDefault = stats.Values.All(s => s.BaseValue == 13);
            Assert.That(allAbilitiesAreDefault, Is.False);
        }
    }
}