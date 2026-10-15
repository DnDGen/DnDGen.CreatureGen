using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Generators.Abilities;
using DnDGen.CreatureGen.Verifiers;
using DnDGen.Infrastructure.Selectors.Collections;
using DnDGen.RollGen;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Integration
{
    public class AbilityRandomizerFactory(ICollectionSelector collectionSelector, Dice dice, ICreatureVerifier creatureVerifier)
    {
        private const string Set = "Set";

        public AbilityRandomizer GetAbilityRandomizer(string[] rolls = null)
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
                Set
            ];

            if (!rolls.Except([Set]).Any())
                throw new ArgumentException("Only Set was provided, so no other roll could be uswed to generate set abilities", nameof(rolls));

            var randomizer = new AbilityRandomizer(collectionSelector.SelectRandomFrom(rolls));

            if (randomizer.Roll == Set)
            {
                randomizer = new AbilityRandomizer(null);
                var setRoll = collectionSelector.SelectRandomFrom(rolls.Except([Set]));

                randomizer.SetRolls[AbilityConstants.Strength] = dice.Roll(setRoll).AsSum();
                randomizer.SetRolls[AbilityConstants.Dexterity] = dice.Roll(setRoll).AsSum();
                randomizer.SetRolls[AbilityConstants.Constitution] = dice.Roll(setRoll).AsSum();
                randomizer.SetRolls[AbilityConstants.Intelligence] = dice.Roll(setRoll).AsSum();
                randomizer.SetRolls[AbilityConstants.Wisdom] = dice.Roll(setRoll).AsSum();
                randomizer.SetRolls[AbilityConstants.Charisma] = dice.Roll(setRoll).AsSum();
            }

            return randomizer;
        }

        public AbilityRandomizer GetAbilityRandomizerForTemplates(string[] templates, string[] rolls = null)
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

            var validRolls = rolls.Where(r => creatureVerifier.VerifyCompatibility(false, null, new(r), null, templates));
            if (!validRolls.Any())
            {
                //INFO: This means the templates themselves are not compatible. This sometimes happens in randomly-generated verifier test cases.
                return new();
            }

            var randomizer = GetAbilityRandomizer([.. validRolls.Concat([Set])]);

            var templatesWithMinimumAbilities = new Dictionary<string, (string a, int min)>
            {
                [CreatureConstants.Templates.Ghost] = (AbilityConstants.Charisma, 6),
                [CreatureConstants.Templates.HalfCelestial] = (AbilityConstants.Intelligence, 4),
                [CreatureConstants.Templates.HalfFiend] = (AbilityConstants.Intelligence, 4),
            };

            foreach (var template in templates)
            {
                //HACK: If we are using set rolls AND have templates that require minimum abilities, we need to make sure we match those minimums
                //In the real world, the validator would catch it before trying to generate, so this is a test-specific circumstance
                if (templatesWithMinimumAbilities.ContainsKey(template) && randomizer.SetRolls.ContainsKey(templatesWithMinimumAbilities[template].a))
                {
                    randomizer.SetRolls[templatesWithMinimumAbilities[template].a] = Ability.DefaultScore + templatesWithMinimumAbilities[template].min;
                }
            }

            return randomizer;
        }

        public AbilityRandomizer GetAbilityRandomizerForCreatureAndTemplates(string creature, string[] templates, string[] rolls = null)
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

            rolls = rolls
                .Where(r => creatureVerifier.VerifyCompatibility(false, creature, new(r), null, templates))
                .ToArray();

            return GetAbilityRandomizerForTemplates(templates, rolls);
        }
    }
}
