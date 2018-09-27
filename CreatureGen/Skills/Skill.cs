using CreatureGen.Abilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CreatureGen.Skills
{
    public class Skill
    {
        public string Name { get; private set; }
        public Ability BaseAbility { get; set; }
        public string Focus { get; private set; }
        public bool ClassSkill { get; set; }
        public int ArmorCheckPenalty { get; set; }
        public int RankCap { get; set; }
        public bool HasArmorCheckPenalty { get; set; }
        public IEnumerable<SkillBonus> Bonuses { get; private set; }

        public bool CircumstantialBonus => Bonuses.Any(b => b.IsConditional);

        public int Bonus
        {
            get
            {
                return Bonuses
                    .Where(b => !b.IsConditional)
                    .Select(b => b.Value)
                    .Sum();
            }
        }

        public double EffectiveRanks
        {
            get
            {
                if (ClassSkill)
                    return ranks;

                return ranks / 2d;
            }
        }

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
                    throw new InvalidOperationException("Ranks cannot exceed the Rank Cap");

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

        public Skill(string name, Ability baseStat, int rankCap, string focus = "")
        {
            Name = name;
            BaseAbility = baseStat;
            RankCap = rankCap;
            Focus = focus;
        }

        public bool IsEqualTo(Skill skill)
        {
            return IsEqualTo(skill.Name, skill.Focus);
        }

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

        public void AddSkillBonus(int value, string condition = "")
        {
            var bonus = new SkillBonus { Value = value, Condition = condition };
            Bonuses = Bonuses.Union(new[] { bonus });
        }
    }
}