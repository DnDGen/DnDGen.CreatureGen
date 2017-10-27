using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Tables;
using CreatureGen.Magics;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Magics.Spells.Known.Clerics
{
    [TestFixture]
    public class SunSpellLevelsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get
            {
                return string.Format(TableNameConstants.Formattable.Adjustments.CLASSSpellLevels, CharacterClassConstants.Domains.Sun);
            }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                SpellConstants.EndureElements,
                SpellConstants.HeatMetal,
                SpellConstants.SearingLight,
                SpellConstants.FireShield,
                SpellConstants.FlameStrike,
                SpellConstants.FireSeeds,
                SpellConstants.Sunbeam,
                SpellConstants.Sunburst,
                SpellConstants.PrismaticSphere
            };

            AssertCollectionNames(names);
        }

        [Test]
        public void AllSunSpellsInAdjustmentsTable()
        {
            var spellGroups = CollectionsMapper.Map(TableNameConstants.Set.Collection.SpellGroups);
            AssertCollectionNames(spellGroups[CharacterClassConstants.Domains.Sun]);
        }

        [TestCase(SpellConstants.EndureElements, 1)]
        [TestCase(SpellConstants.HeatMetal, 2)]
        [TestCase(SpellConstants.SearingLight, 3)]
        [TestCase(SpellConstants.FireShield, 4)]
        [TestCase(SpellConstants.FlameStrike, 5)]
        [TestCase(SpellConstants.FireSeeds, 6)]
        [TestCase(SpellConstants.Sunbeam, 7)]
        [TestCase(SpellConstants.Sunburst, 8)]
        [TestCase(SpellConstants.PrismaticSphere, 9)]
        public override void Adjustment(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
