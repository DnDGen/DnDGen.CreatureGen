using CreatureGen.Alignments;
using EventGen;

namespace CreatureGen.Generators.Alignments
{
    internal class AlignmentGeneratorEventGenDecorator : IAlignmentGenerator
    {
        private readonly GenEventQueue eventQueue;
        private readonly IAlignmentGenerator innerGenerator;

        public AlignmentGeneratorEventGenDecorator(IAlignmentGenerator innerGenerator, GenEventQueue eventQueue)
        {
            this.innerGenerator = innerGenerator;
            this.eventQueue = eventQueue;
        }

        public Alignment Generate(string creatureName)
        {
            eventQueue.Enqueue("CharacterGen", $"Generating alignment for {creatureName}");
            var alignment = innerGenerator.Generate(creatureName);
            eventQueue.Enqueue("CharacterGen", $"Generated {alignment.Full}");

            return alignment;
        }
    }
}
