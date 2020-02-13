using DnDGen.CreatureGen.Selectors.Selections;

namespace DnDGen.CreatureGen.Selectors.Collections
{
    internal interface ICreatureDataSelector
    {
        CreatureDataSelection SelectFor(string creatureName);
    }
}
