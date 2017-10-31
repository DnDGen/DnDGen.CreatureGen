using CreatureGen.Selectors.Selections;

namespace CreatureGen.Selectors.Collections
{
    internal interface ICreatureDataSelector
    {
        CreatureDataSelection SelectFor(string creatureName);
    }
}
