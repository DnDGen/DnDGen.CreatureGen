using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Selectors.Collections;
using DnDGen.CreatureGen.Tables;
using DnDGen.Infrastructure.Selectors.Collections;
using DnDGen.Infrastructure.Selectors.Percentiles;
using DnDGen.RollGen;
using System;
using System.Collections.Generic;

namespace DnDGen.CreatureGen.Generators.Creatures
{
    internal class DemographicsGenerator : IDemographicsGenerator
    {
        private readonly IPercentileSelector percentileSelector;
        private readonly ICollectionSelector collectionsSelector;
        private readonly IAdjustmentsSelector adjustmentsSelector;
        private readonly Dice dice;

        public DemographicsGenerator(IPercentileSelector percentileSelector,
            ICollectionSelector collectionsSelector,
            IAdjustmentsSelector adjustmentsSelector,
            Dice dice)
        {
            this.percentileSelector = percentileSelector;
            this.collectionsSelector = collectionsSelector;
            this.adjustmentsSelector = adjustmentsSelector;
            this.dice = dice;
        }

        public Demographics Generate(string creatureName)
        {
            var demographics = new Demographics();

            demographics.Gender = DetermineGender(creatureName);
            demographics.MaximumAge = DetermineMaximumAge(race);
            demographics.Age = DetermineAge(race);

            var tableName = string.Format(TableNameConstants.Formattable.Adjustments.GENDERHeights, demographics.Gender);
            var baseHeight = adjustmentsSelector.SelectFrom(tableName, creatureName);
            var heightModifier = RollModifier(race, TableNameConstants.Set.Collection.HeightRolls);

            demographics.Height.Value = baseHeight + heightModifier;
            demographics.Height.Description = GetHeightDescription(race, heightModifier);

            tableName = string.Format(TableNameConstants.Formattable.Adjustments.GENDERLengths, demographics.Gender);
            var baseLength = adjustmentsSelector.SelectFrom(tableName, creatureName);
            var lengthModifier = RollModifier(race, TableNameConstants.Set.Collection.HeightRolls);

            demographics.Length.Value = baseLength + lengthModifier;
            demographics.Length.Description = GetLengthDescription(race, heightModifier);

            tableName = string.Format(TableNameConstants.Formattable.Adjustments.GENDERWeights, demographics.Gender);
            var baseWeight = adjustmentsSelector.SelectFrom(tableName, creatureName);
            var weightModifier = RollModifier(race, TableNameConstants.Set.Collection.WeightRolls);

            demographics.Weight.Value = baseWeight + heightModifier * weightModifier;
            demographics.Weight.Description = GetWeightDescription(race, weightModifier);

            return demographics;
        }

        private string DetermineGender(string creatureName)
        {
            return collectionsSelector.SelectRandomFrom(TableNameConstants.Collection.Genders, creatureName);
        }

        private Measurement DetermineMaximumAge(string creatureName)
        {
            var measurement = new Measurement("years");
            measurement.Value = GetMaximumAge(creatureName);

            if (measurement.Value == RaceConstants.Ages.Ageless)
                measurement.Description = "Immortal";
            else if (creatureName == RaceConstants.BaseRaces.Pixie)
                measurement.Description = "Will return to their plane of origin";
            else
                measurement.Description = "Will die of natural causes";

            return measurement;
        }

        private int GetMaximumAge(string creatureName)
        {
            var maximumAgeRoll = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.MaximumAgeRolls, creatureName).Single();

            if (maximumAgeRoll == RaceConstants.Ages.Ageless.ToString())
                return RaceConstants.Ages.Ageless;

            var undead = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.MetaraceGroups, GroupConstants.Undead);
            if (undead.Contains(race.Metarace))
                return RaceConstants.Ages.Ageless;

            var tableName = string.Format(TableNameConstants.Formattable.Adjustments.RACEAges, creatureName);
            var ages = adjustmentsSelector.SelectAllFrom(tableName);

