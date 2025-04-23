using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Defenses;
using DnDGen.CreatureGen.Selectors.Selections;
using DnDGen.CreatureGen.Tables;
using DnDGen.Infrastructure.Selectors.Collections;
using DnDGen.Infrastructure.Selectors.Percentiles;
using DnDGen.RollGen;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Selectors.Collections
{
    internal class AdvancementSelector : IAdvancementSelector
    {
        private readonly IPercentileSelector percentileSelector;
        private readonly ICollectionSelector collectionSelector;
        private readonly ICollectionTypeAndAmountSelector typeAndAmountSelector;
        private readonly ICollectionDataSelector<AdvancementDataSelection> advancementDataSelector;
        private readonly Dice dice;

        public AdvancementSelector(
            IPercentileSelector percentileSelector,
            ICollectionSelector collectionSelector,
            ICollectionDataSelector<AdvancementDataSelection> advancementDataSelector,
            ICollectionTypeAndAmountSelector typeAndAmountSelector,
            Dice dice)
        {
            this.percentileSelector = percentileSelector;
            this.collectionSelector = collectionSelector;
            this.advancementDataSelector = advancementDataSelector;
            this.typeAndAmountSelector = typeAndAmountSelector;
            this.dice = dice;
        }

        public bool IsAdvanced(string creature, IEnumerable<string> templates, string challengeRatingFilter)
        {
            if (challengeRatingFilter != null)
                return false;

            templates ??= [];

            var advancements = GetValidAdvancements(creature, templates);
            if (!advancements.Any())
                return false;

            var isAdvanced = percentileSelector.SelectFrom(.9);
            return isAdvanced;
        }

        private IEnumerable<AdvancementDataSelection> GetValidAdvancements(string creature, IEnumerable<string> templates)
        {
            var advancements = advancementDataSelector.SelectFrom(Config.Name, TableNameConstants.Collection.Advancements, creature);
            var creatureHitDice = typeAndAmountSelector.SelectOneFrom(Config.Name, TableNameConstants.TypeAndAmount.HitDice, creature);
            var maxHitDice = int.MaxValue;

            foreach (var template in templates)
            {
                var templateMaxHitDice = typeAndAmountSelector.SelectOneFrom(Config.Name, TableNameConstants.TypeAndAmount.HitDice, template);
                maxHitDice = Math.Min(templateMaxHitDice.Amount, maxHitDice);
            }

            var roundedhitDice = HitDice.GetRoundedQuantity(creatureHitDice.AmountAsDouble);
            var validAdvancements = advancements.Where(a => a.AdvancementIsValid(dice, maxHitDice - roundedhitDice));

            return validAdvancements;
        }

        public AdvancementDataSelection SelectRandomFor(string creature, IEnumerable<string> templates)
        {
            templates ??= [];

            var advancements = GetValidAdvancements(creature, templates);
            var randomAdvancement = collectionSelector.SelectRandomFrom(advancements);
            var selection = GetAdvancementSelection(creature, randomAdvancement);

            return selection;
        }

        private AdvancementDataSelection GetAdvancementSelection(string creatureName, AdvancementDataSelection selection)
        {
            selection.SetAdditionalProperties(dice);

            if (IsBarghest(creatureName))
            {
                selection.StrengthAdjustment = selection.AdditionalHitDice;
                selection.ConstitutionAdjustment = selection.AdditionalHitDice;
                selection.NaturalArmorAdjustment = selection.AdditionalHitDice;
                selection.CasterLevelAdjustment = selection.AdditionalHitDice;
            }

            return selection;
        }

        private bool IsBarghest(string creatureName)
        {
            return creatureName == CreatureConstants.Barghest || creatureName == CreatureConstants.Barghest_Greater;
        }
    }
}
