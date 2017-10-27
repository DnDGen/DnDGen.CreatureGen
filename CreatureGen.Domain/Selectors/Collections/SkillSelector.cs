using CreatureGen.Domain.Selectors.Selections;
using CreatureGen.Domain.Tables;
using DnDGen.Core.Selectors.Collections;
using System;
using System.Linq;

namespace CreatureGen.Domain.Selectors.Collections
{
    internal class SkillSelector : ISkillSelector
    {
        private readonly ICollectionSelector innerSelector;

        public SkillSelector(ICollectionSelector innerSelector)
        {
            this.innerSelector = innerSelector;
        }

        public SkillSelection SelectFor(string skill)
        {
            var data = innerSelector.SelectFrom(TableNameConstants.Set.Collection.SkillData, skill).ToArray();

            var selection = new SkillSelection();
            selection.BaseStatName = data[DataIndexConstants.SkillSelectionData.BaseStatName];
            selection.SkillName = data[DataIndexConstants.SkillSelectionData.SkillName];
            selection.RandomFociQuantity = Convert.ToInt32(data[DataIndexConstants.SkillSelectionData.RandomFociQuantity]);
            selection.Focus = data[DataIndexConstants.SkillSelectionData.Focus];

            return selection;
        }
    }
}