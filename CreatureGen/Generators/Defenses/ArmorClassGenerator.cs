using CreatureGen.Abilities;
using CreatureGen.Defenses;
using CreatureGen.Selectors.Collections;
using CreatureGen.Tables;
using CreatureGen.Feats;
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
            armorClass.DeflectionBonus = adjustmentsSelector.SelectFrom(TableNameConstants.Set.Adjustments.ArmorDeflectionBonuses, creatureName);
            armorClass.NaturalArmorBonus = GetNaturalArmorBonus(feats);
            armorClass.SizeModifier = adjustmentsSelector.SelectFrom(TableNameConstants.Set.Adjustments.SizeModifiers, size);
            armorClass.CircumstantialBonus = IsNaturalArmorBonusCircumstantial(feats);

            return armorClass;
        }

        private bool IsNaturalArmorBonusCircumstantial(IEnumerable<Feat> feats)
        {
            var thingsThatGrantNaturalArmorBonuses = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ArmorClassModifiers, GroupConstants.NaturalArmor);
            var featsWithNaturalArmorBonuses = feats.Where(f => thingsThatGrantNaturalArmorBonuses.Contains(f.Name));

            return featsWithNaturalArmorBonuses.Any(f => f.Foci.Any());
        }

        private bool IsDodgeBonusCircumstantial(IEnumerable<Feat> feats)
        {
            var deflectionBonuses = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ArmorClassModifiers, GroupConstants.DodgeBonus);
            var featsWithDeflectionBonuses = feats.Where(f => deflectionBonuses.Contains(f.Name));

            return featsWithDeflectionBonuses.Any(f => f.Foci.Any());
        }

        private int GetNaturalArmorBonus(IEnumerable<Feat> feats)
        {
            var thingsThatGrantNaturalArmorBonuses = collectionsSelector.SelectFrom(TableNameConstants.Set.Collection.ArmorClassModifiers, GroupConstants.NaturalArmor);
            var featsWithNaturalArmorBonuses = feats.Where(f => thingsThatGrantNaturalArmorBonuses.Contains(f.Name) && !f.Foci.Any());
            var featNaturalArmorBonuses = featsWithNaturalArmorBonuses.Select(f => f.Power);
            var featNaturalArmorBonus = featNaturalArmorBonuses.Sum();

            return featNaturalArmorBonus;
        }
    }
}