using DnDGen.CreatureGen.Alignments;
using DnDGen.CreatureGen.Generators.Creatures;
using System.Collections.Generic;

namespace DnDGen.CreatureGen.Generators.Alignments
{
    internal interface IAlignmentGenerator
    {
        Alignment Generate(string creatureName, IEnumerable<string> templates, Filters filters);
    }
}