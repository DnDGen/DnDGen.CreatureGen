using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Generators.Abilities;
using DnDGen.CreatureGen.Generators.Creatures;
using DnDGen.CreatureGen.Items;
using DnDGen.CreatureGen.Tables;
using DnDGen.CreatureGen.Templates;
using DnDGen.CreatureGen.Tests.Unit.TestCaseSources;
using DnDGen.CreatureGen.Verifiers;
using DnDGen.CreatureGen.Verifiers.Exceptions;
using DnDGen.Infrastructure.Factories;
using DnDGen.Infrastructure.Models;
using DnDGen.Infrastructure.Selectors.Collections;
using DnDGen.RollGen;
using DnDGen.TreasureGen.Items;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace DnDGen.CreatureGen.Tests.Unit.Generators.Abilities
{
    [TestFixture]
    public class AbilitiesGeneratorTests
    {
        private Mock<ICollectionTypeAndAmountSelector> mockTypeAndAmountSelector;
        private Mock<Dice> mockDice;
        private Mock<JustInTimeFactory> mockJustInTimeFactory;
        private Mock<ICreatureVerifier> mockCreatureVerifier;
        private IAbilitiesGenerator abilitiesGenerator;
        private List<TypeAndAmountDataSelection> creatureAbilitySelections;
        private List<TypeAndAmountDataSelection> ageAbilitySelections;
        private Mock<PartialRoll> mockPartialTotal;
        private AbilityRandomizer randomizer;
        private Demographics demographics;

        [SetUp]
        public void Setup()
        {
            mockTypeAndAmountSelector = new Mock<ICollectionTypeAndAmountSelector>();
            mockDice = new Mock<Dice>();
            mockJustInTimeFactory = new Mock<JustInTimeFactory>();
            mockCreatureVerifier = new Mock<ICreatureVerifier>();
            abilitiesGenerator = new AbilitiesGenerator(mockTypeAndAmountSelector.Object, mockDice.Object, mockJustInTimeFactory.Object, mockCreatureVerifier.Object);
            randomizer = new AbilityRandomizer("my roll");
            demographics = new Demographics();

            demographics.Age.Description = "my age category";

            creatureAbilitySelections =
            [
                new TypeAndAmountDataSelection { Type = "ability", AmountAsDouble = 0 },
                new TypeAndAmountDataSelection { Type = "other ability", AmountAsDouble = 9266 },
                new TypeAndAmountDataSelection { Type = "last ability", AmountAsDouble = -90210 },
            ];

            ageAbilitySelections =
            [
                new TypeAndAmountDataSelection { Type = "ability", AmountAsDouble = 0 },
                new TypeAndAmountDataSelection { Type = "other ability", AmountAsDouble = 0 },
                new TypeAndAmountDataSelection { Type = "last ability", AmountAsDouble = 0 },
            ];

            mockTypeAndAmountSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.TypeAndAmount.AbilityAdjustments, "creature name"))
                .Returns(creatureAbilitySelections);
            mockTypeAndAmountSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.TypeAndAmount.AbilityAdjustments, GroupConstants.All))
                .Returns(creatureAbilitySelections);
            mockTypeAndAmountSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.TypeAndAmount.AbilityAdjustments, "my age category"))
                .Returns(ageAbilitySelections);

            mockPartialTotal = new Mock<PartialRoll>();
            mockDice.Setup(d => d.Roll("my roll")).Returns(mockPartialTotal.Object);

            mockPartialTotal.SetupSequence(d => d.AsSum<int>()).Returns(42).Returns(600).Returns(1337);
            mockPartialTotal.Setup(d => d.AsPotentialMaximum<int>(true)).Returns(int.MaxValue);
        }

        [Test]
        public void GenerateFor_GetAbilitiesFromSelections()
        {
            var abilities = abilitiesGenerator.GenerateFor("creature name", false, randomizer, demographics, []);
            Assert.That(abilities["ability"].Name, Is.EqualTo("ability"));
            Assert.That(abilities["ability"].RacialAdjustment, Is.Zero);
            Assert.That(abilities["ability"].AgeAdjustment, Is.Zero);
            Assert.That(abilities["other ability"].Name, Is.EqualTo("other ability"));
            Assert.That(abilities["other ability"].RacialAdjustment, Is.EqualTo(9266));
            Assert.That(abilities["other ability"].AgeAdjustment, Is.Zero);
            Assert.That(abilities["last ability"].Name, Is.EqualTo("last ability"));
            Assert.That(abilities["last ability"].RacialAdjustment, Is.EqualTo(-90210));
            Assert.That(abilities["last ability"].AgeAdjustment, Is.Zero);
        }

        [Test]
        public void GenerateFor_RollBaseScoresForAbilities()
        {
            var abilities = abilitiesGenerator.GenerateFor("creature name", false, randomizer, demographics, []);
            Assert.That(abilities["ability"].Name, Is.EqualTo("ability"));
            Assert.That(abilities["ability"].BaseScore, Is.EqualTo(42));
            Assert.That(abilities["ability"].RacialAdjustment, Is.Zero);
            Assert.That(abilities["ability"].FullScore, Is.EqualTo(42));
            Assert.That(abilities["ability"].HasScore, Is.True);
            Assert.That(abilities["other ability"].Name, Is.EqualTo("other ability"));
            Assert.That(abilities["other ability"].BaseScore, Is.EqualTo(600));
            Assert.That(abilities["other ability"].RacialAdjustment, Is.EqualTo(9266));
            Assert.That(abilities["other ability"].FullScore, Is.EqualTo(9866));
            Assert.That(abilities["other ability"].HasScore, Is.True);
            Assert.That(abilities["last ability"].Name, Is.EqualTo("last ability"));
            Assert.That(abilities["last ability"].BaseScore, Is.EqualTo(1337));
            Assert.That(abilities["last ability"].RacialAdjustment, Is.EqualTo(-90210));
            Assert.That(abilities["last ability"].FullScore, Is.EqualTo(1));
            Assert.That(abilities["last ability"].HasScore, Is.True);
        }

        [Test]
        public void GenerateFor_RollBaseScoresForAbilities_WithDefaultRandomizer()
        {
            mockDice
                .SetupSequence(d => d.Roll(AbilityConstants.RandomizerRolls.Default).AsSum<int>())
                .Returns(96)
                .Returns(783)
                .Returns(8245);

            var abilities = abilitiesGenerator.GenerateFor("creature name", false, null, demographics, []);
            Assert.That(abilities["ability"].Name, Is.EqualTo("ability"));
            Assert.That(abilities["ability"].BaseScore, Is.EqualTo(96));
            Assert.That(abilities["ability"].RacialAdjustment, Is.Zero);
            Assert.That(abilities["ability"].FullScore, Is.EqualTo(96));
            Assert.That(abilities["ability"].HasScore, Is.True);
            Assert.That(abilities["other ability"].Name, Is.EqualTo("other ability"));
            Assert.That(abilities["other ability"].BaseScore, Is.EqualTo(783));
            Assert.That(abilities["other ability"].RacialAdjustment, Is.EqualTo(9266));
            Assert.That(abilities["other ability"].FullScore, Is.EqualTo(783 + 9266));
            Assert.That(abilities["other ability"].HasScore, Is.True);
            Assert.That(abilities["last ability"].Name, Is.EqualTo("last ability"));
            Assert.That(abilities["last ability"].BaseScore, Is.EqualTo(8245));
            Assert.That(abilities["last ability"].RacialAdjustment, Is.EqualTo(-90210));
            Assert.That(abilities["last ability"].FullScore, Is.EqualTo(1));
            Assert.That(abilities["last ability"].HasScore, Is.True);
        }

        [Test]
        public void GenerateFor_MissingAbilitiesHaveNoScore()
        {
            var allAbilities = new[]
            {
                new TypeAndAmountDataSelection { Type = "ability" },
                new TypeAndAmountDataSelection { Type = "other ability" },
                new TypeAndAmountDataSelection { Type = "last ability" }
            };

            mockTypeAndAmountSelector.Setup(s => s.SelectFrom(Config.Name, TableNameConstants.TypeAndAmount.AbilityAdjustments, GroupConstants.All)).Returns(allAbilities);

            creatureAbilitySelections.RemoveAt(1);

            var abilities = abilitiesGenerator.GenerateFor("creature name", false, randomizer, demographics, []);
            Assert.That(abilities["ability"].Name, Is.EqualTo("ability"));
            Assert.That(abilities["ability"].BaseScore, Is.EqualTo(42));
            Assert.That(abilities["ability"].RacialAdjustment, Is.Zero);
            Assert.That(abilities["ability"].FullScore, Is.EqualTo(42));
            Assert.That(abilities["ability"].HasScore, Is.True);
            Assert.That(abilities["other ability"].Name, Is.EqualTo("other ability"));
            Assert.That(abilities["other ability"].BaseScore, Is.Zero);
            Assert.That(abilities["other ability"].RacialAdjustment, Is.Zero);
            Assert.That(abilities["other ability"].FullScore, Is.Zero);
            Assert.That(abilities["other ability"].HasScore, Is.False);
            Assert.That(abilities["last ability"].Name, Is.EqualTo("last ability"));
            Assert.That(abilities["last ability"].BaseScore, Is.EqualTo(600)); //INFO: 600 instead of 1337 because we never rolled for the ability without a score
            Assert.That(abilities["last ability"].RacialAdjustment, Is.EqualTo(-90210));
            Assert.That(abilities["last ability"].FullScore, Is.EqualTo(1));
            Assert.That(abilities["last ability"].HasScore, Is.True);
        }

        [Test]
        public void GenerateFor_ApplyAbilityAdvancement()
        {
            randomizer.AbilityAdvancements["other ability"] = 1336;

            var abilities = abilitiesGenerator.GenerateFor("creature name", false, randomizer, demographics, []);
            Assert.That(abilities["ability"].Name, Is.EqualTo("ability"));
            Assert.That(abilities["ability"].RacialAdjustment, Is.Zero);
            Assert.That(abilities["ability"].AdvancementAdjustment, Is.Zero);
            Assert.That(abilities["other ability"].Name, Is.EqualTo("other ability"));
            Assert.That(abilities["other ability"].RacialAdjustment, Is.EqualTo(9266));
            Assert.That(abilities["other ability"].AdvancementAdjustment, Is.EqualTo(1336));
            Assert.That(abilities["last ability"].Name, Is.EqualTo("last ability"));
            Assert.That(abilities["last ability"].RacialAdjustment, Is.EqualTo(-90210));
            Assert.That(abilities["last ability"].AdvancementAdjustment, Is.Zero);
        }

        [Test]
        public void GenerateFor_ApplyAbilityAdvancements()
        {
            randomizer.AbilityAdvancements["ability"] = 1336;
            randomizer.AbilityAdvancements["other ability"] = 96;

            var abilities = abilitiesGenerator.GenerateFor("creature name", false, randomizer, demographics, []);
            Assert.That(abilities["ability"].Name, Is.EqualTo("ability"));
            Assert.That(abilities["ability"].RacialAdjustment, Is.Zero);
            Assert.That(abilities["ability"].AdvancementAdjustment, Is.EqualTo(1336));
            Assert.That(abilities["other ability"].Name, Is.EqualTo("other ability"));
            Assert.That(abilities["other ability"].RacialAdjustment, Is.EqualTo(9266));
            Assert.That(abilities["other ability"].AdvancementAdjustment, Is.EqualTo(96));
            Assert.That(abilities["last ability"].Name, Is.EqualTo("last ability"));
            Assert.That(abilities["last ability"].RacialAdjustment, Is.EqualTo(-90210));
            Assert.That(abilities["last ability"].AdvancementAdjustment, Is.Zero);
        }

        [Test]
        public void GenerateFor_ApplySetAbilityScore()
        {
            randomizer.SetRolls["other ability"] = 1336;

            var abilities = abilitiesGenerator.GenerateFor("creature name", false, randomizer, demographics, []);
            Assert.That(abilities["ability"].Name, Is.EqualTo("ability"));
            Assert.That(abilities["ability"].BaseScore, Is.EqualTo(42));
            Assert.That(abilities["ability"].RacialAdjustment, Is.Zero);
            Assert.That(abilities["ability"].FullScore, Is.EqualTo(42));
            Assert.That(abilities["ability"].HasScore, Is.True);
            Assert.That(abilities["other ability"].Name, Is.EqualTo("other ability"));
            Assert.That(abilities["other ability"].BaseScore, Is.EqualTo(1336));
            Assert.That(abilities["other ability"].RacialAdjustment, Is.EqualTo(9266));
            Assert.That(abilities["other ability"].FullScore, Is.EqualTo(1336 + 9266));
            Assert.That(abilities["other ability"].HasScore, Is.True);
            Assert.That(abilities["last ability"].Name, Is.EqualTo("last ability"));
            Assert.That(abilities["last ability"].BaseScore, Is.EqualTo(600));
            Assert.That(abilities["last ability"].RacialAdjustment, Is.EqualTo(-90210));
            Assert.That(abilities["last ability"].FullScore, Is.EqualTo(1));
            Assert.That(abilities["last ability"].HasScore, Is.True);
        }

        [Test]
        public void GenerateFor_ApplySetAbilityScores()
        {
            randomizer.SetRolls["ability"] = 1336;
            randomizer.SetRolls["other ability"] = 96;

            var abilities = abilitiesGenerator.GenerateFor("creature name", false, randomizer, demographics, []);
            Assert.That(abilities["ability"].Name, Is.EqualTo("ability"));
            Assert.That(abilities["ability"].BaseScore, Is.EqualTo(1336));
            Assert.That(abilities["ability"].RacialAdjustment, Is.Zero);
            Assert.That(abilities["ability"].FullScore, Is.EqualTo(1336));
            Assert.That(abilities["ability"].HasScore, Is.True);
            Assert.That(abilities["other ability"].Name, Is.EqualTo("other ability"));
            Assert.That(abilities["other ability"].BaseScore, Is.EqualTo(96));
            Assert.That(abilities["other ability"].RacialAdjustment, Is.EqualTo(9266));
            Assert.That(abilities["other ability"].FullScore, Is.EqualTo(96 + 9266));
            Assert.That(abilities["other ability"].HasScore, Is.True);
            Assert.That(abilities["last ability"].Name, Is.EqualTo("last ability"));
            Assert.That(abilities["last ability"].BaseScore, Is.EqualTo(42));
            Assert.That(abilities["last ability"].RacialAdjustment, Is.EqualTo(-90210));
            Assert.That(abilities["last ability"].FullScore, Is.EqualTo(1));
            Assert.That(abilities["last ability"].HasScore, Is.True);
        }

        [Test]
        public void GenerateFor_ApplyPriorityAbility()
        {
            randomizer.PriorityAbility = "ability";

            var abilities = abilitiesGenerator.GenerateFor("creature name", false, randomizer, demographics, []);
            Assert.That(abilities["ability"].Name, Is.EqualTo("ability"));
            Assert.That(abilities["ability"].BaseScore, Is.EqualTo(1337));
            Assert.That(abilities["ability"].RacialAdjustment, Is.Zero);
            Assert.That(abilities["ability"].FullScore, Is.EqualTo(1337));
            Assert.That(abilities["ability"].HasScore, Is.True);
            Assert.That(abilities["other ability"].Name, Is.EqualTo("other ability"));
            Assert.That(abilities["other ability"].BaseScore, Is.EqualTo(600));
            Assert.That(abilities["other ability"].RacialAdjustment, Is.EqualTo(9266));
            Assert.That(abilities["other ability"].FullScore, Is.EqualTo(9866));
            Assert.That(abilities["other ability"].HasScore, Is.True);
            Assert.That(abilities["last ability"].Name, Is.EqualTo("last ability"));
            Assert.That(abilities["last ability"].BaseScore, Is.EqualTo(42));
            Assert.That(abilities["last ability"].RacialAdjustment, Is.EqualTo(-90210));
            Assert.That(abilities["last ability"].FullScore, Is.EqualTo(1));
            Assert.That(abilities["last ability"].HasScore, Is.True);
        }

        [Test]
        public void GenerateFor_ApplyPriorityAbility_PriorityIsHighest()
        {
            randomizer.PriorityAbility = "last ability";

            var abilities = abilitiesGenerator.GenerateFor("creature name", false, randomizer, demographics, []);
            Assert.That(abilities["ability"].Name, Is.EqualTo("ability"));
            Assert.That(abilities["ability"].BaseScore, Is.EqualTo(42));
            Assert.That(abilities["ability"].RacialAdjustment, Is.Zero);
            Assert.That(abilities["ability"].AgeAdjustment, Is.Zero);
            Assert.That(abilities["ability"].FullScore, Is.EqualTo(42));
            Assert.That(abilities["ability"].HasScore, Is.True);
            Assert.That(abilities["other ability"].Name, Is.EqualTo("other ability"));
            Assert.That(abilities["other ability"].BaseScore, Is.EqualTo(600));
            Assert.That(abilities["other ability"].RacialAdjustment, Is.EqualTo(9266));
            Assert.That(abilities["other ability"].AgeAdjustment, Is.Zero);
            Assert.That(abilities["other ability"].FullScore, Is.EqualTo(9866));
            Assert.That(abilities["other ability"].HasScore, Is.True);
            Assert.That(abilities["last ability"].Name, Is.EqualTo("last ability"));
            Assert.That(abilities["last ability"].BaseScore, Is.EqualTo(1337));
            Assert.That(abilities["last ability"].RacialAdjustment, Is.EqualTo(-90210));
            Assert.That(abilities["last ability"].AgeAdjustment, Is.Zero);
            Assert.That(abilities["last ability"].FullScore, Is.EqualTo(1));
            Assert.That(abilities["last ability"].HasScore, Is.True);
        }

        [Test]
        public void GenerateFor_ApplyAgeCategoryModifiers()
        {
            ageAbilitySelections[0].AmountAsDouble = -1;
            ageAbilitySelections[1].AmountAsDouble = -2;
            ageAbilitySelections[2].AmountAsDouble = -3;

            var abilities = abilitiesGenerator.GenerateFor("creature name", false, randomizer, demographics, []);
            Assert.That(abilities["ability"].Name, Is.EqualTo("ability"));
            Assert.That(abilities["ability"].RacialAdjustment, Is.Zero);
            Assert.That(abilities["ability"].AdvancementAdjustment, Is.Zero);
            Assert.That(abilities["ability"].AgeAdjustment, Is.EqualTo(-1));
            Assert.That(abilities["other ability"].Name, Is.EqualTo("other ability"));
            Assert.That(abilities["other ability"].RacialAdjustment, Is.EqualTo(9266));
            Assert.That(abilities["other ability"].AdvancementAdjustment, Is.Zero);
            Assert.That(abilities["other ability"].AgeAdjustment, Is.EqualTo(-2));
            Assert.That(abilities["last ability"].Name, Is.EqualTo("last ability"));
            Assert.That(abilities["last ability"].RacialAdjustment, Is.EqualTo(-90210));
            Assert.That(abilities["last ability"].AdvancementAdjustment, Is.Zero);
            Assert.That(abilities["last ability"].AgeAdjustment, Is.EqualTo(-3));
        }

        [Test]
        public void GenerateFor_ApplyAllModifiers()
        {
            randomizer.AbilityAdvancements["ability"] = 1336;
            randomizer.AbilityAdvancements["other ability"] = -96;
            randomizer.SetRolls["last ability"] = 783;
            randomizer.SetRolls["other ability"] = 8245;
            randomizer.PriorityAbility = "last ability";

            ageAbilitySelections[0].AmountAsDouble = -1;
            ageAbilitySelections[1].AmountAsDouble = -2;
            ageAbilitySelections[2].AmountAsDouble = -3;

            var abilities = abilitiesGenerator.GenerateFor("creature name", false, randomizer, demographics, []);
            Assert.That(abilities["ability"].Name, Is.EqualTo("ability"));
            Assert.That(abilities["ability"].BaseScore, Is.EqualTo(42));
            Assert.That(abilities["ability"].RacialAdjustment, Is.Zero);
            Assert.That(abilities["ability"].AgeAdjustment, Is.EqualTo(-1));
            Assert.That(abilities["ability"].AdvancementAdjustment, Is.EqualTo(1336));
            Assert.That(abilities["ability"].FullScore, Is.EqualTo(42 + 1336 - 1));
            Assert.That(abilities["ability"].HasScore, Is.True);
            Assert.That(abilities["other ability"].Name, Is.EqualTo("other ability"));
            Assert.That(abilities["other ability"].BaseScore, Is.EqualTo(783));
            Assert.That(abilities["other ability"].RacialAdjustment, Is.EqualTo(9266));
            Assert.That(abilities["other ability"].AgeAdjustment, Is.EqualTo(-2));
            Assert.That(abilities["other ability"].AdvancementAdjustment, Is.EqualTo(-96));
            Assert.That(abilities["other ability"].FullScore, Is.EqualTo(783 + 9266 - 96 - 2));
            Assert.That(abilities["other ability"].HasScore, Is.True);
            Assert.That(abilities["last ability"].Name, Is.EqualTo("last ability"));
            Assert.That(abilities["last ability"].BaseScore, Is.EqualTo(8245));
            Assert.That(abilities["last ability"].RacialAdjustment, Is.EqualTo(-90210));
            Assert.That(abilities["last ability"].AgeAdjustment, Is.EqualTo(-3));
            Assert.That(abilities["last ability"].AdvancementAdjustment, Is.Zero);
            Assert.That(abilities["last ability"].FullScore, Is.EqualTo(1));
            Assert.That(abilities["last ability"].HasScore, Is.True);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void GenerateFor_NoTemplates_IsValid(bool asCharacter)
        {
            mockCreatureVerifier
                .Setup(v => v.VerifyCompatibility(
                    asCharacter,
                    "creature name",
                    randomizer,
                    It.Is<Filters>(f => f.Templates.IsEquivalentTo(new[] { CreatureConstants.Templates.None }))))
                .Returns(true);

            var abilities = abilitiesGenerator.GenerateFor("creature name", asCharacter, randomizer, demographics, []);
            Assert.That(abilities["ability"].Name, Is.EqualTo("ability"));
            Assert.That(abilities["ability"].BaseScore, Is.EqualTo(42));
            Assert.That(abilities["ability"].RacialAdjustment, Is.Zero);
            Assert.That(abilities["ability"].FullScore, Is.EqualTo(42));
            Assert.That(abilities["ability"].HasScore, Is.True);
            Assert.That(abilities["other ability"].Name, Is.EqualTo("other ability"));
            Assert.That(abilities["other ability"].BaseScore, Is.EqualTo(600));
            Assert.That(abilities["other ability"].RacialAdjustment, Is.EqualTo(9266));
            Assert.That(abilities["other ability"].FullScore, Is.EqualTo(9866));
            Assert.That(abilities["other ability"].HasScore, Is.True);
            Assert.That(abilities["last ability"].Name, Is.EqualTo("last ability"));
            Assert.That(abilities["last ability"].BaseScore, Is.EqualTo(1337));
            Assert.That(abilities["last ability"].RacialAdjustment, Is.EqualTo(-90210));
            Assert.That(abilities["last ability"].FullScore, Is.EqualTo(1));
            Assert.That(abilities["last ability"].HasScore, Is.True);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void GenerateFor_1Template_IsValid(bool asCharacter)
        {
            mockCreatureVerifier
                .Setup(v => v.VerifyCompatibility(
                    asCharacter,
                    "creature name",
                    randomizer,
                    It.Is<Filters>(f => f.Templates.IsEquivalentTo(new[] { "my template" }))))
                .Returns(true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var abilities = abilitiesGenerator.GenerateFor("creature name", asCharacter, randomizer, demographics, ["my template"]);
            Assert.That(abilities["ability"].Name, Is.EqualTo("ability"));
            Assert.That(abilities["ability"].BaseScore, Is.EqualTo(42));
            Assert.That(abilities["ability"].RacialAdjustment, Is.Zero);
            Assert.That(abilities["ability"].FullScore, Is.EqualTo(42));
            Assert.That(abilities["ability"].HasScore, Is.True);
            Assert.That(abilities["other ability"].Name, Is.EqualTo("other ability"));
            Assert.That(abilities["other ability"].BaseScore, Is.EqualTo(600));
            Assert.That(abilities["other ability"].RacialAdjustment, Is.EqualTo(9266));
            Assert.That(abilities["other ability"].FullScore, Is.EqualTo(9866));
            Assert.That(abilities["other ability"].HasScore, Is.True);
            Assert.That(abilities["last ability"].Name, Is.EqualTo("last ability"));
            Assert.That(abilities["last ability"].BaseScore, Is.EqualTo(1337));
            Assert.That(abilities["last ability"].RacialAdjustment, Is.EqualTo(-90210));
            Assert.That(abilities["last ability"].FullScore, Is.EqualTo(1));
            Assert.That(abilities["last ability"].HasScore, Is.True);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void GenerateFor_1Template_IsNotValid(bool asCharacter)
        {
            mockCreatureVerifier
                .Setup(v => v.VerifyCompatibility(
                    asCharacter,
                    "creature name",
                    randomizer,
                    It.Is<Filters>(f => f.Templates.IsEquivalentTo(new[] { "my template" }))))
                .Returns(false);

            var generation = () => abilitiesGenerator.GenerateFor("creature name", asCharacter, randomizer, demographics, ["my template"]);
            Assert.That(generation,
                Throws.InstanceOf<InvalidCreatureException>().With.Message.Contains("creature name does not have sufficient ability for template my template"));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void GenerateFor_2Templates_IsValid(bool asCharacter)
        {
            mockCreatureVerifier
                .Setup(v => v.VerifyCompatibility(
                    asCharacter,
                    "creature name",
                    randomizer,
                    It.Is<Filters>(f => f.Templates.IsEquivalentTo(new[] { "my template", "my other template" }))))
                .Returns(true);

            var mockTemplateApplicator1 = new Mock<TemplateApplicator>();
            var mockTemplateApplicator2 = new Mock<TemplateApplicator>();
            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator1.Object);
            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my other template")).Returns(mockTemplateApplicator2.Object);

            var abilities = abilitiesGenerator.GenerateFor("creature name", asCharacter, randomizer, demographics, ["my template", "my other template"]);
            Assert.That(abilities["ability"].Name, Is.EqualTo("ability"));
            Assert.That(abilities["ability"].BaseScore, Is.EqualTo(42));
            Assert.That(abilities["ability"].RacialAdjustment, Is.Zero);
            Assert.That(abilities["ability"].FullScore, Is.EqualTo(42));
            Assert.That(abilities["ability"].HasScore, Is.True);
            Assert.That(abilities["other ability"].Name, Is.EqualTo("other ability"));
            Assert.That(abilities["other ability"].BaseScore, Is.EqualTo(600));
            Assert.That(abilities["other ability"].RacialAdjustment, Is.EqualTo(9266));
            Assert.That(abilities["other ability"].FullScore, Is.EqualTo(9866));
            Assert.That(abilities["other ability"].HasScore, Is.True);
            Assert.That(abilities["last ability"].Name, Is.EqualTo("last ability"));
            Assert.That(abilities["last ability"].BaseScore, Is.EqualTo(1337));
            Assert.That(abilities["last ability"].RacialAdjustment, Is.EqualTo(-90210));
            Assert.That(abilities["last ability"].FullScore, Is.EqualTo(1));
            Assert.That(abilities["last ability"].HasScore, Is.True);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void GenerateFor_2Templates_IsNotValid(bool asCharacter)
        {
            mockCreatureVerifier
                .Setup(v => v.VerifyCompatibility(
                    asCharacter,
                    "creature name",
                    randomizer,
                    It.Is<Filters>(f => f.Templates.IsEquivalentTo(new[] { "my template", "my other template" }))))
                .Returns(false);

            var generation = () => abilitiesGenerator.GenerateFor("creature name", asCharacter, randomizer, demographics, ["my template", "my other template"]);
            Assert.That(generation,
                Throws.InstanceOf<InvalidCreatureException>().With.Message.Contains("creature name does not have sufficient ability for template my template"));
        }

        [Test]
        public void GenerateFor_AppliesTemplateMinimums_NoTemplates()
        {
            var abilities = abilitiesGenerator.GenerateFor("creature name", false, randomizer, demographics, []);
            Assert.That(abilities["ability"].Name, Is.EqualTo("ability"));
            Assert.That(abilities["ability"].BaseScore, Is.EqualTo(42));
            Assert.That(abilities["ability"].RacialAdjustment, Is.Zero);
            Assert.That(abilities["ability"].FullScore, Is.EqualTo(42));
            Assert.That(abilities["ability"].HasScore, Is.True);
            Assert.That(abilities["other ability"].Name, Is.EqualTo("other ability"));
            Assert.That(abilities["other ability"].BaseScore, Is.EqualTo(600));
            Assert.That(abilities["other ability"].RacialAdjustment, Is.EqualTo(9266));
            Assert.That(abilities["other ability"].FullScore, Is.EqualTo(9866));
            Assert.That(abilities["other ability"].HasScore, Is.True);
            Assert.That(abilities["last ability"].Name, Is.EqualTo("last ability"));
            Assert.That(abilities["last ability"].BaseScore, Is.EqualTo(1337));
            Assert.That(abilities["last ability"].RacialAdjustment, Is.EqualTo(-90210));
            Assert.That(abilities["last ability"].FullScore, Is.EqualTo(1));
            Assert.That(abilities["last ability"].HasScore, Is.True);
        }

        [Test]
        public void GenerateFor_AppliesTemplateMinimums_TemplateDoesNotHaveMinimumAbility()
        {
            mockCreatureVerifier
                .Setup(v => v.VerifyCompatibility(
                    false,
                    "creature name",
                    randomizer,
                    It.Is<Filters>(f => f.Templates.IsEquivalentTo(new[] { "my template" }))))
                .Returns(true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            Ability noMin = null;
            mockTemplateApplicator.SetupGet(a => a.MinimumAbility).Returns(noMin);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var abilities = abilitiesGenerator.GenerateFor("creature name", false, randomizer, demographics, ["my template"]);
            Assert.That(abilities["ability"].Name, Is.EqualTo("ability"));
            Assert.That(abilities["ability"].BaseScore, Is.EqualTo(42));
            Assert.That(abilities["ability"].RacialAdjustment, Is.Zero);
            Assert.That(abilities["ability"].FullScore, Is.EqualTo(42));
            Assert.That(abilities["ability"].HasScore, Is.True);
            Assert.That(abilities["other ability"].Name, Is.EqualTo("other ability"));
            Assert.That(abilities["other ability"].BaseScore, Is.EqualTo(600));
            Assert.That(abilities["other ability"].RacialAdjustment, Is.EqualTo(9266));
            Assert.That(abilities["other ability"].FullScore, Is.EqualTo(9866));
            Assert.That(abilities["other ability"].HasScore, Is.True);
            Assert.That(abilities["last ability"].Name, Is.EqualTo("last ability"));
            Assert.That(abilities["last ability"].BaseScore, Is.EqualTo(1337));
            Assert.That(abilities["last ability"].RacialAdjustment, Is.EqualTo(-90210));
            Assert.That(abilities["last ability"].FullScore, Is.EqualTo(1));
            Assert.That(abilities["last ability"].HasScore, Is.True);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(10)]
        public void GenerateFor_AppliesTemplateMinimums_AbilityMeetsTemplateMinimum_Roll(int difference)
        {
            mockCreatureVerifier
                .Setup(v => v.VerifyCompatibility(
                    false,
                    "creature name",
                    randomizer,
                    It.Is<Filters>(f => f.Templates.IsEquivalentTo(new[] { "my template" }))))
                .Returns(true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            var minimum = new Ability("ability") { BaseScore = 42 - difference };
            mockTemplateApplicator.SetupGet(a => a.MinimumAbility).Returns(minimum);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var abilities = abilitiesGenerator.GenerateFor("creature name", false, randomizer, demographics, ["my template"]);
            Assert.That(abilities["ability"].Name, Is.EqualTo("ability"));
            Assert.That(abilities["ability"].BaseScore, Is.EqualTo(42));
            Assert.That(abilities["ability"].RacialAdjustment, Is.Zero);
            Assert.That(abilities["ability"].FullScore, Is.EqualTo(42));
            Assert.That(abilities["ability"].HasScore, Is.True);
            Assert.That(abilities["other ability"].Name, Is.EqualTo("other ability"));
            Assert.That(abilities["other ability"].BaseScore, Is.EqualTo(600));
            Assert.That(abilities["other ability"].RacialAdjustment, Is.EqualTo(9266));
            Assert.That(abilities["other ability"].FullScore, Is.EqualTo(9866));
            Assert.That(abilities["other ability"].HasScore, Is.True);
            Assert.That(abilities["last ability"].Name, Is.EqualTo("last ability"));
            Assert.That(abilities["last ability"].BaseScore, Is.EqualTo(1337));
            Assert.That(abilities["last ability"].RacialAdjustment, Is.EqualTo(-90210));
            Assert.That(abilities["last ability"].FullScore, Is.EqualTo(1));
            Assert.That(abilities["last ability"].HasScore, Is.True);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(10)]
        public void GenerateFor_AppliesTemplateMinimums_AbilityMeetsTemplateMinimum_Set(int difference)
        {
            mockCreatureVerifier
                .Setup(v => v.VerifyCompatibility(
                    false,
                    "creature name",
                    randomizer,
                    It.Is<Filters>(f => f.Templates.IsEquivalentTo(new[] { "my template" }))))
                .Returns(true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            var minimum = new Ability("ability") { BaseScore = 1336 - difference };
            mockTemplateApplicator.SetupGet(a => a.MinimumAbility).Returns(minimum);

            randomizer.SetRolls["ability"] = 1336;

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var abilities = abilitiesGenerator.GenerateFor("creature name", false, randomizer, demographics, ["my template"]);
            Assert.That(abilities["ability"].Name, Is.EqualTo("ability"));
            Assert.That(abilities["ability"].BaseScore, Is.EqualTo(1336));
            Assert.That(abilities["ability"].RacialAdjustment, Is.Zero);
            Assert.That(abilities["ability"].FullScore, Is.EqualTo(1336));
            Assert.That(abilities["ability"].HasScore, Is.True);
            Assert.That(abilities["other ability"].Name, Is.EqualTo("other ability"));
            Assert.That(abilities["other ability"].BaseScore, Is.EqualTo(42));
            Assert.That(abilities["other ability"].RacialAdjustment, Is.EqualTo(9266));
            Assert.That(abilities["other ability"].FullScore, Is.EqualTo(42 + 9266));
            Assert.That(abilities["other ability"].HasScore, Is.True);
            Assert.That(abilities["last ability"].Name, Is.EqualTo("last ability"));
            Assert.That(abilities["last ability"].BaseScore, Is.EqualTo(600));
            Assert.That(abilities["last ability"].RacialAdjustment, Is.EqualTo(-90210));
            Assert.That(abilities["last ability"].FullScore, Is.EqualTo(1));
            Assert.That(abilities["last ability"].HasScore, Is.True);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(10)]
        public void GenerateFor_AppliesTemplateMinimums_AbilityMeetsTemplateMinimum_WithPositiveRacialAdjustment(int difference)
        {
            mockCreatureVerifier
                .Setup(v => v.VerifyCompatibility(
                    false,
                    "creature name",
                    randomizer,
                    It.Is<Filters>(f => f.Templates.IsEquivalentTo(new[] { "my template" }))))
                .Returns(true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            var minimum = new Ability("other ability") { BaseScore = 9866 - difference };
            mockTemplateApplicator.SetupGet(a => a.MinimumAbility).Returns(minimum);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var abilities = abilitiesGenerator.GenerateFor("creature name", false, randomizer, demographics, ["my template"]);
            Assert.That(abilities["ability"].Name, Is.EqualTo("ability"));
            Assert.That(abilities["ability"].BaseScore, Is.EqualTo(42));
            Assert.That(abilities["ability"].RacialAdjustment, Is.Zero);
            Assert.That(abilities["ability"].FullScore, Is.EqualTo(42));
            Assert.That(abilities["ability"].HasScore, Is.True);
            Assert.That(abilities["other ability"].Name, Is.EqualTo("other ability"));
            Assert.That(abilities["other ability"].BaseScore, Is.EqualTo(600));
            Assert.That(abilities["other ability"].RacialAdjustment, Is.EqualTo(9266));
            Assert.That(abilities["other ability"].FullScore, Is.EqualTo(9866));
            Assert.That(abilities["other ability"].HasScore, Is.True);
            Assert.That(abilities["last ability"].Name, Is.EqualTo("last ability"));
            Assert.That(abilities["last ability"].BaseScore, Is.EqualTo(1337));
            Assert.That(abilities["last ability"].RacialAdjustment, Is.EqualTo(-90210));
            Assert.That(abilities["last ability"].FullScore, Is.EqualTo(1));
            Assert.That(abilities["last ability"].HasScore, Is.True);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(10)]
        public void GenerateFor_AppliesTemplateMinimums_AbilityMeetsTemplateMinimum_WithNegativeRacialAdjustment(int difference)
        {
            mockCreatureVerifier
                .Setup(v => v.VerifyCompatibility(
                    false,
                    "creature name",
                    randomizer,
                    It.Is<Filters>(f => f.Templates.IsEquivalentTo(new[] { "my template" }))))
                .Returns(true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            var minimum = new Ability("other ability") { BaseScore = 600 - 13 - difference };
            mockTemplateApplicator.SetupGet(a => a.MinimumAbility).Returns(minimum);

            creatureAbilitySelections[1].AmountAsDouble = -13;

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var abilities = abilitiesGenerator.GenerateFor("creature name", false, randomizer, demographics, ["my template"]);
            Assert.That(abilities["ability"].Name, Is.EqualTo("ability"));
            Assert.That(abilities["ability"].BaseScore, Is.EqualTo(42));
            Assert.That(abilities["ability"].RacialAdjustment, Is.Zero);
            Assert.That(abilities["ability"].FullScore, Is.EqualTo(42));
            Assert.That(abilities["ability"].HasScore, Is.True);
            Assert.That(abilities["other ability"].Name, Is.EqualTo("other ability"));
            Assert.That(abilities["other ability"].BaseScore, Is.EqualTo(600));
            Assert.That(abilities["other ability"].RacialAdjustment, Is.EqualTo(-13));
            Assert.That(abilities["other ability"].FullScore, Is.EqualTo(600 - 13));
            Assert.That(abilities["other ability"].HasScore, Is.True);
            Assert.That(abilities["last ability"].Name, Is.EqualTo("last ability"));
            Assert.That(abilities["last ability"].BaseScore, Is.EqualTo(1337));
            Assert.That(abilities["last ability"].RacialAdjustment, Is.EqualTo(-90210));
            Assert.That(abilities["last ability"].FullScore, Is.EqualTo(1));
            Assert.That(abilities["last ability"].HasScore, Is.True);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(10)]
        public void GenerateFor_AppliesTemplateMinimums_AbilityMeetsTemplateMinimum_WithPositiveAgeAdjustment(int difference)
        {
            mockCreatureVerifier
                .Setup(v => v.VerifyCompatibility(
                    false,
                    "creature name",
                    randomizer,
                    It.Is<Filters>(f => f.Templates.IsEquivalentTo(new[] { "my template" }))))
                .Returns(true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            var minimum = new Ability("ability") { BaseScore = 43 - difference };
            mockTemplateApplicator.SetupGet(a => a.MinimumAbility).Returns(minimum);

            ageAbilitySelections[0].AmountAsDouble = 1;
            ageAbilitySelections[1].AmountAsDouble = 2;
            ageAbilitySelections[2].AmountAsDouble = 3;

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var abilities = abilitiesGenerator.GenerateFor("creature name", false, randomizer, demographics, ["my template"]);
            Assert.That(abilities["ability"].Name, Is.EqualTo("ability"));
            Assert.That(abilities["ability"].BaseScore, Is.EqualTo(42));
            Assert.That(abilities["ability"].RacialAdjustment, Is.Zero);
            Assert.That(abilities["ability"].AdvancementAdjustment, Is.Zero);
            Assert.That(abilities["ability"].AgeAdjustment, Is.EqualTo(1));
            Assert.That(abilities["ability"].FullScore, Is.EqualTo(43));
            Assert.That(abilities["ability"].HasScore, Is.True);
            Assert.That(abilities["other ability"].Name, Is.EqualTo("other ability"));
            Assert.That(abilities["other ability"].BaseScore, Is.EqualTo(600));
            Assert.That(abilities["other ability"].RacialAdjustment, Is.EqualTo(9266));
            Assert.That(abilities["other ability"].AdvancementAdjustment, Is.Zero);
            Assert.That(abilities["other ability"].AgeAdjustment, Is.EqualTo(2));
            Assert.That(abilities["other ability"].FullScore, Is.EqualTo(9868));
            Assert.That(abilities["other ability"].HasScore, Is.True);
            Assert.That(abilities["last ability"].Name, Is.EqualTo("last ability"));
            Assert.That(abilities["last ability"].BaseScore, Is.EqualTo(1337));
            Assert.That(abilities["last ability"].RacialAdjustment, Is.EqualTo(-90210));
            Assert.That(abilities["last ability"].AdvancementAdjustment, Is.Zero);
            Assert.That(abilities["last ability"].AgeAdjustment, Is.EqualTo(3));
            Assert.That(abilities["last ability"].FullScore, Is.EqualTo(1));
            Assert.That(abilities["last ability"].HasScore, Is.True);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(10)]
        public void GenerateFor_AppliesTemplateMinimums_AbilityMeetsTemplateMinimum_WithNegativeAgeAdjustment(int difference)
        {
            mockCreatureVerifier
                .Setup(v => v.VerifyCompatibility(
                    false,
                    "creature name",
                    randomizer,
                    It.Is<Filters>(f => f.Templates.IsEquivalentTo(new[] { "my template" }))))
                .Returns(true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            var minimum = new Ability("ability") { BaseScore = 41 - difference };
            mockTemplateApplicator.SetupGet(a => a.MinimumAbility).Returns(minimum);

            ageAbilitySelections[0].AmountAsDouble = -1;
            ageAbilitySelections[1].AmountAsDouble = -2;
            ageAbilitySelections[2].AmountAsDouble = -3;

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var abilities = abilitiesGenerator.GenerateFor("creature name", false, randomizer, demographics, ["my template"]);
            Assert.That(abilities["ability"].Name, Is.EqualTo("ability"));
            Assert.That(abilities["ability"].BaseScore, Is.EqualTo(42));
            Assert.That(abilities["ability"].RacialAdjustment, Is.Zero);
            Assert.That(abilities["ability"].AdvancementAdjustment, Is.Zero);
            Assert.That(abilities["ability"].AgeAdjustment, Is.EqualTo(-1));
            Assert.That(abilities["ability"].FullScore, Is.EqualTo(41));
            Assert.That(abilities["ability"].HasScore, Is.True);
            Assert.That(abilities["other ability"].Name, Is.EqualTo("other ability"));
            Assert.That(abilities["other ability"].BaseScore, Is.EqualTo(600));
            Assert.That(abilities["other ability"].RacialAdjustment, Is.EqualTo(9266));
            Assert.That(abilities["other ability"].AdvancementAdjustment, Is.Zero);
            Assert.That(abilities["other ability"].AgeAdjustment, Is.EqualTo(-2));
            Assert.That(abilities["other ability"].FullScore, Is.EqualTo(9864));
            Assert.That(abilities["other ability"].HasScore, Is.True);
            Assert.That(abilities["last ability"].Name, Is.EqualTo("last ability"));
            Assert.That(abilities["last ability"].BaseScore, Is.EqualTo(1337));
            Assert.That(abilities["last ability"].RacialAdjustment, Is.EqualTo(-90210));
            Assert.That(abilities["last ability"].AdvancementAdjustment, Is.Zero);
            Assert.That(abilities["last ability"].AgeAdjustment, Is.EqualTo(-3));
            Assert.That(abilities["last ability"].FullScore, Is.EqualTo(1));
            Assert.That(abilities["last ability"].HasScore, Is.True);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(10)]
        public void GenerateFor_AppliesTemplateMinimums_AbilityDoesNotMeetTemplateMinimum_Roll(int difference)
        {
            mockCreatureVerifier
                .Setup(v => v.VerifyCompatibility(
                    false,
                    "creature name",
                    randomizer,
                    It.Is<Filters>(f => f.Templates.IsEquivalentTo(new[] { "my template" }))))
                .Returns(true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            var minimum = new Ability("ability") { BaseScore = 42 + difference };
            mockTemplateApplicator.SetupGet(a => a.MinimumAbility).Returns(minimum);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var abilities = abilitiesGenerator.GenerateFor("creature name", false, randomizer, demographics, ["my template"]);
            Assert.That(abilities["ability"].Name, Is.EqualTo("ability"));
            Assert.That(abilities["ability"].BaseScore, Is.EqualTo(42 + difference));
            Assert.That(abilities["ability"].RacialAdjustment, Is.Zero);
            Assert.That(abilities["ability"].FullScore, Is.EqualTo(42 + difference));
            Assert.That(abilities["ability"].HasScore, Is.True);
            Assert.That(abilities["other ability"].Name, Is.EqualTo("other ability"));
            Assert.That(abilities["other ability"].BaseScore, Is.EqualTo(600));
            Assert.That(abilities["other ability"].RacialAdjustment, Is.EqualTo(9266));
            Assert.That(abilities["other ability"].FullScore, Is.EqualTo(9866));
            Assert.That(abilities["other ability"].HasScore, Is.True);
            Assert.That(abilities["last ability"].Name, Is.EqualTo("last ability"));
            Assert.That(abilities["last ability"].BaseScore, Is.EqualTo(1337));
            Assert.That(abilities["last ability"].RacialAdjustment, Is.EqualTo(-90210));
            Assert.That(abilities["last ability"].FullScore, Is.EqualTo(1));
            Assert.That(abilities["last ability"].HasScore, Is.True);
        }

        [Test]
        public void GenerateFor_AppliesTemplateMinimums_AbilityDoesNotMeetTemplateMinimum_Set()
        {
            //INFO: We can't adjust a set ability. If a set ability won't meet the minimums, then the creature verifier should say the creature is incompatible.
            mockCreatureVerifier
                .Setup(v => v.VerifyCompatibility(
                    false,
                    "creature name",
                    randomizer,
                    It.Is<Filters>(f => f.Templates.IsEquivalentTo(new[] { "my template" }))))
                .Returns(false);

            randomizer.SetRolls["ability"] = 1336;

            var generation = () => abilitiesGenerator.GenerateFor("creature name", false, randomizer, demographics, ["my template"]);
            Assert.That(generation,
                Throws.InstanceOf<InvalidCreatureException>().With.Message.Contains("creature name does not have sufficient ability for template my template"));
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(10)]
        public void GenerateFor_AppliesTemplateMinimums_AbilityDoesNotMeetTemplateMinimum_WithPositiveRacialAdjustment(int difference)
        {
            mockCreatureVerifier
                .Setup(v => v.VerifyCompatibility(
                    false,
                    "creature name",
                    randomizer,
                    It.Is<Filters>(f => f.Templates.IsEquivalentTo(new[] { "my template" }))))
                .Returns(true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            var minimum = new Ability("other ability") { BaseScore = 9866 + difference };
            mockTemplateApplicator.SetupGet(a => a.MinimumAbility).Returns(minimum);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var abilities = abilitiesGenerator.GenerateFor("creature name", false, randomizer, demographics, ["my template"]);
            Assert.That(abilities["ability"].Name, Is.EqualTo("ability"));
            Assert.That(abilities["ability"].BaseScore, Is.EqualTo(42));
            Assert.That(abilities["ability"].RacialAdjustment, Is.Zero);
            Assert.That(abilities["ability"].FullScore, Is.EqualTo(42));
            Assert.That(abilities["ability"].HasScore, Is.True);
            Assert.That(abilities["other ability"].Name, Is.EqualTo("other ability"));
            Assert.That(abilities["other ability"].BaseScore, Is.EqualTo(600 + difference));
            Assert.That(abilities["other ability"].RacialAdjustment, Is.EqualTo(9266));
            Assert.That(abilities["other ability"].FullScore, Is.EqualTo(9866 + difference));
            Assert.That(abilities["other ability"].HasScore, Is.True);
            Assert.That(abilities["last ability"].Name, Is.EqualTo("last ability"));
            Assert.That(abilities["last ability"].BaseScore, Is.EqualTo(1337));
            Assert.That(abilities["last ability"].RacialAdjustment, Is.EqualTo(-90210));
            Assert.That(abilities["last ability"].FullScore, Is.EqualTo(1));
            Assert.That(abilities["last ability"].HasScore, Is.True);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(10)]
        public void GenerateFor_AppliesTemplateMinimums_AbilityDoesNotMeetTemplateMinimum_WithNegativeRacialAdjustment(int difference)
        {
            mockCreatureVerifier
                .Setup(v => v.VerifyCompatibility(
                    false,
                    "creature name",
                    randomizer,
                    It.Is<Filters>(f => f.Templates.IsEquivalentTo(new[] { "my template" }))))
                .Returns(true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            var minimum = new Ability("other ability") { BaseScore = 600 - 13 + difference };
            mockTemplateApplicator.SetupGet(a => a.MinimumAbility).Returns(minimum);

            creatureAbilitySelections[1].AmountAsDouble = -13;

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var abilities = abilitiesGenerator.GenerateFor("creature name", false, randomizer, demographics, ["my template"]);
            Assert.That(abilities["ability"].Name, Is.EqualTo("ability"));
            Assert.That(abilities["ability"].BaseScore, Is.EqualTo(42));
            Assert.That(abilities["ability"].RacialAdjustment, Is.Zero);
            Assert.That(abilities["ability"].FullScore, Is.EqualTo(42));
            Assert.That(abilities["ability"].HasScore, Is.True);
            Assert.That(abilities["other ability"].Name, Is.EqualTo("other ability"));
            Assert.That(abilities["other ability"].BaseScore, Is.EqualTo(600 + difference));
            Assert.That(abilities["other ability"].RacialAdjustment, Is.EqualTo(-13));
            Assert.That(abilities["other ability"].FullScore, Is.EqualTo(600 - 13 + difference));
            Assert.That(abilities["other ability"].HasScore, Is.True);
            Assert.That(abilities["last ability"].Name, Is.EqualTo("last ability"));
            Assert.That(abilities["last ability"].BaseScore, Is.EqualTo(1337));
            Assert.That(abilities["last ability"].RacialAdjustment, Is.EqualTo(-90210));
            Assert.That(abilities["last ability"].FullScore, Is.EqualTo(1));
            Assert.That(abilities["last ability"].HasScore, Is.True);
        }

        [Test]
        public void BUG_GenerateFor_AppliesTemplateMinimums_AbilityDoesNotMeetTemplateMinimum_WithNegativeRacialAdjustment_VeryLow()
        {
            mockCreatureVerifier
                .Setup(v => v.VerifyCompatibility(
                    false,
                    "creature name",
                    randomizer,
                    It.Is<Filters>(f => f.Templates.IsEquivalentTo(new[] { "my template" }))))
                .Returns(true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            var minimum = new Ability("other ability") { BaseScore = 4 };
            mockTemplateApplicator.SetupGet(a => a.MinimumAbility).Returns(minimum);

            creatureAbilitySelections[1].AmountAsDouble = -6;
            mockPartialTotal.SetupSequence(d => d.AsSum<int>()).Returns(4).Returns(5).Returns(13);
            mockPartialTotal.Setup(d => d.AsPotentialMaximum<int>(true)).Returns(18);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var abilities = abilitiesGenerator.GenerateFor("creature name", false, randomizer, demographics, ["my template"]);
            Assert.That(abilities["ability"].Name, Is.EqualTo("ability"));
            Assert.That(abilities["ability"].BaseScore, Is.EqualTo(4));
            Assert.That(abilities["ability"].RacialAdjustment, Is.Zero);
            Assert.That(abilities["ability"].FullScore, Is.EqualTo(4));
            Assert.That(abilities["ability"].HasScore, Is.True);
            Assert.That(abilities["other ability"].Name, Is.EqualTo("other ability"));
            Assert.That(abilities["other ability"].BaseScore, Is.EqualTo(10));
            Assert.That(abilities["other ability"].RacialAdjustment, Is.EqualTo(-6));
            Assert.That(abilities["other ability"].FullScore, Is.EqualTo(4));
            Assert.That(abilities["other ability"].HasScore, Is.True);
            Assert.That(abilities["last ability"].Name, Is.EqualTo("last ability"));
            Assert.That(abilities["last ability"].BaseScore, Is.EqualTo(13));
            Assert.That(abilities["last ability"].RacialAdjustment, Is.EqualTo(-90210));
            Assert.That(abilities["last ability"].FullScore, Is.EqualTo(1));
            Assert.That(abilities["last ability"].HasScore, Is.True);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(10)]
        public void GenerateFor_AppliesTemplateMinimums_AbilityDoesNotMeetTemplateMinimum_WithPositiveAgeAdjustment(int difference)
        {
            mockCreatureVerifier
                .Setup(v => v.VerifyCompatibility(
                    false,
                    "creature name",
                    randomizer,
                    It.Is<Filters>(f => f.Templates.IsEquivalentTo(new[] { "my template" }))))
                .Returns(true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            var minimum = new Ability("ability") { BaseScore = 43 + difference };
            mockTemplateApplicator.SetupGet(a => a.MinimumAbility).Returns(minimum);

            ageAbilitySelections[0].AmountAsDouble = 1;
            ageAbilitySelections[1].AmountAsDouble = 2;
            ageAbilitySelections[2].AmountAsDouble = 3;

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var abilities = abilitiesGenerator.GenerateFor("creature name", false, randomizer, demographics, ["my template"]);
            Assert.That(abilities["ability"].Name, Is.EqualTo("ability"));
            Assert.That(abilities["ability"].BaseScore, Is.EqualTo(42 + difference));
            Assert.That(abilities["ability"].RacialAdjustment, Is.Zero);
            Assert.That(abilities["ability"].AdvancementAdjustment, Is.Zero);
            Assert.That(abilities["ability"].AgeAdjustment, Is.EqualTo(1));
            Assert.That(abilities["ability"].FullScore, Is.EqualTo(43 + difference));
            Assert.That(abilities["ability"].HasScore, Is.True);
            Assert.That(abilities["other ability"].Name, Is.EqualTo("other ability"));
            Assert.That(abilities["other ability"].BaseScore, Is.EqualTo(600));
            Assert.That(abilities["other ability"].RacialAdjustment, Is.EqualTo(9266));
            Assert.That(abilities["other ability"].AdvancementAdjustment, Is.Zero);
            Assert.That(abilities["other ability"].AgeAdjustment, Is.EqualTo(2));
            Assert.That(abilities["other ability"].FullScore, Is.EqualTo(9868));
            Assert.That(abilities["other ability"].HasScore, Is.True);
            Assert.That(abilities["last ability"].Name, Is.EqualTo("last ability"));
            Assert.That(abilities["last ability"].BaseScore, Is.EqualTo(1337));
            Assert.That(abilities["last ability"].RacialAdjustment, Is.EqualTo(-90210));
            Assert.That(abilities["last ability"].AdvancementAdjustment, Is.Zero);
            Assert.That(abilities["last ability"].AgeAdjustment, Is.EqualTo(3));
            Assert.That(abilities["last ability"].FullScore, Is.EqualTo(1));
            Assert.That(abilities["last ability"].HasScore, Is.True);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(10)]
        public void GenerateFor_AppliesTemplateMinimums_AbilityDoesNotMeetTemplateMinimum_WithNegativeAgeAdjustment(int difference)
        {
            mockCreatureVerifier
                .Setup(v => v.VerifyCompatibility(
                    false,
                    "creature name",
                    randomizer,
                    It.Is<Filters>(f => f.Templates.IsEquivalentTo(new[] { "my template" }))))
                .Returns(true);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            var minimum = new Ability("ability") { BaseScore = 41 + difference };
            mockTemplateApplicator.SetupGet(a => a.MinimumAbility).Returns(minimum);

            ageAbilitySelections[0].AmountAsDouble = -1;
            ageAbilitySelections[1].AmountAsDouble = -2;
            ageAbilitySelections[2].AmountAsDouble = -3;

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockTemplateApplicator.Object);

            var abilities = abilitiesGenerator.GenerateFor("creature name", false, randomizer, demographics, ["my template"]);
            Assert.That(abilities["ability"].Name, Is.EqualTo("ability"));
            Assert.That(abilities["ability"].BaseScore, Is.EqualTo(42 + difference));
            Assert.That(abilities["ability"].RacialAdjustment, Is.Zero);
            Assert.That(abilities["ability"].AdvancementAdjustment, Is.Zero);
            Assert.That(abilities["ability"].AgeAdjustment, Is.EqualTo(-1));
            Assert.That(abilities["ability"].FullScore, Is.EqualTo(41 + difference));
            Assert.That(abilities["ability"].HasScore, Is.True);
            Assert.That(abilities["other ability"].Name, Is.EqualTo("other ability"));
            Assert.That(abilities["other ability"].BaseScore, Is.EqualTo(600));
            Assert.That(abilities["other ability"].RacialAdjustment, Is.EqualTo(9266));
            Assert.That(abilities["other ability"].AdvancementAdjustment, Is.Zero);
            Assert.That(abilities["other ability"].AgeAdjustment, Is.EqualTo(-2));
            Assert.That(abilities["other ability"].FullScore, Is.EqualTo(9864));
            Assert.That(abilities["other ability"].HasScore, Is.True);
            Assert.That(abilities["last ability"].Name, Is.EqualTo("last ability"));
            Assert.That(abilities["last ability"].BaseScore, Is.EqualTo(1337));
            Assert.That(abilities["last ability"].RacialAdjustment, Is.EqualTo(-90210));
            Assert.That(abilities["last ability"].AdvancementAdjustment, Is.Zero);
            Assert.That(abilities["last ability"].AgeAdjustment, Is.EqualTo(-3));
            Assert.That(abilities["last ability"].FullScore, Is.EqualTo(1));
            Assert.That(abilities["last ability"].HasScore, Is.True);
        }

        [Test]
        public void ApplyMaxModifier_NoEquipment()
        {
            var abilities = new Dictionary<string, Ability>
            {
                [AbilityConstants.Strength] = new Ability(AbilityConstants.Strength),
                [AbilityConstants.Constitution] = new Ability(AbilityConstants.Constitution),
                [AbilityConstants.Dexterity] = new Ability(AbilityConstants.Dexterity),
                [AbilityConstants.Intelligence] = new Ability(AbilityConstants.Intelligence),
                [AbilityConstants.Wisdom] = new Ability(AbilityConstants.Wisdom),
                [AbilityConstants.Charisma] = new Ability(AbilityConstants.Charisma)
            };

            var equipment = new Equipment
            {
                Armor = null,
                Shield = null
            };

            var modifiedAbilities = abilitiesGenerator.SetMaxBonuses(abilities, equipment);
            Assert.That(modifiedAbilities, Is.EqualTo(abilities));
            Assert.That(modifiedAbilities[AbilityConstants.Strength].MaxModifier, Is.EqualTo(int.MaxValue));
            Assert.That(modifiedAbilities[AbilityConstants.Constitution].MaxModifier, Is.EqualTo(int.MaxValue));
            Assert.That(modifiedAbilities[AbilityConstants.Dexterity].MaxModifier, Is.EqualTo(int.MaxValue));
            Assert.That(modifiedAbilities[AbilityConstants.Intelligence].MaxModifier, Is.EqualTo(int.MaxValue));
            Assert.That(modifiedAbilities[AbilityConstants.Wisdom].MaxModifier, Is.EqualTo(int.MaxValue));
            Assert.That(modifiedAbilities[AbilityConstants.Charisma].MaxModifier, Is.EqualTo(int.MaxValue));
        }

        [Test]
        public void ApplyMaxModifier_ArmorOnly()
        {
            var abilities = new Dictionary<string, Ability>
            {
                [AbilityConstants.Strength] = new Ability(AbilityConstants.Strength),
                [AbilityConstants.Constitution] = new Ability(AbilityConstants.Constitution),
                [AbilityConstants.Dexterity] = new Ability(AbilityConstants.Dexterity),
                [AbilityConstants.Intelligence] = new Ability(AbilityConstants.Intelligence),
                [AbilityConstants.Wisdom] = new Ability(AbilityConstants.Wisdom),
                [AbilityConstants.Charisma] = new Ability(AbilityConstants.Charisma)
            };

            var equipment = new Equipment
            {
                Armor = new Armor
                {
                    MaxDexterityBonus = 9266,
                },
                Shield = null
            };

            var modifiedAbilities = abilitiesGenerator.SetMaxBonuses(abilities, equipment);
            Assert.That(modifiedAbilities, Is.EqualTo(abilities));
            Assert.That(modifiedAbilities[AbilityConstants.Strength].MaxModifier, Is.EqualTo(int.MaxValue));
            Assert.That(modifiedAbilities[AbilityConstants.Constitution].MaxModifier, Is.EqualTo(int.MaxValue));
            Assert.That(modifiedAbilities[AbilityConstants.Dexterity].MaxModifier, Is.EqualTo(9266));
            Assert.That(modifiedAbilities[AbilityConstants.Intelligence].MaxModifier, Is.EqualTo(int.MaxValue));
            Assert.That(modifiedAbilities[AbilityConstants.Wisdom].MaxModifier, Is.EqualTo(int.MaxValue));
            Assert.That(modifiedAbilities[AbilityConstants.Charisma].MaxModifier, Is.EqualTo(int.MaxValue));
        }

        [Test]
        public void ApplyMaxModifier_ShieldOnly_NoMax()
        {
            var abilities = new Dictionary<string, Ability>
            {
                [AbilityConstants.Strength] = new Ability(AbilityConstants.Strength),
                [AbilityConstants.Constitution] = new Ability(AbilityConstants.Constitution),
                [AbilityConstants.Dexterity] = new Ability(AbilityConstants.Dexterity),
                [AbilityConstants.Intelligence] = new Ability(AbilityConstants.Intelligence),
                [AbilityConstants.Wisdom] = new Ability(AbilityConstants.Wisdom),
                [AbilityConstants.Charisma] = new Ability(AbilityConstants.Charisma)
            };

            var equipment = new Equipment
            {
                Armor = null,
                Shield = new Armor
                {
                    MaxDexterityBonus = int.MaxValue
                }
            };

            var modifiedAbilities = abilitiesGenerator.SetMaxBonuses(abilities, equipment);
            Assert.That(modifiedAbilities, Is.EqualTo(abilities));
            Assert.That(modifiedAbilities[AbilityConstants.Strength].MaxModifier, Is.EqualTo(int.MaxValue));
            Assert.That(modifiedAbilities[AbilityConstants.Constitution].MaxModifier, Is.EqualTo(int.MaxValue));
            Assert.That(modifiedAbilities[AbilityConstants.Dexterity].MaxModifier, Is.EqualTo(int.MaxValue));
            Assert.That(modifiedAbilities[AbilityConstants.Intelligence].MaxModifier, Is.EqualTo(int.MaxValue));
            Assert.That(modifiedAbilities[AbilityConstants.Wisdom].MaxModifier, Is.EqualTo(int.MaxValue));
            Assert.That(modifiedAbilities[AbilityConstants.Charisma].MaxModifier, Is.EqualTo(int.MaxValue));
        }

        [Test]
        public void ApplyMaxModifier_ShieldOnly_WithMax()
        {
            var abilities = new Dictionary<string, Ability>
            {
                [AbilityConstants.Strength] = new Ability(AbilityConstants.Strength),
                [AbilityConstants.Constitution] = new Ability(AbilityConstants.Constitution),
                [AbilityConstants.Dexterity] = new Ability(AbilityConstants.Dexterity),
                [AbilityConstants.Intelligence] = new Ability(AbilityConstants.Intelligence),
                [AbilityConstants.Wisdom] = new Ability(AbilityConstants.Wisdom),
                [AbilityConstants.Charisma] = new Ability(AbilityConstants.Charisma)
            };

            var equipment = new Equipment
            {
                Armor = null,
                Shield = new Armor
                {
                    MaxDexterityBonus = 9266,
                }
            };

            var modifiedAbilities = abilitiesGenerator.SetMaxBonuses(abilities, equipment);
            Assert.That(modifiedAbilities, Is.EqualTo(abilities));
            Assert.That(modifiedAbilities[AbilityConstants.Strength].MaxModifier, Is.EqualTo(int.MaxValue));
            Assert.That(modifiedAbilities[AbilityConstants.Constitution].MaxModifier, Is.EqualTo(int.MaxValue));
            Assert.That(modifiedAbilities[AbilityConstants.Dexterity].MaxModifier, Is.EqualTo(9266));
            Assert.That(modifiedAbilities[AbilityConstants.Intelligence].MaxModifier, Is.EqualTo(int.MaxValue));
            Assert.That(modifiedAbilities[AbilityConstants.Wisdom].MaxModifier, Is.EqualTo(int.MaxValue));
            Assert.That(modifiedAbilities[AbilityConstants.Charisma].MaxModifier, Is.EqualTo(int.MaxValue));
        }

        [Test]
        public void ApplyMaxModifier_ArmorAndShield_NoShieldMax()
        {
            var abilities = new Dictionary<string, Ability>
            {
                [AbilityConstants.Strength] = new Ability(AbilityConstants.Strength),
                [AbilityConstants.Constitution] = new Ability(AbilityConstants.Constitution),
                [AbilityConstants.Dexterity] = new Ability(AbilityConstants.Dexterity),
                [AbilityConstants.Intelligence] = new Ability(AbilityConstants.Intelligence),
                [AbilityConstants.Wisdom] = new Ability(AbilityConstants.Wisdom),
                [AbilityConstants.Charisma] = new Ability(AbilityConstants.Charisma)
            };

            var equipment = new Equipment
            {
                Armor = new Armor
                {
                    MaxDexterityBonus = 9266
                },
                Shield = new Armor
                {
                    MaxDexterityBonus = int.MaxValue
                }
            };

            var modifiedAbilities = abilitiesGenerator.SetMaxBonuses(abilities, equipment);
            Assert.That(modifiedAbilities, Is.EqualTo(abilities));
            Assert.That(modifiedAbilities[AbilityConstants.Strength].MaxModifier, Is.EqualTo(int.MaxValue));
            Assert.That(modifiedAbilities[AbilityConstants.Constitution].MaxModifier, Is.EqualTo(int.MaxValue));
            Assert.That(modifiedAbilities[AbilityConstants.Dexterity].MaxModifier, Is.EqualTo(9266));
            Assert.That(modifiedAbilities[AbilityConstants.Intelligence].MaxModifier, Is.EqualTo(int.MaxValue));
            Assert.That(modifiedAbilities[AbilityConstants.Wisdom].MaxModifier, Is.EqualTo(int.MaxValue));
            Assert.That(modifiedAbilities[AbilityConstants.Charisma].MaxModifier, Is.EqualTo(int.MaxValue));
        }

        [Test]
        public void ApplyMaxModifier_ArmorAndShield_WithShieldMax_Higher()
        {
            var abilities = new Dictionary<string, Ability>
            {
                [AbilityConstants.Strength] = new Ability(AbilityConstants.Strength),
                [AbilityConstants.Constitution] = new Ability(AbilityConstants.Constitution),
                [AbilityConstants.Dexterity] = new Ability(AbilityConstants.Dexterity),
                [AbilityConstants.Intelligence] = new Ability(AbilityConstants.Intelligence),
                [AbilityConstants.Wisdom] = new Ability(AbilityConstants.Wisdom),
                [AbilityConstants.Charisma] = new Ability(AbilityConstants.Charisma)
            };

            var equipment = new Equipment
            {
                Armor = new Armor
                {
                    MaxDexterityBonus = 9266
                },
                Shield = new Armor
                {
                    MaxDexterityBonus = 90210
                }
            };

            var modifiedAbilities = abilitiesGenerator.SetMaxBonuses(abilities, equipment);
            Assert.That(modifiedAbilities, Is.EqualTo(abilities));
            Assert.That(modifiedAbilities[AbilityConstants.Strength].MaxModifier, Is.EqualTo(int.MaxValue));
            Assert.That(modifiedAbilities[AbilityConstants.Constitution].MaxModifier, Is.EqualTo(int.MaxValue));
            Assert.That(modifiedAbilities[AbilityConstants.Dexterity].MaxModifier, Is.EqualTo(9266));
            Assert.That(modifiedAbilities[AbilityConstants.Intelligence].MaxModifier, Is.EqualTo(int.MaxValue));
            Assert.That(modifiedAbilities[AbilityConstants.Wisdom].MaxModifier, Is.EqualTo(int.MaxValue));
            Assert.That(modifiedAbilities[AbilityConstants.Charisma].MaxModifier, Is.EqualTo(int.MaxValue));
        }

        [Test]
        public void ApplyMaxModifier_ArmorAndShield_WithShieldMax_Lower()
        {
            var abilities = new Dictionary<string, Ability>
            {
                [AbilityConstants.Strength] = new Ability(AbilityConstants.Strength),
                [AbilityConstants.Constitution] = new Ability(AbilityConstants.Constitution),
                [AbilityConstants.Dexterity] = new Ability(AbilityConstants.Dexterity),
                [AbilityConstants.Intelligence] = new Ability(AbilityConstants.Intelligence),
                [AbilityConstants.Wisdom] = new Ability(AbilityConstants.Wisdom),
                [AbilityConstants.Charisma] = new Ability(AbilityConstants.Charisma)
            };

            var equipment = new Equipment
            {
                Armor = new Armor
                {
                    MaxDexterityBonus = 9266
                },
                Shield = new Armor
                {
                    MaxDexterityBonus = 42
                }
            };

            var modifiedAbilities = abilitiesGenerator.SetMaxBonuses(abilities, equipment);
            Assert.That(modifiedAbilities, Is.EqualTo(abilities));
            Assert.That(modifiedAbilities[AbilityConstants.Strength].MaxModifier, Is.EqualTo(int.MaxValue));
            Assert.That(modifiedAbilities[AbilityConstants.Constitution].MaxModifier, Is.EqualTo(int.MaxValue));
            Assert.That(modifiedAbilities[AbilityConstants.Dexterity].MaxModifier, Is.EqualTo(42));
            Assert.That(modifiedAbilities[AbilityConstants.Intelligence].MaxModifier, Is.EqualTo(int.MaxValue));
            Assert.That(modifiedAbilities[AbilityConstants.Wisdom].MaxModifier, Is.EqualTo(int.MaxValue));
            Assert.That(modifiedAbilities[AbilityConstants.Charisma].MaxModifier, Is.EqualTo(int.MaxValue));
        }
    }
}