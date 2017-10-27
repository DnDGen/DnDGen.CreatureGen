using CreatureGen.Alignments;
using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Generators.Randomizers.Races.BaseRaces;
using CreatureGen.Domain.Tables;
using CreatureGen.Randomizers.Races;
using CreatureGen.Verifiers.Exceptions;
using DnDGen.Core.Selectors.Collections;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace CreatureGen.Tests.Unit.Generators.Randomizers.Races.BaseRaces
{
    [TestFixture]
    public class SetBaseRaceRandomizerTests
    {
        private ISetBaseRaceRandomizer randomizer;
        private CharacterClassPrototype characterClass;
        private Alignment alignment;
        private Mock<ICollectionSelector> mockCollectionsSelector;
        private List<string> alignmentBaseRaces;

        [SetUp]
        public void Setup()
        {
            mockCollectionsSelector = new Mock<ICollectionSelector>();
            randomizer = new SetBaseRaceRandomizer(mockCollectionsSelector.Object);
            characterClass = new CharacterClassPrototype();
            alignment = new Alignment();
            alignmentBaseRaces = new List<string>();

            alignment.Goodness = "goodness";
            alignment.Lawfulness = "lawfulness";
            characterClass.Name = "class name";

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, alignment.Full)).Returns(alignmentBaseRaces);

        }

        [Test]
        public void SetBaseRaceRandomizerIsABaseRaceRandomizer()
        {
            Assert.That(randomizer, Is.InstanceOf<RaceRandomizer>());
        }

        [Test]
        public void ReturnSetBaseRace()
        {
            randomizer.SetBaseRace = "base race";
            alignmentBaseRaces.Add("other base race");
            alignmentBaseRaces.Add("base race");

            var baseRace = randomizer.Randomize(alignment, characterClass);
            Assert.That(baseRace, Is.EqualTo("base race"));
        }

        [Test]
        public void ReturnJustSetBaseRace()
        {
            randomizer.SetBaseRace = "base race";
            alignmentBaseRaces.Add("other base race");
            alignmentBaseRaces.Add("base race");

            var baseRaces = randomizer.GetAllPossible(alignment, characterClass);
            Assert.That(baseRaces, Contains.Item("base race"));
            Assert.That(baseRaces.Count(), Is.EqualTo(1));
        }

        [Test]
        public void ThrowExceptionIfBaseRaceDoesNotMatchAlignment()
        {
            randomizer.SetBaseRace = "base race";
            alignmentBaseRaces.Add("other base race");

            Assert.That(() => randomizer.Randomize(alignment, characterClass), Throws.InstanceOf<IncompatibleRandomizersException>());
        }

        [Test]
        public void ReturnEmptyIfBaseRaceDoesNotMatchAlignment()
        {
            randomizer.SetBaseRace = "base race";
            alignmentBaseRaces.Add("other base race");

            var baseRaces = randomizer.GetAllPossible(alignment, characterClass);
            Assert.That(baseRaces, Is.Empty);
        }
    }
}