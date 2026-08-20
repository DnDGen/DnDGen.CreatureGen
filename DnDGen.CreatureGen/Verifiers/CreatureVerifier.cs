using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Generators.Abilities;
using DnDGen.CreatureGen.Generators.Creatures;
using DnDGen.CreatureGen.Tables;
using DnDGen.CreatureGen.Templates;
using DnDGen.Infrastructure.Factories;
using DnDGen.Infrastructure.Selectors.Collections;
using DnDGen.RollGen;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Verifiers
{
    internal class CreatureVerifier(
        JustInTimeFactory factory,
        ICollectionSelector collectionSelector,
        Dice dice,
        ICreaturePrototypeFactory prototypeFactory) : ICreatureVerifier
    {
        public IEnumerable<CreaturePrototype> GetChainedTemplates(
            IEnumerable<string> sourceCreatures,
            string[] templates,
            bool asCharacter,
            AbilityRandomizer abilityRandomizer = null,
            Filters filters = null)
        {
            if (templates.Length < 2)
            {
                var firstTemplate = templates.FirstOrDefault();
                var creatureNames = GetCompatibleCreaturesForTemplate(sourceCreatures, firstTemplate, asCharacter, abilityRandomizer, filters);
                var prototypes = prototypeFactory.Build(creatureNames, asCharacter, abilityRandomizer);

                if (!string.IsNullOrEmpty(firstTemplate))
                {
                    var applicator = factory.Build<TemplateApplicator>(firstTemplate);
                    prototypes = prototypes.Select(p => applicator.ApplyTo(p, filters));
                }

                return prototypes;
            }

            var protoypes = prototypeFactory.Build(sourceCreatures, asCharacter, abilityRandomizer);

            //INFO: We only want to apply filters to the last creature in a series of chained templates
            for (var i = 0; i < templates.Length - 1; i++)
            {
                protoypes = GetCompatiblePrototypes(protoypes, templates[i]);
            }

            protoypes = GetCompatiblePrototypes(protoypes, templates[^1], filters);

            return protoypes;
        }

        public IEnumerable<string> GetCompatibleCreaturesForTemplate(
            IEnumerable<string> sourceCreatures,
            string template,
            bool asCharacter,
            AbilityRandomizer abilityRandomizer = null,
            Filters filters = null)
        {
            template ??= CreatureConstants.Templates.None;

            var templateCreatures = collectionSelector.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, template + asCharacter);
            var filteredBaseCreatures = sourceCreatures.Intersect(templateCreatures);

            if (filters.Types.Count > 0)
            {
                var typeCreatures = GetUnifiedCreatureGroups(template, filters.Types);
                filteredBaseCreatures = filteredBaseCreatures.Intersect(typeCreatures);
            }

            if (filters.Alignments.Count > 0)
            {
                var alignmentCreatures = GetUnifiedCreatureGroups(template, filters.Alignments);
                filteredBaseCreatures = filteredBaseCreatures.Intersect(alignmentCreatures);
            }

            if (!string.IsNullOrEmpty(filters?.ChallengeRating))
            {
                var groupName = template + asCharacter + filters.ChallengeRating;
                var crCreatures = collectionSelector.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, groupName);
                filteredBaseCreatures = filteredBaseCreatures.Intersect(crCreatures);
            }

            if (!filteredBaseCreatures.Any())
                return filteredBaseCreatures;

            var applicator = factory.Build<TemplateApplicator>(template);
            if (applicator.MinimumAbility is not null)
            {
                abilityRandomizer ??= new();
                var lowestAdjustment = applicator.MinimumAbility.FullScore - abilityRandomizer.GetMax(dice, applicator.MinimumAbility.Name);

                //INFO: If lowestAdjustment is -10, that's all creatures (worst adjustment is -10, can't go lower), so if <= -10, no intersect needed
                //Worst maxRoll is 1 (since abilities should be positive), so highest adjustment is Min - 1
                if (lowestAdjustment > -10)
                {
                    var groupName = applicator.MinimumAbility.Name + lowestAdjustment;
                    var abilityCreatures = collectionSelector.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, groupName);
                    filteredBaseCreatures = filteredBaseCreatures.Intersect(abilityCreatures);
                }
            }

            return filteredBaseCreatures;
        }

        private IEnumerable<string> GetUnifiedCreatureGroups(string prefix, IEnumerable<string> groupNames)
        {
            var group = Enumerable.Empty<string>();

            foreach (var groupName in groupNames)
            {
                var creatures = collectionSelector.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, prefix + groupName);
                group = group.Union(creatures);
            }

            return group;
        }

        private IEnumerable<CreaturePrototype> GetCompatiblePrototypes(IEnumerable<CreaturePrototype> sourceCreatures, string template, Filters filters = null)
        {
            var applicator = factory.Build<TemplateApplicator>(template);
            var compatiblePrototypes = sourceCreatures.Where(p => applicator.IsCompatible(p, filters));
            var updatedPrototypes = compatiblePrototypes.Select(p => applicator.ApplyTo(p, filters));

            //INFO: Trigger immediate execution, so it won't re-apply templates or re-compute validity.
            return [.. updatedPrototypes];
        }

        public bool VerifyCompatibility(bool asCharacter, string creature = null, AbilityRandomizer abilityRandomizer = null, Filters filters = null)
        {
            var valid = abilityRandomizer?.Validate(dice) ?? true;
            if (!valid)
                return false;

            IEnumerable<string> baseCreatures = [creature];
            if (string.IsNullOrEmpty(creature))
            {
                baseCreatures = collectionSelector.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, GroupConstants.All);
            }

            if (asCharacter)
            {
                var characters = collectionSelector.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, GroupConstants.Characters);
                baseCreatures = baseCreatures.Intersect(characters);
            }

            var compatible = baseCreatures.Any();
            if (!compatible)
                return false;

            if (filters?.CleanTemplates?.Length == 1)
            {
                var compatibleCreatures = GetCompatibleCreaturesForTemplate(baseCreatures, filters.CleanTemplates[0], asCharacter, abilityRandomizer, filters);
                return compatibleCreatures.Any();
            }

            if (filters?.CleanTemplates?.Length > 1)
            {
                var compatibleCreatures = GetChainedTemplates(baseCreatures, filters.CleanTemplates, asCharacter, abilityRandomizer, filters);
                return compatibleCreatures.Any();
            }

            //INFO: We can use the None template to verify the filters
            //If there are any of the base creatures in this group, then the filters are valid
            var filteredCreatures = GetCompatibleCreaturesForTemplate(baseCreatures, null, asCharacter, abilityRandomizer, filters);
            if (filteredCreatures.Any())
                return true;

            //INFO: This means that the filters aren't valid for non-templated base creatures.
            //We need to check the templates to see if any of them are valid
            var templates = collectionSelector.SelectFrom(Config.Name, TableNameConstants.Collection.TemplateGroups, GroupConstants.All);
            foreach (var template in templates)
            {
                filteredCreatures = GetCompatibleCreaturesForTemplate(baseCreatures, template, asCharacter, abilityRandomizer, filters);
                if (filteredCreatures.Any())
                    return true;
            }

            return false;
        }
    }
}