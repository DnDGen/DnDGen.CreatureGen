using CreatureGen.Abilities;
using CreatureGen.Alignments;
using CreatureGen.Creatures;
using CreatureGen.Feats;
using CreatureGen.Creatures;
using CreatureGen.Skills;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using TreasureGen.Items;

namespace CreatureGen.Tests.Integration.Stress.Characters
{
    public class CharacterVerifier
    {
        private readonly IEnumerable<string> skillsWithFoci;

        public CharacterVerifier()
        {
            skillsWithFoci = new[]
            {
                SkillConstants.Craft,
                SkillConstants.Knowledge,
                SkillConstants.Perform,
                SkillConstants.Profession,
            };
        }

        public void AssertCharacter(Character character)
        {
            VerifySummary(character);
            VerifyAlignment(character);
            VerifyCharacterClass(character);
            VerifyRace(character);
            VerifyAbilities(character);
            VerifyLanguages(character);
            VerifySkills(character);
            VerifyFeats(character);
            VerifyEquipment(character);
            VerifyMagic(character);
            VerifyCombat(character);

            Assert.That(character.InterestingTrait, Is.Not.Empty, character.Summary);
            Assert.That(character.ChallengeRating, Is.Positive, character.Summary);
            Assert.That(character.ChallengeRating, Is.AtLeast(character.Race.ChallengeRating), character.Summary);
        }

        private void VerifySummary(Character character)
        {
            Assert.That(character.Summary, Is.Not.Empty);
            Assert.That(character.Summary, Contains.Substring(character.Alignment.Full));
            Assert.That(character.Summary, Contains.Substring(character.Class.Summary));
            Assert.That(character.Summary, Contains.Substring(character.Race.Summary));
        }

        private void VerifyAlignment(Character character)
        {
            Assert.That(character.Alignment.Goodness, Is.EqualTo(AlignmentConstants.Good)
                .Or.EqualTo(AlignmentConstants.Neutral)
                .Or.EqualTo(AlignmentConstants.Evil), character.Summary);
            Assert.That(character.Alignment.Lawfulness, Is.EqualTo(AlignmentConstants.Lawful)
                .Or.EqualTo(AlignmentConstants.Neutral)
                .Or.EqualTo(AlignmentConstants.Chaotic), character.Summary);
        }

        private void VerifyCharacterClass(Character character)
        {
            Assert.That(character.Class.Name, Is.Not.Empty, character.Summary);
            Assert.That(character.Class.Level, Is.Positive, character.Summary);
            Assert.That(character.Class.LevelAdjustment, Is.Not.Negative, character.Summary);
            Assert.That(character.Class.EffectiveLevel, Is.Positive, character.Summary);
            Assert.That(character.Class.ProhibitedFields, Is.Not.Null, character.Summary);
            Assert.That(character.Class.SpecialistFields, Is.Not.Null, character.Summary);
            Assert.That(character.Class.Summary, Is.Not.Empty, character.Summary);
        }

        private void VerifyRace(Character character)
        {
            Assert.That(character.Race.BaseRace, Is.Not.Empty, character.Summary);
            Assert.That(character.Race.Metarace, Is.Not.Null, character.Summary);
            Assert.That(character.Race.MetaraceSpecies, Is.Not.Null, character.Summary);
            Assert.That(character.Race.ChallengeRating, Is.Not.Negative, character.Summary);
            Assert.That(character.Race.Summary, Is.Not.Empty, character.Summary);
            Assert.That(character.Race.Size, Is.EqualTo(SizeConstants.Sizes.Large)
                .Or.EqualTo(SizeConstants.Sizes.Colossal)
                .Or.EqualTo(SizeConstants.Sizes.Gargantuan)
                .Or.EqualTo(SizeConstants.Sizes.Huge)
                .Or.EqualTo(SizeConstants.Sizes.Tiny)
                .Or.EqualTo(SizeConstants.Sizes.Medium)
                .Or.EqualTo(SizeConstants.Sizes.Small), character.Summary);

            VerifyLandSpeed(character);
            VerifyAerialSpeed(character);
            VerifySwimSpeed(character);
            VerifyAge(character);
            VerifyMaximumAge(character);
            VerifyHeight(character);
            VerifyWeight(character);
        }

