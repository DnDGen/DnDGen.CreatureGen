﻿using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Selectors.Selections;
using DnDGen.CreatureGen.Skills;
using NUnit.Framework;

namespace DnDGen.CreatureGen.Tests.Unit.Selectors.Selections
{
    [TestFixture]
    public class SkillSelectionTests
    {
        private SkillDataSelection selection;

        [SetUp]
        public void Setup()
        {
            selection = new SkillDataSelection();
        }

        [Test]
        public void SkillSelectionInitialized()
        {
            Assert.That(selection.BaseAbilityName, Is.Empty);
            Assert.That(selection.SkillName, Is.Empty);
            Assert.That(selection.RandomFociQuantity, Is.Zero);
            Assert.That(selection.Focus, Is.Empty);
        }

        [TestCase("skill", "", "skill", "", true)]
        [TestCase("skill", "focus", "skill", "focus", true)]
        [TestCase("skill", "", "skill", "focus", false)]
        [TestCase("skill", "focus", "skill", "", false)]
        [TestCase("skill", "", "other skill", "", false)]
        [TestCase("skill", "focus", "other skill", "focus", false)]
        [TestCase("skill", "focus", "skill", "other focus", false)]
        public void SkillSelectionEqualsSkill(string selectionName, string selectionFocus, string skillName, string skillFocus, bool shouldEqual)
        {
            selection.Focus = selectionFocus;
            selection.SkillName = selectionName;

            var stat = new Ability("stat name");
            var skill = new Skill(skillName, stat, 0, skillFocus);

            var isEqual = selection.IsEqualTo(skill);
            Assert.That(isEqual, Is.EqualTo(shouldEqual));
        }

        [Test]
        public void IfRandomFociQuantityIsPositive_ThrowExceptionWhenTestingEqualToSkill()
        {
            selection.RandomFociQuantity = 1;
            selection.SkillName = "skill";

            var stat = new Ability("stat name");
            var skill = new Skill("skill", stat, 0);

            Assert.That(() => selection.IsEqualTo(skill), Throws.InvalidOperationException.With.Message.EqualTo("Cannot test equality of a skill selection while random foci quantity is positive"));
        }

        [TestCase("skill", "", "skill", "", true)]
        [TestCase("skill", "focus", "skill", "focus", true)]
        [TestCase("skill", "", "skill", "focus", false)]
        [TestCase("skill", "focus", "skill", "", false)]
        [TestCase("skill", "", "other skill", "", false)]
        [TestCase("skill", "focus", "other skill", "focus", false)]
        [TestCase("skill", "focus", "skill", "other focus", false)]
        public void SkillSelectionEqualsSkillSelection(string selectionName, string selectionFocus, string skillName, string skillFocus, bool shouldEqual)
        {
            selection.Focus = selectionFocus;
            selection.SkillName = selectionName;

            var otherSelection = new SkillDataSelection();
            otherSelection.SkillName = skillName;
            otherSelection.Focus = skillFocus;

            var isEqual = selection.IsEqualTo(otherSelection);
            Assert.That(isEqual, Is.EqualTo(shouldEqual));
        }

        [TestCase(1, 0)]
        [TestCase(0, 1)]
        [TestCase(1, 1)]
        public void IfRandomFociQuantityIsPositive_ThrowExceptionWhenTestingEqualToSkillSelection(int selectionQuantity, int otherSelectionQuantity)
        {
            selection.SkillName = "skill";
            selection.RandomFociQuantity = selectionQuantity;

            var otherSelection = new SkillDataSelection();
            otherSelection.SkillName = "skill";
            otherSelection.RandomFociQuantity = otherSelectionQuantity;

            Assert.That(() => selection.IsEqualTo(otherSelection), Throws.InvalidOperationException.With.Message.EqualTo("Cannot test equality of a skill selection while random foci quantity is positive"));
        }
    }
}