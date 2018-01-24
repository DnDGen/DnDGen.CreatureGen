using CreatureGen.Selectors.Selections;
using CreatureGen.Tables;
using DnDGen.Core.Selectors.Collections;
using System;
using System.Linq;

namespace CreatureGen.Selectors.Collections
{
    internal class CreatureDataSelector : ICreatureDataSelector
    {
        private readonly ICollectionSelector collectionSelector;

        public CreatureDataSelector(ICollectionSelector collectionSelector)
        {
            this.collectionSelector = collectionSelector;
        }

        public CreatureDataSelection SelectFor(string creatureName)
        {
            var selection = new CreatureDataSelection();
            var data = collectionSelector.SelectFrom(TableNameConstants.Set.Collection.CreatureData, creatureName).ToArray();

            selection.ChallengeRating = data[DataIndexConstants.CreatureData.ChallengeRating];

            if (!string.IsNullOrEmpty(data[DataIndexConstants.CreatureData.LevelAdjustment]))
                selection.LevelAdjustment = Convert.ToInt32(data[DataIndexConstants.CreatureData.LevelAdjustment]);

            selection.Reach = Convert.ToInt32(data[DataIndexConstants.CreatureData.Reach]);
            selection.Size = data[DataIndexConstants.CreatureData.Size];
            selection.Space = Convert.ToInt32(data[DataIndexConstants.CreatureData.Space]);

            return selection;
        }
    }
}
