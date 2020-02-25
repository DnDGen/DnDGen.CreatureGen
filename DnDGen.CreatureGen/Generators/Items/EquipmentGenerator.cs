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

        public IEnumerable<Attack> AddAttacks()
        {
            throw new NotImplementedException();
        }

        public Equipment Generate(string creatureName, bool canUseEquipment, IEnumerable<Feat> feats, int level, IEnumerable<Attack> attacks, Dictionary<string, Ability> abilities, int numberOfHands)
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
                    var greaterTwoWeaponFeat = feats.Any(f => f.Name == FeatConstants.TwoWeaponFighting_Greater
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

                    if (twoWeapon)
                    {
                        while (equipmentMeleeAttacks.Count() < numberOfHands)
                        {
                            //add other melee attacks (number equal to number of hands)
                            //... how to add attacks so that they get back to the creature...
                        }

                        foreach (var attack in equipmentMeleeAttacks)
                        {
                            if (attack.IsPrimary)
                            {
                                attack.MaxNumberOfAttacks = 4;
                                attack.AttackBonuses.Add(-6);

                                if (twoWeaponFeat)
                                    attack.AttackBonuses.Add(2);
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
                            }
                        }
                    }

                    var proficientMeleeWeaponNames = GetProficientWeaponNames(feats, weaponProficiencyFeats, meleeWeaponNames, twoWeapon);
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

                        if (twoWeapon && isLight)
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

                    var proficientRangedWeaponNames = GetProficientWeaponNames(feats, weaponProficiencyFeats, rangedWeaponNames, false);
                    var nonProficientRangedWeaponNames = weaponNames
                        .Except(proficientRangedWeaponNames.Common)
                        .Except(proficientRangedWeaponNames.Uncommon);

                    var nonProficiencyFeats = feats.Except(weaponProficiencyFeats);

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
            bool twoWeapons)
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

            var twoHandedWeapons = WeaponConstants.GetAllTwoHandedMelee(false, false);
            if (twoWeapons)
            {
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
