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

            //TODO: figure out how to say the attack should be equipment/generated

            return selections;
        }

        private AttackSelection Parse(string input, string originalSize, string advancedSize)
        {
            var sections = input.Split(AttackSelection.Divider);

            var selection = new AttackSelection();
            selection.IsMelee = Convert.ToBoolean(sections[DataIndexConstants.AttackData.IsMelee]);
            selection.IsNatural = Convert.ToBoolean(sections[DataIndexConstants.AttackData.IsNatural]);
            selection.IsPrimary = Convert.ToBoolean(sections[DataIndexConstants.AttackData.IsPrimary]);
            selection.IsSpecial = Convert.ToBoolean(sections[DataIndexConstants.AttackData.IsSpecial]);
            selection.Name = sections[DataIndexConstants.AttackData.Name];
            selection.Damage = sections[DataIndexConstants.AttackData.Damage];

            if (selection.IsNatural)
                selection.Damage = GetAdjustedDamage(selection.Damage, originalSize, advancedSize);

            return selection;
        }

        private string GetAdjustedDamage(string originalDamage, string originalSize, string advancedSize)
        {
            var adjustedDamage = originalDamage;

            var orderedSizes = SizeConstants.GetOrdered();
            var sizeDifference = Array.IndexOf(orderedSizes, advancedSize) - Array.IndexOf(orderedSizes, originalSize);

            while (sizeDifference-- > 0)
            {
                switch (adjustedDamage)
                {
                    case "1d2": adjustedDamage = "1d3"; break;
                    case "1d3": adjustedDamage = "1d4"; break;
                    case "1d4": adjustedDamage = "1d6"; break;
                    case "1d6": adjustedDamage = "1d8"; break;
                    case "1d8": adjustedDamage = "2d6"; break;
                    case "1d10": adjustedDamage = "2d8"; break;
                    case "2d6": adjustedDamage = "3d6"; break;
                    case "2d8": adjustedDamage = "3d8"; break;
                    default: throw new ArgumentException($"{originalDamage} is not a valid damage that can be advanced to {advancedSize} size from {originalSize}");
                }
            }

            return adjustedDamage;
        }
    }
}
