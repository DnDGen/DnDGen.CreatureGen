using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Selectors.Collections;
using DnDGen.CreatureGen.Selectors.Selections;
using DnDGen.CreatureGen.Tables;
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
        private readonly ITypeAndAmountSelector typeAndAmountSelector;

        public DemographicsGenerator(ICollectionSelector collectionsSelector, Dice dice, ITypeAndAmountSelector typeAndAmountSelector)
        {
            this.collectionsSelector = collectionsSelector;
            this.dice = dice;
            this.typeAndAmountSelector = typeAndAmountSelector;
        }

        public Demographics Generate(string creatureName)
        {
            var demographics = new Demographics();

            demographics.Gender = DetermineGender(creatureName);
            demographics.Age = DetermineAge(creatureName);
            demographics.MaximumAge = DetermineMaximumAge(creatureName, demographics.Age);

            var heights = typeAndAmountSelector.Select(TableNameConstants.TypeAndAmount.Heights, creatureName);
            var baseHeight = heights.First(h => h.Type == demographics.Gender);
            var heightModifier = heights.First(h => h.Type == creatureName);

            demographics.HeightOrLength.Value = baseHeight.Amount + heightModifier.Amount;
            demographics.HeightOrLength.Description = GetDescription(heightModifier.RawAmount, heightModifier.Amount, "Short", "Average", "Tall");

            var weights = typeAndAmountSelector.Select(TableNameConstants.TypeAndAmount.Weights, creatureName);
            var baseWeight = weights.First(h => h.Type == demographics.Gender);
            var weightModifier = weights.First(h => h.Type == creatureName);

            demographics.Weight.Value = baseWeight.Amount + heightModifier.Amount * weightModifier.Amount;
            demographics.Weight.Description = GetDescription(weightModifier.RawAmount, weightModifier.Amount, "Light", "Average", "Heavy");

            return demographics;
        }

        private string DetermineGender(string creatureName)
        {
            return collectionsSelector.SelectRandomFrom(TableNameConstants.Collection.Genders, creatureName);
        }

        private Measurement DetermineAge(string creatureName)
        {
            var ageRoll = GetRandomAgeRoll(creatureName);

            var age = new Measurement("years");
            age.Value = ageRoll.Amount;
            age.Description = ageRoll.Type;

            return age;
        }

        private TypeAndAmountSelection GetRandomAgeRoll(string creatureName)
        {
            var ageRolls = typeAndAmountSelector.Select(TableNameConstants.TypeAndAmount.AgeRolls, creatureName).ToArray();
            if (ageRolls.Length == 1)
                return ageRolls[0];

            var common = new[] { ageRolls[0] };
            var uncommon = new[] { ageRolls[1] };
            var rare = new[] { ageRolls[2] };
            var veryRare = new[] { ageRolls[3] };

            var randomAgeRoll = collectionsSelector.SelectRandomFrom(common, uncommon, rare, veryRare);
            return randomAgeRoll;
        }

        private Measurement DetermineMaximumAge(string creatureName, Measurement age)
        {
            var maxAge = new Measurement("years");
            maxAge.Value = GetMaximumAge(creatureName);

            if (age.Value > maxAge.Value)
                maxAge.Value = age.Value;

            var categories = collectionsSelector.SelectFrom(TableNameConstants.Collection.AgeCategories, creatureName + AgeConstants.Categories.Maximum);
            maxAge.Description = categories.Single();

            return maxAge;
        }

        private int GetMaximumAge(string creatureName)
        {
            var ageRolls = typeAndAmountSelector.Select(TableNameConstants.TypeAndAmount.AgeRolls, creatureName);
            var maxAgeRoll = ageRolls.FirstOrDefault(r => r.Type == AgeConstants.Categories.Maximum);

            if (maxAgeRoll == null)
                return AgeConstants.Ageless;

            return maxAgeRoll.Amount;
        }

        private string GetDescription(string roll, int value, params string[] descriptions)
        {
            var percentile = GetPercentile(roll, value);

            var rawIndex = percentile * descriptions.Length;
            rawIndex = Math.Floor(rawIndex);

            var index = Convert.ToInt32(rawIndex);
            index = Math.Min(index, descriptions.Count() - 1);

            return descriptions[index];
        }

        private double GetPercentile(string roll, double value)
        {
            var minimumRoll = dice.Roll(roll).AsPotentialMinimum();
            var maximumRoll = dice.Roll(roll).AsPotentialMaximum();
            var totalRange = maximumRoll - minimumRoll;
            var range = value - minimumRoll;

            if (totalRange > 0)
                return range / totalRange;

            return .5;
        }
    }
}
