using CreatureGen.Alignments;
using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Generators.Randomizers.Races.Metaraces;
using CreatureGen.Creatures;
using CreatureGen.Randomizers.Races;
using NUnit.Framework;
using System.Linq;

namespace CreatureGen.Tests.Unit.Generators.Randomizers.Races.Metaraces
{
    [TestFixture]
    public class NoMetaraceRandomizerTests
    {
        private RaceRandomizer randomizer;
        private Alignment alignment;
        private CharacterClassPrototype characterClass;

        [SetUp]
        public void Setup()
        {
            randomizer = new NoMetaraceRandomizer();
            alignment = new Alignment();
            characterClass = new CharacterClassPrototype();
        }

        [Test]
        public void RandomizeAlwaysReturnsEmptyString()
        {
            var metarace = randomizer.Randomize(alignment, characterClass);
            Assert.That(metarace, Is.EqualTo(SizeConstants.Metaraces.None));
        }

        [Test]
        public void GetAllPossibleResultsReturnsEnumerableOfEmptyString()
        {
            var metaraces = randomizer.GetAllPossible(alignment, characterClass);
            Assert.That(metaraces.Single(), Is.EqualTo(SizeConstants.Metaraces.None));
        }
    }
}