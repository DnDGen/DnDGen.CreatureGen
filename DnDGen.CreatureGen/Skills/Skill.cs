using DnDGen.CreatureGen.Abilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Skills
{
    public class Skill(string name, Ability baseStat, int rankCap, string focus = "")
    {
        public string Name { get; private set; } = name;
        public Ability BaseAbility { get; set; } = baseStat;
        public string Focus { get; private set; } = focus;
        public bool ClassSkill { get; set; }
        public int ArmorCheckPenalty { get; set; }
        public int RankCap { get; set; } = rankCap;
        public bool HasArmorCheckPenalty { get; set; }
        public List<Bonus> Bonuses { get; set; } = [];

        public bool CircumstantialBonus => Bonuses.Any(b => b.IsConditional);

        public string Key => SkillConstants.Build(Name, Focus);

        public int Bonus => Bonuses.Where(b => !b.IsConditional).Sum(b => b.Value);

        public double EffectiveRanks => ClassSkill ? Ranks : Ranks / 2d;

        public bool RanksMaxedOut => Ranks == RankCap;

        private int ranks;

        public int Ranks
        {
            get
            {
                return ranks;
            }
            set
            {
                if (value > RankCap)
                    throw new InvalidOperationException($"{value} Ranks for Skill '{Name}' cannot exceed the Rank Cap of {RankCap}");

                ranks = value;
            }
        }

        public bool QualifiesForSkillSynergy => EffectiveRanks >= 5;

        public int TotalBonus
        {
            get
            {
                var total = EffectiveRanks + Bonus + BaseAbility.Modifier + ArmorCheckPenalty;
                var floor = Math.Floor(total);

                return Convert.ToInt32(floor);
            }
        }

        public bool IsEqualTo(Skill skill) => IsEqualTo(skill.Name, skill.Focus);

        private bool IsEqualTo(string skill, string focus)
        {
            var match = skill == Name;

            if (!match)
                return false;

            return string.IsNullOrEmpty(Focus) || string.IsNullOrEmpty(focus) || focus == Focus;
        }

        public bool IsEqualTo(string skill)
        {
            var skillData = SkillConstants.Parse(skill);

            if (skillData.Length > 1)
                return IsEqualTo(skillData[0], skillData[1]);

            return IsEqualTo(skillData[0], string.Empty);
        }

        public void AddBonus(int value, string condition = "")
        {
            var bonus = new Bonus { Value = value, Condition = condition };
            Bonuses.Add(bonus);
        }
    }
}