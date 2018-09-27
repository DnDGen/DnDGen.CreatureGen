using CreatureGen.Abilities;
using CreatureGen.Skills;
using NUnit.Framework;
using System.Linq;

namespace CreatureGen.Tests.Unit.Skills
{
    [TestFixture]
    public class SkillTests
    {
        private Skill skill;
        private Ability baseAbility;

        [SetUp]
        public void Setup()
        {
            baseAbility = new Ability("base ability");
            skill = new Skill("skill name", baseAbility, 90210);
        }

        [Test]
        public void SkillInitialized()
        {
            Assert.That(skill.Name, Is.EqualTo("skill name"));
            Assert.That(skill.Focus, Is.Empty);
            Assert.That(skill.BaseAbility, Is.EqualTo(baseAbility));
            Assert.That(skill.ArmorCheckPenalty, Is.Zero);
            Assert.That(skill.ClassSkill, Is.False);
            Assert.That(skill.Bonus, Is.Zero);
            Assert.That(skill.Bonuses, Is.Empty);
            Assert.That(skill.Ranks, Is.Zero);
            Assert.That(skill.CircumstantialBonus, Is.False);
            Assert.That(skill.RankCap, Is.EqualTo(90210));
            Assert.That(skill.TotalBonus, Is.Zero);
        }

        [Test]
        public void SkillInitializedWithFocus()
        {
            skill = new Skill("skill name", baseAbility, 90210, "focus");

            Assert.That(skill.Name, Is.EqualTo("skill name"));
            Assert.That(skill.Focus, Is.EqualTo("focus"));
            Assert.That(skill.BaseAbility, Is.EqualTo(baseAbility));
            Assert.That(skill.ArmorCheckPenalty, Is.Zero);
            Assert.That(skill.ClassSkill, Is.False);
            Assert.That(skill.Bonus, Is.Zero);
            Assert.That(skill.Ranks, Is.Zero);
            Assert.That(skill.CircumstantialBonus, Is.False);
            Assert.That(skill.RankCap, Is.EqualTo(90210));
            Assert.That(skill.TotalBonus, Is.Zero);
        }

        [Test]
        public void EffectiveRanksIsRanksIfClassSkill()
        {
            skill.Ranks = 5;
            skill.ClassSkill = true;

            Assert.That(skill.EffectiveRanks, Is.EqualTo(5));
        }

        [Test]
        public void EffectiveRanksIsHalfIfCrossClassSkill()
        {
            skill.Ranks = 5;
            skill.ClassSkill = false;

            Assert.That(skill.EffectiveRanks, Is.EqualTo(2.5));
        }

        [Test]
        public void RanksMaxedOutIfEqualToRankCap()
        {
            skill.Ranks = skill.RankCap;
            Assert.That(skill.RanksMaxedOut, Is.True);
        }

        [Test]
        public void RanksNotMaxedOutIfLessThanRankCap()
        {
            skill.Ranks = skill.RankCap - 1;
            Assert.That(skill.RanksMaxedOut, Is.False);
        }

        [Test]
        public void AdjustRankCap()
        {
            skill.RankCap += 600;
            Assert.That(skill.RankCap, Is.EqualTo(90810));
        }

        [Test]
        public void SetRanks()
        {
            skill.Ranks = 9266;
            Assert.That(skill.Ranks, Is.EqualTo(9266));
        }

        [Test]
        public void CannotSetRanksAboveRankCap()
        {
            skill.Ranks = skill.RankCap;
            Assert.That(() => skill.Ranks++, Throws.InvalidOperationException.With.Message.EqualTo("Ranks cannot exceed the Rank Cap"));
        }

        [Test]
        public void ClassSkillQualifiesForSkillSynergy()
        {
            skill.Ranks = 5;
            skill.ClassSkill = true;

            Assert.That(skill.QualifiesForSkillSynergy, Is.True);
        }

        [Test]
        public void ClassSkillDoesNotQualifyForSkillSynergy()
        {
            skill.Ranks = 4;
            skill.ClassSkill = true;

            Assert.That(skill.QualifiesForSkillSynergy, Is.False);
        }

