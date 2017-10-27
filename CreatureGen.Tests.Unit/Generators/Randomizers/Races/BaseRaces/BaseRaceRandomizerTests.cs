using CreatureGen.Alignments;
using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Generators.Randomizers.Races.BaseRaces;
using CreatureGen.Domain.Tables;
using CreatureGen.Verifiers.Exceptions;
using DnDGen.Core.Selectors.Collections;
using DnDGen.Core.Selectors.Percentiles;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CreatureGen.Tests.Unit.Generators.Randomizers.Races.BaseRaces
{
    [TestFixture]
    public class BaseRaceRandomizerTests
    {
        private TestBaseRaceRandomizer randomizer;
        private Mock<IPercentileSelector> mockPercentileResultSelector;
        private Mock<ICollectionSelector> mockCollectionSelector;

        private string firstBaseRace = "first base race";
        private string secondBaseRace = "second base race";
        private CharacterClassPrototype characterClass;
        private Alignment alignment;
        private List<string> alignmentRaces;

        [SetUp]
        public void Setup()
        {
            mockPercentileResultSelector = new Mock<IPercentileSelector>();
            mockCollectionSelector = new Mock<ICollectionSelector>();
            randomizer = new TestBaseRaceRandomizer(mockPercentileResultSelector.Object, mockCollectionSelector.Object);

            alignment = new Alignment();
            characterClass = new CharacterClassPrototype();
            alignmentRaces = new List<string>();

            characterClass.Name = "class name";
            characterClass.Level = 1;

            alignment.Goodness = Guid.NewGuid().ToString();
            alignment.Lawfulness = Guid.NewGuid().ToString();

            alignmentRaces.Add(firstBaseRace);
            alignmentRaces.Add(secondBaseRace);

            var baseRaces = new[] { firstBaseRace, secondBaseRace, string.Empty };

            mockPercentileResultSelector.Setup(s => s.SelectAllFrom(It.IsAny<string>())).Returns(baseRaces);
            mockPercentileResultSelector.Setup(s => s.SelectFrom(It.IsAny<string>())).Returns(firstBaseRace);
            mockCollectionSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, alignment.Full)).Returns(alignmentRaces);

            var index = 0;
            mockCollectionSelector.Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>())).Returns((IEnumerable<string> ss) => ss.ElementAt(index++ % ss.Count()));
        }

        [Test]
        public void RandomizeGetsAllPossibleResultsFromGetAllPossibleResults()
        {
            randomizer.Randomize(alignment, characterClass);
            mockPercentileResultSelector.Verify(p => p.SelectAllFrom(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void RandomizeThrowsErrorIfNoPossibleResults()
        {
            mockPercentileResultSelector.Setup(p => p.SelectAllFrom(It.IsAny<string>())).Returns(Enumerable.Empty<string>());
            Assert.That(() => randomizer.Randomize(alignment, characterClass), Throws.InstanceOf<IncompatibleRandomizersException>());
        }

        [Test]
        public void RandomizeReturnsBaseRaceFromPercentileResultSelector()
        {
            var result = randomizer.Randomize(alignment, characterClass);
            Assert.That(result, Is.EqualTo(firstBaseRace));
        }

        [Test]
        public void RandomizeAccessesTableAlignmentGoodnessClassNameBaseRaces()
        {
            randomizer.Randomize(alignment, characterClass);
            var tableName = string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, alignment.Goodness, characterClass.Name);
            mockPercentileResultSelector.Verify(p => p.SelectFrom(tableName), Times.Once);
        }

        [Test]
        public void RandomizeRerollsEmptyBaseRace()
        {
            mockPercentileResultSelector.SetupSequence(p => p.SelectFrom(It.IsAny<string>()))
                .Returns(string.Empty)
                .Returns(firstBaseRace);

            var baseRace = randomizer.Randomize(alignment, characterClass);
            Assert.That(baseRace, Is.EqualTo(firstBaseRace));
            mockPercentileResultSelector.Verify(p => p.SelectFrom(It.IsAny<string>()), Times.Exactly(2));
        }

        [Test]
        public void RandomizeRerollsInvalidBaseRaceBecauseNotAllowed()
        {
            randomizer.ForbiddenBaseRace = firstBaseRace;

            mockPercentileResultSelector.SetupSequence(p => p.SelectFrom(It.IsAny<string>()))
                .Returns(firstBaseRace)
                .Returns(secondBaseRace);

            var baseRace = randomizer.Randomize(alignment, characterClass);
            Assert.That(baseRace, Is.EqualTo(secondBaseRace));
            mockPercentileResultSelector.Verify(p => p.SelectFrom(It.IsAny<string>()), Times.Exactly(2));
        }

        [Test]
        public void RandomizeRerollsInvalidBaseRaceBecauseAlignmentDoesNotAllow()
        {
            alignmentRaces.Remove(firstBaseRace);

            mockPercentileResultSelector.SetupSequence(p => p.SelectFrom(It.IsAny<string>()))
                .Returns(firstBaseRace)
                .Returns(secondBaseRace);

            var baseRace = randomizer.Randomize(alignment, characterClass);
            Assert.That(baseRace, Is.EqualTo(secondBaseRace));
            mockPercentileResultSelector.Verify(p => p.SelectFrom(It.IsAny<string>()), Times.Exactly(2));
        }

        [Test]
        public void RandomizeReturnsDefaultBaseRace()
        {
            alignmentRaces.Remove(firstBaseRace);

            mockPercentileResultSelector.Setup(p => p.SelectFrom(It.IsAny<string>())).Returns(firstBaseRace);

            var baseRace = randomizer.Randomize(alignment, characterClass);
            Assert.That(baseRace, Is.EqualTo(secondBaseRace));
        }

        [Test]
        public void GetAllPossibleResultsGetsResultsFromSelector()
        {
            randomizer.GetAllPossible(alignment, characterClass);
            mockPercentileResultSelector.Verify(p => p.SelectAllFrom(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void GetAllPossibleResultsFiltersOutEmptyStrings()
        {
            var classNames = randomizer.GetAllPossible(alignment, characterClass);
            Assert.That(classNames, Contains.Item(firstBaseRace));
            Assert.That(classNames, Contains.Item(secondBaseRace));
            Assert.That(classNames.Count(), Is.EqualTo(2));
        }

        [Test]
        public void GetAllPossibleResultsAccessesTableAlignmentGoodnessClassNameBaseRaces()
        {
            characterClass.Name = "className";
            alignment.Goodness = "goodness";

            randomizer.GetAllPossible(alignment, characterClass);
            mockPercentileResultSelector.Verify(p => p.SelectAllFrom("goodnessclassNameBaseRaces"), Times.Once);
        }

        [Test]
        public void GetAllPossibleResultsFiltersOutUnallowedBaseRaces()
        {
            randomizer.ForbiddenBaseRace = firstBaseRace;

            var results = randomizer.GetAllPossible(alignment, characterClass);
            Assert.That(results, Contains.Item(secondBaseRace));
            Assert.That(results.Count(), Is.EqualTo(1));
        }

        [Test]
        public void GetAllPossibleResultsFiltersOutBaseRacesNotAllowedByAlignment()
        {
            alignmentRaces.Remove(firstBaseRace);

            var results = randomizer.GetAllPossible(alignment, characterClass);
            Assert.That(results, Contains.Item(secondBaseRace));
            Assert.That(results.Count(), Is.EqualTo(1));
        }

        private class TestBaseRaceRandomizer : BaseRaceRandomizerBase
        {
            public string ForbiddenBaseRace { get; set; }

            public TestBaseRaceRandomizer(IPercentileSelector percentileResultSelector, ICollectionSelector collectionSelector)
                : base(percentileResultSelector, new ConfigurableIterationGenerator(2), collectionSelector)
            { }

            protected override bool BaseRaceIsAllowedByRandomizer(string baseRace)
            {
                return baseRace != ForbiddenBaseRace;
            }
        }
    }
}