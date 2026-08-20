using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Generators.Creatures;
using DnDGen.CreatureGen.Templates;
using DnDGen.CreatureGen.Verifiers.Exceptions;
using NUnit.Framework;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDGen.CreatureGen.Tests.Unit.Templates
{
    [TestFixture]
    public class NoneApplicatorTests
    {
        private TemplateApplicator templateApplicator;

        [SetUp]
        public void Setup()
        {
            templateApplicator = new NoneApplicator();
        }

        [Test]
        public void ApplyTo_ThrowsException_WhenCreatureNotCompatible()
        {
            Assert.Pass("Creatures with no filters are always compatible for None template");
        }

        [TestCase(false, "subtype 1", ChallengeRatingConstants.CR1, "wrong alignment", "Alignment filter 'wrong alignment' is not valid")]
        [TestCase(false, "subtype 1", ChallengeRatingConstants.CR2, "original alignment", "CR filter 2 does not match creature CR 1")]
        [TestCase(false, "wrong subtype", ChallengeRatingConstants.CR1, "original alignment", "Type filter 'wrong subtype' is not valid")]
        [TestCase(true, "subtype 1", ChallengeRatingConstants.CR1, "original alignment", "",
            Ignore = "As Character doesn't affect already-generated creature compatiblity")]
        public void ApplyTo_ThrowsException_WhenCreatureNotCompatible_WithFilters(bool asCharacter, string type, string challengeRating, string alignment, string reason)
        {
            var creature = new CreatureBuilder()
                .WithTestValues()
                .WithCreatureType(CreatureConstants.Types.Humanoid)
                .AddSubtype("subtype 1")
                .WithChallengeRating(ChallengeRatingConstants.CR1)
                .WithAlignment("original alignment")
                .Build();

            var clone = new CreatureBuilder()
                .Clone(creature)
                .Build();

            var message = new StringBuilder();
            message.AppendLine("Invalid creature:");
            message.AppendLine($"\tReason: {reason}");
            message.AppendLine($"\tAs Character: {asCharacter}");
            message.AppendLine($"\tCreature: {creature.Name}");
            message.AppendLine($"\tTemplate: None");
            message.AppendLine($"\tType: {type}");
            message.AppendLine($"\tCR: {challengeRating}");
            message.AppendLine($"\tAlignment: {alignment}");

            var filters = new Filters
            {
                Type = type,
                ChallengeRating = challengeRating,
                Alignment = alignment
            };

            var func = () => templateApplicator.ApplyTo(clone, asCharacter, filters);
            Assert.That(func, Throws.InstanceOf<InvalidCreatureException>().With.Message.EqualTo(message.ToString()));
        }

        [Test]
        public void ApplyTo_ReturnsCreature_WithFilters()
        {
            var creature = new CreatureBuilder()
                .WithTestValues()
                .WithCreatureType(CreatureConstants.Types.Humanoid)
                .AddSubtype("subtype 1")
                .WithChallengeRating(ChallengeRatingConstants.CR1)
                .WithAlignment("original alignment")
                .Build();

            var clone = new CreatureBuilder()
                .Clone(creature)
                .Build();

            var filters = new Filters
            {
                Type = "subtype 1",
                ChallengeRating = ChallengeRatingConstants.CR1,
                Alignment = "original alignment"
            };

            var templatedCreature = templateApplicator.ApplyTo(clone, false, filters);
            Assert.That(templatedCreature, Is.EqualTo(clone));
        }

        [Test]
        public void ApplyTo_DoNotAlterCreature()
        {
            var creature = new CreatureBuilder()
                .WithTestValues()
                .Build();

            var clone = new CreatureBuilder()
                .Clone(creature)
                .Build();

            var templatedCreature = templateApplicator.ApplyTo(clone, false);
            Assert.That(templatedCreature, Is.EqualTo(clone));
            AssertCreatureUnchanged(templatedCreature, creature);
        }

        private static void AssertCreatureUnchanged(Creature actual, Creature expected)
        {
            Assert.That(actual.Abilities, Has.Count.EqualTo(expected.Abilities.Count));
            Assert.That(actual.Abilities.Keys, Is.EquivalentTo(expected.Abilities.Keys));

            foreach (var kvp in expected.Abilities)
            {
                Assert.That(actual.Abilities[kvp.Key].AdvancementAdjustment, Is.EqualTo(kvp.Value.AdvancementAdjustment));
                Assert.That(actual.Abilities[kvp.Key].BaseScore, Is.EqualTo(kvp.Value.BaseScore));
                Assert.That(actual.Abilities[kvp.Key].FullScore, Is.EqualTo(kvp.Value.FullScore));
                Assert.That(actual.Abilities[kvp.Key].HasScore, Is.EqualTo(kvp.Value.HasScore));
                Assert.That(actual.Abilities[kvp.Key].Modifier, Is.EqualTo(kvp.Value.Modifier));
                Assert.That(actual.Abilities[kvp.Key].Name, Is.EqualTo(kvp.Value.Name).And.EqualTo(kvp.Key));
                Assert.That(actual.Abilities[kvp.Key].RacialAdjustment, Is.EqualTo(kvp.Value.RacialAdjustment));
            }

            Assert.That(actual.Alignment, Is.Not.Null);
            Assert.That(actual.Alignment.Full, Is.EqualTo(expected.Alignment.Full));
            Assert.That(actual.Alignment.Goodness, Is.EqualTo(expected.Alignment.Goodness));
            Assert.That(actual.Alignment.Lawfulness, Is.EqualTo(expected.Alignment.Lawfulness));

            Assert.That(actual.ArmorClass, Is.Not.Null);
            Assert.That(actual.ArmorClass.ArmorBonus, Is.EqualTo(expected.ArmorClass.ArmorBonus));
            Assert.That(actual.ArmorClass.ArmorBonuses, Is.EqualTo(expected.ArmorClass.ArmorBonuses));
            Assert.That(actual.ArmorClass.Bonuses, Is.EqualTo(expected.ArmorClass.Bonuses));
            Assert.That(actual.ArmorClass.DeflectionBonus, Is.EqualTo(expected.ArmorClass.DeflectionBonus));
            Assert.That(actual.ArmorClass.DeflectionBonuses, Is.EqualTo(expected.ArmorClass.DeflectionBonuses));
            Assert.That(actual.ArmorClass.Dexterity, Is.EqualTo(actual.Abilities[AbilityConstants.Dexterity]));
            Assert.That(actual.ArmorClass.DexterityBonus, Is.EqualTo(expected.ArmorClass.DexterityBonus));
            Assert.That(actual.ArmorClass.DodgeBonus, Is.EqualTo(expected.ArmorClass.DodgeBonus));
            Assert.That(actual.ArmorClass.DodgeBonuses, Is.EqualTo(expected.ArmorClass.DodgeBonuses));
            Assert.That(actual.ArmorClass.FlatFootedBonus, Is.EqualTo(expected.ArmorClass.FlatFootedBonus));
            Assert.That(actual.ArmorClass.IsConditional, Is.EqualTo(expected.ArmorClass.IsConditional));
            Assert.That(actual.ArmorClass.MaxDexterityBonus, Is.EqualTo(expected.ArmorClass.MaxDexterityBonus));
            Assert.That(actual.ArmorClass.NaturalArmorBonus, Is.EqualTo(expected.ArmorClass.NaturalArmorBonus));
            Assert.That(actual.ArmorClass.NaturalArmorBonuses, Is.EqualTo(expected.ArmorClass.NaturalArmorBonuses));
            Assert.That(actual.ArmorClass.ShieldBonus, Is.EqualTo(expected.ArmorClass.ShieldBonus));
            Assert.That(actual.ArmorClass.ShieldBonuses, Is.EqualTo(expected.ArmorClass.ShieldBonuses));
            Assert.That(actual.ArmorClass.SizeModifier, Is.EqualTo(expected.ArmorClass.SizeModifier));
            Assert.That(actual.ArmorClass.TotalBonus, Is.EqualTo(expected.ArmorClass.TotalBonus));
            Assert.That(actual.ArmorClass.TouchBonus, Is.EqualTo(expected.ArmorClass.TouchBonus));

            Assert.That(actual.Attacks.Count(), Is.EqualTo(expected.Attacks.Count()));
            foreach (var attack in expected.Attacks)
            {
                var templatedAttack = actual.Attacks.FirstOrDefault(a => a.Name == attack.Name);
                Assert.That(templatedAttack, Is.Not.Null);
                Assert.That(templatedAttack.FullAttackBonuses, Is.EqualTo(attack.FullAttackBonuses));
                Assert.That(templatedAttack.AttackType, Is.EqualTo(attack.AttackType));
                Assert.That(templatedAttack.BaseAbility, Is.EqualTo(actual.Abilities[attack.BaseAbility.Name]));
                Assert.That(templatedAttack.BaseAttackBonus, Is.EqualTo(attack.BaseAttackBonus));
                Assert.That(templatedAttack.DamageSummary, Is.EqualTo(attack.DamageSummary));
                Assert.That(templatedAttack.DamageBonus, Is.EqualTo(attack.DamageBonus));
                Assert.That(templatedAttack.DamageEffect, Is.EqualTo(attack.DamageEffect));
                Assert.That(templatedAttack.Frequency.Quantity, Is.EqualTo(attack.Frequency.Quantity));
                Assert.That(templatedAttack.Frequency.TimePeriod, Is.EqualTo(attack.Frequency.TimePeriod));
                Assert.That(templatedAttack.IsMelee, Is.EqualTo(attack.IsMelee));
                Assert.That(templatedAttack.IsNatural, Is.EqualTo(attack.IsNatural));
                Assert.That(templatedAttack.IsPrimary, Is.EqualTo(attack.IsPrimary));
                Assert.That(templatedAttack.IsSpecial, Is.EqualTo(attack.IsSpecial));
                Assert.That(templatedAttack.Name, Is.EqualTo(attack.Name));
                Assert.That(templatedAttack.Save, Is.EqualTo(attack.Save));
                Assert.That(templatedAttack.AttackBonuses, Is.EqualTo(attack.AttackBonuses));
                Assert.That(templatedAttack.SizeModifier, Is.EqualTo(attack.SizeModifier));
                Assert.That(templatedAttack.TotalAttackBonus, Is.EqualTo(attack.TotalAttackBonus));
            }

            Assert.That(actual.BaseAttackBonus, Is.EqualTo(expected.BaseAttackBonus));
            Assert.That(actual.CanUseEquipment, Is.EqualTo(expected.CanUseEquipment));
            Assert.That(actual.CasterLevel, Is.EqualTo(expected.CasterLevel));
            Assert.That(actual.ChallengeRating, Is.EqualTo(expected.ChallengeRating));

            Assert.That(actual.Feats.Count(), Is.EqualTo(expected.Feats.Count()));
            foreach (var feat in expected.Feats)
            {
                var templatedFeat = actual.Feats.FirstOrDefault(a => a.Name == feat.Name);
                Assert.That(templatedFeat, Is.Not.Null);
                Assert.That(templatedFeat.CanBeTakenMultipleTimes, Is.EqualTo(feat.CanBeTakenMultipleTimes));
                Assert.That(templatedFeat.Foci, Is.EqualTo(feat.Foci));
                Assert.That(templatedFeat.Frequency.Quantity, Is.EqualTo(feat.Frequency.Quantity));
                Assert.That(templatedFeat.Frequency.TimePeriod, Is.EqualTo(feat.Frequency.TimePeriod));
                Assert.That(templatedFeat.Name, Is.EqualTo(feat.Name));
                Assert.That(templatedFeat.Power, Is.EqualTo(feat.Power));
                Assert.That(templatedFeat.Save, Is.EqualTo(feat.Save));
            }

            Assert.That(actual.GrappleBonus, Is.EqualTo(expected.GrappleBonus));
            Assert.That(actual.HitPoints.Bonus, Is.EqualTo(expected.HitPoints.Bonus));
            Assert.That(actual.HitPoints.Constitution, Is.EqualTo(actual.Abilities[AbilityConstants.Constitution]));
            Assert.That(actual.HitPoints.DefaultRoll, Is.EqualTo(expected.HitPoints.DefaultRoll));
            Assert.That(actual.HitPoints.DefaultTotal, Is.EqualTo(expected.HitPoints.DefaultTotal));
            Assert.That(actual.HitPoints.HitDiceQuantity, Is.EqualTo(expected.HitPoints.HitDiceQuantity));
            Assert.That(actual.HitPoints.HitDice, Has.Count.EqualTo(expected.HitPoints.HitDice.Count));

            for (var i = 0; i < expected.HitPoints.HitDice.Count; i++)
            {
                Assert.That(actual.HitPoints.HitDice[i].Quantity, Is.EqualTo(expected.HitPoints.HitDice[i].Quantity));
                Assert.That(actual.HitPoints.HitDice[i].HitDie, Is.EqualTo(expected.HitPoints.HitDice[i].HitDie));
            }

            Assert.That(actual.HitPoints.RoundedHitDiceQuantity, Is.EqualTo(expected.HitPoints.RoundedHitDiceQuantity));
            Assert.That(actual.HitPoints.Total, Is.EqualTo(expected.HitPoints.Total));

            Assert.That(actual.TotalInitiativeBonus, Is.EqualTo(expected.TotalInitiativeBonus));
            Assert.That(actual.LevelAdjustment, Is.EqualTo(expected.LevelAdjustment));
            Assert.That(actual.Name, Is.EqualTo(expected.Name));
            Assert.That(actual.NumberOfHands, Is.EqualTo(expected.NumberOfHands));

            Assert.That(actual.Reach.Description, Is.EqualTo(expected.Reach.Description));
            Assert.That(actual.Reach.Unit, Is.EqualTo(expected.Reach.Unit));
            Assert.That(actual.Reach.Value, Is.EqualTo(expected.Reach.Value));

            Assert.That(actual.Saves, Has.Count.EqualTo(expected.Saves.Count));
            Assert.That(actual.Saves.Keys, Is.EquivalentTo(expected.Saves.Keys));

            foreach (var kvp in expected.Saves)
            {
                Assert.That(actual.Saves[kvp.Key].BaseAbility, Is.EqualTo(actual.Abilities[kvp.Value.BaseAbility.Name]));
                Assert.That(actual.Saves[kvp.Key].BaseValue, Is.EqualTo(kvp.Value.BaseValue));
                Assert.That(actual.Saves[kvp.Key].Bonus, Is.EqualTo(kvp.Value.Bonus));
                Assert.That(actual.Saves[kvp.Key].Bonuses, Is.EqualTo(kvp.Value.Bonuses));
                Assert.That(actual.Saves[kvp.Key].HasSave, Is.EqualTo(kvp.Value.HasSave));
                Assert.That(actual.Saves[kvp.Key].IsConditional, Is.EqualTo(kvp.Value.IsConditional));
                Assert.That(actual.Saves[kvp.Key].TotalBonus, Is.EqualTo(kvp.Value.TotalBonus));
            }

            Assert.That(actual.Size, Is.EqualTo(expected.Size));
            Assert.That(actual.Skills.Count(), Is.EqualTo(expected.Skills.Count()));
            foreach (var skill in expected.Skills)
            {
                var templatedSkill = actual.Skills.FirstOrDefault(a => a.Name == skill.Name);
                Assert.That(templatedSkill, Is.Not.Null);
                Assert.That(templatedSkill.ArmorCheckPenalty, Is.EqualTo(skill.ArmorCheckPenalty));
                Assert.That(templatedSkill.BaseAbility, Is.EqualTo(actual.Abilities[skill.BaseAbility.Name]));
                Assert.That(templatedSkill.Bonus, Is.EqualTo(skill.Bonus));
                Assert.That(templatedSkill.Bonuses, Is.EqualTo(skill.Bonuses));
                Assert.That(templatedSkill.CircumstantialBonus, Is.EqualTo(skill.CircumstantialBonus));
                Assert.That(templatedSkill.ClassSkill, Is.EqualTo(skill.ClassSkill));
                Assert.That(templatedSkill.EffectiveRanks, Is.EqualTo(skill.EffectiveRanks));
                Assert.That(templatedSkill.Focus, Is.EqualTo(skill.Focus));
                Assert.That(templatedSkill.HasArmorCheckPenalty, Is.EqualTo(skill.HasArmorCheckPenalty));
                Assert.That(templatedSkill.Key, Is.EqualTo(skill.Key));
                Assert.That(templatedSkill.Name, Is.EqualTo(skill.Name));
                Assert.That(templatedSkill.QualifiesForSkillSynergy, Is.EqualTo(skill.QualifiesForSkillSynergy));
                Assert.That(templatedSkill.RankCap, Is.EqualTo(skill.RankCap));
                Assert.That(templatedSkill.Ranks, Is.EqualTo(skill.Ranks));
                Assert.That(templatedSkill.RanksMaxedOut, Is.EqualTo(skill.RanksMaxedOut));
                Assert.That(templatedSkill.TotalBonus, Is.EqualTo(skill.TotalBonus));
            }

            Assert.That(actual.Space.Description, Is.EqualTo(expected.Space.Description));
            Assert.That(actual.Space.Unit, Is.EqualTo(expected.Space.Unit));
            Assert.That(actual.Space.Value, Is.EqualTo(expected.Space.Value));

            Assert.That(actual.SpecialQualities.Count(), Is.EqualTo(expected.SpecialQualities.Count()));
            foreach (var feat in expected.SpecialQualities)
            {
                var templatedFeat = actual.SpecialQualities.FirstOrDefault(a => a.Name == feat.Name);
                Assert.That(templatedFeat, Is.Not.Null);
                Assert.That(templatedFeat.CanBeTakenMultipleTimes, Is.EqualTo(feat.CanBeTakenMultipleTimes));
                Assert.That(templatedFeat.Foci, Is.EqualTo(feat.Foci));
                Assert.That(templatedFeat.Frequency.Quantity, Is.EqualTo(feat.Frequency.Quantity));
                Assert.That(templatedFeat.Frequency.TimePeriod, Is.EqualTo(feat.Frequency.TimePeriod));
                Assert.That(templatedFeat.Name, Is.EqualTo(feat.Name));
                Assert.That(templatedFeat.Power, Is.EqualTo(feat.Power));
                Assert.That(templatedFeat.Save, Is.EqualTo(feat.Save));
            }

            Assert.That(actual.Speeds, Has.Count.EqualTo(expected.Speeds.Count));
            Assert.That(actual.Speeds.Keys, Is.EquivalentTo(expected.Speeds.Keys));

            foreach (var kvp in expected.Speeds)
            {
                Assert.That(actual.Speeds[kvp.Key].Description, Is.EqualTo(kvp.Value.Description));
                Assert.That(actual.Speeds[kvp.Key].Unit, Is.EqualTo(kvp.Value.Unit));
                Assert.That(actual.Speeds[kvp.Key].Value, Is.EqualTo(kvp.Value.Value));
            }

            Assert.That(actual.Summary, Is.EqualTo(expected.Summary));
            Assert.That(actual.Templates, Is.Empty);
            Assert.That(actual.Type.Name, Is.EqualTo(expected.Type.Name));
            Assert.That(actual.Type.SubTypes, Is.EquivalentTo(expected.Type.SubTypes));
        }

        [Test]
        public async Task ApplyToAsync_ThrowsException_WhenCreatureNotCompatible()
        {
            await Task.Run(() => Assert.Pass("Creatures with no filters are always compatible for None template"));
        }

        [TestCase(false, "subtype 1", ChallengeRatingConstants.CR1, "wrong alignment", "Alignment filter 'wrong alignment' is not valid")]
        [TestCase(false, "subtype 1", ChallengeRatingConstants.CR2, "original alignment", "CR filter 2 does not match creature CR 1")]
        [TestCase(false, "wrong subtype", ChallengeRatingConstants.CR1, "original alignment", "Type filter 'wrong subtype' is not valid")]
        [TestCase(true, "subtype 1", ChallengeRatingConstants.CR1, "original alignment", "",
            Ignore = "As Character doesn't affect already-generated creature compatiblity")]
        public async Task ApplyToAsync_ThrowsException_WhenCreatureNotCompatible_WithFilters(
            bool asCharacter,
            string type,
            string challengeRating,
            string alignment,
            string reason)
        {
            var creature = new CreatureBuilder()
                .WithTestValues()
                .WithCreatureType(CreatureConstants.Types.Humanoid)
                .AddSubtype("subtype 1")
                .WithChallengeRating(ChallengeRatingConstants.CR1)
                .WithAlignment("original alignment")
                .Build();

            var clone = new CreatureBuilder()
                .Clone(creature)
                .Build();

            var message = new StringBuilder();
            message.AppendLine("Invalid creature:");
            message.AppendLine($"\tReason: {reason}");
            message.AppendLine($"\tAs Character: {asCharacter}");
            message.AppendLine($"\tCreature: {creature.Name}");
            message.AppendLine($"\tTemplate: None");
            message.AppendLine($"\tType: {type}");
            message.AppendLine($"\tCR: {challengeRating}");
            message.AppendLine($"\tAlignment: {alignment}");

            var filters = new Filters
            {
                Type = type,
                ChallengeRating = challengeRating,
                Alignment = alignment
            };

            await Assert.ThatAsync(async () => await templateApplicator.ApplyToAsync(clone, asCharacter, filters),
                Throws.InstanceOf<InvalidCreatureException>().With.Message.EqualTo(message.ToString()));
        }

        [Test]
        public async Task ApplyToAsync_ReturnsCreature_WithFilters()
        {
            var creature = new CreatureBuilder()
                .WithTestValues()
                .WithCreatureType(CreatureConstants.Types.Humanoid)
                .AddSubtype("subtype 1")
                .WithChallengeRating(ChallengeRatingConstants.CR1)
                .WithAlignment("original alignment")
                .Build();

            var clone = new CreatureBuilder()
                .Clone(creature)
                .Build();

            var filters = new Filters
            {
                Type = "subtype 1",
                ChallengeRating = ChallengeRatingConstants.CR1,
                Alignment = "original alignment"
            };

            var templatedCreature = await templateApplicator.ApplyToAsync(clone, false, filters);
            Assert.That(templatedCreature, Is.EqualTo(clone));
        }

        [Test]
        public async Task ApplyToAsync_DoNotAlterCreature()
        {
            var creature = new CreatureBuilder()
                .WithTestValues()
                .Build();

            var clone = new CreatureBuilder()
                .Clone(creature)
                .Build();

            var templatedCreature = await templateApplicator.ApplyToAsync(clone, false);
            Assert.That(templatedCreature, Is.EqualTo(clone));
            AssertCreatureUnchanged(templatedCreature, creature);
        }

        [Test]
        public void IsCompatible_ReturnsTrue()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void IsCompatible_WithAlignment_ReturnsTrue_WhenPrototypeContainsAlignmentFilter()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void IsCompatible_WithAlignment_ReturnsFalse_WhenPrototypeDoesNotContainAlignmentFilter()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void IsCompatible_WithChallengeRating_ReturnsTrue_WhenChallengeRatingMatches()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void IsCompatible_WithChallengeRating_ReturnsFalse_WhenChallengeRatingDoesNotMatch()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void IsCompatible_WithType_ReturnsTrue_WhenPrototypeContainsTypeFilter()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void IsCompatible_WithType_ReturnsFalse_WhenPrototypeDoesNotContainTypeFilter()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void IsCompatible_WithAllFilters_ReturnsTrue()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void IsCompatible_WithAllFilters_ReturnsFalse_BecausePrototype()
        {
            Assert.Pass("All prototypes are valid for the None template applicator, so returning False is not a valid use case");
        }

        [Test]
        public void IsCompatible_WithAllFilters_ReturnsFalse_BecauseAlignment()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void IsCompatible_WithAllFilters_ReturnsFalse_BecauseChallengeRating()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void IsCompatible_WithAllFilters_ReturnsFalse_BecauseType()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void ApplyTo_Prototype_ThrowsException_WhenCreatureNotCompatible()
        {
            Assert.Pass("Creature prototypes with no filters are always compatible for None template");
        }

        [TestCase(false, "subtype 1", ChallengeRatingConstants.CR1, "wrong alignment", "Alignment filter 'wrong alignment' is not valid")]
        [TestCase(false, "subtype 1", ChallengeRatingConstants.CR2, "original alignment", "CR filter 2 does not match creature CR 1")]
        [TestCase(false, "wrong subtype", ChallengeRatingConstants.CR1, "original alignment", "Type filter 'wrong subtype' is not valid")]
        [TestCase(true, "subtype 1", ChallengeRatingConstants.CR1, "original alignment", "",
            Ignore = "As Character doesn't affect already-generated creature compatiblity")]
        public void ApplyTo_Prototype_ThrowsException_WhenCreatureNotCompatible_WithFilters(bool asCharacter, string type, string challengeRating, string alignment, string reason)
        {
            Assert.Fail("update for prototype");
            var creature = new CreatureBuilder()
                .WithTestValues()
                .WithCreatureType(CreatureConstants.Types.Humanoid)
                .AddSubtype("subtype 1")
                .WithChallengeRating(ChallengeRatingConstants.CR1)
                .WithAlignment("original alignment")
                .Build();

            var clone = new CreatureBuilder()
                .Clone(creature)
                .Build();

            var message = new StringBuilder();
            message.AppendLine("Invalid creature:");
            message.AppendLine($"\tReason: {reason}");
            message.AppendLine($"\tAs Character: {asCharacter}");
            message.AppendLine($"\tCreature: {creature.Name}");
            message.AppendLine($"\tTemplate: None");
            message.AppendLine($"\tType: {type}");
            message.AppendLine($"\tCR: {challengeRating}");
            message.AppendLine($"\tAlignment: {alignment}");

            var filters = new Filters
            {
                Type = type,
                ChallengeRating = challengeRating,
                Alignment = alignment
            };

            var func = () => templateApplicator.ApplyTo(clone, asCharacter, filters);
            Assert.That(func, Throws.InstanceOf<InvalidCreatureException>().With.Message.EqualTo(message.ToString()));
        }

        [Test]
        public void ApplyTo_Prototype_ReturnsPrototype()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void ApplyTo_PrototypeWithAlignment_ReturnsPrototype_WithUpdatedAlignment()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void ApplyTo_Prototype_ReturnsPrototype_WithAdditionalTemplates()
        {
            Assert.Fail("not yet written");
        }
    }
}
