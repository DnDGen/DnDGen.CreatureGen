using DnDGen.CreatureGen.Alignments;
using DnDGen.CreatureGen.Tables;
using DnDGen.Infrastructure.Selectors.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Generators.Alignments
{
    internal class AlignmentGenerator : IAlignmentGenerator
    {
        private readonly ICollectionSelector collectionSelector;

        public AlignmentGenerator(ICollectionSelector collectionSelector)
        {
            this.collectionSelector = collectionSelector;
        }

        public Alignment Generate(string creatureName, IEnumerable<string> templates, string presetAlignment)
        {
            if (!string.IsNullOrEmpty(presetAlignment))
                return new Alignment(presetAlignment);

            var weightedAlignments = collectionSelector.ExplodeAndPreserveDuplicates(Config.Name, TableNameConstants.Collection.AlignmentGroups, creatureName);
            templates ??= Enumerable.Empty<string>();

            foreach (var template in templates)
            {
                var templateAlignments = collectionSelector.SelectFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups, template + GroupConstants.AllowedInput);
                //INFO: Doing this instead of intersect in order to preserve duplicates
                weightedAlignments = weightedAlignments.Where(templateAlignments.Contains);
            }

            var randomAlignment = collectionSelector.SelectRandomFrom(weightedAlignments);

            return new Alignment(randomAlignment);
        }
    }
}