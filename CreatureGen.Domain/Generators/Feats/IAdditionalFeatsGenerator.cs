using CreatureGen.Abilities;
using CreatureGen.CharacterClasses;
using CreatureGen.Combats;
using CreatureGen.Feats;
using CreatureGen.Creatures;
using CreatureGen.Skills;
using System.Collections.Generic;

namespace CreatureGen.Domain.Generators.Feats
{
    internal interface IAdditionalFeatsGenerator
    {
        IEnumerable<Feat> GenerateWith(CharacterClass characterClass, Race race, Dictionary<string, Ability> abilities, IEnumerable<Skill> skills, BaseAttack baseAttack, IEnumerable<Feat> preselectedFeats);
    }
}