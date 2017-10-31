using CreatureGen.Skills;
using System;

namespace CreatureGen.Selectors.Selections
{
    internal class SkillSelection
    {
        public string BaseAbilityName { get; set; }
        public int RandomFociQuantity { get; set; }
        public string SkillName { get; set; }
        public string Focus { get; set; }

        public SkillSelection()
        {
            BaseAbilityName = string.Empty;
            SkillName = string.Empty;
            Focus = string.Empty;
        }

        public bool IsEqualTo(Skill skill)
        {
            if (RandomFociQuantity > 0)
                throw new InvalidOperationException("Cannot test equality of a skill selection while random foci quantity is positive");

            return SkillName == skill.Name && Focus == skill.Focus;
        }

        public bool IsEqualTo(SkillSelection selection)
        {
            if (RandomFociQuantity > 0 || selection.RandomFociQuantity > 0)
                throw new InvalidOperationException("Cannot test equality of a skill selection while random foci quantity is positive");

            return SkillName == selection.SkillName && Focus == selection.Focus;
        }
    }
}