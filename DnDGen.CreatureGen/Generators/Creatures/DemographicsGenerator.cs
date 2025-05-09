using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Tables;
using DnDGen.Infrastructure.Models;
using DnDGen.Infrastructure.Selectors.Collections;
using DnDGen.RollGen;
using System;
using System.Linq;

namespace DnDGen.CreatureGen.Generators.Creatures
{
    internal class DemographicsGenerator : IDemographicsGenerator
    {
        private readonly ICollectionSelector collectionsSelector;
        private readonly Dice dice;
        private readonly ICollectionTypeAndAmountSelector typeAndAmountSelector;
        private readonly string[] heightDescriptions;
        private readonly string[] lengthDescriptions;
        private readonly string[] wingspanDescriptions;
        private readonly string[] weightDescriptions;

        public DemographicsGenerator(ICollectionSelector collectionsSelector, Dice dice, ICollectionTypeAndAmountSelector typeAndAmountSelector)
        {
            this.collectionsSelector = collectionsSelector;
            this.dice = dice;
            this.typeAndAmountSelector = typeAndAmountSelector;

            heightDescriptions = ["Very Short", "Short", "Average", "Tall", "Very Tall"];
            lengthDescriptions = ["Very Short", "Short", "Average", "Long", "Very Long"];
            wingspanDescriptions = ["Very Narrow", "Narrow", "Average", "Broad", "Very Broad"];
            weightDescriptions = ["Very Light", "Light", "Average", "Heavy", "Very Heavy"];
        }

        public Demographics Generate(string creatureName)
        {
            var demographics = new Demographics
            {
                Gender = collectionsSelector.SelectRandomFrom(Config.Name, TableNameConstants.Collection.Genders, creatureName),
                Age = DetermineAge(creatureName),
            };
            demographics.MaximumAge = DetermineMaximumAge(creatureName, demographics.Age);

            var heights = typeAndAmountSelector.SelectFrom(Config.Name, TableNameConstants.TypeAndAmount.Heights, creatureName);
            var baseHeight = heights.First(h => h.Type == demographics.Gender);
            var heightModifier = heights.First(h => h.Type == creatureName);

            demographics.Height.Value = baseHeight.Amount + heightModifier.Amount;
            var rawHeightRoll = GetRoll(baseHeight.Roll, heightModifier.Roll);
            demographics.Height.Description = dice.Describe(rawHeightRoll, (int)demographics.Height.Value, heightDescriptions);

            var lengths = typeAndAmountSelector.SelectFrom(Config.Name, TableNameConstants.TypeAndAmount.Lengths, creatureName);
            var baseLength = lengths.First(h => h.Type == demographics.Gender);
            var lengthModifier = lengths.First(h => h.Type == creatureName);

            demographics.Length.Value = baseLength.Amount + lengthModifier.Amount;
            var rawLengthRoll = GetRoll(baseLength.Roll, lengthModifier.Roll);
            demographics.Length.Description = dice.Describe(rawLengthRoll, (int)demographics.Length.Value, lengthDescriptions);

            var weights = typeAndAmountSelector.SelectFrom(Config.Name, TableNameConstants.TypeAndAmount.Weights, creatureName);
            var baseWeight = weights.First(h => h.Type == demographics.Gender);
            var weightModifier = weights.First(h => h.Type == creatureName);
            var multiplier = Math.Max(heightModifier.Amount, lengthModifier.Amount);

            demographics.Weight.Value = baseWeight.Amount + multiplier * weightModifier.Amount;
            var rawWeightRoll = GetMultipliedRoll(baseWeight.Roll, multiplier, weightModifier.Roll);
            demographics.Weight.Description = dice.Describe(rawWeightRoll, (int)demographics.Weight.Value, weightDescriptions);

            demographics.Wingspan = GenerateWingspan(creatureName, demographics.Gender);
            demographics.Skin = GetRandomAppearance(creatureName, demographics.Gender, TableNameConstants.Collection.AppearanceCategories.Skin);
            demographics.Hair = GetRandomAppearance(creatureName, demographics.Gender, TableNameConstants.Collection.AppearanceCategories.Hair);
            demographics.Eyes = GetRandomAppearance(creatureName, demographics.Gender, TableNameConstants.Collection.AppearanceCategories.Eyes);
            demographics.Other = GetRandomAppearance(creatureName, demographics.Gender, TableNameConstants.Collection.AppearanceCategories.Other);

            return demographics;
        }

