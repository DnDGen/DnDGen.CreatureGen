using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Items;
using DnDGen.CreatureGen.Tables;
using DnDGen.CreatureGen.Templates;
using DnDGen.CreatureGen.Verifiers;
using DnDGen.CreatureGen.Verifiers.Exceptions;
using DnDGen.Infrastructure.Factories;
using DnDGen.Infrastructure.Selectors.Collections;
using DnDGen.RollGen;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Generators.Abilities
{
    internal class AbilitiesGenerator(
        ICollectionTypeAndAmountSelector typeAndAmountSelector,
        Dice dice,
        JustInTimeFactory factory,
        ICreatureVerifier creatureVerifier) : IAbilitiesGenerator
    {
        public Dictionary<string, Ability> GenerateFor(string creatureName, string[] templates, bool asCharacter, AbilityRandomizer randomizer, Demographics demographics)
        {
            randomizer ??= new();

            //This follows advice from the summary comment on creatureVerifier.VerifyCompatibility
            if (templates.Length == 0)
                templates = [CreatureConstants.Templates.None];

            var valid = creatureVerifier.VerifyCompatibility(asCharacter, creatureName, randomizer, null, templates);
            if (!valid)
                throw new InvalidCreatureException(
                    $"{creatureName} does not have sufficient ability for template {templates[0]}",
                    false,
                    creatureName,
                    null,
                    randomizer,
                    templates);

            var abilities = InitializeAbilities(creatureName);
            ApplyRandomizer(abilities, randomizer);
            ApplyAge(abilities, demographics);
            ApplyTemplateMinimum(abilities, templates, randomizer);

            return abilities;
        }

        private Dictionary<string, Ability> InitializeAbilities(string creatureName)
        {
            var abilitySelections = typeAndAmountSelector.SelectFrom(Config.Name, TableNameConstants.TypeAndAmount.AbilityAdjustments, creatureName);
            var allAbilities = typeAndAmountSelector.SelectFrom(Config.Name, TableNameConstants.TypeAndAmount.AbilityAdjustments, GroupConstants.All);
            var abilities = new Dictionary<string, Ability>();

            foreach (var selection in allAbilities)
            {
                abilities[selection.Type] = new Ability(selection.Type);
            }

            foreach (var selection in abilitySelections)
            {
                abilities[selection.Type].RacialAdjustment = selection.Amount;
            }

            var missingAbilities = allAbilities.Select(a => a.Type).Except(abilitySelections.Select(a => a.Type));

            foreach (var abilityName in missingAbilities)
            {
                abilities[abilityName].BaseScore = 0;
            }

            return abilities;
        }

        private void ApplyRandomizer(Dictionary<string, Ability> abilities, AbilityRandomizer randomizer)
        {
            foreach (var abilityKvp in abilities)
            {
                if (!abilityKvp.Value.HasScore)
                    continue;

                if (randomizer.AbilityAdvancements.ContainsKey(abilityKvp.Key))
                {
                    abilityKvp.Value.AdvancementAdjustment = randomizer.AbilityAdvancements[abilityKvp.Key];
                }

                abilityKvp.Value.BaseScore = randomizer.Randomize(abilityKvp.Key, dice);
            }

            if (randomizer.PriorityAbility != null && abilities.ContainsKey(randomizer.PriorityAbility))
            {
                var maxAbilityScore = abilities.Values.Max(a => a.BaseScore);
                var sourceAbility = abilities.Values.First(a => a.BaseScore == maxAbilityScore);
                var sourceAbilityScore = abilities[randomizer.PriorityAbility].BaseScore;

                abilities[randomizer.PriorityAbility].BaseScore = maxAbilityScore;
                sourceAbility.BaseScore = sourceAbilityScore;
            }
        }

        private void ApplyAge(Dictionary<string, Ability> abilities, Demographics demographics)
        {
            var ageAbilities = typeAndAmountSelector.SelectFrom(Config.Name, TableNameConstants.TypeAndAmount.AbilityAdjustments, demographics.Age.Description);

            foreach (var selection in ageAbilities)
            {
                if (!abilities.ContainsKey(selection.Type))
                    continue;

                abilities[selection.Type].AgeAdjustment = selection.Amount;
            }
        }

        private void ApplyTemplateMinimum(Dictionary<string, Ability> abilities, string[] templates, AbilityRandomizer abilityRandomizer)
        {
            for (var i = 0; i < templates.Length; i++)
            {
                var applicator = factory.Build<TemplateApplicator>(templates[i]);
                if (applicator.MinimumAbility == null)
                    continue;

                var creatureAbility = abilities[applicator.MinimumAbility.Name];
                var adjustment = abilityRandomizer.GetAdjustment(dice, creatureAbility, applicator.MinimumAbility.FullScore);
                abilities[applicator.MinimumAbility.Name].BaseScore += adjustment;
            }
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