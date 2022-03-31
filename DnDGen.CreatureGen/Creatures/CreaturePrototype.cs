using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Alignments;
using System.Collections.Generic;

namespace DnDGen.CreatureGen.Creatures
{
    public class CreaturePrototype
    {
        public string Name { get; set; }
        public CreatureType Type { get; set; }
        public Dictionary<string, Ability> Abilities { get; set; }
        public string ChallengeRating { get; set; }
        public List<Alignment> Alignments { get; set; }
        public int? LevelAdjustment { get; set; }
        public int CasterLevel { get; set; }

        public CreaturePrototype()
        {
            Abilities = new Dictionary<string, Ability>();
            Alignments = new List<Alignment>();
            ChallengeRating = string.Empty;
            Name = string.Empty;
            Type = new CreatureType();
        }
    }
}