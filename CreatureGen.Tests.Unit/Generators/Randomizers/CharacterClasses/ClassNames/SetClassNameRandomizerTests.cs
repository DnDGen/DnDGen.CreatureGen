using CreatureGen.Alignments;
using CreatureGen.Domain.Generators.Randomizers.CharacterClasses.ClassNames;
using CreatureGen.Domain.Tables;
using CreatureGen.Randomizers.CharacterClasses;
using CreatureGen.Verifiers.Exceptions;
using DnDGen.Core.Selectors.Collections;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace CreatureGen.Tests.Unit.Generators.Randomizers.CharacterClasses.ClassNames
{
    [TestFixture]
    public class SetClassNameRandomizerTests
    {
        private ISetClassNameRandomizer randomizer;
        private Mock<ICollectionSelector> mockCollectionsSelector;
        private Alignment alignment;
        private List<string> alignmentClasses;

        [SetUp]
        public void Setup()
        {
            mockCollectionsSelector = new Mock<ICollectionSelector>();
            randomizer = new SetClassNameRandomizer(mockCollectionsSelector.Object);
            alignment = new Alignment();
            alignmentClasses = new List<string>();

            alignment.Goodness = "goodness";
            alignment.Lawfulness = "lawfulness";

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, alignment.Full)).Returns(alignmentClasses);
        }

        [Test]
        public void SetClassNameRandomizerIsAClassNameRandomizer()
        {
            Assert.That(randomizer, Is.InstanceOf<IClassNameRandomizer>());
        }

        [Test]
        public void ReturnSetClassName()
        {
            randomizer.SetClassName = "class name";
            alignmentClasses.Add("other class name");
            alignmentClasses.Add("class name");

            var className = randomizer.Randomize(alignment);
            Assert.That(className, Is.EqualTo("class name"));
        }

        [Test]
        public void ReturnJustSetClassName()
        {
            randomizer.SetClassName = "class name";
            alignmentClasses.Add("other class name");
            alignmentClasses.Add("class name");

            var classNames = randomizer.GetAllPossibleResults(alignment);
            Assert.That(classNames, Contains.Item("class name"));
            Assert.That(classNames.Count(), Is.EqualTo(1));
        }

        [Test]
        public void ThrowExceptionIfAlignmentDoesNotMatchClass()
        {
            randomizer.SetClassName = "class name";
            alignmentClasses.Add("other class name");

            Assert.That(() => randomizer.Randomize(alignment), Throws.InstanceOf<IncompatibleRandomizersException>());
        }

        [Test]
        public void ReturnEmptyCollectionIfAlignmentDoesNotMatchClass()
        {
            randomizer.SetClassName = "class name";
            alignmentClasses.Add("other class name");

            var classNames = randomizer.GetAllPossibleResults(alignment);
            Assert.That(classNames, Is.Empty);
        }
    }
}