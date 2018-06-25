using CreatureGen.Attacks;
using System;
using System.Collections.Generic;

namespace CreatureGen.Selectors.Collections
{
    internal class AttackSelector : IAttackSelector
    {
        public IEnumerable<Attack> Select(string creatureName, string size)
        {
            throw new NotImplementedException();

            //INFO: Damage for natural attacks needs to be "advanced" by size - see monster improvement
        }
    }
}
