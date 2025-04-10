using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Alignments;
using DnDGen.CreatureGen.Defenses;
using DnDGen.CreatureGen.Feats;
using DnDGen.CreatureGen.Tables;
using DnDGen.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Selectors.Selections
{
    internal class SpecialQualityDataSelection : DataSelection<SpecialQualityDataSelection>
    {
        public string Feat { get; set; }
        public int FrequencyQuantity { get; set; }
        public string FrequencyTimePeriod { get; set; }
        public string FocusType { get; set; }
        public int Power { get; set; }
        public Dictionary<string, int> MinimumAbilities { get; set; }
        public string RandomFociQuantityRoll { get; set; }
        public IEnumerable<FeatDataSelection.RequiredFeatDataSelection> RequiredFeats { get; set; }
        public IEnumerable<string> RequiredSizes { get; set; }
        public IEnumerable<string> RequiredAlignments { get; set; }
        public bool RequiresEquipment { get; set; }
        public string SaveAbility { get; set; }
        public string Save { get; set; }
        public int SaveBaseValue { get; set; }
        public int MinHitDice { get; set; }
        public int MaxHitDice { get; set; }

        public override Func<string[], SpecialQualityDataSelection> MapTo => Map;
        public override Func<SpecialQualityDataSelection, string[]> MapFrom => Map;

        public override int SectionCount => 10;
        public static char Delimiter => '|';

        public static SpecialQualityDataSelection Map(string[] splitData)
        {
            var selection = new SpecialQualityDataSelection
            {
                Feat = splitData[DataIndexConstants.SpecialQualityData.FeatNameIndex],
                FocusType = splitData[DataIndexConstants.SpecialQualityData.FocusIndex],
                FrequencyQuantity = Convert.ToInt32(splitData[DataIndexConstants.SpecialQualityData.FrequencyQuantityIndex]),
                FrequencyTimePeriod = splitData[DataIndexConstants.SpecialQualityData.FrequencyTimePeriodIndex],
                Power = Convert.ToInt32(splitData[DataIndexConstants.SpecialQualityData.PowerIndex]),
                RandomFociQuantityRoll = splitData[DataIndexConstants.SpecialQualityData.RandomFociQuantityIndex],
                Save = splitData[DataIndexConstants.SpecialQualityData.SaveIndex],
                SaveAbility = splitData[DataIndexConstants.SpecialQualityData.SaveIndex],
                SaveBaseValue = Convert.ToInt32(splitData[DataIndexConstants.SpecialQualityData.SaveBaseValueIndex]),
                MinHitDice = Convert.ToInt32(splitData[DataIndexConstants.SpecialQualityData.MinHitDiceIndex]),
                MaxHitDice = Convert.ToInt32(splitData[DataIndexConstants.SpecialQualityData.MaxHitDiceIndex]),
                RequiresEquipment = Convert.ToBoolean(splitData[DataIndexConstants.SpecialQualityData.RequiresEquipmentIndex]),
                RequiredFeats = GetRequiredFeats(splitData[DataIndexConstants.SpecialQualityData.RequiredFeatsIndex]),
                MinimumAbilities = GetRequiredAbilities(splitData),
                RequiredSizes = GetRequiredSizes(splitData[DataIndexConstants.SpecialQualityData.RequiredSizesIndex]),
                RequiredAlignments = GetRequiredSizes(splitData[DataIndexConstants.SpecialQualityData.RequiredAlignmentsIndex]),
            };

            return selection;
        }

        private static IEnumerable<string> GetRequiredSizes(string requiredSizesData)
        {
            if (string.IsNullOrEmpty(requiredSizesData))
                return [];

            return requiredSizesData.Split(Delimiter);
        }

        private static Dictionary<string, int> GetRequiredSpeeds(string[] splitData) => new()
        {
            [SpeedConstants.Fly] = Convert.ToInt32(splitData[DataIndexConstants.SpecialQualityData.RequiredFlySpeedIndex]),
        };

        private static Dictionary<string, int> GetRequiredAbilities(string[] splitData) => new()
        {
            [AbilityConstants.Strength] = GetRequiredAbility(splitData[DataIndexConstants.SpecialQualityData.RequiredStrengthIndex]),
            [AbilityConstants.Constitution] = GetRequiredAbility(splitData[DataIndexConstants.SpecialQualityData.RequiredConstitutionIndex]),
            [AbilityConstants.Dexterity] = GetRequiredAbility(splitData[DataIndexConstants.SpecialQualityData.RequiredDexterityIndex]),
            [AbilityConstants.Intelligence] = GetRequiredAbility(splitData[DataIndexConstants.SpecialQualityData.RequiredIntelligenceIndex]),
            [AbilityConstants.Wisdom] = GetRequiredAbility(splitData[DataIndexConstants.SpecialQualityData.RequiredWisdomIndex]),
            [AbilityConstants.Charisma] = GetRequiredAbility(splitData[DataIndexConstants.SpecialQualityData.RequiredCharismaIndex]),
        };

        private static int GetRequiredAbility(string requiredAbilities)
        {
            if (string.IsNullOrEmpty(requiredAbilities))
                return 0;

            return Convert.ToInt32(requiredAbilities);
        }

        private static IEnumerable<RequiredSkillDataSelection> GetRequiredSkills(string requiredSkillsData)
        {
            if (string.IsNullOrEmpty(requiredSkillsData))
                return [];

            return requiredSkillsData.Split(Delimiter).Select(DataHelper.Parse<RequiredSkillDataSelection>);
        }

        private static IEnumerable<RequiredFeatDataSelection> GetRequiredFeats(string requiredFeatsData)
        {
            if (string.IsNullOrEmpty(requiredFeatsData))
                return [];

            return requiredFeatsData.Split(Delimiter).Select(DataHelper.Parse<RequiredFeatDataSelection>);
        }

        public static string[] Map(SpecialQualityDataSelection selection)
        {
            var data = new string[selection.SectionCount];
            data[DataIndexConstants.SpecialQualityData.NameIndex] = selection.Feat;
            data[DataIndexConstants.SpecialQualityData.BaseAttackRequirementIndex] = selection.RequiredBaseAttack.ToString();
            data[DataIndexConstants.SpecialQualityData.FocusTypeIndex] = selection.FocusType;
            data[DataIndexConstants.SpecialQualityData.FrequencyQuantityIndex] = selection.FrequencyQuantity.ToString();
            data[DataIndexConstants.SpecialQualityData.FrequencyTimePeriodIndex] = selection.FrequencyTimePeriod;
            data[DataIndexConstants.SpecialQualityData.PowerIndex] = selection.Power.ToString();
            data[DataIndexConstants.SpecialQualityData.MinimumCasterLevelIndex] = selection.MinimumCasterLevel.ToString();
            data[DataIndexConstants.SpecialQualityData.RequiredHandQuantityIndex] = selection.RequiredHands.ToString();
            data[DataIndexConstants.SpecialQualityData.RequiredNaturalWeaponQuantityIndex] = selection.RequiredNaturalWeapons.ToString();
            data[DataIndexConstants.SpecialQualityData.RequiresNaturalArmorIndex] = selection.RequiresNaturalArmor.ToString();
            data[DataIndexConstants.SpecialQualityData.RequiresSpecialAttackIndex] = selection.RequiresSpecialAttack.ToString();
            data[DataIndexConstants.SpecialQualityData.RequiresSpellLikeAbilityIndex] = selection.RequiresSpellLikeAbility.ToString();
            data[DataIndexConstants.SpecialQualityData.RequiresEquipmentIndex] = selection.RequiresEquipment.ToString();
            data[DataIndexConstants.SpecialQualityData.RequiredFeatsIndex] = GetRequiredFeats(selection.RequiredFeats);
            data[DataIndexConstants.SpecialQualityData.RequiredSkillsIndex] = GetRequiredSkills(selection.RequiredSkills);
            data[DataIndexConstants.SpecialQualityData.RequiredStrengthIndex] = GetRequiredAbility(AbilityConstants.Strength, selection.RequiredAbilities);
            data[DataIndexConstants.SpecialQualityData.RequiredConstitutionIndex] = GetRequiredAbility(AbilityConstants.Constitution, selection.RequiredAbilities);
            data[DataIndexConstants.SpecialQualityData.RequiredDexterityIndex] = GetRequiredAbility(AbilityConstants.Dexterity, selection.RequiredAbilities);
            data[DataIndexConstants.SpecialQualityData.RequiredIntelligenceIndex] = GetRequiredAbility(AbilityConstants.Intelligence, selection.RequiredAbilities);
            data[DataIndexConstants.SpecialQualityData.RequiredWisdomIndex] = GetRequiredAbility(AbilityConstants.Wisdom, selection.RequiredAbilities);
            data[DataIndexConstants.SpecialQualityData.RequiredCharismaIndex] = GetRequiredAbility(AbilityConstants.Charisma, selection.RequiredAbilities);
            data[DataIndexConstants.SpecialQualityData.RequiredFlySpeedIndex] = GetRequiredSpeed(SpeedConstants.Fly, selection.RequiredSpeeds);
            data[DataIndexConstants.SpecialQualityData.RequiredSizesIndex] = GetRequiredSizes(selection.RequiredSizes);
            data[DataIndexConstants.SpecialQualityData.TakenMultipleTimesIndex] = selection.CanBeTakenMultipleTimes.ToString();

            return data;
        }

        private static string GetRequiredSizes(IEnumerable<string> requiredSizesData) => string.Join(Delimiter, requiredSizesData);

        private static string GetRequiredSpeed(string speed, Dictionary<string, int> requiredSpeeds)
        {
            if (!requiredSpeeds.ContainsKey(speed))
                return 0.ToString();

            return requiredSpeeds[speed].ToString();
        }

        private static string GetRequiredAbility(string ability, Dictionary<string, int> requiredAbilities)
        {
            if (!requiredAbilities.ContainsKey(ability))
                return 0.ToString();

            return requiredAbilities[ability].ToString();
        }

        private static string GetRequiredSkills(IEnumerable<RequiredSkillDataSelection> requiredSkillsData)
        {
            var parsedData = requiredSkillsData.Select(DataHelper.Parse);
            return string.Join(Delimiter, parsedData);
        }

        private static string GetRequiredFeats(IEnumerable<RequiredFeatDataSelection> requiredFeatsData)
        {
            var parsedData = requiredFeatsData.Select(DataHelper.Parse);
            return string.Join(Delimiter, parsedData);
        }

        public SpecialQualityDataSelection()
        {
            Feat = string.Empty;
            Frequency = new Frequency();
            FocusType = string.Empty;
            MinimumAbilities = [];
            RandomFociQuantityRoll = string.Empty;
            RequiredFeats = [];
            RequiredSizes = [];
            RequiredAlignments = [];
            SaveAbility = string.Empty;
            Save = string.Empty;
            MaxHitDice = int.MaxValue;
        }

        public bool RequirementsMet(Dictionary<string, Ability> abilities, IEnumerable<Feat> feats, bool canUseEquipment, string size, Alignment alignment, HitPoints hitPoints)
        {
            if (!MinimumAbilityMet(abilities))
                return false;

            foreach (var requirement in RequiredFeats)
            {
                var requirementFeats = feats.Where(f => f.Name == requirement.Feat);

                if (!requirementFeats.Any())
                    return false;

                if (requirement.Foci.Any() && !requirementFeats.Any(f => f.Foci.Intersect(requirement.Foci).Any()))
                    return false;
            }

            if (RequiredSizes.Any() && !RequiredSizes.Contains(size))
                return false;

            if (RequiredAlignments.Any() && !RequiredAlignments.Contains(alignment.Full))
                return false;

            if (hitPoints.RoundedHitDiceQuantity < MinHitDice || hitPoints.RoundedHitDiceQuantity > MaxHitDice)
                return false;

            return !RequiresEquipment || canUseEquipment;
        }

        private bool MinimumAbilityMet(Dictionary<string, Ability> abilities)
        {
            if (!MinimumAbilities.Any())
                return true;

            foreach (var minimumAbility in MinimumAbilities)
                if (abilities[minimumAbility.Key].FullScore >= minimumAbility.Value)
                    return true;

            return false;
        }
    }
}