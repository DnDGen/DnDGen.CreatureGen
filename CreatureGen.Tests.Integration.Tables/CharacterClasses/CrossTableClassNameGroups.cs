using CreatureGen.Domain.Tables;
using CreatureGen.Magics;
using DnDGen.Core.Mappers.Collections;
using DnDGen.Core.Mappers.Percentiles;
using Ninject;
using NUnit.Framework;
using System.Linq;

namespace CreatureGen.Tests.Integration.Tables.CharacterClasses
{
    [TestFixture]
    public class CrossTableClassNameGroups : TableTests
    {
        [Inject]
        public CollectionMapper CollectionsMapper { get; set; }
        [Inject]
        public PercentileMapper PercentileMapper { get; set; }

        protected override string tableName
        {
            get
            {
                return TableNameConstants.Set.Collection.ClassNameGroups;
            }
        }

        [Test]
        public void AllClassesHaveHasSpecialistFieldTable()
        {
            var classGroups = CollectionsMapper.Map(TableNameConstants.Set.Collection.ClassNameGroups);

            foreach (var className in classGroups[GroupConstants.All])
            {
                var tableName = string.Format(TableNameConstants.Formattable.TrueOrFalse.CLASSHasSpecialistFields, className);
                var table = PercentileMapper.Map(tableName);
                Assert.That(table, Is.Not.Null);
                Assert.That(table, Is.Not.Empty);

                var indices = Enumerable.Range(1, 100);
                Assert.That(table.Keys, Is.EquivalentTo(indices));
                Assert.That(table.Values, Is.All.EqualTo(bool.TrueString).Or.EqualTo(bool.FalseString));
            }
        }

        [Test]
        public void AllArcaneSpellcasterClassesHaveKnowsAdditionalSpellsTable()
        {
            var classGroups = CollectionsMapper.Map(TableNameConstants.Set.Collection.ClassNameGroups);

            foreach (var className in classGroups[SpellConstants.Sources.Arcane])
            {
                var tableName = string.Format(TableNameConstants.Formattable.TrueOrFalse.CLASSKnowsAdditionalSpells, className);
                var table = PercentileMapper.Map(tableName);
                Assert.That(table, Is.Not.Null);
                Assert.That(table, Is.Not.Empty);

                var indices = Enumerable.Range(1, 100);
                Assert.That(table.Keys, Is.EquivalentTo(indices));
                Assert.That(table.Values, Is.All.EqualTo(bool.TrueString).Or.EqualTo(bool.FalseString));
            }
        }
    }
}
