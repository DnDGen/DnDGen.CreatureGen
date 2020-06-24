using DnDGen.CreatureGen.Tables;
using NUnit.Framework;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Unit.Tables
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
        [TestCase(DataIndexConstants.SpecialQualityData.SaveAbilityIndex, 7)]
        [TestCase(DataIndexConstants.SpecialQualityData.SaveIndex, 8)]
        [TestCase(DataIndexConstants.SpecialQualityData.SaveBaseValueIndex, 9)]
        [TestCase(DataIndexConstants.SpecialQualityData.MinHitDiceIndex, 10)]
        [TestCase(DataIndexConstants.SpecialQualityData.MaxHitDiceIndex, 11)]
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
        [TestCase(DataIndexConstants.SpecialQualityData.SaveAbilityIndex)]
        [TestCase(DataIndexConstants.SpecialQualityData.SaveIndex)]
        [TestCase(DataIndexConstants.SpecialQualityData.SaveBaseValueIndex)]
        [TestCase(DataIndexConstants.SpecialQualityData.MinHitDiceIndex)]
        [TestCase(DataIndexConstants.SpecialQualityData.MaxHitDiceIndex)]
        public void SpecialQualityDataIndicesInitialized(int index)
        {
            var data = DataIndexConstants.SpecialQualityData.InitializeData();
            Assert.That(data.Count, Is.GreaterThan(index));
            Assert.That(data, Is.All.Empty);
        }

        [TestCase(DataIndexConstants.AttackData.NameIndex, 0)]
        [TestCase(DataIndexConstants.AttackData.DamageRollIndex, 1)]
        [TestCase(DataIndexConstants.AttackData.IsNaturalIndex, 2)]
        [TestCase(DataIndexConstants.AttackData.IsMeleeIndex, 3)]
        [TestCase(DataIndexConstants.AttackData.IsPrimaryIndex, 4)]
        [TestCase(DataIndexConstants.AttackData.IsSpecialIndex, 5)]
        [TestCase(DataIndexConstants.AttackData.FrequencyQuantityIndex, 6)]
        [TestCase(DataIndexConstants.AttackData.FrequencyTimePeriodIndex, 7)]
        [TestCase(DataIndexConstants.AttackData.SaveAbilityIndex, 8)]
        [TestCase(DataIndexConstants.AttackData.SaveIndex, 9)]
        [TestCase(DataIndexConstants.AttackData.AttackTypeIndex, 10)]
        [TestCase(DataIndexConstants.AttackData.DamageEffectIndex, 11)]
        [TestCase(DataIndexConstants.AttackData.DamageBonusMultiplierIndex, 12)]
        [TestCase(DataIndexConstants.AttackData.SaveDcBonusIndex, 13)]
        public void AttackDataIndex(int constant, int value)
        {
            Assert.That(constant, Is.EqualTo(value));
        }

        [TestCase(DataIndexConstants.AttackData.NameIndex)]
        [TestCase(DataIndexConstants.AttackData.DamageRollIndex)]
        [TestCase(DataIndexConstants.AttackData.IsNaturalIndex)]
        [TestCase(DataIndexConstants.AttackData.IsMeleeIndex)]
        [TestCase(DataIndexConstants.AttackData.IsPrimaryIndex)]
        [TestCase(DataIndexConstants.AttackData.IsSpecialIndex)]
        [TestCase(DataIndexConstants.AttackData.FrequencyQuantityIndex)]
        [TestCase(DataIndexConstants.AttackData.FrequencyTimePeriodIndex)]
        [TestCase(DataIndexConstants.AttackData.SaveAbilityIndex)]
        [TestCase(DataIndexConstants.AttackData.SaveIndex)]
        [TestCase(DataIndexConstants.AttackData.AttackTypeIndex)]
        [TestCase(DataIndexConstants.AttackData.DamageEffectIndex)]
        [TestCase(DataIndexConstants.AttackData.DamageBonusMultiplierIndex)]
        [TestCase(DataIndexConstants.AttackData.SaveDcBonusIndex)]
        public void AttackDataIndicesInitialized(int index)
        {
            var data = DataIndexConstants.AttackData.InitializeData();
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
        [TestCase(DataIndexConstants.FeatData.RequiresSpellLikeAbilityIndex, 7)]
        [TestCase(DataIndexConstants.FeatData.RequiresNaturalArmorIndex, 8)]
        [TestCase(DataIndexConstants.FeatData.RequiredNaturalWeaponQuantityIndex, 9)]
        [TestCase(DataIndexConstants.FeatData.RequiredHandQuantityIndex, 10)]
        [TestCase(DataIndexConstants.FeatData.RequiresEquipmentIndex, 11)]
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
        [TestCase(DataIndexConstants.FeatData.RequiresSpellLikeAbilityIndex)]
        [TestCase(DataIndexConstants.FeatData.RequiresNaturalArmorIndex)]
        [TestCase(DataIndexConstants.FeatData.RequiredNaturalWeaponQuantityIndex)]
        [TestCase(DataIndexConstants.FeatData.RequiredHandQuantityIndex)]
        [TestCase(DataIndexConstants.FeatData.RequiresEquipmentIndex)]
        public void FeatDataIndicesInitialized(int index)
        {
            var data = DataIndexConstants.FeatData.InitializeData();
            Assert.That(data.Count, Is.GreaterThan(index));
            Assert.That(data, Is.All.Empty);
        }

        [TestCase(DataIndexConstants.SkillSelectionData.BaseAbilityNameIndex, 0)]
        [TestCase(DataIndexConstants.SkillSelectionData.SkillNameIndex, 1)]
        [TestCase(DataIndexConstants.SkillSelectionData.RandomFociQuantityIndex, 2)]
        [TestCase(DataIndexConstants.SkillSelectionData.FocusIndex, 3)]
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