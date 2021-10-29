using DnDGen.CreatureGen.Alignments;

namespace DnDGen.CreatureGen.Generators.Alignments
{
    internal interface IAlignmentGenerator
    {
        Alignment Generate(string creatureName, string template, string presetAlignment);
    }
}