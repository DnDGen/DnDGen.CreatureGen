using CreatureGen.Abilities;
using CreatureGen.CharacterClasses;
using CreatureGen.Creatures;
using CreatureGen.Randomizers.Abilities;
using EventGen;
using System.Collections.Generic;
using System.Linq;

namespace CreatureGen.Domain.Generators.Abilities
{
    internal class AbilitiesGeneratorEventGenDecorator : IAbilitiesGenerator
    {
        private readonly GenEventQueue eventQueue;
        private readonly IAbilitiesGenerator innerGenerator;

        public AbilitiesGeneratorEventGenDecorator(IAbilitiesGenerator innerGenerator, GenEventQueue eventQueue)
        {
            this.innerGenerator = innerGenerator;
            this.eventQueue = eventQueue;
        }

        public Dictionary<string, Ability> GenerateWith(IAbilitiesRandomizer abilitiesRandomizer, CharacterClass characterClass, Race race)
        {
            eventQueue.Enqueue("CharacterGen", $"Generating abilities for {characterClass.Summary} {race.Summary}");
            var abilities = innerGenerator.GenerateWith(abilitiesRandomizer, characterClass, race);

            var abilityDescriptions = abilities.Values.Select(a => $"{a.Name} {a.BaseValue}");
            eventQueue.Enqueue("CharacterGen", $"Generated abilities: [{string.Join(", ", abilityDescriptions)}]");

            return abilities;
        }
    }
}
