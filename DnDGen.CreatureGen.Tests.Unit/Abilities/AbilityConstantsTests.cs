﻿using DnDGen.CreatureGen.Abilities;
using NUnit.Framework;

namespace DnDGen.CreatureGen.Tests.Unit.Abilities
{
    [TestFixture]
    public class AbilityConstantsTests
    {
        [TestCase(AbilityConstants.Strength, "Strength")]
        [TestCase(AbilityConstants.Charisma, "Charisma")]
        [TestCase(AbilityConstants.Constitution, "Constitution")]
        [TestCase(AbilityConstants.Dexterity, "Dexterity")]
        [TestCase(AbilityConstants.Intelligence, "Intelligence")]
        [TestCase(AbilityConstants.Wisdom, "Wisdom")]
        [TestCase(AbilityConstants.RandomizerRolls.Best, "1d3+15")]
        [TestCase(AbilityConstants.RandomizerRolls.BestOfFour, "4d6k3")]
        [TestCase(AbilityConstants.RandomizerRolls.Default, "1d2+9")]
        [TestCase(AbilityConstants.RandomizerRolls.Good, "1d4+11")]
        [TestCase(AbilityConstants.RandomizerRolls.OnesAsSixes, "3d6t1")]
        [TestCase(AbilityConstants.RandomizerRolls.Poor, "1d6+3")]
        [TestCase(AbilityConstants.RandomizerRolls.Raw, "3d6")]
        [TestCase(AbilityConstants.RandomizerRolls.Wild, "2d10")]
        public void AbilityConstant(string constant, string value)
        {
            Assert.That(constant, Is.EqualTo(value));
        }
    }
}