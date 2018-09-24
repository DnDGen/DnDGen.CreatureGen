using CreatureGen.Abilities;
using CreatureGen.Selectors.Selections;
using CreatureGen.Skills;
using NUnit.Framework;
using System.Collections.Generic;

namespace CreatureGen.Tests.Unit.Selectors.Selections
{
    [TestFixture]
    public class RequiredSkillSelectionTests
    {
        private RequiredSkillSelection requiredSkillSelection;
        private List<Skill> otherSkills;
        private Ability baseStat;

        [SetUp]
        public void Setup()
        {
            requiredSkillSelection = new RequiredSkillSelection();
            otherSkills = new List<Skill>();
            baseStat = new Ability("stat name");
        }

        [Test]
        public void RequiredSkillInitialized()
        {
            Assert.That(requiredSkillSelection.Skill, Is.Empty);
            Assert.That(requiredSkillSelection.Focus, Is.Empty);
            Assert.That(requiredSkillSelection.Ranks, Is.EqualTo(0));
        }

        private Skill CreateClassSkill(string skillName, string focus = "")
        {
            var skill = new Skill(skillName, baseStat, int.MaxValue, focus);
            skill.ClassSkill = true;

            return skill;
        }

        [TestCase("skill 1", null, "skill 2", null, "skill 1", null, true)]
        [TestCase("skill 1", null, "skill 2", null, "skill 1", "focus 1", false)]
        [TestCase("skill 1", null, "skill 2", null, "skill 1", "focus 2", false)]
        [TestCase("skill 1", null, "skill 2", "focus 1", "skill 1", null, true)]
        [TestCase("skill 1", null, "skill 2", "focus 1", "skill 1", "focus 1", false)]
        [TestCase("skill 1", null, "skill 2", "focus 1", "skill 1", "focus 2", false)]
        [TestCase("skill 1", null, "skill 2", "focus 2", "skill 1", null, true)]
        [TestCase("skill 1", null, "skill 2", "focus 2", "skill 1", "focus 1", false)]
        [TestCase("skill 1", null, "skill 2", "focus 2", "skill 1", "focus 2", false)]
        [TestCase("skill 1", null, "skill 2", null, "skill 2", null, true)]
        [TestCase("skill 1", null, "skill 2", null, "skill 2", "focus 1", false)]
        [TestCase("skill 1", null, "skill 2", null, "skill 2", "focus 2", false)]
        [TestCase("skill 1", null, "skill 2", "focus 1", "skill 2", null, true)]
        [TestCase("skill 1", null, "skill 2", "focus 1", "skill 2", "focus 1", true)]
        [TestCase("skill 1", null, "skill 2", "focus 1", "skill 2", "focus 2", false)]
        [TestCase("skill 1", null, "skill 2", "focus 2", "skill 2", null, true)]
        [TestCase("skill 1", null, "skill 2", "focus 2", "skill 2", "focus 1", false)]
        [TestCase("skill 1", null, "skill 2", "focus 2", "skill 2", "focus 2", true)]
        [TestCase("skill 1", null, "skill 2", null, "skill 3", null, false)]
        [TestCase("skill 1", null, "skill 2", null, "skill 3", "focus 1", false)]
        [TestCase("skill 1", null, "skill 2", null, "skill 3", "focus 2", false)]
        [TestCase("skill 1", null, "skill 2", "focus 1", "skill 3", null, false)]
        [TestCase("skill 1", null, "skill 2", "focus 1", "skill 3", "focus 1", false)]
        [TestCase("skill 1", null, "skill 2", "focus 1", "skill 3", "focus 2", false)]
        [TestCase("skill 1", null, "skill 2", "focus 2", "skill 3", null, false)]
        [TestCase("skill 1", null, "skill 2", "focus 2", "skill 3", "focus 1", false)]
        [TestCase("skill 1", null, "skill 2", "focus 2", "skill 3", "focus 2", false)]
        [TestCase("skill 1", "focus 1", "skill 2", null, "skill 1", null, true)]
        [TestCase("skill 1", "focus 1", "skill 2", null, "skill 1", "focus 1", true)]
        [TestCase("skill 1", "focus 1", "skill 2", null, "skill 1", "focus 2", false)]
        [TestCase("skill 1", "focus 1", "skill 2", "focus 1", "skill 1", null, true)]
        [TestCase("skill 1", "focus 1", "skill 2", "focus 1", "skill 1", "focus 1", true)]
        [TestCase("skill 1", "focus 1", "skill 2", "focus 1", "skill 1", "focus 2", false)]
        [TestCase("skill 1", "focus 1", "skill 2", "focus 2", "skill 1", null, true)]
        [TestCase("skill 1", "focus 1", "skill 2", "focus 2", "skill 1", "focus 1", true)]
        [TestCase("skill 1", "focus 1", "skill 2", "focus 2", "skill 1", "focus 2", false)]
        [TestCase("skill 1", "focus 1", "skill 2", null, "skill 2", null, true)]
        [TestCase("skill 1", "focus 1", "skill 2", null, "skill 2", "focus 1", false)]
        [TestCase("skill 1", "focus 1", "skill 2", null, "skill 2", "focus 2", false)]
        [TestCase("skill 1", "focus 1", "skill 2", "focus 1", "skill 2", null, true)]
        [TestCase("skill 1", "focus 1", "skill 2", "focus 1", "skill 2", "focus 1", true)]
        [TestCase("skill 1", "focus 1", "skill 2", "focus 1", "skill 2", "focus 2", false)]
        [TestCase("skill 1", "focus 1", "skill 2", "focus 2", "skill 2", null, true)]
        [TestCase("skill 1", "focus 1", "skill 2", "focus 2", "skill 2", "focus 1", false)]
        [TestCase("skill 1", "focus 1", "skill 2", "focus 2", "skill 2", "focus 2", true)]
        [TestCase("skill 1", "focus 1", "skill 2", null, "skill 3", null, false)]
        [TestCase("skill 1", "focus 1", "skill 2", null, "skill 3", "focus 1", false)]
        [TestCase("skill 1", "focus 1", "skill 2", null, "skill 3", "focus 2", false)]
        [TestCase("skill 1", "focus 1", "skill 2", "focus 1", "skill 3", null, false)]
        [TestCase("skill 1", "focus 1", "skill 2", "focus 1", "skill 3", "focus 1", false)]
        [TestCase("skill 1", "focus 1", "skill 2", "focus 1", "skill 3", "focus 2", false)]
        [TestCase("skill 1", "focus 1", "skill 2", "focus 2", "skill 3", null, false)]
        [TestCase("skill 1", "focus 1", "skill 2", "focus 2", "skill 3", "focus 1", false)]
        [TestCase("skill 1", "focus 1", "skill 2", "focus 2", "skill 3", "focus 2", false)]
        [TestCase("skill 1", "focus 2", "skill 2", null, "skill 1", null, true)]
        [TestCase("skill 1", "focus 2", "skill 2", null, "skill 1", "focus 1", false)]
        [TestCase("skill 1", "focus 2", "skill 2", null, "skill 1", "focus 2", true)]
        [TestCase("skill 1", "focus 2", "skill 2", "focus 1", "skill 1", null, true)]
        [TestCase("skill 1", "focus 2", "skill 2", "focus 1", "skill 1", "focus 1", false)]
        [TestCase("skill 1", "focus 2", "skill 2", "focus 1", "skill 1", "focus 2", true)]
        [TestCase("skill 1", "focus 2", "skill 2", "focus 2", "skill 1", null, true)]
        [TestCase("skill 1", "focus 2", "skill 2", "focus 2", "skill 1", "focus 1", false)]
        [TestCase("skill 1", "focus 2", "skill 2", "focus 2", "skill 1", "focus 2", true)]
        [TestCase("skill 1", "focus 2", "skill 2", null, "skill 2", null, true)]
        [TestCase("skill 1", "focus 2", "skill 2", null, "skill 2", "focus 1", false)]
        [TestCase("skill 1", "focus 2", "skill 2", null, "skill 2", "focus 2", false)]
        [TestCase("skill 1", "focus 2", "skill 2", "focus 1", "skill 2", null, true)]
        [TestCase("skill 1", "focus 2", "skill 2", "focus 1", "skill 2", "focus 1", true)]
        [TestCase("skill 1", "focus 2", "skill 2", "focus 1", "skill 2", "focus 2", false)]
        [TestCase("skill 1", "focus 2", "skill 2", "focus 2", "skill 2", null, true)]
        [TestCase("skill 1", "focus 2", "skill 2", "focus 2", "skill 2", "focus 1", false)]
        [TestCase("skill 1", "focus 2", "skill 2", "focus 2", "skill 2", "focus 2", true)]
        [TestCase("skill 1", "focus 2", "skill 2", null, "skill 3", null, false)]
        [TestCase("skill 1", "focus 2", "skill 2", null, "skill 3", "focus 1", false)]
        [TestCase("skill 1", "focus 2", "skill 2", null, "skill 3", "focus 2", false)]
        [TestCase("skill 1", "focus 2", "skill 2", "focus 1", "skill 3", null, false)]
        [TestCase("skill 1", "focus 2", "skill 2", "focus 1", "skill 3", "focus 1", false)]
        [TestCase("skill 1", "focus 2", "skill 2", "focus 1", "skill 3", "focus 2", false)]
        [TestCase("skill 1", "focus 2", "skill 2", "focus 2", "skill 3", null, false)]
        [TestCase("skill 1", "focus 2", "skill 2", "focus 2", "skill 3", "focus 1", false)]
        [TestCase("skill 1", "focus 2", "skill 2", "focus 2", "skill 3", "focus 2", false)]
        [TestCase("skill 1", "focus 1", "skill 1", "focus 2", "skill 1", null, true)]
        [TestCase("skill 1", "focus 2", "skill 1", "focus 1", "skill 1", null, true)]
        [TestCase("skill 1", "focus 1", "skill 1", "focus 2", "skill 1", "focus 1", true)]
        [TestCase("skill 1", "focus 1", "skill 1", "focus 2", "skill 1", "focus 2", true)]
        [TestCase("skill 1", "focus 2", "skill 1", "focus 1", "skill 1", "focus 1", true)]
        [TestCase("skill 1", "focus 2", "skill 1", "focus 1", "skill 1", "focus 2", true)]
        [TestCase("skill 1", "focus 1", "skill 1", "focus 2", "skill 2", null, false)]
        [TestCase("skill 1", "focus 1", "skill 1", "focus 2", "skill 2", "focus 1", false)]
        [TestCase("skill 1", "focus 1", "skill 1", "focus 2", "skill 2", "focus 2", false)]
        [TestCase("skill 1", "focus 2", "skill 1", "focus 1", "skill 2", null, false)]
        [TestCase("skill 1", "focus 2", "skill 1", "focus 1", "skill 2", "focus 1", false)]
        [TestCase("skill 1", "focus 2", "skill 1", "focus 1", "skill 2", "focus 2", false)]
        public void RequirementMetIfOtherSkillMatches(string skill, string focus, string otherSkill, string otherFocus, string requiredSkill, string requiredFocus, bool isMet)
        {
            otherSkills.Add(CreateClassSkill(skill, focus));
            otherSkills.Add(CreateClassSkill(otherSkill, otherFocus));

            requiredSkillSelection.Skill = requiredSkill;
            requiredSkillSelection.Focus = requiredFocus;

            var met = requiredSkillSelection.RequirementMet(otherSkills);
            Assert.That(met, Is.EqualTo(isMet));
        }

