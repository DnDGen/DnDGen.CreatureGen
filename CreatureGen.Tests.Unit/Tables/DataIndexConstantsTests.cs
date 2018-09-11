using CreatureGen.Tables;
using NUnit.Framework;
using System.Linq;

namespace CreatureGen.Tests.Unit.Tables
{
    [TestFixture]
    public class DataIndexConstantsTests
    {
        [TestCase(DataIndexConstants.SpecialQualityData.FeatNameIndex, 0)]
        [TestCase(DataIndexConstants.SpecialQualityData.PowerIndex, 1)]
        [TestCase(DataIndexConstants.SpecialQualityData.FocusIndex, 2)]
        [TestCase(DataIndexConstants.SpecialQualityData.FrequencyQuantityIndex, 3)]
        [TestCase(DataIndexConstants.SpecialQualityData.FrequencyTimePeriodIndex, 4)]
        [TestCase(DataIndexConstants.SpecialQualityData.RandomFociQuantityIndex, 5)]
        [TestCase(DataIndexConstants.SpecialQualityData.RequiresEquipmentIndex, 6)]
        public void SpecialQualityDataIndex(int constant, int value)
        {
            Assert.That(constant, Is.EqualTo(value));
        }

        [TestCase(DataIndexConstants.SpecialQualityData.FeatNameIndex)]
        [TestCase(DataIndexConstants.SpecialQualityData.PowerIndex)]
        [TestCase(DataIndexConstants.SpecialQualityData.FocusIndex)]
        [TestCase(DataIndexConstants.SpecialQualityData.FrequencyQuantityIndex)]
        [TestCase(DataIndexConstants.SpecialQualityData.FrequencyTimePeriodIndex)]
        [TestCase(DataIndexConstants.SpecialQualityData.RandomFociQuantityIndex)]
        [TestCase(DataIndexConstants.SpecialQualityData.RequiresEquipmentIndex)]
        public void SpecialQualityDataIndicesInitialized(int index)
        {
            var data = DataIndexConstants.SpecialQualityData.InitializeData();
            Assert.That(data.Count, Is.GreaterThan(index));
            Assert.That(data, Is.All.Empty);
        }

        [TestCase(DataIndexConstants.FeatData.BaseAttackRequirementIndex, 0)]
        [TestCase(DataIndexConstants.FeatData.FocusTypeIndex, 1)]
        [TestCase(DataIndexConstants.FeatData.PowerIndex, 2)]
        [TestCase(DataIndexConstants.FeatData.FrequencyQuantityIndex, 3)]
        [TestCase(DataIndexConstants.FeatData.FrequencyTimePeriodIndex, 4)]
        [TestCase(DataIndexConstants.FeatData.MinimumCasterLevelIndex, 5)]
        [TestCase(DataIndexConstants.FeatData.RequiresSpecialAttackIndex, 6)]
        [TestCase(DataIndexConstants.FeatData.RequiresSpellLikeAbility, 7)]
        [TestCase(DataIndexConstants.FeatData.RequiresNaturalArmor, 8)]
        [TestCase(DataIndexConstants.FeatData.RequiredNaturalWeaponQuantityIndex, 9)]
        [TestCase(DataIndexConstants.FeatData.RequiredHandQuantityIndex, 10)]
        public void FeatDataIndex(int constant, int value)
        {
            Assert.That(constant, Is.EqualTo(value));
        }

        [TestCase(DataIndexConstants.FeatData.BaseAttackRequirementIndex)]
        [TestCase(DataIndexConstants.FeatData.FocusTypeIndex)]
        [TestCase(DataIndexConstants.FeatData.PowerIndex)]
        [TestCase(DataIndexConstants.FeatData.FrequencyQuantityIndex)]
        [TestCase(DataIndexConstants.FeatData.FrequencyTimePeriodIndex)]
        [TestCase(DataIndexConstants.FeatData.MinimumCasterLevelIndex)]
        [TestCase(DataIndexConstants.FeatData.RequiresSpecialAttackIndex)]
        [TestCase(DataIndexConstants.FeatData.RequiresSpellLikeAbility)]
        [TestCase(DataIndexConstants.FeatData.RequiresNaturalArmor)]
        [TestCase(DataIndexConstants.FeatData.RequiredNaturalWeaponQuantityIndex)]
        [TestCase(DataIndexConstants.FeatData.RequiredHandQuantityIndex)]
        public void FeatDataIndicesInitialized(int index)
        {
            var data = DataIndexConstants.FeatData.InitializeData();
            Assert.That(data.Count, Is.GreaterThan(index));
            Assert.That(data, Is.All.Empty);
        }

