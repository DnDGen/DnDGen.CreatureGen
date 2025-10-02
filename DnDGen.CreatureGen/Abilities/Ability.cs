using System;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Abilities
{
    public class Ability(string name)
    {
        public const int DefaultScore = 10;
        private const int DefaultTemplateScore = -1;

        public string Name { get; private set; } = name;
        public int BaseScore { get; set; } = DefaultScore;
        public int RacialAdjustment { get; set; }
        public int AdvancementAdjustment { get; set; }
        public int AgeAdjustment { get; set; }
        public int TemplateAdjustment { get; set; }
        public int TemplateScore { get; set; } = DefaultTemplateScore;
        public int MaxModifier { get; set; } = int.MaxValue;
        public List<Bonus> Bonuses { get; set; } = [];
        public int Bonus => Bonuses.Where(b => !b.IsConditional).Sum(b => b.Value);

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

                return Math.Max(BaseScore + TemplateAdjustment + RacialAdjustment + AgeAdjustment + AdvancementAdjustment + Bonus, 1);
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

        public string Summary => $"{Name}: {FullScore} ({Modifier:+#;-#;0})";

        public override string ToString() => Summary;
    }
}