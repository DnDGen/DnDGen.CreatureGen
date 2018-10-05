using CreatureGen.Selectors.Collections;
using CreatureGen.Selectors.Selections;
using CreatureGen.Skills;
using CreatureGen.Tables;
using DnDGen.Core.Selectors.Collections;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace CreatureGen.Tests.Unit.Selectors.Collections
{
    [TestFixture]
    public class SkillSelectorTests
    {
        private ISkillSelector skillSelector;
        private Mock<ICollectionSelector> mockInnerSelector;
        private Mock<IBonusSelector> mockBonusSelector;
        private List<BonusSelection> bonusSelections;

        [SetUp]
        public void Setup()
        {
            mockInnerSelector = new Mock<ICollectionSelector>();
            mockBonusSelector = new Mock<IBonusSelector>();
            skillSelector = new SkillSelector(mockInnerSelector.Object, mockBonusSelector.Object);

            bonusSelections = new List<BonusSelection>();

            mockBonusSelector.Setup(s => s.SelectFor(TableNameConstants.TypeAndAmount.SkillBonuses, "creature")).Returns(bonusSelections);
        }

        [Test]
        public void SelectSkillsFromInnerCollectionSelector()
        {
            var skillData = new string[4];
            skillData[DataIndexConstants.SkillSelectionData.BaseStatName] = "base stat";
            skillData[DataIndexConstants.SkillSelectionData.Focus] = "focus";
            skillData[DataIndexConstants.SkillSelectionData.RandomFociQuantity] = "9266";
            skillData[DataIndexConstants.SkillSelectionData.SkillName] = "skill name";

            mockInnerSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.SkillData, "skill")).Returns(skillData);

            var selection = skillSelector.SelectFor("skill");
            Assert.That(selection.BaseAbilityName, Is.EqualTo("base stat"));
            Assert.That(selection.SkillName, Is.EqualTo("skill name"));
            Assert.That(selection.Focus, Is.EqualTo("focus"));
            Assert.That(selection.RandomFociQuantity, Is.EqualTo(9266));
        }

        [Test]
        public void SelectNoSkillBonus()
        {
            var bonusSelections = skillSelector.SelectBonusesFor("creature");
            Assert.That(bonusSelections, Is.Empty);
        }

        [TestCase("skill", "", "", 9266)]
        [TestCase("skill", "", "", -1337)]
        [TestCase("skill", "", "condition", 90210)]
        [TestCase("skill", "", "condition", -1336)]
        [TestCase("skill", "focus", "", 42)]
        [TestCase("skill", "focus", "", -96)]
        [TestCase("skill", "focus", "condition", 600)]
        [TestCase("skill", "focus", "condition", -783)]
        public void SelectSkillBonus(string skillName, string skillFocus, string condition, int bonus)
        {
            SetUpSkillBonus(skillName, skillFocus, condition, bonus);

            var bonusSelections = skillSelector.SelectBonusesFor("creature");
            Assert.That(bonusSelections.Count, Is.EqualTo(1));

            var selection = bonusSelections.Single();
            Assert.That(selection.Bonus, Is.EqualTo(bonus));
            Assert.That(selection.Condition, Is.EqualTo(condition));

            var expected = SkillConstants.Build(skillName, skillFocus);
            Assert.That(selection.Target, Is.EqualTo(expected));
        }

        private void SetUpSkillBonus(
            string skill,
            string focus = "",
            string condition = "",
            int bonus = 1)
        {
            var bonusSelection = new BonusSelection();
            bonusSelection.Bonus = bonus;
            bonusSelection.Target = SkillConstants.Build(skill, focus);
            bonusSelection.Condition = condition;

            bonusSelections.Add(bonusSelection);
        }

        [TestCase("skill", "", "", 1337, "skill", "", "", 9266)]
        [TestCase("skill", "", "", -1336, "skill", "", "condition", 90210)]
        [TestCase("skill", "", "", 96, "skill", "focus", "", -42)]
        [TestCase("skill", "", "", -783, "skill", "focus", "condition", -600)]
        [TestCase("skill", "", "condition", 8245, "skill", "", "", 9266)]
        [TestCase("skill", "", "condition", -90210, "skill", "", "condition", 42)]
        [TestCase("skill", "", "condition", 600, "skill", "focus", "", -1337)]
        [TestCase("skill", "", "condition", -1336, "skill", "focus", "condition", -96)]
        [TestCase("skill", "", "other condition", 8245, "skill", "", "", 9266)]
        [TestCase("skill", "", "other condition", -90210, "skill", "", "condition", 42)]
        [TestCase("skill", "", "other condition", 600, "skill", "focus", "", -1337)]
        [TestCase("skill", "", "other condition", -1336, "skill", "focus", "condition", -96)]
        [TestCase("skill", "focus", "", 783, "skill", "", "", 8245)]
        [TestCase("skill", "focus", "", -9266, "skill", "", "condition", 90210)]
        [TestCase("skill", "focus", "", 42, "skill", "focus", "", -600)]
        [TestCase("skill", "focus", "", -1337, "skill", "focus", "condition", -1336)]
        [TestCase("skill", "focus", "condition", 96, "skill", "", "", 783)]
        [TestCase("skill", "focus", "condition", -8245, "skill", "", "condition", 9266)]
        [TestCase("skill", "focus", "condition", 90210, "skill", "focus", "", -42)]
        [TestCase("skill", "focus", "condition", -600, "skill", "focus", "condition", -1337)]
        [TestCase("skill", "focus", "other condition", 96, "skill", "", "", 783)]
        [TestCase("skill", "focus", "other condition", -8245, "skill", "", "condition", 9266)]
        [TestCase("skill", "focus", "other condition", 90210, "skill", "focus", "", -42)]
        [TestCase("skill", "focus", "other condition", -600, "skill", "focus", "condition", -1337)]
        [TestCase("skill", "other focus", "", 783, "skill", "", "", 8245)]
        [TestCase("skill", "other focus", "", -9266, "skill", "", "condition", 90210)]
        [TestCase("skill", "other focus", "", 42, "skill", "focus", "", -600)]
        [TestCase("skill", "other focus", "", -1337, "skill", "focus", "condition", -1336)]
        [TestCase("skill", "other focus", "condition", 96, "skill", "", "", 783)]
        [TestCase("skill", "other focus", "condition", -8245, "skill", "", "condition", 9266)]
        [TestCase("skill", "other focus", "condition", 90210, "skill", "focus", "", -42)]
        [TestCase("skill", "other focus", "condition", -600, "skill", "focus", "condition", -1337)]
        [TestCase("skill", "other focus", "other condition", 96, "skill", "", "", 783)]
        [TestCase("skill", "other focus", "other condition", -8245, "skill", "", "condition", 9266)]
        [TestCase("skill", "other focus", "other condition", 90210, "skill", "focus", "", -42)]
        [TestCase("skill", "other focus", "other condition", -600, "skill", "focus", "condition", -1337)]
        [TestCase("other skill", "", "", 1337, "skill", "", "", 9266)]
        [TestCase("other skill", "", "", -1336, "skill", "", "condition", 90210)]
        [TestCase("other skill", "", "", 96, "skill", "focus", "", -42)]
        [TestCase("other skill", "", "", -783, "skill", "focus", "condition", -600)]
        [TestCase("other skill", "", "condition", 8245, "skill", "", "", 9266)]
        [TestCase("other skill", "", "condition", -90210, "skill", "", "condition", 42)]
        [TestCase("other skill", "", "condition", 600, "skill", "focus", "", -1337)]
        [TestCase("other skill", "", "condition", -1336, "skill", "focus", "condition", -96)]
        [TestCase("other skill", "focus", "", 783, "skill", "", "", 8245)]
        [TestCase("other skill", "focus", "", -9266, "skill", "", "condition", 90210)]
        [TestCase("other skill", "focus", "", 42, "skill", "focus", "", -600)]
        [TestCase("other skill", "focus", "", -1337, "skill", "focus", "condition", -1336)]
        [TestCase("other skill", "focus", "condition", 96, "skill", "", "", 783)]
        [TestCase("other skill", "focus", "condition", -8245, "skill", "", "condition", 9266)]
        [TestCase("other skill", "focus", "condition", 90210, "skill", "focus", "", -42)]
        [TestCase("other skill", "focus", "condition", -600, "skill", "focus", "condition", -1337)]
        public void SelectSkillBonuses(string skillName1, string skillFocus1, string condition1, int bonus1, string skillName2, string skillFocus2, string condition2, int bonus2)
        {
            SetUpSkillBonus(skillName1, skillFocus1, condition1, bonus1);
            SetUpSkillBonus(skillName2, skillFocus2, condition2, bonus2);

            var bonusSelections = skillSelector.SelectBonusesFor("creature");
            Assert.That(bonusSelections.Count, Is.EqualTo(2));

            var selection = bonusSelections.First();
            Assert.That(selection.Bonus, Is.EqualTo(bonus1));
            Assert.That(selection.Condition, Is.EqualTo(condition1));

            var expected = SkillConstants.Build(skillName1, skillFocus1);
            Assert.That(selection.Target, Is.EqualTo(expected));

            selection = bonusSelections.Last();
            Assert.That(selection.Bonus, Is.EqualTo(bonus2));
            Assert.That(selection.Condition, Is.EqualTo(condition2));

            expected = SkillConstants.Build(skillName2, skillFocus2);
            Assert.That(selection.Target, Is.EqualTo(expected));
        }
    }
}