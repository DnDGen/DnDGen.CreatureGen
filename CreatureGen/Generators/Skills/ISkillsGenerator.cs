using CreatureGen.Abilities;
using CreatureGen.Creatures;
using CreatureGen.Defenses;
using CreatureGen.Feats;
using CreatureGen.Skills;
using System.Collections.Generic;

namespace CreatureGen.Generators.Skills
{
    internal interface ISkillsGenerator
    {
        IEnumerable<Skill> GenerateFor(HitPoints hitPoints, string creatureName, CreatureType creatureType, Dictionary<string, Ability> abilities, bool canUseEquipment, string size);
        IEnumerable<Skill> ApplyBonusesFromFeats(IEnumerable<Skill> skills, IEnumerable<Feat> feats);
    }
}