using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Selectors.Collections;
using DnDGen.CreatureGen.Selectors.Selections;
using DnDGen.CreatureGen.Tables;
using DnDGen.CreatureGen.Templates;
using DnDGen.Infrastructure.Selectors.Collections;
using Moq;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        [SetUp]
        public void Setup()
        {
            mockCollectionSelector = new Mock<ICollectionSelector>();
            mockCreatureDataSelector = new Mock<ICreatureDataSelector>();
            mockAdjustmentSelector = new Mock<IAdjustmentsSelector>();

            templateApplicator = new NoneApplicator(mockCollectionSelector.Object, mockCreatureDataSelector.Object, mockAdjustmentSelector.Object);
        }

        [Test]
        public void ApplyTo_ThrowsException_WhenCreatureNotCompatible()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void ApplyTo_ThrowsException_WhenCreatureNotCompatible_WithFilters(bool asCharacter, string type, string challengeRating, string alignment)
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void ApplyTo_ReturnsCreature_WithFilters()
        {
            Assert.Fail("not yet written");
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
            Assert.That(templatedCreature.Template, Is.EqualTo(creature.Template).And.EqualTo(CreatureConstants.Templates.None));
            Assert.That(templatedCreature.Type.Name, Is.EqualTo(creature.Type.Name));
            Assert.That(templatedCreature.Type.SubTypes, Is.EquivalentTo(creature.Type.SubTypes));
        }

        [Test]
        public async Task ApplyToAsync_ThrowsException_WhenCreatureNotCompatible()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public async Task ApplyToAsync_ThrowsException_WhenCreatureNotCompatible_WithFilters(bool asCharacter, string type, string challengeRating, string alignment)
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public async Task ApplyToAsync_ReturnsCreature_WithFilters()
        {
            Assert.Fail("not yet written");
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
            Assert.That(templatedCreature.Template, Is.EqualTo(creature.Template).And.EqualTo(CreatureConstants.Templates.None));
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
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string t, string c) => types[c]);

            var compatibleCreatures = templateApplicator.GetCompatibleCreatures(creatures, false);
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
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string t, string c) => types[c]);

            var compatibleCreatures = templateApplicator.GetCompatibleCreatures(creatures, false, challengeRating: challengeRating);
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
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string t, string c) => types[c]);
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, It.IsAny<string>()))
                .Returns((string t, string c) => types.Where(kvp => kvp.Value.Contains(c)).Select(kvp => kvp.Key));

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

            var compatibleCreatures = templateApplicator.GetCompatibleCreatures(creatures, false, type: "subtype 2");
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
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string t, string c) => types[c]);
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, It.IsAny<string>()))
                .Returns((string t, string c) => types.Where(kvp => kvp.Value.Contains(c)).Select(kvp => kvp.Key));

            var compatibleCreatures = templateApplicator.GetCompatibleCreatures(creatures, false, type: "subtype 2");
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
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string t, string c) => types[c]);
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, It.IsAny<string>()))
                .Returns((string t, string c) => types.Where(kvp => kvp.Value.Contains(c)).Select(kvp => kvp.Key));

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
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string t, string c) => types[c]);
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, It.IsAny<string>()))
                .Returns((string t, string c) => types.Where(kvp => kvp.Value.Contains(c)).Select(kvp => kvp.Key));

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

            var compatibleCreatures = templateApplicator.GetCompatibleCreatures(new[] { "my creature" }, false, type: type);
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
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string t, string c) => types[c]);

            var compatibleCreatures = templateApplicator.GetCompatibleCreatures(new[] { "my creature" }, false, challengeRating: challengeRating);
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
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string t, string c) => types[c]);

            var compatibleCreatures = templateApplicator.GetCompatibleCreatures(new[] { "my creature" }, true, challengeRating: challengeRating);
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
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string t, string c) => types[c]);

            var compatibleCreatures = templateApplicator.GetCompatibleCreatures(new[] { "my creature" }, true, challengeRating: challengeRating);
            Assert.That(compatibleCreatures.Any(), Is.EqualTo(compatible));
        }

        [TestCase("my alignment", "my alignment", true)]
        [TestCase("my alignment", "wrong alignment", false)]
        public void IsCompatible_AlignmentMustMatch(string alignmentFilter, string creatureAlignment, bool compatible)
        {
            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types[CreatureConstants.Human] = new[] { CreatureConstants.Types.Humanoid, CreatureConstants.Types.Subtypes.Human };
            types[CreatureConstants.Rat] = new[] { CreatureConstants.Types.Vermin };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string t, string c) => types[c]);
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, It.IsAny<string>()))
                .Returns((string t, string c) => types.Where(kvp => kvp.Value.Contains(c)).Select(kvp => kvp.Key));

            var alignments = new Dictionary<string, IEnumerable<string>>();
            alignments["my creature" + GroupConstants.Exploded] = new[] { "other alignment", creatureAlignment };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.AlignmentGroups))
                .Returns(alignments);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.AlignmentGroups, It.IsAny<string>()))
                .Returns((string t, string c) => alignments[c]);

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

            var compatibleCreatures = templateApplicator.GetCompatibleCreatures(new[] { "my creature" }, false, alignment: alignmentFilter);
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
        public void IsCompatible_AllFiltersMustMatch(string type, string challengeRating, bool compatible)
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
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string t, string c) => types[c]);
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, It.IsAny<string>()))
                .Returns((string t, string c) => types.Where(kvp => kvp.Value.Contains(c)).Select(kvp => kvp.Key));

            var alignments = new Dictionary<string, IEnumerable<string>>();
            alignments["my creature" + GroupConstants.Exploded] = new[] { "other alignment", "my alignment" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.AlignmentGroups))
                .Returns(alignments);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.AlignmentGroups, It.IsAny<string>()))
                .Returns((string t, string c) => alignments[c]);

            var compatibleCreatures = templateApplicator.GetCompatibleCreatures(new[] { "my creature" }, false, type, challengeRating);
            Assert.That(compatibleCreatures.Any(), Is.EqualTo(compatible));
        }
    }
}
