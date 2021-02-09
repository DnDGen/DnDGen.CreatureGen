using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Selectors.Selections;
using System.Collections.Generic;

namespace DnDGen.CreatureGen.Selectors.Collections
{
    internal interface IFeatsSelector
    {
        IEnumerable<SpecialQualitySelection> SelectSpecialQualities(string creature, CreatureType creatureType);
        IEnumerable<FeatSelection> SelectFeats();
    }
}