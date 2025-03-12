using DnDGen.CreatureGen.Selectors.Selections;
using System.Collections.Generic;

namespace DnDGen.CreatureGen.Selectors.Collections
{
    internal interface IAdvancementSelector
    {
        AdvancementDataSelection SelectRandomFor(string creature, IEnumerable<string> templates);
        bool IsAdvanced(string creature, IEnumerable<string> templates, string challengeRatingFilter);
    }
}
