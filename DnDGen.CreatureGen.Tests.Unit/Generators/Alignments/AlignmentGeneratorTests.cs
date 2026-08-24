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
        private int randomIndex;

        [SetUp]
        public void Setup()
        {
            mockCollectionSelector = new Mock<ICollectionSelector>();
            mockCreatureVerifier = new Mock<ICreatureVerifier>();
            alignmentGenerator = new AlignmentGenerator(mockCollectionSelector.Object, mockCreatureVerifier.Object);

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
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups, "creature name"))
                .Returns(["wrong alignment", "lawfulness goodness"]);

            var filters = new Filters { Alignments = ["lawfulness goodness"] };
            var alignment = alignmentGenerator.Generate("creature name", ["my template"], filters);
            Assert.That(alignment.Full, Is.EqualTo("lawfulness goodness"));
        }

        [Test]
        public void Generate_WithMultipleAlignmentFiltersAndTemplate()
        {
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups, "creature name"))
                .Returns(["wrong alignment", "lawfulness goodness"]);

            var filters = new Filters { Alignments = ["lawfulness goodness", "other alignment"] };
            var alignment = alignmentGenerator.Generate("creature name", ["my template"], filters);
            Assert.That(alignment.Full, Is.EqualTo("other alignment"));
        }

        [Test]
        public void Generate_WithAlignmentFilterAndMultipleTemplates()
        {
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups, "creature name"))
                .Returns(["wrong alignment", "lawfulness goodness"]);

            var filters = new Filters { Alignments = ["lawfulness goodness"] };
            var alignment = alignmentGenerator.Generate("creature name", ["my template", "my other template"], filters);
            Assert.That(alignment.Full, Is.EqualTo("lawfulness goodness"));
        }

        [Test]
        public void Generate_WithMultipleAlignmentFiltersAndMultipleTemplates()
        {
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups, "creature name"))
                .Returns(["wrong alignment", "lawfulness goodness"]);

            var filters = new Filters { Alignments = ["lawfulness goodness", "other alignment"] };
            var alignment = alignmentGenerator.Generate("creature name", ["my template", "my other template"], filters);
            Assert.That(alignment.Full, Is.EqualTo("other alignment"));
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
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups, "creature name"))
                .Returns(["lawfulness goodness", "wrong alignment"]);

            var prototypes = new[]
            {
                new CreaturePrototype
                {
                    Name = "creature name",
                    Alignments =
                    [
                        new("lawfulness goodness"),
                        new("other-template alignment"),
                        new("other-wrong alignment"),
                    ]
                },
            };

            var templates = new[] { "my template", "my other template" };
            mockCreatureVerifier
                .Setup(a => a.GetChainedTemplates(
                    It.Is<IEnumerable<string>>(n => n.IsEquivalentTo("creature name")),
                    It.Is<string[]>(n => n.IsEquivalentTo(templates)),
                    false,
                    null,
                    null))
                .Returns(prototypes);

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
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups, "creature name"))
                .Returns(
                [
                    "lawfulness goodness",
                    "wrong alignment",
                    "other alignment",
                ]);

            randomIndex = 1;

            var prototypes = new[]
            {
                new CreaturePrototype
                {
                    Name = "creature name",
                    Alignments =
                    [
                        new("lawfulness goodness"),
                        new("other alignment"),
                        new("other-template alignment"),
                        new("other-wrong alignment"),
                        new("chaotic evilness"),
                    ]
                },
            };

            var templates = new[] { "my template", "my other template" };
            mockCreatureVerifier
                .Setup(a => a.GetChainedTemplates(
                    It.Is<IEnumerable<string>>(n => n.IsEquivalentTo("creature name")),
                    It.Is<string[]>(n => n.IsEquivalentTo(templates)),
                    false,
                    null,
                    null))
                .Returns(prototypes);

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

            var prototypes = new[]
            {
                new CreaturePrototype
                {
                    Name = "creature name",
                    Alignments =
                    [
                        new("lawfulness goodness"),
                        new("lawfulness goodness"),
                        new("other alignment"),
                        new("other-template alignment"),
                        new("other-wrong alignment"),
                        new("chaotic evilness"),
                    ]
                },
            };

            var templates = new[] { "my template", "my other template" };
            mockCreatureVerifier
                .Setup(a => a.GetChainedTemplates(
                    It.Is<IEnumerable<string>>(n => n.IsEquivalentTo("creature name")),
                    It.Is<string[]>(n => n.IsEquivalentTo(templates)),
                    false,
                    null,
                    null))
                .Returns(prototypes);

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

            randomIndex = 2;

            var prototypes = new[]
            {
                new CreaturePrototype
                {
                    Name = "protoype 2",
                    Alignments =
                    [
                        new("template2 alignment"),
                        new("lawfulness goodness"),
                        new("other alignment"),
                        new("wrong2 alignment"),
                        new("chaotic evilness"),
                    ]
                }
            };

            var templates = new[] { "my template", "my other template" };
            mockCreatureVerifier
                .Setup(a => a.GetChainedTemplates(
                    It.Is<IEnumerable<string>>(cc => cc.IsEquivalentTo("creature name")),
                    It.Is<string[]>(cc => cc.IsEquivalentTo(templates)),
                    false,
                    null,
                    null))
                .Returns(prototypes);

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
        [Ignore("We explicitly do not honor weighting when multiple templates are applied")]
        public void Generate_RandomWeightedAlignmentFromMultipleGroups_WithMultipleTemplates()
        {
            Assert.Fail("this is not a valid usecase");
        }
    }
}