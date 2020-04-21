using System;

namespace DnDGen.CreatureGen.Abilities
{
    public class Ability
    {
        public string Name { get; private set; }
        public int BaseScore { get; set; }
        public int RacialAdjustment { get; set; }
        public int AdvancementAdjustment { get; set; }
        public int MaxModifier { get; set; }

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
                var modifier = (even - 10) / 2;
                return Math.Min(MaxModifier, modifier);
            }
        }

        public Ability(string name)
        {
            Name = name;
            BaseScore = 10;
            MaxModifier = int.MaxValue;
        }
    }
}