        private void VerifyLandSpeed(Character character)
        {
            Assert.That(character.Race.LandSpeed.Value, Is.Positive, character.Summary);
            Assert.That(character.Race.LandSpeed.Value % 5, Is.EqualTo(0), character.Summary);

            if (character.Race.LandSpeed.Value >= 10)
                Assert.That(character.Race.LandSpeed.Value % 10, Is.EqualTo(0), character.Summary);

            Assert.That(character.Race.LandSpeed.Unit, Is.EqualTo("feet per round"), character.Summary);
            Assert.That(character.Race.LandSpeed.Description, Is.Empty, character.Summary);
        }

        private void VerifyAerialSpeed(Character character)
        {
            Assert.That(character.Race.AerialSpeed.Value, Is.Not.Negative, character.Summary);
            Assert.That(character.Race.AerialSpeed.Value % 5, Is.EqualTo(0), character.Summary);

            if (character.Race.AerialSpeed.Value >= 10)
                Assert.That(character.Race.AerialSpeed.Value % 10, Is.EqualTo(0), character.Summary);

            Assert.That(character.Race.AerialSpeed.Unit, Is.EqualTo("feet per round"), character.Summary);

            if (character.Race.AerialSpeed.Value == 0)
                Assert.That(character.Race.AerialSpeed.Description, Is.Empty, character.Summary);
            else
                Assert.That(character.Race.AerialSpeed.Description, Is.Not.Empty, character.Summary);

            if (character.Race.HasWings)
                Assert.That(character.Race.AerialSpeed.Value, Is.Positive, character.Summary);
        }

        private void VerifySwimSpeed(Character character)
        {
            Assert.That(character.Race.SwimSpeed.Value, Is.Not.Negative, character.Summary);
            Assert.That(character.Race.SwimSpeed.Value % 10, Is.EqualTo(0), character.Summary);
            Assert.That(character.Race.SwimSpeed.Unit, Is.EqualTo("feet per round"), character.Summary);
            Assert.That(character.Race.SwimSpeed.Description, Is.Empty, character.Summary);
        }

        private void VerifyAge(Character character)
        {
            Assert.That(character.Race.Age.Description, Is.EqualTo(SizeConstants.Ages.Adulthood)
                .Or.EqualTo(SizeConstants.Ages.MiddleAge)
                .Or.EqualTo(SizeConstants.Ages.Old)
                .Or.EqualTo(SizeConstants.Ages.Venerable), character.Summary);
            Assert.That(character.Race.Age.Value, Is.Positive, character.Summary);
            Assert.That(character.Race.Age.Unit, Is.EqualTo("Years"), character.Summary);

            if (character.Race.MaximumAge.Value != SizeConstants.Ages.Ageless)
                Assert.That(character.Race.Age.Value, Is.LessThanOrEqualTo(character.Race.MaximumAge.Value), character.Summary);
        }

        private void VerifyMaximumAge(Character character)
        {
            Assert.That(character.Race.MaximumAge.Value, Is.Positive.Or.EqualTo(SizeConstants.Ages.Ageless), character.Summary);
            Assert.That(character.Race.MaximumAge.Unit, Is.EqualTo("Years"), character.Summary);

            if (character.Race.MaximumAge.Value == SizeConstants.Ages.Ageless)
                Assert.That(character.Race.MaximumAge.Description, Is.EqualTo("Immortal"), character.Summary);
            else if (character.Race.BaseRace == SizeConstants.BaseRaces.Pixie)
                Assert.That(character.Race.MaximumAge.Description, Is.EqualTo("Will return to their plane of origin"), character.Summary);
            else
                Assert.That(character.Race.MaximumAge.Description, Is.EqualTo("Will die of natural causes"), character.Summary);
        }

