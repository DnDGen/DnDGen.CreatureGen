using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Generators.Abilities;
using DnDGen.CreatureGen.Generators.Creatures;
using DnDGen.CreatureGen.Tables;
using DnDGen.CreatureGen.Templates;
using DnDGen.Infrastructure.Factories;
using DnDGen.Infrastructure.Selectors.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Verifiers
{
    internal class CreatureVerifier(JustInTimeFactory factory, ICollectionSelector collectionsSelector) : ICreatureVerifier
    {
        public bool VerifyCompatibility(bool asCharacter, string creature = null, AbilityRandomizer abilityRandomizer = null, Filters filters = null)
        {
            IEnumerable<string> baseCreatures = [creature];
            if (string.IsNullOrEmpty(creature))
            {
                baseCreatures = collectionsSelector.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, GroupConstants.All);
            }

            if (asCharacter)
            {
                var characters = collectionsSelector.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, GroupConstants.Characters);
                baseCreatures = baseCreatures.Intersect(characters);
            }

            var compatible = baseCreatures.Any();
            if (!compatible)
                return false;

            if (filters?.CleanTemplates?.Count > 0)
            {
                compatible = TemplatesAreCompatible(filters.CleanTemplates, baseCreatures, asCharacter, abilityRandomizer, filters);
                return compatible;
            }

            //INFO: We can cheat and use the None template applicator to verify the filters
            compatible = TemplatesAreCompatible([CreatureConstants.Templates.None], baseCreatures, asCharacter, abilityRandomizer, filters);
            if (compatible)
                return true;

            var templates = collectionsSelector.SelectFrom(Config.Name, TableNameConstants.Collection.TemplateGroups, GroupConstants.All);
            foreach (var template in templates)
            {
                compatible = TemplatesAreCompatible([template], baseCreatures, asCharacter, abilityRandomizer, filters);
                if (compatible)
                    return true;
            }

            return false;
        }

        private bool TemplatesAreCompatible(
            List<string> templates,
            IEnumerable<string> creatures,
            bool asCharacter,
            AbilityRandomizer abilityRandomizer,
            Filters filters)
        {
            var applicator = factory.Build<TemplateApplicator>(templates[0]);

            if (templates.Count == 1)
            {
                var compatibleCreatures = applicator.GetCompatibleCreatures(creatures, asCharacter, abilityRandomizer, filters);
                return compatibleCreatures.Any();
            }

            var prototypes = applicator.GetCompatiblePrototypes(creatures, asCharacter, abilityRandomizer);

            for (var i = 1; i < templates.Count; i++)
            {
                applicator = factory.Build<TemplateApplicator>(templates[i]);

                //INFO: We only want to apply filters on the last template, once all other templates have been applied
                if (i == filters.CleanTemplates.Count - 1)
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
    }
}