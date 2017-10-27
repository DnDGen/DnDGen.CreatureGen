using CreatureGen.Domain.Tables;
using CreatureGen.Creatures;
using DnDGen.Core.Selectors.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CreatureGen.Domain.Selectors.Collections
{
    internal class LanguageCollectionsSelector : ILanguageCollectionsSelector
    {
        private readonly ICollectionSelector innerSelector;

        public LanguageCollectionsSelector(ICollectionSelector innerSelector)
        {
            this.innerSelector = innerSelector;
        }

        public IEnumerable<string> SelectAutomaticLanguagesFor(Race race, string className)
        {
            var baseRaceLanguages = innerSelector.SelectFrom(TableNameConstants.Set.Collection.AutomaticLanguages, race.BaseRace);
            var metaraceLanguages = innerSelector.SelectFrom(TableNameConstants.Set.Collection.AutomaticLanguages, race.Metarace);
            var classLanguages = innerSelector.SelectFrom(TableNameConstants.Set.Collection.AutomaticLanguages, className);

            return baseRaceLanguages.Union(metaraceLanguages).Union(classLanguages);
        }

        public IEnumerable<string> SelectBonusLanguagesFor(string baseRace, string className)
        {
            var baseRaceLanguages = innerSelector.SelectFrom(TableNameConstants.Set.Collection.BonusLanguages, baseRace);
            var classLanguages = innerSelector.SelectFrom(TableNameConstants.Set.Collection.BonusLanguages, className);

            return baseRaceLanguages.Union(classLanguages);
        }
    }
}