        private void VerifyHeight(Character character)
        {
            Assert.That(character.Race.Height.Value, Is.Positive, character.Summary);
            Assert.That(character.Race.Height.Unit, Is.EqualTo("Inches"), character.Summary);
            Assert.That(character.Race.Height.Description, Is.EqualTo("Short").Or.EqualTo("Average").Or.EqualTo("Tall"), character.Summary);
        }

        private void VerifyWeight(Character character)
        {
            Assert.That(character.Race.Weight.Value, Is.Positive, character.Summary);
            Assert.That(character.Race.Weight.Unit, Is.EqualTo("Pounds"), character.Summary);
            Assert.That(character.Race.Weight.Description, Is.EqualTo("Light").Or.EqualTo("Average").Or.EqualTo("Heavy"), character.Summary);
        }

        private void VerifyLanguages(Character character)
        {
            Assert.That(character.Languages, Is.Not.Empty, character.Summary);
        }

        private void VerifyAbilities(Character character)
        {
            Assert.That(character.Abilities.Count, Is.InRange(5, 6), character.Summary);
            Assert.That(character.Abilities.Keys, Contains.Item(AbilityConstants.Charisma), character.Summary);
            Assert.That(character.Abilities.Keys, Contains.Item(AbilityConstants.Dexterity), character.Summary);
            Assert.That(character.Abilities.Keys, Contains.Item(AbilityConstants.Intelligence), character.Summary);
            Assert.That(character.Abilities.Keys, Contains.Item(AbilityConstants.Strength), character.Summary);
            Assert.That(character.Abilities.Keys, Contains.Item(AbilityConstants.Wisdom), character.Summary);

            if (character.Abilities.Count == 6)
                Assert.That(character.Abilities.Keys, Contains.Item(AbilityConstants.Constitution), character.Summary);

            foreach (var statKVP in character.Abilities)
            {
                var stat = statKVP.Value;
                Assert.That(stat.Name, Is.EqualTo(statKVP.Key), character.Summary);
                Assert.That(stat.BaseValue, Is.AtLeast(3), character.Summary);
            }
        }

        private void VerifySkills(Character character)
        {
            Assert.That(character.Skills, Is.Not.Empty, character.Summary);

            foreach (var skill in character.Skills)
            {
                Assert.That(skill.ArmorCheckPenalty, Is.Not.Positive, character.Summary);
                Assert.That(skill.Ranks, Is.AtMost(skill.RankCap), character.Summary);
                Assert.That(skill.RankCap, Is.Positive, character.Summary);
                Assert.That(skill.Bonus, Is.Not.Negative);
                Assert.That(skill.BaseAbility, Is.Not.Null);
                Assert.That(character.Abilities.Values, Contains.Item(skill.BaseAbility));
                Assert.That(skill.Focus, Is.Not.Null);

                if (skillsWithFoci.Contains(skill.Name))
                    Assert.That(skill.Focus, Is.Not.Empty);
                else
                    Assert.That(skill.Focus, Is.Empty);
            }

            var skillNamesAndFoci = character.Skills.Select(s => s.Name + s.Focus);
            Assert.That(skillNamesAndFoci, Is.Unique);
        }

        private void VerifyFeats(Character character)
        {
            Assert.That(character.Feats.Class, Is.Not.Empty, character.Summary);
            Assert.That(character.Feats.Racial, Is.Not.Null, character.Summary);
            Assert.That(character.Feats.Additional, Is.Not.Empty, character.Summary);
            Assert.That(character.Feats.All, Is.Not.Empty, character.Summary);

            foreach (var feat in character.Feats.All)
            {
                Assert.That(feat.Name, Is.Not.Empty, character.Summary);
                Assert.That(feat.Foci, Is.Not.Null, feat.Name);
                Assert.That(feat.Power, Is.Not.Negative, feat.Name);
                Assert.That(feat.Frequency.Quantity, Is.Not.Negative, feat.Name);
                Assert.That(feat.Frequency.TimePeriod, Is.EqualTo(FeatConstants.Frequencies.Constant)
                    .Or.EqualTo(FeatConstants.Frequencies.AtWill)
                    .Or.EqualTo(FeatConstants.Frequencies.Hit)
                    .Or.EqualTo(FeatConstants.Frequencies.Round)
                    .Or.EqualTo(FeatConstants.Frequencies.Turn)
                    .Or.EqualTo(FeatConstants.Frequencies.Day)
                    .Or.EqualTo(FeatConstants.Frequencies.Week)
                    .Or.Empty, feat.Name);

                if (feat.Name == FeatConstants.SaveBonus)
                    Assert.That(feat.Foci, Is.Not.Empty, character.Race.BaseRace);
            }
        }

