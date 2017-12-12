using CreatureGen.Selectors.Selections;
using DnDGen.Core.Selectors.Collections;
using RollGen;
using System.Collections.Generic;
using System.Linq;

namespace CreatureGen.Selectors.Collections
{
    internal class TypeAndAmountSelector : ITypeAndAmountSelector
    {
        private readonly ICollectionSelector collectionSelector;
        private readonly Dice dice;

        public TypeAndAmountSelector(ICollectionSelector collectionSelector, Dice dice)
        {
            this.collectionSelector = collectionSelector;
            this.dice = dice;
        }

        public IEnumerable<TypeAndAmountSelection> Select(string tableName, string name)
        {
            var collection = collectionSelector.SelectFrom(tableName, name);
            var typesAndAmounts = collection.Select(e => Parse(e));

            return typesAndAmounts;
        }

        private TypeAndAmountSelection Parse(string entry)
        {
            var sections = entry.Split('/');
            var selection = new TypeAndAmountSelection();

            selection.Type = sections[0];
            selection.Amount = dice.Roll(sections[1]).AsSum();

            return selection;
        }

        public Dictionary<string, IEnumerable<TypeAndAmountSelection>> SelectAll(string tableName)
        {
            var table = collectionSelector.SelectAllFrom(tableName);
            var typesAndAmounts = new Dictionary<string, IEnumerable<TypeAndAmountSelection>>();

            foreach (var kvp in table)
                typesAndAmounts[kvp.Key] = kvp.Value.Select(e => Parse(e)).ToArray(); //INFO: We want to execute this immediately, so random rolls are not re-iterated and re-rolled

            return typesAndAmounts;
        }

        public TypeAndAmountSelection SelectOne(string tableName, string name)
        {
            var collection = collectionSelector.SelectFrom(tableName, name);
            var first = collection.First();
            var selection = Parse(first);

            return selection;
        }
    }
}
