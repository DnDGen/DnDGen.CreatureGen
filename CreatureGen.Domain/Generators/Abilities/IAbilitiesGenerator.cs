using CreatureGen.Abilities;
using CreatureGen.CharacterClasses;
using CreatureGen.Creatures;
using CreatureGen.Randomizers.Abilities;
using System.Collections.Generic;

namespace CreatureGen.Domain.Generators.Abilities
{
    internal interface IAbilitiesGenerator
    {
        Dictionary<string, Ability> GenerateWith(IAbilitiesRandomizer abilitiesRandomizer, CharacterClass characterClass, Race race);
    }
}