        [Test]
        public void CrossClassSkillQualifiesForSkillSynergy()
        {
            skill.Ranks = 10;
            skill.ClassSkill = false;

            Assert.That(skill.QualifiesForSkillSynergy, Is.True);
        }

        [Test]
        public void CrossClassSkillDoesNotQualifyForSkillSynergy()
        {
            skill.Ranks = 9;
            skill.ClassSkill = false;

            Assert.That(skill.QualifiesForSkillSynergy, Is.False);
        }

        [Test]
        public void AddSkillBonus()
        {
            skill.AddSkillBonus(42);

            Assert.That(skill.Bonuses, Is.Not.Empty);
            Assert.That(skill.Bonuses.Count, Is.EqualTo(1));

            var bonus = skill.Bonuses.Single();
            Assert.That(bonus.Value, Is.EqualTo(42));
            Assert.That(bonus.Condition, Is.Empty);

            Assert.That(skill.CircumstantialBonus, Is.False);
        }

        [Test]
        public void AddSkillBonuses()
        {
            skill.AddSkillBonus(42);
            skill.AddSkillBonus(600);

            Assert.That(skill.Bonuses, Is.Not.Empty);
            Assert.That(skill.Bonuses.Count, Is.EqualTo(2));

            var first = skill.Bonuses.First();
            var last = skill.Bonuses.Last();

            Assert.That(first.Value, Is.EqualTo(42));
            Assert.That(first.Condition, Is.Empty);

            Assert.That(last.Value, Is.EqualTo(600));
            Assert.That(last.Condition, Is.Empty);

            Assert.That(skill.CircumstantialBonus, Is.False);
        }

        [Test]
        public void AddSkillBonusWithCondition()
        {
            skill.AddSkillBonus(42, "condition");

            Assert.That(skill.Bonuses, Is.Not.Empty);
            Assert.That(skill.Bonuses.Count, Is.EqualTo(1));

            var bonus = skill.Bonuses.Single();
            Assert.That(bonus.Value, Is.EqualTo(42));
            Assert.That(bonus.Condition, Is.EqualTo("condition"));

            Assert.That(skill.CircumstantialBonus, Is.True);
        }

        [Test]
        public void AddSkillBonusesWithCondition()
        {
            skill.AddSkillBonus(42, "condition");
            skill.AddSkillBonus(600, "other condition");

            Assert.That(skill.Bonuses, Is.Not.Empty);
            Assert.That(skill.Bonuses.Count, Is.EqualTo(2));

            var first = skill.Bonuses.First();
            var last = skill.Bonuses.Last();

            Assert.That(first.Value, Is.EqualTo(42));
            Assert.That(first.Condition, Is.EqualTo("condition"));

            Assert.That(last.Value, Is.EqualTo(600));
            Assert.That(last.Condition, Is.EqualTo("other condition"));

            Assert.That(skill.CircumstantialBonus, Is.True);
        }

        [Test]
        public void AddSkillBonusWithAndWithoutCondition()
        {
            skill.AddSkillBonus(42, "condition");
            skill.AddSkillBonus(600);

            Assert.That(skill.Bonuses, Is.Not.Empty);
            Assert.That(skill.Bonuses.Count, Is.EqualTo(2));

            var first = skill.Bonuses.First();
            var last = skill.Bonuses.Last();

            Assert.That(first.Value, Is.EqualTo(42));
            Assert.That(first.Condition, Is.EqualTo("condition"));

            Assert.That(last.Value, Is.EqualTo(600));
            Assert.That(last.Condition, Is.Empty);

            Assert.That(skill.CircumstantialBonus, Is.True);
        }

        [Test]
        public void TotalBonusForClassSkill()
        {
            skill.Ranks = 9266;
            skill.ClassSkill = true;
            skill.ArmorCheckPenalty = -600;

            skill.AddSkillBonus(42);

            Assert.That(skill.TotalBonus, Is.EqualTo(8708));
        }

