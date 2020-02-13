using DnDGen.CreatureGen.Creatures;
using System.Collections.Generic;

namespace DnDGen.CreatureGen.Generators.Creatures
{
    internal interface ISpeedsGenerator
    {
        Dictionary<string, Measurement> Generate(string creatureName);
    }
}
