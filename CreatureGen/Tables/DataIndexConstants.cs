namespace CreatureGen.Tables
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

        internal static class SpecialQualityData
        {
            public const int FeatNameIndex = 0;
            public const int PowerIndex = 1;
            public const int FocusIndex = 2;
            public const int FrequencyQuantityIndex = 3;
            public const int FrequencyTimePeriodIndex = 4;
            public const int RandomFociQuantityIndex = 5;
            public const int RequiresEquipmentIndex = 6;

            public static string[] InitializeData()
            {
                return DataIndexConstants.InitializeData(RequiresEquipmentIndex);
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
            public const int RequiresSpellLikeAbility = 7;
            public const int RequiresNaturalArmor = 8;
            public const int RequiredNaturalWeaponQuantityIndex = 9;
            public const int RequiredHandQuantityIndex = 10;

            public static string[] InitializeData()
            {
                return DataIndexConstants.InitializeData(RequiredHandQuantityIndex);
            }
        }

        internal static class SkillSynergyFeatData
        {
            public const int FeatNameIndex = 0;
            public const int FocusTypeIndex = 1;
            public const int PowerIndex = 2;

            public static string[] InitializeData()
            {
                return DataIndexConstants.InitializeData(PowerIndex);
            }

            public static string BuildDataKey(string sourceSkill, string sourceFocus, string targetSkill, string targetFocus)
            {
                var source = sourceSkill;

                if (!string.IsNullOrEmpty(sourceFocus))
                    source += "/" + sourceFocus;

                var target = targetSkill;

                if (!string.IsNullOrEmpty(targetFocus))
                    target += "/" + targetFocus;

                return $"{source}:{target}";
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
        }
    }
}