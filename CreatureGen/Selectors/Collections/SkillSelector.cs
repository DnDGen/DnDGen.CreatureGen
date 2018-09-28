using CreatureGen.Selectors.Selections;
using CreatureGen.Tables;
using DnDGen.Core.Selectors.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CreatureGen.Selectors.Collections
{
    internal class SkillSelector : ISkillSelector
    {
        private readonly ICollectionSelector innerSelector;
        private readonly ITypeAndAmountSelector typeAndAmountSelector;

        public SkillSelector(ICollectionSelector innerSelector, ITypeAndAmountSelector typeAndAmountSelector)
        {
            this.innerSelector = innerSelector;
            this.typeAndAmountSelector = typeAndAmountSelector;
        }

        public SkillSelection SelectFor(string skill)
        {
            var data = innerSelector.SelectFrom(TableNameConstants.Collection.SkillData, skill).ToArray();

            var selection = new SkillSelection();
            selection.BaseAbilityName = data[DataIndexConstants.SkillSelectionData.BaseStatName];
            selection.SkillName = data[DataIndexConstants.SkillSelectionData.SkillName];
            selection.RandomFociQuantity = Convert.ToInt32(data[DataIndexConstants.SkillSelectionData.RandomFociQuantity]);
            selection.Focus = data[DataIndexConstants.SkillSelectionData.Focus];

            return selection;
        }

        public IEnumerable<SkillBonusSelection> SelectBonusesFor(string source)
        {
            var typeAndAmountSelections = typeAndAmountSelector.Select(TableNameConstants.TypeAndAmount.SkillBonuses, source);
            var bonusSelections = typeAndAmountSelections.Select(s => Parse(s));

            return bonusSelections;
        }

        private SkillBonusSelection Parse(TypeAndAmountSelection input)
        {
            var selection = new SkillBonusSelection();

            selection.Bonus = input.Amount;

            var sections = input.Type.Split(',');
            selection.Skill = sections[0];

            if (sections.Length > 1)
                selection.Condition = sections[1];

            return selection;
        }
    }
}