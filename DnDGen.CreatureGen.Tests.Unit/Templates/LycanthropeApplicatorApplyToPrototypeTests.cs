using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Creatures;
using NUnit.Framework;

namespace DnDGen.CreatureGen.Tests.Unit.Templates
{
    [TestFixture]
    internal class LycanthropeApplicatorApplyToPrototypeTests : LycanthropeApplicatorTestsBase
    {
        [Test]
        public void ApplyTo_Prototype_ReturnsUpdatedPrototype()
        {
            Assert.Fail("not yet written");
            Assert.Fail("Assert Wisdom +2");
            Assert.Fail("Assert Strength conditional bonus");
            Assert.Fail("Assert Con conditional bonus");
            Assert.Fail("Assert Dex conditional bonus");
            Assert.Fail("Assert CR increase (+2 as default for low hit dice");
            Assert.Fail("Assert level adjustment +2 (afflicted)");
            Assert.Fail("Assert shapechanger subtype added");
        }

        [Test]
        public void ApplyTo_Prototype_ReturnsUpdatedPrototype_WithoutWisdom()
        {
            Assert.Fail("not yet written");
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

            var updatedPrototype = applicator.ApplyTo(creature, false);
            Assert.That(updatedPrototype.Name, Is.EqualTo("my creature"));
            Assert.That(updatedPrototype.Abilities, Has.Count.EqualTo(6));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(96 + Ability.DefaultScore + 4 + 783));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Strength].TemplateAdjustment, Is.EqualTo(4));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(1336 + Ability.DefaultScore + 4 + 22));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Charisma].TemplateAdjustment, Is.EqualTo(4));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(600 + Ability.DefaultScore + 4 + 2015));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Intelligence].TemplateAdjustment, Is.EqualTo(4));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(1337 + Ability.DefaultScore + 4 + 9));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Wisdom].TemplateAdjustment, Is.EqualTo(4));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(42 + Ability.DefaultScore + 4 + 2022));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Dexterity].TemplateAdjustment, Is.EqualTo(4));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(90210 + Ability.DefaultScore + 4 + 8245));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Constitution].TemplateAdjustment, Is.EqualTo(4));
        }

        [TestCaseSource(nameof(ChallengeRatings))]
        public void ApplyTo_Prototype_ReturnsUpdatedPrototype_WithUpdatedChallengeRating(
            string originalChallengeRating,
            double animalHitDiceQuantity,
            string updatedChallengeRating)
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void ApplyTo_Prototype_ReturnsUpdatedPrototype_WithoutLevelAdjustment()
        {
            Assert.Fail("not yet written");
        }

        [TestCaseSource(nameof(LevelAdjustments))]
        public void ApplyTo_Prototype_ReturnsUpdatedPrototype_WithUpdatedLevelAdjustment(int? oldLevelAdjustment, int? newLevelAdjustment, bool isNatural)
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void ApplyTo_PrototypeWithAlignment_ReturnsUpdatedPrototype_WithFilteredAlignment()
        {
            Assert.Fail("not yet written");
        }
    }
}
