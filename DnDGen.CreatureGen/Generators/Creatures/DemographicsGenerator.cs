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
        private readonly string[] heightDescriptions;
        private readonly string[] lengthDescriptions;
        private readonly string[] wingspanDescriptions;
        private readonly string[] weightDescriptions;

        public DemographicsGenerator(ICollectionSelector collectionsSelector, Dice dice, ITypeAndAmountSelector typeAndAmountSelector)
        {
            this.collectionsSelector = collectionsSelector;
            this.dice = dice;
            this.typeAndAmountSelector = typeAndAmountSelector;

            heightDescriptions = new[] { "Very Short", "Short", "Average", "Tall", "Very Tall" };
            lengthDescriptions = new[] { "Very Short", "Short", "Average", "Long", "Very Long" };
            wingspanDescriptions = new[] { "Very Narrow", "Narrow", "Average", "Broad", "Very Broad" };
            weightDescriptions = new[] { "Very Light", "Light", "Average", "Heavy", "Very Heavy" };
        }

        public Demographics Generate(string creatureName)
        {
            var demographics = new Demographics();

            demographics.Gender = collectionsSelector.SelectRandomFrom(TableNameConstants.Collection.Genders, creatureName);
            demographics.Age = DetermineAge(creatureName);
            demographics.MaximumAge = DetermineMaximumAge(creatureName, demographics.Age);

            var heights = typeAndAmountSelector.Select(TableNameConstants.TypeAndAmount.Heights, creatureName);
            var baseHeight = heights.First(h => h.Type == demographics.Gender);
            var heightModifier = heights.First(h => h.Type == creatureName);

            demographics.Height.Value = baseHeight.Amount + heightModifier.Amount;
            demographics.Height.Description = dice.Describe(heightModifier.RawAmount, heightModifier.Amount, heightDescriptions);

            var lengths = typeAndAmountSelector.Select(TableNameConstants.TypeAndAmount.Lengths, creatureName);
            var baseLength = lengths.First(h => h.Type == demographics.Gender);
            var lengthModifier = lengths.First(h => h.Type == creatureName);

            demographics.Length.Value = baseLength.Amount + lengthModifier.Amount;
            demographics.Length.Description = dice.Describe(lengthModifier.RawAmount, lengthModifier.Amount, lengthDescriptions);

            var weights = typeAndAmountSelector.Select(TableNameConstants.TypeAndAmount.Weights, creatureName);
            var baseWeight = weights.First(h => h.Type == demographics.Gender);
            var weightModifier = weights.First(h => h.Type == creatureName);
            var multiplier = Math.Max(heightModifier.Amount, lengthModifier.Amount);

            demographics.Weight.Value = baseWeight.Amount + multiplier * weightModifier.Amount;
            demographics.Weight.Description = dice.Describe(weightModifier.RawAmount, weightModifier.Amount, weightDescriptions);

            var wingspans = typeAndAmountSelector.Select(TableNameConstants.TypeAndAmount.Wingspans, creatureName);
            var baseWingspan = wingspans.First(h => h.Type == demographics.Gender);
            var wingspanModifier = wingspans.First(h => h.Type == creatureName);

            demographics.Wingspan.Value = baseWingspan.Amount + wingspanModifier.Amount;
            demographics.Wingspan.Description = dice.Describe(wingspanModifier.RawAmount, wingspanModifier.Amount, wingspanDescriptions);

            demographics.Appearance = collectionsSelector.SelectRandomFrom(TableNameConstants.Collection.Appearances, creatureName);

            return demographics;
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
            var ageRolls = typeAndAmountSelector.Select(TableNameConstants.TypeAndAmount.AgeRolls, creatureName);
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
            var maxAge = new Measurement("years");
            maxAge.Value = GetMaximumAge(creatureName);

            if (age.Value > maxAge.Value)
                maxAge.Value = age.Value;

            var descriptions = collectionsSelector.SelectFrom(TableNameConstants.Collection.MaxAgeDescriptions, creatureName);
            maxAge.Description = descriptions.Single();

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
    }
}
