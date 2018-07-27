using CreatureGen.Tables;
using System;

namespace CreatureGen.Selectors.Helpers
{
    public static class SpecialQualityHelper
    {
        public static string[] BuildData(string featName, string focus = "", int frequencyQuantity = 0, string frequencyTimePeriod = "", int power = 0, int randomFociQuantity = 0)
        {
            return BuildData(featName, randomFociQuantity.ToString(), focus, frequencyQuantity, frequencyTimePeriod, power);
        }

        public static string[] BuildData(string featName, string randomFociQuantity, string focus = "", int frequencyQuantity = 0, string frequencyTimePeriod = "", int power = 0)
        {
            var data = DataIndexConstants.SpecialQualityData.InitializeData();

            data[DataIndexConstants.SpecialQualityData.FeatNameIndex] = featName;
            data[DataIndexConstants.SpecialQualityData.FocusIndex] = focus;
            data[DataIndexConstants.SpecialQualityData.FrequencyQuantityIndex] = Convert.ToString(frequencyQuantity);
            data[DataIndexConstants.SpecialQualityData.FrequencyTimePeriodIndex] = frequencyTimePeriod;
            data[DataIndexConstants.SpecialQualityData.PowerIndex] = Convert.ToString(power);
            data[DataIndexConstants.SpecialQualityData.RandomFociQuantityIndex] = Convert.ToString(randomFociQuantity);

            return data;
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
