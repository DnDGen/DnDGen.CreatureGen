using DnDGen.CreatureGen.Tables;
using DnDGen.Infrastructure.Selectors.Collections;
using DnDGen.RollGen;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Integration.Tables.Helpers
{
    internal class MeasurementHelper
    {
        private readonly Dice dice;
        private readonly ICollectionTypeAndAmountSelector typeAndAmountSelector;
        private readonly ICollectionSelector collectionSelector;

        public MeasurementHelper(Dice dice, ICollectionTypeAndAmountSelector typeAndAmountSelector, ICollectionSelector collectionSelector)
        {
            this.dice = dice;
            this.typeAndAmountSelector = typeAndAmountSelector;
            this.collectionSelector = collectionSelector;
        }

        public double GetAverageHeight(string creature, string gender)
        {
            var roll = GetRoll(creature, gender, TableNameConstants.TypeAndAmount.Heights);
            return dice.Roll(roll).AsPotentialAverage();
        }

        public double GetAverageHeight(string creature)
        {
            var gender = collectionSelector.SelectFrom(Config.Name, TableNameConstants.Collection.Genders, creature).First();
            return GetAverageHeight(creature, gender);
        }

        public double GetAverageLength(string creature, string gender)
        {
            var roll = GetRoll(creature, gender, TableNameConstants.TypeAndAmount.Lengths);
            return dice.Roll(roll).AsPotentialAverage();
        }

        private string GetRoll(string creature, string gender, string table, string multiplier = null)
        {
            var rollSections = typeAndAmountSelector.SelectFrom(Config.Name, table, creature);
            var baseSelection = rollSections.Single(v => v.Type == gender);
            var modifierSelection = rollSections.Single(v => v.Type == creature);

            if (string.IsNullOrEmpty(multiplier))
                return $"{baseSelection.Roll}+{modifierSelection.Roll}";

            return $"{baseSelection.Roll}+{modifierSelection.Roll}*{multiplier}";
        }

        public double GetAverageLength(string creature)
        {
            var gender = collectionSelector.SelectFrom(Config.Name, TableNameConstants.Collection.Genders, creature).First();
            return GetAverageLength(creature, gender);
        }

        public bool IsTall(string creature)
        {
            var height = GetAverageHeight(creature);
            var length = GetAverageLength(creature);

            return height >= length;
        }

        public double GetAverageWeight(string creature, string gender)
        {
            var multiplierTable = TableNameConstants.TypeAndAmount.Heights;
            if (IsTall(creature))
                multiplierTable = TableNameConstants.TypeAndAmount.Lengths;

            var multiplierSelection = typeAndAmountSelector.SelectFrom(Config.Name, multiplierTable, creature).Single(v => v.Type == creature);
            var roll = GetRoll(creature, gender, TableNameConstants.TypeAndAmount.Weights, multiplierSelection.Roll);

            return dice.Roll(roll).AsPotentialAverage();
        }

        public double GetAverageWeight(string creature)
        {
            var gender = collectionSelector.SelectFrom(Config.Name, TableNameConstants.Collection.Genders, creature).First();
            return GetAverageWeight(creature, gender);
        }
    }
}
