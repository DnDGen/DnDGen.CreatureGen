using CreatureGen.Selectors.Selections;
using System.Collections.Generic;
using System.Linq;

namespace CreatureGen.Selectors.Collections
{
    internal class BonusSelector : IBonusSelector
    {
        private readonly ITypeAndAmountSelector typeAndAmountSelector;

        public BonusSelector(ITypeAndAmountSelector typeAndAmountSelector)
        {
            this.typeAndAmountSelector = typeAndAmountSelector;
        }

        public IEnumerable<BonusSelection> SelectFor(string tableName, string source)
        {
            var typeAndAmountSelections = typeAndAmountSelector.Select(tableName, source);
            var bonusSelections = typeAndAmountSelections.Select(s => Parse(s));

            return bonusSelections;
        }

        private BonusSelection Parse(TypeAndAmountSelection input)
        {
            var selection = new BonusSelection();

            selection.Bonus = input.Amount;

            var sections = input.Type.Split(',');
            selection.Source = sections[0];

            if (sections.Length > 1)
                selection.Condition = sections[1];

            return selection;
        }
    }
}
