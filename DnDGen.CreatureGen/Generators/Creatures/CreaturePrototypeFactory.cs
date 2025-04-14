using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Alignments;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Selectors.Selections;
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
        private readonly ICollectionDataSelector<CreatureDataSelection> creatureDataSelector;
        private readonly ICollectionTypeAndAmountSelector typeAndAmountSelector;

        public CreaturePrototypeFactory(
            ICollectionSelector collectionSelector,
            ICollectionDataSelector<CreatureDataSelection> creatureDataSelector,
            ICollectionTypeAndAmountSelector typeAndAmountSelector)
        {
            this.collectionSelector = collectionSelector;
            this.creatureDataSelector = creatureDataSelector;
            this.typeAndAmountSelector = typeAndAmountSelector;
        }

        public IEnumerable<CreaturePrototype> Build(IEnumerable<string> creatureNames, bool asCharacter)
        {
            var allData = creatureDataSelector.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureData);
            var allHitDice = typeAndAmountSelector.SelectAllFrom(Config.Name, TableNameConstants.TypeAndAmount.HitDice);
            var allTypes = collectionSelector.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureTypes);
            var allAlignments = collectionSelector.SelectAllFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups);
            var allAbilityAdjustments = typeAndAmountSelector.SelectAllFrom(Config.Name, TableNameConstants.TypeAndAmount.AbilityAdjustments);
            var allCasterLevels = typeAndAmountSelector.SelectAllFrom(Config.Name, TableNameConstants.TypeAndAmount.Casters);
            var abilityNames = allAbilityAdjustments[CreatureConstants.Human].Select(s => s.Type);

            foreach (var creature in creatureNames)
            {
                var creatureData = allData[creature].Single();
                var hitDice = allHitDice[creature].Single();
                var prototype = new CreaturePrototype
                {
                    Name = creature,
                    Abilities = allAbilityAdjustments[creature].ToDictionary(a => a.Type, a => new Ability(a.Type) { RacialAdjustment = a.Amount }),
                    Alignments = [.. allAlignments[creature].Select(a => new Alignment(a)).Distinct()],
                    CasterLevel = creatureData.CasterLevel,
                    Size = creatureData.Size,
                    ChallengeRating = creatureData.ChallengeRating,
                    HitDiceQuantity = hitDice.AmountAsDouble,
                    LevelAdjustment = creatureData.LevelAdjustment,
                    Type = new CreatureType(allTypes[creature])
                };

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
                    prototype.CasterLevel = Math.Max(maxLevel, creatureData.CasterLevel);
                }

                yield return prototype;
            }
        }
    }
}
