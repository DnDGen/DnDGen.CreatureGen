using CreatureGen.Alignments;

namespace CreatureGen.Generators.Alignments
{
    internal interface IAlignmentGenerator
    {
        Alignment Generate(string creatureName);
    }
}