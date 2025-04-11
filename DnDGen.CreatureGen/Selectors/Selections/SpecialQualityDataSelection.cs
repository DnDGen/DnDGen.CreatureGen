using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Alignments;
using DnDGen.CreatureGen.Defenses;
using DnDGen.CreatureGen.Feats;
using DnDGen.CreatureGen.Tables;
using DnDGen.Infrastructure.Helpers;
using DnDGen.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using static DnDGen.CreatureGen.Selectors.Selections.FeatDataSelection;

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

        public override int SectionCount => 21;
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
                RequiredAlignments = GetRequiredAlignments(splitData[DataIndexConstants.SpecialQualityData.RequiredAlignmentsIndex]),
            };

            return selection;
        }

        private static IEnumerable<string> GetRequiredSizes(string requiredSizesData) => Split(requiredSizesData);
        private static IEnumerable<string> GetRequiredAlignments(string requiredAlignmentsData) => Split(requiredAlignmentsData);

        private static IEnumerable<string> Split(string data)
        {
            if (string.IsNullOrEmpty(data))
                return [];

            return data.Split(Delimiter);
        }

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

        private static IEnumerable<RequiredFeatDataSelection> GetRequiredFeats(string requiredFeatsData) =>
            Split(requiredFeatsData).Select(DataHelper.Parse<RequiredFeatDataSelection>);

        public static string[] Map(SpecialQualityDataSelection selection)
        {
            var data = new string[selection.SectionCount];
            data[DataIndexConstants.SpecialQualityData.FeatNameIndex] = selection.Feat;
            data[DataIndexConstants.SpecialQualityData.FocusIndex] = selection.FocusType;
            data[DataIndexConstants.SpecialQualityData.FrequencyQuantityIndex] = selection.FrequencyQuantity.ToString();
            data[DataIndexConstants.SpecialQualityData.FrequencyTimePeriodIndex] = selection.FrequencyTimePeriod;
            data[DataIndexConstants.SpecialQualityData.PowerIndex] = selection.Power.ToString();
            data[DataIndexConstants.SpecialQualityData.RandomFociQuantityIndex] = selection.RandomFociQuantityRoll;
            data[DataIndexConstants.SpecialQualityData.SaveIndex] = selection.Save;
            data[DataIndexConstants.SpecialQualityData.SaveAbilityIndex] = selection.SaveAbility;
            data[DataIndexConstants.SpecialQualityData.SaveBaseValueIndex] = selection.SaveBaseValue.ToString();
            data[DataIndexConstants.SpecialQualityData.MinHitDiceIndex] = selection.MinHitDice.ToString();
            data[DataIndexConstants.SpecialQualityData.MaxHitDiceIndex] = selection.MaxHitDice.ToString();
            data[DataIndexConstants.SpecialQualityData.RequiresEquipmentIndex] = selection.RequiresEquipment.ToString();
            data[DataIndexConstants.SpecialQualityData.RequiredFeatsIndex] = GetRequiredFeats(selection.RequiredFeats);
            data[DataIndexConstants.SpecialQualityData.RequiredStrengthIndex] = GetRequiredAbility(AbilityConstants.Strength, selection.MinimumAbilities);
            data[DataIndexConstants.SpecialQualityData.RequiredConstitutionIndex] = GetRequiredAbility(AbilityConstants.Constitution, selection.MinimumAbilities);
            data[DataIndexConstants.SpecialQualityData.RequiredDexterityIndex] = GetRequiredAbility(AbilityConstants.Dexterity, selection.MinimumAbilities);
            data[DataIndexConstants.SpecialQualityData.RequiredIntelligenceIndex] = GetRequiredAbility(AbilityConstants.Intelligence, selection.MinimumAbilities);
            data[DataIndexConstants.SpecialQualityData.RequiredWisdomIndex] = GetRequiredAbility(AbilityConstants.Wisdom, selection.MinimumAbilities);
            data[DataIndexConstants.SpecialQualityData.RequiredCharismaIndex] = GetRequiredAbility(AbilityConstants.Charisma, selection.MinimumAbilities);
            data[DataIndexConstants.SpecialQualityData.RequiredSizesIndex] = GetRequiredSizes(selection.RequiredSizes);
            data[DataIndexConstants.SpecialQualityData.RequiredAlignmentsIndex] = GetRequiredAlignments(selection.RequiredAlignments);

            return data;
        }

        private static string GetRequiredSizes(IEnumerable<string> requiredSizesData) => Join(requiredSizesData);
        private static string GetRequiredAlignments(IEnumerable<string> requiredAlignmentsData) => Join(requiredAlignmentsData);
        private static string Join(IEnumerable<string> data) => string.Join(Delimiter, data);

        private static string GetFromDictionary(string key, Dictionary<string, int> dictionary)
        {
            if (!dictionary.ContainsKey(key))
                return 0.ToString();

            return dictionary[key].ToString();
        }

        private static string GetRequiredAbility(string ability, Dictionary<string, int> requiredAbilities) => GetFromDictionary(ability, requiredAbilities);

        private static string GetRequiredFeats(IEnumerable<RequiredFeatDataSelection> requiredFeatsData) =>
            Join(requiredFeatsData.Select(DataHelper.Parse));

        public SpecialQualityDataSelection()
        {
            Feat = string.Empty;
            FrequencyTimePeriod = string.Empty;
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