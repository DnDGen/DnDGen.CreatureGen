using DnDGen.CreatureGen.Selectors.Collections;
using DnDGen.CreatureGen.Selectors.Selections;
using DnDGen.CreatureGen.Tables;
using DnDGen.CreatureGen.Templates;
using DnDGen.CreatureGen.Verifiers;
using DnDGen.Infrastructure.Generators;
using DnDGen.Infrastructure.Selectors.Collections;
using Moq;
using NUnit.Framework;

namespace DnDGen.CreatureGen.Tests.Unit.Verifiers
{
    [TestFixture]
    public class CreatureVerifierTests
    {
        private ICreatureVerifier verifier;
        private Mock<JustInTimeFactory> mockJustInTimeFactory;
        private Mock<ICreatureDataSelector> mockCreatureDataSelector;
        private Mock<ICollectionSelector> mockCollectionSelector;

        [SetUp]
        public void Setup()
        {
            mockJustInTimeFactory = new Mock<JustInTimeFactory>();
            mockCreatureDataSelector = new Mock<ICreatureDataSelector>();
            mockCollectionSelector = new Mock<ICollectionSelector>();
            verifier = new CreatureVerifier(mockJustInTimeFactory.Object, mockCreatureDataSelector.Object, mockCollectionSelector.Object);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void VerifyCompatibility_CreatureAndTemplate_Compatible_IfTemplateApplicatorSaysSo(bool compatible)
        {
            var mockApplicator = new Mock<TemplateApplicator>();
            mockApplicator.Setup(a => a.IsCompatible("creature")).Returns(compatible);

            mockJustInTimeFactory
                .Setup(f => f.Build<TemplateApplicator>("template"))
                .Returns(mockApplicator.Object);

            var isCompatible = verifier.VerifyCompatibility(false, creature: "creature", template: "template");
            Assert.That(isCompatible, Is.EqualTo(compatible));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void VerifyCompatiblity_CreatureAndTemplateAsCharacter_Compatible_IfCharacterAndTemplateApplicatorSaysSo(bool compatible)
        {
            var mockApplicator = new Mock<TemplateApplicator>();
            mockApplicator.Setup(a => a.IsCompatible("creature")).Returns(compatible);

            mockJustInTimeFactory
                .Setup(f => f.Build<TemplateApplicator>("template"))
                .Returns(mockApplicator.Object);

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Characters))
                .Returns(new[] { "character", "creature", "wrong creature" });

            var isCompatible = verifier.VerifyCompatibility(true, creature: "creature", template: "template");
            Assert.That(isCompatible, Is.EqualTo(compatible));
        }

        [Test]
        public void VerifyCompatiblity_CreatureAndTemplateAsCharacter_NotCompatible_IfNotCharacter()
        {
            var mockApplicator = new Mock<TemplateApplicator>();
            mockApplicator.Setup(a => a.IsCompatible("creature")).Returns(true);

            mockJustInTimeFactory
                .Setup(f => f.Build<TemplateApplicator>("template"))
                .Returns(mockApplicator.Object);

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Characters))
                .Returns(new[] { "character", "wrong creature" });

