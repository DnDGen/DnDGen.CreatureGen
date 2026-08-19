using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Alignments;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Generators.Abilities;
using DnDGen.CreatureGen.Selectors.Selections;
using DnDGen.CreatureGen.Tables;
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
            var allData = creatureDataSelector.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureData);
            var allAlignments = collectionSelector.SelectAllFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups);
            var allAbilityAdjustments = typeAndAmountSelector.SelectAllFrom(Config.Name, TableNameConstants.TypeAndAmount.AbilityAdjustments);
            var allCasterLevels = typeAndAmountSelector.SelectAllFrom(Config.Name, TableNameConstants.TypeAndAmount.Casters);
            var abilityNames = allAbilityAdjustments[CreatureConstants.Human].Select(s => s.Type);

            abilityRandomizer ??= new AbilityRandomizer();

            foreach (var creature in creatureNames)
            {
                var creatureData = allData[creature].Single();
                var prototype = new CreaturePrototype
                {
                    Name = creature,
                    Abilities = allAbilityAdjustments[creature].ToDictionary(a => a.Type, a => new Ability(a.Type)
                    {
                        //INFO: Since prototypes are for Template validation, we only want the Maximum ability roll for a given ability
                        //This allows for potentially-high-roll randomizers to have creatures with low abilities to still meet Template Minimum Ability requirements
                        BaseScore = abilityRandomizer.GetMax(dice, a.Type),
                        RacialAdjustment = a.Amount
                    }),
                    Alignments = [.. allAlignments[creature].Select(a => new Alignment(a))],
                    CasterLevel = creatureData.CasterLevel,
                    Size = creatureData.Size,
                    ChallengeRating = creatureData.GetEffectiveChallengeRating(asCharacter),
                    HitDiceQuantity = creatureData.GetEffectiveHitDiceQuantity(asCharacter),
                    LevelAdjustment = creatureData.LevelAdjustment,
                    Type = new CreatureType(creatureData.Types),
                    HasSkeleton = creatureData.HasSkeleton,
                    AsCharacter = asCharacter,
                };

                var missingAbilityNames = abilityNames.Except(prototype.Abilities.Keys).ToArray();
                foreach (var missingAbility in missingAbilityNames)
                {
                    prototype.Abilities[missingAbility] = new Ability(missingAbility) { BaseScore = 0 };
                }

                //INFO: Since prototypes are for Template validation, we only want the Maximum caster level between spellcasting and at-will abilities
                //The caster type/amount is equivalent to the Magic caster level, as opposed to the caster level on the creature data
                if (allCasterLevels[creature].Any())
                {
                    var maxLevel = allCasterLevels[creature].Max(c => c.Amount);
                    prototype.CasterLevel = Math.Max(maxLevel, creatureData.CasterLevel);
                }

                yield return prototype;
            }
        }
    }
}
