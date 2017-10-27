using CreatureGen.Abilities;
using CreatureGen.Domain.Generators.Randomizers.Abilities;
using NUnit.Framework;
using System.Collections.Generic;

namespace CreatureGen.Tests.Unit.Generators.Randomizers.Abilities
{
    [TestFixture]
    public class BaseAbilitiesRandomizerTests
    {
        private TestAbilityRandomizer randomizer;

        [SetUp]
        public void Setup()
        {
            randomizer = new TestAbilityRandomizer();
        }

        [Test]
        public void AbilitiesContainAllAbilities()
        {
            var abilities = randomizer.Randomize();

            Assert.That(abilities.Keys, Contains.Item(AbilityConstants.Strength));
            Assert.That(abilities.Keys, Contains.Item(AbilityConstants.Constitution));
            Assert.That(abilities.Keys, Contains.Item(AbilityConstants.Dexterity));
            Assert.That(abilities.Keys, Contains.Item(AbilityConstants.Intelligence));
            Assert.That(abilities.Keys, Contains.Item(AbilityConstants.Wisdom));
            Assert.That(abilities.Keys, Contains.Item(AbilityConstants.Charisma));
            Assert.That(abilities.Count, Is.EqualTo(6));
        }

        [Test]
        public void AbilitiesRolledTheSamePerStat()
        {
            randomizer.Roll = 11;

            var abilities = randomizer.Randomize();

            Assert.That(abilities[AbilityConstants.Strength].BaseValue, Is.EqualTo(randomizer.Roll));
            Assert.That(abilities[AbilityConstants.Constitution].BaseValue, Is.EqualTo(randomizer.Roll));
            Assert.That(abilities[AbilityConstants.Dexterity].BaseValue, Is.EqualTo(randomizer.Roll));
            Assert.That(abilities[AbilityConstants.Intelligence].BaseValue, Is.EqualTo(randomizer.Roll));
            Assert.That(abilities[AbilityConstants.Wisdom].BaseValue, Is.EqualTo(randomizer.Roll));
            Assert.That(abilities[AbilityConstants.Charisma].BaseValue, Is.EqualTo(randomizer.Roll));
        }

        [Test]
        public void AbilitiesAreRolledIndividually()
        {
            randomizer.Roll = 11;
            randomizer.Reroll = 12;

            var abilities = randomizer.Randomize();

            Assert.That(abilities[AbilityConstants.Strength].BaseValue, Is.EqualTo(16));
            Assert.That(abilities[AbilityConstants.Constitution].BaseValue, Is.EqualTo(13));
            Assert.That(abilities[AbilityConstants.Dexterity].BaseValue, Is.EqualTo(14));
            Assert.That(abilities[AbilityConstants.Intelligence].BaseValue, Is.EqualTo(15));
            Assert.That(abilities[AbilityConstants.Wisdom].BaseValue, Is.EqualTo(17));
            Assert.That(abilities[AbilityConstants.Charisma].BaseValue, Is.EqualTo(12));
        }

        [Test]
        public void LoopUntilAbilitiesAreAllowed()
        {
            randomizer.Roll = 11;
            randomizer.Reroll = 12;
            randomizer.AllowedOnRandomize = 10;

            var abilities = randomizer.Randomize();

            Assert.That(abilities[AbilityConstants.Strength].BaseValue, Is.EqualTo(16));
            Assert.That(abilities[AbilityConstants.Constitution].BaseValue, Is.EqualTo(13));
            Assert.That(abilities[AbilityConstants.Dexterity].BaseValue, Is.EqualTo(14));
            Assert.That(abilities[AbilityConstants.Intelligence].BaseValue, Is.EqualTo(15));
            Assert.That(abilities[AbilityConstants.Wisdom].BaseValue, Is.EqualTo(17));
            Assert.That(abilities[AbilityConstants.Charisma].BaseValue, Is.EqualTo(12));
        }

        [Test]
        public void IfAbilitiesNeverAllowed_ReturnDefaultValues()
        {
            randomizer.Roll = 11;
            randomizer.Reroll = 12;
            randomizer.AllowedOnRandomize = int.MaxValue;

            var abilities = randomizer.Randomize();

            Assert.That(abilities[AbilityConstants.Strength].BaseValue, Is.EqualTo(9266));
            Assert.That(abilities[AbilityConstants.Constitution].BaseValue, Is.EqualTo(9266));
            Assert.That(abilities[AbilityConstants.Dexterity].BaseValue, Is.EqualTo(9266));
            Assert.That(abilities[AbilityConstants.Intelligence].BaseValue, Is.EqualTo(9266));
            Assert.That(abilities[AbilityConstants.Wisdom].BaseValue, Is.EqualTo(9266));
            Assert.That(abilities[AbilityConstants.Charisma].BaseValue, Is.EqualTo(9266));
        }

        private class TestAbilityRandomizer : BaseAbilitiesRandomizer
        {
            public int Roll { get; set; }
            public int Reroll { get; set; }
            public int AllowedOnRandomize { get; set; }

            protected override int defaultValue
            {
                get
                {
                    return 9266;
                }
            }

            private int randomizeCount;

            public TestAbilityRandomizer()
                : base(new ConfigurableIterationGenerator(10))
            {
                randomizeCount = 0;
                AllowedOnRandomize = 1;
            }

            protected override int RollAbility()
            {
                if (randomizeCount + 1 < AllowedOnRandomize || Reroll == 0)
                    return Roll;

                return Reroll++;
            }

            protected override bool AbilitiesAreAllowed(IEnumerable<Ability> stats)
            {
                randomizeCount++;
                return randomizeCount >= AllowedOnRandomize;
            }

            protected override string AbilitiesInvalidMessage(IEnumerable<Ability> abilities)
            {
                return "abilities in unit test are invalid";
            }
        }
    }
}