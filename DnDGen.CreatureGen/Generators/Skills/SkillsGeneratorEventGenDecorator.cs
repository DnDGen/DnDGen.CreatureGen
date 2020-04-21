using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Defenses;
using DnDGen.CreatureGen.Feats;
using DnDGen.CreatureGen.Items;
using DnDGen.CreatureGen.Skills;
using DnDGen.EventGen;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Generators.Skills
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

        public IEnumerable<Skill> SetArmorCheckPenalties(IEnumerable<Skill> skills, Equipment equipment)
        {
            eventQueue.Enqueue("CreatureGen", $"Setting armor check penalties");
            var modifiedSkills = innerGenerator.SetArmorCheckPenalties(skills, equipment);
            eventQueue.Enqueue("CreatureGen", $"Set armor check penalties");

            return modifiedSkills;
        }
    }
}
