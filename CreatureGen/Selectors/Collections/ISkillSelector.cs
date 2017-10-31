using CreatureGen.Selectors.Selections;

namespace CreatureGen.Selectors.Collections
{
    internal interface ISkillSelector
    {
        SkillSelection SelectFor(string skill);
    }
}