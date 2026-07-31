using DnDGen.CreatureGen.Abilities;
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

            var randomizer = new AbilityRandomizer
            {
                Roll = collectionSelector.SelectRandomFrom(rolls)
            };

            if (randomizer.Roll == set)
            {
                randomizer.Roll = string.Empty;
                var setRoll = collectionSelector.SelectRandomFrom(rolls.Except([set]));

                randomizer.SetRolls[AbilityConstants.Strength] = dice.Roll(setRoll).AsSum();
                randomizer.SetRolls[AbilityConstants.Dexterity] = dice.Roll(setRoll).AsSum();
                randomizer.SetRolls[AbilityConstants.Constitution] = dice.Roll(setRoll).AsSum();
                randomizer.SetRolls[AbilityConstants.Intelligence] = dice.Roll(setRoll).AsSum();
                randomizer.SetRolls[AbilityConstants.Wisdom] = dice.Roll(setRoll).AsSum();
                randomizer.SetRolls[AbilityConstants.Charisma] = dice.Roll(setRoll).AsSum();
            }

            ////HACK: This is just to avoid the issue when a randomly-rolled ability
            ////(especially with "Poor" or "Wild") ends up much lower than normally would be with the "Default" roll,
            ////and the template requires an ability to be a minimum value
            //if (templates.Any(t => t != null && templateAbilityMinimums.ContainsKey(t)))
            //{
            //    foreach (var template in templates.Where(templateAbilityMinimums.ContainsKey))
            //    {
            //        if (!randomizer.AbilityAdvancements.ContainsKey(templateAbilityMinimums[template].Ability))
            //        {
            //            randomizer.AbilityAdvancements[templateAbilityMinimums[template].Ability] = 0;
            //        }

            //        var newMin = Math.Max(randomizer.AbilityAdvancements[templateAbilityMinimums[template].Ability], templateAbilityMinimums[template].Minimum);
            //        randomizer.AbilityAdvancements[templateAbilityMinimums[template].Ability] = newMin;
            //        randomizer.PriorityAbility = templateAbilityMinimums[template].Ability;
            //    }
            //}
            //else if (templates.Contains(null))
            //{
            //    //HACK: Here, the template might be randomly selected, so we have to guard against it for the sake of stress testing
            //    foreach (var kvp in templateAbilityMinimums)
            //    {
            //        if (dice.Roll(randomizer.Roll).AsPotentialMinimum() < kvp.Value.Minimum)
            //        {
            //            randomizer.AbilityAdvancements[kvp.Value.Ability] = kvp.Value.Minimum;
            //        }
            //    }
            //}

            return randomizer;
        }
    }
}
