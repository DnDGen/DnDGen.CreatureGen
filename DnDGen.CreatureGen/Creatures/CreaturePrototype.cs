using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Alignments;
using DnDGen.CreatureGen.Defenses;
using System.Collections.Generic;

namespace DnDGen.CreatureGen.Creatures
{
    public class CreaturePrototype
    {
        public string Name { get; set; }
        public CreatureType Type { get; set; }
        public Dictionary<string, Ability> Abilities { get; set; }
        public string Size { get; set; }
        public string ChallengeRating { get; set; }
        public List<Alignment> Alignments { get; set; }
        public int? LevelAdjustment { get; set; }
        public int CasterLevel { get; set; }
        public double HitDiceQuantity { get; set; }

        public CreaturePrototype()
        {
            Abilities = [];
            Alignments = [];
            Size = string.Empty;
            ChallengeRating = string.Empty;
            Name = string.Empty;
            Type = new CreatureType();
        }

        public int GetRoundedHitDiceQuantity() => HitDice.GetRoundedQuantity(HitDiceQuantity);
    }
}