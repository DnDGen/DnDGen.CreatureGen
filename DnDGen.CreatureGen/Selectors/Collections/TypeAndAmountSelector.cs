using DnDGen.CreatureGen.Selectors.Helpers;
using DnDGen.CreatureGen.Selectors.Selections;
using DnDGen.Infrastructure.Selectors.Collections;
using DnDGen.RollGen;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Selectors.Collections
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
            var typesAndAmounts = collection.Select(Parse).ToArray(); //INFO: We want to execute this immediately, so random rolls are not re-iterated and re-rolled

            return typesAndAmounts;
        }

        private TypeAndAmountSelection Parse(string entry)
        {
            var sections = TypeAndAmountHelper.Parse(entry);
            var selection = new TypeAndAmountSelection();

            selection.Type = sections[0];
            selection.RawAmount = sections[1];
            selection.Amount = dice.Roll(selection.RawAmount).AsSum();

            return selection;
        }

        public Dictionary<string, IEnumerable<TypeAndAmountSelection>> SelectAll(string tableName)
        {
            var table = collectionSelector.SelectAllFrom(tableName);
            var typesAndAmounts = new Dictionary<string, IEnumerable<TypeAndAmountSelection>>();

            foreach (var kvp in table)
                typesAndAmounts[kvp.Key] = kvp.Value.Select(Parse).ToArray(); //INFO: We want to execute this immediately, so random rolls are not re-iterated and re-rolled

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
