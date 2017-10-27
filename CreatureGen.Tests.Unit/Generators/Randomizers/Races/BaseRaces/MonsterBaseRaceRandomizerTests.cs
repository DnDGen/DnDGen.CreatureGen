using CreatureGen.Domain.Generators.Randomizers.Races.BaseRaces;
using CreatureGen.Domain.Tables;
using NUnit.Framework;
using System.Collections.Generic;

namespace CreatureGen.Tests.Unit.Generators.Randomizers.Races.BaseRaces
{
    [TestFixture]
    public class MonsterBaseRaceRandomizerTests : BaseRaceRandomizerTestBase
    {
        protected override IEnumerable<string> baseRaces
        {
            get
            {
                return new[]
                {
                    "base race",
                    "standard base race",
                    "non-standard base race",
                    "monster base race",
                    "aquatic base race",
                };
            }
        }

        [SetUp]
        public void Setup()
        {
            randomizer = new MonsterBaseRaceRandomizer(mockPercentileSelector.Object, mockCollectionSelector.Object, generator);

            mockCollectionSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, GroupConstants.Monsters))
                .Returns(new[] { "monster base race", "other base race", "base race" });
        }

        [Test]
        public void OnlyMonsterBaseRacesAllowed()
        {
            var baseRaces = randomizer.GetAllPossible(alignment, characterClass);
            Assert.That(baseRaces, Is.EquivalentTo(new[] { "monster base race", "base race" }));
        }
    }
}