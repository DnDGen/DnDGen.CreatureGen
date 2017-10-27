using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Tables;
using CreatureGen.Magics;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Magics.Spells.Known.Clerics
{
    [TestFixture]
    public class PlantSpellLevelsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get
            {
                return string.Format(TableNameConstants.Formattable.Adjustments.CLASSSpellLevels, CharacterClassConstants.Domains.Plant);
            }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                SpellConstants.Entangle,
                SpellConstants.Barkskin,
                SpellConstants.PlantGrowth,
                SpellConstants.CommandPlants,
                SpellConstants.WallOfThorns,
                SpellConstants.RepelWood,
                SpellConstants.AnimatePlants,
                SpellConstants.ControlPlants,
                SpellConstants.Shambler
            };

            AssertCollectionNames(names);
        }

        [Test]
        public void AllPlantSpellsInAdjustmentsTable()
        {
            var spellGroups = CollectionsMapper.Map(TableNameConstants.Set.Collection.SpellGroups);
            AssertCollectionNames(spellGroups[CharacterClassConstants.Domains.Plant]);
        }

        [TestCase(SpellConstants.Entangle, 1)]
        [TestCase(SpellConstants.Barkskin, 2)]
        [TestCase(SpellConstants.PlantGrowth, 3)]
        [TestCase(SpellConstants.CommandPlants, 4)]
        [TestCase(SpellConstants.WallOfThorns, 5)]
        [TestCase(SpellConstants.RepelWood, 6)]
        [TestCase(SpellConstants.AnimatePlants, 7)]
        [TestCase(SpellConstants.ControlPlants, 8)]
        [TestCase(SpellConstants.Shambler, 9)]
        public override void Adjustment(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
