using DnDGen.CreatureGen.Selectors.Selections;
using DnDGen.CreatureGen.Tables;
using DnDGen.Infrastructure.Selectors.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Selectors.Collections
{
    internal class SkillSelector : ISkillSelector
    {
        private readonly ICollectionSelector collectionSelector;
        private readonly IBonusSelector bonusSelector;

        public SkillSelector(ICollectionSelector collectionSelector, IBonusSelector bonusSelector)
        {
            this.collectionSelector = collectionSelector;
            this.bonusSelector = bonusSelector;
        }

        public SkillSelection SelectFor(string skill)
        {
            var data = collectionSelector.SelectFrom(Config.Name, TableNameConstants.Collection.SkillData, skill).ToArray();

            var selection = new SkillSelection();
            selection.BaseAbilityName = data[DataIndexConstants.SkillSelectionData.BaseAbilityNameIndex];
            selection.SkillName = data[DataIndexConstants.SkillSelectionData.SkillNameIndex];
            selection.RandomFociQuantity = Convert.ToInt32(data[DataIndexConstants.SkillSelectionData.RandomFociQuantityIndex]);
            selection.Focus = data[DataIndexConstants.SkillSelectionData.FocusIndex];

            return selection;
        }

        public IEnumerable<BonusDataSelection> SelectBonusesFor(string source)
        {
            var bonusSelections = bonusSelector.SelectFor(TableNameConstants.TypeAndAmount.SkillBonuses, source);

            return bonusSelections;
        }
    }
}