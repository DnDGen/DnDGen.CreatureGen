using DnDGen.CreatureGen.Selectors.Selections;
using System;
using System.Collections.Generic;

namespace DnDGen.CreatureGen.Selectors.Collections
{
    [Obsolete]
    internal interface IBonusSelector
    {
        IEnumerable<BonusDataSelection> SelectFor(string tableName, string source);
    }
}
