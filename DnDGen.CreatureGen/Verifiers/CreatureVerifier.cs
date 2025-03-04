﻿using DnDGen.CreatureGen.Creatures;
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
                baseCreatures = collectionsSelector.Explode(Config.Name, TableNameConstants.Collection.CreatureGroups, GroupConstants.All);
            }

            if (asCharacter)
            {
                var characters = collectionsSelector.Explode(Config.Name, TableNameConstants.Collection.CreatureGroups, GroupConstants.Characters);
                baseCreatures = baseCreatures.Intersect(characters);
            }

            var compatible = baseCreatures.Any();
            if (!compatible)
                return false;

            if (filters?.CleanTemplates?.Any() == true)
            {
                //INFO: Humans should be compatible with all templates
                //This is a shortcut to validate that multiple templates are compatible with each other
                //If we are also filtering to challenge rating or creature type, we cannot use this shortcut
                if (baseCreatures.Contains(CreatureConstants.Human) && filters.ChallengeRating == null && filters.Type == null)
                {
                    baseCreatures = [CreatureConstants.Human];
                }

                compatible = TemplatesAreCompatible(filters.CleanTemplates, baseCreatures, asCharacter, filters);
                return compatible;
            }

            //INFO: We can cheat and use the None template applicator
            compatible = TemplatesAreCompatible([CreatureConstants.Templates.None], baseCreatures, asCharacter, filters);
            if (compatible)
                return true;

            var templates = collectionsSelector.Explode(Config.Name, TableNameConstants.Collection.CreatureGroups, GroupConstants.Templates);
            foreach (var otherTemplate in templates)
            {
                compatible = TemplatesAreCompatible([otherTemplate], baseCreatures, asCharacter, filters);
                if (compatible)
                    return true;
            }

            return false;
        }

        private bool TemplatesAreCompatible(List<string> templates, IEnumerable<string> creatures, bool asCharacter, Filters filters = null)
        {
            var applicator = factory.Build<TemplateApplicator>(templates[0]);

            if (templates.Count == 1)
            {
                var compatibleCreatures = applicator.GetCompatibleCreatures(creatures, asCharacter, filters);
                return compatibleCreatures.Any();
            }

            var prototypes = applicator.GetCompatiblePrototypes(creatures, asCharacter);

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