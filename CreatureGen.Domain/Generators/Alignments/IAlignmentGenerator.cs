using CreatureGen.Alignments;
using CreatureGen.Randomizers.Alignments;

namespace CreatureGen.Domain.Generators.Alignments
{
    internal interface IAlignmentGenerator
    {
        Alignment GenerateWith(Alignment alignmentPrototype);
        Alignment GeneratePrototype(IAlignmentRandomizer alignmentRandomizer);
    }
}