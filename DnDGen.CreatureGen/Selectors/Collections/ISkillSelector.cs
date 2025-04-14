using DnDGen.CreatureGen.Selectors.Selections;
using System;
using System.Collections.Generic;

namespace DnDGen.CreatureGen.Selectors.Collections
{
    [Obsolete("use collection data selector instead")]
    internal interface ISkillSelector
    {
        SkillDataSelection SelectFor(string skill);
        [Obsolete("use collection data selector instead - <BonusDataSelection>(Config.Name, TableNameConstants.Collection.SkillBonuses, xxx)")]
        IEnumerable<BonusDataSelection> SelectBonusesFor(string source);
    }
}