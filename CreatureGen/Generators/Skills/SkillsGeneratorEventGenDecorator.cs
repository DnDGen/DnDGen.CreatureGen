using CreatureGen.Abilities;
using CreatureGen.Creatures;
using CreatureGen.Defenses;
using CreatureGen.Feats;
using CreatureGen.Skills;
using EventGen;
using System.Collections.Generic;
using System.Linq;

namespace CreatureGen.Generators.Skills
{
    internal class SkillsGeneratorEventGenDecorator : ISkillsGenerator
    {
        private readonly GenEventQueue eventQueue;
        private readonly ISkillsGenerator innerGenerator;

        public SkillsGeneratorEventGenDecorator(ISkillsGenerator innerGenerator, GenEventQueue eventQueue)
        {
            this.innerGenerator = innerGenerator;
            this.eventQueue = eventQueue;
        }

        public IEnumerable<Skill> ApplyBonusesFromFeats(IEnumerable<Skill> skills, IEnumerable<Feat> feats, Dictionary<string, Ability> abilities)
        {
            var updatedSkills = innerGenerator.ApplyBonusesFromFeats(skills, feats, abilities);

            return updatedSkills;
        }

        public IEnumerable<Skill> GenerateFor(HitPoints hitPoints, string creatureName, CreatureType creatureType, Dictionary<string, Ability> abilities, bool canUseEquipment, string size)
        {
            eventQueue.Enqueue("CreatureGen", $"Generating skills for {creatureName}");
            var skills = innerGenerator.GenerateFor(hitPoints, creatureName, creatureType, abilities, canUseEquipment, size);
            eventQueue.Enqueue("CreatureGen", $"Generated {skills.Count()} skills");

            return skills;
        }
    }
}
