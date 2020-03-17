using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Attacks;
using DnDGen.CreatureGen.Feats;
using DnDGen.CreatureGen.Items;
using DnDGen.CreatureGen.Tables;
using DnDGen.Infrastructure.Selectors.Collections;
using DnDGen.Infrastructure.Selectors.Percentiles;
using DnDGen.TreasureGen.Items;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Generators.Items
{
    internal class EquipmentGenerator : IEquipmentGenerator
    {
        private readonly ICollectionSelector collectionSelector;
        private readonly IItemsGenerator itemGenerator;
        private readonly IPercentileSelector percentileSelector;

        public EquipmentGenerator(ICollectionSelector collectionSelector, IItemsGenerator itemGenerator, IPercentileSelector percentileSelector)
        {
            this.collectionSelector = collectionSelector;
            this.itemGenerator = itemGenerator;
            this.percentileSelector = percentileSelector;
        }

        public IEnumerable<Attack> AddAttacks(IEnumerable<Feat> feats, IEnumerable<Attack> attacks, int numberOfHands)
        {
            var allAttacks = new List<Attack>(attacks);

            var unnaturalAttacks = allAttacks.Where(a => !a.IsNatural);

            if (!unnaturalAttacks.Any())
                return allAttacks;

            //Duplicate attacks
            var repeatedAttacks = unnaturalAttacks.Where(a => a.Frequency.Quantity > 1).ToArray();
            foreach (var attack in repeatedAttacks)
            {
                var individualAttacks = Enumerable.Range(1, attack.Frequency.Quantity).Select(i => Clone(attack));

                foreach (var individualAttack in individualAttacks)
                {
                    individualAttack.Frequency.Quantity = 1;
                }

                allAttacks.AddRange(individualAttacks);
                allAttacks.Remove(attack);
            }

            var equipmentMeleeAttacks = unnaturalAttacks.Where(a => a.Name == AttributeConstants.Melee);
            if (!equipmentMeleeAttacks.Any())
                return allAttacks;

            //Add additional melee attacks for two/multi-weapon fighting
            var superiorTwoWeaponFeat = feats.Any(f => f.Name == FeatConstants.SpecialQualities.TwoWeaponFighting_Superior);
            var greaterTwoWeaponFeat = superiorTwoWeaponFeat
                || feats.Any(f => f.Name == FeatConstants.TwoWeaponFighting_Greater
                    || f.Name == FeatConstants.Monster.MultiweaponFighting_Greater);
            var improvedTwoWeaponFeat = greaterTwoWeaponFeat
                || feats.Any(f => f.Name == FeatConstants.TwoWeaponFighting_Improved
                    || f.Name == FeatConstants.Monster.MultiweaponFighting_Improved);
            var twoWeaponFeat = improvedTwoWeaponFeat
                || feats.Any(f => f.Name == FeatConstants.TwoWeaponFighting
                    || f.Name == FeatConstants.Monster.MultiweaponFighting);
            var twoWeapon = twoWeaponFeat
                || equipmentMeleeAttacks.Count() > 1
                || percentileSelector.SelectFrom(.01);

            if (!twoWeapon)
                return allAttacks;

            var attackToClone = equipmentMeleeAttacks.Last();

            while (equipmentMeleeAttacks.Count() < numberOfHands)
            {
                var clone = Clone(attackToClone);
                clone.IsPrimary = false;

                allAttacks.Add(clone);
            }

            foreach (var attack in equipmentMeleeAttacks)
            {
                if (attack.IsPrimary)
                {
                    attack.MaxNumberOfAttacks = 4;
                    attack.AttackBonuses.Add(-6);

                    if (twoWeaponFeat)
                        attack.AttackBonuses.Add(2);

                    if (superiorTwoWeaponFeat)
                        attack.AttackBonuses.Add(4);
                }
                else
                {
                    attack.MaxNumberOfAttacks = 1;
                    attack.AttackBonuses.Add(-10);

                    if (twoWeaponFeat)
                        attack.AttackBonuses.Add(6);

                    if (improvedTwoWeaponFeat)
                        attack.MaxNumberOfAttacks++;

                    if (greaterTwoWeaponFeat)
                        attack.MaxNumberOfAttacks++;

                    if (superiorTwoWeaponFeat)
                        attack.AttackBonuses.Add(4);
                }
            }

            return allAttacks;
        }

        private Attack Clone(Attack attack)
        {
            var clone = new Attack();
            clone.AttackBonuses = new List<int>(attack.AttackBonuses);
            clone.AttackType = attack.AttackType;
            clone.BaseAbility = attack.BaseAbility;
            clone.BaseAttackBonus = attack.BaseAttackBonus;
            clone.DamageBonus = attack.DamageBonus;
            clone.DamageEffect = attack.DamageEffect;
            clone.DamageRoll = attack.DamageRoll;
            clone.Frequency = new Frequency();
            clone.Frequency.Quantity = attack.Frequency.Quantity;
            clone.Frequency.TimePeriod = attack.Frequency.TimePeriod;
            clone.IsMelee = attack.IsMelee;
            clone.IsNatural = attack.IsNatural;
            clone.IsPrimary = attack.IsPrimary;
            clone.IsSpecial = attack.IsSpecial;
            clone.MaxNumberOfAttacks = attack.MaxNumberOfAttacks;
            clone.Name = attack.Name;
            clone.SizeModifier = attack.SizeModifier;

            if (attack.Save != null)
            {
                clone.Save = new SaveDieCheck();
                clone.Save.BaseAbility = attack.Save.BaseAbility;
                clone.Save.BaseValue = attack.Save.BaseValue;
                clone.Save.Save = attack.Save.Save;
            }

            return clone;
        }

        public Equipment Generate(string creatureName, bool canUseEquipment, IEnumerable<Feat> feats, int level, IEnumerable<Attack> attacks, Dictionary<string, Ability> abilities)
        {
            var equipment = new Equipment();

            if (!canUseEquipment)
                return equipment;

            var unnaturalAttacks = attacks.Where(a => !a.IsNatural);

            var weaponProficiencyFeatNames = collectionSelector.SelectFrom(TableNameConstants.Collection.FeatGroups, GroupConstants.WeaponProficiency);
            var weaponProficiencyFeats = feats.Where(f => weaponProficiencyFeatNames.Contains(f.Name));

            var weapons = new List<Weapon>();

            if (weaponProficiencyFeats.Any() && unnaturalAttacks.Any())
            {
                //Generate melee weapons
                var weaponNames = WeaponConstants.GetAllWeapons(false, false);

                var equipmentMeleeAttacks = unnaturalAttacks.Where(a => a.Name == AttributeConstants.Melee);
                if (equipmentMeleeAttacks.Any())
                {
                    var meleeWeaponNames = WeaponConstants.GetAllMelee(false, false);

                    var nonProficiencyFeats = feats.Except(weaponProficiencyFeats);
                    var hasWeaponFinesse = feats.Any(f => f.Name == FeatConstants.WeaponFinesse);
                    var light = WeaponConstants.GetAllLightMelee(true, false);
                    var twoHandedAllowed = equipmentMeleeAttacks.Count() < 2;

                    var proficientMeleeWeaponNames = GetProficientWeaponNames(feats, weaponProficiencyFeats, meleeWeaponNames, twoHandedAllowed);
                    var nonProficientMeleeWeaponNames = weaponNames
                        .Except(proficientMeleeWeaponNames.Common)
                        .Except(proficientMeleeWeaponNames.Uncommon);

                    foreach (var attack in equipmentMeleeAttacks)
                    {
                        var weaponName = collectionSelector.SelectRandomFrom(proficientMeleeWeaponNames.Common, proficientMeleeWeaponNames.Uncommon, null, nonProficientMeleeWeaponNames);
                        var weapon = itemGenerator.GenerateAtLevel(level, ItemTypeConstants.Weapon, weaponName) as Weapon;

                        weapons.Add(weapon);

                        attack.Name = weapon.Name;
                        attack.DamageRoll = weapon.Damage;

                        if (nonProficientMeleeWeaponNames.Contains(weaponName))
                        {
                            attack.AttackBonuses.Add(-4);
                        }

                        if (weapon.Magic.Bonus > 0)
                        {
                            attack.AttackBonuses.Add(weapon.Magic.Bonus);
                        }

                        var bonusFeats = nonProficiencyFeats.Where(f => f.Foci.Any(weapon.NameMatches));
                        foreach (var feat in bonusFeats)
                        {
                            attack.AttackBonuses.Add(feat.Power);
                        }

                        var isLight = light.Any(weapon.NameMatches);
                        if (hasWeaponFinesse && isLight)
                        {
                            attack.BaseAbility = abilities[AbilityConstants.Dexterity];
                        }

                        if (twoHandedAllowed && isLight)
                        {
                            attack.AttackBonuses.Add(2);
                        }
                    }
                }

                //Generate ranged weapons
                var equipmentRangedAttacks = unnaturalAttacks.Where(a => a.Name == AttributeConstants.Ranged);
                if (equipmentRangedAttacks.Any())
                {
                    var rangedWeaponNames = WeaponConstants.GetAllRanged(false, false);
                    var twoHandedAllowed = equipmentRangedAttacks.Count() < 2;

                    var proficientRangedWeaponNames = GetProficientWeaponNames(feats, weaponProficiencyFeats, rangedWeaponNames, twoHandedAllowed);
                    var nonProficientRangedWeaponNames = weaponNames
                        .Except(proficientRangedWeaponNames.Common)
                        .Except(proficientRangedWeaponNames.Uncommon);

                    var nonProficiencyFeats = feats.Except(weaponProficiencyFeats);
                    var crossbows = new[]
                    {
                        WeaponConstants.HandCrossbow,
                        WeaponConstants.HeavyCrossbow,
                        WeaponConstants.LightCrossbow,
                    };

                    var rapidReload = feats.FirstOrDefault(f => f.Name == FeatConstants.RapidReload);

                    foreach (var attack in equipmentRangedAttacks)
                    {
                        var weaponName = collectionSelector.SelectRandomFrom(proficientRangedWeaponNames.Common, proficientRangedWeaponNames.Uncommon, null, nonProficientRangedWeaponNames);
                        var weapon = itemGenerator.GenerateAtLevel(level, ItemTypeConstants.Weapon, weaponName) as Weapon;

                        weapons.Add(weapon);

                        attack.Name = weapon.Name;
                        attack.DamageRoll = weapon.Damage;

                        if (nonProficientRangedWeaponNames.Contains(weaponName))
                        {
                            attack.AttackBonuses.Add(-4);
                        }

                        if (weapon.Magic.Bonus > 0)
                        {
                            attack.AttackBonuses.Add(weapon.Magic.Bonus);
                        }

                        var bonusFeats = nonProficiencyFeats.Where(f => f.Foci.Any(weapon.NameMatches));
                        foreach (var feat in bonusFeats)
                        {
                            attack.AttackBonuses.Add(feat.Power);
                        }

                        if (!attack.IsPrimary)
                        {
                            attack.MaxNumberOfAttacks = 1;
                            continue;
                        }

                        if (weapon.Attributes.Contains(AttributeConstants.Thrown)
                            || weapon.Attributes.Contains(AttributeConstants.Projectile))
                        {
                            attack.MaxNumberOfAttacks = 4;
                        }
                        else
                        {
                            attack.MaxNumberOfAttacks = 1;
                        }

                        if (crossbows.Any(c => weapon.NameMatches(c)))
                        {
                            attack.MaxNumberOfAttacks = 1;

                            if (rapidReload?.Foci?.Any(f => weapon.NameMatches(f)) == true
                                && (weapon.NameMatches(WeaponConstants.LightCrossbow)
                                    || weapon.NameMatches(WeaponConstants.HandCrossbow)))
                            {
                                attack.MaxNumberOfAttacks = 4;
                            }
                        }
                    }
                }

                equipment.Weapons = weapons;
            }

            var armorProficiencyFeatNames = collectionSelector.SelectFrom(TableNameConstants.Collection.FeatGroups, GroupConstants.ArmorProficiency);
            var armorProficiencyFeats = feats.Where(f => armorProficiencyFeatNames.Contains(f.Name));

            if (armorProficiencyFeats.Any())
            {
                var armorNames = ArmorConstants.GetAllArmors(false);
                var proficientArmorNames = GetProficientArmorNames(armorProficiencyFeats);
                var nonProficientArmorNames = armorNames.Except(proficientArmorNames);

                var armorName = collectionSelector.SelectRandomFrom(proficientArmorNames, null, null, nonProficientArmorNames);

                equipment.Armor = itemGenerator.GenerateAtLevel(level, ItemTypeConstants.Armor, armorName);

                var shieldNames = ArmorConstants.GetAllShields(false);
                var proficientShieldNames = GetProficientShieldNames(armorProficiencyFeats);

                var hasTwoHandedWeapon = weapons.Any(w => w.Attributes.Contains(AttributeConstants.Melee) && w.Attributes.Contains(AttributeConstants.TwoHanded));
                if (proficientShieldNames.Any() && !hasTwoHandedWeapon)
                {
                    var nonProficientShieldNames = shieldNames.Except(proficientShieldNames);

                    var shieldName = collectionSelector.SelectRandomFrom(proficientShieldNames, null, null, nonProficientShieldNames);

                    equipment.Shield = itemGenerator.GenerateAtLevel(level, ItemTypeConstants.Armor, shieldName);
                }
            }

            //TODO: items

            //TODO: apply attack bonuses from items

            return equipment;
        }

        private (IEnumerable<string> Common, IEnumerable<string> Uncommon) GetProficientWeaponNames(
            IEnumerable<Feat> feats,
            IEnumerable<Feat> proficiencyFeats,
            IEnumerable<string> baseWeapons,
            bool twoHandedAllowed)
        {
            var common = new List<string>();
            var uncommon = new List<string>();

            var weaponFoci = feats
                .Except(proficiencyFeats)
                .SelectMany(f => f.Foci)
                .Intersect(baseWeapons);

            if (weaponFoci.Any())
            {
                common.AddRange(weaponFoci);
            }

            weaponFoci = proficiencyFeats
                .SelectMany(f => f.Foci)
                .Intersect(baseWeapons);

            if (weaponFoci.Any())
            {
                uncommon.AddRange(weaponFoci);
            }

            if (proficiencyFeats.Any(f => f.Name == FeatConstants.WeaponProficiency_Simple && f.Foci.Contains(GroupConstants.All)))
            {
                var simpleWeapons = WeaponConstants.GetAllSimple(false, false);
                simpleWeapons = simpleWeapons.Intersect(baseWeapons);
                uncommon.AddRange(simpleWeapons);
            }

            if (proficiencyFeats.Any(f => f.Name == FeatConstants.WeaponProficiency_Martial && f.Foci.Contains(GroupConstants.All)))
            {
                var martialWeapons = WeaponConstants.GetAllMartial(false, false);
                martialWeapons = martialWeapons.Intersect(baseWeapons);
                uncommon.AddRange(martialWeapons);
            }

            if (proficiencyFeats.Any(f => f.Name == FeatConstants.WeaponProficiency_Exotic && f.Foci.Contains(GroupConstants.All)))
            {
                var exoticWeapons = WeaponConstants.GetAllExotic(false, false);
                exoticWeapons = exoticWeapons.Intersect(baseWeapons);
                uncommon.AddRange(exoticWeapons);
            }

            var ammunition = WeaponConstants.GetAllAmmunition(false, false)
                .Except(new[] { WeaponConstants.Shuriken });

            common = common.Except(ammunition).ToList();
            uncommon = uncommon.Except(ammunition).ToList();

            if (!twoHandedAllowed)
            {
                var twoHandedWeapons = WeaponConstants.GetAllTwoHandedMelee(false, false);
                common = common.Except(twoHandedWeapons).ToList();
                uncommon = uncommon.Except(twoHandedWeapons).ToList();
            }

            SwapForTemplates(common, WeaponConstants.CompositeShortbow,
                WeaponConstants.CompositePlus0Shortbow,
                WeaponConstants.CompositePlus1Shortbow,
                WeaponConstants.CompositePlus2Shortbow);

            SwapForTemplates(common, WeaponConstants.CompositeLongbow,
                WeaponConstants.CompositePlus0Longbow,
                WeaponConstants.CompositePlus1Longbow,
                WeaponConstants.CompositePlus2Longbow,
                WeaponConstants.CompositePlus3Longbow,
                WeaponConstants.CompositePlus4Longbow);

            SwapForTemplates(uncommon, WeaponConstants.CompositeShortbow,
                WeaponConstants.CompositePlus0Shortbow,
                WeaponConstants.CompositePlus1Shortbow,
                WeaponConstants.CompositePlus2Shortbow);

            SwapForTemplates(uncommon, WeaponConstants.CompositeLongbow,
                WeaponConstants.CompositePlus0Longbow,
                WeaponConstants.CompositePlus1Longbow,
                WeaponConstants.CompositePlus2Longbow,
                WeaponConstants.CompositePlus3Longbow,
                WeaponConstants.CompositePlus4Longbow);

            return (common, uncommon);
        }

        private List<string> SwapForTemplates(List<string> names, string source, params string[] templates)
        {
            if (!names.Contains(source))
                return names;

            names.Remove(source);
            names.AddRange(templates);

            return names;
        }

        private IEnumerable<string> GetProficientArmorNames(IEnumerable<Feat> feats)
        {
            var names = new List<string>();

            if (feats.Any(f => f.Name == FeatConstants.ArmorProficiency_Light))
            {
                var armor = ArmorConstants.GetAllLightArmors(false);
                names.AddRange(armor);
            }

            if (feats.Any(f => f.Name == FeatConstants.ArmorProficiency_Medium))
            {
                var armor = ArmorConstants.GetAllMediumArmors(false);
                names.AddRange(armor);
            }

            if (feats.Any(f => f.Name == FeatConstants.ArmorProficiency_Heavy))
            {
                var armor = ArmorConstants.GetAllHeavyArmors(false);
                names.AddRange(armor);
            }

            return names;
        }

        private IEnumerable<string> GetProficientShieldNames(IEnumerable<Feat> feats)
        {
            var names = new List<string>();

            if (feats.Any(f => f.Name == FeatConstants.ShieldProficiency))
            {
                var shields = ArmorConstants.GetAllShields(false);
                names.AddRange(shields);
            }

            if (!feats.Any(f => f.Name == FeatConstants.ShieldProficiency_Tower))
            {
                names.Remove(ArmorConstants.TowerShield);
            }

            return names;
        }
    }
}
