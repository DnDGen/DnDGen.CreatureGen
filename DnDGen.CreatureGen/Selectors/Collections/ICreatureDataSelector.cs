using DnDGen.CreatureGen.Selectors.Selections;
using System;
using System.Collections.Generic;

namespace DnDGen.CreatureGen.Selectors.Collections
{
    [Obsolete]
    internal interface ICreatureDataSelector
    {
        CreatureDataSelection SelectFor(string creatureName);
        Dictionary<string, CreatureDataSelection> SelectAll();
    }
}