        [Test]
        public void RequirementMetIfSufficientRanks()
        {
            otherSkills.Add(CreateClassSkill("skill 1"));
            otherSkills.Add(CreateClassSkill("skill 2"));
            otherSkills[1].Ranks = 1;

            requiredSkillSelection.Skill = "skill 2";
            requiredSkillSelection.Ranks = 1;

            var met = requiredSkillSelection.RequirementMet(otherSkills);
            Assert.That(met, Is.True);
        }

        [Test]
        public void RequirementMetIfAnyHaveSufficientRanks()
        {
            otherSkills.Add(CreateClassSkill("skill 1", "focus 1"));
            otherSkills.Add(CreateClassSkill("skill 1", "focus 2"));
            otherSkills[1].Ranks = 1;

            requiredSkillSelection.Skill = "skill 1";
            requiredSkillSelection.Ranks = 1;

            var met = requiredSkillSelection.RequirementMet(otherSkills);
            Assert.That(met, Is.True);
        }

        [Test]
        public void RequirementNotMetIfInsufficientRanks()
        {
            otherSkills.Add(CreateClassSkill("skill 1"));
            otherSkills.Add(CreateClassSkill("skill 2"));
            otherSkills[0].Ranks = 2;
            otherSkills[1].Ranks = 1;

            requiredSkillSelection.Skill = "skill 2";
            requiredSkillSelection.Ranks = 2;

            var met = requiredSkillSelection.RequirementMet(otherSkills);
            Assert.That(met, Is.False);
        }

