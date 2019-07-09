using CreatureGen.Abilities;
using CreatureGen.Attacks;
using CreatureGen.Creatures;
using CreatureGen.Defenses;
using CreatureGen.Feats;
using CreatureGen.Selectors.Collections;
using CreatureGen.Selectors.Selections;
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
            var attackSelections = attackSelector.Select(creatureName, originalSize, size);
            var sizeModifier = adjustmentsSelector.SelectFrom<int>(TableNameConstants.Adjustments.SizeModifiers, size);
            var attacks = new List<Attack>();

            foreach (var attackSelection in attackSelections)
            {
                var attack = new Attack();
                attacks.Add(attack);

                attack.Damage = GetAttackDamage(attackSelection, abilities);
                attack.Name = attackSelection.Name;
                attack.IsMelee = attackSelection.IsMelee;
                attack.IsNatural = attackSelection.IsNatural;
                attack.IsPrimary = attackSelection.IsPrimary;
                attack.IsSpecial = attackSelection.IsSpecial;

                if (attack.IsSpecial)
                    continue;

                attack.BaseAttackBonus = baseAttackBonus;
                attack.BaseAbility = GetAbilityForAttack(abilities, attackSelection);
                attack.SizeModifierForAttackBonus = sizeModifier;
            }

            return attacks;
        }

        private string GetAttackDamage(AttackSelection selection, Dictionary<string, Ability> abilities, IEnumerable<AttackSelection> otherAttacks)
        {
            var damage = selection.Damage;

            foreach (var kvp in abilities)
            {
                var target = kvp.Key.ToUpper();
                var bonus = kvp.Value.Modifier;

                if (!selection.IsPrimary)
                {
                    bonus /= 2;
                }
                else if (IsSolePrimary(selection, otherAttacks))
                {
                    bonus *= 3;
                    bonus /= 2;
                }

                if (damage.Contains(target))
                {
                    damage.Replace(target, bonus.ToString());
                }
            }

            return damage;
        }

        private bool IsSolePrimary(AttackSelection selection, IEnumerable<AttackSelection> otherAttacks)
        {
            var soleCount = otherAttacks.Count(a => !a.IsSpecial
                && a.IsMelee == selection.IsMelee
                && a.IsNatural == selection.IsNatural);
            return soleCount < 2;
        }

        private Ability GetAbilityForAttack(Dictionary<string, Ability> abilities, AttackSelection attackSelection)
        {
            if (!attackSelection.IsMelee || !abilities[AbilityConstants.Strength].HasScore)
                return abilities[AbilityConstants.Dexterity];

            return abilities[AbilityConstants.Strength];
        }

        public IEnumerable<Attack> ApplyAttackBonuses(IEnumerable<Attack> attacks, IEnumerable<Feat> feats, Dictionary<string, Ability> abilities)
        {
            var hasMultiattack = feats.Any(f => f.Name == FeatConstants.Monster.Multiattack);
            var hasWeaponFinesse = feats.Any(f => f.Name == FeatConstants.WeaponFinesse);

            foreach (var attack in attacks)
            {
                if (attack.IsSpecial)
                    continue;

                if (!attack.IsPrimary && attack.IsNatural && hasMultiattack)
                    attack.SecondaryAttackModifiers -= 2;
                else if (!attack.IsPrimary)
                    attack.SecondaryAttackModifiers -= 5;

                if (hasWeaponFinesse && attack.IsMelee)
                {
                    attack.BaseAbility = abilities[AbilityConstants.Dexterity];
                }
            }

            return attacks;
        }
    }
}
