using CreatureGen.Abilities;
using CreatureGen.Attacks;
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

        public IEnumerable<Feat> GenerateFeats(
            HitPoints hitPoints,
            int baseAttackBonus,
            Dictionary<string, Ability> abilities,
            IEnumerable<Skill> skills,
            IEnumerable<Attack> attacks,
            IEnumerable<Feat> specialQualities,
            int casterLevel,
            Dictionary<string, Measurement> speeds,
            int naturalArmor,
            int hands,
            string size)
        {
            eventQueue.Enqueue("CreatureGen", $"Generating feats");
            var feats = innerGenerator.GenerateFeats(
                hitPoints,
                baseAttackBonus,
                abilities,
                skills,
                attacks,
                specialQualities,
                casterLevel,
                speeds,
                naturalArmor,
                hands,
                size);

            eventQueue.Enqueue("CreatureGen", $"Generated {feats.Count()} feats");

            return feats;
        }

        public IEnumerable<Feat> GenerateSpecialQualities(string creatureName, HitPoints hitPoints, Dictionary<string, Ability> abilities, IEnumerable<Skill> skills)
        {
            eventQueue.Enqueue("CreatureGen", $"Generating special qualities for {creatureName}");
            var specialQualities = innerGenerator.GenerateSpecialQualities(creatureName, hitPoints, abilities, skills);
            eventQueue.Enqueue("CreatureGen", $"Generated {specialQualities.Count()} special qualities");

            return specialQualities;
        }
    }
}
