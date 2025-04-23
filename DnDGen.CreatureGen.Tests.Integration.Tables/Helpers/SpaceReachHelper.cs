using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Tables;
using DnDGen.Infrastructure.Selectors.Collections;
using DnDGen.RollGen;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Integration.Tables.Helpers
{
    internal class SpaceReachHelper
    {
        private readonly Dice dice;
        private readonly Dictionary<string, double> spaces;
        private readonly Dictionary<string, double> tallReach;
        private readonly Dictionary<string, double> longReach;
        private readonly Dictionary<string, Dictionary<string, string>> creatureHeights;
        private readonly Dictionary<string, Dictionary<string, string>> creatureLengths;

        public SpaceReachHelper(Dice dice, ICollectionTypeAndAmountSelector typeAndAmountSelector)
        {
            this.dice = dice;

            spaces = new Dictionary<string, double>
            {
                [SizeConstants.Fine] = 0.5,
                [SizeConstants.Diminutive] = 1,
                [SizeConstants.Tiny] = 2.5,
                [SizeConstants.Small] = 5,
                [SizeConstants.Medium] = 5,
                [SizeConstants.Large] = 10,
                [SizeConstants.Huge] = 15,
                [SizeConstants.Gargantuan] = 20,
                [SizeConstants.Colossal] = 30,
            };
            tallReach = new Dictionary<string, double>
            {
                [SizeConstants.Fine] = 0,
                [SizeConstants.Diminutive] = 0,
                [SizeConstants.Tiny] = 0,
                [SizeConstants.Small] = 5,
                [SizeConstants.Medium] = 5,
                [SizeConstants.Large] = 10,
                [SizeConstants.Huge] = 15,
                [SizeConstants.Gargantuan] = 20,
                [SizeConstants.Colossal] = 30,
            };
            longReach = new Dictionary<string, double>
            {
                [SizeConstants.Fine] = 0,
                [SizeConstants.Diminutive] = 0,
                [SizeConstants.Tiny] = 0,
                [SizeConstants.Small] = 5,
                [SizeConstants.Medium] = 5,
                [SizeConstants.Large] = 5,
                [SizeConstants.Huge] = 10,
                [SizeConstants.Gargantuan] = 15,
                [SizeConstants.Colossal] = 20,
            };

            creatureHeights = typeAndAmountSelector.SelectAllFrom(Config.Name, TableNameConstants.TypeAndAmount.Heights)
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value.ToDictionary(v => v.Type, v => v.Roll));
            creatureLengths = typeAndAmountSelector.SelectAllFrom(Config.Name, TableNameConstants.TypeAndAmount.Lengths)
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value.ToDictionary(v => v.Type, v => v.Roll));
        }

        public double GetReach(string creature, string size)
        {
            var length = dice.Roll(creatureLengths[creature][creature]).AsPotentialAverage();
            var height = dice.Roll(creatureHeights[creature][creature]).AsPotentialAverage();

            if (height >= length)
                return tallReach[size];

            return longReach[size];
        }

        public double GetSpace(string size) => spaces[size];
    }
}
