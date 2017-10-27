using CreatureGen.Abilities;
using System;

namespace CreatureGen.Skills
{
    public class Skill
    {
        public string Name { get; private set; }
        public Ability BaseAbility { get; private set; }
        public string Focus { get; private set; }

        public int Bonus { get; set; }
        public bool ClassSkill { get; set; }
        public int ArmorCheckPenalty { get; set; }
        public bool CircumstantialBonus { get; set; }
        public int RankCap { get; set; }
        public bool HasArmorCheckPenalty { get; set; }

        public double EffectiveRanks
        {
            get
            {
                if (ClassSkill)
                    return ranks;

                return ranks / 2d;
            }
        }

        public bool RanksMaxedOut
        {
            get
            {
                return Ranks == RankCap;
            }
        }

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

        public bool QualifiesForSkillSynergy
        {
            get
            {
                return EffectiveRanks >= 5;
            }
        }

        public int TotalBonus
        {
            get
            {
                var total = EffectiveRanks + Bonus + BaseAbility.Bonus + ArmorCheckPenalty;
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
            return skill.Name == Name && skill.Focus == Focus;
        }

        public bool IsEqualTo(string skill)
        {
            var sections = skill.Split('/');
            var name = sections[0];
            var focus = sections.Length > 1 ? sections[1] : string.Empty;

            return name == Name && focus == Focus;
        }
    }
}