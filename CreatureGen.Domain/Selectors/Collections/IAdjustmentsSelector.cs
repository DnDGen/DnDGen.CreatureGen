using System.Collections.Generic;

namespace CreatureGen.Domain.Selectors.Collections
{
    internal interface IAdjustmentsSelector
    {
        Dictionary<string, int> SelectAllFrom(string tableName);
        int SelectFrom(string tableName, string name);
    }
}