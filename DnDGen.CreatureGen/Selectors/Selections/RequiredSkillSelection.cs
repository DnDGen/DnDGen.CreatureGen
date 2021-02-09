using DnDGen.CreatureGen.Skills;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Selectors.Selections
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
            var thisSkill = SkillConstants.Build(Skill, Focus);
            var requiredSkills = otherSkills.Where(s => s.IsEqualTo(thisSkill));

            if (!requiredSkills.Any())
                return false;

            if (!string.IsNullOrEmpty(Focus) && !requiredSkills.Any(s => s.Focus == Focus))
                return false;

            var anyHaveSufficientRanks = requiredSkills.Any(s => s.EffectiveRanks >= Ranks);

            return anyHaveSufficientRanks;
        }
    }
}
