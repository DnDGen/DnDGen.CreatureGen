using CreatureGen.Alignments;
using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Generators.Randomizers.Races.Metaraces;
using CreatureGen.Domain.Tables;
using CreatureGen.Randomizers.Races;
using CreatureGen.Verifiers.Exceptions;
using DnDGen.Core.Selectors.Collections;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace CreatureGen.Tests.Unit.Generators.Randomizers.Races.Metaraces
{
    [TestFixture]
    public class SetMetaraceRandomizerTests
    {
        private ISetMetaraceRandomizer randomizer;
        private CharacterClassPrototype characterClass;
        private Alignment alignment;
        private Mock<ICollectionSelector> mockCollectionsSelector;
        private List<string> alignmentMetaraces;
        private List<string> classMetaraces;

        [SetUp]
        public void Setup()
        {
            mockCollectionsSelector = new Mock<ICollectionSelector>();
            randomizer = new SetMetaraceRandomizer(mockCollectionsSelector.Object);
            characterClass = new CharacterClassPrototype();
            alignment = new Alignment();
            alignmentMetaraces = new List<string>();
            classMetaraces = new List<string>();

            alignment.Goodness = "goodness";
            alignment.Lawfulness = "lawfulness";
            characterClass.Name = "class name";

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.MetaraceGroups, alignment.Full)).Returns(alignmentMetaraces);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.MetaraceGroups, characterClass.Name)).Returns(classMetaraces);
        }

        [Test]
        public void SetMetaraceRandomizerIsAMetaraceRandomizer()
        {
            Assert.That(randomizer, Is.InstanceOf<RaceRandomizer>());
        }

        [Test]
        public void ReturnSetMetarace()
        {
            randomizer.SetMetarace = "metarace";
            alignmentMetaraces.Add("other metarace");
            alignmentMetaraces.Add("metarace");
            classMetaraces.Add("other metarace");
            classMetaraces.Add("metarace");

            var metarace = randomizer.Randomize(alignment, characterClass);
            Assert.That(metarace, Is.EqualTo("metarace"));
        }

        [Test]
        public void ReturnJustSetMetarace()
        {
            randomizer.SetMetarace = "metarace";
            alignmentMetaraces.Add("other metarace");
            alignmentMetaraces.Add("metarace");
            classMetaraces.Add("other metarace");
            classMetaraces.Add("metarace");

            var metaraces = randomizer.GetAllPossible(alignment, characterClass);
            Assert.That(metaraces, Contains.Item("metarace"));
            Assert.That(metaraces.Count(), Is.EqualTo(1));
        }

        [Test]
        public void ThrowExceptionIfBaseRaceDoesNotMatchAlignment()
        {
            randomizer.SetMetarace = "metarace";
            alignmentMetaraces.Add("other metarace");
            classMetaraces.Add("other metarace");
            classMetaraces.Add("metarace");

            Assert.That(() => randomizer.Randomize(alignment, characterClass), Throws.InstanceOf<IncompatibleRandomizersException>());
        }

        [Test]
        public void ThrowExceptionIfBaseRaceDoesNotMatchClassName()
        {
            randomizer.SetMetarace = "metarace";
            alignmentMetaraces.Add("other metarace");
            alignmentMetaraces.Add("metarace");
            classMetaraces.Add("other metarace");

            Assert.That(() => randomizer.Randomize(alignment, characterClass), Throws.InstanceOf<IncompatibleRandomizersException>());
        }

        [Test]
        public void ReturnEmptyIfBaseRaceDoesNotMatchAlignment()
        {
            randomizer.SetMetarace = "metarace";
            alignmentMetaraces.Add("other metarace");
            classMetaraces.Add("other metarace");
            classMetaraces.Add("metarace");

            var baseRaces = randomizer.GetAllPossible(alignment, characterClass);
            Assert.That(baseRaces, Is.Empty);
        }

        [Test]
        public void ReturnEmptyIfBaseRaceDoesNotMatchClassName()
        {
            randomizer.SetMetarace = "metarace";
            alignmentMetaraces.Add("other metarace");
            alignmentMetaraces.Add("metarace");
            classMetaraces.Add("other metarace");

            var baseRaces = randomizer.GetAllPossible(alignment, characterClass);
            Assert.That(baseRaces, Is.Empty);
        }
    }
}