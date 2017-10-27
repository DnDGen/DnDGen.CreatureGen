using CreatureGen.Alignments;
using CreatureGen.CharacterClasses;
using DnDGen.Core.Generators;
using DnDGen.Core.Selectors.Collections;
using DnDGen.Core.Selectors.Percentiles;
using Moq;
using NUnit.Framework;
using System;

namespace CreatureGen.Tests.Unit.Generators.Randomizers.Races
{
    [TestFixture]
    public abstract class RaceRandomizerTestBase
    {
        internal Mock<IPercentileSelector> mockPercentileSelector;
        internal Mock<ICollectionSelector> mockCollectionSelector;
        internal Generator generator;
        protected Alignment alignment;
        protected CharacterClassPrototype characterClass;

        [SetUp]
        public void RaceRandomizerTestBaseSetup()
        {
            mockPercentileSelector = new Mock<IPercentileSelector>();
            mockCollectionSelector = new Mock<ICollectionSelector>();
            generator = new ConfigurableIterationGenerator();
            characterClass = new CharacterClassPrototype();
            alignment = new Alignment();

            alignment.Goodness = Guid.NewGuid().ToString();
            alignment.Lawfulness = Guid.NewGuid().ToString();
            characterClass.Level = 1;
            characterClass.Name = Guid.NewGuid().ToString();
        }
    }
}