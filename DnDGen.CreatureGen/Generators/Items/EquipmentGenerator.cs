using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Attacks;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Feats;
using DnDGen.CreatureGen.Items;
using DnDGen.CreatureGen.Selectors;
using DnDGen.CreatureGen.Tables;
using DnDGen.Infrastructure.Generators;
using DnDGen.Infrastructure.Selectors.Collections;
using DnDGen.Infrastructure.Selectors.Percentiles;
using DnDGen.TreasureGen.Items;
using DnDGen.TreasureGen.Items.Magical;
using DnDGen.TreasureGen.Items.Mundane;
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
        private readonly IItemSelector itemSelector;
        private readonly JustInTimeFactory justInTimeFactory;

        public EquipmentGenerator(ICollectionSelector collectionSelector,
            IItemsGenerator itemGenerator,
            IPercentileSelector percentileSelector,
            IItemSelector itemSelector,
            JustInTimeFactory justInTimeFactory)
        {
            this.collectionSelector = collectionSelector;
            this.itemGenerator = itemGenerator;
            this.percentileSelector = percentileSelector;
            this.itemSelector = itemSelector;
            this.justInTimeFactory = justInTimeFactory;
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
                var individualAttacks = Enumerable.Range(1, attack.Frequency.Quantity)
                    .Select(i => Clone(attack))
                    .ToArray();

                foreach (var individualAttack in individualAttacks)
                {
                    individualAttack.Frequency.Quantity = 1;
                }

                allAttacks.Remove(attack);
                allAttacks.AddRange(individualAttacks);
            }

            var equipmentAttacks = unnaturalAttacks.Where(a => a.Name == AttributeConstants.Melee || a.Name == AttributeConstants.Ranged);
            if (!equipmentAttacks.Any())
                return allAttacks;

            //Add additional melee attacks for two/multi-weapon fighting
            var superiorTwoWeaponFeat = feats.Any(f => f.Name == FeatConstants.SpecialQualities.TwoWeaponFighting_Superior
                || f.Name == FeatConstants.SpecialQualities.MultiweaponFighting_Superior);
            var greaterTwoWeaponFeat = feats.Any(f => f.Name == FeatConstants.TwoWeaponFighting_Greater
                || f.Name == FeatConstants.Monster.MultiweaponFighting_Greater);
            var improvedTwoWeaponFeat = feats.Any(f => f.Name == FeatConstants.TwoWeaponFighting_Improved
                || f.Name == FeatConstants.Monster.MultiweaponFighting_Improved);
            var twoWeaponFeat = feats.Any(f => f.Name == FeatConstants.TwoWeaponFighting
                || f.Name == FeatConstants.Monster.MultiweaponFighting);
            var twoWeapon = superiorTwoWeaponFeat
                || greaterTwoWeaponFeat
                || improvedTwoWeaponFeat
                || twoWeaponFeat
                || equipmentAttacks.Count() > 1
                || percentileSelector.SelectFrom(.01);

            if (!twoWeapon)
                return allAttacks;

            allAttacks = AddAttacksPerHand(AttributeConstants.Melee, numberOfHands, allAttacks);
            allAttacks = AddAttacksPerHand(AttributeConstants.Ranged, numberOfHands, allAttacks);

            foreach (var attack in equipmentAttacks)
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

        private List<Attack> AddAttacksPerHand(string name, int numberOfHands, List<Attack> attacks)
        {
            var namedAttacks = attacks.Where(a => a.Name == name);
            if (!namedAttacks.Any())
                return attacks;

            var attackToClone = namedAttacks.Last();

            while (namedAttacks.Count() < numberOfHands)
            {
                var clone = Clone(attackToClone);
                clone.IsPrimary = false;

                attacks.Add(clone);
            }

            return attacks;
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
            clone.Damages = attack.Damages;
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

        public Equipment Generate(string creatureName, bool canUseEquipment, IEnumerable<Feat> feats, int level, IEnumerable<Attack> attacks, Dictionary<string, Ability> abilities, string size)
        {
            var equipment = new Equipment();

            var weaponSize = GetWeaponSize(feats, size);

            //Get predetermined items
            var allPredeterminedItems = GetPredeterminedItems(creatureName, size, weaponSize);
            var predeterminedWeapons = allPredeterminedItems
                .Where(i => i is Weapon)
                .Select(i => i as Weapon)
                .ToList();
            var predeterminedArmors = allPredeterminedItems
                .Where(i => i is Armor)
                .Select(i => i as Armor)
                .ToList();
            equipment.Items = allPredeterminedItems.Except(predeterminedWeapons).Except(predeterminedArmors);

            if (predeterminedArmors.Any())
            {
                equipment.Shield = predeterminedArmors.FirstOrDefault(a => a.Attributes.Contains(AttributeConstants.Shield));
                equipment.Armor = predeterminedArmors.FirstOrDefault(a => !a.Attributes.Contains(AttributeConstants.Shield));
            }

            if (!canUseEquipment)
                return equipment;

            //Generate weapons and attacks
            var unnaturalAttacks = attacks.Where(a => !a.IsNatural);

            var weaponProficiencyFeatNames = collectionSelector.SelectFrom(TableNameConstants.Collection.FeatGroups, GroupConstants.WeaponProficiency);
            var weaponProficiencyFeats = feats.Where(f => weaponProficiencyFeatNames.Contains(f.Name));

            var weapons = new List<Weapon>();
            var hasMultipleEquippedMeleeAttacks = unnaturalAttacks.Count(a => a.Name == AttributeConstants.Melee) >= 2;

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

                    var proficientMeleeWeaponNames = GetProficientWeaponNames(feats, weaponProficiencyFeats, meleeWeaponNames, !hasMultipleEquippedMeleeAttacks);
                    var nonProficientMeleeWeaponNames = meleeWeaponNames
                        .Except(proficientMeleeWeaponNames.Common)
                        .Except(proficientMeleeWeaponNames.Uncommon);

                    if (hasMultipleEquippedMeleeAttacks)
                    {
                        var twoHandedWeapons = WeaponConstants.GetAllTwoHandedMelee(false, false);
                        nonProficientMeleeWeaponNames = nonProficientMeleeWeaponNames.Except(twoHandedWeapons);
                    }

                    var primaryMeleeAttacks = equipmentMeleeAttacks.Where(a => a.IsPrimary).ToArray();
                    var primaryLightBonusAdded = false;

                    foreach (var attack in equipmentMeleeAttacks)
                    {
                        Weapon weapon = null;

                        if (predeterminedWeapons.Any(i => i.Attributes.Contains(AttributeConstants.Melee)))
                        {
                            weapon = predeterminedWeapons.First(i => i.Attributes.Contains(AttributeConstants.Melee));

                            predeterminedWeapons.Remove(weapon);
                        }
                        else
                        {
                            var weaponName = collectionSelector.SelectRandomFrom(
                                proficientMeleeWeaponNames.Common,
                                proficientMeleeWeaponNames.Uncommon,
                                null,
                                nonProficientMeleeWeaponNames);
                            weapon = itemGenerator.GenerateAtLevel(level, ItemTypeConstants.Weapon, weaponName, weaponSize) as Weapon;
                        }

                        weapons.Add(weapon);

                        attack.Name = weapon.Name;
                        attack.Damages.Add(new Damage { Roll = weapon.Damage, Type = weapon.DamageType });

                        //Is not proficient with the weapon
                        if (!proficientMeleeWeaponNames.Common.Any(weapon.NameMatches)
                            && !proficientMeleeWeaponNames.Uncommon.Any(weapon.NameMatches))
                        {
                            attack.AttackBonuses.Add(-4);
                        }

                        if (weapon.Magic.Bonus != 0)
                        {
                            attack.AttackBonuses.Add(weapon.Magic.Bonus);
                        }
                        else if (weapon.Traits.Contains(TraitConstants.Masterwork))
                        {
                            attack.AttackBonuses.Add(1);
                        }

                        var bonusFeats = nonProficiencyFeats.Where(f => f.Foci.Any(weapon.NameMatches));
                        foreach (var feat in bonusFeats)
                        {
                            if (feat.Power != 0)
                                attack.AttackBonuses.Add(feat.Power);
                        }

                        var isLight = light.Any(weapon.NameMatches);
                        if (hasWeaponFinesse && isLight)
                        {
                            attack.BaseAbility = abilities[AbilityConstants.Dexterity];
                        }

                        if (hasMultipleEquippedMeleeAttacks && !attack.IsPrimary && isLight)
                        {
                            attack.AttackBonuses.Add(2);

                            if (!primaryLightBonusAdded)
                            {
                                foreach (var primaryAttack in primaryMeleeAttacks)
                                {
                                    primaryAttack.AttackBonuses.Add(2);
                                }
                            }

                            primaryLightBonusAdded = true;
                        }
                    }
                }

                //Generate ranged weapons
                var equipmentRangedAttacks = unnaturalAttacks.Where(a => a.Name == AttributeConstants.Ranged);
                if (equipmentRangedAttacks.Any())
                {
                    var rangedWeaponNames = GetRangedWithBowTemplates();

                    var proficientRangedWeaponNames = GetProficientWeaponNames(feats, weaponProficiencyFeats, rangedWeaponNames, !hasMultipleEquippedMeleeAttacks);
                    var nonProficientRangedWeaponNames = rangedWeaponNames
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
                        Weapon weapon = null;

                        if (predeterminedWeapons.Any(i =>
                            i.Attributes.Contains(AttributeConstants.Ranged)
                            && !i.Attributes.Contains(AttributeConstants.Melee)))
                        {
                            weapon = predeterminedWeapons.First(i =>
                                i.Attributes.Contains(AttributeConstants.Ranged)
                                && !i.Attributes.Contains(AttributeConstants.Melee));

                            predeterminedWeapons.Remove(weapon);
                        }
                        else
                        {
                            var weaponName = collectionSelector.SelectRandomFrom(
                                proficientRangedWeaponNames.Common,
                                proficientRangedWeaponNames.Uncommon,
                                null,
                                nonProficientRangedWeaponNames);
                            weapon = itemGenerator.GenerateAtLevel(level, ItemTypeConstants.Weapon, weaponName, weaponSize) as Weapon;
                        }

                        weapons.Add(weapon);

                        //Get ammunition
                        if (!string.IsNullOrEmpty(weapon.Ammunition))
                        {
                            Weapon ammo = null;

                            if (predeterminedWeapons.Any(i => i.NameMatches(weapon.Ammunition)))
                            {
                                ammo = predeterminedWeapons.First(i => i.NameMatches(weapon.Ammunition));

                                predeterminedWeapons.Remove(ammo);
                            }
                            else
                            {
                                ammo = itemGenerator.GenerateAtLevel(level, ItemTypeConstants.Weapon, weapon.Ammunition, weaponSize) as Weapon;
                            }

                            weapons.Add(ammo);
                        }

                        //Set up the attack
                        attack.Name = weapon.Name;
                        attack.Damages.Add(new Damage { Roll = weapon.Damage, Type = weapon.DamageType });

                        if (!proficientRangedWeaponNames.Common.Any(weapon.NameMatches)
                            && !proficientRangedWeaponNames.Uncommon.Any(weapon.NameMatches))
                        {
                            attack.AttackBonuses.Add(-4);
                        }

                        if (weapon.Magic.Bonus != 0)
                        {
                            attack.AttackBonuses.Add(weapon.Magic.Bonus);
                        }
                        else if (weapon.Traits.Contains(TraitConstants.Masterwork))
                        {
                            attack.AttackBonuses.Add(1);
                        }

                        var bonusFeats = nonProficiencyFeats.Where(f => f.Foci.Any(weapon.NameMatches));
                        foreach (var feat in bonusFeats)
                        {
                            if (feat.Power != 0)
                                attack.AttackBonuses.Add(feat.Power);
                        }

                        if (!weapon.Attributes.Contains(AttributeConstants.Thrown)
                            && !weapon.Attributes.Contains(AttributeConstants.Projectile))
                        {
                            attack.MaxNumberOfAttacks = 1;
                        }

                        if (crossbows.Any(weapon.NameMatches))
                        {
                            attack.MaxNumberOfAttacks = 1;

                            if (rapidReload?.Foci?.Any(weapon.NameMatches) == true
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

                if (proficientArmorNames.Any() && equipment.Armor == null)
                {
                    var nonProficientArmorNames = armorNames.Except(proficientArmorNames);
                    var armorName = collectionSelector.SelectRandomFrom(proficientArmorNames, null, null, nonProficientArmorNames);

                    equipment.Armor = itemGenerator.GenerateAtLevel(level, ItemTypeConstants.Armor, armorName, size) as Armor;
                }

                var shieldNames = ArmorConstants.GetAllShields(false);
                var proficientShieldNames = GetProficientShieldNames(armorProficiencyFeats);
                var hasTwoHandedWeapon = weapons.Any(w => w.Attributes.Contains(AttributeConstants.Melee) && w.Attributes.Contains(AttributeConstants.TwoHanded));

                if (proficientShieldNames.Any()
                    && !hasTwoHandedWeapon
                    && !hasMultipleEquippedMeleeAttacks
                    && equipment.Shield == null)
                {
                    var nonProficientShieldNames = shieldNames.Except(proficientShieldNames);
                    var shieldName = collectionSelector.SelectRandomFrom(proficientShieldNames, null, null, nonProficientShieldNames);

                    equipment.Shield = itemGenerator.GenerateAtLevel(level, ItemTypeConstants.Armor, shieldName, size) as Armor;
                }
            }

            return equipment;
        }

        private string GetWeaponSize(IEnumerable<Feat> feats, string size)
        {
            var oversized = feats.FirstOrDefault(f => f.Name == FeatConstants.SpecialQualities.OversizedWeapon);
            if (oversized == null)
            {
                return size;
            }

            return oversized.Foci.First();
        }

        private List<string> GetRangedWithBowTemplates()
        {
            var ranged = WeaponConstants.GetAllRanged(false, true, false).ToList();
            var ammo = WeaponConstants.GetAllAmmunition(false, false);

            ranged.Remove(WeaponConstants.CompositeShortbow);
            ranged.Remove(WeaponConstants.CompositeLongbow);
            ranged = ranged.Except(ammo).ToList();

            if (!ranged.Contains(WeaponConstants.Shuriken))
                ranged.Add(WeaponConstants.Shuriken);

            return ranged;
        }

        private (IEnumerable<string> Common, IEnumerable<string> Uncommon) GetProficientWeaponNames(
            IEnumerable<Feat> feats,
            IEnumerable<Feat> proficiencyFeats,
            IEnumerable<string> baseWeapons,
            bool twoHandedAllowed)
        {
            var common = new List<string>();
            var uncommon = new List<string>();

            var nonProficiencyFoci = feats
                .Except(proficiencyFeats)
                .SelectMany(f => f.Foci);

            nonProficiencyFoci = SwapForTemplates(nonProficiencyFoci, WeaponConstants.CompositeShortbow,
                WeaponConstants.CompositeShortbow_StrengthPlus0,
                WeaponConstants.CompositeShortbow_StrengthPlus1,
                WeaponConstants.CompositeShortbow_StrengthPlus2);

            nonProficiencyFoci = SwapForTemplates(nonProficiencyFoci, WeaponConstants.CompositeLongbow,
                WeaponConstants.CompositeLongbow_StrengthPlus0,
                WeaponConstants.CompositeLongbow_StrengthPlus1,
                WeaponConstants.CompositeLongbow_StrengthPlus2,
                WeaponConstants.CompositeLongbow_StrengthPlus3,
                WeaponConstants.CompositeLongbow_StrengthPlus4);

            var nonProficiencyWeaponFoci = nonProficiencyFoci.Intersect(baseWeapons);
            if (nonProficiencyWeaponFoci.Any())
            {
                common.AddRange(nonProficiencyWeaponFoci);
            }

            var proficiencyFoci = proficiencyFeats.SelectMany(f => f.Foci);

            proficiencyFoci = SwapForTemplates(proficiencyFoci, WeaponConstants.CompositeShortbow,
                WeaponConstants.CompositeShortbow_StrengthPlus0,
                WeaponConstants.CompositeShortbow_StrengthPlus1,
                WeaponConstants.CompositeShortbow_StrengthPlus2);

            proficiencyFoci = SwapForTemplates(proficiencyFoci, WeaponConstants.CompositeLongbow,
                WeaponConstants.CompositeLongbow_StrengthPlus0,
                WeaponConstants.CompositeLongbow_StrengthPlus1,
                WeaponConstants.CompositeLongbow_StrengthPlus2,
                WeaponConstants.CompositeLongbow_StrengthPlus3,
                WeaponConstants.CompositeLongbow_StrengthPlus4);

            var proficiencyWeaponFoci = proficiencyFoci.Intersect(baseWeapons);

            if (proficiencyWeaponFoci.Any())
            {
                uncommon.AddRange(proficiencyWeaponFoci);
            }

            if (proficiencyFeats.Any(f => f.Name == FeatConstants.WeaponProficiency_Simple
                && f.Foci.Contains(GroupConstants.All)))
            {
                var simpleWeapons = WeaponConstants.GetAllSimple(false, false);
                simpleWeapons = simpleWeapons.Intersect(baseWeapons);
                uncommon.AddRange(simpleWeapons);
            }

            if (proficiencyFeats.Any(f => f.Name == FeatConstants.WeaponProficiency_Martial
                && f.Foci.Contains(GroupConstants.All)))
            {
                var martialWeapons = WeaponConstants.GetAllMartial(false, true);

                martialWeapons = martialWeapons.Intersect(baseWeapons);
                uncommon.AddRange(martialWeapons);
            }

            if (proficiencyFeats.Any(f => f.Name == FeatConstants.WeaponProficiency_Exotic
                && f.Foci.Contains(GroupConstants.All)))
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

            common = SwapForTemplates(common, WeaponConstants.CompositeShortbow,
                WeaponConstants.CompositeShortbow_StrengthPlus0,
                WeaponConstants.CompositeShortbow_StrengthPlus1,
                WeaponConstants.CompositeShortbow_StrengthPlus2);

            common = SwapForTemplates(common, WeaponConstants.CompositeLongbow,
                WeaponConstants.CompositeLongbow_StrengthPlus0,
                WeaponConstants.CompositeLongbow_StrengthPlus1,
                WeaponConstants.CompositeLongbow_StrengthPlus2,
                WeaponConstants.CompositeLongbow_StrengthPlus3,
                WeaponConstants.CompositeLongbow_StrengthPlus4);

            uncommon = SwapForTemplates(uncommon, WeaponConstants.CompositeShortbow,
                WeaponConstants.CompositeShortbow_StrengthPlus0,
                WeaponConstants.CompositeShortbow_StrengthPlus1,
                WeaponConstants.CompositeShortbow_StrengthPlus2);

            uncommon = SwapForTemplates(uncommon, WeaponConstants.CompositeLongbow,
                WeaponConstants.CompositeLongbow_StrengthPlus0,
                WeaponConstants.CompositeLongbow_StrengthPlus1,
                WeaponConstants.CompositeLongbow_StrengthPlus2,
                WeaponConstants.CompositeLongbow_StrengthPlus3,
                WeaponConstants.CompositeLongbow_StrengthPlus4);

            return (common, uncommon);
        }

        private List<string> SwapForTemplates(IEnumerable<string> names, string source, params string[] templates)
        {
            var swappedNames = names.ToList();

            if (!swappedNames.Contains(source))
                return swappedNames;

            swappedNames.Remove(source);
            swappedNames.AddRange(templates);

            return swappedNames;
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

        private IEnumerable<Item> GetPredeterminedItems(string creatureName, string size, string weaponSize)
        {
            var setItems = new List<Item>();
            var setTreasure = collectionSelector.SelectFrom(TableNameConstants.Collection.PredeterminedItems, creatureName);
            var setItemTemplates = GetTemplates(setTreasure, size, weaponSize);

            foreach (var template in setItemTemplates)
            {
                var item = GetItemFrom(template);
                setItems.Add(item);
            }

            return setItems;
        }

        private IEnumerable<Item> GetTemplates(IEnumerable<string> setTreasure, string size, string weaponSize)
        {
            var templates = new List<Item>();
            var sizes = SizeConstants.GetOrdered();

            foreach (var setItemTemplate in setTreasure)
            {
                var template = itemSelector.SelectFrom(setItemTemplate);
                if (!template.Traits.Intersect(sizes).Any())
                {
                    if (template.ItemType == ItemTypeConstants.Weapon)
                    {
                        template.Traits.Add(weaponSize);
                    }
                    else
                    {
                        template.Traits.Add(size);
                    }
                }

                templates.Add(template);
            }

            return templates;
        }

        private Item GetItemFrom(Item template)
        {
            if (template.IsMagical)
            {
                var magicalGenerator = justInTimeFactory.Build<MagicalItemGenerator>(template.ItemType);
                return magicalGenerator.Generate(template, false);
            }

            var mundaneGenerator = justInTimeFactory.Build<MundaneItemGenerator>(template.ItemType);
            return mundaneGenerator.Generate(template, false);
        }
    }
}
