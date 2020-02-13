using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Alignments;
using DnDGen.CreatureGen.Attacks;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Defenses;
using DnDGen.CreatureGen.Feats;
using DnDGen.CreatureGen.Skills;
using DnDGen.EventGen;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Generators.Feats
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
            string size,
            bool canUseEquipment)
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
                size,
                canUseEquipment);

            eventQueue.Enqueue("CreatureGen", $"Generated {feats.Count()} feats");

            return feats;
        }

        public IEnumerable<Feat> GenerateSpecialQualities(
            string creatureName,
            CreatureType creatureType,
            HitPoints hitPoints,
            Dictionary<string, Ability> abilities,
            IEnumerable<Skill> skills,
            bool canUseEquipment,
            string size,
            Alignment alignment)
        {
            eventQueue.Enqueue("CreatureGen", $"Generating special qualities for {creatureName}");
            var specialQualities = innerGenerator.GenerateSpecialQualities(creatureName, creatureType, hitPoints, abilities, skills, canUseEquipment, size, alignment);
            eventQueue.Enqueue("CreatureGen", $"Generated {specialQualities.Count()} special qualities");

            return specialQualities;
        }
    }
}
