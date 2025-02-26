using DnDGen.CreatureGen.Selectors.Selections;
using System;
using System.Collections.Generic;

namespace DnDGen.CreatureGen.Selectors.Collections
{
    [Obsolete]
    internal interface IBonusSelector
    {
        IEnumerable<BonusSelection> SelectFor(string tableName, string source);
    }
}
