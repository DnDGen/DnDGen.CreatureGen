using CreatureGen.Abilities;
using CreatureGen.Creatures;
using CreatureGen.Defenses;
using CreatureGen.Feats;
using CreatureGen.Skills;
using EventGen;
using System.Collections.Generic;
using System.Linq;

namespace CreatureGen.Generators.Feats
{
    internal class FeatsGeneratorEventDecorator : IFeatsGenerator
    {
        private readonly GenEventQueue eventQueue;
        private readonly IFeatsGenerator innerGenerator;

        public FeatsGeneratorEventDecorator(IFeatsGenerator innerGenerator, GenEventQueue eventQueue)
        {
            this.innerGenerator = innerGenerator;
            this.eventQueue = eventQueue;
        }

        public IEnumerable<Feat> GenerateFeats(Creature creature)
        {
            eventQueue.Enqueue("CreatureGen", $"Generating feats for {creature.Summary}");
            var feats = innerGenerator.GenerateFeats(creature);
            eventQueue.Enqueue("CreatureGen", $"Generated {feats.Count()} feats");

            return feats;
        }

        public IEnumerable<Feat> GenerateSpecialQualities(string creatureName, HitPoints hitPoints, string size, Dictionary<string, Ability> abilities, IEnumerable<Skill> skills)
        {
            eventQueue.Enqueue("CreatureGen", $"Generating special qualities for {creatureName}");
            var specialQualities = innerGenerator.GenerateSpecialQualities(creatureName, hitPoints, size, abilities, skills);
            eventQueue.Enqueue("CreatureGen", $"Generated {specialQualities.Count()} special qualities");

            return specialQualities;
        }
    }
}
