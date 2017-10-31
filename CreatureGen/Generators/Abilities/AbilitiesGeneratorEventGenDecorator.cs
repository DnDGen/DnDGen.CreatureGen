using CreatureGen.Abilities;
using EventGen;
using System.Collections.Generic;
using System.Linq;

namespace CreatureGen.Generators.Abilities
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

        public Dictionary<string, Ability> GenerateFor(string creatureName)
        {
            eventQueue.Enqueue("CharacterGen", $"Generating abilities for {creatureName}");
            var abilities = innerGenerator.GenerateFor(creatureName);

            var abilityDescriptions = abilities.Values.Select(a => $"{a.Name} {a.BaseValue}");
            eventQueue.Enqueue("CharacterGen", $"Generated abilities: [{string.Join(", ", abilityDescriptions)}]");

            return abilities;
        }
    }
}
