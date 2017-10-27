using CreatureGen.Abilities;
using CreatureGen.Randomizers.Abilities;
using Ninject;
using NUnit.Framework;
using System;

namespace CreatureGen.Tests.Integration.Stress.Randomizers.Abilities
{
    [TestFixture]
    public class SetAbilitiesRandomizerTests : StressTests
    {
        [Inject]
        public ISetAbilitiesRandomizer SetAbilitiesRandomizer { get; set; }
        [Inject]
        public Random Random { get; set; }

        [Test]
        public void StressSetAbilities()
        {
            stressor.Stress(GenerateAndAssertAbilities);
        }

        protected void GenerateAndAssertAbilities()
        {
            SetAbilitiesRandomizer.SetCharisma = Random.Next();
            SetAbilitiesRandomizer.SetConstitution = Random.Next();
            SetAbilitiesRandomizer.SetDexterity = Random.Next();
            SetAbilitiesRandomizer.SetIntelligence = Random.Next();
            SetAbilitiesRandomizer.SetStrength = Random.Next();
            SetAbilitiesRandomizer.SetWisdom = Random.Next();

            var stats = SetAbilitiesRandomizer.Randomize();

            Assert.That(stats.Count, Is.EqualTo(6));
            Assert.That(stats.Keys, Contains.Item(AbilityConstants.Charisma));
            Assert.That(stats.Keys, Contains.Item(AbilityConstants.Constitution));
            Assert.That(stats.Keys, Contains.Item(AbilityConstants.Dexterity));
            Assert.That(stats.Keys, Contains.Item(AbilityConstants.Intelligence));
            Assert.That(stats.Keys, Contains.Item(AbilityConstants.Strength));
            Assert.That(stats.Keys, Contains.Item(AbilityConstants.Wisdom));
            Assert.That(stats[AbilityConstants.Charisma].BaseValue, Is.EqualTo(SetAbilitiesRandomizer.SetCharisma));
            Assert.That(stats[AbilityConstants.Constitution].BaseValue, Is.EqualTo(SetAbilitiesRandomizer.SetConstitution));
            Assert.That(stats[AbilityConstants.Dexterity].BaseValue, Is.EqualTo(SetAbilitiesRandomizer.SetDexterity));
            Assert.That(stats[AbilityConstants.Intelligence].BaseValue, Is.EqualTo(SetAbilitiesRandomizer.SetIntelligence));
            Assert.That(stats[AbilityConstants.Strength].BaseValue, Is.EqualTo(SetAbilitiesRandomizer.SetStrength));
            Assert.That(stats[AbilityConstants.Wisdom].BaseValue, Is.EqualTo(SetAbilitiesRandomizer.SetWisdom));
        }
    }
}