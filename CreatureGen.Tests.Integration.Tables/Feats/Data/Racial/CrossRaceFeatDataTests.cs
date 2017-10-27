using CreatureGen.Domain.Tables;
using CreatureGen.Creatures;
using DnDGen.Core.Mappers.Collections;
using Ninject;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Feats.Data.Racial
{
    [TestFixture]
    public class CrossRaceFeatDataTests : TableTests
    {
        protected override string tableName
        {
            get
            {
                return TableNameConstants.Set.Collection.BaseRaceGroups;
            }
        }

        [Inject]
        internal CollectionMapper CollectionsMapper { get; set; }

        [Test]
        public void AllBaseRacesHaveFeatDataTables()
        {
            var baseRaceGroups = CollectionsMapper.Map(TableNameConstants.Set.Collection.BaseRaceGroups);
            var allBaseRaces = baseRaceGroups[GroupConstants.All];

            foreach (var baseRace in allBaseRaces)
            {
                var tableName = string.Format(TableNameConstants.Formattable.Collection.RACEFeatData, baseRace);
                var featsData = CollectionsMapper.Map(tableName);

                Assert.That(featsData, Is.Not.Null);
            }
        }

        [Test]
        public void AllMetaracesHaveFeatDataTables()
        {
            var metaraceGroups = CollectionsMapper.Map(TableNameConstants.Set.Collection.MetaraceGroups);
            var allMetaraces = metaraceGroups[GroupConstants.All];

            foreach (var metarRace in allMetaraces)
            {
                var tableName = string.Format(TableNameConstants.Formattable.Collection.RACEFeatData, metarRace);
                var featsData = CollectionsMapper.Map(tableName);

                Assert.That(featsData, Is.Not.Null);
            }
        }

        [Test]
        public void AllMetaraceSpeciesHaveFeatDataTables()
        {
            var metaraceSpecies = new[]
            {
                SizeConstants.Metaraces.Species.Black,
                SizeConstants.Metaraces.Species.Blue,
                SizeConstants.Metaraces.Species.Brass,
                SizeConstants.Metaraces.Species.Bronze,
                SizeConstants.Metaraces.Species.Copper,
                SizeConstants.Metaraces.Species.Gold,
                SizeConstants.Metaraces.Species.Green,
                SizeConstants.Metaraces.Species.Red,
                SizeConstants.Metaraces.Species.Silver,
                SizeConstants.Metaraces.Species.White,
            };

            foreach (var species in metaraceSpecies)
            {
                var tableName = string.Format(TableNameConstants.Formattable.Collection.RACEFeatData, species);
                var featsData = CollectionsMapper.Map(tableName);

                Assert.That(featsData, Is.Not.Null);
            }
        }
    }
}
