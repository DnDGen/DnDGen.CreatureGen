using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Selectors.Selections;
using System.Collections.Generic;

namespace DnDGen.CreatureGen.Selectors.Collections
{
    internal interface IFeatsSelector
    {
        IEnumerable<SpecialQualityDataSelection> SelectSpecialQualities(string creature, CreatureType creatureType);
        IEnumerable<FeatDataSelection> SelectFeats();
    }
}