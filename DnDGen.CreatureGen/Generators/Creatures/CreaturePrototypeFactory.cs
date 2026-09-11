using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Alignments;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Generators.Abilities;
using DnDGen.CreatureGen.Selectors.Selections;
using DnDGen.CreatureGen.Tables;
using DnDGen.Infrastructure.Models;
using DnDGen.Infrastructure.Selectors.Collections;
using DnDGen.RollGen;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Generators.Creatures
{
    internal class CreaturePrototypeFactory(
        ICollectionSelector collectionSelector,
        ICollectionDataSelector<CreatureDataSelection> creatureDataSelector,
        ICollectionTypeAndAmountSelector typeAndAmountSelector,
        Dice dice) : ICreaturePrototypeFactory
    {
        public IEnumerable<CreaturePrototype> Build(IEnumerable<string> creatureNames, bool asCharacter, AbilityRandomizer abilityRandomizer = null)
        {
            //If we only have 1 creature, avoid the SelectAll calls
            if (creatureNames.Count() == 1)
            {
                yield return Build(creatureNames.Single(), asCharacter, abilityRandomizer);
                yield break;
            }

            var allData = creatureDataSelector.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureData);
            var allAlignments = collectionSelector.SelectAllFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups);
            var allAbilityAdjustments = typeAndAmountSelector.SelectAllFrom(Config.Name, TableNameConstants.TypeAndAmount.AbilityAdjustments);
            var allCasterLevels = typeAndAmountSelector.SelectAllFrom(Config.Name, TableNameConstants.TypeAndAmount.Casters);
            var abilityNames = allAbilityAdjustments[CreatureConstants.Human].Select(s => s.Type);

            abilityRandomizer ??= new AbilityRandomizer();

            foreach (var creature in creatureNames)
            {
                var creatureData = allData[creature].Single();
                yield return Build(
                    creature,
                    asCharacter,
                    abilityRandomizer,
                    allAbilityAdjustments[creature],
                    abilityNames,
                    allAlignments[creature],
                    creatureData,
                    allCasterLevels[creature]);
            }
        }

        public CreaturePrototype Build(string creatureName, bool asCharacter, AbilityRandomizer abilityRandomizer = null)
        {
            var creatureData = creatureDataSelector.SelectOneFrom(Config.Name, TableNameConstants.Collection.CreatureData, creatureName);
            var alignments = collectionSelector.SelectFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups, creatureName);
            var abilityAdjustments = typeAndAmountSelector.SelectFrom(Config.Name, TableNameConstants.TypeAndAmount.AbilityAdjustments, creatureName);
            var casterLevels = typeAndAmountSelector.SelectFrom(Config.Name, TableNameConstants.TypeAndAmount.Casters, creatureName);
            var abilityNames = typeAndAmountSelector.SelectFrom(Config.Name, TableNameConstants.TypeAndAmount.AbilityAdjustments, CreatureConstants.Human).Select(s => s.Type);

            abilityRandomizer ??= new AbilityRandomizer();

            return Build(creatureName, asCharacter, abilityRandomizer, abilityAdjustments, abilityNames, alignments, creatureData, casterLevels);
        }

        private CreaturePrototype Build(
            string creatureName,
            bool asCharacter,
            AbilityRandomizer abilityRandomizer,
            IEnumerable<TypeAndAmountDataSelection> abilityAdjustments,
            IEnumerable<string> allAbilityNames,
            IEnumerable<string> alignments,
            CreatureDataSelection creatureData,
            IEnumerable<TypeAndAmountDataSelection> casterLevels)
        {
            var prototype = new CreaturePrototype
            {
                Name = creatureName,
                Abilities = abilityAdjustments.ToDictionary(a => a.Type, a => new Ability(a.Type)
                {
                    //INFO: Since prototypes are for Template validation, we only want the Maximum ability roll for a given ability
                    //This allows for potentially-high-roll randomizers to have creatures with low abilities to still meet Template Minimum Ability requirements
                    BaseScore = abilityRandomizer.GetMax(dice, a.Type),
                    RacialAdjustment = a.Amount
                }),
                Alignments = [.. alignments.Select(a => new Alignment(a))],
                AsCharacter = asCharacter,
                CasterLevel = creatureData.CasterLevel,
                ChallengeRating = creatureData.GetEffectiveChallengeRating(asCharacter),
                HasSkeleton = creatureData.HasSkeleton,
                HitDiceQuantity = creatureData.GetEffectiveHitDiceQuantity(asCharacter),
                LevelAdjustment = creatureData.LevelAdjustment,
                Size = creatureData.Size,
                Type = new CreatureType(creatureData.Types),
            };

            var missingAbilityNames = allAbilityNames.Except(prototype.Abilities.Keys);
            foreach (var missingAbility in missingAbilityNames)
            {
                prototype.Abilities[missingAbility] = new Ability(missingAbility) { BaseScore = 0 };
            }

            //INFO: Since prototypes are for Template validation, we only want the Maximum caster level between spellcasting and at-will abilities
            //The caster type/amount is equivalent to the Magic caster level, as opposed to the caster level on the creature data
            if (casterLevels.Any())
            {
                var maxLevel = casterLevels.Max(c => c.Amount);
                prototype.CasterLevel = Math.Max(maxLevel, creatureData.CasterLevel);
            }

            return prototype;
        }

        public CreaturePrototype Clone(CreaturePrototype source)
        {
            var clone = new CreaturePrototype
            {
                Name = source.Name,
                Abilities = source.Abilities.ToDictionary(
                    kvp => kvp.Key,
                    kvp => new Ability(kvp.Value.Name) { BaseScore = kvp.Value.BaseScore, RacialAdjustment = kvp.Value.RacialAdjustment }),
                Alignments = [.. source.Alignments.Select(a => new Alignment(a.Full))],
                AsCharacter = source.AsCharacter,
                CasterLevel = source.CasterLevel,
                ChallengeRating = source.ChallengeRating,
                HasSkeleton = source.HasSkeleton,
                HitDiceQuantity = source.HitDiceQuantity,
                LevelAdjustment = source.LevelAdjustment,
                Size = source.Size,
                Type = new CreatureType(source.Type.AllTypes)
            };

            return clone;
        }
    }
}
