using System;

namespace DnDGen.CreatureGen.Abilities
{
    public class Ability
    {
        public string Name { get; private set; }
        public int BaseScore { get; set; }
        public int RacialAdjustment { get; set; }
        public int AdvancementAdjustment { get; set; }

        public bool HasScore
        {
            get
            {
                return BaseScore > 0;
            }
        }

        public int FullScore
        {
            get
            {
                if (!HasScore)
                    return 0;

                return Math.Max(BaseScore + RacialAdjustment + AdvancementAdjustment, 1);
            }
        }

        public int Modifier
        {
            get
            {
                if (!HasScore)
                    return 0;

                var even = FullScore - FullScore % 2;
                return (even - 10) / 2;
            }
        }

        public Ability(string name)
        {
            Name = name;
            BaseScore = 10;
        }
    }
}