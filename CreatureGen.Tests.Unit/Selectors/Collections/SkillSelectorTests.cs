using CreatureGen.Selectors.Collections;
using CreatureGen.Tables;
using DnDGen.Core.Selectors.Collections;
using Moq;
using NUnit.Framework;

namespace CreatureGen.Tests.Unit.Selectors.Collections
{
    [TestFixture]
    public class SkillSelectorTests
    {
        private ISkillSelector skillSelector;
        private Mock<ICollectionSelector> mockInnerSelector;

        [SetUp]
        public void Setup()
        {
            mockInnerSelector = new Mock<ICollectionSelector>();
            skillSelector = new SkillSelector(mockInnerSelector.Object);
        }

        [Test]
        public void GetCollectionFromInnerCollectionSelector()
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
    }
}