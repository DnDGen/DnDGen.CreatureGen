using CreatureGen.Skills;
using CreatureGen.Abilities;
using CreatureGen.CharacterClasses;
using CreatureGen.Creatures;
using System.Collections.Generic;

namespace CreatureGen.Domain.Generators.Skills
{
    internal interface ISkillsGenerator
    {
        IEnumerable<Skill> GenerateWith(CharacterClass characterClass, Race race, Dictionary<string, Ability> abilities);
    }
}