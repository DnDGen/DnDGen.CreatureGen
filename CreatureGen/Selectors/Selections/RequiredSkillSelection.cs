using CreatureGen.Skills;
using System.Collections.Generic;
using System.Linq;

namespace CreatureGen.Selectors.Selections
{
    internal class RequiredSkillSelection
    {
        public string Skill { get; set; }
        public string Focus { get; set; }
        public int Ranks { get; set; }

        public RequiredSkillSelection()
        {
            Skill = string.Empty;
            Focus = string.Empty;
        }

        public bool RequirementMet(IEnumerable<Skill> otherSkills)
        {
            var requiredSkill = otherSkills.FirstOrDefault(s => s.Name == Skill);

            if (requiredSkill == null)
                return false;

            if (!string.IsNullOrEmpty(Focus) && requiredSkill.Focus != Focus)
                return false;

            return requiredSkill.EffectiveRanks >= Ranks;
        }
    }
}
