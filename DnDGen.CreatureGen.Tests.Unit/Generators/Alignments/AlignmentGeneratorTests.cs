using DnDGen.CreatureGen.Generators.Alignments;
using DnDGen.CreatureGen.Tables;
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
        private int randomIndex;

        [SetUp]
        public void Setup()
        {
            mockCollectionSelector = new Mock<ICollectionSelector>();
            alignmentGenerator = new AlignmentGenerator(mockCollectionSelector.Object);

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
            var alignment = alignmentGenerator.Generate("creature name", new[] { "my template" }, "lawfulness goodness");
            Assert.That(alignment.Full, Is.EqualTo("lawfulness goodness"));
        }

        [Test]
        public void Generate_PresetAlignment_WithMultipleTemplates()
        {
            var alignment = alignmentGenerator.Generate("creature name", new[] { "my template", "my other template" }, "lawfulness goodness");
            Assert.That(alignment.Full, Is.EqualTo("lawfulness goodness"));
        }

        [Test]
        public void Generate_Alignment()
        {
            mockCollectionSelector
                .Setup(s => s.ExplodeAndPreserveDuplicates(TableNameConstants.Collection.AlignmentGroups, "creature name"))
                .Returns(new[] { "lawfulness goodness" });

            var alignment = alignmentGenerator.Generate("creature name", null, null);
            Assert.That(alignment.Full, Is.EqualTo("lawfulness goodness"));
        }

        [Test]
        public void Generate_Alignment_WithTemplate()
        {
            mockCollectionSelector
                .Setup(s => s.ExplodeAndPreserveDuplicates(TableNameConstants.Collection.AlignmentGroups, "creature name"))
                .Returns(new[] { "other wrong alignment", "lawfulness goodness", "wrong alignment" });

            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.AlignmentGroups, "my template" + GroupConstants.AllowedInput))
                .Returns(new[] { "template alignment", "lawfulness goodness" });

            var alignment = alignmentGenerator.Generate("creature name", new[] { "my template" }, null);
            Assert.That(alignment.Full, Is.EqualTo("lawfulness goodness"));
        }

        [TestCase(null)]
        [TestCase("")]
        public void Generate_Alignment_WithEmptyTemplate(string empty)
        {
            mockCollectionSelector
                .Setup(s => s.ExplodeAndPreserveDuplicates(TableNameConstants.Collection.AlignmentGroups, "creature name"))
                .Returns(new[] { "lawfulness goodness", "wrong alignment" });

            var alignment = alignmentGenerator.Generate("creature name", new[] { empty }, null);
            Assert.That(alignment.Full, Is.EqualTo("lawfulness goodness"));
        }

        [Test]
        public void Generate_Alignment_WithMultipleTemplates()
        {
            mockCollectionSelector
                .Setup(s => s.ExplodeAndPreserveDuplicates(TableNameConstants.Collection.AlignmentGroups, "creature name"))
                .Returns(new[] { "lawfulness goodness", "wrong alignment" });

            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.AlignmentGroups, "my template" + GroupConstants.AllowedInput))
                .Returns(new[] { "template alignment", "lawfulness goodness" });

            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.AlignmentGroups, "my other template" + GroupConstants.AllowedInput))
                .Returns(new[] { "other template alignment", "lawfulness goodness", "other wrong alignment" });

            var alignment = alignmentGenerator.Generate("creature name", new[] { "my template", "my other template" }, null);
            Assert.That(alignment.Full, Is.EqualTo("lawfulness goodness"));
        }

        [Test]
        public void Generate_RandomAlignment()
        {
            mockCollectionSelector
                .Setup(s => s.ExplodeAndPreserveDuplicates(TableNameConstants.Collection.AlignmentGroups, "creature name"))
                .Returns(new[]
                {
                    "lawfulness goodness",
                    "other alignment"
                });

            randomIndex = 1;

            var alignment = alignmentGenerator.Generate("creature name", null, null);
            Assert.That(alignment.Full, Is.EqualTo("other alignment"));
        }

        [Test]
        public void Generate_RandomAlignment_WithTemplate()
        {
            mockCollectionSelector
                .Setup(s => s.ExplodeAndPreserveDuplicates(TableNameConstants.Collection.AlignmentGroups, "creature name"))
                .Returns(new[]
                {
                    "lawfulness goodness",
                    "wrong alignment",
                    "other alignment",
                });

            randomIndex = 1;

            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.AlignmentGroups, "my template" + GroupConstants.AllowedInput))
                .Returns(new[] { "template alignment", "lawfulness goodness", "other alignment" });

            var alignment = alignmentGenerator.Generate("creature name", new[] { "my template" }, null);
            Assert.That(alignment.Full, Is.EqualTo("other alignment"));
        }

        [Test]
        public void Generate_RandomAlignment_WithMultipleTemplates()
        {
            mockCollectionSelector
                .Setup(s => s.ExplodeAndPreserveDuplicates(TableNameConstants.Collection.AlignmentGroups, "creature name"))
                .Returns(new[]
                {
                    "lawfulness goodness",
                    "wrong alignment",
                    "other alignment",
                });

            randomIndex = 1;

            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.AlignmentGroups, "my template" + GroupConstants.AllowedInput))
                .Returns(new[] { "template alignment", "lawfulness goodness", "other alignment" });

            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.AlignmentGroups, "my other template" + GroupConstants.AllowedInput))
                .Returns(new[] { "other template alignment", "lawfulness goodness", "other wrong alignment", "other alignment", "chaotic evilness" });

            var alignment = alignmentGenerator.Generate("creature name", new[] { "my template", "my other template" }, null);
            Assert.That(alignment.Full, Is.EqualTo("other alignment"));
        }

        [Test]
        public void Generate_RandomWeightedAlignment()
        {
            mockCollectionSelector
                .Setup(s => s.ExplodeAndPreserveDuplicates(TableNameConstants.Collection.AlignmentGroups, "creature name"))
                .Returns(new[]
                {
                    "lawfulness goodness",
                    "lawfulness goodness",
                    "other alignment"
                });

            randomIndex = 1;

            var alignment = alignmentGenerator.Generate("creature name", null, null);
            Assert.That(alignment.Full, Is.EqualTo("lawfulness goodness"));
        }

        [Test]
        public void Generate_RandomWeightedAlignment_WithTemplate()
        {
            mockCollectionSelector
                .Setup(s => s.ExplodeAndPreserveDuplicates(TableNameConstants.Collection.AlignmentGroups, "creature name"))
                .Returns(new[]
                {
                    "lawfulness goodness",
                    "wrong alignment",
                    "lawfulness goodness",
                    "other alignment"
                });

            randomIndex = 1;

            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.AlignmentGroups, "my template" + GroupConstants.AllowedInput))
                .Returns(new[] { "template alignment", "lawfulness goodness", "other alignment" });

            var alignment = alignmentGenerator.Generate("creature name", new[] { "my template" }, null);
            Assert.That(alignment.Full, Is.EqualTo("lawfulness goodness"));
        }

        [Test]
        public void Generate_RandomWeightedAlignment_WithMultipleTemplates()
        {
            mockCollectionSelector
                .Setup(s => s.ExplodeAndPreserveDuplicates(TableNameConstants.Collection.AlignmentGroups, "creature name"))
                .Returns(new[]
                {
                    "lawfulness goodness",
                    "wrong alignment",
                    "lawfulness goodness",
                    "other alignment"
                });

            randomIndex = 1;

            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.AlignmentGroups, "my template" + GroupConstants.AllowedInput))
                .Returns(new[] { "template alignment", "lawfulness goodness", "other alignment" });

            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.AlignmentGroups, "my other template" + GroupConstants.AllowedInput))
                .Returns(new[] { "other template alignment", "lawfulness goodness", "other wrong alignment", "other alignment", "chaotic evilness" });

            var alignment = alignmentGenerator.Generate("creature name", new[] { "my template", "my other template" }, null);
            Assert.That(alignment.Full, Is.EqualTo("lawfulness goodness"));
        }

        [Test]
        public void Generate_RandomAlignmentFromMultipleGroups()
        {
            mockCollectionSelector
                .Setup(s => s.ExplodeAndPreserveDuplicates(TableNameConstants.Collection.AlignmentGroups, "creature name"))
                .Returns(new[]
                {
                    "lawfulness goodness",
                    "other alignment",
                    "wrong lawfulness goodness",
                    "other alignment",
                });

            randomIndex = 3;

            var alignment = alignmentGenerator.Generate("creature name", null, null);
            Assert.That(alignment.Full, Is.EqualTo("other alignment"));
        }

        [Test]
        public void Generate_RandomAlignmentFromMultipleGroups_WithTemplate()
        {
            mockCollectionSelector
                .Setup(s => s.ExplodeAndPreserveDuplicates(TableNameConstants.Collection.AlignmentGroups, "creature name"))
                .Returns(new[]
                {
                    "lawfulness goodness",
                    "wrong alignment",
                    "other alignment",
                    "wrong lawfulness goodness",
                    "wrong alignment",
                    "other alignment",
                });

            randomIndex = 3;

            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.AlignmentGroups, "my template" + GroupConstants.AllowedInput))
                .Returns(new[] { "template alignment", "lawfulness goodness", "other wrong alignment", "wrong lawfulness goodness", "other alignment" });

            var alignment = alignmentGenerator.Generate("creature name", new[] { "my template" }, null);
            Assert.That(alignment.Full, Is.EqualTo("other alignment"));
        }

        [Test]
        public void Generate_RandomAlignmentFromMultipleGroups_WithMultipleTemplates()
        {
            mockCollectionSelector
                .Setup(s => s.ExplodeAndPreserveDuplicates(TableNameConstants.Collection.AlignmentGroups, "creature name"))
                .Returns(new[]
                {
                    "lawfulness goodness",
                    "wrong alignment",
                    "other alignment",
                    "wrong lawfulness goodness",
                    "wrong alignment",
                    "other alignment",
                });

            randomIndex = 2;

            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.AlignmentGroups, "my template" + GroupConstants.AllowedInput))
                .Returns(new[] { "template alignment", "lawfulness goodness", "other wrong alignment", "wrong lawfulness goodness", "other alignment" });

            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.AlignmentGroups, "my other template" + GroupConstants.AllowedInput))
                .Returns(new[] { "other template alignment", "lawfulness goodness", "other wrong alignment", "other alignment", "chaotic evilness" });

            var alignment = alignmentGenerator.Generate("creature name", new[] { "my template", "my other template" }, null);
            Assert.That(alignment.Full, Is.EqualTo("other alignment"));
        }

        [Test]
        public void Generate_RandomWeightedAlignmentFromMultipleGroups()
        {
            mockCollectionSelector
                .Setup(s => s.ExplodeAndPreserveDuplicates(TableNameConstants.Collection.AlignmentGroups, "creature name"))
                .Returns(new[]
                {
                    "lawfulness goodness",
                    "lawfulness goodness",
                    "other alignment",
                    "wrong lawfulness goodness",
                    "other alignment",
                    "other alignment",
                });

            randomIndex = 4;

            var alignment = alignmentGenerator.Generate("creature name", null, null);
            Assert.That(alignment.Full, Is.EqualTo("other alignment"));
        }

        [Test]
        public void Generate_RandomWeightedAlignmentFromMultipleGroups_WithTemplate()
        {
            mockCollectionSelector
                .Setup(s => s.ExplodeAndPreserveDuplicates(TableNameConstants.Collection.AlignmentGroups, "creature name"))
                .Returns(new[]
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
                });

            randomIndex = 4;

            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.AlignmentGroups, "my template" + GroupConstants.AllowedInput))
                .Returns(new[] { "template alignment", "lawfulness goodness", "other alignment" });

            var alignment = alignmentGenerator.Generate("creature name", new[] { "my template" }, null);
            Assert.That(alignment.Full, Is.EqualTo("other alignment"));
        }

        [Test]
        public void Generate_RandomWeightedAlignmentFromMultipleGroups_WithMultipleTemplates()
        {
            mockCollectionSelector
                .Setup(s => s.ExplodeAndPreserveDuplicates(TableNameConstants.Collection.AlignmentGroups, "creature name"))
                .Returns(new[]
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
                });

            randomIndex = 4;

            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.AlignmentGroups, "my template" + GroupConstants.AllowedInput))
                .Returns(new[] { "template alignment", "lawfulness goodness", "other alignment" });

            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.AlignmentGroups, "my other template" + GroupConstants.AllowedInput))
                .Returns(new[] { "other template alignment", "lawfulness goodness", "other wrong alignment", "other alignment", "chaotic evilness" });

            var alignment = alignmentGenerator.Generate("creature name", new[] { "my template", "my other template" }, null);
            Assert.That(alignment.Full, Is.EqualTo("other alignment"));
        }
    }
}