using DnDGen.CreatureGen.Abilities;
using System.Collections.Generic;

namespace DnDGen.CreatureGen.Generators.Abilities
{
    internal interface IAbilitiesGenerator
    {
        Dictionary<string, Ability> GenerateFor(string creatureName);
    }
}