using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Selectors.Helpers;
using DnDGen.CreatureGen.Selectors.Selections;
using DnDGen.CreatureGen.Tables;
using DnDGen.Infrastructure.Selectors.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Selectors.Collections
{
    internal class FeatsSelector : IFeatsSelector
    {
        private readonly ICollectionSelector collectionsSelector;
        private readonly ICollectionDataSelector<FeatDataSelection> featDataSelector;
        private readonly ITypeAndAmountSelector typeAndAmountSelector;
        private readonly SpecialQualityHelper helper;

        public FeatsSelector(ICollectionSelector collectionsSelector, ITypeAndAmountSelector typeAndAmountSelector, ICollectionDataSelector<FeatDataSelection> featDataSelector)
        {
            this.collectionsSelector = collectionsSelector;
            this.featDataSelector = featDataSelector;
            this.typeAndAmountSelector = typeAndAmountSelector;

            helper = new SpecialQualityHelper();
        }

        public IEnumerable<FeatDataSelection> SelectFeats()
        {
            var featData = featDataSelector.SelectAllFrom(Config.Name, TableNameConstants.Collection.FeatData);
            return featData.Values.Select(d => d.Single());
        }

        private Dictionary<string, int> GetRequiredAbilities(IEnumerable<string> requiresAbilities, string feat)
        {
            var requiredAbilities = new Dictionary<string, int>();
            if (!requiresAbilities.Contains(feat))
                return requiredAbilities;

            var requiredAbilitiesAndValues = typeAndAmountSelector.Select(TableNameConstants.TypeAndAmount.FeatAbilityRequirements, feat);

            foreach (var selection in requiredAbilitiesAndValues)
            {
                requiredAbilities[selection.Type] = selection.Amount;
            }

            return requiredAbilities;
        }

        public IEnumerable<SpecialQualityDataSelection> SelectSpecialQualities(string creature, CreatureType creatureType)
        {
            var specialQualitiesWithSource = new Dictionary<string, IEnumerable<string>>();

            specialQualitiesWithSource[creature] = collectionsSelector.SelectFrom(Config.Name, TableNameConstants.Collection.SpecialQualityData, creature);
            specialQualitiesWithSource[creatureType.Name] = collectionsSelector.SelectFrom(Config.Name, TableNameConstants.Collection.SpecialQualityData, creatureType.Name);

            foreach (var subtype in creatureType.SubTypes)
            {
                specialQualitiesWithSource[subtype] = collectionsSelector.SelectFrom(Config.Name, TableNameConstants.Collection.SpecialQualityData, subtype);
            }

            if (!specialQualitiesWithSource.SelectMany(kvp => kvp.Value).Any())
                return Enumerable.Empty<SpecialQualityDataSelection>();

            var specialQualitySelections = new List<SpecialQualityDataSelection>();
            var usedSpecialQualities = new HashSet<string>();

            var requiredAlignments = collectionsSelector.SelectFrom(Config.Name, TableNameConstants.Collection.FeatGroups, TableNameConstants.Collection.RequiredAlignments);
            var requiredSizes = collectionsSelector.SelectFrom(Config.Name, TableNameConstants.Collection.FeatGroups, TableNameConstants.Collection.RequiredSizes);
            var requiredFeats = collectionsSelector.SelectFrom(Config.Name, TableNameConstants.Collection.FeatGroups, TableNameConstants.Collection.RequiredFeats);
            var requiredAbilities = collectionsSelector.SelectFrom(Config.Name, TableNameConstants.Collection.FeatGroups, TableNameConstants.TypeAndAmount.FeatAbilityRequirements);

            foreach (var specialQualityKvp in specialQualitiesWithSource)
            {
                var source = specialQualityKvp.Key;

                foreach (var specialQualityData in specialQualityKvp.Value)
                {
                    if (usedSpecialQualities.Contains(specialQualityData))
                    {
                        continue;
                    }

                    var data = helper.ParseEntry(specialQualityData);

                    var specialQualitySelection = new SpecialQualityDataSelection();
                    specialQualitySelection.Feat = data[DataIndexConstants.SpecialQualityData.FeatNameIndex];
                    specialQualitySelection.Power = Convert.ToInt32(data[DataIndexConstants.SpecialQualityData.PowerIndex]);
                    specialQualitySelection.FocusType = data[DataIndexConstants.SpecialQualityData.FocusIndex];
                    specialQualitySelection.Frequency.Quantity = Convert.ToInt32(data[DataIndexConstants.SpecialQualityData.FrequencyQuantityIndex]);
                    specialQualitySelection.Frequency.TimePeriod = data[DataIndexConstants.SpecialQualityData.FrequencyTimePeriodIndex];
                    specialQualitySelection.RandomFociQuantityRoll = data[DataIndexConstants.SpecialQualityData.RandomFociQuantityIndex];
                    specialQualitySelection.RequiresEquipment = Convert.ToBoolean(data[DataIndexConstants.SpecialQualityData.RequiresEquipmentIndex]);
                    specialQualitySelection.Save = data[DataIndexConstants.SpecialQualityData.SaveIndex];
                    specialQualitySelection.SaveAbility = data[DataIndexConstants.SpecialQualityData.SaveAbilityIndex];
                    specialQualitySelection.SaveBaseValue = Convert.ToInt32(data[DataIndexConstants.SpecialQualityData.SaveBaseValueIndex]);
                    specialQualitySelection.MinHitDice = Convert.ToInt32(data[DataIndexConstants.SpecialQualityData.MinHitDiceIndex]);
                    specialQualitySelection.MaxHitDice = Convert.ToInt32(data[DataIndexConstants.SpecialQualityData.MaxHitDiceIndex]);

                    var requirementKey = helper.BuildKey(source, data);
                    specialQualitySelection.RequiredFeats = GetRequiredFeats(requiredFeats, requirementKey);
                    specialQualitySelection.MinimumAbilities = GetRequiredAbilities(requiredAbilities, requirementKey);
                    specialQualitySelection.RequiredSizes = GetRequiredSizes(requiredSizes, requirementKey);
                    specialQualitySelection.RequiredAlignments = GetRequiredAlignments(requiredAlignments, requirementKey);

                    specialQualitySelections.Add(specialQualitySelection);
                }

                usedSpecialQualities.UnionWith(specialQualityKvp.Value);
            }

            return specialQualitySelections;
        }

        private IEnumerable<string> GetRequiredSizes(IEnumerable<string> requiresSizes, string feat)
        {
            if (!requiresSizes.Contains(feat))
                return Enumerable.Empty<string>();

            return collectionsSelector.SelectFrom(Config.Name, TableNameConstants.Collection.RequiredSizes, feat);
        }

        private IEnumerable<string> GetRequiredAlignments(IEnumerable<string> requiresAlignments, string feat)
        {
            if (!requiresAlignments.Contains(feat))
                return Enumerable.Empty<string>();

            return collectionsSelector.SelectFrom(Config.Name, TableNameConstants.Collection.RequiredAlignments, feat);
        }

        private IEnumerable<RequiredFeatSelection> GetRequiredFeats(IEnumerable<string> requiresFeats, string feat)
        {
            var requiredFeatsSelections = new List<RequiredFeatSelection>();
            if (!requiresFeats.Contains(feat))
                return requiredFeatsSelections;

            var requiredFeatsData = collectionsSelector.SelectFrom(Config.Name, TableNameConstants.Collection.RequiredFeats, feat);
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