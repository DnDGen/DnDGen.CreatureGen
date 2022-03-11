using DnDGen.CreatureGen.Abilities;
using System;
using System.Collections.Generic;
using System.Text;

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
            SetRolls = new Dictionary<string, int>();
            AbilityAdvancements = new Dictionary<string, int>();
        }
    }
}
