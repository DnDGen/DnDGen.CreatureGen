using CreatureGen.Domain.Selectors.Selections;
using EventGen;
using System.Collections.Generic;

namespace CreatureGen.Domain.Selectors.Collections
{
    internal class FeatsSelectorEventGenDecorator : IFeatsSelector
    {
        private readonly GenEventQueue eventQueue;
        private readonly IFeatsSelector innerGenerator;

        public FeatsSelectorEventGenDecorator(IFeatsSelector innerGenerator, GenEventQueue eventQueue)
        {
            this.innerGenerator = innerGenerator;
            this.eventQueue = eventQueue;
        }

        public IEnumerable<AdditionalFeatSelection> SelectAdditional()
        {
            eventQueue.Enqueue("CharacterGen", $"Selecting additional feat data");
            var featSelections = innerGenerator.SelectAdditional();
            eventQueue.Enqueue("CharacterGen", $"Selected additional feat data");

            return featSelections;
        }

        public IEnumerable<CharacterClassFeatSelection> SelectClass(string characterClassName)
        {
            eventQueue.Enqueue("CharacterGen", $"Selecting class feat data for {characterClassName}");
            var featSelections = innerGenerator.SelectClass(characterClassName);
            eventQueue.Enqueue("CharacterGen", $"Selected class feat data for {characterClassName}");

            return featSelections;
        }

        public IEnumerable<RacialFeatSelection> SelectRacial(string race)
        {
            eventQueue.Enqueue("CharacterGen", $"Selecting racial feat data for {race}");
            var featSelections = innerGenerator.SelectRacial(race);
            eventQueue.Enqueue("CharacterGen", $"Selected racial feat data for {race}");

            return featSelections;
        }
    }
}
