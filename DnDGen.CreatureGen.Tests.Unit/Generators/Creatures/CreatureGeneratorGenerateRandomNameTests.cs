using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Generators.Abilities;
using DnDGen.CreatureGen.Generators.Creatures;
using DnDGen.CreatureGen.Tables;
using DnDGen.CreatureGen.Tests.Unit.TestCaseSources;
using DnDGen.CreatureGen.Verifiers.Exceptions;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

            var creatures = new[] { "wrong creature", creatureName, "other creature" };
            var templates = new[] { "wrong template", template, "other template" };
            mockCreatureVerifier.Setup(v => v.VerifyCompatibility(asCharacter, null, null, filters)).Returns(true);

            var group = asCharacter ? GroupConstants.Characters : GroupConstants.All;
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, group))
                .Returns(creatures);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.TemplateGroups, GroupConstants.All))
                .Returns(templates);

            mockCreatureVerifier
                .Setup(v => v.GetCompatibleCreaturesForTemplate(It.IsAny<IEnumerable<string>>(), CreatureConstants.Templates.None, asCharacter, null, filters))
                .Returns((IEnumerable<string> cc, string t, bool asC, AbilityRandomizer r, Filters f) => cc.Intersect([creatureName]));
            mockCreatureVerifier
                .Setup(v => v.GetCompatibleCreaturesForTemplate(It.IsAny<IEnumerable<string>>(), template, asCharacter, null, filters))
                .Returns((IEnumerable<string> cc, string t, bool asC, AbilityRandomizer r, Filters f) => cc.Intersect([creatureName]));
            mockCreatureVerifier
                .Setup(v => v.GetCompatibleCreaturesForTemplate(It.IsAny<IEnumerable<string>>(), "other template", asCharacter, null, filters))
                .Returns((IEnumerable<string> cc, string t, bool asC, AbilityRandomizer r, Filters f) => cc.Intersect([creatureName]));
            mockCreatureVerifier
                .Setup(v => v.GetCompatibleCreaturesForTemplate(It.IsAny<IEnumerable<string>>(), "wrong template", asCharacter, null, filters))
                .Returns([]);

            SetupCreatureValidity(creatureName, asCharacter, filters);

            var name = creatureGenerator.GenerateRandomName(asCharacter, filters);
            Assert.That(name.Creature, Is.EqualTo(creatureName));
            Assert.That(name.Templates, Is.EqualTo([CreatureConstants.Templates.None]));
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

            var creatures = new[] { "wrong creature", creatureName, "other creature" };
            var templates = new[] { "wrong template", template, "other template" };
            mockCreatureVerifier.Setup(v => v.VerifyCompatibility(false, null, null, filters)).Returns(true);

            var group = GroupConstants.All;
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, group))
                .Returns(creatures);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.TemplateGroups, GroupConstants.All))
                .Returns(templates);

            mockCreatureVerifier
                .Setup(v => v.GetCompatibleCreaturesForTemplate(
                    It.IsAny<IEnumerable<string>>(),
                    It.Is<string>(t => t == null || t == CreatureConstants.Templates.None),
                    false,
                    null,
                    filters))
                .Returns((IEnumerable<string> cc, string t, bool asC, AbilityRandomizer r, Filters f) => cc.Intersect([creatureName]));
            mockCreatureVerifier
                .Setup(v => v.GetCompatibleCreaturesForTemplate(It.IsAny<IEnumerable<string>>(), template, false, null, filters))
                .Returns((IEnumerable<string> cc, string t, bool asC, AbilityRandomizer r, Filters f) => cc.Intersect([creatureName]));
            mockCreatureVerifier
                .Setup(v => v.GetCompatibleCreaturesForTemplate(It.IsAny<IEnumerable<string>>(), "other template", false, null, filters))
                .Returns((IEnumerable<string> cc, string t, bool asC, AbilityRandomizer r, Filters f) => cc.Intersect([creatureName]));
            mockCreatureVerifier
                .Setup(v => v.GetCompatibleCreaturesForTemplate(It.IsAny<IEnumerable<string>>(), "wrong template", false, null, filters))
                .Returns([]);

            var name = creatureGenerator.GenerateRandomName(false, filters, empty);
            Assert.That(name.Creature, Is.EqualTo(creatureName));
            Assert.That(name.Templates, Is.EqualTo([CreatureConstants.Templates.None]));
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

            var creatures = new[] { "wrong creature", creatureName, "other creature" };
            mockCreatureVerifier.Setup(v => v.VerifyCompatibility(asCharacter, null, null, filters)).Returns(true);

            var group = asCharacter ? GroupConstants.Characters : GroupConstants.All;
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, group))
                .Returns(creatures);

            mockCreatureVerifier
                .Setup(v => v.GetChainedTemplates(It.IsAny<IEnumerable<string>>(), It.Is<string[]>(t => t.IsEquivalentTo(template)), asCharacter, null, filters))
                .Returns((IEnumerable<string> cc, List<string> tt, bool asC, AbilityRandomizer r, Filters f) => cc
                    .Intersect([creatureName])
                    .Select(c => new CreaturePrototype { Name = c, Templates = [template] }));

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

            var creatures = new[] { "wrong creature", creatureName, "other creature" };
            mockCreatureVerifier.Setup(v => v.VerifyCompatibility(asCharacter, null, null, filters)).Returns(true);

            var group = asCharacter ? GroupConstants.Characters : GroupConstants.All;
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, group))
                .Returns(creatures);

            mockCreatureVerifier
                .Setup(v => v.GetChainedTemplates(It.IsAny<IEnumerable<string>>(), It.Is<string[]>(t => t.IsEquivalentTo(template)), asCharacter, null, filters))
                .Returns((IEnumerable<string> cc, List<string> tt, bool asC, AbilityRandomizer r, Filters f) => cc
                    .Intersect([creatureName])
                    .Select(c => new CreaturePrototype { Name = c, Templates = [template] }));

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
            var creatures = new[] { "wrong creature", creatureName, "other creature" };
            mockCreatureVerifier.Setup(v => v.VerifyCompatibility(asCharacter, null, null, filters, templates)).Returns(true);

            var group = asCharacter ? GroupConstants.Characters : GroupConstants.All;
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, group))
                .Returns(creatures);

            mockCreatureVerifier
                .Setup(v => v.GetChainedTemplates(It.IsAny<IEnumerable<string>>(), templates, asCharacter, null, filters))
                .Returns((IEnumerable<string> cc, List<string> tt, bool asC, AbilityRandomizer r, Filters f) => cc
                    .Intersect([creatureName])
                    .Select(c => new CreaturePrototype { Name = c, Templates = [.. templates] }));

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

            var creatures = new[] { "wrong creature", creatureName, "other creature" };
            var templates = new[] { "wrong template", template, "other template" };
            mockCreatureVerifier.Setup(v => v.VerifyCompatibility(asCharacter, null, null, filters)).Returns(true);

            var group = asCharacter ? GroupConstants.Characters : GroupConstants.All;
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, group))
                .Returns(creatures);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.TemplateGroups, GroupConstants.All))
                .Returns(templates);

            mockCreatureVerifier
                .Setup(v => v.GetCompatibleCreaturesForTemplate(It.IsAny<IEnumerable<string>>(), CreatureConstants.Templates.None, asCharacter, null, filters))
                .Returns([]);
            mockCreatureVerifier
                .Setup(v => v.GetCompatibleCreaturesForTemplate(It.IsAny<IEnumerable<string>>(), template, asCharacter, null, filters))
                .Returns((IEnumerable<string> cc, string t, bool asC, AbilityRandomizer r, Filters f) => cc.Intersect([creatureName]));
            mockCreatureVerifier
                .Setup(v => v.GetCompatibleCreaturesForTemplate(It.IsAny<IEnumerable<string>>(), "other template", asCharacter, null, filters))
                .Returns([]);
            mockCreatureVerifier
                .Setup(v => v.GetCompatibleCreaturesForTemplate(It.IsAny<IEnumerable<string>>(), "wrong template", asCharacter, null, filters))
                .Returns([]);

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

            var creatures = new[] { "wrong creature name", creatureName, "other creature" };
            var templates = new[] { "wrong template", template, "other template" };
            mockCreatureVerifier.Setup(v => v.VerifyCompatibility(asCharacter, null, null, filters)).Returns(true);

            var group = asCharacter ? GroupConstants.Characters : GroupConstants.All;
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, group))
                .Returns(creatures);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.TemplateGroups, GroupConstants.All))
                .Returns(templates);

            mockCreatureVerifier
                .Setup(v => v.GetCompatibleCreaturesForTemplate(It.IsAny<IEnumerable<string>>(), CreatureConstants.Templates.None, asCharacter, null, filters))
                .Returns([]);
            mockCreatureVerifier
                .Setup(v => v.GetCompatibleCreaturesForTemplate(It.IsAny<IEnumerable<string>>(), template, asCharacter, null, filters))
                .Returns((IEnumerable<string> cc, string t, bool asC, AbilityRandomizer r, Filters f) => cc.Intersect([creatureName]));
            mockCreatureVerifier
                .Setup(v => v.GetCompatibleCreaturesForTemplate(It.IsAny<IEnumerable<string>>(), "other template", asCharacter, null, filters))
                .Returns((IEnumerable<string> cc, string t, bool asC, AbilityRandomizer r, Filters f) => cc.Intersect([creatureName]));
            mockCreatureVerifier
                .Setup(v => v.GetCompatibleCreaturesForTemplate(It.IsAny<IEnumerable<string>>(), "wrong template", asCharacter, null, filters))
                .Returns([]);

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
            mockCreatureVerifier.Setup(v => v.VerifyCompatibility(asCharacter, null, null, filters)).Returns(true);

            var group = asCharacter ? GroupConstants.Characters : GroupConstants.All;
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, group))
                .Returns(creatures);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.TemplateGroups, GroupConstants.All))
                .Returns(templates);

            mockCreatureVerifier
                .Setup(v => v.GetCompatibleCreaturesForTemplate(
                    It.IsAny<IEnumerable<string>>(),
                    It.Is<string>(t => t == null || t == CreatureConstants.Templates.None),
                    asCharacter,
                    null,
                    filters))
                .Returns((IEnumerable<string> cc, string t, bool asC, AbilityRandomizer r, Filters f) => cc.Except(["wrong creature"]));
            mockCreatureVerifier
                .Setup(v => v.GetCompatibleCreaturesForTemplate(It.IsAny<IEnumerable<string>>(), template, asCharacter, null, filters))
                .Returns((IEnumerable<string> cc, string t, bool asC, AbilityRandomizer r, Filters f) => cc);
            mockCreatureVerifier
                .Setup(v => v.GetCompatibleCreaturesForTemplate(It.IsAny<IEnumerable<string>>(), "other template", asCharacter, null, filters))
                .Returns((IEnumerable<string> cc, string t, bool asC, AbilityRandomizer r, Filters f) => cc);
            mockCreatureVerifier
                .Setup(v => v.GetCompatibleCreaturesForTemplate(It.IsAny<IEnumerable<string>>(), "wrong template", asCharacter, null, filters))
                .Returns([]);

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(It.Is<IEnumerable<string>>(cc => cc.IsEquivalentTo(creatures.Union(new[] { template, "other template" })))))
                .Returns(creatureName);

            var name = creatureGenerator.GenerateRandomName(asCharacter, filters);
            Assert.That(name.Creature, Is.EqualTo(creatureName));
            Assert.That(name.Templates, Is.EqualTo([CreatureConstants.Templates.None]));
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

            var creatures = new[] { "wrong creature", creatureName, "other creature" };
            var templates = new[] { "wrong template", template, "other template" };
            mockCreatureVerifier.Setup(v => v.VerifyCompatibility(asCharacter, null, null, filters, template)).Returns(true);

            var group = asCharacter ? GroupConstants.Characters : GroupConstants.All;
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, group))
                .Returns(creatures);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.TemplateGroups, GroupConstants.All))
                .Returns(templates);

            mockCreatureVerifier
                .Setup(v => v.GetChainedTemplates(It.IsAny<IEnumerable<string>>(), It.Is<string[]>(t => t.IsEquivalentTo(template)), asCharacter, null, filters))
                .Returns((IEnumerable<string> cc, List<string> tt, bool asC, AbilityRandomizer r, Filters f) => cc
                    .Select(c => new CreaturePrototype { Name = c, Templates = [template] }));

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

            var creatures = new[] { "wrong creature", creatureName, "other creature" };
            var templates = new[] { "wrong template", template, "other template" };
            mockCreatureVerifier.Setup(v => v.VerifyCompatibility(asCharacter, null, null, filters, template)).Returns(true);

            var group = asCharacter ? GroupConstants.Characters : GroupConstants.All;
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, group))
                .Returns(creatures);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.TemplateGroups, GroupConstants.All))
                .Returns(templates);

            mockCreatureVerifier
                .Setup(v => v.GetChainedTemplates(It.IsAny<IEnumerable<string>>(), It.Is<string[]>(t => t.IsEquivalentTo(template)), asCharacter, null, filters))
                .Returns((IEnumerable<string> cc, List<string> tt, bool asC, AbilityRandomizer r, Filters f) => cc
                    .Select(c => new CreaturePrototype { Name = c, Templates = [template] }));

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

            var creatures = new[] { "wrong creature", creatureName, "other creature" };
            var templates = new[] { template1, template2 };
            mockCreatureVerifier.Setup(v => v.VerifyCompatibility(asCharacter, null, null, filters, templates)).Returns(true);

            var group = asCharacter ? GroupConstants.Characters : GroupConstants.All;
            var allTemplates = new[] { "wrong template", template1, "other template", template2, "another template" };
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, group))
                .Returns(creatures);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.TemplateGroups, GroupConstants.All))
                .Returns(allTemplates);

            mockCreatureVerifier
                .Setup(v => v.GetChainedTemplates(It.IsAny<IEnumerable<string>>(), templates, asCharacter, null, filters))
                .Returns((IEnumerable<string> cc, List<string> tt, bool asC, AbilityRandomizer r, Filters f) => cc
                    .Select(c => new CreaturePrototype { Name = c, Templates = [.. templates] }));

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
            var templates = new[] { "wrong template", template, "other template" };
            mockCreatureVerifier.Setup(v => v.VerifyCompatibility(asCharacter, null, null, filters)).Returns(true);

            var group = asCharacter ? GroupConstants.Characters : GroupConstants.All;
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, group))
                .Returns(creatures);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.TemplateGroups, GroupConstants.All))
                .Returns(templates);

            mockCreatureVerifier
                .Setup(v => v.GetCompatibleCreaturesForTemplate(It.IsAny<IEnumerable<string>>(), CreatureConstants.Templates.None, asCharacter, null, filters))
                .Returns((IEnumerable<string> cc, string t, bool asC, AbilityRandomizer r, Filters f) => cc.Except(["wrong creature"]));
            mockCreatureVerifier
                .Setup(v => v.GetCompatibleCreaturesForTemplate(It.IsAny<IEnumerable<string>>(), template, asCharacter, null, filters))
                .Returns((IEnumerable<string> cc, string t, bool asC, AbilityRandomizer r, Filters f) => cc.Except(["other wrong creature"]));
            mockCreatureVerifier
                .Setup(v => v.GetCompatibleCreaturesForTemplate(It.IsAny<IEnumerable<string>>(), "other template", asCharacter, null, filters))
                .Returns((IEnumerable<string> cc, string t, bool asC, AbilityRandomizer r, Filters f) => cc);
            mockCreatureVerifier
                .Setup(v => v.GetCompatibleCreaturesForTemplate(It.IsAny<IEnumerable<string>>(), "wrong template", asCharacter, null, filters))
                .Returns([]);

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

            var message = new StringBuilder();
            message.AppendLine("Invalid creature:");
            message.AppendLine($"\tAs Character: {asCharacter}");

            if (template == CreatureConstants.Templates.None)
                message.AppendLine($"\tTemplate: None");
            else if (template != null)
                message.AppendLine($"\tTemplate: {template}");

            if (type != null)
                message.AppendLine($"\tType: {type}");

            if (challengeRating != null)
                message.AppendLine($"\tCR: {challengeRating}");

            if (alignment != null)
                message.AppendLine($"\tAlignment: {alignment}");

            message.AppendLine($"\tAbility Roll: {AbilityConstants.RandomizerRolls.Default}");

            var function = () => creatureGenerator.GenerateRandomName(asCharacter, filters, template);
            Assert.That(function, Throws.InstanceOf<InvalidCreatureException>().With.Message.EqualTo(message.ToString()));
        }
    }
}