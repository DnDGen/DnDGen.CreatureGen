using DnDGen.Infrastructure.Selectors.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Selectors.Collections
{
    [Obsolete]
    internal class AdjustmentsSelector : IAdjustmentsSelector
    {
        private readonly ICollectionSelector collectionsSelector;

        public AdjustmentsSelector(ICollectionSelector collectionsSelector)
        {
            this.collectionsSelector = collectionsSelector;
        }

        public Dictionary<string, T> SelectAllFrom<T>(string tableName)
        {
            var collectionTable = collectionsSelector.SelectAllFrom(Config.Name, tableName);
            var adjustmentTable = new Dictionary<string, T>();

            foreach (var kvp in collectionTable)
            {
                adjustmentTable[kvp.Key] = GetAdjustment<T>(kvp.Value);
            }

            return adjustmentTable;
        }

        public T SelectFrom<T>(string tableName, string name)
        {
            var collection = collectionsSelector.SelectFrom(Config.Name, tableName, name);
            var adjustment = GetAdjustment<T>(collection);

            return adjustment;
        }

        private T GetAdjustment<T>(IEnumerable<string> collection)
        {
            var firstItem = collection.First();
            var adjustment = (T)Convert.ChangeType(firstItem, typeof(T));

            return adjustment;
        }
    }
}