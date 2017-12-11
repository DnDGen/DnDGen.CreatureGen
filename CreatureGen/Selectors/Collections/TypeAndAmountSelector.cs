using CreatureGen.Selectors.Selections;
using System;
using System.Collections.Generic;

namespace CreatureGen.Selectors.Collections
{
    internal class TypeAndAmountSelector : ITypeAndAmountSelector
    {
        public IEnumerable<TypeAndAmountSelection> Select(string tableName, string name)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, IEnumerable<TypeAndAmountSelection>> SelectAll(string tableName)
        {
            throw new NotImplementedException();
        }

        public TypeAndAmountSelection SelectOne(string tableName, string name)
        {
            throw new NotImplementedException();
        }
    }
}
