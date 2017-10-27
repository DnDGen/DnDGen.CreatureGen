using CreatureGen.Abilities;
using CreatureGen.CharacterClasses;
using CreatureGen.Combats;
using CreatureGen.Feats;
using CreatureGen.Creatures;
using CreatureGen.Skills;
using EventGen;
using System.Collections.Generic;

namespace CreatureGen.Domain.Generators.Feats
{
    internal class FeatsGeneratorEventGenDecorator : IFeatsGenerator
    {
        private readonly GenEventQueue eventQueue;
        private readonly IFeatsGenerator innerGenerator;

        public FeatsGeneratorEventGenDecorator(IFeatsGenerator innerGenerator, GenEventQueue eventQueue)
        {
            this.innerGenerator = innerGenerator;
            this.eventQueue = eventQueue;
        }

        public FeatCollections GenerateWith(CharacterClass characterClass, Race race, Dictionary<string, Ability> abilities, IEnumerable<Skill> skills, BaseAttack baseAttack)
        {
            eventQueue.Enqueue("CharacterGen", $"Generating feats for {characterClass.Summary} {race.Summary}");
            var feats = innerGenerator.GenerateWith(characterClass, race, abilities, skills, baseAttack);
            eventQueue.Enqueue("CharacterGen", $"Generated feats");

            return feats;
        }
    }
}
