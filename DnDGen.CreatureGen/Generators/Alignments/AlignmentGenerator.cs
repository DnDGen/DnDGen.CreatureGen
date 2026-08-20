using DnDGen.CreatureGen.Alignments;
using DnDGen.CreatureGen.Generators.Creatures;
using DnDGen.CreatureGen.Tables;
using DnDGen.CreatureGen.Verifiers;
using DnDGen.CreatureGen.Verifiers.Exceptions;
using DnDGen.Infrastructure.Selectors.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Generators.Alignments
{
    internal class AlignmentGenerator(ICollectionSelector collectionSelector, ICreatureVerifier creatureVerifier) : IAlignmentGenerator
    {
        public Alignment Generate(string creatureName, IEnumerable<string> templates, Filters filters)
        {
            var weightedAlignments = GetWeightedAlignments(creatureName, templates);

            if (filters?.Alignments?.Count > 0)
            {
                //INFO: Doing Where instead of Intersect in order to preserve weighting
                weightedAlignments = weightedAlignments.Where(filters.Alignments.Contains);
            }

            if (!weightedAlignments.Any())
                throw new InvalidCreatureException(
                    $"Creature {creatureName} has no valid alignments for templates [{string.Join(", ", templates)}]. Filters: {filters.GetDescription(false)}",
                    false,
                    creatureName);

            var randomAlignment = collectionSelector.SelectRandomFrom(weightedAlignments);
            return new Alignment(randomAlignment);
        }

        private IEnumerable<string> GetWeightedAlignments(string creatureName, IEnumerable<string> templates)
        {
            var weightedAlignments = collectionSelector.SelectFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups, creatureName);
            var templatesArray = templates?.ToArray() ?? [];

            if (templatesArray.Length == 0)
                return weightedAlignments;

            if (templatesArray.Length == 1)
            {
                var templateAlignments = collectionSelector.SelectFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups, templatesArray[0] + GroupConstants.AllowedInput);

                //INFO: Doing this instead of intersect in order to preserve duplicates/weighting
                weightedAlignments = weightedAlignments.Where(templateAlignments.Contains);

                return weightedAlignments;
            }

            var prototype = creatureVerifier.GetChainedTemplates([creatureName], [.. templates], false).Single();
            weightedAlignments = prototype.Alignments.Select(a => a.Full);

            return weightedAlignments;
        }
    }
}