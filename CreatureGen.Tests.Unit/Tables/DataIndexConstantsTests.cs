using CreatureGen.Domain.Tables;
using NUnit.Framework;

namespace CreatureGen.Tests.Unit.Tables
{
    [TestFixture]
    public class DataIndexConstantsTests
    {
        [TestCase(DataIndexConstants.CharacterClassFeatData.FeatNameIndex, 0)]
        [TestCase(DataIndexConstants.CharacterClassFeatData.MinimumLevelRequirementIndex, 1)]
        [TestCase(DataIndexConstants.CharacterClassFeatData.FocusTypeIndex, 2)]
        [TestCase(DataIndexConstants.CharacterClassFeatData.PowerIndex, 3)]
        [TestCase(DataIndexConstants.CharacterClassFeatData.FrequencyQuantityIndex, 4)]
        [TestCase(DataIndexConstants.CharacterClassFeatData.FrequencyTimePeriodIndex, 5)]
        [TestCase(DataIndexConstants.CharacterClassFeatData.MaximumLevelRequirementIndex, 6)]
        [TestCase(DataIndexConstants.CharacterClassFeatData.FrequencyQuantityStatIndex, 7)]
        [TestCase(DataIndexConstants.CharacterClassFeatData.SizeRequirementIndex, 8)]
        [TestCase(DataIndexConstants.CharacterClassFeatData.AllowFocusOfAllIndex, 9)]
        public void CharacterClassFeatDataIndex(int constant, int value)
        {
            Assert.That(constant, Is.EqualTo(value));
        }

        [TestCase(DataIndexConstants.RacialFeatData.FeatNameIndex, 0)]
        [TestCase(DataIndexConstants.RacialFeatData.SizeRequirementIndex, 1)]
        [TestCase(DataIndexConstants.RacialFeatData.MinimumHitDiceRequirementIndex, 2)]
        [TestCase(DataIndexConstants.RacialFeatData.PowerIndex, 3)]
        [TestCase(DataIndexConstants.RacialFeatData.FocusIndex, 4)]
        [TestCase(DataIndexConstants.RacialFeatData.FrequencyQuantityIndex, 5)]
        [TestCase(DataIndexConstants.RacialFeatData.FrequencyTimePeriodIndex, 6)]
        [TestCase(DataIndexConstants.RacialFeatData.MaximumHitDiceRequirementIndex, 7)]
        [TestCase(DataIndexConstants.RacialFeatData.RequiredStatIndex, 8)]
        [TestCase(DataIndexConstants.RacialFeatData.RequiredStatMinimumValueIndex, 9)]
        [TestCase(DataIndexConstants.RacialFeatData.RandomFociQuantity, 10)]
        public void RacialFeatDataIndex(int constant, int value)
        {
            Assert.That(constant, Is.EqualTo(value));
        }

        [TestCase(DataIndexConstants.AdditionalFeatData.BaseAttackRequirementIndex, 0)]
        [TestCase(DataIndexConstants.AdditionalFeatData.FocusTypeIndex, 1)]
        [TestCase(DataIndexConstants.AdditionalFeatData.PowerIndex, 2)]
        [TestCase(DataIndexConstants.AdditionalFeatData.FrequencyQuantityIndex, 3)]
        [TestCase(DataIndexConstants.AdditionalFeatData.FrequencyTimePeriodIndex, 4)]
        public void AdditionalFeatDataIndex(int constant, int value)
        {
            Assert.That(constant, Is.EqualTo(value));
        }

        [TestCase(DataIndexConstants.SkillSelectionData.BaseStatName, 0)]
        [TestCase(DataIndexConstants.SkillSelectionData.SkillName, 1)]
        [TestCase(DataIndexConstants.SkillSelectionData.RandomFociQuantity, 2)]
        [TestCase(DataIndexConstants.SkillSelectionData.Focus, 3)]
        public void SkillSelectionDataIndex(int constant, int value)
        {
            Assert.That(constant, Is.EqualTo(value));
        }
    }
}