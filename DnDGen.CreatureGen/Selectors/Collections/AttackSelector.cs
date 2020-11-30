using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Selectors.Selections;
using DnDGen.CreatureGen.Tables;
using DnDGen.Infrastructure.Selectors.Collections;
using System;
using System.Collections.Generic;

namespace DnDGen.CreatureGen.Selectors.Collections
{
    internal class AttackSelector : IAttackSelector
    {
        private readonly ICollectionSelector collectionSelector;
        private readonly Dictionary<string, string> damageMaps;

        public AttackSelector(ICollectionSelector collectionSelector)
        {
            this.collectionSelector = collectionSelector;

            damageMaps = new Dictionary<string, string>();
            damageMaps["2d8"] = "3d8";
            damageMaps["2d6"] = "3d6";
            damageMaps["1d10"] = "2d8";
            damageMaps["1d8"] = "2d6";
            damageMaps["1d6"] = "1d8";
            damageMaps["1d4"] = "1d6";
            damageMaps["1d3"] = "1d4";
            damageMaps["1d2"] = "1d3";
        }

        public IEnumerable<AttackSelection> Select(string creatureName, string originalSize, string advancedSize)
        {
            var attackData = collectionSelector.SelectFrom(TableNameConstants.Collection.AttackData, creatureName);
            var selections = new List<AttackSelection>();

            foreach (var data in attackData)
            {
                var selection = Parse(data, originalSize, advancedSize);
                selections.Add(selection);
            }

            return selections;
        }

        private AttackSelection Parse(string input, string originalSize, string advancedSize)
        {
            var selection = AttackSelection.From(input);

            if (selection.IsNatural && originalSize != advancedSize)
            {
                foreach (var damage in selection.Damages)
                {
                    damage.Roll = GetAdjustedDamage(damage.Roll, originalSize, advancedSize);
                }
            }

            return selection;
        }

        private string GetAdjustedDamage(string originalDamage, string originalSize, string advancedSize)
        {
            var adjustedDamage = originalDamage;

            var orderedSizes = SizeConstants.GetOrdered();
            var sizeDifference = Array.IndexOf(orderedSizes, advancedSize) - Array.IndexOf(orderedSizes, originalSize);

            while (sizeDifference-- > 0 && damageMaps.ContainsKey(adjustedDamage))
            {
                adjustedDamage = damageMaps[adjustedDamage];
            }

            return adjustedDamage;
        }
    }
}
