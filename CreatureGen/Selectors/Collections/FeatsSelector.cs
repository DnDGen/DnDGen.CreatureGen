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
        private readonly ITypeAndAmountSelector typeAndAmountSelector;

        public FeatsSelector(ICollectionSelector collectionsSelector, ITypeAndAmountSelector typeAndAmountSelector)
        {
            this.collectionsSelector = collectionsSelector;
            this.typeAndAmountSelector = typeAndAmountSelector;
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
            featSelection.RequiredSkills = GetRequiredSkills(featSelection.Feat);
            featSelection.RequiredAbilities = GetRequiredAbilities(featSelection.Feat);

            var featsTakenMultipleTimes = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.FeatGroups, GroupConstants.TakenMultipleTimes);
            featSelection.CanBeTakenMultipleTimes = featsTakenMultipleTimes.Contains(featSelection.Feat);

            return featSelection;
        }

        private Dictionary<string, int> GetRequiredAbilities(string feat)
        {
            var requiredAbilitiesAndValues = typeAndAmountSelector.Select(TableNameConstants.Set.TypeAndAmount.FeatAbilityRequirements, feat);
            var requiredAbilities = new Dictionary<string, int>();

            foreach (var selection in requiredAbilitiesAndValues)
            {
                requiredAbilities[selection.Type] = selection.Amount;
            }

            return requiredAbilities;
        }

        private IEnumerable<RequiredSkillSelection> GetRequiredSkills(string feat)
        {
            var requiredSkillsAndRanks = typeAndAmountSelector.Select(TableNameConstants.Set.TypeAndAmount.FeatSkillRankRequirements, feat);
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
            var splitData = selection.Type.Split('/');
            var requiredSkill = new RequiredSkillSelection();
            requiredSkill.Skill = splitData[0];
            requiredSkill.Ranks = selection.Amount;

            if (splitData.Length > 1)
                requiredSkill.Focus = splitData[1];

            return requiredSkill;
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
                specialQualitySelection.RequiredFeats = GetRequiredFeats(creature + specialQualitySelection.Feat);
                specialQualitySelection.MinimumAbilities = GetRequiredAbilities(creature + specialQualitySelection.Feat);

                specialQualitySelections.Add(specialQualitySelection);
            }

            return specialQualitySelections;
        }

        private IEnumerable<RequiredFeatSelection> GetRequiredFeats(string feat)
        {
            var requiredFeatsData = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.RequiredFeats, feat);
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
                requiredFeat.Focus = splitData[1];

            return requiredFeat;
        }
    }
}