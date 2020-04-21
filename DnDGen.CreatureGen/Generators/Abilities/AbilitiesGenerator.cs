using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Items;
using DnDGen.CreatureGen.Selectors.Collections;
using DnDGen.CreatureGen.Tables;
using DnDGen.RollGen;
using DnDGen.TreasureGen.Items;
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

        public Dictionary<string, Ability> GenerateFor(string creatureName)
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
                abilities[selection.Type].BaseScore = dice.Roll(3).d6().AsSum();
            }

            var missingAbilities = allAbilities.Select(a => a.Type).Except(abilitySelections.Select(a => a.Type));

            foreach (var abilityName in missingAbilities)
            {
                abilities[abilityName].BaseScore = 0;
            }

            return abilities;
        }

        public Dictionary<string, Ability> SetMaxBonuses(Dictionary<string, Ability> abilities, Equipment equipment)
        {
            if (equipment.Armor != null)
            {
                var armor = equipment.Armor as Armor;
                abilities[AbilityConstants.Dexterity].MaxModifier = armor.MaxDexterityBonus;
            }

            if (equipment.Shield != null)
            {
                var shield = equipment.Shield as Armor;

                if (shield.MaxDexterityBonus < abilities[AbilityConstants.Dexterity].MaxModifier)
                {
                    abilities[AbilityConstants.Dexterity].MaxModifier = shield.MaxDexterityBonus;
                }
            }

            return abilities;
        }
    }
}