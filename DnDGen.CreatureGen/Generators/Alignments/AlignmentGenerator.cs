using DnDGen.CreatureGen.Alignments;
using DnDGen.CreatureGen.Tables;
using DnDGen.Infrastructure.Selectors.Collections;

namespace DnDGen.CreatureGen.Generators.Alignments
{
    internal class AlignmentGenerator : IAlignmentGenerator
    {
        private ICollectionSelector collectionSelector;

        public AlignmentGenerator(ICollectionSelector collectionSelector)
        {
            this.collectionSelector = collectionSelector;
        }

        public Alignment Generate(string creatureName, string presetAlignment)
        {
            if (!string.IsNullOrEmpty(presetAlignment))
                return new Alignment(presetAlignment);

            var weightedAlignments = collectionSelector.ExplodeAndPreserveDuplicates(TableNameConstants.Collection.AlignmentGroups, creatureName);
            var randomAlignment = collectionSelector.SelectRandomFrom(weightedAlignments);

            return new Alignment(randomAlignment);
        }
    }
}