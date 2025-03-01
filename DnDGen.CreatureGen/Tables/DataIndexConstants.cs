namespace DnDGen.CreatureGen.Tables
{
    internal static class DataIndexConstants
    {
        private static string[] InitializeData(int maxIndex)
        {
            var capacity = maxIndex + 1;
            var data = new string[capacity];

            for (var i = 0; i < data.Length; i++)
                data[i] = string.Empty;

            return data;
        }

        internal static class AttackData
        {
            public const int NameIndex = 0;
            public const int DamageDataIndex = 1;
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
            public const int RequiredGenderIndex = 14;

            public static string[] InitializeData()
            {
                return DataIndexConstants.InitializeData(RequiredGenderIndex);
            }

            internal static class DamageData
            {
                public const int RollIndex = 0;
                public const int TypeIndex = 1;
                public const int ConditionIndex = 2;

                public static string[] InitializeData()
                {
                    return DataIndexConstants.InitializeData(ConditionIndex);
                }
            }
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

            public static string[] InitializeData()
            {
                return DataIndexConstants.InitializeData(MaxHitDiceIndex);
            }
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

            public static string[] InitializeData()
            {
                return DataIndexConstants.InitializeData(RequiresEquipmentIndex);
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

            public static string[] InitializeData()
            {
                return DataIndexConstants.InitializeData(NumberOfHands);
            }
        }

        internal static class AdvancementSelectionData
        {
            public const int Size = 0;
            public const int Space = 1;
            public const int Reach = 2;
            public const int AdditionalHitDiceRoll = 3;
        }
    }
}