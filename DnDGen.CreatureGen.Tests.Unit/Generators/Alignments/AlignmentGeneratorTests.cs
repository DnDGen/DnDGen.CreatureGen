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
        public void GeneratePresetAlignment()
        {
            var alignment = alignmentGenerator.Generate("creature name", "lawfulness goodness");
            Assert.That(alignment.Full, Is.EqualTo("lawfulness goodness"));
        }

        [Test]
        public void GenerateAlignment()
        {
            mockCollectionSelector
                .Setup(s => s.ExplodeAndPreserveDuplicates(TableNameConstants.Collection.AlignmentGroups, "creature name"))
                .Returns(new[]
                {
                    "lawfulness goodness"
                });

            var alignment = alignmentGenerator.Generate("creature name", null);
            Assert.That(alignment.Full, Is.EqualTo("lawfulness goodness"));
        }

        [Test]
        public void GenerateRandomAlignment()
        {
            mockCollectionSelector
                .Setup(s => s.ExplodeAndPreserveDuplicates(TableNameConstants.Collection.AlignmentGroups, "creature name"))
                .Returns(new[]
                {
                    "lawfulness goodness",
                    "other alignment"
                });

            randomIndex = 1;

            var alignment = alignmentGenerator.Generate("creature name", null);
            Assert.That(alignment.Full, Is.EqualTo("other alignment"));
        }

        [Test]
        public void GenerateRandomWeightedAlignment()
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

            var alignment = alignmentGenerator.Generate("creature name", null);
            Assert.That(alignment.Full, Is.EqualTo("lawfulness goodness"));
        }

        [Test]
        public void GenerateRandomAlignmentFromMultipleGroups()
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

            var alignment = alignmentGenerator.Generate("creature name", null);
            Assert.That(alignment.Full, Is.EqualTo("other alignment"));
        }

        [Test]
        public void GenerateRandomWeightedAlignmentFromMultipleGroups()
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

            var alignment = alignmentGenerator.Generate("creature name", null);
            Assert.That(alignment.Full, Is.EqualTo("other alignment"));
        }
    }
}