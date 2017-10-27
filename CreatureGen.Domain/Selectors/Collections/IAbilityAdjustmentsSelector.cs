using CreatureGen.Creatures;
using System.Collections.Generic;

namespace CreatureGen.Domain.Selectors.Collections
{
    internal interface IAbilityAdjustmentsSelector
    {
        Dictionary<string, int> SelectFor(Race race);
    }
}