        private void VerifyEquipment(Character character)
        {
            if (character.Feats.All.SelectMany(f => f.Foci).Contains(FeatConstants.Foci.UnarmedStrike) == false)
            {
                var feats = GetAllFeatsMessage(character.Feats.All);

                Assert.That(character.Equipment.PrimaryHand, Is.Not.Null, feats);
                Assert.That(character.Equipment.PrimaryHand.Name, Is.Not.Empty, feats);
                Assert.That(character.Equipment.PrimaryHand.ItemType, Is.EqualTo(ItemTypeConstants.Weapon), character.Equipment.PrimaryHand.Name);
                Assert.That(character.Equipment.PrimaryHand.Quantity, Is.Positive, character.Equipment.PrimaryHand.Name);
                Assert.That(character.Equipment.PrimaryHand.CanBeUsedAsWeaponOrArmor, Is.True, character.Equipment.PrimaryHand.Name);
                Assert.That(character.Equipment.PrimaryHand.CriticalMultiplier, Is.Not.Empty, character.Equipment.PrimaryHand.Name);
                Assert.That(character.Equipment.PrimaryHand.Damage, Is.Not.Empty, character.Equipment.PrimaryHand.Name);
                Assert.That(character.Equipment.PrimaryHand.DamageType, Is.Not.Empty, character.Equipment.PrimaryHand.Name);
                Assert.That(character.Equipment.PrimaryHand.Size, Is.EqualTo(character.Race.Size), character.Equipment.PrimaryHand.Name);
                Assert.That(character.Equipment.PrimaryHand.ThreatRange, Is.Not.Empty, character.Equipment.PrimaryHand.Name);

                if (character.Equipment.OffHand != null)
                {
                    Assert.That(character.Equipment.OffHand, Is.InstanceOf<Armor>().Or.InstanceOf<Weapon>(), feats);

                    if (character.Equipment.OffHand is Weapon)
                    {
                        var weapon = character.Equipment.OffHand as Weapon;
                        Assert.That(weapon, Is.Not.Null, feats);
                        Assert.That(weapon.Name, Is.Not.Empty, feats);
                        Assert.That(weapon.ItemType, Is.EqualTo(ItemTypeConstants.Weapon), character.Equipment.OffHand.Name);
                        Assert.That(weapon.Quantity, Is.Positive, character.Equipment.OffHand.Name);
                        Assert.That(weapon.CanBeUsedAsWeaponOrArmor, Is.True, character.Equipment.OffHand.Name);
                        Assert.That(weapon.CriticalMultiplier, Is.Not.Empty, character.Equipment.OffHand.Name);
                        Assert.That(weapon.Damage, Is.Not.Empty, character.Equipment.OffHand.Name);
                        Assert.That(weapon.DamageType, Is.Not.Empty, character.Equipment.OffHand.Name);
                        Assert.That(weapon.Size, Is.EqualTo(character.Race.Size), character.Equipment.OffHand.Name);
                        Assert.That(weapon.ThreatRange, Is.Not.Empty, character.Equipment.OffHand.Name);

                        if (weapon != character.Equipment.PrimaryHand)
                        {
                            Assert.That(weapon.Attributes, Is.All.Not.EqualTo(AttributeConstants.TwoHanded));
                            Assert.That(weapon.Attributes, Contains.Item(AttributeConstants.Melee));
                        }
                        else
                        {
                            Assert.That(weapon.Attributes, Contains.Item(AttributeConstants.TwoHanded));
                        }
                    }
                    else if (character.Equipment.OffHand is Armor)
                    {
                        var shield = character.Equipment.OffHand as Armor;
                        Assert.That(shield, Is.Not.Null, feats);
                        Assert.That(shield.Name, Is.Not.Empty, feats);
                        Assert.That(shield.ItemType, Is.EqualTo(ItemTypeConstants.Armor), character.Equipment.OffHand.Name);
                        Assert.That(shield.Quantity, Is.Positive, character.Equipment.OffHand.Name);
                        Assert.That(shield.CanBeUsedAsWeaponOrArmor, Is.True, character.Equipment.OffHand.Name);
                        Assert.That(shield.ArmorBonus, Is.Positive, character.Equipment.OffHand.Name);
                        Assert.That(shield.ArmorCheckPenalty, Is.Not.Positive, character.Equipment.OffHand.Name);
                        Assert.That(shield.MaxDexterityBonus, Is.Not.Negative, character.Equipment.OffHand.Name);
                        Assert.That(shield.Size, Is.EqualTo(character.Race.Size), character.Equipment.OffHand.Name);
                        Assert.That(shield.Attributes, Contains.Item(AttributeConstants.Shield));
                    }
                }

                if (character.Equipment.Armor != null)
                {
                    Assert.That(character.Equipment.Armor, Is.Not.Null, feats);
                    Assert.That(character.Equipment.Armor.Name, Is.Not.Empty, feats);
                    Assert.That(character.Equipment.Armor.ItemType, Is.EqualTo(ItemTypeConstants.Armor), character.Equipment.Armor.Name);
                    Assert.That(character.Equipment.Armor.Quantity, Is.Positive, character.Equipment.Armor.Name);
                    Assert.That(character.Equipment.Armor.CanBeUsedAsWeaponOrArmor, Is.True, character.Equipment.Armor.Name);
                    Assert.That(character.Equipment.Armor.ArmorBonus, Is.Positive, character.Equipment.Armor.Name);
                    Assert.That(character.Equipment.Armor.ArmorCheckPenalty, Is.Not.Positive, character.Equipment.Armor.Name);
                    Assert.That(character.Equipment.Armor.MaxDexterityBonus, Is.Not.Negative, character.Equipment.Armor.Name);
                    Assert.That(character.Equipment.Armor.Size, Is.EqualTo(character.Race.Size), character.Equipment.Armor.Name);
                    Assert.That(character.Equipment.Armor.Attributes, Is.All.Not.EqualTo(AttributeConstants.Shield));
                }
            }

            Assert.That(character.Equipment.Treasure, Is.Not.Null, character.Summary);
            Assert.That(character.Equipment.Treasure.Items, Is.Not.Null, character.Summary);
            Assert.That(character.Equipment.Treasure.Items, Is.All.Not.Null, character.Summary);

            foreach (var item in character.Equipment.Treasure.Items)
            {
                Assert.That(item.Name, Is.Not.Empty, character.Summary);
                Assert.That(item.Quantity, Is.Positive, item.Name);
            }
        }

