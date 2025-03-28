using DnDGen.CreatureGen.Selectors.Selections;
using DnDGen.CreatureGen.Tables;
using DnDGen.Infrastructure.Selectors.Collections;
using System.Collections.Generic;

namespace DnDGen.CreatureGen.Selectors.Collections
{
    internal class AttackSelector : IAttackSelector
    {
        private readonly ICollectionDataSelector<AttackDataSelection> attackDataSelector;
        private readonly ICollectionDataSelector<DamageDataSelection> damageDataSelector;

        public AttackSelector(ICollectionDataSelector<AttackDataSelection> attackDataSelector, ICollectionDataSelector<DamageDataSelection> damageDataSelector)
        {
            this.attackDataSelector = attackDataSelector;
            this.damageDataSelector = damageDataSelector;
        }

        public IEnumerable<AttackDataSelection> Select(string creatureName, string size)
        {
            var attackSelections = attackDataSelector.SelectFrom(Config.Name, TableNameConstants.Collection.AttackData, creatureName);

            foreach (var selection in attackSelections)
            {
                var key = selection.BuildDamageKey(creatureName, size);
                var damageSelections = damageDataSelector.SelectFrom(Config.Name, TableNameConstants.Collection.DamageData, key);
                selection.Damages.AddRange(damageSelections);
            }

            return attackSelections;
        }
    }
}
