using System;
using System.Collections.Generic;

namespace DnDGen.CreatureGen.Selectors.Collections
{
    [Obsolete]
    internal interface IAdjustmentsSelector
    {
        Dictionary<string, T> SelectAllFrom<T>(string tableName);
        T SelectFrom<T>(string tableName, string name);
    }
}