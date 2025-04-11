using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Selectors.Selections;
using DnDGen.CreatureGen.Tables;
using DnDGen.Infrastructure.Helpers;
using DnDGen.Infrastructure.Selectors.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Selectors.Collections
{
    internal class FeatsSelector : IFeatsSelector
    {
        private readonly ICollectionDataSelector<FeatDataSelection> featDataSelector;
        private readonly ICollectionDataSelector<SpecialQualityDataSelection> specialQualityDataSelector;

        public FeatsSelector(ICollectionDataSelector<FeatDataSelection> featDataSelector, ICollectionDataSelector<SpecialQualityDataSelection> specialQualityDataSelector)
        {
            this.featDataSelector = featDataSelector;
            this.specialQualityDataSelector = specialQualityDataSelector;
        }

        public IEnumerable<FeatDataSelection> SelectFeats()
        {
            var featData = featDataSelector.SelectAllFrom(Config.Name, TableNameConstants.Collection.FeatData);
            return featData.Values.Select(d => d.Single());
        }

        public IEnumerable<SpecialQualityDataSelection> SelectSpecialQualities(string creature, CreatureType creatureType)
        {
            var specialQualitiesWithSource = new Dictionary<string, IEnumerable<SpecialQualityDataSelection>>
            {
                [creature] = specialQualityDataSelector.SelectFrom(Config.Name, TableNameConstants.Collection.SpecialQualityData, creature),
                [creatureType.Name] = specialQualityDataSelector.SelectFrom(Config.Name, TableNameConstants.Collection.SpecialQualityData, creatureType.Name)
            };

            foreach (var subtype in creatureType.SubTypes)
            {
                specialQualitiesWithSource[subtype] = specialQualityDataSelector.SelectFrom(Config.Name, TableNameConstants.Collection.SpecialQualityData, subtype);
            }

            if (!specialQualitiesWithSource.SelectMany(kvp => kvp.Value).Any())
                return [];

            var specialQualitySelections = new List<SpecialQualityDataSelection>();
            var usedSpecialQualities = new HashSet<string>();

            foreach (var specialQualityKvp in specialQualitiesWithSource)
            {
                foreach (var specialQualitySelection in specialQualityKvp.Value)
                {
                    var parsedData = DataHelper.Parse(specialQualitySelection);
                    if (usedSpecialQualities.Contains(parsedData))
                    {
                        continue;
                    }

                    specialQualitySelections.Add(specialQualitySelection);
                    usedSpecialQualities.Add(parsedData);
                }
            }

            return specialQualitySelections;
        }
    }
}