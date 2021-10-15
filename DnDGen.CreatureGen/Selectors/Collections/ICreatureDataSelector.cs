using DnDGen.CreatureGen.Selectors.Selections;
using System.Collections.Generic;

namespace DnDGen.CreatureGen.Selectors.Collections
{
    internal interface ICreatureDataSelector
    {
        CreatureDataSelection SelectFor(string creatureName);
        Dictionary<string, CreatureDataSelection> SelectAll();
    }
}
