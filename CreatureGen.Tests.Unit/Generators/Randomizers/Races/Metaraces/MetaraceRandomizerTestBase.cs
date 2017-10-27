using CreatureGen.Domain.Tables;
using CreatureGen.Randomizers.Races;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace CreatureGen.Tests.Unit.Generators.Randomizers.Races.Metaraces
{
    [TestFixture]
    public abstract class MetaraceRandomizerTestBase : RaceRandomizerTestBase
    {
        protected RaceRandomizer randomizer;
        protected abstract IEnumerable<string> metaraces { get; }

        [SetUp]
        public void MetaraceRandomizerTestBaseSetup()
        {
            mockPercentileSelector.Setup(s => s.SelectAllFrom(It.IsAny<string>())).Returns(metaraces);
            mockCollectionSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.MetaraceGroups, alignment.Full)).Returns(metaraces);
            mockCollectionSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.MetaraceGroups, characterClass.Name)).Returns(metaraces);
        }
    }
}