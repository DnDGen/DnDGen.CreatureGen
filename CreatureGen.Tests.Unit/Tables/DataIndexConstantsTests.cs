using CreatureGen.Tables;
using NUnit.Framework;

namespace CreatureGen.Tests.Unit.Tables
{
    [TestFixture]
    public class DataIndexConstantsTests
    {
        [TestCase(DataIndexConstants.SpecialQualityData.FeatNameIndex, 0)]
        [TestCase(DataIndexConstants.SpecialQualityData.SizeRequirementIndex, 1)]
        [TestCase(DataIndexConstants.SpecialQualityData.MinimumHitDiceRequirementIndex, 2)]
        [TestCase(DataIndexConstants.SpecialQualityData.PowerIndex, 3)]
        [TestCase(DataIndexConstants.SpecialQualityData.FocusIndex, 4)]
        [TestCase(DataIndexConstants.SpecialQualityData.FrequencyQuantityIndex, 5)]
        [TestCase(DataIndexConstants.SpecialQualityData.FrequencyTimePeriodIndex, 6)]
        [TestCase(DataIndexConstants.SpecialQualityData.MaximumHitDiceRequirementIndex, 7)]
        [TestCase(DataIndexConstants.SpecialQualityData.RandomFociQuantity, 8)]
        public void SpecialQualityDataIndex(int constant, int value)
        {
            Assert.That(constant, Is.EqualTo(value));
        }

        [TestCase(DataIndexConstants.FeatData.BaseAttackRequirementIndex, 0)]
        [TestCase(DataIndexConstants.FeatData.FocusTypeIndex, 1)]
        [TestCase(DataIndexConstants.FeatData.PowerIndex, 2)]
        [TestCase(DataIndexConstants.FeatData.FrequencyQuantityIndex, 3)]
        [TestCase(DataIndexConstants.FeatData.FrequencyTimePeriodIndex, 4)]
        public void FeatDataIndex(int constant, int value)
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

        [TestCase(DataIndexConstants.CreatureData.ChallengeRating, 0)]
        [TestCase(DataIndexConstants.CreatureData.LevelAdjustment, 1)]
        [TestCase(DataIndexConstants.CreatureData.Reach, 2)]
        [TestCase(DataIndexConstants.CreatureData.Size, 3)]
        [TestCase(DataIndexConstants.CreatureData.Space, 4)]
        [TestCase(DataIndexConstants.CreatureData.CanUseEquipment, 5)]
        public void CreatureDataIndex(int constant, int value)
        {
            Assert.That(constant, Is.EqualTo(value));
        }
    }
}