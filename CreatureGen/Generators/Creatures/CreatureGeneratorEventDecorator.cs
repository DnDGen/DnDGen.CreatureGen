using CreatureGen.Creatures;
using EventGen;

namespace CreatureGen.Generators.Creatures
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
            var spacer = string.IsNullOrEmpty(template) ? string.Empty : " ";
            eventQueue.Enqueue("CreatureGen", $"Generating {template}{spacer}{creatureName}");
            var creature = innerGenerator.Generate(creatureName, template);
            eventQueue.Enqueue("CreatureGen", $"Generated {creature.Summary}");

            return creature;
        }
    }
}
