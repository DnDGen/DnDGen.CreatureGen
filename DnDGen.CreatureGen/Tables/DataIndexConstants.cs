namespace DnDGen.CreatureGen.Tables
{
    internal static class DataIndexConstants
    {
        internal static class AttackData
        {
            public const int NameIndex = 0;
            public const int RequiredGenderIndex = 1;
            public const int IsNaturalIndex = 2;
            public const int IsMeleeIndex = 3;
            public const int IsPrimaryIndex = 4;
            public const int IsSpecialIndex = 5;
            public const int FrequencyQuantityIndex = 6;
            public const int FrequencyTimePeriodIndex = 7;
            public const int SaveAbilityIndex = 8;
            public const int SaveIndex = 9;
            public const int AttackTypeIndex = 10;
            public const int DamageEffectIndex = 11;
            public const int DamageBonusMultiplierIndex = 12;
            public const int SaveDcBonusIndex = 13;

            internal static class DamageData
            {
                public const int RollIndex = 0;
                public const int TypeIndex = 1;
                public const int ConditionIndex = 2;
            }
        }

        internal static class BonusData
        {
            public const int BonusIndex = 0;
            public const int TargetIndex = 1;
            public const int ConditionIndex = 2;
        }

        internal static class SpecialQualityData
        {
            public const int FeatNameIndex = 0;
            public const int PowerIndex = 1;
            public const int FocusIndex = 2;
            public const int FrequencyQuantityIndex = 3;
            public const int FrequencyTimePeriodIndex = 4;
            public const int RandomFociQuantityIndex = 5;
            public const int RequiresEquipmentIndex = 6;
            public const int SaveAbilityIndex = 7;
            public const int SaveIndex = 8;
            public const int SaveBaseValueIndex = 9;
            public const int MinHitDiceIndex = 10;
            public const int MaxHitDiceIndex = 11;
            public const int RequiredFeatsIndex = 12;
            public const int RequiredSizesIndex = 13;
            public const int RequiredAlignmentsIndex = 14;
            public const int RequiredStrengthIndex = 15;
            public const int RequiredConstitutionIndex = 16;
            public const int RequiredDexterityIndex = 17;
            public const int RequiredIntelligenceIndex = 18;
            public const int RequiredWisdomIndex = 19;
            public const int RequiredCharismaIndex = 20;
        }

        internal static class FeatData
        {
            public const int BaseAttackRequirementIndex = 0;
            public const int FocusTypeIndex = 1;
            public const int PowerIndex = 2;
            public const int FrequencyQuantityIndex = 3;
            public const int FrequencyTimePeriodIndex = 4;
            public const int MinimumCasterLevelIndex = 5;
            public const int RequiresSpecialAttackIndex = 6;
            public const int RequiresSpellLikeAbilityIndex = 7;
            public const int RequiresNaturalArmorIndex = 8;
            public const int RequiredNaturalWeaponQuantityIndex = 9;
            public const int RequiredHandQuantityIndex = 10;
            public const int RequiresEquipmentIndex = 11;
            public const int NameIndex = 12;
            public const int RequiredFeatsIndex = 13;
            public const int RequiredSkillsIndex = 14;
            public const int RequiredStrengthIndex = 15;
            public const int RequiredConstitutionIndex = 16;
            public const int RequiredDexterityIndex = 17;
            public const int RequiredIntelligenceIndex = 18;
            public const int RequiredWisdomIndex = 19;
            public const int RequiredCharismaIndex = 20;
            public const int RequiredFlySpeedIndex = 21;
            public const int TakenMultipleTimesIndex = 22;
            public const int RequiredSizesIndex = 23;

            internal static class RequiredSkillData
            {
                public const int SkillIndex = 0;
                public const int FocusIndex = 1;
                public const int RanksIndex = 2;
            }

            internal static class RequiredFeatData
            {
                public const int FeatIndex = 0;
                public const int FociIndex = 1;
            }
        }

        internal static class SkillSelectionData
        {
            public const int BaseAbilityNameIndex = 0;
            public const int SkillNameIndex = 1;
            public const int RandomFociQuantityIndex = 2;
            public const int FocusIndex = 3;
        }

        internal static class CreatureData
        {
            public const int ChallengeRating = 0;
            public const int LevelAdjustment = 1;
            public const int Reach = 2;
            public const int Size = 3;
            public const int Space = 4;
            public const int CanUseEquipment = 5;
            public const int CasterLevel = 6;
            public const int NaturalArmor = 7;
            public const int NumberOfHands = 8;
            public const int BaseAttackQuality = 9;
        }

        internal static class AdvancementSelectionData
        {
            public const int Size = 0;
            public const int Space = 1;
            public const int Reach = 2;
            public const int AdditionalHitDiceRoll = 3;
            public const int StrengthAdjustment = 4;
            public const int ConstitutionAdjustment = 5;
            public const int DexterityAdjustment = 6;
            public const int NaturalArmorAdjustment = 7;
            public const int ChallengeRatingDivisor = 8;
            public const int AdjustedChallengeRating = 9;
        }
    }
}