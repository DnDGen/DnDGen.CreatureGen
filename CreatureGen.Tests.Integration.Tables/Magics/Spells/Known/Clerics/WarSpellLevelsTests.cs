using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Tables;
using CreatureGen.Magics;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Magics.Spells.Known.Clerics
{
    [TestFixture]
    public class WarSpellLevelsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get
            {
                return string.Format(TableNameConstants.Formattable.Adjustments.CLASSSpellLevels, CharacterClassConstants.Domains.War);
            }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                SpellConstants.MagicWeapon,
                SpellConstants.SpiritualWeapon,
                SpellConstants.MagicVestment,
                SpellConstants.DivinePower,
                SpellConstants.FlameStrike,
                SpellConstants.BladeBarrier,
                SpellConstants.PowerWordBlind,
                SpellConstants.PowerWordStun,
                SpellConstants.PowerWordKill
            };

            AssertCollectionNames(names);
        }

        [Test]
        public void AllWarSpellsInAdjustmentsTable()
        {
            var spellGroups = CollectionsMapper.Map(TableNameConstants.Set.Collection.SpellGroups);
            AssertCollectionNames(spellGroups[CharacterClassConstants.Domains.War]);
        }

        [TestCase(SpellConstants.MagicWeapon, 1)]
        [TestCase(SpellConstants.SpiritualWeapon, 2)]
        [TestCase(SpellConstants.MagicVestment, 3)]
        [TestCase(SpellConstants.DivinePower, 4)]
        [TestCase(SpellConstants.FlameStrike, 5)]
        [TestCase(SpellConstants.BladeBarrier, 6)]
        [TestCase(SpellConstants.PowerWordBlind, 7)]
        [TestCase(SpellConstants.PowerWordStun, 8)]
        [TestCase(SpellConstants.PowerWordKill, 9)]
        public override void Adjustment(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
