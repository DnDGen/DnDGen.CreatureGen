using CreatureGen.Creatures;
using CreatureGen.Skills;
using System.Collections.Generic;

namespace CreatureGen.Generators.Skills
{
    internal interface ISkillsGenerator
    {
        IEnumerable<Skill> GenerateFor(Creature creature);
    }
}