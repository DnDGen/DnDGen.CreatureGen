using System;

namespace DnDGen.CreatureGen.Abilities
{
    public class Ability
    {
        public const int DefaultScore = 10;
        private const int DefaultTemplateScore = -1;

        public string Name { get; private set; }
        public int BaseScore { get; set; }
        public int RacialAdjustment { get; set; }
        public int AdvancementAdjustment { get; set; }
        public int TemplateAdjustment { get; set; }
        public int TemplateScore { get; set; }
        public int MaxModifier { get; set; }

        public bool HasTemplateScore => TemplateScore > DefaultTemplateScore;

        public bool HasScore
        {
            get
            {
                if (HasTemplateScore)
                    return TemplateScore > 0;

                return (BaseScore + TemplateAdjustment) > 0;
            }
        }

        public int FullScore
        {
            get
            {
                if (!HasScore)
                    return 0;

                if (HasTemplateScore)
                    return TemplateScore + TemplateAdjustment;

                return Math.Max(BaseScore + TemplateAdjustment + RacialAdjustment + AdvancementAdjustment, 1);
            }
        }

        public int Modifier
        {
            get
            {
                if (!HasScore)
                    return 0;

                var even = FullScore - FullScore % 2;
                var modifier = (even - DefaultScore) / 2;
                return Math.Min(MaxModifier, modifier);
            }
        }

        public Ability(string name)
        {
            Name = name;
            BaseScore = DefaultScore;
            MaxModifier = int.MaxValue;
            TemplateScore = DefaultTemplateScore;
        }
    }
}