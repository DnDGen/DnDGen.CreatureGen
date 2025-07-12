using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Selectors.Selections;
using DnDGen.CreatureGen.Skills;
using DnDGen.CreatureGen.Tables;
using NUnit.Framework;

namespace DnDGen.CreatureGen.Tests.Unit.Selectors.Selections
{
    [TestFixture]
    public class SkillDataSelectionTests
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

        [Test]
        public void SectionCountIs4()
        {
            Assert.That(selection.SectionCount, Is.EqualTo(4));
        }

        [Test]
        public void Map_FromString_ReturnsSelection()
        {
            var data = new string[selection.SectionCount];
            data[DataIndexConstants.SkillSelectionData.SkillNameIndex] = "my skill";
            data[DataIndexConstants.SkillSelectionData.RandomFociQuantityIndex] = "9266";
            data[DataIndexConstants.SkillSelectionData.FocusIndex] = "my focus";
            data[DataIndexConstants.SkillSelectionData.BaseAbilityNameIndex] = "my ability";

            var newSelection = SkillDataSelection.Map(data);
            Assert.That(newSelection, Is.Not.Null);
            Assert.That(newSelection.SkillName, Is.EqualTo("my skill"));
            Assert.That(newSelection.Focus, Is.EqualTo("my focus"));
            Assert.That(newSelection.RandomFociQuantity, Is.EqualTo(9266));
            Assert.That(newSelection.BaseAbilityName, Is.EqualTo("my ability"));
        }

        [Test]
        public void Map_FromSelection_ReturnsString()
        {
            var selection = new SkillDataSelection
            {
                SkillName = "my skill",
                Focus = "my focus",
                RandomFociQuantity = 9266,
                BaseAbilityName = "my ability",
            };

            var rawData = SkillDataSelection.Map(selection);
            Assert.That(rawData.Length, Is.EqualTo(selection.SectionCount));
            Assert.That(rawData[DataIndexConstants.SkillSelectionData.SkillNameIndex], Is.EqualTo("my skill"));
            Assert.That(rawData[DataIndexConstants.SkillSelectionData.RandomFociQuantityIndex], Is.EqualTo("9266"));
            Assert.That(rawData[DataIndexConstants.SkillSelectionData.FocusIndex], Is.EqualTo("my focus"));
            Assert.That(rawData[DataIndexConstants.SkillSelectionData.BaseAbilityNameIndex], Is.EqualTo("my ability"));
        }

        [Test]
        public void MapTo_ReturnsSelection()
        {
            var data = new string[selection.SectionCount];
            data[DataIndexConstants.SkillSelectionData.SkillNameIndex] = "my skill";
            data[DataIndexConstants.SkillSelectionData.RandomFociQuantityIndex] = "9266";
            data[DataIndexConstants.SkillSelectionData.FocusIndex] = "my focus";
            data[DataIndexConstants.SkillSelectionData.BaseAbilityNameIndex] = "my ability";

            var newSelection = selection.MapTo(data);
            Assert.That(newSelection, Is.Not.Null);
            Assert.That(newSelection.SkillName, Is.EqualTo("my skill"));
            Assert.That(newSelection.Focus, Is.EqualTo("my focus"));
            Assert.That(newSelection.RandomFociQuantity, Is.EqualTo(9266));
            Assert.That(newSelection.BaseAbilityName, Is.EqualTo("my ability"));
        }

        [Test]
        public void MapFrom_ReturnsString()
        {
            var selection = new SkillDataSelection
            {
                SkillName = "my skill",
                Focus = "my focus",
                RandomFociQuantity = 9266,
                BaseAbilityName = "my ability",
            };

            var rawData = selection.MapFrom(selection);
            Assert.That(rawData.Length, Is.EqualTo(selection.SectionCount));
            Assert.That(rawData[DataIndexConstants.SkillSelectionData.SkillNameIndex], Is.EqualTo("my skill"));
            Assert.That(rawData[DataIndexConstants.SkillSelectionData.RandomFociQuantityIndex], Is.EqualTo("9266"));
            Assert.That(rawData[DataIndexConstants.SkillSelectionData.FocusIndex], Is.EqualTo("my focus"));
            Assert.That(rawData[DataIndexConstants.SkillSelectionData.BaseAbilityNameIndex], Is.EqualTo("my ability"));
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