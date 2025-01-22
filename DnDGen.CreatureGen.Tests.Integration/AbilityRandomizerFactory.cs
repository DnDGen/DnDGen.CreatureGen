using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Generators.Abilities;
using DnDGen.Infrastructure.Selectors.Collections;
using DnDGen.RollGen;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Integration
{
    public class AbilityRandomizerFactory
    {
        private readonly ICollectionSelector collectionSelector;
        private readonly Dictionary<string, (string Ability, int Minimum)> templateAbilityMinimums;
        private readonly Dice dice;

        public AbilityRandomizerFactory(ICollectionSelector collectionSelector, Dice dice)
        {
            this.collectionSelector = collectionSelector;
            this.dice = dice;

            templateAbilityMinimums = new Dictionary<string, (string Ability, int Minimum)>
            {
                [CreatureConstants.Templates.Ghost] = (AbilityConstants.Charisma, 6),
                [CreatureConstants.Templates.HalfCelestial] = (AbilityConstants.Intelligence, 4),
                [CreatureConstants.Templates.HalfFiend] = (AbilityConstants.Intelligence, 4)
            };
        }

        public AbilityRandomizer GetAbilityRandomizer(string[] templates, string[] rolls = null)
        {
            rolls ??=
            [
                AbilityConstants.RandomizerRolls.Heroic,
                AbilityConstants.RandomizerRolls.BestOfFour,
                AbilityConstants.RandomizerRolls.Default,
                AbilityConstants.RandomizerRolls.Average,
                AbilityConstants.RandomizerRolls.Good,
                AbilityConstants.RandomizerRolls.OnesAsSixes,
                AbilityConstants.RandomizerRolls.Poor,
                AbilityConstants.RandomizerRolls.Raw,
                AbilityConstants.RandomizerRolls.Wild,
            ];

            var randomizer = new AbilityRandomizer
            {
                Roll = collectionSelector.SelectRandomFrom(rolls)
            };

            //HACK: This is just to avoid the issue when a randomly-rolled ability
            //(especially with "Poor" or "Wild") ends up much lower than normally would be with the "Default" roll,
            //and the template requires an ability to be a minimum value
            if (templates.Any(t => t != null && templateAbilityMinimums.ContainsKey(t)))
            {
                foreach (var template in templates.Where(templateAbilityMinimums.ContainsKey))
                {
                    if (!randomizer.AbilityAdvancements.ContainsKey(templateAbilityMinimums[template].Ability))
                    {
                        randomizer.AbilityAdvancements[templateAbilityMinimums[template].Ability] = 0;
                    }

                    var newMin = Math.Max(randomizer.AbilityAdvancements[templateAbilityMinimums[template].Ability], templateAbilityMinimums[template].Minimum);
                    randomizer.AbilityAdvancements[templateAbilityMinimums[template].Ability] = newMin;
                    randomizer.PriorityAbility = templateAbilityMinimums[template].Ability;
                }
            }
            else if (templates.Contains(null))
            {
                //HACK: Here, the template might be randomly selected, so we have to guard against it for the sake of stress testing
                foreach (var kvp in templateAbilityMinimums)
                {
                    if (dice.Roll(randomizer.Roll).AsPotentialMinimum() < kvp.Value.Minimum)
                    {
                        randomizer.AbilityAdvancements[kvp.Value.Ability] = kvp.Value.Minimum;
                    }
                }
            }

            return randomizer;
        }
    }
}
