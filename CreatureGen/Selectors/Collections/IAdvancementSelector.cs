using CreatureGen.Creatures;
using CreatureGen.Selectors.Selections;

namespace CreatureGen.Selectors.Collections
{
    internal interface IAdvancementSelector
    {
        AdvancementSelection SelectRandomFor(string creature, CreatureType creatureType);
        bool IsAdvanced(string creature);
    }
}
