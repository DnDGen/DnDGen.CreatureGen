using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Generators.Creatures;
using DnDGen.CreatureGen.Tables;
using DnDGen.CreatureGen.Templates;
using DnDGen.Infrastructure.Generators;
using DnDGen.Infrastructure.Selectors.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Verifiers
{
    internal class CreatureVerifier : ICreatureVerifier
    {
        private readonly JustInTimeFactory factory;
        private readonly ICollectionSelector collectionsSelector;

        public CreatureVerifier(JustInTimeFactory factory, ICollectionSelector collectionsSelector)
        {
            this.factory = factory;
            this.collectionsSelector = collectionsSelector;
        }

        public bool VerifyCompatibility(bool asCharacter, string creature = null, Filters filters = null)
        {
            IEnumerable<string> baseCreatures = new[] { creature };
            if (string.IsNullOrEmpty(creature))
            {
                baseCreatures = collectionsSelector.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.All);
            }

            if (asCharacter)
            {
                var characters = collectionsSelector.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Characters);
                baseCreatures = baseCreatures.Intersect(characters);
            }

            if (!baseCreatures.Any())
                return false;

            if (filters?.Templates?.Any() == true)
            {
                var applicator = factory.Build<TemplateApplicator>(filters.Templates[0]);

                if (filters.Templates.Count == 1)
                {
                    var compatibleCreatures = applicator.GetCompatibleCreatures(baseCreatures, asCharacter, filters);
                    return compatibleCreatures.Any();
                }

                var prototypes = applicator.GetCompatiblePrototypes(baseCreatures, asCharacter);

                for (var i = 1; i < filters.Templates.Count; i++)
                {
                    applicator = factory.Build<TemplateApplicator>(filters.Templates[i]);

                    //INFO: We only want to apply filters on the last template, once all other templates have been applied
                    if (i == filters.Templates.Count - 1)
                    {
                        prototypes = applicator.GetCompatiblePrototypes(prototypes, asCharacter, filters);
                    }
                    else
                    {
                        prototypes = applicator.GetCompatiblePrototypes(prototypes, asCharacter);
                    }
                }

                return prototypes.Any();
            }

            //INFO: We can cheat and use the None template applicator
            var noneApplicator = factory.Build<TemplateApplicator>(CreatureConstants.Templates.None);

            var nonTemplateCreatures = noneApplicator.GetCompatibleCreatures(baseCreatures, asCharacter, filters);
            if (nonTemplateCreatures.Any())
                return true;

            var templates = collectionsSelector.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Templates);

            foreach (var otherTemplate in templates)
            {
                var templateApplicator = factory.Build<TemplateApplicator>(otherTemplate);

                var filteredBaseCreatures = templateApplicator.GetCompatibleCreatures(baseCreatures, asCharacter, filters);
                if (filteredBaseCreatures.Any())
                    return true;
            }

            return false;
        }
    }
}