using CreatureGen.Domain.Tables;
using DnDGen.Core.Mappers.Percentiles;
using Ninject;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace CreatureGen.Tests.Integration.Tables.Items.Powers
{
    [TestFixture]
    public class CrossPowersTests : TableTests
    {
        [Inject]
        public PercentileMapper PercentileMapper { get; set; }

        protected override string tableName
        {
            get
            {
                return string.Format(TableNameConstants.Formattable.Percentile.LevelXPower, 1);
            }
        }

        private IEnumerable<int> levels;
        private IEnumerable<int> indices;

        [SetUp]
        public void Setup()
        {
            levels = Enumerable.Range(1, 30);
            indices = Enumerable.Range(1, 100);
        }

        [Test]
        public void PowerTableExistsForAllLevels()
        {
            foreach (var level in levels)
            {
                AssertTable(level);
            }
        }

        private void AssertTable(int level)
        {
            var tableName = string.Format(TableNameConstants.Formattable.Percentile.LevelXPower, level);
            var table = PercentileMapper.Map(tableName);

            Assert.That(table, Is.Not.Null);
            Assert.That(table.Keys, Is.EquivalentTo(indices));
            Assert.That(table.Values, Is.All.Not.Null);
            Assert.That(table.Values, Is.All.Not.Empty);
        }
    }
}
