using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Attacks;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Defenses;
using DnDGen.CreatureGen.Feats;
using DnDGen.CreatureGen.Selectors.Collections;
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
        private readonly ICollectionTypeAndAmountSelector typeAndAmountSelector;
        private readonly IAttackSelector attackSelector;

        public AttacksGenerator(ICollectionSelector collectionSelector, ICollectionTypeAndAmountSelector typeAndAmountSelector, IAttackSelector attackSelector)
        {
            this.collectionSelector = collectionSelector;
            this.typeAndAmountSelector = typeAndAmountSelector;
            this.attackSelector = attackSelector;
        }

        public int GenerateBaseAttackBonus(CreatureType creatureType, HitPoints hitPoints)
        {
            if (hitPoints.RoundedHitDiceQuantity == 0)
                return 0;

            var baseAttackQuality = collectionSelector.FindCollectionOf(
                Config.Name,
                TableNameConstants.Collection.CreatureGroups,
                creatureType.Name,
                GroupConstants.GoodBaseAttack, GroupConstants.AverageBaseAttack, GroupConstants.PoorBaseAttack);

            return baseAttackQuality switch
            {
                GroupConstants.GoodBaseAttack => GetGoodBaseAttackBonus(hitPoints.RoundedHitDiceQuantity),
                GroupConstants.AverageBaseAttack => GetAverageBaseAttackBonus(hitPoints.RoundedHitDiceQuantity),
                GroupConstants.PoorBaseAttack => GetPoorBaseAttackBonus(hitPoints.RoundedHitDiceQuantity),
                _ => throw new ArgumentException($"{creatureType.Name} has no base attack"),
            };
        }

        private int GetGoodBaseAttackBonus(int hitDiceQuantity) => hitDiceQuantity;
        private int GetAverageBaseAttackBonus(int hitDiceQuantity) => hitDiceQuantity * 3 / 4;
        private int GetPoorBaseAttackBonus(int hitDiceQuantity) => hitDiceQuantity / 2;

        public int? GenerateGrappleBonus(string creature, string size, int baseAttackBonus, Ability strength)
        {
            if (!strength.HasScore)
                return null;

            var sizeModifier = typeAndAmountSelector.SelectOneFrom(Config.Name, TableNameConstants.Adjustments.GrappleBonuses, size);
            var creatureModifier = typeAndAmountSelector.SelectOneFrom(Config.Name, TableNameConstants.Adjustments.GrappleBonuses, creature);
            return baseAttackBonus + strength.Modifier + sizeModifier.Amount + creatureModifier.Amount;
        }

        public IEnumerable<Attack> GenerateAttacks(
            string creatureName,
            string size,
            int baseAttackBonus,
            Dictionary<string, Ability> abilities,
            int hitDiceQuantity,
            string gender)
        {
            var attackSelections = attackSelector.Select(creatureName, size);
            var sizeModifier = typeAndAmountSelector.SelectOneFrom(Config.Name, TableNameConstants.Adjustments.SizeModifiers, size);
            var attacks = attackSelections
                .Where(s => s.RequirementsMet(gender))
                .Select(s => Attack.From(s, abilities, hitDiceQuantity, baseAttackBonus, sizeModifier.Amount));

            return attacks;
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
