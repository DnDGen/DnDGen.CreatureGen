using CreatureGen.Abilities;
using CreatureGen.Selectors.Collections;
using CreatureGen.Tables;
using RollGen;
using System.Collections.Generic;
using System.Linq;

namespace CreatureGen.Generators.Abilities
{
    internal class AbilitiesGenerator : IAbilitiesGenerator
    {
        private readonly ITypeAndAmountSelector typeAndAmountSelector;
        private readonly Dice dice;

        public AbilitiesGenerator(ITypeAndAmountSelector typeAndAmountSelector, Dice dice)
        {
            this.typeAndAmountSelector = typeAndAmountSelector;
            this.dice = dice;
        }

        public Dictionary<string, Ability> GenerateFor(string creatureName)
        {
            var abilitySelections = typeAndAmountSelector.Select(TableNameConstants.Set.TypeAndAmount.AbilityAdjustments, creatureName);
            var allAbilities = typeAndAmountSelector.Select(TableNameConstants.Set.TypeAndAmount.AbilityAdjustments, GroupConstants.All);
            var abilities = new Dictionary<string, Ability>();

            foreach (var selection in allAbilities)
            {
                abilities[selection.Type] = new Ability(selection.Type);
            }

            foreach (var selection in abilitySelections)
            {
                abilities[selection.Type].RacialAdjustment = selection.Amount;
                abilities[selection.Type].BaseScore = dice.Roll(3).d6().AsSum();
            }

            var missingAbilities = allAbilities.Select(a => a.Type).Except(abilitySelections.Select(a => a.Type));

            foreach (var abilityName in missingAbilities)
            {
                abilities[abilityName].BaseScore = 0;
            }

            return abilities;
        }
    }
}