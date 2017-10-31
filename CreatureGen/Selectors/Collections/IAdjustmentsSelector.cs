using System.Collections.Generic;

namespace CreatureGen.Selectors.Collections
{
    internal interface IAdjustmentsSelector
    {
        Dictionary<string, int> SelectAllFrom(string tableName);
        int SelectFrom(string tableName, string name);
    }
}