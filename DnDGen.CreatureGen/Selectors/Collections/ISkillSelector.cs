using DnDGen.CreatureGen.Selectors.Selections;
using System.Collections.Generic;

namespace DnDGen.CreatureGen.Selectors.Collections
{
    internal interface ISkillSelector
    {
        SkillSelection SelectFor(string skill);
        IEnumerable<BonusSelection> SelectBonusesFor(string source);
    }
}