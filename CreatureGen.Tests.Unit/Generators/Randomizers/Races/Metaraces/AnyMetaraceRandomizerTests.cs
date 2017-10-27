using CreatureGen.Domain.Generators.Randomizers.Races.Metaraces;
using CreatureGen.Creatures;
using CreatureGen.Randomizers.Races;
using NUnit.Framework;
using System.Collections.Generic;

namespace CreatureGen.Tests.Unit.Generators.Randomizers.Races.Metaraces
{
    [TestFixture]
    public class AnyMetaraceRandomizerTests : MetaraceRandomizerTestBase
    {
        protected override IEnumerable<string> metaraces
        {
            get
            {
                return new[] {
                    "genetic metarace",
                    "lycanthrope metarace",
                    "undead metarace",
                    SizeConstants.Metaraces.None,
                };
            }
        }

        [SetUp]
        public void Setup()
        {
            randomizer = new AnyMetaraceRandomizer(mockPercentileSelector.Object, generator, mockCollectionSelector.Object);
        }

        [Test]
        public void AllMetaracesAllowed()
        {
            (randomizer as IForcableMetaraceRandomizer).ForceMetarace = false;
            var allMetaraces = randomizer.GetAllPossible(alignment, characterClass);
            Assert.That(allMetaraces, Is.EquivalentTo(metaraces));
        }

        [Test]
        public void OnlyMetaracesAllowed()
        {
            (randomizer as IForcableMetaraceRandomizer).ForceMetarace = true;
            var metaraces = randomizer.GetAllPossible(alignment, characterClass);
            Assert.That(metaraces, Is.EquivalentTo(new[] { "genetic metarace", "lycanthrope metarace", "undead metarace" }));
        }
    }
}