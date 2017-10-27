using CreatureGen.Abilities;
using CreatureGen.Domain.Generators.Randomizers.Abilities;
using CreatureGen.Randomizers.Abilities;
using NUnit.Framework;

namespace CreatureGen.Tests.Unit.Generators.Randomizers.Abilities
{
    [TestFixture]
    public class SetAbilitiesRandomizerTests
    {
        private ISetAbilitiesRandomizer randomizer;

        [SetUp]
        public void Setup()
        {
            randomizer = new SetAbilitiesRandomizer();
        }

        [Test]
        public void SetAbilitiesRandomizerIsAnAbilitiesRandomizer()
        {
            Assert.That(randomizer, Is.InstanceOf<IAbilitiesRandomizer>());
        }

        [Test]
        public void SetAbilitiesContainAllAbilities()
        {
            var stats = randomizer.Randomize();

            Assert.That(stats.Count, Is.EqualTo(6));
            Assert.That(stats.Keys, Contains.Item(AbilityConstants.Charisma));
            Assert.That(stats.Keys, Contains.Item(AbilityConstants.Constitution));
            Assert.That(stats.Keys, Contains.Item(AbilityConstants.Dexterity));
            Assert.That(stats.Keys, Contains.Item(AbilityConstants.Intelligence));
            Assert.That(stats.Keys, Contains.Item(AbilityConstants.Strength));
            Assert.That(stats.Keys, Contains.Item(AbilityConstants.Wisdom));
        }

        [Test]
        public void AbilitiesAreNotNull()
        {
            var stats = randomizer.Randomize();

            foreach (var stat in stats)
                Assert.That(stat, Is.Not.Null);
        }

        [Test]
        public void AbilitiesDefaultTo10()
        {
            var stats = randomizer.Randomize();

            foreach (var stat in stats.Values)
                Assert.That(stat.BaseValue, Is.EqualTo(10));
        }

        [Test]
        public void AllowAdjustmentsByDefault()
        {
            Assert.That(randomizer.AllowAdjustments, Is.True);
        }

        [Test]
        public void ReturnSetAbilities()
        {
            randomizer.SetStrength = 9266;
            randomizer.SetCharisma = 90210;
            randomizer.SetConstitution = 42;
            randomizer.SetDexterity = -3;
            randomizer.SetIntelligence = int.MaxValue;
            randomizer.SetWisdom = int.MinValue;

            var stats = randomizer.Randomize();
            Assert.That(stats[AbilityConstants.Charisma].BaseValue, Is.EqualTo(90210));
            Assert.That(stats[AbilityConstants.Constitution].BaseValue, Is.EqualTo(42));
            Assert.That(stats[AbilityConstants.Dexterity].BaseValue, Is.EqualTo(-3));
            Assert.That(stats[AbilityConstants.Intelligence].BaseValue, Is.EqualTo(int.MaxValue));
            Assert.That(stats[AbilityConstants.Strength].BaseValue, Is.EqualTo(9266));
            Assert.That(stats[AbilityConstants.Wisdom].BaseValue, Is.EqualTo(int.MinValue));
        }
    }
}