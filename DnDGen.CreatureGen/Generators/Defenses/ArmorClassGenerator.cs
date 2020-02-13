using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Defenses;
using DnDGen.CreatureGen.Feats;
using DnDGen.CreatureGen.Selectors.Collections;
using DnDGen.CreatureGen.Tables;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Generators.Defenses
{
    internal class ArmorClassGenerator : IArmorClassGenerator
    {
        private readonly IBonusSelector bonusSelector;
        private readonly IAdjustmentsSelector adjustmentsSelector;

        public ArmorClassGenerator(IBonusSelector bonusSelector, IAdjustmentsSelector adjustmentsSelector)
        {
            this.bonusSelector = bonusSelector;
            this.adjustmentsSelector = adjustmentsSelector;
        }

        public ArmorClass GenerateWith(Dictionary<string, Ability> abilities, string size, string creatureName, CreatureType creatureType, IEnumerable<Feat> feats, int naturalArmor)
        {
            var armorClass = new ArmorClass();
            armorClass.Dexterity = abilities[AbilityConstants.Dexterity];

            if (creatureType.SubTypes.Contains(CreatureConstants.Types.Subtypes.Incorporeal))
            {
                var deflectionBonus = Math.Max(1, abilities[AbilityConstants.Charisma].Modifier);
                armorClass.AddBonus(ArmorClassConstants.Deflection, deflectionBonus);
            }

            armorClass.SizeModifier = adjustmentsSelector.SelectFrom<int>(TableNameConstants.Adjustments.SizeModifiers, size);

            var inertialArmorFeat = feats.FirstOrDefault(f => f.Name == FeatConstants.SpecialQualities.InertialArmor);
            if (inertialArmorFeat != null)
            {
                armorClass.AddBonus(ArmorClassConstants.Armor, inertialArmorFeat.Power);
            }

            if (feats.Any(f => f.Name == FeatConstants.SpecialQualities.UnearthlyGrace))
            {
                armorClass.AddBonus(ArmorClassConstants.Deflection, abilities[AbilityConstants.Charisma].Modifier);
            }

            if (naturalArmor > 0)
            {
                armorClass.AddBonus(ArmorClassConstants.Natural, naturalArmor);
            }

            armorClass = GetRacialArmorClassBonuses(armorClass, creatureName, creatureType);

            return armorClass;
        }

        private ArmorClass GetRacialArmorClassBonuses(ArmorClass armorClass, string creatureName, CreatureType creatureType)
        {
            var creatureBonuses = bonusSelector.SelectFor(TableNameConstants.TypeAndAmount.ArmorClassBonuses, creatureName);
            var creatureTypeBonuses = bonusSelector.SelectFor(TableNameConstants.TypeAndAmount.ArmorClassBonuses, creatureType.Name);

            var bonuses = creatureBonuses.Union(creatureTypeBonuses);

            foreach (var subtype in creatureType.SubTypes)
            {
                var subtypeBonuses = bonusSelector.SelectFor(TableNameConstants.TypeAndAmount.ArmorClassBonuses, subtype);
                bonuses = bonuses.Union(subtypeBonuses);
            }

            foreach (var bonus in bonuses)
            {
                armorClass.AddBonus(bonus.Target, bonus.Bonus, bonus.Condition);
            }

            return armorClass;
        }
    }
}