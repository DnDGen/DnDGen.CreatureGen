﻿using DnDGen.Infrastructure.Models;

namespace DnDGen.CreatureGen.Selectors.Selections
{
    internal class CreatureDataSelection : DataSelection<CreatureDataSelection>
    {
        public string Size { get; set; }
        public string ChallengeRating { get; set; }
        public double Space { get; set; }
        public double Reach { get; set; }
        public int? LevelAdjustment { get; set; }
        public int CasterLevel { get; set; }
        public int NumberOfHands { get; set; }
        public int NaturalArmor { get; set; }
        public bool CanUseEquipment { get; set; }

        public CreatureDataSelection()
        {
            Size = string.Empty;
            ChallengeRating = string.Empty;
        }
    }
}
