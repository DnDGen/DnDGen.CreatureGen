using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Attacks;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Defenses;
using DnDGen.CreatureGen.Feats;
using DnDGen.CreatureGen.Selectors.Collections;
using DnDGen.CreatureGen.Selectors.Selections;
using DnDGen.CreatureGen.Tables;
using DnDGen.Infrastructure.Selectors.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Generators.Attacks
{
    internal class AttacksGenerator : IAttacksGenerator
    {
        private readonly ICollectionSelector collectionSelector;
        private readonly IAdjustmentsSelector adjustmentsSelector;
        private readonly IAttackSelector attackSelector;

        public AttacksGenerator(ICollectionSelector collectionSelector, IAdjustmentsSelector adjustmentsSelector, IAttackSelector attackSelector)
        {
            this.collectionSelector = collectionSelector;
            this.adjustmentsSelector = adjustmentsSelector;
            this.attackSelector = attackSelector;
        }

        public int GenerateBaseAttackBonus(CreatureType creatureType, HitPoints hitPoints)
        {
            if (hitPoints.RoundedHitDiceQuantity == 0)
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

        public int? GenerateGrappleBonus(string creature, string size, int baseAttackBonus, Ability strength)
        {
            if (!strength.HasScore)
                return null;

            var sizeModifier = adjustmentsSelector.SelectFrom<int>(TableNameConstants.Adjustments.GrappleBonuses, size);
            var creatureModifier = adjustmentsSelector.SelectFrom<int>(TableNameConstants.Adjustments.GrappleBonuses, creature);
            return baseAttackBonus + strength.Modifier + sizeModifier + creatureModifier;
        }

        public IEnumerable<Attack> GenerateAttacks(string creatureName, string originalSize, string size, int baseAttackBonus, Dictionary<string, Ability> abilities, int hitDiceQuantity)
        {
            var attackSelections = attackSelector.Select(creatureName, originalSize, size);
            var sizeModifier = adjustmentsSelector.SelectFrom<int>(TableNameConstants.Adjustments.SizeModifiers, size);
            var attacks = new List<Attack>();

            foreach (var attackSelection in attackSelections)
            {
                var attack = new Attack();
                attacks.Add(attack);

                attack.DamageRoll = attackSelection.DamageRoll;
                attack.DamageEffect = attackSelection.DamageEffect;
                attack.DamageBonus = GetDamageBonus(abilities, attackSelection.DamageBonusMultiplier);
                attack.Name = attackSelection.Name;
                attack.IsMelee = attackSelection.IsMelee;
                attack.IsNatural = attackSelection.IsNatural;
                attack.IsPrimary = attackSelection.IsPrimary;
                attack.IsSpecial = attackSelection.IsSpecial;
                attack.AttackType = attackSelection.AttackType;

                attack.Frequency = new Frequency();
                attack.Frequency.Quantity = attackSelection.FrequencyQuantity;
                attack.Frequency.TimePeriod = attackSelection.FrequencyTimePeriod;

                if (!string.IsNullOrEmpty(attackSelection.SaveAbility))
                {
                    attack.Save = new SaveDieCheck();
                    attack.Save.BaseAbility = abilities[attackSelection.SaveAbility];
                    attack.Save.BaseValue = 10 + hitDiceQuantity / 2 + attackSelection.SaveDcBonus;
                    attack.Save.Save = attackSelection.Save;
                }

                attack.BaseAttackBonus = baseAttackBonus;
                attack.SizeModifier = sizeModifier;
                attack.BaseAbility = GetAbilityForAttack(abilities, attackSelection);
            }

            return attacks;
        }

        private int GetDamageBonus(Dictionary<string, Ability> abilities, double multiplier)
        {
            var modifier = abilities[AbilityConstants.Strength].Modifier;
            if (modifier < 0)
                return modifier;

            return Convert.ToInt32(Math.Floor(abilities[AbilityConstants.Strength].Modifier * multiplier));
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
            var rockThrowing = feats.FirstOrDefault(f => f.Name == FeatConstants.SpecialQualities.RockThrowing);

            foreach (var attack in attacks)
            {
                if (attack.IsSpecial)
                    continue;

                if (!attack.IsPrimary)
                {
                    attack.AttackBonuses.Add(-5);
                }

                if (!attack.IsPrimary && attack.IsNatural && hasMultiattack)
                {
                    attack.AttackBonuses.Add(3);
                }

                if (hasWeaponFinesse && attack.IsNatural && attack.IsMelee)
                {
                    attack.BaseAbility = abilities[AbilityConstants.Dexterity];
                }

                if (rockThrowing != null && attack.Name == "Rock")
                {
                    attack.AttackBonuses.Add(rockThrowing.Power);
                }
            }

            return attacks;
        }
    }
}
