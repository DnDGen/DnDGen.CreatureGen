using CreatureGen.Tables;
using System;

namespace CreatureGen.Selectors.Helpers
{
    public static class SpecialQualityHelper
    {
        public static string[] BuildData(
            string featName,
            string focus = "",
            int frequencyQuantity = 0,
            string frequencyTimePeriod = "",
            int power = 0,
            int randomFociQuantity = 0,
            bool requiresEquipment = false,
            string saveAbility = "",
            string save = "",
            int saveBaseValue = 0)
        {
            return BuildData(featName, randomFociQuantity.ToString(), focus, frequencyQuantity, frequencyTimePeriod, power, requiresEquipment, saveAbility, save, saveBaseValue);
        }

        public static string[] BuildData(
            string featName,
            string randomFociQuantity,
            string focus = "",
            int frequencyQuantity = 0,
            string frequencyTimePeriod = "",
            int power = 0,
            bool requiresEquipment = false,
            string saveAbility = "",
            string save = "",
            int saveBaseValue = 0)
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

            return data;
        }

        public static string BuildRequirementKey(string creature, string[] data)
        {
            return BuildRequirementKey(creature, data[DataIndexConstants.SpecialQualityData.FeatNameIndex], data[DataIndexConstants.SpecialQualityData.FocusIndex]);
        }

        public static string BuildRequirementKey(string creature, string featName, string focus)
        {
            return $"{creature}{featName}{focus}";
        }

        public static string BuildRequirementKey(string creature, string input)
        {
            var data = ParseData(input);
            return BuildRequirementKey(creature, data);
        }

        public static string BuildData(string[] data)
        {
            return string.Join("#", data);
        }

        public static string[] ParseData(string input)
        {
            return input.Split('#');
        }
    }
}
