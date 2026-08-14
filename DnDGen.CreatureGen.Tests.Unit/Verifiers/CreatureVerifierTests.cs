using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Generators.Abilities;
using DnDGen.CreatureGen.Generators.Creatures;
using DnDGen.CreatureGen.Tables;
using DnDGen.CreatureGen.Templates;
using DnDGen.CreatureGen.Verifiers;
using DnDGen.Infrastructure.Factories;
using DnDGen.Infrastructure.Selectors.Collections;
using DnDGen.RollGen;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Unit.Verifiers
{
    [TestFixture]
    public class CreatureVerifierTests
    {
        private ICreatureVerifier verifier;
        private Mock<JustInTimeFactory> mockJustInTimeFactory;
        private Mock<ICollectionSelector> mockCollectionSelector;
        private Mock<Dice> mockDice;
        private Mock<ICreaturePrototypeFactory> mockCreaturePrototypeFactory;
        private AbilityRandomizer abilityRandomizer;

        private const string creature = "my creature";
        private static readonly string[] allCreatures = ["character", creature, "wrong creature", "wrong character"];
        private static readonly string[] allCharacters = ["character", creature, "wrong character"];

        [SetUp]
        public void Setup()
        {
            mockJustInTimeFactory = new Mock<JustInTimeFactory>();
            mockCollectionSelector = new Mock<ICollectionSelector>();
            mockDice = new Mock<Dice>();
            mockCreaturePrototypeFactory = new Mock<ICreaturePrototypeFactory>();
            verifier = new CreatureVerifier(mockJustInTimeFactory.Object, mockCollectionSelector.Object, mockDice.Object, mockCreaturePrototypeFactory.Object);

            abilityRandomizer = new("my roll");
            mockDice.Setup(d => d.Roll(abilityRandomizer.Roll).AsPotentialMinimum<int>()).Returns(1);

            SetUpCreatureGroup(GroupConstants.All, allCreatures);
            SetUpCreatureGroup(GroupConstants.Characters, allCharacters);
            SetUpCreatureGroup(CreatureConstants.Templates.None + true, allCharacters);
            SetUpCreatureGroup(CreatureConstants.Templates.None + false, allCreatures);

            SetupApplicator(CreatureConstants.Templates.None);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void VerifyCompatibility_AbilityRandomizer_DefaultIsValid(bool asCharacter)
        {
            var isCompatible = verifier.VerifyCompatibility(asCharacter, creature, null);
            Assert.That(isCompatible, Is.True);
        }

        private Mock<TemplateApplicator> SetupApplicator(string template)
        {
            var mockApplicator = new Mock<TemplateApplicator>();
            mockJustInTimeFactory
                .Setup(f => f.Build<TemplateApplicator>(template))
                .Returns(mockApplicator.Object);

            return mockApplicator;
        }

        [TestCase(true)]
        [TestCase(false)]
        public void VerifyCompatibility_AbilityRandomizer_Valid(bool asCharacter)
        {
            var isCompatible = verifier.VerifyCompatibility(asCharacter, creature, abilityRandomizer);
            Assert.That(isCompatible, Is.True);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void VerifyCompatibility_AbilityRandomizer_Invalid(bool asCharacter)
        {
            mockDice.Setup(d => d.Roll(abilityRandomizer.Roll).AsPotentialMinimum<int>()).Returns(0);

            var isCompatible = verifier.VerifyCompatibility(asCharacter, creature, abilityRandomizer);
            Assert.That(isCompatible, Is.False);
        }

        [TestCase(true, true)]
        [TestCase(true, false)]
        [TestCase(false, true)]
        [TestCase(false, false)]
        public void VerifyCompatibility_Creature_WithDefaults(bool asCharacter, bool compatible)
        {
            if (!compatible)
                SetUpCreatureGroup(CreatureConstants.Templates.None + asCharacter, ["character", "wrong creature"]);

            var isCompatible = verifier.VerifyCompatibility(asCharacter, creature);
            Assert.That(isCompatible, Is.EqualTo(compatible));
        }

        [TestCase(true, true)]
        [TestCase(true, false)]
        [TestCase(false, true)]
        [TestCase(false, false)]
        public void VerifyCompatibility_CreatureAnd1Template_Compatible(bool asCharacter, bool compatible)
        {
            var filters = new Filters();
            filters.Templates.Add("template");

            SetUpCreatureGroup("template" + asCharacter, compatible ? ["character", creature, "wrong creature"] : ["character", "wrong creature"]);

            SetupApplicator("template");

            var isCompatible = verifier.VerifyCompatibility(asCharacter, creature, abilityRandomizer, filters);
            Assert.That(isCompatible, Is.EqualTo(compatible));
        }

        [TestCase(true, true)]
        [TestCase(true, false)]
        [TestCase(false, true)]
        [TestCase(false, false)]
        public void VerifyCompatibility_CreatureAnd2Templates_Compatible(bool asCharacter, bool compatible)
        {
            var filters = new Filters();
            filters.Templates.Add("template 1");
            filters.Templates.Add("template 2");

            SetUpCreatureGroup("template 1", ["character", creature, "wrong creature"]);

            mockCreaturePrototypeFactory
                .Setup(f => f.Build(It.IsAny<IEnumerable<string>>(), asCharacter, abilityRandomizer))
                .Returns((IEnumerable<string> cc, bool _, AbilityRandomizer _) => cc.Select(c => new CreaturePrototype { Name = c }));

            var mockApplicator1 = SetupApplicator("template 1");
            var mockApplicator2 = SetupApplicator("template 2");
            mockApplicator1
                .Setup(a => a.IsCompatible(It.IsAny<CreaturePrototype>(), asCharacter, null))
                .Returns(true);
            mockApplicator1
                .Setup(a => a.ApplyTo(It.IsAny<CreaturePrototype>(), asCharacter, null))
                .Returns((CreaturePrototype cp, bool _, Filters _) => cp);
            mockApplicator2
                .Setup(a => a.IsCompatible(It.IsAny<CreaturePrototype>(), asCharacter, filters))
                .Returns((CreaturePrototype cp, bool _, Filters _) => compatible && cp.Name == creature);
            mockApplicator2
                .Setup(a => a.ApplyTo(It.IsAny<CreaturePrototype>(), asCharacter, filters))
                .Returns((CreaturePrototype cp, bool _, Filters _) => cp);

            var isCompatible = verifier.VerifyCompatibility(asCharacter, creature, abilityRandomizer, filters);
            Assert.That(isCompatible, Is.EqualTo(compatible));
        }

        [TestCase(true, true)]
        [TestCase(true, false)]
        [TestCase(false, true)]
        [TestCase(false, false)]
        public void VerifyCompatibility_CreatureAnd3Templates_Compatible(bool asCharacter, bool compatible)
        {
            var filters = new Filters();
            filters.Templates.Add("template 1");
            filters.Templates.Add("template 2");
            filters.Templates.Add("template 3");

            SetUpCreatureGroup("template 1", ["character", creature, "wrong creature"]);

            mockCreaturePrototypeFactory
                .Setup(f => f.Build(It.IsAny<IEnumerable<string>>(), asCharacter, abilityRandomizer))
                .Returns((IEnumerable<string> cc, bool _, AbilityRandomizer _) => cc.Select(c => new CreaturePrototype { Name = c }));

            var mockApplicator1 = SetupApplicator("template 1");
            var mockApplicator2 = SetupApplicator("template 2");
            var mockApplicator3 = SetupApplicator("template 3");
            mockApplicator1
                .Setup(a => a.IsCompatible(It.IsAny<CreaturePrototype>(), asCharacter, null))
                .Returns(true);
            mockApplicator1
                .Setup(a => a.ApplyTo(It.IsAny<CreaturePrototype>(), asCharacter, null))
                .Returns((CreaturePrototype cp, bool _, Filters _) => cp);
            mockApplicator2
                .Setup(a => a.IsCompatible(It.IsAny<CreaturePrototype>(), asCharacter, null))
                .Returns(true);
            mockApplicator2
                .Setup(a => a.ApplyTo(It.IsAny<CreaturePrototype>(), asCharacter, null))
                .Returns((CreaturePrototype cp, bool _, Filters _) => cp);
            mockApplicator3
                .Setup(a => a.IsCompatible(It.IsAny<CreaturePrototype>(), asCharacter, filters))
                .Returns((CreaturePrototype cp, bool _, Filters _) => compatible && cp.Name == creature);
            mockApplicator3
                .Setup(a => a.ApplyTo(It.IsAny<CreaturePrototype>(), asCharacter, filters))
                .Returns((CreaturePrototype cp, bool _, Filters _) => cp);

            var isCompatible = verifier.VerifyCompatibility(asCharacter, creature, abilityRandomizer, filters);
            Assert.That(isCompatible, Is.EqualTo(compatible));
        }

        [TestCase(true, true)]
        [TestCase(true, false)]
        [TestCase(false, true)]
        [TestCase(false, false)]
        public void BUG_VerifyCompatibility_CreatureAndNoneTemplate_Compatible(bool asCharacter, bool compatible)
        {
            var filters = new Filters();
            filters.Templates.Add(CreatureConstants.Templates.None);

            if (!compatible)
                SetUpCreatureGroup(CreatureConstants.Templates.None + asCharacter, ["character", "wrong creature"]);

            var isCompatible = verifier.VerifyCompatibility(asCharacter, creature, abilityRandomizer, filters);
            Assert.That(isCompatible, Is.EqualTo(compatible));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void VerifyCompatiblity_CreatureAndTemplateAsCharacter_Compatible(bool compatible)
        {
            var filters = new Filters();
            filters.Templates.Add("template");

            SetUpCreatureGroup("template" + true, compatible ? ["character", creature, "wrong creature"] : ["character", "wrong creature"]);

            SetupApplicator("template");

            var isCompatible = verifier.VerifyCompatibility(true, creature, abilityRandomizer, filters);
            Assert.That(isCompatible, Is.EqualTo(compatible));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void BUG_VerifyCompatiblity_CreatureAndNoneTemplateAsCharacter_Compatible(bool compatible)
        {
            var filters = new Filters();
            filters.Templates.Add(CreatureConstants.Templates.None);

            if (!compatible)
                SetUpCreatureGroup(CreatureConstants.Templates.None + true, ["character", "wrong character"]);

            var isCompatible = verifier.VerifyCompatibility(true, creature, abilityRandomizer, filters);
            Assert.That(isCompatible, Is.EqualTo(compatible));
        }

        [Test]
        public void VerifyCompatiblity_CreatureAndTemplateAsCharacter_NotCompatible_IfNotCharacter()
        {
            var filters = new Filters();
            filters.Templates.Add("template");

            SetUpCreatureGroup(GroupConstants.Characters, allCharacters.Except([creature]));
            SetUpCreatureGroup("template" + true, ["character", creature, "wrong creature"]);

            SetupApplicator("template");

            var isCompatible = verifier.VerifyCompatibility(true, creature, abilityRandomizer, filters);
            Assert.That(isCompatible, Is.False);
        }

        [Test]
        public void BUG_VerifyCompatiblity_CreatureAndNoneTemplateAsCharacter_NotCompatible_IfNotCharacter()
        {
            var filters = new Filters();
            filters.Templates.Add(CreatureConstants.Templates.None);

            SetUpCreatureGroup(GroupConstants.Characters, allCharacters.Except([creature]));

            var isCompatible = verifier.VerifyCompatibility(true, creature, abilityRandomizer, filters);
            Assert.That(isCompatible, Is.False);
        }

        [Test]
        public void VerifyCompatiblity_Template_Compatible_IfTemplate()
        {
            var filters = new Filters();
            filters.Templates.Add("template");

            SetUpCreatureGroup("template" + false, ["character", creature, "template creature"]);

            SetupApplicator("template");

            var isCompatible = verifier.VerifyCompatibility(false, null, abilityRandomizer, filters);
            Assert.That(isCompatible, Is.True);
        }

        [Test]
        public void VerifyCompatiblity_TemplateWithMinimumAbility_Compatible()
        {
            var filters = new Filters();
            filters.Templates.Add("template");

            SetUpCreatureGroup("template" + false, ["template character", creature, "wrong template creature"]);
            SetUpCreatureGroup("my ability-3", ["ability character", creature, "wrong ability creature"]);

            var mockApplicator = SetupApplicator("template");
            var minAbility = new Ability("my ability") { BaseScore = 6 };
            mockApplicator.SetupGet(a => a.MinimumAbility).Returns(minAbility);

            mockDice.Setup(d => d.Roll(abilityRandomizer.Roll).AsPotentialMaximum<int>(true)).Returns(9);

            var isCompatible = verifier.VerifyCompatibility(false, null, abilityRandomizer, filters);
            Assert.That(isCompatible, Is.True);
        }

        [Test]
        public void BUG_VerifyCompatiblity_NoneTemplate_Compatible()
        {
            var filters = new Filters();
            filters.Templates.Add(CreatureConstants.Templates.None);

            var isCompatible = verifier.VerifyCompatibility(false, null, abilityRandomizer, filters);
            Assert.That(isCompatible, Is.True);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void VerifyCompatiblity_Template_NotCompatible_IfNotTemplate(bool asCharacter)
        {
            var filters = new Filters();
            filters.Templates.Add("template");

            SetUpCreatureGroup("template" + asCharacter, ["template creature", "wrong template creature"]);

            SetupApplicator("template");

            var isCompatible = verifier.VerifyCompatibility(asCharacter, null, abilityRandomizer, filters);
            Assert.That(isCompatible, Is.False);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void VerifyCompatiblity_Template_NotCompatible_IfNotMinimumAbility(bool asCharacter)
        {
            var filters = new Filters();
            filters.Templates.Add("template");

            SetUpCreatureGroup("template" + asCharacter, ["template creature", creature, "wrong template creature"]);
            SetUpCreatureGroup("my ability-3", ["ability character", "wrong ability creature"]);

            var mockApplicator = SetupApplicator("template");
            var minAbility = new Ability("my ability") { BaseScore = 6 };
            mockApplicator.SetupGet(a => a.MinimumAbility).Returns(minAbility);

            mockDice.Setup(d => d.Roll(abilityRandomizer.Roll).AsPotentialMaximum<int>(true)).Returns(9);

            var isCompatible = verifier.VerifyCompatibility(asCharacter, null, abilityRandomizer, filters);
            Assert.That(isCompatible, Is.True);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void BUG_VerifyCompatiblity_NoneTemplate_NotCompatible_IfNotTemplate(bool asCharacter)
        {
            var filters = new Filters();
            filters.Templates.Add(CreatureConstants.Templates.None);

            SetUpCreatureGroup(CreatureConstants.Templates.None + asCharacter, ["none creature", "other wrong creature"]);

            var isCompatible = verifier.VerifyCompatibility(asCharacter, null, abilityRandomizer, filters);
            Assert.That(isCompatible, Is.False);
        }

        [Test]
        public void VerifyCompatiblity_TemplateAsCharacter_NotCompatible_IfNotCharacter()
        {
            var filters = new Filters();
            filters.Templates.Add("template");

            SetUpCreatureGroup(GroupConstants.Characters, ["character", "wrong character"]);
            SetUpCreatureGroup("template" + true, [creature, "wrong creature"]);

            SetupApplicator("template");

            var isCompatible = verifier.VerifyCompatibility(true, null, abilityRandomizer, filters);
            Assert.That(isCompatible, Is.False);
        }

        [Test]
        public void VerifyCompatiblity_TemplateWithMinimumAbilityAsCharacter_NotCompatible_IfNotCharacter()
        {
            var filters = new Filters();
            filters.Templates.Add("template");

            SetUpCreatureGroup(GroupConstants.Characters, ["character", "wrong character"]);
            SetUpCreatureGroup("template" + true, [creature, "wrong creature"]);
            SetUpCreatureGroup("my ability-3", ["ability character", "character", creature, "wrong ability creature"]);

            var mockApplicator = SetupApplicator("template");
            var minAbility = new Ability("my ability") { BaseScore = 6 };
            mockApplicator.SetupGet(a => a.MinimumAbility).Returns(minAbility);

            mockDice.Setup(d => d.Roll(abilityRandomizer.Roll).AsPotentialMaximum<int>(true)).Returns(9);

            var isCompatible = verifier.VerifyCompatibility(true, null, abilityRandomizer, filters);
            Assert.That(isCompatible, Is.False);
        }

        [TestCase(true, null, null, null)]
        [TestCase(true, null, null, "my alignment")]
        [TestCase(true, null, "my type", null)]
        [TestCase(true, null, "my type", "my alignment")]
        [TestCase(true, "my challenge rating", null, null)]
        [TestCase(true, "my challenge rating", null, "my alignment")]
        [TestCase(true, "my challenge rating", "my type", null)]
        [TestCase(true, "my challenge rating", "my type", "my alignment")]
        [TestCase(false, null, null, null)]
        [TestCase(false, null, null, "my alignment")]
        [TestCase(false, null, "my type", null)]
        [TestCase(false, null, "my type", "my alignment")]
        [TestCase(false, "my challenge rating", null, null)]
        [TestCase(false, "my challenge rating", null, "my alignment")]
        [TestCase(false, "my challenge rating", "my type", null)]
        [TestCase(false, "my challenge rating", "my type", "my alignment")]
        public void VerifyCompatiblity_TemplateAndFilters_Compatible(bool asCharacter, string cr, string type, string alignment)
        {
            var filters = new Filters
            {
                Type = type,
                ChallengeRating = cr,
                Alignment = alignment
            };
            filters.Templates.Add("template");

            SetUpCreatureGroup("template" + asCharacter, ["template character", creature, "wrong template creature"]);

            if (cr != null)
                SetUpCreatureGroup("template" + asCharacter + cr, ["template cr character", creature, "wrong cr creature"]);

            if (type != null)
                SetUpCreatureGroup("template" + type, ["template type character", creature, "wrong type creature"]);

            if (alignment != null)
                SetUpCreatureGroup("template" + alignment, ["template alignment character", creature, "wrong alignment creature"]);

            SetupApplicator("template");

            var isCompatible = verifier.VerifyCompatibility(asCharacter, null, abilityRandomizer, filters);
            Assert.That(isCompatible, Is.True);
        }

        [TestCase(true, null, null, null)]
        [TestCase(true, null, null, "my alignment")]
        [TestCase(true, null, "my type", null)]
        [TestCase(true, null, "my type", "my alignment")]
        [TestCase(true, "my challenge rating", null, null)]
        [TestCase(true, "my challenge rating", null, "my alignment")]
        [TestCase(true, "my challenge rating", "my type", null)]
        [TestCase(true, "my challenge rating", "my type", "my alignment")]
        [TestCase(false, null, null, null)]
        [TestCase(false, null, null, "my alignment")]
        [TestCase(false, null, "my type", null)]
        [TestCase(false, null, "my type", "my alignment")]
        [TestCase(false, "my challenge rating", null, null)]
        [TestCase(false, "my challenge rating", null, "my alignment")]
        [TestCase(false, "my challenge rating", "my type", null)]
        [TestCase(false, "my challenge rating", "my type", "my alignment")]
        public void VerifyCompatiblity_TemplateWithMinimumAbilityAndFilters_Compatible(bool asCharacter, string cr, string type, string alignment)
        {
            var filters = new Filters
            {
                Type = type,
                ChallengeRating = cr,
                Alignment = alignment
            };
            filters.Templates.Add("template");

            SetUpCreatureGroup("template" + asCharacter, ["template character", creature, "wrong template creature"]);
            SetUpCreatureGroup("my ability-3", ["ability character", creature, "wrong ability creature"]);

            if (cr != null)
                SetUpCreatureGroup("template" + asCharacter + cr, ["template cr character", creature, "wrong cr creature"]);

            if (type != null)
                SetUpCreatureGroup("template" + type, ["template type character", creature, "wrong type creature"]);

            if (alignment != null)
                SetUpCreatureGroup("template" + alignment, ["template alignment character", creature, "wrong alignment creature"]);

            var mockApplicator = SetupApplicator("template");
            var minAbility = new Ability("my ability") { BaseScore = 6 };
            mockApplicator.SetupGet(a => a.MinimumAbility).Returns(minAbility);

            mockDice.Setup(d => d.Roll(abilityRandomizer.Roll).AsPotentialMaximum<int>(true)).Returns(9);

            var isCompatible = verifier.VerifyCompatibility(asCharacter, null, abilityRandomizer, filters);
            Assert.That(isCompatible, Is.True);
        }

        [TestCase(true, null, null, null)]
        [TestCase(true, null, null, "my alignment")]
        [TestCase(true, null, "my type", null)]
        [TestCase(true, null, "my type", "my alignment")]
        [TestCase(true, "my challenge rating", null, null)]
        [TestCase(true, "my challenge rating", null, "my alignment")]
        [TestCase(true, "my challenge rating", "my type", null)]
        [TestCase(true, "my challenge rating", "my type", "my alignment")]
        [TestCase(false, null, null, null)]
        [TestCase(false, null, null, "my alignment")]
        [TestCase(false, null, "my type", null)]
        [TestCase(false, null, "my type", "my alignment")]
        [TestCase(false, "my challenge rating", null, null)]
        [TestCase(false, "my challenge rating", null, "my alignment")]
        [TestCase(false, "my challenge rating", "my type", null)]
        [TestCase(false, "my challenge rating", "my type", "my alignment")]
        public void BUG_VerifyCompatiblity_NoneTemplateAndFilters_Compatible(bool asCharacter, string cr, string type, string alignment)
        {
            var filters = new Filters
            {
                Type = type,
                ChallengeRating = cr,
                Alignment = alignment
            };
            filters.Templates.Add(CreatureConstants.Templates.None);

            SetUpCreatureGroup(CreatureConstants.Templates.None + asCharacter, ["template character", creature, "wrong template creature"]);

            if (cr != null)
                SetUpCreatureGroup(CreatureConstants.Templates.None + asCharacter + cr, ["template cr character", creature, "wrong cr creature"]);

            if (type != null)
                SetUpCreatureGroup(CreatureConstants.Templates.None + type, ["template type character", creature, "wrong type creature"]);

            if (alignment != null)
                SetUpCreatureGroup(CreatureConstants.Templates.None + alignment, ["template alignment character", creature, "wrong alignment creature"]);

            var isCompatible = verifier.VerifyCompatibility(asCharacter, null, abilityRandomizer, filters);
            Assert.That(isCompatible, Is.True);
        }

        [TestCase(true, null, null, null)]
        [TestCase(true, null, null, "my alignment")]
        [TestCase(true, null, "my type", null)]
        [TestCase(true, null, "my type", "my alignment")]
        [TestCase(true, "my challenge rating", null, null)]
        [TestCase(true, "my challenge rating", null, "my alignment")]
        [TestCase(true, "my challenge rating", "my type", null)]
        [TestCase(true, "my challenge rating", "my type", "my alignment")]
        [TestCase(false, null, null, null)]
        [TestCase(false, null, null, "my alignment")]
        [TestCase(false, null, "my type", null)]
        [TestCase(false, null, "my type", "my alignment")]
        [TestCase(false, "my challenge rating", null, null)]
        [TestCase(false, "my challenge rating", null, "my alignment")]
        [TestCase(false, "my challenge rating", "my type", null)]
        [TestCase(false, "my challenge rating", "my type", "my alignment")]
        public void VerifyCompatiblity_TemplateAndFilters_NotCompatible_IfNotTemplate(bool asCharacter, string cr, string type, string alignment)
        {
            var filters = new Filters
            {
                Type = type,
                ChallengeRating = cr,
                Alignment = alignment
            };
            filters.Templates.Add("template");

            SetUpCreatureGroup("template" + asCharacter, ["template character", "wrong template creature"]);

            if (cr != null)
                SetUpCreatureGroup("template" + asCharacter + cr, ["template cr character", creature, "wrong cr creature"]);

            if (type != null)
                SetUpCreatureGroup("template" + type, ["template type character", creature, "wrong type creature"]);

            if (alignment != null)
                SetUpCreatureGroup("template" + alignment, ["template alignment character", creature, "wrong alignment creature"]);

            SetupApplicator("template");

            var isCompatible = verifier.VerifyCompatibility(asCharacter, null, abilityRandomizer, filters);
            Assert.That(isCompatible, Is.False);
        }

        [TestCase(true, "my challenge rating", null, null)]
        [TestCase(true, "my challenge rating", null, "my alignment")]
        [TestCase(true, "my challenge rating", "my type", null)]
        [TestCase(true, "my challenge rating", "my type", "my alignment")]
        [TestCase(false, "my challenge rating", null, null)]
        [TestCase(false, "my challenge rating", null, "my alignment")]
        [TestCase(false, "my challenge rating", "my type", null)]
        [TestCase(false, "my challenge rating", "my type", "my alignment")]
        public void VerifyCompatiblity_TemplateAndFilters_NotCompatible_IfNotCR(bool asCharacter, string cr, string type, string alignment)
        {
            var filters = new Filters
            {
                Type = type,
                ChallengeRating = cr,
                Alignment = alignment
            };
            filters.Templates.Add("template");

            SetUpCreatureGroup("template" + asCharacter, ["template character", creature, "wrong template creature"]);

            if (cr != null)
                SetUpCreatureGroup("template" + asCharacter + cr, ["template cr character", "wrong cr creature"]);

            if (type != null)
                SetUpCreatureGroup("template" + type, ["template type character", creature, "wrong type creature"]);

            if (alignment != null)
                SetUpCreatureGroup("template" + alignment, ["template alignment character", creature, "wrong alignment creature"]);

            SetupApplicator("template");

            var isCompatible = verifier.VerifyCompatibility(asCharacter, null, abilityRandomizer, filters);
            Assert.That(isCompatible, Is.False);
        }

        [TestCase(true, null, "my type", null)]
        [TestCase(true, null, "my type", "my alignment")]
        [TestCase(true, "my challenge rating", "my type", null)]
        [TestCase(true, "my challenge rating", "my type", "my alignment")]
        [TestCase(false, null, "my type", null)]
        [TestCase(false, null, "my type", "my alignment")]
        [TestCase(false, "my challenge rating", "my type", null)]
        [TestCase(false, "my challenge rating", "my type", "my alignment")]
        public void VerifyCompatiblity_TemplateAndFilters_NotCompatible_IfNotType(bool asCharacter, string cr, string type, string alignment)
        {
            var filters = new Filters
            {
                Type = type,
                ChallengeRating = cr,
                Alignment = alignment
            };
            filters.Templates.Add("template");

            SetUpCreatureGroup("template" + asCharacter, ["template character", creature, "wrong template creature"]);

            if (cr != null)
                SetUpCreatureGroup("template" + asCharacter + cr, ["template cr character", creature, "wrong cr creature"]);

            if (type != null)
                SetUpCreatureGroup("template" + type, ["template type character", "wrong type creature"]);

            if (alignment != null)
                SetUpCreatureGroup("template" + alignment, ["template alignment character", creature, "wrong alignment creature"]);

            SetupApplicator("template");

            var isCompatible = verifier.VerifyCompatibility(asCharacter, null, abilityRandomizer, filters);
            Assert.That(isCompatible, Is.False);
        }

        [TestCase(true, null, null, "my alignment")]
        [TestCase(true, null, "my type", "my alignment")]
        [TestCase(true, "my challenge rating", null, "my alignment")]
        [TestCase(true, "my challenge rating", "my type", "my alignment")]
        [TestCase(false, null, null, "my alignment")]
        [TestCase(false, null, "my type", "my alignment")]
        [TestCase(false, "my challenge rating", null, "my alignment")]
        [TestCase(false, "my challenge rating", "my type", "my alignment")]
        public void VerifyCompatiblity_TemplateAndFilters_NotCompatible_IfNotAlignment(bool asCharacter, string cr, string type, string alignment)
        {
            var filters = new Filters
            {
                Type = type,
                ChallengeRating = cr,
                Alignment = alignment
            };
            filters.Templates.Add("template");

            SetUpCreatureGroup("template" + asCharacter, ["template character", creature, "wrong template creature"]);

            if (cr != null)
                SetUpCreatureGroup("template" + asCharacter + cr, ["template cr character", creature, "wrong cr creature"]);

            if (type != null)
                SetUpCreatureGroup("template" + type, ["template type character", creature, "wrong type creature"]);

            if (alignment != null)
                SetUpCreatureGroup("template" + alignment, ["template alignment character", "wrong alignment creature"]);

            SetupApplicator("template");

            var isCompatible = verifier.VerifyCompatibility(asCharacter, null, abilityRandomizer, filters);
            Assert.That(isCompatible, Is.False);
        }

        [TestCase(true, null, null, null)]
        [TestCase(true, null, null, "my alignment")]
        [TestCase(true, null, "my type", null)]
        [TestCase(true, null, "my type", "my alignment")]
        [TestCase(true, "my challenge rating", null, null)]
        [TestCase(true, "my challenge rating", null, "my alignment")]
        [TestCase(true, "my challenge rating", "my type", null)]
        [TestCase(true, "my challenge rating", "my type", "my alignment")]
        [TestCase(false, null, null, null)]
        [TestCase(false, null, null, "my alignment")]
        [TestCase(false, null, "my type", null)]
        [TestCase(false, null, "my type", "my alignment")]
        [TestCase(false, "my challenge rating", null, null)]
        [TestCase(false, "my challenge rating", null, "my alignment")]
        [TestCase(false, "my challenge rating", "my type", null)]
        [TestCase(false, "my challenge rating", "my type", "my alignment")]
        public void VerifyCompatiblity_TemplateWithMinimumAbilityAndFilters_NotCompatible_IfNotMinimumAbility(bool asCharacter, string cr, string type, string alignment)
        {
            var filters = new Filters
            {
                Type = type,
                ChallengeRating = cr,
                Alignment = alignment
            };
            filters.Templates.Add("template");

            SetUpCreatureGroup("template" + asCharacter, ["template character", creature, "wrong template creature"]);
            SetUpCreatureGroup("my ability-3", ["ability character", "wrong ability creature"]);

            if (cr != null)
                SetUpCreatureGroup("template" + asCharacter + cr, ["template cr character", creature, "wrong cr creature"]);

            if (type != null)
                SetUpCreatureGroup("template" + type, ["template type character", creature, "wrong type creature"]);

            if (alignment != null)
                SetUpCreatureGroup("template" + alignment, ["template alignment character", creature, "wrong alignment creature"]);

            var mockApplicator = SetupApplicator("template");
            var minAbility = new Ability("my ability") { BaseScore = 6 };
            mockApplicator.SetupGet(a => a.MinimumAbility).Returns(minAbility);

            mockDice.Setup(d => d.Roll(abilityRandomizer.Roll).AsPotentialMaximum<int>(true)).Returns(9);

            var isCompatible = verifier.VerifyCompatibility(asCharacter, null, abilityRandomizer, filters);
            Assert.That(isCompatible, Is.False);
        }

        [TestCase(true, null, null, null)]
        [TestCase(true, null, null, "my alignment")]
        [TestCase(true, null, "my type", null)]
        [TestCase(true, null, "my type", "my alignment")]
        [TestCase(true, "my challenge rating", null, null)]
        [TestCase(true, "my challenge rating", null, "my alignment")]
        [TestCase(true, "my challenge rating", "my type", null)]
        [TestCase(true, "my challenge rating", "my type", "my alignment")]
        [TestCase(false, null, null, null)]
        [TestCase(false, null, null, "my alignment")]
        [TestCase(false, null, "my type", null)]
        [TestCase(false, null, "my type", "my alignment")]
        [TestCase(false, "my challenge rating", null, null)]
        [TestCase(false, "my challenge rating", null, "my alignment")]
        [TestCase(false, "my challenge rating", "my type", null)]
        [TestCase(false, "my challenge rating", "my type", "my alignment")]
        public void BUG_VerifyCompatiblity_NoneTemplateAndFilters_NotCompatible_IfNotTemplate(bool asCharacter, string cr, string type, string alignment)
        {
            var filters = new Filters
            {
                Type = type,
                ChallengeRating = cr,
                Alignment = alignment
            };
            filters.Templates.Add(CreatureConstants.Templates.None);

            SetUpCreatureGroup(CreatureConstants.Templates.None + asCharacter, ["template character", "wrong template creature"]);

            if (cr != null)
                SetUpCreatureGroup(CreatureConstants.Templates.None + asCharacter + cr, ["template cr character", creature, "wrong cr creature"]);

            if (type != null)
                SetUpCreatureGroup(CreatureConstants.Templates.None + type, ["template type character", creature, "wrong type creature"]);

            if (alignment != null)
                SetUpCreatureGroup(CreatureConstants.Templates.None + alignment, ["template alignment character", creature, "wrong alignment creature"]);

            var isCompatible = verifier.VerifyCompatibility(asCharacter, null, abilityRandomizer, filters);
            Assert.That(isCompatible, Is.False);
        }

        [TestCase(true, "my challenge rating", null, null)]
        [TestCase(true, "my challenge rating", null, "my alignment")]
        [TestCase(true, "my challenge rating", "my type", null)]
        [TestCase(true, "my challenge rating", "my type", "my alignment")]
        [TestCase(false, "my challenge rating", null, null)]
        [TestCase(false, "my challenge rating", null, "my alignment")]
        [TestCase(false, "my challenge rating", "my type", null)]
        [TestCase(false, "my challenge rating", "my type", "my alignment")]
        public void BUG_VerifyCompatiblity_NoneTemplateAndFilters_NotCompatible_IfNotCR(bool asCharacter, string cr, string type, string alignment)
        {
            var filters = new Filters
            {
                Type = type,
                ChallengeRating = cr,
                Alignment = alignment
            };
            filters.Templates.Add(CreatureConstants.Templates.None);

            SetUpCreatureGroup(CreatureConstants.Templates.None + asCharacter, ["template character", creature, "wrong template creature"]);

            if (cr != null)
                SetUpCreatureGroup(CreatureConstants.Templates.None + asCharacter + cr, ["template cr character", "wrong cr creature"]);

            if (type != null)
                SetUpCreatureGroup(CreatureConstants.Templates.None + type, ["template type character", creature, "wrong type creature"]);

            if (alignment != null)
                SetUpCreatureGroup(CreatureConstants.Templates.None + alignment, ["template alignment character", creature, "wrong alignment creature"]);

            var isCompatible = verifier.VerifyCompatibility(asCharacter, null, abilityRandomizer, filters);
            Assert.That(isCompatible, Is.False);
        }

        [TestCase(true, null, "my type", null)]
        [TestCase(true, null, "my type", "my alignment")]
        [TestCase(true, "my challenge rating", "my type", null)]
        [TestCase(true, "my challenge rating", "my type", "my alignment")]
        [TestCase(false, null, "my type", null)]
        [TestCase(false, null, "my type", "my alignment")]
        [TestCase(false, "my challenge rating", "my type", null)]
        [TestCase(false, "my challenge rating", "my type", "my alignment")]
        public void BUG_VerifyCompatiblity_NoneTemplateAndFilters_NotCompatible_IfNotType(bool asCharacter, string cr, string type, string alignment)
        {
            var filters = new Filters
            {
                Type = type,
                ChallengeRating = cr,
                Alignment = alignment
            };
            filters.Templates.Add(CreatureConstants.Templates.None);

            SetUpCreatureGroup(CreatureConstants.Templates.None + asCharacter, ["template character", creature, "wrong template creature"]);

            if (cr != null)
                SetUpCreatureGroup(CreatureConstants.Templates.None + asCharacter + cr, ["template cr character", creature, "wrong cr creature"]);

            if (type != null)
                SetUpCreatureGroup(CreatureConstants.Templates.None + type, ["template type character", "wrong type creature"]);

            if (alignment != null)
                SetUpCreatureGroup(CreatureConstants.Templates.None + alignment, ["template alignment character", creature, "wrong alignment creature"]);

            var isCompatible = verifier.VerifyCompatibility(asCharacter, null, abilityRandomizer, filters);
            Assert.That(isCompatible, Is.False);
        }

        [TestCase(true, null, null, "my alignment")]
        [TestCase(true, null, "my type", "my alignment")]
        [TestCase(true, "my challenge rating", null, "my alignment")]
        [TestCase(true, "my challenge rating", "my type", "my alignment")]
        [TestCase(false, null, null, "my alignment")]
        [TestCase(false, null, "my type", "my alignment")]
        [TestCase(false, "my challenge rating", null, "my alignment")]
        [TestCase(false, "my challenge rating", "my type", "my alignment")]
        public void BUG_VerifyCompatiblity_NoneTemplateAndFilters_NotCompatible_IfNotAlignment(bool asCharacter, string cr, string type, string alignment)
        {
            var filters = new Filters
            {
                Type = type,
                ChallengeRating = cr,
                Alignment = alignment
            };
            filters.Templates.Add(CreatureConstants.Templates.None);

            SetUpCreatureGroup(CreatureConstants.Templates.None + asCharacter, ["template character", creature, "wrong template creature"]);

            if (cr != null)
                SetUpCreatureGroup(CreatureConstants.Templates.None + asCharacter + cr, ["template cr character", creature, "wrong cr creature"]);

            if (type != null)
                SetUpCreatureGroup(CreatureConstants.Templates.None + type, ["template type character", creature, "wrong type creature"]);

            if (alignment != null)
                SetUpCreatureGroup(CreatureConstants.Templates.None + alignment, ["template alignment character", "wrong alignment creature"]);

            var isCompatible = verifier.VerifyCompatibility(asCharacter, null, abilityRandomizer, filters);
            Assert.That(isCompatible, Is.False);
        }

        [TestCase(null, null, null)]
        [TestCase(null, null, "my alignment")]
        [TestCase(null, "my type", null)]
        [TestCase(null, "my type", "my alignment")]
        [TestCase("my challenge rating", null, null)]
        [TestCase("my challenge rating", null, "my alignment")]
        [TestCase("my challenge rating", "my type", null)]
        [TestCase("my challenge rating", "my type", "my alignment")]
        public void VerifyCompatiblity_TemplateAndFiltersAsCharacter_NotCompatible_IfNotCharacter(string cr, string type, string alignment)
        {
            var filters = new Filters
            {
                Type = type,
                ChallengeRating = cr,
                Alignment = alignment
            };
            filters.Templates.Add("template");

            SetUpCreatureGroup(GroupConstants.Characters, ["character", "wrong character"]);
            SetUpCreatureGroup("template" + true, ["template character", creature, "wrong template creature"]);

            if (cr != null)
                SetUpCreatureGroup("template" + true + cr, ["template cr character", creature, "wrong cr creature"]);

            if (type != null)
                SetUpCreatureGroup("template" + type, ["template type character", creature, "wrong type creature"]);

            if (alignment != null)
                SetUpCreatureGroup("template" + alignment, ["template alignment character", creature, "wrong alignment creature"]);

            SetupApplicator("template");

            var isCompatible = verifier.VerifyCompatibility(true, null, abilityRandomizer, filters);
            Assert.That(isCompatible, Is.False);
        }

        [TestCase(null, null, null)]
        [TestCase(null, null, "my alignment")]
        [TestCase(null, "my type", null)]
        [TestCase(null, "my type", "my alignment")]
        [TestCase("my challenge rating", null, null)]
        [TestCase("my challenge rating", null, "my alignment")]
        [TestCase("my challenge rating", "my type", null)]
        [TestCase("my challenge rating", "my type", "my alignment")]
        public void VerifyCompatiblity_TemplateWithMinimumAbilityAndFiltersAsCharacter_NotCompatible_IfNotCharacter(string cr, string type, string alignment)
        {
            var filters = new Filters
            {
                Type = type,
                ChallengeRating = cr,
                Alignment = alignment
            };
            filters.Templates.Add("template");

            SetUpCreatureGroup(GroupConstants.Characters, ["character", "wrong character"]);
            SetUpCreatureGroup("template" + true, ["template character", creature, "wrong template creature"]);
            SetUpCreatureGroup("my ability-3", ["ability character", creature, "wrong ability creature"]);

            if (cr != null)
                SetUpCreatureGroup("template" + true + cr, ["template cr character", creature, "wrong cr creature"]);

            if (type != null)
                SetUpCreatureGroup("template" + type, ["template type character", creature, "wrong type creature"]);

            if (alignment != null)
                SetUpCreatureGroup("template" + alignment, ["template alignment character", creature, "wrong alignment creature"]);

            var mockApplicator = SetupApplicator("template");
            var minAbility = new Ability("my ability") { BaseScore = 6 };
            mockApplicator.SetupGet(a => a.MinimumAbility).Returns(minAbility);

            mockDice.Setup(d => d.Roll(abilityRandomizer.Roll).AsPotentialMaximum<int>(true)).Returns(9);

            var isCompatible = verifier.VerifyCompatibility(true, null, abilityRandomizer, filters);
            Assert.That(isCompatible, Is.False);
        }

        [TestCase(null, null, null)]
        [TestCase(null, null, "my alignment")]
        [TestCase(null, "my type", null)]
        [TestCase(null, "my type", "my alignment")]
        [TestCase("my challenge rating", null, null)]
        [TestCase("my challenge rating", null, "my alignment")]
        [TestCase("my challenge rating", "my type", null)]
        [TestCase("my challenge rating", "my type", "my alignment")]
        public void BUG_VerifyCompatiblity_NoneTemplateAndFiltersAsCharacter_NotCompatible_IfNotCharacter(string cr, string type, string alignment)
        {
            var filters = new Filters
            {
                Type = type,
                ChallengeRating = cr,
                Alignment = alignment
            };
            filters.Templates.Add(CreatureConstants.Templates.None);

            SetUpCreatureGroup(GroupConstants.Characters, ["character", "wrong character"]);
            SetUpCreatureGroup(CreatureConstants.Templates.None + true, ["template character", creature, "wrong template creature"]);

            if (cr != null)
                SetUpCreatureGroup(CreatureConstants.Templates.None + true + cr, ["template cr character", creature, "wrong cr creature"]);

            if (type != null)
                SetUpCreatureGroup(CreatureConstants.Templates.None + type, ["template type character", creature, "wrong type creature"]);

            if (alignment != null)
                SetUpCreatureGroup(CreatureConstants.Templates.None + alignment, ["template alignment character", creature, "wrong alignment creature"]);

            var isCompatible = verifier.VerifyCompatibility(true, null, abilityRandomizer, filters);
            Assert.That(isCompatible, Is.False);
        }

        [TestCase(true, null, null, null)]
        [TestCase(true, null, null, "my alignment")]
        [TestCase(true, null, "my type", null)]
        [TestCase(true, null, "my type", "my alignment")]
        [TestCase(true, "my challenge rating", null, null)]
        [TestCase(true, "my challenge rating", null, "my alignment")]
        [TestCase(true, "my challenge rating", "my type", null)]
        [TestCase(true, "my challenge rating", "my type", "my alignment")]
        [TestCase(false, null, null, null)]
        [TestCase(false, null, null, "my alignment")]
        [TestCase(false, null, "my type", null)]
        [TestCase(false, null, "my type", "my alignment")]
        [TestCase(false, "my challenge rating", null, null)]
        [TestCase(false, "my challenge rating", null, "my alignment")]
        [TestCase(false, "my challenge rating", "my type", null)]
        [TestCase(false, "my challenge rating", "my type", "my alignment")]
        public void VerifyCompatiblity_WithFilters_Compatible_BaseCreature(bool asCharacter, string cr, string type, string alignment)
        {
            var filters = new Filters
            {
                Type = type,
                ChallengeRating = cr,
                Alignment = alignment
            };

            SetUpCreatureGroup(CreatureConstants.Templates.None + asCharacter, ["template creature", creature, "wrong template creature"]);

            if (cr != null)
                SetUpCreatureGroup(CreatureConstants.Templates.None + asCharacter + cr, ["template cr creature", creature, "wrong cr creature"]);

            if (type != null)
                SetUpCreatureGroup(CreatureConstants.Templates.None + type, ["template type creature", creature, "wrong type creature"]);

            if (alignment != null)
                SetUpCreatureGroup(CreatureConstants.Templates.None + alignment, ["template alignment creature", creature, "wrong alignment creature"]);

            var templates = new[] { "template", "other template" };
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.TemplateGroups, GroupConstants.All))
                .Returns(templates);

            foreach (var template in templates)
            {
                SetupApplicator(template);
            }

            var isCompatible = verifier.VerifyCompatibility(asCharacter, null, abilityRandomizer, filters);
            Assert.That(isCompatible, Is.True);

            mockCollectionSelector.Verify(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.TemplateGroups, GroupConstants.All), Times.Never);
        }

        [TestCase(true, null, null, null)]
        [TestCase(true, null, null, "my alignment")]
        [TestCase(true, null, "my type", null)]
        [TestCase(true, null, "my type", "my alignment")]
        [TestCase(true, "my challenge rating", null, null)]
        [TestCase(true, "my challenge rating", null, "my alignment")]
        [TestCase(true, "my challenge rating", "my type", null)]
        [TestCase(true, "my challenge rating", "my type", "my alignment")]
        [TestCase(false, null, null, null)]
        [TestCase(false, null, null, "my alignment")]
        [TestCase(false, null, "my type", null)]
        [TestCase(false, null, "my type", "my alignment")]
        [TestCase(false, "my challenge rating", null, null)]
        [TestCase(false, "my challenge rating", null, "my alignment")]
        [TestCase(false, "my challenge rating", "my type", null)]
        [TestCase(false, "my challenge rating", "my type", "my alignment")]
        public void VerifyCompatiblity_WithFilters_Compatible_Template(bool asCharacter, string cr, string type, string alignment)
        {
            var filters = new Filters
            {
                Type = type,
                ChallengeRating = cr,
                Alignment = alignment
            };

            SetUpCreatureGroup(CreatureConstants.Templates.None + asCharacter, ["template creature", creature, "wrong template creature"]);

            if (cr != null)
                SetUpCreatureGroup(CreatureConstants.Templates.None + asCharacter + cr, ["template cr creature", "wrong cr creature"]);

            if (type != null)
                SetUpCreatureGroup(CreatureConstants.Templates.None + type, ["template type creature", "wrong type creature"]);

            if (alignment != null)
                SetUpCreatureGroup(CreatureConstants.Templates.None + alignment, ["template alignment creature", "wrong alignment creature"]);

            var templates = new[] { "template", "other template" };
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.TemplateGroups, GroupConstants.All))
                .Returns(templates);

            foreach (var template in templates)
            {
                SetupApplicator(template);
                var isTemplate = template == "template";

                SetUpCreatureGroup(
                    template + asCharacter,
                    isTemplate ? ["template character", creature, "wrong template creature"] : ["template character", "wrong template creature"]);

                if (cr != null)
                    SetUpCreatureGroup(template + asCharacter + cr, ["template cr creature", creature, "wrong cr creature"]);

                if (type != null)
                    SetUpCreatureGroup(template + type, ["template type creature", creature, "wrong type creature"]);

                if (alignment != null)
                    SetUpCreatureGroup(template + alignment, ["template alignment creature", creature, "wrong alignment creature"]);
            }

            var isCompatible = verifier.VerifyCompatibility(asCharacter, null, abilityRandomizer, filters);
            Assert.That(isCompatible, Is.True);
        }

        [TestCase(true, null, null, null)]
        [TestCase(true, null, null, "my alignment")]
        [TestCase(true, null, "my type", null)]
        [TestCase(true, null, "my type", "my alignment")]
        [TestCase(true, "my challenge rating", null, null)]
        [TestCase(true, "my challenge rating", null, "my alignment")]
        [TestCase(true, "my challenge rating", "my type", null)]
        [TestCase(true, "my challenge rating", "my type", "my alignment")]
        [TestCase(false, null, null, null)]
        [TestCase(false, null, null, "my alignment")]
        [TestCase(false, null, "my type", null)]
        [TestCase(false, null, "my type", "my alignment")]
        [TestCase(false, "my challenge rating", null, null)]
        [TestCase(false, "my challenge rating", null, "my alignment")]
        [TestCase(false, "my challenge rating", "my type", null)]
        [TestCase(false, "my challenge rating", "my type", "my alignment")]
        public void VerifyCompatiblity_WithFilters_Compatible_AnyTemplate(bool asCharacter, string cr, string type, string alignment)
        {
            var filters = new Filters
            {
                Type = type,
                ChallengeRating = cr,
                Alignment = alignment
            };

            SetUpCreatureGroup(CreatureConstants.Templates.None + asCharacter, ["template creature", creature, "wrong template creature"]);

            if (cr != null)
                SetUpCreatureGroup(CreatureConstants.Templates.None + asCharacter + cr, ["template cr creature", "wrong cr creature"]);

            if (type != null)
                SetUpCreatureGroup(CreatureConstants.Templates.None + type, ["template type creature", "wrong type creature"]);

            if (alignment != null)
                SetUpCreatureGroup(CreatureConstants.Templates.None + alignment, ["template alignment creature", "wrong alignment creature"]);

            var templates = new[] { "template", "other template" };
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.TemplateGroups, GroupConstants.All))
                .Returns(templates);

            foreach (var template in templates)
            {
                SetupApplicator(template);
                var isTemplate = template == "other template";

                SetUpCreatureGroup(
                    template + asCharacter,
                    isTemplate ? ["template character", creature, "wrong template creature"] : ["template character", "wrong template creature"]);

                if (cr != null)
                    SetUpCreatureGroup(template + asCharacter + cr, ["template cr creature", creature, "wrong cr creature"]);

                if (type != null)
                    SetUpCreatureGroup(template + type, ["template type creature", creature, "wrong type creature"]);

                if (alignment != null)
                    SetUpCreatureGroup(template + alignment, ["template alignment creature", creature, "wrong alignment creature"]);
            }

            var isCompatible = verifier.VerifyCompatibility(asCharacter, null, abilityRandomizer, filters);
            Assert.That(isCompatible, Is.True);
        }

        [TestCase(null)]
        [TestCase("")]
        public void VerifyCompatiblity_WithFilters_Compatible_Template_IgnoreEmptyTemplates(string empty)
        {
            var filters = new Filters
            {
                Type = "my type",
                ChallengeRating = "my challenge rating",
                Alignment = "my alignment"
            };
            filters.Templates.Add(empty);

            SetUpCreatureGroup(CreatureConstants.Templates.None + false, ["template creature", creature, "wrong template creature"]);
            SetUpCreatureGroup(CreatureConstants.Templates.None + false + filters.ChallengeRating, ["template cr creature", "wrong cr creature"]);
            SetUpCreatureGroup(CreatureConstants.Templates.None + filters.Type, ["template type creature", "wrong type creature"]);
            SetUpCreatureGroup(CreatureConstants.Templates.None + filters.Alignment, ["template alignment creature", "wrong alignment creature"]);

            var templates = new[] { "template", "other template" };
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.TemplateGroups, GroupConstants.All))
                .Returns(templates);

            foreach (var template in templates)
            {
                SetupApplicator(template);
                var isTemplate = template == "template";

                SetUpCreatureGroup(
                    template + false,
                    isTemplate ? ["template character", creature, "wrong template creature"] : ["template character", "wrong template creature"]);

                SetUpCreatureGroup(template + false + filters.ChallengeRating, ["template cr creature", creature, "wrong cr creature"]);
                SetUpCreatureGroup(template + filters.Type, ["template type creature", creature, "wrong type creature"]);
                SetUpCreatureGroup(template + filters.Alignment, ["template alignment creature", creature, "wrong alignment creature"]);
            }

            var isCompatible = verifier.VerifyCompatibility(false, null, abilityRandomizer, filters);
            Assert.That(isCompatible, Is.True);
        }

        [TestCase(true, null, null, null)]
        [TestCase(true, null, null, "my alignment")]
        [TestCase(true, null, "my type", null)]
        [TestCase(true, null, "my type", "my alignment")]
        [TestCase(true, "my challenge rating", null, null)]
        [TestCase(true, "my challenge rating", null, "my alignment")]
        [TestCase(true, "my challenge rating", "my type", null)]
        [TestCase(true, "my challenge rating", "my type", "my alignment")]
        [TestCase(false, null, null, null)]
        [TestCase(false, null, null, "my alignment")]
        [TestCase(false, null, "my type", null)]
        [TestCase(false, null, "my type", "my alignment")]
        [TestCase(false, "my challenge rating", null, null)]
        [TestCase(false, "my challenge rating", null, "my alignment")]
        [TestCase(false, "my challenge rating", "my type", null)]
        [TestCase(false, "my challenge rating", "my type", "my alignment")]
        public void BUG_VerifyCompatiblity_NoneTemplateWithFilters_NotCompatible_DoNotTryRandomTemplates(bool asCharacter, string cr, string type, string alignment)
        {
            var filters = new Filters
            {
                Type = type,
                ChallengeRating = cr,
                Alignment = alignment
            };
            filters.Templates.Add(CreatureConstants.Templates.None);

            SetUpCreatureGroup(CreatureConstants.Templates.None + false, ["template creature", creature, "wrong template creature"]);
            SetUpCreatureGroup(CreatureConstants.Templates.None + false + filters.ChallengeRating, ["template cr creature", "wrong cr creature"]);
            SetUpCreatureGroup(CreatureConstants.Templates.None + filters.Type, ["template type creature", "wrong type creature"]);
            SetUpCreatureGroup(CreatureConstants.Templates.None + filters.Alignment, ["template alignment creature", "wrong alignment creature"]);

            var templates = new[] { "template", "other template" };
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.TemplateGroups, GroupConstants.All))
                .Returns(templates);

            foreach (var template in templates)
            {
                SetupApplicator(template);
                SetUpCreatureGroup(template + asCharacter, ["template character", creature, "wrong template creature"]);
                SetUpCreatureGroup(template + asCharacter + filters.ChallengeRating, ["template cr creature", creature, "wrong cr creature"]);
                SetUpCreatureGroup(template + filters.Type, ["template type creature", creature, "wrong type creature"]);
                SetUpCreatureGroup(template + filters.Alignment, ["template alignment creature", creature, "wrong alignment creature"]);
            }

            var isCompatible = verifier.VerifyCompatibility(asCharacter, null, abilityRandomizer, filters);
            Assert.That(isCompatible, Is.False);

            mockCollectionSelector.Verify(c => c.SelectFrom(Config.Name, TableNameConstants.Collection.TemplateGroups, GroupConstants.All), Times.Never);
        }

        [TestCase(null, null, null)]
        [TestCase(null, null, "my alignment")]
        [TestCase(null, "my type", null)]
        [TestCase(null, "my type", "my alignment")]
        [TestCase("my challenge rating", null, null)]
        [TestCase("my challenge rating", null, "my alignment")]
        [TestCase("my challenge rating", "my type", null)]
        [TestCase("my challenge rating", "my type", "my alignment")]
        public void VerifyCompatiblity_WithFiltersAsCharacter_NotCompatible_IfNotCharacter(string cr, string type, string alignment)
        {
            var filters = new Filters
            {
                Type = type,
                ChallengeRating = cr,
                Alignment = alignment
            };

            SetUpCreatureGroup(GroupConstants.Characters, ["character", "wrong character"]);

            SetUpCreatureGroup(CreatureConstants.Templates.None + true, ["template creature", creature, "wrong template creature"]);
            SetUpCreatureGroup(CreatureConstants.Templates.None + true + filters.ChallengeRating, ["template cr creature", creature, "wrong cr creature"]);
            SetUpCreatureGroup(CreatureConstants.Templates.None + filters.Type, ["template type creature", creature, "wrong type creature"]);
            SetUpCreatureGroup(CreatureConstants.Templates.None + filters.Alignment, ["template alignment creature", creature, "wrong alignment creature"]);

            var templates = new[] { "template", "other template" };
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.TemplateGroups, GroupConstants.All))
                .Returns(templates);

            foreach (var template in templates)
            {
                SetupApplicator(template);
                SetUpCreatureGroup(template + true, ["template character", creature, "wrong template creature"]);
                SetUpCreatureGroup(template + true + filters.ChallengeRating, ["template cr creature", creature, "wrong cr creature"]);
                SetUpCreatureGroup(template + filters.Type, ["template type creature", creature, "wrong type creature"]);
                SetUpCreatureGroup(template + filters.Alignment, ["template alignment creature", creature, "wrong alignment creature"]);
            }

            var isCompatible = verifier.VerifyCompatibility(true, null, abilityRandomizer, filters);
            Assert.That(isCompatible, Is.False);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void GetCompatibleCreaturesForTemplate_ReturnCompatibleCreatures_NoFilters(bool asCharacter)
        {
            var creatures = new[] { creature, "wrong creature 2", "my other creature", "wrong creature 1", "wrong creature 3" };

            var templateCreatures = new[] { "my template creature", "my other creature", "something else", creature, "whatever" };
            SetUpCreatureGroup("my template" + asCharacter, templateCreatures);

            SetupApplicator("my template");

            var compatibleCreatures = verifier.GetCompatibleCreaturesForTemplate(creatures, "my template", asCharacter);
            Assert.That(compatibleCreatures, Is.EqualTo([creature, "my other creature"]));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void GetCompatibleCreaturesForTemplate_ReturnCompatibleCreatures_NoneMatching(bool asCharacter)
        {
            var creatures = new[] { creature, "wrong creature 2", "my other creature", "wrong creature 1", "wrong creature 3" };

            var templateCreatures = new[] { "my template creature", "something else", "whatever" };
            SetUpCreatureGroup("my template" + asCharacter, templateCreatures);

            SetupApplicator("my template");

            var compatibleCreatures = verifier.GetCompatibleCreaturesForTemplate(creatures, "my template", asCharacter);
            Assert.That(compatibleCreatures, Is.Empty);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void GetCompatibleCreaturesForTemplate_ReturnCompatibleCreatures_EmptyTemplateGroup(bool asCharacter)
        {
            var creatures = new[] { creature, "wrong creature 2", "my other creature", "wrong creature 1", "wrong creature 3" };

            SetUpCreatureGroup("my template" + asCharacter, []);

            SetupApplicator("my template");

            var compatibleCreatures = verifier.GetCompatibleCreaturesForTemplate(creatures, "my template", asCharacter);
            Assert.That(compatibleCreatures, Is.Empty);
        }

        private void SetUpCreatureGroup(string groupName, IEnumerable<string> group)
        {
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, groupName))
                .Returns(group);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void GetCompatibleCreaturesForTemplate_WithDefaultAbilityRandomizer_ReturnCompatibleCreatures_NoFilters(bool asCharacter)
        {
            var creatures = new[] { creature, "wrong creature 2", "my other creature", "wrong creature 1", "wrong creature 3" };
            mockDice.Setup(d => d.Roll(AbilityConstants.RandomizerRolls.Default).AsPotentialMaximum<int>(true)).Returns(11);

            var templateCreatures = new[] { "my template creature", "my other creature", "something else", creature, "whatever" };
            SetUpCreatureGroup("my template" + asCharacter, templateCreatures);
            SetUpCreatureGroup("my ability-5", ["low-ability creature", "my other creature", "high-ability creature", creature]);

            var mockApplicator = SetupApplicator("my template");
            var minAbility = new Ability("my ability") { BaseScore = 6 };
            mockApplicator.SetupGet(a => a.MinimumAbility).Returns(minAbility);

            var compatibleCreatures = verifier.GetCompatibleCreaturesForTemplate(creatures, "my template", asCharacter);
            Assert.That(compatibleCreatures, Is.EqualTo([creature, "my other creature"]));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void GetCompatibleCreaturesForTemplate_WithDefaultAbilityRandomizer_ReturnCompatibleCreatures_EmptyTemplateGroup(bool asCharacter)
        {
            var creatures = new[] { creature, "wrong creature 2", "my other creature", "wrong creature 1", "wrong creature 3" };
            mockDice.Setup(d => d.Roll(AbilityConstants.RandomizerRolls.Default).AsPotentialMaximum<int>(true)).Returns(11);

            SetUpCreatureGroup("my template" + asCharacter, []);
            SetUpCreatureGroup("my ability-5", ["low-ability creature", "my other creature", "high-ability creature", creature]);

            var mockApplicator = SetupApplicator("my template");
            var minAbility = new Ability("my ability") { BaseScore = 6 };
            mockApplicator.SetupGet(a => a.MinimumAbility).Returns(minAbility);

            var compatibleCreatures = verifier.GetCompatibleCreaturesForTemplate(creatures, "my template", asCharacter);
            Assert.That(compatibleCreatures, Is.Empty);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void GetCompatibleCreaturesForTemplate_WithDefaultAbilityRandomizer_ReturnCompatibleCreatures_EmptyAbilityGroup(bool asCharacter)
        {
            var creatures = new[] { creature, "wrong creature 2", "my other creature", "wrong creature 1", "wrong creature 3" };
            mockDice.Setup(d => d.Roll(AbilityConstants.RandomizerRolls.Default).AsPotentialMaximum<int>(true)).Returns(11);

            var templateCreatures = new[] { "my template creature", "my other creature", "something else", creature, "whatever" };
            SetUpCreatureGroup("my template" + asCharacter, templateCreatures);
            SetUpCreatureGroup("my ability-5", []);

            var mockApplicator = SetupApplicator("my template");
            var minAbility = new Ability("my ability") { BaseScore = 6 };
            mockApplicator.SetupGet(a => a.MinimumAbility).Returns(minAbility);

            var compatibleCreatures = verifier.GetCompatibleCreaturesForTemplate(creatures, "my template", asCharacter);
            Assert.That(compatibleCreatures, Is.Empty);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void GetCompatibleCreaturesForTemplate_WithDefaultAbilityRandomizer_ReturnCompatibleCreatures_NoneMatching(bool asCharacter)
        {
            var creatures = new[] { creature, "wrong creature 2", "my other creature", "wrong creature 1", "wrong creature 3" };
            mockDice.Setup(d => d.Roll(AbilityConstants.RandomizerRolls.Default).AsPotentialMaximum<int>(true)).Returns(11);

            var templateCreatures = new[] { "my template creature", "something else", "whatever", "my other creature" };
            SetUpCreatureGroup("my template" + asCharacter, templateCreatures);
            SetUpCreatureGroup("my ability-5", ["low-ability creature", "my wrong creature", "high-ability creature", creature]);

            var mockApplicator = SetupApplicator("my template");
            var minAbility = new Ability("my ability") { BaseScore = 6 };
            mockApplicator.SetupGet(a => a.MinimumAbility).Returns(minAbility);

            var compatibleCreatures = verifier.GetCompatibleCreaturesForTemplate(creatures, "my template", asCharacter);
            Assert.That(compatibleCreatures, Is.Empty);
        }

        [TestCase(true, 4, 14)]
        [TestCase(true, 4, 15)]
        [TestCase(true, 4, 16)]
        [TestCase(true, 4, 17)]
        [TestCase(true, 4, 18)]
        [TestCase(true, 4, 19)]
        [TestCase(true, 4, 20)]
        [TestCase(true, 4, 100)]
        [TestCase(true, 6, 16)]
        [TestCase(true, 6, 17)]
        [TestCase(true, 6, 18)]
        [TestCase(true, 6, 19)]
        [TestCase(true, 6, 20)]
        [TestCase(true, 6, 100)]
        [TestCase(false, 4, 14)]
        [TestCase(false, 4, 15)]
        [TestCase(false, 4, 16)]
        [TestCase(false, 4, 17)]
        [TestCase(false, 4, 18)]
        [TestCase(false, 4, 19)]
        [TestCase(false, 4, 20)]
        [TestCase(false, 4, 100)]
        [TestCase(false, 6, 16)]
        [TestCase(false, 6, 17)]
        [TestCase(false, 6, 18)]
        [TestCase(false, 6, 19)]
        [TestCase(false, 6, 20)]
        [TestCase(false, 6, 100)]
        public void GetCompatibleCreaturesForTemplate_WithAbilityRandomizer_ReturnCompatibleCreatures_LowRequiredAdjustment_Roll(bool asCharacter, int minScore, int maxRoll)
        {
            var creatures = new[] { creature, "wrong creature 2", "my other creature", "wrong creature 1", "wrong creature 3" };
            var randomizer = new AbilityRandomizer("my roll");
            mockDice.Setup(d => d.Roll("my roll").AsPotentialMaximum<int>(true)).Returns(maxRoll);

            var templateCreatures = new[] { "my template creature", "my other creature", "something else", creature, "whatever" };
            SetUpCreatureGroup("my template" + asCharacter, templateCreatures);

            var mockApplicator = SetupApplicator("my template");
            var minAbility = new Ability("my ability") { BaseScore = minScore };
            mockApplicator.SetupGet(a => a.MinimumAbility).Returns(minAbility);

            var compatibleCreatures = verifier.GetCompatibleCreaturesForTemplate(creatures, "my template", asCharacter);
            Assert.That(compatibleCreatures, Is.EqualTo([creature, "my other creature"]));
        }

        [TestCase(true, 4, 14)]
        [TestCase(true, 4, 15)]
        [TestCase(true, 4, 16)]
        [TestCase(true, 4, 17)]
        [TestCase(true, 4, 18)]
        [TestCase(true, 4, 19)]
        [TestCase(true, 4, 20)]
        [TestCase(true, 4, 100)]
        [TestCase(true, 6, 16)]
        [TestCase(true, 6, 17)]
        [TestCase(true, 6, 18)]
        [TestCase(true, 6, 19)]
        [TestCase(true, 6, 20)]
        [TestCase(true, 6, 100)]
        [TestCase(false, 4, 14)]
        [TestCase(false, 4, 15)]
        [TestCase(false, 4, 16)]
        [TestCase(false, 4, 17)]
        [TestCase(false, 4, 18)]
        [TestCase(false, 4, 19)]
        [TestCase(false, 4, 20)]
        [TestCase(false, 4, 100)]
        [TestCase(false, 6, 16)]
        [TestCase(false, 6, 17)]
        [TestCase(false, 6, 18)]
        [TestCase(false, 6, 19)]
        [TestCase(false, 6, 20)]
        [TestCase(false, 6, 100)]
        public void GetCompatibleCreaturesForTemplate_WithAbilityRandomizer_ReturnCompatibleCreatures_LowRequiredAdjustment_Set(bool asCharacter, int minScore, int setRoll)
        {
            var creatures = new[] { creature, "wrong creature 2", "my other creature", "wrong creature 1", "wrong creature 3" };
            var randomizer = new AbilityRandomizer("my roll");
            randomizer.SetRolls[AbilityConstants.Charisma] = setRoll;

            var templateCreatures = new[] { "my template creature", "my other creature", "something else", creature, "whatever" };
            SetUpCreatureGroup("my template" + asCharacter, templateCreatures);

            var mockApplicator = SetupApplicator("my template");
            var minAbility = new Ability("my ability") { BaseScore = minScore };
            mockApplicator.SetupGet(a => a.MinimumAbility).Returns(minAbility);

            var compatibleCreatures = verifier.GetCompatibleCreaturesForTemplate(creatures, "my template", asCharacter);
            Assert.That(compatibleCreatures, Is.EqualTo([creature, "my other creature"]));
        }

        [TestCase(true, 4, 1)]
        [TestCase(true, 4, 2)]
        [TestCase(true, 4, 3)]
        [TestCase(true, 4, 4)]
        [TestCase(true, 4, 5)]
        [TestCase(true, 4, 6)]
        [TestCase(true, 4, 7)]
        [TestCase(true, 4, 8)]
        [TestCase(true, 4, 9)]
        [TestCase(true, 4, 10)]
        [TestCase(true, 4, 11)]
        [TestCase(true, 4, 12)]
        [TestCase(true, 4, 13)]
        [TestCase(true, 6, 1)]
        [TestCase(true, 6, 2)]
        [TestCase(true, 6, 3)]
        [TestCase(true, 6, 4)]
        [TestCase(true, 6, 5)]
        [TestCase(true, 6, 6)]
        [TestCase(true, 6, 7)]
        [TestCase(true, 6, 8)]
        [TestCase(true, 6, 9)]
        [TestCase(true, 6, 10)]
        [TestCase(true, 6, 11)]
        [TestCase(true, 6, 12)]
        [TestCase(true, 6, 13)]
        [TestCase(true, 6, 14)]
        [TestCase(true, 6, 15)]
        [TestCase(false, 4, 1)]
        [TestCase(false, 4, 2)]
        [TestCase(false, 4, 3)]
        [TestCase(false, 4, 4)]
        [TestCase(false, 4, 5)]
        [TestCase(false, 4, 6)]
        [TestCase(false, 4, 7)]
        [TestCase(false, 4, 8)]
        [TestCase(false, 4, 9)]
        [TestCase(false, 4, 10)]
        [TestCase(false, 4, 11)]
        [TestCase(false, 4, 12)]
        [TestCase(false, 4, 13)]
        [TestCase(false, 6, 1)]
        [TestCase(false, 6, 2)]
        [TestCase(false, 6, 3)]
        [TestCase(false, 6, 4)]
        [TestCase(false, 6, 5)]
        [TestCase(false, 6, 6)]
        [TestCase(false, 6, 7)]
        [TestCase(false, 6, 8)]
        [TestCase(false, 6, 9)]
        [TestCase(false, 6, 10)]
        [TestCase(false, 6, 11)]
        [TestCase(false, 6, 12)]
        [TestCase(false, 6, 13)]
        [TestCase(false, 6, 14)]
        [TestCase(false, 6, 15)]
        public void GetCompatibleCreaturesForTemplate_WithAbilityRandomizer_ReturnCompatibleCreatures_RequiredAdjustment_Roll(bool asCharacter, int minScore, int maxRoll)
        {
            var creatures = new[] { creature, "wrong creature 2", "my other creature", "wrong creature 1", "wrong creature 3" };
            var randomizer = new AbilityRandomizer("my roll");
            mockDice.Setup(d => d.Roll("my roll").AsPotentialMaximum<int>(true)).Returns(maxRoll);

            var templateCreatures = new[] { "my template creature", "my other creature", "something else", creature, "whatever" };
            SetUpCreatureGroup("my template" + asCharacter, templateCreatures);
            SetUpCreatureGroup($"my ability{minScore - maxRoll}", ["low-ability creature", "my other creature", "high-ability creature", creature]);

            var mockApplicator = SetupApplicator("my template");
            var minAbility = new Ability("my ability") { BaseScore = minScore };
            mockApplicator.SetupGet(a => a.MinimumAbility).Returns(minAbility);

            var compatibleCreatures = verifier.GetCompatibleCreaturesForTemplate(creatures, "my template", asCharacter);
            Assert.That(compatibleCreatures, Is.EqualTo([creature, "my other creature"]));
        }

        [TestCase(true, 4, 1)]
        [TestCase(true, 4, 2)]
        [TestCase(true, 4, 3)]
        [TestCase(true, 4, 4)]
        [TestCase(true, 4, 5)]
        [TestCase(true, 4, 6)]
        [TestCase(true, 4, 7)]
        [TestCase(true, 4, 8)]
        [TestCase(true, 4, 9)]
        [TestCase(true, 4, 10)]
        [TestCase(true, 4, 11)]
        [TestCase(true, 4, 12)]
        [TestCase(true, 4, 13)]
        [TestCase(true, 6, 1)]
        [TestCase(true, 6, 2)]
        [TestCase(true, 6, 3)]
        [TestCase(true, 6, 4)]
        [TestCase(true, 6, 5)]
        [TestCase(true, 6, 6)]
        [TestCase(true, 6, 7)]
        [TestCase(true, 6, 8)]
        [TestCase(true, 6, 9)]
        [TestCase(true, 6, 10)]
        [TestCase(true, 6, 11)]
        [TestCase(true, 6, 12)]
        [TestCase(true, 6, 13)]
        [TestCase(true, 6, 14)]
        [TestCase(true, 6, 15)]
        [TestCase(false, 4, 1)]
        [TestCase(false, 4, 2)]
        [TestCase(false, 4, 3)]
        [TestCase(false, 4, 4)]
        [TestCase(false, 4, 5)]
        [TestCase(false, 4, 6)]
        [TestCase(false, 4, 7)]
        [TestCase(false, 4, 8)]
        [TestCase(false, 4, 9)]
        [TestCase(false, 4, 10)]
        [TestCase(false, 4, 11)]
        [TestCase(false, 4, 12)]
        [TestCase(false, 4, 13)]
        [TestCase(false, 6, 1)]
        [TestCase(false, 6, 2)]
        [TestCase(false, 6, 3)]
        [TestCase(false, 6, 4)]
        [TestCase(false, 6, 5)]
        [TestCase(false, 6, 6)]
        [TestCase(false, 6, 7)]
        [TestCase(false, 6, 8)]
        [TestCase(false, 6, 9)]
        [TestCase(false, 6, 10)]
        [TestCase(false, 6, 11)]
        [TestCase(false, 6, 12)]
        [TestCase(false, 6, 13)]
        [TestCase(false, 6, 14)]
        [TestCase(false, 6, 15)]
        public void GetCompatibleCreaturesForTemplate_WithAbilityRandomizer_ReturnCompatibleCreatures_RequiredAdjustment_Set(bool asCharacter, int minScore, int setRoll)
        {
            var creatures = new[] { creature, "wrong creature 2", "my other creature", "wrong creature 1", "wrong creature 3" };
            var randomizer = new AbilityRandomizer("my roll");
            randomizer.SetRolls[AbilityConstants.Charisma] = setRoll;

            var templateCreatures = new[] { "my template creature", "my other creature", "something else", creature, "whatever" };
            SetUpCreatureGroup("my template" + asCharacter, templateCreatures);
            SetUpCreatureGroup($"my ability{minScore - setRoll}", ["low-ability creature", "my other creature", "high-ability creature", creature]);

            var mockApplicator = SetupApplicator("my template");
            var minAbility = new Ability("my ability") { BaseScore = minScore };
            mockApplicator.SetupGet(a => a.MinimumAbility).Returns(minAbility);

            var compatibleCreatures = verifier.GetCompatibleCreaturesForTemplate(creatures, "my template", asCharacter);
            Assert.That(compatibleCreatures, Is.EqualTo([creature, "my other creature"]));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void GetCompatibleCreaturesForTemplate_WithAlignment_ReturnCompatibleCreatures(bool asCharacter)
        {
            var creatures = new[] { creature, "alignment creature", "my other creature", "wrong creature" };

            SetUpCreatureGroup("my template" + asCharacter, [creature, "ghost creature", "my other creature"]);
            SetUpCreatureGroup("my template" + "preset alignment", [creature, "my other creature", "alignment creature"]);

            SetupApplicator("my template");

            var filters = new Filters { Alignment = "preset alignment" };

            var compatibleCreatures = verifier.GetCompatibleCreaturesForTemplate(creatures, "my template", asCharacter, null, filters);
            Assert.That(compatibleCreatures, Is.EqualTo([creature, "my other creature"]));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void GetCompatibleCreaturesForTemplate_WithAlignment_ReturnCompatibleCreatures_EmptyTemplateGroup(bool asCharacter)
        {
            var creatures = new[] { creature, "alignment creature", "my other creature", "wrong creature" };

            SetUpCreatureGroup("my template" + asCharacter, []);
            SetUpCreatureGroup("my template" + "preset alignment", [creature, "my other creature", "alignment creature"]);

            SetupApplicator("my template");

            var filters = new Filters { Alignment = "preset alignment" };

            var compatibleCreatures = verifier.GetCompatibleCreaturesForTemplate(creatures, "my template", asCharacter, null, filters);
            Assert.That(compatibleCreatures, Is.Empty);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void GetCompatibleCreaturesForTemplate_WithAlignment_ReturnCompatibleCreatures_EmptyAlignmentGroup(bool asCharacter)
        {
            var creatures = new[] { creature, "alignment creature", "my other creature", "wrong creature" };

            SetUpCreatureGroup("my template" + asCharacter, [creature, "ghost creature", "my other creature"]);
            SetUpCreatureGroup("my template" + "preset alignment", []);

            SetupApplicator("my template");

            var filters = new Filters { Alignment = "preset alignment" };

            var compatibleCreatures = verifier.GetCompatibleCreaturesForTemplate(creatures, "my template", asCharacter, null, filters);
            Assert.That(compatibleCreatures, Is.Empty);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void GetCompatibleCreaturesForTemplate_WithAlignment_ReturnCompatibleCreatures_NoneMatching(bool asCharacter)
        {
            var creatures = new[] { creature, "alignment creature", "my other creature", "wrong creature" };

            SetUpCreatureGroup("my template" + asCharacter, [creature, "ghost creature", "alignment creature"]);
            SetUpCreatureGroup("my template" + "preset alignment", ["my other creature", "alignment creature"]);

            SetupApplicator("my template");

            var filters = new Filters { Alignment = "preset alignment" };

            var compatibleCreatures = verifier.GetCompatibleCreaturesForTemplate(creatures, "my template", asCharacter, null, filters);
            Assert.That(compatibleCreatures, Is.Empty);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void GetCompatibleCreaturesForTemplate_WithChallengeRating_ReturnCompatibleCreatures(bool asCharacter)
        {
            var creatures = new[] { creature, "wrong creature 2", "my other creature", "wrong creature 1", "wrong creature 3" };

            SetUpCreatureGroup("my template" + asCharacter, creatures);
            SetUpCreatureGroup("my template" + asCharacter + "my CR", [creature, "my other creature", "CR creature"]);

            SetupApplicator("my template");

            var filters = new Filters { ChallengeRating = "my CR" };

            var compatibleCreatures = verifier.GetCompatibleCreaturesForTemplate(creatures, "my template", asCharacter, null, filters);
            Assert.That(compatibleCreatures, Is.EquivalentTo([creature, "my other creature"]));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void GetCompatibleCreaturesForTemplate_WithChallengeRating_ReturnCompatibleCreatures_EmptyTemplateGroup(bool asCharacter)
        {
            var creatures = new[] { creature, "alignment creature", "my other creature", "wrong creature" };

            SetUpCreatureGroup("my template" + asCharacter, []);
            SetUpCreatureGroup("my template" + asCharacter + "my CR", [creature, "my other creature", "CR creature"]);

            SetupApplicator("my template");

            var filters = new Filters { ChallengeRating = "my CR" };

            var compatibleCreatures = verifier.GetCompatibleCreaturesForTemplate(creatures, "my template", asCharacter, null, filters);
            Assert.That(compatibleCreatures, Is.Empty);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void GetCompatibleCreaturesForTemplate_WithChallengeRating_ReturnCompatibleCreatures_EmptyChallengeRatingGroup(bool asCharacter)
        {
            var creatures = new[] { creature, "alignment creature", "my other creature", "wrong creature" };

            SetUpCreatureGroup("my template" + asCharacter, [creature, "ghost creature", "my other creature"]);
            SetUpCreatureGroup("my template" + asCharacter + "my CR", []);

            SetupApplicator("my template");

            var filters = new Filters { ChallengeRating = "my CR" };

            var compatibleCreatures = verifier.GetCompatibleCreaturesForTemplate(creatures, "my template", asCharacter, null, filters);
            Assert.That(compatibleCreatures, Is.Empty);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void GetCompatibleCreaturesForTemplate_WithChallengeRating_ReturnCompatibleCreatures_NoneMatching(bool asCharacter)
        {
            var creatures = new[] { creature, "alignment creature", "my other creature", "wrong creature" };

            SetUpCreatureGroup("my template" + asCharacter, [creature, "ghost creature", "CR creature"]);
            SetUpCreatureGroup("my template" + asCharacter + "my CR", ["my other creature", "CR creature"]);

            SetupApplicator("my template");

            var filters = new Filters { ChallengeRating = "my CR" };

            var compatibleCreatures = verifier.GetCompatibleCreaturesForTemplate(creatures, "my template", asCharacter, null, filters);
            Assert.That(compatibleCreatures, Is.Empty);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void GetCompatibleCreaturesForTemplate_WithType_ReturnCompatibleCreatures(bool asCharacter)
        {
            var creatures = new[] { creature, "outsider creature", "my other creature", "evil creature", "ghost creature" };

            SetUpCreatureGroup("my template" + asCharacter, [creature, "ghost creature", "my other creature"]);
            SetUpCreatureGroup("my template" + "my type", ["my other creature", creature, "type creature"]);

            SetupApplicator("my template");

            var filters = new Filters { Type = "my type" };

            var compatibleCreatures = verifier.GetCompatibleCreaturesForTemplate(creatures, "my template", asCharacter, null, filters);
            Assert.That(compatibleCreatures, Is.EquivalentTo([creature, "my other creature"]));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void GetCompatibleCreaturesForTemplate_WithType_ReturnCompatibleCreatures_EmptyTemplateGroup(bool asCharacter)
        {
            var creatures = new[] { creature, "outsider creature", "my other creature", "evil creature", "ghost creature" };

            SetUpCreatureGroup("my template" + asCharacter, []);
            SetUpCreatureGroup("my template" + "my type", ["my other creature", creature, "type creature"]);

            SetupApplicator("my template");

            var filters = new Filters { Type = "my type" };

            var compatibleCreatures = verifier.GetCompatibleCreaturesForTemplate(creatures, "my template", asCharacter, null, filters);
            Assert.That(compatibleCreatures, Is.Empty);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void GetCompatibleCreaturesForTemplate_WithType_ReturnCompatibleCreatures_EmptyTypeGroup(bool asCharacter)
        {
            var creatures = new[] { creature, "outsider creature", "my other creature", "evil creature", "ghost creature" };

            SetUpCreatureGroup("my template" + asCharacter, [creature, "ghost creature", "my other creature"]);
            SetUpCreatureGroup("my template" + "my type", []);

            SetupApplicator("my template");

            var filters = new Filters { Type = "my type" };

            var compatibleCreatures = verifier.GetCompatibleCreaturesForTemplate(creatures, "my template", asCharacter, null, filters);
            Assert.That(compatibleCreatures, Is.Empty);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void GetCompatibleCreaturesForTemplate_WithType_ReturnCompatibleCreatures_NoneMatching(bool asCharacter)
        {
            var creatures = new[] { creature, "outsider creature", "my other creature", "evil creature", "ghost creature" };

            SetUpCreatureGroup("my template" + asCharacter, [creature, "ghost creature", "type creature"]);
            SetUpCreatureGroup("my template" + "my type", ["my other creature", "type creature"]);

            SetupApplicator("my template");

            var filters = new Filters { Type = "my type" };

            var compatibleCreatures = verifier.GetCompatibleCreaturesForTemplate(creatures, "my template", asCharacter, null, filters);
            Assert.That(compatibleCreatures, Is.Empty);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void GetCompatibleCreaturesForTemplate_WithAllFilters_ReturnCompatibleCreatures(bool asCharacter)
        {
            var creatures = new[] { creature, "wrong creature", "my other creature", "ghost creature", "alignment creature", "CR creature", "type creature" };
            var randomizer = new AbilityRandomizer("my roll");
            mockDice.Setup(d => d.Roll("my roll").AsPotentialMaximum<int>(true)).Returns(9);

            SetUpCreatureGroup("my template" + asCharacter, [creature, "ghost creature", "my other creature"]);
            SetUpCreatureGroup("my ability-3", ["low-ability creature", "my other creature", "high-ability creature", creature]);
            SetUpCreatureGroup("my template" + "my alignment", ["alignment creature", "my other creature", creature]);
            SetUpCreatureGroup("my template" + asCharacter + "my CR", [creature, "my other creature", "CR creature"]);
            SetUpCreatureGroup("my template" + "my type", ["my other creature", "type creature", creature]);

            var mockApplicator = SetupApplicator("my template");
            var minAbility = new Ability("my ability") { BaseScore = 6 };
            mockApplicator.SetupGet(a => a.MinimumAbility).Returns(minAbility);

            var filters = new Filters { Alignment = "my alignment", ChallengeRating = "my CR", Type = "my type" };

            var compatibleCreatures = verifier.GetCompatibleCreaturesForTemplate(creatures, "my template", asCharacter, randomizer, filters);
            Assert.That(compatibleCreatures, Is.EquivalentTo([creature, "my other creature"]));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void GetCompatibleCreaturesForTemplate_WithAllFilters_ReturnCompatibleCreatures_NoneMatch(bool asCharacter)
        {
            var creatures = new[]
            {
                creature,
                "wrong creature",
                "my other creature",
                "another creature",
                "ghost creature",
                "high-ability creature",
                "alignment creature",
                "CR creature",
                "type creature"
            };
            var randomizer = new AbilityRandomizer("my roll");
            mockDice.Setup(d => d.Roll("my roll").AsPotentialMaximum<int>(true)).Returns(9);

            SetUpCreatureGroup("my template" + asCharacter, [creature, "ghost creature", "my other creature", "another creature"]);
            SetUpCreatureGroup("my ability-3", ["low-ability creature", "my other creature", "high-ability creature", creature]);
            SetUpCreatureGroup("my template" + "my alignment", ["alignment creature", creature, "another creature"]);
            SetUpCreatureGroup("my template" + asCharacter + "my CR", ["my other creature", "CR creature", "another creature"]);
            SetUpCreatureGroup("my template" + "my type", ["my other creature", "type creature", creature, "another creature"]);

            var mockApplicator = SetupApplicator("my template");
            var minAbility = new Ability("my ability") { BaseScore = 6 };
            mockApplicator.SetupGet(a => a.MinimumAbility).Returns(minAbility);

            var filters = new Filters { Alignment = "my alignment", ChallengeRating = "my CR", Type = "my type" };

            var compatibleCreatures = verifier.GetCompatibleCreaturesForTemplate(creatures, "my template", asCharacter, randomizer, filters);
            Assert.That(compatibleCreatures, Is.Empty);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void GetChainedTemplates_NoTemplates_ReturnsPrototypes(bool asCharacter)
        {
            Assert.Fail("not yet written");
        }

        [TestCase(true)]
        [TestCase(false)]
        public void GetChainedTemplates_NoTemplatesWithAbilityRandomizer_ReturnsPrototypes(bool asCharacter)
        {
            Assert.Fail("not yet written");
        }

        [TestCase(true)]
        [TestCase(false)]
        public void GetChainedTemplates_NoTemplatesWithFilters_ReturnsPrototypes(bool asCharacter)
        {
            Assert.Fail("not yet written");
        }

        [TestCase(true)]
        [TestCase(false)]
        public void GetChainedTemplates_NoTemplatesWithAbilityRandomizerAndFilters_ReturnsPrototypes(bool asCharacter)
        {
            Assert.Fail("not yet written");
        }

        [TestCase(true)]
        [TestCase(false)]
        public void GetChainedTemplates_1Template_ReturnsPrototypes(bool asCharacter)
        {
            Assert.Fail("not yet written");
        }

        [TestCase(true)]
        [TestCase(false)]
        public void GetChainedTemplates_1TemplateWithAbilityRandomizer_ReturnsPrototypes(bool asCharacter)
        {
            Assert.Fail("not yet written");
        }

        [TestCase(true)]
        [TestCase(false)]
        public void GetChainedTemplates_1TemplateWithFilters_ReturnsPrototypes(bool asCharacter)
        {
            Assert.Fail("not yet written");
        }

        [TestCase(true)]
        [TestCase(false)]
        public void GetChainedTemplates_1TemplateWithAbilityRandomizerAndFilters_ReturnsPrototypes(bool asCharacter)
        {
            Assert.Fail("not yet written");
        }

        [TestCase(true)]
        [TestCase(false)]
        public void GetChainedTemplates_2Templates_ReturnsPrototypes(bool asCharacter)
        {
            Assert.Fail("not yet written");
        }

        [TestCase(true)]
        [TestCase(false)]
        public void GetChainedTemplates_2TemplatesWithAbilityRandomizer_ReturnsPrototypes(bool asCharacter)
        {
            Assert.Fail("not yet written");
        }

        [TestCase(true)]
        [TestCase(false)]
        public void GetChainedTemplates_2TemplatesWithFilters_ReturnsPrototypes(bool asCharacter)
        {
            Assert.Fail("not yet written");
        }

        [TestCase(true)]
        [TestCase(false)]
        public void GetChainedTemplates_2TemplatesWithAbilityRandomizerAndFilters_ReturnsPrototypes(bool asCharacter)
        {
            Assert.Fail("not yet written");
        }

        [TestCase(true)]
        [TestCase(false)]
        public void GetChainedTemplates_3Templates_ReturnsPrototypes(bool asCharacter)
        {
            Assert.Fail("not yet written");
        }

        [TestCase(true)]
        [TestCase(false)]
        public void GetChainedTemplates_3TemplatesWithAbilityRandomizer_ReturnsPrototypes(bool asCharacter)
        {
            Assert.Fail("not yet written");
        }

        [TestCase(true)]
        [TestCase(false)]
        public void GetChainedTemplates_3TemplatesWithFilters_ReturnsPrototypes(bool asCharacter)
        {
            Assert.Fail("not yet written");
        }

        [TestCase(true)]
        [TestCase(false)]
        public void GetChainedTemplates_3TemplatesWithAbilityRandomizerAndFilters_ReturnsPrototypes(bool asCharacter)
        {
            Assert.Fail("not yet written");
        }
    }
}