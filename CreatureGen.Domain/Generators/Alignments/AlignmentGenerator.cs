using CreatureGen.Alignments;
using CreatureGen.Randomizers.Alignments;

namespace CreatureGen.Domain.Generators.Alignments
{
    internal class AlignmentGenerator : IAlignmentGenerator
    {
        public Alignment GeneratePrototype(IAlignmentRandomizer alignmentRandomizer)
        {
            return alignmentRandomizer.Randomize();
        }

        public Alignment GenerateWith(Alignment alignmentPrototype)
        {
            return alignmentPrototype;
        }
    }
}