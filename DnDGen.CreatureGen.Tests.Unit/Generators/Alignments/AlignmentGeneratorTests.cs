using DnDGen.CreatureGen.Alignments;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Generators.Alignments;
using DnDGen.CreatureGen.Generators.Creatures;
using DnDGen.CreatureGen.Tables;
using DnDGen.CreatureGen.Tests.Unit.TestCaseSources;
using DnDGen.CreatureGen.Verifiers;
using DnDGen.Infrastructure.Selectors.Collections;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Unit.Generators.Alignments
{
    [TestFixture]
    public class AlignmentGeneratorTests
    {
        private IAlignmentGenerator alignmentGenerator;
        private Mock<ICollectionSelector> mockCollectionSelector;
        private Mock<ICreatureVerifier> mockCreatureVerifier;
        private Mock<ICreaturePrototypeFactory> mockPrototypeFactory;
        private int randomIndex;

        [SetUp]
        public void Setup()
        {
            mockCollectionSelector = new Mock<ICollectionSelector>();
            mockCreatureVerifier = new Mock<ICreatureVerifier>();
            mockPrototypeFactory = new Mock<ICreaturePrototypeFactory>();
            alignmentGenerator = new AlignmentGenerator(mockCollectionSelector.Object, mockCreatureVerifier.Object, mockPrototypeFactory.Object);

            randomIndex = 0;
            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>()))
                .Returns((IEnumerable<string> c) => c.ElementAt(randomIndex));
        }

        [Test]
        public void Generate_WithAlignmentFilter()
        {
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups, "creature name"))
                .Returns(["wrong alignment", "lawfulness goodness"]);

            var filters = new Filters { Alignments = ["lawfulness goodness"] };
            var alignment = alignmentGenerator.Generate("creature name", null, filters);
            Assert.That(alignment.Full, Is.EqualTo("lawfulness goodness"));
        }

        [Test]
        public void Generate_WithMultipleAlignmentFilters()
        {
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups, "creature name"))
                .Returns(["wrong alignment", "lawfulness goodness", "other alignment"]);

            randomIndex = 1;

            var filters = new Filters { Alignments = ["lawfulness goodness", "other alignment"] };
            var alignment = alignmentGenerator.Generate("creature name", null, filters);
            Assert.That(alignment.Full, Is.EqualTo("other alignment"));
        }

        [Test]
        public void Generate_WithAlignmentFilterAndTemplate()
        {
            var creatureAlignments = new[] { "wrong alignment", "lawfulness goodness" };
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups, "creature name"))
                .Returns(creatureAlignments);

            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups, "my template" + GroupConstants.AllowedInput))
                .Returns(["template alignment", "lawfulness goodness", "other alignment"]);

            var filters = new Filters { Alignments = ["lawfulness goodness"] };
            SetupChainedTemplates(["my template"], creatureAlignments, ["lawfulness goodness"], filters);

            var alignment = alignmentGenerator.Generate("creature name", ["my template"], filters);
            Assert.That(alignment.Full, Is.EqualTo("lawfulness goodness"));
        }

        private void SetupChainedTemplates(string[] templates, string[] creatureAlignments, string[] goodAlignments, Filters filters)
        {
            var prototype = new CreaturePrototype { Name = "creature name", Alignments = [.. creatureAlignments.Select(a => new Alignment(a))] };
            mockPrototypeFactory
                .Setup(f => f.Build(It.Is<IEnumerable<string>>(c => c.IsEquivalentTo("creature name")), false, null))
                .Returns([prototype]);
            mockPrototypeFactory
                .Setup(f => f.Clone(prototype))
                .Returns(() => new CreaturePrototype { Name = "creature name", Alignments = [.. prototype.Alignments] });

            mockCreatureVerifier
                .Setup(v => v.GetChainedTemplates(
                    It.Is<IEnumerable<CreaturePrototype>>(p => p.Count() == 1 && p.Single().Name == "creature name"),
                    It.Is<string[]>(t => t.IsEquivalentTo(templates)),
                    filters))
                .Returns([]);

            foreach (var alignment in goodAlignments)
            {
                mockCreatureVerifier
                    .Setup(v => v.GetChainedTemplates(
                        It.Is<IEnumerable<CreaturePrototype>>(p => p.Count() == 1
                            && p.Single().Name == "creature name"
                            && p.Single().Alignments.IsEquivalentTo(new Alignment(alignment))),
                        It.Is<string[]>(t => t.IsEquivalentTo(templates)),
                        filters))
                    .Returns([prototype]);
            }
        }

        [Test]
        public void Generate_WithMultipleAlignmentFiltersAndTemplate()
        {
            var creatureAlignments = new[] { "wrong alignment", "lawfulness goodness", "other alignment" };
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups, "creature name"))
                .Returns(creatureAlignments);

            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups, "my template" + GroupConstants.AllowedInput))
                .Returns(["template alignment", "lawfulness goodness", "other alignment"]);

            randomIndex = 1;

            var filters = new Filters { Alignments = ["lawfulness goodness", "other alignment"] };
            SetupChainedTemplates(["my template"], creatureAlignments, ["lawfulness goodness", "other alignment"], filters);

            var alignment = alignmentGenerator.Generate("creature name", ["my template"], filters);
            Assert.That(alignment.Full, Is.EqualTo("other alignment"));
        }

        [Test]
        public void Generate_WithAlignmentFilterAndMultipleTemplates()
        {
            var creatureAlignments = new[] { "wrong alignment", "lawfulness goodness" };
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups, "creature name"))
                .Returns(creatureAlignments);

            var filters = new Filters { Alignments = ["lawfulness goodness"] };
            var templates = new[] { "my template", "my other template" };
            SetupChainedTemplates(templates, creatureAlignments, ["lawfulness goodness"], filters);

            var alignment = alignmentGenerator.Generate("creature name", templates, filters);
            Assert.That(alignment.Full, Is.EqualTo("lawfulness goodness"));
        }

        [Test]
        public void Generate_WithMultipleAlignmentFiltersAndMultipleTemplates()
        {
            var creatureAlignments = new[] { "wrong alignment", "lawfulness goodness", "other alignment" };
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups, "creature name"))
                .Returns(creatureAlignments);

            randomIndex = 1;

            var filters = new Filters { Alignments = ["lawfulness goodness", "other alignment"] };
            var templates = new[] { "my template", "my other template" };
            SetupChainedTemplates(templates, creatureAlignments, ["lawfulness goodness", "other alignment"], filters);

            var alignment = alignmentGenerator.Generate("creature name", templates, filters);
            Assert.That(alignment.Full, Is.EqualTo("other alignment"));
        }

        [Test]
        public void BUG_Generate_WithAlignmentFilterAndMultipleTemplates_TemplatesTransformAlignment()
        {
            var creatureAlignments = new[] { "wrong alignment", "base alignment", "other alignment" };
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups, "creature name"))
                .Returns(creatureAlignments);

            var filters = new Filters { Alignments = ["template alignment"] };
            var templates = new[] { "my template", "my other template" };
            SetupChainedTemplates(templates, creatureAlignments, ["base alignment"], filters);

            var alignment = alignmentGenerator.Generate("creature name", templates, filters);
            Assert.That(alignment.Full, Is.EqualTo("base alignment"));
        }

        [Test]
        public void Generate_Alignment()
        {
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups, "creature name"))
                .Returns(["lawfulness goodness"]);

            var alignment = alignmentGenerator.Generate("creature name", null, null);
            Assert.That(alignment.Full, Is.EqualTo("lawfulness goodness"));
        }

        [Test]
        public void Generate_Alignment_WithTemplate()
        {
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups, "creature name"))
                .Returns(["other wrong alignment", "lawfulness goodness", "wrong alignment"]);

            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups, "my template" + GroupConstants.AllowedInput))
                .Returns(["template alignment", "lawfulness goodness"]);

            var alignment = alignmentGenerator.Generate("creature name", ["my template"], null);
            Assert.That(alignment.Full, Is.EqualTo("lawfulness goodness"));
        }

        [Test]
        public void Generate_Alignment_WithMultipleTemplates()
        {
            var creatureAlignments = new[] { "lawfulness goodness", "wrong alignment" };
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups, "creature name"))
                .Returns(creatureAlignments);

            var templates = new[] { "my template", "my other template" };
            SetupChainedTemplates(templates, creatureAlignments, ["lawfulness goodness"], null);

            var alignment = alignmentGenerator.Generate("creature name", templates, null);
            Assert.That(alignment.Full, Is.EqualTo("lawfulness goodness"));
        }

        [Test]
        public void Generate_RandomAlignment()
        {
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups, "creature name"))
                .Returns(
                [
                    "lawfulness goodness",
                    "other alignment"
                ]);

            randomIndex = 1;

            var alignment = alignmentGenerator.Generate("creature name", null, null);
            Assert.That(alignment.Full, Is.EqualTo("other alignment"));
        }

        [Test]
        public void Generate_RandomAlignment_WithTemplate()
        {
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups, "creature name"))
                .Returns(
                [
                    "lawfulness goodness",
                    "wrong alignment",
                    "other alignment",
                ]);

            randomIndex = 1;

            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups, "my template" + GroupConstants.AllowedInput))
                .Returns(["template alignment", "lawfulness goodness", "other alignment"]);

            var alignment = alignmentGenerator.Generate("creature name", ["my template"], null);
            Assert.That(alignment.Full, Is.EqualTo("other alignment"));
        }

        [Test]
        public void Generate_RandomAlignment_WithMultipleTemplates()
        {
            var creatureAlignments = new[] { "lawfulness goodness", "wrong alignment", "other alignment" };
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups, "creature name"))
                .Returns(creatureAlignments);

            randomIndex = 1;

            var templates = new[] { "my template", "my other template" };
            SetupChainedTemplates(templates, creatureAlignments, ["lawfulness goodness", "other alignment"], null);

            var alignment = alignmentGenerator.Generate("creature name", templates, null);
            Assert.That(alignment.Full, Is.EqualTo("other alignment"));
        }

        [Test]
        public void Generate_RandomWeightedAlignment()
        {
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups, "creature name"))
                .Returns(
                [
                    "lawfulness goodness",
                    "lawfulness goodness",
                    "other alignment"
                ]);

            randomIndex = 1;

            var alignment = alignmentGenerator.Generate("creature name", null, null);
            Assert.That(alignment.Full, Is.EqualTo("lawfulness goodness"));
        }

        [Test]
        public void Generate_RandomWeightedAlignment_WithTemplate()
        {
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups, "creature name"))
                .Returns(
                [
                    "lawfulness goodness",
                    "wrong alignment",
                    "lawfulness goodness",
                    "other alignment"
                ]);

            randomIndex = 1;

            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups, "my template" + GroupConstants.AllowedInput))
                .Returns(["template alignment", "lawfulness goodness", "other alignment"]);

            var alignment = alignmentGenerator.Generate("creature name", ["my template"], null);
            Assert.That(alignment.Full, Is.EqualTo("lawfulness goodness"));
        }

        [Test]
        public void Generate_RandomWeightedAlignment_WithMultipleTemplates()
        {
            var creatureAlignments = new[]
            {
                "lawfulness goodness",
                "wrong alignment",
                "lawfulness goodness",
                "other alignment"
            };
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups, "creature name"))
                .Returns(creatureAlignments);

            randomIndex = 1;

            var templates = new[] { "my template", "my other template" };
            SetupChainedTemplates(templates, creatureAlignments, ["lawfulness goodness", "other alignment"], null);

            var alignment = alignmentGenerator.Generate("creature name", templates, null);
            Assert.That(alignment.Full, Is.EqualTo("lawfulness goodness"));
        }

        [Test]
        public void Generate_RandomAlignmentFromMultipleGroups()
        {
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups, "creature name"))
                .Returns(
                [
                    "lawfulness goodness",
                    "other alignment",
                    "wrong lawfulness goodness",
                    "other alignment",
                ]);

            randomIndex = 3;

            var alignment = alignmentGenerator.Generate("creature name", null, null);
            Assert.That(alignment.Full, Is.EqualTo("other alignment"));
        }

        [Test]
        public void Generate_RandomAlignmentFromMultipleGroups_WithTemplate()
        {
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups, "creature name"))
                .Returns(
                [
                    "lawfulness goodness",
                    "wrong alignment",
                    "other alignment",
                    "wrong lawfulness goodness",
                    "wrong alignment",
                    "other alignment",
                ]);

            randomIndex = 3;

            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups, "my template" + GroupConstants.AllowedInput))
                .Returns(["template alignment", "lawfulness goodness", "other wrong alignment", "wrong lawfulness goodness", "other alignment"]);

            var alignment = alignmentGenerator.Generate("creature name", ["my template"], null);
            Assert.That(alignment.Full, Is.EqualTo("other alignment"));
        }

        [Test]
        public void Generate_RandomAlignmentFromMultipleGroups_WithMultipleTemplates()
        {
            var creatureAlignments = new[]
            {
                "lawfulness goodness",
                "wrong alignment",
                "other alignment",
                "wrong lawfulness goodness",
                "wrong alignment",
                "other alignment",
            };
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups, "creature name"))
                .Returns(creatureAlignments);

            randomIndex = 2;

            var templates = new[] { "my template", "my other template" };
            SetupChainedTemplates(templates, creatureAlignments, ["lawfulness goodness", "other alignment"], null);

            var alignment = alignmentGenerator.Generate("creature name", templates, null);
            Assert.That(alignment.Full, Is.EqualTo("other alignment"));
        }

        [Test]
        public void Generate_RandomWeightedAlignmentFromMultipleGroups()
        {
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups, "creature name"))
                .Returns(
                [
                    "lawfulness goodness",
                    "lawfulness goodness",
                    "other alignment",
                    "wrong lawfulness goodness",
                    "other alignment",
                    "other alignment",
                ]);

            randomIndex = 4;

            var alignment = alignmentGenerator.Generate("creature name", null, null);
            Assert.That(alignment.Full, Is.EqualTo("other alignment"));
        }

        [Test]
        public void Generate_RandomWeightedAlignmentFromMultipleGroups_WithTemplate()
        {
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups, "creature name"))
                .Returns(
                [
                    "lawfulness goodness",
                    "wrong alignment",
                    "lawfulness goodness",
                    "wrong alignment",
                    "other alignment",
                    "wrong alignment",
                    "wrong lawfulness goodness",
                    "wrong alignment",
                    "other alignment",
                    "wrong alignment",
                    "other alignment",
                ]);

            randomIndex = 4;

            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups, "my template" + GroupConstants.AllowedInput))
                .Returns(["template alignment", "lawfulness goodness", "other alignment"]);

            var alignment = alignmentGenerator.Generate("creature name", ["my template"], null);
            Assert.That(alignment.Full, Is.EqualTo("other alignment"));
        }

        [Test]
        public void Generate_RandomWeightedAlignmentFromMultipleGroups_WithMultipleTemplates()
        {
            var creatureAlignments = new[]
            {
                "lawfulness goodness",
                "wrong alignment",
                "lawfulness goodness",
                "wrong alignment",
                "other alignment",
                "wrong alignment",
                "wrong lawfulness goodness",
                "wrong alignment",
                "other alignment",
                "wrong alignment",
                "other alignment",
            };
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups, "creature name"))
                .Returns(creatureAlignments);

            randomIndex = 4;

            var templates = new[] { "my template", "my other template" };
            SetupChainedTemplates(templates, creatureAlignments, ["lawfulness goodness", "other alignment"], null);

            var alignment = alignmentGenerator.Generate("creature name", templates, null);
            Assert.That(alignment.Full, Is.EqualTo("other alignment"));
        }
    }
}