        [Test]
        public void TotalBonusForCrossClassSkill()
        {
            skill.Ranks = 9266;
            skill.ClassSkill = false;
            skill.ArmorCheckPenalty = -600;
            baseAbility.BaseScore = 7;

            skill.AddSkillBonus(42);

            Assert.That(skill.TotalBonus, Is.EqualTo(4073));
        }

        [Test]
        public void TotalBonusForCrossClassSkillRoundedDown()
        {
            skill.Ranks = 9265;
            skill.ClassSkill = false;
            skill.ArmorCheckPenalty = -600;
            baseAbility.BaseScore = 7;

            skill.AddSkillBonus(42);

            Assert.That(skill.TotalBonus, Is.EqualTo(4072));
        }

        [Test]
        public void BonusIncludesBonus()
        {
            skill.AddSkillBonus(42);

            Assert.That(skill.Bonus, Is.EqualTo(42));
        }

        [Test]
        public void BonusIncludesBonuses()
        {
            skill.AddSkillBonus(42);
            skill.AddSkillBonus(600);

            Assert.That(skill.Bonus, Is.EqualTo(642));
        }

        [Test]
        public void BonusIncludesDuplicateBonuses()
        {
            skill.AddSkillBonus(42);
            skill.AddSkillBonus(42);

            Assert.That(skill.Bonus, Is.EqualTo(84));
        }

        [Test]
        public void BonusDoesNotIncludeConditionalBonus()
        {
            skill.AddSkillBonus(42, "condition");
            skill.AddSkillBonus(600);

            Assert.That(skill.Bonus, Is.EqualTo(600));
        }

        [Test]
        public void BonusDoesNotIncludeConditionalBonuses()
        {
            skill.AddSkillBonus(42, "condition");
            skill.AddSkillBonus(600, "other condition");
            skill.AddSkillBonus(1337);

            Assert.That(skill.Bonus, Is.EqualTo(1337));
        }

        [Test]
        public void AllBonusesAreConditional()
        {
            skill.AddSkillBonus(42, "condition");
            skill.AddSkillBonus(600, "other condition");

            Assert.That(skill.Bonus, Is.Zero);
        }

        [TestCase("skill", "", "skill", "", true)]
        [TestCase("skill", "focus", "skill", "focus", true)]
        [TestCase("skill", "", "skill", "focus", true)]
        [TestCase("skill", "focus", "skill", "", true)]
        [TestCase("skill", "", "other skill", "", false)]
        [TestCase("skill", "focus", "other skill", "focus", false)]
        [TestCase("skill", "focus", "skill", "other focus", false)]
        public void SkillIsEqualToOtherSkill(string skillName, string skillFocus, string otherSkillName, string otherSkillFocus, bool shouldEqual)
        {
            skill = new Skill(skillName, baseAbility, 0, skillFocus);
            var otherSkill = new Skill(otherSkillName, baseAbility, 0, otherSkillFocus);

            var isEqual = skill.IsEqualTo(otherSkill);
            Assert.That(isEqual, Is.EqualTo(shouldEqual));
        }

        [TestCase("skill", "", "skill", "", true)]
        [TestCase("skill", "focus", "skill", "focus", true)]
        [TestCase("skill", "", "skill", "focus", true)]
        [TestCase("skill", "focus", "skill", "", true)]
        [TestCase("skill", "", "other skill", "", false)]
        [TestCase("skill", "focus", "other skill", "focus", false)]
        [TestCase("skill", "focus", "skill", "other focus", false)]
        public void SkillIsEqualToString(string skillName, string skillFocus, string otherSkillName, string otherSkillFocus, bool shouldEqual)
        {
            skill = new Skill(skillName, baseAbility, 0, skillFocus);
            var otherSkill = otherSkillFocus.Any() ? $"{otherSkillName}/{otherSkillFocus}" : otherSkillName;

            var isEqual = skill.IsEqualTo(otherSkill);
            Assert.That(isEqual, Is.EqualTo(shouldEqual));
        }
    }
}