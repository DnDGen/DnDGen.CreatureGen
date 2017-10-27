using CreatureGen.Domain.Tables;
using System;
using System.Collections.Generic;

namespace CreatureGen.Tests.Integration.Tables.Feats.Data.CharacterClasses
{
    public abstract class CharacterClassFeatDataTests : DataTests
    {
        protected override void PopulateIndices(IEnumerable<string> collection)
        {
            indices[DataIndexConstants.CharacterClassFeatData.FeatNameIndex] = "FeatId";
            indices[DataIndexConstants.CharacterClassFeatData.FocusTypeIndex] = "FocusType";
            indices[DataIndexConstants.CharacterClassFeatData.FrequencyQuantityIndex] = "FrequencyQuantity";
            indices[DataIndexConstants.CharacterClassFeatData.FrequencyQuantityStatIndex] = "FrequencyQuantityStat";
            indices[DataIndexConstants.CharacterClassFeatData.FrequencyTimePeriodIndex] = "FrequencyTimePeriod";
            indices[DataIndexConstants.CharacterClassFeatData.MaximumLevelRequirementIndex] = "MaxLevel";
            indices[DataIndexConstants.CharacterClassFeatData.MinimumLevelRequirementIndex] = "MinLevel";
            indices[DataIndexConstants.CharacterClassFeatData.PowerIndex] = "Power";
            indices[DataIndexConstants.CharacterClassFeatData.SizeRequirementIndex] = "SizeRequirement";
            indices[DataIndexConstants.CharacterClassFeatData.AllowFocusOfAllIndex] = "AllowFocusOfAll";
        }

        public virtual void ClassFeatData(string name, string feat, string focusType, int frequencyQuantity, string frequencyQuantityStat, string frequencyTimePeriod, int minimumLevel, int maximumLevel, int power, string sizeRequirement, bool allowFocusOfAll)
        {
            var data = new List<string>();
            for (var i = 0; i < 10; i++)
                data.Add(string.Empty);

            data[DataIndexConstants.CharacterClassFeatData.FeatNameIndex] = feat;
            data[DataIndexConstants.CharacterClassFeatData.FocusTypeIndex] = focusType;
            data[DataIndexConstants.CharacterClassFeatData.FrequencyQuantityIndex] = Convert.ToString(frequencyQuantity);
            data[DataIndexConstants.CharacterClassFeatData.FrequencyQuantityStatIndex] = frequencyQuantityStat;
            data[DataIndexConstants.CharacterClassFeatData.FrequencyTimePeriodIndex] = frequencyTimePeriod;
            data[DataIndexConstants.CharacterClassFeatData.MaximumLevelRequirementIndex] = Convert.ToString(maximumLevel);
            data[DataIndexConstants.CharacterClassFeatData.MinimumLevelRequirementIndex] = Convert.ToString(minimumLevel);
            data[DataIndexConstants.CharacterClassFeatData.PowerIndex] = Convert.ToString(power);
            data[DataIndexConstants.CharacterClassFeatData.SizeRequirementIndex] = sizeRequirement;
            data[DataIndexConstants.CharacterClassFeatData.AllowFocusOfAllIndex] = Convert.ToString(allowFocusOfAll);

            Data(name, data);
        }
    }
}
