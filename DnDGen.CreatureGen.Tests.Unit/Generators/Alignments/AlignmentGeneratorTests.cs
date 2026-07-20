using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Generators.Alignments;
using DnDGen.CreatureGen.Tables;
using DnDGen.CreatureGen.Templates;
using DnDGen.CreatureGen.Tests.Unit.TestCaseSources;
using DnDGen.Infrastructure.Factories;
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
        private Mock<JustInTimeFactory> mockFactory;
        private int randomIndex;

        [SetUp]
        public void Setup()
        {
            mockCollectionSelector = new Mock<ICollectionSelector>();
            mockFactory = new Mock<JustInTimeFactory>();
            alignmentGenerator = new AlignmentGenerator(mockCollectionSelector.Object, mockFactory.Object);

            randomIndex = 0;
            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>()))
                .Returns((IEnumerable<string> c) => c.ElementAt(randomIndex));
        }

        [Test]
        public void Generate_PresetAlignment()
        {
            var alignment = alignmentGenerator.Generate("creature name", null, "lawfulness goodness");
            Assert.That(alignment.Full, Is.EqualTo("lawfulness goodness"));
        }

        [Test]
        public void Generate_PresetAlignment_WithTemplate()
        {
            var alignment = alignmentGenerator.Generate("creature name", ["my template"], "lawfulness goodness");
            Assert.That(alignment.Full, Is.EqualTo("lawfulness goodness"));
        }

        [Test]
        public void Generate_PresetAlignment_WithMultipleTemplates()
        {
            var alignment = alignmentGenerator.Generate("creature name", ["my template", "my other template"], "lawfulness goodness");
            Assert.That(alignment.Full, Is.EqualTo("lawfulness goodness"));
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

            var mockMyTemplateApplicator = new Mock<TemplateApplicator>();
            var mockMyOtherTemplateApplicator = new Mock<TemplateApplicator>();

            mockFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockMyTemplateApplicator.Object);
            mockFactory.Setup(f => f.Build<TemplateApplicator>("my other template")).Returns(mockMyOtherTemplateApplicator.Object);

            var prototypes1 = new[]
            {
                new CreaturePrototype
                {
                    Name = "creature name",
                    Alignments =
                    [
                        new("template alignment"),
                        new("lawfulness goodness"),
                    ]
                },
            };
            var prototypes2 = new[]
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

            mockMyTemplateApplicator.Setup(a => a.GetCompatiblePrototypes(It.Is<IEnumerable<string>>(n => n.IsEquivalentTo(new[] { "creature name" })), false, null)).Returns(prototypes1);
            mockMyOtherTemplateApplicator.Setup(a => a.GetCompatiblePrototypes(prototypes1, false, null)).Returns(prototypes2);

            var alignment = alignmentGenerator.Generate("creature name", ["my template", "my other template"], null);
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

            var mockMyTemplateApplicator = new Mock<TemplateApplicator>();
            var mockMyOtherTemplateApplicator = new Mock<TemplateApplicator>();

            mockFactory.Setup(f => f.Build<TemplateApplicator>("my template")).Returns(mockMyTemplateApplicator.Object);
            mockFactory.Setup(f => f.Build<TemplateApplicator>("my other template")).Returns(mockMyOtherTemplateApplicator.Object);

            var prototypes1 = new[]
            {
                new CreaturePrototype
                {
                    Name = "creature name",
                    Alignments =
                    [
                        new("template alignment"),
                        new("lawfulness goodness"),
                        new("other alignment"),
                    ]
                },
            };
            var prototypes2 = new[]
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

            mockMyTemplateApplicator.Setup(a => a.GetCompatiblePrototypes(It.Is<IEnumerable<string>>(n => n.IsEquivalentTo(new[] { "creature name" })), false, null)).Returns(prototypes1);
            mockMyOtherTemplateApplicator.Setup(a => a.GetCompatiblePrototypes(prototypes1, false, null)).Returns(prototypes2);

            var alignment = alignmentGenerator.Generate("creature name", ["my template", "my other template"], null);
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
        [Ignore("We explicitly do not honor weighting when multiple templates are applied")]
        public void Generate_RandomWeightedAlignment_WithMultipleTemplates()
        {
            Assert.Fail("this is not a valid usecase");
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

            var mockApplicator1 = new Mock<TemplateApplicator>();
            var mockApplicator2 = new Mock<TemplateApplicator>();
            var prototypes1 = new[]
            {
                new CreaturePrototype
                {
                    Name = "protoype 1",
                    Alignments =
                    [
                        new("template alignment"),
                        new("lawfulness goodness"),
                        new("other wrong alignment"),
                        new("wrong lawfulness goodness"),
                        new("other alignment"),
                    ]
                }
            };
            var prototypes2 = new[]
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

            mockApplicator1
                .Setup(a => a.GetCompatiblePrototypes(It.Is<IEnumerable<string>>(cc => cc.IsEquivalentTo(new[] { "creature name" })), false, null))
                .Returns(prototypes1);
            mockApplicator2
                .Setup(a => a.GetCompatiblePrototypes(prototypes1, false, null))
                .Returns(prototypes2);

            mockFactory
                .Setup(f => f.Build<TemplateApplicator>("my template"))
                .Returns(mockApplicator1.Object);
            mockFactory
                .Setup(f => f.Build<TemplateApplicator>("my other template"))
                .Returns(mockApplicator2.Object);

            var alignment = alignmentGenerator.Generate("creature name", ["my template", "my other template"], null);
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