using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Tables;
using DnDGen.Core.Mappers.Collections;
using Ninject;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Feats.Data.CharacterClasses
{
    [TestFixture]
    public class CrossClassFeatDataTests : TableTests
    {
        protected override string tableName
        {
            get
            {
                return TableNameConstants.Set.Collection.ClassNameGroups;
            }
        }

        [Inject]
        internal CollectionMapper CollectionsMapper { get; set; }

        [Test]
        public void AllClassesHaveFeatDataTables()
        {
            var classNameGroups = CollectionsMapper.Map(TableNameConstants.Set.Collection.ClassNameGroups);
            var allClasses = classNameGroups[GroupConstants.All];

            foreach (var className in allClasses)
            {
                var tableName = string.Format(TableNameConstants.Formattable.Collection.CLASSFeatData, className);
                var featsData = CollectionsMapper.Map(tableName);

                Assert.That(featsData, Is.Not.Null);
            }
        }

        [Test]
        public void AllDomainsHaveFeatDataTables()
        {
            var domains = new[]
            {
                CharacterClassConstants.Domains.Air,
                CharacterClassConstants.Domains.Animal,
                CharacterClassConstants.Domains.Chaos,
                CharacterClassConstants.Domains.Death,
                CharacterClassConstants.Domains.Destruction,
                CharacterClassConstants.Domains.Earth,
                CharacterClassConstants.Domains.Evil,
                CharacterClassConstants.Domains.Fire,
                CharacterClassConstants.Domains.Good,
                CharacterClassConstants.Domains.Healing,
                CharacterClassConstants.Domains.Knowledge,
                CharacterClassConstants.Domains.Law,
                CharacterClassConstants.Domains.Luck,
                CharacterClassConstants.Domains.Magic,
                CharacterClassConstants.Domains.Plant,
                CharacterClassConstants.Domains.Protection,
                CharacterClassConstants.Domains.Strength,
                CharacterClassConstants.Domains.Sun,
                CharacterClassConstants.Domains.Travel,
                CharacterClassConstants.Domains.Trickery,
                CharacterClassConstants.Domains.War,
                CharacterClassConstants.Domains.Water,
            };

            foreach (var domain in domains)
            {
                var tableName = string.Format(TableNameConstants.Formattable.Collection.CLASSFeatData, domain);
                var featsData = CollectionsMapper.Map(tableName);

                Assert.That(featsData, Is.Not.Null);
            }
        }

        [Test]
        public void AllSchoolsHaveFeatDataTables()
        {
            var schools = new[]
            {
                CharacterClassConstants.Schools.Abjuration,
                CharacterClassConstants.Schools.Conjuration,
                CharacterClassConstants.Schools.Divination,
                CharacterClassConstants.Schools.Enchantment,
                CharacterClassConstants.Schools.Evocation,
                CharacterClassConstants.Schools.Illusion,
                CharacterClassConstants.Schools.Necromancy,
                CharacterClassConstants.Schools.Transmutation,
            };

            foreach (var school in schools)
            {
                var tableName = string.Format(TableNameConstants.Formattable.Collection.CLASSFeatData, school);
                var featsData = CollectionsMapper.Map(tableName);

                Assert.That(featsData, Is.Not.Null);
            }
        }
    }
}
