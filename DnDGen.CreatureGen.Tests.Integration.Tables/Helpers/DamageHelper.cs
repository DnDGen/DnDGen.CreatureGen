using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Selectors.Selections;
using DnDGen.CreatureGen.Tables;
using DnDGen.Infrastructure.Selectors.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Integration.Tables.Helpers
{
    internal class DamageHelper
    {
        private readonly ICollectionDataSelector<CreatureDataSelection> creatureDataSelector;
        private readonly ICollectionDataSelector<AdvancementDataSelection> advancementDataSelector;
        private readonly ICollectionDataSelector<AttackDataSelection> attackDataSelector;

        public DamageHelper(ICollectionDataSelector<CreatureDataSelection> creatureDataSelector, ICollectionDataSelector<AdvancementDataSelection> advancementDataSelector, ICollectionDataSelector<AttackDataSelection> attackDataSelector)
        {
            this.creatureDataSelector = creatureDataSelector;
            this.advancementDataSelector = advancementDataSelector;
            this.attackDataSelector = attackDataSelector;
        }

        public IEnumerable<string> GetAllCreatureDamageKeys()
        {
            var creatures = CreatureConstants.GetAll();
            var attackDamageKeys = new List<string>();

            foreach (var creature in creatures)
            {
                var keys = GetCreatureDamageKeys(creature);
                attackDamageKeys.AddRange(keys);
            }

            return attackDamageKeys;
        }

        public IEnumerable<string> GetCreatureDamageKeys(string creature)
        {
            var attackDamageKeys = new List<string>();
            var creatureAttackData = attackDataSelector.SelectFrom(Config.Name, TableNameConstants.Collection.AttackData, creature);
            var advancementData = advancementDataSelector.SelectFrom(Config.Name, TableNameConstants.Collection.Advancements, creature);
            var creatureData = creatureDataSelector.SelectOneFrom(Config.Name, TableNameConstants.Collection.CreatureData, creature);

            var sizes = advancementData
                .Select(a => a.Size)
                .Union([creatureData.Size])
                //INFO: This ensures smaller sizes come first
                .OrderDescending();

            foreach (var size in sizes)
            {
                var keys = creatureAttackData.Select(s => s.BuildDamageKey(creature, size));
                attackDamageKeys.AddRange(keys);
            }

            return attackDamageKeys;
        }

        public IEnumerable<string> GetAllTemplateDamageKeys()
        {
            var templates = CreatureConstants.Templates.GetAll();
            var attackDamageKeys = new List<string>();

            foreach (var template in templates)
            {
                var keys = GetTemplateDamageKeys(template);
                attackDamageKeys.AddRange(keys);
            }

            return attackDamageKeys;
        }

        public IEnumerable<string> GetTemplateDamageKeys(string template)
        {
            var attackDamageKeys = new List<string>();
            var templateAttackData = attackDataSelector.SelectFrom(Config.Name, TableNameConstants.Collection.AttackData, template);
            var sizes = SizeConstants.GetOrdered();

            foreach (var size in sizes)
            {
                var keys = templateAttackData.Select(s => s.BuildDamageKey(template, size));
                attackDamageKeys.AddRange(keys);
            }

            return attackDamageKeys;
        }
    }
}
