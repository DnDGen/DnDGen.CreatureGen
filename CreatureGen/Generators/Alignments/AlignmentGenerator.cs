using CreatureGen.Alignments;
using CreatureGen.Tables;
using DnDGen.Core.Selectors.Collections;

namespace CreatureGen.Generators.Alignments
{
    internal class AlignmentGenerator : IAlignmentGenerator
    {
        private ICollectionSelector collectionSelector;

        public AlignmentGenerator(ICollectionSelector collectionSelector)
        {
            this.collectionSelector = collectionSelector;
        }

        public Alignment Generate(string creatureName)
        {
            var weightedAlignments = collectionSelector.ExplodeAndPreserveDuplicates(TableNameConstants.Set.Collection.AlignmentGroups, creatureName);
            var randomAlignment = collectionSelector.SelectRandomFrom(weightedAlignments);

            return new Alignment(randomAlignment);
        }
    }
}