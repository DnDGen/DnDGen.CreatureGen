using DnDGen.CreatureGen.Selectors.Selections;
using System.Collections.Generic;

namespace DnDGen.CreatureGen.Selectors.Collections
{
    internal interface IBonusSelector
    {
        IEnumerable<BonusSelection> SelectFor(string tableName, string source);
    }
}
