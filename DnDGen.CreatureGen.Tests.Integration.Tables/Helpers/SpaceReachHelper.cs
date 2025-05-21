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
        private readonly ICollectionTypeAndAmountSelector typeAndAmountSelector;
        private readonly Dictionary<string, double> defaultSpace;
        private readonly Dictionary<string, double> defaultTallReach;
        private readonly Dictionary<string, double> defaultLongReach;

        public SpaceReachHelper(Dice dice, ICollectionTypeAndAmountSelector typeAndAmountSelector)
        {
            this.dice = dice;
            this.typeAndAmountSelector = typeAndAmountSelector;

            defaultSpace = new Dictionary<string, double>
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
            defaultTallReach = new Dictionary<string, double>
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
            defaultLongReach = new Dictionary<string, double>
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
        }

        public double GetDefaultReach(string creature, string size)
        {
            if (IsTall(creature))
                return defaultTallReach[size];

            return defaultLongReach[size];
        }

        private bool IsTall(string creature)
        {
            var lengthRoll = typeAndAmountSelector.SelectFrom(Config.Name, TableNameConstants.TypeAndAmount.Lengths, creature)
                .Where(v => v.Type == creature)
                .Select(v => v.Roll)
                .Single();
            var heightRoll = typeAndAmountSelector.SelectFrom(Config.Name, TableNameConstants.TypeAndAmount.Heights, creature)
                .Where(v => v.Type == creature)
                .Select(v => v.Roll)
                .Single();

            var length = dice.Roll(lengthRoll).AsPotentialAverage();
            var height = dice.Roll(heightRoll).AsPotentialAverage();

            return height >= length;
        }

        public double GetDefaultSpace(string size) => defaultSpace[size];

        public double GetAdvancedReach(string creature, string originalSize, double originalReach, string advancedSize)
        {
            if (advancedSize == originalSize)
                return originalReach;

            if (IsTall(creature))
                return ComputeIncrease(originalReach, defaultTallReach[originalSize], defaultTallReach[advancedSize]);

            return ComputeIncrease(originalReach, defaultLongReach[originalSize], defaultLongReach[advancedSize]);
        }

        public double GetAdvancedSpace(string originalSize, double originalSpace, string advancedSize)
        {
            if (originalSize == advancedSize)
                return originalSpace;

            return ComputeIncrease(originalSpace, defaultSpace[originalSize], defaultSpace[advancedSize]);
        }

        private double ComputeIncrease(double original, double originalDefault, double advancedDefault)
        {
            if (original == originalDefault)
                return advancedDefault;

            var divisor = originalDefault > 0 ? originalDefault : 1;
            var multiplier = original / divisor;
            return advancedDefault * multiplier;
        }
    }
}
