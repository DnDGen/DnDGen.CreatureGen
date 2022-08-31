using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Generators.Creatures;
using DnDGen.CreatureGen.Tables;
using DnDGen.CreatureGen.Templates;
using DnDGen.CreatureGen.Tests.Unit.TestCaseSources;
using DnDGen.CreatureGen.Verifiers.Exceptions;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DnDGen.CreatureGen.Tests.Unit.Generators.Creatures
{
    [TestFixture]
    internal class CreatureGeneratorGenerateRandomNameTests : CreatureGeneratorTests
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
            var filters = new Filters();
            filters.Type = type;
            filters.ChallengeRating = cr;
            filters.Alignment = alignment;

            var creatures = new[] { "wrong creature", creatureName, "other creature" };
            var templates = new[] { "wrong template", template, "other template" };
            mockCreatureVerifier.Setup(v => v.VerifyCompatibility(asCharacter, null, filters)).Returns(true);

            var group = asCharacter ? GroupConstants.Characters : GroupConstants.All;
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, group))
                .Returns(creatures);
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Templates))
                .Returns(templates);

            var mockNoneApplicator = new Mock<TemplateApplicator>();
            mockNoneApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), asCharacter, filters))
                .Returns((IEnumerable<string> cc, bool asC, Filters f) => cc.Intersect(new[] { creatureName }));

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>(CreatureConstants.Templates.None)).Returns(mockNoneApplicator.Object);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), asCharacter, filters))
                .Returns((IEnumerable<string> cc, bool asC, Filters f) => cc.Intersect(new[] { creatureName }));

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>(template)).Returns(mockTemplateApplicator.Object);

            var mockOtherTemplateApplicator = new Mock<TemplateApplicator>();
            mockOtherTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), asCharacter, filters))
                .Returns((IEnumerable<string> cc, bool asC, Filters f) => cc.Intersect(new[] { creatureName }));

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("other template")).Returns(mockOtherTemplateApplicator.Object);

            var mockWrongTemplateApplicator = new Mock<TemplateApplicator>();
            mockWrongTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), asCharacter, filters))
                .Returns(Enumerable.Empty<string>());

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("wrong template")).Returns(mockWrongTemplateApplicator.Object);

            var name = creatureGenerator.GenerateRandomName(asCharacter, filters);
            Assert.That(name.Creature, Is.EqualTo(creatureName));
            Assert.That(name.Templates, Is.EqualTo(new[] { CreatureConstants.Templates.None }));
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
            var filters = new Filters();
            filters.Templates.Add(template);
            filters.Type = type;
            filters.ChallengeRating = cr;
            filters.Alignment = alignment;

            var creatures = new[] { "wrong creature", creatureName, "other creature" };
            mockCreatureVerifier.Setup(v => v.VerifyCompatibility(asCharacter, null, filters)).Returns(true);

            var group = asCharacter ? GroupConstants.Characters : GroupConstants.All;
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, group))
                .Returns(creatures);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatiblePrototypes(It.IsAny<IEnumerable<string>>(), asCharacter, filters))
                .Returns((IEnumerable<string> cc, bool asC, Filters f) => cc
                    .Intersect(new[] { creatureName })
                    .Select(c => new CreaturePrototype { Name = c }));

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>(template)).Returns(mockTemplateApplicator.Object);

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
        public void GenerateRandomName_GenerateCreatureName_WithPresetTemplate(bool asCharacter, string type, string cr, string alignment)
        {
            var creatureName = "my creature";
            var template = "my template";
            var filters = new Filters();
            filters.Templates.Add(template);
            filters.Type = type;
            filters.ChallengeRating = cr;
            filters.Alignment = alignment;

            var creatures = new[] { "wrong creature", creatureName, "other creature" };
            mockCreatureVerifier.Setup(v => v.VerifyCompatibility(asCharacter, null, filters)).Returns(true);

            var group = asCharacter ? GroupConstants.Characters : GroupConstants.All;
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, group))
                .Returns(creatures);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatiblePrototypes(It.IsAny<IEnumerable<string>>(), asCharacter, filters))
                .Returns((IEnumerable<string> cc, bool asC, Filters f) => cc
                    .Intersect(new[] { creatureName })
                    .Select(c => new CreaturePrototype { Name = c }));

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>(template)).Returns(mockTemplateApplicator.Object);

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
        public void GenerateRandomName_GenerateCreatureName_WithMultiplePresetTemplates(bool asCharacter, string type, string cr, string alignment)
        {
            var creatureName = "my creature";
            var template1 = "my template";
            var template2 = "my other template";
            var filters = new Filters();
            filters.Templates.Add(template1);
            filters.Templates.Add(template2);
            filters.Type = type;
            filters.ChallengeRating = cr;
            filters.Alignment = alignment;

            var creatures = new[] { "wrong creature", creatureName, "other creature" };
            mockCreatureVerifier.Setup(v => v.VerifyCompatibility(asCharacter, null, filters)).Returns(true);

            var group = asCharacter ? GroupConstants.Characters : GroupConstants.All;
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, group))
                .Returns(creatures);

            var mockTemplateApplicator1 = new Mock<TemplateApplicator>();
            mockTemplateApplicator1
                .Setup(a => a.GetCompatiblePrototypes(It.IsAny<IEnumerable<string>>(), asCharacter, null))
                .Returns((IEnumerable<string> cc, bool asC, Filters f) => cc.Select(c => new CreaturePrototype { Name = c }));

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>(template1)).Returns(mockTemplateApplicator1.Object);

            var mockTemplateApplicator2 = new Mock<TemplateApplicator>();
            mockTemplateApplicator2
                .Setup(a => a.GetCompatiblePrototypes(It.IsAny<IEnumerable<CreaturePrototype>>(), filters))
                .Returns((IEnumerable<CreaturePrototype> cc, Filters f) => cc.Where(c => c.Name == creatureName));

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>(template2)).Returns(mockTemplateApplicator2.Object);

            var name = creatureGenerator.GenerateRandomName(asCharacter, filters);
            Assert.That(name.Creature, Is.EqualTo(creatureName));
            Assert.That(name.Templates, Is.EqualTo(new[] { template1, template2 }));
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
            var filters = new Filters();
            filters.Type = type;
            filters.ChallengeRating = cr;
            filters.Alignment = alignment;

            var creatures = new[] { "wrong creature", creatureName, "other creature" };
            var templates = new[] { "wrong template", template, "other template" };
            mockCreatureVerifier.Setup(v => v.VerifyCompatibility(asCharacter, null, filters)).Returns(true);

            var group = asCharacter ? GroupConstants.Characters : GroupConstants.All;
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, group))
                .Returns(creatures);
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Templates))
                .Returns(templates);

            var mockNoneApplicator = new Mock<TemplateApplicator>();
            mockNoneApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), asCharacter, filters))
                .Returns((IEnumerable<string> cc, bool asC, Filters f) => Enumerable.Empty<string>());

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>(CreatureConstants.Templates.None)).Returns(mockNoneApplicator.Object);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), asCharacter, filters))
                .Returns((IEnumerable<string> cc, bool asC, Filters f) => cc.Intersect(new[] { creatureName }));

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>(template)).Returns(mockTemplateApplicator.Object);

            var mockWrongTemplateApplicator1 = new Mock<TemplateApplicator>();
            mockWrongTemplateApplicator1
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), asCharacter, filters))
                .Returns(Enumerable.Empty<string>());

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("other template")).Returns(mockWrongTemplateApplicator1.Object);

            var mockWrongTemplateApplicator2 = new Mock<TemplateApplicator>();
            mockWrongTemplateApplicator2
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), asCharacter, filters))
                .Returns(Enumerable.Empty<string>());

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("wrong template")).Returns(mockWrongTemplateApplicator2.Object);

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
            var filters = new Filters();
            filters.Type = type;
            filters.ChallengeRating = cr;
            filters.Alignment = alignment;

            var creatures = new[] { "wrong creature name", creatureName, "other creature" };
            var templates = new[] { "wrong template", template, "other template" };
            mockCreatureVerifier.Setup(v => v.VerifyCompatibility(asCharacter, null, filters)).Returns(true);

            var group = asCharacter ? GroupConstants.Characters : GroupConstants.All;
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, group))
                .Returns(creatures);
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Templates))
                .Returns(templates);

            var mockNoneApplicator = new Mock<TemplateApplicator>();
            mockNoneApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), asCharacter, filters))
                .Returns((IEnumerable<string> cc, bool asC, Filters f) => Enumerable.Empty<string>());

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>(CreatureConstants.Templates.None)).Returns(mockNoneApplicator.Object);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), asCharacter, filters))
                .Returns((IEnumerable<string> cc, bool asC, Filters f) => cc.Intersect(new[] { creatureName }));

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>(template)).Returns(mockTemplateApplicator.Object);

            var mockWrongTemplateApplicator1 = new Mock<TemplateApplicator>();
            mockWrongTemplateApplicator1
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), asCharacter, filters))
                .Returns((IEnumerable<string> cc, bool asC, Filters f) => cc.Intersect(new[] { creatureName }));

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("other template")).Returns(mockWrongTemplateApplicator1.Object);

            var mockWrongTemplateApplicator2 = new Mock<TemplateApplicator>();
            mockWrongTemplateApplicator2
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), asCharacter, filters))
                .Returns(Enumerable.Empty<string>());

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("wrong template")).Returns(mockWrongTemplateApplicator2.Object);

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(It.Is<IEnumerable<string>>(cc => cc.IsEquivalentTo(new[] { template, "other template" }))))
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
            var filters = new Filters();
            filters.Type = type;
            filters.ChallengeRating = cr;
            filters.Alignment = alignment;

            var creatures = new[] { "wrong creature", creatureName, "other creature" };
            var templates = new[] { "wrong template", template, "other template" };
            mockCreatureVerifier.Setup(v => v.VerifyCompatibility(asCharacter, null, filters)).Returns(true);

            var group = asCharacter ? GroupConstants.Characters : GroupConstants.All;
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, group))
                .Returns(creatures);
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Templates))
                .Returns(templates);

            var mockNoneApplicator = new Mock<TemplateApplicator>();
            mockNoneApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), asCharacter, filters))
                .Returns((IEnumerable<string> cc, bool asC, Filters f) => cc.Except(new[] { "wrong creature" }));

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>(CreatureConstants.Templates.None)).Returns(mockNoneApplicator.Object);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), asCharacter, filters))
                .Returns((IEnumerable<string> cc, bool asC, Filters f) => cc);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>(template)).Returns(mockTemplateApplicator.Object);

            var mockOtherTemplateApplicator = new Mock<TemplateApplicator>();
            mockOtherTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), asCharacter, filters))
                .Returns((IEnumerable<string> cc, bool asC, Filters f) => cc);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("other template")).Returns(mockOtherTemplateApplicator.Object);

            var mockWrongTemplateApplicator = new Mock<TemplateApplicator>();
            mockWrongTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), asCharacter, filters))
                .Returns(Enumerable.Empty<string>());

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("wrong template")).Returns(mockWrongTemplateApplicator.Object);

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(It.Is<IEnumerable<string>>(cc => cc.IsEquivalentTo(creatures.Union(new[] { template, "other template" })))))
                .Returns(creatureName);

            var name = creatureGenerator.GenerateRandomName(asCharacter, filters);
            Assert.That(name.Creature, Is.EqualTo(creatureName));
            Assert.That(name.Templates, Is.EqualTo(new[] { CreatureConstants.Templates.None }));
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
            var filters = new Filters();
            filters.Templates.Add(template);
            filters.Type = type;
            filters.ChallengeRating = cr;
            filters.Alignment = alignment;

            var creatures = new[] { "wrong creature", creatureName, "other creature" };
            var templates = new[] { "wrong template", template, "other template" };
            mockCreatureVerifier.Setup(v => v.VerifyCompatibility(asCharacter, null, filters)).Returns(true);

            var group = asCharacter ? GroupConstants.Characters : GroupConstants.All;
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, group))
                .Returns(creatures);
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Templates))
                .Returns(templates);

            var mockNoneApplicator = new Mock<TemplateApplicator>();
            mockNoneApplicator
                .Setup(a => a.GetCompatiblePrototypes(It.IsAny<IEnumerable<string>>(), asCharacter, filters))
                .Returns((IEnumerable<string> cc, bool asC, Filters f) => cc.Select(c => new CreaturePrototype { Name = c }));

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>(CreatureConstants.Templates.None)).Returns(mockNoneApplicator.Object);

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(It.Is<IEnumerable<string>>(cc => cc.IsEquivalentTo(creatures))))
                .Returns(creatureName);

            var name = creatureGenerator.GenerateRandomName(asCharacter, filters);
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
            var filters = new Filters();
            filters.Templates.Add(template);
            filters.Type = type;
            filters.ChallengeRating = cr;
            filters.Alignment = alignment;

            var creatures = new[] { "wrong creature", creatureName, "other creature" };
            var templates = new[] { "wrong template", template, "other template" };
            mockCreatureVerifier.Setup(v => v.VerifyCompatibility(asCharacter, null, filters)).Returns(true);

            var group = asCharacter ? GroupConstants.Characters : GroupConstants.All;
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, group))
                .Returns(creatures);
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Templates))
                .Returns(templates);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatiblePrototypes(It.IsAny<IEnumerable<string>>(), asCharacter, filters))
                .Returns((IEnumerable<string> cc, bool asC, Filters f) => cc.Select(c => new CreaturePrototype { Name = c }));

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>(template)).Returns(mockTemplateApplicator.Object);

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(It.Is<IEnumerable<string>>(cc => cc.IsEquivalentTo(creatures))))
                .Returns(creatureName);

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
        public void GenerateRandomName_GenerateRandomCreatureName_WithMultiplePresetTemplates(bool asCharacter, string type, string cr, string alignment)
        {
            var creatureName = "my creature";
            var template1 = "my template";
            var template2 = "my other template";
            var filters = new Filters();
            filters.Templates.Add(template1);
            filters.Templates.Add(template2);
            filters.Type = type;
            filters.ChallengeRating = cr;
            filters.Alignment = alignment;

            var creatures = new[] { "wrong creature", creatureName, "other creature" };
            var templates = new[] { "wrong template", template1, "other template", template2, "another template" };
            mockCreatureVerifier.Setup(v => v.VerifyCompatibility(asCharacter, null, filters)).Returns(true);

            var group = asCharacter ? GroupConstants.Characters : GroupConstants.All;
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, group))
                .Returns(creatures);
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Templates))
                .Returns(templates);

            var mockTemplateApplicator1 = new Mock<TemplateApplicator>();
            mockTemplateApplicator1
                .Setup(a => a.GetCompatiblePrototypes(It.IsAny<IEnumerable<string>>(), asCharacter, null))
                .Returns((IEnumerable<string> cc, bool asC, Filters f) => cc.Select(c => new CreaturePrototype { Name = c }));

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>(template1)).Returns(mockTemplateApplicator1.Object);

            var mockTemplateApplicator2 = new Mock<TemplateApplicator>();
            mockTemplateApplicator2
                .Setup(a => a.GetCompatiblePrototypes(It.IsAny<IEnumerable<CreaturePrototype>>(), filters))
                .Returns((IEnumerable<CreaturePrototype> cc, Filters f) => cc);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>(template2)).Returns(mockTemplateApplicator2.Object);

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(It.Is<IEnumerable<string>>(cc => cc.IsEquivalentTo(creatures))))
                .Returns(creatureName);

            var name = creatureGenerator.GenerateRandomName(asCharacter, filters);
            Assert.That(name.Creature, Is.EqualTo(creatureName));
            Assert.That(name.Templates, Is.EqualTo(new[] { template1, template2 }));
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
            var filters = new Filters();
            filters.Type = type;
            filters.ChallengeRating = cr;
            filters.Alignment = alignment;

            var creatures = new[] { "wrong creature", "other wrong creature", creatureName, "other creature" };
            var templates = new[] { "wrong template", template, "other template" };
            mockCreatureVerifier.Setup(v => v.VerifyCompatibility(asCharacter, null, filters)).Returns(true);

            var group = asCharacter ? GroupConstants.Characters : GroupConstants.All;
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, group))
                .Returns(creatures);
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, GroupConstants.Templates))
                .Returns(templates);

            var mockNoneApplicator = new Mock<TemplateApplicator>();
            mockNoneApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), asCharacter, filters))
                .Returns((IEnumerable<string> cc, bool asC, Filters f) => cc.Except(new[] { "wrong creature" }));

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>(CreatureConstants.Templates.None)).Returns(mockNoneApplicator.Object);

            var mockTemplateApplicator = new Mock<TemplateApplicator>();
            mockTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), asCharacter, filters))
                .Returns((IEnumerable<string> cc, bool asC, Filters f) => cc.Except(new[] { "other wrong creature" }));

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>(template)).Returns(mockTemplateApplicator.Object);

            var mockOtherTemplateApplicator = new Mock<TemplateApplicator>();
            mockOtherTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), asCharacter, filters))
                .Returns((IEnumerable<string> cc, bool asC, Filters f) => cc);

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("other template")).Returns(mockOtherTemplateApplicator.Object);

            var mockWrongTemplateApplicator = new Mock<TemplateApplicator>();
            mockWrongTemplateApplicator
                .Setup(a => a.GetCompatibleCreatures(It.IsAny<IEnumerable<string>>(), asCharacter, filters))
                .Returns(Enumerable.Empty<string>());

            mockJustInTimeFactory.Setup(f => f.Build<TemplateApplicator>("wrong template")).Returns(mockWrongTemplateApplicator.Object);

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(It.Is<IEnumerable<string>>(cc => cc.IsEquivalentTo(new[]
                {
                    "other creature",
                    creatureName,
                    "other wrong creature",
                    template,
                    "other template"
                }))))
                .Returns(template);
            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(It.Is<IEnumerable<string>>(cc => cc.IsEquivalentTo(new[]
                {
                    "other creature",
                    creatureName,
                    "wrong creature"
                }))))
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
            var filters = new Filters();
            filters.Templates.Add(template);
            filters.Type = type;
            filters.ChallengeRating = challengeRating;
            filters.Alignment = alignment;

            mockCreatureVerifier.Setup(v => v.VerifyCompatibility(asCharacter, null, filters)).Returns(false);

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

            Assert.That(() => creatureGenerator.GenerateRandomName(asCharacter, filters),
                Throws.InstanceOf<InvalidCreatureException>().With.Message.EqualTo(message.ToString()));
        }
    }
}