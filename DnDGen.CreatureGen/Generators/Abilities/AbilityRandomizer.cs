using DnDGen.CreatureGen.Abilities;
using DnDGen.RollGen;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Generators.Abilities
{
    public class AbilityRandomizer
    {
        public string Roll { get; set; }
        public Dictionary<string, int> SetRolls { get; set; }
        public string PriorityAbility { get; set; }
        public Dictionary<string, int> AbilityAdvancements { get; set; }

        public AbilityRandomizer()
        {
            Roll = AbilityConstants.RandomizerRolls.Default;
            SetRolls = [];
            AbilityAdvancements = [];
        }

        internal int Randomize(string ability, Dice dice)
        {
            if (SetRolls.ContainsKey(ability))
                return SetRolls[ability];

            return dice.Roll(Roll).AsSum();
        }

        internal bool Validate(string ability, Dice dice, int minimum, params int[] adjustments)
        {
            var adjustmentSum = adjustments.Sum();
            if (SetRolls.ContainsKey(ability))
                return SetRolls[ability] + adjustmentSum >= minimum;

            var max = dice.Roll(Roll).AsPotentialMaximum();
            return max + adjustmentSum >= minimum;
        }

        internal int GetAdjustment(Dice dice, Ability creatureAbility, int minimum)
        {
            if (SetRolls.ContainsKey(creatureAbility.Name))
                return 0;

            if (creatureAbility.FullScore >= minimum)
                return 0;

            //INFO: Can't use FullScore, since it baselines at 1. If BaseScore is 5 and RacialAdjustment is -6 the true score is -1, not 1.
            var adjustment = minimum - creatureAbility.RawScore;
            var maxRoll = dice.Roll(Roll).AsPotentialMaximum();
            var maxAdjustment = maxRoll - creatureAbility.BaseScore;

            if (adjustment > maxAdjustment)
                throw new InvalidOperationException($"Cannot increase ability {creatureAbility.Name} by {adjustment}, max allowed is {maxAdjustment}");

            return adjustment;
        }
    }
}
