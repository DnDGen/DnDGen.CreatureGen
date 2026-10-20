using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Defenses;
using DnDGen.CreatureGen.Generators.Creatures;
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
    internal class AdvancementSelector(
        IPercentileSelector percentileSelector,
        ICollectionSelector collectionSelector,
        ICollectionDataSelector<AdvancementDataSelection> advancementDataSelector,
        ICollectionTypeAndAmountSelector typeAndAmountSelector,
        Dice dice) : IAdvancementSelector
    {
        public bool IsAdvanced(string creature, IEnumerable<string> templates, double hitDiceQuantity, Filters filters)
        {
            if (filters?.ChallengeRatings?.Count > 0)
                return false;

            templates ??= [];

            var advancements = GetValidAdvancements(creature, templates, hitDiceQuantity, filters);
            if (!advancements.Any())
                return false;

            var isAdvanced = percentileSelector.SelectFrom(.9);
            return isAdvanced;
        }

        private IEnumerable<AdvancementDataSelection> GetValidAdvancements(string creature, IEnumerable<string> templates, double hitDiceQuantity, Filters filters)
        {
            var advancements = advancementDataSelector.SelectFrom(Config.Name, TableNameConstants.Collection.Advancements, creature);
            var maxHitDice = int.MaxValue;

            foreach (var template in templates)
            {
                var templateMaxHitDice = typeAndAmountSelector.SelectOneFrom(Config.Name, TableNameConstants.TypeAndAmount.MaxHitDice, template);
                maxHitDice = Math.Min(templateMaxHitDice.Amount, maxHitDice);
            }

            var roundedhitDice = HitDice.GetRoundedQuantity(hitDiceQuantity);
            var validAdvancements = advancements.Where(a => a.AdvancementIsValid(dice, maxHitDice - roundedhitDice, filters));

            return validAdvancements;
        }

        public AdvancementDataSelection SelectRandomFor(string creature, IEnumerable<string> templates, double hitDiceQuantity, Filters filters)
        {
            templates ??= [];

            var advancements = GetValidAdvancements(creature, templates, hitDiceQuantity, filters);
            if (!advancements.Any())
                throw new InvalidOperationException($"No valid advancements for {creature}");

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

        private static bool IsBarghest(string creatureName) => creatureName == CreatureConstants.Barghest || creatureName == CreatureConstants.Barghest_Greater;
    }
}