        private void VerifyMagic(Character character)
        {
            Assert.That(character.Magic.Animal, Is.Not.Null, character.Summary);

            foreach (var spells in character.Magic.SpellsPerDay)
            {
                Assert.That(spells.Level, Is.Not.Negative, spells.Level.ToString());
                Assert.That(spells.Quantity, Is.Not.Negative, spells.Level.ToString());
                Assert.That(spells.Source, Is.Not.Empty, spells.Level.ToString());
            }

            foreach (var spell in character.Magic.KnownSpells)
            {
                Assert.That(spell.Level, Is.Not.Negative);
                Assert.That(spell.Metamagic, Is.Empty);
                Assert.That(spell.Name, Is.Not.Empty);
                Assert.That(spell.Source, Is.Not.Empty);
            }

            foreach (var spell in character.Magic.PreparedSpells)
            {
                Assert.That(spell.Level, Is.Not.Negative);
                Assert.That(spell.Metamagic, Is.Empty);
                Assert.That(spell.Name, Is.Not.Empty);
                Assert.That(spell.Source, Is.Not.Empty);

                var knownSpellNames = character.Magic.KnownSpells.Select(s => s.Name);
                Assert.That(knownSpellNames, Contains.Item(spell.Name), character.Class.Name);
            }
        }

