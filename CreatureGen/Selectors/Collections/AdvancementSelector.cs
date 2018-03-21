using CreatureGen.Selectors.Selections;
using CreatureGen.Tables;
using DnDGen.Core.Selectors.Collections;
using DnDGen.Core.Selectors.Percentiles;
using System;
using System.Linq;

namespace CreatureGen.Selectors.Collections
{
    internal class AdvancementSelector : IAdvancementSelector
    {
        private readonly ITypeAndAmountSelector typeAndAmountSelector;
        private readonly IPercentileSelector percentileSelector;
        private readonly ICollectionSelector collectionSelector;

        public AdvancementSelector(ITypeAndAmountSelector typeAndAmountSelector, IPercentileSelector percentileSelector, ICollectionSelector collectionSelector)
        {
            this.typeAndAmountSelector = typeAndAmountSelector;
            this.percentileSelector = percentileSelector;
            this.collectionSelector = collectionSelector;
        }

        public bool IsAdvanced(string creature)
        {
            var typesAndAmounts = typeAndAmountSelector.Select(TableNameConstants.Set.Collection.Advancements, creature);

            return percentileSelector.SelectFrom(.1) && typesAndAmounts.Any();
        }

        public AdvancementSelection SelectRandomFor(string creature)
        {
            var typesAndAmounts = typeAndAmountSelector.Select(TableNameConstants.Set.Collection.Advancements, creature);
            var randomTypeAndAmount = collectionSelector.SelectRandomFrom(typesAndAmounts);
            var selection = GetAdvancementSelection(randomTypeAndAmount);

            return selection;
        }

        private AdvancementSelection GetAdvancementSelection(TypeAndAmountSelection typeAndAmount)
        {
            var selection = new AdvancementSelection();
            selection.AdditionalHitDice = typeAndAmount.Amount;

            var sections = typeAndAmount.Type.Split(',');
            selection.Size = sections[0];
            selection.Space = Convert.ToDouble(sections[1]);
            selection.Reach = Convert.ToDouble(sections[2]);

            return selection;
        }
    }
}
