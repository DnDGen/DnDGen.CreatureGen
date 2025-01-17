using DnDGen.CreatureGen.Tables;
using DnDGen.CreatureGen.Tests.Integration.TestData;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Integration.Tables.Creatures.Appearances
{
    [TestFixture]
    internal class SkinAppearancesTests : AppearancesTests
    {
        protected override string tableName =>
            TableNameConstants.Collection.Appearances(TableNameConstants.Collection.AppearanceCategories.Skin);

        [Test]
        public void SkinAppearancesNames()
        {
            var creatureKeys = GetCollectionCreatureKeys();
            var names = new List<string>();

            foreach (var creatureKey in creatureKeys)
            {
                names.Add(creatureKey + Rarity.Common.ToString());
                names.Add(creatureKey + Rarity.Uncommon.ToString());
                names.Add(creatureKey + Rarity.Rare.ToString());
            }

            AssertCollectionNames(names);
        }

        [TestCaseSource(typeof(CreatureTestData), nameof(CreatureTestData.Creatures))]
        public void CreatureSkinAppearances(string creature)
        {
            var genders = collectionSelector.SelectFrom(Config.Name, TableNameConstants.Collection.Genders, creature);
            var creatureKeys = GetCollectionCreatureKeys();
            var keys = genders
                .Select(g => creature + g)
                .Concat([creature])
                .Intersect(creatureKeys);

            foreach (var key in keys)
            {
                AssertCreatureAppearance(TableNameConstants.Collection.AppearanceCategories.Skin, key);
            }
        }

        [TestCaseSource(typeof(CreatureTestData), nameof(CreatureTestData.Templates))]
        public void TemplateSkinAppearances(string template)
        {
            var creatureKeys = GetCollectionCreatureKeys();
            Assert.That(creatureKeys, Contains.Item(template));
            AssertCreatureAppearance(TableNameConstants.Collection.AppearanceCategories.Skin, template);
        }

        [Test]
        [Ignore("Don't run this unless you need to bulk update the appearances file")]
        public void DEBUG_WriteXml()
        {
            WriteXml(TableNameConstants.Collection.AppearanceCategories.Skin);
        }

        [Test]
        public void NoAppearancesIncludeTODO()
        {
            AssertNoAppearancesIncludeTODO(TableNameConstants.Collection.AppearanceCategories.Skin);
        }
    }
}
