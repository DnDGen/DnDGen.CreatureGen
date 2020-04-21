using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Items;
using DnDGen.EventGen;
using System.Collections.Generic;

namespace DnDGen.CreatureGen.Generators.Abilities
{
    internal class AbilitiesGeneratorEventDecorator : IAbilitiesGenerator
    {
        private readonly GenEventQueue eventQueue;
        private readonly IAbilitiesGenerator innerGenerator;

        public AbilitiesGeneratorEventDecorator(IAbilitiesGenerator innerGenerator, GenEventQueue eventQueue)
        {
            this.innerGenerator = innerGenerator;
            this.eventQueue = eventQueue;
        }

        public Dictionary<string, Ability> GenerateFor(string creatureName)
        {
            eventQueue.Enqueue("CreatureGen", $"Generating abilities for {creatureName}");
            var abilities = innerGenerator.GenerateFor(creatureName);

            eventQueue.Enqueue("CreatureGen", $"Generated {abilities.Count} abilities");

            return abilities;
        }

        public Dictionary<string, Ability> SetMaxBonuses(Dictionary<string, Ability> abilities, Equipment equipment)
        {
            eventQueue.Enqueue("CreatureGen", $"Setting max modifiers for abilities");
            var modifiedAbilities = innerGenerator.SetMaxBonuses(abilities, equipment);

            eventQueue.Enqueue("CreatureGen", $"Set max modifiers");

            return modifiedAbilities;
        }
    }
}
