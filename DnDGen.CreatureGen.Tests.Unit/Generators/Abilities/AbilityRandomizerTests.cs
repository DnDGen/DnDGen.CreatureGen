using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Generators.Abilities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace DnDGen.CreatureGen.Tests.Unit.Generators.Abilities
{
    [TestFixture]
    public class AbilityRandomizerTests
    {
        [Test]
        public void AbilityRandomizer_IsInitialized()
        {
            var randomizer = new AbilityRandomizer();
            Assert.That(randomizer.Roll, Is.EqualTo(AbilityConstants.RandomizerRolls.Default));
            Assert.That(randomizer.PriorityAbility, Is.Null);
            Assert.That(randomizer.AbilityAdvancements, Is.Not.Null.And.Empty);
            Assert.That(randomizer.SetRolls, Is.Not.Null.And.Empty);
        }
    }
}
