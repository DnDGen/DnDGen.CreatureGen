using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Feats;
using DnDGen.CreatureGen.Selectors.Selections;
using DnDGen.TreasureGen.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace DnDGen.CreatureGen.Attacks
{
    public class Attack
    {
        public string Name { get; set; }
        public int BaseAttackBonus { get; set; }
        public int SizeModifier { get; set; }
        public Ability BaseAbility { get; set; }
        public bool IsMelee { get; set; }
        public List<Damage> Damages { get; set; }
        public string DamageEffect { get; set; }
        public int DamageBonus { get; set; }
        public bool IsPrimary { get; set; }
        public bool IsSpecial { get; set; }
        public bool IsNatural { get; set; }
        public Frequency Frequency { get; set; }
        public SaveDieCheck Save { get; set; }
        public string AttackType { get; set; }
        public List<int> AttackBonuses { get; set; }
        public int MaxNumberOfAttacks { get; set; }

        public int TotalAttackBonus
        {
            get
            {
                var abilityBonus = 0;
                if (BaseAbility != null)
                    abilityBonus = BaseAbility.Modifier;

                return BaseAttackBonus + abilityBonus + SizeModifier + AttackBonuses.Sum();
            }
        }

        public int[] FullAttackBonuses
        {
            get
            {
                var bonuses = new List<int>();
                var decrement = 0;

                do
                {
                    bonuses.Add(TotalAttackBonus - decrement);
                    decrement += 5;
                }
                while (BaseAttackBonus - decrement > 0 && bonuses.Count < MaxNumberOfAttacks);

                return [.. bonuses];
            }
        }

        public string DamageSummary
        {
            get
            {
                if (!Damages.Any())
                    return DamageEffect;

                var damage = string.Join(" + ", Damages.Select(d => d.Summary));
                if (DamageBonus > 0)
                {
                    var regex = new Regex(Regex.Escape(Damages[0].Roll));
                    damage = regex.Replace(damage, $"{Damages[0].Roll}+{DamageBonus}", 1);
                }
                else if (DamageBonus < 0)
                {
                    var regex = new Regex(Regex.Escape(Damages[0].Roll));
                    damage = regex.Replace(damage, $"{Damages[0].Roll}{DamageBonus}", 1);
                }

                if (!string.IsNullOrEmpty(DamageEffect))
                {
                    damage += $" plus {DamageEffect}";
                }

                return damage;
            }
        }

        public Attack()
        {
            Name = string.Empty;
            Damages = [];
            DamageEffect = string.Empty;
            AttackBonuses = [];
            MaxNumberOfAttacks = 1;
            Frequency = new Frequency();
            AttackType = string.Empty;
        }

        internal static Attack From(AttackDataSelection selection, Dictionary<string, Ability> abilities, int hitDiceQuantity, int baseAttackBonus, int sizeModifier)
        {
            var attack = new Attack
            {
                Damages = [.. selection.Damages.Select(d => d.To())],
                DamageEffect = selection.DamageEffect,
                DamageBonus = GetDamageBonus(abilities, selection.DamageBonusMultiplier),
                Name = selection.Name,
                IsMelee = selection.IsMelee,
                IsNatural = selection.IsNatural,
                IsPrimary = selection.IsPrimary,
                IsSpecial = selection.IsSpecial,
                AttackType = selection.AttackType,
                Frequency = new Frequency
                {
                    Quantity = selection.FrequencyQuantity,
                    TimePeriod = selection.FrequencyTimePeriod
                }
            };

            if (!string.IsNullOrEmpty(selection.SaveAbility) || !string.IsNullOrEmpty(selection.Save))
            {
                attack.Save = new SaveDieCheck
                {
                    BaseValue = 10 + selection.SaveDcBonus,
                    Save = selection.Save ?? string.Empty
                };

                if (attack.IsNatural && !string.IsNullOrEmpty(selection.SaveAbility))
                {
                    attack.Save.BaseAbility = abilities[selection.SaveAbility];
                    attack.Save.BaseValue += hitDiceQuantity / 2;
                }
            }

            attack.BaseAttackBonus = baseAttackBonus;
            attack.SizeModifier = sizeModifier;
            attack.BaseAbility = GetAbilityForAttack(abilities, selection);

            return attack;
        }

        private static int GetDamageBonus(Dictionary<string, Ability> abilities, double multiplier)
        {
            var modifier = abilities[AbilityConstants.Strength].Modifier;
            if (modifier < 0)
                return modifier;

            return Convert.ToInt32(Math.Floor(abilities[AbilityConstants.Strength].Modifier * multiplier));
        }

        private static Ability GetAbilityForAttack(Dictionary<string, Ability> abilities, AttackDataSelection attackSelection)
        {
            if (!attackSelection.IsMelee || !abilities[AbilityConstants.Strength].HasScore)
                return abilities[AbilityConstants.Dexterity];

            return abilities[AbilityConstants.Strength];
        }
    }
}
