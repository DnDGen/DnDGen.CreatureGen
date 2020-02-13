using System.Collections.Generic;

namespace DnDGen.CreatureGen.Selectors.Collections
{
    internal interface IAdjustmentsSelector
    {
        Dictionary<string, int> SelectAllFrom(string tableName);
        T SelectFrom<T>(string tableName, string name);
    }
}