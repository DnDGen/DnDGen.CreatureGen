using CreatureGen.Creatures;
using CreatureGen.Selectors.Collections;
using CreatureGen.Tables;
using DnDGen.Core.Selectors.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CreatureGen.Generators.Creatures
{
    internal class SpeedsGenerator : ISpeedsGenerator
    {
        private readonly ITypeAndAmountSelector typeAndAmountSelector;
        private readonly ICollectionSelector collectionSelector;

        public SpeedsGenerator(ITypeAndAmountSelector typeAndAmountSelector, ICollectionSelector collectionSelector)
        {
            this.typeAndAmountSelector = typeAndAmountSelector;
            this.collectionSelector = collectionSelector;
        }

        public Dictionary<string, Measurement> Generate(string creatureName)
        {
            var speedTypesAndAmounts = typeAndAmountSelector.Select(TableNameConstants.Collection.Speeds, creatureName);
            var speeds = new Dictionary<string, Measurement>();

            foreach (var speedTypeAndAmount in speedTypesAndAmounts)
            {
                var measurement = new Measurement("feet per round");
                measurement.Value = speedTypeAndAmount.Amount;

                if (speedTypeAndAmount.Type == SpeedConstants.Fly)
                    measurement.Description = collectionSelector.SelectFrom(TableNameConstants.Collection.AerialManeuverability, creatureName).Single();

                speeds[speedTypeAndAmount.Type] = measurement;
            }

            return speeds;
        }
    }
}
