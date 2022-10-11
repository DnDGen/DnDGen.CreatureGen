using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Items;
using DnDGen.CreatureGen.Selectors.Collections;
using DnDGen.CreatureGen.Tables;
using DnDGen.RollGen;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Generators.Abilities
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

        public Dictionary<string, Ability> GenerateFor(string creatureName, AbilityRandomizer randomizer)
        {
            var abilitySelections = typeAndAmountSelector.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, creatureName);
            var allAbilities = typeAndAmountSelector.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, GroupConstants.All);
            var abilities = new Dictionary<string, Ability>();

            foreach (var selection in allAbilities)
            {
                abilities[selection.Type] = new Ability(selection.Type);
            }

            foreach (var selection in abilitySelections)
            {
                abilities[selection.Type].RacialAdjustment = selection.Amount;

                if (randomizer.AbilityAdvancements.ContainsKey(selection.Type))
                {
                    abilities[selection.Type].AdvancementAdjustment = randomizer.AbilityAdvancements[selection.Type];
                }

                if (randomizer.SetRolls.ContainsKey(selection.Type))
                {
                    abilities[selection.Type].BaseScore = randomizer.SetRolls[selection.Type];
                }
                else
                {
                    abilities[selection.Type].BaseScore = dice.Roll(randomizer.Roll).AsSum();
                }
            }

            if (randomizer.PriorityAbility != null && abilities.ContainsKey(randomizer.PriorityAbility))
            {
                var maxAbilityScore = abilities.Values.Max(a => a.BaseScore);
                var maxAbility = abilities.Values.First(a => a.BaseScore == maxAbilityScore);
                var originalAbilityScore = abilities[randomizer.PriorityAbility].BaseScore;

                abilities[randomizer.PriorityAbility].BaseScore = maxAbilityScore;
                maxAbility.BaseScore = originalAbilityScore;
            }

            var missingAbilities = allAbilities.Select(a => a.Type).Except(abilitySelections.Select(a => a.Type));

            foreach (var abilityName in missingAbilities)
            {
                abilities[abilityName].BaseScore = 0;
            }

            throw new NotImplementedException("Apply age effects to abilities");

            return abilities;
        }

        public Dictionary<string, Ability> SetMaxBonuses(Dictionary<string, Ability> abilities, Equipment equipment)
        {
            if (equipment.Armor != null)
            {
                abilities[AbilityConstants.Dexterity].MaxModifier = equipment.Armor.MaxDexterityBonus;
            }

            if (equipment.Shield != null)
            {
                if (equipment.Shield.MaxDexterityBonus < abilities[AbilityConstants.Dexterity].MaxModifier)
                {
                    abilities[AbilityConstants.Dexterity].MaxModifier = equipment.Shield.MaxDexterityBonus;
                }
            }

            return abilities;
        }
    }
}