using CreatureGen.Domain.Tables;
using DnDGen.Core.Mappers.Collections;
using Ninject;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Races.BaseRaces.Ages
{
    [TestFixture]
    public class CrossBaseRaceAgesTests : TableTests
    {
        [Inject]
        public CollectionMapper CollectionsMapper { get; set; }

        protected override string tableName
        {
            get
            {
                return TableNameConstants.Set.Collection.BaseRaceGroups;
            }
        }

        [Test]
        public void AllBaseRacesHaveAgeTables()
        {
            var baseRaceGroups = CollectionsMapper.Map(TableNameConstants.Set.Collection.BaseRaceGroups);
            var allBaseRaces = baseRaceGroups[GroupConstants.All];

            foreach (var baseRace in allBaseRaces)
            {
                var tableName = string.Format(TableNameConstants.Formattable.Adjustments.RACEAges, baseRace);
                var ages = CollectionsMapper.Map(tableName);

                Assert.That(ages, Is.Not.Null);
                Assert.That(ages, Is.Not.Empty);
            }
        }
    }
}