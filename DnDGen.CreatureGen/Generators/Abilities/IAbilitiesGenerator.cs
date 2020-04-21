using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Items;
using System.Collections.Generic;

namespace DnDGen.CreatureGen.Generators.Abilities
{
    internal interface IAbilitiesGenerator
    {
        Dictionary<string, Ability> GenerateFor(string creatureName);
        Dictionary<string, Ability> SetMaxBonuses(Dictionary<string, Ability> abilities, Equipment equipment);
    }
}