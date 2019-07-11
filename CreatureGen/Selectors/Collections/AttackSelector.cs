using CreatureGen.Creatures;
using CreatureGen.Selectors.Selections;
using CreatureGen.Tables;
using DnDGen.Core.Selectors.Collections;
using System;
using System.Collections.Generic;

namespace CreatureGen.Selectors.Collections
{
    internal class AttackSelector : IAttackSelector
    {
        private readonly ICollectionSelector collectionSelector;

        public AttackSelector(ICollectionSelector collectionSelector)
        {
            this.collectionSelector = collectionSelector;
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
            var sections = input.Split(AttackSelection.Divider);

            var selection = new AttackSelection();
            selection.IsMelee = Convert.ToBoolean(sections[DataIndexConstants.AttackData.IsMeleeIndex]);
            selection.IsNatural = Convert.ToBoolean(sections[DataIndexConstants.AttackData.IsNaturalIndex]);
            selection.IsPrimary = Convert.ToBoolean(sections[DataIndexConstants.AttackData.IsPrimaryIndex]);
            selection.IsSpecial = Convert.ToBoolean(sections[DataIndexConstants.AttackData.IsSpecialIndex]);
            selection.Name = sections[DataIndexConstants.AttackData.NameIndex];
            selection.Damage = sections[DataIndexConstants.AttackData.DamageIndex];

            if (selection.IsNatural)
                selection.Damage = GetAdjustedDamage(selection.Damage, originalSize, advancedSize);

            return selection;
        }

        private string GetAdjustedDamage(string originalDamage, string originalSize, string advancedSize)
        {
            var adjustedDamage = originalDamage;

            var orderedSizes = SizeConstants.GetOrdered();
            var sizeDifference = Array.IndexOf(orderedSizes, advancedSize) - Array.IndexOf(orderedSizes, originalSize);

            var damageMaps = new Dictionary<string, string>();
            damageMaps["2d8"] = "3d8";
            damageMaps["2d6"] = "3d6";
            damageMaps["1d10"] = "2d8";
            damageMaps["1d8"] = "2d6";
            damageMaps["1d6"] = "1d8";
            damageMaps["1d4"] = "1d6";
            damageMaps["1d3"] = "1d4";
            damageMaps["1d2"] = "1d3";

            while (sizeDifference-- > 0)
            {
                foreach (var kvp in damageMaps)
                {
                    adjustedDamage = adjustedDamage.Replace(kvp.Key, kvp.Value);
                }
            }

            return adjustedDamage;
        }
    }
}