        [Test]
        public void RequirementMetIfSufficientRanksWithFocus()
        {
            otherSkills.Add(CreateClassSkill("skill 1"));
            otherSkills.Add(CreateClassSkill("skill 2", "focus"));
            otherSkills[1].Ranks = 1;

            requiredSkillSelection.Skill = "skill 2";
            requiredSkillSelection.Focus = "focus";
            requiredSkillSelection.Ranks = 1;

            var met = requiredSkillSelection.RequirementMet(otherSkills);
            Assert.That(met, Is.True);
        }

        [Test]
        public void RequirementNotMetIfInsufficientRanksWithFocus()
        {
            otherSkills.Add(CreateClassSkill("skill 1", "focus"));
            otherSkills.Add(CreateClassSkill("skill 2", "focus"));
            otherSkills[0].Ranks = 2;
            otherSkills[1].Ranks = 1;

            requiredSkillSelection.Skill = "skill 2";
            requiredSkillSelection.Focus = "focus";
            requiredSkillSelection.Ranks = 2;

            var met = requiredSkillSelection.RequirementMet(otherSkills);
            Assert.That(met, Is.False);
        }

        [Test]
        public void RequirementMetIfSufficientRanksAsCrossClass()
        {
            otherSkills.Add(CreateClassSkill("skill 1"));
            otherSkills.Add(CreateClassSkill("skill 2"));
            otherSkills[1].Ranks = 2;
            otherSkills[1].ClassSkill = false;

            requiredSkillSelection.Skill = "skill 2";
            requiredSkillSelection.Ranks = 1;

            var met = requiredSkillSelection.RequirementMet(otherSkills);
            Assert.That(met, Is.True);
        }

        [Test]
        public void RequirementNotMetIfInsufficientRanksAsCrossClass()
        {
            otherSkills.Add(CreateClassSkill("skill 1"));
            otherSkills.Add(CreateClassSkill("skill 2"));
            otherSkills[0].Ranks = 2;
            otherSkills[1].Ranks = 1;
            otherSkills[1].ClassSkill = false;

            requiredSkillSelection.Skill = "skill 2";
            requiredSkillSelection.Ranks = 1;

            var met = requiredSkillSelection.RequirementMet(otherSkills);
            Assert.That(met, Is.False);
        }

        [Test]
        public void RequirementMetIf0RanksRequired()
        {
            otherSkills.Add(CreateClassSkill("skill 1"));
            otherSkills.Add(CreateClassSkill("skill 2"));
            otherSkills[1].Ranks = 0;

            requiredSkillSelection.Skill = "skill 2";
            requiredSkillSelection.Ranks = 0;

            var met = requiredSkillSelection.RequirementMet(otherSkills);
            Assert.That(met, Is.True);
        }
    }
}
