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
            var specialQualities = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.SpecialQualityData, creature);
            if (!specialQualities.Any())
                return Enumerable.Empty<SpecialQualitySelection>();

            var specialQualitySelections = new List<SpecialQualitySelection>();

            foreach (var specialQuality in specialQualities)
            {
                var data = specialQuality.Split('/');

                var specialQualitySelection = new SpecialQualitySelection();
                specialQualitySelection.Feat = data[DataIndexConstants.SpecialQualityData.FeatNameIndex];
                specialQualitySelection.SizeRequirement = data[DataIndexConstants.SpecialQualityData.SizeRequirementIndex];
                specialQualitySelection.Power = Convert.ToInt32(data[DataIndexConstants.SpecialQualityData.PowerIndex]);
                specialQualitySelection.MinimumHitDieRequirement = Convert.ToInt32(data[DataIndexConstants.SpecialQualityData.MinimumHitDiceRequirementIndex]);
                specialQualitySelection.FocusType = data[DataIndexConstants.SpecialQualityData.FocusIndex];
                specialQualitySelection.Frequency.Quantity = Convert.ToInt32(data[DataIndexConstants.SpecialQualityData.FrequencyQuantityIndex]);
                specialQualitySelection.Frequency.TimePeriod = data[DataIndexConstants.SpecialQualityData.FrequencyTimePeriodIndex];
                specialQualitySelection.MaximumHitDieRequirement = Convert.ToInt32(data[DataIndexConstants.SpecialQualityData.MaximumHitDiceRequirementIndex]);
                specialQualitySelection.RandomFociQuantity = data[DataIndexConstants.SpecialQualityData.RandomFociQuantity];
                specialQualitySelection.RequiredFeats = GetRequiredFeats(specialQualitySelection.Feat);

                var abilityNames = data[DataIndexConstants.SpecialQualityData.RequiredAbilityIndex].Split(',');
                var abilityValue = Convert.ToInt32(data[DataIndexConstants.SpecialQualityData.RequiredAbilityMinimumValueIndex]);

                if (!string.IsNullOrEmpty(abilityNames[0]))
                    for (var i = 0; i < abilityNames.Length; i++)
                        specialQualitySelection.MinimumAbilities[abilityNames[i]] = abilityValue;

                specialQualitySelections.Add(specialQualitySelection);
            }

            return specialQualitySelections;
        }

        private IEnumerable<RequiredFeatSelection> GetRequiredFeats(string feat)
        {
            var allRequiredFeats = collectionsSelector.SelectAllFrom(TableNameConstants.Set.Collection.RequiredFeats);
            var requiredFeats = new List<RequiredFeatSelection>();

            if (!allRequiredFeats.ContainsKey(feat))
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