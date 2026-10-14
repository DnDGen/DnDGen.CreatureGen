using DnDGen.CreatureGen.Abilities;
using DnDGen.RollGen;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Generators.Abilities
{
    public class AbilityRandomizer
    {
        public string Roll { get; private set; }
        public Dictionary<string, int> SetRolls { get; set; }
        public string PriorityAbility { get; set; }
        public Dictionary<string, int> AbilityAdvancements { get; set; }

        private int? maxRoll;
        private int? minRoll;

        public AbilityRandomizer(string roll) : this()
        {
            Roll = roll;
        }

        public AbilityRandomizer()
        {
            Roll = AbilityConstants.RandomizerRolls.Default;
            SetRolls = [];
            AbilityAdvancements = [];
            maxRoll = null;
            minRoll = null;
        }

        public bool Validate(Dice dice)
        {
            var valid = true;

            if (!string.IsNullOrEmpty(Roll))
            {
                var min = GetMinRoll(dice);
                valid &= min >= 1;
            }

            if (SetRolls?.Count > 0)
            {
                valid &= SetRolls.Values.All(v => v >= 1);
            }

            return valid;
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
            var max = GetMax(dice, ability);
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
            var maxRoll = GetMaxRoll(dice);
            var maxAdjustment = maxRoll - creatureAbility.BaseScore;

            if (adjustment > maxAdjustment)
                throw new InvalidOperationException($"Cannot increase ability {creatureAbility.Name} by {adjustment}, max allowed is {maxAdjustment}");

            return adjustment;
        }

        internal (int min, int max) GetRange(Dice dice, string abilityName) => (GetMin(dice, abilityName), GetMax(dice, abilityName));

        internal int GetMin(Dice dice, string abilityName)
        {
            if (SetRolls.ContainsKey(abilityName))
                return SetRolls[abilityName];

            return GetMinRoll(dice);
        }

        private int GetMinRoll(Dice dice)
        {
            minRoll ??= dice.Roll(Roll).AsPotentialMinimum();
            return minRoll.Value;
        }

        internal int GetMax(Dice dice, string abilityName)
        {
            if (SetRolls.ContainsKey(abilityName))
                return SetRolls[abilityName];

            return GetMaxRoll(dice);
        }

        private int GetMaxRoll(Dice dice)
        {
            maxRoll ??= dice.Roll(Roll).AsPotentialMaximum();
            return maxRoll.Value;
        }

        public override string ToString()
        {
            if (SetRolls.Count == 0)
                return Roll;

            var pairs = SetRolls.Select(kvp => $"{kvp.Key}: {kvp.Value}");
            return string.Join("; ", pairs);
        }
    }
}
