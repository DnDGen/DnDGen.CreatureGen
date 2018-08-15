using CreatureGen.Abilities;
using CreatureGen.Creatures;
using CreatureGen.Defenses;
using CreatureGen.Feats;
using CreatureGen.Selectors.Collections;
using CreatureGen.Tables;
using DnDGen.Core.Selectors.Collections;
using System;
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

        public ArmorClass GenerateWith(Dictionary<string, Ability> abilities, string size, string creatureName, CreatureType creatureType, IEnumerable<Feat> feats, int naturalArmor)
        {
            var armorClass = new ArmorClass();
            armorClass.Dexterity = abilities[AbilityConstants.Dexterity];

            if (creatureType.SubTypes.Contains(CreatureConstants.Types.Subtypes.Incorporeal))
                armorClass.DeflectionBonus = Math.Max(1, abilities[AbilityConstants.Charisma].Modifier);

            armorClass.SizeModifier = adjustmentsSelector.SelectFrom<int>(TableNameConstants.Adjustments.SizeModifiers, size);
            armorClass.ArmorBonus = GetArmorBonus(feats);
            armorClass.NaturalArmorBonus = naturalArmor;

            return armorClass;
        }

        private int GetArmorBonus(IEnumerable<Feat> feats)
        {
            var thingsThatGrantArmorBonuses = collectionsSelector.SelectFrom(TableNameConstants.Collection.ArmorClassModifiers, GroupConstants.ArmorBonus);
            var featsWithArmorBonuses = feats.Where(f => thingsThatGrantArmorBonuses.Contains(f.Name) && !f.Foci.Any());
            var featArmorBonuses = featsWithArmorBonuses.Select(f => f.Power);
            var featArmorBonus = featArmorBonuses.Sum();

            return featArmorBonus;
        }
    }
}