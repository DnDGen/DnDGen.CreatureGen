using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Generators.Abilities;
using DnDGen.CreatureGen.Generators.Creatures;
using DnDGen.CreatureGen.Verifiers;
using DnDGen.Infrastructure.Selectors.Collections;
using DnDGen.RollGen;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Integration
{
    public class AbilityRandomizerFactory(ICollectionSelector collectionSelector, Dice dice, ICreatureVerifier creatureVerifier)
    {
        private const string Set = "Set";

        public AbilityRandomizer GetRandomAbilityRandomizer(string[] rolls = null)
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

            var randomizer = new AbilityRandomizer(collectionSelector.SelectRandomFrom(rolls));

            if (randomizer.Roll == Set)
            {
                randomizer = new AbilityRandomizer(null);
                randomizer.SetRolls[AbilityConstants.Strength] = dice.Roll(AbilityConstants.RandomizerRolls.Raw).AsSum();
                randomizer.SetRolls[AbilityConstants.Dexterity] = dice.Roll(AbilityConstants.RandomizerRolls.Raw).AsSum();
                randomizer.SetRolls[AbilityConstants.Constitution] = dice.Roll(AbilityConstants.RandomizerRolls.Raw).AsSum();
                randomizer.SetRolls[AbilityConstants.Intelligence] = dice.Roll(AbilityConstants.RandomizerRolls.Raw).AsSum();
                randomizer.SetRolls[AbilityConstants.Wisdom] = dice.Roll(AbilityConstants.RandomizerRolls.Raw).AsSum();
                randomizer.SetRolls[AbilityConstants.Charisma] = dice.Roll(AbilityConstants.RandomizerRolls.Raw).AsSum();
            }

            return randomizer;
        }

        public AbilityRandomizer GetRandomAbilityRandomizerForTemplates(bool asCharacter, string[] templates, Filters filters, string[] rolls = null)
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

            var validRolls = rolls.Where(r => creatureVerifier.VerifyCompatibility(asCharacter, null, new(r), filters, templates));
            if (!validRolls.Any())
            {
                //INFO: This means the templates themselves are not compatible. This sometimes happens in randomly-generated verifier test cases.
                return new();
            }

            var randomizer = GetRandomAbilityRandomizer([.. validRolls.Concat([Set])]);
            var valid = creatureVerifier.VerifyCompatibility(asCharacter, null, randomizer, filters, templates);

            //HACK: For some filters, they are only valid with ability-dependent templates
            //Since this method assumes it will return a randomizer valid for the given inputs,
            //We need to adjust the set values to make this work
            if (!valid && randomizer.SetRolls.Count > 0)
            {
                randomizer.SetRolls[AbilityConstants.Charisma] = Ability.DefaultScore + 6;
                randomizer.SetRolls[AbilityConstants.Intelligence] = Ability.DefaultScore + 4;
            }

            return randomizer;
        }

        public AbilityRandomizer GetRandomAbilityRandomizerForCreatureAndTemplates(bool asCharacter, string creature, string[] templates, string[] rolls = null)
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
                .Where(r => creatureVerifier.VerifyCompatibility(asCharacter, creature, new(r), null, templates))
                .ToArray();

            var randomizer = GetRandomAbilityRandomizerForTemplates(asCharacter, templates, null, rolls);
            var valid = creatureVerifier.VerifyCompatibility(asCharacter, creature, randomizer, null, templates);

            //HACK: The creature might be paired with an ability-dependent template
            //Since this method assumes it will return a randomizer valid for the given inputs,
            //We need to adjust the set values to make this work
            if (!valid && randomizer.SetRolls.Count > 0)
            {
                randomizer.SetRolls[AbilityConstants.Charisma] = Ability.DefaultScore + 6;
                randomizer.SetRolls[AbilityConstants.Intelligence] = Ability.DefaultScore + 4;
            }

            return randomizer;
        }
    }
}
