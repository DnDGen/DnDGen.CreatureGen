using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Alignments;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Selectors.Collections;
using DnDGen.CreatureGen.Tables;
using DnDGen.Infrastructure.Selectors.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Generators.Creatures
{
    internal class CreaturePrototypeFactory : ICreaturePrototypeFactory
    {
        private readonly ICollectionSelector collectionSelector;
        private readonly IAdjustmentsSelector adjustmentSelector;
        private readonly ICreatureDataSelector creatureDataSelector;
        private readonly ITypeAndAmountSelector typeAndAmountSelector;

        public CreaturePrototypeFactory(
            ICollectionSelector collectionSelector,
            IAdjustmentsSelector adjustmentSelector,
            ICreatureDataSelector creatureDataSelector,
            ITypeAndAmountSelector typeAndAmountSelector)
        {
            this.collectionSelector = collectionSelector;
            this.adjustmentSelector = adjustmentSelector;
            this.creatureDataSelector = creatureDataSelector;
            this.typeAndAmountSelector = typeAndAmountSelector;
        }

        public IEnumerable<CreaturePrototype> Build(IEnumerable<string> creatureNames, bool asCharacter)
        {
            var allData = creatureDataSelector.SelectAll();
            var allHitDice = adjustmentSelector.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice);
            var allTypes = collectionSelector.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureTypes);
            var allAlignments = collectionSelector.SelectAllFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups);
            var allAbilityAdjustments = typeAndAmountSelector.SelectAll(TableNameConstants.TypeAndAmount.AbilityAdjustments);
            var allCasterLevels = typeAndAmountSelector.SelectAll(TableNameConstants.TypeAndAmount.Casters);
            var abilityNames = allAbilityAdjustments[CreatureConstants.Human].Select(s => s.Type);

            foreach (var creature in creatureNames)
            {
                var prototype = new CreaturePrototype();
                prototype.Name = creature;
                prototype.Abilities = allAbilityAdjustments[creature]
                    .ToDictionary(a => a.Type, a => new Ability(a.Type) { RacialAdjustment = a.Amount });
                prototype.Alignments = allAlignments[creature].Select(a => new Alignment(a)).Distinct().ToList();
                prototype.CasterLevel = allData[creature].CasterLevel;
                prototype.Size = allData[creature].Size;
                prototype.ChallengeRating = allData[creature].ChallengeRating;
                prototype.HitDiceQuantity = allHitDice[creature];
                prototype.LevelAdjustment = allData[creature].LevelAdjustment;
                prototype.Type = new CreatureType(allTypes[creature]);

                if (asCharacter && prototype.HitDiceQuantity <= 1 && prototype.Type.Name == CreatureConstants.Types.Humanoid)
                {
                    prototype.ChallengeRating = ChallengeRatingConstants.CR0;
                }

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
                    prototype.CasterLevel = Math.Max(maxLevel, allData[creature].CasterLevel);
                }

                yield return prototype;
            }
        }
    }
}
