using DnDGen.CreatureGen.Alignments;
using DnDGen.EventGen;

namespace DnDGen.CreatureGen.Generators.Alignments
{
    internal class AlignmentGeneratorEventDecorator : IAlignmentGenerator
    {
        private readonly GenEventQueue eventQueue;
        private readonly IAlignmentGenerator innerGenerator;

        public AlignmentGeneratorEventDecorator(IAlignmentGenerator innerGenerator, GenEventQueue eventQueue)
        {
            this.innerGenerator = innerGenerator;
            this.eventQueue = eventQueue;
        }

        public Alignment Generate(string creatureName)
        {
            eventQueue.Enqueue("CreatureGen", $"Generating alignment for {creatureName}");
            var alignment = innerGenerator.Generate(creatureName);

            if (string.IsNullOrEmpty(alignment.Full))
                eventQueue.Enqueue("CreatureGen", $"Generated no alignment");
            else
                eventQueue.Enqueue("CreatureGen", $"Generated {alignment.Full}");

            return alignment;
        }
    }
}
