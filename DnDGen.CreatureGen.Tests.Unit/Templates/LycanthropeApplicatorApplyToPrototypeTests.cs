using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Alignments;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Generators.Creatures;
using DnDGen.CreatureGen.Tests.Unit.TestCaseSources;
using DnDGen.CreatureGen.Verifiers.Exceptions;
using NUnit.Framework;

namespace DnDGen.CreatureGen.Tests.Unit.Templates
{
    [TestFixture]
    internal class LycanthropeApplicatorApplyToPrototypeTests : LycanthropeApplicatorTestsBase
    {
        [Test]
        public void ApplyTo_Prototype_ThrowsException_WhenCreatureNotCompatible()
        {
            var creature = new CreaturePrototypeBuilder()
                .WithTestValues()
                .WithCreatureType(CreatureConstants.Types.MonstrousHumanoid, "subtype 1", "subtype 2")
                .WithChallengeRating(ChallengeRatingConstants.CR1)
                .WithLevelAdjustment(90210)
                .Build();

            SetUpAnimalBasics("my animal", size: SizeConstants.Medium, hitDiceQuantity: 1);

            var expected = new InvalidCreatureException("Type 'Monstrous Humanoid' is not valid", false, creature.Name, templates: ["my lycanthrope"]);
            var function = () => applicator.ApplyTo(creature);
            Assert.That(function, Throws.InstanceOf<InvalidCreatureException>().With.Message.EqualTo(expected.Message));
        }

