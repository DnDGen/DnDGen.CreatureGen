using CreatureGen.Creatures;
using CreatureGen.Selectors.Helpers;
using CreatureGen.Selectors.Selections;
using CreatureGen.Skills;
using CreatureGen.Tables;
using DnDGen.Core.Selectors.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CreatureGen.Selectors.Collections
{
    internal class FeatsSelector : IFeatsSelector
    {
        private readonly ICollectionSelector collectionsSelector;
        private readonly ITypeAndAmountSelector typeAndAmountSelector;

        public FeatsSelector(ICollectionSelector collectionsSelector, ITypeAndAmountSelector typeAndAmountSelector)
        {
            this.collectionsSelector = collectionsSelector;
            this.typeAndAmountSelector = typeAndAmountSelector;
        }

        public IEnumerable<FeatSelection> SelectFeats()
        {
            var featData = collectionsSelector.SelectAllFrom(TableNameConstants.Collection.FeatData);
            var featSelections = new List<FeatSelection>();

            foreach (var dataKVP in featData)
            {
                var featSelection = SelectFeat(dataKVP);
                featSelections.Add(featSelection);
            }

            return featSelections;
        }

        private FeatSelection SelectFeat(KeyValuePair<string, IEnumerable<string>> dataKVP)
        {
            var featSelection = new FeatSelection();
            featSelection.Feat = dataKVP.Key;

            var data = dataKVP.Value.ToArray();
            featSelection.RequiredBaseAttack = Convert.ToInt32(data[DataIndexConstants.FeatData.BaseAttackRequirementIndex]);
            featSelection.FocusType = data[DataIndexConstants.FeatData.FocusTypeIndex];
            featSelection.Frequency.Quantity = Convert.ToInt32(data[DataIndexConstants.FeatData.FrequencyQuantityIndex]);
            featSelection.Frequency.TimePeriod = data[DataIndexConstants.FeatData.FrequencyTimePeriodIndex];
            featSelection.Power = Convert.ToInt32(data[DataIndexConstants.FeatData.PowerIndex]);
            featSelection.MinimumCasterLevel = Convert.ToInt32(data[DataIndexConstants.FeatData.MinimumCasterLevelIndex]);
            featSelection.RequiredHands = Convert.ToInt32(data[DataIndexConstants.FeatData.RequiredHandQuantityIndex]);
            featSelection.RequiredNaturalWeapons = Convert.ToInt32(data[DataIndexConstants.FeatData.RequiredNaturalWeaponQuantityIndex]);
            featSelection.RequiresNaturalArmor = Convert.ToBoolean(data[DataIndexConstants.FeatData.RequiresNaturalArmor]);
            featSelection.RequiresSpecialAttack = Convert.ToBoolean(data[DataIndexConstants.FeatData.RequiresSpecialAttackIndex]);
            featSelection.RequiresSpellLikeAbility = Convert.ToBoolean(data[DataIndexConstants.FeatData.RequiresSpellLikeAbility]);

            featSelection.RequiredFeats = GetRequiredFeats(featSelection.Feat);
            featSelection.RequiredSkills = GetRequiredSkills(featSelection.Feat);
            featSelection.RequiredAbilities = GetRequiredAbilities(featSelection.Feat);
            featSelection.RequiredSpeeds = GetRequiredSpeeds(featSelection.Feat);
            featSelection.RequiredSizes = GetRequiredSizes(featSelection.Feat);

            var featsTakenMultipleTimes = collectionsSelector.SelectFrom(TableNameConstants.Collection.FeatGroups, GroupConstants.TakenMultipleTimes);
            featSelection.CanBeTakenMultipleTimes = featsTakenMultipleTimes.Contains(featSelection.Feat);

            return featSelection;
        }

        private Dictionary<string, int> GetRequiredAbilities(string feat)
        {
            var requiredAbilitiesAndValues = typeAndAmountSelector.Select(TableNameConstants.TypeAndAmount.FeatAbilityRequirements, feat);
            var requiredAbilities = new Dictionary<string, int>();

            foreach (var selection in requiredAbilitiesAndValues)
            {
                requiredAbilities[selection.Type] = selection.Amount;
            }

            return requiredAbilities;
        }

        private Dictionary<string, int> GetRequiredSpeeds(string feat)
        {
            var requiredSpeedsAndValues = typeAndAmountSelector.Select(TableNameConstants.TypeAndAmount.FeatSpeedRequirements, feat);
            var requiredSpeeds = new Dictionary<string, int>();

            foreach (var selection in requiredSpeedsAndValues)
            {
                requiredSpeeds[selection.Type] = selection.Amount;
            }

            return requiredSpeeds;
        }

        private IEnumerable<RequiredSkillSelection> GetRequiredSkills(string feat)
        {
            var requiredSkillsAndRanks = typeAndAmountSelector.Select(TableNameConstants.TypeAndAmount.FeatSkillRankRequirements, feat);
            var requiredSkillSelections = new List<RequiredSkillSelection>();

            foreach (var selection in requiredSkillsAndRanks)
            {
                var requiredSkill = ParseRequiredSkillData(selection);
                requiredSkillSelections.Add(requiredSkill);
            }

            return requiredSkillSelections;
        }

        private RequiredSkillSelection ParseRequiredSkillData(TypeAndAmountSelection selection)
        {
            var splitData = SkillConstants.Parse(selection.Type);
            var requiredSkill = new RequiredSkillSelection();
            requiredSkill.Skill = splitData[0];
            requiredSkill.Ranks = selection.Amount;

            if (splitData.Length > 1)
                requiredSkill.Focus = splitData[1];

            return requiredSkill;
        }

        public IEnumerable<SpecialQualitySelection> SelectSpecialQualities(string creature, CreatureType creatureType)
        {
            var specialQualitiesWithSource = new Dictionary<string, IEnumerable<string>>();

            specialQualitiesWithSource[creature] = collectionsSelector.SelectFrom(TableNameConstants.Collection.SpecialQualityData, creature);
            specialQualitiesWithSource[creatureType.Name] = collectionsSelector.SelectFrom(TableNameConstants.Collection.SpecialQualityData, creatureType.Name);

            foreach (var subtype in creatureType.SubTypes)
            {
                specialQualitiesWithSource[subtype] = collectionsSelector.SelectFrom(TableNameConstants.Collection.SpecialQualityData, subtype);
            }

            if (!specialQualitiesWithSource.SelectMany(kvp => kvp.Value).Any())
                return Enumerable.Empty<SpecialQualitySelection>();

            var specialQualitySelections = new List<SpecialQualitySelection>();
            var usedSpecialQualities = new List<string>();

            foreach (var specialQualityKvp in specialQualitiesWithSource)
            {
                var source = specialQualityKvp.Key;
                var specialQualities = specialQualityKvp.Value;
                var newSpecialQualities = specialQualityKvp.Value.Except(usedSpecialQualities).ToArray();

                foreach (var specialQuality in newSpecialQualities)
                {
                    var data = SpecialQualityHelper.ParseData(specialQuality);

                    var specialQualitySelection = new SpecialQualitySelection();
                    specialQualitySelection.Feat = data[DataIndexConstants.SpecialQualityData.FeatNameIndex];
                    specialQualitySelection.Power = Convert.ToInt32(data[DataIndexConstants.SpecialQualityData.PowerIndex]);
                    specialQualitySelection.FocusType = data[DataIndexConstants.SpecialQualityData.FocusIndex];
                    specialQualitySelection.Frequency.Quantity = Convert.ToInt32(data[DataIndexConstants.SpecialQualityData.FrequencyQuantityIndex]);
                    specialQualitySelection.Frequency.TimePeriod = data[DataIndexConstants.SpecialQualityData.FrequencyTimePeriodIndex];
                    specialQualitySelection.RandomFociQuantity = data[DataIndexConstants.SpecialQualityData.RandomFociQuantityIndex];
                    specialQualitySelection.RequiresEquipment = Convert.ToBoolean(data[DataIndexConstants.SpecialQualityData.RequiresEquipmentIndex]);

                    specialQualitySelection.RequiredFeats = GetRequiredFeats(source + specialQualitySelection.Feat);
                    specialQualitySelection.MinimumAbilities = GetRequiredAbilities(source + specialQualitySelection.Feat);
                    specialQualitySelection.RequiredSizes = GetRequiredSizes(source + specialQualitySelection.Feat);
                    specialQualitySelection.RequiredAlignments = GetRequiredAlignments(source + specialQualitySelection.Feat);

                    specialQualitySelections.Add(specialQualitySelection);
                    usedSpecialQualities.Add(specialQuality);
                }
            }

            return specialQualitySelections;
        }

        private IEnumerable<string> GetRequiredSizes(string source)
        {
            return collectionsSelector.SelectFrom(TableNameConstants.Collection.RequiredSizes, source);
        }

        private IEnumerable<string> GetRequiredAlignments(string source)
        {
            return collectionsSelector.SelectFrom(TableNameConstants.Collection.RequiredAlignments, source);
        }

        private IEnumerable<RequiredFeatSelection> GetRequiredFeats(string feat)
        {
            var requiredFeatsData = collectionsSelector.SelectFrom(TableNameConstants.Collection.RequiredFeats, feat);
            var requiredFeatsSelections = new List<RequiredFeatSelection>();

            foreach (var requiredFeatData in requiredFeatsData)
            {
                var requiredFeat = ParseRequiredFeatData(requiredFeatData);
                requiredFeatsSelections.Add(requiredFeat);
            }

            return requiredFeatsSelections;
        }

        private RequiredFeatSelection ParseRequiredFeatData(string requiredFeatData)
        {
            var splitData = requiredFeatData.Split('/');
            var requiredFeat = new RequiredFeatSelection();
            requiredFeat.Feat = splitData[0];

            if (splitData.Length > 1)
                requiredFeat.Foci = splitData[1].Split(',');

            return requiredFeat;
        }
    }
}