using DnDGen.CreatureGen.Alignments;
using DnDGen.CreatureGen.Tables;
using DnDGen.Infrastructure.Selectors.Collections;
using System.Linq;

namespace DnDGen.CreatureGen.Generators.Alignments
{
    internal class AlignmentGenerator : IAlignmentGenerator
    {
        private ICollectionSelector collectionSelector;

        public AlignmentGenerator(ICollectionSelector collectionSelector)
        {
            this.collectionSelector = collectionSelector;
        }

        public Alignment Generate(string creatureName, string template, string presetAlignment)
        {
            if (!string.IsNullOrEmpty(presetAlignment))
                return new Alignment(presetAlignment);

            var weightedAlignments = collectionSelector.ExplodeAndPreserveDuplicates(TableNameConstants.Collection.AlignmentGroups, creatureName);

            if (template != null)
            {
                var templateAlignments = collectionSelector.SelectFrom(TableNameConstants.Collection.AlignmentGroups, template + GroupConstants.AllowedInput);
                //INFO: Doing this instead of intersect in order to preserve duplicates
                weightedAlignments = weightedAlignments.Where(templateAlignments.Contains);
            }

            var randomAlignment = collectionSelector.SelectRandomFrom(weightedAlignments);

            return new Alignment(randomAlignment);
        }
    }
}