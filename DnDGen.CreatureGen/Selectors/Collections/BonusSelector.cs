using DnDGen.CreatureGen.Selectors.Selections;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Selectors.Collections
{
    [Obsolete]
    internal class BonusSelector : IBonusSelector
    {
        private readonly ITypeAndAmountSelector typeAndAmountSelector;

        public BonusSelector(ITypeAndAmountSelector typeAndAmountSelector)
        {
            this.typeAndAmountSelector = typeAndAmountSelector;
        }

        public IEnumerable<BonusDataSelection> SelectFor(string tableName, string source)
        {
            var typeAndAmountSelections = typeAndAmountSelector.Select(tableName, source);
            var bonusSelections = typeAndAmountSelections.Select(s => Parse(s));

            return bonusSelections;
        }

        private BonusDataSelection Parse(TypeAndAmountSelection input)
        {
            var selection = new BonusDataSelection();

            selection.Bonus = input.Amount;

            var sections = input.Type.Split(BonusDataSelection.Divider);
            selection.Target = sections[0];

            if (sections.Length > 1)
                selection.Condition = sections[1];

            return selection;
        }
    }
}
