using DnDGen.CreatureGen.Attacks;
using DnDGen.CreatureGen.Feats;
using DnDGen.CreatureGen.Items;
using DnDGen.CreatureGen.Tables;
using DnDGen.Infrastructure.Selectors.Collections;
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

        public EquipmentGenerator(ICollectionSelector collectionSelector)
        {
            this.collectionSelector = collectionSelector;
        }

        public Equipment Generate(string creatureName, bool canUseEquipment, IEnumerable<Feat> feats, int level, IEnumerable<Attack> attacks)
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
                var weaponNames = WeaponConstants.GetAllWeapons(false);

                var equipmentMeleeAttacks = unnaturalAttacks.Where(a => a.Name == AttributeConstants.Melee);
                if (equipmentMeleeAttacks.Any())
                {
                    var meleeWeaponNames = WeaponConstants.GetAllMelee(false);

                    var proficientMeleeWeaponNames = GetProficientWeaponNames(feats, weaponProficiencyFeats, meleeWeaponNames);
                    var nonProficientMeleeWeaponNames = weaponNames
                        .Except(proficientMeleeWeaponNames.Common)
                        .Except(proficientMeleeWeaponNames.Uncommon);

                    var weighted = collectionSelector.CreateWeighted(proficientMeleeWeaponNames.Common, proficientMeleeWeaponNames.Uncommon, null, nonProficientMeleeWeaponNames);

                    foreach (var attack in equipmentMeleeAttacks)
                    {
                        var weaponName = collectionSelector.SelectRandomFrom(weighted);
                        var weapon = itemGenerator.GenerateAtLevel(level, ItemTypeConstants.Weapon, weaponName) as Weapon;

                        weapons.Add(weapon);

                        attack.Name = weapon.Name;
                        attack.DamageRoll = weapon.Damage;
                    }
                }

                var equipmentRangedAttacks = unnaturalAttacks.Where(a => a.Name == AttributeConstants.Ranged);
                if (equipmentRangedAttacks.Any())
                {
                    var rangedWeaponNames = WeaponConstants.GetAllRanged(false);

                    var proficientRangedWeaponNames = GetProficientWeaponNames(feats, weaponProficiencyFeats, rangedWeaponNames);
                    var nonProficientRangedWeaponNames = weaponNames
                        .Except(proficientRangedWeaponNames.Common)
                        .Except(proficientRangedWeaponNames.Uncommon);

                    var weighted = collectionSelector.CreateWeighted(proficientRangedWeaponNames.Common, proficientRangedWeaponNames.Uncommon, null, nonProficientRangedWeaponNames);

                    foreach (var attack in equipmentRangedAttacks)
                    {
                        var weaponName = collectionSelector.SelectRandomFrom(weighted);
                        var weapon = itemGenerator.GenerateAtLevel(level, ItemTypeConstants.Weapon, weaponName) as Weapon;

                        weapons.Add(weapon);

                        attack.Name = weapon.Name;
                        attack.DamageRoll = weapon.Damage;
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

            // check if creature has specific items (such as hag heartstone, flaming whip for balor, etc.)

            return equipment;
        }

        private (IEnumerable<string> Common, IEnumerable<string> Uncommon) GetProficientWeaponNames(
            IEnumerable<Feat> feats,
            IEnumerable<Feat> proficiencyFeats,
            IEnumerable<string> baseWeapons)
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
                var simpleWeapons = WeaponConstants.GetAllSimple(false);
                simpleWeapons = simpleWeapons.Intersect(baseWeapons);
                uncommon.AddRange(simpleWeapons);
            }

            if (proficiencyFeats.Any(f => f.Name == FeatConstants.WeaponProficiency_Martial && f.Foci.Contains(GroupConstants.All)))
            {
                var martialWeapons = WeaponConstants.GetAllMartial(false);
                martialWeapons = martialWeapons.Intersect(baseWeapons);
                uncommon.AddRange(martialWeapons);
            }

            if (proficiencyFeats.Any(f => f.Name == FeatConstants.WeaponProficiency_Exotic && f.Foci.Contains(GroupConstants.All)))
            {
                var exoticWeapons = WeaponConstants.GetAllExotic(false);
                exoticWeapons = exoticWeapons.Intersect(baseWeapons);
                uncommon.AddRange(exoticWeapons);
            }

            return (common, uncommon);
        }

        private IEnumerable<string> GetProficientArmorNames(IEnumerable<Feat> feats)
        {
            var names = new List<string>();

            if (feats.Any(f => f.Name == FeatConstants.ArmorProficiency_Light))
            {
                var armor = ArmorConstants.GetAllLight(false);
                names.AddRange(armor);
            }

            if (feats.Any(f => f.Name == FeatConstants.ArmorProficiency_Medium))
            {
                var armor = ArmorConstants.GetAllMedium(false);
                names.AddRange(armor);
            }

            if (feats.Any(f => f.Name == FeatConstants.ArmorProficiency_Heavy))
            {
                var armor = ArmorConstants.GetAllHeavy(false);
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