        private string GetRoll(string baseAmount, string modifier) => $"{baseAmount}+{modifier}";
        private string GetMultipliedRoll(string baseAmount, double multiplier, string modifier) => $"{baseAmount}+{multiplier}*{modifier}";

        private string GetRandomAppearance(string creatureName, string gender, string category)
        {
            var collectionName = creatureName;
            var tableName = TableNameConstants.Collection.Appearances(category);

            if (!string.IsNullOrEmpty(gender) && collectionsSelector.IsCollection(Config.Name, tableName, creatureName + gender + Rarity.Common.ToString()))
                collectionName += gender;

            var common = collectionsSelector.SelectFrom(Config.Name, tableName, collectionName + Rarity.Common.ToString());
            var uncommon = collectionsSelector.SelectFrom(Config.Name, tableName, collectionName + Rarity.Uncommon.ToString());
            var rare = collectionsSelector.SelectFrom(Config.Name, tableName, collectionName + Rarity.Rare.ToString());

            var appearance = collectionsSelector.SelectRandomFrom(common, uncommon, rare);
            return appearance;
        }

        private Measurement DetermineAge(string creatureName)
        {
            var ageRoll = GetRandomAgeRoll(creatureName);

            var age = new Measurement("years")
            {
                Value = ageRoll.Amount,
                Description = ageRoll.Type
            };

            var months = dice.Roll("(1d12-1)/12").AsSum<double>();
            age.Value += months;

            if (age.Value == 0)
                age.Value += 1 / 12d;

            return age;
        }

        private TypeAndAmountDataSelection GetRandomAgeRoll(string creatureName)
        {
            var ageRolls = typeAndAmountSelector.SelectFrom(Config.Name, TableNameConstants.TypeAndAmount.AgeRolls, creatureName);
            var nonCommonCategories = new[]
            {
                AgeConstants.Categories.MiddleAge,
                AgeConstants.Categories.Old,
                AgeConstants.Categories.Venerable,
                AgeConstants.Categories.Maximum
            };
            var common = ageRolls.Where(r => !nonCommonCategories.Contains(r.Type));
            var uncommon = ageRolls.Where(r => r.Type == AgeConstants.Categories.MiddleAge);
            var rare = ageRolls.Where(r => r.Type == AgeConstants.Categories.Old);
            var veryRare = ageRolls.Where(r => r.Type == AgeConstants.Categories.Venerable);

            var randomAgeRoll = collectionsSelector.SelectRandomFrom(common, uncommon, rare, veryRare);
            return randomAgeRoll;
        }

        private Measurement DetermineMaximumAge(string creatureName, Measurement age)
        {
            var maxAge = new Measurement("years")
            {
                Value = GetMaximumAge(creatureName)
            };

            if (age.Value > maxAge.Value)
                maxAge.Value = age.Value < 1 ? age.Value : Math.Floor(age.Value);

            var descriptions = collectionsSelector.SelectFrom(Config.Name, TableNameConstants.Collection.MaxAgeDescriptions, creatureName);
            maxAge.Description = descriptions.Single();

            return maxAge;
        }

        private int GetMaximumAge(string creatureName)
        {
            var ageRolls = typeAndAmountSelector.SelectFrom(Config.Name, TableNameConstants.TypeAndAmount.AgeRolls, creatureName);
            var maxAgeRoll = ageRolls.FirstOrDefault(r => r.Type == AgeConstants.Categories.Maximum);

            if (maxAgeRoll == null)
                return AgeConstants.Ageless;

            return maxAgeRoll.Amount;
        }

        private Measurement GenerateWingspan(string creatureName, string gender)
            => GenerateWingspan(TableNameConstants.TypeAndAmount.Wingspans, creatureName, gender);

        private Measurement GenerateWingspan(string tablename, string creatureName, string gender)
        {
            var wingspans = typeAndAmountSelector.SelectFrom(Config.Name, tablename, creatureName);
            var baseWingspan = wingspans.First(h => h.Type == gender);
            var wingspanModifier = wingspans.First(h => h.Type == creatureName);

            var wingspan = new Measurement("inches")
            {
                Value = baseWingspan.Amount + wingspanModifier.Amount
            };

            var rawWingspanRoll = GetRoll(baseWingspan.Roll, wingspanModifier.Roll);
            wingspan.Description = dice.Describe(rawWingspanRoll, (int)wingspan.Value, wingspanDescriptions);

            return wingspan;
        }

