using CreatureGen.Alignments;
using CreatureGen.Tables;
using DnDGen.Core.Selectors.Collections;
using System.Collections.Generic;
using System.Linq;

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
            //INFO: Not using Explode as that does deduplication, and we want duplication/weighting with these alignments
            var alignmentGroups = collectionSelector.SelectFrom(TableNameConstants.Set.Collection.AlignmentGroups, creatureName);
            var weightedAlignments = new List<string>(alignmentGroups);

            while (weightedAlignments.Any(a => collectionSelector.IsCollection(TableNameConstants.Set.Collection.AlignmentGroups, a)))
            {
                alignmentGroups = weightedAlignments.Where(a => collectionSelector.IsCollection(TableNameConstants.Set.Collection.AlignmentGroups, a)).ToArray();

                foreach (var alignmentGroup in alignmentGroups)
                {
                    var alignments = collectionSelector.SelectFrom(TableNameConstants.Set.Collection.AlignmentGroups, alignmentGroup);
                    weightedAlignments.AddRange(alignments);
                    weightedAlignments.Remove(alignmentGroup);
                }
            }

            var randomAlignment = collectionSelector.SelectRandomFrom(weightedAlignments);

            return new Alignment(randomAlignment);
        }
    }
}