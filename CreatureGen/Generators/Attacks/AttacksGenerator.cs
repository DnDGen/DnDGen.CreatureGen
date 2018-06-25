using CreatureGen.Abilities;
using CreatureGen.Attacks;
using CreatureGen.Creatures;
using CreatureGen.Defenses;
using CreatureGen.Feats;
using CreatureGen.Selectors.Collections;
using CreatureGen.Tables;
using DnDGen.Core.Selectors.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CreatureGen.Generators.Attacks
{
    internal class AttacksGenerator : IAttacksGenerator
    {
        private readonly ICollectionSelector collectionSelector;
        private readonly IAdjustmentsSelector adjustmentsSelector;
        private readonly IAttackSelector attackSelector;

        internal AttacksGenerator(ICollectionSelector collectionSelector, IAdjustmentsSelector adjustmentsSelector, IAttackSelector attackSelector)
        {
            this.collectionSelector = collectionSelector;
            this.adjustmentsSelector = adjustmentsSelector;
            this.attackSelector = attackSelector;
        }

        public int GenerateBaseAttackBonus(CreatureType creatureType, HitPoints hitPoints)
        {
            if (hitPoints.HitDiceQuantity == 0)
                return 0;

            var baseAttackQuality = collectionSelector.FindCollectionOf(TableNameConstants.Collection.CreatureGroups, creatureType.Name, GroupConstants.GoodBaseAttack, GroupConstants.AverageBaseAttack, GroupConstants.PoorBaseAttack);

            switch (baseAttackQuality)
            {
                case GroupConstants.GoodBaseAttack: return GetGoodBaseAttackBonus(hitPoints.RoundedHitDiceQuantity);
                case GroupConstants.AverageBaseAttack: return GetAverageBaseAttackBonus(hitPoints.RoundedHitDiceQuantity);
                case GroupConstants.PoorBaseAttack: return GetPoorBaseAttackBonus(hitPoints.RoundedHitDiceQuantity);
                default: throw new ArgumentException($"{creatureType.Name} has no base attack");
            }
        }

        private int GetGoodBaseAttackBonus(int hitDiceQuantity)
        {
            return hitDiceQuantity;
        }

        private int GetAverageBaseAttackBonus(int hitDiceQuantity)
        {
            return hitDiceQuantity * 3 / 4;
        }

        private int GetPoorBaseAttackBonus(int hitDiceQuantity)
        {
            return hitDiceQuantity / 2;
        }

        public int? GenerateGrappleBonus(string size, int baseAttackBonus, Ability strength)
        {
            if (!strength.HasScore)
                return null;

            var sizeModifier = adjustmentsSelector.SelectFrom<int>(TableNameConstants.Adjustments.GrappleBonuses, size);
            return baseAttackBonus + strength.Modifier + sizeModifier;
        }

        public IEnumerable<Attack> GenerateAttacks(string creatureName, string originalSize, string size, int baseAttackBonus, Dictionary<string, Ability> abilities)
        {
            var attacks = attackSelector.Select(creatureName, size);
            var sizeModifier = adjustmentsSelector.SelectFrom<int>(TableNameConstants.Adjustments.AttackBonuses, size);

            foreach (var attack in attacks)
            {
                if (attack.IsSpecial)
                    continue;

                attack.BaseAttackBonus = baseAttackBonus;
                attack.BaseAbility = GetAbilityForAttack(abilities, attack);
                attack.SizeModifierForAttackBonus = sizeModifier;

                if (attack.IsNatural)
                    attack.Damage = AdjustNaturalAttackDamage(originalSize, size, attack.Damage);
            }

            return attacks;
        }

        private string AdjustNaturalAttackDamage(string originalSize, string advancedSize, string originalDamage)
        {
            if (originalSize == advancedSize)
                return originalDamage;

            var sizes = SizeConstants.GetOrdered();
            var originalIndex = Array.IndexOf(sizes, originalSize);
            var advancedIndex = Array.IndexOf(sizes, advancedSize);
            var indexDifference = advancedIndex - originalIndex;
            var adjustedDamage = originalDamage;

            while (indexDifference-- > 0)
            {
                switch (adjustedDamage)
                {
                    case "1d2": adjustedDamage = "1d3"; break;
                    case "1d3": adjustedDamage = "1d4"; break;
                    case "1d4": adjustedDamage = "1d6"; break;
                    case "1d6": adjustedDamage = "1d8"; break;
                    case "1d8": adjustedDamage = "2d6"; break;
                    case "1d10": adjustedDamage = "2d8"; break;
                    case "2d6": adjustedDamage = "3d6"; break;
                    case "2d8": adjustedDamage = "3d8"; break;
                    default: throw new ArgumentException($"Damage of {originalDamage} cannot be advanced to size {advancedSize} from {originalSize}");
                }
            }

            return adjustedDamage;
        }

        private Ability GetAbilityForAttack(Dictionary<string, Ability> abilities, Attack attack)
        {
            if (!attack.IsMelee || !abilities[AbilityConstants.Strength].HasScore)
                return abilities[AbilityConstants.Dexterity];

            return abilities[AbilityConstants.Strength];
        }

        public IEnumerable<Attack> ApplyAttackBonuses(IEnumerable<Attack> attacks, IEnumerable<Feat> feats)
        {
            foreach (var attack in attacks)
            {
                if (attack.IsSpecial)
                    continue;

                if (!attack.IsPrimary && attack.IsNatural && feats.Any(f => f.Name == FeatConstants.Monster.Multiattack))
                    attack.SecondaryAttackModifiers -= 2;
                else if (!attack.IsPrimary)
                    attack.SecondaryAttackModifiers -= 5;
            }

            return attacks;
        }
    }
}
