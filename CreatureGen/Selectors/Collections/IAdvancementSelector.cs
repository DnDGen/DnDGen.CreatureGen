using CreatureGen.Selectors.Selections;

namespace CreatureGen.Selectors.Collections
{
    internal interface IAdvancementSelector
    {
        AdvancementSelection SelectRandomFor(string creature);
        bool IsAdvanced(string creature);
    }
}
