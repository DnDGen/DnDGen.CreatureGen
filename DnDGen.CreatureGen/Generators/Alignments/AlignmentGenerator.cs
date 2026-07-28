using DnDGen.CreatureGen.Alignments;
using DnDGen.CreatureGen.Tables;
using DnDGen.CreatureGen.Templates;
using DnDGen.CreatureGen.Verifiers.Exceptions;
using DnDGen.Infrastructure.Factories;
using DnDGen.Infrastructure.Selectors.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Generators.Alignments
{
    internal class AlignmentGenerator(ICollectionSelector collectionSelector, JustInTimeFactory factory) : IAlignmentGenerator
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
                    creatureName,
                    null);

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

                //INFO: Doing this instead of intersect in order to preserve duplicates
                weightedAlignments = weightedAlignments.Where(templateAlignments.Contains);

                return weightedAlignments;
            }

            var applicator = factory.Build<TemplateApplicator>(templatesArray[0]);
            var prototypes = applicator.GetCompatiblePrototypes([creatureName], false);

            for (var i = 1; i < templatesArray.Length; i++)
            {
                applicator = factory.Build<TemplateApplicator>(templatesArray[i]);

                //INFO: The only filter we would care about would be a preset alignment, which we already handle earlier
                //So we do not need to pass filters to the applicators
                prototypes = applicator.GetCompatiblePrototypes(prototypes, false);
            }

            //INFO: At this point, after multiple templates, we are choosing to ignore weighting
            weightedAlignments = prototypes.SelectMany(p => p.Alignments).Select(a => a.Full);

            return weightedAlignments;
        }
    }
}