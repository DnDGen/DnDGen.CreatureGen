using CreatureGen.Abilities;
using CreatureGen.CharacterClasses;
using CreatureGen.Feats;
using CreatureGen.Creatures;
using CreatureGen.Skills;
using System.Collections.Generic;

namespace CreatureGen.Domain.Generators.Feats
{
    internal interface IClassFeatsGenerator
    {
        IEnumerable<Feat> GenerateWith(CharacterClass characterClass, Race race, Dictionary<string, Ability> abilities, IEnumerable<Feat> racialFeats, IEnumerable<Skill> skills);
    }
}