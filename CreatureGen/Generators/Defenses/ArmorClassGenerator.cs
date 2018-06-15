using CreatureGen.Abilities;
using CreatureGen.Defenses;
using CreatureGen.Feats;
using CreatureGen.Selectors.Collections;
using CreatureGen.Tables;
using DnDGen.Core.Selectors.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CreatureGen.Generators.Defenses
{
    internal class ArmorClassGenerator : IArmorClassGenerator
    {
        private readonly ICollectionSelector collectionsSelector;
        private readonly IAdjustmentsSelector adjustmentsSelector;

        public ArmorClassGenerator(ICollectionSelector collectionsSelector, IAdjustmentsSelector adjustmentsSelector)
        {
            this.collectionsSelector = collectionsSelector;
            this.adjustmentsSelector = adjustmentsSelector;
        }

        public ArmorClass GenerateWith(Ability dexterity, string size, string creatureName, IEnumerable<Feat> feats)
        {
            var armorClass = new ArmorClass();
            armorClass.Dexterity = dexterity;
            armorClass.DeflectionBonus = adjustmentsSelector.SelectFrom<int>(TableNameConstants.Set.Adjustments.ArmorDeflectionBonuses, creatureName);
            armorClass.SizeModifier = adjustmentsSelector.SelectFrom<int>(TableNameConstants.Set.Adjustments.SizeModifiers, size);
            armorClass.ArmorBonus = GetArmorBonus(feats);

            return armorClass;
        }

        private int GetArmorBonus(IEnumerable<Feat> feats)
        {
            var thingsThatGrantArmorBonuses = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ArmorClassModifiers, GroupConstants.ArmorBonus);
            var featsWithArmorBonuses = feats.Where(f => thingsThatGrantArmorBonuses.Contains(f.Name) && !f.Foci.Any());
            var featArmorBonuses = featsWithArmorBonuses.Select(f => f.Power);
            var featArmorBonus = featArmorBonuses.Sum();

            return featArmorBonus;
        }
    }
}