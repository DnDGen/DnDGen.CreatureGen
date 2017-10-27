using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Tables;
using CreatureGen.Magics;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Magics.Spells.Known.Clerics
{
    [TestFixture]
    public class TrickerySpellLevelsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get
            {
                return string.Format(TableNameConstants.Formattable.Adjustments.CLASSSpellLevels, CharacterClassConstants.Domains.Trickery);
            }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                SpellConstants.DisguiseSelf,
                SpellConstants.Invisibility,
                SpellConstants.Nondetection,
                SpellConstants.Confusion,
                SpellConstants.FalseVision,
                SpellConstants.Mislead,
                SpellConstants.Screen,
                SpellConstants.PolymorphAnyObject,
                SpellConstants.TimeStop
            };

            AssertCollectionNames(names);
        }

        [Test]
        public void AllTrickerySpellsInAdjustmentsTable()
        {
            var spellGroups = CollectionsMapper.Map(TableNameConstants.Set.Collection.SpellGroups);
            AssertCollectionNames(spellGroups[CharacterClassConstants.Domains.Trickery]);
        }

        [TestCase(SpellConstants.DisguiseSelf, 1)]
        [TestCase(SpellConstants.Invisibility, 2)]
        [TestCase(SpellConstants.Nondetection, 3)]
        [TestCase(SpellConstants.Confusion, 4)]
        [TestCase(SpellConstants.FalseVision, 5)]
        [TestCase(SpellConstants.Mislead, 6)]
        [TestCase(SpellConstants.Screen, 7)]
        [TestCase(SpellConstants.PolymorphAnyObject, 8)]
        [TestCase(SpellConstants.TimeStop, 9)]
        public override void Adjustment(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
