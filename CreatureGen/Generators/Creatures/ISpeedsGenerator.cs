using CreatureGen.Creatures;
using System.Collections.Generic;

namespace CreatureGen.Generators.Creatures
{
    internal interface ISpeedsGenerator
    {
        Dictionary<string, Measurement> Generate(string creatureName);
    }
}
