namespace DnDGen.CreatureGen.Tables
{
    internal static class TableNameConstants
    {
        internal static class Adjustments
        {
            public const string ArcaneSpellFailures = "ArcaneSpellFailures";
            public const string GrappleBonuses = "GrappleBonuses";
            public const string SizeModifiers = "SizeModifiers";
            public const string SkillPoints = "SkillPoints";
        }

        internal static class Collection
        {
            public const string AbilityGroups = "AbilityGroups";
            public const string Advancements = "Advancements";
            public const string AerialManeuverability = "AerialManeuverability";
            public const string AlignmentGroups = "AlignmentGroups";
            public const string ArmorClassBonuses = "ArmorClassBonuses";
            public const string AttackData = "AttackData";
            public const string CasterGroups = "CasterGroups";
            public const string CreatureData = "CreatureData";
            public const string CreatureGroups = "CreatureGroups";
            public const string CreatureTypes = "CreatureTypes";
            public const string DamageData = "DamageData";
            public const string FeatData = "FeatData";
            public const string FeatFoci = "FeatFoci";
            public const string FeatGroups = "FeatGroups";
            public const string Genders = "Genders";
            public const string LanguageGroups = "LanguageGroups";
            public const string MaxAgeDescriptions = "MaxAgeDescriptions";
            public const string PredeterminedItems = "PredeterminedItems";
            public const string RequiredAlignments = "RequiredAlignments";
            public const string RequiredFeats = "RequiredFeats";
            public const string RequiredSizes = "RequiredSizes";
            public const string SaveBonuses = "SaveBonuses";
            public const string SaveGroups = "SaveGroups";
            public const string SkillBonuses = "SkillBonuses";
            public const string SkillData = "SkillData";
            public const string SkillGroups = "SkillGroups";
            public const string SpecialQualityData = "SpecialQualityData";
            public const string Speeds = "Speeds";
            public const string SpellGroups = "SpellGroups";
            public const string TemplateGroups = "TemplateGroups";
            public const string WeightRolls = "WeightRolls";

            public static string Appearances(string category) => $"{category}Appearances";
            public static class AppearanceCategories
            {
                public const string Skin = "Skin";
                public const string Hair = "Hair";
                public const string Eyes = "Eyes";
                public const string Other = "Other";
            }
        }

        internal static class TypeAndAmount
        {
            public const string AbilityAdjustments = "AbilityAdjustments";
            public const string AgeRolls = "AgeRolls";
            public const string Casters = "Casters";
            public const string FeatAbilityRequirements = "FeatAbilityRequirements";
            public const string FeatSkillRankRequirements = "FeatSkillRankRequirements";
            public const string FeatSpeedRequirements = "FeatSpeedRequirements";
            public const string Heights = "Heights";
            public const string HitDice = "HitDice";
            public const string KnownSpells = "KnownSpells";
            public const string Lengths = "Lengths";
            public const string SpellDomains = "SpellDomains";
            public const string SpellLevels = "SpellLevels";
            public const string SpellsPerDay = "SpellsPerDay";
            public const string Weights = "Weights";
            public const string Wingspans = "Wingspans";
        }
    }
}