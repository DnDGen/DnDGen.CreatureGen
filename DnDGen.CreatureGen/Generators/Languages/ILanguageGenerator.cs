using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Skills;
using System.Collections.Generic;

namespace DnDGen.CreatureGen.Generators.Languages
{
    internal interface ILanguageGenerator
    {
        IEnumerable<string> GenerateWith(string creature, Dictionary<string, Ability> abilities, IEnumerable<Skill> skills);
    }
}