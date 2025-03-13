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
        private readonly ICollectionDataSelector<AttackDataSelection> attackDataSelector;
        private readonly ICollectionDataSelector<DamageDataSelection> damageDataSelector;
        private readonly Dictionary<string, string> damageMaps;
        private readonly string[] orderedSizes;

        public AttackSelector(ICollectionSelector collectionSelector, ICollectionDataSelector<AttackDataSelection> attackDataSelector, ICollectionDataSelector<DamageDataSelection> damageDataSelector)
        {
            this.collectionSelector = collectionSelector;
            this.attackDataSelector = attackDataSelector;
            this.damageDataSelector = damageDataSelector;

            damageMaps = new Dictionary<string, string>
            {
                ["2d8"] = "3d8",
                ["2d6"] = "3d6",
                ["1d10"] = "2d8",
                ["1d8"] = "2d6",
                ["1d6"] = "1d8",
                ["1d4"] = "1d6",
                ["1d3"] = "1d4",
                ["1d2"] = "1d3"
            };

            orderedSizes = SizeConstants.GetOrdered();
        }

        public IEnumerable<AttackDataSelection> Select(string creatureName, string originalSize, string advancedSize)
        {
            var attackSelections = attackDataSelector.SelectFrom(Config.Name, TableNameConstants.Collection.AttackData, creatureName);

            foreach (var selection in attackSelections)
            {
                var key = selection.BuildKey(creatureName);
                var damageSelections = damageDataSelector.SelectFrom(Config.Name, TableNameConstants.Collection.DamageData, key);
                selection.Damages.AddRange(damageSelections);

                if (selection.IsNatural && originalSize != advancedSize)
                {
                    foreach (var damage in selection.Damages)
                    {
                        damage.Roll = GetAdjustedDamage(damage.Roll, originalSize, advancedSize);
                    }
                }
            }

            return attackSelections;
        }

        private string GetAdjustedDamage(string originalDamage, string originalSize, string advancedSize)
        {
            var adjustedDamage = originalDamage;
            var sizeDifference = Array.IndexOf(orderedSizes, advancedSize) - Array.IndexOf(orderedSizes, originalSize);

            while (sizeDifference-- > 0 && damageMaps.ContainsKey(adjustedDamage))
            {
                adjustedDamage = damageMaps[adjustedDamage];
            }

            return adjustedDamage;
        }
    }
}
