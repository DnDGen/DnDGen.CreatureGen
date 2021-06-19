using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Alignments;
using DnDGen.CreatureGen.Attacks;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Defenses;
using DnDGen.CreatureGen.Feats;
using DnDGen.CreatureGen.Magics;
using DnDGen.CreatureGen.Skills;
using DnDGen.TreasureGen.Items;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Integration
{
    public class CreatureAsserter
    {
        private readonly IEnumerable<string> skillsWithFoci;

        public CreatureAsserter()
        {
            skillsWithFoci = new[]
            {
                SkillConstants.Craft,
                SkillConstants.Knowledge,
                SkillConstants.Perform,
                SkillConstants.Profession,
            };
        }

        public void AssertCreature(Creature creature)
        {
            VerifySummary(creature);
            VerifyAlignment(creature);
            VerifyStatistics(creature);
            VerifyAbilities(creature);
            VerifySkills(creature);
            VerifyFeats(creature);
            VerifyCombat(creature);
            VerifyEquipment(creature);
            VerifyMagic(creature);

            Assert.That(creature.ChallengeRating, Is.Not.Empty, creature.Summary);
            Assert.That(creature.CasterLevel, Is.Not.Negative, creature.Summary);
            Assert.That(creature.NumberOfHands, Is.Not.Negative, creature.Summary);
            Assert.That(creature.Languages, Is.Empty.Or.Unique, creature.Summary);
        }

        public void VerifyMagic(Creature creature)
        {
            Assert.That(creature.Magic, Is.Not.Null);

            if (string.IsNullOrEmpty(creature.Magic.Caster))
            {
                Assert.That(creature.Magic.CasterLevel, Is.Zero, creature.Summary);
                Assert.That(creature.Magic.ArcaneSpellFailure, Is.Zero, creature.Summary);
                Assert.That(creature.Magic.KnownSpells, Is.Empty, creature.Summary);
                Assert.That(creature.Magic.SpellsPerDay, Is.Empty, creature.Summary);
                Assert.That(creature.Magic.PreparedSpells, Is.Empty, creature.Summary);
                Assert.That(creature.Magic.Domains, Is.Empty, creature.Summary);

                return;
            }

            Assert.That(creature.Magic.Caster, Is.EqualTo(SpellConstants.Casters.Bard)
                .Or.EqualTo(SpellConstants.Casters.Cleric)
                .Or.EqualTo(SpellConstants.Casters.Druid)
                .Or.EqualTo(SpellConstants.Casters.Sorcerer), creature.Summary);

            Assert.That(creature.Magic.CasterLevel, Is.Positive, creature.Summary);
            Assert.That(creature.Magic.ArcaneSpellFailure, Is.InRange(0, 100), creature.Summary);
            Assert.That(creature.Magic.Domains, Is.Not.Null, creature.Summary);

            var castingAbility = string.Empty;
            if (creature.Magic.Caster == SpellConstants.Casters.Bard || creature.Magic.Caster == SpellConstants.Casters.Sorcerer)
            {
                castingAbility = AbilityConstants.Charisma;
            }
            else if (creature.Magic.Caster == SpellConstants.Casters.Cleric || creature.Magic.Caster == SpellConstants.Casters.Druid)
            {
                castingAbility = AbilityConstants.Wisdom;
            }

            Assert.That(creature.Magic.CastingAbility.Name, Is.EqualTo(castingAbility), creature.Summary);
            Assert.That(creature.Magic.CastingAbility, Is.EqualTo(creature.Abilities[castingAbility]), creature.Summary);

            //INFO: Undead templates do not get to regenerate magic based on new abilities
            var castingScore = creature.Magic.CastingAbility.FullScore;
            var undeadTemplates = new[]
            {
                CreatureConstants.Templates.Ghost,
                CreatureConstants.Templates.Lich,
                CreatureConstants.Templates.Vampire,
                CreatureConstants.Templates.Skeleton,
                CreatureConstants.Templates.Zombie,
            };

            if (undeadTemplates.Contains(creature.Template))
            {
                castingScore -= creature.Magic.CastingAbility.TemplateAdjustment;
            }

            if (castingScore < 10)
            {
                Assert.That(creature.Magic.SpellsPerDay, Is.Empty, $"{creature.Summary}: {creature.Magic.Caster} ({creature.Magic.CastingAbility.Name}): {creature.Magic.CastingAbility.FullScore} (+{creature.Magic.CastingAbility.TemplateAdjustment})");
                Assert.That(creature.Magic.KnownSpells, Is.Empty, $"{creature.Summary}: {creature.Magic.Caster} ({creature.Magic.CastingAbility.Name}): {creature.Magic.CastingAbility.FullScore} (+{creature.Magic.CastingAbility.TemplateAdjustment})");
                Assert.That(creature.Magic.PreparedSpells, Is.Empty, $"{creature.Summary}: {creature.Magic.Caster} ({creature.Magic.CastingAbility.Name}): {creature.Magic.CastingAbility.FullScore} (+{creature.Magic.CastingAbility.TemplateAdjustment})");

                return;
            }

            Assert.That(creature.Magic.SpellsPerDay, Is.Not.Empty, $"{creature.Summary}: {creature.Magic.Caster} ({creature.Magic.CastingAbility.Name}): {creature.Magic.CastingAbility.FullScore} (+{creature.Magic.CastingAbility.TemplateAdjustment})");
            Assert.That(creature.Magic.KnownSpells, Is.Not.Empty, $"{creature.Summary}: {creature.Magic.Caster} ({creature.Magic.CastingAbility.Name}): {creature.Magic.CastingAbility.FullScore} (+{creature.Magic.CastingAbility.TemplateAdjustment})");
            Assert.That(creature.Magic.PreparedSpells, Is.Not.Null, $"{creature.Summary}: {creature.Magic.Caster} ({creature.Magic.CastingAbility.Name}): {creature.Magic.CastingAbility.FullScore} (+{creature.Magic.CastingAbility.TemplateAdjustment})");

            if (creature.Equipment.Armor == null && creature.Equipment.Shield == null)
            {
                Assert.That(creature.Magic.ArcaneSpellFailure, Is.Zero, creature.Summary);
            }

            var hasDomain = creature.Magic.Domains.Any();

            foreach (var spellQuantity in creature.Magic.SpellsPerDay)
            {
                Assert.That(spellQuantity.BonusSpells, Is.Not.Negative, creature.Summary);
                Assert.That(spellQuantity.HasDomainSpell, Is.EqualTo(hasDomain && spellQuantity.Level > 0), creature.Summary);
                Assert.That(spellQuantity.Level, Is.InRange(0, 9), creature.Summary);
                Assert.That(spellQuantity.Quantity, Is.Not.Negative, creature.Summary);
                Assert.That(spellQuantity.Source, Is.EqualTo(creature.Magic.Caster), creature.Summary);
                Assert.That(spellQuantity.TotalQuantity, Is.Not.Negative, creature.Summary);

                if (creature.Magic.PreparedSpells.Any())
                {
                    var spells = creature.Magic.PreparedSpells.Where(s => s.Level == spellQuantity.Level);
                    Assert.That(spells.Count(), Is.EqualTo(spellQuantity.TotalQuantity));
                }
            }

            foreach (var spell in creature.Magic.KnownSpells)
            {
                Assert.That(spell.Metamagic, Is.Empty, creature.Summary);
                Assert.That(spell.Level, Is.InRange(0, 9), creature.Summary);
                Assert.That(spell.Source, Is.EqualTo(creature.Magic.Caster), creature.Summary);
            }

            foreach (var spell in creature.Magic.PreparedSpells)
            {
                Assert.That(spell.Metamagic, Is.Empty, creature.Summary);
                Assert.That(spell.Level, Is.InRange(0, 9), creature.Summary);
                Assert.That(spell.Source, Is.EqualTo(creature.Magic.Caster), creature.Summary);

                var knownSpells = creature.Magic.KnownSpells.Where(s => s.Name == spell.Name && s.Source == spell.Source);
                Assert.That(knownSpells, Is.Not.Empty, creature.Summary);

                //INFO: Doing it this way for when a spell can have multiple levels due to domains
                var knownLevels = knownSpells.Select(s => s.Level);
                Assert.That(knownLevels, Contains.Item(spell.Level), $"{creature.Summary}: {spell.Name}");
            }
        }

        private void VerifyEquipment(Creature creature)
        {
            Assert.That(creature.Equipment, Is.Not.Null, creature.Summary);

            if (!creature.CanUseEquipment)
            {
                Assert.That(creature.Equipment.Weapons, Is.Empty, creature.Summary);
                Assert.That(creature.Equipment.Items, Is.Empty, creature.Summary);

                if (creature.Name == CreatureConstants.HellHound_NessianWarhound)
                    Assert.That(creature.Equipment.Armor, Is.Not.Null, creature.Summary);
                else
                    Assert.That(creature.Equipment.Armor, Is.Null, creature.Summary);
            }

            var armorNames = ArmorConstants.GetAllArmors(true);
            var shieldNames = ArmorConstants.GetAllShields(true);
            var weaponNames = WeaponConstants.GetAllWeapons(true, false);

            if (creature.Equipment.Armor != null)
            {
                Assert.That(creature.Equipment.Armor.ArmorBonus, Is.Positive, creature.Summary + creature.Equipment.Armor.Name);
                Assert.That(armorNames, Contains.Item(creature.Equipment.Armor.Name), creature.Summary + creature.Equipment.Armor.Name);
            }

            if (creature.Equipment.Shield != null)
            {
                Assert.That(creature.Equipment.Shield.ArmorBonus, Is.Positive, creature.Summary + creature.Equipment.Shield.Name);
                Assert.That(shieldNames, Contains.Item(creature.Equipment.Shield.Name), creature.Summary + creature.Equipment.Shield.Name);
            }

            var unnaturalAttacks = creature.Attacks.Where(a => !a.IsNatural && creature.Equipment.Weapons.Any(w => a.Name.StartsWith(w.Description)));

            foreach (var attack in unnaturalAttacks)
            {
                Weapon weapon = null;

                //INFO: Lycanthropes have a modifed name for the attack based on their form
                if (creature.Template.Contains("Lycanthrope"))
                {
                    weapon = creature.Equipment.Weapons.FirstOrDefault(w => attack.Name.StartsWith($"{w.Description} ("));
                }
                else
                {
                    weapon = creature.Equipment.Weapons.FirstOrDefault(w => attack.Name == w.Description);
                }

                Assert.That(weapon, Is.Not.Null, $"{creature.Summary}: {attack.Name}");
                Assert.That(weapon.DamageDescription, Is.Not.Empty, $"{creature.Summary}: {weapon.Description}");
                Assert.That(weaponNames, Contains.Item(weapon.Name), $"{creature.Summary}: {weapon.Description}");

                Assert.That(attack.Damages, Is.Not.Empty.And.Count.EqualTo(weapon.Damages.Count));

                for (var i = 0; i < weapon.Damages.Count; i++)
                {
                    if (i == 0)
                    {
                        Assert.That(attack.DamageDescription, Contains.Substring(weapon.Damages[i].Roll), $"{creature.Summary}; Weapon: {weapon.Description}");
                        Assert.That(attack.DamageDescription, Contains.Substring(weapon.Damages[i].Type), $"{creature.Summary}; Weapon: {weapon.Description}");
                        Assert.That(attack.DamageDescription, Contains.Substring(weapon.Damages[i].Condition), $"{creature.Summary}; Weapon: {weapon.Description}");
                    }
                    else
                    {
                        Assert.That(attack.DamageDescription, Contains.Substring(weapon.Damages[i].Description), $"{creature.Summary}; Weapon: {weapon.Description}");
                    }
                }

                if (weapon.Attributes.Contains(AttributeConstants.Melee))
                {
                    Assert.That(attack.AttackType, Contains.Substring("melee"), $"{creature.Summary}: {weapon.Description}");
                }
                else if (weapon.Attributes.Contains(AttributeConstants.Ranged))
                {
                    Assert.That(attack.AttackType, Contains.Substring("ranged"), $"{creature.Summary}: {weapon.Description}");
                }
            }
        }

        private void VerifySummary(Creature creature)
        {
            Assert.That(creature.Name, Is.Not.Empty);
            Assert.That(creature.Template, Is.Not.Null);
            Assert.That(creature.Summary, Is.Not.Empty);
            Assert.That(creature.Summary, Contains.Substring(creature.Name));
            Assert.That(creature.Summary, Contains.Substring(creature.Template));
        }

        private void VerifyAlignment(Creature creature)
        {
            Assert.That(creature.Alignment, Is.Not.Null);

            if (!string.IsNullOrEmpty(creature.Alignment.Full))
            {
                Assert.That(creature.Alignment.Goodness, Is.EqualTo(AlignmentConstants.Good)
                    .Or.EqualTo(AlignmentConstants.Neutral)
                    .Or.EqualTo(AlignmentConstants.Evil), creature.Summary);
                Assert.That(creature.Alignment.Lawfulness, Is.EqualTo(AlignmentConstants.Lawful)
                    .Or.EqualTo(AlignmentConstants.Neutral)
                    .Or.EqualTo(AlignmentConstants.Chaotic), creature.Summary);
            }
        }

        private void VerifyStatistics(Creature creature)
        {
            var ordered = ChallengeRatingConstants.GetOrdered();
            var numbers = Enumerable.Range(1, 100).Select(i => i.ToString());
            var challengeRatings = ordered.Union(numbers);

            Assert.That(challengeRatings, Contains.Item(creature.ChallengeRating));
            Assert.That(creature.Size, Is.EqualTo(SizeConstants.Large)
                .Or.EqualTo(SizeConstants.Colossal)
                .Or.EqualTo(SizeConstants.Gargantuan)
                .Or.EqualTo(SizeConstants.Huge)
                .Or.EqualTo(SizeConstants.Tiny)
                .Or.EqualTo(SizeConstants.Diminutive)
                .Or.EqualTo(SizeConstants.Medium)
                .Or.EqualTo(SizeConstants.Small), creature.Summary);

            VerifySpeeds(creature);
        }

        private void VerifySpeeds(Creature creature)
        {
            foreach (var speedKVP in creature.Speeds)
            {
                VerifySpeed(speedKVP.Value, creature.Summary, speedKVP.Key);
            }
        }

        private void VerifySpeed(Measurement speed, string creatureSummary, string name)
        {
            var message = $"{creatureSummary} {name}";
            Assert.That(speed.Value, Is.Not.Negative, message);
            Assert.That(speed.Value % 5, Is.EqualTo(0), message);
            Assert.That(speed.Unit, Is.EqualTo("feet per round"), message);

            if (name == SpeedConstants.Fly)
            {
                Assert.That(speed.Description, Is.Not.Empty, message);
            }
            else if (creatureSummary.Contains("Lycanthrope"))
            {
                Assert.That(speed.Description, Is.Empty.Or.EqualTo("In Animal Form"), message);
            }
            else
            {
                Assert.That(speed.Description, Is.Empty, message);
            }
        }

        private void VerifyAbilities(Creature creature)
        {
            Assert.That(creature.Abilities.Keys, Contains.Item(AbilityConstants.Charisma), creature.Summary);
            Assert.That(creature.Abilities.Keys, Contains.Item(AbilityConstants.Constitution), creature.Summary);
            Assert.That(creature.Abilities.Keys, Contains.Item(AbilityConstants.Dexterity), creature.Summary);
            Assert.That(creature.Abilities.Keys, Contains.Item(AbilityConstants.Intelligence), creature.Summary);
            Assert.That(creature.Abilities.Keys, Contains.Item(AbilityConstants.Strength), creature.Summary);
            Assert.That(creature.Abilities.Keys, Contains.Item(AbilityConstants.Wisdom), creature.Summary);
            Assert.That(creature.Abilities.Count, Is.EqualTo(6), creature.Summary);

            foreach (var statKVP in creature.Abilities)
            {
                var stat = statKVP.Value;
                Assert.That(stat.Name, Is.EqualTo(statKVP.Key), creature.Summary);
                Assert.That(stat.FullScore, Is.Not.Negative, creature.Summary);
            }
        }

        private void VerifySkills(Creature creature)
        {
            if (!creature.Skills.Any())
            {
                if (creature.HitPoints.HitDiceQuantity > 0)
                    Assert.That(creature.Abilities[AbilityConstants.Intelligence].HasScore, Is.False);

                if (creature.Abilities[AbilityConstants.Intelligence].HasScore)
                    Assert.That(creature.HitPoints.HitDiceQuantity, Is.Zero);
            }

            foreach (var skill in creature.Skills)
            {
                var message = creature.Summary + skill.Name;

                Assert.That(skill.ArmorCheckPenalty, Is.Not.Positive, message);
                Assert.That(skill.Ranks, Is.AtMost(skill.RankCap), message);
                Assert.That(skill.RankCap, Is.Positive, message);
                Assert.That(skill.BaseAbility, Is.Not.Null, message);
                Assert.That(creature.Abilities.Values, Contains.Item(skill.BaseAbility), message);
                Assert.That(skill.Focus, Is.Not.Null, message);

                if (skillsWithFoci.Contains(skill.Name))
                    Assert.That(skill.Focus, Is.Not.Empty, message);
                else
                    Assert.That(skill.Focus, Is.Empty, message);
            }

            var skillNamesAndFoci = creature.Skills.Select(s => s.Name + s.Focus);
            Assert.That(skillNamesAndFoci, Is.Unique);
        }

        private void VerifyFeats(Creature creature)
        {
            Assert.That(creature.Feats, Is.Not.Null, creature.Summary);
            Assert.That(creature.SpecialQualities, Is.Not.Null, creature.Summary);

            var weapons = WeaponConstants.GetAllWeapons(false, false);
            var allFeats = creature.Feats.Union(creature.SpecialQualities);

            foreach (var feat in allFeats)
            {
                var message = $"Creature: {creature.Summary}\nFeat: {feat.Name}";

                Assert.That(feat.Name, Is.Not.Empty, message);
                Assert.That(feat.Foci, Is.Not.Null, message);
                Assert.That(feat.Foci, Is.All.Not.Null, message);
                Assert.That(feat.Foci, Is.All.Not.EqualTo(FeatConstants.Foci.NoValidFociAvailable), message);
                Assert.That(feat.Power, Is.Not.Negative, message);
                Assert.That(feat.Frequency.Quantity, Is.Not.Negative, message);
                Assert.That(feat.Frequency.TimePeriod, Is.EqualTo(FeatConstants.Frequencies.Constant)
                    .Or.EqualTo(FeatConstants.Frequencies.AtWill)
                    .Or.EqualTo(FeatConstants.Frequencies.Hit)
                    .Or.EqualTo(FeatConstants.Frequencies.Round)
                    .Or.EqualTo(FeatConstants.Frequencies.Minute)
                    .Or.EqualTo(FeatConstants.Frequencies.Hour)
                    .Or.EqualTo(FeatConstants.Frequencies.Day)
                    .Or.EndsWith(FeatConstants.Frequencies.Day)
                    .Or.EqualTo(FeatConstants.Frequencies.Week)
                    .Or.EqualTo(FeatConstants.Frequencies.Month)
                    .Or.EqualTo(FeatConstants.Frequencies.Year)
                    .Or.EqualTo(FeatConstants.Frequencies.Life)
                    .Or.Empty, message);

                if (!creature.CanUseEquipment)
                {
                    var weaponFoci = feat.Foci.Intersect(weapons);
                    Assert.That(weaponFoci, Is.Empty, message);

                    //TODO: Also should assert that equipment is empty, but equipment does not exist on creatures yet
                    //add it once we have added that
                }
            }
        }

        private void VerifyCombat(Creature creature)
        {
            Assert.That(creature.BaseAttackBonus, Is.Not.Negative, creature.Summary);

            //INFO: Hit Dice can be empty, if the creature was generated as a character

            foreach (var hitDice in creature.HitPoints.HitDice)
            {
                Assert.That(hitDice.Quantity, Is.Positive, creature.Summary);
                Assert.That(hitDice.RoundedQuantity, Is.Positive, creature.Summary);
                Assert.That(hitDice.HitDie, Is.EqualTo(6)
                    .Or.EqualTo(8)
                    .Or.EqualTo(10)
                    .Or.EqualTo(12), creature.Summary);
                Assert.That(hitDice.Divisor, Is.Positive, creature.Summary);
                Assert.That(hitDice.DefaultRoll, Contains.Substring($"{hitDice.RoundedQuantity}d{hitDice.HitDie}"), creature.Summary);

                Assert.That(creature.HitPoints.DefaultRoll, Contains.Substring($"{hitDice.RoundedQuantity}d{hitDice.HitDie}"), creature.Summary);
            }

            Assert.That(creature.HitPoints.HitDiceQuantity, Is.Not.Negative, creature.Summary);
            Assert.That(creature.HitPoints.RoundedHitDiceQuantity, Is.Not.Negative
                .And.AtLeast(creature.HitPoints.HitDice.Count), creature.Summary);

            Assert.That(creature.HitPoints.Total, Is.Not.Negative
                .And.AtLeast(creature.HitPoints.RoundedHitDiceQuantity), creature.Summary);
            Assert.That(creature.HitPoints.DefaultTotal, Is.Not.Negative
                .And.AtLeast(creature.HitPoints.RoundedHitDiceQuantity), creature.Summary);

            Assert.That(creature.FullMeleeAttack, Is.Not.Null, creature.Summary);
            Assert.That(creature.FullRangedAttack, Is.Not.Null, creature.Summary);

            if (creature.MeleeAttack != null)
            {
                Assert.That(creature.MeleeAttack.IsMelee, Is.True, creature.Summary);
                Assert.That(creature.MeleeAttack.IsSpecial, Is.False, creature.Summary);
                Assert.That(creature.FullMeleeAttack, Is.Not.Empty, creature.Summary);
                Assert.That(creature.FullMeleeAttack.All(a => a.IsMelee && !a.IsSpecial), Is.True, creature.Summary);
            }

            if (creature.RangedAttack != null)
            {
                Assert.That(creature.RangedAttack.IsMelee, Is.False, creature.Summary);
                Assert.That(creature.RangedAttack.IsSpecial, Is.False, creature.Summary);
                Assert.That(creature.FullRangedAttack, Is.Not.Empty, creature.Summary);
                Assert.That(creature.FullRangedAttack.All(a => !a.IsMelee && !a.IsSpecial), Is.True, creature.Summary);
            }

            foreach (var attack in creature.Attacks)
                AssertAttack(attack, creature);

            Assert.That(creature.ArmorClass.TotalBonus, Is.Positive, creature.Summary);
            Assert.That(creature.ArmorClass.FlatFootedBonus, Is.Positive, creature.Summary);
            Assert.That(creature.ArmorClass.TouchBonus, Is.Positive, creature.Summary);

            Assert.That(creature.InitiativeBonus, Is.Not.Negative, creature.Summary);

            if (creature.Abilities[AbilityConstants.Dexterity].HasScore)
                Assert.That(creature.TotalInitiativeBonus, Is.AtLeast(creature.Abilities[AbilityConstants.Dexterity].Modifier), creature.Summary);
            else
                Assert.That(creature.TotalInitiativeBonus, Is.AtLeast(creature.Abilities[AbilityConstants.Intelligence].Modifier), creature.Summary);

            Assert.That(creature.Saves[SaveConstants.Reflex].TotalBonus, Is.AtLeast(creature.Abilities[AbilityConstants.Dexterity].Modifier), creature.Summary);
            Assert.That(creature.Saves[SaveConstants.Will].TotalBonus, Is.AtLeast(creature.Abilities[AbilityConstants.Wisdom].Modifier), creature.Summary);
            Assert.That(creature.Saves[SaveConstants.Fortitude].TotalBonus, Is.AtLeast(creature.Abilities[AbilityConstants.Constitution].Modifier), creature.Summary);
        }

        private void AssertAttack(Attack attack, Creature creature)
        {
            var message = $"Creature: {creature.Summary}; Attack: {attack.Name}";
            var meleeEquipmentAttacks = creature.Attacks.Where(a => a.IsMelee
                && (creature.Equipment.Weapons.Any(w => a.Name.StartsWith(w.Description)) || a.Name.StartsWith(AttributeConstants.Melee)));
            var rangedEquipmentAttacks = creature.Attacks.Where(a => !a.IsMelee
                && (creature.Equipment.Weapons.Any(w => a.Name.StartsWith(w.Description)) || a.Name.StartsWith(AttributeConstants.Ranged)));

            Assert.That(attack.Name, Is.Not.Empty, message);
            Assert.That(attack.AttackType, Is.Not.Empty, message);
            Assert.That(attack.BaseAttackBonus, Is.Not.Negative, message);
            Assert.That(attack.Frequency, Is.Not.Null, message);
            Assert.That(attack.Frequency.Quantity, Is.Not.Negative, message);
            Assert.That(attack.Frequency.TimePeriod, Contains.Substring(FeatConstants.Frequencies.Round)
                .Or.Contains(FeatConstants.Frequencies.Hit)
                .Or.Contains(FeatConstants.Frequencies.Minute)
                .Or.Contains(FeatConstants.Frequencies.Hour)
                .Or.Contains(FeatConstants.Frequencies.Day)
                .Or.Contains(FeatConstants.Frequencies.Week)
                .Or.Contains(FeatConstants.Frequencies.Month)
                .Or.Contains(FeatConstants.Frequencies.Year)
                .Or.Contains(FeatConstants.Frequencies.Life)
                .Or.Contains(FeatConstants.Frequencies.AtWill)
                .Or.Contains(FeatConstants.Frequencies.Constant), message);

            if (!attack.IsNatural)
            {
                Assert.That(creature.CanUseEquipment, Is.True, message);
            }

            if (!attack.IsPrimary
                && !attack.IsSpecial
                && !attack.IsNatural
                && ((meleeEquipmentAttacks.Contains(attack) && meleeEquipmentAttacks.Count() > 1)
                    || rangedEquipmentAttacks.Contains(attack) && rangedEquipmentAttacks.Count() > 1))
            {
                Assert.That(attack.AttackBonuses, Contains.Item(-10), message);
            }
            else if (!attack.IsPrimary && !attack.IsSpecial)
            {
                Assert.That(attack.AttackBonuses, Contains.Item(-5), message);
            }

            if (!attack.IsSpecial)
            {
                Assert.That(attack.BaseAbility, Is.Not.Null, message);
                Assert.That(creature.Abilities.Values, Contains.Item(attack.BaseAbility), message);

                if (attack.IsNatural)
                {
                    Assert.That(attack.DamageDescription, Is.Not.Empty, message);
                }
            }

            if (attack.Save != null)
            {
                if (attack.IsNatural && attack.Save.BaseAbility != null)
                    Assert.That(creature.Abilities.Values, Contains.Item(attack.Save.BaseAbility), message);

                Assert.That(attack.Save.BaseValue, Is.Positive, message);
                Assert.That(attack.Save.DC, Is.Positive, message);
                Assert.That(attack.Save.Save, Is.EqualTo(SaveConstants.Fortitude)
                    .Or.EqualTo(SaveConstants.Reflex)
                    .Or.EqualTo(SaveConstants.Will)
                    .Or.Empty, message);
            }

            var clawDamage = $"{AttributeConstants.DamageTypes.Piercing}/{AttributeConstants.DamageTypes.Slashing}";
            var biteDamage = $"{AttributeConstants.DamageTypes.Piercing}/{AttributeConstants.DamageTypes.Slashing}/{AttributeConstants.DamageTypes.Bludgeoning}";

            Weapon weapon = null;

            //INFO: Lycanthropes have a modifed name for the attack based on their form
            if (creature.Template.Contains("Lycanthrope"))
            {
                weapon = creature.Equipment.Weapons.FirstOrDefault(w => attack.Name.StartsWith($"{w.Description} ("));
            }
            else
            {
                weapon = creature.Equipment.Weapons.FirstOrDefault(w => attack.Name == w.Description);
            }

            if (weapon != null)
            {
                Assert.That(attack.Damages,
                    Is.Not.Empty.And.Count.EqualTo(weapon.Damages.Count),
                    $"{message}; Weapon: {weapon.Description} ({weapon.DamageDescription}); Attack Damage: {attack.DamageDescription}");

                for (var i = 0; i < weapon.Damages.Count; i++)
                {
                    if (i == 0)
                    {
                        Assert.That(attack.DamageDescription, Contains.Substring(weapon.Damages[i].Roll), $"{message}; Weapon: {weapon.Description}");
                        Assert.That(attack.DamageDescription, Contains.Substring(weapon.Damages[i].Type), $"{message}; Weapon: {weapon.Description}");
                        Assert.That(attack.DamageDescription, Contains.Substring(weapon.Damages[i].Condition), $"{message}; Weapon: {weapon.Description}");
                    }
                    else
                    {
                        Assert.That(attack.DamageDescription, Contains.Substring(weapon.Damages[i].Description), $"{message}; Weapon: {weapon.Description}");
                    }
                }

                return;
            }

            foreach (var damage in attack.Damages)
            {
                Assert.That(damage.Roll, Is.Not.Empty);
                Assert.That(damage.Type, Is.Empty
                    .Or.EqualTo(clawDamage)
                    .Or.EqualTo(biteDamage)
                    .Or.EqualTo(AttributeConstants.DamageTypes.Bludgeoning)
                    .Or.EqualTo(AttributeConstants.DamageTypes.Piercing)
                    .Or.EqualTo(AttributeConstants.DamageTypes.Slashing)
                    .Or.EqualTo(FeatConstants.Foci.Elements.Acid)
                    .Or.EqualTo(FeatConstants.Foci.Elements.Fire)
                    .Or.EqualTo(FeatConstants.Foci.Elements.Electricity)
                    .Or.EqualTo(FeatConstants.Foci.Elements.Cold)
                    .Or.EqualTo(FeatConstants.Foci.Elements.Sonic)
                    .Or.EqualTo(AbilityConstants.Charisma)
                    .Or.EqualTo(AbilityConstants.Constitution)
                    .Or.EqualTo(AbilityConstants.Dexterity)
                    .Or.EqualTo(AbilityConstants.Intelligence)
                    .Or.EqualTo(AbilityConstants.Strength)
                    .Or.EqualTo(AbilityConstants.Wisdom)
                    .Or.EqualTo("Negative Level")
                    .Or.EqualTo("Positive energy")
                    .Or.EqualTo("Ability points (of ghost's choosing)"), $"{message}; Damage: {damage.Description}");
            }
        }

        public void AssertCreatureAsCharacter(Creature creature)
        {
            AssertCreature(creature);

            var multiHitDieHumanoids = new[]
            {
                CreatureConstants.Bugbear,
                CreatureConstants.Gnoll,
                CreatureConstants.Lizardfolk,
                CreatureConstants.Locathah,
                CreatureConstants.Troglodyte,
            };

            var lycanthropes = new[]
            {
                CreatureConstants.Templates.Lycanthrope_Bear_Brown_Afflicted,
                CreatureConstants.Templates.Lycanthrope_Bear_Brown_Natural,
                CreatureConstants.Templates.Lycanthrope_Boar_Afflicted,
                CreatureConstants.Templates.Lycanthrope_Boar_Dire_Afflicted,
                CreatureConstants.Templates.Lycanthrope_Boar_Dire_Natural,
                CreatureConstants.Templates.Lycanthrope_Boar_Natural,
                CreatureConstants.Templates.Lycanthrope_Rat_Dire_Afflicted,
                CreatureConstants.Templates.Lycanthrope_Rat_Dire_Natural,
                CreatureConstants.Templates.Lycanthrope_Tiger_Afflicted,
                CreatureConstants.Templates.Lycanthrope_Tiger_Natural,
                CreatureConstants.Templates.Lycanthrope_Wolf_Afflicted,
                CreatureConstants.Templates.Lycanthrope_Wolf_Dire_Afflicted,
                CreatureConstants.Templates.Lycanthrope_Wolf_Dire_Natural,
                CreatureConstants.Templates.Lycanthrope_Wolf_Natural,
            };

            if (creature.Type.Name == CreatureConstants.Types.Humanoid
                && !multiHitDieHumanoids.Contains(creature.Name)
                && !lycanthropes.Contains(creature.Template))
            {
                Assert.That(creature.HitPoints.HitDice, Is.Empty);
                Assert.That(creature.HitPoints.DefaultTotal, Is.Zero);
                Assert.That(creature.HitPoints.Total, Is.Zero);

                Assert.That(creature.ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR0));
                Assert.That(creature.Feats, Is.Empty, string.Join(", ", creature.Feats.Select(f => f.Name)));
                Assert.That(creature.Skills, Is.Empty, string.Join(", ", creature.Skills.Select(f => f.Name)));
            }
        }
    }
}
