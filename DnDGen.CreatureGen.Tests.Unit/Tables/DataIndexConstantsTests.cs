using DnDGen.CreatureGen.Tables;
using NUnit.Framework;

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
        [TestCase(DataIndexConstants.SpecialQualityData.RequiredFeatsIndex, 12)]
        [TestCase(DataIndexConstants.SpecialQualityData.RequiredSizesIndex, 13)]
        [TestCase(DataIndexConstants.SpecialQualityData.RequiredAlignmentsIndex, 14)]
        [TestCase(DataIndexConstants.SpecialQualityData.RequiredStrengthIndex, 15)]
        [TestCase(DataIndexConstants.SpecialQualityData.RequiredConstitutionIndex, 16)]
        [TestCase(DataIndexConstants.SpecialQualityData.RequiredDexterityIndex, 17)]
        [TestCase(DataIndexConstants.SpecialQualityData.RequiredIntelligenceIndex, 18)]
        [TestCase(DataIndexConstants.SpecialQualityData.RequiredWisdomIndex, 19)]
        [TestCase(DataIndexConstants.SpecialQualityData.RequiredCharismaIndex, 20)]
        public void SpecialQualityDataIndex(int constant, int value)
        {
            Assert.That(constant, Is.EqualTo(value));
        }

        [TestCase(DataIndexConstants.AttackData.NameIndex, 0)]
        [TestCase(DataIndexConstants.AttackData.RequiredGenderIndex, 1)]
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

        [TestCase(DataIndexConstants.AttackData.DamageData.RollIndex, 0)]
        [TestCase(DataIndexConstants.AttackData.DamageData.TypeIndex, 1)]
        [TestCase(DataIndexConstants.AttackData.DamageData.ConditionIndex, 2)]
        public void DamageDataIndex(int constant, int value)
        {
            Assert.That(constant, Is.EqualTo(value));
        }

        [TestCase(DataIndexConstants.BonusData.TargetIndex, 0)]
        [TestCase(DataIndexConstants.BonusData.BonusIndex, 1)]
        [TestCase(DataIndexConstants.BonusData.ConditionIndex, 2)]
        public void BonusDataIndex(int constant, int value)
        {
            Assert.That(constant, Is.EqualTo(value));
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
        [TestCase(DataIndexConstants.FeatData.NameIndex, 12)]
        [TestCase(DataIndexConstants.FeatData.RequiredFeatsIndex, 13)]
        [TestCase(DataIndexConstants.FeatData.RequiredSkillsIndex, 14)]
        [TestCase(DataIndexConstants.FeatData.RequiredStrengthIndex, 15)]
        [TestCase(DataIndexConstants.FeatData.RequiredConstitutionIndex, 16)]
        [TestCase(DataIndexConstants.FeatData.RequiredDexterityIndex, 17)]
        [TestCase(DataIndexConstants.FeatData.RequiredIntelligenceIndex, 18)]
        [TestCase(DataIndexConstants.FeatData.RequiredWisdomIndex, 19)]
        [TestCase(DataIndexConstants.FeatData.RequiredCharismaIndex, 20)]
        [TestCase(DataIndexConstants.FeatData.RequiredFlySpeedIndex, 21)]
        [TestCase(DataIndexConstants.FeatData.TakenMultipleTimesIndex, 22)]
        [TestCase(DataIndexConstants.FeatData.RequiredSizesIndex, 23)]
        public void FeatDataIndex(int constant, int value)
        {
            Assert.That(constant, Is.EqualTo(value));
        }

        [TestCase(DataIndexConstants.FeatData.RequiredSkillData.SkillIndex, 0)]
        [TestCase(DataIndexConstants.FeatData.RequiredSkillData.FocusIndex, 1)]
        [TestCase(DataIndexConstants.FeatData.RequiredSkillData.RanksIndex, 2)]
        public void FeatData_RequiredSkillIndex(int constant, int value)
        {
            Assert.That(constant, Is.EqualTo(value));
        }

        [TestCase(DataIndexConstants.FeatData.RequiredFeatData.FeatIndex, 0)]
        [TestCase(DataIndexConstants.FeatData.RequiredFeatData.FociIndex, 1)]
        public void FeatData_RequiredFeatIndex(int constant, int value)
        {
            Assert.That(constant, Is.EqualTo(value));
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
        [TestCase(DataIndexConstants.CreatureData.BaseAttackQuality, 9)]
        [TestCase(DataIndexConstants.CreatureData.Types, 10)]
        [TestCase(DataIndexConstants.CreatureData.HitDiceQuantity, 11)]
        [TestCase(DataIndexConstants.CreatureData.HitDie, 12)]
        [TestCase(DataIndexConstants.CreatureData.HasSkeleton, 13)]
        public void CreatureDataIndex(int constant, int value)
        {
            Assert.That(constant, Is.EqualTo(value));
        }

        [TestCase(DataIndexConstants.AdvancementSelectionData.Size, 0)]
        [TestCase(DataIndexConstants.AdvancementSelectionData.Space, 1)]
        [TestCase(DataIndexConstants.AdvancementSelectionData.Reach, 2)]
        [TestCase(DataIndexConstants.AdvancementSelectionData.AdditionalHitDiceRoll, 3)]
        [TestCase(DataIndexConstants.AdvancementSelectionData.StrengthAdjustment, 4)]
        [TestCase(DataIndexConstants.AdvancementSelectionData.ConstitutionAdjustment, 5)]
        [TestCase(DataIndexConstants.AdvancementSelectionData.DexterityAdjustment, 6)]
        [TestCase(DataIndexConstants.AdvancementSelectionData.NaturalArmorAdjustment, 7)]
        [TestCase(DataIndexConstants.AdvancementSelectionData.ChallengeRatingDivisor, 8)]
        [TestCase(DataIndexConstants.AdvancementSelectionData.AdjustedChallengeRating, 9)]
        public void AdvancementSelectionDataIndex(int constant, int value)
        {
            Assert.That(constant, Is.EqualTo(value));
        }
    }
}