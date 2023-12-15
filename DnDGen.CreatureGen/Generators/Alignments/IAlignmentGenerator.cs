using DnDGen.CreatureGen.Alignments;
using System.Collections.Generic;

namespace DnDGen.CreatureGen.Generators.Alignments
{
    internal interface IAlignmentGenerator
    {
        Alignment Generate(string creatureName, IEnumerable<string> templates, string presetAlignment);
    }
}