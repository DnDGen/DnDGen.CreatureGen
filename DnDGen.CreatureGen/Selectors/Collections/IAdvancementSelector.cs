using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Selectors.Selections;
using System.Collections.Generic;

namespace DnDGen.CreatureGen.Selectors.Collections
{
    internal interface IAdvancementSelector
    {
        AdvancementSelection SelectRandomFor(string creature, IEnumerable<string> templates, CreatureType creatureType, string originalSize, string originalChallengeRating);
        bool IsAdvanced(string creature, string challengeRatingFilter);
    }
}
