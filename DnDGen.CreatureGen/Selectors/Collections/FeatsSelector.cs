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
            Console.WriteLine($"[{DateTime.Now:O}] FeatsSelector: Selecting feat data");
            var featData = collectionsSelector.SelectAllFrom(TableNameConstants.Collection.FeatData);
            var featSelections = new List<FeatSelection>();

            Console.WriteLine($"[{DateTime.Now:O}] FeatsSelector: Selecting feats that can be selected multiple times");
            var featsTakenMultipleTimes = collectionsSelector.SelectFrom(TableNameConstants.Collection.FeatGroups, GroupConstants.TakenMultipleTimes);

            Console.WriteLine($"[{DateTime.Now:O}] FeatsSelector: Selecting all ability requirements");
            var requiredAbilities = typeAndAmountSelector.SelectAll(TableNameConstants.TypeAndAmount.FeatAbilityRequirements);

            Console.WriteLine($"[{DateTime.Now:O}] FeatsSelector: Selecting all speed requirements");
            var requiredSpeeds = typeAndAmountSelector.SelectAll(TableNameConstants.TypeAndAmount.FeatSpeedRequirements);

            Console.WriteLine($"[{DateTime.Now:O}] FeatsSelector: Selecting all skill and rank requirements");
            var requiredSkills = typeAndAmountSelector.SelectAll(TableNameConstants.TypeAndAmount.FeatSkillRankRequirements);

            Console.WriteLine($"[{DateTime.Now:O}] FeatsSelector: Selecting all feat requirements");
            var requiredFeatsData = collectionsSelector.SelectAllFrom(TableNameConstants.Collection.RequiredFeats);

            Console.WriteLine($"[{DateTime.Now:O}] FeatsSelector: Selecting all size requirements");
            var requiredSizes = collectionsSelector.SelectAllFrom(TableNameConstants.Collection.RequiredSizes);

            Console.WriteLine($"[{DateTime.Now:O}] FeatsSelector: Parsing feat data for {featData.Count} feats");
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

                featSelection.RequiredFeats = GetRequiredFeats(requiredFeatsData[featSelection.Feat]);
                featSelection.RequiredSkills = GetRequiredSkills(requiredSkills[featSelection.Feat]);
                featSelection.RequiredAbilities = GetRequiredAbilities(requiredAbilities[featSelection.Feat]);
                featSelection.RequiredSpeeds = GetRequiredSpeeds(requiredSpeeds[featSelection.Feat]);
                featSelection.RequiredSizes = requiredSizes[featSelection.Feat];

                featSelection.CanBeTakenMultipleTimes = featsTakenMultipleTimes.Contains(featSelection.Feat);

                featSelections.Add(featSelection);
            }

            Console.WriteLine($"[{DateTime.Now:O}] FeatsSelector: Selected {featSelections.Count} feat selections");
            return featSelections;
        }

        private Dictionary<string, int> GetRequiredAbilities(IEnumerable<TypeAndAmountSelection> requiredAbilitiesAndValues)
        {
            var requiredAbilities = new Dictionary<string, int>();

            foreach (var selection in requiredAbilitiesAndValues)
            {
                requiredAbilities[selection.Type] = selection.Amount;
            }

            return requiredAbilities;
        }

        private Dictionary<string, int> GetRequiredSpeeds(IEnumerable<TypeAndAmountSelection> requiredSpeedsAndValues)
        {
            var requiredSpeeds = new Dictionary<string, int>();

            foreach (var selection in requiredSpeedsAndValues)
            {
                requiredSpeeds[selection.Type] = selection.Amount;
            }

            return requiredSpeeds;
        }

        private IEnumerable<RequiredSkillSelection> GetRequiredSkills(IEnumerable<TypeAndAmountSelection> requiredSkillsAndRanks)
        {
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

            Console.WriteLine($"[{DateTime.Now:O}] FeatsSelector: Getting special quality data for {creature}");
            specialQualitiesWithSource[creature] = collectionsSelector.SelectFrom(TableNameConstants.Collection.SpecialQualityData, creature);

            Console.WriteLine($"[{DateTime.Now:O}] FeatsSelector: Getting special quality data for {creatureType.Name}");
            specialQualitiesWithSource[creatureType.Name] = collectionsSelector.SelectFrom(TableNameConstants.Collection.SpecialQualityData, creatureType.Name);

            foreach (var subtype in creatureType.SubTypes)
            {
                Console.WriteLine($"[{DateTime.Now:O}] FeatsSelector: Getting special quality data for {subtype}");
                specialQualitiesWithSource[subtype] = collectionsSelector.SelectFrom(TableNameConstants.Collection.SpecialQualityData, subtype);
            }

            if (!specialQualitiesWithSource.SelectMany(kvp => kvp.Value).Any())
                return Enumerable.Empty<SpecialQualitySelection>();

            var specialQualitySelections = new List<SpecialQualitySelection>();
            var usedSpecialQualities = new HashSet<string>();

            Console.WriteLine($"[{DateTime.Now:O}] FeatsSelector: Selecting all alignment requirements");
            var requiredAlignments = collectionsSelector.SelectAllFrom(TableNameConstants.Collection.RequiredAlignments);

            Console.WriteLine($"[{DateTime.Now:O}] FeatsSelector: Selecting all size requirements");
            var requiredSizes = collectionsSelector.SelectAllFrom(TableNameConstants.Collection.RequiredSizes);

            Console.WriteLine($"[{DateTime.Now:O}] FeatsSelector: Selecting all feat requirements");
            var requiredFeatsData = collectionsSelector.SelectAllFrom(TableNameConstants.Collection.RequiredFeats);

            Console.WriteLine($"[{DateTime.Now:O}] FeatsSelector: Selecting all ability requirements");
            var requiredAbilities = typeAndAmountSelector.SelectAll(TableNameConstants.TypeAndAmount.FeatAbilityRequirements);

            foreach (var specialQualityKvp in specialQualitiesWithSource)
            {
                var source = specialQualityKvp.Key;
                Console.WriteLine($"[{DateTime.Now:O}] FeatsSelector: Parsing special quality data for {source}");

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
                    specialQualitySelection.RequiredFeats = GetRequiredFeats(requiredFeatsData[requirementKey]);
                    specialQualitySelection.MinimumAbilities = GetRequiredAbilities(requiredAbilities[requirementKey]);
                    specialQualitySelection.RequiredSizes = requiredSizes[requirementKey];
                    specialQualitySelection.RequiredAlignments = requiredAlignments[requirementKey];

                    specialQualitySelections.Add(specialQualitySelection);
                }

                usedSpecialQualities.UnionWith(specialQualityKvp.Value);
            }

            return specialQualitySelections;
        }

        private IEnumerable<RequiredFeatSelection> GetRequiredFeats(IEnumerable<string> requiredFeatsData)
        {
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