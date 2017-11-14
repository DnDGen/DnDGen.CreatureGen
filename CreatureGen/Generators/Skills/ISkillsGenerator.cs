using CreatureGen.Abilities;
using CreatureGen.Defenses;
using CreatureGen.Feats;
using CreatureGen.Skills;
using System.Collections.Generic;

namespace CreatureGen.Generators.Skills
{
    internal interface ISkillsGenerator
    {
        IEnumerable<Skill> GenerateFor(HitPoints hitPoints, string creatureName, Dictionary<string, Ability> abilities);
        IEnumerable<Skill> ApplyBonusesFromFeats(IEnumerable<Skill> skills, IEnumerable<Feat> feats);
    }
}