        private Measurement GenerateTemplateWingspan(Demographics source, string creatureName)
        {
            var tablename = TableNameConstants.TypeAndAmount.Heights;
            if (source.Length.Value > source.Height.Value)
                tablename = TableNameConstants.TypeAndAmount.Lengths;

            return GenerateWingspan(tablename, creatureName, source.Gender);
        }

        public static string GetAppearanceSeparator(string appearance)
        {
            if (string.IsNullOrWhiteSpace(appearance))
                return string.Empty;

            if (appearance.EndsWith('.'))
                return " ";

            return ". ";
        }

        public Demographics UpdateByTemplate(Demographics source, string creature, string template, bool addWingspan = false, bool overwriteAppearance = false)
        {
            UpdateAppearance(source, template, overwriteAppearance);
            UpdateAge(source, template);
            UpdateHeight(source, creature, template);
            UpdateLength(source, creature, template);
            UpdateWeight(source, creature, template);

            if (addWingspan && source.Wingspan.Value == 0)
                source.Wingspan = GenerateTemplateWingspan(source, creature);

            return source;
        }

        private void UpdateAppearance(Demographics source, string template, bool overwriteAppearance)
        {
            source.Skin = UpdateAppearance(source.Skin, TableNameConstants.Collection.AppearanceCategories.Skin, template, overwriteAppearance, source.Gender);
            source.Hair = UpdateAppearance(source.Hair, TableNameConstants.Collection.AppearanceCategories.Hair, template, overwriteAppearance, source.Gender);
            source.Eyes = UpdateAppearance(source.Eyes, TableNameConstants.Collection.AppearanceCategories.Eyes, template, overwriteAppearance, source.Gender);
            source.Other = UpdateAppearance(source.Other, TableNameConstants.Collection.AppearanceCategories.Other, template, overwriteAppearance, source.Gender);
        }

        private string UpdateAppearance(string source, string category, string template, bool overwrite, string gender)
        {
            var templateAppearance = GetRandomAppearance(template, gender, category);
            if (overwrite)
                return templateAppearance;

            if (string.IsNullOrEmpty(templateAppearance))
                return source;

            var separator = GetAppearanceSeparator(source);
            var newAppearance = source + separator + templateAppearance;
            return newAppearance.Trim();
        }

        private void UpdateAge(Demographics source, string template)
        {
            var ageRolls = typeAndAmountSelector.SelectFrom(Config.Name, TableNameConstants.TypeAndAmount.AgeRolls, template);
            var maxAgeRoll = ageRolls.First(r => r.Type == AgeConstants.Categories.Maximum);
            var ageRoll = ageRolls.First(r => r.Type != AgeConstants.Categories.Maximum);

            if (ageRoll.Type == AgeConstants.Categories.Multiplier)
            {
                source.Age.Value *= ageRoll.Amount;
            }
            else if (ageRoll.Amount > 0)
            {
                source.Age.Value += ageRoll.Amount;
                source.Age.Description = ageRoll.Type;
            }

            //INFO: If template age category is multiplier, it also applies to the Maximum age
            if (ageRoll.Type == AgeConstants.Categories.Multiplier)
            {
                source.MaximumAge.Value *= ageRoll.Amount;
            }
            else if (maxAgeRoll.Amount == AgeConstants.Ageless)
            {
                source.MaximumAge.Value = maxAgeRoll.Amount;
                source.MaximumAge.Description = ageRoll.Type;
            }
        }

        private void UpdateHeight(Demographics source, string creature, string template)
            => UpdateMeasurement(source.Height, TableNameConstants.TypeAndAmount.Heights, creature, template, source.Gender, heightDescriptions);

        private void UpdateLength(Demographics source, string creature, string template)
            => UpdateMeasurement(source.Length, TableNameConstants.TypeAndAmount.Lengths, creature, template, source.Gender, lengthDescriptions);

