using DnDGen.CreatureGen.Selectors.Selections;
using System.Collections.Generic;

namespace DnDGen.CreatureGen.Selectors.Collections
{
    internal interface ISkillSelector
    {
        SkillDataSelection SelectFor(string skill);
        IEnumerable<BonusDataSelection> SelectBonusesFor(string source);
    }
}