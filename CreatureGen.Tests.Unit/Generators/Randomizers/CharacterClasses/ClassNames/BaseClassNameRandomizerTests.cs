using CreatureGen.Alignments;
using CreatureGen.Domain.Generators.Randomizers.CharacterClasses.ClassNames;
using CreatureGen.Domain.Tables;
using CreatureGen.Verifiers.Exceptions;
using DnDGen.Core.Generators;
using DnDGen.Core.Selectors.Collections;
using DnDGen.Core.Selectors.Percentiles;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace CreatureGen.Tests.Unit.Generators.Randomizers.CharacterClasses.ClassNames
{
    [TestFixture]
    public class BaseClassNameRandomizerTests
    {
        private TestClassRandomizer classNameRandomizer;
        private Mock<IPercentileSelector> mockPercentileSelector;
        private Mock<ICollectionSelector> mockCollectionsSelector;
        private Alignment alignment;

        private string firstClass = "first class";
        private string secondClass = "second class";

        [SetUp]
        public void Setup()
        {
            mockPercentileSelector = new Mock<IPercentileSelector>();
            mockCollectionsSelector = new Mock<ICollectionSelector>();
            var generator = new ConfigurableIterationGenerator(2);

            classNameRandomizer = new TestClassRandomizer(mockPercentileSelector.Object, generator, mockCollectionsSelector.Object);

            alignment = new Alignment();
            alignment.Goodness = AlignmentConstants.Good;

            mockPercentileSelector.Setup(p => p.SelectAllFrom(It.IsAny<string>())).Returns(new[] { firstClass, secondClass });
            mockPercentileSelector.Setup(p => p.SelectFrom(It.IsAny<string>())).Returns(firstClass);

            var index = 0;
            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>())).Returns((IEnumerable<string> ss) => ss.ElementAt(index++ % ss.Count()));
        }

        [Test]
        public void RandomizeGetsAllPossibleResultsFromGetAllPossibleResults()
        {
            classNameRandomizer.Randomize(alignment);
            mockPercentileSelector.Verify(p => p.SelectAllFrom(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void RandomizeThrowsErrorIfNoPossibleResults()
        {
            mockPercentileSelector.Setup(p => p.SelectAllFrom(It.IsAny<string>())).Returns(Enumerable.Empty<string>());
            Assert.That(() => classNameRandomizer.Randomize(alignment), Throws.InstanceOf<IncompatibleRandomizersException>());
        }

        [Test]
        public void RandomizeReturnCharacterClassFromPercentileResultSelector()
        {
            var result = classNameRandomizer.Randomize(alignment);
            Assert.That(result, Is.EqualTo(firstClass));
        }

        [Test]
        public void RandomizeAccessesTableAlignmentGoodnessClassNames()
        {
            classNameRandomizer.Randomize(alignment);

            var tableName = string.Format("{0}CharacterClasses", alignment.Goodness);
            mockPercentileSelector.Verify(p => p.SelectFrom(tableName), Times.Once);
        }

        [Test]
        public void RandomizeLoopsUntilAllowedClassNameIsRolled()
        {
            mockPercentileSelector.SetupSequence(p => p.SelectFrom(It.IsAny<string>())).Returns("invalid class name")
                .Returns(firstClass);

            classNameRandomizer.Randomize(alignment);
            mockPercentileSelector.Verify(p => p.SelectFrom(It.IsAny<string>()), Times.Exactly(2));
        }

        [Test]
        public void RandomizeReturnsDefault()
        {
            mockPercentileSelector.Setup(p => p.SelectFrom(It.IsAny<string>())).Returns("invalid class name");

            var className = classNameRandomizer.Randomize(alignment);
            Assert.That(className, Is.EqualTo(firstClass));
        }

        [Test]
        public void GetAllPossibleResultsGetsResultsFromSelector()
        {
            var classNames = classNameRandomizer.GetAllPossibleResults(alignment);

            Assert.That(classNames, Contains.Item(firstClass));
            Assert.That(classNames, Contains.Item(secondClass));
            Assert.That(classNames.Count(), Is.EqualTo(2));
        }

        [Test]
        public void GetAllPossibleResultsAccessesTableAlignmentGoodnessClassNames()
        {
            classNameRandomizer.GetAllPossibleResults(alignment);

            var tableName = string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCharacterClasses, alignment.Goodness);
            mockPercentileSelector.Verify(p => p.SelectAllFrom(tableName), Times.Once);
        }

        [Test]
        public void GetAllPossibleResultsFiltersOutUnallowedCharacterClasses()
        {
            classNameRandomizer.NotAllowedClassName = firstClass;
            var results = classNameRandomizer.GetAllPossibleResults(alignment);

            Assert.That(results, Contains.Item(secondClass));
            Assert.That(results.Count(), Is.EqualTo(1));
        }

        private class TestClassRandomizer : BaseClassNameRandomizer
        {
            public string NotAllowedClassName { get; set; }

            public TestClassRandomizer(IPercentileSelector percentileResultSelector, Generator generator, ICollectionSelector collectionsSelector)
                : base(percentileResultSelector, generator, collectionsSelector)
            { }

            protected override bool CharacterClassIsAllowed(string className, Alignment alignment)
            {
                return className != NotAllowedClassName;
            }
        }
    }
}