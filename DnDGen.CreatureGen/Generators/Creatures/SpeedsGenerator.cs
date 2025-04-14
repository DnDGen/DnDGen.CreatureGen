using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Tables;
using DnDGen.Infrastructure.Selectors.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Generators.Creatures
{
    internal class SpeedsGenerator : ISpeedsGenerator
    {
        private readonly ICollectionTypeAndAmountSelector typeAndAmountSelector;
        private readonly ICollectionSelector collectionSelector;

        public SpeedsGenerator(ICollectionTypeAndAmountSelector typeAndAmountSelector, ICollectionSelector collectionSelector)
        {
            this.typeAndAmountSelector = typeAndAmountSelector;
            this.collectionSelector = collectionSelector;
        }

        public Dictionary<string, Measurement> Generate(string creatureName)
        {
            var speedTypesAndAmounts = typeAndAmountSelector.SelectFrom(Config.Name, TableNameConstants.Collection.Speeds, creatureName);
            var speeds = new Dictionary<string, Measurement>();

            foreach (var speedTypeAndAmount in speedTypesAndAmounts)
            {
                var measurement = new Measurement("feet per round")
                {
                    Value = speedTypeAndAmount.Amount
                };

                if (speedTypeAndAmount.Type == SpeedConstants.Fly)
                    measurement.Description = collectionSelector.SelectFrom(Config.Name, TableNameConstants.Collection.AerialManeuverability, creatureName).Single();

                speeds[speedTypeAndAmount.Type] = measurement;
            }

            return speeds;
        }
    }
}