        private void VerifyCombat(Character character)
        {
            Assert.That(character.Combat.BaseAttack.BaseBonus, Is.Not.Negative, character.Summary);
            Assert.That(character.Combat.BaseAttack.DexterityBonus, Is.EqualTo(character.Abilities[AbilityConstants.Dexterity].Bonus), character.Summary);
            Assert.That(character.Combat.BaseAttack.StrengthBonus, Is.EqualTo(character.Abilities[AbilityConstants.Strength].Bonus), character.Summary);
            Assert.That(character.Combat.BaseAttack.AllMeleeBonuses.Count, Is.InRange(1, 4), character.Summary);
            Assert.That(character.Combat.BaseAttack.AllRangedBonuses.Count, Is.InRange(1, 4), character.Summary);
            Assert.That(character.Combat.BaseAttack.AllRangedBonuses.Count, Is.EqualTo(character.Combat.BaseAttack.AllMeleeBonuses.Count()), character.Summary);
            Assert.That(character.Combat.BaseAttack.AllMeleeBonuses, Is.Unique, character.Summary);
            Assert.That(character.Combat.BaseAttack.AllMeleeBonuses, Is.Ordered.Descending, character.Summary);
            Assert.That(character.Combat.BaseAttack.AllRangedBonuses, Is.Unique, character.Summary);
            Assert.That(character.Combat.BaseAttack.AllRangedBonuses, Is.Ordered.Descending, character.Summary);
            Assert.That(character.Combat.BaseAttack.AllMeleeBonuses.First(), Is.EqualTo(character.Combat.BaseAttack.MeleeBonus));
            Assert.That(character.Combat.BaseAttack.AllRangedBonuses.First(), Is.EqualTo(character.Combat.BaseAttack.RangedBonus));
            Assert.That(character.Combat.BaseAttack.RacialModifier, Is.Not.Negative);

            if (character.Abilities[AbilityConstants.Dexterity].Bonus != character.Abilities[AbilityConstants.Strength].Bonus)
                Assert.That(character.Combat.BaseAttack.AllMeleeBonuses, Is.Not.EquivalentTo(character.Combat.BaseAttack.AllRangedBonuses), character.Summary);

            Assert.That(character.Combat.HitPoints, Is.AtLeast(character.Class.Level), character.Summary);
            Assert.That(character.Combat.ArmorClass.Full, Is.Positive, character.Summary);
            Assert.That(character.Combat.ArmorClass.FlatFooted, Is.Positive, character.Summary);
            Assert.That(character.Combat.ArmorClass.Touch, Is.Positive, character.Summary);
            Assert.That(character.Combat.AdjustedDexterityBonus, Is.AtMost(character.Abilities[AbilityConstants.Dexterity].Bonus), character.Summary);
            Assert.That(character.Combat.InitiativeBonus, Is.AtLeast(character.Combat.AdjustedDexterityBonus));

            Assert.That(character.Combat.SavingThrows.Reflex, Is.AtLeast(character.Abilities[AbilityConstants.Dexterity].Bonus));
            Assert.That(character.Combat.SavingThrows.Will, Is.AtLeast(character.Abilities[AbilityConstants.Wisdom].Bonus));
            Assert.That(character.Combat.SavingThrows.HasFortitudeSave, Is.EqualTo(character.Abilities.ContainsKey(AbilityConstants.Constitution)));

            if (character.Combat.SavingThrows.HasFortitudeSave)
                Assert.That(character.Combat.SavingThrows.Fortitude, Is.AtLeast(character.Abilities[AbilityConstants.Constitution].Bonus));
        }

        private string GetAllFeatsMessage(IEnumerable<Feat> feats)
        {
            var featsWithFoci = feats.Where(f => f.Foci.Any()).Select(f => $"{f.Name}: {string.Join(", ", f.Foci)}").OrderBy(f => f);
            return string.Join("; ", featsWithFoci);
        }
    }
}