            var isCompatible = verifier.VerifyCompatibility(true, creature: "creature", template: "template");
            Assert.That(isCompatible, Is.False);
        }

        [Test]
        public void VerifyCompatiblity_Template_Compatible()
        {
            var mockApplicator = new Mock<TemplateApplicator>();
            mockApplicator.Setup(a => a.IsCompatible("creature")).Returns(true);

            mockJustInTimeFactory
                .Setup(f => f.Build<TemplateApplicator>("template"))
                .Returns(mockApplicator.Object);

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.All))
                .Returns(new[] { "character", "creature", "wrong creature" });

            var isCompatible = verifier.VerifyCompatibility(false, template: "template");
            Assert.That(isCompatible, Is.True);
        }

        [Test]
        public void VerifyCompatiblity_Template_NotCompatible_IfNotTemplate()
        {
            var mockApplicator = new Mock<TemplateApplicator>();
            mockApplicator.Setup(a => a.IsCompatible("creature")).Returns(false);

            mockJustInTimeFactory
                .Setup(f => f.Build<TemplateApplicator>("template"))
                .Returns(mockApplicator.Object);

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.All))
                .Returns(new[] { "character", "creature", "wrong creature" });

            var isCompatible = verifier.VerifyCompatibility(false, template: "template");
            Assert.That(isCompatible, Is.False);
        }

        [Test]
        public void VerifyCompatiblity_TemplateAsCharacter_Compatible()
        {
            var mockApplicator = new Mock<TemplateApplicator>();
            mockApplicator.Setup(a => a.IsCompatible("creature")).Returns(true);

            mockJustInTimeFactory
                .Setup(f => f.Build<TemplateApplicator>("template"))
                .Returns(mockApplicator.Object);

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Characters))
                .Returns(new[] { "character", "creature", "wrong creature" });

            var isCompatible = verifier.VerifyCompatibility(true, template: "template");
            Assert.That(isCompatible, Is.True);
        }

        [Test]
        public void VerifyCompatiblity_TemplateAsCharacter_NotCompatible_IfNotTemplate()
        {
            var mockApplicator = new Mock<TemplateApplicator>();
            mockApplicator.Setup(a => a.IsCompatible("creature")).Returns(false);

            mockJustInTimeFactory
                .Setup(f => f.Build<TemplateApplicator>("template"))
                .Returns(mockApplicator.Object);

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Characters))
                .Returns(new[] { "character", "creature", "wrong creature" });

            var isCompatible = verifier.VerifyCompatibility(true, template: "template");
            Assert.That(isCompatible, Is.False);
        }

        [Test]
        public void VerifyCompatiblity_TemplateAsCharacter_NotCompatible_IfNotCharacter()
        {
            var mockApplicator = new Mock<TemplateApplicator>();
            mockApplicator.Setup(a => a.IsCompatible("creature")).Returns(true);

            mockJustInTimeFactory
                .Setup(f => f.Build<TemplateApplicator>("template"))
                .Returns(mockApplicator.Object);

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Characters))
                .Returns(new[] { "character", "wrong creature" });

            var isCompatible = verifier.VerifyCompatibility(true, template: "template");
            Assert.That(isCompatible, Is.False);
        }

        [Test]
        public void VerifyCompatiblity_TemplateAndChallengeRating_Compatible()
        {
            var mockApplicator = new Mock<TemplateApplicator>();
            mockApplicator.Setup(a => a.IsCompatible("creature")).Returns(true);
            mockApplicator.Setup(a => a.GetPotentialChallengeRating("creature")).Returns("challenge rating");

            mockJustInTimeFactory
                .Setup(f => f.Build<TemplateApplicator>("template"))
                .Returns(mockApplicator.Object);

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.All))
                .Returns(new[] { "character", "creature", "wrong creature" });

            var isCompatible = verifier.VerifyCompatibility(false, template: "template", challengeRating: "challenge rating");
            Assert.That(isCompatible, Is.True);
        }

        [Test]
        public void VerifyCompatiblity_TemplateAndChallengeRating_NotCompatible_IfNotTemplate()
        {
            var mockApplicator = new Mock<TemplateApplicator>();
            mockApplicator.Setup(a => a.IsCompatible("creature")).Returns(false);
            mockApplicator.Setup(a => a.GetPotentialChallengeRating("creature")).Returns("challenge rating");

            mockJustInTimeFactory
                .Setup(f => f.Build<TemplateApplicator>("template"))
                .Returns(mockApplicator.Object);

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.All))
                .Returns(new[] { "character", "creature", "wrong creature" });

            var isCompatible = verifier.VerifyCompatibility(false, template: "template", challengeRating: "challenge rating");
            Assert.That(isCompatible, Is.False);
        }

        [Test]
        public void VerifyCompatiblity_TemplateAndChallengeRating_NotCompatible_IfNotChallengeRating()
        {
            var mockApplicator = new Mock<TemplateApplicator>();
            mockApplicator.Setup(a => a.IsCompatible("creature")).Returns(true);
            mockApplicator.Setup(a => a.GetPotentialChallengeRating("creature")).Returns("wrong challenge rating");

            mockJustInTimeFactory
                .Setup(f => f.Build<TemplateApplicator>("template"))
                .Returns(mockApplicator.Object);

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.All))
                .Returns(new[] { "character", "creature", "wrong creature" });

            var isCompatible = verifier.VerifyCompatibility(false, template: "template", challengeRating: "challenge rating");
            Assert.That(isCompatible, Is.False);
        }

        [Test]
        public void VerifyCompatiblity_TemplateAndChallengeRatingAsCharacter_Compatible()
        {
            var mockApplicator = new Mock<TemplateApplicator>();
            mockApplicator.Setup(a => a.IsCompatible("creature")).Returns(true);
            mockApplicator.Setup(a => a.GetPotentialChallengeRating("creature")).Returns("challenge rating");

            mockJustInTimeFactory
                .Setup(f => f.Build<TemplateApplicator>("template"))
                .Returns(mockApplicator.Object);

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Characters))
                .Returns(new[] { "character", "creature", "wrong creature" });

            var isCompatible = verifier.VerifyCompatibility(true, template: "template", challengeRating: "challenge rating");
            Assert.That(isCompatible, Is.True);
        }

        [Test]
        public void VerifyCompatiblity_TemplateAndChallengeRatingAsCharacter_NotCompatible_IfNotTemplate()
        {
            var mockApplicator = new Mock<TemplateApplicator>();
            mockApplicator.Setup(a => a.IsCompatible("creature")).Returns(false);
            mockApplicator.Setup(a => a.GetPotentialChallengeRating("creature")).Returns("challenge rating");

            mockJustInTimeFactory
                .Setup(f => f.Build<TemplateApplicator>("template"))
                .Returns(mockApplicator.Object);

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Characters))
                .Returns(new[] { "character", "creature", "wrong creature" });

            var isCompatible = verifier.VerifyCompatibility(true, template: "template", challengeRating: "challenge rating");
            Assert.That(isCompatible, Is.False);
        }

        [Test]
        public void VerifyCompatiblity_TemplateAndChallengeRatingAsCharacter_NotCompatible_IfNotChallengeRating()
        {
            var mockApplicator = new Mock<TemplateApplicator>();
            mockApplicator.Setup(a => a.IsCompatible("creature")).Returns(true);
            mockApplicator.Setup(a => a.GetPotentialChallengeRating("creature")).Returns("wrong challenge rating");

            mockJustInTimeFactory
                .Setup(f => f.Build<TemplateApplicator>("template"))
                .Returns(mockApplicator.Object);

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Characters))
                .Returns(new[] { "character", "creature", "wrong creature" });

            var isCompatible = verifier.VerifyCompatibility(true, template: "template", challengeRating: "challenge rating");
            Assert.That(isCompatible, Is.False);
        }

        [Test]
        public void VerifyCompatiblity_TemplateAndChallengeRatingAsCharacter_NotCompatible_IfNotCharacter()
        {
            var mockApplicator = new Mock<TemplateApplicator>();
            mockApplicator.Setup(a => a.IsCompatible("creature")).Returns(true);
            mockApplicator.Setup(a => a.GetPotentialChallengeRating("creature")).Returns("challenge rating");

            mockJustInTimeFactory
                .Setup(f => f.Build<TemplateApplicator>("template"))
                .Returns(mockApplicator.Object);

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Characters))
                .Returns(new[] { "character", "wrong creature" });

            var isCompatible = verifier.VerifyCompatibility(true, template: "template", challengeRating: "challenge rating");
            Assert.That(isCompatible, Is.False);
        }

        [Test]
        public void VerifyCompatiblity_TypeAndChallengeRating_Compatible_BaseCreature()
        {
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.All))
                .Returns(new[] { "character", "creature", "wrong creature" });

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "type"))
                .Returns(new[] { "type creature", "creature", "other wrong creature" });

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "challenge rating"))
                .Returns(new[] { "CR creature", "creature", "another wrong creature" });

            var templates = new[] { "template", "other template" };
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Templates))
                .Returns(templates);

            foreach (var template in templates)
            {
                var mockApplicator = new Mock<TemplateApplicator>();
                mockJustInTimeFactory
                    .Setup(f => f.Build<TemplateApplicator>(template))
                    .Returns(mockApplicator.Object);
            }

            var isCompatible = verifier.VerifyCompatibility(false, type: "type", challengeRating: "challenge rating");
            Assert.That(isCompatible, Is.True);
        }

        [Test]
        public void VerifyCompatiblity_TypeAndChallengeRating_Compatible_Template()
        {
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.All))
                .Returns(new[] { "character", "creature", "wrong creature" });

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "type"))
                .Returns(new[] { "type creature", "other wrong creature" });

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "challenge rating"))
                .Returns(new[] { "CR creature", "another wrong creature" });

            var templates = new[] { "template", "other template" };
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Templates))
                .Returns(templates);

            foreach (var template in templates)
            {
                var mockApplicator = new Mock<TemplateApplicator>();
                mockApplicator.Setup(a => a.IsCompatible("creature")).Returns(template == "template");
                mockApplicator.Setup(a => a.GetPotentialChallengeRating("creature")).Returns("challenge rating");
                mockApplicator.Setup(a => a.GetPotentialTypes("creature")).Returns(new[] { "other type", "type", "wrong type" });

                mockJustInTimeFactory
                    .Setup(f => f.Build<TemplateApplicator>(template))
                    .Returns(mockApplicator.Object);
            }

            var isCompatible = verifier.VerifyCompatibility(false, type: "type", challengeRating: "challenge rating");
            Assert.That(isCompatible, Is.True);
        }

        [Test]
        public void VerifyCompatiblity_TypeAndChallengeRating_NotCompatible_IfNotType()
        {
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.All))
                .Returns(new[] { "character", "creature", "wrong creature" });

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "type"))
                .Returns(new[] { "type creature", "other wrong creature" });

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "challenge rating"))
                .Returns(new[] { "CR creature", "another wrong creature" });

            var templates = new[] { "template", "other template" };
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Templates))
                .Returns(templates);

            foreach (var template in templates)
            {
                var mockApplicator = new Mock<TemplateApplicator>();
                mockApplicator.Setup(a => a.IsCompatible("creature")).Returns(template == "template");
                mockApplicator.Setup(a => a.GetPotentialChallengeRating("creature")).Returns("challenge rating");
                mockApplicator.Setup(a => a.GetPotentialTypes("creature")).Returns(new[] { "other type", "wrong type" });

                mockJustInTimeFactory
                    .Setup(f => f.Build<TemplateApplicator>(template))
                    .Returns(mockApplicator.Object);
            }

            var isCompatible = verifier.VerifyCompatibility(false, type: "type", challengeRating: "challenge rating");
            Assert.That(isCompatible, Is.False);
        }

        [Test]
        public void VerifyCompatiblity_TypeAndChallengeRating_NotCompatible_IfNotChallengeRating()
        {
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.All))
                .Returns(new[] { "character", "creature", "wrong creature" });

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "type"))
                .Returns(new[] { "type creature", "other wrong creature" });

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "challenge rating"))
                .Returns(new[] { "CR creature", "another wrong creature" });

            var templates = new[] { "template", "other template" };
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Templates))
                .Returns(templates);

            foreach (var template in templates)
            {
                var mockApplicator = new Mock<TemplateApplicator>();
                mockApplicator.Setup(a => a.IsCompatible("creature")).Returns(template == "template");
                mockApplicator.Setup(a => a.GetPotentialChallengeRating("creature")).Returns("wrong challenge rating");
                mockApplicator.Setup(a => a.GetPotentialTypes("creature")).Returns(new[] { "other type", "type", "wrong type" });

                mockJustInTimeFactory
                    .Setup(f => f.Build<TemplateApplicator>(template))
                    .Returns(mockApplicator.Object);
            }

            var isCompatible = verifier.VerifyCompatibility(false, type: "type", challengeRating: "challenge rating");
            Assert.That(isCompatible, Is.False);
        }

        [Test]
        public void VerifyCompatiblity_TypeAndChallengeRatingAsCharacter_Compatible_BaseCreature()
        {
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Characters))
                .Returns(new[] { "character", "creature", "wrong creature" });

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "type"))
                .Returns(new[] { "type creature", "creature", "other wrong creature" });

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "challenge rating"))
                .Returns(new[] { "CR creature", "creature", "another wrong creature" });

            var templates = new[] { "template", "other template" };
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Templates))
                .Returns(templates);

            foreach (var template in templates)
            {
                var mockApplicator = new Mock<TemplateApplicator>();
                mockJustInTimeFactory
                    .Setup(f => f.Build<TemplateApplicator>(template))
                    .Returns(mockApplicator.Object);
            }

            var isCompatible = verifier.VerifyCompatibility(true, type: "type", challengeRating: "challenge rating");
            Assert.That(isCompatible, Is.True);
        }

        [Test]
        public void VerifyCompatiblity_TypeAndChallengeRatingAsCharacter_Compatible_Template()
        {
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Characters))
                .Returns(new[] { "character", "creature", "wrong creature" });

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "type"))
                .Returns(new[] { "type creature", "other wrong creature" });

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "challenge rating"))
                .Returns(new[] { "CR creature", "another wrong creature" });

            var templates = new[] { "template", "other template" };
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Templates))
                .Returns(templates);

            foreach (var template in templates)
            {
                var mockApplicator = new Mock<TemplateApplicator>();
                mockApplicator.Setup(a => a.IsCompatible("creature")).Returns(template == "template");
                mockApplicator.Setup(a => a.GetPotentialChallengeRating("creature")).Returns("challenge rating");
                mockApplicator.Setup(a => a.GetPotentialTypes("creature")).Returns(new[] { "other type", "type", "wrong type" });

                mockJustInTimeFactory
                    .Setup(f => f.Build<TemplateApplicator>(template))
                    .Returns(mockApplicator.Object);
            }

            var isCompatible = verifier.VerifyCompatibility(true, type: "type", challengeRating: "challenge rating");
            Assert.That(isCompatible, Is.True);
        }

        [Test]
        public void VerifyCompatiblity_TypeAndChallengeRatingAsCharacter_NotCompatible_IfNotType()
        {
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Characters))
                .Returns(new[] { "character", "creature", "wrong creature" });

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "type"))
                .Returns(new[] { "type creature", "other wrong creature" });

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "challenge rating"))
                .Returns(new[] { "CR creature", "another wrong creature" });

            var templates = new[] { "template", "other template" };
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Templates))
                .Returns(templates);

            foreach (var template in templates)
            {
                var mockApplicator = new Mock<TemplateApplicator>();
                mockApplicator.Setup(a => a.IsCompatible("creature")).Returns(template == "template");
                mockApplicator.Setup(a => a.GetPotentialChallengeRating("creature")).Returns("challenge rating");
                mockApplicator.Setup(a => a.GetPotentialTypes("creature")).Returns(new[] { "other type", "wrong type" });

                mockJustInTimeFactory
                    .Setup(f => f.Build<TemplateApplicator>(template))
                    .Returns(mockApplicator.Object);
            }

            var isCompatible = verifier.VerifyCompatibility(true, type: "type", challengeRating: "challenge rating");
            Assert.That(isCompatible, Is.False);
        }

        [Test]
        public void VerifyCompatiblity_TypeAndChallengeRatingAsCharacter_NotCompatible_IfNotChallengeRating()
        {
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Characters))
                .Returns(new[] { "character", "creature", "wrong creature" });

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "type"))
                .Returns(new[] { "type creature", "other wrong creature" });

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "challenge rating"))
                .Returns(new[] { "CR creature", "another wrong creature" });

            var templates = new[] { "template", "other template" };
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Templates))
                .Returns(templates);

            foreach (var template in templates)
            {
                var mockApplicator = new Mock<TemplateApplicator>();
                mockApplicator.Setup(a => a.IsCompatible("creature")).Returns(template == "template");
                mockApplicator.Setup(a => a.GetPotentialChallengeRating("creature")).Returns("wrong challenge rating");
                mockApplicator.Setup(a => a.GetPotentialTypes("creature")).Returns(new[] { "other type", "type", "wrong type" });

                mockJustInTimeFactory
                    .Setup(f => f.Build<TemplateApplicator>(template))
                    .Returns(mockApplicator.Object);
            }

            var isCompatible = verifier.VerifyCompatibility(true, type: "type", challengeRating: "challenge rating");
            Assert.That(isCompatible, Is.False);
        }

        [Test]
        public void VerifyCompatiblity_TypeAndChallengeRatingAsCharacter_NotCompatible_IfNotCharacter()
        {
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Characters))
                .Returns(new[] { "character", "wrong creature" });

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "type"))
                .Returns(new[] { "type creature", "other wrong creature" });

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "challenge rating"))
                .Returns(new[] { "CR creature", "another wrong creature" });

            var templates = new[] { "template", "other template" };
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Templates))
                .Returns(templates);

            foreach (var template in templates)
            {
                var mockApplicator = new Mock<TemplateApplicator>();
                mockApplicator.Setup(a => a.IsCompatible("creature")).Returns(template == "template");
                mockApplicator.Setup(a => a.GetPotentialChallengeRating("creature")).Returns("challenge rating");
                mockApplicator.Setup(a => a.GetPotentialTypes("creature")).Returns(new[] { "other type", "type", "wrong type" });

                mockJustInTimeFactory
                    .Setup(f => f.Build<TemplateApplicator>(template))
                    .Returns(mockApplicator.Object);
            }

            var isCompatible = verifier.VerifyCompatibility(true, type: "type", challengeRating: "challenge rating");
            Assert.That(isCompatible, Is.False);
        }

        [Test]
        public void VerifyCompatiblity_ChallengeRating_Compatible_BaseCreature()
        {
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.All))
                .Returns(new[] { "character", "creature", "wrong creature" });

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "challenge rating"))
                .Returns(new[] { "CR creature", "creature", "another wrong creature" });

            var templates = new[] { "template", "other template" };
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Templates))
                .Returns(templates);

            foreach (var template in templates)
            {
                var mockApplicator = new Mock<TemplateApplicator>();
                mockJustInTimeFactory
                    .Setup(f => f.Build<TemplateApplicator>(template))
                    .Returns(mockApplicator.Object);
            }

            var isCompatible = verifier.VerifyCompatibility(false, challengeRating: "challenge rating");
            Assert.That(isCompatible, Is.True);
        }

        [Test]
        public void VerifyCompatiblity_ChallengeRating_Compatible_Template()
        {
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.All))
                .Returns(new[] { "character", "creature", "wrong creature" });

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "challenge rating"))
                .Returns(new[] { "CR creature", "another wrong creature" });

            var templates = new[] { "template", "other template" };
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Templates))
                .Returns(templates);

            foreach (var template in templates)
            {
                var mockApplicator = new Mock<TemplateApplicator>();
                mockApplicator.Setup(a => a.IsCompatible("creature")).Returns(template == "template");
                mockApplicator.Setup(a => a.GetPotentialChallengeRating("creature")).Returns("challenge rating");

                mockJustInTimeFactory
                    .Setup(f => f.Build<TemplateApplicator>(template))
                    .Returns(mockApplicator.Object);
            }

            var isCompatible = verifier.VerifyCompatibility(false, challengeRating: "challenge rating");
            Assert.That(isCompatible, Is.True);
        }

        [Test]
        public void VerifyCompatiblity_ChallengeRating_NotCompatible_IfNotChallengeRating()
        {
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.All))
                .Returns(new[] { "character", "creature", "wrong creature" });

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "challenge rating"))
                .Returns(new[] { "CR creature", "another wrong creature" });

            var templates = new[] { "template", "other template" };
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Templates))
                .Returns(templates);

            foreach (var template in templates)
            {
                var mockApplicator = new Mock<TemplateApplicator>();
                mockApplicator.Setup(a => a.IsCompatible("creature")).Returns(template == "template");
                mockApplicator.Setup(a => a.GetPotentialChallengeRating("creature")).Returns("wrong challenge rating");

                mockJustInTimeFactory
                    .Setup(f => f.Build<TemplateApplicator>(template))
                    .Returns(mockApplicator.Object);
            }

            var isCompatible = verifier.VerifyCompatibility(false, challengeRating: "challenge rating");
            Assert.That(isCompatible, Is.False);
        }

        [Test]
        public void VerifyCompatiblity_ChallengeRatingAsCharacter_Compatible_BaseCreature()
        {
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Characters))
                .Returns(new[] { "character", "creature", "wrong creature" });

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "challenge rating"))
                .Returns(new[] { "CR creature", "creature", "another wrong creature" });

            var templates = new[] { "template", "other template" };
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Templates))
                .Returns(templates);

            foreach (var template in templates)
            {
                var mockApplicator = new Mock<TemplateApplicator>();
                mockJustInTimeFactory
                    .Setup(f => f.Build<TemplateApplicator>(template))
                    .Returns(mockApplicator.Object);
            }

            var isCompatible = verifier.VerifyCompatibility(true, challengeRating: "challenge rating");
            Assert.That(isCompatible, Is.True);
        }

        [Test]
        public void VerifyCompatiblity_ChallengeRatingAsCharacter_Compatible_Template()
        {
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Characters))
                .Returns(new[] { "character", "creature", "wrong creature" });

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "challenge rating"))
                .Returns(new[] { "CR creature", "another wrong creature" });

            var templates = new[] { "template", "other template" };
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Templates))
                .Returns(templates);

            foreach (var template in templates)
            {
                var mockApplicator = new Mock<TemplateApplicator>();
                mockApplicator.Setup(a => a.IsCompatible("creature")).Returns(template == "template");
                mockApplicator.Setup(a => a.GetPotentialChallengeRating("creature")).Returns("challenge rating");

                mockJustInTimeFactory
                    .Setup(f => f.Build<TemplateApplicator>(template))
                    .Returns(mockApplicator.Object);
            }

            var isCompatible = verifier.VerifyCompatibility(true, challengeRating: "challenge rating");
            Assert.That(isCompatible, Is.True);
        }

        [Test]
        public void VerifyCompatiblity_ChallengeRatingAsCharacter_NotCompatible_IfNotChallengeRating()
        {
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Characters))
                .Returns(new[] { "character", "creature", "wrong creature" });

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "challenge rating"))
                .Returns(new[] { "CR creature", "another wrong creature" });

            var templates = new[] { "template", "other template" };
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Templates))
                .Returns(templates);

            foreach (var template in templates)
            {
                var mockApplicator = new Mock<TemplateApplicator>();
                mockApplicator.Setup(a => a.IsCompatible("creature")).Returns(template == "template");
                mockApplicator.Setup(a => a.GetPotentialChallengeRating("creature")).Returns("wrong challenge rating");

                mockJustInTimeFactory
                    .Setup(f => f.Build<TemplateApplicator>(template))
                    .Returns(mockApplicator.Object);
            }

            var isCompatible = verifier.VerifyCompatibility(true, challengeRating: "challenge rating");
            Assert.That(isCompatible, Is.False);
        }

        [Test]
        public void VerifyCompatiblity_ChallengeRatingAsCharacter_NotCompatible_IfNotCharacter()
        {
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Characters))
                .Returns(new[] { "character", "wrong creature" });

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "challenge rating"))
                .Returns(new[] { "CR creature", "another wrong creature" });

            var templates = new[] { "template", "other template" };
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Templates))
                .Returns(templates);

            foreach (var template in templates)
            {
                var mockApplicator = new Mock<TemplateApplicator>();
                mockApplicator.Setup(a => a.IsCompatible("creature")).Returns(template == "template");
                mockApplicator.Setup(a => a.GetPotentialChallengeRating("creature")).Returns("challenge rating");

                mockJustInTimeFactory
                    .Setup(f => f.Build<TemplateApplicator>(template))
                    .Returns(mockApplicator.Object);
            }

            var isCompatible = verifier.VerifyCompatibility(true, challengeRating: "challenge rating");
            Assert.That(isCompatible, Is.False);
        }

        [Test]
        public void VerifyCompatiblity_Type_Compatible_BaseCreature()
        {
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.All))
                .Returns(new[] { "character", "creature", "wrong creature" });

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "type"))
                .Returns(new[] { "type creature", "creature", "other wrong creature" });

            var templates = new[] { "template", "other template" };
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Templates))
                .Returns(templates);

            foreach (var template in templates)
            {
                var mockApplicator = new Mock<TemplateApplicator>();
                mockJustInTimeFactory
                    .Setup(f => f.Build<TemplateApplicator>(template))
                    .Returns(mockApplicator.Object);
            }

            var isCompatible = verifier.VerifyCompatibility(false, type: "type");
            Assert.That(isCompatible, Is.True);
        }

        [Test]
        public void VerifyCompatiblity_Type_Compatible_Template()
        {
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.All))
                .Returns(new[] { "character", "creature", "wrong creature" });

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "type"))
                .Returns(new[] { "type creature", "other wrong creature" });

            var templates = new[] { "template", "other template" };
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Templates))
                .Returns(templates);

            foreach (var template in templates)
            {
                var mockApplicator = new Mock<TemplateApplicator>();
                mockApplicator.Setup(a => a.IsCompatible("creature")).Returns(template == "template");
                mockApplicator.Setup(a => a.GetPotentialTypes("creature")).Returns(new[] { "other type", "type", "wrong type" });

                mockJustInTimeFactory
                    .Setup(f => f.Build<TemplateApplicator>(template))
                    .Returns(mockApplicator.Object);
            }

            var isCompatible = verifier.VerifyCompatibility(false, type: "type");
            Assert.That(isCompatible, Is.True);
        }

        [Test]
        public void VerifyCompatiblity_Type_NotCompatible_IfNotType()
        {
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.All))
                .Returns(new[] { "character", "creature", "wrong creature" });

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "type"))
                .Returns(new[] { "type creature", "other wrong creature" });

            var templates = new[] { "template", "other template" };
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Templates))
                .Returns(templates);

            foreach (var template in templates)
            {
                var mockApplicator = new Mock<TemplateApplicator>();
                mockApplicator.Setup(a => a.IsCompatible("creature")).Returns(template == "template");
                mockApplicator.Setup(a => a.GetPotentialTypes("creature")).Returns(new[] { "other type", "wrong type" });

                mockJustInTimeFactory
                    .Setup(f => f.Build<TemplateApplicator>(template))
                    .Returns(mockApplicator.Object);
            }

            var isCompatible = verifier.VerifyCompatibility(false, type: "type");
            Assert.That(isCompatible, Is.False);
        }

        [Test]
        public void VerifyCompatiblity_TypeAsCharacter_Compatible_BaseCreature()
        {
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Characters))
                .Returns(new[] { "character", "creature", "wrong creature" });

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "type"))
                .Returns(new[] { "type creature", "creature", "other wrong creature" });

            var templates = new[] { "template", "other template" };
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Templates))
                .Returns(templates);

            foreach (var template in templates)
            {
                var mockApplicator = new Mock<TemplateApplicator>();
                mockJustInTimeFactory
                    .Setup(f => f.Build<TemplateApplicator>(template))
                    .Returns(mockApplicator.Object);
            }

            var isCompatible = verifier.VerifyCompatibility(true, type: "type");
            Assert.That(isCompatible, Is.True);
        }

        [Test]
        public void VerifyCompatiblity_TypeAsCharacter_Compatible_Template()
        {
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Characters))
                .Returns(new[] { "character", "creature", "wrong creature" });

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "type"))
                .Returns(new[] { "type creature", "other wrong creature" });

            var templates = new[] { "template", "other template" };
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Templates))
                .Returns(templates);

            foreach (var template in templates)
            {
                var mockApplicator = new Mock<TemplateApplicator>();
                mockApplicator.Setup(a => a.IsCompatible("creature")).Returns(template == "template");
                mockApplicator.Setup(a => a.GetPotentialTypes("creature")).Returns(new[] { "other type", "type", "wrong type" });

                mockJustInTimeFactory
                    .Setup(f => f.Build<TemplateApplicator>(template))
                    .Returns(mockApplicator.Object);
            }

            var isCompatible = verifier.VerifyCompatibility(true, type: "type");
            Assert.That(isCompatible, Is.True);
        }

        [Test]
        public void VerifyCompatiblity_TypeAsCharacter_NotCompatible_IfNotType()
        {
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Characters))
                .Returns(new[] { "character", "creature", "wrong creature" });

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "type"))
                .Returns(new[] { "type creature", "other wrong creature" });

            var templates = new[] { "template", "other template" };
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Templates))
                .Returns(templates);

            foreach (var template in templates)
            {
                var mockApplicator = new Mock<TemplateApplicator>();
                mockApplicator.Setup(a => a.IsCompatible("creature")).Returns(template == "template");
                mockApplicator.Setup(a => a.GetPotentialTypes("creature")).Returns(new[] { "other type", "wrong type" });

                mockJustInTimeFactory
                    .Setup(f => f.Build<TemplateApplicator>(template))
                    .Returns(mockApplicator.Object);
            }

            var isCompatible = verifier.VerifyCompatibility(true, type: "type");
            Assert.That(isCompatible, Is.False);
        }

        [Test]
        public void VerifyCompatiblity_TypeAsCharacter_NotCompatible_IfNotCharacter()
        {
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Characters))
                .Returns(new[] { "character", "wrong creature" });

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "type"))
                .Returns(new[] { "type creature", "other wrong creature" });

            var templates = new[] { "template", "other template" };
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Templates))
                .Returns(templates);

            foreach (var template in templates)
            {
                var mockApplicator = new Mock<TemplateApplicator>();
                mockApplicator.Setup(a => a.IsCompatible("creature")).Returns(template == "template");
                mockApplicator.Setup(a => a.GetPotentialTypes("creature")).Returns(new[] { "other type", "type", "wrong type" });

                mockJustInTimeFactory
                    .Setup(f => f.Build<TemplateApplicator>(template))
                    .Returns(mockApplicator.Object);
            }

            var isCompatible = verifier.VerifyCompatibility(true, type: "type");
            Assert.That(isCompatible, Is.False);
        }

        [Test]
        public void CanBeCharacter_FalseIfNullLevelAdjustment()
        {
            var creatureData = new CreatureDataSelection();
            creatureData.LevelAdjustment = null;

            mockCreatureDataSelector
                .Setup(s => s.SelectFor("creature"))
                .Returns(creatureData);

            var canBeCharacter = verifier.CanBeCharacter("creature");
            Assert.That(canBeCharacter, Is.False);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(10)]
        public void CanBeCharacter_TrueIfLevelAdjustment(int levelAdjustment)
        {
            var creatureData = new CreatureDataSelection();
            creatureData.LevelAdjustment = levelAdjustment;

            mockCreatureDataSelector
                .Setup(s => s.SelectFor("creature"))
                .Returns(creatureData);

            var canBeCharacter = verifier.CanBeCharacter("creature");
            Assert.That(canBeCharacter, Is.True);
        }
    }
}