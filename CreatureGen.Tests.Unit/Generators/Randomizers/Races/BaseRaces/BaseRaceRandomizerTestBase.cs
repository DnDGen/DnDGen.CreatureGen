using CreatureGen.Domain.Tables;
using CreatureGen.Randomizers.Races;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace CreatureGen.Tests.Unit.Generators.Randomizers.Races.BaseRaces
{
    [TestFixture]
    public abstract class BaseRaceRandomizerTestBase : RaceRandomizerTestBase
    {
        protected RaceRandomizer randomizer;
        protected abstract IEnumerable<string> baseRaces { get; }

        [SetUp]
        public void BaseRaceRandomizerTestBaseSetup()
        {
            mockPercentileSelector.Setup(p => p.SelectAllFrom(It.IsAny<string>())).Returns(baseRaces);
            mockCollectionSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, alignment.Full)).Returns(baseRaces);
        }
    }
}