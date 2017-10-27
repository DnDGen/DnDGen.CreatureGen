using CreatureGen.Domain.Selectors.Selections;

namespace CreatureGen.Domain.Selectors.Collections
{
    internal interface ISkillSelector
    {
        SkillSelection SelectFor(string skill);
    }
}