        private void UpdateMeasurement(Measurement measurement, string tablename, string creature, string template, string gender, string[] descriptions)
        {
            if (measurement.Value == 0)
                return;

            var templateMeasurements = typeAndAmountSelector.SelectFrom(Config.Name, tablename, template);
            var templateModifier = templateMeasurements.First();

            if (templateModifier.Amount == 0)
                return;

            var creatureMeasurements = typeAndAmountSelector.SelectFrom(Config.Name, tablename, creature);
            var creatureBase = creatureMeasurements.First(h => h.Type == gender);
            var creatureModifier = creatureMeasurements.First(h => h.Type == creature);
            var rawCreatureRoll = GetRoll(creatureBase.Roll, creatureModifier.Roll);

            if (templateModifier.Amount < 0)
                measurement.Value -= GetBelowAverageDecrease(measurement.Value, rawCreatureRoll);
            else if (templateModifier.Amount > 0)
                measurement.Value += GetAboveAverageIncrease(measurement.Value, rawCreatureRoll);

            measurement.Description = dice.Describe(rawCreatureRoll, (int)measurement.Value, descriptions);
        }

        private void UpdateWeight(Demographics source, string creature, string template)
        {
            if (source.Weight.Value == 0)
                return;

            var templateMeasurements = typeAndAmountSelector.SelectFrom(Config.Name, TableNameConstants.TypeAndAmount.Weights, template);
            var templateModifier = templateMeasurements.First();

            if (templateModifier.Amount == 0)
                return;

            var multiplier = 0d;
            if (source.Height.Value >= source.Length.Value)
                multiplier = GetMultiplier(TableNameConstants.TypeAndAmount.Heights, creature, source.Gender, source.Height.Value);
            else
                multiplier = GetMultiplier(TableNameConstants.TypeAndAmount.Lengths, creature, source.Gender, source.Length.Value);

            var weights = typeAndAmountSelector.SelectFrom(Config.Name, TableNameConstants.TypeAndAmount.Weights, creature);
            var baseWeight = weights.First(h => h.Type == source.Gender);
            var weightModifier = weights.First(h => h.Type == creature);
            var rawCreatureRoll = GetMultipliedRoll(baseWeight.Roll, multiplier, weightModifier.Roll);

            if (templateModifier.Amount < 0)
                source.Weight.Value -= GetBelowAverageDecrease(source.Weight.Value, rawCreatureRoll);
            else if (templateModifier.Amount > 0)
                source.Weight.Value += GetAboveAverageIncrease(source.Weight.Value, rawCreatureRoll);

            source.Weight.Description = dice.Describe(rawCreatureRoll, (int)source.Weight.Value, weightDescriptions);
        }

        private double GetMultiplier(string tablename, string creature, string gender, double baseValue)
        {
            var measurements = typeAndAmountSelector.SelectFrom(Config.Name, tablename, creature);
            var baseMeasurement = measurements.First(h => h.Type == gender);
            return baseValue - baseMeasurement.Amount;
        }

        private int GetBelowAverageDecrease(double originalValue, string rawRoll)
        {
            var minimum = dice.Roll(rawRoll).AsPotentialMinimum();
            return Convert.ToInt32((originalValue - minimum) / 2);
        }

        private int GetAboveAverageIncrease(double originalValue, string rawRoll)
        {
            var maximum = dice.Roll(rawRoll).AsPotentialMaximum();
            return Convert.ToInt32((maximum - originalValue) / 2);
        }

        public Demographics AdjustDemographicsBySize(Demographics demographics, string originalSize, string advancedSize)
        {
            var orderedSizes = SizeConstants.GetOrdered();
            var originalIndex = Array.IndexOf(orderedSizes, originalSize);
            var advancedIndex = Array.IndexOf(orderedSizes, advancedSize);
            var sizeDifference = advancedIndex - originalIndex;

            //INFO: If the advancement has adjusted the size of the creature, we need to increase the demographics,
            //specifically the height and weight. Roughly, x2 for each size category increase for height and x8 for the weight
            if (sizeDifference > 0)
            {
                var heightMultiplier = Math.Pow(2, sizeDifference);
                var weightMultiplier = Math.Pow(8, sizeDifference);

                demographics.Height.Value *= heightMultiplier;
                demographics.Length.Value *= heightMultiplier;
                demographics.Wingspan.Value *= heightMultiplier;
                demographics.Weight.Value *= weightMultiplier;
            }

            return demographics;
        }
    }
}
