using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Selectors.Helpers;
using DnDGen.CreatureGen.Selectors.Selections;
using DnDGen.CreatureGen.Skills;
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
        private readonly ITypeAndAmountSelector typeAndAmountSelector;
        private readonly SpecialQualityHelper helper;

        public FeatsSelector(ICollectionSelector collectionsSelector, ITypeAndAmountSelector typeAndAmountSelector)
        {
            this.collectionsSelector = collectionsSelector;
            this.typeAndAmountSelector = typeAndAmountSelector;

            helper = new SpecialQualityHelper();
        }

        public IEnumerable<FeatSelection> SelectFeats()
        {
            var featData = collectionsSelector.SelectAllFrom(TableNameConstants.Collection.FeatData);
            var featSelections = new List<FeatSelection>();

            var featsTakenMultipleTimes = collectionsSelector.SelectFrom(TableNameConstants.Collection.FeatGroups, GroupConstants.TakenMultipleTimes);
            var requiredAbilities = collectionsSelector.SelectFrom(TableNameConstants.Collection.FeatGroups, TableNameConstants.TypeAndAmount.FeatAbilityRequirements);
            var requiredSpeeds = collectionsSelector.SelectFrom(TableNameConstants.Collection.FeatGroups, TableNameConstants.TypeAndAmount.FeatSpeedRequirements);
            var requiredSkills = collectionsSelector.SelectFrom(TableNameConstants.Collection.FeatGroups, TableNameConstants.TypeAndAmount.FeatSkillRankRequirements);
            var requiredFeats = collectionsSelector.SelectFrom(TableNameConstants.Collection.FeatGroups, TableNameConstants.Collection.RequiredFeats);
            var requiredSizes = collectionsSelector.SelectFrom(TableNameConstants.Collection.FeatGroups, TableNameConstants.Collection.RequiredSizes);

            foreach (var dataKVP in featData)
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
                featSelection.RequiresNaturalArmor = Convert.ToBoolean(data[DataIndexConstants.FeatData.RequiresNaturalArmorIndex]);
                featSelection.RequiresSpecialAttack = Convert.ToBoolean(data[DataIndexConstants.FeatData.RequiresSpecialAttackIndex]);
                featSelection.RequiresSpellLikeAbility = Convert.ToBoolean(data[DataIndexConstants.FeatData.RequiresSpellLikeAbilityIndex]);
                featSelection.RequiresEquipment = Convert.ToBoolean(data[DataIndexConstants.FeatData.RequiresEquipmentIndex]);

                featSelection.RequiredFeats = GetRequiredFeats(requiredFeats, featSelection.Feat);
                featSelection.RequiredSkills = GetRequiredSkills(requiredSkills, featSelection.Feat);
                featSelection.RequiredAbilities = GetRequiredAbilities(requiredAbilities, featSelection.Feat);
                featSelection.RequiredSpeeds = GetRequiredSpeeds(requiredSpeeds, featSelection.Feat);
                featSelection.RequiredSizes = GetRequiredSizes(requiredSizes, featSelection.Feat);

                featSelection.CanBeTakenMultipleTimes = featsTakenMultipleTimes.Contains(featSelection.Feat);

                featSelections.Add(featSelection);
            }

            return featSelections;
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

        private Dictionary<string, int> GetRequiredSpeeds(IEnumerable<string> requiresSpeeds, string feat)
        {
            var requiredSpeeds = new Dictionary<string, int>();
            if (!requiresSpeeds.Contains(feat))
                return requiredSpeeds;

            var requiredSpeedsAndValues = typeAndAmountSelector.Select(TableNameConstants.TypeAndAmount.FeatSpeedRequirements, feat);

            foreach (var selection in requiredSpeedsAndValues)
            {
                requiredSpeeds[selection.Type] = selection.Amount;
            }

            return requiredSpeeds;
        }

        private IEnumerable<RequiredSkillSelection> GetRequiredSkills(IEnumerable<string> requiresSkills, string feat)
        {
            var requiredSkillSelections = new List<RequiredSkillSelection>();
            if (!requiresSkills.Contains(feat))
                return requiredSkillSelections;

            var requiredSkillsAndRanks = typeAndAmountSelector.Select(TableNameConstants.TypeAndAmount.FeatSkillRankRequirements, feat);

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
            var usedSpecialQualities = new HashSet<string>();

            var requiredAlignments = collectionsSelector.SelectFrom(TableNameConstants.Collection.FeatGroups, TableNameConstants.Collection.RequiredAlignments);
            var requiredSizes = collectionsSelector.SelectFrom(TableNameConstants.Collection.FeatGroups, TableNameConstants.Collection.RequiredSizes);
            var requiredFeats = collectionsSelector.SelectFrom(TableNameConstants.Collection.FeatGroups, TableNameConstants.Collection.RequiredFeats);
            var requiredAbilities = collectionsSelector.SelectFrom(TableNameConstants.Collection.FeatGroups, TableNameConstants.TypeAndAmount.FeatAbilityRequirements);

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

                    var specialQualitySelection = new SpecialQualitySelection();
                    specialQualitySelection.Feat = data[DataIndexConstants.SpecialQualityData.FeatNameIndex];
                    specialQualitySelection.Power = Convert.ToInt32(data[DataIndexConstants.SpecialQualityData.PowerIndex]);
                    specialQualitySelection.FocusType = data[DataIndexConstants.SpecialQualityData.FocusIndex];
                    specialQualitySelection.Frequency.Quantity = Convert.ToInt32(data[DataIndexConstants.SpecialQualityData.FrequencyQuantityIndex]);
                    specialQualitySelection.Frequency.TimePeriod = data[DataIndexConstants.SpecialQualityData.FrequencyTimePeriodIndex];
                    specialQualitySelection.RandomFociQuantity = data[DataIndexConstants.SpecialQualityData.RandomFociQuantityIndex];
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

            return collectionsSelector.SelectFrom(TableNameConstants.Collection.RequiredSizes, feat);
        }

        private IEnumerable<string> GetRequiredAlignments(IEnumerable<string> requiresAlignments, string feat)
        {
            if (!requiresAlignments.Contains(feat))
                return Enumerable.Empty<string>();

            return collectionsSelector.SelectFrom(TableNameConstants.Collection.RequiredAlignments, feat);
        }

        private IEnumerable<RequiredFeatSelection> GetRequiredFeats(IEnumerable<string> requiresFeats, string feat)
        {
            var requiredFeatsSelections = new List<RequiredFeatSelection>();
            if (!requiresFeats.Contains(feat))
                return requiredFeatsSelections;

            var requiredFeatsData = collectionsSelector.SelectFrom(TableNameConstants.Collection.RequiredFeats, feat);
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