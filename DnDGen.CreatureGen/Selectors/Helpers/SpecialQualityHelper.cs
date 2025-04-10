﻿using DnDGen.CreatureGen.Selectors.Selections;
using DnDGen.CreatureGen.Tables;
using System;

namespace DnDGen.CreatureGen.Selectors.Helpers
{
    public class SpecialQualityHelper : DataHelper
    {
        public SpecialQualityHelper()
            : base(SpecialQualityDataSelection.Divider)
        { }

        public string[] BuildData(
            string featName,
            string focus = "",
            int frequencyQuantity = 0,
            string frequencyTimePeriod = "",
            int power = 0,
            int randomFociQuantity = 0,
            bool requiresEquipment = false,
            string saveAbility = "",
            string save = "",
            int saveBaseValue = 0,
            int minHitDice = 0,
            int maxHitDice = int.MaxValue)
        {
            return BuildData(featName, randomFociQuantity.ToString(), focus, frequencyQuantity, frequencyTimePeriod, power, requiresEquipment, saveAbility, save, saveBaseValue, minHitDice, maxHitDice);
        }

        public string[] BuildData(
            string featName,
            string randomFociQuantity,
            string focus = "",
            int frequencyQuantity = 0,
            string frequencyTimePeriod = "",
            int power = 0,
            bool requiresEquipment = false,
            string saveAbility = "",
            string save = "",
            int saveBaseValue = 0,
            int minHitDice = 0,
            int maxHitDice = int.MaxValue)
        {
            var data = DataIndexConstants.SpecialQualityData.InitializeData();

            data[DataIndexConstants.SpecialQualityData.FeatNameIndex] = featName;
            data[DataIndexConstants.SpecialQualityData.FocusIndex] = focus;
            data[DataIndexConstants.SpecialQualityData.FrequencyQuantityIndex] = Convert.ToString(frequencyQuantity);
            data[DataIndexConstants.SpecialQualityData.FrequencyTimePeriodIndex] = frequencyTimePeriod;
            data[DataIndexConstants.SpecialQualityData.PowerIndex] = Convert.ToString(power);
            data[DataIndexConstants.SpecialQualityData.RandomFociQuantityIndex] = randomFociQuantity;
            data[DataIndexConstants.SpecialQualityData.RequiresEquipmentIndex] = Convert.ToString(requiresEquipment);
            data[DataIndexConstants.SpecialQualityData.SaveAbilityIndex] = saveAbility;
            data[DataIndexConstants.SpecialQualityData.SaveIndex] = save;
            data[DataIndexConstants.SpecialQualityData.SaveBaseValueIndex] = Convert.ToString(saveBaseValue);
            data[DataIndexConstants.SpecialQualityData.MinHitDiceIndex] = Convert.ToString(minHitDice);
            data[DataIndexConstants.SpecialQualityData.MaxHitDiceIndex] = Convert.ToString(maxHitDice);

            return data;
        }

        public override string BuildKey(string creature, string[] data)
        {
            return BuildKeyFromSections(creature,
                data[DataIndexConstants.SpecialQualityData.FeatNameIndex],
                data[DataIndexConstants.SpecialQualityData.FocusIndex],
                data[DataIndexConstants.SpecialQualityData.PowerIndex]);
        }

        public override bool ValidateEntry(string entry)
        {
            var data = ParseEntry(entry);
            var init = DataIndexConstants.SpecialQualityData.InitializeData();
            return data.Length == init.Length;
        }
    }
}
