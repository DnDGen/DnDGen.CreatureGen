using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Alignments;
using DnDGen.CreatureGen.Attacks;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Defenses;
using DnDGen.CreatureGen.Feats;
using DnDGen.CreatureGen.Generators.Creatures;
using DnDGen.CreatureGen.Magics;
using DnDGen.CreatureGen.Skills;
using DnDGen.CreatureGen.Verifiers;
using DnDGen.TreasureGen.Items;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DnDGen.CreatureGen.Tests.Integration
{
    public class CreatureAsserter
    {
        private readonly IEnumerable<string> skillsWithFoci;
        private readonly ICreatureVerifier creatureVerifier;

        public CreatureAsserter(ICreatureVerifier creatureVerifier)
        {
            this.creatureVerifier = creatureVerifier;

            skillsWithFoci = new[]
            {
                SkillConstants.Craft,
                SkillConstants.Knowledge,
                SkillConstants.Perform,
                SkillConstants.Profession,
            };
        }

        public void AssertCreature(Creature creature, string message = null)
        {
            message ??= creature.Summary;

            foreach (var type in creature.Type.AllTypes)
            {
                var verifierMessage = new StringBuilder();
                verifierMessage.AppendLine(message);
                verifierMessage.AppendLine($"\tAs Character: {false}");
                verifierMessage.AppendLine($"\tCreature Type: {type}");
                verifierMessage.AppendLine($"\tCreature Alignment: {creature.Alignment.Full}");

                //INFO: We are not asserting that the challenge rating is valid
                //Since the CR can be altered by advancement and by generating as a character
                var filters = new Filters();
                filters.Type = type;
                filters.Templates = creature.Templates;
                filters.Alignment = creature.Alignment.Full;

                var isValid = creatureVerifier.VerifyCompatibility(false, creature.Name, filters);
                Assert.That(isValid, Is.True, verifierMessage.ToString());
            }

            VerifySummary(creature, message);
            VerifyDemographics(creature, message);
            VerifyAlignment(creature, message);
            VerifyStatistics(creature, message);
            VerifyAbilities(creature, message);
            VerifySkills(creature, message);
            VerifyFeats(creature, message);
            VerifyCombat(creature, message);
            VerifyEquipment(creature, message);
            VerifyMagic(creature, message);

            Assert.That(creature.ChallengeRating, Is.Not.Empty, message);
            Assert.That(creature.CasterLevel, Is.Not.Negative, message);
            Assert.That(creature.NumberOfHands, Is.Not.Negative, message);
            Assert.That(creature.Languages, Is.Empty.Or.Unique, message);
        }

        public void VerifyMagic(Creature creature, string message)
        {
            Assert.That(creature.Magic, Is.Not.Null, message);

            if (string.IsNullOrEmpty(creature.Magic.Caster))
            {
                Assert.That(creature.Magic.CasterLevel, Is.Zero, message);
                Assert.That(creature.Magic.ArcaneSpellFailure, Is.Zero, message);
                Assert.That(creature.Magic.KnownSpells, Is.Empty, message);
                Assert.That(creature.Magic.SpellsPerDay, Is.Empty, message);
                Assert.That(creature.Magic.PreparedSpells, Is.Empty, message);
                Assert.That(creature.Magic.Domains, Is.Empty, message);

                return;
            }

            Assert.That(creature.Magic.Caster, Is.EqualTo(SpellConstants.Casters.Bard)
                .Or.EqualTo(SpellConstants.Casters.Cleric)
                .Or.EqualTo(SpellConstants.Casters.Druid)
                .Or.EqualTo(SpellConstants.Casters.Sorcerer), message);

            Assert.That(creature.Magic.CasterLevel, Is.Positive, message);
            Assert.That(creature.Magic.ArcaneSpellFailure, Is.InRange(0, 100), message);
            Assert.That(creature.Magic.Domains, Is.Not.Null, message);

            var castingAbility = string.Empty;
            if (creature.Magic.Caster == SpellConstants.Casters.Bard || creature.Magic.Caster == SpellConstants.Casters.Sorcerer)
            {
                castingAbility = AbilityConstants.Charisma;
            }
            else if (creature.Magic.Caster == SpellConstants.Casters.Cleric || creature.Magic.Caster == SpellConstants.Casters.Druid)
            {
                castingAbility = AbilityConstants.Wisdom;
            }

            Assert.That(creature.Magic.CastingAbility.Name, Is.EqualTo(castingAbility), message);
            Assert.That(creature.Magic.CastingAbility, Is.EqualTo(creature.Abilities[castingAbility]), message);

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

            if (undeadTemplates.Intersect(creature.Templates).Any())
            {
                castingScore -= creature.Magic.CastingAbility.TemplateAdjustment;
            }

            var casterMessage = $"{message}\nCaster: {creature.Magic.Caster}\nCasting Ability: {creature.Magic.CastingAbility.Name}: {creature.Magic.CastingAbility.FullScore} (+{creature.Magic.CastingAbility.TemplateAdjustment})";

            if (castingScore < 10)
            {
                Assert.That(creature.Magic.SpellsPerDay, Is.Empty, casterMessage);
                Assert.That(creature.Magic.KnownSpells, Is.Empty, casterMessage);
                Assert.That(creature.Magic.PreparedSpells, Is.Empty, casterMessage);

                return;
            }

            Assert.That(creature.Magic.SpellsPerDay, Is.Not.Empty, casterMessage);
            Assert.That(creature.Magic.KnownSpells, Is.Not.Empty, casterMessage);
            Assert.That(creature.Magic.PreparedSpells, Is.Not.Null, casterMessage);

            if (creature.Equipment.Armor == null && creature.Equipment.Shield == null)
            {
                Assert.That(creature.Magic.ArcaneSpellFailure, Is.Zero, message);
            }

            var hasDomain = creature.Magic.Domains.Any();

            foreach (var spellQuantity in creature.Magic.SpellsPerDay)
            {
                Assert.That(spellQuantity.BonusSpells, Is.Not.Negative, message);
                Assert.That(spellQuantity.HasDomainSpell, Is.EqualTo(hasDomain && spellQuantity.Level > 0), message);
                Assert.That(spellQuantity.Level, Is.InRange(0, 9), message);
                Assert.That(spellQuantity.Quantity, Is.Not.Negative, message);
                Assert.That(spellQuantity.Source, Is.EqualTo(creature.Magic.Caster), message);
                Assert.That(spellQuantity.TotalQuantity, Is.Not.Negative, message);

                if (creature.Magic.PreparedSpells.Any())
                {
                    var spells = creature.Magic.PreparedSpells.Where(s => s.Level == spellQuantity.Level);
                    Assert.That(spells.Count(), Is.EqualTo(spellQuantity.TotalQuantity), message);
                }
            }

            foreach (var spell in creature.Magic.KnownSpells)
            {
                Assert.That(spell.Metamagic, Is.Empty, message);
                Assert.That(spell.Level, Is.InRange(0, 9), message);
                Assert.That(spell.Source, Is.EqualTo(creature.Magic.Caster), message);
            }

            foreach (var spell in creature.Magic.PreparedSpells)
            {
                Assert.That(spell.Metamagic, Is.Empty, message);
                Assert.That(spell.Level, Is.InRange(0, 9), message);
                Assert.That(spell.Source, Is.EqualTo(creature.Magic.Caster), message);

                var knownSpells = creature.Magic.KnownSpells.Where(s => s.Name == spell.Name && s.Source == spell.Source);
                Assert.That(knownSpells, Is.Not.Empty, message);

                //INFO: Doing it this way for when a spell can have multiple levels due to domains
                var knownLevels = knownSpells.Select(s => s.Level);
                Assert.That(knownLevels, Contains.Item(spell.Level), $"{message}\nSpell: {spell.Name}");
            }
        }

        private void VerifyEquipment(Creature creature, string message)
        {
            Assert.That(creature.Equipment, Is.Not.Null, message);

            if (!creature.CanUseEquipment)
            {
                Assert.That(creature.Equipment.Weapons, Is.Empty, message);
                Assert.That(creature.Equipment.Items, Is.Empty, message);

                if (creature.Name == CreatureConstants.HellHound_NessianWarhound)
                    Assert.That(creature.Equipment.Armor, Is.Not.Null, message);
                else
                    Assert.That(creature.Equipment.Armor, Is.Null, message);
            }

            var armorNames = ArmorConstants.GetAllArmors(true);
            var shieldNames = ArmorConstants.GetAllShields(true);
            var weaponNames = WeaponConstants.GetAllWeapons(true, false);

            if (creature.Equipment.Armor != null)
            {
                var armorMessage = $"{message}\nArmor: {creature.Equipment.Armor.Description}";
                Assert.That(creature.Equipment.Armor.ArmorBonus, Is.Positive, armorMessage);
                Assert.That(armorNames, Contains.Item(creature.Equipment.Armor.Name), armorMessage);
            }

            if (creature.Equipment.Shield != null)
            {
                var shieldMessage = $"{message}\nShield: {creature.Equipment.Shield.Description}";
                Assert.That(creature.Equipment.Shield.ArmorBonus, Is.Positive, shieldMessage);
                Assert.That(shieldNames, Contains.Item(creature.Equipment.Shield.Name), shieldMessage);
            }

            var unnaturalAttacks = creature.Attacks.Where(a => !a.IsNatural && creature.Equipment.Weapons.Any(w => a.Name.StartsWith(w.Description)));

            foreach (var attack in unnaturalAttacks)
            {
                Weapon weapon = null;

                //INFO: Lycanthropes have a modifed name for the attack based on their form
                if (creature.Templates.Any(t => t.Contains("Lycanthrope")))
                {
                    weapon = creature.Equipment.Weapons.FirstOrDefault(w => attack.Name.StartsWith($"{w.Description} ("));
                }
                else
                {
                    weapon = creature.Equipment.Weapons.FirstOrDefault(w => attack.Name == w.Description);
                }

                Assert.That(weapon, Is.Not.Null, $"{message}\nAttack: {attack.Name}");

                var weaponMessage = $"{message}\nWeapon: {weapon.Description}";
                Assert.That(weapon.DamageDescription, Is.Not.Empty, weaponMessage);
                Assert.That(weaponNames, Contains.Item(weapon.Name), weaponMessage);

                Assert.That(attack.Damages, Is.Not.Empty.And.Count.EqualTo(weapon.Damages.Count));

                for (var i = 0; i < weapon.Damages.Count; i++)
                {
                    if (i == 0)
                    {
                        Assert.That(attack.DamageDescription, Contains.Substring(weapon.Damages[i].Roll), weaponMessage);
                        Assert.That(attack.DamageDescription, Contains.Substring(weapon.Damages[i].Type), weaponMessage);
                        Assert.That(attack.DamageDescription, Contains.Substring(weapon.Damages[i].Condition), weaponMessage);
                    }
                    else
                    {
                        Assert.That(attack.DamageDescription, Contains.Substring(weapon.Damages[i].Description), weaponMessage);
                    }
                }

                if (weapon.Attributes.Contains(AttributeConstants.Melee))
                {
                    Assert.That(attack.AttackType, Contains.Substring("melee"), weaponMessage);
                }
                else if (weapon.Attributes.Contains(AttributeConstants.Ranged))
                {
                    Assert.That(attack.AttackType, Contains.Substring("ranged"), weaponMessage);
                }
            }
        }

        private void VerifySummary(Creature creature, string message)
        {
            Assert.That(creature.Name, Is.Not.Empty, message);
            Assert.That(creature.Templates, Is.Not.Null, message);
            Assert.That(creature.Summary, Is.Not.Empty, message);
            Assert.That(creature.Summary, Contains.Substring(creature.Name), message);

            foreach (var template in creature.Templates)
            {
                Assert.That(creature.Summary, Contains.Substring(template), message);
            }
        }

        private void VerifyDemographics(Creature creature, string message)
        {
            Assert.That(creature.Demographics, Is.Not.Null, message);
            Assert.That(creature.Demographics.Age, Is.Not.Null, message);
            Assert.That(creature.Demographics.Age.Unit, Is.EqualTo("years"), message);
            Assert.That(creature.Demographics.Age.Value, Is.Positive, message);
            Assert.That(creature.Demographics.Age.Description, Is.Not.Empty, message);
            Assert.That(creature.Demographics.Gender, Is.Not.Empty, message);
            Assert.That(creature.Demographics.HeightOrLength, Is.Not.Null, message);
            Assert.That(creature.Demographics.HeightOrLength.Unit, Is.EqualTo("inches"), message);
            Assert.That(creature.Demographics.HeightOrLength.Value, Is.Positive, message);
            Assert.That(creature.Demographics.HeightOrLength.Description, Is.Not.Empty, message);
            Assert.That(creature.Demographics.MaximumAge, Is.Not.Null, message);
            Assert.That(creature.Demographics.MaximumAge.Unit, Is.EqualTo("years"), message);
            Assert.That(creature.Demographics.MaximumAge.Value, Is.Positive, message);
            Assert.That(creature.Demographics.MaximumAge.Description, Is.Not.Empty, message);
            Assert.That(creature.Demographics.Weight, Is.Not.Null, message);
            Assert.That(creature.Demographics.Weight.Unit, Is.EqualTo("pounds"), message);
            Assert.That(creature.Demographics.Weight.Value, Is.Positive, message);
            Assert.That(creature.Demographics.Weight.Description, Is.Not.Empty, message);
        }

        private void VerifyAlignment(Creature creature, string message)
        {
            Assert.That(creature.Alignment, Is.Not.Null, message);

            if (!string.IsNullOrEmpty(creature.Alignment.Full))
            {
                Assert.That(creature.Alignment.Goodness, Is.EqualTo(AlignmentConstants.Good)
                    .Or.EqualTo(AlignmentConstants.Neutral)
                    .Or.EqualTo(AlignmentConstants.Evil), message);
                Assert.That(creature.Alignment.Lawfulness, Is.EqualTo(AlignmentConstants.Lawful)
                    .Or.EqualTo(AlignmentConstants.Neutral)
                    .Or.EqualTo(AlignmentConstants.Chaotic), message);
            }
        }

        private void VerifyStatistics(Creature creature, string message)
        {
            var ordered = ChallengeRatingConstants.GetOrdered();
            var numbers = Enumerable.Range(1, 100).Select(i => i.ToString());
            var challengeRatings = ordered.Union(numbers);

            Assert.That(challengeRatings, Contains.Item(creature.ChallengeRating), message);
            Assert.That(creature.Size, Is.EqualTo(SizeConstants.Large)
                .Or.EqualTo(SizeConstants.Colossal)
                .Or.EqualTo(SizeConstants.Gargantuan)
                .Or.EqualTo(SizeConstants.Huge)
                .Or.EqualTo(SizeConstants.Tiny)
                .Or.EqualTo(SizeConstants.Diminutive)
                .Or.EqualTo(SizeConstants.Medium)
                .Or.EqualTo(SizeConstants.Small), message);

            VerifySpeeds(creature, message);
        }

        private void VerifySpeeds(Creature creature, string message)
        {
            foreach (var speedKVP in creature.Speeds)
            {
                VerifySpeed(speedKVP.Value, message, speedKVP.Key);
            }
        }

        private void VerifySpeed(Measurement speed, string creatureSummary, string name)
        {
            var message = $"{creatureSummary}\nSpeed: {name}";
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

        private void VerifyAbilities(Creature creature, string message)
        {
            Assert.That(creature.Abilities.Keys, Contains.Item(AbilityConstants.Charisma), message);
            Assert.That(creature.Abilities.Keys, Contains.Item(AbilityConstants.Constitution), message);
            Assert.That(creature.Abilities.Keys, Contains.Item(AbilityConstants.Dexterity), message);
            Assert.That(creature.Abilities.Keys, Contains.Item(AbilityConstants.Intelligence), message);
            Assert.That(creature.Abilities.Keys, Contains.Item(AbilityConstants.Strength), message);
            Assert.That(creature.Abilities.Keys, Contains.Item(AbilityConstants.Wisdom), message);
            Assert.That(creature.Abilities.Count, Is.EqualTo(6), message);

            foreach (var statKVP in creature.Abilities)
            {
                var stat = statKVP.Value;
                Assert.That(stat.Name, Is.EqualTo(statKVP.Key), message);
                Assert.That(stat.FullScore, Is.Not.Negative, message);
            }
        }

        private void VerifySkills(Creature creature, string message)
        {
            if (!creature.Skills.Any())
            {
                if (creature.HitPoints.HitDiceQuantity > 0)
                    Assert.That(creature.Abilities[AbilityConstants.Intelligence].HasScore, Is.False, message);

                if (creature.Abilities[AbilityConstants.Intelligence].HasScore)
                    Assert.That(creature.HitPoints.HitDiceQuantity, Is.Zero, message);
            }

            foreach (var skill in creature.Skills)
            {
                var skillMessage = message + $"\nSkill: {skill.Name}";

                Assert.That(skill.ArmorCheckPenalty, Is.Not.Positive, skillMessage);
                Assert.That(skill.Ranks, Is.AtMost(skill.RankCap), skillMessage);
                Assert.That(skill.RankCap, Is.Positive, skillMessage);
                Assert.That(skill.BaseAbility, Is.Not.Null, skillMessage);
                Assert.That(creature.Abilities.Values, Contains.Item(skill.BaseAbility), skillMessage);
                Assert.That(skill.Focus, Is.Not.Null, skillMessage);

                if (skillsWithFoci.Contains(skill.Name))
                    Assert.That(skill.Focus, Is.Not.Empty, skillMessage);
                else
                    Assert.That(skill.Focus, Is.Empty, skillMessage);
            }

            var skillNamesAndFoci = creature.Skills.Select(s => s.Name + s.Focus);
            Assert.That(skillNamesAndFoci, Is.Unique, message);
        }

        private void VerifyFeats(Creature creature, string message)
        {
            Assert.That(creature.Feats, Is.Not.Null, message);
            Assert.That(creature.SpecialQualities, Is.Not.Null, message);

            var weapons = WeaponConstants.GetAllWeapons(false, false);
            var allFeats = creature.Feats.Union(creature.SpecialQualities);

            foreach (var feat in allFeats)
            {
                var featMessage = $"{message}\nFeat: {feat.Name}";

                Assert.That(feat.Name, Is.Not.Empty, featMessage);
                Assert.That(feat.Foci, Is.Not.Null, featMessage);
                Assert.That(feat.Foci, Is.All.Not.Null, featMessage);
                Assert.That(feat.Foci, Is.All.Not.EqualTo(FeatConstants.Foci.NoValidFociAvailable), featMessage);
                Assert.That(feat.Power, Is.Not.Negative, featMessage);
                Assert.That(feat.Frequency.Quantity, Is.Not.Negative, featMessage);
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
                    .Or.Empty, featMessage);

                if (!creature.CanUseEquipment)
                {
                    var weaponFoci = feat.Foci.Intersect(weapons);
                    Assert.That(weaponFoci, Is.Empty, featMessage);
                }
            }
        }

        private void VerifyCombat(Creature creature, string message)
        {
            Assert.That(creature.BaseAttackBonus, Is.Not.Negative, message);

            //INFO: Hit Dice can be empty, if the creature was generated as a character
            foreach (var hitDice in creature.HitPoints.HitDice)
            {
                Assert.That(hitDice.Quantity, Is.Positive, message);
                Assert.That(hitDice.RoundedQuantity, Is.Positive, message);
                Assert.That(hitDice.HitDie, Is.EqualTo(6)
                    .Or.EqualTo(8)
                    .Or.EqualTo(10)
                    .Or.EqualTo(12), message);
                Assert.That(hitDice.Divisor, Is.Positive, message);
                Assert.That(hitDice.DefaultRoll, Contains.Substring($"{hitDice.RoundedQuantity}d{hitDice.HitDie}"), message);

                Assert.That(creature.HitPoints.DefaultRoll, Contains.Substring($"{hitDice.RoundedQuantity}d{hitDice.HitDie}"), message);
            }

            Assert.That(creature.HitPoints.HitDiceQuantity, Is.Not.Negative, message);
            Assert.That(creature.HitPoints.RoundedHitDiceQuantity, Is.Not.Negative
                .And.AtLeast(creature.HitPoints.HitDice.Count), message);

            Assert.That(creature.HitPoints.Total, Is.Not.Negative
                .And.AtLeast(creature.HitPoints.RoundedHitDiceQuantity), message);
            Assert.That(creature.HitPoints.DefaultTotal, Is.Not.Negative
                .And.AtLeast(creature.HitPoints.RoundedHitDiceQuantity), message);

            Assert.That(creature.FullMeleeAttack, Is.Not.Null, message);
            Assert.That(creature.FullRangedAttack, Is.Not.Null, message);

            if (creature.MeleeAttack != null)
            {
                Assert.That(creature.MeleeAttack.IsMelee, Is.True, message);
                Assert.That(creature.MeleeAttack.IsSpecial, Is.False, message);
                Assert.That(creature.FullMeleeAttack, Is.Not.Empty, message);
                Assert.That(creature.FullMeleeAttack.All(a => a.IsMelee && !a.IsSpecial), Is.True, message);
            }

            if (creature.RangedAttack != null)
            {
                Assert.That(creature.RangedAttack.IsMelee, Is.False, message);
                Assert.That(creature.RangedAttack.IsSpecial, Is.False, message);
                Assert.That(creature.FullRangedAttack, Is.Not.Empty, message);
                Assert.That(creature.FullRangedAttack.All(a => !a.IsMelee && !a.IsSpecial), Is.True, message);
            }

            foreach (var attack in creature.Attacks)
                AssertAttack(attack, creature, message);

            Assert.That(creature.ArmorClass.TotalBonus, Is.Positive, message);
            Assert.That(creature.ArmorClass.FlatFootedBonus, Is.Positive, message);
            Assert.That(creature.ArmorClass.TouchBonus, Is.Positive, message);

            Assert.That(creature.InitiativeBonus, Is.Not.Negative, message);

            if (creature.Abilities[AbilityConstants.Dexterity].HasScore)
                Assert.That(creature.TotalInitiativeBonus, Is.AtLeast(creature.Abilities[AbilityConstants.Dexterity].Modifier), message);
            else
                Assert.That(creature.TotalInitiativeBonus, Is.AtLeast(creature.Abilities[AbilityConstants.Intelligence].Modifier), message);

            Assert.That(creature.Saves[SaveConstants.Reflex].TotalBonus, Is.AtLeast(creature.Abilities[AbilityConstants.Dexterity].Modifier), message);
            Assert.That(creature.Saves[SaveConstants.Will].TotalBonus, Is.AtLeast(creature.Abilities[AbilityConstants.Wisdom].Modifier), message);
            Assert.That(creature.Saves[SaveConstants.Fortitude].TotalBonus, Is.AtLeast(creature.Abilities[AbilityConstants.Constitution].Modifier), message);
        }

        private void AssertAttack(Attack attack, Creature creature, string message)
        {
            var attackMessage = $"{message}\nAttack: {attack.Name}";
            var meleeEquipmentAttacks = creature.Attacks.Where(a => a.IsMelee
                && (creature.Equipment.Weapons.Any(w => a.Name.StartsWith(w.Description)) || a.Name.StartsWith(AttributeConstants.Melee)));
            var rangedEquipmentAttacks = creature.Attacks.Where(a => !a.IsMelee
                && (creature.Equipment.Weapons.Any(w => a.Name.StartsWith(w.Description)) || a.Name.StartsWith(AttributeConstants.Ranged)));

            Assert.That(attack.Name, Is.Not.Empty, attackMessage);
            Assert.That(attack.AttackType, Is.Not.Empty, attackMessage);
            Assert.That(attack.BaseAttackBonus, Is.Not.Negative, attackMessage);
            Assert.That(attack.Frequency, Is.Not.Null, attackMessage);
            Assert.That(attack.Frequency.Quantity, Is.Not.Negative, attackMessage);
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
                .Or.Contains(FeatConstants.Frequencies.Constant), attackMessage);

            if (!attack.IsNatural)
            {
                Assert.That(creature.CanUseEquipment, Is.True, attackMessage);
            }

            if (!attack.IsPrimary
                && !attack.IsSpecial
                && !attack.IsNatural
                && ((meleeEquipmentAttacks.Contains(attack) && meleeEquipmentAttacks.Count() > 1)
                    || rangedEquipmentAttacks.Contains(attack) && rangedEquipmentAttacks.Count() > 1))
            {
                Assert.That(attack.AttackBonuses, Contains.Item(-10), attackMessage);
            }
            else if (!attack.IsPrimary && !attack.IsSpecial)
            {
                Assert.That(attack.AttackBonuses, Contains.Item(-5), attackMessage);
            }

            if (!attack.IsSpecial)
            {
                Assert.That(attack.BaseAbility, Is.Not.Null, message);
                Assert.That(creature.Abilities.Values, Contains.Item(attack.BaseAbility), attackMessage);

                if (attack.IsNatural)
                {
                    Assert.That(attack.DamageDescription, Is.Not.Empty, attackMessage);
                }
            }

            if (attack.Save != null)
            {
                if (attack.IsNatural && attack.Save.BaseAbility != null)
                    Assert.That(creature.Abilities.Values, Contains.Item(attack.Save.BaseAbility), attackMessage);

                Assert.That(attack.Save.BaseValue, Is.Positive, message);
                Assert.That(attack.Save.DC, Is.Positive, message);
                Assert.That(attack.Save.Save, Is.EqualTo(SaveConstants.Fortitude)
                    .Or.EqualTo(SaveConstants.Reflex)
                    .Or.EqualTo(SaveConstants.Will)
                    .Or.Empty, attackMessage);
            }

            var clawDamage = $"{AttributeConstants.DamageTypes.Piercing}/{AttributeConstants.DamageTypes.Slashing}";
            var biteDamage = $"{AttributeConstants.DamageTypes.Piercing}/{AttributeConstants.DamageTypes.Slashing}/{AttributeConstants.DamageTypes.Bludgeoning}";

            Weapon weapon = null;

            //INFO: Lycanthropes have a modifed name for the attack based on their form
            if (creature.Templates.Any(t => t.Contains("Lycanthrope")))
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
                    $"{attackMessage}\nWeapon: {weapon.Description} ({weapon.DamageDescription})\nAttack Damage: {attack.DamageDescription}");

                for (var i = 0; i < weapon.Damages.Count; i++)
                {
                    if (i == 0)
                    {
                        Assert.That(attack.DamageDescription, Contains.Substring(weapon.Damages[i].Roll), $"{attackMessage}\nWeapon: {weapon.Description}");
                        Assert.That(attack.DamageDescription, Contains.Substring(weapon.Damages[i].Type), $"{attackMessage}\nWeapon: {weapon.Description}");
                        Assert.That(attack.DamageDescription, Contains.Substring(weapon.Damages[i].Condition), $"{attackMessage}\nWeapon: {weapon.Description}");
                    }
                    else
                    {
                        Assert.That(attack.DamageDescription, Contains.Substring(weapon.Damages[i].Description), $"{attackMessage}\nWeapon: {weapon.Description}");
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
                    .Or.EqualTo("Ability points (of ghost's choosing)"), $"{attackMessage}\nDamage: {damage.Description}");
            }
        }

        public void AssertCreatureAsCharacter(Creature creature, string message = null)
        {
            message ??= creature.Summary;

            AssertCreature(creature, message);

            foreach (var type in creature.Type.AllTypes)
            {
                var verifierMessage = new StringBuilder();
                verifierMessage.AppendLine(message);
                verifierMessage.AppendLine($"\tAs Character: {true}");
                verifierMessage.AppendLine($"\tCreature Type: {type}");
                verifierMessage.AppendLine($"\tCreature Alignment: {creature.Alignment.Full}");

                //INFO: We are not asserting that the challenge rating is valid
                //Since the CR can be altered by advancement and by generating as a character
                var filters = new Filters();
                filters.Type = type;
                filters.Templates = creature.Templates;
                filters.Alignment = creature.Alignment.Full;

                var isValid = creatureVerifier.VerifyCompatibility(true, creature.Name, filters);
                Assert.That(isValid, Is.True, verifierMessage.ToString());
            }

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
                CreatureConstants.Templates.Lycanthrope_Bear_Black_Afflicted,
                CreatureConstants.Templates.Lycanthrope_Bear_Black_Natural,
                CreatureConstants.Templates.Lycanthrope_Bear_Brown_Afflicted,
                CreatureConstants.Templates.Lycanthrope_Bear_Brown_Natural,
                CreatureConstants.Templates.Lycanthrope_Bear_Dire_Afflicted,
                CreatureConstants.Templates.Lycanthrope_Bear_Dire_Natural,
                CreatureConstants.Templates.Lycanthrope_Bear_Polar_Afflicted,
                CreatureConstants.Templates.Lycanthrope_Bear_Polar_Natural,
                CreatureConstants.Templates.Lycanthrope_Boar_Afflicted,
                CreatureConstants.Templates.Lycanthrope_Boar_Natural,
                CreatureConstants.Templates.Lycanthrope_Boar_Dire_Afflicted,
                CreatureConstants.Templates.Lycanthrope_Boar_Dire_Natural,
                CreatureConstants.Templates.Lycanthrope_Rat_Afflicted,
                CreatureConstants.Templates.Lycanthrope_Rat_Natural,
                CreatureConstants.Templates.Lycanthrope_Rat_Dire_Afflicted,
                CreatureConstants.Templates.Lycanthrope_Rat_Dire_Natural,
                CreatureConstants.Templates.Lycanthrope_Tiger_Afflicted,
                CreatureConstants.Templates.Lycanthrope_Tiger_Natural,
                CreatureConstants.Templates.Lycanthrope_Tiger_Dire_Afflicted,
                CreatureConstants.Templates.Lycanthrope_Tiger_Dire_Natural,
                CreatureConstants.Templates.Lycanthrope_Wolf_Afflicted,
                CreatureConstants.Templates.Lycanthrope_Wolf_Natural,
                CreatureConstants.Templates.Lycanthrope_Wolf_Dire_Afflicted,
                CreatureConstants.Templates.Lycanthrope_Wolf_Dire_Natural,
            };

            if (creature.Type.Name == CreatureConstants.Types.Humanoid
                && !multiHitDieHumanoids.Contains(creature.Name)
                && !lycanthropes.Intersect(creature.Templates).Any())
            {
                Assert.That(creature.HitPoints.HitDice, Is.Empty, message);
                Assert.That(creature.HitPoints.DefaultTotal, Is.Zero, message);
                Assert.That(creature.HitPoints.Total, Is.Zero, message);

                Assert.That(creature.ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR0), message);
                Assert.That(creature.Feats, Is.Empty, string.Join(", ", creature.Feats.Select(f => f.Name)));
                Assert.That(creature.Skills, Is.Empty, string.Join(", ", creature.Skills.Select(f => f.Name)));
            }
        }
    }
}