        [TestCaseSource(typeof(LycanthropeTestData), nameof(LycanthropeTestData.IncompatibleFilters))]
        public void ApplyTo_ThrowsException_WhenCreatureNotCompatible_WithFilters(bool asCharacter, string type, string challengeRating, string alignment)
        {
            var creature = new CreaturePrototypeBuilder()
                .WithTestValues()
                .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                .WithChallengeRating(ChallengeRatingConstants.CR1)
                .WithLevelAdjustment(90210)
                .WithAsCharacter(asCharacter)
                .WithAlignments("original alignment")
                .Build();

            SetUpAnimalBasics("my animal", size: SizeConstants.Medium, hitDiceQuantity: 1);

            var filters = new Filters
            {
                Types = [type],
                ChallengeRatings = [challengeRating],
                Alignments = [alignment]
            };
            var (Compatible, Reason) = filters.AreCompatible(
                ["original alignment"],
                [ChallengeRatingConstants.CR3],
                [CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2", CreatureConstants.Types.Subtypes.Shapechanger]);
            var expected = new InvalidCreatureException(Reason, asCharacter, creature.Name, filters, templates: ["my lycanthrope"]);

            var function = () => applicator.ApplyTo(creature, filters);
            Assert.That(function, Throws.InstanceOf<InvalidCreatureException>().With.Message.EqualTo(expected.Message));
        }

        [Test]
        public void ApplyTo_Prototype_ReturnsUpdatedPrototype()
        {
            var creature = new CreaturePrototypeBuilder()
                .WithTestValues()
                .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                .WithChallengeRating(ChallengeRatingConstants.CR1)
                .WithLevelAdjustment(90210)
                .WithAbility(AbilityConstants.Strength, 96)
                .WithAbility(AbilityConstants.Constitution, 22)
                .WithAbility(AbilityConstants.Dexterity, 42)
                .WithAbility(AbilityConstants.Intelligence, 600)
                .WithAbility(AbilityConstants.Wisdom, 1337)
                .WithAbility(AbilityConstants.Charisma, 1336)
                .Build();

            SetUpAnimalBasics("my animal", size: SizeConstants.Medium, hitDiceQuantity: 1);
            SetAnimalAbilityAdjustments("my animal", 22, 2022, 8245);

            var updatedPrototype = applicator.ApplyTo(creature);
            Assert.That(updatedPrototype, Is.SameAs(creature));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(Ability.DefaultScore + 96));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Strength].TemplateAdjustment, Is.Zero);
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Strength].Bonuses, Has.Count.EqualTo(1));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Strength].Bonuses[0].IsConditional, Is.True);
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Strength].Bonuses[0].Value, Is.EqualTo(22));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Strength].Bonuses[0].Condition, Is.EqualTo("In Animal or Hybrid form"));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(Ability.DefaultScore + 22));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Constitution].TemplateAdjustment, Is.Zero);
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Constitution].Bonuses, Has.Count.EqualTo(1));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Constitution].Bonuses[0].IsConditional, Is.True);
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Constitution].Bonuses[0].Value, Is.EqualTo(8245));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Constitution].Bonuses[0].Condition, Is.EqualTo("In Animal or Hybrid form"));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(Ability.DefaultScore + 42));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Dexterity].TemplateAdjustment, Is.Zero);
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Dexterity].Bonuses, Has.Count.EqualTo(1));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Dexterity].Bonuses[0].IsConditional, Is.True);
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Dexterity].Bonuses[0].Value, Is.EqualTo(2022));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Dexterity].Bonuses[0].Condition, Is.EqualTo("In Animal or Hybrid form"));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(Ability.DefaultScore + 600));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Intelligence].TemplateAdjustment, Is.Zero);
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Intelligence].Bonuses, Is.Empty);
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(Ability.DefaultScore + 1337 + 2));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Wisdom].TemplateAdjustment, Is.EqualTo(2));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Wisdom].Bonuses, Is.Empty);
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(Ability.DefaultScore + 1336));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Charisma].TemplateAdjustment, Is.Zero);
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Charisma].Bonuses, Is.Empty);
            Assert.That(updatedPrototype.ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR3));
            Assert.That(updatedPrototype.LevelAdjustment, Is.EqualTo(90210 + 2));
            Assert.That(updatedPrototype.Type.Name, Is.EqualTo(CreatureConstants.Types.Humanoid));
            Assert.That(updatedPrototype.Type.SubTypes, Is.EquivalentTo(["subtype 1", "subtype 2", CreatureConstants.Types.Subtypes.Shapechanger]));
            Assert.That(updatedPrototype.Templates, Is.EqualTo(["my lycanthrope"]));
        }

        [Test]
        public void ApplyTo_Prototype_ReturnsUpdatedPrototype_WithoutWisdom()
        {
            var creature = new CreaturePrototypeBuilder()
                .WithTestValues()
                .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                .WithAbility(AbilityConstants.Strength, 96)
                .WithAbility(AbilityConstants.Constitution, 22)
                .WithAbility(AbilityConstants.Dexterity, 42)
                .WithAbility(AbilityConstants.Intelligence, 600)
                .WithoutAbility(AbilityConstants.Wisdom)
                .WithAbility(AbilityConstants.Charisma, 1336)
                .Build();

            SetUpAnimalBasics("my animal", size: SizeConstants.Medium, hitDiceQuantity: 1);
            SetAnimalAbilityAdjustments("my animal", 22, 2022, 8245);

            var updatedPrototype = applicator.ApplyTo(creature);
            Assert.That(updatedPrototype, Is.SameAs(creature));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(Ability.DefaultScore + 96));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Strength].TemplateAdjustment, Is.Zero);
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Strength].Bonuses, Has.Count.EqualTo(1));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Strength].Bonuses[0].IsConditional, Is.True);
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Strength].Bonuses[0].Value, Is.EqualTo(22));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Strength].Bonuses[0].Condition, Is.EqualTo("In Animal or Hybrid form"));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(Ability.DefaultScore + 22));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Constitution].TemplateAdjustment, Is.Zero);
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Constitution].Bonuses, Has.Count.EqualTo(1));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Constitution].Bonuses[0].IsConditional, Is.True);
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Constitution].Bonuses[0].Value, Is.EqualTo(8245));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Constitution].Bonuses[0].Condition, Is.EqualTo("In Animal or Hybrid form"));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(Ability.DefaultScore + 42));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Dexterity].TemplateAdjustment, Is.Zero);
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Dexterity].Bonuses, Has.Count.EqualTo(1));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Dexterity].Bonuses[0].IsConditional, Is.True);
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Dexterity].Bonuses[0].Value, Is.EqualTo(2022));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Dexterity].Bonuses[0].Condition, Is.EqualTo("In Animal or Hybrid form"));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(Ability.DefaultScore + 600));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Intelligence].TemplateAdjustment, Is.Zero);
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Intelligence].Bonuses, Is.Empty);
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Wisdom].FullScore, Is.Zero);
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Wisdom].TemplateAdjustment, Is.Zero);
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Wisdom].Bonuses, Is.Empty);
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(Ability.DefaultScore + 1336));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Charisma].TemplateAdjustment, Is.Zero);
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Charisma].Bonuses, Is.Empty);
        }

        [Test]
        public void ApplyTo_Prototype_ReturnsUpdatedPrototype_WithImprovedTemplateAdjustments()
        {
            var creature = new CreaturePrototypeBuilder()
                .WithTestValues()
                .WithName("my creature")
                .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                .WithAbility(AbilityConstants.Strength, 96, 783)
                .WithAbility(AbilityConstants.Constitution, 90210, 8245)
                .WithAbility(AbilityConstants.Dexterity, 42, 2015)
                .WithAbility(AbilityConstants.Intelligence, 600, 9)
                .WithAbility(AbilityConstants.Wisdom, 1337, 22)
                .WithAbility(AbilityConstants.Charisma, 1336, 2022)
                .Build();

            SetUpAnimalBasics("my animal", size: SizeConstants.Medium, hitDiceQuantity: 1);
            SetAnimalAbilityAdjustments("my animal", 227, 12, 2300);

            var updatedPrototype = applicator.ApplyTo(creature);
            Assert.That(updatedPrototype.Name, Is.EqualTo("my creature"));
            Assert.That(updatedPrototype.Abilities, Has.Count.EqualTo(6));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(Ability.DefaultScore + 96 + 783));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Strength].TemplateAdjustment, Is.EqualTo(783));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Strength].Bonuses, Has.Count.EqualTo(1));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Strength].Bonuses[0].IsConditional, Is.True);
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Strength].Bonuses[0].Value, Is.EqualTo(227));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Strength].Bonuses[0].Condition, Is.EqualTo("In Animal or Hybrid form"));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(Ability.DefaultScore + 90210 + 8245));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Constitution].TemplateAdjustment, Is.EqualTo(8245));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Constitution].Bonuses, Has.Count.EqualTo(1));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Constitution].Bonuses[0].IsConditional, Is.True);
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Constitution].Bonuses[0].Value, Is.EqualTo(2300));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Constitution].Bonuses[0].Condition, Is.EqualTo("In Animal or Hybrid form"));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(Ability.DefaultScore + 42 + 2015));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Dexterity].TemplateAdjustment, Is.EqualTo(2015));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Dexterity].Bonuses, Has.Count.EqualTo(1));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Dexterity].Bonuses[0].IsConditional, Is.True);
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Dexterity].Bonuses[0].Value, Is.EqualTo(12));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Dexterity].Bonuses[0].Condition, Is.EqualTo("In Animal or Hybrid form"));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(Ability.DefaultScore + 600 + 9));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Intelligence].TemplateAdjustment, Is.EqualTo(9));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Intelligence].Bonuses, Is.Empty);
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(Ability.DefaultScore + 1337 + 22 + 2));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Wisdom].TemplateAdjustment, Is.EqualTo(22 + 2));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Wisdom].Bonuses, Is.Empty);
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(Ability.DefaultScore + 1336 + 2022));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Charisma].TemplateAdjustment, Is.EqualTo(2022));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Charisma].Bonuses, Is.Empty);
        }

        [TestCaseSource(typeof(LycanthropeTestData), nameof(LycanthropeTestData.ChallengeRatings))]
        public void ApplyTo_Prototype_ReturnsUpdatedPrototype_WithUpdatedChallengeRating(
            string originalChallengeRating,
            double animalHitDiceQuantity,
            string updatedChallengeRating)
        {
            var creature = new CreaturePrototypeBuilder()
                .WithTestValues()
                .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                .WithChallengeRating(originalChallengeRating)
                .Build();

            SetUpAnimalBasics("my animal", size: SizeConstants.Medium, hitDiceQuantity: animalHitDiceQuantity);
            SetAnimalAbilityAdjustments("my animal");

            var updatedPrototype = applicator.ApplyTo(creature);
            Assert.That(updatedPrototype, Is.SameAs(creature));
            Assert.That(updatedPrototype.ChallengeRating, Is.EqualTo(updatedChallengeRating));
        }

        [Test]
        public void ApplyTo_Prototype_ReturnsUpdatedPrototype_WithoutLevelAdjustment()
        {
            var creature = new CreaturePrototypeBuilder()
                .WithTestValues()
                .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                .WithLevelAdjustment(null)
                .Build();

            SetUpAnimalBasics("my animal", size: SizeConstants.Medium, hitDiceQuantity: 1);
            SetAnimalAbilityAdjustments("my animal");

            var updatedPrototype = applicator.ApplyTo(creature);
            Assert.That(updatedPrototype, Is.SameAs(creature));
            Assert.That(updatedPrototype.LevelAdjustment, Is.Null);
        }

        [TestCaseSource(typeof(LycanthropeTestData), nameof(LycanthropeTestData.LevelAdjustments))]
        public void ApplyTo_Prototype_ReturnsUpdatedPrototype_WithUpdatedLevelAdjustment(int? oldLevelAdjustment, int? newLevelAdjustment, bool isNatural)
        {
            var creature = new CreaturePrototypeBuilder()
                .WithTestValues()
                .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                .WithLevelAdjustment(oldLevelAdjustment)
                .Build();

            applicator.IsNatural = isNatural;

            SetUpAnimalBasics("my animal", size: SizeConstants.Medium, hitDiceQuantity: 1);
            SetAnimalAbilityAdjustments("my animal");

            var updatedPrototype = applicator.ApplyTo(creature);
            Assert.That(updatedPrototype, Is.SameAs(creature));
            Assert.That(updatedPrototype.LevelAdjustment, Is.EqualTo(newLevelAdjustment));
        }

        [Test]
        public void ApplyTo_PrototypeWithAlignment_ReturnsUpdatedPrototype_WithFilteredAlignment()
        {
            var creature = new CreaturePrototypeBuilder()
                .WithTestValues()
                .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                .WithAlignments("original alignment", "other alignment")
                .Build();

            SetUpAnimalBasics("my animal", size: SizeConstants.Medium, hitDiceQuantity: 1);
            SetAnimalAbilityAdjustments("my animal");

            var filters = new Filters { Alignments = ["other alignment"] };

            var updatedPrototype = applicator.ApplyTo(creature, filters);
            Assert.That(updatedPrototype, Is.SameAs(creature));
            Assert.That(updatedPrototype.Alignments, Is.EqualTo([new Alignment("other alignment")]));
        }

        [Test]
        public void ApplyTo_PrototypeWithAlignment_ReturnsUpdatedPrototype_WithFilteredAlignment_PreserveAlignmentWeighting()
        {
            var creature = new CreaturePrototypeBuilder()
                .WithTestValues()
                .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                .WithAlignments("original alignment", "other alignment", "original alignment")
                .Build();

            SetUpAnimalBasics("my animal", size: SizeConstants.Medium, hitDiceQuantity: 1);
            SetAnimalAbilityAdjustments("my animal");

            var filters = new Filters { Alignments = ["original alignment"] };

            var updatedPrototype = applicator.ApplyTo(creature, filters);
            Assert.That(updatedPrototype, Is.SameAs(creature));
            Assert.That(updatedPrototype.Alignments, Is.EqualTo([new Alignment("original alignment"), new Alignment("original alignment")]));
        }

        [Test]
        public void ApplyTo_Prototype_ReturnsUpdatedPrototype_WithAdditionalTemplates()
        {
            var creature = new CreaturePrototypeBuilder()
                .WithTestValues()
                .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                .Build();
            creature.Templates.Add("my template");

            SetUpAnimalBasics("my animal", size: SizeConstants.Medium, hitDiceQuantity: 1);
            SetAnimalAbilityAdjustments("my animal");

            var updatedPrototype = applicator.ApplyTo(creature);
            Assert.That(updatedPrototype, Is.SameAs(creature));
            Assert.That(updatedPrototype.Templates, Is.EqualTo(["my template", "my lycanthrope"]));
        }
    }
}
