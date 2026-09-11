using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Generators.Abilities;
using DnDGen.Infrastructure.Selectors.Collections;
using DnDGen.RollGen;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Integration
{
    public class AbilityRandomizerFactory(ICollectionSelector collectionSelector, Dice dice)
    {
        public AbilityRandomizer GetAbilityRandomizer(string[] templates, string[] rolls = null)
        {
            const string set = "Set";
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
                set
            ];

            var templatesWithMinimumAbilities = new[]
            {
                CreatureConstants.Templates.Ghost,
                CreatureConstants.Templates.HalfCelestial,
                CreatureConstants.Templates.HalfFiend,
            };

            if (templates.Intersect(templatesWithMinimumAbilities).Any())
            {
                rolls = [.. rolls.Except([AbilityConstants.RandomizerRolls.Poor])];
            }

            var randomizer = new AbilityRandomizer(collectionSelector.SelectRandomFrom(rolls));

            if (randomizer.Roll == set)
            {
                randomizer = new AbilityRandomizer(null);
                var setRoll = collectionSelector.SelectRandomFrom(rolls.Except([set]));

                randomizer.SetRolls[AbilityConstants.Strength] = dice.Roll(setRoll).AsSum();
                randomizer.SetRolls[AbilityConstants.Dexterity] = dice.Roll(setRoll).AsSum();
                randomizer.SetRolls[AbilityConstants.Constitution] = dice.Roll(setRoll).AsSum();
                randomizer.SetRolls[AbilityConstants.Intelligence] = dice.Roll(setRoll).AsSum();
                randomizer.SetRolls[AbilityConstants.Wisdom] = dice.Roll(setRoll).AsSum();
                randomizer.SetRolls[AbilityConstants.Charisma] = dice.Roll(setRoll).AsSum();
            }

            return randomizer;
        }
    }
}
