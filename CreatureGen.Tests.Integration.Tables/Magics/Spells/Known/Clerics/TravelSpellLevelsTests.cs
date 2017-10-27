using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Tables;
using CreatureGen.Magics;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Magics.Spells.Known.Clerics
{
    [TestFixture]
    public class TravelSpellLevelsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get
            {
                return string.Format(TableNameConstants.Formattable.Adjustments.CLASSSpellLevels, CharacterClassConstants.Domains.Travel);
            }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                SpellConstants.Longstrider,
                SpellConstants.LocateObject,
                SpellConstants.Fly,
                SpellConstants.DimensionDoor,
                SpellConstants.Teleport,
                SpellConstants.FindThePath,
                SpellConstants.Teleport_Greater,
                SpellConstants.PhaseDoor,
                SpellConstants.AstralProjection
            };

            AssertCollectionNames(names);
        }

        [Test]
        public void AllTravelSpellsInAdjustmentsTable()
        {
            var spellGroups = CollectionsMapper.Map(TableNameConstants.Set.Collection.SpellGroups);
            AssertCollectionNames(spellGroups[CharacterClassConstants.Domains.Travel]);
        }

        [TestCase(SpellConstants.Longstrider, 1)]
        [TestCase(SpellConstants.LocateObject, 2)]
        [TestCase(SpellConstants.Fly, 3)]
        [TestCase(SpellConstants.DimensionDoor, 4)]
        [TestCase(SpellConstants.Teleport, 5)]
        [TestCase(SpellConstants.FindThePath, 6)]
        [TestCase(SpellConstants.Teleport_Greater, 7)]
        [TestCase(SpellConstants.PhaseDoor, 8)]
        [TestCase(SpellConstants.AstralProjection, 9)]
        public override void Adjustment(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
