﻿using DnDGen.CreatureGen.Creatures;
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
        private readonly ITypeAndAmountSelector typeAndAmountSelector;
        private readonly IPercentileSelector percentileSelector;
        private readonly ICollectionSelector collectionSelector;
        private readonly ICollectionTypeAndAmountSelector collectionTypeAndAmountSelector;
        private readonly ICollectionDataSelector<AdvancementDataSelection> advancementDataSelector;
        private readonly Dice dice;

        public AdvancementSelector(
            ITypeAndAmountSelector typeAndAmountSelector,
            IPercentileSelector percentileSelector,
            ICollectionSelector collectionSelector,
            ICollectionDataSelector<AdvancementDataSelection> advancementDataSelector,
            ICollectionTypeAndAmountSelector collectionTypeAndAmountSelector,
            Dice dice)
        {
            this.typeAndAmountSelector = typeAndAmountSelector;
            this.percentileSelector = percentileSelector;
            this.collectionSelector = collectionSelector;
            this.advancementDataSelector = advancementDataSelector;
            this.collectionTypeAndAmountSelector = collectionTypeAndAmountSelector;
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
            var advancements = advancementDataSelector.SelectFrom(Config.Name, TableNameConstants.TypeAndAmount.Advancements, creature);
            var creatureHitDice = collectionTypeAndAmountSelector.SelectOneFrom(Config.Name, TableNameConstants.TypeAndAmount.HitDice, creature);
            var maxHitDice = int.MaxValue;

            foreach (var template in templates)
            {
                var templateMaxHitDice = collectionTypeAndAmountSelector.SelectOneFrom(Config.Name, TableNameConstants.TypeAndAmount.HitDice, template);
                maxHitDice = Math.Min(templateMaxHitDice.Amount, maxHitDice);
            }

            var validAdvancements = advancements.Where(a => a.AdvancementIsValid(dice, maxHitDice - creatureHitDice.Amount));

            return validAdvancements;
        }

        public AdvancementDataSelection SelectRandomFor(string creature, IEnumerable<string> templates, CreatureType creatureType, string originalSize, string originalChallengeRating)
        {
            templates ??= [];

            var advancements = GetValidAdvancements(creature, templates);
            var randomAdvancement = collectionSelector.SelectRandomFrom(advancements);
            var selection = GetAdvancementSelection(creature, creatureType, originalSize, originalChallengeRating, randomAdvancement);

            return selection;
        }

        private AdvancementDataSelection GetAdvancementSelection(
            string creatureName,
            CreatureType creatureType,
            string originalSize,
            string originalChallengeRating,
            AdvancementDataSelection selection)
        {
            selection.SetAdditionalHitDice(dice);

            if (IsBarghest(creatureName))
            {
                selection.StrengthAdjustment = selection.AdditionalHitDice;
                selection.ConstitutionAdjustment = selection.AdditionalHitDice;
                selection.NaturalArmorAdjustment = selection.AdditionalHitDice;
                selection.CasterLevelAdjustment = selection.AdditionalHitDice;
            }

            selection.AdjustedChallengeRating = AdjustChallengeRating(originalSize, selection.Size, originalChallengeRating, selection.AdditionalHitDice, creatureType.Name);

            return selection;
        }

        private string AdjustChallengeRating(string size, string advancedSize, string originalChallengeRating, int additionalHitDice, string creatureType)
        {
            var sizeAdjustedChallengeRating = AdjustChallengeRating(size, advancedSize, originalChallengeRating);
            var hitDieAdjustedChallengeRating = AdjustChallengeRating(sizeAdjustedChallengeRating, additionalHitDice, creatureType);

            return hitDieAdjustedChallengeRating;
        }

        private bool IsBarghest(string creatureName)
        {
            return creatureName == CreatureConstants.Barghest || creatureName == CreatureConstants.Barghest_Greater;
        }

        private string AdjustChallengeRating(string originalSize, string advancedSize, string originalChallengeRating)
        {
            var sizes = SizeConstants.GetOrdered();
            var originalSizeIndex = Array.IndexOf(sizes, originalSize);
            var advancedIndex = Array.IndexOf(sizes, advancedSize);
            var largeIndex = Array.IndexOf(sizes, SizeConstants.Large);

            if (advancedIndex < largeIndex || originalSize == advancedSize)
            {
                return originalChallengeRating;
            }

            var increase = advancedIndex - Math.Max(largeIndex - 1, originalSizeIndex);
            var adjustedChallengeRating = ChallengeRatingConstants.IncreaseChallengeRating(originalChallengeRating, increase);

            return adjustedChallengeRating;
        }

        private string AdjustChallengeRating(string originalChallengeRating, int additionalHitDice, string creatureType)
        {
            var creatureTypeDivisor = typeAndAmountSelector.SelectOne(TableNameConstants.TypeAndAmount.Advancements, creatureType);
            var divisor = creatureTypeDivisor.Amount;
            var advancementAmount = additionalHitDice / divisor;

            return ChallengeRatingConstants.IncreaseChallengeRating(originalChallengeRating, advancementAmount);
        }
    }
}
