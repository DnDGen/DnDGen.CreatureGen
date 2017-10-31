using CreatureGen.Tables;
using System;
using System.Collections.Generic;

namespace CreatureGen.Tests.Integration.Tables.Feats.Data.Racial
{
    public abstract class RacialFeatDataTests : DataTests
    {
        protected override void PopulateIndices(IEnumerable<string> collection)
        {
            indices[DataIndexConstants.RacialFeatData.FeatNameIndex] = "Feat Name";
            indices[DataIndexConstants.RacialFeatData.FocusIndex] = "Focus Type";
            indices[DataIndexConstants.RacialFeatData.FrequencyQuantityIndex] = "Frequency Quantity";
            indices[DataIndexConstants.RacialFeatData.FrequencyTimePeriodIndex] = "Frequency Time Period";
            indices[DataIndexConstants.RacialFeatData.MinimumHitDiceRequirementIndex] = "Min Hit Dice Requirement";
            indices[DataIndexConstants.RacialFeatData.SizeRequirementIndex] = "Size Requirement";
            indices[DataIndexConstants.RacialFeatData.PowerIndex] = "Power";
            indices[DataIndexConstants.RacialFeatData.MaximumHitDiceRequirementIndex] = "Max Hit Dice Requirement";
            indices[DataIndexConstants.RacialFeatData.RequiredStatIndex] = "Required Stat";
            indices[DataIndexConstants.RacialFeatData.RequiredStatMinimumValueIndex] = "Required Stat Minimum Value";
            indices[DataIndexConstants.RacialFeatData.RandomFociQuantity] = "Random Foci Quantity";
        }

        public virtual void RacialFeatData(string name, string feat, string focus, int frequencyQuantity, string frequencyTimePeriod, int minimumHitDiceRequirement, string sizeRequirement, int power, int maximumHitDiceRequirement, int requiredStatMinimumValue, params string[] minimumAbilities)
        {
            RacialFeatData(name, feat, focus, frequencyQuantity, frequencyTimePeriod, minimumHitDiceRequirement, sizeRequirement, power, maximumHitDiceRequirement, string.Empty, requiredStatMinimumValue, minimumAbilities);
        }

        public void RacialFeatData(string name, string feat, string focus, int frequencyQuantity, string frequencyTimePeriod, int minimumHitDiceRequirement, string sizeRequirement, int power, int maximumHitDiceRequirement, string randomFociQuantity, int requiredStatMinimumValue, params string[] minimumAbilities)
        {
            var data = new List<string>();
            for (var i = 0; i < 11; i++)
                data.Add(string.Empty);

            var requiredStat = string.Join(",", minimumAbilities);

            data[DataIndexConstants.RacialFeatData.FeatNameIndex] = feat;
            data[DataIndexConstants.RacialFeatData.FocusIndex] = focus;
            data[DataIndexConstants.RacialFeatData.FrequencyQuantityIndex] = Convert.ToString(frequencyQuantity);
            data[DataIndexConstants.RacialFeatData.MinimumHitDiceRequirementIndex] = Convert.ToString(minimumHitDiceRequirement);
            data[DataIndexConstants.RacialFeatData.FrequencyTimePeriodIndex] = frequencyTimePeriod;
            data[DataIndexConstants.RacialFeatData.SizeRequirementIndex] = sizeRequirement;
            data[DataIndexConstants.RacialFeatData.PowerIndex] = Convert.ToString(power);
            data[DataIndexConstants.RacialFeatData.MaximumHitDiceRequirementIndex] = Convert.ToString(maximumHitDiceRequirement);
            data[DataIndexConstants.RacialFeatData.RequiredStatIndex] = requiredStat;
            data[DataIndexConstants.RacialFeatData.RequiredStatMinimumValueIndex] = Convert.ToString(requiredStatMinimumValue);
            data[DataIndexConstants.RacialFeatData.RandomFociQuantity] = randomFociQuantity;

            Data(name, data);
        }
    }
}
