using CreatureGen.Creatures;
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

        public IEnumerable<Skill> GenerateFor(Creature creature)
        {
            eventQueue.Enqueue("CreatureGen", $"Generating skills for {creature.Summary}");
            var skills = innerGenerator.GenerateFor(creature);

            var skillNames = skills.Select(s => s.Name);
            eventQueue.Enqueue("CreatureGen", $"Generated skills: [{string.Join(", ", skillNames)}]");

            return skills;
        }
    }
}
