using CreatureGen.Abilities;
using CreatureGen.CharacterClasses;
using CreatureGen.Creatures;
using CreatureGen.Skills;
using EventGen;
using System.Collections.Generic;
using System.Linq;

namespace CreatureGen.Domain.Generators.Skills
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

        public IEnumerable<Skill> GenerateWith(CharacterClass characterClass, Race race, Dictionary<string, Ability> abilities)
        {
            eventQueue.Enqueue("CharacterGen", $"Generating skills for {characterClass.Summary} {race.Summary}");
            var skills = innerGenerator.GenerateWith(characterClass, race, abilities);

            var skillNames = skills.Select(s => s.Name);
            eventQueue.Enqueue("CharacterGen", $"Generated skills: [{string.Join(", ", skillNames)}]");

            return skills;
        }
    }
}
