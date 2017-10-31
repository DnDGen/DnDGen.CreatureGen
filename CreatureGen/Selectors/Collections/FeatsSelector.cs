using CreatureGen.Selectors.Selections;
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
        private readonly IAdjustmentsSelector adjustmentsSelector;

        public FeatsSelector(ICollectionSelector collectionsSelector, IAdjustmentsSelector adjustmentsSelector)
        {
            this.collectionsSelector = collectionsSelector;
            this.adjustmentsSelector = adjustmentsSelector;
        }

        public IEnumerable<FeatSelection> SelectFeats()
        {
            var featData = collectionsSelector.SelectAllFrom(TableNameConstants.Set.Collection.FeatData);
            var featSelections = new List<FeatSelection>();

            foreach (var dataKVP in featData)
            {
                var featSelection = Select(dataKVP);
                featSelections.Add(featSelection);
            }

            return featSelections;
        }

        private FeatSelection Select(KeyValuePair<string, IEnumerable<string>> dataKVP)
        {
            var featSelection = new FeatSelection();
            featSelection.Feat = dataKVP.Key;

            var data = dataKVP.Value.ToArray();
            featSelection.RequiredBaseAttack = Convert.ToInt32(data[DataIndexConstants.FeatData.BaseAttackRequirementIndex]);
            featSelection.FocusType = data[DataIndexConstants.FeatData.FocusTypeIndex];
            featSelection.Frequency.Quantity = Convert.ToInt32(data[DataIndexConstants.FeatData.FrequencyQuantityIndex]);
            featSelection.Frequency.TimePeriod = data[DataIndexConstants.FeatData.FrequencyTimePeriodIndex];
            featSelection.Power = Convert.ToInt32(data[DataIndexConstants.FeatData.PowerIndex]);
            featSelection.RequiredFeats = GetRequiredFeats(featSelection.Feat);

            var featsWithSkillRequirements = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, GroupConstants.HasSkillRequirements);
            if (featsWithSkillRequirements.Contains(dataKVP.Key))
            {
                featSelection.RequiredSkills = GetRequiredSkills(featSelection.Feat);
            }

            var featsWithStatRequirements = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, GroupConstants.HasAbilityRequirements);
            if (featsWithStatRequirements.Contains(dataKVP.Key))
            {
                var tableName = string.Format(TableNameConstants.Formattable.Adjustments.FEATAbilityRequirements, dataKVP.Key);
                featSelection.RequiredAbilities = adjustmentsSelector.SelectAllFrom(tableName);
            }

            var featsTakenMultipleTimes = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, GroupConstants.TakenMultipleTimes);
            featSelection.CanBeTakenMultipleTimes = featsTakenMultipleTimes.Contains(featSelection.Feat);

            return featSelection;
        }

        private IEnumerable<RequiredSkillSelection> GetRequiredSkills(string feat)
        {
            var tableName = string.Format(TableNameConstants.Formattable.Adjustments.FEATSkillRankRequirements, feat);
            var requiredSkillsAndRanks = adjustmentsSelector.SelectAllFrom(tableName);
            var requiredSkillSelections = new List<RequiredSkillSelection>();

            foreach (var kvp in requiredSkillsAndRanks)
            {
                var splitData = kvp.Key.Split('/');
                var requiredSkill = new RequiredSkillSelection();
                requiredSkill.Skill = splitData[0];
                requiredSkill.Ranks = kvp.Value;

                if (splitData.Length > 1)
                    requiredSkill.Focus = splitData[1];

                requiredSkillSelections.Add(requiredSkill);
            }

            return requiredSkillSelections;
        }

        public IEnumerable<SpecialQualitySelection> SelectSpecialQualities(string creature)
        {
            var tableName = string.Format(TableNameConstants.Formattable.Collection.CREATURESpecialQualityData, creature);
            var featData = collectionsSelector.SelectAllFrom(tableName);
            var racialFeatSelections = new List<SpecialQualitySelection>();

            foreach (var dataKVP in featData)
            {
                //INFO: Calling ToArray so we can access indices
                var data = dataKVP.Value.ToArray();

                var racialFeatSelection = new SpecialQualitySelection();
                racialFeatSelection.Feat = data[DataIndexConstants.SpecialQualityData.FeatNameIndex];
                racialFeatSelection.SizeRequirement = data[DataIndexConstants.SpecialQualityData.SizeRequirementIndex];
                racialFeatSelection.Power = Convert.ToInt32(data[DataIndexConstants.SpecialQualityData.PowerIndex]);
                racialFeatSelection.MinimumHitDieRequirement = Convert.ToInt32(data[DataIndexConstants.SpecialQualityData.MinimumHitDiceRequirementIndex]);
                racialFeatSelection.FocusType = data[DataIndexConstants.SpecialQualityData.FocusIndex];
                racialFeatSelection.Frequency.Quantity = Convert.ToInt32(data[DataIndexConstants.SpecialQualityData.FrequencyQuantityIndex]);
                racialFeatSelection.Frequency.TimePeriod = data[DataIndexConstants.SpecialQualityData.FrequencyTimePeriodIndex];
                racialFeatSelection.MaximumHitDieRequirement = Convert.ToInt32(data[DataIndexConstants.SpecialQualityData.MaximumHitDiceRequirementIndex]);
                racialFeatSelection.RandomFociQuantity = data[DataIndexConstants.SpecialQualityData.RandomFociQuantity];
                racialFeatSelection.RequiredFeats = GetRequiredFeats(dataKVP.Key);

                var statNames = data[DataIndexConstants.SpecialQualityData.RequiredStatIndex].Split(',');
                var statValue = Convert.ToInt32(data[DataIndexConstants.SpecialQualityData.RequiredStatMinimumValueIndex]);

                if (string.IsNullOrEmpty(statNames[0]) == false)
                    for (var i = 0; i < statNames.Length; i++)
                        racialFeatSelection.MinimumAbilities[statNames[i]] = statValue;

                racialFeatSelections.Add(racialFeatSelection);
            }

            return racialFeatSelections;
        }

        private IEnumerable<RequiredFeatSelection> GetRequiredFeats(string feat)
        {
            var allRequiredFeats = collectionsSelector.SelectAllFrom(TableNameConstants.Set.Collection.RequiredFeats);
            var requiredFeats = new List<RequiredFeatSelection>();

            if (allRequiredFeats.ContainsKey(feat) == false)
                return requiredFeats;

            var requiredFeatsData = allRequiredFeats[feat];

            foreach (var requiredFeatData in requiredFeatsData)
            {
                var splitData = requiredFeatData.Split('/');
                var requiredFeat = new RequiredFeatSelection();
                requiredFeat.Feat = splitData[0];

                if (splitData.Length > 1)
                    requiredFeat.Focus = splitData[1];

                requiredFeats.Add(requiredFeat);
            }

            return requiredFeats;
        }
    }
}