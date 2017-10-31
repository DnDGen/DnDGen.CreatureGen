using CreatureGen.Attacks;
using System.Collections.Generic;

namespace CreatureGen.Selectors.Collections
{
    internal interface IAttackSelector
    {
        IEnumerable<Attack> Select(string creatureName);
    }
}
