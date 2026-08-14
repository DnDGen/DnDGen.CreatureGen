using DnDGen.CreatureGen.Alignments;
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
        public Alignment Generate(string creatureName, IEnumerable<string> templates, string presetAlignment)
        {
            if (!string.IsNullOrEmpty(presetAlignment))
                return new Alignment(presetAlignment);

            var weightedAlignments = GetWeightedAlignments(creatureName, templates);
            if (!weightedAlignments.Any())
                throw new InvalidCreatureException(
                    $"Creature {creatureName} has no valid alignments for templates [{string.Join(", ", templates)}]",
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

            var prototypes = creatureVerifier.GetChainedTemplates([creatureName], [.. templates], false);

            //INFO: At this point, after multiple templates, we are choosing to ignore weighting
            weightedAlignments = prototypes.SelectMany(p => p.Alignments).Select(a => a.Full);

            return weightedAlignments;
        }
    }
}