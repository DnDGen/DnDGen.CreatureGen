using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Selectors.Selections;

namespace DnDGen.CreatureGen.Selectors.Collections
{
    internal interface IAdvancementSelector
    {
        AdvancementSelection SelectRandomFor(string creature, CreatureType creatureType, string originalSize, string originalChallengeRating);
        bool IsAdvanced(string creature);
    }
}