            var oldestAgeGroup = GetOldestAgeGroup(ages);
            return ages[oldestAgeGroup] + dice.Roll(maximumAgeRoll).AsSum();
        }

        private string GetHeightDescription(Race race, int heightModifier)
        {
            return GetDescription(TableNameConstants.Set.Collection.HeightRolls, race, heightModifier, "Short", "Average", "Tall");
        }

        private string GetLengthDescription(Race race, int lengthModifier)
        {
            return GetDescription(TableNameConstants.Set.Collection.LengthRolls, race, lengthModifier, "Short", "Average", "Long");
        }

        private string GetWeightDescription(Race race, int weightModifier)
        {
            return GetDescription(TableNameConstants.Set.Collection.WeightRolls, race, weightModifier, "Light", "Average", "Heavy");
        }

        private string GetDescription(string tableName, Race race, int modifier, params string[] descriptions)
        {
            var roll = collectionsSelector.SelectFrom(tableName, creatureName).Single();
            var percentile = GetPercentile(roll, modifier);

            var rawIndex = percentile * descriptions.Length;
            rawIndex = Math.Floor(rawIndex);

            var index = Convert.ToInt32(rawIndex);
            index = Math.Min(index, descriptions.Count() - 1);

            return descriptions[index];
        }

        private double GetPercentile(string roll, double modifier)
        {
            var minimumRoll = dice.Roll(roll).AsPotentialMinimum();
            var maximumRoll = dice.Roll(roll).AsPotentialMaximum();
            var totalRange = maximumRoll - minimumRoll;
            var range = modifier - minimumRoll;

            if (totalRange > 0)
                return range / totalRange;

            return .5;
        }

        private Measurement GenerateAge(Race race, CharacterClass characterClass)
        {
            var age = new Measurement("Years");
            age.Value = GetAgeInYears(race, characterClass);
            age.Description = GetAgeDescription(race, age.Value);

            return age;
        }

        private Measurement GetDefaultAge(Race race)
        {
            var age = new Measurement("Years");
            age.Value = race.MaximumAge.Value;
            age.Description = GetAgeDescription(race, age.Value);

            return age;
        }

        private Measurement DetermineAge(Race race, CharacterClass characterClass)
        {
            var age = generator.Generate(
                () => GenerateAge(race, characterClass),
                a => race.MaximumAge.Value >= a.Value || race.MaximumAge.Value == RaceConstants.Ages.Ageless,
                () => GetDefaultAge(race),
                a => $"{a.Value} {a.Unit} is greater than maximum age of {race.MaximumAge.Value} {race.MaximumAge.Unit}",
                $"age for {race.Summary} {characterClass.Summary} of {race.MaximumAge.Value} years");

            return age;
        }

        private string GetOldestAgeGroup(Dictionary<string, int> ages)
        {
            return ages.OrderByDescending(kvp => kvp.Value).First().Key;
        }

        private int GetAgeInYears(Race race, CharacterClass characterClass)
        {
            var tableName = string.Format(TableNameConstants.Formattable.Adjustments.RACEAges, creatureName);
            var adultAge = adjustmentsSelector.SelectFrom(tableName, RaceConstants.Ages.Adulthood);
            var additionalAge = GetAdditionalAge(creatureName, characterClass);
            var ageInYears = adultAge + additionalAge;

            return ageInYears;
        }

        private int GetAdditionalAge(string baseRace, CharacterClass characterClass)
        {
            var classType = collectionsSelector.FindCollectionOf(TableNameConstants.Set.Collection.ClassNameGroups, characterClass.Name, allClassTypes.ToArray());
            var tableName = string.Format(TableNameConstants.Formattable.Collection.CLASSTYPEAgeRolls, classType);
            var trainingAgeRoll = collectionsSelector.SelectFrom(tableName, baseRace).Single();
            var additionalAge = 0;

            for (var i = 0; i < characterClass.Level; i++)
            {
                additionalAge += dice.Roll(trainingAgeRoll).AsSum();
            }

            return additionalAge;
        }

        private string GetAgeDescription(Race race, int ageInYears)
        {
            var tableName = string.Format(TableNameConstants.Formattable.Adjustments.RACEAges, creatureName);
            var ages = adjustmentsSelector.SelectAllFrom(tableName);

            if (ageInYears >= ages[RaceConstants.Ages.Venerable] && ages[RaceConstants.Ages.Venerable] != RaceConstants.Ages.Ageless)
                return RaceConstants.Ages.Venerable;

            if (ageInYears >= ages[RaceConstants.Ages.Old] && ages[RaceConstants.Ages.Old] != RaceConstants.Ages.Ageless)
                return RaceConstants.Ages.Old;

            if (ageInYears >= ages[RaceConstants.Ages.MiddleAge] && ages[RaceConstants.Ages.MiddleAge] != RaceConstants.Ages.Ageless)
                return RaceConstants.Ages.MiddleAge;

            return RaceConstants.Ages.Adulthood;
        }

        private int RollModifier(Race race, string tableName)
        {
            var roll = collectionsSelector.SelectFrom(tableName, creatureName).Single();
            return dice.Roll(roll).AsSum();
        }
    }
}
