using DnDGen.CreatureGen.Alignments;
using DnDGen.CreatureGen.Generators.Creatures;

namespace DnDGen.CreatureGen.Generators.Alignments
{
    internal interface IAlignmentGenerator
    {
        Alignment Generate(string creatureName, string[] templates, Filters filters);
    }
}