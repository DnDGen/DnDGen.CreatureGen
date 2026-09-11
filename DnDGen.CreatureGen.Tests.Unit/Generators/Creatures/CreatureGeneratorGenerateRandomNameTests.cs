using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Generators.Creatures;
using DnDGen.CreatureGen.Tests.Unit.TestCaseSources;
using DnDGen.CreatureGen.Verifiers.Exceptions;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Unit.Generators.Creatures
{
    [TestFixture]
    internal class CreatureGeneratorGenerateRandomNameTests : CreatureGeneratorTestsBase
    {
        [TestCase(true, null, null, null)]
        [TestCase(true, null, null, "my alignment")]
        [TestCase(true, null, "my challenge rating", null)]
        [TestCase(true, null, "my challenge rating", "my alignment")]
        [TestCase(true, "my type", null, null)]
        [TestCase(true, "my type", null, "my alignment")]
        [TestCase(true, "my type", "my challenge rating", null)]
        [TestCase(true, "my type", "my challenge rating", "my alignment")]
        [TestCase(false, null, null, null)]
        [TestCase(false, null, null, "my alignment")]
        [TestCase(false, null, "my challenge rating", null)]
        [TestCase(false, null, "my challenge rating", "my alignment")]
        [TestCase(false, "my type", null, null)]
        [TestCase(false, "my type", null, "my alignment")]
        [TestCase(false, "my type", "my challenge rating", null)]
        [TestCase(false, "my type", "my challenge rating", "my alignment")]
        public void GenerateRandomName_GenerateCreatureName_NoTemplate(bool asCharacter, string type, string cr, string alignment)
        {
            var creatureName = "my creature";
            var template = "my template";
            var filters = new Filters
            {
                Types = [type],
                ChallengeRatings = [cr],
                Alignments = [alignment]
            };

            SetupAllCreatureGroup(asCharacter, ["wrong creature", creatureName, "other creature"]);
            SetupAllTemplateGroup(["wrong template", template, "other template"]);
            SetupCreatureValidity(creatureName, asCharacter, filters);
            SetupIndividualTemplateValidity("other template", asCharacter, creatureName, null, filters);
            SetupIndividualTemplateValidity("wrong template", asCharacter, creatureName, null, filters, []);


            var name = creatureGenerator.GenerateRandomName(asCharacter, filters);
            Assert.That(name.Creature, Is.EqualTo(creatureName));
            Assert.That(name.Templates, Is.Empty);
        }

        [TestCase(null)]
        [TestCase("")]
        public void BUG_GenerateRandomName_GenerateCreatureName_EmptyTemplate(string empty)
        {
            var creatureName = "my creature";
            var template = "my template";
            var filters = new Filters
            {
                Types = ["my type"],
                ChallengeRatings = ["my challenge rating"],
                Alignments = ["my alignment"]
            };

            SetupAllCreatureGroup(false, ["wrong creature", creatureName, "other creature"]);
            SetupAllTemplateGroup(["wrong template", template, "other template"]);
            SetupCreatureValidity(creatureName, false, filters);
            SetupIndividualTemplateValidity("other template", false, creatureName, null, filters);
            SetupIndividualTemplateValidity("wrong template", false, creatureName, null, filters, []);

            var name = creatureGenerator.GenerateRandomName(false, filters, empty);
            Assert.That(name.Creature, Is.EqualTo(creatureName));
            Assert.That(name.Templates, Is.Empty);
        }

        [TestCase(true, null, null, null)]
        [TestCase(true, null, null, "my alignment")]
        [TestCase(true, null, "my challenge rating", null)]
        [TestCase(true, null, "my challenge rating", "my alignment")]
        [TestCase(true, "my type", null, null)]
        [TestCase(true, "my type", null, "my alignment")]
        [TestCase(true, "my type", "my challenge rating", null)]
        [TestCase(true, "my type", "my challenge rating", "my alignment")]
        [TestCase(false, null, null, null)]
        [TestCase(false, null, null, "my alignment")]
        [TestCase(false, null, "my challenge rating", null)]
        [TestCase(false, null, "my challenge rating", "my alignment")]
        [TestCase(false, "my type", null, null)]
        [TestCase(false, "my type", null, "my alignment")]
        [TestCase(false, "my type", "my challenge rating", null)]
        [TestCase(false, "my type", "my challenge rating", "my alignment")]
        public void GenerateRandomName_GenerateCreatureName_WithPresetNoneTemplate(bool asCharacter, string type, string cr, string alignment)
        {
            var creatureName = "my creature";
            var template = CreatureConstants.Templates.None;
            var filters = new Filters
            {
                Types = [type],
                ChallengeRatings = [cr],
                Alignments = [alignment]
            };

            SetupAllCreatureGroup(asCharacter, ["wrong creature", creatureName, "other creature"]);
            SetupCreatureValidity(creatureName, asCharacter, filters, null, template);

            var name = creatureGenerator.GenerateRandomName(asCharacter, filters, template);
            Assert.That(name.Creature, Is.EqualTo(creatureName));
            Assert.That(name.Templates.Single(), Is.EqualTo(template));
        }

        [TestCase(true, null, null, null)]
        [TestCase(true, null, null, "my alignment")]
        [TestCase(true, null, "my challenge rating", null)]
        [TestCase(true, null, "my challenge rating", "my alignment")]
        [TestCase(true, "my type", null, null)]
        [TestCase(true, "my type", null, "my alignment")]
        [TestCase(true, "my type", "my challenge rating", null)]
        [TestCase(true, "my type", "my challenge rating", "my alignment")]
        [TestCase(false, null, null, null)]
        [TestCase(false, null, null, "my alignment")]
        [TestCase(false, null, "my challenge rating", null)]
        [TestCase(false, null, "my challenge rating", "my alignment")]
        [TestCase(false, "my type", null, null)]
        [TestCase(false, "my type", null, "my alignment")]
        [TestCase(false, "my type", "my challenge rating", null)]
        [TestCase(false, "my type", "my challenge rating", "my alignment")]
        public void GenerateRandomName_GenerateCreatureName_WithPresetTemplate(bool asCharacter, string type, string cr, string alignment)
        {
            var creatureName = "my creature";
            var template = "my template";
            var filters = new Filters
            {
                Types = [type],
                ChallengeRatings = [cr],
                Alignments = [alignment]
            };

            SetupAllCreatureGroup(asCharacter, ["wrong creature", creatureName, "other creature"]);
            SetupCreatureValidity(creatureName, asCharacter, filters, null, template);

            var name = creatureGenerator.GenerateRandomName(asCharacter, filters, template);
            Assert.That(name.Creature, Is.EqualTo(creatureName));
            Assert.That(name.Templates.Single(), Is.EqualTo(template));
        }

        [TestCase(true, null, null, null)]
        [TestCase(true, null, null, "my alignment")]
        [TestCase(true, null, "my challenge rating", null)]
        [TestCase(true, null, "my challenge rating", "my alignment")]
        [TestCase(true, "my type", null, null)]
        [TestCase(true, "my type", null, "my alignment")]
        [TestCase(true, "my type", "my challenge rating", null)]
        [TestCase(true, "my type", "my challenge rating", "my alignment")]
        [TestCase(false, null, null, null)]
        [TestCase(false, null, null, "my alignment")]
        [TestCase(false, null, "my challenge rating", null)]
        [TestCase(false, null, "my challenge rating", "my alignment")]
        [TestCase(false, "my type", null, null)]
        [TestCase(false, "my type", null, "my alignment")]
        [TestCase(false, "my type", "my challenge rating", null)]
        [TestCase(false, "my type", "my challenge rating", "my alignment")]
        public void GenerateRandomName_GenerateCreatureName_WithMultiplePresetTemplates(bool asCharacter, string type, string cr, string alignment)
        {
            var creatureName = "my creature";
            var template1 = "my template";
            var template2 = "my other template";
            var filters = new Filters
            {
                Types = [type],
                ChallengeRatings = [cr],
                Alignments = [alignment]
            };
            var templates = new[] { template1, template2 };

            SetupAllCreatureGroup(asCharacter, ["wrong creature", creatureName, "other creature"]);
            SetupCreatureValidity(creatureName, asCharacter, filters, null, template1, template2);

            var name = creatureGenerator.GenerateRandomName(asCharacter, filters, templates);
            Assert.That(name.Creature, Is.EqualTo(creatureName));
            Assert.That(name.Templates, Is.EqualTo([template1, template2]));
        }

        [TestCase(true, null, null, null)]
        [TestCase(true, null, null, "my alignment")]
        [TestCase(true, null, "my challenge rating", null)]
        [TestCase(true, null, "my challenge rating", "my alignment")]
        [TestCase(true, "my type", null, null)]
        [TestCase(true, "my type", null, "my alignment")]
        [TestCase(true, "my type", "my challenge rating", null)]
        [TestCase(true, "my type", "my challenge rating", "my alignment")]
        [TestCase(false, null, null, null)]
        [TestCase(false, null, null, "my alignment")]
        [TestCase(false, null, "my challenge rating", null)]
        [TestCase(false, null, "my challenge rating", "my alignment")]
        [TestCase(false, "my type", null, null)]
        [TestCase(false, "my type", null, "my alignment")]
        [TestCase(false, "my type", "my challenge rating", null)]
        [TestCase(false, "my type", "my challenge rating", "my alignment")]
        public void GenerateRandomName_GenerateCreatureName_WithRandomTemplate_Only1TemplateCompatible(bool asCharacter, string type, string cr, string alignment)
        {
            var creatureName = "my creature";
            var template = "my template";
            var filters = new Filters
            {
                Types = [type],
                ChallengeRatings = [cr],
                Alignments = [alignment]
            };

            SetupFilterValidity(asCharacter, filters, null);
            SetupAllCreatureGroup(asCharacter, ["wrong creature", creatureName, "other creature"]);
            SetupAllTemplateGroup(["wrong template", template, "other template"]);
            SetupDefaultTemplateValidity(asCharacter, creatureName, null, filters, []);
            SetupIndividualTemplateValidity(template, asCharacter, creatureName, null, filters);
            SetupIndividualTemplateValidity("other template", asCharacter, creatureName, null, filters, []);
            SetupIndividualTemplateValidity("wrong template", asCharacter, creatureName, null, filters, []);

            var name = creatureGenerator.GenerateRandomName(asCharacter, filters);
            Assert.That(name.Creature, Is.EqualTo(creatureName));
            Assert.That(name.Templates.Single(), Is.EqualTo(template));
        }

        [TestCase(true, null, null, null)]
        [TestCase(true, null, null, "my alignment")]
        [TestCase(true, null, "my challenge rating", null)]
        [TestCase(true, null, "my challenge rating", "my alignment")]
        [TestCase(true, "my type", null, null)]
        [TestCase(true, "my type", null, "my alignment")]
        [TestCase(true, "my type", "my challenge rating", null)]
        [TestCase(true, "my type", "my challenge rating", "my alignment")]
        [TestCase(false, null, null, null)]
        [TestCase(false, null, null, "my alignment")]
        [TestCase(false, null, "my challenge rating", null)]
        [TestCase(false, null, "my challenge rating", "my alignment")]
        [TestCase(false, "my type", null, null)]
        [TestCase(false, "my type", null, "my alignment")]
        [TestCase(false, "my type", "my challenge rating", null)]
        [TestCase(false, "my type", "my challenge rating", "my alignment")]
        public void GenerateRandomName_GenerateCreatureName_WithRandomTemplate_OnlyTemplatesCompatible(bool asCharacter, string type, string cr, string alignment)
        {
            var creatureName = "my creature";
            var template = "my template";
            var filters = new Filters
            {
                Types = [type],
                ChallengeRatings = [cr],
                Alignments = [alignment]
            };

            SetupFilterValidity(asCharacter, filters, null);
            SetupAllCreatureGroup(asCharacter, ["wrong creature", creatureName, "other creature"]);
            SetupAllTemplateGroup(["wrong template", template, "other template"]);
            SetupDefaultTemplateValidity(asCharacter, creatureName, null, filters, []);
            SetupIndividualTemplateValidity(template, asCharacter, creatureName, null, filters);
            SetupIndividualTemplateValidity("other template", asCharacter, creatureName, null, filters);
            SetupIndividualTemplateValidity("wrong template", asCharacter, creatureName, null, filters, []);

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(It.Is<IEnumerable<string>>(cc => cc.IsEquivalentTo(template, "other template"))))
                .Returns(template);

            var name = creatureGenerator.GenerateRandomName(asCharacter, filters);
            Assert.That(name.Creature, Is.EqualTo(creatureName));
            Assert.That(name.Templates.Single(), Is.EqualTo(template));
        }

        [TestCase(true, null, null, null)]
        [TestCase(true, null, null, "my alignment")]
        [TestCase(true, null, "my challenge rating", null)]
        [TestCase(true, null, "my challenge rating", "my alignment")]
        [TestCase(true, "my type", null, null)]
        [TestCase(true, "my type", null, "my alignment")]
        [TestCase(true, "my type", "my challenge rating", null)]
        [TestCase(true, "my type", "my challenge rating", "my alignment")]
        [TestCase(false, null, null, null)]
        [TestCase(false, null, null, "my alignment")]
        [TestCase(false, null, "my challenge rating", null)]
        [TestCase(false, null, "my challenge rating", "my alignment")]
        [TestCase(false, "my type", null, null)]
        [TestCase(false, "my type", null, "my alignment")]
        [TestCase(false, "my type", "my challenge rating", null)]
        [TestCase(false, "my type", "my challenge rating", "my alignment")]
        public void GenerateRandomName_GenerateRandomCreatureName_NoTemplate(bool asCharacter, string type, string cr, string alignment)
        {
            var creatureName = "my creature";
            var template = "my template";
            var filters = new Filters
            {
                Types = [type],
                ChallengeRatings = [cr],
                Alignments = [alignment]
            };
            var creatures = new[] { "wrong creature", creatureName, "other creature" };
            var templates = new[] { "wrong template", template, "other template" };

            SetupFilterValidity(asCharacter, filters, null);
            SetupAllCreatureGroup(asCharacter, creatures);
            SetupAllTemplateGroup(templates);
            SetupDefaultTemplateValidity(asCharacter, creatureName, null, filters, [.. creatures.Except(["wrong creature"])]);
            SetupIndividualTemplateValidity(template, asCharacter, creatureName, null, filters, creatures);
            SetupIndividualTemplateValidity("other template", asCharacter, creatureName, null, filters, creatures);
            SetupIndividualTemplateValidity("wrong template", asCharacter, creatureName, null, filters, []);

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(It.Is<IEnumerable<string>>(cc => cc.IsEquivalentTo(creatures.Concat(new[] { template, "other template" })))))
                .Returns(creatureName);

            var name = creatureGenerator.GenerateRandomName(asCharacter, filters);
            Assert.That(name.Creature, Is.EqualTo(creatureName));
            Assert.That(name.Templates, Is.Empty);
        }

        [TestCase(true, null, null, null)]
        [TestCase(true, null, null, "my alignment")]
        [TestCase(true, null, "my challenge rating", null)]
        [TestCase(true, null, "my challenge rating", "my alignment")]
        [TestCase(true, "my type", null, null)]
        [TestCase(true, "my type", null, "my alignment")]
        [TestCase(true, "my type", "my challenge rating", null)]
        [TestCase(true, "my type", "my challenge rating", "my alignment")]
        [TestCase(false, null, null, null)]
        [TestCase(false, null, null, "my alignment")]
        [TestCase(false, null, "my challenge rating", null)]
        [TestCase(false, null, "my challenge rating", "my alignment")]
        [TestCase(false, "my type", null, null)]
        [TestCase(false, "my type", null, "my alignment")]
        [TestCase(false, "my type", "my challenge rating", null)]
        [TestCase(false, "my type", "my challenge rating", "my alignment")]
        public void GenerateRandomName_GenerateRandomCreatureName_WithPresetNoneTemplate(bool asCharacter, string type, string cr, string alignment)
        {
            var creatureName = "my creature";
            var template = CreatureConstants.Templates.None;
            var filters = new Filters
            {
                Types = [type],
                ChallengeRatings = [cr],
                Alignments = [alignment]
            };

            SetupAllCreatureGroup(asCharacter, ["wrong creature", creatureName, "other creature"]);
            SetupAllTemplateGroup(["wrong template", template, "other template"]);
            SetupCreatureValidity(creatureName, asCharacter, filters, null, template);
            SetupIndividualTemplateValidity("other template", asCharacter, creatureName, null, filters, []);
            SetupIndividualTemplateValidity("wrong template", asCharacter, creatureName, null, filters, []);

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<CreaturePrototype>>()))
                .Returns((IEnumerable<CreaturePrototype> cc) => cc.Single(c => c.Name == creatureName));

            var name = creatureGenerator.GenerateRandomName(asCharacter, filters, template);
            Assert.That(name.Creature, Is.EqualTo(creatureName));
            Assert.That(name.Templates.Single(), Is.EqualTo(CreatureConstants.Templates.None));
        }

        [TestCase(true, null, null, null)]
        [TestCase(true, null, null, "my alignment")]
        [TestCase(true, null, "my challenge rating", null)]
        [TestCase(true, null, "my challenge rating", "my alignment")]
        [TestCase(true, "my type", null, null)]
        [TestCase(true, "my type", null, "my alignment")]
        [TestCase(true, "my type", "my challenge rating", null)]
        [TestCase(true, "my type", "my challenge rating", "my alignment")]
        [TestCase(false, null, null, null)]
        [TestCase(false, null, null, "my alignment")]
        [TestCase(false, null, "my challenge rating", null)]
        [TestCase(false, null, "my challenge rating", "my alignment")]
        [TestCase(false, "my type", null, null)]
        [TestCase(false, "my type", null, "my alignment")]
        [TestCase(false, "my type", "my challenge rating", null)]
        [TestCase(false, "my type", "my challenge rating", "my alignment")]
        public void GenerateRandomName_GenerateRandomCreatureName_WithPresetTemplate(bool asCharacter, string type, string cr, string alignment)
        {
            var creatureName = "my creature";
            var template = "my template";
            var filters = new Filters
            {
                Types = [type],
                ChallengeRatings = [cr],
                Alignments = [alignment]
            };

            SetupAllCreatureGroup(asCharacter, ["wrong creature", creatureName, "other creature"]);
            SetupAllTemplateGroup(["wrong template", template, "other template"]);
            SetupCreatureValidity(creatureName, asCharacter, filters, null, template);
            SetupIndividualTemplateValidity("other template", asCharacter, creatureName, null, filters, []);
            SetupIndividualTemplateValidity("wrong template", asCharacter, creatureName, null, filters, []);

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<CreaturePrototype>>()))
                .Returns((IEnumerable<CreaturePrototype> cc) => cc.Single(c => c.Name == creatureName));

            var name = creatureGenerator.GenerateRandomName(asCharacter, filters, template);
            Assert.That(name.Creature, Is.EqualTo(creatureName));
            Assert.That(name.Templates.Single(), Is.EqualTo(template));
        }

        [TestCase(true, null, null, null)]
        [TestCase(true, null, null, "my alignment")]
        [TestCase(true, null, "my challenge rating", null)]
        [TestCase(true, null, "my challenge rating", "my alignment")]
        [TestCase(true, "my type", null, null)]
        [TestCase(true, "my type", null, "my alignment")]
        [TestCase(true, "my type", "my challenge rating", null)]
        [TestCase(true, "my type", "my challenge rating", "my alignment")]
        [TestCase(false, null, null, null)]
        [TestCase(false, null, null, "my alignment")]
        [TestCase(false, null, "my challenge rating", null)]
        [TestCase(false, null, "my challenge rating", "my alignment")]
        [TestCase(false, "my type", null, null)]
        [TestCase(false, "my type", null, "my alignment")]
        [TestCase(false, "my type", "my challenge rating", null)]
        [TestCase(false, "my type", "my challenge rating", "my alignment")]
        public void GenerateRandomName_GenerateRandomCreatureName_WithMultiplePresetTemplates(bool asCharacter, string type, string cr, string alignment)
        {
            var creatureName = "my creature";
            var template1 = "my template";
            var template2 = "my other template";
            var filters = new Filters
            {
                Types = [type],
                ChallengeRatings = [cr],
                Alignments = [alignment]
            };
            var templates = new[] { template1, template2 };

            SetupAllCreatureGroup(asCharacter, ["wrong creature", creatureName, "other creature"]);
            SetupAllTemplateGroup(["wrong template", template1, "other template", template2, "another template"]);
            SetupCreatureValidity(creatureName, asCharacter, filters, null, templates);
            SetupIndividualTemplateValidity("other template", asCharacter, creatureName, null, filters, []);
            SetupIndividualTemplateValidity("wrong template", asCharacter, creatureName, null, filters, []);

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<CreaturePrototype>>()))
                .Returns((IEnumerable<CreaturePrototype> cc) => cc.Single(c => c.Name == creatureName));

            var name = creatureGenerator.GenerateRandomName(asCharacter, filters, templates);
            Assert.That(name.Creature, Is.EqualTo(creatureName));
            Assert.That(name.Templates, Is.EqualTo([template1, template2]));
        }

        [TestCase(true, null, null, null)]
        [TestCase(true, null, null, "my alignment")]
        [TestCase(true, null, "my challenge rating", null)]
        [TestCase(true, null, "my challenge rating", "my alignment")]
        [TestCase(true, "my type", null, null)]
        [TestCase(true, "my type", null, "my alignment")]
        [TestCase(true, "my type", "my challenge rating", null)]
        [TestCase(true, "my type", "my challenge rating", "my alignment")]
        [TestCase(false, null, null, null)]
        [TestCase(false, null, null, "my alignment")]
        [TestCase(false, null, "my challenge rating", null)]
        [TestCase(false, null, "my challenge rating", "my alignment")]
        [TestCase(false, "my type", null, null)]
        [TestCase(false, "my type", null, "my alignment")]
        [TestCase(false, "my type", "my challenge rating", null)]
        [TestCase(false, "my type", "my challenge rating", "my alignment")]
        public void GenerateRandomName_GenerateRandomCreatureName_WithRandomTemplate(bool asCharacter, string type, string cr, string alignment)
        {
            var creatureName = "my creature";
            var template = "my template";
            var filters = new Filters
            {
                Types = [type],
                ChallengeRatings = [cr],
                Alignments = [alignment]
            };

            var creatures = new[] { "wrong creature", "other wrong creature", creatureName, "other creature" };
            SetupFilterValidity(asCharacter, filters, null);
            SetupAllCreatureGroup(asCharacter, ["wrong creature", "other wrong creature", creatureName, "other creature"]);
            SetupAllTemplateGroup(["wrong template", template, "other template"]);
            SetupDefaultTemplateValidity(asCharacter, creatureName, null, filters, [.. creatures.Except(["wrong creature"])]);
            SetupIndividualTemplateValidity(template, asCharacter, creatureName, null, filters, [.. creatures.Except(["other wrong creature"])]);
            SetupIndividualTemplateValidity("other template", asCharacter, creatureName, null, filters, creatures);
            SetupIndividualTemplateValidity("wrong template", asCharacter, creatureName, null, filters, []);

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(It.Is<IEnumerable<string>>(cc => cc.IsEquivalentTo(
                    "other creature",
                    creatureName,
                    "other wrong creature",
                    template,
                    "other template"))))
                .Returns(template);
            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(It.Is<IEnumerable<string>>(cc => cc.IsEquivalentTo(
                    "other creature",
                    creatureName,
                    "wrong creature"))))
                .Returns(creatureName);

            var name = creatureGenerator.GenerateRandomName(asCharacter, filters);
            Assert.That(name.Creature, Is.EqualTo(creatureName));
            Assert.That(name.Templates.Single(), Is.EqualTo(template));
        }

        [TestCase(true, null, null, null, null)]
        [TestCase(true, null, null, null, "my alignment")]
        [TestCase(true, null, null, "challenge rating", null)]
        [TestCase(true, null, null, "challenge rating", "my alignment")]
        [TestCase(true, null, "type", null, null)]
        [TestCase(true, null, "type", null, "my alignment")]
        [TestCase(true, null, "type", "challenge rating", null)]
        [TestCase(true, null, "type", "challenge rating", "my alignment")]
        [TestCase(true, CreatureConstants.Templates.None, null, null, null)]
        [TestCase(true, CreatureConstants.Templates.None, null, null, "my alignment")]
        [TestCase(true, CreatureConstants.Templates.None, null, "challenge rating", null)]
        [TestCase(true, CreatureConstants.Templates.None, null, "challenge rating", "my alignment")]
        [TestCase(true, CreatureConstants.Templates.None, "type", null, null)]
        [TestCase(true, CreatureConstants.Templates.None, "type", null, "my alignment")]
        [TestCase(true, CreatureConstants.Templates.None, "type", "challenge rating", null)]
        [TestCase(true, CreatureConstants.Templates.None, "type", "challenge rating", "my alignment")]
        [TestCase(true, "template", null, null, null)]
        [TestCase(true, "template", null, null, "my alignment")]
        [TestCase(true, "template", null, "challenge rating", null)]
        [TestCase(true, "template", null, "challenge rating", "my alignment")]
        [TestCase(true, "template", "type", null, null)]
        [TestCase(true, "template", "type", null, "my alignment")]
        [TestCase(true, "template", "type", "challenge rating", null)]
        [TestCase(true, "template", "type", "challenge rating", "my alignment")]
        [TestCase(false, null, null, null, null)]
        [TestCase(false, null, null, null, "my alignment")]
        [TestCase(false, null, null, "challenge rating", null)]
        [TestCase(false, null, null, "challenge rating", "my alignment")]
        [TestCase(false, null, "type", null, null)]
        [TestCase(false, null, "type", null, "my alignment")]
        [TestCase(false, null, "type", "challenge rating", null)]
        [TestCase(false, null, "type", "challenge rating", "my alignment")]
        [TestCase(false, CreatureConstants.Templates.None, null, null, null)]
        [TestCase(false, CreatureConstants.Templates.None, null, null, "my alignment")]
        [TestCase(false, CreatureConstants.Templates.None, null, "challenge rating", null)]
        [TestCase(false, CreatureConstants.Templates.None, null, "challenge rating", "my alignment")]
        [TestCase(false, CreatureConstants.Templates.None, "type", null, null)]
        [TestCase(false, CreatureConstants.Templates.None, "type", null, "my alignment")]
        [TestCase(false, CreatureConstants.Templates.None, "type", "challenge rating", null)]
        [TestCase(false, CreatureConstants.Templates.None, "type", "challenge rating", "my alignment")]
        [TestCase(false, "template", null, null, null)]
        [TestCase(false, "template", null, null, "my alignment")]
        [TestCase(false, "template", null, "challenge rating", null)]
        [TestCase(false, "template", null, "challenge rating", "my alignment")]
        [TestCase(false, "template", "type", null, null)]
        [TestCase(false, "template", "type", null, "my alignment")]
        [TestCase(false, "template", "type", "challenge rating", null)]
        [TestCase(false, "template", "type", "challenge rating", "my alignment")]
        public void GenerateRandomName_ThrowException_WhenNotCompatible(bool asCharacter, string template, string type, string challengeRating, string alignment)
        {
            var filters = new Filters
            {
                Types = [type],
                ChallengeRatings = [challengeRating],
                Alignments = [alignment]
            };

            mockCreatureVerifier.Setup(v => v.VerifyCompatibility(asCharacter, null, null, filters, template)).Returns(false);

            var expected = new InvalidCreatureException(null, asCharacter, null, AbilityConstants.RandomizerRolls.Default, filters, template == null ? [] : [template]);

            var function = () => creatureGenerator.GenerateRandomName(asCharacter, filters, template);
            Assert.That(function, Throws.InstanceOf<InvalidCreatureException>().With.Message.EqualTo(expected.Message));
        }
    }
}