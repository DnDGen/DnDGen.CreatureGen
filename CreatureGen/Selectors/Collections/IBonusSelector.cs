using CreatureGen.Selectors.Selections;
using System.Collections.Generic;

namespace CreatureGen.Selectors.Collections
{
    internal interface IBonusSelector
    {
        IEnumerable<BonusSelection> SelectFor(string tableName, string source);
    }
}
