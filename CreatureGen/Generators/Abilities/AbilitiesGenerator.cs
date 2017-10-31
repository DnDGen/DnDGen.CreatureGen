using CreatureGen.Abilities;
using CreatureGen.Selectors.Collections;
using CreatureGen.Tables;
using RollGen;
using System.Collections.Generic;

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
            var abilitySelections = typeAndAmountSelector.Select(TableNameConstants.Set.Collection.AbilityGroups, creatureName);
            var abilities = new Dictionary<string, Ability>();

            foreach (var selection in abilitySelections)
            {
                abilities[selection.Type] = new Ability(selection.Type);
                abilities[selection.Type].RacialAdjustment = selection.Amount;
                abilities[selection.Type].BaseValue = dice.Roll(3).d6().AsSum();
            }

            return abilities;
        }
    }
}