using CreatureGen.Alignments;
using CreatureGen.Randomizers.Alignments;
using EventGen;

namespace CreatureGen.Domain.Generators.Alignments
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

        public Alignment GeneratePrototype(IAlignmentRandomizer alignmentRandomizer)
        {
            var prototype = innerGenerator.GeneratePrototype(alignmentRandomizer);

            return prototype;
        }

        public Alignment GenerateWith(Alignment alignmentPrototype)
        {
            eventQueue.Enqueue("CharacterGen", $"Generating alignment from prototype {alignmentPrototype.Full}");
            var alignment = innerGenerator.GenerateWith(alignmentPrototype);
            eventQueue.Enqueue("CharacterGen", $"Generated {alignment.Full}");

            return alignment;
        }
    }
}
