using DnDGen.CreatureGen.Selectors.Selections;
using DnDGen.CreatureGen.Tables;
using DnDGen.Infrastructure.Selectors.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Selectors.Collections
{
    [Obsolete]
    internal class CreatureDataSelector : ICreatureDataSelector
    {
        private readonly ICollectionSelector collectionSelector;

        public CreatureDataSelector(ICollectionSelector collectionSelector)
        {
            this.collectionSelector = collectionSelector;
        }

        private CreatureDataSelection Parse(IEnumerable<string> creatureData)
        {
            var selection = new CreatureDataSelection();
            var data = creatureData.ToArray();

            selection.ChallengeRating = data[DataIndexConstants.CreatureData.ChallengeRating];

            if (!string.IsNullOrEmpty(data[DataIndexConstants.CreatureData.LevelAdjustment]))
                selection.LevelAdjustment = Convert.ToInt32(data[DataIndexConstants.CreatureData.LevelAdjustment]);

            selection.Reach = Convert.ToDouble(data[DataIndexConstants.CreatureData.Reach]);
            selection.Size = data[DataIndexConstants.CreatureData.Size];
            selection.Space = Convert.ToDouble(data[DataIndexConstants.CreatureData.Space]);
            selection.CanUseEquipment = Convert.ToBoolean(data[DataIndexConstants.CreatureData.CanUseEquipment]);
            selection.CasterLevel = Convert.ToInt32(data[DataIndexConstants.CreatureData.CasterLevel]);
            selection.NaturalArmor = Convert.ToInt32(data[DataIndexConstants.CreatureData.NaturalArmor]);
            selection.NumberOfHands = Convert.ToInt32(data[DataIndexConstants.CreatureData.NumberOfHands]);

            return selection;
        }

        public CreatureDataSelection SelectFor(string creatureName)
        {
            var data = collectionSelector.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureData, creatureName);
            var selection = Parse(data);

            return selection;
        }

        public Dictionary<string, CreatureDataSelection> SelectAll()
        {
            var data = collectionSelector.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureData);
            var selections = data.ToDictionary(kvp => kvp.Key, kvp => Parse(kvp.Value));

            return selections;
        }
    }
}
