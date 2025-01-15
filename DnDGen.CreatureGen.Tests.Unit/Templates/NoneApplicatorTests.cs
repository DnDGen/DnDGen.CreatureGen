using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Alignments;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Generators.Creatures;
using DnDGen.CreatureGen.Selectors.Collections;
using DnDGen.CreatureGen.Selectors.Selections;
using DnDGen.CreatureGen.Tables;
using DnDGen.CreatureGen.Templates;
using DnDGen.CreatureGen.Tests.Unit.TestCaseSources;
using DnDGen.CreatureGen.Verifiers.Exceptions;
using DnDGen.Infrastructure.Selectors.Collections;
using Moq;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDGen.CreatureGen.Tests.Unit.Templates
{
    [TestFixture]
    public class NoneApplicatorTests
    {
        private TemplateApplicator templateApplicator;
        private Mock<ICollectionSelector> mockCollectionSelector;
        private Mock<ICreatureDataSelector> mockCreatureDataSelector;
        private Mock<IAdjustmentsSelector> mockAdjustmentSelector;
        private Mock<ICreaturePrototypeFactory> mockPrototypeFactory;

        [SetUp]
        public void Setup()
        {
            mockCollectionSelector = new Mock<ICollectionSelector>();
            mockCreatureDataSelector = new Mock<ICreatureDataSelector>();
            mockAdjustmentSelector = new Mock<IAdjustmentsSelector>();
            mockPrototypeFactory = new Mock<ICreaturePrototypeFactory>();

            templateApplicator = new NoneApplicator(
                mockCollectionSelector.Object,
                mockCreatureDataSelector.Object,
                mockAdjustmentSelector.Object,
                mockPrototypeFactory.Object);
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

            var filters = new Filters();
            filters.Type = type;
            filters.ChallengeRating = challengeRating;
            filters.Alignment = alignment;

            Assert.That(() => templateApplicator.ApplyTo(clone, asCharacter, filters),
                Throws.InstanceOf<InvalidCreatureException>().With.Message.EqualTo(message.ToString()));
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

            var filters = new Filters();
            filters.Type = "subtype 1";
            filters.ChallengeRating = ChallengeRatingConstants.CR1;
            filters.Alignment = "original alignment";

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
            Assert.That(templatedCreature.Abilities, Has.Count.EqualTo(creature.Abilities.Count));
            Assert.That(templatedCreature.Abilities.Keys, Is.EquivalentTo(creature.Abilities.Keys));

            foreach (var kvp in creature.Abilities)
            {
                Assert.That(templatedCreature.Abilities[kvp.Key].AdvancementAdjustment, Is.EqualTo(kvp.Value.AdvancementAdjustment));
                Assert.That(templatedCreature.Abilities[kvp.Key].BaseScore, Is.EqualTo(kvp.Value.BaseScore));
                Assert.That(templatedCreature.Abilities[kvp.Key].FullScore, Is.EqualTo(kvp.Value.FullScore));
                Assert.That(templatedCreature.Abilities[kvp.Key].HasScore, Is.EqualTo(kvp.Value.HasScore));
                Assert.That(templatedCreature.Abilities[kvp.Key].Modifier, Is.EqualTo(kvp.Value.Modifier));
                Assert.That(templatedCreature.Abilities[kvp.Key].Name, Is.EqualTo(kvp.Value.Name).And.EqualTo(kvp.Key));
                Assert.That(templatedCreature.Abilities[kvp.Key].RacialAdjustment, Is.EqualTo(kvp.Value.RacialAdjustment));
            }

            Assert.That(templatedCreature.Alignment, Is.Not.Null);
            Assert.That(templatedCreature.Alignment.Full, Is.EqualTo(creature.Alignment.Full));
            Assert.That(templatedCreature.Alignment.Goodness, Is.EqualTo(creature.Alignment.Goodness));
            Assert.That(templatedCreature.Alignment.Lawfulness, Is.EqualTo(creature.Alignment.Lawfulness));

            Assert.That(templatedCreature.ArmorClass, Is.Not.Null);
            Assert.That(templatedCreature.ArmorClass.ArmorBonus, Is.EqualTo(creature.ArmorClass.ArmorBonus));
            Assert.That(templatedCreature.ArmorClass.ArmorBonuses, Is.EqualTo(creature.ArmorClass.ArmorBonuses));
            Assert.That(templatedCreature.ArmorClass.Bonuses, Is.EqualTo(creature.ArmorClass.Bonuses));
            Assert.That(templatedCreature.ArmorClass.DeflectionBonus, Is.EqualTo(creature.ArmorClass.DeflectionBonus));
            Assert.That(templatedCreature.ArmorClass.DeflectionBonuses, Is.EqualTo(creature.ArmorClass.DeflectionBonuses));
            Assert.That(templatedCreature.ArmorClass.Dexterity, Is.EqualTo(templatedCreature.Abilities[AbilityConstants.Dexterity]));
            Assert.That(templatedCreature.ArmorClass.DexterityBonus, Is.EqualTo(creature.ArmorClass.DexterityBonus));
            Assert.That(templatedCreature.ArmorClass.DodgeBonus, Is.EqualTo(creature.ArmorClass.DodgeBonus));
            Assert.That(templatedCreature.ArmorClass.DodgeBonuses, Is.EqualTo(creature.ArmorClass.DodgeBonuses));
            Assert.That(templatedCreature.ArmorClass.FlatFootedBonus, Is.EqualTo(creature.ArmorClass.FlatFootedBonus));
            Assert.That(templatedCreature.ArmorClass.IsConditional, Is.EqualTo(creature.ArmorClass.IsConditional));
            Assert.That(templatedCreature.ArmorClass.MaxDexterityBonus, Is.EqualTo(creature.ArmorClass.MaxDexterityBonus));
            Assert.That(templatedCreature.ArmorClass.NaturalArmorBonus, Is.EqualTo(creature.ArmorClass.NaturalArmorBonus));
            Assert.That(templatedCreature.ArmorClass.NaturalArmorBonuses, Is.EqualTo(creature.ArmorClass.NaturalArmorBonuses));
            Assert.That(templatedCreature.ArmorClass.ShieldBonus, Is.EqualTo(creature.ArmorClass.ShieldBonus));
            Assert.That(templatedCreature.ArmorClass.ShieldBonuses, Is.EqualTo(creature.ArmorClass.ShieldBonuses));
            Assert.That(templatedCreature.ArmorClass.SizeModifier, Is.EqualTo(creature.ArmorClass.SizeModifier));
            Assert.That(templatedCreature.ArmorClass.TotalBonus, Is.EqualTo(creature.ArmorClass.TotalBonus));
            Assert.That(templatedCreature.ArmorClass.TouchBonus, Is.EqualTo(creature.ArmorClass.TouchBonus));

            Assert.That(templatedCreature.Attacks.Count(), Is.EqualTo(creature.Attacks.Count()));
            foreach (var attack in creature.Attacks)
            {
                var templatedAttack = templatedCreature.Attacks.FirstOrDefault(a => a.Name == attack.Name);
                Assert.That(templatedAttack, Is.Not.Null);
                Assert.That(templatedAttack.FullAttackBonuses, Is.EqualTo(attack.FullAttackBonuses));
                Assert.That(templatedAttack.AttackType, Is.EqualTo(attack.AttackType));
                Assert.That(templatedAttack.BaseAbility, Is.EqualTo(templatedCreature.Abilities[attack.BaseAbility.Name]));
                Assert.That(templatedAttack.BaseAttackBonus, Is.EqualTo(attack.BaseAttackBonus));
                Assert.That(templatedAttack.DamageDescription, Is.EqualTo(attack.DamageDescription));
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

            Assert.That(templatedCreature.BaseAttackBonus, Is.EqualTo(creature.BaseAttackBonus));
            Assert.That(templatedCreature.CanUseEquipment, Is.EqualTo(creature.CanUseEquipment));
            Assert.That(templatedCreature.CasterLevel, Is.EqualTo(creature.CasterLevel));
            Assert.That(templatedCreature.ChallengeRating, Is.EqualTo(creature.ChallengeRating));

            Assert.That(templatedCreature.Feats.Count(), Is.EqualTo(creature.Feats.Count()));
            foreach (var feat in creature.Feats)
            {
                var templatedFeat = templatedCreature.Feats.FirstOrDefault(a => a.Name == feat.Name);
                Assert.That(templatedFeat, Is.Not.Null);
                Assert.That(templatedFeat.CanBeTakenMultipleTimes, Is.EqualTo(feat.CanBeTakenMultipleTimes));
                Assert.That(templatedFeat.Foci, Is.EqualTo(feat.Foci));
                Assert.That(templatedFeat.Frequency.Quantity, Is.EqualTo(feat.Frequency.Quantity));
                Assert.That(templatedFeat.Frequency.TimePeriod, Is.EqualTo(feat.Frequency.TimePeriod));
                Assert.That(templatedFeat.Name, Is.EqualTo(feat.Name));
                Assert.That(templatedFeat.Power, Is.EqualTo(feat.Power));
                Assert.That(templatedFeat.Save, Is.EqualTo(feat.Save));
            }

            Assert.That(templatedCreature.GrappleBonus, Is.EqualTo(creature.GrappleBonus));
            Assert.That(templatedCreature.HitPoints.Bonus, Is.EqualTo(creature.HitPoints.Bonus));
            Assert.That(templatedCreature.HitPoints.Constitution, Is.EqualTo(templatedCreature.Abilities[AbilityConstants.Constitution]));
            Assert.That(templatedCreature.HitPoints.DefaultRoll, Is.EqualTo(creature.HitPoints.DefaultRoll));
            Assert.That(templatedCreature.HitPoints.DefaultTotal, Is.EqualTo(creature.HitPoints.DefaultTotal));
            Assert.That(templatedCreature.HitPoints.HitDiceQuantity, Is.EqualTo(creature.HitPoints.HitDiceQuantity));
            Assert.That(templatedCreature.HitPoints.HitDice, Has.Count.EqualTo(creature.HitPoints.HitDice.Count));

            for (var i = 0; i < templatedCreature.HitPoints.HitDice.Count; i++)
            {
                Assert.That(templatedCreature.HitPoints.HitDice[i].Quantity, Is.EqualTo(creature.HitPoints.HitDice[i].Quantity));
                Assert.That(templatedCreature.HitPoints.HitDice[i].HitDie, Is.EqualTo(creature.HitPoints.HitDice[i].HitDie));
            }

            Assert.That(templatedCreature.HitPoints.RoundedHitDiceQuantity, Is.EqualTo(creature.HitPoints.RoundedHitDiceQuantity));
            Assert.That(templatedCreature.HitPoints.Total, Is.EqualTo(creature.HitPoints.Total));

            Assert.That(templatedCreature.TotalInitiativeBonus, Is.EqualTo(creature.TotalInitiativeBonus));
            Assert.That(templatedCreature.LevelAdjustment, Is.EqualTo(creature.LevelAdjustment));
            Assert.That(templatedCreature.Name, Is.EqualTo(creature.Name));
            Assert.That(templatedCreature.NumberOfHands, Is.EqualTo(creature.NumberOfHands));

            Assert.That(templatedCreature.Reach.Description, Is.EqualTo(creature.Reach.Description));
            Assert.That(templatedCreature.Reach.Unit, Is.EqualTo(creature.Reach.Unit));
            Assert.That(templatedCreature.Reach.Value, Is.EqualTo(creature.Reach.Value));

            Assert.That(templatedCreature.Saves, Has.Count.EqualTo(creature.Saves.Count));
            Assert.That(templatedCreature.Saves.Keys, Is.EquivalentTo(creature.Saves.Keys));

            foreach (var kvp in creature.Saves)
            {
                Assert.That(templatedCreature.Saves[kvp.Key].BaseAbility, Is.EqualTo(templatedCreature.Abilities[kvp.Value.BaseAbility.Name]));
                Assert.That(templatedCreature.Saves[kvp.Key].BaseValue, Is.EqualTo(kvp.Value.BaseValue));
                Assert.That(templatedCreature.Saves[kvp.Key].Bonus, Is.EqualTo(kvp.Value.Bonus));
                Assert.That(templatedCreature.Saves[kvp.Key].Bonuses, Is.EqualTo(kvp.Value.Bonuses));
                Assert.That(templatedCreature.Saves[kvp.Key].HasSave, Is.EqualTo(kvp.Value.HasSave));
                Assert.That(templatedCreature.Saves[kvp.Key].IsConditional, Is.EqualTo(kvp.Value.IsConditional));
                Assert.That(templatedCreature.Saves[kvp.Key].TotalBonus, Is.EqualTo(kvp.Value.TotalBonus));
            }

            Assert.That(templatedCreature.Size, Is.EqualTo(creature.Size));
            Assert.That(templatedCreature.Skills.Count(), Is.EqualTo(creature.Skills.Count()));
            foreach (var skill in creature.Skills)
            {
                var templatedSkill = templatedCreature.Skills.FirstOrDefault(a => a.Name == skill.Name);
                Assert.That(templatedSkill, Is.Not.Null);
                Assert.That(templatedSkill.ArmorCheckPenalty, Is.EqualTo(skill.ArmorCheckPenalty));
                Assert.That(templatedSkill.BaseAbility, Is.EqualTo(templatedCreature.Abilities[skill.BaseAbility.Name]));
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

            Assert.That(templatedCreature.Space.Description, Is.EqualTo(creature.Space.Description));
            Assert.That(templatedCreature.Space.Unit, Is.EqualTo(creature.Space.Unit));
            Assert.That(templatedCreature.Space.Value, Is.EqualTo(creature.Space.Value));

            Assert.That(templatedCreature.SpecialQualities.Count(), Is.EqualTo(creature.SpecialQualities.Count()));
            foreach (var feat in creature.SpecialQualities)
            {
                var templatedFeat = templatedCreature.SpecialQualities.FirstOrDefault(a => a.Name == feat.Name);
                Assert.That(templatedFeat, Is.Not.Null);
                Assert.That(templatedFeat.CanBeTakenMultipleTimes, Is.EqualTo(feat.CanBeTakenMultipleTimes));
                Assert.That(templatedFeat.Foci, Is.EqualTo(feat.Foci));
                Assert.That(templatedFeat.Frequency.Quantity, Is.EqualTo(feat.Frequency.Quantity));
                Assert.That(templatedFeat.Frequency.TimePeriod, Is.EqualTo(feat.Frequency.TimePeriod));
                Assert.That(templatedFeat.Name, Is.EqualTo(feat.Name));
                Assert.That(templatedFeat.Power, Is.EqualTo(feat.Power));
                Assert.That(templatedFeat.Save, Is.EqualTo(feat.Save));
            }

            Assert.That(templatedCreature.Speeds, Has.Count.EqualTo(creature.Speeds.Count));
            Assert.That(templatedCreature.Speeds.Keys, Is.EquivalentTo(creature.Speeds.Keys));

            foreach (var kvp in creature.Speeds)
            {
                Assert.That(templatedCreature.Speeds[kvp.Key].Description, Is.EqualTo(kvp.Value.Description));
                Assert.That(templatedCreature.Speeds[kvp.Key].Unit, Is.EqualTo(kvp.Value.Unit));
                Assert.That(templatedCreature.Speeds[kvp.Key].Value, Is.EqualTo(kvp.Value.Value));
            }

            Assert.That(templatedCreature.Summary, Is.EqualTo(creature.Summary));
            Assert.That(templatedCreature.Templates, Is.Empty);
            Assert.That(templatedCreature.Type.Name, Is.EqualTo(creature.Type.Name));
            Assert.That(templatedCreature.Type.SubTypes, Is.EquivalentTo(creature.Type.SubTypes));
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
        public async Task ApplyToAsync_ThrowsException_WhenCreatureNotCompatible_WithFilters(bool asCharacter, string type, string challengeRating, string alignment, string reason)
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

            var filters = new Filters();
            filters.Type = type;
            filters.ChallengeRating = challengeRating;
            filters.Alignment = alignment;

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

            var filters = new Filters();
            filters.Type = "subtype 1";
            filters.ChallengeRating = ChallengeRatingConstants.CR1;
            filters.Alignment = "original alignment";

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
            Assert.That(templatedCreature.Abilities, Has.Count.EqualTo(creature.Abilities.Count));
            Assert.That(templatedCreature.Abilities.Keys, Is.EquivalentTo(creature.Abilities.Keys));

            foreach (var kvp in creature.Abilities)
            {
                Assert.That(templatedCreature.Abilities[kvp.Key].AdvancementAdjustment, Is.EqualTo(kvp.Value.AdvancementAdjustment));
                Assert.That(templatedCreature.Abilities[kvp.Key].BaseScore, Is.EqualTo(kvp.Value.BaseScore));
                Assert.That(templatedCreature.Abilities[kvp.Key].FullScore, Is.EqualTo(kvp.Value.FullScore));
                Assert.That(templatedCreature.Abilities[kvp.Key].HasScore, Is.EqualTo(kvp.Value.HasScore));
                Assert.That(templatedCreature.Abilities[kvp.Key].Modifier, Is.EqualTo(kvp.Value.Modifier));
                Assert.That(templatedCreature.Abilities[kvp.Key].Name, Is.EqualTo(kvp.Value.Name).And.EqualTo(kvp.Key));
                Assert.That(templatedCreature.Abilities[kvp.Key].RacialAdjustment, Is.EqualTo(kvp.Value.RacialAdjustment));
            }

            Assert.That(templatedCreature.Alignment, Is.Not.Null);
            Assert.That(templatedCreature.Alignment.Full, Is.EqualTo(creature.Alignment.Full));
            Assert.That(templatedCreature.Alignment.Goodness, Is.EqualTo(creature.Alignment.Goodness));
            Assert.That(templatedCreature.Alignment.Lawfulness, Is.EqualTo(creature.Alignment.Lawfulness));

            Assert.That(templatedCreature.ArmorClass, Is.Not.Null);
            Assert.That(templatedCreature.ArmorClass.ArmorBonus, Is.EqualTo(creature.ArmorClass.ArmorBonus));
            Assert.That(templatedCreature.ArmorClass.ArmorBonuses, Is.EqualTo(creature.ArmorClass.ArmorBonuses));
            Assert.That(templatedCreature.ArmorClass.Bonuses, Is.EqualTo(creature.ArmorClass.Bonuses));
            Assert.That(templatedCreature.ArmorClass.DeflectionBonus, Is.EqualTo(creature.ArmorClass.DeflectionBonus));
            Assert.That(templatedCreature.ArmorClass.DeflectionBonuses, Is.EqualTo(creature.ArmorClass.DeflectionBonuses));
            Assert.That(templatedCreature.ArmorClass.Dexterity, Is.EqualTo(templatedCreature.Abilities[AbilityConstants.Dexterity]));
            Assert.That(templatedCreature.ArmorClass.DexterityBonus, Is.EqualTo(creature.ArmorClass.DexterityBonus));
            Assert.That(templatedCreature.ArmorClass.DodgeBonus, Is.EqualTo(creature.ArmorClass.DodgeBonus));
            Assert.That(templatedCreature.ArmorClass.DodgeBonuses, Is.EqualTo(creature.ArmorClass.DodgeBonuses));
            Assert.That(templatedCreature.ArmorClass.FlatFootedBonus, Is.EqualTo(creature.ArmorClass.FlatFootedBonus));
            Assert.That(templatedCreature.ArmorClass.IsConditional, Is.EqualTo(creature.ArmorClass.IsConditional));
            Assert.That(templatedCreature.ArmorClass.MaxDexterityBonus, Is.EqualTo(creature.ArmorClass.MaxDexterityBonus));
            Assert.That(templatedCreature.ArmorClass.NaturalArmorBonus, Is.EqualTo(creature.ArmorClass.NaturalArmorBonus));
            Assert.That(templatedCreature.ArmorClass.NaturalArmorBonuses, Is.EqualTo(creature.ArmorClass.NaturalArmorBonuses));
            Assert.That(templatedCreature.ArmorClass.ShieldBonus, Is.EqualTo(creature.ArmorClass.ShieldBonus));
            Assert.That(templatedCreature.ArmorClass.ShieldBonuses, Is.EqualTo(creature.ArmorClass.ShieldBonuses));
            Assert.That(templatedCreature.ArmorClass.SizeModifier, Is.EqualTo(creature.ArmorClass.SizeModifier));
            Assert.That(templatedCreature.ArmorClass.TotalBonus, Is.EqualTo(creature.ArmorClass.TotalBonus));
            Assert.That(templatedCreature.ArmorClass.TouchBonus, Is.EqualTo(creature.ArmorClass.TouchBonus));

            Assert.That(templatedCreature.Attacks.Count(), Is.EqualTo(creature.Attacks.Count()));
            foreach (var attack in creature.Attacks)
            {
                var templatedAttack = templatedCreature.Attacks.FirstOrDefault(a => a.Name == attack.Name);
                Assert.That(templatedAttack, Is.Not.Null);
                Assert.That(templatedAttack.FullAttackBonuses, Is.EqualTo(attack.FullAttackBonuses));
                Assert.That(templatedAttack.AttackType, Is.EqualTo(attack.AttackType));
                Assert.That(templatedAttack.BaseAbility, Is.EqualTo(templatedCreature.Abilities[attack.BaseAbility.Name]));
                Assert.That(templatedAttack.BaseAttackBonus, Is.EqualTo(attack.BaseAttackBonus));
                Assert.That(templatedAttack.DamageDescription, Is.EqualTo(attack.DamageDescription));
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

            Assert.That(templatedCreature.BaseAttackBonus, Is.EqualTo(creature.BaseAttackBonus));
            Assert.That(templatedCreature.CanUseEquipment, Is.EqualTo(creature.CanUseEquipment));
            Assert.That(templatedCreature.CasterLevel, Is.EqualTo(creature.CasterLevel));
            Assert.That(templatedCreature.ChallengeRating, Is.EqualTo(creature.ChallengeRating));

            Assert.That(templatedCreature.Feats.Count(), Is.EqualTo(creature.Feats.Count()));
            foreach (var feat in creature.Feats)
            {
                var templatedFeat = templatedCreature.Feats.FirstOrDefault(a => a.Name == feat.Name);
                Assert.That(templatedFeat, Is.Not.Null);
                Assert.That(templatedFeat.CanBeTakenMultipleTimes, Is.EqualTo(feat.CanBeTakenMultipleTimes));
                Assert.That(templatedFeat.Foci, Is.EqualTo(feat.Foci));
                Assert.That(templatedFeat.Frequency.Quantity, Is.EqualTo(feat.Frequency.Quantity));
                Assert.That(templatedFeat.Frequency.TimePeriod, Is.EqualTo(feat.Frequency.TimePeriod));
                Assert.That(templatedFeat.Name, Is.EqualTo(feat.Name));
                Assert.That(templatedFeat.Power, Is.EqualTo(feat.Power));
                Assert.That(templatedFeat.Save, Is.EqualTo(feat.Save));
            }

            Assert.That(templatedCreature.GrappleBonus, Is.EqualTo(creature.GrappleBonus));
            Assert.That(templatedCreature.HitPoints.Bonus, Is.EqualTo(creature.HitPoints.Bonus));
            Assert.That(templatedCreature.HitPoints.Constitution, Is.EqualTo(templatedCreature.Abilities[AbilityConstants.Constitution]));
            Assert.That(templatedCreature.HitPoints.DefaultRoll, Is.EqualTo(creature.HitPoints.DefaultRoll));
            Assert.That(templatedCreature.HitPoints.DefaultTotal, Is.EqualTo(creature.HitPoints.DefaultTotal));
            Assert.That(templatedCreature.HitPoints.HitDiceQuantity, Is.EqualTo(creature.HitPoints.HitDiceQuantity));
            Assert.That(templatedCreature.HitPoints.HitDice, Has.Count.EqualTo(creature.HitPoints.HitDice.Count));

            for (var i = 0; i < templatedCreature.HitPoints.HitDice.Count; i++)
            {
                Assert.That(templatedCreature.HitPoints.HitDice[i].Quantity, Is.EqualTo(creature.HitPoints.HitDice[i].Quantity));
                Assert.That(templatedCreature.HitPoints.HitDice[i].HitDie, Is.EqualTo(creature.HitPoints.HitDice[i].HitDie));
            }

            Assert.That(templatedCreature.HitPoints.RoundedHitDiceQuantity, Is.EqualTo(creature.HitPoints.RoundedHitDiceQuantity));
            Assert.That(templatedCreature.HitPoints.Total, Is.EqualTo(creature.HitPoints.Total));

            Assert.That(templatedCreature.InitiativeBonus, Is.EqualTo(creature.InitiativeBonus));
            Assert.That(templatedCreature.TotalInitiativeBonus, Is.EqualTo(creature.TotalInitiativeBonus));
            Assert.That(templatedCreature.LevelAdjustment, Is.EqualTo(creature.LevelAdjustment));
            Assert.That(templatedCreature.Name, Is.EqualTo(creature.Name));
            Assert.That(templatedCreature.NumberOfHands, Is.EqualTo(creature.NumberOfHands));

            Assert.That(templatedCreature.Reach.Description, Is.EqualTo(creature.Reach.Description));
            Assert.That(templatedCreature.Reach.Unit, Is.EqualTo(creature.Reach.Unit));
            Assert.That(templatedCreature.Reach.Value, Is.EqualTo(creature.Reach.Value));

            Assert.That(templatedCreature.Saves, Has.Count.EqualTo(creature.Saves.Count));
            Assert.That(templatedCreature.Saves.Keys, Is.EquivalentTo(creature.Saves.Keys));

            foreach (var kvp in creature.Saves)
            {
                Assert.That(templatedCreature.Saves[kvp.Key].BaseAbility, Is.EqualTo(templatedCreature.Abilities[kvp.Value.BaseAbility.Name]));
                Assert.That(templatedCreature.Saves[kvp.Key].BaseValue, Is.EqualTo(kvp.Value.BaseValue));
                Assert.That(templatedCreature.Saves[kvp.Key].Bonus, Is.EqualTo(kvp.Value.Bonus));
                Assert.That(templatedCreature.Saves[kvp.Key].Bonuses, Is.EqualTo(kvp.Value.Bonuses));
                Assert.That(templatedCreature.Saves[kvp.Key].HasSave, Is.EqualTo(kvp.Value.HasSave));
                Assert.That(templatedCreature.Saves[kvp.Key].IsConditional, Is.EqualTo(kvp.Value.IsConditional));
                Assert.That(templatedCreature.Saves[kvp.Key].TotalBonus, Is.EqualTo(kvp.Value.TotalBonus));
            }

            Assert.That(templatedCreature.Size, Is.EqualTo(creature.Size));
            Assert.That(templatedCreature.Skills.Count(), Is.EqualTo(creature.Skills.Count()));
            foreach (var skill in creature.Skills)
            {
                var templatedSkill = templatedCreature.Skills.FirstOrDefault(a => a.Name == skill.Name);
                Assert.That(templatedSkill, Is.Not.Null);
                Assert.That(templatedSkill.ArmorCheckPenalty, Is.EqualTo(skill.ArmorCheckPenalty));
                Assert.That(templatedSkill.BaseAbility, Is.EqualTo(templatedCreature.Abilities[skill.BaseAbility.Name]));
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

            Assert.That(templatedCreature.Space.Description, Is.EqualTo(creature.Space.Description));
            Assert.That(templatedCreature.Space.Unit, Is.EqualTo(creature.Space.Unit));
            Assert.That(templatedCreature.Space.Value, Is.EqualTo(creature.Space.Value));

            Assert.That(templatedCreature.SpecialQualities.Count(), Is.EqualTo(creature.SpecialQualities.Count()));
            foreach (var feat in creature.SpecialQualities)
            {
                var templatedFeat = templatedCreature.SpecialQualities.FirstOrDefault(a => a.Name == feat.Name);
                Assert.That(templatedFeat, Is.Not.Null);
                Assert.That(templatedFeat.CanBeTakenMultipleTimes, Is.EqualTo(feat.CanBeTakenMultipleTimes));
                Assert.That(templatedFeat.Foci, Is.EqualTo(feat.Foci));
                Assert.That(templatedFeat.Frequency.Quantity, Is.EqualTo(feat.Frequency.Quantity));
                Assert.That(templatedFeat.Frequency.TimePeriod, Is.EqualTo(feat.Frequency.TimePeriod));
                Assert.That(templatedFeat.Name, Is.EqualTo(feat.Name));
                Assert.That(templatedFeat.Power, Is.EqualTo(feat.Power));
                Assert.That(templatedFeat.Save, Is.EqualTo(feat.Save));
            }

            Assert.That(templatedCreature.Speeds, Has.Count.EqualTo(creature.Speeds.Count));
            Assert.That(templatedCreature.Speeds.Keys, Is.EquivalentTo(creature.Speeds.Keys));

            foreach (var kvp in creature.Speeds)
            {
                Assert.That(templatedCreature.Speeds[kvp.Key].Description, Is.EqualTo(kvp.Value.Description));
                Assert.That(templatedCreature.Speeds[kvp.Key].Unit, Is.EqualTo(kvp.Value.Unit));
                Assert.That(templatedCreature.Speeds[kvp.Key].Value, Is.EqualTo(kvp.Value.Value));
            }

            Assert.That(templatedCreature.Summary, Is.EqualTo(creature.Summary));
            Assert.That(templatedCreature.Templates, Is.Empty);
            Assert.That(templatedCreature.Type.Name, Is.EqualTo(creature.Type.Name));
            Assert.That(templatedCreature.Type.SubTypes, Is.EquivalentTo(creature.Type.SubTypes));
        }

        [Test]
        public void GetCompatibleCreatures_ReturnCompatibleCreatures()
        {
            var creatures = new[] { "my creature", "my other creature" };

            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = 4;
            hitDice["my other creature"] = 1;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var data = new Dictionary<string, CreatureDataSelection>();
            data["my creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["my other creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR10 };

            mockCreatureDataSelector
                .Setup(s => s.SelectAll())
                .Returns(data);
            mockCreatureDataSelector
                .Setup(s => s.SelectFor(It.IsAny<string>()))
                .Returns((string c) => data[c]);

            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["my other creature"] = new[] { CreatureConstants.Types.Giant, "subtype 2" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string a, string t, string c) => types[c]);

            var alignments = new Dictionary<string, IEnumerable<string>>();
            alignments["my creature" + GroupConstants.Exploded] = new[] { "other alignment", "my alignment" };
            alignments["my other creature" + GroupConstants.Exploded] = new[] { "other alignment", "my alignment" };
            alignments["wrong creature 1" + GroupConstants.Exploded] = new[] { "other alignment", "my alignment" };
            alignments["wrong creature 2" + GroupConstants.Exploded] = new[] { "other alignment", "my alignment" };
            alignments["wrong creature 3" + GroupConstants.Exploded] = new[] { "other alignment", "my alignment" };
            alignments["wrong creature 4" + GroupConstants.Exploded] = new[] { "other alignment", "my alignment" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups))
                .Returns(alignments);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups, It.IsAny<string>()))
                .Returns((string a, string t, string c) => alignments[c]);

            var compatibleCreatures = templateApplicator.GetCompatibleCreatures(creatures, false);
            Assert.That(compatibleCreatures, Is.EqualTo(new[] { "my creature", "my other creature" }));
        }

        [Test]
        public void GetCompatibleCreatures_ReturnCompatibleCreatures_WhenAlignmentFilterValid()
        {
            var creatures = new[] { "my creature", "wrong creature 2", "my other creature", "wrong creature 1" };

            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["my other creature"] = new[] { CreatureConstants.Types.Giant, "subtype 3" };
            types["wrong creature 1"] = new[] { CreatureConstants.Types.Outsider, "subtype 2" };
            types["wrong creature 2"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string a, string t, string c) => types[c]);

            var alignments = new Dictionary<string, IEnumerable<string>>();
            alignments["my creature" + GroupConstants.Exploded] = new[] { "preset alignment", "other alignment" };
            alignments["my other creature" + GroupConstants.Exploded] = new[] { "preset alignment", "other alignment" };
            alignments["wrong creature 1" + GroupConstants.Exploded] = new[] { "wrong alignment", "other alignment" };
            alignments["wrong creature 2" + GroupConstants.Exploded] = new[] { "wrong alignment" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups))
                .Returns(alignments);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups, It.IsAny<string>()))
                .Returns((string a, string t, string c) => alignments[c]);

            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, "preset alignment"))
                .Returns(new[] { "wrong creature 3", "my other creature", "wrong creature 4", "my creature", "wrong creature 5" });

            var data = new Dictionary<string, CreatureDataSelection>();
            data["my creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["my other creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["wrong creature 1"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["wrong creature 2"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };

            mockCreatureDataSelector
                .Setup(s => s.SelectAll())
                .Returns(data);
            mockCreatureDataSelector
                .Setup(s => s.SelectFor(It.IsAny<string>()))
                .Returns((string c) => data[c]);

            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = 1;
            hitDice["my other creature"] = 1;
            hitDice["wrong creature 1"] = 1;
            hitDice["wrong creature 2"] = 1;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var filters = new Filters { Alignment = "preset alignment" };

            var compatibleCreatures = templateApplicator.GetCompatibleCreatures(creatures, false, filters);
            Assert.That(compatibleCreatures, Is.EqualTo(new[] { "my creature", "my other creature" }));
        }

        [TestCaseSource(nameof(ChallengeRatings))]
        public void GetCompatibleCreatures_WithChallengeRating_ReturnCompatibleCreatures(string challengeRating)
        {
            var creatures = new[] { "my creature", "wrong creature 1", "my other creature", "wrong creature 2" };

            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = 4;
            hitDice["my other creature"] = 4;
            hitDice["wrong creature 1"] = 4;
            hitDice["wrong creature 2"] = 4;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var data = new Dictionary<string, CreatureDataSelection>();
            data["my creature"] = new CreatureDataSelection { ChallengeRating = challengeRating };
            data["my other creature"] = new CreatureDataSelection { ChallengeRating = challengeRating };
            data["wrong creature 1"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.IncreaseChallengeRating(challengeRating, 1) };
            data["wrong creature 2"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.IncreaseChallengeRating(challengeRating, -1) };

            mockCreatureDataSelector
                .Setup(s => s.SelectAll())
                .Returns(data);
            mockCreatureDataSelector
                .Setup(s => s.SelectFor(It.IsAny<string>()))
                .Returns((string c) => data[c]);

            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["my other creature"] = new[] { CreatureConstants.Types.Giant, "subtype 2" };
            types["wrong creature 1"] = new[] { CreatureConstants.Types.Undead, "subtype 1" };
            types["wrong creature 2"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 3" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string a, string t, string c) => types[c]);

            var alignments = new Dictionary<string, IEnumerable<string>>();
            alignments["my creature" + GroupConstants.Exploded] = new[] { "other alignment", "my alignment" };
            alignments["my other creature" + GroupConstants.Exploded] = new[] { "other alignment", "my alignment" };
            alignments["wrong creature 1" + GroupConstants.Exploded] = new[] { "other alignment", "my alignment" };
            alignments["wrong creature 2" + GroupConstants.Exploded] = new[] { "other alignment", "my alignment" };
            alignments["wrong creature 3" + GroupConstants.Exploded] = new[] { "other alignment", "my alignment" };
            alignments["wrong creature 4" + GroupConstants.Exploded] = new[] { "other alignment", "my alignment" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups))
                .Returns(alignments);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups, It.IsAny<string>()))
                .Returns((string a, string t, string c) => alignments[c]);

            var filters = new Filters { ChallengeRating = challengeRating };

            var compatibleCreatures = templateApplicator.GetCompatibleCreatures(creatures, false, filters);
            Assert.That(compatibleCreatures, Is.EqualTo(new[] { "my creature", "my other creature" }));
        }

        private static IEnumerable ChallengeRatings => ChallengeRatingConstants.GetOrdered().Select(cr => new TestCaseData(cr));

        [Test]
        public void GetCompatibleCreatures_WithType_ReturnCompatibleCreatures()
        {
            var creatures = new[] { "my creature", "wrong creature 1", "my other creature", "wrong creature 2" };

            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["my other creature"] = new[] { CreatureConstants.Types.Giant, "subtype 2" };
            types["wrong creature 1"] = new[] { CreatureConstants.Types.Undead, "subtype 1" };
            types["wrong creature 2"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 3" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string a, string t, string c) => types[c]);
            mockCollectionSelector
                .Setup(s => s.Explode(Config.Name, TableNameConstants.Collection.CreatureGroups, It.IsAny<string>()))
                .Returns((string a, string t, string c) => types.Where(kvp => kvp.Value.Contains(c)).Select(kvp => kvp.Key));

            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = 4;
            hitDice["my other creature"] = 4;
            hitDice["wrong creature 1"] = 4;
            hitDice["wrong creature 2"] = 4;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var data = new Dictionary<string, CreatureDataSelection>();
            data["my creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["my other creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["wrong creature 1"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR2 };
            data["wrong creature 2"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1_2nd };

            mockCreatureDataSelector
                .Setup(s => s.SelectAll())
                .Returns(data);
            mockCreatureDataSelector
                .Setup(s => s.SelectFor(It.IsAny<string>()))
                .Returns((string c) => data[c]);

            var alignments = new Dictionary<string, IEnumerable<string>>();
            alignments["my creature" + GroupConstants.Exploded] = new[] { "other alignment", "my alignment" };
            alignments["my other creature" + GroupConstants.Exploded] = new[] { "other alignment", "my alignment" };
            alignments["wrong creature 1" + GroupConstants.Exploded] = new[] { "other alignment", "my alignment" };
            alignments["wrong creature 2" + GroupConstants.Exploded] = new[] { "other alignment", "my alignment" };
            alignments["wrong creature 3" + GroupConstants.Exploded] = new[] { "other alignment", "my alignment" };
            alignments["wrong creature 4" + GroupConstants.Exploded] = new[] { "other alignment", "my alignment" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups))
                .Returns(alignments);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups, It.IsAny<string>()))
                .Returns((string a, string t, string c) => alignments[c]);

            var filters = new Filters { Type = "subtype 2" };

            var compatibleCreatures = templateApplicator.GetCompatibleCreatures(creatures, false, filters);
            Assert.That(compatibleCreatures, Is.EqualTo(new[] { "my creature", "my other creature" }));
        }

        [Test]
        public void GetCompatibleCreatures_WithTypeAndChallengeRating_ReturnCompatibleCreatures()
        {
            var creatures = new[] { "my creature", "wrong creature 1", "my other creature", "wrong creature 2", "wrong creature 3", "wrong creature 4" };

            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = 4;
            hitDice["my other creature"] = 4;
            hitDice["wrong creature 1"] = 4;
            hitDice["wrong creature 2"] = 4;
            hitDice["wrong creature 3"] = 4;
            hitDice["wrong creature 4"] = 4;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var data = new Dictionary<string, CreatureDataSelection>();
            data["my creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["my other creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["wrong creature 1"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR2 };
            data["wrong creature 2"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1_2nd };
            data["wrong creature 3"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["wrong creature 4"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };

            mockCreatureDataSelector
                .Setup(s => s.SelectAll())
                .Returns(data);
            mockCreatureDataSelector
                .Setup(s => s.SelectFor(It.IsAny<string>()))
                .Returns((string c) => data[c]);

            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["my other creature"] = new[] { CreatureConstants.Types.Giant, "subtype 2" };
            types["wrong creature 1"] = new[] { CreatureConstants.Types.Undead, "subtype 3" };
            types["wrong creature 2"] = new[] { CreatureConstants.Types.Humanoid };
            types["wrong creature 3"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1" };
            types["wrong creature 4"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 3" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string a, string t, string c) => types[c]);
            mockCollectionSelector
                .Setup(s => s.Explode(Config.Name, TableNameConstants.Collection.CreatureGroups, It.IsAny<string>()))
                .Returns((string a, string t, string c) => types.Where(kvp => kvp.Value.Contains(c)).Select(kvp => kvp.Key));

            var alignments = new Dictionary<string, IEnumerable<string>>();
            alignments["my creature" + GroupConstants.Exploded] = new[] { "other alignment", "my alignment" };
            alignments["my other creature" + GroupConstants.Exploded] = new[] { "other alignment", "my alignment" };
            alignments["wrong creature 1" + GroupConstants.Exploded] = new[] { "other alignment", "my alignment" };
            alignments["wrong creature 2" + GroupConstants.Exploded] = new[] { "other alignment", "my alignment" };
            alignments["wrong creature 3" + GroupConstants.Exploded] = new[] { "other alignment", "my alignment" };
            alignments["wrong creature 4" + GroupConstants.Exploded] = new[] { "other alignment", "my alignment" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups))
                .Returns(alignments);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups, It.IsAny<string>()))
                .Returns((string a, string t, string c) => alignments[c]);

            var filters = new Filters { Type = "subtype 2", ChallengeRating = ChallengeRatingConstants.CR1 };

            var compatibleCreatures = templateApplicator.GetCompatibleCreatures(creatures, false, filters);
            Assert.That(compatibleCreatures, Is.EqualTo(new[] { "my creature", "my other creature" }));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void IsCompatible_ReturnsTrue(bool isCharacter)
        {
            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = 4;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var data = new Dictionary<string, CreatureDataSelection>();
            data["my creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };

            mockCreatureDataSelector
                .Setup(s => s.SelectAll())
                .Returns(data);
            mockCreatureDataSelector
                .Setup(s => s.SelectFor(It.IsAny<string>()))
                .Returns((string c) => data[c]);

            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string a, string t, string c) => types[c]);
            mockCollectionSelector
                .Setup(s => s.Explode(Config.Name, TableNameConstants.Collection.CreatureGroups, It.IsAny<string>()))
                .Returns((string a, string t, string c) => types.Where(kvp => kvp.Value.Contains(c)).Select(kvp => kvp.Key));

            var alignments = new Dictionary<string, IEnumerable<string>>();
            alignments["my creature" + GroupConstants.Exploded] = new[] { "other alignment", "my alignment" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups))
                .Returns(alignments);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups, It.IsAny<string>()))
                .Returns((string a, string t, string c) => alignments[c]);

            var compatibleCreatures = templateApplicator.GetCompatibleCreatures(new[] { "my creature" }, isCharacter);
            Assert.That(compatibleCreatures, Is.EqualTo(new[] { "my creature" }));
        }

        [TestCase(null, true)]
        [TestCase(CreatureConstants.Types.Humanoid, true)]
        [TestCase(CreatureConstants.Types.Outsider, false)]
        [TestCase(CreatureConstants.Types.Dragon, false)]
        [TestCase(CreatureConstants.Types.Undead, false)]
        [TestCase("subtype 1", true)]
        [TestCase("subtype 2", true)]
        [TestCase(CreatureConstants.Types.Subtypes.Native, false)]
        [TestCase(CreatureConstants.Types.Subtypes.Augmented, false)]
        [TestCase(CreatureConstants.Types.Subtypes.Shapechanger, false)]
        [TestCase(CreatureConstants.Types.Subtypes.Incorporeal, false)]
        [TestCase("wrong type", false)]
        public void IsCompatible_TypeMustMatch(string type, bool compatible)
        {
            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string a, string t, string c) => types[c]);
            mockCollectionSelector
                .Setup(s => s.Explode(Config.Name, TableNameConstants.Collection.CreatureGroups, It.IsAny<string>()))
                .Returns((string a, string t, string c) => types.Where(kvp => kvp.Value.Contains(c)).Select(kvp => kvp.Key));

            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = 4;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var data = new Dictionary<string, CreatureDataSelection>();
            data["my creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };

            mockCreatureDataSelector
                .Setup(s => s.SelectAll())
                .Returns(data);
            mockCreatureDataSelector
                .Setup(s => s.SelectFor(It.IsAny<string>()))
                .Returns((string c) => data[c]);

            var alignments = new Dictionary<string, IEnumerable<string>>();
            alignments["my creature" + GroupConstants.Exploded] = new[] { "other alignment", "my alignment" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups))
                .Returns(alignments);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups, It.IsAny<string>()))
                .Returns((string a, string t, string c) => alignments[c]);

            var filters = new Filters { Type = type };

            var compatibleCreatures = templateApplicator.GetCompatibleCreatures(new[] { "my creature" }, false, filters);
            Assert.That(compatibleCreatures.Any(), Is.EqualTo(compatible));
        }

        [TestCase(ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR0, false)]
        [TestCase(ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1_3rd, false)]
        [TestCase(ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1_2nd, true)]
        [TestCase(ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1, false)]
        [TestCase(ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR2, false)]
        [TestCase(ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR3, false)]
        [TestCase(ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR0, false)]
        [TestCase(ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR1_2nd, false)]
        [TestCase(ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR1, true)]
        [TestCase(ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR2, false)]
        [TestCase(ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR3, false)]
        [TestCase(ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR4, false)]
        [TestCase(ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR0, false)]
        [TestCase(ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR1_2nd, false)]
        [TestCase(ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR1, false)]
        [TestCase(ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR2, true)]
        [TestCase(ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR3, false)]
        [TestCase(ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR4, false)]
        [TestCase(ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR5, false)]
        public void IsCompatible_ChallengeRatingMustMatch(string original, string challengeRating, bool compatible)
        {
            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = 1;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var data = new Dictionary<string, CreatureDataSelection>();
            data["my creature"] = new CreatureDataSelection { ChallengeRating = original };

            mockCreatureDataSelector
                .Setup(s => s.SelectAll())
                .Returns(data);
            mockCreatureDataSelector
                .Setup(s => s.SelectFor(It.IsAny<string>()))
                .Returns((string c) => data[c]);

            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string a, string t, string c) => types[c]);

            var alignments = new Dictionary<string, IEnumerable<string>>();
            alignments["my creature" + GroupConstants.Exploded] = new[] { "other alignment", "my alignment" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups))
                .Returns(alignments);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups, It.IsAny<string>()))
                .Returns((string a, string t, string c) => alignments[c]);

            var filters = new Filters { ChallengeRating = challengeRating };

            var compatibleCreatures = templateApplicator.GetCompatibleCreatures(new[] { "my creature" }, false, filters);
            Assert.That(compatibleCreatures.Any(), Is.EqualTo(compatible));
        }

        [TestCase(0.5, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR0, true)]
        [TestCase(0.5, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1_3rd, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1_2nd, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR2, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR3, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR0, true)]
        [TestCase(0.5, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR1_2nd, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR1, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR2, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR3, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR4, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR0, true)]
        [TestCase(0.5, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR1_2nd, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR1, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR2, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR3, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR4, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR5, false)]
        [TestCase(1, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR0, true)]
        [TestCase(1, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1_3rd, false)]
        [TestCase(1, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1_2nd, false)]
        [TestCase(1, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1, false)]
        [TestCase(1, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR2, false)]
        [TestCase(1, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR3, false)]
        [TestCase(1, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR0, true)]
        [TestCase(1, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR1_2nd, false)]
        [TestCase(1, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR1, false)]
        [TestCase(1, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR2, false)]
        [TestCase(1, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR3, false)]
        [TestCase(1, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR4, false)]
        [TestCase(1, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR0, true)]
        [TestCase(1, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR1_2nd, false)]
        [TestCase(1, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR1, false)]
        [TestCase(1, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR2, false)]
        [TestCase(1, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR3, false)]
        [TestCase(1, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR4, false)]
        [TestCase(1, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR5, false)]
        [TestCase(2, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR0, false)]
        [TestCase(2, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1_3rd, false)]
        [TestCase(2, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1_2nd, true)]
        [TestCase(2, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1, false)]
        [TestCase(2, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR2, false)]
        [TestCase(2, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR3, false)]
        [TestCase(2, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR0, false)]
        [TestCase(2, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR1_2nd, false)]
        [TestCase(2, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR1, true)]
        [TestCase(2, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR2, false)]
        [TestCase(2, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR3, false)]
        [TestCase(2, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR4, false)]
        [TestCase(2, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR0, false)]
        [TestCase(2, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR1_2nd, false)]
        [TestCase(2, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR1, false)]
        [TestCase(2, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR2, true)]
        [TestCase(2, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR3, false)]
        [TestCase(2, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR4, false)]
        [TestCase(2, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR5, false)]
        public void IsCompatible_ChallengeRatingMustMatch_HumanoidCharacter(double hitDiceQuantity, string original, string challengeRating, bool compatible)
        {
            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = hitDiceQuantity;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var data = new Dictionary<string, CreatureDataSelection>();
            data["my creature"] = new CreatureDataSelection { ChallengeRating = original };

            mockCreatureDataSelector
                .Setup(s => s.SelectAll())
                .Returns(data);
            mockCreatureDataSelector
                .Setup(s => s.SelectFor(It.IsAny<string>()))
                .Returns((string c) => data[c]);

            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string a, string t, string c) => types[c]);

            var alignments = new Dictionary<string, IEnumerable<string>>();
            alignments["my creature" + GroupConstants.Exploded] = new[] { "other alignment", "my alignment" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups))
                .Returns(alignments);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups, It.IsAny<string>()))
                .Returns((string a, string t, string c) => alignments[c]);

            var filters = new Filters { ChallengeRating = challengeRating };

            var compatibleCreatures = templateApplicator.GetCompatibleCreatures(new[] { "my creature" }, true, filters);
            Assert.That(compatibleCreatures.Any(), Is.EqualTo(compatible));
        }

        [TestCase(0.5, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR0, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1_3rd, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1_2nd, true)]
        [TestCase(0.5, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR2, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR3, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR0, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR1_2nd, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR1, true)]
        [TestCase(0.5, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR2, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR3, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR4, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR0, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR1_2nd, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR1, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR2, true)]
        [TestCase(0.5, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR3, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR4, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR5, false)]
        [TestCase(1, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR0, false)]
        [TestCase(1, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1_3rd, false)]
        [TestCase(1, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1_2nd, true)]
        [TestCase(1, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1, false)]
        [TestCase(1, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR2, false)]
        [TestCase(1, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR3, false)]
        [TestCase(1, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR0, false)]
        [TestCase(1, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR1_2nd, false)]
        [TestCase(1, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR1, true)]
        [TestCase(1, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR2, false)]
        [TestCase(1, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR3, false)]
        [TestCase(1, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR4, false)]
        [TestCase(1, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR0, false)]
        [TestCase(1, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR1_2nd, false)]
        [TestCase(1, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR1, false)]
        [TestCase(1, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR2, true)]
        [TestCase(1, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR3, false)]
        [TestCase(1, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR4, false)]
        [TestCase(1, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR5, false)]
        [TestCase(2, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR0, false)]
        [TestCase(2, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1_3rd, false)]
        [TestCase(2, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1_2nd, true)]
        [TestCase(2, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1, false)]
        [TestCase(2, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR2, false)]
        [TestCase(2, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR3, false)]
        [TestCase(2, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR0, false)]
        [TestCase(2, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR1_2nd, false)]
        [TestCase(2, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR1, true)]
        [TestCase(2, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR2, false)]
        [TestCase(2, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR3, false)]
        [TestCase(2, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR4, false)]
        [TestCase(2, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR0, false)]
        [TestCase(2, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR1_2nd, false)]
        [TestCase(2, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR1, false)]
        [TestCase(2, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR2, true)]
        [TestCase(2, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR3, false)]
        [TestCase(2, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR4, false)]
        [TestCase(2, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR5, false)]
        public void IsCompatible_ChallengeRatingMustMatch_NonHumanoidCharacter(double hitDiceQuantity, string original, string challengeRating, bool compatible)
        {
            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = hitDiceQuantity;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var data = new Dictionary<string, CreatureDataSelection>();
            data["my creature"] = new CreatureDataSelection { ChallengeRating = original };

            mockCreatureDataSelector
                .Setup(s => s.SelectAll())
                .Returns(data);
            mockCreatureDataSelector
                .Setup(s => s.SelectFor(It.IsAny<string>()))
                .Returns((string c) => data[c]);

            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Giant, "subtype 1", "subtype 2" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string a, string t, string c) => types[c]);

            var alignments = new Dictionary<string, IEnumerable<string>>();
            alignments["my creature" + GroupConstants.Exploded] = new[] { "other alignment", "my alignment" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups))
                .Returns(alignments);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups, It.IsAny<string>()))
                .Returns((string a, string t, string c) => alignments[c]);

            var filters = new Filters { ChallengeRating = challengeRating };

            var compatibleCreatures = templateApplicator.GetCompatibleCreatures(new[] { "my creature" }, true, filters);
            Assert.That(compatibleCreatures.Any(), Is.EqualTo(compatible));
        }

        [TestCase("my alignment", "my alignment", true)]
        [TestCase("my alignment", "wrong alignment", false)]
        public void IsCompatible_AlignmentMustMatch(string creatureAlignment, string alignmentFilter, bool compatible)
        {
            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types[CreatureConstants.Human] = new[] { CreatureConstants.Types.Humanoid, CreatureConstants.Types.Subtypes.Human };
            types[CreatureConstants.Rat] = new[] { CreatureConstants.Types.Vermin };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string a, string t, string c) => types[c]);
            mockCollectionSelector
                .Setup(s => s.Explode(Config.Name, TableNameConstants.Collection.CreatureGroups, It.IsAny<string>()))
                .Returns((string a, string t, string c) => types.Where(kvp => kvp.Value.Contains(c)).Select(kvp => kvp.Key));

            var alignments = new Dictionary<string, IEnumerable<string>>();
            alignments["my creature" + GroupConstants.Exploded] = new[] { "other alignment", "my alignment" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups))
                .Returns(alignments);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups, It.IsAny<string>()))
                .Returns((string a, string t, string c) => alignments[c]);

            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, "my alignment"))
                .Returns(new[] { "my other creature", "my creature", "wrong creature" });
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, "wrong alignment"))
                .Returns(new[] { "my other creature", "wrong creature" });

            var data = new Dictionary<string, CreatureDataSelection>();
            data["my creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };

            mockCreatureDataSelector
                .Setup(s => s.SelectAll())
                .Returns(data);
            mockCreatureDataSelector
                .Setup(s => s.SelectFor(It.IsAny<string>()))
                .Returns((string c) => data[c]);

            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = 4;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var filters = new Filters { Alignment = alignmentFilter };

            var compatibleCreatures = templateApplicator.GetCompatibleCreatures(new[] { "my creature" }, false, filters);
            Assert.That(compatibleCreatures.Any(), Is.EqualTo(compatible));
        }

        [TestCase("subtype 1", ChallengeRatingConstants.CR2, "my alignment", false)]
        [TestCase("subtype 1", ChallengeRatingConstants.CR2, "wrong alignment", false)]
        [TestCase("subtype 1", ChallengeRatingConstants.CR1, "my alignment", true)]
        [TestCase("subtype 1", ChallengeRatingConstants.CR1, "wrong alignment", false)]
        [TestCase("wrong subtype", ChallengeRatingConstants.CR2, "my alignment", false)]
        [TestCase("wrong subtype", ChallengeRatingConstants.CR2, "wrong alignment", false)]
        [TestCase("wrong subtype", ChallengeRatingConstants.CR1, "my alignment", false)]
        [TestCase("wrong subtype", ChallengeRatingConstants.CR1, "wrong alignment", false)]
        public void IsCompatible_AllFiltersMustMatch(string type, string challengeRating, string alignment, bool compatible)
        {
            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = 2;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var data = new Dictionary<string, CreatureDataSelection>();
            data["my creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };

            mockCreatureDataSelector
                .Setup(s => s.SelectAll())
                .Returns(data);
            mockCreatureDataSelector
                .Setup(s => s.SelectFor(It.IsAny<string>()))
                .Returns((string c) => data[c]);

            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string a, string t, string c) => types[c]);
            mockCollectionSelector
                .Setup(s => s.Explode(Config.Name, TableNameConstants.Collection.CreatureGroups, It.IsAny<string>()))
                .Returns((string a, string t, string c) => types.Where(kvp => kvp.Value.Contains(c)).Select(kvp => kvp.Key));

            var alignments = new Dictionary<string, IEnumerable<string>>();
            alignments["my creature" + GroupConstants.Exploded] = new[] { "other alignment", "my alignment" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups))
                .Returns(alignments);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups, It.IsAny<string>()))
                .Returns((string a, string t, string c) => alignments[c]);

            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, "my alignment"))
                .Returns(new[] { "my other creature", "my creature", "wrong creature" });
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, "wrong alignment"))
                .Returns(new[] { "my other creature", "wrong creature" });

            var filters = new Filters { Alignment = alignment, Type = type, ChallengeRating = challengeRating };

            var compatibleCreatures = templateApplicator.GetCompatibleCreatures(new[] { "my creature" }, false, filters);
            Assert.That(compatibleCreatures.Any(), Is.EqualTo(compatible));
        }

        [Test]
        public void GetCompatiblePrototypes_FromNames_ReturnCompatibleCreatures()
        {
            var creatures = new[] { "my creature", "my other creature" };

            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = 4;
            hitDice["my other creature"] = 1;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var data = new Dictionary<string, CreatureDataSelection>();
            data["my creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["my other creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR10 };

            mockCreatureDataSelector
                .Setup(s => s.SelectAll())
                .Returns(data);
            mockCreatureDataSelector
                .Setup(s => s.SelectFor(It.IsAny<string>()))
                .Returns((string c) => data[c]);

            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["my other creature"] = new[] { CreatureConstants.Types.Giant, "subtype 2" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string a, string t, string c) => types[c]);

            var alignments = new Dictionary<string, IEnumerable<string>>();
            alignments["my creature" + GroupConstants.Exploded] = new[] { "other alignment", "my alignment" };
            alignments["my other creature" + GroupConstants.Exploded] = new[] { "another alignment", "my alignment" };
            alignments["wrong creature 1" + GroupConstants.Exploded] = new[] { "other alignment", "my alignment" };
            alignments["wrong creature 2" + GroupConstants.Exploded] = new[] { "other alignment", "my alignment" };
            alignments["wrong creature 3" + GroupConstants.Exploded] = new[] { "other alignment", "my alignment" };
            alignments["wrong creature 4" + GroupConstants.Exploded] = new[] { "other alignment", "my alignment" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups))
                .Returns(alignments);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups, It.IsAny<string>()))
                .Returns((string a, string t, string c) => alignments[c]);

            var prototypes = new[]
            {
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my creature")
                    .WithCreatureType(types["my creature"].ToArray())
                    .WithAlignments(alignments["my creature" + GroupConstants.Exploded].ToArray())
                    .WithChallengeRating(data["my creature"].ChallengeRating)
                    .WithCasterLevel(data["my creature"].CasterLevel)
                    .WithLevelAdjustment(data["my creature"].LevelAdjustment)
                    .WithHitDiceQuantity(hitDice["my creature"])
                    .WithoutAbility(AbilityConstants.Strength)
                    .WithAbility(AbilityConstants.Constitution, 90210)
                    .WithAbility(AbilityConstants.Dexterity, 42)
                    .WithAbility(AbilityConstants.Intelligence, 600)
                    .WithAbility(AbilityConstants.Wisdom, 1337)
                    .WithAbility(AbilityConstants.Charisma, 1336)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my other creature")
                    .WithCreatureType(types["my other creature"].ToArray())
                    .WithAlignments(alignments["my other creature" + GroupConstants.Exploded].ToArray())
                    .WithChallengeRating(data["my other creature"].ChallengeRating)
                    .WithCasterLevel(data["my other creature"].CasterLevel)
                    .WithLevelAdjustment(data["my other creature"].LevelAdjustment)
                    .WithHitDiceQuantity(hitDice["my other creature"])
                    .WithAbility(AbilityConstants.Strength, 96)
                    .WithAbility(AbilityConstants.Constitution, 783)
                    .WithAbility(AbilityConstants.Dexterity, 8245)
                    .WithAbility(AbilityConstants.Intelligence, -8)
                    .WithAbility(AbilityConstants.Wisdom, 0)
                    .WithAbility(AbilityConstants.Charisma, 1)
                    .Build(),
            };
            mockPrototypeFactory
                .Setup(f => f.Build(It.Is<IEnumerable<string>>(cc => cc.IsEquivalentTo(new[] { "my creature", "my other creature" })), false))
                .Returns(prototypes);

            var compatibleCreatures = templateApplicator.GetCompatiblePrototypes(creatures, false).ToArray();
            Assert.That(compatibleCreatures, Has.Length.EqualTo(2));

            Assert.That(compatibleCreatures[0].Name, Is.EqualTo("my creature"));
            Assert.That(compatibleCreatures[0].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[0].Type.Name, Is.EqualTo(CreatureConstants.Types.Humanoid));
            Assert.That(compatibleCreatures[0].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 1",
                "subtype 2",
            }));
            Assert.That(compatibleCreatures[0].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].FullScore, Is.Zero);
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(Ability.DefaultScore + 1336));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(Ability.DefaultScore + 600));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(Ability.DefaultScore + 1337));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(Ability.DefaultScore + 42));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(Ability.DefaultScore + 90210));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Alignments, Is.EqualTo(new[]
            {
                new Alignment("other alignment"),
                new Alignment("my alignment"),
            }));
            Assert.That(compatibleCreatures[0].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[0].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR1));
            Assert.That(compatibleCreatures[0].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[0].HitDiceQuantity, Is.EqualTo(4));

            Assert.That(compatibleCreatures[1].Name, Is.EqualTo("my other creature"));
            Assert.That(compatibleCreatures[1].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[1].Type.Name, Is.EqualTo(CreatureConstants.Types.Giant));
            Assert.That(compatibleCreatures[1].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 2",
            }));
            Assert.That(compatibleCreatures[1].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(Ability.DefaultScore + 1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(Ability.DefaultScore - 8));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(Ability.DefaultScore + 8245));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(Ability.DefaultScore + 783));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(Ability.DefaultScore + 96));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Alignments, Is.EqualTo(new[]
            {
                new Alignment("another alignment"),
                new Alignment("my alignment"),
            }));
            Assert.That(compatibleCreatures[1].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[1].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR10));
            Assert.That(compatibleCreatures[1].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[1].HitDiceQuantity, Is.EqualTo(1));
        }

        [Test]
        public void GetCompatiblePrototypes_FromNames_ReturnCompatibleCreatures_WhenAlignmentFilterValid()
        {
            var creatures = new[] { "my creature", "wrong creature 2", "my other creature", "wrong creature 1" };

            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["my other creature"] = new[] { CreatureConstants.Types.Giant, "subtype 3" };
            types["wrong creature 1"] = new[] { CreatureConstants.Types.Outsider, "subtype 2" };
            types["wrong creature 2"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string a, string t, string c) => types[c]);

            var alignments = new Dictionary<string, IEnumerable<string>>();
            alignments["my creature" + GroupConstants.Exploded] = new[] { "preset alignment", "other alignment" };
            alignments["my other creature" + GroupConstants.Exploded] = new[] { "preset alignment", "other alignment" };
            alignments["wrong creature 1" + GroupConstants.Exploded] = new[] { "wrong alignment", "other alignment" };
            alignments["wrong creature 2" + GroupConstants.Exploded] = new[] { "wrong alignment" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups))
                .Returns(alignments);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups, It.IsAny<string>()))
                .Returns((string a, string t, string c) => alignments[c]);

            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, "preset alignment"))
                .Returns(new[] { "wrong creature 3", "my other creature", "wrong creature 4", "my creature", "wrong creature 5" });

            var data = new Dictionary<string, CreatureDataSelection>();
            data["my creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["my other creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["wrong creature 1"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["wrong creature 2"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };

            mockCreatureDataSelector
                .Setup(s => s.SelectAll())
                .Returns(data);
            mockCreatureDataSelector
                .Setup(s => s.SelectFor(It.IsAny<string>()))
                .Returns((string c) => data[c]);

            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = 1;
            hitDice["my other creature"] = 1;
            hitDice["wrong creature 1"] = 1;
            hitDice["wrong creature 2"] = 1;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var prototypes = new[]
            {
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my creature")
                    .WithCreatureType(types["my creature"].ToArray())
                    .WithAlignments(alignments["my creature" + GroupConstants.Exploded].ToArray())
                    .WithChallengeRating(data["my creature"].ChallengeRating)
                    .WithCasterLevel(data["my creature"].CasterLevel)
                    .WithLevelAdjustment(data["my creature"].LevelAdjustment)
                    .WithHitDiceQuantity(hitDice["my creature"])
                    .WithoutAbility(AbilityConstants.Strength)
                    .WithAbility(AbilityConstants.Constitution, 90210)
                    .WithAbility(AbilityConstants.Dexterity, 42)
                    .WithAbility(AbilityConstants.Intelligence, 600)
                    .WithAbility(AbilityConstants.Wisdom, 1337)
                    .WithAbility(AbilityConstants.Charisma, 1336)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my other creature")
                    .WithCreatureType(types["my other creature"].ToArray())
                    .WithAlignments(alignments["my other creature" + GroupConstants.Exploded].ToArray())
                    .WithChallengeRating(data["my other creature"].ChallengeRating)
                    .WithCasterLevel(data["my other creature"].CasterLevel)
                    .WithLevelAdjustment(data["my other creature"].LevelAdjustment)
                    .WithHitDiceQuantity(hitDice["my other creature"])
                    .WithAbility(AbilityConstants.Strength, 96)
                    .WithAbility(AbilityConstants.Constitution, 783)
                    .WithAbility(AbilityConstants.Dexterity, 8245)
                    .WithAbility(AbilityConstants.Intelligence, -8)
                    .WithAbility(AbilityConstants.Wisdom, 0)
                    .WithAbility(AbilityConstants.Charisma, 1)
                    .Build(),
            };
            mockPrototypeFactory
                .Setup(f => f.Build(It.Is<IEnumerable<string>>(cc => cc.IsEquivalentTo(new[] { "my creature", "my other creature" })), false))
                .Returns(prototypes);

            var filters = new Filters { Alignment = "preset alignment" };

            var compatibleCreatures = templateApplicator.GetCompatiblePrototypes(creatures, false, filters).ToArray();
            Assert.That(compatibleCreatures, Has.Length.EqualTo(2));

            Assert.That(compatibleCreatures[0].Name, Is.EqualTo("my creature"));
            Assert.That(compatibleCreatures[0].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[0].Type.Name, Is.EqualTo(CreatureConstants.Types.Humanoid));
            Assert.That(compatibleCreatures[0].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 1",
                "subtype 2",
            }));
            Assert.That(compatibleCreatures[0].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].FullScore, Is.Zero);
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(Ability.DefaultScore + 1336));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(Ability.DefaultScore + 600));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(Ability.DefaultScore + 1337));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(Ability.DefaultScore + 42));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(Ability.DefaultScore + 90210));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Alignments, Is.EqualTo(new[]
            {
                new Alignment("preset alignment"),
            }));
            Assert.That(compatibleCreatures[0].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[0].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR1));
            Assert.That(compatibleCreatures[0].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[0].HitDiceQuantity, Is.EqualTo(1));

            Assert.That(compatibleCreatures[1].Name, Is.EqualTo("my other creature"));
            Assert.That(compatibleCreatures[1].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[1].Type.Name, Is.EqualTo(CreatureConstants.Types.Giant));
            Assert.That(compatibleCreatures[1].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 3",
            }));
            Assert.That(compatibleCreatures[1].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(Ability.DefaultScore + 1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(Ability.DefaultScore - 8));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(Ability.DefaultScore + 8245));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(Ability.DefaultScore + 783));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(Ability.DefaultScore + 96));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Alignments, Is.EqualTo(new[]
            {
                new Alignment("preset alignment"),
            }));
            Assert.That(compatibleCreatures[1].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[1].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR1));
            Assert.That(compatibleCreatures[1].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[1].HitDiceQuantity, Is.EqualTo(1));
        }

        [TestCaseSource(nameof(ChallengeRatings))]
        public void GetCompatiblePrototypes_FromNames_WithChallengeRating_ReturnCompatibleCreatures(string challengeRating)
        {
            var creatures = new[] { "my creature", "wrong creature 1", "my other creature", "wrong creature 2" };

            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = 4;
            hitDice["my other creature"] = 4;
            hitDice["wrong creature 1"] = 4;
            hitDice["wrong creature 2"] = 4;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var data = new Dictionary<string, CreatureDataSelection>();
            data["my creature"] = new CreatureDataSelection { ChallengeRating = challengeRating };
            data["my other creature"] = new CreatureDataSelection { ChallengeRating = challengeRating };
            data["wrong creature 1"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.IncreaseChallengeRating(challengeRating, 1) };
            data["wrong creature 2"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.IncreaseChallengeRating(challengeRating, -1) };

            mockCreatureDataSelector
                .Setup(s => s.SelectAll())
                .Returns(data);
            mockCreatureDataSelector
                .Setup(s => s.SelectFor(It.IsAny<string>()))
                .Returns((string c) => data[c]);

            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["my other creature"] = new[] { CreatureConstants.Types.Giant, "subtype 2" };
            types["wrong creature 1"] = new[] { CreatureConstants.Types.Undead, "subtype 1" };
            types["wrong creature 2"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 3" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string a, string t, string c) => types[c]);

            var alignments = new Dictionary<string, IEnumerable<string>>();
            alignments["my creature" + GroupConstants.Exploded] = new[] { "other alignment", "my alignment" };
            alignments["my other creature" + GroupConstants.Exploded] = new[] { "another alignment", "my alignment" };
            alignments["wrong creature 1" + GroupConstants.Exploded] = new[] { "other alignment", "my alignment" };
            alignments["wrong creature 2" + GroupConstants.Exploded] = new[] { "other alignment", "my alignment" };
            alignments["wrong creature 3" + GroupConstants.Exploded] = new[] { "other alignment", "my alignment" };
            alignments["wrong creature 4" + GroupConstants.Exploded] = new[] { "other alignment", "my alignment" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups))
                .Returns(alignments);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups, It.IsAny<string>()))
                .Returns((string a, string t, string c) => alignments[c]);

            var prototypes = new[]
            {
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my creature")
                    .WithCreatureType(types["my creature"].ToArray())
                    .WithAlignments(alignments["my creature" + GroupConstants.Exploded].ToArray())
                    .WithChallengeRating(data["my creature"].ChallengeRating)
                    .WithCasterLevel(data["my creature"].CasterLevel)
                    .WithLevelAdjustment(data["my creature"].LevelAdjustment)
                    .WithHitDiceQuantity(hitDice["my creature"])
                    .WithoutAbility(AbilityConstants.Strength)
                    .WithAbility(AbilityConstants.Constitution, 90210)
                    .WithAbility(AbilityConstants.Dexterity, 42)
                    .WithAbility(AbilityConstants.Intelligence, 600)
                    .WithAbility(AbilityConstants.Wisdom, 1337)
                    .WithAbility(AbilityConstants.Charisma, 1336)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my other creature")
                    .WithCreatureType(types["my other creature"].ToArray())
                    .WithAlignments(alignments["my other creature" + GroupConstants.Exploded].ToArray())
                    .WithChallengeRating(data["my other creature"].ChallengeRating)
                    .WithCasterLevel(data["my other creature"].CasterLevel)
                    .WithLevelAdjustment(data["my other creature"].LevelAdjustment)
                    .WithHitDiceQuantity(hitDice["my other creature"])
                    .WithAbility(AbilityConstants.Strength, 96)
                    .WithAbility(AbilityConstants.Constitution, 783)
                    .WithAbility(AbilityConstants.Dexterity, 8245)
                    .WithAbility(AbilityConstants.Intelligence, -8)
                    .WithAbility(AbilityConstants.Wisdom, 0)
                    .WithAbility(AbilityConstants.Charisma, 1)
                    .Build(),
            };
            mockPrototypeFactory
                .Setup(f => f.Build(It.Is<IEnumerable<string>>(cc => cc.IsEquivalentTo(new[] { "my creature", "my other creature" })), false))
                .Returns(prototypes);

            var filters = new Filters { ChallengeRating = challengeRating };

            var compatibleCreatures = templateApplicator.GetCompatiblePrototypes(creatures, false, filters).ToArray();
            Assert.That(compatibleCreatures, Has.Length.EqualTo(2));

            Assert.That(compatibleCreatures[0].Name, Is.EqualTo("my creature"));
            Assert.That(compatibleCreatures[0].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[0].Type.Name, Is.EqualTo(CreatureConstants.Types.Humanoid));
            Assert.That(compatibleCreatures[0].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 1",
                "subtype 2",
            }));
            Assert.That(compatibleCreatures[0].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].FullScore, Is.Zero);
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(Ability.DefaultScore + 1336));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(Ability.DefaultScore + 600));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(Ability.DefaultScore + 1337));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(Ability.DefaultScore + 42));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(10 + 90210));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Alignments, Is.EqualTo(new[]
            {
                new Alignment("other alignment"),
                new Alignment("my alignment"),
            }));
            Assert.That(compatibleCreatures[0].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[0].ChallengeRating, Is.EqualTo(challengeRating));
            Assert.That(compatibleCreatures[0].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[0].HitDiceQuantity, Is.EqualTo(4));

            Assert.That(compatibleCreatures[1].Name, Is.EqualTo("my other creature"));
            Assert.That(compatibleCreatures[1].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[1].Type.Name, Is.EqualTo(CreatureConstants.Types.Giant));
            Assert.That(compatibleCreatures[1].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 2",
            }));
            Assert.That(compatibleCreatures[1].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(Ability.DefaultScore + 1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(8255));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(10 + 783));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(106));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Alignments, Is.EqualTo(new[]
            {
                new Alignment("another alignment"),
                new Alignment("my alignment"),
            }));
            Assert.That(compatibleCreatures[1].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[1].ChallengeRating, Is.EqualTo(challengeRating));
            Assert.That(compatibleCreatures[1].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[1].HitDiceQuantity, Is.EqualTo(4));
        }

        [Test]
        public void GetCompatiblePrototypes_FromNames_WithType_ReturnCompatibleCreatures()
        {
            var creatures = new[] { "my creature", "wrong creature 1", "my other creature", "wrong creature 2" };

            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["my other creature"] = new[] { CreatureConstants.Types.Giant, "subtype 2" };
            types["wrong creature 1"] = new[] { CreatureConstants.Types.Undead, "subtype 1" };
            types["wrong creature 2"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 3" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string a, string t, string c) => types[c]);
            mockCollectionSelector
                .Setup(s => s.Explode(Config.Name, TableNameConstants.Collection.CreatureGroups, It.IsAny<string>()))
                .Returns((string a, string t, string c) => types.Where(kvp => kvp.Value.Contains(c)).Select(kvp => kvp.Key));

            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = 4;
            hitDice["my other creature"] = 4;
            hitDice["wrong creature 1"] = 4;
            hitDice["wrong creature 2"] = 4;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var data = new Dictionary<string, CreatureDataSelection>();
            data["my creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["my other creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["wrong creature 1"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR2 };
            data["wrong creature 2"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1_2nd };

            mockCreatureDataSelector
                .Setup(s => s.SelectAll())
                .Returns(data);
            mockCreatureDataSelector
                .Setup(s => s.SelectFor(It.IsAny<string>()))
                .Returns((string c) => data[c]);

            var alignments = new Dictionary<string, IEnumerable<string>>();
            alignments["my creature" + GroupConstants.Exploded] = new[] { "other alignment", "my alignment" };
            alignments["my other creature" + GroupConstants.Exploded] = new[] { "another alignment", "my alignment" };
            alignments["wrong creature 1" + GroupConstants.Exploded] = new[] { "other alignment", "my alignment" };
            alignments["wrong creature 2" + GroupConstants.Exploded] = new[] { "other alignment", "my alignment" };
            alignments["wrong creature 3" + GroupConstants.Exploded] = new[] { "other alignment", "my alignment" };
            alignments["wrong creature 4" + GroupConstants.Exploded] = new[] { "other alignment", "my alignment" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups))
                .Returns(alignments);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups, It.IsAny<string>()))
                .Returns((string a, string t, string c) => alignments[c]);

            var prototypes = new[]
            {
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my creature")
                    .WithCreatureType(types["my creature"].ToArray())
                    .WithAlignments(alignments["my creature" + GroupConstants.Exploded].ToArray())
                    .WithChallengeRating(data["my creature"].ChallengeRating)
                    .WithCasterLevel(data["my creature"].CasterLevel)
                    .WithLevelAdjustment(data["my creature"].LevelAdjustment)
                    .WithHitDiceQuantity(hitDice["my creature"])
                    .WithoutAbility(AbilityConstants.Strength)
                    .WithAbility(AbilityConstants.Constitution, 90210)
                    .WithAbility(AbilityConstants.Dexterity, 42)
                    .WithAbility(AbilityConstants.Intelligence, 600)
                    .WithAbility(AbilityConstants.Wisdom, 1337)
                    .WithAbility(AbilityConstants.Charisma, 1336)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my other creature")
                    .WithCreatureType(types["my other creature"].ToArray())
                    .WithAlignments(alignments["my other creature" + GroupConstants.Exploded].ToArray())
                    .WithChallengeRating(data["my other creature"].ChallengeRating)
                    .WithCasterLevel(data["my other creature"].CasterLevel)
                    .WithLevelAdjustment(data["my other creature"].LevelAdjustment)
                    .WithHitDiceQuantity(hitDice["my other creature"])
                    .WithAbility(AbilityConstants.Strength, 96)
                    .WithAbility(AbilityConstants.Constitution, 783)
                    .WithAbility(AbilityConstants.Dexterity, 8245)
                    .WithAbility(AbilityConstants.Intelligence, -8)
                    .WithAbility(AbilityConstants.Wisdom, 0)
                    .WithAbility(AbilityConstants.Charisma, 1)
                    .Build(),
            };
            mockPrototypeFactory
                .Setup(f => f.Build(It.Is<IEnumerable<string>>(cc => cc.IsEquivalentTo(new[] { "my creature", "my other creature" })), false))
                .Returns(prototypes);

            var filters = new Filters { Type = "subtype 2" };

            var compatibleCreatures = templateApplicator.GetCompatiblePrototypes(creatures, false, filters).ToArray();
            Assert.That(compatibleCreatures, Has.Length.EqualTo(2));

            Assert.That(compatibleCreatures[0].Name, Is.EqualTo("my creature"));
            Assert.That(compatibleCreatures[0].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[0].Type.Name, Is.EqualTo(CreatureConstants.Types.Humanoid));
            Assert.That(compatibleCreatures[0].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 1",
                "subtype 2",
            }));
            Assert.That(compatibleCreatures[0].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].FullScore, Is.Zero);
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(Ability.DefaultScore + 1336));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(Ability.DefaultScore + 600));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(Ability.DefaultScore + 1337));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(Ability.DefaultScore + 42));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(10 + 90210));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Alignments, Is.EqualTo(new[]
            {
                new Alignment("other alignment"),
                new Alignment("my alignment"),
            }));
            Assert.That(compatibleCreatures[0].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[0].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR1));
            Assert.That(compatibleCreatures[0].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[0].HitDiceQuantity, Is.EqualTo(4));

            Assert.That(compatibleCreatures[1].Name, Is.EqualTo("my other creature"));
            Assert.That(compatibleCreatures[1].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[1].Type.Name, Is.EqualTo(CreatureConstants.Types.Giant));
            Assert.That(compatibleCreatures[1].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 2",
            }));
            Assert.That(compatibleCreatures[1].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(Ability.DefaultScore + 1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(Ability.DefaultScore - 8));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(Ability.DefaultScore + 8245));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(Ability.DefaultScore + 783));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(Ability.DefaultScore + 96));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Alignments, Is.EqualTo(new[]
            {
                new Alignment("another alignment"),
                new Alignment("my alignment"),
            }));
            Assert.That(compatibleCreatures[1].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[1].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR1));
            Assert.That(compatibleCreatures[1].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[1].HitDiceQuantity, Is.EqualTo(4));
        }

        [Test]
        public void GetCompatiblePrototypes_FromNames_WithTypeAndChallengeRating_ReturnCompatibleCreatures()
        {
            var creatures = new[] { "my creature", "wrong creature 1", "my other creature", "wrong creature 2", "wrong creature 3", "wrong creature 4" };

            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = 4;
            hitDice["my other creature"] = 4;
            hitDice["wrong creature 1"] = 4;
            hitDice["wrong creature 2"] = 4;
            hitDice["wrong creature 3"] = 4;
            hitDice["wrong creature 4"] = 4;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var data = new Dictionary<string, CreatureDataSelection>();
            data["my creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["my other creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["wrong creature 1"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR2 };
            data["wrong creature 2"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1_2nd };
            data["wrong creature 3"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["wrong creature 4"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };

            mockCreatureDataSelector
                .Setup(s => s.SelectAll())
                .Returns(data);
            mockCreatureDataSelector
                .Setup(s => s.SelectFor(It.IsAny<string>()))
                .Returns((string c) => data[c]);

            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["my other creature"] = new[] { CreatureConstants.Types.Giant, "subtype 2" };
            types["wrong creature 1"] = new[] { CreatureConstants.Types.Undead, "subtype 3" };
            types["wrong creature 2"] = new[] { CreatureConstants.Types.Humanoid };
            types["wrong creature 3"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1" };
            types["wrong creature 4"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 3" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string a, string t, string c) => types[c]);
            mockCollectionSelector
                .Setup(s => s.Explode(Config.Name, TableNameConstants.Collection.CreatureGroups, It.IsAny<string>()))
                .Returns((string a, string t, string c) => types.Where(kvp => kvp.Value.Contains(c)).Select(kvp => kvp.Key));

            var alignments = new Dictionary<string, IEnumerable<string>>();
            alignments["my creature" + GroupConstants.Exploded] = new[] { "other alignment", "my alignment" };
            alignments["my other creature" + GroupConstants.Exploded] = new[] { "another alignment", "my alignment" };
            alignments["wrong creature 1" + GroupConstants.Exploded] = new[] { "other alignment", "my alignment" };
            alignments["wrong creature 2" + GroupConstants.Exploded] = new[] { "other alignment", "my alignment" };
            alignments["wrong creature 3" + GroupConstants.Exploded] = new[] { "other alignment", "my alignment" };
            alignments["wrong creature 4" + GroupConstants.Exploded] = new[] { "other alignment", "my alignment" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups))
                .Returns(alignments);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups, It.IsAny<string>()))
                .Returns((string a, string t, string c) => alignments[c]);

            var prototypes = new[]
            {
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my creature")
                    .WithCreatureType(types["my creature"].ToArray())
                    .WithAlignments(alignments["my creature" + GroupConstants.Exploded].ToArray())
                    .WithChallengeRating(data["my creature"].ChallengeRating)
                    .WithCasterLevel(data["my creature"].CasterLevel)
                    .WithLevelAdjustment(data["my creature"].LevelAdjustment)
                    .WithHitDiceQuantity(hitDice["my creature"])
                    .WithoutAbility(AbilityConstants.Strength)
                    .WithAbility(AbilityConstants.Constitution, 90210)
                    .WithAbility(AbilityConstants.Dexterity, 42)
                    .WithAbility(AbilityConstants.Intelligence, 600)
                    .WithAbility(AbilityConstants.Wisdom, 1337)
                    .WithAbility(AbilityConstants.Charisma, 1336)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my other creature")
                    .WithCreatureType(types["my other creature"].ToArray())
                    .WithAlignments(alignments["my other creature" + GroupConstants.Exploded].ToArray())
                    .WithChallengeRating(data["my other creature"].ChallengeRating)
                    .WithCasterLevel(data["my other creature"].CasterLevel)
                    .WithLevelAdjustment(data["my other creature"].LevelAdjustment)
                    .WithHitDiceQuantity(hitDice["my other creature"])
                    .WithAbility(AbilityConstants.Strength, 96)
                    .WithAbility(AbilityConstants.Constitution, 783)
                    .WithAbility(AbilityConstants.Dexterity, 8245)
                    .WithAbility(AbilityConstants.Intelligence, -8)
                    .WithAbility(AbilityConstants.Wisdom, 0)
                    .WithAbility(AbilityConstants.Charisma, 1)
                    .Build(),
            };
            mockPrototypeFactory
                .Setup(f => f.Build(It.Is<IEnumerable<string>>(cc => cc.IsEquivalentTo(new[] { "my creature", "my other creature" })), false))
                .Returns(prototypes);

            var filters = new Filters { Type = "subtype 2", ChallengeRating = ChallengeRatingConstants.CR1 };

            var compatibleCreatures = templateApplicator.GetCompatiblePrototypes(creatures, false, filters).ToArray();
            Assert.That(compatibleCreatures, Has.Length.EqualTo(2));

            Assert.That(compatibleCreatures[0].Name, Is.EqualTo("my creature"));
            Assert.That(compatibleCreatures[0].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[0].Type.Name, Is.EqualTo(CreatureConstants.Types.Humanoid));
            Assert.That(compatibleCreatures[0].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 1",
                "subtype 2",
            }));
            Assert.That(compatibleCreatures[0].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].FullScore, Is.Zero);
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(Ability.DefaultScore + 1336));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(Ability.DefaultScore + 600));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(Ability.DefaultScore + 1337));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(Ability.DefaultScore + 42));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(10 + 90210));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Alignments, Is.EqualTo(new[]
            {
                new Alignment("other alignment"),
                new Alignment("my alignment"),
            }));
            Assert.That(compatibleCreatures[0].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[0].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR1));
            Assert.That(compatibleCreatures[0].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[0].HitDiceQuantity, Is.EqualTo(4));

            Assert.That(compatibleCreatures[1].Name, Is.EqualTo("my other creature"));
            Assert.That(compatibleCreatures[1].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[1].Type.Name, Is.EqualTo(CreatureConstants.Types.Giant));
            Assert.That(compatibleCreatures[1].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 2",
            }));
            Assert.That(compatibleCreatures[1].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(Ability.DefaultScore + 1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(Ability.DefaultScore - 8));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(Ability.DefaultScore + 8245));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(Ability.DefaultScore + 783));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(Ability.DefaultScore + 96));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Alignments, Is.EqualTo(new[]
            {
                new Alignment("another alignment"),
                new Alignment("my alignment"),
            }));
            Assert.That(compatibleCreatures[1].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[1].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR1));
            Assert.That(compatibleCreatures[1].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[1].HitDiceQuantity, Is.EqualTo(4));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void GetCompatiblePrototypes_FromPrototypes_ReturnCompatibleCreatures(bool asCharacter)
        {
            var creatures = new[]
            {
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my creature")
                    .WithHitDiceQuantity(4)
                    .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                    .WithAlignments("another alignment", "my alignment")
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my other creature")
                    .WithChallengeRating(ChallengeRatingConstants.CR10)
                    .WithCreatureType(CreatureConstants.Types.Giant, "subtype 2")
                    .WithAlignments("other alignment", "my alignment")
                    .Build(),
            };

            var compatibleCreatures = templateApplicator.GetCompatiblePrototypes(creatures, asCharacter).ToArray();
            Assert.That(compatibleCreatures, Has.Length.EqualTo(2));

            Assert.That(compatibleCreatures[0].Name, Is.EqualTo("my creature"));
            Assert.That(compatibleCreatures[0].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[0].Type.Name, Is.EqualTo(CreatureConstants.Types.Humanoid));
            Assert.That(compatibleCreatures[0].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 1",
                "subtype 2",
            }));
            Assert.That(compatibleCreatures[0].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Alignments, Is.EqualTo(new[]
            {
                new Alignment("another alignment"),
                new Alignment("my alignment"),
            }));
            Assert.That(compatibleCreatures[0].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[0].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR1));
            Assert.That(compatibleCreatures[0].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[0].HitDiceQuantity, Is.EqualTo(4));

            Assert.That(compatibleCreatures[1].Name, Is.EqualTo("my other creature"));
            Assert.That(compatibleCreatures[1].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[1].Type.Name, Is.EqualTo(CreatureConstants.Types.Giant));
            Assert.That(compatibleCreatures[1].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 2",
            }));
            Assert.That(compatibleCreatures[1].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Alignments, Is.EqualTo(new[]
            {
                new Alignment("other alignment"),
                new Alignment("my alignment"),
            }));
            Assert.That(compatibleCreatures[1].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[1].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR10));
            Assert.That(compatibleCreatures[1].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[1].HitDiceQuantity, Is.EqualTo(1));
        }

        [Test]
        public void GetCompatiblePrototypes_FromPrototypes_ReturnCompatibleCreatures_WhenAlignmentFilterValid()
        {
            var creatures = new[]
            {
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my creature")
                    .WithHitDiceQuantity(4)
                    .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                    .WithAlignments("preset alignment", "my alignment")
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("wrong creature 1")
                    .WithCreatureType(CreatureConstants.Types.Outsider, "subtype 2")
                    .WithAlignments("wrong alignment", "other alignment")
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my other creature")
                    .WithChallengeRating(ChallengeRatingConstants.CR10)
                    .WithCreatureType(CreatureConstants.Types.Giant, "subtype 3")
                    .WithAlignments("other alignment", "preset alignment")
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("wrong creature 2")
                    .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                    .WithAlignments("wrong alignment")
                    .Build(),
            };

            var filters = new Filters { Alignment = "preset alignment" };

            var compatibleCreatures = templateApplicator.GetCompatiblePrototypes(creatures, false, filters).ToArray();
            Assert.That(compatibleCreatures, Has.Length.EqualTo(2));

            Assert.That(compatibleCreatures[0].Name, Is.EqualTo("my creature"));
            Assert.That(compatibleCreatures[0].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[0].Type.Name, Is.EqualTo(CreatureConstants.Types.Humanoid));
            Assert.That(compatibleCreatures[0].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 1",
                "subtype 2",
            }));
            Assert.That(compatibleCreatures[0].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Alignments, Is.EqualTo(new[]
            {
                new Alignment("preset alignment"),
            }));
            Assert.That(compatibleCreatures[0].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[0].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR1));
            Assert.That(compatibleCreatures[0].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[0].HitDiceQuantity, Is.EqualTo(4));

            Assert.That(compatibleCreatures[1].Name, Is.EqualTo("my other creature"));
            Assert.That(compatibleCreatures[1].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[1].Type.Name, Is.EqualTo(CreatureConstants.Types.Giant));
            Assert.That(compatibleCreatures[1].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 3",
            }));
            Assert.That(compatibleCreatures[1].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Alignments, Is.EqualTo(new[]
            {
                new Alignment("preset alignment"),
            }));
            Assert.That(compatibleCreatures[1].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[1].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR10));
            Assert.That(compatibleCreatures[1].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[1].HitDiceQuantity, Is.EqualTo(1));
        }

        [TestCaseSource(nameof(ChallengeRatings))]
        public void GetCompatiblePrototypes_FromPrototypes_WithChallengeRating_ReturnCompatibleCreatures(string challengeRating)
        {
            var creatures = new[]
            {
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my creature")
                    .WithHitDiceQuantity(4)
                    .WithChallengeRating(challengeRating)
                    .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                    .WithAlignments("other alignment", "my alignment")
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("wrong creature 1")
                    .WithHitDiceQuantity(4)
                    .WithChallengeRating(ChallengeRatingConstants.IncreaseChallengeRating(challengeRating, 1))
                    .WithCreatureType(CreatureConstants.Types.Undead, "subtype 1")
                    .WithAlignments("wrong alignment", "other alignment")
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my other creature")
                    .WithHitDiceQuantity(4)
                    .WithChallengeRating(challengeRating)
                    .WithCreatureType(CreatureConstants.Types.Giant, "subtype 2")
                    .WithAlignments("another alignment", "my alignment")
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("wrong creature 2")
                    .WithHitDiceQuantity(4)
                    .WithChallengeRating(ChallengeRatingConstants.IncreaseChallengeRating(challengeRating, -1))
                    .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 3")
                    .WithAlignments("wrong alignment")
                    .Build(),
            };

            var filters = new Filters { ChallengeRating = challengeRating };

            var compatibleCreatures = templateApplicator.GetCompatiblePrototypes(creatures, false, filters).ToArray();
            Assert.That(compatibleCreatures, Has.Length.EqualTo(2));

            Assert.That(compatibleCreatures[0].Name, Is.EqualTo("my creature"));
            Assert.That(compatibleCreatures[0].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[0].Type.Name, Is.EqualTo(CreatureConstants.Types.Humanoid));
            Assert.That(compatibleCreatures[0].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 1",
                "subtype 2",
            }));
            Assert.That(compatibleCreatures[0].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Alignments, Is.EqualTo(new[]
            {
                new Alignment("other alignment"),
                new Alignment("my alignment"),
            }));
            Assert.That(compatibleCreatures[0].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[0].ChallengeRating, Is.EqualTo(challengeRating));
            Assert.That(compatibleCreatures[0].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[0].HitDiceQuantity, Is.EqualTo(4));

            Assert.That(compatibleCreatures[1].Name, Is.EqualTo("my other creature"));
            Assert.That(compatibleCreatures[1].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[1].Type.Name, Is.EqualTo(CreatureConstants.Types.Giant));
            Assert.That(compatibleCreatures[1].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 2",
            }));
            Assert.That(compatibleCreatures[1].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Alignments, Is.EqualTo(new[]
            {
                new Alignment("another alignment"),
                new Alignment("my alignment"),
            }));
            Assert.That(compatibleCreatures[1].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[1].ChallengeRating, Is.EqualTo(challengeRating));
            Assert.That(compatibleCreatures[1].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[1].HitDiceQuantity, Is.EqualTo(4));
        }

        [Test]
        public void GetCompatiblePrototypes_FromPrototypes_WithType_ReturnCompatibleCreatures()
        {
            var creatures = new[]
            {
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my creature")
                    .WithHitDiceQuantity(4)
                    .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                    .WithAlignments("other alignment", "my alignment")
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("wrong creature 1")
                    .WithHitDiceQuantity(4)
                    .WithCreatureType(CreatureConstants.Types.Undead, "subtype 1")
                    .WithAlignments("wrong alignment", "other alignment")
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my other creature")
                    .WithHitDiceQuantity(4)
                    .WithCreatureType(CreatureConstants.Types.Giant, "subtype 2")
                    .WithAlignments("another alignment", "my alignment")
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("wrong creature 2")
                    .WithHitDiceQuantity(4)
                    .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 3")
                    .WithAlignments("wrong alignment")
                    .WithChallengeRating(ChallengeRatingConstants.CR1_2nd)
                    .Build(),
            };

            var filters = new Filters { Type = "subtype 2" };

            var compatibleCreatures = templateApplicator.GetCompatiblePrototypes(creatures, false, filters).ToArray();
            Assert.That(compatibleCreatures, Has.Length.EqualTo(2));

            Assert.That(compatibleCreatures[0].Name, Is.EqualTo("my creature"));
            Assert.That(compatibleCreatures[0].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[0].Type.Name, Is.EqualTo(CreatureConstants.Types.Humanoid));
            Assert.That(compatibleCreatures[0].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 1",
                "subtype 2",
            }));
            Assert.That(compatibleCreatures[0].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Alignments, Is.EqualTo(new[]
            {
                new Alignment("other alignment"),
                new Alignment("my alignment"),
            }));
            Assert.That(compatibleCreatures[0].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[0].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR1));
            Assert.That(compatibleCreatures[0].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[0].HitDiceQuantity, Is.EqualTo(4));

            Assert.That(compatibleCreatures[1].Name, Is.EqualTo("my other creature"));
            Assert.That(compatibleCreatures[1].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[1].Type.Name, Is.EqualTo(CreatureConstants.Types.Giant));
            Assert.That(compatibleCreatures[1].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 2",
            }));
            Assert.That(compatibleCreatures[1].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Alignments, Is.EqualTo(new[]
            {
                new Alignment("another alignment"),
                new Alignment("my alignment"),
            }));
            Assert.That(compatibleCreatures[1].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[1].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR1));
            Assert.That(compatibleCreatures[1].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[1].HitDiceQuantity, Is.EqualTo(4));
        }

        [Test]
        public void GetCompatiblePrototypes_FromPrototypes_WithTypeAndChallengeRating_ReturnCompatibleCreatures()
        {
            var creatures = new[]
            {
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my creature")
                    .WithHitDiceQuantity(4)
                    .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("wrong creature 1")
                    .WithHitDiceQuantity(4)
                    .WithChallengeRating(ChallengeRatingConstants.CR2)
                    .WithCreatureType(CreatureConstants.Types.Undead, "subtype 3")
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my other creature")
                    .WithHitDiceQuantity(4)
                    .WithCreatureType(CreatureConstants.Types.Giant, "subtype 2")
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("wrong creature 2")
                    .WithHitDiceQuantity(4)
                    .WithChallengeRating(ChallengeRatingConstants.CR1_2nd)
                    .WithCreatureType(CreatureConstants.Types.Humanoid)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("wrong creature 3")
                    .WithHitDiceQuantity(4)
                    .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1")
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("wrong creature 4")
                    .WithHitDiceQuantity(4)
                    .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 3")
                    .Build(),
            };

            var filters = new Filters { Type = "subtype 2", ChallengeRating = ChallengeRatingConstants.CR1 };

            var compatibleCreatures = templateApplicator.GetCompatiblePrototypes(creatures, false, filters).ToArray();
            Assert.That(compatibleCreatures, Has.Length.EqualTo(2));

            Assert.That(compatibleCreatures[0].Name, Is.EqualTo("my creature"));
            Assert.That(compatibleCreatures[0].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[0].Type.Name, Is.EqualTo(CreatureConstants.Types.Humanoid));
            Assert.That(compatibleCreatures[0].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 1",
                "subtype 2",
            }));
            Assert.That(compatibleCreatures[0].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[0].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR1));
            Assert.That(compatibleCreatures[0].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[0].HitDiceQuantity, Is.EqualTo(4));

            Assert.That(compatibleCreatures[1].Name, Is.EqualTo("my other creature"));
            Assert.That(compatibleCreatures[1].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[1].Type.Name, Is.EqualTo(CreatureConstants.Types.Giant));
            Assert.That(compatibleCreatures[1].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 2",
            }));
            Assert.That(compatibleCreatures[1].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[1].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR1));
            Assert.That(compatibleCreatures[1].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[1].HitDiceQuantity, Is.EqualTo(4));
        }
    }
}
