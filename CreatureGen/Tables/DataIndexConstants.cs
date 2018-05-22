using System.Collections.Generic;

namespace CreatureGen.Tables
{
    internal static class DataIndexConstants
    {
        private static List<string> InitializeData(int maxIndex)
        {
            var capacity = maxIndex + 1;
            var data = new List<string>(capacity);

            while (data.Count < data.Capacity)
                data.Add(string.Empty);

            return data;
        }

        internal static class SpecialQualityData
        {
            public const int FeatNameIndex = 0;
            public const int SizeRequirementIndex = 1;
            public const int MinimumHitDiceRequirementIndex = 2;
            public const int PowerIndex = 3;
            public const int FocusIndex = 4;
            public const int FrequencyQuantityIndex = 5;
            public const int FrequencyTimePeriodIndex = 6;
            public const int MaximumHitDiceRequirementIndex = 7;
            public const int RandomFociQuantity = 8;
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
            public const int RequiresSpellLikeAbility = 7;
            public const int RequiresNaturalArmor = 8;
            public const int RequiredNaturalWeaponQuantityIndex = 9;
            public const int RequiredHandQuantityIndex = 10;

            public static List<string> InitializeData()
            {
                return DataIndexConstants.InitializeData(RequiredHandQuantityIndex);
            }
        }

        internal static class SkillSelectionData
        {
            public const int BaseStatName = 0;
            public const int SkillName = 1;
            public const int RandomFociQuantity = 2;
            public const int Focus = 3;
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

            public static List<string> InitializeData()
            {
                return DataIndexConstants.InitializeData(NumberOfHands);
            }
        }
    }
}