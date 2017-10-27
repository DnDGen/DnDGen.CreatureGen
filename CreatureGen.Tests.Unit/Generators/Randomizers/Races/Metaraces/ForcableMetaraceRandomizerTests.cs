using CreatureGen.Alignments;
using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Generators.Randomizers.Races.Metaraces;
using CreatureGen.Domain.Tables;
using CreatureGen.Creatures;
using CreatureGen.Verifiers.Exceptions;
using DnDGen.Core.Selectors.Collections;
using DnDGen.Core.Selectors.Percentiles;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CreatureGen.Tests.Unit.Generators.Randomizers.Races.Metaraces
{
    [TestFixture]
    public class ForcableMetaraceRandomizerTests
    {
        private TestForcableMetaraceRandomizer forcableMetaraceRandomizer;
        private Mock<IPercentileSelector> mockPercentileSelector;
        private Mock<ICollectionSelector> mockCollectionsSelector;

        private string firstMetarace = "first metarace";
        private string secondMetarace = "second metarace";
        private CharacterClassPrototype characterClass;
        private Alignment alignment;
        private List<string> alignmentRaces;
        private List<string> classRaces;
        private List<string> metaraces;

        [SetUp]
        public void Setup()
        {
            mockPercentileSelector = new Mock<IPercentileSelector>();
            mockCollectionsSelector = new Mock<ICollectionSelector>();
            forcableMetaraceRandomizer = new TestForcableMetaraceRandomizer(mockPercentileSelector.Object, mockCollectionsSelector.Object);

            alignment = new Alignment();
            characterClass = new CharacterClassPrototype();
            alignmentRaces = new List<string>();
            classRaces = new List<string>();

            metaraces = new List<string> { firstMetarace, secondMetarace, SizeConstants.Metaraces.None };

            alignment.Goodness = Guid.NewGuid().ToString();
            alignment.Lawfulness = Guid.NewGuid().ToString();
            characterClass.Level = 1;
            characterClass.Name = Guid.NewGuid().ToString();

            alignmentRaces.Add(firstMetarace);
            alignmentRaces.Add(secondMetarace);
            classRaces.Add(firstMetarace);
            classRaces.Add(secondMetarace);

            var tableName = string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSMetaraces, alignment.Goodness, characterClass.Name);
            mockPercentileSelector.Setup(s => s.SelectAllFrom(tableName)).Returns(metaraces);
            mockPercentileSelector.Setup(s => s.SelectFrom(tableName)).Returns(firstMetarace);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.MetaraceGroups, alignment.Full)).Returns(alignmentRaces);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.MetaraceGroups, characterClass.Name)).Returns(classRaces);


            var index = 0;
            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>())).Returns((IEnumerable<string> ss) => ss.ElementAt(index++ % ss.Count()));
        }

        [Test]
        public void RandomizeThrowsErrorIfNoPossibleResults()
        {
            metaraces.Clear();
            Assert.That(() => forcableMetaraceRandomizer.Randomize(alignment, characterClass), Throws.InstanceOf<IncompatibleRandomizersException>());
        }

        [Test]
        public void RandomizeReturnsMetaraceFromSelector()
        {
            var result = forcableMetaraceRandomizer.Randomize(alignment, characterClass);
            Assert.That(result, Is.EqualTo(firstMetarace));
        }

        [Test]
        public void RandomizeRerollsNoMetarace()
        {
            mockPercentileSelector.SetupSequence(p => p.SelectFrom(It.IsAny<string>()))
                .Returns(SizeConstants.Metaraces.None)
                .Returns(firstMetarace);

            forcableMetaraceRandomizer.ForceMetarace = true;

            var metarace = forcableMetaraceRandomizer.Randomize(alignment, characterClass);
            Assert.That(metarace, Is.EqualTo(firstMetarace));
            mockPercentileSelector.Verify(p => p.SelectFrom(It.IsAny<string>()), Times.Exactly(2));
        }

        [Test]
        public void RandomizeDoesNotRerollNoMetarace()
        {
            mockPercentileSelector.SetupSequence(p => p.SelectFrom(It.IsAny<string>()))
                .Returns(SizeConstants.Metaraces.None)
                .Returns(firstMetarace);

            forcableMetaraceRandomizer.ForceMetarace = false;

            var metarace = forcableMetaraceRandomizer.Randomize(alignment, characterClass);
            Assert.That(metarace, Is.EqualTo(SizeConstants.Metaraces.None));
            mockPercentileSelector.Verify(p => p.SelectFrom(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void RandomizeRerollsInvalidMetaraceBecauseNotAllowed()
        {
            forcableMetaraceRandomizer.ForbiddenMetarace = firstMetarace;

            mockPercentileSelector.SetupSequence(p => p.SelectFrom(It.IsAny<string>()))
                .Returns(firstMetarace)
                .Returns(secondMetarace);

            var metarace = forcableMetaraceRandomizer.Randomize(alignment, characterClass);
            Assert.That(metarace, Is.EqualTo(secondMetarace));
            mockPercentileSelector.Verify(p => p.SelectFrom(It.IsAny<string>()), Times.Exactly(2));
        }

        [Test]
        public void RandomizeRerollsInvalidMetaraceBecauseAlignmentDoesNotAllow()
        {
            alignmentRaces.Remove(firstMetarace);

            mockPercentileSelector.SetupSequence(p => p.SelectFrom(It.IsAny<string>()))
                .Returns(firstMetarace)
                .Returns(secondMetarace);

            var metarace = forcableMetaraceRandomizer.Randomize(alignment, characterClass);
            Assert.That(metarace, Is.EqualTo(secondMetarace));
            mockPercentileSelector.Verify(p => p.SelectFrom(It.IsAny<string>()), Times.Exactly(2));
        }

        [Test]
        public void RandomizeRerollsInvalidMetaraceBecauseClassDoesNotAllow()
        {
            classRaces.Remove(firstMetarace);

            mockPercentileSelector.SetupSequence(p => p.SelectFrom(It.IsAny<string>()))
                .Returns(firstMetarace)
                .Returns(secondMetarace);

            var metarace = forcableMetaraceRandomizer.Randomize(alignment, characterClass);
            Assert.That(metarace, Is.EqualTo(secondMetarace));
            mockPercentileSelector.Verify(p => p.SelectFrom(It.IsAny<string>()), Times.Exactly(2));
        }

        [Test]
        public void RandomizeReturnsNoMetarace()
        {
            classRaces.Remove(firstMetarace);

            mockPercentileSelector.Setup(p => p.SelectFrom(It.IsAny<string>())).Returns(firstMetarace);

            var metarace = forcableMetaraceRandomizer.Randomize(alignment, characterClass);
            Assert.That(metarace, Is.EqualTo(SizeConstants.Metaraces.None));
        }

        [Test]
        public void RandomizeReturnsForcedMetarace()
        {
            classRaces.Remove(firstMetarace);
            forcableMetaraceRandomizer.ForceMetarace = true;

            mockPercentileSelector.Setup(p => p.SelectFrom(It.IsAny<string>())).Returns(firstMetarace);

            var metarace = forcableMetaraceRandomizer.Randomize(alignment, characterClass);
            Assert.That(metarace, Is.EqualTo(secondMetarace));
        }

        [Test]
        public void GetAllPossibleResultsGetsNonEmptyResults()
        {
            var races = forcableMetaraceRandomizer.GetAllPossible(alignment, characterClass);

            Assert.That(races, Contains.Item(firstMetarace));
            Assert.That(races, Contains.Item(secondMetarace));
        }

        [Test]
        public void GetAllPossibleResultsFiltersOutUnallowedBaseRaces()
        {
            forcableMetaraceRandomizer.ForbiddenMetarace = firstMetarace;
            var results = forcableMetaraceRandomizer.GetAllPossible(alignment, characterClass);

            Assert.That(results, Contains.Item(secondMetarace));
            Assert.That(results, Is.All.Not.EqualTo(firstMetarace));
        }

        [Test]
        public void IfForceMetaraceIsTrueThenEmptyMetaraceIsNotAllowed()
        {
            forcableMetaraceRandomizer.ForceMetarace = true;
            var results = forcableMetaraceRandomizer.GetAllPossible(alignment, characterClass);
            Assert.That(results, Is.All.Not.EqualTo(SizeConstants.Metaraces.None));
        }

        [Test]
        public void IfForceMetaraceIsFalseThenEmptyMetaraceIsAllowed()
        {
            forcableMetaraceRandomizer.ForceMetarace = false;
            var results = forcableMetaraceRandomizer.GetAllPossible(alignment, characterClass);
            Assert.That(results, Contains.Item(SizeConstants.Metaraces.None));
        }

        [Test]
        public void GetAllPossibleResultsFiltersOutMetaracesNotAllowedByAlignment()
        {
            alignmentRaces.Remove(firstMetarace);

            var results = forcableMetaraceRandomizer.GetAllPossible(alignment, characterClass);
            Assert.That(results, Contains.Item(secondMetarace));
            Assert.That(results, Is.All.Not.EqualTo(firstMetarace));
        }

        [Test]
        public void GetAllPossibleResultsFiltersOutMetaracesNotAllowedByClass()
        {
            classRaces.Remove(firstMetarace);

            var results = forcableMetaraceRandomizer.GetAllPossible(alignment, characterClass);
            Assert.That(results, Contains.Item(secondMetarace));
            Assert.That(results, Is.All.Not.EqualTo(firstMetarace));
        }

        private class TestForcableMetaraceRandomizer : ForcableMetaraceBase
        {
            public string ForbiddenMetarace { get; set; }

            public TestForcableMetaraceRandomizer(IPercentileSelector percentileResultSelector, ICollectionSelector collectionSelector)
                : base(percentileResultSelector, new ConfigurableIterationGenerator(2), collectionSelector)
            { }

            protected override bool MetaraceIsAllowed(string metarace)
            {
                return metarace != ForbiddenMetarace;
            }
        }
    }
}