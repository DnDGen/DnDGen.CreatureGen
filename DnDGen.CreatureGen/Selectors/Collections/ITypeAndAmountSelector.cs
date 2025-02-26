using DnDGen.CreatureGen.Selectors.Selections;
using System;
using System.Collections.Generic;

namespace DnDGen.CreatureGen.Selectors.Collections
{
    [Obsolete]
    internal interface ITypeAndAmountSelector
    {
        Dictionary<string, IEnumerable<TypeAndAmountSelection>> SelectAll(string tableName);
        IEnumerable<TypeAndAmountSelection> Select(string tableName, string name);
        TypeAndAmountSelection SelectOne(string tableName, string name);
    }
}
