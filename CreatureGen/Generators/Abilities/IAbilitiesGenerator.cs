using CreatureGen.Abilities;
using System.Collections.Generic;

namespace CreatureGen.Generators.Abilities
{
    internal interface IAbilitiesGenerator
    {
        Dictionary<string, Ability> GenerateFor(string creatureName);
    }
}