        [TestCase(DataIndexConstants.SkillSynergyFeatData.FeatNameIndex, 0)]
        [TestCase(DataIndexConstants.SkillSynergyFeatData.FocusTypeIndex, 1)]
        [TestCase(DataIndexConstants.SkillSynergyFeatData.PowerIndex, 2)]
        public void SkillSynergyFeatDataIndex(int constant, int value)
        {
            Assert.That(constant, Is.EqualTo(value));
        }

        [TestCase(DataIndexConstants.SkillSynergyFeatData.FeatNameIndex)]
        [TestCase(DataIndexConstants.SkillSynergyFeatData.FocusTypeIndex)]
        [TestCase(DataIndexConstants.SkillSynergyFeatData.PowerIndex)]
        public void SkillSynergyFeatDataIndicesInitialized(int index)
        {
            var data = DataIndexConstants.SkillSynergyFeatData.InitializeData();
            Assert.That(data.Count, Is.GreaterThan(index));
            Assert.That(data, Is.All.Empty);
        }

        [TestCase("source skill", null, "target skill", null, "source skill:target skill")]
        [TestCase("source skill", "source focus", "target skill", null, "source skill/source focus:target skill")]
        [TestCase("source skill", null, "target skill", "target focus", "source skill:target skill/target focus")]
        [TestCase("source skill", "source focus", "target skill", "target focus", "source skill/source focus:target skill/target focus")]
        public void SkillSynergyKey(string sourceSkill, string sourceFocus, string targetSkill, string targetFocus, string expectedKey)
        {
            var key = DataIndexConstants.SkillSynergyFeatData.BuildDataKey(sourceSkill, sourceFocus, targetSkill, targetFocus);
            Assert.That(key, Is.EqualTo(expectedKey));
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
        [TestCase(DataIndexConstants.CreatureData.CasterLevel, 6)]
        [TestCase(DataIndexConstants.CreatureData.NaturalArmor, 7)]
        [TestCase(DataIndexConstants.CreatureData.NumberOfHands, 8)]
        public void CreatureDataIndex(int constant, int value)
        {
            Assert.That(constant, Is.EqualTo(value));
        }

        [TestCase(DataIndexConstants.CreatureData.ChallengeRating)]
        [TestCase(DataIndexConstants.CreatureData.LevelAdjustment)]
        [TestCase(DataIndexConstants.CreatureData.Reach)]
        [TestCase(DataIndexConstants.CreatureData.Size)]
        [TestCase(DataIndexConstants.CreatureData.Space)]
        [TestCase(DataIndexConstants.CreatureData.CanUseEquipment)]
        [TestCase(DataIndexConstants.CreatureData.CasterLevel)]
        [TestCase(DataIndexConstants.CreatureData.NaturalArmor)]
        [TestCase(DataIndexConstants.CreatureData.NumberOfHands)]
        public void CreatureDataIndicesInitialized(int index)
        {
            var data = DataIndexConstants.CreatureData.InitializeData();
            Assert.That(data.Count, Is.GreaterThan(index));
            Assert.That(data, Is.All.Empty);
        }

        [TestCase(DataIndexConstants.AdvancementSelectionData.Size, 0)]
        [TestCase(DataIndexConstants.AdvancementSelectionData.Space, 1)]
        [TestCase(DataIndexConstants.AdvancementSelectionData.Reach, 2)]
        public void AdvancementSelectionDataIndex(int constant, int value)
        {
            Assert.That(constant, Is.EqualTo(value));
        }
    }
}