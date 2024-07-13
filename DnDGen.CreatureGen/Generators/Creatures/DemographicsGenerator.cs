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

            heightDescriptions = ["Very Short", "Short", "Average", "Tall", "Very Tall"];
            lengthDescriptions = ["Very Short", "Short", "Average", "Long", "Very Long"];
            wingspanDescriptions = ["Very Narrow", "Narrow", "Average", "Broad", "Very Broad"];
            weightDescriptions = ["Very Light", "Light", "Average", "Heavy", "Very Heavy"];
        }

        public Demographics Generate(string creatureName)
        {
            var demographics = new Demographics();

            demographics.Gender = collectionsSelector.SelectRandomFrom(Config.Name, TableNameConstants.Collection.Genders, creatureName);
            demographics.Age = DetermineAge(creatureName);
            demographics.MaximumAge = DetermineMaximumAge(creatureName, demographics.Age);

            var heights = typeAndAmountSelector.Select(TableNameConstants.TypeAndAmount.Heights, creatureName);
            var baseHeight = heights.First(h => h.Type == demographics.Gender);
            var heightModifier = heights.First(h => h.Type == creatureName);

            demographics.Height.Value = baseHeight.Amount + heightModifier.Amount;
            var rawHeightRoll = $"{baseHeight.RawAmount}+{heightModifier.RawAmount}";
            demographics.Height.Description = dice.Describe(rawHeightRoll, (int)demographics.Height.Value, heightDescriptions);

            var lengths = typeAndAmountSelector.Select(TableNameConstants.TypeAndAmount.Lengths, creatureName);
            var baseLength = lengths.First(h => h.Type == demographics.Gender);
            var lengthModifier = lengths.First(h => h.Type == creatureName);

            demographics.Length.Value = baseLength.Amount + lengthModifier.Amount;
            var rawLengthRoll = $"{baseLength.RawAmount}+{lengthModifier.RawAmount}";
            demographics.Length.Description = dice.Describe(rawLengthRoll, (int)demographics.Length.Value, lengthDescriptions);

            var weights = typeAndAmountSelector.Select(TableNameConstants.TypeAndAmount.Weights, creatureName);
            var baseWeight = weights.First(h => h.Type == demographics.Gender);
            var weightModifier = weights.First(h => h.Type == creatureName);
            var multiplier = Math.Max(heightModifier.Amount, lengthModifier.Amount);

            demographics.Weight.Value = baseWeight.Amount + multiplier * weightModifier.Amount;
            var rawWeightRoll = $"{baseWeight.RawAmount}+{multiplier}*{weightModifier.RawAmount}";
            demographics.Weight.Description = dice.Describe(rawWeightRoll, (int)demographics.Weight.Value, weightDescriptions);

            demographics.Wingspan = GenerateWingspan(creatureName, demographics.Gender);
            demographics.Appearance = GetRandomAppearance(creatureName, demographics.Gender);

            return demographics;
        }

        private string GetRandomAppearance(string creatureName, string gender)
        {
            var collectionName = creatureName;

            if (collectionsSelector.IsCollection(Config.Name, TableNameConstants.Collection.Appearances, creatureName + gender + Rarity.Common.ToString()))
                collectionName += gender;

            var common = collectionsSelector.SelectFrom(Config.Name, TableNameConstants.Collection.Appearances, collectionName + Rarity.Common.ToString());
            var uncommon = collectionsSelector.SelectFrom(Config.Name, TableNameConstants.Collection.Appearances, collectionName + Rarity.Uncommon.ToString());
            var rare = collectionsSelector.SelectFrom(Config.Name, TableNameConstants.Collection.Appearances, collectionName + Rarity.Rare.ToString());
            var veryRare = collectionsSelector.SelectFrom(Config.Name, TableNameConstants.Collection.Appearances, collectionName + Rarity.VeryRare.ToString());

            var appearance = collectionsSelector.SelectRandomFrom(common, uncommon, rare, veryRare);
            return appearance;
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

            var descriptions = collectionsSelector.SelectFrom(Config.Name, TableNameConstants.Collection.MaxAgeDescriptions, creatureName);
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

        public Measurement GenerateWingspan(string creatureName, string baseKey)
        {
            var wingspans = typeAndAmountSelector.Select(TableNameConstants.TypeAndAmount.Wingspans, creatureName);
            var baseWingspan = wingspans.First(h => h.Type == baseKey);
            var wingspanModifier = wingspans.First(h => h.Type == creatureName);

            var wingspan = new Measurement("inches");
            wingspan.Value = baseWingspan.Amount + wingspanModifier.Amount;
            var rawWingspanRoll = $"{baseWingspan.RawAmount}+{wingspanModifier.RawAmount}";
            wingspan.Description = dice.Describe(rawWingspanRoll, (int)wingspan.Value, wingspanDescriptions);

            return wingspan;
        }
    }
}
