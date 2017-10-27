using CreatureGen.Abilities;
using CreatureGen.Feats;
using CreatureGen.Creatures;
using CreatureGen.Skills;
using System.Collections.Generic;

namespace CreatureGen.Domain.Generators.Feats
{
    internal interface IRacialFeatsGenerator
    {
        IEnumerable<Feat> GenerateWith(Race race, IEnumerable<Skill> skills, Dictionary<string, Ability> abilities);
    }
}