using CreatureGen.Creatures;
using EventGen;
using System.Collections.Generic;
using System.Linq;

namespace CreatureGen.Domain.Generators.Creatures
{
    internal class CreatureGeneratorEventDecorator : ICreatureGenerator
    {
        private readonly GenEventQueue eventQueue;
        private readonly ICreatureGenerator innerGenerator;

        public CreatureGeneratorEventDecorator(ICreatureGenerator innerGenerator, GenEventQueue eventQueue)
        {
            this.innerGenerator = innerGenerator;
            this.eventQueue = eventQueue;
        }

        public Creature Generate(string creatureName, string template)
        {
            eventQueue.Enqueue("CreatureGen", $"Generating {template} {creatureName}");
            var creature = innerGenerator.Generate(creatureName, template);
            eventQueue.Enqueue("CreatureGen", $"Generated {creature.Summary}");

            return creature;
        }

        public IEnumerable<Creature> Generate(string creatureName, string template, int quantity)
        {
            eventQueue.Enqueue("CreatureGen", $"Generating {quantity} {template} {creatureName}");
            var creatures = innerGenerator.Generate(creatureName, template, quantity);
            eventQueue.Enqueue("CreatureGen", $"Generated {creatures.Count()} {creatures.First().Summary}");

            return creatures;
        }
    }
}
